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
    public class ACCESSORY_STOREDController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/ACCESSORY_STORED
        public IQueryable<ACCESSORY_STORED> GetACCESSORY_STORED()
        {
            return db.ACCESSORY_STORED;
        }

        // GET: api/ACCESSORY_STORED/5
        [ResponseType(typeof(ACCESSORY_STORED))]
        public async Task<IHttpActionResult> GetACCESSORY_STORED(string id)
        {
            ACCESSORY_STORED aCCESSORY_STORED = await db.ACCESSORY_STORED.FindAsync(id);
            if (aCCESSORY_STORED == null)
            {
                return NotFound();
            }

            return Ok(aCCESSORY_STORED);
        }

        // PUT: api/ACCESSORY_STORED/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutACCESSORY_STORED(string id, ACCESSORY_STORED aCCESSORY_STORED)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aCCESSORY_STORED.ACCESSORY_ID)
            {
                return BadRequest();
            }

            db.Entry(aCCESSORY_STORED).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ACCESSORY_STOREDExists(id))
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

        // POST: api/ACCESSORY_STORED
        [ResponseType(typeof(ACCESSORY_STORED))]
        public async Task<IHttpActionResult> PostACCESSORY_STORED(ACCESSORY_STORED aCCESSORY_STORED)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ACCESSORY_STORED.Add(aCCESSORY_STORED);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ACCESSORY_STOREDExists(aCCESSORY_STORED.ACCESSORY_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aCCESSORY_STORED.ACCESSORY_ID }, aCCESSORY_STORED);
        }

        // DELETE: api/ACCESSORY_STORED/5
        [ResponseType(typeof(ACCESSORY_STORED))]
        public async Task<IHttpActionResult> DeleteACCESSORY_STORED(string id)
        {
            ACCESSORY_STORED aCCESSORY_STORED = await db.ACCESSORY_STORED.FindAsync(id);
            if (aCCESSORY_STORED == null)
            {
                return NotFound();
            }

            db.ACCESSORY_STORED.Remove(aCCESSORY_STORED);
            await db.SaveChangesAsync();

            return Ok(aCCESSORY_STORED);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ACCESSORY_STOREDExists(string id)
        {
            return db.ACCESSORY_STORED.Count(e => e.ACCESSORY_ID == id) > 0;
        }
    }
}