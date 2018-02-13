using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonClass;
using System.Data;


/// <summary>
/// 2018-2-12 
/// url中pnum参数总是比实际的rpum少1，导致最后一题打完还要再按一次，是否和server.transfar有关？
/// 题目乱码问题
/// 结果提交尚未实现
/// 答题完成后跳转尚未实现
/// 题目提示
/// </summary>
namespace yuanxiao
{
    public partial class mainAnwser : System.Web.UI.Page
    {
        string Rid = "", Pnum = "";//rid为题组号，对应的应该是group里面的gid，Pnum序号，第几题。

        protected void Page_Load(object sender, EventArgs e)
        {

          
                Rid = Request.QueryString["rid"];
                Pnum = Request.QueryString["Pnum"];
           



            if (!IsPostBack)
            {

                Initilazition.Init();
                PostQuestionToPage();
            }
        }

        void PostQuestionToPage()
        {
            if (Pnum == "1")
            {
                GetRiddlesCache();
            }
            string Unserializestring = ReadCache(Pnum);
            if (Unserializestring == "")
            {

            }
            else
            {
                IList<string> riddleDetail = serializeRiddle(Unserializestring);
                Rquestion.InnerText = riddleDetail[0];
                AnswerA.Text = riddleDetail[1];
                AnswerB.Text = riddleDetail[2];
                AnswerC.Text = riddleDetail[3];
                AnswerD.Text = riddleDetail[4];
            }

        }

        /// <summary>
        /// 将题目列表插入到Cookies中，存储规则为YuanXiao{Rid}-{Rpnum}  ：  值
        /// </summary>
        void GetRiddlesCache()
        {
            IList<string> RquestionList = GetRiddles(Rid);
            int Rpnum = 1;//用于预先读取题目时候计数。
            foreach (string Rquestion in RquestionList)
            {
                HttpCookie httpCookie = new HttpCookie($"YuanXiao{Rid}-{Rpnum}");//Cookies存储规则为YuanXiao{Rid}-{Rpnum}  ：  值
                httpCookie.Value = Rquestion;
                Response.Cookies.Add(httpCookie);
                Rpnum++;
            }
        }


        /// <summary>
        /// 根据当前题目数，获取缓存中的题目内容
        /// </summary>
        /// <param name="Rpnum"></param>
        /// <returns></returns>
        string ReadCache(string Rpnum)
        {
            if (Request.Cookies[$"YuanXiao{Rid}-{Rpnum}"] != null)
            {
                return Request.Cookies[$"YuanXiao{Rid}-{Rpnum}"].Value;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据对应字符串序列化成可推送到页面上的列表,0为题目，1为答案A，2为答案B，3为答案C，4为答案D
        /// </summary>
        /// <param name="unserializeRiddleString"></param>
        /// <returns></returns>
        IList<string> serializeRiddle(string unserializeRiddleString)
        {
            IList<string> val = new List<string>();
            string[] strtmp = unserializeRiddleString.Split('~');
            foreach (string Str in strtmp)
            {
                val.Add(Str);
            }
            return val;

        }



        /// <summary>
        /// 根据题组的ID返回对应题目的文本内容
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        IList<string> GetRiddles(string GroupID)
        {
            IList<string> val = new List<string>();
            string riddlenumtmp = dbwork.SelectSingle("Griddles", "RiddleGroup", " GID=" + GroupID);//获取对应题组里面包含的题目，以“~”分割
            string[] riddlenums = riddlenumtmp.Split('~');//获取的数据进行分割，形成数组
            string selectCMD = "select Rquestion,Ranswers from Riddle where ";
            foreach (string riddlenum in riddlenums)
            {
                selectCMD += " Rpnum = " + riddlenum + " or ";
            }
            selectCMD = selectCMD.Substring(0, selectCMD.Length - 3) + " order by Rpnum asc";
            //select Rquestion,Ranswers from Riddle where Rpnum=1 or Rpnum=2or Rpnum=3 or Rpnum=4 or Rpnum=5
            DataTable dt = dbwork.SelectMutily(selectCMD);//获取对应题目
            for (int questionindex = 0; questionindex < dt.Rows.Count; questionindex++)
            {
                string inval = dt.Rows[questionindex][0] + "~" + dt.Rows[questionindex][1];
                val.Add(inval);
            }
            //5行2列的表，最终合成一个字段，题目~答案A~答案B~答案C~答案D
            return val;
        }



        protected void next_Click(object sender, EventArgs e)
        {
            updateresult();
            if (Pnum == "5")
            {

            }
            else
            {
                int nextPnum = Convert.ToInt16(Pnum);
                nextPnum++;
                Server.Transfer($"mainAnwser.aspx?rid={Rid}&Pnum={nextPnum}");
            }
        }

        void updateresult()
        {

        }
    }
}