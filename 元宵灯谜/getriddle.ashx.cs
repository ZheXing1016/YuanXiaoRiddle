﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data;

namespace 元宵灯谜
{
    /// <summary>
    /// 2018-2-13 具体选题的算法还没有完成
    /// 2018-2-14 目前还未完成循环节点位置的排序，如正常应该是8,9,1,2,3，目前出来的是1,2,3,8,9
    /// 2018-2-22 完成了选题循环的问题
    /// </summary>
    public class getriddle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            yuanxiao.Initilazition.takeMaxRowsCount();
            /*yuanxiao.Initilazition.MaxRowsCount = 4*/

            yuanxiao.Initilazition.Init();
            string PAGE = context.Request.Form["PAGE"];
            string LIMIT = context.Request.Form["LIMIT"];
            string LASTGID = context.Request.Form["LASTGID"];

            string[] RiddleListTmp = pushRiddle(PAGE, LIMIT, LASTGID).Split('~');

            yuanxiao.riddleGet rg = new yuanxiao.riddleGet();
            rg.Riddles = new List<yuanxiao.riddles>();
            yuanxiao.riddles RiddleVal = new yuanxiao.riddles();//riddles的类，是在riddleGet类中的一个小类，用于存储题组号和答题人
            if (yuanxiao.Initilazition.MaxRowsCount % Convert.ToInt16(LIMIT) == 0)
                rg.totalpage = (yuanxiao.Initilazition.MaxRowsCount / Convert.ToInt16(LIMIT)).ToString();//获取totalpage最大页数
            else
                rg.totalpage = (yuanxiao.Initilazition.MaxRowsCount / Convert.ToInt16(LIMIT) + 1).ToString();//获取totalpage最大页数，不能整除最后要+1
            for (int i = 0; i < RiddleListTmp.Length - 1; i++)
            {

                if (i % 2 == 0)
                {
                    RiddleVal.GID = RiddleListTmp[i];//添加题组号                   
                }
                else
                {
                    RiddleVal.APNAME = RiddleListTmp[i];//添加答题人
                    rg.Riddles.Add(RiddleVal);
                    RiddleVal = new yuanxiao.riddles();//由于类的存储采用的是指针，所以要new一个
                }
            }
            rg.lastgid = RiddleListTmp[RiddleListTmp.Length - 1];
            string val = JsonConvert.SerializeObject(rg);
            context.Response.Write(val);
            context.Response.End();
        }


        /// <summary>
        /// 返回该组题目情况，返回格式为“第1题题号~第1题被选情况~第2题题号~第2题被选情况~第3题题号~第3题被选情况……~最后一道题的题号”
        /// retun  = (page * limit + ( stratval + step(步进) ))%maxpagecount，要注意等于maxpagecount的情况
        /// 终点值，就是返回值的逆运算：(（maxpagecount + nowpagestartval）- limit*(page-1))%maxpagecount
        /// 查询名字：select Pname from RiddleGroup where GID=1 or GID=2 or GID=3 ...
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="lastGID"></param>
        /// <returns></returns>
        public string pushRiddle(string page, string limit, string lastGID)
        {
            bool isToStart = false;//判断是否循环到头了，比如789123，确保是这种顺序出现
            int startval = 0;
            string pushlastGID = "";
            if (lastGID == "0")
            {
                Random rd = new Random();
                startval = rd.Next(1, yuanxiao.Initilazition.MaxRowsCount);
            }
            else
            {
                startval = Convert.ToInt32(lastGID) + 1;
            }
            string reval = "";
            DataTable PnameList = new DataTable();
            string selectSTR = "";
            for (int i = 0; i < Convert.ToInt16(limit); i++)
            {
                int reGID = (i + startval) % yuanxiao.Initilazition.MaxRowsCount;
                if (reGID == 0)//当startval为最大值的时候，会出现余数为0的情况
                {
                    reGID = yuanxiao.Initilazition.MaxRowsCount;
                    isToStart = true;//当出现最大值的时候，就说明跨过了一个循环
                }
                if (page != "1")
                {
                    int StartPageStartVal = ((yuanxiao.Initilazition.MaxRowsCount + startval) -
                        Convert.ToInt32(limit) * (Convert.ToInt32(page) - 1)) % yuanxiao.Initilazition.MaxRowsCount;
                    if (reGID == StartPageStartVal)
                    {
                        pushlastGID = (reGID - 1).ToString();//考虑最后不一定能够达到limit的数量，所以破的时候也需要记录lastGID
                        if (pushlastGID == "0")//还是存在%计算时候，出现的边界情况
                        {
                            pushlastGID = yuanxiao.Initilazition.MaxRowsCount.ToString();
                        }
                        break;
                    }
                }
                selectSTR += $"GID={reGID} or ";

                if (i == Convert.ToInt16(limit) - 1)
                {
                    pushlastGID = reGID.ToString();//获取lastGID最后一个的GID   
                }

            }

            selectSTR = $"select Pname,GID from RiddleGroup where {selectSTR.Substring(0, selectSTR.Length - 3)}";
            if (isToStart)
            {
                PnameList = sortTableVal(CommonClass.dbwork.SelectMutily(selectSTR));
            }
            else
            {
                PnameList = CommonClass.dbwork.SelectMutily(selectSTR);
            }
            for (int i = 0; i < PnameList.Rows.Count; i++)
            {

                reval += PnameList.Rows[i][1] + "~" + PnameList.Rows[i][0] + "~";
            }

            return reval.Substring(0, reval.Length - 1) + "~" + pushlastGID;
        }


        /// <summary>
        /// 首先对全表遍历，如果发现下一行的GID-当前行的GID>1，则定下一行为divnum，并且跳出遍历循环
        /// 在获取divnum的前提下，将old里面的内容根据顺序，插入到dttmp中
        /// </summary>
        /// <param name="oldDataTable"></param>
        /// <returns></returns>
        DataTable sortTableVal(DataTable oldDataTable)
        {
            DataTable dttmp = new DataTable();
            dttmp.Columns.Add("Pname");
            dttmp.Columns.Add("GID");
            oldDataTable.Columns[1].DataType = typeof(int);
            dttmp.Columns[1].DataType = typeof(int);
            int divnum = 0;//分界线
            for (int rowindex = 0; rowindex < oldDataTable.Rows.Count; rowindex++)
            {
                if (rowindex < oldDataTable.Rows.Count - 1)
                {
                    if ((Convert.ToInt32(oldDataTable.Rows[rowindex + 1][1]) - Convert.ToInt32(oldDataTable.Rows[rowindex][1])) > 1)
                    {
                        divnum = rowindex + 1;//获取分界线//必须加1，break以等于divnum为准
                        break;
                    }                 
                }
            }
            int oldTatblerowindex = divnum;
            int dttmprowindex = 0;
            while (true)
            {            
                dttmp.Rows.Add();
                dttmp.Rows[dttmprowindex][0] = oldDataTable.Rows[oldTatblerowindex][0];
                dttmp.Rows[dttmprowindex][1] = oldDataTable.Rows[oldTatblerowindex][1];
                dttmprowindex++;
                oldTatblerowindex++;
                if(oldTatblerowindex>=oldDataTable.Rows.Count)
                {
                    oldTatblerowindex = 0;
                }
                if(oldTatblerowindex==divnum)
                {
                    break;
                }
            }
           
            return dttmp;

            // return oldDataTable;
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