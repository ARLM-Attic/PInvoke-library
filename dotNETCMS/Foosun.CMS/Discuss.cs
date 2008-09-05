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
    public class Discuss
    {
        private IDiscuss dal;
        public Discuss()
        {
            dal = Foosun.DALFactory.DataAccess.CreateDiscuss();
        }
        #region message_discussacti.aspx
        public DataTable sel(string UserNum)
        {
            return dal.sel(UserNum);
        }
        public int Add(STDiscussActive DA)
        {
            return dal.Add(DA);
        }
        public string sel_1()
        {
            return dal.sel_1();
        }
        #endregion

        #region message_discussacti_list.aspx
        public DataTable sel_2()
        {
            return dal.sel_2();
        }
        public string sel_3(string UserNum)
        {
            return dal.sel_3(UserNum);
        }
        public int Delete(string ID, string SiteID)
        {
            return dal.Delete(ID, SiteID);
        }
        #endregion

        #region message_discussclass_add.aspx
        public DataTable sel_4()
        {
            return dal.sel_4();
        }
        public int Add_1(string DcID, string Cname, string Content, string indexnumber, string SiteID)
        {
            return dal.Add_1(DcID, Cname, Content, indexnumber, SiteID);
        }
        #endregion

        #region message_discusssubclass_add.aspx
        public DataTable sel_5()
        {
            return dal.sel_5();
        }

        public DataTable getsClasslist(string ClassID)
        {
            return dal.getsClasslist(ClassID);
        }

        public void getsClassDel(string ID)
        {
            dal.getsClassDel(ID);
        }

        #endregion

        #region discussclass.aspx
        public int Delete_1(string ID)
        {
            return dal.Delete_1(ID);
        }
        #endregion

        #region userdiscuss_list.aspx
        public DataTable sel_6()
        {
            return dal.sel_6();
        }
        public int Delete_2(string ID)
        {
            return dal.Delete_2(ID);
        }
        #endregion

        #region add_discussManage.aspx
        public int getDiscussTitle(string Title)
        {
            return dal.getDiscussTitle(Title);
        }
        public DataTable sel_49()
        {
            return dal.sel_49();
        }
        public DataTable sel_50(string provinces)
        {
            return dal.sel_50(provinces);
        }

        public DataTable sel_52(string UserGroupNumber)
        {
            return dal.sel_52(UserGroupNumber);
        }
        public DataTable sel_53(string UserNum)
        {
            return dal.sel_53(UserNum);
        }
        public int sel_54(string um)
        {
            return dal.sel_54(um);
        }
        public DataTable sel_55()
        {
            return dal.sel_55();
        }
        public int Update_9(int cPoint2, int aPoint2, string UserNum)
        {
            return dal.Update_9(cPoint2,aPoint2,UserNum);
        }
        public int Add_12(STDiscuss DIS)
        {
            return dal.Add_12(DIS);
        }
        public DataTable sel_56()
        {
            return dal.sel_56();
        }
        #endregion

        #region discuss_Manageadd.aspx
        public DataTable sel_7(string UserNum)
        {
            return  dal.sel_7(UserNum);
        }
        public string sel_8(string DisID)
        {
            return dal.sel_8(DisID);
        }
        public DataTable sel_9(string DisID)
        {
            return dal.sel_9(DisID);
        }
        public int Update(int Authority2, int Authority3, string UserNum)
        {
            return dal.Update(Authority2, Authority3, UserNum);           
        }
        public int Add_2(string GhID, string UserNum, int Authority3, int Authority2, DateTime Creatime)
        {
            return dal.Add_2(GhID, UserNum,Authority3,Authority2, Creatime);           
        }
        public int Update_1(string Fundwarehouse1, string DisID)
        {
            return dal.Update_1(Fundwarehouse1, DisID);          
        }
        public int sel_10(string UserNum, string DisID)
        {
            return dal.sel_10(UserNum, DisID);            
        }
        public int Add_3(string Member, string DisID, string UserNum, DateTime Creatime)
        {
            return dal.Add_3(Member, DisID, UserNum, Creatime);           
        }
        public DataTable sel_11()
        {
            return dal.sel_11();
        }
        #endregion

        #region discussacti_add.aspx
        public DataTable sel_12(string AId)
        {
            return dal.sel_12(AId);
        }
        public int sel_13(string AId)
        {
            return dal.sel_13(AId);
        }
        public int Add_4(string Telephone, int ParticipationNum, int isCompanion, string UserNum, string AIds, string PId, DateTime CreaTime)
        {
            return dal.Add_4(Telephone,ParticipationNum,isCompanion,UserNum,AIds,PId,CreaTime);
        }
        public DataTable sel_14()
        {
            return dal.sel_14();
        }
        #endregion

        #region discussacti_DC.aspx
        public DataTable sel_15(string Aid)
        {
            return dal.sel_15(Aid);
        }
        #endregion

        #region discussacti_list.aspx
        public DataTable sel_16()
        {
            return dal.sel_16();
        }
        #endregion

        #region discussacti_up.aspx
        public DataTable sel_17(string AId)
        {
            return dal.sel_17(AId);
        }
        public int Update_2(string Activesubject, string ActivePlace, string ActiveExpense, int Anum, string ActivePlan, string Contactmethod, DateTime Cutofftime, DateTime CreaTime, int ALabel, string AIds)
        { 
            return dal.Update_2(Activesubject,ActivePlace,ActiveExpense,Anum,ActivePlan,Contactmethod,Cutofftime,CreaTime,ALabel,AIds);
        }
        #endregion

        #region discussactiestablish_list.aspx

        public int Delete_3(string ID)
        {
            return dal.Delete_3(ID);
        }
        public int Delete_4(string ID)
        {
            return dal.Delete_4(ID);
        }
        public int sel_64(string ID)
        {
            return dal.sel_64(ID);
        }
        #endregion

        #region discussactijoin_list.aspx
        public int Delete_5(string ID)
        {
            return dal.Delete_5(ID);
        }
        #endregion

        #region discussManage_DC.aspx
        public void Update_3(string DisID)
        {
            dal.Update_3(DisID);
        }
        public DataTable sel_19(string DisID)
        {
            return dal.sel_19(DisID);
        }
        public string sel_20(string DcID)
        {
            return dal.sel_20(DcID);
        }
        public string sel_21(string DcID, string indexnumber)
        {
            return dal.sel_21(DcID, indexnumber);
        }
        #endregion

        #region discussManageestablish_list.aspx
        public DataTable sel_22()
        {
            return dal.sel_22();
        }
        public int Delete_6(string DisID)
        {
            return dal.Delete_6(DisID);
        }
        public int Delete_7(string DisID)
        {
            return dal.Delete_7(DisID);
        }
        public int sel_63(string DisID)
        {
            return dal.sel_63(DisID);
        }
        #endregion

        #region discussManagejoin_list.aspx
        public int Delete_8(string ID)
        {
            return dal.Delete_8(ID);
        }
        #endregion

        #region discussphoto_add.aspx
        public DataTable sel_23(string DisID)
        {
            return dal.sel_23(DisID);
        }
        #endregion

        #region discussphoto_up.aspx
        public DataTable sel_24(string PhotoID)
        {
            return dal.sel_24(PhotoID);
        }
        public DataTable sel_25(string DisID)
        {
            return dal.sel_25(DisID);
        }
        public string sel_26(string PhotoalbumID)
        {
            return dal.sel_26(PhotoalbumID);
        }
        #endregion

        #region discussPhotoalbum.aspx
        public DataTable sel_27(string DisIDs)
        {
            return dal.sel_27(DisIDs);
        }
        #endregion

        #region discussPhotoalbum_up.aspx
        public string sel_28(string PhotoalbumIDsa)
        {
            return dal.sel_28(PhotoalbumIDsa);
        }
        #endregion

        #region discussPhotoalbumlist.aspx
        public DataTable sel_29(string DisID)
        {
            return dal.sel_29(DisID);
        }
        public string sel_30(string DisID)
        {
            return dal.sel_30(DisID);
        }
        public DataTable sel_31(string ID)
        {
            return dal.sel_31(ID);
        }
        public int Delete_9(string ID)
        {
            return dal.Delete_9(ID);
        }
        public int Delete_10(string ID)
        {
            return dal.Delete_10(ID);
        }
        #endregion 

        #region discussphoto.aspx
        public string sel_60(string UserNum)
        {
            return dal.sel_60(UserNum);
        }
        public string sel_61(string PhotoalbumID)
        {
            return dal.sel_61(PhotoalbumID);
        }
        public int Delete_16(string ID)
        {
            return dal.Delete_16(ID);
        }
        public string sel_62(string ID)
        {
            return dal.sel_62(ID);
        }
        #endregion

        #region discussphotoclass.aspx
        public int sel_32(string UserNum, string ID)
        {
            return dal.sel_32(UserNum,ID);
        }
        #endregion

        #region discusssubclass_add.aspx
        public DataTable sel_33()
        {
            return dal.sel_33();
        }
        public int Add_5(string DcID, string Cname, string Content, string indexnumber)
        {
            return dal.Add_5(DcID, Cname, Content, indexnumber);
        }
        #endregion

        #region discussTopi_add.aspx
        public string sel_34(string DisIDs)
        {
            return dal.sel_34(DisIDs);
        }
        public DataTable sel_35()
        {
            return dal.sel_35();
        }
        public int Add_6(string DtID, string Title, string Content, int source, string DtUrl, string UserNum, DateTime creatTime, DateTime voteTime, string DisID)
        {
            return dal.Add_6(DtID, Title, Content, source, DtUrl, UserNum, creatTime, voteTime, DisID);
        }
        #endregion

        #region discussTopi_ballot.aspx
        public int Add_7(string DtID, string Title, string Content, string UserNum, DateTime creatTime, DateTime voteTime, string DisID)
        {
            return dal.Add_7(DtID, Title, Content, UserNum, creatTime, voteTime, DisID);
        }
        public int sel_36(string VoteID)
        {
            return dal.sel_36(VoteID);
        }
        public bool Add_8(string DtID, string VoteID, string votegenre, string Voteitem)
        {
            return dal.Add_8(DtID, VoteID, votegenre, Voteitem);
        }
        #endregion

        #region discussTopi_commentary.aspx
        public DataTable sel_37(string Dtd)
        {
            return dal.sel_37(Dtd);
        }

        public void Add_9(Foosun.Model.STADDDiscuss uc)
        {
            dal.Add_9(uc);
        }

        public DataTable sel_38(string DtID)
        {
            return dal.sel_38(DtID);
        }
        public int Update_4(DateTime te, string hidd)
        {
            return dal.Update_4(te,hidd);
        }
        public string sel_39(string VoteID)
        {
            return dal.sel_39(VoteID);
        }
        public int Update_5(int VoteNumsel, string VoteID)
        {
            return dal.Update_5(VoteNumsel, VoteID);
        }
        public int Update_6(DateTime tm, string VoteID)
        {
            return dal.Update_6(tm, VoteID);
        }
        public DataTable sel_46(string UserNum)
        {
            return dal.sel_46(UserNum);
        }
        public DataTable sel_47(string DisIDs)
        {
            return dal.sel_47(DisIDs);
        }
        public DataTable sel_48(string DtID)
        {
            return dal.sel_48(DtID);
        }

        public DataTable getTopicinfo(string DtID)
        {
            return dal.getTopicinfo(DtID);
        }

        public void updateTopicDtID(string DtID, string title, string content)
        {
            dal.updateTopicDtID(DtID, title, content);
        }
        #endregion

        #region discussTopi_del.aspx
        public int sel_59(string ID)
        {
            return dal.sel_59(ID);
        }
        public int Delete_13(string ID)
        {
            return dal.Delete_13(ID);
        }
        public int Delete_14(string ID)
        {
            return dal.Delete_14(ID);
        }

        public int Delete_15(string ID)
        {
            return dal.Delete_15(ID);
        }
        #endregion

        #region discussTopi_list.aspx
        public DataTable sel_40(string DisIDs)
        {
            return dal.sel_40(DisIDs);
        }
        public string sel_41(string DisIDs)
        {
            return dal.sel_41(DisIDs);
        }
        public DataTable sel_42(string UserNum)
        {
            return dal.sel_42(UserNum);
        }
        public int Delete_11(string DtID)
        {
            return dal.Delete_11(DtID);
        }
        public int Delete_12(string ParentID)
        {
            return dal.Delete_12(ParentID);
        }
        #endregion

        #region discussTopi_view.aspx
        public int sel_43(string DtIDa)
        {
            return dal.sel_43(DtIDa);
        }
        #endregion

        #region disFundwarehouse.aspx
        public int Add_10(string GhID, string UserNum, int gPoint1, int iPoint1, DateTime Creatime)
        {
            return dal.Add_10(GhID, UserNum, gPoint1, iPoint1, Creatime);
        }
        public DataTable sel_44(string DisID)
        {
            return dal.sel_44(DisID);
        }
        public DataTable sel_45(string UserNum)
        {
            return dal.sel_45(UserNum);
        }
        public int Update_7(int iPoint1, int gPoint1, string UserNum)
        {
            return dal.Update_7(iPoint1, gPoint1, UserNum);
        }
        public int Update_8(string Fundwarehouse1, string DisID)
        {
            return dal.Update_8(Fundwarehouse1, DisID);
        }
        public int Add_11(string Fundwarehouse2, string UserNum, string DisID, DateTime Creatime)
        {
            return dal.Add_11(Fundwarehouse2, UserNum, DisID, Creatime);
        }
        #endregion

        #region up_discussManage.aspx
        public string sel_57(string UserNum)
        {
            return dal.sel_57(UserNum);
        }
        public DataTable sel_58(string DID, string UserName)
        {
            return dal.sel_58(DID, UserName);
        }
        public int Update_10(string Cname, string Authority, string Authoritymoney, string D_Content, string D_anno, DateTime Creatime, string ClassID, string Did, string UserName1)
        {
            return dal.Update_10(Cname, Authority, Authoritymoney, D_Content, D_anno, Creatime, ClassID, Did, UserName1);
        }
        #endregion
    }
}