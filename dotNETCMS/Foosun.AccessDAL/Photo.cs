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
    public class Photo : DbBase, IPhoto
    {
        #region photo.aspx
        public DataTable sel(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "select pwd,UserName from " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_1(string PhotoalbumIDs)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumIDs);
            string Sql = "select pwd from " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public DataTable sel_13(string photoID)
        {
            OleDbParameter param = new OleDbParameter("@photoID", photoID);
            string Sql = "Select PhotoID,PhotoName,PhotoTime,UserNum,PhotoContent,PhotoalbumID,PhotoUrl From " + Pre + "user_photo where PhotoID=@photoID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_14(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "Select PhotoalbumName From " + Pre + "user_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_20(string PhotoalbumIDs)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumIDs);
            string Sql = "select UserName from " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        #endregion

        #region photo_add.aspx
        public DataTable sel_2(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select PhotoalbumName,PhotoalbumID From " + Pre + "User_Photoalbum where isDisPhotoalbum=0 and UserName=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_3()
        {
            string Sql = "select PhotoID from " + Pre + "User_Photo";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add(STPhoto Pho, string UserNum, string PhotoalbumID, string PhotoUrl, string PhotoID)
        {
            OleDbParameter[] parm = GetPhoto(Pho);
            int i_length = parm.Length;
            Array.Resize<OleDbParameter>(ref parm, i_length + 4);
            parm[i_length] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 18);
            parm[i_length].Value = UserNum;
            parm[i_length + 1] = new OleDbParameter("@PhotoalbumID", OleDbType.VarWChar, 18);
            parm[i_length + 1].Value = PhotoalbumID;
            parm[i_length + 2] = new OleDbParameter("@PhotoUrl", OleDbType.VarWChar, 200);
            parm[i_length + 2].Value = PhotoUrl;
            parm[i_length + 3] = new OleDbParameter("@PhotoID", OleDbType.VarWChar, 18);
            parm[i_length + 3].Value = PhotoID;
            string Sql = "insert into " + Pre + "User_Photo("+Database.getParam(parm)+") values("+Database.getAParam(parm)+")";
            
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }
        private OleDbParameter[] GetPhoto(STPhoto Pho)
        {
            OleDbParameter[] parm = new OleDbParameter[3];
            parm[0] = new OleDbParameter("@PhotoName", OleDbType.VarWChar, 18);
            parm[0].Value = Pho.PhotoName;
            parm[1] = new OleDbParameter("@PhotoContent", OleDbType.VarWChar, 200);
            parm[1].Value = Pho.PhotoContent;
            parm[2] = new OleDbParameter("@PhotoTime", OleDbType.Date);
            parm[2].Value = DateTime.Now;
            return parm;
        }
        #endregion

        #region photo_del.aspx
        public int Delete(string PhotoID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoID", PhotoID);
            string Sql = "delete from " + Pre + "User_Photo  where PhotoID=@PhotoID";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region photo_up.aspx
        public DataTable sel_4(string PhotoID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoID", PhotoID);
            string Sql = "select PhotoName,PhotoalbumID,PhotoContent,PhotoUrl from " + Pre + "User_Photo where PhotoID=@PhotoID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_5(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "Select PhotoalbumName From " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public string sel_6(string PhotoIDs)
        {
            OleDbParameter param = new OleDbParameter("@PhotoID", PhotoIDs);
            string Sql = "select PhotoUrl from " + Pre + "User_Photo where PhotoID=@PhotoID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update(string PhotoName, DateTime PhotoTime, string PhotoalbumID, string PhotoContent, string PhotoUrl1, string PhotoIDs)
        {
            OleDbParameter[] param = new OleDbParameter[6];
            param[0] = new OleDbParameter("@PhotoName", OleDbType.VarWChar, 18);
            param[0].Value = PhotoName;
            param[1] = new OleDbParameter("@PhotoTime", OleDbType.Date, 8);
            param[1].Value = PhotoTime;
            param[2] = new OleDbParameter("@PhotoalbumID", OleDbType.VarWChar, 18);
            param[2].Value = PhotoalbumID;
            param[3] = new OleDbParameter("@PhotoContent", OleDbType.VarWChar, 200);
            param[3].Value = PhotoContent;
            param[4] = new OleDbParameter("@PhotoUrl", OleDbType.VarWChar, 200);
            param[4].Value = PhotoUrl1;
            param[5] = new OleDbParameter("@PhotoID", OleDbType.VarWChar, 200);
            param[5].Value = PhotoIDs;
            //经检查参数顺序一致 arjun
            string Sql = "update " + Pre + "User_Photo set PhotoName=@PhotoName,PhotoTime=@PhotoTime,PhotoalbumID=@PhotoalbumID,PhotoContent=@PhotoContent,PhotoUrl=@PhotoUrl where PhotoID=@PhotoID";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region Photoalbum.aspx
        public DataTable sel_7(string UserNum)
        {
            string Sql = "Select ClassName,ClassID From " + Pre + "user_PhotoalbumClass where UserName='" + UserNum + "' and isDisclass=0";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Add_1(STPhotoalbum Pb, string UserNum)
        {
            ///经检查ＳＱＬ参数顺序一致 arjun
            //string Sql = "insert into " + Pre + "user_Photoalbum(PhotoalbumName,PhotoalbumID,Creatime,UserName,PhotoalbumJurisdiction,isDisPhotoalbum,pwd,PhotoalbumUrl,DisID,ClassID) values(@PhotoalbumName,@PhotoalbumID,@Creatime,@UserNum,@PhotoalbumJurisdiction,@isDisPhotoalbum,@pwd,@PhotoalbumUrl,@DisID,@ClassID)";
            //OleDbParameter[] parm = GetPhotoalbum(Pb);
            //int i_length = parm.Length;
            //Array.Resize<OleDbParameter>(ref parm, i_length + 1);
            //parm[i_length] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 18);
            //parm[i_length].Value = UserNum;
            //bug修改 周峻平 2008-6-20 Access数据库不支持存储过程
            string Sql = "insert into " + Pre + "user_Photoalbum(PhotoalbumName,PhotoalbumID,Creatime,UserName,PhotoalbumJurisdiction,isDisPhotoalbum,pwd,PhotoalbumUrl,DisID,ClassID) values('" + Pb.PhotoalbumName + "','" + Rand.Number(12) + "','" + DateTime.Now + "','" + UserNum + "','" + Pb.PhotoalbumJurisdiction + "'," + Pb.isDisPhotoalbum + ",'" + Pb.pwd + "','" + Pb.PhotoalbumUrl + "','" + Pb.DisID + "','" + Pb.ClassID + "')";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        private OleDbParameter[] GetPhotoalbum(STPhotoalbum Pb)
        {
            OleDbParameter[] parm = new OleDbParameter[9];
            parm[0] = new OleDbParameter("@PhotoalbumName", OleDbType.VarWChar, 18);
            parm[0].Value = Pb.PhotoalbumName;
            parm[1] = new OleDbParameter("@PhotoalbumID", OleDbType.VarWChar, 200);
            parm[1].Value = Rand.Number(12);
            parm[2] = new OleDbParameter("@Creatime", OleDbType.Date);
            parm[2].Value = DateTime.Now;
            parm[3] = new OleDbParameter("@PhotoalbumJurisdiction", OleDbType.VarWChar, 200);
            parm[3].Value = Pb.PhotoalbumJurisdiction;
            parm[4] = new OleDbParameter("@isDisPhotoalbum", OleDbType.VarWChar, 200);
            parm[4].Value = Pb.isDisPhotoalbum;
            parm[5] = new OleDbParameter("@pwd", OleDbType.VarWChar, 200);
            parm[5].Value = Pb.pwd;
            parm[6] = new OleDbParameter("@PhotoalbumUrl", OleDbType.VarWChar, 200);
            parm[6].Value = Pb.PhotoalbumUrl;
            parm[7] = new OleDbParameter("@DisID", OleDbType.VarWChar, 200);
            parm[7].Value = Pb.DisID;
            parm[8] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 200);
            parm[8].Value = Pb.ClassID;
            return parm;
        }
        #endregion

        #region Photoalbum_up.aspx
        public DataTable sel_8(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "select pwd,PhotoalbumName,PhotoalbumJurisdiction,ClassID from " + Pre + "user_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public string sel_9(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string Sql = "select ClassName from " + Pre + "user_PhotoalbumClass where ClassID=@ClassID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_1(string PhotoalbumName, string PhotoalbumJurisdiction, string ClassID, DateTime Creatime, string PhotoalbumIDs)
        {
            OleDbParameter[] param = new OleDbParameter[5];
            param[0] = new OleDbParameter("@PhotoalbumName", OleDbType.VarWChar, 200);
            param[0].Value = PhotoalbumName;
            param[1] = new OleDbParameter("@PhotoalbumJurisdiction", OleDbType.VarWChar, 50);
            param[1].Value = PhotoalbumJurisdiction;
            param[2] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 18);
            param[2].Value = ClassID;
            param[3] = new OleDbParameter("@Creatime", OleDbType.VarWChar, 200);
            param[3].Value = Creatime;
            param[4] = new OleDbParameter("@PhotoalbumIDs", OleDbType.VarWChar, 18);
            param[4].Value = PhotoalbumIDs;
            //经修改后参数顺序一致 arjun
            string Sql = "update " + Pre + "user_Photoalbum set PhotoalbumName=@PhotoalbumName,PhotoalbumJurisdiction=@PhotoalbumJurisdiction,ClassID=@ClassID,Creatime=@Creatime where PhotoalbumID=@PhotoalbumIDs";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_10()
        {
            string Sql = "select pwd from " + Pre + "user_Photoalbum";
            return DbHelper.ExecuteScalar(CommandType.Text, Sql, null).ToString();
        }
        public int Update_2(string newpwds, string PhotoalbumIDs)
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@newpwds", OleDbType.VarWChar, 18);
            param[0].Value = newpwds;
            param[1] = new OleDbParameter("@PhotoalbumIDs", OleDbType.VarWChar, 18);
            param[1].Value = PhotoalbumIDs;

            //经检查参数顺序一致 arjun
            string Sql = "update " + Pre + "user_Photoalbum set pwd=@newpwds where PhotoalbumID=@PhotoalbumIDs";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region Photoalbumlist.aspx
        public string sel_11(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", ID);
            string Sql = "select PhotoalbumUrl  from " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Delete_1(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", ID);
            //缺少from arjun
            //string Sql = "delete " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            string Sql = "delete from " + Pre + "User_Photoalbum where PhotoalbumID=@PhotoalbumID";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete_2(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", ID);
            string Sql = "delete from  " + Pre + "User_Photo where PhotoalbumID=@PhotoalbumID";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public string sel_12(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserName from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int sel_19(string ID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", ID);
            string Sql = "select count(*) from " + Pre + "User_Photo where PhotoalbumID=@PhotoalbumID";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public DataTable GetPage(string UserNum, string ClassID, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string Sql = "";
            if (UserNum != null && UserNum != "")
            {
                Sql = " and UserName='" + UserNum + "'";
            }
            if (ClassID != "" && ClassID != null)
            {
                Sql += "and ClassID='" + ClassID + "'";
            }
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserName", UserNum), new OleDbParameter("@ClassID", ClassID) };
            string AllFields = "PhotoalbumName,PhotoalbumID,UserName,Creatime,pwd";
            string Condition = "" + Pre + "User_Photoalbum where isDisPhotoalbum=0 " + Sql + "";
            string IndexField = "id";
            string OrderFields = "order by ID desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, param);
        }
        #endregion

        #region photoclass.aspx
        public int Delete_3(string ID)
        {
            
            OleDbParameter param = new OleDbParameter("@ClassID", ID);
            //缺少from arjun
            //string Sql = "delete " + Pre + "user_PhotoalbumClass where ClassID=@ClassID";
            string Sql = "delete from " + Pre + "user_PhotoalbumClass where ClassID=@ClassID";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete_4(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ID);
            //缺少from arjun
            //string Sql = "delete " + Pre + "User_Photoalbum  where ClassID=@ClassID";
            string Sql = "delete from " + Pre + "User_Photoalbum  where ClassID=@ClassID";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public DataTable sel_15(string ID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ID);
            string Sql = "select PhotoalbumName,PhotoalbumUrl  from " + Pre + "User_Photoalbum where ClassID=@ClassID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public DataTable sel_16()
        {
            string Sql = "select AId,UserNum from " + Pre + "user_DiscussActiveMember";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        #endregion

        #region  photoclass_add.aspx
        public int Add_2(string ClassName, string ClassID, DateTime Creatime, string UserNum, int isDisclass, string DisID)
        {
            OleDbParameter[] param = new OleDbParameter[6];
            param[0] = new OleDbParameter("@ClassName", OleDbType.VarWChar, 50);
            param[0].Value = ClassName;
            param[1] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 18);
            param[1].Value = ClassID;
            param[2] = new OleDbParameter("@Creatime", OleDbType.Date, 8);
            param[2].Value = Creatime;
            param[3] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 18);
            param[3].Value = UserNum;
            param[4] = new OleDbParameter("@isDisclass", OleDbType.Integer, 4);
            param[4].Value = isDisclass;
            param[5] = new OleDbParameter("@DisID", OleDbType.VarWChar, 18);
            param[5].Value = DisID;
            //经检查参数顺序一致 arjun
            string Sql = "insert into " + Pre + "user_PhotoalbumClass(ClassName,ClassID,Creatime,UserName,isDisclass,DisID) values(@ClassName,@ClassID,@Creatime,@UserNum,@isDisclass,@DisID)";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region photoclass_up.aspx
        public string sel_17(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string Sql = "select  ClassName  from  " + Pre + "user_PhotoalbumClass where ClassID=@ClassID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int Update_3(string ClassName, DateTime Creatime, string ClassIDs)
        {
            OleDbParameter[] param = new OleDbParameter[3];
            param[0] = new OleDbParameter("@ClassName", OleDbType.VarWChar, 50);
            param[0].Value = ClassName;
            param[1] = new OleDbParameter("@Creatime", OleDbType.Date, 8);
            param[1].Value = Creatime;
            param[2] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 18);
            param[2].Value = ClassIDs;
            //经检查参数顺序一致 arjun
            string Sql = "update  " + Pre + "user_PhotoalbumClass set ClassName=@ClassName,Creatime=@Creatime where ClassID=@ClassID";
            return (int)DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion

        public DataTable sel_18(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "Select PhotoUrl From " + Pre + "user_photo where PhotoalbumID=@PhotoalbumID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

    }
}