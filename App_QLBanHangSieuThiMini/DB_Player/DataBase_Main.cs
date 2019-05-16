﻿using System.Data;
using System.Data.SqlClient;

namespace App_QLBanHangSieuThiMini.DB_Player
{
    internal class DataBase_Main
    {
        private string ConnStr = "Data Source=(local);Initial Catalog=QL_BanHang;Integrated Security=True ";
        private SqlConnection conn = null;
        private SqlCommand comn = null;
        private SqlDataAdapter da = null;
        private SqlDataReader dr = null;

        public DataBase_Main()
        {
            conn = new SqlConnection(ConnStr);
            comn = conn.CreateCommand();
        }
        public string MyExecuteQueryDataReader(string strSQL, CommandType ct)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            comn.CommandText = strSQL;
            comn.CommandType = ct;
            dr = comn.ExecuteReader();
            if (dr.Read())
                return dr["ChucDanh"].ToString();
            else
                return "0";

        }
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            comn.CommandText = strSQL;
            comn.CommandType = ct;
            da = new SqlDataAdapter(comn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string error)
        {
            bool f = false;
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            comn.CommandText = strSQL;
            comn.CommandType = ct;
            try
            {
                comn.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return f;
        }
    }
}