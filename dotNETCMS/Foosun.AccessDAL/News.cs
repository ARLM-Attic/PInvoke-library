using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class News : DbBase, INews
    {
        public DataTable GetTables()
        {
            string Sql = "select TableName from " + Pre + "sys_NewsIndex";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        #region 归档新闻
        public DataTable CoverTabNews1(string SeleStr, string TableID_Sql, string boxs)
        {
            string Cover_SqlS = "Select " + SeleStr + "," + TableID_Sql + " From " + Pre + "old_News as a, " + Pre + "sys_NewsIndex as b where a.id=" + boxs + " and a.DataLib = b.TableName";
            return DbHelper.ExecuteTable(CommandType.Text, Cover_SqlS, null);
        }

        public int delPP(string boxs)
        {
            string His_Sql = "Delete From " + Pre + "old_News  where id in(" + boxs + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, His_Sql, null);
        }
        public int locks(string boxs)
        {
            string His_Sql = "Update " + Pre + "old_News Set isLock=1 where id in(" + boxs + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, His_Sql, null);

        }
        public int unlovkc(string boxs)
        {
            string His_Sql = "Update " + Pre + "old_News Set isLock=0 where id in(" + boxs + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, His_Sql, null);
        }
        public int delalpl()
        {
            string His_Sql = "Delete From " + Pre + "old_News";
            return DbHelper.ExecuteNonQuery(CommandType.Text, His_Sql, null);
        }
        #endregion

        /// <summary>
        /// 添加新闻点击
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        public int AddNewsClick(string NewsID)
        {
            OleDbParameter param = new OleDbParameter("@NewsID", NewsID);
            string Sql = "Update " + Pre + "news Set Click=Click+1 Where NewsID=@NewsID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);

            Sql = "Select Click From " + Pre + "news Where NewsID=@NewsID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 取得评论列表
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回数据表</returns>
        public DataTable getCommentList(string NewsID)
        {
            OleDbParameter param = new OleDbParameter("@NewsID", NewsID);
            string Sql = "Select Commid,Title,Content,UserNum,creatTime,IP,commtype,QID,id,GoodTitle From " + Pre + "api_commentary Where InfoID=@NewsID And isRecyle=0 And islock=0 Order By OrderID desc,creatTime Desc,id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }


        /// <summary>
        /// 添加评论信息
        /// </summary>
        /// <param name="ci">实体类</param>
        /// <returns>如果添加成功返回1</returns>
        public int AddComment(Foosun.Model.Comment ci)
        {
            OleDbParameter[] param = GetCommentParameters(ci);
            string Commid = Foosun.Common.Rand.Number(12);
            while (true)
            {
                string checkSql = "select count(ID) from " + Pre + "api_commentary where Commid='" + Commid + "'";
                int recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
                if (recordCount < 1)
                    break;
                else
                    Commid = Foosun.Common.Rand.Number(12, true);
            }

            string Sql = "Insert Into " + Pre + "api_commentary(Commid,InfoID,APIID,DataLib,Title,Content,creatTime,IP,QID,UserNum,isRecyle,islock,OrderID,GoodTitle,isCheck,SiteID,commtype) Values('" + Commid + "',@InfoID,@APIID,@DataLib,@Title,@Content,@creatTime,@IP,@QID,@UserNum,@isRecyle,@islock,@OrderID,@GoodTitle,@isCheck,@SiteID,@commtype)";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "InfoID,APIID,DataLib,Title,Content,creatTime,IP,QID,UserNum,isRecyle,islock,OrderID,GoodTitle,isCheck,SiteID,commtype")));
        }


        /// <summary>
        /// 获得构造参数
        /// </summary>
        /// <param name="ci"></param>
        /// <returns></returns>
        private OleDbParameter[] GetCommentParameters(Foosun.Model.Comment ci)
        {
            OleDbParameter[] param = new OleDbParameter[18];
            param[0] = new OleDbParameter("@Id", OleDbType.Integer, 4);
            param[0].Value = ci.Id;
            param[1] = new OleDbParameter("@Commid", OleDbType.VarWChar, 12);
            param[1].Value = ci.Commid;
            param[2] = new OleDbParameter("@InfoID", OleDbType.VarWChar, 12);
            param[2].Value = ci.InfoID;

            param[3] = new OleDbParameter("@APIID", OleDbType.VarWChar, 20);
            param[3].Value = ci.APIID;
            param[4] = new OleDbParameter("@DataLib", OleDbType.VarWChar, 20);
            param[4].Value = ci.DataLib;
            param[5] = new OleDbParameter("@Title", OleDbType.VarWChar, 200);
            param[5].Value = ci.Title;
            param[6] = new OleDbParameter("@Content", OleDbType.VarWChar, 200);
            param[6].Value = ci.Content;

            param[7] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[7].Value = ci.creatTime;
            param[8] = new OleDbParameter("@IP", OleDbType.VarWChar, 20);
            param[8].Value = ci.IP;
            param[9] = new OleDbParameter("@QID", OleDbType.VarWChar, 12);
            param[9].Value = ci.QID;
            param[10] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[10].Value = ci.UserNum;

            param[11] = new OleDbParameter("@isRecyle", OleDbType.Integer, 1);
            param[11].Value = ci.isRecyle;
            param[12] = new OleDbParameter("@islock", OleDbType.Integer, 1);
            param[12].Value = ci.islock;
            param[13] = new OleDbParameter("@OrderID", OleDbType.Integer, 1);
            param[13].Value = ci.OrderID;
            param[14] = new OleDbParameter("@GoodTitle", OleDbType.Integer, 1);
            param[14].Value = ci.GoodTitle;

            param[15] = new OleDbParameter("@isCheck", OleDbType.Integer, 1);
            param[15].Value = ci.isCheck;
            param[16] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[16].Value = ci.SiteID;

            param[17] = new OleDbParameter("@commtype", OleDbType.Integer, 1);
            param[17].Value = ci.commtype;
            return param;
        }

        /// <summary>
        /// 得到评论观点
        /// </summary>
        /// <param name="infoID"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int returnCommentGD(string infoID, int num)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@InfoID", OleDbType.VarWChar, 12);
            param[0].Value = infoID;
            param[1] = new OleDbParameter("@commtype", OleDbType.VarWChar, 4);
            param[1].Value = num;

            int perstr = 100;
            string sql = "select count(id) from " + Pre + "api_commentary where InfoID=@InfoID";
            int recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, sql, param);

            string sql1 = "select count(id) from " + Pre + "api_commentary where InfoID=@InfoID and commtype=@commtype";
            int recordCount1 = (int)DbHelper.ExecuteScalar(CommandType.Text, sql1, param);
            perstr = (recordCount1 * 100 / recordCount);
            return perstr;
        }


        /// <summary>
        /// 得到新闻的DIG数
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public int gettopnum(string NewsID, string getNum)
        {
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@NewsID", OleDbType.VarWChar, 12);
            param[0].Value = NewsID;

            int intnum = 0;
            if (getNum == "1")
            {
                string usql = "update " + Pre + "news set TopNum=TopNum+1 where NewsID=@NewsID";
                DbHelper.ExecuteNonQuery(CommandType.Text, usql, param);
            }
            string sql = "select TopNum from " + Pre + "news where NewsID=@NewsID";
            intnum = (int)DbHelper.ExecuteScalar(CommandType.Text, sql, param);
            return intnum;
        }

        /// <summary>
        /// 得到评论数
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="Todays"></param>
        /// <returns></returns>
        public string getCommCounts(string NewsID, string Todays)
        {
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@NewsID", OleDbType.VarWChar, 12);
            param[0].Value = NewsID;

            string whereSTR = "";
            if (Todays == "1")
            {
                whereSTR = "And DateDiff('d',[creatTime] ,Now()) = 0 ";
            }
            string Sql = "Select Count(ID) From [" + Pre + "api_commentary] Where [InfoID]=@NewsID " + whereSTR + " and islock=0";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 得到投票
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public DataTable getvote(string NewsID)
        {
            OleDbParameter[] param = new OleDbParameter[1];
            param[0] = new OleDbParameter("@NewsID", OleDbType.VarWChar, 12);
            param[0].Value = NewsID;
            string Sql = "Select NewsID,voteTitle,voteContent,isTimeOutTime,ismTF,isMember,creattime From [" + Pre + "news_vote] Where [NewsID]=@NewsID and DateDiff('d',[isTimeOutTime] ,Now()) > 0";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return dt;
        }

        public string getChannelTable(int ChID)
        {
            string TableStr = "#";
            //string TmpTable = string.Empty;
            //int GetTableRecord = 0;
            OleDbParameter param = new OleDbParameter("@ChID", ChID);
            string sql = "select DataLib from " + Pre + "sys_channel where ID=@ChID";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, param);
            if (dr.Read())
            {
                //TmpTable = dr["DataLib"].ToString();
                //string TableSQL = "select count(*) from sysobjects where id = object_id(N'[" + TmpTable + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
                //GetTableRecord = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, TableSQL, null));
                //if (GetTableRecord > 0)
                //{
                //    TableStr = TmpTable;
                //}
                //修改是否存在表 arjun

                if (Database.ExitTable(dr["DataLib"].ToString()))
                {
                    TableStr = dr["DataLib"].ToString();
                }
            }
            dr.Close();
            return TableStr;
        }

        public IDataReader getNewsInfo(string NewsID,int ChID)
        {
            string Sql = string.Empty;
            OleDbParameter[] param = new OleDbParameter[1];
            if (ChID != 0)
            {
                param[0] = new OleDbParameter("@NewsID", OleDbType.Integer, 4);
                param[0].Value = int.Parse(NewsID);
                Sql = "Select * From [" + getChannelTable(ChID) + "] Where [id]=@NewsID";
            }
            else
            {
                param[0] = new OleDbParameter("@NewsID", OleDbType.VarWChar, 12);
                param[0].Value = NewsID;
                Sql = "Select * From [" + Pre + "news] Where [NewsID]=@NewsID";
            }
            return DbHelper.ExecuteReader(CommandType.Text, Sql, param);
        }

        public IDataReader getClassInfo(string ClassID, int ChID)
        {
            string Sql = string.Empty;
            OleDbParameter[] param = new OleDbParameter[1];
            if (ChID != 0)
            {
                param[0] = new OleDbParameter("@ClassID", OleDbType.Integer, 4);
                param[0].Value = int.Parse(ClassID);
                Sql = "Select id,SavePath,FileName From [" + Pre + "chanelclass] Where [id]=@ClassID";
            }
            else
            {
                param[0] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
                param[0].Value = ClassID;
                Sql = "Select ClassID,SavePath,SaveClassframe From [" + Pre + "news_class] Where [ClassID]=@ClassID";
            }
            return DbHelper.ExecuteReader(CommandType.Text, Sql, param);
        }

    }
}