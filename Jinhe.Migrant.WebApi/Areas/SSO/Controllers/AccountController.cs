using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jinhe.Migrant.WebApi.Areas.SSO.Controllers
{
    public class AccountController : Controller
    {
        // GET: SSO/Home
        public ActionResult Login()
        {
            return View();
        }
    }
}