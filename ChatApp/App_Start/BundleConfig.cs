// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="">
//   Copyright © 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.ChatApp
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/content/css/app").Include("~/content/app.css"));

            bundles.Add(new ScriptBundle("~/js/app").Include(

                    // libraries (jQuery, SignalR, angular)
                    "~/Scripts/vendor/jquery-2.0.3.js",
                    "~/Scripts/vendor/jquery.signalR-2.0.0.min.js",
                    "~/Scripts/vendor/angular.js",

                    // angular libraries (SignalR-Hub, cookies, ui-router, ui, ui-utils, animate)
                    "~/Scripts/vendor/angular-signalr-hub.js",
                    "~/Scripts/vendor/angular-cookies.js",
                    "~/scripts/vendor/angular-ui.js",
                    "~/scripts/vendor/ui-utils.js",
                    "~/scripts/vendor/angular-ui-router.js",
                    "~/scripts/vendor/angular-animate.js",
                    "~/scripts/vendor/angular-sanitize.js",

                    // my stuff (filters, services, directives, controllers, app)
                    "~/scripts/filters.js",
                    "~/scripts/services.js",
                    "~/scripts/directives.js",
                    "~/scripts/controllers.js",
                    "~/scripts/app.js"
                ) // end .Include
            ); //end .Add
        }
    }
}
