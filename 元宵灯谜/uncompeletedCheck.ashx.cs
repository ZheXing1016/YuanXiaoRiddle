using System;
using System.Collections.Generic;
using System.Web;
using CommonClass;
using Newtonsoft.Json;
using System.Data;

namespace yuanxiao
{
    /// <summary>
    /// 2018-2-12已用时间尚未推送出去
    /// 2018-2-13基本完成，test
    /// 2018-2-13 服务器上还有问题
    /// </summary>
    public class uncompeletedCheck : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Logging logging = new Logging();
            try
            {
                Initilazition.Init();
                riddleUncompeletedMoudle rum = new riddleUncompeletedMoudle();
                string username = context.Request.Form["UNCOMPELETED"];
                logging.Infolog(typeof(uncompeletedCheck), $"GET UNCOMPELETED={username}");
                string uncompeleted = isNoPauseAnswer(username);
                if (uncompeleted == "")
                {
                    rum.RID = "";
                    rum.PNUM = "";
                    rum.COSTTIME = "";

                }
                else
                {
                    string[] rumtmp = uncompeleted.Split('~');
                    rum.RID = rumtmp[0];
                    rum.PNUM = rumtmp[1];
                    rum.COSTTIME = rumtmp[2];
                }
                string reval = JsonConvert.SerializeObject(rum);
                context.Response.Write(reval);
                logging.Infolog(typeof(uncompeletedCheck), $"RETURN {reval}");
                context.Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message != "正在中止线程。" && ex.Message != "Thread was being aborted.")
                {
                    logging.Errorlog(typeof(uncompeletedCheck), ex.Message);
                    context.Response.Write("app error");
                    context.Response.End();
                }
            }
        }


        /// <summary>
        /// 根据PID来判断最后一次提交的那组题目中有没有没有答完的，返回空为没有中断的答题，返回error为有错误，返回 题组数~题目数  为有中断答题
        /// </summary>
        /// <returns></returns>
        public static string isNoPauseAnswer(string username)
        {
            string PID = dbwork.SelectSingle("PID", "Persons", $" Plogin='{username}'");
            if (PID != "")
            {
                DataTable RrecordStringList = dbwork.SelectMutily($"select Rrecord,GID,Rcosttime from RiddleGroup where Pname='{username}' order by Lasttime DESC");
                if (RrecordStringList.Rows.Count==0)
                {
                    return "";
                }
                else
                {

                    string[] strtmp = RrecordStringList.Rows[0][0].ToString().Split('~');//只看最后一次提交的那组题目是否存在未答完的情况
                    for (int j = 0; j < strtmp.Length; j++)//遍历数组，找到第一个没有答题的位置
                    {
                        if (strtmp[j] == "")
                        {
                            return RrecordStringList.Rows[0][1] + "~" + j+"~"+RrecordStringList.Rows[0][2];
                        }
                    }

                    return "";


                }
            }
            else
            {

                return  "error";
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