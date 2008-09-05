using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
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

            OleDbConnection Conn = new OleDbConnection(Database.FoosunConnectionString);
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
                OleDbParameter[] param = GetstyleClassParameters(sc);
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
            OleDbConnection Conn = new OleDbConnection(Database.FoosunConnectionString);
            Conn.Open();
            try
            {
                string checkSql = "";
                int recordCount = 0;
                checkSql = "select count(*) from " + Pre + "sys_styleclass Where ClassID<>'" + sc.ClassID + "' And Sname='" + sc.Sname + "' and isRecyle=0";
                recordCount = (int)DbHelper.ExecuteScalar(Conn, CommandType.Text, checkSql, null);
                if (recordCount > 0)
                {
                    throw new Exception("样式分类名称重复!");
                }
                string Sql = "Update " + Pre + "sys_styleclass Set Sname='" + sc.Sname + "'";
                Sql += " Where ClassID='" + sc.ClassID + "'";
                OleDbParameter[] param = GetstyleClassParameters(sc);
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
            OleDbConnection Conn = new OleDbConnection(Database.FoosunConnectionString);
            Conn.Open();
            OleDbTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_style = "Delete From " + Pre + "sys_styleclass Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_style, null);
                string str_styleClass = "Delete From " + Pre + "sys_LabelStyle Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_styleClass, null);
                tran.Commit();
                Conn.Close();
            }
            catch (OleDbException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void styleClassRDel(string id)
        {
            OleDbConnection Conn = new OleDbConnection(Database.FoosunConnectionString);
            Conn.Open();
            OleDbTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_style = "Update " + Pre + "sys_LabelStyle Set isRecyle=1 Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_style, null);
                string str_styleClass = "Update " + Pre + "sys_styleclass Set isRecyle=1 Where ClassID='" + id + "' And SiteID='" + SiteID + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_styleClass, null);
                tran.Commit();
                Conn.Close();
            }
            catch (OleDbException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public int styleAdd(Foosun.Model.StyleInfo sc)
        {
            int result = 0;
            OleDbConnection Conn = new OleDbConnection(Database.FoosunConnectionString);
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
                OleDbParameter[] param = GetstyleParameters(sc);
                string Sql = "insert into " + Pre + "sys_LabelStyle (";
                Sql += "styleID,"+Database.getParam(param)+",SiteID";
                Sql += ") values ('" + styleID + "',";
                Sql += Database.getAParam(param)+",'" + SiteID + "')";
               
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
            OleDbConnection Conn = new OleDbConnection(Database.FoosunConnectionString);
            Conn.Open();
            try
            {
                string checkSql = "";
                int recordCount = 0;
                checkSql = "select count(*) from " + Pre + "sys_LabelStyle Where styleID<>'" + sc.styleID + "' And styleName='" + sc.StyleName + "'";
                recordCount = (int)DbHelper.ExecuteScalar(Conn, CommandType.Text, checkSql, null);
                if (recordCount > 0)
                {
                    throw new Exception("样式名称重复,请重新修改!");
                }
                OleDbParameter[] param = GetstyleParameters(sc);
                string Sql = "Update " + Pre + "sys_LabelStyle Set "+Database.getModifyParam(param)+"";
                Sql += " Where styleID='" + sc.styleID + "'";
                
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
            OleDbParameter param = new OleDbParameter("@id", id);
            string str_sql = "Delete From " + Pre + "sys_LabelStyle Where styleID=@id And SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }
        public void styleRdel(string id)
        {
            OleDbParameter param = new OleDbParameter("@id", id);
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

        private OleDbParameter[] GetstyleParameters(Foosun.Model.StyleInfo sc)
        {
            OleDbParameter[] param = new OleDbParameter[6];
            param[0] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
            param[0].Value = sc.ClassID;
            param[1] = new OleDbParameter("@StyleName", OleDbType.VarWChar, 30);
            param[1].Value = sc.StyleName;
            param[2] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[2].Value = sc.Content;
            param[3] = new OleDbParameter("@Description", OleDbType.VarWChar, 200);
            param[3].Value = sc.Description;
            param[4] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            param[4].Value = sc.CreatTime;
            param[5] = new OleDbParameter("@isRecyle", OleDbType.Integer, 1);
            param[5].Value = sc.isRecyle;
            return param;
        }

        private OleDbParameter[] GetstyleClassParameters(Foosun.Model.StyleClassInfo sc)
        {
            OleDbParameter[] param = new OleDbParameter[3];
            param[0] = new OleDbParameter("@Sname", OleDbType.VarWChar, 30);
            param[0].Value = sc.Sname;
            param[1] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            param[1].Value = sc.CreatTime;
            param[2] = new OleDbParameter("@isRecyle", OleDbType.Integer, 1);
            param[2].Value = sc.isRecyle;
            return param;
        }


    }
}
