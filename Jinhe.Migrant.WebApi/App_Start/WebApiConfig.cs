using Jinhe.MvcExt.WebApi.Filters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Jinhe.Migrant.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            SwaggerConfig.Register(config);

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 启用跨域
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(corsAttr);

            config.Filters.Add(new ApiResultAttribute());
            config.Filters.Add(new ApiExceptionFilterAttribute());
            config.Filters.Add(new RequestLogFilter());
            config.Filters.Add(new MediaTypeFormatterAttribute());
        }
    }
}
