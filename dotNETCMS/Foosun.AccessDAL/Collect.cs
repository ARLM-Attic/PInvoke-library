using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Collections;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Global;
using Foosun.DALProfile;
using Foosun.Config;
using Foosun.Control;

namespace Foosun.AccessDAL
{
    public class Collect : DbBase, ICollect
    {
        public DataTable GetFolderSitePage(int FolderID, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {

            if (FolderID < 1)
            {

                OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
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
                //SQLserver中case when 为 IIf(expr,   truepart,   falsepart) 
                string Sql = "(select 0 as TP,ID,SiteFolder as SName,'' as objURL,'' as LockState from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "') union (select 1 as TP,ID,SiteName as SName,objURL,iif(LinkSetting is null or PagebodySetting is null or  PageTitleSetting is null,'无效', '有效') as LockState from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and (Folder is null or Folder=0))";
                OleDbDataAdapter ap = new OleDbDataAdapter(Sql, cn);
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
                return DbHelper.ExecutePage(Database.CollectConnectionString, "1 as TP,ID,SiteName as SName,objURL,iif(LinkSetting is null or PagebodySetting is null or  PageTitleSetting is null,'无效','有效') as LockState", Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and Folder=" + FolderID, "ID", "Order by ID", PageIndex, PageSize, out RecordCount, out PageCount, null);
            }
        }
        public void SiteCopy(int id)
        {
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
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
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
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
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
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
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
            cn.Open();
            OleDbTransaction tran = cn.BeginTransaction();
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
            return DbHelper.ExecuteTable(Database.CollectConnectionString, CommandType.Text, Sql, null);
        }
        public DataTable GetSite(int id)
        {
            //string Sql = "select a.*,b.OldContent,b.ReContent,b.IgnoreCase from " + Pre + "Collect_Site a left join (" + Pre + "Collect_Rule b inner join " + Pre + "Collect_RuleApply c on b.ID=c.RuleID) on c.SiteID=a.ID where a.ChannelID='" + Current.SiteID + "' and a.ID=" + id;
            //string Sql = "select a.*,b.OldContent,b.ReContent,b.IgnoreCase from " + Pre + "Collect_Site a," + Pre + "Collect_Rule b, " + Pre + "Collect_RuleApply c where  b.ID=c.RuleID and  c.SiteID=a.ID and a.ChannelID='" + Current.SiteID + "' and a.ID=" + id;
            string Sql = "select * from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and ID=" + id;
            DataTable dt = new DataTable();
            dt = DbHelper.ExecuteTable(Database.CollectConnectionString, CommandType.Text, Sql, null);
            dt.Columns.Add("OldContent", typeof(string));
            dt.Columns.Add("IgnoreCase", typeof(string));
            dt.Columns.Add("ReContent", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                DataTable dtt = new DataTable();
                Sql = "select b.OldContent,b.IgnoreCase,b.ReContent from " + Pre + "Collect_Rule b inner join " + Pre + "Collect_RuleApply c on b.ID=c.RuleID where  c.SiteID="+dr["ID"];
                dtt=DbHelper.ExecuteTable(Database.CollectConnectionString, CommandType.Text, Sql, null);
                if (dtt.Rows.Count > 0)
                {
                    dr["OldContent"] = dtt.Rows[0][0];
                    dr["IgnoreCase"] = dtt.Rows[0][1];
                    dr["ReContent"] = dtt.Rows[0][2];
                }
            }
            return dt;
            //return DbHelper.ExecuteTable(Database.CollectConnectionString, CommandType.Text, Sql, null);
        }
        public int SiteAdd(CollectSiteInfo st)
        {
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
            cn.Open();
            try
            {
                string Sql = "select count(*) from " + Pre + "Collect_Site where ChannelID='" + Current.SiteID + "' and SiteName=@SiteName";
                OleDbParameter parm = new OleDbParameter("@SiteName", OleDbType.VarWChar, 50);
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
                Sql += ",IsStyle,IsDIV,IsA,IsClass,IsFont,IsSpan,IsObject,IsIFrame,IsScript,Encode,ClassID,MaxNum,ChannelID) values (";
                Sql += "@SiteName,@objURL,@Folder,@SaveRemotePic,@Audit,@IsReverse,@IsAutoPicNews,@TextTF";
                Sql += ",@IsStyle,@IsDIV,@IsA,@IsClass,@IsFont,@IsSpan,@IsObject,@IsIFrame,@IsScript,@Encode,@ClassID,10000,'" + Current.SiteID + "')";
                Sql += "";
                DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, Database.getNewParam(GetParameters(st), "SiteName,objURL,Folder,SaveRemotePic,Audit,IsReverse,IsAutoPicNews,TextTF,IsStyle,IsDIV,IsA,IsClass,IsFont,IsSpan,IsObject,IsIFrame,IsScript,Encode,ClassID,ChannelID"));
                Sql = "select @@identity";
                //bug修改　周峻平 2008-6-20　将最近插入的ＩＤ查询出来
                int result = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, null));
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
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
            cn.Open();
            string Sql = "select count(*) from " + Pre + "Collect_SiteFolder where ChannelID='" + Current.SiteID + "' and SiteFolder=@SiteFolder";
            OleDbParameter prm = new OleDbParameter("@SiteFolder", OleDbType.VarWChar, 50);
            prm.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, prm));
            if (n > 0)
            {
                cn.Close();
                throw new Exception("栏目名称重复");
            }
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@SiteFolder", OleDbType.VarWChar, 50);
            param[0].Value = Name;
            param[1] = new OleDbParameter("@SiteFolderDetail", OleDbType.VarWChar);
            param[1].Value = Description.Trim().Equals("") ? DBNull.Value : (object)Description;
            Sql = "insert into " + Pre + "Collect_SiteFolder (SiteFolder,SiteFolderDetail,ChannelID) values (@SiteFolder,@SiteFolderDetail,'" + Current.SiteID + "')";
            n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
            
            
            cn.Close();
            return n;
        }
        public void SiteUpdate(CollectSiteInfo st, int step)
        {
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
            cn.Open();
            try
            {
                OleDbParameter[] param = null;
                string Sql = "";
                if (step == 1)
                {
                    #region 第一步
                    Sql = "select count(*) from " + Pre + "Collect_Site where SiteName=@SiteName and ChannelID='" + Current.SiteID + "' and ID<>@ID";
                    OleDbParameter[] parm = new OleDbParameter[2];
                    parm[0] = new OleDbParameter("@SiteName", OleDbType.VarWChar, 50);
                    parm[0].Value = st.SiteName;
                    parm[1] = new OleDbParameter("@ID", OleDbType.Integer);
                    parm[1].Value = st.ID;
                    int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, parm));
                    if (n > 0)
                    {
                        cn.Close();
                        throw new Exception("采集站点名称重复!");
                    }
                    param = GetParameters(st);
                    Sql = "update " + Pre + "Collect_Site Set "+Database.getModifyParam(param)+" where ChannelID='" + Current.SiteID + "' and ID=" + st.ID;
                    
                    #endregion 第一步
                }
                else if (step == 2)
                {
                    #region 第二步
                    param = new OleDbParameter[5];
                    param[0] = new OleDbParameter("@ListSetting", OleDbType.VarWChar, 4000);
                    param[0].Value = st.ListSetting;
                    param[1] = new OleDbParameter("@OtherPageSetting", OleDbType.VarWChar, 4000);
                    param[1].Value = st.OtherPageSetting.Trim().Equals("") ? DBNull.Value : (object)st.OtherPageSetting;
                    param[2] = new OleDbParameter("@StartPageNum", OleDbType.Integer);
                    param[2].Value = st.StartPageNum < 0 ? DBNull.Value : (object)st.StartPageNum;
                    param[3] = new OleDbParameter("@EndPageNum", OleDbType.Integer);
                    param[3].Value = st.EndPageNum < 0 ? DBNull.Value : (object)st.EndPageNum;
                    param[4] = new OleDbParameter("@OtherType", OleDbType.Integer);
                    param[4].Value = st.OtherType;
                    Sql = "update " + Pre + "Collect_Site set "+Database.getModifyParam(param)+" where ChannelID='" + Current.SiteID + "' and ID=" + st.ID;
                   
                    #endregion 第二步
                }
                else if (step == 3)
                {
                    #region 第三步
                    Sql = "update " + Pre + "Collect_Site set LinkSetting=@LinkSetting where ChannelID='" + Current.SiteID + "' and ID=" + st.ID;
                    param = new OleDbParameter[1];
                    param[0] = new OleDbParameter("@LinkSetting", OleDbType.VarWChar, 4000);
                    param[0].Value = st.LinkSetting;
                    #endregion 第三步
                }
                else if (step == 4)
                {
                    #region 第四步
                    param = new OleDbParameter[10];
                    param[0] = new OleDbParameter("@PageTitleSetting", OleDbType.VarWChar, 4000);
                    param[0].Value = st.PageTitleSetting;
                    param[1] = new OleDbParameter("@PagebodySetting", OleDbType.VarWChar, 4000);
                    param[1].Value = st.PagebodySetting;
                    param[2] = new OleDbParameter("@AuthorSetting", OleDbType.VarWChar, 4000);
                    param[2].Value = st.AuthorSetting.Equals("") ? DBNull.Value : (object)st.AuthorSetting;
                    param[3] = new OleDbParameter("@SourceSetting", OleDbType.VarWChar);
                    param[3].Value = st.SourceSetting.Equals("") ? DBNull.Value : (object)st.SourceSetting;
                    param[4] = new OleDbParameter("@AddDateSetting", OleDbType.VarWChar);
                    param[4].Value = st.AddDateSetting.Equals("") ? DBNull.Value : (object)st.AddDateSetting;
                    param[5] = new OleDbParameter("@HandSetAuthor", OleDbType.VarWChar, 100);
                    param[5].Value = st.HandSetAuthor.Equals("") ? DBNull.Value : (object)st.HandSetAuthor;
                    param[6] = new OleDbParameter("@HandSetSource", OleDbType.VarWChar, 100);
                    param[6].Value = st.HandSetSource.Equals("") ? DBNull.Value : (object)st.HandSetSource;
                    param[7] = new OleDbParameter("@HandSetAddDate", OleDbType.Date);
                    param[7].Value = (st.HandSetAddDate.Year < 1753) ? DBNull.Value : (object)st.HandSetAddDate;
                    param[8] = new OleDbParameter("@OtherNewsType", OleDbType.Integer);
                    param[8].Value = st.OtherNewsType;
                    param[9] = new OleDbParameter("@OtherNewsPageSetting", OleDbType.VarWChar, 4000);
                    param[9].Value = st.OtherNewsPageSetting.Trim().Equals("") ? DBNull.Value : (object)st.OtherNewsPageSetting;
                    Sql = "Update " + Pre + "Collect_Site set "+Database.getModifyParam(param)+" where ChannelID='" + Current.SiteID + "' and ID=" + st.ID;
                   
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
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
            cn.Open();
            string Sql = "select count(*) from " + Pre + "Collect_SiteFolder where SiteFolder=@SiteFolder and ChannelID='" + Current.SiteID + "' and ID<>" + id;
            OleDbParameter prm = new OleDbParameter("@SiteFolder", OleDbType.VarWChar, 50);
            prm.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, prm));
            if (n > 0)
            {
                cn.Close();
                throw new Exception("栏目名称重复");
            }
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@SiteFolder", OleDbType.VarWChar, 50);
            param[0].Value = Name;
            param[1] = new OleDbParameter("@SiteFolderDetail", OleDbType.VarWChar);
            param[1].Value = Description.Trim().Equals("") ? DBNull.Value : (object)Description;
            DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
            Sql = "update " + Pre + "Collect_SiteFolder set "+Database.getModifyParam(param)+" where ChannelID='" + Current.SiteID + "' and ID=" + id;
           
            cn.Close();
        }
        private OleDbParameter[] GetParameters(CollectSiteInfo st)
        {
            OleDbParameter[] pmt = new OleDbParameter[19];
            pmt[0] = new OleDbParameter("@SiteName", OleDbType.VarWChar, 50);
            pmt[0].Value = st.SiteName;
            pmt[1] = new OleDbParameter("@objURL", OleDbType.VarWChar, 250);
            pmt[1].Value = st.objURL;
            pmt[2] = new OleDbParameter("@Folder", OleDbType.Integer);
            pmt[2].Value = st.Folder < 1 ? DBNull.Value : (object)st.Folder;
            pmt[3] = new OleDbParameter("@SaveRemotePic", OleDbType.Boolean);
            pmt[3].Value = st.SaveRemotePic;
            pmt[4] = new OleDbParameter("@Audit", OleDbType.VarWChar, 10);
            pmt[4].Value = st.Audit;
            pmt[5] = new OleDbParameter("@IsReverse", OleDbType.Boolean);
            pmt[5].Value = st.IsReverse;
            pmt[6] = new OleDbParameter("@IsAutoPicNews", OleDbType.Boolean);
            pmt[6].Value = st.IsAutoPicNews;
            pmt[7] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 50);
            pmt[7].Value = st.ClassID;
            pmt[8] = new OleDbParameter("@TextTF", OleDbType.Boolean);
            pmt[8].Value = st.TextTF;
            pmt[9] = new OleDbParameter("@IsStyle", OleDbType.Boolean);
            pmt[9].Value = st.IsStyle;
            pmt[10] = new OleDbParameter("@IsDIV", OleDbType.Boolean);
            pmt[10].Value = st.IsDIV;
            pmt[11] = new OleDbParameter("@IsA", OleDbType.Boolean);
            pmt[11].Value = st.IsA;
            pmt[12] = new OleDbParameter("@IsClass", OleDbType.Boolean);
            pmt[12].Value = st.IsClass;
            pmt[13] = new OleDbParameter("@IsFont", OleDbType.Boolean);
            pmt[13].Value = st.IsFont;
            pmt[14] = new OleDbParameter("@IsSpan", OleDbType.Boolean);
            pmt[14].Value = st.IsSpan;
            pmt[15] = new OleDbParameter("@IsObject", OleDbType.Boolean);
            pmt[15].Value = st.IsObject;
            pmt[16] = new OleDbParameter("@IsIFrame", OleDbType.Boolean);
            pmt[16].Value = st.IsIFrame;
            pmt[17] = new OleDbParameter("@IsScript", OleDbType.Boolean);
            pmt[17].Value = st.IsScript;
            pmt[18] = new OleDbParameter("@Encode", OleDbType.VarWChar, 50);
            pmt[18].Value = st.Encode;
            return pmt;
        }
        public DataTable GetRulePage(int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            return DbHelper.ExecutePage(Database.CollectConnectionString, "ID,RuleName,AddDate", Pre + "Collect_Rule where ChannelID='" + Current.SiteID + "'", "ID", "Order by ID", PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        public void RuleDelete(int id)
        {
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
            cn.Open();
            OleDbTransaction trans = cn.BeginTransaction();
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
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
            cn.Open();
            string Sql = "select count(*) from " + Pre + "Collect_Rule where RuleName=@RuleName and ChannelID='" + Current.SiteID + "'";
            OleDbParameter param = new OleDbParameter("@RuleName", OleDbType.VarWChar, 50);
            param.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
            if (n > 0)
            {
                cn.Close();
                throw new Exception("规则名称重复");
            }
            OleDbTransaction Trans = cn.BeginTransaction();
            try
            {
                
                OleDbParameter[] parm = new OleDbParameter[5];
                parm[0] = new OleDbParameter("@RuleName", OleDbType.VarWChar, 50);
                parm[0].Value = Name;
                parm[1] = new OleDbParameter("@OldContent", OleDbType.VarWChar, 100);
                parm[1].Value = OldStr;
                parm[2] = new OleDbParameter("@ReContent", OleDbType.VarWChar, 100);
                parm[2].Value = NewStr;
                parm[3] = new OleDbParameter("@AddDate", OleDbType.Date);
                parm[3].Value = DateTime.Now;
                parm[4] = new OleDbParameter("@IgnoreCase", OleDbType.Boolean);
                parm[4].Value = IgnoreCase;
                //Sql = "insert into " + Pre + "Collect_Rule (RuleName,OldContent,ReContent,AddDate,IgnoreCase,ChannelID) values (@RuleName,@OldContent,@ReContent,@AddDate,@IgnoreCase,'" + Current.SiteID + "')";
                //Sql += "";
                Sql = "insert into " + Pre + "Collect_Rule ("+Database.getParam(parm)+") values ("+Database.getAParam(parm)+",'" + Current.SiteID + "')";
                Sql += "";
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
            OleDbConnection cn = new OleDbConnection(Database.CollectConnectionString);
            cn.Open();
            string Sql = "select count(*) from " + Pre + "Collect_Rule where RuleName=@RuleName and ChannelID='" + Current.SiteID + "' and ID<>" + RuleID;
            OleDbParameter param = new OleDbParameter("@RuleName", OleDbType.VarWChar, 50);
            param.Value = Name;
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, Sql, param));
            if (n > 0)
            {
                cn.Close();
                throw new Exception("规则名称重复");
            }
            OleDbTransaction Trans = cn.BeginTransaction();
            try
            {
                //Sql = "update " + Pre + "Collect_Rule set RuleName=@RuleName,OldContent=@OldContent,ReContent=@ReContent,IgnoreCase=@IgnoreCase where ChannelID='" + Current.SiteID + "' and ID=" + RuleID;
                OleDbParameter[] parm = new OleDbParameter[4];
                parm[0] = new OleDbParameter("@RuleName", OleDbType.VarWChar, 50);
                parm[0].Value = Name;
                parm[1] = new OleDbParameter("@OldContent", OleDbType.VarWChar, 100);
                parm[1].Value = OldStr;
                parm[2] = new OleDbParameter("@ReContent", OleDbType.VarWChar, 100);
                parm[2].Value = NewStr;
                parm[3] = new OleDbParameter("@IgnoreCase", OleDbType.Boolean);
                parm[3].Value = IgnoreCase;
                Sql = "update " + Pre + "Collect_Rule set "+Database.getModifyParam(parm)+" where ChannelID='" + Current.SiteID + "' and ID=" + RuleID;
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
            return DbHelper.ExecuteTable(Database.CollectConnectionString, CommandType.Text, Sql, null);
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
            return DbHelper.ExecuteTable(Database.CollectConnectionString, CommandType.Text, Sql, null);
        }
        public void NewsAdd(CollectNewsInfo newsinfo)
        {
            OleDbParameter[] parm = Database.getNewParam(GetNewsParams(newsinfo), "Title,Links,Author,Source,Content,AddDate,SiteID,ClassID");
            string Sql = "insert into " + Pre + "Collect_News ([Title],[Links],[Author],[Source],[Content],[AddDate],[ImagesCount],[SiteID],[History],[ReviewTF],[CollectTime],[ChannelID],[ClassID]) values (";
            Sql += "@Title,@Links,@Author,@Source,@Content,@AddDate,0,@SiteID,0,0,'" + DateTime.Now + "','" + Current.SiteID + "',@ClassID)";
            DbHelper.ExecuteNonQuery(Database.CollectConnectionString, CommandType.Text, Sql, GetNewsParams(newsinfo));
        }
        public bool TitleExist(string title)
        {
            if (title == null)
                return false;
            string Sql = "select count(id) from " + Pre + "Collect_News where Title=@Title";
            OleDbParameter Param = new OleDbParameter("@Title", title);
            int n = Convert.ToInt32(DbHelper.ExecuteScalar(Database.CollectConnectionString, CommandType.Text, Sql, Param));
            if (n > 0)
                return true;
            else
                return false;
        }
        public DataTable GetNewsPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            return DbHelper.ExecutePage(Database.CollectConnectionString, "a.ID,Title,AddDate,SiteName,History,CollectTime", Pre + "Collect_News a left join " + Pre + "Collect_Site b on a.SiteID=b.ID where a.ChannelID='" + Current.SiteID + "'", "a.ID", "Order by History asc,a.ID desc", PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        public void NewsDelete(string id)
        {
            string Sql = "Delete from " + Pre + "Collect_News where ChannelID='" + Current.SiteID + "'";
            if (id.Equals("0"))
                Sql += " and History=1";
            else
                Sql += " and ID in (" + id + ")";
            DbHelper.ExecuteNonQuery(Database.CollectConnectionString, CommandType.Text, Sql, null);
        }
        public CollectNewsInfo GetNews(int id)
        {
            CollectNewsInfo info = new CollectNewsInfo();
            string Sql = "select [Title],[Links],[SiteID],[Author],[Source],[AddDate],[Content],[CollectTime],[ClassID] from " + Pre + "Collect_News where ChannelID='" + Current.SiteID + "' and ID=" + id;
            IDataReader rd = DbHelper.ExecuteReader(Database.CollectConnectionString, CommandType.Text, Sql, null);
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
            }
            rd.Close();
            return info;
        }
        public void NewsUpdate(int id, CollectNewsInfo info)
        {
            OleDbParameter[] parm = Database.getNewParam(GetNewsParams(info), "Title,Links,SiteID,Author,Source,AddDate,Content,ClassID");
            string Sql = "update " + Pre + "Collect_News set [Title]=@Title,[Links]=@Links,[SiteID]=@SiteID,[Author]=@Author";
            Sql += ",[Source]=@Source,[AddDate]=@AddDate,[Content]=@Content,[ClassID]=@ClassID where ChannelID='" + Current.SiteID + "' and ID=" + id;
            DbHelper.ExecuteNonQuery(Database.CollectConnectionString, CommandType.Text, Sql, parm);
        }
        private OleDbParameter[] GetNewsParams(CollectNewsInfo info)
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
            OleDbParameter[] param = new OleDbParameter[8];
            param[0] = new OleDbParameter("@Title", OleDbType.VarWChar, 100);
            param[0].Value = info.Title;
            param[1] = new OleDbParameter("@Links", OleDbType.VarWChar, 200);
            param[1].Value = info.Links;
            param[2] = new OleDbParameter("@Author", OleDbType.VarWChar, 100);
            param[2].Value = info.Author.Trim().Equals("") ? DBNull.Value : (object)info.Author;
            param[3] = new OleDbParameter("@Source", OleDbType.VarWChar, 100);
            param[3].Value = info.Source.Trim().Equals("") ? DBNull.Value : (object)info.Source;
            param[4] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[4].Value = info.Content;
            param[5] = new OleDbParameter("@AddDate", OleDbType.Date);
            param[5].Value = info.AddDate.Year < 1753 ? DBNull.Value : (object)info.AddDate;
            param[6] = new OleDbParameter("@SiteID", OleDbType.Integer);
            param[6].Value = info.SiteID;
            param[7] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
            param[7].Value = info.ClassID;
            return param;
        }
        #region 新闻入库
        ///////////////////////////新闻入库专用///////////////////////////////////////
        private OleDbConnection connetion;
        private OleDbConnection confoosun;
        private int nStoreSucceed = 0;
        private int nStoreFailed = 0;
        public void StoreNews(bool UnStore, int[] id, out int nSucceed, out int nFailed)
        {
            nSucceed = 0;
            nFailed = 0;
            connetion = new OleDbConnection(Database.CollectConnectionString);
            confoosun = new OleDbConnection(Database.FoosunConnectionString);
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
                    if (r["AddDate"] != DBNull.Value) dtAddDate = (DateTime)r["AddDate"];
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
                        #region SQL参数赋值
                        OleDbParameter[] param = new OleDbParameter[27];
                        param[0] = new OleDbParameter("@NewsID", OleDbType.VarWChar, 13);
                        param[0].Value = NewsID;
                        param[1] = new OleDbParameter("@NewsType", OleDbType.Integer);
                        param[1].Value = 0;
                        param[2] = new OleDbParameter("@OrderID", OleDbType.Integer);
                        param[2].Value = 0;
                        param[3] = new OleDbParameter("@NewsTitle", OleDbType.VarWChar, 100);
                        param[3].Value = sTitle;
                        param[4] = new OleDbParameter("@URLaddress", OleDbType.VarWChar, 200);
                        param[4].Value = sLinks;
                        param[5] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
                        param[5].Value = sClass;
                        param[6] = new OleDbParameter("@Author", OleDbType.VarWChar, 100);
                        param[6].Value = sAuthor;
                        param[7] = new OleDbParameter("@Souce", OleDbType.VarWChar, 100);
                        param[7].Value = sSource;
                        param[8] = new OleDbParameter("@Templet", OleDbType.VarWChar, 200);
                        if (Temp == string.Empty)
                            param[8].Value = DBNull.Value;
                        else
                            param[8].Value = Temp;
                        param[9] = new OleDbParameter("@Content", OleDbType.VarWChar);
                        param[9].Value = sContent;
                        param[10] = new OleDbParameter("@CreatTime", OleDbType.Date);
                        param[10].Value = dtAddDate;
                        param[11] = new OleDbParameter("@IsLock", OleDbType.Integer);
                        param[11].Value = IsLock;
                        param[12] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
                        param[12].Value = Current.SiteID;
                        param[13] = new OleDbParameter("@Editor", OleDbType.VarWChar, 18);
                        param[13].Value = Foosun.Global.Current.UserName;
                        param[14] = new OleDbParameter("@CheckStat", OleDbType.VarWChar, 10);
                        param[14].Value = CheckSate;
                        param[15] = new OleDbParameter("@NewsProperty", OleDbType.VarWChar, 30);
                        param[15].Value = "0,0,0,0,0,0,0,0";
                        param[16] = new OleDbParameter("@FileName", OleDbType.VarWChar, 100);
                        param[16].Value = FileName;
                        param[17] = new OleDbParameter("@FileEXName", OleDbType.VarWChar, 6);
                        param[17].Value = FileExName;
                        param[18] = new OleDbParameter("@isDelPoint", OleDbType.Integer);
                        param[18].Value = isDelPoint;
                        param[19] = new OleDbParameter("@Gpoint", OleDbType.Integer);
                        param[19].Value = Gpoint;
                        param[20] = new OleDbParameter("@iPoint", OleDbType.Integer);
                        param[20].Value = iPoint;
                        param[21] = new OleDbParameter("@GroupNumber", OleDbType.VarWChar);
                        param[21].Value = GroupNumber;
                        param[22] = new OleDbParameter("@DataLib", OleDbType.VarWChar, 20);
                        param[22].Value = DataLib;
                        param[23] = new OleDbParameter("@SavePath", OleDbType.VarWChar, 200);
                        if (SavePath == string.Empty)
                            param[23].Value = DBNull.Value;
                        else
                            param[23].Value = SavePath;
                        param[24] = new OleDbParameter("@TitleITF", OleDbType.Integer);
                        param[24].Value = 0;
                        param[25] = new OleDbParameter("@NewsPicTopline", OleDbType.Integer);
                        param[25].Value = 0;
                        param[26] = new OleDbParameter("@Click", OleDbType.Integer);
                        param[26].Value = 0;
                        #endregion SQL参数赋值
                    #endregion 取新闻的默认值选项
                        //[ContentPicTF],[CommTF],[DiscussTF],[TopNum],[VoteTF],[isRecyle],[isHtml],[isConstr],[isVoteTF]
                        Sql = "insert into " + Pre + "news (" + Database.getParam(param) + ",[ContentPicTF],[CommTF],[DiscussTF],[TopNum],[VoteTF],[isRecyle],[isHtml],[isConstr],[isVoteTF]";
                        Sql += ") values (" + Database.getAParam(param) + "";
                        Sql += ",0,1,0,0,0,0,0,0,0)";
                        //'" + FileName + "','.html',0,0,0,0,1,1,0,1,0,'FS_News',0,0,0)";

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
