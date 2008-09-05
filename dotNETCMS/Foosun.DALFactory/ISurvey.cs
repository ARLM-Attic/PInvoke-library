using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface ISurvey
    {
        #region ManageVote.aspx
        string VoteTitle_Sql(int idt);
        string VoteItem_Sql(int idi);
        string VoteUser_Sql(string Str_UserNum);
        int Delete(string CheckboxArray);
        int Delete_1();
        int Delete_2(int RID);
        #endregion

        #region setClass.aspx
        DataTable Str_VoteSql(int VID);
        DataTable sel_TitleSql(int VID);
        bool Str_VoteSql_1(int VID);
        bool Str_VoteTitleSql(int VID);
        bool Str_VoteItemSql(int tid);
        bool Str_VoteStepsSql(int tid);
        bool Str_VoteManageSql(int tid);
        int sel_1(string Str_ClassName);
        int Add(string Str_ClassName, string Str_Description, string SiteID);
        DataTable sel_2(string CheckboxArray);
        void Del_Str_VoteSql(string CheckboxArray);
        void Del_Str_VoteTitleSql(string CheckboxArray);
        void Del_Str_VoteItemSql(int tid);
        void Del_Str_VoteStepsSql(int tid);
        void Del_Str_VoteManageSql(int tid);
        int Update_Str_InSql(string Str_ClassNameE, string Str_DescriptionE, int VID);
        bool Del_1();
        bool Del_2();
        bool Del_3();
        bool Del_4();
        #endregion

        #region setItem.aspx
        DataTable Str_SelectSql();
        DataTable Str_ItemSql(int IID);
        DataTable SQl_title(string KeyWord);
        string VoteTitle_Sql_1(int idt);
        int Str_CheckSql(string Str_ItemName);
        int Add_Str_InSql(string Str_vote_CTName, string Str_ItemName, string Str_ItemValue, string Str_ItemMode, string Str_PicSrc, string Str_DisColor, string Str_VoteCount, string Str_ItemDetail, string SiteID);
        int Del_Vote_Sql(string CheckboxArray);
        int Del_Vote_Sql_1();
        int Update_Str_UpdateSql(string Str_classnameedit, string Str_itemnameedit, string Str_valueedit, string Str_itemmodele, string Str_picsurl, string Str_discoloredit, string Str_pointqe, string Str_discriptionitem, int IID);
        int Del_Str_ItemSql(int IID);
        #endregion

        #region setParam.aspx
        DataTable sel_5();
        int Update_Str_InSqls(string Str_IPtime, string Str_IsReg, string Str_IpLimit, string SiteID);
        #endregion

        #region setSteps.aspx
        DataTable sel_3();
        DataTable sel_4(string KeyWord);
        string sel_VoteTitleS_Sql(int TIDS);
        string sel_VoteTitleU_Sql(int TIDU);
        string sel_VoteSteps_Sql(int SID);
        int sel_Str_CheckSql(string Str_vote_CNameSe, string Str_vote_CNameUse);
        int Add_Str_InSql_1(string Str_vote_CNameSe, string Str_vote_CNameUse, string Str_StepsN, string SiteID);
        DataTable sel_6(int SID);
        int Del_5(int SID);
        int Del_6(string CheckboxArray);
        int Del_7();
        int Update_1(string Str_votecnameEditse, string Str_votecnameEditue, string Str_NumEdit, int SID);
        #endregion

        #region setTitle.aspx
        DataTable sel_7();
        DataTable sel_8(int TID);
        bool Del_Str_titleSql(int TID);
        bool Del_Str_itemSql_1(int TID);
        bool Del_Str_stepSql(int TID);
        bool Del_Str_manageSql(int TID);
        DataTable sel_9(string KeyWord);
        DataTable sel_VoteClassSql();
        string sel_VoteClass_Sql(int idv);
        int Del_Vote_Sql_2(string CheckboxArray);
        int Del_Str_itemSql_2(string CheckboxArray);
        int Del_Str_stepSql_3(string CheckboxArray);
        int Del_Str_manageSql_2(string CheckboxArray);
        int sel_10(string Str_Title);
        int Add_Str_InSql_2(string Str_Classname, string Str_Title, string Str_TypeSelect, string Str_MaxselectNum, string Str_DisModel, string Str_Starttime, string Str_Endtime, string Str_SortStyle, string Str_isSteps, string SiteID);
        int Update_Str_UpdateSqls(string Str_ClassNameE, string Str_TitleE, string Str_TypeE, string Str_MaxNumE, string Str_DisModelE, string Str_StartTimeE, string Str_EndTimeE, string Str_StyleE, string Str_isSteps, int TID);
        int Del_8();
        #endregion


        DataTable sel_11(int Tid);
        DataTable sel_12(int Tid, string SiteID);
        DataTable sel_13(int Tid, string SiteID, int Steps);
        DataTable sel_14(int Tid);
        DataTable sel_15(int Tid, string SiteID, int Steps);
        DataTable sel_16();
        DataTable sel_17(int TID);
        int Add_1(string CheckboxArray, int tid, string othercontent, string strvip, string strvtime, string UserNum);
        int sel_IP();
        DataTable sel_18(int tid);
        DataTable sel_19(int tid);
        int sel_20(int tid, int iid);
        int sel_21(int tid);
    }
    public sealed partial class DataAccess
    {
        public static ISurvey CreateSurvey()
        {
            string className = path + ".Survey";
            return (ISurvey)Assembly.Load(path).CreateInstance(className);
        }
    }
}