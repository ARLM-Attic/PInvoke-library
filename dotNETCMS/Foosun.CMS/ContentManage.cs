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
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Foosun.DALFactory;
using Foosun.Model;


namespace Foosun.CMS
{
    public class ContentManage
    {
        Foosun.DALFactory.IContentManage dal;
        public ContentManage()
        {
            dal = DataAccess.CreateContentManage();
        }

        #region 站点列表导航

        /// <summary>
        /// 得到列表返回值
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public IDataReader GetClassSitenewsstr(string ParentID, string SiteID)
        {
            return dal.GetClassSitenewsstr(ParentID, SiteID);
        }
        #endregion 站点列表导航

        #region 新闻内容管理开始
        #region 新闻内容
        /// <summary>
        /// 更新栏目状态
        /// </summary>
        /// <param name="Num">1为已生，0为未生成</param>
        public void updateClassStat(int Num, string ClassID)
        {
            dal.updateClassStat(Num, ClassID);
        }

        /// <summary>
        /// 更新新闻是否生成静态页面
        /// </summary>
        /// <param name="Num"></param>
        /// <param name="NewsID"></param>
        public void updateNewsHTML(int Num, string NewsID)
        {
            dal.updateNewsHTML(Num, NewsID);
        }

        /// <summary>
        /// 得到导航内容
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public IDataReader getNaviClass(string ClassID)
        {
            return dal.getNaviClass(ClassID);
        }

        /// <summary>
        /// 根据ID获得新闻NewsID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNewsIDfromID1(int id)
        {
            return dal.GetNewsIDfromID1(id);
        }
        /// <summary>
        /// 新闻内容管理.得到索引表
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public IDataReader GetNewsIndex()
        {
            return dal.GetNewsIndex();
        }
        /// <summary>
        /// 新闻内容管理.得到站点表
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetSiteID()
        {
            DataTable dt = dal.GetSiteID();
            return dt;
        }
        /// <summary>
        /// 彻底删除新闻
        /// </summary>
        /// <param name="nID"></param>
        public void del_all(int nID, string Tablename)
        {
            dal.del_all(nID, Tablename);
        }
        /// <summary>
        /// 删除新闻到回收站
        /// </summary>
        /// <param name="nID"></param>
        public void del_Recyle(string nID, string Tablename)
        {
            dal.del_Recyle(nID, Tablename);
        }
        /// <summary>
        /// 锁定新闻
        /// </summary>
        /// <param name="nID"></param>
        public void del_Lock(string nID, string Tablename)
        {
            dal.del_Lock(nID, Tablename);
        }

        // by Simplt.Xie
        /// <summary>
        /// 修改新闻得到新闻属性
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <returns></returns>
        public IDataReader getNewsID(string NewsID)
        {
            return dal.getNewsID(NewsID);
        }

        /// <summary>
        /// 继承栏目设置
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getClassParam(string ClassID)
        {
            DataTable dt = dal.getClassParam(ClassID);
            return dt;
        }

        /// <summary>
        /// 继承参数设置
        /// </summary>
        /// <returns></returns>
        public DataTable getSysParam()
        {
            DataTable dt = dal.getSysParam();
            return dt;
        }

        /// <summary>
        /// 来源入库
        /// </summary>
        /// <param name="nID"></param>
        public void iGen(string _TempStr, string _URL, string _EmailURL, int _num)
        {
            dal.iGen(_TempStr, _URL, _EmailURL, _num);
        }

        /// <summary>
        /// 获取栏目数据库表
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public string getDataLib(string ClassID)
        {
            return dal.getDataLib(ClassID);
        }

        /// <summary>
        /// 得到最新Tags
        /// </summary>
        /// <returns></returns>
        public DataTable getTagsList()
        {
            DataTable dt = dal.getTagsList();
            return dt;
        }

        /// <summary>
        /// 判断新闻标题是否存在
        /// </summary>
        /// <param name="NewsTitle"></param>
        /// <returns></returns>
        public int newsTitletf(string NewsTitle, string dtable, string EditAction, string NewsID)
        {
            return dal.newsTitletf(NewsTitle, dtable, EditAction, NewsID);
        }

        /// <summary>
        /// 得到最新新闻ID
        /// </summary>
        /// <returns></returns>
        public DataTable getTopNewsId(string Datatb)
        {
            DataTable dt = dal.getTopNewsId(Datatb);
            return dt;
        }

        ///// <summary>
        ///// 插入子新闻
        ///// </summary>
        ///// <param name="uc"></param>
        //public void insertSubNewsContent(string NewsID, string getNewsID, string NewsTitle, string DataLib, string TitleColor, int TitleBTF, int TitleITF, int colsNum)
        //{
        //    dal.insertSubNewsContent(NewsID, getNewsID, NewsTitle, DataLib, TitleColor, TitleBTF, TitleITF, colsNum);
        //}

        /// <summary>
        /// 删除子新闻
        /// </summary>
        /// <param name="uc"></param>
        public void delNewsContent(string NewsID)
        {
            dal.delNewsContent(NewsID);
        }

        /// <summary>
        /// 插入新闻
        /// </summary>
        /// <param name="uc"></param>
        public void insertNewsContent(Foosun.Model.NewsContent uc)
        {
            dal.insertNewsContent(uc);
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="uc"></param>
        public void UpdateNewsContent(Foosun.Model.NewsContent uc)
        {
            dal.UpdateNewsContent(uc);
        }

        /// <summary>
        /// 插入头条
        /// </summary>
        /// <param name="uc"></param>
        public void intsertTT(Foosun.Model.NewsContentTT uc)
        {
            dal.intsertTT(uc);
        }

        /// <summary>
        /// 更新头条
        /// </summary>
        /// <param name="uc"></param>
        public void UpdateTT(Foosun.Model.NewsContentTT uc)
        {
            dal.UpdateTT(uc);
        }

        /// <summary>
        /// 插入投票
        /// </summary>
        /// <param name="uc"></param>
        public void intsertVote(Foosun.Model.VoteContent uc)
        {
            dal.intsertVote(uc);
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
            dal.insertFileURL(URLName, NewsID, DataLib, FileURL, OrderID);
        }

        /// <summary>   
        /// 删除id不是ids的附件
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="NewsID"></param>
        public void deleteFileUrl(string ids, string NewsID)
        {
            dal.deleteFileUrl(ids, NewsID);
        }

        /// <summary>
        /// 更新附件
        /// </summary>
        /// <param name="DataLib"></param>
        /// <param name="FileURL"></param>
        /// <param name="OrderID"></param>
        public void updateFileURL(string URLName, string DataLib, string FileURL, int OrderID, int ID)
        {
            dal.updateFileURL(URLName, DataLib, FileURL, OrderID, ID);
        }

        public void deleteFilesurl(int flgTF, string NewsID)
        {
            dal.deleteFilesurl(flgTF, NewsID);
        }

        /// <summary>
        /// 得到某条新闻的附件列表
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataTB"></param>
        /// <returns></returns>
        public DataTable getFileList(string NewsID, string DataTB)
        {
            return dal.getFileList(NewsID, DataTB);
        }

        /// <summary>
        /// 得到某一个接点是否存在
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int getFileIDTF(int ID)
        {
            return dal.getFileIDTF(ID);
        }

        /// <summary>
        /// 更新投票
        /// </summary>
        /// <param name="uc"></param>
        public void UpdateVote(Foosun.Model.VoteContent uc)
        {
            dal.UpdateVote(uc);
        }

        public DataTable getNewsIDTF(string NewsID, string Datatb)
        {
            DataTable dt = dal.getNewsIDTF(NewsID, Datatb);
            return dt;
        }
        /// <summary>
        /// ajax保存新闻进临时库！返回编号
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public string saveAjaxContent(string Content)
        {
            return dal.saveAjaxContent(Content);
        }


        /// <summary>
        /// 得到某一个新闻的子新闻
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <returns></returns>
        public DataTable getSubNewsID(string NewsID)
        {
            DataTable dt = dal.getSubNewsID(NewsID);
            return dt;
        }


        /// <summary>
        /// 得到某一个新闻的投票参数
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <returns></returns>
        public DataTable getVoteID(string NewsID, string DataLib)
        {
            DataTable dt = dal.getVoteID(NewsID, DataLib);
            return dt;
        }

        /// <summary>
        /// 得到某一个新闻的头条参数
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="DataLib"></param>
        /// <returns></returns>
        public DataTable getTopline(string NewsID, string DataLib, int NewsTFNum)
        {
            DataTable dt = dal.getTopline(NewsID, DataLib, NewsTFNum);
            return dt;
        }
        /// <summary>
        /// 根据编号读取相应控件类型
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        public DataTable getDefineID(string _str)
        {
            DataTable dt = dal.getDefineID(_str);
            return dt;
        }

        /// <summary>
        /// 得到内部连接地址
        /// </summary>
        /// <returns></returns>
        public DataTable getGenContent()
        {
            DataTable dt = dal.getGenContent();
            return dt;
        }

        /// <summary>
        /// 得到栏目中文名称
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public string getClassCName(string ClassID)
        {
            return dal.getClassCName(ClassID);
        }

        /// <summary>
        /// 得到专题中文名称
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public string getspecialCName(string ClassID)
        {
            return dal.getspecialCName(ClassID);
        }

        /// <summary>
        /// 取得专题与新闻对应表
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns></returns>
        public DataTable getSpecialNews(string NewsID)
        {
            return dal.getSpecialNews(NewsID);
        }


        #endregion 新闻内容
        #region 栏目
        /// <summary>
        /// 得到栏目信息
        /// </summary>
        /// <param name="nID"></param>
        public DataTable getClassContent(string ClassID)
        {
            DataTable dt = dal.getClassContent(ClassID);
            return dt;
        }

        /// <summary>
        /// 得到英文名称是否重复
        /// </summary>
        /// <param name="ClassEname"></param>
        /// <returns></returns>
        public DataTable getClassEname(string ClassEname)
        {
            DataTable dt = dal.getClassEname(ClassEname);
            return dt;
        }
        /// <summary>
        /// 得到栏目下子栏目的数量
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable checkHasSub(string ParentID)
        {
            DataTable dt = dal.checkHasSub(ParentID);
            return dt;
        }

        /// <summary>
        /// 得到栏目父栏目信息
        /// </summary>
        /// <param name="nID"></param>
        public DataTable getParentClass(string ClassID)
        {
            DataTable dt = dal.getParentClass(ClassID);
            return dt;
        }
        /// <summary>
        /// 得到自定义字段信息
        /// </summary>
        /// <param name="nID"></param>
        public DataTable getdefineTable()
        {
            DataTable dt = dal.getdefineTable();
            return dt;
        }

        /// <summary>
        /// 得到自定义字段信息(修改)
        /// </summary>
        /// <param name="nID"></param>
        public DataTable getdefineEditTable(string ClassID)
        {
            DataTable dt = dal.getdefineEditTable(ClassID);
            return dt;
        }


        /// <summary>
        /// 得到某个ID字定义字段的值
        /// </summary>
        /// <param name="TempID"></param>
        /// <returns></returns>
        public DataTable getdefineEditTablevalue(int TempID)
        {
            DataTable dt = dal.getdefineEditTablevalue(TempID);
            return dt;
        }



        /// <summary>
        /// 插入栏目数据
        /// </summary>
        /// <param name="uc"></param>
        public void insertClassContent(Foosun.Model.ClassContent uc)
        {
            dal.insertClassContent(uc);
        }

        /// <summary>
        /// 更新栏目数据
        /// </summary>
        /// <param name="uc"></param>
        public void UpdateClassContent(Foosun.Model.ClassContent uc)
        {
            dal.UpdateClassContent(uc);
        }

        /// <summary>
        /// 删除栏目到回收站
        /// </summary>
        /// <param name="ClassID"></param>
        public void del_recyleClass(string ClassID)
        {
            dal.del_recyleClass(ClassID);
        }

        /// <summary>
        /// 彻底删除栏目
        /// </summary>
        /// <param name="ClassID"></param>
        public void del_Class(string ClassID)
        {
            dal.del_Class(ClassID);
        }

        /// <summary>
        /// 得到栏目下的子类并彻底删除
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public void GetChildClassdel(string ParentID)
        {
            dal.GetChildClassdel(ParentID);
        }
        /// <summary>
        /// 得到栏目下的子类并删除到回收站
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public void GetChildClassdel_recyle(string ParentID)
        {
            dal.GetChildClassdel_recyle(ParentID);
        }

        /// <summary>
        /// 得到栏目列表下的子类
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable getChildList(string ParentID)
        {
            DataTable dt = dal.getChildList(ParentID);
            return dt;
        }

        /// <summary>
        /// 改变栏目状态
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="NUM"></param>
        public void ChangeLock(string ClassID, int NUM)
        {
            dal.ChangeLock(ClassID, NUM);
        }

        /// <summary>
        /// 得到栏目状态
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable getLock(string ClassID)
        {
            DataTable dt = dal.getLock(ClassID);
            return dt;
        }

        /// <summary>
        /// 复位所有栏目
        /// </summary>
        public void resetClass()
        {
            dal.resetClass();
        }

        /// <summary>
        /// 更改排序
        /// </summary>
        public void resetOrder(int OrderID, string ClassID)
        {
            dal.resetOrder(OrderID, ClassID);
        }

        /// <summary>
        /// 得到源栏目
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable getSouceClass()
        {
            DataTable dt = dal.getSouceClass();
            return dt;
        }


        /// <summary>
        /// 删除源栏目
        /// </summary>
        public void delSouce(string ClassID)
        {
            dal.delSouce(ClassID);
        }

        /// <summary>
        /// 更新目标栏目
        /// </summary>
        public void updateSouce(string sClassID, string tClassID)
        {
            dal.updateSouce(sClassID, tClassID);
        }

        /// <summary>
        /// 更新目标下新闻
        /// </summary>
        public void updateSouce1(string sClassID, string tClassID)
        {
            dal.updateSouce1(sClassID, tClassID);
        }

        /// <summary>
        /// 更新目标栏目
        /// </summary>
        public void changeParent(string sClassID, string tClassID)
        {
            dal.changeParent(sClassID, tClassID);
        }

        /// <summary>
        /// 初始化栏目
        /// </summary>
        public void delClassAll()
        {
            dal.delClassAll();
        }
        /// <summary>
        /// 初始化栏目
        /// </summary>
        public void clearNewsInfo(string ClassId)
        {
            dal.clearNewsInfo(ClassId);
        }

        /// <summary>
        /// 得到栏目
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable getClassInfo_Templet()
        {
            DataTable dt = dal.getClassInfo_Templet();
            return dt;
        }

        /// <summary>
        /// 得到栏目
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public void UpdateClassInfo(string strUpdate, string _Str)
        {
            dal.UpdateClassInfo(strUpdate, _Str);
        }

        /// <summary>
        /// 更新栏目下新闻模板
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public void UpdateClassNewsInfo(string templet, string _Str)
        {
            dal.UpdateClassNewsInfo(templet, _Str);
        }


        /// <summary>
        /// 更新权重
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public void updateOrderP(string ClassID, int OrderID)
        {
            dal.updateOrderP(ClassID, OrderID);
        }

        public DataTable getClassList_Show(string ParentID)
        {
            DataTable dt = dal.getClassList_Show(ParentID);
            return dt;
        }

        /// <summary>
        /// 得到单页面
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getPageContent(string ClassID)
        {
            DataTable dt = dal.getPageContent(ClassID);
            return dt;
        }

        /// <summary>
        /// 添加单页面
        /// </summary>
        /// <param name="uc"></param>
        public void insertPage(Foosun.Model.PageContent uc)
        {
            dal.insertPage(uc);
        }

        /// <summary>
        /// 修改单页面
        /// </summary>
        /// <param name="uc"></param>
        public void updatePage(Foosun.Model.PageContent uc)
        {
            dal.updatePage(uc);
        }


        #endregion 栏目

        #endregion 新闻内容管理结束

        #region 新闻列表
        public DataTable GetPage(string SpecialID, string Editor, string ClassID, string sKeywrd, string DdlKwdType, string sChooses, string SiteID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(SpecialID, Editor, ClassID, sKeywrd, DdlKwdType, sChooses, SiteID, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }

        public DataTable getSiteList()
        {
            return dal.getSiteList();
        }

        public DataTable getSiteParam(string SiteID)
        {
            return dal.getSiteParam(SiteID);
        }

        public int Del_news(string id)
        {
            return dal.Del_news(id);
        }
        public string sel_path(string id)
        {
            return dal.sel_path(id);
        }
        public void Del_newsc(string Id)
        {
            dal.Del_newsc(Id);
        }
        public int Update_Lock(string id, int nums)
        {
            return dal.Update_Lock(id, nums);
        }

        public int Update_ResetOrde(string id)
        {
            return dal.Update_ResetOrde(id);
        }

        public void allCheck(int[] id)
        {
            dal.allCheck(id);
        }

        public DataTable sel_old_News()
        {
            return dal.sel_old_News();
        }
        /// <summary>
        /// 得到栏目下新闻的创建时间
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable sel_old_classNews(string ClassID)
        {
            return dal.sel_old_classNews(ClassID);
        }

        /// <summary>
        /// 得到归档数字
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public int sel_old_classInHitoryDay(string ClassID)
        {
            return dal.sel_old_classInHitoryDay(ClassID);
        }

        public int Add_old_News(string fieldnm, string id, DateTime oldtime)
        {
            return dal.Add_old_News(fieldnm, id, oldtime);
        }
        public int del_new_News(string id)
        {
            return dal.del_new_News(id);
        }
        public int settop(string id)
        {
            return dal.settop(id);
        }
        public int unsettop(string id)
        {
            return dal.unsettop(id);
        }

        public DataTable sel_JS()
        {
            return dal.sel_JS();
        }
        public DataTable sel_JSNews(string NewsID)
        {
            return dal.sel_JSNews(NewsID);
        }
        public int Add_JSFile(string JsID, string Njf_title, string NewsId, string PicPath, string ClassId, string SiteID, DateTime CreatTime, DateTime TojsTime)
        {
            return dal.Add_JSFile(JsID, Njf_title, NewsId, PicPath, ClassId, SiteID, CreatTime, TojsTime);
        }
        public DataTable sel_News_Class()
        {
            return dal.sel_News_Class();
        }
        public DataTable sel_LblNewsTable(string LblNewsTable, string s)
        {
            return dal.sel_LblNewsTable(LblNewsTable, s);
        }
        public DataTable sel_PID(string PID)
        {
            return dal.sel_PID(PID);
        }
        public int del_move(string sTb, string sOrgNews)
        {
            return dal.del_move(sTb, sOrgNews);
        }
        public DataTable sel_sys_NewsIndex(string ClassID)
        {
            return dal.sel_sys_NewsIndex(ClassID);
        }
        public int sel_newsclass(string cid)
        {
            return dal.sel_newsclass(cid);
        }
        public DataTable sel_NewsTitle(string sTb, string ClassID)
        {
            return dal.sel_NewsTitle(sTb, ClassID);
        }
        public DataTable sel1(string sTb, string sOrgNews)
        {
            return dal.sel1(sTb, sOrgNews);
        }
        public string sel_copy_clsaa(string ClassID)
        {
            return dal.sel_copy_clsaa(ClassID);
        }

        public string getFileNameInfo(string NewsID, string DataLib)
        {
            return dal.getFileNameInfo(NewsID, DataLib);
        }
        public void Copy_news(string ClassID, string DataLib, string sOrgNews, string sTb, string NewsID, string FileName)
        {
            dal.Copy_news(ClassID, DataLib, sOrgNews, sTb, NewsID, FileName);
        }
        public DataTable sel_copy_classnews(string NewsTable, string ClassID)
        {
            return dal.sel_copy_classnews(NewsTable, ClassID);
        }
        public void Copy_ClassNews(string ClassID, string DataLib, string sOrgNews, string sTb, string NewsID, string FileName)
        {
            dal.Copy_ClassNews(ClassID, DataLib, sOrgNews, sTb, NewsID, FileName);
        }
        public int del_classmove(string sTb, string sOrgNews)
        {
            return dal.del_classmove(sTb, sOrgNews);
        }
        public int Up_news2(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string sOrgNews)
        {
            return dal.Up_news2(CommTF, DiscussTF, NewsProperty, Templet, OrderID, CommLinkTF, Click, FileEXName, sTb, sOrgNews);
        }
        public int Up_news1(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string sOrgNews)
        {
            return dal.Up_news1(CommTF, DiscussTF, NewsProperty, Templet, OrderID, CommLinkTF, Click, FileEXName, sTb, sOrgNews);
        }
        public void Up_Classnews(int CommTF, int DiscussTF, string NewsProperty, string Templet, int OrderID, int CommLinkTF, int Click, string FileEXName, string sTb, string ClassID, string Tags, string Souce)
        {
            dal.Up_Classnews(CommTF, DiscussTF, NewsProperty, Templet, OrderID, CommLinkTF, Click, FileEXName, sTb, ClassID, Tags, Souce);
        }

        public int sel_NewsID(string NewsID)
        {
            return dal.sel_NewsID(NewsID);
        }

        public string sel_sclasstext(string sclassid)
        {
            return dal.sel_sclasstext(sclassid);
        }
        public int delNumber(string ClassID)
        {
            return dal.delNumber(ClassID);
        }
        public DataTable sle_PicUrl(string ID, string tb)
        {
            return dal.sle_PicUrl(ID, tb);
        }
        public int Up_PicURL(string PicURL, string SPicURL, string ID, string tb)
        {
            return dal.Up_PicURL(PicURL, SPicURL, ID, tb);
        }

        public void upCheckStat(string getID, int levelsID)
        {
            dal.upCheckStat(getID, levelsID);
        }

        public int Up_Lock(string ID)
        {
            return dal.Up_Lock(ID);
        }
        public string select_CheckStat(string ID)
        {
            return dal.select_CheckStat(ID);
        }

        public DataTable getLockNews(string UserName)
        {
            return dal.getLockNews(UserName);
        }
        #endregion

        #region 不规则新闻
        public DataTable GetPages(int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPages(PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        public int Str_DelSql(string UnID)
        {
            return dal.Str_DelSql(UnID);
        }

        public DataTable sel(string unNewsid)
        {
            return dal.sel(unNewsid);
        }

        public DataTable sel_DTNews(string NewsTable, string ONewsID)
        {
            return dal.sel_DTNews(NewsTable, ONewsID);
        }
        public DataTable sel_DT_PicInfo(string NewsID)
        {
            return dal.sel_DT_PicInfo(NewsID);
        }
        public int sel_unNewsid(string unNewsid)
        {
            return dal.sel_unNewsid(unNewsid);
        }
        public int Add_1(string unName, string titleCSS, string unNewsid, string NewsID, string NewsTitle, string NewsTable, string TTNewsCSS, string IsMakePic, string SiteID)
        {
            return dal.Add_1(unName, titleCSS, unNewsid, NewsID, NewsTitle, NewsTable, TTNewsCSS, IsMakePic, SiteID);
        }

        public void delUnID(string UnID)
        {
            dal.delUnID(UnID);
        }

        public int Add_2(string unName, string titleCSS, string SubCSS, string unNewsid, string Arr_OldNewsId, string NewsRow, string NewsTitle, string NewsTable, string SiteID)
        {
            return dal.Add_2(unName, titleCSS, SubCSS, unNewsid, Arr_OldNewsId, NewsRow, NewsTitle, NewsTable, SiteID);
        }

        public int Add_SubNews(string unNewsid, string Arr_OldNewsId, string NewsRow, string NewsTitle, string NewsTable, string SiteID, string titleCSS)
        {
            return dal.Add_SubNews(unNewsid, Arr_OldNewsId, NewsRow, NewsTitle, NewsTable, SiteID, titleCSS);
        }

        public DataTable getUNews(string unNewsid)
        {
            return dal.getUNews(unNewsid);
        }
        public void delSubID(string UnID)
        {
            dal.delSubID(UnID);
        }

        public DataTable sel_TbClass()
        {
            return dal.sel_TbClass();
        }
        public DataTable sel_TbClass1(string PID)
        {
            return dal.sel_TbClass1(PID);
        }
        public DataTable GetPageiframe(string DdlClass, string sKeywrds, string sChoose, string DdlKwdType, int pageindex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPageiframe(DdlClass, sKeywrds, sChoose, DdlKwdType, pageindex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        public int del_Table(string ID)
        {
            return dal.del_Table(ID);
        }
        public int Add_fieldnm(string fieldnm, string id, DateTime oldtime)
        {
            return dal.Add_fieldnm(fieldnm, id, oldtime);
        }
        public int Del_fieldnm(string id)
        {
            return dal.Del_fieldnm(id);
        }
        public DataTable sel_old()
        {
            return dal.sel_old();
        }

        public int Update1(string id)
        {
            return dal.Update1(id);
        }

        public int Update2(string id)
        {
            return dal.Update2(id);
        }

        public string sel_paths(string id)
        {
            return dal.sel_path(id);
        }

        public int infoIDNum(string InfoID, string APIID, string dbtable)
        {
            return dal.infoIDNum(InfoID, APIID, dbtable);
        }

        #endregion
        #region 自定义字段
        #region 修改新闻获得自定义字段

        public string modifyNewsDefineValue(string defineColumns, string NewsID, string DataLib, string DsApiID)
        {
            return dal.modifyNewsDefineValue(defineColumns, NewsID, DataLib, DsApiID);
        }

        #endregion 修改新闻获得自定义字段
        #region 插入自定义字段值

        public void insertDefineSign(string DsNewsID, string DsEName, string DsNewsTable, int DsType, string DsContent, string DsApiID)
        {
            dal.insertDefineSign(DsNewsID, DsEName, DsNewsTable, DsType, DsContent, DsApiID);
        }

        public void UpdateDefineSign(string DsNewsID, string DsEName, string DsNewsTable, int DsType, string DsContent, string DsApiID)
        {
            dal.UpdateDefineSign(DsNewsID, DsEName, DsNewsTable, DsType, DsContent, DsApiID);
        }
        #endregion 插入自定义字段值
        #endregion 自定义字段

        #region 省市信息
        public DataTable getProvinceOrCityList(string pid)
        {
            return dal.getProvinceOrCityList(pid);
        }
        #endregion
        //#region 插入临时表
        //public void insertFormTB(string Prot, string NewsID, DateTime CreatTime, string DataTable, int NewsType, int isConstr, int MaxNumber, int updateNum, string ClassID)
        //{
        //    dal.insertFormTB(Prot, NewsID, CreatTime, DataTable, NewsType, isConstr, MaxNumber, updateNum, ClassID);
        //}
        //#endregion 插入临时表
        #region 百度新闻协议使用
        /// <summary>
        /// 从临时表中读取记录
        /// </summary>
        /// <returns></returns>
        public DataTable getLastFormTB()
        {
            return dal.getLastFormTB();
        }

        /// <summary>
        /// 清除过期的临时新闻
        /// </summary>
        public void delTBDateNumber(int dateNum)
        {
            dal.delTBDateNumber(dateNum);
        }

        public void delTBTypeNumber(int getcondition)
        {
            dal.delTBTypeNumber(getcondition);
        }

        public void delTBNewsID(string NewsID)
        {
            dal.delTBNewsID(NewsID);
        }

        public void delTBNewsClassID(string ClassID)
        {
            dal.delTBNewsClassID(ClassID);
        }

        #endregion 百度新闻协议

        #region 统筹
        public int getNewsRecordEdior(string UserName)
        {
            return dal.getNewsRecordEdior(UserName);
        }
        #endregion 统筹

        /// <summary>
        /// 浏览新闻获得参数
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public string getnewsReview(string NewsID, string gType)
        {
            return dal.getnewsReview(NewsID, gType);
        }

        /// <summary>
        /// 得到新闻附件地址
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string getNewsAccessory(int ID)
        {
            return dal.getNewsAccessory(ID);
        }

        /// <summary>
        /// 更新导航
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public void updateReplaceNavi(string ClassID)
        {
            dal.updateReplaceNavi(ClassID);
        }

        /// <summary>
        /// 得到栏目是不是单页面
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public int getclassPage(string ClassID)
        {
            return dal.getclassPage(ClassID);
        }

        /// <summary>
        /// 复位栏目
        /// </summary>
        /// <param name="ClassID">栏目编号字符串</param>
        public void ClassReset(string ClassID)
        {
            dal.ClassReset(ClassID);
        }

        /// <summary>
        /// 新闻统计
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="flg"></param>
        /// <returns></returns>
        public int newsstat(string siteid, string flg)
        {
            return dal.newsstat(siteid, flg);
        }

        /// <summary>
        /// 得到预览
        /// </summary>
        /// <param name="uID"></param>
        /// <returns></returns>
        public DataTable getUnNewsReview(string uID)
        {
            return dal.getUnNewsReview(uID);
        }

        /// <summary>
        /// 得到栏目下新闻数
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public int getClassNewsCount(string ClassID)
        {
            return dal.getClassNewsCount(ClassID);
        }

        /// <summary>
        /// 批量更新新闻属性
        /// </summary>
        /// <param name="Pro"></param>
        /// <param name="NewsID"></param>
        public void updateNewsPro(string str, string ID, int num)
        {
            dal.updateNewsPro(str, ID, num);
        }
        /// <summary>
        /// 批量更新新闻模板
        /// </summary>
        /// <param name="Pro"></param>
        /// <param name="NewsID"></param>
        public void updateNewstemplet(string str, string ID, int num)
        {
            dal.updateNewstemplet(str, ID, num);
        }
        /// <summary>
        /// 更新新闻权重
        /// </summary>
        /// <param name="str"></param>
        /// <param name="NewsID"></param>
        /// <param name="num"></param>
        public void updateNewsOrder(string str, string ID, int num)
        {
            dal.updateNewsOrder(str, ID, num);
        }

        /// <summary>
        /// 更新评论连接
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsComm(string str, string ID, int num)
        {
            dal.updateNewsComm(str, ID, num);
        }

        /// <summary>
        /// 更新TAG
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsTAG(string str, string ID, int num)
        {
            dal.updateNewsTAG(str, ID, num);
        }

        /// <summary>
        /// 更新点击
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsClick(string str, string ID, int num)
        {
            dal.updateNewsClick(str, ID, num);
        }

        /// <summary>
        /// 更新来源
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsSouce(string str, string ID, int num)
        {
            dal.updateNewsSouce(str, ID, num);
        }

        /// <summary>
        /// 更新来源
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ID"></param>
        /// <param name="num"></param>
        public void updateNewsFileEXstr(string str, string ID, int num)
        {
            dal.updateNewsFileEXstr(str, ID, num);
        }

        public void addSpecialTo(string NewsID, string SpecialID)
        {
            dal.addSpecialTo(NewsID, SpecialID);
        }

        public string GetParamBase(string Name)
        {
            return dal.GetParamBase(Name);
        }

        /// <summary>
        /// 根据ID得到newsID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getNewsIDById(string id)
        {
            return dal.getNewsIDById(id);
        }
    }
}
