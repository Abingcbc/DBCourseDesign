using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiThrottle;
using System.Web.Http.Cors;

namespace DBCourseDesign
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            log4net.Config.XmlConfigurator.Configure();
            // Web API 配置和服务
            //config.Filters.Add(new RequireHttpsAttribute());
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Endpoint throttling based on IP
            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                Policy = new ThrottlePolicy(perSecond: 2, perMinute: 30)
                {
                    IpThrottling = true,
                    EndpointThrottling = true
                },
                Repository = new CacheRepository()
            });
        }
    }
}
