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
    public class PATROLsController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/PATROLs
        public IQueryable<PATROL> GetPATROL()
        {
            return db.PATROL;
        }

        // GET: api/PATROLs/5
        [ResponseType(typeof(PATROL))]
        public async Task<IHttpActionResult> GetPATROL(string id)
        {
            PATROL pATROL = await db.PATROL.FindAsync(id);
            if (pATROL == null)
            {
                return NotFound();
            }

            return Ok(pATROL);
        }

        // PUT: api/PATROLs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPATROL(string id, PATROL pATROL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pATROL.ID)
            {
                return BadRequest();
            }

            db.Entry(pATROL).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PATROLExists(id))
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

        // POST: api/PATROLs
        [ResponseType(typeof(PATROL))]
        public async Task<IHttpActionResult> PostPATROL(PATROL pATROL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PATROL.Add(pATROL);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PATROLExists(pATROL.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pATROL.ID }, pATROL);
        }

        // DELETE: api/PATROLs/5
        [ResponseType(typeof(PATROL))]
        public async Task<IHttpActionResult> DeletePATROL(string id)
        {
            PATROL pATROL = await db.PATROL.FindAsync(id);
            if (pATROL == null)
            {
                return NotFound();
            }

            db.PATROL.Remove(pATROL);
            await db.SaveChangesAsync();

            return Ok(pATROL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PATROLExists(string id)
        {
            return db.PATROL.Count(e => e.ID == id) > 0;
        }
    }
}