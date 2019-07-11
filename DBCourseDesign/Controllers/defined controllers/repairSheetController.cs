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


        [HttpPost]
        [Route("api/sheets/scheduleDetail")]
        public async Task<IHttpActionResult> scehduleRepairSheet(repairSheetReceiver input)
        {
            try
            {
                input.stfId = input.stfId.Substring(2);
                input.RSTid = input.RSTid.Substring(2);
                var repairSheet = await db.REPAIR_ORDER.FindAsync(input);
                if (repairSheet == null)
                    throw new Exception();
                db.Entry(repairSheet).Reference(e => e.EQ_IN_USE).Load();
                var oldEq = repairSheet.EQ_IN_USE;
                oldEq.STATUS = "0";
                decimal?[] oldEqLocation = new decimal?[2] { repairSheet.EQ_IN_USE.LATITUDE, repairSheet.EQ_IN_USE.LONGITUDE };
                foreach (var requirement in input.ls)
                {
                    if (requirement.statue == "器材")
                    {
                        var neededEqType = await db.EQ_TYPE.FirstOrDefaultAsync(e => e.TYPE_NAME == requirement.type && e.MODEL_NUMBER == requirement.model);
                        string neededEqTypeId = neededEqType.ID;
                        var availableList = db.EQ_STORED.Where(e => e.EQ_TYPE_ID == neededEqTypeId).Include(e => e.WAREHOUSE).ToList();
                        availableList.OrderBy(w => Math.Pow((double)w.WAREHOUSE.LATITUDE - (double)oldEqLocation[0], 2) + Math.Pow((double)w.WAREHOUSE.LONGITUDE - (double)oldEqLocation[1], 2));
                        var nearest = availableList.First();
                        db.EQ_STORED.Remove(nearest);
                    }
                    else
                    {
                        var neededAcType = await db.ACCESSORY.FirstOrDefaultAsync(e => e.TYPE_NAME == requirement.type && e.MODEL_NUMBER == requirement.model);
                        string neededAcTypeId = neededAcType.ID;
                        var availableList = db.ACCESSORY_STORED.Where(e => e.ACCESSORY_ID == neededAcTypeId).Include(e => e.WAREHOUSE).ToList();
                        availableList.OrderBy(w => Math.Pow((double)w.WAREHOUSE.LATITUDE - (double)oldEqLocation[0], 2) + Math.Pow((double)w.WAREHOUSE.LONGITUDE - (double)oldEqLocation[1], 2));
                        var deleteAcStoreList = new List<ACCESSORY_STORED>();
                        int unsolved = int.Parse(requirement.number);
                        for (int i = 0; i != availableList.Count(); ++i)
                        {
                            if (availableList[i].QUANTITY > unsolved)
                            {
                                availableList[i].QUANTITY -= unsolved;
                                unsolved = 0;
                                break;
                            }
                            else if (availableList[i].QUANTITY == unsolved)
                            {
                                db.ACCESSORY_STORED.Remove(availableList[i]);
                                unsolved = 0;
                                break;
                            }
                            else
                            {
                                unsolved -= availableList[i].QUANTITY;
                                db.ACCESSORY_STORED.Remove(availableList[i]);
                            }
                        }
                        if (unsolved != 0)
                            throw new Exception();
                    }
                }
                var workSheet = new WORK_ORDER
                {
                    ID = "0",
                    DISPATCHER_ID = repairSheet.DISPATCHER_ID,
                    EQ_ID = repairSheet.EQ_ID,
                    INSERT_TIME = DateTime.Now,
                    INSERT_BY = repairSheet.DISPATCHER_ID,
                    REPAIRER_ID = input.stfId,
                    WORK_PICTURE = dbConsts.unUploadUrl,
                    STATUS = "0"
                };
                db.WORK_ORDER.Add(workSheet);
                repairSheet.STATUS = "2";
                await db.SaveChangesAsync();
                return Ok(returnHelper.make(""));
            }
            catch(Exception)
            {
                return (Ok(returnHelper.fail()));
            }
        }


        [HttpGet]
        [Route("api/sheets/repairSheetDetail")]
        public async Task<IHttpActionResult> getEqNacNrepairer()
        {
            try
            {
                var result = new eqNacNrepairerDto();
                result.staff = db.REPAIRER.Include(e => e.STAFF).Select(e => new staffIdAndName
                {
                    staffId = e.ID,
                    staffName = e.STAFF.NAME
                }).ToList();
                result.equipType = db.EQ_STORED.Join(db.EQ_TYPE, s => s.EQ_TYPE_ID, t => t.ID, (s, t) => new { t.TYPE_NAME, t.MODEL_NUMBER, s.ID }).GroupBy(
                    e => new { e.MODEL_NUMBER, e.TYPE_NAME }).Select(e => new storageAndNum
                    { model = e.Key.MODEL_NUMBER, type = e.Key.TYPE_NAME, number = e.Count() }).ToList();
                for(int i=0;i!=result.equipType.Count();++i)
                {
                    result.equipType[i].no = i;
                }
                result.accessory = db.ACCESSORY_STORED.Join(db.ACCESSORY, s => s.ACCESSORY_ID, t => t.ID, (s, t) => new { t.TYPE_NAME, t.MODEL_NUMBER, s.QUANTITY }).GroupBy(
                    e => new { e.MODEL_NUMBER, e.TYPE_NAME }).Select(e => new storageAndNum
                    { model = e.Key.MODEL_NUMBER, type = e.Key.TYPE_NAME, number = e.Sum(a => a.QUANTITY)}).ToList();
                for (int i = 0; i != result.accessory.Count(); ++i)
                {
                    result.accessory[i].no = i;
                }
                return Ok(returnHelper.make(result));
            }
            catch (Exception)
            {

                //return (Ok(returnHelper.fail()));
                throw;
            }
        }
    }
}