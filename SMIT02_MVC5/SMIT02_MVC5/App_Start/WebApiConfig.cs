using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using SMIT02_MVC5.Models;

namespace SMIT02_MVC5
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Member>("Members");
            
            builder.EntitySet<Item>("Items");
            builder.EntitySet<CartBuy>("CartBuys");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            
            builder.EntitySet<CartBuy>("CartBuys");
            builder.EntitySet<Item>("Items");
            
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
