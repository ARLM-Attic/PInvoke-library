using System;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using Foosun.Common;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Message : DbBase, IMessage
    {
        #region Message_box.aspx
        public int sel(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRecyle = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Update(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set isRecyle='1' where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_3(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and issRecyle = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public int Update_1(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issRecyle='1' where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public DataTable sel_1(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select FileTF,SortType from " + Pre + "User_Message where Mid=@Mid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public DataTable sel_2(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select isRdel,issDel from " + Pre + "User_Message where Mid=@Mid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public int Delete(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "delete from " + Pre + "user_MessFiles where mID=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void Delete_1(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issDel=1 where Mid=@Mid and UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);

            string Sql1 = "update " + Pre + "user_Message set isRdel=1 where Mid=@Mid and Rec_UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql1, param);

            string Sql2 = "update " + Pre + "user_Message set isRdel=1,issDel=1 where SortType=0 and Mid=@Mid and Rec_UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql2, param);
        }

        public int Update_2(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set isRdel=0 where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public int sel_4(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRdel = 0 and issDel = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public int Update_3(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issDel=0 where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_5(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRdel= 0 and issDel = 1";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Delete_3(string ID, string UserNum)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@Mid", ID), new SqlParameter("@UserNum", UserNum) };
            string Sql = "Delete From " + Pre + "user_Message  where Mid=@Mid and UserNum=@UserNum";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Update_4(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set isRdel='0' where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_6(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRdel = 0 and issDel = 1";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Update_5(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issDel='0'  where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_7(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select Id from " + Pre + "user_Message where Mid=@Mid and isRdel = 1 and issDel = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Update_6(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "update " + Pre + "user_Message set issDel='0',isRdel='0'  where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int sel_8(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "select count(Id) from " + Pre + "user_Message where Mid=@Mid and isRdel = 0 and issDel = 0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Delete_4(string ID)
        {
            SqlParameter param = new SqlParameter("@Mid", ID);
            string Sql = "Delete From " + Pre + "user_Message  where Mid=@Mid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region message_write.aspx
        public DataTable sel_9(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select FriendUserNum,UserName from " + Pre + "User_Friend  where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public string sel_10(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select UserGroupNumber From " + Pre + "sys_User where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        public DataTable sel_11(string u_meGroupNumber)
        {
            SqlParameter param = new SqlParameter("@GroupNumber", u_meGroupNumber);
            string Sql = "select MessageNum,MessageGroupNum From " + Pre + "User_Group where GroupNumber=@GroupNumber";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public int sel_12(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select count(UserNum) from " + Pre + "User_Message where UserNum=@UserNum and issDel=0";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public DataTable sel_15(string UserName)
        {
            SqlParameter param = new SqlParameter("@UserName", UserName);
            string Sql = "select UserNum from " + Pre + "sys_User where UserName=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public void Add(Foosun.Model.message uc)
        {
            string Sql = "insert into " + Pre + "User_Message(Mid,UserNum,Title,Content,CreatTime,Send_DateTime,SortType,Rec_UserNum,FileTF,LevelFlag,isRead,issDel,isRdel,isRecyle,issRecyle) values(@Mid,@UserNum,@Title,@Content,@CreatTime,@Send_DateTime,@SortType,@Rec_UserNum,@FileTF,@LevelFlag,'0','0','0','0','0')";
            SqlParameter[] param = addParam(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        private SqlParameter[] addParam(Foosun.Model.message uc)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Mid", SqlDbType.NVarChar, 12);
            param[0].Value = uc.Mid;
            param[1] = new SqlParameter("@UserNum", SqlDbType.NVarChar, 15);
            param[1].Value = uc.UserNum;
            param[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            param[2].Value = uc.Title;
            param[3] = new SqlParameter("@Content", SqlDbType.NText);
            param[3].Value = uc.Content;
            param[4] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[4].Value = uc.CreatTime;
            param[5] = new SqlParameter("@Send_DateTime", SqlDbType.DateTime, 8);
            param[5].Value = uc.Send_DateTime;
            param[6] = new SqlParameter("@SortType", SqlDbType.TinyInt, 1);
            param[6].Value = uc.SortType;
            param[7] = new SqlParameter("@Rec_UserNum", SqlDbType.NVarChar, 15);
            param[7].Value = uc.Rec_UserNum;
            param[8] = new SqlParameter("@FileTF", SqlDbType.TinyInt, 1);
            param[8].Value = uc.FileTF;
            param[9] = new SqlParameter("@LevelFlag", SqlDbType.TinyInt, 1);
            param[9].Value = uc.LevelFlag;
            return param;
        }


        public int Add_1(string MfID, string Mid, string UserNum, string fileName, string FileUrl, DateTime CreatTime)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@MfID",SqlDbType.NVarChar,18 );
            param[0].Value = MfID;
            param[1] = new SqlParameter("@Mid",SqlDbType.NVarChar,18 );
            param[1].Value = Mid;
            param[2] = new SqlParameter("@UserNum",SqlDbType.NVarChar,16 );
            param[2].Value = UserNum;
            param[3] = new SqlParameter("@fileName",SqlDbType.NVarChar,50 );
            param[3].Value = fileName;
            param[4] = new SqlParameter("@FileUrl",SqlDbType.NVarChar,100 );
            param[4].Value = FileUrl;
            param[5] = new SqlParameter("@CreatTime",SqlDbType.DateTime,8 );
            param[5].Value = CreatTime;
            string Sql = "insert into " + Pre + "User_MessFiles(MfID,mID,UserNum,FileName,FileUrl,CreatTime) values(@MfID,@Mid,@UserNum,@fileName,@FileUrl,@CreatTime)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        #endregion

        #region Message_red.aspx
        public int sel_16(string Mid)
        {
            SqlParameter param = new SqlParameter("@Mid", Mid);
            string Sql = "select count(Mid) from " + Pre + "User_Message where Mid=@Mid";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public void Update_7(string Mid)
        {
            SqlParameter param = new SqlParameter("@Mid", Mid);
            string Sql = "update " + Pre + "User_Message set isRead='1' where Mid=@Mid";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public DataTable sel_17(string Mid)
        {
            SqlParameter param = new SqlParameter("@Mid", Mid);
            string Sql = "select Title,Content,Rec_UserNum,FileTF,LevelFlag  from " + Pre + "User_Message where Mid=@Mid";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public string sel_18(string Rec_UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", Rec_UserNum);
            string Sql = "select UserName from " + Pre + "sys_User where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        public DataTable sel_19(string Mids)
        {
            SqlParameter param = new SqlParameter("@MID", Mids);
            string Sql = "select FileName,FileUrl from " + Pre + "User_MessFiles where mID=@MID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public string sel_20(string Midw)
        {
            SqlParameter param = new SqlParameter("@Mid", Midw);
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
