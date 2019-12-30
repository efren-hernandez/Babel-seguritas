using Babel.Test.Seguritas.Presentacion.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Babel.Test.Seguritas.Presentacion.DAO
{
    public class SeguritasContext : DbContext
    {
        public SeguritasContext() : base("SeguritasContext")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<PlanCobertura> PlanCoberturas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}