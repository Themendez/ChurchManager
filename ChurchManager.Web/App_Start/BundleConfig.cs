using System.Web;
using System.Web.Optimization;

namespace ChurchManager.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/base")
                            .Include("~/Scripts/lib/jquery/jquery-2.0.3.js")
                            .Include("~/Scripts/lib/angular/angular.js")
                            .Include("~/Scripts/lib/angular/angular-animate.js")
                            .Include("~/Scripts/lib/bootstrap/bootstrap.js")
                            .IncludeDirectory("~/Scripts/lib/plugin", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/churchmanager")
                            .Include("~/Scripts/app/module.js")
                            .IncludeDirectory("~/Scripts/app/services", "*.js", true)
                            .IncludeDirectory("~/Scripts/app/filters", "*.js", true)
                            .IncludeDirectory("~/Scripts/app/directives", "*.js", true)
                            .IncludeDirectory("~/Scripts/app/controllers", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/styles")
                .Include("~/Content/css/bootstrap.css")
                .Include("~/Content/css/font-awesome.css")
                .Include("~/Content/css/site.css"));
        }
    }
}