using System.Web.Http;
using System.Web.Mvc;

namespace Jinhe.Migrant.WebApi.Areas.SSO
{
    public class SSOAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SSO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SSO_default",
                "SSO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}