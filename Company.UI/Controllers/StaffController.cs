using Common;
using Company.IBLL;
using Company.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Webdiyer.WebControls.Mvc;

namespace Company.UI
{
    public class StaffController : Controller
    {
        //数据上下文对象
        private CompanyEntities db = new CompanyEntities();

        private IStaffService StaffService = BLLContainer.Container.Resolve<IStaffService>();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="limit">每页数据量</param>
        /// <returns></returns>
        [HttpPost]        
        public JsonResult IndexResult(int page=1,int limit=30)
        {
            List<Staff> list_staff = new List<Staff>();
            string lq_name = Request["username"];            
            if (string.IsNullOrEmpty(lq_name))
            {
                list_staff = db.Staff.Where(p => p.StaffId != "").ToList();
            }
            if (!string.IsNullOrEmpty(lq_name))
            {
                list_staff = db.Staff.Where(p => p.Name.Contains(lq_name)).ToList();
            }
            var data = list_staff.Skip((page - 1) * limit).Take(limit);

            PagedList<Staff> list_paged = new PagedList<Staff>(data, page, limit, list_staff.Count);

            DataJsonHelper datajson = new DataJsonHelper();
            datajson.code = 0;
            datajson.msg = "";
            datajson.data = list_paged;
            datajson.count = list_staff.Count();
            return Json(datajson);
        }



        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staff/Create
        public ActionResult CreateOrEdit()
        {
            string staffid = Request["id"];
            if (string.IsNullOrEmpty(staffid))
            {
                return View();
            }

            return View();
        }

        [HttpPost]
        public JsonResult CreateOrEditResult()
        {
            string msg = string.Empty;
            string json = Request["json"];
            HttpReturn httpReturn = new HttpReturn();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Staff staff = serializer.Deserialize<Staff>(json);
            staff.StaffId= Guid.NewGuid().ToString();
            db.Staff.Add(staff);
            db.Entry(staff).State = EntityState.Added;
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
        // POST: Staff/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffId,Name,Age,Sex")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staff.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staff);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staff/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId,Name,Age,Sex")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staff.Find(id);
            db.Staff.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
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
