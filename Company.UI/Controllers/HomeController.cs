﻿using Company.Model;
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
            string action = Request["action"];
            if (string.IsNullOrEmpty(action))
            {
                return Json("");
            }
            List<TreeModel> treeModel = new List<TreeModel>();
            List<TreeBost> treeBost = new List<TreeBost>();
            List<Menu> Imenu = db.Menu.Where(p => p.ParentId == "0" && p.GUID != "").ToList();
            if (action == "lay")
            {
                treeModel = GetTreeList(Imenu);
                return Json(treeModel);
            }
            else
            {
                treeBost = GetBoostrapTreeList(Imenu);
                return Json(treeBost);
            }
        }

        public List<TreeModel> GetTreeList(List<Menu> Imenu)
        {
            List<TreeModel> treeModel = new List<TreeModel>();            
            foreach (var item in Imenu)
            {
                TreeModel t = new TreeModel();
                t.id = item.GUID;
                t.Title = item.Title;
                t.Icon = item.Icon;
                t.Href = item.Href;
                t.spread = false;//默认不展开
                t.ParentId = item.ParentId;                
                t.children = GetTreeList(GetChildMenuList(item.GUID));
                treeModel.Add(t);
            }
            return treeModel;
        }

        public List<TreeBost> GetBoostrapTreeList(List<Menu> Imenu)
        {
            List<TreeBost> treeBost  = new List<TreeBost>();
            foreach (var item in Imenu)
            {
                TreeBost t = new TreeBost();
                t.id = item.GUID;
                t.text = item.Title;                
                t.href = item.Href;
                t.pid = item.ParentId;
                t.icon = item.Icon;
                t.seq = item.Seq;
                List<TreeBost> trees = new List<TreeBost>();
                trees = GetBoostrapTreeList(GetChildMenuList(item.GUID));
                t.nodes = trees.Count == 0 ? null : trees;
                treeBost.Add(t);
            }
            return treeBost;
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