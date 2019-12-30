using System;
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
    public class ClienteController : ApiController
    {
        private readonly SeguritasContext db = new SeguritasContext();

        // GET: api/Cliente
        public IQueryable<DTO_Cliente> GetClientes()
        {
            var clientes = from c in db.Clientes
                           select new DTO_Cliente()
                           {
                               Id = c.Id,
                               Nombre = c.Nombre,
                               FechaCreacion = c.FechaCreacion
                           };

            return clientes;
        }

        // GET: api/Cliente/5
        [ResponseType(typeof(DTO_Cliente))]
        public async Task<IHttpActionResult> GetCliente(int id)
        {
            Cliente c = await db.Clientes.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }

            DTO_Cliente cliente = new DTO_Cliente()
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaCreacion = c.FechaCreacion
            };

            return Ok(cliente);
        }

        // PUT: api/Cliente/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCliente(int id, DTO_Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.Id)
            {
                return BadRequest();
            }

            if (db.Clientes.Any(x => x.Nombre == cliente.Nombre))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El cliente ya está registrado"));
            }

            Cliente actualizaCliente = await db.Clientes.FindAsync(id);
            actualizaCliente.Nombre = cliente.Nombre;

            db.Entry(actualizaCliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Cliente
        [ResponseType(typeof(DTO_Cliente))]
        public async Task<IHttpActionResult> PostCliente(DTO_Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Clientes.Any(x => x.Nombre == cliente.Nombre))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El cliente ya está registrado"));
            }

            Cliente nuevoCliente = new Cliente()
            {
                Nombre = cliente.Nombre,
                FechaCreacion = DateTime.Now
            };

            db.Clientes.Add(nuevoCliente);
            await db.SaveChangesAsync();

            cliente.Id = nuevoCliente.Id;

            return CreatedAtRoute("DefaultApi", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Cliente/5
        [ResponseType(typeof(DTO_Cliente))]
        public async Task<IHttpActionResult> DeleteCliente(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            DTO_Cliente eliminaCliente = new DTO_Cliente()
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                FechaCreacion = cliente.FechaCreacion
            };

            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();

            return Ok(eliminaCliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Clientes.Any(e => e.Id == id);
        }
    }
}