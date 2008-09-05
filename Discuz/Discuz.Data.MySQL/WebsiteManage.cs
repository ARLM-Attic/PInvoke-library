#if NET1
#else
using System;
using System.Data;
using System.Data.Common;
using Discuz.Config;
using MySql.Data.MySqlClient;

namespace Discuz.Data.MySql
{
    public partial class DataProvider : IDataProvider
    {
        private DbParameter[] GetParms(string startdate, string enddate)
        {
            DbParameter[] parms = new DbParameter[2];
            if (startdate != "")
            {
                parms[0] = DbHelper.MakeInParam("?startdate", (DbType)MySqlDbType.Datetime, 8, DateTime.Parse(startdate));
            }
            if (enddate != "")
            {
                parms[1] = DbHelper.MakeInParam("?enddate", (DbType)MySqlDbType.Datetime, 8, DateTime.Parse(enddate).AddDays(1));
            }
            return parms;
        }

        public DataTable GetTopicListByCondition(string postname,int forumid, string posterlist, string keylist, string startdate, string enddate, int pageSize, int currentPage)
        {
            string sql = "";
            string condition = GetCondition(forumid, posterlist, keylist, startdate, enddate);
            DbParameter[] parms = GetParms(startdate, enddate);
            int pageTop = (currentPage - 1) * pageSize;

            int mintid = DatabaseProvider.GetInstance().GetMinPostTableTid(postname);
            int maxtid = DatabaseProvider.GetInstance().GetMaxPostTableTid(postname);
            //if (currentPage == 1)
            //{
            //sql = "SELECT t.*,f.`name` FROM `" + BaseConfigs.GetTablePrefix + "topics` t LEFT JOIN " + BaseConfigs.GetTablePrefix + "forums f ON t.fid=f.fid LEFT JOIN `" + BaseConfigs.GetTablePrefix + "forumfields` as ff ON f.`fid`=ff.`fid` AND (ff.`viewperm` IS NULL OR ff.`viewperm`='' OR InStr(','+ff.`viewperm`+',',',7,')<>0) "
            //        + "WHERE `closed`<>1 " + condition + "  AND `tid`>=" + mintid + " AND `tid`<=" + maxtid + " ORDER BY `tid` DESC LIMIT " + pageTop + "," + pageSize.ToString();

            sql =
                       string.Format(
                           "SELECT t.*,f.`name` FROM `{1}topics` t LEFT JOIN `{1}forums` f ON t.fid=f.fid LEFT JOIN `{1}forumfields` ff ON f.`fid`=ff.`fid` AND (ff.`viewperm` IS NULL OR ff.`viewperm`='' OR InStr(','+ff.`viewperm`+',',',7,')<>0) WHERE `closed`<>1 AND `status`=1 AND `password`='' {2} AND `tid`>=" + mintid + " AND `tid`<=" + maxtid + " ORDER BY `tid` DESC limit {3},{4}",
                           pageSize, BaseConfigs.GetTablePrefix, condition, pageTop, pageSize);

            //}
            //else
            //{
            //    sql = "SELECT t.*,f.`name` FROM `" + BaseConfigs.GetTablePrefix + "topics` t LEFT JOIN " + BaseConfigs.GetTablePrefix + "forums f ON t.fid=f.fid "
            //        + "WHERE `closed`<>1 AND `tid`<(SELECT MIN(`tid`) FROM (SELECT `tid` FROM `" + BaseConfigs.GetTablePrefix + "topics` WHERE "
            //        + "`closed`<>1 " + condition + " ORDER BY `tid` DESC LIMIT 0,"+pageTop.ToString()+") AS tblTmp)" + condition + " ORDER BY `tid` DESC LIMIT 0,"+pageSize.ToString();
            //}
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
        }

        private static string GetCondition(int forumid, string posterlist, string keylist, string startdate, string enddate)
        {
            string condition = "";
            if (forumid != 0)
            {
                condition += " AND f.`fid`=" + forumid;
            }
            if (posterlist != "")
            {
                string[] poster = posterlist.Split(',');
                condition += " AND `poster` in (";
                string tempposerlist = "";
                foreach (string p in poster)
                {
                    tempposerlist += "'" + p + "',";
                }
                if (tempposerlist != "")
                    tempposerlist = tempposerlist.Substring(0, tempposerlist.Length - 1);
                condition += tempposerlist + ")";
            }
            if (keylist != "")
            {
                string tempkeylist = "";
                foreach (string key in keylist.Split(','))
                {
                    tempkeylist += " `title` LIKE '%" + RegEsc(key) + "%' OR";
                }
                tempkeylist = tempkeylist.Substring(0, tempkeylist.Length - 2);
                condition += " AND (" + tempkeylist + ")";
            }
            //if (startdate != "")
            //{
            //    condition += " AND `postdatetime`>='" + startdate + " 00:00:00'";
            //}
            //if (enddate != "")
            //{
            //    condition += " AND `postdatetime`<='" + enddate + " 23:59:59'";
            //}
            if (startdate != "")
            {
                //condition += " AND [postdatetime]>='" + startdate + " 00:00:00'";
                condition += " AND `postdatetime`>=?startdate";
            }
            if (enddate != "")
            {
                //condition += " AND [postdatetime]<='" + enddate + " 23:59:59'";
                condition += " AND `postdatetime`<=?enddate";
            }
            return condition;
        }

        public int GetTopicListCountByCondition(string postname,int forumid, string posterlist, string keylist, string startdate, string enddate)
        {
            string sql = string.Format("SELECT COUNT(1) FROM `{0}topics` t LEFT JOIN `{0}forums` f ON t.fid=f.fid LEFT JOIN `{0}forumfields` ff ON f.`fid`=ff.`fid`   AND (ff.`viewperm` IS NULL OR ff.`viewperm`='' OR InStr(','+ff.`viewperm`+',',',7,')<>0) WHERE `closed`<>1 AND `status`=1 AND `password`=''", BaseConfigs.GetTablePrefix);
            string condition = GetCondition(forumid, posterlist, keylist, startdate, enddate);
            DbParameter[] parms = GetParms(startdate, enddate);
            if (condition != "")
                sql += condition;
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, parms).ToString());
        }

        public DataTable GetTopicListByTidlist(string posttableid, string tidlist)
        {
            string sql = "SELECT * FROM `" + BaseConfigs.GetTablePrefix + "topics` WHERE (`tid` IN (" + tidlist + ")) ORDER BY INSTR('" + tidlist + "',`tid`)";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }





        #region 前台聚合页相关函数

        public DataTable GetWebSiteAggForumTopicList(string showtype, int topnumber)
        {
            DataTable topicList = new DataTable();
            switch (showtype)
            {
                default://按版块查
                    {
                        topicList = DbHelper.ExecuteDataset("SELECT f.`fid`, f.`name`, f.`lasttid` AS `tid`, f.`lasttitle` AS `title` , f.`lastposterid` AS `posterid`, f.`lastposter` AS `poster`, f.`lastpost` AS `postdatetime`, t.`views`, t.`replies` FROM `" + BaseConfigs.GetTablePrefix + "forums` f LEFT JOIN `" + BaseConfigs.GetTablePrefix + "topics` t  ON f.`lasttid` = t.`tid` WHERE f.`status`=1  AND `layer`> 0 AND f.`fid` IN (SELECT ff.`fid` FROM `" + BaseConfigs.GetTablePrefix + "forumfields` ff WHERE ff.`password` ='') AND t.`displayorder`>=0").Tables[0]; break;
                    }
                case "1"://按最新主题查
                    {
                        topicList = DbHelper.ExecuteDataset("SELECT t.`tid`, t.`title`, t.`postdatetime`, t.`poster`, t.`posterid`, t.`fid`, t.`views`, t.`replies`, f.`name` FROM `" + BaseConfigs.GetTablePrefix + "topics` t LEFT OUTER JOIN `" + BaseConfigs.GetTablePrefix + "forums` f ON t.`fid` = f.`fid` WHERE t.`displayorder`>=0 AND f.`status`=1  AND `layer`> 0 AND f.`fid` IN (SELECT ff.`fid` FROM `" + BaseConfigs.GetTablePrefix + "forumfields` ff WHERE ff.`password` ='') ORDER BY t.`postdatetime` DESC LIMIT 0," + topnumber).Tables[0]; break;
                    }
                case "2"://按精华主题查
                    {
                        topicList = DbHelper.ExecuteDataset("SELECT t.`tid`, t.`title`, t.`postdatetime`, t.`poster`, t.`posterid`, t.`fid`, t.`views`, t.`replies`, f.`name` FROM `" + BaseConfigs.GetTablePrefix + "topics` t LEFT OUTER JOIN `" + BaseConfigs.GetTablePrefix + "forums` f ON t.`fid` = f.`fid` WHERE t.`digest` >0 AND f.`status`=1  AND `layer`> 0 AND f.`fid` IN (SELECT ff.`fid` FROM `" + BaseConfigs.GetTablePrefix + "forumfields` ff WHERE ff.`password` ='') ORDER BY t.`digest` DESC LIMIT 0," + topnumber).Tables[0]; break;
                    }
                case "3"://按版块查
                    {
                        topicList = DbHelper.ExecuteDataset("SELECT f.`fid`, f.`name`, f.`lasttid` AS `tid`, f.`lasttitle` AS `title` , f.`lastposterid` AS `posterid`, f.`lastposter` AS `poster`, f.`lastpost` AS `postdatetime`, t.`views`, t.`replies` FROM `" + BaseConfigs.GetTablePrefix + "forums` f LEFT JOIN `" + BaseConfigs.GetTablePrefix + "topics` t  ON f.`lasttid` = t.`tid` WHERE f.`status`=1  AND `layer`> 0 AND f.`fid` IN (SELECT ff.`fid` FROM `" + BaseConfigs.GetTablePrefix + "forumfields` ff WHERE ff.`password` ='') AND t.`displayorder`>=0").Tables[0]; break;
                    }
            }
            return topicList;
        }


        public DataTable GetWebSiteAggHotForumList(int topnumber)
        {
            return DbHelper.ExecuteDataset("SELECT `fid`, `name`, `topics` FROM `" + BaseConfigs.GetTablePrefix + "forums` WHERE `status`=1 AND `layer`> 0 AND `fid` IN (SELECT `fid` FROM `" + BaseConfigs.GetTablePrefix + "forumfields` WHERE `password`='') ORDER BY `topics` DESC, `posts` DESC, `todayposts` DESC LIMIT " + topnumber).Tables[0];
        }
        #endregion

        //public DataTable GetWebSiteAggForumTopicList(string type, int forumid,int topnumber, string posttablename)
        //{
        //    string orderby = " [t].[tid] ";
        //    if (type.ToLower() == "newtopic")//新帖
        //    {
        //        orderby = " [t].[tid] ";
        //    }
        //    if (type.ToLower() == "hottopic")//热帖
        //    {
        //        orderby = " [t].[replies] ";
        //    }
        //    if (type.ToLower() == "views")//浏览量
        //    {
        //        orderby = " [t].[views] ";
        //    }
        //    if (type.ToLower() == "lastpost")//
        //    {
        //        orderby = " [t].[lastpost] ";
        //    }

        //    string condition = ""; 
        //    if (forumid > 0)
        //    {
        //        condition += " [t].[fid] = " + forumid + " OR (CHARINDEX('," + forumid + ",' , ',' + RTRIM([f].[parentidlist]) + ',') > 0)) ";
        //    }

        //    if (type.ToLower().IndexOf("digest")>=0)//精华帖
        //    {
        //        condition += " [t].[digest] = " + type.Replace("digest", "[digest]").Replace(":"," = ") + " AND ";
        //    }
        //    condition += " [t].[displayorder]>=0 AND [t].[closed]<>1 AND [p].[layer]= 0 ";

        //    return DbHelper.ExecuteDataset("SELECT TOP " + topnumber + " [t].[tid] AS [tid], [t].[title] AS [title], [t].[poster] AS [poster], [t].[posterid] AS [posterid], [t].[postdatetime] AS [postdatetime], [f].[fid], [f].[name], [p].[message] AS [message] FROM [" + BaseConfigs.GetTablePrefix + "topics] AS [t] LEFT JOIN [" + posttablename + "] AS [p] ON [t].[tid] = [p].[tid] LEFT JOIN [" + BaseConfigs.GetTablePrefix + "forums] AS [f] ON [t].[fid] = [f].[fid] WHERE " + condition + " ORDER BY " + orderby + " DESC ").Tables[0];
        //}

        //public DataTable GetWebSiteAggForumHotTopicList()
        //{
        //    string condition = "";
        //    if (forumid >= 0)
        //    {
        //        condition += " [t].[fid] = " + forumid + " AND ";
        //    }
        //    condition += " [displayorder]>=0 AND [closed]<>1 ";
        //    return DbHelper.ExecuteDataset("SELECT TOP " + topnumber + " [t].[tid], [t].[title] FROM [" + BaseConfigs.GetTablePrefix + "topics] AS [t] LEFT JOIN [" + posttablename + "] WHERE "+condition+" ORDER BY [t].[replies] DESC").Tables[0];
        //}


        
        ///// <summary>
        ///// 获得用户列表DataTable
        ///// </summary>
        ///// <param name="pagesize">每页记录数</param>
        ///// <param name="pageindex">当前页数</param>
        ///// <returns>用户列表DataTable</returns>
        //public DataTable GetWebSiteAggUserList(string type, int topnumber)
        //{
        //    switch (i)
        //    {
        //        //case 0:
        //        //    orderby = "ORDER BY [" + BaseConfigs.GetTablePrefix + "users].[uid] " + ordertype;
        //        //    break;
        //        case 0:
        //            orderby = string.Format("ORDER BY [{0}users].[username] {1},[{0}users].[uid] {1}", BaseConfigs.GetTablePrefix, ordertype);
        //            break;
        //        case 1:
        //            orderby = string.Format("ORDER BY [{0}users].[credits] {1},[{0}users].[uid] {1}", BaseConfigs.GetTablePrefix, ordertype);
        //            break;
        //        case 2:
        //            orderby = string.Format("ORDER BY [{0}users].[posts] {1},[{0}users].[uid] {1}", BaseConfigs.GetTablePrefix, ordertype);
        //            break;
        //        case 3:
        //            orderby = string.Format("WHERE [{0}users].[adminid] > 0 ORDER BY [{0}users].[adminid] {1}, [{0}users].[uid] {1}", BaseConfigs.GetTablePrefix, ordertype);
        //            break;
        //        //case "joindate":
        //        //    orderby = "ORDER BY [" + BaseConfigs.GetTablePrefix + "users].[joindate] " + ordertype + ",[" + BaseConfigs.GetTablePrefix + "users].[uid] " + ordertype;
        //        //    break;
        //        case 4:
        //            orderby = string.Format("ORDER BY [{0}users].[lastactivity] {1},[{0}users].[uid] {1}", BaseConfigs.GetTablePrefix, ordertype);
        //            break;
        //        case 5:
        //            orderby = string.Format("ORDER BY [{0}users].[joindate] {1},[{0}users].[uid] {1}", BaseConfigs.GetTablePrefix, ordertype);
        //            break;
        //        default:
        //            orderby = string.Format("ORDER BY [{0}users].[uid] {1}", BaseConfigs.GetTablePrefix, ordertype);
        //            break;
        //    }

        //    DbParameter[] prams = {
        //                               DbHelper.MakeInParam("@pagesize", (DbType)MySqlDbType.Int32,4,pagesize),
        //                               DbHelper.MakeInParam("@pageindex",(DbType)MySqlDbType.Int32,4,pageindex),
        //                               DbHelper.MakeInParam("@orderby",(DbType)MySqlDbType.VarChar,1000,orderby)
        //                           };
        //    return DbHelper.ExecuteDataset(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "getuserlist", prams).Tables[0];
        //}

    }
}
#endif