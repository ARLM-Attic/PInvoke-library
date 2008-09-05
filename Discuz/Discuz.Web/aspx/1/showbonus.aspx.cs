using System;
using System.Data;
using System.Text;
using System.Web;
using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Config;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Data;

namespace Discuz.Web
{
    /// <summary>
    /// 查看悬赏主题页面
    /// </summary>
    public class showbonus : PageBase
    {
        #region 页面变量

        /// <summary>
        /// 主题信息
        /// </summary>
        public TopicInfo topic;

#if NET1
		public ShowtopicPagePostInfoCollection postlist;
        public ShowtopicPageAttachmentInfoCollection attachmentlist;
        public PrivateMessageInfoCollection pmlist = new PrivateMessageInfoCollection();
#else
        /// <summary>
        /// 帖子列表
        /// </summary>
        public List<ShowbonusPagePostInfo> postlist; // = new Discuz.Common.Generic.List<ShowtopicPagePostInfo>();

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<ShowtopicPageAttachmentInfo> attachmentlist;

        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist; //= new Discuz.Common.Generic.List<PrivateMessageInfo>();

        /// <summary>
        /// 悬赏给分日志
        /// </summary>
        public List<BonusLogInfo> bonuslogs;
#endif

        /// <summary>
        /// 投票选项列表
        /// </summary>
        public DataTable polllist;

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
        /// 所属版块Id
        /// </summary>
        public int forumid;

        /// <summary>
        /// 所属版块名称
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
        /// 是否是投票帖
        /// </summary>
        public bool ispoll;

        /// <summary>
        /// 是否允许投票
        /// </summary>
        public bool allowvote;

        /// <summary>
        /// 是否允许评分
        /// </summary>
        public bool allowrate;

        /// <summary>
        /// 主题标题
        /// </summary>
        public string topictitle;

        /// <summary>
        /// 回复标题
        /// </summary>
        public string replytitle;

        /// <summary>
        /// 主题魔法表情
        /// </summary>
        public string topicmagic = "";

        /// <summary>
        /// 主题浏览量
        /// </summary>
        public int topicviews;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;

        /// <summary>
        /// 回复帖子数
        /// </summary>
        public int postcount;

        /// <summary>
        /// 分页页数
        /// </summary>
        public int pagecount;

        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers;
        /// <summary>
        /// 表情Javascript数组
        /// </summary>
        public string smilies;

        /// <summary>
        /// 参与投票的用户列表
        /// </summary>
        public string voters;

        /// <summary>
        /// 论坛跳转链接选项
        /// </summary>
        public string forumlistboxoptions;

        /// <summary>
        /// 是否是管理者
        /// </summary>
        public int ismoder = 0;

        /// <summary>
        /// 上一次进行的管理操作
        /// </summary>
        public string moderactions;

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
        /// 是否允许 [img]标签
        /// </summary>
        public int allowimg;

        /// <summary>
        /// 是否显示评分记录
        /// </summary>
        public int showratelog;

        /// <summary>
        /// 可用的扩展积分名称列表
        /// </summary>
        public string[] score;

        /// <summary>
        /// 可用的扩展积分单位列表
        /// </summary>
        public string[] scoreunit;

        /// <summary>
        /// 是否受发贴灌水限制
        /// </summary>
        public int disablepostctrl;

        /// <summary>
        /// 积分策略信息
        /// </summary>
        public UserExtcreditsInfo userextcreditsinfo;

        /// <summary>
        /// 主题鉴定信息
        /// </summary>
        public TopicIdentify topicidentify;

        /// <summary>
        /// 用户的管理组信息
        /// </summary>
        public AdminGroupInfo admininfo = null;

        /// <summary>
        /// 当前版块信息
        /// </summary>
        public ForumInfo forum;

        /// <summary>
        /// 是否只查看楼主贴子 1:只看楼主  0:显示全部
        /// </summary>
        public string onlyauthor = "0";

        /// <summary>
        /// 当前的主题类型
        /// </summary>
        public string topictypes = "";

        /// <summary>
        /// 表情分类列表
        /// </summary>
        public DataTable smilietypes = new DataTable();

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
        /// 当为投票帖时有用,0=单选，1=多选
        /// </summary>
        public int polltype = -1;

        /// <summary>
        /// 投票结束时间
        /// </summary>
        public string pollenddatetime = "";

        /// <summary>
        /// 论坛弹出导航菜单HTML代码
        /// </summary>
        public string navhomemenu = "";

        /// <summary>
        /// 帖间通栏广告
        /// </summary>
        public string postleaderboardad = string.Empty;

        /// <summary>
        /// 是否显示短消息列表
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
        /// 每页帖子数
        /// </summary>
        public int ppp;

        /// <summary>
        /// 是否显示需要登录后访问的错误提示
        /// </summary>
        public bool needlogin = false;

        /// <summary>
        /// 第一页表情的JSON
        /// </summary>
        public string firstpagesmilies = string.Empty;

        /// <summary>
        /// 相关主题集合
        /// </summary>
        public List<TopicInfo> relatedtopics;
        /// <summary>
        /// 本版是否启用了Tag
        /// </summary>
        public bool enabletag = false;
        public bool isenddebate = true;
        #endregion       
       
        protected override void ShowPage()
        {
            headerad = "";
            footerad = "";
            postleaderboardad = "";

            doublead = "";
            floatad = "";

            allowrate = false;
            disablepostctrl = 0;
            // 获取主题ID
            topicid = DNTRequest.GetInt("topicid", -1);
            // 获取该主题的信息
            string go = DNTRequest.GetString("go").Trim().ToLower();
            int fid = DNTRequest.GetInt("forumid", 0);
            firstpagesmilies = Caches.GetSmiliesFirstPageCache();
           
            if (go == "")
            {
                fid = 0;
            }
            else if (fid == 0)
            {
                go = "";
            }

            string errmsg = "";
            switch (go)
            {
                case "prev":
                    topic = Topics.GetTopicInfo(topicid, fid, 1);
                    errmsg = "没有更旧的主题, 请返回";
                    break;
                case "next":
                    topic = Topics.GetTopicInfo(topicid, fid, 2);
                    errmsg = "没有更新的主题, 请返回";
                    break;
                default:
                    topic = Topics.GetTopicInfo(topicid);
                    errmsg = "该主题不存在";
                    break;
            }

            if (topic == null)
            {
                AddErrLine(errmsg);

                headerad = Advertisements.GetOneHeaderAd("", 0);
                footerad = Advertisements.GetOneFooterAd("", 0);
                pagewordad = Advertisements.GetPageWordAd("", 0);
                doublead = Advertisements.GetDoubleAd("", 0);
                floatad = Advertisements.GetFloatAd("", 0);
                return;
            }

            if (topic.Special != 3)
            { 
                //未结束的悬赏
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + this.ShowTopicAspxRewrite(topic.Tid,1));
                return;
            }

            ////当为投票帖时
            //if (topic.Poll == 1)
            //{
            //    polltype = Polls.GetPollType(topic.Tid);
            //    pollenddatetime = Polls.GetPollEnddatetime(topic.Tid);
            //}

            if (topic.Identify > 0)
            {
                topicidentify = Caches.GetTopicIdentify(topic.Identify);
            }
            topicid = topic.Tid;
            forumid = topic.Fid;
            forum = Forums.GetForumInfo(forumid);

            forumname = forum.Name;
            pagetitle = topic.Title;
            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);
            navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);

            fid = forumid;

            ///得到广告列表
            ///头部
            headerad = Advertisements.GetOneHeaderAd("", fid);
            footerad = Advertisements.GetOneFooterAd("", fid);
            postleaderboardad = Advertisements.GetOnePostLeaderboardAD("", fid);

            pagewordad = Advertisements.GetPageWordAd("", fid);
            doublead = Advertisements.GetDoubleAd("", fid);
            floatad = Advertisements.GetFloatAd("", fid);
     
            // 检查是否具有版主的身份
            if (useradminid != 0)
            {
                ismoder = Moderators.IsModer(useradminid, userid, forumid) ? 1 : 0;
                //得到管理组信息
                admininfo = AdminGroups.GetAdminGroupInfo(useradminid);
                if (admininfo != null)
                {
                    disablepostctrl = admininfo.Disablepostctrl;
                }
            }
            //验证不通过则返回
            if (!IsConditionsValid())
                return;

            showratelog = GeneralConfigs.GetConfig().DisplayRateCount > 0 ? 1 : 0;


            topictitle = topic.Title.Trim();
            replytitle = topictitle;
            if (replytitle.Length >= 50)
            {
                replytitle = Utils.CutString(replytitle, 0, 50) + "...";
            }

            //topicmagic = ForumUtils.ShowTopicMagic(topic.Magic);
            topicviews = topic.Views + 1 +
                         (config.TopicQueueStats == 1 ? TopicStats.GetStoredTopicViewCount(topic.Tid) : 0);
            smilies = Caches.GetSmiliesCache();
            smilietypes = Caches.GetSmilieTypesCache();


            //编辑器状态
            StringBuilder sb = new StringBuilder();
            sb.Append("var Allowhtml=1;\r\n"); //+ allhtml.ToString() + "

            parseurloff = 0;

            smileyoff = 1 - forum.Allowsmilies;
            sb.Append("var Allowsmilies=" + (1 - smileyoff).ToString() + ";\r\n");


            bbcodeoff = 1;
            if (forum.Allowbbcode == 1)
            {
                if (usergroupinfo.Allowcusbbcode == 1)
                {
                    bbcodeoff = 0;
                }
            }
            sb.Append("var Allowbbcode=" + (1 - bbcodeoff).ToString() + ";\r\n");

            usesig = ForumUtils.GetCookie("sigstatus") == "0" ? 0 : 1;

            allowimg = forum.Allowimgcode;
            sb.Append("var Allowimgcode=" + allowimg.ToString() + ";\r\n");

            AddScript(sb.ToString());
            int price = 0;
            if (topic.Special != 3)//非悬赏
            {
                //直接跳转至showtopic
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + this.ShowTopicAspxRewrite(topic.Tid, 1));
                return;
            }

            if (topic.Special == 3)//已给分的悬赏帖
            {
                bonuslogs = Bonus.GetLogs(topic.Tid);
            }

            if (topic.Moderated > 0)
            {
                moderactions = TopicAdmins.GetTopicListModeratorLog(topicid);
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

            if (newpmcount > 0)
            {
                pmlist = PrivateMessages.GetPrivateMessageCollectionForIndex(userid, 5, 1, 1);
                showpmhint = Convert.ToInt32(Users.GetShortUserInfo(userid).Newsletter) > 4;
            }


//            ispoll = false;
//            allowvote = false;
//            if (topic.Poll == 1)
//            {
//                ispoll = true;
//                polllist = Polls.GetPollList(topicid);
//                voters = Polls.GetVoters(topicid, username, out allowvote);
////				if (Polls.AllowVote(topicid, username))
////				{
////					allowvote = true;
////				}
//            }

//            if (ispoll && DateTime.Parse(pollenddatetime) < DateTime.Now)
//            {
//                allowvote = false;
//            }


            // 获取帖子总数
            //postcount = Posts.GetPostCount(topicid);
            //onlyauthor = DNTRequest.GetString("onlyauthor");
            //if (onlyauthor == "" || onlyauthor == "0")
            //{
                postcount = topic.Replies + 1;
            //}
            //else
            //{
            //    postcount = DatabaseProvider.GetInstance().GetPostCount(Posts.GetPostTableID(topicid), topicid, topic.Posterid);
            //}

            // 得到Ppp设置
            //ppp = Utils.StrToInt(ForumUtils.GetCookie("ppp"), config.Ppp);


            //if (ppp <= 0)
            //{
            //    ppp = config.Ppp;
            //}

            ////获取总页数
            //pagecount = postcount%ppp == 0 ? postcount/ppp : postcount/ppp + 1;
            //if (pagecount == 0)
            //{
            //    pagecount = 1;
            //}
            //// 得到当前用户请求的页数
            //if (DNTRequest.GetString("page").ToLower().Equals("end"))
            //{
            //    pageid = pagecount;
            //}
            //else
            //{
            //    pageid = DNTRequest.GetInt("page", 1);
            //}
            ////修正请求页数中可能的错误
            //if (pageid < 1)
            //{
            //    pageid = 1;
            //}
            //if (pageid > pagecount)
            //{
            //    pageid = pagecount;
            //}
            //判断是否为回复可见帖, hide=0为不解析[hide]标签, hide>0解析为回复可见字样, hide=-1解析为以下内容回复可见字样显示真实内容
            //将逻辑判断放入取列表的循环中处理,此处只做是否为回复人的判断，主题作者也该可见
            int hide = 1;
            if (topic.Hide == 1 && (Posts.IsReplier(topicid, userid) || ismoder == 1))
            {
                hide = -1;
            }


            //获取当前页主题列表

            DataSet ds = new DataSet();
            PostpramsInfo postpramsInfo = new PostpramsInfo();
            postpramsInfo.Fid = forum.Fid;
            postpramsInfo.Tid = topicid;
            postpramsInfo.Jammer = forum.Jammer;
            postpramsInfo.Pagesize = int.MaxValue;
            postpramsInfo.Pageindex = 1;
            postpramsInfo.Getattachperm = forum.Getattachperm;
            postpramsInfo.Usergroupid = usergroupid;
            postpramsInfo.Attachimgpost = config.Attachimgpost;
            postpramsInfo.Showattachmentpath = config.Showattachmentpath;
            postpramsInfo.Hide = hide;
            postpramsInfo.Price = price;
            postpramsInfo.Usergroupreadaccess = usergroupinfo.Readaccess;
            if (ismoder == 1)
                postpramsInfo.Usergroupreadaccess = int.MaxValue;
            postpramsInfo.CurrentUserid = userid;
            postpramsInfo.Showimages = forum.Allowimgcode;
            postpramsInfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
            postpramsInfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
            postpramsInfo.Smiliesmax = config.Smiliesmax;
            postpramsInfo.Bbcodemode = config.Bbcodemode;
            postpramsInfo.CurrentUserGroup = usergroupinfo;
            if (!(onlyauthor.Equals("") || onlyauthor.Equals("0")))
            {
                postpramsInfo.Condition =
                    string.Format(" {0}.posterid={1}", Posts.GetPostTableName(topicid), topic.Posterid);
            }
            postlist = Posts.GetPostListWithBonus(postpramsInfo, out attachmentlist, ismoder == 1);//Posts.GetPostList(postpramsInfo, out attachmentlist, ismoder == 1);

            //加载帖内广告
            inpostad = Advertisements.GetInPostAd("", fid, templatepath, postlist.Count > ppp ? ppp : postlist.Count);
            //快速发帖广告
            quickeditorad = Advertisements.GetQuickEditorAD("", fid);

            //快速编辑器背景广告
            string[] quickbgad = Advertisements.GetQuickEditorBgAd("", fid);

            if (quickbgad.Length > 1)
            {
                quickbgadimg = quickbgad[0];
                quickbgadlink = quickbgad[1];
            }

            if (postlist.Count <= 0)
            {
                AddErrLine("读取信息失败");
                return;
            }
            
            //更新页面Meta中的Description项, 提高SEO友好性
            string metadescritpion = Utils.RemoveHtml(postlist[0].Message);
            metadescritpion = metadescritpion.Length > 100 ? metadescritpion.Substring(0, 100) : metadescritpion;
            UpdateMetaInfo(config.Seokeywords, metadescritpion, config.Seohead);

            //获取相关主题集合
            enabletag = (config.Enabletag & forum.Allowtag) == 1;
            if (enabletag)
            {
                relatedtopics = Topics.GetRelatedTopics(topicid, 5);
            }


            ////得到页码链接
            //if (onlyauthor == "" || onlyauthor == "0")
            //{
            //    if (config.Aspxrewrite == 1)
            //    {
            //        pagenumbers = Utils.GetStaticPageNumbers(pageid, pagecount, "showtopic-" + topicid.ToString(), config.Extname, 8);
            //    }
            //    else
            //    {
            //        pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "showtopic.aspx?topicid=" + topicid.ToString(), 8);
            //    }
            //}
            //else
            //{
            //    pagenumbers =
            //        Utils.GetPageNumbers(pageid, pagecount, "showtopic.aspx?onlyauthor=1&topicid=" + topicid, 8);
            //}


            //更新查看次数
            //Topics.UpdateTopicViews(topicid);
            TopicStats.Track(topicid, 1);

            OnlineUsers.UpdateAction(olid, UserAction.ShowTopic.ActionID, forumid, forumname, topicid, topictitle,
                                     config.Onlinetimeout);
            forumlistboxoptions = Caches.GetForumListBoxOptionsCache();

            ////得到页码链接
            //if (onlyauthor == "" || onlyauthor == "0")
            //{
                ForumUtils.WriteCookie("referer",
                                       string.Format("showbonus.aspx?topicid={0}", topicid.ToString()));
            //}
            //else
            //{
            //    ForumUtils.WriteCookie("referer",
            //                           string.Format("showtopic.aspx?onlyauthor=1&topicid={0}&page={1}", topicid.ToString(), pageid.ToString()));
            //}

            score = Scoresets.GetValidScoreName();
            scoreunit = Scoresets.GetValidScoreUnit();

            string oldtopic = ForumUtils.GetCookie("oldtopic") + "D";
            if (oldtopic.IndexOf("D" + topic.Tid.ToString() + "D") == -1 &&
                DateTime.Now.AddMinutes(-1*600) < DateTime.Parse(topic.Lastpost))
            {
                oldtopic = "D" + topic.Tid.ToString() + Utils.CutString(oldtopic, 0, oldtopic.Length - 1);
                if (oldtopic.Length > 3000)
                {
                    oldtopic = Utils.CutString(oldtopic, 0, 3000);
                    oldtopic = Utils.CutString(oldtopic, 0, oldtopic.LastIndexOf("D"));
                }
                ForumUtils.WriteCookie("oldtopic", oldtopic);
            }

            //// 判断是否需要生成游客缓存页面
            //if (userid == -1 && pageid == 1)
            //{
            //    int topiccachemark = 100 -
            //                         (int)
            //                         (topic.Displayorder*15 + topic.Digest*10 + Math.Min(topic.Views/20, 50) +
            //                          Math.Min(topic.Replies/config.Ppp*1.5, 15));
            //    if (topiccachemark < config.Topiccachemark)
            //    {
            //        isguestcachepage = 1;
            //    }
            //}

            
           
        }

        private bool IsConditionsValid()
        {
            // 如果包含True, 则必然允许某项扩展积分的评分
            if (usergroupinfo.Raterange.IndexOf("True") > -1)
            {
                allowrate = true;
            }

            // 如果主题ID非数字
            if (topicid == -1)
            {
                AddErrLine("无效的主题ID");

                return false;
            }

            // 如果该主题不存在
            if (topic == null)
            {
                AddErrLine("不存在的主题ID");
                return false;
            }

            if (topic.Closed > 1)
            {
                topicid = topic.Closed;
                topic = Topics.GetTopicInfo(topicid);

                // 如果该主题不存在
                if (topic == null || topic.Closed > 1)
                {
                    AddErrLine("不存在的主题ID");
                    return false;
                }
            }

            if (topic.Readperm > usergroupinfo.Readaccess && topic.Posterid != userid && useradminid != 1 &&
                ismoder != 1)
            {
                AddErrLine(string.Format("本主题阅读权限为: {0}, 您当前的身份 \"{1}\" 阅读权限不够", topic.Readperm.ToString(), usergroupinfo.Grouptitle));
                if (userid == -1)
                {
                    needlogin = true;
                }
                return false;
            }

            if (topic.Displayorder == -1)
            {
                AddErrLine("此主题已被删除！");
                return false;
            }

            if (topic.Displayorder == -2)
            {
                AddErrLine("此主题未经审核！");
                return false;
            }

            if (forum.Password != "" &&
                Utils.MD5(forum.Password) != ForumUtils.GetCookie("forum" + forumid.ToString() + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                //SetBackLink("showforum-" + forumid.ToString() + config.Extname);
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + "showforum-" + forumid.ToString() + config.Extname, true);
                return false;
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
                        return false;
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
                        return false;
                    }
                }
            }

            //是否显示回复链接
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

            if ((topic.Closed == 0 && canreply) || ismoder == 1)
            {
                canreply = true;
            }
            else
            {
                canreply = false;
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

            //判断是否有发主题的权限
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

            //　如果当前用户非管理员并且论坛设定了禁止发帖时间段，当前时间如果在其中的一个时间段内，不允许用户发帖
            if (useradminid != 1 && usergroupinfo.Disableperiodctrl != 1)
            {
                string visittime = "";
                if (Scoresets.BetweenTime(config.Postbanperiods, out visittime))
                {
                    canposttopic = false;
                }
            }

            //是否有回复的权限
            if (topic.Closed == 0 && userid > -1)
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
            return true;
        }
    }
}