using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace CommonClass
{
    class dbwork//直接针对数据库进行操作的类
    {
        public static string SelectSingle(string col, string tablename, string foctrystr)//输入查找列，表名和查找条件（where后面的内容）返回一条内容
        {
            OleDbConnection conn = new OleDbConnection(getconnstr.connstr);
            OleDbCommand comm = new OleDbCommand();
            comm.Connection = conn;
            conn.Open();            
            comm.CommandText = "select "+ col +" from "+ tablename +" where "+ foctrystr +"";
            object ExecuteScalarTmp = comm.ExecuteScalar();
            string val = "";
            if (ExecuteScalarTmp!=null)
            {
                val = ExecuteScalarTmp.ToString();
            }            
            conn.Close();
            return val;
        }
        public static string SelectSingle(string col, string tablename)//输入查找列，表名和查找条件（where后面的内容）返回一条内容
        {
            OleDbConnection conn = new OleDbConnection(getconnstr.connstr);
            OleDbCommand comm = new OleDbCommand();
            comm.Connection = conn;
            conn.Open();
            comm.CommandText = "select " + col + " from " + tablename ;
            object ExecuteScalarTmp = comm.ExecuteScalar();
            string val = "";
            if (ExecuteScalarTmp != null)
            {
                val = ExecuteScalarTmp.ToString();
            }
            conn.Close();
            return val;
        }

        public static DataTable SelectMutily(string selectstr)//直接输入查找语句返回多行（datatable形式）
        {
            OleDbConnection conn = new OleDbConnection(getconnstr.connstr);
            OleDbCommand comm = new OleDbCommand();
            comm.Connection = conn;
            conn.Open();
            comm.CommandText = selectstr;
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(comm);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public static int InsertInto(string colnamess, string valuess,string tablename)//输入列名，值（多项用`分隔）和表名插入，返回插入多少行数
        {
            string[] colnames = colnamess.Split('`');
            string[] values = valuess.Split('`');
            if (colnames.Length == values.Length)
            {
                string colname = "", value = "";
                for (int i = 0; i < values.Length; i++)
                {
                    colname += colnames[i] + ",";
                    value += "\'" + values[i] + "\',";
                }
                OleDbConnection conn = new OleDbConnection(getconnstr.connstr);
                OleDbCommand comm = new OleDbCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "insert into " + tablename + "(" + colname.Substring(0, colname.Length - 1) + ") values(" + value.Substring(0, value.Length - 1) + ")";
                int val = comm.ExecuteNonQuery();
                conn.Close();
                return val;
            }
            else
            {
                return -1;
            }
        }

        public static int UpdateSet(string colnamess, string valuess, string tablename, string foctrystr)//输入列名，值（多项用`分隔）和表名插入，返回更新多少行数
        {
            string[] colnames = colnamess.Split('`');
            string[] values = valuess.Split('`');
            if (colnames.Length == values.Length)
            {
                string updatestr = "";
                for (int i = 0; i < values.Length; i++)
                {
                    updatestr += colnames[i] + "=\'" + values[i] + "\',";
                }
                OleDbConnection conn = new OleDbConnection(getconnstr.connstr);
                OleDbCommand comm = new OleDbCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "update " + tablename + " set " + updatestr.Substring(0, updatestr.Length - 1) + " where " + foctrystr;
                int val = comm.ExecuteNonQuery();
                conn.Close();
                return val;
            }
            else
            {
                return -1;
            }
        }

        public static int DeleteFrom(string tablename, string foctrystr)//输入表名和条件，返回删除行数
        {
            OleDbConnection conn = new OleDbConnection(getconnstr.connstr);
            OleDbCommand comm = new OleDbCommand();
            comm.Connection = conn;
            conn.Open();
            comm.CommandText = "delete from " + tablename + " where " + foctrystr;
            int val = comm.ExecuteNonQuery();
            conn.Close();
            return val;
        }
    }
}
