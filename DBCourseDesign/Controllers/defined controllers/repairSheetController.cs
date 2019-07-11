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
            var result = new List<repairSheetDto>();
            foreach (var r in db.REPAIR_ORDER)
            {
                var details = r.DESCRIPTION.Split((char)30);
                result.Add(new repairSheetDto()
                {
                    title = "RS" + r.ID,
                    cover = r.REPORT_PICTURE,
                    state = r.STATUS,
                    type = "维修类型：" + r.REPAIR_TYPE,
                    details = details[0],
                    stuffNeeded = details.Count() <= 1 ? null : details[1],
                    telNumber = r.TEL_NUMBER
                });
            }
            return returnHelper.make(result);
        }
    }
}