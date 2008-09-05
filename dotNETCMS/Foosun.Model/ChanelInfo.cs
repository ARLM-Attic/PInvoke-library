using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    /*public struct STSearchCondition
    {
        public string name;
        public object value;

    }*/
    public struct STSite
    {
        public string ChannelID;
        public string CName;
        public string EName;
        public string ChannCName;
        public int IsURL;
        public string Urladdress;
        public string DataLib;
        public string IndexTemplet;
        public string ClassTemplet;
        public string ReadNewsTemplet;
        public string SpecialTemplet;
        public int isLock;
        public string Domain;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber;
        public int isCheck;
        public string Keywords;
        public string Descript;
        public int ContrTF;
        public int ShowNaviTF;
        public string UpfileType;
        public int UpfileSize;
        public string NaviContent;
        public string NaviPicURL;
        public int SaveType;
        public string PicSavePath;
        public int SaveFileType;
        public string SaveDirPath;
        public string SaveDirRule;
        public string SaveFileRule;
        public string NaviPosition;
        public string IndexEXName;
        public string ClassEXName;
        public string NewsEXName;
        public string SpecialEXName;
        public int classRefeshNum;
        public int infoRefeshNum;
        public int DelNum;
        public int SpecialNum;
    }
}
