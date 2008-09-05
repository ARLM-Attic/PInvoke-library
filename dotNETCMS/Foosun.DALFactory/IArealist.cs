using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IArealist
    {
        #region arealist.aspx
        DataTable sel(string Cid);
        string sel_1(string ID);
        int Delete(string ID);
        int Delete_2(string ID);
        #endregion

        #region arealist_add.aspx
        int Add(string Cid, string cityName, DateTime creatTime, int orderID);
        DataTable sel_2();
        #endregion

        #region Arealist.cs
        DataTable sel_3();
        int Add_1(string Pid, string Cid, string cityName, DateTime creatTime, int orderID);
        DataTable sel_4();
        int sel_nameTF(string aName);
        #endregion

        #region arealist_City.aspx
        int Delete_3(string ID);
        #endregion

        #region arealist_upc.aspx
        DataTable sel_5();
        DataTable sel_6(string pname);
        int Update(string Pid, string cityName, DateTime creatTime, string cids, int OrderID);
        #endregion

        #region arealist_upp.aspx
        DataTable sel_7(string Cid);
        int Update_1(string cityName, DateTime creatTime, string Cids, int OrderID);
        #endregion
    }
    public sealed partial class DataAccess
    {
        public static IArealist CreateArealist()
        {
            string className = path + ".Arealist";
            return (IArealist)Assembly.Load(path).CreateInstance(className);
        }
    }
}
