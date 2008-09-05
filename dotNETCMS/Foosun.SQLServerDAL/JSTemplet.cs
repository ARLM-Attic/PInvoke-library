using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Global;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class JSTemplet : DbBase, IJSTemplet
    {
        public DataTable List()
        {
            string Sql = "select TempletID,CName,JSTType from " + Pre + "news_JSTemplet where SiteID='" + Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable ClassList()
        {
            string Sql = "select id,ClassID,CName,ParentID from " + Pre + "News_JST_Class where SiteID='" + Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable GetCustom()
        {
            string Sql = "select defineCname,defineValue From " + Pre + "define_data Where SiteID='" + Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable reviewTempletContent(string tid)
        {
            string Sql = "select JSTContent From " + Pre + "news_JSTemplet Where TempletID='" + tid + "' and SiteID='" + Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable GetPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, string ParentID)
        {
            RecordCount = 0;
            PageCount = 0;
            if (ParentID.IndexOf("'") >= 0) ParentID = ParentID.Replace("'", "");
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            try
            {
                string Sql = "select count(*) from " + Pre + "news_JST_Class where SiteID='" + Current.SiteID + "' and ParentID='" + ParentID + "'";
                int m = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null));
                Sql = "select count(*) from " + Pre + "news_JSTemplet where SiteID='" + Current.SiteID + "' and JSClassid='" + ParentID + "'";
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null));
                RecordCount = m + n;
                if (RecordCount % PageSize == 0)
                    PageCount = RecordCount / PageSize;
                else
                    PageCount = RecordCount / PageSize + 1;
                if (PageIndex > PageCount)
                    PageIndex = PageCount;
                if (PageIndex < 1)
                    PageIndex = 1;
                Sql = "(SELECT a.id, 255 AS JSTType, a.ClassID AS TmpID, a.CName, a.CreatTime, COUNT(b.id) AS NumCLS,(SELECT COUNT(*) FROM " + Pre + "news_JSTemplet WHERE JSClassid = a.ClassID) AS NumTMP FROM " + Pre + "news_JST_Class a LEFT OUTER JOIN " + Pre + "news_JST_Class b ON a.ClassID = b.ParentID where a.ParentID='" + ParentID + "' and a.SiteID='" + Current.SiteID + "' GROUP BY a.id, a.CName, a.CreatTime, a.ClassID) union ";
                Sql += "(select id,JSTType,TempletID as TmpID,CName,CreatTime,NumCLS=0,NumTMP=0 from " + Pre + "news_JSTemplet where JSClassid='" + ParentID + "' and SiteID='" + Current.SiteID + "')";
                SqlDataAdapter ap = new SqlDataAdapter(Sql, cn);
                DataSet st = new DataSet();
                ap.Fill(st, (PageIndex - 1) * PageSize, PageSize, "RESULT");
                return st.Tables[0];
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public void Add(string CName, string JSClassid, int JSTType, string JSTContent)
        {
            Edit(0, CName, JSClassid, JSTType, JSTContent);
        }
        public void Update(int id, string CName, string JSClassid, string JSTContent)
        {
            Edit(id, CName, JSClassid, -1, JSTContent);
        }
        private void Edit(int id, string CName, string JSClassid, int JSTType, string JSTContent)
        {
            string Sql = "select count(*) from " + Pre + "news_JSTemplet where SiteID='" + Current.SiteID + "' and CName=@CName";
            if (id > 0)
                Sql += " and id<>" + id;
            SqlParameter parm = new SqlParameter("@CName", SqlDbType.NVarChar, 50);
            parm.Value = CName;
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            try
            {
                cn.Open();
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, parm));
                if (n > 0)
                    throw new Exception("JS模型名称不能重复,该JS模型名称已存在!");
                if (id > 0)
                {
                    Sql = "update " + Pre + "news_JSTemplet set CName=@CName,JSClassid=@JSClassid,JSTContent=@JSTContent where SiteID=@SiteID and id=" + id;
                }
                else
                {
                    string JsTID = Foosun.Common.Rand.Str(12);
                    if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "news_JSTemplet where TempletID='" + JsTID + "'")) > 0)
                    {
                        JsTID = Foosun.Common.Rand.Str(12, true);
                    }
                    Sql = "insert into " + Pre + "news_JSTemplet (TempletID,CName,JSClassid,JSTType,JSTContent,CreatTime,SiteID) values ";
                    Sql += "('" + JsTID + "',@CName,@JSClassid," + JSTType + ",@JSTContent,'" + DateTime.Now + "',@SiteID)";
                }
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@CName", SqlDbType.NVarChar, 50);
                param[0].Value = CName;
                param[1] = new SqlParameter("@JSClassid", SqlDbType.NVarChar, 12);
                param[1].Value = JSClassid;
                param[2] = new SqlParameter("@JSTContent", SqlDbType.NText);
                param[2].Value = JSTContent;
                param[3] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
                param[3].Value = Current.SiteID;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public DataTable GetSingle(int id)
        {
            string Sql = "select * from " + Pre + "news_JSTemplet where Siteid='" + Current.SiteID + "' and id=" + id;
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable GetClass(int id)
        {
            string Sql = "SELECT * FROM " + Pre + "news_JST_Class where SiteID='" + Current.SiteID + "' and id=" + id;
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public void ClassAdd(string CName, string ParentID, string Description)
        {
            ClassEdit(0, CName, ParentID, Description);
        }
        public void ClassUpdate(int id, string CName, string ParentID, string Description)
        {
            ClassEdit(id, CName, ParentID, Description);
        }
        private void ClassEdit(int id, string CName, string ParentID, string Description)
        {
            string Sql = "select count(*) from " + Pre + "news_JST_Class where SiteID='" + Current.SiteID + "' and CName=@CName";
            if (id > 0)
                Sql += " and id<>" + id;
            SqlParameter parm = new SqlParameter("@CName", SqlDbType.NVarChar, 50);
            parm.Value = CName;
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            try
            {
                cn.Open();
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, parm));
                if (n > 0)
                    throw new Exception("JS模型分类名称不能重复,该分类名称已存在!");
                if (id > 0)
                {
                    Sql = "update " + Pre + "news_JST_Class set CName=@CName,ParentID=@ParentID,Description=@Description where SiteID=@SiteID and id=" + id;
                }
                else
                {
                    string CLID = Foosun.Common.Rand.Str(12);
                    if (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "news_JST_Class where ClassID='" + CLID + "'")) > 0)
                    {
                        CLID = Foosun.Common.Rand.Str(12, true);
                    }
                    Sql = "insert into " + Pre + "news_JST_Class (ClassID,CName,ParentID,Description,CreatTime,SiteID) values ";
                    Sql += "('" + CLID + "',@CName,@ParentID,@Description,'" + DateTime.Now + "',@SiteID)";
                }
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@CName", SqlDbType.NVarChar, 50);
                param[0].Value = CName;
                param[1] = new SqlParameter("@ParentID", SqlDbType.NVarChar, 12);
                param[1].Value = ParentID;
                param[2] = new SqlParameter("@Description", SqlDbType.NVarChar, 500);
                param[2].Value = Description.Equals("") ? DBNull.Value : (object)Description;
                param[3] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
                param[3].Value = Current.SiteID;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public void Delete(int id)
        {
            string Sql = "delete from " + Pre + "news_JSTemplet where SiteID='" + Current.SiteID + "' and id=" + id;
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public void ClassDelete(string id)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            try
            {
                if (id.IndexOf("'") >= 0) id = id.Replace("'", "''");
                IList<string> lstid = new List<string>();
                cn.Open();
                DataTable tb = DbHelper.ExecuteTable(cn, CommandType.Text, "select ClassID,ParentID from " + Pre + "news_JST_Class where SiteID='" + Current.SiteID + "'", null);
                FindChildren(tb, id, ref lstid);
                string ids = "'" + id + "'";
                foreach (string x in lstid)
                {
                    ids += ",'" + x + "'";
                }
                SqlTransaction tran = cn.BeginTransaction();
                try
                {
                    string Sql = "delete from " + Pre + "news_JSTemplet where SiteID='" + Current.SiteID + "' and JSClassid in (" + ids + ")";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    Sql = "delete from " + Pre + "news_JST_Class where SiteID='" + Current.SiteID + "' and ClassID in (" + ids + ")";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, Sql, null);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void FindChildren(DataTable tb, string PID, ref IList<string> list)
        {
            DataRow[] row = tb.Select("ParentID='" + PID + "'");
            if (row.Length < 1)
                return;
            else
            {
                foreach (DataRow r in row)
                {
                    list.Add(r["ClassID"].ToString());
                    FindChildren(tb, r["ClassID"].ToString(), ref list);
                }
            }
        }
    }
}
