<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.stats" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:05:10.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:05:10. 
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


	templateBuilder.Append("<!--header end-->\r\n");
	templateBuilder.Append("	<div id=\"foruminfo\">\r\n");
	templateBuilder.Append("		<div id=\"nav\">\r\n");
	templateBuilder.Append("			<a href=\"" + config.Forumurl.ToString().Trim() + "\" class=\"home\">" + config.Forumtitle.ToString().Trim() + "</a>  &raquo; <a href=\"stats.aspx\">统计</a>  &raquo; <strong>\r\n");

	if (type=="")
	{

	templateBuilder.Append("		基本概况\r\n");

	}
	else if (type=="views")
	{

	templateBuilder.Append("		流量统计\r\n");

	}
	else if (type=="client")
	{

	templateBuilder.Append("		客户软件\r\n");

	}
	else if (type=="posts")
	{

	templateBuilder.Append("		发帖量记录\r\n");

	}
	else if (type=="forumsrank")
	{

	templateBuilder.Append("		版块排行\r\n");

	}
	else if (type=="topicsrank")
	{

	templateBuilder.Append("		主题排行\r\n");

	}
	else if (type=="postsrank")
	{

	templateBuilder.Append("		发帖排行\r\n");

	}
	else if (type=="creditsrank")
	{

	templateBuilder.Append("		积分排行\r\n");

	}
	else if (type=="onlinetime")
	{

	templateBuilder.Append("		在线时间\r\n");

	}
	else if (type=="trade")
	{

	templateBuilder.Append("		交易排行\r\n");

	}
	else if (type=="team")
	{

	templateBuilder.Append("		管理团队\r\n");

	}
	else if (type=="modworks")
	{

	templateBuilder.Append("		管理统计\r\n");

	}	//end if

	templateBuilder.Append("</strong>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		function changeTab(obj)\r\n");
	templateBuilder.Append("		{\r\n");
	templateBuilder.Append("			if (obj.className == 'currenttab')\r\n");
	templateBuilder.Append("			{\r\n");
	templateBuilder.Append("				obj.className = '';\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("			else\r\n");
	templateBuilder.Append("			{\r\n");
	templateBuilder.Append("				obj.className = 'currenttab';\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	</" + "script>\r\n");
	templateBuilder.Append("	<div class=\"statstab\">\r\n");
	templateBuilder.Append("		<a id=\"tab_main\" class=\"currenttab\" onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\" href=\"stats.aspx\">基本状况</a>\r\n");

	if (statstatus)
	{

	templateBuilder.Append("		<a id=\"tab_views\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=views\">流量统计</a>\r\n");
	templateBuilder.Append("		<a id=\"tab_client\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=client\">客户软件</a>\r\n");

	}	//end if

	templateBuilder.Append("		<a id=\"tab_posts\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=posts\">发帖量记录</a>\r\n");
	templateBuilder.Append("		<a id=\"tab_forumsrank\"   onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=forumsrank\">版块排行</a>\r\n");
	templateBuilder.Append("		<a id=\"tab_topicsrank\"   onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=topicsrank\">主题排行</a>\r\n");
	templateBuilder.Append("		<a id=\"tab_postsrank\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=postsrank\">发帖排行</a>\r\n");
	templateBuilder.Append("		<a id=\"tab_creditsrank\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=creditsrank\">积分排行</a>\r\n");
	templateBuilder.Append("		<!--\r\n");
	templateBuilder.Append("		<a id=\"tab_trade\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=trade\">交易排行</a>\r\n");
	templateBuilder.Append("		-->\r\n");

	if (config.Oltimespan>0)
	{

	templateBuilder.Append("		<a id=\"tab_onlinetime\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=onlinetime\">在线时间</a>\r\n");

	}	//end if

	templateBuilder.Append("		<!--\r\n");
	templateBuilder.Append("		<a id=\"tab_team\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\"  href=\"?type=team\">管理团队</a>\r\n");
	templateBuilder.Append("		<a id=\"tab_modworks\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" style=\"cursor: pointer;\" href=\"?type=modworks\">管理统计</a>\r\n");
	templateBuilder.Append("		-->\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	try{\r\n");
	templateBuilder.Append("		$(\"tab_main\").className = \"\";\r\n");
	templateBuilder.Append("		$(\"tab_\" + '" + type.ToString() + "').className = \"currenttab\";\r\n");
	templateBuilder.Append("	}catch(e){\r\n");
	templateBuilder.Append("		$(\"tab_main\").className = \"currenttab\";\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("	</" + "script>\r\n");

	if (page_err==0)
	{


	if (type=="")
	{

	templateBuilder.Append("		<div class=\"mainbox viewsstats\">\r\n");
	templateBuilder.Append("			<h3>基本状况</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">注册会员</td>\r\n");
	templateBuilder.Append("					<td>" + members.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">发帖会员</td>\r\n");
	templateBuilder.Append("					<td>" + mempost.ToString() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">管理成员</td>\r\n");
	templateBuilder.Append("					<td>" + admins.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">未发帖会员</td>\r\n");
	templateBuilder.Append("					<td>" + memnonpost.ToString() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">新会员</td>\r\n");
	templateBuilder.Append("					<td>" + lastmember.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">发帖会员占总数</td>\r\n");
	templateBuilder.Append("					<td>" + mempostpercent.ToString() + "%</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">今日论坛之星</td>\r\n");
	templateBuilder.Append("					<td>\r\n");

	if (bestmem!="")
	{

	templateBuilder.Append("<a href=\"userinfo.aspx?username=" + bestmem.ToString() + "\">" + bestmem.ToString() + "</a>(" + bestmemposts.ToString() + ")\r\n");

	}	//end if

	templateBuilder.Append("</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">平均每人发帖数</td>\r\n");
	templateBuilder.Append("					<td>" + mempostavg.ToString() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>	\r\n");
	templateBuilder.Append("		<div class=\"mainbox viewsstats\">\r\n");
	templateBuilder.Append("			<h3>论坛统计</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">版块数</td>\r\n");
	templateBuilder.Append("					<td style=\"width:15%\">" + forums.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">平均每日新增帖子数</td>\r\n");
	templateBuilder.Append("					<td style=\"width:15%\">" + postsaddavg.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">最热门版块</td>\r\n");
	templateBuilder.Append("					<td><a href=\"" + ShowForumAspxRewrite(hotforum.Fid,0).ToString().Trim() + "\" target=\"_blank\">" + hotforum.Name.ToString().Trim() + "</a></td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">主题数</td>\r\n");
	templateBuilder.Append("					<td>" + topics.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">平均每日注册会员数</td>\r\n");
	templateBuilder.Append("					<td>" + membersaddavg.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">主题数</td>\r\n");
	templateBuilder.Append("					<td>" + hotforum.Topics.ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">帖子数</td>\r\n");
	templateBuilder.Append("					<td>" + posts.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">最近24小时新增帖子数</td>\r\n");
	templateBuilder.Append("					<td>" + postsaddtoday.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">帖子数</td>\r\n");
	templateBuilder.Append("					<td>" + hotforum.Posts.ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">平均每个主题被回复次数</td>\r\n");
	templateBuilder.Append("					<td>" + topicreplyavg.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">今日新增会员数</td>\r\n");
	templateBuilder.Append("					<td>" + membersaddtoday.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">论坛活跃指数</td>\r\n");
	templateBuilder.Append("					<td>" + activeindex.ToString() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	if (statstatus)
	{

	templateBuilder.Append("		<div class=\"mainbox viewsstats\">\r\n");
	templateBuilder.Append("			<h3>流量概况</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">总页面流量</td>\r\n");
	templateBuilder.Append("					<td>" + totalstats["hits"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">访问量最多的月份</td>\r\n");
	templateBuilder.Append("					<td>" + yearofmaxmonth.ToString() + " 年 " + monthofmaxmonth.ToString() + " 月</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">共计来访</td>\r\n");
	templateBuilder.Append("					<td>" + totalstats["visitors"].ToString().Trim() + " 人次</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">月份总页面流量</td>\r\n");
	templateBuilder.Append("					<td>" + maxmonth.ToString() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">会员</td>\r\n");
	templateBuilder.Append("					<td>" + totalstats["members"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">时段</td>\r\n");
	templateBuilder.Append("					<td>" + maxhourfrom.ToString() + " - " + maxhourto.ToString() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">游客</td>\r\n");
	templateBuilder.Append("					<td>" + totalstats["guests"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">时段总页面流量</td>\r\n");
	templateBuilder.Append("					<td>" + maxhour.ToString() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">平均每人浏览</td>\r\n");
	templateBuilder.Append("					<td>" + pageviewavg.ToString() + "</td>\r\n");
	templateBuilder.Append("					<td class=\"statsitem\">&nbsp;</td>\r\n");
	templateBuilder.Append("					<td>&nbsp;</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if

	templateBuilder.Append("		<div class=\"mainbox viewsstats\">\r\n");
	templateBuilder.Append("			<h3>月份流量</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");

	if (statstatus)
	{

	templateBuilder.Append("					" + monthofstatsbar.ToString() + "\r\n");

	}
	else
	{

	templateBuilder.Append("					<thead>\r\n");
	templateBuilder.Append("						<td colspan=\"2\">每月新增帖子记录</td>\r\n");
	templateBuilder.Append("					</thead>\r\n");
	templateBuilder.Append("					" + monthpostsofstatsbar.ToString() + "\r\n");
	templateBuilder.Append("					<thead>\r\n");
	templateBuilder.Append("						<td colspan=\"2\">每日新增帖子记录</td>\r\n");
	templateBuilder.Append("					</thead>\r\n");
	templateBuilder.Append("					" + daypostsofstatsbar.ToString() + "\r\n");

	}	//end if

	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (type=="views")
	{

	templateBuilder.Append("		<div class=\"mainbox viewsstats\">\r\n");
	templateBuilder.Append("			<h3>流量统计</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td colspan=\"2\">星期流量</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				" + weekofstatsbar.ToString() + "\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td colspan=\"2\">时段流量</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				" + hourofstatsbar.ToString() + "\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (type=="client")
	{

	templateBuilder.Append("		<div class=\"mainbox viewsstats\">\r\n");
	templateBuilder.Append("			<h3>客户软件</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td colspan=\"2\">操作系统</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				" + osofstatsbar.ToString() + "\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td colspan=\"2\">浏览器</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				" + browserofstatsbar.ToString() + "\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (type=="posts")
	{

	templateBuilder.Append("		<div class=\"mainbox viewsstats\">\r\n");
	templateBuilder.Append("			<h3>发帖量记录</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td colspan=\"2\">每月新增帖子记录</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				" + monthpostsofstatsbar.ToString() + "\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td colspan=\"2\">每日新增帖子记录</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				" + daypostsofstatsbar.ToString() + "\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (type=="forumsrank")
	{

	templateBuilder.Append("		<div class=\"mainbox topicstats\">\r\n");
	templateBuilder.Append("			<h3>版块排行</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td width=\"25%\">发帖 排行榜</td>\r\n");
	templateBuilder.Append("						<td width=\"25%\">回复 排行榜</td>\r\n");
	templateBuilder.Append("						<td width=\"25%\">最近 30 天发帖 排行榜</td>\r\n");
	templateBuilder.Append("						<td width=\"25%\">最近 24 小时发帖 排行榜</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td><ul>" + topicsforumsrank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("						<td><ul>" + postsforumsrank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("						<td><ul>" + thismonthforumsrank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("						<td><ul>" + todayforumsrank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (type=="topicsrank")
	{

	templateBuilder.Append("		<div class=\"mainbox topicstats\">\r\n");
	templateBuilder.Append("			<h3>主题排行</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td width=\"50%\">被浏览最多的主题</td>\r\n");
	templateBuilder.Append("						<td>被回复最多的主题</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td><ul>" + hottopics.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("						<td><ul>" + hotreplytopics.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (type=="postsrank")
	{

	templateBuilder.Append("		<div class=\"mainbox topicstats\">\r\n");
	templateBuilder.Append("			<h3>发帖排行</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td width=\"25%\">发帖 排行榜</td>\r\n");
	templateBuilder.Append("						<td width=\"25%\">精华帖 排行榜</td>\r\n");
	templateBuilder.Append("						<td width=\"25%\">最近 30 天发帖 排行榜</td>\r\n");
	templateBuilder.Append("						<td width=\"25%\">最近 24 小时发帖 排行榜</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td><ul>" + postsrank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("						<td><ul>" + digestpostsrank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("						<td><ul>" + thismonthpostsrank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("						<td><ul>" + todaypostsrank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (type=="creditsrank")
	{

	templateBuilder.Append("		<div class=\"mainbox topicstats\">\r\n");
	templateBuilder.Append("			<h3>积分排行</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td>积分 排行榜</td>\r\n");

	if (score[1].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td>" + score[1].ToString().Trim() + " 排行榜</td>\r\n");

	}	//end if


	if (score[2].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td>" + score[2].ToString().Trim() + " 排行榜</td>\r\n");

	}	//end if


	if (score[3].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td>" + score[3].ToString().Trim() + " 排行榜</td>\r\n");

	}	//end if


	if (score[4].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td>" + score[4].ToString().Trim() + " 排行榜</td>\r\n");

	}	//end if


	if (score[5].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td>" + score[5].ToString().Trim() + " 排行榜</td>\r\n");

	}	//end if


	if (score[6].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td>" + score[6].ToString().Trim() + " 排行榜</td>\r\n");

	}	//end if


	if (score[7].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td>" + score[7].ToString().Trim() + " 排行榜</td>\r\n");

	}	//end if


	if (score[8].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td>" + score[8].ToString().Trim() + " 排行榜</td>\r\n");

	}	//end if

	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td><ul>" + creditsrank.ToString() + "</ul></td>\r\n");

	if (score[1].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td><ul>" + extcreditsrank1.ToString() + "</ul></td>\r\n");

	}	//end if


	if (score[2].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td><ul>" + extcreditsrank2.ToString() + "</ul></td>\r\n");

	}	//end if


	if (score[3].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td><ul>" + extcreditsrank3.ToString() + "</ul></td>\r\n");

	}	//end if


	if (score[4].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td><ul>" + extcreditsrank4.ToString() + "</ul></td>\r\n");

	}	//end if


	if (score[5].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td><ul>" + extcreditsrank5.ToString() + "</ul></td>\r\n");

	}	//end if


	if (score[6].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td><ul>" + extcreditsrank6.ToString() + "</ul></td>\r\n");

	}	//end if


	if (score[7].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td><ul>" + extcreditsrank7.ToString() + "</ul></td>\r\n");

	}	//end if


	if (score[8].ToString().Trim()!="")
	{

	templateBuilder.Append("							<td><ul>" + extcreditsrank8.ToString() + "</ul></td>\r\n");

	}	//end if

	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (type=="onlinetime")
	{

	templateBuilder.Append("		<div class=\"mainbox topicstats\">\r\n");
	templateBuilder.Append("			<h3>主题排行</h3>\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td width=\"50%\">总在线时间排行(小时)</td>\r\n");
	templateBuilder.Append("						<td>本月在线时间排行(小时)</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td><ul>" + totalonlinerank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("						<td><ul>" + thismonthonlinerank.ToString() + "</ul></td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");
	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (lastupdate!="" && nextupdate!="")
	{

	templateBuilder.Append("		<div class=\"hintinfo notice\">统计数据已被缓存，上次于 " + lastupdate.ToString() + " 被更新，下次将于 " + nextupdate.ToString() + " 进行更新</div>\r\n");

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

	templateBuilder.Append("	</div>\r\n");


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
