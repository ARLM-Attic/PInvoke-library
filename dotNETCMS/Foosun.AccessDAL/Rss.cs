using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Rss : DbBase, IRss
    {
        public int sel(string ClassID)
        {
            string Sql = "select count(*) from " + Pre + "news_Class where ParentID='" + ClassID + "' and isLock=0 and isRecyle=0 and IsURL=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }

        public DataTable getxmllist(string ClassID)
        {
            string _datalib = Pre + "_news";
            string getclassSQL = "Select DataLib from " + Pre + "news_class where ClassID='" + ClassID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, getclassSQL, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _datalib = dt.Rows[0]["DataLib"].ToString();
                }
                dt.Clear(); dt.Dispose();
            }
            string Sql = "select top 5 NewsTitle,CreatTime from " + _datalib + " where ClassID='" + ClassID + "' and isLock=0 and isRecyle=0 order by orderID asc,id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }
    }
}
