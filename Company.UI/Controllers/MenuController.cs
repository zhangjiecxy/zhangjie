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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="limit">每页数据量</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IndexResult(int page = 1, int limit = 30)
        {
            List<Menu> menu = new List<Menu>();
            string title = Request["title"];
            if (string.IsNullOrEmpty(title))
            {
                menu = db.Menu.Where(p => p.GUID != "").OrderByDescending(p => p.Seq).ToList();
            }
            if (!string.IsNullOrEmpty(title))
            {
                menu = db.Menu.Where(p => p.Title.Contains(title)).ToList();
            }
            var data = menu.Skip((page - 1) * limit).Take(limit);

            PagedList<Menu> pmenu= new PagedList<Menu>(data, page, limit, menu.Count);

            LayuiJsonHelper datajson = new LayuiJsonHelper();
            datajson.code = 0;
            datajson.msg = "";
            if (menu.Count() > 10)
                datajson.data = pmenu;
            else
                datajson.data = menu;
            datajson.count = menu.Count();
            return Json(datajson);
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
