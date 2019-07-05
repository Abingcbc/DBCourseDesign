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
    public class PATROL_LOGController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/PATROL_LOG
        public IQueryable<PATROL_LOG> GetPATROL_LOG()
        {
            return db.PATROL_LOG;
        }

        // GET: api/PATROL_LOG/5
        [ResponseType(typeof(PATROL_LOG))]
        public async Task<IHttpActionResult> GetPATROL_LOG(string id)
        {
            PATROL_LOG pATROL_LOG = await db.PATROL_LOG.FindAsync(id);
            if (pATROL_LOG == null)
            {
                return NotFound();
            }

            return Ok(pATROL_LOG);
        }

        // PUT: api/PATROL_LOG/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPATROL_LOG(string id, PATROL_LOG pATROL_LOG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pATROL_LOG.ID)
            {
                return BadRequest();
            }

            db.Entry(pATROL_LOG).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PATROL_LOGExists(id))
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

        // POST: api/PATROL_LOG
        [ResponseType(typeof(PATROL_LOG))]
        public async Task<IHttpActionResult> PostPATROL_LOG(PATROL_LOG pATROL_LOG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PATROL_LOG.Add(pATROL_LOG);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PATROL_LOGExists(pATROL_LOG.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pATROL_LOG.ID }, pATROL_LOG);
        }

        // DELETE: api/PATROL_LOG/5
        [ResponseType(typeof(PATROL_LOG))]
        public async Task<IHttpActionResult> DeletePATROL_LOG(string id)
        {
            PATROL_LOG pATROL_LOG = await db.PATROL_LOG.FindAsync(id);
            if (pATROL_LOG == null)
            {
                return NotFound();
            }

            db.PATROL_LOG.Remove(pATROL_LOG);
            await db.SaveChangesAsync();

            return Ok(pATROL_LOG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PATROL_LOGExists(string id)
        {
            return db.PATROL_LOG.Count(e => e.ID == id) > 0;
        }
    }
}