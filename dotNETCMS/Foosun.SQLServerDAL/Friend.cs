///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
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
    public class Friend : DbBase, IFriend
    {
        #region friend_add.aspx
        public DataTable sel_1(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select FriendName,HailFellow From " + Pre + "User_FriendClass where UserNum=@UserNum or gdfz='1'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_2(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select UserName From " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int sel_3(string UserNum, string bUserName)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserNum", UserNum), new SqlParameter("@bUserName", bUserName) };
            string Sql = "Select count(*) From " + Pre + "User_Friend where UserNum=@UserNum and UserName=@bUserName";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int sel_4(string UserName)
        {
            SqlParameter param = new SqlParameter("@UserName", UserName);
            string Sql = "Select count(*) From " + Pre + "sys_user where UserName=@UserName";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public DataTable sel_5(string UserName)
        {
            SqlParameter param = new SqlParameter("@UserName", UserName);
            string Sql = "Select Addfriendbs,UserNum From " + Pre + "sys_User where UserName=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Add_1(STRequestinformation Req)
        {
            string Sql = "insert into " + Pre + "User_Requestinformation (qUsername,bUsername,datatime,Content,ischick) values(@qUserName,@bUserName,@CreatTime,@Content,1)";
            SqlParameter[] parm = GetRequestinformation(Req);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int Add_2(STFriend Fri, string UserNum)
        {
            string Sql = "insert into " + Pre + "User_Friend (FriendUserNum,UserNum,UserName,bUserNum,HailFellow,CreatTime,hyyz) values(@FriendUserNum,@UserNum,@bUserName,@bdUserName,@HailFellow,@CreatTime,1)";
            SqlParameter[] parm = GetFriend(Fri);
            int i_length = parm.Length;
            Array.Resize<SqlParameter>(ref parm, i_length + 1);
            parm[i_length] = new SqlParameter("@UserNum", UserNum);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int Add_3(STRequestinformation Req)
        {
            string Sql = "insert into " + Pre + "User_Requestinformation (qUsername,bUsername,datatime,Content,ischick) values(@qUserName,@bUserName,@CreatTime,@Content,0)";
            SqlParameter[] parm = GetRequestinformation(Req);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int Add_4(STFriend Fri, string UserNum)
        {
            string Sql = "insert into " + Pre + "User_Friend (FriendUserNum,UserNum,UserName,bUserNum,HailFellow,CreatTime,hyyz) values(@FriendUserNum,@UserNum,@bUserName,@bdUserName,@HailFellow,@CreatTime,0)";
            SqlParameter[] parm = GetFriend(Fri);
            int i_length = parm.Length;
            Array.Resize<SqlParameter>(ref parm, i_length + 1);
            parm[i_length] = new SqlParameter("@UserNum", UserNum);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        private SqlParameter[] GetRequestinformation(STRequestinformation Req)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@qUserName", SqlDbType.NVarChar, 50);
            parm[0].Value = Req.qUsername;
            parm[1] = new SqlParameter("@bUserName", SqlDbType.NVarChar, 50);
            parm[1].Value = Req.bUsername;
            parm[2] = new SqlParameter("@Content", SqlDbType.NVarChar, 50);
            parm[2].Value = Req.Content;
            parm[3] = new SqlParameter("@CreatTime", SqlDbType.DateTime);
            parm[3].Value = DateTime.Now;
            return parm;
        }
        private SqlParameter[] GetFriend(STFriend Fri)
        {
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@bUserName", SqlDbType.NVarChar, 50);
            parm[0].Value = Fri.UserName;
            parm[1] = new SqlParameter("@bdUserName", SqlDbType.NVarChar, 50);
            parm[1].Value = Fri.bUserNum;
            parm[2] = new SqlParameter("@HailFellow", SqlDbType.NVarChar, 50);
            parm[2].Value = Fri.HailFellow;
            parm[3] = new SqlParameter("@CreatTime", SqlDbType.DateTime);
            parm[3].Value = DateTime.Now;
            parm[4] = new SqlParameter("@FriendUserNum", SqlDbType.NVarChar, 50);
            parm[4].Value = Rand.Number(12);
            return parm;
        }
        #endregion

        #region friend_Establishment.aspx
        public string sel_6(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select Addfriendbs From " + Pre + "sys_user where UserNum=@UserNum";
            string ret = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
            if (ret == null && ret == "")
                throw new Exception("对不起,数据错误!");
            else
                return ret;
        }
        public int Update(int FE, string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "update " + Pre + "sys_user set Addfriendbs='" + FE + "'where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region friendList.aspx
        public int Delete(string FriendUserNum)
        {
            SqlParameter param = new SqlParameter("@FriendUserNum", FriendUserNum);
            string Sql = "Delete  " + Pre + "User_Friend where FriendUserNum=@FriendUserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_GroupNumber(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select UserGroupNumber from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_ReadUser(string GroupNumber)
        {
            SqlParameter param = new SqlParameter("@GroupNumber", GroupNumber);
            string Sql = "select ReadUser from " + Pre + "user_Group where GroupNumber=@GroupNumber";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #region friendmanage.aspx
        public int Delete1(string HailFellow)
        {
            SqlParameter param = new SqlParameter("@HailFellow", HailFellow);
            string Sql = "Delete  " + Pre + "User_FriendClass where HailFellow=@HailFellow";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public int FriendClassCount(string HailFellow)
        {
            SqlParameter param = new SqlParameter("@HailFellow", HailFellow);
            string Sql = "select count(*) as nnn from " + Pre + "User_Friend where HailFellow=@HailFellow and usernum='" + Foosun.Global.Current.UserNum + "'";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #region friendmanage_add.aspx
        public int sel_7(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select count(*) from " + Pre + "User_FriendClass where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_8()
        {
            string Sql = "select HailFellow from " + Pre + "User_FriendClass";
            return (string)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }
        public int Add_5(STFriendClass FCl, string UserNum)
        {
            string Sql = "insert into " + Pre + "User_FriendClass(UserNum,FriendName,Content,CreatTime,HailFellow)values(@UserNum,@FriendName,@Contents,@CreatTime,@HailFellow)";
            SqlParameter[] parm = GetFriendClass(FCl);
            int i_length = parm.Length;
            Array.Resize<SqlParameter>(ref parm, i_length + 1);
            parm[i_length] = new SqlParameter("@UserNum", UserNum);
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        private SqlParameter[] GetFriendClass(STFriendClass FCl)
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@FriendName", SqlDbType.NVarChar, 50);
            parm[0].Value = FCl.FriendName;
            parm[1] = new SqlParameter("@Contents", SqlDbType.NVarChar, 50);
            parm[1].Value = FCl.Content;
            parm[2] = new SqlParameter("@HailFellow", SqlDbType.NVarChar, 50);
            parm[2].Value = FCl.HailFellow;
            parm[3] = new SqlParameter("@CreatTime", SqlDbType.DateTime);
            parm[3].Value = DateTime.Now;
            return parm;
        }
        #endregion

        #region Requestinformation.aspx
        public DataTable sel_9(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select FriendName,HailFellow From " + Pre + "User_FriendClass where UserNum=@UserNum or gdfz='1'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_10(string u_menume)
        {
            SqlParameter param = new SqlParameter("@u_menume", u_menume);
            string Sql = "select qUsername from " + Pre + "User_Requestinformation where bUsername=@u_menume and ischick=1  order by id desc";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_Content(string u_menume)
        {
            SqlParameter param = new SqlParameter("@u_menume", u_menume);
            string Sql = "select Content from " + Pre + "User_Requestinformation where bUsername=@u_menume";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_11(string bUserName)
        {
            SqlParameter param = new SqlParameter("@bUserName", bUserName);
            string Sql = "select UserNum from " + Pre + "sys_User where UserName=@bUserName";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_1(string bUsername, string qUsername)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@bUserName", bUsername), new SqlParameter("@qUsername", qUsername) };
            string Sql = "update " + Pre + "User_Requestinformation set ischick=0 where bUsername=@bUserName and qUsername=@qUsername";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int Update_2(string bUserNum, string qUserNum)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@bUserNum", bUserNum), new SqlParameter("@qUserNum", qUserNum) };
            string Sql = "update " + Pre + "User_Friend set hyyz='0' where bUserNum=@bUserNum and UserNum=@qUserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int Add_6(string FriendUserNum, string UserNum, string bUserName, string bdUserName, string Hail_Fellow, DateTime CreatTime)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@FriendUserNum", FriendUserNum), new SqlParameter("@UserNum", UserNum), new SqlParameter("@bUserName", bUserName), new SqlParameter("@bdUserName", bdUserName), new SqlParameter("@Hail_Fellow", Hail_Fellow), new SqlParameter("@CreatTime", CreatTime) };
            string Sql = "insert into " + Pre + "User_Friend (FriendUserNum,UserNum,UserName,bUserNum,HailFellow,CreatTime,hyyz) values(@FriendUserNum,@UserNum,@bUserName,@bdUserName,@Hail_Fellow,@CreatTime,0)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete_2(string UserName, int ID)
        {
            SqlParameter param = new SqlParameter("@UserName", UserName);
            string Sql = "delete " + Pre + "User_Requestinformation where bUserName=@UserName and ID=" + ID + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        public DataTable getFriendList(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select top 15 UserName From " + Pre + "User_Friend where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
    }

}
