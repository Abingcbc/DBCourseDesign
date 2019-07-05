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
    public class DISPATCHERsController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/DISPATCHERs
        public IQueryable<DISPATCHER> GetDISPATCHER()
        {
            return db.DISPATCHER;
        }

        // GET: api/DISPATCHERs/5
        [ResponseType(typeof(DISPATCHER))]
        public async Task<IHttpActionResult> GetDISPATCHER(string id)
        {
            DISPATCHER dISPATCHER = await db.DISPATCHER.FindAsync(id);
            if (dISPATCHER == null)
            {
                return NotFound();
            }

            return Ok(dISPATCHER);
        }

        // PUT: api/DISPATCHERs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDISPATCHER(string id, DISPATCHER dISPATCHER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dISPATCHER.ID)
            {
                return BadRequest();
            }

            db.Entry(dISPATCHER).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DISPATCHERExists(id))
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

        // POST: api/DISPATCHERs
        [ResponseType(typeof(DISPATCHER))]
        public async Task<IHttpActionResult> PostDISPATCHER(DISPATCHER dISPATCHER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DISPATCHER.Add(dISPATCHER);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DISPATCHERExists(dISPATCHER.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dISPATCHER.ID }, dISPATCHER);
        }

        // DELETE: api/DISPATCHERs/5
        [ResponseType(typeof(DISPATCHER))]
        public async Task<IHttpActionResult> DeleteDISPATCHER(string id)
        {
            DISPATCHER dISPATCHER = await db.DISPATCHER.FindAsync(id);
            if (dISPATCHER == null)
            {
                return NotFound();
            }

            db.DISPATCHER.Remove(dISPATCHER);
            await db.SaveChangesAsync();

            return Ok(dISPATCHER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DISPATCHERExists(string id)
        {
            return db.DISPATCHER.Count(e => e.ID == id) > 0;
        }
    }
}