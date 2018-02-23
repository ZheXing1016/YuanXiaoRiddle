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
            Logging logging = new Logging();
            try
            {
                yuanxiao.Initilazition.Init();
                string GID = context.Request.Form["GID"];
                string PNUM = context.Request.Form["PNUM"];
                string ANSWER = context.Request.Form["ANSWER"];
                string COSTTIME = context.Request.Form["COSTTIME"];
                logging.Infolog(typeof(answering), $" GET GID={GID},PNUM={PNUM},ANSWER={ANSWER},COSTTIME={COSTTIME}");
                int updateconut = 0;
                if (PNUM == "0")
                {
                    updateconut = dbwork.UpdateSet("Rrecord`Lasttime", $"~~~~ `{DateTime.Now.ToString()}", "RiddleGroup", $" GID={GID}");
                    logging.Infolog(typeof(answering), $" DATA getNew");
                }
                else
                {
                    string preresult = dbwork.SelectSingle("Rrecord",
                        "RiddleGroup", $" GID={GID}");//先获取原先的答案字符串
                    int nowIndex = Convert.ToInt32(PNUM);
                    string nowresult = "";
                    if (ANSWER == "U")
                    {
                        nowresult = makeAnswerStr(preresult, nowIndex, ANSWER);
                        updateconut = dbwork.UpdateSet("Rrecord`Rcosttime`Lasttime",
                            $"{nowresult}`{COSTTIME}`{DateTime.Now.ToString()}", "RiddleGroup", $" GID={GID}");
                        logging.Infolog(typeof(answering), $" DATA {preresult}->{nowresult}");
                    }
                }


                yuanxiao.riddleContentGet rcg = new yuanxiao.riddleContentGet();
                if (PNUM != "5")
                {
                    string[] Riddles = dbwork.SelectSingle("Griddles", "RiddleGroup", $"GID={GID}").Split('~');
                    string nextpnum = Riddles[Convert.ToInt32(PNUM)];
                    DataTable riddleContent = dbwork.SelectMutily($"select Rquestion,Rtip from Riddle where Rpnum={nextpnum}");
                    rcg.rquestion = riddleContent.Rows[0][0].ToString();
                    rcg.rtips = riddleContent.Rows[0][1].ToString();

                }
                else
                {
                    rcg.rquestion = "";
                    rcg.rtips = "";
                }

                string restr = JsonConvert.SerializeObject(rcg);
                context.Response.Write(restr);
                logging.Infolog(typeof(answering), $" RETURN {restr}");
                context.Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message != "正在中止线程。" && ex.Message != "Thread was being aborted.")
                {
                    logging.Errorlog(typeof(answering), ex.Message);
                    context.Response.Write("app error");
                    context.Response.End();
                }
            }


        }

        string makeAnswerStr(string oldAnswerStr, int Pnum, string NewAnswer)
        {
            string[] strtmp = oldAnswerStr.Split('~');
            strtmp[Pnum - 1] = NewAnswer;
            string reval = "";
            for (int i = 0; i < Pnum; i++)
            {
                reval += $"{strtmp[i]}~";
            }
            return reval.Substring(0, reval.Length - 1);
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