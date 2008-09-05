#if NET1
#else
using System;
using System.Text;
using Discuz.Data;
using System.Data;
using Discuz.Entity;
using System.Data.SqlClient;
using Discuz.Config;
using System.Data.Common;
using System.IO;
using Discuz.Common;
using Discuz.Common.Generic;

namespace Discuz.Space.Data
{
    public class DataProvider 
    {
        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        private string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                //Regex rgx = new Regex(s);
                //keyword = rgx.Replace(keyword, "\\" + s);
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;

                }

            }
            return str;
        }
        private DbParameter[] GetDateSpanParms(string startdate, string enddate)
        {
            DbParameter[] parms = new DbParameter[2];
            if (startdate != "")
            {
                parms[0] = DbHelper.MakeInParam("@startdate", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(startdate));
            }
            if (enddate != "")
            {
                parms[1] = DbHelper.MakeInParam("@enddate", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(enddate).AddDays(1));
            }
            return parms;
        }

        public int AddSpacePhoto(PhotoInfo photoinfo)
        {
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,photoinfo.Userid),
                    DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20, photoinfo.Username),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 20,photoinfo.Title),
					DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4,photoinfo.Albumid),
					DbHelper.MakeInParam("@filename", (DbType)SqlDbType.NVarChar, 255,photoinfo.Filename),
					DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.NVarChar, 255,photoinfo.Attachment),
					DbHelper.MakeInParam("@filesize", (DbType)SqlDbType.Int, 4,photoinfo.Filesize),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NVarChar, 200,photoinfo.Description),
                    DbHelper.MakeInParam("@isattachment",(DbType)SqlDbType.Int,4,photoinfo.IsAttachment),
                    DbHelper.MakeInParam("@commentstatus", (DbType)SqlDbType.TinyInt, 1, (byte)photoinfo.Commentstatus),
                    DbHelper.MakeInParam("@tagstatus", (DbType)SqlDbType.TinyInt, 1, (byte)photoinfo.Tagstatus)
					//DbHelper.MakeInParam("@creatdatetime", (DbType)SqlDbType.DateTime, 8,spaceAlbum.Createdatetime)
				};
            string sqlstring = String.Format("INSERT INTO [{0}photos] ([userid], [username], [title], [albumid], [filename], [attachment], [filesize], [description],[isattachment],[commentstatus], [tagstatus]) VALUES ( @userid, @username, @title, @albumid, @filename, @attachment, @filesize, @description,@isattachment, @commentstatus, @tagstatus);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);

            //向关联表中插入相关数据
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), 0);
        }

        public int GetSpacePhotoCountByAlbumId(int albumid)
        {
            DbParameter parm = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            string sql = string.Format("SELECT COUNT(1) FROM [{0}photos] WHERE [albumid]=@albumid", BaseConfigs.GetTablePrefix);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parm), 0);
        }

        public bool SaveSpaceAlbum(AlbumInfo spaceAlbum)
        {
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, spaceAlbum.Albumid),
					DbHelper.MakeInParam("@albumcateid", (DbType)SqlDbType.Int, 4, spaceAlbum.Albumcateid),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 50,spaceAlbum.Title),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 200,spaceAlbum.Description),
					DbHelper.MakeInParam("@password", (DbType)SqlDbType.NChar, 50,spaceAlbum.Password),
					DbHelper.MakeInParam("@imgcount", (DbType)SqlDbType.Int, 4,spaceAlbum.Imgcount),
					DbHelper.MakeInParam("@logo", (DbType)SqlDbType.NChar, 255, spaceAlbum.Logo),
					DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 8,spaceAlbum.Type)
				};
            string sqlstring = String.Format("UPDATE [{0}albums] SET [albumcateid] = @albumcateid, [title] = @title, [description] = @description, [password] = @password, [imgcount] = @imgcount, [logo] = @logo, [type] = @type WHERE [albumid] = @albumid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
        }



        #region Space 个人数据操作

        public IDataReader GetSpaceConfigDataByUserID(int userid)
        {
            IDataReader IDataReader = DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}spaceconfigs] WHERE [userid] = {1}", BaseConfigs.GetTablePrefix, userid));
            return IDataReader;
        }


        public IDataReader GetSpaceConfigDataBySpaceID(int spaceid)
        {
            IDataReader IDataReader = DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT * FROM [{0}spaceconfigs] WHERE [spaceid] = {1}", BaseConfigs.GetTablePrefix, spaceid));
            return IDataReader;
        }


        /// <summary>
        /// 保存用户space配置信息
        /// </summary>
        /// <param name="spaceconfiginfo"></param>
        /// 
        /// <returns></returns>
        public bool SaveSpaceConfigData(SpaceConfigInfo spaceconfiginfo)
        {
            //try
            //{
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@spacetitle", (DbType)SqlDbType.NVarChar, 100, spaceconfiginfo.Spacetitle),
										   DbHelper.MakeInParam("@description", (DbType)SqlDbType.NVarChar, 200, spaceconfiginfo.Description),
										   DbHelper.MakeInParam("@blogdispmode", (DbType)SqlDbType.TinyInt, 1, spaceconfiginfo.BlogDispMode),
										   DbHelper.MakeInParam("@bpp", (DbType)SqlDbType.Int, 4, spaceconfiginfo.Bpp),
										   DbHelper.MakeInParam("@commentpref", (DbType)SqlDbType.TinyInt, 1, spaceconfiginfo.Commentpref),
										   DbHelper.MakeInParam("@messagepref", (DbType)SqlDbType.TinyInt, 1, spaceconfiginfo.MessagePref),
										   //DbHelper.MakeInParam("@rewritename", (DbType)SqlDbType.VarChar, 100, spaceconfiginfo.Rewritename),
										   DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4, spaceconfiginfo.ThemeID),
										   DbHelper.MakeInParam("@themepath", (DbType)SqlDbType.NChar, 50, spaceconfiginfo.ThemePath),
										   DbHelper.MakeInParam("@status", (DbType)SqlDbType.Int, 4, spaceconfiginfo.Status),
										   DbHelper.MakeInParam("@updatedatetime", (DbType)SqlDbType.SmallDateTime, 4, DateTime.Now),
										   
										   //DbHelper.MakeInParam("@defaulttab", (DbType)SqlDbType.Int, 4, DateTime.Now),
										   
										   DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, spaceconfiginfo.UserID)
									   };
            string sqlstring = "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceconfigs] SET [spacetitle] = @spacetitle ,[description] = @description,[blogdispmode] = @blogdispmode,[bpp] = @bpp, [commentpref] = @commentpref, [messagepref] = @messagepref, [themeid]=@themeid,[themepath] = @themepath, [updatedatetime] = @updatedatetime WHERE [userid] = @userid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }


        /// <summary>
        /// 建议用户space信息
        /// </summary>
        /// <param name="spaceconfiginfo"></param>
        /// 
        /// <returns></returns>
        public int AddSpaceConfigData(SpaceConfigInfo spaceconfiginfo)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,spaceconfiginfo.UserID),
					DbHelper.MakeInParam("@spacetitle", (DbType)SqlDbType.NChar, 100,spaceconfiginfo.Spacetitle),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 200,spaceconfiginfo.Description),
					DbHelper.MakeInParam("@blogdispmode", (DbType)SqlDbType.TinyInt, 1,spaceconfiginfo.BlogDispMode),
					DbHelper.MakeInParam("@bpp", (DbType)SqlDbType.Int, 4,spaceconfiginfo.Bpp),
					DbHelper.MakeInParam("@commentpref", (DbType)SqlDbType.TinyInt, 1,spaceconfiginfo.Commentpref),
					DbHelper.MakeInParam("@messagepref", (DbType)SqlDbType.TinyInt, 1,spaceconfiginfo.MessagePref),
					DbHelper.MakeInParam("@rewritename", (DbType)SqlDbType.Char, 100,spaceconfiginfo.Rewritename),
					DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4,spaceconfiginfo.ThemeID),
					DbHelper.MakeInParam("@themepath", (DbType)SqlDbType.NChar, 50,spaceconfiginfo.ThemePath),
					DbHelper.MakeInParam("@postcount", (DbType)SqlDbType.Int, 4,spaceconfiginfo.PostCount),
					DbHelper.MakeInParam("@commentcount", (DbType)SqlDbType.Int, 4,spaceconfiginfo.CommentCount),
					DbHelper.MakeInParam("@visitedtimes", (DbType)SqlDbType.Int, 4,spaceconfiginfo.VisitedTimes),
					DbHelper.MakeInParam("@createdatetime", (DbType)SqlDbType.SmallDateTime, 4,spaceconfiginfo.CreateDateTime),
					DbHelper.MakeInParam("@updatedatetime", (DbType)SqlDbType.SmallDateTime, 4,spaceconfiginfo.UpdateDateTime),
					DbHelper.MakeInParam("@status", (DbType)SqlDbType.Int, 4, spaceconfiginfo.Status),
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "spaceconfigs] ([userid], [spacetitle], [description], [blogdispmode], [bpp], [commentpref], [messagepref], [rewritename], [themeid], [themepath], [postcount], [commentcount], [visitedtimes], [createdatetime], [updatedatetime]) VALUES (@userid, @spacetitle, @description, @blogdispmode, @bpp, @commentpref, @messagepref, @rewritename, @themeid, @themepath, @postcount, @commentcount, @visitedtimes, @createdatetime, @updatedatetime);SELECT SCOPE_IDENTITY()");

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), 0);
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return 0;
            //}
        }

        /// <summary>
        /// 为当前用户的SPACE访问量加1
        /// </summary>
        /// <param name="userid"></param>
        /// 
        /// <returns></returns>
        public bool CountUserSpaceVisitedTimesByUserID(int userid)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,userid)
				};

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceconfigs] SET [visitedtimes] = [visitedtimes] + 1 WHERE [userid] = @userid", parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }


        /// <summary>
        /// 更新当前用户的SPACE日志数
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool CountUserSpacePostCountByUserID(int userid, int postcount)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@postcount", (DbType)SqlDbType.Int, 4,postcount),
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,userid)
				};

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceconfigs] SET [postcount] = [postcount] + @postcount  WHERE [userid] = @userid", parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }


        /// <summary>
        /// 更新当前用户的SPACE评论数
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool CountUserSpaceCommentCountByUserID(int userid, int commentcount)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@commentcount", (DbType)SqlDbType.Int, 4,commentcount),
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,userid)
				};

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceconfigs] SET [commentcount] = [commentcount] + @commentcount  WHERE [userid] = @userid", parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }
        #endregion


        #region Space 主题数据操作
        public IDataReader GetSpaceThemeDataByThemeID(int themeid)
        {
            IDataReader IDataReader = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE [themeid] = " + themeid);
            return IDataReader;
        }
        #endregion


        #region Space 评论数据操作
        public bool AddSpaceComment(SpaceCommentInfo spacecomments)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					//DbHelper.MakeInParam("@commentid", (DbType)SqlDbType.Int, 4,spacecomments.CommentID),
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,spacecomments.PostID),
					DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 50,spacecomments.Author),
					DbHelper.MakeInParam("@email", (DbType)SqlDbType.NVarChar, 100,spacecomments.Email),
					DbHelper.MakeInParam("@url", (DbType)SqlDbType.NVarChar, 255,spacecomments.Url),
					DbHelper.MakeInParam("@ip", (DbType)SqlDbType.VarChar, 100,spacecomments.Ip),
					DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.SmallDateTime, 4,spacecomments.PostDateTime),
					DbHelper.MakeInParam("@content", (DbType)SqlDbType.NText, 0,spacecomments.Content),
					DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int, 4,spacecomments.ParentID),
					DbHelper.MakeInParam("@posttitle", (DbType)SqlDbType.NVarChar, 60,spacecomments.PostTitle),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,spacecomments.Uid)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "spacecomments] ( [postid], [author], [email], [url], [ip], [postdatetime], [content], [parentid], [uid],[posttitle] ) VALUES ( @postid, @author, @email, @url, @ip, @postdatetime, @content, @parentid, @uid, @posttitle)");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }

        public bool SaveSpaceComment(SpaceCommentInfo spacecomments)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@commentid", (DbType)SqlDbType.Int, 4,spacecomments.CommentID),
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,spacecomments.PostID),
					DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 50,spacecomments.Author),
					DbHelper.MakeInParam("@email", (DbType)SqlDbType.NVarChar, 100,spacecomments.Email),
					DbHelper.MakeInParam("@url", (DbType)SqlDbType.NVarChar, 255,spacecomments.Url),
					DbHelper.MakeInParam("@ip", (DbType)SqlDbType.VarChar, 100,spacecomments.Ip),
					DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.SmallDateTime, 4,spacecomments.PostDateTime),
					DbHelper.MakeInParam("@content", (DbType)SqlDbType.NText, 0,spacecomments.Content),
					DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int, 4,spacecomments.ParentID),
					DbHelper.MakeInParam("@posttitle", (DbType)SqlDbType.NVarChar, 60,spacecomments.PostTitle),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,spacecomments.Uid)
				};
            string sqlstring = String.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "spacecomments]  Set [postid] = @postid, [author] = @author, [email] = @email, [url] = @url, [ip] = @ip, [postdatetime] = @postdatetime, [content] = @content, [parentid] = @parentid, [uid] = @uid, [posttitle]=@posttile  WHERE [commentid] = @commentid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentidList">删除评论的commentid列表</param>
        /// <returns></returns>
        public bool DeleteSpaceComments(string commentidList, int userid)
        {
            if (!Utils.IsNumericArray(commentidList.Split(',')))
                return false;

            try
            {
                string sqlstring = string.Format(@"DELETE FROM [{0}spacecomments] 
                                                     FROM [{0}spaceposts] 
                                                     WHERE [{0}spaceposts].[postid] = [{0}spacecomments].[postid] AND [{0}spaceposts].[uid]={1} AND
                                                     [{0}spacecomments].[commentid] IN ({2})", BaseConfigs.GetTablePrefix, userid, commentidList);//"DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE [commentid] IN (" + commentidList + ") AND [uid]=" + userid;
                DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpaceComments(int userid)
        {
            try
            {
                string sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE [uid]=" + userid;
                DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentid">删除评论的commentid</param>
        /// <returns></returns>
        public bool DeleteSpaceComment(int commentid)
        {
            try
            {
                string sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE [commentid] = " + commentid;
                DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
                return true;
            }
            catch
            {
                return false;
            }
        }

        

        /// <summary>
        /// 返回指定页数与条件的评论列表
        /// </summary>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="currentPage">当前页号</param>
        /// <param name="userid">用户ID</param>
        /// <param name="orderbyASC">排序方式，true为升序，false为降序</param>
        /// <returns></returns>
        public DataTable GetSpaceCommentList(int pageSize, int currentPage, int userid, bool orderbyASC)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                string ordertype = orderbyASC ? "ASC" : "DESC";
                int pageTop = (currentPage - 1) * pageSize;

                string sql = "";

                if (currentPage == 1)
                {
                    sql = string.Format(@"SELECT TOP {0} [sc].* FROM  
                        [{1}spacecomments] AS [sc], [{1}spaceposts] AS [sp] WHERE [sc].[postid]=[sp].[postid] AND [sp].[uid]=@userid ORDER BY [sc].[commentid] {2}", pageSize, BaseConfigs.GetTablePrefix, ordertype);
                }
                else
                {
                    if (!orderbyASC)
                    {
                        sql = string.Format(@"SELECT TOP {0} [sc].* FROM 
                            [{1}spacecomments] AS [sc], [{1}spaceposts] AS [sp] WHERE [commentid] < (SELECT min([commentid])  FROM 
                             (SELECT TOP {2} [sc1].[commentid] FROM [{1}spacecomments] AS [sc1], [{1}spaceposts] AS [sp1] WHERE 
                            [sc1].[postid]=[sp1].[postid] AND [sp1].[uid]=@userid ORDER BY [sc1].[commentid] {3}) AS tblTmp ) AND [sc].[postid]=[sp].[postid] AND [sp].[uid]=@userid ORDER BY [sc].[commentid] {3}", pageSize, BaseConfigs.GetTablePrefix, pageTop, ordertype);
                    }
                    else
                    {
                        sql = string.Format(@"SELECT TOP {0} [sc].* FROM 
                            [{1}spacecomments] AS [sc], [{1}spaceposts] AS [sp] WHERE [commentid] > (SELECT MAX([commentid])  FROM 
                            (SELECT TOP {2} [commentid] FROM [{1}spacecomments] AS [sc1], [{1}spaceposts] AS [sp1] WHERE 
                            [sc1].[postid]=[sp1].[postid] AND [sp1].[uid]=@userid ORDER BY [commentid] {3}) AS tblTmp ) AND [sc].[postid]=[sp].[postid] AND [sp].[uid]=@userid ORDER BY [sc].[commentid] {3}", pageSize, BaseConfigs.GetTablePrefix, pageTop, ordertype);
                    }
                }
                return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetSpaceCommentListByPostid(int pageSize, int currentPage, int postid, bool orderbyASC)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4, postid);
                string ordertype = orderbyASC ? "ASC" : "DESC";
                int pageTop = (currentPage - 1) * pageSize;

                string sql = "";

                if (currentPage == 1)
                {
                    sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                        + "[" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE [postid]=@postid ORDER BY [commentid] " + ordertype;
                }
                else
                {
                    if (!orderbyASC)
                    {
                        sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                            + "[" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE [commentid] < (SELECT min([commentid])  FROM "
                            + "(SELECT TOP " + pageTop + " [commentid] FROM [" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE "
                            + "[postid]=@postid ORDER BY [commentid] " + ordertype + ") AS tblTmp ) AND [postid]=@postid ORDER BY [commentid] " + ordertype;
                    }
                    else
                    {
                        sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                            + "[" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE [commentid] > (SELECT MAX([commentid])  FROM "
                            + "(SELECT TOP " + pageTop + " [commentid] FROM [" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE "
                            + "[postid]=@postid ORDER BY [commentid] " + ordertype + ") AS tblTmp ) AND [postid]=@postid ORDER BY [commentid] " + ordertype;

                    }
                }
                return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }


        /// <summary>
        /// 返回满足条件的评论数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetSpaceCommentsCount(int userid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                return (int)DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT([sc].[commentid]) FROM [{0}spacecomments] AS [sc], [{0}spaceposts] AS [sp] WHERE [sc].[postid]=[sp].[postid] AND [sp].[uid]=@userid", BaseConfigs.GetTablePrefix), parm);
            }
            catch
            {
                return 0;
            }
        }

        public int GetSpaceCommentsCountByPostid(int postid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4, postid);
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([commentid]) FROM [" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE [postid]=@postid", parm);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回全部评论数
        /// </summary>
        /// <returns></returns>
        public DataTable GetSpaceNewComments(int topcount, int userid)
        {
            try
            {
                string sql = "SELECT TOP " + topcount.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spacecomments] WHERE [postid] IN (SELECT TOP 10 [postid] FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [uid] = " + userid + " AND [commentcount]>0 ORDER BY [postid] DESC) ORDER BY [commentid] DESC";
                return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];

            }
            catch
            {
                return new DataTable();
            }
        }


        #endregion


        #region Space 日志数据操作
        public int AddSpacePost(SpacePostInfo spaceposts)
        {
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,spaceposts.Postid),
					DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 20,spaceposts.Author),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,spaceposts.Uid),
					DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,spaceposts.Postdatetime),
					DbHelper.MakeInParam("@content", (DbType)SqlDbType.NText, 0,spaceposts.Content),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 150,spaceposts.Title),
					DbHelper.MakeInParam("@category", (DbType)SqlDbType.VarChar, 255,spaceposts.Category),
					DbHelper.MakeInParam("@poststatus", (DbType)SqlDbType.TinyInt, 1,spaceposts.PostStatus),
					DbHelper.MakeInParam("@commentstatus", (DbType)SqlDbType.TinyInt, 1,spaceposts.CommentStatus),
					DbHelper.MakeInParam("@postupdatetime", (DbType)SqlDbType.DateTime, 8,spaceposts.PostUpDateTime),
					DbHelper.MakeInParam("@commentcount", (DbType)SqlDbType.Int, 4,spaceposts.Commentcount)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "spaceposts] ([author], [uid], [postdatetime], [content], [title], [category], [poststatus], [commentstatus], [postupdatetime], [commentcount]) VALUES ( @author, @uid, @postdatetime, @content, @title, @category, @poststatus, @commentstatus, @postupdatetime, @commentcount);SELECT SCOPE_IDENTITY();");

            //向关联表中插入相关数据
            int postid = Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), 0);
            sqlstring = "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceconfigs] SET [updatedatetime]=@postupdatetime WHERE [userid]=@uid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            if (postid > 0)
            {
                foreach (string cateogryid in spaceposts.Category.Split(','))
                {
                    if (cateogryid != "")
                    {
                        SpacePostCategoryInfo spacepostCategoryInfo = new SpacePostCategoryInfo();
                        spacepostCategoryInfo.PostID = postid;
                        spacepostCategoryInfo.CategoryID = Convert.ToInt32(cateogryid);
                        AddSpacePostCategory(spacepostCategoryInfo);
                    }
                }
            }

            DbParameter[] prams1 = 
				{
					DbHelper.MakeInParam("@spacepostid", (DbType)SqlDbType.Int, 4, postid),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,spaceposts.Uid)
				};

            //更新与当前日志关联的附件表中的数据
            DbHelper.ExecuteReader(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceattachments] SET [spacepostid] = @spacepostid  WHERE [spacepostid] = 0 AND [uid] = @uid ", prams1);

            //对当前用户日志加1
            CountUserSpacePostCountByUserID(spaceposts.Uid, 1);

            return postid;
        }

        public bool SaveSpacePost(SpacePostInfo spaceposts)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4, spaceposts.Postid),
					DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 20, spaceposts.Author),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, spaceposts.Uid),
					DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, spaceposts.Postdatetime),
					DbHelper.MakeInParam("@content", (DbType)SqlDbType.NText, 0, spaceposts.Content),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 150, spaceposts.Title),
					DbHelper.MakeInParam("@category", (DbType)SqlDbType.VarChar, 255, spaceposts.Category),
					DbHelper.MakeInParam("@poststatus", (DbType)SqlDbType.TinyInt, 1, spaceposts.PostStatus),
					DbHelper.MakeInParam("@commentstatus", (DbType)SqlDbType.TinyInt, 1, spaceposts.CommentStatus),
					DbHelper.MakeInParam("@postupdatetime", (DbType)SqlDbType.DateTime, 8, spaceposts.PostUpDateTime)
				};
            string sqlstring = String.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "spaceposts]  SET [author] = @author, [uid] = @uid, [postdatetime] = @postdatetime, [content] = @content, [title] = @title, [category] = @category, [poststatus] = @poststatus, [commentstatus] = @commentstatus, [postupdatetime] = @postupdatetime WHERE [postid] = @postid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            sqlstring = "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceconfigs] SET [updatedatetime]=@postupdatetime WHERE [userid]=@uid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            //先删除指定的日志关联数据再插入新数据
            DeleteSpacePostCategoryByPostID(spaceposts.Postid);

            foreach (string cateogryid in spaceposts.Category.Split(','))
            {
                if (cateogryid != "")
                {
                    SpacePostCategoryInfo spacepostCategoryInfo = new SpacePostCategoryInfo();
                    spacepostCategoryInfo.PostID = spaceposts.Postid;
                    spacepostCategoryInfo.CategoryID = Convert.ToInt32(cateogryid);
                    AddSpacePostCategory(spacepostCategoryInfo);
                }
            }


            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }

        public IDataReader GetSpacePost(int postid)
        {
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE  [postid]=" + postid);
            return dr;
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="postidList">删除日志的postid列表</param>
        /// <returns></returns>
        public bool DeleteSpacePosts(string postidList, int userid)
        {
            if (!Utils.IsNumericArray(postidList.Split(',')))
            {
                return false;
            }

            string sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [postid] IN (" + postidList + ") AND [uid]=" + userid;
            int deletedCount = DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);

            if (deletedCount > 0)
            {
                sqlstring = string.Format("UPDATE [{0}spaceconfigs] SET [postcount] = [postcount] - {1} WHERE [userid] = {2}", BaseConfigs.GetTablePrefix, deletedCount, userid);
                DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
            }
            return true;
        }

        public bool DeleteSpacePosts(int userid)
        {
            string sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [uid]=" + userid;
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
            sqlstring = string.Format("UPDATE [{0}spaceconfigs] SET [postcount] = 0 WHERE [userid] = {1}", BaseConfigs.GetTablePrefix, userid);
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);

            return true;
        }

        /// <summary>
        /// 返回指定页数与条件的日志列表
        /// </summary>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="currentPage">当前页号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public DataTable SpacePostsList(int pageSize, int currentPage, int userid)
        {
            DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
            int pageTop = (currentPage - 1) * pageSize;
            string sql = "";
            if (currentPage == 1)
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [uid]=@userid ORDER BY [postid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [postid] < (SELECT min([postid])  FROM "
                    + "(SELECT TOP " + pageTop + " [postid] FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE "
                    + "[uid]=@userid ORDER BY [postid] DESC) AS tblTmp ) AND [uid]=@userid ORDER BY [postid] DESC";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
        }

        public DataTable SpacePostsList(int pageSize, int currentPage, int userid, int poststatus)
        {
            DbParameter[] parms = 
			{
			    DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
				DbHelper.MakeInParam("@poststatus", (DbType)SqlDbType.Int, 4, poststatus)
			};
            int pageTop = (currentPage - 1) * pageSize;
            string sql = "";
            if (currentPage == 1)
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [uid]=@userid AND [poststatus]=@poststatus ORDER BY [postid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [postid] < (SELECT min([postid])  FROM "
                    + "(SELECT TOP " + pageTop + " [postid] FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE "
                    + "[uid]=@userid AND [poststatus]=@poststatus ORDER BY [postid] DESC) AS tblTmp ) AND [uid]=@userid AND [poststatus]=@poststatus ORDER BY [postid] DESC";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable SpacePostsList(int pageSize, int currentPage, int userid, DateTime postdatetime)
        {
            DbParameter[] parms = 
			{
			    DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
				DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, postdatetime)
			};

            int pageTop = (currentPage - 1) * pageSize;
            string sql = "";
            if (currentPage == 1)
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [uid]=@userid AND [poststatus]=1 AND "
                    + "DATEDIFF(d, @postdatetime, postdatetime) = 0 ORDER BY [postid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [postid] < (SELECT min([postid])  FROM "
                    + "(SELECT TOP " + pageTop + " [postid] FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE "
                    + "[uid]=@userid AND [poststatus]=1 AND DATEDIFF(d, @postdatetime, postdatetime) = 0 ORDER BY [postid] DESC) AS tblTmp ) "
                    + "AND [uid]=@userid AND [poststatus]=1 AND DATEDIFF(d, @postdatetime, postdatetime) = 0 ORDER BY [postid] DESC";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
        }

        /// <summary>
        /// 返回满足条件的日志数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetSpacePostsCount(int userid)
        {
            try
            {
                DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid)
				};
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([postid]) FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE uid=@userid", parms);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回满足条件的日志数
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="poststatus"></param>
        /// <returns></returns>
        public int GetSpacePostsCount(int userid, int poststatus)
        {
            try
            {
                DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                    DbHelper.MakeInParam("@poststatus", (DbType)SqlDbType.Int, 4, poststatus)
				};
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([postid]) FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [uid]=@userid AND [poststatus]=@poststatus", parms);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回满足条件的日志数
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="poststatus"></param>
        /// <returns></returns>
        public int GetSpacePostsCount(int userid, int poststatus, string postdatetime)
        {
            try
            {
                DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                    DbHelper.MakeInParam("@poststatus", (DbType)SqlDbType.Int, 4, poststatus),
                    DbHelper.MakeInParam("@postdatetime",(DbType)SqlDbType.DateTime, 8, DateTime.Parse(postdatetime))
				};
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([postid]) FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [uid]=@userid AND [poststatus]=@poststatus AND DATEDIFF(d, @postdatetime, postdatetime) = 0", parms);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 为当前用户的SPACE日志查看数加1
        /// </summary>
        /// <param name="postid"></param>
        /// 
        /// <returns></returns>
        public bool CountUserSpacePostByUserID(int postid)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,postid)
				};

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceposts] SET [views] = [views] + 1 WHERE [postid] = @postid", parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }


        /// <summary>
        /// 更新当前日志数的评论数
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool CountSpaceCommentCountByPostID(int postid, int commentcount)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@commentcount", (DbType)SqlDbType.Int, 4,commentcount),
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,postid)
				};

            if (commentcount >= 0)
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceposts] SET [commentcount] = [commentcount] + @commentcount  WHERE [postid] = @postid ", parms);
            }
            else
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceposts] SET [commentcount] = [commentcount] + @commentcount  WHERE [postid] = @postid AND [commentcount]>0", parms);
            }
            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }


        /// <summary>
        /// 更新当前日志数的浏览量
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool CountUserSpaceViewsByUserID(int postid, int views)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@views", (DbType)SqlDbType.Int, 4,views),
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,postid)
				};

            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spaceposts] SET [views] = [views] + @views  WHERE [postid] = @postid", parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }
        #endregion


        #region 日志类型 操作类

        public IDataReader GetSpaceCategoryByCategoryID(int categoryid)
        {
            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [categoryid] = " + categoryid);
            return reader;
        }


        public bool AddSpaceCategory(SpaceCategoryInfo spacecategories)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,spacecategories.CategoryID),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 50,spacecategories.Title),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,spacecategories.Uid),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NVarChar, 1000,spacecategories.Description),
					DbHelper.MakeInParam("@typeid", (DbType)SqlDbType.Int, 4,spacecategories.TypeID),
					DbHelper.MakeInParam("@categorycount", (DbType)SqlDbType.Int, 4,spacecategories.CategoryCount),
					DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4,spacecategories.Displayorder)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "spacecategories] ( [title], [uid], [description], [typeid], [categorycount], [displayorder]) VALUES ( @title, @uid, @description, @typeid, @categorycount, @displayorder)");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}

        }

        public bool SaveSpaceCategory(SpaceCategoryInfo spacecategories)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,spacecategories.CategoryID),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 50,spacecategories.Title),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,spacecategories.Uid),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NVarChar, 1000,spacecategories.Description),
					DbHelper.MakeInParam("@typeid", (DbType)SqlDbType.Int, 4,spacecategories.TypeID),
					DbHelper.MakeInParam("@categorycount", (DbType)SqlDbType.Int, 4,spacecategories.CategoryCount),
					DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4,spacecategories.Displayorder)
				};
            string sqlstring = String.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "spacecategories] SET  [title] = @title, [uid] = @uid, [description] = @description, [typeid] = @typeid, [categorycount] = @categorycount, [displayorder] = @displayorder WHERE [categoryid] = @categoryid ");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}

        }

        /// <summary>
        ///	获取分类列表
        /// </summary>
        /// <param name="idList">分类的ID，以","分隔</param>
        /// <returns>返回分类名称列表</returns>
        public string GetCategoryNameByIdList(string idList)
        {
            if (idList.ToString() != "")
            {
                string sql = "SELECT [title] FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [categoryid] IN (" + idList + ")";
                IDataReader categoryReader = DbHelper.ExecuteReader(CommandType.Text, sql);
                string categoryNameList = "";
                if (categoryReader != null)
                {
                    while (categoryReader.Read())
                    {
                        categoryNameList += categoryReader["title"].ToString() + ",";
                    }
                    categoryReader.Close();
                }
                if (categoryNameList == "")
                {
                    return "";
                }
                else
                {
                    return categoryNameList.Substring(0, categoryNameList.Length - 1);
                }
            }
            else
            {
                return "&nbsp;";
            }
        }


        /// <summary>
        ///	获取分类列表
        /// </summary>
        /// <param name="userid">用户的id</param>
        /// <returns>返回分类名称列表</returns>
        public IDataReader GetCategoryNameByUserID(int userid)
        {
            if (userid > 0)
            {
                string sql = "SELECT [categoryid], [title]  FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [uid] = " + userid;
                IDataReader categoryReader = DbHelper.ExecuteReader(CommandType.Text, sql);
                return categoryReader;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据用户id获取分类列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetSpaceCategoryListByUserId(int userid)
        {
            DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
            string sql = "SELECT [categoryid], [title], [description] FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [uid]=@userid ORDER BY [displayorder], [categoryid]";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
        }

        /// <summary>
        ///	获取分类列表
        /// </summary>
        /// <param name="idList">分类的ID, 以","分隔</param>
        /// <returns>返回分类名称列表</returns>
        public IDataReader GetCategoryIDAndName(string idList)
        {
            if (idList.Trim() == "")
            {
                return null;
            }

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT [categoryid],[title] FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [categoryid] IN (" + idList + ")");
            return reader;
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="categoryidList">删除分类的categoryid列表</param>
        /// <returns></returns>
        public bool DeleteSpaceCategory(string categoryidList, int userid)
        {
            if (!Utils.IsNumericList(categoryidList))
            { 
                return false; 
            }

            try
            {
                //清除分类的categoryid列表相关信息

                string sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [categoryid] IN (" + categoryidList + ") AND [uid]=" + userid;
                DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);

                //清除分类的categoryid关联表
                sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacepostcategories] WHERE [categoryid] IN (" + categoryidList + ")";
                DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpaceCategory(int userid)
        {
            try
            {
                //清除分类的categoryid列表相关信息
                string sqlstring = "SELECT [categoryid] FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [uid]=" + userid;
                DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, sqlstring).Tables[0];
                string categoryidList = "";
                foreach (DataRow dr in dt.Rows)
                {
                    categoryidList += dr["categoryid"].ToString();
                }
                if (categoryidList != "")
                {
                    categoryidList = categoryidList.Substring(0, categoryidList.Length - 1);
                    //清除分类的categoryid关联表
                    sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacepostcategories] WHERE [categoryid] IN (" + categoryidList + ")";
                    DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);
                }

                sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [uid]=" + userid;
                DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 返回指定页数与条件的分类列表
        /// </summary>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="currentPage">当前页号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public DataTable GetSpaceCategoryList(int pageSize, int currentPage, int userid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                int pageTop = (currentPage - 1) * pageSize;
                string sql = "";
                if (currentPage == 1)
                {
                    sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                        + "[" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE uid=@userid ORDER BY [categoryid] DESC";
                }
                else
                {
                    sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                        + "[" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [categoryid] < (SELECT min([categoryid])  FROM "
                        + "(SELECT TOP " + pageTop + " [categoryid] FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE "
                        + "[uid]=@userid ORDER BY [categoryid] DESC) AS tblTmp ) AND [uid]=@userid ORDER BY [categoryid] DESC";
                }
                return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 返回满足条件的分类数
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int GetSpaceCategoryCount(int userid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([categoryid]) FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [uid]=@userid", parm);
            }
            catch
            {
                return 0;
            }
        }


        #endregion


        #region 日志关联类型 操作类
        public bool AddSpacePostCategory(SpacePostCategoryInfo spacepostcategories)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4,spacepostcategories.ID),
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,spacepostcategories.PostID),
					DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,spacepostcategories.CategoryID)
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "spacepostcategories] ([postid], [categoryid]) VALUES ( @postid, @categoryid)");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}

        }

        public bool SaveSpacePostCategory(SpacePostCategoryInfo spacepostcategories)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4,spacepostcategories.ID),
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,spacepostcategories.PostID),
					DbHelper.MakeInParam("@categoryid", (DbType)SqlDbType.Int, 4,spacepostcategories.CategoryID)
				};
            string sqlstring = String.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "spacepostcategories] SET [postid] = @postid, [categoryid] = @categoryid WHERE  [id] = @id");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}

        }

        public bool DeleteSpacePostCategoryByPostID(int postid)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4,postid)
				};
            string sqlstring = String.Format("DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacepostcategories] WHERE [postid] = @postid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch
            //{
            //    return false;
            //}

        }

        #endregion


        #region 日志附件 操作类
        public bool AddSpaceAttachment(SpaceAttachmentInfo spaceattachments)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4,spaceattachments.AID),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,spaceattachments.UID),
					DbHelper.MakeInParam("@spacepostid", (DbType)SqlDbType.Int, 4,spaceattachments.SpacePostID),
					DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,spaceattachments.PostDateTime),
					DbHelper.MakeInParam("@filename", (DbType)SqlDbType.NChar, 100,spaceattachments.FileName),
					DbHelper.MakeInParam("@filetype", (DbType)SqlDbType.NChar, 50,spaceattachments.FileType),
					DbHelper.MakeInParam("@filesize", (DbType)SqlDbType.Float, 8,spaceattachments.FileSize),
					DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.NChar, 100,spaceattachments.Attachment),
					DbHelper.MakeInParam("@downloads", (DbType)SqlDbType.Int, 4,spaceattachments.Downloads),
					
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "spaceattachments] ( [uid], [spacepostid], [postdatetime], [filename], [filetype], [filesize], [attachment], [downloads]) VALUES ( @uid, @spacepostid, @postdatetime, @filename, @filetype, @filesize, @attachment, @downloads)");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }

        public bool SaveSpaceAttachment(SpaceAttachmentInfo spaceattachments)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4,spaceattachments.AID),
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4,spaceattachments.UID),
					DbHelper.MakeInParam("@spacepostid", (DbType)SqlDbType.Int, 4,spaceattachments.SpacePostID),
					DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8,spaceattachments.PostDateTime),
					DbHelper.MakeInParam("@filename", (DbType)SqlDbType.NChar, 100,spaceattachments.FileName),
					DbHelper.MakeInParam("@filetype", (DbType)SqlDbType.NChar, 50,spaceattachments.FileType),
					DbHelper.MakeInParam("@filesize", (DbType)SqlDbType.Float, 8,spaceattachments.FileSize),
					DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.NChar, 100,spaceattachments.Attachment),
					DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.Int, 4,spaceattachments.Downloads)
				};
            string sqlstring = String.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "spaceattachments]  SET [uid] = @uid, [spacepostid] = @spacepostid, [postdatetime] = @postdatetime, [filename] = @filename, [filetype] = @filetype, [filesize] = @filesize, [attachment] = @attachment, [downloads] = @downloads  WHERE [aid] = @aid");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }


        /// <summary>
        /// 返回指定页数与条件的分类列表
        /// </summary>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="currentPage">当前页号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public DataTable GetSpaceAttachmentList(int pageSize, int currentPage, int userid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                int pageTop = (currentPage - 1) * pageSize;
                string sql = "";
                if (currentPage == 1)
                {
                    sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                        + "[" + BaseConfigs.GetTablePrefix + "spaceattachments] WHERE [uid]=@userid ORDER BY [aid] DESC";
                }
                else
                {
                    sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                        + "[" + BaseConfigs.GetTablePrefix + "spaceattachments] WHERE [aid] < (SELECT min([aid])  FROM "
                        + "(SELECT TOP " + pageTop + " [aid] FROM [" + BaseConfigs.GetTablePrefix + "spaceattachments] WHERE "
                        + "[uid]=@userid ORDER BY [aid] DESC) AS tblTmp ) AND [uid]=@userid ORDER BY [aid] DESC";
                }
                return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 返回满足条件的分类数
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int GetSpaceAttachmentCount(int userid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([aid]) FROM [" + BaseConfigs.GetTablePrefix + "spaceattachments] WHERE [uid]=@userid", parm);
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// 删除指定的附件记录和相关文件
        /// </summary>
        /// <param name="aidlist">附件ID串, 格式:1,3,5</param>
        /// <returns></returns>
        public bool DeleteSpaceAttachmentByIDList(string aidlist, int userid)
        {

            if (!Utils.IsNumericList(aidlist))
            { 
                return false; 
            }

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT [filename] FROM [" + BaseConfigs.GetTablePrefix + "spaceattachments] WHERE [aid] IN( " + aidlist + " ) AND [uid]=" + userid, null);

            if (reader != null)
            {
                string path = Utils.GetMapPath(BaseConfigs.GetForumPath);
                while (reader.Read())
                {
                    try
                    {
                        System.IO.File.Delete(path + reader[0].ToString());
                    }
                    catch
                    { ;}
                }
                reader.Close();
            }

            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE  FROM  [" + BaseConfigs.GetTablePrefix + "spaceattachments] WHERE [aid] IN( " + aidlist + " )", null);

            return true;
        }

        #endregion


        #region 友情链接 操作类

        /// <summary>
        /// 返回满足条件的友情链接数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetSpaceLinkCount(int userid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([linkid]) FROM [" + BaseConfigs.GetTablePrefix + "spacelinks] WHERE [userid]=@userid", parm);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回指定页数与条件的友情链接列表
        /// </summary>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="currentPage">当前页号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public DataTable GetSpaceLinkList(int pageSize, int currentPage, int userid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                int pageTop = (currentPage - 1) * pageSize;
                string sql = "";
                if (currentPage == 1)
                {
                    sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                        + "[" + BaseConfigs.GetTablePrefix + "spacelinks] WHERE [userid]=@userid ORDER BY [linkid] DESC";
                }
                else
                {
                    sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                        + "[" + BaseConfigs.GetTablePrefix + "spacelinks] WHERE [linkid] < (SELECT min([linkid])  FROM "
                        + "(SELECT TOP " + pageTop + " [linkid] FROM [" + BaseConfigs.GetTablePrefix + "spacelinks] WHERE "
                        + "[userid]=@userid ORDER BY [linkid] DESC) AS tblTmp ) AND [userid]=@userid ORDER BY [linkid] DESC";
                }
                return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

        public IDataReader GetSpaceLinkByLinkID(int linkid)
        {
            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "spacelinks] WHERE [linkid] = " + linkid);
            return reader;
        }

        public bool SaveSpaceLink(SpaceLinkInfo spacelinks)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@linkid", (DbType)SqlDbType.Int, 4,spacelinks.LinkId),
					DbHelper.MakeInParam("@linktitle", (DbType)SqlDbType.NVarChar, 50,spacelinks.LinkTitle),
					DbHelper.MakeInParam("@linkurl", (DbType)SqlDbType.VarChar,255,spacelinks.LinkUrl),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NVarChar, 200,spacelinks.Description),
				};
            string sqlstring = String.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "spacelinks] SET  [linktitle] = @linktitle, [linkurl] = @linkurl, [description] = @description WHERE [linkid] = @linkid ");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}

        }

        public bool AddSpaceLink(SpaceLinkInfo spacelinks)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@linkid", (DbType)SqlDbType.Int, 4,spacelinks.LinkId),
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,spacelinks.UserId),
					DbHelper.MakeInParam("@linktitle", (DbType)SqlDbType.NVarChar, 50,spacelinks.LinkTitle),
					DbHelper.MakeInParam("@linkurl", (DbType)SqlDbType.VarChar,255,spacelinks.LinkUrl),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NVarChar, 200,spacelinks.Description),
				};
            string sqlstring = String.Format("INSERT INTO [" + BaseConfigs.GetTablePrefix + "spacelinks] ( [userid], [linktitle], [linkurl], [description]) VALUES ( @userid, @linktitle, @linkurl,  @description)");

            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}

        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="linksList">删除友情链接的linkid列表</param>
        /// <returns></returns>
        public bool DeleteSpaceLink(string linksList, int userid)
        {
            
            if (!Utils.IsNumericList(linksList))
            {
                return false;
            }

            try
            {
                    string sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacelinks] WHERE [linkid] IN (" + linksList + ") AND userid=" + userid;
                    DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);

                    return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpaceLink(int userid)
        {
            try
            {
                string sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacelinks] WHERE userid=" + userid;
                DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion



        public string GetThemeDropDownTreeSql()
        {
            return "SELECT [themeid], [name], [type] AS [parentid] FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] ORDER BY [themeid]";
        }

        public string GetTemplateDropDownSql()
        {
            return "SELECT [templateid], [name] FROM [" + BaseConfigs.GetTablePrefix + "spacetemplates] ORDER BY [templateid]";
        }

        public string GetCategoryCheckListSql(int userid)
        {
            return "SELECT [categoryid], [title] FROM [" + BaseConfigs.GetTablePrefix + "spacecategories] WHERE [uid]=" + userid + " ORDER BY [displayorder], [categoryid]";
        }

        #region 对ThemeInfo的操作
        public IDataReader GetThemeInfos()
        {
            string sql = string.Format(@"SELECT * FROM [{0}spacethemes] ORDER BY [type]", BaseConfigs.GetTablePrefix);

            return RunSelectSql(sql, null);
        }

        public IDataReader GetThemeInfoById(int themeId)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4, themeId)
			};

            string sql = string.Format(@"SELECT * FROM [{0}spacethemes] WHERE [themeid] = @themeid", BaseConfigs.GetTablePrefix);

            return RunSelectSql(sql, parms);
        }

        #endregion

        #region 对spacemoduledefs表的操作
        public IDataReader GetModuleDefInfoById(int moduleDefInfoId)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduledefid", (DbType)SqlDbType.Int, 4, moduleDefInfoId)	
			};

            string sql = string.Format(@"SELECT * FROM [{0}spacemoduledefs] WHERE [moduledefid] = @moduledefid", BaseConfigs.GetTablePrefix);

            return RunSelectSql(sql, parms);
        }


        /// <summary>
        /// 添加ModuleDef信息至数据库
        /// </summary>
        /// <param name="moduleDefInfo"></param>
        /// <returns></returns>
        public bool AddModuleDef(ModuleDefInfo moduleDefInfo)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@modulename", (DbType)SqlDbType.NVarChar, 20, moduleDefInfo.ModuleName),
				DbHelper.MakeInParam("@cachetime", (DbType)SqlDbType.Int, 4, moduleDefInfo.CacheTime),
				DbHelper.MakeInParam("@configfile", (DbType)SqlDbType.VarChar, 100, moduleDefInfo.ConfigFile),
				DbHelper.MakeInParam("@controller", (DbType)SqlDbType.VarChar, 255, moduleDefInfo.BussinessController),
			};

            string sql = string.Format(@"INSERT INTO [{0}spacemoduledefs]([modulename], [cachetime], [configfile], [controller]) VALUES(@moduledefid, @modulename, @cachetime, @configfile, @controller)", BaseConfigs.GetTablePrefix);
            return RunExecuteSql(sql, parms);
        }

        /// <summary>
        /// 修改指定的ModuleDef信息
        /// </summary>
        /// <param name="moduleDefInfo"></param>
        /// <returns></returns>
        public bool UpdateModuleDef(ModuleDefInfo moduleDefInfo)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduledefid", (DbType)SqlDbType.Int, 4, moduleDefInfo.ModuleDefID),
				DbHelper.MakeInParam("@modulename", (DbType)SqlDbType.NVarChar, 20, moduleDefInfo.ModuleName),
				DbHelper.MakeInParam("@cachetime", (DbType)SqlDbType.Int, 4, moduleDefInfo.CacheTime),
				DbHelper.MakeInParam("@configfile", (DbType)SqlDbType.VarChar, 100, moduleDefInfo.ConfigFile),
				DbHelper.MakeInParam("@controller", (DbType)SqlDbType.VarChar, 255, moduleDefInfo.BussinessController),
			};

            string sql = string.Format(@"UPDATE [{0}spacemoduledefs] SET [modulename]=@modulename, [cachetime]=@cachetime, [configfile]=@configfile, [controller]=@controller WHERE [moduledefid]=@moduledefid", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }

        /// <summary>
        /// 删除指定的ModuleDef信息
        /// </summary>
        /// <param name="moduleDefId"></param>
        /// <returns></returns>
        public bool DeleteModuleDef(int moduleDefId)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduledefid", (DbType)SqlDbType.Int, 4, moduleDefId),
			};

            string sql = string.Format(@"DELETE FROM [{0}spacemoduledefs] WHERE [moduledefid]=@moduledefid", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }


        public int GetModuleDefIdByUrl(string url)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@configfile", (DbType)SqlDbType.VarChar, 100, url),
			};

            string commandText = string.Format(@"SELECT [moduledefid] FROM [{0}spacemoduledefs] WHERE [configfile]=@configfile", BaseConfigs.GetTablePrefix);

            string str = DbHelper.ExecuteScalarToStr(CommandType.Text, commandText, parms);
            return str == string.Empty ? 0 : Convert.ToInt32(str);
        }

        #endregion

        #region 对spacemodules表的操作

        public int GetModulesCountByTabId(int tabId, int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabId),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)
			};

            string sql = string.Format(@"SELECT COUNT(1) FROM [{0}spacemodules] WHERE [tabid] = @tabid AND [uid]=@uid", BaseConfigs.GetTablePrefix);

            int reval = Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parms), 0);

            return reval;
        }

        /// <summary>
        /// 根据TabId获得Modules集合
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public IDataReader GetModulesByTabId(int tabId, int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabId),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)	
			};

            string sql = string.Format(@"SELECT * FROM [{0}spacemodules] WHERE [tabid] = @tabid AND [uid]=@uid ORDER BY [panename], [displayorder]", BaseConfigs.GetTablePrefix);

            return RunSelectSql(sql, parms);
        }


        /// <summary>
        /// 根据ModuleId获得Module
        /// </summary>
        /// <param name="moduleInfoId"></param>
        /// <returns></returns>
        public IDataReader GetModuleInfoById(int moduleInfoId, int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleInfoId),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)
			};

            string sql = string.Format(@"SELECT * FROM [{0}spacemodules] WHERE [moduleid] = @moduleid AND [uid]=@uid", BaseConfigs.GetTablePrefix);

            return RunSelectSql(sql, parms);
        }


        /// <summary>
        /// 添加Module至数据库
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        public bool AddModule(ModuleInfo moduleInfo)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleInfo.ModuleID),
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, moduleInfo.TabID),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, moduleInfo.Uid),
				DbHelper.MakeInParam("@moduledefid", (DbType)SqlDbType.Int, 4, moduleInfo.ModuleDefID),
				DbHelper.MakeInParam("@panename", (DbType)SqlDbType.VarChar, 10, moduleInfo.PaneName),
				DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, moduleInfo.DisplayOrder),
				DbHelper.MakeInParam("@userpref", (DbType)SqlDbType.NVarChar, 4000, moduleInfo.UserPref),
				DbHelper.MakeInParam("@val", (DbType)SqlDbType.TinyInt, 1, moduleInfo.Val),
				DbHelper.MakeInParam("@moduleurl", (DbType)SqlDbType.VarChar, 255, moduleInfo.ModuleUrl),
				DbHelper.MakeInParam("@moduletype", (DbType)SqlDbType.TinyInt, 2, moduleInfo.ModuleType)
			};

            string sql = string.Format(@"INSERT INTO [{0}spacemodules]([moduleid], [tabid], [uid], [moduledefid], [panename], [displayorder], [userpref], [val], [moduleurl], [moduletype]) VALUES(@moduleid, @tabid, @uid, @moduledefid, @panename, @displayorder, @userpref, @val, @moduleurl, @moduletype)", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }

        /// <summary>
        /// 更新指定的Module信息
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        public bool UpdateModule(ModuleInfo moduleInfo)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleInfo.ModuleID),
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, moduleInfo.TabID),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, moduleInfo.Uid),
				DbHelper.MakeInParam("@moduledefid", (DbType)SqlDbType.Int, 4, moduleInfo.ModuleDefID),
				DbHelper.MakeInParam("@panename", (DbType)SqlDbType.VarChar, 10, moduleInfo.PaneName),
				DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, moduleInfo.DisplayOrder),
				DbHelper.MakeInParam("@userpref", (DbType)SqlDbType.NVarChar, 4000, moduleInfo.UserPref),
				DbHelper.MakeInParam("@val", (DbType)SqlDbType.TinyInt, 1, moduleInfo.Val),
				DbHelper.MakeInParam("@moduleurl", (DbType)SqlDbType.VarChar, 255, moduleInfo.ModuleUrl),
				DbHelper.MakeInParam("@moduletype", (DbType)SqlDbType.TinyInt, 2, moduleInfo.ModuleType)
			};

            string sql = string.Format(@"UPDATE [{0}spacemodules] SET [tabid]=@tabid, [moduledefid]=@moduledefid, [panename]=@panename, [displayorder]=@displayorder,[userpref]=@userpref,[val]=@val, moduleurl=@moduleurl, moduletype=@moduletype WHERE [moduleid]=@moduleid AND [uid]=@uid", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }

        /// <summary>
        /// 删除指定的Module信息
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public bool DeleteModule(int moduleId, int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleId),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)
			};

            string sql = string.Format(@"DELETE FROM [{0}spacemodules] WHERE [moduleid]=@moduleid AND [uid]=@uid", BaseConfigs.GetTablePrefix);
            return RunExecuteSql(sql, parms);
        }

        public bool DeleteModule(int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)
			};

            string sql = string.Format(@"DELETE FROM [{0}spacemodules] WHERE [uid]=@uid", BaseConfigs.GetTablePrefix);
            return RunExecuteSql(sql, parms);
        }

        /// <summary>
        /// 为模块排序
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="panename"></param>
        /// <param name="displayorder"></param>
        public void UpdateModuleOrder(int mid, int uid, string panename, int displayorder)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, mid),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
				DbHelper.MakeInParam("@panename", (DbType)SqlDbType.VarChar, 10, panename),
				DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayorder)
			};
            string commandText = string.Format(@"UPDATE [{0}spacemodules] SET [panename]=@panename, [displayorder]=@displayorder WHERE [moduleid]=@moduleid AND [uid]=@uid", BaseConfigs.GetTablePrefix);
            RunExecuteSql(commandText, parms);
        }

        public void UpdateModuleTab(int moduleid, int uid, int tabid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleid),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabid)
			};
            string commandText = string.Format(@"UPDATE [{0}spacemodules] SET [displayorder]=0, [tabid]=@tabid WHERE [moduleid]=@moduleid AND [uid]=@uid", BaseConfigs.GetTablePrefix);
            RunExecuteSql(commandText, parms);
        }

        public int GetMaxModuleIdByUid(int userid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, userid)
			};
            string commandText = string.Format(@"SELECT TOP 1 [moduleid] FROM [{0}spacemodules] WHERE [uid]=@uid ORDER BY [moduleid] DESC", BaseConfigs.GetTablePrefix);
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        #endregion

        #region 对spacetabs表的操作

        /// <summary>
        /// 根据Uid获得Tab集合
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetTabInfosByUid(int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)
			};

            string sql = string.Format(@"SELECT * FROM [{0}spacetabs] WHERE [uid]=@uid ORDER BY [tabid] ASC", BaseConfigs.GetTablePrefix);

            return RunSelectSql(sql, parms);
        }

        /// <summary>
        /// 根据TabId获得Tab
        /// </summary>
        /// <param name="tabInfoId"></param>
        /// <returns></returns>
        public IDataReader GetTabInfoById(int tabInfoId, int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabInfoId),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)	
			};

            string sql = string.Format(@"SELECT * FROM [{0}spacetabs] WHERE [tabid] = @tabid AND [uid]=@uid", BaseConfigs.GetTablePrefix);

            return RunSelectSql(sql, parms);
        }

        /// <summary>
        /// 添加Tab信息至数据库
        /// </summary>
        /// <param name="tabInfo"></param>
        /// <returns></returns>
        public bool AddTab(TabInfo tabInfo)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabInfo.TabID),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, tabInfo.UserID),
				DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, tabInfo.DisplayOrder),
				DbHelper.MakeInParam("@tabname", (DbType)SqlDbType.NVarChar, 50, tabInfo.TabName),
				DbHelper.MakeInParam("@iconfile", (DbType)SqlDbType.VarChar, 50, tabInfo.IconFile),
				DbHelper.MakeInParam("@template", (DbType)SqlDbType.VarChar, 50, tabInfo.Template)
			};

            string sql = string.Format(@"INSERT INTO [{0}spacetabs]([tabid], [uid], [displayorder], [tabname], [iconfile], [template]) VALUES(@tabid, @uid, @displayorder, @tabname, @iconfile, @template)", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }

        /// <summary>
        /// 更新指定Tab信息
        /// </summary>
        /// <param name="tabInfo"></param>
        /// <returns></returns>
        public bool UpdateTab(TabInfo tabInfo)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabInfo.TabID),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, tabInfo.UserID),
				DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, tabInfo.DisplayOrder),
				DbHelper.MakeInParam("@tabname", (DbType)SqlDbType.NVarChar, 50, tabInfo.TabName),
				DbHelper.MakeInParam("@iconfile", (DbType)SqlDbType.VarChar, 50, tabInfo.IconFile),
				DbHelper.MakeInParam("@template", (DbType)SqlDbType.VarChar, 50, tabInfo.Template)
			};

            string sql = string.Format(@"UPDATE [{0}spacetabs] SET [displayorder]=@displayorder, [tabname]=@tabname, [iconfile]=@iconfile, [template] = @template WHERE [tabid]=@tabid AND [uid]=@uid", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }

        /// <summary>
        /// 删除Tab信息
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public bool DeleteTab(int tabId, int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabId),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
			};

            string sql = string.Format(@"DELETE FROM [{0}spacetabs] WHERE [tabid]=@tabid AND [uid]=@uid", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }

        public bool DeleteTab(int uid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
			};

            string sql = string.Format(@"DELETE FROM [{0}spacetabs] WHERE [uid]=@uid", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }

        public int GetTabInfoCountByUserId(int userid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, userid),
			};
            string commandText = string.Format(@"SELECT COUNT(1) FROM [{0}spacetabs] WHERE [uid]=@uid", BaseConfigs.GetTablePrefix);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms), 0);
        }

        public bool SetTabTemplate(int tabid, int uid, string template)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabid),
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
				DbHelper.MakeInParam("@template", (DbType)SqlDbType.VarChar, 50, template)
			};

            string sql = string.Format(@"UPDATE [{0}spacetabs] SET [template] = @template WHERE [tabid]=@tabid AND [uid]=@uid", BaseConfigs.GetTablePrefix);

            return RunExecuteSql(sql, parms);
        }

        public int GetMaxTabIdByUid(int userid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, userid)
			};
            string commandText = string.Format(@"SELECT TOP 1 [tabid] FROM [{0}spacetabs] WHERE [uid]=@uid ORDER BY [tabid] DESC", BaseConfigs.GetTablePrefix);
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        #endregion

        #region config

        public void ClearDefaultTab(int userid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid)
			};
            string commandText = string.Format("UPDATE [{0}spaceconfigs] SET [defaulttab]=0 WHERE [userid]=@userid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);

        }
        public void SetDefaultTab(int userid, int tabid)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
				DbHelper.MakeInParam("@tabid", (DbType)SqlDbType.Int, 4, tabid)
			};
            string commandText = string.Format("UPDATE [{0}spaceconfigs] SET [defaulttab]=@tabid WHERE [userid]=@userid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public void SetSpaceTheme(int userid, int themeid, string themepath)
        {
            DbParameter[] parms =
			{
				DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
				DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4, themeid),
				DbHelper.MakeInParam("@themepath", (DbType)SqlDbType.VarChar, 50, themepath)
			};
            string commandText = string.Format("UPDATE [{0}spaceconfigs] SET [themeid]=@themeid, [themepath]=@themepath WHERE [userid]=@userid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #endregion

        /// <summary>
        /// 运行非Select语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        private bool RunExecuteSql(string sql, DbParameter[] parms)
        {
            try
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 运行Select语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        private IDataReader RunSelectSql(string sql, DbParameter[] parms)
        {
            return DbHelper.ExecuteReader(CommandType.Text, sql, parms);
        }

        public DataRow GetThemes()
        {
            string sql = "SELECT TOP 1 newid() AS row,[themeid],[directory] FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE type<>0 ORDER BY row";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0].Rows[0];
        }

        public DataTable GetUnActiveSpaceList()
        {
            string sql = "SELECT [uid],s.[spaceid],[spacetitle],[username],[createdatetime] FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s ";
            sql += "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "users] u ON s.[userid]=u.[uid] ";
            sql += "WHERE s.[spaceid] IN (SELECT ABS([spaceid]) spaceid  FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [spaceid] < 0) ORDER BY s.[spaceid] DESC";
            return DbHelper.ExecuteDataset(sql).Tables[0];
        }

        public void DeleteSpaces(string uidlist)
        {
            DbHelper.ExecuteNonQuery("DELETE FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] WHERE [userid] IN (" + uidlist + ")");
        }

        public void DeleteSpaceThemes(string themeidlist)
        {
            DbHelper.ExecuteNonQuery("DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacethemes]  WHERE [themeid] IN(" + themeidlist + ")");
        }

        public void UpdateSpaceThemeInfo(int themeid, string name, string author, string copyright)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 50, name),
                                        DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 100, author),
                                        DbHelper.MakeInParam("@copyright", (DbType)SqlDbType.NVarChar, 100, copyright),
                                        DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4, themeid)
                                    };
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "spacethemes] SET [name]=@name, [author]=@author, [copyright]=@copyright WHERE themeid=@themeid";

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public DataTable GetSpaceThemeDirectory()
        {
            return DbHelper.ExecuteDataset("SELECT [directory] FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE [type]<>0").Tables[0];
        }

        public bool IsThemeExist(string name)
        {
            DbParameter parm = DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 50, name);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE name=@name", parm), 0) > 0;
        }

        public bool IsThemeExist(string name, int themeid)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 50, name),
                                    DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, themeid)
                                };
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE [name]=@name AND themeid<>@id", parms), 0) > 0;
        }

        public void AddSpaceTheme(string directory, string name, int type, string author, string createdate, string copyright)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@directory", (DbType)SqlDbType.VarChar, 100, directory),
                                        DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 50, name),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 50, type),
                                        DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 100, author),
                                        DbHelper.MakeInParam("@createdate", (DbType)SqlDbType.NVarChar, 50, createdate),
                                        DbHelper.MakeInParam("@copyright", (DbType)SqlDbType.NVarChar, 100, copyright)
                                    };
            string sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "spacethemes]([directory], [name], [type], [author], [createdate], [copyright]) VALUES(@directory,@name,@type,@author,@createdate,@copyright)";

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public void UpdateThemeName(int themeid, string name)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4, themeid),
                                        DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 50, name)
                                    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "spacethemes] SET name=@name WHERE themeid=@themeid", parms);
        }

        public void DeleteTheme(int themeid)
        {
            DbParameter parm = DbHelper.MakeInParam("@themeid", (DbType)SqlDbType.Int, 4, themeid);
            string sql = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE [themeid]=@themeid OR [type]=@themeid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parm);
        }

        #region PhotoComment

        public IDataReader GetPhotoCommentCollection(int photoid)
        {
            string commandText = "SELECT * FROM[" + BaseConfigs.GetTablePrefix + "photocomments] WHERE [photoid]=" + photoid + " ORDER BY [commentid] ASC";
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public void CreatePhotoComment(PhotoCommentInfo pcomment)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, pcomment.Userid),
                                        DbHelper.MakeInParam("@username", (DbType)SqlDbType.NVarChar, 20, pcomment.Username),
                                        DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4, pcomment.Photoid),
                                        DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.SmallDateTime, 4, pcomment.Postdatetime),
                                        DbHelper.MakeInParam("@ip", (DbType)SqlDbType.VarChar, 100, pcomment.Ip),
                                        DbHelper.MakeInParam("@content", (DbType)SqlDbType.NVarChar, 2000, pcomment.Content)
                                    };
            string commandText = string.Format("INSERT INTO [{0}photocomments]([userid], [username], [photoid], [postdatetime], [ip], [content]) VALUES(@userid, @username, @photoid, @postdatetime, @ip, @content)", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除图片评论
        /// </summary>
        /// <param name="commentid">评论Id</param>
        public void DeletePhotoComment(int commentid)
        {
            string commandText = string.Format("DELETE FROM [{0}photocomments] WHERE [commentid]={1}", BaseConfigs.GetTablePrefix, commentid);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }
        #endregion

        public DataTable GetSpaceList(int pagesize, int currentpage, string username, string dateStart, string dateEnd)
        {
            int pagetop = (currentpage - 1) * pagesize;
            DbParameter[] parms = {
				DbHelper.MakeInParam("@dateStart", (DbType)SqlDbType.DateTime, 8, dateStart),
				DbHelper.MakeInParam("@dateEnd", (DbType)SqlDbType.DateTime, 8, dateEnd)
                                  };
            string condition = GetSpaceListCondition(username, dateStart, dateEnd);
            string sqlstring;
            if (currentpage == 1)
            {
                sqlstring = "SELECT TOP " + pagesize.ToString() + " s.[spaceid],[userid],[spacetitle],[username],[grouptitle],[postcount],[commentcount],[createdatetime],[status] ";
                sqlstring += "FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s ";
                sqlstring += "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "users] u ON s.userid=u.uid  ";
                sqlstring += "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "usergroups] g ON u.[groupid]=g.[groupid] ";
                if (condition != "")
                    sqlstring += "WHERE " + condition + " ";
                sqlstring += "ORDER BY s.spaceid DESC";
            }
            else
            {
                sqlstring = "SELECT TOP " + pagesize.ToString() + " s.[spaceid],[userid],[spacetitle],[username],[grouptitle],[postcount],[commentcount],[createdatetime],[status] ";
                sqlstring += "FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s ";
                sqlstring += "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "users] u ON s.[userid]=u.[uid] ";
                sqlstring += "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "usergroups] g ON u.[groupid]=g.[groupid] ";
                sqlstring += "WHERE s.[spaceid]<(SELECT MIN([spaceid]) FROM (SELECT TOP " + pagetop + " [spaceid] FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] ";
                sqlstring += "ORDER BY [spaceid] DESC) AS tblTmp) ";
                if (condition != "")
                    sqlstring += "AND " + condition + " ";
                sqlstring += "ORDER BY s.[spaceid] DESC";

            }
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];

        }

        private string GetSpaceListCondition(string username, string dateStart, string dateEnd)
        {
            string condition = " 1=1 ";
            if (username != "")
                condition += " AND u.[username] LIKE'%" + RegEsc(username) + "%'";
            if (dateStart != "")
                condition += " AND s.[createdatetime] >=@dateStart";
            if (dateEnd != "")
                condition += " AND s.[createdatetime] <=@dateEnd";
            return condition;
        }

        public void AdminOpenSpaceStatusBySpaceidlist(string spaceidlist)
        {
            DbHelper.ExecuteNonQuery("UPDATE [" + BaseConfigs.GetTablePrefix + "spaceconfigs] SET [status]=[status]&~" + (int)SpaceStatusType.AdminClose + "  WHERE [spaceid] IN (" + spaceidlist + ")");
        }

        public void AdminCloseSpaceStatusBySpaceidlist(string spaceidlist)
        {
            DbHelper.ExecuteNonQuery("UPDATE [" + BaseConfigs.GetTablePrefix + "spaceconfigs] SET [status]=[status]|" + (int)SpaceStatusType.AdminClose + "  WHERE [spaceid] IN (" + spaceidlist + ")");
        }

        public int GetSpaceRecordCount(string username, string dateStart, string dateEnd)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@dateStart", (DbType)SqlDbType.DateTime, 8, dateStart),
				DbHelper.MakeInParam("@dateEnd", (DbType)SqlDbType.DateTime, 8, dateEnd)
                                  };

            string condition = GetSpaceListCondition(username, dateStart, dateEnd);
            string sqlstring = "SELECT COUNT(s.[spaceid]) FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s LEFT JOIN [" + BaseConfigs.GetTablePrefix + "users] u ON s.[userid]=u.[uid] ";
            if (condition != "")
                sqlstring += " WHERE " + condition;
            return Convert.ToInt32(DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0].Rows[0][0].ToString());
        }

        public bool IsRewritenameExist(string rewriteName)
        {
            DbParameter parm = DbHelper.MakeInParam("@rewritename", (DbType)SqlDbType.Char, 100, rewriteName);
            string sql = string.Format("SELECT COUNT(1) FROM [{0}spaceconfigs] WHERE [rewritename]=@rewritename", BaseConfigs.GetTablePrefix);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parm), 0) > 0;
        }

        public void UpdateUserSpaceRewriteName(int userid, string rewritename)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                DbHelper.MakeInParam("@rewritename", (DbType)SqlDbType.Char, 100, rewritename)
            };

            string sql = string.Format("UPDATE [{0}spaceconfigs] SET [rewritename]=@rewritename WHERE [userid]=@userid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public string GetUidBySpaceid(string spaceid)
        {
            DbParameter parm = DbHelper.MakeInParam("@spaceid", (DbType)SqlDbType.Int, 4, spaceid);
            string sql = string.Format("SELECT [userid] FROM [{0}spaceconfigs] WHERE [spaceid]=@spaceid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteScalar(CommandType.Text, sql, parm).ToString();
        }

        public string GetSpaceattachmentsAidListByUid(int uid)
        {
            string aidlist = "";
            DbParameter parm = DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid);
            string sql = string.Format("SELECT [aid] FROM [{0}spaceattachments] WHERE [uid]=@uid", BaseConfigs.GetTablePrefix);
            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
            if (dt.Rows.Count == 0)
                return "";
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    aidlist += dr["aid"].ToString() + ",";
                }
                if (aidlist != "")
                    aidlist = aidlist.Substring(0, aidlist.Length - 1);
            }
            return aidlist;
        }

        public void DeleteSpaceByUid(int uid)
        {
            DbParameter parm = DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid);
            string sql = string.Format("DELETE FROM [{0}spaceconfigs] WHERE [userid]=@uid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parm);
        }

        public void UpdateCustomizePanelContent(int moduleid, int userid, string modulecontent)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleid),                
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                DbHelper.MakeInParam("@modulecontent", (DbType)SqlDbType.NText, 0, modulecontent)
            };

            string sql = string.Format("UPDATE [{0}spacecustomizepanels] SET [panelcontent]=@modulecontent WHERE [moduleid]=@moduleid AND [userid]=@userid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public bool ExistCustomizePanelContent(int moduleid, int userid)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleid),
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid)
            };

            string sql = string.Format("SELECT COUNT(1) FROM [{0}spacecustomizepanels] WHERE [moduleid]=@moduleid AND [userid]=@userid", BaseConfigs.GetTablePrefix);

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parms), 0) > 0;
        }

        public void AddCustomizePanelContent(int moduleid, int userid, string modulecontent)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleid),
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                DbHelper.MakeInParam("@modulecontent", (DbType)SqlDbType.NText, 0, modulecontent)
            };

            string sql = string.Format("INSERT INTO [{0}spacecustomizepanels]([moduleid], [userid], [panelcontent]) VALUES(@moduleid, @userid, @modulecontent)", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public object GetCustomizePanelContent(int moduleid, int userid)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleid),
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid)
            };

            string sql = string.Format("SELECT [panelcontent] FROM [{0}spacecustomizepanels] WHERE [moduleid]=@moduleid AND [userid]=@userid", BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
        }

        public void DeleteCustomizePanelContent(int moduleid, int userid)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@moduleid", (DbType)SqlDbType.Int, 4, moduleid),
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid)
            };

            string sql = string.Format("DELETE FROM [{0}spacecustomizepanels] WHERE [moduleid]=@moduleid AND [userid]=@userid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public IDataReader GetModulesByUserId(int uid)
        {
            DbParameter parm = DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid);

            string sql = string.Format("SELECT * FROM [{0}spacemodules] WHERE [uid]=@uid", BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteReader(CommandType.Text, sql, parm);
        }


        public string GetSapceThemeList(int themeid)
        {
            return "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE [type]=" + themeid;
        }

        public string DeleteSpaceThemeByThemeid(int themeid)
        {
            return "DELETE FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE [themeid]=" + themeid;
        }

        public IDataReader GetModuleDefList()
        {
            string sql = string.Format("SELECT * FROM [{0}spacemoduledefs]", BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        public void UpdateModuleDefInfo(string configfile, string controller)
        {
            string sql = string.Format("UPDATE [{0}spacemoduledefs] SET [controller]='{1}' WHERE [configfile]='{2}'", BaseConfigs.GetTablePrefix, controller, configfile);
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }


        public void AddModuleDefInfo(ModuleDefInfo mdi)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@modulename", (DbType)SqlDbType.NVarChar, 20, mdi.ModuleName),
                                        DbHelper.MakeInParam("@cachetime", (DbType)SqlDbType.Int, 4, mdi.CacheTime),
                                        DbHelper.MakeInParam("@configfile", (DbType)SqlDbType.VarChar, 100, mdi.ConfigFile),
                                        DbHelper.MakeInParam("@controller", (DbType)SqlDbType.VarChar, 255, mdi.BussinessController)
            };

            string sql = string.Format("INSERT INTO [{0}spacemoduledefs]([modulename], [cachetime], [configfile], [controller]) VALUES(@modulename, @cachetime, @configfile, @controller)", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);

        }


        public void DeleteModuleDefByUrl(string url)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@configfile", (DbType)SqlDbType.VarChar, 100, url)
            };

            string sql = string.Format("DELETE FROM [{0}spacemoduledefs] WHERE [configfile] = @configfile", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public DataTable GetSearchSpacePostsList(int pagesize, string postids)
        {
            string commandText = string.Format("SELECT TOP {1} [{0}spaceposts].[postid], [{0}spaceposts].[title], [{0}spaceposts].[author], [{0}spaceposts].[uid], [{0}spaceposts].[postdatetime], [{0}spaceposts].[commentcount], [{0}spaceposts].[views] FROM [{0}spaceposts] WHERE [{0}spaceposts].[postid] IN({2}) ORDER BY CHARINDEX(CONVERT(VARCHAR(8),[{0}spaceposts].[postid]),'{2}')", BaseConfigs.GetTablePrefix, pagesize, postids);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public DataTable GetSearchAlbumList(int pagesize, string albumids)
        {
            string commandText = string.Format("SELECT TOP {1} [{0}albums].[albumid], [{0}albums].[title], [{0}albums].[username], [{0}albums].[userid], [{0}albums].[createdatetime], [{0}albums].[imgcount], [{0}albums].[views], [{0}albumcategories].[albumcateid],[{0}albumcategories].[title] AS [categorytitle] FROM [{0}albums] LEFT JOIN [{0}albumcategories] ON [{0}albumcategories].[albumcateid] = [{0}albums].[albumcateid] WHERE [{0}albums].[albumid] IN({2}) ORDER BY CHARINDEX(CONVERT(VARCHAR(8),[{0}albums].[albumid]),'{2}')", BaseConfigs.GetTablePrefix, pagesize, albumids);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public int GetSpaceAttachmentSizeByUserid(int userid)
        {
            string sql = "SELECT ISNULL(SUM(filesize), 0) AS [filesize] FROM [" + BaseConfigs.GetTablePrefix + "spaceattachments] WHERE uid=" + userid;
            //object o = DbHelper.ExecuteScalar(CommandType.Text,sql);
            return (int)DbHelper.ExecuteScalar(CommandType.Text, sql);
        }

        public string GetSpaceThemes()
        {
            return "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "spacethemes] WHERE [type]=0";
        }

        public void CreateSpacePostTags(string tags, int postid, int userid, string postdatetime)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@tags", (DbType)SqlDbType.NVarChar, 55, tags),
                DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4, postid),
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, postdatetime)                
            };

            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}createspaceposttags", BaseConfigs.GetTablePrefix), parms);
        }

        public IDataReader GetTagsListBySpacePost(int postid)
        {
            DbParameter parm = DbHelper.MakeInParam("@postid", (DbType)SqlDbType.Int, 4, postid);

            string sql = string.Format("SELECT [{0}tags].* FROM [{0}tags], [{0}spaceposttags] WHERE [{0}spaceposttags].[tagid] = [{0}tags].[tagid] AND [{0}spaceposttags].[spacepostid] = @postid ORDER BY [orderid]", BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteReader(CommandType.Text, sql, parm);
        }

        public int GetSpacePostCountWithSameTag(int tagid)
        {
            DbParameter parm = DbHelper.MakeInParam("@tagid", (DbType)SqlDbType.Int, 4, tagid);

            string sql = string.Format("SELECT COUNT(1) FROM [{0}spaceposttags] AS [spt],[{0}spaceposts] AS [sp] WHERE [spt].[spacepostid] = [sp].[postid] AND [sp].[poststatus] = 1 AND [tagid] = @tagid", BaseConfigs.GetTablePrefix);

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parm), 0);
        }

        public IDataReader GetSpacePostsWithSameTag(int tagid, int pageindex, int pagesize)
        {
            string sql = string.Format("{0}getspacepostlistbytag", BaseConfigs.GetTablePrefix);
            DbParameter[] parms = {
                DbHelper.MakeInParam("@tagid", (DbType)SqlDbType.Int, 4, tagid),
                DbHelper.MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, pageindex),
                DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int, 4, pagesize)
            };

            return DbHelper.ExecuteReader(CommandType.StoredProcedure, sql, parms);
        }

        public IDataReader GetHotTagsListForSpace(int count)
        {
            string sql = string.Format("SELECT TOP {0} * FROM [{1}tags] WHERE [scount] > 0 ORDER BY [scount] DESC,[orderid]", count, BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        public void DeleteSpacePostTags(int spacepostid)
        {
            DbParameter parm = DbHelper.MakeInParam("@spacepostid", (DbType)SqlDbType.Int, 4, spacepostid);
            string sql = string.Format("{0}deletespaceposttags", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, parm);
        }

        public void CreatePhotoTags(string tags, int photoid, int userid, string postdatetime)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@tags", (DbType)SqlDbType.NVarChar, 55, tags),
                DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4, photoid),
                DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, postdatetime)                
            };

            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}createphototags", BaseConfigs.GetTablePrefix), parms);
        }

        public IDataReader GetHotTagsListForPhoto(int count)
        {
            string sql = string.Format("SELECT TOP {0} * FROM [{1}tags] WHERE [pcount] > 0 ORDER BY [pcount] DESC,[orderid]", count, BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        public int GetPhotoCountWithSameTag(int tagid)
        {
            DbParameter parm = DbHelper.MakeInParam("@tagid", (DbType)SqlDbType.Int, 4, tagid);

            string sql = string.Format("SELECT COUNT(1) FROM [{0}phototags] AS [pt],[{0}photos] AS [p],[{0}albums] AS [a] WHERE [pt].[tagid] = @tagid AND [p].[photoid] = [pt].[photoid] AND [p].[albumid] = [a].[albumid] AND [a].[type]=0", BaseConfigs.GetTablePrefix);

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parm), 0);
        }


        public IDataReader GetPhotosWithSameTag(int tagid, int pageid, int pagesize)
        {
            DbParameter[] parm = {
                                    DbHelper.MakeInParam("@tagid", (DbType)SqlDbType.Int, 4, tagid),
                                    DbHelper.MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, pageid),
                                    DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int, 4, pagesize)
                                 };
            string sql = string.Format("{0}getphotolistbytag", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, sql, parm);
        }

        public IDataReader GetTagsListByPhotoId(int photoid)
        {
            string sql = string.Format("SELECT [{0}tags].* FROM [{0}tags], [{0}phototags] WHERE [{0}phototags].[tagid] = [{0}tags].[tagid] AND [{0}phototags].[photoid] = @photoid ORDER BY [orderid]", BaseConfigs.GetTablePrefix);

            DbParameter parm = DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4, photoid);

            return DbHelper.ExecuteReader(CommandType.Text, sql, parm);
        }

        public IDataReader GetSpacePostCategorys(string spacepostids)
        {
            string sql = string.Format("SELECT [a].[categoryid],[a].[title],[postid] FROM [{0}spacecategories] AS [a],[{0}spacepostcategories] AS [b] WHERE [a].[categoryid]=[b].[categoryid] AND [postid] IN ({1})", BaseConfigs.GetTablePrefix, spacepostids);

            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }








        #region 聚合相册相关函数

        public DataTable GetAlbumListByCondition(string username, string title, string description, string startdate, string enddate, int pageSize, int currentPage, bool isshowall)
        {
            string sql = "";
            string condition = GetAlbumListCondition(username, title, description, startdate, enddate);
            DbParameter[] parms = GetDateSpanParms(startdate, enddate);
            int pageTop = (currentPage - 1) * pageSize;

            string strisshowall = "";
            if (isshowall)
            {
                strisshowall = " 1=1";
            }
            else
            {
                strisshowall = " [type] = 0 AND  [imgcount] > 0 ";
            }
            if (currentPage == 1)
            {
                sql =
                    string.Format("SELECT TOP {0} *  FROM [{1}albums] WHERE {2} {3} ORDER BY [albumid] DESC", pageSize,
                                  BaseConfigs.GetTablePrefix, strisshowall, condition);
            }
            else
            {
                sql =
                    string.Format(
                        "SELECT TOP {0} * FROM [{1}albums] WHERE [albumid]<(SELECT MIN([albumid]) FROM (SELECT TOP {2} [albumid] FROM [{1}albums] WHERE  {3} {4} ORDER BY [albumid] DESC) AS tblTmp) AND {3} {4} ORDER BY [albumid] DESC",
                        pageSize, BaseConfigs.GetTablePrefix, pageTop, strisshowall, condition);
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
        }

        private string GetAlbumListCondition(string usernamelist, string titlelist, string descriptionlist, string startdate, string enddate)
        {
            string condition = "";
            if (usernamelist != "")
            {
                string[] username = usernamelist.Split(',');
                condition += " AND [username] in (";
                string tempusernamelist = "";
                foreach (string p in username)
                {
                    tempusernamelist += "'" + p + "',";
                }
                if (tempusernamelist != "")
                    tempusernamelist = tempusernamelist.Substring(0, tempusernamelist.Length - 1);
                condition += tempusernamelist + ")";
            }
            if (titlelist != "")
            {
                string[] title = titlelist.Split(',');
                condition += " AND [title] in (";
                string temptitlelist = "";
                foreach (string p in title)
                {
                    temptitlelist += "'" + p + "',";
                }
                if (temptitlelist != "")
                    temptitlelist = temptitlelist.Substring(0, temptitlelist.Length - 1);
                condition += temptitlelist + ")";
            }
            if (descriptionlist != "")
            {
                string tempdescriptionlist = "";
                foreach (string description in descriptionlist.Split(','))
                {
                    tempdescriptionlist += " [description] LIKE '%" + RegEsc(description) + "%' OR";
                }
                tempdescriptionlist = tempdescriptionlist.Substring(0, tempdescriptionlist.Length - 2);
                condition += " AND (" + tempdescriptionlist + ")";
            }
            if (startdate != "")
            {
                //condition += " AND [createdatetime]>='" + startdate + " 00:00:00'";
                condition += " AND [createdatetime]>=@startdate";
            }
            if (enddate != "")
            {
                //condition += " AND [createdatetime]<='" + enddate + " 23:59:59'";
                condition += " AND [createdatetime]<=@enddate";
            }
            return condition;
        }

        public int GetAlbumListCountByCondition(string username, string title, string description, string startdate, string enddate, bool isshowall)
        {
            string sql = string.Format("SELECT COUNT(1) FROM [{0}albums] t", BaseConfigs.GetTablePrefix);
            if (isshowall)
            {
                sql += " WHERE 1=1";
            }
            else
            {
                sql += " WHERE [type] = 0 AND  [imgcount] > 0";
            }

            DbParameter[] parms = GetDateSpanParms(startdate, enddate);

            string condition = GetAlbumListCondition(username, title, description, startdate, enddate);
            if (condition != "")
                sql += condition;
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, parms).ToString());
        }


        public DataTable GetAlbumLitByAlbumidList(string albumlist)
        {
            if (!Discuz.Common.Utils.IsNumericArray(albumlist.Split(',')))
            {
                return new DataTable();
            }
            string sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [type] = 0 "
                + "AND [albumid] IN (" + albumlist + ") ORDER BY CHARINDEX(CONVERT(VARCHAR(8),[albumid]),'" + albumlist + "')";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        #endregion

        #region 聚合个人空间相关函数
        public int GetSpaceCountByCondition(string posterlist, string keylist, string startdate, string enddate)
        {
            string sql = "SELECT COUNT(1) FROM (SELECT s.*,u.[username] FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s LEFT JOIN [" + BaseConfigs.GetTablePrefix + "users] u ON s.[userid]=u.[uid]) AS tblTmp WHERE [status]=0";
            string condition = GetSpaceCondition(posterlist, keylist, startdate, enddate);
            DbParameter[] parms = GetDateSpanParms(startdate, enddate);
            if (condition != "")
                sql += condition;
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, parms).ToString());
        }

        public DataTable GetSpaceByCondition(string posterlist, string keylist, string startdate, string enddate, int pageSize, int currentPage)
        {
            string sql = "";
            string condition = GetSpaceCondition(posterlist, keylist, startdate, enddate);
            DbParameter[] parms = GetDateSpanParms(startdate, enddate);
            int pageTop = (currentPage - 1) * pageSize;
            if (currentPage == 1)
            {

                sql = "SELECT TOP " + pageSize + " s.*,u.[username],f.[avatar],(SELECT COUNT(1) FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [userid]=s.[userid]) albumcount "
                    + "FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s LEFT JOIN [" + BaseConfigs.GetTablePrefix + "userfields] f ON [userid]=f.[uid] "
                    + "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "users] u ON u.[uid]=[userid] WHERE [status]=0" + condition + " ORDER BY s.[spaceid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pageSize + " s.*,u.[username],f.[avatar],(SELECT COUNT(1) FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [userid]=s.[userid]) albumcount "
                    + "FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s LEFT JOIN [" + BaseConfigs.GetTablePrefix + "userfields] f ON [userid]=f.[uid] "
                    + "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "users] u ON u.[uid]=[userid] WHERE [status]=0 AND s.[spaceid]<(SELECT MIN([spaceid]) FROM (SELECT TOP " + pageTop
                    + " [spaceid] FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] WHERE [status]=0 " + condition + " ORDER BY [spaceid] DESC) AS tblTmp)" + condition + " ORDER BY s.[spaceid] DESC";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
        }

        private string GetSpaceCondition(string posterlist, string keylist, string startdate, string enddate)
        {
            string condition = "";
            if (posterlist != "")
            {
                string[] poster = posterlist.Split(',');
                condition += " AND [username] in (";
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
                    tempkeylist += " [spacetitle] LIKE '%" + RegEsc(key) + "%' OR";
                }
                tempkeylist = tempkeylist.Substring(0, tempkeylist.Length - 2);
                condition += " AND (" + tempkeylist + ")";
            }
            if (startdate != "")
            {
                //condition += " AND [createdatetime]>='" + startdate + " 00:00:00'";
                condition += " AND [createdatetime]>=@startdate";
            }
            if (enddate != "")
            {
                //condition += " AND [createdatetime]<='" + enddate + " 23:59:59'";
                condition += " AND [createdatetime]<=@enddate";
            }
            return condition;
        }

        public DataTable GetSpaceLitByTidlist(string spaceidlist)
        {
            if (!Discuz.Common.Utils.IsNumericArray(spaceidlist.Split(',')))
            {
                return new DataTable();
            }
            string sql = "SELECT s.*,f.[avatar],(SELECT COUNT(1) FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE userid=s.userid) albumcount "
                + "FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s LEFT JOIN [" + BaseConfigs.GetTablePrefix + "userfields] f ON [userid]=f.[uid] "
                + "WHERE ([spaceid] IN (" + spaceidlist + ")) ORDER BY CHARINDEX(CONVERT(VARCHAR(8),[spaceid]),'" + spaceidlist + "')";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }
        #endregion
        public DataTable GetPhotoByIdOrderList(string idlist)
        {
            if (!Common.Utils.IsNumericArray(idlist.Split(',')))
            {
                return new DataTable();
            }
            string sql = "SELECT [photoid],[title] FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE (photoid IN (" + idlist + ")) "
                + "ORDER BY CHARINDEX(CONVERT(VARCHAR(8),photoid),'" + idlist + "')";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public DataTable GetAlbumByIdOrderList(string idlist)
        {
            if (!Common.Utils.IsNumericArray(idlist.Split(',')))
            {
                return new DataTable();
            }
            string sql = "SELECT [albumid],[title] FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE ([albumid] IN (" + idlist + ")) "
                + "ORDER BY CHARINDEX(CONVERT(VARCHAR(8),[albumid]),'" + idlist + "')";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }


        public DataTable GetWebSiteAggSpaceTopComments(int topnumber)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP " + topnumber + " [postid],[content],[posttitle],[author],[uid] FROM [" + BaseConfigs.GetTablePrefix + "spacecomments] ORDER BY [commentid] DESC").Tables[0];
        }

        public string[] GetSpaceLastPostInfo(int userid)
        {
            DbParameter pram = DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, userid);
            string sql =
                string.Format(
                    "SELECT TOP 1 [postid],[title] FROM [{0}spaceposts] WHERE [uid]=@uid ORDER BY [postdatetime] DESC",
                    BaseConfigs.GetTablePrefix);

            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, sql, pram).Tables[0];
            string[] result = new string[2];
            if (dt != null && dt.Rows.Count != 0)
            {
                result[0] = dt.Rows[0]["postid"].ToString();
                result[1] = dt.Rows[0]["title"].ToString().Trim();
            }
            else
            {
                result[0] = "0";
                result[1] = "";
            }
            return result;
        }

        public int GetSpacePostCountByCondition(string posterlist, string keylist, string startdate, string enddate)
        {
            string sql = "SELECT COUNT(1) FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE 1=1";
            string condition = GetSpacePostCondition(posterlist, keylist, startdate, enddate);

            DbParameter[] parms = GetDateSpanParms(startdate, enddate);

            if (condition != "")
                sql += condition;
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, parms).ToString());
        }

        private string GetSpacePostCondition(string posterlist, string keylist, string startdate, string enddate)
        {
            string condition = "";
            if (posterlist != "")
            {
                string[] poster = posterlist.Split(',');
                condition += " AND [author] in (";
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
                    tempkeylist += " [title] LIKE '%" + RegEsc(key) + "%' OR";
                }
                tempkeylist = tempkeylist.Substring(0, tempkeylist.Length - 2);
                condition += " AND (" + tempkeylist + ")";
            }
            if (startdate != "")
            {
                //condition += " AND [postdatetime]>='" + startdate + " 00:00:00'";
                condition += " AND [postdatetime]>=@startdate";
            }
            if (enddate != "")
            {
                //condition += " AND [postdatetime]<='" + enddate + " 23:59:59'";
                condition += " AND [postdatetime]<=@enddate";
            }
            return condition;
        }

        public DataTable GetSpacePostByCondition(string posterlist, string keylist, string startdate, string enddate, int pageSize, int currentPage)
        {
            string sql = "";
            DbParameter[] parms = GetDateSpanParms(startdate, enddate);

            //DbParameter[] parms = {
            //                               DbHelper.MakeInParam("@startdate",(DbType)SqlDbType.DateTime, 8, ),
            //                               DbHelper.MakeInParam("@enddate",(DbType)SqlDbType.DateTime, 8,endtime)
            //                           };

            string condition = GetSpacePostCondition(posterlist, keylist, startdate, enddate);
            int pageTop = (currentPage - 1) * pageSize;
            if (currentPage == 1)
            {

                sql = "SELECT TOP " + pageSize + " * FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE 1=1" + condition + " ORDER BY [postid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pageSize + " * FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [postid]<(SELECT MIN([postid]) FROM (SELECT TOP " + pageTop
                    + " [postid] FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE 1=1 " + condition + " ORDER BY [postid] DESC) AS tblTmp)" + condition + " ORDER BY [postid] DESC";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetSpacepostLitByTidlist(string postidlist)
        {
            if (!Common.Utils.IsNumericArray(postidlist.Split(',')))
            {
                return new DataTable();
            }
            string sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "spaceposts]"
                + "WHERE ([postid] IN (" + postidlist + ")) ORDER BY CHARINDEX(CONVERT(VARCHAR(8),[postid]),'" + postidlist + "')";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public int GetUidByAlbumid(int albumid)
        {
            DbParameter pram = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            string sql = "SELECT [userid] FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [albumid]=@albumid";
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, pram).ToString());
        }

        public DataTable GetWebSiteAggSpacePostList(int topnumber)
        {
            return DbHelper.ExecuteDataset(" SELECT TOP " + topnumber + " [postid], [author], [uid], [postdatetime], [title], [commentcount], [views] FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [poststatus] = 1 ORDER BY [postdatetime] DESC").Tables[0];
        }

        public DataTable GetWebSiteAggRecentUpdateSpaceList(int topnumber)
        {
            return DbHelper.ExecuteDataset(" SELECT TOP " + topnumber + " [spaceid], [userid], [spacetitle], [postcount], [commentcount], [visitedtimes] FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] WHERE [status] = 0 AND [postcount]>0 ORDER BY [updatedatetime] DESC").Tables[0];
        }

        public DataTable GetWebSiteAggTopSpaceList(string orderby,int topnumber)
        {
            return DbHelper.ExecuteDataset(" SELECT TOP " + topnumber + " s.*,u.[avatar] FROM [" + BaseConfigs.GetTablePrefix + "spaceconfigs] s LEFT JOIN [" + BaseConfigs.GetTablePrefix + "userfields] u ON s.[userid] = u.[uid]  WHERE s.[status] = 0 ORDER BY s.[" + orderby + "] DESC").Tables[0];
        }

        public DataTable GetWebSiteAggTopSpacePostList(string orderby, int topnumber)
        {
            return DbHelper.ExecuteDataset(" SELECT TOP " + topnumber + " [postid],[title],[author],[uid],[postdatetime],[commentcount],[views] FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [poststatus] = 1 ORDER BY [" + orderby + "] DESC").Tables[0];
        }


        public DataTable GetWebSiteAggSpacePostsList(int pageSize, int currentPage)
        {
            DataTable dt = new DataTable();

            int pageTop = (currentPage - 1) * pageSize;
            if (currentPage == 1)
            {
                string sql = "SELECT TOP " + pageSize + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spaceposts] ORDER BY [postid] DESC";
                dt = DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            }
            else
            {
                string sql = "SELECT TOP " + pageSize + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "spaceposts] WHERE [postid] < (SELECT min([postid])  FROM "
                    + "(SELECT TOP " + pageTop + " [postid] FROM [" + BaseConfigs.GetTablePrefix + "spaceposts] "
                    + "ORDER BY [postid] DESC) AS tblTmp ) ORDER BY [postid] DESC";
                dt = DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            }
            return dt;
        }

        public int GetWebSiteAggSpacePostsCount()
        {
            try
            {
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([postid]) FROM [" + BaseConfigs.GetTablePrefix + "spaceposts]");
            }
            catch
            {
                return 0;
            }
        }

        #region 照片操作相关函数

        public int GetPhotoCountByCondition(string photousernamelist, string keylist, string startdate, string enddate)
        {
            string sql = "SELECT COUNT(1) FROM [" + BaseConfigs.GetTablePrefix + "photos] p LEFT JOIN [" + BaseConfigs.GetTablePrefix + "albums] a ON p.[albumid]=a.[albumid] WHERE a.[type]=0";
            DbParameter[] parms = GetDateSpanParms(startdate, enddate);
            string condition = GetPhotoCondition(photousernamelist, keylist, startdate, enddate);
            if (condition != "")
                sql += condition;
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, parms).ToString());
        }

        private string GetPhotoCondition(string photousernamelist, string keylist, string startdate, string enddate)
        {
            string condition = "";
            if (photousernamelist != "")
            {
                string[] poster = photousernamelist.Split(',');
                condition += " AND p.[username] in (";
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
                    tempkeylist += " p.[title] LIKE '%" + RegEsc(key) + "%' OR";
                }
                tempkeylist = tempkeylist.Substring(0, tempkeylist.Length - 2);
                condition += " AND (" + tempkeylist + ")";
            }
            if (startdate != "")
            {
                condition += " AND p.[postdate]>=@startdate";
            }
            if (enddate != "")
            {
                condition += " AND p.[postdate]<=@enddate";
            }
            return condition;
        }

        public DataTable GetPhotoByCondition(string photousernamelist, string keylist, string startdate, string enddate, int pageSize, int currentPage)
        {
            string sql = "";
            DbParameter[] parms = GetDateSpanParms(startdate, enddate);
            string condition = GetPhotoCondition(photousernamelist, keylist, startdate, enddate);
            int pageTop = (currentPage - 1) * pageSize;
            if (currentPage == 1)
            {

                sql = "SELECT TOP " + pageSize + " p.* FROM [" + BaseConfigs.GetTablePrefix + "photos] p LEFT JOIN [" + BaseConfigs.GetTablePrefix + "albums] a ON p.[albumid]=a.[albumid] WHERE a.[type]=0" + condition + " ORDER BY p.[photoid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pageSize + " p.* FROM [" + BaseConfigs.GetTablePrefix + "photos] p LEFT JOIN [" + BaseConfigs.GetTablePrefix + "albums] a ON p.[albumid]=a.[albumid] WHERE a.[type]=0 AND p.[photoid]<(SELECT MIN([photoid]) FROM (SELECT TOP " + pageTop
                    + " p.[photoid] FROM [" + BaseConfigs.GetTablePrefix + "photos] p LEFT JOIN [" + BaseConfigs.GetTablePrefix + "albums] a ON p.[albumid]=a.[albumid] WHERE a.[type]=0 " + condition + " ORDER BY p.[photoid] DESC) AS tblTmp)" + condition + " ORDER BY p.[photoid] DESC";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
        }

        #endregion

    }
}
#endif
