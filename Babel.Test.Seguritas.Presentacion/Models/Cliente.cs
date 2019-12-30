using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babel.Test.Seguritas.Presentacion.Models
{
    public class Cliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [MinLength(1, ErrorMessage = "El nombre es requerido y no puede estar vacío"), MaxLength(250, ErrorMessage = "El nombre debe tener máximo 250 caracteres")]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Plan> Planes { get; set; }
    }
}