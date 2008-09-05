using System;
using System.Collections.Generic;

namespace Foosun.Model
{
    [Serializable]
    public class Comment
    {
        public int Id;
        public string Commid;
        public string InfoID;
        public string APIID;
        public string DataLib;
        public string Title;
        public string Content;
        public DateTime creatTime;
        public string IP;
        public string QID;
        public string UserNum;
        public int isRecyle;
        public int islock;
        public int OrderID;
        public int GoodTitle;
        public int isCheck;
        public string SiteID;
        public int commtype;
        public int ChID;
    }
}
