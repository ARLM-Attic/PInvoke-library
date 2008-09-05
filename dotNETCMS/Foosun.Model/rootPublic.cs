using System;
using System.Collections.Generic;

namespace Foosun.Model
{
    [Serializable]
    public class sys_User
    {
        public DateTime RegTime;
        public string UserNum;
        public string UserName;
        public string UserPassword;
        public int isAdmin;
        public int LoginNumber;
        public int OnlineTF;
        public int OnlineTime;
        public int isLock;
        public int iPoint;
        public int gPoint;
        public int cPoint;
        public int ePoint;
        public int aPoint;
        public string UserGroupNumber;
        public string NickName;
        public string RealName;
        public string PassQuestion;
        public string PassKey;
        public string CertType;
        public string CertNumber;
        public string Email;
        public string Mobile;
        public string SiteID;
        public int isIDcard;
        public int EmailATF;
        public string EmailCode;
        public int isMobile;
        public string MobileCode;
    }

    public class sys_userfields
    {
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
}