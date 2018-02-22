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
    /// 2018-2-13 部署到服务器上还有点问题
    /// </summary>
    public class isriddlegot : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                yuanxiao.Initilazition.Init();
                string CHECKRIDDLE = context.Request.Form["CHECKRIDDLE"];//获取题组号
                logging.Infolog($"isriddlegot.ashx GET CHECKRIDDLE={CHECKRIDDLE}");
                yuanxiao.LoginMoudle status = new yuanxiao.LoginMoudle();
                string gotPID = dbwork.SelectSingle("PID", "RiddleGroup", $" GID={CHECKRIDDLE}");//查询对应题组号用户ID的值，如果为0，那说明没有，如果有，就要查询到对应用户ID的名字并返回
                if (gotPID != "0")
                {
                    string username = dbwork.SelectSingle("Pname", "Persons", $" PID={gotPID}");
                    status.status = username;//如果已经被选掉了，就返回被谁选掉了
                }
                else
                {
                    status.status = "0";//没有人选调，就返回0
                }
                context.Response.Write(JsonConvert.SerializeObject(status));
                logging.Infolog($"isriddlegot.ashx.ashx RETURN {JsonConvert.SerializeObject(status)}");
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