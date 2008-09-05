using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Entity;
using Discuz.Config;
using System.IO;
using Discuz.Album.Data;

namespace Discuz.Album.Pages
{
    /// <summary>
    /// 相册列表页
    /// </summary>
    public class showalbumlist : PageBase
    {
        #region 页面数量

#if NET1
		public AlbumInfoCollection albumlist;
        public AlbumCategoryInfoCollection albumcategorylist;
#else
        /// <summary>
        /// 相册列表
        /// </summary>
        public List<AlbumInfo> albumlist = new List<AlbumInfo>();

        /// <summary>
        /// 相册分类列表
        /// </summary>
        public List<AlbumCategoryInfo> albumcategorylist = new List<AlbumCategoryInfo>();
#endif

        /// <summary>
        /// 当前相册分类
        /// </summary>
        public AlbumCategoryInfo currentalbumcategory;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int currentpage = 1;

        /// <summary>
        /// 相册所属用户的用户Id
        /// </summary>
        public int albumsuserid = 0;

        /// <summary>
        /// 相册数量
        /// </summary>
        public int albumscount = 0;

        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers = string.Empty;

        /// <summary>
        /// 分页总数
        /// </summary>
        public int pagecount = 0;

        /// <summary>
        /// 当前相册分类Id
        /// </summary>
        public int currentcate = 0;

        /// <summary>
        /// 相册所属用户的用户名
        /// </summary>
        public string albumusername = string.Empty;

        /// <summary>
        /// 上一页链接
        /// </summary>
        public string prevpage = string.Empty;

        /// <summary>
        /// 下一页链接
        /// </summary>
        public string nextpage = string.Empty;

        /// <summary>
        /// 相册弹出导航菜单HTML代码
        /// </summary>
        public string navhomemenu = string.Empty;

        #endregion

        private int pageSize = 16;

        protected override void ShowPage()
        {
            pagetitle = "相册列表";

            if (config.Enablealbum != 1)
            {
                AddErrLine("相册功能已被关闭");
                return;
            }

            navhomemenu = Albums.GetPhotoListMenuDivCache();
            currentpage = DNTRequest.GetInt("page", 1);

            albumsuserid = DNTRequest.GetInt("uid", 0);

            currentcate = DNTRequest.GetInt("cate", 0);

            if (currentcate != 0)
            {
                currentalbumcategory = DTOProvider.GetAlbumCategory(currentcate);
                pagetitle = currentalbumcategory.Title;
            }
            if (albumsuserid != 0)
            {
                albumusername = Users.GetUserName(albumsuserid);
                pagetitle = albumusername + "的" + pagetitle;
            }

            albumscount = DTOProvider.GetSpaceAlbumListCount(albumsuserid, currentcate);

            pagecount = albumscount%pageSize == 0 ? albumscount/pageSize : albumscount/pageSize + 1;

            if (pagecount == 0)
            {
                pagecount = 1;
            }

            if (currentpage < 1)
            {
                currentpage = 1;
            }

            if (currentpage > pagecount)
            {
                currentpage = pagecount;
            }

            string parm = string.Empty;
            if (currentcate != 0)
            {
                parm = "cate=" + currentcate;

                if (albumsuserid != 0)
                {
                    parm += "&uid=" + albumsuserid;
                }
            }
            else
            {
                if (albumsuserid != 0)
                {
                    parm += "uid=" + albumsuserid;
                }
            }

            if (parm != string.Empty)
            {
                parm += "&page=";
            }
            else
            {
                parm += "page=";
            }

            if (currentpage > 1)
            {
                prevpage = "<a href='showalbumlist.aspx?" + parm + (currentpage - 1) + "'>上一页</a>";
            }

            if (currentpage < pagecount)
            {
                nextpage = "<a href='showalbumlist.aspx?" + parm + (currentpage + 1) + "'>下一页</a>";
            }

            pagenumbers = Utils.GetPageNumbers(currentpage, pagecount, "showalbumlist.aspx", 8);
            albumlist = DTOProvider.GetSpaceAlbumList(albumsuserid, currentcate, pageSize, currentpage);

            if (albumlist.Count == 0)
            {
                AddMsgLine("暂无相册");
            }

            albumcategorylist = DTOProvider.GetAlbumCategory();
        }
    }
}