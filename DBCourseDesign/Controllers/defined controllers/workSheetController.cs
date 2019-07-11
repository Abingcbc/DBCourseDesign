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
    public class sheetsController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        /// <summary>
        /// return all the work sheets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/sheets/workSheet")]
        public returnDto<List<workSheetDto>> GetWorkSheets()
        {
            var workSheets = db.WORK_ORDER.Join(db.STAFF, w => w.REPAIRER_ID, s => s.ID, (w, repairer) => new { w, repairer }).Join
                (db.STAFF, w => w.w.DISPATCHER_ID, s => s.ID, (w, dispatcher) => new { w, dispatcher }).Join
                (db.EQ_IN_USE, w => w.w.w.EQ_ID, e => e.ID, (w, e) => new workSheetDto()
                {
                    id =  w.w.w.ID,
                    dispatcherID = w.w.w.DISPATCHER_ID,
                    equipID = w.w.w.EQ_ID,
                    repairArea = e.ADDRESS,
                    repairerID = w.w.w.REPAIRER_ID,
                    statue = w.w.w.STATUS,
                    work_picture = w.w.w.WORK_PICTURE,
                    repairerName = w.w.repairer.NAME,
                    dispatcherName = w.dispatcher.NAME
                }).ToList();
            foreach (var record in workSheets)
            {
                record.id = "WS" + record.id;
                record.equipID = "EQ" + record.equipID;
                record.dispatcherID = "ST" + record.dispatcherID;
                record.repairerID = "ST" + record.repairerID;
            }
            return returnHelper.make(workSheets);
        }

        /// <summary>
        /// remove target workSheet from Database
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/sheets/workSheetRow")]
        public async Task<IHttpActionResult> deleteWorkSheet(stringReceiver sR)
        {
            string id = sR.decoded();
            try
            {
                WORK_ORDER wORK_ORDER = await db.WORK_ORDER.FindAsync(id);
                if (wORK_ORDER == null)
                {
                    throw new ApplicationException();
                }
                db.WORK_ORDER.Remove(wORK_ORDER);
                await db.SaveChangesAsync();
                var workSheets = GetWorkSheets().data.ToList();
                return Ok(returnHelper.make(workSheets));
            }
            catch (Exception)
            {
                return Ok(returnHelper.fail());
            }
        }
    }
}