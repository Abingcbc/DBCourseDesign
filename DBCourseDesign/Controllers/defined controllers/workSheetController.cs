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
        public IQueryable<workSheetDto> GetWorkSheets()
        {
            var workSheets = db.WORK_ORDER.Join(db.EQ_IN_USE, w => w.EQ_ID, e => e.ID,
                (w, e) => new workSheetDto()
                {
                    id = w.ID,
                    dispatcherID = w.DISPATCHER_ID,
                    equipID = w.EQ_ID,
                    repairArea = e.ADDRESS,
                    repairerID = w.REPAIRER_ID,
                    status = w.STATUS,
                    work_picture = w.WORK_PICTURE
                });
            return workSheets;
        }

        /// <summary>
        /// remove target workSheet from Database
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/sheets/workSheet")]
        [ResponseType(typeof(deleteWorkSheetDto))]
        public async Task<IHttpActionResult> GetWorkSheet(string id)
        {
            try
            {
                WORK_ORDER wORK_ORDER = await db.WORK_ORDER.FindAsync(id);
                if (wORK_ORDER == null)
                {
                    throw new ApplicationException();
                }
                db.WORK_ORDER.Remove(wORK_ORDER);
                var workSheets = GetWorkSheets().ToList();
                var result = new deleteWorkSheetDto()
                {
                    data = workSheets,
                    deleteInfo = "ok"
                };
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok(new deleteWorkSheetDto()
                {
                    deleteInfo = "false"
                });
            }
        }
    }
}