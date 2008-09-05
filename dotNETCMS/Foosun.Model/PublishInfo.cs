using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    /// <summary>
    /// 新闻栏目常用信息类
    /// </summary>
    public class PubClassInfo
    {
        public int Id;
        public string ClassID = string.Empty;
        public string ClassCName = string.Empty;
        public string ClassEName = string.Empty;
        public string ParentID = string.Empty;
        public int IsURL;
        //public int OrderID;
        public string URLaddress = string.Empty;
        public string Domain = string.Empty;
        public string ClassTemplet = string.Empty;
        //public string ReadNewsTemplet = string.Empty;
        public string SavePath = string.Empty;
        public string SaveClassframe = string.Empty;
        //public int Checkint;
        public string ClassSaveRule = string.Empty;
        public string ClassIndexRule = string.Empty;
        //public string NewsSavePath = string.Empty;
        //public string NewsFileRule = string.Empty;
        //public string PicDirPath = string.Empty;
        //public int ContentPicTF;
        //public string ContentPICurl = string.Empty;
        //public string ContentPicSize = string.Empty;
        //public int InHitoryDay;
        //public string DataLib = string.Empty;
        public string SiteID = string.Empty;
        public int NaviShowtf;
        public string NaviPIC = string.Empty;
        public string NaviContent = string.Empty;
        public string MetaKeywords = string.Empty;
        public string MetaDescript = string.Empty;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber = string.Empty;
        //public string FileName = string.Empty;
        //public int isLock;
        //public int isRecyle;
        public string NaviPosition = string.Empty;
        public string NewsPosition = string.Empty;
        //public int isComm;
        //public string Defineworkey = string.Empty;
        //public DateTime CreatTime;
        public int isPage;
        public string PageContent = string.Empty;
        //public string ModelID = string.Empty;
        //public int isunHTML;
    }

    /// <summary>
    /// 频道栏目常用类
    /// </summary>
    public class PubCHClassInfo
    {
        public int Id;
        public string classCName = string.Empty;
        public string classEName = string.Empty;
        public int ParentID;
        public int isPage;
        public int OrderID;
        public string Templet = string.Empty;
        public string SavePath = string.Empty;
        public string FileName = string.Empty;
        public int ChID;
        public string PicURL = string.Empty;
        public string NaviContent = string.Empty;
        public string MetaKeywords = string.Empty;
        public string MetaDescript = string.Empty;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber = string.Empty;
        public string ClassNavi = string.Empty;
        public string ContentNavi = string.Empty;
        public string PageContent = string.Empty;
    }

    /// <summary>
    /// 专题常用信息类
    /// </summary>
    public class PubSpecialInfo
    {
        public int Id;
        public string SpecialID = string.Empty;
        public string SpecialCName = string.Empty;
        public string specialEName = string.Empty;
        public string ParentID = string.Empty;
        //public string Domain = string.Empty;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber = string.Empty;
        public string saveDirPath = string.Empty;
        public string SavePath = string.Empty;
        public string FileName = string.Empty;
        public string FileEXName = string.Empty;
        public string NaviPicURL = string.Empty;
        public string NaviContent = string.Empty;
        public string SiteID = string.Empty;
        public string Templet = string.Empty;
        //public int isLock;
        //public int isRecyle;
        //public DateTime CreatTime;
        public string NaviPosition = string.Empty;
        //public string ModelID = string.Empty;
    }

    /// <summary>
    /// 频道专题常用信息类
    /// </summary>
    public class PubCHSpecialInfo
    {
        public int Id;
        public int ChID;
        public int ParentID;
        public int islock;
        public int isRec;
        public int OrderID;
        public string specialCName = string.Empty;
        public string specialEName = string.Empty;
        public string binddomain = string.Empty;
        public string savePath = string.Empty;
        public string filename = string.Empty;
        public string PicURL = string.Empty;
        public string navicontent = string.Empty;
        public string templet = string.Empty;
        //public string NaviPosition = string.Empty;
        public string SiteID = string.Empty;
    }
}
