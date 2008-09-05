<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.showbonus" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 14:21:56.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 14:21:56. 
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
	templateBuilder.Append("</" + "script>\r\n");

	if (enabletag)
	{

	templateBuilder.Append("<script type=\"text/javascript\" src=\"cache/tag/closedtags.txt\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"cache/tag/colorfultags.txt\"></" + "script>\r\n");

	}	//end if

	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_showtopic.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/ajax.js\"></" + "script>\r\n");
	templateBuilder.Append("<div id=\"foruminfo\">\r\n");
	templateBuilder.Append("	<div id=\"nav\">\r\n");
	templateBuilder.Append("		<div class=\"userinfolist\">\r\n");
	templateBuilder.Append("			<p><a id=\"forumlist\" href=\"" + config.Forumurl.ToString().Trim() + "\" \r\n");

	if (config.Forumjump==1)
	{

	templateBuilder.Append("			onmouseover=\"showMenu(this.id);\" onmouseout=\"showMenu(this.id);\"\r\n");

	}	//end if

	templateBuilder.Append("			>" + config.Forumtitle.ToString().Trim() + "</a>  &raquo; " + forumnav.ToString() + "\r\n");
	int ishtmltitle = Topics.GetMagicValue(topic.Magic, MagicType.HtmlTitle);
	

	if (ishtmltitle==1)
	{

	templateBuilder.Append("				  &raquo; <strong>" + Topics.GetHtmlTitle(topic.Tid).ToString().Trim() + "</strong>\r\n");

	}
	else
	{

	templateBuilder.Append("				  &raquo; <strong>" + topictitle.ToString() + "</strong>\r\n");

	}	//end if

	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("		</div>\r\n");
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


	if (page_err==0)
	{

	int loopi = 1;
	
	int valuablepostcount = 0;
	
	int valuelesspostcount = 0;
	

	int post__loop__id=0;
	foreach(ShowbonusPagePostInfo post in postlist)
	{
		post__loop__id++;


	if (post.Id!=1 && post.Isbest==1)
	{

	 valuablepostcount = valuablepostcount+1;
	

	}	//end if


	if (post.Id!=1 && post.Isbest==0)
	{

	 valuelesspostcount = valuelesspostcount+1;
	

	}	//end if


	if (post.Id==1)
	{

	templateBuilder.Append("		<div class=\"mainbox viewthread specialthread specialthread_5\">\r\n");
	templateBuilder.Append("			<h3>\r\n");

	if (forum.Applytopictype==1 && forum.Topictypeprefix==1)
	{

	templateBuilder.Append("				" + topictypes.ToString() + " \r\n");

	}	//end if

	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	
	templateBuilder.Append("				<a title=\"点击查看原始版本\" href=\"" + aspxrewriteurl.ToString() + "\">悬赏主题</a>\r\n");
	templateBuilder.Append("			</h3>\r\n");
	templateBuilder.Append("			<table cellspacing=\"0\" cellpadding=\"0\" summary=\"悬赏主题\">\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("				<td class=\"postcontent\">\r\n");
	templateBuilder.Append("					<label>[已解决 - <a href=\"#bestpost\">最佳答案</a>]\r\n");
	templateBuilder.Append("							悬赏价格: <strong>金钱 " + topic.Price.ToString().Trim() + " </strong>\r\n");
	templateBuilder.Append("					</label>\r\n");
	templateBuilder.Append("					<h1>" + post.Title.ToString().Trim() + " </h1>\r\n");
	templateBuilder.Append("					<div  class=\"postmessage\">\r\n");
	templateBuilder.Append("						<div id=\"firstpost\">\r\n");
	templateBuilder.Append("							<h4>补充资料</h4>\r\n");
	templateBuilder.Append("							" + post.Message.ToString().Trim() + "\r\n");
	templateBuilder.Append("						</div>\r\n");
	templateBuilder.Append("						<div class=\"quote\">\r\n");
	templateBuilder.Append("							<div class=\"text\"><p>本帖得分:</p>\r\n");
	templateBuilder.Append("								<div class=\"attachmentinfo\">\r\n");

	int bonuslog__loop__id=0;
	foreach(BonusLogInfo bonuslog in bonuslogs)
	{
		bonuslog__loop__id++;

	 aspxrewriteurl = this.UserInfoAspxRewrite(bonuslog.Answerid);
	
	string unit = scoreunit[ bonuslog.Extid ];
	
	string name = score[ bonuslog.Extid ];
	
	templateBuilder.Append("										<a href=\"" + aspxrewriteurl.ToString() + "\">" + bonuslog.Answername.ToString().Trim() + "</a>(" + bonuslog.Bonus.ToString().Trim() + " " + unit.ToString() + "" + name.ToString() + ")\r\n");

	if (bonuslog__loop__id!=bonuslogs.Count)
	{

	templateBuilder.Append("											,\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("								</div>\r\n");
	templateBuilder.Append("							</div>\r\n");
	templateBuilder.Append("						</div>\r\n");

	if (enabletag)
	{

	templateBuilder.Append("						<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("							function forumhottag_callback(data)\r\n");
	templateBuilder.Append("							{\r\n");
	templateBuilder.Append("								tags = data;\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("						</" + "script>\r\n");
	templateBuilder.Append("						<script type=\"text/javascript\" src=\"cache/hottags_forum_cache_jsonp.txt\"></" + "script>\r\n");
	templateBuilder.Append("						<div id=\"topictag\">\r\n");
	int hastag = Topics.GetMagicValue(topic.Magic, MagicType.TopicTag);
	

	if (hastag==1)
	{

	templateBuilder.Append("								<script type=\"text/javascript\">getTopicTags(" + topic.Tid.ToString().Trim() + ");</" + "script>\r\n");

	}
	else
	{

	templateBuilder.Append("								<script type=\"text/javascript\">parsetag();</" + "script>\r\n");

	}	//end if

	templateBuilder.Append("						</div>\r\n");

	}	//end if

	templateBuilder.Append("					</div>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td class=\"postauthor\">\r\n");

	if (post.Posterid!=-1)
	{

	templateBuilder.Append("					<cite>\r\n");
	templateBuilder.Append("						<a href=\"#\" target=\"_blank\" id=\"memberinfo_" + loopi.ToString() + "\" class=\"dropmenu\"  onmouseover=\"showMenu(this.id,false)\">\r\n");

	if (post.Onlinestate==1)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/useronline.gif\" alt=\"在线\" title=\"在线\"/>\r\n");

	}
	else
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/useroutline.gif\"  alt=\"离线\" title=\"离线\"/>\r\n");

	}	//end if

	templateBuilder.Append("						" + post.Poster.ToString().Trim() + "\r\n");
	templateBuilder.Append("						</a>\r\n");
	templateBuilder.Append("					</cite>\r\n");

	if (config.Showavatars==1)
	{

	templateBuilder.Append("					<div class=\"avatar\">\r\n");

	if (post.Avatar!="")
	{

	templateBuilder.Append("						<img src=\"" + post.Avatar.ToString().Trim() + "\" onerror=\"this.onerror=null;this.src='templates/" + templatepath.ToString() + "/images/noavatar.gif';\"  \r\n");

	if (post.Avatarwidth>0)
	{

	templateBuilder.Append(" width=\"" + post.Avatarwidth.ToString().Trim() + "\" height=\"" + post.Avatarheight.ToString().Trim() + "\" \r\n");

	}	//end if

	templateBuilder.Append(" alt=\"头像\" />			\r\n");

	}	//end if

	templateBuilder.Append("					</div>\r\n");

	}	//end if


	if (post.Nickname!="")
	{

	templateBuilder.Append("					<p><em>" + post.Nickname.ToString().Trim() + "</em></p>\r\n");

	}	//end if

	templateBuilder.Append("					<p>\r\n");
	templateBuilder.Append("						<script language=\"javascript\" type=\"text/javascript\">\r\n");
	templateBuilder.Append("							ShowStars(" + post.Stars.ToString().Trim() + ", " + config.Starthreshold.ToString().Trim() + ");\r\n");
	templateBuilder.Append("						</" + "script>\r\n");
	templateBuilder.Append("					</p>\r\n");

	if (config.Enablespace==1 || config.Enablealbum==1)
	{

	templateBuilder.Append("					<ul>\r\n");

	if (config.Enablespace==1)
	{

	templateBuilder.Append("						<li class=\"space\">\r\n");

	if (post.Spaceid>0)
	{

	templateBuilder.Append("<a href=\"" + spaceurl.ToString() + "space/?uid=" + post.Posterid.ToString().Trim() + "\">个人空间</a>\r\n");

	}
	else
	{

	templateBuilder.Append("<a href=\"###\" onclick=\"nospace('" + post.Poster.ToString().Trim() + "');\">个人空间</a>\r\n");

	}	//end if

	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (config.Enablealbum==1)
	{

	templateBuilder.Append("						<li class=\"albumpic\"><a href=\"showalbumlist.aspx?uid=" + post.Posterid.ToString().Trim() + "\">相册</a></li>\r\n");

	}	//end if

	templateBuilder.Append("					</ul>\r\n");

	}	//end if

	templateBuilder.Append("					<ul class=\"otherinfo\">\r\n");
	templateBuilder.Append("						<li>性别:<script type=\"text/javascript\">document.write(displayGender(" + post.Gender.ToString().Trim() + "));</" + "script></li>\r\n");

	if (post.Bday!="")
	{

	templateBuilder.Append("						<li>生日:" + post.Bday.ToString().Trim() + "</li>\r\n");

	}	//end if

	templateBuilder.Append("						<li>来自:" + post.Location.ToString().Trim() + "</li>\r\n");
	templateBuilder.Append("						<li>积分:" + post.Credits.ToString().Trim() + "</li>\r\n");
	templateBuilder.Append("						<li>帖子:" + post.Posts.ToString().Trim() + "</li>\r\n");
	templateBuilder.Append("						<li>注册:\r\n");

	if (post.Joindate!="")
	{

	templateBuilder.Append(Convert.ToDateTime(post.Joindate).ToString("yyyy-MM-dd"));

	}	//end if

	templateBuilder.Append("</li>					\r\n");
	templateBuilder.Append("					</ul>\r\n");

	if (post.Medals!="")
	{

	templateBuilder.Append("						<div class=\"medals\">" + post.Medals.ToString().Trim() + "</div>\r\n");

	}	//end if


	}
	else
	{

	templateBuilder.Append("						<div class=\"ipshow\"><strong>" + post.Poster.ToString().Trim() + "</strong>  " + post.Ip.ToString().Trim() + "\r\n");

	if (useradminid>0 && admininfo.Allowviewip==1)
	{

	templateBuilder.Append("								<a href=\"getip.aspx?pid=" + post.Pid.ToString().Trim() + "&topicid=" + topicid.ToString() + "\" title=\"查看IP\"><img src=\"templates/" + templatepath.ToString() + "/images/ip.gif\" alt=\"查看IP\"/></a>\r\n");

	}	//end if

	templateBuilder.Append("						</div>\r\n");
	templateBuilder.Append("						<div class=\"noregediter\">\r\n");
	templateBuilder.Append("							未注册\r\n");
	templateBuilder.Append("						</div>\r\n");

	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("				<td class=\"postcontent\">\r\n");
	templateBuilder.Append("					<div class=\"postactions\">\r\n");

	if (userid!=-1)
	{


	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("    show_report_button();\r\n");
	templateBuilder.Append("</" + "script>\r\n");


	templateBuilder.Append("|\r\n");

	}	//end if

	templateBuilder.Append("						<a href=\"favorites.aspx?topicid=" + topicid.ToString() + "\">收藏</a>|\r\n");

	if (ismoder==1)
	{

	templateBuilder.Append("							<a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "\">编辑</a>|\r\n");
	templateBuilder.Append("							<a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>|\r\n");

	if (post.Posterid!=-1)
	{

	templateBuilder.Append("								<a href=\"###\" onclick=\"action_onchange('rate',$('moderate'),'" + post.Pid.ToString().Trim() + "');\">评分</a>\r\n");

	if (post.Ratetimes>0)
	{

	templateBuilder.Append("								|<a href=\"###\" onclick=\"action_onchange('cancelrate',$('moderate'),'" + post.Pid.ToString().Trim() + "');\">撤销评分</a>&nbsp;\r\n");

	}	//end if


	}	//end if


	}
	else
	{


	if (post.Posterid!=-1 && userid==post.Posterid)
	{


	if (topic.Closed==0)
	{

	templateBuilder.Append("									<a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "\">编辑</a>|\r\n");

	}	//end if

	templateBuilder.Append("								<a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>|\r\n");

	}	//end if


	if (usergroupinfo.Raterange!="" && post.Posterid!=-1)
	{

	templateBuilder.Append("<a href=\"###\" onclick=\"action_onchange('rate',$('moderate'),'" + post.Pid.ToString().Trim() + "');\">评分</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("					</div>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td class=\"postauthor\">\r\n");
	templateBuilder.Append("					&nbsp;\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>	\r\n");

	}
	else if (post.Isbest==2)
	{

	templateBuilder.Append("		<div class=\"box othergoodsinfo\">\r\n");
	templateBuilder.Append("			<ul class=\"tabs\">\r\n");
	templateBuilder.Append("				 <li class=\"current\"><a name=\"bestpost\"></a>最佳答案</li>\r\n");
	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("			<div class=\"specialpost\">\r\n");
	templateBuilder.Append("				<div class=\"postinfo\">\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(post.Posterid);
	
	templateBuilder.Append("					<h2><a id=\"memberinfo_" + loopi.ToString() + "\" href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\" onmouseover=\"showMenu(this.id,false)\" class=\"dropmenu\">" + post.Poster.ToString().Trim() + "</a> <span>\r\n");
	templateBuilder.Append(Convert.ToDateTime(post.Postdatetime).ToString("yyyy-MM-dd hh:mm"));
	templateBuilder.Append("</span></h2>\r\n");
	templateBuilder.Append("					<cite>\r\n");

	if (ismoder==1)
	{

	templateBuilder.Append("					        <a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "&pageid=" + pageid.ToString() + "\">编辑</a>\r\n");
	templateBuilder.Append("					        <a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}
	else
	{


	if (post.Posterid!=-1 && userid==post.Posterid)
	{


	if (topic.Closed==0)
	{

	templateBuilder.Append("						            <a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "&pageid=" + pageid.ToString() + "\">编辑</a>\r\n");

	}	//end if

	templateBuilder.Append("					            <a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}	//end if


	}	//end if


	if (canreply)
	{

	templateBuilder.Append("							<a href=\"postreply.aspx?topicid=" + topicid.ToString() + "&postid=" + post.Pid.ToString().Trim() + "&quote=yes\">引用</a>\r\n");

	if (userid!=-1)
	{

	templateBuilder.Append("								<a href=\"###\" onclick=\"replyToFloor('" + post.Id.ToString().Trim() + "', '" + post.Poster.ToString().Trim() + "', '" + post.Pid.ToString().Trim() + "')\">回复</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("					</cite>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				<div class=\"postmessage\">\r\n");
	templateBuilder.Append("					<div class=\"t_msgfont\">" + post.Message.ToString().Trim() + "</div>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (post.Posterid!=-1)
	{

	templateBuilder.Append("		<!-- member menu -->\r\n");
	templateBuilder.Append("		<div class=\"popupmenu_popup userinfopanel\" id=\"memberinfo_" + loopi.ToString() + "_menu\" style=\"display: none; z-index: 50; filter: progid:dximagetransform.microsoft.shadow(direction=135,color=#cccccc,strength=2); left: 19px; clip: rect(auto auto auto auto); position absolute; top: 253px; width:150px;\" initialized ctrlkey=\"userinfo2\" h=\"209\">\r\n");
	templateBuilder.Append("				<p class=\"recivemessage\"><a href=\"usercppostpm.aspx?msgtoid=" + post.Posterid.ToString().Trim() + "\" target=\"_blank\">发送短消息</a></p>\r\n");

	if (useradminid>0)
	{


	if (admininfo.Allowviewip==1)
	{

	templateBuilder.Append("				<p  class=\"seeip\"><a href=\"getip.aspx?pid=" + post.Pid.ToString().Trim() + "&topicid=" + topicid.ToString() + "\" title=\"查看IP\">查看IP</a></p>\r\n");

	}	//end if


	if (admininfo.Allowbanuser==1)
	{

	templateBuilder.Append("				<p><a href=\"useradmin.aspx?action=banuser&uid=" + post.Posterid.ToString().Trim() + "\" title=\"禁止用户\">禁止用户</a></p>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("				<p>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(post.Posterid);
	
	templateBuilder.Append("				<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">查看公共资料</a></p>\r\n");
	templateBuilder.Append("				<p><a href=\"search.aspx?posterid=" + post.Posterid.ToString().Trim() + "\">查找该会员全部帖子</a></p>\r\n");
	templateBuilder.Append("				<ul>\r\n");
	templateBuilder.Append("				<li>UID:" + post.Posterid.ToString().Trim() + "</li>\r\n");
	templateBuilder.Append("				<li>精华:\r\n");

	if (post.Digestposts>0)
	{

	templateBuilder.Append("<a href=\"search.aspx?posterid=" + post.Posterid.ToString().Trim() + "&type=digest\">" + post.Digestposts.ToString().Trim() + "</a>\r\n");

	}
	else
	{

	templateBuilder.Append("" + post.Digestposts.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("</li>\r\n");

	if (score[1].ToString().Trim()!="")
	{

	templateBuilder.Append("					<li>" + score[1].ToString().Trim() + ":" + post.Extcredits1.ToString().Trim() + " " + scoreunit[1].ToString().Trim() + "</li>\r\n");

	}	//end if


	if (score[2].ToString().Trim()!="")
	{

	templateBuilder.Append("					<li>" + score[2].ToString().Trim() + ":" + post.Extcredits2.ToString().Trim() + " " + scoreunit[2].ToString().Trim() + "</li>\r\n");

	}	//end if


	if (score[3].ToString().Trim()!="")
	{

	templateBuilder.Append("					<li>" + score[3].ToString().Trim() + ":" + post.Extcredits3.ToString().Trim() + " " + scoreunit[3].ToString().Trim() + "</li>\r\n");

	}	//end if


	if (score[4].ToString().Trim()!="")
	{

	templateBuilder.Append("					<li>" + score[4].ToString().Trim() + ":" + post.Extcredits4.ToString().Trim() + " " + scoreunit[4].ToString().Trim() + "</li>\r\n");

	}	//end if


	if (score[5].ToString().Trim()!="")
	{

	templateBuilder.Append("					<li>" + score[5].ToString().Trim() + ":" + post.Extcredits5.ToString().Trim() + " " + scoreunit[5].ToString().Trim() + "</li>\r\n");

	}	//end if


	if (score[6].ToString().Trim()!="")
	{

	templateBuilder.Append("					<li>" + score[6].ToString().Trim() + ":" + post.Extcredits6.ToString().Trim() + " " + scoreunit[6].ToString().Trim() + "</li>\r\n");

	}	//end if


	if (score[7].ToString().Trim()!="")
	{

	templateBuilder.Append("					<li>" + score[7].ToString().Trim() + ":" + post.Extcredits7.ToString().Trim() + " " + scoreunit[7].ToString().Trim() + "</li>\r\n");

	}	//end if


	if (score[8].ToString().Trim()!="")
	{

	templateBuilder.Append("					<li>" + score[8].ToString().Trim() + ":" + post.Extcredits8.ToString().Trim() + " " + scoreunit[8].ToString().Trim() + "</li>\r\n");

	}	//end if

	templateBuilder.Append("				</ul>\r\n");
	templateBuilder.Append("				<p>状态:\r\n");

	if (post.Onlinestate==1)
	{

	templateBuilder.Append("在线\r\n");

	}
	else
	{

	templateBuilder.Append("离线\r\n");

	}	//end if

	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("				<ul class=\"tools\">\r\n");

	if (post.Msn!="")
	{

	templateBuilder.Append("					<li>\r\n");
	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/msnchat.gif\" alt=\"MSN Messenger: " + post.Msn.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("						<a href=\"mailto:" + post.Msn.ToString().Trim() + "\" target=\"_blank\">" + post.Msn.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("					</li>\r\n");

	}	//end if


	if (post.Skype!="")
	{

	templateBuilder.Append("					<li>\r\n");
	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/skype.gif\" alt=\"Skype: " + post.Skype.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("						<a href=\"skype:" + post.Skype.ToString().Trim() + "\" target=\"_blank\">" + post.Skype.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("					</li>\r\n");

	}	//end if


	if (post.Icq!="")
	{

	templateBuilder.Append("					<li>\r\n");
	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/icq.gif\" alt=\"ICQ: " + post.Icq.ToString().Trim() + "\" />\r\n");
	templateBuilder.Append("						<a href=\"http://wwp.icq.com/scripts/search.dll?to=" + post.Icq.ToString().Trim() + "\" target=\"_blank\">" + post.Icq.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("					</li>\r\n");

	}	//end if


	if (post.Qq!="")
	{

	templateBuilder.Append("					<li>\r\n");
	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/qq.gif\" alt=\"QQ: " + post.Qq.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("						<a href=\"http://wpa.qq.com/msgrd?V=1&Uin=" + post.Qq.ToString().Trim() + "&Site=" + config.Forumtitle.ToString().Trim() + "&Menu=yes\" target=\"_blank\">" + post.Qq.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("					</li>\r\n");

	}	//end if


	if (post.Yahoo!="")
	{

	templateBuilder.Append("					<li>\r\n");
	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/yahoo.gif\" width=\"16\" alt=\"Yahoo Messenger: " + post.Yahoo.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("						<a href=\"http://edit.yahoo.com/config/send_webmesg?.target=" + post.Yahoo.ToString().Trim() + "&.src=pg\" target=\"_blank\">" + post.Yahoo.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("					</li>\r\n");

	}	//end if

	templateBuilder.Append("				</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<!-- member menu -->\r\n");

	}	//end if

	 loopi = loopi+1;
	

	}	//end loop


	if (postlist.Count>=2)
	{


	if (valuablepostcount!=0)
	{

	templateBuilder.Append("<div id=\"ajaxdebateposts\">\r\n");
	templateBuilder.Append("<div class=\"box specialpostcontainer\">\r\n");
	templateBuilder.Append("	 <ul class=\"tabs\">\r\n");
	templateBuilder.Append("		<li class=\"current\">有价值的答案</li>\r\n");
	templateBuilder.Append("	 </ul>\r\n");

	int valuablepost__loop__id=0;
	foreach(ShowbonusPagePostInfo valuablepost in postlist)
	{
		valuablepost__loop__id++;


	if (valuablepost.Id!=1 && valuablepost.Isbest==1)
	{

	templateBuilder.Append("			<div class=\"specialpost\">\r\n");
	templateBuilder.Append("				<div class=\"postinfo\">\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(valuablepost.Posterid);
	
	templateBuilder.Append("					<h2><a id=\"memberinfo_" + loopi.ToString() + "\" href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\" onmouseover=\"showMenu(this.id,false)\" class=\"dropmenu\">" + valuablepost.Poster.ToString().Trim() + "</a> <span>\r\n");
	templateBuilder.Append(Convert.ToDateTime(valuablepost.Postdatetime).ToString("yyyy-MM-dd hh:mm"));
	templateBuilder.Append("</span></h2>\r\n");
	templateBuilder.Append("					<cite>\r\n");

	if (ismoder==1)
	{

	templateBuilder.Append("					        <a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + valuablepost.Pid.ToString().Trim() + "&pageid=" + pageid.ToString() + "\">编辑</a>\r\n");
	templateBuilder.Append("					        <a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + valuablepost.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}
	else
	{


	if (valuablepost.Posterid!=-1 && userid==valuablepost.Posterid)
	{


	if (topic.Closed==0)
	{

	templateBuilder.Append("						            <a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + valuablepost.Pid.ToString().Trim() + "&pageid=" + pageid.ToString() + "\">编辑</a>\r\n");

	}	//end if

	templateBuilder.Append("					            <a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + valuablepost.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}	//end if


	}	//end if


	if (canreply)
	{

	templateBuilder.Append("							<a href=\"postreply.aspx?topicid=" + topicid.ToString() + "&postid=" + valuablepost.Pid.ToString().Trim() + "&quote=yes\">引用</a>\r\n");

	if (userid!=-1)
	{

	templateBuilder.Append("								<a href=\"###\" onclick=\"replyToFloor('" + valuablepost.Id.ToString().Trim() + "', '" + valuablepost.Poster.ToString().Trim() + "', '" + valuablepost.Pid.ToString().Trim() + "')\">回复</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("					</cite>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				<div class=\"postmessage\">\r\n");
	templateBuilder.Append("					<div class=\"t_msgfont\">" + valuablepost.Message.ToString().Trim() + "</div>\r\n");
	templateBuilder.Append("				</div>				\r\n");
	templateBuilder.Append("			</div>	\r\n");

	if (valuablepost.Posterid!=-1)
	{

	templateBuilder.Append("				<!-- member menu -->\r\n");
	templateBuilder.Append("				<div class=\"popupmenu_popup userinfopanel\" id=\"memberinfo_" + loopi.ToString() + "_menu\" style=\"display: none; z-index: 50; filter: progid:dximagetransform.microsoft.shadow(direction=135,color=#cccccc,strength=2); left: 19px; clip: rect(auto auto auto auto); position absolute; top: 253px; width:150px;\" initialized ctrlkey=\"userinfo2\" h=\"209\">\r\n");
	templateBuilder.Append("					<p class=\"recivemessage\"><a href=\"usercppostpm.aspx?msgtoid=" + valuablepost.Posterid.ToString().Trim() + "\" target=\"_blank\">发送短消息</a></p>\r\n");

	if (useradminid>0)
	{


	if (admininfo.Allowviewip==1)
	{

	templateBuilder.Append("					<p  class=\"seeip\"><a href=\"getip.aspx?pid=" + valuablepost.Pid.ToString().Trim() + "&topicid=" + topicid.ToString() + "\" title=\"查看IP\">查看IP</a></p>\r\n");

	}	//end if


	if (admininfo.Allowbanuser==1)
	{

	templateBuilder.Append("					<p><a href=\"useradmin.aspx?action=banuser&uid=" + valuablepost.Posterid.ToString().Trim() + "\" title=\"禁止用户\">禁止用户</a></p>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("					<p>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(valuablepost.Posterid);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">查看公共资料</a></p>\r\n");
	templateBuilder.Append("					<p><a href=\"search.aspx?posterid=" + valuablepost.Posterid.ToString().Trim() + "\">查找该会员全部帖子</a></p>\r\n");
	templateBuilder.Append("					<ul>\r\n");
	templateBuilder.Append("					<li>UID:<span>" + valuablepost.Posterid.ToString().Trim() + "</span></li>\r\n");
	templateBuilder.Append("					<li>精华:<span>\r\n");

	if (valuablepost.Digestposts>0)
	{

	templateBuilder.Append("<a href=\"search.aspx?posterid=" + valuablepost.Posterid.ToString().Trim() + "&type=digest\">" + valuablepost.Digestposts.ToString().Trim() + "</a>\r\n");

	}
	else
	{

	templateBuilder.Append("" + valuablepost.Digestposts.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("</span></li>\r\n");

	if (score[1].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[1].ToString().Trim() + ":<span>" + valuablepost.Extcredits1.ToString().Trim() + " " + scoreunit[1].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[2].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[2].ToString().Trim() + ":<span>" + valuablepost.Extcredits2.ToString().Trim() + " " + scoreunit[2].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[3].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[3].ToString().Trim() + ":<span>" + valuablepost.Extcredits3.ToString().Trim() + " " + scoreunit[3].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[4].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[4].ToString().Trim() + ":<span>" + valuablepost.Extcredits4.ToString().Trim() + " " + scoreunit[4].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[5].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[5].ToString().Trim() + ":<span>" + valuablepost.Extcredits5.ToString().Trim() + " " + scoreunit[5].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[6].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[6].ToString().Trim() + ":<span>" + valuablepost.Extcredits6.ToString().Trim() + " " + scoreunit[6].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[7].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[7].ToString().Trim() + ":<span>" + valuablepost.Extcredits7.ToString().Trim() + " " + scoreunit[7].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[8].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[8].ToString().Trim() + ":<span>" + valuablepost.Extcredits8.ToString().Trim() + " " + scoreunit[8].ToString().Trim() + "</span></li>\r\n");

	}	//end if

	templateBuilder.Append("					</ul>\r\n");
	templateBuilder.Append("					<p>状态:<span>\r\n");

	if (valuablepost.Onlinestate==1)
	{

	templateBuilder.Append("						在线\r\n");

	}
	else
	{

	templateBuilder.Append("						离线\r\n");

	}	//end if

	templateBuilder.Append("</span>\r\n");
	templateBuilder.Append("					</p>\r\n");
	templateBuilder.Append("					<ul class=\"tools\">\r\n");

	if (valuablepost.Msn!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/msnchat.gif\" alt=\"MSN Messenger: " + valuablepost.Msn.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("							<a href=\"mailto:" + valuablepost.Msn.ToString().Trim() + "\" target=\"_blank\">" + valuablepost.Msn.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (valuablepost.Skype!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/skype.gif\" alt=\"Skype: " + valuablepost.Skype.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("							<a href=\"skype:" + valuablepost.Skype.ToString().Trim() + "\" target=\"_blank\">" + valuablepost.Skype.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (valuablepost.Icq!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/icq.gif\" alt=\"ICQ: " + valuablepost.Icq.ToString().Trim() + "\" />\r\n");
	templateBuilder.Append("							<a href=\"http://wwp.icq.com/scripts/search.dll?to=" + valuablepost.Icq.ToString().Trim() + "\" target=\"_blank\">" + valuablepost.Icq.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (valuablepost.Qq!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/qq.gif\" alt=\"QQ: " + valuablepost.Qq.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("							<a href=\"http://wpa.qq.com/msgrd?V=1&Uin=" + valuablepost.Qq.ToString().Trim() + "&Site=" + config.Forumtitle.ToString().Trim() + "&Menu=yes\" target=\"_blank\">" + valuablepost.Qq.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (valuablepost.Yahoo!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/yahoo.gif\" width=\"16\" alt=\"Yahoo Messenger: " + valuablepost.Yahoo.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("							<a href=\"http://edit.yahoo.com/config/send_webmesg?.target=" + valuablepost.Yahoo.ToString().Trim() + "&.src=pg\" target=\"_blank\">" + valuablepost.Yahoo.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if

	templateBuilder.Append("					</ul>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				<!-- member menu -->\r\n");

	}	//end if


	}	//end if

	 loopi = loopi+1;
	

	}	//end loop

	templateBuilder.Append("	</div>\r\n");

	}	//end if


	if (valuelesspostcount!=0)
	{

	templateBuilder.Append("        <div class=\"box othergoodsinfo\">\r\n");
	templateBuilder.Append("			<ul class=\"tabs\">\r\n");
	templateBuilder.Append("				<li class=\"current\">没有有价值的答案</li>\r\n");
	templateBuilder.Append("			</ul>\r\n");

	int valueless__loop__id=0;
	foreach(ShowbonusPagePostInfo valueless in postlist)
	{
		valueless__loop__id++;


	if (valueless.Id>1 && valueless.Isbest==0)
	{

	templateBuilder.Append("			<div class=\"specialpost\">\r\n");
	templateBuilder.Append("				<div class=\"postinfo\">\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(valueless.Posterid);
	
	templateBuilder.Append("					<h2><a id=\"memberinfo_" + loopi.ToString() + "\" href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\" onmouseover=\"showMenu(this.id,false)\" class=\"dropmenu\">" + valueless.Poster.ToString().Trim() + "</a> <span>\r\n");
	templateBuilder.Append(Convert.ToDateTime(valueless.Postdatetime).ToString("yyyy-MM-dd hh:mm"));
	templateBuilder.Append("</span></h2>\r\n");
	templateBuilder.Append("					<cite>\r\n");

	if (ismoder==1)
	{

	templateBuilder.Append("					        <a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + valueless.Pid.ToString().Trim() + "&pageid=" + pageid.ToString() + "\">编辑</a>\r\n");
	templateBuilder.Append("					        <a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + valueless.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}
	else
	{


	if (valueless.Posterid!=-1 && userid==valueless.Posterid)
	{


	if (topic.Closed==0)
	{

	templateBuilder.Append("						            <a href=\"editpost.aspx?topicid=" + topicid.ToString() + "&postid=" + valueless.Pid.ToString().Trim() + "&pageid=" + pageid.ToString() + "\">编辑</a>\r\n");

	}	//end if

	templateBuilder.Append("					            <a href=\"delpost.aspx?topicid=" + topicid.ToString() + "&postid=" + valueless.Pid.ToString().Trim() + "\" onclick=\"return confirm('确定要删除吗?');\">删除</a>\r\n");

	}	//end if


	}	//end if


	if (canreply)
	{

	templateBuilder.Append("							<a href=\"postreply.aspx?topicid=" + topicid.ToString() + "&postid=" + valueless.Pid.ToString().Trim() + "&quote=yes\">引用</a>\r\n");

	if (userid!=-1)
	{

	templateBuilder.Append("								<a href=\"###\" onclick=\"replyToFloor('" + valueless.Id.ToString().Trim() + "', '" + valueless.Poster.ToString().Trim() + "', '" + valueless.Pid.ToString().Trim() + "')\">回复</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("					</cite>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				<div class=\"postmessage\">\r\n");
	templateBuilder.Append("					<div class=\"t_msgfont\">" + valueless.Message.ToString().Trim() + "</div>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("			</div>\r\n");

	if (valueless.Posterid!=-1)
	{

	templateBuilder.Append("				<!-- member menu -->\r\n");
	templateBuilder.Append("				<div class=\"popupmenu_popup userinfopanel\" id=\"memberinfo_" + loopi.ToString() + "_menu\" style=\"display: none; z-index: 50; filter: progid:dximagetransform.microsoft.shadow(direction=135,color=#cccccc,strength=2); left: 19px; clip: rect(auto auto auto auto); position absolute; top: 253px; width:150px;\" initialized ctrlkey=\"userinfo2\" h=\"209\">\r\n");
	templateBuilder.Append("					<p class=\"recivemessage\"><a href=\"usercppostpm.aspx?msgtoid=" + valueless.Posterid.ToString().Trim() + "\" target=\"_blank\">发送短消息</a></p>\r\n");

	if (useradminid>0)
	{


	if (admininfo.Allowviewip==1)
	{

	templateBuilder.Append("					<p  class=\"seeip\"><a href=\"getip.aspx?pid=" + valueless.Pid.ToString().Trim() + "&topicid=" + topicid.ToString() + "\" title=\"查看IP\">查看IP</a></p>\r\n");

	}	//end if


	if (admininfo.Allowbanuser==1)
	{

	templateBuilder.Append("					<p><a href=\"useradmin.aspx?action=banuser&uid=" + valueless.Posterid.ToString().Trim() + "\" title=\"禁止用户\">禁止用户</a></p>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("					<p>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(valueless.Posterid);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">查看公共资料</a></p>\r\n");
	templateBuilder.Append("					<p><a href=\"search.aspx?posterid=" + valueless.Posterid.ToString().Trim() + "\">查找该会员全部帖子</a></p>\r\n");
	templateBuilder.Append("					<ul>\r\n");
	templateBuilder.Append("					<li>UID:<span>" + valueless.Posterid.ToString().Trim() + "</span></li>\r\n");
	templateBuilder.Append("					<li>精华:<span>\r\n");

	if (valueless.Digestposts>0)
	{

	templateBuilder.Append("<a href=\"search.aspx?posterid=" + valueless.Posterid.ToString().Trim() + "&type=digest\">" + valueless.Digestposts.ToString().Trim() + "</a>\r\n");

	}
	else
	{

	templateBuilder.Append("" + valueless.Digestposts.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("</span></li>\r\n");

	if (score[1].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[1].ToString().Trim() + ":<span>" + valueless.Extcredits1.ToString().Trim() + " " + scoreunit[1].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[2].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[2].ToString().Trim() + ":<span>" + valueless.Extcredits2.ToString().Trim() + " " + scoreunit[2].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[3].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[3].ToString().Trim() + ":<span>" + valueless.Extcredits3.ToString().Trim() + " " + scoreunit[3].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[4].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[4].ToString().Trim() + ":<span>" + valueless.Extcredits4.ToString().Trim() + " " + scoreunit[4].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[5].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[5].ToString().Trim() + ":<span>" + valueless.Extcredits5.ToString().Trim() + " " + scoreunit[5].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[6].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[6].ToString().Trim() + ":<span>" + valueless.Extcredits6.ToString().Trim() + " " + scoreunit[6].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[7].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[7].ToString().Trim() + ":<span>" + valueless.Extcredits7.ToString().Trim() + " " + scoreunit[7].ToString().Trim() + "</span></li>\r\n");

	}	//end if


	if (score[8].ToString().Trim()!="")
	{

	templateBuilder.Append("						<li>" + score[8].ToString().Trim() + ":<span>" + valueless.Extcredits8.ToString().Trim() + " " + scoreunit[8].ToString().Trim() + "</span></li>\r\n");

	}	//end if

	templateBuilder.Append("					</ul>\r\n");
	templateBuilder.Append("					<p>状态:<span>\r\n");

	if (valueless.Onlinestate==1)
	{

	templateBuilder.Append("						在线\r\n");

	}
	else
	{

	templateBuilder.Append("						离线\r\n");

	}	//end if

	templateBuilder.Append("</span>\r\n");
	templateBuilder.Append("					</p>\r\n");
	templateBuilder.Append("					<ul class=\"tools\">\r\n");

	if (valueless.Msn!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/msnchat.gif\" alt=\"MSN Messenger: " + valueless.Msn.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("							<a href=\"mailto:" + valueless.Msn.ToString().Trim() + "\" target=\"_blank\">" + valueless.Msn.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (valueless.Skype!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/skype.gif\" alt=\"Skype: " + valueless.Skype.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("							<a href=\"skype:" + valueless.Skype.ToString().Trim() + "\" target=\"_blank\">" + valueless.Skype.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (valueless.Icq!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/icq.gif\" alt=\"ICQ: " + valueless.Icq.ToString().Trim() + "\" />\r\n");
	templateBuilder.Append("							<a href=\"http://wwp.icq.com/scripts/search.dll?to=" + valueless.Icq.ToString().Trim() + "\" target=\"_blank\">" + valueless.Icq.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (valueless.Qq!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/qq.gif\" alt=\"QQ: " + valueless.Qq.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("							<a href=\"http://wpa.qq.com/msgrd?V=1&Uin=" + valueless.Qq.ToString().Trim() + "&Site=" + config.Forumtitle.ToString().Trim() + "&Menu=yes\" target=\"_blank\">" + valueless.Qq.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if


	if (valueless.Yahoo!="")
	{

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/yahoo.gif\" width=\"16\" alt=\"Yahoo Messenger: " + valueless.Yahoo.ToString().Trim() + "\"/>\r\n");
	templateBuilder.Append("							<a href=\"http://edit.yahoo.com/config/send_webmesg?.target=" + valueless.Yahoo.ToString().Trim() + "&.src=pg\" target=\"_blank\">" + valueless.Yahoo.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end if

	templateBuilder.Append("					</ul>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				<!-- member menu -->\r\n");

	}	//end if


	}	//end if

	 loopi = loopi+1;
	

	}	//end loop

	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (canreply && userid!=-1)
	{

	templateBuilder.Append("<!--快速回复主题,将_ajaxquickreply替换成_quickreply可变为传统form提交方式-->\r\n");


	if (quickeditorad!="")
	{

	templateBuilder.Append("<div class=\"leaderboard\">" + quickeditorad.ToString() + "</div>\r\n");

	}	//end if

	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/post.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/bbcode.js\"></" + "script>\r\n");
	templateBuilder.Append("<form method=\"post\" name=\"postform\" id=\"postform\" action=\"postreply.aspx?topicid=" + topicid.ToString() + "\" enctype=\"multipart/form-data\" onsubmit=\"return validate(this);\">\r\n");
	templateBuilder.Append("<div id=\"quickpost\" class=\"box\">\r\n");
	templateBuilder.Append("	<h4>快速回复帖子</h4>\r\n");
	templateBuilder.Append("	<div class=\"postoptions\">\r\n");
	templateBuilder.Append("		<h5>选项</h5>\r\n");
	templateBuilder.Append("		<p><label><input type=\"checkbox\" value=\"1\" name=\"parseurloff\" id=\"parseurloff\" \r\n");

	if (parseurloff==1)
	{

	templateBuilder.Append("checked \r\n");

	}	//end if

	templateBuilder.Append(" /> 禁用</label>URL 识别</label></p>\r\n");
	templateBuilder.Append("		<p><label><input type=\"checkbox\" value=\"1\" name=\"smileyoff\" id=\"smileyoff\" \r\n");

	if (smileyoff==1)
	{

	templateBuilder.Append(" checked disabled \r\n");

	}	//end if

	templateBuilder.Append(" /> 禁用 </label>表情</p>\r\n");
	templateBuilder.Append("		<p><label><input type=\"checkbox\" value=\"1\" name=\"bbcodeoff\" id=\"bbcodeoff\" \r\n");

	if (bbcodeoff==1)
	{

	templateBuilder.Append(" checked disabled \r\n");

	}	//end if

	templateBuilder.Append(" /> 禁用 </label>Discuz!代码</p>\r\n");
	templateBuilder.Append("		<p><label><input type=\"checkbox\" value=\"1\" name=\"usesig\" id=\"usesig\"\r\n");

	if (usesig==1)
	{

	templateBuilder.Append("checked\r\n");

	}	//end if

	templateBuilder.Append("/> 使用个人签名</label></p>\r\n");
	templateBuilder.Append("		<p><label><input type=\"checkbox\" name=\"emailnotify\" id=\"emailnotify\" /> 发送邮件通知楼主</label></p>	\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"postform\">\r\n");
	templateBuilder.Append("		<h5><label for=\"subject\">标题</label>\r\n");

	if (isenddebate==false)
	{

	templateBuilder.Append("			<select name=\"debateopinion\" id=\"debateopinion\">\r\n");
	templateBuilder.Append("				<option value=\"0\" selected></option>\r\n");
	templateBuilder.Append("				<option value=\"1\">正方</option>\r\n");
	templateBuilder.Append("				<option value=\"2\">反方</option>\r\n");
	templateBuilder.Append("			</select>\r\n");

	}	//end if

	templateBuilder.Append("			<input type=\"text\" id=\"title\" name=\"title\" size=\"84\" tabindex=\"1\" value=\"\" /><input type=\"hidden\" id=\"postid\" name=\"postid\" value=\"-1\" />\r\n");
	templateBuilder.Append("		</h5>\r\n");
	templateBuilder.Append("		<p><label>内容</label>\r\n");
	templateBuilder.Append("		<textarea rows=\"7\" cols=\"80\" name=\"message\" id=\"message\" onKeyDown=\"ctlent(event,false);\" tabindex=\"2\" class=\"autosave\" style=\"background:url(" + quickbgadimg.ToString() + ") no-repeat 0 0; \" \r\n");

	if (quickbgadlink!="")
	{

	templateBuilder.Append(" onfocus=\"$('adlinkbtn').style.display='';$('closebtn').style.display='';\"\r\n");

	}	//end if

	templateBuilder.Append("></textarea>\r\n");
	templateBuilder.Append("		</p>\r\n");

	if (isseccode)
	{

	templateBuilder.Append("		<p>验证码:\r\n");

	templateBuilder.Append("<!-- onkeydown=\"return (event.keyCode ? event.keyCode : event.which ? event.which : event.charCode) != 13\"-->\r\n");
	templateBuilder.Append("<input size=\"10\"  style=\"width:50px;\" class=\"colorblue\" onfocus=\"this.className='colorfocus';\" onblur=\"this.className='colorblue';\" onkeyup=\"changevcode(this.form, this.value);\" />\r\n");
	templateBuilder.Append("&nbsp;\r\n");
	templateBuilder.Append("<img src=\"tools/VerifyImagePage.aspx?time=" + Processtime.ToString() + "\" class=\"cursor\" id=\"vcodeimg\" onclick=\"this.src='tools/VerifyImagePage.aspx?id=" + olid.ToString() + "&time=' + Math.random();\" />\r\n");
	templateBuilder.Append("<input name=\"reloadvcade\" type=\"button\" class=\"colorblue\" id=\"reloadvcade\" value=\"刷新验证码\"  onclick=\"document.getElementById('vcodeimg').src='tools/VerifyImagePage.aspx?time=' + Math.random();\" tabindex=\"-1\"  style=\"color:#99cc00; width:75px;\" />\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	$('vcodeimg').src='tools/VerifyImagePage.aspx?bgcolor=F5FAFE&time=' + Math.random();\r\n");
	templateBuilder.Append("	//document.getElementById('vcode').value = \"\";\r\n");
	templateBuilder.Append("	function changevcode(form, value)\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		if (!$('vcode'))\r\n");
	templateBuilder.Append("		{\r\n");
	templateBuilder.Append("			var vcode = document.createElement('input');\r\n");
	templateBuilder.Append("			vcode.id = 'vcode';\r\n");
	templateBuilder.Append("			vcode.name = 'vcode';\r\n");
	templateBuilder.Append("			vcode.type = 'hidden';\r\n");
	templateBuilder.Append("			vcode.value = value;\r\n");
	templateBuilder.Append("			form.appendChild(vcode);\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("		else\r\n");
	templateBuilder.Append("		{\r\n");
	templateBuilder.Append("			$('vcode').value = value;\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("</" + "script>\r\n");


	templateBuilder.Append("</p>\r\n");

	}	//end if

	templateBuilder.Append("		<p class=\"btns\">\r\n");
	templateBuilder.Append("			<a href=\"###\" id=\"closebtn\" style=\"display:none;\" onclick=\"$('message').style.background='';this.style.display='none';\">关闭广告</a>\r\n");
	templateBuilder.Append("			<a href=\"" + quickbgadlink.ToString() + "\" id=\"adlinkbtn\" style=\"display:none;\" target=\"_blank\" onclick=\"\">进入广告</a>\r\n");
	templateBuilder.Append("			<button type=\"submit\" id=\"postsubmit\" name=\"replysubmit\" value=\"发表帖子\" tabindex=\"3\">发表帖子</button>[可按Ctrl+Enter发布]&nbsp;\r\n");
	templateBuilder.Append("			<input  name=\"topicsreset\" id=\"restoredata\" value=\"清空内容\" tabindex=\"6\" title=\"清空内容\" onclick=\"javascript:document.postform.reset(); return true; \" type=\"reset\" />\r\n");
	templateBuilder.Append("			<input name=\"restoredata\" id=\"restoredata\" value=\"恢复数据\" tabindex=\"5\" title=\"恢复上次自动保存的数据\" onclick=\"loadData();\" type=\"button\" />\r\n");
	templateBuilder.Append("			<input type=\"hidden\" id=\"postbytopictype\" name=\"postbytopictype\" value=\"" + forum.Postbytopictype.ToString().Trim() + "\" tabindex=\"3\" />\r\n");
	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"smilies\">\r\n");
	templateBuilder.Append("		 <script type=\"text/javascript\">\r\n");
	templateBuilder.Append("					var textobj = $('message');\r\n");
	templateBuilder.Append("					var lang = new Array();\r\n");
	templateBuilder.Append("					if(is_ie >= 5 || is_moz >= 2) {\r\n");
	templateBuilder.Append("						window.onbeforeunload = function () {\r\n");
	templateBuilder.Append("							saveData(textobj.value);\r\n");
	templateBuilder.Append("						};\r\n");
	templateBuilder.Append("						lang['post_autosave_none'] = \"没有可以恢复的数据\";\r\n");
	templateBuilder.Append("						lang['post_autosave_confirm'] = \"本操作将覆盖当前帖子内容，确定要恢复数据吗？\";\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					else {\r\n");
	templateBuilder.Append("						$('restoredata').style.display = 'none';\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					var bbinsert = parseInt('1');\r\n");
	templateBuilder.Append("					var smileyinsert = parseInt('1');\r\n");
	templateBuilder.Append("					var editor_id = 'posteditor';\r\n");
	templateBuilder.Append("					var smiliesCount = 9;\r\n");
	templateBuilder.Append("					var colCount = 3;\r\n");
	templateBuilder.Append("					var showsmiliestitle = 0;\r\n");
	templateBuilder.Append("					var smiliesIsCreate = 0;\r\n");
	templateBuilder.Append("					var scrMaxLeft; //表情滚动条宽度\r\n");
	templateBuilder.Append("			var smilies_HASH = {};\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	string defaulttypname = string.Empty;
	
	templateBuilder.Append("		<div class=\"navcontrol\">\r\n");
	templateBuilder.Append("			<div class=\"smiliepanel\">\r\n");
	templateBuilder.Append("				<div id=\"scrollbar\" class=\"scrollbar\">\r\n");
	templateBuilder.Append("					<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("						<tr>\r\n");

	int stype__loop__id=0;
	foreach(DataRow stype in smilietypes.Rows)
	{
		stype__loop__id++;


	if (stype__loop__id==1)
	{

	 defaulttypname = stype["code"].ToString().Trim();
	

	}	//end if

	templateBuilder.Append("							<td id=\"t_s_" + stype__loop__id.ToString() + "\"><div id=\"s_" + stype__loop__id.ToString() + "\" onclick=\"showsmiles(" + stype__loop__id.ToString() + ", '" + stype["code"].ToString().Trim() + "');\" \r\n");

	if (stype__loop__id!=1)
	{

	templateBuilder.Append("style=\"display:none;\"\r\n");

	}
	else
	{

	templateBuilder.Append("class=\"lian\"\r\n");

	}	//end if

	templateBuilder.Append(">" + stype["code"].ToString().Trim() + "</div></td>\r\n");

	}	//end loop

	templateBuilder.Append("						</tr>\r\n");
	templateBuilder.Append("					</table>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				<div id=\"scrlcontrol\">\r\n");
	templateBuilder.Append("					<img src=\"editor/images/smilie_prev_default.gif\" alt=\"向前\" onmouseover=\"if($('scrollbar').scrollLeft>0)this.src=this.src.replace(/_default|_selected/, '_hover');\" onmouseout=\"this.src=this.src.replace(/_hover|_selected/, '_default');\" onmousedown=\"if($('scrollbar').scrollLeft>0){this.src=this.src.replace(/_hover|_default/, '_selected');this.boder=1;}\" onmouseup=\"if($('scrollbar').scrollLeft>0)this.src=this.src.replace(/_selected/, '_hover');else{this.src=this.src.replace(/_selected|_hover/, '_default');}this.border=0;\" onclick=\"scrollSmilieTypeBar($('scrollbar'), 1-$('t_s_1').clientWidth);\"/>&nbsp;\r\n");
	templateBuilder.Append("					<img src=\"editor/images/smilie_next_default.gif\" alt=\"向后\"  onmouseover=\"if($('scrollbar').scrollLeft<scrMaxLeft)this.src=this.src.replace(/_default|_selected/, '_hover');\" onmouseout=\"this.src=this.src.replace(/_hover|_selected/, '_default');\" onmousedown=\"if($('scrollbar').scrollLeft<scrMaxLeft){this.src=this.src.replace(/_hover|_default/, '_selected');this.boder=1;}\" onmouseup=\"if($('scrollbar').scrollLeft<scrMaxLeft)this.src=this.src.replace(/_selected/, '_hover');else{this.src=this.src.replace(/_selected|_hover/, '_default');}this.border=0;\" onclick=\"scrollSmilieTypeBar($('scrollbar'), $('t_s_1').clientWidth);\" />\r\n");
	templateBuilder.Append("				</div>	\r\n");
	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("			<div id=\"showsmilie\"><img src=\"images/common/loading_wide.gif\" width=\"90%\" alt=\"表情加载\"/><p>正在加载表情...</p></div>\r\n");
	templateBuilder.Append("			<div id=\"showsmilie_pagenum\">&nbsp;</div>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			var firstpagesmilies_json ={ " + firstpagesmilies.ToString() + " };\r\n");
	templateBuilder.Append("			showFirstPageSmilies(firstpagesmilies_json, '" + defaulttypname.ToString() + "', 12);\r\n");
	templateBuilder.Append("			function getSmilies(func){\r\n");
	templateBuilder.Append("				var c=\"tools/ajax.aspx?t=smilies\";\r\n");
	templateBuilder.Append("				_sendRequest(c,function(d){var e={};try{e=eval(\"(\"+d+\")\")}catch(f){e={}}var h=e?e:null;func(h);e=null;func=null},false,true)\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("			getSmilies(function(obj){ \r\n");
	templateBuilder.Append("			smilies_HASH = obj; \r\n");
	templateBuilder.Append("			showsmiles(1, '" + defaulttypname.ToString() + "');\r\n");
	templateBuilder.Append("			});\r\n");
	templateBuilder.Append("			window.onload = function() {\r\n");
	templateBuilder.Append("				$('scrollbar').scrollLeft = 10000;\r\n");
	templateBuilder.Append("				scrMaxLeft = $('scrollbar').scrollLeft;\r\n");
	templateBuilder.Append("				$('scrollbar').scrollLeft = 1;	\r\n");
	templateBuilder.Append("				if ($('scrollbar').scrollLeft > 0)\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					$('scrlcontrol').style.display = '';\r\n");
	templateBuilder.Append("					$('scrollbar').scrollLeft = 0;	\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</form>\r\n");



	}	//end if


	if (useradminid>0||usergroupinfo.Raterange!=""||config.Forumjump==1)
	{

	templateBuilder.Append("<div id=\"footfilter\" class=\"box\">\r\n");

	if (useradminid>0)
	{

	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			function action_onchange(value,objfrm,postid){\r\n");
	templateBuilder.Append("				if (value != ''){\r\n");
	templateBuilder.Append("					objfrm.operat.value = value;\r\n");
	templateBuilder.Append("					objfrm.postid.value = postid;\r\n");
	templateBuilder.Append("					if(value != 'delpost'){\r\n");
	templateBuilder.Append("						objfrm.submit();\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					else{\r\n");
	templateBuilder.Append("						$('delpost').submit();\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	templateBuilder.Append("		<form id=\"moderate\" name=\"moderate\" method=\"post\" action=\"topicadmin.aspx?action=moderate&forumid=" + forumid.ToString() + "\">\r\n");
	templateBuilder.Append("			<input name=\"forumid\" type=\"hidden\" value=\"" + forumid.ToString() + "\" />\r\n");
	templateBuilder.Append("			<input name=\"topicid\" type=\"hidden\" value=\"" + topicid.ToString() + "\" />\r\n");
	templateBuilder.Append("			<input name=\"postid\" type=\"hidden\" value=\"\" />\r\n");
	templateBuilder.Append("			<input name=\"operat\" type=\"hidden\" value=\"\" />\r\n");
	templateBuilder.Append("			<select id=\"operatSel\" onchange=\"action_onchange(this.options[this.selectedIndex].value,this.form,0);\"\r\n");
	templateBuilder.Append("				name=\"operatSel\">\r\n");
	templateBuilder.Append("				<option value=\"\" selected=\"selected\">管理选项</option>\r\n");
	templateBuilder.Append("				<option value=\"delete\">删除主题</option>\r\n");
	templateBuilder.Append("				<option value=\"delpost\">批量删帖</option>\r\n");
	templateBuilder.Append("				<option value=\"close\">关闭主题</option>\r\n");
	templateBuilder.Append("				<option value=\"move\">移动主题</option>\r\n");
	templateBuilder.Append("				<option value=\"copy\">复制主题</option>\r\n");
	templateBuilder.Append("				<option value=\"highlight\">高亮显示</option>\r\n");
	templateBuilder.Append("				<option value=\"digest\">设置精华</option>\r\n");
	templateBuilder.Append("				<option value=\"identify\">鉴定主题</option>\r\n");
	templateBuilder.Append("				<option value=\"displayorder\">主题置顶</option>\r\n");
	templateBuilder.Append("				<option value=\"split\">分割主题</option>\r\n");
	templateBuilder.Append("				<option value=\"merge\">合并主题</option>\r\n");
	templateBuilder.Append("				<option value=\"repair\">修复主题</option>\r\n");
	templateBuilder.Append("			</select>\r\n");
	templateBuilder.Append("		</form>\r\n");

	}
	else if (usergroupinfo.Raterange!="")
	{

	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			function action_onchange(value,objfrm,postid){\r\n");
	templateBuilder.Append("				if (value != ''){\r\n");
	templateBuilder.Append("					objfrm.operat.value = value;\r\n");
	templateBuilder.Append("					objfrm.postid.value = postid;\r\n");
	templateBuilder.Append("					if(value != 'delpost'){\r\n");
	templateBuilder.Append("						objfrm.submit();\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					else{\r\n");
	templateBuilder.Append("						$('delpost').submit();\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	templateBuilder.Append("		<form id=\"moderate\" name=\"moderate\" method=\"post\" action=\"topicadmin.aspx?action=moderate&forumid=" + forumid.ToString() + "\">\r\n");
	templateBuilder.Append("			<input name=\"forumid\" type=\"hidden\" value=\"" + forumid.ToString() + "\" />\r\n");
	templateBuilder.Append("			<input name=\"topicid\" type=\"hidden\" value=\"" + topicid.ToString() + "\" />\r\n");
	templateBuilder.Append("			<input name=\"postid\" type=\"hidden\" value=\"\" />\r\n");
	templateBuilder.Append("			<input name=\"operat\" type=\"hidden\" value=\"\" />\r\n");
	templateBuilder.Append("		</form>\r\n");

	}	//end if


	if (config.Forumjump==1)
	{

	templateBuilder.Append("		<select onchange=\"if(this.options[this.selectedIndex].value != '') {if(" + config.Aspxrewrite.ToString().Trim() + ") {\r\n");
	templateBuilder.Append("		window.location='showforum-'+this.options[this.selectedIndex].value+'" + config.Extname.ToString().Trim() + "'; }else{window.location='showforum.aspx?forumid='+this.options[this.selectedIndex].value;}}\">\r\n");
	templateBuilder.Append("			  <option>论坛跳转...</option>\r\n");
	templateBuilder.Append("			" + forumlistboxoptions.ToString() + "\r\n");
	templateBuilder.Append("		</select>\r\n");

	}	//end if

	templateBuilder.Append("</div>\r\n");

	}	//end if


	}	//end if


	}
	else
	{


	if (needlogin)
	{


	templateBuilder.Append("<div class=\"box message\">\r\n");
	templateBuilder.Append("	<h1>" + config.Forumtitle.ToString().Trim() + " 提示信息</h1>\r\n");
	templateBuilder.Append("	<p>您无权进行当前操作，这可能因以下原因之一造成</p>\r\n");
	templateBuilder.Append("	<p><b>" + msgbox_text.ToString() + "</b></p>\r\n");
	templateBuilder.Append("	<p>您还没有登录，请填写下面的登录表单后再尝试访问。</p>\r\n");
	templateBuilder.Append("	<form id=\"formlogin\" name=\"formlogin\" method=\"post\" action=\"login.aspx\" onsubmit=\"submitLogin(this);\">\r\n");
	templateBuilder.Append("		<input type=\"hidden\" value=\"2592000\" name=\"cookietime\"/>\r\n");
	templateBuilder.Append("	<div class=\"box\" style=\"margin: 10px auto; width: 60%;\">\r\n");
	templateBuilder.Append("		<table cellpadding=\"4\" cellspacing=\"0\" width=\"100%\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td colspan=\"2\">会员登录</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>用户名</td>\r\n");
	templateBuilder.Append("				<td><input type=\"text\" id=\"username\" name=\"username\" size=\"25\" maxlength=\"40\" tabindex=\"2\" />  <a href=\"register.aspx\" tabindex=\"-1\" accesskey=\"r\" title=\"立即注册 (ALT + R)\">立即注册</a>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>密码</td>\r\n");
	templateBuilder.Append("				<td><input type=\"password\" name=\"password\" size=\"25\" tabindex=\"3\" /> <a href=\"getpassword.aspx\" tabindex=\"-1\" accesskey=\"g\" title=\"忘记密码 (ALT + G)\">忘记密码</a>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");

	if (config.Secques==1)
	{

	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>安全问题</td>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<select name=\"questionid\" tabindex=\"4\">\r\n");
	templateBuilder.Append("					<option value=\"0\">&nbsp;</option>\r\n");
	templateBuilder.Append("					<option value=\"1\">母亲的名字</option>\r\n");
	templateBuilder.Append("					<option value=\"2\">爷爷的名字</option>\r\n");
	templateBuilder.Append("					<option value=\"3\">父亲出生的城市</option>\r\n");
	templateBuilder.Append("					<option value=\"4\">您其中一位老师的名字</option>\r\n");
	templateBuilder.Append("					<option value=\"5\">您个人计算机的型号</option>\r\n");
	templateBuilder.Append("					<option value=\"6\">您最喜欢的餐馆名称</option>\r\n");
	templateBuilder.Append("					<option value=\"7\">驾驶执照的最后四位数字</option>\r\n");
	templateBuilder.Append("					</select>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>答案</td>\r\n");
	templateBuilder.Append("				<td><input type=\"text\" name=\"answer\" size=\"25\" tabindex=\"5\" /></td>\r\n");
	templateBuilder.Append("			</tr>\r\n");

	}	//end if

	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>&nbsp;</td>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<button class=\"submit\" type=\"submit\" name=\"loginsubmit\" id=\"loginsubmit\" value=\"true\" tabindex=\"6\">会员登录</button>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	</form>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	document.getElementById(\"username\").focus();\r\n");
	templateBuilder.Append("	function submitLogin(loginForm)\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		loginForm.action = 'login.aspx?loginsubmit=true&reurl=' + escape(window.location);\r\n");
	templateBuilder.Append("		loginForm.submit();\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("</" + "script>\r\n");
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


	}	//end if

	templateBuilder.Append("</div>\r\n");


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
