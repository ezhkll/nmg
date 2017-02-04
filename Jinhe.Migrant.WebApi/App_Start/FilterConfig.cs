using Jinhe.Migrant.WebApi.Filters;
using System.Web;
using System.Web.Mvc;

namespace Jinhe.Migrant.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JsonResultFilterAttribute());
            // new HandleErrorAttribute();
            // filters.Add(new ExceptionFilterAttribute());
        }
    }
}
