//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By ZhenJiang.Wang                  ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class Friend
    {
        private IFriend dal;
        public Friend()
        {
            dal = Foosun.DALFactory.DataAccess.CreateFriend();
        }
        #region friend_add.aspx
        public DataTable  sel_1(string UserNum)
        {
            return dal.sel_1(UserNum);
        }
        public string sel_2(string UserNum)
        {
            return dal.sel_2(UserNum);
        }
        public int sel_3(string UserNum, string bUserName)
        {
            return dal.sel_3(UserNum, bUserName);
        }
        public int sel_4(string UserName)
        {
            return dal.sel_4(UserName);
        }
        public DataTable sel_5(string UserName)
        {
            return dal.sel_5(UserName);
        }
        public int Add_1(STRequestinformation Req)
        {
            return dal.Add_1(Req);
        }
        public int Add_2(STFriend Fri, string UserNum)
        {
            return dal.Add_2(Fri, UserNum);
        }
        public int Add_3(STRequestinformation Req)
        {
            return dal.Add_3(Req);
        }
        public int Add_4(STFriend Fri, string UserNum)
        {
            return dal.Add_4(Fri, UserNum);
        }
        #endregion

        #region friend_Establishment.aspx
        public string sel_6(string UserNum)
        {
            return dal.sel_6(UserNum);
        }
        public int Update(int FE, string UserNum)
        {
            return dal.Update(FE, UserNum);
        }
        #endregion

        #region friendList.aspx
        public int Delete(string FriendUserNum)
        {
            return dal.Delete(FriendUserNum);
        }
        public string sel_GroupNumber(string UserNum)
        {
            return dal.sel_GroupNumber(UserNum);
        }
        public string sel_ReadUser(string GroupNumber)
        {
            return dal.sel_ReadUser(GroupNumber);
        }
        #endregion

        #region friendmanage.aspx
        public int Delete1(string HailFellow)
        {
            return dal.Delete1(HailFellow);
        }
        /// <summary>
        /// 返回类别好友数目
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public int FriendClassCount(string HailFellow)
        {
            return dal.FriendClassCount(HailFellow);
        }
        #endregion

        #region friendmanage_add.aspx
        public int sel_7(string UserNum)
        {
            return dal.sel_7(UserNum);
        }
        public string sel_8()
        {
            return dal.sel_8();
        }
        public int Add_5(STFriendClass FCl, string UserNum)
        {
            return dal.Add_5(FCl, UserNum);
        }
        #endregion

        #region Requestinformation.aspx
        public DataTable sel_9(string UserNum)
        {
            return dal.sel_9(UserNum);
        }

        public string sel_10(string u_menume)
        {
            return dal.sel_10(u_menume);
        }

        public string sel_11(string bUserName)
        {
            return dal.sel_11(bUserName);
        }

        public int Update_1(string bUsername, string qUsername)
        {
            return dal.Update_1(bUsername, qUsername);
        }

        public int Update_2(string bUserNum, string qUserNum)
        {
            return dal.Update_2(bUserNum, qUserNum);
        }

        public int Add_6(string FriendUserNum,string UserNum, string bUserName, string bdUserName, string Hail_Fellow, DateTime CreatTime)
        {
            return dal.Add_6(FriendUserNum,UserNum, bUserName, bdUserName, Hail_Fellow, CreatTime);
        }

        public int Delete_2(string UserName,int ID)
        {
            return dal.Delete_2(UserName, ID);
        }

        public string sel_Content(string u_menume)
        {
            return dal.sel_Content(u_menume);
        }
        #endregion

        /// <summary>
        /// 得到好友列表
        /// </summary>
        /// <param name="UserNum">用户名</param>
        /// <returns></returns>
        public DataTable getFriendList(string UserNum)
        {
            return dal.getFriendList(UserNum);
        }

    }
}