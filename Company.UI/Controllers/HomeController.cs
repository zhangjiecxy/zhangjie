using Company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Abp.Runtime.Security;
using System.Net.Http;
using Common;

namespace Company.UI.Controllers
{
    public class HomeController : Controller
    {
        //数据上下文对象
        private CompanyEntities db = new CompanyEntities();

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

        [HttpPost]
        public JsonResult GetMenuResult()
        {
            List<TreeModel> treeModel = new List<TreeModel>();
            List<Menu> Imenu = db.Menu.Where(p => p.ParentId == "" && p.GUID != "").ToList();

            treeModel = GetTreeList(Imenu);
            return Json(treeModel);
        }

        public List<TreeModel> GetTreeList(List<Menu> Imenu)
        {
            List<TreeModel> treeModel = new List<TreeModel>();            
            foreach (var item in Imenu)
            {
                TreeModel t = new TreeModel();
                t.id = item.GUID;
                t.title = item.Title;
                t.icon = item.Icon;
                t.href = item.Href;
                t.spread = false;//默认不展开
                t.children = GetTreeList(GetChildMenuList(item.GUID));
                treeModel.Add(t);
            }
            return treeModel;
        }

        /// <summary>
        /// 根据父节点获取子节点
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public List<Menu> GetChildMenuList(string ParentId)
        {
            List<Menu> Imenu = db.Menu.Where(p => p.GUID != "").ToList();
            var result= Imenu.Where(x => x.ParentId == ParentId);
            return result.ToList();
        }
    }
}