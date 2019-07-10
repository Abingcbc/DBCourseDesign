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
    public class repairSheetController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        [HttpGet]
        [Route("api/repairSheet/allRepairSheet")]
        public returnDto<List<repairSheetDto>> GetAllRepairSheets()
        {
            var result = db.REPAIR_ORDER.Select(r => new repairSheetDto
            {
                cover = r.REPORT_PICTURE,
                id = r.ID,
                state = "状态：" + r.STATUS,
                title = "ID：" + r.ID,
                type = "维修类型：" + r.REPAIR_TYPE
            }).ToList();
            return returnHelper.make(result);

        }
    }
}