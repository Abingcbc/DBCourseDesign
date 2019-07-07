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
    public class EQ_TYPEController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/EQ_TYPE
        public IQueryable<EQ_TYPE> GetEQ_TYPE()
        {
            return db.EQ_TYPE;
        }

        // GET: api/EQ_TYPE/5
        [ResponseType(typeof(EQ_TYPE))]
        public async Task<IHttpActionResult> GetEQ_TYPE(string id)
        {
            EQ_TYPE eQ_TYPE = await db.EQ_TYPE.FindAsync(id);
            if (eQ_TYPE == null)
            {
                return NotFound();
            }

            return Ok(eQ_TYPE);
        }

        // PUT: api/EQ_TYPE/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEQ_TYPE(string id, EQ_TYPE eQ_TYPE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eQ_TYPE.ID)
            {
                return BadRequest();
            }

            db.Entry(eQ_TYPE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQ_TYPEExists(id))
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

        // POST: api/EQ_TYPE
        [ResponseType(typeof(EQ_TYPE))]
        public async Task<IHttpActionResult> PostEQ_TYPE(EQ_TYPE eQ_TYPE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQ_TYPE.Add(eQ_TYPE);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EQ_TYPEExists(eQ_TYPE.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eQ_TYPE.ID }, eQ_TYPE);
        }

        // DELETE: api/EQ_TYPE/5
        [ResponseType(typeof(EQ_TYPE))]
        public async Task<IHttpActionResult> DeleteEQ_TYPE(string id)
        {
            EQ_TYPE eQ_TYPE = await db.EQ_TYPE.FindAsync(id);
            if (eQ_TYPE == null)
            {
                return NotFound();
            }

            db.EQ_TYPE.Remove(eQ_TYPE);
            await db.SaveChangesAsync();

            return Ok(eQ_TYPE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQ_TYPEExists(string id)
        {
            return db.EQ_TYPE.Count(e => e.ID == id) > 0;
        }
    }
}