using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babel.Test.Seguritas.Presentacion.Models
{
    public class Cobertura
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MinLength(1, ErrorMessage = "La descripción es requerida y no puede estar vacía"), MaxLength(250, ErrorMessage = "La descripción debe tener máximo 250 caracteres")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<PlanCobertura> PlanCobertura { get; set; }
    }
}