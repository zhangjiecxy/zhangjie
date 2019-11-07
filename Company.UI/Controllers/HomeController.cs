using Company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Abp.Runtime.Security;

namespace Company.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Response.Redirect("~/Index.aspx");
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {        
            return View();
        }
    }
}