using System.Web;
using System.Web.Optimization;

namespace BikesShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/css/Bikecss").Include(
                      "~/css/bootstrap.css",
                      "~/css/style.css", "~/css/table-style.css", "~/css/basictable.css", "~/css/simplelightbox.min.css", "~/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/BikesJQuery").Include(
                    "~/js/bootstrap.js" ,
                    "~/js/SmoothScroll.min.js",
                    "~/js/jarallax.js",
                    "~/js/responsiveslides.min.js",
                    "~/js/simple-lightbox.js",
                    "~/js/jquery-1.11.1.min.js"));
        }
    }

}
