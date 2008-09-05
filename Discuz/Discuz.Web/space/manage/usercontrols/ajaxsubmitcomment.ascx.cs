using System;

using Discuz.Common;
using Discuz.Forum;
using Discuz.Space.Entities;
using Discuz.Space.Provider;
using Discuz.Entity;
using Discuz.Data;

namespace Discuz.Space.Manage
{
	
	/// <summary>
	///	评论提交控件
	/// </summary>
	public class ajaxsubmitcomment : DiscuzSpaceUCBase
	{

		public string completeinfo = "";

		//评论信息
		public string commentcontent = "";

		//作者的EMAIL
		public string commentemail = "";

		//作者名称
		public string commentauthor = "";

		//作者主页
		public string commenturl= "";

		//验证码
		public string vcode =  "";

		//验证码
		public int postid =  0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				if(Discuz.Common.DNTRequest.GetString("load") =="true")
				{
					postid = DNTRequest.GetInt("postid", 0);
					commentcontent = DNTRequest.GetString("commentcontent");
					commentemail = DNTRequest.GetString("commentemail");
					commentauthor = DNTRequest.GetString("commentauthor");
					commenturl = DNTRequest.GetString("commenturl");
					vcode = DNTRequest.GetString("vcode");

					//提交保存时
					if (DNTRequest.GetString("submit") == "true")
					{
						Submit_CategoryInfo();
						return;
					}

					if (userid > 0)
					{
						commentcontent = "";
						commentemail = _userinfo.Email;
						commentauthor = _userinfo.Username;
						commenturl  = _userinfo.Website;
					}
				}

                forumurlnopage = (!forumurlnopage.EndsWith("/"))? forumurlnopage + "/" : forumurlnopage;
			}
		}

		private void Submit_CategoryInfo()
		{
			if (!OnlineUsers.CheckUserVerifyCode(olid, DNTRequest.GetString("vcode")))
			{
				completeinfo = "验证码错误,请重新输入";
				return;
			}

			if (commentcontent == "")
			{
				completeinfo = "请输入评论内容";
				return;
			}

            SpacePostInfo __spacepostinfo = BlogProvider.GetSpacepostsInfo(Space.Data.DbProvider.GetInstance().GetSpacePost(postid));

			if (__spacepostinfo.CommentStatus == 1)
			{
                completeinfo = "当前日志不允许评论";
				return;
			}
		
			if ((__spacepostinfo.CommentStatus == 2)&&(userid < 1))
			{
                completeinfo = "当前日志仅允许注册用户评论";
				return;
			}
	

			SpaceCommentInfo __spacecommentinfo = new SpaceCommentInfo();
			__spacecommentinfo.PostID = postid; 
			__spacecommentinfo.Author = commentauthor != ""?commentauthor:"匿名";
			__spacecommentinfo.Email = commentemail;
			__spacecommentinfo.Url = commenturl;
			__spacecommentinfo.Ip = DNTRequest.GetIP();
			__spacecommentinfo.PostDateTime = DateTime.Now;
			__spacecommentinfo.Content = ForumUtils.BanWordFilter(commentcontent);
			__spacecommentinfo.ParentID = 0;
            __spacecommentinfo.Uid = (commentauthor == username) ? userid: -1;
			__spacecommentinfo.PostTitle = Utils.HtmlEncode(ForumUtils.BanWordFilter(__spacepostinfo.Title));

            Space.Data.DbProvider.GetInstance().AddSpaceComment(__spacecommentinfo);

            Space.Data.DbProvider.GetInstance().CountUserSpaceCommentCountByUserID(__spacepostinfo.Uid, 1);

            Space.Data.DbProvider.GetInstance().CountSpaceCommentCountByPostID(postid, 1);
		
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
