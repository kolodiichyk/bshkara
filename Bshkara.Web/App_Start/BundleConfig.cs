using System.Web.Optimization;

namespace Bshkara.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

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
                "~/Content/site.css"));

            /* METRONIC THEME */

            // Theme scripts
            bundles.Add(new ScriptBundle("~/CorePluginsScripts/js").Include(
                "~/Assets/global/plugins/jquery.min.js",
                "~/Assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Assets/global/plugins/js.cookie.min.js",
                "~/Assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Assets/global/plugins/jquery.blockui.min.js",
                "~/Assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                "~/Assets/global/plugins/bootstrap-toastr/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/ThemeGlobalScripts/js").Include(
                "~/Assets/global/scripts/app.min.js"));

            // Theme styles
            bundles.Add(new StyleBundle("~/GlobalMandatoryStyle/css").Include(
                "~/Assets/global/plugins/font-awesome/css/font-awesome.min.css",
                "~/Assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Assets/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/Assets/global/plugins/icheck/skins/all.css",
                "~/Assets/global/plugins/bootstrap-toastr/toastr.min.css"));

            bundles.Add(new StyleBundle("~/ThemeGlobalStyle/css").Include(
                "~/Assets/global/css/components.min.css",
                "~/Assets/global/css/plugins.min.css"));

            bundles.Add(new StyleBundle("~/Theme4/css").Include(
                "~/Assets/layouts/layout4/css/layout.min.css",
                "~/Assets/layouts/layout4/css/themes/default.min.css",
                "~/Assets/layouts/layout4/css/custom.min.css"));

            // Base pages
            bundles.Add(new StyleBundle("~/BasePageStyle/css").Include());
            bundles.Add(new ScriptBundle("~/BasePageScripts/js").Include(
                "~/Assets/pages/scripts/ui-toastr.min.js"));

            bundles.Add(new ScriptBundle("~/LoginPlugins/js").Include(
                "~/Assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Assets/global/plugins/jquery-validation/js/additional-methods.min.js",
                "~/Assets/global/plugins/icheck/icheck.min.js",
                "~/Assets/global/plugins/select2/js/select2.full.min.js"));

            bundles.Add(new StyleBundle("~/LoginStyles/css").Include(
                "~/Assets/pages/css/login.css"));

            bundles.Add(new StyleBundle("~/RegisterStyles/css").Include(
                "~/Assets/pages/css/register.css"));


            bundles.Add(new StyleBundle("~/ForgotPasswordStyles/css").Include(
                "~/Assets/pages/css/forgetpassword.css"));

            bundles.Add(new StyleBundle("~/ForgotPasswordConfirmationStyles/css").Include(
                "~/Assets/pages/css/forgotpasswordconfirmation.css"));
        }
    }
}