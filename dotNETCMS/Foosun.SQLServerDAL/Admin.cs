using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Admin : DbBase, IAdmin
    {
        //private string SiteID;

        /// <summary>
        /// 锁定管理员
        /// </summary>
        /// <param name="id">管理员用户编号</param>
        public void Lock(string id)
        {
            SqlParameter param = new SqlParameter("@UserNum", id);
            string str_sql = "Update " + Pre + "sys_admin Set isLock=1 where UserNum=@UserNum";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }
        /// <summary>
        /// 解锁管理员
        /// </summary>
        /// <param name="id">管理员用户编号</param>
        public void UnLock(string id)
        {
            SqlParameter param = new SqlParameter("@UserNum", id);
            string str_sql = "Update " + Pre + "sys_admin Set isLock=0 where UserNum=@UserNum";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="id">管理员编号</param>
        public void Del(string id)
        {
            SqlParameter param = new SqlParameter("@UserNum", id);
            string str_Admin = "Delete From  " + Pre + "sys_admin where UserNum=@UserNum";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Admin, param);
            string str_User = "Update " + Pre + "sys_User Set isAdmin=0 where UserNum=@UserNum";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_User, param);
        }

        /// <summary>
        /// 得到管理员组列表
        /// </summary>
        /// <returns>返回的Table</returns>
        public DataTable GetAdminGroupList()
        {
            string str_Sql = "Select adminGroupNumber,GroupName From " + Pre + "sys_AdminGroup where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }

        /// <summary>
        /// 得到管理员的权限列表
        /// </summary>
        /// <param name="UserNum">用户编号</param>
        /// <param name="Id">管理员ID</param>
        /// <returns></returns>
        public string GetAdminPopList(string UserNum, int Id)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserNum", UserNum), new SqlParameter("@Id", Id) };
            string str = "0|";
            string str_Sql = "Select isSuper,PopList From " + Pre + "sys_Admin where ID=@Id and UserNum=@UserNum";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                str = dt.Rows[0]["isSuper"].ToString() + "|" + dt.Rows[0]["PopList"].ToString();
                dt.Clear(); dt.Dispose();
            }
            return str;
        }

        /// <summary>
        /// 得到站点列表
        /// </summary>
        /// <returns>返回DataTable</returns>
        public DataTable GetSiteList()
        {
            string str_Sql = "";
            if (Foosun.Global.Current.SiteID == "0")
                str_Sql = "Select ChannelID,CName From " + Pre + "news_site Where isRecyle=0 And IsURL=0 and islock=0";
            else
                str_Sql = "Select ChannelID,CName From " + Pre + "news_site Where isRecyle=0 and islock=0 and ParentID='" + Foosun.Global.Current.SiteID + "' And IsURL=0";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null); ;
        }

        /// <summary>
        /// 增加管理员，并插入User表
        /// </summary>
        /// <param name="ac">构造</param>
        /// <returns></returns>
        public int Add(Foosun.Model.AdminInfo ac)
        {
            int result = 0;
            string checkSql = "";
            int recordCount = 0;
            string UserNum = Foosun.Common.Rand.Number(12);
            while (true)
            {
                checkSql = "select count(*) from " + Pre + "sys_User where UserNum='" + UserNum + "'";
                recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
                if (recordCount < 1)
                    break;
                else
                    UserNum = Foosun.Common.Rand.Number(12, true);
            }
            checkSql = "select ID,UserNum from " + Pre + "sys_User where UserName='" + ac.UserName + "'";
            DataTable dts = DbHelper.ExecuteTable(CommandType.Text, checkSql, null);
            SqlParameter[] param = GetAdminParameters(ac);
            if (dts != null)
            {
                if (dts.Rows.Count > 0)
                {
                    string str_upUser = "update " + Pre + "sys_User set isAdmin=1,siteID='" + ac.SiteID + "' where UserNum='" + dts.Rows[0]["UserNum"].ToString() + "'";
                    DbHelper.ExecuteNonQuery(CommandType.Text, str_upUser, null);

                    string str_Admin = "Insert Into " + Pre + "sys_admin(UserNum,isSuper,adminGroupNumber,";
                    str_Admin += "OnlyLogin,isChannel,isLock,SiteID,isChSupper,Iplimited) Values ";
                    str_Admin += "('" + dts.Rows[0]["UserNum"].ToString() + "',@isSuper,@adminGroupNumber,@OnlyLogin,@isChannel";
                    str_Admin += ",@isLock,@SiteID,@isChSupper,@Iplimited)";
                    DbHelper.ExecuteNonQuery(CommandType.Text, str_Admin, param);
                    result = 1;
                }
                else
                {
                    string str_User = "Insert Into " + Pre + "sys_User(UserNum,UserName,UserPassword";
                    str_User += ",RealName,isAdmin,Email,RegTime,SiteID,LoginNumber,OnlineTF,OnlineTime";
                    str_User += ",isLock,aPoint,ePoint,cPoint,gPoint,iPoint,UserGroupNumber,isIDcard) Values (";
                    str_User += "'" + UserNum + "',@UserName,@UserPassword,@RealName,@isAdmin,@Email,";
                    str_User += "@RegTime,@SiteID,@LoginNumber,@OnlineTF,@OnlineTime,@isLock,@aPoint,";
                    str_User += "@ePoint,@cPoint,@gPoint,@iPoint,@UserGroupNumber,0)";
                    DbHelper.ExecuteNonQuery(CommandType.Text, str_User, param);

                    string str_Admin = "Insert Into " + Pre + "sys_admin(UserNum,isSuper,adminGroupNumber,";
                    str_Admin += "OnlyLogin,isChannel,isLock,SiteID,isChSupper,Iplimited) Values ";
                    str_Admin += "('" + UserNum + "',@isSuper,@adminGroupNumber,@OnlyLogin,@isChannel";
                    str_Admin += ",@isLock,@SiteID,@isChSupper,@Iplimited)";
                    DbHelper.ExecuteNonQuery(CommandType.Text, str_Admin, param);
                    result = 1;
                }
                dts.Clear(); dts.Dispose();
            }
            else
            {
                throw new Exception("意外错误");
            }
            return result;
        }

        /// <summary>
        /// 编辑管理员
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        public int Edit(Foosun.Model.AdminInfo ac)
        {
            SqlParameter[] param = GetAdminParameters(ac);
            string str_adminSql = "";
            string str_userSql = "";
            if (ac.UserPassword != null && ac.UserPassword != "" && ac.UserPassword != string.Empty)
            {
                str_adminSql = "Update " + Pre + "sys_User Set UserPassword=@UserPassword,RealName=@RealName";
                str_adminSql += ",Email=@Email,SiteID=@SiteID Where UserNum='" + ac.UserNum + "'";
            }
            else
            {
                str_adminSql = "Update " + Pre + "sys_User Set RealName=@RealName,Email=@Email,";
                str_adminSql += "SiteID=@SiteID Where UserNum='" + ac.UserNum + "'";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, str_adminSql, param);

            if (ac.isChSupper == 0)
            {
                str_userSql = "Update " + Pre + "sys_admin Set adminGroupNumber=@adminGroupNumber,";
                str_userSql += "OnlyLogin=@OnlyLogin,isChannel=@isChannel,isLock=@isLock,";
                str_userSql += "isChSupper=@isChSupper,Iplimited=@Iplimited,";
                str_userSql += "SiteID=@SiteID Where UserNum='" + ac.UserNum + "'";
            }
            else
            {
                str_userSql = "Update " + Pre + "sys_admin ";
                str_userSql += "Set Iplimited=@Iplimited Where UserNum='" + ac.UserNum + "'";
            }
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, str_userSql, param));
        }

        /// <summary>
        /// 得到管理员相关信息
        /// </summary>
        /// <param name="id">传入的管理员编号</param>
        /// <returns></returns>
        public DataTable GetAdminInfo(string id)
        {
            SqlParameter param = new SqlParameter("@UserNum", id);
            string str_Sql = "Select a.UserNum,a.isSuper,a.adminGroupNumber,a.PopList,a.OnlyLogin,a.isChannel,a.isLock,a.SiteID,a.isChSupper,a.Iplimited,b.RealName,b.Email,b.UserName From " + Pre + "sys_admin as a," + Pre + "sys_User as b Where a.UserNum=b.UserNum and b.isAdmin=1 and a.UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, param);
        }

        /// <summary>
        /// 获得构造参数
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        private SqlParameter[] GetAdminParameters(Foosun.Model.AdminInfo ac)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 18);
            param[0].Value = ac.UserName;
            param[1] = new SqlParameter("@UserPassword", SqlDbType.NVarChar, 32);
            param[1].Value = ac.UserPassword;
            param[2] = new SqlParameter("@RealName", SqlDbType.NVarChar, 20);
            param[2].Value = ac.RealName;

            param[3] = new SqlParameter("@isAdmin", SqlDbType.TinyInt, 1);
            param[3].Value = ac.isAdmin;
            param[4] = new SqlParameter("@Email", SqlDbType.NVarChar, 120);
            param[4].Value = ac.Email;
            param[5] = new SqlParameter("@RegTime", SqlDbType.DateTime, 8);
            param[5].Value = ac.RegTime;
            param[6] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[6].Value = ac.SiteID;

            param[7] = new SqlParameter("@LoginNumber", SqlDbType.Int, 4);
            param[7].Value = ac.LoginNumber;
            param[8] = new SqlParameter("@OnlineTF", SqlDbType.Int, 4);
            param[8].Value = ac.OnlineTF;
            param[9] = new SqlParameter("@OnlineTime", SqlDbType.Int, 4);
            param[9].Value = ac.OnlineTime;
            param[10] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[10].Value = ac.isLock;

            param[11] = new SqlParameter("@aPoint", SqlDbType.Int, 4);
            param[11].Value = ac.aPoint;
            param[12] = new SqlParameter("@ePoint", SqlDbType.Int, 4);
            param[12].Value = ac.ePoint;
            param[13] = new SqlParameter("@cPoint", SqlDbType.Int, 4);
            param[13].Value = ac.cPoint;
            param[14] = new SqlParameter("@gPoint", SqlDbType.Int, 4);
            param[14].Value = ac.gPoint;

            param[15] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[15].Value = ac.iPoint;
            param[16] = new SqlParameter("@UserGroupNumber", SqlDbType.NVarChar, 12);
            param[16].Value = ac.UserGroupNumber;

            param[17] = new SqlParameter("@isSuper", SqlDbType.TinyInt, 1);
            param[17].Value = ac.isSuper;
            param[18] = new SqlParameter("@adminGroupNumber", SqlDbType.NVarChar, 12);
            param[18].Value = ac.adminGroupNumber;

            param[19] = new SqlParameter("@OnlyLogin", SqlDbType.TinyInt, 1);
            param[19].Value = ac.OnlyLogin;
            param[20] = new SqlParameter("@isChannel", SqlDbType.TinyInt, 1);
            param[20].Value = ac.isChannel;
            param[21] = new SqlParameter("@isChSupper", SqlDbType.TinyInt, 1);
            param[21].Value = ac.isChSupper;
            param[22] = new SqlParameter("@Iplimited", SqlDbType.NText);
            param[22].Value = ac.Iplimited;

            return param;
        }
        /// <summary>
        /// 更新会员权限
        /// </summary>
        /// <param name="UserNum"></param>
        /// <param name="Id"></param>
        /// <param name="PopLIST"></param>
        public void UpdatePOPlist(string UserNum, int Id, string PopLIST)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserNum", UserNum), new SqlParameter("@PopLIST", PopLIST) };
            string str_User = "Update " + Pre + "sys_admin Set PopList=@PopLIST where UserNum=@UserNum and ID=" + Id + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_User, param);
        }


        /// <summary>
        /// 得到管理员列表
        /// </summary>
        /// <returns></returns>
        public DataTable getAdmininfoList()
        {
            string str_Sql = "Select a.ID,a.UserNum,a.isSuper,a.adminGroupNumber,b.UserName From " + Pre + "sys_admin a left join " + Pre + "sys_User b  on a.UserNum=b.UserNum Where b.isAdmin=1 and a.SiteID='" + Foosun.Global.Current.SiteID + "' order by a.id desc";
            return DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
        }
    }
}
