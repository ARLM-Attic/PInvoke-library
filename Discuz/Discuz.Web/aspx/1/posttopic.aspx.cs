using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Plugin.Album;
using Discuz.Plugin.Space;

namespace Discuz.Web
{
    /// <summary>
    /// 发表主题页面
    /// </summary>
    public class posttopic : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 所属版块名称
        /// </summary>
        public string forumname;
        /// <summary>
        /// 所属板块Id
        /// </summary>
        public int forumid;
        /// <summary>
        /// 主题内容
        /// </summary>
        public string message;
        /// <summary>
        /// 是否允许发表主题
        /// </summary>
        public bool allowposttopic;
        /// <summary>
        /// 表情Javascript数组
        /// </summary>
        public string smilies;
        /// <summary>
        /// 主题图标
        /// </summary>
        public string topicicons;
        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav = string.Empty;
        /// <summary>
        /// 编辑器自定义按钮
        /// </summary>
        public string customeditbuttons;
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
        /// 是否受发贴灌水限制
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
        /// 积分策略信息
        /// </summary>
        public UserExtcreditsInfo userextcreditsinfo;
        /// <summary>
        /// 最高售价
        /// </summary>
        public int maxprice;
        /// <summary>
        /// 所属版块信息
        /// </summary>
        public ForumInfo forum;
        /// <summary>
        /// 主题分类选项字串
        /// </summary>
        public string topictypeselectoptions;
        /// <summary>
        /// 表情列表
        /// </summary>
        public DataTable smilietypes;
        /// <summary>
        /// 相册列表
        /// </summary>
        public DataTable albumlist;
        /// <summary>
        /// 是否允许上传附件
        /// </summary>
        public bool canpostattach;
        /// <summary>
        /// 是否允许同时发布到相册
        /// </summary>
        public bool caninsertalbum;
        /// <summary>
        /// 交易积分
        /// </summary>
        public int creditstrans;
        /// <summary>
        /// 投票截止时间
        /// </summary>
        public string enddatetime = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd");
        /// <summary>
        /// 是否允许Html标题
        /// </summary>
        public bool canhtmltitle = false;

        /// <summary>
        /// 第一页表情的JSON
        /// </summary>
        public string firstpagesmilies = string.Empty;

        /// <summary>
        /// 发帖人的个人空间Id
        /// </summary>
        public int spaceid = 0;
        /// <summary>
        /// 本版是否可用Tag
        /// </summary>
        public bool enabletag = false;
        /// <summary>
        /// 发帖的类型，如普通帖、悬赏帖等。。
        /// </summary>
        public string type;
        /// <summary>
        /// 当前登录用户的交易积分值, 仅悬赏帖时有效
        /// </summary>
        public float mycurrenttranscredits;

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

            canhtmltitle = config.Htmltitle == 1 && Utils.InArray(usergroupid.ToString(), config.Htmltitleusergroup);
            firstpagesmilies = Caches.GetSmiliesFirstPageCache();
            bool createpoll = false;
            string[] pollitem = { };

            //内容设置为空;  
            message = "";
            //maxprice = usergroupinfo.Maxprice > Scoresets.GetMaxIncPerTopic() ? Scoresets.GetMaxIncPerTopic() : usergroupinfo.Maxprice;
            maxprice = usergroupinfo.Maxprice;

            forumid = DNTRequest.GetInt("forumid", -1);

            allowposttopic = true;

            if (forumid == -1)
            {
                allowposttopic = false;
                AddErrLine("错误的论坛ID");
                forumnav = "";
                return;
            }
            else
            {
                forum = Forums.GetForumInfo(forumid);
                if (forum == null || forum.Layer == 0)
                {
                    allowposttopic = false;
                    AddErrLine("错误的论坛ID");
                    forumnav = "";
                    return;
                }
                forumname = forum.Name;
                pagetitle = Utils.RemoveHtml(forum.Name);
                forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);
                enabletag = (config.Enabletag & forum.Allowtag) == 1;
                if (forum.Applytopictype == 1)  //启用主题分类
                {
                    topictypeselectoptions = Forums.GetCurrentTopicTypesOption(forum.Fid, forum.Topictypes);
                }
            }

            //得到用户可以上传的文件类型
            StringBuilder sbAttachmentTypeSelect = new StringBuilder();
            if (!usergroupinfo.Attachextensions.Trim().Equals(""))
            {
                sbAttachmentTypeSelect.Append("[id] in (");
                sbAttachmentTypeSelect.Append(usergroupinfo.Attachextensions);
                sbAttachmentTypeSelect.Append(")");
            }

            if (!forum.Attachextensions.Equals(""))
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

            //得到今天允许用户上传的附件总大小(字节)
            int MaxTodaySize = 0;
            if (userid > 0)
            {
                MaxTodaySize = Attachments.GetUploadFileSizeByuserid(userid);		//今天已上传大小
            }
            attachsize = usergroupinfo.Maxsizeperday - MaxTodaySize;//今天可上传得大小



            StringBuilder sb = new StringBuilder();
            //sb.Append("var allowhtml=1;\r\n"); //+ allhtml.ToString() + "

            parseurloff = 0;

            smileyoff = 1 - forum.Allowsmilies;
            //sb.Append("var allowsmilies=" + (1-smileyoff).ToString() + ";\r\n");


            bbcodeoff = 1;
            if (forum.Allowbbcode == 1 && usergroupinfo.Allowcusbbcode == 1)
            {
                bbcodeoff = 0;
            }
            //sb.Append("var allowbbcode=" + (1-bbcodeoff).ToString() + ";\r\n");

            usesig = ForumUtils.GetCookie("sigstatus") == "0" ? 0 : 1;

            allowimg = forum.Allowimgcode;
            //sb.Append("var allowimgcode=" + allowimg.ToString() + ";\r\n");



            //AddScript(sb.ToString());


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

            if (forum.Password != "" && Utils.MD5(forum.Password) != ForumUtils.GetCookie("forum" + forumid.ToString() + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                SetBackLink(base.ShowForumAspxRewrite(forumid, 0));
                return;
            }


            if (!Forums.AllowViewByUserID(forum.Permuserlist, userid)) //判断当前用户在当前版块浏览权限
            {
                if (string.IsNullOrEmpty(forum.Viewperm))//当板块权限为空时，按照用户组权限
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

            if (!Forums.AllowPostByUserID(forum.Permuserlist, userid)) //判断当前用户在当前版块发主题权限
            {
                if (forum.Postperm == null || forum.Postperm == string.Empty)//权限设置为空时，根据用户组权限判断
                {
                    // 验证用户是否有发表主题的权限
                    if (usergroupinfo.Allowpost != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有发表主题的权限");
                        needlogin = true;
                        return;
                    }
                }
                else//权限设置不为空时,根据板块权限判断
                {
                    if (!Forums.AllowPost(forum.Postperm, usergroupid))
                    {
                        AddErrLine("您没有在该版块发表主题的权限");
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
                if (forum.Postattachperm == "")
                {
                    if (usergroupinfo.Allowpostattach == 1)
                    {
                        canpostattach = true;
                    }
                }
                else
                {
                    if (Forums.AllowPostAttach(forum.Postattachperm, usergroupid))
                    {
                        canpostattach = true;
                    }
                }
            }

            ShortUserInfo user = Users.GetShortUserInfo(userid);
            if (canpostattach && user != null && apb != null &&
                (config.Enablealbum == 1) &&
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
            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(useradminid);
            disablepost = 0;
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


            creditstrans = Scoresets.GetCreditsTrans();
            userextcreditsinfo = Scoresets.GetScoreSet(creditstrans);

            //message = ForumUtils.GetCookie("postmessage");
            if (userid > 0)
            {
                spaceid = Users.GetShortUserInfo(userid).Spaceid;
            }

            type = DNTRequest.GetString("type").ToLower();

            //int specialpost = 0;
            if (forum.Allowspecialonly > 0 && Utils.StrIsNullOrEmpty(type))
            {
                AddErrLine(string.Format("当前版块 \"{0}\" 不允许发表普通主题", forum.Name));
                return;
            }

            if (forum.Allowpostspecial > 0)
            {
                if (type == "poll" && (forum.Allowpostspecial & 1) != 1)
                {
                    AddErrLine(string.Format("当前版块 \"{0}\" 不允许发表投票", forum.Name));
                    return;
                }

                if (type == "bonus" && (forum.Allowpostspecial & 4) != 4)
                {
                    AddErrLine(string.Format("当前版块 \"{0}\" 不允许发表悬赏", forum.Name));
                    return;
                }
                if (type == "debate" && (forum.Allowpostspecial & 16) != 16)
                {
                    AddErrLine(string.Format("当前版块 \"{0}\" 不允许发表辩论", forum.Name));
                    return;
                }
            }

            // 验证用户是否有发布投票的权限
            if (type == "poll" && usergroupinfo.Allowpostpoll != 1)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有发布投票的权限", usergroupinfo.Grouptitle));
                needlogin = true;
                return;
            }

            // 验证用户是否有发布悬赏的权限
            if (type == "bonus" && usergroupinfo.Allowbonus != 1)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有发布悬赏的权限", usergroupinfo.Grouptitle));
                needlogin = true;
                return;
            }

            // 验证用户是否有发起辩论的权限
            if (type == "debate" && usergroupinfo.Allowdebate != 1)
            {
                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有发起辩论的权限", usergroupinfo.Grouptitle));
                needlogin = true;
                return;
            }

            if (type == "bonus")
            {
                //当“交易积分设置”有效时(1-8的整数):
                int creditTrans = Scoresets.GetCreditsTrans();
                if (creditTrans <= 0)
                {
                    AddErrLine(string.Format("系统未设置\"交易积分设置\", 无法判断当前要使用的(扩展)积分字段, 暂时无法发布悬赏", usergroupinfo.Grouptitle));
                    return;
                }
                mycurrenttranscredits = Users.GetUserExtCredits(userid, creditTrans);
            }


            //如果不是提交...
            if (!ispost)
            {
                AddLinkCss(BaseConfigs.GetForumPath + "templates/" + templatepath + "/editor.css", "css");

                smilies = Caches.GetSmiliesCache();
                smilietypes = Caches.GetSmilieTypesCache();
                customeditbuttons = Caches.GetCustomEditButtonList();
                topicicons = Caches.GetTopicIconsCache();
            }
            else
            {
                SetBackLink(string.Format("posttopic.aspx?forumid={0}&restore=1&type={1}", forumid, type));

                string postmessage = DNTRequest.GetString("message");
                ForumUtils.WriteCookie("postmessage", postmessage);

                #region 常规项验证

                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }


                if (forum.Applytopictype == 1 && forum.Postbytopictype == 1 && topictypeselectoptions != string.Empty)
                {
                    if (DNTRequest.GetString("typeid").Trim().Equals(""))
                    {
                        AddErrLine("主题类型不能为空");
                    }
                    //检测所选主题分类是否有效
                    if (!Forums.IsCurrentForumTopicType(DNTRequest.GetString("typeid").Trim(), forum.Topictypes))
                    {
                        AddErrLine("错误的主题类型");
                    }
                }
                if (DNTRequest.GetString("title").Trim().Equals(""))
                {
                    AddErrLine("标题不能为空");
                }
                else if (DNTRequest.GetString("title").IndexOf("　") != -1)
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



                // 如果用户上传了附件,则检测用户是否有上传附件的权限
                if (ForumUtils.IsPostFile())
                {
                    if (Attachments.GetAttachmentTypeArray(sbAttachmentTypeSelect.ToString()).Trim() == "")
                    {
                        AddErrLine("系统不允许上传附件");
                    }

                    if (!Forums.AllowPostAttachByUserID(forum.Permuserlist, userid))
                    {
                        if (!Forums.AllowPostAttach(forum.Postattachperm, usergroupid))
                        {
                            AddErrLine("您没有在该版块上传附件的权限");
                        }
                        else if (usergroupinfo.Allowpostattach != 1)
                        {
                            AddErrLine(string.Format("您当前的身份 \"{0}\" 没有上传附件的权限", usergroupinfo.Grouptitle));
                        }
                    }
                }

                #endregion

                #region 投票验证
                if (!DNTRequest.GetString("createpoll").Equals(""))
                {
                    // 验证用户是否有发布投票的权限
                    if (usergroupinfo.Allowpostpoll != 1)
                    {
                        AddErrLine(string.Format("您当前的身份 \"{0}\" 没有发布投票的权限", usergroupinfo.Grouptitle));
                        return;
                    }


                    createpoll = true;
                    pollitem = Utils.SplitString(DNTRequest.GetString("PollItemname"), "\r\n");
                    if (pollitem.Length < 2)
                    {
                        AddErrLine("投票项不得少于2个");
                    }
                    else if (pollitem.Length > config.Maxpolloptions)
                    {
                        AddErrLine(string.Format("系统设置为投票项不得多于{0}个", config.Maxpolloptions));
                    }
                    else
                    {
                        for (int i = 0; i < pollitem.Length; i++)
                        {
                            if (pollitem[i].Trim().Equals(""))
                            {
                                AddErrLine("投票项不能为空");
                            }
                        }
                    }

                    enddatetime = DNTRequest.GetString("enddatetime");
                    if (!Utils.IsDateString(enddatetime))
                    {
                        AddErrLine("投票结束日期格式错误");
                    }
                }
                #endregion

                bool isbonus = type == "bonus";

                #region 悬赏/售价验证

                int topicprice = 0;
                string tmpprice = DNTRequest.GetString("topicprice");

                if (Regex.IsMatch(tmpprice, "^[0-9]*[0-9][0-9]*$") || tmpprice == string.Empty)
                {
                    if (!isbonus)
                    {
                        topicprice = Utils.StrToInt(tmpprice, 0);

                        if (topicprice > maxprice && maxprice > 0)
                        {
                            if (userextcreditsinfo.Unit.Equals(""))
                            {
                                AddErrLine(string.Format("主题售价不能高于 {0} {1}", maxprice.ToString(), userextcreditsinfo.Name));
                            }
                            else
                            {
                                AddErrLine(string.Format("主题售价不能高于 {0} {1}({2})", maxprice.ToString(), userextcreditsinfo.Name, userextcreditsinfo.Unit));
                            }
                        }
                        else if (topicprice > 0 && maxprice <= 0)
                        {
                            AddErrLine(string.Format("您当前的身份 \"{0}\" 未被允许出售主题", usergroupinfo.Grouptitle));
                        }
                        else if (topicprice < 0)
                        {
                            AddErrLine("主题售价不能为负数");
                        }
                    }
                    else
                    {
                        topicprice = Utils.StrToInt(tmpprice, 0);

                        if (usergroupinfo.Allowbonus == 0)
                        {
                            AddErrLine(string.Format("您当前的身份 \"{0}\" 未被允许进行悬赏", usergroupinfo.Grouptitle));
                        }

                        if (topicprice < usergroupinfo.Minbonusprice || topicprice > usergroupinfo.Maxbonusprice)
                        {
                            AddErrLine(string.Format("悬赏价格超出范围, 您应在 {0} - {1} {2}{3} 范围内进行悬赏", usergroupinfo.Minbonusprice, usergroupinfo.Maxbonusprice,
                                userextcreditsinfo.Unit, userextcreditsinfo.Name));
                        }
                    }
                }
                else
                {
                    if (!isbonus)
                    {
                        AddErrLine("主题售价只能为整数");
                    }
                    else
                    {
                        AddErrLine("悬赏价格只能为整数");
                    }
                }
                #endregion

                string positiveopinion = DNTRequest.GetString("positiveopinion");
                string negativeopinion = DNTRequest.GetString("negativeopinion");
                string terminaltime = DNTRequest.GetString("terminaltime");

                if (type == "debate")
                {
                    if (usergroupinfo.Allowdebate != 1)
                    {
                        AddErrLine(string.Format("您当前的身份 \"{0}\" 没有发起辩论的权限", usergroupinfo.Grouptitle));
                        return;

                    }

                    if (positiveopinion == string.Empty)
                    {
                        AddErrLine("正方观点不能为空");
                    }
                    if (negativeopinion == string.Empty)
                    {
                        AddErrLine("反方观点不能为空");
                    }
                    if (!Utils.IsDateString(terminaltime))
                    {
                        AddErrLine("结束日期格式不正确");
                    }

                }

                if (IsErr())
                {
                    return;
                }


                int iconid = DNTRequest.GetInt("iconid", 0);
                if (iconid > 15 || iconid < 0)
                {
                    iconid = 0;
                }
                int hide = 0;
                if (ForumUtils.IsHidePost(postmessage) && usergroupinfo.Allowhidecode == 1)
                {
                    hide = 1;
                }

                string curdatetime = Utils.GetDateTime();

                TopicInfo topicinfo = new TopicInfo();
                topicinfo.Fid = forumid;
                topicinfo.Iconid = iconid;
                if (useradminid == 1)
                {
                    topicinfo.Title = Utils.HtmlEncode(DNTRequest.GetString("title"));
                    message = Utils.HtmlEncode(postmessage);
                }
                else
                {
                    topicinfo.Title = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("title")));
                    message = Utils.HtmlEncode(ForumUtils.BanWordFilter(postmessage));
                }


                if (ForumUtils.HasBannedWord(topicinfo.Title) || ForumUtils.HasBannedWord(message))
                {
                    AddErrLine("对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!");
                    return;
                }

                topicinfo.Typeid = DNTRequest.GetInt("typeid", 0);
                if (usergroupinfo.Allowsetreadperm == 1)
                {
                    int topicreadperm = DNTRequest.GetInt("topicreadperm", 0);
                    topicreadperm = topicreadperm > 255 ? 255 : topicreadperm;
                    topicinfo.Readperm = topicreadperm;
                }
                else
                {
                    topicinfo.Readperm = 0;
                }
                topicinfo.Price = topicprice;
                topicinfo.Poster = username;
                topicinfo.Posterid = userid;
                topicinfo.Postdatetime = curdatetime;
                topicinfo.Lastpost = curdatetime;
                topicinfo.Lastposter = username;
                topicinfo.Views = 0;
                topicinfo.Replies = 0;

                if (forum.Modnewposts == 1 && useradminid != 1)
                {
                    if (useradminid > 1)
                    {
                        if (disablepost == 1)
                        {
                            topicinfo.Displayorder = 0;
                        }
                        else
                        {
                            topicinfo.Displayorder = -2;
                        }
                    }
                    else
                    {
                        topicinfo.Displayorder = -2;
                    }
                }
                else
                {
                    topicinfo.Displayorder = 0;
                }

                if (useradminid != 1)
                {
                    if (Scoresets.BetweenTime(config.Postmodperiods) || ForumUtils.HasAuditWord(topicinfo.Title) || ForumUtils.HasAuditWord(message))
                    {
                        topicinfo.Displayorder = -2;
                    }
                }


                topicinfo.Highlight = "";
                topicinfo.Digest = 0;
                topicinfo.Rate = 0;
                topicinfo.Hide = hide;
                //topicinfo.Poll = 0;
                topicinfo.Attachment = 0;
                topicinfo.Moderated = 0;
                topicinfo.Closed = 0;

                string htmltitle = DNTRequest.GetString("htmltitle").Trim();
                if (htmltitle != string.Empty && Utils.HtmlDecode(htmltitle).Trim() != topicinfo.Title)
                {
                    topicinfo.Magic = 11000;
                    //按照  附加位/htmltitle(1位)/magic(3位)/以后扩展（未知位数） 的方式来存储
                    //例： 11001
                }

                //标签(Tag)操作                
                string tags = DNTRequest.GetString("tags").Trim();
                string[] tagArray = null;
                if (enabletag && tags != string.Empty)
                {
                    tagArray = Utils.SplitString(tags, " ", true, 2, 10);
                    if (tagArray.Length > 0 && tagArray.Length <= 5)
                    {
                        if (topicinfo.Magic == 0)
                        {
                            topicinfo.Magic = 10000;
                        }
                        topicinfo.Magic = Utils.StrToInt(topicinfo.Magic.ToString() + "1", 0);
                    }
                    else
                    {

                        AddErrLine("超过标签数的最大限制，最多可填写 5 个标签");
                        return;
                    }
                }

                if (isbonus)
                {
                    topicinfo.Special = 2;

                    //检查积分是否足够
                    if (mycurrenttranscredits < topicprice)
                    {
                        AddErrLine("您的积分不足, 无法进行悬赏");
                        return;
                    }
                    else
                    {
                        Users.UpdateUserExtCredits(topicinfo.Posterid, Scoresets.GetCreditsTrans(), -topicprice);
                    }
                }

                if (type == "poll")
                {
                    topicinfo.Special = 1;
                }
                //辩论帖
                if (type == "debate")
                {
                    topicinfo.Special = 4;
                }

                int topicid = Topics.CreateTopic(topicinfo);
                //保存htmltitle
                if (canhtmltitle && htmltitle != string.Empty && htmltitle != topicinfo.Title)
                {
                    Topics.WriteHtmlTitleFile(htmltitle, topicid);
                }

                if (enabletag && tagArray != null && tagArray.Length > 0)
                {
                    if (ForumUtils.HasBannedWord(tags))
                    {
                        AddErrLine("标签中含有系统禁止词语,请修改");
                        return;
                    }

                    ForumTags.CreateTopicTags(tagArray, topicid, userid, curdatetime);
                }

                if (type == "debate")
                {
                    DebateInfo debatetopic = new DebateInfo();
                    debatetopic.Tid = topicid;
                    debatetopic.Positiveopinion = positiveopinion;
                    debatetopic.Negativeopinion = negativeopinion;
                    //debatetopic.Positivecolor = DNTRequest.GetString("positivecolor");
                    //debatetopic.Negativecolor = DNTRequest.GetString("negativecolor");
                    debatetopic.Terminaltime = Convert.ToDateTime(DNTRequest.GetString("terminaltime"));
                    Topics.AddDebateTopic(debatetopic);
                }

                PostInfo postinfo = new PostInfo();
                postinfo.Fid = forumid;
                postinfo.Tid = topicid;
                postinfo.Parentid = 0;
                postinfo.Layer = 0;
                postinfo.Poster = username;
                postinfo.Posterid = userid;
                if (useradminid == 1)
                {
                    postinfo.Title = Utils.HtmlEncode(DNTRequest.GetString("title"));
                }
                else
                {
                    postinfo.Title = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("title")));
                }

                postinfo.Postdatetime = curdatetime;
                postinfo.Message = message;
                postinfo.Ip = DNTRequest.GetIP();
                postinfo.Lastedit = "";

                if (ForumUtils.HasAuditWord(postinfo.Message))
                {
                    postinfo.Invisible = 1;
                }

                if (forum.Modnewposts == 1 && useradminid != 1)
                {
                    if (useradminid > 1)
                    {
                        if (disablepost == 1)
                        {
                            postinfo.Invisible = 0;
                        }
                        else
                        {
                            postinfo.Invisible = 1;
                        }
                    }
                    else
                    {
                        postinfo.Invisible = 1;
                    }
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
                        postinfo.Invisible = 0;
                    }
                }



                postinfo.Usesig = Utils.StrToInt(DNTRequest.GetString("usesig"), 0);
                postinfo.Htmlon = 1;

                postinfo.Smileyoff = smileyoff;
                if (smileyoff == 0 && forum.Allowsmilies == 1)
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
                postinfo.Topictitle = topicinfo.Title;

                int postid = 0;

                try
                {
                    postid = Posts.CreatePost(postinfo);
                }
                catch
                {
                    TopicAdmins.DeleteTopics(topicid.ToString(), false);
                    AddErrLine("帖子保存出现异常");
                    return;
                }

                Topics.AddParentForumTopics(forum.Parentidlist.Trim(), 1, 1);


                //设置用户的积分
                ///首先读取版块内自定义积分
                ///版设置了自定义积分则使用，否则使用论坛默认积分
                float[] values = null;
                if (!forum.Postcredits.Equals(""))
                {
                    int index = 0;
                    float tempval = 0;
                    values = new float[8];
                    foreach (string ext in Utils.SplitString(forum.Postcredits, ","))
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
                        tempval = Utils.StrToFloat(ext, 0);
                        values[index - 1] = tempval;
                        index++;
                        if (index > 8)
                        {
                            break;
                        }
                    }
                }

                //if (values != null)
                //{
                //    ///使用版块内积分
                //    UserCredits.UpdateUserCreditsByPostTopic(userid, values);
                //}
                //else
                //{
                //    ///使用默认积分
                //    UserCredits.UpdateUserCreditsByPostTopic(userid);
                //}

                StringBuilder itemvaluelist = new StringBuilder("");
                if (createpoll)
                {
                    // 生成以回车换行符为分割的项目与结果列
                    for (int i = 0; i < pollitem.Length; i++)
                    {
                        itemvaluelist.Append("0\r\n");
                    }

                    string PollItemname = Utils.HtmlEncode(DNTRequest.GetFormString("PollItemname"));
                    if (PollItemname != "")
                    {
                        int multiple = DNTRequest.GetString("multiple") == "on" ? 1 : 0;
                        int maxchoices = 0;
                        if (multiple <= 0)
                        {
                            multiple = 0;
                        }

                        if (multiple == 1)
                        {
                            maxchoices = DNTRequest.GetInt("maxchoices", 1);
                            if (maxchoices > pollitem.Length)
                            {
                                maxchoices = pollitem.Length;
                            }
                        }

                        if (!Polls.CreatePoll(topicid, multiple, pollitem.Length, PollItemname.Trim(), itemvaluelist.ToString().Trim(), enddatetime, userid, maxchoices, DNTRequest.GetString("visiblepoll") == "on" ? 1 : 0))
                        {
                            AddErrLine("投票错误");
                            return;
                        }
                    }
                    else
                    {
                        AddErrLine("投票项为空");
                        return;
                    }
                }


                sb = new StringBuilder();
                sb.Remove(0, sb.Length);

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
                    int errorAttachment = Attachments.BindAttachment(attachmentinfo, postid, sb, topicid, userid);
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
                    //加入相册
                    if (config.Enablealbum == 1 && apb != null)
                    {
                        sb.Append(apb.CreateAttachment(attachmentinfo, usergroupid, userid, username));
                    }
                }

                //添加日志的操作
                SpacePluginBase spb = SpacePluginProvider.GetInstance();
                if (DNTRequest.GetFormString("addtoblog") == "on" && spb != null)
                {
                    if (userid != -1 && spaceid > 0)
                    {
                        spb.CreateTopic(topicinfo, postinfo, attachmentinfo);
                    }
                    else
                    {
                        AddMsgLine("您的个人空间尚未开通, 无法同时添加为日志");
                    }
                }

                OnlineUsers.UpdateAction(olid, UserAction.PostTopic.ActionID, forumid, forumname, -1, "", config.Onlinetimeout);
                // 更新在线表中的用户最后发帖时间
                OnlineUsers.UpdatePostTime(olid);

                if (sb.Length > 0)
                {
                    SetUrl(base.ShowTopicAspxRewrite(topicid, 0));
                    SetMetaRefresh(5);
                    SetShowBackLink(true);
                    sb.Insert(0, "<table cellspacing=\"0\" cellpadding=\"4\" border=\"0\"><tr><td colspan=2 align=\"left\"><span class=\"bold\"><nobr>发表主题成功,但以下附件上传失败:</nobr></span><br /></td></tr>");
                    sb.Append("</table>");
                    AddMsgLine(sb.ToString());
                }
                else
                {

                    SetShowBackLink(false);
                    if (useradminid != 1)
                    {
                        bool needaudit = false; //是否需要审核

                        if (Scoresets.BetweenTime(config.Postmodperiods))
                        {
                            needaudit = true;
                        }
                        else
                        {
                            if (forum.Modnewposts == 1 && useradminid != 1)
                            {
                                if (useradminid > 1)
                                {
                                    if (disablepost == 1 && topicinfo.Displayorder != -2)
                                    {
                                        if (useradminid == 3 && !Moderators.IsModer(useradminid, userid, forumid))
                                        {
                                            needaudit = true;
                                        }
                                        else
                                        {
                                            needaudit = false;
                                        }
                                    }
                                    else
                                    {
                                        needaudit = true;
                                    }
                                }
                                else
                                {
                                    needaudit = true;
                                }
                            }
                            else
                            {
                                if (useradminid != 1 && topicinfo.Displayorder == -2)
                                {
                                    needaudit = true;
                                }
                            }
                        }
                        if (needaudit)
                        {
                            SetUrl(base.ShowForumAspxRewrite(forumid, 0));
                            SetMetaRefresh();
                            AddMsgLine("发表主题成功, 但需要经过审核才可以显示. 返回该版块");
                        }
                        else
                        {
                            PostTopicSucceed(values, topicinfo, topicid);
                        }
                    }
                    else
                    {
                        PostTopicSucceed(values, topicinfo, topicid);
                    }
                }
                ForumUtils.WriteCookie("postmessage", "");


                //如果已登录就不需要再登录
                if (needlogin && userid > 0)
                    needlogin = false;
            }
        }

        /// <summary>
        /// 发帖成功
        /// </summary>
        /// <param name="values">版块积分设置</param>
        /// <param name="topicinfo">主题信息</param>
        /// <param name="topicid">主题ID</param>
        private void PostTopicSucceed(float[] values, TopicInfo topicinfo, int topicid)
        {
            if (values != null)
            {
                ///使用版块内积分
                UserCredits.UpdateUserCreditsByPostTopic(userid, values);
            }
            else
            {
                ///使用默认积分
                UserCredits.UpdateUserCreditsByPostTopic(userid);
            }
            SetUrl(topicinfo.Special == 4 ? ShowDebateAspxRewrite(topicid) : ShowTopicAspxRewrite(topicid, 0));
            SetMetaRefresh();
            AddMsgLine("发表主题成功, 返回该主题<br />(<a href=\"" + base.ShowForumAspxRewrite(forumid, 0) + "\">点击这里返回 " + forumname + "</a>)<br />");
        }
    }
}
