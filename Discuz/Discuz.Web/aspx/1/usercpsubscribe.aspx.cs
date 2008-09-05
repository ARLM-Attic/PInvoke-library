using System;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
    /// <summary>
    /// 查看主题订阅页面
    /// </summary>
    public class usercpsubscribe : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 收藏列表
        /// </summary>
        public DataTable favoriteslist;

        /// <summary>
        /// 收藏类型列表
        /// </summary>
        public int typeid;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;

        /// <summary>
        /// 收藏的主题/相册/日志数
        /// </summary>
        public int topiccount;

        /// <summary>
        /// 分页页数
        /// </summary>
        public int pagecount;

        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();
        #endregion

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }

            user = Users.GetUserInfo(userid);

            typeid = DNTRequest.GetInt("typeid", 0);
            FavoriteType type = FavoriteType.ForumTopic;
            switch (typeid)
            {
                case 1:
                    type = FavoriteType.Album;
                    break;
                case 2:
                    type = FavoriteType.SpacePost;
                    break;
                case 3:
                    type = FavoriteType.Goods;
                    break;
                default:
                    type = FavoriteType.ForumTopic;
                    break;
            }
            if (DNTRequest.IsPost())
            {
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }
                string titemid = DNTRequest.GetFormString("titemid");

                if (Utils.StrIsNullOrEmpty(titemid))
                {
                    AddErrLine("您未选中任何数据信息，当前操作失败！");
                    return;
                }

                if(!Utils.IsNumericList(titemid))
                {
                    return;
                }
                string[] pmitemid = Utils.SplitString(titemid, ",");

                int retval = Favorites.DeleteFavorites(userid, pmitemid, type);

                if (retval == -1)
                {
                    AddErrLine("参数无效");
                    return;
                }

                SetShowBackLink(false);
                AddMsgLine("删除完毕");
            }
            else
            {
                //得到当前用户请求的页数
                pageid = DNTRequest.GetInt("page", 1);
                //获取主题总数
                topiccount = Favorites.GetFavoritesCount(userid, type);
                //获取总页数
                pagecount = topiccount%16 == 0 ? topiccount/16 : topiccount/16 + 1;
                if (pagecount == 0)
                {
                    pagecount = 1;
                }
                //修正请求页数中可能的错误
                if (pageid < 1)
                {
                    pageid = 1;
                }
                if (pageid > pagecount)
                {
                    pageid = pagecount;
                }

                favoriteslist = Favorites.GetFavoritesList(userid, 16, pageid, type);

                pagenumbers =
                    Utils.GetPageNumbers(pageid, pagecount, string.Format("usercpsubscribe.aspx?typeid={0}", typeid.ToString()), 8);
            }
        }
    }
}