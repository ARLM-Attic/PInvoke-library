using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    public class ChannelInfo
    {
        public int Id;
        public int channelType;
        public string channelName;
        public string binddomain;
        public string channelItem;
        public string channelEItem;
        public int isConstr;
        public string ClassSave;
        public string ClassFileName;
        public string SavePath;
        public string FileName;
        public string channelDescript;
        public string DataLib;
        public int islock;
        public string channelunit;
        public string htmldir;
        public string indexFileName;
        public int issys;
        public int isHTML;
        public int upfilessize;
        public string upfiletype;
        public int ischeck;
        public string TempletPath;
        public string indextemplet;
        public string classtemplet;
        public string newstemplet;
        public string specialtemplet;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber;
    }

    public class ChannelValue
    {
        public int Id;
        public int ChID;
        public int OrderID;
        public string CName;
        public string EName;
        public string vDescript;
        public int vType;
        public string vLength;
        public string vValue;
        public int isNulls;
        public int isUser;
        public string vitem;
        public int isLock;
        public string SiteID;
        public string fieldLength;
        public int isSearch;
        public int HTMLedit;
        public string vHeight;
    }

    public class ChannelClassInfo
    {
        public int Id;
        public int ParentID;
        public int ChID;
        public string classCName;
        public string classEName;
        public int OrderID;
        public int isPage;
        public string PageContent;
        public string Templet;
        public string ContentTemplet;
        public string SavePath;
        public string FileName;
        public string ContentSavePath;
        public string ContentFileNameRule;
        public int isShowNavi;
        public string NaviContent;
        public string KeyMeta;
        public string DescMeta;
        public string PicURL;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber;
        public int isLock;
        public string ClassNavi;
        public string ContentNavi;
        public string SiteID;
    }
    public class ChannelSpecialInfo
    {
        public int Id;
        public int ParentID;
        public int ChID;
        public int OrderID;
        public string specialCName;
        public string specialEName;
        public string binddomain;
        public string navicontent;
        public string savePath;
        public string filename;
        public string templet;
        public int islock;
        public int isRec;
        public string PicURL;
    }

    public class styleChContent
    {
        public int Id;
        public int ChID;
        public int classID;
        public string styleName;
        public string styleContent;
        public int isLock;
        public string styleDescript;
        public string SiteID;
        public DateTime creattime;
    }
    public class LabelChContent
    {
        public int Id;
        public int ChID;
        public int ClassID;
        public string LabelName;
        public string LabelContent;
        public int isLock;
        public string LabelDescript;
        public string SiteID;
        public DateTime CreatTime;
    }

    public class ChInfoContent
    {
        public int Id;
        public int ChID;
        public int ClassID;
        public string SpecialID;
        public string title;
        public string TitleColor;
        public int TitleITF;
        public int TitleBTF;
        public string PicURL;
        public string NaviContent;
        public string Content;
        public string Author;
        public string Souce;
        public int OrderID;
        public string Tags;
        public string Templet;
        public string SavePath;
        public string FileName;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber;
        public string Metakeywords;
        public string Metadesc;
        public int Click;
        public int isHTML;
        public int isConstr;
        public int islock;
        public string Editor;
        public string ContentProperty;
    }
}
