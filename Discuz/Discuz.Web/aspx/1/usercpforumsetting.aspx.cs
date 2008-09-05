using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
	/// <summary>
	/// 论坛设置
	/// </summary>
	public class usercpforumsetting : PageBase
	{
        /// <summary>
        /// 当前用户信息
        /// </summary>
		public UserInfo user = new UserInfo();

		protected override void ShowPage()
		{
			pagetitle = "用户控制面板";
			
			if (userid == -1)
			{
				AddErrLine("你尚未登录");
				
				return;
			}
			user = Users.GetUserInfo(userid);

			if(DNTRequest.IsPost())
			{
                SetBackLink("usercpforumsetting.aspx");
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }
				UserInfo __userinfo = new UserInfo();
				__userinfo.Uid = userid;
				__userinfo.Tpp = DNTRequest.GetInt("tpp", 0);
				__userinfo.Ppp = DNTRequest.GetInt("ppp", 0);
				__userinfo.Pmsound = DNTRequest.GetInt("pmsound", 0);
				__userinfo.Invisible = DNTRequest.GetInt("invisible", 0);
                __userinfo.Customstatus = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("customstatus")));
                //获取提交的内容并进行脏字和Html处理
                string signature = Utils.HtmlEncode(ForumUtils.BanWordFilter(DNTRequest.GetString("signature")));

                int sigstatus = DNTRequest.GetInt("sigstatus", 0);
                //错误参数值纠正
                if (sigstatus != 0)
                {
                    sigstatus = 1;
                }

                PostpramsInfo _postpramsinfo = new PostpramsInfo();
                _postpramsinfo.Usergroupid = usergroupid;
                _postpramsinfo.Attachimgpost = config.Attachimgpost;
                _postpramsinfo.Showattachmentpath = config.Showattachmentpath;
                _postpramsinfo.Hide = 0;
                _postpramsinfo.Price = 0;
                _postpramsinfo.Sdetail = signature;
                _postpramsinfo.Smileyoff = 1;
                _postpramsinfo.Bbcodeoff = 1 - usergroupinfo.Allowsigbbcode;
                _postpramsinfo.Parseurloff = 1;
                _postpramsinfo.Showimages = usergroupinfo.Allowsigimgcode;
                _postpramsinfo.Allowhtml = 0;
                _postpramsinfo.Signature = 1;
                _postpramsinfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
                _postpramsinfo.Customeditorbuttoninfo = null;//Editors.GetCustomEditButtonListWithInfo();
                _postpramsinfo.Smiliesmax = config.Smiliesmax;
                _postpramsinfo.Signature = 1;


                string sightml = UBB.UBBToHTML(_postpramsinfo);

                //if (usergroupinfo.Maxsigsize<Utils.ClearHtml(sightml).Length)
                if (DNTRequest.GetString("signature").Length > usergroupinfo.Maxsigsize)
                {
                    AddErrLine(string.Format("您的签名长度超过 {0} 字符的限制，请返回修改。", usergroupinfo.Maxsigsize));
                    return;
                }

                if (sightml.Length >= 1000)
                {
                    AddErrLine("您的签名转换后超出系统最大长度， 请返回修改");
                    return;
                }

                __userinfo.Sigstatus = sigstatus;
                __userinfo.Signature = signature;
                __userinfo.Sightml = sightml;
				
				Users.UpdateUserForumSetting(__userinfo);
				OnlineUsers.UpdateInvisible(olid, __userinfo.Invisible);

                ForumUtils.WriteCookie("sigstatus", sigstatus);
				ForumUtils.WriteCookie("tpp", __userinfo.Tpp.ToString());
				ForumUtils.WriteCookie("ppp", __userinfo.Ppp.ToString());
				ForumUtils.WriteCookie("pmsound", __userinfo.Pmsound.ToString());

				SetUrl("usercpforumsetting.aspx");
				SetMetaRefresh();
				SetShowBackLink(true);
				AddMsgLine("修改论坛设置完毕");

			}
		}
	}
}
