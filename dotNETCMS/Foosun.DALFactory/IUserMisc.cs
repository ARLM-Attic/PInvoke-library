using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;
using Foosun.Model;


namespace Foosun.DALFactory
{
    public interface IUserMisc
    {
        DataTable getSiteList();
        IDataReader Navilist(string UserNum);
        IDataReader aplist(string UserNum);
        DataTable calendar(string UserNum);
        DataTable messageChar(string UserNum);
        IDataReader ShortcutList(string UserNum,int _Num);
        IDataReader menuNavilist(string type, string UserNum);
        DataTable getUserInfobase1(int Uid);
        DataTable getUserInfobase1_user(string UserNum);
        DataTable getUserInfobase2_user(string UserNum);
        DataTable getUserInfobase2(string strUserNum);
        DataTable sexlist(int Uid);
        DataTable marriagelist(int Uid);
        DataTable isopenlist(int Uid);
        DataTable getUserInfoParam(int Uid);
        DataTable getUserInfoNum(int Uid);
        DataTable getUserInfoRecord(string UserNum);
        DataTable getUserInfoContact(string UserNum);
        DataTable getUserContactRecord(string UserNum);
        DataTable getPassWord(int Uid);
        DataTable getUserInfoBaseStat(int Uid);
        DataTable getLockStat(int Uid);
        DataTable getAdminsStat(int Uid);
        DataTable getCertsStat(int Uid);
        DataTable getGroupListStat(int Uid);
        DataTable userStatlist(int Uid);
        DataTable idCardlist(int Uid);
        DataTable GroupListStr();
        DataTable GetGroupNumber(string UserGroupNumber);
        DataTable GetGroupRecord(string UserGroupNumber);
        DataTable getGroupEdit(int Gid);
        DataTable getAnnounceEdit(int aid);
        DataTable getOnlinePay();
        DataTable ManagemenuNavilist();
        DataTable ManagechildmenuNavilist(string pID);
        DataTable getManageChildNaviRecord(string am_ClassID);
        DataTable navimenusub(string _str);
        DataTable GetNaviEditID(int nID);
        DataTable Getparentidlist();
        DataTable Getchildparentidlist(string pID);
        IDataReader QShortcutList(int _num);
        DataTable QeditAction(int QID);
        DataTable QGetRecord(int num);
        DataTable QGetNumberRecord(string StrNumber);
        DataTable getUserUserNumRecord(string UserNum);
        DataTable getUserUserfields(string UserNum);
        DataTable getICardTF();
        DataTable getCardNumberTF(string CardNumber);
        bool getCardPassTF(string CardPass);
        DataTable getCardInfoID(int ID);
        DataTable getmoneylist();
        DataTable getleves();
        void insertCardR(Foosun.Model.IDCARD uc);
        void UpdateCardR(Foosun.Model.IDCARD uc);
        void delALLCARD();
        string getUserGChange(string GroupNumber);
        void Announcedels(string Aid);
        void UpdateUserInfoBase(Foosun.Model.UserInfo uc);
        void UpdateUserInfoBase1(Foosun.Model.UserInfo1 uc1);
        void UpdateUserInfoBase2(Foosun.Model.UserInfo1 uc2);
        void UpdateUserInfoContact1(Foosun.Model.UserInfo2 uc);
        void UpdateUserInfoContact2(Foosun.Model.UserInfo2 uc);
        void UpdateUserInfoBaseStat(Foosun.Model.UserInfo3 uc);
        void InsertGroup(Foosun.Model.UserInfo4 uc);
        void updateGroupEdit(Foosun.Model.UserInfo4 uc);
        void InsertAnnounce(Foosun.Model.UserInfo5 uc);
        void UpdateAnnounce(Foosun.Model.UserInfo5 uc);
        void UpdateOnlinePay(Foosun.Model.UserInfo6 uc);
        void InsertManageMenu(Foosun.Model.UserInfo7 uc);
        void EditManageMenu(Foosun.Model.UserInfo7 uc);
        void EditManageMenu1(Foosun.Model.UserInfo7 uc);
        void InsertQMenu(Foosun.Model.UserInfo8 uc);
        void UpdateQMenu(Foosun.Model.UserInfo8 uc);
        void UpdateUserSafe(int Uid, string PassQuestion, string PassKey, string password);
        void UpdateUserInfoIDCard(int Uid, string _temp);
        void GroupDels(int Gid);
        void AnnounceLockAction(string Aid, string lockstr);
        void ICarddels(string iId);
        void ICardLockAction(string iId, string lockstr);
        DataTable GetPage(string _islock, string _isuse, string _isbuy, string _timeout, string _SiteID,string cardnumber,string cardpassword, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        void Shortcutdel(int Qid);
        void Shortcutde2(string ClassID);
        void QShortcutdel(int QID, int _num);
        void ResetICard();
        void SaveDataICard(string f_IDcardFiles);
        int getPasswordTF(string password);
        void updateMobile(string _MobileNumber, int BindTF);
        DataTable getMobileBindTF();
        string sel_pic(string PhotoalbumID);

        int sel_picnum(string PhotoalbumID);
        DataTable getConstrClass(string UserNum);
        DataTable getConstrID(string ConID, string UserNum);
        //得到是否是管理员
        int getisAdmin();
        string getAdminPopandSupper(string UserNum);
        void updateURL(string URLName, string URL, string URLColor, string ClassID, string Content, int NUM, int ID);
        void updateClass(string ClassName, int NUM, int ID);
        DataTable getURL(int ID);
        void delURL(int ID);
        void delclass(int ID);
        DataTable getClassList(string UserNum);
        DataTable getClassURLList(int ClassID);
        DataTable getClassInfo(int ID);
        string GetUserLogs(int ID);
        #region API
        IDataReader GetUserAPiInfo(string UserName);
        #endregion API

        SysUserInfo GetUserInfo(string username);
        bool CreateUserInfo(SysUserInfo userinfo);
        bool UpdateUserInfo(SysUserInfo userinfo);
         

    }

    public sealed partial class DataAccess
    {
        public static IUserMisc CreateUserMisc()
        {
            string className = path + ".UserMisc";
            return (IUserMisc)Assembly.Load(path).CreateInstance(className);
        }
    }
}
