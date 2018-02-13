using System;
using System.Collections.Generic;
using System.Web;

namespace yuanxiao
{
    public class Initilazition
    {
        public static void Init()
        {
            CommonClass.getconnstr.connstr = @"Provider=Microsoft.Ace.OleDb.12.0;Data Source="+
                AppDomain.CurrentDomain.BaseDirectory
                + @"\maindb.accdb;Persist Security Info=False;";
        }
    }
}