//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==                Code By Simplt.Xie                     == 
//===========================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IrootPublic
    {
        #region 接口函数
        int getSiteID(string SiteID);
        string getUserName(string UserNum);
        int getUserName_uid(string UserNum);
        string getUserNameUserNum(string UserName);
        string getGroupName(string GroupNumber);
        string getUserGroupName(string UserNum);
        string getIDGroupNumber(string GroupNumber);
        string GetRegGroupNumber();
        string getgPointName();
        string siteName();
        string siteCopyRight();
        string sitedomain();
        string indexTempletfile();
        string allTemplet();
        int ReadType();
        string SiteEmail();
        string getGidGroupNumber(int Gid);
        int LinkType();
        int CheckInt();
        int CheckNewsTitle();
        string SaveClassFilePath(string siteid);
        string SaveIndexPage();
        int PicServerTF();
        string SaveNewsFilePath();
        string SaveNewsDirPath();
        string PicServerDomain();
        string getUidUserNum(int Uid);
        string getGroupNameFlag(string UserNum);
        double getDiscount(string UserNum);
        string getUserChar(string UserNum);
        int ConstrTF();
        string upfileType();
        IDataReader GetGroupList();
        DataTable GetHelpId(string helpId);
        DataTable GetselectNewsList();
        DataTable GetselectLabelList();
        string getSingleLableStyle(string StyleID);
        DataTable GetselectLabelList1(string ClassID);
        IDataReader GetajaxsNewsList(string ParentID);
        string getSiteIDFromClass(string ClassID);
        DataTable getNewsTableIndex();
        IDataReader GetajaxsspecialList(string ParentID);
        DataTable getClassListPublic(string ParentID);
        DataTable getSpecialListPublic(string ParentID);
        DataTable getUploadInfo();
        DataTable getGroupUpInfo(string UserNum);
        DataTable getWaterInfo();
        void SaveUserAdminLogs(int num, int _num, string UserNum, string Title, string Content);
        string getResultPage(string _Content, DateTime _DateTime, string ClassID,string EName);
        string getClassEName(string ClassID);
        string getUserGroupNumber(string strUserNum);
        int getUserLoginCode();
        string getGIPoint(string UserNum);
        int getcPoint(string UserNum);
        string getChName(string SiteID);
        int getUserUserInfo(string UserNum);
        void delUserAllInfo(string UserNum);
        void delSiteAllInfo(string SiteID);
        void delNewsAllInfo(string NewsID);
        #endregion 接口函数
    }


     public sealed partial class DataAccess
    {
         public static IrootPublic CreaterootPublic()
        {
            string className = path + ".rootPublic";
            return (IrootPublic)Assembly.Load(path).CreateInstance(className);
        }
    }
}
