using System.Web;
using System.Web.Optimization;

namespace UserStories.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/group").Include("~/Scripts/userStory/groupManager/groupManager.js"));
            bundles.Add(new ScriptBundle("~/bundles/story").Include("~/Scripts/userStory/storyManager/storyManager.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

             //bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
             //                                                        "~/Scripts/kendo/jquery.js",
             //                                                        "~/Scripts/kendo/kendo.all.js",            
             //                                                        "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                                                                     "~/Scripts/kendo/jquery.js",
                                                                     "~/Scripts/kendo/kendo.all.js",
                                                                     "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/Bootstrap/css").Include("~/Content/Bootstrap/bootstrap.css"));

            //bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
            //                                                           "~/Content/kendo/kendo.common-material.css",
            //                                                            "~/Content/kendo/kendo.material.css",
            //                                                            "~/Content/kendo/kendo.material.mobile.css"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                                                                       "~/Content/kendo/kendo.common.css",
                                                                       "~/Content/kendo/kendo.dataviz.css",
                                                                       "~/Content/kendo/kendo.default.css"));
        }
    }
}