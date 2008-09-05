#if NET1
#else
using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Discuz.Data;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;
using MySql.Data.MySqlClient;

namespace Discuz.Data.MySql
{
    public partial class DataProvider : IDataProvider
    {

        private static int _lastRemoveTimeout;

        public DataTable GetUsers(string idlist)
        {
            if (!Utils.IsNumericArray(idlist.Split(',')))
            {
                return new DataTable();
            }

            string sql = string.Format("SELECT `uid`,`username` FROM `{0}users` WHERE `groupid` IN ({1})", BaseConfigs.GetTablePrefix, idlist);
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];

        }

        public DataTable GetUserGroupInfoByGroupid(int groupid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int32, 4,groupid)
			};
            string sql = "SELECT * From  `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`=?groupid LIMIT 1";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public DataTable GetAdmingroupByAdmingid(int admingid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?admingid",(DbType)MySqlDbType.Int32, 4,admingid)
			};
            string sql = "SELECT * From  `" + BaseConfigs.GetTablePrefix + "admingroups` WHERE `admingid`=?admingid LIMIT 1";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public DataTable GetMedal()
        {
            string sql = "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "medals`";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public string GetMedalSql()
        {
            return "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "medals`";
        }

        public DataTable GetExistMedalList()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `medalid`,`image` FROM `" + BaseConfigs.GetTablePrefix + "medals` WHERE `image`<>''").Tables[0];
        }

        public void AddMedal(int medalid, string name, int available, string image)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?medalid", (DbType)MySqlDbType.Int16,2, medalid),
				DbHelper.MakeInParam("?name", (DbType)MySqlDbType.VarChar,50, name),
                DbHelper.MakeInParam("?available", (DbType)MySqlDbType.Int32, 4, available),
				DbHelper.MakeInParam("?image",(DbType)MySqlDbType.VarChar,30,image)
			};
            string sql = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "medals` (`medalid`,`name`,`available`,`image`) Values (?medalid,?name,?available,?image)";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void UpdateMedal(int medalid, string name, string image)
        {
            DbParameter[] prams =
			{

				DbHelper.MakeInParam("?name", (DbType)MySqlDbType.VarChar,50, name),
				DbHelper.MakeInParam("?image",(DbType)MySqlDbType.VarChar,30,image),
                DbHelper.MakeInParam("?medalid", (DbType)MySqlDbType.Int16,2, medalid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "medals` SET `name`=?name,`image`=?image  Where `medalid`=?medalid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void SetAvailableForMedal(int available, string medailidlist)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?available", (DbType)MySqlDbType.Int32, 4, available)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "medals` SET `available`=?available WHERE `medalid` IN(" + medailidlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void DeleteMedalById(string medailidlist)
        {
            string sql = "DELETE FROM `" + BaseConfigs.GetTablePrefix + "medals` WHERE `medalid` IN(" + medailidlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public int GetMaxMedalId()
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT IFNULL(MAX(`medalid`),0) FROM `" + BaseConfigs.GetTablePrefix + "medals`"), 0) + 1;
        }

        public string GetGroupInfo()
        {
            string sql = "SELECT `groupid`, `grouptitle` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` ORDER BY `groupid`";
            return sql;
        }

        /// <summary>
        /// 获得到指定管理组信息
        /// </summary>
        /// <returns>管理组信息</returns>
        public DataTable GetAdminGroupList()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "admingroups`").Tables[0];
        }

        /// <summary>
        /// 设置管理组信息
        /// </summary>
        /// <param name="__admingroupsInfo">管理组信息</param>
        /// <returns>更改记录数</returns>
        public int SetAdminGroupInfo(AdminGroupInfo __admingroupsInfo)
        {
            DbParameter[] prams = {

									   DbHelper.MakeInParam("?alloweditpost",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Alloweditpost),
									   DbHelper.MakeInParam("?alloweditpoll",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Alloweditpoll),
									   DbHelper.MakeInParam("?allowstickthread",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowstickthread),
									   DbHelper.MakeInParam("?allowmodpost",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowmodpost),
									   DbHelper.MakeInParam("?allowdelpost",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowdelpost),
									   DbHelper.MakeInParam("?allowmassprune",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowmassprune),
									   DbHelper.MakeInParam("?allowrefund",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowrefund),
									   DbHelper.MakeInParam("?allowcensorword",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowcensorword),
									   DbHelper.MakeInParam("?allowviewip",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowviewip),
									   DbHelper.MakeInParam("?allowbanip",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowbanip),
									   DbHelper.MakeInParam("?allowedituser",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowedituser),
									   DbHelper.MakeInParam("?allowmoduser",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowmoduser),
									   DbHelper.MakeInParam("?allowbanuser",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowbanuser),
									   DbHelper.MakeInParam("?allowpostannounce",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowpostannounce),
									   DbHelper.MakeInParam("?allowviewlog",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowviewlog),
									   DbHelper.MakeInParam("?disablepostctrl",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Disablepostctrl),
                                       DbHelper.MakeInParam("?allowviewrealname",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowviewrealname),
                                       DbHelper.MakeInParam("?admingid",(DbType)MySqlDbType.Int16,2,__admingroupsInfo.Admingid)
								   };


            string strSQL = "UPDATE `" + BaseConfigs.GetTablePrefix + "admingroups`" +
                                     " SET " +
                                        "`alloweditpost`=?alloweditpost," +
                                        "`alloweditpoll`=?alloweditpoll," +
                                        "`allowstickthread`=?allowstickthread," +
                                        "`allowmodpost`=?allowmodpost," +
                                        "`allowdelpost`=?allowdelpost," +
                                        "`allowmassprune`=?allowmassprune," +
                                        "`allowrefund`=?allowrefund," +
                                        "`allowcensorword`=?allowcensorword," +
                                        "`allowviewip`=?allowviewip," +
                                        "`allowbanip`=?allowbanip," +
                                        "`allowedituser`=?allowedituser," +
                                        "`allowmoduser`=?allowmoduser," +
                                        "`allowbanuser`=?allowbanuser," +
                                        "`allowpostannounce`=?allowpostannounce," +
                                        "`allowviewlog`=?allowviewlog," +
                                        "`disablepostctrl`=?disablepostctrl, " +
                                         "`allowviewrealname`=?allowviewrealname" +

                                       " WHERE `admingid`=?admingid";


            return DbHelper.ExecuteNonQuery(CommandType.Text, strSQL, prams);
        }

        /// <summary>
        /// 创建一个新的管理组信息
        /// </summary>
        /// <param name="__admingroupsInfo">要添加的管理组信息</param>
        /// <returns>更改记录数</returns>
        public int CreateAdminGroupInfo(AdminGroupInfo __admingroupsInfo)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?admingid",(DbType)MySqlDbType.Int16,2,__admingroupsInfo.Admingid),
									   DbHelper.MakeInParam("?alloweditpost",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Alloweditpost),
									   DbHelper.MakeInParam("?alloweditpoll",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Alloweditpoll),
									   DbHelper.MakeInParam("?allowstickthread",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowstickthread),
									   DbHelper.MakeInParam("?allowmodpost",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowmodpost),
									   DbHelper.MakeInParam("?allowdelpost",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowdelpost),
									   DbHelper.MakeInParam("?allowmassprune",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowmassprune),
									   DbHelper.MakeInParam("?allowrefund",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowrefund),
									   DbHelper.MakeInParam("?allowcensorword",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowcensorword),
									   DbHelper.MakeInParam("?allowviewip",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowviewip),
									   DbHelper.MakeInParam("?allowbanip",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowbanip),
									   DbHelper.MakeInParam("?allowedituser",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowedituser),
									   DbHelper.MakeInParam("?allowmoduser",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowmoduser),
									   DbHelper.MakeInParam("?allowbanuser",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowbanuser),
									   DbHelper.MakeInParam("?allowpostannounce",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowpostannounce),
									   DbHelper.MakeInParam("?allowviewlog",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Allowviewlog),
									   DbHelper.MakeInParam("?disablepostctrl",(DbType)MySqlDbType.Int32,4,__admingroupsInfo.Disablepostctrl)
								   };
            string strSQL = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "admingroups`" +
                                            "(`admingid`," +
                                            "`alloweditpost`," +
                                            "`alloweditpoll`," +
                                            "`allowstickthread`," +
                                            "`allowmodpost`," +
                                            "`allowdelpost`," +
                                            "`allowmassprune`," +
                                            "`allowrefund`," +
                                            "`allowcensorword`," +
                                            "`allowviewip`," +
                                            "`allowbanip`," +
                                            "`allowedituser`," +
                                            "`allowmoduser`," +
                                            "`allowbanuser`," +
                                            "`allowpostannounce`," +
                                            "`allowviewlog`," +
                                            "`disablepostctrl`) " +
                                          "VALUES " +
                                            "(?admingid," +
                                            "?alloweditpost," +
                                            "?alloweditpoll," +
                                            "?allowstickthread," +
                                            "?allowmodpost," +
                                            "?allowdelpost," +
                                            "?allowmassprune," +
                                            "?allowrefund," +
                                            "?allowcensorword," +
                                            "?allowviewip," +
                                            "?allowbanip," +
                                            "?allowedituser," +
                                            "?allowmoduser," +
                                            "?allowbanuser," +
                                            "?allowpostannounce," +
                                            "?allowviewlog," +
                                            "?disablepostctrl)";



            return DbHelper.ExecuteNonQuery(CommandType.Text, strSQL, prams);
        }

        /// <summary>
        /// 删除指定的管理组信息
        /// </summary>
        /// <param name="admingid">管理组ID</param>
        /// <returns>更改记录数</returns>
        public int DeleteAdminGroupInfo(short admingid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?admingid",(DbType)MySqlDbType.Int16,2,admingid),
								   };
            return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "admingroups` WHERE `admingid` = ?admingid", prams);
        }

        public string GetAdminGroupInfoSql()
        {
            return "Select * From `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `radminid`>0 AND `radminid`<=3  Order By `groupid`";
        }

        public DataTable GetRaterangeByGroupid(int groupid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int32, 4,groupid)
			};
            string sql = "SELECT `raterange` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`=?groupid LIMIT 1";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public void UpdateRaterangeByGroupid(string raterange, int groupid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("?raterange",(DbType)MySqlDbType.VarChar, 500,raterange),
				DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int32, 4,groupid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups` SET `raterange`=?raterange  WHERE `groupid`=?groupid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public string GetAudituserSql()
        {
            return "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "users` Where `groupid`=8";
        }

        public DataSet GetAudituserUid()
        {
            string sql = "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `groupid`=8";
            return DbHelper.ExecuteDataset(CommandType.Text, sql);
        }

        public void ClearAuthstrByUidlist(string uidlist)
        {
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "userfields` SET `authstr`=''  WHERE `uid` IN (" + uidlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void ClearAllUserAuthstr()
        {
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "userfields` SET `authstr`=''  WHERE `uid` IN (SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `groupid`=8 )";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void DeleteUserByUidlist(string uidlist)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "userfields` WHERE `uid` IN(" + uidlist + ")");
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid` IN(" + uidlist + ")");
        }

        public void DeleteAuditUser()
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "userfields` WHERE `uid` IN (SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `groupid`=8 )");
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `groupid`=8 ");
        }

        public DataTable GetAuditUserEmail()
        {
            string sql = "SELECT `username`,`password`,`email` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `groupid`=8";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }
        public DataTable GetUserEmailByUidlist(string uidlist)
        {
            string sql = "SELECT `username`,`password`,`email` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid` IN(" + uidlist + ")";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public string GetUserGroup()
        {
            string sql = "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `radminid`= 0 And `groupid`>8 ORDER BY `groupid`";
            return sql;
        }

        public string GetUserGroupTitle()
        {
            return "SELECT `groupid`,`grouptitle` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `radminid`= 0 And `groupid`>8 ORDER BY `groupid`";
        }

        public DataTable GetUserGroupWithOutGuestTitle()
        {
            return DbHelper.ExecuteDataset("SELECT `groupid`,`grouptitle` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`<>7  ORDER BY `groupid` ASC").Tables[0];
        }

        public string GetAdminUserGroupTitle()
        {
            string sql = "SELECT `groupid`,`grouptitle` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `radminid`> 0 AND `radminid`<=3  ORDER BY `groupid`";
            return sql;
        }

        public void CombinationUsergroupScore(int sourceusergroupid, int targetusergroupid)
        {
            DbParameter[] prams = 
			{
                DbHelper.MakeInParam("?sourceusergroupid",(DbType)MySqlDbType.Int32, 4,sourceusergroupid),
				DbHelper.MakeInParam("?targetusergroupid",(DbType)MySqlDbType.Int32, 4,targetusergroupid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups` SET `creditshigher`=(SELECT `creditshigher` FROM "
                + "`" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`=?sourceusergroupid) WHERE `groupid`=?targetusergroupid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void DeleteUserGroupInfo(int groupid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int32, 4,groupid)
			};
            string sql = "DELETE FROM `" + BaseConfigs.GetTablePrefix + "usergroups` Where `groupid`=?groupid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void DeleteAdminGroupInfo(int admingid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?admingid",(DbType)MySqlDbType.Int32, 4,admingid)
			};
            string sql = "DELETE FROM `" + BaseConfigs.GetTablePrefix + "admingroups` Where `admingid`=?admingid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void ChangeUsergroup(int soureceusergroupid, int targetusergroupid)
        {
            DbParameter[] prams =
			{

                DbHelper.MakeInParam("?targetusergroupid",(DbType)MySqlDbType.Int32, 4,targetusergroupid),
                DbHelper.MakeInParam("?soureceusergroupid",(DbType)MySqlDbType.Int32, 4,soureceusergroupid)
			};
            string sql = "Update `" + BaseConfigs.GetTablePrefix + "users` SET `groupid`=?targetusergroupid Where `groupid`=?soureceusergroupid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }
        public DataTable GetAdmingid(int admingid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?admingid",(DbType)MySqlDbType.Int32, 4,admingid)
			};
            string sql = "SELECT `admingid`  FROM `" + BaseConfigs.GetTablePrefix + "admingroups` WHERE `admingid`=?admingid";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public void ChangeUserAdminidByGroupid(int adminid, int groupid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?adminid",(DbType)MySqlDbType.Int32, 4,adminid),
                DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int32, 4,groupid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `adminid`=?adminid WHERE `groupid`=?groupid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public DataTable GetAvailableMedal()
        {
            string sql = "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "medals` WHERE `available`=1";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public bool IsExistMedalAwardRecord(int medalid, int userid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?medalid", (DbType)MySqlDbType.Int32,4, medalid),
				DbHelper.MakeInParam("?userid",(DbType)MySqlDbType.Int32,4,userid)
			};
            string sql = "SELECT ID FROM `" + BaseConfigs.GetTablePrefix + "medalslog` WHERE `medals`=?medalid AND `uid`=?userid LIMIT 1";
            if (DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count != 0)
                return true;
            else
                return false;
        }

        public void AddMedalslog(int adminid, string adminname, string ip, string username, int uid, string actions, int medals, string reason)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?adminid", (DbType)MySqlDbType.Int32,4, adminid),
				DbHelper.MakeInParam("?adminname",(DbType)MySqlDbType.VarChar,50,adminname),
                DbHelper.MakeInParam("?ip", (DbType)MySqlDbType.VarChar,15, ip),
				DbHelper.MakeInParam("?username",(DbType)MySqlDbType.VarChar,50,username),
                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid),
				DbHelper.MakeInParam("?actions",(DbType)MySqlDbType.VarChar,100,actions),
                DbHelper.MakeInParam("?medals", (DbType)MySqlDbType.Int32,4, medals),
				DbHelper.MakeInParam("?reason",(DbType)MySqlDbType.VarChar,100,reason)
			};
            string sql = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "medalslog` (`adminid`,`adminname`,`ip`,`username`,`uid`,`actions`,`medals`,`reason`) VALUES (?adminid,?adminname,?ip,?username,?uid,?actions,?medals,?reason)";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void UpdateMedalslog(string newactions, DateTime postdatetime, string reason, string oldactions, int medals, int uid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?newactions",(DbType)MySqlDbType.VarChar,100,newactions),
                DbHelper.MakeInParam("?postdatetime",(DbType)MySqlDbType.Date,8,postdatetime),
				DbHelper.MakeInParam("?reason",(DbType)MySqlDbType.VarChar,100,reason),
                DbHelper.MakeInParam("?oldactions",(DbType)MySqlDbType.String,100,oldactions),
                DbHelper.MakeInParam("?medals", (DbType)MySqlDbType.Int32,4, medals),
                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid)
			};
            string sql = "Update `" + BaseConfigs.GetTablePrefix + "medalslog` SET `actions`=?newactions ,`postdatetime`=?postdatetime, reason=?reason  WHERE `actions`=?oldactions AND `medals`=?medals  AND `uid`=?uid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void UpdateMedalslog(string actions, DateTime postdatetime, string reason, int uid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?actions",(DbType)MySqlDbType.VarChar,100,actions),
                DbHelper.MakeInParam("?postdatetime",(DbType)MySqlDbType.Datetime,8,postdatetime),
				DbHelper.MakeInParam("?reason",(DbType)MySqlDbType.VarChar,100,reason),
                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid)
			};
            string sql = "Update `" + BaseConfigs.GetTablePrefix + "medalslog` SET `actions`=?actions ,`postdatetime`=?postdatetime,reason=?reason  WHERE `uid`=?uid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void UpdateMedalslog(string newactions, DateTime postdatetime, string reason, string oldactions, string medalidlist, int uid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?newactions",(DbType)MySqlDbType.VarChar,100,newactions),
                DbHelper.MakeInParam("?postdatetime",(DbType)MySqlDbType.Datetime,8,postdatetime),
				DbHelper.MakeInParam("?reason",(DbType)MySqlDbType.VarChar,100,reason),
                DbHelper.MakeInParam("?oldactions",(DbType)MySqlDbType.VarChar,100,oldactions),
                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid)
			};
            string sql = "Update `" + BaseConfigs.GetTablePrefix + "medalslog` SET `actions`='" + newactions + "' ,`postdatetime`='" + postdatetime + "', reason='" + reason + "'  WHERE `actions`='" + oldactions + "' AND `medals` NOT IN (" + medalidlist + ") AND `uid`=" + uid + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void SetStopTalkUser(string uidlist)
        {
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `groupid`=4, `adminid`=0  WHERE `uid` IN (" + uidlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void ChangeUserGroupByUid(int groupid, string uidlist)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int32,4,groupid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `groupid`=?groupid  WHERE `uid` IN (" + uidlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public DataTable GetTableListInfo()
        {
            string sql = "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "tablelist`";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public void DeletePostByPosterid(int tabid, int posterid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("?posterid", (DbType)MySqlDbType.Int32,4, posterid)
			};
            string sql = "DELETE FROM  `" + BaseConfigs.GetTablePrefix + "posts" + tabid + "`   WHERE `posterid`=?posterid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void DeleteTopicByPosterid(int posterid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("?posterid", (DbType)MySqlDbType.Int32,4, posterid)
			};
            string sql = "DELETE FROM `" + BaseConfigs.GetTablePrefix + "topics` WHERE `posterid`=?posterid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void ClearPosts(int uid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `digestposts`=0 , `posts`=0  WHERE `uid`=?uid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void UpdateEmailValidateInfo(string authstr, DateTime authtime, int uid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?authstr",(DbType)MySqlDbType.VarChar,20,authstr),
                DbHelper.MakeInParam("?authtime",(DbType)MySqlDbType.Datetime,8,authtime),
                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "userfields` SET `Authstr`=?authstr,`Authtime`=?authtime ,`Authflag`=1  WHERE `uid`=?uid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public int GetRadminidByGroupid(int groupid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32,4, groupid)
			};
            string sql = "SELECT `radminid` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`=?groupid LIMIT 1";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, prams));
        }

        public string GetTemplateInfo()
        {
            string sql = "SELECT `templateid`, `name` FROM `" + BaseConfigs.GetTablePrefix + "templates`";
            return sql;
        }

        public DataTable GetUserEmailByGroupid(string groupidlist)
        {
            string sql = "SELECT `username`,`Email`  From `" + BaseConfigs.GetTablePrefix + "users` WHERE `Email` Is Not null AND `Email`<>'' AND `groupid` IN(" + groupidlist + ")";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public DataTable GetUserGroupExceptGroupid(int groupid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int32, 4,groupid)
			};
            string sql = "SELECT `groupid` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `radminid`=0 And `groupid`>8 AND `groupid`<>?groupid";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        /// <summary>
        /// 创建收藏信息
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="tid">主题ID</param>
        /// <returns>创建成功返回 1 否则返回 0</returns>	
        public int CreateFavorites(int uid, int tid)
        {
            return CreateFavorites(uid, tid, 0);
        }

        /// <summary>
        /// 创建收藏信息
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="tid">主题ID</param>
        /// <param name="type">收藏类型，0=主题，1=相册，2=博客日志</param>
        /// <returns>创建成功返回 1 否则返回 0</returns>	
        public int CreateFavorites(int uid, int tid, byte type)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
									   DbHelper.MakeInParam("?tid",(DbType)MySqlDbType.Int32,4,tid),
                                       DbHelper.MakeInParam("?type", (DbType)MySqlDbType.UInt16, 4, type)
								   };


            string sqlCreateFavorite = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "favorites` (`uid`,`tid`,`typeid`) VALUES(?uid,?tid,?type)";

            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlCreateFavorite, prams);
        }



        /// <summary>
        /// 删除指定用户的收藏信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="fitemid">要删除的收藏信息id列表,以英文逗号分割</param>
        /// <returns>删除的条数．出错时返回 -1</returns>
        public int DeleteFavorites(int uid, string fidlist, byte type)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid),
                                        DbHelper.MakeInParam("?typeid", (DbType)MySqlDbType.Int32, 1, type)
			                        };
            return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "favorites` WHERE `tid` IN (" + fidlist + ") AND `uid` = ?uid  AND `typeid`=?typeid", prams);
        }

        /// <summary>
        /// 得到用户收藏信息列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pagesize">分页时每页的记录数</param>
        /// <param name="pageindex">当前页码</param>
        /// <returns>用户信息列表</returns>
        public DataTable GetFavoritesList(int uid, int pagesize, int pageindex)
        {
            return GetFavoritesList(uid, pagesize, pageindex, 0);
        }

        /// <summary>
        /// 得到用户收藏信息列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pagesize">分页时每页的记录数</param>
        /// <param name="pageindex">当前页码</param>
        /// <param name="typeid">收藏类型id</param>
        /// <returns>用户信息列表</returns>
        public DataTable GetFavoritesList(int uid, int pagesize, int currentpage, int typeid)
        {
            DbParameter[] prams = {

                                       DbHelper.MakeInParam("?pagesize", (DbType)MySqlDbType.Int32,4,pagesize),
                                       DbHelper.MakeInParam("?pageindex",(DbType)MySqlDbType.Int32,4,currentpage),
								    DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid)
                                   };


            String sql = "";

            switch (typeid)
            {
                case 1:

                    sql = "SELECT `tid`, `uid`, `albumid`, `albumcateid`, `posterid`, `poster`, `title`, `description`, `logo`, `password`, `imgcount`, `views`, `type`, `postdatetime`  FROM (SELECT `f`.`tid`, `f`.`uid`, `albumid`, `albumcateid`, `userid` AS `posterid`, `username` AS `poster`, `title`, `description`, `logo`, `password`, `imgcount`, `views`, `type`, `createdatetime` AS `postdatetime` FROM `" + BaseConfigs.GetTablePrefix + "favorites` `f`,`" + BaseConfigs.GetTablePrefix + "albums` `albums` WHERE `f`.`tid`=`albums`.`albumid` AND `f`.`typeid`=1 AND `f`.`uid`=" + uid + ") f ORDER BY `tid` DESC LIMIT " + ((currentpage - 1) * pagesize).ToString() + "," + pagesize.ToString();
                    break;
                case 2:
                    string tempstring0 = "SELECT `f`.`tid`, `f`.`uid`, `postid`, `author` AS `poster`, `spaceposts`.`uid` AS `posterid`, `postdatetime`, `title`, `category`, `poststatus`, `commentstatus`, `postupdatetime`, `commentcount`, `views` FROM `" + BaseConfigs.GetTablePrefix + "favorites` `f`,`" + BaseConfigs.GetTablePrefix + "spaceposts` `spaceposts` WHERE `f`.`tid`=`spaceposts`.`postid` AND `f`.`typeid`=2 AND `f`.`uid`=" + uid.ToString() + "";
                    sql = "SELECT `tid`, `postid`, `poster`, `posterid`, `uid`, `postdatetime`, `title`, `category`, `poststatus`, `commentstatus`, `postupdatetime`, `commentcount`, `views`  FROM (" + tempstring0 + ") f ORDER BY `tid` DESC LIMIT " + ((currentpage - 1) * pagesize).ToString() + "," + pagesize.ToString();
                    break;

                default:
                    string tempstring1 = "SELECT `f`.`uid`,`f`.`tid`,`topics`.`title`,`topics`.`poster`,`topics`.`postdatetime`,`topics`.`replies`,`topics`.`views`,`topics`.`posterid` FROM `" + BaseConfigs.GetTablePrefix + "favorites` `f`,`" + BaseConfigs.GetTablePrefix + "topics` `topics` WHERE `f`.`tid`=`topics`.`tid` AND `f`.`typeid`=0 AND `f`.`uid`=" + uid + "";
                    sql = "SELECT `uid`,`tid`,`title`,`poster`,`postdatetime`,`replies`,`views`,`posterid`  FROM (" + tempstring1 + ") f ORDER BY `tid` DESC LIMIT " + ((currentpage - 1) * pagesize).ToString() + "," + pagesize.ToString(); ;
                    break;
            }

            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 得到用户收藏的总数
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>收藏总数</returns>
        public int GetFavoritesCount(int uid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
								   };



            string sqlGetFavoritescount = "SELECT COUNT(`uid`) as `c` FROM `" + BaseConfigs.GetTablePrefix + "favorites` WHERE `uid`=?uid";


            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlGetFavoritescount, prams).ToString(), 0);


        }

        public int GetFavoritesCount(int uid, int typeid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
                                       DbHelper.MakeInParam("?typeid",(DbType)MySqlDbType.Int32,1,typeid)
								   };
            String sql = "SELECT COUNT(uid) as c FROM `" + BaseConfigs.GetTablePrefix + "favorites` WHERE `uid`=?uid AND `typeid`=?typeid";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, prams).ToString(), 0);
            //return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "getfavoritescountbytype", prams).ToString(), 0);
        }

        public int CheckFavoritesIsIN(int uid, int tid)
        {
            return CreateFavorites(uid, tid, 0);
        }

        /// <summary>
        /// 收藏夹里是否包含了指定的主题
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="tid">主题id</param>
        /// <returns></returns>
        public int CheckFavoritesIsIN(int uid, int tid, byte type)
        {
            DbParameter[] prams = {

									   DbHelper.MakeInParam("?tid",(DbType)MySqlDbType.Int32,4,tid),
                                        DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
                                        DbHelper.MakeInParam("?type", (DbType)MySqlDbType.Int32, 4, type)
			};
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(`tid`) AS `tidcount` FROM `" + BaseConfigs.GetTablePrefix + "favorites` WHERE `tid`=?tid AND `uid`=?uid AND `typeid`=?type", prams), 0);
        }


        public void UpdateUserAllInfo(UserInfo userinfo)
        {
            string sqlstring = "Update `" + BaseConfigs.GetTablePrefix + "users` Set username=?username ,nickname=?nickname,secques=?secques,gender=?gender,adminid=?adminid,groupid=?groupid,groupexpiry=?groupexpiry,extgroupids=?extgroupids, regip=?regip," +
                "joindate=?joindate , lastip=?lastip, lastvisit=?lastvisit,  lastactivity=?lastactivity, lastpost=?lastpost, lastposttitle=?lastposttitle,posts=?posts, digestposts=?digestposts,oltime=?oltime,pageviews=?pageviews,credits=?credits," +
                "avatarshowid=?avatarshowid, email=?email,bday=?bday,sigstatus=?sigstatus,tpp=?tpp,ppp=?ppp,templateid=?templateid,pmsound=?pmsound," +
                "showemail=?showemail,newsletter=?newsletter,invisible=?invisible,newpm=?newpm,accessmasks=?accessmasks,extcredits1=?extcredits1,extcredits2=?extcredits2,extcredits3=?extcredits3,extcredits4=?extcredits4,extcredits5=?extcredits5,extcredits6=?extcredits6,extcredits7=?extcredits7,extcredits8=?extcredits8   Where uid=?uid";

            DbParameter[] prams = {
				DbHelper.MakeInParam("?username", (DbType)MySqlDbType.VarChar, 20, userinfo.Username),
				DbHelper.MakeInParam("?nickname", (DbType)MySqlDbType.VarChar, 10, userinfo.Nickname),
				DbHelper.MakeInParam("?secques", (DbType)MySqlDbType.VarChar, 8, userinfo.Secques),
				DbHelper.MakeInParam("?gender", (DbType)MySqlDbType.Int32, 4, userinfo.Gender),
				DbHelper.MakeInParam("?adminid", (DbType)MySqlDbType.Int32, 4, userinfo.Uid == 1 ? 1 : userinfo.Adminid),
				DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int16, 2, userinfo.Groupid),
				DbHelper.MakeInParam("?groupexpiry", (DbType)MySqlDbType.Int32, 4, userinfo.Groupexpiry),
				DbHelper.MakeInParam("?extgroupids", (DbType)MySqlDbType.VarChar, 60, userinfo.Extgroupids),
				DbHelper.MakeInParam("?regip", (DbType)MySqlDbType.VarChar, 15, userinfo.Regip),
				DbHelper.MakeInParam("?joindate", (DbType)MySqlDbType.Datetime, 4, userinfo.Joindate),
				DbHelper.MakeInParam("?lastip", (DbType)MySqlDbType.VarChar, 15, userinfo.Lastip),
				DbHelper.MakeInParam("?lastvisit", (DbType)MySqlDbType.Datetime, 8, userinfo.Lastvisit),
				DbHelper.MakeInParam("?lastactivity", (DbType)MySqlDbType.Datetime, 8, userinfo.Lastactivity),
				DbHelper.MakeInParam("?lastpost", (DbType)MySqlDbType.Datetime, 8, userinfo.Lastpost),
				DbHelper.MakeInParam("?lastposttitle", (DbType)MySqlDbType.VarChar, 80, userinfo.Lastposttitle),
				DbHelper.MakeInParam("?posts", (DbType)MySqlDbType.Int32, 4, userinfo.Posts),
				DbHelper.MakeInParam("?digestposts", (DbType)MySqlDbType.Int16, 2, userinfo.Digestposts),
				DbHelper.MakeInParam("?oltime", (DbType)MySqlDbType.Int32, 4, userinfo.Oltime),
				DbHelper.MakeInParam("?pageviews", (DbType)MySqlDbType.Int32, 4, userinfo.Pageviews),
				DbHelper.MakeInParam("?credits", (DbType)MySqlDbType.Decimal, 10, userinfo.Credits),
				DbHelper.MakeInParam("?avatarshowid", (DbType)MySqlDbType.Int32, 4, userinfo.Avatarshowid),
				DbHelper.MakeInParam("?email", (DbType)MySqlDbType.VarChar, 50, userinfo.Email.ToString()),
				DbHelper.MakeInParam("?bday", (DbType)MySqlDbType.VarChar, 10, userinfo.Bday.ToString()),
				DbHelper.MakeInParam("?sigstatus", (DbType)MySqlDbType.Int32, 4, userinfo.Sigstatus.ToString()),
				DbHelper.MakeInParam("?tpp", (DbType)MySqlDbType.Int32, 4, userinfo.Tpp),
				DbHelper.MakeInParam("?ppp", (DbType)MySqlDbType.Int32, 4, userinfo.Ppp),
				DbHelper.MakeInParam("?templateid", (DbType)MySqlDbType.Int32, 4, userinfo.Templateid),
				DbHelper.MakeInParam("?pmsound", (DbType)MySqlDbType.Int32, 4, userinfo.Pmsound),
				DbHelper.MakeInParam("?showemail", (DbType)MySqlDbType.Int32, 4, userinfo.Showemail),
				DbHelper.MakeInParam("?newsletter", (DbType)MySqlDbType.Int32, 4, userinfo.Newsletter),
				DbHelper.MakeInParam("?invisible", (DbType)MySqlDbType.Int32, 4, userinfo.Invisible),
				DbHelper.MakeInParam("?newpm", (DbType)MySqlDbType.Int32, 4, userinfo.Newpm),
				DbHelper.MakeInParam("?accessmasks", (DbType)MySqlDbType.Int32, 4, userinfo.Accessmasks),
				DbHelper.MakeInParam("?extcredits1", (DbType)MySqlDbType.Decimal, 10, userinfo.Extcredits1),
				DbHelper.MakeInParam("?extcredits2", (DbType)MySqlDbType.Decimal, 10, userinfo.Extcredits2),
				DbHelper.MakeInParam("?extcredits3", (DbType)MySqlDbType.Decimal, 10, userinfo.Extcredits3),
				DbHelper.MakeInParam("?extcredits4", (DbType)MySqlDbType.Decimal, 10, userinfo.Extcredits4),
				DbHelper.MakeInParam("?extcredits5", (DbType)MySqlDbType.Decimal, 10, userinfo.Extcredits5),
				DbHelper.MakeInParam("?extcredits6", (DbType)MySqlDbType.Decimal, 10, userinfo.Extcredits6),
				DbHelper.MakeInParam("?extcredits7", (DbType)MySqlDbType.Decimal, 10, userinfo.Extcredits7),
				DbHelper.MakeInParam("?extcredits8", (DbType)MySqlDbType.Decimal, 10, userinfo.Extcredits8),
				DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, userinfo.Uid)
			};

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, prams);
        }

        public void DeleteModerator(int uid)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "moderators` WHERE `uid`=" + uid);
        }

        public void UpdateUserField(UserInfo __userinfo, string signature, string authstr, string sightml)
        {

            DbParameter[] prams1 = {
				DbHelper.MakeInParam("?website", (DbType)MySqlDbType.VarChar, 80, __userinfo.Website),
				DbHelper.MakeInParam("?icq", (DbType)MySqlDbType.VarChar, 12, __userinfo.Icq),
				DbHelper.MakeInParam("?qq", (DbType)MySqlDbType.VarChar, 12, __userinfo.Qq),
				DbHelper.MakeInParam("?yahoo", (DbType)MySqlDbType.VarChar, 40, __userinfo.Yahoo),
				DbHelper.MakeInParam("?msn", (DbType)MySqlDbType.VarChar, 40, __userinfo.Msn),
				DbHelper.MakeInParam("?skype", (DbType)MySqlDbType.VarChar, 40, __userinfo.Skype),
				DbHelper.MakeInParam("?location", (DbType)MySqlDbType.VarChar, 50, __userinfo.Location),
				DbHelper.MakeInParam("?customstatus", (DbType)MySqlDbType.VarChar, 50, __userinfo.Customstatus),
				DbHelper.MakeInParam("?avatar", (DbType)MySqlDbType.VarChar, 255, __userinfo.Avatar),
				DbHelper.MakeInParam("?avatarwidth", (DbType)MySqlDbType.Int32, 4, __userinfo.Avatarwidth),
				DbHelper.MakeInParam("?avatarheight", (DbType)MySqlDbType.Int32, 4, __userinfo.Avatarheight),
				DbHelper.MakeInParam("?medals", (DbType)MySqlDbType.VarChar, 300, __userinfo.Medals),
				DbHelper.MakeInParam("?authstr", (DbType)MySqlDbType.VarChar, 20, authstr),
				DbHelper.MakeInParam("?authtime", (DbType)MySqlDbType.Datetime, 4, __userinfo.Authtime),
				DbHelper.MakeInParam("?authflag", (DbType)MySqlDbType.Int16, 1, 1),
				DbHelper.MakeInParam("?bio", (DbType)MySqlDbType.VarChar, 500, __userinfo.Bio.ToString()),
				DbHelper.MakeInParam("?signature", (DbType)MySqlDbType.VarChar, 500, signature),
				DbHelper.MakeInParam("?sightml", (DbType)MySqlDbType.VarChar, 1000, sightml),
                DbHelper.MakeInParam("?Realname", (DbType)MySqlDbType.VarChar, 1000, __userinfo.Realname),
                DbHelper.MakeInParam("?Idcard", (DbType)MySqlDbType.VarChar, 1000, __userinfo.Idcard),
                DbHelper.MakeInParam("?Mobile", (DbType)MySqlDbType.VarChar, 1000, __userinfo.Mobile),
                DbHelper.MakeInParam("?Phone", (DbType)MySqlDbType.VarChar, 1000, __userinfo.Phone),
				DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, __userinfo.Uid)
			};
            string sqlstring = "Update `" + BaseConfigs.GetTablePrefix + "userfields` Set website=?website,icq=?icq,qq=?qq,yahoo=?yahoo,msn=?msn,skype=?skype,location=?location,customstatus=?customstatus, avatar=?avatar," +
                        "avatarwidth=?avatarwidth , avatarheight=?avatarheight, medals=?medals,  authstr=?authstr, authtime=?authtime, authflag=?authflag,bio=?bio, signature=?signature,sightml=?sightml,realname=?Realname,idcard=?Idcard,mobile=?Mobile,phone=?Phone   Where uid=?uid";

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, prams1);
        }



        public void UpdatePMSender(int msgfromid, string msgfrom)
        {
            DbParameter[] parms = {

                                        DbHelper.MakeInParam("?msgfrom", (DbType)MySqlDbType.VarChar, 20, msgfrom),
                                        DbHelper.MakeInParam("?msgfromid", (DbType)MySqlDbType.Int32, 4, msgfromid)
                                    };

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "pms` SET `msgfrom`=?msgfrom WHERE `msgfromid`=?msgfromid", parms);
        }

        public void UpdatePMReceiver(int msgtoid, string msgto)
        {
            DbParameter[] parms = {

                                        DbHelper.MakeInParam("?msgto", (DbType)MySqlDbType.VarChar, 20, msgto),
                                         DbHelper.MakeInParam("?msgtoid", (DbType)MySqlDbType.Int32, 4, msgtoid)
                                    };

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "pms` SET `msgto`=?msgto  WHERE `msgtoid`=?msgtoid", parms);
        }



        public DataRowCollection GetModerators(string oldusername)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("?oldusername", (DbType)MySqlDbType.VarChar, 20, RegEsc(oldusername))
			};

            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `fid`,`moderators` FROM  `" + BaseConfigs.GetTablePrefix + "forumfields` WHERE `moderators` LIKE '% ?oldusername %'", prams).Tables[0].Rows;
        }

        public DataTable GetModeratorsTable(string oldusername)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("?oldusername", (DbType)MySqlDbType.VarChar, 20, RegEsc(oldusername))
			};

            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `fid`,`moderators` FROM  `" + BaseConfigs.GetTablePrefix + "forumfields` WHERE `moderators` LIKE '% ?oldusername %'", prams).Tables[0];
        }

        public void UpdateModerators(int fid, string moderators)
        {
            DbParameter[] parm = {
                                        DbHelper.MakeInParam("?moderators", (DbType)MySqlDbType.VarChar, 20, moderators),
                                        DbHelper.MakeInParam("?fid", (DbType)MySqlDbType.Int32, 4, fid)
                                    };

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "forumfields` SET `moderators`=?moderators  WHERE `fid`=?fid", parm);
        }

        public void UpdateUserCredits(int userid, float credits, float extcredits1, float extcredits2, float extcredits3, float extcredits4, float extcredits5, float extcredits6, float extcredits7, float extcredits8)
        {
            DbParameter[] prams1 = {

					DbHelper.MakeInParam("?Credits",(DbType)MySqlDbType.Decimal,9, credits),
					DbHelper.MakeInParam("?Extcredits1", (DbType)MySqlDbType.Decimal, 20,extcredits1),
					DbHelper.MakeInParam("?Extcredits2", (DbType)MySqlDbType.Decimal, 20,extcredits2),
					DbHelper.MakeInParam("?Extcredits3", (DbType)MySqlDbType.Decimal, 20,extcredits3),
					DbHelper.MakeInParam("?Extcredits4", (DbType)MySqlDbType.Decimal, 20,extcredits4),
					DbHelper.MakeInParam("?Extcredits5", (DbType)MySqlDbType.Decimal, 20,extcredits5),
					DbHelper.MakeInParam("?Extcredits6", (DbType)MySqlDbType.Decimal, 20,extcredits6),
					DbHelper.MakeInParam("?Extcredits7", (DbType)MySqlDbType.Decimal, 20,extcredits7),
					DbHelper.MakeInParam("?Extcredits8", (DbType)MySqlDbType.Decimal, 20,extcredits8),
                    DbHelper.MakeInParam("?targetuid",(DbType)MySqlDbType.Int32, 4,userid.ToString())
										};

            string sqlstring = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET credits=?Credits,extcredits1=?Extcredits1, extcredits2=?Extcredits2, extcredits3=?Extcredits3, extcredits4=?Extcredits4, extcredits5=?Extcredits5, extcredits6=?Extcredits6, extcredits7=?Extcredits7, extcredits8=?Extcredits8 WHERE `uid`=?targetuid";

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, prams1);
        }

        public void UpdateUserCredits(int userid, int extcreditsid, float score)
        {
            DbParameter[] prams1 = {
					DbHelper.MakeInParam("?targetuid",(DbType)MySqlDbType.Int32,4,userid.ToString()),
					DbHelper.MakeInParam("?Extcredits", (DbType)MySqlDbType.Float, 8, score)
             };

            string sqlstring = string.Format("UPDATE `{0}users` SET extcredits{1}=extcredits{1} + ?Extcredits WHERE `uid`=?targetuid", BaseConfigs.GetTablePrefix, extcreditsid);

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, prams1);
        }

        public void CombinationUser(string posttablename, UserInfo targetuserinfo, UserInfo srcuserinfo)
        {
            DbParameter[] prams = {
					DbHelper.MakeInParam("?target_uid", (DbType)MySqlDbType.Int32, 4, targetuserinfo.Uid),
					DbHelper.MakeInParam("?target_username", (DbType)MySqlDbType.VarChar, 20, targetuserinfo.Username.Trim()),
					DbHelper.MakeInParam("?src_uid", (DbType)MySqlDbType.Int32, 4, srcuserinfo.Uid)
				};

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE  `" + BaseConfigs.GetTablePrefix + "topics` SET `posterid`=?target_uid,`poster`=?target_username  WHERE `posterid`=?src_uid", prams);

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `posts`=" + (srcuserinfo.Posts + targetuserinfo.Posts) + "WHERE `uid`=?target_uid", prams);

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE  `" + posttablename + "` SET `posterid`=?target_uid,`poster`=?target_username  WHERE `posterid`=?src_uid", prams);

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE  `" + BaseConfigs.GetTablePrefix + "pms` SET `msgtoid`=?target_uid,`msgto`=?target_username  WHERE `msgtoid`=?src_uid", prams);

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE  `" + BaseConfigs.GetTablePrefix + "attachments` SET `uid`=?target_uid WHERE `uid`=?src_uid", prams);

        }

        /// <summary>
        /// 通过用户名得到UID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetuidByusername(string username)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("?username", (DbType)MySqlDbType.VarChar, 20, username)
			};

            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `username`=?username LIMIT 1", prams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除指定用户的所有信息
        /// </summary>
        /// <param name="uid">指定的用户uid</param>
        /// <param name="delposts">是否删除帖子</param>
        /// <param name="delpms">是否删除短消息</param>
        /// <returns></returns>
        public bool DelUserAllInf(int uid, bool delposts, bool delpms)
        {
            //  SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
            //  conn.Open();
            //  using (SqlTransaction trans = conn.BeginTransaction())
            //   {
            //   try
            //   {
            DbHelper.ExecuteNonQuery(CommandType.Text, "Delete From `" + BaseConfigs.GetTablePrefix + "users` Where `uid`=" + uid.ToString());
            DbHelper.ExecuteNonQuery(CommandType.Text, "Delete From `" + BaseConfigs.GetTablePrefix + "userfields` Where `uid`=" + uid.ToString());
            DbHelper.ExecuteNonQuery(CommandType.Text, "Delete From `" + BaseConfigs.GetTablePrefix + "polls` Where `userid`=" + uid.ToString());
            DbHelper.ExecuteNonQuery(CommandType.Text, "Delete From `" + BaseConfigs.GetTablePrefix + "favorites` Where `uid`=" + uid.ToString());

            if (delposts)
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "Delete From `" + BaseConfigs.GetTablePrefix + "topics` Where `posterid`=" + uid.ToString());

                //清除用户所发的帖子
                foreach (DataRow dr in DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "tablelist`").Tables[0].Rows)
                {
                    if (dr["id"].ToString() != "")
                    {
                        DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM  `" + BaseConfigs.GetTablePrefix + "posts" + dr["id"].ToString() + "`   WHERE `posterid`=" + uid);
                    }
                }
            }
            else
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "topics` SET `poster`='该用户已被删除'  Where `posterid`=" + uid.ToString());

                DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "topics` SET `lastposter`='该用户已被删除'  Where `lastpostid`=" + uid.ToString());

                //清除用户所发的帖子
                foreach (DataRow dr in DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "tablelist`").Tables[0].Rows)
                {
                    if (dr["id"].ToString() != "")
                    {
                        DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE  `" + BaseConfigs.GetTablePrefix + "posts" + dr["id"].ToString() + "` SET  `poster`='该用户已被删除'  WHERE `posterid`=" + uid);
                    }
                }
            }

            if (delpms)
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "pms` Where `msgfromid`=" + uid.ToString());
            }
            else
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "pms` SET `msgfrom`='该用户已被删除'  Where `msgfromid`=" + uid.ToString());
                DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "pms` SET `msgto`='该用户已被删除'  Where `msgtoid`=" + uid.ToString());
            }

            //删除版主表的相关用户信息
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "moderators` WHERE `uid`=" + uid.ToString());

            //更新当前论坛总人数
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "Statistics` SET `totalusers`=`totalusers`-1");

            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, "SELECT `uid`,`username` FROM `" + BaseConfigs.GetTablePrefix + "users` ORDER BY `uid` DESC LIMIT 1").Tables[0];
            if (dt.Rows.Count > 0)
            {
                //更新当前论坛最新注册会员信息
                DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "statistics` SET `lastuserid`=" + dt.Rows[0][0] + ", `lastusername`='" + dt.Rows[0][1] + "'");
            }



            //trans.Commit();

            //   }
            // catch (Exception ex)
            //{
            //    trans.Rollback();
            //    throw ex;
            //  }
            // }
            // conn.Close();
            return true;
        }

        public DataTable GetUserGroup(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);

            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`=?groupid", parm).Tables[0];
        }

        public DataTable GetAdminGroup(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);

            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "admingroups` WHERE `admingid`=?groupid LIMIT 1", parm).Tables[0];
        }

        public void AddUserGroup(UserGroupInfo __usergroupinfo, int Creditshigher, int Creditslower)
        {
            DbParameter[] prams = 
					{
						DbHelper.MakeInParam("?Radminid",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Radminid),
						DbHelper.MakeInParam("?Grouptitle",(DbType)MySqlDbType.VarChar,50, Utils.RemoveFontTag(__usergroupinfo.Grouptitle)),
						DbHelper.MakeInParam("?Creditshigher",(DbType)MySqlDbType.Int32,4,Creditshigher),
						DbHelper.MakeInParam("?Creditslower",(DbType)MySqlDbType.Int32,4,Creditslower),
						DbHelper.MakeInParam("?Stars",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Stars),
						DbHelper.MakeInParam("?Color",(DbType)MySqlDbType.VarChar,7,__usergroupinfo.Color),
						DbHelper.MakeInParam("?Groupavatar",(DbType)MySqlDbType.VarChar,60,__usergroupinfo.Groupavatar),
						DbHelper.MakeInParam("?Readaccess",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Readaccess),
						DbHelper.MakeInParam("?Allowvisit",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowvisit),
						DbHelper.MakeInParam("?Allowpost",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowpost),
						DbHelper.MakeInParam("?Allowreply",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowreply),
						DbHelper.MakeInParam("?Allowpostpoll",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowpostpoll),
						DbHelper.MakeInParam("?Allowdirectpost",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowdirectpost),
						DbHelper.MakeInParam("?Allowgetattach",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowgetattach),
						DbHelper.MakeInParam("?Allowpostattach",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowpostattach),
						DbHelper.MakeInParam("?Allowvote",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowvote),
						DbHelper.MakeInParam("?Allowmultigroups",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowmultigroups),
						DbHelper.MakeInParam("?Allowsearch",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowsearch),
						DbHelper.MakeInParam("?Allowavatar",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowavatar),
						DbHelper.MakeInParam("?Allowcstatus",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowcstatus),
						DbHelper.MakeInParam("?Allowuseblog",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowuseblog),
						DbHelper.MakeInParam("?Allowinvisible",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowinvisible),
						DbHelper.MakeInParam("?Allowtransfer",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowtransfer),
						DbHelper.MakeInParam("?Allowsetreadperm",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowsetreadperm),
						DbHelper.MakeInParam("?Allowsetattachperm",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowsetattachperm),
						DbHelper.MakeInParam("?Allowhidecode",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowhidecode),
						DbHelper.MakeInParam("?Allowhtml",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowhtml),
						DbHelper.MakeInParam("?Allowcusbbcode",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowcusbbcode),
						DbHelper.MakeInParam("?Allownickname",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allownickname),
						DbHelper.MakeInParam("?Allowsigbbcode",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowsigbbcode),
						DbHelper.MakeInParam("?Allowsigimgcode",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowsigimgcode),
						DbHelper.MakeInParam("?Allowviewpro",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowviewpro),
						DbHelper.MakeInParam("?Allowviewstats",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Allowviewstats),
						DbHelper.MakeInParam("?Disableperiodctrl",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Disableperiodctrl),
						DbHelper.MakeInParam("?Reasonpm",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Reasonpm),
						DbHelper.MakeInParam("?Maxprice",(DbType)MySqlDbType.Int32,2,__usergroupinfo.Maxprice),
						DbHelper.MakeInParam("?Maxpmnum",(DbType)MySqlDbType.Int32,2,__usergroupinfo.Maxpmnum),
						DbHelper.MakeInParam("?Maxsigsize",(DbType)MySqlDbType.Int32,2,__usergroupinfo.Maxsigsize),
						DbHelper.MakeInParam("?Maxattachsize",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Maxattachsize),
						DbHelper.MakeInParam("?Maxsizeperday",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Maxsizeperday),
						DbHelper.MakeInParam("?Attachextensions",(DbType)MySqlDbType.VarChar,100,__usergroupinfo.Attachextensions),
                        DbHelper.MakeInParam("?Maxspaceattachsize",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Maxspaceattachsize),
                        DbHelper.MakeInParam("?Maxspacephotosize",(DbType)MySqlDbType.Int32,4,__usergroupinfo.Maxspacephotosize),
						DbHelper.MakeInParam("?Raterange",(DbType)MySqlDbType.VarChar,100,__usergroupinfo.Raterange)
					};

            string sqlstring = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "usergroups`  (`radminid`,`grouptitle`,`creditshigher`,`creditslower`," +
                "`stars` ,`color`, `groupavatar`,`readaccess`, `allowvisit`,`allowpost`,`allowreply`," +
                "`allowpostpoll`, `allowdirectpost`,`allowgetattach`,`allowpostattach`,`allowvote`,`allowmultigroups`," +
                "`allowsearch`,`allowavatar`,`allowcstatus`,`allowuseblog`,`allowinvisible`,`allowtransfer`," +
                "`allowsetreadperm`,`allowsetattachperm`,`allowhidecode`,`allowhtml`,`allowcusbbcode`,`allownickname`," +
                "`allowsigbbcode`,`allowsigimgcode`,`allowviewpro`,`allowviewstats`,`disableperiodctrl`,`reasonpm`," +
                "`maxprice`,`maxpmnum`,`maxsigsize`,`maxattachsize`,`maxsizeperday`,`attachextensions`,`raterange`,`maxspaceattachsize`,`maxspacephotosize`) VALUES(" +
                "?Radminid,?Grouptitle,?Creditshigher,?Creditslower,?Stars,?Color,?Groupavatar,?Readaccess,?Allowvisit,?Allowpost,?Allowreply," +
                "?Allowpostpoll,?Allowdirectpost,?Allowgetattach,?Allowpostattach,?Allowvote,?Allowmultigroups,?Allowsearch,?Allowavatar,?Allowcstatus," +
                "?Allowuseblog,?Allowinvisible,?Allowtransfer,?Allowsetreadperm,?Allowsetattachperm,?Allowhidecode,?Allowhtml,?Allowcusbbcode,?Allownickname," +
                "?Allowsigbbcode,?Allowsigimgcode,?Allowviewpro,?Allowviewstats,?Disableperiodctrl,?Reasonpm,?Maxprice,?Maxpmnum,?Maxsigsize,?Maxattachsize," +
                "?Maxsizeperday,?Attachextensions,?Raterange,?Maxspaceattachsize,?Maxspacephotosize)";

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, prams);
        }

        public void AddOnlineList(string grouptitle)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, GetMaxUserGroupId()),
                                        DbHelper.MakeInParam("?title", (DbType)MySqlDbType.VarChar, 50, grouptitle)
                                    };
            string sqlstring = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "onlinelist` (`groupid`, `title`, `img`) VALUES(?groupid,?title,'')";

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        public DataTable GetMinCreditHigher()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT MIN(Creditshigher) FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`>8 AND `radminid`=0 ").Tables[0];
        }

        public DataTable GetMaxCreditLower()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT MAX(Creditslower) FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`>8 AND `radminid`=0 ").Tables[0];
        }

        public DataTable GetUserGroupByCreditshigher(int Creditshigher)
        {
            DbParameter parm = DbHelper.MakeInParam("?Creditshigher", (DbType)MySqlDbType.Int32, 4, Creditshigher);
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `groupid`,`creditshigher`,`creditslower` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`>8 AND `radminid`=0  AND `Creditshigher`<=?Creditshigher AND ?Creditshigher<`Creditslower` LIMIT 1", parm).Tables[0];
        }

        public void UpdateUserGroupCreditsHigher(int currentGroupID, int Creditslower)
        {
            DbParameter[] parms = {

                                        DbHelper.MakeInParam("?creditshigher", (DbType)MySqlDbType.Int32, 4, Creditslower),
                                        DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, currentGroupID)
                                    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups` SET creditshigher=?creditshigher WHERE `groupid`=?groupid", parms);
        }

        public void UpdateUserGroupCreidtsLower(int currentCreditsHigher, int Creditshigher)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("?creditslower", (DbType)MySqlDbType.Int32, 4, Creditshigher),
                                        DbHelper.MakeInParam("?creditshigher", (DbType)MySqlDbType.Int32, 4, currentCreditsHigher)
                                    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups` SET `creditslower`=?creditslower WHERE `groupid`>8 AND `radminid`=0 AND `creditshigher`=?creditshigher", parms);
        }

        public DataTable GetUserGroupByCreditsHigherAndLower(int Creditshigher, int Creditslower)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("?Creditshigher", (DbType)MySqlDbType.Int32, 4, Creditshigher),
                                        DbHelper.MakeInParam("?Creditslower", (DbType)MySqlDbType.Int32, 4, Creditslower)
                                    };
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `groupid` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`>8 AND `radminid`=0 AND `Creditshigher`=?Creditshigher AND `Creditslower`=?Creditslower", parms).Tables[0];
        }
        public int GetGroupCountByCreditsLower(int Creditshigher)
        {
            DbParameter parm = DbHelper.MakeInParam("?creditslower", (DbType)MySqlDbType.Int32, 4, Creditshigher);
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `groupid` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`>8 AND `radminid`=0 AND `creditslower`=?creditslower", parm).Tables[0].Rows.Count;
        }

        public void UpdateUserGroupsCreditsLowerByCreditsLower(int Creditslower, int Creditshigher)
        {
            DbParameter[] parms = {

                                        DbHelper.MakeInParam("?Creditslower", (DbType)MySqlDbType.Int32, 4, Creditslower),
                                        DbHelper.MakeInParam("?Creditshigher", (DbType)MySqlDbType.Int32, 4, Creditshigher)
                                    };
            DbHelper.ExecuteDataset(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups` SET `creditslower`=?Creditslower WHERE `groupid`>8 AND `radminid`=0 AND `creditslower`=?Creditshigher", parms);
        }

        public void UpdateUserGroupTitleAndCreditsByGroupid(int groupid, string grouptitle, int creditslower, int creditshigher)
        {
            DbParameter[] parms = {
               
                DbHelper.MakeInParam("?grouptitle",(DbType)MySqlDbType.VarChar,50,grouptitle),
                DbHelper.MakeInParam("?creditshigher",(DbType)MySqlDbType.Int32,4,creditshigher),
                DbHelper.MakeInParam("?creditslower",(DbType)MySqlDbType.Int32,4,creditslower), 
                DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int32,4,groupid)
            };
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups` SET `grouptitle`=?grouptitle,`creditshigher`=?creditshigher,`creditslower`=?creditslower WHERE `groupid`=?groupid";
            DbHelper.ExecuteDataset(CommandType.Text, sql, parms);
        }


        public void UpdateUserGroupsCreditsHigherByCreditsHigher(int Creditshigher, int Creditslower)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("?Creditshigher", (DbType)MySqlDbType.Int32, 4, Creditshigher),
                                        DbHelper.MakeInParam("?Creditslower", (DbType)MySqlDbType.Int32, 4, Creditslower)
                                    };

            DbHelper.ExecuteDataset(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups` SET `Creditshigher`=?Creditshigher WHERE `groupid`>8 AND `radminid`=0 AND `Creditshigher`=?Creditslower", parms);
        }

        public DataTable GetUserGroupCreditsLowerAndHigher(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);

            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `groupid`,`creditshigher`,`creditslower` FROM `" + BaseConfigs.GetTablePrefix + "usergroups`  WHERE `groupid`=?groupid LIMIT 1", parm).Tables[0];
        }

        public void UpdateUserGroup(UserGroupInfo usergroupinfo, int Creditshigher, int Creditslower)
        {
            DbParameter[] prams = 
					{
						DbHelper.MakeInParam("?Radminid",(DbType)MySqlDbType.Int32,4,(usergroupinfo.Groupid == 1) ? 1 : usergroupinfo.Radminid),
						DbHelper.MakeInParam("?Grouptitle",(DbType)MySqlDbType.VarChar,50, Utils.RemoveFontTag(usergroupinfo.Grouptitle)),
						DbHelper.MakeInParam("?Creditshigher",(DbType)MySqlDbType.Int32,4,Creditshigher),
						DbHelper.MakeInParam("?Creditslower",(DbType)MySqlDbType.Int32,4,Creditslower),
						DbHelper.MakeInParam("?Stars",(DbType)MySqlDbType.Int32,4,usergroupinfo.Stars),
						DbHelper.MakeInParam("?Color",(DbType)MySqlDbType.VarChar,7,usergroupinfo.Color),
						DbHelper.MakeInParam("?Groupavatar",(DbType)MySqlDbType.VarChar,60,usergroupinfo.Groupavatar),
						DbHelper.MakeInParam("?Readaccess",(DbType)MySqlDbType.Int32,4,usergroupinfo.Readaccess),
						DbHelper.MakeInParam("?Allowvisit",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowvisit),
						DbHelper.MakeInParam("?Allowpost",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowpost),
						DbHelper.MakeInParam("?Allowreply",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowreply),
						DbHelper.MakeInParam("?Allowpostpoll",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowpostpoll),
						DbHelper.MakeInParam("?Allowdirectpost",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowdirectpost),
						DbHelper.MakeInParam("?Allowgetattach",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowgetattach),
						DbHelper.MakeInParam("?Allowpostattach",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowpostattach),
						DbHelper.MakeInParam("?Allowvote",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowvote),
						DbHelper.MakeInParam("?Allowmultigroups",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowmultigroups),
						DbHelper.MakeInParam("?Allowsearch",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowsearch),
						DbHelper.MakeInParam("?Allowavatar",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowavatar),
						DbHelper.MakeInParam("?Allowcstatus",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowcstatus),
						DbHelper.MakeInParam("?Allowuseblog",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowuseblog),
						DbHelper.MakeInParam("?Allowinvisible",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowinvisible),
						DbHelper.MakeInParam("?Allowtransfer",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowtransfer),
						DbHelper.MakeInParam("?Allowsetreadperm",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowsetreadperm),
						DbHelper.MakeInParam("?Allowsetattachperm",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowsetattachperm),
						DbHelper.MakeInParam("?Allowhidecode",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowhidecode),
						DbHelper.MakeInParam("?Allowhtml",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowhtml),
						DbHelper.MakeInParam("?Allowcusbbcode",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowcusbbcode),
						DbHelper.MakeInParam("?Allownickname",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allownickname),
						DbHelper.MakeInParam("?Allowsigbbcode",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowsigbbcode),
						DbHelper.MakeInParam("?Allowsigimgcode",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowsigimgcode),
						DbHelper.MakeInParam("?Allowviewpro",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowviewpro),
						DbHelper.MakeInParam("?Allowviewstats",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowviewstats),
                        DbHelper.MakeInParam("?Allowtrade",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowtrade),
                        DbHelper.MakeInParam("?Allowdiggs",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowdiggs),
						DbHelper.MakeInParam("?Disableperiodctrl",(DbType)MySqlDbType.Int32,4,usergroupinfo.Disableperiodctrl),

                        DbHelper.MakeInParam("?Allowdebate",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowdebate),
                        DbHelper.MakeInParam("?Allowbonus",(DbType)MySqlDbType.Int32,4,usergroupinfo.Allowbonus),
                        DbHelper.MakeInParam("?Minbonusprice",(DbType)MySqlDbType.Int32,4,usergroupinfo.Minbonusprice),
                        DbHelper.MakeInParam("?Maxbonusprice",(DbType)MySqlDbType.Int32,4,usergroupinfo.Maxbonusprice),

						DbHelper.MakeInParam("?Reasonpm",(DbType)MySqlDbType.Int32,4,usergroupinfo.Reasonpm),
						DbHelper.MakeInParam("?Maxprice",(DbType)MySqlDbType.Int32,2,usergroupinfo.Maxprice),
						DbHelper.MakeInParam("?Maxpmnum",(DbType)MySqlDbType.Int32,2,usergroupinfo.Maxpmnum),
						DbHelper.MakeInParam("?Maxsigsize",(DbType)MySqlDbType.Int32,2,usergroupinfo.Maxsigsize),
						DbHelper.MakeInParam("?Maxattachsize",(DbType)MySqlDbType.Int32,4,usergroupinfo.Maxattachsize),
						DbHelper.MakeInParam("?Maxsizeperday",(DbType)MySqlDbType.Int32,4,usergroupinfo.Maxsizeperday),
						DbHelper.MakeInParam("?Attachextensions",(DbType)MySqlDbType.VarChar,100,usergroupinfo.Attachextensions),
                        DbHelper.MakeInParam("?Maxspaceattachsize",(DbType)MySqlDbType.Int32,4,usergroupinfo.Maxspaceattachsize),
                        DbHelper.MakeInParam("?Maxspacephotosize",(DbType)MySqlDbType.Int32,4,usergroupinfo.Maxspacephotosize),
						DbHelper.MakeInParam("?Groupid",(DbType)MySqlDbType.Int32,4,usergroupinfo.Groupid)

					};

            string sqlstring = "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups`  SET `radminid`=?Radminid,`grouptitle`=?Grouptitle,`creditshigher`=?Creditshigher," +
                "`creditslower`=?Creditslower,`stars`=?Stars,`color`=?Color,`groupavatar`=?Groupavatar,`readaccess`=?Readaccess, `allowvisit`=?Allowvisit,`allowpost`=?Allowpost," +
                "`allowreply`=?Allowreply,`allowpostpoll`=?Allowpostpoll, `allowdirectpost`=?Allowdirectpost,`allowgetattach`=?Allowgetattach,`allowpostattach`=?Allowpostattach," +
                "`allowvote`=?Allowvote,`allowmultigroups`=?Allowmultigroups,`allowsearch`=?Allowsearch,`allowavatar`=?Allowavatar,`allowcstatus`=?Allowcstatus," +
                "`allowuseblog`=?Allowuseblog,`allowinvisible`=?Allowinvisible,`allowtransfer`=?Allowtransfer,`allowsetreadperm`=?Allowsetreadperm," +
                "`allowsetattachperm`=?Allowsetattachperm,`allowhidecode`=?Allowhidecode,`allowhtml`=?Allowhtml,`allowcusbbcode`=?Allowcusbbcode,`allownickname`=?Allownickname," +
                "`allowsigbbcode`=?Allowsigbbcode,`allowsigimgcode`=?Allowsigimgcode,`allowviewpro`=?Allowviewpro,`allowviewstats`=?Allowviewstats,`allowtrade`=?Allowtrade," +
                "`allowdiggs`=?Allowdiggs,`disableperiodctrl`=?Disableperiodctrl,`allowdebate`=?Allowdebate,`allowbonus`=?Allowbonus,`minbonusprice`=?Minbonusprice,`maxbonusprice`=?Maxbonusprice," +
                "`reasonpm`=?Reasonpm,`maxprice`=?Maxprice,`maxpmnum`=?Maxpmnum,`maxsigsize`=?Maxsigsize,`maxattachsize`=?Maxattachsize," +
                "`maxsizeperday`=?Maxsizeperday,`attachextensions`=?Attachextensions,`maxspaceattachsize`=?Maxspaceattachsize,`maxspacephotosize`=?Maxspacephotosize  WHERE `groupid`=?Groupid";


            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, prams);
        }


        public void UpdateOnlineList(UserGroupInfo __usergroupinfo)
        {
            DbParameter[] parms = {
                                       // DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, __usergroupinfo.Groupid),
                                        DbHelper.MakeInParam("?title", (DbType)MySqlDbType.VarChar, 50, Utils.RemoveFontTag(__usergroupinfo.Grouptitle))
                                    };
            string sqlstring = "UPDATE `" + BaseConfigs.GetTablePrefix + "onlinelist` SET `title`=?title WHERE `groupid`=" + __usergroupinfo.Groupid + "";

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        public bool IsSystemOrTemplateUserGroup(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT *  FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE (`system`=1 OR `type`=1) AND `groupid`=?groupid", parm).Tables[0].Rows.Count > 0;
        }

        public DataTable GetOthersCommonUserGroup(int exceptgroupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, exceptgroupid);
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `groupid` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `radminid`=0 And `groupid`>8 AND `groupid`<>?groupid", parm).Tables[0];
        }

        public string GetUserGroupRAdminId(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `radminid` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE  `groupid`=?groupid", parm).Tables[0].Rows[0][0].ToString();
        }

        public void UpdateUserGroupLowerAndHigherToLimit(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "usergroups` SET `creditshigher`=-9999999 ,creditslower=9999999  WHERE `groupid`=?groupid", parm);
        }


        public void DeleteUserGroup(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`=?groupid", parm);
        }

        public void DeleteAdminGroup(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "admingroups` WHERE `admingid`=?groupid", parm);
        }

        public void DeleteOnlineList(int groupid)
        {
            DbParameter parm = DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid);
            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "onlinelist` WHERE `groupid`=?groupid", parm);
        }

        public int GetMaxUserGroupId()
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT IFNULL(MAX(groupid),0) FROM " + BaseConfigs.GetTablePrefix + "usergroups"), 0);
        }



        public bool DeletePaymentLog()
        {
            try
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "paymentlog`");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 按指定条件删除日志
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public bool DeletePaymentLog(string condition)
        {
            try
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "paymentlog` WHERE " + condition);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public DataTable GetPaymentLogList(int pagesize, int currentpage)
        {
            string sqlstring = "SELECT " + BaseConfigs.GetTablePrefix + "paymentlog.*, " + BaseConfigs.GetTablePrefix + "topics.fid as fid, " + BaseConfigs.GetTablePrefix + "topics.postdatetime as postdatatime, " + BaseConfigs.GetTablePrefix + "topics.poster as authorname, " + BaseConfigs.GetTablePrefix + "topics.title, " + BaseConfigs.GetTablePrefix + "users.username as username, " + BaseConfigs.GetTablePrefix + "paymentlog.id" + " FROM (" + BaseConfigs.GetTablePrefix + "paymentlog INNER JOIN " + BaseConfigs.GetTablePrefix + "topics ON " + BaseConfigs.GetTablePrefix + "paymentlog.tid = " + BaseConfigs.GetTablePrefix + "topics.tid) INNER JOIN " + BaseConfigs.GetTablePrefix + "users ON " + BaseConfigs.GetTablePrefix + "paymentlog.uid = " + BaseConfigs.GetTablePrefix + "users.uid LIMIT " + ((currentpage - 1) * pagesize).ToString() + "," + pagesize.ToString();
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring).Tables[0];
        }

        public DataTable GetPaymentLogList(int pagesize, int currentpage, string condition)
        {
            string sqlstring = "SELECT " + BaseConfigs.GetTablePrefix + "paymentlog.*, " + BaseConfigs.GetTablePrefix + "topics.fid as fid, " + BaseConfigs.GetTablePrefix + "topics.postdatetime as postdatatime, " + BaseConfigs.GetTablePrefix + "topics.poster as authorname, " + BaseConfigs.GetTablePrefix + "topics.title as title, " + BaseConfigs.GetTablePrefix + "users.username as username " +
  "FROM (" + BaseConfigs.GetTablePrefix + "paymentlog INNER JOIN " + BaseConfigs.GetTablePrefix + "topics ON " + BaseConfigs.GetTablePrefix + "paymentlog.tid = " + BaseConfigs.GetTablePrefix + "topics.tid) INNER JOIN " + BaseConfigs.GetTablePrefix + "users ON " + BaseConfigs.GetTablePrefix + "paymentlog.uid = " + BaseConfigs.GetTablePrefix + "users.uid " +
  "WHERE " + condition + " " +
  "ORDER BY " + BaseConfigs.GetTablePrefix + "paymentlog.id DESC LIMIT " + ((currentpage - 1) * pagesize).ToString() + "," + pagesize.ToString();

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring).Tables[0];
        }

        /// <summary>
        /// 得到积分交易日志记录数
        /// </summary>
        /// <returns></returns>
        public int GetPaymentLogListCount()
        {
            return Convert.ToInt32(DbHelper.ExecuteDataset(CommandType.Text, "SELECT count(id) FROM `" + BaseConfigs.GetTablePrefix + "paymentlog`").Tables[0].Rows[0][0].ToString());
        }

        /// <summary>
        /// 得到指定查询条件下的积分交易日志数
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public int GetPaymentLogListCount(string condition)
        {
            return Convert.ToInt32(DbHelper.ExecuteDataset(CommandType.Text, "SELECT count(id) FROM `" + BaseConfigs.GetTablePrefix + "paymentlog` WHERE " + condition).Tables[0].Rows[0][0].ToString());
        }

        public void DeleteModeratorByFid(int fid)
        {
            DbParameter[] prams =
            {
                DbHelper.MakeInParam("?fid", (DbType)MySqlDbType.Int32, 4, fid)
		    };
            string sql = "DELETE FROM `" + BaseConfigs.GetTablePrefix + "moderators` WHERE `fid`=?fid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }



        public DataTable GetUidModeratorByFid(string fidlist)
        {
            string sql = "SELECT DISTINCT `uid` FROM `" + BaseConfigs.GetTablePrefix + "moderators` WHERE `fid` IN(" + fidlist + ")";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public void AddModerator(int uid, int fid, int displayorder, int inherited)
        {
            DbParameter[] prams =
            {
                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid),
                DbHelper.MakeInParam("?fid", (DbType)MySqlDbType.Int16, 2, fid),
                DbHelper.MakeInParam("?displayorder", (DbType)MySqlDbType.Int16, 2, displayorder),
                DbHelper.MakeInParam("?inherited", (DbType)MySqlDbType.Int16, 2, inherited)
		    };
            string sql = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "moderators` (uid,fid,displayorder,inherited) VALUES(?uid,?fid,?displayorder,?inherited)";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public DataTable GetModeratorInfo(string moderator)
        {
            DbParameter[] prams =
            {
				DbHelper.MakeInParam("?username", (DbType)MySqlDbType.VarChar, 20, moderator.Trim())
			};

            return DbHelper.ExecuteDataset(CommandType.Text, "Select `uid`,`groupid`  From `" + BaseConfigs.GetTablePrefix + "users` Where `groupid`<>7 AND `groupid`<>8 AND `username`=?username LIMIT 1", prams).Tables[0];
        }

        public void SetModerator(string moderator)
        {
            DbParameter[] prams = 
            {
				DbHelper.MakeInParam("?username", (DbType)MySqlDbType.VarChar, 20, moderator.Trim())
			};
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `adminid`=3,`groupid`=3 WHERE `username`=?username", prams);
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "online` SET `adminid`=3,`groupid`=3 WHERE `username`=?username", prams);
        }



        public DataTable GetUidAdminIdByUsername(string username)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?username", (DbType)MySqlDbType.VarChar, 20, username)
			};
            string sql = "Select `uid`,`adminid` From `" + BaseConfigs.GetTablePrefix + "users` Where `username` = ?username LIMIT 1";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public DataTable GetUidInModeratorsByUid(int currentfid, int uid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?currentfid", (DbType)MySqlDbType.Int32, 4, currentfid),
                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
			};
            string sql = "Select `uid`  FROM `" + BaseConfigs.GetTablePrefix + "moderators` WHERE `fid`<>?currentfid AND `uid`=?uid LIMIT 1";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public void UpdateUserOnlineInfo(int groupid, int userid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid),
                DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32, 4, userid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "online` SET `groupid`=?groupid  WHERE `userid`=?userid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void UpdateUserOtherInfo(int groupid, int userid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?groupid", (DbType)MySqlDbType.Int32, 4, groupid),
                DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32, 4, userid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `groupid`=?groupid ,`adminid`=0  WHERE `uid`=?userid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        /// <summary>
        /// 得到论坛中最后注册的用户ID和用户名
        /// </summary>
        /// <param name="lastuserid">输出参数：最后注册的用户ID</param>
        /// <param name="lastusername">输出参数：最后注册的用户名</param>
        /// <returns>存在返回true,不存在返回false</returns>
        public bool GetLastUserInfo(out string lastuserid, out string lastusername)
        {
            lastuserid = "";
            lastusername = "";

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid`,`username` FROM `" + BaseConfigs.GetTablePrefix + "users` ORDER BY `uid` DESC LIMIT 1");
            if (reader != null)
            {
                if (reader.Read())
                {
                    lastuserid = reader["uid"].ToString();
                    lastusername = reader["username"].ToString().Trim();
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;

        }

        public IDataReader GetTopUsers(int statcount, int lastuid)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("@lastuid", (DbType)MySqlDbType.Int32, 4, lastuid),
			};

            return DbHelper.ExecuteReader(CommandType.Text, "SELECT TOP " + statcount + " [uid] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [uid] > @lastuid", prams);
        }

        //public IDataReader GetTopUsers(int statcount, int lastuid)
        //{
        //    DbParameter[] prams = {
        //        DbHelper.MakeInParam("?lastuid", (DbType)MySqlDbType.Int32,4, lastuid),
        //    };

        //    return DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid` > ?lastuid LIMIT " + statcount.ToString(), prams);
        //}

        public void ResetUserDigestPosts(int userid)
        {
            DbParameter parm = DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, userid);
            //DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET [digestposts]=(SELECT COUNT(tid) AS [digestposts] FROM `" + BaseConfigs.GetTablePrefix + "topics` WHERE `" + BaseConfigs.GetTablePrefix + "topics`.[posterid] = `" + BaseConfigs.GetTablePrefix + "users`.[uid] AND [digest] > 0) WHERE `" + BaseConfigs.GetTablePrefix + "users`.[uid] = ?uid", parm);

            int countdigestpost = Utils.StrToInt(DbHelper.ExecuteScalarToStr(CommandType.Text, "SELECT COUNT(tid) AS `digestposts` FROM `" + BaseConfigs.GetTablePrefix + "topics` WHERE `" + BaseConfigs.GetTablePrefix + "topics`.`posterid` =" + userid + " AND `digest` > 0"), 0);
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `digestposts`=" + countdigestpost + " WHERE `" + BaseConfigs.GetTablePrefix + "users`.`uid` = ?uid", parm);



        }

        public IDataReader GetUsers(int start_uid, int end_uid)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("?start_uid", (DbType)MySqlDbType.Int32, 4, start_uid),
				DbHelper.MakeInParam("?end_uid", (DbType)MySqlDbType.Int32, 4, end_uid)
			};

            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid` >= ?start_uid AND `uid`<=?end_uid", prams);
        }

        public void UpdateUserPostCount(int postcount, int userid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("?postcount", (DbType)MySqlDbType.Int32, 4, postcount),
                                        DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32, 4, userid)
                                    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `posts`=?postcount WHERE `" + BaseConfigs.GetTablePrefix + "users`.`uid` = ?userid", parms);
        }


        /// <summary>
        /// 获得所有版主列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetModeratorList()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT * FROM `{0}moderators`", BaseConfigs.GetTablePrefix)).Tables[0];
        }


        /// <summary>
        /// 获得全部在线用户数
        /// </summary>
        /// <returns></returns>
        public int GetOnlineAllUserCount()
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(olid) FROM `" + BaseConfigs.GetTablePrefix + "online`"), 1);
        }

        /// <summary>
        /// 创建在线表
        /// </summary>
        /// <returns></returns>
        public int CreateOnlineTable()
        {
            try
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "DROP TABLE IF EXISTS `" + BaseConfigs.GetTablePrefix + "online`");
                DbHelper.ExecuteNonQuery(CommandType.Text, "CREATE TABLE `" + BaseConfigs.GetTablePrefix + "online` (`olid` int(11) NOT NULL auto_increment,`userid` int(11) NOT NULL default '-1',`ip` varchar(15) NOT NULL default '0.0.0.0', `username` varchar(20) NOT NULL default '',`nickname` varchar(20) NOT NULL default '',`password` varchar(32) NOT NULL default '',`groupid` smallint(6) NOT NULL default '0',`olimg` varchar(80) NOT NULL default '', `adminid` smallint(6) NOT NULL default '0',`invisible` smallint(6) NOT NULL default '0',`action` smallint(6) NOT NULL default '0', `lastactivity` smallint(6) NOT NULL default '0', `lastposttime` datetime NOT NULL default '1900-01-01 00:00:00',`lastpostpmtime` datetime NOT NULL default '1900-01-01 00:00:00', `lastsearchtime` datetime NOT NULL default '1900-01-01 00:00:00', `lastupdatetime` datetime NOT NULL,`forumid` int(11) NOT NULL default '0',`forumname` varchar(50) NOT NULL default '', `titleid` int(11) NOT NULL default '0',`title` varchar(80) NOT NULL default '', `verifycode` varchar(10) NOT NULL default '',PRIMARY KEY(`olid`), KEY `forum` (`userid`,`forumid`,`invisible`),KEY `forumid` (`forumid`), KEY `invisible` (`userid`,`invisible`),KEY `ip` (`userid`,`ip`),KEY `password` (`userid`,`password`) ) ENGINE=MEMORY AUTO_INCREMENT=1 DEFAULT CHARSET=gbk");
                DbHelper.ExecuteNonQuery(CommandType.Text, "ALTER TABLE `" + BaseConfigs.GetTablePrefix + "online` ADD PRIMARY KEY ( `olid` ) ");

                DbHelper.ExecuteNonQuery(CommandType.Text, "CREATE INDEX `forum` ON `" + BaseConfigs.GetTablePrefix + "online`(`userid`, `forumid`, `invisible`);");
                DbHelper.ExecuteNonQuery(CommandType.Text, "CREATE INDEX `invisible` ON `" + BaseConfigs.GetTablePrefix + "online`(`userid`, `invisible`)");
                DbHelper.ExecuteNonQuery(CommandType.Text, "CREATE INDEX `forumid` ON `" + BaseConfigs.GetTablePrefix + "online`(`forumid`)");
                DbHelper.ExecuteNonQuery(CommandType.Text, "CREATE INDEX `password` ON `" + BaseConfigs.GetTablePrefix + "online`(`userid`, `password`)");
                DbHelper.ExecuteNonQuery(CommandType.Text, "CREATE INDEX `ip` ON `" + BaseConfigs.GetTablePrefix + "online`(`userid`, `ip`)");


                return 1;
            }
            catch
            {
                return -1;
            }
        }

        ///// <summary>
        ///// 取得在线表最后一条记录的tickcount字段
        ///// </summary>
        ///// <returns></returns>
        //public DateTime GetLastUpdateTime()
        //{
        //    object val =
        //        DbHelper.ExecuteScalar(CommandType.Text,
        //                               "SELECT TOP 1 [lastupdatetime] FROM [" + BaseConfigs.GetTablePrefix +
        //                               "online] ORDER BY [olid] DESC");
        //    return val == null ? DateTime.Now : Convert.ToDateTime(val);
        //}

        /// <summary>
        /// 获得在线注册用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        public int GetOnlineUserCount()
        {

            return int.Parse(DbHelper.ExecuteDataset(CommandType.Text, "SELECT COUNT(olid) FROM `" + BaseConfigs.GetTablePrefix + "online` WHERE `userid`>0").Tables[0].Rows[0][0].ToString());

        }

        /// <summary>
        /// 获得版块在线用户列表
        /// </summary>
        /// <param name="forumid">版块Id</param>
        /// <returns></returns>
        public DataTable GetForumOnlineUserListTable(int forumid)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT * FROM `{0}online` WHERE `forumid`={1}", BaseConfigs.GetTablePrefix, forumid)).Tables[0];
        }

        /// <summary>
        /// 获得全部在线用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetOnlineUserListTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "online`").Tables[0];
        }

        /// <summary>
        /// 获得版块在线用户列表
        /// </summary>
        /// <param name="forumid">版块Id</param>
        /// <returns></returns>
        public IDataReader GetForumOnlineUserList(int forumid)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}online` WHERE `forumid`={1}", BaseConfigs.GetTablePrefix, forumid.ToString()));
        }

        /// <summary>
        /// 获得全部在线用户列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetOnlineUserList()
        {
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "online`");
        }

        /// <summary>
        /// 返回在线用户图例
        /// </summary>
        /// <returns></returns>
        public DataTable GetOnlineGroupIconTable()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `groupid`, `displayorder`, `title`, `img` FROM `" + BaseConfigs.GetTablePrefix + "onlinelist` WHERE `img` <> '' ORDER BY `displayorder`").Tables[0];
        }

        /// <summary>
        /// 根据uid获得olid
        /// </summary>
        /// <param name="uid">uid</param>
        /// <returns>olid</returns>
        public int GetOlidByUid(int uid)
        {
            DbParameter[] parms = { DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32, 4, uid) };
            return Utils.StrToInt(DbHelper.ExecuteScalarToStr(CommandType.Text, string.Format("SELECT olid FROM `{0}online` WHERE `userid`=?userid", BaseConfigs.GetTablePrefix), parms), -1);
        }

        /// <summary>
        /// 获得指定在线用户
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <returns>在线用户的详细信息</returns>
        public IDataReader GetOnlineUser(int olid)
        {
            DbParameter[] parms = { DbHelper.MakeInParam("?olid", (DbType)MySqlDbType.Int32, 4, olid) };
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM `{0}online` WHERE `olid`=?olid", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 获得指定用户的详细信息
        /// </summary>
        /// <param name="userid">在线用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>用户的详细信息</returns>
        public DataTable GetOnlineUser(int userid, string password)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32, 4, userid),
                                        DbHelper.MakeInParam("?password", (DbType)MySqlDbType.VarChar, 32, password)
                                    };
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT * FROM `{0}online` WHERE `userid`=?userid AND `password`=?password LIMit 1", BaseConfigs.GetTablePrefix), parms).Tables[0];
        }

        /// <summary>
        /// 获得指定用户的详细信息
        /// </summary>
        /// <param name="userid">在线用户ID</param>
        /// <param name="ip">IP</param>
        /// <returns></returns>
        public DataTable GetOnlineUserByIP(int userid, string ip)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32, 4, userid),
                                        DbHelper.MakeInParam("?ip", (DbType)MySqlDbType.VarChar, 15, ip)
                                    };
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT * FROM `{0}online` WHERE `userid`=?userid AND `ip`=?ip LIMIT 1", BaseConfigs.GetTablePrefix), parms).Tables[0];
        }

        /// <summary>
        /// 检查在线用户验证码是否有效
        /// </summary>
        /// <param name="olid">在组用户ID</param>
        /// <param name="verifycode">验证码</param>
        /// <returns>在组用户ID</returns>
        public bool CheckUserVerifyCode(int olid, string verifycode, string newverifycode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("?olid", (DbType)MySqlDbType.Int32, 4, olid),
                                        DbHelper.MakeInParam("?verifycode", (DbType)MySqlDbType.VarChar, 10, verifycode)
                                    };
            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT `olid` FROM `{0}online` WHERE `olid`=?olid and `verifycode`=?verifycode LIMIT 1", BaseConfigs.GetTablePrefix), parms).Tables[0];
            parms[1].Value = newverifycode;
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}online` SET `verifycode`=?verifycode WHERE `olid`=?olid", BaseConfigs.GetTablePrefix), parms);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 设置用户在线状态
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="onlinestate">在线状态，１在线</param>
        /// <returns></returns>
        public int SetUserOnlineState(int uid, int onlinestate)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}users` SET `onlinestate`={1},`lastactivity`=now() WHERE `uid`={2}", BaseConfigs.GetTablePrefix, onlinestate, uid));
        }

        /// <summary>
        /// 删除符合条件的一个或多个用户信息
        /// </summary>
        /// <returns>删除行数</returns>
        public int DeleteRowsByIP(string ip)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?ip",(DbType)MySqlDbType.VarChar,15,ip)
								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `onlinestate`=0,`lastactivity`=now() WHERE `uid` IN (SELECT `userid` FROM `" + BaseConfigs.GetTablePrefix + "online` WHERE `userid`>0 AND `ip`=?ip)", prams);
            if (ip != "0.0.0.0")
            {
                return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "online` WHERE `userid`=-1 AND `ip`=?ip", prams);
            }
            return 0;
        }

        /// <summary>
        /// 删除在线表中指定在线id的行
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <returns></returns>
        public int DeleteRows(int olid)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "online` WHERE `olid`=" + olid.ToString());
        }

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作</param>
        /// <param name="inid">所在位置代码</param>
        public void UpdateAction(int olid, int action, int inid)
        {
            DbParameter[] prams = {
                                           //DbHelper.MakeInParam("?tickcount",(DbType)MySqlDbType.Int32,4,System.Environment.TickCount),
                                          DbHelper.MakeInParam("?lastupdatetime",(DbType)MySqlDbType.Datetime,8,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
										   DbHelper.MakeInParam("?action",(DbType)MySqlDbType.Int16,2,action),
										   DbHelper.MakeInParam("?forumid",(DbType)MySqlDbType.Int32,4,inid),
										   DbHelper.MakeInParam("?forumname",(DbType)MySqlDbType.VarChar,100,""),
										   DbHelper.MakeInParam("?titleid",(DbType)MySqlDbType.Int32,4,inid),
										   DbHelper.MakeInParam("?title",(DbType)MySqlDbType.VarChar,160,""),
										   DbHelper.MakeInParam("?olid",(DbType)MySqlDbType.Int32,4,olid)

									   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "online` SET `lastactivity`=`action`,`action`=?action,`lastupdatetime`=?lastupdatetime,`forumid`=?forumid,`forumname`=?forumname,`titleid`=?titleid,`title`=?title WHERE `olid`=?olid", prams);
        }

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作id</param>
        /// <param name="fid">版块id</param>
        /// <param name="forumname">版块名</param>
        /// <param name="tid">主题id</param>
        /// <param name="topictitle">主题名</param>
        public void UpdateAction(int olid, int action, int fid, string forumname, int tid, string topictitle)
        {
            DbParameter[] prams = {
                                           //DbHelper.MakeInParam("?tickcount",(DbType)MySqlDbType.Int32,4,System.Environment.TickCount),
                                           DbHelper.MakeInParam("?lastupdatetime",(DbType)MySqlDbType.Datetime,8,DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))),
										   DbHelper.MakeInParam("?action",(DbType)MySqlDbType.Int16,2,action),
										   DbHelper.MakeInParam("?forumid",(DbType)MySqlDbType.Int32,4,fid),
										   DbHelper.MakeInParam("?forumname",(DbType)MySqlDbType.VarChar,100,forumname),
										   DbHelper.MakeInParam("?titleid",(DbType)MySqlDbType.Int32,4,tid),
										   DbHelper.MakeInParam("?title",(DbType)MySqlDbType.VarChar,160,topictitle),
										   DbHelper.MakeInParam("?olid",(DbType)MySqlDbType.Int32,4,olid)

									   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "online` SET `lastactivity`=`action`,`action`=?action,`lastupdatetime`=?lastupdatetime,`forumid`=?forumid,`forumname`=?forumname,`titleid`=?titleid,`title`=?title WHERE `olid`=?olid", prams);
        }

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public void UpdateLastTime(int olid)
        {
            DbParameter[] prams = {
                                           DbHelper.MakeInParam("?lastupdatetime",(DbType)MySqlDbType.Datetime,8,DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))),
                                           //DbHelper.MakeInParam("?tickcount",(DbType)MySqlDbType.Int32,4,System.Environment.TickCount),
										   DbHelper.MakeInParam("?olid",(DbType)MySqlDbType.Int32,4,olid)

									   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "online` SET `lastupdatetime`=?lastupdatetime WHERE `olid`=?olid", prams);
        }

        /// <summary>
        /// 更新用户最后发帖时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public void UpdatePostTime(int olid)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}online` SET `lastposttime`=now() WHERE `olid`={1}", BaseConfigs.GetTablePrefix, olid.ToString()));
        }

        /// <summary>
        /// 更新用户最后发短消息时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public void UpdatePostPMTime(int olid)
        {

            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}online` SET `lastpostpmtime`=now() WHERE `olid`={1}", BaseConfigs.GetTablePrefix, olid.ToString()));

        }

        /// <summary>
        /// 更新在线表中指定用户是否隐身
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="invisible">是否隐身</param>
        public void UpdateInvisible(int olid, int invisible)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}online` SET `invisible`={1} WHERE `olid`={2}", BaseConfigs.GetTablePrefix, invisible.ToString(), olid.ToString()));
        }

        /// <summary>
        /// 更新在线表中指定用户的用户密码
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="password">用户密码</param>
        public void UpdatePassword(int olid, string password)
        {

            DbParameter[] prams = {

									   DbHelper.MakeInParam("?olid",(DbType)MySqlDbType.Int32,4,olid),
                                       DbHelper.MakeInParam("?password",(DbType)MySqlDbType.String,32,password)

								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}online` SET `password`=now() WHERE `olid`=?olid", BaseConfigs.GetTablePrefix), prams);
        }

        /// <summary>
        /// 更新用户IP地址
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="ip">ip地址</param>
        public void UpdateIP(int olid, string ip)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?ip",(DbType)MySqlDbType.VarChar,15,ip),
									   DbHelper.MakeInParam("?olid",(DbType)MySqlDbType.Int32,4,olid)

								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}online` SET `ip`=?ip WHERE `olid`=?olid", BaseConfigs.GetTablePrefix), prams);

        }

        /// <summary>
        /// 更新用户最后搜索时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public void UpdateSearchTime(int olid)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}online` SET `lastsearchtime`=now() WHERE `olid`={1}", BaseConfigs.GetTablePrefix, olid.ToString()));
        }

        /// <summary>
        /// 更新用户的用户组
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="groupid">组名</param>
        public void UpdateGroupid(int userid, int groupid)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}online` SET `groupid`={1} WHERE `userid`={2}", BaseConfigs.GetTablePrefix, groupid.ToString(), userid.ToString()));
        }


        /// <summary>
        /// 获得指定ID的短消息的内容
        /// </summary>
        /// <param name="pmid">短消息pmid</param>
        /// <returns>短消息内容</returns>
        public IDataReader GetPrivateMessageInfo(int pmid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?pmid", (DbType)MySqlDbType.Int32,4, pmid),
			                        };
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `pmid`=?pmid LIMIT 1", prams);
        }

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttypewhere">筛选条件</param>
        /// <returns>短信息列表</returns>
        public IDataReader GetPrivateMessageList(int userid, int folder, int pagesize, int pageindex, int inttype)
        {

            string strwhere = "";
            if (inttype == 1)
            {
                strwhere = "`new`=1";
            }

            DbParameter[] prams = {
                         
                                       DbHelper.MakeInParam("?strwhere",(DbType)MySqlDbType.VarChar,500,strwhere)

                                   };

            string strsql;
            string msgformortoid = "msgtoid";
            if (folder == 1 || folder == 2)
            {

                msgformortoid = "msgfromid";
            }

            //if (pageindex == 1)
            //{
                if (strwhere != "")
                {
                    strsql = "SELECT `pmid`,`msgfrom`,`msgfromid`,`msgto`,`msgtoid`,`folder`,`new`,`subject`,`postdatetime`,`message` FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `" + msgformortoid + "`= " + userid + "  AND `folder`= " + folder + "  AND  " + strwhere + " ORDER BY `pmid` DESC LIMIT " + ((pageindex- 1) * pagesize).ToString()+ "," + pagesize;
                }
                else
                {
                    strsql = "SELECT `pmid`,`msgfrom`,`msgfromid`,`msgto`,`msgtoid`,`folder`,`new`,`subject`,`postdatetime`,`message` FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `" + msgformortoid + "`= " + userid + "  AND `folder`= " + folder + "  ORDER BY `pmid` DESC LIMIT " + ((pageindex - 1) * pagesize).ToString() + "," + pagesize;

                }



            return DbHelper.ExecuteReader(CommandType.Text, strsql,prams);
        }

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        public int GetPrivateMessageCount(int userid, int folder, int state)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?userid",(DbType)MySqlDbType.Int32,4,userid),
									   DbHelper.MakeInParam("?folder",(DbType)MySqlDbType.Int32,4,folder),
									   DbHelper.MakeInParam("?state",(DbType)MySqlDbType.Int32,4,state)
								   };



            string sqlGetPMCount = string.Empty;
            if (folder == -1)
            {
                sqlGetPMCount = "SELECT COUNT(pmid) AS `pmcount` FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE (`msgtoid`=?userid AND `folder`=0) OR (`msgfromid`=?userid AND `folder` = 1)  OR (`msgfromid` = ?userid AND `folder` = 2)";
            }
            else
            {
                if (folder == 0)
                {
                    if (state == -1)
                    {
                        sqlGetPMCount = "SELECT COUNT(pmid) AS `pmcount` FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `msgtoid`=?userid AND `folder`=?folder";
                    }
                    else
                    {
                        sqlGetPMCount = "SELECT COUNT(pmid) AS `pmcount` FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `msgtoid`=?userid AND `folder`=?folder AND `new`=?state";
                    }
                }
                else
                {
                    if (state == -1)
                    {
                        sqlGetPMCount = "SELECT COUNT(pmid) AS `pmcount` FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `msgfromid`=?userid AND `folder`=?folder";
                    }
                    else
                    {
                        sqlGetPMCount = "SELECT COUNT(pmid) AS `pmcount` FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `msgfromid`=?userid AND `folder`=?folder AND `new`=?state";
                    }
                }
            }


            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlGetPMCount, prams).ToString(), 0);
        }

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="__privatemessageinfo">短消息内容</param>
        /// <param name="savetosentbox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        public int CreatePrivateMessage(PrivateMessageInfo __privatemessageinfo, int savetosentbox)
        {
            if (__privatemessageinfo.Folder != 0)
            {
                __privatemessageinfo.Msgfrom = __privatemessageinfo.Msgto;
            }
            else
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `newpmcount`= ABS(IF(ISNULL(`newpmcount`),0,newpmcount)*1)+1,`newpm` = 1 WHERE `uid`=" + __privatemessageinfo.Msgtoid + "");
            }

            DbParameter[] prams = {

									   DbHelper.MakeInParam("?msgfrom",(DbType)MySqlDbType.VarChar,20,__privatemessageinfo.Msgfrom),
									   DbHelper.MakeInParam("?msgfromid",(DbType)MySqlDbType.Int32,4,__privatemessageinfo.Msgfromid),
									   DbHelper.MakeInParam("?msgto",(DbType)MySqlDbType.VarChar,20,__privatemessageinfo.Msgto),
									   DbHelper.MakeInParam("?msgtoid",(DbType)MySqlDbType.Int32,4,__privatemessageinfo.Msgtoid),
									   DbHelper.MakeInParam("?folder",(DbType)MySqlDbType.Int16,2,__privatemessageinfo.Folder),
									   DbHelper.MakeInParam("?new",(DbType)MySqlDbType.Int32,4,__privatemessageinfo.New),
									   DbHelper.MakeInParam("?subject",(DbType)MySqlDbType.VarString,80,__privatemessageinfo.Subject),
									   DbHelper.MakeInParam("?postdatetime",(DbType)MySqlDbType.Datetime,8,DateTime.Parse(__privatemessageinfo.Postdatetime)),
									   DbHelper.MakeInParam("?message",(DbType)MySqlDbType.VarChar,0,__privatemessageinfo.Message),
									   DbHelper.MakeInParam("?savetosentbox",(DbType)MySqlDbType.Int32,4,savetosentbox),
                                       DbHelper.MakeInParam("?pmid",(DbType)MySqlDbType.Int32,4,__privatemessageinfo.Pmid)
								   };

            string sql1 = "insert into `" + BaseConfigs.GetTablePrefix + "pms`(`msgfrom`,`msgfromid`,`msgto`,`msgtoid`,`folder`,`new`,`subject`,`postdatetime`,`message`) VALUES(?msgfrom,?msgfromid,?msgto,?msgtoid,?folder,?new,?subject,?postdatetime,?message)";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql1, prams);

            int s = Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, "select pmid from `" + BaseConfigs.GetTablePrefix + "pms` order by pmid desc LIMIT 1").Tables[0].Rows[0][0].ToString(), -1);






            if ((savetosentbox == 1) && (__privatemessageinfo.Folder == 0))
            {


                DbHelper.ExecuteNonQuery(CommandType.Text, "INSERT INTO `" + BaseConfigs.GetTablePrefix + "pms` " +
                          "(`msgfrom`,`msgfromid`,`msgto`,`msgtoid`,`folder`,`new`,`subject`,`postdatetime`,`message`) " +
                          "VALUES " +
                          "(?msgfrom,?msgfromid,?msgto,?msgtoid,1,?new,?subject,?postdatetime,?message)", prams);
            }




            return s;
        }

        /// <summary>
        /// 删除指定用户的短信息
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="pmitemid">要删除的短信息列表(数组)</param>
        /// <returns>删除记录数</returns>
        public int DeletePrivateMessages(int userid, string pmidlist)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32,4, userid)
			};

            return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `pmid` IN (" + pmidlist + ") AND (`msgtoid` = ?userid OR `msgfromid` = ?userid)", prams);
        }

        /// <summary>
        /// 获得新短消息数
        /// </summary>
        /// <returns></returns>
        public int GetNewPMCount(int userid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32,4, userid)
			};
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(`pmid`) AS `pmcount` FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `new` = 1 AND `folder` = 0 AND `msgtoid` = ?userid", prams), 0);
        }

        /// <summary>
        /// 删除指定用户的一条短消息
        /// </summary>
        /// <param name="userid">用户Ｉｄ</param>
        /// <param name="pmid">ＰＭＩＤ</param>
        /// <returns></returns>
        public int DeletePrivateMessage(int userid, int pmid)
        {
            DbParameter[] prams = {     DbHelper.MakeInParam("?pmid", (DbType)MySqlDbType.Int32,4, pmid),
									   DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32,4, userid)

			};
            return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM `" + BaseConfigs.GetTablePrefix + "pms` WHERE `pmid`=?pmid AND (`msgtoid` = ?userid OR `msgfromid` = ?userid)", prams);

        }

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmid">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        public int SetPrivateMessageState(int pmid, byte state)
        {

            DbParameter[] prams = {
                                       DbHelper.MakeInParam("?state",(DbType)MySqlDbType.Int16,1,state),
									   DbHelper.MakeInParam("?pmid", (DbType)MySqlDbType.Int32,1,pmid)

								   };
            return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "pms` SET `new`=?state WHERE `pmid`=?pmid", prams);

        }

        public int GetRAdminIdByGroup(int groupid)
        {
            return Convert.ToInt32(DbHelper.ExecuteDataset(CommandType.Text, "SELECT `radminid` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`=" + groupid + " LIMIT 1").Tables[0].Rows[0][0].ToString());
        }

        public string GetUserGroupsStr()
        {
            return "SELECT `groupid`, `grouptitle` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` ORDER BY `groupid`";
        }


        public DataTable GetUserNameListByGroupid(string groupidlist)
        {
            string sql = "SELECT `uid` ,`username`  From `" + BaseConfigs.GetTablePrefix + "users` WHERE `groupid` IN(" + groupidlist + ")";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public DataTable GetUserNameByUid(int uid)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
			};
            string sql = "SELECT `username` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=?uid LIMIT 1";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public void ResetPasswordUid(string password, int uid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("?password", (DbType)MySqlDbType.String, 32, password),
				DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
			};
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `password`=?password WHERE `uid`=?uid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void SendPMToUser(string msgfrom, int msgfromid, string msgto, int msgtoid, int folder, string subject, DateTime postdatetime, string message)
        {
            DbParameter[] prams =
			{
				DbHelper.MakeInParam("?msgfrom", (DbType)MySqlDbType.VarChar,50, msgfrom),
				DbHelper.MakeInParam("?msgfromid", (DbType)MySqlDbType.Int32, 4, msgfromid),
				DbHelper.MakeInParam("?msgto", (DbType)MySqlDbType.String,50, msgto),
				DbHelper.MakeInParam("?msgtoid", (DbType)MySqlDbType.Int32, 4, msgtoid),
                DbHelper.MakeInParam("?folder", (DbType)MySqlDbType.Int16, 2, folder),
                DbHelper.MakeInParam("?subject", (DbType)MySqlDbType.String,60, subject),
                DbHelper.MakeInParam("?postdatetime", (DbType)MySqlDbType.Datetime,8, postdatetime),
				DbHelper.MakeInParam("?message",(DbType)MySqlDbType.String, 0,message)
			};
            string sql = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "pms` (msgfrom,msgfromid,msgto,msgtoid,folder,new,subject,postdatetime,message) " +
                "VALUES (?msgfrom,?msgfromid,?msgto,?msgtoid,?folder,1,?subject,?postdatetime,?message)";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);


            DbParameter[] prams1 =
			{

				DbHelper.MakeInParam("?msgtoid", (DbType)MySqlDbType.Int32, 4, msgtoid)

			};
            sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `newpmcount`=`newpmcount`+1  WHERE `uid` =?msgtoid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams1);
        }

        public string GetSystemGroupInfoSql()
        {
            return "Select * From `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`<=8 Order By `groupid`";
        }

        public void UpdateUserCredits(int uid, string credits)
        {
            DbParameter[] prams_credits = {
											   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
										   };

            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}users` SET `credits` = {1} WHERE `uid`=?uid", BaseConfigs.GetTablePrefix, credits), prams_credits);
        }

        public void UpdateUserGroup(int uid, int groupid)
        {
            DbParameter[] prams_credits = {
											   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
										   };

            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}users` SET `groupid` = {1} WHERE `uid`=?uid", BaseConfigs.GetTablePrefix, groupid), prams_credits);

        }

        public bool CheckUserCreditsIsEnough(int uid, float[] values)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid),
									   DbHelper.MakeInParam("?extcredits1", (DbType)MySqlDbType.Decimal, 8, values[0]),
									   DbHelper.MakeInParam("?extcredits2", (DbType)MySqlDbType.Decimal, 8, values[1]),
									   DbHelper.MakeInParam("?extcredits3", (DbType)MySqlDbType.Decimal, 8, values[2]),
									   DbHelper.MakeInParam("?extcredits4", (DbType)MySqlDbType.Decimal, 8, values[3]),
									   DbHelper.MakeInParam("?extcredits5", (DbType)MySqlDbType.Decimal, 8, values[4]),
									   DbHelper.MakeInParam("?extcredits6", (DbType)MySqlDbType.Decimal, 8, values[5]),
									   DbHelper.MakeInParam("?extcredits7", (DbType)MySqlDbType.Decimal, 8, values[6]),
									   DbHelper.MakeInParam("?extcredits8", (DbType)MySqlDbType.Decimal, 8, values[7])
								   };
            string CommandText = "SELECT COUNT(1) FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=?uid AND"
                    + "	`extcredits1`>= IF(?extcredits1<0 ,ABS(?extcredits1),`extcredits1`) AND "
                    + "	`extcredits2`>= IF(?extcredits2<0 ,ABS(?extcredits2),`extcredits2`) AND "
                    + "	`extcredits3`>= IF(?extcredits3<0 ,ABS(?extcredits3),`extcredits3`) AND "
                    + "	`extcredits4`>= IF(?extcredits4<0 ,ABS(?extcredits4),`extcredits4`) AND "
                    + "	`extcredits5`>= IF(?extcredits5<0 ,ABS(?extcredits5),`extcredits5`) AND "
                    + "	`extcredits6`>= IF(?extcredits6<0 ,ABS(?extcredits6),`extcredits6`) AND "
                    + "	`extcredits7`>= IF(?extcredits7<0 ,ABS(?extcredits7),`extcredits7`) AND "
                    + "	`extcredits8`>= IF(?extcredits8<0 ,ABS(?extcredits8),`extcredits8`) ";

            if (Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, CommandText, prams)) == 0)
            {
                return false;
            }
            return true;
        }

        public void UpdateUserCredits(int uid, float[] values)
        {
            DbParameter[] prams = {

									   DbHelper.MakeInParam("?extcredits1", (DbType)MySqlDbType.Decimal, 8, values[0]),
									   DbHelper.MakeInParam("?extcredits2", (DbType)MySqlDbType.Decimal, 8, values[1]),
									   DbHelper.MakeInParam("?extcredits3", (DbType)MySqlDbType.Decimal, 8, values[2]),
									   DbHelper.MakeInParam("?extcredits4", (DbType)MySqlDbType.Decimal, 8, values[3]),
									   DbHelper.MakeInParam("?extcredits5", (DbType)MySqlDbType.Decimal, 8, values[4]),
									   DbHelper.MakeInParam("?extcredits6", (DbType)MySqlDbType.Decimal, 8, values[5]),
									   DbHelper.MakeInParam("?extcredits7", (DbType)MySqlDbType.Decimal, 8, values[6]),
									   DbHelper.MakeInParam("?extcredits8", (DbType)MySqlDbType.Decimal, 8, values[7]),
                                       DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
								   };

            string CommandText = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET "
                + "		`extcredits1`=`extcredits1` + ?extcredits1, "
                + "		`extcredits2`=`extcredits2` + ?extcredits2, "
                + "		`extcredits3`=`extcredits3` + ?extcredits3, "
                + "		`extcredits4`=`extcredits4` + ?extcredits4, "
                + "		`extcredits5`=`extcredits5` + ?extcredits5, "
                + "		`extcredits6`=`extcredits6` + ?extcredits6, "
                + "		`extcredits7`=`extcredits7` + ?extcredits7, "
                + "		`extcredits8`=`extcredits8` + ?extcredits8 "
                + "WHERE `uid`=?uid";

            DbHelper.ExecuteNonQuery(CommandType.Text, CommandText, prams);
        }

        public bool CheckUserCreditsIsEnough(int uid, DataRow values, int pos, int mount)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid),
									   DbHelper.MakeInParam("?extcredits1", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits1"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits2", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits2"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits3", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits3"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits4", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits4"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits5", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits5"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits6", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits6"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits7", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits7"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits8", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits8"],0) * pos * mount)
								   };
            //string CommandText = "SELECT COUNT(1) FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE [uid]=?uid AND"
            //        + "	[extcredits1]>= (case when ?extcredits1 >= 0 then abs(?extcredits1) else 0 end) AND "
            //        + "	[extcredits2]>= (case when ?extcredits2 >= 0 then abs(?extcredits2) else 0 end) AND "
            //        + "	[extcredits3]>= (case when ?extcredits3 >= 0 then abs(?extcredits3) else 0 end) AND "
            //        + "	[extcredits4]>= (case when ?extcredits4 >= 0 then abs(?extcredits4) else 0 end) AND "
            //        + "	[extcredits5]>= (case when ?extcredits5 >= 0 then abs(?extcredits5) else 0 end) AND "
            //        + "	[extcredits6]>= (case when ?extcredits6 >= 0 then abs(?extcredits6) else 0 end) AND "
            //        + "	[extcredits7]>= (case when ?extcredits7 >= 0 then abs(?extcredits7) else 0 end) AND "
            //        + "	[extcredits8]>= (case when ?extcredits8 >= 0 then abs(?extcredits8) else 0 end) ";






            String CommandText = "SELECT count(1) FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=?uid AND"
                    + "	`extcredits1`>= IF(?extcredits1>=0,abs(?extcredits1),0) AND "
                    + "	`extcredits2`>= IF(?extcredits2>=0,abs(?extcredits2),0) AND "
                    + "	`extcredits3`>= IF(?extcredits3>=0,abs(?extcredits3),0) AND "
                    + "	`extcredits4`>= IF(?extcredits4>=0,abs(?extcredits4),0) AND "
                    + "	`extcredits5`>= IF(?extcredits5>=0,abs(?extcredits5),0) AND "
                    + "	`extcredits6`>= IF(?extcredits6>=0,abs(?extcredits6),0) AND "
                    + "	`extcredits7`>= IF(?extcredits7>=0,abs(?extcredits7),0) AND "
                    + "	`extcredits8`>= IF(?extcredits8>=0,abs(?extcredits8),0)";

            if (Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, CommandText, prams)) == 0)
            {
                return false;
            }
            return true;
        }

        public void UpdateUserCredits(int uid, DataRow values, int pos, int mount)
        {
            DbParameter[] prams = {

									   DbHelper.MakeInParam("?extcredits1", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits1"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits2", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits2"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits3", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits3"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits4", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits4"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits5", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits5"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits6", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits6"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits7", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits7"],0) * pos * mount),
									   DbHelper.MakeInParam("?extcredits8", (DbType)MySqlDbType.Decimal, 8, Utils.StrToFloat(values["extcredits8"],0) * pos * mount),
                                       DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
								   };
            if (pos < 0 && mount < 0)
            {
                for (int i = 1; i < prams.Length; i++)
                {
                    prams[i].Value = -Convert.ToInt32(prams[i].Value);
                }
            }

            string CommandText = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET "
                + "	`extcredits1`=`extcredits1` + ?extcredits1, "
                + "	`extcredits2`=`extcredits2` + ?extcredits2, "
                + "	`extcredits3`=`extcredits3` + ?extcredits3, "
                + "	`extcredits4`=`extcredits4` + ?extcredits4, "
                + "	`extcredits5`=`extcredits5` + ?extcredits5, "
                + "	`extcredits6`=`extcredits6` + ?extcredits6, "
                + "	`extcredits7`=`extcredits7` + ?extcredits7, "
                + "	`extcredits8`=`extcredits8` + ?extcredits8 "
                + "WHERE `uid`=?uid";

            DbHelper.ExecuteNonQuery(CommandType.Text, CommandText, prams);
        }


        public DataTable GetUserGroups()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "usergroups` ORDER BY `groupid`").Tables[0];
        }

        public DataTable GetUserGroupRateRange(int groupid)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT `raterange` FROM `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `groupid`=" + groupid.ToString() + " LIMIT 1").Tables[0];
        }

        public IDataReader GetUserTodayRate(int uid)
        {
            DbParameter[] prams = {
						                DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
								    };
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `extcredits`, SUM(ABS(`score`)) AS `todayrate` FROM `" + BaseConfigs.GetTablePrefix + "ratelog` WHERE DATEDIFF(`postdatetime`,CURDATE()) = 0 AND `uid` = ?uid GROUP BY `extcredits`", prams);
        }


        public string GetSpecialGroupInfoSql()
        {
            return "Select * From `" + BaseConfigs.GetTablePrefix + "usergroups` WHERE `radminid`=-1 And `groupid`>8 Order By `groupid`";
        }



        ///// <summary>
        ///// 更新在线时间
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <returns></returns>
        //public int UpdateOnlineTime(int uid)
        //{
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}users] SET [oltime] = [oltime] + DATEDIFF(n,[lastvisit],GETDATE()) WHERE [uid]={1}", BaseConfigs.GetTablePrefix, uid));
        //}

        /// <summary>
        /// 返回指定用户的信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public IDataReader GetUserInfoToReader(int uid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid),
			};
            string sql = "SELECT `" + BaseConfigs.GetTablePrefix + "users`.*, `" + BaseConfigs.GetTablePrefix + "userfields`.website,`" + BaseConfigs.GetTablePrefix + "userfields`.icq,`" + BaseConfigs.GetTablePrefix + "userfields`.qq,`" + BaseConfigs.GetTablePrefix + "userfields`.yahoo,`" + BaseConfigs.GetTablePrefix + "userfields`.msn,`" + BaseConfigs.GetTablePrefix + "userfields`.skype,`" + BaseConfigs.GetTablePrefix + "userfields`.location,`" + BaseConfigs.GetTablePrefix + "userfields`.customstatus,`" + BaseConfigs.GetTablePrefix + "userfields`.avatar,`" + BaseConfigs.GetTablePrefix + "userfields`.avatarwidth,`" + BaseConfigs.GetTablePrefix + "userfields`.avatarheight,`" + BaseConfigs.GetTablePrefix + "userfields`.medals,`" + BaseConfigs.GetTablePrefix + "userfields`.bio,`" + BaseConfigs.GetTablePrefix + "userfields`.signature,`" + BaseConfigs.GetTablePrefix + "userfields`.sightml,`" + BaseConfigs.GetTablePrefix + "userfields`.authstr,`" + BaseConfigs.GetTablePrefix + "userfields`.authtime,`" + BaseConfigs.GetTablePrefix + "userfields`.authflag,`" + BaseConfigs.GetTablePrefix + "userfields`.realname,`" + BaseConfigs.GetTablePrefix + "userfields`.idcard,`" + BaseConfigs.GetTablePrefix + "userfields`.mobile,`" + BaseConfigs.GetTablePrefix + "userfields`.phone  " +
"FROM " + BaseConfigs.GetTablePrefix + "users LEFT JOIN " + BaseConfigs.GetTablePrefix + "userfields ON `" + BaseConfigs.GetTablePrefix + "users`.`uid`=`" + BaseConfigs.GetTablePrefix + "userfields`.`uid` WHERE `" + BaseConfigs.GetTablePrefix + "users`.`uid`=?uid LIMIT 1";

            // return DbHelper.ExecuteReader(CommandType.Text, string.Format(sql,BaseConfigs.GetTablePrefix), prams);
            return DbHelper.ExecuteReader(CommandType.Text, string.Format(sql, BaseConfigs.GetTablePrefix), prams);
        }

        /// <summary>
        /// 获取简短用户信息
        /// </summary>
        /// <param name="uid">用id</param>
        /// <returns>用户简短信息</returns>
        public IDataReader GetShortUserInfoToReader(int uid)
        {

            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32,4, uid),
			};

            //return DbHelper.ExecuteReader(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "getshortuserinfo", prams);
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=?uid", prams);
        }

        /// <summary>
        /// 根据IP查找用户
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>用户信息</returns>
        public IDataReader GetUserInfoByIP(string ip)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?regip", (DbType)MySqlDbType.String,15, ip),
			};

            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `" + BaseConfigs.GetTablePrefix + "users`.*, `" + BaseConfigs.GetTablePrefix + "userfields`.* FROM `" + BaseConfigs.GetTablePrefix + "users` LEFT JOIN `" + BaseConfigs.GetTablePrefix + "userfields` ON `" + BaseConfigs.GetTablePrefix + "users`.`uid`=`" + BaseConfigs.GetTablePrefix + "userfields`.`uid` WHERE `" + BaseConfigs.GetTablePrefix + "users`.`regip`=?regip ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`uid` DESC LIMIT 1", prams);

        }

        public IDataReader GetUserName(int uid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
			};
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `username` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `" + BaseConfigs.GetTablePrefix + "users`.`uid`=?uid LIMIT 1", prams);
        }

        public IDataReader GetUserJoinDate(int uid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
			};
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `joindate` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `" + BaseConfigs.GetTablePrefix + "users`.`uid`=?uid LIMIT 1", prams);
        }

        public IDataReader GetUserID(string username)
        {
            DbParameter[] prams = {
								   DbHelper.MakeInParam("?username",(DbType)MySqlDbType.VarChar,20,username),
			};
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `" + BaseConfigs.GetTablePrefix + "users`.`username`=?username LIMIT 1", prams);

        }

        public DataTable GetUserList(int pagesize, int currentpage)
        {
            #region 获得用户列表
            return DbHelper.ExecuteDataset("SELECT a.`uid`, a.`username`,a.`nickname`, a.`joindate`, a.`credits`, a.`posts`, a.`lastactivity`, a.`email`,a.`lastvisit`,a.`lastvisit`,a.`accessmasks`, a.`location`,`" + BaseConfigs.GetTablePrefix + "usergroups`.`grouptitle` FROM (SELECT `" + BaseConfigs.GetTablePrefix + "users`.*,`" + BaseConfigs.GetTablePrefix + "userfields`.`location` FROM `" + BaseConfigs.GetTablePrefix + "users` LEFT JOIN `" + BaseConfigs.GetTablePrefix + "userfields` ON `" + BaseConfigs.GetTablePrefix + "userfields`.`uid` = `" + BaseConfigs.GetTablePrefix + "users`.`uid`) AS a LEFT JOIN `" + BaseConfigs.GetTablePrefix + "usergroups` ON `" + BaseConfigs.GetTablePrefix + "usergroups`.`groupid`=a.`groupid` ORDER BY a.`uid` DESC LIMIT " + ((currentpage - 1) * pagesize).ToString() + "," + pagesize.ToString()).Tables[0];
            #endregion
        }


        /// <summary>
        /// 获得用户列表DataTable
        /// </summary>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageindex">当前页数</param>
        /// <returns>用户列表DataTable</returns>
        public DataTable GetUserList(int pagesize, int pageindex, string orderby, string ordertype)
        {
            #region Access,sql语句 getuserlist



            string[] arrayorderby = new string[] { "username", "credits", "posts", "admin", "lastactivity" };
            int i = Array.IndexOf(arrayorderby, orderby);


            switch (i)
            {
                //case "uid":
                //    orderby = "ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`uid` " + ordertype;
                //    break;
                case 0:
                    orderby = "ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`username` " + ordertype + ",`" + BaseConfigs.GetTablePrefix + "users`.`uid` " + ordertype;
                    break;
                case 1:
                    orderby = "ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`credits` " + ordertype + ",`" + BaseConfigs.GetTablePrefix + "users`.`uid` " + ordertype;
                    break;
                case 2:
                    orderby = "ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`posts` " + ordertype + ",`" + BaseConfigs.GetTablePrefix + "users`.`uid` " + ordertype;
                    break;
                case 3:
                    orderby = "WHERE `" + BaseConfigs.GetTablePrefix + "users`.`adminid` > 0 ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`adminid`,`" + BaseConfigs.GetTablePrefix + "users`.`uid` " + ordertype;
                    break;
                //case "joindate":
                //    orderby = "ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`joindate` " + ordertype + ",`" + BaseConfigs.GetTablePrefix + "users`.`uid` " + ordertype;
                //    break;
                case 4:
                    orderby = "ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`lastactivity` " + ordertype + "," + BaseConfigs.GetTablePrefix + "users.uid " + ordertype;
                    break;
                default:
                    orderby = "ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`uid` " + ordertype;
                    break;
            }

            string strSQL = "";
            string tableUsers = string.Concat(BaseConfigs.GetTablePrefix, "users");
            string tableUserFields = string.Concat(BaseConfigs.GetTablePrefix, "userfields");

            strSQL = "SELECT `" + tableUsers + "`.`uid`,`" + tableUsers + "`.`username`,`" + tableUsers + "`.`groupid`,`" + tableUsers + "`.`nickname`, `" + tableUsers + "`.`joindate`, `" + tableUsers + "`.`credits`, `" + tableUsers + "`.`posts`,`" + tableUsers + "`.`lastactivity`, `" + tableUsers + "`.`email`, `" + tableUserFields + "`.`location` " +
                "FROM `" + tableUsers + "` " +
                "LEFT JOIN `" + tableUserFields + "` " +
                "ON `" + tableUserFields + "`.`uid` = `" + tableUsers + "`.`uid` " + orderby + " LIMIT " + ((pageindex - 1) * pagesize).ToString() + "," + pagesize.ToString();

            #endregion
            return DbHelper.ExecuteDataset(CommandType.Text, strSQL).Tables[0];

        }

        /// <summary>
        /// 判断指定用户名是否已存在
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>如果已存在该用户id则返回true, 否则返回false</returns>
        public bool Exists(int uid)
        {
            DbParameter[] prams = {
										 DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
			};
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=?uid", prams)) >= 1;
        }

        /// <summary>
        /// 判断指定用户名是否已存在.
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>如果已存在该用户名则返回true, 否则返回false</returns>
        public bool Exists(string username)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?username",(DbType)MySqlDbType.String,20,username),
			};
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(1) FROM `{0}users` WHERE `username`=?username", BaseConfigs.GetTablePrefix), prams)) >= 1;
        }

        /// <summary>
        /// 是否有指定ip地址的用户注册
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>存在返回true,否则返回false</returns>
        public bool ExistsByIP(string ip)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?regip",(DbType)MySqlDbType.String, 15,ip),
			};
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(1) FROM `{0}users` WHERE `regip`=?regip", BaseConfigs.GetTablePrefix), prams)) >= 1;
        }

        /// <summary>
        /// 检测Email和安全项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="email">email</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public IDataReader CheckEmailAndSecques(string username, string email, string secques)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?username",(DbType)MySqlDbType.String,20,username),
									   DbHelper.MakeInParam("?email",(DbType)MySqlDbType.String,50, email),
									   DbHelper.MakeInParam("?secques",(DbType)MySqlDbType.String,8, secques)
								   };
            String sqlstring = "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `username`=?username AND `email`=?email AND `secques`=?secques LIMIT 1";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, prams);
        }

        /// <summary>
        /// 检测密码和安全项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public IDataReader CheckPasswordAndSecques(string username, string password, bool originalpassword, string secques)
        {

            DbParameter[] prams = {
									   DbHelper.MakeInParam("?username",(DbType)MySqlDbType.String,20,username),
									   DbHelper.MakeInParam("?password",(DbType)MySqlDbType.String,32, originalpassword ? Utils.MD5(password) : password),
									   DbHelper.MakeInParam("?secques",(DbType)MySqlDbType.String,8, secques)
								   };
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `username`=?username AND `password`=?password AND `secques`=?secques LIMIT 1", prams);
        }

        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public IDataReader CheckPassword(string username, string password, bool originalpassword)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?username",(DbType)MySqlDbType.String,20, username),
									   DbHelper.MakeInParam("?password",(DbType)MySqlDbType.String,32, originalpassword ? Utils.MD5(password) : password)
								   };
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `username`=?username AND `password`=?password LIMIT 1", prams);
        }

        /// <summary>
        /// 检测DVBBS兼容模式的密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public IDataReader CheckDvBbsPasswordAndSecques(string username, string password)
        {


            DbParameter[] prams = {
									   DbHelper.MakeInParam("?username",(DbType)MySqlDbType.String,20,username),
									  // DbHelper.MakeInParam("?password",(DbType)MySqlDbType.String,32, Utils.MD5(password).Substring(8, 16))
								   };
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid`, `password`, `secques` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `username`=?username LIMIT 1", prams);
        }

        /// <summary>
        /// 检测密码
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <param name="groupid">用户组id</param>
        /// <param name="adminid">管理id</param>
        /// <returns>如果用户密码正确则返回uid, 否则返回-1</returns>
        public IDataReader CheckPassword(int uid, string password, bool originalpassword)
        {

            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
									   DbHelper.MakeInParam("?password",(DbType)MySqlDbType.String,32, originalpassword ? Utils.MD5(password) : password)
								   };

            String sql = "SELECT `uid`, `groupid`, `adminid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=?uid AND `password`=?password LIMIT 1";

            return DbHelper.ExecuteReader(CommandType.Text, sql, prams);
        }

        /// <summary>
        /// 根据指定的email查找用户并返回用户uid
        /// </summary>
        /// <param name="email">email地址</param>
        /// <returns>用户uid</returns>
        public IDataReader FindUserEmail(string email)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?email",(DbType)MySqlDbType.String,50, email),
								   };
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `email`=?email LIMIT 1", prams);
        }

        /// <summary>
        /// 得到论坛中用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public int GetUserCount()
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(uid) FROM `" + BaseConfigs.GetTablePrefix + "users`"), 0);
        }

        /// <summary>
        /// 得到论坛中用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public int GetUserCountByAdmin()
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(uid) FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `" + BaseConfigs.GetTablePrefix + "users`.`adminid` > 0"), 0);
        }

        /// <summary>
        /// 创建新用户.
        /// </summary>
        /// <param name="__userinfo">用户信息</param>
        /// <returns>返回用户ID, 如果已存在该用户名则返回-1</returns>
        public int CreateUser(UserInfo __userinfo)
        {
            if (Exists(__userinfo.Username))
            {
                return -1;
            }

            DbParameter[] prams = {
										 DbHelper.MakeInParam("?username",(DbType)MySqlDbType.String,20,__userinfo.Username),
										 DbHelper.MakeInParam("?nickname",(DbType)MySqlDbType.String,20,__userinfo.Nickname),
										 DbHelper.MakeInParam("?password",(DbType)MySqlDbType.String,32,__userinfo.Password),
										 DbHelper.MakeInParam("?secques",(DbType)MySqlDbType.String,8,__userinfo.Secques),
										 DbHelper.MakeInParam("?gender",(DbType)MySqlDbType.Int32,4,__userinfo.Gender),
										 DbHelper.MakeInParam("?adminid",(DbType)MySqlDbType.Int32,4,__userinfo.Adminid),
										 DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int16,2,__userinfo.Groupid),
										 DbHelper.MakeInParam("?groupexpiry",(DbType)MySqlDbType.Int32,4,__userinfo.Groupexpiry),
										 DbHelper.MakeInParam("?extgroupids",(DbType)MySqlDbType.String,60,__userinfo.Extgroupids),
										 DbHelper.MakeInParam("?regip",(DbType)MySqlDbType.VarChar,0,__userinfo.Regip),
										 DbHelper.MakeInParam("?joindate",(DbType)MySqlDbType.VarChar,0,__userinfo.Joindate),
										 DbHelper.MakeInParam("?lastip",(DbType)MySqlDbType.String,15,__userinfo.Lastip),
										 DbHelper.MakeInParam("?lastvisit",(DbType)MySqlDbType.VarChar,0,__userinfo.Lastvisit),
										 DbHelper.MakeInParam("?lastactivity",(DbType)MySqlDbType.VarChar,0,__userinfo.Lastactivity),
										 DbHelper.MakeInParam("?lastpost",(DbType)MySqlDbType.VarChar,0,__userinfo.Lastpost),
										 DbHelper.MakeInParam("?lastpostid",(DbType)MySqlDbType.Int32,4,__userinfo.Lastpostid),
										 DbHelper.MakeInParam("?lastposttitle",(DbType)MySqlDbType.VarChar,0,__userinfo.Lastposttitle),
										 DbHelper.MakeInParam("?posts",(DbType)MySqlDbType.Int32,4,__userinfo.Posts),
										 DbHelper.MakeInParam("?digestposts",(DbType)MySqlDbType.Int16,2,__userinfo.Digestposts),
										 DbHelper.MakeInParam("?oltime",(DbType)MySqlDbType.Int16,2,__userinfo.Oltime),
										 DbHelper.MakeInParam("?pageviews",(DbType)MySqlDbType.Int32,4,__userinfo.Pageviews),
										 DbHelper.MakeInParam("?credits",(DbType)MySqlDbType.Int32,4,__userinfo.Credits),
										 DbHelper.MakeInParam("?extcredits1",(DbType)MySqlDbType.Double,8,__userinfo.Extcredits1),
										 DbHelper.MakeInParam("?extcredits2",(DbType)MySqlDbType.Double,8,__userinfo.Extcredits2),
										 DbHelper.MakeInParam("?extcredits3",(DbType)MySqlDbType.Double,8,__userinfo.Extcredits3),
										 DbHelper.MakeInParam("?extcredits4",(DbType)MySqlDbType.Double,8,__userinfo.Extcredits4),
										 DbHelper.MakeInParam("?extcredits5",(DbType)MySqlDbType.Double,8,__userinfo.Extcredits5),
										 DbHelper.MakeInParam("?extcredits6",(DbType)MySqlDbType.Double,8,__userinfo.Extcredits6),
										 DbHelper.MakeInParam("?extcredits7",(DbType)MySqlDbType.Double,8,__userinfo.Extcredits7),
										 DbHelper.MakeInParam("?extcredits8",(DbType)MySqlDbType.Double,8,__userinfo.Extcredits8),
										 DbHelper.MakeInParam("?avatarshowid",(DbType)MySqlDbType.Int32,4,__userinfo.Avatarshowid),
										 DbHelper.MakeInParam("?email",(DbType)MySqlDbType.String,50,__userinfo.Email),
										 DbHelper.MakeInParam("?bday",(DbType)MySqlDbType.VarChar,0,__userinfo.Bday),
										 DbHelper.MakeInParam("?sigstatus",(DbType)MySqlDbType.Int32,4,__userinfo.Sigstatus),
										 DbHelper.MakeInParam("?tpp",(DbType)MySqlDbType.Int32,4,__userinfo.Tpp),
										 DbHelper.MakeInParam("?ppp",(DbType)MySqlDbType.Int32,4,__userinfo.Ppp),
										 DbHelper.MakeInParam("?templateid",(DbType)MySqlDbType.Int16,2,__userinfo.Templateid),
										 DbHelper.MakeInParam("?pmsound",(DbType)MySqlDbType.Int32,4,__userinfo.Pmsound),
										 DbHelper.MakeInParam("?showemail",(DbType)MySqlDbType.Int32,4,__userinfo.Showemail),
										 DbHelper.MakeInParam("?newsletter",(DbType)MySqlDbType.Int32,4,__userinfo.Newsletter),
										 DbHelper.MakeInParam("?invisible",(DbType)MySqlDbType.Int32,4,__userinfo.Invisible),
										 DbHelper.MakeInParam("?newpm",(DbType)MySqlDbType.Int32,4,__userinfo.Newpm),
										 DbHelper.MakeInParam("?accessmasks",(DbType)MySqlDbType.Int32,4,__userinfo.Accessmasks)
									 };
            DbParameter[] prams2 = {
									   //
										DbHelper.MakeInParam("?website",(DbType)MySqlDbType.VarChar,80,__userinfo.Website),
										DbHelper.MakeInParam("?icq",(DbType)MySqlDbType.VarChar,12,__userinfo.Icq),
										DbHelper.MakeInParam("?qq",(DbType)MySqlDbType.VarChar,12,__userinfo.Qq),
										DbHelper.MakeInParam("?yahoo",(DbType)MySqlDbType.VarChar,40,__userinfo.Yahoo),
										DbHelper.MakeInParam("?msn",(DbType)MySqlDbType.VarChar,40,__userinfo.Msn),
										DbHelper.MakeInParam("?skype",(DbType)MySqlDbType.VarChar,40,__userinfo.Skype),
										DbHelper.MakeInParam("?location",(DbType)MySqlDbType.VarChar,30,__userinfo.Location),
										DbHelper.MakeInParam("?customstatus",(DbType)MySqlDbType.VarChar,30,__userinfo.Customstatus),
										DbHelper.MakeInParam("?avatar",(DbType)MySqlDbType.VarChar,255,__userinfo.Avatar),
										DbHelper.MakeInParam("?avatarwidth",(DbType)MySqlDbType.Int32,4,(__userinfo.Avatarwidth == 0)? 60 : __userinfo.Avatarwidth),
										DbHelper.MakeInParam("?avatarheight",(DbType)MySqlDbType.Int32,4,(__userinfo.Avatarheight == 0)? 60 : __userinfo.Avatarheight),
										DbHelper.MakeInParam("?medals",(DbType)MySqlDbType.VarChar,40, __userinfo.Medals),
										DbHelper.MakeInParam("?bio",(DbType)MySqlDbType.VarChar,0,__userinfo.Bio),
										DbHelper.MakeInParam("?signature",(DbType)MySqlDbType.VarChar,0,__userinfo.Signature),
										DbHelper.MakeInParam("?sightml",(DbType)MySqlDbType.VarChar,0,__userinfo.Sightml),
                						DbHelper.MakeInParam("?authstr",(DbType)MySqlDbType.VarChar,20,__userinfo.Authstr),
                                        //DbHelper.MakeInParam("?authtime",(DbType)MySqlDbType.VarChar,0,__userinfo.Authtime),
                						DbHelper.MakeInParam("?realname",(DbType)MySqlDbType.VarChar,10,__userinfo.Realname),
                						DbHelper.MakeInParam("?idcard",(DbType)MySqlDbType.VarChar,20,__userinfo.Idcard),
                						DbHelper.MakeInParam("?mobile",(DbType)MySqlDbType.VarChar,20,__userinfo.Mobile),
                						DbHelper.MakeInParam("?phone",(DbType)MySqlDbType.VarChar,20,__userinfo.Phone)
								         };

            string sqlstring = string.Empty;

            int uid, id;

            MySqlConnection conn = new MySqlConnection(DbHelper.ConnectionString);
            conn.Open();
            using (MySqlTransaction trans = conn.BeginTransaction())
            {
                try
                {

                    DbHelper.ExecuteNonQuery(out id, trans, CommandType.Text, "INSERT INTO `" + BaseConfigs.GetTablePrefix + "users" + "`(`username`,`nickname`, `password`, `secques`, `gender`, `adminid`, `groupid`, `groupexpiry`, `extgroupids`, `regip`, `joindate`, `lastip`, `lastvisit`, `lastactivity`, `lastpost`, `lastpostid`, `lastposttitle`, `posts`, `digestposts`, `oltime`, `pageviews`, `credits`, `extcredits1`, `extcredits2`, `extcredits3`, `extcredits4`, `extcredits5`, `extcredits6`, `extcredits7`, `extcredits8`, `avatarshowid`, `email`, `bday`, `sigstatus`, `tpp`, `ppp`, `templateid`, `pmsound`, `showemail`, `newsletter`, `invisible`, `newpm`, `accessmasks`) " +
                        "VALUES(?username,?nickname, ?password, ?secques, ?gender, ?adminid, ?groupid, ?groupexpiry, ?extgroupids, ?regip, ?joindate, ?lastip, ?lastvisit, ?lastactivity, ?lastpost, ?lastpostid, ?lastposttitle, ?posts, ?digestposts, ?oltime, ?pageviews, ?credits, ?extcredits1, ?extcredits2, ?extcredits3, ?extcredits4, ?extcredits5, ?extcredits6, ?extcredits7, ?extcredits8, ?avatarshowid, ?email, ?bday, ?sigstatus, ?tpp, ?ppp, ?templateid, ?pmsound, ?showemail, ?newsletter, ?invisible, ?newpm, ?accessmasks);SELECT @@session.identity", prams);
                    uid = id;
                    //uid = (int)DbHelper.ExecuteScalar(trans, CommandType.Text, "select uid from `" + BaseConfigs.GetTablePrefix + "users` order by uid desc LIMIT 1");
                    //DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "statistics" + "` SET `totalusers`=`totalusers` + 1,`lastusername`='" + __userinfo.Username + "',`lastuserid`=" + uid);


                    sqlstring = "INSERT INTO `" + BaseConfigs.GetTablePrefix + "userfields` (`uid`,`website`,`icq`,`qq`,`yahoo`,`msn`,`skype`,`location`,`customstatus`,`avatar`,`avatarwidth`,`avatarheight`,`medals`,`bio`,`signature`,`sightml`,`authstr`,`authtime`,`realname`,`idcard`,`mobile`,`phone`) VALUES (" + uid +
                        ",?website,?icq,?qq,?yahoo,?msn,?skype,?location,?customstatus,?avatar,?avatarwidth,?avatarheight,?medals,?bio,?signature,?sightml,?authstr,NOW(),?realname,?idcard,?mobile,?phone)";



                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, sqlstring, prams2);

                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "statistics" + "` SET `totalusers`=`totalusers` + 1,`lastusername`=?username,`lastuserid`=" + uid, prams);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }

                finally
                {
                    conn.Close();

                }


            }



            return Utils.StrToInt(uid, -1);
        }

        /// <summary>
        /// 更新权限验证字符串
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="authstr">验证串</param>
        /// <param name="authflag">验证标志</param>
        public void UpdateAuthStr(int uid, string authstr, int authflag)
        {

            DbParameter[] prams = {

									   DbHelper.MakeInParam("?authstr", (DbType)MySqlDbType.String, 20, authstr),
									   DbHelper.MakeInParam("?authflag", (DbType)MySqlDbType.Int32, 4, authflag),
                                       DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
								   };


            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "userfields" + "` SET `authstr`=?authstr, `authtime` = now(), `authflag`=?authflag WHERE `uid`=?uid";


            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        /// <summary>
        /// 更新指定用户的个人资料
        /// </summary>
        /// <param name="__userinfo">用户信息</param>
        /// <returns>如果用户不存在则为false, 否则为true</returns>
        public void UpdateUserProfile(UserInfo __userinfo)
        {
            DbParameter[] prams1 = {
										  DbHelper.MakeInParam("?nickname",(DbType)MySqlDbType.String,20,__userinfo.Nickname),
										  DbHelper.MakeInParam("?gender", (DbType)MySqlDbType.Int32, 4, __userinfo.Gender),
										  DbHelper.MakeInParam("?email", (DbType)MySqlDbType.String, 50, __userinfo.Email),
										  DbHelper.MakeInParam("?bday", (DbType)MySqlDbType.String, 10, __userinfo.Bday),
										  DbHelper.MakeInParam("?showemail", (DbType)MySqlDbType.Int32, 4, __userinfo.Showemail),
										  DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, __userinfo.Uid)

									  };
            DbParameter[] prams2 = {
										DbHelper.MakeInParam("?website", (DbType)MySqlDbType.VarChar, 80, __userinfo.Website),
										  DbHelper.MakeInParam("?icq",(DbType) MySqlDbType.VarChar, 12, __userinfo.Icq),
										  DbHelper.MakeInParam("?qq",(DbType)MySqlDbType.VarChar, 12, __userinfo.Qq),
										  DbHelper.MakeInParam("?yahoo", (DbType)MySqlDbType.VarChar, 40, __userinfo.Yahoo),
										  DbHelper.MakeInParam("?msn", (DbType)MySqlDbType.VarChar, 40, __userinfo.Msn),
										  DbHelper.MakeInParam("?skype", (DbType)MySqlDbType.VarChar, 40, __userinfo.Skype),
										  DbHelper.MakeInParam("?location", (DbType)MySqlDbType.VarChar, 30, __userinfo.Location),
										  DbHelper.MakeInParam("?bio", (DbType)MySqlDbType.VarChar, 0, __userinfo.Bio),
										//  DbHelper.MakeInParam("?signature", (DbType)MySqlDbType.VarChar, 0, __userinfo.Signature),
                DbHelper.MakeInParam("?realname",(DbType)MySqlDbType.VarChar,10,__userinfo.Realname),

                                       DbHelper.MakeInParam("?idcard",(DbType)MySqlDbType.VarChar,20,__userinfo.Idcard),
                                       DbHelper.MakeInParam("?mobile",(DbType)MySqlDbType.VarChar,20,__userinfo.Mobile),
                                       DbHelper.MakeInParam("?phone",(DbType)MySqlDbType.VarChar,20,__userinfo.Phone),
										  DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, __userinfo.Uid)
									  };


            MySqlConnection conn = new MySqlConnection(BaseConfigs.GetDBConnectString);
            conn.Open();
            using (MySqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users" + "` SET  `nickname`=?nickname, `gender`=?gender , `email`=?email , `bday`=?bday, `showemail`=?showemail WHERE `uid`=?uid", prams1);
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "userfields" + "` SET  `website`=?website , `icq`=?icq , `qq`=?qq , `yahoo`=?yahoo , `msn`=?msn , `skype`=?skype , `location`=?location , `bio`=?bio,`idcard`=?idcard,`mobile`=?mobile,`phone`=?phone,`realname`=?realname WHERE  `uid`=?uid", prams2);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 更新用户论坛设置
        /// </summary>
        /// <param name="__userinfo">用户信息</param>
        /// <returns>如果用户不存在则返回false, 否则返回true</returns>
        public void UpdateUserForumSetting(UserInfo __userinfo)
        {
            DbParameter[] prams1 = {

										 DbHelper.MakeInParam("?tpp",(DbType)MySqlDbType.Int32,4,__userinfo.Tpp),
										 DbHelper.MakeInParam("?ppp",(DbType)MySqlDbType.Int32,4,__userinfo.Ppp),
                                         //DbHelper.MakeInParam("?templateid",(DbType)MySqlDbType.Int16,2,__userinfo.Templateid),
                                         //DbHelper.MakeInParam("?pmsound",(DbType)MySqlDbType.Int32,4,__userinfo.Pmsound),
                                         //DbHelper.MakeInParam("?newsletter",(DbType)MySqlDbType.Int32,4,__userinfo.Newsletter),
										 DbHelper.MakeInParam("?invisible",(DbType)MySqlDbType.Int32,4,__userinfo.Invisible),
										 DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,__userinfo.Uid)
									 };
            DbParameter[] prams2 = {

										  
                DbHelper.MakeInParam("?signature", (DbType)MySqlDbType.VarChar, 500, __userinfo.Signature),
                
                DbHelper.MakeInParam("?sightml", (DbType)MySqlDbType.VarChar, 1000, __userinfo.Sightml),
                DbHelper.MakeInParam("?customstatus",(DbType)MySqlDbType.VarChar,30,__userinfo.Customstatus),
                 DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,__userinfo.Uid)
									  };

            //  MySqlConnection conn = new MySqlConnection(BaseConfigs.GetDBConnectString);
            // conn.Open();
            //using (MySqlTransaction trans = conn.BeginTransaction())
            //{
            //  try
            //{
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users" + "` SET  `tpp`=?tpp, `ppp`=?ppp,`invisible`=?invisible WHERE `uid`=?uid", prams1);
            //DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users" + "` SET  `tpp`=?tpp, `ppp`=?ppp, `templateid`=?templateid, `pmsound`=?pmsound, `newsletter`=?newsletter, `invisible`=?invisible WHERE `uid`=?uid", prams1);
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "userfields" + "` SET  `signature`=?signature,`sightml` = ?sightml,`customstatus`=?customstatus WHERE  `uid`=?uid", prams2);
        }

        /// <summary>
        /// 修改用户自定义积分字段的值
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="extid">扩展字段序号(1-8)</param>
        /// <param name="pos">增加的数值(可以是负数)</param>
        /// <returns>执行是否成功</returns>
        public void UpdateUserExtCredits(int uid, int extid, float pos)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `extcredits" + extid.ToString() + "`=`extcredits" + extid.ToString() + "` + (" + pos.ToString() + ") WHERE `uid`=" + uid.ToString());
        }

        /// <summary>
        /// 获得指定用户的指定积分扩展字段的值
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="extid">扩展字段序号(1-8)</param>
        /// <returns>值</returns>
        public float GetUserExtCredits(int uid, int extid)
        {
            return Utils.StrToFloat(DbHelper.ExecuteDataset(CommandType.Text, "SELECT `extcredits" + extid.ToString() + "` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=" + uid.ToString()).Tables[0].Rows[0][0], 0);
        }

        ///// <summary>
        ///// 更新用户签名
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="signature">签名</param>
        ///// <returns>如果用户不存在则返回false, 否则返回true</returns>
        //public void UpdateUserSignature(int uid, int sigstatus, string signature, string sightml)
        //{
        //    DbParameter[] prams = {
        //                               DbHelper.MakeInParam("@uid",(DbType)MySqlDbType.Int32,4,uid),
        //                               DbHelper.MakeInParam("@sigstatus",(DbType)MySqlDbType.Int32,4,sigstatus),
        //                               DbHelper.MakeInParam("@signature",(DbType)MySqlDbType.VarChar,500,signature),
        //                               DbHelper.MakeInParam("@sightml",(DbType)MySqlDbType.VarChar,1000,sightml)
        //                           };
        //    DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "updateusersignature", prams);
        //}

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="avatar">头像</param>
        /// <param name="avatarwidth">头像宽度</param>
        /// <param name="avatarheight">头像高度</param>
        /// <returns>如果用户不存在则返回false, 否则返回true</returns>
        public void UpdateUserPreference(int uid, string avatar, int avatarwidth, int avatarheight, int templateid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,uid),
									   DbHelper.MakeInParam("?avatar",(DbType)MySqlDbType.VarChar,255,avatar),
									   DbHelper.MakeInParam("?avatarwidth",(DbType)MySqlDbType.Int32,4,avatarwidth),
									   DbHelper.MakeInParam("?avatarheight",(DbType)MySqlDbType.Int32,4,avatarheight),
                                       DbHelper.MakeInParam("?templateid", (DbType)MySqlDbType.Int32, 4, templateid)
								   };
            MySqlConnection Msc = new MySqlConnection(DbHelper.ConnectionString);
            Msc.Open();
            using (MySqlTransaction trans = Msc.BeginTransaction())
            {
                try
                {
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "userfields` SET `avatar`=?avatar, `avatarwidth`=?avatarwidth, `avatarheight`=?avatarheight WHERE `uid`=?uid", prams);
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `templateid`=?templateid WHERE `uid`=?uid", prams);
                    trans.Commit();
                }


                catch
                {
                    trans.Rollback();
                }
                finally
                {

                    Msc.Close();
                }

            }
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <returns>成功返回true否则false</returns>
        public void UpdateUserPassword(int uid, string password, bool originalpassword)
        {
            DbParameter[] prams = {
                                       DbHelper.MakeInParam("?password", (DbType)MySqlDbType.String, 100, originalpassword ? Utils.MD5(password) : password),
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)

								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE " + BaseConfigs.GetTablePrefix + "users  SET  `password`=?password  WHERE  `uid`=?uid", prams);
        }

        /// <summary>
        /// 更新用户安全问题
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>成功返回true否则false</returns>
        public void UpdateUserSecques(int uid, string secques)
        {
            DbParameter[] prams = {

									   DbHelper.MakeInParam("?secques", (DbType)MySqlDbType.String, 8, secques),
                                       DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
								   };

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `secques`=?secques WHERE `uid`=?uid", prams);
        }

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="uid">用户id</param>
        public void UpdateUserLastvisit(int uid, string ip)
        {
            DbParameter[] prams = {

									   DbHelper.MakeInParam("?ip", (DbType)MySqlDbType.VarChar,15, ip),
            DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `lastvisit`=NOW(), `lastip`=?ip WHERE `uid` =?uid", prams);
        }

        public void UpdateUserOnlineStateAndLastActivity(string uidlist, int onlinestate, string activitytime)
        {
            DbParameter[] prams = {
                                        DbHelper.MakeInParam("?onlinestate", (DbType)MySqlDbType.Int32, 4, onlinestate),
									    DbHelper.MakeInParam("?activitytime", (DbType)MySqlDbType.VarChar, 25, activitytime)
								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `onlinestate`=?onlinestate,`lastactivity` = ?activitytime WHERE `uid` IN (" + uidlist + ")", prams);
        }

        public void UpdateUserOnlineStateAndLastActivity(int uid, int onlinestate, string activitytime)
        {
            DbParameter[] prams = {

                                        DbHelper.MakeInParam("?onlinestate", (DbType)MySqlDbType.Int32, 4, onlinestate),
									    DbHelper.MakeInParam("?activitytime", (DbType)MySqlDbType.VarChar, 25, activitytime),
                                           DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `onlinestate`=?onlinestate,`lastactivity` = ?activitytime WHERE `uid`=?uid", prams);
        }

        public void UpdateUserOnlineStateAndLastVisit(string uidlist, int onlinestate, string activitytime)
        {
            DbParameter[] prams = {
                                        DbHelper.MakeInParam("?onlinestate", (DbType)MySqlDbType.Int32, 4, onlinestate),
									    DbHelper.MakeInParam("?activitytime", (DbType)MySqlDbType.VarChar, 25, activitytime)
								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `onlinestate`=?onlinestate,`lastvisit` = ?activitytime WHERE `uid` IN (" + uidlist + ")", prams);
        }

        public void UpdateUserOnlineStateAndLastVisit(int uid, int onlinestate, string activitytime)
        {
            DbParameter[] prams = {
                                        DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid),
                                        DbHelper.MakeInParam("?onlinestate", (DbType)MySqlDbType.Int32, 4, onlinestate),
									    DbHelper.MakeInParam("?activitytime", (DbType)MySqlDbType.Datetime, 8, DateTime.Parse(activitytime))
								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `onlinestate`=?onlinestate,`lastvisit` = ?activitytime WHERE `uid`=?uid", prams);
        }

        /// <summary>
        /// 更新用户当前的在线时间和最后活动时间
        /// </summary>
        /// <param name="uid">用户uid</param>
        public void UpdateUserLastActivity(int uid, string activitytime)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid),
									   DbHelper.MakeInParam("?activitytime", (DbType)MySqlDbType.Datetime, 8, DateTime.Parse(activitytime))
								   };


            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `lastactivity` = ?activitytime  WHERE `uid` = ?uid", prams);

        }

        /// <summary>
        /// 设置用户信息表中未读短消息的数量
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="pmnum">短消息数量</param>
        /// <returns>更新记录个数</returns>
        public int SetUserNewPMCount(int uid, int pmnum)
        {
            DbParameter[] prams = {
                                       DbHelper.MakeInParam("?value", (DbType)MySqlDbType.Int32, 4, pmnum),
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)

			};
            return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `newpmcount`=?value WHERE `uid`=?uid", prams);
        }

        /// <summary>
        /// 更新指定用户的勋章信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="medals">勋章信息</param>
        public void UpdateMedals(int uid, string medals)
        {
            DbParameter[] prams = {
                DbHelper.MakeInParam("?medals", (DbType)MySqlDbType.VarChar, 300, medals),
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)

								   };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "userfields` SET `medals`=?medals WHERE `uid`=?uid", prams);

        }

        public int DecreaseNewPMCount(int uid, int subval)
        {
            DbParameter[] prams = {

									   DbHelper.MakeInParam("?subval", (DbType)MySqlDbType.Int32, 4, subval),
            DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid)
			};

            try
            {
                //    return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET [newpmcount]=CASE WHEN [newpmcount] >= 0 THEN [newpmcount]-?subval ELSE 0 END WHERE [uid]=?uid", prams);
                //  return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET [newpmcount]=IIF([newpmcount] >= 0,[newpmcount]-?subval,0) WHERE [uid]=?uid", prams);
                return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `newpmcount`=IF(`newpmcount` >= 0,`newpmcount`-?subval,0) WHERE `uid`=?uid", prams);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 得到用户新短消息数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>新短消息数</returns>
        public int GetUserNewPMCount(int uid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid),
			};
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT `newpmcount` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=?uid", prams), 0);
        }

        /// <summary>
        /// 更新用户精华数
        /// </summary>
        /// <param name="useridlist">uid列表</param>
        /// <returns></returns>
        public int UpdateUserDigest(string useridlist)
        {
            string count = "SELECT COUNT(`tid`) AS `digest` FROM `" + BaseConfigs.GetTablePrefix + "topics` WHERE `" + BaseConfigs.GetTablePrefix + "topics`.`posterid` in (" + useridlist + ") AND `digest`>0";
            int i = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, count));
            string sql = "update `" + BaseConfigs.GetTablePrefix + "users` SET `digestposts` = " + i + " where `uid` in (" + useridlist + ")";


            return DbHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 更新用户SpaceID
        /// </summary>
        /// <param name="spaceid">要更新的SpaceId</param>
        /// <param name="userid">要更新的UserId</param>
        /// <returns></returns>
        public void UpdateUserSpaceId(int spaceid, int userid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("?spaceid",(DbType)MySqlDbType.Int32,4,spaceid),
									   DbHelper.MakeInParam("?uid",(DbType)MySqlDbType.Int32,4,userid)
								   };
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `spaceid`=?spaceid WHERE `uid`=?uid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public DataTable GetUserIdByAuthStr(string authstr)
        {
            DbParameter[] prams = {
										  DbHelper.MakeInParam("?authstr",(DbType)MySqlDbType.VarChar,20,authstr)
				};

            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, "SELECT `uid` FROM `" + BaseConfigs.GetTablePrefix + "userfields` WHERE DateDiff(`authtime`,CURDATE())<=3  AND `authstr`=?authstr", prams).Tables[0];

            return dt;
        }

        /// <summary>
        /// 执行在线用户向表及缓存中添加的操作。
        /// </summary>
        /// <param name="onlineuserinfo">在组用户信息内容</param>
        /// <returns>添加成功则返回刚刚添加的olid,失败则返回0</returns>
        public int AddOnlineUser(OnlineUserInfo __onlineuserinfo, int timeout)
        {

            string strDelTimeOutSql = "";
            // 如果timeout为负数则代表不需要精确更新用户是否在线的状态
            if (timeout > 0)
            {
                if (__onlineuserinfo.Userid > 0)
                {
                    strDelTimeOutSql = string.Format("{0}UPDATE `{1}users` SET `onlinestate`=1 WHERE `uid`={2};", strDelTimeOutSql, BaseConfigs.GetTablePrefix, __onlineuserinfo.Userid.ToString());
                    DbHelper.ExecuteNonQuery(CommandType.Text, strDelTimeOutSql);
                }
            }
            else
            {
                timeout = timeout * -1;
            }

            if (timeout > 9999)
            {
                timeout = 9999;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();

            //IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT `userid` FROM `{0}online` WHERE `tickcount`<{1}", BaseConfigs.GetTablePrefix, Utils.SafeInt32(System.Environment.TickCount - timeout * 60000).ToString()));
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT `userid` FROM `{0}online` WHERE `lastupdatetime`<'{1}'", BaseConfigs.GetTablePrefix, DateTime.Now.AddMinutes(timeout * -1).ToString("yyyy-MM-dd HH:mm:ss")));
            try
            {
                while (dr.Read())
                {
                    sb.Append(",");
                    sb.Append(dr[0].ToString());
                    if (dr[0].ToString() != "-1")
                    {
                        sb2.Append(",");
                        sb2.Append(dr[0].ToString());
                    }
                }
            }
            finally
            {
                dr.Close();
            }

            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
                strDelTimeOutSql = string.Format("{0}DELETE FROM `{1}online` WHERE `userid` IN ({2});", strDelTimeOutSql, BaseConfigs.GetTablePrefix, sb.ToString());
                DbHelper.ExecuteNonQuery(CommandType.Text, strDelTimeOutSql);
            }
            if (sb2.Length > 0)
            {
                sb2.Remove(0, 1);
                strDelTimeOutSql = string.Format("{0}UPDATE `{1}users` SET `onlinestate`=0,`lastactivity`=NOW() WHERE `uid` IN ({2});", strDelTimeOutSql, BaseConfigs.GetTablePrefix, sb2.ToString());
                DbHelper.ExecuteNonQuery(CommandType.Text, strDelTimeOutSql);
            }




            DbParameter[] prams = {
									   DbHelper.MakeInParam("?userid",(DbType)MySqlDbType.Int32,4,__onlineuserinfo.Userid),
									   DbHelper.MakeInParam("?ip",(DbType)MySqlDbType.VarChar,15,__onlineuserinfo.Ip),
									   DbHelper.MakeInParam("?username",(DbType)MySqlDbType.VarChar,40,__onlineuserinfo.Username),
                                       //DbHelper.MakeInParam("?tickcount",(DbType)MySqlDbType.Int32,4,System.Environment.TickCount),
									   DbHelper.MakeInParam("?nickname",(DbType)MySqlDbType.VarChar,40,__onlineuserinfo.Nickname),
									   DbHelper.MakeInParam("?password",(DbType)MySqlDbType.String,32,__onlineuserinfo.Password),
									   DbHelper.MakeInParam("?groupid",(DbType)MySqlDbType.Int16,2,__onlineuserinfo.Groupid),
									   DbHelper.MakeInParam("?olimg",(DbType)MySqlDbType.VarChar,80,__onlineuserinfo.Olimg),
									   DbHelper.MakeInParam("?adminid",(DbType)MySqlDbType.Int16,2,__onlineuserinfo.Adminid),
									   DbHelper.MakeInParam("?invisible",(DbType)MySqlDbType.Int16,2,__onlineuserinfo.Invisible),
									   DbHelper.MakeInParam("?action",(DbType)MySqlDbType.Int16,2,__onlineuserinfo.Action),
									   DbHelper.MakeInParam("?lastactivity",(DbType)MySqlDbType.Int16,2,__onlineuserinfo.Lastactivity),
									   DbHelper.MakeInParam("?lastposttime",(DbType)MySqlDbType.Datetime,8,DateTime.Parse(__onlineuserinfo.Lastposttime)),
									   DbHelper.MakeInParam("?lastpostpmtime",(DbType)MySqlDbType.Datetime,8,DateTime.Parse(__onlineuserinfo.Lastpostpmtime)),
									   DbHelper.MakeInParam("?lastsearchtime",(DbType)MySqlDbType.Datetime,8,DateTime.Parse(__onlineuserinfo.Lastsearchtime)),
									   DbHelper.MakeInParam("?lastupdatetime",(DbType)MySqlDbType.Datetime,8,DateTime.Parse(__onlineuserinfo.Lastupdatetime)),
									   DbHelper.MakeInParam("?forumid",(DbType)MySqlDbType.Int32,4,__onlineuserinfo.Forumid),
									   DbHelper.MakeInParam("?forumname",(DbType)MySqlDbType.VarChar,50,""),
									   DbHelper.MakeInParam("?titleid",(DbType)MySqlDbType.Int32,4,__onlineuserinfo.Titleid),
									   DbHelper.MakeInParam("?title",(DbType)MySqlDbType.VarChar,80,""),
									   DbHelper.MakeInParam("?verifycode",(DbType)MySqlDbType.VarChar,10,__onlineuserinfo.Verifycode)
								   };
            //MySqlConnection cn = new MySqlConnection(BaseConfigs.GetDBConnectString);

            // DbHelper.ExecuteNonQuery(CommandType.Text, strDelTimeOutSql + "INSERT INTO `" + BaseConfigs.GetTablePrefix + "online`(`userid`,`ip`,`username`,`tickcount`,`nickname`,`password`,`groupid`,`olimg`,`adminid`,`invisible`,`action`,`lastactivity`,`lastposttime`,`lastpostpmtime`,`lastsearchtime`,`lastupdatetime`,`forumid`,`forumname`,`titleid`,`title`, `verifycode`)VALUES(?userid,?ip,?username,?tickcount,?nickname,?password,?groupid,?olimg,?adminid,?invisible,?action,?lastactivity,?lastposttime,?lastpostpmtime,?lastsearchtime,?lastupdatetime,?forumid,?forumname,?titleid,?title,?verifycode)", prams);
            int olid, id;
            DbHelper.ExecuteNonQuery(out id, CommandType.Text, "INSERT INTO `" + BaseConfigs.GetTablePrefix + "online`(`userid`,`ip`,`username`,`nickname`,`password`,`groupid`,`olimg`,`adminid`,`invisible`,`action`,`lastactivity`,`lastposttime`,`lastpostpmtime`,`lastsearchtime`,`lastupdatetime`,`forumid`,`forumname`,`titleid`,`title`, `verifycode`) VALUES(?userid,?ip,?username,?nickname,?password,?groupid,?olimg,?adminid,?invisible,?action,?lastactivity,?lastposttime,?lastpostpmtime,?lastsearchtime,?lastupdatetime,?forumid,?forumname,?titleid,?title,?verifycode)", prams);
            olid = id;
            //int olid = Int32.Parse(DbHelper.ExecuteDataset(CommandType.Text, "select  `olid` from `" + BaseConfigs.GetTablePrefix + "online` order by `olid` desc LIMIT 1").Tables[0].Rows[0][0].ToString());

            // 如果id值太大则重建在线表
            if (olid > 2147483000)
            {
                CreateOnlineTable();
                DbHelper.ExecuteNonQuery(CommandType.Text, "INSERT INTO `" + BaseConfigs.GetTablePrefix + "online`(`userid`,`ip`,`username`,`nickname`,`password`,`groupid`,`olimg`,`adminid`,`invisible`,`action`,`lastactivity`,`lastposttime`,`lastpostpmtime`,`lastsearchtime`,`lastupdatetime`,`forumid`,`titleid`,`verifycode`) VALUES(?userid,?ip,?username,?nickname,?password,?groupid,?olimg,?adminid,?invisible,?action,?lastactivity,?lastposttime,?lastpostpmtime,?lastsearchtime,?lastupdatetime,?forumid,?forumname,?titleid,?title,?verifycode)", prams);
                return 1;
            }


            return 0;

        }

        private void DeleteExpiredOnlineUsers(int timeout)
        {
            System.Text.StringBuilder timeoutStrBuilder = new System.Text.StringBuilder();
            System.Text.StringBuilder memberStrBuilder = new System.Text.StringBuilder();

            string strDelTimeOutSql = "";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT `olid`, `userid` FROM `{0}online` WHERE `lastupdatetime`<'{1}'", BaseConfigs.GetTablePrefix, DateTime.Parse(DateTime.Now.AddMinutes(timeout * -1).ToString("yyyy-MM-dd HH:mm:ss"))));
            while (dr.Read())
            {
                timeoutStrBuilder.Append(",");
                timeoutStrBuilder.Append(dr["olid"].ToString());
                if (dr["userid"].ToString() != "-1")
                {
                    memberStrBuilder.Append(",");
                    memberStrBuilder.Append(dr["userid"].ToString());
                }
            }
            dr.Close();

            if (timeoutStrBuilder.Length > 0)
            {
                timeoutStrBuilder.Remove(0, 1);
                strDelTimeOutSql = string.Format("DELETE FROM `{0}online` WHERE `olid` IN ({1});", BaseConfigs.GetTablePrefix, timeoutStrBuilder.ToString());
            }
            if (memberStrBuilder.Length > 0)
            {
                memberStrBuilder.Remove(0, 1);
                strDelTimeOutSql = string.Format("{0}UPDATE `{1}users` SET `onlinestate`=0,`lastactivity`=GETDATE() WHERE `uid` IN ({2});", strDelTimeOutSql, BaseConfigs.GetTablePrefix, memberStrBuilder.ToString());
            }
            if (strDelTimeOutSql != string.Empty)
                DbHelper.ExecuteNonQuery(strDelTimeOutSql);
        }

        public DataTable GetUserInfo(int userid)
        {
            DbParameter parm = DbHelper.MakeInParam("@uid", (DbType)MySqlDbType.Int32, 4, userid);
            string sql = "SELECT TOP 1 * FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `uid`=@uid";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
        }

        public DataTable GetUserInfo(string username, string password)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("?username", (DbType)MySqlDbType.String, 20, username),
                                        DbHelper.MakeInParam("?password", (DbType)MySqlDbType.VarChar, 32, password)
                                    };
            string sql = "select * from `" + BaseConfigs.GetTablePrefix + "users`  where `username`=?username And `password`=?password LIMIT 1";

            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
        }

        public void UpdateUserSpaceId(int userid)
        {
            DbParameter parm = DbHelper.MakeInParam("?userid", (DbType)MySqlDbType.Int32, 4, userid);
            string sql = "UPDATE `" + BaseConfigs.GetTablePrefix + "users` SET `spaceid`=ABS(`spaceid`) WHERE `uid`=?userid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parm);
        }

        public int GetUserIdByRewriteName(string rewritename)
        {
            DbParameter parm = DbHelper.MakeInParam("?rewritename", (DbType)MySqlDbType.String, 100, rewritename);
            string sql = string.Format("SELECT `userid` FROM `{0}spaceconfigs` WHERE `rewritename`=?rewritename", BaseConfigs.GetTablePrefix);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parm), -1);
        }

        public void UpdateUserPMSetting(UserInfo user)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, user.Uid),
                                    DbHelper.MakeInParam("?pmsound", (DbType)MySqlDbType.Int32, 4, user.Pmsound),
                                    DbHelper.MakeInParam("?newsletter", (DbType)MySqlDbType.Int32, 4, (int)user.Newsletter)
                                };
            string sql = string.Format(@"UPDATE `{0}users` SET `pmsound`=?pmsound, `newsletter`=?newsletter WHERE `uid`=?uid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public void ClearUserSpace(int uid)
        {
            DbParameter parm = DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid);
            string sql = string.Format("UPDATE `{0}users` SET `spaceid`=0 WHERE `uid`=?uid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parm);
        }


        public IDataReader GetUserInfoByName(string username)
        {
            //DbParameter parm =DbHelper.MakeInParam("@username", (DbType)MySqlDbType.VarChar, 20, username);
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT `uid`, `username` FROM `" + BaseConfigs.GetTablePrefix + "users` WHERE `username` like '%" + RegEsc(username) + "%'");
        }


        public DataTable UserList(int pagesize, int currentpage, string condition)
        {
            int pagetop = (currentpage - 1) * pagesize;

            //if (currentpage == 1)
            //{
            return DbHelper.ExecuteDataset("SELECT `" + BaseConfigs.GetTablePrefix + "users`.`uid`, `" + BaseConfigs.GetTablePrefix + "users`.`username`,`" + BaseConfigs.GetTablePrefix + "users`.`nickname`, `" + BaseConfigs.GetTablePrefix + "users`.`joindate`, `" + BaseConfigs.GetTablePrefix + "users`.`credits`, `" + BaseConfigs.GetTablePrefix + "users`.`posts`, `" + BaseConfigs.GetTablePrefix + "users`.`lastactivity`, `" + BaseConfigs.GetTablePrefix + "users`.`email`,`" + BaseConfigs.GetTablePrefix + "users`.`lastvisit`,`" + BaseConfigs.GetTablePrefix + "users`.`lastvisit`,`" + BaseConfigs.GetTablePrefix + "users`.`accessmasks`, `" + BaseConfigs.GetTablePrefix + "userfields`.`location`,`" + BaseConfigs.GetTablePrefix + "usergroups`.`grouptitle` FROM `" + BaseConfigs.GetTablePrefix + "users` LEFT JOIN `" + BaseConfigs.GetTablePrefix + "userfields` ON `" + BaseConfigs.GetTablePrefix + "userfields`.`uid` = `" + BaseConfigs.GetTablePrefix + "users`.`uid`  LEFT JOIN `" + BaseConfigs.GetTablePrefix + "usergroups` ON `" + BaseConfigs.GetTablePrefix + "usergroups`.`groupid`=`" + BaseConfigs.GetTablePrefix + "users`.`groupid` WHERE " + condition + " ORDER BY `" + BaseConfigs.GetTablePrefix + "users`.`uid` DESC LIMIT " + pagetop + "," + pagesize.ToString() + "").Tables[0];
        }
        public void LessenTotalUsers()
        {

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE `" + BaseConfigs.GetTablePrefix + "statistics` SET `totalusers`=`totalusers`-1");
        }

        public void UpdateOnlineTime(int oltimespan, int uid)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid),
                                    DbHelper.MakeInParam("?oltimespan", (DbType)MySqlDbType.Int32, 2, oltimespan),
                                    DbHelper.MakeInParam("?lastupdate", (DbType)MySqlDbType.Datetime, 8, DateTime.Now),
                                    DbHelper.MakeInParam("?expectedlastupdate", (DbType)MySqlDbType.Datetime, 8, DateTime.Now.AddMinutes(0 - oltimespan))
                                };
            string sql = string.Format("UPDATE `{0}onlinetime` SET `thismonth`=`thismonth`+?oltimespan, `total`=`total`+?oltimespan, `lastupdate`=?lastupdate WHERE `uid`=?uid AND `lastupdate`<=?expectedlastupdate", BaseConfigs.GetTablePrefix);
            if (DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms) < 1)
            {
                try
                {
                    DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("INSERT INTO `{0}onlinetime`(`uid`, `thismonth`, `total`, `lastupdate`) VALUES(?uid, ?oltimespan, ?oltimespan, ?lastupdate)", BaseConfigs.GetTablePrefix), parms);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 重置每月在线时间(清零)
        /// </summary>
        public void ResetThismonthOnlineTime()
        {
            DbHelper.ExecuteNonQuery(string.Format("UPDATE `{0}onlinetime` SET `thismonth`=0", BaseConfigs.GetTablePrefix));
        }

        public void SynchronizeOltime(int uid)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("?uid", (DbType)MySqlDbType.Int32, 4, uid),
                                };
            string sql = string.Format("SELECT `total` FROM `{0}onlinetime` WHERE `uid`=?uid", BaseConfigs.GetTablePrefix);
            int total = Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parms), 0);
            
            if (DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE `{0}users` SET `oltime`={1} WHERE `oltime`<{1} AND `uid`=?uid", BaseConfigs.GetTablePrefix, total), parms) < 1)
            {

                try
                {
                    sql = string.Format("UPDATE `{0}onlinetime` SET `total`=(SELECT `oltime` FROM `{0}users` WHERE `uid`=?uid) WHERE `uid`=?uid", BaseConfigs.GetTablePrefix);
                    DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
                }
                catch
                {
                }
            }
        }

        public IDataReader GetUserByOnlineTime(string field)
        {
            string commandText = string.Format("SELECT  `o`.`uid`, `u`.`username`, `o`.`{0}` FROM `{1}onlinetime` `o` LEFT JOIN `{1}users` `u` ON `o`.`uid`=`u`.`uid` ORDER BY `o`.`{0}` DESC limit 20", field, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }
    }
}
#endif