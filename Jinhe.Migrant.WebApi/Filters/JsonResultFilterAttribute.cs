using Jinhe.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jinhe.Migrant.WebApi.Filters
{
    /// <summary>
    /// 对Mvc返回的JsonResult进行统一包装
    /// </summary>
    public class JsonResultFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.Controller.ControllerContext.RequestContext.HttpContext.Request;
            HttpResponseBase httpResponse = filterContext.HttpContext.Response;
            ResponseData result = new ResponseData();
            // filterContext.RequestContext.HttpContext.Request.Headers
            bool isReturnJsonResult = false;
            if (filterContext.Result is JsonResult)
            {
                var response = filterContext.Result as JsonResult;
                var resultData = response.Data;
                result.Data = resultData;
                isReturnJsonResult = true;
            }
            else if ((request.IsAjaxRequest() || request.ContentType.Contains("application/json"))
                && filterContext.Result is ContentResult)
            {
                result.Data = (filterContext.Result as ContentResult).Content;
                isReturnJsonResult = true;
            }
            else if (filterContext.Exception != null)
            {
                // result = new ExceptionResult(filterContext.Exception);
            }
            else
            {
                base.OnActionExecuted(filterContext);
            }

            // 主要是为了解决MVC返回JsonResult时仍然使用JavaScriptSerializer序列化导致DateTime返回的是 @"\\/Date\((\d+)\)\\/"格式的问题
            if (isReturnJsonResult)
            {
                filterContext.Result = null;
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
            }
        }
    }
}