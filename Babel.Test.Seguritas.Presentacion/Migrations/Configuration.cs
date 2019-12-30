namespace Babel.Test.Seguritas.Presentacion.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Babel.Test.Seguritas.Presentacion.DAO.SeguritasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Babel.Test.Seguritas.Presentacion.DAO.SeguritasContext";
        }

        protected override void Seed(Babel.Test.Seguritas.Presentacion.DAO.SeguritasContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Clientes.AddOrUpdate(x => x.Id,
                new Models.Cliente() { Id = 1, Nombre = "Efrén Hernández", FechaCreacion = new DateTime(2019, 12, 28) },
                new Models.Cliente() { Id = 2, Nombre = "Juan Pérez", FechaCreacion = new DateTime(2019, 12, 28) },
                new Models.Cliente() { Id = 3, Nombre = "Iván Martínez", FechaCreacion = new DateTime(2019, 12, 28) }
            );

            context.Planes.AddOrUpdate(x => x.Id,
                new Models.Plan() { Id = 1, Descripcion = "Plan 110101", FechaCreacion = new DateTime(2019, 12, 28), ClienteId = 1 },
                new Models.Plan() { Id = 2, Descripcion = "Plan 110102", FechaCreacion = new DateTime(2019, 12, 28), ClienteId = 1 },
                new Models.Plan() { Id = 3, Descripcion = "Plan 110201", FechaCreacion = new DateTime(2019, 12, 28), ClienteId = 2 }
            );

            context.Coberturas.AddOrUpdate(x => x.Id,
                new Models.Cobertura() { Id = 1, Descripcion = "Seguro de Vida", FechaCreacion = new DateTime(2019, 12, 28) },
                new Models.Cobertura() { Id = 2, Descripcion = "Seguro de Salud", FechaCreacion = new DateTime(2019, 12, 28) },
                new Models.Cobertura() { Id = 3, Descripcion = "Seguro de Automóvil", FechaCreacion = new DateTime(2019, 12, 28) }
            );

            context.PlanCoberturas.AddOrUpdate(
                new Models.PlanCobertura() { PlanId = 1, CoberturaId = 1 },
                new Models.PlanCobertura() { PlanId = 1, CoberturaId = 2 },
                new Models.PlanCobertura() { PlanId = 1, CoberturaId = 3 },
                new Models.PlanCobertura() { PlanId = 2, CoberturaId = 1 },
                new Models.PlanCobertura() { PlanId = 2, CoberturaId = 2 }
            );
        }
    }
}
