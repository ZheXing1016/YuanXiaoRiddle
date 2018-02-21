using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 元宵灯谜
{
    /// <summary>
    /// result 的摘要说明
    /// </summary>
    public class result : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            yuanxiao.resultModule resultModule = new yuanxiao.resultModule();
            resultModule.result= "已经完成答题";
            string reval = Newtonsoft.Json.JsonConvert.SerializeObject(resultModule);
            context.Response.Write(reval);
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