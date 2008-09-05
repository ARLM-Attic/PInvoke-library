<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.forumindex" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:05:15.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:05:15. 
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


	templateBuilder.Append("<div id=\"foruminfo\">\r\n");
	templateBuilder.Append("	<div id=\"userinfo\">\r\n");
	templateBuilder.Append("		<div id=\"nav\">\r\n");
	templateBuilder.Append("		<p><a id=\"forumlist\" href=\"" + config.Forumurl.ToString().Trim() + "\" \r\n");

	if (config.Forumjump==1)
	{

	templateBuilder.Append("onmouseover=\"showMenu(this.id);\" onmouseout=\"showMenu(this.id);\"\r\n");

	}	//end if

	templateBuilder.Append(">" + config.Forumtitle.ToString().Trim() + "</a>		主题:<em>" + totaltopic.ToString() + "</em>, 帖子:<em>" + totalpost.ToString() + "</em> \r\n");
	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("		<p>\r\n");

	if (userid==-1)
	{

	templateBuilder.Append("		<form id=\"loginform\" name=\"login\" method=\"post\" action=\"login.aspx\">\r\n");
	templateBuilder.Append("			<input type=\"hidden\" name=\"referer\" value=\"index.aspx\" />\r\n");
	templateBuilder.Append("			<input onclick=\"if(this.value=='用户名')this.value = ''\" value=\"用户名\" tabindex=\"1\" maxlength=\"40\" size=\"15\" name=\"username\" id=\"username\" \r\n");
	templateBuilder.Append("	type=\"text\" />\r\n");
	templateBuilder.Append("			<input type=\"password\" size=\"10\" name=\"password\" id=\"password\" tabindex=\"3\" />\r\n");
	templateBuilder.Append("			<button value=\"true\" type=\"submit\" name=\"userlogin\" onclick=\"javascript:window.location.replace('?agree=yes')\"> 登录 </button>\r\n");
	templateBuilder.Append("		</form>\r\n");

	}
	else
	{

	templateBuilder.Append("		您上次访问是在: " + userinfo.Lastvisit.ToString().Trim() + " 		\r\n");

	if (config.Enablespace==1)
	{


	if (isactivespace)
	{

	templateBuilder.Append("			<a href=\"" + spaceurl.ToString() + "space\">个人空间</a>\r\n");

	}
	else if (isallowapply)
	{

	templateBuilder.Append("			<a href=\"spaceregister.aspx\">申请空间</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("			<a href=\"showtopiclist.aspx\">查看新帖</a>\r\n");

	}	//end if

	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div id=\"forumstats\">\r\n");

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

	templateBuilder.Append("		<p>\r\n");
	templateBuilder.Append("		今日:<em>" + todayposts.ToString() + "</em>, 昨日:<em>" + yesterdayposts.ToString() + "</em>, \r\n");

	if (highestpostsdate!="")
	{

	templateBuilder.Append("		最高日:<em>" + highestposts.ToString() + "</em>(" + highestpostsdate.ToString() + ")\r\n");

	}	//end if

	templateBuilder.Append("			<a href=\"showtopiclist.aspx?type=digest&amp;forums=all\">精华区</a>\r\n");

	if (config.Rssstatus!=0)
	{

	templateBuilder.Append("			<a href=\"tools/rss.aspx\" target=\"_blank\"><img src=\"templates/" + templatepath.ToString() + "/images/rss.gif\" alt=\"rss\"/></a>\r\n");

	}	//end if

	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (announcementcount>0)
	{

	templateBuilder.Append("<div onmouseout=\"annstop = 0\" onmouseover=\"annstop = 1\" id=\"announcement\">\r\n");
	templateBuilder.Append("	<div id=\"announcementbody\">\r\n");
	templateBuilder.Append("		<ul>		\r\n");

	int announcement__loop__id=0;
	foreach(DataRow announcement in announcementlist.Rows)
	{
		announcement__loop__id++;

	templateBuilder.Append("        <li><a href=\"announcement.aspx#" + announcement["id"].ToString().Trim() + "\">" + announcement["title"].ToString().Trim() + "<em>" + announcement["starttime"].ToString().Trim() + "</em></a></li>\r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	var anndelay = 3000;\r\n");
	templateBuilder.Append("	var annst = 0;\r\n");
	templateBuilder.Append("	var annstop = 0;\r\n");
	templateBuilder.Append("	var annrowcount = 0;\r\n");
	templateBuilder.Append("	var anncount = 0;\r\n");
	templateBuilder.Append("	var annlis = $('announcementbody').getElementsByTagName(\"LI\");\r\n");
	templateBuilder.Append("	var annrows = new Array();\r\n");
	templateBuilder.Append("	var annstatus;\r\n");
	templateBuilder.Append("	function announcementScroll() {\r\n");
	templateBuilder.Append("		if(annstop) {\r\n");
	templateBuilder.Append("			annst = setTimeout('announcementScroll()', anndelay);\r\n");
	templateBuilder.Append("			return;\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("		if(!annst) {\r\n");
	templateBuilder.Append("			var lasttop = -1;\r\n");
	templateBuilder.Append("			for(i = 0;i < annlis.length;i++) {\r\n");
	templateBuilder.Append("				if(lasttop != annlis[i].offsetTop) {\r\n");
	templateBuilder.Append("					if(lasttop == -1) {\r\n");
	templateBuilder.Append("						lasttop = 0;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					annrows[annrowcount] = annlis[i].offsetTop - lasttop;\r\n");
	templateBuilder.Append("					annrowcount++;\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				lasttop = annlis[i].offsetTop;\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("			if(annrows.length == 1) {\r\n");
	templateBuilder.Append("				$('announcement').onmouseover = $('announcement').onmouseout = null;\r\n");
	templateBuilder.Append("			} else {\r\n");
	templateBuilder.Append("				annrows[annrowcount] = annrows[1];\r\n");
	templateBuilder.Append("				$('announcementbody').innerHTML += '<br style=\"clear:both\" />' + $('announcementbody').innerHTML;\r\n");
	templateBuilder.Append("				annst = setTimeout('announcementScroll()', anndelay);\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("			annrowcount = 1;\r\n");
	templateBuilder.Append("			return;\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("		if(annrowcount >= annrows.length) {\r\n");
	templateBuilder.Append("			$('announcementbody').scrollTop = 0;\r\n");
	templateBuilder.Append("			annrowcount = 1;\r\n");
	templateBuilder.Append("			annst = setTimeout('announcementScroll()', anndelay);\r\n");
	templateBuilder.Append("		} else {\r\n");
	templateBuilder.Append("			anncount = 0;\r\n");
	templateBuilder.Append("			announcementScrollnext(annrows[annrowcount]);\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("	function announcementScrollnext(time) {\r\n");
	templateBuilder.Append("		$('announcementbody').scrollTop++;\r\n");
	templateBuilder.Append("		anncount++;\r\n");
	templateBuilder.Append("		if(anncount != time) {\r\n");
	templateBuilder.Append("			annst = setTimeout('announcementScrollnext(' + time + ')', 10);\r\n");
	templateBuilder.Append("		} else {\r\n");
	templateBuilder.Append("			annrowcount++;\r\n");
	templateBuilder.Append("			annst = setTimeout('announcementScroll()', anndelay);\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("	announcementScroll();\r\n");
	templateBuilder.Append("</" + "script>\r\n");

	}	//end if



	if (pagewordad.Length>0)
	{

	templateBuilder.Append("<!--adtext-->\r\n");
	templateBuilder.Append("<div id=\"ad_text\" class=\"ad_text\">\r\n");
	templateBuilder.Append("	<table cellspacing=\"1\" cellpadding=\"0\" summary=\"Text Ad\">\r\n");
	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	int adindex = 0;
	

	int pageword__loop__id=0;
	foreach(string pageword in pagewordad)
	{
		pageword__loop__id++;


	if (adindex<4)
	{

	templateBuilder.Append("				<td>" + pageword.ToString() + "</td>\r\n");
	 adindex = adindex+1;
	

	}
	else
	{

	templateBuilder.Append("				</tr><tr>\r\n");
	templateBuilder.Append("				<td>" + pageword.ToString() + "</td>\r\n");
	 adindex = 1;
	

	}	//end if


	}	//end loop


	if (pagewordad.Length%4>0)
	{


					for (int j = 0; j < (4 - pagewordad.Length % 4); j++)
					{
				
	templateBuilder.Append("				<td>&nbsp;</td>\r\n");

					}
				

	}	//end if

	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<!--adtext-->\r\n");

	}	//end if





	if (newpmcount>0 && showpmhint)
	{

	templateBuilder.Append("<!--短信息 area start-->\r\n");
	templateBuilder.Append("<div class=\"mainbox\">\r\n");

	if (pmsound>0)
	{

	templateBuilder.Append("		<bgsound src=\"sound/pm" + pmsound.ToString() + ".wav\" />\r\n");

	}	//end if

	templateBuilder.Append("	<span class=\"headactions\"><a href=\"usercpinbox.aspx\" target=\"_blank\">查看详情</a> <a href=\"###\" onclick=\"document.getElementById('frmnewpm').submit();\">不再提示</a></span>\r\n");
	templateBuilder.Append("	<h3>您有 " + newpmcount.ToString() + " 条新的短消息</h3>\r\n");
	templateBuilder.Append("	<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">\r\n");

	int pm__loop__id=0;
	foreach(PrivateMessageInfo pm in pmlist)
	{
		pm__loop__id++;

	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td style=\"width:53px;text-align:center;\"><img src=\"templates/" + templatepath.ToString() + "/images/message_" + pm.New.ToString().Trim() + ".gif\" alt=\"短信息\"/></td>\r\n");
	templateBuilder.Append("			<th><a href=\"usercpshowpm.aspx?pmid=" + pm.Pmid.ToString().Trim() + "\">" + pm.Subject.ToString().Trim() + "</a></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<a href=\"userinfo.aspx?userid=" + pm.Msgfromid.ToString().Trim() + "\" target=\"_blank\">" + pm.Msgfrom.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("				<span class=\"fontfamily\">" + pm.Postdatetime.ToString().Trim() + "</span>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	<form id=\"frmnewpm\" name=\"frmnewpm\" method=\"post\" action=\"#\">\r\n");
	templateBuilder.Append("		<input id=\"ignore\" name=\"ignore\" type=\"hidden\" value=\"yes\" />\r\n");
	templateBuilder.Append("	</form>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<!--短信息 area end-->\r\n");

	}	//end if



	templateBuilder.Append("<!--topic-->\r\n");
	int lastforumlayer = -1;
	
	int lastcolcount = 1;
	
	int lastforumid = 0;
	
	int subforumcount = 0;
	

	int forum__loop__id=0;
	foreach(IndexPageForumInfo forum in forumlist)
	{
		forum__loop__id++;


	if (forum.Layer==0)
	{


	if (lastforumlayer>-1)
	{


	if (lastcolcount!=1)
	{


	if (subforumcount!=0)
	{

	for (int i = 0; i < lastcolcount-subforumcount; i++)
	{
		templateBuilder.Append("<td>&nbsp;</td>");
	}

	templateBuilder.Append("		</tr>\r\n");

	}	//end if

	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("</div>\r\n");

	}
	else
	{

	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("	</div>			\r\n");

	}	//end if

	templateBuilder.Append("<div id=\"ad_intercat_" + lastforumid.ToString() + "\"></div>\r\n");

	}	//end if


	if (forum.Colcount==1)
	{

	templateBuilder.Append("<div class=\"mainbox forumlist\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\">\r\n");

	if (forum.Moderators!="")
	{

	templateBuilder.Append("			分类版主: " + forum.Moderators.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("<img id=\"category_" + forum.Fid.ToString().Trim() + "_img\"  src=\"\r\n");

	if (forum.Collapse!="")
	{

	templateBuilder.Append("		templates/" + templatepath.ToString() + "/images/collapsed_yes.gif\r\n");

	}
	else
	{

	templateBuilder.Append("		templates/" + templatepath.ToString() + "/images/collapsed_no.gif\r\n");

	}	//end if

	templateBuilder.Append("		\" alt=\"展开/收起\" onclick=\"toggle_collapse('category_" + forum.Fid.ToString().Trim() + "');\"/>\r\n");
	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<h3>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(forum.Fid,0);
	
	templateBuilder.Append("		<a href=\"" + aspxrewriteurl.ToString() + "\">" + forum.Name.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("	</h3>	\r\n");
	templateBuilder.Append("	<table id=\"category_" + forum.Fid.ToString().Trim() + "\" summary=\"category_" + forum.Fid.ToString().Trim() + "\" cellspacing=\"0\" cellpadding=\"0\"  style=\"" + forum.Collapse.ToString().Trim() + "\">\r\n");
	templateBuilder.Append("	<thead class=\"category\">\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>版块</th>\r\n");
	templateBuilder.Append("			<td class=\"nums\">主题</td>\r\n");
	templateBuilder.Append("			<td class=\"nums\">帖子</td>\r\n");
	templateBuilder.Append("			<td class=\"lastpost\">最后发表</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</thead>\r\n");

	}
	else
	{

	 subforumcount = 0;
	
	templateBuilder.Append("<div class=\"mainbox forumlist\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\">\r\n");

	if (forum.Moderators!="")
	{

	templateBuilder.Append("			分类版主: " + forum.Moderators.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("		<img id=\"category_" + forum.Fid.ToString().Trim() + "_img\"  src=\"templates/" + templatepath.ToString() + "/images/collapsed_no.gif\" alt=\"展开/收起\" onclick=\"toggle_collapse('category_" + forum.Fid.ToString().Trim() + "');\"/>\r\n");
	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<h3>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(forum.Fid,0);
	
	templateBuilder.Append("		<a href=\"" + aspxrewriteurl.ToString() + "\">" + forum.Name.ToString().Trim() + "</a>					\r\n");
	templateBuilder.Append("	</h3>\r\n");
	templateBuilder.Append("	<table id=\"category_" + forum.Fid.ToString().Trim() + "\" summary=\"category_" + forum.Fid.ToString().Trim() + "\" cellspacing=\"0\" cellpadding=\"0\"  style=\"" + forum.Collapse.ToString().Trim() + "\">	\r\n");

	}	//end if

	 lastforumlayer = 0;
	
	 lastcolcount = forum.Colcount;
	
	 lastforumid = forum.Fid;
	

	}
	else
	{


	if (forum.Colcount==1)
	{

	templateBuilder.Append("		<tbody id=\"forum" + forum.Fid.ToString().Trim() + "\">\r\n");
	templateBuilder.Append("			<tr>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(forum.Lasttid,0);
	
	templateBuilder.Append("				<th \r\n");

	if (forum.Havenew=="new")
	{

	templateBuilder.Append("class=\"new\"\r\n");

	}	//end if

	templateBuilder.Append(">\r\n");

	if (forum.Icon!="")
	{

	templateBuilder.Append("					<img src=\"" + forum.Icon.ToString().Trim() + "\" border=\"0\" align=\"left\" hspace=\"5\" alt=\"" + forum.Name.ToString().Trim() + "\"/>\r\n");

	}	//end if

	templateBuilder.Append("					<h2>\r\n");

	if (forum.Redirect=="")
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(forum.Fid,0);
	
	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\">\r\n");

	}
	else
	{

	templateBuilder.Append("						<a href=\"" + forum.Redirect.ToString().Trim() + "\" target=\"_blank\">\r\n");

	}	//end if

	templateBuilder.Append("					" + forum.Name.ToString().Trim() + "</a>\r\n");

	if (forum.Todayposts>0)
	{

	templateBuilder.Append("<em>(" + forum.Todayposts.ToString().Trim() + ")</em>\r\n");

	}	//end if

	templateBuilder.Append("					</h2>\r\n");

	if (forum.Description!="")
	{

	templateBuilder.Append("<p>" + forum.Description.ToString().Trim() + "</p>\r\n");

	}	//end if


	if (forum.Moderators!="")
	{

	templateBuilder.Append("<p class=\"moderators\">版主: " + forum.Moderators.ToString().Trim() + "</p>\r\n");

	}	//end if

	templateBuilder.Append("				</th>\r\n");
	templateBuilder.Append("				<td class=\"nums\">" + forum.Topics.ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\">" + forum.Posts.ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td class=\"lastpost\">\r\n");

	if (forum.Status==-1)
	{

	templateBuilder.Append("				私密版块\r\n");

	}
	else
	{


	if (forum.Lasttid!=0)
	{

	templateBuilder.Append("					<p>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(forum.Lasttid,0);
	
	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\">" + forum.Lasttitle.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("					</p>\r\n");
	templateBuilder.Append("					<div class=\"topicbackwriter\">by\r\n");

	if (forum.Lastposter!="")
	{


	if (forum.Lastposterid==-1)
	{

	templateBuilder.Append("								游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(forum.Lastposterid);
	
	templateBuilder.Append("								<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + forum.Lastposter.ToString().Trim() + "</a>\r\n");

	}	//end if


	}
	else
	{

	templateBuilder.Append("							匿名\r\n");

	}	//end if

	templateBuilder.Append("						- <a href=\"showtopic.aspx?topicid=" + forum.Lasttid.ToString().Trim() + "&page=end#lastpost\" title=\"" + forum.Lastpost.ToString().Trim() + "\"><span><script type=\"text/javascript\">document.write(convertdate('" + forum.Lastpost.ToString().Trim() + "'));</" + "script></span></a>\r\n");
	templateBuilder.Append("					</div>\r\n");

	}
	else
	{

	templateBuilder.Append("					从未\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}
	else
	{

	 subforumcount = subforumcount+1;
	
	double colwidth = 99.9 / forum.Colcount;
	

	if (subforumcount==1)
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");

	}	//end if

	templateBuilder.Append("			<th style=\"width:" + colwidth.ToString() + "%;\"\r\n");

	if (forum.Havenew=="new")
	{

	templateBuilder.Append("class=\"new\"\r\n");

	}	//end if

	templateBuilder.Append(">\r\n");
	templateBuilder.Append("				<h2>\r\n");

	if (forum.Icon!="")
	{

	templateBuilder.Append("					<img src=\"" + forum.Icon.ToString().Trim() + "\" border=\"0\" align=\"left\" hspace=\"5\" alt=\"" + forum.Name.ToString().Trim() + "\"/>\r\n");

	}	//end if


	if (forum.Redirect=="")
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(forum.Fid,0);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\">\r\n");

	}
	else
	{

	templateBuilder.Append("					<a href=\"" + forum.Redirect.ToString().Trim() + "\" target=\"_blank\">\r\n");

	}	//end if

	templateBuilder.Append("				" + forum.Name.ToString().Trim() + "</a>\r\n");

	if (forum.Todayposts>0)
	{

	templateBuilder.Append("				<em>(" + forum.Todayposts.ToString().Trim() + ")</em>\r\n");

	}	//end if

	templateBuilder.Append("				</h2>\r\n");
	templateBuilder.Append("				<p>主题:" + forum.Topics.ToString().Trim() + ", 帖数:" + forum.Posts.ToString().Trim() + "</p>\r\n");

	if (forum.Status==-1)
	{

	templateBuilder.Append("				<p>私密版块</p>\r\n");

	}
	else
	{


	if (forum.Lasttid!=0)
	{

	templateBuilder.Append("						<p>最后: <a href=\"showtopic.aspx?topicid=" + forum.Lasttid.ToString().Trim() + "&page=end#lastpost\" title=\"" + forum.Lasttitle.ToString().Trim() + "\"><span><script type=\"text/javascript\">document.write(convertdate('" + forum.Lastpost.ToString().Trim() + "'));</" + "script></span></a> by \r\n");

	if (forum.Lastposter!="")
	{


	if (forum.Lastposterid==-1)
	{

	templateBuilder.Append("									游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(forum.Lastposterid);
	
	templateBuilder.Append("									<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + forum.Lastposter.ToString().Trim() + "</a>\r\n");

	}	//end if


	}
	else
	{

	templateBuilder.Append("								匿名\r\n");

	}	//end if

	templateBuilder.Append("						</p>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("			</th>\r\n");

	if (subforumcount==forum.Colcount)
	{

	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	 subforumcount = 0;
	

	}	//end if


	}	//end if

	 lastforumlayer = 1;
	
	 lastcolcount = forum.Colcount;
	

	}	//end if


	}	//end loop


	if (lastcolcount!=1 && subforumcount!=0)
	{

	for (int i = 0; i < lastcolcount-subforumcount; i++)
	{
		templateBuilder.Append("<td>&nbsp;</td>");
	}

	templateBuilder.Append("		</tr>\r\n");

	}	//end if

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<!--end topic-->\r\n");


	if (config.Enabletag==1)
	{

	templateBuilder.Append("<!--tag-->\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"cache/tag/closedtags.txt\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"cache/tag/colorfultags.txt\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_tags.js\"></" + "script>	\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/ajax.js\"></" + "script>\r\n");
	templateBuilder.Append("<table cellspacing=\"1\" cellpadding=\"0\" class=\"portalbox\" summary=\"HeadBox\">\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("	<td>\r\n");
	templateBuilder.Append("		<div id=\"hottags\">\r\n");
	templateBuilder.Append("			<h3><a target=\"_blank\" href=\"tags.aspx\">热门标签</a></h3>\r\n");
	templateBuilder.Append("			<ul id=\"forumhottags\">\r\n");
	templateBuilder.Append("				<script type=\"text/javascript\" src=\"cache/hottags_forum_cache_jsonp.txt\" onerror=\"this.onerror=null;getajaxforumhottags();\"></" + "script>\r\n");
	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody> \r\n");
	templateBuilder.Append("</table>\r\n");
	templateBuilder.Append("<!--tag end-->\r\n");

	}	//end if




	if (forumlinkcount>0)
	{

	templateBuilder.Append("<div class=\"box\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\"><img id=\"forumlinks_img\" src=\"templates/" + templatepath.ToString() + "/images/collapsed_no.gif\" alt=\"\" onclick=\"toggle_collapse('linklist');\" /></span>\r\n");
	templateBuilder.Append("	<h4>友情链接</h4>\r\n");
	templateBuilder.Append("	<table id=\"forumlinks\" cellspacing=\"0\" cellpadding=\"0\" style=\"table-layout: fixed;\" summary=\"友情链接\">\r\n");

	int forumlink__loop__id=0;
	foreach(DataRow forumlink in forumlinklist.Rows)
	{
		forumlink__loop__id++;

	templateBuilder.Append("		<tbody>	\r\n");
	templateBuilder.Append("		<tr>	\r\n");

	if (forumlink["logo"].ToString().Trim()!="")
	{

	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<a href=\"" + forumlink["url"].ToString().Trim() + "\" target=\"_blank\"><img src=\"" + forumlink["logo"].ToString().Trim() + "\" alt=\"" + forumlink["name"].ToString().Trim() + "\"  class=\"forumlink_logo\"/></a>\r\n");
	templateBuilder.Append("				<h5><a href=\"" + forumlink["url"].ToString().Trim() + "\" target=\"_blank\">" + forumlink["name"].ToString().Trim() + "</a></h5>\r\n");
	templateBuilder.Append("				<p>" + forumlink["note"].ToString().Trim() + "</p>\r\n");
	templateBuilder.Append("			</td>\r\n");

	}
	else if (forumlink["name"].ToString().Trim()!="$$otherlink$$")
	{

	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<h5>\r\n");
	templateBuilder.Append("					<a href=\"" + forumlink["url"].ToString().Trim() + "\" target=\"_blank\">" + forumlink["name"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("				</h5>\r\n");
	templateBuilder.Append("				<p>" + forumlink["note"].ToString().Trim() + "</p>\r\n");
	templateBuilder.Append("			</td>\r\n");

	}
	else
	{

	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				" + forumlink["note"].ToString().Trim() + "\r\n");
	templateBuilder.Append("			</td>\r\n");

	}	//end if

	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	if (config.Whosonlinestatus!=0 && config.Whosonlinestatus!=2)
	{

	templateBuilder.Append("<div class=\"box\" id=\"online\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\">\r\n");

	if (DNTRequest.GetString("showonline")=="no")
	{

	templateBuilder.Append("			<a href=\"?showonline=yes#online\"><img src=\"templates/" + templatepath.ToString() + "/images/collapsed_yes.gif\" alt=\"展开/收起\" />\r\n");

	}
	else
	{

	templateBuilder.Append("			<a href=\"?showonline=no#online\"><img src=\"templates/" + templatepath.ToString() + "/images/collapsed_no.gif\" alt=\"展开/收起\" />\r\n");

	}	//end if

	templateBuilder.Append("		</a>\r\n");
	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<h4>\r\n");
	templateBuilder.Append("		<strong><a href=\"" + forumurl.ToString() + "onlineuser.aspx\">在线用户</a></strong>- <em>" + totalonline.ToString() + "</em> 人在线 \r\n");

	if (showforumonline)
	{

	templateBuilder.Append("- " + totalonlineuser.ToString() + " 会员<span id=\"invisible\"></span>, " + totalonlineguest.ToString() + " 游客\r\n");

	}	//end if

	templateBuilder.Append("- 最高记录是 <em>" + highestonlineusercount.ToString() + "</em> 于 <em>" + highestonlineusertime.ToString() + "</em>\r\n");
	templateBuilder.Append("	</h4>\r\n");
	templateBuilder.Append("	<dl id=\"onlinelist\">\r\n");
	templateBuilder.Append("		<dt>" + onlineiconlist.ToString() + "</dt>\r\n");
	templateBuilder.Append("		<dd class=\"onlineusernumber\">\r\n");
	templateBuilder.Append("			共<strong>" + totalusers.ToString() + "</strong>位会员 <span class=\"newuser\">新会员:\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(lastuserid);
	
	templateBuilder.Append("			<a href=\"" + aspxrewriteurl.ToString() + "\">" + lastusername.ToString() + "</a></span>\r\n");
	templateBuilder.Append("		</dd>\r\n");
	templateBuilder.Append("		<dd>\r\n");
	templateBuilder.Append("			<ul class=\"userlist\">\r\n");

	if (showforumonline)
	{

	int invisiblecount = 0;
	

	int onlineuser__loop__id=0;
	foreach(OnlineUserInfo onlineuser in onlineuserlist)
	{
		onlineuser__loop__id++;


	if (onlineuser.Invisible==1)
	{

	 invisiblecount = invisiblecount + 1;
	
	templateBuilder.Append("				<li>(隐身会员)</li>\r\n");

	}
	else
	{

	templateBuilder.Append("				<li>" + onlineuser.Olimg.ToString().Trim() + "\r\n");

	if (onlineuser.Userid==-1)
	{

	templateBuilder.Append("							" + onlineuser.Username.ToString().Trim() + "\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(onlineuser.Userid);
	
	templateBuilder.Append("							<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\" title=\"时间: " + onlineuser.Lastupdatetime.ToString().Trim() + "\r\n");
	templateBuilder.Append("操作: " + onlineuser.Actionname.ToString().Trim() + "\r\n");

	if (onlineuser.Forumname!="")
	{

	templateBuilder.Append("版块: " + onlineuser.Forumname.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("	\">" + onlineuser.Username.ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("				</li>\r\n");

	}	//end if


	}	//end loop


	if (invisiblecount>0)
	{

	templateBuilder.Append("					<script type=\"text/javascript\">$('invisible').innerHTML = '(" + invisiblecount.ToString() + "' + \" 隐身)\";</" + "script>\r\n");

	}	//end if


	}
	else
	{

	templateBuilder.Append("				<li style=\"width: auto\"><a href=\"?showonline=yes#online\">点击查看在线列表</a></li>\r\n");

	}	//end if

	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</dd>\r\n");
	templateBuilder.Append("	</dl>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if

	templateBuilder.Append("<div class=\"legend\">\r\n");
	templateBuilder.Append("	<label><img src=\"templates/" + templatepath.ToString() + "/images/forum_new.gif\" alt=\"有新帖的版块\" />有新帖的版块</label>\r\n");
	templateBuilder.Append("	<label><img src=\"templates/" + templatepath.ToString() + "/images/forum.gif\" alt=\"无新帖的版块\" />无新帖的版块</label>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (config.Forumjump==1)
	{

	templateBuilder.Append("	" + navhomemenu.ToString() + "\r\n");

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



	templateBuilder.Append("" + mediaad.ToString() + "\r\n");
	templateBuilder.Append("" + inforumad.ToString() + "\r\n");

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
