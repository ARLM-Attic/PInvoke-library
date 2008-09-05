using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;
namespace Foosun.AccessDAL
{
    public class Survey : DbBase, ISurvey
    {
        #region ManageVote.aspx
        private string SiteID;
        public Survey()
        {
            try
            {
                SiteID = Foosun.Global.Current.SiteID;
            }
            catch
            {
                SiteID = "0";
            }
        }
        public string VoteTitle_Sql(int idt)
        {
            #region 从调查主题表中取主题名

            string Sql = "Select Title From " + Pre + "vote_Title where TID=" + idt + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
            //return Sql;
            #endregion
        }
        public string VoteItem_Sql(int idi)
        {
            #region 从调查选项表中取选项名
            string Sql = "Select ItemName From " + Pre + "vote_Item where IID=" + idi + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
           // return Sql;
            #endregion
        }
        public string VoteUser_Sql(string Str_UserNum)
        {
            #region 从会员表中取会员名
            if (Str_UserNum == "guest" || Str_UserNum == "")
            {
                return "guest";
            }
            string Sql = "Select UserName From " + Pre + "sys_User where UserNum='" + Str_UserNum + "' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
            //return Sql;
            #endregion
        }
        public int Delete(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_Manage where rid in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Delete_1()
        {
            string Sql = "Delete From " + Pre + "vote_Manage where SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Delete_2(int RID)
        {
            string Sql = "Delete From " + Pre + "vote_Manage where rid=" + RID + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion

        #region setClass.aspx
        public DataTable Str_VoteSql(int VID)
        {
            string Sql = "Select vid,ClassName,Description From " + Pre + "vote_Class where SiteID='" + SiteID + "' and vid=" + VID;
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_TitleSql(int VID)
        {
            string Sql = "Select TID From " + Pre + "vote_Title where vid=" + VID + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public bool Str_VoteSql_1(int VID)
        {
            string Sql = "Delete From " + Pre + "vote_Class where vid=" + VID + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Str_VoteTitleSql(int VID)
        {
            string Sql = "Delete From " + Pre + "vote_Title where vid=" + VID + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Str_VoteItemSql(int tid)
        {
            string Sql = "Delete From " + Pre + "vote_Item where TID=" + tid + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Str_VoteStepsSql(int tid)
        {
            string Sql = "Delete From " + Pre + "vote_Steps where TIDS=" + tid + " or TIDU=" + tid + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Str_VoteManageSql(int tid)
        {
            string Sql = "Delete From " + Pre + "vote_Manage where TID=" + tid + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int sel_1(string Str_ClassName)
        {
            string Sql = "Select count(ClassName) From " + Pre + "vote_Class Where ClassName='" + Str_ClassName + "' and SiteID='" + SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }
        public int Add(string Str_ClassName, string Str_Description, string SiteID)
        {
            string Sql = "Insert into " + Pre + "vote_Class (ClassName,Description,SiteID) Values('" + Str_ClassName + "','" + Str_Description + "','" + SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_2(string CheckboxArray)
        {
            string Sql = "Select TID From " + Pre + "vote_Title where vid in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public void Del_Str_VoteSql(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_Class where vid in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public void Del_Str_VoteTitleSql(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_Title where vid in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public void Del_Str_VoteItemSql(int tid)
        {
            string Sql = "Delete From " + Pre + "vote_Item where TID in(" + tid + ") and SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public void Del_Str_VoteStepsSql(int tid)
        {
            string Sql = "Delete From " + Pre + "vote_Steps where TIDS in(" + tid + ") or TIDU in(" + tid + ") and SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public void Del_Str_VoteManageSql(int tid)
        {
            string Sql = "Delete From " + Pre + "vote_Manage where TID in(" + tid + ") and SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Update_Str_InSql(string Str_ClassNameE, string Str_DescriptionE, int VID)
        {
            string Sql = "Update " + Pre + "vote_Class Set ClassName='" + Str_ClassNameE + "',Description='" + Str_DescriptionE + "' where vid=" + VID + "  and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public bool Del_1()
        {
            string Sql = "Delete From " + Pre + "vote_Class where SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Del_2()
        {
            string Sql = "Delete From " + Pre + "vote_Title where SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Del_3()
        {
            string Sql = "Delete From " + Pre + "vote_Item where SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Del_4()
        {
            string Sql = "Delete From " + Pre + "vote_Steps where SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region setItem.aspx
        public DataTable Str_SelectSql()
        {
            string Sql = "Select a.VID,a.ClassName,b.TID,b.Title From " + Pre + "vote_Class as a, " + Pre + "vote_Title as b where a.VID=b.VID and a.SiteID='" + SiteID + "' and b.SiteID='" + SiteID + "'"; return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable Str_ItemSql(int IID)
        {
            string Sql = "Select TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail From " + Pre + "vote_Item where IID=" + IID + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable SQl_title(string KeyWord)
        {
            string Sql = "Select Title,TID From " + Pre + "vote_Title where Title like '%" + KeyWord + "%' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public string VoteTitle_Sql_1(int idt)
        {
            string Sql = "Select Title From " + Pre + "vote_Title where TID=" + idt + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        public int Str_CheckSql(string Str_ItemName)
        {
            string Sql = "Select count(ItemName) From " + Pre + "vote_Item Where ItemName='" + Str_ItemName + "' and SiteID='" + SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }
        public int Add_Str_InSql(string Str_vote_CTName, string Str_ItemName, string Str_ItemValue, string Str_ItemMode, string Str_PicSrc, string Str_DisColor, string Str_VoteCount, string Str_ItemDetail, string SiteID)
        {
            string Sql = "Insert into " + Pre + "vote_Item (TID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail,SiteID) Values('" + Str_vote_CTName + "','" + Str_ItemName + "','" + Str_ItemValue + "','" + Str_ItemMode + "','" + Str_PicSrc + "','" + Str_DisColor + "','" + Str_VoteCount + "','" + Str_ItemDetail + "','" + SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_Vote_Sql(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_Item where iid in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_Vote_Sql_1()
        {
            string Sql = "Delete From " + Pre + "vote_Item where SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Update_Str_UpdateSql(string Str_classnameedit, string Str_itemnameedit, string Str_valueedit, string Str_itemmodele, string Str_picsurl, string Str_discoloredit, string Str_pointqe, string Str_discriptionitem, int IID)
        {
            string Sql = "Update " + Pre + "vote_Item Set TID='" + Str_classnameedit + "',ItemName='" + Str_itemnameedit + "',ItemValue='" + Str_valueedit + "',ItemMode='" + Str_itemmodele + "',PicSrc='" + Str_picsurl + "',DisColor='" + Str_discoloredit + "',VoteCount='" + Str_pointqe + "',ItemDetail='" + Str_discriptionitem + "' where SiteID='" + SiteID + "' and iid=" + IID;
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_Str_ItemSql(int IID)
        {
            string Sql = "Delete From " + Pre + "vote_Item where iid=" + IID + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion

        #region setParam.aspx
        public DataTable sel_5()
        {
            string Sql = "Select * From " + Pre + "vote_Param";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Update_Str_InSqls(string Str_IPtime, string Str_IsReg, string Str_IpLimit, string SiteID)
        {
            int i = 0;
            string Sql = "Update " + Pre + "vote_Param Set IPtime='" + Str_IPtime + "',IsReg='" + Str_IsReg + "',IpLimit = '" + Str_IpLimit + "',SiteID='" + SiteID + "'";

            i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                Sql = "Insert into " + Pre + "vote_Param(IPtime,IsReg,IpLimit,SiteId) values('" + Str_IPtime + "','" + Str_IsReg + "','" + Str_IpLimit + "','" + SiteID + "')";
                return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            }
            return i;

        }
        #endregion

        #region setSteps.aspx
        public DataTable sel_3()
        {
            string Sql = "Select TID,Title From " + Pre + "vote_Title where isSteps=1 and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_4(string KeyWord)
        {
            string Sql = "Select Title,TID From " + Pre + "vote_Title where Title like '%" + KeyWord + "%' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public string sel_VoteTitleS_Sql(int TIDS)
        {
            string Sql = "Select Title From " + Pre + "vote_Title where TID=" + TIDS + " and isSteps=1 and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        public string sel_VoteTitleU_Sql(int TIDU)
        {
            string Sql = "Select Title From " + Pre + "vote_Title where TID=" + TIDU + " and isSteps=1 and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        public string sel_VoteSteps_Sql(int SID)
        {
            string Sql = "Select Steps From " + Pre + "vote_Steps where SID=" + SID + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        public int sel_Str_CheckSql(string Str_vote_CNameSe, string Str_vote_CNameUse)
        {
            string Sql = "Select count(*) From " + Pre + "vote_Steps Where TIDS=" + Str_vote_CNameSe + " and TIDU=" + Str_vote_CNameUse + " and SiteID='" + SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }
        public int Add_Str_InSql_1(string Str_vote_CNameSe, string Str_vote_CNameUse, string Str_StepsN, string SiteID)
        {
            string Sql = "Insert into " + Pre + "vote_Steps (TIDS,TIDU,Steps,SiteID) Values('" + Str_vote_CNameSe + "','" + Str_vote_CNameUse + "','" + Str_StepsN + "','" + SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_6(int SID)
        {
            string Sql = "Select SID,TIDS,Steps,TIDU From " + Pre + "vote_Steps where SiteID='" + SiteID + "' and SID=" + SID;
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Del_5(int SID)
        {
            string Sql = "Delete From " + Pre + "vote_Steps where sid=" + SID + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_6(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_Steps where sid in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_7()
        {
            string Sql = "Delete From " + Pre + "setSteps where SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Update_1(string Str_votecnameEditse, string Str_votecnameEditue, string Str_NumEdit, int SID)
        {
            string Sql = "Update " + Pre + "vote_Steps Set TIDS=" + Str_votecnameEditse + ",TIDU=" + Str_votecnameEditue + ",Steps=" + Str_NumEdit + "  where SiteID='" + SiteID + "' and sid=" + SID;
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion

        #region setTitle.aspx
        public DataTable sel_7()
        {
            string Sql = "Select VID,ClassName From " + Pre + "vote_Class where SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_8(int TID)
        {
            string Sql = "Select TID,VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode,isSteps From " + Pre + "vote_Title where SiteID='" + SiteID + "' and TID=" + TID;
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public bool Del_Str_titleSql(int TID)
        {
            string Sql = "Delete From " + Pre + "vote_Title where tid=" + TID + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Del_Str_itemSql_1(int TID)
        {
            string Sql = "Delete From " + Pre + "vote_Item where TID = " + TID + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Del_Str_stepSql(int TID)
        {
            string Sql = "Delete From " + Pre + "vote_Steps where TIDS = " + TID + " or TIDU=" + TID + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Del_Str_manageSql(int TID)
        {
            string Sql = "Delete From " + Pre + "vote_manage where TID = " + TID + " and SiteID='" + SiteID + "'";
            int i = DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public DataTable sel_9(string KeyWord)
        {
            string Sql = "Select ClassName,VID From " + Pre + "vote_Class where ClassName like '%" + KeyWord + "%' and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_VoteClassSql()
        {
            string Sql = "Select VID From " + Pre + "vote_Class where SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public string sel_VoteClass_Sql(int idv)
        {
            string Sql = "Select ClassName From " + Pre + "vote_Class where VID=" + idv + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }

        public int Del_Vote_Sql_2(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_Title where tid in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_Str_itemSql_2(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_Item where TID in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_Str_stepSql_3(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_Steps where TIDS in(" + CheckboxArray + ") or TIDU in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_Str_manageSql_2(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "vote_manage where TID in(" + CheckboxArray + ") and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int sel_10(string Str_Title)
        {
            string Sql = "Select count(Title) From " + Pre + "vote_Title Where Title='" + Str_Title + "' and SiteID='" + SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }
        public int Add_Str_InSql_2(string Str_Classname, string Str_Title, string Str_TypeSelect, string Str_MaxselectNum, string Str_DisModel, string Str_Starttime, string Str_Endtime, string Str_SortStyle, string Str_isSteps, string SiteID)
        {
            string Sql = "Insert into " + Pre + "vote_Title (VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode,isSteps,SiteID) Values('" + Str_Classname + "','" + Str_Title + "','" + Str_TypeSelect + "','" + Str_MaxselectNum + "','" + Str_DisModel + "','" + Str_Starttime + "','" + Str_Endtime + "','" + Str_SortStyle + "','" + Str_isSteps + "','" + SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Update_Str_UpdateSqls(string Str_ClassNameE, string Str_TitleE, string Str_TypeE, string Str_MaxNumE, string Str_DisModelE, string Str_StartTimeE, string Str_EndTimeE, string Str_StyleE, string Str_isSteps, int TID)
        {
            string Sql = "Update " + Pre + "vote_Title Set VID='" + Str_ClassNameE + "',Title='" + Str_TitleE + "',Type='" + Str_TypeE + "',MaxNum='" + Str_MaxNumE + "',DisMode='" + Str_DisModelE + "',StartDate='" + Str_StartTimeE + "',EndDate='" + Str_EndTimeE + "',ItemMode='" + Str_StyleE + "',isSteps='" + Str_isSteps + "' where SiteID='" + SiteID + "' and tid=" + TID;
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Del_8()
        {
            string Sql = "Delete From " + Pre + "vote_Title where SiteID='" + SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion

        public DataTable sel_11(int Tid)
        {
            string Sql = "Select * From " + Pre + "vote_Title where TID = " + Tid + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_12(int Tid, string SiteID)
        {
            string Sql = "Select top 1 TIDU,Steps From " + Pre + "vote_Steps where TIDS = " + Tid + " and SiteID='" + SiteID + "' order by Steps";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_13(int Tid, string SiteID, int Steps)
        {

            string Sql = "Select top 1 TIDU,Steps From " + Pre + "vote_Steps where TIDS = " + Tid + " and SiteID='" + SiteID + "' and Steps > " + Steps + " order by Steps";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable sel_14(int Tid)
        {
            string Sql = "Select * From " + Pre + "vote_Item where TID = " + Tid + " and SiteID='" + SiteID + "' order by IID asc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_15(int Tid, string SiteID, int Steps)
        {

            string Sql = "Select top 2 TIDU,Steps From " + Pre + "vote_Steps where TIDS = " + Tid + " and SiteID='" + SiteID + "' and Steps < " + Steps + " order by Steps desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_16()
        {
            string Sql = "Select IPtime,IsReg,IpLimit From " + Pre + "vote_Param";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add_1(string CheckboxArray, int tid, string othercontent, string strvip, string strvtime, string UserNum)
        {
            string Sql = "Insert into " + Pre + "vote_Manage(IID,TID,OtherContent,VoteIp,VoteTime,UserNumber,SiteID) Values('" + CheckboxArray + "'," + tid + ",'" + othercontent + "','" + strvip + "','" + strvtime + "','" + UserNum + "','0')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable sel_17(int TID)
        {
            string Sql = "select top 1 VoteIp,VoteTime,UserNumber from " + Pre + "vote_Manage where TID=" + TID + " and SiteID='" + SiteID + "' order by VoteTime desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int sel_IP()
        {
            int PageError = 0;
            string IPLIST = "";
            string LoginIP = Foosun.Global.Current.ClientIP;
            string IPSQL = "select IpLimit from " + Pre + "vote_Param";
            DataTable IPDT = (DataTable)DbHelper.ExecuteTable(CommandType.Text, IPSQL, null);
            if (IPDT != null)
            {
                if (IPDT.Rows.Count > 0)
                {
                    IPLIST = IPDT.Rows[0]["IpLimit"].ToString();
                    if (IPLIST != "" && IPLIST != null)
                    {
                        int IPTF = 0;
                        string[] arrIP = IPLIST.Split(new Char[] { '|' });
                        for (int i = 0; i < arrIP.Length; i++)
                        {
                            if (arrIP[i].IndexOf('*') >= 0)
                            {
                                string strIP = arrIP[i];
                                string[] IPR = strIP.Split(new Char[] { '.' });
                                string[] IPR1 = LoginIP.Split(new Char[] { '.' });
                                if (IPR[2] == "*")
                                {
                                    if (IPR1[0] + IPR1[1] == IPR[0] + IPR[1])
                                    {
                                        IPTF = 1;
                                    }
                                }
                                else if (IPR[3] == "*")
                                {
                                    if (IPR1[0] + IPR1[1] + IPR1[2] == IPR[0] + IPR[1] + IPR[2])
                                    {
                                        IPTF = 1;
                                    }
                                }
                            }
                            else
                            {
                                if (arrIP[i].ToString() == LoginIP.ToString())
                                {
                                    IPTF = 1;
                                }
                            }
                        }
                        if (IPTF == 1)
                        {
                            PageError = 1;
                        }
                    }

                }
                IPDT.Clear();
                IPDT.Dispose();
            }
            return PageError;
        }
        public DataTable sel_18(int tid)
        {
            string Sql = "Select Title From " + Pre + "vote_Title where TID=" + tid + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_19(int tid)
        {
            string Sql = "Select * From " + Pre + "vote_Item where TID=" + tid + " and SiteID='" + SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int sel_20(int tid, int iid)
        {
            string Sql = "Select count(*) From " + Pre + "vote_Manage where TID = " + tid + " and IID = " + iid + " and SiteID='" + SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }
        public int sel_21(int tid)
        {
            string Sql = "Select count(*) From " + Pre + "vote_Manage where TID = " + tid + " and SiteID='" + SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, null);
        }
    }
}


