using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Global;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class FreeLabel : DbBase, IFreeLabel
    {
        public IList<FreeLablelDBInfo> GetTables()
        {
            IList<FreeLablelDBInfo> Tables = new List<FreeLablelDBInfo>();
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            cn.Open();
            DataTable dt = cn.GetSchema("Tables");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[3].ToString() == "BASE TABLE")
                {
                    string TabNm = dt.Rows[i][2].ToString();
                    if (!TabNm.ToLower().Equals("dtproperties"))
                        Tables.Add(new FreeLablelDBInfo(TabNm, TabNm, ""));
                }
            }
            cn.Close();
            return Tables;
        }
        public IList<FreeLablelDBInfo> GetFields(string TableName)
        {
            IList<FreeLablelDBInfo> Fields = new List<FreeLablelDBInfo>();
            string Sql = "select top 1 * from " + TableName + " where 1=0";
            IDataReader rd = DbHelper.ExecuteReader(DBConfig.CmsConString, CommandType.Text, Sql, null);
            for (int i = 0; i < rd.FieldCount; i++)
            {
                string fdnm = rd.GetName(i);
                Fields.Add(new FreeLablelDBInfo(fdnm, fdnm, rd.GetDataTypeName(i)));
            }
            if (rd.IsClosed == false)
                rd.Close();
            return Fields;
        }
        public bool IsNameRepeat(int id, string Name)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            cn.Open();
            try
            {
                bool flag = IsRepeat(cn, id, Name);
                return flag;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        private bool IsRepeat(SqlConnection cn, int id, string Name)
        {
            string Sql = "select count(*) from " + Pre + "sys_LabelFree where SiteID='" + Current.SiteID + "' and LabelName=@LabelName";
            if (id > 0)
                Sql += " and id<>" + id;
            SqlParameter param = new SqlParameter("@LabelName", SqlDbType.NVarChar, 30);
            param.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
            if (n > 0)
                return true;
            else
                return false;

        }
        public bool Add(FreeLabelInfo info)
        {
            return Edit(info);
        }
        public bool Update(FreeLabelInfo info)
        {
            return Edit(info);
        }
        private bool Edit(FreeLabelInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            cn.Open();
            try
            {
                if (IsRepeat(cn, info.Id, info.LabelName))
                    return false;
                string Sql = "";
                if (info.Id < 1)
                {
                    string LblID = Foosun.Common.Rand.Number(12);
                    while (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "sys_LabelFree where LabelID='" + LblID + "'", null)) > 0)
                    {
                        LblID = Foosun.Common.Rand.Number(12, true);
                    }
                    Sql = "insert into " + Pre + "sys_LabelFree (LabelID,LabelName,LabelSQL,StyleContent,Description,CreatTime,SiteID) values ('" + LblID + "',@LabelName,@LabelSQL,@StyleContent,@Description,'" + DateTime.Now + "','" + Current.SiteID + "')";
                }
                else
                {
                    Sql = "update " + Pre + "sys_LabelFree set LabelName=@LabelName,LabelSQL=@LabelSQL,StyleContent=@StyleContent,Description=@Description where SiteID='" + Current.SiteID + "' and Id=" + info.Id;
                }
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@LabelName", SqlDbType.NVarChar, 30);
                parm[0].Value = info.LabelName;
                parm[1] = new SqlParameter("@LabelSQL", SqlDbType.NVarChar, 4000);
                parm[1].Value = info.LabelSQL;
                parm[2] = new SqlParameter("@StyleContent", SqlDbType.NVarChar, 4000);
                parm[2].Value = info.StyleContent;
                parm[3] = new SqlParameter("@Description", SqlDbType.NVarChar, 200);
                parm[3].Value = info.Description.Equals("") ? DBNull.Value : (object)info.Description;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, parm);
                return true;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public FreeLabelInfo GetSingle(int id)
        {
            string Sql = "select LabelName,LabelSQL,StyleContent,Description,CreatTime,SiteID from " + Pre + "sys_LabelFree where SiteID='" + Current.SiteID + "' and Id=" + id;
            IDataReader rd = DbHelper.ExecuteReader(DBConfig.CmsConString, CommandType.Text, Sql, null);
            if (rd.Read())
            {
                string desc = "";
                if (!rd.IsDBNull(3)) desc = rd.GetString(3);
                FreeLabelInfo info = new FreeLabelInfo(id, rd.GetString(0), rd.GetString(1), rd.GetString(2), desc);
                rd.Close();
                return info;
            }
            else
            {
                rd.Close();
                throw new Exception("没有找到相关的自由标签记录!");
            }
        }
        public bool Delete(int id)
        {
            string Sql = "delete from " + Pre + "sys_LabelFree where SiteID='" + Current.SiteID + "' and Id=" + id;
            int n = DbHelper.ExecuteNonQuery(DBConfig.CmsConString, CommandType.Text, Sql, null);
            if (n > 0)
                return true;
            else
                return false;
        }
        public DataTable TestSQL(string Sql)
        {
            DataTable tb = DbHelper.ExecuteTable(DBConfig.CmsConString, CommandType.Text, Sql, null);
            return tb;
        }
    }
}
