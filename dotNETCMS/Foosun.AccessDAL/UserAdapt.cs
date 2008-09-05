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
    class UserAdapt : DbBase, IUserAdapt
    {
        public bool isExist(string username)
        {
            OleDbParameter param = new OleDbParameter("@username", username);
            string sql = "select count(UserName) from " + Pre + "sys_User where UserName=@username";
            if (Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param)) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getUserNumByUserName(string username)
        {
            OleDbParameter param = new OleDbParameter("@username", username);
            string sql = "select UserNum from " + Pre + "sys_User where UserName=@username";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }
    }
}
