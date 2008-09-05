using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Common;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Discuss : DbBase, IDiscuss
    {
        #region message_discussacti.aspx
        public DataTable sel(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName,SiteID from " + Pre + "sys_User where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Add(STDiscussActive DA)
        {
            string Sql = "Insert Into " + Pre + "user_DiscussActive(Activesubject,ActivePlace,ActiveExpense,Anum,ActivePlan,Contactmethod,Cutofftime,CreaTime,AId,UserName,ALabel,siteID) values(@Activesubject,@ActivePlace,@ActiveExpense,@Anum,@ActivePlan,@Contactmethod,@Cutofftime,@CreaTime,@AId,@UserName1,@ALabel,@siteID)";
            OleDbParameter[] parm = GetDiscussActive(DA);
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        private OleDbParameter[] GetDiscussActive(STDiscussActive DA)
        {
            OleDbParameter[] parm = new OleDbParameter[12];
            parm[0] = new OleDbParameter("@Activesubject", OleDbType.VarWChar, 50);
            parm[0].Value = DA.Activesubject;
            parm[1] = new OleDbParameter("@ActivePlace", OleDbType.VarWChar, 200);
            parm[1].Value = DA.ActivePlace;
            parm[2] = new OleDbParameter("@ActiveExpense", OleDbType.VarWChar, 50);
            parm[2].Value = DA.ActiveExpense;
            parm[3] = new OleDbParameter("@Anum", OleDbType.Integer, 4);
            parm[3].Value = DA.Anum;
            parm[4] = new OleDbParameter("@ActivePlan", OleDbType.VarWChar, 50);
            parm[4].Value = DA.ActivePlan;
            parm[5] = new OleDbParameter("@Contactmethod", OleDbType.VarWChar, 50);
            parm[5].Value = DA.Contactmethod;
            parm[6] = new OleDbParameter("@Cutofftime", OleDbType.Date);
            parm[6].Value = DA.Cutofftime;
            parm[7] = new OleDbParameter("@CreaTime", OleDbType.Date);
            parm[7].Value = DA.CreaTime;
            parm[8] = new OleDbParameter("@AId", OleDbType.VarWChar, 18);
            parm[8].Value = DA.AId;
            parm[9] = new OleDbParameter("@UserName1", OleDbType.VarWChar, 50);
            parm[9].Value = DA.UserName;
            parm[10] = new OleDbParameter("@ALabel", OleDbType.Integer, 4);
            parm[10].Value = DA.ALabel;
            parm[11] = new OleDbParameter("@siteID", OleDbType.VarWChar, 50);
            parm[11].Value = DA.siteID;
            return parm;
        }
        public string sel_1()
        {
            string Sql = "select AId from " + Pre + "user_DiscussActive";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
        }
        #endregion

        #region message_discussacti_list.aspx
        public DataTable sel_2()
        {
            string Sql = "select AId,UserNum from " + Pre + "user_DiscussActiveMember ";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public string sel_3(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select SiteID from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Delete(string ID, string SiteID)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@ID", ID), new OleDbParameter("@SiteID", SiteID) };
            string Sql = " delete " + Pre + "user_DiscussActive where AId=@ID and SiteID=@SiteID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region message_discussclass_add.aspx
        public DataTable sel_4()
        {
            string Sql = "select DcID from " + Pre + "User_DiscussClass";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add_1(string DcID, string Cname, string Content, string indexnumber, string SiteID)
        {
            OleDbParameter[] param = new OleDbParameter[5];
            param[0] = new OleDbParameter("@DcID", OleDbType.VarWChar, 50);
            param[0].Value = DcID;
            param[1] = new OleDbParameter("@Cname", OleDbType.VarWChar, 50);
            param[1].Value = Cname;
            param[2] = new OleDbParameter("@Content", OleDbType.VarWChar, 200);
            param[2].Value = Content;
            param[3] = new OleDbParameter("@indexnumber", OleDbType.VarWChar, 50);
            param[3].Value = indexnumber;
            param[4] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[4].Value = SiteID;
            string Sql = "insert into " + Pre + "User_DiscussClass(DcID,Cname,Content,indexnumber,SiteID) values(@DcID,@Cname,@Content,@indexnumber,@SiteID)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region message_discusssubclass_add.aspx
        public DataTable sel_5()
        {
            string Sql = "select DcID,Cname from " + Pre + "user_DiscussClass where indexnumber='0'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable getsClasslist(string ClassID)
        {
            string _ClassID = ClassID;
            if (ClassID == "")
                _ClassID = "";
            else
                _ClassID = " and indexnumber=@ClassID";
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string Sql = "select DcID,Cname from " + Pre + "user_DiscussClass where UserNum='" + Foosun.Global.Current.UserNum + "' " + _ClassID + " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public void getsClassDel(string ID)
        {
            OleDbParameter param = new OleDbParameter("@DcID", ID);
            string Sql = "delete from " + Pre + "user_DiscussClass where UserNum='" + Foosun.Global.Current.UserNum + "' and DcID=@DcID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        #endregion

        #region message_discussclass.aspx
        public int Delete_1(string ID)
        {
            OleDbParameter param = new OleDbParameter("@DcID", ID);
            string Sql = " delete " + Pre + "User_DiscussClass where DcID=@DcID and  SiteID=" + Foosun.Global.Current.SiteID + "";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region message_userdiscuss_list.aspx
        public DataTable sel_6()
        {
            string Sql = "select DisID,UserNum from " + Pre + "User_DiscussMember ";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Delete_2(string ID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", ID);
            string Sql = "delete " + Pre + "user_Discuss where DisID=@DisID and  SiteID=" + Foosun.Global.Current.SiteID + "";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region add_discussManage.aspx
        public int getDiscussTitle(string Title)
        { 
            OleDbParameter param = new OleDbParameter("@Cname",Title);
            string sql = "select count(id) from " + Pre + "user_Discuss where Cname=@Cname";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        public DataTable sel_49()
        {
            string Sql = "select DcID,Cname from " + Pre + "user_DiscussClass where indexnumber='0'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel_50(string provinces)
        {
            OleDbParameter param = new OleDbParameter("@indexnumber", provinces);
            string Sql = "select Cname,DcID from " + Pre + "user_DiscussClass where indexnumber=@indexnumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public DataTable sel_52(string UserGroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", UserGroupNumber);
            string Sql = "select GroupCreatNum,GroupSize,GroupPerNum,GroupTF from " + Pre + "user_Group where GroupNumber=@GroupNumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_53(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName,SiteID,cPoint,aPoint From " + Pre + "sys_User where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int sel_54(string um)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", um);
            string Sql = "select count(*) from " + Pre + "user_Discuss  where UserName=@UserNum";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public DataTable sel_55()
        {
            string Sql = "select cPointParam,aPointparam from " + Pre + "sys_PramUser";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Update_9(int cPoint2, int aPoint2, string UserNum)
        {
            string Sql = "update " + Pre + "sys_User set cPoint='" + cPoint2 + "',aPoint='" + aPoint2 + "' where UserNum='" + UserNum + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Add_12(STDiscuss DIS)
        {
            string Sql = "insert into " + Pre + "user_Discuss(DisID,Cname,Authority,Authoritymoney,UserName,Browsenumber,D_Content,D_anno,Creatime,ClassID,Fundwarehouse,GroupSize,GroupPerNum,SiteID) values(@DisID,@Cname,@Authority,@Authoritymoney,@UserName,0,@D_Content,@D_anno,@Creatime,@ClassID,@Fundwarehouse,@GroupSize,@GroupPerNum,@SiteID)";
            OleDbParameter[] parm = selDiscuss(DIS);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public DataTable sel_56()
        {
            string Sql = "select DisID from " + Pre + "user_Discuss";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        private OleDbParameter[] selDiscuss(STDiscuss DIS)
        {
            OleDbParameter[] parm = new OleDbParameter[13];
            parm[0] = new OleDbParameter("@DisID", OleDbType.VarWChar, 12);
            parm[0].Value = DIS.DisID;
            parm[1] = new OleDbParameter("@Cname", OleDbType.VarWChar, 100);
            parm[1].Value = DIS.Cname;
            parm[2] = new OleDbParameter("@Authority", OleDbType.VarWChar, 12);
            parm[2].Value = DIS.Authority;
            parm[3] = new OleDbParameter("@Authoritymoney", OleDbType.VarWChar, 12);
            parm[3].Value = DIS.Authoritymoney;
            parm[4] = new OleDbParameter("@UserName", OleDbType.VarWChar, 15);
            parm[4].Value = DIS.UserNames;
            parm[5] = new OleDbParameter("@D_Content", OleDbType.VarWChar, 200);
            parm[5].Value = DIS.D_Content;
            parm[6] = new OleDbParameter("@D_anno", OleDbType.VarWChar, 200);
            parm[6].Value = DIS.D_anno;
            parm[7] = new OleDbParameter("@Creatime", OleDbType.Date, 8);
            parm[7].Value = DIS.Creatimes;
            parm[8] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 50);
            parm[8].Value = DIS.ClassID;
            parm[9] = new OleDbParameter("@Fundwarehouse", OleDbType.VarWChar, 50);
            parm[9].Value = DIS.Fundwarehouse;
            parm[10] = new OleDbParameter("@GroupSize", OleDbType.Integer, 4);
            parm[10].Value = DIS.GroupSize;
            parm[11] = new OleDbParameter("@GroupPerNum", OleDbType.Integer, 4);
            parm[11].Value = DIS.GroupPerNum;
            parm[12] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            parm[12].Value = DIS.SiteID;
            return parm;
        }
        #endregion

        #region discuss_Manageadd.aspx
        public DataTable sel_7(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select iPoint,gPoint From " + Pre + "sys_User where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_8(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "select Authoritymoney from " + Pre + "User_Discuss where DisID=@DisID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_9(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "select Fundwarehouse from " + Pre + "user_Discuss where DisID=@DisID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update(int Authority2, int Authority3, string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "update " + Pre + "sys_User set iPoint=iPoint-" + Authority2 + ",gPoint=gPoint-" + Authority3 + " where UserNum=@UserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Add_2(string GhID, string UserNum, int Authority3, int Authority2, DateTime Creatime)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@GhID", OleDbType.VarWChar, 12);
            param[0].Value = GhID;
            param[1] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[1].Value = UserNum;

            string Sql = "insert into " + Pre + "User_Ghistory(GhID,UserNUM,Gpoint,iPoint,ghtype,Money,CreatTime) values(@GhID,@UserNum," + Authority3 + "," + Authority2 + ",0,0,'" + Creatime + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Update_1(string Fundwarehouse1, string DisID)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@Fundwarehouse", Fundwarehouse1), new OleDbParameter("@DisID", DisID) };
            string Sql = "update " + Pre + "user_Discuss set Fundwarehouse=@Fundwarehouse where DisID=@DisID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_10(string UserNum, string DisID)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@DisID", DisID) };
            string Sql = "select count(*) from " + Pre + "User_DiscussMember where UserNum=@UserNum and DisID=@DisID";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Add_3(string Member, string DisID, string UserNum, DateTime Creatime)
        {
            OleDbParameter[] param = new OleDbParameter[3];
            param[0] = new OleDbParameter("@Member",OleDbType.VarWChar,12);
            param[0].Value = Member;
            param[1] = new OleDbParameter("@DisID", OleDbType.VarWChar,12);
            param[1].Value = DisID;
            param[2] = new OleDbParameter("@UserNum" ,OleDbType.VarWChar,12);
            param[2].Value = UserNum;
            string Sql = "insert into " + Pre + "User_DiscussMember(Member,DisID,UserNum,Creatime) values(@Member,@DisID,@UserNum,'" + Creatime + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public DataTable sel_11()
        {
            string Sql = "select Member from " + Pre + "User_DiscussMember";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        #endregion

        #region discussacti_add.aspx
        public DataTable sel_12(string AId)
        {
            OleDbParameter param = new OleDbParameter("@AId", AId);
            string Sql = "select Cutofftime,Anum from " + Pre + "user_DiscussActive where AId=@AId";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int sel_13(string AId)
        {
            OleDbParameter param = new OleDbParameter("@AId", AId);
            string Sql = "select sum(ParticipationNum) from " + Pre + "user_DiscussActiveMember where AId=@AId";
            object dir = DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
            if (dir != null && dir != DBNull.Value)
            {
                return Convert.ToInt32(dir);
            }
            else { return 0; }
        }
        public int Add_4(string Telephone, int ParticipationNum, int isCompanion, string UserNum, string AIds, string PId, DateTime CreaTime)
        {
            OleDbParameter[] param = new OleDbParameter[7];
            param[0] = new OleDbParameter("@Telephone", OleDbType.VarWChar, 18);
            param[0].Value = Telephone;
            param[1] = new OleDbParameter("@ParticipationNum", OleDbType.Integer, 4);
            param[1].Value = ParticipationNum;
            param[2] = new OleDbParameter("@isCompanion", OleDbType.Integer, 4);
            param[2].Value = isCompanion;
            param[3] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 18);
            param[3].Value = UserNum;
            param[4] = new OleDbParameter("@AIds", OleDbType.VarWChar, 18);
            param[4].Value = AIds;
            param[5] = new OleDbParameter("@PId", OleDbType.VarWChar, 18);
            param[5].Value = PId;
            param[6] = new OleDbParameter("@CreaTime", OleDbType.Date);
            param[6].Value = CreaTime;
            //string Sql = "Insert Into " + Pre + "user_DiscussActiveMember(Telephone,ParticipationNum,isCompanion,UserNum,AId,PId,CreaTime) values(@Telephone,@ParticipationNum,@isCompanion,@UserNum,@AIds,@PId,@CreaTime)";
            string Sql = "Insert Into " + Pre + "user_DiscussActiveMember("+Database.getParam(param)+") values("+Database.getAParam(param)+")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public DataTable sel_14()
        {
            string Sql = "select PId from " + Pre + "user_DiscussActiveMember";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        #endregion

        #region discussacti_DC.aspx
        public DataTable sel_15(string Aid)
        {
            OleDbParameter param = new OleDbParameter("@Aid", Aid);
            string Sql = "select Activesubject,ActivePlace,ActiveExpense,Anum,ActivePlan,Contactmethod,Cutofftime,CreaTime,UserName from " + Pre + "user_DiscussActive where Aid=@Aid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussacti_list.aspx
        public DataTable sel_16()
        {
            string Sql = "select AId,UserNum from " + Pre + "user_DiscussActiveMember";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        #endregion

        #region discussacti_up.aspx
        public DataTable sel_17(string AId)
        {
            OleDbParameter param = new OleDbParameter("@Aid", AId);
            string Sql = "select Activesubject,ActivePlace,ActiveExpense,Anum,ActivePlan,Contactmethod,Cutofftime,ALabel from  " + Pre + "user_DiscussActive where AId=@Aid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update_2(string Activesubject, string ActivePlace, string ActiveExpense, int Anum, string ActivePlan, string Contactmethod, DateTime Cutofftime, DateTime CreaTime, int ALabel, string AIds)
        {
            OleDbParameter[] param = new OleDbParameter[10];
            param[0] = new OleDbParameter("@Activesubject", OleDbType.VarWChar, 50);
            param[0].Value = Activesubject;
            param[1] = new OleDbParameter("@ActivePlace", OleDbType.VarWChar, 50);
            param[1].Value = ActivePlace;
            param[2] = new OleDbParameter("@ActiveExpense", OleDbType.VarWChar, 50);
            param[2].Value = ActiveExpense;
            param[3] = new OleDbParameter("@Anum", OleDbType.Integer, 4);
            param[3].Value = Anum;
            param[4] = new OleDbParameter("@ActivePlan", OleDbType.VarWChar, 200);
            param[4].Value = ActivePlan;
            param[5] = new OleDbParameter("@Contactmethod", OleDbType.VarWChar, 50);
            param[5].Value = Contactmethod;
            param[6] = new OleDbParameter("@Cutofftime", OleDbType.Date,8);
            param[6].Value = Cutofftime;
            param[7] = new OleDbParameter("@CreaTime", OleDbType.Date,8);
            param[7].Value = CreaTime;
            param[8] = new OleDbParameter("@ALabel", OleDbType.Integer, 4);
            param[8].Value = ALabel;
            param[9] = new OleDbParameter("@AIds", OleDbType.VarWChar,18);
            param[9].Value = AIds;

            string Sql = "update " + Pre + "user_DiscussActive set "+Database.getModifyParam(param)+" where AId=@AIds";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussactiestablish_list.aspx

        public int Delete_3(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Aid", ID);
            string Sql = " delete " + Pre + "user_DiscussActiveMember where Aid=@Aid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete_4(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Aid", ID);
            string Sql = " delete " + Pre + "user_DiscussActive where Aid=@Aid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_64(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Aid", ID);
            string Sql = "select count(ID) from " + Pre + "user_DiscussActiveMember where Aid=@Aid";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussactijoin_list.aspx
        public int Delete_5(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PId", ID);
            string Sql = " delete " + Pre + "user_DiscussActiveMember where PId=@PId";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussManage_DC.aspx
        public void Update_3(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "update " + Pre + "User_Discuss set  Browsenumber=Browsenumber+1 where DisID=@DisID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public DataTable sel_19(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "select Cname,Authoritymoney,UserName,Browsenumber,D_Content,Creatime,ClassID from " + Pre + "User_Discuss where DisID=@DisID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_20(string DcID)
        {
            OleDbParameter param = new OleDbParameter("@DcID", DcID);
            string _str = "栏目不存在.";
            string Sql = "select Cname from " + Pre + "user_DiscussClass where DcID=@DcID";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _str = dt.Rows[0]["Cname"].ToString();
                }
                dt.Clear(); dt.Dispose();
            }
            return _str;
        }
        public string sel_21(string DcID, string indexnumber)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@DcID", DcID), new OleDbParameter("@indexnumber", indexnumber) };
            string _str = "栏目不存在.";
            string Sql = "select Cname from " + Pre + "user_DiscussClass where DcID=@DcID and indexnumber=@indexnumber";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _str = dt.Rows[0]["Cname"].ToString();
                }
                dt.Clear(); dt.Dispose();
            }
            return _str;
        }
        #endregion

        #region discussManageestablish_list.aspx
        public DataTable sel_22()
        {
            string Sql = "select DisID,UserNum from " + Pre + "User_DiscussMember";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Delete_6(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = " delete " + Pre + "user_DiscussMember where DisID=@DisID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete_7(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = " delete " + Pre + "user_Discuss where DisID=@DisID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_63(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "select count(ID) from " + Pre + "user_DiscussMember where DisID=@DisID";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussManagejoin_list.aspx
        public int Delete_8(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Member", ID);
            string Sql = " delete " + Pre + "user_DiscussMember where Member=@Member and UserNum='" + Foosun.Global.Current.UserNum + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussphoto.aspx
        public string sel_60(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select UserName From " + Pre + "sys_User where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_61(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "Select PhotoalbumName From " + Pre + "user_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_62(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoID", ID);
            string Sql = "select UserNum from " + Pre + "User_Photo where PhotoID=@PhotoID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Delete_16(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoID", ID);
            string Sql = "delete " + Pre + "User_Photo  where PhotoID=@PhotoID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussphoto_add.aspx
        public DataTable sel_23(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "Select PhotoalbumName,PhotoalbumID From " + Pre + "User_Photoalbum where isDisPhotoalbum=1 And DisID=@DisID and UserName='" + Foosun.Global.Current.UserNum + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussphoto_up.aspx
        public DataTable sel_24(string PhotoID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoID", PhotoID);
            string Sql = "select PhotoName,PhotoalbumID,PhotoContent,PhotoUrl,UserNum  from " + Pre + "User_Photo where PhotoID=@PhotoID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_25(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "Select PhotoalbumName,PhotoalbumID From " + Pre + "User_Photoalbum where Mid(PhotoalbumJurisdiction,1,1) = '1' And DisID=@DisID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_26(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "Select PhotoalbumName From " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #region discussPhotoalbum.aspx
        public DataTable sel_27(string DisIDs)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisIDs);
            string Sql = "Select ClassName,ClassID From " + Pre + "user_PhotoalbumClass where isDisclass=1 and DisID=@DisID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussPhotoalbum_up.aspx
        public string sel_28(string PhotoalbumIDsa)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumIDsa);
            string Sql = "select UserName  from " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #region discussPhotoalbumlist.aspx
        public DataTable sel_29(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "select UserNum from " + Pre + "User_DiscussMember where DisID=@DisID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_30(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "select UserName  from " + Pre + "User_Discuss  where DisID=@DisID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_31(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", ID);
            string Sql = "select PhotoalbumUrl,UserName  from " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Delete_9(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", ID);
            string Sql = " delete " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete_10(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", ID);
            string Sql = " delete " + Pre + "User_Photo where PhotoalbumID=@PhotoalbumID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussphotoclass.aspx
        public int sel_32(string UserNum, string ID)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@ClassID", ID) };
            string Sql = "select count(UserName) from " + Pre + "User_PhotoalbumClass where UserName=@UserNum and ClassID=@ClassID";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        #endregion

        #region discusssubclass_add.aspx
        public DataTable sel_33()
        {
            string Sql = "select DcID,Cname from " + Pre + "user_DiscussClass where indexnumber='0'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add_5(string DcID, string Cname, string Content, string indexnumber)
        {
            OleDbParameter[] param = new OleDbParameter[4] ;
            param[0] = new OleDbParameter("@DcID", OleDbType.VarWChar, 50);
            param[0].Value  = DcID;
            param[1] =  new OleDbParameter("@Cname", OleDbType.VarWChar,50);
            param[1].Value  = Cname;
            param[2] =  new OleDbParameter("@Content", OleDbType.VarWChar,200);
            param[2].Value  = Content;
            param[3] =  new OleDbParameter("@indexnumber", OleDbType.VarWChar,50);
            param[3].Value  = indexnumber;
            string Sql = "insert into " + Pre + "User_DiscussClass("+Database.getParam(param)+",SiteID,UserNum) values("+Database.getAParam(param)+",'" + Foosun.Global.Current.SiteID + "','" + Foosun.Global.Current.UserNum + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussTopi_add.aspx
        public string sel_34(string DisIDs)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisIDs);
            string Sql = "select Authority from " + Pre + "User_Discuss where DisID=@DisID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_35()
        {
            string Sql = "select DtID from " + Pre + "User_DiscussTopic";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add_6(string DtID, string Title, string Content, int source, string DtUrl, string UserNum, DateTime creatTime, DateTime voteTime, string DisID)
        {
            OleDbParameter[] param = new OleDbParameter[9];
            param[0] = new OleDbParameter("@DtID", OleDbType.VarWChar, 12);
            param[0].Value = DtID;
            param[1] = new OleDbParameter("@Title", OleDbType.VarWChar, 200);
            param[1].Value = Title;
            param[2] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[2].Value = Content;
            param[3] = new OleDbParameter("@source", OleDbType.Integer, 4);
            param[3].Value = source;
            param[4] = new OleDbParameter("@DtUrl", OleDbType.VarWChar, 50);
            param[4].Value = DtUrl;
            param[5] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[5].Value = UserNum;
            param[6] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[6].Value = creatTime;
            param[7] = new OleDbParameter("@voteTime", OleDbType.Date, 8);
            param[7].Value = voteTime;
            param[8] = new OleDbParameter("@DisID", OleDbType.VarWChar, 18);
            param[8].Value = DisID;
            string Sql = "insert into " + Pre + "User_DiscussTopic("+Database.getParam(param)+",ParentID,VoteTF) values("+Database.getAParam(param)+",'0',0)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussTopi_ballot.aspx
        public int Add_7(string DtID, string Title, string Content, string UserNum, DateTime creatTime, DateTime voteTime, string DisID)
        {
            OleDbParameter[] param = new OleDbParameter[7];
            param[0] = new OleDbParameter("@DtID", OleDbType.VarWChar, 12);
            param[0].Value = DtID;
            param[1] = new OleDbParameter("@Title", OleDbType.VarWChar, 200);
            param[1].Value = Title;
            param[2] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[2].Value = Content;
            param[3] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[3].Value = UserNum;
            param[4] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[4].Value = creatTime;
            param[5] = new OleDbParameter("@voteTime", OleDbType.Date, 8);
            param[5].Value = voteTime;
            param[6] = new OleDbParameter("@DisID", OleDbType.VarWChar, 18);
            param[6].Value = DisID;
            string Sql = "insert into " + Pre + "User_DiscussTopic("+Database.getParam(param)+",source,ParentID,VoteTF) values("+Database.getAParam(param)+",0,'0',1)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_36(string VoteID)
        {
            OleDbParameter param = new OleDbParameter("@VoteID", VoteID);
            string Sql = "select count(*) from " + Pre + "user_Vote where VoteID=@VoteID";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public bool Add_8(string DtID, string VoteID, string votegenre, string Voteitem)
        {
            OleDbParameter[] param = new OleDbParameter[4];
            param[0] = new OleDbParameter("@DtID", OleDbType.VarWChar, 18);
            param[0].Value = DtID;
            param[1] = new OleDbParameter("@VoteID", OleDbType.VarWChar, 18);
            param[1].Value = VoteID;
            param[2] = new OleDbParameter("@votegenre", OleDbType.Integer, 1);
            param[2].Value = votegenre;
            param[3] = new OleDbParameter("@Voteitem", OleDbType.VarWChar, 50);
            param[3].Value = Voteitem;

            string Sql = "insert into " + Pre + "user_Vote("+Database.getParam(param)+",VoteNum) values("+Database.getAParam(param)+",0)";
            if (Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param)) != 0)
                return true;
            else
                return false;
        }
        #endregion

        #region discussTopi_commentary.aspx
        public DataTable sel_37(string Dtd)
        {
            OleDbParameter param = new OleDbParameter("@DtID", Dtd);
            string Sql = "select VoteTF,voteTime from " + Pre + "User_DiscussTopic  where  DtID=@DtID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }


        public void Add_9(Foosun.Model.STADDDiscuss uc)
        {
            OleDbParameter[] parm = Add_9_Parameters(uc); 
            string Sql = "insert into " + Pre + "User_DiscussTopic(" + Database.getParam(parm) + ") values("+Database.getAParam(parm)+")";
            
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        private OleDbParameter[] Add_9_Parameters(Foosun.Model.STADDDiscuss uc1)
        {
            OleDbParameter[] param = new OleDbParameter[7];
            param[0] = new OleDbParameter("@DtID", OleDbType.VarWChar, 12);
            param[0].Value = uc1.DtID;
            param[1] = new OleDbParameter("@Title", OleDbType.VarWChar, 200);
            param[1].Value = uc1.Title;
            param[2] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[2].Value = uc1.Content;
            param[3] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[3].Value = uc1.UserNum;
            param[4] = new OleDbParameter("@ParentID", OleDbType.VarWChar, 12);
            param[4].Value = uc1.ParentID;
            param[5] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[5].Value = uc1.creatTime;
            param[6] = new OleDbParameter("@DisID", OleDbType.VarWChar, 18);
            param[6].Value = uc1.DisID;
            return param;
        }

        public DataTable sel_38(string DtID)
        {
            OleDbParameter param = new OleDbParameter("@DtID", DtID);
            string Sql = "select VoteID,VoteNum,Voteitem,votegenre,CreaTime from " + Pre + "User_Vote  where  DtID=@DtID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update_4(DateTime tm, string hidd)
        {
            string Sql = "update " + Pre + "User_Vote set CreaTime='" + tm + "' where VoteID in (" + hidd + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public string sel_39(string VoteID)
        {
            OleDbParameter param = new OleDbParameter("@VoteID", VoteID);
            string Sql = "select VoteNum from " + Pre + "User_Vote  where  VoteID=@VoteID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_5(int VoteNumsel, string VoteID)
        {
            OleDbParameter param = new OleDbParameter("@VoteID", VoteID);
            string Sql = "update " + Pre + "User_Vote set VoteNum='" + VoteNumsel + "' where VoteID=@VoteID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Update_6(DateTime tm, string VoteID)
        {
            OleDbParameter param = new OleDbParameter("@VoteID", VoteID);
            string Sql = "update " + Pre + "User_Vote set CreaTime='" + tm + "' where VoteID=@VoteID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public DataTable sel_46(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select UserName,iPoint,gPoint,cPoint,ePoint,aPoint,RegTime,UserFace,userFacesize From " + Pre + "sys_User where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_47(string DisIDs)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisIDs);
            string Sql = "select Authority from " + Pre + "User_Discuss where DisID=@DisID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public DataTable sel_48(string DtID)
        {
            OleDbParameter param = new OleDbParameter("@DtID", DtID);
            string Sql = "Select DtID,title,Content,creatTime,UserNum From " + Pre + "User_DiscussTopic where DtID=@DtID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        /// <summary>
        /// 编辑帖子得到信息
        /// </summary>
        /// <param name="DtID"></param>
        /// <returns></returns>
        public DataTable getTopicinfo(string DtID)
        {
            OleDbParameter param = new OleDbParameter("@DtID", DtID);
            string Sql = "Select DtID,title,Content,creatTime,UserNum From " + Pre + "User_DiscussTopic where DtID=@DtID and UserNum='" + Foosun.Global.Current.UserNum + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public void updateTopicDtID(string DtID, string title, string content)
        {
            OleDbParameter[] param = new OleDbParameter[3];
            param[0] = new OleDbParameter("@DtID", OleDbType.VarWChar, 12);
            param[0].Value = DtID;
            param[1] = new OleDbParameter("@title", OleDbType.VarWChar, 200);
            param[1].Value = title;
            param[2] = new OleDbParameter("@content", OleDbType.VarWChar);
            param[2].Value = content;
            string Sql = "update " + Pre + "User_DiscussTopic set title=@title,content=@content where DtID=@DtID and UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussTopi_del.aspx
        public int sel_59(string ID)
        {
            object result;
            OleDbParameter param = new OleDbParameter("@DtID", ID);
            string Sql = "select VoteTF from " + Pre + "User_DiscussTopic where DtID=@DtID ";
            result=DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
            try
            {
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
        }
        public int Delete_13(string ID)
        {
            OleDbParameter param = new OleDbParameter("@DtID", ID);
            string Sql = "delete " + Pre + "User_DiscussTopic where DtID=@DtID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete_14(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ParentID", ID);
            string Sql = "delete " + Pre + "User_DiscussTopic where ParentID=@ParentID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int Delete_15(string ID)
        {
            OleDbParameter param = new OleDbParameter("@DtID", ID);
            string Sql = "delete " + Pre + "User_Vote  where DtID=@DtID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussTopi_list.aspx
        public DataTable sel_40(string DisIDs)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisIDs);
            string Sql = "select UserNum from " + Pre + "User_DiscussTopic where DisID=@DisID and UserNum='" + Foosun.Global.Current.UserNum + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_41(string DisIDs)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisIDs);
            string Sql = "select D_anno from " + Pre + "User_Discuss where DisID=@DisID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_42(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Delete_11(string DtID)
        {
            OleDbParameter param = new OleDbParameter("@DtID", DtID);
            string Sql = "delete " + Pre + "User_DiscussTopic where DtID=@DtID and UserNum='" + Foosun.Global.Current.UserNum + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete_12(string ParentID)
        {
            OleDbParameter param = new OleDbParameter("@ParentID", ParentID);
            string Sql = "delete " + Pre + "User_DiscussTopic where ParentID=@ParentID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region discussTopi_view.aspx
        public int sel_43(string DtIDa)
        {
            OleDbParameter param = new OleDbParameter("@DtID", DtIDa);
            string Sql = "select sum(VoteNum) from " + Pre + "User_Vote where DtID=@DtID";
            object dir = DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
            if (dir != null && dir != DBNull.Value)
            {
                return Convert.ToInt32(dir);
            }
            else { return 0; }
        }
        #endregion

        #region disFundwarehouse.aspx
        public int Add_10(string GhID, string UserNum, int gPoint1, int iPoint1, DateTime Creatime)
        {
            //<--wjl
            OleDbParameter[] param = new OleDbParameter[5];
            //wjl-->
            param[0] = new OleDbParameter("@GhID", OleDbType.VarWChar, 12);
            param[0].Value = GhID;
            param[1] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[1].Value = UserNum;
            param[2] = new OleDbParameter("@gPoint1", OleDbType.Integer, 4);
            param[2].Value = gPoint1;
            param[3] = new OleDbParameter("@iPoint1", OleDbType.Integer, 4);
            param[3].Value = iPoint1;

            //<--修改者：吴静岚 时间：2008-06-24 解决会员捐献错误
            param[4] = new OleDbParameter("@Creatime", OleDbType.Date, 8);
            param[4].Value = Creatime;

            string Sql = "insert into [" + Pre + "User_Ghistory] ([GhID],[UserNUM],[Gpoint],[iPoint],[ghtype],[Money],[CreatTime]) values(@GhID,@UserNum,@gPoint1,@iPoint1,0,0,@Creatime)";
            //wjl-->
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public DataTable sel_44(string DisID)
        {
            OleDbParameter param = new OleDbParameter("@DisID", DisID);
            string Sql = "select Fundwarehouse from " + Pre + "user_Discuss where DisID=@DisID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_45(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select iPoint,gPoint From " + Pre + "sys_User where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update_7(int iPoint1, int gPoint1, string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "update " + Pre + "sys_User set iPoint=iPoint-" + iPoint1 + ",gPoint=gPoint-" + gPoint1 + " where UserNum=@UserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Update_8(string Fundwarehouse1, string DisID)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@Fundwarehouse", OleDbType.VarWChar, 50);
            param[0].Value = Fundwarehouse1;
            param[1] = new OleDbParameter("@DisID", OleDbType.VarWChar, 12);
            param[1].Value = DisID;

            string Sql = "update " + Pre + "user_Discuss set Fundwarehouse=@Fundwarehouse where DisID=@DisID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Add_11(string Fundwarehouse2, string UserNum, string DisID, DateTime Creatime)
        {
            OleDbParameter[] param = new OleDbParameter[4];
            param[0] = new OleDbParameter("@Fundwarehouse2", OleDbType.VarWChar, 50);
            param[0].Value = Fundwarehouse2;
            param[1] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 12);
            param[1].Value = UserNum;
            param[2] = new OleDbParameter("@DisID", OleDbType.VarWChar, 12);
            param[2].Value = DisID;
            param[3] = new OleDbParameter("@Creatime", OleDbType.Date, 8);
            param[3].Value = Creatime;
            string Sql = "insert into " + Pre + "User_DiscussContribute(Membermoney,Member,DisID,Creatime) values(@Fundwarehouse2,@UserNum,@DisID,@Creatime)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region up_discussManage.aspx
        public string sel_57(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_58(string DID, string UserName)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@DisID", DID), new OleDbParameter("@UserName", UserName) };
            string Sql = "select Cname,Authority,Authoritymoney,D_Content,D_anno,ClassID from " + Pre + "user_Discuss where DisID=@DisID and UserName=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update_10(string Cname, string Authority, string Authoritymoney, string D_Content, string D_anno, DateTime Creatime, string ClassID, string Did, string UserName1)
        {
            OleDbParameter[] param = new OleDbParameter[9];
            param[0] = new OleDbParameter("@Cname",OleDbType.VarWChar,100);
            param[0].Value = Cname;
            param[1] = new OleDbParameter("@Authority",OleDbType.VarWChar,12);
            param[1].Value = Authority;
            param[2] = new OleDbParameter("@Authoritymoney",OleDbType.VarWChar,12);
            param[2].Value = Authoritymoney;
            param[3] = new OleDbParameter("@D_Content",OleDbType.VarWChar,200);
            param[3].Value = D_Content;
            param[4] = new OleDbParameter("@D_anno",OleDbType.VarWChar,200);
            param[4].Value = D_anno;
            param[5] = new OleDbParameter("@Creatime",OleDbType.Date,8);
            param[5].Value = Creatime;
            param[6] = new OleDbParameter("@ClassID",OleDbType.VarWChar,50);
            param[6].Value = ClassID;
            param[7] = new OleDbParameter("@Did",OleDbType.VarWChar,50);
            param[7].Value = Did;
            param[8] = new OleDbParameter("@UserName1",OleDbType.VarWChar,15);
            param[8].Value = UserName1;
            string Sql = "update " + Pre + "user_Discuss set Cname=@Cname,Authority=@Authority,Authoritymoney=@Authoritymoney,D_Content=@D_Content,D_anno=@D_anno,Creatime=@Creatime,ClassID=@ClassID where DisID=@Did and UserName=@UserName1";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion
    }
}

