//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Simplt.Xie                      == 
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
    public interface IModel
    {
        string getUrl(string Type, int ID, int ChID);
        string GetChannEName(int ChID);
        int GetTopChID(string EName);
        IDataReader GetTopicInfo(int ID, int ChID);
        #region 基础，创建频道部分
        IDataReader getModelTemplet();
        IDataReader getModelTempletisConstr();
        IDataReader getModelinfo(int ID);
        void creatModeltable(string DataTable, int channelType, int isConstr);
        void updateDate(Foosun.Model.ChannelInfo uc);
        void updateDate1(Foosun.Model.ChannelInfo uc);
        int getItemCount(string eName, int modelID);
        int getDbCount(string sTable, int modelID);
        int getSysCord(int ID);
        void delModel(int ID);
        void ModelStat(int ID, int isLock);
        IDataReader getChInfoMenu(int modelID);
        IDataReader getChValue(int ID);
        void insertFields(Foosun.Model.ChannelValue uc, string TableSTR);
        void UpdateFields(Foosun.Model.ChannelValue uc, string TableSTR);
        string getChannelTable(int ChID);
        bool getChannelValueTF(int ChID, string EName, int vID);
        void delFileds(int ID, string TableStr);
        void updateValueFileds(int ID, int Num);
        #endregion
        #region 栏目部分
        void updateOrder(int ID, int OrderID, int Num);
        string getClassName(int ClassID);
        IDataReader ChannelInfo(int ChID);
        void insertClassInfo(Foosun.Model.ChannelClassInfo uc);
        void updateClassInfo(Foosun.Model.ChannelClassInfo uc);
        int getClassInfoCord(string EName, int ID);
        IDataReader GetClassInfo(int ClassID);
        int GetTopClassID();
        int getClassNumber(int ClassID);
        int delClass(int ClassID);
        int Reset_allClass(int ClassID,int ChID);
        int lockstat(int ClassID, int num);
        IDataReader getClassList(int ClassID, int ChID);
        void utilClass(int sClassID, int tClassID, int ChID);
        void moveClass(int sClassID, int tClassID);
        #endregion
        #region 内容部分
        DataTable GetChannelValueFormInfo(int ChID, string DTable, int ID);
        DataTable GetChannelUserValueFormInfo(int ChID, string DTable, int ID);
        DataTable GetPage(string keywords, string islock, string author, string ClassID, string SpecialID, string stat, int ChID, string dbTable, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        int delContent(int id, int chid,int num);
        int lockContent(int id, int chid, int num);
        void setOrderContent(int id, int chid, int num);
        int inserContentInfo(Foosun.Model.ChInfoContent uc, string DTable);
        void updateContentInfo(Foosun.Model.ChInfoContent uc, string DTable);
        void updateUserContentInfo(Foosun.Model.ChInfoContent uc, string DTable);
        void updatePreContentInfo(int ID, string PreContentName, object PreContent, string DTable);
        IDataReader getContentAll(int ChID, int ID);
        #endregion 
        #region 专题部分
        string getSpecialName(int SpecialID);
        IDataReader getSpecialInfo(int SpecialID);
        int getSpecialCord(string EName, int speicalId);
        void insertSpecialInfo(Foosun.Model.ChannelSpecialInfo uc);
        void updateSpecialInfo(Foosun.Model.ChannelSpecialInfo uc);
        int getSpecialNumber(int SpecialID);
        int Reset_allSpecial(int SpecialID, int ChID);
        int lockstatSpecial(int SpecialID, int num);
        int delSpecial(int SpecialID);
        IDataReader getSpecialList(int SpecialID, int ChID);
        void utilSpecial(int sSpecialID, int tSpecialID, int ChID);
        void moveSpecial(int sSpecialID, int tSpecialID);
        int GetSpecialInfoCount(int ID,int ChID);
        #endregion 
        #region 标签
        IDataReader getStyleClassList(int ClassID, int ChID);
        DataTable GetStylePage(string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        string getStyleClassName(int ClassID);
        int delStyleContent(int id, int chid, int num);
        int lockStyleContent(int id, int chid, int num);
        void InsertStyleContent(Foosun.Model.styleChContent uc);
        void UpdateStyleContent(Foosun.Model.styleChContent uc);
        IDataReader GetStyleContent(int Id, int ChID);
        int GetStyleRecord(string CName, int ID, int ChID);
        void InsertStyleClassContent(int ID, int ChID, string cName);
        void UpdateStyleClassContent(int ID, int ChID, string cName);
        int GetStyleClassRecord(string cName, int ID, int ChID);
        IDataReader GetStyleClassListManage(int ChID, int ParentID);
        IDataReader GetStyleClassInfo(int id, int ChID);
        IDataReader GetDefineStyle(int ChID);
        IDataReader GetDefineUserStyle(int ChID);
        IDataReader GetLabelClassList(int ChID, int ParentID);
        IDataReader GetLabelContent(int ChID, int ID);
        int GetLabelNameTF(int ChID, string CName, int ID);
        void InsertLabelContent(Foosun.Model.LabelChContent uc);
        void UpdateLabelContent(Foosun.Model.LabelChContent uc);

        DataTable GetLabelPage(string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        DataTable GetSLabelPage(int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        string getLabelClassName(int ClassID);
        int delLabelContent(int id, int chid, int num);
        int lockLabelContent(int id, int chid, int num);
        IDataReader GetLabelClassInfo(int id, int ChID);
        int GetLabelClassRecord(string cName, int ID, int ChID);
        void InsertLabelClassContent(int ID, int ChID, string cName);
        void UpdateLabelClassContent(int ID, int ChID, string cName);
        IDataReader GetLabelClassListManage(int ChID, int ParentID);
        int delLabelClassContent(int id, int chid);
        int delStyleClassContent(int id, int chid);
        IDataReader GetStyleListAll(int ChID);
        #endregion
        int getclassPage(int ClassID);
        int getClassIDfromTable(int ID, int ChID);
        void updateInfoSpecial(string ID, string SpecialID, int ChID);
        DataTable GetUserChannelPage(string Author,string keywords, string ClassID, int ChID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        void updateUserInfo(int Id, int ChID, int Num, string UserName);
        string getfUrl(int ID, int ChID);
        int AddinfoClick(int ID, int ChID);
    }


    public sealed partial class DataAccess
    {
        public static IModel CreateModel()
        {
            string className = path + ".Model";
            return (IModel)Assembly.Load(path).CreateInstance(className);
        }
    }
}
