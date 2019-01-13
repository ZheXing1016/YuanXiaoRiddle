using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass;
using System.Data;

namespace 元宵灯谜
{
    /// <summary>
    /// result 的摘要说明
    /// </summary>
    public class result : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Logging logging = new Logging();
            try
            {


                string GID = context.Request.Form["GID"];
                logging.Infolog(typeof(result), $" GET GID ={GID}");
                yuanxiao.resultModule resultModule = new yuanxiao.resultModule();
                DataTable lastReslutData = dbwork.SelectMutily($"select Rrecord,Rcosttime from RiddleGroup where GID={GID}");
                int correctNum = chkAnswer(GID, lastReslutData.Rows[0][0].ToString());
                if (correctNum == 5)
                {
                    resultModule.result = $"哇塞，好厉害，恭喜您在{lastReslutData.Rows[0][1]}的时间里面解出了该组所有灯谜！坐等公会发奖品吧(＾－＾)V";
                    resultModule.status = "1";
                }
                else if (correctNum == 0)
                {
                    resultModule.result = $"天啦噜，您花了{lastReslutData.Rows[0][1]}居然一道题目都没有解开，系统为您默哀1秒钟。好了，再接再厉吧，或者找老应伯要点安慰奖什么的，本系统为您作证！❥(^_-)";
                    resultModule.status = "-1";
                }
                else
                {
                    resultModule.result = $"您在{lastReslutData.Rows[0][1]}完成了该组灯谜的解答，共答对了{correctNum}题目。请再接再厉，要解出一组所有的灯谜才可以拿到公会准备的奖品哦ヾ(◍°∇°◍)ﾉﾞ";
                    resultModule.status = "0";

                }


                string reval = Newtonsoft.Json.JsonConvert.SerializeObject(resultModule);
                context.Response.Write(reval);
                logging.Infolog(typeof(result), $"RETURN {reval}");
                context.Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message != "正在中止线程。" && ex.Message != "Thread was being aborted.")
                {
                    logging.Errorlog(typeof(result), ex.Message);
                    context.Response.Write("app error");
                    context.Response.End();
                }
            }
        }

        int chkAnswer(string GID, string answerstr)
        {
            string[] RiddlesInGroup = dbwork.SelectSingle("Griddles", "RiddleGroup", $"GID={GID}").Split('~');
            string[] RightAnswers = new string[5];
            for (int RiddlesInGroupIndex = 0; RiddlesInGroupIndex < RiddlesInGroup.Length; RiddlesInGroupIndex++)
            {
                RightAnswers[RiddlesInGroupIndex] = dbwork.SelectSingle("Ranswer", "Riddle", $"Rpnum={RiddlesInGroup[RiddlesInGroupIndex]}");
            }
            string[] customAnswer = answerstr.Split('~');
            int correctCount = 0;
            for (int RightAnswersIndex = 0; RightAnswersIndex < RightAnswers.Length; RightAnswersIndex++)
            {                
                if (isAnswerRight(RightAnswers[RightAnswersIndex], customAnswer[RightAnswersIndex]))
                {
                    correctCount++;
                }
            }
            return correctCount;
        }


        public static bool isAnswerRight(string rightAnswerStr, string customAnswer)
        {
            string[] options = rightAnswerStr.Split('/');
            if (Array.IndexOf(options, customAnswer) > -1)
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