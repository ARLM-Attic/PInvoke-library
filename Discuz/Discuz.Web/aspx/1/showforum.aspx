<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.showforum" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 14:22:03.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 14:22:03. 
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


	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/ajax.js\"></" + "script>\r\n");

	if (page_err==0)
	{

	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	var templatepath = \"" + templatepath.ToString() + "\";\r\n");
	templateBuilder.Append("	var fid = parseInt(" + forum.Fid.ToString().Trim() + ");\r\n");
	templateBuilder.Append("	var postminchars = parseInt(" + config.Minpostsize.ToString().Trim() + ");\r\n");
	templateBuilder.Append("	var postmaxchars = parseInt(" + config.Maxpostsize.ToString().Trim() + ");\r\n");
	templateBuilder.Append("	var disablepostctrl = parseInt(" + disablepostctrl.ToString() + ");\r\n");
	templateBuilder.Append("	</" + "script>\r\n");

	}	//end if

	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_showforum.js\"></" + "script>\r\n");
	templateBuilder.Append("<div id=\"foruminfo\">\r\n");
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

	if (page_err==0)
	{

	templateBuilder.Append("		<p>\r\n");
	templateBuilder.Append("			<a href=\"showtopiclist.aspx?type=digest&amp;forums=" + forum.Fid.ToString().Trim() + "\">精华帖区</a>\r\n");

	if (config.Rssstatus!=0)
	{

	 aspxrewriteurl = this.RssAspxRewrite(forum.Fid);
	
	templateBuilder.Append("			<a href=\"tools/" + aspxrewriteurl.ToString() + "\" target=\"_blank\"><img src=\"templates/" + templatepath.ToString() + "/images/rss.gif\" alt=\"Rss\"/></a>\r\n");

	}	//end if

	templateBuilder.Append("		</p>\r\n");

	}	//end if

	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div id=\"nav\">\r\n");
	templateBuilder.Append("		<div class=\"userinfolist\">\r\n");
	templateBuilder.Append("			<p><a id=\"forumlist\" href=\"" + config.Forumurl.ToString().Trim() + "\" \r\n");

	if (config.Forumjump==1)
	{

	templateBuilder.Append("onmouseover=\"showMenu(this.id);\" onmouseout=\"showMenu(this.id);\"\r\n");

	}	//end if

	templateBuilder.Append("			>" + config.Forumtitle.ToString().Trim() + "</a> &raquo; " + forumnav.ToString() + " </p>\r\n");

	if (page_err==0)
	{

	templateBuilder.Append("			<p> 版主: \r\n");
	templateBuilder.Append("			<em>\r\n");

	if (forum.Moderators!="")
	{

	templateBuilder.Append("				" + forum.Moderators.ToString().Trim() + "\r\n");

	}
	else
	{

	templateBuilder.Append("				*空缺中*\r\n");

	}	//end if

	templateBuilder.Append("			</em>\r\n");
	templateBuilder.Append("			</p>\r\n");

	}	//end if

	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (page_err==0)
	{


	if (config.Forumjump==1)
	{

	templateBuilder.Append("	" + navhomemenu.ToString() + "\r\n");

	}	//end if


	if (showforumlogin==1)
	{

	templateBuilder.Append("	<div class=\"mainbox formbox\">\r\n");
	templateBuilder.Append("		<h3>本版块已经被管理员设置了密码</h3>\r\n");
	templateBuilder.Append("		<form id=\"forumlogin\" name=\"forumlogin\" method=\"post\" action=\"\">\r\n");
	templateBuilder.Append("			<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<th>请输入密码:</th>\r\n");
	templateBuilder.Append("					<td><input name=\"forumpassword\" type=\"password\" id=\"forumpassword\" size=\"20\"/></td>\r\n");
	templateBuilder.Append("				</tr>\r\n");
	templateBuilder.Append("				</tbody>\r\n");

	if (isseccode)
	{

	templateBuilder.Append("				<tbody>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<th>输入验证码:</th>\r\n");
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




	if (forum.Rules!="")
	{

	templateBuilder.Append("<table class=\"portalbox\" cellspacing=\"1\" cellpadding=\"0\" summary=\"rules and recommend\">\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("	<td id=\"rules\">\r\n");
	templateBuilder.Append("		<span class=\"headactions recommendrules\"><img id=\"rules_img\" title=\"收起/展开\" onclick=\"$('rules_link').style.display = '';toggle_collapse('rules', 1);\" alt=\"收起/展开\" src=\"templates/" + templatepath.ToString() + "/images/collapsed_no.gif\" /></span> <h3>本版规则</h3>" + forum.Rules.ToString().Trim() + "\r\n");
	templateBuilder.Append("	</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");
	templateBuilder.Append("</table>\r\n");

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




	if (subforumcount>0)
	{



	if (forum.Colcount==1)
	{

	templateBuilder.Append("<!--ntforumboxstart-->\r\n");
	templateBuilder.Append("<div class=\"mainbox forumlist\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\">\r\n");
	templateBuilder.Append("		<img id=\"category_" + forum.Fid.ToString().Trim() + "_img\" src=\"templates/" + templatepath.ToString() + "/images/collapsed_yes.gif\" alt=\"展开/收起\" onclick=\"toggle_collapse('category_" + forum.Fid.ToString().Trim() + "');\"/>\r\n");
	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<h3>子版块</h3>\r\n");
	templateBuilder.Append("	<table cellspacing=\"0\" cellpadding=\"0\" summary=\"category_" + forum.Fid.ToString().Trim() + "\">\r\n");
	templateBuilder.Append("		<thead class=\"category\">\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>版块</th>\r\n");
	templateBuilder.Append("				<td class=\"nums\">主题</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\">帖子</td>\r\n");
	templateBuilder.Append("				<td class=\"lastpost\">最后发表</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	int subforum__loop__id=0;
	foreach(IndexPageForumInfo subforum in subforumlist)
	{
		subforum__loop__id++;

	templateBuilder.Append("		<tbody id=\"category_" + forum.Fid.ToString().Trim() + "\">\r\n");
	templateBuilder.Append("			<tr>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(subforum.Fid,0);
	
	templateBuilder.Append("				<th \r\n");

	if (subforum.Havenew=="new")
	{

	templateBuilder.Append("class=\"new\"\r\n");

	}	//end if

	templateBuilder.Append(">\r\n");
	templateBuilder.Append("					<h2>\r\n");

	if (subforum.Icon!="")
	{

	templateBuilder.Append("							<img src=\"" + subforum.Icon.ToString().Trim() + "\" border=\"0\" align=\"left\" hspace=\"5\" alt=\"" + subforum.Name.ToString().Trim() + "\"/>\r\n");

	}	//end if


	if (subforum.Redirect=="")
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(subforum.Fid,0);
	
	templateBuilder.Append("							<a href=\"" + aspxrewriteurl.ToString() + "\">\r\n");

	}
	else
	{

	templateBuilder.Append("							<a href=\"" + subforum.Redirect.ToString().Trim() + "\" target=\"_blank\">\r\n");

	}	//end if

	templateBuilder.Append("						" + subforum.Name.ToString().Trim() + "</a>\r\n");

	if (subforum.Todayposts>0)
	{

	templateBuilder.Append("<span class=\"today\">(" + subforum.Todayposts.ToString().Trim() + ")</span>\r\n");

	}	//end if

	templateBuilder.Append("					</h2>\r\n");

	if (subforum.Description!="")
	{

	templateBuilder.Append("<p>" + subforum.Description.ToString().Trim() + "</p>\r\n");

	}	//end if


	if (subforum.Moderators!="")
	{

	templateBuilder.Append("<p class=\"moderators\">版主:" + subforum.Moderators.ToString().Trim() + "</p>\r\n");

	}	//end if

	templateBuilder.Append("				</th>\r\n");
	templateBuilder.Append("				<td class=\"nums\">" + subforum.Topics.ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\">" + subforum.Posts.ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td class=\"lastposter\">\r\n");

	if (subforum.Status==-1)
	{

	templateBuilder.Append("						私密论坛\r\n");

	}
	else
	{


	if (subforum.Lasttid!=0)
	{

	templateBuilder.Append("						<p>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(subforum.Lasttid,0);
	
	templateBuilder.Append("							<a href=\"" + aspxrewriteurl.ToString() + "\">" + subforum.Lasttitle.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("						</p>\r\n");
	templateBuilder.Append("						<div class=\"topicbackwriter\">by\r\n");

	if (subforum.Lastposter!="")
	{


	if (subforum.Lastposterid==-1)
	{

	templateBuilder.Append("									游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(subforum.Lastposterid);
	
	templateBuilder.Append("									<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + subforum.Lastposter.ToString().Trim() + "</a>\r\n");

	}	//end if


	}
	else
	{

	templateBuilder.Append("								匿名\r\n");

	}	//end if

	 aspxrewriteurl = this.ShowTopicAspxRewrite(subforum.Lasttid,0);
	
	templateBuilder.Append("							<a href=\"" + aspxrewriteurl.ToString() + "\" title=\"" + subforum.Lasttitle.ToString().Trim() + "\"><span>\r\n");
	templateBuilder.Append(Convert.ToDateTime(subforum.Lastpost).ToString(" yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</span></a>\r\n");
	templateBuilder.Append("						</div>\r\n");

	}
	else
	{

	templateBuilder.Append("							从未\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			  </tr>\r\n");
	templateBuilder.Append("		   </tbody>\r\n");

	}	//end loop

	templateBuilder.Append("			</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("<!--ntforumbox end-->\r\n");

	}
	else
	{

	templateBuilder.Append("<!--ntforumbox start-->\r\n");
	templateBuilder.Append("<div class=\"mainbox forumlist\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\">\r\n");

	if (forum.Moderators!="")
	{

	templateBuilder.Append("			分类版主: " + forum.Moderators.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("		<img id=\"category_" + forum.Fid.ToString().Trim() + "_img\"  src=\"templates/" + templatepath.ToString() + "/images/collapsed_yes.gif\" alt=\"展开/收起\" onclick=\"toggle_collapse('category_" + forum.Fid.ToString().Trim() + "');\"/>\r\n");
	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<h3>子论坛</h3>\r\n");
	templateBuilder.Append("	<table id=\"category_" + forum.Fid.ToString().Trim() + "\"  cellspacing=\"0\" cellpadding=\"0\" summary=\"category_" + forum.Fid.ToString().Trim() + "\">	\r\n");
	int subforumindex = 0;
	
	double colwidth = 99.6 / forum.Colcount;
	

	int subforum__loop__id=0;
	foreach(IndexPageForumInfo subforum in subforumlist)
	{
		subforum__loop__id++;

	 subforumindex = subforumindex+1;
	

	if (subforumindex==1)
	{

	templateBuilder.Append("		<tbody id=\"category_" + forum.Fid.ToString().Trim() + "\">\r\n");
	templateBuilder.Append("			<tr>\r\n");

	}	//end if

	templateBuilder.Append("			  <th style=\"width:" + colwidth.ToString() + "%;\"\r\n");

	if (subforum.Havenew=="new")
	{

	templateBuilder.Append("class=\"new\"\r\n");

	}	//end if

	templateBuilder.Append(">\r\n");
	templateBuilder.Append("					<h2>\r\n");

	if (subforum.Icon!="")
	{

	templateBuilder.Append("						<img src=\"" + subforum.Icon.ToString().Trim() + "\" alt=\"" + subforum.Name.ToString().Trim() + "\" hspace=\"5\" />\r\n");

	}	//end if


	if (forum.Redirect=="")
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(subforum.Fid,0);
	
	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\">\r\n");

	}
	else
	{

	templateBuilder.Append("						<a href=\"" + subforum.Redirect.ToString().Trim() + "\" target=\"_blank\">\r\n");

	}	//end if

	templateBuilder.Append("					" + subforum.Name.ToString().Trim() + "</a>\r\n");

	if (subforum.Todayposts>0)
	{

	templateBuilder.Append("					<span class=\"today\">(" + subforum.Todayposts.ToString().Trim() + ")</span>\r\n");

	}	//end if

	templateBuilder.Append("					</h2>\r\n");
	templateBuilder.Append("					<p>主题:" + subforum.Topics.ToString().Trim() + ", 帖数:" + subforum.Posts.ToString().Trim() + "</p>\r\n");

	if (subforum.Status==-1)
	{

	templateBuilder.Append("					<p>私密版块</p>\r\n");

	}
	else
	{


	if (subforum.Lasttid!=0)
	{

	templateBuilder.Append("							<p>最后: <a href=\"showtopic.aspx?topicid=" + subforum.Lasttid.ToString().Trim() + "&page=end#lastpost\" title=\"" + subforum.Lasttitle.ToString().Trim() + "\"><span>\r\n");
	templateBuilder.Append(Convert.ToDateTime(subforum.Lastpost).ToString(" yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</span></a> by \r\n");

	if (subforum.Lastposter!="")
	{


	if (subforum.Lastposterid==-1)
	{

	templateBuilder.Append("										游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(subforum.Lastposterid);
	
	templateBuilder.Append("										<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + subforum.Lastposter.ToString().Trim() + "</a>\r\n");

	}	//end if


	}
	else
	{

	templateBuilder.Append("									匿名\r\n");

	}	//end if

	templateBuilder.Append("							</p>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("			  </th>\r\n");

	if (subforumindex==forum.Colcount)
	{

	templateBuilder.Append("			</tr>\r\n");
	 subforumindex = 0;
	

	}	//end if


	}	//end loop


	if (subforumindex!=0)
	{

	for (int i = 0; i < forum.Colcount-subforumindex; i++)
	{
		templateBuilder.Append("<td>&nbsp;</td>");
	}

	templateBuilder.Append("			</tr>\r\n");

	}	//end if

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<!--ntforumbox end-->\r\n");

	}	//end if




	}	//end if


	if (forum.Layer!=0)
	{

	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) { if(parseInt('" + config.Aspxrewrite.ToString().Trim() + "')==1) {window.location='showforum-" + forumid.ToString() + "-' + (parseInt(this.value) > 0 ? parseInt(this.value) : 1) + '" + config.Extname.ToString().Trim() + "';}else{window.location='showforum.aspx?forumid=" + forumid.ToString() + "&page=' + (parseInt(this.value) > 0 ? parseInt(this.value) : 1)}}\" size=\"4\" maxlength=\"9\" />页</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");

	if (userid<0||canposttopic)
	{

	templateBuilder.Append("	<span onmouseover=\"$('newspecial').id = 'newspecialtmp';this.id = 'newspecial';if($('newspecial_menu').childNodes.length>0)  showMenu(this.id);\" id=\"newspecial\" class=\"postbtn\"><a title=\"发新话题\" id=\"newtopic\" href=\"posttopic.aspx?forumid=" + forum.Fid.ToString().Trim() + "\" onmouseover=\"if($('newspecial_menu').childNodes.length>0)  showMenu(this.id);\"><img alt=\"发新话题\" src=\"templates/" + templatepath.ToString() + "/images/newtopic.gif\"/></a></span>\r\n");

	}	//end if

	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div id=\"headfilter\">\r\n");
	templateBuilder.Append("	<ul class=\"tabs\">\r\n");
	templateBuilder.Append("	    <li \r\n");

	if (filter=="")
	{

	templateBuilder.Append("class=\"current\"\r\n");

	}	//end if

	templateBuilder.Append(" ><a href=\"" + ShowForumAspxRewrite(forumid,0).ToString() + "\">全部</a></li>\r\n");
	templateBuilder.Append("		<li \r\n");

	if (filter=="digest")
	{

	templateBuilder.Append("class=\"current\"\r\n");

	}	//end if

	templateBuilder.Append(" ><a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&filter=digest\">精华</a></li>\r\n");
	int specialpost = forum.Allowpostspecial&1;
	

	if (specialpost==1)
	{

	templateBuilder.Append("		<li \r\n");

	if (filter=="poll")
	{

	templateBuilder.Append("class=\"current\"\r\n");

	}	//end if

	templateBuilder.Append(" ><a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&filter=poll\">投票</a></li>\r\n");

	}	//end if

	 specialpost = forum.Allowpostspecial&4;
	

	if (specialpost==4)
	{

	templateBuilder.Append("		<li id=\"rewardmenu\" class=\"\r\n");

	if (filter=="reward" || filter=="rewarding")
	{

	templateBuilder.Append("current\r\n");

	}
	else
	{


	if (filter=="rewarded")
	{

	templateBuilder.Append("current\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append(" dropmenu\" onMouseOver=\"showMenu(this.id);\"><a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&filter=reward\">悬赏</a></li>\r\n");

	}	//end if

	 specialpost = forum.Allowpostspecial&16;
	

	if (specialpost==16)
	{

	templateBuilder.Append("		<li \r\n");

	if (filter=="debate")
	{

	templateBuilder.Append("class=\"current\"\r\n");

	}	//end if

	templateBuilder.Append(" ><a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&filter=debate\">辩论</a></li>\r\n");

	}	//end if

	templateBuilder.Append("	</ul>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<ul class=\"popupmenu_popup headermenu_popup\" id=\"rewardmenu_menu\" style=\"display: none\">\r\n");
	templateBuilder.Append("	<li><a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&filter=rewarding\">进行中的悬赏</a></li>\r\n");
	templateBuilder.Append("	<li><a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&filter=rewarded\">已结束的悬赏</a></li>\r\n");
	templateBuilder.Append("</ul>\r\n");
	templateBuilder.Append("<form id=\"moderate\" name=\"moderate\" method=\"post\" action=\"topicadmin.aspx?action=moderate&forumid=" + forumid.ToString() + "\">\r\n");
	templateBuilder.Append("<div class=\"mainbox threadlist\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\">\r\n");

	if (forum.Applytopictype==1 && forum.Viewbytopictype==1)
	{

	templateBuilder.Append("		" + topictypeselectlink.ToString() + "\r\n");

	}	//end if

	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<h1>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(forum.Fid,0);
	
	templateBuilder.Append("		<a href=\"" + aspxrewriteurl.ToString() + "\">" + forum.Name.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("		<!--<em>(主题:" + topiccount.ToString() + "篇 帖子:" + forum.Posts.ToString().Trim() + "个 今日帖子:" + forum.Todayposts.ToString().Trim() + "个)</em>-->\r\n");
	templateBuilder.Append("	</h1>\r\n");
	templateBuilder.Append("	<table summary=\"" + forum.Fid.ToString().Trim() + "\" id=\"" + forum.Fid.ToString().Trim() + "\" cellspacing=\"0\" cellpadding=\"0\">\r\n");
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
	templateBuilder.Append("		<!--announcement start-->\r\n");

	int announcement__loop__id=0;
	foreach(DataRow announcement in announcementlist.Rows)
	{
		announcement__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td><img src=\"templates/" + templatepath.ToString() + "/images/announcement.gif\" alt=\"announcement\" /></td>\r\n");
	templateBuilder.Append("			<td>&nbsp;</td>\r\n");
	templateBuilder.Append("			<th>\r\n");
	templateBuilder.Append("				<a href=\"announcement.aspx#" + announcement["id"].ToString().Trim() + "\">" + announcement["title"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("			</th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<cite>\r\n");

	if (Utils.StrToInt(announcement["posterid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("					游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(announcement["posterid"].ToString().Trim());
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\">" + announcement["poster"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("				</cite>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("			<td>-</td>\r\n");
	templateBuilder.Append("			<td>-</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("		<!--announcement end-->\r\n");
	templateBuilder.Append("		<!--NtForumList start-->\r\n");

	int toptopic__loop__id=0;
	foreach(ShowforumPageTopicInfo toptopic in toptopiclist)
	{
		toptopic__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td class=\"folder\">\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(toptopic.Tid,0);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\"><img src=\"templates/" + templatepath.ToString() + "/images/t_top" + toptopic.Displayorder.ToString().Trim() + ".gif\"/></a>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td class=\"icon\">\r\n");

	if (toptopic.Iconid!=0)
	{

	templateBuilder.Append("							<img src=\"images/posticons/" + toptopic.Iconid.ToString().Trim() + ".gif\" alt=\"listicon\" />\r\n");

	}
	else
	{

	templateBuilder.Append("							&nbsp;\r\n");

	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<th class=\"common\">\r\n");
	templateBuilder.Append("					<label>\r\n");

	if (toptopic.Digest>0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/digest" + toptopic.Digest.ToString().Trim() + ".gif\" alt=\"digtest\"/>\r\n");

	}	//end if


	if (toptopic.Special==1)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/pollsmall.gif\" alt=\"投票\" />\r\n");

	}	//end if


	if (toptopic.Special==2 || toptopic.Special==3)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/bonus.gif\" alt=\"悬赏\"/>\r\n");

	}	//end if


	if (toptopic.Special==4)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/debatesmall.gif\" alt=\"辩论\"/>\r\n");

	}	//end if


	if (toptopic.Attachment>0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/attachment.gif\" alt=\"附件\"/>\r\n");

	}	//end if


	if (toptopic.Rate>0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/agree.gif\" alt=\"正分\"/>\r\n");

	}	//end if


	if (toptopic.Rate<0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/disagree.gif\" alt=\"负分\"/>\r\n");

	}	//end if

	templateBuilder.Append("					</label>\r\n");

	if (useradminid>0 && ismoder)
	{


	if (toptopic.Fid==forum.Fid)
	{

	templateBuilder.Append("						<input type=\"checkbox\" name=\"topicid\" value=\"" + toptopic.Tid.ToString().Trim() + "\" />\r\n");

	}
	else
	{

	templateBuilder.Append("						<input type=\"checkbox\" disabled />\r\n");

	}	//end if


	}	//end if


	if (toptopic.Replies>0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/topItem_exp.gif\" id=\"imgButton_" + toptopic.Tid.ToString().Trim() + "\" onclick=\"showtree(" + toptopic.Tid.ToString().Trim() + "," + ppp.ToString() + "," + config.Aspxrewrite.ToString().Trim() + ");\" class=\"cursor\" alt=\"展开帖子列表\" title=\"展开帖子列表\" />\r\n");

	}
	else
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/no-sublist.gif\" id=\"imgButton_" + toptopic.Tid.ToString().Trim() + "\" alt=\"闭合帖子列表\"/>\r\n");

	}	//end if


	if (forum.Applytopictype==1 && forum.Topictypeprefix==1)
	{

	templateBuilder.Append("							<em>\r\n");

	if (forum.Viewbytopictype==1 && toptopic.Topictypename!="")
	{

	templateBuilder.Append("							[<a href=\"showforum.aspx?forumid=" + toptopic.Fid.ToString().Trim() + "&typeid=" + toptopic.Typeid.ToString().Trim() + "\" >" + toptopic.Topictypename.ToString().Trim() + "</a>]\r\n");

	}
	else if (toptopic.Topictypename!="")
	{

	templateBuilder.Append("							[" + toptopic.Topictypename.ToString().Trim() + "]\r\n");

	}	//end if

	templateBuilder.Append("							</em>\r\n");

	}	//end if

	 aspxrewriteurl = this.ShowTopicAspxRewrite(toptopic.Tid,0);
	

	if (toptopic.Special==3)
	{

	 aspxrewriteurl = this.ShowBonusAspxRewrite(toptopic.Tid,0);
	

	}	//end if


	if (toptopic.Special==4)
	{

	 aspxrewriteurl = this.ShowDebateAspxRewrite(toptopic.Tid);
	

	}	//end if

	int ishtmltitle = Topics.GetMagicValue(toptopic.Magic, MagicType.HtmlTitle);
	

	if (ishtmltitle==1)
	{

	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\">" + Topics.GetHtmlTitle(toptopic.Tid).ToString().Trim() + "</a>\r\n");

	}
	else
	{

	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\">" + toptopic.Title.ToString().Trim() + "</a>\r\n");

	}	//end if


	if (toptopic.Special==2)
	{

	templateBuilder.Append("						- [悬赏 " + userextcreditsinfo.Name.ToString().Trim() + " <span class=\"bold\">" + toptopic.Price.ToString().Trim() + "</span> " + userextcreditsinfo.Unit.ToString().Trim() + "] \r\n");

	}
	else if (toptopic.Special==3)
	{

	templateBuilder.Append("						- [已结束的悬赏]\r\n");

	}
	else if (toptopic.Special==0)
	{


	if (toptopic.Price>0)
	{

	templateBuilder.Append("							- [售价 " + userextcreditsinfo.Name.ToString().Trim() + " <span class=\"bold\">" + toptopic.Price.ToString().Trim() + "</span> " + userextcreditsinfo.Unit.ToString().Trim() + "] \r\n");

	}	//end if


	}	//end if


	if (toptopic.Readperm>0)
	{

	templateBuilder.Append("						- [阅读权限 <span class=\"bold\">" + toptopic.Readperm.ToString().Trim() + "</span>] \r\n");

	}	//end if


	if (toptopic.Replies/ppp>0)
	{

	templateBuilder.Append("							<span class=\"threadpages\"><script type=\"text/javascript\">getpagenumbers(\"" + config.Extname.ToString().Trim() + "\"," + toptopic.Replies.ToString().Trim() + "," + ppp.ToString() + ",0,\"\"," + toptopic.Tid.ToString().Trim() + ");</" + "script></span>				\r\n");

	}	//end if

	templateBuilder.Append("				</th>\r\n");
	templateBuilder.Append("				<td class=\"author\">\r\n");
	templateBuilder.Append("					<cite>\r\n");

	if (toptopic.Posterid==-1)
	{

	templateBuilder.Append("						游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(toptopic.Posterid);
	
	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\">" + toptopic.Poster.ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("</cite>\r\n");
	templateBuilder.Append("					<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(toptopic.Postdatetime).ToString(" yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\"><strong>" + toptopic.Replies.ToString().Trim() + "</strong> / <em>" + toptopic.Views.ToString().Trim() + "</em></td>\r\n");
	templateBuilder.Append("				<td class=\"lastpost\">\r\n");
	templateBuilder.Append("					<em><a href=\"showtopic.aspx?topicid=" + toptopic.Tid.ToString().Trim() + "&page=end#lastpost\">\r\n");
	templateBuilder.Append(Convert.ToDateTime(toptopic.Lastpost).ToString(" yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</a></em>\r\n");
	templateBuilder.Append("					<cite>by\r\n");

	if (toptopic.Lastposterid==-1)
	{

	templateBuilder.Append("							游客\r\n");

	}
	else
	{

	templateBuilder.Append("							<a href=\"" + UserInfoAspxRewrite(toptopic.Lastposterid).ToString().Trim() + "\" target=\"_blank\">" + toptopic.Lastposter.ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("					</cite>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("			<tr><td colspan=\"7\" id=\"divTopic" + toptopic.Tid.ToString().Trim() + "\" style=\"border:0; padding:0;\"></td></tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");

	if (showsplitter)
	{

	templateBuilder.Append("		<thead class=\"separation\">\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>&nbsp;</td>\r\n");
	templateBuilder.Append("				<td>&nbsp;</td>\r\n");
	templateBuilder.Append("				<td colspan=\"4\">版块主题</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	}	//end if


	int topic__loop__id=0;
	foreach(ShowforumPageTopicInfo topic in topiclist)
	{
		topic__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td class=\"folder\">\r\n");

	if (topic.Folder!="")
	{

	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\"><img src=\"templates/" + templatepath.ToString() + "/images/folder_" + topic.Folder.ToString().Trim() + ".gif\" alt=\"topicicon\" /></a>\r\n");

	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td class=\"icon\">\r\n");

	if (topic.Iconid!=0)
	{

	templateBuilder.Append("						<img src=\"images/posticons/" + topic.Iconid.ToString().Trim() + ".gif\" alt=\"listicon\"/>\r\n");

	}
	else
	{

	templateBuilder.Append("						&nbsp;\r\n");

	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<th class=\"common\">\r\n");
	templateBuilder.Append("					<label>\r\n");

	if (topic.Digest>0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/digest" + topic.Digest.ToString().Trim() + ".gif\" alt=\"精华\"/>\r\n");

	}	//end if


	if (topic.Special==1)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/pollsmall.gif\" alt=\"投票\" />\r\n");

	}	//end if


	if (topic.Special==2 || topic.Special==3)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/bonus.gif\" alt=\"悬赏\"/>\r\n");

	}	//end if


	if (topic.Special==4)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/debatesmall.gif\" alt=\"辩论\"/>\r\n");

	}	//end if


	if (topic.Attachment>0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/attachment.gif\" alt=\"附件\"/>\r\n");

	}	//end if


	if (topic.Rate>0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/agree.gif\" alt=\"正分\"/>\r\n");

	}	//end if


	if (topic.Rate<0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/disagree.gif\" alt=\"负分\"/>\r\n");

	}	//end if

	templateBuilder.Append("					</label>\r\n");

	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("						<input type=\"checkbox\" name=\"topicid\" value=\"" + topic.Tid.ToString().Trim() + "\" />\r\n");

	}	//end if


	if (topic.Replies>0)
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/topItem_exp.gif\" id=\"imgButton_" + topic.Tid.ToString().Trim() + "\" onclick=\"showtree(" + topic.Tid.ToString().Trim() + "," + ppp.ToString() + "," + config.Aspxrewrite.ToString().Trim() + ");\" class=\"cursor\" alt=\"展开帖子列表\" title=\"展开帖子列表\" />\r\n");

	}
	else
	{

	templateBuilder.Append("						<img src=\"templates/" + templatepath.ToString() + "/images/no-sublist.gif\" id=\"imgButton_" + topic.Tid.ToString().Trim() + "\" alt=\"闭合帖子列表\" />\r\n");

	}	//end if


	if (pageid==1 && forum.Allowthumbnail==1)
	{


	if (topic.Attachment==2)
	{

	templateBuilder.Append("							<span id=\"t_thumbnail_" + topic.Tid.ToString().Trim() + "\" onmouseover=\"showMenu(this.id, 0, 0, 1, 0)\">\r\n");

	if (forum.Applytopictype==1 && forum.Topictypeprefix==1)
	{

	templateBuilder.Append("								<em>\r\n");

	if (forum.Viewbytopictype==1 && topic.Topictypename!="")
	{

	templateBuilder.Append("								[<a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&typeid=" + topic.Typeid.ToString().Trim() + "\" >" + topic.Topictypename.ToString().Trim() + "</a>]\r\n");

	}
	else if (topic.Topictypename!="")
	{

	templateBuilder.Append("								[" + topic.Topictypename.ToString().Trim() + "]\r\n");

	}	//end if

	templateBuilder.Append("								</em>\r\n");

	}	//end if

	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	

	if (topic.Special==3)
	{

	 aspxrewriteurl = this.ShowBonusAspxRewrite(topic.Tid,0);
	

	}	//end if


	if (topic.Special==4)
	{

	 aspxrewriteurl = this.ShowDebateAspxRewrite(topic.Tid);
	

	}	//end if

	int ishtmltitle = Topics.GetMagicValue(topic.Magic, MagicType.HtmlTitle);
	

	if (ishtmltitle==1)
	{

	templateBuilder.Append("								<a href=\"" + aspxrewriteurl.ToString() + "\">" + Topics.GetHtmlTitle(topic.Tid).ToString().Trim() + "</a>\r\n");

	}
	else
	{

	templateBuilder.Append("								<a href=\"" + aspxrewriteurl.ToString() + "\">" + topic.Title.ToString().Trim() + "</a>\r\n");

	}	//end if


	if (topic.Folder=="new")
	{

	templateBuilder.Append("								<img src=\"templates/" + templatepath.ToString() + "/images/posts_new.gif\" />\r\n");

	}	//end if

	templateBuilder.Append("							</span>\r\n");

	}
	else
	{


	if (forum.Applytopictype==1 && forum.Topictypeprefix==1)
	{

	templateBuilder.Append("								<em>\r\n");

	if (forum.Viewbytopictype==1 && topic.Topictypename!="")
	{

	templateBuilder.Append("								[<a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&typeid=" + topic.Typeid.ToString().Trim() + "\" >" + topic.Topictypename.ToString().Trim() + "</a>]\r\n");

	}
	else if (topic.Topictypename!="")
	{

	templateBuilder.Append("								[" + topic.Topictypename.ToString().Trim() + "]\r\n");

	}	//end if

	templateBuilder.Append("								</em>\r\n");

	}	//end if

	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	

	if (topic.Special==3)
	{

	 aspxrewriteurl = this.ShowBonusAspxRewrite(topic.Tid,0);
	

	}	//end if


	if (topic.Special==4)
	{

	 aspxrewriteurl = this.ShowDebateAspxRewrite(topic.Tid);
	

	}	//end if

	int ishtmltitle = Topics.GetMagicValue(topic.Magic, MagicType.HtmlTitle);
	

	if (ishtmltitle==1)
	{

	templateBuilder.Append("								<a href=\"" + aspxrewriteurl.ToString() + "\">" + Topics.GetHtmlTitle(topic.Tid).ToString().Trim() + "</a>\r\n");

	}
	else
	{

	templateBuilder.Append("								<a href=\"" + aspxrewriteurl.ToString() + "\">" + topic.Title.ToString().Trim() + "</a>\r\n");

	}	//end if


	if (topic.Folder=="new")
	{

	templateBuilder.Append("								<img src=\"templates/" + templatepath.ToString() + "/images/posts_new.gif\"/>\r\n");

	}	//end if


	}	//end if


	}
	else
	{


	if (forum.Applytopictype==1 && forum.Topictypeprefix==1)
	{

	templateBuilder.Append("								<em>\r\n");

	if (forum.Viewbytopictype==1 && topic.Topictypename!="")
	{

	templateBuilder.Append("								[<a href=\"showforum.aspx?forumid=" + forumid.ToString() + "&typeid=" + topic.Typeid.ToString().Trim() + "\" >" + topic.Topictypename.ToString().Trim() + "</a>]\r\n");

	}
	else if (topic.Topictypename!="")
	{

	templateBuilder.Append("								[" + topic.Topictypename.ToString().Trim() + "]\r\n");

	}	//end if

	templateBuilder.Append("								</em>\r\n");

	}	//end if

	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic.Tid,0);
	

	if (topic.Special==3)
	{

	 aspxrewriteurl = this.ShowBonusAspxRewrite(topic.Tid,0);
	

	}	//end if


	if (topic.Special==4)
	{

	 aspxrewriteurl = this.ShowDebateAspxRewrite(topic.Tid);
	

	}	//end if

	int ishtmltitle = Topics.GetMagicValue(topic.Magic, MagicType.HtmlTitle);
	

	if (ishtmltitle==1)
	{

	templateBuilder.Append("							<a href=\"" + aspxrewriteurl.ToString() + "\">" + Topics.GetHtmlTitle(topic.Tid).ToString().Trim() + "</a>\r\n");

	}
	else
	{

	templateBuilder.Append("							<a href=\"" + aspxrewriteurl.ToString() + "\">" + topic.Title.ToString().Trim() + "</a>\r\n");

	}	//end if


	if (topic.Folder=="new")
	{

	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/posts_new.gif\"/>\r\n");

	}	//end if


	}	//end if


	if (topic.Special==2)
	{

	templateBuilder.Append("						- [悬赏 " + userextcreditsinfo.Name.ToString().Trim() + " <span class=\"bold\">" + topic.Price.ToString().Trim() + "</span> " + userextcreditsinfo.Unit.ToString().Trim() + "] \r\n");

	}
	else if (topic.Special==3)
	{

	templateBuilder.Append("						- [已结束的悬赏]\r\n");

	}
	else if (topic.Special==0)
	{


	if (topic.Price>0)
	{

	templateBuilder.Append("							- [售价 " + userextcreditsinfo.Name.ToString().Trim() + " <span class=\"bold\">" + topic.Price.ToString().Trim() + "</span> " + userextcreditsinfo.Unit.ToString().Trim() + "] \r\n");

	}	//end if


	}	//end if


	if (topic.Readperm>0)
	{

	templateBuilder.Append("						- [阅读权限 <span class=\"bold\">" + topic.Readperm.ToString().Trim() + "</span>] \r\n");

	}	//end if


	if (topic.Replies/ppp>0)
	{

	templateBuilder.Append("						<span class=\"threadpages\"><script type=\"text/javascript\">getpagenumbers(\"" + config.Extname.ToString().Trim() + "\", " + topic.Replies.ToString().Trim() + "," + ppp.ToString() + ",0,\"\"," + topic.Tid.ToString().Trim() + ");</" + "script></span>\r\n");

	}	//end if

	templateBuilder.Append("				</th>\r\n");
	templateBuilder.Append("				<td class=\"author\">\r\n");
	templateBuilder.Append("					<cite>\r\n");

	if (topic.Posterid==-1)
	{

	templateBuilder.Append("							游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(topic.Posterid);
	
	templateBuilder.Append("							<a href=\"" + aspxrewriteurl.ToString() + "\">" + topic.Poster.ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("					</cite>\r\n");
	templateBuilder.Append("					<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(topic.Postdatetime).ToString(" yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\"><strong>" + topic.Replies.ToString().Trim() + "</strong> / <em>" + topic.Views.ToString().Trim() + "</em></td>\r\n");
	templateBuilder.Append("				<td class=\"lastpost\">							\r\n");
	templateBuilder.Append("							<em><a href=\"showtopic.aspx?topicid=" + topic.Tid.ToString().Trim() + "&page=end#lastpost\">\r\n");
	templateBuilder.Append(Convert.ToDateTime(topic.Lastpost).ToString(" yyyy-MM-dd HH:mm"));
	templateBuilder.Append("</a></em>\r\n");
	templateBuilder.Append("							<cite>by\r\n");

	if (topic.Lastposterid==-1)
	{

	templateBuilder.Append("								游客\r\n");

	}
	else
	{

	templateBuilder.Append("								<a href=\"" + UserInfoAspxRewrite(topic.Lastposterid).ToString().Trim() + "\" target=\"_blank\">" + topic.Lastposter.ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("							</cite>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");

	if (pageid==1 && forum.Allowthumbnail==1)
	{


	if (topic.Attachment==2)
	{

	string timg = Attachments.GetThumbnailByTid(topic.Tid,160,ThumbnailType.Thumbnail);
	

	if (timg!="")
	{

	templateBuilder.Append("						<div id=\"t_thumbnail_" + topic.Tid.ToString().Trim() + "_menu\" style=\"display: none;\" class=\"popupmenu_popup\"><img src=\"" + timg.ToString() + "\" /></div>\r\n");

	}	//end if


	}	//end if


	}	//end if

	templateBuilder.Append("			<tr><td colspan=\"6\" id=\"divTopic" + topic.Tid.ToString().Trim() + "\" style=\" border:0;padding:0;\"></td></tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("		<!--NtForumList end-->\r\n");
	templateBuilder.Append("	</table>\r\n");

	if (useradminid>0 && ismoder)
	{

	templateBuilder.Append("	<div class=\"footoperation\"><strong>批量管理选项</strong> &nbsp;\r\n");
	templateBuilder.Append("		<input class=\"radio\" name=\"operat\" type=\"hidden\" />\r\n");
	templateBuilder.Append("		<input class=\"checkbox\" name=\"chkall\" onclick=\"checkall(this.form, 'topicid')\" type=\"checkbox\" />全选\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'delete';document.moderate.submit()\"/>删除主题</button>\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'move';document.moderate.submit()\" />移动主题</button>\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'highlight';document.moderate.submit()\" />高亮显示</button>\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'type';document.moderate.submit()\" />主题分类</button>\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'identify';document.moderate.submit()\" />鉴定主题</button>\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'close';document.moderate.submit()\" />关闭/打开主题</button>\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'displayorder';document.moderate.submit()\" />置顶/解除置顶</button>\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'digest';document.moderate.submit()\" />加入/解除精华</button>\r\n");
	templateBuilder.Append("		<button onclick=\"document.moderate.operat.value = 'bump';document.moderate.submit()\" />提升/下沉主题</button>\r\n");
	templateBuilder.Append("	</div>\r\n");

	}	//end if

	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</form>\r\n");
	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) { if(parseInt('" + config.Aspxrewrite.ToString().Trim() + "')==1) {window.location='showforum-" + forumid.ToString() + "-' + (parseInt(this.value) > 0 ? parseInt(this.value) : 1) + '" + config.Extname.ToString().Trim() + "';}else{window.location='showforum.aspx?forumid=" + forumid.ToString() + "&page=' + (parseInt(this.value) > 0 ? parseInt(this.value) : 1)}}\" size=\"4\" maxlength=\"9\" />页</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");

	if (userid<0||canposttopic)
	{

	templateBuilder.Append("	<span onmouseover=\"$('newspecial').id = 'newspecialtmp';this.id = 'newspecial'; if($('newspecial_menu').childNodes.length>0)  showMenu(this.id);\" id=\"Span1\" class=\"postbtn\"><a title=\"发新话题\" id=\"A1\" href=\"posttopic.aspx?forumid=" + forum.Fid.ToString().Trim() + "\" onmouseover=\"if($('newspecial_menu').childNodes.length>0)  showMenu(this.id);\"><img alt=\"发新话题\" src=\"templates/" + templatepath.ToString() + "/images/newtopic.gif\"/></a></span>\r\n");

	}	//end if

	templateBuilder.Append("</div>\r\n");

	if (canquickpost)
	{



	if (quickeditorad!="")
	{

	templateBuilder.Append("<div class=\"leaderboard\">" + quickeditorad.ToString() + "</div>\r\n");

	}	//end if

	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/post.js\"></" + "script>\r\n");
	templateBuilder.Append("<form method=\"post\" name=\"postform\" id=\"postform\" action=\"posttopic.aspx?forumid=" + forumid.ToString() + "\"\r\n");
	templateBuilder.Append("	enctype=\"multipart/form-data\" onsubmit=\"return validate(this);\">\r\n");
	templateBuilder.Append("<div id=\"quickpost\" class=\"box\"> \r\n");
	templateBuilder.Append("	<h4>快速发新话题</h4>\r\n");
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

	templateBuilder.Append("/> 使用个人签名</label></p>	\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"postform\">\r\n");
	templateBuilder.Append("		<h5>\r\n");
	templateBuilder.Append("		<label for=\"subject\">标题</label>\r\n");

	if (forum.Applytopictype==1 && topictypeselectoptions!="")
	{

	templateBuilder.Append("			<select name=\"typeid\" id=\"typeid\">\r\n");
	templateBuilder.Append("			" + topictypeselectoptions.ToString() + "\r\n");
	templateBuilder.Append("			</select>\r\n");

	}	//end if

	templateBuilder.Append("			<input type=\"text\" id=\"title\" name=\"title\" size=\"84\" tabindex=\"1\" value=\"\"/>\r\n");
	templateBuilder.Append("		</h5>\r\n");
	templateBuilder.Append("		<p><label>内容</label>\r\n");
	templateBuilder.Append("		<textarea rows=\"5\" cols=\"80\" name=\"message\" id=\"message\" onKeyDown=\"ctlent(event);\" tabindex=\"2\" class=\"autosave\"  style=\"background:url(" + quickbgadimg.ToString() + ") no-repeat 0 0; \" \r\n");

	if (quickbgadlink!="")
	{

	templateBuilder.Append(" onfocus=\"$('adlinkbtn').style.display='';$('closebtn').style.display='';\"\r\n");

	}	//end if

	templateBuilder.Append(" ></textarea>\r\n");
	templateBuilder.Append("		</p>\r\n");

	if (isseccode)
	{

	templateBuilder.Append("<p class=\"formcode\">验证码:\r\n");

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
	templateBuilder.Append("			<button type=\"submit\" id=\"postsubmit\" name=\"topicsubmit\" value=\"发表帖子\" tabindex=\"3\">发表帖子</button>[可按Ctrl+Enter发布]&nbsp;\r\n");
	templateBuilder.Append("			<input name=\"restoredata\" id=\"restoredata\" value=\"恢复数据\" tabindex=\"5\" title=\"恢复上次自动保存的数据\" onclick=\"loadData();\" type=\"button\" />&nbsp;<input name=\"topicsreset\" value=\"清空内容\" tabindex=\"6\" type=\"reset\" />\r\n");
	templateBuilder.Append("			<input type=\"hidden\" id=\"postbytopictype\" name=\"postbytopictype\" value=\"" + forum.Postbytopictype.ToString().Trim() + "\" tabindex=\"3\" />\r\n");
	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"smilies\">\r\n");

	if (Issmileyinsert==1)
	{

	templateBuilder.Append("			<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			var textobj = $('message');\r\n");
	templateBuilder.Append("			var lang = new Array();\r\n");
	templateBuilder.Append("			if (is_ie >= 5 || is_moz >= 2) {\r\n");
	templateBuilder.Append("				window.onbeforeunload = function () {\r\n");
	templateBuilder.Append("					saveData(textobj.value);\r\n");
	templateBuilder.Append("				};\r\n");
	templateBuilder.Append("				lang['post_autosave_none'] = \"没有可以恢复的数据\";\r\n");
	templateBuilder.Append("				lang['post_autosave_confirm'] = \"本操作将覆盖当前帖子内容，确定要恢复数据吗？\";\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("			else {\r\n");
	templateBuilder.Append("				$('restoredata').style.display = 'none';\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("			var bbinsert = parseInt('1');\r\n");
	templateBuilder.Append("			var smileyinsert = parseInt('1');\r\n");
	templateBuilder.Append("			var editor_id = 'posteditor';\r\n");
	templateBuilder.Append("			var smiliesCount = 9;\r\n");
	templateBuilder.Append("			var colCount = 3;\r\n");
	templateBuilder.Append("			var showsmiliestitle = 0;\r\n");
	templateBuilder.Append("			var smiliesIsCreate = 0;\r\n");
	templateBuilder.Append("			var scrMaxLeft; //表情滚动条宽度\r\n");
	templateBuilder.Append("			var smilies_HASH = {};\r\n");
	templateBuilder.Append("			</" + "script>\r\n");
	string defaulttypname = string.Empty;
	
	templateBuilder.Append("			<div class=\"navcontrol\">\r\n");
	templateBuilder.Append("			<div class=\"smiliepanel\">\r\n");
	templateBuilder.Append("					<div id=\"scrollbar\" class=\"scrollbar\">\r\n");
	templateBuilder.Append("							<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("								<tr>\r\n");

	int stype__loop__id=0;
	foreach(DataRow stype in smilietypes.Rows)
	{
		stype__loop__id++;


	if (stype__loop__id==1)
	{

	 defaulttypname = stype["code"].ToString().Trim();
	

	}	//end if

	templateBuilder.Append("									<td id=\"t_s_" + stype__loop__id.ToString() + "\"><div id=\"s_" + stype__loop__id.ToString() + "\" onclick=\"showsmiles(" + stype__loop__id.ToString() + ", '" + stype["code"].ToString().Trim() + "');\" \r\n");

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

	templateBuilder.Append("								</tr>\r\n");
	templateBuilder.Append("							</table>\r\n");
	templateBuilder.Append("					</div>\r\n");
	templateBuilder.Append("					<div id=\"scrlcontrol\">\r\n");
	templateBuilder.Append("						<img src=\"editor/images/smilie_prev_default.gif\" alt=\"向前\" onmouseover=\"if($('scrollbar').scrollLeft>0)this.src=this.src.replace(/_default|_selected/, '_hover');\" onmouseout=\"this.src=this.src.replace(/_hover|_selected/, '_default');\" onmousedown=\"if($('scrollbar').scrollLeft>0){this.src=this.src.replace(/_hover|_default/, '_selected');this.boder=1;}\" onmouseup=\"if($('scrollbar').scrollLeft>0)this.src=this.src.replace(/_selected/, '_hover');else{this.src=this.src.replace(/_selected|_hover/, '_default');}this.border=0;\" onclick=\"scrollSmilieTypeBar($('scrollbar'), 0-$('t_s_1').clientWidth);\"/>&nbsp;\r\n");
	templateBuilder.Append("						<img src=\"editor/images/smilie_next_default.gif\" alt=\"向后\" onmouseover=\"if($('scrollbar').scrollLeft<scrMaxLeft)this.src=this.src.replace(/_default|_selected/, '_hover');\" onmouseout=\"this.src=this.src.replace(/_hover|_selected/, '_default');\" onmousedown=\"if($('scrollbar').scrollLeft<scrMaxLeft){this.src=this.src.replace(/_hover|_default/, '_selected');this.boder=1;}\" onmouseup=\"if($('scrollbar').scrollLeft<scrMaxLeft)this.src=this.src.replace(/_selected/, '_hover');else{this.src=this.src.replace(/_selected|_hover/, '_default');}this.border=0;\" onclick=\"scrollSmilieTypeBar($('scrollbar'), $('t_s_1').clientWidth);\" />\r\n");
	templateBuilder.Append("					</div>\r\n");
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

	}	//end if

	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</form>\r\n");



	}	//end if


	if (userid<0||canposttopic)
	{

	templateBuilder.Append("	<ul class=\"popupmenu_popup newspecialmenu\" id=\"newspecial_menu\" style=\"display: none\">\r\n");

	if (forum.Allowspecialonly<=0)
	{

	templateBuilder.Append("	<li><a href=\"posttopic.aspx?forumid=" + forum.Fid.ToString().Trim() + "\">发新主题</a></li>\r\n");

	}	//end if

	 specialpost = forum.Allowpostspecial&1;
	

	if (specialpost==1 && usergroupinfo.Allowpostpoll==1)
	{

	templateBuilder.Append("	<li class=\"poll\"><a href=\"posttopic.aspx?forumid=" + forum.Fid.ToString().Trim() + "&type=poll\">发布投票</a></li>\r\n");

	}	//end if

	 specialpost = forum.Allowpostspecial&4;
	

	if (specialpost==4 && usergroupinfo.Allowbonus==1)
	{

	templateBuilder.Append("		<li class=\"reward\"><a href=\"posttopic.aspx?forumid=" + forum.Fid.ToString().Trim() + "&type=bonus\">发布悬赏</a></li>\r\n");

	}	//end if

	 specialpost = forum.Allowpostspecial&16;
	

	if (specialpost==16 && usergroupinfo.Allowdebate==1)
	{

	templateBuilder.Append("		<li class=\"debate\"><a href=\"posttopic.aspx?forumid=" + forum.Fid.ToString().Trim() + "&type=debate\">发起辩论</a></li>\r\n");

	}	//end if

	templateBuilder.Append("	</ul>\r\n");

	}	//end if

	templateBuilder.Append("<div id=\"footfilter\" class=\"box\">\r\n");
	templateBuilder.Append("	<form name=\"LookBySearch\" method=\"post\" action=\"showforum.aspx?search=1&forumid=" + forumid.ToString() + "&typeid=" + topictypeid.ToString() + "&filter=" + filter.ToString() + "\">\r\n");

	if (topictypeid==0)
	{

	templateBuilder.Append("		查看\r\n");
	templateBuilder.Append("		<select name=\"cond\" id=\"cond\">\r\n");
	templateBuilder.Append("		  <option value=\"0\" \r\n");

	if (cond==0)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">全部主题</option>\r\n");
	templateBuilder.Append("		  <option value=\"1\" \r\n");

	if (cond==1)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">1 天以来主题</option>\r\n");
	templateBuilder.Append("		  <option value=\"2\" \r\n");

	if (cond==2)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">2 天以来主题</option>\r\n");
	templateBuilder.Append("		  <option value=\"7\" \r\n");

	if (cond==7)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">1 周以来主题</option>\r\n");
	templateBuilder.Append("		  <option value=\"30\" \r\n");

	if (cond==30)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">1 个月以来主题</option>\r\n");
	templateBuilder.Append("		  <option value=\"90\" \r\n");

	if (cond==90)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">3 个月以来主题</option>\r\n");
	templateBuilder.Append("		  <option value=\"180\" \r\n");

	if (cond==180)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">6 个月以来主题</option>\r\n");
	templateBuilder.Append("		  <option value=\"365\" \r\n");

	if (cond==365)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">1 年以来主题</option>\r\n");
	templateBuilder.Append("		</select>\r\n");

	}	//end if

	templateBuilder.Append("		排序方式\r\n");
	templateBuilder.Append("		<select name=\"order\" id=\"order\">\r\n");
	templateBuilder.Append("		  <option value=\"1\" \r\n");

	if (order==1)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">最后回复时间</option>\r\n");
	templateBuilder.Append("		  <option value=\"2\" \r\n");

	if (order==2)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">发布时间</option>\r\n");
	templateBuilder.Append("		</select>\r\n");
	templateBuilder.Append("		<select name=\"direct\" id=\"direct\">\r\n");
	templateBuilder.Append("		  <option value=\"0\" \r\n");

	if (direct==0)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">按升序排列</option>\r\n");
	templateBuilder.Append("		  <option value=\"1\" \r\n");

	if (direct==1)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">按降序排列</option>\r\n");
	templateBuilder.Append("		</select>\r\n");
	templateBuilder.Append("		<button type=\"submit\">提交</button>\r\n");
	templateBuilder.Append("	</form>\r\n");

	if (config.Forumjump==1)
	{

	templateBuilder.Append("    <select onchange=\"if(this.options[this.selectedIndex].value != '') { jumpurl(this.options[this.selectedIndex].value," + config.Aspxrewrite.ToString().Trim() + ",'" + config.Extname.ToString().Trim() + "');}\">\r\n");
	templateBuilder.Append("		<option>论坛跳转...</option>\r\n");
	templateBuilder.Append("		" + forumlistboxoptions.ToString() + "\r\n");
	templateBuilder.Append("		</select>\r\n");

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
	templateBuilder.Append("		       } \r\n");
	templateBuilder.Append("		    }\r\n");
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


	if (config.Whosonlinestatus!=0 && config.Whosonlinestatus!=1)
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
	templateBuilder.Append("		<strong>在线用户</strong>- <em>" + forumtotalonline.ToString() + "</em> 人在线\r\n");
	templateBuilder.Append("	</h4>\r\n");
	templateBuilder.Append("	<dl id=\"onlinelist\">\r\n");
	templateBuilder.Append("		<dt>" + onlineiconlist.ToString() + "</dt>\r\n");
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
	
	templateBuilder.Append("							<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + onlineuser.Username.ToString().Trim() + "</a>\r\n");

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


	if (forum.Layer!=0)
	{

	templateBuilder.Append("<div class=\"legend\">\r\n");
	templateBuilder.Append("	<label><img src=\"templates/" + templatepath.ToString() + "/images/folder_new.gif\" alt=\"有新的回复\"/>有新回复</label>\r\n");
	templateBuilder.Append("	<label><img src=\"templates/" + templatepath.ToString() + "/images/folder_old.gif\" alt=\"无新回复\"/>无新回复</label>\r\n");
	templateBuilder.Append("	<label><img src=\"templates/" + templatepath.ToString() + "/images/folder_newhot.gif\" alt=\"多于15篇回复\"/>热门主题</label>\r\n");
	templateBuilder.Append("	<label><img src=\"templates/" + templatepath.ToString() + "/images/folder_closed.gif\" alt=\"关闭的主题\"/>关闭主题</label>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if

	templateBuilder.Append("		</div>\r\n");

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

	templateBuilder.Append("	</div>\r\n");

	}	//end if



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


	templateBuilder.Append("" + mediaad.ToString() + "\r\n");

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
