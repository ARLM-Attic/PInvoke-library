using System;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class FsLog : DbBase, IFsLog
    {

        public int Add(int IsManager, string Title, string Content, string IP, string UserNum, string SiteID)
        {
            string SQL = "Insert Into " + Pre + "sys_logs(title,content,creatTime,IP,usernum,SiteID,ismanage) Values(@title,@content,@creatTime,@IP,@usernum,@SiteID,@ismanage)";
            SqlParameter[] parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@title", SqlDbType.NVarChar, 50);
            parm[0].Value = Title;
            parm[1] = new SqlParameter("@content", SqlDbType.NText);
            parm[1].Value = Content;
            parm[2] = new SqlParameter("@creatTime", SqlDbType.DateTime);
            parm[2].Value = DateTime.Now;
            parm[3] = new SqlParameter("@IP", SqlDbType.NVarChar, 16);
            parm[3].Value = IP;
            parm[4] = new SqlParameter("@usernum", SqlDbType.NVarChar, 15);
            parm[4].Value = UserNum;
            parm[5] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            parm[5].Value = SiteID;
            parm[6] = new SqlParameter("@ismanage", SqlDbType.NVarChar, 50);
            parm[6].Value = IsManager;
            return DbHelper.ExecuteNonQuery(CommandType.Text, SQL, parm);
        }
    }
}
