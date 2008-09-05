using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Collections;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Global;
using Foosun.DALProfile;
using Foosun.Config;
using Foosun.Control;

namespace Foosun.SQLServerDAL
{
    public class Collect : DbBase, ICollect
    {
        public DataTable GetFolderSitePage(int FolderID, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {

            if (FolderID < 1)
            {

                SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
                cn.Open();
                int nf = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "'", null));
                int ns = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and (Folder is null or Folder=0)", null));
                RecordCount = nf + ns;
                if (RecordCount % PageSize == 0)
                    PageCount = RecordCount / PageSize;
                else
                    PageCount = RecordCount / PageSize + 1;
                if (PageIndex > PageCount)
                    PageIndex = PageCount;
                if (PageIndex < 1)
                    PageIndex = 1;
                int nStart = PageSize * (PageIndex - 1);
                string Sql = "(select 0 as TP,ID,SiteFolder as SName,'' as objURL,'' as LockState from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "') union (select 1 as TP,ID,SiteName as SName,objURL,case when LinkSetting is null or PagebodySetting is null or  PageTitleSetting is null then '无效' else '有效' end as LockState from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and (Folder is null or Folder=0))";
                SqlDataAdapter ap = new SqlDataAdapter(Sql, cn);
                DataSet st = new DataSet();
                ap.Fill(st, nStart, PageSize, "REST");
                DataTable tb = st.Tables[0];
                st.Dispose();
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                return tb;
            }
            else
            {
                return DbHelper.ExecutePage(DBConfig.CollectConString, "1 as TP,ID,SiteName as SName,objURL,case when LinkSetting is null or PagebodySetting is null or  PageTitleSetting is null then '无效' else '有效' end as LockState", Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and Folder=" + FolderID, "ID", "Order by ID", PageIndex, PageSize, out RecordCount, out PageCount, null);
            }
        }
        public void SiteCopy(int id)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            try
            {
                string Sql = "select * from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and ID=" + id;
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, null);
                string snm = "", Column = "";
                if (rd.Read())
                {
                    snm = rd["SiteName"].ToString();
                    for (int i = 0; i < rd.FieldCount; i++)
                    {
                        string clnm = rd.GetName(i);
                        if (!(clnm.Equals("ID") || clnm.Equals("SiteName")))
                        {
                            Column += "," + clnm;
                        }
                    }
                }
                else
                {
                    rd.Close();
                    throw new Exception("0%没有找到该记录");
                }
                rd.Close();
                string snewnm = "复件 " + snm;
                int n = 2;
                while (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "Collect_Site where SiteName='" + snewnm + "'", null)) > 0)
                {
                    snewnm = "复件(" + n + ") " + snm;
                    n++;
                }
                Sql = "insert into " + Pre + "Collect_Site (SiteName" + Column + ") select '" + snewnm + "' as NewName" + Column + " from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and ID=" + id;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, null);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public void FolderCopy(int id)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            try
            {
                string Sql = "select SiteFolder from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "' and ID=" + id;
                object snm = DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null);
                if (snm == null)
                    throw new Exception("没有找到相关目录的记录");
                int n = 2;
                string snewnm = "复件 " + snm;
                /*
                if (Regex.Match(snm,@"^复件\s").Success)
                {
                    snewnm = "复件(" + n + ")" + snm;
                    n++;
                }
                else if (Regex.Match(snm, @"^复件(/d)").Success)
                {
                    n = int.Parse(Regex.Match(snm, @"(?<=复件\()\d+?(?=\).+)").Value);
                    n++;
                    snewnm = "复件(" + n + ")" + snm;
                    n++;
                }
                else
                {
                    snewnm = "复件 " + snm;
                }
                 */
                while (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "Collect_SiteFolder where SiteFolder='" + snewnm + "'", null)) > 0)
                {
                    snewnm = "复件(" + n + ") " + snm;
                    n++;
                }
                Sql = "insert into " + Pre + "Collect_SiteFolder (SiteFolder,SiteFolderDetail,ChannelID) select '" + snewnm + "' as NewName,SiteFolderDetail,ChannelID from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "' and ID=" + id;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, null);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public void FolderDelete(int id)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            try
            {
                string Sql = "select count(*) from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and Folder=" + id;
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null));
                if (n > 0)
                    throw new Exception("该栏目下有站点,不能删除!");
                Sql = "Delete from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "' and ID=" + id;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, null);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public void SiteDelete(int id)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                string[] Sql = new string[2];
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, "Delete from " + Pre + "Collect_RuleApply where SiteID=" + id, null);
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, "Delete from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and ID=" + id, null);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public DataTable GetFolder(int id, bool all)
        {
            string Sql = "select ID,SiteFolder,SiteFolderDetail from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "'";
            if (!all)
                Sql += " and ID=" + id;
            return DbHelper.ExecuteTable(DBConfig.CollectConString, CommandType.Text, Sql, null);
        }
        public DataTable GetSite(int id)
        {
            string Sql = "select a.*,b.OldContent,b.ReContent,b.IgnoreCase from " + Pre + "Collect_Site a left join (" + Pre + "Collect_Rule b inner join " + Pre + "Collect_RuleApply c on b.ID=c.RuleID) on c.SiteID=a.ID where a.ChannelID='" + Current.SiteID + "' and a.ID=" + id;
            return DbHelper.ExecuteTable(DBConfig.CollectConString, CommandType.Text, Sql, null);
        }
        public int SiteAdd(CollectSiteInfo st)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            try
            {
                string Sql = "select count(*) from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and SiteName=@SiteName";
                SqlParameter parm = new SqlParameter("@SiteName", SqlDbType.NVarChar, 50);
                parm.Value = st.SiteName;
                int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, parm));
                if (n > 0)
                {
                    cn.Close();
                    return 0;
                    //throw new Exception("采集站点名称重复!");
                }
                Sql = "insert into " + Pre + "Collect_Site (";
                Sql += "SiteName,objURL,Folder,SaveRemotePic,Audit,IsReverse,IsAutoPicNews,TextTF";
                Sql += ",IsStyle,IsDIV,IsA,IsClass,IsFont,IsSpan,IsObject,IsIFrame,IsScript,Encode,ClassID,ChannelID) values (";
                Sql += "@SiteName,@objURL,@Folder,@SaveRemotePic,@Audit,@IsReverse,@IsAutoPicNews,@TextTF";
                Sql += ",@IsStyle,@IsDIV,@IsA,@IsClass,@IsFont,@IsSpan,@IsObject,@IsIFrame,@IsScript,@Encode,@ClassID,'" + Current.SiteID + "')";
                Sql += ";SELECT @@IDENTITY";
                int result = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, GetParameters(st)));
                return result;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public int FolderAdd(string Name, string Description)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            string Sql = "select count(*) from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "' and SiteFolder=@SiteFolder";
            SqlParameter prm = new SqlParameter("@SiteFolder", SqlDbType.NVarChar, 50);
            prm.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, prm));
            if (n > 0)
            {
                cn.Close();
                throw new Exception("栏目名称重复");
            }
            Sql = "insert into " + Pre + "Collect_SiteFolder (SiteFolder,SiteFolderDetail,ChannelID) values (@SiteFolder,@SiteFolderDetail,'" + Current.SiteID + "');SELECT @@IDENTITY";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SiteFolder", SqlDbType.NVarChar, 50);
            param[0].Value = Name;
            param[1] = new SqlParameter("@SiteFolderDetail", SqlDbType.NText);
            param[1].Value = Description.Trim().Equals("") ? DBNull.Value : (object)Description;
            n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
            cn.Close();
            return n;
        }
        public void SiteUpdate(CollectSiteInfo st, int step)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            try
            {
                SqlParameter[] param = null;
                string Sql = "";
                if (step == 1)
                {
                    #region 第一步
                    Sql = "select count(*) from " + Pre + "Collect_Site where SiteName=@SiteName and ChannelID='" + Current.SiteID + "' and ID<>@ID";
                    SqlParameter[] parm = new SqlParameter[2];
                    parm[0] = new SqlParameter("@SiteName", SqlDbType.NVarChar, 50);
                    parm[0].Value = st.SiteName;
                    parm[1] = new SqlParameter("@ID", SqlDbType.Int);
                    parm[1].Value = st.ID;
                    int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, parm));
                    if (n > 0)
                    {
                        cn.Close();
                        throw new Exception("采集站点名称重复!");
                    }
                    Sql = "update " + Pre + "Collect_Site Set SiteName=@SiteName,objURL=@objURL,Folder=@Folder,ClassID=@ClassID,";
                    Sql += "SaveRemotePic=@SaveRemotePic,Audit=@Audit,IsReverse=@IsReverse,IsAutoPicNews=@IsAutoPicNews,TextTF=@TextTF,";
                    Sql += "IsStyle=@IsStyle,IsDIV=@IsDIV,IsA=@IsA,IsClass=@IsClass,IsFont=@IsFont,IsSpan=@IsSpan,";
                    Sql += "IsObject=@IsObject,IsIFrame=@IsIFrame,IsScript=@IsScript,Encode=@Encode where ChannelID='" + Current.SiteID + "' and ID=" + st.ID;
                    param = GetParameters(st);
                    #endregion 第一步
                }
                else if (step == 2)
                {
                    #region 第二步
                    Sql = "update " + Pre + "Collect_Site set ListSetting=@ListSetting,OtherPageSetting=@OtherPageSetting,";
                    Sql += "StartPageNum=@StartPageNum,EndPageNum=@EndPageNum,OtherType=@OtherType where ChannelID='" + Current.SiteID + "' and ID=" + st.ID;
                    param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ListSetting", SqlDbType.NVarChar, 4000);
                    param[0].Value = st.ListSetting;
                    param[1] = new SqlParameter("@OtherPageSetting", SqlDbType.NVarChar, 4000);
                    param[1].Value = st.OtherPageSetting.Trim().Equals("") ? DBNull.Value : (object)st.OtherPageSetting;
                    param[2] = new SqlParameter("@StartPageNum", SqlDbType.Int);
                    param[2].Value = st.StartPageNum < 0 ? DBNull.Value : (object)st.StartPageNum;
                    param[3] = new SqlParameter("@EndPageNum", SqlDbType.Int);
                    param[3].Value = st.EndPageNum < 0 ? DBNull.Value : (object)st.EndPageNum;
                    param[4] = new SqlParameter("@OtherType", SqlDbType.Int);
                    param[4].Value = st.OtherType;
                    #endregion 第二步
                }
                else if (step == 3)
                {
                    #region 第三步
                    Sql = "update " + Pre + "Collect_Site set LinkSetting=@LinkSetting where ChannelID='" + Current.SiteID + "' and ID=" + st.ID;
                    param = new SqlParameter[1];
                    param[0] = new SqlParameter("@LinkSetting", SqlDbType.NVarChar, 4000);
                    param[0].Value = st.LinkSetting;
                    #endregion 第三步
                }
                else if (step == 4)
                {
                    #region 第四步
                    Sql = "Update " + Pre + "Collect_Site set PageTitleSetting=@PageTitleSetting,PagebodySetting=@PagebodySetting";
                    Sql += ",AuthorSetting=@AuthorSetting,SourceSetting=@SourceSetting,AddDateSetting=@AddDateSetting,HandSetAuthor=@HandSetAuthor";
                    Sql += ",HandSetSource=@HandSetSource,HandSetAddDate=@HandSetAddDate,OtherNewsType=@OtherNewsType";
                    Sql += ",OtherNewsPageSetting=@OtherNewsPageSetting where ChannelID='" + Current.SiteID + "' and ID=" + st.ID;
                    param = new SqlParameter[10];
                    param[0] = new SqlParameter("@PageTitleSetting", SqlDbType.NVarChar, 4000);
                    param[0].Value = st.PageTitleSetting;
                    param[1] = new SqlParameter("@PagebodySetting", SqlDbType.NVarChar, 4000);
                    param[1].Value = st.PagebodySetting;
                    param[2] = new SqlParameter("@AuthorSetting", SqlDbType.NVarChar, 4000);
                    param[2].Value = st.AuthorSetting.Equals("") ? DBNull.Value : (object)st.AuthorSetting;
                    param[3] = new SqlParameter("@SourceSetting", SqlDbType.NText);
                    param[3].Value = st.SourceSetting.Equals("") ? DBNull.Value : (object)st.SourceSetting;
                    param[4] = new SqlParameter("@AddDateSetting", SqlDbType.NText);
                    param[4].Value = st.AddDateSetting.Equals("") ? DBNull.Value : (object)st.AddDateSetting;
                    param[5] = new SqlParameter("@HandSetAuthor", SqlDbType.NVarChar, 100);
                    param[5].Value = st.HandSetAuthor.Equals("") ? DBNull.Value : (object)st.HandSetAuthor;
                    param[6] = new SqlParameter("@HandSetSource", SqlDbType.NVarChar, 100);
                    param[6].Value = st.HandSetSource.Equals("") ? DBNull.Value : (object)st.HandSetSource;
                    param[7] = new SqlParameter("@HandSetAddDate", SqlDbType.DateTime);
                    param[7].Value = (st.HandSetAddDate.Year < 1753) ? DBNull.Value : (object)st.HandSetAddDate;
                    param[8] = new SqlParameter("@OtherNewsType", SqlDbType.Int);
                    param[8].Value = st.OtherNewsType;
                    param[9] = new SqlParameter("@OtherNewsPageSetting", SqlDbType.NVarChar, 4000);
                    param[9].Value = st.OtherNewsPageSetting.Trim().Equals("") ? DBNull.Value : (object)st.OtherNewsPageSetting;
                    #endregion 第四步
                }
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public void FolderUpdate(int id, string Name, string Description)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            string Sql = "select count(*) from " + Pre + "Collect_SiteFolder where SiteFolder=@SiteFolder and ChannelID='" + Current.SiteID + "' and ID<>" + id;
            SqlParameter prm = new SqlParameter("@SiteFolder", SqlDbType.NVarChar, 50);
            prm.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, prm));
            if (n > 0)
            {
                cn.Close();
                throw new Exception("栏目名称重复");
            }
            Sql = "update " + Pre + "Collect_SiteFolder set SiteFolder=@SiteFolder,SiteFolderDetail=@SiteFolderDetail where ChannelID='" + Current.SiteID + "' and ID=" + id;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SiteFolder", SqlDbType.NVarChar, 50);
            param[0].Value = Name;
            param[1] = new SqlParameter("@SiteFolderDetail", SqlDbType.NText);
            param[1].Value = Description.Trim().Equals("") ? DBNull.Value : (object)Description;
            DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
            cn.Close();
        }
        private SqlParameter[] GetParameters(CollectSiteInfo st)
        {
            SqlParameter[] pmt = new SqlParameter[19];
            pmt[0] = new SqlParameter("@SiteName", SqlDbType.NVarChar, 50);
            pmt[0].Value = st.SiteName;
            pmt[1] = new SqlParameter("@objURL", SqlDbType.NVarChar, 250);
            pmt[1].Value = st.objURL;
            pmt[2] = new SqlParameter("@Folder", SqlDbType.Int);
            pmt[2].Value = st.Folder < 1 ? DBNull.Value : (object)st.Folder;
            pmt[3] = new SqlParameter("@SaveRemotePic", SqlDbType.Bit);
            pmt[3].Value = st.SaveRemotePic;
            pmt[4] = new SqlParameter("@Audit", SqlDbType.NVarChar, 10);
            pmt[4].Value = st.Audit;
            pmt[5] = new SqlParameter("@IsReverse", SqlDbType.Bit);
            pmt[5].Value = st.IsReverse;
            pmt[6] = new SqlParameter("@IsAutoPicNews", SqlDbType.Bit);
            pmt[6].Value = st.IsAutoPicNews;
            pmt[7] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 50);
            pmt[7].Value = st.ClassID;
            pmt[8] = new SqlParameter("@TextTF", SqlDbType.Bit);
            pmt[8].Value = st.TextTF;
            pmt[9] = new SqlParameter("@IsStyle", SqlDbType.Bit);
            pmt[9].Value = st.IsStyle;
            pmt[10] = new SqlParameter("@IsDIV", SqlDbType.Bit);
            pmt[10].Value = st.IsDIV;
            pmt[11] = new SqlParameter("@IsA", SqlDbType.Bit);
            pmt[11].Value = st.IsA;
            pmt[12] = new SqlParameter("@IsClass", SqlDbType.Bit);
            pmt[12].Value = st.IsClass;
            pmt[13] = new SqlParameter("@IsFont", SqlDbType.Bit);
            pmt[13].Value = st.IsFont;
            pmt[14] = new SqlParameter("@IsSpan", SqlDbType.Bit);
            pmt[14].Value = st.IsSpan;
            pmt[15] = new SqlParameter("@IsObject", SqlDbType.Bit);
            pmt[15].Value = st.IsObject;
            pmt[16] = new SqlParameter("@IsIFrame", SqlDbType.Bit);
            pmt[16].Value = st.IsIFrame;
            pmt[17] = new SqlParameter("@IsScript", SqlDbType.Bit);
            pmt[17].Value = st.IsScript;
            pmt[18] = new SqlParameter("@Encode", SqlDbType.NVarChar, 50);
            pmt[18].Value = st.Encode;
            return pmt;
        }
        public DataTable GetRulePage(int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            return DbHelper.ExecutePage(DBConfig.CollectConString, "ID,RuleName,AddDate", Pre + "Collect_Rule where ChannelID='" + Current.SiteID + "'", "ID", "Order by ID", PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        public void RuleDelete(int id)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            SqlTransaction trans = cn.BeginTransaction();
            try
            {
                string Sql = "delete from " + Pre + "Collect_RuleApply where RuleID=" + id;
                DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                Sql = "delete from " + Pre + "Collect_Rule where ChannelID='" + Current.SiteID + "' and ID=" + id;
                DbHelper.ExecuteNonQuery(trans, CommandType.Text, Sql, null);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public int RuleAdd(string Name, string OldStr, string NewStr, int[] AppSites, bool IgnoreCase)
        {
            int id = 0;
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            string Sql = "select count(*) from " + Pre + "Collect_Rule where RuleName=@RuleName and ChannelID='" + Current.SiteID + "'";
            SqlParameter param = new SqlParameter("@RuleName", SqlDbType.NVarChar, 50);
            param.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
            if (n > 0)
            {
                cn.Close();
                throw new Exception("规则名称重复");
            }
            SqlTransaction Trans = cn.BeginTransaction();
            try
            {
                Sql = "insert into " + Pre + "Collect_Rule (RuleName,OldContent,ReContent,AddDate,IgnoreCase,ChannelID) values (@RuleName,@OldContent,@ReContent,@AddDate,@IgnoreCase,'" + Current.SiteID + "')";
                Sql += ";SELECT @@IDENTITY";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@RuleName", SqlDbType.NVarChar, 50);
                parm[0].Value = Name;
                parm[1] = new SqlParameter("@OldContent", SqlDbType.NVarChar, 100);
                parm[1].Value = OldStr;
                parm[2] = new SqlParameter("@ReContent", SqlDbType.NVarChar, 100);
                parm[2].Value = NewStr;
                parm[3] = new SqlParameter("@AddDate", SqlDbType.DateTime);
                parm[3].Value = DateTime.Now;
                parm[4] = new SqlParameter("@IgnoreCase", SqlDbType.Bit);
                parm[4].Value = IgnoreCase;
                id = Convert.ToInt32(DbHelper.ExecuteScalar(Trans, CommandType.Text, Sql, parm));
                if (AppSites != null && AppSites.Length > 0)
                {
                    foreach (int stid in AppSites)
                    {
                        Sql = "delete from " + Pre + "Collect_RuleApply where SiteID=" + stid;
                        DbHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, null);
                        Sql = "insert into " + Pre + "Collect_RuleApply(SiteID,RuleID,RefreshTime) values (" + stid + "," + id + ",'" + DateTime.Now + "')";
                        DbHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, null);
                    }
                }
                Trans.Commit();
            }
            catch
            {
                Trans.Rollback();
                throw;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return id;
        }
        public void RuleUpdate(int RuleID, string Name, string OldStr, string NewStr, int[] AppSites, bool IgnoreCase)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CollectConString);
            cn.Open();
            string Sql = "select count(*) from " + Pre + "Collect_Rule where RuleName=@RuleName and ChannelID='" + Current.SiteID + "' and ID<>" + RuleID;
            SqlParameter param = new SqlParameter("@RuleName", SqlDbType.NVarChar, 50);
            param.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
            if (n > 0)
            {
                cn.Close();
                throw new Exception("规则名称重复");
            }
            SqlTransaction Trans = cn.BeginTransaction();
            try
            {
                Sql = "update " + Pre + "Collect_Rule set RuleName=@RuleName,OldContent=@OldContent,ReContent=@ReContent,IgnoreCase=@IgnoreCase where ChannelID='" + Current.SiteID + "' and ID=" + RuleID;
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@RuleName", SqlDbType.NVarChar, 50);
                parm[0].Value = Name;
                parm[1] = new SqlParameter("@OldContent", SqlDbType.NVarChar, 100);
                parm[1].Value = OldStr;
                parm[2] = new SqlParameter("@ReContent", SqlDbType.NVarChar, 100);
                parm[2].Value = NewStr;
                parm[3] = new SqlParameter("@IgnoreCase", SqlDbType.Bit);
                parm[3].Value = IgnoreCase;
                DbHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, parm);
                Sql = "delete from " + Pre + "Collect_RuleApply where RuleID=" + RuleID;
                DbHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, null);
                if (AppSites != null && AppSites.Length > 0)
                {
                    foreach (int stid in AppSites)
                    {
                        Sql = "delete from " + Pre + "Collect_RuleApply where SiteID=" + stid;
                        DbHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, null);
                        Sql = "insert into " + Pre + "Collect_RuleApply(SiteID,RuleID,RefreshTime) values (" + stid + "," + RuleID + ",'" + DateTime.Now + "')";
                        DbHelper.ExecuteNonQuery(Trans, CommandType.Text, Sql, null);
                    }
                }
                Trans.Commit();
            }
            catch
            {
                Trans.Rollback();
                throw;
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public DataTable GetRule(int id)
        {
            string Sql = "select * from " + Pre + "Collect_Rule where ChannelID='" + Current.SiteID + "' and ID=" + id;
            return DbHelper.ExecuteTable(DBConfig.CollectConString, CommandType.Text, Sql, null);
        }
        //获得过滤规则和过滤应用到的站点
        public DataTable GetRuleApplyList()
        {
            string Sql = "select * from " + Pre + "Collect_Rule a inner join " + Pre + "Collect_RuleApply b on a.ID = b.RuleID";
            return DbHelper.ExecuteTable(DBConfig.CollectConString, CommandType.Text, Sql, null);
        }
        public DataTable SiteList()
        {
            string Sql = "select a.id,SiteName,RuleID from " + Pre + "Collect_Site a left outer join " + Pre + "Collect_RuleApply b on a.ID = b.SiteID where a.ChannelID='" + Current.SiteID + "'";
            return DbHelper.ExecuteTable(DBConfig.CollectConString, CommandType.Text, Sql, null);
        }
        public void NewsAdd(CollectNewsInfo newsinfo)
        {
            string Sql = "insert into " + Pre + "Collect_News ([Title],[Links],[Author],[Source],[Content],[AddDate],[ImagesCount],[SiteID],[History],[ReviewTF],[CollectTime],[ChannelID],[ClassID]) values (";
            Sql += "@Title,@Links,@Author,@Source,@Content,@AddDate,0,@SiteID,0,0,'" + DateTime.Now + "','" + Current.SiteID + "',@ClassID)";
            DbHelper.ExecuteNonQuery(DBConfig.CollectConString, CommandType.Text, Sql, GetNewsParams(newsinfo));
        }
        public bool TitleExist(string title)
        {
            string Sql = "select count(id) from " + Pre + "Collect_News where Title=@Title";
            SqlParameter Param = new SqlParameter("@Title", title);
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(DBConfig.CollectConString, CommandType.Text, Sql, Param));
            if (n > 0)
                return true;
            else
                return false;
        }
        public DataTable GetNewsPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            return DbHelper.ExecutePage(DBConfig.CollectConString, "a.ID,Title,AddDate,SiteName,History,CollectTime", Pre + "Collect_News a left join " + Pre + "Collect_Site b on a.SiteID=b.ID where a.ChannelID='" + Current.SiteID + "'", "a.ID", "Order by History asc,a.ID desc", PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        public void NewsDelete(string id)
        {
            string Sql = "Delete from " + Pre + "Collect_News where ChannelID='" + Current.SiteID + "'";
            if (id.Equals("0"))
                Sql += " and History=1";
            else
                Sql += " and ID in (" + id + ")";
            DbHelper.ExecuteNonQuery(DBConfig.CollectConString, CommandType.Text, Sql, null);
        }
        public CollectNewsInfo GetNews(int id)
        {
            CollectNewsInfo info = new CollectNewsInfo();
            string Sql = "select [Title],[Links],[SiteID],[Author],[Source],[AddDate],[Content],[CollectTime],[ClassID],[History] from " + Pre + "Collect_News where ChannelID='" + Current.SiteID + "' and ID=" + id;
            IDataReader rd = DbHelper.ExecuteReader(DBConfig.CollectConString, CommandType.Text, Sql, null);
            if (rd.Read())
            {
                info.Title = rd.GetString(0);
                info.Links = rd.GetString(1);
                info.SiteID = rd.GetInt32(2);
                if (!rd.IsDBNull(3)) info.Author = rd.GetString(3);
                if (!rd.IsDBNull(4)) info.Source = rd.GetString(4);
                if (!rd.IsDBNull(5)) info.AddDate = rd.GetDateTime(5);
                if (!rd.IsDBNull(6)) info.Content = rd.GetString(6);
                info.CollectTime = rd.GetDateTime(7);
                info.ClassID = rd.GetString(8);
                info.History = rd.GetBoolean(9);
            }
            rd.Close();
            return info;
        }
        public void NewsUpdate(int id, CollectNewsInfo info)
        {
            string Sql = "update " + Pre + "Collect_News set [Title]=@Title,[Links]=@Links,[SiteID]=@SiteID,[Author]=@Author";
            Sql += ",[Source]=@Source,[AddDate]=@AddDate,[Content]=@Content,[ClassID]=@ClassID where ChannelID='" + Current.SiteID + "' and ID=" + id;
            DbHelper.ExecuteNonQuery(DBConfig.CollectConString, CommandType.Text, Sql, GetNewsParams(info));
        }
        private SqlParameter[] GetNewsParams(CollectNewsInfo info)
        {
            if (info.Author == null)
            {
                info.Author = "";
            }
            if (info.Source == null)
            {
                info.Source = "";
            }
            if (info.AddDate == null)
            {
                info.AddDate = DateTime.Now;
            }
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            param[0].Value = info.Title;
            param[1] = new SqlParameter("@Links", SqlDbType.NVarChar, 200);
            param[1].Value = info.Links;
            param[2] = new SqlParameter("@Author", SqlDbType.NVarChar, 100);
            param[2].Value = info.Author.Trim().Equals("") ? DBNull.Value : (object)info.Author;
            param[3] = new SqlParameter("@Source", SqlDbType.NVarChar, 100);
            param[3].Value = info.Source.Trim().Equals("") ? DBNull.Value : (object)info.Source;
            param[4] = new SqlParameter("@Content", SqlDbType.NText);
            param[4].Value = info.Content;
            param[5] = new SqlParameter("@AddDate", SqlDbType.DateTime);
            param[5].Value = info.AddDate.Year < 1753 ? DBNull.Value : (object)info.AddDate;
            param[6] = new SqlParameter("@SiteID", SqlDbType.Int);
            param[6].Value = info.SiteID;
            param[7] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
            param[7].Value = info.ClassID;
            return param;
        }
        #region 新闻入库
        ///////////////////////////新闻入库专用///////////////////////////////////////
        private SqlConnection connetion;
        private SqlConnection confoosun;
        private int nStoreSucceed = 0;
        private int nStoreFailed = 0;
        public void StoreNews(bool UnStore, int[] id, out int nSucceed, out int nFailed)
        {
            nSucceed = 0;
            nFailed = 0;
            connetion = new SqlConnection(DBConfig.CollectConString);
            confoosun = new SqlConnection(DBConfig.CmsConString);
            string Sql = "select a.ID,a.Title,a.Links,a.Author,a.Source,a.Content,a.AddDate,a.RecTF,a.TodayNewsTF,a.MarqueeNews";
            Sql += ",a.SBSNews,a.ReviewTF,a.ClassID,b.Audit from " + Pre + "Collect_News a inner join " + Pre + "Collect_Site b";
            Sql += " on a.SiteID=b.ID where a.ChannelID='" + Current.SiteID + "'";
            try
            {
                connetion.Open();
                confoosun.Open();
                if (UnStore)
                {
                    Sql += " and History=0";
                }
                else
                {
                    string strid = "";
                    for (int i = 0; i < id.Length; i++)
                    {
                        if (i > 0)
                            strid += ",";
                        strid += id[i].ToString();
                    }
                    Sql += " and a.id in (" + strid + ")";
                }
                StoreStep(Sql);
                nSucceed = nStoreSucceed;
                nFailed = nStoreFailed;
            }
            finally
            {
                StoreEnd();
            }
        }
        private void StoreStep(string Sql)
        {
            int IsAudit = 3;
            int AID = 0;
            object obj = DbHelper.ExecuteScalar(confoosun, CommandType.Text, "select max(id) from " + Pre + "news", null);
            if (obj != null && obj != DBNull.Value)
                AID = Convert.ToInt32(obj);
            AID++;
            DataTable dt = DbHelper.ExecuteTable(connetion, CommandType.Text, Sql, null);
            CollectNewsInfo Info = new CollectNewsInfo();
            foreach (DataRow r in dt.Rows)
            {
                try
                {
                    bool isExistedClassID = true;//将要入库新闻的栏目ID是否有效
                    int id = Convert.ToInt32(r["id"]);
                    string sTitle = r["Title"].ToString();
                    string sClass = r["ClassID"].ToString();
                    string sContent = r["Content"].ToString();
                    string sLinks = r["Links"].ToString();
                    string sAuthor = "";
                    if (r["Author"] != DBNull.Value) sAuthor = r["Author"].ToString();
                    string sSource = "";
                    if (r["Source"] != DBNull.Value) sSource = r["Source"].ToString();
                    DateTime dtAddDate = DateTime.Now;
                    if (r["AddDate"] != DBNull.Value) dtAddDate = DateTime.Parse(r["AddDate"].ToString());
                    IsAudit = (int)r["Audit"];

                    string CheckSate = "3|1|1|1";
                    int IsLock = 1;
                    switch (IsAudit)
                    {
                        case 0:
                            CheckSate = "0|0|0|0";
                            IsLock = 0;
                            break;
                        case 1:
                            CheckSate = "1|1|0|0";
                            break;
                        case 2:
                            CheckSate = "2|1|1|0";
                            break;
                    }
                    #region 取新闻的默认值选项
                    #region 用于保存新闻的变量
                    string NewsPathRule = string.Empty;
                    string NewsFileRule = string.Empty;
                    byte isDelPoint = 0;
                    int Gpoint = 0;
                    int iPoint = 0;
                    string GroupNumber = string.Empty;
                    string FileExName = ".html";
                    string DataLib = Pre + "news";
                    string Temp = string.Empty;
                    string ClassEname = string.Empty;
                    #endregion 用于保存新闻的变量
                    Sql = "select NewsSavePath,NewsFileRule,isDelPoint,Gpoint,iPoint,GroupNumber,FileName,DataLib,ReadNewsTemplet,ClassEName";
                    Sql += " from " + Pre + "news_Class where ClassID='" + sClass + "'";
                    IDataReader rd = DbHelper.ExecuteReader(confoosun, CommandType.Text, Sql, null);
                    if (rd.Read())
                    {
                        #region 赋值
                        ClassEname = rd["ClassEName"].ToString();
                        if (rd["NewsSavePath"] != DBNull.Value)
                            NewsPathRule = rd["NewsSavePath"].ToString();
                        if (rd["NewsFileRule"] != DBNull.Value)
                            NewsFileRule = rd["NewsFileRule"].ToString();
                        if (rd["isDelPoint"] != DBNull.Value)
                            isDelPoint = Convert.ToByte(rd["isDelPoint"]);
                        if (rd["Gpoint"] != DBNull.Value)
                            Gpoint = Convert.ToInt32(rd["Gpoint"]);
                        if (rd["iPoint"] != DBNull.Value)
                            iPoint = Convert.ToInt32(rd["iPoint"]);
                        if (rd["GroupNumber"] != DBNull.Value)
                            GroupNumber = rd["GroupNumber"].ToString();
                        if (rd["FileName"] != DBNull.Value)
                            FileExName = rd["FileName"].ToString();
                        if (rd["DataLib"] != DBNull.Value)
                            DataLib = rd["DataLib"].ToString();
                        if (rd["ReadNewsTemplet"] != DBNull.Value)
                            Temp = rd["ReadNewsTemplet"].ToString();
                        #endregion 赋值
                    }
                    else
                    {
                        nStoreFailed++;
                        isExistedClassID = false;
                    }
                    rd.Close();
                    if (isExistedClassID)
                    {
                        #region 保存的文件名等的计算
                        string SavePath = ExChangeRule(NewsPathRule, sClass, ClassEname, AID);
                        string FileName = ExChangeRule(NewsFileRule, sClass, ClassEname, AID);
                        string NewsID = Foosun.Common.Rand.Number(12);
                        if (FileName == string.Empty)
                            FileName = Foosun.Common.Rand.Number(12);
                        while (Convert.ToInt32(DbHelper.ExecuteScalar(confoosun, CommandType.Text, "select count(ID) from " + Pre + "news where NewsID='" + NewsID + "' or FileName='" + FileName + "'", null)) > 0)
                        {
                            NewsID = Foosun.Common.Rand.Number(12, true);
                            FileName = FileName + "_" + Foosun.Common.Rand.Number(3, true);
                        }
                        #endregion
                    #endregion 取新闻的默认值选项
                        Sql = "insert into " + Pre + "news (";
                        Sql += "[NewsID],[NewsType],[OrderID],[NewsTitle],[TitleITF],[URLaddress],[ClassID],[NewsPicTopline],[Author],[Souce],[Templet],[Content],";
                        Sql += "[CreatTime],[isLock],[SiteID],[Editor],[CheckStat],[NewsProperty],[Click],[FileName],[FileEXName],[isDelPoint],[Gpoint],";
                        Sql += "[iPoint],[GroupNumber],[ContentPicTF],[CommTF],[DiscussTF],[TopNum],[VoteTF],[isRecyle],[DataLib],[isVoteTF],[SavePath],";
                        Sql += "[isHtml],[isConstr]";
                        Sql += ") values (";
                        Sql += "@NewsID,@NewsType,0,@NewsTitle,0,@URLaddress,@ClassID,0,@Author,@Souce,@Templet,@Content,";
                        Sql += "@CreatTime,@IsLock,@SiteID,@Editor,@CheckStat,@NewsProperty,0,@FileName,@FileEXName,@isDelPoint,@Gpoint,";
                        Sql += "@iPoint,@GroupNumber,0,1,0,0,0,0,@DataLib,0,@SavePath,";
                        Sql += "0,0)";
                        //'" + FileName + "','.html',0,0,0,0,1,1,0,1,0,'FS_News',0,0,0)";
                        #region SQL参数赋值
                        SqlParameter[] param = new SqlParameter[23];
                        param[0] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 13);
                        param[0].Value = NewsID;
                        param[1] = new SqlParameter("@NewsType", SqlDbType.TinyInt);
                        param[1].Value = 0;
                        param[2] = new SqlParameter("@NewsTitle", SqlDbType.NVarChar, 100);
                        param[2].Value = sTitle;
                        param[3] = new SqlParameter("@URLaddress", SqlDbType.NVarChar, 200);
                        param[3].Value = sLinks;
                        param[4] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
                        param[4].Value = sClass;
                        param[5] = new SqlParameter("@Author", SqlDbType.NVarChar, 100);
                        param[5].Value = sAuthor;
                        param[6] = new SqlParameter("@Souce", SqlDbType.NVarChar, 100);
                        param[6].Value = sSource;
                        param[7] = new SqlParameter("@Templet", SqlDbType.NVarChar, 200);
                        if (Temp == string.Empty)
                            param[7].Value = DBNull.Value;
                        else
                            param[7].Value = Temp;
                        param[8] = new SqlParameter("@Content", SqlDbType.NText);
                        param[8].Value = sContent;
                        param[9] = new SqlParameter("@CreatTime", SqlDbType.DateTime);
                        param[9].Value = dtAddDate;
                        param[10] = new SqlParameter("@IsLock", SqlDbType.TinyInt);
                        param[10].Value = IsLock;
                        param[11] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
                        param[11].Value = Current.SiteID;
                        param[12] = new SqlParameter("@Editor", SqlDbType.NVarChar, 18);
                        param[12].Value = Foosun.Global.Current.UserName;
                        param[13] = new SqlParameter("@CheckStat", SqlDbType.NVarChar, 10);
                        param[13].Value = CheckSate;
                        param[14] = new SqlParameter("@NewsProperty", SqlDbType.NVarChar, 30);
                        param[14].Value = "0,0,0,0,0,0,0,0";
                        param[15] = new SqlParameter("@FileName", SqlDbType.NVarChar, 100);
                        param[15].Value = FileName;
                        param[16] = new SqlParameter("@FileEXName", SqlDbType.NVarChar, 6);
                        param[16].Value = FileExName;
                        param[17] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt);
                        param[17].Value = isDelPoint;
                        param[18] = new SqlParameter("@Gpoint", SqlDbType.Int);
                        param[18].Value = Gpoint;
                        param[19] = new SqlParameter("@iPoint", SqlDbType.Int);
                        param[19].Value = iPoint;
                        param[20] = new SqlParameter("@GroupNumber", SqlDbType.NText);
                        param[20].Value = GroupNumber;
                        param[21] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 20);
                        param[21].Value = DataLib;
                        param[22] = new SqlParameter("@SavePath", SqlDbType.NVarChar, 200);
                        if (SavePath == string.Empty)
                            param[22].Value = DBNull.Value;
                        else
                            param[22].Value = SavePath;
                        #endregion SQL参数赋值
                        DbHelper.ExecuteNonQuery(confoosun, CommandType.Text, Sql, param);
                        Sql = "update " + Pre + "Collect_News set History=1 where ID=" + id;
                        DbHelper.ExecuteNonQuery(connetion, CommandType.Text, Sql, null);
                        nStoreSucceed++;
                        AID++;
                    }
                }
                catch
                {
                    nStoreFailed++;
                }
            }
            dt.Clear();
            dt.Dispose();
        }
        private void StoreEnd()
        {
            if (connetion != null && connetion.State == ConnectionState.Open)
                connetion.Close();
            if (confoosun != null && confoosun.State == ConnectionState.Open)
                confoosun.Close();
        }
        private string ExChangeRule(string RuleStr, string classid, string classename, int autoid)
        {
            string RetVal = string.Empty;
            if (RuleStr != null && RuleStr != string.Empty)
            {
                RetVal = RuleStr;
                DateTime Now = DateTime.Now;
                RetVal = Regex.Replace(RetVal, @"\{\@year02\}", Now.Year.ToString().Substring(2, 2), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                RetVal = Regex.Replace(RetVal, @"\{\@year04\}", Now.Year.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                RetVal = Regex.Replace(RetVal, @"\{\@month\}", Now.Month.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                RetVal = Regex.Replace(RetVal, @"\{\@day\}", Now.Day.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                RetVal = Regex.Replace(RetVal, @"\{\@hour\}", Now.Hour.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                RetVal = Regex.Replace(RetVal, @"\{\@minute\}", Now.Minute.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                RetVal = Regex.Replace(RetVal, @"\{\@second\}", Now.Second.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                RetVal = Regex.Replace(RetVal, @"\{\@ClassId\}", classid, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                RetVal = Regex.Replace(RetVal, @"\{\@EName\}", classename, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex reg = new Regex(@"\{\@Ram(?<m>\d+)_(?<n>\d+)\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match match = reg.Match(RetVal);
                while (match.Success)
                {
                    int m = Convert.ToInt32(match.Groups["m"].Value);
                    int n = Convert.ToInt32(match.Groups["n"].Value);
                    string s = match.Value;
                    string rnd = string.Empty;
                    switch (n)
                    {
                        case 0:
                            rnd = Foosun.Common.Rand.Number(m);
                            break;
                        case 1:
                            rnd = Foosun.Common.Rand.Str_char(m);
                            break;
                        case 2:
                            rnd = Foosun.Common.Rand.Str(m);
                            break;
                    }
                    RetVal = RetVal.Replace(s, rnd);
                    match = reg.Match(RetVal);
                }
                RetVal = Regex.Replace(RetVal, @"\{\@自动编号ID\}", autoid.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            return RetVal;
        }
        #endregion 新闻入库
    }
}
