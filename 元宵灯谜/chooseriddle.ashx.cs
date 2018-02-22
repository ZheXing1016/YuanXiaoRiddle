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
            try
            {
                yuanxiao.Initilazition.Init();
                string GID = context.Request.Form["GID"];
                string USER = context.Request.Form["USER"];
                logging.Infolog($"chooseriddle.ashx GET GID={GID},USER={USER}");
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
                        int updateRowsCount = CommonClass.dbwork.UpdateSet("PID`Pname`Lasttime", $"{PID}`{USER}`{DateTime.Now.ToString()}", "RiddleGroup", $"GID={GID}");
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
                logging.Infolog($"chooseriddle.ashx RETURN {reval}");
                context.Response.End();
            }
            catch(Exception ex)
            {
                logging.Errorlog(ex.ToString());
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