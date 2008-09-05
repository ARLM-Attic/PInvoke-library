//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By ZhaoHui.Chen                    ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class Survey
    {
        private ISurvey dal;
        public Survey()
        {
            dal = Foosun.DALFactory.DataAccess.CreateSurvey();
        }
        #region ManageVote.aspx
        public string VoteTitle_Sql(int idt)
        {
            return dal.VoteTitle_Sql(idt);
        }
        public string VoteItem_Sql(int idi)
        {
            return dal.VoteItem_Sql(idi);
        }
        public string VoteUser_Sql(string Str_UserNum)
        {
            return dal.VoteUser_Sql(Str_UserNum);
        }
        public int Delete(string CheckboxArray)
        {
            return dal.Delete(CheckboxArray);
        }
        public int Delete_1()
        {
            return dal.Delete_1();
        }
        public int Delete_2(int RID)
        {
            return dal.Delete_2(RID);
        }
        #endregion

        #region setClass.aspx
        public DataTable Str_VoteSql(int VID)
        {
            return dal.Str_VoteSql(VID);
        }
        public DataTable sel_TitleSql(int VID)
        {
            return dal.sel_TitleSql(VID);
        }
        public bool Str_VoteSql_1(int VID)
        {
            return dal.Str_VoteSql_1(VID);
        }
        public bool Str_VoteTitleSql(int VID)
        {
            return dal.Str_VoteTitleSql(VID);
        }
        public bool Str_VoteItemSql(int tid)
        {
            return dal.Str_VoteItemSql(tid);
        }
        public bool Str_VoteStepsSql(int tid)
        {
            return dal.Str_VoteStepsSql(tid);
        }
        public bool Str_VoteManageSql(int tid)
        {
            return dal.Str_VoteManageSql(tid);
        }
        public int sel_1(string Str_ClassName)
        {
            return dal.sel_1(Str_ClassName);
        }
        public int Add(string Str_ClassName, string Str_Description, string SiteID)
        {
            return dal.Add(Str_ClassName, Str_Description, SiteID);
        }
        public DataTable sel_2(string CheckboxArray)
        {
            return dal.sel_2(CheckboxArray);
        }
        public void Del_Str_VoteSql(string CheckboxArray)
        {
            dal.Del_Str_VoteSql(CheckboxArray);
        }
        public void Del_Str_VoteTitleSql(string CheckboxArray)
        {
            dal.Del_Str_VoteTitleSql(CheckboxArray);
        }
        public void Del_Str_VoteItemSql(int tid)
        {
            dal.Del_Str_VoteItemSql(tid);
        }
        public void Del_Str_VoteStepsSql(int tid)
        {
            dal.Del_Str_VoteStepsSql(tid);
        }
        public void Del_Str_VoteManageSql(int tid)
        {
            dal.Del_Str_VoteManageSql(tid);
        }
        public int Update_Str_InSql(string Str_ClassNameE, string Str_DescriptionE, int VID)
        {
            return dal.Update_Str_InSql(Str_ClassNameE, Str_DescriptionE, VID);
        }
        public bool Del_1()
        {
            return dal.Del_1();
        }
        public bool Del_2()
        {
            return dal.Del_2();
        }
        public bool Del_3()
        {
            return dal.Del_3();
        }
        public bool Del_4()
        {
            return dal.Del_4();
        }
        #endregion

        #region setItem.aspx
        public DataTable Str_SelectSql()
        {
            return dal.Str_SelectSql();
        }
        public DataTable Str_ItemSql(int IID)
        {
            return dal.Str_ItemSql(IID);
        }
        public DataTable SQl_title(string KeyWord)
        {
            return dal.SQl_title(KeyWord);
        }
        public string VoteTitle_Sql_1(int idt)
        {
            return dal.VoteTitle_Sql_1(idt);
        }
        public int Str_CheckSql(string Str_ItemName)
        {
            return dal.Str_CheckSql(Str_ItemName);
        }
        public int Add_Str_InSql(string Str_vote_CTName, string Str_ItemName, string Str_ItemValue, string Str_ItemMode, string Str_PicSrc, string Str_DisColor, string Str_VoteCount, string Str_ItemDetail, string SiteID)
        { 
            return dal.Add_Str_InSql(Str_vote_CTName, Str_ItemName, Str_ItemValue, Str_ItemMode, Str_PicSrc, Str_DisColor, Str_VoteCount, Str_ItemDetail, SiteID);
        }
        public int Del_Vote_Sql(string CheckboxArray)
        {
            return dal.Del_Vote_Sql(CheckboxArray);
        }
        public int Del_Vote_Sql_1()
        {
            return dal.Del_Vote_Sql_1();
        }
        public int Update_Str_UpdateSql(string Str_classnameedit, string Str_itemnameedit, string Str_valueedit, string Str_itemmodele, string Str_picsurl, string Str_discoloredit, string Str_pointqe, string Str_discriptionitem, int IID)
        {
            return dal.Update_Str_UpdateSql(Str_classnameedit, Str_itemnameedit, Str_valueedit, Str_itemmodele, Str_picsurl, Str_discoloredit, Str_pointqe, Str_discriptionitem, IID);
        }
        public int Del_Str_ItemSql(int IID)
        {
            return dal.Del_Str_ItemSql(IID);
        }
        #endregion

        #region setParam.aspx
        public DataTable sel_5()
        {
            return dal.sel_5();
        }
        public int Update_Str_InSqls(string Str_IPtime, string Str_IsReg, string Str_IpLimit, string SiteID)
        {
            return dal.Update_Str_InSqls(Str_IPtime, Str_IsReg, Str_IpLimit, SiteID);
        }
        #endregion

        #region setSteps.aspx
        public DataTable sel_3()
        {
            return dal.sel_3();
        }
        public DataTable sel_4(string KeyWord)
        {
            return dal.sel_4(KeyWord);
        }
        public string sel_VoteTitleS_Sql(int TIDS)
        {
            return dal.sel_VoteTitleS_Sql(TIDS);
        }
        public string sel_VoteTitleU_Sql(int TIDU)
        {
            return dal.sel_VoteTitleU_Sql(TIDU);
        }
        public string sel_VoteSteps_Sql(int SID)
        {
            return dal.sel_VoteSteps_Sql(SID);
        }
        public int sel_Str_CheckSql(string Str_vote_CNameSe, string Str_vote_CNameUse)
        {
            return dal.sel_Str_CheckSql(Str_vote_CNameSe, Str_vote_CNameUse);
        }
        public int Add_Str_InSql_1(string Str_vote_CNameSe, string Str_vote_CNameUse, string Str_StepsN, string SiteID)
        {
            return dal.Add_Str_InSql_1(Str_vote_CNameSe, Str_vote_CNameUse, Str_StepsN, SiteID);
        }
        public DataTable sel_6(int SID)
        {
            return dal.sel_6(SID);
        }
        public int Del_5(int SID)
        {
            return dal.Del_5(SID);
        }
        public int Del_6(string CheckboxArray)
        {
            return dal.Del_6(CheckboxArray);
        }
        public int Del_7()
        {
            return dal.Del_7();
        }
        public int Update_1(string Str_votecnameEditse, string Str_votecnameEditue, string Str_NumEdit, int SID)
        {
            return dal.Update_1(Str_votecnameEditse, Str_votecnameEditue, Str_NumEdit, SID);
        }
        #endregion

        #region setTitle.aspx
        public DataTable sel_7()
        {
            return dal.sel_7();
        }
        public DataTable sel_8(int TID)
        {
            return dal.sel_8(TID);
        }
        public bool Del_Str_titleSql(int TID)
        {
            return dal.Del_Str_titleSql(TID);
        }
        public bool Del_Str_itemSql_1(int TID)
        {
            return dal.Del_Str_itemSql_1(TID);
        }
        public bool Del_Str_stepSql(int TID)
        {
            return dal.Del_Str_stepSql(TID);
        }
        public bool Del_Str_manageSql(int TID)
        {
            return dal.Del_Str_manageSql(TID);
        }
        public DataTable sel_9(string KeyWord)
        {
            return dal.sel_9(KeyWord);
        }
        public DataTable sel_VoteClassSql()
        {
            return dal.sel_VoteClassSql();
        }
        public string sel_VoteClass_Sql(int idv)
        {
            return dal.sel_VoteClass_Sql(idv);
        }
        public int Del_Vote_Sql_2(string CheckboxArray)
        {
            return dal.Del_Vote_Sql_2(CheckboxArray);
        }
        public int Del_Str_itemSql_2(string CheckboxArray)
        {
            return dal.Del_Str_itemSql_2(CheckboxArray);
        }
        public int Del_Str_stepSql_3(string CheckboxArray)
        {
            return dal.Del_Str_stepSql_3(CheckboxArray);
        }
        public int Del_Str_manageSql_2(string CheckboxArray)
        {
            return dal.Del_Str_manageSql_2(CheckboxArray);
        }
        public int sel_10(string Str_Title)
        {
            return dal.sel_10(Str_Title);
        }
        public int Add_Str_InSql_2(string Str_Classname, string Str_Title, string Str_TypeSelect, string Str_MaxselectNum, string Str_DisModel, string Str_Starttime, string Str_Endtime, string Str_SortStyle, string Str_isSteps, string SiteID)
        {
            return dal.Add_Str_InSql_2(Str_Classname, Str_Title, Str_TypeSelect, Str_MaxselectNum, Str_DisModel, Str_Starttime, Str_Endtime, Str_SortStyle, Str_isSteps, SiteID);
        }
        public int Update_Str_UpdateSqls(string Str_ClassNameE, string Str_TitleE, string Str_TypeE, string Str_MaxNumE, string Str_DisModelE, string Str_StartTimeE, string Str_EndTimeE, string Str_StyleE, string Str_isSteps, int TID)
        {
            return dal.Update_Str_UpdateSqls(Str_ClassNameE, Str_TitleE, Str_TypeE, Str_MaxNumE, Str_DisModelE, Str_StartTimeE, Str_EndTimeE, Str_StyleE, Str_isSteps, TID);
        }
        public int Del_8()
        {
            return dal.Del_8();
        }
        #endregion


        public DataTable sel_11(int Tid)
        {
            return dal.sel_11(Tid);
        }
        public DataTable sel_12(int Tid, string SiteID)
        {
            return dal.sel_12(Tid, SiteID);
        }
        public DataTable sel_13(int Tid, string SiteID, int Steps)
        { 
            return dal.sel_13(Tid, SiteID, Steps);
        }
        public DataTable sel_14(int Tid)
        {
            return dal.sel_14(Tid);
        }
        public DataTable sel_15(int Tid, string SiteID, int Steps)
        {
            return dal.sel_15(Tid, SiteID, Steps);
        }
        public DataTable sel_16()
        {
            return dal.sel_16();
        }
        public int Add_1(string CheckboxArray, int tid, string othercontent, string strvip, string strvtime, string UserNum)
        {
            return dal.Add_1(CheckboxArray, tid, othercontent, strvip, strvtime, UserNum);
        }
        public DataTable sel_17(int TID)
        {
            return dal.sel_17(TID);
        }
        public int sel_IP()
        {
            return dal.sel_IP();
        }
        public DataTable sel_18(int tid)
        {
            return dal.sel_18(tid);
        }
        public DataTable sel_19(int tid)
        {
            return dal.sel_19(tid);
        }
        public int sel_20(int tid, int iid)
        {
            return dal.sel_20(tid, iid);
        }
        public int sel_21(int tid)
        {
            return dal.sel_21(tid);
        }
    }
}