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

namespace Foosun.CMS.Style
{
    public class Style
    {
        private IStyle stClass;
        public Style()
        {
            stClass = DataAccess.CreateStyle();
        }
        public int sytleClassAdd(Foosun.Model.StyleClassInfo sc)
        {
            int result = stClass.sytleClassAdd(sc);
            return result;
        }
        public int styleClassEdit(Foosun.Model.StyleClassInfo sc)
        {
            int result = stClass.styleClassEdit(sc);
            return result;
        }
        public void styleClassDel(string id)
        {
            stClass.styleClassDel(id);
        }
        public void styleClassRDel(string id)
        {
            stClass.styleClassRDel(id);
        }
        public int styleAdd(Foosun.Model.StyleInfo sc)
        {
            int result = stClass.styleAdd(sc);
            return result;
        }

        public int styleNametf(string CName)
        {
            return stClass.styleNametf(CName);
        }

        public int styleEdit(Foosun.Model.StyleInfo sc)
        {
            int result = stClass.styleEdit(sc);
            return result;
        }
        public void styleDel(string id)
        {
            stClass.styleDel(id);
        }
        public void styleRdel(string id)
        {
            stClass.styleRdel(id);
        }
        public DataTable getstyleClassInfo(string id)
        {
            DataTable dt = stClass.getstyleClassInfo(id);
            return dt;
        }
        public DataTable getstyleInfo(string id)
        {
            DataTable dt = stClass.getstyleInfo(id);
            return dt;
        }
        public DataTable styledefine()
        {
            DataTable dt = stClass.styledefine();
            return dt;
        }
        public DataTable styleClassList()
        {
            DataTable dt = stClass.styleClassList();
            return dt;
        }
    }
}
