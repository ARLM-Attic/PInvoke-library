using System;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class UserList : DbBase, IUserList
    {
        public string getGroupName(string strGroupNumber)
        {
            SqlParameter param = new SqlParameter("@GroupNumber", strGroupNumber);
            string RStr = "";
            string Sql = "select GroupName from " + Pre + "user_Group where GroupNumber=@GroupNumber";
            IDataReader rdr = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            if (rdr.Read())
            {
                RStr = rdr["GroupName"].ToString();
            }
            rdr.Close();
            return RStr;
        }

        public int singdel(int id)
        {
            //删除投稿
            rootPublic pd = new rootPublic();
            string _user = pd.getUidUserNum(id);
            pd.delUserAllInfo(_user);
            string Sql = "delete from " + Pre + "sys_user where id = " + id + Foosun.Common.Public.getSessionStr() + "";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null));
        }

        public int dels(string id)
        {
            rootPublic pd = new rootPublic();
            string _user = "";
            if (id.IndexOf(",") == -1)
            {
                _user = pd.getUidUserNum(int.Parse(id));
                pd.delUserAllInfo(_user);
            }
            else
            {
                for (int m = 0; m < id.Split(',').Length; m++)
                {
                    _user = pd.getUidUserNum(int.Parse(id.Split(',')[m]));
                    pd.delUserAllInfo(_user);
                }
            }
            string Sql = "delete from " + Pre + "sys_user where id in (" + id + ") " + Foosun.Common.Public.getSessionStr() + "";
            object result = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return (int)result;
        }

        public int isLock(string id)
        {
            string Sql = "update " + Pre + "sys_user set islock=1 where id in (" + id.Trim() + ") " + Foosun.Common.Public.getSessionStr() + "";
            object result = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return (int)result;
        }

        public int unLock(string id)
        {
            string Sql = "update " + Pre + "sys_user set islock=0 where id in (" + id.Trim() + ") " + Foosun.Common.Public.getSessionStr() + "";
            object result = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return (int)result;
        }


        public DataTable GroupList()
        {
            string Sql = "select ID,GroupNumber,GroupName from " + Pre + "user_group where SiteID='" + Foosun.Global.Current.SiteID + "' order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public int bIpoint(string uid, int sPoint)
        {
            string Sql = "update " + Pre + "sys_user set iPoint=iPoint+" + sPoint + " where id in (" + uid + ") " + Foosun.Common.Public.getSessionStr() + "";
            object result = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return (int)result;
        }

        public int sIpoint(string uid, int sPoint)
        {
            string Sql = "update " + Pre + "sys_user set iPoint=iPoint-" + sPoint + " where id in (" + uid + ") " + Foosun.Common.Public.getSessionStr() + "";
            object result = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return (int)result;
        }

        public int bGpoint(string uid, int sPoint)
        {
            string Sql = "update " + Pre + "sys_user set gPoint=gPoint+" + sPoint + " where id in (" + uid + ") " + Foosun.Common.Public.getSessionStr() + "";
            object result = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return (int)result;
        }

        public int sGpoint(string uid, int sPoint)
        {
            string Sql = "update " + Pre + "sys_user set gPoint=gPoint-" + sPoint + " where id in (" + uid + ") " + Foosun.Common.Public.getSessionStr() + "";
            object result = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            return (int)result;
        }

        public DataTable GetPage(string UserName, string RealName, string UserNum, string Sex, string siPoint, string biPoint, string sgPoint, string bgPoint, string _userlock, string _group, string _iscerts, string _SiteID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {

            string QSQL = "";
            if (UserName != "" && UserName != null) { QSQL = " and UserName like '%" + UserName + "%'"; }
            if (RealName != "" && RealName != null) { QSQL = " and RealName like '%" + RealName + "%'"; }
            if (UserNum != "" && UserNum != null) { QSQL = " and UserNum = '" + UserNum + "'"; }
            if (Sex != "" && Sex != null)
            {
                int _Sex = int.Parse(Sex.ToString());
                QSQL += " and Sex = " + _Sex + "";
            }
            if (siPoint != "" && siPoint != null)
            {
                int _siPoint = int.Parse(siPoint.ToString());
                QSQL += " and iPoint >= " + _siPoint + "";
            }

            if (biPoint != "" && biPoint != null)
            {
                int _biPoint = int.Parse(biPoint.ToString());
                QSQL += " and iPoint <= " + _biPoint + "";
            }

            if (sgPoint != "" && sgPoint != null)
            {
                int _sgPoint = int.Parse(sgPoint.ToString());
                QSQL += " and gPoint >= " + _sgPoint + "";
            }

            if (bgPoint != "" && bgPoint != null)
            {
                int _bgPoint = int.Parse(bgPoint.ToString());
                QSQL += " and gPoint <= " + _bgPoint + "";
            }

            if (_userlock != null && _userlock != "")
            {
                QSQL += " and islock=" + int.Parse(_userlock) + "";
            }

            if (_group != "" && _group != null)
            {
                QSQL += " and UserGroupNumber='" + _group + "'";
            }

            if (_iscerts != "" && _iscerts != null)
            {
                if (_iscerts == "1")
                {
                    QSQL += " and isIDcard=" + int.Parse(_iscerts) + "";
                }
                else
                {
                    QSQL += "and isIDcard<>1 and len(IDcardFiles)>2";
                }
            }

            if (_SiteID != "" && _SiteID != null) { QSQL += " and SiteID='" + _SiteID + "'"; }
            else { QSQL += " and SiteID='" + Foosun.Global.Current.SiteID + "'"; }
            string AllFields = "id,userNum,username,RealName,UserGroupNumber,islock,RegTime,ipoint,gPoint,LastLoginTime,LastIP,isIDcard,isAdmin";
            string Condition = "" + Pre + "sys_user where  1=1 " + QSQL + "";
            string IndexField = "ID";
            string OrderFields = "order by Id Desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
    }
}
