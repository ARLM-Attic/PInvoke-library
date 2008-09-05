using System;
using System.Text;
using System.Data;
using System.Xml;


#if NET1
#else

#endif

using Discuz.Entity;
using Discuz.Plugin.Album;

namespace Discuz.Aggregation
{
    /// <summary>
    /// 相册聚合数据类
    /// </summary>
    public class AlbumAggregationData : AggregationData
    {

        /// <summary>
        /// 图片轮换字符串
        /// </summary>
        private static StringBuilder __albumRotatepic = null;

#if NET1

        //推荐到聚合首页的相册列表
        private static AlbumInfoCollection __recommandAlbumListForWebSite;

        //推荐到聚合空间首页的相册列表
        private static AlbumInfoCollection __recommandAlbumListForSpaceIndex;

        //推荐到聚合相册首页的相册列表
        private static AlbumInfoCollection __recommandAlbumListForAlbumIndex;

        //焦点相册列表
        private static AlbumInfoCollection __focusAlbumList;

        //焦点图片列表
        private static PhotoInfoCollection __focusPhotoList;

        //推荐图片列表
        private static PhotoInfoCollection __recommandPhotoList;

        //一周热图总排行
        private static PhotoInfoCollection __weekHotPhotoList;

#else

        //推荐到聚合首页的相册列表
        private static Discuz.Common.Generic.List<AlbumInfo> __recommandAlbumListForWebSite;

        //推荐到聚合空间首页的相册列表
        private static Discuz.Common.Generic.List<AlbumInfo> __recommandAlbumListForSpaceIndex;

        //推荐到聚合相册首页的相册列表
        private static Discuz.Common.Generic.List<AlbumInfo> __recommandAlbumListForAlbumIndex;

        //焦点相册列表
        private static Discuz.Common.Generic.List<AlbumInfo> __focusAlbumList;

        //焦点图片列表
        private static Discuz.Common.Generic.List<PhotoInfo> __focusPhotoList;

        //推荐图片列表
        private static Discuz.Common.Generic.List<PhotoInfo> __recommandPhotoList;

        //一周热图总排行
        private static Discuz.Common.Generic.List<PhotoInfo> __weekHotPhotoList;

#endif

        
        /// <summary>
        /// 从XML中检索出相册的轮换图片信息
        /// </summary>
        /// <returns></returns>
        public new string GetRotatePicData()
        {
            //当文件未被修改时将直接返回相关记录
            if (__albumRotatepic != null)
            {
                return __albumRotatepic.ToString();
            }
            __albumRotatepic = new StringBuilder();
            __albumRotatepic.Append(base.GetRotatePicStr("Albumindex"));

            return __albumRotatepic.ToString();
        }


        /// <summary>
        /// 清空数据绑定
        /// </summary>
        public override void ClearDataBind()
        {
            __recommandAlbumListForWebSite = null;

            __recommandAlbumListForSpaceIndex = null;

            __recommandAlbumListForAlbumIndex = null;

            __focusAlbumList = null;

            __focusPhotoList = null;

            __recommandPhotoList = null;

            __albumRotatepic = null;

            __weekHotPhotoList = null;
        }


       
#if NET1
        
        #region 相册聚合

        /// <summary>
        /// 获得推荐图片列表
        /// </summary>
        /// <param name="nodename">节点名称</param>
        /// <returns></returns>
        public PhotoInfoCollection GetRecommandPhotoList(string nodename)
        {
           //当文件未被修改时将直接返回相关记录
            if (__recommandPhotoList != null)
            {
                return __recommandPhotoList;
            }

            __recommandPhotoList = new PhotoInfoCollection();
            XmlNodeList xmlnodelist = __xmlDoc.DocumentElement.SelectNodes("/Aggregationinfo/Aggregationpage/" + nodename + "/" + nodename + "_photolist/Photo");

            foreach (XmlNode xmlnode in xmlnodelist)
            {
                PhotoInfo __recommandPhoto = new PhotoInfo();

                __recommandPhoto.Photoid = (__xmlDoc.GetSingleNodeValue(xmlnode, "photoid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "photoid"));
                __recommandPhoto.Filename = (__xmlDoc.GetSingleNodeValue(xmlnode, "filename") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "filename");
                __recommandPhoto.Attachment = (__xmlDoc.GetSingleNodeValue(xmlnode, "attachment") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "attachment");
                __recommandPhoto.Filesize = (__xmlDoc.GetSingleNodeValue(xmlnode, "filesize") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "filesize"));
                __recommandPhoto.Description = (__xmlDoc.GetSingleNodeValue(xmlnode, "description") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "description");
                __recommandPhoto.Postdate = (__xmlDoc.GetSingleNodeValue(xmlnode, "postdate") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "postdate");
                __recommandPhoto.Albumid = (__xmlDoc.GetSingleNodeValue(xmlnode, "albumid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "albumid"));
                __recommandPhoto.Userid = (__xmlDoc.GetSingleNodeValue(xmlnode, "userid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "userid"));
                __recommandPhoto.Title = (__xmlDoc.GetSingleNodeValue(xmlnode, "title") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "title");
                __recommandPhoto.Views = (__xmlDoc.GetSingleNodeValue(xmlnode, "views") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "views"));
                __recommandPhoto.Commentstatus = (__xmlDoc.GetSingleNodeValue(xmlnode, "commentstatus") == null) ? PhotoStatus.Owner : (PhotoStatus)Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "commentstatus"));
                __recommandPhoto.Tagstatus = (__xmlDoc.GetSingleNodeValue(xmlnode, "tagstatus") == null) ? PhotoStatus.Owner : (PhotoStatus)Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "tagstatus"));
                __recommandPhoto.Comments = (__xmlDoc.GetSingleNodeValue(xmlnode, "comments") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "comments"));
                __recommandPhoto.Username = (__xmlDoc.GetSingleNodeValue(xmlnode, "username") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "username");
                __recommandPhotoList.Add(__recommandPhoto);
            }
            return __recommandPhotoList;
        }

        
        /// <summary>
        /// 获得焦点图片列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="focusphotocount">返回的记录数</param>
        /// <param name="vaildDays">有效天数</param>
        /// <returns></returns>
        public PhotoInfoCollection GetFocusPhotoList(int type, int focusphotocount, int vaildDays)
        {
            //当文件未被修改时将直接返回相关记录
            if (__focusPhotoList != null)
            {
                return __focusPhotoList;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetFocusPhotoList(type, focusphotocount, vaildDays);

            __focusPhotoList = new PhotoInfoCollection();
            if(reader!=null)
            {
                while (reader.Read())
                {
                    PhotoInfo pi = BlogProvider.GetPhotoEntity(reader);
                    pi.Filename = Globals.GetThumbnailImage(pi.Filename);
                    __focusPhotoList.Add(pi);
                }
                reader.Close();
            }
            return __focusPhotoList;
        }

        
        /// <summary>
        /// 一周热图总排行
        /// </summary>
        /// <param name="focusphotocount">返回的记录数</param>
        /// <returns></returns>
        public PhotoInfoCollection GetWeekHotPhotoList(int focusphotocount)
        {
            //当文件未被修改时将直接返回相关记录
            if (__weekHotPhotoList != null)
            {
                return __weekHotPhotoList;
            }

            __weekHotPhotoList = new PhotoInfoCollection();
            IDataReader reader = DatabaseProvider.GetInstance().GetFocusPhotoList(0, focusphotocount, 7);
            if (reader != null)
            {
                while (reader.Read())
                {
                    PhotoInfo pi = BlogProvider.GetPhotoEntity(reader);
                    pi.Filename = Globals.GetThumbnailImage(pi.Filename);
                    pi.Title = pi.Title.Trim();
                    __weekHotPhotoList.Add(pi);
                }
                reader.Close();
            }
            return __weekHotPhotoList;
        }


        /// <summary>
        /// 获得推荐相册列表
        /// </summary>
        /// <param name="nodename">节点名称</param>
        /// <returns></returns>
        public AlbumInfoCollection GetRecommandAlbumList(string nodename)
        {
            AlbumInfoCollection __recommandAlbumList = null;

            switch (nodename)
            {
                case "Website":
                    {
                        __recommandAlbumList = __recommandAlbumListForWebSite; break;
                    }
                case "Spaceindex":
                    {
                        __recommandAlbumList = __recommandAlbumListForSpaceIndex; break;
                    }
                case "Albumindex":
                    {
                        __recommandAlbumList = __recommandAlbumListForAlbumIndex; break;
                    }
                default:
                    {
                        __recommandAlbumList = __recommandAlbumListForWebSite; break;
                    }
            }

            //当文件未被修改时将直接返回相关记录
            if (__recommandAlbumList != null)
            {
                return __recommandAlbumList;
            }


            __recommandAlbumList = new AlbumInfoCollection();
            XmlNodeList xmlnodelist = __xmlDoc.DocumentElement.SelectNodes("/Aggregationinfo/Aggregationpage/" + nodename + "/" + nodename + "_albumlist/Album");

            foreach (XmlNode xmlnode in xmlnodelist)
            {
                AlbumInfo album = new AlbumInfo();

                album.Albumid = (__xmlDoc.GetSingleNodeValue(xmlnode, "albumid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "albumid"));
                album.Userid = (__xmlDoc.GetSingleNodeValue(xmlnode, "userid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "userid"));
                album.Username = (__xmlDoc.GetSingleNodeValue(xmlnode, "username") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "username");
                album.Title = (__xmlDoc.GetSingleNodeValue(xmlnode, "title") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "title");
                album.Description = (__xmlDoc.GetSingleNodeValue(xmlnode, "description") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "description");
                album.Logo = (__xmlDoc.GetSingleNodeValue(xmlnode, "logo") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "logo");
                album.Password = (__xmlDoc.GetSingleNodeValue(xmlnode, "password") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "password");
                album.Imgcount = (__xmlDoc.GetSingleNodeValue(xmlnode, "imgcount") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "imgcount"));
                album.Views = (__xmlDoc.GetSingleNodeValue(xmlnode, "views") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "views"));
                album.Type = (__xmlDoc.GetSingleNodeValue(xmlnode, "type") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "type"));
                album.Createdatetime = (__xmlDoc.GetSingleNodeValue(xmlnode, "createdatetime") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "createdatetime");
                album.Albumcateid = (__xmlDoc.GetSingleNodeValue(xmlnode, "albumcateid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "albumcateid"));
                __recommandAlbumList.Add(album);
            }

            switch (nodename)
            {
                case "Website":
                    {
                        __recommandAlbumListForWebSite = __recommandAlbumList; break;
                    }
                case "Spaceindex":
                    {
                        __recommandAlbumListForSpaceIndex = __recommandAlbumList; break;
                    }
                case "Albumindex":
                    {
                        __recommandAlbumListForAlbumIndex = __recommandAlbumList; break;
                    }
                default:
                    {
                        __recommandAlbumListForWebSite = __recommandAlbumList; break;
                    }
            }
            return __recommandAlbumList;
        }


        /// <summary>
        /// 获得焦点相册列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="focusphotocount">返回记录条数</param>
        /// <param name="vaildDays">有效天数</param>
        /// <returns></returns>
        public AlbumInfoCollection GetFocusAlbumList(int type, int focusphotocount, int vaildDays)
        {
            //当文件未被修改时将直接返回相关记录
            if (__focusAlbumList != null)
            {
                return __focusAlbumList;
            }

            IDataReader reader = DatabaseProvider.GetInstance().GetAlbumListByCondition(type, focusphotocount, vaildDays);

            __focusAlbumList = new AlbumInfoCollection();
            if(reader!=null)
            {
                while (reader.Read())
                {
                    __focusAlbumList.Add(BlogProvider.GetAlbumEntity(reader));
                }
                reader.Close();
            }
            return __focusAlbumList;
        }

        #endregion
#else
        #region 泛型聚合修改


        /// <summary>
        /// 获得推荐图片列表
        /// </summary>
        /// <param name="nodename">节点名称</param>
        /// <returns></returns>
        public Discuz.Common.Generic.List<PhotoInfo> GetRecommandPhotoList(string nodename)
        {
            //当文件未被修改时将直接返回相关记录
            if (__recommandPhotoList != null)
            {
                return __recommandPhotoList;
            }

            __recommandPhotoList = new Discuz.Common.Generic.List<PhotoInfo>();
            XmlNodeList xmlnodelist = __xmlDoc.DocumentElement.SelectNodes("/Aggregationinfo/Aggregationpage/" + nodename + "/" + nodename + "_photolist/Photo");

            foreach (XmlNode xmlnode in xmlnodelist)
            {
                PhotoInfo __recommandPhoto = new PhotoInfo();

                __recommandPhoto.Photoid = (__xmlDoc.GetSingleNodeValue(xmlnode, "photoid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "photoid"));
                __recommandPhoto.Filename = (__xmlDoc.GetSingleNodeValue(xmlnode, "filename") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "filename");
                __recommandPhoto.Attachment = (__xmlDoc.GetSingleNodeValue(xmlnode, "attachment") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "attachment");
                __recommandPhoto.Filesize = (__xmlDoc.GetSingleNodeValue(xmlnode, "filesize") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "filesize"));
                __recommandPhoto.Description = (__xmlDoc.GetSingleNodeValue(xmlnode, "description") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "description");
                __recommandPhoto.Postdate = (__xmlDoc.GetSingleNodeValue(xmlnode, "postdate") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "postdate");
                __recommandPhoto.Albumid = (__xmlDoc.GetSingleNodeValue(xmlnode, "albumid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "albumid"));
                __recommandPhoto.Userid = (__xmlDoc.GetSingleNodeValue(xmlnode, "userid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "userid"));
                __recommandPhoto.Title = (__xmlDoc.GetSingleNodeValue(xmlnode, "title") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "title");
                __recommandPhoto.Views = (__xmlDoc.GetSingleNodeValue(xmlnode, "views") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "views"));
                __recommandPhoto.Commentstatus = (__xmlDoc.GetSingleNodeValue(xmlnode, "commentstatus") == null) ? PhotoStatus.Owner : (PhotoStatus)Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "commentstatus"));
                __recommandPhoto.Tagstatus = (__xmlDoc.GetSingleNodeValue(xmlnode, "tagstatus") == null) ? PhotoStatus.Owner : (PhotoStatus)Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "tagstatus"));
                __recommandPhoto.Comments = (__xmlDoc.GetSingleNodeValue(xmlnode, "comments") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "comments"));
                __recommandPhoto.Username = (__xmlDoc.GetSingleNodeValue(xmlnode, "username") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "username");
                __recommandPhotoList.Add(__recommandPhoto);

            }
            return __recommandPhotoList;
        }


        /// <summary>
        /// 获得焦点图片列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="focusphotocount">返回的记录数</param>
        /// <param name="vaildDays">有效天数</param>
        /// <returns></returns>
        public Discuz.Common.Generic.List<PhotoInfo> GetFocusPhotoList(int type, int focusphotocount, int vaildDays)
        {
            //当文件未被修改时将直接返回相关记录
            if (__focusPhotoList != null)
            {
                return __focusPhotoList;
            }

            __focusPhotoList = new Discuz.Common.Generic.List<PhotoInfo>();
            IDataReader reader = AlbumPluginProvider.GetInstance().GetFocusPhotoList(type, focusphotocount, vaildDays);
            if (reader != null)
            {
                while (reader.Read())
                {
                    PhotoInfo pi = AlbumPluginProvider.GetInstance().GetPhotoEntity(reader);
                    pi.Filename = AlbumPluginProvider.GetInstance().GetThumbnailImage(pi.Filename);
                    pi.Title = pi.Title.Trim();
                    __focusPhotoList.Add(pi);
                }
                reader.Close();
            }
            return __focusPhotoList;
        }


        /// <summary>
        /// 一周热图总排行
        /// </summary>
        /// <param name="focusphotocount">返回的记录数</param>
        /// <returns></returns>
        public Discuz.Common.Generic.List<PhotoInfo> GetWeekHotPhotoList(int focusphotocount)
        {
            //当文件未被修改时将直接返回相关记录
            if (__weekHotPhotoList != null)
            {
                return __weekHotPhotoList;
            }

            __weekHotPhotoList = new Discuz.Common.Generic.List<PhotoInfo>();
            IDataReader reader = AlbumPluginProvider.GetInstance().GetFocusPhotoList(0, focusphotocount, 7);
            if (reader != null)
            {
                while (reader.Read())
                {
                    PhotoInfo pi = AlbumPluginProvider.GetInstance().GetPhotoEntity(reader);
                    pi.Filename = AlbumPluginProvider.GetInstance().GetThumbnailImage(pi.Filename);
                    pi.Title = pi.Title.Trim();
                    __weekHotPhotoList.Add(pi);
                }
                reader.Close();
            }
            return __weekHotPhotoList;
        }

        
        /// <summary>
        /// 获得推荐相册列表
        /// </summary>
        /// <param name="nodename">节点名称</param>
        /// <returns></returns>
        public Discuz.Common.Generic.List<AlbumInfo> GetRecommandAlbumList(string nodename)
        {
            Discuz.Common.Generic.List<AlbumInfo> __recommandAlbumList = null;

            switch (nodename)
            {
                case "Website":
                    {
                        __recommandAlbumList = __recommandAlbumListForWebSite; break;
                    }
                case "Spaceindex":
                    {
                        __recommandAlbumList = __recommandAlbumListForSpaceIndex; break;
                    }
                case "Albumindex":
                    {
                        __recommandAlbumList = __recommandAlbumListForAlbumIndex; break;
                    }
                default:
                    {
                        __recommandAlbumList = __recommandAlbumListForWebSite; break;
                    }
            }

            //当文件未被修改时将直接返回相关记录
            if (__recommandAlbumList != null)
            {
                return __recommandAlbumList;
            }

            __recommandAlbumList = new Discuz.Common.Generic.List<AlbumInfo>();
            XmlNodeList xmlnodelist = __xmlDoc.DocumentElement.SelectNodes("/Aggregationinfo/Aggregationpage/" + nodename + "/" + nodename + "_albumlist/Album");

            foreach (XmlNode xmlnode in xmlnodelist)
            {
                AlbumInfo album = new AlbumInfo();

                album.Albumid = (__xmlDoc.GetSingleNodeValue(xmlnode, "albumid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "albumid"));
                album.Userid = (__xmlDoc.GetSingleNodeValue(xmlnode, "userid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "userid"));
                album.Username = (__xmlDoc.GetSingleNodeValue(xmlnode, "username") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "username");
                album.Title = (__xmlDoc.GetSingleNodeValue(xmlnode, "title") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "title");
                album.Description = (__xmlDoc.GetSingleNodeValue(xmlnode, "description") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "description");
                album.Logo = (__xmlDoc.GetSingleNodeValue(xmlnode, "logo") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "logo");
                album.Password = (__xmlDoc.GetSingleNodeValue(xmlnode, "password") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "password");
                album.Imgcount = (__xmlDoc.GetSingleNodeValue(xmlnode, "imgcount") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "imgcount"));
                album.Views = (__xmlDoc.GetSingleNodeValue(xmlnode, "views") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "views"));
                album.Type = (__xmlDoc.GetSingleNodeValue(xmlnode, "type") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "type"));
                album.Createdatetime = (__xmlDoc.GetSingleNodeValue(xmlnode, "createdatetime") == null) ? "" : __xmlDoc.GetSingleNodeValue(xmlnode, "createdatetime");
                album.Albumcateid = (__xmlDoc.GetSingleNodeValue(xmlnode, "albumcateid") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "albumcateid"));
                __recommandAlbumList.Add(album);
            }

            switch (nodename)
            {
                case "Website":
                    {
                        __recommandAlbumListForWebSite = __recommandAlbumList; break;
                    }
                case "Spaceindex":
                    {
                        __recommandAlbumListForSpaceIndex = __recommandAlbumList; break;
                    }
                case "Albumindex":
                    {
                        __recommandAlbumListForAlbumIndex = __recommandAlbumList; break;
                    }
                default:
                    {
                        __recommandAlbumListForWebSite = __recommandAlbumList; break;
                    }
            }
            return __recommandAlbumList;

        }


        /// <summary>
        /// 获得焦点相册列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="focusphotocount">返回记录条数</param>
        /// <param name="vaildDays">有效天数</param>
        /// <returns></returns>
        public Discuz.Common.Generic.List<AlbumInfo> GetAlbumList(int type, int focusphotocount, int vaildDays)
        {
            //当文件未被修改时将直接返回相关记录
            if (__focusAlbumList != null)
            {
                return __focusAlbumList;
            }

            IDataReader reader = AlbumPluginProvider.GetInstance().GetAlbumListByCondition(type, focusphotocount, vaildDays);
            __focusAlbumList = new Discuz.Common.Generic.List<AlbumInfo>();
            if (reader != null)
            {
                while (reader.Read())
                {
                    __focusAlbumList.Add(AlbumPluginProvider.GetInstance().GetAlbumEntity(reader));
                }
                reader.Close();
            }
            return __focusAlbumList;
        }

        #endregion
#endif
    }
}
