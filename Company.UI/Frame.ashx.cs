
using baseclass;
using Common;
using Company.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace Company.UI
{
    /// <summary>
    /// Frame 的摘要说明
    /// </summary>
    public class Frame : IHttpHandler, IRequiresSessionState
    {
        //数据上下文对象
        private CompanyEntities db = new CompanyEntities();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";
            string active = HttpContext.Current.Request["action"];
            string Account = context.Request["Account"];          //账户
            string Pwd = context.Request["Pwd"];                    //密码
            string code = context.Request["code"].ToLower();        //验证码
            string Msg = "";
            string UserId = "";
            switch (active)
            {
                case "login"://登录
                    try
                    {
                        string IPAddress = RequestHelper.GetIPAddress();

                        string ls_code = HttpContext.Current.Session["dt_session_code"].ToString().ToLower();
                        if (code != ls_code)
                        {
                            Msg = "1";//验证码输入不正确
                        }
                        else
                        {
                            string ls_name1  = db.User_Login.Find(Account).Login_name;
                             db.User_Login.SqlQuery("select Login_name from User_Login where Login_name=@loginname"
                                , new SqlParameter("@loginname", Account));

                            string ls_name = SqlHelper.ExecuteScalar("select Login_name from User_Login where Login_name=@loginname", "@loginname=" + Account);
                            string ls_pwd = SqlHelper.ExecuteScalar("select Login_password from User_Login where Login_name=@loginname", "@loginname=" + Account);

                            if (string.IsNullOrEmpty(ls_name))
                            {
                                Msg = "4"; //账户或密码不对
                            }
                            if (ls_name == Account && ls_pwd == Pwd)
                            {
                                context.Session["Account"] = Account;
                                context.Session["Pwd"] = Pwd;
                                Msg = "3";//验证成功
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Msg = ex.Message;
                    }
                    context.Response.Write(Msg);
                    context.Response.End();
                    break;
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}