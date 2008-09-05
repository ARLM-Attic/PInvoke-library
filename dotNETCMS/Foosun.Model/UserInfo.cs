using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    public enum EnumUserType
    {
        Admin = 0,
        Person = 1
    }
    [Serializable]

    public class UserInfo
    {
        public int Id;
        public string NickName;
        public string RealName;
        public int sex;
        public DateTime birthday;
        public string Userinfo;
        public string UserFace;
        public string userFacesize;
        public int marriage;
        public int isopen;
        public string UserGroupNumber;
        public string email;

    }

    //点卡
    public class IDCARD
    {
        public int Id;
        public string CaID;
        public string CardNumber;
        public string CardPassWord;
        public DateTime creatTime;
        public int Money;
        public int Point;
        public int isBuy;
        public int isUse;
        public int isLock;
        public string UserNum;
        public string siteID;
        public DateTime TimeOutDate;
    }

    //会员基本信息构造函数
    public class UserInfo1
    {
        public string UserNum;
        public string Nation;
        public string nativeplace;
        public string character;
        public string UserFan;
        public string orgSch;
        public string job;
        public string education;
        public string Lastschool;
    }
    //会员联系方式构造函数
    public class UserInfo2
    {
        public string UserNum;
        public string province;
        public string City;
        public string Address;
        public string Postcode;
        public string FaTel;
        public string WorkTel;
        public string Fax;
        public string QQ;
        public string MSN;
    }
    //会员状态构造函数
    public class UserInfo3
    {
        public int Id;
        public string UserNum;
        public string UserGroupNumber;
        public int islock;
        public int isadmin;
        public string CertType;
        public string CertNumber;
        public int ipoint;
        public int gpoint;
        public int cpoint;
        public int epoint;
        public int apoint;
        public int onlineTime;
        public DateTime RegTime;
        public DateTime LastLoginTime;
        public int LoginNumber;
        public int LoginLimtNumber;
        public string lastIP;
        public string SiteID;
    }
    //构造会员组插入数据
    public class UserInfo4
    {
        public int gID;
        public string GroupNumber;
        public string GroupName;
        public int iPoint;
        public int Gpoint;
        public int Rtime;
        public int LenCommContent;
        public int CommCheckTF;
        public int PostCommTime;
        public string upfileType;
        public int upfileNum;
        public int upfileSize;
        public int DayUpfilenum;
        public int ContrNum;
        public int DicussTF;
        public int PostTitle;
        public int ReadUser;
        public int MessageNum;
        public string MessageGroupNum;
        public int IsCert;
        public int CharTF;
        public int CharHTML;
        public int CharLenContent;
        public int RegMinute;
        public int PostTitleHTML;
        public int DelSelfTitle;
        public int DelOTitle;
        public int EditSelfTitle;
        public int EditOtitle;
        public int ReadTitle;
        public int MoveSelfTitle;
        public int MoveOTitle;
        public int TopTitle;
        public int GoodTitle;
        public int LockUser;
        public string UserFlag;
        public int CheckTtile;
        public int IPTF;
        public int EncUser;
        public int OCTF;
        public int StyleTF;
        public int UpfaceSize;
        public string GIChange;
        public string GTChageRate;
        public string LoginPoint;
        public string RegPoint;
        public int GroupTF;
        public int GroupSize;
        public int GroupPerNum;
        public int GroupCreatNum;
        public DateTime CreatTime;
        public string SiteID;
        public double Discount;
    }
    //公告构造函数
    public class UserInfo5
    {
        public int Id;
        public string newsID;
        public string Title;
        public string content;
        public DateTime creatTime;
        public string GroupNumber;
        public string getPoint;
        public string SiteId;
        public int isLock;
    }
    //在线支付构造函数
    public class UserInfo6
    {
        public int onpayType;
        public string O_userName;
        public string O_key;
        public string O_sendurl;
        public string O_returnurl;
        public string O_md5;
        public string O_other1;
        public string O_other2;
        public string O_other3;
        public int Id;
    }

    //菜单构造函数
    public class UserInfo7
    {
        public string api_IdentID;
        public string am_ClassID;
        public string Am_position;
        public string am_Name;
        public string am_FilePath;
        public string am_target;
        public string am_ParentID;
        public int am_type;
        public DateTime am_creatTime;
        public int am_orderID;
        public int isSys;
        public string siteID;
        public string userNum;
        public int am_ID;
        public string popCode;
    }
    
    //快捷方式构造函数
    public class UserInfo8
    {
        public string QmID;
        public string qName;
        public string FilePath;
        public int Ismanage;
        public int OrderID;
        public string usernum;
        public string SiteID;
        public int Id;
    }
}
