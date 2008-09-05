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

namespace Discuz.Album.Data
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


        #region	相册 操作类



        public void AddAlbumCategory(AlbumCategoryInfo aci)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 50, aci.Title),
                                        DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 300, aci.Description),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, aci.Displayorder)
                                };

            string sql = string.Format(@"INSERT INTO [{0}albumcategories]([title], [description], [albumcount], [displayorder])
                            VALUES(@title, @description, 0, @displayorder)", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public void UpdateAlbumCategory(AlbumCategoryInfo aci)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@albumcateid", (DbType)SqlDbType.Int, 4, aci.Albumcateid),
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 50, aci.Title),
                                        DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 300, aci.Description),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, aci.Displayorder)
                                };

            string sql = string.Format(@"UPDATE [{0}albumcategories] 
                                         SET [title]=@title, [description]=@description, [displayorder]=@displayorder 
                                         WHERE [albumcateid]=@albumcateid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }


        public void DeleteAlbumCategory(int albumcateid)
        {
            DbParameter parm = DbHelper.MakeInParam("@albumcateid", (DbType)SqlDbType.Int, 4, albumcateid);

            string sql = string.Format(@"DELETE FROM [{0}albumcategories] 
                                         WHERE [albumcateid]=@albumcateid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parm);
        }

        public int GetSpaceAlbumsCount(int userid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([albumid]) FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [userid]=@userid", parm);
            }
            catch
            {
                return 0;
            }
        }

        public bool CountAlbumByAlbumID(int albumid)
        {
            //try
            //{
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "albums] SET [views] = [views] + 1 WHERE [albumid] = " + albumid);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }

        public DataTable SpaceAlbumsList(int pageSize, int currentPage, int userid)
        {
            DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
            int pageTop = (currentPage - 1) * pageSize;
            string sql = "";
            if (currentPage == 1)
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "albums] WHERE [userid]=@userid ORDER BY [albumid] DESC";
            }
            else
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "albums] WHERE [albumid] < (SELECT min([albumid])  FROM "
                    + "(SELECT TOP " + pageTop + " [albumid] FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE "
                    + "[userid]=@userid ORDER BY [albumid] DESC) AS tblTmp ) AND [userid]=@userid ORDER BY [albumid] DESC";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
        }

        public IDataReader SpaceAlbumsList(int userid, int albumcategoryid, int pageSize, int currentPage)
        {
            DbParameter[] parms = {
										DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, userid),
										DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int, 4, pageSize),
										DbHelper.MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, currentPage),
                                        DbHelper.MakeInParam("@albumcateid", (DbType)SqlDbType.Int, 4, albumcategoryid)
								   };

            return DbHelper.ExecuteReader(CommandType.StoredProcedure, BaseConfigs.GetTablePrefix + "getalbumlist", parms);
        }

        public int SpaceAlbumsListCount(int userid, int albumcategoryid)
        {
            string sql = string.Format("SELECT COUNT(1) FROM [{0}albums] WHERE [imgcount]>0 ", BaseConfigs.GetTablePrefix);
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int,  4, userid),
                                       DbHelper.MakeInParam("@albumcateid", (DbType)SqlDbType.Int, 4, albumcategoryid)
								   };
            if (userid > 0)
            {
                sql += " AND [userid]=@userid";
            }

            if (albumcategoryid != 0)
            {
                sql += " AND [albumcateid]=@albumcateid";
            }

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parms), 0);
        }

        public IDataReader GetSpaceAlbumById(int albumId)
        {
            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [albumid]=" + albumId);
            return reader;
        }

        public DataTable GetSpaceAlbumByUserId(int userid)
        {
            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [userid]=" + userid).Tables[0];
            return dt;
        }


        public IDataReader GetRecommendAlbumList(string idlist)
        {
            if (!Utils.IsNumericArray(idlist.Split(',')))
            {
                return null;
            }

            string sql = string.Format("SELECT * FROM [{0}albums] WHERE [albumid] IN ({1})", BaseConfigs.GetTablePrefix, idlist);

            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        public bool AddSpaceAlbum(AlbumInfo spaceAlbum)
        {
            //try
            //{
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,spaceAlbum.Userid),
					DbHelper.MakeInParam("@albumcateid", (DbType)SqlDbType.Int, 4,spaceAlbum.Albumcateid),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 50,spaceAlbum.Title),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 200,spaceAlbum.Description),
					DbHelper.MakeInParam("@password", (DbType)SqlDbType.NChar, 50,spaceAlbum.Password),
					DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 8,spaceAlbum.Type),
                    DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20, spaceAlbum.Username)
					//DbHelper.MakeInParam("@creatdatetime", (DbType)SqlDbType.DateTime, 8,spaceAlbum.Createdatetime)
				};
            string sqlstring = String.Format("INSERT INTO [{0}albums] ([userid], [username], [albumcateid], [title], [description], [password], [type]) VALUES ( @userid, @username, @albumcateid, @title, @description, @password, @type)", BaseConfigs.GetTablePrefix);

            //向关联表中插入相关数据
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }

        public bool SaveSpaceAlbum(AlbumInfo spaceAlbum)
        {
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }

        public void UpdateAlbumViews(int albumid)
        {
            DbParameter parm = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            string sql = string.Format("UPDATE [{0}albums] SET [views]=[views]+1 WHERE [albumid]=@albumid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parm);
        }

        public bool DeleteSpaceAlbum(int albumId, int userid)
        {
            //try
            //{
            //删除照片及文件
            string sqlstring = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [albumid]=" + albumId + " AND [userid]=" + userid;
            DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring);

            return true;
            //}
            //catch (Exception ex)
            //{
            //    errormsg = Globals.TransferSqlErrorInfo(ex.Message);
            //    return false;
            //}
        }



#if NET1

        public AlbumCategoryInfoCollection GetAlbumCategory()
        {
            string sql = string.Format("SELECT * FROM [{0}albumcategories] ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, sql);

            AlbumCategoryInfoCollection acic = new AlbumCategoryInfoCollection();

            if (reader != null)
            {
                while (reader.Read())
                {
                    AlbumCategoryInfo aci = new AlbumCategoryInfo();
                    aci.Albumcateid = Utils.StrToInt(reader["albumcateid"], 0);
                    aci.Albumcount = Utils.StrToInt(reader["albumcount"], 0);
                    aci.Description = reader["description"].ToString();
                    aci.Displayorder = Utils.StrToInt(reader["displayorder"], 0);
                    aci.Title = reader["title"].ToString();
                    acic.Add(aci);
                }
                reader.Close();
            }
            return acic;
        }

#else

        public Discuz.Common.Generic.List<AlbumCategoryInfo> GetAlbumCategory()
        {
            string sql = string.Format("SELECT * FROM [{0}albumcategories] ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, sql);

            Discuz.Common.Generic.List<AlbumCategoryInfo> acic = new Discuz.Common.Generic.List<AlbumCategoryInfo>();

            if (reader != null)
            {
                while (reader.Read())
                {
                    AlbumCategoryInfo aci = new AlbumCategoryInfo();
                    aci.Albumcateid = Utils.StrToInt(reader["albumcateid"], 0);
                    aci.Albumcount = Utils.StrToInt(reader["albumcount"], 0);
                    aci.Description = reader["description"].ToString();
                    aci.Displayorder = Utils.StrToInt(reader["displayorder"], 0);
                    aci.Title = reader["title"].ToString();
                    acic.Add(aci);
                }
                reader.Close();
            }
            return acic;
        }
#endif
        public string GetAlbumCategorySql()
        {
            return string.Format("SELECT * FROM [{0}albumcategories] ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);
        }
        #endregion

        #region 照片 操作类

        /// <summary>
        /// 获取图片集合
        /// </summary>
        /// <param name="userid">用户Id,必须指定一个用户,不能为0</param>
        /// <param name="albumid">相册Id，当为0时表示此用户所有相册</param>
        /// <param name="count">取出的数量</param>
        /// <returns></returns>
        public IDataReader GetPhotoListByUserId(int userid, int albumid, int count)
        {
            string sql = string.Format("SELECT TOP {0} [p].* FROM [{1}photos] [p],[{1}albums] [a] WHERE [a].[albumid] = [p].[albumid] AND [a].[type]=0 AND [p].[userid]=@userid", count, BaseConfigs.GetTablePrefix);

            if (albumid > 0)
            {
                sql += " AND [p].[albumid]=@albumid";
            }

            sql += " ORDER BY [p].[postdate] DESC";

            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid)
                                    };

            return DbHelper.ExecuteReader(CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 获得图片排行图集合
        /// </summary>
        /// <param name="type">排行方式，0浏览量，1评论数,2上传时间，3收藏数</param>
        /// <returns></returns>
        public IDataReader GetPhotoRankList(int type, int photocount)
        {
            string sql = string.Format("SELECT TOP {0} [p].* FROM [{1}photos] [p],[{1}albums] [a] WHERE [a].[albumid] = [p].[albumid] AND [a].[type]=0",
                                        photocount, BaseConfigs.GetTablePrefix);

            switch (type)
            {
                case 0:
                    sql += " ORDER BY [p].[views] DESC";
                    break;
                case 1:
                    sql += " ORDER BY [p].[comments] DESC";
                    break;
                case 2:
                    sql += " ORDER BY [p].[postdate] DESC";
                    break;
                case 3:
                    sql = string.Format(@"SELECT * FROM [{0}albums] WHERE albumid IN (SELECT TOP {1} [tid] 
		                                                                FROM [{0}favorites]
		                                                                WHERE  [typeid]=1 AND [tid] in (SELECT [albumid] 
                                                                                                        FROM [{0}albums] 
                                                                                                        WHERE [type]=0) 
		                                                                GROUP BY [tid] 
		                                                                ORDER BY COUNT([tid]) DESC)", BaseConfigs.GetTablePrefix, photocount);
                    break;
                default:
                    sql += " ORDER BY [p].[views] DESC";
                    break;
            }

            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }

        public IDataReader GetFocusPhotoList(int type, int focusphotocount, int validDays)
        {
            //DbParameter parm = DbHelper.MakeInParam("@validDays", (DbType)SqlDbType.Int, 4, validDays);
            //string sql = string.Format("SELECT TOP {0} [p].* FROM [{1}photos] [p],[{1}albums] [a] WHERE DATEDIFF(d, getdate(), [postdate]) < @validDays AND [a].[albumid] = [p].[albumid] AND [a].[type]=0",
            //                            focusphotocount, BaseConfigs.GetTablePrefix);
            //switch (type)
            //{
            //    case 0:
            //        sql += " ORDER BY [p].[views] DESC";
            //        break;
            //    case 1:
            //        sql += " ORDER BY [p].[comments] DESC";
            //        break;
            //    case 2:
            //        sql += " ORDER BY [p].[postdate] DESC";
            //        break;
            //    default:
            //        sql += " ORDER BY [p].[views] DESC";
            //        break;
            //}
            //return DbHelper.ExecuteReader(CommandType.Text, sql, parm);
            return GetFocusPhotoList(type, focusphotocount, validDays, -1);
        }


        public IDataReader GetFocusPhotoList(int type, int focusphotocount, int validDays, int uid)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@validDays", (DbType)SqlDbType.Int, 4, validDays),
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)
                               };
            string sql = string.Format("SELECT TOP {0} [p].* FROM [{1}photos] [p],[{1}albums] [a] WHERE DATEDIFF(d, [postdate], getdate()) < @validDays AND [a].[albumid] = [p].[albumid] AND [a].[type]=0{2}",
                                        focusphotocount, BaseConfigs.GetTablePrefix, uid > 1 ? " AND [p].[userid] =@uid" : string.Empty);
            switch (type)
            {
                case 0:
                    sql += " ORDER BY [p].[views] DESC";
                    break;
                case 1:
                    sql += " ORDER BY [p].[comments] DESC";
                    break;
                case 2:
                    sql += " ORDER BY [p].[postdate] DESC";
                    break;
                default:
                    sql += " ORDER BY [p].[views] DESC";
                    break;
            }
            return DbHelper.ExecuteReader(CommandType.Text, sql, parms);
        }


        public IDataReader GetRecommendPhotoList(string idlist)
        {
            if (!Utils.IsNumericArray(idlist.Split(',')))
            {
                return null;
            }

            string sql = string.Format("SELECT [p].* FROM [{0}photos] [p],[{0}albums] [a] WHERE [p].[albumid] = [a].[albumid] AND [a].[type]=0 AND [p].[photoid] IN ({1}) ORDER BY CHARINDEX(CONVERT(VARCHAR(8),[p].[photoid]),'{1}')", BaseConfigs.GetTablePrefix, idlist);

            return DbHelper.ExecuteReader(CommandType.Text, sql);
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

        /// <summary>
        /// 更新图片信息(仅更新 标题、描述、评论设置和标签设置4项)
        /// </summary>
        /// <param name="photo"></param>
        public void UpdatePhotoInfo(PhotoInfo photo)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4, photo.Photoid),
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 20, photo.Title),
                                        DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 200, photo.Description),
                                        DbHelper.MakeInParam("@commentstatus", (DbType)SqlDbType.TinyInt, 1, (byte)photo.Commentstatus),
                                        DbHelper.MakeInParam("@tagstatus", (DbType)SqlDbType.TinyInt, 1, (byte)photo.Tagstatus)
                                    };

            string sql = string.Format("UPDATE [{0}photos] SET [title]=@title, [description]=@description, [commentstatus]=@commentstatus, [tagstatus]=@tagstatus WHERE [photoid]=@photoid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 通过相册ID得到相册中所有图片的信息
        /// </summary>
        /// <param name="albumid">相册ID</param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public DataTable GetSpacePhotoByAlbumID(int albumid)
        {
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4,albumid)
				};
            string sqlstring = String.Format("SELECT * FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [albumid] = @albumid");

            //向关联表中插入相关数据
            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];

            return dt;
        }

        /// <summary>
        /// 获得照片信息
        /// </summary>
        /// <param name="photoid">图片Id</param>
        /// <param name="albumid">相册Id</param>
        /// <param name="mode">模式,0=当前图片,1上一张,2下一张</param>
        /// <returns></returns>
        public IDataReader GetPhotoByID(int photoid, int albumid, byte mode)
        {
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4,photoid),
                    DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid)
				};
            string sqlstring;

            switch (mode)
            {
                case 1:
                    sqlstring = String.Format("SELECT TOP 1 * FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [albumid] = @albumid AND [photoid]<@photoid ORDER BY [photoid] DESC");
                    break;
                case 2:
                    sqlstring = String.Format("SELECT TOP 1 * FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [albumid] = @albumid AND [photoid]>@photoid ORDER BY [photoid] ASC");
                    break;
                default:
                    sqlstring = String.Format("SELECT * FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [photoid] = @photoid");
                    break;
            }
            //向关联表中插入相关数据
            IDataReader idr = DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);

            return idr;
        }

        public void UpdatePhotoViews(int photoid)
        {
            DbParameter parm = DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4, photoid);
            string sql = string.Format("UPDATE [{0}photos] SET [views]=[views]+1 WHERE [photoid]=@photoid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parm);
        }

        public void UpdatePhotoComments(int photoid, int count)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4, photoid),
                DbHelper.MakeInParam("@count", (DbType)SqlDbType.Int, 4, count),
            };
            string commandText = string.Format("UPDATE [{0}photos] SET [comments]=[comments]+@count WHERE [photoid]=@photoid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public int GetSpacePhotosCount(int albumid)
        {
            try
            {
                DbParameter parm = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
                return (int)DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT([photoid]) FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [albumid]=@albumid", parm);
            }
            catch
            {
                return 0;
            }
        }

        public DataTable SpacePhotosList(int pageSize, int currentPage, int userid, int albumid)
        {
            //try
            //{
            //"userid=" + userid + " AND albumid=" + albumid
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,userid),
					DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4,albumid)
				};
            int pageTop = (currentPage - 1) * pageSize;
            string sql = "";
            if (currentPage == 1)
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "photos] WHERE [userid]=@userid AND [albumid]=@albumid ORDER BY [photoid] ASC";
            }
            else
            {
                sql = "SELECT TOP " + pageSize.ToString() + " * FROM "
                    + "[" + BaseConfigs.GetTablePrefix + "photos] WHERE [photoid] > (SELECT MAX([photoid])  FROM "
                    + "(SELECT TOP " + pageTop + " [photoid] FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE "
                    + "[userid]=@userid AND [albumid]=@albumid ORDER BY [photoid] ASC) AS tblTmp ) AND [userid]=@userid "
                    + "AND [albumid]=@albumid ORDER BY [photoid] ASC";
            }
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0];
            //}
            //catch
            //{
            //    return new DataTable();
            //}
        }

        public DataTable SpacePhotosList(int albumid)
        {
            DbParameter parm = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            string sql = sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [albumid]=@albumid ORDER BY [photoid] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parm).Tables[0];
        }

        public bool DeleteSpacePhotoByIDList(string photoidlist, int albumid, int userid)
        {
            if (photoidlist == "")
                return false;
            if (!Utils.IsNumericList(photoidlist))
                return false;

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT [filename],[isattachment] FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [photoid] IN( " + photoidlist + " ) AND [userid]=" + userid, null);

            if (reader != null)
            {
                while (reader.Read())
                {
                    try
                    {
                        string file = Utils.GetMapPath(BaseConfigs.GetForumPath + reader["filename"].ToString());
                        if (reader["isattachment"].ToString() == "0")    //如果是附件图片，则不删除原图，但缩略图、方图将被删除
                        {
                            System.IO.File.Delete(file);
                        }
                        string thumbnailimg = file.Replace(Path.GetExtension(file), "_thumbnail" + Path.GetExtension(file));
                        if (File.Exists(thumbnailimg))
                            File.Delete(thumbnailimg);
                        string squareimg = file.Replace(Path.GetExtension(file), "_square" + Path.GetExtension(file));
                        if (File.Exists(squareimg))
                            File.Delete(squareimg);
                    }
                    catch
                    { }
                }
                reader.Close();
            }

            DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [photoid] IN( " + photoidlist + " ) AND [userid]=" + userid, null);

            return true;
        }

        public int ChangeAlbum(int targetAlbumId, string photoIdList, int userid)
        {
            if (!Utils.IsNumericList(photoIdList))
            {
                return 0;
            }
                
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "photos] SET albumid=" + targetAlbumId + " WHERE photoid IN (" + photoIdList + ") AND [userid]=" + userid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql);
           
        }

        public int GetPhotoSizeByUserid(int userid)
        {
            string sql = "SELECT ISNULL(SUM(filesize), 0) AS [filesize] FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE userid=" + userid;
            //object o = DbHelper.ExecuteScalar(CommandType.Text,sql);
            return (int)DbHelper.ExecuteScalar(CommandType.Text, sql);
        }

        public int GetSpacePhotoCountByAlbumId(int albumid)
        {
            DbParameter parm = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            string sql = string.Format("SELECT COUNT(1) FROM [{0}photos] WHERE [albumid]=@albumid", BaseConfigs.GetTablePrefix);
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sql, parm), 0);
        }

        public DataTable GetPhotosByAlbumid(int albumid)
        {
            DbParameter parm = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            string commandText = string.Format("SELECT [photoid], [userid], [username], [title], [filename] FROM [{0}photos] WHERE [albumid]=@albumid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText, parm).Tables[0];
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

        public int GetUidByAlbumid(int albumid)
        {
            DbParameter pram = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            string sql = "SELECT [userid] FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [albumid]=@albumid";
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, pram).ToString());
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

        public IDataReader GetAlbumListByCondition(int type, int focusphotocount, int vaildDays)
        {
            DbParameter parm = DbHelper.MakeInParam("@vailddays", (DbType)SqlDbType.Int, 4, vaildDays);
            string sql = string.Format("SELECT TOP {0} * FROM [{1}albums] WHERE DATEDIFF(d, [createdatetime], getdate()) < @vailddays AND [imgcount]>0 AND [type]=0", focusphotocount, BaseConfigs.GetTablePrefix);

            switch (type)
            {
                case 0:
                    sql += " ORDER BY [createdatetime] DESC";
                    break;
                case 1:
                    sql += " ORDER BY [views] DESC";
                    break;
                case 2:
                    sql += " ORDER BY [imgcount] DESC";
                    break;
                default:
                    sql += " ORDER BY [createdatetime] DESC";
                    break;
            }

            return DbHelper.ExecuteReader(CommandType.Text, sql, parm);
        }



        public void DeletePhotoTags(int photoid)
        {
            DbParameter parm = DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4, photoid);
            string commandText = string.Format("{0}deletephototags", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, commandText, parm);

        }


        public void DeleteAll(int userid)
        {
            DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);
            string commandText = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "photocomments] WHERE [userid]=@userid";
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parm);

            commandText = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "photos] WHERE [userid]=@userid";
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parm);

            commandText = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [userid]=@userid";
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parm);
        }
    }
}
