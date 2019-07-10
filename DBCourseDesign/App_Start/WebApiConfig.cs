﻿using System;
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
            // Web API 配置和服务
            //config.Filters.Add(new RequireHttpsAttribute());
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API 路由
            var cors1 = new EnableCorsAttribute("http://10.0.1.8", "*", "*");
            config.EnableCors(cors1);
            var cors2 = new EnableCorsAttribute("http://172.16.42.19:8000", "*", "*");
            config.EnableCors(cors2);
            var cors3 = new EnableCorsAttribute("http://172.16.42.20:8000", "*", "*");
            config.EnableCors(cors3);
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
