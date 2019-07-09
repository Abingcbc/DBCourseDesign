//wyc
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
    public class EqController : ApiController
    {
        private FEMSContext db = new FEMSContext();
        //wyc
        [HttpGet]
        [Route("api/equipment/stored")]
        public IQueryable<detailedEQStorageDto> GetEqStored()
        {
            var result = db.EQ_STORED.Include(e => e.WAREHOUSE).Include(e => e.EQ_TYPE).Select<EQ_STORED, detailedEQStorageDto>
                (e => new detailedEQStorageDto
                {
                    icon = e.EQ_TYPE.PICTURE,
                    id = e.ID,
                    modelID = e.EQ_TYPE.MODEL_NUMBER,
                    name = e.EQ_TYPE.TYPE_NAME,
                    price = e.EQ_TYPE.PRICE.ToString(),
                    productTime = e.PRODUCT_TIME,
                    status = e.STATUS == "正常" ? "0" : "1",
                    warehouseID = e.WAREHOUSE.NAME
                });
            return result;
        }

        //wyc
        [HttpGet]
        [Route("api/equipment/using")]
        public IQueryable<EqInUseDto> GetEqUsing()
        {
            var result = db.EQ_IN_USE.Include(e => e.EQ_TYPE).Select<EQ_IN_USE, EqInUseDto>
                (e => new EqInUseDto
                {
                    id = e.ID,
                    type = e.EQ_TYPE.TYPE_NAME,
                    status = e.STATUS,
                    model = e.EQ_TYPE.MODEL_NUMBER,
                    address = e.ADDRESS
                });
            return result;
        }

        //wyc
        [HttpPost]
        [Route("api/equipment/detail")]
        [ResponseType(typeof(detailedEqInUseDto))]
        public async Task<IHttpActionResult> GetEqUsingDetail(string id)
        {
            var eq = await db.EQ_IN_USE.Include(e => e.EQ_TYPE).FirstOrDefaultAsync(e => e.ID == id);
            var result = new detailedEqInUseDto()
            {
                name = eq.EQ_TYPE.TYPE_NAME,
                factory_time = eq.PRODUCTION_TIME,
                install_time = eq.INSTALL_TIME,
                unit = eq.MANAGER,
                address = eq.ADDRESS,
                if_damage = eq.STATUS,
                QRcode = eq.QR_CODE
                //order

            };
            return Ok(result);
        }

        //wyc
        [HttpPost]
        [Route("api/equipment/using")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> addEqStored(eqStoredReceiver input)
        {
            var type = await db.EQ_TYPE.FirstOrDefaultAsync(e => e.TYPE_NAME == input.name && e.MODEL_NUMBER == input.model);
            var warehouse = await db.WAREHOUSE.FirstOrDefaultAsync(e => e.NAME == input.warehouse);
            var result = new EQ_STORED
            {
                PRODUCT_TIME = input.productTime,
                STATUS = "0",
                EQ_TYPE_ID = type.ID,
                WAREHOUSE_ID = warehouse.ID           
            };
            db.EQ_STORED.Add(result);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}