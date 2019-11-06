using Company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.UI.Controllers
{
    public class HomeController : Controller
    {
        //数据上下文对象
        CompanyEntities db = new CompanyEntities();
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}