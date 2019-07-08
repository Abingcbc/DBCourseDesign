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
    public class EQ_IN_USEController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/EQ_IN_USE
        public IQueryable<EQ_IN_USE> GetEQ_IN_USE()
        {
            return db.EQ_IN_USE;
        }

        // GET: api/EQ_IN_USE/5
        [ResponseType(typeof(EQ_IN_USE))]
        public async Task<IHttpActionResult> GetEQ_IN_USE(string id)
        {
            EQ_IN_USE eQ_IN_USE = await db.EQ_IN_USE.FindAsync(id);
            if (eQ_IN_USE == null)
            {
                return NotFound();
            }

            return Ok(eQ_IN_USE);
        }

        // PUT: api/EQ_IN_USE/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEQ_IN_USE(string id, EQ_IN_USE eQ_IN_USE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eQ_IN_USE.ID)
            {
                return BadRequest();
            }

            db.Entry(eQ_IN_USE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQ_IN_USEExists(id))
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

        // POST: api/EQ_IN_USE
        [ResponseType(typeof(EQ_IN_USE))]
        public async Task<IHttpActionResult> PostEQ_IN_USE(EQ_IN_USE eQ_IN_USE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQ_IN_USE.Add(eQ_IN_USE);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EQ_IN_USEExists(eQ_IN_USE.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eQ_IN_USE.ID }, eQ_IN_USE);
        }

        // DELETE: api/EQ_IN_USE/5
        [ResponseType(typeof(EQ_IN_USE))]
        public async Task<IHttpActionResult> DeleteEQ_IN_USE(string id)
        {
            EQ_IN_USE eQ_IN_USE = await db.EQ_IN_USE.FindAsync(id);
            if (eQ_IN_USE == null)
            {
                return NotFound();
            }

            db.EQ_IN_USE.Remove(eQ_IN_USE);
            await db.SaveChangesAsync();

            return Ok(eQ_IN_USE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQ_IN_USEExists(string id)
        {
            return db.EQ_IN_USE.Count(e => e.ID == id) > 0;
        }
    }
}