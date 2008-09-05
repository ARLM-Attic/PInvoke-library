using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;


namespace Foosun.AccessDAL
{
    public class Install : DbBase, IInstall
    {
        public int InserAdmin(string UserName, string Password)
        {
            string sql = "select count(id) from " + Pre + "sys_User where UserName='" + UserName + "'";
            int countTF = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, null));
            string gsql = "select count(id) from " + Pre + "sys_User where isAdmin=1";
            countTF = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, gsql, null));
            if (countTF == 0)
            {
                string s_adminpwd = Foosun.Common.Input.MD5(Password, true);
                string s_usernum = Foosun.Common.Rand.Number(12);
                string s_Addadmin = "insert into [" + Pre + "sys_User] ([UserNum],[UserName],[UserPassword],[NickName]," +
                                    "[RealName],[isAdmin],[UserGroupNumber],[PassQuestion],[PassKey],[CertType],[CertNumber]," +
                                    "[Email],[mobile],[Sex],[birthday],[Userinfo],[UserFace],[userFacesize],[marriage],[iPoint]," +
                                    "[gPoint],[cPoint],[ePoint],[aPoint],[isLock],[RegTime],[LastLoginTime],[OnlineTime],[OnlineTF]," +
                                    "[LoginNumber],[FriendClass],[LoginLimtNumber],[LastIP],[SiteID],[Addfriend],[isOpen]," +
                                    "[ParmConstrNum],[isIDcard],[IDcardFiles],[Addfriendbs],[EmailATF],[EmailCode],[isMobile]," +
                                    "[BindTF],[MobileCode]) " +
                                    "values " +
                                    "('" + s_usernum + "','" + UserName + "','" + s_adminpwd + "','admin'," +
                                    "'admin',1,'00000000001','','','','','','12345678901',0,'1986-12-6 00:00:00',''," +
                                    "'/sysImages/user/noHeadpic.gif','50|50',0,15,10,2,0,2,0,'" + DateTime.Now + "'," +
                                    "'" + DateTime.Now + "',0,0,1,'',0,'127.0.0.1','0',2,0,0,1,'',2,1,'',1,0,'');";

                countTF=Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, s_Addadmin, null));

                if(countTF==0)
                {
                    return 0;
                }
                s_Addadmin=" insert into [" + Pre + "sys_admin] ([UserNum],[isSuper],[adminGroupNumber],[PopList]," +
                                    "[OnlyLogin],[isChannel],[isLock],[SiteID],[isChSupper],[Iplimited],[verCode]) " +
                                    "values " +
                                    "('" + s_usernum + "',1,'00000001','',0,0,0,'0',0,'','')";
                return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, s_Addadmin, null));
            }
            else
            {
                return 0;
            }
        }
    }
}
