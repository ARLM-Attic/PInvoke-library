///************************************************************************************************************
///**********Composing Wang Zhen jinag*************************************************************************
///************************************************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.Common;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Constr : DbBase, IConstr
    {
        #region 前台
        public int Add(STConstr Con)
        {
            string Sql = "Insert Into " + Pre + "user_Constr(ConID,Content,ClassID,Title,creatTime,Source,Tags,Contrflg,Author,UserNum,isCheck,PicURL,SiteID,ispass,isadmidel,isuserdel) Values(@ConID,@Content,@ClassID,@Title,@creatTime,@Source,@Tags,@Contrflg,@Author,@UserNum,0,@PicURL,@SiteID,0,0,0)";
            SqlParameter[] parm = GetParameters(Con);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int selGroupNumber(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            int result = 0;
            string Sql = "select UserGroupNumber from " + Pre + "sys_user where UserNum=@UserNum";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            dr.Read();
            string GroupNumber = dr["UserGroupNumber"].ToString();
            string Sql1 = "select ContrNum from " + Pre + "User_Group where GroupNumber='" + GroupNumber + "'";
            dr.Close();
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
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select Ccid,cName from " + Pre + "User_ConstrClass where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel1(string ConID)
        {
            SqlParameter param = new SqlParameter("@ConID", ConID);
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
            SqlParameter param = new SqlParameter("@Ccid", u_ClassID);
            string Sql = "select cName from " + Pre + "User_ConstrClass where Ccid=@Ccid";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string selSiteID(string u_SiteID)
        {
            SqlParameter param = new SqlParameter("@ChannelID", u_SiteID);
            string Sql = "select CName from " + Pre + "News_site ChannelID=@ChannelID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Delete(string ID)
        {
            SqlParameter param = new SqlParameter("@ConID", ID);
            string Sql = "delete " + Pre + "sys_userother where ConID=@ConID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public int Update(STConstr Con, string ConIDs)
        {
            string SQL = "update " + Pre + "user_Constr set Content=@Content,ClassID=@ClassID,Title=@Title,Contrflg=@Contrflg,creatTime=@creatTime,Source=@Source,Tags=@Tags,Author=@Author,PicURL=@PicURL,SiteID=@SiteID where ConID=@Con_ID";
            SqlParameter[] parm = GetParameters(Con);
            int i_length = parm.Length;
            Array.Resize<SqlParameter>(ref parm, i_length + 1);
            parm[i_length] = new SqlParameter("@Con_ID", ConIDs);
            return DbHelper.ExecuteNonQuery(CommandType.Text, SQL, parm);
        }
        private SqlParameter[] GetParameters(STConstr Con)
        {
            SqlParameter[] parm = new SqlParameter[12];
            parm[0] = new SqlParameter("@ConID", SqlDbType.NVarChar, 50);
            parm[0].Value = Rand.Number(12);
            parm[1] = new SqlParameter("@Content", SqlDbType.NText);
            parm[1].Value = Con.Content;
            parm[2] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 16);
            parm[2].Value = Con.ClassID;
            parm[3] = new SqlParameter("@Title", SqlDbType.NVarChar, 16);
            parm[3].Value = Con.Title;
            parm[4] = new SqlParameter("@creatTime", SqlDbType.DateTime);
            parm[4].Value = DateTime.Now;
            parm[5] = new SqlParameter("@Source", SqlDbType.NVarChar, 12);
            parm[5].Value = Con.Source;
            parm[6] = new SqlParameter("@Tags", SqlDbType.NVarChar, 50);
            if (Con.Tags == "" || Con.Tags == null)
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = Con.Tags;
            }
            parm[7] = new SqlParameter("@Contrflg", SqlDbType.NVarChar, 50);
            parm[7].Value = Con.Contrflg;
            parm[8] = new SqlParameter("@Author", SqlDbType.NVarChar, 50);
            parm[8].Value = Con.Author;
            parm[9] = new SqlParameter("@UserNum", SqlDbType.NVarChar, 50);
            parm[9].Value = Con.UserNum;
            parm[10] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 50);
            if (Con.PicURL == "" || Con.PicURL == null)
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = Con.PicURL;
            }
            parm[11] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 50);
            parm[11].Value = Con.SiteID;
            return parm;
        }
        public int sel3(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select count(*) from " + Pre + "sys_userother where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Add1(STuserother Con, string UserNum)
        {
            string Sql = "insert into " + Pre + "sys_userother(ConID,address,postcode,RealName,bankName,bankaccount,bankcard,bankRealName,UserNum) values(@ConID,@address,@postcode,@RealName,@bankName,@bankaccount,@bankcard,@bankRealName,@UserNum) ";
            SqlParameter[] parm = GetUserother(Con);
            int i_length = parm.Length;
            Array.Resize<SqlParameter>(ref parm, i_length + 1);
            parm[i_length] = new SqlParameter("@UserNum", UserNum);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public int Update1(STuserother Con, string ConIDs)
        {
            string Sql = "update " + Pre + "sys_userother set address=@address,postcode=@postcode,RealName=@RealName,bankName=@bankName,bankaccount=@bankaccount,bankcard=@bankcard,bankRealName=@bankRealName where ConID=@ConIDs ";
            SqlParameter[] parm = GetUserother(Con);
            int i_length = parm.Length;
            Array.Resize<SqlParameter>(ref parm, i_length + 1);
            parm[i_length] = new SqlParameter("@ConIDs", ConIDs);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);

        }
        public DataTable sel4(string ConIDs)
        {
            SqlParameter param = new SqlParameter("@ConIDs", ConIDs);
            string Sql = "select address,postcode,RealName,bankName,bankaccount,bankcard,bankRealName from " + Pre + "sys_userother where ConID=@ConIDs ";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);

        }
        private SqlParameter[] GetUserother(STuserother Con)
        {
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@ConID", SqlDbType.NVarChar, 15);
            parm[0].Value = Rand.Number(12);
            parm[1] = new SqlParameter("@address", SqlDbType.NVarChar, 100);
            parm[1].Value = Con.address;
            parm[2] = new SqlParameter("@postcode", SqlDbType.NVarChar, 20);
            parm[2].Value = Con.postcode;
            parm[3] = new SqlParameter("@RealName", SqlDbType.NVarChar, 20);
            parm[3].Value = Con.RealName;
            parm[4] = new SqlParameter("@bankName", SqlDbType.NVarChar, 100);
            parm[4].Value = Con.bankName;
            parm[5] = new SqlParameter("@bankaccount", SqlDbType.NVarChar, 30);
            parm[5].Value = Con.bankaccount;
            parm[6] = new SqlParameter("@bankcard", SqlDbType.NVarChar, 50);
            parm[6].Value = Con.bankcard;
            parm[7] = new SqlParameter("@bankRealName", SqlDbType.NVarChar, 50);
            parm[7].Value = Con.bankRealName;
            return parm;
        }
        public DataTable sel5(string ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string Sql = "select Id from " + Pre + "user_Constr where ClassID=@ID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Delete1(string ID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ID);
            string Sql = "Delete " + Pre + "user_Constr Where ClassID=@ClassID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
            string Sql1 = "delete " + Pre + "user_ConstrClass where Ccid=@ClassID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql1, param);
        }
        public DataTable sel6()
        {
            string Sql = "select Ccid from " + Pre + "user_ConstrClass";

            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add2(STConstrClass Con, string Ccid, string UserNum)
        {
            string Sql = "insert into " + Pre + "user_ConstrClass(Ccid,UserNum,cName,Content,creatTime) values(@Ccid,@UserNum,@cName,@Content,@creatTime)";
            SqlParameter[] parm = GetConstrClass(Con);
            int i_length = parm.Length;
            Array.Resize<SqlParameter>(ref parm, i_length + 3);
            parm[i_length + 1] = new SqlParameter("@Ccid", Ccid);
            parm[i_length + 2] = new SqlParameter("@UserNum", UserNum);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        public string ConstrTF()
        {
            string Sql = "select ConstrTF from " + Pre + "sys_PramUser where SiteID = '" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        #region ConstrClass_up.aspx
        public DataTable sel7(string Ccid)
        {
            SqlParameter param = new SqlParameter("@Ccid", Ccid);
            string Sql = "select cName,Content from " + Pre + "user_ConstrClass where Ccid=@Ccid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update2(STConstrClass Con, string Ccid)
        {
            string Sql = "update " + Pre + "user_ConstrClass set cName=@cName,Content=@Content where  Ccid=@Ccid";
            SqlParameter[] parm = GetConstrClass(Con);
            int i_length = parm.Length;
            Array.Resize<SqlParameter>(ref parm, i_length + 2);
            parm[i_length + 1] = new SqlParameter("@Ccid", Ccid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        #endregion

        private SqlParameter[] GetConstrClass(STConstrClass Con)
        {
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@cName", SqlDbType.NVarChar, 100);
            parm[0].Value = Con.cName;
            parm[1] = new SqlParameter("@Content", SqlDbType.NVarChar, 20);
            parm[1].Value = Con.Content;
            parm[2] = new SqlParameter("@creatTime", SqlDbType.DateTime);
            parm[2].Value = DateTime.Now;
            return parm;
        }

        #region Constrlist.aspx
        public string sel_cName(string Ccid)
        {
            SqlParameter param = new SqlParameter("@Ccid", Ccid);
            string Sql = "select cName from " + Pre + "user_ConstrClass where  Ccid=@Ccid";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_Tags(string ID)
        {
            SqlParameter param = new SqlParameter("@ConID", ID);
            string Sql = "select Contrflg from " + Pre + "user_Constr where ConID=@ConID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_Tage1(string tagsd, string ID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@tagsd", SqlDbType.NVarChar, 10);
            param[0].Value = tagsd;
            param[1] = new SqlParameter("@ID", SqlDbType.NVarChar, 12);
            param[1].Value = ID;
            string Sql = "Update " + Pre + "user_Constr Set  Contrflg=@tagsd where ConID=@ID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete2(string ID)
        {
            SqlParameter param = new SqlParameter("@ConID", ID);
            string Sql = "delete " + Pre + "user_Constr where ConID=@ConID";
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

            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@UserNum", UserNum), new SqlParameter("@ClassID", ClassID) };
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, param);
        }

        public DataTable GetPage1(int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string AllFields = "UserNum";
            string Condition = "(select DISTINCT UserNum from " + Pre + "user_Constr where substring(Contrflg,3,1) = '1' and SiteID='" + Foosun.Global.Current.SiteID + "') UserNum1";
            string IndexField = "UserNum";
            string OrderFields = "order by UserNum desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        #endregion

        #region Constrlistpass.aspx
        public int Delete3(string ID)
        {
            SqlParameter param = new SqlParameter("@ConID", ID);
            string Sql = "delete " + Pre + "user_Constr where ConID=@ConID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region Constrlistpass_DC.aspx
        public DataTable sel8(string ID)
        {
            SqlParameter param = new SqlParameter("@ConID", ID);
            string Sql = "select Title,creatTime,ClassID,passcontent from " + Pre + "user_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel9(string ClassID)
        {
            SqlParameter param = new SqlParameter("@Ccid", ClassID);
            string Sql = "select cName from " + Pre + "user_ConstrClass where  Ccid=@Ccid";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel19(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_User where  UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #endregion

        #region 后台

        #region Constr_chicklist.aspx
        public DataTable sel10(string ID)
        {
            SqlParameter param = new SqlParameter("@ConID", ID);
            string Sql = "select isadmidel,isuserdel from " + Pre + "user_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public int Update3(string ID)
        {
            SqlParameter param = new SqlParameter("@ConID", ID);
            string Sql = "update " + Pre + "user_Constr set isadmidel=1 where ConID=@ConID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public int Delete4(string ID)
        {
            SqlParameter param = new SqlParameter("@ConID", ID);
            string Sql = "delete " + Pre + "user_Constr where ConID=@ConID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region Constr_Edit.aspx
        public DataTable sel11(string ConIDs)
        {
            SqlParameter param = new SqlParameter("@ConID", ConIDs);
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
            SqlParameter param = new SqlParameter("@ConID", ConIDp);
            string Sql = "select Title,Content,Source,Author,PicURL,SiteID,UserNum,creatTime,Tags from " + Pre + "User_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Add3(string NewsID, string Title, string PicURL, string ClassID, string Author, string UserNum, string Source, string Contents, string creatTime, string SiteID, string Tags, string DataLib, string NewsTemplet, string strSavePath, string strfileName, string strfileexName, string strCheckInt)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
            param[0].Value = NewsID;
            param[1] = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            param[1].Value = Title;
            param[2] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 200);
            param[2].Value = PicURL;
            param[3] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
            param[3].Value = ClassID;
            param[4] = new SqlParameter("@Author", SqlDbType.NVarChar, 100);
            param[4].Value = Author;
            param[5] = new SqlParameter("@UserNum", SqlDbType.NVarChar, 18);
            param[5].Value = UserNum;
            param[6] = new SqlParameter("@Source", SqlDbType.NVarChar, 100);
            param[6].Value = Source;
            param[7] = new SqlParameter("@Contents", SqlDbType.NText);
            param[7].Value = Contents;
            param[8] = new SqlParameter("@creatTime", SqlDbType.DateTime, 8);
            param[8].Value = DateTime.Now;
            param[9] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[9].Value = SiteID;
            param[10] = new SqlParameter("@Tags", SqlDbType.NVarChar, 100);
            param[10].Value = Tags;
            param[11] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 20);
            param[11].Value = DataLib;
            param[12] = new SqlParameter("@NewsTemplet", SqlDbType.NVarChar, 200);
            param[12].Value = NewsTemplet;
            param[13] = new SqlParameter("@strSavePath", SqlDbType.NVarChar, 200);
            param[13].Value = strSavePath;
            param[14] = new SqlParameter("@strfileName", SqlDbType.NVarChar, 100);
            param[14].Value = strfileName;
            param[15] = new SqlParameter("@strfileexName", SqlDbType.NVarChar, 6);
            param[15].Value = strfileexName;
            param[16] = new SqlParameter("@strCheckInt", SqlDbType.NVarChar, 10);
            param[16].Value = strCheckInt;

            string Sql = "insert into " + DataLib + "(NewsID,NewsType,NewsTitle,TitleITF,PicURL,ClassID,Author,Editor,Souce," +
                         "Content,CreatTime,SiteID,Tags,OrderID,CommlinkTF,SubNewsTF,NewsProperty,newspictopline,templet,click," +
                         "savepath,fileName,fileEXName,isDelPoint,gPoint,ipoint,groupNumber,ContentPicTF,CommTF,DiscussTF,topnum," +
                         "voteTF,checkstat,islock,isRecyle,isVoteTF,isHTML,DataLib,isConstr,DefineID) values(@NewsID,0,@Title,0,@PicURL," +
                         "@ClassID,@Author,@UserNum,@Source,@Contents,@creatTime,@SiteID,@Tags,0,0,0,'0,0,0,0,0,0,0,0'," +
                         "0,@NewsTemplet,0,@strSavePath,@strfileName,@strfileexName,0,0,0,'',0,1,1,0,0,@strCheckInt,0,0,0,0,@DataLib,1,0)";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        //重载
        public int Add3(string NewsID, int NewsType, string Title, string PicURL, string ClassID, string Author, string UserNum, string Source, string Contents, string creatTime, string SiteID, string Tags, string DataLib, string NewsTemplet, string strSavePath, string strfileName, string strfileexName, string strCheckInt)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@NewsID", SqlDbType.NVarChar, 12);
            param[0].Value = NewsID;
            param[1] = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            param[1].Value = Title;
            param[2] = new SqlParameter("@PicURL", SqlDbType.NVarChar, 200);
            param[2].Value = PicURL;
            param[3] = new SqlParameter("@ClassID", SqlDbType.NVarChar, 12);
            param[3].Value = ClassID;
            param[4] = new SqlParameter("@Author", SqlDbType.NVarChar, 100);
            param[4].Value = Author;
            param[5] = new SqlParameter("@UserNum", SqlDbType.NVarChar, 18);
            param[5].Value = UserNum;
            param[6] = new SqlParameter("@Source", SqlDbType.NVarChar, 100);
            param[6].Value = Source;
            param[7] = new SqlParameter("@Contents", SqlDbType.NText);
            param[7].Value = Contents;
            param[8] = new SqlParameter("@creatTime", SqlDbType.DateTime, 8);
            param[8].Value = DateTime.Now;
            param[9] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[9].Value = SiteID;
            param[10] = new SqlParameter("@Tags", SqlDbType.NVarChar, 100);
            param[10].Value = Tags;
            param[11] = new SqlParameter("@DataLib", SqlDbType.NVarChar, 20);
            param[11].Value = DataLib;
            param[12] = new SqlParameter("@NewsTemplet", SqlDbType.NVarChar, 200);
            param[12].Value = NewsTemplet;
            param[13] = new SqlParameter("@strSavePath", SqlDbType.NVarChar, 200);
            param[13].Value = strSavePath;
            param[14] = new SqlParameter("@strfileName", SqlDbType.NVarChar, 100);
            param[14].Value = strfileName;
            param[15] = new SqlParameter("@strfileexName", SqlDbType.NVarChar, 6);
            param[15].Value = strfileexName;
            param[16] = new SqlParameter("@strCheckInt", SqlDbType.NVarChar, 10);
            param[16].Value = strCheckInt;

            string Sql = "insert into " + DataLib + "(NewsID,NewsType,NewsTitle,TitleITF,PicURL,ClassID,Author,Editor,Souce," +
                         "Content,CreatTime,SiteID,Tags,OrderID,CommlinkTF,SubNewsTF,NewsProperty,newspictopline,templet,click," +
                         "savepath,fileName,fileEXName,isDelPoint,gPoint,ipoint,groupNumber,ContentPicTF,CommTF,DiscussTF,topnum," +
                         "voteTF,checkstat,islock,isRecyle,isVoteTF,isHTML,DataLib,isConstr,DefineID) values(@NewsID," + NewsType + ",@Title,0,@PicURL," +
                         "@ClassID,@Author,@UserNum,@Source,@Contents,@creatTime,@SiteID,@Tags,0,0,0,'0,0,0,0,0,0,0,0'," +
                         "0,@NewsTemplet,0,@strSavePath,@strfileName,@strfileexName,0,0,0,'',0,1,1,0,0,@strCheckInt,0,0,0,0,@DataLib,1,0)";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void updateConstrStrat(string ConID)
        {
            SqlParameter param = new SqlParameter("@ConID", ConID);
            string Sql = "update " + Pre + "User_Constr set isCheck=1 where ConID=@ConID";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int Update4(string ConIDp)
        {
            SqlParameter param = new SqlParameter("@ConID", ConIDp);
            string Sql = "update " + Pre + "User_Constr set isCheck=1 where ConID=@ConID";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public DataTable sel14(string PCIdsa)
        {
            SqlParameter param = new SqlParameter("@PCId", PCIdsa);
            string Sql = "select gPoint,iPoint,money from " + Pre + "sys_ParmConstr where PCId=@PCId";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Add4(string NewsID, int gPoint, int iPoint, int Money1, DateTime CreatTime1, string UserNum, string content4)
        {
            SqlParameter[] param = new SqlParameter[7] ;
            param[0] = new SqlParameter("@NewsID",SqlDbType.NVarChar,12);
            param[0].Value = NewsID;
            param[1] = new SqlParameter("@gPoint",SqlDbType.Int,4);
            param[1].Value = gPoint;
            param[2] = new SqlParameter("@iPoint",SqlDbType.Int,4);
            param[2].Value = iPoint;
            param[3] = new SqlParameter("@Money1",SqlDbType.Int,4);
            param[3].Value = Money1;
            param[4] = new SqlParameter("@CreatTime1",SqlDbType.DateTime,8);
            param[4].Value = CreatTime1;
            param[5] = new SqlParameter("@UserNum",SqlDbType.NVarChar,12);
            param[5].Value = UserNum;
            param[6] = new SqlParameter("@content4",SqlDbType.NText);
            param[6].Value = content4;

            string Sql = "insert into " + Pre + "User_Ghistory(GhID,ghtype,Gpoint,iPoint,Money,CreatTime,UserNUM,gtype,content,siteID) values(@NewsID,1,@gPoint,@iPoint,@Money1,@CreatTime1,@UserNum,4,@content4,'" + Foosun.Global.Current.SiteID + "')";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public DataTable sel15(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
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
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
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
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
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
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
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
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select UserName,ParmConstrNum from " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel21(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
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
            SqlParameter[] param = new SqlParameter[4] ;
            param[0] = new SqlParameter("@userNum",SqlDbType.NVarChar,12);
            param[0].Value = UserNum1;
            param[1] = new SqlParameter("@ParmConstrNums",SqlDbType.Int,4);
            param[1].Value = ParmConstrNums;
            param[2] = new SqlParameter("@payTime",SqlDbType.DateTime,8);
            param[2].Value = payTime;
            param[3] = new SqlParameter("@constrPayID",SqlDbType.NVarChar,12);
            param[3].Value = constrPayID;

            string Sql = "insert into " + Pre + "user_constrPay(userNum,Money,payTime,constrPayID,SiteID,PayAdmin) values(@userNum,@ParmConstrNums,@payTime,@constrPayID,'" + Foosun.Global.Current.SiteID + "','" + Foosun.Global.Current.UserName + "')";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        public int Update5(string UserNum1)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum1);
            string Sql = "update " + Pre + "sys_user set ParmConstrNum=0 where UserNum=@UserNum";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region Constr_Return.aspx
        public DataTable sel23(string ConID)
        {
            SqlParameter param = new SqlParameter("@ConID", ConID);
            string Sql = "select Title,ispass from " + Pre + "User_Constr where ConID=@ConID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update6(string passcontent, string ConIDs)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@passcontent", SqlDbType.NText);
            param[0].Value = passcontent;
            param[1] = new SqlParameter("@ConID", SqlDbType.NVarChar, 12);
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
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@PCId", SqlDbType.NVarChar, 18);
            param[0].Value = PCId;
            param[1] = new SqlParameter("@ConstrPayName", SqlDbType.NVarChar, 20);
            param[1].Value = ConstrPayName;
            param[2] = new SqlParameter("@gpoint", SqlDbType.Int, 4);
            param[2].Value = gpoint;
            param[3] = new SqlParameter("@ipoint", SqlDbType.Int, 4);
            param[3].Value = ipoint;
            param[4] = new SqlParameter("@moneys1", SqlDbType.Int, 4);
            param[4].Value = moneys1;
            param[5] = new SqlParameter("@Gunit", SqlDbType.NVarChar, 10);
            param[5].Value = Gunit;
            string Sql = "insert into " + Pre + "sys_ParmConstr(PCId,ConstrPayName,gPoint,iPoint,money,Gunit,SiteID) values(@PCId,@ConstrPayName,@gpoint,@ipoint,@moneys1,@Gunit,'" + Foosun.Global.Current.SiteID + "')";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
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
            SqlParameter param = new SqlParameter("@PCId", ID);
            string Sql = " delete " + Pre + "sys_ParmConstr where PCId=@PCId";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region Constr_SetParamup.aspx
        public DataTable sel25(string PCIdup)
        {
            SqlParameter param = new SqlParameter("@PCId", PCIdup);
            string Sql = "select ConstrPayName,gPoint,iPoint,money,Gunit from " + Pre + "sys_ParmConstr where PCId=@PCId";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update6(string ConstrPayName, string gpoint, string ipoint, int moneys1, string Gunit, string PCIdup)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@PCId", SqlDbType.NVarChar, 18);
            param[0].Value = PCIdup;
            param[1] = new SqlParameter("@ConstrPayName", SqlDbType.NVarChar, 20);
            param[1].Value = ConstrPayName;
            param[2] = new SqlParameter("@gpoint", SqlDbType.Int, 4);
            param[2].Value = gpoint;
            param[3] = new SqlParameter("@ipoint", SqlDbType.Int, 4);
            param[3].Value = ipoint;
            param[4] = new SqlParameter("@moneys1", SqlDbType.Int, 4);
            param[4].Value = moneys1;
            param[5] = new SqlParameter("@Gunit", SqlDbType.NVarChar, 10);
            param[5].Value = Gunit;
            string Sql = "update " + Pre + "sys_ParmConstr set ConstrPayName=@ConstrPayName,gPoint=@gPoint,iPoint=@iPoint,money=@moneys1,Gunit=@Gunit where PCId=@PCId";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }
        #endregion

        #region Constr_Stat.aspx
        public int sel26(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select count(Id) from " + Pre + "User_Constr where UserNum=@UserNum and substring(Contrflg,3,1) = '1' and isadmidel=0 and ispass=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int sel27(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select count(Id) from " + Pre + "User_Constr where UserNum=@UserNum and isCheck=1  and substring(Contrflg,3,1) = '1' and isadmidel=0 and ispass=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public DataTable sel28(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select creatTime from " + Pre + "User_Constr where UserNum=@UserNum and isCheck=1  and substring(Contrflg,3,1) = '1' and isadmidel=0 and ispass=0";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int sel29(string UserNum, int m1)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select count(Id) from " + Pre + "User_Constr where UserNum=@UserNum and Month(creatTime)= '" + m1 + "'  and substring(Contrflg,3,1) = '1' and isadmidel=0 and ispass=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        #endregion

        #region paymentannals.aspx
        public DataTable sel30(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_user where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel31(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select isSuper from " + Pre + "sys_admin where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Delete6(string ID)
        {
            SqlParameter param = new SqlParameter("@constrPayID", ID);
            string Sql = "delete " + Pre + "user_constrPay where constrPayID=@constrPayID";
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
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string Sql = "select ReadNewsTemplet,NewsSavePath,NewsFileRule,FileName,checkint from " + Pre + "news_class Where ClassID=@ClassID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        #endregion
    }
}