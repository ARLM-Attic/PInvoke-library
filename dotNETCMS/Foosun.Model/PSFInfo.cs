using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    public class PSF
    {
        public int Id;
        public string psfID;
        public string psfName;
        public string LocalDir;
        public string RemoteDir;
        public int isAll;
        public int isSub;
        public DateTime CreatTime;
        public int isRecyle;
        public string SiteID;
    }

    public class Task
    {
        public int Id;
        public string taskID;
        public string TaskName;
        public int isIndex;
        public string ClassID;
        public string News;
        public string Special;
        public string TimeSet;
        public DateTime CreatTime;
        public string SiteID;
    }
}
