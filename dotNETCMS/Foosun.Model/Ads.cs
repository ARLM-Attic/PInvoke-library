using System;
using System.Collections.Generic;

namespace Foosun.Model
{
    [Serializable]
    public class AdsListInfo
    {
        public string type;
        public string showSiteID;
        public string showAdsType;
        public string adsType;
        public string searchType;
        public string SearchKey;
    }
    public class AdsClassInfo
    {
        public string AcID;
        public string Cname;
        public string ParentID;
        public DateTime creatTime;
        public string SiteID;
        public int Adprice;
    }

    public class AdsInfo
    {
        public string AdID;
        public string adName;
        public string ClassID;
        public string CusID;
        public int adType;
        public string leftPic;
        public string rightPic;
        public string leftSize;
        public string rightSize;
        public string LinkURL;
        public int CycTF;
        public string CycAdID;
        public int CycSpeed;
        public int CycDic;
        public int ClickNum;
        public int ShowNum;
        public int CondiTF;
        public int maxShowClick;
        public DateTime TimeOutDay;
        public int maxClick;
        public DateTime creatTime;
        public int AdTxtNum;
        public int isLock;
        public string SiteID;
        public string AdTxtContent;
        public string AdTxtCss;
        public string AdTxtLink;
        public string OldClass;
    }
}
