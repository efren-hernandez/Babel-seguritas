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
    public class PlanCoberturaController : ApiController
    {
        private readonly SeguritasContext db = new SeguritasContext();

        // GET: api/PlanCobertura
        public IQueryable<DTO_PlanCobertura> GetPlanCoberturas()
        {
            var lista = from pc in db.PlanCoberturas
                        select new DTO_PlanCobertura()
                        {
                            ClienteId = pc.Plan.ClienteId,
                            Cliente = pc.Plan.Cliente.Nombre,
                            PlanId = pc.PlanId,
                            Plan = pc.Plan.Descripcion,
                            CoberturaId = pc.CoberturaId,
                            Cobertura = pc.Cobertura.Descripcion
                        };

            return lista;
        }

        // GET: api/PlanCobertura/5
        [ResponseType(typeof(DTO_PlanCobertura))]
        public async Task<IHttpActionResult> GetPlanCobertura(int id)
        {
            PlanCobertura pc = await db.PlanCoberturas.FindAsync(id);
            if (pc == null)
            {
                return NotFound();
            }

            DTO_PlanCobertura planCobertura = new DTO_PlanCobertura()
            {
                ClienteId = pc.Plan.ClienteId,
                Cliente = pc.Plan.Cliente.Nombre,
                PlanId = pc.PlanId,
                Plan = pc.Plan.Descripcion,
                CoberturaId = pc.CoberturaId,
                Cobertura = pc.Cobertura.Descripcion
            };

            return Ok(planCobertura);
        }

        // PUT: api/PlanCobertura/5
        //Not Implemented

        // POST: api/PlanCobertura
        [ResponseType(typeof(DTO_PlanCobertura))]
        public async Task<IHttpActionResult> PostPlanCobertura(DTO_PlanCobertura planCobertura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PlanCobertura nuevoPC = new PlanCobertura()
            {
                PlanId = planCobertura.PlanId,
                CoberturaId = planCobertura.CoberturaId,
            };

            db.PlanCoberturas.Add(nuevoPC);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = planCobertura.PlanId }, planCobertura);
        }

        // DELETE: api/PlanCobertura/5
        [ResponseType(typeof(DTO_PlanCobertura))]
        [Route("api/PlanCobertura/DEL")]
        [HttpPost]
        public async Task<IHttpActionResult> DeletePlanCobertura(DTO_PlanCobertura pc)
        {
            PlanCobertura planCobertura = await db.PlanCoberturas.FirstOrDefaultAsync(x => x.PlanId == pc.PlanId && x.CoberturaId == pc.CoberturaId);
            if (planCobertura == null)
            {
                return NotFound();
            }

            DTO_PlanCobertura nuevoPC = new DTO_PlanCobertura()
            {
                PlanId = pc.PlanId,
                CoberturaId = pc.CoberturaId,
            };

            db.PlanCoberturas.Remove(planCobertura);
            await db.SaveChangesAsync();

            return Ok(nuevoPC);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}