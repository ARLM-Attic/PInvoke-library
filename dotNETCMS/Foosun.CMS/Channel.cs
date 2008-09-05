//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==              Code By Simplt.Xie                       == 
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
    public class Channel
    {
        Foosun.DALFactory.IModel dal;
        public Channel()
        {
            dal = DataAccess.CreateModel();
        }
        public string getUrl(string Type, int ID, int ChID)
        {
            return dal.getUrl(Type, ID, ChID);
        }

        public IDataReader GetTopicInfo(int ID, int ChID)
        {
            return dal.GetTopicInfo(ID, ChID);
        }

        public string GetChannEName(int ChID)
        {
            return dal.GetChannEName(ChID);
        }
        public int GetTopChID(string EName)
        {
            return dal.GetTopChID(EName);
        }
        #region 基础，创建
        /// <summary>
        /// 得到模型模板
        /// </summary>
        /// <returns></returns>
        public IDataReader getModelTemplet()
        {
            return dal.getModelTemplet();
        }

        /// <summary>
        /// 得到允许投稿的频道
        /// </summary>
        /// <returns></returns>
        public IDataReader getModelTempletisConstr()
        {
            return dal.getModelTempletisConstr();
        }
        /// <summary>
        /// 得到模型具体参数
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        public IDataReader getModelinfo(int ID)
        {
            return dal.getModelinfo(ID);
        }

        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <param name="DataTable"></param>
        public void creatModeltable(string DataTable, int channelType, int isConstr)
        {
            dal.creatModeltable(DataTable, channelType, isConstr);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="uc"></param>
        public void updateDate(Foosun.Model.ChannelInfo uc)
        {
            dal.updateDate(uc);
        }
        public void updateDate1(Foosun.Model.ChannelInfo uc)
        {
            dal.updateDate1(uc);
        }

        /// <summary>
        /// 英文是否存在
        /// </summary>
        /// <param name="eName"></param>
        /// <returns></returns>
        public int getItemCount(string eName, int modelID)
        {
            return dal.getItemCount(eName, modelID);
        }
        /// <summary>
        ///表名是否存在
        /// </summary>
        /// <param name="eName"></param>
        /// <returns></returns>
        public int getDbCount(string sTable, int modelID)
        {
            return dal.getDbCount(sTable, modelID);
        }

        /// <summary>
        /// 得到是否是系统表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int getSysCord(int ID)
        {
            return dal.getSysCord(ID);
        }

        /// <summary>
        /// 删除频道
        /// </summary>
        /// <param name="ID"></param>
        public void delModel(int ID)
        {
            dal.delModel(ID);
        }

        /// <summary>
        /// 锁定删除
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="isLock"></param>
        public void ModelStat(int ID, int isLock)
        {
            dal.ModelStat(ID, isLock);
        }

        /// <summary>
        /// 得到相关参数
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public IDataReader getChInfoMenu(int modelID)
        {
            return dal.getChInfoMenu(modelID);
        }

        /// <summary>
        /// 得到单个字段属性
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IDataReader getChValue(int ID)
        {
            return dal.getChValue(ID);
        }

        /// <summary>
        /// 插入值
        /// </summary>
        /// <param name="uc"></param>
        public void insertFields(Foosun.Model.ChannelValue uc, string TableSTR)
        {
            dal.insertFields(uc, TableSTR);
        }

        public void UpdateFields(Foosun.Model.ChannelValue uc, string TableSTR)
        {
            dal.UpdateFields(uc, TableSTR);
        }
        /// <summary>
        /// 得到频道所使用的表
        /// </summary>
        /// <param name="ChID"></param>
        /// <returns></returns>
        public string getChannelTable(int ChID)
        {
            return dal.getChannelTable(ChID);
        }

        public bool getChannelValueTF(int ChID, string EName, int vID)
        {
            return dal.getChannelValueTF(ChID, EName, vID);
        }

        public void delFileds(int ID, string TableStr)
        {
            dal.delFileds(ID, TableStr);
        }

        public void updateValueFileds(int ID, int Num)
        {
            dal.updateValueFileds(ID, Num);
        }
        #endregion
        #region 栏目部分
        public void updateOrder(int ID, int OrderID, int Num)
        {
            dal.updateOrder(ID, OrderID, Num);
        }
        /// <summary>
        /// 得到栏目的名称
        /// </summary>
        /// <param name="ClassID">自动编号ID</param>
        /// <returns>栏目名称</returns>
        public string getClassName(int ClassID)
        {
            return dal.getClassName(ClassID);
        }

        /// <summary>
        /// 继承频道信息
        /// </summary>
        /// <param name="ChID">频道ＩＤ</param>
        /// <returns></returns>
        public IDataReader ChannelInfo(int ChID)
        {
            return dal.ChannelInfo(ChID);
        }

        public void insertClassInfo(Foosun.Model.ChannelClassInfo uc)
        {
            dal.insertClassInfo(uc);
        }

        public void updateClassInfo(Foosun.Model.ChannelClassInfo uc)
        {
            dal.updateClassInfo(uc);
        }

        /// <summary>
        /// 判断英文名称是否重复
        /// </summary>
        /// <param name="EName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int getClassInfoCord(string EName, int ID)
        {
            return dal.getClassInfoCord(EName, ID);
        }

        public IDataReader GetClassInfo(int ClassID)
        {
            return dal.GetClassInfo(ClassID);
        }

        public int GetTopClassID()
        {
            return dal.GetTopClassID();
        }

        public int getClassNumber(int ClassID)
        {
            return dal.getClassNumber(ClassID);
        }

        public int delClass(int ClassID)
        {
            return dal.delClass(ClassID);
        }

        public int Reset_allClass(int ClassID,int ChID)
        {
            return dal.Reset_allClass(ClassID,ChID);
        }

        public int lockstat(int ClassID, int num)
        {
            return dal.lockstat(ClassID, num);
        }

        public IDataReader getClassList(int ClassID,int ChID)
        {
            return dal.getClassList(ClassID, ChID);
        }

        //合并栏目
        public void utilClass(int sClassID, int tClassID, int ChID)
        {
            dal.utilClass(sClassID, tClassID, ChID);
        }
        //移动栏目
        public void moveClass(int sClassID, int tClassID)
        {
            dal.moveClass(sClassID, tClassID);
        }
        #endregion
        #region 内容部分
        public DataTable GetChannelValueFormInfo(int ChID, string DTable,int ID)
        {
            return dal.GetChannelValueFormInfo(ChID, DTable,ID);
        }

        public DataTable GetChannelUserValueFormInfo(int ChID, string DTable, int ID)
        {
            return dal.GetChannelUserValueFormInfo(ChID, DTable, ID);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="ClassID"></param>
        /// <param name="stat"></param>
        /// <param name="ChID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <param name="SqlCondition"></param>
        /// <returns></returns>
        public DataTable GetPage(string keywords, string islock, string author, string ClassID, string SpecialID, string stat, int ChID, string dbTable, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(keywords,islock,author, ClassID,SpecialID, stat, ChID, dbTable, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }

        public int delContent(int id, int chid,int num)
        {
            return dal.delContent(id,chid,num);
        }

        public int lockContent(int id, int chid, int num)
        {
            return dal.lockContent(id, chid, num);
        }

        public void setOrderContent(int id, int chid, int num)
        {
            dal.setOrderContent(id, chid, num);
        }
        #endregion 

        public int inserContentInfo(Foosun.Model.ChInfoContent uc,string DTable)
        { 
            return dal.inserContentInfo(uc,DTable);
        }

        public void updateContentInfo(Foosun.Model.ChInfoContent uc, string DTable)
        {
            dal.updateContentInfo(uc, DTable);
        }
        public void updateUserContentInfo(Foosun.Model.ChInfoContent uc, string DTable)
        {
            dal.updateUserContentInfo(uc, DTable);
        }

        public void updatePreContentInfo(int ID, string PreContentName, object PreContent, string DTable)
        {
            dal.updatePreContentInfo(ID, PreContentName, PreContent, DTable);
        }

        public IDataReader getContentAll(int ChID,int ID)
        {
            return dal.getContentAll(ChID, ID);
        }
        #region 专题部分

        public string getSpecialName(int SpecialID)
        {
            return dal.getSpecialName(SpecialID);
        }

        public IDataReader getSpecialInfo(int SpecialID)
        {
            return dal.getSpecialInfo(SpecialID);
        }
        /// <summary>
        /// 检查专题英文名称是否重复
        /// </summary>
        /// <param name="EName"></param>
        /// <param name="speicalId"></param>
        /// <returns></returns>
        public int getSpecialCord(string EName, int speicalId)
        {
            return dal.getSpecialCord(EName, speicalId);
        }

        public void insertSpecialInfo(Foosun.Model.ChannelSpecialInfo uc)
        {
            dal.insertSpecialInfo(uc);
        }

        public void updateSpecialInfo(Foosun.Model.ChannelSpecialInfo uc)
        {
            dal.updateSpecialInfo(uc);
        }

        public int getSpecialNumber(int SpecialID)
        {
            return dal.getSpecialNumber(SpecialID);
        }
        public int Reset_allSpecial(int SpecialID, int ChID)
        {
            return dal.Reset_allSpecial(SpecialID, ChID);
        }

        public int lockstatSpecial(int SpecialID, int num)
        {
            return dal.lockstatSpecial(SpecialID, num);
        }

        public int delSpecial(int SpecialID)
        {
            return dal.delSpecial(SpecialID);
        }

        public IDataReader getSpecialList(int SpecialID, int ChID)
        {
            return dal.getSpecialList(SpecialID, ChID);
        }

        //合并栏目
        public void utilSpecial(int sSpecialID, int tSpecialID, int ChID)
        {
            dal.utilSpecial(sSpecialID, tSpecialID, ChID);
        }
        //移动栏目
        public void moveSpecial(int sSpecialID, int tSpecialID)
        {
            dal.moveSpecial(sSpecialID, tSpecialID);
        }

        public int GetSpecialInfoCount(int ID,int ChID)
        {
            return dal.GetSpecialInfoCount(ID, ChID);
        }
        #endregion 
        #region 标签
        public IDataReader getStyleClassList(int ClassID, int ChID)
        {
            return dal.getStyleClassList(ClassID,ChID);
        }

        public DataTable GetStylePage(string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetStylePage(keywords, ClassID, ChID, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }

        public string getStyleClassName(int ClassID)
        {
            return dal.getStyleClassName(ClassID);
        }

        public int delStyleContent(int id, int chid, int num)
        {
            return dal.delStyleContent(id, chid, num);
        }

        public int lockStyleContent(int id, int chid, int num)
        {
            return dal.lockStyleContent(id, chid, num);
        }
        public void InsertStyleContent(Foosun.Model.styleChContent uc)
        {
            dal.InsertStyleContent(uc);
        }
        public void UpdateStyleContent(Foosun.Model.styleChContent uc)
        {
            dal.UpdateStyleContent(uc);
        }

        public int GetStyleRecord(string CName, int ID, int ChID)
        {
            return dal.GetStyleRecord(CName, ID, ChID);
        }

        public void InsertStyleClassContent(int ID, int ChID, string cName)
        {
            dal.InsertStyleClassContent(ID, ChID, cName);
        }

        public void UpdateStyleClassContent(int ID, int ChID, string cName)
        {
            dal.UpdateStyleClassContent(ID, ChID, cName);
        }

        public IDataReader GetStyleContent(int Id, int ChID)
        {
            return dal.GetStyleContent(Id,ChID);
        }

        public int GetStyleClassRecord(string cName, int ID, int ChID)
        {
            return dal.GetStyleClassRecord(cName, ID, ChID);
        }

        public IDataReader GetStyleClassListManage(int ChID, int ParentID)
        {
            return dal.GetStyleClassListManage(ChID, ParentID);
        }

        public IDataReader GetStyleClassInfo(int id, int ChID)
        {
            return dal.GetStyleClassInfo(id, ChID);
        }

        public IDataReader GetDefineStyle(int ChID)
        {
            return dal.GetDefineStyle(ChID);
        }

        public IDataReader GetDefineUserStyle(int ChID)
        {
            return dal.GetDefineUserStyle(ChID);
        }


        public IDataReader GetLabelClassList(int ChID,int ParentID)
        {
            return dal.GetLabelClassList(ChID, ParentID);
        }

        public IDataReader GetLabelContent(int ChID, int ID)
        {
            return dal.GetLabelContent(ChID, ID);
        }

        public int GetLabelNameTF(int ChID, string CName,int ID)
        {
            return dal.GetLabelNameTF(ChID, CName, ID);
        }

        public void InsertLabelContent(Foosun.Model.LabelChContent uc)
        {
            dal.InsertLabelContent(uc);
        }
        public void UpdateLabelContent(Foosun.Model.LabelChContent uc)
        {
            dal.UpdateLabelContent(uc);
        }


        public DataTable GetLabelPage(string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetLabelPage(keywords, ClassID, ChID, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }

        public string getLabelClassName(int ClassID)
        {
            return dal.getLabelClassName(ClassID);
        }

        public int delLabelContent(int id, int chid, int num)
        {
            return dal.delLabelContent(id, chid, num);
        }

        public int lockLabelContent(int id, int chid, int num)
        {
            return dal.lockLabelContent(id, chid, num);
        }

        public IDataReader GetLabelClassInfo(int id, int ChID)
        {
            return dal.GetLabelClassInfo(id, ChID);
        }
        public int GetLabelClassRecord(string cName, int ID, int ChID)
        {
            return dal.GetLabelClassRecord(cName, ID, ChID);
        }

        public void InsertLabelClassContent(int ID, int ChID, string cName)
        {
            dal.InsertLabelClassContent(ID, ChID, cName);
        }

        public void UpdateLabelClassContent(int ID, int ChID, string cName)
        {
            dal.UpdateLabelClassContent(ID, ChID, cName);
        }

        public IDataReader GetLabelClassListManage(int ChID, int ParentID)
        {
            return dal.GetLabelClassListManage(ChID, ParentID);
        }

        public int delLabelClassContent(int id, int chid)
        {
            return dal.delLabelClassContent(id, chid);
        }
        /// <summary>
        /// 删除标签分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="chid"></param>
        /// <returns></returns>
        public int delStyleClassContent(int id, int chid)
        {
            return dal.delStyleClassContent(id, chid);
        }
        /// <summary>
        /// 得到标签分页
        /// </summary>
        /// <param name="ChID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <param name="SqlCondition"></param>
        /// <returns></returns>
        public DataTable GetSLabelPage(int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetSLabelPage(ChID, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        /// <summary>
        /// 得到所有样式
        /// </summary>
        /// <param name="ChID"></param>
        /// <returns></returns>
        public IDataReader GetStyleListAll(int ChID)
        {
            return dal.GetStyleListAll(ChID);
        }
        #endregion

        /// <summary>
        /// 判断是否单页面
        /// </summary>
        public int getclassPage(int ClassID)
        {
            return dal.getclassPage(ClassID);
        }
        public int getClassIDfromTable(int ID, int ChID)
        {
            return dal.getClassIDfromTable(ID, ChID);
        }

        public void updateInfoSpecial(string ID, string SpecialID, int ChID)
        {
            dal.updateInfoSpecial(ID, SpecialID, ChID);
        }

        #region 前台会员部分
        public DataTable GetUserChannelPage(string Author,string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetUserChannelPage(Author,keywords, ClassID, ChID, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }

        public void updateUserInfo(int Id, int ChID, int Num,string UserName)
        {
            dal.updateUserInfo(Id, ChID, Num, UserName);
        }

        public string getfUrl(int ID, int ChID)
        {
            return dal.getfUrl(ID, ChID);
        }

        public int AddinfoClick(int ID, int ChID)
        {
            return dal.AddinfoClick(ID, ChID);
        }
        #endregion 
    }
}
