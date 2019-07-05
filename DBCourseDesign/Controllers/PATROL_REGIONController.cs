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
    public class PATROL_REGIONController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/PATROL_REGION
        public IQueryable<PATROL_REGION> GetPATROL_REGION()
        {
            return db.PATROL_REGION;
        }

        // GET: api/PATROL_REGION/5
        [ResponseType(typeof(PATROL_REGION))]
        public async Task<IHttpActionResult> GetPATROL_REGION(string id)
        {
            PATROL_REGION pATROL_REGION = await db.PATROL_REGION.FindAsync(id);
            if (pATROL_REGION == null)
            {
                return NotFound();
            }

            return Ok(pATROL_REGION);
        }

        // PUT: api/PATROL_REGION/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPATROL_REGION(string id, PATROL_REGION pATROL_REGION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pATROL_REGION.PATROL_ID)
            {
                return BadRequest();
            }

            db.Entry(pATROL_REGION).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PATROL_REGIONExists(id))
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

        // POST: api/PATROL_REGION
        [ResponseType(typeof(PATROL_REGION))]
        public async Task<IHttpActionResult> PostPATROL_REGION(PATROL_REGION pATROL_REGION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PATROL_REGION.Add(pATROL_REGION);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PATROL_REGIONExists(pATROL_REGION.PATROL_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pATROL_REGION.PATROL_ID }, pATROL_REGION);
        }

        // DELETE: api/PATROL_REGION/5
        [ResponseType(typeof(PATROL_REGION))]
        public async Task<IHttpActionResult> DeletePATROL_REGION(string id)
        {
            PATROL_REGION pATROL_REGION = await db.PATROL_REGION.FindAsync(id);
            if (pATROL_REGION == null)
            {
                return NotFound();
            }

            db.PATROL_REGION.Remove(pATROL_REGION);
            await db.SaveChangesAsync();

            return Ok(pATROL_REGION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PATROL_REGIONExists(string id)
        {
            return db.PATROL_REGION.Count(e => e.PATROL_ID == id) > 0;
        }
    }
}