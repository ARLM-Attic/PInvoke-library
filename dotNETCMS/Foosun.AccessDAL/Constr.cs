///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.Common;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Constr : DbBase, IConstr
    {
        #region 前台
        public int Add(STConstr Con)
        {
            string Sql = "Insert Into " + Pre + "user_Constr(ConID,Content,ClassID,Title,creatTime,Source,Tags,Contrflg,Author,UserNum,isCheck,PicURL,SiteID,ispass,isadmidel,isuserdel) Values(@ConID,@Content,@ClassID,@Title,@creatTime,@Source,@Tags,@Contrflg,@Author,@UserNum,0,@PicURL,@SiteID,0,0,0)";
            OleDbParameter[] parm = GetParameters(Con);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int selGroupNumber(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            int result = 0;
            string Sql = "select UserGroupNumber from " + Pre + "sys_user where UserNum=@UserNum";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            dr.Read();
            string GroupNumber = dr["UserGroupNumber"].ToString();
            string Sql1 = "select ContrNum from " + Pre + "User_Group where GroupNumber='" + GroupNumber + "'";
            IDataReader dr1 = DbHelper.ExecuteReader(CommandType.Text, Sql1, null);
            dr1.Read();
            int ContrNum = int.Parse(dr1["ContrNum"].ToString()); 
            dr1.Close();
            string Sql2 = "select count(*) from " + Pre + "user_Constr where UserNum=@UserNum";
            int cut = (int)DbHelper.ExecuteScalar(CommandType.Text, Sql2, param);
            if (cut >= ContrNum)
            {
                result = 1;
            }
            return result;
        }
        public DataTable selConstrClass(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select Ccid,cName from " + Pre + "User_ConstrClass where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel1(string ConID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ConID);
            string Sql = "select Content,ClassID,Title,Source,Tags,Contrflg,Author,isCheck,PicURL,SiteID from " + Pre + "user_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int sel2()
        {
            string Sql = "select count(*) from " + Pre + "sys_userother";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
        }
        public string selcName(string u_ClassID)
        {
            OleDbParameter param = new OleDbParameter("@Ccid", u_ClassID);
            string Sql = "select cName from " + Pre + "User_ConstrClass where Ccid=@Ccid";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string selSiteID(string u_SiteID)
        {
            OleDbParameter param = new OleDbParameter("@ChannelID", u_SiteID);
            string Sql = "select CName from " + Pre + "News_site ChannelID=@ChannelID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Delete(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ID);
            string Sql = "delete from " + Pre + "sys_userother where ConID=@ConID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public int Update(STConstr Con, string ConIDs)
        {
            string SQL = "update " + Pre + "user_Constr set Content=@Content,ClassID=@ClassID,Title=@Title,Contrflg=@Contrflg,creatTime=@creatTime,Source=@Source,Tags=@Tags,Author=@Author,PicURL=@PicURL,SiteID=@SiteID where ConID=@Con_ID";
            OleDbParameter[] parm = Database.getNewParam(GetParameters(Con), "Content,ClassID,Title,Contrflg,creatTime,Source,Tags,Author,PicURL,SiteID,Con_ID");
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 1);
            parm[i_length] = new OleDbParameter("@Con_ID", ConIDs);
            return DbHelper.ExecuteNonQuery(CommandType.Text, SQL, parm);
        }
        private OleDbParameter[] GetParameters(STConstr Con)
        {
            OleDbParameter[] parm = new OleDbParameter[12];
            parm[0] = new OleDbParameter("@ConID", OleDbType.VarWChar, 50);
            parm[0].Value = Rand.Number(12);
            parm[1] = new OleDbParameter("@Content", OleDbType.VarWChar);
            parm[1].Value = Con.Content;
            parm[2] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 16);
            parm[2].Value = Con.ClassID;
            parm[3] = new OleDbParameter("@Title", OleDbType.VarWChar, 16);
            parm[3].Value = Con.Title;
            parm[4] = new OleDbParameter("@creatTime", OleDbType.Date);
            parm[4].Value = DateTime.Now;
            parm[5] = new OleDbParameter("@Source", OleDbType.VarWChar, 12);
            parm[5].Value = Con.Source;
            parm[6] = new OleDbParameter("@Tags", OleDbType.VarWChar, 50);
            if (Con.Tags == "" || Con.Tags == null)
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = Con.Tags;
            }
            parm[7] = new OleDbParameter("@Contrflg", OleDbType.VarWChar, 50);
            parm[7].Value = Con.Contrflg;
            parm[8] = new OleDbParameter("@Author", OleDbType.VarWChar, 50);
            parm[8].Value = Con.Author;
            parm[9] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 50);
            parm[9].Value = Con.UserNum;
            parm[10] = new OleDbParameter("@PicURL", OleDbType.VarWChar, 50);
            if (Con.PicURL == "" || Con.PicURL == null)
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = Con.PicURL;
            }
            parm[11] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 50);
            parm[11].Value = Con.SiteID;
            return parm;
        }
        public int sel3(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select count(*) from " + Pre + "sys_userother where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Add1(STuserother Con, string UserNum)
        {
            string Sql = "insert into " + Pre + "sys_userother(ConID,address,postcode,RealName,bankName,bankaccount,bankcard,bankRealName,UserNum) values(@ConID,@address,@postcode,@RealName,@bankName,@bankaccount,@bankcard,@bankRealName,@UserNum) ";
            OleDbParameter[] parm = Database.getNewParam(GetUserother(Con), "ConID,address,postcode,RealName,bankName,bankaccount,bankcard,bankRealName,UserNum");
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 1);
            parm[i_length] = new OleDbParameter("@UserNum", UserNum);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int Update1(STuserother Con, string ConIDs)
        {
            string Sql = "update " + Pre + "sys_userother set address=@address,postcode=@postcode,RealName=@RealName,bankName=@bankName,bankaccount=@bankaccount,bankcard=@bankcard,bankRealName=@bankRealName where ConID=@ConIDs ";
            OleDbParameter[] parm = Database.getNewParam(GetUserother(Con),"address,postcode,RealName,bankName,bankaccount,bankcard,bankRealName,ConIDs");
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 1);
            parm[i_length] = new OleDbParameter("@ConIDs", ConIDs);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);

        }
        public DataTable sel4(string ConIDs)
        {
            OleDbParameter param = new OleDbParameter("@ConIDs", ConIDs);
            string Sql = "select address,postcode,RealName,bankName,bankaccount,bankcard,bankRealName from " + Pre + "sys_userother where ConID=@ConIDs ";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);

        }
        private OleDbParameter[] GetUserother(STuserother Con)
        {
            OleDbParameter[] parm = new OleDbParameter[8];
            parm[0] = new OleDbParameter("@ConID", OleDbType.VarWChar, 15);
            parm[0].Value = Rand.Number(12);
            parm[1] = new OleDbParameter("@address", OleDbType.VarWChar, 100);
            parm[1].Value = Con.address;
            parm[2] = new OleDbParameter("@postcode", OleDbType.VarWChar, 20);
            parm[2].Value = Con.postcode;
            parm[3] = new OleDbParameter("@RealName", OleDbType.VarWChar, 20);
            parm[3].Value = Con.RealName;
            parm[4] = new OleDbParameter("@bankName", OleDbType.VarWChar, 100);
            parm[4].Value = Con.bankName;
            parm[5] = new OleDbParameter("@bankaccount", OleDbType.VarWChar, 30);
            parm[5].Value = Con.bankaccount;
            parm[6] = new OleDbParameter("@bankcard", OleDbType.VarWChar, 50);
            parm[6].Value = Con.bankcard;
            parm[7] = new OleDbParameter("@bankRealName", OleDbType.VarWChar, 50);
            parm[7].Value = Con.bankRealName;
            return parm;
        }
        public DataTable sel5(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ID", ID);
            string Sql = "select Id from " + Pre + "user_Constr where ClassID=@ID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Delete1(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ID);
            string Sql = "Delete from " + Pre + "user_Constr Where ClassID=@ClassID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
            string Sql1 = "delete from " + Pre + "user_ConstrClass where Ccid=@ClassID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql1, param);
        }
        public DataTable sel6()
        {
            string Sql = "select Ccid from " + Pre + "user_ConstrClass";

            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add2(STConstrClass Con, string Ccid, string UserNum)
        {
            string Sql = "insert into " + Pre + "user_ConstrClass(Ccid,UserNum,cName,Content,creatTime) values('" + Ccid + "','" + UserNum + "','" + Con.cName + "','" + Con.Content + "',#" + DateTime.Now + "#)";
            //OleDbParameter[] parm = Database.getNewParam(GetConstrClass(Con), "Ccid,UserNum,cName,Content,creatTime");
            //int i_length = parm.Length;
            //Array.Resize<OleDbParameter>(ref parm, i_length + 3);
            //parm[i_length + 1] = new OleDbParameter("@Ccid", Ccid);
            //parm[i_length + 2] = new OleDbParameter("@UserNum", UserNum);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public string ConstrTF()
        {
            string Sql = "select ConstrTF from " + Pre + "sys_PramUser where SiteID = '" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        #region ConstrClass_up.aspx
        public DataTable sel7(string Ccid)
        {
            OleDbParameter param = new OleDbParameter("@Ccid", Ccid);
            string Sql = "select cName,Content from " + Pre + "user_ConstrClass where Ccid=@Ccid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update2(STConstrClass Con, string Ccid)
        {
            string Sql = "update " + Pre + "user_ConstrClass set cName=@cName,Content=@Content where  Ccid=@Ccid";
            OleDbParameter[] parm = Database.getNewParam(GetConstrClass(Con), "cName,Content,Ccid");
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 2);
            parm[i_length + 1] = new OleDbParameter("@Ccid", Ccid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        #endregion

        private OleDbParameter[] GetConstrClass(STConstrClass Con)
        {
            OleDbParameter[] parm = new OleDbParameter[3];
            parm[0] = new OleDbParameter("@cName", OleDbType.VarWChar, 100);
            parm[0].Value = Con.cName;
            parm[1] = new OleDbParameter("@Content", OleDbType.VarWChar, 20);
            parm[1].Value = Con.Content;
            parm[2] = new OleDbParameter("@creatTime", OleDbType.Date);
            parm[2].Value = DateTime.Now;
            return parm;
        }

        #region Constrlist.aspx
        public string sel_cName(string Ccid)
        {
            OleDbParameter param = new OleDbParameter("@Ccid", Ccid);
            string Sql = "select cName from " + Pre + "user_ConstrClass where  Ccid=@Ccid";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_Tags(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ID);
            string Sql = "select Contrflg from " + Pre + "user_Constr where ConID=@ConID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_Tage1(string tagsd, string ID)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@tagsd", OleDbType.VarWChar, 10);
            param[0].Value = tagsd;
            param[1] = new OleDbParameter("@ID", OleDbType.VarWChar, 12);
            param[1].Value = ID;
            string Sql = "Update " + Pre + "user_Constr Set  Contrflg=@tagsd where ConID=@ID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete2(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ID);
            string Sql = "delete from " + Pre + "user_Constr where ConID=@ConID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public DataTable GetPage(string UserNum, string ClassID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string Sql = "";
            if (UserNum != null && UserNum != "")
            {
                Sql += " where UserNum=@UserNum";
            }
            if (ClassID != null && !ClassID.Equals(""))
            {
                Sql += " and ClassID=@ClassID";
            }
            string AllFields = "ConID,Title,creatTime,ClassID,isCheck,Contrflg,ispass,UserNum";
            string Condition = "" + Pre + "user_Constr " + Sql + "";
            string IndexField = "id";
            string OrderFields = "order by Id desc";

            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserNum", UserNum), new OleDbParameter("@ClassID", ClassID) };
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, param);
        }

        public DataTable GetPage1(int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string AllFields = "UserNum";
            string Condition = "(select DISTINCT UserNum from " + Pre + "user_Constr where Mid(Contrflg,3,1) = '1' and SiteID='" + Foosun.Global.Current.SiteID + "') UserNum1";
            string IndexField = "UserNum";
            string OrderFields = "order by UserNum desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        #endregion

        #region Constrlistpass.aspx
        public int Delete3(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ID);
            string Sql = "delete from " + Pre + "user_Constr where ConID=@ConID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region Constrlistpass_DC.aspx
        public DataTable sel8(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ID);
            string Sql = "select Title,creatTime,ClassID,passcontent from " + Pre + "user_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel9(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@Ccid", ClassID);
            string Sql = "select cName from " + Pre + "user_ConstrClass where  Ccid=@Ccid";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel19(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_User where  UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #endregion

        #region 后台

        #region Constr_chicklist.aspx
        public DataTable sel10(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ID);
            string Sql = "select isadmidel,isuserdel from " + Pre + "user_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public int Update3(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ID);
            string Sql = "update " + Pre + "user_Constr set isadmidel=1 where ConID=@ConID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public int Delete4(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ID);
            string Sql = "delete from " + Pre + "user_Constr where ConID=@ConID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region Constr_Edit.aspx
        public DataTable sel11(string ConIDs)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ConIDs);
            string Sql = "select Content,Title,Author,isCheck,Tags from  " + Pre + "User_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel12()
        {
            string Sql = "Select ConstrPayName,PCId From " + Pre + "sys_ParmConstr where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable sel13(string ConIDp)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ConIDp);
            string Sql = "select Title,Content,Source,Author,PicURL,SiteID,UserNum,creatTime,Tags from " + Pre + "User_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Add3(string NewsID, string Title, string PicURL, string ClassID, string Author, string UserNum, string Source, string Contents, string creatTime, string SiteID, string Tags, string DataLib, string NewsTemplet, string strSavePath, string strfileName, string strfileexName, string strCheckInt)
        {/*
          *   param[0] = new OleDbParameter("@NewsID",OleDbType.VarWChar,12);
            param[0].Value = NewsID;
            param[1] = new OleDbParameter("@Title",OleDbType.VarWChar,100);
            param[1].Value = Title;
            param[2] = new OleDbParameter("@PicURL",OleDbType.VarWChar,200);
            param[2].Value = PicURL;
            param[3] = new OleDbParameter("@ClassID",OleDbType.VarWChar,12);
            param[3].Value = ClassID;
            param[4] = new OleDbParameter("@Author",OleDbType.VarWChar,100);
            param[4].Value = Author;
            param[5] = new OleDbParameter("@UserNum",OleDbType.VarWChar,18);
            param[5].Value = UserNum;
            param[6] = new OleDbParameter("@Source",OleDbType.VarWChar,100);
            param[6].Value = Source;
            param[7] = new OleDbParameter("@Contents",OleDbType.VarWChar);
            param[7].Value = Contents;
            param[8] = new OleDbParameter("@creatTime",OleDbType.Date,8);
            param[8].Value = DateTime.Now;
            param[9] = new OleDbParameter("@SiteID",OleDbType.VarWChar,12);
            param[9].Value = SiteID;
            param[10] = new OleDbParameter("@Tags",OleDbType.VarWChar,100);
            param[10].Value = Tags;
            param[11] = new OleDbParameter("@DataLib",OleDbType.VarWChar,20);
            param[11].Value = DataLib;
            param[12] = new OleDbParameter("@NewsTemplet",OleDbType.VarWChar,200);
            param[12].Value = NewsTemplet;
            param[13] = new OleDbParameter("@strSavePath",OleDbType.VarWChar,200);
            param[13].Value = strSavePath;
            param[14] = new OleDbParameter("@strfileName",OleDbType.VarWChar,100);
            param[14].Value = strfileName;
            param[15] = new OleDbParameter("@strfileexName", OleDbType.VarWChar, 6);
            param[15].Value = strfileexName;
            param[16] = new OleDbParameter("@strCheckInt", OleDbType.VarWChar, 10);
          */
            OleDbParameter[] param = new OleDbParameter[17];
            param[0] = new OleDbParameter("@NewsID", OleDbType.VarWChar, 12);
            param[0].Value = NewsID;
            param[1] = new OleDbParameter("@Title", OleDbType.VarWChar, 100);
            param[1].Value = Title;
            param[2] = new OleDbParameter("@PicURL", OleDbType.VarWChar, 200);
            param[2].Value = PicURL;
            param[3] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
            param[3].Value = ClassID;
            param[4] = new OleDbParameter("@Author", OleDbType.VarWChar, 100);
            param[4].Value = Author;
            param[5] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 18);
            param[5].Value = UserNum;
            param[6] = new OleDbParameter("@Source", OleDbType.VarWChar, 100);
            param[6].Value = Source;
            param[7] = new OleDbParameter("@Contents", OleDbType.VarWChar);
            param[7].Value = Contents;
            param[8] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[8].Value = DateTime.Now;
            param[9] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[9].Value = SiteID;
            param[10] = new OleDbParameter("@Tags", OleDbType.VarWChar, 100);
            param[10].Value = Tags;
            param[11] = new OleDbParameter("@DataLib", OleDbType.VarWChar, 20);
            param[11].Value = DataLib;
            param[12] = new OleDbParameter("@NewsTemplet", OleDbType.VarWChar, 200);
            param[12].Value = NewsTemplet;
            param[13] = new OleDbParameter("@strSavePath", OleDbType.VarWChar, 200);
            param[13].Value = strSavePath;
            param[14] = new OleDbParameter("@strfileName", OleDbType.VarWChar, 100);
            param[14].Value = strfileName;
            param[15] = new OleDbParameter("@strfileexName", OleDbType.VarWChar, 6);
            param[15].Value = strfileexName;
            param[16] = new OleDbParameter("@strCheckInt", OleDbType.VarWChar, 10);
            param[16].Value = strCheckInt;

            //string Sql = "insert into " + DataLib + "(NewsID,NewsType,NewsTitle,TitleITF,PicURL,ClassID,Author,Editor,Souce," +
            //             "Content,CreatTime,SiteID,Tags,OrderID,CommlinkTF,SubNewsTF,NewsProperty,newspictopline,templet,click," +
            //             "savepath,fileName,fileEXName,isDelPoint,gPoint,ipoint,groupNumber,ContentPicTF,CommTF,DiscussTF,topnum," +
            //             "voteTF,checkstat,islock,isRecyle,isVoteTF,isHTML,DataLib,isConstr,DefineID) values(@NewsID,0,@Title,0,@PicURL," +
            //             "@ClassID,@Author,@UserNum,@Source,@Contents,@creatTime,@SiteID,@Tags,0,0,0,'aaaa'," +
            //             "0,@NewsTemplet,0,@strSavePath,@strfileName,@strfileexName,0,0,0,'',0,1,1,0,0,@strCheckInt,0,0,0,0,@DataLib,1,0)";
            string Sql = "insert into " + DataLib + "(NewsID,NewsType,NewsTitle,TitleITF,PicURL,ClassID,Author,Editor,Souce," +
             "Content,CreatTime,SiteID,Tags,OrderID,CommlinkTF,SubNewsTF,NewsProperty,newspictopline,templet,click," +
             "savepath,fileName,fileEXName,isDelPoint,gPoint,ipoint,groupNumber,ContentPicTF,CommTF,DiscussTF,topnum," +
             "voteTF,checkstat,islock,isRecyle,isVoteTF,isHTML,DataLib,isConstr,DefineID) values('" + NewsID + "',0,'" + Title + "',0,'" + PicURL + "'," +
             "'" + ClassID + "','" + Author + "','" + UserNum + "','" + Source + "','" + Contents + "','" + DateTime.Now + "','" + SiteID + "','" + Tags + "',0,0,0,'0,0,0,0,0,0,0,0'," +
             "0,'" + NewsTemplet + "',0,'" + strSavePath + "','" + strfileName + "','" + strfileexName + "',0,0,0,'',0,1,1,0,0,'" + strCheckInt + "',0,0,0,0,'" + DataLib + "',1,0)";
           // return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "NewsID,Title,PicURL,ClassID,Author,UserNum,Source,Contents,creatTime,SiteID,Tags,DataLib,NewsTemplet,strSavePath,strfileName,strfileexName,strCheckInt"));
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        //重载（wxh NewsType 2008-7-11）
        public int Add3(string NewsID, int NewsType, string Title, string PicURL, string ClassID, string Author, string UserNum, string Source, string Contents, string creatTime, string SiteID, string Tags, string DataLib, string NewsTemplet, string strSavePath, string strfileName, string strfileexName, string strCheckInt)
        {

            string Sql = "insert into " + DataLib + "(NewsID,NewsType,NewsTitle,TitleITF,PicURL,ClassID,Author,Editor,Souce," +
             "Content,CreatTime,SiteID,Tags,OrderID,CommlinkTF,SubNewsTF,NewsProperty,newspictopline,templet,click," +
             "savepath,fileName,fileEXName,isDelPoint,gPoint,ipoint,groupNumber,ContentPicTF,CommTF,DiscussTF,topnum," +
             "voteTF,checkstat,islock,isRecyle,isVoteTF,isHTML,DataLib,isConstr,DefineID) values('" + NewsID + "'," + NewsType + ",'" + Title + "',0,'" + PicURL + "'," +
             "'" + ClassID + "','" + Author + "','" + UserNum + "','" + Source + "','" + Contents + "','" + DateTime.Now + "','" + SiteID + "','" + Tags + "',0,0,0,'0,0,0,0,0,0,0,0'," +
             "0,'" + NewsTemplet + "',0,'" + strSavePath + "','" + strfileName + "','" + strfileexName + "',0,0,0,'',0,1,1,0,0,'" + strCheckInt + "',0,0,0,0,'" + DataLib + "',1,0)";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public void updateConstrStrat(string ConID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ConID);
            string Sql = "update " + Pre + "User_Constr set isCheck=1 where ConID=@ConID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int Update4(string ConIDp)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ConIDp);
            string Sql = "update " + Pre + "User_Constr set isCheck=1 where ConID=@ConID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public DataTable sel14(string PCIdsa)
        {
            OleDbParameter param = new OleDbParameter("@PCId", PCIdsa);
            string Sql = "select gPoint,iPoint,money from " + Pre + "sys_ParmConstr where PCId=@PCId";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Add4(string NewsID, int gPoint, int iPoint, int Money1, DateTime CreatTime1, string UserNum, string content4)
        {
            OleDbParameter[] param = new OleDbParameter[7] ;
            param[0] = new OleDbParameter("@NewsID",OleDbType.VarWChar,12);
            param[0].Value = NewsID;
            param[1] = new OleDbParameter("@gPoint",OleDbType.Integer,4);
            param[1].Value = gPoint;
            param[2] = new OleDbParameter("@iPoint",OleDbType.Integer,4);
            param[2].Value = iPoint;
            param[3] = new OleDbParameter("@Money1",OleDbType.Integer,4);
            param[3].Value = Money1;
            param[4] = new OleDbParameter("@CreatTime1",OleDbType.Date,8);
            param[4].Value = CreatTime1;
            param[5] = new OleDbParameter("@UserNum",OleDbType.VarWChar,12);
            param[5].Value = UserNum;
            param[6] = new OleDbParameter("@content4",OleDbType.VarWChar);
            param[6].Value = content4;
            string Sql = "insert into " + Pre + "User_Ghistory(GhID,ghtype,Gpoint,iPoint,[Money],CreatTime,UserNUM,gtype,content,siteID) values(@NewsID,1,@gPoint,@iPoint,@Money1,@CreatTime1,@UserNum,4,@content4,'" + Foosun.Global.Current.SiteID + "')";
            //string Sql = "insert into " + Pre + "User_Ghistory(GhID,ghtype,Gpoint,iPoint,Money,CreatTime,UserNUM,gtype,content,siteID) values('" + NewsID + "',1," + gPoint + "," + iPoint + "," + Money1 + ",'" + CreatTime1 + "','" + UserNum + "',4,'" + content4 + "','" + Foosun.Global.Current.SiteID + "')";
            // return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "NewsID,gPoint,iPoint,Money1,CreatTime1,UserNum,content4")));
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public DataTable sel15(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select iPoint,gPoint,ParmConstrNum,cPoint,aPoint from " + Pre + "sys_User where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel16()
        {
            string Sql = "select cPointParam,aPointparam from " + Pre + "sys_PramUser";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Update5(int iPoint2, int gPoint2, Decimal Money3, int cPoint2, int aPoint2, string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "update " + Pre + "sys_User set iPoint='" + iPoint2 + "',gPoint='" + gPoint2 + "',ParmConstrNum=" + Money3 + ",cPoint='" + cPoint2 + "',aPoint='" + aPoint2 + "' where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public DataTable sel17()
        {
            string Sql = "select NewsID from " + Pre + "News";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public string sel18(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string _tb = Pre + "news";
            string Sql = "select DataLib from " + Pre + "news_Class where ClassID=@ClassID";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (dt != null)
            {
                if (dt.Rows.Count > 0) { _tb = dt.Rows[0]["DataLib"].ToString(); }
                dt.Clear(); dt.Dispose();
            }
            return _tb;
        }

        public int getParmConstrNum(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            int intflg = 0;
            string Sql = "select ParmConstrNum from " + Pre + "sys_user where UserNum=@UserNum and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (Foosun.Common.Input.IsInteger(dt.Rows[0]["ParmConstrNum"].ToString()))
                    {
                        intflg = int.Parse(dt.Rows[0]["ParmConstrNum"].ToString());
                    }
                }
                dt.Clear(); dt.Dispose();
            }
            return intflg;
        }
        #endregion

        #region Constr_Pay.aspx
        public DataTable sel20(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName,ParmConstrNum from " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel21(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select address,postcode,RealName,bankName,bankcard,bankRealName from " + Pre + "sys_userother where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel22()
        {
            string Sql = "select constrPayID from " + Pre + "user_constrPay";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add5(string UserNum1, int ParmConstrNums, DateTime payTime, string constrPayID)
        {
            OleDbParameter[] param = new OleDbParameter[4] ;
            param[0] = new OleDbParameter("@userNum",OleDbType.VarWChar,12);
            param[0].Value = UserNum1;
            param[1] = new OleDbParameter("@ParmConstrNums",OleDbType.Integer,4);
            param[1].Value = ParmConstrNums;
            param[2] = new OleDbParameter("@payTime",OleDbType.Date,8);
            param[2].Value = payTime;
            param[3] = new OleDbParameter("@constrPayID",OleDbType.VarWChar,12);
            param[3].Value = constrPayID;

            string Sql = "insert into " + Pre + "user_constrPay(userNum,Money,payTime,constrPayID,SiteID,PayAdmin) values(@userNum,@ParmConstrNums,@payTime,@constrPayID,'" + Foosun.Global.Current.SiteID + "','" + Foosun.Global.Current.UserName + "')";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "userNum,Money,payTime,constrPayID")));
        }
        public int Update5(string UserNum1)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum1);
            string Sql = "update " + Pre + "sys_user set ParmConstrNum=0 where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region Constr_Return.aspx
        public DataTable sel23(string ConID)
        {
            OleDbParameter param = new OleDbParameter("@ConID", ConID);
            string Sql = "select Title,ispass from " + Pre + "User_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update6(string passcontent, string ConIDs)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@passcontent", OleDbType.VarWChar);
            param[0].Value = passcontent;
            param[1] = new OleDbParameter("@ConID", OleDbType.VarWChar, 12);
            param[1].Value = ConIDs;
            string Sql = "update " + Pre + "User_Constr set ispass='1',passcontent=@passcontent where ConID=@ConID ";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region Constr_SetParam.aspx
        public int Add6(string PCId, string ConstrPayName, string gpoint, string ipoint, int moneys1, string Gunit)
        {
            string CSql = "select count(id) from " + Pre + "sys_ParmConstr where ConstrPayName='" + ConstrPayName + "'";
            int CCount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, CSql, null));
            if (CCount > 0)
            {
                return 0;
            }
            OleDbParameter[] param = new OleDbParameter[6];
            param[0] = new OleDbParameter("@PCId", OleDbType.VarWChar, 18);
            param[0].Value = PCId;
            param[1] = new OleDbParameter("@ConstrPayName", OleDbType.VarWChar, 20);
            param[1].Value = ConstrPayName;
            param[2] = new OleDbParameter("@gpoint", OleDbType.Integer, 4);
            param[2].Value = gpoint;
            param[3] = new OleDbParameter("@ipoint", OleDbType.Integer, 4);
            param[3].Value = ipoint;
            param[4] = new OleDbParameter("@money", OleDbType.Integer, 4);
            param[4].Value = moneys1;
            param[5] = new OleDbParameter("@Gunit", OleDbType.VarWChar, 10);
            param[5].Value = Gunit;
            string Sql = "insert into " + Pre + "sys_ParmConstr(PCId,ConstrPayName,gPoint,iPoint,money,Gunit,SiteID) values(@PCId,@ConstrPayName,@gpoint,@ipoint,@moneys1,@Gunit,'" + Foosun.Global.Current.SiteID + "')";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "PCId,ConstrPayName,gPoint,iPoint,money,Gunit")));
        }

        public DataTable sel24()
        {
            string Sql = "select PCId from " + Pre + "sys_ParmConstr";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        #endregion

        #region Constr_SetParamlist.aspx
        public int Delete5(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PCId", ID);
            string Sql = " delete from " + Pre + "sys_ParmConstr where PCId=@PCId";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region Constr_SetParamup.aspx
        public DataTable sel25(string PCIdup)
        {
            OleDbParameter param = new OleDbParameter("@PCId", PCIdup);
            string Sql = "select ConstrPayName,gPoint,iPoint,money,Gunit from " + Pre + "sys_ParmConstr where PCId=@PCId";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update6(string ConstrPayName, string gpoint, string ipoint, int moneys1, string Gunit, string PCIdup)
        {
            OleDbParameter[] param = new OleDbParameter[6];
            param[0] = new OleDbParameter("@PCId", OleDbType.VarWChar, 18);
            param[0].Value = PCIdup;
            param[1] = new OleDbParameter("@ConstrPayName", OleDbType.VarWChar, 20);
            param[1].Value = ConstrPayName;
            param[2] = new OleDbParameter("@gpoint", OleDbType.Integer, 4);
            param[2].Value = gpoint;
            param[3] = new OleDbParameter("@ipoint", OleDbType.Integer, 4);
            param[3].Value = ipoint;
            param[4] = new OleDbParameter("@moneys1", OleDbType.Integer, 4);
            param[4].Value = moneys1;
            param[5] = new OleDbParameter("@Gunit", OleDbType.VarWChar, 10);
            param[5].Value = Gunit;
            string Sql = "update " + Pre + "sys_ParmConstr set ConstrPayName=@ConstrPayName,gPoint=@gPoint,iPoint=@iPoint,money=@moneys1,Gunit=@Gunit where PCId=@PCId";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param, "ConstrPayName,gPoint,iPoint,money,Gunit,PCId")));
        }
        #endregion

        #region Constr_Stat.aspx
        public int sel26(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select count(Id) from " + Pre + "User_Constr where UserNum=@UserNum and Mid(Contrflg,3,1) = '1' and isadmidel=0 and ispass=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int sel27(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select count(Id) from " + Pre + "User_Constr where UserNum=@UserNum and isCheck=1  and Mid(Contrflg,3,1) = '1' and isadmidel=0 and ispass=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public DataTable sel28(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select creatTime from " + Pre + "User_Constr where UserNum=@UserNum and isCheck=1  and Mid(Contrflg,3,1) = '1' and isadmidel=0 and ispass=0";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int sel29(string UserNum, int m1)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select count(Id) from " + Pre + "User_Constr where UserNum=@UserNum and Month(creatTime)= '" + m1 + "'  and Mid(Contrflg,3,1) = '1' and isadmidel=0 and ispass=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        #endregion

        #region paymentannals.aspx
        public DataTable sel30(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel31(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select isSuper from " + Pre + "sys_admin where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Delete6(string ID)
        {
            OleDbParameter param = new OleDbParameter("@constrPayID", ID);
            string Sql = "delete from " + Pre + "user_constrPay where constrPayID=@constrPayID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        /// <summary>
        /// 得到栏目模板，扩展名等。
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable getClassInfo(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string Sql = "select ReadNewsTemplet,NewsSavePath,NewsFileRule,FileName,checkint from " + Pre + "news_class Where ClassID=@ClassID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        #endregion
    }
}