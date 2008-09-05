using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;
using System.Web;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Foosun.AccessDAL
{
    public class Database : DbBase, IDatabase
    {
        //static public string FoosunConnectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(DBConfig.CmsConString);
        //static public string HelpKeyConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(DBConfig.HelpConString);
        //static public string CollectConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(Database.CollectConnectionString);
        static public string FoosunConnectionString = DBConfig.CmsConString;
        static public string HelpKeyConnectionString = DBConfig.HelpConString;
        static public string CollectConnectionString = DBConfig.CollectConString;

        public DataTable ExecuteSql(string sqlText)
        {
            DataTable dt = null;
            string Connstr = FoosunConnectionString;
            OleDbConnection conn = new OleDbConnection(Connstr);
            conn.Open();
            try
            {
                dt = DbHelper.ExecuteTable(CommandType.Text, sqlText, null);
            }
            catch (OleDbException e)
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
            //OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@oldTxt", oldTxt), new OleDbParameter("@newTxt", newTxt) };
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataTable dt = new DataTable();

            da.SelectCommand = new OleDbCommand("select [" + FieldName + "],ID from [" + Table + "]", new OleDbConnection(DBConfig.CmsConString));
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][FieldName] = dt.Rows[i][FieldName].ToString().Replace(oldTxt, newTxt);
            }
            OleDbCommand olecmd = new OleDbCommand("Update [" + Table + "] set [" + FieldName + "]=@FieldName where id=@ID ", new OleDbConnection(DBConfig.CmsConString));
            olecmd.Parameters.Add(@"@"+FieldName,OleDbType.VarChar,200,FieldName);
            olecmd.Parameters.Add("@ID", OleDbType.Integer, 16,"ID");
            da.UpdateCommand = olecmd;
            da.Update(dt);
            dt.Dispose();
            da.Dispose();
            //string Sql = "update [" + Table + "] set [" + FieldName + "]=replace([" + FieldName + "] ,@oldTxt,@newTxt)";
            //DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        /// <summary>
        /// 取得参数名称排列
        /// code by arjun
        /// </summary>
        /// <param name="ole">OleDbParameter</param>
        /// <returns>参数名称排列</returns>
        public static string getParam(OleDbParameter[] ole)
        {
            StringBuilder str=new StringBuilder();
            for (int i = 0; i < ole.Length; i++)
            {
                bool exeyes = true;
                //if (ole[i].ParameterName.Replace("@", "").ToLower() == "domain")
                //{
                //    str.Append(ole[i].ParameterName.Replace("@", "").ToLower().Replace("domain", "[domain]"));
                //    exeyes = false;
                //}
                //if (ole[i].ParameterName.Replace("@", "").ToLower() == "name")
                //{
                //    str.Append(ole[i].ParameterName.Replace("@", "").ToLower().Replace("name", "[name]"));
                //    exeyes = false;
                //}
                //if (ole[i].ParameterName.Replace("@", "").ToLower() == "memo")
                //{
                //    str.Append(ole[i].ParameterName.Replace("@", "").ToLower().Replace("memo", "[memo]"));
                //    exeyes = false;
                //}
                string tempStr = ole[i].ParameterName.Replace("@", "").ToLower();
                tempStr = "[" + tempStr + "]";
                str.Append(tempStr);
                exeyes = false;

                if (exeyes)
                {
                    str.Append(ole[i].ParameterName.Replace("@", ""));
                }
                if (i != ole.Length - 1)
                {
                    str.Append(",");
                }
            }
            string Tmpstr = str.ToString().ToLower();
            Tmpstr = Tmpstr.Replace("@", "");
            //Tmpstr = Tmpstr.Replace(",domain,", ",[domain],");
            //Tmpstr = Tmpstr.Replace(",name,", ",[name],");
            return Tmpstr;
        }
        /// <summary>
        /// 取得参数值排列
        /// code by arjun
        /// </summary>
        /// <param name="ole">OleDbParameter</param>
        /// <returns>参数值排列</returns>
        public static string getAParam(OleDbParameter[] ole)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < ole.Length; i++)
            {
                //str.Append("@");
                str.Append(ole[i].ParameterName);
                if (i != ole.Length - 1)
                {
                    str.Append(",");
                }
            }
            return str.ToString();
        }
        /// <summary>
        /// 取得修改参数和值排列
        /// code by arjun
        /// </summary>
        /// <param name="ole">OleDbParameter</param>
        /// <returns>参数=值,参数=值</returns>
        public static string getModifyParam(OleDbParameter[] ole)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < ole.Length; i++)
            {
                bool exeyes = true;
                //if (ole[i].ParameterName.Replace("@", "").ToLower() == "domain")
                //{
                //    str.Append(ole[i].ParameterName.Replace("@", "").ToLower().Replace("domain", "[domain]"));
                //    exeyes = false;
                //}
                //if (ole[i].ParameterName.Replace("@", "").ToLower() == "name")
                //{
                //    str.Append(ole[i].ParameterName.Replace("@", "").ToLower().Replace("name", "[name]"));
                //    exeyes = false;
                //}
                //if (ole[i].ParameterName.Replace("@", "").ToLower() == "money")
                //{
                //    str.Append(ole[i].ParameterName.Replace("@", "").ToLower().Replace("money", "[money]"));
                //    exeyes = false;
                //}
                //if (ole[i].ParameterName.Replace("@", "").ToLower() == "memo")
                //{
                //    str.Append(ole[i].ParameterName.Replace("@", "").ToLower().Replace("memo", "[memo]"));
                //    exeyes = false;
                //}
                string tempStr = ole[i].ParameterName.Replace("@", "").ToLower();
                tempStr = "[" + tempStr + "]";
                str.Append(tempStr);
                exeyes = false;
                if (exeyes)
                {
                    str.Append(ole[i].ParameterName.Replace("@", ""));
                }
                str.Append("=");
                //str.Append("@");
                str.Append(ole[i].ParameterName);
                if (i != ole.Length - 1)
                {
                    str.Append(",");
                }
            }
            return str.ToString();
        }
        /// <summary>
        /// 取得新OleDbParameter对象
        /// code by arjun
        /// </summary>
        /// <param name="ole">OleDbParameter对象数组</param>
        /// <param name="str">要取得的新参数的名称,以","分隔</param>
        /// <returns>OleDbParameter对象</returns>
        public static OleDbParameter[] getNewParam(OleDbParameter[] ole,string str)
        {
            if (str == null || str == "")
            {
                return ole;
            }
            string[] arrStr = str.Split(',');
            OleDbParameter[] param=new OleDbParameter[arrStr.Length];
            for (int i = 0; i < arrStr.Length; i++)
            {
                for (int j = 0; j < ole.Length; j++)
                {
                    if (ole[j].ParameterName.ToLower().Replace("@", "") == arrStr[i].ToLower())
                    {
                        param[i] = ole[j];
                    }
                }
            }
            return param;
        }
        /// <summary>
        /// 判断表名是否在数据库中存在
        /// code by arjun
        /// </summary>
        /// <param name="tabname">表名</param>
        /// <returns>返回bool值</returns>
        public static bool ExitTable(string tabname)
        {
            DataTable dt = null;
            //string sqlText = "SELECT [name] as tabName FROM MSysObjects WHERE (((type)=1   Or   (type)=6) AND ((Mid([name],1,4))<>'MSys')) ";
            string sqlText = "select * from fs_tables where tabName='"+tabname+"'";
            string Connstr = FoosunConnectionString;
            OleDbConnection conn = new OleDbConnection(Connstr);
            conn.Open();
            try
            {
                dt = DbHelper.ExecuteTable(CommandType.Text, sqlText, null);
            }
            catch
            {
                //throw e;
            }
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 取得所有表名称
        /// code by arjun
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable getTables()
        {
            DataTable dt = null;
            string sqlText = "select * from fs_tables";
            string Connstr = FoosunConnectionString;
            OleDbConnection conn = new OleDbConnection(Connstr);
            conn.Open();
            try
            {
                dt = DbHelper.ExecuteTable(CommandType.Text, sqlText, null);
            }
            catch
            {
                //throw e;
            }
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        /// <summary>
        /// 取得ＳＱＬ串中的参数
        /// code by arjun
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>参数串</returns>
        public static string getSqlParam(string sql)
        {
            if (sql == null || sql == "")
            {
                return "";
            }
            string pattern = @"@([a-zA-Z_])+";
            StringBuilder str = new StringBuilder();
            Regex r = new Regex(pattern, RegexOptions.Compiled);
            Match mymatch = r.Match(sql);
            while (mymatch.Success)
            {
                Match tempM = mymatch;
                if (mymatch.Value.Equals(tempM.Value))
                {
                    mymatch = mymatch.NextMatch();
                    continue;
                }
                str.Append(mymatch.Value.Replace("@",""));
                str.Append(",");
                mymatch=mymatch.NextMatch();
            }
            if (str == null||str.ToString()=="")
            {
                return "";
            }
            return str.ToString().Substring(0,str.ToString().Length-1);
        }
    }
}
