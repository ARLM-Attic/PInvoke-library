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
    public class DynamicTrans : DbBase, IDynamicTrans
    {
        /// <summary>
        /// 得到新闻内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IDataReader GetNewsInfo(string ID, int Num, int ChID, string DTable)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            if (ChID != 0)
            {
                param[0] = new OleDbParameter("@ID", OleDbType.Integer, 4);
                param[0].Value = int.Parse(ID);
            }
            else
            {
                param[0] = new OleDbParameter("@ID", OleDbType.VarWChar, 12);
                param[0].Value = ID;
            }
            param[1] = new OleDbParameter("@ChID", OleDbType.Integer, 4);
            param[1].Value = ChID;
            string sql = string.Empty;
            if (ChID != 0)
            {
                if (Num == 0)
                {
                    sql = "select id,isDelPoint,Templet,Gpoint,iPoint,GroupNumber from " + DTable + " where ID=@ID and isLock=0";
                }
                else
                {
                    sql = "select id,isDelPoint,Gpoint,iPoint,GroupNumber from " + Pre + "sys_channelclass where ClassID=@ID and isLock=0";
                }
            }
            else
            {
                if (Num == 0)
                {
                    sql = "select NewsID,isDelPoint,Templet,Gpoint,iPoint,GroupNumber from " + Pre + "news where NewsID=@ID and isLock=0 and isRecyle=0 and NewsType<>2";
                }
                else
                {
                    sql = "select ClassID,isDelPoint,Gpoint,iPoint,GroupNumber from " + Pre + "news_Class where ClassID=@ID and isLock=0 and isRecyle=0 and isURL<>1";
                }
            }
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public IDataReader getUserInfo(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string sql = "select isAdmin,UserGroupNumber,iPoint,gPoint from " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public int UpdateHistory(int infoType, string infoID, int iPoint, int Gpoint, string UserNum,string IP)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@infoID", infoID), new OleDbParameter("@IP", IP), new OleDbParameter("@infoType", infoType) };
            string uSql = "insert into " + Pre + "user_note(infoType,infoID,logTime,IP,UserNum) values(@infoType,@infoID,'" + DateTime.Now + "',@IP,@UserNum)";
            DbHelper.ExecuteNonQuery(CommandType.Text, uSql, param);

            string Sql = "update " + Pre + "sys_User set iPoint=iPoint-" + iPoint + ",Gpoint=Gpoint-" + Gpoint + " where UserNum=@UserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public bool getUserNote(string UserNum, string infoID,int Num)
        {
            bool bltf = false;
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@infoID", infoID), new OleDbParameter("@infoType", Num) };
            string sql = "select count(id) from " + Pre + "user_note where UserNum=@UserNum and infoID=@infoID and infoType=@infoType";
            int i = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (i > 0)
            {
                bltf = true;
            }
            return bltf;
        }
    }
}
