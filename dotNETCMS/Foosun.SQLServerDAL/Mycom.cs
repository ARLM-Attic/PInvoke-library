using System;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Common;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Mycom : DbBase, IMycom
    {
        #region usermycom_Look.aspx
        public DataTable sel(string Commid)
        {
            SqlParameter param = new SqlParameter("@Commid", Commid);
            string Sql = "select Title,Content,IP,UserNum,creatTime from " + Pre + "api_commentary where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update(string Title, string Contents, DateTime CreatTime, string Commid)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 200);
            param[0].Value = Title;
            param[1] = new SqlParameter("@Contents", SqlDbType.NText);
            param[1].Value = Contents;
            param[2] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[2].Value = CreatTime;
            param[3] = new SqlParameter("@Commid", SqlDbType.NVarChar, 12);
            param[3].Value = Commid;

            string Sql = "update " + Pre + "api_commentary set Title=@Title,Content=@Contents,creatTime=@CreatTime where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public DataTable GetPage(string UserNum2, string GoodTitle2, string UserNum, string title, string Um, string dtm1, string dtm2, string isCheck, string islock, string SiteID, string infoID, string APIID, string DTable, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            if (UserNum2 == null) UserNum2 = "";
            if (UserNum == null) UserNum = "";
            if (title == null)title = "";
            if (Um == null) Um = "";
            if (isCheck == null) isCheck = "";
            if (islock == null) islock = "";
            if (SiteID == null) SiteID = "";
            if (infoID == null) infoID = "";
            if (APIID == null) APIID = "";
            if (dtm1 == null) dtm1 = "";
            if (dtm2 == null) dtm2 = "";
            if (GoodTitle2 == null) GoodTitle2 = "";
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserNum2", UserNum2), new SqlParameter("@UserNum", UserNum), new SqlParameter("@title", title), new SqlParameter("@Um", Um), new SqlParameter("@dtm1", dtm1), new SqlParameter("@dtm2", dtm2), new SqlParameter("@isCheck", isCheck), new SqlParameter("@islock", islock), new SqlParameter("@SiteID", SiteID), new SqlParameter("@infoID", infoID), new SqlParameter("@APIID", APIID) };
            string QSQL = "";
            if (UserNum != "" && UserNum != null)
                QSQL = " and UserNum=@UserNum";
            if (title != "" && title != null)
                QSQL += " and Title like '%" + title + "%'";
            if (dtm1 != "" && dtm1 != null && dtm2 != "" && dtm2 != null)
            {
                DateTime dtms1 = DateTime.Parse(dtm1);
                DateTime dtms2 = DateTime.Parse(dtm2);
                QSQL += " and creatTime >= '" + dtms1 + "' and  creatTime <= '" + dtms1 + "'";
            }
            if (isCheck != "" && isCheck != null && isCheck != "0")
            {
                QSQL += " and isCheck=@isCheck";
            }
            if (islock != "" && islock != null && islock != "0")
            {
                QSQL += " and islock=@islock";
            }
            string GT = null;
            if (GoodTitle2 != null && GoodTitle2 != "")
                GT = " and GoodTitle='1'";
            string um = null;
            if (UserNum2 != null && UserNum2 != "")
                um = " and UserNum=@UserNum2";
            string siteID1 = "";
            if (Foosun.Global.Current.SiteID != "0")
                siteID1 = " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            else
            {
                if (SiteID != "" && SiteID != null)
                    siteID1 = " and SiteID=@SiteID";
            }
            if (infoID != string.Empty && infoID != null)
                QSQL += " and InfoID=@infoID";
            if (APIID != string.Empty && APIID != null)
                QSQL += " and APIID=@APIID";
            if (DTable != string.Empty && DTable != null)
                QSQL += " and DataLib = '" + DTable + "'";
            string AllFields = "Commid,Title,InfoID,APIID,creatTime,isCheck,UserNum,islock,OrderID,GoodTitle,datalib,Content";
            string Condition = "" + Pre + "api_commentary where 1=1 " + QSQL + siteID1 + um + GT + "";
            string IndexField = "ID";
            string OrderFields = "order by OrderID Desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, param);
        }
        public string sel_2(string InfoID, string datalib)
        {
            SqlParameter param = new SqlParameter("@NewsID", InfoID);
            string Sql = "select a.NewsType,a.URLaddress,a.SavePath,a.FileName,a.FileEXName,a.isDelPoint,a.NewsTitle,b.savepath as savepath1,b.SaveClassframe from " + datalib + " a," + Pre + "news_class b where a.NewsID=@NewsID and a.classid=b.classid";
            IDataReader dt = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            string URL = "";
            string NewsTitle = "";
            string dimm = Foosun.Config.UIConfig.dirDumm;
            if (dimm.Trim() != string.Empty)
            {
                dimm = "/" + dimm;
            }
            if (dt.Read())
            {
                NewsTitle = dt["NewsTitle"].ToString(); ;
                if (dt["NewsType"].ToString() != "2")
                {
                    if (dt["isDelPoint"].ToString() == "0")
                    {
                        URL = dimm + "/" + dt["savepath1"] + "/" + dt["SaveClassframe"] + "/" + dt["SavePath"] + "/" + dt["FileName"] + dt["FileEXName"];
                    }
                    else
                    {
                        URL = dimm + "/content.aspx?id=" + InfoID + "";
                    }
                    URL = URL.Replace("//", "/");
                }
                else
                {
                    URL = dt["URLaddress"].ToString(); 
                }
                
            }
            dt.Close();
            return "<a href=\"" + URL + "\" class=\"list_link\" target=\"_blank\">" + NewsTitle + "</a>";
        }
        
        public string sel_3(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select UserGroupNumber from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_4(string GroupNumber)
        {
            SqlParameter param = new SqlParameter("@GroupNumber", GroupNumber);
            string Sql = "select DelSelfTitle,DelOTitle from " + Pre + "user_Group where GroupNumber=@GroupNumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public void Delete(string Commid)
        {
            SqlParameter param = new SqlParameter("@Commid", Commid);
            string Sql = "delete " + Pre + "api_commentary  where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_5(string Commid)
        {
            SqlParameter param = new SqlParameter("@Commid", Commid);
            string Sql = "select OrderID from " + Pre + "api_commentary where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_1(int OrderID, string Commid)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            param[0].Value = OrderID;
            param[1] = new SqlParameter("@Commid", SqlDbType.NVarChar, 12);
            param[1].Value = Commid;
            string Sql = "update  " + Pre + "api_commentary set OrderID=@OrderID where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_6(string Commid)
        {
            SqlParameter param = new SqlParameter("@Commid", Commid);
            string Sql = "select GoodTitle from " + Pre + "api_commentary where Commid=@Commid and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public void Update_2(int GoodTitle, string Commid)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@GoodTitle", SqlDbType.Int, 1);
            param[0].Value = GoodTitle;
            param[1] = new SqlParameter("@Commid", SqlDbType.NVarChar, 12);
            param[1].Value = Commid;
            string Sql = "update  " + Pre + "api_commentary set GoodTitle=@GoodTitle where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_7(string Commid)
        {
            SqlParameter param = new SqlParameter("@Commid", Commid);
            string Sql = "select isCheck from " + Pre + "api_commentary where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_3(string Commid, int ch)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@Commid", Commid), new SqlParameter("@ch", ch) };
            string Sql = "update  " + Pre + "api_commentary set isCheck=@ch where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public string sel_8(string Commid)
        {
            SqlParameter param = new SqlParameter("@Commid", Commid);
            string Sql = "select islock from " + Pre + "api_commentary where Commid=@Commid " + Foosun.Common.Public.getSessionStr() + "";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_4(int islock, string Commid)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@islock", islock), new SqlParameter("@Commid", Commid) };
            string Sql = "update  " + Pre + "api_commentary set islock=@islock where Commid=Commid" + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_9(string UserName)
        {
            SqlParameter param = new SqlParameter("@UserName", UserName);
            string Sql = "select UserNum from " + Pre + "sys_user where UserName=@UserName " + Foosun.Common.Public.getSessionStr() + "";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion
    }
}
