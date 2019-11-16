using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common;
using Company.Model;
using MvcPager;

namespace Company.UI
{
    public class MenuController : Controller
    {
        //数据上下文对象
        private CompanyEntities db = new CompanyEntities();

        // GET: Menu
        public ActionResult Index()
        {
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
