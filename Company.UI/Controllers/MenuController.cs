using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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


        public ActionResult CreateOrEdit()
        {
            Menu menu = new Menu();
            string id = Request["id"];
            string action = Request["action"];
            if (!string.IsNullOrEmpty(action))
            {
                ViewBag.action = action;
            }
            if (!string.IsNullOrEmpty(id))
            {
                List<Menu> lmenu = new List<Menu>();
                menu = db.Menu.Where(p => p.GUID.Equals(id)).ToList()[0];
            }
            return View("_CreateOrEdit", menu);//查看、编辑
        }

        [HttpPost]
        public JsonResult CreateOrEditResult()
        {
            string msg = string.Empty;
            string json = Request["json"];
            string action = Request["action"];
            HttpReturn httpReturn = new HttpReturn();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Menu menu = serializer.Deserialize<Menu>(json);
            menu.GUID = menu.GUID == "" ? Guid.NewGuid().ToString() : menu.GUID;
            menu.ParentId = menu.ParentId == "" ? "0" : menu.ParentId;
            menu.OperateTime = DateTime.Now;
            menu.Operater = Session["Account"]==null?"": Session["Account"].ToString();
            switch (action)
            {
                case "create":
                    db.Entry(menu).State = EntityState.Added;
                    break;
                default:
                    db.Entry(menu).State = EntityState.Modified;
                    break;
            }
            try
            {
                if (db.SaveChanges() > 0)
                {
                    httpReturn.msg = "";
                    httpReturn.code = 0;
                }
            }
            catch (Exception e)
            {
                httpReturn.code = 1;
                httpReturn.msg = e.Message;
            }

            return Json(httpReturn);
        }

        [HttpPost]
        public JsonResult DeleteResult()
        {
            string id = Request["id"];
            Menu menu = new Menu();
            HttpReturn httpReturn = new HttpReturn();

            if (!string.IsNullOrEmpty(id))
            {
                menu = db.Menu.Find(id);
                if (menu != null)
                {
                    db.Menu.Remove(menu);
                }
            }

            try
            {
                if (db.SaveChanges() > 0)
                {
                    httpReturn.msg = "";
                    httpReturn.code = 0;
                }
            }
            catch (Exception e)
            {
                httpReturn.code = 1;
                httpReturn.msg = e.Message;
            }

            return Json(httpReturn);
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
