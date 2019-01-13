using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass;

namespace 元宵灯谜
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class checkanswer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Logging logging = new Logging();
            try
            {
                string GID = context.Request.Form["GID"];
                string PNUM = context.Request.Form["PNUM"];
                string ANSWER = context.Request.Form["ANSWER"];
                logging.Infolog(typeof(checkanswer), $" GET GID={GID},PNUM={PNUM},ANSWER={ANSWER}");
                string[] RiddleNums = dbwork.SelectSingle("Griddles", "RiddleGroup", $"GID={GID}").Split('~');
                string RiddleNum = RiddleNums[Convert.ToInt16(PNUM) - 1];
                string rightAnswerStr = dbwork.SelectSingle("Ranswer", "Riddle", $"Rpnum={RiddleNum}");
                yuanxiao.LoginMoudle status = new yuanxiao.LoginMoudle();
                if (result.isAnswerRight(rightAnswerStr, ANSWER))
                {
                    status.status = "1";
                }
                else
                {
                    status.status = "0";
                }

                string restr = Newtonsoft.Json.JsonConvert.SerializeObject(status);
                context.Response.Write(restr);
                logging.Infolog(typeof(checkanswer), $" RETURN {restr}");
                context.Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message != "正在中止线程。" && ex.Message != "Thread was being aborted.")
                {
                    logging.Errorlog(typeof(checkanswer), ex.Message);
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