<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.register" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:04:12.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:04:12. 
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


	templateBuilder.Append("<div id=\"nav\"><a href=\"" + config.Forumurl.ToString().Trim() + "\">" + config.Forumtitle.ToString().Trim() + "</a> &raquo; 用户注册</div>\r\n");
	templateBuilder.Append("<!--reg start-->\r\n");

	if (agree=="")
	{


	if (page_err==0)
	{


	if (config.Rules==1)
	{

	templateBuilder.Append("		<form id=\"form1\" name=\"form1\" method=\"post\" action=\"?agree=1\">\r\n");
	templateBuilder.Append("		<div class=\"mainbox formbox\">\r\n");
	templateBuilder.Append("		<h1>用户注册协议</h1>\r\n");
	templateBuilder.Append("		<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" align=\"center\" class=\"register\">\r\n");
	templateBuilder.Append("			<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("				<textarea name=\"textarea\" cols=\"\" rows=\"\" readonly=\"readonly\" style=\"width:700px;height:320px;margin:10px 0;\">" + config.Rulestxt.ToString().Trim() + "</textarea>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("			</tbody>\r\n");
	templateBuilder.Append("			<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("			<td  style=\"padding:2px;\">\r\n");
	templateBuilder.Append("			  <input name=\"agree\" disabled=\"disabled\" type=\"submit\" id=\"agree\" value=\"我同意\" />\r\n");
	templateBuilder.Append("			  &nbsp;&nbsp;\r\n");
	templateBuilder.Append("			  <input name=\"cancel\" type=\"button\" id=\"cancel\" value=\"不同意\" onClick=\"javascript:location.replace('index.aspx')\" />				  \r\n");
	templateBuilder.Append("				<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("				var secs = 5;\r\n");
	templateBuilder.Append("				var wait = secs * 1000;\r\n");
	templateBuilder.Append("				document.getElementById(\"agree\").value = \"同 意(\" + secs + \")\";\r\n");
	templateBuilder.Append("				document.getElementById(\"agree\").disabled = true;\r\n");
	templateBuilder.Append("				for(i = 1; i <= secs; i++) {\r\n");
	templateBuilder.Append("						window.setTimeout(\"update(\" + i + \")\", i * 1000);\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				window.setTimeout(\"timer()\", wait);\r\n");
	templateBuilder.Append("				function update(num, value) {\r\n");
	templateBuilder.Append("						if(num == (wait/1000)) {\r\n");
	templateBuilder.Append("								document.getElementById(\"agree\").value = \"同 意\";\r\n");
	templateBuilder.Append("						} else {\r\n");
	templateBuilder.Append("								printnr = (wait / 1000) - num;\r\n");
	templateBuilder.Append("								document.getElementById(\"agree\").value = \"同 意(\" + printnr + \")\";\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				function timer() {\r\n");
	templateBuilder.Append("						document.getElementById(\"agree\").disabled = false;\r\n");
	templateBuilder.Append("						document.getElementById(\"agree\").value = \"同 意\";\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				</" + "script>\r\n");
	templateBuilder.Append("				 </td>\r\n");
	templateBuilder.Append("			 </tr>\r\n");
	templateBuilder.Append("			 </tbody>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		</form>\r\n");


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



	}
	else
	{

	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		location.replace('register.aspx?agree=yes')\r\n");
	templateBuilder.Append("		</" + "script>\r\n");

	}	//end if


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


	}
	else
	{


	if (createuser=="")
	{

	templateBuilder.Append("<form id=\"form1\" name=\"form1\" method=\"post\" action=\"?agree=1&createuser=1\">\r\n");
	templateBuilder.Append("<div class=\"mainbox formbox\">\r\n");
	templateBuilder.Append("	<h1>填写注册信息</h1>\r\n");
	templateBuilder.Append("	<table summary=\"注册\" cellspacing=\"0\" cellpadding=\"0\">\r\n");
	templateBuilder.Append("	<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>基本信息 ( * 为必填项)</th>\r\n");
	templateBuilder.Append("			<td>&nbsp;</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</thead>\r\n");
	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"username\">用户名:</label></th>\r\n");
	templateBuilder.Append("		<td>\r\n");
	templateBuilder.Append("			<input name=\"username\" type=\"text\" id=\"username\" maxlength=\"20\" size=\"20\" onblur=\"checkusername(this.value);\" /><span id=\"checkresult\">不超过20个字符</span>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");
	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"password\">密码</label></th>\r\n");
	templateBuilder.Append("		<td>\r\n");
	templateBuilder.Append("			<input name=\"password\" type=\"password\" id=\"password\" size=\"20\" onkeyup=\"return loadinputcontext(this);\" /><span>不得少于6个字符</span>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>	\r\n");
	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"showmsg\">密码强度</label></th>\r\n");
	templateBuilder.Append("		<td>\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("				var PasswordStrength ={\r\n");
	templateBuilder.Append("					Level : [\"极佳\",\"一般\",\"较弱\",\"太短\"],\r\n");
	templateBuilder.Append("					LevelValue : [15,10,5,0],//强度值\r\n");
	templateBuilder.Append("					Factor : [1,2,5],//字符加数,分别为字母，数字，其它\r\n");
	templateBuilder.Append("					KindFactor : [0,0,10,20],//密码含几种组成的加数 \r\n");
	templateBuilder.Append("					Regex : [/[a-zA-Z]/g,/\\d/g,/[^a-zA-Z0-9]/g] //字符正则数字正则其它正则\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				PasswordStrength.StrengthValue = function(pwd)\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					var strengthValue = 0;\r\n");
	templateBuilder.Append("					var ComposedKind = 0;\r\n");
	templateBuilder.Append("					for(var i = 0 ; i < this.Regex.length;i++)\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						var chars = pwd.match(this.Regex[i]);\r\n");
	templateBuilder.Append("						if(chars != null)\r\n");
	templateBuilder.Append("						{\r\n");
	templateBuilder.Append("							strengthValue += chars.length * this.Factor[i];\r\n");
	templateBuilder.Append("							ComposedKind ++;\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					strengthValue += this.KindFactor[ComposedKind];\r\n");
	templateBuilder.Append("					return strengthValue;\r\n");
	templateBuilder.Append("				} \r\n");
	templateBuilder.Append("				PasswordStrength.StrengthLevel = function(pwd)\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					var value = this.StrengthValue(pwd);\r\n");
	templateBuilder.Append("					for(var i = 0 ; i < this.LevelValue.length ; i ++)\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						if(value >= this.LevelValue[i] )\r\n");
	templateBuilder.Append("							return this.Level[i];\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				function loadinputcontext(o)\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("				   var showmsg=PasswordStrength.StrengthLevel(o.value);\r\n");
	templateBuilder.Append("				   switch(showmsg)\r\n");
	templateBuilder.Append("				   {\r\n");
	templateBuilder.Append("					  case \"太短\": showmsg+=\" <img src='images/level/1.gif' width='88' height='11' />\";break;\r\n");
	templateBuilder.Append("					  case \"较弱\": showmsg+=\" <img src='images/level/2.gif' width='88' height='11' />\";break;\r\n");
	templateBuilder.Append("					  case \"一般\": showmsg+=\" <img src='images/level/3.gif' width='88' height='11' />\";break;\r\n");
	templateBuilder.Append("					  case \"极佳\": showmsg+=\" <img src='images/level/4.gif' width='88' height='11' />\";break;\r\n");
	templateBuilder.Append("				   }\r\n");
	templateBuilder.Append("				   document.getElementById('showmsg').innerHTML = showmsg;\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				function htmlEncode(source, display, tabs)\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					function special(source)\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						var result = '';\r\n");
	templateBuilder.Append("						for (var i = 0; i < source.length; i++)\r\n");
	templateBuilder.Append("						{\r\n");
	templateBuilder.Append("							var c = source.charAt(i);\r\n");
	templateBuilder.Append("							if (c < ' ' || c > '~')\r\n");
	templateBuilder.Append("							{\r\n");
	templateBuilder.Append("								c = '&#' + c.charCodeAt() + ';';\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("							result += c;\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						return result;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					function format(source)\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						// Use only integer part of tabs, and default to 4\r\n");
	templateBuilder.Append("						tabs = (tabs >= 0) ? Math.floor(tabs) : 4;\r\n");
	templateBuilder.Append("						// split along line breaks\r\n");
	templateBuilder.Append("						var lines = source.split(/\\r\\n|\\r|\\n/);\r\n");
	templateBuilder.Append("						// expand tabs\r\n");
	templateBuilder.Append("						for (var i = 0; i < lines.length; i++)\r\n");
	templateBuilder.Append("						{\r\n");
	templateBuilder.Append("							var line = lines[i];\r\n");
	templateBuilder.Append("							var newLine = '';\r\n");
	templateBuilder.Append("							for (var p = 0; p < line.length; p++)\r\n");
	templateBuilder.Append("							{\r\n");
	templateBuilder.Append("								var c = line.charAt(p);\r\n");
	templateBuilder.Append("								if (c === '\\t')\r\n");
	templateBuilder.Append("								{\r\n");
	templateBuilder.Append("									var spaces = tabs - (newLine.length % tabs);\r\n");
	templateBuilder.Append("									for (var s = 0; s < spaces; s++)\r\n");
	templateBuilder.Append("									{\r\n");
	templateBuilder.Append("										newLine += ' ';\r\n");
	templateBuilder.Append("									}\r\n");
	templateBuilder.Append("								}\r\n");
	templateBuilder.Append("								else\r\n");
	templateBuilder.Append("								{\r\n");
	templateBuilder.Append("									newLine += c;\r\n");
	templateBuilder.Append("								}\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("							// If a line starts or ends with a space, it evaporates in html\r\n");
	templateBuilder.Append("							// unless it's an nbsp.\r\n");
	templateBuilder.Append("							newLine = newLine.replace(/(^ )|( $)/g, '&nbsp;');\r\n");
	templateBuilder.Append("							lines[i] = newLine;\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						// re-join lines\r\n");
	templateBuilder.Append("						var result = lines.join('<br />');\r\n");
	templateBuilder.Append("						// break up contiguous blocks of spaces with non-breaking spaces\r\n");
	templateBuilder.Append("						result = result.replace(/  /g, ' &nbsp;');\r\n");
	templateBuilder.Append("						// tada!\r\n");
	templateBuilder.Append("						return result;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					var result = source;\r\n");
	templateBuilder.Append("					// ampersands (&)\r\n");
	templateBuilder.Append("					result = result.replace(/\\&/g,'&amp;');\r\n");
	templateBuilder.Append("					// less-thans (<)\r\n");
	templateBuilder.Append("					result = result.replace(/\\</g,'&lt;');\r\n");
	templateBuilder.Append("					// greater-thans (>)\r\n");
	templateBuilder.Append("					result = result.replace(/\\>/g,'&gt;');\r\n");
	templateBuilder.Append("					if (display)\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						// format for display\r\n");
	templateBuilder.Append("						result = format(result);\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					else\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						// Replace quotes if it isn't for display,\r\n");
	templateBuilder.Append("						// since it's probably going in an html attribute.\r\n");
	templateBuilder.Append("						result = result.replace(new RegExp('\"','g'), '&quot;');\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					// special characters\r\n");
	templateBuilder.Append("					result = special(result);\r\n");
	templateBuilder.Append("					// tada!\r\n");
	templateBuilder.Append("					return result;\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				var profile_username_toolong = '对不起，您的用户名超过 20 个字符，请输入一个较短的用户名。';\r\n");
	templateBuilder.Append("				var profile_username_tooshort = '对不起，您输入的用户名小于3个字符, 请输入一个较长的用户名。';\r\n");
	templateBuilder.Append("				var profile_username_pass = \"可用\";\r\n");
	templateBuilder.Append("				function checkusername(username)\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					var unlen = username.replace(/[^\\x00-\\xff]/g, \"**\").length;\r\n");
	templateBuilder.Append("					if(unlen < 3 || unlen > 20) {\r\n");
	templateBuilder.Append("						document.getElementById(\"checkresult\").innerHTML = \"<font color='#009900'>\" + (unlen < 3 ? profile_username_tooshort : profile_username_toolong) + \"</font>\";\r\n");
	templateBuilder.Append("						return;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					ajaxRead(\"tools/ajax.aspx?t=checkusername&username=\" + escape(username), \"showcheckresult(obj,'\" + username + \"');\");\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				function showcheckresult(obj, username)\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					var res = obj.getElementsByTagName('result');\r\n");
	templateBuilder.Append("					var resContainer = document.getElementById(\"checkresult\");\r\n");
	templateBuilder.Append("					var result = \"\";\r\n");
	templateBuilder.Append("					if (res[0] != null && res[0] != undefined)\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						if (res[0].childNodes.length > 1) {\r\n");
	templateBuilder.Append("							result = res[0].childNodes[1].nodeValue;\r\n");
	templateBuilder.Append("						} else {\r\n");
	templateBuilder.Append("							result = res[0].firstChild.nodeValue;    		\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					if (result == \"1\")\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						resContainer.innerHTML = \"<font color='#009900'>对不起，您输入的用户名 \\\"\" + htmlEncode(username, true, 4) + \"\\\" 已经被他人使用或被禁用，请选择其他名字后再试。</font>\";\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					else\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						resContainer.innerHTML = profile_username_pass;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				function checkSetting()\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					if ($('receiveuser').checked)\r\n");
	templateBuilder.Append("					{\r\n");
	templateBuilder.Append("						$('showhint').disabled = false;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					else\r\n");
	templateBuilder.Append("					{			\r\n");
	templateBuilder.Append("						$('showhint').checked = false;\r\n");
	templateBuilder.Append("						$('showhint').disabled = true;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("			</" + "script>\r\n");
	templateBuilder.Append("			<script type=\"text/javascript\" src=\"javascript/ajax.js\"></" + "script>\r\n");
	templateBuilder.Append("			<strong id=\"showmsg\"></strong>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");
	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"password2\">重复输入密码</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"password2\" type=\"password\" id=\"password2\" size=\"20\"/></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");
	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"email\">Email</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"email\" type=\"text\" id=\"email\" size=\"30\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");

	if (config.Realnamesystem==1)
	{


	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"realname\">真实姓名</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"realname\" type=\"text\" id=\"realname\" size=\"10\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"idcard\">身份证号码</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"idcard\" type=\"text\" id=\"idcard\" size=\"20\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"mobile\">移动电话号码</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"mobile\" type=\"text\" id=\"mobile\" size=\"20\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"phone\">固定电话号码</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"phone\" type=\"text\" id=\"phone\" size=\"20\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");



	}	//end if


	if (isseccode)
	{

	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"vcode\">验证码</label></th>\r\n");
	templateBuilder.Append("		<td>\r\n");

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
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");

	}	//end if

	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th>&nbsp;</th>\r\n");
	templateBuilder.Append("		<td><input name=\"submit\" type=\"submit\" value=\"创建用户\"/>  <input type=\"button\" onclick=\"javascript:window.location.replace('./index.aspx')\" value=\"取消\"/></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (config.Regadvance==1)
	{

	templateBuilder.Append("<div class=\"mainbox formbox\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\">\r\n");
	templateBuilder.Append("		<a href=\"###\" onclick=\"toggle_collapse('regoptions');\"><img id= \"regoptions_img\" src=\"templates/" + templatepath.ToString() + "/images/open_yes.gif\" alt=\"展开\" /></a>\r\n");
	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<h1>填写可选项</h1>\r\n");
	templateBuilder.Append("	<table summary=\"注册 高级选项\" cellspacing=\"0\" cellpadding=\"0\" id=\"regoptions\" style=\"display: none;\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>找回密码问题</th>\r\n");
	templateBuilder.Append("			<td>&nbsp;</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"question\">问题</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"question\" id=\"question\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">无</option>\r\n");
	templateBuilder.Append("				  <option value=\"1\">母亲的名字</option>\r\n");
	templateBuilder.Append("				  <option value=\"2\">爷爷的名字</option>\r\n");
	templateBuilder.Append("				  <option value=\"3\">父亲出生的城市</option>\r\n");
	templateBuilder.Append("				  <option value=\"4\">您其中一位老师的名字</option>\r\n");
	templateBuilder.Append("				  <option value=\"5\">您个人计算机的型号</option>\r\n");
	templateBuilder.Append("				  <option value=\"6\">您最喜欢的餐馆名称</option>\r\n");
	templateBuilder.Append("				  <option value=\"7\">驾驶执照的最后四位数字</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>		\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"answer\">答案</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"answer\" type=\"text\" id=\"answer\" size=\"30\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>个人信息</th>\r\n");
	templateBuilder.Append("			<td>&nbsp;</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	if (config.Realnamesystem==0)
	{


	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"realname\">真实姓名</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"realname\" type=\"text\" id=\"realname\" size=\"10\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"idcard\">身份证号码</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"idcard\" type=\"text\" id=\"idcard\" size=\"20\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"mobile\">移动电话号码</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"mobile\" type=\"text\" id=\"mobile\" size=\"20\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"phone\">固定电话号码</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"phone\" type=\"text\" id=\"phone\" size=\"20\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");



	}	//end if

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"gender\">性别</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"gender\" value=\"1\" style=\"InPutRadio\"/>男\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"gender\" value=\"2\"  style=\"InPutRadio\"/>女\r\n");
	templateBuilder.Append("				<input name=\"gender\" type=\"radio\" value=\"0\" checked=\"checked\"  style=\"InPutRadio\"/>保密\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"nickname\">昵称</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"nickname\" type=\"text\" id=\"nickname\" size=\"20\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>	\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"bday\">生日</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"bday_y\" type=\"text\" id=\"bday_y\" size=\"4\" maxlength=\"4\" />年\r\n");
	templateBuilder.Append("				<input name=\"bday_m\" type=\"text\" id=\"bday_m\" size=\"2\"  maxlength=\"2\"/>月\r\n");
	templateBuilder.Append("				<input name=\"bday_d\" type=\"text\" id=\"bday_d\" size=\"2\"  maxlength=\"2\"/>日\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"location\">来自</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"location\" type=\"text\" id=\"location\" size=\"20\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"msn\">MSN Messenger</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"msn\" type=\"text\" id=\"msn\" size=\"30\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"yahoo\">Yahoo Messenger</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"yahoo\" type=\"text\" id=\"yahoo\" size=\"30\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"skype\">Skype</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"skype\" type=\"text\" id=\"skype\" size=\"30\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"ICQ\">ICQ</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"icq\" type=\"text\" id=\"icq\" size=\"12\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"qq\">QQ</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"qq\" type=\"text\" id=\"qq\" size=\"12\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"homepage\">主页</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"homepage\" type=\"text\" id=\"homepage\" size=\"30\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"bio\">自我介绍</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<textarea name=\"bio\" cols=\"50\" rows=\"6\" id=\"bio\" style=\"height:95px;width:85%;\"></textarea>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>论坛使用喜好设置:</th>\r\n");
	templateBuilder.Append("			<td>&nbsp;</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"templateid\">风格</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"templateid\" id=\"templateid\" >\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected>默认</option>\r\n");

	int template__loop__id=0;
	foreach(DataRow template in templatelist.Rows)
	{
		template__loop__id++;

	templateBuilder.Append("					<option value=\"" + template["templateid"].ToString().Trim() + "\">" + template["name"].ToString().Trim() + "</option>\r\n");

	}	//end loop

	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"tpp\">每页主题数</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"tpp\" id=\"tpp\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">默认</option>\r\n");
	templateBuilder.Append("				  <option value=\"15\">15</option>\r\n");
	templateBuilder.Append("				  <option value=\"20\">20</option>\r\n");
	templateBuilder.Append("				  <option value=\"25\">25</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"ppp\">每页帖子数</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"ppp\" id=\"ppp\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">默认</option>\r\n");
	templateBuilder.Append("				  <option value=\"10\">10</option>\r\n");
	templateBuilder.Append("				  <option value=\"15\">15</option>\r\n");
	templateBuilder.Append("				  <option value=\"20\">20</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"newpm\">是否提示短消息</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"newpm\" type=\"radio\" value=\"radiobutton\" checked=\"checked\"  style=\"InPutRadio\"/>是\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"newpm\" value=\"radiobutton\"  style=\"InPutRadio\"/>否\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"pmsound\">短消息铃声</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"pmsound\" id=\"pmsound\">\r\n");
	templateBuilder.Append("				  <option value=\"1\" selected=\"selected\">默认</option>							  \r\n");
	templateBuilder.Append("				  <option value=\"1\">提示音1</option>							  \r\n");
	templateBuilder.Append("				  <option value=\"2\">提示音2</option>							  \r\n");
	templateBuilder.Append("				  <option value=\"3\">提示音3</option>		\r\n");
	templateBuilder.Append("				  <option value=\"4\">提示音4</option>		\r\n");
	templateBuilder.Append("				  <option value=\"5\">提示音5</option>							  \r\n");
	templateBuilder.Append("				  <option value=\"0\">无</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"showemail\">是否显示Email</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"showemail\" type=\"radio\" value=\"1\" checked=\"checked\"  style=\"InPutRadio\"/>是\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"showemail\" value=\"0\"  style=\"InPutRadio\"/>否\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"receivesetting\">消息接收设置</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input id=\"receiveuser\" onclick=\"checkSetting();\" type=\"checkbox\" name=\"receivesetting\" value=\"2\" checked=\"checked\" />接收用户短消息\r\n");
	templateBuilder.Append("				<input id=\"showhint\" onclick=\"checkSetting();\" type=\"checkbox\" name=\"receivesetting\" value=\"4\" checked=\"checked\" />显示短消息提示框\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"invisible\">是否隐身</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"invisible\" value=\"1\"  style=\"InPutRadio\"/>是\r\n");
	templateBuilder.Append("				<input name=\"invisible\" type=\"radio\" value=\"0\" checked=\"checked\"  style=\"InPutRadio\"/>否\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"signature\">签名</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<textarea name=\"signature\" cols=\"50\" rows=\"6\" id=\"signature\" style=\"height:95px;width:85%;\"></textarea>\r\n");
	templateBuilder.Append("				<input name=\"sigstatus\" type=\"checkbox\" id=\"sigstatus\" value=\"1\" checked=\"checked\" />使用签名\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>&nbsp;</th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"submit\" type=\"submit\" value=\"创建用户\"/>  <input type=\"button\" onclick=\"javascript:window.location.replace('./index.aspx')\" value=\"取消\"/></td>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if

	templateBuilder.Append("</form>\r\n");
	templateBuilder.Append("<!--reg end-->\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}
	else
	{


	if (createuser!="")
	{


	if (page_err==0)
	{


	templateBuilder.Append("<div class=\"box message\">\r\n");
	templateBuilder.Append("	<h1>Discuz!NT Board 提示信息</h1>\r\n");
	templateBuilder.Append("	<p>" + msgbox_text.ToString() + "</p>\r\n");

	if (msgbox_url!="")
	{

	templateBuilder.Append("	<p><a href=\"" + msgbox_url.ToString() + "\">如果浏览器没有转向, 请点击这里.</a></p>\r\n");

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



	}	//end if

	templateBuilder.Append("	</div>\r\n");

	}	//end if


	}	//end if


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
