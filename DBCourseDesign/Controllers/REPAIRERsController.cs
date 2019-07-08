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
    public class REPAIRERsController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/REPAIRERs
        public IQueryable<REPAIRER> GetREPAIRER()
        {
            return db.REPAIRER;
        }

        // GET: api/REPAIRERs/5
        [ResponseType(typeof(REPAIRER))]
        public async Task<IHttpActionResult> GetREPAIRER(string id)
        {
            REPAIRER rEPAIRER = await db.REPAIRER.FindAsync(id);
            if (rEPAIRER == null)
            {
                return NotFound();
            }

            return Ok(rEPAIRER);
        }

        // PUT: api/REPAIRERs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutREPAIRER(string id, REPAIRER rEPAIRER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rEPAIRER.ID)
            {
                return BadRequest();
            }

            db.Entry(rEPAIRER).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!REPAIRERExists(id))
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

        // POST: api/REPAIRERs
        [ResponseType(typeof(REPAIRER))]
        public async Task<IHttpActionResult> PostREPAIRER(REPAIRER rEPAIRER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.REPAIRER.Add(rEPAIRER);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (REPAIRERExists(rEPAIRER.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rEPAIRER.ID }, rEPAIRER);
        }

        // DELETE: api/REPAIRERs/5
        [ResponseType(typeof(REPAIRER))]
        public async Task<IHttpActionResult> DeleteREPAIRER(string id)
        {
            REPAIRER rEPAIRER = await db.REPAIRER.FindAsync(id);
            if (rEPAIRER == null)
            {
                return NotFound();
            }

            db.REPAIRER.Remove(rEPAIRER);
            await db.SaveChangesAsync();

            return Ok(rEPAIRER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool REPAIRERExists(string id)
        {
            return db.REPAIRER.Count(e => e.ID == id) > 0;
        }
    }
}