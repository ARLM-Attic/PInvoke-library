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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IContentManage
    {
        #region 栏目
        IDataReader GetClassSitenewsstr(string ParentID, string SiteID);
        IDataReader GetNewsIndex();
        DataTable GetSiteID();
        DataTable getClassContent(string ClassID);
        DataTable checkHasSub(string ParentID);
        DataTable getParentClass(string ClassID);
        DataTable getdefineTable();
        DataTable getdefineEditTable(string ClassID);
        DataTable getdefineEditTablevalue(int TempID);
        void del_all(int nID, string Tablename);
        void del_Recyle(string nID, string Tablename);
        void del_Lock(string nID, string Tablename);
        void insertClassContent(Foosun.Model.ClassContent uc);
        void UpdateClassContent(Foosun.Model.ClassContent uc);
        void del_recyleClass(string ClassID);
        void del_Class(string ClassID);
        void GetChildClassdel(string ParentID);
        void GetChildClassdel_recyle(string ParentID);
        DataTable getChildList(string ParentID);
        void ChangeLock(string ClassId, int NUM);
        DataTable getLock(string ClassID);
        void resetClass();
        void resetOrder(int OrderID, string ClassID);
        DataTable getSouceClass();
        void delSouce(string ClassID);
        void updateSouce(string sClassID, string tClassID);
        void updateSouce1(string sClassID, string tClassID);
        void changeParent(string sClassID, string tClassID);
        void delClassAll();
        void clearNewsInfo(string ClassId);
        DataTable getClassInfo_Templet();
        void UpdateClassInfo(string strUpdate, string _Str);
        void UpdateClassNewsInfo(string templet, string _Str);
        void updateOrderP(string ClassID, int OrderID);
        DataTable getClassEname(string ClassEname);
        void ClassReset(string ClassID);
        #endregion 栏目

        #region 新闻内容
        void updateClassStat(int Num, string ClassID);
        void updateNewsHTML(int Num, string NewsID);
        string GetNewsIDfromID1(int ID);
        IDataReader getNaviClass(string ClassID);
        DataTable getTagsList();
        IDataReader getNewsID(string NewsID);
        DataTable getClassParam(string ClassID);
        DataTable getSysParam();
        void iGen(string _TempStr, string _URL, string _EmailURL, int _num);
        string getDataLib(string ClassID);
        void insertNewsContent(Foosun.Model.NewsContent uc);
        void UpdateNewsContent(Foosun.Model.NewsContent uc);
        DataTable getTopNewsId(string Datatb);
        DataTable getNewsIDTF(string NewsID, string Datatb);
        void intsertTT(Foosun.Model.NewsContentTT uc);
        void UpdateTT(Foosun.Model.NewsContentTT uc);
        void intsertVote(Foosun.Model.VoteContent uc);
        void insertFileURL(string URLName, string NewsID, string DataLib, string FileURL, int OrderID);
        void updateFileURL(string URLName, string DataLib, string FileURL, int OrderID, int ID);
        void deleteFileUrl(string ids, string NewsID);
        DataTable getSpecialNews(string NewsID);
        void deleteFilesurl(int flgTF, string NewsID);
        DataTable getFileList(string NewsID, string DataTB);
        int getFileIDTF(int ID);
        void UpdateVote(Foosun.Model.VoteContent uc);
        DataTable getVoteID(string NewsID, string DataLib);
        DataTable getTopline(string NewsID, string DataLib, int NewsTFNum);
        DataTable getDefineID(string _str);
        DataTable getGenContent();
        DataTable getClassList_Show(string ParentID);
        //void insertSubNewsContent(string NewsID, string getNewsID, string NewsTitle, string DataLib, string TitleColor, int TitleBTF, int TitleITF, int colsNum);
        void delNewsContent(string NewsID);
        DataTable getSubNewsID(string NewsID);
        int newsTitletf(string NewsTitle, string dtable, string EditAction, string NewsID);
        string saveAjaxContent(string Content);
        DataTable getPageContent(string ClassID);
        void insertPage(Foosun.Model.PageContent uc);
        void updatePage(Foosun.Model.PageContent uc);
        string getClassCName(string ClassID);
        string getspecialCName(string ClassID);
        void updateReplaceNavi(string ClassID);
        #endregion 新闻内容

        #region 新闻列表
        DataTable GetPage(string SpecialID, string Editor, string ClassID, string sKeywrd, string DdlKwdType, string sChooses, string SiteID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        DataTable getSiteList();
        DataTable getSiteParam(string SiteID);
        int Del_news(string id);
        string sel_path(string id);
        void Del_newsc(string Id);
        int Update_Lock(string id, int nums);
        int Update_ResetOrde(string id);
        void allCheck(int[] id);
        DataTable sel_old_News();
        DataTable sel_old_classNews(string ClassID);
        int sel_old_classInHitoryDay(string ClassID);
        int Add_old_News(string fieldnm, string id, DateTime oldtime);
        int del_new_News(string id);
        int settop(string id);
        int unsettop(string id);
        DataTable sel_JS();
        DataTable sel_JSNews(string NewsID);
        int Add_JSFile(string JsID, string Njf_title, string NewsId, string PicPath, string ClassId, string SiteID, DateTime CreatTime, DateTime TojsTime);
        DataTable sel_News_Class();
        DataTable sel_LblNewsTable(string LblNewsTable, string s);
        DataTable sel_PID(string PID);
        int del_move(string sTb, string sOrgNews);
        DataTable sel_sys_NewsIndex(string ClassID);
        int sel_newsclass(string cid);
        DataTable sel_NewsTitle(string sTb, string ClassID);
        DataTable sel1(string sTb, string sOrgNews);
        string sel_copy_clsaa(string ClassID);
        string getFileNameInfo(string NewsID, string DataLib);
        void Copy_news(string ClassID, string DataLib, string sOrgNews, string sTb, string NewsID, string FileName);
        DataTable sel_copy_classnews(string NewsTable, string ClassID);
        void Copy_ClassNews(string ClassID, string DataLib, string sOrgNews, string sTb, string NewsID, string fileName);
        int del_classmove(string sTb, string sOrgNews);
        int Up_news2(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string sOrgNews);
        int Up_news1(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string sOrgNews);
        void Up_Classnews(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string ClassID, string Tags, string Souce);
        int sel_NewsID(string NewsID);
        string sel_sclasstext(string sclassid);
        int delNumber(string ClassID);
        DataTable sle_PicUrl(string ID, string tb);
        int Up_PicURL(string PicURL, string SPicURL, string ID, string tb);
        void upCheckStat(string getID, int levelsID);
        int Up_Lock(string ID);
        string select_CheckStat(string ID);
        DataTable getLockNews(string UserName);
        string getNewsAccessory(int ID);
        #endregion

        #region  省市信息
        DataTable getProvinceOrCityList(string pid);
        #endregion

        #region 不规则新闻
        DataTable GetPages(int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        int Str_DelSql(string UnID);
        DataTable sel(string unNewsid);
        DataTable sel_DTNews(string NewsTable, string ONewsID);
        DataTable sel_DT_PicInfo(string NewsID);
        int sel_unNewsid(string unNewsid);
        void delUnID(string UnID);
        int Add_1(string unName, string titleCSS, string unNewsid, string NewsID, string NewsTitle, string NewsTable, string TTNewsCSS, string IsMakePic, string SiteID);
        int Add_2(string unName, string titleCSS, string SubCSS, string unNewsid, string Arr_OldNewsId, string NewsRow, string NewsTitle, string NewsTable, string SiteID);
        int Add_SubNews(string unNewsid, string Arr_OldNewsId, string NewsRow, string NewsTitle, string NewsTable, string SiteID, string titleCSS);
        DataTable getUNews(string unNewsid);
        void delSubID(string UnID);
        DataTable sel_TbClass();
        DataTable sel_TbClass1(string PID);
        DataTable GetPageiframe(string DdlClass, string sKeywrds, string sChoose, string DdlKwdType, int pageindex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        int del_Table(string ID);
        int Add_fieldnm(string fieldnm, string id, DateTime oldtime);
        int Del_fieldnm(string id);
        DataTable sel_old();
        int Update1(string id);
        int Update2(string id);
        string sel_paths(string id);
        int infoIDNum(string InfoID, string APIID, string dbtable);
        #endregion
        #region 自定义字段
        string modifyNewsDefineValue(string defineColumns, string NewsID, string DataLib, string DsApiID);
        void insertDefineSign(string DsNewsID, string DsEName, string DsNewsTable, int DsType, string DsContent, string DsApiID);
        void UpdateDefineSign(string DsNewsID, string DsEName, string DsNewsTable, int DsType, string DsContent, string DsApiID);

        #endregion 自定义字段
        //void insertFormTB(string Prot, string NewsID, DateTime CreatTime, string DataTable, int NewsType, int isConstr, int MaxNumber, int updateNum, string ClassID);
        DataTable getLastFormTB();
        void delTBDateNumber(int dateNum);
        void delTBTypeNumber(int getcondition);
        void delTBNewsID(string NewsID);
        void delTBNewsClassID(string ClassID);
        int getNewsRecordEdior(string UserName);
        string getnewsReview(string NewsID, string gType);
        int getclassPage(string ClassID);
        int newsstat(string siteid, string flg);
        DataTable getUnNewsReview(string uID);
        int getClassNewsCount(string ClassID);

        void updateNewsPro(string Pro, string NewsID, int num);
        void updateNewstemplet(string str, string NewsID, int num);
        void updateNewsOrder(string str, string ID, int num);
        void updateNewsComm(string str, string ID, int num);
        void updateNewsTAG(string str, string ID, int num);
        void updateNewsClick(string str, string ID, int num);
        void updateNewsSouce(string str, string ID, int num);
        void updateNewsFileEXstr(string str, string ID, int num);
        void addSpecialTo(string NewsID, string SpecialID);
        string GetParamBase(string Name);
        string getNewsIDById(string id);
    }

    public sealed partial class DataAccess
    {
        public static IContentManage CreateContentManage()
        {
            string className = path + ".ContentManage";
            return (IContentManage)Assembly.Load(path).CreateInstance(className);
        }
    }

}
