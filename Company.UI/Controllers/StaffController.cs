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
            return PartialView();
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
                list_staff = db.Staff.Where(p => p.StaffId != "").OrderByDescending(p=>p.Name).ToList();
            }
            if (!string.IsNullOrEmpty(lq_name))
            {
                list_staff = db.Staff.Where(p => p.Name.Contains(lq_name)).ToList();
            }
            var data = list_staff.Skip((page - 1) * limit).Take(limit);

            PagedList<Staff> list_paged = new PagedList<Staff>(data, page, limit, list_staff.Count);

            LayuiJsonHelper datajson = new LayuiJsonHelper();
            datajson.code = 0;
            datajson.msg = "";
            datajson.data = list_paged;
            datajson.count = list_staff.Count();
            return Json(datajson);
        }


        
        public ActionResult CreateOrEdit()
        {
            Staff staff = new Staff();
            string staffid = Request["id"];
            string action = Request["action"];
            if (!string.IsNullOrEmpty(action))
            {
                ViewBag.action = action;
            }
            if (!string.IsNullOrEmpty(staffid))
            {
                List<Staff> lstaff = new List<Staff>();
                staff = db.Staff.Where(p => p.StaffId.Equals(staffid)).ToList()[0];
            }
            return View("_CreateOrEdit",staff);//查看、编辑
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
            Staff staff = new Staff();
            HttpReturn httpReturn = new HttpReturn();

            if (!string.IsNullOrEmpty(id))
            {
                staff = db.Staff.Find(id);
                if (staff!=null)
                {
                    db.Staff.Remove(staff);
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
