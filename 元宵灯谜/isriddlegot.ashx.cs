using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass;
using Newtonsoft.Json;

namespace 元宵灯谜
{
    /// <summary>
    /// 判断题目是否被选掉了
    /// </summary>
    public class isriddlegot : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string CHECKRIDDLE = context.Request.Form["CHECKRIDDLE"];
            yuanxiao.LoginMoudle status = new yuanxiao.LoginMoudle();
            string gotPID = dbwork.SelectSingle("PID", "RiddleGroup", $" GID={CHECKRIDDLE}");
            if (gotPID != "0")
            {
                string username = dbwork.SelectSingle("Pname", "Persons", $" PID={gotPID}");
                status.status = username;
            }
            else
            {
                status.status = "0";
            }
            context.Response.Write(JsonConvert.SerializeObject(status));
            context.Response.End();
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