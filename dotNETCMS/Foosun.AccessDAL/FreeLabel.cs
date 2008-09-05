using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Global;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class FreeLabel : DbBase, IFreeLabel
    {
        /// <summary>
        /// access 版本重写得到信息表函数
        /// code by arjun
        /// </summary>
        /// <returns>表信息</returns>
        public IList<FreeLablelDBInfo> GetTables()
        {
            OleDbConnection conn = new OleDbConnection(DBConfig.CmsConString);            
            try
            {
                conn.Open();
                DataTable dt = conn.GetSchema("Tables");
                IList<FreeLablelDBInfo> Tables = new List<FreeLablelDBInfo>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[3].ToString() == "TABLE")
                    {
                        string TabNm = dt.Rows[i][2].ToString();
                        if (!TabNm.ToLower().Equals("dtproperties"))
                            Tables.Add(new FreeLablelDBInfo(TabNm, TabNm, ""));
                    }
                }
                return Tables;
            }
            catch
            {
                return null;
            }
            finally 
            {
                conn.Close();
            }
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
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
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
        private bool IsRepeat(OleDbConnection cn, int id, string Name)
        {
            string Sql = "select count(*) from " + Pre + "sys_LabelFree where SiteID='" + Current.SiteID + "' and LabelName=@LabelName";
            if (id > 0)
                Sql += " and id<>" + id;
            OleDbParameter param = new OleDbParameter("@LabelName", OleDbType.VarWChar, 30);
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
            OleDbConnection cn = new OleDbConnection(Database.FoosunConnectionString);
            cn.Open();
            try
            {
                if (IsRepeat(cn, info.Id, info.LabelName))
                    return false;
                string Sql = "";
                OleDbParameter[] parm = new OleDbParameter[4];
                parm[0] = new OleDbParameter("@LabelName", OleDbType.VarWChar, 30);
                parm[0].Value = info.LabelName;
                parm[1] = new OleDbParameter("@LabelSQL", OleDbType.VarWChar, 4000);
                parm[1].Value = info.LabelSQL;
                parm[2] = new OleDbParameter("@StyleContent", OleDbType.VarWChar, 4000);
                parm[2].Value = info.StyleContent;
                parm[3] = new OleDbParameter("@Description", OleDbType.VarWChar, 200);
                parm[3].Value = info.Description.Equals("") ? DBNull.Value : (object)info.Description;
                if (info.Id < 1)
                {
                    string LblID = Foosun.Common.Rand.Number(12);
                    while (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "sys_LabelFree where LabelID='" + LblID + "'", null)) > 0)
                    {
                        LblID = Foosun.Common.Rand.Number(12, true);
                    }
                    Sql = "insert into " + Pre + "sys_LabelFree (LabelID,"+Database.getParam(parm)+",CreatTime,SiteID) values ('" + LblID + "',"+Database.getAParam(parm)+",'" + DateTime.Now + "','" + Current.SiteID + "')";
                }
                else
                {
                    Sql = "update " + Pre + "sys_LabelFree set "+Database.getModifyParam(parm)+" where SiteID='" + Current.SiteID + "' and Id=" + info.Id;
                }
                
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
