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
    public class User : DbBase, IUser
    {
        public DataTable CheckUser(string UserName, string Pwd)
        {
            string md5Pwd = Foosun.Common.Input.MD5(Pwd);
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@UserName", UserName), new OleDbParameter("@Pwd", md5Pwd) };
            string Sql = "select userName,UserPassword,isAdmin,islock,UserNum,SiteID from " + Pre + "sys_User where UserName=@UserName and UserPassword=@Pwd";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public DataTable CheckManage(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserNum,isSuper,adminGroupNumber,PopList,OnlyLogin,isChannel,isLock,SiteID,isChSupper,Iplimited,verCode from " + Pre + "sys_admin where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public int sel_email(string email)
        {
            OleDbParameter param = new OleDbParameter("@email", email);
            string sql = "select count(id) from " + Pre + "sys_user where Email=@email";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }

        #region 登陆限制开始
        ///// <summary>
        ///// 查找是否有此管理员
        ///// </summary>
        ///// <param name="strUserName"></param>
        ///// <returns></returns>
        //public DataTable CheckUserTF(string strUserName)
        //{
        //    string Sql = "select id from " + Pre + "sys_User where UserName='" + UserName + "' and isAdmin=1";
        //    DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        //    return dt;
        //}
        ///// <summary>
        ///// 开始读取管理员是否有限制
        ///// </summary>
        ///// <param name="UserNum"></param>
        ///// <returns></returns>
        //public DataTable readAdminlimit(string UserNum)
        //{
        //    string Sql = "select LimitType from " + Pre + "sys_admin where UserNum='" + UserNum + "'";
        //    DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        //    return dt;
        //}

        ///// <summary>
        ///// 检查是否有登陆记录
        ///// </summary>
        ///// <param name="UserNum"></param>
        ///// <returns></returns>
        //public int loginNumTF(string UserNum)
        //{
        //    int intflg = 0;
        //    string Sql = "select id from " + Pre + "sys_loginnum where UserNum='" + UserNum + "'";
        //    DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0){intflg = 1;}
        //        dt.Clear(); dt.Dispose();
        //    }
        //}

        ///// <summary>
        ///// 得到已经登陆的次数。
        ///// </summary>
        ///// <param name="UserNum"></param>
        ///// <returns></returns>
        //public int getLoginNum(string UserNum)
        //{
        //    int intflg = 0;
        //    string Sql = "select loginNum from " + Pre + "sys_loginnum where UserNum='" + UserNum + "'";
        //    DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0) { intflg = int.Parse(dt.Rows[0]["loginNum"].ToString()); }
        //        dt.Clear(); dt.Dispose();
        //    }
        //}

        ///// <summary>
        ///// 插入登陆记录
        ///// </summary>
        ///// <param name="UserNum"></param>
        //public void insertLoginNum(string UserNum)
        //{
        //    string Sql = "insert into " + Pre + "sys_loginnum(";
        //    Sql += "UserNum,loginNum,creatTime";
        //    Sql += ") values (";
        //    Sql += "'" + UserNum + "',1,'" + DateTime.Now + "')";
        //    DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        //}
        #endregion 登陆限制结束
        #region 管理员验证
        public int Managestate(string strUserNum)
        {
            int flg = 1;
            if (strUserNum != null)
            {

            }
            return flg;
        }
        #endregion 管理员验证


        #region 日历
        public void UserLogsDels(int LId)
        {
            string Sql = "Delete From  " + Pre + "user_userlogs where id=" + LId + " and UserNum = '" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public DataTable getUserLogsValue(int LID)
        {
            string Sql = "Select id,title,Content,LogDateTime,dateNum From " + Pre + "user_userlogs Where ID=" + LID + " and UserNum='" + Foosun.Global.Current.UserNum + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public DataTable getUserLogsRecord(string LogID)
        {
            OleDbParameter param = new OleDbParameter("@LogID", LogID);
            string Sql = "Select logID From " + Pre + "user_userlogs Where logID=@LogID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public DataTable getCountselt(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "select count(*) from  " + Pre + "user_Requestinformation where bUsername=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public DataTable getIschick(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "select ischick from  " + Pre + "User_Requestinformation where bUsername=@UserName";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        public DataTable isAdminUser(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select isAdmin from " + Pre + "sys_User where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        /// <summary>
        /// 则插入新记录日历
        /// </summary>
        /// <param name="uc2"></param>
        public void InsertUserLogs(Foosun.Model.UserLog1 uc2)
        {

            string Sql = "insert into " + Pre + "user_userlogs(";
            Sql += "LogID,title,content,creatTime,dateNum,LogDateTime,usernum,SiteID";
            Sql += ") values (";
            Sql += "@LogID,@title,@content,@creatTime,@dateNum,@LogDateTime,@usernum,'" + Foosun.Global.Current.SiteID + "')";

            OleDbParameter[] parm = Database.getNewParam(InsertUserLogsParameters(uc2), "LogID,title,content,creatTime,dateNum,LogDateTime,usernum");
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserLog1构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertUserLogsParameters(Foosun.Model.UserLog1 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[8];
            param[0] = new OleDbParameter("@LogID", OleDbType.VarWChar, 12);
            param[0].Value = uc1.LogID;
            param[1] = new OleDbParameter("@title", OleDbType.VarWChar, 50);
            param[1].Value = uc1.title;
            param[2] = new OleDbParameter("@content", OleDbType.VarWChar);
            param[2].Value = uc1.content;
            param[3] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[3].Value = uc1.creatTime;
            param[4] = new OleDbParameter("@dateNum", OleDbType.SmallInt, 2);
            param[4].Value = uc1.dateNum;
            param[5] = new OleDbParameter("@LogDateTime", OleDbType.Date, 8);
            param[5].Value = uc1.LogDateTime;
            param[6] = new OleDbParameter("@usernum", OleDbType.VarWChar, 15);
            param[6].Value = uc1.usernum;
            param[7] = new OleDbParameter("@Id", OleDbType.Integer, 4);
            param[7].Value = uc1.Id;
            return param;
        }


        /// <summary>
        /// 则更新记录日历
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateUserLogs(Foosun.Model.UserLog1 uc2)
        {
            OleDbParameter[] parm = UpdateUserLogsParameters(uc2);
            Array.Resize<OleDbParameter>(ref parm, parm.Length - 1);
            string Sql = "update " + Pre + "user_userlogs set "+Database.getModifyParam(parm)+" where Id=" + uc2.Id + " and userNum='" + Foosun.Global.Current.UserNum + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserLog1构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] UpdateUserLogsParameters(Foosun.Model.UserLog1 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[5];
            param[0] = new OleDbParameter("@title", OleDbType.VarWChar, 50);
            param[0].Value = uc1.title;
            param[1] = new OleDbParameter("@content", OleDbType.VarWChar);
            param[1].Value = uc1.content;
            param[2] = new OleDbParameter("@dateNum", OleDbType.SmallInt, 2);
            param[2].Value = uc1.dateNum;
            param[3] = new OleDbParameter("@LogDateTime", OleDbType.Date, 8);
            param[3].Value = uc1.LogDateTime;
            param[4] = new OleDbParameter("@Id", OleDbType.Integer, 4);
            param[4].Value = uc1.Id;
            return param;
        }

        #endregion 日历

        #region 会员好友添加检查



        public DataTable sel_isAdmin(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string selectUserName = "select isAdmin from " + Pre + "sys_User where UserNum=@UserNum";
            return DbHelper.ExecuteTable(CommandType.Text, selectUserName, param);
        }
        #endregion

        /// <summary>
        /// 查询用户密码是否正确
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public string sel_pwd(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select UserPassword From " + Pre + "sys_User where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        /// <summary>
        /// 登陆会员所在的会员组的过期时间
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public int sel_Rtime(string GroupName)
        {
            OleDbParameter param = new OleDbParameter("@GroupName", GroupName);
            string Sql = "select Rtime from  " + Pre + "user_Group  where GroupNumber=@GroupName";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        /// <summary>
        /// 得到用户所在的会员组编号
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public string sel_UserGroupNumber(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserGroupNumber from " + Pre + "sys_user where UserNum=@UserNum";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        /// <summary>
        /// 前台会员注册
        /// </summary>
        public int sel_ChannelID(string SiteID)
        {
            OleDbParameter param = new OleDbParameter("@SiteID", SiteID);
            string Sql = "select count(*) from " + Pre + "news_site where ChannelID=@SiteID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        int IUser.GetUncheckFriendsCount(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@bUsername", UserName);
            string Sql = "select count(*) from  " + Pre + "User_Requestinformation where bUsername=@bUsername and ischick=1";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        /// <summary>
        /// 注册会员协议
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public DataTable sel_RegContent(string SiteID)
        {
            string Sql = "select RegContent,regItem,returnemail,returnmobile,RegTF from " + Pre + "sys_PramUser";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 得到用户名是否被占用
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int sel_username(string ID)
        {
            OleDbParameter param = new OleDbParameter("@UserName", ID);
            string Sql = "select Id from " + Pre + "sys_User where UserName=@UserName";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 得到会员编号是否重复
        /// </summary>
        /// <returns></returns>
        public string sel_um()
        {
            string Sql = "select UserNum from " + Pre + "sys_User";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
        }

        public string sel_UserGroupNumbers(string SiteID)
        {
            string Sql = "select RegGroupNumber from " + Pre + "sys_PramUser";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, null));
        }

        public int sel_getUserMobileBindTF(string Moblie)
        {
            OleDbParameter param = new OleDbParameter("@Moblie", Moblie);
            int intflg = 0;
            string Sql = "select ID from " + Pre + "sys_User where BindTF=1 and mobile=@Moblie";
            object obj = DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
            if (obj != null && obj != DBNull.Value)
            {
                intflg = 1;
            }
            return intflg;
        }

        /// <summary>
        /// 捆绑手机
        /// </summary>
        /// <param name="UserName"></param>
        public void sel_updateMobileBindTF(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "Update " + Pre + "sys_User set BindTF=1 where UserName=@UserName";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        /// <summary>
        /// 得到手机验证码
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool sel_getUserMobileCode(string UserName, out string mobile, out string mobilecode)
        {
            mobile = string.Empty;
            mobilecode = string.Empty;
            bool flag = false;
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "select isMobile,mobile,MobileCode from " + Pre + "sys_User where UserName=@UserName";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            if (rd.Read())
            {
                if (!rd.IsDBNull(0) && rd.GetByte(0) != 0X00)
                    flag = true;
                if (!rd.IsDBNull(1))
                    mobile = rd.GetString(1);
                if (!rd.IsDBNull(2))
                    mobilecode = rd.GetString(2);
            }
            rd.Close();
            return flag;
        }

        /// <summary>
        /// 更新手机状态
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int sel_updateUserMobileStat(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "update " + Pre + "sys_User Set isMobile=1 where UserName=@UserName";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 创建用户到数据库中
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public int Add_User(Foosun.Model.User ui)
        {
            OleDbParameter[] param = Database.getNewParam(getUserInfo(ui), "UserNum,UserName,UserPassword,NickName,RealName,isAdmin," +
                        "UserGroupNumber,PassQuestion,PassKey,CertType,CertNumber,Email,mobile,Sex,birthday,Userinfo," +
                        "UserFace,userFacesize,marriage,iPoint,gPoint,cPoint,ePoint,aPoint,isLock,RegTime,LastLoginTime," +
                        "OnlineTime,OnlineTF,LoginNumber,FriendClass,LoginLimtNumber,LastIP,SiteID,Addfriend,isOpen," +
                        "ParmConstrNum,isIDcard,IDcardFiles,Addfriendbs,EmailATF,EmailCode,isMobile,BindTF,MobileCode");

            string Sql = "Insert Into " + Pre + "sys_User (UserNum,UserName,UserPassword,NickName,RealName,isAdmin," +
                        "UserGroupNumber,PassQuestion,PassKey,CertType,CertNumber,Email,mobile,Sex,birthday,Userinfo," +
                        "UserFace,userFacesize,marriage,iPoint,gPoint,cPoint,ePoint,aPoint,isLock,RegTime,LastLoginTime," +
                        "OnlineTime,OnlineTF,LoginNumber,FriendClass,LoginLimtNumber,LastIP,SiteID,Addfriend,isOpen," +
                        "ParmConstrNum,isIDcard,IDcardFiles,Addfriendbs,EmailATF,EmailCode,isMobile,BindTF,MobileCode) " +
                        "Values" +
                        "(@UserNum,@UserName,@UserPassword,@NickName,@RealName,@isAdmin,@UserGroupNumber,@PassQuestion," +
                        "@PassKey,@CertType,@CertNumber,@Email,@mobile,@Sex,@birthday,@Userinfo,@UserFace,@userFacesize," +
                        "@marriage,@iPoint,@gPoint,@cPoint,@ePoint,@aPoint,@isLock,@RegTime,@LastLoginTime,@OnlineTime," +
                        "@OnlineTF,@LoginNumber,@FriendClass,@LoginLimtNumber,@LastIP,@SiteID,@Addfriend,@isOpen," +
                        "@ParmConstrNum,@isIDcard,@IDcardFiles,@Addfriendbs,@EmailATF,@EmailCode,@isMobile,@BindTF,@MobileCode)";
            return Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param));
        }

        /// <summary>
        /// 创建用户附表
        /// </summary>
        /// <param name="suf"></param>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public int Add_userfields(Foosun.Model.UserFields ufi)
        {
            OleDbParameter[] param = Database.getNewParam(getUuserfields(ufi), "UserNum,province,City,Address,Postcode,FaTel,WorkTel," +
                         "QQ,MSN,Fax,character,UserFan,Nation,nativeplace,Job,education,Lastschool,orgSch");
            string Sql = "insert into " + Pre + "sys_userfields (UserNum,province,City,Address,Postcode,FaTel,WorkTel," +
                         "QQ,MSN,Fax,[character],UserFan,[Nation],nativeplace,[Job],education,Lastschool,orgSch) " +
                         "values" +
                         "(@userNum,@province,@City,@Address,@Postcode,@FaTel,@WorkTel,@QQ,@MSN,@Fax,@character,@UserFan," +
                         "@Nation,@nativeplace,@Job,@education,@Lastschool,@orgSch)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        /// <summary>
        ///  插入收入支出历史
        /// </summary>
        /// <param name="ugi"></param>
        /// <returns></returns>
        public int Add_Ghistory(Foosun.Model.UserGhistory ugi)
        {
            OleDbParameter[] param = Database.getNewParam(getUserGhistory(ugi), "GhID,ghtype,Gpoint,iPoint,Money,CreatTime,UserNUM,gtype,content,SiteID");
            string Sql = "insert into " + Pre + "User_Ghistory(GhID,ghtype,Gpoint,iPoint,[Money],CreatTime,UserNUM,gtype," +
                         "content,SiteID) values(@GhID,@ghtype,@Gpoint,@iPoint,@Money,@CreatTime,@userNum,@gtype,@content,@SiteID)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        private OleDbParameter[] getUserGhistory(Foosun.Model.UserGhistory ugi)
        {
            OleDbParameter[] parm = new OleDbParameter[11];
            parm[0] = new OleDbParameter("@id", OleDbType.Integer, 4);
            parm[0].Value = ugi.Id;
            parm[1] = new OleDbParameter("@GhID", OleDbType.VarWChar, 12);
            parm[1].Value = ugi.GhID;
            parm[2] = new OleDbParameter("@ghtype", OleDbType.Integer, 4);
            parm[2].Value = ugi.ghtype;

            parm[3] = new OleDbParameter("@Gpoint", OleDbType.Integer, 4);
            parm[3].Value = ugi.Gpoint;
            parm[4] = new OleDbParameter("@iPoint", OleDbType.Integer, 4);
            parm[4].Value = ugi.iPoint;
            parm[5] = new OleDbParameter("@Money", OleDbType.Currency, 8);
            parm[5].Value = ugi.Money;

            parm[6] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            parm[6].Value = ugi.CreatTime;
            parm[7] = new OleDbParameter("@userNum", OleDbType.VarWChar, 12);
            parm[7].Value = ugi.userNum;
            parm[8] = new OleDbParameter("@gtype", OleDbType.Integer, 4);
            parm[8].Value = ugi.gtype;

            parm[9] = new OleDbParameter("@content", OleDbType.VarWChar);
            parm[9].Value = ugi.content;
            parm[10] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            parm[10].Value = ugi.SiteID;
            return parm;
        }

        private OleDbParameter[] getUuserfields(Foosun.Model.UserFields ufi)
        {
            OleDbParameter[] parm = new OleDbParameter[19];
            parm[0] = new OleDbParameter("@id", OleDbType.Integer, 4);
            parm[0].Value = ufi.ID;
            parm[1] = new OleDbParameter("@userNum", OleDbType.VarWChar, 15);
            parm[1].Value = ufi.userNum;
            parm[2] = new OleDbParameter("@province", OleDbType.VarWChar, 20);
            parm[2].Value = ufi.province;

            parm[3] = new OleDbParameter("@City", OleDbType.VarWChar, 20);
            parm[3].Value = ufi.City;
            parm[4] = new OleDbParameter("@Address", OleDbType.VarWChar, 50);
            parm[4].Value = ufi.Address;
            parm[5] = new OleDbParameter("@Postcode", OleDbType.VarWChar, 10);
            parm[5].Value = ufi.Postcode;

            parm[6] = new OleDbParameter("@FaTel", OleDbType.VarWChar, 30);
            parm[6].Value = ufi.FaTel;
            parm[7] = new OleDbParameter("@WorkTel", OleDbType.VarWChar, 30);
            parm[7].Value = ufi.WorkTel;
            parm[8] = new OleDbParameter("@QQ", OleDbType.VarWChar, 30);
            parm[8].Value = ufi.QQ;

            parm[9] = new OleDbParameter("@MSN", OleDbType.VarWChar, 150);
            parm[9].Value = ufi.MSN;
            parm[10] = new OleDbParameter("@Fax", OleDbType.VarWChar, 30);
            parm[10].Value = ufi.Fax;
            parm[11] = new OleDbParameter("@character", OleDbType.VarWChar);
            parm[11].Value = ufi.character;

            parm[12] = new OleDbParameter("@UserFan", OleDbType.VarWChar);
            parm[12].Value = ufi.UserFan;
            parm[13] = new OleDbParameter("@Nation", OleDbType.VarWChar, 12);
            parm[13].Value = ufi.Nation;
            parm[14] = new OleDbParameter("@nativeplace", OleDbType.VarWChar, 20);
            parm[14].Value = ufi.nativeplace;

            parm[15] = new OleDbParameter("@Job", OleDbType.VarWChar, 30);
            parm[15].Value = ufi.Job;
            parm[16] = new OleDbParameter("@education", OleDbType.VarWChar, 20);
            parm[16].Value = ufi.education;
            parm[17] = new OleDbParameter("@Lastschool", OleDbType.VarWChar, 80);
            parm[17].Value = ufi.Lastschool;

            parm[18] = new OleDbParameter("@orgSch", OleDbType.VarWChar, 10);
            parm[18].Value = ufi.orgSch;
            return parm;
        }

        public string sel_setPoint(string SiteID)
        {
            string _str = "0|0";
            string Sql = "select setPoint from " + Pre + "sys_PramUser";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (rdr != null)
            {
                if (rdr.Rows.Count > 0)
                {
                    _str = rdr.Rows[0]["setPoint"].ToString();
                    if (_str.IndexOf('|') == -1)
                    {
                        _str = "0|0";
                    }
                }
                rdr.Clear(); rdr.Dispose();
            }
            return _str;
        }

        public DataTable sel_reg(string SiteID)
        {
            string Sql = "select RegContent,regItem from " + Pre + "sys_PramUser";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 读取会员所在组允许上传的图片类型及大小
        /// </summary>
        /// <param name="groupNumber"></param>
        public string getuserUpFile(string groupNumber)
        {
            OleDbParameter param = new OleDbParameter("@groupNumber", groupNumber);
            string _STR = "jpg,gif,jpeg,bmp,png,swf,rar,zip|500|3";
            string Sql = "select upfileType,upfileSize,DayUpfilenum from " + Pre + "user_Group where GroupNumber=@groupNumber";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _STR = dt.Rows[0]["upfileType"].ToString() + "|" + dt.Rows[0]["upfileSize"].ToString() + "|" + dt.Rows[0]["DayUpfilenum"].ToString();
                }
                dt.Clear(); dt.Dispose();
            }
            return _STR;
        }
        /// <summary>
        /// 得到用户注册时间
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public string getRegTime(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string flg = "3000-1-1";
            string SQL = "select RegTime from " + Pre + "sys_user where UserNum=@UserNum";
            object obj = DbHelper.ExecuteScalar(CommandType.Text, SQL, param);
            if (obj != null && obj != DBNull.Value)
            {
                flg = obj.ToString();
            }
            return flg;
        }

        /// <summary>
        /// 得到会员的文章
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public DataTable getContent(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select top 6 id,ConID,ClassID,Title,Content,creatTime,PicURL,isCheck from " + Pre + "user_Constr where UserNum=@UserNum order by id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        /// <summary>
        /// 得到会员的讨论组
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public DataTable getGroup(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
            string Sql = "select top 6 DisID,Cname,D_Content,Creatime from " + Pre + "user_Discuss where UserName=@UserName order by id desc";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        #region 得到wap
        public DataTable getWapParam()
        {
            string Sql = "select top 1 WapTF,WapPath,WapDomain,WapLastNum from " + Pre + "sys_Pramother";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public IDataReader getWapContent(string SiteID)
        {
            OleDbParameter param = new OleDbParameter("@SiteID", SiteID);
            string Sql = "select id,NewsID,NewsTitle,CreatTime from " + Pre + "News where Mid(NewsProperty,13,1)='1' and siteID=@SiteID order by id desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, param);
        }
        #endregion wap结束

        /// <summary>
        /// 构造会员参数
        /// </summary>
        /// <param name="ui">会员实体类</param>
        /// <returns></returns>
        public OleDbParameter[] getUserInfo(Foosun.Model.User ui)
        {
            OleDbParameter[] parm = new OleDbParameter[46];
            parm[0] = new OleDbParameter("@Id", OleDbType.Integer, 4);
            parm[0].Value = ui.Id;
            parm[1] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            parm[1].Value = ui.UserNum;
            parm[2] = new OleDbParameter("@UserName", OleDbType.VarWChar, 18);
            parm[2].Value = ui.UserName;

            parm[3] = new OleDbParameter("@UserPassword", OleDbType.VarWChar, 32);
            parm[3].Value = ui.UserPassword;
            parm[4] = new OleDbParameter("@NickName", OleDbType.VarWChar, 20);
            parm[4].Value = ui.NickName;
            parm[5] = new OleDbParameter("@RealName", OleDbType.VarWChar, 20);
            parm[5].Value = ui.RealName;

            parm[6] = new OleDbParameter("@isAdmin", OleDbType.Integer, 4);
            parm[6].Value = ui.isAdmin;
            parm[7] = new OleDbParameter("@UserGroupNumber", OleDbType.VarWChar, 12);
            parm[7].Value = ui.UserGroupNumber;
            parm[8] = new OleDbParameter("@PassQuestion", OleDbType.VarWChar, 20);
            parm[8].Value = ui.PassQuestion;

            parm[9] = new OleDbParameter("@PassKey", OleDbType.VarWChar, 20);
            parm[9].Value = ui.PassKey;
            parm[10] = new OleDbParameter("@CertType", OleDbType.VarWChar, 15);
            parm[10].Value = ui.CertType;
            parm[11] = new OleDbParameter("@CertNumber", OleDbType.VarWChar, 20);
            parm[11].Value = ui.CertNumber;

            parm[12] = new OleDbParameter("@Email", OleDbType.VarWChar, 220);
            parm[12].Value = ui.Email;
            parm[13] = new OleDbParameter("@mobile", OleDbType.VarWChar, 15);
            parm[13].Value = ui.mobile;
            parm[14] = new OleDbParameter("@Sex", OleDbType.Integer, 4);
            parm[14].Value = ui.Sex;

            parm[15] = new OleDbParameter("@birthday", OleDbType.Date, 8);
            parm[15].Value = ui.birthday;
            parm[16] = new OleDbParameter("@Userinfo", OleDbType.VarWChar);
            parm[16].Value = ui.Userinfo;
            parm[17] = new OleDbParameter("@UserFace", OleDbType.VarWChar, 220);
            parm[17].Value = ui.UserFace;

            parm[18] = new OleDbParameter("@userFacesize", OleDbType.VarWChar, 8);
            parm[18].Value = ui.userFacesize;
            parm[19] = new OleDbParameter("@marriage", OleDbType.Integer, 4);
            parm[19].Value = ui.marriage;
            parm[20] = new OleDbParameter("@iPoint", OleDbType.Integer, 4);
            parm[20].Value = ui.iPoint;

            parm[21] = new OleDbParameter("@gPoint", OleDbType.Integer, 4);
            parm[21].Value = ui.gPoint;
            parm[22] = new OleDbParameter("@cPoint", OleDbType.Integer, 4);
            parm[22].Value = ui.cPoint;
            parm[23] = new OleDbParameter("@ePoint", OleDbType.Integer, 4);
            parm[23].Value = ui.ePoint;

            parm[24] = new OleDbParameter("@aPoint", OleDbType.Integer, 4);
            parm[24].Value = ui.aPoint;
            parm[25] = new OleDbParameter("@isLock", OleDbType.Integer, 4);
            parm[25].Value = ui.isLock;
            parm[26] = new OleDbParameter("@RegTime", OleDbType.Date, 8);
            parm[26].Value = ui.RegTime;

            parm[27] = new OleDbParameter("@LastLoginTime", OleDbType.Date, 8);
            parm[27].Value = ui.LastLoginTime;
            parm[28] = new OleDbParameter("@OnlineTime", OleDbType.Integer, 4);
            parm[28].Value = ui.OnlineTime;
            parm[29] = new OleDbParameter("@OnlineTF", OleDbType.Integer, 4);
            parm[29].Value = ui.OnlineTF;

            parm[30] = new OleDbParameter("@LoginNumber", OleDbType.Integer, 4);
            parm[30].Value = ui.LoginNumber;
            parm[31] = new OleDbParameter("@FriendClass", OleDbType.VarWChar, 50);
            parm[31].Value = ui.FriendClass;
            parm[32] = new OleDbParameter("@LoginLimtNumber", OleDbType.Integer, 4);
            parm[32].Value = ui.LoginLimtNumber;

            parm[33] = new OleDbParameter("@LastIP", OleDbType.VarWChar, 16);
            parm[33].Value = ui.LastIP;
            parm[34] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            parm[34].Value = ui.SiteID;
            parm[35] = new OleDbParameter("@Addfriend", OleDbType.Integer, 4);
            parm[35].Value = ui.Addfriend;

            parm[36] = new OleDbParameter("@isOpen", OleDbType.Integer, 4);
            parm[36].Value = ui.isOpen;
            parm[37] = new OleDbParameter("@ParmConstrNum", OleDbType.Integer, 4);
            parm[37].Value = ui.ParmConstrNum;
            parm[38] = new OleDbParameter("@isIDcard", OleDbType.Integer, 4);
            parm[38].Value = ui.isIDcard;

            parm[39] = new OleDbParameter("@IDcardFiles", OleDbType.VarWChar, 220);
            parm[39].Value = ui.IDcardFiles;
            parm[40] = new OleDbParameter("@Addfriendbs", OleDbType.Integer, 4);
            parm[40].Value = ui.Addfriendbs;
            parm[41] = new OleDbParameter("@EmailATF", OleDbType.Integer, 4);
            parm[41].Value = ui.EmailATF;

            parm[42] = new OleDbParameter("@EmailCode", OleDbType.VarWChar, 32);
            parm[42].Value = ui.EmailCode;
            parm[43] = new OleDbParameter("@isMobile", OleDbType.Integer, 4);
            parm[43].Value = ui.isMobile;

            parm[44] = new OleDbParameter("@BindTF", OleDbType.Integer, 4);
            parm[44].Value = ui.BindTF;
            parm[45] = new OleDbParameter("@MobileCode", OleDbType.VarWChar, 32);
            parm[45].Value = ui.MobileCode;
            return parm;
        }

        /// <summary>
        /// 取得会员信息(如果传值会员自动编号为0的话，则用随机编号取值)
        /// </summary>
        /// <param name="UserNum">会员随机编号</param>
        /// <param name="ID">会员自动编号</param>
        /// <returns></returns>
        public Foosun.Model.User UserInfo(string UserNum, int ID)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "";
            if (ID != 0)
                Sql = "Select * From " + Pre + "SYS_USER Where ID=" + ID;
            else
                Sql = "Select * From " + Pre + "SYS_USER Where UserNum=@UserNum";

            Foosun.Model.User ui = new Foosun.Model.User();

            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            while (rd.Read())
            {
                ui.Id = Convert.ToInt32(rd["ID"]);
                ui.UserNum = Convert.ToString(rd["UserNum"]);
                ui.UserName = Convert.ToString(rd["UserName"]);
                ui.UserPassword = Convert.ToString(rd["UserPassword"]);
                if (rd["NickName"] == DBNull.Value) { ui.NickName = ""; } else { ui.NickName = Convert.ToString(rd["NickName"]); }
                if (rd["RealName"] == DBNull.Value) { ui.RealName = ""; } else { ui.RealName = Convert.ToString(rd["RealName"]); }
                ui.isAdmin = Convert.ToByte(rd["isAdmin"]);
                ui.UserGroupNumber = Convert.ToString(rd["UserGroupNumber"]);
                if (rd["PassQuestion"] == DBNull.Value) { ui.PassQuestion = ""; } else { ui.PassQuestion = Convert.ToString(rd["PassQuestion"]); }
                if (rd["PassKey"] == DBNull.Value) { ui.PassKey = ""; } else { ui.PassKey = Convert.ToString(rd["PassKey"]); }
                if (rd["CertType"] == DBNull.Value) { ui.CertType = ""; } else { ui.CertType = Convert.ToString(rd["CertType"]); }
                if (rd["CertNumber"] == DBNull.Value) { ui.CertNumber = ""; } else { ui.CertNumber = Convert.ToString(rd["CertNumber"]); }
                if (rd["Email"] == DBNull.Value) { ui.Email = ""; } else { ui.Email = Convert.ToString(rd["Email"]); }
                if (rd["mobile"] == DBNull.Value) { ui.mobile = ""; } else { ui.mobile = Convert.ToString(rd["mobile"]); }
                if (rd["Sex"] == DBNull.Value) { ui.Sex = 0; } else { ui.Sex = Convert.ToByte(rd["Sex"]); }
                if (rd["birthday"] == DBNull.Value) { ui.birthday = Convert.ToDateTime("1980-11-11"); } else { ui.birthday = Convert.ToDateTime(rd["birthday"]); }
                if (rd["Userinfo"] == DBNull.Value) { ui.Userinfo = ""; } else { ui.Userinfo = Convert.ToString(rd["Userinfo"]); }
                if (rd["UserFace"] == DBNull.Value) { ui.UserFace = ""; } else { ui.UserFace = Convert.ToString(rd["UserFace"]); }
                if (rd["userFacesize"] == DBNull.Value) { ui.userFacesize = ""; } else { ui.userFacesize = Convert.ToString(rd["userFacesize"]); }
                if (rd["marriage"] == DBNull.Value) { ui.marriage = 0; } else { ui.marriage = Convert.ToByte(rd["marriage"]); }
                ui.iPoint = Convert.ToInt32(rd["iPoint"]);
                ui.gPoint = Convert.ToInt32(rd["gPoint"]);
                ui.cPoint = Convert.ToInt32(rd["cPoint"]);
                ui.ePoint = Convert.ToInt32(rd["ePoint"]);
                ui.aPoint = Convert.ToInt32(rd["aPoint"]);
                ui.isLock = Convert.ToByte(rd["isLock"]);
                ui.RegTime = Convert.ToDateTime(rd["RegTime"]);
                if (rd["LastLoginTime"] == DBNull.Value) { ui.LastLoginTime = DateTime.Now; } else { ui.LastLoginTime = Convert.ToDateTime(rd["LastLoginTime"]); }
                ui.OnlineTime = Convert.ToInt32(rd["OnlineTime"]);
                ui.OnlineTF = Convert.ToInt32(rd["OnlineTF"]);
                ui.LoginNumber = Convert.ToInt32(rd["LoginNumber"]);
                if (rd["FriendClass"] == DBNull.Value) { ui.FriendClass = ""; } else { ui.FriendClass = Convert.ToString(rd["FriendClass"]); }
                if (rd["LoginLimtNumber"] == DBNull.Value) { ui.LoginLimtNumber = 0; } else { ui.LoginLimtNumber = Convert.ToInt32(rd["LoginLimtNumber"]); }
                if (rd["LastIP"] == DBNull.Value) { ui.LastIP = ""; } else { ui.LastIP = Convert.ToString(rd["LastIP"]); }
                if (rd["SiteID"] == DBNull.Value) { ui.SiteID = ""; } else { ui.SiteID = Convert.ToString(rd["SiteID"]); }
                if (rd["Addfriend"] == DBNull.Value) { ui.Addfriend = "2"; } else { ui.Addfriend = Convert.ToString(rd["Addfriend"]); }
                if (rd["isOpen"] == DBNull.Value) { ui.isOpen = 0; } else { ui.isOpen = Convert.ToByte(rd["isOpen"]); }
                if (rd["ParmConstrNum"] == DBNull.Value) { ui.ParmConstrNum = 0; } else { ui.ParmConstrNum = Convert.ToInt32(rd["ParmConstrNum"]); }
                if (rd["isIDcard"] == DBNull.Value) { ui.isIDcard = 0; } else { ui.isIDcard = Convert.ToByte(rd["isIDcard"]); }
                if (rd["IDcardFiles"] == DBNull.Value) { ui.IDcardFiles = ""; } else { ui.IDcardFiles = Convert.ToString(rd["IDcardFiles"]); }
                if (rd["Addfriendbs"] == DBNull.Value) { ui.Addfriendbs = 0; } else { ui.Addfriendbs = Convert.ToByte(rd["Addfriendbs"]); }
                if (rd["EmailATF"] == DBNull.Value) { ui.EmailATF = 0; } else { ui.EmailATF = Convert.ToByte(rd["EmailATF"]); }
                if (rd["EmailCode"] == DBNull.Value) { ui.EmailCode = ""; } else { ui.EmailCode = Convert.ToString(rd["EmailCode"]); }
                if (rd["isMobile"] == DBNull.Value) { ui.isMobile = 0; } else { ui.isMobile = Convert.ToByte(rd["isMobile"]); }
                if (rd["BindTF"] == DBNull.Value) { ui.BindTF = 0; } else { ui.BindTF = Convert.ToByte(rd["BindTF"]); }
                if (rd["MobileCode"] == DBNull.Value) { ui.MobileCode = ""; } else { ui.MobileCode = Convert.ToString(rd["MobileCode"]); }
            }
            rd.Close();
            return ui;
        }

        /// <summary>
        /// 取得会员附加信息
        /// </summary>
        /// <param name="UserNum">用户编号</param>
        /// <returns></returns>
        public Foosun.Model.UserFields UserFields(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select * From " + Pre + "sys_userfields Where UserNum=@UserNum";
            Foosun.Model.UserFields ui = new Foosun.Model.UserFields();

            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            while (rd.Read())
            {
                ui.ID = Convert.ToInt32(rd["ID"]);
                ui.userNum = Convert.ToString(rd["userNum"]);
                if (rd["province"] == DBNull.Value) { ui.province = ""; } else { ui.province = Convert.ToString(rd["province"]); }
                if (rd["City"] == DBNull.Value) { ui.City = ""; } else { ui.City = Convert.ToString(rd["City"]); }
                if (rd["Address"] == DBNull.Value) { ui.Address = ""; } else { ui.Address = Convert.ToString(rd["Address"]); }
                if (rd["Postcode"] == DBNull.Value) { ui.Postcode = ""; } else { ui.Postcode = Convert.ToString(rd["Postcode"]); }
                if (rd["FaTel"] == DBNull.Value) { ui.FaTel = ""; } else { ui.FaTel = Convert.ToString(rd["FaTel"]); }
                if (rd["WorkTel"] == DBNull.Value) { ui.WorkTel = ""; } else { ui.WorkTel = Convert.ToString(rd["WorkTel"]); }
                if (rd["QQ"] == DBNull.Value) { ui.QQ = ""; } else { ui.QQ = Convert.ToString(rd["QQ"]); }
                if (rd["MSN"] == DBNull.Value) { ui.MSN = ""; } else { ui.MSN = Convert.ToString(rd["MSN"]); }
                if (rd["Fax"] == DBNull.Value) { ui.Fax = ""; } else { ui.Fax = Convert.ToString(rd["Fax"]); }
                if (rd["character"] == DBNull.Value) { ui.character = ""; } else { ui.character = Convert.ToString(rd["character"]); }
                if (rd["UserFan"] == DBNull.Value) { ui.UserFan = ""; } else { ui.UserFan = Convert.ToString(rd["UserFan"]); }
                if (rd["Nation"] == DBNull.Value) { ui.Nation = ""; } else { ui.Nation = Convert.ToString(rd["Nation"]); }
                if (rd["nativeplace"] == DBNull.Value) { ui.nativeplace = ""; } else { ui.nativeplace = Convert.ToString(rd["nativeplace"]); }
                if (rd["Job"] == DBNull.Value) { ui.Job = ""; } else { ui.Job = Convert.ToString(rd["Job"]); }
                if (rd["education"] == DBNull.Value) { ui.education = ""; } else { ui.education = Convert.ToString(rd["education"]); }
                if (rd["Lastschool"] == DBNull.Value) { ui.Lastschool = ""; } else { ui.Lastschool = Convert.ToString(rd["Lastschool"]); }
                if (rd["orgSch"] == DBNull.Value) { ui.orgSch = ""; } else { ui.orgSch = Convert.ToString(rd["orgSch"]); }
            }
            rd.Close();
            return ui;
        }


        /// <summary>
        /// 取得会员组信息
        /// </summary>
        /// <param name="GroupNumber">会员组随机编号</param>
        /// <returns></returns>
        public Foosun.Model.UserGroup UserGroup(string GroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", GroupNumber);
            string Sql = "Select * From " + Pre + "user_Group Where GroupNumber=@GroupNumber";
            Foosun.Model.UserGroup ugi = new Foosun.Model.UserGroup();

            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            while (rd.Read())
            {
                ugi.Id = Convert.ToInt32(rd["ID"]);
                if (rd["GroupNumber"] == DBNull.Value) { ugi.GroupNumber = ""; } else { ugi.GroupNumber = Convert.ToString(rd["GroupNumber"]); }
                if (rd["GroupName"] == DBNull.Value) { ugi.GroupName = ""; } else { ugi.GroupName = Convert.ToString(rd["GroupName"]); }
                if (rd["iPoint"] == DBNull.Value) { ugi.iPoint = 0; } else { ugi.iPoint = Convert.ToInt32(rd["iPoint"]); }
                if (rd["Gpoint"] == DBNull.Value) { ugi.Gpoint = 0; } else { ugi.Gpoint = Convert.ToInt32(rd["Gpoint"]); }
                if (rd["Rtime"] == DBNull.Value) { ugi.Rtime = 0; } else { ugi.Rtime = Convert.ToInt32(rd["Rtime"]); }
                if (rd["Discount"] == DBNull.Value) { ugi.Discount = 0; } else { ugi.Discount = Convert.ToDouble(rd["Discount"]); }
                if (rd["LenCommContent"] == DBNull.Value) { ugi.LenCommContent = 0; } else { ugi.LenCommContent = Convert.ToInt32(rd["LenCommContent"]); }
                if (rd["CommCheckTF"] == DBNull.Value) { ugi.CommCheckTF = 0; } else { ugi.CommCheckTF = Convert.ToByte(rd["CommCheckTF"]); }
                if (rd["PostCommTime"] == DBNull.Value) { ugi.PostCommTime = 0; } else { ugi.PostCommTime = Convert.ToInt32(rd["PostCommTime"]); }
                if (rd["upfileType"] == DBNull.Value) { ugi.upfileType = ""; } else { ugi.upfileType = Convert.ToString(rd["upfileType"]); }
                if (rd["upfileNum"] == DBNull.Value) { ugi.upfileNum = 0; } else { ugi.upfileNum = Convert.ToInt32(rd["upfileNum"]); }
                if (rd["upfileSize"] == DBNull.Value) { ugi.upfileSize = 0; } else { ugi.upfileSize = Convert.ToInt32(rd["upfileSize"]); }
                if (rd["DayUpfilenum"] == DBNull.Value) { ugi.DayUpfilenum = 0; } else { ugi.DayUpfilenum = Convert.ToInt32(rd["DayUpfilenum"]); }
                if (rd["ContrNum"] == DBNull.Value) { ugi.ContrNum = 0; } else { ugi.ContrNum = Convert.ToInt32(rd["ContrNum"]); }
                if (rd["DicussTF"] == DBNull.Value) { ugi.DicussTF = 0; } else { ugi.DicussTF = Convert.ToByte(rd["DicussTF"]); }
                if (rd["PostTitle"] == DBNull.Value) { ugi.PostTitle = 0; } else { ugi.PostTitle = Convert.ToByte(rd["PostTitle"]); }
                if (rd["ReadUser"] == DBNull.Value) { ugi.ReadUser = 0; } else { ugi.ReadUser = Convert.ToByte(rd["ReadUser"]); }
                if (rd["MessageNum"] == DBNull.Value) { ugi.MessageNum = 0; } else { ugi.MessageNum = Convert.ToInt32(rd["MessageNum"]); }
                if (rd["MessageGroupNum"] == DBNull.Value) { ugi.MessageGroupNum = ""; } else { ugi.MessageGroupNum = Convert.ToString(rd["MessageGroupNum"]); }
                if (rd["IsCert"] == DBNull.Value) { ugi.IsCert = 0; } else { ugi.IsCert = Convert.ToByte(rd["IsCert"]); }
                if (rd["CharTF"] == DBNull.Value) { ugi.CharTF = 0; } else { ugi.CharTF = Convert.ToByte(rd["CharTF"]); }
                if (rd["CharHTML"] == DBNull.Value) { ugi.CharHTML = 0; } else { ugi.CharHTML = Convert.ToByte(rd["CharHTML"]); }
                if (rd["CharLenContent"] == DBNull.Value) { ugi.CharLenContent = 0; } else { ugi.CharLenContent = Convert.ToInt32(rd["CharLenContent"]); }
                if (rd["RegMinute"] == DBNull.Value) { ugi.RegMinute = 0; } else { ugi.RegMinute = Convert.ToInt32(rd["RegMinute"]); }
                if (rd["PostTitleHTML"] == DBNull.Value) { ugi.PostTitleHTML = 0; } else { ugi.PostTitleHTML = Convert.ToByte(rd["PostTitleHTML"]); }
                if (rd["DelSelfTitle"] == DBNull.Value) { ugi.DelSelfTitle = 0; } else { ugi.DelSelfTitle = Convert.ToByte(rd["DelSelfTitle"]); }
                if (rd["DelOTitle"] == DBNull.Value) { ugi.DelOTitle = 0; } else { ugi.DelOTitle = Convert.ToByte(rd["DelOTitle"]); }
                if (rd["EditSelfTitle"] == DBNull.Value) { ugi.EditSelfTitle = 0; } else { ugi.EditSelfTitle = Convert.ToByte(rd["EditSelfTitle"]); }
                if (rd["EditOtitle"] == DBNull.Value) { ugi.EditOtitle = 0; } else { ugi.EditOtitle = Convert.ToByte(rd["EditOtitle"]); }
                if (rd["ReadTitle"] == DBNull.Value) { ugi.ReadTitle = 0; } else { ugi.ReadTitle = Convert.ToByte(rd["ReadTitle"]); }
                if (rd["MoveSelfTitle"] == DBNull.Value) { ugi.MoveSelfTitle = 0; } else { ugi.MoveSelfTitle = Convert.ToByte(rd["MoveSelfTitle"]); }
                if (rd["MoveOTitle"] == DBNull.Value) { ugi.MoveOTitle = 0; } else { ugi.MoveOTitle = Convert.ToByte(rd["MoveOTitle"]); }
                if (rd["TopTitle"] == DBNull.Value) { ugi.TopTitle = 0; } else { ugi.TopTitle = Convert.ToByte(rd["TopTitle"]); }
                if (rd["GoodTitle"] == DBNull.Value) { ugi.GoodTitle = 0; } else { ugi.GoodTitle = Convert.ToByte(rd["GoodTitle"]); }
                if (rd["LockUser"] == DBNull.Value) { ugi.LockUser = 0; } else { ugi.LockUser = Convert.ToByte(rd["LockUser"]); }
                if (rd["UserFlag"] == DBNull.Value) { ugi.UserFlag = ""; } else { ugi.UserFlag = Convert.ToString(rd["UserFlag"]); }
                if (rd["CheckTtile"] == DBNull.Value) { ugi.CheckTtile = 0; } else { ugi.CheckTtile = Convert.ToByte(rd["CheckTtile"]); }
                if (rd["IPTF"] == DBNull.Value) { ugi.IPTF = 0; } else { ugi.IPTF = Convert.ToByte(rd["IPTF"]); }
                if (rd["EncUser"] == DBNull.Value) { ugi.EncUser = 0; } else { ugi.EncUser = Convert.ToByte(rd["EncUser"]); }
                if (rd["OCTF"] == DBNull.Value) { ugi.OCTF = 0; } else { ugi.OCTF = Convert.ToByte(rd["OCTF"]); }
                if (rd["StyleTF"] == DBNull.Value) { ugi.StyleTF = 0; } else { ugi.StyleTF = Convert.ToByte(rd["StyleTF"]); }
                if (rd["UpfaceSize"] == DBNull.Value) { ugi.UpfaceSize = 0; } else { ugi.UpfaceSize = Convert.ToInt32(rd["UpfaceSize"]); }
                if (rd["GIChange"] == DBNull.Value) { ugi.GIChange = ""; } else { ugi.GIChange = Convert.ToString(rd["GIChange"]); }
                if (rd["GTChageRate"] == DBNull.Value) { ugi.GTChageRate = ""; } else { ugi.GTChageRate = Convert.ToString(rd["GTChageRate"]); }
                if (rd["LoginPoint"] == DBNull.Value) { ugi.LoginPoint = ""; } else { ugi.LoginPoint = Convert.ToString(rd["LoginPoint"]); }
                if (rd["RegPoint"] == DBNull.Value) { ugi.RegPoint = ""; } else { ugi.RegPoint = Convert.ToString(rd["RegPoint"]); }
                if (rd["GroupTF"] == DBNull.Value) { ugi.GroupTF = 0; } else { ugi.GroupTF = Convert.ToByte(rd["GroupTF"]); }
                if (rd["GroupSize"] == DBNull.Value) { ugi.GroupSize = 0; } else { ugi.GroupSize = Convert.ToInt32(rd["GroupSize"]); }
                if (rd["GroupPerNum"] == DBNull.Value) { ugi.GroupPerNum = 0; } else { ugi.GroupPerNum = Convert.ToInt32(rd["GroupPerNum"]); }
                if (rd["GroupCreatNum"] == DBNull.Value) { ugi.GroupCreatNum = 0; } else { ugi.GroupCreatNum = Convert.ToInt32(rd["GroupCreatNum"]); }
                if (rd["CreatTime"] == DBNull.Value) { ugi.CreatTime = DateTime.Now; } else { ugi.CreatTime = Convert.ToDateTime(rd["CreatTime"]); }
                if (rd["SiteID"] == DBNull.Value) { ugi.SiteID = ""; } else { ugi.SiteID = Convert.ToString(rd["SiteID"]); }
            }
            rd.Close();
            return ugi;
        }


        /// <summary>
        /// 取得站点会员参数
        /// </summary>
        /// <param name="SiteID">站点编号</param>
        /// <returns></returns>
        public Foosun.Model.UserParam UserParam(string SiteID)
        {
            OleDbParameter param = new OleDbParameter("@SiteID", SiteID);
            string Sql = "Select * From " + Pre + "sys_PramUser Where SiteID='0'";
            Foosun.Model.UserParam upi = new Foosun.Model.UserParam();

            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            while (rd.Read())
            {
                upi.ID = Convert.ToInt32(rd["ID"]);
                if (rd["RegGroupNumber"] == DBNull.Value) { upi.RegGroupNumber = ""; } else { upi.RegGroupNumber = Convert.ToString(rd["RegGroupNumber"]); }
                if (rd["ConstrTF"] == DBNull.Value) { upi.ConstrTF = 0; } else { upi.ConstrTF = Convert.ToByte(rd["ConstrTF"]); }
                if (rd["RegTF"] == DBNull.Value) { upi.RegTF = 0; } else { upi.RegTF = Convert.ToByte(rd["RegTF"]); }
                if (rd["UserLoginCodeTF"] == DBNull.Value) { upi.UserLoginCodeTF = 0; } else { upi.UserLoginCodeTF = Convert.ToByte(rd["UserLoginCodeTF"]); }
                if (rd["CommCodeTF"] == DBNull.Value) { upi.CommCodeTF = 0; } else { upi.CommCodeTF = Convert.ToByte(rd["CommCodeTF"]); }
                if (rd["CommCheck"] == DBNull.Value) { upi.CommCheck = 0; } else { upi.CommCheck = Convert.ToByte(rd["CommCheck"]); }
                if (rd["SendMessageTF"] == DBNull.Value) { upi.SendMessageTF = 0; } else { upi.SendMessageTF = Convert.ToByte(rd["SendMessageTF"]); }
                if (rd["UnRegCommTF"] == DBNull.Value) { upi.UnRegCommTF = 0; } else { upi.UnRegCommTF = Convert.ToByte(rd["UnRegCommTF"]); }
                if (rd["CommHTMLLoad"] == DBNull.Value) { upi.CommHTMLLoad = 0; } else { upi.CommHTMLLoad = Convert.ToByte(rd["CommHTMLLoad"]); }
                if (rd["Commfiltrchar"] == DBNull.Value) { upi.Commfiltrchar = ""; } else { upi.Commfiltrchar = Convert.ToString(rd["Commfiltrchar"]); }
                if (rd["IPLimt"] == DBNull.Value) { upi.IPLimt = ""; } else { upi.IPLimt = Convert.ToString(rd["IPLimt"]); }
                if (rd["GpointName"] == DBNull.Value) { upi.GpointName = ""; } else { upi.GpointName = Convert.ToString(rd["GpointName"]); }
                if (rd["LoginLock"] == DBNull.Value) { upi.LoginLock = ""; } else { upi.LoginLock = Convert.ToString(rd["LoginLock"]); }
                if (rd["LevelID"] == DBNull.Value) { upi.LevelID = ""; } else { upi.LevelID = Convert.ToString(rd["LevelID"]); }
                if (rd["RegContent"] == DBNull.Value) { upi.RegContent = ""; } else { upi.RegContent = Convert.ToString(rd["RegContent"]); }
                if (rd["setPoint"] == DBNull.Value) { upi.setPoint = ""; } else { upi.setPoint = Convert.ToString(rd["setPoint"]); }
                if (rd["regItem"] == DBNull.Value) { upi.regItem = ""; } else { upi.regItem = Convert.ToString(rd["regItem"]); }
                if (rd["returnemail"] == DBNull.Value) { upi.returnemail = 0; } else { upi.returnemail = Convert.ToByte(rd["returnemail"]); }
                if (rd["returnmobile"] == DBNull.Value) { upi.returnmobile = 0; } else { upi.returnmobile = Convert.ToByte(rd["returnmobile"]); }
                if (rd["onpayType"] == DBNull.Value) { upi.onpayType = 0; } else { upi.onpayType = Convert.ToByte(rd["onpayType"]); }
                if (rd["o_userName"] == DBNull.Value) { upi.o_userName = ""; } else { upi.o_userName = Convert.ToString(rd["o_userName"]); }
                if (rd["o_key"] == DBNull.Value) { upi.o_key = ""; } else { upi.o_key = Convert.ToString(rd["o_key"]); }
                if (rd["o_sendurl"] == DBNull.Value) { upi.o_sendurl = ""; } else { upi.o_sendurl = Convert.ToString(rd["o_sendurl"]); }
                if (rd["o_returnurl"] == DBNull.Value) { upi.o_returnurl = ""; } else { upi.o_returnurl = Convert.ToString(rd["o_returnurl"]); }
                if (rd["o_md5"] == DBNull.Value) { upi.o_md5 = ""; } else { upi.o_md5 = Convert.ToString(rd["o_md5"]); }
                if (rd["o_other1"] == DBNull.Value) { upi.o_other1 = ""; } else { upi.o_other1 = Convert.ToString(rd["o_other1"]); }
                if (rd["o_other2"] == DBNull.Value) { upi.o_other2 = ""; } else { upi.o_other2 = Convert.ToString(rd["o_other2"]); }
                if (rd["o_other3"] == DBNull.Value) { upi.o_other3 = ""; } else { upi.o_other3 = Convert.ToString(rd["o_other3"]); }
                if (rd["GhClass"] == DBNull.Value) { upi.GhClass = 0; } else { upi.GhClass = Convert.ToByte(rd["GhClass"]); }
                if (rd["cPointParam"] == DBNull.Value) { upi.cPointParam = ""; } else { upi.cPointParam = Convert.ToString(rd["cPointParam"]); }
                if (rd["aPointparam"] == DBNull.Value) { upi.aPointparam = ""; } else { upi.aPointparam = Convert.ToString(rd["aPointparam"]); }
                if (rd["SiteID"] == DBNull.Value) { upi.SiteID = ""; } else { upi.SiteID = Convert.ToString(rd["SiteID"]); }
            }
            rd.Close();
            return upi;
        }

    }
}
