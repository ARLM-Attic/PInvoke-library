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
    public class Constr
    {
        private IConstr dal;
        public Constr()
        {
            dal = Foosun.DALFactory.DataAccess.CreateConstr();
        }
        #region 前台
        public int Add(STConstr Con)
        {
            return dal.Add(Con);
        }
        public int Update(STConstr Con, string ConIDs)
        {
            return dal.Update(Con, ConIDs);
        }
        public int selGroupNumber(string UserNum)
        {
            return dal.selGroupNumber(UserNum);
        }
        public DataTable selConstrClass(string UserNum)
        {
            return dal.selConstrClass(UserNum);
        }
        public string selcName(string u_ClassID)
        {
            return dal.selcName(u_ClassID);
            
        }
        public string selSiteID(string u_SiteID)
        {
            return dal.selcName(u_SiteID);
        }
        public DataTable sel1(string ConID)
        {
            return dal.sel1(ConID);
        }
        public int sel2()
        {
            return dal.sel2();
        }
        public int Delete(string ID)
        {
            return dal.Delete(ID);
        }
        public int sel3(string UserNum)
        {
            return dal.sel3(UserNum);
        }
        public int Add1(STuserother Con, string UserNum)
        {
            return dal.Add1(Con, UserNum);
        }
        public int Update1(STuserother Con, string ConIDs)
        {
            return dal.Update1(Con, ConIDs);
        }
        public DataTable sel4(string ConIds)
        {
            return dal.sel4(ConIds);
        }
        public string ConstrTF()
        {
            return dal.ConstrTF();
        }

        #region ConstrClass.aspx
        public int Delete1(string ID)
        {
            return dal.Delete1(ID);
        }
        public DataTable sel5(string ID)
        {
            return dal.sel5(ID);
        }
        #endregion 

        #region ConstrClass_add.aspx
        public DataTable sel6()
        {
            return dal.sel6();
        }
        public int Add2(STConstrClass Con, string Ccid, string UserNum)
        {
            return dal.Add2(Con, Ccid, UserNum);
        }
        #endregion

        #region ConstrClass_up.aspx
        public DataTable sel7(string Ccid)
        {
            return dal.sel7(Ccid);
        }
        public int Update2(STConstrClass Con, string Ccid)
        {
            return dal.Update2(Con,Ccid);
        }
        #endregion

        #region Constrlist.aspx
        public string sel_cName(string Ccid)
        {
            return dal.sel_cName(Ccid);
        }
        public string sel_Tags(string ID)
        {
            return dal.sel_Tags(ID);
        }
        public int Update_Tage1(string tagsd, string ID)
        {
            return dal.Update_Tage1(tagsd,ID);
        }
        public int Delete2(string ID)
        {
            return dal.Delete2(ID);
        }
        public DataTable GetPage(string UserNum, string ClassID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(UserNum, ClassID, PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        public DataTable GetPage1(int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage1(PageIndex, PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        #endregion

        #region Constrlistpass.aspx
        public int Delete3(string ID)
        {
            return dal.Delete3(ID);
        }
        #endregion

        #region Constrlistpass_DC.aspx
        public DataTable sel8(string ID)
        {
            return dal.sel8(ID);
        }
        public string sel9(string ClassID)
        {
            return dal.sel9(ClassID);
        }
        public string sel19(string UserNum)
        {
            return dal.sel19(UserNum);
        }
        #endregion

        #endregion

        #region 后台

        #region Constr_chicklist.aspx
        public DataTable sel10(string ID)
        {
            return dal.sel10(ID);
        }
        public int Update3(string ID)
        {
            return dal.Update3(ID);
        }
        public int Delete4(string ID)
        {
            return dal.Delete4(ID);
        }
        #endregion

        #region Constr_Edit.aspx
        public DataTable sel11(string ConIDs)
        {
            return dal.sel11(ConIDs);
        }
        public DataTable sel12()
        {
            return dal.sel12();
        }
        public DataTable sel13(string ConIDp)
        {
            return dal.sel13(ConIDp);
        }
        public int Add3(string NewsID, string NewsTitle, string PicURL, string ClassID, string Author, string Editor, string Souce, string Content, string CreatTime, string SiteID, string Tags, string DataLib,string NewsTemplet,string strSavePath,string strfileName,string strfileexName,string strCheckInt)
        {
            return dal.Add3(NewsID, NewsTitle, PicURL, ClassID, Author, Editor, Souce, Content, CreatTime, SiteID, Tags, DataLib, NewsTemplet, strSavePath, strfileName, strfileexName, strCheckInt);
        }
        public int Add3(string NewsID, int NewsType, string NewsTitle, string PicURL, string ClassID, string Author, string Editor, string Souce, string Content, string CreatTime, string SiteID, string Tags, string DataLib, string NewsTemplet, string strSavePath, string strfileName, string strfileexName, string strCheckInt)
        {
            return dal.Add3(NewsID, NewsType, NewsTitle, PicURL, ClassID, Author, Editor, Souce, Content, CreatTime, SiteID, Tags, DataLib, NewsTemplet, strSavePath, strfileName, strfileexName, strCheckInt);
        }
        public int Add4(string NewsID, int gPoint, int iPoint, int Money1, DateTime CreatTime1, string UserNum, string content4)
        {
            return dal.Add4(NewsID, gPoint, iPoint, Money1, CreatTime1, UserNum, content4);
        }
        public int Update5(int iPoint2, int gPoint2, Decimal Money3, int cPoint2, int aPoint2, string UserNum)
        {
            return dal.Update5(iPoint2, gPoint2, Money3, cPoint2, aPoint2, UserNum);
        }
        public int Update4(string ConIDp)
        {
            return dal.Update4(ConIDp);
        }
        public DataTable sel14(string PCIdsa)
        {
            return dal.sel14(PCIdsa);
        }
        public DataTable sel15(string UserNum)
        {
            return dal.sel15(UserNum);
        }
        public DataTable sel16()
        {
            return dal.sel16();
        }
        public DataTable sel17()
        {
            return dal.sel17();
        }
        public string sel18(string ClassID)
        {
            return dal.sel18(ClassID);
        }

        public void updateConstrStrat(string ConID)
        {
            dal.updateConstrStrat(ConID);
        }

        public int getParmConstrNum(string UserNum)
        {
            return dal.getParmConstrNum(UserNum);
        }

        #endregion

        #region Constr_Pay.aspx
        public DataTable sel20(string UserNum)
        {
            return dal.sel20(UserNum);
        }
        public DataTable sel21(string UserNum)
        {
            return dal.sel21(UserNum);
        }
        public DataTable sel22()
        {
            return dal.sel22();
        }
        public int Add5(string UserNum1, int ParmConstrNums, DateTime payTime, string constrPayID)
        {
            return dal.Add5(UserNum1, ParmConstrNums, payTime, constrPayID);
        }
        public int Update5(string UserNum1)
        {
            return dal.Update5(UserNum1);
        }
        #endregion

        #region Constr_Return.aspx
        public DataTable sel23(string ConID)
        {
            return dal.sel23(ConID);
        }
        public int Update6(string passcontent, string ConIDs)
        {
            return dal.Update6(passcontent, ConIDs);
        }
        #endregion

        #region Constr_SetParam.aspx
        public int Add6(string PCId, string ConstrPayName, string gpoint, string ipoint, int moneys1, string Gunit)
        {
            return dal.Add6(PCId, ConstrPayName, gpoint, ipoint, moneys1, Gunit);
        }
        public DataTable sel24()
        {
            return dal.sel24();
        }
        #endregion

        #region Constr_SetParamlist.aspx
        public int Delete5(string ID)
        {
            return dal.Delete5(ID);
        }
        #endregion

        #region Constr_SetParamup.aspx
        public DataTable sel25(string PCIdup)
        {
            return dal.sel25(PCIdup);
        }
        public int Update6(string ConstrPayName, string gpoint, string ipoint, int moneys1, string Gunit, string PCIdup)
        {
            return dal.Update6(ConstrPayName, gpoint, ipoint, moneys1, Gunit, PCIdup);
        }
        #endregion

        #region Constr_Stat.aspx
        public int sel26(string UserNum)
        {
            return dal.sel26(UserNum);
        }
        public int sel27(string UserNum)
        {
            return dal.sel27(UserNum);
        }
        public DataTable sel28(string UserNum)
        {
            return dal.sel28(UserNum);
        }
        public int sel29(string UserNum, int m1)
        {
            return dal.sel29(UserNum,m1);
        }
        #endregion

        #region paymentannals.aspx
        public DataTable sel30(string UserNum)
        {
            return dal.sel30(UserNum);
        }
        public string sel31(string UserNum)
        {
            return dal.sel31(UserNum);
        }
        public int Delete6(string ID)
        {
            return dal.Delete6(ID);
        }
        #endregion

        /// <summary>
        /// 得到栏目模板，扩展名等。
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getClassInfo(string ClassID)
        {
            return dal.getClassInfo(ClassID);
        }
        #endregion
    }
}
