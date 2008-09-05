//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==        Code By Simplt.Xie & ZhenJiang.Wang            == 
//===========================================================
using System;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class ContentManage : DbBase, IContentManage
    {

        /// <summary>
        /// 得到站点栏目列表结果  
        /// </summary>
        /// <returns></returns>
        public IDataReader GetClassSitenewsstr(string ParentID, string SiteID)
        {
            string Sql = "Select ClassID,[Domain],DataLib,SiteID,ClassCName,(Select Count(id) from " + Pre + "news_Class where ParentID=a.ClassID and isRecyle=0 and isUrl=0 and islock=0 and isPage=0) as HasSub from " + Pre + "news_Class a where ParentID='" + ParentID + "' and isRecyle=0 and isUrl=0 and islock=0 and SiteID='" + SiteID + "' and isPage=0 order by OrderID desc,id desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }



        #region 内容管理开始



        #region 新闻
        /// <summary>
        /// 更新栏目状态
        /// </summary>
        /// <param name="Num">1为已生成，0表示未生成</param>
        public void updateClassStat(int Num, string ClassID)
        {
            string str_sql = "update " + Pre + "news_Class set isunHTML=" + Num + " where ClassId='" + ClassID + "' " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 更新新闻状态
        /// </summary>
        /// <param name="Num">1为已生成，0表示未生成</param>
        public void updateNewsHTML(int Num, string NewsID)
        {
            string str_sql = "update " + Pre + "news set isHtml=" + Num + " where NewsID='" + NewsID + "' " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 得到导航内容
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public IDataReader getNaviClass(string ClassID)
        {
            string Sql = "select ClassCName,ClassEName,ParentID,ClassID,DataLib,SavePath,SaveClassframe,ClassSaveRule from " + Pre + "news_class where ClassID=@ClassID " + Foosun.Common.Public.getSessionStr() + "";
            SqlParameter Param = new SqlParameter("@ClassID", ClassID);
            return DbHelper.ExecuteReader(CommandType.Text, Sql, Param);
        }


        /// <summary>
        /// 新闻内容管理.得到索引表
        /// </summary>
        /// <returns></returns>

        public IDataReader GetNewsIndex()
        {
            string Sql = "select TableName,id from " + Pre + "sys_NewsIndex order by Id asc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 新闻内容管理.得到站点表
        /// </summary>
        /// <returns></returns>

        public DataTable GetSiteID()
        {
            string Sql = "select ChannelID,CName from " + Pre + "News_site where isLock=0 and isURL=0 and isRecyle=0";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getTagsList()
        {
            string Sql = "select top 15 CName from " + Pre + "news_Gen where gType=0 and islock=0 order by id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 彻底删除新闻
        /// </summary>
        /// <param name="nID"></param>
        /// <param name="Tablename"></param>
        public void del_all(int nID, string Tablename)
        {
            string str_sql = "delete from " + Tablename + " where Id=" + nID + " and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 删除新闻到回收站
        /// </summary>
        /// <param name="nID"></param>
        /// <param name="Tablename"></param>
        public void del_Recyle(string nID, string Tablename)
        {
            string str_sql = "update " + Tablename + " set isRecyle=1 where Id in (" + nID + ") " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 锁定新闻
        /// </summary>
        /// <param name="nID"></param>
        /// <param name="Tablename"></param>
        public void del_Lock(string nID, string Tablename)
        {
            string str_sql = "update " + Tablename + " set isLock=1 where Id in (" + nID + ") " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        // by Simplt.Xie
        /// <summary>
        /// 得到指定新闻的DataTable
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <returns></returns>
        public IDataReader getNewsID(string NewsID)
        {
            SqlParameter param = new SqlParameter("@NewsID", NewsID);
            string Sql = "select a.*,b.ClassCName from " + Pre + "News a left join " + Pre + "news_Class b ";
            Sql += " on a.ClassID=b.ClassID  where a.NewsID=@NewsID";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, param);
        }
        /// <summary>
        /// 得到继承栏目的DataTable
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getClassParam(string ClassID)
        {
            string Sql = "select * from " + Pre + "News_Class where ClassID = '" + ClassID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 得到继承栏目的DataTable
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getSysParam()
        {
            string Sql = "select * from " + Pre + "sys_param";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 检查新闻标题
        /// </summary>
        /// <param name="NewsTitle"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        public int newsTitletf(string NewsTitle, string dtable, string EditAction, string NewsID)
        {
            int intflg = 0;
            SqlParameter param = new SqlParameter("@NewsTitle", NewsTitle);
            string Sql = "";
            if (EditAction == "Edit")
            {
                Sql = "select ID from " + dtable + " where NewsTitle=@NewsTitle and NewsID!='" + NewsID + "'";
            }
            else
            {
                Sql = "select ID from " + dtable + " where NewsTitle=@NewsTitle";
            }
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (rdr != null)
            {
                if (rdr.Rows.Count > 0) { intflg = 1; }
                rdr.Clear(); rdr.Dispose();
            }
            return intflg;
        }

        /// <summary>
        /// 插入常规
        /// </summary>
        /// <param name="_TempStr"></param>
        /// <param name="_URL"></param>
        /// <param name="_EmailURL"></param>
        /// <param name="_num"></param>
        public void iGen(string _TempStr, string _URL, string _EmailURL, int _num)
        {
            string SQLTF = "select id from " + Pre + "News_Gen where Cname='" + _TempStr.Trim() + "' and gType=" + _num + " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, SQLTF, null);
            if (rdr != null)
            {
                if (rdr.Rows.Count == 0)
                {
                    string Sql = "insert into " + Pre + "News_Gen(";
                    Sql += "Cname,gType,URL,EmailURL,isLock,SiteID";
                    Sql += ") values (";
                    Sql += "'" + _TempStr + "'," + _num + ",'" + _URL + "','" + _EmailURL + "',0,'" + Foosun.Global.Current.SiteID + "')";
                    DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
                }
                rdr.Clear(); rdr.Dispose();
            }
        }

        /// <summary>
        /// 得到内部连接地址
        /// </summary>
        /// <returns></returns>
        public DataTable getGenContent()
        {
            string Sql = "select Cname,URL from " + Pre + "news_Gen where gType=3 and islock=0";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }


        public string getDataLib(string ClassID)
        {
            string Sql = "select DataLib from " + Pre + "news_Class where ClassID=@ClassID";
            SqlParameter Param = new SqlParameter("@ClassID", ClassID);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, Param));
        }

        /// <summary>
        /// 得到最新新闻 ID
        /// </summary>
        /// <returns></returns>
        public DataTable getTopNewsId(string Datatb)
        {
            string Sql = "select Id from " + Datatb + " order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 插入栏目新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void insertNewsContent(Foosun.Model.NewsContent uc)
        {
            string Sql = "insert into " + uc.DataLib + "(";
            Sql += "NewsID,NewsType,OrderID,NewsTitle,sNewsTitle,TitleColor,TitleITF,TitleBTF,CommLinkTF,SubNewsTF,URLaddress,PicURL,SPicURL,ClassID,Author,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,vURL,naviContent,Click,CreatTime,EditTime,SavePath,FileName,FileEXName,ContentPicTF,ContentPicURL,ContentPicSize,CommTF,DiscussTF,TopNum,VoteTF,CheckStat,isLock,isRecyle,SiteID,DataLib,DefineID,isVoteTF,Editor,isHtml,isDelPoint,Gpoint,iPoint,GroupNumber,Metakeywords,Metadesc,isConstr,isFiles";
            Sql += ") values (";
            Sql += "@NewsID,@NewsType,@OrderID,@NewsTitle,@sNewsTitle,@TitleColor,@TitleITF,@TitleBTF,@CommLinkTF,@SubNewsTF,@URLaddress,@PicURL,@SPicURL,@ClassID,@Author,@Souce,@Tags,@NewsProperty,@NewsPicTopline,@Templet,@Content,@vURL,@naviContent,@Click,@CreatTime,'" + DateTime.Now + "',@SavePath,@FileName,@FileEXName,@ContentPicTF,@ContentPicURL,@ContentPicSize,@CommTF,@DiscussTF,@TopNum,@VoteTF,@CheckStat,@isLock,@isRecyle,@SiteID,@DataLib,@DefineID,@isVoteTF,@Editor,@isHtml,@isDelPoint,@Gpoint,@iPoint,@GroupNumber,@Metakeywords,@Metadesc,0,@isFiles)";
            SqlParameter[] parm = insertNewsContentParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);

            //添加专题
            if (uc.SpecialID != null && uc.SpecialID != "")
            {
                string[] arr_specialID = uc.SpecialID.Split(',');
                for (int i = 0; i < arr_specialID.Length; i++)
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@SpecialID", SqlDbType.NVarChar, 20);
                    param[0].Value = arr_specialID[i].ToString();
                    param[1] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
                    param[1].Value = uc.NewsID;
                    Sql = "Insert Into " + Pre + "special_news(SpecialID,NewsID) Values(@SpecialID,@NewsID)";
                    DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
                }
            }

        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateNewsContent(Foosun.Model.NewsContent uc)
        {
            string Sql = "Update " + uc.DataLib + " set CreatTime=@CreatTime,NewsType=@NewsType,OrderID=@OrderID,NewsTitle=@NewsTitle,sNewsTitle=@sNewsTitle,TitleColor=@TitleColor,TitleITF=@TitleITF,TitleBTF=@TitleBTF,CommLinkTF=@CommLinkTF,SubNewsTF=@SubNewsTF,URLaddress=@URLaddress,PicURL=@PicURL,SPicURL=@SPicURL,ClassID=@ClassID,Author=@Author,Souce=@Souce,Tags=@Tags,NewsProperty=@NewsProperty,NewsPicTopline=@NewsPicTopline,Templet=@Templet,Content=@Content,vURL=@vURL,naviContent=@naviContent,Click=@Click,EditTime='" + uc.EditTime.ToString() + "',ContentPicTF=@ContentPicTF,ContentPicURL=@ContentPicURL,ContentPicSize=@ContentPicSize,CommTF=@CommTF,DiscussTF=@DiscussTF,TopNum=@TopNum,VoteTF=@VoteTF,CheckStat=@CheckStat,DefineID=@DefineID,isVoteTF=@isVoteTF,Editor=@Editor,isHtml=@isHtml,isDelPoint=@isDelPoint,Gpoint=@Gpoint,iPoint=@iPoint,GroupNumber=@GroupNumber,Metakeywords=@Metakeywords,Metadesc=@Metadesc,isFiles=@isFiles where NewsId='" + uc.NewsID + "' " + Foosun.Common.Public.getSessionStr() + "";
            SqlParameter[] parm = insertNewsContentParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
            string usql = "update " + uc.DataLib + " set islock=1 where NewsID='" + uc.NewsID + "' and substring(CheckStat,3,5)!='0|0|0'";
            DbHelper.ExecuteNonQuery(CommandType.Text, usql, parm);

            //删除新闻所属专题
            DbHelper.ExecuteNonQuery(CommandType.Text, "Delete From " + Pre + "special_news Where NewsID='" + uc.NewsID + "'", null);
            //添加专题
            if (uc.SpecialID != null && uc.SpecialID != "")
            {
                string[] arr_specialID = uc.SpecialID.Split(',');
                for (int i = 0; i < arr_specialID.Length; i++)
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@SpecialID", SqlDbType.NVarChar, 20);
                    param[0].Value = arr_specialID[i].ToString();
                    param[1] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
                    param[1].Value = uc.NewsID;
                    Sql = "Insert Into " + Pre + "special_news(SpecialID,NewsID) Values(@SpecialID,@NewsID)";
                    DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
                }
            }
        }

        /// <summary>
        /// 取得专题与新闻的对应表
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns></returns>
        public DataTable getSpecialNews(string NewsID)
        {
            SqlParameter param = new SqlParameter("@NewsID", NewsID);
            string Sql = "Select a.SpecialID,b.SpecialCName From " + Pre + "special_news as a," + Pre + "news_special as b Where a.SpecialID=b.SpecialID And a.NewsID=@NewsID And b.isLock=0 And b.isRecyle=0";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        /// <summary>
        /// 获取NewsContent构造（修改）
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateNewsContentParameters(Foosun.Model.NewsContent uc)
        {
            SqlParameter[] param = new SqlParameter[45];
            param[0] = new SqlParameter("@NewsType", SqlDbType.TinyInt, 1);
            param[0].Value = uc.NewsType;
            param[1] = new SqlParameter("@OrderID", SqlDbType.TinyInt, 1);
            param[1].Value = uc.OrderID;
            param[2] = new SqlParameter("@NewsTitle", SqlDbType.NVarChar, 100);
            param[2].Value = uc.NewsTitle;
            param[3] = new SqlParameter("@sNewsTitle", SqlDbType.NVarChar, 100);
            param[3].Value = uc.sNewsTitle;
            param[4] = new SqlParameter("@TitleColor", SqlDbType.NVarChar, 10);
            param[4].Value = uc.TitleColor;
            param[5] = new SqlParameter("@TitleITF", SqlDbType.TinyInt, 1);
            param[5].Value = uc.TitleITF;
            param[6] = new SqlParameter("@TitleBTF", SqlDbType.TinyInt, 1);
            param[6].Value = uc.TitleBTF;
            param[7] = new SqlParameter("@CommLinkTF", SqlDbType.TinyInt, 1);
            param[7].Value = uc.CommLinkTF;
            param[8] = new SqlParameter("@SubNewsTF", SqlDbType.TinyInt, 1);
            param[8].Value = uc.SubNewsTF;
            param[9] = new SqlParameter("@URLaddress", SqlDbType.NVarChar, 200);
            param[9].Value = uc.URLaddress;
            param[10] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 200);
            param[10].Value = uc.PicURL;
            param[11] = new SqlParameter("@SPicURL", SqlDbType.NVarChar, 200);
            param[11].Value = uc.SPicURL;
            param[12] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
            param[12].Value = uc.ClassID;
            param[13] = new SqlParameter("@SpecialID", SqlDbType.NVarChar, 200);
            param[13].Value = "";
            param[14] = new SqlParameter("@Author", SqlDbType.NVarChar, 100);
            param[14].Value = uc.Author;
            param[15] = new SqlParameter("@Souce", SqlDbType.NVarChar, 100);
            param[15].Value = uc.Souce;
            param[16] = new SqlParameter("@Tags", SqlDbType.NVarChar, 100);
            param[16].Value = uc.Tags;
            param[17] = new SqlParameter("@NewsProperty", SqlDbType.NVarChar, 30);
            param[17].Value = uc.NewsProperty;
            param[18] = new SqlParameter("@Templet", SqlDbType.NVarChar, 200);
            param[18].Value = uc.Templet;
            param[19] = new SqlParameter("@Content", SqlDbType.NText);
            param[19].Value = uc.Content;
            param[20] = new SqlParameter("@naviContent", SqlDbType.NVarChar, 255);
            param[20].Value = uc.naviContent;
            param[21] = new SqlParameter("@ContentPicTF", SqlDbType.TinyInt, 1);
            param[21].Value = uc.ContentPicTF;
            param[22] = new SqlParameter("@ContentPicURL", SqlDbType.NVarChar, 200);
            param[22].Value = uc.ContentPicURL;
            param[23] = new SqlParameter("@ContentPicSize", SqlDbType.NVarChar, 10);
            param[23].Value = uc.ContentPicSize;
            param[24] = new SqlParameter("@CommTF", SqlDbType.TinyInt, 1);
            param[24].Value = uc.CommTF;
            param[25] = new SqlParameter("@DiscussTF", SqlDbType.TinyInt, 1);
            param[25].Value = uc.DiscussTF;
            param[26] = new SqlParameter("@TopNum", SqlDbType.Int, 4);
            param[26].Value = uc.TopNum;
            param[27] = new SqlParameter("@VoteTF", SqlDbType.TinyInt, 1);
            param[27].Value = uc.VoteTF;
            param[28] = new SqlParameter("@NewsPicTopline", SqlDbType.TinyInt, 1);
            param[28].Value = uc.NewsPicTopline;
            param[29] = new SqlParameter("@CheckStat", SqlDbType.NVarChar, 10);
            param[29].Value = uc.CheckStat;
            param[30] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[30].Value = uc.isLock;
            param[31] = new SqlParameter("@DefineID", SqlDbType.TinyInt, 1);
            param[31].Value = uc.DefineID;
            param[32] = new SqlParameter("@isVoteTF", SqlDbType.TinyInt, 1);
            param[32].Value = uc.isVoteTF;
            param[33] = new SqlParameter("@Editor", SqlDbType.NVarChar, 18);
            param[33].Value = uc.Editor;
            param[34] = new SqlParameter("@isHtml", SqlDbType.TinyInt, 1);
            param[34].Value = uc.isHtml;
            param[35] = new SqlParameter("@Click", SqlDbType.Int, 4);
            param[35].Value = uc.Click;
            param[36] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt, 1);
            param[36].Value = uc.isDelPoint;
            param[37] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[37].Value = uc.Gpoint;
            param[38] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[38].Value = uc.iPoint;
            param[39] = new SqlParameter("@GroupNumber", SqlDbType.NText);
            param[39].Value = uc.GroupNumber;
            param[40] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
            param[40].Value = uc.NewsID;
            param[41] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 20);
            param[41].Value = uc.DataLib;
            param[42] = new SqlParameter("@Metakeywords", SqlDbType.NVarChar, 200);
            param[42].Value = uc.Metakeywords;
            param[43] = new SqlParameter("@Metadesc", SqlDbType.NVarChar, 200);
            param[43].Value = uc.Metadesc;
            param[44] = new SqlParameter("@isFiles", SqlDbType.TinyInt, 1);
            param[44].Value = uc.isFiles;
            return param;
        }

        /// <summary>
        /// 获取NewsContent构造(插入)
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private SqlParameter[] insertNewsContentParameters(Foosun.Model.NewsContent uc)
        {
            SqlParameter[] param = new SqlParameter[52];
            param[0] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
            param[0].Value = uc.NewsID;
            param[1] = new SqlParameter("@NewsType", SqlDbType.TinyInt, 1);
            param[1].Value = uc.NewsType;
            param[2] = new SqlParameter("@OrderID", SqlDbType.TinyInt, 1);
            param[2].Value = uc.OrderID;
            param[3] = new SqlParameter("@NewsTitle", SqlDbType.NVarChar, 100);
            param[3].Value = uc.NewsTitle;
            param[4] = new SqlParameter("@sNewsTitle", SqlDbType.NVarChar, 100);
            param[4].Value = uc.sNewsTitle;
            param[5] = new SqlParameter("@TitleColor", SqlDbType.NVarChar, 10);
            param[5].Value = uc.TitleColor;
            param[6] = new SqlParameter("@TitleITF", SqlDbType.TinyInt, 1);
            param[6].Value = uc.TitleITF;
            param[7] = new SqlParameter("@TitleBTF", SqlDbType.TinyInt, 1);
            param[7].Value = uc.TitleBTF;
            param[8] = new SqlParameter("@CommLinkTF", SqlDbType.TinyInt, 1);
            param[8].Value = uc.CommLinkTF;
            param[9] = new SqlParameter("@SubNewsTF", SqlDbType.TinyInt, 1);
            param[9].Value = uc.SubNewsTF;
            param[10] = new SqlParameter("@URLaddress", SqlDbType.NVarChar, 200);
            param[10].Value = uc.URLaddress;
            param[11] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 200);
            param[11].Value = uc.PicURL;
            param[12] = new SqlParameter("@SPicURL", SqlDbType.NVarChar, 200);
            param[12].Value = uc.SPicURL;
            param[13] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
            param[13].Value = uc.ClassID;
            param[14] = new SqlParameter("@SpecialID", SqlDbType.NVarChar, 200);
            param[14].Value = "";
            param[15] = new SqlParameter("@Author", SqlDbType.NVarChar, 100);
            param[15].Value = uc.Author;
            param[16] = new SqlParameter("@Souce", SqlDbType.NVarChar, 100);
            param[16].Value = uc.Souce;
            param[17] = new SqlParameter("@Tags", SqlDbType.NVarChar, 100);
            param[17].Value = uc.Tags;
            param[18] = new SqlParameter("@NewsProperty", SqlDbType.NVarChar, 30);
            param[18].Value = uc.NewsProperty;
            param[19] = new SqlParameter("@Templet", SqlDbType.NVarChar, 200);
            param[19].Value = uc.Templet;
            param[20] = new SqlParameter("@Content", SqlDbType.NText);
            param[20].Value = uc.Content;
            param[21] = new SqlParameter("@naviContent", SqlDbType.NVarChar, 255);
            param[21].Value = uc.naviContent;
            param[22] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[22].Value = uc.CreatTime;
            param[23] = new SqlParameter("@SavePath", SqlDbType.NVarChar, 200);
            param[23].Value = uc.SavePath;
            param[24] = new SqlParameter("@FileName", SqlDbType.NVarChar, 100);
            param[24].Value = uc.FileName;
            param[25] = new SqlParameter("@FileEXName", SqlDbType.NVarChar, 6);
            param[25].Value = uc.FileEXName;
            param[26] = new SqlParameter("@ContentPicTF", SqlDbType.TinyInt, 1);
            param[26].Value = uc.ContentPicTF;
            param[27] = new SqlParameter("@ContentPicURL", SqlDbType.NVarChar, 200);
            param[27].Value = uc.ContentPicURL;
            param[28] = new SqlParameter("@ContentPicSize", SqlDbType.NVarChar, 10);
            param[28].Value = uc.ContentPicSize;
            param[29] = new SqlParameter("@CommTF", SqlDbType.TinyInt, 1);
            param[29].Value = uc.CommTF;
            param[30] = new SqlParameter("@DiscussTF", SqlDbType.TinyInt, 1);
            param[30].Value = uc.DiscussTF;
            param[31] = new SqlParameter("@TopNum", SqlDbType.Int, 4);
            param[31].Value = uc.TopNum;
            param[32] = new SqlParameter("@VoteTF", SqlDbType.TinyInt, 1);
            param[32].Value = uc.VoteTF;
            param[33] = new SqlParameter("@NewsPicTopline", SqlDbType.TinyInt, 1);
            param[33].Value = uc.NewsPicTopline;
            param[34] = new SqlParameter("@CheckStat", SqlDbType.NVarChar, 10);
            param[34].Value = uc.CheckStat;
            param[35] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[35].Value = uc.isLock;
            param[36] = new SqlParameter("@isRecyle", SqlDbType.TinyInt, 1);
            param[36].Value = uc.isRecyle;
            param[37] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[37].Value = uc.SiteID;
            param[38] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 20);
            param[38].Value = uc.DataLib;
            param[39] = new SqlParameter("@DefineID", SqlDbType.TinyInt, 1);
            param[39].Value = uc.DefineID;
            param[40] = new SqlParameter("@isVoteTF", SqlDbType.TinyInt, 1);
            param[40].Value = uc.isVoteTF;
            param[41] = new SqlParameter("@Editor", SqlDbType.NVarChar, 18);
            param[41].Value = uc.Editor;
            param[42] = new SqlParameter("@isHtml", SqlDbType.TinyInt, 1);
            param[42].Value = uc.isHtml;
            param[43] = new SqlParameter("@Click", SqlDbType.Int, 4);
            param[43].Value = uc.Click;
            param[44] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt, 1);
            param[44].Value = uc.isDelPoint;
            param[45] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[45].Value = uc.Gpoint;
            param[46] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[46].Value = uc.iPoint;
            param[47] = new SqlParameter("@GroupNumber", SqlDbType.NText);
            param[47].Value = uc.GroupNumber;
            param[48] = new SqlParameter("@Metakeywords", SqlDbType.NVarChar, 200);
            param[48].Value = uc.Metakeywords;
            param[49] = new SqlParameter("@Metadesc", SqlDbType.NVarChar, 200);
            param[49].Value = uc.Metadesc;
            param[50] = new SqlParameter("@isFiles", SqlDbType.TinyInt, 1);
            param[50].Value = uc.isFiles;
            param[51] = new SqlParameter("@vURL", SqlDbType.NVarChar, 200);
            param[51].Value = uc.vURL;
            return param;
        }


        public DataTable getNewsIDTF(string NewsID, string Datatb)
        {
            string Sql = "select NewsID from " + Datatb + " where NewsID='" + NewsID + "' order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 插入子新闻
        /// </summary>
        /// <param name="uc2"></param>
        public void insertSubNewsContent(string NewsID, string getNewsID, string NewsTitle, string DataLib, string TitleColor, int TitleBTF, int TitleITF, int colsNum)
        {
            string Sql = "insert into " + Pre + "news_sub(";
            Sql += "NewsID,getNewsID,NewsTitle,DataLib,TitleColor,TitleBTF,TitleITF,colsNum,SiteID";
            Sql += ") values (";
            Sql += "'" + NewsID + "','" + getNewsID + "','" + NewsTitle + "','" + DataLib + "','" + TitleColor + "'," + TitleBTF + "," + TitleITF + "," + colsNum + ",'" + Foosun.Global.Current.SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 删除子新闻
        /// </summary>
        /// <param name="NewsID"></param>
        public void delNewsContent(string NewsID)
        {
            string Sql = "delete from " + Pre + "news_sub where NewsId = '" + NewsID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 插入头条
        /// </summary>
        /// <param name="uc2"></param>
        public void intsertTT(Foosun.Model.NewsContentTT uc)
        {
            string Sql = "insert into " + Pre + "news_topline(";
            Sql += "NewsTF,NewsID,DataLib,tl_font,tl_style,tl_size,tl_color,tl_space,tl_PicColor,tl_SavePath,Creattime,tl_Title,tl_Width,SiteID";
            Sql += ") values (";
            Sql += "@NewsTF,@NewsID,@DataLib,@tl_font,@tl_style,@tl_size,@tl_color,@tl_space,@tl_PicColor,@tl_SavePath,@Creattime,@tl_Title,@tl_Width,@SiteID)";
            SqlParameter[] parm = intsertTTParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 更新头条
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateTT(Foosun.Model.NewsContentTT uc)
        {
            string Sql = "update " + Pre + "news_topline set NewsTF=@NewsTF,NewsID=@NewsID,DataLib=@DataLib,tl_font=@tl_font,tl_style=@tl_style,tl_size=@tl_size,tl_color=@tl_color,tl_space=@tl_space,tl_PicColor=@tl_PicColor,tl_SavePath=@tl_SavePath,Creattime=@Creattime,tl_Title=@tl_Title,tl_Width=@tl_Width,SiteID=@SiteID where NewsID='" + uc.NewsID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            SqlParameter[] parm = intsertTTParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取NewsContentTT构造
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private SqlParameter[] intsertTTParameters(Foosun.Model.NewsContentTT uc)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@NewsTF", SqlDbType.TinyInt, 1);
            param[0].Value = uc.NewsTF;
            param[1] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
            param[1].Value = uc.NewsID;
            param[2] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 50);
            param[2].Value = uc.DataLib;
            param[3] = new SqlParameter("@tl_font", SqlDbType.NVarChar, 20);
            param[3].Value = uc.tl_font;
            param[4] = new SqlParameter("@tl_style", SqlDbType.TinyInt, 1);
            param[4].Value = uc.tl_style;
            param[5] = new SqlParameter("@tl_size", SqlDbType.TinyInt, 1);
            param[5].Value = uc.tl_size;
            param[6] = new SqlParameter("@tl_color", SqlDbType.NVarChar, 8);
            param[6].Value = uc.tl_color;
            param[7] = new SqlParameter("@tl_space", SqlDbType.TinyInt, 1);
            param[7].Value = uc.tl_space;
            param[8] = new SqlParameter("@tl_PicColor", SqlDbType.NVarChar, 8);
            param[8].Value = uc.tl_PicColor;
            param[9] = new SqlParameter("@tl_SavePath", SqlDbType.NVarChar, 220);
            param[9].Value = uc.tl_SavePath;
            param[10] = new SqlParameter("@Creattime", SqlDbType.DateTime, 8);
            param[10].Value = uc.Creattime;
            param[11] = new SqlParameter("@tl_Title", SqlDbType.NVarChar, 150);
            param[11].Value = uc.tl_Title;
            param[12] = new SqlParameter("@tl_Width", SqlDbType.Int, 4);
            param[12].Value = uc.tl_Width;
            param[13] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[13].Value = uc.SiteID;
            return param;
        }

        /// <summary>
        /// 插入附件
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <param name="FileURL"></param>
        /// <param name="OrderID"></param>
        public void insertFileURL(string URLName, string NewsID, string DataLib, string FileURL, int OrderID)
        {
            string Sql = "insert into " + Pre + "news_URL(";
            Sql += "URLName,NewsID,DataLib,FileURL,CreatTime,OrderID";
            Sql += ") values (";
            Sql += "'" + URLName + "','" + NewsID + "','" + DataLib + "','" + FileURL + "','" + DateTime.Now + "'," + OrderID + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 删除id不是ids的附件
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="NewsID"></param>
        public void deleteFileUrl(string ids, string NewsID)
        {
            string sql = "delete from " + Pre + "news_URL where newsid='" + NewsID + "' and id not in(" + ids + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 更新附件文件地址
        /// </summary>
        /// <param name="URLName"></param>
        /// <param name="DataLib"></param>
        /// <param name="FileURL"></param>
        /// <param name="OrderID"></param>
        /// <param name="ID"></param>
        public void updateFileURL(string URLName, string DataLib, string FileURL, int OrderID, int ID)
        {
            string Sql = "update " + Pre + "news_URL set ";
            Sql += "URLName='" + URLName + "',DataLib='" + DataLib + "',FileURL='" + FileURL + "',OrderID=" + OrderID + " where id=" + ID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 更新附件
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <param name="FileURL"></param>
        /// <param name="OrderID"></param>
        public void deleteFilesurl(int flgTF, string NewsID)
        {
            string Sql = null;
            string SQL1 = null;
            if (flgTF == 1)
            {
                SQL1 = "select id,NewsID,DataLib from " + Pre + "news_URL order by id desc";
                DataTable dt = DbHelper.ExecuteTable(CommandType.Text, SQL1, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string SQLN = "select id from " + dt.Rows[i]["DataLib"].ToString() + " where NewsID='" + dt.Rows[i]["NewsID"].ToString() + "' order by id desc";
                        DataTable dts = DbHelper.ExecuteTable(CommandType.Text, SQLN, null);
                        if (dts != null && dts.Rows.Count == 0)
                        {
                            Sql = "delete from " + Pre + "news_URL where NewsId='" + dt.Rows[i]["NewsID"].ToString() + "'";
                            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
                            dts.Clear(); dts.Dispose();
                        }
                    }
                    dt.Clear(); dt.Dispose();
                }
            }
            else
            {
                string getSQL = "select NewsID,DataLib from " + Pre + "News where ID=" + int.Parse(NewsID) + "";
                DataTable dtss = DbHelper.ExecuteTable(CommandType.Text, getSQL, null);
                if (dtss != null && dtss.Rows.Count > 0)
                {
                    Sql = "delete from " + Pre + "news_URL where NewsId='" + dtss.Rows[0]["NewsID"].ToString() + "'";
                    DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
                    string sSql = "update " + dtss.Rows[0]["DataLib"].ToString() + " set isFiles=0 where NewsId='" + dtss.Rows[0]["NewsID"].ToString() + "'";
                    DbHelper.ExecuteNonQuery(CommandType.Text, sSql, null);
                    dtss.Clear(); dtss.Dispose();
                }
            }
        }

        /// <summary>
        /// 得到新闻是否有附件
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int getFileIDTF(int ID)
        {
            int intflg = 0;
            string Sql = "select id from " + Pre + "news_URL where ID='" + ID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (rdr != null)
            {
                if (rdr.Rows.Count > 0)
                {
                    intflg = 1;
                }
                rdr.Clear(); rdr.Dispose();
            }
            return intflg;
        }

        /// <summary>
        /// 得到某条新闻的附件列表
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataTB"></param>
        /// <returns></returns>
        public DataTable getFileList(string NewsID, string DataTB)
        {
            SqlParameter param = new SqlParameter("@NewsID", NewsID);
            string Sql = "select URLName,id,FileURL,OrderID from " + Pre + "news_URL where NewsID=@NewsID and DataLib='" + DataTB + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        /// <summary>
        /// 得到新闻附件地址
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string getNewsAccessory(int ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string Sql = "select FileURL from " + Pre + "news_URL where ID=@ID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 插入投票
        /// </summary>
        /// <param name="uc2"></param>
        public void intsertVote(Foosun.Model.VoteContent uc)
        {
            string Sql = "insert into " + Pre + "news_vote(";
            Sql += "voteNum,voteTitle,voteContent,creattime,ismTF,isMember,NewsID,DataLib,SiteID,isTimeOutTime";
            Sql += ") values (";
            Sql += "@voteNum,@voteTitle,@voteContent,@creattime,@ismTF,@isMember,@NewsID,@DataLib,@SiteID,@isTimeOutTime)";
            SqlParameter[] parm = intsertVoteParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 更新投票
        /// </summary>
        /// <param name="uc"></param>
        public void UpdateVote(Foosun.Model.VoteContent uc)
        {
            string Sql = "Update " + Pre + "news_vote set voteTitle=@voteTitle,voteContent=@voteContent,creattime=@creattime,ismTF=@ismTF,isMember=@isMember,isTimeOutTime=@isTimeOutTime where NewsId='" + uc.NewsID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            SqlParameter[] parm = updateVoteParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取VoteContent构造(插入)
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private SqlParameter[] intsertVoteParameters(Foosun.Model.VoteContent ucv)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@voteNum", SqlDbType.NVarChar, 20);
            param[0].Value = ucv.voteNum;
            param[1] = new SqlParameter("@voteTitle", SqlDbType.NVarChar, 100);
            param[1].Value = ucv.voteTitle;
            param[2] = new SqlParameter("@voteContent", SqlDbType.NText);
            param[2].Value = ucv.voteContent;
            param[3] = new SqlParameter("@creattime", SqlDbType.DateTime, 8);
            param[3].Value = ucv.creattime;
            param[4] = new SqlParameter("@ismTF", SqlDbType.TinyInt, 1);
            param[4].Value = ucv.ismTF;
            param[5] = new SqlParameter("@isMember", SqlDbType.TinyInt, 1);
            param[5].Value = ucv.isMember;
            param[6] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
            param[6].Value = ucv.NewsID;
            param[7] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 20);
            param[7].Value = ucv.DataLib;
            param[8] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[8].Value = ucv.SiteID;
            param[9] = new SqlParameter("@isTimeOutTime", SqlDbType.DateTime, 8);
            param[9].Value = ucv.isTimeOutTime;
            return param;
        }

        /// <summary>
        /// 获取VoteContent构造（更新）
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private SqlParameter[] updateVoteParameters(Foosun.Model.VoteContent ucv)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@voteTitle", SqlDbType.NVarChar, 100);
            param[0].Value = ucv.voteTitle;
            param[1] = new SqlParameter("@voteContent", SqlDbType.NText);
            param[1].Value = ucv.voteContent;
            param[2] = new SqlParameter("@creattime", SqlDbType.DateTime, 8);
            param[2].Value = ucv.creattime;
            param[3] = new SqlParameter("@ismTF", SqlDbType.TinyInt, 1);
            param[3].Value = ucv.ismTF;
            param[4] = new SqlParameter("@isMember", SqlDbType.TinyInt, 1);
            param[4].Value = ucv.isMember;
            param[5] = new SqlParameter("@isTimeOutTime", SqlDbType.DateTime, 8);
            param[5].Value = ucv.isTimeOutTime;
            param[6] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
            param[6].Value = ucv.NewsID;
            return param;
        }

        /// <summary>
        /// 得到投票(NewsID)
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <returns></returns>
        public DataTable getVoteID(string NewsID, string DataLib)
        {
            string Sql = "select voteNum,NewsID,voteContent,ismTF,isMember,SiteID,isTimeOutTime from " + Pre + "news_vote where NewsID='" + NewsID + "' and SiteID='" + Foosun.Global.Current.SiteID + "' and DataLib='" + DataLib + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 得到头条(NewsID)
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <returns></returns>
        public DataTable getTopline(string NewsID, string DataLib, int NewsTFNum)
        {
            string Sql = "select NewsTF,NewsID,DataLib,tl_style,tl_font,tl_size,tl_color,tl_space,tl_PicColor,tl_Title,tl_Width,SiteID,tl_SavePath from " + Pre + "news_topline where NewsID='" + NewsID + "' and SiteID='" + Foosun.Global.Current.SiteID + "' and DataLib='" + DataLib + "' and NewsTF=" + NewsTFNum + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到自定义字段
        /// </summary>
        /// <param name="_Str"></param>
        /// <returns></returns>
        public DataTable getDefineID(string _Str)
        {
            string Sql = "Select defineCname,defineType,IsNull,defineValue,defineExpr From " + Pre + "define_data Where defineInfoId in(" + _Str + ") and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// ajax保存新闻
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public string saveAjaxContent(string Content)
        {
            //删除今天前的新闻
            string SqlTemp = "delete from " + Pre + "news_temp where CreatTime<>" + DateTime.Now.ToShortDateString() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, SqlTemp, null);

            string _RandStr = Foosun.Common.Rand.Number(12);
            string Sql = "insert into " + Pre + "news_temp(";
            Sql += "randNum,Content,CreatTime";
            Sql += ") values (";
            Sql += "'" + _RandStr + "','" + Content + "','" + DateTime.Now.ToShortDateString() + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return _RandStr;
        }

        /// <summary>
        /// 得到栏目中文名称
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public string getClassCName(string ClassID)
        {
            string strflg = "没选择栏目";
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string Sql = "Select ClassCName From " + Pre + "news_class where ClassID=@ClassID";
            object obj = DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
            if (obj != null)
            {
                if (obj != DBNull.Value)
                    strflg = obj.ToString();
                else
                    strflg = string.Empty;
            }
            return strflg;
        }

        public string getspecialCName(string ClassID)
        {
            string strflg = "没选择专题";
            string Sql = "Select SpecialCName From " + Pre + "news_special where SpecialID='" + ClassID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (rdr != null)
            {
                if (rdr.Rows.Count > 0)
                {
                    strflg = rdr.Rows[0]["SpecialCName"].ToString();
                }
                rdr.Clear(); rdr.Dispose();
            }
            return strflg;
        }

        #endregion 新闻

        #region 栏目开始
        /// <summary>
        /// 得到栏目信息
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable getClassList_Show(string ParentID)
        {
            string Sql = "Select ClassID,ClassCName,ParentID From " + Pre + "news_class where ParentID='" + ParentID + "' " + Foosun.Common.Public.getSessionStr() + " and isUrl=0 and isLock=0 and isRecyle=0 and isPage=0 order by OrderID desc,id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到栏目信息
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getClassContent(string ClassID)
        {
            string Sql = "Select ClassID,ClassCName,ClassEName,ParentID,IsURL,Checkint,OrderID,Urladdress,Domain,ClassTemplet,ReadNewsTemplet,SavePath,SaveClassframe,ClassSaveRule,ClassIndexRule,NewsSavePath,NewsFileRule,PicDirPath,ContentPicTF,ContentPICurl,ContentPicSize,InHitoryDay,DataLib,SiteID,NaviShowtf,NaviPIC,NaviContent,MetaKeywords,MetaDescript,isDelPoint,Gpoint,iPoint,GroupNumber,FileName,isComm,NaviPosition,NewsPosition,Defineworkey From " + Pre + "news_class where ClassID='" + ClassID + "' " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }
        /// <summary>
        /// 得到栏目下子栏目数量
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable checkHasSub(string ParentID)
        {
            string Sql = "select count(*) from " + Pre + "news_Class where ParentID ='" + ParentID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 判断 栏目重复
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getClassEname(string ClassEname)
        {
            string Sql = "Select ClassEName From " + Pre + "news_class where ClassEName='" + ClassEname + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到父类型是否合法
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getParentClass(string ClassID)
        {
            string Sql = "Select ClassID From " + Pre + "News_Class Where ClassID='" + ClassID + "' " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到自定义类型
        /// </summary>
        /// <returns></returns>
        public DataTable getdefineTable()
        {
            string Sql = "Select id,defineInfoId,defineCname From " + Pre + "Define_Data where 1=1 " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到自定义字段类型（修改）
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getdefineEditTable(string ClassID)
        {
            string Sql = "Select Defineworkey From " + Pre + "News_Class where ClassID='" + ClassID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到某个自定义字段的值
        /// </summary>
        /// <param name="TempID"></param>
        /// <returns></returns>
        public DataTable getdefineEditTablevalue(int TempID)
        {
            string Sql = "Select id,defineInfoId,defineCname,defineColumns,defineType,IsNull,defineValue,defineExpr,definedvalue From " + Pre + "Define_Data where id=" + TempID + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 插入栏目新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void insertClassContent(Foosun.Model.ClassContent uc)
        {
            string Sql = "insert into " + Pre + "News_Class(";
            Sql += "ClassID,ClassCName,ClassEName,URLaddress,ParentID,IsURL,OrderID,NaviShowtf,NaviContent,NaviPIC,MetaKeywords,MetaDescript,SiteID,isLock,isRecyle,NaviPosition,Domain,ClassTemplet,ReadNewsTemplet,SavePath,SaveClassframe,Checkint,ClassSaveRule,ClassIndexRule,NewsSavePath,NewsFileRule,PicDirPath,ContentPicTF,ContentPICurl,ContentPicSize,InHitoryDay,isDelPoint,Gpoint,iPoint,GroupNumber,FileName,isComm,NewsPosition,Defineworkey,CreatTime,isPage,ModelID,isunHTML,DataLib";
            Sql += ") values (";
            Sql += "@ClassID,@ClassCName,@ClassEName,@URLaddress,@ParentID,@IsURL,@OrderID,@NaviShowtf,@NaviContent,@NaviPIC,@MetaKeywords,@MetaDescript,@SiteID,@isLock,@isRecyle,@NaviPosition,@Domain,@ClassTemplet,@ReadNewsTemplet,@SavePath,@SaveClassframe,@Checkint,@ClassSaveRule,@ClassIndexRule,@NewsSavePath,@NewsFileRule,@PicDirPath,@ContentPicTF,@ContentPICurl,@ContentPicSize,@InHitoryDay,@isDelPoint,@Gpoint,@iPoint,@GroupNumber,@FileName,@isComm,@NewsPosition,@Defineworkey,@CreatTime,0,'0',0,'" + Pre + "News')";
            SqlParameter[] parm = insertClassContentParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }


        /// <summary>
        /// 更新栏目新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateClassContent(Foosun.Model.ClassContent uc)
        {
            string Sql = "Update " + Pre + "News_Class set ClassCName=@ClassCName,ClassEName=@ClassEName,URLaddress=@URLaddress,ParentID=@ParentID,IsURL=@IsURL,OrderID=@OrderID,NaviShowtf=@NaviShowtf,NaviContent=@NaviContent,NaviPIC=@NaviPIC,MetaKeywords=@MetaKeywords,MetaDescript=@MetaDescript,isLock=@isLock,isRecyle=@isRecyle,NaviPosition=@NaviPosition,Domain=@Domain,ClassTemplet=@ClassTemplet,ReadNewsTemplet=@ReadNewsTemplet,SavePath=@SavePath,SaveClassframe=@SaveClassframe,Checkint=@Checkint,ClassSaveRule=@ClassSaveRule,ClassIndexRule=@ClassIndexRule,NewsSavePath=@NewsSavePath,NewsFileRule=@NewsFileRule,PicDirPath=@PicDirPath,ContentPicTF=@ContentPicTF,ContentPICurl=@ContentPICurl,ContentPicSize=@ContentPicSize,InHitoryDay=@InHitoryDay,isDelPoint=@isDelPoint,Gpoint=@Gpoint,iPoint=@iPoint,GroupNumber=@GroupNumber,FileName=@FileName,isComm=@isComm,NewsPosition=@NewsPosition,Defineworkey=@Defineworkey,isPage=0,ModelID='0' where ClassID='" + uc.ClassID.ToString() + "' " + Foosun.Common.Public.getSessionStr() + "";

            SqlParameter[] parm = insertClassContentParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取ClassContent构造
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private SqlParameter[] insertClassContentParameters(Foosun.Model.ClassContent uc)
        {
            SqlParameter[] param = new SqlParameter[41];
            param[0] = new SqlParameter("@Defineworkey", SqlDbType.NVarChar, 255);
            param[0].Value = uc.Defineworkey;
            param[1] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
            param[1].Value = uc.ClassID;
            param[2] = new SqlParameter("@ClassCName", SqlDbType.NVarChar, 50);
            param[2].Value = uc.ClassCName;
            param[3] = new SqlParameter("@ClassEName", SqlDbType.NVarChar, 50);
            param[3].Value = uc.ClassEName;
            param[4] = new SqlParameter("@ParentID", SqlDbType.NVarChar, 12);
            param[4].Value = uc.ParentID;
            param[5] = new SqlParameter("@IsURL", SqlDbType.TinyInt, 1);
            param[5].Value = uc.IsURL;
            param[6] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            param[6].Value = uc.OrderID;
            param[7] = new SqlParameter("@URLaddress", SqlDbType.NVarChar, 200);
            param[7].Value = uc.URLaddress;
            param[8] = new SqlParameter("@Domain", SqlDbType.NVarChar, 150);
            param[8].Value = uc.Domain;
            param[9] = new SqlParameter("@ClassTemplet", SqlDbType.NVarChar, 200);
            param[9].Value = uc.ClassTemplet;
            param[10] = new SqlParameter("@ReadNewsTemplet", SqlDbType.NVarChar, 200);
            param[10].Value = uc.ReadNewsTemplet;
            param[11] = new SqlParameter("@SavePath", SqlDbType.NVarChar, 50);
            param[11].Value = uc.SavePath;
            param[12] = new SqlParameter("@SaveClassframe", SqlDbType.NVarChar, 200);
            param[12].Value = uc.SaveClassframe;
            param[13] = new SqlParameter("@Checkint", SqlDbType.TinyInt, 1);
            param[13].Value = uc.Checkint;
            param[14] = new SqlParameter("@ClassSaveRule", SqlDbType.NVarChar, 200);
            param[14].Value = uc.ClassSaveRule;
            param[15] = new SqlParameter("@ClassIndexRule", SqlDbType.NVarChar, 50);
            param[15].Value = uc.ClassIndexRule;
            param[16] = new SqlParameter("@NewsSavePath", SqlDbType.NVarChar, 50);
            param[16].Value = uc.NewsSavePath;
            param[17] = new SqlParameter("@NewsFileRule", SqlDbType.NVarChar, 200);
            param[17].Value = uc.NewsFileRule;
            param[18] = new SqlParameter("@PicDirPath", SqlDbType.NVarChar, 50);
            param[18].Value = uc.PicDirPath;
            param[19] = new SqlParameter("@ContentPicTF", SqlDbType.TinyInt, 1);
            param[19].Value = uc.ContentPicTF;
            param[20] = new SqlParameter("@ContentPICurl", SqlDbType.NVarChar, 200);
            param[20].Value = uc.ContentPICurl;
            param[21] = new SqlParameter("@ContentPicSize", SqlDbType.NVarChar, 15);
            param[21].Value = uc.ContentPicSize;
            param[22] = new SqlParameter("@InHitoryDay", SqlDbType.Int, 4);
            param[22].Value = uc.InHitoryDay;
            param[24] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[24].Value = uc.SiteID;
            param[25] = new SqlParameter("@NaviShowtf", SqlDbType.TinyInt, 1);
            param[25].Value = uc.NaviShowtf;
            param[26] = new SqlParameter("@NaviPIC", SqlDbType.NVarChar, 200);
            param[26].Value = uc.NaviPIC;
            param[27] = new SqlParameter("@NaviContent", SqlDbType.NVarChar, 255);
            param[27].Value = uc.NaviContent;
            param[28] = new SqlParameter("@MetaKeywords", SqlDbType.NVarChar, 200);
            param[28].Value = uc.MetaKeywords;
            param[29] = new SqlParameter("@MetaDescript", SqlDbType.NVarChar, 200);
            param[29].Value = uc.MetaDescript;
            param[30] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt, 1);
            param[30].Value = uc.isDelPoint;
            param[31] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[31].Value = uc.Gpoint;
            param[32] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[32].Value = uc.iPoint;
            param[33] = new SqlParameter("@GroupNumber", SqlDbType.NVarChar, 255);
            param[33].Value = uc.GroupNumber;
            param[34] = new SqlParameter("@FileName", SqlDbType.NVarChar, 6);
            param[34].Value = uc.FileName;
            param[35] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[35].Value = uc.isLock;
            param[36] = new SqlParameter("@isRecyle", SqlDbType.TinyInt, 1);
            param[36].Value = uc.isRecyle;
            param[37] = new SqlParameter("@NaviPosition", SqlDbType.NVarChar, 255);
            param[37].Value = uc.NaviPosition;
            param[38] = new SqlParameter("@NewsPosition", SqlDbType.NVarChar, 255);
            param[38].Value = uc.NewsPosition;
            param[39] = new SqlParameter("@isComm", SqlDbType.TinyInt, 1);
            param[39].Value = uc.isComm;
            param[40] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[40].Value = uc.CreatTime;

            return param;
        }

        /// <summary>
        /// 删除栏目到回收站
        /// </summary>
        /// <param name="ClassID"></param>
        public void del_recyleClass(string ClassID)
        {
            string str_sql = "Update " + Pre + "news_Class Set isRecyle=1 Where ClassID ='" + ClassID.ToString() + "' " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
            updateClass_News(ClassID);
        }

        /// <summary>
        /// 彻底删除栏目
        /// </summary>
        /// <param name="ClassID"></param>
        public void del_Class(string ClassID)
        {
            string str_sql = "Delete From " + Pre + "news_Class Where ClassID ='" + ClassID.ToString() + "' " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
            delClass_News(ClassID);
        }

        /// <summary>
        /// 得到栏目下的子类并彻底删除
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>

        public void GetChildClassdel(string ParentID)
        {
            string Sql = "select ClassID from " + Pre + "news_Class where ParentID = '" + ParentID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    del_Class(dt.Rows[0]["ClassID"].ToString());
                    GetChildClassdel(dt.Rows[0]["ClassID"].ToString());
                }
                dt.Clear(); dt.Dispose();
            }
        }

        /// <summary>
        /// 得到子类新闻
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public DataTable getSubNewsID(string NewsID)
        {
            string Sql = "Select NewsID,getNewsID,NewsTitle,DataLib,TitleColor,TitleBTF,TitleITF,colsNum From " + Pre + "news_sub Where NewsID='" + NewsID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到栏目下的子类并删除到回收站
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>

        public void GetChildClassdel_recyle(string ParentID)
        {
            string Sql = "select ClassID from " + Pre + "news_Class where ParentID = '" + ParentID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    del_recyleClass(dt.Rows[0]["ClassID"].ToString());
                    GetChildClassdel_recyle(dt.Rows[0]["ClassID"].ToString());
                }
                dt.Clear(); dt.Dispose();
            }
        }


        /// <summary>
        /// 删除栏目同时删除新闻
        /// </summary>
        /// <param name="ClassID"></param>
        public void delClass_News(string ClassID)
        {
            //删除新闻
            //string getSQL = "select TableName from " + Pre + "sys_newsIndex order by id desc";
            //DataTable dt = DbHelper.ExecuteTable(CommandType.Text, getSQL, null);
            //if (dt != null)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            string NewsSQL = "Delete From " + Pre + "news Where ClassID ='" + ClassID.ToString() + "' and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, NewsSQL, null);
            //    }
            //    dt.Clear(); dt.Dispose();
            //}
        }

        /// <summary>
        /// 删除栏目同时更新新闻到回收站
        /// </summary>
        /// <param name="ClassID"></param>
        public void updateClass_News(string ClassID)
        {
            //删除新闻
            //string getSQL = "select TableName from " + Pre + "sys_newsIndex order by id desc";
            //DataTable dt = DbHelper.ExecuteTable(CommandType.Text, getSQL, null);
            //if (dt != null)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            string NewsSQL = "Update " + Pre + "news set isRecyle=1 Where ClassID ='" + ClassID.ToString() + "' and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, NewsSQL, null);
            //    }
            //    dt.Clear(); dt.Dispose();
            //}
        }


        /// <summary>
        /// 得到栏目列表的子类
        /// </summary>
        /// <returns></returns>
        public DataTable getChildList(string ParentID)
        {
            string Sql = "Select id,ClassID,ClassCName,ClassEname,ParentID,OrderID,IsURL,IsLock,[Domain],NaviShowtf,isPage,ClassTemplet,ReadNewsTemplet From " + Pre + "News_Class Where isRecyle<>1 and ParentID='" + ParentID + "' order by OrderId desc,id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到栏目列表的子类
        /// </summary>
        /// <returns></returns>
        public void ChangeLock(string ClassID, int NUM)
        {
            string str_sql = "Update " + Pre + "news_Class Set isLock=" + NUM + "  Where ClassID='" + ClassID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 得到栏目列表的子类
        /// </summary>
        /// <returns></returns>
        public DataTable getLock(string ClassID)
        {
            string Sql = "Select isLock From " + Pre + "news_Class Where ClassID='" + ClassID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 复位所有栏目
        /// </summary>
        /// <param name="ClassID"></param>
        public void resetClass()
        {
            string str_sql = "Update " + Pre + "news_Class Set ParentID=0 Where isLock=0 and isRecyle=0 and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="ClassID">栏目ID</param>
        public void resetOrder(int OrderID, string ClassID)
        {
            string str_sql = "Update " + Pre + "news_Class Set OrderID=" + OrderID + " Where ClassID='" + ClassID + "' and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 得到源栏目列表
        /// </summary>
        /// <returns></returns>
        public DataTable getSouceClass()
        {
            string Sql = "Select ClassID,ClassCName,ParentID From " + Pre + "news_Class where SiteID='" + Foosun.Global.Current.SiteID + "' and IsURL=0";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 删除源栏目
        /// </summary>
        /// <param name="ClassID"></param>
        public void delSouce(string ClassID)
        {
            string str_sql = "Delete From " + Pre + "news_Class Where ClassID='" + ClassID + "' and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 更新目标栏目
        /// </summary>
        /// <param name="ClassID"></param>
        public void updateSouce(string sClassID, string tClassID)
        {
            //string getcsql = "select ClassID from " + Pre + "news_class where ClassID='" + sClassID + "'";
            //DataTable dt = DbHelper.ExecuteTable(CommandType.Text, getcsql, null);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            string usql = "update " + Pre + "news_class set ParentID='" + tClassID + "' where ParentID='" + sClassID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, usql, null);
            string str_sql = "Update " + Pre + "News Set ClassID='" + tClassID + "' Where ClassID='" + sClassID + "' and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
            //    dt.Clear(); dt.Dispose();
            //}
        }

        /// <summary>
        /// 更新目标下新闻
        /// </summary>
        /// <param name="ClassID"></param>
        public void updateSouce1(string sClassID, string tClassID)
        {
            string str_sql = "Update " + Pre + "News Set ClassID='" + tClassID + "' Where ClassID='" + sClassID + "'  and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 更新目标下新闻
        /// </summary>
        /// <param name="ClassID"></param>
        public void changeParent(string sClassID, string tClassID)
        {
            string str_sql = "Update " + Pre + "News_Class Set ParentID='" + tClassID + "' Where ClassID='" + sClassID + "'  and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 初始化栏目
        /// </summary>
        /// <param name="ClassID"></param>
        public void delClassAll()
        {
            string str_sql = "delete From " + Pre + "News_Class Where SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);

            //string Sql2 = "Select TableName From " + Pre + "sys_newsIndex order by id desc";
            //DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql2, null);
            //if (rdr != null)
            //{
            //    if (rdr.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < rdr.Rows.Count; i++)
            //        {
            string str_sql1 = "delete From " + Pre + "news Where SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql1, null);
            //        }
            //        rdr.Clear(); rdr.Dispose();
            //    }
            //}
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="ClassID"></param>
        public void clearNewsInfo(string ClassId)
        {
            string str_sql = "delete From " + Pre + "News where ClassID='" + ClassId.ToString() + "'" + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);

            //string Sql2 = "Select TableName From " + Pre + "sys_newsIndex order by id desc";
            //DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql2, null);
            //if (rdr != null)
            //{
            //    if (rdr.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < rdr.Rows.Count; i++)
            //        {
            //            string str_sql1 = "delete From " + rdr.Rows[i]["TableName"].ToString() + " Where ClassID='" + ClassId.ToString() + "'" + Foosun.Common.Public.getSessionStr() + "";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql1, null);
            //        }
            //        rdr.Clear(); rdr.Dispose();
            //    }
            //}
        }

        /// <summary>
        /// 得到栏信息（批量设置属性）
        /// </summary>
        /// <returns></returns>
        public DataTable getClassInfo_Templet()
        {
            string Sql = "Select ClassID,ClassCname,ParentID From " + Pre + "News_Class where SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="strUpdate"></param>
        /// <param name="_str"></param>
        public void UpdateClassInfo(string strUpdate, string _str)
        {
            string Sql = "update " + Pre + "News_Class Set " + strUpdate + " where ClassID in (" + _str + ") " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 更新所有的表
        /// </summary>
        /// <param name="templet"></param>
        /// <param name="_str"></param>
        public void UpdateClassNewsInfo(string templet, string _str)
        {
            //string tbSQL = "select TableName from " + Pre + "sys_newsIndex Order by id desc";
            //DataTable dt = DbHelper.ExecuteTable(CommandType.Text, tbSQL, null);
            //if (dt != null)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            string Sql = "update " + Pre + "news Set Templet = '" + templet + "' where ClassID in (" + _str + ") " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            //    }
            //    dt.Clear(); dt.Dispose();
            //}
        }

        /// <summary>
        /// 更新权重
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="OrderID"></param>
        public void updateOrderP(string ClassID, int OrderID)
        {
            string Sql = "update " + Pre + "News_Class Set OrderID=" + OrderID + " where ClassID ='" + ClassID + "' " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 得到单页面
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getPageContent(string ClassID)
        {
            string Sql = "Select * From " + Pre + "News_Class where ClassID='" + ClassID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 添加单页面
        /// </summary>
        /// <param name="uc"></param>
        public void insertPage(Foosun.Model.PageContent uc)
        {
            string Sql = "insert into " + Pre + "News_Class(";
            Sql += "ClassID,ClassCName,ClassEName,ParentID,IsURL,OrderID,NaviShowtf,MetaKeywords,MetaDescript,SiteID,isLock,isRecyle,ClassTemplet,SavePath,isDelPoint,Gpoint,iPoint,GroupNumber,CreatTime,isPage,PageContent,InHitoryDay,ContentPicTF,Checkint,ModelID,isunHTML,DataLib";
            Sql += ") values (";
            Sql += "@ClassID,@ClassCName,@ClassEName,@ParentID,@IsURL,@OrderID,@NaviShowtf,@MetaKeywords,@MetaDescript,@SiteID,0,0,@ClassTemplet,@SavePath,@isDelPoint,@Gpoint,@iPoint,@GroupNumber,@CreatTime,@isPage,@Content,0,0,0,'0',0,'" + Pre + "news')";
            SqlParameter[] parm = insertPageContentParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 更新单页面
        /// </summary>
        /// <param name="uc"></param>
        public void updatePage(Foosun.Model.PageContent uc)
        {
            string Sql = "Update " + Pre + "News_Class set ClassCName=@ClassCName,ParentID=@ParentID,IsURL=@IsURL,OrderID=@OrderID,NaviShowtf=@NaviShowtf,MetaKeywords=@MetaKeywords,MetaDescript=@MetaDescript,ClassTemplet=@ClassTemplet,SavePath=@SavePath,isDelPoint=@isDelPoint,Gpoint=@Gpoint,iPoint=@iPoint,GroupNumber=@GroupNumber,isPage=@isPage,PageContent=@Content,InHitoryDay=0,ContentPicTF=0,Checkint=0,ModelID='0' where ClassID='" + uc.ClassID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";

            SqlParameter[] parm = insertPageContentParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取PageContent构造
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private SqlParameter[] insertPageContentParameters(Foosun.Model.PageContent uc)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
            param[0].Value = uc.ClassID;
            param[1] = new SqlParameter("@ClassCName", SqlDbType.NVarChar, 50);
            param[1].Value = uc.ClassCName;
            param[2] = new SqlParameter("@ClassEName", SqlDbType.NVarChar, 50);
            param[2].Value = uc.ClassEName;
            param[3] = new SqlParameter("@ParentID", SqlDbType.NVarChar, 12);
            param[3].Value = uc.ParentID;
            param[4] = new SqlParameter("@IsURL", SqlDbType.TinyInt, 1);
            param[4].Value = uc.IsURL;
            param[5] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            param[5].Value = uc.OrderID;
            param[6] = new SqlParameter("@Content", SqlDbType.NText);
            param[6].Value = uc.Content;
            param[7] = new SqlParameter("@ClassTemplet", SqlDbType.NVarChar, 200);
            param[7].Value = uc.ClassTemplet;
            param[8] = new SqlParameter("@SavePath", SqlDbType.NVarChar, 50);
            param[8].Value = uc.SavePath;
            param[9] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[9].Value = uc.SiteID;
            param[10] = new SqlParameter("@NaviShowtf", SqlDbType.TinyInt, 1);
            param[10].Value = uc.NaviShowtf;
            param[11] = new SqlParameter("@MetaKeywords", SqlDbType.NVarChar, 200);
            param[11].Value = uc.MetaKeywords;
            param[12] = new SqlParameter("@MetaDescript", SqlDbType.NVarChar, 200);
            param[12].Value = uc.MetaDescript;
            param[13] = new SqlParameter("@isDelPoint", SqlDbType.TinyInt, 1);
            param[13].Value = uc.isDelPoint;
            param[14] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[14].Value = uc.Gpoint;
            param[15] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[15].Value = uc.iPoint;
            param[16] = new SqlParameter("@GroupNumber", SqlDbType.NVarChar, 250);
            param[16].Value = uc.GroupNumber;
            param[17] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[17].Value = uc.CreatTime;
            param[18] = new SqlParameter("@isPage", SqlDbType.TinyInt, 1);
            param[18].Value = uc.isPage;
            return param;
        }


        #endregion 栏目

        #region 新闻列表
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="SpecialID">专题编号</param>
        /// <param name="Editor">作者</param>
        /// <param name="NewsDbTbs">表名</param>
        /// <param name="ClassID">栏目</param>
        /// <param name="sKeywrd">关键字</param>
        /// <param name="DdlKwdType">关键字类型</param>
        /// <param name="sChooses">提交的类型</param>
        /// <param name="SiteID">站点</param>
        /// <param name="TablePrefix">表扩展名</param>
        /// <param name="PageIndex">每页数量</param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="PageCount"></param>
        /// <param name="SqlCondition">SQL</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetPage(string SpecialID, string Editor, string ClassID, string sKeywrd, string DdlKwdType, string sChooses, string SiteID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string sFilter = " where a.isRecyle=0";
            if (ClassID != null && ClassID != "")
                sFilter += " and a.ClassID='" + ClassID + "'";
            if (SpecialID != null && SpecialID != "")
                sFilter += " and a.NewsID In (Select NewsID From " + Pre + "special_news Where SpecialID='" + SpecialID + "') ";
            string sKeywrds = sKeywrd;
            if (sKeywrds != "" && sKeywrds != null)
            {
                switch (DdlKwdType)
                {
                    case "content":
                        sFilter += " and Content like '%" + sKeywrds + "%'";
                        break;
                    case "author":
                        sFilter += " and Author like '%" + sKeywrds + "%'";
                        break;
                    case "editor":
                        sFilter += " and NewsTitle like '%" + sKeywrds + "%'";
                        break;
                    default:
                        sFilter += " and NewsTitle like '%" + sKeywrds + "%'";
                        break;
                }
            }
            string sChoose = sChooses;
            switch (sChoose)
            {
                case "Auditing":
                    sFilter += " and (CheckStat is null or CheckStat='0|0|0|0' or CheckStat='1|0|0|0' or  CheckStat='2|0|0|0' or  CheckStat='3|0|0|0')";
                    break;
                case "UnAuditing":
                    sFilter += " and (CheckStat<>'0|0|0|0' and CheckStat<>'1|0|0|0' and CheckStat<>'2|0|0|0' and CheckStat<>'3|0|0|0' and CheckStat is not null)";
                    break;
                case "All":
                    break;
                case "Contribute":
                    sFilter += " and a.isConstr=1";
                    break;
                case "Commend":
                    sFilter += " and SUBSTRING(NewsProperty,1,1)='1'";
                    break;
                case "Lock":
                    sFilter += " and a.isLock=1";
                    break;
                case "UnLock":
                    sFilter += " and a.isLock=0";
                    break;
                case "Top":
                    sFilter += " and a.OrderID=10";
                    break;
                case "Hot":
                    sFilter += " and SUBSTRING(NewsProperty,5,1)='1'";
                    break;
                case "Splendid":
                    sFilter += " and SUBSTRING(NewsProperty,15,1)='1'";
                    break;
                case "Headline":
                    sFilter += " and SUBSTRING(NewsProperty,9,1)='1'";
                    break;
                case "Slide":
                    sFilter += " and SUBSTRING(NewsProperty,7,1)='1'";
                    break;
                case "my":
                    sFilter += " and Editor='" + Foosun.Global.Current.UserName + "'";
                    break;
                case "isHtml":
                    sFilter += " and a.isHtml=1";
                    break;
                case "unisHtml":
                    sFilter += " and a.isHtml=0";
                    break;
                case "discuzz":
                    sFilter += " and a.DiscussTF=1";
                    break;
                case "commat":
                    sFilter += " and a.CommTF=1";
                    break;
                case "voteTF":
                    sFilter += " and a.VoteTF=1";
                    break;
                case "contentPicTF":
                    sFilter += " and a.ContentPicTF=1";
                    break;
                case "POPTF":
                    sFilter += " and a.isDelPoint!=0";
                    break;
                case "Pic":
                    sFilter += " and a.NewsType=1";
                    break;
                case "FilesURL":
                    sFilter += " and a.isFiles=1";
                    break;
            }
            if (SiteID != "" && SiteID != null)
            {
                sFilter += " and a.SiteID='" + SiteID + "'";
            }
            else
            {
                sFilter += " and a.SiteID='" + Foosun.Global.Current.SiteID + "'";
            }

            if (Editor != "")
            {
                sFilter += " and a.Editor='" + Editor + "'";
            }
            string AllFields = "a.Id,a.NewsID,a.NewsType,a.TitleColor,a.TitleITF,a.TitleBTF,a.Author,a.DataLib,a.OrderID,a.NewsTitle,a.ishtml,a.Editor,a.Click,a.isConstr,a.ClassID,a.isLock,a.NewsProperty,a.CheckStat,a.URLaddress,b.UserName,c.ClassCName";
            string Condition = Pre + "News a left join " + Pre + "sys_User b on a.Editor=b.UserName left join " + Pre + "News_Class c on a.ClassID=c.ClassID " + sFilter;
            string IndexField = "a.Id";
            string OrderFields = "order by a.OrderID desc,a.Id desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        /// <summary>
        /// 得到站点列表
        /// </summary>
        /// <returns></returns>
        public DataTable getSiteList()
        {
            string Sql = "select ID,ChannelID,CName from " + Pre + "news_site where IsURL=0 and isRecyle=0 and isLock=0 order by id";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }
        /// <summary>
        /// 得到站点参数
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public DataTable getSiteParam(string SiteID)
        {
            string Sql = "select ClassTemplet,ReadNewsTemplet,SaveFileRule,SaveDirPath,SaveDirRule,PicSavePath,DataLib from " + Pre + "news_site where IsURL=0 and isRecyle=0 and isLock=0 and ChannelID=@ChannelID";
            SqlParameter Param = new SqlParameter("@ChannelID", SiteID);
            return DbHelper.ExecuteTable(CommandType.Text, Sql, Param);
        }
        /// <summary>
        /// 更新新闻到回收站
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Del_news(string id)
        {
            string Sql = "update " + Pre + "News set isRecyle=1 where Id in (" + id + ") " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 得到新闻的路径
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string sel_path(string id)
        {
            string Sql = "select SavePath from " + Pre + "News where id=" + id;
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        /// <summary>
        /// 彻底删除新闻
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Id"></param>
        public void Del_newsc(string Id)
        {
            string dSql = "select NewsID from " + Pre + "News where id=" + Id;
            object obj = DbHelper.ExecuteScalar(CommandType.Text, dSql, null);
            if (obj != null && obj != DBNull.Value)
            {
                delSubID(obj.ToString());
            }
            string Sql = "delete from " + Pre + "News where Id=" + Id + " " + Foosun.Common.Public.getSessionStr();
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 更新新闻状态
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="id"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int Update_Lock(string id, int nums)
        {
            string Sql = "update " + Pre + "News set isLock=" + nums + " where Id in (" + id + ") " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 重置权重
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Update_ResetOrde(string id)
        {
            string Sql = "update " + Pre + "News set OrderID=0 where Id in (" + id + ") " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 得到归档的最新新闻
        /// </summary>
        /// <returns></returns>
        public DataTable sel_old_News()
        {
            string Sql = "select top 1 * from " + Pre + "old_News order by id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 得到归档数字并归档
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable sel_old_classNews(string ClassID)
        {
            string SQLs = "select id,CreatTime from " + Pre + "News Where ClassID='" + ClassID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, SQLs, null);
        }

        public int sel_old_classInHitoryDay(string ClassID)
        {
            int intflg = 180;
            string SQL = "select InHitoryDay from " + Pre + "News_Class Where ClassID='" + ClassID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, SQL, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                intflg = int.Parse(dt.Rows[0]["InHitoryDay"].ToString());
                dt.Clear(); dt.Dispose();
            }
            return intflg;
        }

        public int Add_old_News(string fieldnm, string id, DateTime oldtime)
        {
            string Sql = "insert into " + Pre + "old_News (" + fieldnm + ",oldtime,DataLib) select " + fieldnm + ",oldtime='" + oldtime + "',DataLib='" + Pre + "News' from " + Pre + "News where Id in (" + id + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int del_new_News(string id)
        {
            string Sql = "delete from " + Pre + "News where Id in (" + id + ") " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int settop(string id)
        {
            string Sql = "update " + Pre + "News set OrderID=10 where  Id=" + id + " " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int unsettop(string id)
        {
            string Sql = "update " + Pre + "News set OrderID=0 where Id=" + id + "" + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public int delNumber(string ClassID)
        {
            int delCount = int.Parse(Foosun.Common.Public.readparamConfig("delinfoNumber", "refresh"));
            string whereStr = "";
            if (delCount != 0)
            {
                string cSQL = "select count(id) from " + Pre + "News where ClassID='" + ClassID + "' " + Foosun.Common.Public.getSessionStr() + "";
                int tCount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, cSQL, null));
                if (delCount < tCount)
                {
                    string gSQL = "select top " + delCount + " id from " + Pre + "News where ClassID='" + ClassID + "' " + Foosun.Common.Public.getSessionStr() + " order by id desc";
                    int gCount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, gSQL, null));
                    whereStr = " and id>=" + gCount + "";
                }
            }
            string Sql = "delete from " + Pre + "News where ClassID='" + ClassID + "' " + whereStr + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_JS()
        {
            string Sql = "select JsID,JSName from " + Pre + "News_JS where SiteID='" + Foosun.Global.Current.SiteID + "' and jsType=1";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_JSNews(string NewsID)
        {
            string Sql = "select NewsID,NewsTitle,PicURL,ClassID,CreatTime from " + Pre + "News where SiteID='" + Foosun.Global.Current.SiteID + "' and Id='" + NewsID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add_JSFile(string JsID, string Njf_title, string NewsId, string PicPath, string ClassId, string SiteID, DateTime CreatTime, DateTime TojsTime)
        {
            string Sql = "insert into " + Pre + "News_JSFile(JsID,Njf_title,NewsId,NewsTable,PicPath,ClassId,SiteID,CreatTime,TojsTime) values('" + JsID + "','" + Njf_title + "','" + NewsId + "','" + Pre + "News','" + PicPath + "','" + ClassId + "','" + SiteID + "','" + CreatTime + "','" + TojsTime + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_News_Class()
        {
            string Sql = "select ClassID,ClassCName,ParentID from " + Pre + "News_Class where isURL=0 and isLock=0 and isRecyle=0 and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_LblNewsTable(string LblNewsTable, string s)
        {
            string Sql = "select Id,NewsID,NewsTitle from " + LblNewsTable + " where Id in ('" + s + "')";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_PID(string PID)
        {
            string Sql = "select ClassID,ClassCName,ParentID from " + Pre + "News_Class where ParentID='" + PID + "' and isURL=0 and isLock=0 and isRecyle=0 and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int del_move(string sTb, string sOrgNews)
        {
            string Sql = "delete " + sTb + " where newsID = " + sOrgNews + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int del_classmove(string sTb, string sOrgNews)
        {
            string Sql = "delete " + sTb + " where newsID ='" + sOrgNews + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_sys_NewsIndex(string ClassID)
        {
            string Sql = "select DataLib from " + Pre + "news_Class where ClassID='" + ClassID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int sel_newsclass(string cid)
        {
            string Sql = "select count(*) from " + Pre + "news_class where IsURL=1 and ClassID='" + cid + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }
        public DataTable sel_NewsTitle(string sTb, string ClassID)
        {
            string Sql = "select Id,NewsTitle,Content from " + sTb + " where ClassID='" + ClassID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel1(string sTb, string sOrgNews)
        {
            string Sql = "select Id,NewsTitle,Content from " + sTb + " where Id in (" + sOrgNews + ")";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public string sel_copy_clsaa(string ClassID)
        {
            string Sql = "select DataLib from " + Pre + "news_Class where ClassID='" + ClassID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }

        public string getFileNameInfo(string NewsID, string DataLib)
        {
            string flg = Foosun.Common.Rand.Number(5);
            NewsID = NewsID.Replace("'", "");
            string Sql = "select FileName from " + DataLib + " where NewsID ='" + NewsID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                flg = dt.Rows[0]["FileName"].ToString() + "_1";
                dt.Clear(); dt.Dispose();
            }
            return flg;
        }


        public void Copy_news(string ClassID, string DataLib, string sOrgNews, string sTb, string NewsID, string FileName)
        {
            string Sql = "insert into " + DataLib + "(NewsID,NewsType,OrderID,NewsTitle,sNewsTitle,TitleColor,TitleITF,TitleBTF,CommLinkTF,SubNewsTF,URLaddress,PicURL,SPicURL,ClassID,SpecialID,Author,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,naviContent,Click,CreatTime,EditTime,SavePath,FileName,FileEXName,ContentPicTF,ContentPicURL,ContentPicSize,CommTF,DiscussTF,TopNum,VoteTF,CheckStat,isLock,isRecyle,SiteID,DataLib,DefineID,isVoteTF,Editor,isHtml,isDelPoint,Gpoint,iPoint,GroupNumber,isConstr)select '" + NewsID + "',NewsType,OrderID,NewsTitle,sNewsTitle,TitleColor,TitleITF,TitleBTF,CommLinkTF,SubNewsTF,URLaddress,PicURL,SPicURL,'" + ClassID + "',SpecialID,Author,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,naviContent,Click,CreatTime,EditTime,SavePath,FileName,FileEXName,ContentPicTF,ContentPicURL,ContentPicSize,CommTF,DiscussTF,TopNum,VoteTF,CheckStat,isLock,isRecyle,SiteID,'" + DataLib + "',DefineID,isVoteTF,Editor,isHtml,isDelPoint,Gpoint,iPoint,GroupNumber,isConstr from " + sTb + " where Newsid =" + sOrgNews + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (FileName.Trim() != "")
            {
                string gSQL = "select id from " + DataLib + " where ClassID='" + ClassID + "' and FileName='" + FileName + "'";
                DataTable dt = DbHelper.ExecuteTable(CommandType.Text, gSQL, null);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0) { FileName = FileName + "1"; }
                    dt.Clear(); dt.Dispose();
                }
                string tSQL = "update " + DataLib + " set FileName='" + FileName + "',isHtml=0 where NewsID='" + NewsID + "'";
                DbHelper.ExecuteNonQuery(CommandType.Text, tSQL, null);
            }
        }

        public DataTable sel_copy_classnews(string NewsTable, string ClassID)
        {
            string Sql = "select id,NewsID from " + NewsTable + " where ClassID='" + ClassID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public void Copy_ClassNews(string ClassID, string DataLib, string sOrgNews, string sTb, string NewsID, string FileName)
        {
            string Sql = "insert into " + DataLib + "(NewsID,NewsType,OrderID,NewsTitle,sNewsTitle,TitleColor,TitleITF,TitleBTF,CommLinkTF,SubNewsTF,URLaddress,PicURL,SPicURL,ClassID,SpecialID,Author,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,naviContent,Click,CreatTime,EditTime,SavePath,FileName,FileEXName,ContentPicTF,ContentPicURL,ContentPicSize,CommTF,DiscussTF,TopNum,VoteTF,CheckStat,isLock,isRecyle,SiteID,DataLib,DefineID,isVoteTF,Editor,isHtml,isDelPoint,Gpoint,iPoint,GroupNumber,isConstr)select '" + NewsID + "',NewsType,OrderID,NewsTitle,sNewsTitle,TitleColor,TitleITF,TitleBTF,CommLinkTF,SubNewsTF,URLaddress,PicURL,SPicURL,'" + ClassID + "',SpecialID,Author,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,naviContent,Click,CreatTime,EditTime,SavePath,FileName,FileEXName,ContentPicTF,ContentPicURL,ContentPicSize,CommTF,DiscussTF,TopNum,VoteTF,CheckStat,isLock,isRecyle,SiteID,'" + DataLib + "',DefineID,isVoteTF,Editor,isHtml,isDelPoint,Gpoint,iPoint,GroupNumber,isConstr from " + sTb + " where newsid ='" + sOrgNews + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (FileName.Trim() != "")
            {
                string gSQL = "select id from " + DataLib + " where ClassID='" + ClassID + "' and FileName='" + FileName + "'";
                DataTable dt = DbHelper.ExecuteTable(CommandType.Text, gSQL, null);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0) { FileName = FileName + "1"; }
                    dt.Clear(); dt.Dispose();
                }
                string tSQL = "update " + DataLib + " set FileName='" + FileName + "',isHTML=0 where NewsID='" + NewsID + "'";
                DbHelper.ExecuteNonQuery(CommandType.Text, tSQL, null);
            }
        }

        public int Up_news1(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string sOrgNews)
        {
            string Sql = "Update " + sTb + " set NewsProperty='" + NewsProperty + "',Templet='" + Templet + "',OrderID=" + OrderID + ",CommLinkTF=" + CommLinkTF + ",Click=" + Click + ",FileEXName='" + FileEXName + "',CommTF=" + CommTF + ",DiscussTF=" + DiscussTF + " where NewsID in (" + sOrgNews + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public int Up_news2(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string sOrgNews)
        {
            if (NewsProperty == "" && Templet == "" && OrderID == 0 && CommLinkTF == 0 && Click == 0 && FileEXName == "")
            {
                return 0;
            }
            else
            {
                string Sql = "Update " + sTb + " set ";
                Sql += "CommTF=" + CommTF + ",";
                Sql += "DiscussTF=" + DiscussTF + ",";
                if (NewsProperty != "")
                {
                    Sql += "NewsProperty='" + NewsProperty + "',";
                }
                if (Templet != "")
                {
                    Sql += "Templet='" + Templet + "',";
                }
                if (OrderID != 0)
                {
                    Sql += "OrderID=" + OrderID + ",";
                }
                if (CommLinkTF != 0)
                {
                    Sql += "CommLinkTF=" + CommLinkTF + ",";
                }
                if (Click != 0)
                {
                    Sql += "Click=" + Click + ",";
                }
                if (FileEXName != "")
                {
                    Sql += "FileEXName='" + FileEXName + "'";
                }
                Sql = Foosun.Common.Public.Lost(Sql) + " where NewsID in (" + sOrgNews + ")";
                return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            }

        }
        public void Up_Classnews(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string ClassID, string Tags, string Souce)
        {
            //if (NewsProperty == "" && Templet == "" && OrderID == 0 && CommLinkTF == 0 && Click == 0 && FileEXName == "")
            string Sql = "Update " + sTb + " set ";
            Sql += "CommTF=" + CommTF;
            Sql += ",DiscussTF=" + DiscussTF;
            if (NewsProperty != "")
            {
                Sql += ",NewsProperty='" + NewsProperty + "'";
            }
            if (Templet != "")
            {
                Sql += ",Templet='" + Templet + "'";
            }
            if (OrderID != 0)
            {
                Sql += ",OrderID=" + OrderID + "";
            }
            if (CommLinkTF != 0)
            {
                Sql += ",CommLinkTF=" + CommLinkTF + "";
            }
            if (Click != 0)
            {
                Sql += ",Click=" + Click + "";
            }
            if (FileEXName != "")
            {
                Sql += ",FileEXName='" + FileEXName + "'";
            }
            if (Tags.Trim() != "")
            {
                Sql += ",Tags='" + Tags + "'";
            }
            if (Souce.Trim() != "")
            {
                Sql += ",Souce='" + Souce + "'";
            }
            Sql += " where ClassID = '" + ClassID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int sel_NewsID(string NewsID)
        {
            string Sql = "select count(ID) from " + Pre + "News where NewsID=@NewsID";
            SqlParameter Param = new SqlParameter("@NewsID", NewsID);
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, Param);
        }
        public string sel_sclasstext(string ClassID)
        {
            string Sql = "select ClassCName from " + Pre + "news_Class where ClassID='" + ClassID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        public DataTable sle_PicUrl(string ID, string tb)
        {
            string Sql = "select PicURL,SPicUrl from " + tb + " where Id='" + ID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Up_PicURL(string PicURL, string SPicURL, string ID, string tb)
        {
            string Sql = "update " + tb + " set PicURL='" + SPicURL + "',SPicURL='" + PicURL + "' where Id='" + ID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public void allCheck(int[] id)
        {

            string ids = string.Empty;
            foreach (int i in id)
            {
                ids += i + ",";
            }
            string Sql = "update " + Pre + "news SET checkstat = CASE WHEN checkstat IS NULL THEN '0|0|0|0' ";
            Sql += "when checkstat<>'0|0|0|0' then SUBSTRING(checkstat,1,1)+'|0|0|0' else '0|0|0|0' END,islock=0";
            Sql += " where Id in (" + ids.TrimEnd(',') + ") " + Foosun.Common.Public.getSessionStr();
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public void upCheckStat(string getID, int levelsID)
        {
            string _CheckStat = "0|0|0|0";
            string GSql = "select CheckStat from " + Pre + "News where Id = " + getID + "";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, GSql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _CheckStat = dt.Rows[0]["CheckStat"].ToString();
                }
                dt.Clear(); dt.Dispose();
            }
            string[] checkStatARR = _CheckStat.Split('|');
            string cSTR1 = checkStatARR[0];
            string cSTR2 = checkStatARR[1];
            string cSTR3 = checkStatARR[2];
            string cSTR4 = checkStatARR[3];
            switch (levelsID)
            {
                case 1:
                    cSTR2 = "0";
                    break;
                case 2:
                    cSTR3 = "0";
                    break;
                case 3:
                    cSTR4 = "0";
                    break;
            }

            string RCheckStat = cSTR1 + "|" + cSTR2 + "|" + cSTR3 + "|" + cSTR4;
            string Sql = "update " + Pre + "News set CheckStat='" + RCheckStat + "' where Id = " + getID + " " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);

            string gSQL = "select NewsProperty,CheckStat,islock,NewsID,NewsType,CreatTime,DataLib,NewsType,isConstr,ClassID from " + Pre + "News where ID=" + getID + "";
            DataTable dts = DbHelper.ExecuteTable(CommandType.Text, gSQL, null);
            if (dts != null && dts.Rows.Count > 0)
            {
                string[] TCheckStat = dts.Rows[0]["CheckStat"].ToString().Split('|');
                string Tmp1 = TCheckStat[1] + "|";
                Tmp1 += TCheckStat[2] + "|";
                Tmp1 += TCheckStat[3];
                if (Tmp1 == "0|0|0")
                {
                    //int intisConstr = 0;
                    //if (Foosun.Common.Input.IsInteger(dts.Rows[0]["isConstr"].ToString())) { intisConstr = int.Parse(dts.Rows[0]["isConstr"].ToString()); }
                    //insertFormTB(dts.Rows[0]["NewsProperty"].ToString(), dts.Rows[0]["NewsID"].ToString(), DateTime.Parse(dts.Rows[0]["CreatTime"].ToString()), TableName, int.Parse(dts.Rows[0]["NewsType"].ToString()), intisConstr, 3000, 0, dts.Rows[0]["ClassID"].ToString());
                    //更新状态
                    string Sqls = "update " + Pre + "News set islock=0 where Id = " + getID + " " + Foosun.Common.Public.getSessionStr() + "";
                    DbHelper.ExecuteNonQuery(CommandType.Text, Sqls, null);
                }
                dts.Clear(); dts.Dispose();
            }
        }

        public int Up_Lock(string ID)
        {
            string Sql = "update " + Pre + "News set isLock = 0 where Id = " + ID + " ";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public string select_CheckStat(string ID)
        {
            string Sql = "select CheckStat from " + Pre + "News where Id = " + ID + " ";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }

        public DataTable getLockNews(string UserName)
        {
            string Sql = "";
            if (UserName == "0")
            {//0|0|0|0
                Sql = "select TOP 5 NewsId,NewsTitle,CheckStat,CreatTime,ClassID from " + Pre + "news where substring(CheckStat,3,5)!='0|0|0' and isRecyle=0 and SiteID='" + Foosun.Global.Current.SiteID + "' order by Id desc";
            }
            else
            {
                Sql = "select TOP 5 NewsId,NewsTitle,CheckStat,CreatTime,ClassID from " + Pre + "news where isRecyle=0 and SiteID='" + Foosun.Global.Current.SiteID + "' and Editor='" + UserName + "' order by Id desc";
            }
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }


        #endregion

        #region 不规则新闻
        /// <summary>
        /// 不规则新闻分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <param name="SqlCondition"></param>
        /// <returns></returns>
        public DataTable GetPages(int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string AllFields = "*";
            string Condition = "(SELECT DISTINCT Unid,(Select top 1 UnName from [" + Pre + "News_unNews] where unid=a.unid order by [rows],id desc) as UnName from [" + Pre + "News_unNews] a where 1=1" + Foosun.Common.Public.getSessionStr() + ") Unnews";
            string IndexField = "Unid";
            string OrderFields = "order by Unid Desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        /// <summary>
        /// 删除不规则新闻
        /// </summary>
        /// <param name="UnID"></param>
        /// <returns></returns>
        public int Str_DelSql(string UnID)
        {
            string Sql = "delete " + Pre + "news_unNews WHERE UnID='" + UnID + "'" + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 得到不规则新闻
        /// </summary>
        /// <param name="unNewsid"></param>
        /// <returns></returns>
        public DataTable sel(string unNewsid)
        {
            string Sql = "Select unName,titleCSS,SubCSS,UnID,ONewsID,[Rows],unTitle,NewsTable From [" + Pre + "news_unNews] where UnID='" + unNewsid + "'" + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable sel_DTNews(string NewsTable, string ONewsID)
        {
            string Sql = "Select NewsTitle From " + Pre + "News where NewsID='" + ONewsID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable sel_DT_PicInfo(string NewsID)
        {
            string Sql = "Select tl_font,tl_style,tl_size,tl_color,tl_space,tl_PicColor,tl_Width From [fs_news_topline] where NewsID='" + NewsID + "' AND NewsTF=1";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public int sel_unNewsid(string unNewsid)
        {
            string Sql = "Select count(id) From [" + Pre + "News_unNews] where UnID='" + unNewsid + "'" + Foosun.Common.Public.getSessionStr();
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }

        public int Add_1(string unName, string titleCSS, string unNewsid, string NewsID, string NewsTitle, string NewsTable, string TTNewsCSS, string IsMakePic, string SiteID)
        {
            string Sql = "INSERT INTO " + Pre + "News_unNews(unName,titleCSS,UnID,ONewsID,[Rows],unTitle,NewsTable,CreatTime,SiteID) VALUES('" + unName + "','" + titleCSS + "','" + unNewsid + "','" + NewsID + "',0,'" + NewsTitle + "','" + NewsTable + "',GETDATE(),'" + SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);

        }

        public void delUnID(string UnID)
        {
            string Sql = "delete from  " + Pre + "News_unNews where UnID='" + UnID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }


        public int Add_2(string unName, string titleCSS, string SubCSS, string unNewsid, string Arr_OldNewsId, string NewsRow, string NewsTitle, string NewsTable, string SiteID)
        {
            string Sql = "INSERT INTO " + Pre + "News_unNews(unName,titleCSS,SubCSS,UnID,ONewsID,[Rows],unTitle,NewsTable,CreatTime,SiteID) VALUES('" + unName + "','" + titleCSS + "','" + SubCSS + "','" + unNewsid + "','" + Arr_OldNewsId + "'," + NewsRow + ",'" + NewsTitle + "','" + NewsTable + "',GETDATE(),'" + SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 插入子类
        /// </summary>
        /// <param name="unNewsid">编号</param>
        /// <param name="Arr_OldNewsId">新闻ID</param>
        /// <param name="NewsRow">行</param>
        /// <param name="NewsTitle">标题</param>
        /// <param name="NewsTable">新闻表</param>
        /// <param name="SiteID">站点ID</param>
        /// <returns></returns>
        public int Add_SubNews(string unNewsid, string Arr_OldNewsId, string NewsRow, string NewsTitle, string NewsTable, string SiteID, string titleCSS)
        {
            string Sql = "INSERT INTO " + Pre + "News_Sub(NewsID,getNewsID,colsNum,NewsTitle,DataLib,CreatTime,SiteID,titleCSS) VALUES('" + unNewsid + "','" + Arr_OldNewsId + "'," + NewsRow + ",'" + NewsTitle + "','" + NewsTable + "',GETDATE(),'" + SiteID + "','" + titleCSS + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 得到子新闻
        /// </summary>
        /// <param name="unNewsid"></param>
        /// <returns></returns>
        public DataTable getUNews(string unNewsid)
        {
            string Sql = "Select NewsID,NewsTitle,getNewsID,colsNum,DataLib,titleCSS From [" + Pre + "news_Sub] where NewsID='" + unNewsid + "'" + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 删除不规则新闻
        /// </summary>
        /// <param name="UnID"></param>
        public void delSubID(string UnID)
        {
            string Sql = "delete from " + Pre + "News_Sub where NewsID='" + UnID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_TbClass()
        {
            string Sql = "select ClassID,ClassCName,ParentID from " + Pre + "News_Class";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_TbClass1(string PID)
        {
            string Sql = "select ClassID,ClassCName,ParentID from " + Pre + "News_Class where ParentID='" + PID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable GetPageiframe(string DdlClass, string sKeywrds, string sChoose, string DdlKwdType, int pageindex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string sFilter = " where a.isRecyle=0";
            if (DdlClass != "0")
            {
                sFilter += " and a.ClassID='" + DdlClass + "'";
            }
            string sKeywrdsd = sKeywrds;
            if (sKeywrdsd != "")
            {
                switch (DdlKwdType)
                {
                    case "content":
                        sFilter += " and Content like '%" + sKeywrds + "%'";
                        break;
                    case "author":
                        sFilter += " and Author like '%" + sKeywrds + "%'";
                        break;
                    case "editor":
                        sFilter += " and NewsTitle like '%" + sKeywrds + "%'";
                        break;
                    default:
                        sFilter += " and NewsTitle like '%" + sKeywrds + "%'";
                        break;
                }
            }
            string sChooses = sChoose;
            switch (sChooses)
            {
                case "All":
                    break;
                case "Contribute":
                    sFilter += " and isAdmin=0";
                    break;
                case "Commend":
                    sFilter += " and SUBSTRING(NewsProperty,1,1)='1'";
                    break;
                case "Top":
                    sFilter += " and a.OrderID=0";
                    break;
                case "Hot":
                    sFilter += " and SUBSTRING(NewsProperty,5,1)='1'";
                    break;
                case "Splendid":
                    sFilter += " and SUBSTRING(NewsProperty,15,1)='1'";
                    break;
                case "Headline":
                    sFilter += " and SUBSTRING(NewsProperty,9,1)='1'";
                    break;
                case "Slide":
                    sFilter += " and SUBSTRING(NewsProperty,7,1)='1'";
                    break;
                case "Pic":
                    sFilter += " and NewsType=1";
                    break;
            }
            sFilter += " and a.isLock=0";
            string AllFields = "a.Id,a.NewsID,a.NewsType,a.OrderID,a.NewsTitle,a.Author,a.Click,a.CheckStat,b.UserName,c.ClassCName";
            string Condition = Pre + "News a left join " + Pre + "sys_User b on a.Editor=b.UserNum left join " + Pre + "News_Class c on a.ClassID=c.ClassID " + sFilter;
            string IndexField = "a.Id";
            string OrderFields = "order by a.OrderID asc,a.Id desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, pageindex, PageSize, out RecordCount, out PageCount, null);
        }

        public int del_Table(string ID)
        {
            string Sql = "delete from " + Pre + "News where Id in (" + ID + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public int Add_fieldnm(string fieldnm, string id, DateTime oldtime)
        {
            string Sql = "insert into " + Pre + "old_News (" + fieldnm + ",oldtime,DataLib) select " + fieldnm + ",oldtime='" + DateTime.Now + "',DataLib='" + Pre + "News' from " + Pre + "News where Id in (" + id + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_fieldnm(string id)
        {
            string Sql = "delete from " + Pre + "News where Id in (" + id + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_old()
        {
            string Sql = "select top 1 * from " + Pre + "old_News";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Update1(string id)
        {
            string Sql = "update " + Pre + "News set isLock=1 where Id in (" + id + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Update2(string id)
        {
            string Sql = "update " + Pre + "News set isRecyle=1 where Id in (" + id + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public string sel_paths(string id)
        {
            string Sql = "select SavePath from " + Pre + "News where id=" + id;
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
        }

        #endregion
        #endregion 内容管理结束

        #region  获得省份或城市的信息

        /// <summary>
        /// 获得省份或城市的信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable getProvinceOrCityList(string pid)
        {
            string Sql = "Select cityName,Cid From " + Pre + "sys_City where Pid = '" + pid + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }


        #endregion

        public int infoIDNum(string InfoID, string APIID, string dbtable)
        {
            string andSTR = "";
            if (APIID != "0") { andSTR = " and DataLib='" + dbtable + "'"; }
            string Sql = "select count(*) from " + Pre + "api_commentary where InfoID='" + InfoID + "' and APIID='" + APIID + "' " + andSTR + "";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }


        #region 自定义字段

        public string modifyNewsDefineValue(string defineColumns, string NewsID, string DataLib, string DsApiID)
        {
            string _STR = " | ";
            string Sql = "select DsEname,DsContent from " + Pre + "define_save where DsEname='" + defineColumns + "' and DsNewsID='" + NewsID + "' and DsNewsTable='" + DataLib + "' and DsApiID='" + DsApiID + "' " + Foosun.Common.Public.getSessionStr() + "";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _STR = dt.Rows[0]["DsEname"].ToString() + "|" + dt.Rows[0]["DsContent"].ToString();
                }
                dt.Clear(); dt.Dispose();
            }
            return _STR;
        }

        public void insertDefineSign(string DsNewsID, string DsEName, string DsNewsTable, int DsType, string DsContent, string DsApiID)
        {
            string TSql = "select ID from " + Pre + "define_save where DsNewsID='" + DsNewsID + "' and DsEName='" + DsEName + "' and DsNewsTable='" + DsNewsTable + "' and DsApiID='" + DsApiID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, TSql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string DSql = "delete from " + Pre + "define_save where DsNewsID='" + DsNewsID + "' and DsEName='" + DsEName + "' and DsNewsTable='" + DsNewsTable + "' and DsApiID='" + DsApiID + "'";
                    DbHelper.ExecuteNonQuery(CommandType.Text, DSql, null);
                }
                dt.Clear(); dt.Dispose();
            }
            string Sql = "insert into " + Pre + "define_save (DsNewsID,DsEname,DsNewsTable,DsType,DsContent,DsApiID,SiteID) VALUES ('" + DsNewsID + "','" + DsEName + "','" + DsNewsTable + "'," + DsType + ",'" + DsContent + "','" + DsApiID + "','" + Foosun.Global.Current.SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }


        public void UpdateDefineSign(string DsNewsID, string DsEName, string DsNewsTable, int DsType, string DsContent, string DsApiID)
        {
            string Sql = "";
            string TSql = "select ID from " + Pre + "define_save where DsNewsID='" + DsNewsID + "' and DsEName='" + DsEName + "' and DsNewsTable='" + DsNewsTable + "' and DsApiID='" + DsApiID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, TSql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Sql = "update " + Pre + "define_save set DsNewsTable='" + DsNewsTable + "',DsContent='" + DsContent + "' where DsNewsID='" + DsNewsID + "' and DsEName='" + DsEName + "' and DsApiID='" + DsApiID + "' " + Foosun.Common.Public.getSessionStr() + "";
                }
                else
                {
                    Sql = "insert into " + Pre + "define_save (DsNewsID,DsEname,DsNewsTable,DsType,DsContent,DsApiID,SiteID) VALUES ('" + DsNewsID + "','" + DsEName + "','" + DsNewsTable + "'," + DsType + ",'" + DsContent + "','" + DsApiID + "','" + Foosun.Global.Current.SiteID + "')";
                }
                DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
                dt.Clear(); dt.Dispose();
            }
        }

        #endregion 自定义字段

        //#region 插入临时表
        //public void insertFormTB(string Prot, string NewsID, DateTime CreatTime, string DataTable, int NewsType, int isConstr, int MaxNumber, int updateNum,string ClassID)
        //{
        //    string[] getProt = Prot.Split(',');
        //    int isRec = int.Parse(getProt[0]);
        //    int isMarquee = int.Parse(getProt[1]);
        //    int isHOT = int.Parse(getProt[2]);
        //    int isFilt = int.Parse(getProt[3]);
        //    int isTT = int.Parse(getProt[4]);
        //    int isAnnouce = int.Parse(getProt[5]);
        //    int isWap = int.Parse(getProt[6]);
        //    int isJC = int.Parse(getProt[7]);
        //    string Sql = "";
        //    if (updateNum == 0)
        //    {
        //        string sTF = "select id from " + Pre + "news_temp where NewsID='" + NewsID + "' and DataLib='" + DataTable + "' order by id desc";
        //        DataTable dtTF = DbHelper.ExecuteTable(CommandType.Text, sTF, null);
        //        if (dtTF != null)
        //        {
        //            if (dtTF.Rows.Count == 0)
        //            {
        //                Sql = "insert into " + Pre + "news_temp (NewsID,DataLib,NewsType,CreatTime,IsRec,isHot,isTT,isAnnounce,isMarQuee,isConstr,isJC,isWap,isFilt,ClassID)";
        //                Sql += " VALUES ('" + NewsID + "','" + DataTable + "'," + NewsType + ",'" + CreatTime + "'," + isRec + "," + isHOT + "," + isTT + "," + isAnnouce + "," + isMarquee + "," + isConstr + "," + isJC + "," + isWap + "," + isFilt + ",'" + ClassID + "')";
        //                DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        //            }
        //            dtTF.Clear(); dtTF.Dispose();
        //        }
        //    }
        //    else
        //    {
        //        Sql = "update " + Pre + "news_temp set DataLib='" + DataTable + "',NewsType=" + NewsType + ",IsRec=" + isRec + ",isHot=" + isHOT + ",isTT=" + isTT + ",isAnnounce=" + isAnnouce + ",isMarQuee=" + isMarquee + ",isConstr=" + isConstr + ",isJC=" + isJC + ",isWap=" + isWap + ",isFilt=" + isFilt + " where NewsID='" + NewsID + "'";
        //        DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        //    }
        //    //删除多余的信息
        //    RandFirst:
        //        string SqlTf = "select ID from " + Pre + "news_temp order by id desc";
        //        DataTable rTF = DbHelper.ExecuteTable(CommandType.Text, SqlTf, null);
        //        if (rTF.Rows.Count > MaxNumber)
        //        {
        //            string SQL1 = "delete from " + Pre + "news_temp where id=(select top 1 id from " + Pre + "news_temp order by id asc)";
        //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL1, null);
        //            rTF.Clear(); rTF.Dispose();
        //            goto RandFirst;
        //        }

        //}

        //#endregion 插入临时表

        public DataTable getLastFormTB()
        {
            string Sql = "select id,NewsID,DataLib,NewsType from " + Pre + "news where islock=0 and isRecyle=0 and siteID='" + Foosun.Global.Current.SiteID + "' order by CreatTime desc,id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }


        public void delTBDateNumber(int dateNum)
        {
            //扩展使用
            //string SQL = "delete from " + Pre + "news_temp where DateDiff(d,CreatTime,'" + DateTime.Now + "') > " + dateNum + "";
            //DbHelper.ExecuteNonQuery(CommandType.Text, SQL, null);
        }

        public void delTBNewsID(string NewsID)
        {
            //扩展使用
            //string[] nID = NewsID.Split(',');
            //for (int i = 0; i < nID.Length; i++)
            //{
            //    if (nID[i].Trim() != "")
            //    {
            //        if (Foosun.Common.Input.IsInteger(nID[i]) == false){continue;}
            //        else
            //        {
            //            string gSQL = "select NewsID from " + Pre + "News where ID='" + int.Parse(nID[i]) + "'";
            //            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, gSQL, null);
            //            if (dt != null && dt.Rows.Count > 0)
            //            {
            //                string SQL = "delete from " + Pre + "news_temp where NewsID ='" + dt.Rows[0]["NewsID"].ToString() + "' and DataLib='" + DataTable + "'";
            //                DbHelper.ExecuteNonQuery(CommandType.Text, SQL, null);
            //                dt.Clear(); dt.Dispose();
            //            }
            //        }
            //    }
            //}
        }

        public void delTBNewsClassID(string ClassID)
        {
            //扩展使用
            //string SQL = "delete from " + Pre + "news_temp where ClassID ='" + ClassID + "'";
            //DbHelper.ExecuteNonQuery(CommandType.Text, SQL, null);
        }

        public void delTBTypeNumber(int getcondition)
        {
            //扩展使用
            ////清除推荐
            //    RandFirst:
            //        string Sql = "select ID from " + Pre + "news_temp where isRec=1 order by id desc";
            //        DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            //        if (dt.Rows.Count > getcondition)
            //        {
            //            string SQL = "delete from " + Pre + "news_temp where isRec=1 and id=(select top 1 id from " + Pre + "news_temp where isRec=1 order by id asc)";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL, null);
            //            dt.Clear(); dt.Dispose();
            //            goto RandFirst;
            //        }

            //    //清除滚动
            //    RandFirst1:
            //        string Sql1 = "select ID from " + Pre + "news_temp where isMarQuee=1 order by id desc";
            //        DataTable dt1 = DbHelper.ExecuteTable(CommandType.Text, Sql1, null);
            //        if (dt1.Rows.Count > getcondition)
            //        {
            //            string SQL_1 = "delete from " + Pre + "news_temp where isMarQuee=1 and id=(select top 1 id from " + Pre + "news_temp where isMarQuee=1 order by id asc)";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL_1, null);
            //            dt1.Clear(); dt1.Dispose();
            //            goto RandFirst1;
            //        }
            //    //清除热点
            //    RandFirst2:
            //        string Sql2 = "select ID from " + Pre + "news_temp where isHot=1 order by id desc";
            //        DataTable dt2 = DbHelper.ExecuteTable(CommandType.Text, Sql2, null);
            //        if (dt2.Rows.Count > getcondition)
            //        {
            //            string SQL_2 = "delete from " + Pre + "news_temp where isHot=1 and id=(select top 1 id from " + Pre + "news_temp where isHot=1 order by id asc)";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL_2, null);
            //            dt2.Clear(); dt2.Dispose();
            //            goto RandFirst2;
            //        }
            //    //清除幻灯
            //    RandFirst3:
            //        string Sql3 = "select ID from " + Pre + "news_temp where isFilt=1 order by id desc";
            //        DataTable dt3 = DbHelper.ExecuteTable(CommandType.Text, Sql3, null);
            //        if (dt3.Rows.Count > getcondition)
            //        {
            //            string SQL_3 = "delete from " + Pre + "news_temp where isFilt=1 and id=(select top 1 id from " + Pre + "news_temp where isFilt=1 order by id asc)";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL_3, null);
            //            dt3.Clear(); dt3.Dispose();
            //            goto RandFirst3;
            //        }
            //    //清除头条
            //    RandFirst4:
            //        string Sql4 = "select ID from " + Pre + "news_temp where isTT=1 order by id desc";
            //        DataTable dt4 = DbHelper.ExecuteTable(CommandType.Text, Sql4, null);
            //        if (dt4.Rows.Count > getcondition)
            //        {
            //            string SQL_4 = "delete from " + Pre + "news_temp where isTT=1 and id=(select top 1 id from " + Pre + "news_temp where isTT=1 order by id asc)";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL_4, null);
            //            dt4.Clear(); dt4.Dispose();
            //            goto RandFirst4;
            //        }
            //    //清除公告
            //    RandFirst5:
            //        string Sql5 = "select ID from " + Pre + "news_temp where isAnnounce=1 order by id desc";
            //        DataTable dt5 = DbHelper.ExecuteTable(CommandType.Text, Sql5, null);
            //         if (dt5.Rows.Count > getcondition)
            //        {
            //            string SQL_5 = "delete from " + Pre + "news_temp where isAnnounce=1 and id=(select top 1 id from " + Pre + "news_temp where isAnnounce=1 order by id asc)";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL_5, null);
            //            dt5.Clear(); dt5.Dispose();
            //            goto RandFirst5;
            //        }
            //    //清除WAP
            //    RandFirst6:
            //        string Sql6 = "select ID from " + Pre + "news_temp where isWap=1 order by id desc";
            //        DataTable dt6 = DbHelper.ExecuteTable(CommandType.Text, Sql6, null);
            //        if (dt6.Rows.Count > getcondition)
            //        {
            //            string SQL_6 = "delete from " + Pre + "news_temp where isWap=1 and id=(select top 1 id from " + Pre + "news_temp where isWap=1 order by id asc)";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL_6, null);
            //            dt6.Clear(); dt6.Dispose();
            //            goto RandFirst6;
            //        }
            //    //清除精彩
            //    RandFirst7:
            //        string Sql7 = "select ID from " + Pre + "news_temp where isJC=1 order by id desc";
            //        DataTable dt7 = DbHelper.ExecuteTable(CommandType.Text, Sql7, null);
            //        if (dt7.Rows.Count > getcondition)
            //        {
            //            string SQL_7 = "delete from " + Pre + "news_temp where isJC=1 and id=(select top 1 id from " + Pre + "news_temp where isJC=1 order by id asc)";
            //            DbHelper.ExecuteNonQuery(CommandType.Text, SQL_7, null);
            //            dt7.Clear(); dt7.Dispose();
            //            goto RandFirst7;
            //        }
        }

        public int getNewsRecordEdior(string UserName)
        {
            int Rint = 0;
            string nSql = "";
            //string Sql = "select id,TableName from " + Pre + "sys_newsIndex order by id desc";
            //DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            nSql = "select id from " + Pre + "news where Editor='" + UserName + "'";
            DataTable dts = DbHelper.ExecuteTable(CommandType.Text, nSql, null);
            if (dts != null && dts.Rows.Count > 0)
            {
                Rint = Rint + dts.Rows.Count;
                dts.Clear(); dts.Dispose();
            }
            //    }
            //    dt.Clear(); dt.Dispose();
            //}
            return Rint;
        }

        /// <summary>
        /// 得到浏览新闻的参数
        /// </summary>
        /// <param name="NewsID"></param>
        /// 
        /// <returns></returns>
        public string getnewsReview(string ID, string gType)
        {
            string newspath = string.Empty;
            string newspath1 = string.Empty;
            string sql = string.Empty;
            string dim = Foosun.Config.UIConfig.dirDumm.Trim();
            string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
            if (dim != string.Empty) { dim = "/" + dim; }
            SqlParameter param = new SqlParameter("@ID", ID);
            if (gType != "special")
            {
                if (gType == "class")
                {
                    sql = "select IsURL,URLaddress,SavePath,SaveClassframe,ClassSaveRule,isDelPoint,ClassID,isPage from " + Pre + "news_class where ClassID=@ID";
                    IDataReader dt = DbHelper.ExecuteReader(CommandType.Text, sql, param);
                    while (dt.Read())
                    {
                        if (dt["isURL"].ToString() == "1")
                        {
                            if (dt["URLaddress"].ToString().IndexOf("http://") > -1)
                            {
                                newspath = dt["URLaddress"].ToString();
                            }
                            else
                            {
                                newspath = "http://" + dt["URLaddress"].ToString();
                            }
                        }
                        else
                        {
                            if (dt["isDelPoint"].ToString() != "0")
                            {
                                newspath1 = dim + "/list-" + dt["ClassID"].ToString() + ".aspx";
                            }
                            else
                            {
                                if (ReadType == "1")
                                {
                                    if (dt["isPage"].ToString() == "1")
                                    {
                                        newspath1 = dim + "/page-" + dt["ClassID"].ToString() + ".aspx";
                                    }
                                    else
                                    {
                                        newspath1 = dim + "/list-" + dt["ClassID"].ToString() + ".aspx";
                                    }
                                }
                                else
                                {
                                    if (dt["isPage"].ToString() == "1")
                                    {
                                        newspath1 = dim + "/" + dt["SavePath"].ToString();
                                    }
                                    else
                                    {
                                        newspath1 = dim + "/" + dt["SavePath"].ToString() + "/" + dt["SaveClassframe"].ToString() + "/" + dt["ClassSaveRule"].ToString();
                                    }
                                }
                            }
                            newspath = newspath1.Replace("//", "/").Replace(@"\\", @"\").Replace("//", "/");
                        }
                    }
                    dt.Close();
                }
                else
                {
                    sql = "select a.newsid,a.URLaddress,a.NewsType,a.SavePath,a.FileName,a.FileEXName,b.SavePath as SavePath1,b.SaveClassframe,a.isDelPoint from " + Pre + "news a," + Pre + "news_class b where a.classid=b.classid and a.NewsID=@ID";
                    IDataReader dt = DbHelper.ExecuteReader(CommandType.Text, sql, param);
                    while (dt.Read())
                    {
                        if (dt["NewsType"].ToString() != "2")
                        {
                            if (dt["isDelPoint"].ToString() != "0")
                            {
                                newspath1 = dim + "/content.aspx?id=" + dt["NewsID"].ToString() + "";
                            }
                            else
                            {
                                if (ReadType == "1")
                                {
                                    newspath1 = dim + "/content.aspx?id=" + dt["NewsID"].ToString() + "";
                                }
                                else
                                {
                                    newspath1 = dim + "/" + dt["SavePath1"].ToString() + "/" + dt["SaveClassframe"].ToString() + "/" + dt["SavePath"].ToString() + "/" + dt["FileName"].ToString() + dt["FileEXName"].ToString();
                                }
                            }
                            newspath = newspath1.Replace("//", "/").Replace(@"\\", @"\");
                        }
                        else
                        {
                            if (dt["URLaddress"].ToString().IndexOf("http://") > -1)
                            {
                                newspath = dt["URLaddress"].ToString();
                            }
                            else
                            {
                                newspath = "http://" + dt["URLaddress"].ToString();
                            }
                        }
                    }
                    dt.Close();
                }
                
            }
            else
            {
                //专题地址
                sql = "select SpecialID,SavePath,saveDirPath,FileName,FileEXName,isDelPoint from " + Pre + "news_special where SpecialID=@ID";
                IDataReader dt = DbHelper.ExecuteReader(CommandType.Text, sql, param);
                while (dt.Read())
                {
                    if (dt["isDelPoint"].ToString() != "0")
                    {
                        newspath1 = dim + "/special-" + dt["SpecialID"].ToString() + ".aspx";
                    }
                    else
                    {
                        if (ReadType == "1")
                        {
                            newspath1 = dim + "/special-" + dt["SpecialID"].ToString() + ".aspx";
                        }
                        else
                        {
                            newspath1 = dim + "/" + dt["SavePath"].ToString() + "/" + dt["saveDirPath"].ToString() + "/" + dt["FileName"].ToString() + dt["FileEXName"].ToString();
                        }
                    }
                    newspath = newspath1.Replace("//", "/").Replace(@"\\", @"\");
                }
                dt.Close();
            }
            return newspath;
        }

        /// <summary>
        /// 更新导航
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public void updateReplaceNavi(string ClassID)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            try
            {
                cn.Open();
                string NewsPosition = "";
                string url = "";
                string dim = Foosun.Config.UIConfig.dirDumm;
                if (dim.Trim() != string.Empty)
                {
                    dim = "/" + dim;
                }
                string sql = "select ClassID,SavePath,SaveClassframe,ClassSaveRule,NaviPosition,NewsPosition from " + Pre + "news_class where ClassID=@ClassID";
                SqlParameter Prm = new SqlParameter("@ClassID", ClassID);
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, sql, Prm);
                if (rd.Read())
                {
                    NewsPosition = rd["NewsPosition"].ToString();
                    url = dim + "/" + rd["SavePath"].ToString() + "/" + rd["SaveClassframe"].ToString() + "/" + rd["ClassSaveRule"].ToString();
                    NewsPosition = NewsPosition.Replace("{@ClassURL}", url).Replace("//", "/");
                }
                rd.Close();
                string Usql = "update " + Pre + "news_class set NewsPosition=@NewsPosition where ClassID=@ClassID";
                SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@NewsPosition",NewsPosition),new SqlParameter("@ClassID",ClassID)
            };
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Usql, Param);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        /// <summary>
        /// 根据ID获得NewsID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNewsIDfromID1(int id)
        {
            string flg = "0|0";
            string sql = "select NewsID,ClassID from " + Pre + "news where id=" + id;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, sql, null);
            if (rd.Read())
            {
                flg = rd.GetString(0) + "|" + rd.GetString(1);
            }
            rd.Close();
            return flg;
        }

        /// <summary>
        /// 得到栏目是否是单页面
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public int getclassPage(string ClassID)
        {
            string sql = "select isPage from " + Pre + "news_class where ClassID=@ClassID";
            SqlParameter Param = new SqlParameter("@ClassID", ClassID);
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, Param));
        }

        /// <summary>
        /// 复位栏目
        /// </summary>
        /// <param name="ClassID">栏目编号字符串</param>
        public void ClassReset(string ClassID)
        {
            string Sql = "Update " + Pre + "news_Class Set ParentID=0 Where isLock=0 and isRecyle=0";
            if (ClassID != null)
                Sql += " and ClassID In(" + ClassID + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 新闻统计
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="flg"></param>
        /// <returns></returns>
        public int newsstat(string siteid, string flg)
        {
            SqlParameter param = new SqlParameter("@SiteID", siteid);
            string sql = "";
            switch (flg)
            {
                case "m":
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID and DateDiff(month,[CreatTime] ,Getdate())=0";
                    break;
                case "pm":
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID and DateDiff(month,[CreatTime] ,Getdate())=1";
                    break;
                case "pz":
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID and DateDiff(week,[CreatTime] ,Getdate())=1";
                    break;
                case "z":
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID and DateDiff(week,[CreatTime] ,Getdate())=0";
                    break;
                case "pd":
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID and DateDiff(Day,[CreatTime] ,Getdate())=1";
                    break;
                case "d":
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID and DateDiff(Day,[CreatTime] ,Getdate())=0";
                    break;
                case "c":
                    sql = "select count(id) from " + Pre + "news_class where SiteID=@SiteID";
                    break;
                case "s":
                    sql = "select count(id) from " + Pre + "news_special where SiteID=@SiteID";
                    break;
                case "a":
                    sql = "select count(id) from " + Pre + "sys_admin where SiteID=@SiteID";
                    break;
                case "de":
                    sql = "select count(id) from " + Pre + "define_data where SiteID=@SiteID";
                    break;
                case "v":
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID and vURL!='' and vURL!=null";
                    break;
                case "mo":
                    sql = "select count(id) from " + Pre + "sys_channel where SiteID=@SiteID";
                    break;
                case "u":
                    sql = "select count(id) from " + Pre + "sys_user where SiteID=@SiteID";
                    break;
                case "co":
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID and isconstr=1";
                    break;
                default:
                    sql = "select count(id) from " + Pre + "news where SiteID=@SiteID";
                    break;
            }
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        /// <summary>
        /// 得到不规则新闻
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public DataTable getUnNewsReview(string uID)
        {
            SqlParameter param = new SqlParameter("@uID", uID);
            string Sql = "Select [unName],[TitleCSS],[SubCSS],[ONewsID],[Rows],[unTitle],[NewsTable] From [" + Pre + "news_unNews] Where [UnID]=@uID Order By [Rows] Asc,[ID] Asc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        /// <summary>
        /// 得到栏目下新闻数
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public int getClassNewsCount(string ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string Sql = "Select count(id) From [" + Pre + "news] Where [ClassID]=@ClassID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="Pro"></param>
        /// <param name="NewsID"></param>
        public void updateNewsPro(string str, string ID, int num)
        {
            string Sql = "";
            if (num == 0)
            {
                Sql = "Update " + Pre + "news Set NewsProperty='" + str + "' Where NewsID In (" + ID + ")";
            }
            else
            {
                Sql = "Update " + Pre + "news Set NewsProperty='" + str + "' Where ClassID In (" + ID + ")";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewstemplet(string str, string ID, int num)
        {
            string Sql = "";
            if (num == 0)
            {
                Sql = "Update " + Pre + "news Set Templet='" + str + "' Where NewsID In (" + ID + ")";
            }
            else
            {
                Sql = "Update " + Pre + "news Set Templet='" + str + "' Where ClassID In (" + ID + ")";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 批量更新权重
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsOrder(string str, string ID, int num)
        {
            string Sql = "";
            if (num == 0)
            {
                Sql = "Update " + Pre + "news Set OrderID=" + int.Parse(str) + " Where NewsID In (" + ID + ")";
            }
            else
            {
                Sql = "Update " + Pre + "news Set OrderID=" + int.Parse(str) + " Where ClassID In (" + ID + ")";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 更新评论连接
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsComm(string str, string ID, int num)
        {
            string Sql = "";
            if (num == 0)
            {
                Sql = "Update " + Pre + "news Set CommLinkTF=" + int.Parse(str) + " Where NewsID In (" + ID + ")";
            }
            else
            {
                Sql = "Update " + Pre + "news Set CommLinkTF=" + int.Parse(str) + " Where ClassID In (" + ID + ")";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 更新关键字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsTAG(string str, string ID, int num)
        {
            string Sql = "";
            if (num == 0)
            {
                Sql = "Update " + Pre + "news Set Tags='" + str + "' Where NewsID In (" + ID + ")";
            }
            else
            {
                Sql = "Update " + Pre + "news Set Tags='" + str + "' Where ClassID In (" + ID + ")";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }


        /// <summary>
        /// 新闻点击
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsClick(string str, string ID, int num)
        {
            string Sql = "";
            if (num == 0)
            {
                Sql = "Update " + Pre + "news Set Click=" + int.Parse(str) + " Where NewsID In (" + ID + ")";
            }
            else
            {
                Sql = "Update " + Pre + "news Set Click=" + int.Parse(str) + " Where ClassID In (" + ID + ")";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 更新来源
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsSouce(string str, string ID, int num)
        {
            string Sql = "";
            if (num == 0)
            {
                Sql = "Update " + Pre + "news Set Souce='" + str + "' Where NewsID In (" + ID + ")";
            }
            else
            {
                Sql = "Update " + Pre + "news Set Souce='" + str + "' Where ClassID In (" + ID + ")";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 更新扩展名
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsFileEXstr(string str, string ID, int num)
        {
            string Sql = "";
            if (num == 0)
            {
                Sql = "Update " + Pre + "news Set FileEXName='" + str + "' Where NewsID In (" + ID + ")";
            }
            else
            {
                Sql = "Update " + Pre + "news Set FileEXName='" + str + "' Where ClassID In (" + ID + ")";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public void addSpecialTo(string NewsID, string SpecialID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@NewsID", SqlDbType.Int, 4);
            param[0].Value = int.Parse(NewsID);
            param[1] = new SqlParameter("@SpecialID", SqlDbType.NVarChar, 20);
            param[1].Value = SpecialID;
            string gsql = "select NewsID from " + Pre + "news where ID=@NewsID";
            string NewsIDstr = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, gsql, param));
            string Sql = "insert into " + Pre + "special_news(";
            Sql += "SpecialID,NewsID";
            Sql += ") values (@SpecialID,'" + NewsIDstr + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public string GetParamBase(string Name)
        {
            string sql = "select " + Name + " from " + Pre + "sys_param";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }

        /// <summary>
        /// 根据ID得到newsID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getNewsIDById(string id)
        {
            string sql = "select NewsID from " + Pre + "news where Id=" + id;
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
        }
    }
}