using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Publish : DbBase, IPublish, IDisposable
    {
        public IDataReader GetSysParam()
        {
            string Sql = "select top 1 LinkType,SiteDomain,SaveIndexPage,SiteName,CopyRight from " + Pre + "sys_param order by id desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        public IList<PubClassInfo> GetClassList()
        {
            IList<PubClassInfo> list = new List<PubClassInfo>();
            string Sql = "select Id,ClassID,ClassCName,ClassEName,ParentID,[Domain],IsURL,URLaddress,ClassTemplet,SavePath,SaveClassframe,ClassSaveRule";
            Sql += ",ClassIndexRule,SiteID,NaviPIC,NaviContent,MetaKeywords,MetaDescript,isDelPoint,Gpoint,iPoint,GroupNumber,NaviShowtf,NaviPosition,NewsPosition,isPage";
            //wjl 2008-07-23 栏目导航问题
            Sql += " from " + Pre + "news_Class where isRecyle=0 and isLock=0 and NaviShowtf = 1  order by OrderID desc,id desc";
            //--wjl>
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                PubClassInfo info = new PubClassInfo();
                info.Id = (int)rd["Id"];
                info.ClassID = rd["ClassID"].ToString();
                if (rd["ClassCName"] != DBNull.Value) info.ClassCName = rd["ClassCName"].ToString();
                if (rd["ClassEName"] != DBNull.Value) info.ClassEName = rd["ClassEName"].ToString();
                if (rd["ParentID"] != DBNull.Value) info.ParentID = rd["ParentID"].ToString();
                if (rd["IsURL"] != DBNull.Value) info.IsURL = Convert.ToInt32(rd["IsURL"].ToString());
                if (rd["URLaddress"] != DBNull.Value) info.URLaddress = rd["URLaddress"].ToString();
                if (rd["ClassTemplet"] != DBNull.Value) info.ClassTemplet = rd["ClassTemplet"].ToString();
                if (rd["SavePath"] != DBNull.Value) info.SavePath = rd["SavePath"].ToString();
                if (rd["Domain"] != DBNull.Value) info.Domain = rd["Domain"].ToString();
                if (rd["SaveClassframe"] != DBNull.Value) info.SaveClassframe = rd["SaveClassframe"].ToString();
                if (rd["ClassSaveRule"] != DBNull.Value) info.ClassSaveRule = rd["ClassSaveRule"].ToString();
                if (rd["ClassIndexRule"] != DBNull.Value) info.ClassIndexRule = rd["ClassIndexRule"].ToString();
                if (rd["SiteID"] != DBNull.Value) info.SiteID = rd["SiteID"].ToString();
                if (rd["NaviPIC"] != DBNull.Value) info.NaviPIC = rd["NaviPIC"].ToString();
                if (rd["NaviContent"] != DBNull.Value) info.NaviContent = rd["NaviContent"].ToString();
                if (rd["MetaKeywords"] != DBNull.Value) info.MetaKeywords = rd["MetaKeywords"].ToString();
                if (rd["MetaDescript"] != DBNull.Value) info.MetaDescript = rd["MetaDescript"].ToString();
                if (rd["isDelPoint"] != DBNull.Value) info.isDelPoint = Convert.ToInt32(rd["isDelPoint"].ToString());
                if (rd["Gpoint"] != DBNull.Value) info.Gpoint = Convert.ToInt32(rd["Gpoint"].ToString());
                if (rd["iPoint"] != DBNull.Value) info.iPoint = Convert.ToInt32(rd["iPoint"].ToString());
                if (rd["GroupNumber"] != DBNull.Value) info.GroupNumber = rd["GroupNumber"].ToString();
                if (rd["NaviPosition"] != DBNull.Value) info.NaviPosition = rd["NaviPosition"].ToString();
                if (rd["NewsPosition"] != DBNull.Value) info.NewsPosition = rd["NewsPosition"].ToString();
                if (rd["isPage"] != DBNull.Value) info.isPage = Convert.ToInt32(rd["isPage"].ToString());
                if (rd["NaviShowtf"] != DBNull.Value) info.NaviShowtf = Convert.ToInt32(rd["NaviShowtf"].ToString());
                list.Add(info);
            }
            rd.Close();
            return list;
        }
        /// <summary>
        /// 获取某个栏目的信息
        /// </summary>
        /// <param name="ClassID">栏目编号</param>
        /// <returns>栏目的信息</returns>
        public PubClassInfo GetClassById(string ClassID)
        {
            try
            {
                string Sql = "select Id,ClassID,ClassCName,ClassEName,ParentID,[Domain],IsURL,URLaddress,ClassTemplet,SavePath,SaveClassframe,ClassSaveRule";
                Sql += ",ClassIndexRule,SiteID,NaviPIC,NaviContent,MetaKeywords,MetaDescript,isDelPoint,Gpoint,iPoint,GroupNumber,NaviShowtf,NaviPosition,NewsPosition,isPage";
                Sql += " from " + Pre + "news_Class where isRecyle=0 and isLock=0 and ClassID='" + ClassID + "'";
                IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);

                PubClassInfo info = new PubClassInfo();
                if (rd.Read())
                {
                    info.Id = (int)rd["Id"];
                    info.ClassID = rd["ClassID"].ToString();
                    if (rd["ClassCName"] != DBNull.Value) info.ClassCName = rd["ClassCName"].ToString();
                    if (rd["ClassEName"] != DBNull.Value) info.ClassEName = rd["ClassEName"].ToString();
                    if (rd["ParentID"] != DBNull.Value) info.ParentID = rd["ParentID"].ToString();
                    if (rd["IsURL"] != DBNull.Value) info.IsURL = Convert.ToInt32(rd["IsURL"].ToString());
                    if (rd["URLaddress"] != DBNull.Value) info.URLaddress = rd["URLaddress"].ToString();
                    if (rd["ClassTemplet"] != DBNull.Value) info.ClassTemplet = rd["ClassTemplet"].ToString();
                    if (rd["SavePath"] != DBNull.Value) info.SavePath = rd["SavePath"].ToString();
                    if (rd["Domain"] != DBNull.Value) info.Domain = rd["Domain"].ToString();
                    if (rd["SaveClassframe"] != DBNull.Value) info.SaveClassframe = rd["SaveClassframe"].ToString();
                    if (rd["ClassSaveRule"] != DBNull.Value) info.ClassSaveRule = rd["ClassSaveRule"].ToString();
                    if (rd["ClassIndexRule"] != DBNull.Value) info.ClassIndexRule = rd["ClassIndexRule"].ToString();
                    if (rd["SiteID"] != DBNull.Value) info.SiteID = rd["SiteID"].ToString();
                    if (rd["NaviPIC"] != DBNull.Value) info.NaviPIC = rd["NaviPIC"].ToString();
                    if (rd["NaviContent"] != DBNull.Value) info.NaviContent = rd["NaviContent"].ToString();
                    if (rd["MetaKeywords"] != DBNull.Value) info.MetaKeywords = rd["MetaKeywords"].ToString();
                    if (rd["MetaDescript"] != DBNull.Value) info.MetaDescript = rd["MetaDescript"].ToString();
                    if (rd["isDelPoint"] != DBNull.Value) info.isDelPoint = Convert.ToInt32(rd["isDelPoint"].ToString());
                    if (rd["Gpoint"] != DBNull.Value) info.Gpoint = Convert.ToInt32(rd["Gpoint"].ToString());
                    if (rd["iPoint"] != DBNull.Value) info.iPoint = Convert.ToInt32(rd["iPoint"].ToString());
                    if (rd["GroupNumber"] != DBNull.Value) info.GroupNumber = rd["GroupNumber"].ToString();
                    if (rd["NaviPosition"] != DBNull.Value) info.NaviPosition = rd["NaviPosition"].ToString();
                    if (rd["NewsPosition"] != DBNull.Value) info.NewsPosition = rd["NewsPosition"].ToString();
                    if (rd["isPage"] != DBNull.Value) info.isPage = Convert.ToInt32(rd["isPage"].ToString());
                    if (rd["NaviShowtf"] != DBNull.Value) info.NaviShowtf = Convert.ToInt32(rd["NaviShowtf"].ToString());
                }
                rd.Close();
                return info;
            }
            catch
            {
                return null;
            }
        }
        public IList<PubCHClassInfo> GetCHClassList()
        {
            IList<PubCHClassInfo> list = new List<PubCHClassInfo>();
            string Sql = "select Id,ClassCName,ClassEName,ParentID,Templet,SavePath,FileName";
            Sql += ",ChID,PicURL,NaviContent,KeyMeta,DescMeta,isDelPoint,Gpoint,iPoint,GroupNumber,ClassNavi,ContentNavi,isPage";
            Sql += " from " + Pre + "sys_channelclass where isLock=0";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                PubCHClassInfo info = new PubCHClassInfo();
                info.Id = (int)rd["Id"];
                if (rd["classCName"] != DBNull.Value) info.classCName = rd["classCName"].ToString();
                if (rd["classEName"] != DBNull.Value) info.classEName = rd["classEName"].ToString();
                if (rd["ParentID"] != DBNull.Value) info.ParentID = Convert.ToInt32(rd["ParentID"].ToString());
                if (rd["Templet"] != DBNull.Value) info.Templet = rd["Templet"].ToString();
                if (rd["SavePath"] != DBNull.Value) info.SavePath = rd["SavePath"].ToString();
                if (rd["FileName"] != DBNull.Value) info.FileName = rd["FileName"].ToString();
                if (rd["ChID"] != DBNull.Value) info.ChID = Convert.ToInt32(rd["ChID"].ToString());
                if (rd["PicURL"] != DBNull.Value) info.PicURL = rd["PicURL"].ToString();
                if (rd["NaviContent"] != DBNull.Value) info.NaviContent = rd["NaviContent"].ToString();
                if (rd["KeyMeta"] != DBNull.Value) info.MetaKeywords = rd["KeyMeta"].ToString();
                if (rd["DescMeta"] != DBNull.Value) info.MetaDescript = rd["DescMeta"].ToString();
                if (rd["isDelPoint"] != DBNull.Value) info.isDelPoint = Convert.ToInt32(rd["isDelPoint"].ToString());
                if (rd["Gpoint"] != DBNull.Value) info.Gpoint = Convert.ToInt32(rd["Gpoint"].ToString());
                if (rd["iPoint"] != DBNull.Value) info.iPoint = Convert.ToInt32(rd["iPoint"].ToString());
                if (rd["GroupNumber"] != DBNull.Value) info.GroupNumber = rd["GroupNumber"].ToString();
                if (rd["ClassNavi"] != DBNull.Value) info.ClassNavi = rd["ClassNavi"].ToString();
                if (rd["ContentNavi"] != DBNull.Value) info.ContentNavi = rd["ContentNavi"].ToString();
                if (rd["isPage"] != DBNull.Value) info.isPage = Convert.ToInt32(rd["isPage"].ToString());
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        public IList<PubSpecialInfo> GetSpecialList()
        {
            IList<PubSpecialInfo> list = new List<PubSpecialInfo>();
            string Sql = "select Id,SpecialID,SpecialCName,specialEName,ParentID,isDelPoint,saveDirPath,SavePath,FileName,FileEXName,NaviPicURL,";
            Sql += "NaviContent,SiteID,Templet,NaviPosition,Gpoint,iPoint,GroupNumber";
            Sql += " from " + Pre + "news_special where isRecyle=0 and isLock=0";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                PubSpecialInfo info = new PubSpecialInfo();
                info.Id = (int)rd["Id"];
                info.SpecialID = rd["SpecialID"].ToString();
                if (rd["SpecialCName"] != DBNull.Value) info.SpecialCName = rd["SpecialCName"].ToString();
                if (rd["specialEName"] != DBNull.Value) info.specialEName = rd["specialEName"].ToString();
                if (rd["ParentID"] != DBNull.Value) info.ParentID = rd["ParentID"].ToString();
                if (rd["isDelPoint"] != DBNull.Value) info.isDelPoint = Convert.ToInt32(rd["isDelPoint"].ToString());
                if (rd["Gpoint"] != DBNull.Value) info.Gpoint = Convert.ToInt32(rd["Gpoint"].ToString());
                if (rd["iPoint"] != DBNull.Value) info.iPoint = Convert.ToInt32(rd["iPoint"].ToString());
                if (rd["GroupNumber"] != DBNull.Value) info.GroupNumber = rd["GroupNumber"].ToString();
                if (rd["saveDirPath"] != DBNull.Value) info.saveDirPath = rd["saveDirPath"].ToString();
                if (rd["SavePath"] != DBNull.Value) info.SavePath = rd["SavePath"].ToString();
                if (rd["FileName"] != DBNull.Value) info.FileName = rd["FileName"].ToString();
                if (rd["FileEXName"] != DBNull.Value) info.FileEXName = rd["FileEXName"].ToString();
                if (rd["NaviPicURL"] != DBNull.Value) info.NaviPicURL = rd["NaviPicURL"].ToString();
                if (rd["NaviContent"] != DBNull.Value) info.NaviContent = rd["NaviContent"].ToString();
                if (rd["SiteID"] != DBNull.Value) info.SiteID = rd["SiteID"].ToString();
                if (rd["Templet"] != DBNull.Value) info.Templet = rd["Templet"].ToString();
                if (rd["NaviPosition"] != DBNull.Value) info.NaviPosition = rd["NaviPosition"].ToString();
                list.Add(info);
            }
            rd.Close();
            return list;
        }
        /// <summary>
        /// 取得专题信息
        /// </summary>
        /// <param name="specialID">专题ID</param>
        /// <returns></returns>
        public PubSpecialInfo GetSpecial(string specialID)
        {
            try
            {
                string Sql = "select Id,SpecialID,SpecialCName,specialEName,ParentID,isDelPoint,saveDirPath,SavePath,FileName,FileEXName,NaviPicURL,";
                Sql += "NaviContent,SiteID,Templet,NaviPosition,Gpoint,iPoint,GroupNumber";
                Sql += " from " + Pre + "news_special where isRecyle=0 and isLock=0 and SpecialID='" + specialID + "'";
                PubSpecialInfo info = new PubSpecialInfo();
                IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
                if (rd.Read())
                {
                    info.Id = (int)rd["Id"];
                    info.SpecialID = rd["SpecialID"].ToString();
                    if (rd["SpecialCName"] != DBNull.Value) info.SpecialCName = rd["SpecialCName"].ToString();
                    if (rd["specialEName"] != DBNull.Value) info.specialEName = rd["specialEName"].ToString();
                    if (rd["ParentID"] != DBNull.Value) info.ParentID = rd["ParentID"].ToString();
                    if (rd["isDelPoint"] != DBNull.Value) info.isDelPoint = Convert.ToInt32(rd["isDelPoint"].ToString());
                    if (rd["Gpoint"] != DBNull.Value) info.Gpoint = Convert.ToInt32(rd["Gpoint"].ToString());
                    if (rd["iPoint"] != DBNull.Value) info.iPoint = Convert.ToInt32(rd["iPoint"].ToString());
                    if (rd["GroupNumber"] != DBNull.Value) info.GroupNumber = rd["GroupNumber"].ToString();
                    if (rd["saveDirPath"] != DBNull.Value) info.saveDirPath = rd["saveDirPath"].ToString();
                    if (rd["SavePath"] != DBNull.Value) info.SavePath = rd["SavePath"].ToString();
                    if (rd["FileName"] != DBNull.Value) info.FileName = rd["FileName"].ToString();
                    if (rd["FileEXName"] != DBNull.Value) info.FileEXName = rd["FileEXName"].ToString();
                    if (rd["NaviPicURL"] != DBNull.Value) info.NaviPicURL = rd["NaviPicURL"].ToString();
                    if (rd["NaviContent"] != DBNull.Value) info.NaviContent = rd["NaviContent"].ToString();
                    if (rd["SiteID"] != DBNull.Value) info.SiteID = rd["SiteID"].ToString();
                    if (rd["Templet"] != DBNull.Value) info.Templet = rd["Templet"].ToString();
                    if (rd["NaviPosition"] != DBNull.Value) info.NaviPosition = rd["NaviPosition"].ToString();
                }
                rd.Close();
                return info;
            }
            catch
            {
                return null;
            }

        }

        public IList<PubCHSpecialInfo> GetCHSpecialList()
        {
            IList<PubCHSpecialInfo> list = new List<PubCHSpecialInfo>();
            string Sql = "select Id,ChID,specialCName,specialEName,ParentID,binddomain,navicontent,savePath,filename,templet,";
            Sql += "islock,isRec,PicURL,OrderID,SiteID";
            Sql += " from " + Pre + "sys_channelspecial where isLock=0";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            while (rd.Read())
            {
                PubCHSpecialInfo info = new PubCHSpecialInfo();
                info.Id = (int)rd["Id"];
                info.ChID = Convert.ToInt32(rd["ChID"].ToString());
                if (rd["specialCName"] != DBNull.Value) info.specialCName = rd["specialCName"].ToString();
                if (rd["specialEName"] != DBNull.Value) info.specialEName = rd["specialEName"].ToString();
                if (rd["ParentID"] != DBNull.Value) info.ParentID =  Convert.ToInt32(rd["ParentID"].ToString());
                if (rd["binddomain"] != DBNull.Value) info.binddomain = rd["binddomain"].ToString();
                if (rd["navicontent"] != DBNull.Value) info.navicontent = rd["navicontent"].ToString();
                if (rd["savePath"] != DBNull.Value) info.savePath = rd["savePath"].ToString();
                if (rd["filename"] != DBNull.Value) info.filename = rd["filename"].ToString();
                if (rd["templet"] != DBNull.Value) info.templet = rd["templet"].ToString();
                if (rd["islock"] != DBNull.Value) info.islock = Convert.ToInt32(rd["islock"].ToString());
                if (rd["isRec"] != DBNull.Value) info.isRec = Convert.ToInt32(rd["isRec"].ToString());
                if (rd["PicURL"] != DBNull.Value) info.PicURL = rd["PicURL"].ToString();
                if (rd["OrderID"] != DBNull.Value) info.OrderID = Convert.ToInt32(rd["OrderID"].ToString());
                if (rd["SiteID"] != DBNull.Value) info.SiteID = rd["SiteID"].ToString();
                list.Add(info);
            }
            rd.Close();
            return list;
        }

        public DataTable GetLastNews(int topnum, string classid)
        {
            OleDbParameter Param = null;
            string Sql = "select top " + topnum + " a.NewsTitle,a.sNewsTitle,a.URLaddress,a.Content,a.CreatTime,a.SavePath,a.FileName,a.FileEXName,";
            Sql += "b.savepath as savepath1,b.SaveClassframe,b.ClassEName,b.ClassCName,b.ClassSaveRule from " + Pre + "news a," + Pre + "news_class b";
            Sql += " where a.islock=0  and a.ClassID=b.ClassID and a.isRecyle=0 and b.isPage<>1 and b.islock=0";
            if (classid != "0")
            {
                Sql += " and b.ClassID=@ClassID";
                Param = new OleDbParameter("@ClassID", classid);
            }
            Sql += " order by a.id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, Param);
        }
        public DataTable GetTodayNews(string siteid, string classid)
        {
            string Sql = "select NewsType,NewsTitle,URLaddress,SavePath,FileName,FileEXName from " + Pre + "news where ClassID=@ClassID";
            Sql += " And DateDiff('d',[creatTime] ,Now()) = 0 and islock=0 and isRecyle=0 and SiteID=@SiteID order by id desc";
            OleDbParameter[] Param = new OleDbParameter[]{
                new OleDbParameter("@ClassID",classid),
            new OleDbParameter("@SiteID",siteid)};
            return DbHelper.ExecuteTable(CommandType.Text, Sql, Param);
        }
        public IDataReader GetSinglePageClass(string classid)
        {
            string Sql = "select ClassTemplet,ClassCName,SavePath,PageContent,MetaKeywords,MetaDescript from " + Pre + "news_class";
            Sql += " where ClassID=@ClassID and isLock=0 and isRecyle=0 and isPage=1";
            OleDbParameter Param = new OleDbParameter("@ClassID", classid);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }
        public IDataReader GetNewsSavePath(string newsid)
        {
            string Sql = "select a.templet,a.classid,a.datalib,a.SavePath,a.FileName,a.FileEXName,b.SavePath as SavePath1,b.SaveClassframe,a.NewsID,a.isDelPoint from " + Pre + "news a, ";
            Sql += Pre + "news_class b where a.classid=b.classid and a.newsid=@newsid";
            OleDbParameter Param = new OleDbParameter("@newsid", newsid);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }
        public string GetSysLabelContent(string labelname)
        {
            string Sql = "select Label_Content from " + Pre + "sys_Label where Label_Name=@Label_Name and isBack=0 and isRecyle=0";
            OleDbParameter Param = new OleDbParameter("@Label_Name", labelname);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }

        /// <summary>
        ///从频道标签库中获取数据
        /// </summary>
        /// <param name="labelname"></param>
        /// <returns></returns>
        public string GetChannelSysLabelContent(string labelname)
        {
            string Sql = "select LabelContent from " + Pre + "sys_channellabel where LabelName=@LabelName and isLock=0";
            OleDbParameter Param = new OleDbParameter("@LabelName", labelname);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }

        public IDataReader GetFreeLabelContent(string labelname)
        {
            string Sql = "select LabelSQL,StyleContent from " + Pre + "sys_LabelFree where LabelName=@LabelName";
            OleDbParameter Param = new OleDbParameter("@LabelName", labelname);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }

        public DataTable ExecuteSql(string sql)
        {
            return DbHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public IDataReader GetTemplatePath()
        {
            string Sql = "select IndexTemplet,IndexFileName,ReadNewsTemplet,ClassListTemplet,SpecialTemplet from " + Pre + "sys_param";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        #region 选择发布选项部分
        public IDataReader GetPublishSpecial(string spid, out int ncount)
        {
            string SqlCondition = " where isLock=0 and isRecyle=0";
            if (spid != null && spid.Trim() != "")
            {
                SqlCondition = " where specialID in (" + spid + ")";
            }
            string SqlCount = "select count(id) from " + Pre + "news_special" + SqlCondition;
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            string Sql = "select specialID,Templet,SavePath,saveDirPath,FileName,FileEXName from " + Pre + "news_special" + SqlCondition;
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        public IDataReader GetPublishClass(string siteid, string classid, bool isflag, out int ncount)
        {
            string SqlCondition = "";
            if (classid.Trim() == "")
            {
                if (isflag)
                {
                    SqlCondition = " where isunHTML <>1 and isPage<>1 and isLock=0 and isRecyle=0 and IsURL=0 and SiteID='" + siteid + "'";
                }
                else
                {
                    SqlCondition = " where isLock=0 and isRecyle=0 and IsURL=0 and isPage<>1 and isLock=0 and SiteID='" + siteid + "'";
                }
            }
            else
            {
                SqlCondition = " where classID in (" + classid + ")";
            }
            string SqlCount = "select count(id) from " + Pre + "news_class" + SqlCondition;
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            string Sql = "select Datalib,classtemplet,classid,SavePath,SaveClassframe,ClassSaveRule from " + Pre + "news_class" + SqlCondition;
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        #region 发布新闻
        /// <summary>
        /// 选择发布所有新闻时，取得所有新闻
        /// </summary> 
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        public IDataReader GetPublishNewsAll(out int ncount)
        {
            string TopParam = "";
            int refreshNum = int.Parse(Foosun.Common.Public.readparamConfig("infoNumber", "refresh"));
            if (refreshNum != 0)
            {
                TopParam = "top " + Foosun.Common.Public.readparamConfig("infoNumber", "refresh") + "";
            }
            string Sql = "select " + TopParam + " NewsID,templet,datalib,classID,SavePath,FileName,FileEXName from " + Pre + "news where isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2";
            string SqlCount = "select count(ID) from " + Pre + "news where isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2";
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            if (refreshNum != 0)
            {
                if (refreshNum < ncount)
                {
                    ncount = refreshNum;
                }
            }
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 选择发布最新时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        public IDataReader GetPublishNewsLast(int topnum, bool unpublish, out int ncount)
        {
            string Sql = "select top " + topnum + " NewsID,templet,datalib,classID,SavePath,FileName,FileEXName from " + Pre + "news where isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2";
            string SqlCount = "select count(ID) from " + Pre + "news where isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2";
            if (unpublish)
            {
                Sql += " and isHtml=0";
                SqlCount += " and isHtml=0";
            }
            Sql += " order by id desc";
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        #region 根据栏目选择发布的新闻
        /// <summary>
        /// 根据栏目发布新闻
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="unpublish"></param>
        /// <param name="isdesc"></param>
        /// <param name="condition"></param>
        /// <param name="ncount"></param>
        /// <returns></returns>
        public IDataReader GetPublishNewsByClass(string classid, bool unpublish, bool isdesc, string condition, out int ncount)
        {
            string SqlCondition = " where isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2 and ClassID in (" + classid + ")";
            string SqlOrder = "";
            if (unpublish)
            {
                //只发布未发布的
                SqlCondition += " and isHtml=0 ";
            }
            switch (condition)
            {
                #region 条件判断
                case "0":
                    SqlOrder = " order by newsID ";
                    break;
                case "1":
                    SqlOrder = " order by Click ";
                    break;
                case "2":
                    SqlOrder = " order by OrderID ";
                    break;
                case "3":
                    SqlOrder = " order by CreatTime ";
                    break;
                case "4":
                    SqlCondition += " and Mid(NewsProperty,1,1)='1'";
                    SqlOrder = " order by newsID ";
                    break;
                case "5":
                    SqlCondition += " and Mid(NewsProperty,3,1)='1'";
                    SqlOrder = " order by newsID ";
                    break;
                case "6":
                    SqlCondition += " and Mid(NewsProperty,5,1)='1'";
                    SqlOrder = " order by newsID ";
                    break;
                case "7":
                    SqlCondition += " and Mid(NewsProperty,7,1)='1'";
                    SqlOrder = " order by newsID ";
                    break;
                case "8":
                    SqlCondition += " and Mid(NewsProperty,9,1)='1'";
                    SqlOrder = " order by newsID ";
                    break;
                case "9":
                    SqlCondition += " and Mid(NewsProperty,11,1)='1'";
                    SqlOrder = " order by newsID ";
                    break;
                case "10":
                    SqlCondition += " and Mid(NewsProperty,15,1)='1'";
                    SqlOrder = " order by newsID ";
                    break;
                case "11":
                    SqlCondition += " and author<>''";
                    SqlOrder = " order by newsID ";
                    break;
                case "12":
                    SqlCondition += " and Souce<>''";
                    SqlOrder = " order by newsID ";
                    break;
                case "13":
                    SqlCondition += " and tags<>''";
                    SqlOrder = " order by newsID ";
                    break;
                case "14":
                    SqlCondition += " and newstype=1";
                    SqlOrder = " order by newsID ";
                    break;
                case "15":
                    SqlCondition += " and isFiles=1";
                    SqlOrder = " order by newsID ";
                    break;
                case "16":
                    SqlCondition += " and vURL<>''";
                    SqlOrder = " order by newsID ";
                    break;
                case "17":
                    SqlCondition += " and ContentPicTF=1";
                    SqlOrder = " order by newsID ";
                    break;
                case "18":
                    SqlCondition += " and VoteTF=1";
                    SqlOrder = " order by newsID ";
                    break;
                case "19":
                    SqlCondition += " and (select count(b.id) from " + Pre + "api_commentary b where b.InfoID=a.newsid)>0";
                    SqlOrder = " order by newsID";
                    break;
                default:
                    break;
                #endregion 条件判断
            }
            if (isdesc)
            {
                //倒序
                SqlOrder += " desc ";
            }
            string Sql = "select newsid,templet,datalib,classID,SavePath,FileName,FileEXName from " + Pre + "news a " + SqlCondition + SqlOrder;
            string SqlCount = "select count(a.ID) from " + Pre + "news a " + SqlCondition;
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        #endregion 根据栏目选择发布的新闻
        /// <summary>
        /// 选择按照日期发布时，取得所有新闻
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="ncount"></param>
        /// <returns></returns>
        public IDataReader GetPublishNewsByTime(DateTime starttime, DateTime endtime, out int ncount)
        {
            string Sql = "select newsID,templet,datalib,classID,SavePath,FileName,FileEXName from " + Pre + "news where creattime between '" + starttime + "' and '" + endtime + "' and isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2";
            string SqlCount = "select count(ID) from " + Pre + "news where creattime between '" + starttime + "' and '" + endtime + "' and isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2";
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 选择按照ID发布时，取得所有新闻
        /// </summary>
        /// <param name="minid"></param>
        /// <param name="maxid"></param>
        /// <param name="ncount"></param>
        /// <returns></returns>
        public IDataReader GetPublishNewsByID(int minid, int maxid, out int ncount)
        {
            string Sql = "select newsid,templet,datalib,classID,SavePath,FileName,FileEXName from " + Pre + "news where isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2 and id between " + minid + " and " + maxid;
            string SqlCount = "select count(id) from " + Pre + "news where isRecyle=0 and isLock=0 and isDelPoint=0 and NewsType<>2 and id between " + minid + " and " + maxid;
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }
        #endregion 发布新闻
        #endregion 选择发布选项部分
        /*
        public string GetClassIDByAdID(string adid)
        {
            string Sql = "Select [ClassID] From [" + Pre + "ads] Where [AdID]=@AdID";
            OleDbParameter Param = new OleDbParameter("@AdID", adid);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }*/
        public IDataReader GetJsPath(string jsid)
        {
            string Sql = "Select [jssavepath],[jsfilename] From [" + Pre + "news_js] Where [JsID]=@JsID";
            OleDbParameter Param = new OleDbParameter("@JsID", jsid);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }

        public IDataReader GetsClassInfo(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string sql = "select ID,isDelPoint,ClassID,SavePath,SaveClassFrame,ClassSaveRule,[Domain] from " + Pre + "news_class where ClassID=@ClassID and islock=0 and isRecyle=0";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public DataTable GetSysUser(int topnum)
        {
            string Sql = "Select Top " + topnum + " [UserName],[RegTime] From [" + Pre + "sys_User] Where [isLock]=0 Order By [RegTime] Desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable GetApiComm(int LoopNumber)
        {
            string Sql = "Select top " + LoopNumber + " [InfoID],[Commid],[Content],[creatTime],[DataLib] From [" + Pre + "API_commentary] Where [isRecyle]=0 And [islock]=0 And [isCheck]=0 Order By [creatTime] Desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public string GetNewsTag(string newsid)
        {
            string Sql = "Select [Tags] From [" + Pre + "News] Where [NewsID]=@NewsID";
            OleDbParameter Param = new OleDbParameter("@NewsID", newsid);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }
        public void UpdateNewsIsHtml(string tablename, string ishtml, string idfield, IList<string> succeedlist)
        {
            OleDbParameter[] sqlParams = new OleDbParameter[4];
            sqlParams[0] = new OleDbParameter("@tableName", OleDbType.VarChar, 30);
            sqlParams[0].Value = tablename;
            sqlParams[1] = new OleDbParameter("@filedname", OleDbType.VarChar, 30);
            sqlParams[1].Value = ishtml;
            sqlParams[2] = new OleDbParameter("@idtype", OleDbType.VarChar, 30);
            sqlParams[2].Value = idfield;
            for (int i = 0; i < succeedlist.Count; i++)
            {
                sqlParams[3] = new OleDbParameter("@newsID", OleDbType.VarChar, 12);
                sqlParams[3].Value = succeedlist[i];
                DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "fs_publish_updateishtml", sqlParams);
            }
        }

        public void UpdateCHNewsIsHtml(string tablename, string ishtml, string idfield, IList<string> succeedlist)
        {
            OleDbParameter[] sqlParams = new OleDbParameter[4];
            sqlParams[0] = new OleDbParameter("@tableName", OleDbType.VarChar, 30);
            sqlParams[0].Value = tablename;
            sqlParams[1] = new OleDbParameter("@filedname", OleDbType.VarChar, 30);
            sqlParams[1].Value = ishtml;
            sqlParams[2] = new OleDbParameter("@idtype", OleDbType.VarChar, 30);
            sqlParams[2].Value = idfield;
            for (int i = 0; i < succeedlist.Count; i++)
            {
                sqlParams[3] = new OleDbParameter("@ID", OleDbType.Integer, 4);
                sqlParams[3].Value = int.Parse(succeedlist[i]);
                DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "fs_publish_CHupdateishtml", sqlParams);
            }
        }

        public IDataReader GetDiscussInfo(string grouptype,int TopNumber)
        {
            //arjun  用查询的结果做排序在ACCESS中不支持,用临时表解决
            string Sql;
            //string tabStr = "";
            IDataReader returndb;
            //bool isCreateTable=false;
            //Random rnd = new Random();
            //tabStr = "tab" + rnd.Next(1000);
            switch (grouptype)
            {
                case "hot":
                    //isCreateTable = true;
                    Sql = "Select top " + TopNumber + " * from (select [DisID],[Cname],[Creatime],((Select Count([Id])  From [" + Pre + "User_DiscussMember] Where [" + Pre + "User_DiscussMember].[DisID]=[" + Pre + "User_Discuss].[DisID])+Browsenumber) As cnt1 From [" + Pre + "User_Discuss]) as t order by Cnt1 desc";
                    //DbHelper.ExecuteNonQuery(CommandType.Text, Sql,null);
                    //Sql = "select top " + TopNumber + " * from "+tabStr+" order by cnt1 desc" ;
                    break;
                case "click":
                    Sql = "Select top " + TopNumber + "  [DisID],[Cname],[Creatime],[Browsenumber] From [" + Pre + "User_Discuss] Order By [Browsenumber] Desc";
                    break;
                case "Mmore":
                    //isCreateTable = true;
                    Sql = "Select top " + TopNumber + " * from (select [DisID],[Cname],[Creatime],(Select Count([Id]) From [" + Pre + "User_DiscussMember] Where [" + Pre + "User_DiscussMember].[DisID]=[" + Pre + "User_Discuss].[DisID]) As Cnt1 From [" + Pre + "User_Discuss]) as t order by cnt1 desc";
                    //DbHelper.ExecuteNonQuery(CommandType.Text, Sql,null);
                    //Sql = "select top " + TopNumber + " * from "+tabStr+" order by cnt1 desc" ;
                    break;
                case "Last":
                    Sql = "Select top " + TopNumber + "  [DisID],[Cname],[Creatime] From [" + Pre + "User_Discuss] Order By [Creatime] Desc";
                    break;
                default:
                    Sql = "Select top " + TopNumber + "  [DisID],[Cname],[Creatime] From [" + Pre + "User_Discuss] Order By [Creatime] Desc";
                    break;
            }
            returndb = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            //if (isCreateTable)
            //{
            //    DbHelper.ExecuteNonQuery(CommandType.Text, "drop table " + tabStr,null);
            //}
            return returndb;
        }
        public string GetMetaContent(string id, string Str, int num)
        {
            string Sql = "";
            switch (Str)
            {
                case "News":
                    if (num == 0)
                        Sql = "select top 1 Metakeywords from " + Pre + "news where NewsID=@ID";
                    else
                        Sql = "select top 1 Metadesc from " + Pre + "news where NewsID=@ID";
                    break;
                case "Class":
                    if (num == 0)
                        Sql = "select top 1 MetaKeywords from " + Pre + "news_class where ClassID=@ID";
                    else
                        Sql = "select top 1 MetaDescript from " + Pre + "news_class where ClassID=@ID";
                    break;
                case "Special":
                    Sql = "select top 1 SpecialCName from " + Pre + "news_special where SpecialID=@ID";
                    break;
            }
            if (Sql != "")
            {
                OleDbParameter Param = new OleDbParameter("@ID", id);
                return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
            }
            else
                return "";
        }
        public string GetPageTitle(string id, string Str)
        {
            string Sql = "";
            switch (Str)
            {
                case "News":
                    Sql = "select top 1 NewsTitle from " + Pre + "news where NewsID=@ID";
                    break;
                case "Class":
                    Sql = "select top 1 ClassCName from " + Pre + "news_class where ClassID=@ID";
                    break;
                case "Special":
                    Sql = "select top 1 SpecialCName from " + Pre + "news_special where SpecialID=@ID";
                    break;
            }
            if (Sql != "")
            {
                OleDbParameter Param = new OleDbParameter("@ID", id);
                return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
            }
            else
                return "";
        }
        public IDataReader GetNewsFiles(string newsid)
        {
            string Sql = "select id,URLName,FileURL from " + Pre + "news_URL where [NewsID]=@NewsID order by orderid desc";
            OleDbParameter Param = new OleDbParameter("@NewsID", newsid);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }
        public IDataReader GetPrePage(int id, string datalib, int num, string classid, int ChID)
        {
            if (ChID == 0)
            {
                string Sql = "select top 1 a.newsID,a.NewsTitle,a.SavePath,a.FileName,a.FileEXName,b.savepath as savepath1,b.SaveClassframe,a.isDelPoint from " + Pre + "news a," + Pre + "news_class b where a.id";
                if (num == 0)
                    Sql += ">";
                else
                    Sql += "<";
                Sql += id + " and a.CLassID=@ClassID and a.ClassID=b.ClassID and a.NewsType<>2 and a.islock=0 and a.isRecyle=0 order by a.id";
                if (num == 0)
                    Sql += " asc";
                else
                    Sql += " desc";
                OleDbParameter Param = new OleDbParameter("@ClassID", classid);
                return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
            }
            else
            {
                string csql = "select top 1 a.id,a.Title,a.SavePath,a.FileName,b.SavePath as savepath1,a.isDelPoint from " + datalib + " a," + Pre + "sys_channelclass b where a.id";
                if (num == 0)
                    csql += ">";
                else
                    csql += "<";
                csql += id + " and a.CLassID=@ID and a.ClassID=b.id and a.islock=0 order by a.id";
                if (num == 0)
                    csql += " asc";
                else
                    csql += " desc";
                OleDbParameter Param1 = new OleDbParameter("@ID", int.Parse(classid));
                return DbHelper.ExecuteReader(CommandType.Text, csql, Param1);
            }
        }

        public IDataReader GetNewsInfoAndClassInfo(string NewsID, string DataLib)
        {
            string Sql = "select a.SavePath,a.FileName,a.FileEXName,a.NewsType,a.URLaddress,a.isDelPoint,b.SavePath as SavePath1,b.SaveClassframe from " + Pre + "news a," + Pre + "news_class b where a.NewsID=@NewsID and a.ClassID=b.ClassID and a.isLock=0 and a.isRecyle=0";
            OleDbParameter Param = new OleDbParameter("@NewsID", NewsID);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }

        public int GetCommCount(string newsid, int td,int ChID)
        {
            string WhereStr = string.Empty;
            if (ChID != 0)
            {
                WhereStr = " and ChID=" + ChID + "";
            }
            string Sql = "Select Count(ID) From [" + Pre + "api_commentary] Where [InfoID]=@NewsID and islock=0" + WhereStr;
            if (td == 1)
            {
                Sql += " And DateDiff('d',[creatTime] ,Now())=0";
            }
            OleDbParameter Param = new OleDbParameter("@NewsID", newsid);
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }
        public string GetStyleContent(string styleid)
        {
            string Sql = "select [Content] from " + Pre + "sys_LabelStyle where styleID=@styleID and isRecyle=0";
            OleDbParameter Param = new OleDbParameter("@styleID", styleid);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }

        public string GetCHStyleContent(int ID, int ChID)
        {
            string Sql = "select [styleContent] from " + Pre + "sys_channelstyle where id=@ID and isLock=0 and ChID=" + ChID + "";
            OleDbParameter Param = new OleDbParameter("@ID", ID);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }

        public IDataReader GetNaviShowClass(string parentid)
        {
            string Sql = "select [ClassID],[ClassCName],[ParentID],[ClassSaveRule],[SaveClassFrame],[SavePath],[isDelPoint],[Domain] from " + Pre + "News_Class where ParentID=@ParentID and isLock=0 and isRecyle=0 and NaviShowtf=1 order by orderid desc,id desc";
            OleDbParameter Param = new OleDbParameter("@ParentID", parentid);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }

        public DataTable Gethistory(int Numday)
        {
            string Sql = "select * from " + Pre + "old_news where DateDiff('d',[creatTime] ,Now()) = " + Numday + " and isLock=0 and isRecyle=0 order by orderid desc,id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable Gethistory(string Strday)
        {
            string Sql = "select * from " + Pre + "old_news where DateDiff('d',[creatTime],'" + Convert.ToDateTime(Strday) + "')=0  and isLock=0 and isRecyle=0 order by orderid desc,id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable GetTopLine(string newsid)
        {
            OleDbParameter Param = null;
            string Sql = "select top 1 NewsID,tl_SavePath from " + DBConfig.TableNamePrefix + "news_topline ";
            if (newsid != null && newsid != "")
            {
                Sql += " where NewsID=@NewsID";
                Param = new OleDbParameter("@NewsID", newsid);
            }
            else
            {
                Sql += " order by Id desc";
            }
            return DbHelper.ExecuteTable(CommandType.Text, Sql, Param);

        }

        public DataTable GetPosition(string ID, int Num)
        {
            OleDbParameter param = new OleDbParameter("@ID", ID);
            string sql = string.Empty;
            if (Num == 0)
            {
                sql = "select * from " + Pre + "news_class where ClassID=@ID";
            }
            else
            {
                sql = "select * from " + Pre + "news_special where SpecialID=@ID";
            }
            return DbHelper.ExecuteTable(CommandType.Text, sql, param);
        }



        public IDataReader GetNewsDetail(int id, string newsid)
        {
            OleDbParameter Param = null;
            string Sql = "Select [Id],[NewsID],[NewsType],[OrderID],[NewsTitle],[sNewsTitle],[TitleColor],[TitleITF],[TitleBTF],[CommLinkTF],";
            Sql += "[SubNewsTF],[URLaddress],[PicURL],[SPicURL],[ClassID],[SpecialID],[Author],[Souce],[Tags],[NewsProperty],[NewsPicTopline],";
            Sql += "[Templet],[Content],[Metakeywords],[Metadesc],[naviContent],[Click],[CreatTime],[EditTime],[SavePath],[FileName],";
            Sql += "[FileEXName],[isDelPoint],[Gpoint],[iPoint],[GroupNumber],[ContentPicTF],[ContentPicURL],[ContentPicSize],[CommTF],";
            Sql += "[DiscussTF],[TopNum],[VoteTF],[CheckStat],[isLock],[isRecyle],[SiteID],[DataLib],[DefineID],[isVoteTF],[Editor],[isHtml],";
            Sql += "[isConstr],[isFiles],[vURL] From [" + Pre + "News]";
            if (id > 0)
            {
                Sql += " Where ID=" + id;
            }
            else
            {
                Sql += " Where [NewsID]=@NewsID";
                Param = new OleDbParameter("@NewsID", newsid);
            }
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }

        public string GetClassIDByNewsID(string newsid)
        {
            string Sql = "Select [ClassID] From " + DBConfig.TableNamePrefix + "news Where [NewsID]=@NewsID";
            OleDbParameter Param = new OleDbParameter("@NewsID", newsid);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }

        public IDataReader GetTopUser(int topnum, string orderfld)
        {
            string Sql = "Select Top " + topnum + " [NickName],[UserName],[iPoint],[gPoint],[ePoint],[RegTime],(Select Count(*) From [" + Pre + "User_Constr] Where [" + Pre + "sys_User].UserNum=[" + Pre + "User_Constr].UserNum) Cnt From [" + Pre + "sys_User] Where [isLock]=0";
            switch (orderfld)
            {
                case "iPoint":
                    Sql += " Order By [iPoint] Desc,[ID] Desc";
                    break;
                case "gPoint":
                    Sql += " Order By [gPoint] Desc,[ID] Desc";
                    break;
                case "ePoint":
                    Sql += " Order By [ePoint] Desc,[ID] Desc";
                    break;
                case "Cnt":
                    Sql += "Order By [Cnt] Desc,[ID] Desc";
                    break;
                default:
                    Sql += " Order By [RegTime] Desc,[ID] Desc";
                    break;
            }
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        public DataTable GetUnRule(string UnID, string SiteID)
        {
            string Sql = string.Empty;
            if (UnID != "0")
            {
                Sql = "Select [unName],[TitleCSS],[SubCSS],[ONewsID],[Rows],[unTitle],[NewsTable] From [" + Pre + "news_unNews] Where [UnID]=@UnID And [SiteID]=@SiteID Order By [Rows] Asc,[ID] asc";
            }
            else
            {
                Sql = "Select [unName],[TitleCSS],[SubCSS],[ONewsID],[Rows],[unTitle],[NewsTable] From [" + Pre + "news_unNews] Where [UnID] in (select top 1 UnID from [" + Pre + "news_unNews] order by id desc) [SiteID]=@SiteID Order By [Rows] Asc,[ID] asc";
            }
            OleDbParameter[] Param = new OleDbParameter[] { new OleDbParameter("@UnID", UnID), new OleDbParameter("@SiteID", SiteID) };
            return DbHelper.ExecuteTable(CommandType.Text, Sql, Param);
        }

        public DataTable GetSubUnRule(string NewsID)
        {
            string Sql = "Select ID,getNewsID,NewsTitle,DataLib,TitleCSS,colsNum,SiteID,CreatTime From [" + Pre + "news_sub] Where [NewsID]=@NewsID Order By [colsNum] Asc,[ID] asc";
            OleDbParameter[] Param = new OleDbParameter[] { new OleDbParameter("@NewsID", NewsID) };
            return DbHelper.ExecuteTable(CommandType.Text, Sql, Param);
        }

        public DataTable GetSubClass(string ClassID, int isParent, string OrderBy, string Desc)
        {
            OleDbParameter Param = new OleDbParameter("@ClassID", ClassID);
            string Sql = string.Empty;
            if (isParent == 1)
            {
                Sql = "Select ClassID From [" + Pre + "news_class] Where [ParentID]=@ClassID and isLock=0 and isRecyle=0 and isPage=0 order by " + OrderBy + " " + Desc + ",id " + Desc + "";
            }
            else
            {
                Sql = "Select ClassID,IsURL,URLaddress,SavePath,SaveClassframe,ClassSaveRule,ClassIndexRule From [" + Pre + "news_class] Where [ClassID]=@ClassID and isLock=0 and isRecyle=0 and isPage=0";
            }
            //OleDbParameter[] Param = new OleDbParameter[] { new OleDbParameter("@NewsID", NewsID) };
            return DbHelper.ExecuteTable(CommandType.Text, Sql, Param);
        }
        public string GetDefinedValue(string dfid, string dfcolumn)
        {
            string Sql = "Select [dsContent] From [" + Pre + "define_save] Where [DsNewsID]=@defineInfoId And [DsEName]=@defineColumns";
            OleDbParameter[] Param = new OleDbParameter[]{
                new OleDbParameter("@defineInfoId", dfid),
                new OleDbParameter("@defineColumns", dfcolumn)
            };
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }

        public string GetCHDefinedValue(int ID, string dfcolumn,string DTalbe)
        {
            string Sql = "Select " + dfcolumn + " From [" + DTalbe + "] Where ID=@ID";
            OleDbParameter[] Param = new OleDbParameter[]
            {
                new OleDbParameter("@ID", ID),
                new OleDbParameter("@defineColumns", dfcolumn)
            };
            string ReturnValue = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
            return ReturnValue;
        }
        /// <summary>
        /// 文字副新闻
        /// </summary>
        /// <param name="TopNum"></param>
        /// <returns></returns>
        public DataTable GetTextSubNews(int TopNum)
        {
            string Sql = "select top " + TopNum + " NewsTitle,TitleColor,TitleITF,TitleBTF,NewsID,ClassID,SavePath,FileName,FileEXName,isDelPoint from " + Pre + "news where Mid(NewsProperty,9,1)='1' and Mid(NewsProperty,1,1)='1' and islock=0 and isRecyle=0 and NewsPicTopline=0 order by EditTime desc,id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        #region 频道开始
        public string GetCHDatable(int ChID)
        {
            OleDbParameter param = new OleDbParameter("@ChID", ChID);
            string sql = "select DataLib from " + Pre + "sys_channel Where ID=@ChID";
            string ChTable = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (ChTable != string.Empty)
            {
                return ChTable;
            }
            else
            {
                return "#";
            }
        }


        public IDataReader GetCHDetail(int id, string DTable)
        {
            string Sql = "Select [Id],[OrderID],[Title],[TitleColor],[TitleITF],[TitleBTF],";
            Sql += "[PicURL],[ClassID],[SpecialID],[Author],[Souce],[Tags],[ContentProperty],";
            Sql += "[Templet],[Content],[Metakeywords],[Metadesc],[naviContent],[Click],[CreatTime],[SavePath],[FileName],";
            Sql += "[isDelPoint],[Gpoint],[iPoint],[GroupNumber],";
            Sql += "[isLock],[ChID],[Editor],[isHtml],[isConstr] From [" + DTable + "]";
            Sql += " Where [ID]=@ID";
            OleDbParameter Param = new OleDbParameter("@ID", id);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }


        /// <summary>
        /// 选择发布频道所有新闻时，取得所有新闻
        /// </summary> 
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        public IDataReader GetPublishCHNewsAll(string DTable,out int ncount)
        {
            string Sql = "select ID,Templet,classID,SavePath,FileName from " + DTable + " where isLock=0 and isDelPoint=0";
            string SqlCount = "select count(ID) from " + DTable + " where isLock=0 and isDelPoint=0";
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 选择发布频道最新时，取得所有新闻
        /// </summary>
        /// <param name="strParams">页面信息字符串</param>
        /// <returns>取得符合条件的所有新闻的查询结果</returns>
        public IDataReader GetPublishCHNewsLast(string DTable,int topnum, bool unpublish, out int ncount)
        {
            string Sql = "select top " + topnum + " ID,Templet,classID,SavePath,FileName from " + DTable + " where isLock=0 and isDelPoint=0";
            string SqlCount = "select count(ID) from " + DTable + " where isLock=0 and isDelPoint=0";
            if (unpublish)
            {
                Sql += " and isHtml=0";
                SqlCount += " and isHtml=0";
            }
            Sql += " order by id desc";
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }


        /// <summary>
        /// 根据栏目发布新闻
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="unpublish"></param>
        /// <param name="isdesc"></param>
        /// <param name="condition"></param>
        /// <param name="ncount"></param>
        /// <returns></returns>
        public IDataReader GetPublishCHNewsByClass(string DTable,string classid, bool unpublish, bool isdesc, string condition, out int ncount)
        {
            string SqlCondition = " where isLock=0 and isDelPoint=0 and ClassID in (" + classid + ")";
            string SqlOrder = "";
            if (unpublish)
            {
                //只发布未发布的
                SqlCondition += " and isHtml=0 ";
            }
            string Sql = "select ID,Templet,classID,SavePath,FileName from " + DTable + " a " + SqlCondition + SqlOrder;
            string SqlCount = "select count(a.ID) from " + DTable + " a " + SqlCondition;
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }


        /// <summary>
        /// 选择按照日期发布时，取得所有新闻
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="ncount"></param>
        /// <returns></returns>
        public IDataReader GetPublishCHNewsByTime(string DTable,DateTime starttime, DateTime endtime, out int ncount)
        {
            string Sql = "select ID,Templet,classID,SavePath,FileName from " + DTable + " where creattime between '" + starttime + "' and '" + endtime + "' and isLock=0 and isDelPoint=0";
            string SqlCount = "select count(ID) from " + DTable + " where creattime between '" + starttime + "' and '" + endtime + "' and isLock=0 and isDelPoint=0";
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 选择按照ID发布时，取得所有新闻
        /// </summary>
        /// <param name="minid"></param>
        /// <param name="maxid"></param>
        /// <param name="ncount"></param>
        /// <returns></returns>
        public IDataReader GetPublishCHNewsByID(string DTable,int minid, int maxid, out int ncount)
        {
            string Sql = "select ID,Templet,classID,SavePath,FileName from " + DTable + " where isLock=0 and isDelPoint=0 and id between " + minid + " and " + maxid;
            string SqlCount = "select count(id) from " + DTable + " where isLock=0 and isDelPoint=0 and id between " + minid + " and " + maxid;
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }


        public IDataReader GetPublishCHClass(string classid,int ChID, out int ncount)
        {
            string SqlCondition = string.Empty;
            if (classid.Trim() == "")
            {
                SqlCondition = " where isLock=0 and isPage<>1 and isDelPoint=0 and ChID=" + ChID + "";
            }
            else
            {
                SqlCondition = " where  isLock=0 and isPage<>1 and isDelPoint=0 classID in (" + classid + ")";
            }
            string SqlCount = "select count(id) from " + Pre + "sys_channelclass" + SqlCondition;
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            string Sql = "select id,Templet,SavePath,FileName from " + Pre + "sys_channelclass" + SqlCondition;
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        public IDataReader GetPublishCHSpecial(int ChID,string spid, out int ncount)
        {
            string SqlCondition = " where isLock=0 and ChID=" + ChID + "";
            if (spid != null && spid.Trim() != "")
            {
                SqlCondition = " where isLock=0 and id in (" + spid + ")  and ChID=" + ChID + "";
            }
            string SqlCount = "select count(id) from " + Pre + "sys_channelspecial" + SqlCondition;
            ncount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, SqlCount, null));
            string Sql = "select id,templet,savePath,filename from " + Pre + "sys_channelspecial" + SqlCondition;
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        public string GetCHPageTitle(int id, string Str, int ChID)
        {
            string Sql = string.Empty;
            switch (Str)
            {
                case "ChIndex":
                    Sql = "select channelName from " + Pre + "sys_channel where id=@ID";
                    break;
                case "ChNews":
                    Sql = "select Title from " + GetCHDatable(ChID) + " where id=@ID";
                    break;
                case "ChClass":
                    Sql = "select classCName from " + Pre + "sys_channelclass where id=@ID and ChID=" + ChID + "";
                    break;
                case "ChSpecial":
                    Sql = "select specialCName from " + Pre + "sys_channelspecial where id=@ID and ChID=" + ChID + "";
                    break;
                default:
                    return string.Empty;
            }
            OleDbParameter Param = null;
            if (Str == "ChIndex")
            {
                Param = new OleDbParameter("@ID", ChID);
            }
            else
            {
                Param = new OleDbParameter("@ID", id);
            }
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }

        public IDataReader GetPositionNavi(int id, string Str, int ChID)
        {
            string Sql = "select id,channelName,htmldir,indexFileName from " + Pre + "sys_channel where id=@ChID";
            OleDbParameter param = new OleDbParameter("@ChID", ChID);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, param);
        }


        public IDataReader GetFieldName(int ChID)
        {
            string sql = "select CName,EName from " + Pre + "sys_channelvalue where ChID=@ChID and isLock=0 and isSearch=1";
            OleDbParameter param = new OleDbParameter("@ChID", ChID);
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public string GetCHMeta(int id, int Num, int ChID,string Str)
        {
            string sql = string.Empty;
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@ID", OleDbType.Integer, 4);
            param[0].Value = id;
            param[1] = new OleDbParameter("@ChID", OleDbType.Integer, 4);
            param[1].Value = ChID;
            switch (Str)
            { 
                case "ChIndex":
                    sql = "select channelName from " + Pre + "sys_channel where ID=" + ChID + "";
                    return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
                case "ChClass":
                    if (Num == 0)
                    {
                        sql = "select KeyMeta from " + Pre + "sys_channelclass where ID=@ID and ChID=@ChID";
                    }
                    else
                    {
                        sql = "select DescMeta from " + Pre + "sys_channelclass where ID=@ID and ChID=@ChID";
                    }
                    return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
                case "ChNews":
                    if (Num == 0)
                    {
                        sql = "select Metakeywords from " + GetCHDatable(ChID) + " where ID=@ID and ChID=@ChID";
                    }
                    else
                    {
                        sql = "select Metadesc from " + GetCHDatable(ChID) + " where ID=@ID and ChID=@ChID";
                    }
                    return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
                case "ChSpecial":
                    if (Num == 0)
                    {
                        sql = "select specialCName from " + Pre + "sys_channelspecial where ID=@ID and ChID=@ChID";
                    }
                    else
                    {
                        sql = "select navicontent from " + Pre + "sys_channelspecial where ID=@ID and ChID=@ChID";
                    }
                    return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
                default:
                    return string.Empty;
            }
        }

        public IDataReader GetCHPosition(int ID, int Num,int ChID)
        {
            OleDbParameter param = new OleDbParameter("@ID", ID);
            string sql = string.Empty;
            switch (Num)
            {
                case 0:
                    sql = "select id,SavePath,FileName,isDelPoint,classCName,ParentID from " + Pre + "sys_channelclass where id=@ID and ChID=" + ChID + "";
                    break;
                case 1:
                    sql = "select b.id,b.SavePath,b.FileName,b.isDelPoint,classCName,b.ParentID from " + GetCHDatable(ChID) + " a," + Pre + "sys_channelclass b where a.id=@ID and a.ClssID=b.ID";
                    break;
                case 2:
                    sql = "select id,parentID,SavePath,filename,specialCName from " + Pre + "sys_channelspecial where id=@ID and ChID=" + ChID + "";
                    break;
            }
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public IDataReader GetSingleCHPageClass(int classid)
        {
            string Sql = "select Templet,classCName,SavePath,PageContent,MetaKeywords,MetaDescript,FileName from " + Pre + "syschannelclass";
            Sql += " where ID=@ClassID and isLock=0 and isPage=1";
            OleDbParameter Param = new OleDbParameter("@ClassID", classid);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }

        public IDataReader GetCHNewsSavePath(int newsid,int ChID)
        {
            string Sql = "select a.templet,a.classid,a.SavePath,a.FileName,b.SavePath as SavePath1,b.FileName as FileName1,a.id,a.isDelPoint from " + GetCHDatable(ChID) + " a, ";
            Sql += Pre + "sys_channelclass b where a.classid=b.id and a.id=@newsid";
            OleDbParameter Param = new OleDbParameter("@newsid", newsid);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }


        public DataTable GetLastCHNews(int topnum, int classid, int ChID)
        {
            OleDbParameter Param = null;
            string Sql = "select top " + topnum + " a.Title,a.Content,a.CreatTime,a.SavePath,a.FileName,";
            Sql += "b.savepath as savepath1,b.ClassEName,b.ClassCName,b.id as id1 from " + GetCHDatable(ChID) + " a," + Pre + "sys_channelclass b";
            Sql += " where a.islock=0  and a.ClassID=b.id and b.isPage<>1 and b.islock=0";
            if (classid != 0)
            {
                Sql += " and b.id=@ClassID";
                Param = new OleDbParameter("@ClassID", classid);
            }
            Sql += " order by a.id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, Param);
        }

        public IDataReader GetFriend(int Type, int Number,int IsAdmin)
        {
            string sql = string.Empty;
            OleDbParameter[] param = new OleDbParameter[3];
            param[0] = new OleDbParameter("@Type", OleDbType.Integer, 4);
            param[0].Value = Type;
            param[1] = new OleDbParameter("@Number", OleDbType.Integer, 4);
            param[1].Value = Number;
            param[2] = new OleDbParameter("@IsAdmin", OleDbType.Integer, 4);
            param[2].Value = IsAdmin;
            if(IsAdmin==3)
            {
                sql = "select top " + Number + " Name,Url,PicUrl,Content,ClassID from " + Pre + "friend_link where Type=@Type and Lock=0 order by id desc";
            }
            else
            {
                sql = "select top " + Number + " Name,Url,PicUrl,Content,ClassID from " + Pre + "friend_link where Type=@Type and isUser=@IsAdmin and Lock=0 order by id desc";
            }
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }
        /// <summary>
        /// 根据新闻ID获取域名
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public string getNewsDomain(string NewsID)
        {
            string sql = "select [DoMain] from " + Pre + "news_class where classid=(select classid from " + Pre + "news where newsid='" + NewsID + "')";
            string sResult = null;
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                sResult =  dr[0] == null ? "" : dr[0].ToString();
            }
            dr.Close();
            return sResult;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        void IDisposable.Dispose()
        {
            DbHelper.CloseConn();
            GC.SuppressFinalize(this);
        }
#endregion

        /// <summary>
        /// 更改新闻是否生成静态页面状态（wxh 20008-6-23）
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public int updateIsHtmlState(string newsID)
        {
            string sql = string.Format("update {0}news set isHtml = 1 where NewsID = '{1}'", Pre, newsID);
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }
    }
}
