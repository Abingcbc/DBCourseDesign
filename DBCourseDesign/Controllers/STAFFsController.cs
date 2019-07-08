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
    public class STAFFsController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/STAFFs
        public IQueryable<STAFF> GetSTAFF()
        {
            return db.STAFF;
        }

        // GET: api/STAFFs/5
        [ResponseType(typeof(STAFF))]
        public async Task<IHttpActionResult> GetSTAFF(string id)
        {
            STAFF sTAFF = await db.STAFF.FindAsync(id);
            if (sTAFF == null)
            {
                return NotFound();
            }

            return Ok(sTAFF);
        }

        // PUT: api/STAFFs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSTAFF(string id, STAFF sTAFF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sTAFF.ID)
            {
                return BadRequest();
            }

            db.Entry(sTAFF).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!STAFFExists(id))
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

        // POST: api/STAFFs
        [ResponseType(typeof(STAFF))]
        public async Task<IHttpActionResult> PostSTAFF(STAFF sTAFF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.STAFF.Add(sTAFF);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (STAFFExists(sTAFF.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sTAFF.ID }, sTAFF);
        }

        // DELETE: api/STAFFs/5
        [ResponseType(typeof(STAFF))]
        public async Task<IHttpActionResult> DeleteSTAFF(string id)
        {
            STAFF sTAFF = await db.STAFF.FindAsync(id);
            if (sTAFF == null)
            {
                return NotFound();
            }

            db.STAFF.Remove(sTAFF);
            await db.SaveChangesAsync();

            return Ok(sTAFF);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool STAFFExists(string id)
        {
            return db.STAFF.Count(e => e.ID == id) > 0;
        }
    }
}