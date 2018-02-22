using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 元宵灯谜
{
    /// <summary>
    /// chooseriddle 的摘要说明
    /// </summary>
    public class chooseriddle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Logging logging = new Logging();
            try
            {
                yuanxiao.Initilazition.Init();
                string GID = context.Request.Form["GID"];
                string USER = context.Request.Form["USER"];
                logging.Infolog(typeof(chooseriddle), $"chooseriddle.ashx GET GID={GID},USER={USER}");
                yuanxiao.LoginMoudle status = new yuanxiao.LoginMoudle();
                string GotName = CommonClass.dbwork.SelectSingle("Pname", "RiddleGroup", $" GID={GID}");
                if (GotName == "")
                {
                    string PID = CommonClass.dbwork.SelectSingle("PID", "Persons", $" Pname='{USER}'");
                    if (PID == "")
                    {
                        status.status = "error";
                    }
                    else
                    {
                        int updateRowsCount = CommonClass.dbwork.UpdateSet("PID`Pname`Rrecord`Rcosttime`Lasttime", $"{PID}`{USER}`~~~~`00:00`{DateTime.Now.ToString()}", "RiddleGroup", $"GID={GID}");
                        if (updateRowsCount > 0)
                        {
                            status.status = "ok";
                        }
                        else
                        {
                            status.status = "error";
                        }
                    }

                }
                else
                {
                    status.status = GotName;
                }
                string reval = Newtonsoft.Json.JsonConvert.SerializeObject(status);
                context.Response.Write(reval);
                logging.Infolog(typeof(chooseriddle), $"chooseriddle.ashx RETURN {reval}");
                context.Response.End();
            }
            catch(Exception ex)
            {
                if (ex.Message != "正在中止线程。" && ex.Message != "Thread was being aborted.")
                {
                    logging.Errorlog(typeof(chooseriddle), ex.Message);
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