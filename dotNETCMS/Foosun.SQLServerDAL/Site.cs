using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Common;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Site : DbBase, ISite
    {
        private SqlTransaction Trans;
        public int Add(STSite site, string CurrentSiteID, out string SiteID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            cn.Open();
            try
            {
                SiteID = Rand.Number(12);
                while (true)
                {
                    string SqlRptID = "select count(*) from " + Pre + "News_site where ChannelID='" + SiteID + "'";
                    int RcdCount = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, SqlRptID, null);
                    if (RcdCount < 1)
                        break;
                    else
                        SiteID = Foosun.Common.Rand.Number(12, true);
                }
                string SqlRptEnm = "select count(*) from " + Pre + "News_site where EName='" + site.EName + "'";
                int n = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, SqlRptEnm, null);
                if (n > 0)
                {
                    throw new Exception("对不起,该英文名称已被别的频道所使用!");
                }
                string SqlRptCnm = "select count(*) from " + Pre + "News_site where CName='" + site.CName + "'";
                n = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, SqlRptCnm, null);
                if (n > 0)
                {
                    throw new Exception("对不起,该中文名称已被别的频道所使用!");
                }
                string Sql = "insert into " + Pre + "News_site (";
                Sql += "ChannelID,CName,EName,ParentID,ChannCName,isLock,IsURL,Urladdress,DataLib,IndexTemplet,ClassTemplet,ReadNewsTemplet,SpecialTemplet,Domain,";
                Sql += "isCheck,Keywords,Descript,ContrTF,ShowNaviTF,UpfileType,UpfileSize,NaviContent,NaviPicURL,SaveType,PicSavePath,SaveFileType,";
                Sql += "SaveDirPath,SaveDirRule,SaveFileRule,NaviPosition,IndexEXName,ClassEXName,NewsEXName,SpecialEXName,classRefeshNum,infoRefeshNum,";
                Sql += "DelNum,SpecialNum,isDelPoint,Gpoint,iPoint,GroupNumber,CreatTime,SiteID";
                Sql += ") values ('" + SiteID + "',";
                Sql += "@CName,@EName,'0',@ChannCName,@isLock,@IsURL,@Urladdress,@DataLib,@IndexTemplet,@ClassTemplet,@ReadNewsTemplet,@SpecialTemplet,@Domain,";
                Sql += "@isCheck,@Keywords,@Descript,@ContrTF,@ShowNaviTF,@UpfileType,@UpfileSize,@NaviContent,@NaviPicURL,@SaveType,@PicSavePath,@SaveFileType,";
                Sql += "@SaveDirPath,@SaveDirRule,@SaveFileRule,@NaviPosition,@IndexEXName,@ClassEXName,@NewsEXName,@SpecialEXName,@classRefeshNum,@infoRefeshNum,";
                Sql += "@DelNum,@SpecialNum,@isDelPoint,@Gpoint,@iPoint,@GroupNumber";
                Sql += ",'" + DateTime.Now + "','" + CurrentSiteID + "')";
                Sql += ";SELECT @@IDENTITY";
                SqlParameter[] param = GetParameters(site);
                int result = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
                return result;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public bool Delete(string id, out Exception e, out string[] DelFiles)
        {
            bool flag = false;
            DelFiles = null;
            e = null;
            id = "'" + id + "'";
            if (id.IndexOf(",") > 0)
                id = id.Replace(",", "','");
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            cn.Open();

            ArrayList FilePath = new ArrayList();
            FilePath.Clear();
            try
            {
                ArrayList NewsTable = this.GetNewsTables(cn);
                for (int i = 0; i < NewsTable.Count; i++)
                {
                    string SqlPath = "select SavePath,FileName,FileEXName from " + NewsTable[i].ToString() + " where SiteID in (" + id + ")";
                    IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, SqlPath, null);
                    while (rd.Read())
                    {
                        if (!rd.IsDBNull(0) && !rd.IsDBNull(1) && !rd.IsDBNull(2))
                        {
                            string filepath = rd.GetString(0) + rd.GetString(1) + rd.GetString(2);
                            FilePath.Add(filepath);
                        }
                    }
                    rd.Close();
                }
                DelFiles = (string[])FilePath.ToArray(typeof(string));
                Trans = cn.BeginTransaction();
                for (int i = 0; i < NewsTable.Count; i++)
                {
                    string SqlDelNews = "delete from " + NewsTable[i].ToString() + " where SiteID in (" + id + ")";
                    DbHelper.ExecuteNonQuery(Trans, CommandType.Text, SqlDelNews, null);
                }
                string SqlDelSpc = "delete from " + Pre + "News_special where SiteID in (" + id + ")";
                DbHelper.ExecuteNonQuery(Trans, CommandType.Text, SqlDelSpc, null);
                string SqlDelCls = "delete from " + Pre + "News_Class where SiteID in (" + id + ")";
                DbHelper.ExecuteNonQuery(Trans, CommandType.Text, SqlDelCls, null);
                string SqlDelSt = "delete from " + Pre + "News_site where ChannelID in (" + id + ")";
                DbHelper.ExecuteNonQuery(Trans, CommandType.Text, SqlDelSt, null);
                Trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                e = ex;
                flag = false;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return flag;
        }
        public void Update(int id, STSite site)
        {

            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            cn.Open();
            try
            {
                string SqlEnm = "select count(*) from " + Pre + "News_site where Id<>" + id + " and EName='" + site.EName + "'";
                int n = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, SqlEnm, null);
                if (n > 0)
                {
                    throw new Exception("对不起,该英文名称已被别的频道所使用");
                }
                string SqlCnm = "select count(*) from " + Pre + "News_site where Id<>" + id + " and CName='" + site.CName + "'";
                n = (int)DbHelper.ExecuteScalar(cn, CommandType.Text, SqlCnm, null);
                if (n > 0)
                {

                    throw new Exception("对不起,该中文名称已被别的频道所使用");
                }
                string Sql = "update " + Pre + "News_site set CName=@CName,ChannCName=@ChannCName,isLock=@isLock,IsURL=@IsURL,Urladdress=@Urladdress,";
                Sql += "DataLib=@DataLib,IndexTemplet=@IndexTemplet,ClassTemplet=@ClassTemplet,ReadNewsTemplet=@ReadNewsTemplet,SpecialTemplet=@SpecialTemplet,";
                Sql += "Domain=@Domain,isCheck=@isCheck,Keywords=@Keywords,Descript=@Descript,ContrTF=ContrTF,ShowNaviTF=@ShowNaviTF,";
                Sql += "UpfileType=@UpfileType,UpfileSize=@UpfileSize,NaviContent=@NaviContent,NaviPicURL=@NaviPicURL,SaveType=@SaveType,PicSavePath=@PicSavePath,";
                Sql += "SaveFileType=@SaveFileType,SaveDirPath=@SaveDirPath,SaveDirRule=@SaveDirRule,SaveFileRule=@SaveFileRule,NaviPosition=@NaviPosition,IndexEXName=@IndexEXName,";
                Sql += "ClassEXName=@ClassEXName,NewsEXName=@NewsEXName,SpecialEXName=@SpecialEXName,classRefeshNum=@classRefeshNum,infoRefeshNum=@infoRefeshNum,DelNum=@DelNum,";
                Sql += "SpecialNum=@SpecialNum,isDelPoint=@isDelPoint,Gpoint=@Gpoint,iPoint=@iPoint,GroupNumber=@GroupNumber where Id=" + id;
                SqlParameter[] param = GetParameters(site);
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
            }
            catch
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
                throw;
            }
        }
        public void Recyle(string id)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            cn.Open();
            ArrayList NewsTable = this.GetNewsTables(cn);
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                string SqlSite = "update " + Pre + "News_site set isRecyle=1 where ChannelID = '" + id + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, SqlSite, null);
                string SqlClass = "update " + Pre + "News_Class set isRecyle=1 where SiteID = '" + id + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, SqlClass, null);
                string SqlSpecial = "update " + Pre + "News_special set isRecyle=1 where SiteID = '" + id + "'";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, SqlSpecial, null);
                for (int i = 0; i < NewsTable.Count; i++)
                {
                    string SqlNews = "update " + NewsTable[i] + " set isRecyle=1 where SiteID = '" + id + "'";
                    DbHelper.ExecuteNonQuery(tran, CommandType.Text, SqlNews, null);
                }
                tran.Commit();
                cn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                cn.Close();
                throw e;
            }
        }
        public DataTable List(SiteType sttype)
        {
            string Sql = "select ChannelID,CName from " + Pre + "news_site";
            if (sttype == SiteType.External)
                Sql += " where IsURL=1";
            else if (sttype == SiteType.System)
                Sql += " where IsURL=0";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 得到站点列表
        /// </summary>
        /// <returns></returns>
        public IDataReader siteList()
        {
            string Sql = "select ChannelID,CName,ChannCName from " + Pre + "news_site where ParentID='0' and isURL=0 and isLock=0 and isRecyle=0 " + Foosun.Common.Public.getCHStr() + " order by id asc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }


        public DataTable GetSingle(int id)
        {
            string Sql = "select * from " + Pre + "News_site where Id=" + id;
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable GetSiteInfo(string ChannelID)
        {
            string Sql = "select * from " + Pre + "News_site where ChannelID='" + ChannelID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        private ArrayList GetNewsTables(SqlConnection connection)
        {
            ArrayList NewsTable = new ArrayList();
            NewsTable.Clear();
            IDataReader rd = DbHelper.ExecuteReader(connection, CommandType.Text, "select TableName from " + Pre + "sys_NewsIndex", null);
            while (rd.Read())
            {
                NewsTable.Add(rd.GetString(0));
            }
            rd.Close();
            return NewsTable;
        }
        private SqlParameter[] GetParameters(STSite st)
        {

            SqlParameter[] param = new SqlParameter[40];

            param[0] = new SqlParameter("@GroupNumber", SqlDbType.NText);
            param[0].Value = st.GroupNumber.Equals("") ? DBNull.Value : (object)st.GroupNumber;

            param[1] = new SqlParameter("@CName", SqlDbType.NVarChar, 50);
            param[1].Value = st.CName;
            param[2] = new SqlParameter("@EName", SqlDbType.NVarChar, 50);
            param[2].Value = st.EName;
            param[3] = new SqlParameter("@ChannCName", SqlDbType.NVarChar, 50);
            param[3].Value = st.ChannCName;
            param[4] = new SqlParameter("@isLock", SqlDbType.TinyInt);
            param[4].Value = st.isLock;
            param[5] = new SqlParameter("@IsURL", SqlDbType.TinyInt);
            param[5].Value = st.IsURL;
            param[6] = new SqlParameter("@Urladdress", SqlDbType.NVarChar, 200);
            param[6].Value = st.Urladdress.Equals("") ? DBNull.Value : (object)st.Urladdress;
            param[7] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 20);
            param[7].Value = st.DataLib;
            param[8] = new SqlParameter("@IndexTemplet", SqlDbType.NVarChar, 200);
            param[8].Value = st.IndexTemplet.Equals("") ? DBNull.Value : (object)st.IndexTemplet;
            param[9] = new SqlParameter("@ClassTemplet", SqlDbType.NVarChar, 200);
            param[9].Value = st.ClassTemplet.Equals("") ? DBNull.Value : (object)st.ClassTemplet;
            param[10] = new SqlParameter("@ReadNewsTemplet", SqlDbType.NVarChar, 200);
            param[10].Value = st.ReadNewsTemplet.Equals("") ? DBNull.Value : (object)st.ReadNewsTemplet;
            param[11] = new SqlParameter("@SpecialTemplet", SqlDbType.NVarChar, 200);
            param[11].Value = st.SpecialTemplet.Equals("") ? DBNull.Value : (object)st.SpecialTemplet;
            param[12] = new SqlParameter("@Domain", SqlDbType.NVarChar, 100);
            param[12].Value = st.Domain;
            param[13] = new SqlParameter("@isCheck", SqlDbType.TinyInt);
            param[13].Value = st.isCheck.Equals(-1) ? DBNull.Value : (object)st.isCheck;
            param[14] = new SqlParameter("@Keywords", SqlDbType.NVarChar, 100);
            param[14].Value = st.Keywords;
            param[15] = new SqlParameter("@Descript", SqlDbType.NVarChar, 200);
            param[15].Value = st.Descript;
            param[16] = new SqlParameter("@ContrTF", SqlDbType.TinyInt);
            param[16].Value = st.ContrTF;
            param[17] = new SqlParameter("@ShowNaviTF", SqlDbType.TinyInt);
            param[17].Value = st.ShowNaviTF.Equals("") ? DBNull.Value : (object)st.ShowNaviTF;
            param[18] = new SqlParameter("@UpfileType", SqlDbType.NVarChar, 150);
            param[18].Value = st.UpfileType.Equals("") ? DBNull.Value : (object)st.UpfileType;
            param[19] = new SqlParameter("@UpfileSize", SqlDbType.Int);
            param[19].Value = st.UpfileSize.Equals(-1) ? DBNull.Value : (object)st.UpfileSize;

            param[20] = new SqlParameter("@NaviContent", SqlDbType.NVarChar, 255);
            param[20].Value = st.NaviContent;
            param[21] = new SqlParameter("@NaviPicURL", SqlDbType.NVarChar, 200);
            param[21].Value = st.NaviPicURL;
            param[22] = new SqlParameter("@SaveType", SqlDbType.TinyInt);
            param[22].Value = st.SaveType.Equals(-1) ? DBNull.Value : (object)st.SaveType;
            param[23] = new SqlParameter("@PicSavePath", SqlDbType.NVarChar, 100);
            param[23].Value = st.PicSavePath.Equals("") ? DBNull.Value : (object)st.PicSavePath;
            param[24] = new SqlParameter("@SaveFileType", SqlDbType.TinyInt);
            param[24].Value = st.SaveFileType.Equals(-1) ? DBNull.Value : (object)st.SaveFileType;
            param[25] = new SqlParameter("@SaveDirPath", SqlDbType.NVarChar, 100);
            param[25].Value = st.SaveDirPath.Equals("") ? DBNull.Value : (object)st.SaveDirPath;
            param[26] = new SqlParameter("@SaveDirRule", SqlDbType.NVarChar, 200);
            param[26].Value = st.SaveDirRule.Equals("") ? DBNull.Value : (object)st.SaveDirRule;
            param[27] = new SqlParameter("@SaveFileRule", SqlDbType.NVarChar, 100);
            param[27].Value = st.SaveFileRule.Equals("") ? DBNull.Value : (object)st.SaveFileRule;
            param[28] = new SqlParameter("@NaviPosition", SqlDbType.NVarChar, 255);
            param[28].Value = st.NaviPosition.Equals("") ? DBNull.Value : (object)st.NaviPosition;
            param[29] = new SqlParameter("@IndexEXName", SqlDbType.NVarChar, 6);
            param[29].Value = st.IndexEXName.Equals("") ? DBNull.Value : (object)st.IndexEXName;
            param[30] = new SqlParameter("@ClassEXName", SqlDbType.NVarChar, 6);
            param[30].Value = st.ClassEXName.Equals("") ? DBNull.Value : (object)st.ClassEXName;
            param[31] = new SqlParameter("@NewsEXName", SqlDbType.NVarChar, 6);
            param[31].Value = st.NewsEXName.Equals("") ? DBNull.Value : (object)st.NewsEXName;
            param[32] = new SqlParameter("@SpecialEXName", SqlDbType.NVarChar, 6);
            param[32].Value = st.SpecialEXName.Equals("") ? DBNull.Value : (object)st.SpecialEXName;
            param[33] = new SqlParameter("@classRefeshNum", SqlDbType.Int);
            param[33].Value = st.classRefeshNum < 1 ? 800 : (object)st.classRefeshNum;
            param[34] = new SqlParameter("@infoRefeshNum", SqlDbType.Int);
            param[34].Value = st.infoRefeshNum < 1 ? 100 : (object)st.infoRefeshNum;
            param[35] = new SqlParameter("@DelNum", SqlDbType.Int);
            param[35].Value = st.DelNum < 1 ? 200 : (object)st.DelNum;
            param[36] = new SqlParameter("@SpecialNum", SqlDbType.Int);
            param[36].Value = st.SpecialNum < 1 ? 500 : (object)st.SpecialNum;
            param[37] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt);
            param[37].Value = st.isDelPoint;
            param[38] = new SqlParameter("@Gpoint", SqlDbType.Int);
            param[38].Value = st.Gpoint < 0 ? DBNull.Value : (object)st.Gpoint;
            param[39] = new SqlParameter("@iPoint", SqlDbType.Int);
            param[39].Value = st.iPoint < 0 ? DBNull.Value : (object)st.iPoint;
            return param;
        }

        public int getsiteClassCount(string siteid)
        {
            string sql = "select count(id) from " + Pre + "news_class where SiteID='" + siteid + "' and isRecyle=0 and islock=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, sql, null);
        }
    }
}
