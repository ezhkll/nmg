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
    public class ExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            HttpResponseBase httpResponse = filterContext.HttpContext.Response;
            ResponseData responseData = new ResponseData()
            {
                Message = filterContext.Exception.Message
            };
            if (filterContext.Exception is NotImplementedException)
            {

            }
            else if (filterContext.Exception is Exceptions.ASoftException)
            {
                responseData.Code = (filterContext.Exception as ASoftException).Code;
            }
            else if (filterContext.Exception.GetType()?.BaseType.FullName == "System.Data.Common.DbException")
            {
                //MapDbExceptionRespose(context.Exception, ref responseData);
            }
            else if (filterContext.Exception is System.FormatException)
            {
                responseData.Code = RetCode.C1002;
                responseData.Message = "输入的参数格式错误";
            }
            else
            {
                responseData.Code = RetCode.C1111;
            }

            filterContext.Result = null;
            using (JsonTextWriter writer = new JsonTextWriter(httpResponse.Output) { Formatting = Formatting.Indented })
            {
                JsonSerializerSettings setting = new JsonSerializerSettings();
                var timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                setting.Converters.Add(timeFormat);
                JsonSerializer serializer = JsonSerializer.Create(setting);
                serializer.Serialize(writer, responseData);
                writer.Flush();
                writer.Close();
            }
        }
    }
}