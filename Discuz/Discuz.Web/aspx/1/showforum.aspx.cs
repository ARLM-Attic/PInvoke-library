using System;
using System.Data;
using System.Text;
using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Data;
using System.Web;
using Discuz.Config;
using Discuz.Plugin.Mall;

namespace Discuz.Web
{
    /// <summary>
    /// 查看版块页面
    /// </summary>
    public class showforum : PageBase
    {
        #region 页面变量

#if NET1
		public OnlineUserInfoCollection onlineuserlist = new OnlineUserInfoCollection();
        public ShowforumPageTopicInfoCollection topiclist = new ShowforumPageTopicInfoCollection();
        public ShowforumPageTopicInfoCollection toptopiclist = new ShowforumPageTopicInfoCollection();
        public IndexPageForumInfoCollection subforumlist = new IndexPageForumInfoCollection();
        public PrivateMessageInfoCollection pmlist = new PrivateMessageInfoCollection();
#else
        /// <summary>
        /// 当前版块在线用户列表
        /// </summary>
        public List<OnlineUserInfo> onlineuserlist; // = new Discuz.Common.Generic.List<OnlineUserInfo>();

        /// <summary>
        /// 主题列表
        /// </summary>
        public Topics.ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> topiclist =
            new Topics.ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();

        /// <summary>
        /// 置顶主题列表
        /// </summary>
        public Topics.ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> toptopiclist =
            new Topics.ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();

        /// <summary>
        /// 子版块列表
        /// </summary>
        public List<IndexPageForumInfo> subforumlist; 

        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist; 
#endif

        /// <summary>
        /// 在线图例列表
        /// </summary>
        public string onlineiconlist = "";

        /// <summary>
        /// 公告列表
        /// </summary>
        public DataTable announcementlist = new DataTable();

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
        /// Silverlight广告
        /// </summary>
        public string mediaad;
        
        /// <summary>
        /// 快速发帖广告
        /// </summary>
        public string quickeditorad = string.Empty;

        /// <summary>
        /// 快速编辑器背景广告
        /// </summary>
        public string quickbgadimg = string.Empty;
        public string quickbgadlink = string.Empty;

        /// <summary>
        /// 当前版块信息
        /// </summary>
        public ForumInfo forum = new ForumInfo();

        /// <summary>
        /// 用户的管理组信息
        /// </summary>
        public AdminGroupInfo admingroupinfo = new AdminGroupInfo();

        /// <summary>
        /// 积分策略
        /// </summary>
        public UserExtcreditsInfo userextcreditsinfo = new UserExtcreditsInfo();

        /// <summary>
        /// 当前版块总在线用户数
        /// </summary>
        public int forumtotalonline;

        /// <summary>
        /// 当前版块总在线注册用户数
        /// </summary>
        public int forumtotalonlineuser;

        /// <summary>
        /// 当前版块总在线游客数
        /// </summary>
        public int forumtotalonlineguest;

        /// <summary>
        /// 当前版块在线隐身用户数
        /// </summary>
        public int forumtotalonlineinvisibleuser;

        /// <summary>
        /// 当前版块ID
        /// </summary>
        public int forumid;

        /// <summary>
        /// 当前版块名称
        /// </summary>
        public string forumname;

        /// <summary>
        /// 子版块数
        /// </summary>
        public int subforumcount;

        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav = "";

        /// <summary>
        /// 是否显示版块密码提示 1为显示, 0不显示
        /// </summary>
        public int showforumlogin;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;

        /// <summary>
        /// 主题总数
        /// </summary>
        public int topiccount = 0;

        /// <summary>
        /// 分页总数
        /// </summary>
        public int pagecount = 1;

        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers = "";

        /// <summary>
        /// 置顶主题数
        /// </summary>
        public int toptopiccount = 0;

        /// <summary>
        /// 版块跳转链接选项
        /// </summary>
        public string forumlistboxoptions;

        /// <summary>
        /// 最近访问的版块选项
        /// </summary>
        public string visitedforumsoptions;

        /// <summary>
        /// 是否允许Rss订阅
        /// </summary>
        public int forumallowrss;

        /// <summary>
        /// 是否显示在线列表
        /// </summary>
        public bool showforumonline;

        /// <summary>
        /// 是否显示分隔符
        /// </summary>
        public bool showsplitter;

        /// <summary>
        /// 是否受发帖控制限制
        /// </summary>
        public int disablepostctrl;

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
        /// 是否允许 [img] 标签
        /// </summary>
        public int allowimg;

        /// <summary>
        /// 表情Javascript数组
        /// </summary>
        public string smilies;

        /// <summary>
        /// 每页显示主题数
        /// </summary>
        public int tpp;

        /// <summary>
        /// 每页显示帖子数
        /// </summary>
        public int ppp;

        /// <summary>
        /// 是否是管理者
        /// </summary>
        public bool ismoder = false;

        /// <summary>
        /// 主题分类选项
        /// </summary>
        public string topictypeselectoptions; //当前版块的主题类型选项

        /// <summary>
        /// 当前版块的主题类型链接串
        /// </summary>
        public string topictypeselectlink;

        /// <summary>
        /// 主题分类Id
        /// </summary>
        public int topictypeid = 0; //主题类型

        /// <summary>
        /// 过滤主题类型
        /// </summary>
        public string filter = "";

        /// <summary>
        /// 表情分类列表
        /// </summary>
        public DataTable smilietypes = new DataTable();

        /// <summary>
        /// 是否允许发表主题
        /// </summary>
        public bool canposttopic = false; //是否有发表主题的权限

        /// <summary>
        /// 是否允许快速发表主题
        /// </summary>
        public bool canquickpost = false; //是否有快速发表主题的权限

        /// <summary>
        /// 论坛弹出导航菜单HTML代码
        /// </summary>
        public string navhomemenu = "";

        /// <summary>
        /// 是否显示短消息提示
        /// </summary>
        public bool showpmhint = false;

        /// <summary>
        /// 是否显示需要登录后访问的错误提示
        /// </summary>
        public bool needlogin = false;

        /// <summary>
        /// 排序方式
        /// </summary>
        public int order = 1; //排序字段
        /// <summary>
        /// 时间范围
        /// </summary>
        public int cond = 0;
        /// <summary>
        /// 排序方向
        /// </summary>
        public int direct = 1; //排序方向[默认：降序]

        /// <summary>
        /// 第一页表情的JSON
        /// </summary>
        public string firstpagesmilies = string.Empty;

        /// <summary>
        /// 获取绑定相关版块的商品分类信息
        /// </summary>
        public string goodscategoryfid = "{}";

        #endregion

        private string condition = ""; //查询条件

       
        protected override void ShowPage()
        {
            if (config.Enablemall > 0)
            {
                goodscategoryfid = Discuz.Plugin.Mall.MallPluginProvider.GetInstance().GetGoodsCategoryWithFid();
            }

            forumnav = "";
            forumallowrss = 0;
            forumid = DNTRequest.GetInt("forumid", -1);
            //ignorelink = "";

            ///得到广告列表
            ///头部
            headerad = Advertisements.GetOneHeaderAd("", forumid);
            footerad = Advertisements.GetOneFooterAd("", forumid);

            pagewordad = Advertisements.GetPageWordAd("", forumid);
            doublead = Advertisements.GetDoubleAd("", forumid);
            floatad = Advertisements.GetFloatAd("", forumid);
            mediaad = Advertisements.GetMediaAd(templatepath, "", forumid);
            //快速发帖广告
            quickeditorad = Advertisements.GetQuickEditorAD("", forumid);

            //快速编辑器背景广告
            string[] quickbgad = Advertisements.GetQuickEditorBgAd("", forumid);

            if (quickbgad.Length > 1)
            {
                quickbgadimg = quickbgad[0];
                quickbgadlink = quickbgad[1];
            }

            disablepostctrl = 0;
            smilies = Caches.GetSmiliesCache();
            smilietypes = Caches.GetSmilieTypesCache();
            firstpagesmilies = Caches.GetSmiliesFirstPageCache();
         

            if (userid > 0 && useradminid > 0)
            {
                admingroupinfo = AdminGroups.GetAdminGroupInfo(useradminid);
            }

            if (admingroupinfo != null)
            {
                this.disablepostctrl = admingroupinfo.Disablepostctrl;
            }


            if (forumid == -1)
            {
                AddLinkRss("tools/rss.aspx", "最新主题");
                AddErrLine("无效的版块ID");

                return;
            }
            else
            {
                forum = Forums.GetForumInfo(forumid);
                if (forum == null)
                {
                    if (config.Rssstatus == 1)
                    {
                        AddLinkRss("tools/rss.aspx", Utils.EncodeHtml(config.Forumtitle) + " 最新主题");
                    }
                    AddErrLine("不存在的版块ID");
                    return;
                }

                //当版块有外部链接时,则直接跳转
                if (forum.Redirect != null && forum.Redirect != string.Empty)
                {
                    System.Web.HttpContext.Current.Response.Redirect(forum.Redirect);
                    return ;
                }
                //当允许发表交易帖时,则跳转到相应的商品列表页
                if (config.Enablemall == 1 && forum.Istrade == 1)
                {
                    MallPluginBase mpb = MallPluginProvider.GetInstance();
                    int categoryid = mpb.GetGoodsCategoryIdByFid(forum.Fid);
                    if (categoryid > 0)
                    {
                        HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + base.ShowGoodsListAspxRewrite(categoryid, 1));
                        return;
                    }
                }
                if (useradminid > 0)
                {
                    // 检查是否具有版主的身份
                    ismoder = Moderators.IsModer(useradminid, userid, forumid);
                }

                #region 对搜索条件进行检索

                string orderStr = "";
                condition = "";


                topictypeid = DNTRequest.GetInt("typeid", 0);
                if (topictypeid > 0)
                {
                    condition = DatabaseProvider.GetInstance().showforumcondition(1, 0) + topictypeid;                    
                }

                filter = DNTRequest.GetString("filter");
                if (!Utils.StrIsNullOrEmpty(filter))
                {
                    condition += DatabaseProvider.GetInstance().GetTopicFilterCondition(filter);
                }

                if (DNTRequest.GetString("search").Trim() != "") //进行指定查询
                {
                    //多少时间以来的数据
                    cond = DNTRequest.GetInt("cond", -1);
                    if (cond < 1)
                    {
                        cond = 0;
                    }
                    else
                    {
                        if (!(topictypeid > 0)) //当有主题分类时，则不加入下面的日期查询条件
                        {
                            condition = DatabaseProvider.GetInstance().showforumcondition(2, cond);
                        }
                    }

                    //排序的字段
                    order = DNTRequest.GetInt("order", -1);
                    switch (order)
                    {
                        case 2:
                            orderStr = DatabaseProvider.GetInstance().showforumcondition(3, 0); //发布时间

                            break;
                        default:
                            orderStr = "";
                            break;
                    }

                    if (DNTRequest.GetInt("direct", -1) == 0)
                    {
                        direct = 0;
                    }
                }

                #endregion                

                if (forum.Fid < 1)
                {
                    if (config.Rssstatus == 1 && forum.Allowrss == 1)
                    {
                        AddLinkRss("tools/" + base.RssAspxRewrite(forum.Fid), Utils.EncodeHtml(forum.Name) + " 最新主题");
                    }
                    AddErrLine("不存在的版块ID");
                    return;
                }
                if (config.Rssstatus == 1)
                {
                    AddLinkRss("tools/" + base.RssAspxRewrite(forum.Fid), Utils.EncodeHtml(forum.Name) + " 最新主题");
                }
                forumname = forum.Name;
                pagetitle = Utils.RemoveHtml(forum.Name);
                subforumcount = forum.Subforumcount;
                forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);
                navhomemenu = Caches.GetForumListMenuDivCache(usergroupid, userid, config.Extname);

                //更新页面Meta中的Description项, 提高SEO友好性
                UpdateMetaInfo(config.Seokeywords, forum.Description, config.Seohead);


                if (forum.Applytopictype == 1) //启用主题分类
                {
                    topictypeselectoptions = Forums.GetCurrentTopicTypesOption(forum.Fid, forum.Topictypes);
                }

                if (forum.Viewbytopictype == 1) //允许按类别浏览
                {
                    topictypeselectlink = Forums.GetCurrentTopicTypesLink(forum.Fid, forum.Topictypes, "showforum.aspx");
                }


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


                // 是否显示版块密码提示 1为显示, 0不显示
                showforumlogin = 1;
                // 如果版块未设密码
                if (forum.Password == "")
                {
                    showforumlogin = 0;
                }
                else
                {
                    // 如果检测到相应的cookie正确
                    if (Utils.MD5(forum.Password) == ForumUtils.GetCookie("forum" + forumid.ToString() + "password"))
                    {
                        showforumlogin = 0;
                    }
                    else
                    {
                        // 如果用户提交的密码正确则保存cookie
                        if (forum.Password == DNTRequest.GetString("forumpassword"))
                        {
                            ForumUtils.WriteCookie("forum" + forumid.ToString() + "password", Utils.MD5(forum.Password));
                            showforumlogin = 0;
                        }
                    }
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

                //判断是否有快速发主题的权限
                if (canposttopic == true)
                {
                    if (config.Fastpost == 1 || config.Fastpost == 3)
                    {
                        canquickpost = true;
                    }
                }

                userextcreditsinfo = Scoresets.GetScoreSet(Scoresets.GetCreditsTrans());

                if (newpmcount > 0)
                {
                    pmlist = PrivateMessages.GetPrivateMessageCollectionForIndex(userid, 5, 1, 1);
                    showpmhint = Convert.ToInt32(Users.GetShortUserInfo(userid).Newsletter) > 4;
                }

                // 得到子版块列表
                //subforumlist = Forums.GetForumList(forumid, config.Hideprivate, usergroupid, config.Moddisplay);
                if (subforumcount > 0)
                {
                    subforumlist =
                        Forums.GetSubForumCollection(forumid, forum.Colcount, config.Hideprivate, usergroupid,
                                                     config.Moddisplay);
                }

                //得到当前用户请求的页数
                pageid = DNTRequest.GetInt("page", 1);

                //获取主题总数
                topiccount = Topics.GetTopicCount(forumid, condition, true);


                // 得到Tpp设置
                tpp = Utils.StrToInt(ForumUtils.GetCookie("tpp"), config.Tpp);

                if (tpp <= 0)
                {
                    tpp = config.Tpp;
                }

                // 得到Tpp设置
                ppp = Utils.StrToInt(ForumUtils.GetCookie("ppp"), config.Ppp);

                if (ppp <= 0)
                {
                    ppp = config.Ppp;
                }

                //修正请求页数中可能的错误
                if (pageid < 1)
                {
                    pageid = 1;
                }

                int toptopicpagecount = 0;


                if (forum.Layer > 0)
                {
                    // 得到公告
                    //announcementlist = Announcements.GetSimplifiedAnnouncementList(nowdatetime, "2999-01-01 00:00:00");


                    //获取当前页置顶主题列表
                    DataRow dr = Topics.GetTopTopicListID(forumid);
                    if (dr != null && dr["tid"].ToString() != "")
                    {
                        topiccount = topiccount + Utils.StrToInt(dr["tid0Count"].ToString(), 0);

                        //获取总页数
                        pagecount = topiccount%tpp == 0 ? topiccount/tpp : topiccount/tpp + 1;
                        if (pagecount == 0)
                        {
                            pagecount = 1;
                        }

                        if (pageid > pagecount)
                        {
                            pageid = pagecount;
                        }
                        toptopiccount = Utils.StrToInt(dr["tidCount"].ToString(), 0);
                        if (toptopiccount > tpp*(pageid - 1))
                        {
                            toptopiclist =
                                Topics.GetTopTopicCollection(forumid, tpp, pageid, dr["tid"].ToString(), forum.Autoclose,
                                                             forum.Topictypeprefix);
                            toptopicpagecount = toptopiccount/tpp;
                        }

                        if (toptopicpagecount >= pageid || (pageid == 1 && toptopicpagecount != toptopiccount))
                        {
                            if (orderStr == "" && direct == 1)
                            {
                                topiclist =
                                    Topics.GetTopicCollection(forumid, tpp - toptopiccount%tpp,
                                                              pageid - toptopicpagecount, 0, 600, config.Hottopic,
                                                              forum.Autoclose, forum.Topictypeprefix, condition);
                            }
                            else
                            {
                                if (direct == 0 && orderStr == string.Empty)
                                {
                                    orderStr = "lastpostid";
                                }
                                topiclist =
                                    Topics.GetTopicCollection(forumid, tpp - toptopiccount%tpp,
                                                              pageid - toptopicpagecount, 0, 600, config.Hottopic,
                                                              forum.Autoclose, forum.Topictypeprefix, condition,
                                                              orderStr, direct);
                            }
                        }
                        else
                        {
                            if (orderStr == "" && direct == 1)
                            {
                                topiclist =
                                    Topics.GetTopicCollection(forumid, tpp, pageid - toptopicpagecount,
                                                              toptopiccount%tpp, 600, config.Hottopic, forum.Autoclose,
                                                              forum.Topictypeprefix, condition);
                            }
                            else
                            {
                                if (direct == 0 && orderStr == string.Empty)
                                {
                                    orderStr = "lastpostid";
                                }
                                topiclist =
                                    Topics.GetTopicCollection(forumid, tpp, pageid - toptopicpagecount,
                                                              toptopiccount%tpp, 600, config.Hottopic, forum.Autoclose,
                                                              forum.Topictypeprefix, condition, orderStr, direct);
                            }
                        }
                    }
                    else
                    {
#if NET1
                        toptopiclist = new ShowforumPageTopicInfoCollection();
#else
                        toptopiclist = new Topics.ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();
#endif

                        //获取总页数
                        pagecount = topiccount%tpp == 0 ? topiccount/tpp : topiccount/tpp + 1;
                        if (pagecount == 0)
                        {
                            pagecount = 1;
                        }

                        if (pageid > pagecount)
                        {
                            pageid = pagecount;
                        }


                        //toptopiclist = new DataTable();
                        toptopicpagecount = 0;
                        if (orderStr == "" && direct == 1)
                        {
                            topiclist =
                                Topics.GetTopicCollection(forumid, tpp, pageid, 0, 600, config.Hottopic, forum.Autoclose,
                                                          forum.Topictypeprefix, condition);
                        }
                        else
                        {
                            if (direct == 0 && orderStr == string.Empty)
                            {
                                orderStr = "lastpostid";
                            }
                            topiclist =
                                Topics.GetTopicCollection(forumid, tpp, pageid, 0, 600, config.Hottopic, forum.Autoclose, 
                                                            forum.Topictypeprefix, condition, orderStr, direct);
                        }
                    }
//
//					if(forum.Topictypeprefix==1)
//					{
//						toptopiclist = Topics.GetTopicTypeName(toptopiclist);
//						topiclist = Topics.GetTopicTypeName(topiclist);
//					}

                    //如果topiclist为空则更新当前论坛帖数
                    if (topiclist == null || topiclist.Count == 0 || topiclist.Count > topiccount)
                    {
                        Forums.SetRealCurrentTopics(forum.Fid);
                    }


                    //得到页码链接
                    if (DNTRequest.GetString("search") == "")
                    {
                        if (topictypeid == 0)
                        {
                            if (config.Aspxrewrite == 1)
                            {
                                if (Utils.StrIsNullOrEmpty(filter))
                                {
                                    pagenumbers = Utils.GetStaticPageNumbers(pageid, pagecount, "showforum-" + forumid.ToString(), config.Extname, 8);
                                }
                                else
                                {
                                    pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "showforum.aspx?forumid=" + forumid.ToString() + "&filter=" + filter, 8);
                                }
                            }
                            else
                            {
                                pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "showforum.aspx?forumid=" + forumid.ToString() + (Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter), 8);
                            }
                        }
                        else //当有主题类型条件时
                        {
                            pagenumbers =
                                Utils.GetPageNumbers(pageid, pagecount,
                                                     "showforum.aspx?forumid=" + forumid.ToString() + "&typeid=" +
                                                     topictypeid +(Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter), 8);
                        }
                    }
                    else
                    {
                        pagenumbers =
                            Utils.GetPageNumbers(pageid, pagecount,
                                                 "showforum.aspx?search=1&cond=" + DNTRequest.GetString("cond").Trim() +
                                                 "&order=" + DNTRequest.GetString("order") + "&direct=" +
                                                 DNTRequest.GetString("direct") + "&forumid=" + forumid.ToString() +
                                                 "&typeid=" + topictypeid + (Utils.StrIsNullOrEmpty(filter) ? "" : "&filter=" + filter), 8);
                    }
                }
            }

            forumlistboxoptions = Caches.GetForumListBoxOptionsCache();

            OnlineUsers.UpdateAction(olid, UserAction.ShowForum.ActionID, forumid, forumname, -1, "",
                                     config.Onlinetimeout);

            showsplitter = false;
            if (toptopiclist.Count > 0 && topiclist.Count > 0)
            {
                showsplitter = true;
            }

            showforumonline = false;
            onlineiconlist = Caches.GetOnlineGroupIconList();
            if (forumtotalonline < config.Maxonlinelist || DNTRequest.GetString("showonline") == "yes")
            {
                showforumonline = true;
                onlineuserlist =
                    OnlineUsers.GetForumOnlineUserCollection(forumid, out forumtotalonline, out forumtotalonlineguest,
                                                             out forumtotalonlineuser, out forumtotalonlineinvisibleuser);
            }

            if (DNTRequest.GetString("showonline") == "no")
            {
                showforumonline = false;
            }

            ForumUtils.UpdateVisitedForumsOptions(forumid);
            visitedforumsoptions = ForumUtils.GetVisitedForumsOptions(config.Visitedforums);
            forumallowrss = forum.Allowrss;
        }
    }
}
