using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    class Discuss
    {

    }
    public struct STDiscuss
    {
        public string DisID;
        public string Cname;
        public string UserNames; 
        public string Authority; 
        public string Authoritymoney;
        public string D_Content;
        public string D_anno;
        public DateTime Creatimes;
        public string ClassID;
        public string Fundwarehouse;
        public int GroupSize;
        public int GroupPerNum;
        public string SiteID;
    }
    public struct STDiscussActive
    {
        public string Activesubject;
        public string ActivePlace;
        public string ActiveExpense;
        public int Anum;
        public string ActivePlan;
        public string Contactmethod;
        public DateTime Cutofftime;
        public DateTime CreaTime;
        public string AId;
        public string UserName;
        public int ALabel;
        public string siteID;
    }

    public struct STADDDiscuss
    {
        public string DtID;
        public string Title;
        public string Content;
        public string UserNum;
        public string ParentID;
        public DateTime creatTime;
        public string DisID;
    }
}