using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Babel.Test.Seguritas.Presentacion
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/cliente").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app/cliente.js"));

            bundles.Add(new ScriptBundle("~/bundles/plan").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app/plan.js"));

            bundles.Add(new ScriptBundle("~/bundles/cobertura").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app/cobertura.js"));

            bundles.Add(new ScriptBundle("~/bundles/planCobertura").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app/planCobertura.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app/app.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}