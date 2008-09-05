using System;
using System.Collections.Generic;

namespace Foosun.Model
{
    [Serializable]
    public class AdminInfo
    {
        public string UserNum;
        public string UserName;
        public string UserPassword;
        public string RealName;
        public int isAdmin;
        public string Email;
        public DateTime RegTime;
        public string SiteID;
        public int LoginNumber;
        public int OnlineTF;
        public int OnlineTime;
        public int isLock;
        public int aPoint;
        public int ePoint;
        public int cPoint;
        public int gPoint;
        public int iPoint;
        public string UserGroupNumber;

        public int isSuper;
        public string adminGroupNumber;
        public int OnlyLogin;
        public int isChannel;
        public int isChSupper;
        public string Iplimited;
    }
}
