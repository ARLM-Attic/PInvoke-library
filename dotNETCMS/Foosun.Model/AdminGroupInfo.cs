using System;
using System.Collections.Generic;

namespace Foosun.Model
{
    [Serializable]
    public class AdminGroupInfo
    {
        public int Id;
        public string adminGroupNumber;
        public string GroupName;
        public string ClassList;
        public string SpecialList;
        public string channelList;
        public DateTime CreatTime;
        public string SiteID;
    }
}
