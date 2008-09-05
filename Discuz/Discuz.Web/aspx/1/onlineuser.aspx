<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.onlineuser" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008-7-4 12:33:30.
		本页面代码由Discuz!NT模板引擎生成于 2008-7-4 12:33:30. 
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
	templateBuilder.Append("	<div id=\"nav\">\r\n");
	templateBuilder.Append("		<a href=\"" + config.Forumurl.ToString().Trim() + "\">" + config.Forumtitle.ToString().Trim() + "</a> &raquo; <strong>在线用户列表</strong>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div id=\"headsearch\">\r\n");
	templateBuilder.Append("		<div id=\"search\">\r\n");

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


	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>共" + totalonline.ToString() + "人在线</em> - <strong>" + totalonlineuser.ToString() + "位会员</strong> \r\n");

	if (totalonlineinvisibleuser>0)
	{

	templateBuilder.Append("		(" + totalonlineinvisibleuser.ToString() + "隐身)\r\n");
	templateBuilder.Append("		,\r\n");

	}	//end if

	templateBuilder.Append(" " + totalonlineguest.ToString() + " 位游客 | 最高纪录是 " + highestonlineusercount.ToString() + " 于 " + highestonlineusertime.ToString() + "\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"mainbox\">\r\n");
	templateBuilder.Append("	<h3>在线用户列表</h3>\r\n");
	templateBuilder.Append("	<table summary=\"在线用户列表\" cellspacing=\"0\" cellpadding=\"0\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("			<th>&nbsp;</th>\r\n");
	templateBuilder.Append("			<th>用户名</th>\r\n");
	templateBuilder.Append("			<th>时间</th>\r\n");
	templateBuilder.Append("			<th>当前动作</th>\r\n");
	templateBuilder.Append("			<th>所在论坛</th>\r\n");
	templateBuilder.Append("			<th>所在主题</th>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	int onlineuserinfo__loop__id=0;
	foreach(DataRow onlineuserinfo in onlineuserlist.Rows)
	{
		onlineuserinfo__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td><img src=\"templates/" + templatepath.ToString() + "/images/member.gif\" alt=\"用户\" /></td>\r\n");
	templateBuilder.Append("				<td>\r\n");

	if (onlineuserinfo["userid"].ToString().Trim()=="-1")
	{

	templateBuilder.Append("						 游客\r\n");

	}
	else
	{


	if (onlineuserinfo["invisible"].ToString().Trim()=="1")
	{

	templateBuilder.Append("						 (隐身用户)\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(onlineuserinfo["userid"].ToString().Trim());
	
	templateBuilder.Append("						 <a href=\"" + aspxrewriteurl.ToString() + "\">" + onlineuserinfo["username"].ToString().Trim() + "</a>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td>" + onlineuserinfo["lastupdatetime"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td>" + onlineuserinfo["action"].ToString().Trim() + "&nbsp;</td>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(onlineuserinfo["forumid"].ToString().Trim(),0);
	
	templateBuilder.Append("				<td><a href=\"" + aspxrewriteurl.ToString() + "\">" + onlineuserinfo["forumname"].ToString().Trim() + "</a>&nbsp;</td>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(onlineuserinfo["topicid"].ToString().Trim(),0);
	
	templateBuilder.Append("				<td><a href=\"" + aspxrewriteurl.ToString() + "\">" + onlineuserinfo["title"].ToString().Trim() + "</a>&nbsp;</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>共" + onlineusernumber.ToString() + "名用户</em><strong>" + pagecount.ToString() + "页</strong>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) {\r\n");
	templateBuilder.Append("	window.location='onlineuser.aspx?page='+this.value;}\"  size=\"4\" maxlength=\"9\" class=\"colorblue2\"/>页\r\n");
	templateBuilder.Append("		</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
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
	templateBuilder.Append("			Powered by <strong><a href=\"http://nt.discuz.net\" target=\"_blank\" title=\"Discuz!NT 2.5 (.NET Framework 2.0/3.x)\">Discuz!NT</a></strong> <em>2.5.0</em>&nbsp;beta\r\n");

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
