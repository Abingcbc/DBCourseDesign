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
    public class EQ_STOREDController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/EQ_STORED
        public IQueryable<EQ_STORED> GetEQ_STORED()
        {
            return db.EQ_STORED;
        }

        // GET: api/EQ_STORED/5
        [ResponseType(typeof(EQ_STORED))]
        public async Task<IHttpActionResult> GetEQ_STORED(string id)
        {
            EQ_STORED eQ_STORED = await db.EQ_STORED.FindAsync(id);
            if (eQ_STORED == null)
            {
                return NotFound();
            }

            return Ok(eQ_STORED);
        }

        // PUT: api/EQ_STORED/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEQ_STORED(string id, EQ_STORED eQ_STORED)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eQ_STORED.ID)
            {
                return BadRequest();
            }

            db.Entry(eQ_STORED).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQ_STOREDExists(id))
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

        // POST: api/EQ_STORED
        [ResponseType(typeof(EQ_STORED))]
        public async Task<IHttpActionResult> PostEQ_STORED(EQ_STORED eQ_STORED)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQ_STORED.Add(eQ_STORED);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EQ_STOREDExists(eQ_STORED.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eQ_STORED.ID }, eQ_STORED);
        }

        // DELETE: api/EQ_STORED/5
        [ResponseType(typeof(EQ_STORED))]
        public async Task<IHttpActionResult> DeleteEQ_STORED(string id)
        {
            EQ_STORED eQ_STORED = await db.EQ_STORED.FindAsync(id);
            if (eQ_STORED == null)
            {
                return NotFound();
            }

            db.EQ_STORED.Remove(eQ_STORED);
            await db.SaveChangesAsync();

            return Ok(eQ_STORED);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQ_STOREDExists(string id)
        {
            return db.EQ_STORED.Count(e => e.ID == id) > 0;
        }
    }
}