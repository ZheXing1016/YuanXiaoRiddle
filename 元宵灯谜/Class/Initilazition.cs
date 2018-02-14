using System;
using System.Collections.Generic;
using System.Web;
using CommonClass;

namespace yuanxiao
{
    public class Initilazition
    {
        public static int MaxRowsCount;
        public static void Init()
        {
            getconnstr.connstr = @"Provider=Microsoft.Ace.OleDb.12.0;Data Source="+
                AppDomain.CurrentDomain.BaseDirectory
                + @"\maindb.accdb;Persist Security Info=False;";
           
        }
        public static void takeMaxRowsCount()
        {
            MaxRowsCount = Convert.ToInt32(dbwork.SelectSingle("count(*)", "RiddleGroup"));//获取最大页数，方便懒加载
        }
    }
}