using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Common;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Arealist : DbBase, IArealist
    {
        #region arealist.aspx
        public DataTable sel(string Cid)
        {
            string Sql = "Select Cid,cityName,creatTime From " + Pre + "Sys_City Where Pid='" + Cid + "' and SiteID='" + Foosun.Global.Current.SiteID + "' order by OrderID desc,id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public string sel_1(string ID)
        {
            string Sql = "select Pid from " + Pre + "Sys_City where Cid='" + ID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }

        public int Delete(string ID)
        {
            string Sql = "delete " + Pre + "Sys_City where (Cid='" + ID + "'and Pid='0') or Pid='" + ID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public int Delete_2(string ID)
        {
            string Sql = "delete " + Pre + "Sys_City where  Cid='" + ID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion

        #region arealist_add.aspx
        public int Add(string Cid, string cityName, DateTime creatTime, int orderID)
        {
            string Sql = "insert into " + Pre + "Sys_City(Cid,cityName,Pid,creatTime,SiteID,orderID) values('" + Cid + "','" + cityName + "','0','" + creatTime + "','" + Foosun.Global.Current.SiteID + "'," + orderID + ")";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public DataTable sel_2()
        {
            string Sql = "select Cid from " + Pre + "Sys_City where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public int sel_nameTF(string aName)
        {
            int intflg = 0;
            string Sql = "select id from " + Pre + "Sys_City where cityName='" + aName + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0) { intflg = 1; }
                dt.Clear(); dt.Dispose();
            }
            return intflg;
        }
        #endregion

        #region Arealist.cs
        public DataTable sel_3()
        {
            string Sql = "select Cid,cityName,Pid from " + Pre + "Sys_City where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add_1(string Pid, string Cid, string cityName, DateTime creatTime, int orderID)
        {
            string Sql = "insert into " + Pre + "Sys_City(Pid,Cid,cityName,creatTime,SiteID,orderID) values('" + Pid + "','" + Cid + "','" + cityName + "','" + creatTime + "','" + Foosun.Global.Current.SiteID + "'," + orderID + ")";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_4()
        {
            string Sql = "select Cid from " + Pre + "Sys_City";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        #endregion

        #region arealist_City.aspx
        public int Delete_3(string ID)
        {
            string Sql = "delete " + Pre + "Sys_City where Cid='" + ID + "'";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion

        #region arealist_upc.aspx
        public DataTable sel_5()
        {
            string Sql = "select Cid,cityName from " + Pre + "Sys_City where Pid='0' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_6(string pname)
        {
            string Sql = "select cityName,Pid,OrderID from " + Pre + "Sys_City where Cid='" + pname + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Update(string Pid, string cityName, DateTime creatTime, string cids, int OrderID)
        {
            string Sql = "update " + Pre + "Sys_City set Pid='" + Pid + "',cityName='" + cityName + "',creatTime='" + creatTime + "',OrderID=" + OrderID + " where Cid='" + cids + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion

        #region arealist_upp.aspx
        public DataTable sel_7(string Cid)
        {
            string Sql = "select cityName,OrderID from " + Pre + "Sys_City where Cid='" + Cid + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return dt;
        }

        public int Update_1(string cityName, DateTime creatTime, string Cids, int orderID)
        {
            string Sql = "update " + Pre + "Sys_City set cityName='" + cityName + "',creatTime='" + creatTime + "',orderID=" + orderID + " where Cid='" + Cids + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion
    }
}