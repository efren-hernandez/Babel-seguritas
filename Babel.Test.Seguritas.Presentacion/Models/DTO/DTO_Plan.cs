using System;

namespace Babel.Test.Seguritas.Presentacion.Models.DTO
{
    public class DTO_Plan
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
    }
}