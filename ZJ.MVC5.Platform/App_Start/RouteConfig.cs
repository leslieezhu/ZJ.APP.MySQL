using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZJ.MVC5.Platform
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
            ////defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
            ////defaults: new { controller = "Test", action = "ShopPic", id = UrlParameter.Optional },
            //namespaces: new string[] { "ZJ.MVC5.Platform.Areas.Movie" } //影片管理
            //).DataTokens.Add("Area", "Movie");
            defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
            namespaces: new string[] { "ZJ.MVC5.Platform.Areas.QuestionBank" }
            ).DataTokens.Add("Area", "QuestionBank");


        }


    }
}
