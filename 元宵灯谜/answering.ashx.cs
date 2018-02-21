using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass;
using System.Data;
using Newtonsoft.Json;

namespace 元宵灯谜
{
    /// <summary>
    /// answering 的摘要说明
    /// </summary>
    public class answering : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            yuanxiao.Initilazition.Init();
            string GID = context.Request.Form["GID"];
            string PNUM = context.Request.Form["PNUM"];
            string ANSWER = context.Request.Form["ANSWER"];
            string COSTTIME = context.Request.Form["COSTTIME"];
            int updateconut = 0;
            if (PNUM=="0")
            {
                updateconut =  dbwork.UpdateSet("Rrecord`Lasttime", $"~~~~`{DateTime.Now.ToString()}", "RiddleGroup", $" GID={GID}");
            }
            else
            {
                string preresult = dbwork.SelectSingle("Rrecord", 
                    "RiddleGroup", $" GID={GID}");//先获取原先的答案字符串
                int nowIndex = Convert.ToInt16(PNUM);
                string nowresult = preresult.Insert((nowIndex - 1) * 2, ANSWER);//对应位置公式为（nowindex-1) * 2
                updateconut =  dbwork.UpdateSet("Rrecord`Rcosttime`Lasttime",
                    $"{nowresult}`{COSTTIME}`{DateTime.Now.ToString()}", "RiddleGroup", $" GID={GID}");
            }


            yuanxiao.riddleContentGet rcg = new yuanxiao.riddleContentGet();
            rcg.ranswers = new yuanxiao.selection();
            if (PNUM != "5")
            {
                string[] Riddles = dbwork.SelectSingle("Griddles", "RiddleGroup", $"GID={GID}").Split('~');
                string nextpnum = Riddles[Convert.ToInt32(PNUM)];
                DataTable riddleContent = dbwork.SelectMutily($"select Rquestion,Ranswers from Riddle where Rpnum={nextpnum}");
                rcg.rquestion = riddleContent.Rows[0][0].ToString();
                string[] seletions = riddleContent.Rows[0][1].ToString().Split('~');
                rcg.ranswers.a = seletions[0];
                rcg.ranswers.b = seletions[1];
                rcg.ranswers.c = seletions[2];
                rcg.ranswers.d = seletions[3];
            }
            else
            {
                rcg.rquestion = "";
                rcg.ranswers.a = "";
                rcg.ranswers.b = "";
                rcg.ranswers.c = "";
                rcg.ranswers.d = "";
            }

            string restr = JsonConvert.SerializeObject(rcg);
            context.Response.Write(restr);
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