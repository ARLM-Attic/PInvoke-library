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
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class Help
    {
        private IHelp dal;
        public Help()
        {
            dal = Foosun.DALFactory.DataAccess.CreateHelp();
        }
        public int Str_CheckSql(string Str_HelpID)
        {
            return dal.Str_CheckSql(Str_HelpID);
        }
        public int Str_InsSql(string Str_HelpID, string Str_CnHelpTitle, string Str_CnHelpContent)
        {
            return dal.Str_InsSql(Str_HelpID, Str_CnHelpTitle, Str_CnHelpContent);
        }

        public int updatehelp(int Str_HelpID, string Str_CnHelpTitle, string Str_CnHelpContent)
        {
            return dal.updatehelp(Str_HelpID, Str_CnHelpTitle, Str_CnHelpContent);
        }

        public int Str_DelSql(int ID)
        {
            return dal.Str_DelSql(ID);
        }
        public DataTable GetPage(string _HelpID,int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(_HelpID,PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        public DataTable getHelpID(int id)
        {
            return dal.getHelpID(id);
        }

        public DataTable getHelpID1(string HelpId)
        {
            return dal.getHelpID1(HelpId);
        }
    }
}