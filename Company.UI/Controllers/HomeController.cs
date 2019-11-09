using Company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Abp.Runtime.Security;
using System.Net.Http;
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
            //byte[] buffer = WebClient.DownloadData("");
            // utf-8, gb2312, gbk, utf-1......
            //string html = System.Text.Encoding.GetEncoding("utf-8").GetString(buffer);
            HttpClient client = new HttpClient(new HttpClientHandler());
            var html = client.GetStringAsync("https://blog.csdn.net/qq_36051316/article/details/84380024").Result.ToString();

            return View();
        }

        public ActionResult Contact()
        {        
            return View();
        }
    }
}