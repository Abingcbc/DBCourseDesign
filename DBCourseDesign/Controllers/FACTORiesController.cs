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
    public class FACTORiesController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        // GET: api/FACTORies
        public IQueryable<FACTORY> GetFACTORY()
        {
            return db.FACTORY;
        }

        // GET: api/FACTORies/5
        [ResponseType(typeof(FACTORY))]
        public async Task<IHttpActionResult> GetFACTORY(string id)
        {
            FACTORY fACTORY = await db.FACTORY.FindAsync(id);
            if (fACTORY == null)
            {
                return NotFound();
            }

            return Ok(fACTORY);
        }

        // PUT: api/FACTORies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFACTORY(string id, FACTORY fACTORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fACTORY.ID)
            {
                return BadRequest();
            }

            db.Entry(fACTORY).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FACTORYExists(id))
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

        // POST: api/FACTORies
        [ResponseType(typeof(FACTORY))]
        public async Task<IHttpActionResult> PostFACTORY(FACTORY fACTORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FACTORY.Add(fACTORY);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FACTORYExists(fACTORY.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = fACTORY.ID }, fACTORY);
        }

        // DELETE: api/FACTORies/5
        [ResponseType(typeof(FACTORY))]
        public async Task<IHttpActionResult> DeleteFACTORY(string id)
        {
            FACTORY fACTORY = await db.FACTORY.FindAsync(id);
            if (fACTORY == null)
            {
                return NotFound();
            }

            db.FACTORY.Remove(fACTORY);
            await db.SaveChangesAsync();

            return Ok(fACTORY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FACTORYExists(string id)
        {
            return db.FACTORY.Count(e => e.ID == id) > 0;
        }
    }
}