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
            yuanxiao.Initilazition.MaxRowsCount = 4;

            string LIMIT = context.Request.Form["LIMIT"];
            string LASTGID = context.Request.Form["LASTGID"];

            string[] RiddleListTmp = pushRiddle(LIMIT, LASTGID).Split('~');

            yuanxiao.riddleGet rg = new yuanxiao.riddleGet();
            rg.Riddles = new List<yuanxiao.riddles>();
            yuanxiao.riddles RiddleVal = new yuanxiao.riddles();//riddles的类，是在riddleGet类中的一个小类，用于存储题组号和答题人
            if (yuanxiao.Initilazition.MaxRowsCount % Convert.ToInt16(LIMIT) == 0)
                rg.totalpage = (yuanxiao.Initilazition.MaxRowsCount / Convert.ToInt16(LIMIT)).ToString();//获取totalpage最大页数
            else
                rg.totalpage = (yuanxiao.Initilazition.MaxRowsCount / Convert.ToInt16(LIMIT) + 1).ToString();//获取totalpage最大页数，不能整除最后要+1
            for (int i = 0; i < RiddleListTmp.Length; i++)
            {

                if (i % 2 == 0)
                {
                    RiddleVal.GID = RiddleListTmp[i];//添加题组号
                    if (i == RiddleListTmp.Length - 2)//获取lastGID最后一个的GID
                    {
                        rg.lastgid = RiddleListTmp[i];
                    }
                }
                else
                {
                    RiddleVal.APNAME = RiddleListTmp[i];//添加答题人
                    rg.Riddles.Add(RiddleVal);
                    RiddleVal = new yuanxiao.riddles();//由于类的存储采用的是指针，所以要new一个
                }
            }
            string val = JsonConvert.SerializeObject(rg);
            context.Response.Write(val);
            context.Response.End();
        }


        /// <summary>
        /// 返回该组题目情况，返回格式为“第1题题号~第1题被选情况~第2题题号~第2题被选情况~第3题题号~第3题被选情况……”
        /// 流程：第一次进入（lastGID为0）获取一个起始数字，并记录为STARTVAL，然后出LIIMIT条题目情况，下一次获取上一次的lastGID
        /// 在此基础上+1后再获取后面的LIMIT条。如果超过最大条则从1开始，直到STARTVAL-1的位置。
        /// 关于循环后位置处理：如果起始位置小于STARTVAL且最后的值大于等于STARTVAL，则提供题目数字直接到STARTVAL-1为止，
        /// 否则就直接出LIMIT条信息
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="lastGID"></param>
        /// <returns></returns>
        public string pushRiddle(string limit, string lastGID)
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