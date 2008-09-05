using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    public class stat_Param
    {
        public string SystemName;
        public string SystemNameE;
        public int ipCheck;
        public int isOnlinestat;
        public int ipTime;
        public int pageNum;
        public string cookies;
        public int pointNum;
        public string SiteID;
    }
    public class stat_Info
    {
        public int vyear;
        public int vmonth;
        public int vday;
        public int vhour;
        public string vtime;
        public int vweek;
        public string vip;
        public string vwhere;
        public string vwheref;
        public string vcome;
        public string vpage;
        public string vsoft;
        public string vOS;
        public int vwidth;
        public string classid;
        public string SiteID;
    }
}