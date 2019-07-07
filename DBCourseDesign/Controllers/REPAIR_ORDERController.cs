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
    public class REPAIR_ORDERController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/REPAIR_ORDER
        public IQueryable<REPAIR_ORDER> GetREPAIR_ORDER()
        {
            return db.REPAIR_ORDER;
        }

        // GET: api/REPAIR_ORDER/5
        [ResponseType(typeof(REPAIR_ORDER))]
        public async Task<IHttpActionResult> GetREPAIR_ORDER(string id)
        {
            REPAIR_ORDER rEPAIR_ORDER = await db.REPAIR_ORDER.FindAsync(id);
            if (rEPAIR_ORDER == null)
            {
                return NotFound();
            }

            return Ok(rEPAIR_ORDER);
        }

        // PUT: api/REPAIR_ORDER/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutREPAIR_ORDER(string id, REPAIR_ORDER rEPAIR_ORDER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rEPAIR_ORDER.ID)
            {
                return BadRequest();
            }

            db.Entry(rEPAIR_ORDER).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!REPAIR_ORDERExists(id))
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

        // POST: api/REPAIR_ORDER
        [ResponseType(typeof(REPAIR_ORDER))]
        public async Task<IHttpActionResult> PostREPAIR_ORDER(REPAIR_ORDER rEPAIR_ORDER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.REPAIR_ORDER.Add(rEPAIR_ORDER);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (REPAIR_ORDERExists(rEPAIR_ORDER.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rEPAIR_ORDER.ID }, rEPAIR_ORDER);
        }

        // DELETE: api/REPAIR_ORDER/5
        [ResponseType(typeof(REPAIR_ORDER))]
        public async Task<IHttpActionResult> DeleteREPAIR_ORDER(string id)
        {
            REPAIR_ORDER rEPAIR_ORDER = await db.REPAIR_ORDER.FindAsync(id);
            if (rEPAIR_ORDER == null)
            {
                return NotFound();
            }

            db.REPAIR_ORDER.Remove(rEPAIR_ORDER);
            await db.SaveChangesAsync();

            return Ok(rEPAIR_ORDER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool REPAIR_ORDERExists(string id)
        {
            return db.REPAIR_ORDER.Count(e => e.ID == id) > 0;
        }
    }
}