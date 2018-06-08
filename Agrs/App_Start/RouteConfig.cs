using System.Web.Mvc;
using System.Web.Routing;

namespace Agrs
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: null,
                url: "Agrs/F1/Save",
                defaults: new { controller = "F1", action = "Save" }
            );
            routes.MapRoute(
                name: null,
                url: "Agrs/F1/Filter/GetDataForFilteredView",
                defaults: new { controller = "F1", action = "GetDataForFilteredView" }
            );
            routes.MapRoute(
                name: null,
                url: "Agrs/F1/SelectorWithListBox/GetData",
                defaults: new { controller = "F1", action = "GetDataForSelectorWithListBox" }
            );
            routes.MapRoute(
                name: null,
                url: "Agrs/F1/FilteredView/GetDataForDetailSection",
                defaults: new { controller = "F1", action = "GetDataForDetailSection" }
            );
            routes.MapRoute(
                name: null,
                url: "Agrs/F1",
                defaults: new { controller = "F1", action = "Index" }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*pathInfo}");
        }
    }
}
