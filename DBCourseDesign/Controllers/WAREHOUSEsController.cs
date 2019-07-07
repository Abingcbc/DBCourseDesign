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
    public class WAREHOUSEsController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/WAREHOUSEs
        public IQueryable<WAREHOUSE> GetWAREHOUSE()
        {
            return db.WAREHOUSE;
        }

        // GET: api/WAREHOUSEs/5
        [ResponseType(typeof(WAREHOUSE))]
        public async Task<IHttpActionResult> GetWAREHOUSE(string id)
        {
            WAREHOUSE wAREHOUSE = await db.WAREHOUSE.FindAsync(id);
            if (wAREHOUSE == null)
            {
                return NotFound();
            }

            return Ok(wAREHOUSE);
        }

        // PUT: api/WAREHOUSEs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWAREHOUSE(string id, WAREHOUSE wAREHOUSE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wAREHOUSE.ID)
            {
                return BadRequest();
            }

            db.Entry(wAREHOUSE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WAREHOUSEExists(id))
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

        // POST: api/WAREHOUSEs
        [ResponseType(typeof(WAREHOUSE))]
        public async Task<IHttpActionResult> PostWAREHOUSE(WAREHOUSE wAREHOUSE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WAREHOUSE.Add(wAREHOUSE);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WAREHOUSEExists(wAREHOUSE.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = wAREHOUSE.ID }, wAREHOUSE);
        }

        // DELETE: api/WAREHOUSEs/5
        [ResponseType(typeof(WAREHOUSE))]
        public async Task<IHttpActionResult> DeleteWAREHOUSE(string id)
        {
            WAREHOUSE wAREHOUSE = await db.WAREHOUSE.FindAsync(id);
            if (wAREHOUSE == null)
            {
                return NotFound();
            }

            db.WAREHOUSE.Remove(wAREHOUSE);
            await db.SaveChangesAsync();

            return Ok(wAREHOUSE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WAREHOUSEExists(string id)
        {
            return db.WAREHOUSE.Count(e => e.ID == id) > 0;
        }
    }
}