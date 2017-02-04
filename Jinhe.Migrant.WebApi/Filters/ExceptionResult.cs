using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jinhe.Migrant.WebApi.Filters
{
    public class ResponseData
    {
        public RetCode Code { set; get; }

        public string Message { set; get; }

        public bool Success
        {
            get
            {
                return this.Code == RetCode.C0000;
            }
        }

        public object Data { set; get; }
    }

    public class ExceptionResult : ResponseData
    {
        public ExceptionResult(System.Exception ex)
        {
            Message = ex.Message;
            if (ex is NotImplementedException)
            {

            }
            else if (ex is Exceptions.ASoftException)
            {
                this.Code = (ex as ASoftException).Code;
            }
            else if (ex.GetType()?.BaseType.FullName == "System.Data.Common.DbException")
            {
                //MapDbExceptionRespose(context.Exception, ref responseData);
            }
            else if (ex is System.FormatException)
            {
                Code = RetCode.C1002;
                Message = "输入的参数格式错误";
            }
            else
            {
                Code = RetCode.C1111;
            }
        }
    }
}