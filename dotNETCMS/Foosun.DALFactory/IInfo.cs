///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IInfo
    {
        #region announce.aspx
        string sel_1(string UserNum);
        #endregion

        #region ChangePassword.aspx
        int sel_2(string UserNum, string UserPassword);
        int Update(string UserPassword, string UserNum);
        #endregion

        #region collection.aspx
        int Delete(string DisID);
        bool addTo(string NewsID,int ChID);
        #endregion

        #region Exchange.aspx
        string sel_3(string UserNum);
        string sel_4(string UserGroupNumber);
        DataTable sel_5(string UserNum);
        string sel_6(string UserGroupNumber);
        int Update1(int ipoint2, int gpoint2, string UserNum);
        int Add(STGhistory Gh, int ghtype, string UserNum, string content);
        #endregion

        #region getPassword.aspx
        DataTable sel_7(string UserName);
        DataTable sel_8(string u_un);
        int Update2(string UserPassword, string UserName);
        #endregion

        #region getPoint.aspx
        DataTable sel_9(string Number);
        DataTable sel_10(string cnm);
        int sel_11(string cnm);
        int sel_12();
        int Add1(string GhID, string UserNum, int Gpoint, int Money, DateTime CreatTime, string content);
        int Update3(string UserNum, string cnm);
        int Update4(int Money1, string UserNum);
        int Update5(int points, string UserNum);
        #endregion

        #region history.aspx
        int Delete1(string ID);
        string sel_13(string UserNum);
        #endregion

        #region /history.aspx
        int historyCount(string strDay);
        #endregion

        #region mycom.aspx
        DataTable sel_14();
        string sel_15(string UserNum);
        DataTable sel_16(string GroupNumber);
        DataTable sel_17(string UserNum);
        DataTable sel_18(string UserGroupNumber);
        DataTable GetPage(string title, string Um, string dtm1, string dtm2, string isCheck, string islock, string SiteID, string UserNum, int DelOTitle, int EditOtitle, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        int Delete2(string Commid);
        #endregion

        #region mycom_Look.aspx
        DataTable sel_19(string Commid);
        #endregion

        #region mycom_up.aspx
        int Update6(string Title, string Contents, DateTime CreatTime, string Commid, int islocks);
        #endregion

        #region pointhistory.aspx
        DataTable GetPagepoi(string typep, string UM, string sle_NUM, string SiteID,string UserNum, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        DataTable sel_20(string UM);
        DataTable sel_21(string UserNum);
        int Delete3(string GhID);
        #endregion

        #region 友情连接
        DataTable IsOpen();
        DataTable ClassName_Click(string ClassID);
        DataTable ClassInfo();
        DataTable StartUserC();
        DataTable PramValue();
        int ISExitNamee(string Str_Name);
        int SaveLink(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, string Str_Author, string Str_Mail, string Str_ContentFor);
        void delf(string id);
        DataTable getflist(int num, string uid);
        #endregion
    }
    public sealed partial class DataAccess
    {
        public static IInfo CreateInfo()
        {
            string className = path + ".Info";
            return (IInfo)Assembly.Load(path).CreateInstance(className);
        }
    }
}
