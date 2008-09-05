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
    public class Mycom
    {
        private IMycom dal;
        public Mycom()
        {
            dal = Foosun.DALFactory.DataAccess.CreateMycom();
        }
        #region usermycom_Look.aspx
        public DataTable sel(string Commid)
        {
            return dal.sel(Commid);
        }
        public int Update(string Title, string Contents, DateTime CreatTime, string Commid)
        {
            return dal.Update(Title, Contents, CreatTime, Commid);
        }
        public DataTable GetPage(string UserNum2, string GoodTitle2, string UserNum, string title, string Um, string dtm1, string dtm2, string isCheck, string islock, string SiteID, string InfoID, string APIID, string DTable, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(UserNum2, GoodTitle2, UserNum, title, Um, dtm1, dtm2, isCheck, islock, SiteID,InfoID,APIID,DTable, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        //public DataTable sel_1()
        //{
        //    return dal.sel_1();
        //}
        public string sel_2(string InfoID,string datalib)
        {
            return dal.sel_2(InfoID, datalib);
        }
        public string sel_3(string UserNum)
        {
            return dal.sel_3(UserNum);
        }
        public DataTable sel_4(string GroupNumber)
        {
            return dal.sel_4(GroupNumber);
        }
        public void Delete(string Commid)
        {
            dal.Delete(Commid);
        }
        public string sel_5(string Commid)
        {
            return dal.sel_5(Commid);
        }
        public int Update_1(int OrderID, string Commid)
        {
            return dal.Update_1(OrderID,Commid);
        }
        public string sel_6(string Commid)
        {
            return dal.sel_6(Commid);
        }
        public void Update_2(int GoodTitle, string Commid)
        {
            dal.Update_2(GoodTitle, Commid);
        }
        public string sel_7(string Commid)
        {
            return dal.sel_7(Commid);
        }
        public int Update_3(string Commid,int ch)
        {
            return dal.Update_3(Commid, ch);
        }
        public string sel_8(string Commid)
        {
            return dal.sel_8(Commid);
        }
        public int Update_4(int islock, string Commid)
        {
            return dal.Update_4(islock, Commid);
        }
        public string sel_9(string UserName)
        {
            return dal.sel_9(UserName);
        }
        #endregion
    }
}