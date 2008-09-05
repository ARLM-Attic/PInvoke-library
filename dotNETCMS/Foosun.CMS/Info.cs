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
    public class Info
    {
        private IInfo dal;
        public Info()
        {
            dal = Foosun.DALFactory.DataAccess.CreateInfo();
        }
        #region announce.aspx
        public string sel_1(string UserNum)
        {
            return dal.sel_1(UserNum);
        }
        #endregion

        #region ChangePassword.aspx
        public int sel_2(string UserNum, string UserPassword)
        {
            return dal.sel_2(UserNum, UserPassword);
        }
        public int Update(string UserPassword, string UserNum)
        {
            return dal.Update(UserPassword, UserNum);
        }
        #endregion

        #region collection.aspx
        public int Delete(string DisID)
        {
            return dal.Delete(DisID);
        }

        public bool addTo(string NewsID,int ChID)
        {
            return dal.addTo(NewsID, ChID);
        }
        #endregion

        #region Exchange.aspx
        public string sel_3(string UserNum)
        {
            return dal.sel_3(UserNum);
        }
        public string sel_4(string UserGroupNumber)
        {
            return dal.sel_4(UserGroupNumber);
        }
        public DataTable sel_5(string UserNum)
        {
            return dal.sel_5(UserNum);
        }
        public string sel_6(string UserGroupNumber)
        {
            return dal.sel_6(UserGroupNumber);
        }
        public int Update1(int ipoint2, int gpoint2, string UserNum)
        {
            return dal.Update1(ipoint2, gpoint2, UserNum);
        }
        public int Add(STGhistory Gh, int ghtype, string UserNum, string content)
        {
            return dal.Add(Gh, ghtype, UserNum, content);
        }
        #endregion

        #region getPassword.aspx
        public DataTable sel_7(string UserName)
        {
            return dal.sel_7(UserName);
        }
        public DataTable sel_8(string u_un)
        {
            return dal.sel_8(u_un);
        }
        public int Update2(string UserPassword, string UserName)
        {
            return dal.Update2(UserPassword, UserName);
        }
        #endregion

        #region getPoint.aspx
        public DataTable sel_9(string Number)
        {
            return dal.sel_9(Number);
        }
        public DataTable sel_10(string cnm)
        {
            return dal.sel_10(cnm);
        }
        public int sel_11(string cnm)
        {
            return dal.sel_11(cnm);
        }
        public int sel_12()
        {
            return dal.sel_12();
        }
        public int Add1(string GhID, string UserNum, int Gpoint, int Money, DateTime CreatTime, string content)
        {
            return dal.Add1(GhID, UserNum, Gpoint, Money, CreatTime, content);
        }
        public int Update3(string UserNum, string cnm)
        {
            return dal.Update3(UserNum, cnm);
        }
        public int Update4(int Money1, string UserNum)
        {
            return dal.Update4(Money1, UserNum);
        }
        public int Update5(int points, string UserNum)
        {
            return dal.Update5(points, UserNum);
        }
        #endregion

        #region history.aspx
        public int Delete1(string ID)
        {
            return dal.Delete1(ID);
        }
        public string sel_13(string UserNum)
        {
            return dal.sel_13(UserNum);
        }
        #endregion

        #region mycom.aspx
        public DataTable sel_14()
        {
            return dal.sel_14();
        }
        public string sel_15(string UserNum)
        {
            return dal.sel_15(UserNum);
        }
        public DataTable sel_16(string GroupNumber)
        {
            return dal.sel_16(GroupNumber);
        }
        public DataTable sel_17(string UserNum)
        {
            return dal.sel_17(UserNum);
        }
        public DataTable sel_18(string UserGroupNumber)
        {
            return dal.sel_18(UserGroupNumber);
        }
        public DataTable GetPage(string title, string Um, string dtm1, string dtm2, string isCheck, string islock, string SiteID, string UserNum, int DelOTitle, int EditOtitle, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(title, Um, dtm1, dtm2, isCheck, islock, SiteID, UserNum, DelOTitle, EditOtitle, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        public int Delete2(string Commid)
        {
            return dal.Delete2(Commid);
        }
        #endregion

        #region mycom_Look.aspx
        public DataTable sel_19(string Commid)
        {
            return dal.sel_19(Commid);
        }
        #endregion

        #region mycom_up.aspx
        public int Update6(string Title, string Contents, DateTime CreatTime, string Commid, int islocks)
        {
            return dal.Update6(Title, Contents, CreatTime, Commid,islocks);
        }
        #endregion

        public DataTable GetPagepoi(string typep, string UM, string sle_NUM,string SiteID,string UserNum, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPagepoi(typep, UM, sle_NUM,SiteID,UserNum, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        public DataTable sel_20(string UM)
        {
            return dal.sel_20(UM);
        }
        public DataTable sel_21(string UserNum)
        {
            return dal.sel_21(UserNum);
        }
        public int Delete3(string GhID)
        {
            return dal.Delete3(GhID);
        }
        #region history.aspx
        public int historyCount(string Str_day)
        {
            return dal.historyCount(Str_day);
        }
        #endregion

        #region 友情连接
        public DataTable IsOpen()
        {
            return dal.IsOpen();
        }
        public DataTable ClassName_Click(string ClassID)
        {
            return dal.ClassName_Click(ClassID);
        }
        public DataTable ClassInfo()
        {
            return dal.ClassInfo();
        }
        public DataTable StartUserC()
        {
            return dal.StartUserC();
        }
        public DataTable PramValue()
        {
            return dal.PramValue();
        }
        public int ISExitNamee(string Str_Name)
        {
            return dal.ISExitNamee(Str_Name);
        }
        public int SaveLink(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, string Str_Author, string Str_Mail, string Str_ContentFor)
        {
            return dal.SaveLink(Str_Class,Str_Name, Str_Type, Str_Url, Str_Content,Str_PicUrl,Str_Author,Str_Mail, Str_ContentFor);
        }

        public void delf(string id)
        {
            dal.delf(id);
        }

        public DataTable getflist(int num,string uid)
        {
            return dal.getflist(num,uid);
        }

        #endregion
    }
}
