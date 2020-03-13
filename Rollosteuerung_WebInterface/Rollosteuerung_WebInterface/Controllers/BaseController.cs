using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Rollosteuerung_WebInterface.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        public static IPrincipal BaseUser { get; set; }

        public static HttpContextBase Context { get; set; }

    }
}