///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
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
    public class Info : DbBase, IInfo
    {
        #region announce.aspx
        public string sel_1(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select SiteID from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion 

        #region ChangePassword.aspx
        public int sel_2(string UserNum, string UserPassword)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@UserPassword", UserPassword) };
            int flg = 0;
            string Sql = "select ID from " + Pre + "sys_User where UserNum=@UserNum and UserPassword=@UserPassword";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, Database.getNewParam(param, Database.getSqlParam(Sql)));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    flg = 1;
                }
                dt.Clear(); dt.Dispose();
            }
            return flg;
        }
        public int Update(string UserPassword, string UserNum)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            
            param[0] = new OleDbParameter("@UserPassword", OleDbType.VarWChar, 32);
            param[0].Value = UserPassword;
            param[1] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[1].Value = UserNum;
            string Sql = "update " + Pre + "sys_User set UserPassword=@UserPassword where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, Database.getSqlParam(Sql))));
        }
        #endregion

        #region collection.aspx
        public int Delete(string FID)
        {
            OleDbParameter param = new OleDbParameter("@FID", FID);
            string Sql = "delete " + Pre + "API_Faviate where FID=@FID And UserNum='" + Foosun.Global.Current.UserNum + "'";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        public bool addTo(string NewsID,int ChID)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@NewsID", OleDbType.VarWChar, 15);
            param[0].Value = NewsID;
            param[1] = new OleDbParameter("@ChID", OleDbType.Integer, 4);
            param[1].Value = ChID;
            string gsql = "select count(id) from " + Pre + "API_Faviate where FID=@NewsID and ChID=@ChID And UserNum='" + Foosun.Global.Current.UserNum + "'";
            int i_Count = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, gsql, Database.getNewParam(param, Database.getSqlParam(gsql))));
            if (i_Count == 0)
            {
                string Sql = "insert " + Pre + "API_Faviate(FID,UserNum,CreatTime,APIID,DataLib,ChID) values(@NewsID,'" + Foosun.Global.Current.UserNum + "','" + DateTime.Now + "','0','',@ChID)";
                DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, Database.getSqlParam(Sql)));
                return true;
            }
            return false;
        }
        #endregion

        #region Exchange.aspx
        public string sel_3(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select UserGroupNumber From " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_4(string UserGroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", UserGroupNumber);
            string Sql = "Select GIChange From " + Pre + "user_Group  where GroupNumber=@GroupNumber";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_5(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select UserGroupNumber,iPoint,gPoint From " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_6(string UserGroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", UserGroupNumber);
            string Sql = "Select GTChageRate From " + Pre + "user_Group  where GroupNumber=@GroupNumber";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update1(int ipoint2, int gpoint2, string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "update " + Pre + "sys_user set iPoint='" + ipoint2 + "',gPoint='" + gpoint2 + "' where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public int Add(STGhistory Gh, int ghtype, string UserNum, string content)
        {
            string Sql = "insert into " + Pre + "User_Ghistory(GhID,ghtype,Gpoint,iPoint,[Money],CreatTime,UserNUM,gtype,content) values(@GhID," + ghtype + ",@Gpoint,@iPoint,0,@CreatTime,@UserNum,3,@content)";
            OleDbParameter[] parm = GetGhistory(Gh);
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 2);
            parm[i_length] = new OleDbParameter("@UserNum", UserNum);
            parm[i_length + 1] = new OleDbParameter("@content", content);
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(parm, Database.getSqlParam(Sql)));
        }
        private OleDbParameter[] GetGhistory(STGhistory Gh)
        {
            OleDbParameter[] parm = new OleDbParameter[4];
            parm[0] = new OleDbParameter("@GhID", OleDbType.VarWChar, 50);
            parm[0].Value = Rand.Number(12);
            parm[1] = new OleDbParameter("@Gpoint", OleDbType.VarWChar, 50);
            parm[1].Value = Gh.Gpoint;
            parm[2] = new OleDbParameter("@iPoint", OleDbType.VarWChar, 50);
            parm[2].Value = Gh.iPoint;
            parm[3] = new OleDbParameter("@CreatTime", OleDbType.Date);
            parm[3].Value = DateTime.Now;
            return parm;
        }
        #endregion

        #region getPassword.aspx
        public DataTable sel_7(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "select UserName,PassQuestion from " + Pre + "sys_User where UserName=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_8(string u_un)
        {
            OleDbParameter param = new OleDbParameter("@UserName", u_un);
            string Sql = "select PassKey,Email from " + Pre + "sys_User where UserName=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update2(string UserPassword, string UserName)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            
            param[0] = new OleDbParameter("@UserPassword", OleDbType.VarWChar, 32);
            param[0].Value = UserPassword;
            param[1] = new OleDbParameter("@UserName", OleDbType.VarWChar, 15);
            param[1].Value = UserName;
            string Sql = "update " + Pre + "sys_User set UserPassword=@UserPassword where UserName=@UserName";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region getPoint.aspx
        public DataTable sel_9(string Number)
        {
            OleDbParameter param = new OleDbParameter("@CardNumber", Number);
            string Sql = "Select CardNumber,CardPassWord,[Money],TimeOutDate,isUse,isLock,isBuy,Point From " + Pre + "user_Card  where CardNumber=@CardNumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_10(string cnm)
        {
            OleDbParameter param = new OleDbParameter("@CardNumber", cnm);
            string Sql = "Select [Money],Point From " + Pre + "User_Card where CardNumber=@CardNumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public int sel_11(string cnm)
        {
            OleDbParameter param = new OleDbParameter("@CardNumber", cnm);
            int flg = 0;
            string Sql = "Select isUse From " + Pre + "user_Card where CardNumber=@CardNumber";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                flg = int.Parse(dt.Rows[0]["isUse"].ToString());
                dt.Clear(); dt.Dispose();
            }
            return flg;
        }

        public int sel_12()
        {
            int flg = 0;
            string Sql = "select GhClass from " + Pre + "sys_PramUser";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                flg = int.Parse(dt.Rows[0]["GhClass"].ToString());
                dt.Clear(); dt.Dispose();
            }
            return flg;
        }

        public int Add1(string GhID, string UserNum, int Gpoint, int Money, DateTime CreatTime, string content)
        {
            OleDbParameter[] param = new OleDbParameter[3];
            param[0] = new OleDbParameter("@GhID", OleDbType.VarWChar, 12);
            param[0].Value = GhID;
            param[1] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[1].Value = UserNum;
            param[2] = new OleDbParameter("@content", OleDbType.VarWChar);
            param[2].Value = content;

            string Sql = "insert into " + Pre + "User_Ghistory(GhID,UserNUM,Gpoint,ghtype,[Money],CreatTime,gtype,content) values(@GhID,@UserNum," + Gpoint + ",1," + Money + ",'" + CreatTime + "',2,@content)";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "GhID,UserNum,content"));
        }
        public int Update3(string UserNum, string cnm)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[0].Value = UserNum;
            param[1] = new OleDbParameter("@CardNumber", OleDbType.VarWChar, 30);
            param[1].Value = cnm;

            string Sql = "update " + Pre + "user_Card set isUse=1,UserNum=@UserNum where CardNumber=@CardNumber";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "UserNum,CardNumber"));
        }
        public int Update4(int Money1, string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "update " + Pre + "sys_User set gPoint=gPoint+" + Money1 + " where UserNum=@UserNum";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Update5(int points, string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "update " + Pre + "sys_User set iPoint=iPoint+" + points + " where UserNum=@UserNum";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region history.aspx
        public int Delete1(string ID)
        {
            OleDbParameter param = new OleDbParameter("@GhID", ID);
            string Sql = "delete " + Pre + "user_Ghistory  where  GhID=@GhID";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_13(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_user where  UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #region mycom.aspx
        public DataTable sel_14()
        {
            string Sql = "select API_ID,API_Name from " + Pre + "api_Ident";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public string sel_15(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserGroupNumber from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_16(string GroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", GroupNumber);
            string Sql = "select TopTitle,GoodTitle,CheckTtile,OCTF from " + Pre + "user_Group where GroupNumber=@GroupNumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_17(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserGroupNumber,SiteID from " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_18(string UserGroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", UserGroupNumber);
            string Sql = "select DelSelfTitle,DelOTitle,EditSelfTitle,EditOtitle,ReadTitle from " + Pre + "user_Group where GroupNumber=@GroupNumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable GetPage(string title, string Um, string dtm1, string dtm2, string isCheck, string islock, string SiteID, string UserNum, int DelOTitle, int EditOtitle, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string QSQL = "";
            if (title != "" && title != null)
            {
                QSQL = " and Title like '%" + title + "%'";
            }
            if (dtm1 != "" && dtm1 != null && dtm2 != "" && dtm2 != null)
            {
                DateTime dtms1 = DateTime.Parse(dtm1);
                DateTime dtms2 = DateTime.Parse(dtm2);
                QSQL += " and creatTime >= '" + dtms1 + "' and  creatTime <= '" + dtms2 + "'";
            }
            if (isCheck != "" && isCheck != null && isCheck != "0")
            {
                //int isChecks = 0;
                //if (isCheck == "1")
                //{
                //    isChecks = 0;
                //}
                //else
                //{
                //    isChecks = 1;
                //}
                QSQL += " and isCheck=@isCheck";
            }
            if (islock != "" && islock != null && islock != "0")
            {
                int islocks = 0;
                if (islock == "1")
                {
                    islocks = 0;
                }
                else
                {
                    islocks = 1;
                }
                QSQL += " and islock = '" + islocks + "'";
            }
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@SiteID", SiteID) };
            string sl = null;
            if (UserNum != "")
            {
                sl = "" + Pre + "api_commentary  where  UserNum=@UserNum and SiteID=@SiteID " + QSQL + "";
            }
            else
            {
                sl = "" + Pre + "api_commentary where SiteID=@SiteID " + QSQL + "";
            }
            string AllFields = "Commid,Title,InfoID,APIID,creatTime,isCheck,UserNum,islock,OrderID,GoodTitle,Content,Datalib";
            string Condition = sl;
            string IndexField = "Id";
            string OrderFields = "order by OrderID Desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, Database.getNewParam(param, Database.getSqlParam(Condition)));
        }
        public int Delete2(string Commid)
        {
            OleDbParameter param = new OleDbParameter("@Commid", Commid);
            string Sql = "delete " + Pre + "api_commentary  where Commid=@Commid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region mycom_Look.aspx
        public DataTable sel_19(string Commid)
        {
            OleDbParameter param = new OleDbParameter("@Commid", Commid);
            string Sql = "select Title,Content from " + Pre + "api_commentary where Commid=@Commid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        #endregion

        #region mycom_up.aspx
        public int Update6(string Title, string Contents, DateTime CreatTime, string Commid,int islock)
        {
            OleDbParameter[] param = new OleDbParameter[5];
            param[0] = new OleDbParameter("@Title", OleDbType.VarWChar, 200);
            param[0].Value = "";
            param[1] = new OleDbParameter("@Contents", OleDbType.VarWChar, 200);
            param[1].Value = Contents;
            
            param[2] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            param[2].Value = CreatTime;
            param[3] = new OleDbParameter("@Commid", OleDbType.VarWChar, 12);
            param[3].Value = Commid;
            string Tmp = "";
            if (islock != 2)
            {
                Tmp = ",islock=" + islock + "";
            }
            string Sql = "update " + Pre + "api_commentary set Title=@Title,Content=@Contents,creatTime=@creatTime " + Tmp + " where Commid=@Commid";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param,Database.getSqlParam(Sql)));
        }
        #endregion

        #region pointhistory.aspx
        public DataTable GetPagepoi(string typep, string UM, string sle_NUM, string SiteID, string UserNum, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string gtype = string.Empty;
            string gtypes = string.Empty;
            if (typep != "0" && typep != null)
            {
                gtype = "and gtype=@gtype";
            }
            if (typep == "8")
            {
                gtypes = "and ghtype=1";
            }
            if (typep == "9")
            {
                gtypes = "and ghtype=0";
            }
            string UserNumstr = "";
            if (UserNum != "" && UserNum != null)
            {
                UserNumstr = " and UserNum=@UserNum";
            }
            string sel_UM = string.Empty;
            if (UM != string.Empty)
            {
                if (sle_NUM != string.Empty)
                {
                    sel_UM = " and UserNUM=@sle_NUM";
                }
                if (typep == "8" || typep == "9")
                {
                    gtype = null;
                }
                else
                {
                    gtypes = null;
                }
            }
            string siteID1 = "";
            if (SiteID != "" && SiteID != "0" && SiteID != null)
            {
                if (Foosun.Global.Current.SiteID == "0")
                {
                    siteID1 = " and SiteID='" + SiteID + "'";
                }
                else
                {
                    siteID1 = " and SiteID='" + Foosun.Global.Current.SiteID + "'";
                }
            }
            else
            {
                siteID1 = " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            }
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@gtype", int.Parse(typep)), new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@sle_NUM", sle_NUM) };
            string AllFields = "GhID,ghtype,Gpoint,iPoint,[Money],CreatTime,UserNUM,gtype,content";
            string Condition = "" + Pre + "user_Ghistory where 1=1 " + gtype + sel_UM + siteID1 + gtypes + UserNumstr + "";
            string IndexField = "id";
            string OrderFields = "order by id";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, Database.getNewParam(param, Database.getSqlParam(Condition)));
        }
        public DataTable sel_20(string UM)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UM);
            string Sql = "select UserNum from " + Pre + "sys_user where UserName=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_21(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Delete3(string GhID)
        {
            OleDbParameter param = new OleDbParameter("@GhID", GhID);
            string Sql = "delete " + Pre + "user_Ghistory  where  GhID=@GhID " + Foosun.Common.Public.getSessionStr() + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region 前台友情连接
        public DataTable IsOpen()
        {
            string Str_Satrt_Sql = "Select IsOpen From " + Pre + "friend_pram";
            return DbHelper.ExecuteTable(CommandType.Text, Str_Satrt_Sql, null);
        }
        public DataTable ClassName_Click(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string Class_Sql = "Select ClassCName From " + Pre + "friend_class where ClassID=@ClassID";
            return DbHelper.ExecuteTable(CommandType.Text, Class_Sql, param);
        }
        public DataTable ClassInfo()
        {
            string Str_SQL = "Select ClassID,ClassCName,ParentID From " + Pre + "friend_class";
            return DbHelper.ExecuteTable(CommandType.Text, Str_SQL, null);
        }
        public DataTable StartUserC()
        {
            string Str_FriSql = "Select UserNum,Email From " + Pre + "sys_User where UserNum='" + Foosun.Global.Current.UserNum + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Str_FriSql, null);
        }
        public DataTable PramValue()
        {
            string Fri_Pram_Sql = "Select Content From " + Pre + "friend_pram";
            return DbHelper.ExecuteTable(CommandType.Text, Fri_Pram_Sql, null);
        }
        public int ISExitNamee(string Str_Name)
        {
            OleDbParameter param = new OleDbParameter("@Name", Str_Name);
            string Str_CheckSql = "Select count(Name) From " + Pre + "friend_link Where Name=@Name";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Str_CheckSql, param);
        }
        public int SaveLink(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, string Str_Author, string Str_Mail, string Str_ContentFor)
        {
            OleDbParameter[] param = new OleDbParameter[13];
            
            param[0] = new OleDbParameter("@Name", OleDbType.VarWChar, 50);
            param[0].Value = Str_Name;
            param[1] = new OleDbParameter("@Type", OleDbType.Integer, 1);
            param[1].Value = Convert.ToInt32(Str_Type);
            param[2] = new OleDbParameter("@Url", OleDbType.VarWChar, 250);
            param[2].Value = Str_Url;
            param[3] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[3].Value = Str_Content;
            param[4] = new OleDbParameter("@PicUrl", OleDbType.VarWChar, 250);
            param[4].Value = Str_PicUrl;
            param[5] = new OleDbParameter("@Lock", OleDbType.Integer);
            param[5].Value = 1;
            param[6] = new OleDbParameter("@IsUser", OleDbType.Integer);
            param[6].Value = 1;
            param[7] = new OleDbParameter("@Author", OleDbType.VarWChar, 50);
            param[7].Value = Str_Author;
            param[8] = new OleDbParameter("@Mail", OleDbType.VarWChar, 150);
            param[8].Value = Str_Mail;
            param[9] = new OleDbParameter("@ContentFor", OleDbType.VarWChar);
            param[9].Value = Str_ContentFor;
            param[10] = new OleDbParameter("@isAdmin", OleDbType.Integer);
            param[10].Value = 0;
            param[11] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
            param[11].Value = Str_Class;
            param[12] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[12].Value = Foosun.Global.Current.SiteID;


            string Str_InSql = "Insert into " + Pre + "friend_link (" + Database.getParam(param) + ") Values(" + Database.getAParam(param) + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, param);
        }

        public void delf(string id)
        {
            OleDbParameter param = new OleDbParameter("@ID", id);
            string Str_InSql = "delete from " + Pre + "friend_link where Author='" + Foosun.Global.Current.UserNum + "' and ID=@ID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, param);
        }

        public DataTable getflist(int num, string uid)
        {
            OleDbParameter param = new OleDbParameter("@Author", uid);
            string Str_InSql = "select Name,Url,Content,PicUrl,Author,LinkContent from " + Pre + "friend_link where Author=@Author and Type=" + num + "";
            return DbHelper.ExecuteTable(CommandType.Text, Str_InSql, param);
        }
        #endregion

        #region /history.aspx
        public int historyCount(string Strday)
        {
            OleDbParameter param = new OleDbParameter("@Strday", Strday);
            string Str_InSql = "select count(*) as a from " + Pre + "old_news where DateDiff('d',[CreatTime],@Strday)=0  and isLock=0 and isRecyle=0 ";
            int returnCount=Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Str_InSql, param));
            return returnCount;
        }
        #endregion
    }
}
