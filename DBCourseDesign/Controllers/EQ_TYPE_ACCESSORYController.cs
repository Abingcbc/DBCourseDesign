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
    public class EQ_TYPE_ACCESSORYController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/EQ_TYPE_ACCESSORY
        public IQueryable<EQ_TYPE_ACCESSORY> GetEQ_TYPE_ACCESSORY()
        {
            return db.EQ_TYPE_ACCESSORY;
        }

        // GET: api/EQ_TYPE_ACCESSORY/5
        [ResponseType(typeof(EQ_TYPE_ACCESSORY))]
        public async Task<IHttpActionResult> GetEQ_TYPE_ACCESSORY(string id)
        {
            EQ_TYPE_ACCESSORY eQ_TYPE_ACCESSORY = await db.EQ_TYPE_ACCESSORY.FindAsync(id);
            if (eQ_TYPE_ACCESSORY == null)
            {
                return NotFound();
            }

            return Ok(eQ_TYPE_ACCESSORY);
        }

        // PUT: api/EQ_TYPE_ACCESSORY/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEQ_TYPE_ACCESSORY(string id, EQ_TYPE_ACCESSORY eQ_TYPE_ACCESSORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eQ_TYPE_ACCESSORY.EQ_TYPE_ID)
            {
                return BadRequest();
            }

            db.Entry(eQ_TYPE_ACCESSORY).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQ_TYPE_ACCESSORYExists(id))
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

        // POST: api/EQ_TYPE_ACCESSORY
        [ResponseType(typeof(EQ_TYPE_ACCESSORY))]
        public async Task<IHttpActionResult> PostEQ_TYPE_ACCESSORY(EQ_TYPE_ACCESSORY eQ_TYPE_ACCESSORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQ_TYPE_ACCESSORY.Add(eQ_TYPE_ACCESSORY);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EQ_TYPE_ACCESSORYExists(eQ_TYPE_ACCESSORY.EQ_TYPE_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eQ_TYPE_ACCESSORY.EQ_TYPE_ID }, eQ_TYPE_ACCESSORY);
        }

        // DELETE: api/EQ_TYPE_ACCESSORY/5
        [ResponseType(typeof(EQ_TYPE_ACCESSORY))]
        public async Task<IHttpActionResult> DeleteEQ_TYPE_ACCESSORY(string id)
        {
            EQ_TYPE_ACCESSORY eQ_TYPE_ACCESSORY = await db.EQ_TYPE_ACCESSORY.FindAsync(id);
            if (eQ_TYPE_ACCESSORY == null)
            {
                return NotFound();
            }

            db.EQ_TYPE_ACCESSORY.Remove(eQ_TYPE_ACCESSORY);
            await db.SaveChangesAsync();

            return Ok(eQ_TYPE_ACCESSORY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQ_TYPE_ACCESSORYExists(string id)
        {
            return db.EQ_TYPE_ACCESSORY.Count(e => e.EQ_TYPE_ID == id) > 0;
        }
    }
}