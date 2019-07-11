﻿using System;
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
    public class workController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        /// <summary>
        /// return all the work sheets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/sheets/checkSheet")]
        public returnDto<IQueryable<checkSheetDto>> GetCheckSheets()
        {
            var checkSheets = db.PATROL_LOG.Join(db.STAFF, p => p.PATROL_ID, s => s.ID, (p, staff) => new { p, staff }).Join
                (db.EQ_IN_USE, p =>p.p.EQ_ID, e => e.ID, (p, EQ) => new checkSheetDto()
                {
                    id = "PS" + p.p.ID,
                    checkArea = EQ.ADDRESS,
                    checkPic = p.p.PATROL_PICTURE,
                    checkTime = p.p.PATROL_TIME,
                    eqID = "EQ" + EQ.ID,
                    patrolID = "ST" + p.staff.ID,
                    patrolName = p.staff.NAME
                });
            return returnHelper.make(checkSheets);
        }

        /// <summary>
        /// remove target workSheet from Database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/sheets/checkSheetRow")]
        public async Task<IHttpActionResult> DeleteCheckSheet(stringReceiver id)
        {
            try
            {
                PATROL_LOG patrol_log = await db.PATROL_LOG.FindAsync(id.decoded());
                if (patrol_log == null)
                {
                    throw new ApplicationException();
                }
                db.PATROL_LOG.Remove(patrol_log);
                await db.SaveChangesAsync();
                var checkSheets = GetCheckSheets().data.ToList();
                return Ok(returnHelper.make(checkSheets));
            }
            catch (Exception)
            {
                return Ok(returnHelper.fail());
            }
        }
    }
}