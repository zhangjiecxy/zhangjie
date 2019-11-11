using Common;
using Company.Model;
using System;
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
            string Account = context.Request["Account"]==null?"": context.Request["Account"];          //账户
            string Pwd = context.Request["Pwd"]==null?"": context.Request["Pwd"];                    //密码
            string ls_checkcode = context.Request["code"]==null?"": context.Request["code"].ToLower();        //验证码
            string ls_Msg = string.Empty;
            string ls_dbref = string.Empty;

            switch (active)
            {
                case "login"://登录
                    try
                    {
                        string IPAddress = RequestHelper.GetIPAddress();

                        string ls_code = HttpContext.Current.Session["dt_session_code"].ToString().ToLower();
                        if (ls_checkcode != ls_code)
                        {
                            ls_Msg = "1";//验证码输入不正确
                        }
                        else
                        {
                            User_Login user_Login = db.User_Login.Find(Account);

                            string ls_name = user_Login == null ? null : user_Login.Login_name;
                            string ls_pwd = user_Login == null ? null : user_Login.Login_password;

                            if (string.IsNullOrEmpty(ls_name))
                            {
                                ls_Msg = "4"; //账户或密码不对
                            }
                            if (ls_name == Account && ls_pwd == Pwd)
                            {
                                context.Session["Account"] = Account;
                                context.Session["Pwd"] = Pwd;
                                context.Session["IP"] = IPAddress;
                                try
                                {
                                    #region 将用户信息存入用户信息表
                                    User_Login_Info user_Login_Info = new User_Login_Info();
                                    user_Login_Info.IP = IPAddress;
                                    user_Login_Info.PassWord = Base64Helper.EncodeBase64(Pwd);
                                    user_Login_Info.UserId = context.Request["ASP.NET_SessionId"];
                                    user_Login_Info.UserName = Account;
                                    user_Login_Info.DateTime = DateTime.Now.ToString();
                                    db.User_Login_Info.Add(user_Login_Info);
                                    db.SaveChanges();

                                    #endregion
                                }
                                catch (Exception e)
                                {

                                }

                                ls_Msg = "3";//验证成功
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ls_Msg = ex.Message;
                    }
                    context.Response.Write(ls_Msg);
                    context.Response.End();

                    break;
            }
        }

        public void InsertUserInfo()
        {

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