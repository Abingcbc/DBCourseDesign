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
    public class ACCESSORiesController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/ACCESSORies
        public IQueryable<ACCESSORY> GetACCESSORY()
        {
            return db.ACCESSORY;
        }

        // GET: api/ACCESSORies/5
        [ResponseType(typeof(ACCESSORY))]
        public async Task<IHttpActionResult> GetACCESSORY(string id)
        {
            ACCESSORY aCCESSORY = db.ACCESSORY.Find(id);
            if (aCCESSORY == null)
            {
                return NotFound();
            }

            return Ok(aCCESSORY);
        }

        // PUT: api/ACCESSORies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutACCESSORY(string id, ACCESSORY aCCESSORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aCCESSORY.ID)
            {
                return BadRequest();
            }

            db.Entry(aCCESSORY).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ACCESSORYExists(id))
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

        // POST: api/ACCESSORies
        [ResponseType(typeof(ACCESSORY))]
        public async Task<IHttpActionResult> PostACCESSORY(ACCESSORY aCCESSORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ACCESSORY.Add(aCCESSORY);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ACCESSORYExists(aCCESSORY.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aCCESSORY.ID }, aCCESSORY);
        }

        // DELETE: api/ACCESSORies/5
        [ResponseType(typeof(ACCESSORY))]
        public async Task<IHttpActionResult> DeleteACCESSORY(string id)
        {
            ACCESSORY aCCESSORY = await db.ACCESSORY.FindAsync(id);
            if (aCCESSORY == null)
            {
                return NotFound();
            }

            db.ACCESSORY.Remove(aCCESSORY);
            await db.SaveChangesAsync();

            return Ok(aCCESSORY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ACCESSORYExists(string id)
        {
            return db.ACCESSORY.Count(e => e.ID == id) > 0;
        }
    }
}