//hpy

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
    public class WarehouseAccessoryController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        [HttpGet]
        [Route("api/accessory/allAccessory")]
        public IQueryable<detailedAccessoryStorageDto> GetAllAccessories()
        {
            var result = db.ACCESSORY_STORED.Include(acSt => acSt.ACCESSORY).Include(acSt => acSt.WAREHOUSE).Select
                ((acSt) => new detailedAccessoryStorageDto()
                {
                    accessoryId = acSt.ACCESSORY_ID,
                    num = acSt.QUANTITY,
                    model = acSt.ACCESSORY.MODEL_NUMBER,
                    type = acSt.ACCESSORY.TYPE_NAME,
                    price = acSt.ACCESSORY.PRICE,
                    warehouse = acSt.WAREHOUSE.NAME
                });
            return result;
        }

        [HttpPost]
        [Route("api/accessory/addAccessory")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> addAccessory(addAccessoryReceiver input)
        {
            WAREHOUSE targetWarehouse;
            try
            {
                targetWarehouse = await db.WAREHOUSE.FirstAsync(e => e.NAME == input.warehouse);
                if (targetWarehouse == null)
                    throw new ApplicationException();
                var AccessoriesInTarget = await db.ACCESSORY_STORED.FirstOrDefaultAsync(e => e.WAREHOUSE_ID == targetWarehouse.ID && e.ACCESSORY_ID == input.id);
                if (AccessoriesInTarget == null)
                    db.ACCESSORY_STORED.Add(
                        new ACCESSORY_STORED
                        {
                            ACCESSORY_ID = input.id,
                            QUANTITY = input.num,
                            WAREHOUSE_ID = targetWarehouse.ID,
                        });
                else
                    AccessoriesInTarget.QUANTITY += input.num;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}