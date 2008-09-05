///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IFriend
    {
        #region friend_add.aspx
        DataTable sel_1(string UserNum);
        string sel_2(string UserNum);
        int sel_3(string UserNum, string bUserName);
        int sel_4(string UserName);
        DataTable sel_5(string UserName);
        int Add_1(STRequestinformation Req);
        int Add_2(STFriend Fri, string UserNum);
        int Add_3(STRequestinformation Req);
        int Add_4(STFriend Fri, string UserNum);
        #endregion

        #region friend_Establishment.aspx
        string sel_6(string UserNum);
        int Update(int FE, string UserNum);
        #endregion

        #region friendList.aspx
        int Delete(string FriendUserNum);
        string sel_GroupNumber(string UserNum);
        string sel_ReadUser(string GroupNumber);
        #endregion

        #region friendmanage.aspx
        int Delete1(string HailFellow);
        int FriendClassCount(string HailFellow);
        #endregion

        #region friendmanage_add.aspx
        int sel_7(string UserNum);
        string sel_8();
        int Add_5(STFriendClass FCl, string UserNum);
        #endregion

        #region Requestinformation.aspx
        DataTable sel_9(string UserNum);
        string sel_10(string u_menume);
        string sel_Content(string u_menume);
        string sel_11(string bUserName);
        int Update_1(string bUsername, string qUsername);
        int Update_2(string bUserNum, string qUserNum);
        int Add_6(string FriendUserNum,string UserNum, string bUserName, string bdUserName, string Hail_Fellow, DateTime CreatTime);
        int Delete_2(string UserName,int ID);
        #endregion
        DataTable getFriendList(string UserNum);

    }
    public sealed partial class DataAccess
    {
        public static IFriend CreateFriend()
        {
            string className = path + ".Friend";
            return (IFriend)Assembly.Load(path).CreateInstance(className);
        }
    }
}
