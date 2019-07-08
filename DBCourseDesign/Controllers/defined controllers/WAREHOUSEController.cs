using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DBCourseDesign.Models;

namespace DBCourseDesign.Controllers
{
    public class WAREHOUSEController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/WAREHOUSE/
        [HttpGet]
        [Route("api/WAREHOUSE/preview")]
        public IQueryable<WAREHOUSEPreviewDto> GetPriveiw()
        {
            var warehouse = db.WAREHOUSE.Include(b => b.AREA).Select<WAREHOUSE, WAREHOUSEPreviewDto>
                (e => new WAREHOUSEPreviewDto { id = e.ID, name = e.NAME, address = e.REGION.COUNTY });
            return warehouse;
        }

        [HttpPost]
        [Route("api/WAREHOUSE/DETAIL")]
        [ResponseType(typeof(WAREHOUSEDetailDto))]
        public async Task<IHttpActionResult> GetDetail(string id)
        {
            var warehouse = db.WAREHOUSE.Find(id);
            if (warehouse == null)
                return NotFound();
            db.Entry(warehouse).Reference(p => p.REGION).Load();
            var dto = new WAREHOUSEDetailDto()
            {
                name = warehouse.NAME,
                address = warehouse.REGION.COUNTY,
                detailedAddress = warehouse.LOCATION
            };
            return Ok(dto);
        }

        [HttpGet]
        [Route("api/WAREHOUSE/allAddress")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> GetAllCountiesWithWarehouse()
        {
            var result = db.WAREHOUSE
            .Join(db.REGION, w => w.REGION_ID, g => g.ID, (w, g) => g.COUNTY)
            .OrderBy(item => item).Distinct().ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/WAREHOUSE/allWarehouse")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> GetAllWarehouses()
        {
            //without distinct to check errors
            var result = db.WAREHOUSE.Select(e => e.NAME)
            .OrderBy(item => item).ToList();
            return Ok(result);
        }

        [HttpPost]
        [Route("api/WAREHOUSE/goods")]
        [ResponseType(typeof(WAREHOUSEStorageDto))]
        public async Task<IHttpActionResult> GetStorage(string id)
        {
            //without distinct to check errors
            var warehouse = db.WAREHOUSE.Find(id);
            if (warehouse == null)
                return NotFound();
            var warehouseTable = new List<WAREHOUSE>();
            warehouseTable.Add(warehouse);
            var accessories = GetStoredAccessories(warehouse);
            var eqs = GetStoredEQ(warehouse);
            var dto = new WAREHOUSEStorageDto()
            {
                accessory = accessories,
                equipment = eqs
            };
            return Ok(dto);
        }

        private List<EQStorageDto> GetStoredEQ(WAREHOUSE warehouse)
        {
            var warehouseTable = new List<WAREHOUSE>();
            warehouseTable.Add(warehouse);
            var eqs = warehouseTable.Join(db.EQ_STORED, w => w.ID, g => g.WAREHOUSE_ID,
                (w, g) => new { g.ID, g.EQ_TYPE_ID });
            var temp1 = eqs.ToList();
            var temp = eqs
                .Join(db.EQ_TYPE, w => w.EQ_TYPE_ID, e => e.ID,
                (w, e) => new EQStorageDto() { id = w.ID, model = e.MODEL_NUMBER, type = e.TYPE_NAME }).ToList();
            return temp;
        }

        private List<ACCESSORYStorageDto> GetStoredAccessories(WAREHOUSE warehouse)
        {
            var warehouseTable = new List<WAREHOUSE>();
            warehouseTable.Add(warehouse);
            var accessories = warehouseTable.Join(db.ACCESSORY_STORED, w => w.ID, g => g.WAREHOUSE_ID,
                (w, g) => new { g.ACCESSORY_ID, g.QUANTITY }).Join(db.ACCESSORY, w => w.ACCESSORY_ID, a => a.ID,
                (w, a) => new ACCESSORYStorageDto() { id = w.ACCESSORY_ID, model = a.MODEL_NUMBER, type = a.TYPE_NAME, number = w.QUANTITY }).ToList();
            return accessories;
        }


        [HttpPost]
        [Route("api/WAREHOUSE/schedule")]
        [ResponseType(typeof(WAREHOUSEStorageDto))]
        public async Task<IHttpActionResult> dispatchAccessory(ACCESSORYDispatchReceiver input)
        {
            WAREHOUSE originalWarehouse;
            try
            {
                originalWarehouse = await db.WAREHOUSE.FirstAsync(e => e.NAME == input.from);
                if (input.type.ToLower() == "equipment")
                {
                    var record = db.EQ_STORED.Find(input.id);
                    if (record == null)
                        throw new ApplicationException();
                    db.Entry(record).Reference(e => e.WAREHOUSE).Load();
                    if (record.WAREHOUSE.NAME != input.from)
                    {
                        throw new ApplicationException();
                    }
                    var targetWarehouse = await db.WAREHOUSE.FirstAsync(e => e.NAME == input.to);
                    record.WAREHOUSE = targetWarehouse;
                }
                else
                {
                    if (originalWarehouse == null)
                        throw new ApplicationException();
                    var record = await db.ACCESSORY_STORED.FirstAsync(e => e.ACCESSORY_ID == input.id && e.WAREHOUSE_ID == originalWarehouse.ID);
                    if (record == null || record.QUANTITY < input.num)
                        throw new ApplicationException();
                    if (record.QUANTITY == input.num)
                        db.ACCESSORY_STORED.Remove(record);
                    else
                        //not sure if the change will be saved to database
                        record.QUANTITY -= input.num;
                    //debug
                    var temp = db.Entry(record).State;
                    var targetWarehouse = await db.WAREHOUSE.FirstAsync(e => e.NAME == input.to);
                    //accessories already in target warehouse
                    var AccessoriesInTarget = db.ACCESSORY_STORED.First(e => e.WAREHOUSE_ID == targetWarehouse.ID && e.ACCESSORY_ID == input.id);
                    if (AccessoriesInTarget == null)
                        db.ACCESSORY_STORED.Add(
                            new ACCESSORY_STORED
                            {
                                ACCESSORY_ID = input.id,
                                QUANTITY = input.num,
                                WAREHOUSE_ID = targetWarehouse.ID
                            });
                    else
                        AccessoriesInTarget.QUANTITY += input.num;
                }
                await db.SaveChangesAsync();
                
                if (input.type.ToLower() == "equipment")
                {
                    return Ok(GetStoredEQ(originalWarehouse));
                }
                else
                {
                    return Ok(GetStoredAccessories(originalWarehouse));
                }
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}