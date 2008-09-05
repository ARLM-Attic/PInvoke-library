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
    public class UserMisc
    {
        Foosun.DALFactory.IUserMisc dal;
        public UserMisc()
        {
            dal = DataAccess.CreateUserMisc();
        }

        public string GetUserLogs(int ID)
        {
            return dal.GetUserLogs(ID);
        }

        public DataTable getSiteList()
        {
            DataTable dt = dal.getSiteList();
            return dt;
        }

        #region 菜单部分
        public IDataReader Navilist(string strUserNum)
        {
            return dal.Navilist(strUserNum);
        }

        public IDataReader aplist(string strUserNum)
        {
            return dal.aplist(strUserNum);
        }

        public DataTable calendar(string strUserNum)
        {
            DataTable dt = dal.calendar(strUserNum);
            return dt;
        }

        public DataTable messageChar(string strUserNum)
        {
            DataTable dt = dal.messageChar(strUserNum);
            return dt;
        }

        public IDataReader ShortcutList(string strUserNum, int _Num)
        {
            return dal.ShortcutList(strUserNum, _Num);
        }

        public IDataReader menuNavilist(string type, string strUserNum)
        {
            return dal.menuNavilist(type, strUserNum);
        }
        /// <summary>
        /// 得到菜单
        /// </summary>
        /// <returns></returns>
        public DataTable ManagemenuNavilist()
        {
            DataTable dt = dal.ManagemenuNavilist();
            return dt;
        }
        /// <summary>
        /// 得到子菜单
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="nchar"></param>
        /// <returns></returns>
        public DataTable ManagechildmenuNavilist(string pID)
        {
            DataTable dt = dal.ManagechildmenuNavilist(pID);
            return dt;
        }

        /// <summary>
        /// 检查编号是否有重复
        /// </summary>
        /// <param name="am_ClassID"></param>
        /// <returns></returns>
        public DataTable getManageChildNaviRecord(string am_ClassID)
        {
            DataTable dt = dal.getManageChildNaviRecord(am_ClassID);
            return dt;
        }

        public void InsertManageMenu(Foosun.Model.UserInfo7 uc)
        {
            dal.InsertManageMenu(uc);
        }

        public void Shortcutdel(int qID)
        {
            dal.Shortcutdel(qID);
        }

        public void Shortcutde2(string ClassID)
        {
            dal.Shortcutde2(ClassID);
        }

        public DataTable navimenusub(string _str)
        {
            DataTable dt = dal.navimenusub(_str);
            return dt;
        }

        public DataTable GetNaviEditID(int nID)
        {
            DataTable dt = dal.GetNaviEditID(nID);
            return dt;
        }

        public DataTable Getparentidlist()
        {
            DataTable dt = dal.Getparentidlist();
            return dt;
        }

        public DataTable Getchildparentidlist(string pID)
        {
            DataTable dt = dal.Getchildparentidlist(pID);
            return dt;
        }

        public void EditManageMenu(Foosun.Model.UserInfo7 uc)
        {
            dal.EditManageMenu(uc);
        }

        public void EditManageMenu1(Foosun.Model.UserInfo7 uc)
        {
            dal.EditManageMenu1(uc);
        }

        public void QShortcutdel(int QID, int _num)
        {
            dal.QShortcutdel(QID, _num);
        }

        public IDataReader QShortcutList(int _num)
        {
            return dal.QShortcutList(_num);
        }

        public DataTable QeditAction(int QID)
        {
            DataTable dt = dal.QeditAction(QID);
            return dt;
        }
        public DataTable QGetRecord(int num)
        {
            DataTable dt = dal.QGetRecord(num);
            return dt;
        }

        public void InsertQMenu(Foosun.Model.UserInfo8 uc)
        {
            dal.InsertQMenu(uc);
        }

        public void UpdateQMenu(Foosun.Model.UserInfo8 uc)
        {
            dal.UpdateQMenu(uc);
        }

        public DataTable QGetNumberRecord(string StrNumber)
        {
            DataTable dt = dal.QGetNumberRecord(StrNumber);
            return dt;
        }

        #endregion 菜单部分

        #region 会员列表部分

        public DataTable getUserInfobase1(int Uid)
        {
            DataTable dt = dal.getUserInfobase1(Uid);
            return dt;
        }

        public DataTable getUserInfobase2(string strUserNum)
        {
            DataTable dt = dal.getUserInfobase2(strUserNum);
            return dt;
        }
        public DataTable sexlist(int Uid)
        {
            DataTable dt = dal.sexlist(Uid);
            return dt;
        }
        public DataTable marriagelist(int Uid)
        {
            DataTable dt = dal.marriagelist(Uid);
            return dt;
        }

        public DataTable isopenlist(int Uid)
        {
            DataTable dt = dal.isopenlist(Uid);
            return dt;
        }

        public DataTable getUserInfoParam(int Uid)
        {
            DataTable dt = dal.getUserInfoParam(Uid);
            return dt;
        }

        public DataTable getUserInfoNum(int Uid)
        {
            DataTable dt = dal.getUserInfoNum(Uid);
            return dt;
        }

        public DataTable getUserInfoRecord(string strUserNum)
        {
            DataTable dt = dal.getUserInfoRecord(strUserNum);
            return dt;
        }

        public DataTable getPassWord(int Uid)
        {
            DataTable dt = dal.getPassWord(Uid);
            return dt;
        }

        public void UpdateUserInfoBase(Foosun.Model.UserInfo uc)
        {
            dal.UpdateUserInfoBase(uc);
        }

        public void UpdateUserInfoBase1(Foosun.Model.UserInfo1 uc1)
        {
            dal.UpdateUserInfoBase1(uc1);
        }

        public void UpdateUserInfoBase2(Foosun.Model.UserInfo1 uc2)
        {
            dal.UpdateUserInfoBase2(uc2);
        }

        public DataTable getUserInfoContact(string strUserNum)
        {
            DataTable dt = dal.getUserInfoContact(strUserNum);
            return dt;
        }

        public DataTable getUserContactRecord(string strUserNum)
        {
            DataTable dt = dal.getUserContactRecord(strUserNum);
            return dt;
        }

        public void UpdateUserInfoContact1(Foosun.Model.UserInfo2 uc)
        {
            dal.UpdateUserInfoContact1(uc);
        }

        public void UpdateUserInfoContact2(Foosun.Model.UserInfo2 uc)
        {
            dal.UpdateUserInfoContact2(uc);
        }

        public void UpdateUserSafe(int Uid, string PassQuestion, string PassKey, string password)
        {
            dal.UpdateUserSafe(Uid, PassQuestion, PassKey, password);
        }

        public void UpdateUserInfoBaseStat(Foosun.Model.UserInfo3 uc)
        {
            dal.UpdateUserInfoBaseStat(uc);
        }


        public DataTable getUserInfoBaseStat(int Uid)
        {
            DataTable dt = dal.getUserInfoBaseStat(Uid);
            return dt;
        }

        public DataTable getLockStat(int Uid)
        {
            DataTable dt = dal.getLockStat(Uid);
            return dt;
        }

        public DataTable getAdminsStat(int Uid)
        {
            DataTable dt = dal.getAdminsStat(Uid);
            return dt;
        }

        public DataTable getCertsStat(int Uid)
        {
            DataTable dt = dal.getCertsStat(Uid);
            return dt;
        }

        public DataTable getGroupListStat(int Uid)
        {
            DataTable dt = dal.getGroupListStat(Uid);
            return dt;
        }

        public void UpdateUserInfoIDCard(int Uid, string _temp)
        {
            dal.UpdateUserInfoIDCard(Uid, _temp);
        }

        public DataTable userStatlist(int Uid)
        {
            DataTable dt = dal.userStatlist(Uid);
            return dt;
        }

        public DataTable idCardlist(int Uid)
        {
            DataTable dt = dal.idCardlist(Uid);
            return dt;
        }

        public DataTable getleves()
        {
            DataTable dt = dal.getleves();
            return dt;
        }

        public void updateMobile(string _MobileNumber, int BindTF)
        {
            dal.updateMobile(_MobileNumber, BindTF);
        }

        /// <summary>
        /// 得到手机是否捆绑
        /// </summary>
        /// <returns></returns>
        public DataTable getMobileBindTF()
        {
            DataTable dt = dal.getMobileBindTF();
            return dt;
        }

        #endregion 会员列表部分

        #region 会员组

        public void GroupDels(int Gid)
        {
            dal.GroupDels(Gid);
        }

        public DataTable GroupListStr()
        {
            DataTable dt = dal.GroupListStr();
            return dt;
        }

        public DataTable GetGroupRecord(string UserGroupNumber)
        {
            DataTable dt = dal.GetGroupRecord(UserGroupNumber);
            return dt;
        }

        public void InsertGroup(Foosun.Model.UserInfo4 uc)
        {
            dal.InsertGroup(uc);
        }

        public DataTable GetGroupNumber(string GroupNumber)
        {
            DataTable dt = dal.GetGroupNumber(GroupNumber);
            return dt;
        }

        public DataTable getGroupEdit(int Gid)
        {
            DataTable dt = dal.getGroupEdit(Gid);
            return dt;
        }

        public void updateGroupEdit(Foosun.Model.UserInfo4 uc)
        {
            dal.updateGroupEdit(uc);
        }
        #endregion 会员组

        #region 公告开始

        public void Announcedels(string Aid)
        {
            dal.Announcedels(Aid);
        }

        public void AnnounceLockAction(string Aid, string lockstr)
        {
            dal.AnnounceLockAction(Aid, lockstr);
        }

        public void InsertAnnounce(Foosun.Model.UserInfo5 uc)
        {
            dal.InsertAnnounce(uc);
        }

        public void UpdateAnnounce(Foosun.Model.UserInfo5 uc)
        {
            dal.UpdateAnnounce(uc);
        }

        public DataTable getAnnounceEdit(int aid)
        {
            DataTable dt = dal.getAnnounceEdit(aid);
            return dt;
        }


        #endregion 公告结束

        #region 点卡开始

        public void ICarddels(string iId)
        {
            dal.ICarddels(iId);
        }

        public void ICardLockAction(string iId, string lockstr)
        {
            dal.ICardLockAction(iId, lockstr);
        }

        public DataTable GetPage(string _islock, string _isuse, string _isbuy, string _timeout, string _SiteID, string cardnumber, string cardpassword, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(_islock, _isuse, _isbuy, _timeout, _SiteID, cardnumber, cardpassword, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }

        /// <summary>
        /// 判断点卡是否重复
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <returns></returns>
        public DataTable getCardNumberTF(string CardNumber)
        {
            DataTable dt = dal.getCardNumberTF(CardNumber);
            return dt;
        }

        /// <summary>
        /// 判断点卡密码是否重复
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <returns></returns>
        public bool getCardPassTF(string CardPass)
        {
            return dal.getCardPassTF(CardPass);
        }

        public void insertCardR(Foosun.Model.IDCARD uc)
        {
            dal.insertCardR(uc);
        }

        public void UpdateCardR(Foosun.Model.IDCARD uc)
        {
            dal.UpdateCardR(uc);
        }

        public DataTable getCardInfoID(int ID)
        {
            DataTable dt = dal.getCardInfoID(ID);
            return dt;
        }
        /// <summary>
        /// 删除所有点卡 
        /// </summary>
        public void delALLCARD()
        {
            dal.delALLCARD();
        }
        #endregion 点卡结束

        #region 在线支付开始

        public DataTable getOnlinePay()
        {
            DataTable dt = dal.getOnlinePay();
            return dt;
        }

        public void UpdateOnlinePay(Foosun.Model.UserInfo6 uc)
        {
            dal.UpdateOnlinePay(uc);
        }

        public DataTable getmoneylist()
        {
            DataTable dt = dal.getmoneylist();
            return dt;
        }

        #endregion 在线支付结束

        #region 前台会员部分

        public DataTable getUserUserNumRecord(string UserNum)
        {
            DataTable dt = dal.getUserUserNumRecord(UserNum);
            return dt;
        }

        public string getUserGChange(string GroupNumber)
        {
            return dal.getUserGChange(GroupNumber);
        }

        public DataTable getUserUserfields(string UserNum)
        {
            DataTable dt = dal.getUserUserfields(UserNum);
            return dt;
        }


        public DataTable getUserInfobase1_user(string UserNum)
        {
            DataTable dt = dal.getUserInfobase1_user(UserNum);
            return dt;
        }

        public DataTable getUserInfobase2_user(string strUserNum)
        {
            DataTable dt = dal.getUserInfobase2_user(strUserNum);
            return dt;
        }

        public int getPasswordTF(string password)
        {
            return dal.getPasswordTF(password);
        }

        public DataTable getICardTF()
        {
            DataTable dt = dal.getICardTF();
            return dt;
        }

        public void ResetICard()
        {
            dal.ResetICard();
        }

        public void SaveDataICard(string f_IDcardFiles)
        {
            dal.SaveDataICard(f_IDcardFiles);
        }

        /// <summary>
        /// 得到是否管理员
        /// </summary>
        /// <returns></returns>
        public int getisAdmin()
        {
            return dal.getisAdmin();
        }

        #endregion 前台会员部分结束

        public string sel_pic(string PhotoalbumID)
        {
            return dal.sel_pic(PhotoalbumID);
        }
        public int sel_picnum(string PhotoalbumID)
        {
            return dal.sel_picnum(PhotoalbumID);
        }
        #region 投稿

        public DataTable getConstrClass(string UserNum)
        {
            DataTable dt = dal.getConstrClass(UserNum);
            return dt;
        }

        /// <summary>
        /// 获得文章ID内容信息
        /// </summary>
        /// <param name="ConID"></param>
        /// <returns></returns>
        public DataTable getConstrID(string ConID, string UserNum)
        {
            DataTable dt = dal.getConstrID(ConID, UserNum);
            return dt;
        }

        public string getAdminPopandSupper(string UserNum)
        {
            return dal.getAdminPopandSupper(UserNum);
        }

        #endregion 投稿

        //URL
        public void updateURL(string URLName, string URL, string URLColor, string ClassID, string Content, int NUM, int ID)
        {
            dal.updateURL(URLName, URL, URLColor, ClassID, Content, NUM, ID);
        }

        public void updateClass(string ClassName, int NUM, int ID)
        {
            dal.updateClass(ClassName, NUM, ID);
        }

        public DataTable getURL(int ID)
        {
            return dal.getURL(ID);
        }

        public void delURL(int ID)
        {
            dal.delURL(ID);
        }

        public void delclass(int ID)
        {
            dal.delclass(ID);
        }

        public DataTable getClassList(string UserNum)
        {
            return dal.getClassList(UserNum);
        }

        public DataTable getClassURLList(int ClassID)
        {
            return dal.getClassURLList(ClassID);
        }

        public DataTable getClassInfo(int ID)
        {
            return dal.getClassInfo(ID);
        }
        #region API
        public IDataReader GetUserAPiInfo(string UserName)
        {
            return dal.GetUserAPiInfo(UserName);
        }

        #endregion API
        /// <summary>
        /// 获取用户信息实例
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public SysUserInfo GetUserInfo(string username)
        {
            return dal.GetUserInfo(username);
        }
        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public bool CreateUserInfo(SysUserInfo userinfo)
        {
            return  dal.CreateUserInfo(userinfo);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(SysUserInfo userinfo)
        {
            return dal.UpdateUserInfo(userinfo);
        }
    }
}
