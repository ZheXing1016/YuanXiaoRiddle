using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using CommonClass;
using System.Data;

namespace yuanxiao
{
    /// <summary>
    /// Login1 的摘要说明
    /// </summary>
    public class Login1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Logging logging = new Logging();
            //try
            //{
                Initilazition.Init();
                string Username = context.Request.Form["USER"];
                string Password = context.Request.Form["PASSWORD"];
                logging.Infolog(typeof(Login1), $"login.ashx GET Username={Username},Password={Password}");
                LoginMoudle login = new LoginMoudle();
                if (isLoginOK(Username, Password))
                {
                    string uncompeleted = uncompeletedCheck.isNoPauseAnswer(Username);
                    switch (uncompeleted)
                    {
                        case "error":
                            login.status = "error";
                            break;
                        case "":
                            login.status = "0";
                            break;
                        default:
                            login.status = "1";
                            break;
                    }
                }
                else
                {
                    login.status = "-1";
                }

                string reval = JsonConvert.SerializeObject(login);//转成对应的json
              //LoginMoudle.status=1  =>  {"status":"1"};
                context.Response.Write(reval);
                logging.Infolog(typeof(Login1), $"login.ashx RETURN {reval}");
                context.Response.End();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message != "正在中止线程。" && ex.Message != "Thread was being aborted.")
            //    {
            //        logging.Errorlog(typeof(Login1), ex.Message);
            //        context.Response.Write("app error");
            //        context.Response.End();
            //    }

            //}
        }


            /// <summary>
            /// 判断登入用户名和密码是否正确
            /// </summary>
            /// <param name="username"></param>
            /// <param name="password"></param>
            /// <returns></returns>
            bool isLoginOK(string username, string password)
            {
                string usercount = dbwork.SelectSingle("count(*)", "Persons", $" Plogin='{username}' and Ppassword='{password}'");
                if (Convert.ToInt16(usercount) >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
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