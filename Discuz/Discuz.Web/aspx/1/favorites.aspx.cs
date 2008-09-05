using Discuz.Common;
using Discuz.Forum;
using Discuz.Entity;
using Discuz.Plugin.Space;
using Discuz.Plugin.Album;
using Discuz.Plugin.Mall;

namespace Discuz.Web
{
	/// <summary>
	/// 添加收藏页
	/// </summary>
	public class favorites : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 将要收藏的主题信息
        /// </summary>
		public TopicInfo topic;
        /// <summary>
        /// 主题所属版块
        /// </summary>
		public int forumid;
        /// <summary>
        /// 主题所属版块名称
        /// </summary>
		public string forumname;
        /// <summary>
        /// 主题Id
        /// </summary>
		public int topicid;
        /// <summary>
        /// 主题标题
        /// </summary>
		public string topictitle;
        /// <summary>
        /// 论坛导航信息
        /// </summary>
		public string forumnav;
        /// <summary>
        /// 将要收藏的相册Id
        /// </summary>
        public int albumid;
        /// <summary>
        /// 将要收藏的日志Id
        /// </summary>
        public int blogid;
        /// <summary>
        /// 将要收藏的商品Id
        /// </summary>
        public int goodsid;
        /// <summary>
        /// 主题所属版块
        /// </summary>
        public ForumInfo forum;
       
        #endregion

        protected override void ShowPage()
		{
            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }
			
			string referer = ForumUtils.GetCookie("referer");

			// 获取主题ID
			topicid = DNTRequest.GetInt("topicid", -1);
            albumid = DNTRequest.GetInt("albumid", -1);
            blogid = DNTRequest.GetInt("postid", -1);
            goodsid = DNTRequest.GetInt("goodsid", -1);
            
            if (topicid != -1)//收藏的是主题
            {
                // 获取该主题的信息
                TopicInfo topic = Topics.GetTopicInfo(topicid);
                // 如果该主题不存在
                if (topic == null)
                {
                    AddErrLine("不存在的主题ID");
                    return;
                }

                topictitle = topic.Title;
                forumid = topic.Fid;
                forum = Forums.GetForumInfo(forumid);
                forumname = forum.Name;
                pagetitle = Utils.RemoveHtml(forum.Name);
                forumnav = forum.Pathlist;

                // 检查用户是否拥有足够权限                
                if (config.Maxfavorites <= Favorites.GetFavoritesCount(userid))
                {
                    AddErrLine("您收藏的主题数目已经达到系统设置的数目上限");
                    return;
                }

                if (Favorites.CheckFavoritesIsIN(userid, topicid) != 0)
                {
                    AddErrLine("您过去已经收藏过这个主题,请返回");
                    return;
                }

                if (Favorites.CreateFavorites(userid, topicid) > 0)
                {
                    AddMsgLine("指定主题已成功添加到收藏夹中,现在将回到上一页");
                    SetUrl(referer);
                    SetMetaRefresh();
                    SetShowBackLink(false);
                }
            }

            if (albumid != -1)
            {
                AlbumPluginBase apb = AlbumPluginProvider.GetInstance();
                if (apb == null)
                {
                    AddErrLine("未安装相册插件");
                    return;
                }
                AlbumInfo album = apb.GetAlbumInfo(albumid);

                if (album == null)
                {
                    AddErrLine("不存在的相册ID");
                    return;
                }

                // 检查用户是否拥有足够权限                
                if (config.Maxfavorites <= Favorites.GetFavoritesCount(userid))
                {
                    AddErrLine("您收藏的相册数目已经达到系统设置的数目上限");
                    return;
                }

                if (Favorites.CheckFavoritesIsIN(userid, albumid, FavoriteType.Album) != 0)
                {
                    AddErrLine("您过去已经收藏过这个相册,请返回");
                    return;
                }

                if (Favorites.CreateFavorites(userid, albumid, FavoriteType.Album) > 0)
                {
                    AddMsgLine("指定相册已成功添加到收藏夹中,现在将回到上一页");
                    SetUrl(referer);
                    SetMetaRefresh();
                    SetShowBackLink(false);
                }
            }

            if (blogid != -1)
            {
                SpacePluginBase spb = SpacePluginProvider.GetInstance();
                if (spb == null)
                {
                    AddErrLine("未安装空间插件");
                    return;
                }

                SpacePostInfo sp = spb.GetSpacepostsInfo(blogid);

                if (sp == null)
                {
                    AddErrLine("不存在的文章ID");
                    return;
                }

                // 检查用户是否拥有足够权限                
                if (config.Maxfavorites <= Favorites.GetFavoritesCount(userid))
                {
                    AddErrLine("您收藏的文章数目已经达到系统设置的数目上限");
                    return;
                }

                if (Favorites.CheckFavoritesIsIN(userid, blogid, FavoriteType.SpacePost) != 0)
                {
                    AddErrLine("您过去已经收藏过这个文章,请返回");
                    return;
                }

                if (Favorites.CreateFavorites(userid, blogid, FavoriteType.SpacePost) > 0)
                {
                    AddMsgLine("指定文章已成功添加到收藏夹中,现在将回到上一页");
                    SetUrl(referer);
                    SetMetaRefresh();
                    SetShowBackLink(false);
                }
            }

            MallPluginBase mpb = MallPluginProvider.GetInstance();

            if (mpb != null && goodsid != -1)
            {
                Goodsinfo goodsinfo = mpb.GetGoodsInfo(goodsid);

                if (goodsinfo == null)
                {
                    AddErrLine("不存在的商品ID");
                    return;
                }

                // 检查用户是否拥有足够权限                
                if (config.Maxfavorites <= Favorites.GetFavoritesCount(userid))
                {
                    AddErrLine("您收藏的文章数目已经达到系统设置的数目上限");
                    return;
                }

                if (Favorites.CheckFavoritesIsIN(userid, goodsid, FavoriteType.Goods) != 0)
                {
                    AddErrLine("您过去已经收藏过这件商品,请返回");
                    return;
                }

                if (Favorites.CreateFavorites(userid, goodsid, FavoriteType.Goods) > 0)
                {
                    AddMsgLine("指定商品已成功添加到收藏夹中,现在将回到上一页");
                    SetUrl(referer);
                    SetMetaRefresh();
                    SetShowBackLink(false);
                }
            }
		}
	}
}
