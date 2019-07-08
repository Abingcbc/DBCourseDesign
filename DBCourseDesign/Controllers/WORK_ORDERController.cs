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
    public class WORK_ORDERController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/WORK_ORDER
        public IQueryable<WORK_ORDER> GetWORK_ORDER()
        {
            return db.WORK_ORDER;
        }

        // GET: api/WORK_ORDER/5
        [ResponseType(typeof(WORK_ORDER))]
        public async Task<IHttpActionResult> GetWORK_ORDER(string id)
        {
            WORK_ORDER wORK_ORDER = await db.WORK_ORDER.FindAsync(id);
            if (wORK_ORDER == null)
            {
                return NotFound();
            }

            return Ok(wORK_ORDER);
        }

        // PUT: api/WORK_ORDER/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWORK_ORDER(string id, WORK_ORDER wORK_ORDER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != wORK_ORDER.ID)
            {
                return BadRequest();
            }

            db.Entry(wORK_ORDER).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WORK_ORDERExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WORK_ORDER
        [ResponseType(typeof(WORK_ORDER))]
        public async Task<IHttpActionResult> PostWORK_ORDER(WORK_ORDER wORK_ORDER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WORK_ORDER.Add(wORK_ORDER);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WORK_ORDERExists(wORK_ORDER.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = wORK_ORDER.ID }, wORK_ORDER);
        }

        // DELETE: api/WORK_ORDER/5
        [ResponseType(typeof(WORK_ORDER))]
        public async Task<IHttpActionResult> DeleteWORK_ORDER(string id)
        {
            WORK_ORDER wORK_ORDER = await db.WORK_ORDER.FindAsync(id);
            if (wORK_ORDER == null)
            {
                return NotFound();
            }

            db.WORK_ORDER.Remove(wORK_ORDER);
            await db.SaveChangesAsync();

            return Ok(wORK_ORDER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WORK_ORDERExists(string id)
        {
            return db.WORK_ORDER.Count(e => e.ID == id) > 0;
        }
    }
}