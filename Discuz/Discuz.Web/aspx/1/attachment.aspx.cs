using System;
using System.Web;
using System.Data;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Plugin.Mall;

namespace Discuz.Web
{
	/// <summary>
	/// attachment页的类派生于BasePage类
	/// </summary>
	public class attachment : PageBase
    {
        #region 变量声明
        /// <summary>
        /// 附件所属主题信息
        /// </summary>
		public TopicInfo topic;
        /// <summary>
        /// 附件信息
        /// </summary>
		public AttachmentInfo attachmentinfo;
        /// <summary>
        /// 附件所属版块Id
        /// </summary>
		public int forumid;
        /// <summary>
        /// 附件所属版块名称
        /// </summary>
		public string forumname;
        /// <summary>
        /// 附件所属主题Id
        /// </summary>
		public int topicid;
        /// <summary>
        /// 附件Id
        /// </summary>
		public int attachmentid;
        /// <summary>
        /// 附件所属主题标题
        /// </summary>
		public string topictitle;
        /// <summary>
        /// 论坛导航信息
        /// </summary>
		public string forumnav;
        /// <summary>
        /// 是否需要登录后进行下载
        /// </summary>
	    public bool needlogin = false;
             /// <summary>
        /// 附件商品信息
        /// </summary>
		public Goodsinfo goodsinfo;
        /// <summary>
        /// 商品附件信息
        /// </summary>
		public Goodsattachmentinfo goodsattachmentinfo;
          /// <summary>
        /// 附件所属商品Id
        /// </summary>
		public int goodsid;
          /// <summary>
        /// 附件所属商品标题
        /// </summary>
		public string goodstitle;
        #endregion 变量声明

        protected override void ShowPage()
		{
            pagetitle = "附件下载";

			//　如果当前用户非管理员并且论坛设定了禁止下载附件时间段，当前时间如果在其中的一个时间段内，则不允许用户下载附件
			if (useradminid != 1 && usergroupinfo.Disableperiodctrl != 1)
			{
				string visittime = "";
                if (Scoresets.BetweenTime(config.Attachbanperiods, out visittime))
				{
					AddErrLine("在此时间段( " + visittime + " )内用户不可以下载附件");
					return;
				}
			}

			// 获取附件ID
			attachmentid = DNTRequest.GetInt("attachmentid", -1);
			// 如果附件ID非数字
			if(attachmentid == -1)
			{
				AddErrLine("无效的附件ID");
				return;
			}

            if (DNTRequest.GetString("goodsattach") == "yes")
            {
                GetGoodsAttachInfo(attachmentid);
            }
            else
            {
                // 获取该附件的信息
                attachmentinfo = Attachments.GetAttachmentInfo(attachmentid);
                // 如果该附件不存在
                if (attachmentinfo == null)
                {
                    AddErrLine("不存在的附件ID");
                    return;
                }
                topicid = attachmentinfo.Tid;

                // 获取该主题的信息
                topic = Topics.GetTopicInfo(topicid);
                // 如果该主题不存在
                if (topic == null)
                {
                    AddErrLine("不存在的主题ID");
                    return;
                }

                topictitle = topic.Title;
                forumid = topic.Fid;
                ForumInfo forum = Forums.GetForumInfo(forumid);
                forumname = forum.Name;

                pagetitle = Utils.RemoveHtml(forum.Name);
                forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);

                //添加判断特殊用户的代码
                if (!Forums.AllowViewByUserID(forum.Permuserlist, userid))
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

                 //添加判断特殊用户的代码
                if (!Forums.AllowGetAttachByUserID(forum.Permuserlist, userid))
                {
                    if (forum.Getattachperm == "" || forum.Getattachperm == null)
                    {
                        // 验证用户是否有下载附件的权限
                        if (usergroupinfo.Allowgetattach != 1)
                        {
                            AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有下载或查看附件的权限");
                            if (userid == -1)
                            {
                                needlogin = true;
                            }
                            return;
                        }
                    }
                    else
                    {
                        if (!Forums.AllowGetAttach(forum.Getattachperm, usergroupid))
                        {
                            AddErrLine("您没有在该版块下载附件的权限");
                            if (userid == -1)
                            {
                                needlogin = true;
                            }
                            return;
                        }
                    }
                }

                // 检查用户是否拥有足够的阅读权限
                if ((attachmentinfo.Readperm > usergroupinfo.Readaccess) && (attachmentinfo.Uid != userid) && (!Moderators.IsModer(useradminid, userid, forumid)))
                {
                    AddErrLine("您的阅读权限不够");
                    if (userid == -1)
                    {
                        needlogin = true;
                    }
                    return;
                }
                //如果图片是不直接显示(作为附件显示) 并且不是作者本人下载都会扣分
                if (config.Showimages != 1 || !Utils.IsImgFilename(attachmentinfo.Filename.Trim()) && userid != attachmentinfo.Uid)
                {
                    if (UserCredits.UpdateUserCreditsByDownloadAttachment(userid) == -1)
                    {
                        AddErrLine("您的积分不足");
                        return;
                    }

                }

                if (attachmentinfo.Filename.IndexOf("http") < 0)
                {
                    if (!System.IO.File.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath + @"upload/" + attachmentinfo.Filename)))
                    {
                        AddErrLine("该附件文件不存在或已被删除");
                        return;
                    }
                }

                Attachments.UpdateAttachmentDownloads(attachmentid);

                if (attachmentinfo.Filename.IndexOf("http") < 0)
                {
                    Utils.ResponseFile(Utils.GetMapPath(BaseConfigs.GetForumPath + @"upload/" + attachmentinfo.Filename), System.IO.Path.GetFileName(attachmentinfo.Attachment), attachmentinfo.Filetype);
                    //ResponseFile(BaseConfigs.GetForumPath + "upload/" + attachmentinfo.Filename.Trim(), attachmentinfo.Filetype);
                }
                else
                {
                    //Utils.ResponseFile(attachmentinfo.Filename.Trim(), System.IO.Path.GetFileName(attachmentinfo.Filename).Trim(), attachmentinfo.Filetype);
                    ResponseFile(attachmentinfo.Filename.Trim(), attachmentinfo.Filetype);
                }
            }

		}


        public void GetGoodsAttachInfo(int attachmentid)
        {
            MallPluginBase mpb = MallPluginProvider.GetInstance();
            if (mpb == null)
            {
                AddErrLine("未安装商城插件");
                return;
            }
            // 获取该附件的信息
            goodsattachmentinfo = mpb.GetGoodsAttachmentsByAid(attachmentid);
            // 如果该附件不存在
            if (goodsattachmentinfo == null)
            {
                AddErrLine("不存在的附件ID");
                return;
            }
            goodsid = goodsattachmentinfo.Goodsid;

            // 获取该商品的信息
            goodsinfo = mpb.GetGoodsInfo(goodsid);
            // 如果该商品不存在
            if (goodsinfo == null)
            {
                AddErrLine("不存在的主题ID");
                return;
            }

            goodstitle = goodsinfo.Title;
            forumid = mpb.GetCategoriesFid(goodsinfo.Categoryid);
            ForumInfo forum = Forums.GetForumInfo(forumid);
            forumname = forum.Name;

            pagetitle = Utils.RemoveHtml(forum.Name);
            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);

            //添加判断特殊用户的代码
            if (!Forums.AllowViewByUserID(forum.Permuserlist, userid))
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

            //添加判断特殊用户的代码
            if (!Forums.AllowGetAttachByUserID(forum.Permuserlist, userid))
            {
                if (forum.Getattachperm == "" || forum.Getattachperm == null)
                {
                    // 验证用户是否有下载附件的权限
                    if (usergroupinfo.Allowgetattach != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有下载或查看附件的权限");
                        if (userid == -1)
                        {
                            needlogin = true;
                        }
                        return;
                    }
                }
                else
                {
                    if (!Forums.AllowGetAttach(forum.Getattachperm, usergroupid))
                    {
                        AddErrLine("您没有在该版块下载附件的权限");
                        if (userid == -1)
                        {
                            needlogin = true;
                        }
                        return;
                    }
                }
            }

            // 检查用户是否拥有足够的阅读权限
            if ((goodsattachmentinfo.Readperm > usergroupinfo.Readaccess) && (goodsattachmentinfo.Uid != userid) && (!Moderators.IsModer(useradminid, userid, forumid)))
            {
                AddErrLine("您的阅读权限不够");
                if (userid == -1)
                {
                    needlogin = true;
                }
                return;
            }
            //如果图片是不直接显示(作为附件显示) 并且不是作者本人下载都会扣分
            if (config.Showimages != 1 || !Utils.IsImgFilename(goodsattachmentinfo.Filename.Trim()) && userid != attachmentinfo.Uid)
            {
                if (UserCredits.UpdateUserCreditsByDownloadAttachment(userid) == -1)
                {
                    AddErrLine("您的积分不足");
                    return;
                }

            }

            if (goodsattachmentinfo.Filename.IndexOf("http") < 0)
            {
                if (!System.IO.File.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath + @"upload/" + goodsattachmentinfo.Filename)))
                {
                    AddErrLine("该附件文件不存在或已被删除");
                    return;
                }
            }

            //Attachments.UpdateAttachmentDownloads(attachmentid);

            if (goodsattachmentinfo.Filename.IndexOf("http") < 0)
            {
                Utils.ResponseFile(Utils.GetMapPath(BaseConfigs.GetForumPath + @"upload/" + goodsattachmentinfo.Filename), System.IO.Path.GetFileName(goodsattachmentinfo.Attachment), goodsattachmentinfo.Filetype);
                //ResponseFile(BaseConfigs.GetForumPath + "upload/" + goodsattachmentinfo.Filename.Trim(), goodsattachmentinfo.Filetype);
            }
            else
            {
                ResponseFile(goodsattachmentinfo.Filename.Trim(), goodsattachmentinfo.Filetype);
            }
        }

        public static void ResponseFile(string url, string filetype)
        {
            HttpContext.Current.Response.Redirect(url);
            return;
        }
	}
}
