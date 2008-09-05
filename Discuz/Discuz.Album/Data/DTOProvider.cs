using System;
using System.Collections;
using System.Data;
using System.Text;
using Discuz.Cache;
using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Entity;

namespace Discuz.Album.Data
{
    public class DTOProvider
    {
        #region Space 相册操作
        public static AlbumCategoryInfo GetAlbumCategory(int albumcateid)
        {
            foreach (AlbumCategoryInfo aci in GetAlbumCategory())
            {
                if (aci.Albumcateid == albumcateid)
                {
                    return aci;
                }
            }

            return new AlbumCategoryInfo();
        }



        public static AlbumInfo GetAlbumInfo(int aid)
        {
            IDataReader reader = DbProvider.GetInstance().GetSpaceAlbumById(aid);

            if (reader.Read())
            {
                AlbumInfo albumsinfo = GetAlbumEntity(reader);

                reader.Close();
                return albumsinfo;
            }
            else
            {
                reader.Close();
                return null;
            }
        }

        public static AlbumInfo[] GetSpaceAlbumsInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return null;

            AlbumInfo[] albumsinfoarray = new AlbumInfo[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                albumsinfoarray[i] = new AlbumInfo();
                albumsinfoarray[i].Albumid = Convert.ToInt32(dt.Rows[i]["albumid"].ToString());
                albumsinfoarray[i].Userid = Convert.ToInt32(dt.Rows[i]["userid"].ToString());
                albumsinfoarray[i].Title = dt.Rows[i]["title"].ToString();
                albumsinfoarray[i].Description = dt.Rows[i]["description"].ToString();
                albumsinfoarray[i].Logo = dt.Rows[i]["logo"].ToString();
                albumsinfoarray[i].Password = dt.Rows[i]["password"].ToString();
                albumsinfoarray[i].Imgcount = Convert.ToInt32(dt.Rows[i]["imgcount"].ToString());
                albumsinfoarray[i].Views = Convert.ToInt32(dt.Rows[i]["views"].ToString());
                albumsinfoarray[i].Type = Convert.ToInt32(dt.Rows[i]["type"].ToString());
                albumsinfoarray[i].Createdatetime = dt.Rows[i]["createdatetime"].ToString();

            }

            dt.Dispose();
            return albumsinfoarray;
        }

        public static void UpdateAlbumViews(int albumid)
        {
            Data.DbProvider.GetInstance().UpdateAlbumViews(albumid);
        }
        #endregion

        #region Space 照片操作
        public static PhotoInfo GetPhotoInfo(IDataReader reader)
        {
            if (reader == null)
                return null;

            if (reader.Read())
            {
                PhotoInfo pi = GetPhotoEntity(reader);
                reader.Close();
                return pi;
            }
            else
            {
                return null;
            }
        }




        public static PhotoInfo GetPhotoEntity(IDataReader reader)
        {
            PhotoInfo photoinfo = new PhotoInfo();
            photoinfo.Photoid = Convert.ToInt32(reader["photoid"].ToString());
            photoinfo.Filename = reader["filename"].ToString();
            photoinfo.Attachment = reader["attachment"].ToString();
            photoinfo.Filesize = Convert.ToInt32(reader["filesize"].ToString());
            photoinfo.Description = reader["description"].ToString();
            photoinfo.Postdate = reader["postdate"].ToString();
            photoinfo.Albumid = Convert.ToInt32(reader["albumid"].ToString());
            photoinfo.Userid = Convert.ToInt32(reader["userid"].ToString());
            photoinfo.Title = reader["title"].ToString();
            photoinfo.Views = Convert.ToInt32(reader["views"]);
            photoinfo.Commentstatus = (PhotoStatus)Utils.StrToInt(reader["commentstatus"], 0);
            photoinfo.Tagstatus = (PhotoStatus)Utils.StrToInt(reader["tagstatus"], 0);
            photoinfo.Comments = Utils.StrToInt(reader["comments"], 0);
            photoinfo.Username = reader["username"].ToString();

            return photoinfo;
        }

        public static int GetSpacePhotosCount(int albumid)
        {
            return Data.DbProvider.GetInstance().GetSpacePhotoCountByAlbumId(albumid);
        }

        public static int GetSpaceAlbumListCount(int userid, int albumcategoryid)
        {
            return Data.DbProvider.GetInstance().SpaceAlbumsListCount(userid, albumcategoryid);
        }

        public static AlbumInfo GetAlbumEntity(IDataReader reader)
        {
            AlbumInfo album = new AlbumInfo();

            album.Albumid = Utils.StrToInt(reader["albumid"], 0);
            album.Userid = Utils.StrToInt(reader["userid"], 0);
            album.Username = reader["username"].ToString();
            album.Title = reader["title"].ToString();
            album.Description = reader["description"].ToString();
            album.Logo = reader["logo"].ToString();
            album.Password = reader["password"].ToString();
            album.Imgcount = Utils.StrToInt(reader["imgcount"], 0);
            album.Views = Utils.StrToInt(reader["views"], 0);
            album.Type = Utils.StrToInt(reader["type"], 0);
            album.Createdatetime = reader["createdatetime"].ToString();
            album.Albumcateid = Utils.StrToInt(reader["albumcateid"], 0);

            return album;
        }

        #endregion

        public static PhotoInfo GetPhotoInfo(int photoid, int albumid, byte mode)
        {
            PhotoInfo photo = new PhotoInfo();
            IDataReader reader = Data.DbProvider.GetInstance().GetPhotoByID(photoid, albumid, mode);

            photo = GetPhotoInfo(reader);
            reader.Close();
            return photo;

        }

        public static void UpdatePhotoViews(int photoid)
        {
            Data.DbProvider.GetInstance().UpdatePhotoViews(photoid);
        }



        public static void UpdatePhotoInfo(PhotoInfo photo)
        {
            Data.DbProvider.GetInstance().UpdatePhotoInfo(photo);
        }

#if NET1       

       

        public static AlbumCategoryInfoCollection GetAlbumCategory()
        {
            DNTCache cache = DNTCache.GetCacheService();
            AlbumCategoryInfoCollection acic = cache.RetrieveObject("/Space/AlbumCategory") as AlbumCategoryInfoCollection;

            if (acic == null)
            {
                acic = Data.DbProvider.GetInstance().GetAlbumCategory();
                cache.AddObject("/Space/AlbumCategory", (CollectionBase)acic);
            }

            return acic;
        }


        public static AlbumInfoCollection GetSpaceAlbumList(int userid, int albumcategoryid, int pageSize, int currentPage)
        {
            AlbumInfoCollection aic = new AlbumInfoCollection();
            IDataReader reader = Data.DbProvider.GetInstance().SpaceAlbumsList(userid, albumcategoryid, pageSize, currentPage);
            if(reader != null)
            {
                while (reader.Read())
                {
                    aic.Add(GetAlbumEntity(reader));
                }
                reader.Close();
            }
            return aic;
        }


        public static AlbumInfoCollection GetAlbumRankList(int albumcount)
        {
            DNTCache cache = DNTCache.GetCacheService();

            AlbumInfoCollection aic = cache.RetrieveObject(string.Format("/Photo/AlbumRank{0}", albumcount)) as AlbumInfoCollection;

            if (aic == null)
            {
                IDataReader reader = Data.DbProvider.GetInstance().GetPhotoRankList(3, albumcount);
                aic = new AlbumInfoCollection();
                if(reader != null)
                {
                    while (reader.Read())
                    {
                        aic.Add(GetAlbumEntity(reader));
                    }
                    reader.Close();
                }
                cache.AddObject(string.Format("/Photo/AlbumRank{0}", albumcount), (CollectionBase)aic);
            }

            return aic;
        }


        public static PhotoCommentInfoCollection GetPhotoCommentCollection(int photoid)
        {
            PhotoCommentInfoCollection comments = new PhotoCommentInfoCollection();
            IDataReader reader = Data.DbProvider.GetInstance().GetPhotoCommentCollection(photoid);
            if(reader != null)
            {
                while (reader.Read())
                {
                    PhotoCommentInfo comment = new PhotoCommentInfo();
                    comment.Commentid = Convert.ToInt32(reader["commentid"]);
                    comment.Content = Utils.RemoveHtml(reader["content"].ToString());
                    comment.Ip = reader["ip"].ToString();
                    //comment.Parentid = Convert.ToInt32(reader["parentid"]);
                    comment.Photoid = Convert.ToInt32(reader["photoid"]);
                    comment.Postdatetime = Convert.ToDateTime(reader["postdatetime"]);
                    comment.Userid = Convert.ToInt32(reader["userid"]);
                    comment.Username = reader["username"].ToString();

                    comments.Add(comment);
                }
                reader.Close();
            }
            return comments;
        }

        public static PhotoInfoCollection GetPhotoListByUserId(int userid, int albumid, int count)
        {
            PhotoInfoCollection photolist = new PhotoInfoCollection();

            IDataReader reader = Data.DbProvider.GetInstance().GetPhotoListByUserId(userid, albumid, count);

            if(reader != null)
            {
                while (reader.Read())
                {
                    photolist.Add(GetPhotoEntity(reader));
                }
                reader.Close();
            }
            return photolist;
        }

        public static PhotoInfoCollection GetSpacePhotosInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return new PhotoInfoCollection();

            PhotoInfoCollection photosinfoarray = new PhotoInfoCollection();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PhotoInfo photo = new PhotoInfo();
                photo.Photoid = Convert.ToInt32(dt.Rows[i]["photoid"].ToString());
                photo.Filename = dt.Rows[i]["filename"].ToString();
                photo.Attachment = dt.Rows[i]["attachment"].ToString();
                photo.Filesize = Convert.ToInt32(dt.Rows[i]["filesize"].ToString());
                photo.Description = dt.Rows[i]["description"].ToString();
                photo.Postdate = dt.Rows[i]["postdate"].ToString();
                photo.Albumid = Convert.ToInt32(dt.Rows[i]["albumid"].ToString());
                photo.Userid = Convert.ToInt32(dt.Rows[i]["userid"].ToString());
                photo.Title = dt.Rows[i]["title"].ToString();
                photo.Views = Convert.ToInt32(dt.Rows[i]["views"]);
                photo.Commentstatus = (PhotoStatus)Utils.StrToInt(dt.Rows[i]["commentstatus"], 0);
                photo.Tagstatus = (PhotoStatus)Utils.StrToInt(dt.Rows[i]["tagstatus"], 0);
                photo.Comments = Utils.StrToInt(dt.Rows[i]["comments"], 0);

                photosinfoarray.Add(photo);
            }
            dt.Dispose();
            return photosinfoarray;
        }

        /// <summary>
        /// 获得图片排行图集合
        /// </summary>
        /// <param name="type">排行方式，0浏览量，1评论数,2上传时间</param>
        /// <returns></returns>
        public static PhotoInfoCollection GetPhotoRankList(int type, int photocount)
        {
            DNTCache cache = DNTCache.GetCacheService();

            PhotoInfoCollection pic = cache.RetrieveObject(string.Format("/Photo/PhotoRank{0}-{1}", type, photocount)) as PhotoInfoCollection;

            if (pic == null)
            {
                IDataReader reader = Data.DbProvider.GetInstance().GetPhotoRankList(type, photocount);
                pic = new PhotoInfoCollection();
                if(reader != null)
                {
                    while (reader.Read())
                    {
                        PhotoInfo pi = GetPhotoEntity(reader);
                        pi.Filename = Globals.GetSquareImage(pi.Filename);
                        pic.Add(pi);
                    }
                    reader.Close();
                }
                cache.AddObject(string.Format("/Photo/PhotoRank{0}-{1}", type, photocount), (ICollection)pic);
            }

            return pic;
        }

#else

        #region 集合改泛型

        public static Discuz.Common.Generic.List<AlbumCategoryInfo> GetAlbumCategory()
        {
            DNTCache cache = DNTCache.GetCacheService();
            Discuz.Common.Generic.List<AlbumCategoryInfo> acic = cache.RetrieveObject("/Space/AlbumCategory") as Discuz.Common.Generic.List<AlbumCategoryInfo>;

            if (acic == null)
            {
                acic = new Discuz.Common.Generic.List<AlbumCategoryInfo>();
                acic = Data.DbProvider.GetInstance().GetAlbumCategory();
                cache.AddObject("/Space/AlbumCategory", (ICollection)acic);
            }

            return acic;
        }


        public static Discuz.Common.Generic.List<AlbumInfo> GetSpaceAlbumList(int userid, int albumcategoryid, int pageSize, int currentPage)
        {
            Discuz.Common.Generic.List<AlbumInfo> aic = new Discuz.Common.Generic.List<AlbumInfo>();
            IDataReader reader = Data.DbProvider.GetInstance().SpaceAlbumsList(userid, albumcategoryid, pageSize, currentPage);
            while (reader.Read())
            {
                aic.Add(GetAlbumEntity(reader));
            }
            reader.Close();
            return aic;
        }


        public static Discuz.Common.Generic.List<AlbumInfo> GetAlbumRankList(int albumcount)
        {
            DNTCache cache = DNTCache.GetCacheService();

            Discuz.Common.Generic.List<AlbumInfo> aic = cache.RetrieveObject(string.Format("/Photo/AlbumRank{0}", albumcount)) as Discuz.Common.Generic.List<AlbumInfo>;

            if (aic == null)
            {
                IDataReader reader = Data.DbProvider.GetInstance().GetPhotoRankList(3, albumcount);
                aic = new Discuz.Common.Generic.List<AlbumInfo>();
                while (reader.Read())
                {
                    aic.Add(GetAlbumEntity(reader));
                }
                reader.Close();
                cache.AddObject(string.Format("/Photo/AlbumRank{0}", albumcount), (ICollection)aic);
            }

            return aic;
        }

        public static Discuz.Common.Generic.List<PhotoCommentInfo> GetPhotoCommentCollection(int photoid)
        {
            Discuz.Common.Generic.List<PhotoCommentInfo> comments = new Discuz.Common.Generic.List<PhotoCommentInfo>();
            IDataReader reader = Data.DbProvider.GetInstance().GetPhotoCommentCollection(photoid);
            while (reader.Read())
            {
                PhotoCommentInfo comment = new PhotoCommentInfo();
                comment.Commentid = Convert.ToInt32(reader["commentid"]);
                comment.Content = Utils.RemoveHtml(reader["content"].ToString());
                comment.Ip = reader["ip"].ToString();
                //comment.Parentid = Convert.ToInt32(reader["parentid"]);
                comment.Photoid = Convert.ToInt32(reader["photoid"]);
                comment.Postdatetime = Convert.ToDateTime(reader["postdatetime"]);
                comment.Userid = Convert.ToInt32(reader["userid"]);
                comment.Username = reader["username"].ToString();

                comments.Add(comment);
            }
            reader.Close();
            return comments;
        }

        public static List<PhotoInfo> GetPhotoListByUserId(int userid, int albumid, int count)
        {
            List<PhotoInfo> photolist = new List<PhotoInfo>();

            IDataReader reader = Data.DbProvider.GetInstance().GetPhotoListByUserId(userid, albumid, count);
            while (reader.Read())
            {
                photolist.Add(GetPhotoEntity(reader));
            }
            reader.Close();

            return photolist;
        }

        public static Discuz.Common.Generic.List<PhotoInfo> GetSpacePhotosInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return new Discuz.Common.Generic.List<PhotoInfo>();

            Discuz.Common.Generic.List<PhotoInfo> photosinfoarray = new Discuz.Common.Generic.List<PhotoInfo>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PhotoInfo photo = new PhotoInfo();
                photo.Photoid = Convert.ToInt32(dt.Rows[i]["photoid"].ToString());
                photo.Filename = dt.Rows[i]["filename"].ToString();
                photo.Attachment = dt.Rows[i]["attachment"].ToString();
                photo.Filesize = Convert.ToInt32(dt.Rows[i]["filesize"].ToString());
                photo.Description = dt.Rows[i]["description"].ToString();
                photo.Postdate = dt.Rows[i]["postdate"].ToString();
                photo.Albumid = Convert.ToInt32(dt.Rows[i]["albumid"].ToString());
                photo.Userid = Convert.ToInt32(dt.Rows[i]["userid"].ToString());
                photo.Title = dt.Rows[i]["title"].ToString();
                photo.Views = Convert.ToInt32(dt.Rows[i]["views"]);
                photo.Commentstatus = (PhotoStatus)Utils.StrToInt(dt.Rows[i]["commentstatus"], 0);
                photo.Tagstatus = (PhotoStatus)Utils.StrToInt(dt.Rows[i]["tagstatus"], 0);
                photo.Comments = Utils.StrToInt(dt.Rows[i]["comments"], 0);

                photosinfoarray.Add(photo);
            }
            dt.Dispose();
            return photosinfoarray;
        }


        /// <summary>
        /// 获得图片排行图集合
        /// </summary>
        /// <param name="type">排行方式，0浏览量，1评论数,2上传时间</param>
        /// <returns></returns>
        public static Discuz.Common.Generic.List<PhotoInfo> GetPhotoRankList(int type, int photocount)
        {
            DNTCache cache = DNTCache.GetCacheService();

            Discuz.Common.Generic.List<PhotoInfo> pic = cache.RetrieveObject(string.Format("/Photo/PhotoRank{0}-{1}", type, photocount)) as Discuz.Common.Generic.List<PhotoInfo>;

            if (pic == null)
            {
                IDataReader reader = Data.DbProvider.GetInstance().GetPhotoRankList(type, photocount);
                pic = new Discuz.Common.Generic.List<PhotoInfo>();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        PhotoInfo pi = GetPhotoEntity(reader);
                        pi.Filename = Globals.GetSquareImage(pi.Filename);
                        pic.Add(pi);
                    }
                    reader.Close();
                }
                cache.AddObject(string.Format("/Photo/PhotoRank{0}-{1}", type, photocount), (ICollection)pic);
            }

            return pic;
        }
        #endregion

#endif
    }
}
