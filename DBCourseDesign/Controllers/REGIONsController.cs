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
    public class REGIONsController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/REGIONs
        public IQueryable<REGION> GetREGION()
        {
            return db.REGION;
        }

        // GET: api/REGIONs/5
        [ResponseType(typeof(REGION))]
        public async Task<IHttpActionResult> GetREGION(string id)
        {
            REGION rEGION = await db.REGION.FindAsync(id);
            if (rEGION == null)
            {
                return NotFound();
            }

            return Ok(rEGION);
        }

        // PUT: api/REGIONs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutREGION(string id, REGION rEGION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rEGION.ID)
            {
                return BadRequest();
            }

            db.Entry(rEGION).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!REGIONExists(id))
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

        // POST: api/REGIONs
        [ResponseType(typeof(REGION))]
        public async Task<IHttpActionResult> PostREGION(REGION rEGION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.REGION.Add(rEGION);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (REGIONExists(rEGION.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rEGION.ID }, rEGION);
        }

        // DELETE: api/REGIONs/5
        [ResponseType(typeof(REGION))]
        public async Task<IHttpActionResult> DeleteREGION(string id)
        {
            REGION rEGION = await db.REGION.FindAsync(id);
            if (rEGION == null)
            {
                return NotFound();
            }

            db.REGION.Remove(rEGION);
            await db.SaveChangesAsync();

            return Ok(rEGION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool REGIONExists(string id)
        {
            return db.REGION.Count(e => e.ID == id) > 0;
        }
    }
}