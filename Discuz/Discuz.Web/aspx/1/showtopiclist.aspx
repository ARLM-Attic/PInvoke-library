<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.showtopiclist" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:05:39.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:05:39. 
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



	if (page_err==0)
	{

	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/ajax.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("var templatepath = \"" + templatepath.ToString() + "\";\r\n");
	templateBuilder.Append("var fid = parseInt(" + forum.Fid.ToString().Trim() + ");\r\n");
	templateBuilder.Append("</" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_showforum.js\"></" + "script>\r\n");
	templateBuilder.Append("<div id=\"foruminfo\">\r\n");
	templateBuilder.Append("	<div id=\"nav\">\r\n");
	templateBuilder.Append("		<a href=\"" + config.Forumurl.ToString().Trim() + "\">" + config.Forumtitle.ToString().Trim() + "</a> &raquo; " + forumnav.ToString() + "\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div id=\"forumstats\">\r\n");
	templateBuilder.Append("		<p>\r\n");

	if (forumid==-1)
	{

	templateBuilder.Append("			<a href=\"forumindex.aspx\">全部</a>\r\n");
	templateBuilder.Append("			<a href=\"showtopiclist.aspx?type=digest&amp;forums=" + forums.ToString() + "\">精华帖区</a>\r\n");

	}
	else
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(forumid,0);
	
	templateBuilder.Append("			<a href=\"" + aspxrewriteurl.ToString() + "\">全部</a>\r\n");
	templateBuilder.Append("			<a href=\"showtopiclist.aspx?forumid=" + forumid.ToString() + "&type=digest\">精华帖区</a>\r\n");

	}	//end if


	if (config.Rssstatus!=0)
	{

	templateBuilder.Append("			<a href=\"tools/rss.aspx\" target=\"_blank\"><img src=\"templates/" + templatepath.ToString() + "/images/rss.gif\" alt=\"Rss\"/></a>\r\n");

	}	//end if

	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (showforumlogin==1)
	{

	templateBuilder.Append("	<div class=\"mainbox formbox\">\r\n");
	templateBuilder.Append("		<h3>本版块已经被管理员设置了密码</h3>\r\n");
	templateBuilder.Append("		<form id=\"forumlogin\" name=\"forumlogin\" method=\"post\" action=\"\">\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<th><label for=\"forumpassword\">请输入密码</label></th>\r\n");
	templateBuilder.Append("					<td><input name=\"forumpassword\" type=\"password\" id=\"forumpassword\" size=\"20\"/></td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");

	if (isseccode)
	{

	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<th><label for=\"vcode\">输入验证码</label></th>\r\n");
	templateBuilder.Append("					<td>\r\n");

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


	templateBuilder.Append("</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");

	}	//end if

	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<th>&nbsp;</th>\r\n");
	templateBuilder.Append("					<td>\r\n");
	templateBuilder.Append("						<input type=\"submit\"  value=\"确定\"/>\r\n");
	templateBuilder.Append("					</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</form>\r\n");
	templateBuilder.Append("	</div>\r\n");

	}
	else
	{

	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "		\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"mainbox threadlist\">\r\n");
	templateBuilder.Append("	<h3>\r\n");

	if (forumid>0)
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(forum.Fid,0);
	
	templateBuilder.Append("		<a href=\"" + aspxrewriteurl.ToString() + "\">" + forum.Name.ToString().Trim() + "</a>\r\n");

	}
	else if (type=="digest")
	{

	templateBuilder.Append("			精华帖\r\n");

	}
	else
	{

	templateBuilder.Append("			新帖\r\n");

	}	//end if

	templateBuilder.Append("	</h3>\r\n");
	templateBuilder.Append("	<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" summary=\"帖子\">\r\n");
	templateBuilder.Append("		<thead class=\"category\">\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td class=\"folder\">&nbsp;</td>\r\n");
	templateBuilder.Append("				<td class=\"icon\">&nbsp;</td>\r\n");
	templateBuilder.Append("				<th>标题</th>\r\n");
	templateBuilder.Append("				<td class=\"author\">作者</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\">回复/查看</td>\r\n");
	templateBuilder.Append("				<td class=\"lastpost\">最后发表</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");
	templateBuilder.Append("	<!--announcement area start-->\r\n");

	int announcement__loop__id=0;
	foreach(DataRow announcement in announcementlist.Rows)
	{
		announcement__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td style=\"line-height:28px;\"><img src=\"templates/" + templatepath.ToString() + "/images/announcement.gif\" alt=\"announcement\"/></td>\r\n");
	templateBuilder.Append("			<td>&nbsp;</td>\r\n");
	templateBuilder.Append("			<th><a href=\"announcement.aspx#" + announcement["id"].ToString().Trim() + "\">" + announcement["title"].ToString().Trim() + "</a></th>\r\n");
	templateBuilder.Append("			<td colspan=\"3\">\r\n");

	if (Utils.StrToInt(announcement["posterid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("					游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(announcement["posterid"].ToString().Trim());
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\">" + announcement["poster"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	<!--announcement area end-->\r\n");
	templateBuilder.Append("	<!--showtopiclist start-->\r\n");

	int topic__loop__id=0;
	foreach(ShowforumPageTopicInfo topic in topiclist)
	{
		topic__loop__id++;

	templateBuilder.Append("		<tbody id=\"normalthread_" + topic.Tid.ToString().Trim() + "\" >\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td class=\"folder\">\r\n");

	if (topic.Folder!="")
	{

	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	
	templateBuilder.Append("				<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\"><img src=\"templates/" + templatepath.ToString() + "/images/folder_" + topic.Folder.ToString().Trim() + ".gif\" alt=\"主题图标\"/></a>\r\n");

	}
	else
	{

	templateBuilder.Append("				&nbsp;\r\n");

	}	//end if

	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("			<td class=\"icon\">\r\n");

	if (topic.Iconid!=0)
	{

	templateBuilder.Append("					<img src=\"images/posticons/" + topic.Iconid.ToString().Trim() + ".gif\" alt=\"示图\"/>\r\n");

	}
	else
	{

	templateBuilder.Append("					&nbsp;\r\n");

	}	//end if

	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("			<th class=\"common\">	\r\n");
	templateBuilder.Append("				<label>\r\n");

	if (topic.Digest>0)
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/digest" + topic.Digest.ToString().Trim() + ".gif\" alt=\"digtest\"/>\r\n");

	}	//end if


	if (topic.Special==1)
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/pollsmall.gif\" alt=\"投票\" />\r\n");

	}	//end if


	if (topic.Special==2 || topic.Special==3)
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/bonus.gif\" alt=\"悬赏\"/>\r\n");

	}	//end if


	if (topic.Special==4)
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/debatesmall.gif\" alt=\"辩论\"/>\r\n");

	}	//end if


	if (topic.Attachment>0)
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/attachment.gif\" alt=\"附件\"/>\r\n");

	}	//end if

	templateBuilder.Append("				</label>\r\n");
	templateBuilder.Append("				<span>\r\n");

	if (topic.Replies>0)
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/topItem_exp.gif\" id=\"imgButton_" + topic.Tid.ToString().Trim() + "\" onclick=\"showtree(" + topic.Tid.ToString().Trim() + "," + config.Ppp.ToString().Trim() + ");\" class=\"cursor\" alt=\"展开帖子列表\" title=\"展开帖子列表\" />\r\n");

	}
	else
	{

	templateBuilder.Append("					<img src=\"templates/" + templatepath.ToString() + "/images/no-sublist.gif\" id=\"imgButton_" + topic.Tid.ToString().Trim() + "\"/>\r\n");

	}	//end if

	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	
	templateBuilder.Append("				<a href=\"" + aspxrewriteurl.ToString() + "\">" + topic.Title.ToString().Trim() + "</a>\r\n");

	if (topic.Replies/config.Ppp>0)
	{

	templateBuilder.Append("					<script type=\"text/javascript\">getpagenumbers(\"" + config.Extname.ToString().Trim() + "\", " + topic.Replies.ToString().Trim() + "," + config.Ppp.ToString().Trim() + ",0,\"\"," + topic.Tid.ToString().Trim() + ",\"\",\"\"," + config.Aspxrewrite.ToString().Trim() + ");</" + "script>\r\n");

	}	//end if

	templateBuilder.Append("				</span>\r\n");
	templateBuilder.Append("			</th>\r\n");
	templateBuilder.Append("			<td class=\"author\">\r\n");
	templateBuilder.Append("				<cite>\r\n");

	if (topic.Posterid==-1)
	{

	templateBuilder.Append("					游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(topic.Posterid);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\">" + topic.Poster.ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("</cite>\r\n");
	templateBuilder.Append("				<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(topic.Postdatetime).ToString("yy-MM-dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("			<td class=\"nums\"><em>" + topic.Replies.ToString().Trim() + "</em> / " + topic.Views.ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("			<td class=\"lastpost\">\r\n");
	templateBuilder.Append("					<em><a href=\"showtopic.aspx?topicid=" + topic.Tid.ToString().Trim() + "&page=end#lastpost\">\r\n");
	templateBuilder.Append(Convert.ToDateTime(topic.Lastpost).ToString("yy-MM-dd HH:mm"));
	templateBuilder.Append("</a></em>\r\n");
	templateBuilder.Append("					<cite>\r\n");

	if (topic.Lastposterid==-1)
	{

	templateBuilder.Append("						游客\r\n");

	}
	else
	{

	templateBuilder.Append("						<a href=\"" + UserInfoAspxRewrite(topic.Lastposterid).ToString().Trim() + "\" target=\"_blank\">" + topic.Lastposter.ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("					</cite>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td colspan=\"5\"><div id=\"divTopic" + topic.Tid.ToString().Trim() + "\"></div></td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	<!--showtopiclist end-->\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "		\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("function CheckAll(form)\r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("	for (var i = 0; i < form.elements.length; i++)\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		var e = form.elements[i];\r\n");
	templateBuilder.Append("		if (e.id == \"fidlist\"){\r\n");
	templateBuilder.Append("		   e.checked = form.chkall.checked;\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("function SH_SelectOne(obj)\r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("	for (var i = 0; i < document.getElementById(\"LookBySearch\").elements.length; i++)\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		var e = document.getElementById(\"LookBySearch\").elements[i];\r\n");
	templateBuilder.Append("		if (e.id == \"fidlist\"){\r\n");
	templateBuilder.Append("		   if (!e.checked){\r\n");
	templateBuilder.Append("			document.getElementById(\"chkall\").checked = false;\r\n");
	templateBuilder.Append("			return;\r\n");
	templateBuilder.Append("		   }\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("	document.getElementById(\"chkall\").checked = true;\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("function ShowDetailGrid(tid)\r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("   if(" + config.Aspxrewrite.ToString().Trim() + ")\r\n");
	templateBuilder.Append("   {\r\n");
	templateBuilder.Append("       window.location.href = \"showforum-\" + tid + \"" + config.Extname.ToString().Trim() + "\";\r\n");
	templateBuilder.Append("   }\r\n");
	templateBuilder.Append("   else\r\n");
	templateBuilder.Append("   {\r\n");
	templateBuilder.Append("       window.location.href = \"showforum.aspx?forumid=\" + tid ;\r\n");
	templateBuilder.Append("   }\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("</" + "script>\r\n");

	if (forumid==-1)
	{

	templateBuilder.Append("<form name=\"LookBySearch\" method=\"post\" action=\"showtopiclist.aspx?search=1&forumid=" + forumid.ToString() + "&type=" + type.ToString() + "&newtopic=" + newtopic.ToString() + "&forums=" + forums.ToString() + "\" ID=\"LookBySearch\">\r\n");
	templateBuilder.Append("<div class=\"box\" style=\"padding-bottom:0;\">\r\n");
	templateBuilder.Append("	<h4>以下论坛版块:</h4>\r\n");
	templateBuilder.Append("	<table width=\"100%\" border=\"0\" cellspacing=\"3\" cellpadding=\"0\">\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			 " + forumcheckboxlist.ToString() + "\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	<div style=\"padding:6px 0; border:none; border-top:1px solid #CCC; background:#EEE;\">\r\n");
	templateBuilder.Append("		<span style=\"float:right;\">\r\n");
	templateBuilder.Append("			排序方式\r\n");
	templateBuilder.Append("			<select name=\"order\" id=\"order\">\r\n");
	templateBuilder.Append("			  <option value=\"1\" \r\n");

	if (order==1)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">最后回复时间</option>\r\n");
	templateBuilder.Append("			  <option value=\"2\" \r\n");

	if (order==2)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">发布时间</option>\r\n");
	templateBuilder.Append("			</select>	\r\n");
	templateBuilder.Append("			<select name=\"direct\" id=\"direct\">\r\n");
	templateBuilder.Append("			  <option value=\"0\" \r\n");

	if (direct==0)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">按升序排列</option>\r\n");
	templateBuilder.Append("			  <option value=\"1\" \r\n");

	if (direct==1)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">按降序排列</option>\r\n");
	templateBuilder.Append("			</select>\r\n");
	templateBuilder.Append("			<button type=\"submit\" onclick=\"document.LookBySearch.submit();\">提交</button>\r\n");
	templateBuilder.Append("		</span>\r\n");
	templateBuilder.Append("		<input checked=\"checked\" title=\"选中/取消选中 本页所有Case\" onclick=\"CheckAll(this.form)\" type=\"checkbox\" name=\"chkall\" id=\"chkall\">全选/取消全选\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</form>\r\n");
	templateBuilder.Append("<div id=\"footfilter\" class=\"box\">\r\n");

	if (config.Forumjump==1)
	{

	templateBuilder.Append("    <select onchange=\"if(this.options[this.selectedIndex].value != '') { jumpurl(this.options[this.selectedIndex].value," + config.Aspxrewrite.ToString().Trim() + ",'" + config.Extname.ToString().Trim() + "');}\">\r\n");
	templateBuilder.Append("		<option>论坛跳转...</option>\r\n");
	templateBuilder.Append("		" + forumlistboxoptions.ToString() + "\r\n");
	templateBuilder.Append("	</select>\r\n");

	}	//end if


	if (config.Visitedforums>0)
	{

	templateBuilder.Append("    <select name=\"select2\" onchange=\"if(this.options[this.selectedIndex].value != '') {jumpurl(this.options[this.selectedIndex].value," + config.Aspxrewrite.ToString().Trim() + ",'" + config.Extname.ToString().Trim() + "');}\">\r\n");
	templateBuilder.Append("		<option>最近访问...</option>" + visitedforumsoptions.ToString() + "\r\n");
	templateBuilder.Append("	</select>\r\n");

	}	//end if

	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	var categorydata = " + goodscategoryfid.ToString() + ";		\r\n");
	templateBuilder.Append("	function jumpurl(fid, aspxrewrite, extname) {\r\n");
	templateBuilder.Append("		for(var i in categorydata) {\r\n");
	templateBuilder.Append("		   if(categorydata[i].fid == fid) {\r\n");
	templateBuilder.Append("			   if(aspxrewrite) {\r\n");
	templateBuilder.Append("				   window.location='showgoodslist-' +categorydata[i].categoryid + extname;\r\n");
	templateBuilder.Append("			   }\r\n");
	templateBuilder.Append("			   else {\r\n");
	templateBuilder.Append("				   window.location='showgoodslist.aspx?categoryid=' +categorydata[i].categoryid;\r\n");
	templateBuilder.Append("			   }\r\n");
	templateBuilder.Append("			   return;\r\n");
	templateBuilder.Append("		   } \r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("		if(aspxrewrite) {\r\n");
	templateBuilder.Append("			window.location='showforum-' + fid + extname;\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("		else {\r\n");
	templateBuilder.Append("			window.location='showforum.aspx?forumid=' + fid ;\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("	</" + "script>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	}	//end if

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


	templateBuilder.Append("</div>\r\n");

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
