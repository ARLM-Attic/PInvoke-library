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
    public class AdminGroup : DbBase, IAdminGroup
    {
        private string SiteID;
        public AdminGroup()
        {
            SiteID = Foosun.Global.Current.SiteID;
        }
        /// <summary>
        /// 增加管理员组
        /// </summary>
        /// <param name="agci">构造参数</param>
        /// <returns></returns>
        public int add(Foosun.Model.AdminGroupInfo agci)
        {
            string checkSql = "";
            int recordCount = 0;
            string AdminGruopNum = Foosun.Common.Rand.Number(8);
            while (true)
            {
                checkSql = "select count(*) from " + Pre + "sys_AdminGroup where adminGroupNumber='" + AdminGruopNum + "'";
                recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
                if (recordCount < 1)
                    break;
                else
                    AdminGruopNum = Foosun.Common.Rand.Number(12, true);
            }
            checkSql = "select count(*) from " + Pre + "sys_AdminGroup where GroupName='" + agci.GroupName + "'";
            recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
            if (recordCount > 0)
            {
                throw new Exception("管理员组名称重复,请重新添加!");
            }
            string Sql = "insert into " + Pre + "sys_AdminGroup (";
            Sql += "adminGroupNumber,GroupName,ClassList,channelList,SpecialList,CreatTime,SiteID";
            Sql += ") values ('" + AdminGruopNum + "',";
            Sql += "@GroupName,@ClassList,@channelList,@SpecialList,@CreatTime,@SiteID)";
            SqlParameter[] param = GetAdminGroupParameters(agci);
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        public int Edit(Foosun.Model.AdminGroupInfo agci)
        {
            string str_Sql = "Update " + Pre + "sys_AdminGroup Set ClassList=@ClassList,SpecialList=@SpecialList,";
            str_Sql += "channelList=@channelList Where adminGroupNumber='" + agci.adminGroupNumber + "'";

            SqlParameter[] param = GetAdminGroupParameters(agci);
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, param));
        }

        public void Del(string id)
        {
            string str_Sql = "Delete From  " + Pre + "sys_AdminGroup where adminGroupNumber='" + id + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        public DataTable getInfo(string id)
        {
            string str_Sql = "Select adminGroupNumber,GroupName,ClassList,SpecialList,channelList From " + Pre + "sys_AdminGroup Where SiteID='" + SiteID + "' and adminGroupNumber='" + id + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public DataTable getClassList(string col, string TbName, string sqlselect)
        {
            string str_Sql = "Select " + col + " From " + Pre + TbName + " " + sqlselect + " Order By ID Asc";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        public DataTable getColCname(string colname, string TbName, string classid, string id)
        {
            string str_Sql = "Select " + colname + " From " + Pre + TbName + " Where " + classid + "='" + id + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        private SqlParameter[] GetAdminGroupParameters(Foosun.Model.AdminGroupInfo agci)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@adminGroupNumber", SqlDbType.NVarChar, 8);
            param[0].Value = agci.adminGroupNumber;
            param[1] = new SqlParameter("@GroupName", SqlDbType.NVarChar, 20);
            param[1].Value = agci.GroupName;
            param[2] = new SqlParameter("@ClassList", SqlDbType.NText);
            param[2].Value = agci.ClassList;
            param[3] = new SqlParameter("@SpecialList", SqlDbType.NText);
            param[3].Value = agci.SpecialList;

            param[4] = new SqlParameter("@channelList", SqlDbType.NText);
            param[4].Value = agci.channelList;
            param[5] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[5].Value = agci.CreatTime;
            param[6] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[6].Value = agci.SiteID;
            return param;
        }
    }
}
