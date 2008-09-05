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
    public class Style : DbBase, IStyle
    {
        private string SiteID;
        public Style()
        {
            SiteID = Foosun.Global.Current.SiteID;
        }
        public int sytleClassAdd(Foosun.Model.StyleClassInfo sc)
        {
            int result = 0;

            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            try
            {
                string checkSql = "";
                int recordCount = 0;
                string ClassID = Foosun.Common.Rand.Number(12);
                while (true)
                {
                    checkSql = "select count(*) from " + Pre + "sys_styleclass where ClassID='" + ClassID + "'";
                    recordCount = (int)DbHelper.ExecuteScalar(Conn, CommandType.Text, checkSql, null);
                    if (recordCount < 1)
                        break;
                    else
                        ClassID = Foosun.Common.Rand.Number(12, true);
                }
                checkSql = "select count(*) from " + Pre + "sys_styleclass where Sname='" + sc.Sname + "'";
                recordCount = (int)DbHelper.ExecuteScalar(Conn, CommandType.Text, checkSql, null);
                if (recordCount > 0)
                {
                    throw new Exception("样式分类名称重复,如果不存在，回收站中也可能存在此样式分类!");
                }
                string Sql = "insert into " + Pre + "sys_styleclass (";
                Sql += "ClassID,Sname,CreatTime,SiteID,isRecyle";
                Sql += ") values ('" + ClassID + "',";
                Sql += "@Sname,@CreatTime,'" + SiteID + "',@isRecyle)";
                SqlParameter[] param = GetstyleClassParameters(sc);
                result = Convert.ToInt32(DbHelper.ExecuteNonQuery(Conn, CommandType.Text, Sql, param));
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return result;
        }

        public int styleNametf(string CName)
        {
            string checkSql = "select count(*) from " + Pre + "sys_LabelStyle Where StyleName='" + CName + "' and isRecyle=0";
            int recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
            return recordCount;
        }
        public int styleClassEdit(Foosun.Model.StyleClassInfo sc)
        {
            int result = 0;
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            try
            {
                string checkSql = "";
                int recordCount = 0;
                checkSql = "select count(*) from " + Pre + "sys_styleclass Where ClassID!='" + sc.ClassID + "' And Sname='" + sc.Sname + "' and isRecyle=0";
                recordCount = (int)DbHelper.ExecuteScalar(Conn, CommandType.Text, checkSql, null);
                if (recordCount > 0)
                {
                    throw new Exception("样式分类名称重复!");
                }
                string Sql = "Update " + Pre + "sys_styleclass Set Sname='" + sc.Sname + "'";
                Sql += " Where ClassID='" + sc.ClassID + "'";
                SqlParameter[] param = GetstyleClassParameters(sc);
                result = Convert.ToInt32(DbHelper.ExecuteNonQuery(Conn, CommandType.Text, Sql, param));
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return result;
        }
        public void styleClassDel(string id)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_style = "Delete From " + Pre + "sys_styleclass Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_style, null);
                string str_styleClass = "Delete From " + Pre + "sys_LabelStyle Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_styleClass, null);
                tran.Commit();
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void styleClassRDel(string id)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_style = "Update " + Pre + "sys_LabelStyle Set isRecyle=1 Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_style, null);
                string str_styleClass = "Update " + Pre + "sys_styleclass Set isRecyle=1 Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_styleClass, null);
                tran.Commit();
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public int styleAdd(Foosun.Model.StyleInfo sc)
        {
            int result = 0;
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            try
            {
                string checkSql = "";
                int recordCount = 0;
                string styleID = Foosun.Common.Rand.Number(12);
                while (true)
                {
                    checkSql = "select count(*) from " + Pre + "sys_LabelStyle where styleID='" + styleID + "'";
                    recordCount = (int)DbHelper.ExecuteScalar(Conn, CommandType.Text, checkSql, null);
                    if (recordCount < 1)
                        break;
                    else
                        styleID = Foosun.Common.Rand.Number(12, true);
                }
                checkSql = "select count(*) from " + Pre + "sys_LabelStyle where StyleName='" + sc.StyleName + "' and isRecyle=0";
                recordCount = (int)DbHelper.ExecuteScalar(Conn, CommandType.Text, checkSql, null);
                if (recordCount > 0)
                {
                    throw new Exception("样式名称重复!");
                }
                string Sql = "insert into " + Pre + "sys_LabelStyle (";
                Sql += "styleID,ClassID,StyleName,Content,Description,CreatTime,isRecyle,SiteID";
                Sql += ") values ('" + styleID + "',";
                Sql += "@ClassID,@StyleName,@Content,@Description,@CreatTime,@isRecyle,'" + SiteID + "')";
                SqlParameter[] param = GetstyleParameters(sc);
                result = Convert.ToInt32(DbHelper.ExecuteNonQuery(Conn, CommandType.Text, Sql, param));
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return result;
        }
        public int styleEdit(Foosun.Model.StyleInfo sc)
        {
            int result = 0;
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            try
            {
                string checkSql = "";
                int recordCount = 0;
                checkSql = "select count(*) from " + Pre + "sys_LabelStyle Where styleID!='" + sc.styleID + "' And styleName='" + sc.StyleName + "'";
                recordCount = (int)DbHelper.ExecuteScalar(Conn, CommandType.Text, checkSql, null);
                if (recordCount > 0)
                {
                    throw new Exception("样式名称重复,请重新修改!");
                }
                string Sql = "Update " + Pre + "sys_LabelStyle Set ClassID=@ClassID,StyleName=@StyleName,Content=@Content,Description=@Description";
                Sql += " Where styleID='" + sc.styleID + "'";
                SqlParameter[] param = GetstyleParameters(sc);
                result = Convert.ToInt32(DbHelper.ExecuteNonQuery(Conn, CommandType.Text, Sql, param));
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return result;
        }
        public void styleDel(string id)
        {
            SqlParameter param = new SqlParameter("@id", id);
            string str_sql = "Delete From " + Pre + "sys_LabelStyle Where styleID=@id And SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }
        public void styleRdel(string id)
        {
            SqlParameter param = new SqlParameter("@id", id);
            string str_sql = "Update " + Pre + "sys_LabelStyle Set isRecyle=1 Where ID=@id And SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }
        public DataTable getstyleClassInfo(string id)
        {
            string str_Sql = "Select Sname From " + Pre + "sys_styleclass Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }
        public DataTable getstyleInfo(string id)
        {
            string str_Sql = "Select ClassID,StyleName,Content,Description From " + Pre + "sys_LabelStyle Where SiteID='" + SiteID + "' And styleID='" + id + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }
        public DataTable styledefine()
        {
            string str_Sql = "Select defineCname,defineColumns From " + Pre + "define_data Where SiteID='" + SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }
        public DataTable styleClassList()
        {
            string str_Sql = "Select ClassID,Sname From " + Pre + "sys_styleclass Where SiteID='" + SiteID + "' And isRecyle=0";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }

        private SqlParameter[] GetstyleParameters(Foosun.Model.StyleInfo sc)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
            param[0].Value = sc.ClassID;
            param[1] = new SqlParameter("@StyleName", SqlDbType.NVarChar, 30);
            param[1].Value = sc.StyleName;
            param[2] = new SqlParameter("@Content", SqlDbType.NText);
            param[2].Value = sc.Content;
            param[3] = new SqlParameter("@Description", SqlDbType.NVarChar, 200);
            param[3].Value = sc.Description;
            param[4] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[4].Value = sc.CreatTime;
            param[5] = new SqlParameter("@isRecyle", SqlDbType.TinyInt, 1);
            param[5].Value = sc.isRecyle;
            return param;
        }

        private SqlParameter[] GetstyleClassParameters(Foosun.Model.StyleClassInfo sc)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Sname", SqlDbType.NVarChar, 30);
            param[0].Value = sc.Sname;
            param[1] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[1].Value = sc.CreatTime;
            param[2] = new SqlParameter("@isRecyle", SqlDbType.TinyInt, 1);
            param[2].Value = sc.isRecyle;
            return param;
        }


    }
}
