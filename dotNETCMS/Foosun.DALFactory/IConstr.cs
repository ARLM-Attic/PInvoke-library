///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IConstr
    {
        #region 前台
        int Add(STConstr Con);
         int selGroupNumber(string UserNum);
         DataTable selConstrClass(string UserNum);
         int Update(STConstr Con, string ConIDs);
         string selcName(string u_ClassID);
         string selSiteID(string u_SiteID);
         DataTable sel1(string ConID);
         int sel2();
         int Delete(string ID);
         int sel3(string UserNum);
        int Add1(STuserother Con, string UserNum);
         int Update1(STuserother Con, string ConIDs);
         DataTable sel4(string ConIds);
         int Delete1(string ID);
         DataTable sel5(string ID);
         DataTable sel6();
         int Add2(STConstrClass Con, string Ccid, string UserNum);
        DataTable sel7(string Ccid);
        int Update2(STConstrClass Con, string Ccid);
        string sel_cName(string Ccid);
        string sel_Tags(string ID);
        int Update_Tage1(string tagsd, string ID);
        int Delete2(string ID);
        int Delete3(string ID);
        DataTable sel8(string ID);
        string sel9(string ClassID);
        string sel19(string UserNum);
        DataTable GetPage(string UserNum, string ClassID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        DataTable GetPage1(int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        string ConstrTF();
        #endregion

        #region 后台
        #region Constr_chicklist.aspx
        DataTable sel10(string ID);
        int Update3(string ID);
        int Delete4(string ID);
        #endregion

        #region Constr_Edit.aspx
        DataTable sel11(string ConIDs);
        DataTable sel12();
        DataTable sel13(string ConIDp);
        int Add3(string NewsID, string NewsTitle, string PicURL, string ClassID, string Author, string Editor, string Souce, string Content, string CreatTime, string SiteID, string Tags, string DataLib, string NewsTemplet, string strSavePath, string strfileName, string strfileexName, string strCheckInt);
        int Add3(string NewsID, int NewsType, string NewsTitle, string PicURL, string ClassID, string Author, string Editor, string Souce, string Content, string CreatTime, string SiteID, string Tags, string DataLib, string NewsTemplet, string strSavePath, string strfileName, string strfileexName, string strCheckInt);
        int Add4(string NewsID, int gPoint, int iPoint, int Money1, DateTime CreatTime1, string UserNum, string content4);
        int Update5(int iPoint2, int gPoint2, Decimal Money3, int cPoint2, int aPoint2, string UserNum);
        int Update4(string ConIDp);
        DataTable sel14(string PCIdsa);
        DataTable sel15(string UserNum);
        DataTable sel16();
        DataTable sel17();
        string sel18(string ClassID);
        void updateConstrStrat(string ConID);
        int getParmConstrNum(string UserNum);
        #endregion

        #region Constr_Pay.aspx
        DataTable sel20(string UserNum);
        DataTable sel21(string UserNum);
        DataTable sel22();
        int Add5(string UserNum1, int ParmConstrNums, DateTime payTime, string constrPayID);
        int Update5(string UserNum1);
        #endregion

        #region Constr_Return.aspx
        DataTable sel23(string ConID);
        int Update6(string passcontent, string ConIDs);
        #endregion

        #region Constr_SetParam.aspx
        int Add6(string PCId, string ConstrPayName, string gpoint, string ipoint, int moneys1, string Gunit);
        DataTable sel24();
        #endregion

        #region Constr_SetParamlist.aspx
        int Delete5(string ID);
        #endregion

        #region Constr_SetParamup.aspx
        DataTable sel25(string PCIdup);
        int Update6(string ConstrPayName, string gpoint, string ipoint, int moneys1, string Gunit, string PCIdup);
        #endregion

        #region Constr_Stat.aspx
        int sel26(string UserNum);
        int sel27(string UserNum);
        DataTable sel28(string UserNum);
        int sel29(string UserNum, int m1);
        #endregion

        #region paymentannals.aspx
        DataTable sel30(string UserNum);
        string sel31(string UserNum);
        int Delete6(string ID);
        #endregion
        #endregion
        DataTable getClassInfo(string ClassID);
    }
    public sealed partial class DataAccess
    {
        public static IConstr CreateConstr()
        {
            string className = path + ".Constr";
            return (IConstr)Assembly.Load(path).CreateInstance(className);
        }
    }
}
