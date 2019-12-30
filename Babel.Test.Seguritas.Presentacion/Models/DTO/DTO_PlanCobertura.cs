namespace Babel.Test.Seguritas.Presentacion.Models.DTO
{
    public class DTO_PlanCobertura
    {
        public int ClienteId { get; set; }
        public int PlanId { get; set; }
        public int CoberturaId { get; set; }

        public string Cliente { get; set; }
        public string Plan { get; set; }
        public string Cobertura { get; set; }
    }
}