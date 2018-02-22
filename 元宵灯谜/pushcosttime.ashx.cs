using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass;
using Newtonsoft.Json;

namespace 元宵灯谜
{
    /// <summary>
    /// 2018-2-13完成
    /// </summary>
    public class pushcosttime : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Logging logging = new Logging();
            try
            {               
                string GID = context.Request.Form["GID"];
                string COSTTIME = context.Request.Form["COSTTIME"];
                logging.Infolog(typeof(pushcosttime), $" GET GID={GID},Password={COSTTIME}");
                dbwork.UpdateSet("Rcosttime`Lasttime", $"{COSTTIME}`{DateTime.Now.ToString()}", "RiddleGroup", $" GID={GID}");
                yuanxiao.LoginMoudle status = new yuanxiao.LoginMoudle();
                status.status = "OK";
                context.Response.Write(JsonConvert.SerializeObject(status));
                context.Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message != "正在中止线程。" && ex.Message != "Thread was being aborted.")
                {
                    logging.Errorlog(typeof(pushcosttime), ex.Message);
                    context.Response.Write("app error");
                    context.Response.End();
                }
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