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
    public class Message : DbBase, IMessage
    {
        #region Message_box.aspx
        public int sel(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRecyle = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Update(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set isRecyle='1' where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_3(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and issRecyle = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public int Update_1(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issRecyle='1' where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public DataTable sel_1(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select FileTF,SortType from " + Pre + "User_Message where Mid=@Mid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public DataTable sel_2(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select isRdel,issDel from " + Pre + "User_Message where Mid=@Mid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public int Delete(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "delete from " + Pre + "user_MessFiles where mID=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void Delete_1(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issDel=1 where Mid=@Mid and UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);

            string Sql1 = "update " + Pre + "user_Message set isRdel=1 where Mid=@Mid and Rec_UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql1, param);

            string Sql2 = "update " + Pre + "user_Message set isRdel=1,issDel=1 where SortType=0 and Mid=@Mid and Rec_UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql2, param);
        }

        public int Update_2(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set isRdel=0 where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int sel_4(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRdel = 0 and issDel = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public int Update_3(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issDel=0 where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_5(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRdel= 0 and issDel = 1";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Delete_3(string ID, string UserNum)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@Mid", ID), new OleDbParameter("@UserNum", UserNum) };
            string Sql = "Delete From " + Pre + "user_Message  where Mid=@Mid and UserNum=@UserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Update_4(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set isRdel='0' where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_6(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRdel = 0 and issDel = 1";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Update_5(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issDel='0'  where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_7(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select Id from " + Pre + "user_Message where Mid=@Mid and isRdel = 1 and issDel = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Update_6(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issDel='0',isRdel='0'  where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_8(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRdel = 0 and issDel = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Delete_4(string ID)
        {
            OleDbParameter param = new OleDbParameter("@Mid", ID);
            string Sql = "Delete From " + Pre + "user_Message  where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region message_write.aspx
        public DataTable sel_9(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select FriendUserNum,UserName from " + Pre + "User_Friend  where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public string sel_10(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select UserGroupNumber From " + Pre + "sys_User where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        public DataTable sel_11(string u_meGroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", u_meGroupNumber);
            string Sql = "select MessageNum,MessageGroupNum From " + Pre + "User_Group where GroupNumber=@GroupNumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public int sel_12(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select count(UserNum) from " + Pre + "User_Message where UserNum=@UserNum and issDel=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public DataTable sel_15(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "select UserNum from " + Pre + "sys_User where UserName=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public void Add(Foosun.Model.message uc)
        {
            OleDbParameter[] param = addParam(uc); 
            string Sql = "insert into " + Pre + "User_Message(" + Database.getParam(param) + ",isRead,issDel,isRdel,isRecyle,issRecyle) values(" + Database.getAParam(param) + ",'0','0','0','0','0')";
            
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        private OleDbParameter[] addParam(Foosun.Model.message uc)
        {
            OleDbParameter[] param = new OleDbParameter[10];
            param[0] = new OleDbParameter("@Mid", OleDbType.VarWChar, 12);
            param[0].Value = uc.Mid;
            param[1] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[1].Value = uc.UserNum;
            param[2] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
            param[2].Value = uc.Title;
            param[3] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[3].Value = uc.Content;
            param[4] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            param[4].Value = uc.CreatTime;
            param[5] = new OleDbParameter("@Send_DateTime", OleDbType.Date, 8);
            param[5].Value = uc.Send_DateTime;
            param[6] = new OleDbParameter("@SortType", OleDbType.Integer, 1);
            param[6].Value = uc.SortType;
            param[7] = new OleDbParameter("@Rec_UserNum", OleDbType.VarWChar, 15);
            param[7].Value = uc.Rec_UserNum;
            param[8] = new OleDbParameter("@FileTF", OleDbType.Integer, 1);
            param[8].Value = uc.FileTF;
            param[9] = new OleDbParameter("@LevelFlag", OleDbType.Integer, 1);
            param[9].Value = uc.LevelFlag;
            return param;
        }


        public int Add_1(string MfID, string Mid, string UserNum, string fileName, string FileUrl, DateTime CreatTime)
        {
            OleDbParameter[] param = new OleDbParameter[6];
            param[0] = new OleDbParameter("@MfID",OleDbType.VarWChar,18 );
            param[0].Value = MfID;
            param[1] = new OleDbParameter("@Mid",OleDbType.VarWChar,18 );
            param[1].Value = Mid;
            param[2] = new OleDbParameter("@UserNum",OleDbType.VarWChar,16 );
            param[2].Value = UserNum;
            param[3] = new OleDbParameter("@fileName",OleDbType.VarWChar,50 );
            param[3].Value = fileName;
            param[4] = new OleDbParameter("@FileUrl",OleDbType.VarWChar,100 );
            param[4].Value = FileUrl;
            param[5] = new OleDbParameter("@CreatTime",OleDbType.Date,8 );
            param[5].Value = CreatTime;
            string Sql = "insert into " + Pre + "User_MessFiles("+Database.getParam(param)+") values("+Database.getAParam(param)+")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        #endregion

        #region Message_red.aspx
        public int sel_16(string Mid)
        {
            OleDbParameter param = new OleDbParameter("@Mid", Mid);
            string Sql = "select count(Mid) from " + Pre + "User_Message where Mid=@Mid";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public void Update_7(string Mid)
        {
            OleDbParameter param = new OleDbParameter("@Mid", Mid);
            string Sql = "update " + Pre + "User_Message set isRead='1' where Mid=@Mid";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public DataTable sel_17(string Mid)
        {
            OleDbParameter param = new OleDbParameter("@Mid", Mid);
            string Sql = "select Title,Content,Rec_UserNum,FileTF,LevelFlag  from " + Pre + "User_Message where Mid=@Mid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public string sel_18(string Rec_UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", Rec_UserNum);
            string Sql = "select UserName from " + Pre + "sys_User where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        public DataTable sel_19(string Mids)
        {
            OleDbParameter param = new OleDbParameter("@MID", Mids);
            string Sql = "select FileName,FileUrl from " + Pre + "User_MessFiles where mID=@MID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public string sel_20(string Midw)
        {
            OleDbParameter param = new OleDbParameter("@Mid", Midw);
            string Sql = "select FileTF  from " + Pre + "User_Message where Mid=@Mid";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 清除无用的消息
        /// </summary>
        /// <returns></returns>
        public int clearmessage()
        {
            int j = 0;
            string Sql = "select ID,Mid from " + Pre + "User_Message where issDel=1 and isRdel=1 order by id desc";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Sql1 = "delete from " + Pre + "User_Message where id=" + int.Parse(dt.Rows[i]["id"].ToString()) + "";
                    DbHelper.ExecuteNonQuery(CommandType.Text, Sql1, null);

                    string Sql2 = "delete from " + Pre + "user_MessFiles where mID='" + dt.Rows[i]["Mid"].ToString() + "'";
                    DbHelper.ExecuteNonQuery(CommandType.Text, Sql2, null);
                    j++;
                }
            }
            return j;
        }

        public void clearmessagerecyle()
        {
            string Sql1 = "delete from " + Pre + "User_Message where issRecyle=1 and isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql1, null);
        }

        #endregion
    }
}
