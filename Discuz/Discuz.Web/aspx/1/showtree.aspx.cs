using System;
using System.Data;
using System.Web;
using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Web
{
    /// <summary>
    /// showforum 的摘要说明.
    /// </summary>
    public class showtree : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 主题信息
        /// </summary>
        public TopicInfo topic;

        /// <summary>
        /// 主题所属帖子
        /// </summary>
        public ShowtopicPagePostInfo post;

        /// <summary>
        /// 投票选项列表
        /// </summary>
        public DataTable polloptionlist;

        /// <summary>
        /// 投票帖类型
        /// </summary>
        public PollInfo pollinfo;

        /// <summary>
        /// 是否显示投票结果
        /// </summary>
        public bool showpollresult = true;

        //public DataRow attachment;

#if NET1		
        public ShowtopicPageAttachmentInfoCollection attachmentlist;
        public PrivateMessageInfoCollection pmlist = new PrivateMessageInfoCollection();
#else
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<ShowtopicPageAttachmentInfo> attachmentlist = new List<ShowtopicPageAttachmentInfo>();

        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist = new List<PrivateMessageInfo>();


        /// <summary>
        /// 悬赏给分日志
        /// </summary>
        public List<BonusLogInfo> bonuslogs;
#endif

        /// <summary>
        /// 主题回复列表
        /// </summary>
        public DataTable posttree;

        /// <summary>
        /// 页内文字广告
        /// </summary>
        public string[] pagewordad;

        /// <summary>
        /// 对联广告
        /// </summary>
        public string doublead;

        /// <summary>
        /// 浮动广告
        /// </summary>
        public string floatad;

        /// <summary>
        /// 快速编辑器背景广告
        /// </summary>
        public string quickbgadimg = string.Empty;
        public string quickbgadlink = string.Empty;

        /// <summary>
        /// 版块Id
        /// </summary>
        public int forumid;

        /// <summary>
        /// 版块名称
        /// </summary>
        public string forumname;

        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav;

        /// <summary>
        /// 主题Id
        /// </summary>
        public int topicid;

        /// <summary>
        /// 帖子Id
        /// </summary>
        public int postid;

        /// <summary>
        /// 是否是投票帖
        /// </summary>
        public bool ispoll;

        /// <summary>
        /// 是否允许投票
        /// </summary>
        public bool allowvote;

        /// <summary>
        /// 主题标题
        /// </summary>
        public string topictitle;

        /// <summary>
        /// 回复标题
        /// </summary>
        public string replytitle;

        /// <summary>
        /// 主题浏览量
        /// </summary>
        public int topicviews;

        /// <summary>
        /// 主题帖子数
        /// </summary>
        public int postcount;

        /// <summary>
        /// 表情Javascript数组
        /// </summary>
        public string smilies;

        /// <summary>
        /// 参与投票的用户列表
        /// </summary>
        public string voters;

        /// <summary>
        /// 当前用户是否是管理者
        /// </summary>
        public int ismoder;

        /// <summary>
        /// 是否显示评分记录
        /// </summary>
        public int showratelog;

        /// <summary>
        /// 是否解析URL
        /// </summary>
        public int parseurloff;

        /// <summary>
        /// 是否解析表情
        /// </summary>
        public int smileyoff;

        /// <summary>
        /// 是否解析 Discuz!NT 代码
        /// </summary>
        public int bbcodeoff;

        /// <summary>
        /// 是否使用签名
        /// </summary>
        public int usesig;

        /// <summary>
        /// 可用的扩展积分名称
        /// </summary>
        public string[] score;

        /// <summary>
        /// 可用的扩展积分单位
        /// </summary>
        public string[] scoreunit;

        /// <summary>
        /// 
        /// </summary>
        public string ignorelink;

        /// <summary>
        /// 主题鉴定信息
        /// </summary>
        public int quickpost;

        /// <summary>
        /// 是否受发贴灌水限制
        /// </summary>
        public int disablepostctrl;

        /// <summary>
        /// 主题鉴定信息
        /// </summary>
        public TopicIdentify topicidentify;

        /// <summary>
        /// 当前用户所属的管理组信息
        /// </summary>
        public AdminGroupInfo admininfo;

        /// <summary>
        /// 当前版块信息
        /// </summary>
        public ForumInfo forum;

        /// <summary>
        /// 是否允许下载附件
        /// </summary>
        public DataTable smilietypes = new DataTable();

        /// <summary>
        /// 是否显示回复链接
        /// </summary>
        public bool isshowreplaylink = false;

        /// <summary>
        /// 是否显示下载链接
        /// </summary>
        public bool allowdownloadattach = false;

        /// <summary>
        /// 是否有发表主题的权限
        /// </summary>
        public bool canposttopic = false;

        /// <summary>
        /// 是否有回复的权限
        /// </summary>
        public bool canreply = false;

        /// <summary>
        /// 论坛弹出导航菜单HTML代码
        /// </summary>
        public string navhomemenu = "";

        /// <summary>
        /// 是否显示短消息提示
        /// </summary>
        public bool showpmhint = false;

        /// <summary>
        /// 帖内广告
        /// </summary>
        public string inpostad = string.Empty;

        /// <summary>
        /// 快速发帖广告
        /// </summary>
        public string quickeditorad = string.Empty;

        /// <summary>
        /// 主题分类列表
        /// </summary>
        public string topictypes = "";

        /// <summary>
        /// 积分策略信息
        /// </summary>
        public UserExtcreditsInfo userextcreditsinfo;

        /// <summary>
        /// 是否显示需要登录后访问的错误提示
        /// </summary>
        public bool needlogin = false;

        /// <summary>
        /// 第一页表情的JSON
        /// </summary>
        public string firstpagesmilies = string.Empty;

        /// <summary>
        /// 本版是否启用Tag
        /// </summary>
        public bool enabletag = false;
        #endregion

       
		protected override void ShowPage()
		{
			ismoder = 0;
			disablepostctrl = 0;
			quickpost = 1;
			forumnav = "";
			topictitle = "";
			ignorelink = "";
            forumid = 0;
            firstpagesmilies = Caches.GetSmiliesFirstPageCache();
            //　如果当前用户非管理员并且论坛设定了禁止发帖时间段，当前时间如果在其中的一个时间段内，不允许用户发帖
            if (useradminid != 1 && usergroupinfo.Disableperiodctrl != 1)
            {
                string visittime = "";
                if (Scoresets.BetweenTime(config.Postbanperiods, out visittime))
                {
                    quickpost = 0;
                }
            }

            ///得到广告列表
            ///头部
            headerad = Advertisements.GetOneHeaderAd("", forumid);
            footerad = Advertisements.GetOneFooterAd("", forumid);

            pagewordad = Advertisements.GetPageWordAd("", forumid);
            doublead = Advertisements.GetDoubleAd("", forumid);
            floatad = Advertisements.GetFloatAd("", forumid);
            inpostad = Advertisements.GetInPostAd("", forumid, templatepath, 1);
            //快速发帖广告
            quickeditorad = Advertisements.GetQuickEditorAD("", forumid);

            //快速编辑器背景广告
            string[] quickbgad = Advertisements.GetQuickEditorBgAd("", forumid);

            if (quickbgad.Length > 1)
            {
                quickbgadimg = quickbgad[0];
                quickbgadlink = quickbgad[1];
            }

            topicid = DNTRequest.GetInt("topicid", -1);
            postid = DNTRequest.GetInt("postid", -1);
            // 如果主题ID非数字则判断帖子ID
            if (topicid != -1)
            {
                postid = DNTRequest.GetInt("postid", -1);
                if (postid == -1)
                {
                    // 如果只有主题id则现实主题的第一个帖子的内容
                    postid = Posts.GetFirstPostId(topicid);
                }
            }
            //"+BaseConfigs.GetTablePrefix+"getsinglepost
            PostInfo __postinfo = Posts.GetPostInfo(topicid, postid);
            if (__postinfo == null)
            {
                //
                AddErrLine("错误的主题");
                return;
            }
            else
            {
                if (topicid == -1)
                {
                    topicid = __postinfo.Tid;
                }
            }
            if (topicid != __postinfo.Tid)
            {
                AddErrLine("主题ID无效");
                return;
            }
            smilies = Caches.GetSmiliesCache();
            smilietypes = Caches.GetSmilieTypesCache();

            //编辑器状态
            parseurloff = 0;

            usesig = config.Showsignatures;


            // 获取该主题的信息
            topic = Topics.GetTopicInfo(topicid);
            // 如果该主题不存在
            if (topic == null)
            {
                AddErrLine("不存在的主题ID");
                return;
            }

            if (topic.Displayorder == -1)
            {
                AddErrLine("此主题已被删除！");
                return;
            }

            if (topic.Displayorder == -2)
            {
                AddErrLine("此主题未经审核！");
                return;
            }

            if (topic.Identify > 0)
            {
                topicidentify = Caches.GetTopicIdentify(topic.Identify);
                //原来的鉴定项已被移除，则将其恢复成未被鉴定的主题
                if (topic.Identify != topicidentify.Identifyid)
                {
                    TopicAdmins.IdentifyTopic(topicid.ToString(), 0);
                }
            }

            forumid = topic.Fid;
            // 检查是否具有版主的身份
            ismoder = Moderators.IsModer(useradminid, userid, forumid) ? 1 : 0;
            admininfo = AdminGroups.GetAdminGroupInfo(useradminid);
            if (admininfo != null)
            {
                disablepostctrl = admininfo.Disablepostctrl;
            }

            if (topic.Readperm > usergroupinfo.Readaccess && topic.Posterid != userid && useradminid != 1 &&
                ismoder == 0)
            {
                AddErrLine("本主题阅读权限为: " + topic.Readperm.ToString() + ", 您当前的身份 \"" + usergroupinfo.Grouptitle +
                           "\" 阅读权限不够");
                if (userid == -1)
                {
                    needlogin = true;
                }
                return;
            }


            topictitle = topic.Title.Trim();
            replytitle = topictitle;
            if (replytitle.Length >= 50)
            {
                replytitle = Utils.CutString(replytitle, 0, 50) + "...";
            }

            topicviews = topic.Views + 1;

            forum = Forums.GetForumInfo(forumid);
            enabletag = (config.Enabletag & forum.Allowtag) == 1;
            forumname = forum.Name;
            pagetitle = topic.Title;
            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);
            navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);
            smileyoff = 1 - forum.Allowsmilies;
            bbcodeoff = 1;
            if (forum.Allowbbcode == 1)
            {
                if (usergroupinfo.Allowcusbbcode == 1)
                {
                    bbcodeoff = 0;
                }
            }

            if (forum.Password != "" &&
                Utils.MD5(forum.Password) != ForumUtils.GetCookie("forum" + forumid.ToString() + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                //SetBackLink("showforum-" + forumid.ToString() + config.Extname);
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + "showforum-" + forumid.ToString() + config.Extname, true);
                return;
            }


            if (!Forums.AllowViewByUserID(forum.Permuserlist, userid)) //判断当前用户在当前版块浏览权限
            {
                if (forum.Viewperm == null || forum.Viewperm == string.Empty) //当板块权限为空时，按照用户组权限
                {
                    if (usergroupinfo.Allowvisit != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有浏览该版块的权限");
                        if (userid == -1)
                        {
                            needlogin = true;
                        }
                        return;
                    }
                }
                else //当板块权限不为空，按照板块权限
                {
                    if (!Forums.AllowView(forum.Viewperm, usergroupid))
                    {
                        AddErrLine("您没有浏览该版块的权限");
                        if (userid == -1)
                        {
                            needlogin = true;
                        }
                        return;
                    }
                }
            }

            //是否显示回复链接
            if (Forums.AllowReplyByUserID(forum.Permuserlist, userid))
            {
                isshowreplaylink = true;
            }
            else
            {
                if (forum.Replyperm == null || forum.Replyperm == string.Empty) //权限设置为空时，根据用户组权限判断
                {
                    // 验证用户是否有发表主题的权限
                    if (usergroupinfo.Allowreply == 1)
                    {
                        isshowreplaylink = true;
                    }
                }
                else if (Forums.AllowReply(forum.Replyperm, usergroupid))
                {
                    isshowreplaylink = true;
                }
            }

            //当前用户是否有允许下载附件权限
            if (Forums.AllowGetAttachByUserID(forum.Permuserlist, userid))
            {
                allowdownloadattach = true;
            }
            else
            {
                if (forum.Getattachperm == null || forum.Getattachperm == string.Empty) //权限设置为空时，根据用户组权限判断
                {
                    // 验证用户是否有有允许下载附件权限
                    if (usergroupinfo.Allowgetattach == 1)
                    {
                        allowdownloadattach = true;
                    }
                }
                else if (Forums.AllowGetAttach(forum.Getattachperm, usergroupid))
                {
                    allowdownloadattach = true;
                }
            }
            try
            {
                topictypes = Caches.GetTopicTypeArray()[topic.Typeid].ToString();
                topictypes = topictypes != "" ? "[" + topictypes + "]" : "";
            }
            catch
            {
                ;
            }

            userextcreditsinfo = Scoresets.GetScoreSet(Scoresets.GetCreditsTrans());

            //判断是否有发主题的权限
            if (quickpost == 1)
            {
                if (userid > -1 && Forums.AllowPostByUserID(forum.Permuserlist, userid))
                {
                    canposttopic = true;
                }

                if (forum.Postperm == null || forum.Postperm == string.Empty) //权限设置为空时，根据用户组权限判断
                {
                    // 验证用户是否有发表主题的权限
                    if (usergroupinfo.Allowpost == 1)
                    {
                        canposttopic = true;
                    }
                }
                else if (Forums.AllowPost(forum.Postperm, usergroupid))
                {
                    canposttopic = true;
                }
            }


            //是否有回复的权限
            if (topic.Closed == 0 || ismoder == 1)
            {
                if (config.Fastpost == 2 || config.Fastpost == 3)
                {
                    if (Forums.AllowReplyByUserID(forum.Permuserlist, userid))
                    {
                        canreply = true;
                    }
                    else
                    {
                        if (forum.Replyperm == null || forum.Replyperm == string.Empty) //权限设置为空时，根据用户组权限判断
                        {
                            // 验证用户是否有发表主题的权限
                            if (usergroupinfo.Allowreply == 1)
                            {
                                canreply = true;
                            }
                        }
                        else if (Forums.AllowReply(forum.Replyperm, usergroupid))
                        {
                            canreply = true;
                        }
                    }
                }
            }


            showratelog = GeneralConfigs.GetConfig().DisplayRateCount > 0 ? 1 : 0;


            //购买帖子操作
            //判断是否为回复可见帖, price=0为非购买可见(正常), price>0 为购买可见, price=-1为购买可见但当前用户已购买
            int price = 0;
            if (topic.Special == 0)//普通主题
            {
                if (topic.Price > 0 && userid != topic.Posterid && ismoder != 1)
                {
                    price = topic.Price;
                    //时间乘以-1是因为当Configs.GetMaxChargeSpan()==0时,帖子始终为购买帖
                    if (PaymentLogs.IsBuyer(topicid, userid) ||
                        (Utils.StrDateDiffHours(topic.Postdatetime, Scoresets.GetMaxChargeSpan()) > 0 &&
                         Scoresets.GetMaxChargeSpan() != 0)) //判断当前用户是否已经购买
                    {
                        price = -1;
                    }
                }
                if (price > 0)
                {
                    HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + "buytopic.aspx?topicid=" + topic.Tid);
                    return;
                }

            }

            if (newpmcount > 0)
            {
                pmlist = PrivateMessages.GetPrivateMessageCollectionForIndex(userid, 5, 1, 1);
                if (DNTRequest.GetUrl().IndexOf("?") != -1)
                {
                    ignorelink = DNTRequest.GetRawUrl() + "?ignore=yes";
                }
                else
                {
                    ignorelink = DNTRequest.GetRawUrl() + "&ignore=yes";
                }

                showpmhint = Convert.ToInt32(Users.GetShortUserInfo(userid).Newsletter) > 4;
            }


            ispoll = false;
            allowvote = false;
            if (topic.Special == 1)
            {
                ispoll = true;
                polloptionlist = Polls.GetPollOptionList(topicid);
                pollinfo = Polls.GetPollInfo(topicid);
                voters = Polls.GetVoters(topicid, userid, username, out allowvote);

                if (pollinfo.Visible == 1 && //当为投票才可见时
                  (allowvote || (userid == -1 && Utils.InArray(topicid.ToString(), ForumUtils.GetCookie("dnt_polled")))))//当允许投票或为游客时
                {
                    showpollresult = false;
                }
            }

            if (topic.Special == 3)//已给分的悬赏帖
            {
                bonuslogs = Bonus.GetLogs(topic.Tid);
            }


            if (ispoll && DateTime.Parse(pollinfo.Expiration) < DateTime.Now)
            {
                allowvote = false;
            }

            postcount = topic.Replies + 1;


            //判断是否为回复可见帖, hide=0为不解析[hide]标签, hide>0解析为回复可见字样, hide=-1解析为以下内容回复可见字样显示真实内容
            //将逻辑判断放入取列表的循环中处理,此处只做是否为回复人的判断
            int hide = 1;
            if (topic.Hide == 1 && (Posts.IsReplier(topicid, userid) || ismoder == 1))
            {
                hide = -1;
            }

            PostpramsInfo postpramsInfo = new PostpramsInfo();
            postpramsInfo.Tid = forum.Fid;
            postpramsInfo.Tid = topicid;
            postpramsInfo.Pid = postid;
            postpramsInfo.Jammer = forum.Jammer;
            postpramsInfo.Pagesize = 1;
            postpramsInfo.Pageindex = 1;
            postpramsInfo.Getattachperm = forum.Getattachperm;
            postpramsInfo.Usergroupid = usergroupid;
            postpramsInfo.Attachimgpost = config.Attachimgpost;
            postpramsInfo.Showattachmentpath = config.Showattachmentpath;
            postpramsInfo.Hide = hide;
            postpramsInfo.Price = price;
            postpramsInfo.Ubbmode = false;
            postpramsInfo.Usergroupreadaccess = usergroupinfo.Readaccess;

            if (ismoder == 1)
                postpramsInfo.Usergroupreadaccess = int.MaxValue;

            postpramsInfo.CurrentUserid = userid;
            postpramsInfo.CurrentUserGroup = usergroupinfo;


            postpramsInfo.Showimages = forum.Allowimgcode;
            postpramsInfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
            postpramsInfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
            postpramsInfo.Smiliesmax = config.Smiliesmax;

            post = Posts.GetSinglePost(postpramsInfo, out attachmentlist, ismoder == 1);

            if (post == null)
            {
                AddErrLine("读取信息失败");
                return;
            }

            posttree = Posts.GetPostTree(topicid);

            //更新查看次数
            //Topics.UpdateTopicViews(topicid);
            TopicStats.Track(topicid, 1);

            OnlineUsers.UpdateAction(olid, UserAction.ShowTopic.ActionID, forumid, forumname, topicid, topictitle,
                                     config.Onlinetimeout);
            score = Scoresets.GetValidScoreName();
            scoreunit = Scoresets.GetValidScoreUnit();

            //得到用户设置的每页显示帖子数各短消息数量
            if (userid != -1)
            {
                ShortUserInfo __userinfo = Users.GetShortUserInfo(userid);
                if (__userinfo != null)
                {
                    if (__userinfo.Newpm == 0)
                    {
                        newpmcount = 0;
                    }
                }
            }
        }
    }
}
