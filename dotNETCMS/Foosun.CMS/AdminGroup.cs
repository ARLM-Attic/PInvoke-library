//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Xi.Deng                         ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Data;
using Foosun.Model;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class AdminGroup
    {
        private IAdminGroup agc;
        public AdminGroup()
        {
            agc = DataAccess.CreateAdminGroup();
        }

        public int add(Foosun.Model.AdminGroupInfo agci)
        {
            int result = agc.add(agci);
            return result;
        }

        public int Edit(Foosun.Model.AdminGroupInfo agci)
        {
            int result = agc.Edit(agci);
            return result;
        }

        public void Del(string id)
        {
            agc.Del(id);
        }

        public DataTable getInfo(string id)
        {
            DataTable dt = agc.getInfo(id);
            return dt;
        }

        public DataTable getClassList(string col, string TbName, string sqlselect)
        {
            DataTable dt = agc.getClassList(col, TbName, sqlselect);
            return dt;
        }

        public DataTable getColCname(string colname, string TbName, string classid, string id)
        {
            DataTable dt = agc.getColCname(colname, TbName, classid, id);
            return dt;
        }
    }
}
