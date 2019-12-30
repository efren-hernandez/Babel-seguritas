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
using Babel.Test.Seguritas.Presentacion.DAO;
using Babel.Test.Seguritas.Presentacion.Models;
using Babel.Test.Seguritas.Presentacion.Models.DTO;

namespace Babel.Test.Seguritas.Presentacion.Controllers
{
    public class PlanController : ApiController
    {
        private readonly SeguritasContext db = new SeguritasContext();

        // GET: api/Plan
        public IQueryable<DTO_Plan> GetPlanes()
        {
            var planes = from p in db.Planes
                         select new DTO_Plan()
                         {
                             Id = p.Id,
                             Descripcion = p.Descripcion,
                             FechaCreacion = p.FechaCreacion,
                             ClienteId = p.ClienteId,
                             Cliente = p.Cliente.Nombre
                         };

            return planes;
        }

        // GET: api/Plan/5
        [ResponseType(typeof(DTO_Plan))]
        public async Task<IHttpActionResult> GetPlan(int id)
        {
            Plan p = await db.Planes.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }

            DTO_Plan plan = new DTO_Plan()
            {
                Id = p.Id,
                Descripcion = p.Descripcion,
                FechaCreacion = p.FechaCreacion,
                ClienteId = p.ClienteId,
                Cliente = p.Cliente.Nombre
            };

            return Ok(plan);
        }

        // PUT: api/Plan/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlan(int id, DTO_Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plan.Id)
            {
                return BadRequest();
            }

            Plan actualizaPlan = await db.Planes.FindAsync(id);
            actualizaPlan.Descripcion = plan.Descripcion;
            actualizaPlan.ClienteId = plan.ClienteId;

            db.Entry(actualizaPlan).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
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

        // POST: api/Plan
        [ResponseType(typeof(DTO_Plan))]
        public async Task<IHttpActionResult> PostPlan(DTO_Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Plan nuevoPlan = new Plan()
            {
                Descripcion = plan.Descripcion,
                FechaCreacion = DateTime.Now,
                ClienteId = plan.ClienteId
            };

            db.Planes.Add(nuevoPlan);
            await db.SaveChangesAsync();

            plan.Id = nuevoPlan.Id;

            return CreatedAtRoute("DefaultApi", new { id = plan.Id }, plan);
        }

        // DELETE: api/Plan/5
        [ResponseType(typeof(DTO_Plan))]
        public async Task<IHttpActionResult> DeletePlan(int id)
        {
            Plan plan = await db.Planes.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            DTO_Plan eliminaPlan = new DTO_Plan()
            {
                Id = plan.Id,
                Descripcion = plan.Descripcion,
                FechaCreacion = plan.FechaCreacion,
                ClienteId = plan.ClienteId
            };

            db.Planes.Remove(plan);
            await db.SaveChangesAsync();

            return Ok(eliminaPlan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlanExists(int id)
        {
            return db.Planes.Any(e => e.Id == id);
        }
    }
}