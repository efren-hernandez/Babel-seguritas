using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Babel.Test.Seguritas.Presentacion.Models
{
    public class PlanCobertura
    {
        [Key, Column(Order = 1)]
        public int PlanId { get; set; }
        [Key, Column(Order = 2)]
        public int CoberturaId { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual Cobertura Cobertura { get; set; }
    }
}