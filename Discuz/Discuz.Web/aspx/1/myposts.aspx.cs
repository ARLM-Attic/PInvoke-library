using System;
using System.Data;
using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
    /// <summary>
    /// 我的帖子
    /// </summary>
    public class myposts : PageBase
    {
        #region 页面变量
#if NET1
		public MyTopicInfoCollection topics;
#else
        /// <summary>
        /// 帖子所属的主题列表
        /// </summary>
        public List<MyTopicInfo> topics;
#endif
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;
        /// <summary>
        /// 总页数
        /// </summary>
        public int pagecount;
        /// <summary>
        /// 主题总数
        /// </summary>
        public int topiccount;
        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers;
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public UserInfo user = new UserInfo();
        #endregion

        private int pagesize = 16;

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }

            user = Users.GetUserInfo(userid);

            //得到当前用户请求的页数
            pageid = DNTRequest.GetInt("page", 1);
            //获取主题总数
            topiccount = Topics.GetTopicsCountbyReplyUserId(this.userid);
            //获取总页数
            pagecount = topiccount%pagesize == 0 ? topiccount/pagesize : topiccount/pagesize + 1;
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

            this.topics = Topics.GetTopicsByReplyUserId(this.userid, pageid, pagesize, 600, config.Hottopic);

            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "myposts.aspx", 8);
        }
    }
}