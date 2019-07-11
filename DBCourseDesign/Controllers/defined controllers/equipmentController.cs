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
        public async Task<IHttpActionResult> GetEqStored()
        {
            var result = db.EQ_STORED.Include(e => e.WAREHOUSE).Include(e => e.EQ_TYPE).Select<EQ_STORED, detailedEQStorageDto>
                (e => new detailedEQStorageDto
                {
                    icon = e.EQ_TYPE.PICTURE,
                    id = "EQ" + e.ID,
                    model = e.EQ_TYPE.MODEL_NUMBER,
                    name = e.EQ_TYPE.TYPE_NAME,
                    price = e.EQ_TYPE.PRICE.ToString(),
                    productTime = e.PRODUCT_TIME,
                    status = e.STATUS,
                    warehouse = e.WAREHOUSE.NAME
                });
            return Ok(returnHelper.make(result));
        }

        //wyc
        [HttpGet]
        [Route("api/equipment/using")]
        public async Task<IHttpActionResult> GetEqUsing()
        {
            var result = db.EQ_IN_USE.Include(e => e.EQ_TYPE).Select<EQ_IN_USE, EqInUseDto>
                (e => new EqInUseDto
                {
                    id = "EQ" + e.ID,
                    type = e.EQ_TYPE.TYPE_NAME,
                    status = e.STATUS,
                    model = e.EQ_TYPE.MODEL_NUMBER,
                    address = e.ADDRESS
                });
            return Ok(returnHelper.make(result));
        }

        //wyc
        [HttpPost]
        [Route("api/equipment/detail")]
        [ResponseType(typeof(returnDto<detailedEqInUseDto>))]
        public async Task<IHttpActionResult> GetEqUsingDetail(stringReceiver id)
        {
            var eq = await db.EQ_IN_USE.Include(e => e.EQ_TYPE).FirstOrDefaultAsync(e => e.ID == id.id);
            var result = new detailedEqInUseDto()
            {
                name = eq.EQ_TYPE.TYPE_NAME,
                factory_time = eq.PRODUCTION_TIME,
                install_time = eq.INSTALL_TIME,
                unit = eq.MANAGER,
                address = eq.ADDRESS,
                if_damage = eq.STATUS,
                QRcode = eq.QR_CODE,
                icon = eq.EQ_TYPE.PICTURE

            };
            return Ok(returnHelper.make(result));
        }

        //wyc
        [HttpPost]
        [Route("api/equipment/stored")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> addEqStored(eqStoredReceiver input)
        {
            var type = await db.EQ_TYPE.FirstOrDefaultAsync(e => e.TYPE_NAME == input.name && e.MODEL_NUMBER == input.model);
            var warehouse = await db.WAREHOUSE.FirstOrDefaultAsync(e => e.NAME == input.warehouse);
            var result = new EQ_STORED()
            {
                PRODUCT_TIME = input.productTime,
                STATUS = "0",
                EQ_TYPE_ID = "EQ" + type.ID,
                WAREHOUSE_ID = "WH" + warehouse.ID,
                ID = "placeHolder"
            };
            db.EQ_STORED.Add(result);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("api/equipment/allType")]
        [ResponseType(typeof(returnDto<List<string>>))]
        public async Task<IHttpActionResult> GetAllTypes()
        {
            //without distinct to check errors
            var result = db.EQ_TYPE.Select(a => a.TYPE_NAME)
            .OrderBy(item => item).Distinct().ToList();
            return Ok(returnHelper.make(result));
        }
    }
}