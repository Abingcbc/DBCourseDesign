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
        //[HttpPost]
        //[Route("api/sheets/scheduleDetail")]
        //public async Task<IHttpActionResult> scehduleRepairSheet(repairSheetReceiver input)
        //{
        //    try
        //    {
        //        input.stfId = input.stfId.Substring(2);
        //        input.RSTid = input.RSTid.Substring(2);
        //        var repairSheet = await db.REPAIR_ORDER.FindAsync(input);
        //        if (repairSheet == null)
        //            throw new Exception();
        //        db.Entry(repairSheet).Reference(e => e.EQ_IN_USE).Load();
        //        var oldEq = repairSheet.EQ_IN_USE;
        //        decimal?[] oldEqLocation = new decimal?[2] { repairSheet.EQ_IN_USE.LATITUDE, repairSheet.EQ_IN_USE.LONGITUDE };
        //        foreach(var requirement in input.ls)
        //        {
        //            if(requirement.statue == "器材")
        //            {
        //                var neededEqType = await db.EQ_TYPE.FirstOrDefaultAsync(e => e.TYPE_NAME == requirement.type && e.MODEL_NUMBER == requirement.model);
        //                string neededEqTypeId = neededEqType.ID;
        //                var availableList = db.EQ_STORED.Where(e => e.EQ_TYPE_ID == neededEqTypeId).Include(e => e.WAREHOUSE).ToList();
        //                availableList.OrderBy(w => Math.Pow((double)w.WAREHOUSE.LATITUDE - (double)oldEqLocation[0], 2) + Math.Pow((double)w.WAREHOUSE.LONGITUDE - (double)oldEqLocation[1], 2));
        //                var nearest =  availableList.First();
        //                var newUsing = new EQ_IN_USE
        //                {
        //                    ID = "0",
        //                    QR_CODE = "null",
        //                    ADDRESS = old
        //                }
        //            }
        //            else
        //            {

        //            }
        //        }
        //    } 
        //    foreach (var r in db.REPAIR_ORDER)
        //    {
        //        var details = r.DESCRIPTION.Split((char)30);
        //        result.Add(new repairSheetDto()
        //        {
        //            title = "RS" + r.ID,
        //            cover = r.REPORT_PICTURE,
        //            state = r.STATUS,
        //            type = "维修类型：" + r.REPAIR_TYPE,
        //            details = details[0],
        //            stuffNeeded = details.Count() <= 1 ? null : details[1],
        //            telNumber = r.TEL_NUMBER
        //        });
        //    }
        //    return returnHelper.make(result);
        //}
    }
}