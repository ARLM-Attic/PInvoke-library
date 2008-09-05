using System;
using System.Data;
using System.Text;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Data;
using System.IO;
using Discuz.Plugin.Album;

namespace Discuz.Web
{
    /// <summary>
    /// 回复页面类
    /// </summary>
    public class postreply : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 主题信息
        /// </summary>
        public TopicInfo topic;
        /// <summary>
        /// 最后5条回复列表
        /// </summary>
        public DataTable lastpostlist;
        /// <summary>
        /// 所属版块Id
        /// </summary>
        public int forumid;
        /// <summary>
        /// 所属版块名称
        /// </summary>
        public string forumname;
        /// <summary>
        /// 主题Id
        /// </summary>
        public int topicid;
        /// <summary>
        /// 帖子Id
        /// </summary>
        public int postid;
        /// <summary>
        /// 回复内容
        /// </summary>
        public string message;
        /// <summary>
        /// 表情Javascript数组
        /// </summary>
        public string smilies;
        /// <summary>
        /// 主题标题
        /// </summary>
        public string topictitle = string.Empty;
        /// <summary>
        /// 回复标题
        /// </summary>
        public string replytitle;
        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav = string.Empty;
        /// <summary>
        /// 编辑器自定义按钮
        /// </summary>
        public string customeditbuttons;
        /// <summary>
        /// 主题图标
        /// </summary>
        public string topicicons;
        /// <summary>
        /// 是否解析网址
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
        /// 是否允许 [img] 代码
        /// </summary>
        public int allowimg;
        /// <summary>
        /// 是否受灌水限制
        /// </summary>
        public int disablepost;
        /// <summary>
        /// 允许的附件类型和大小数组
        /// </summary>
        public string attachextensions;
        /// <summary>
        /// 允许的附件类型
        /// </summary>
        public string attachextensionsnosize;
        /// <summary>
        /// 今天可上传附件大小
        /// </summary>
        public int attachsize;
        /// <summary>
        /// 继续进行回复
        /// </summary>
        public string continuereply = "";
        /// <summary>
        /// 所属版块信息
        /// </summary>
        public ForumInfo forum;
        /// <summary>
        /// 表情分类
        /// </summary>
        public DataTable smilietypes;
        /// <summary>
        /// 当前用户相册列表
        /// </summary>
        public DataTable albumlist;
        /// <summary>
        /// 是否允许回复
        /// </summary>
        public bool canreply = false;
        /// <summary>
        /// 是否允许插入到相册
        /// </summary>
        public bool caninsertalbum;
        /// <summary>
        /// 是否允许上传附件
        /// </summary>
        public bool canpostattach = false;
        /// <summary>
        /// 第一页表情的JSON
        /// </summary>
        public string firstpagesmilies = string.Empty;
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool needlogin = false;
        #endregion


        AlbumPluginBase apb = AlbumPluginProvider.GetInstance();

        protected override void ShowPage()
        {
            #region 临时帐号发帖
            int realuserid = -1;
            bool tempaccountspost = false;
            string tempusername = DNTRequest.GetString("tempusername");
            if (tempusername != "" && tempusername != username)
            {
                string temppassword = DNTRequest.GetString("temppassword");
                int question = DNTRequest.GetInt("question", 0);
                string answer = DNTRequest.GetString("answer");
                if (config.Passwordmode == 1)
                {
                    if (config.Secques == 1)
                    {
                        realuserid = Users.CheckDvBbsPasswordAndSecques(tempusername, temppassword, question, answer);
                    }
                    else
                    {
                        realuserid = Users.CheckDvBbsPassword(tempusername, temppassword);
                    }
                }
                else
                {
                    if (config.Secques == 1)
                    {
                        realuserid = Users.CheckPasswordAndSecques(tempusername, temppassword, true, question, answer);
                    }
                    else
                    {
                        realuserid = Users.CheckPassword(tempusername, temppassword, true);
                    }
                }
                if (realuserid == -1)
                {
                    AddErrLine("临时帐号登录失败，无法继续发帖。");
                    return;
                }
                else
                {
                    userid = realuserid;
                    username = tempusername;
                    usergroupinfo = UserGroups.GetUserGroupInfo(Users.GetShortUserInfo(userid).Groupid);
                    usergroupid = usergroupinfo.Groupid;
                    useradminid = Users.GetShortUserInfo(userid).Adminid;
                    tempaccountspost = true;
                }
            }
            #endregion
            // 获取主题ID
            topicid = DNTRequest.GetInt("topicid", -1);
            // 获取postid
            postid = DNTRequest.GetInt("postid", -1);
            PostInfo postinfo = new PostInfo();
            int layer = 1;
            int parentid = 0;
            message = "";
            topictitle = "";
            replytitle = "";
            forumnav = "";
            firstpagesmilies = Caches.GetSmiliesFirstPageCache();
            if (!DNTRequest.IsPost())
            {
                continuereply = DNTRequest.GetQueryString("continuereply");
            }

            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(useradminid);
            disablepost = 0;
            //　如果当前用户非管理员并且论坛设定了禁止发帖时间段，当前时间如果在其中的一个时间段内，不允许用户发帖
            if (useradminid != 1 && usergroupinfo.Disableperiodctrl != 1)
            {
                string visittime = "";
                if (Scoresets.BetweenTime(config.Postbanperiods, out visittime))
                {
                    AddErrLine("在此时间段( " + visittime + " )内用户不可以发帖");
                    return;
                }
            }

            if (postid != -1)
            {
                postinfo = Posts.GetPostInfo(topicid, postid);
                if (postinfo == null)
                {
                    AddErrLine("无效的帖子ID");
                    return;
                }
                if (topicid != postinfo.Tid)
                {
                    AddErrLine("主题ID无效");
                    return;
                }

                layer = postinfo.Layer + 1;
                parentid = postinfo.Parentid;


                if (!DNTRequest.GetString("quote").Equals(""))
                {
                    if ((postinfo.Message.IndexOf("[hide]") > -1) && (postinfo.Message.IndexOf("[/hide]") > -1))
                    {
                        message = "[quote] 原帖由 [b]" + postinfo.Poster + "[/b] 于 " + postinfo.Postdatetime + " 发表\r\n ***隐藏帖*** [/quote]";
                    }
                    else
                    {
                        message = "[quote] 原帖由 [b]" + postinfo.Poster + "[/b] 于 " + postinfo.Postdatetime + " 发表\r\n" + UBB.ClearAttachUBB(Utils.GetSubString(postinfo.Message, 200, "......")) + " [/quote]";
                    }
                }
            }
            else
            {
                // 如果主题ID非数字
                if (topicid == -1)
                {
                    AddErrLine("无效的主题ID");
                    return;
                }
            }

            // 获取该主题的信息
            topic = Topics.GetTopicInfo(topicid);
            // 如果该主题不存在
            if (topic == null)
            {
                AddErrLine("不存在的主题ID");
                return;
            }

            topictitle = topic.Title.Trim();
            replytitle = topictitle;
            if (replytitle.Length >= 50)
            {
                replytitle = Utils.CutString(replytitle, 0, 50) + "...";
            }

            pagetitle = topictitle.Trim();
            forumid = topic.Fid;
            //　如果当前用户非管理员并且该主题已关闭，不允许用户发帖
            if (admininfo == null || !Moderators.IsModer(admininfo.Admingid, userid, forumid))
            {
                if (topic.Closed == 1)
                {
                    AddErrLine("主题已关闭无法回复");
                    return;
                }
            }

            forum = Forums.GetForumInfo(forumid);
            forumname = forum.Name.Trim();
            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);

            if (topic.Readperm > usergroupinfo.Readaccess && topic.Posterid != userid && useradminid != 1 && !Utils.InArray(username, forum.Moderators.Split(',')))
            {
                AddErrLine("本主题阅读权限为: " + topic.Readperm.ToString() + ", 您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 阅读权限不够");
                return;
            }

            if (!ispost)
            {
                smilies = Caches.GetSmiliesCache();
                smilietypes = Caches.GetSmilieTypesCache();
                customeditbuttons = Caches.GetCustomEditButtonList();
                //topicicons = Caches.GetTopicIconsCache();
            }

            //得到用户可以上传的文件类型
            StringBuilder sbAttachmentTypeSelect = new StringBuilder();
            if (!usergroupinfo.Attachextensions.Trim().Equals(""))
            {
                sbAttachmentTypeSelect.Append("[id] in (");
                sbAttachmentTypeSelect.Append(usergroupinfo.Attachextensions);
                sbAttachmentTypeSelect.Append(")");
            }

            if (!forum.Attachextensions.Trim().Equals(""))
            {
                if (sbAttachmentTypeSelect.Length > 0)
                {
                    sbAttachmentTypeSelect.Append(" AND ");
                }
                sbAttachmentTypeSelect.Append("[id] in (");
                sbAttachmentTypeSelect.Append(forum.Attachextensions);
                sbAttachmentTypeSelect.Append(")");
            }
            attachextensions = Attachments.GetAttachmentTypeArray(sbAttachmentTypeSelect.ToString());
            attachextensionsnosize = Attachments.GetAttachmentTypeString(sbAttachmentTypeSelect.ToString());

            //得到今天允许用户上传的附件总大上(字节)
            int MaxTodaySize = 0;
            if (userid > 0)
            {
                MaxTodaySize = Attachments.GetUploadFileSizeByuserid(userid);		//今天已上传大小
            }
            attachsize = usergroupinfo.Maxsizeperday - MaxTodaySize;




            StringBuilder builder = new StringBuilder();
            //sb.Append("var Allowhtml=1;\r\n"); //+ allhtml.ToString() + "

            parseurloff = 0;

            smileyoff = 1 - forum.Allowsmilies;
            //sb.Append("var Allowsmilies=" + (1-smileyoff).ToString() + ";\r\n");


            bbcodeoff = 1;
            if (forum.Allowbbcode == 1 && usergroupinfo.Allowcusbbcode == 1)
            {
                bbcodeoff = 0;
            }

            //sb.Append("var Allowbbcode=" + (1-bbcodeoff).ToString() + ";\r\n");

            usesig = ForumUtils.GetCookie("sigstatus") == "0" ? 0 : 1;

            allowimg = forum.Allowimgcode;
            //sb.Append("var Allowimgcode=" + allowimg.ToString() + ";\r\n");



            //AddScript(sb.ToString());

            if (forum.Password != "" && Utils.MD5(forum.Password) != ForumUtils.GetCookie("forum" + forumid.ToString() + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                SetBackLink(base.ShowForumAspxRewrite(forumid, 0));
                return;
            }


            if (!Forums.AllowViewByUserID(forum.Permuserlist, userid)) //判断当前用户在当前版块浏览权限
            {
                if (forum.Viewperm == null || forum.Viewperm == string.Empty)//当板块权限为空时，按照用户组权限
                {
                    if (usergroupinfo.Allowvisit != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有浏览该版块的权限");
                        needlogin = true;
                        return;
                    }
                }
                else//当板块权限不为空，按照板块权限
                {
                    if (!Forums.AllowView(forum.Viewperm, usergroupid))
                    {
                        AddErrLine("您没有浏览该版块的权限");
                        needlogin = true;
                        return;
                    }
                }
            }


            //是否有回复的权限
            if (!Forums.AllowReplyByUserID(forum.Permuserlist, userid))
            {
                if (forum.Replyperm == null || forum.Replyperm == string.Empty)//当板块权限为空时根据用户组权限判断
                {
                    // 验证用户是否有发表主题的权限
                    if (usergroupinfo.Allowreply != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有发表回复的权限");
                        needlogin = true;
                        return;
                    }
                }
                else//板块权限不为空时根据板块权限判断
                {
                    if (!Forums.AllowReply(forum.Replyperm, usergroupid))
                    {
                        AddErrLine("您没有在该版块发表回复的权限");
                        needlogin = true;
                        return;
                    }
                }
            }


            //是否有上传附件的权限
            if (Forums.AllowPostAttachByUserID(forum.Permuserlist, userid))
            {
                canpostattach = true;
            }
            else
            {
                if (forum.Postattachperm == null || forum.Postattachperm == string.Empty)//当板块权限为空时根据用户组权限判断
                {
                    // 验证用户是否有上传附件的权限
                    if (usergroupinfo.Allowpostattach == 1)
                    {
                        canpostattach = true;
                    }
                }
                else//板块权限不为空时根据板块权限判断
                {
                    if (Forums.AllowPostAttach(forum.Postattachperm, usergroupid))
                    {
                        canpostattach = true;
                    }
                }
            }

            ShortUserInfo user = Users.GetShortUserInfo(userid);
            if (canpostattach && user != null && (config.Enablealbum == 1) && apb != null &&
                (UserGroups.GetUserGroupInfo(user.Groupid).Maxspacephotosize - apb.GetPhotoSizeByUserid(userid) > 0))
            {
                caninsertalbum = true;
                albumlist = apb.GetSpaceAlbumByUserId(userid);
            }
            else
            {
                caninsertalbum = false;
            }


            // 如果是受灌水限制用户, 则判断是否是灌水
            if (admininfo != null)
            {
                disablepost = admininfo.Disablepostctrl;
            }
            if (admininfo == null || admininfo.Disablepostctrl != 1)
            {

                int Interval = Utils.StrDateDiffSeconds(lastposttime, config.Postinterval);
                if (Interval < 0)
                {
                    AddErrLine("系统规定发帖间隔为" + config.Postinterval.ToString() + "秒, 您还需要等待 " + (Interval * -1).ToString() + " 秒");
                    return;
                }
                else if (userid != -1)
                {
                    string joindate = Users.GetUserJoinDate(userid);
                    if (joindate == "")
                    {
                        AddErrLine("您的用户资料出现错误");
                        return;
                    }

                    Interval = Utils.StrDateDiffMinutes(joindate, config.Newbiespan);
                    if (Interval < 0)
                    {
                        AddErrLine("系统规定新注册用户必须要在" + config.Newbiespan.ToString() + "分钟后才可以发帖, 您还需要等待 " + (Interval * -1).ToString() + " 分");
                        return;
                    }

                }
            }

            //如果不是提交...
            if (!ispost)
            {
                if (forum.Templateid > 0)
                {
                    templatepath = Templates.GetTemplateItem(forum.Templateid).Directory;
                }

                AddLinkCss(BaseConfigs.GetForumPath + "templates/" + templatepath + "/editor.css", "css");

                //判断是否为回复可见帖, hide=0为非回复可见(正常), hide > 0为回复可见, hide=-1为回复可见但当前用户已回复
                int hide = 0;
                if (topic.Hide == 1)
                {
                    hide = topic.Hide;
                    if (Posts.IsReplier(topicid, userid))
                    {
                        hide = -1;
                    }
                }
                //判断是否为回复可见帖, price=0为非购买可见(正常), price > 0 为购买可见, price=-1为购买可见但当前用户已购买
                int price = 0;
                if (topic.Price > 0)
                {
                    price = topic.Price;
                    if (PaymentLogs.IsBuyer(topicid, userid))//判断当前用户是否已经购买
                    {
                        price = -1;
                    }
                }

                PostpramsInfo postpramsinfo = new PostpramsInfo();
                postpramsinfo.Fid = forum.Fid;
                postpramsinfo.Tid = topicid;
                postpramsinfo.Jammer = forum.Jammer;
                postpramsinfo.Pagesize = 5;
                postpramsinfo.Pageindex = 1;
                postpramsinfo.Getattachperm = forum.Getattachperm;
                postpramsinfo.Usergroupid = usergroupid;
                postpramsinfo.Attachimgpost = config.Attachimgpost;
                postpramsinfo.Showattachmentpath = config.Showattachmentpath;
                postpramsinfo.Hide = hide;
                postpramsinfo.Price = price;
                postpramsinfo.Ubbmode = false;

                postpramsinfo.Showimages = forum.Allowimgcode;
                postpramsinfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
                postpramsinfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
                postpramsinfo.Smiliesmax = config.Smiliesmax;
                postpramsinfo.Bbcodemode = config.Bbcodemode;

                lastpostlist = Posts.GetLastPostList(postpramsinfo);
            }
            else
            {

                string backlink = "";
                if (DNTRequest.GetInt("topicid", -1) > 0)
                {
                    backlink = string.Format("postreply.aspx?topicid={0}", this.topicid.ToString());
                }
                else
                {
                    backlink = string.Format("postreply.aspx?postid={0}", this.postid.ToString());
                }

                if (!DNTRequest.GetString("quote").Equals(""))
                {
                    backlink = string.Format("{0}&quote={1}", backlink, DNTRequest.GetString("quote"));
                }
                backlink += "&restore=1";
                SetBackLink(backlink);

                string postmessage = DNTRequest.GetString("message");

                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }


                //if (DNTRequest.GetString("title").Trim().Equals(""))
                //{
                //    AddErrLine("主题不能为空");
                //}
                //else 
                if (DNTRequest.GetString("title").IndexOf("　") != -1)
                {
                    AddErrLine("标题不能包含全角空格符");
                }
                else if (DNTRequest.GetString("title").Length > 60)
                {
                    AddErrLine("标题最大长度为60个字符,当前为 " + DNTRequest.GetString("title").Length.ToString() + " 个字符");
                }

                if (postmessage.Equals(""))
                {
                    AddErrLine("内容不能为空");
                }

                if (admininfo != null && admininfo.Disablepostctrl != 1)
                {
                    if (postmessage.Length < config.Minpostsize)
                    {
                        AddErrLine("您发表的内容过少, 系统设置要求帖子内容不得少于 " + config.Minpostsize.ToString() + " 字多于 " + config.Maxpostsize.ToString() + " 字");
                    }
                    else if (postmessage.Length > config.Maxpostsize)
                    {
                        AddErrLine("您发表的内容过多, 系统设置要求帖子内容不得少于 " + config.Minpostsize.ToString() + " 字多于 " + config.Maxpostsize.ToString() + " 字");
                    }
                }

                if (topic.Special == 4 && DNTRequest.GetInt("debateopinion", 0) == 0)
                {
                    AddErrLine("请选择您在辩论中的观点");
                }
                DebateInfo debateexpand = Debates.GetDebateTopic(topic.Tid);
                if (topic.Special == 4 && debateexpand.Terminaltime < DateTime.Now)
                {
                    AddErrLine("此辩论主题已经到期");

                }

                if (IsErr())
                {
                    return;
                }

                // 如果用户上传了附件,则检测用户是否有上传附件的权限
                if (ForumUtils.IsPostFile())
                {

                    if (!Forums.AllowPostAttachByUserID(forum.Permuserlist, userid))
                    {
                        if (!Forums.AllowPostAttach(forum.Postattachperm, usergroupid))
                        {
                            AddErrLine("您没有在该版块上传附件的权限");
                            return;
                        }
                        else if (usergroupinfo.Allowpostattach != 1)
                        {
                            AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有上传附件的权限");
                            return;
                        }
                    }
                }

                //新用户广告强力屏蔽检查

                if ((config.Disablepostad == 1) && useradminid < 1 || userid == -1)  //如果开启新用户广告强力屏蔽检查或是游客
                {
                    if (userid == -1 || (config.Disablepostadpostcount != 0 && user.Posts <= config.Disablepostadpostcount) ||
                        (config.Disablepostadregminute != 0 && DateTime.Now.AddMinutes(-config.Disablepostadregminute) <= Convert.ToDateTime(user.Joindate)))
                    {
                        foreach (string regular in config.Disablepostadregular.Replace("\r", "").Split('\n'))
                        {
                            if (Posts.IsAD(regular, DNTRequest.GetString("title"), postmessage))
                            {
                                AddErrLine("发帖失败，内容中似乎有广告信息，请检查标题和内容，如有疑问请与管理员联系");
                                return;
                            }
                        }
                    }
                }

                if (IsErr())
                {
                    return;
                }


                int iconid = DNTRequest.GetInt("iconid", 0);
                if (iconid > 15)
                {
                    iconid = 0;
                }

                int hide = 0;
                if (ForumUtils.IsHidePost(postmessage) && usergroupinfo.Allowhidecode == 1)
                {
                    hide = 1;
                }

                string curdatetime = Utils.GetDateTime();

                postinfo = new PostInfo();
                postinfo.Fid = forumid;
                postinfo.Tid = topicid;
                postinfo.Parentid = parentid;
                postinfo.Layer = layer;
                postinfo.Poster = username;
                postinfo.Posterid = userid;

                if (useradminid == 1)
                {
                    postinfo.Title = Utils.HtmlEncode(DNTRequest.GetString("title"));
                    postinfo.Message = Utils.HtmlEncode(postmessage);
                }
                else
                {
                    postinfo.Title = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("title")));
                    postinfo.Message = Utils.HtmlEncode(ForumUtils.BanWordFilter(postmessage));
                }
                postinfo.Postdatetime = curdatetime;


                if (ForumUtils.HasBannedWord(postinfo.Title) || ForumUtils.HasBannedWord(postinfo.Message))
                {
                    AddErrLine("对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!");
                    return;
                }

                postinfo.Ip = DNTRequest.GetIP();
                postinfo.Lastedit = "";
                postinfo.Debateopinion = DNTRequest.GetInt("debateopinion", 0);
                if (forum.Modnewposts == 1 && useradminid != 1 && useradminid!=2)
                {
                    postinfo.Invisible = 1;
                }
                else
                {
                    postinfo.Invisible = 0;
                }

                //　如果当前用户非管理员并且论坛设定了发帖审核时间段，当前时间如果在其中的一个时间段内，则用户所发帖均为待审核状态
                if (useradminid != 1)
                {
                    if (Scoresets.BetweenTime(config.Postmodperiods))
                    {
                        postinfo.Invisible = 1;
                    }

                    if (ForumUtils.HasAuditWord(postinfo.Title) || ForumUtils.HasAuditWord(postinfo.Message))
                    {
                        postinfo.Invisible = 1;
                    }
                }



                postinfo.Usesig = Utils.StrToInt(DNTRequest.GetString("usesig"), 0);
                postinfo.Htmlon = 1;

                postinfo.Smileyoff = smileyoff;
                if (smileyoff == 0)
                {
                    postinfo.Smileyoff = Utils.StrToInt(DNTRequest.GetString("smileyoff"), 0);
                }

                postinfo.Bbcodeoff = 1;
                if (usergroupinfo.Allowcusbbcode == 1 && forum.Allowbbcode == 1)
                {
                    postinfo.Bbcodeoff = Utils.StrToInt(DNTRequest.GetString("bbcodeoff"), 0);
                }
                postinfo.Parseurloff = Utils.StrToInt(DNTRequest.GetString("parseurloff"), 0);
                postinfo.Attachment = 0;
                postinfo.Rate = 0;
                postinfo.Ratetimes = 0;
                postinfo.Topictitle = topic.Title;

                // 产生新帖子
                postid = Posts.CreatePost(postinfo);
                //Utils.WriteCookie("ispostcookies", "true");
                int pid = postid;

                if (hide == 1)
                {
                    topic.Hide = hide;
                    Topics.UpdateTopicHide(topicid);
                }
                Topics.UpdateTopicReplies(topicid);
                Topics.AddParentForumTopics(forum.Parentidlist.Trim(), 0, 1);
                //设置用户的积分
                ///首先读取版块内自定义积分
                ///版设置了自定义积分则使用，否则使用论坛默认积分
                float[] values = null;
                if (!forum.Replycredits.Equals(""))
                {
                    int index = 0;
                    float tempval = 0;
                    values = new float[8];
                    foreach (string ext in Utils.SplitString(forum.Replycredits, ","))
                    {

                        if (index == 0)
                        {
                            if (!ext.Equals("True"))
                            {
                                values = null;
                                break;
                            }
                            index++;
                            continue;
                        }
                        tempval = Utils.StrToFloat(ext, 0.0f);
                        values[index - 1] = tempval;
                        index++;
                        if (index > 8)
                        {
                            break;
                        }
                    }
                }

                
                builder = new StringBuilder();
                builder.Remove(0, builder.Length);

                int watermarkstatus = config.Watermarkstatus;
                if (forum.Disablewatermark == 1)
                {
                    watermarkstatus = 0;
                }
                AttachmentInfo[] attachmentinfo = ForumUtils.SaveRequestFiles(forumid, config.Maxattachments, usergroupinfo.Maxsizeperday, usergroupinfo.Maxattachsize, MaxTodaySize, attachextensions, watermarkstatus, config, "postfile");
                if (attachmentinfo != null)
                {
                    if (attachmentinfo.Length > config.Maxattachments)
                    {
                        AddErrLine("系统设置为每个帖子附件不得多于" + config.Maxattachments + "个");
                        return;
                    }
                    int errorAttachment = Attachments.BindAttachment(attachmentinfo, postid, builder, topicid, userid);

                    int[] aid = Attachments.CreateAttachments(attachmentinfo);

                    string tempMessage = Attachments.FilterLocalTags(aid, attachmentinfo, postinfo.Message);

                    if (!tempMessage.Equals(postinfo.Message))
                    {
                        postinfo.Message = tempMessage;
                        postinfo.Pid = postid;
                        Posts.UpdatePost(postinfo);
                    }

                    UserCredits.UpdateUserCreditsByUploadAttachment(userid, aid.Length - errorAttachment);
                }

                //加入相册
                if (!tempaccountspost)
                {
                    if (config.Enablealbum == 1 && apb != null)
                    {
                        builder.Append(apb.CreateAttachment(attachmentinfo, usergroupid, userid, username));
                    }
                }

                //if (config.Aspxrewrite == 1)
                //{
                //    SetUrl("showtopic-" + topicid.ToString().Trim() + ".aspx#" + pid.ToString());
                //}
                //else
                //{
                //}

                if (topic.Special == 4)
                {
                    //辩论地址
                    SetUrl(Urls.ShowDebateAspxRewrite(topicid));
                }
                else
                {
                    SetUrl("showtopic.aspx?page=end&topicid=" + topicid.ToString().Trim() + "#" + pid.ToString());

                }



                if (DNTRequest.GetFormString("continuereply") == "on")
                {
                    SetUrl("postreply.aspx?topicid=" + topicid.ToString().Trim() + "&continuereply=yes");
                }

                OnlineUsers.UpdateAction(olid, UserAction.PostReply.ActionID, forumid, forumname, topicid, topictitle, config.Onlinetimeout);
                // 更新在线表中的用户最后发帖时间
                OnlineUsers.UpdatePostTime(olid);

                if (builder.Length > 0)
                {
                    UpdateUserCredits(values);
                    SetMetaRefresh(5);
                    SetShowBackLink(true);
                    builder.Insert(0, "<table cellspacing=\"0\" cellpadding=\"4\" border=\"0\"><tr><td colspan=2 align=\"left\"><span class=\"bold\"><nobr>发表主题成功,但以下附件上传失败:</nobr></span><br /></td></tr>");
                    builder.Append("</table>");
                    AddMsgLine(builder.ToString());
                }
                else
                {

                    SetMetaRefresh();
                    SetShowBackLink(false);
                    //上面已经进行用户组判断
                    if (postinfo.Invisible == 1)
                    {
                        AddMsgLine(string.Format("发表回复成功, 但需要经过审核才可以显示. {0}<br /><br />(<a href=\"" + base.ShowForumAspxRewrite(forumid, 0) + "\">点击这里返回 {1}</a>)", (DNTRequest.GetFormString("continuereply") == "on" ? "继续回复" : "返回该主题"), forumname));
                    }
                    else
                    {
                        UpdateUserCredits(values);
                        AddMsgLine(string.Format("发表回复成功, {0}<br />(<a href=\"" + base.ShowForumAspxRewrite(forumid, 0) + "\">点击这里返回 {1}</a>)<br />", (DNTRequest.GetFormString("continuereply") == "on" ? "继续回复" : "返回该主题"), forumname));
                    }
                }
                // 删除主题游客缓存
                if (topic.Replies < (config.Ppp + 10))
                {
                    ForumUtils.DeleteTopicCacheFile(topicid);
                }
                //发送邮件通知
                if (DNTRequest.GetString("emailnotify") == "on")
                {
                    SendNotifyEmail(Users.GetShortUserInfo(topic.Posterid).Email.Trim(), postinfo, "http://" + DNTRequest.GetCurrentFullHost() + "/showtopic.aspx?page=end&topicid=" + topicid.ToString().Trim() + "#" + pid.ToString());
                }
            }

        }

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="values">版块积分设置</param>
        private void UpdateUserCredits(float[] values)
        {
            if (values != null)
            {
                ///使用版块内积分
                UserCredits.UpdateUserCreditsByPosts(userid, values);
            }
            else
            {
                ///使用默认积分
                UserCredits.UpdateUserCreditsByPosts(userid);
            }
        }

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <param name="email">接收人邮箱</param>
        /// <param name="postinfo">帖子信息</param>
        /// <param name="jumpurl">跳转链接</param>
        public void SendNotifyEmail(string email, PostInfo postinfo, string jumpurl)
        {
            StringBuilder sb_body = new StringBuilder("# 回复: <a href=\"" + jumpurl + "\" target=\"_blank\">" + topic.Title + "</a>");
            //发送人邮箱
            string cur_email = Users.GetShortUserInfo(userid).Email.Trim();
            sb_body.Append("\r\n");
            sb_body.Append("\r\n");
            sb_body.Append(postinfo.Message);
            sb_body.Append("\r\n<hr/>");
            sb_body.Append("作 者:" + postinfo.Poster);
            sb_body.Append("\r\n");
            sb_body.Append("Email:<a href=\"mailto:" + cur_email + "\" target=\"_blank\">" + cur_email + "</a>");
            sb_body.Append("\r\n");
            sb_body.Append("URL:<a href=\"" + jumpurl + "\" target=\"_blank\">" + jumpurl + "</a>");
            sb_body.Append("\r\n");
            sb_body.Append("时 间:" + postinfo.Postdatetime);
            Emails.SendEmailNotify(email, "[" + config.Forumtitle + "回复通知]" + topic.Title, sb_body.ToString());
        }

    }
}
