//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Simplt.Xie                      ==
//===========================================================
using System;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class UserList
    {
        private IUserList dal;
        public UserList()
        {
            dal = Foosun.DALFactory.DataAccess.CreateUserList();
        }

        public string getGroupName(string strGroupNumber)
        {
            return dal.getGroupName(strGroupNumber);
        }

        public int singdel(int id)
        {
            return dal.singdel(id);
        }

        public int dels(string id)
        {
            return dal.dels(id);
        }

        public int isLock(string id)
        {
            return dal.isLock(id);
        }

        public int unLock(string id)
        {
            return dal.unLock(id);
        }

        public DataTable GroupList()
        {
            DataTable dt = dal.GroupList();
            return dt;
        }

        public int bIpoint(string uid, int sPoint)
        {
            return dal.bIpoint(uid, sPoint);
        }

        public int sIpoint(string uid, int sPoint)
        {
            return dal.sIpoint(uid, sPoint);
        }

        public int bGpoint(string uid, int sPoint)
        {
            return dal.bGpoint(uid, sPoint);
        }

        public int sGpoint(string uid, int sPoint)
        {
            return dal.sGpoint(uid, sPoint);
        }

        public DataTable GetPage(string UserName, string RealName, string UserNum, string Sex,  string siPoint, string biPoint, string sgPoint,string bgPoint,string _userlock,string _group,string _iscerts,string _SiteID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(UserName, RealName, UserNum, Sex, siPoint, biPoint, sgPoint, bgPoint, _userlock, _group, _iscerts, _SiteID, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }

    }
}
