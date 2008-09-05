//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Simplt.Xie                      ==
//===========================================================
using System;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class user
    {
        Foosun.DALFactory.IUser dal;
        public user()
        {
            dal = DataAccess.CreateUser();
        }
        /// <summary>
        /// 管理员验证
        /// </summary>
        /// <param name="strUserNum"></param>
        /// <returns></returns>
        public int Managestate(string strUserNum)
        {
            return dal.Managestate(strUserNum);
        }
        public int GetUncheckFriendsCount(string UserName)
        {
            return dal.GetUncheckFriendsCount(UserName);
        }
        /// <summary>
        /// 管理员登陆(检查用户是否存在)
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPWD">原始密码</param>
        /// <returns></returns>
        public DataTable CheckUser(string strUserName, string strPWD)
        {
            DataTable dt = dal.CheckUser(strUserName, strPWD);
            return dt;
        }
        #region 登陆限制开始
        ///// <summary>
        ///// 判断是否有此管理员
        ///// </summary>
        ///// <param name="strUserName"></param>
        ///// <returns></returns>
        //public DataTable CheckUserTF(string strUserName)
        //{
        //    DataTable dt = dal.CheckUserTF(strUserName);
        //    return dt;
        //}

        ///// <summary>
        ///// 开始读取管理员是否有限制
        ///// </summary>
        ///// <param name="UserNum"></param>
        ///// <returns></returns>
        //public DataTable readAdminlimit(string UserNum)
        //{
        //    DataTable dt = dal.readAdminlimit(UserNum);
        //    return dt;
        //}

        ///// <summary>
        ///// 查找登陆里是否有记录
        ///// </summary>
        ///// <param name="UserNum"></param>
        ///// <returns></returns>
        //public int loginNumTF(string UserNum)
        //{
        //    return dal.loginNumTF(UserNum);
        //}

        //public int getLoginNum(string UserNum)
        //{
        //    return dal.getLoginNum(UserNum);
        //}        /// <summary>
        ///// 插入登陆记录
        ///// </summary>
        ///// <param name="?"></param>
        //public void insertLoginNum(string UserNum)
        //{
        //    dal.insertLoginNum(UserNum);
        //}
        #endregion 登陆限制结束
        public DataTable CheckManage(string UserNum)
        {
            DataTable dt = dal.CheckManage(UserNum);
            return dt;
        }

        public void UserLogsDels(int LId)
        {
            dal.UserLogsDels(LId);
        }

        public DataTable getUserLogsValue(int LId)
        {
            DataTable dt = dal.getUserLogsValue(LId);
            return dt;
        }

        public DataTable getUserLogsRecord(string logID)
        {
            DataTable dt = dal.getUserLogsRecord(logID);
            return dt;
        }

        public void InsertUserLogs(Foosun.Model.UserLog1 uc)
        {
            dal.InsertUserLogs(uc);
        }

        public void UpdateUserLogs(Foosun.Model.UserLog1 uc)
        {
            dal.UpdateUserLogs(uc);
        }

        public DataTable getCountselt(string UserName)
        {
            DataTable dt = dal.getCountselt(UserName);
            return dt;
        }

        public DataTable getIschick(string UserName)
        {
            DataTable dt = dal.getIschick(UserName);
            return dt;
        }
        /// <summary>
        /// 判断是否是管理员
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable isAdminUser(string UserNum)
        {
            DataTable dt = dal.isAdminUser(UserNum);
            return dt;
        }

        public DataTable sel_isAdmin(string UserNum)
        {
            return dal.sel_isAdmin(UserNum);
        }

        public string sel_pwd(string UserNum)
        {
            return dal.sel_pwd(UserNum);
        }
        public int sel_Rtime(string GroupName)
        {
            return dal.sel_Rtime(GroupName);
        }
        public string sel_UserGroupNumber(string UserNum)
        {
            return dal.sel_UserGroupNumber(UserNum);
        }
        /// <summary>
        /// 读取会员所在组允许上传的图片类型及大小
        /// </summary>
        /// <param name="groupNumber"></param>
        /// <returns></returns>
        public string getuserUpFile(string groupNumber)
        {
            return dal.getuserUpFile(groupNumber);
        }

        /// <summary>
        /// 前台会员注册
        /// </summary>
        /// 
        public int sel_ChannelID(string SiteID)
        {
            return dal.sel_ChannelID(SiteID);
        }
        public DataTable sel_RegContent(string SiteID)
        {
            return dal.sel_RegContent(SiteID);
        }
        public int sel_username(string ID)
        {
            return dal.sel_username(ID);
        }
        public int sel_email(string email)
        {
            return dal.sel_email(email);
        }

        public string sel_um()
        {
            return dal.sel_um();
        }
        public string sel_UserGroupNumbers(string SiteID)
        {
            return dal.sel_UserGroupNumbers(SiteID);
        }

        public bool sel_getUserMobileCode(string UserName, out string mobile, out string mobilecode)
        {
            return dal.sel_getUserMobileCode(UserName, out mobile, out mobilecode);
        }

        public int sel_updateUserMobileStat(string UserName)
        {
            return dal.sel_updateUserMobileStat(UserName);
        }

        public int sel_getUserMobileBindTF(string Moblie)
        {
            return dal.sel_getUserMobileBindTF(Moblie);
        }

        public void sel_updateMobileBindTF(string UserName)
        {
            dal.sel_updateMobileBindTF(UserName);
        }

        public int Add_User(Foosun.Model.User ui)
        {
            return dal.Add_User(ui);
        }
        public int Add_userfields(Foosun.Model.UserFields ufi)
        {
            return dal.Add_userfields(ufi);
        }
        public int Add_Ghistory(Foosun.Model.UserGhistory ugi)
        {
            return dal.Add_Ghistory(ugi);
        }

        public string sel_setPoint(string SiteID)
        {
            return dal.sel_setPoint(SiteID);
        }
        public DataTable sel_reg(string SiteID)
        {
            return dal.sel_reg(SiteID);
        }

        public string getRegTime(string UserNum)
        {
            return dal.getRegTime(UserNum);
        }

        public DataTable getContent(string UserNum)
        {
            return dal.getContent(UserNum);
        }

        public DataTable getGroup(string UserName)
        {
            return dal.getGroup(UserName);
        }
        #region 得到wap

        public DataTable getWapParam()
        {
            return dal.getWapParam();
        }

        public IDataReader getWapContent(string SiteID)
        {
            return dal.getWapContent(SiteID);
        }

        #endregion 得到wap


        /// <summary>
        /// 取得会员信息(如果传值会员自动编号为0的话，则用随机编号取值)
        /// </summary>
        /// <param name="UserNum">会员随机编号</param>
        /// <param name="ID">会员自动编号</param>
        /// <returns></returns>
        public Foosun.Model.User UserInfo(string UserNum, int ID)
        {
            return dal.UserInfo(UserNum, ID);
        }

        /// <summary>
        /// 取得会员附加信息
        /// </summary>
        /// <param name="UserNum">用户编号</param>
        /// <returns></returns>
        public Foosun.Model.UserFields UserFields(string UserNum)
        {
            return dal.UserFields(UserNum);
        }

        /// <summary>
        /// 取得会员组信息
        /// </summary>
        /// <param name="GroupNumber">会员组随机编号</param>
        /// <returns></returns>
        public Foosun.Model.UserGroup UserGroup(string GroupNumber)
        {
            return dal.UserGroup(GroupNumber);
        }

        /// <summary>
        /// 取得站点会员参数
        /// </summary>
        /// <param name="SiteID">站点编号</param>
        /// <returns></returns>
        public Foosun.Model.UserParam UserParam(string SiteID)
        {
            return dal.UserParam(SiteID);
        }
    }
}
