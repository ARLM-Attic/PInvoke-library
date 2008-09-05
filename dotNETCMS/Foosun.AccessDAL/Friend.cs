///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
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
    public class Friend : DbBase, IFriend
    {
        #region friend_add.aspx
        public DataTable sel_1(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select FriendName,HailFellow From " + Pre + "User_FriendClass where UserNum=@UserNum or gdfz=1";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_2(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select UserName From " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int sel_3(string UserNum, string bUserName)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@bUserName", bUserName) };
            string Sql = "Select count(*) From " + Pre + "User_Friend where UserNum=@UserNum and UserName=@bUserName";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int sel_4(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "Select count(*) From " + Pre + "sys_user where UserName=@UserName";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public DataTable sel_5(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "Select Addfriendbs,UserNum From " + Pre + "sys_User where UserName=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Add_1(STRequestinformation Req)
        {
            OleDbParameter[] parm = GetRequestinformation(Req);
            string Sql = "insert into " + Pre + "User_Requestinformation ("+Database.getParam(parm)+",ischick) values("+Database.getAParam(parm)+",1)";
            
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int Add_2(STFriend Fri, string UserNum)
        {
            OleDbParameter[] parm = GetFriend(Fri); 
            string Sql = "insert into " + Pre + "User_Friend ("+Database.getParam(parm)+",hyyz) values("+Database.getAParam(parm)+",1)";
            
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 1);
            parm[i_length] = new OleDbParameter("@UserNum", UserNum);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int Add_3(STRequestinformation Req)
        {
            OleDbParameter[] parm = GetRequestinformation(Req); 
            string Sql = "insert into " + Pre + "User_Requestinformation ("+Database.getParam(parm)+",ischick) values("+Database.getAParam(parm)+",0)";
            
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int Add_4(STFriend Fri, string UserNum)
        {
            string Sql = "insert into " + Pre + "User_Friend (FriendUserNum,UserNum,UserName,bUserNum,HailFellow,CreatTime,hyyz) values(@FriendUserNum,@UserNum,@bUserName,@bdUserName,@HailFellow,@CreatTime,0)";
            OleDbParameter[] parm = GetFriend(Fri);
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 1);
            parm[i_length] = new OleDbParameter("@UserNum", UserNum);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(parm, "FriendUserNum,UserNum,UserName,bUserNum,HailFellow,CreatTime"));
        }
        private OleDbParameter[] GetRequestinformation(STRequestinformation Req)
        {
            OleDbParameter[] parm = new OleDbParameter[4];
            parm[0] = new OleDbParameter("@qUserName", OleDbType.VarWChar, 50);
            parm[0].Value = Req.qUsername;
            parm[1] = new OleDbParameter("@bUserName", OleDbType.VarWChar, 50);
            parm[1].Value = Req.bUsername;
            parm[2] = new OleDbParameter("@Content", OleDbType.VarWChar, 50);
            parm[2].Value = Req.Content;
            parm[3] = new OleDbParameter("@DataTime", OleDbType.Date);
            parm[3].Value = DateTime.Now;
            return parm;
        }
        private OleDbParameter[] GetFriend(STFriend Fri)
        {
            OleDbParameter[] parm = new OleDbParameter[5];
            parm[0] = new OleDbParameter("@UserName", OleDbType.VarWChar, 50);
            parm[0].Value = Fri.UserName;
            parm[1] = new OleDbParameter("@bUserNum", OleDbType.VarWChar, 50);
            parm[1].Value = Fri.bUserNum;
            parm[2] = new OleDbParameter("@HailFellow", OleDbType.VarWChar, 50);
            parm[2].Value = Fri.HailFellow;
            parm[3] = new OleDbParameter("@CreatTime", OleDbType.Date);
            parm[3].Value = DateTime.Now;
            parm[4] = new OleDbParameter("@FriendUserNum", OleDbType.VarWChar, 50);
            parm[4].Value = Rand.Number(12);
            return parm;
        }
        #endregion

        #region friend_Establishment.aspx
        public string sel_6(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select Addfriendbs From " + Pre + "sys_user where UserNum=@UserNum";
            string ret = Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
            if (ret == null && ret == "")
                throw new Exception("对不起,数据错误!");
            else
                return ret;
        }
        public int Update(int FE, string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "update " + Pre + "sys_user set Addfriendbs='" + FE + "'where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region friendList.aspx
        public int Delete(string FriendUserNum)
        {
            OleDbParameter param = new OleDbParameter("@FriendUserNum", FriendUserNum);
            string Sql = "Delete from " + Pre + "User_Friend where FriendUserNum=@FriendUserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_GroupNumber(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserGroupNumber from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_ReadUser(string GroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", GroupNumber);
            string Sql = "select ReadUser from " + Pre + "user_Group where GroupNumber=@GroupNumber";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #region friendmanage.aspx
        public int Delete1(string HailFellow)
        {
            OleDbParameter param = new OleDbParameter("@HailFellow", HailFellow);
            string Sql = "Delete from " + Pre + "User_FriendClass where HailFellow=@HailFellow";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public int FriendClassCount(string HailFellow)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", HailFellow);
            string Sql = "select count(*) as nnn from " + Pre + "User_Friend where HailFellow=@HailFellow and usernum='" + Foosun.Global.Current.UserNum + "'";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #region friendmanage_add.aspx
        public int sel_7(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
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
            OleDbParameter[] parm = GetFriendClass(FCl);
            
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 1);
            parm[i_length] = new OleDbParameter("@UserNum", UserNum);    
            string Sql = "insert into " + Pre + "User_FriendClass(" + Database.getParam(parm) + ")values(" + Database.getAParam(parm) + ")";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        private OleDbParameter[] GetFriendClass(STFriendClass FCl)
        {
            OleDbParameter[] parm = new OleDbParameter[4];
            parm[0] = new OleDbParameter("@FriendName", OleDbType.VarWChar, 50);
            parm[0].Value = FCl.FriendName;
            parm[1] = new OleDbParameter("@Content", OleDbType.VarWChar, 50);
            parm[1].Value = FCl.Content;
            parm[2] = new OleDbParameter("@HailFellow", OleDbType.VarWChar, 50);
            parm[2].Value = FCl.HailFellow;
            parm[3] = new OleDbParameter("@CreatTime", OleDbType.Date);
            parm[3].Value = DateTime.Now;
            return parm;
        }
        #endregion

        #region Requestinformation.aspx
        public DataTable sel_9(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select FriendName,HailFellow From " + Pre + "User_FriendClass where UserNum=@UserNum or gdfz='1'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_10(string u_menume)
        {
            OleDbParameter param = new OleDbParameter("@u_menume", u_menume);
            string Sql = "select qUsername from " + Pre + "User_Requestinformation where bUsername=@u_menume and ischick=1  order by id desc";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_Content(string u_menume)
        {
            OleDbParameter param = new OleDbParameter("@u_menume", u_menume);
            string Sql = "select Content from " + Pre + "User_Requestinformation where bUsername=@u_menume";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_11(string bUserName)
        {
            OleDbParameter param = new OleDbParameter("@bUserName", bUserName);
            string Sql = "select UserNum from " + Pre + "sys_User where UserName=@bUserName";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_1(string bUsername, string qUsername)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@bUserName", bUsername), new OleDbParameter("@qUsername", qUsername) };
            string Sql = "update " + Pre + "User_Requestinformation set ischick=0 where bUsername=@bUserName and qUsername=@qUsername";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int Update_2(string bUserNum, string qUserNum)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@bUserNum", bUserNum), new OleDbParameter("@qUserNum", qUserNum) };
            string Sql = "update " + Pre + "User_Friend set hyyz='0' where bUserNum=@bUserNum and UserNum=@qUserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int Add_6(string FriendUserNum, string UserNum, string bUserName, string bdUserName, string Hail_Fellow, DateTime CreatTime)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@FriendUserNum", FriendUserNum), new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@bUserName", bUserName), new OleDbParameter("@bdUserName", bdUserName), new OleDbParameter("@Hail_Fellow", Hail_Fellow), new OleDbParameter("@CreatTime", CreatTime) };
            string Sql = "insert into " + Pre + "User_Friend (FriendUserNum,UserNum,UserName,bUserNum,HailFellow,CreatTime,hyyz) values(@FriendUserNum,@UserNum,@bUserName,@bdUserName,@Hail_Fellow,@CreatTime,0)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "FriendUserNum,UserNum,UserName,bUserNum,HailFellow,CreatTime"));
        }
        public int Delete_2(string UserName, int ID)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "delete from " + Pre + "User_Requestinformation where bUserName=@UserName and ID=" + ID + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        public DataTable getFriendList(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select top 15 UserName From " + Pre + "User_Friend where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
    }

}
