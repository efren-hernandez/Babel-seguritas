using Babel.Test.Seguritas.Presentacion.DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Babel.Test.Seguritas.Presentacion.Controllers
{
    public class AppController : ApiController
    {
        private readonly SeguritasContext db = new SeguritasContext();

        // GET: api/Cliente/Count
        [ResponseType(typeof(AppTotales))]
        public async Task<IHttpActionResult> GetApp()
        {
            int clientes = await db.Clientes.CountAsync();
            int planes = await db.Planes.CountAsync();
            int coberturas = await db.Coberturas.CountAsync();

            AppTotales appTotales = new AppTotales()
            {
                TotalClientes = clientes,
                TotalPlanes = planes,
                TotalCoberturas = coberturas
            };

            return Ok(appTotales);
        }
    }

    public class AppTotales
    {
        public int TotalClientes { get; set; }
        public int TotalPlanes { get; set; }
        public int TotalCoberturas { get; set; }
    }
}
