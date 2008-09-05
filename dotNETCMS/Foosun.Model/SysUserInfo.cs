using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{

    public class SysUserInfo
    {
        public SysUserInfo()
        {
            _fields = new SysUserFields(this.UserNum);
        }
        private long _id;
        private string _usernum = string.Empty;
        private string _username = string.Empty;
        private string _userpassword = string.Empty;
        private string _nickname = string.Empty;
        private string _realname = string.Empty;
        private byte _isadmin;
        private string _usergroupnumber = string.Empty;
        private string _passquestion = string.Empty;
        private string _passkey = string.Empty;
        private string _certtype = string.Empty;
        private string _certnumber = string.Empty;
        private string _email = string.Empty;
        private string _mobile = string.Empty;
        private byte _sex;
        private DateTime _birthday = DateTime.Parse("1900-1-1");
        private string _userinfo = string.Empty;
        private string _userface = string.Empty;
        private string _userfacesize = string.Empty;
        private byte _marriage;
        private int _ipoint;
        private int _gpoint;
        private int _cpoint;
        private int _epoint;
        private int _apoint;
        private byte _islock;
        private DateTime _regtime = DateTime.Parse("1900-1-1");
        private DateTime _lastlogintime = DateTime.Parse("1900-1-1");
        private int _onlinetime;
        private int _onlinetf;
        private int _loginnumber;
        private string _friendclass = string.Empty;
        private int _loginlimtnumber;
        private string _lastip = string.Empty;
        private string _siteid = string.Empty;
        private int _addfriend;
        private byte _isopen;
        private int _parmconstrnum;
        private byte _isidcard;
        private string _idcardfiles = string.Empty;
        private byte _addfriendbs;
        private byte _emailatf;
        private string _emailcode = string.Empty;
        private byte _ismobile;
        private byte _bindtf;
        private string _mobilecode = string.Empty;
        SysUserFields _fields  ;
        /// <summary>
        /// 用户扩展信息
        /// </summary>
        public SysUserFields Fields
        {
            set { _fields = value; }
            get { return _fields; }
        }
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string UserNum
        {
            set { _usernum = value;
            _fields.userNum = _usernum;
            }
            get { return _usernum; }
        }
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        public string UserPassword
        {
            set { _userpassword = value; }
            get { return _userpassword; }
        }
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        public byte isAdmin
        {
            set { _isadmin = value; }
            get { return _isadmin; }
        }
        public string UserGroupNumber
        {
            set { _usergroupnumber = value; }
            get { return _usergroupnumber; }
        }
        public string PassQuestion
        {
            set { _passquestion = value; }
            get { return _passquestion; }
        }
        public string PassKey
        {
            set { _passkey = value; }
            get { return _passkey; }
        }
        public string CertType
        {
            set { _certtype = value; }
            get { return _certtype; }
        }
        public string CertNumber
        {
            set { _certnumber = value; }
            get { return _certnumber; }
        }
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        public byte Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        public DateTime birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        public string Userinfo
        {
            set { _userinfo = value; }
            get { return _userinfo; }
        }
        public string UserFace
        {
            set { _userface = value; }
            get { return _userface; }
        }
        public string userFacesize
        {
            set { _userfacesize = value; }
            get { return _userfacesize; }
        }
        public byte marriage
        {
            set { _marriage = value; }
            get { return _marriage; }
        }
        public int iPoint
        {
            set { _ipoint = value; }
            get { return _ipoint; }
        }
        public int gPoint
        {
            set { _gpoint = value; }
            get { return _gpoint; }
        }
        public int cPoint
        {
            set { _cpoint = value; }
            get { return _cpoint; }
        }
        public int ePoint
        {
            set { _epoint = value; }
            get { return _epoint; }
        }
        public int aPoint
        {
            set { _apoint = value; }
            get { return _apoint; }
        }
        public byte isLock
        {
            set { _islock = value; }
            get { return _islock; }
        }
        public DateTime RegTime
        {
            set { _regtime = value; }
            get { return _regtime; }
        }
        public DateTime LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        public int OnlineTime
        {
            set { _onlinetime = value; }
            get { return _onlinetime; }
        }
        public int OnlineTF
        {
            set { _onlinetf = value; }
            get { return _onlinetf; }
        }
        public int LoginNumber
        {
            set { _loginnumber = value; }
            get { return _loginnumber; }
        }
        public string FriendClass
        {
            set { _friendclass = value; }
            get { return _friendclass; }
        }
        public int LoginLimtNumber
        {
            set { _loginlimtnumber = value; }
            get { return _loginlimtnumber; }
        }
        public string LastIP
        {
            set { _lastip = value; }
            get { return _lastip; }
        }
        public string SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }
        public int Addfriend
        {
            set { _addfriend = value; }
            get { return _addfriend; }
        }
        public byte isOpen
        {
            set { _isopen = value; }
            get { return _isopen; }
        }
        public int ParmConstrNum
        {
            set { _parmconstrnum = value; }
            get { return _parmconstrnum; }
        }
        public byte isIDcard
        {
            set { _isidcard = value; }
            get { return _isidcard; }
        }
        public string IDcardFiles
        {
            set { _idcardfiles = value; }
            get { return _idcardfiles; }
        }
        public byte Addfriendbs
        {
            set { _addfriendbs = value; }
            get { return _addfriendbs; }
        }
        public byte EmailATF
        {
            set { _emailatf = value; }
            get { return _emailatf; }
        }
        public string EmailCode
        {
            set { _emailcode = value; }
            get { return _emailcode; }
        }
        public byte isMobile
        {
            set { _ismobile = value; }
            get { return _ismobile; }
        }
        public byte BindTF
        {
            set { _bindtf = value; }
            get { return _bindtf; }
        }
        public string MobileCode
        {
            set { _mobilecode = value; }
            get { return _mobilecode; }
        }
    }

}
