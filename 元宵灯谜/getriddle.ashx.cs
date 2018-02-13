using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace 元宵灯谜
{
    /// <summary>
    /// 2018-2-13 具体选题的算法还没有完成
    /// </summary>
    public class getriddle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string limit = context.Request.Form["limit"];
            string[] RiddleListTmp = pushRiddle(limit).Split('~');
            
            IList<yuanxiao.riddles> riddles = new List<yuanxiao.riddles>();
            yuanxiao.riddles RiddleVal = new yuanxiao.riddles();
            for (int i = 0; i < RiddleListTmp.Length; i++)
            {
               
                if (i % 2 == 0)
                {
                    RiddleVal.GID = RiddleListTmp[i];
                }
                else
                {
                   RiddleVal.APNAME = RiddleListTmp[i];
                   riddles.Add(RiddleVal);
                    RiddleVal = new yuanxiao.riddles();
                }
            }
            string val = JsonConvert.SerializeObject(riddles);
            context.Response.Write(val);
            context.Response.End();
        }

        public string pushRiddle(string limit)
        {
            //具体do点啥
            return "1~test~2~~3~";
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