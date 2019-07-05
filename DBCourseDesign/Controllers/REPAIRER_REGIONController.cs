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
    public class REPAIRER_REGIONController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/REPAIRER_REGION
        public IQueryable<REPAIRER_REGION> GetREPAIRER_REGION()
        {
            return db.REPAIRER_REGION;
        }

        // GET: api/REPAIRER_REGION/5
        [ResponseType(typeof(REPAIRER_REGION))]
        public async Task<IHttpActionResult> GetREPAIRER_REGION(string id)
        {
            REPAIRER_REGION rEPAIRER_REGION = await db.REPAIRER_REGION.FindAsync(id);
            if (rEPAIRER_REGION == null)
            {
                return NotFound();
            }

            return Ok(rEPAIRER_REGION);
        }

        // PUT: api/REPAIRER_REGION/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutREPAIRER_REGION(string id, REPAIRER_REGION rEPAIRER_REGION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rEPAIRER_REGION.REPAIRER_ID)
            {
                return BadRequest();
            }

            db.Entry(rEPAIRER_REGION).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!REPAIRER_REGIONExists(id))
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

        // POST: api/REPAIRER_REGION
        [ResponseType(typeof(REPAIRER_REGION))]
        public async Task<IHttpActionResult> PostREPAIRER_REGION(REPAIRER_REGION rEPAIRER_REGION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.REPAIRER_REGION.Add(rEPAIRER_REGION);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (REPAIRER_REGIONExists(rEPAIRER_REGION.REPAIRER_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rEPAIRER_REGION.REPAIRER_ID }, rEPAIRER_REGION);
        }

        // DELETE: api/REPAIRER_REGION/5
        [ResponseType(typeof(REPAIRER_REGION))]
        public async Task<IHttpActionResult> DeleteREPAIRER_REGION(string id)
        {
            REPAIRER_REGION rEPAIRER_REGION = await db.REPAIRER_REGION.FindAsync(id);
            if (rEPAIRER_REGION == null)
            {
                return NotFound();
            }

            db.REPAIRER_REGION.Remove(rEPAIRER_REGION);
            await db.SaveChangesAsync();

            return Ok(rEPAIRER_REGION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool REPAIRER_REGIONExists(string id)
        {
            return db.REPAIRER_REGION.Count(e => e.REPAIRER_ID == id) > 0;
        }
    }
}