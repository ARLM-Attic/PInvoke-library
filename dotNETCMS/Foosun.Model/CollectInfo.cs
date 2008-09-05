using System;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    public class CollectSiteInfo
    {
        public int ID;
        public string SiteName = "";
        public string objURL = "";
        public string Encode = "";
        public string OtherPara = "";
        public int MaxNum;
        public string ListSetting = "";
        public string LinkSetting = "";
        public int OtherType;
        public string OtherPageSetting = "";
        public int StartPageNum;
        public int EndPageNum;
        public string PagebodySetting = "";
        public string PageTitleSetting = "";
        public int OtherNewsType;
        public string OtherNewsPageSetting = "";
        public string AuthorSetting = "";
        public string SourceSetting = "";
        public string AddDateSetting = "";
        public bool IsAutoCollect;
        public int CollectDate;
        public bool TextTF;
        public bool SaveRemotePic;
        public int Audit;
        public bool IsStyle;
        public bool IsDIV;
        public bool IsA;
        public bool IsClass;
        public bool IsFont;
        public bool IsSpan;
        public bool IsObject;
        public bool IsIFrame;
        public bool IsScript;
        public bool IsReverse;
        public bool IsAutoPicNews;
        public string HandSetAuthor = "";
        public string HandSetSource = "";
        public DateTime HandSetAddDate;
        public int Folder;
        public string ClassID;
    }
    [Serializable]
    public class CollectNewsInfo
    {
        public string Title = "";
        public string Links = "";
        public string Author = "";
        public string Source = "";
        public string Content = "";
        public DateTime AddDate;
        public int ImagesCount;
        public int SiteID;
        public string ClassID = "";
        public string ChannelID = "";
        public bool History;
        public bool ReviewTF;
        public bool IsLock;
        public DateTime CollectTime;
    }
}
