///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    class Constr
    {

    }

    public struct STConstr
    {
        public string ClassID;
        public string Title;
        public string Content;
        public string Source;
        public string Tags;
        public string Contrflg;
        public string Author;
        public string UserNum;
        public string PicURL;
        public string SiteID;
    }

    public struct STConstrClass
    {
        public string cName;
        public string Content;
    }

    public struct STConstrPay
    {
        public int Id;
        public string UCid;
        public string UserNum;
        public string cName;
        public string creatTime;
        public string Content;
     }
    public struct STuserother
    {
        public string address;
        public string postcode;
        public string RealName;
        public string bankName;
        public string bankaccount;
        public string bankcard;
        public string bankRealName;
    }
}