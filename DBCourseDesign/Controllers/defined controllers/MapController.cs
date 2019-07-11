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
    public class MapController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/map/getAll
        [HttpGet]
        [Route("api/map/getAll")]
        public returnDto<MapDto> GetPriveiw()
        {
            var result = new MapDto();
            result.warehouse = db.WAREHOUSE.Select
                (e => new MapWarehouseDto
                {
                    lat = e.LATITUDE,
                    lon = e.LONGITUDE,
                    detailedAddress = e.LOCATION,
                    id = "WA" + e.ID,
                    name = e.NAME
                }).ToList();
            result.usingEquipment = db.EQ_IN_USE.Include(e => e.EQ_TYPE).Select(e => new MapEqDto()
            {
                detailedAddress = e.ADDRESS,
                lat = e.LATITUDE,
                lon = e.LONGITUDE,
                model = e.EQ_TYPE.MODEL_NUMBER,
                type = e.EQ_TYPE.TYPE_NAME,
                qrCode = e.QR_CODE,
                pic = e.EQ_TYPE.PICTURE,
                id = "EQ" + e.ID
            }).ToList();
            return returnHelper.make(result);
        }
    }
}