using System;
using System.Collections;
using System.Data;
using System.Text;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Web.UI;
using Discuz.Data;
using System.IO;
using Discuz.Common.Generic;
using System.Text.RegularExpressions;
using Discuz.Plugin.Album;

namespace Discuz.Web
{
    /// <summary>
    /// 编辑帖子页面
    /// </summary>
    public class editpost : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 帖子所属版块Id
        /// </summary>
        public int forumid;
        /// <summary>
        /// 帖子所属版块名称
        /// </summary>
        public string forumname;
        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav;
        /// <summary>
        /// 帖子信息
        /// </summary>
        public PostInfo postinfo;
        /// <summary>
        /// 帖子所属主题信息
        /// </summary>
        public TopicInfo topic;
        /// <summary>
        /// 投票选项列表
        /// </summary>
        public DataTable polloptionlist;
        /// <summary>
        /// 投票帖类型
        /// </summary>
        public PollInfo pollinfo;
        /// <summary>
        /// 附件列表
        /// </summary>
        public DataTable attachmentlist;
        /// <summary>
        /// 附件数
        /// </summary>
        public int attachmentcount;
        /// <summary>
        /// 投票截止时间
        /// </summary>
        //public string pollenddatetime;
        /// <summary>
        /// 帖子内容
        /// </summary>
        public string message;
        /// <summary>
        /// 表情的JavaScript数组
        /// </summary>
        public string smilies;
        /// <summary>
        /// 自定义编辑器按钮
        /// </summary>
        public string customeditbuttons;
        /// <summary>
        /// 主题图标
        /// </summary>
        public string topicicons;
        /// <summary>
        /// 是否进行URL解析
        /// </summary>
        public int parseurloff;
        /// <summary>
        /// 是否进行表情解析
        /// </summary>
        public int smileyoff;
        /// <summary>
        /// 是否进行Discuz!NT代码解析
        /// </summary>
        public int bbcodeoff;
        /// <summary>
        /// 是否使用签名
        /// </summary>
        public int usesig;
        /// <summary>
        /// 是否允许[img]代码
        /// </summary>
        public int allowimg;
        /// <summary>
        /// 是否受发帖控制限制
        /// </summary>
        public int disablepostctrl;
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
        /// 当前版块的主题类型选项
        /// </summary>
        public string topictypeselectoptions;
        /// <summary>
        /// 表情分类
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
        /// 是否允许将图片放入相册
        /// </summary>
        public bool caninsertalbum;
        /// <summary>
        /// 是否显示下载链接
        /// </summary>		
        public bool allowviewattach = false;
        /// <summary>
        /// 是否有Html标题的权限
        /// </summary>
        public bool canhtmltitle = false;
        /// <summary>
        /// 当前帖的Html标题
        /// </summary>
        public string htmltitle = "";
        /// <summary>
        /// 第一页表情的JSON
        /// </summary>
        public string firstpagesmilies = string.Empty;
        /// <summary>
        /// 主题所用标签
        /// </summary>
        public string topictags = string.Empty;
        /// <summary>
        /// 本版是否启用了Tag
        /// </summary>
        public bool enabletag = false;

        #endregion
        // 是否允许编辑帖子, 初始false为不允许
        bool alloweditpost = false;

        AlbumPluginBase apb = AlbumPluginProvider.GetInstance();

        protected override void ShowPage()
        {

            forumnav = "";
            //maxprice = usergroupinfo.Maxprice > Scoresets.GetMaxIncPerTopic() ? Scoresets.GetMaxIncPerTopic() : usergroupinfo.Maxprice;
            maxprice = usergroupinfo.Maxprice;
            AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(useradminid);
            firstpagesmilies = Caches.GetSmiliesFirstPageCache();
            this.disablepostctrl = 0;
            if (admininfo != null)
            {
                this.disablepostctrl = admininfo.Disablepostctrl;
            }

            int topicid = DNTRequest.GetInt("topicid", -1);
            int postid = DNTRequest.GetInt("postid", -1);

            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }

            // 如果帖子ID非数字
            if (postid == -1)
            {
                //
                AddErrLine("无效的帖子ID");

                return;
            }

            postinfo = Posts.GetPostInfo(topicid, postid);
            // 如果帖子不存在
            if (postinfo == null)
            {
                //
                AddErrLine("不存在的帖子ID");

                return;
            }


            // 获取主题ID
            if (topicid != postinfo.Tid)
            {
                AddErrLine("主题ID无效");
                return;
            }

            // 如果主题ID非数字
            if (topicid == -1)
            {
                //
                AddErrLine("无效的主题ID");

                return;
            }

            // 获取该主题的信息
            topic = Topics.GetTopicInfo(topicid);
            // 如果该主题不存在
            if (topic == null)
            {
                AddErrLine("不存在的主题ID");

                return;
            }

            //非创始人且作者与当前编辑者不同时
            if (postinfo.Posterid != userid && BaseConfigs.GetFounderUid != userid)
            {
                if (postinfo.Posterid == BaseConfigs.GetFounderUid)
                {
                    AddErrLine("您无权编辑创始人的帖子");
                    return;
                }
                if (postinfo.Posterid != -1)
                {
                    UserGroupInfo postergroup = UserGroups.GetUserGroupInfo(Users.GetShortUserInfo(postinfo.Posterid).Groupid);
                    if (postergroup.Radminid > 0 && postergroup.Radminid < useradminid)
                    {
                        AddErrLine("您无权编辑更高权限人的帖子");
                        return;
                    }
                }
            }

            pagetitle = postinfo.Title;

            ///得到所在版块信息
            forumid = topic.Fid;
            forum = Forums.GetForumInfo(forumid);


            // 如果该版块不存在
            if (forum == null)
            {
                AddErrLine("版块已不存在");
                forum = new ForumInfo();
                return;
            }

            if (forum.Password != "" && Utils.MD5(forum.Password) != ForumUtils.GetCookie("forum" + forumid.ToString() + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                SetBackLink(base.ShowForumAspxRewrite(forumid, 0));
                return;
            }

            forumname = forum.Name;
            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);

            if (forum.Applytopictype == 1)  //启用主题分类
            {
                topictypeselectoptions = Forums.GetCurrentTopicTypesOption(forum.Fid, forum.Topictypes);
            }

            //得到用户可以上传的文件类型
            System.Text.StringBuilder sbAttachmentTypeSelect = new StringBuilder();
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


            //-------------设置帖子的可用功能allhtml,smileyoff,parseurlof,bbcodeoff
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("var Allowhtml=1;\r\n"); //+ allhtml.ToString() + "

            parseurloff = 0;

            smileyoff = 1 - forum.Allowsmilies;
            //sb.Append("var Allowsmilies=" + (1-smileyoff).ToString() + ";\r\n");


            bbcodeoff = 1;
            if (forum.Allowbbcode == 1)
            {
                if (usergroupinfo.Allowcusbbcode == 1)
                {
                    bbcodeoff = 0;
                }
            }
            //sb.Append("var Allowbbcode=" + (1-bbcodeoff).ToString() + ";\r\n");

            usesig = 1;

            allowimg = forum.Allowimgcode;
            //sb.Append("var Allowimgcode=" + allowimg.ToString() + ";\r\n");



            //AddScript(sb.ToString());

            //---------------


            parseurloff = postinfo.Parseurloff;

            if (!DNTRequest.IsPost())
            {
                smileyoff = postinfo.Smileyoff;
            }

            bbcodeoff = 1;
            if (usergroupinfo.Allowcusbbcode == 1)
            {
                bbcodeoff = postinfo.Bbcodeoff;
            }
            usesig = postinfo.Usesig;


            if (!Forums.AllowViewByUserID(forum.Permuserlist, userid)) //判断当前用户在当前版块浏览权限
            {
                if (forum.Viewperm == null || forum.Viewperm == string.Empty)//当板块权限为空时，按照用户组权限
                {
                    if (usergroupinfo.Allowvisit != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有浏览该版块的权限");
                        return;
                    }
                }
                else//当板块权限不为空，按照板块权限
                {
                    if (!Forums.AllowView(forum.Viewperm, usergroupid))
                    {
                        AddErrLine("您没有浏览该版块的权限");
                        return;
                    }
                }
            }


            //当前用户是否有允许下载附件权限
            if (Forums.AllowGetAttachByUserID(forum.Permuserlist, userid))
            {
                allowviewattach = true;
            }
            else
            {
                if (forum.Getattachperm == null || forum.Getattachperm == string.Empty)//权限设置为空时，根据用户组权限判断
                {
                    // 验证用户是否有有允许下载附件权限
                    if (usergroupinfo.Allowgetattach == 1)
                    {
                        allowviewattach = true;
                    }
                }
                else if (Forums.AllowGetAttach(forum.Getattachperm, usergroupid))
                {
                    allowviewattach = true;
                }
            }


            //是否有上传附件的权限
            if (Forums.AllowPostAttachByUserID(forum.Permuserlist, userid))
            {
                canpostattach = true;
            }
            else
            {
                if (forum.Postattachperm == null || forum.Postattachperm == "")
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


                // 判断当前用户是否有修改权限
                // 检查是否具有版主的身份
                //if (!Moderators.IsModer(useradminid, userid, forumid) || AdminGroups.GetAdminGroupInfo(useradminid) == null)
                if (!Moderators.IsModer(useradminid, userid, forumid))
                {
                    if (postinfo.Posterid != userid)
                    {
                        AddErrLine("你并非作者, 且你当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有修改该帖的权限");

                        return;
                    }
                    else if (config.Edittimelimit > 0 && Utils.StrDateDiffMinutes(postinfo.Postdatetime, config.Edittimelimit) > 0)
                    {
                        AddErrLine("抱歉, 系统规定只能在帖子发表" + config.Edittimelimit.ToString() + "分钟内才可以修改");
                        return;

                    }

                }



            userextcreditsinfo = Scoresets.GetScoreSet(Scoresets.GetCreditsTrans());

            //bool allowvote = false;
            if (topic.Special == 1 && postinfo.Layer == 0)
            {
                pollinfo = Polls.GetPollInfo(topicid);                 

                //if (Polls.GetVoters(topicid, userid, username, out allowvote).Equals(""))
                {                      
                    polloptionlist = Polls.GetPollOptionList(topicid);
                }
            }
            if (postinfo.Layer == 0)
            {
                canhtmltitle = config.Htmltitle == 1 && Utils.InArray(usergroupid.ToString(), config.Htmltitleusergroup);
            }

            attachmentlist = Attachments.GetAttachmentListByPid(postid);
            attachmentcount = attachmentlist.Rows.Count;

            ShortUserInfo user = Users.GetShortUserInfo(userid);
            if (canpostattach && (config.Enablealbum == 1) && apb != null
                && (UserGroups.GetUserGroupInfo(user.Groupid).Maxspacephotosize - apb.GetPhotoSizeByUserid(userid) > 0))
            {
                caninsertalbum = true;
                albumlist = apb.GetSpaceAlbumByUserId(userid);
            }
            else
            {
                caninsertalbum = false;
            }

            if (Topics.GetMagicValue(topic.Magic, MagicType.HtmlTitle) == 1)
            {
                htmltitle = Topics.GetHtmlTitle(topic.Tid).Replace("\"", "\\\"").Replace("'", "\\'");
            }

            enabletag = (config.Enabletag & forum.Allowtag) == 1;
            if (enabletag && Topics.GetMagicValue(topic.Magic, MagicType.TopicTag) == 1)
            {
                List<TagInfo> tags = ForumTags.GetTagsListByTopic(topic.Tid);

                foreach (TagInfo tag in tags)
                {
                    if (tag.Orderid > -1)
                    {
                        topictags += string.Format(" {0}", tag.Tagname);
                    }
                }
                topictags = topictags.Trim();
            }

            if (!ispost)
            {
                AddLinkCss(BaseConfigs.GetForumPath + "templates/" + templatepath + "/editor.css", "css");

                // 帖子内容
                message = postinfo.Message;

                smilies = Caches.GetSmiliesCache();
                smilietypes = Caches.GetSmilieTypesCache();
                customeditbuttons = Caches.GetCustomEditButtonList();
                topicicons = Caches.GetTopicIconsCache();
            }
            else
            {

                SetBackLink("editpost.aspx?topicid=" + postinfo.Tid + "&postid=" + this.postinfo.Pid.ToString());

                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                if (postinfo.Layer == 0 && forum.Applytopictype == 1 && forum.Postbytopictype == 1 && topictypeselectoptions != string.Empty)
                {
                    if (DNTRequest.GetString("typeid").Trim().Equals("") || DNTRequest.GetString("typeid").Trim().Equals("0"))
                    {
                        AddErrLine("主题类型不能为空");
                        return;
                    }

                    if (!Forums.IsCurrentForumTopicType(DNTRequest.GetString("typeid").Trim(), forum.Topictypes))
                    {
                        AddErrLine("错误的主题类型");
                        return;
                    }
                }

                ///删除附件
                if (DNTRequest.GetInt("isdeleteatt", 0) == 1)
                {
                    int aid = DNTRequest.GetFormInt("aid", 0);
                    if (aid > 0)
                    {
                        if (Attachments.DeleteAttachment(aid) > 0)
                        {
                            attachmentlist = Attachments.GetAttachmentListByPid(postid);
                            attachmentcount = Attachments.GetAttachmentCountByPid(postid);
                        }
                    }

                    AddLinkCss(BaseConfigs.GetForumPath + "templates/" + templatepath + "/editor.css", "css");

                    // 帖子内容
                    message = postinfo.Message;

                    smilies = Caches.GetSmiliesCache();
                    customeditbuttons = Caches.GetCustomEditButtonList();
                    topicicons = Caches.GetTopicIconsCache();

                    ispost = false;

                    return;
                }

                if (DNTRequest.GetString("title").Trim().Equals("") && postinfo.Layer == 0)
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

                string postmessage = DNTRequest.GetString("message");
                if (postmessage.Equals(""))
                {
                    AddErrLine("内容不能为空");
                }


                if (admininfo != null && this.disablepostctrl != 1)
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


                string enddatetime = DNTRequest.GetString("enddatetime");
                string[] pollitem = { };

                if (!DNTRequest.GetString("updatepoll").Equals("")　&& topic.Special == 1)
                {
                         
                    pollinfo.Multiple = DNTRequest.GetInt("multiple", 0);

                    // 验证用户是否有发布投票的权限
                    if (usergroupinfo.Allowpostpoll != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有发布投票的权限");
                        return;
                    }


                    //createpoll = true;
                    pollitem = Utils.SplitString(DNTRequest.GetString("PollItemname"), "\r\n");
                    if (pollitem.Length < 2)
                    {
                        AddErrLine("投票项不得少于2个");
                    }
                    else if (pollitem.Length > config.Maxpolloptions)
                    {
                        AddErrLine("系统设置为投票项不得多于" + config.Maxpolloptions + "个");
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
                }

                int topicprice = 0;
                string tmpprice = DNTRequest.GetString("topicprice");

                if (Regex.IsMatch(tmpprice, "^[0-9]*[0-9][0-9]*$") || tmpprice == string.Empty)
                {
                    if (topic.Special != 2)
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
                    if (topic.Special != 2)
                    {
                        AddErrLine("主题售价只能为整数");
                    }
                    else
                    {
                        AddErrLine("悬赏价格只能为整数");
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


                string curdatetime = Utils.GetDateTime();


                if (useradminid == 1)
                {
                    postinfo.Title = Utils.HtmlEncode(DNTRequest.GetString("title"));
                    postinfo.Message = Utils.HtmlEncode(DNTRequest.GetString("message"));
                }
                else
                {
                    postinfo.Title = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("title")));
                    postinfo.Message = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("message")));
                }
               

                if (ForumUtils.HasBannedWord(postinfo.Title) || ForumUtils.HasBannedWord(postinfo.Message))
                {
                    AddErrLine("对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!");
                    return;
                }

                if (useradminid != 1)
                {
                    if (ForumUtils.HasAuditWord(postinfo.Title) || ForumUtils.HasAuditWord(postinfo.Message))
                    {
                        AddErrLine("对不起, 管理员设置了需要对发帖进行审核, 您没有权力编辑已通过审核的帖子, 请返回修改!");
                        return;
                        //postinfo.Invisible = 1;

                        //if (postinfo.Layer == 0) //当为主题帖时
                        //{
                        //    topic.Displayorder = -2;
                        //}
                    }
                }
                //如果是不是管理员组,或者编辑间隔超过60秒,则附加编辑信息
                if (Utils.StrDateDiffSeconds(postinfo.Postdatetime, 60) > 0 && config.Editedby == 1 && useradminid != 1)
                {
                    postinfo.Lastedit = username + " 最后编辑于 " + Utils.GetDateTime();
                }
                postinfo.Usesig = Utils.StrToInt(DNTRequest.GetString("usesig"), 0);
                postinfo.Htmlon = 1;
                postinfo.Smileyoff = smileyoff;
                if (smileyoff == 0)
                {
                    postinfo.Smileyoff = Utils.StrToInt(DNTRequest.GetString("smileyoff"), 0);
                }

                postinfo.Bbcodeoff = 1;
                if (usergroupinfo.Allowcusbbcode == 1)
                {
                    postinfo.Bbcodeoff = Utils.StrToInt(DNTRequest.GetString("bbcodeoff"), 0);
                }
                postinfo.Parseurloff = Utils.StrToInt(DNTRequest.GetString("parseurloff"), 0);

                // 如果所在管理组存在且所在管理组有删帖的管理权限
                if (admininfo != null && admininfo.Alloweditpost == 1 && Moderators.IsModer(useradminid, userid, forumid))
                {
                    alloweditpost = true;
                }
                else if (userid != postinfo.Posterid)
                {
                    AddErrLine("您当前的身份不是作者");
                    return;
                }
                else
                {
                    alloweditpost = true;
                }


                if (alloweditpost)
                {

                    if (postinfo.Layer == 0)
                    {

                        ///修改投票信息
                        StringBuilder itemvaluelist = new StringBuilder("");
                        if (topic.Special == 1)
                        {
                            string pollItemname = Utils.HtmlEncode(DNTRequest.GetFormString("PollItemname"));

                            if (pollItemname != "")
                            {
                                int multiple = DNTRequest.GetString("multiple") == "on" ? 1 : 0;
                                int maxchoices = 0;

                                if (multiple == 1)
                                {
                                    maxchoices = DNTRequest.GetInt("maxchoices", 0);
                                    if (maxchoices > pollitem.Length)
                                    {
                                        maxchoices = pollitem.Length;
                                    }
                                }

                                if (!Polls.UpdatePoll(topicid, multiple, pollitem.Length, DNTRequest.GetFormString("PollOptionID").Trim(), pollItemname.Trim(), DNTRequest.GetFormString("PollOptionDisplayOrder").Trim(), enddatetime, maxchoices, DNTRequest.GetString("visiblepoll") == "on" ? 1 : 0))
                                {
                                    AddErrLine("投票错误,请检查显示顺序");
                                    return;
                                }
                            }
                            else
                            {
                                AddErrLine("投票项为空");
                                return;
                            }
                        }


                        int iconid = DNTRequest.GetInt("iconid", 0);
                        if (iconid > 15 || iconid < 0)
                        {
                            iconid = 0;
                        }

                        topic.Iconid = iconid;
                        topic.Title = postinfo.Title;

                        //悬赏差价处理
                        if (topic.Special == 2)
                        {
                            int pricediff = topicprice - topic.Price;
                            if (pricediff > 0)
                            {
                                //扣分
                                if (Users.GetUserExtCredits(topic.Posterid, Scoresets.GetCreditsTrans()) < pricediff)
                                {
                                    AddErrLine("主题作者积分不足, 无法追加悬赏");
                                    return;
                                }
                                else
                                {
                                    topic.Price = topicprice;
                                    Users.UpdateUserExtCredits(topic.Posterid, Scoresets.GetCreditsTrans(), -pricediff);
                                }
                            }
                            else if (pricediff < 0)
                            {
                                AddErrLine("不能降低悬赏价格");
                                return;
                            }
                        }
                        else if (topic.Special == 0)//普通主题,出售
                        {
                            topic.Price = topicprice;
                        }
                        if (usergroupinfo.Allowsetreadperm == 1)
                        {
                            int topicreadperm = DNTRequest.GetInt("topicreadperm", 0);
                            topicreadperm = topicreadperm > 255 ? 255 : topicreadperm;
                            topic.Readperm = topicreadperm;
                        }                        
                        if (ForumUtils.IsHidePost(postmessage) && usergroupinfo.Allowhidecode == 1)
                        {
                            topic.Hide = 1;
                        }

                        topic.Typeid = DNTRequest.GetFormInt("typeid", 0);

                        string htmltitle = DNTRequest.GetString("htmltitle").Trim();
                        if (htmltitle != string.Empty && Utils.HtmlDecode(htmltitle).Trim() != topic.Title)
                        {
                            topic.Magic = 11000;
                            //按照  附加位/htmltitle(1位)/magic(3位)/以后扩展（未知位数） 的方式来存储
                            //例： 11001
                        }
                        else
                        {
                            topic.Magic = 0;
                        }

                        ForumTags.DeleteTopicTags(topic.Tid);
                        Topics.DeleteRelatedTopics(topic.Tid);
                        string tags = DNTRequest.GetString("tags").Trim();
                        string[] tagArray = null;
                        if (enabletag && tags != string.Empty)
                        {
                            if (ForumUtils.HasBannedWord(tags))
                            {
                                AddErrLine("标签中含有系统禁止词语,请修改");
                                return;
                            }

                            tagArray = Utils.SplitString(tags, " ", true, 10);
                            if (tagArray.Length > 0)
                            {
                                topic.Magic = Topics.SetMagicValue(topic.Magic, MagicType.TopicTag, 1);
                                ForumTags.CreateTopicTags(tagArray, topic.Tid, userid, curdatetime);
                            }
                        }

                        Topics.UpdateTopic(topic);

                        //保存htmltitle
                        if (canhtmltitle && htmltitle != string.Empty && htmltitle != topic.Title)
                        {
                            Topics.WriteHtmlTitleFile(htmltitle, topic.Tid);
                        }


                    }
                    else
                    {
                        if (ForumUtils.IsHidePost(postmessage) && usergroupinfo.Allowhidecode == 1)
                        {
                            topic.Hide = 1;
                        }

                        Topics.UpdateTopicHide(topicid);

                    }

                    // 通过验证的用户可以编辑帖子
                    Posts.UpdatePost(postinfo);






                    sb = new StringBuilder();
                    sb.Remove(0, sb.Length);

                    //编辑帖子时如果进行了批量删除附件
                    string delAttId = DNTRequest.GetFormString("deleteaid");
                    if (delAttId != string.Empty)
                    {
                        if (Utils.IsNumericArray(delAttId.Split(',')))//如果要删除的附件ID列表为数字数组
                        {
                            Attachments.DeleteAttachment(delAttId);

                        }
                    }
                    //编辑帖子时如果进行了更新附件操作
                    string updatedAttId = DNTRequest.GetFormString("attachupdatedid");//被更新的附件Id列表
                    string updateAttId = DNTRequest.GetFormString("attachupdateid");//所有已上传的附件Id列表
                    string[] descriptionArray = DNTRequest.GetFormString("attachupdatedesc").Split(',');//所有已上传的附件的描述
                    string[] readpermArray = DNTRequest.GetFormString("attachupdatereadperm").Split(',');//所有已上传得附件的阅读权限

                    ArrayList updateAttArrayList = new ArrayList();
                    if (updateAttId != string.Empty)
                    {
                        foreach (string s in updateAttId.Split(','))
                        {
                            if (!Utils.InArray(s, delAttId, ","))//已上传的附件Id不在被删除的附件Id列表中时
                            {
                                updateAttArrayList.Add(s);
                            }
                        }
                    }

                    string[] updateAttArray = (string[])updateAttArrayList.ToArray(typeof(string));

                    if (updateAttId != string.Empty)//原来有附件
                    {
                        //						if (updatedAttId != string.Empty)//原来的附件有更新
                        //						{
                        int watermarkstate = config.Watermarkstatus;

                        if (forum.Disablewatermark == 1)
                            watermarkstate = 0;

                        string[] updatedAttArray = updatedAttId.Split(',');

                        string filekey = "attachupdated";

                        //保存新的文件
                        AttachmentInfo[] attArray =
                            ForumUtils.SaveRequestFiles(forumid, config.Maxattachments + updateAttArray.Length,
                                                        usergroupinfo.Maxsizeperday, usergroupinfo.Maxattachsize, MaxTodaySize,
                                                        attachextensions, watermarkstate, config, filekey);

                        if (Utils.IsNumericArray(updateAttArray))
                        {
                            for (int i = 0; i < updateAttArray.Length; i++) //遍历原来所有附件
                            {
                                string attachmentId = updateAttArray[i];
                                if (Utils.InArray(attachmentId, updatedAttArray)) //附件文件被更新
                                {
                                    if (Utils.InArray(attachmentId, delAttId, ","))//附件进行了删除操作, 则不操作此附件,即使其也被更新
                                    {
                                        continue;
                                    }
                                    //更新附件
                                    int attachmentUpdatedIndex = GetAttachmentUpdatedIndex(attachmentId, updatedAttArray);//获取此次上传的被更新附件在数组中的索引
                                    if (attachmentUpdatedIndex > -1)//附件索引存在
                                    {
                                        if (attArray[attachmentUpdatedIndex].Sys_noupload.Equals(string.Empty)) //由此属性为空可以判断上传成功
                                        {
                                            //获取将被更新的附件信息
                                            AttachmentInfo attachmentInfo =
                                                Attachments.GetAttachmentInfo(Utils.StrToInt(updatedAttArray[attachmentUpdatedIndex], 0));
                                            if (attachmentInfo != null)
                                            {
                                                if (attachmentInfo.Filename.Trim().ToLower().IndexOf("http") < 0)
                                                {
                                                    //删除原来的文件
                                                    File.Delete(
                                                        Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/" +
                                                                         attachmentInfo.Filename));
                                                }

                                                //记住Aid以便稍后更新
                                                attArray[attachmentUpdatedIndex].Aid = attachmentInfo.Aid;
                                                attArray[attachmentUpdatedIndex].Description = descriptionArray[i];
                                                int att_readperm = Utils.StrToInt(readpermArray[i], 0);
                                                att_readperm = att_readperm > 255 ? 255 : att_readperm;
                                                attArray[attachmentUpdatedIndex].Readperm = att_readperm;

                                                Attachments.UpdateAttachment(attArray[attachmentUpdatedIndex]);
                                            }
                                        }
                                        else //上传失败的附件，稍后提示
                                        {
                                            sb.Append("<tr><td align=\"left\">");
                                            sb.Append(attArray[attachmentUpdatedIndex].Attachment);
                                            sb.Append("</td>");
                                            sb.Append("<td align=\"left\">");
                                            sb.Append(attArray[attachmentUpdatedIndex].Sys_noupload);
                                            sb.Append("</td></tr>");
                                        }
                                    }
                                }
                                else //仅修改了阅读权限和描述等
                                {
                                    if (Utils.InArray(updateAttArray[i], delAttId, ","))
                                    {
                                        continue;
                                    }
                                    if ((attachmentlist.Rows[i]["readperm"].ToString() != readpermArray[i]) ||
                                        (attachmentlist.Rows[i]["description"].ToString().Trim() != descriptionArray[i]))
                                    {
                                        int att_readperm = Utils.StrToInt(readpermArray[i], 0);
                                        att_readperm = att_readperm > 255 ? 255 : att_readperm;
                                        Attachments.UpdateAttachment(Utils.StrToInt(updateAttArray[i], 0), att_readperm,
                                                                     descriptionArray[i]);
                                    }
                                }
                            }
                        }
                        //						}
                    }

                    ///上传附件
                    int watermarkstatus = config.Watermarkstatus;
                    if (forum.Disablewatermark == 1)
                    {
                        watermarkstatus = 0;
                    }
                    AttachmentInfo[] attachmentinfo = ForumUtils.SaveRequestFiles(forumid, config.Maxattachments - updateAttArray.Length, usergroupinfo.Maxsizeperday, usergroupinfo.Maxattachsize, MaxTodaySize, attachextensions, watermarkstatus, config, "postfile");
                    if (attachmentinfo != null)
                    {
                        if (attachmentinfo.Length > config.Maxattachments - updateAttArray.Length)
                        {
                            AddErrLine("系统设置为附件不得多于" + config.Maxattachments + "个");
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
                    if (config.Enablealbum == 1 && apb != null)
                    {
                        sb.Append(apb.CreateAttachment(attachmentinfo, usergroupid, userid, username));
                    }


                    //编辑后跳转地址
                    if (topic.Special == 4)
                    { 
                        //辩论地址
                        SetUrl(Urls.ShowDebateAspxRewrite(topicid));
                    }
                    else if (DNTRequest.GetQueryString("referer") != "")//ajax快速回复将传递referer参数
                    {
                        //SetUrl(Utils.UrlDecode(DNTRequest.GetQueryString("referer")));
                        SetUrl(string.Format("showtopic.aspx?page=end&topicid={0}#{1}", topicid.ToString().Trim(), postinfo.Pid));
                    }
                    else if (DNTRequest.GetQueryString("pageid") != "")//如果不是ajax,则应该是带pageid的参数
                    {
                        if (config.Aspxrewrite == 1)
                        {
                            SetUrl(string.Format("showtopic-{0}-{2}{1}#{3}", topicid.ToString(), config.Extname, DNTRequest.GetString("pageid"), postinfo.Pid));
                        }
                        else
                        {
                            SetUrl(string.Format("showtopic.aspx?&topicid={0}&page={2}#{1}", topicid.ToString(), postinfo.Pid, DNTRequest.GetString("pageid")));
                        }
                    }
                    else//如果都为空.就跳转到第一页(以免意外情况)
                    {
                        if (config.Aspxrewrite == 1)
                        {
                            SetUrl(string.Format("showtopic-{0}{1}", topicid.ToString(), config.Extname));
                        }
                        else
                        {
                            SetUrl(string.Format("showtopic.aspx?&topicid={0}", topicid.ToString()));
                        }
                    }

                    if (sb.Length > 0)
                    {
                        SetMetaRefresh(5);
                        SetShowBackLink(true);
                        sb.Insert(0, "<table cellspacing=\"0\" cellpadding=\"4\" border=\"0\"><tr><td colspan=2 align=\"left\"><span class=\"bold\"><nobr>编辑帖子成功,但以下附件上传失败:</nobr></span><br /></td></tr>");
                        sb.Append("</table>");
                        AddMsgLine(sb.ToString());
                    }
                    else
                    {
                        SetMetaRefresh();
                        SetShowBackLink(false);
                        AddMsgLine("编辑帖子成功, 返回该主题");
                    }
                    // 删除主题游客缓存
                    if (postinfo.Layer == 0)
                    {
                        ForumUtils.DeleteTopicCacheFile(topicid);
                    }
                    return;
                }
                else
                {
                    AddErrLine("您当前的身份没有编辑帖子的权限");
                    return;
                }

            }

        }

        private int GetAttachmentUpdatedIndex(string attachmentId, string[] updatedAttArray)
        {
            for (int i = 0; i < updatedAttArray.Length; i++)
            {
                if (updatedAttArray[i] == attachmentId)
                {
                    return i;
                }
            }

            return -1;
        }

    }
}
