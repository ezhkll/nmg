using Jinhe.Migrant.WebApi.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace Jinhe.Migrant.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            var result = new ExceptionResult(filterContext.Exception);
            HttpResponseBase httpResponse = filterContext.HttpContext.Response;
            filterContext.Result = new JsonResult()
            {
                Data = result
            };

            using (JsonTextWriter writer = new JsonTextWriter(httpResponse.Output) { Formatting = Formatting.Indented })
            {
                JsonSerializerSettings setting = new JsonSerializerSettings();
                var timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                setting.Converters.Add(timeFormat);
                JsonSerializer serializer = JsonSerializer.Create(setting);
                serializer.Serialize(writer, result);
                writer.Flush();
                writer.Close();
            }
            return;
        }
    }
}