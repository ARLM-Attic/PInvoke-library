using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IDiscuss
    {
        #region message_discussacti.aspx
        DataTable sel(string UserNum);
        int Add(STDiscussActive DA);
        string sel_1();
        #endregion

        #region message_discussacti_list.aspx
        DataTable sel_2();
        string sel_3(string UserNum);
        int Delete(string ID, string SiteID);
        #endregion

        #region message_discussclass_add.aspx
        DataTable sel_4();
        int Add_1(string DcID, string Cname, string Content, string indexnumber, string SiteID);
        #endregion

        #region message_discusssubclass_add.aspx
        DataTable sel_5();
        DataTable getsClasslist(string ClassID);
        void getsClassDel(string ID);
        #endregion

        #region discussclass.aspx
        int Delete_1(string ID);
        #endregion

        #region userdiscuss_list.aspx
        DataTable sel_6();
        int Delete_2(string ID);
        #endregion

        #region add_discussManage.aspx
        int getDiscussTitle(string Title);
        DataTable sel_49();
        DataTable sel_50(string provinces);
        DataTable sel_52(string UserGroupNumber);
        DataTable sel_53(string UserNum);
        int sel_54(string um);
        DataTable sel_55();
        int Update_9(int cPoint2, int aPoint2, string UserNum);
        int Add_12(STDiscuss DIS);
        DataTable sel_56();
        #endregion

        #region discuss_Manageadd.aspx
        DataTable sel_7(string UserNum);
        string sel_8(string DisID);
        DataTable sel_9(string DisID);
        int Update(int Authority2, int Authority3, string UserNum);
        int Add_2(string GhID, string UserNum, int Authority3, int Authority2, DateTime Creatime);
        int Update_1(string Fundwarehouse1, string DisID);
        int sel_10(string UserNum, string DisID);
        int Add_3(string Member, string DisID, string UserNum, DateTime Creatime);
        DataTable sel_11();
        #endregion

        #region discussacti_add.aspx
        DataTable sel_12(string AId);
        int sel_13(string AId);
        int Add_4(string Telephone, int ParticipationNum, int isCompanion, string UserNum, string AIds, string PId, DateTime CreaTime);
        DataTable sel_14();
        #endregion

        #region discussacti_DC.aspx
        DataTable sel_15(string Aid);
        
        #endregion

        #region discussacti_list.aspx
        DataTable sel_16();
        #endregion

        #region discussacti_up.aspx
        DataTable sel_17(string AId);
        int Update_2(string Activesubject, string ActivePlace, string ActiveExpense, int Anum, string ActivePlan, string Contactmethod, DateTime Cutofftime, DateTime CreaTime, int ALabel, string AIds);
        #endregion

        #region discussactiestablish_list.aspx
        int Delete_3(string ID);
        int Delete_4(string ID);
        int sel_64(string ID);
        #endregion

        #region discussactijoin_list.aspx
        int Delete_5(string ID);
        #endregion

        #region discussManage_DC.aspx
        void Update_3(string DisID);
        DataTable sel_19(string DisID);
        string sel_20(string DcID);
        string sel_21(string DcID, string indexnumber);
        #endregion

        #region discussManageestablish_list.aspx
        DataTable sel_22();
        int Delete_6(string DisID);
        int Delete_7(string DisID);
        int sel_63(string DisID);
        #endregion

        #region discussManagejoin_list.aspx
        int Delete_8(string ID);
        #endregion

        #region discussphoto.aspx
        string sel_60(string UserNum);
        string sel_61(string PhotoalbumID);
        string sel_62(string ID);
        int Delete_16(string ID);
        #endregion

        #region discussphoto_add.aspx
        DataTable sel_23(string DisID);
        #endregion

        #region discussphoto_up.aspx
        DataTable sel_24(string PhotoID);
        DataTable sel_25(string DisID);
        string sel_26(string PhotoalbumID);
        #endregion

        #region discussPhotoalbum.aspx
        DataTable sel_27(string DisIDs);
        #endregion

        #region discussPhotoalbum_up.aspx
        string sel_28(string PhotoalbumIDsa);
        #endregion

        #region discussPhotoalbumlist.aspx
        DataTable sel_29(string DisID);
        string sel_30(string DisID);
        DataTable sel_31(string ID);
        int Delete_9(string ID);
        int Delete_10(string ID);
        #endregion 

        #region discussphotoclass.aspx
        int sel_32(string UserNum, string ID);
        #endregion

        #region discusssubclass_add.aspx
        DataTable sel_33();
        int Add_5(string DcID, string Cname, string Content, string indexnumber);
        #endregion

        #region discussTopi_add.aspx
        string sel_34(string DisIDs);
        DataTable sel_35();
        int Add_6(string DtID, string Title, string Content, int source, string DtUrl, string UserNum, DateTime creatTime, DateTime voteTime, string DisID);
        #endregion

        #region discussTopi_ballot.aspx
        int Add_7(string DtID, string Title, string Content, string UserNum, DateTime creatTime, DateTime voteTime, string DisID);
        int sel_36(string VoteID);
        bool Add_8(string DtID, string VoteID, string votegenre, string Voteitem);
        #endregion

        #region discussTopi_commentary.aspx
        DataTable sel_37(string Dtd);
        //int Add_9(string DtsID, string Titles, string Contentss, string UserNum, string DtIDa, DateTime creatTime, string DisIDx);
        void Add_9(Foosun.Model.STADDDiscuss uc);
        DataTable sel_38(string DtID);
        int Update_4(DateTime te, string hidd);
        string sel_39(string VoteID);
        int Update_5(int VoteNumsel, string VoteID);
        int Update_6(DateTime tm, string VoteID);
        DataTable sel_46(string UserNum);
        DataTable sel_47(string DisIDs);
        DataTable sel_48(string DtID);
        DataTable getTopicinfo(string DtID);
        void updateTopicDtID(string DtID,string title,string content);
        #endregion

        #region discussTopi_del.aspx
        int sel_59(string ID);
        int Delete_13(string ID);
        int Delete_14(string ID);
        int Delete_15(string ID);
        #endregion

        #region discussTopi_list.aspx
        DataTable sel_40(string DisIDs);
        string sel_41(string DisIDs);
        DataTable sel_42(string UserNum);
        int Delete_11(string DtID);
        int Delete_12(string ParentID);
        #endregion

        #region discussTopi_view.aspx
        int sel_43(string DtIDa);
        #endregion

        #region disFundwarehouse.aspx
        int Add_10(string GhID, string UserNum, int gPoint1, int iPoint1, DateTime Creatime);
        DataTable sel_44(string DisID);
        DataTable sel_45(string UserNum);
        int Update_7(int iPoint1, int gPoint1, string UserNum);
        int Update_8(string Fundwarehouse1, string DisID);
        int Add_11(string Fundwarehouse2, string UserNum, string DisID, DateTime Creatime);
        #endregion

        #region up_discussManage.aspx
        string sel_57(string UserNum);
        DataTable sel_58(string DID, string UserName);
        int Update_10(string Cname, string Authority, string Authoritymoney, string D_Content, string D_anno, DateTime Creatime, string ClassID, string Did, string UserName1);
        #endregion

    }
    public sealed partial class DataAccess
    {
        public static IDiscuss CreateDiscuss()
        {
            string className = path + ".Discuss";
            return (IDiscuss)Assembly.Load(path).CreateInstance(className);
        }
    }
}