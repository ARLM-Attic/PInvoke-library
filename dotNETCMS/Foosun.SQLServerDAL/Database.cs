using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Database : DbBase, IDatabase
    {
        public DataTable ExecuteSql(string sqlText)
        {
            DataTable dt = null;
            SqlConnection conn = new SqlConnection(DBConfig.CmsConString);
            conn.Open();
            try
            {
                dt = DbHelper.ExecuteTable(CommandType.Text, sqlText, null);
            }
            catch (SqlException e)
            {
                throw e;
            }
            if (conn != null && conn.State == ConnectionState.Open)
            {                
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public int backSqlData(int type, string backpath)
        {
            string s_dbCstring = "";
            if (type == 1)
                s_dbCstring = Foosun.Config.DBConfig.CmsConString;
            else if (type == 2)
                s_dbCstring = Foosun.Config.DBConfig.HelpConString;
            else
                s_dbCstring = Foosun.Config.DBConfig.CollectConString;

            string[] a_dbNamestring = s_dbCstring.Split(';');
            string[] a_dbNameS = a_dbNamestring[3].ToString().Split('=');

            string Sql = "backup DATABASE [" + a_dbNameS[1].ToString() + "] to disk='" + backpath + "' with format";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return 1;
        }

        public void Replace(string oldTxt, string newTxt, string Table, string FieldName)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@oldTxt", oldTxt), new SqlParameter("@newTxt", newTxt) };
            string Sql = "update [" + Table + "] set [" + FieldName + "]=replace([" + FieldName + "] ,@oldTxt,@newTxt)";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
    }
}
