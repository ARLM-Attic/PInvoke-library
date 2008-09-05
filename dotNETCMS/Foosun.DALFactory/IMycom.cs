using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IMycom
    {
        #region usermycom_Look.aspx
        DataTable sel(string Commid);
        int Update(string Title, string Contents, DateTime CreatTime, string Commid);
        DataTable GetPage(string UserNum2, string GoodTitle2, string UserNum, string title, string Um, string dtm1, string dtm2, string isCheck, string islock, string SiteID,string InfoID, string APIID, string DTable, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        //DataTable sel_1();
        string sel_2(string InfoID, string datalib);
        string sel_3(string UserNum);
        DataTable sel_4(string GroupNumber);
        void Delete(string Commid);
        string sel_5(string Commid);
        int Update_1(int OrderID, string Commid);
        string sel_6(string Commid);
        void Update_2(int GoodTitle, string Commid);
        string sel_7(string Commid);
        int Update_3(string Commid, int ch);
        string sel_8(string Commid);
        int Update_4(int islock, string Commid);
        string sel_9(string UserName);

        #endregion
    }
    public sealed partial class DataAccess
    {
        public static IMycom CreateMycom()
        {
            string className = path + ".Mycom";
            return (IMycom)Assembly.Load(path).CreateInstance(className);
        }
    }
}