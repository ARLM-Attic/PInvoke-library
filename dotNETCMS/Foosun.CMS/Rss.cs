//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By ZhenJiang.Wang                  ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class Rss
    {
        private IRss dal;
        public Rss()
        {
            dal = Foosun.DALFactory.DataAccess.CreateRss();
        }
        public int sel(string ClassID)
        {
            return dal.sel(ClassID);
        }

        public DataTable getxmllist(string ClassID)
        {
            DataTable dt = dal.getxmllist(ClassID);
            return dt;
        }

    }
}
