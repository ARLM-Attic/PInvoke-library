using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class FsLog : DbBase, IFsLog
    {

        public int Add(int IsManager, string Title, string Content, string IP, string UserNum, string SiteID)
        {
            OleDbParameter[] parm = new OleDbParameter[7];
            parm[0] = new OleDbParameter("@title", OleDbType.VarWChar, 50);
            parm[0].Value = Title;
            parm[1] = new OleDbParameter("@content", OleDbType.VarWChar);
            parm[1].Value = Content;
            parm[2] = new OleDbParameter("@creatTime", OleDbType.Date);
            parm[2].Value = DateTime.Now;
            parm[3] = new OleDbParameter("@IP", OleDbType.VarWChar, 16);
            parm[3].Value = IP;
            parm[4] = new OleDbParameter("@usernum", OleDbType.VarWChar, 15);
            parm[4].Value = UserNum;
            parm[5] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            parm[5].Value = SiteID;
            parm[6] = new OleDbParameter("@ismanage", OleDbType.VarWChar, 50);
            parm[6].Value = IsManager; 
            string SQL = "Insert Into " + Pre + "sys_logs("+Database.getParam(parm)+") Values("+Database.getAParam(parm)+")";
            
            return DbHelper.ExecuteNonQuery(CommandType.Text, SQL, parm);
        }
    }
}
