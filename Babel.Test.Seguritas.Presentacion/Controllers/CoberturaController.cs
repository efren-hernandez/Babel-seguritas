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
    public class CoberturaController : ApiController
    {
        private readonly SeguritasContext db = new SeguritasContext();

        // GET: api/Cobertura
        public IQueryable<DTO_Cobertura> GetCoberturas()
        {
            var coberturas = from c in db.Coberturas
                             select new DTO_Cobertura()
                             {
                                 Id = c.Id,
                                 Descripcion = c.Descripcion,
                                 FechaCreacion = c.FechaCreacion
                             };

            return coberturas;
        }

        // GET: api/Cobertura/5
        [ResponseType(typeof(DTO_Cobertura))]
        public async Task<IHttpActionResult> GetCobertura(int id)
        {
            Cobertura c = await db.Coberturas.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }

            DTO_Cobertura cobertura = new DTO_Cobertura()
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
                FechaCreacion = c.FechaCreacion
            };

            return Ok(cobertura);
        }

        // PUT: api/Cobertura/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCobertura(int id, DTO_Cobertura cobertura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cobertura.Id)
            {
                return BadRequest();
            }

            Cobertura actualizaCobertura = await db.Coberturas.FindAsync(id);
            actualizaCobertura.Descripcion = cobertura.Descripcion;

            db.Entry(actualizaCobertura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoberturaExists(id))
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

        // POST: api/Cobertura
        [ResponseType(typeof(DTO_Cobertura))]
        public async Task<IHttpActionResult> PostCobertura(DTO_Cobertura cobertura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cobertura nuevaCobertura = new Cobertura()
            {
                Descripcion = cobertura.Descripcion,
                FechaCreacion = DateTime.Now
            };

            db.Coberturas.Add(nuevaCobertura);
            await db.SaveChangesAsync();

            cobertura.Id = nuevaCobertura.Id;

            return CreatedAtRoute("DefaultApi", new { id = cobertura.Id }, cobertura);
        }

        // DELETE: api/Cobertura/5
        [ResponseType(typeof(DTO_Cobertura))]
        public async Task<IHttpActionResult> DeleteCobertura(int id)
        {
            Cobertura cobertura = await db.Coberturas.FindAsync(id);
            if (cobertura == null)
            {
                return NotFound();
            }

            DTO_Cobertura eliminaCobertura = new DTO_Cobertura()
            {
                Id = cobertura.Id,
                Descripcion = cobertura.Descripcion,
                FechaCreacion = cobertura.FechaCreacion
            };

            db.Coberturas.Remove(cobertura);
            await db.SaveChangesAsync();

            return Ok(eliminaCobertura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoberturaExists(int id)
        {
            return db.Coberturas.Count(e => e.Id == id) > 0;
        }
    }
}