<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.showdebate" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 14:21:58.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 14:21:58. 
	*/

	base.OnInit(e);


	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	templateBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	templateBuilder.Append("<head>\r\n");
	templateBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n");
	templateBuilder.Append("" + meta.ToString() + "\r\n");

	if (pagetitle=="首页")
	{

	templateBuilder.Append("<title>" + config.Forumtitle.ToString().Trim() + " " + config.Seotitle.ToString().Trim() + " - Powered by Discuz!NT</title>\r\n");

	}
	else
	{

	templateBuilder.Append("<title>" + pagetitle.ToString() + " - " + config.Forumtitle.ToString().Trim() + " " + config.Seotitle.ToString().Trim() + " - Powered by Discuz!NT</title>\r\n");

	}	//end if

	templateBuilder.Append("<link rel=\"icon\" href=\"favicon.ico\" type=\"image/x-icon\" />\r\n");
	templateBuilder.Append("<link rel=\"shortcut icon\" href=\"favicon.ico\" type=\"image/x-icon\" />\r\n");
	templateBuilder.Append("<!-- 调用样式表 -->\r\n");
	templateBuilder.Append("<link rel=\"stylesheet\" href=\"templates/" + templatepath.ToString() + "/dnt.css\" type=\"text/css\" media=\"all\"  />\r\n");
	templateBuilder.Append("" + link.ToString() + "\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_report.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_utils.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/common.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/menu.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	var aspxrewrite = " + config.Aspxrewrite.ToString().Trim() + ";\r\n");
	templateBuilder.Append("</" + "script>\r\n");
	templateBuilder.Append("" + script.ToString() + "\r\n");
	templateBuilder.Append("</head>\r\n");


	templateBuilder.Append("<body>\r\n");
	templateBuilder.Append("<div id=\"append_parent\"></div><div id=\"ajaxwaitid\"></div>\r\n");
	templateBuilder.Append("<div class=\"wrap\">\r\n");
	templateBuilder.Append("<div id=\"header\">\r\n");
	templateBuilder.Append("	<h2><a href=\"" + config.Forumurl.ToString().Trim() + "\" title=\"Discuz!NT|BBS|论坛 - Powered by Discuz!NT\"><img src=\"templates/" + templatepath.ToString() + "/images/logo.gif\" alt=\"Discuz! Board NT|BBS|论坛\"/></a>\r\n");
	templateBuilder.Append("	</h2>\r\n");

	if (headerad!="")
	{

	templateBuilder.Append("		<div id=\"ad_headerbanner\">" + headerad.ToString() + "</div>\r\n");

	}	//end if

	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div id=\"menu\">\r\n");

	if (config.Isframeshow!=0)
	{

	templateBuilder.Append("	<div class=\"frameswitch\">\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			if(top == self) {\r\n");
	templateBuilder.Append("				document.write('<a href=\"frame.aspx?f=1\" target=\"_top\">分栏模式<\\/a>');\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	templateBuilder.Append("	</div>\r\n");

	}	//end if

	templateBuilder.Append("	<span class=\"avataonline\">\r\n");

	if (userid==-1)
	{

	templateBuilder.Append("			<a href=\"" + forumurl.ToString() + "login.aspx\" class=\"reg\">登录</a>\r\n");
	templateBuilder.Append("			<a href=\"" + forumurl.ToString() + "register.aspx\" class=\"reg\">注册</a>\r\n");

	}
	else
	{

	templateBuilder.Append("			<cite>欢迎:<a class=\"dropmenu\" id=\"viewpro\" onmouseover=\"showMenu(this.id)\">" + username.ToString() + "</a></cite>\r\n");
	templateBuilder.Append("			<a href=\"" + forumurl.ToString() + "logout.aspx?userkey=" + userkey.ToString() + "\">退出</a>\r\n");

	}	//end if

	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<ul>\r\n");

	if (userid!=-1)
	{

	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "usercpinbox.aspx\" class=\"notabs\">短消息</a></li>\r\n");
	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "usercp.aspx\" class=\"reg\">用户中心</a></li>\r\n");

	if (useradminid==1)
	{

	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "admin/index.aspx\" target=\"_blank\"  class=\"reg\">系统设置</a></li>\r\n");

	}	//end if

	templateBuilder.Append("		<li id=\"my\" class=\"dropmenu\" onMouseOver=\"showMenu(this.id);\"><a href=\"#\">我的</a></li>\r\n");

	}	//end if

	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "tags.aspx\" \r\n");

	if (userid==-1)
	{

	templateBuilder.Append("class=\"notabs\"\r\n");

	}	//end if

	templateBuilder.Append(" >标签</a></li>\r\n");

	if (usergroupinfo.Allowviewstats==1)
	{

	templateBuilder.Append("		<li id=\"stats\" class=\"dropmenu\" onmouseover=\"showMenu(this.id)\"><a href=\"stats.aspx\">统计</a></li>\r\n");

	}	//end if

	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "showuser.aspx\">会员</a></li>\r\n");
	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "onlineuser.aspx\">在线</a></li>\r\n");

	if (usergroupinfo.Allowsearch>0)
	{

	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "search.aspx\">搜索</a></li>\r\n");

	}	//end if


	if (config.Enablespace==1)
	{

	templateBuilder.Append("		<li><a href=\"" + config.Spaceurl.ToString().Trim() + "\">" + config.Spacename.ToString().Trim() + "</a></li>\r\n");

	}	//end if


	if (config.Enablealbum==1)
	{

	templateBuilder.Append("		<li><a href=\"" + config.Albumurl.ToString().Trim() + "\">" + config.Albumname.ToString().Trim() + "</a></li>\r\n");

	}	//end if

	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "help.aspx\" target=\"_blank\">帮助</a></li>\r\n");
	templateBuilder.Append("	</ul>\r\n");
	templateBuilder.Append("</div>\r\n");


	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("var templatepath = \"" + templatepath.ToString() + "\";\r\n");
	templateBuilder.Append("var postminchars = parseInt(" + config.Minpostsize.ToString().Trim() + ");\r\n");
	templateBuilder.Append("var postmaxchars = parseInt(" + config.Maxpostsize.ToString().Trim() + ");\r\n");
	templateBuilder.Append("var disablepostctrl = parseInt(" + disablepostctrl.ToString() + ");\r\n");
	templateBuilder.Append("var forumpath = \"" + forumpath.ToString() + "\";\r\n");
	templateBuilder.Append("var ismoder = " + ismoder.ToString() + ";\r\n");
	templateBuilder.Append("var userid = parseInt('" + userid.ToString() + "');\r\n");
	templateBuilder.Append("</" + "script>\r\n");

	if (enabletag)
	{

	templateBuilder.Append("<script type=\"text/javascript\" src=\"cache/tag/closedtags.txt\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"cache/tag/colorfultags.txt\"></" + "script>\r\n");

	}	//end if

	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_showtopic.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/ajax.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_debate.js\"></" + "script>\r\n");

	if (page_err==0)
	{

	templateBuilder.Append("<div id=\"foruminfo\">\r\n");
	templateBuilder.Append("	<div id=\"nav\">\r\n");
	templateBuilder.Append("		<a id=\"forumlist\" href=\"" + config.Forumurl.ToString().Trim() + "\" \r\n");

	if (config.Forumjump==1)
	{

	templateBuilder.Append("		onmouseover=\"showMenu(this.id);\" onmouseout=\"showMenu(this.id);\"\r\n");

	}	//end if

	templateBuilder.Append("		>" + config.Forumtitle.ToString().Trim() + "</a> &raquo; " + forumnav.ToString() + "\r\n");
	int ishtmltitle = Topics.GetMagicValue(topic.Magic, MagicType.HtmlTitle);
	

	if (ishtmltitle==1)
	{

	templateBuilder.Append("		  &raquo; <strong>" + Topics.GetHtmlTitle(topic.Tid).ToString().Trim() + "</strong>\r\n");

	}
	else
	{

	templateBuilder.Append("		  &raquo; <strong>" + topictitle.ToString() + "</strong>\r\n");

	}	//end if

	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div id=\"headsearch\">\r\n");
	templateBuilder.Append("		<div id=\"search\">\r\n");

	if (usergroupinfo.Allowsearch>0)
	{


	templateBuilder.Append("			<form method=\"post\" action=\"search.aspx\" target=\"_blank\" onsubmit=\"bind_keyword(this);\">\r\n");
	templateBuilder.Append("				<input type=\"hidden\" name=\"poster\" />\r\n");
	templateBuilder.Append("				<input type=\"hidden\" name=\"keyword\" />\r\n");
	templateBuilder.Append("				<input type=\"hidden\" name=\"type\" value=\"\" />\r\n");
	templateBuilder.Append("				<input id=\"keywordtype\" type=\"hidden\" name=\"keywordtype\" value=\"0\"/>\r\n");
	templateBuilder.Append("				<div id=\"searchbar\">\r\n");
	templateBuilder.Append("					<dl>\r\n");
	templateBuilder.Append("						<dt id=\"quicksearch\" class=\"s2\" onclick=\"showMenu(this.id, false);\" onmouseover=\"MouseCursor(this);\">帖子标题</dt>\r\n");
	templateBuilder.Append("						<dd class=\"textinput\"><input type=\"text\" name=\"keywordf\" value=\"\" class=\"text\"/></dd>\r\n");
	templateBuilder.Append("						<dd><input name=\"searchsubmit\" type=\"submit\" value=\"\" class=\"s3\"/></dd>\r\n");
	templateBuilder.Append("					</dl>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("			</form>\r\n");
	templateBuilder.Append("			<script type=\"text/javascript\">function bind_keyword(form){if(form.keywordtype.value=='8'){form.keyword.value='';form.poster.value=form.keywordf.value; } else { form.poster.value=''; form.keyword.value=form.keywordf.value;if(form.keywordtype.value == '2')form.type.value = 'spacepost';if(form.keywordtype.value == '3')form.type.value = 'album';}}</" + "script>\r\n");



	}	//end if

	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (config.Forumjump==1)
	{

	templateBuilder.Append("	" + navhomemenu.ToString() + "\r\n");

	}	//end if

	templateBuilder.Append("<div class=\"mainbox viewthread specialthread specialthread_5\">\r\n");
	templateBuilder.Append("	<h3>\r\n");

	if (forum.Applytopictype==1 && forum.Topictypeprefix==1)
	{

	templateBuilder.Append("		" + topictypes.ToString() + " \r\n");

	}	//end if

	templateBuilder.Append("辩论主题\r\n");
	templateBuilder.Append("	</h3>\r\n");
	templateBuilder.Append("	<table cellspacing=\"0\" cellpadding=\"0\" summary=\"辩论主题\">\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("		<td class=\"postcontent\">\r\n");
	templateBuilder.Append("			<h1>" + debatepost.Title.ToString().Trim() + " </h1>\r\n");
	templateBuilder.Append("			<div class=\"postmessage\">\r\n");
	templateBuilder.Append("				<div id=\"firstpost\">\r\n");
	templateBuilder.Append("					" + debatepost.Message.ToString().Trim() + "\r\n");
	templateBuilder.Append("				</div>\r\n");

	if (enabletag)
	{

	templateBuilder.Append("				<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("					function forumhottag_callback(data)\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						tags = data;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				</" + "script>\r\n");
	templateBuilder.Append("				<script type=\"text/javascript\" src=\"cache/hottags_forum_cache_jsonp.txt\"></" + "script>\r\n");
	templateBuilder.Append("				<div id=\"topictag\">\r\n");
	int hastag = Topics.GetMagicValue(topic.Magic, MagicType.TopicTag);
	

	if (hastag==1)
	{

	templateBuilder.Append("						<script type=\"text/javascript\">getTopicTags(" + topic.Tid.ToString().Trim() + ");</" + "script>\r\n");

	}
	else
	{

	templateBuilder.Append("						<script type=\"text/javascript\">parsetag();</" + "script>\r\n");

	}	//end if

	templateBuilder.Append("				</div>\r\n");

	}	//end if

	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("		<td class=\"postauthor\">\r\n");

	if (debatepost.Posterid!=-1)
	{

	templateBuilder.Append("				<cite>\r\n");
	templateBuilder.Append("					<a href=\"#\" target=\"_blank\" id=\"memberinfo_topic\" class=\"dropmenu\"  onmouseover=\"showMenu(this.id,false)\">\r\n");

	if (debatepost.Onlinestate==1)
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/useronline.gif\" alt=\"在线\" title=\"在线\"/>\r\n");

	}
	else
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/useroutline.gif\"  alt=\"离线\" title=\"离线\"/>\r\n");

	}	//end if

	templateBuilder.Append("					<em>发起人:</em>" + debatepost.Poster.ToString().Trim() + "\r\n");
	templateBuilder.Append("					</a>\r\n");
	templateBuilder.Append("				</cite>\r\n");

	if (config.Showavatars==1)
	{

	templateBuilder.Append("				<div class=\"avatar\">\r\n");

	if (debatepost.Avatar!="")
	{

	templateBuilder.Append("					<img src=\"" + debatepost.Avatar.ToString().Trim() + "\" onerror=\"this.onerror=null;this.src='templates/" + templatepath.ToString() + "/images/noavatar.gif';\" \r\n");

	if (debatepost.Avatarwidth>0)
	{

	templateBuilder.Append(" width=\"" + debatepost.Avatarwidth.ToString().Trim() + "\" height=\"" + debatepost.Avatarheight.ToString().Trim() + "\" \r\n");

	}	//end if

	templateBuilder.Append(" alt=\"头像\"/>			\r\n");

	}	//end if

	templateBuilder.Append("				</div>\r\n");

	}	//end if


	}
	else
	{

	templateBuilder.Append("				<div class=\"ipshow\"><strong>" + debatepost.Poster.ToString().Trim() + "</strong>  " + debatepost.Ip.ToString().Trim() + "\r\n");

	if (useradminid>0 && admininfo.Allowviewip==1)
	{

	templateBuilder.Append("						<a href=\"getip.aspx?pid=" + debatepost.Pid.ToString().Trim() + "&topicid=" + topicid.ToString() + "\" title=\"查看IP\"><img src=\"templates/" + templatepath.ToString() + "/images/ip.gif\" alt=\"查看IP\"/></a>\r\n");

	}	//end if

	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				<!--guest-->\r\n");
	templateBuilder.Append("				<div class=\"noregediter\">\r\n");
	templateBuilder.Append("					未注册\r\n");
	templateBuilder.Append("				</div>\r\n");

	}	//end if

	templateBuilder.Append("			<p>开始时间&nbsp; \r\n");
	templateBuilder.Append(Convert.ToDateTime(debatepost.Postdatetime).ToString(" yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("			<p>结束时间&nbsp;\r\n");
	templateBuilder.Append(Convert.ToDateTime(debateexpand.Terminaltime).ToString(" yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("		<td class=\"postcontent\">\r\n");
	templateBuilder.Append("		<div class=\"postactions\">\r\n");

	if (userid!=-1)
	{


	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("    show_report_button();\r\n");
	templateBuilder.Append("</" + "script>\r\n");


	templateBuilder.Append("|\r\n");

	}	//end if

	templateBuilder.Append("			<a href=\"favorites.aspx?topicid=" + topicid.ToString() + "\">收藏</a>|\r\n");

	if (ismoder==1)
	{

	templateBuilder.Append("				<a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + debatepost.Pid.ToString().Trim() + "\">编辑</a>|\r\n");
	templateBuilder.Append("				<a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + debatepost.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>|\r\n");

	if (debatepost.Posterid!=-1)
	{

	templateBuilder.Append("					<a href=\"###\" onclick=\"action_onchange('rate',$('moderate'),'" + debatepost.Pid.ToString().Trim() + "');\">评分</a>\r\n");

	if (debatepost.Ratetimes>0)
	{

	templateBuilder.Append("					<a href=\"###\" onclick=\"action_onchange('cancelrate',$('moderate'),'" + debatepost.Pid.ToString().Trim() + "');\">撤销评分</a>|\r\n");

	}	//end if


	}	//end if


	if (debatepost.Layer==0 && topic.Special==4)
	{


	if (isenddebate==true  && userid==debatepost.Posterid)
	{

	templateBuilder.Append("|<a href=\"###\" onClick=\"showMenu(this.id)\" id=\"commentdebates\" name=\"commentdebates\">点评</a>\r\n");

	}	//end if


	}	//end if


	}
	else
	{


	if (debatepost.Posterid!=-1 && userid==debatepost.Posterid)
	{


	if (topic.Closed==0)
	{

	templateBuilder.Append("						<a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + debatepost.Pid.ToString().Trim() + "\">编辑</a>|\r\n");

	}	//end if

	templateBuilder.Append("					<a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + debatepost.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>|\r\n");

	}	//end if


	if (usergroupinfo.Raterange!="" && debatepost.Posterid!=-1)
	{

	templateBuilder.Append("<a href=\"###\" onclick=\"action_onchange('rate',$('moderate'),'" + debatepost.Pid.ToString().Trim() + "');\">评分</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("		<td class=\"postauthor\">&nbsp;</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div id=\"commentdebates_menu\" style=\"display: none; width:270px;\" class=\"popupmenu_popup\">\r\n");
	templateBuilder.Append("	<form id=\"commentform\" >\r\n");
	templateBuilder.Append("		<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n");
	templateBuilder.Append("		  <tr>\r\n");
	templateBuilder.Append("   		 <td><textarea name=\"commentdabetas\" cols=\"43\" rows=\"6\" id=\"commentdabetas\"></textarea></td>\r\n");
	templateBuilder.Append("		  </tr>                                                      \r\n");
	templateBuilder.Append("		  <tr>\r\n");
	templateBuilder.Append("			<td><input type=\"button\" value=\"提交\"  onclick=\"commentdebates(" + topic.Tid.ToString().Trim() + ",'firstpost')\"/></td>\r\n");
	templateBuilder.Append("		  </tr>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("	</form>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (debatepost.Posterid!=-1)
	{

	templateBuilder.Append("	<!-- member menu -->\r\n");
	templateBuilder.Append("	<div class=\"popupmenu_popup userinfopanel\" id=\"memberinfo_topic_menu\" style=\"display: none; z-index: 50; filter: progid:dximagetransform.microsoft.shadow(direction=135,color=#cccccc,strength=2); left: 19px; clip: rect(auto auto auto auto); position absolute; top: 253px; width:150px;\" initialized ctrlkey=\"userinfo2\" h=\"209\">\r\n");
	templateBuilder.Append("		<p class=\"recivemessage\"><a href=\"usercppostpm.aspx?msgtoid=" + debatepost.Posterid.ToString().Trim() + "\" target=\"_blank\">发送短消息</a></p>\r\n");

	if (useradminid>0)
	{


	if (admininfo.Allowviewip==1)
	{

	templateBuilder.Append("		<p class=\"seeip\"><a href=\"getip.aspx?pid=" + debatepost.Pid.ToString().Trim() + "&topicid=" + topicid.ToString() + "\" title=\"查看IP\">查看IP</a></p>\r\n");

	}	//end if


	if (admininfo.Allowbanuser==1)
	{

	templateBuilder.Append("		<p><a href=\"useradmin.aspx?action=banuser&uid=" + debatepost.Posterid.ToString().Trim() + "\" title=\"禁止用户\">禁止用户</a></p>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("		<p>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(debatepost.Posterid);
	
	templateBuilder.Append("			<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">查看公共资料</a>\r\n");
	templateBuilder.Append("		</p>\r\n");

	if (debatepost.Nickname!="")
	{

	templateBuilder.Append("		<p>昵称<em>:" + debatepost.Nickname.ToString().Trim() + "</em></p>\r\n");

	}	//end if

	templateBuilder.Append("		<p>\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			ShowStars(" + debatepost.Stars.ToString().Trim() + ", " + config.Starthreshold.ToString().Trim() + ");\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	templateBuilder.Append("		</p>\r\n");

	if (debatepost.Medals!="")
	{

	templateBuilder.Append("		<div class=\"medals\">" + debatepost.Medals.ToString().Trim() + "</div>\r\n");

	}	//end if

	templateBuilder.Append("		<ul>\r\n");

	if (config.Userstatusby==1)
	{

	templateBuilder.Append("			<li>组别:" + debatepost.Status.ToString().Trim() + "</li>\r\n");

	}	//end if

	templateBuilder.Append("			<li>性别:<script type=\"text/javascript\">document.write(displayGender(" + debatepost.Gender.ToString().Trim() + "));</" + "script></span></li>\r\n");

	if (debatepost.Bday!="")
	{

	templateBuilder.Append("			<li>生日:" + debatepost.Bday.ToString().Trim() + "</li>\r\n");

	}	//end if

	templateBuilder.Append("			<li>来自:" + debatepost.Location.ToString().Trim() + "</li>\r\n");
	templateBuilder.Append("			<li>积分:" + debatepost.Credits.ToString().Trim() + "</li>\r\n");
	templateBuilder.Append("			<li>帖子:" + debatepost.Posts.ToString().Trim() + "</li>\r\n");
	templateBuilder.Append("			<li>注册:\r\n");

	if (debatepost.Joindate!="")
	{

	templateBuilder.Append(Convert.ToDateTime(debatepost.Joindate).ToString("yyyy-MM-dd"));

	}	//end if

	templateBuilder.Append("</li>\r\n");
	templateBuilder.Append("			<li>UID:" + debatepost.Posterid.ToString().Trim() + "</li>\r\n");
	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		<p>状态:\r\n");

	if (debatepost.Onlinestate==1)
	{

	templateBuilder.Append("在线\r\n");

	}
	else
	{

	templateBuilder.Append("离线\r\n");

	}	//end if

	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("		<ul class=\"tools\">\r\n");

	if (debatepost.Msn!="")
	{

	templateBuilder.Append("			<li>\r\n");
	templateBuilder.Append("				<img src=\"templates/" + templatepath.ToString() + "/images/msnchat.gif\" alt=\"MSN Messenger: " + debatepost.Msn.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("				<a href=\"mailto:" + debatepost.Msn.ToString().Trim() + "\" target=\"_blank\">" + debatepost.Msn.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("			</li>\r\n");

	}	//end if


	if (debatepost.Skype!="")
	{

	templateBuilder.Append("			<li>\r\n");
	templateBuilder.Append("				<img src=\"templates/" + templatepath.ToString() + "/images/skype.gif\" alt=\"Skype: " + debatepost.Skype.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("				<a href=\"skype:" + debatepost.Skype.ToString().Trim() + "\" target=\"_blank\">" + debatepost.Skype.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("			</li>\r\n");

	}	//end if


	if (debatepost.Icq!="")
	{

	templateBuilder.Append("			<li>\r\n");
	templateBuilder.Append("				<img src=\"templates/" + templatepath.ToString() + "/images/icq.gif\" alt=\"ICQ: " + debatepost.Icq.ToString().Trim() + "\" />\r\n");
	templateBuilder.Append("				<a href=\"http://wwp.icq.com/scripts/search.dll?to=" + debatepost.Icq.ToString().Trim() + "\" target=\"_blank\">" + debatepost.Icq.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("			</li>\r\n");

	}	//end if


	if (debatepost.Qq!="")
	{

	templateBuilder.Append("			<li>\r\n");
	templateBuilder.Append("				<img src=\"templates/" + templatepath.ToString() + "/images/qq.gif\" alt=\"QQ: " + debatepost.Qq.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("				<a href=\"http://wpa.qq.com/msgrd?V=1&Uin=" + debatepost.Qq.ToString().Trim() + "&Site=" + config.Forumtitle.ToString().Trim() + "&Menu=yes\" target=\"_blank\">" + debatepost.Qq.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("			</li>\r\n");

	}	//end if


	if (debatepost.Yahoo!="")
	{

	templateBuilder.Append("			<li>\r\n");
	templateBuilder.Append("				<img src=\"templates/" + templatepath.ToString() + "/images/yahoo.gif\" width=\"16\" alt=\"Yahoo Messenger: " + debatepost.Yahoo.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("				<a href=\"http://edit.yahoo.com/config/send_webmesg?.target=" + debatepost.Yahoo.ToString().Trim() + "&.src=pg\" target=\"_blank\">" + debatepost.Yahoo.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("			</li>\r\n");

	}	//end if

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<!-- member menu -->\r\n");

	}	//end if

	templateBuilder.Append("<div id=\"ajaxdebateposts\">\r\n");
	templateBuilder.Append("<div class=\"box specialpostcontainer\">\r\n");
	templateBuilder.Append("	<ul class=\"tabs\">\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	
	templateBuilder.Append("		 <li class=\"current\" style=\"padding:0 8px;\">辩论详情</li><li><a href=\"" + aspxrewriteurl.ToString() + "\">普通模式</a></li>\r\n");
	templateBuilder.Append("	</ul>\r\n");
	templateBuilder.Append("	<div class=\"talkbox\">\r\n");
	templateBuilder.Append("		<div class=\"specialtitle\">\r\n");
	templateBuilder.Append("			<div class=\"squaretitle\">\r\n");
	templateBuilder.Append("				<p>正方观点</p>\r\n");
	templateBuilder.Append("				" + debateexpand.Positiveopinion.ToString().Trim() + "\r\n");
	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("			<div class=\"sidetitle\">\r\n");
	templateBuilder.Append("				<p>反方观点</p>\r\n");
	templateBuilder.Append("				" + debateexpand.Negativeopinion.ToString().Trim() + "\r\n");
	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<div class=\"balance\">\r\n");
	templateBuilder.Append("			<span class=\"scalevalue1\"><b id=\"positivediggs\">" + debateexpand.Positivediggs.ToString().Trim() + "</b></span>\r\n");
	templateBuilder.Append("			<span class=\"scalevalue\"><b id=\"negativediggs\">" + debateexpand.Negativediggs.ToString().Trim() + "</b></span>\r\n");
	templateBuilder.Append("			<div id=\"positivepercent\" class=\"squareboll\" style=\"width:" + positivepercent.ToString() + "%;\"></div>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<div class=\"talkinner\">\r\n");
	templateBuilder.Append("			<div class=\"squarebox\">\r\n");
	templateBuilder.Append("				<div class=\"buttoncontrol\"><button onclick=\"$('positivepostform').style.display='';this.style.display='none';\">加入正方</button></div>\r\n");
	templateBuilder.Append("				<div id=\"positivepostform\" style=\"display: none;\">\r\n");
	templateBuilder.Append("					<form method=\"post\" name=\"postform\" id=\"postform\" action=\"postreply.aspx?topicid=" + topicid.ToString() + "\"	enctype=\"multipart/form-data\" onsubmit=\"return validate(this);\" >\r\n");
	templateBuilder.Append("						<input type=\"hidden\" id=\"title\" name=\"title\" size=\"84\" tabindex=\"1\" value=\"\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" id=\"postid\" name=\"postid\" value=\"-1\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"debateopinion\" value=\"1\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"parseurloff\" value=\"" + parseurloff.ToString() + "\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"smileyoff\" value=\"" + smileyoff.ToString() + "\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"bbcodeoff\" value=\"" + bbcodeoff.ToString() + "\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"usesig\" value=\"0\" />\r\n");
	templateBuilder.Append("						<table cellspacing=\"0\" cellpadding=\"0\" summary=\"正方观点\">\r\n");
	templateBuilder.Append("							<tr><td>我的意见：</td></tr>\r\n");
	templateBuilder.Append("							<tr>\r\n");
	templateBuilder.Append("								<td>\r\n");
	templateBuilder.Append("									<textarea name=\"message\" cols=\"6\" rows=\"4\" class=\"autosave\" id=\"message\" tabindex=\"2\" onkeydown=\"ctlent(event, this.form);\"></textarea>\r\n");
	templateBuilder.Append("								</td>\r\n");
	templateBuilder.Append("							</tr>\r\n");
	templateBuilder.Append("							<tr>\r\n");
	templateBuilder.Append("								<td>\r\n");
	templateBuilder.Append("									<input type=\"submit\" name=\"replysubmit\" value=\"我要发表\" class=\"submitbutton\"/>\r\n");
	templateBuilder.Append("								</td>\r\n");
	templateBuilder.Append("							</tr>\r\n");
	templateBuilder.Append("						</table>\r\n");
	templateBuilder.Append("					</form>\r\n");
	templateBuilder.Append("				</div>\r\n");

	if (positivepostlist.Count>0)
	{

	templateBuilder.Append("					<div id=\"positive_pagenumbers_top\" class=\"debatepages\">" + positivepagenumbers.ToString() + "</div>\r\n");
	templateBuilder.Append("					<div id=\"positivepage_owner\">\r\n");

	int positivepost__loop__id=0;
	foreach(ShowtopicPagePostInfo positivepost in positivepostlist)
	{
		positivepost__loop__id++;

	templateBuilder.Append("							<div class=\"square\">\r\n");
	templateBuilder.Append("								<table cellspacing=\"0\" cellpadding=\"0\" summary=\"正方观点\">\r\n");
	templateBuilder.Append("								<tbody>\r\n");
	templateBuilder.Append("								<tr>\r\n");
	templateBuilder.Append("								<td class=\"supportbox\">\r\n");
	templateBuilder.Append("									<p style=\"background:#FFF;\">\r\n");
	templateBuilder.Append("										<span style=\"padding:4px;\">支持度</span>\r\n");
	templateBuilder.Append("										<span class=\"talknum\" id=\"diggs" + positivepost.Pid.ToString().Trim() + "\">" + positivepost.Diggs.ToString().Trim() + "</span>\r\n");

	if (!isenddebate  && positivepost.Posterid!=userid)
	{


	if (!positivepost.Digged)
	{

	templateBuilder.Append("										<span class=\"cliktalk\" id=\"cliktalk" + positivepost.Pid.ToString().Trim() + "\"><a href=\"javascript:void(0);\" onclick=\"digg(" + positivepost.Pid.ToString().Trim() + "," + topic.Tid.ToString().Trim() + ",1)\">支持</a></span>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("									</p>\r\n");
	templateBuilder.Append("								</td>\r\n");
	templateBuilder.Append("								<td class=\"comment\">\r\n");
	templateBuilder.Append("									<h3><span>时间:\r\n");
	templateBuilder.Append(Convert.ToDateTime(positivepost.Postdatetime).ToString("yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</span>发表者:<a id=\"poster" + positivepost.Pid.ToString().Trim() + "\" href=\"" + UserInfoAspxRewrite(positivepost.Posterid).ToString().Trim() + "\">" + positivepost.Poster.ToString().Trim() + "</a>\r\n");

	if (ismoder==1)
	{

	templateBuilder.Append("												<a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + positivepost.Pid.ToString().Trim() + "&debate=1\">编辑</a>|\r\n");
	templateBuilder.Append("												<a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + positivepost.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}
	else
	{


	if (positivepost.Posterid!=-1 && userid==positivepost.Posterid)
	{

	templateBuilder.Append("												<a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + positivepost.Pid.ToString().Trim() + "&debate=1\">编辑</a>|\r\n");
	templateBuilder.Append("												<a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + positivepost.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("									</h3>\r\n");
	templateBuilder.Append("									<div class=\"debatemessage\"  id=\"message" + positivepost.Pid.ToString().Trim() + "\">\r\n");
	templateBuilder.Append("									" + positivepost.Message.ToString().Trim() + "\r\n");
	templateBuilder.Append("									</div>\r\n");

	if (!isenddebate  && positivepost.Posterid!=userid)
	{

	templateBuilder.Append("									<p class=\"othertalk\"><a id=\"reply_btn_" + positivepost.Pid.ToString().Trim() + "\" href=\"###\" onclick=\"showDebatReplyBox(" + topic.Tid.ToString().Trim() + ", " + positivepost.Pid.ToString().Trim() + ", 2, " + parseurloff.ToString() + ", " + smileyoff.ToString() + ", " + bbcodeoff.ToString() + ");this.style.display='none';\">我不同意</a><div id=\"reply_box_owner_" + positivepost.Pid.ToString().Trim() + "\"></div>\r\n");
	templateBuilder.Append("									</p>\r\n");

	}	//end if

	templateBuilder.Append("								</td>\r\n");
	templateBuilder.Append("								</tr>\r\n");
	templateBuilder.Append("								</tbody>\r\n");
	templateBuilder.Append("								</table>\r\n");
	templateBuilder.Append("							</div>\r\n");

	}	//end loop

	templateBuilder.Append("					</div>\r\n");
	templateBuilder.Append("					<div id=\"positive_pagenumbers_buttom\" class=\"debatepages\">" + positivepagenumbers.ToString() + "</div>\r\n");
	templateBuilder.Append("					<div class=\"buttoncontrol\"><button onclick=\"$('positivepostform2').innerHTML=$('positivepostform').innerHTML;$('positivepostform2').style.display='';this.style.display='none';\">加入正方</button></div>\r\n");
	templateBuilder.Append("					<div id=\"positivepostform2\" style=\"display:none;\"></div>\r\n");

	}	//end if

	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("			<div class=\"oppositionbox\">\r\n");
	templateBuilder.Append("				<div class=\"buttoncontrol\"><button onclick=\"$('negativepostform').style.display='';this.style.display='none';\">加入反方</button></div>\r\n");
	templateBuilder.Append("				<div id=\"negativepostform\" style=\"display: none;\" >\r\n");
	templateBuilder.Append("					<form method=\"post\" name=\"postform\" id=\"postform\" action=\"postreply.aspx?topicid=" + topicid.ToString() + "\"	enctype=\"multipart/form-data\" onsubmit=\"return validate(this);\" >\r\n");
	templateBuilder.Append("						<input type=\"hidden\" id=\"title\" name=\"title\" size=\"84\" tabindex=\"1\" value=\"\"/>\r\n");
	templateBuilder.Append("						<input type=\"hidden\" id=\"postid\" name=\"postid\" value=\"-1\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"debateopinion\" value=\"2\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"parseurloff\" value=\"" + parseurloff.ToString() + "\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"smileyoff\" value=\"" + smileyoff.ToString() + "\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"bbcodeoff\" value=\"" + bbcodeoff.ToString() + "\" />\r\n");
	templateBuilder.Append("						<input type=\"hidden\" name=\"usesig\" value=\"0\" />\r\n");
	templateBuilder.Append("						<table cellspacing=\"0\" cellpadding=\"0\" summary=\"反方观点\">\r\n");
	templateBuilder.Append("							<tr>\r\n");
	templateBuilder.Append("								<td>我的意见：</td>\r\n");
	templateBuilder.Append("							</tr>\r\n");
	templateBuilder.Append("							<tr>\r\n");
	templateBuilder.Append("								<td>\r\n");
	templateBuilder.Append("									<textarea name=\"message\" cols=\"6\" rows=\"4\" class=\"autosave\" id=\"message\" tabindex=\"2\" onkeydown=\"ctlent(event, this.form);\"></textarea>\r\n");
	templateBuilder.Append("								</td>\r\n");
	templateBuilder.Append("							</tr>\r\n");
	templateBuilder.Append("							<tr>\r\n");
	templateBuilder.Append("								<td>\r\n");
	templateBuilder.Append("									<input type=\"submit\" name=\"replysubmit\" value=\"我要发表\" class=\"submitbutton\"/>\r\n");
	templateBuilder.Append("								</td>\r\n");
	templateBuilder.Append("							</tr>\r\n");
	templateBuilder.Append("						</table>\r\n");
	templateBuilder.Append("					</form>\r\n");
	templateBuilder.Append("				</div>\r\n");

	if (negativepostlist.Count>0)
	{

	templateBuilder.Append("					<div id=\"negative_pagenumbers_top\" class=\"debatepages\">" + negativepagenumbers.ToString() + "</div>\r\n");
	templateBuilder.Append("					<div id=\"negativepage_owner\">\r\n");

	int negativepost__loop__id=0;
	foreach(ShowtopicPagePostInfo negativepost in negativepostlist)
	{
		negativepost__loop__id++;

	templateBuilder.Append("							<div class=\"square\">\r\n");
	templateBuilder.Append("								<table cellspacing=\"0\" cellpadding=\"0\" summary=\"反方观点\">\r\n");
	templateBuilder.Append("								<tbody>\r\n");
	templateBuilder.Append("								<tr>\r\n");
	templateBuilder.Append("								<td class=\"supportbox\">\r\n");
	templateBuilder.Append("									<p style=\"background:#FFF;\">\r\n");
	templateBuilder.Append("										<span style=\"padding:4px;\">支持度</span>\r\n");
	templateBuilder.Append("										<span class=\"talknum\" id=\"diggs" + negativepost.Pid.ToString().Trim() + "\">" + negativepost.Diggs.ToString().Trim() + "</span>\r\n");

	if (!isenddebate && negativepost.Posterid!=userid)
	{


	if (!negativepost.Digged)
	{

	templateBuilder.Append("										<span class=\"cliktalk\" id=\"cliktalk" + negativepost.Pid.ToString().Trim() + "\"><a href=\"javascript:void(0);\" onclick=\"digg(" + negativepost.Pid.ToString().Trim() + "," + topic.Tid.ToString().Trim() + ",2)\">支持</a></span>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("									</p>\r\n");
	templateBuilder.Append("								</td>\r\n");
	templateBuilder.Append("								<td class=\"comment\">\r\n");
	templateBuilder.Append("									<h3><span>时间:\r\n");
	templateBuilder.Append(Convert.ToDateTime(negativepost.Postdatetime).ToString("yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</span>发表者:<a id=\"poster" + negativepost.Pid.ToString().Trim() + "\" href=\"" + UserInfoAspxRewrite(negativepost.Posterid).ToString().Trim() + "\">" + negativepost.Poster.ToString().Trim() + "</a>\r\n");

	if (ismoder==1)
	{

	templateBuilder.Append("												<a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + negativepost.Pid.ToString().Trim() + "&debate=1\">编辑</a>|\r\n");
	templateBuilder.Append("												<a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + negativepost.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}
	else
	{


	if (negativepost.Posterid!=-1 && userid==negativepost.Posterid)
	{

	templateBuilder.Append("												<a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + negativepost.Pid.ToString().Trim() + "&debate=1\">编辑</a>|\r\n");
	templateBuilder.Append("												<a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + negativepost.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("									</h3>\r\n");
	templateBuilder.Append("									<div class=\"debatemessage\" id=\"message" + negativepost.Pid.ToString().Trim() + "\">\r\n");
	templateBuilder.Append("									" + negativepost.Message.ToString().Trim() + "\r\n");
	templateBuilder.Append("									</div>\r\n");

	if (!isenddebate  && negativepost.Posterid!=userid)
	{

	templateBuilder.Append("									<p class=\"othertalk\"><a href=\"###\" id=\"reply_btn_" + negativepost.Pid.ToString().Trim() + "\" onclick=\"showDebatReplyBox(" + topic.Tid.ToString().Trim() + ", " + negativepost.Pid.ToString().Trim() + ", 1, " + parseurloff.ToString() + ", " + smileyoff.ToString() + ", " + bbcodeoff.ToString() + ");this.style.display='none';\">我不同意</a><div id=\"reply_box_owner_" + negativepost.Pid.ToString().Trim() + "\"></div>\r\n");
	templateBuilder.Append("									</p>\r\n");

	}	//end if

	templateBuilder.Append("								</td>\r\n");
	templateBuilder.Append("								</tr>\r\n");
	templateBuilder.Append("								</tbody>\r\n");
	templateBuilder.Append("								</table>\r\n");
	templateBuilder.Append("							</div>\r\n");

	}	//end loop

	templateBuilder.Append("					</div>\r\n");
	templateBuilder.Append("					<div id=\"negative_pagenumbers_buttom\" class=\"debatepages\">" + negativepagenumbers.ToString() + "</div>\r\n");
	templateBuilder.Append("					<div class=\"buttoncontrol\"><button onclick=\"$('negativepostform2').innerHTML=$('negativepostform').innerHTML;$('negativepostform2').style.display='';this.style.display='none';\">加入反方</button></div>\r\n");
	templateBuilder.Append("					<div id=\"negativepostform2\" style=\"display:none;\"></div>\r\n");

	}	//end if

	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}
	else
	{


	templateBuilder.Append("<div class=\"box message\">\r\n");
	templateBuilder.Append("	<h1>出现了" + page_err.ToString() + "个错误</h1>\r\n");
	templateBuilder.Append("	<p>" + msgbox_text.ToString() + "</p>\r\n");
	templateBuilder.Append("	<p class=\"errorback\">\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			if(" + msgbox_showbacklink.ToString() + ")\r\n");
	templateBuilder.Append("			{\r\n");
	templateBuilder.Append("				document.write(\"<a href=\\\"" + msgbox_backlink.ToString() + "\\\">返回上一步</a> &nbsp; &nbsp;|&nbsp; &nbsp  \");\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	templateBuilder.Append("		<a href=\"forumindex.aspx\">论坛首页</a>\r\n");

	if (usergroupid==7)
	{

	templateBuilder.Append("		 &nbsp; &nbsp|&nbsp; &nbsp; <a href=\"register.aspx\">注册</a>\r\n");

	}	//end if

	templateBuilder.Append("	</p>\r\n");
	templateBuilder.Append("</div>\r\n");



	}	//end if

	templateBuilder.Append("</div>\r\n");


	if (floatad!="")
	{

	templateBuilder.Append("	<script type=\"text/javascript\" src=\"javascript/template_floatadv.js\"></" + "script>\r\n");
	templateBuilder.Append("	" + floatad.ToString() + "\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">theFloaters.play();</" + "script>\r\n");

	}
	else if (doublead!="")
	{

	templateBuilder.Append("	<script type=\"text/javascript\" src=\"javascript/template_floatadv.js\"></" + "script>\r\n");
	templateBuilder.Append("	" + doublead.ToString() + "\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">theFloaters.play();</" + "script>\r\n");

	}	//end if





	if (footerad!="")
	{

	templateBuilder.Append("<!--底部广告显示-->\r\n");
	templateBuilder.Append("<div id=\"ad_footerbanner\">" + footerad.ToString() + "</div>\r\n");
	templateBuilder.Append("<!--底部广告结束-->\r\n");

	}	//end if

	templateBuilder.Append("<div id=\"footer\">\r\n");
	templateBuilder.Append("	<div class=\"wrap\">\r\n");
	templateBuilder.Append("		<div id=\"footlinks\">\r\n");
	templateBuilder.Append("			<p><a href=\"" + config.Weburl.ToString().Trim() + "\" target=\"_blank\">" + config.Webtitle.ToString().Trim() + "</a>&nbsp; " + config.Linktext.ToString().Trim() + "\r\n");

	if (config.Sitemapstatus==1)
	{

	templateBuilder.Append("&nbsp;<a href=\"tools/sitemap.aspx\" target=\"_blank\" title=\"百度论坛收录协议\">Sitemap</a>\r\n");

	}	//end if

	templateBuilder.Append("				" + config.Statcode.ToString().Trim() + "\r\n");
	templateBuilder.Append("			</p>\r\n");
	templateBuilder.Append("			<p>\r\n");
	templateBuilder.Append("			<a href=\"http://www.comsenz.com/\" target=\"_blank\">Comsenz Technology Ltd</a>\r\n");
	templateBuilder.Append("			- <a href=\"" + forumurl.ToString() + "archiver/index.aspx\" target=\"_blank\">简洁版本</a>\r\n");
	templateBuilder.Append("			- <span class=\"scrolltop\" onclick=\"window.scrollTo(0,0);\">TOP</span>\r\n");

	if (config.Stylejump==1)
	{


	if (userid!=-1 || config.Guestcachepagetimeout<=0)
	{

	templateBuilder.Append("			- <span id=\"styleswitcher\" class=\"dropmenu\" onmouseover=\"showMenu(this.id)\" onclick=\"window.location.href='" + forumurl.ToString() + "showtemplate.aspx'\">界面风格</span>\r\n");
	templateBuilder.Append("				<div id=\"styleswitcher_menu\" class=\"popupmenu_popup\" style=\"display: none;\">\r\n");
	templateBuilder.Append("					<ul>\r\n");
	templateBuilder.Append("						" + templatelistboxoptions.ToString() + "\r\n");
	templateBuilder.Append("					</ul>\r\n");
	templateBuilder.Append("				</div>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("			</p>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<a title=\"Powered by Discuz!NT\" target=\"_blank\" href=\"http://nt.discuz.net\"><img border=\"0\" alt=\"Discuz!\" src=\"templates/" + templatepath.ToString() + "/images/discuznt_logo.gif\"/></a>\r\n");
	templateBuilder.Append("		<p id=\"copyright\">\r\n");
	templateBuilder.Append("			Powered by <strong><a href=\"http://nt.discuz.net\" target=\"_blank\" title=\"Discuz!NT 2.5 (.net Framework 2.0)\">Discuz!NT</a></strong> <em>2.5.0</em>&nbsp;beta\r\n");

	if (config.Licensed==1)
	{

	templateBuilder.Append("				(<a href=\"\" onclick=\"this.href='http://nt.discuz.net/certificate/?host='+location.href.substring(0, location.href.lastIndexOf('/'))\" target=\"_blank\">Licensed</a>)\r\n");

	}	//end if

	templateBuilder.Append("				" + config.Forumcopyright.ToString().Trim() + "\r\n");
	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("		<p id=\"debuginfo\">\r\n");

	if (config.Debug!=0)
	{

	templateBuilder.Append("			Processed in " + this.Processtime.ToString().Trim() + " second(s)\r\n");

	if (isguestcachepage==1)
	{

	templateBuilder.Append("				(Cached).\r\n");

	}
	else if (querycount>1)
	{

	templateBuilder.Append("				 , " + querycount.ToString() + " queries.\r\n");

	}
	else
	{

	templateBuilder.Append("				        , " + querycount.ToString() + " query.\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("		" + config.Icp.ToString().Trim() + "\r\n");
	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<ul class=\"popupmenu_popup headermenu_popup\" id=\"stats_menu\" style=\"display: none\">\r\n");
	templateBuilder.Append("	<li><a href=\"stats.aspx\">基本状况</a></li>\r\n");

	if (config.Statstatus==1)
	{

	templateBuilder.Append("	<li><a href=\"stats.aspx?type=views\">流量统计</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"stats.aspx?type=client\">客户软件</a></li>\r\n");

	}	//end if

	templateBuilder.Append("	<li><a href=\"stats.aspx?type=posts\">发帖量记录</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"stats.aspx?type=forumsrank\">版块排行</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"stats.aspx?type=topicsrank\">主题排行</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"stats.aspx?type=postsrank\">发帖排行</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"stats.aspx?type=creditsrank\">积分排行</a></li>\r\n");

	if (config.Oltimespan>0)
	{

	templateBuilder.Append("	<li><a href=\"stats.aspx?type=onlinetime\">在线时间</a></li>\r\n");

	}	//end if

	templateBuilder.Append("</ul>\r\n");
	templateBuilder.Append("<ul class=\"popupmenu_popup headermenu_popup\" id=\"my_menu\" style=\"display: none\">\r\n");
	templateBuilder.Append("	<li><a href=\"mytopics.aspx\">我的主题</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"myposts.aspx\">我的帖子</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"search.aspx?posterid=" + userid.ToString() + "&amp;type=digest\">我的精华</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"myattachment.aspx\">我的附件</a></li>\r\n");

	if (config.Enablespace==1)
	{

	templateBuilder.Append("	<li><a href=\"" + spaceurl.ToString() + "space/\">我的空间</a></li>\r\n");

	}	//end if


	if (config.Enablealbum==1)
	{

	templateBuilder.Append("	<li><a href=\"showalbumlist.aspx?uid=" + userid.ToString() + "\">我的相册</a></li>\r\n");

	}	//end if


	if (config.Enablemall>=1)
	{

	templateBuilder.Append("	<li><a href=\"usercpmygoods.aspx\">我的商品</a></li>\r\n");

	}	//end if

	templateBuilder.Append("	<script type=\"text/javascript\" src=\"javascript/mymenu.js\"></" + "script>\r\n");
	templateBuilder.Append("</ul>\r\n");
	templateBuilder.Append("<ul class=\"popupmenu_popup\" id=\"viewpro_menu\" style=\"display: none\">\r\n");

	if (useravatar!="")
	{

	templateBuilder.Append("		<img src=\"" + useravatar.ToString() + "\"/>\r\n");

	}	//end if

	 aspxrewriteurl = this.UserInfoAspxRewrite(userid);
	
	templateBuilder.Append("	<li class=\"popuser\"><a href=\"" + aspxrewriteurl.ToString() + "\">我的资料</a></li>\r\n");

	if (config.Enablespace==1)
	{

	templateBuilder.Append("	 <li class=\"poplink\">\r\n");
	templateBuilder.Append("	<a href=\"" + spaceurl.ToString() + "space/\">我的空间</a>\r\n");
	templateBuilder.Append("	</li>\r\n");

	}	//end if

	templateBuilder.Append("</ul>\r\n");



	templateBuilder.Append("<div id=\"quicksearch_menu\" class=\"searchmenu\" style=\"display: none;\">\r\n");
	templateBuilder.Append("				<div onclick=\"document.getElementById('keywordtype').value='0';document.getElementById('quicksearch').innerHTML='帖子标题';document.getElementById('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">帖子标题</div>\r\n");
	templateBuilder.Append("				<div onclick=\"document.getElementById('keywordtype').value='2';document.getElementById('quicksearch').innerHTML='空间日志';document.getElementById('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">空间日志</div>\r\n");
	templateBuilder.Append("				<div onclick=\"document.getElementById('keywordtype').value='3';document.getElementById('quicksearch').innerHTML='相册标题';document.getElementById('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">相册标题</div>\r\n");
	templateBuilder.Append("				<div onclick=\"document.getElementById('keywordtype').value='8';document.getElementById('quicksearch').innerHTML='作&nbsp;&nbsp;者';document.getElementById('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">作&nbsp;&nbsp;者</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</body>\r\n");
	templateBuilder.Append("</html>\r\n");



	Response.Write(templateBuilder.ToString());
}
</script>
