<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.topicadmin" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:05:52.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:05:52. 
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
	templateBuilder.Append("		<a href=\"" + config.Forumurl.ToString().Trim() + "\">" + config.Forumtitle.ToString().Trim() + "</a>  &raquo;  " + forumnav.ToString() + "  &raquo;  <strong>" + operationtitle.ToString() + "</strong>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (DNTRequest.GetString("operation")=="")
	{


	if (page_err==0)
	{

	templateBuilder.Append("<div class=\"mainbox formbox\">\r\n");
	templateBuilder.Append("<form id=\"moderate\" name=\"moderate\" method=\"post\" action=\"topicadmin.aspx?action=moderate&operation=" + operation.ToString() + "\">\r\n");
	templateBuilder.Append("		<input type=\"hidden\" name=\"topicid\" value=\"" + topiclist.ToString() + "\" />\r\n");
	templateBuilder.Append("		<input type=\"hidden\" name=\"forumid\" value=\"" + forumid.ToString() + "\" />\r\n");

	if (config.Aspxrewrite==1)
	{

	templateBuilder.Append("		<input type=\"hidden\" id=\"referer\" name=\"referer\" value=\"showforum-" + forumid.ToString() + "" + config.Extname.ToString().Trim() + "\" />\r\n");

	}
	else
	{

	templateBuilder.Append("		<input type=\"hidden\" id=\"referer\" name=\"referer\" value=\"showforum.aspx?forumid=" + forumid.ToString() + "\">\r\n");

	}	//end if

	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	var re = getQueryString(\"referer\");\r\n");
	templateBuilder.Append("	if (re != \"\")\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		$(\"referer\").value = unescape(re);\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("</" + "script>\r\n");
	templateBuilder.Append("	<h3>" + operationtitle.ToString() + "</h3>\r\n");
	templateBuilder.Append("	<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>用户名</th>\r\n");
	templateBuilder.Append("				<td>" + username.ToString() + "&nbsp;<a href=\"logout.aspx?userkey=" + userkey.ToString() + "\">退出登录</a></td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	if (operation=="highlight")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>字体样式</th>\r\n");
	templateBuilder.Append("				<td><input type=\"checkbox\" name=\"highlight_style_b\" value=\"B\" /> <strong>粗体</strong> <input type=\"checkbox\" name=\"highlight_style_i\" value=\"I\" /> <em>斜体</em><input type=\"checkbox\" name=\"highlight_style_u\" value=\"U\" /> <u>下划线</u>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>字体颜色:</th>\r\n");
	templateBuilder.Append("				<td><!--colorpicker层显示开始-->						\r\n");
	templateBuilder.Append("				<script type=\"text/javascript\" src=\"javascript/template_colorpicker.js\"></" + "script>\r\n");
	templateBuilder.Append("				<input type=\"text\" value=\"\" name=\"highlight_color\" id=\"highlight_color\"  size=\"7\" class=\"colorblue\" onfocus=\"this.className='colorfocus';\" onblur=\"this.className='colorblue';\" />\r\n");
	templateBuilder.Append("				<select name=\"highlight_colorselect\" id=\"highlight_colorselect\" onChange=\"selectoptioncolor(this)\" style=\"margin-bottom:2px;\">\r\n");
	templateBuilder.Append("					<option value=\"\">默认</option>  \r\n");
	templateBuilder.Append("					<option style=\"background:#FF0000\" value=\"#FF0000\"></option>  \r\n");
	templateBuilder.Append("					<option style=\"background:#FFA500\" value=\"#FFA500\"></option> \r\n");
	templateBuilder.Append("					<option style=\"background:#FFFF00\" value=\"#FFFF00\"></option> \r\n");
	templateBuilder.Append("					<option style=\"background:#008000\" value=\"#008000\"></option> \r\n");
	templateBuilder.Append("					<option style=\"background:#00FFFF\" value=\"#00FFFF\"></option> \r\n");
	templateBuilder.Append("					<option style=\"background:#0000FF\" value=\"#0000FF\"></option> \r\n");
	templateBuilder.Append("					<option style=\"background:#800080\" value=\"#800080\"></option> \r\n");
	templateBuilder.Append("					<option style=\"background:#808080\" value=\"#808080\"></option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("				<img class=\"img\" title=\"选择颜色\" src=\"templates/" + templatepath.ToString() + "/images/colorpicker.gif\" id=s_bgcolor onclick=\"IsShowColorPanel(this);\" style=\"cursor:hand; border:0px;\" />\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="displayorder")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"level\">级别</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");

	if (displayorder>0)
	{

	templateBuilder.Append("				<input type=\"radio\" value=\"0\" name=\"level\" />解除置顶\r\n");

	}	//end if

	templateBuilder.Append("				<input name=\"level\" type=\"radio\" value=\"1\"\r\n");

	if (displayorder<=1)
	{

	templateBuilder.Append("				 checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("				 /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" /> <input type=\"radio\" value=\"2\" name=\"level\"\r\n");

	if (displayorder==2)
	{

	templateBuilder.Append("				 checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("				 /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" />\r\n");
	templateBuilder.Append("				<input type=\"radio\" value=\"3\" name=\"level\"\r\n");

	if (displayorder==3)
	{

	templateBuilder.Append("				 checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("				 /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" />\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="digest")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"level\">级别</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");

	if (digest>0)
	{

	templateBuilder.Append("				<input type=\"radio\" value=\"0\" name=\"level\" />解除精华\r\n");

	}	//end if

	templateBuilder.Append("				<input name=\"level\" type=\"radio\" value=\"1\"\r\n");

	if (digest<=1)
	{

	templateBuilder.Append("				 checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("				 /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" /> <input type=\"radio\" value=\"2\" name=\"level\"\r\n");

	if (digest==2)
	{

	templateBuilder.Append("				 checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("				 /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" />\r\n");
	templateBuilder.Append("				<input type=\"radio\" value=\"3\" name=\"level\"\r\n");

	if (digest==3)
	{

	templateBuilder.Append("				 checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("				 /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" /><img src=\"templates/" + templatepath.ToString() + "/images/star_level1.gif\" width=\"16\" height=\"16\" />\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="move")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"moveto\">目标版块</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<select name=\"moveto\">\r\n");
	templateBuilder.Append("						" + forumlist.ToString() + "\r\n");
	templateBuilder.Append("					</select>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"type\">移动方式</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<input type=\"radio\" checked=\"checked\" value=\"normal\" name=\"type\" />\r\n");
	templateBuilder.Append("				移动主题&nbsp;&nbsp;<input type=\"radio\" value=\"redirect\" name=\"type\" /> 移动主题并在原来的论坛中保留转向\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="close")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"close\">操作</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<input type=\"radio\" checked=\"checked\" value=\"0\" name=\"close\" />\r\n");
	templateBuilder.Append("				打开主题&nbsp;&nbsp; <input type=\"radio\" value=\"1\" name=\"close\" /> 关闭主题\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="banpost")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"bandpost\">操作</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<input id=\"banpost1\" name=\"banpost\" type=\"radio\" value=\"0\" />取消屏蔽\r\n");
	templateBuilder.Append("					<input id=\"banpost2\" name=\"banpost\" type=\"radio\" value=\"-2\" checked/>屏蔽帖子\r\n");
	templateBuilder.Append("					<input type=\"hidden\" name=\"postid\" id=\"postid\" value=\"" + postidlist.ToString() + "\" />\r\n");
	templateBuilder.Append("				<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("					var status = getQueryString(\"banstatus\");\r\n");
	templateBuilder.Append("					if (status == \"0\") {\r\n");
	templateBuilder.Append("						$(\"banpost1\").checked = true;\r\n");
	templateBuilder.Append("						$(\"banpost2\").checked = false;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("					else {\r\n");
	templateBuilder.Append("						$(\"banpost2\").checked = true;\r\n");
	templateBuilder.Append("						$(\"banpost1\").checked = false;\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				</" + "script>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="bump")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"bumptype\">操作</lable></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<input id=\"bumptype1\" name=\"bumptype\" type=\"radio\" value=\"1\"  checked/>主题提升\r\n");
	templateBuilder.Append("					&nbsp;&nbsp; \r\n");
	templateBuilder.Append("					<input id=\"bumptype2\" name=\"bumptype\" type=\"radio\" value=\"-1\"/>主题下沉\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="copy")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"copyto\">目标论坛/分类</label></div>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<select name=\"copyto\">\r\n");
	templateBuilder.Append("						" + forumlist.ToString() + "\r\n");
	templateBuilder.Append("					</select>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="split")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"subject\">新主题的标题</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<input type=\"text\" id=\"\" name=\"subject\" size=\"45\" value=\"\" />\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="merge")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"othertid\">主题tid</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<INPUT size=\"10\" name=\"othertid\" ID=\"othertid\" />&nbsp;\r\n");

	if (config.Aspxrewrite==1)
	{

	templateBuilder.Append("					<SPAN class=\"smalltxt\">即将与这个主题合并的主题id,如showtopic-22.aspx，tid 为 22</SPAN>\r\n");

	}
	else
	{

	templateBuilder.Append("					<SPAN class=\"smalltxt\">即将与这个主题合并的主题id,如showtopic.aspx?topicid=22，tid 为 22</SPAN>\r\n");

	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="type")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"typeid\">目标分类</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("				<select name=\"typeid\" ID=\"typeid\">" + topictypeselectoptions.ToString() + "</select>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="rate")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"postid\">作者</label></th>\r\n");
	templateBuilder.Append("				<td>" + poster.ToString() + "<INPUT type=\"hidden\" size=\"10\" name=\"postid\" ID=\"postid\" value=\"" + postidlist.ToString() + "\" /></td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>标题:</th>\r\n");
	templateBuilder.Append("				<td>" + title.ToString() + "</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"score\">评分</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");

	int score__loop__id=0;
	foreach(DataRow score in scorelist.Rows)
	{
		score__loop__id++;

	templateBuilder.Append("					<div style=\"padding-left:3px;margin-top:3px;\">\r\n");
	templateBuilder.Append("					<select name=\"select\" onchange=\"this.form.score" + score["ScoreCode"].ToString().Trim() + ".value=this.value\">\r\n");
	templateBuilder.Append("					  <option value=\"0\" selected=\"selected\">" + score["ScoreName"].ToString().Trim() + "</option>\r\n");
	templateBuilder.Append("					  <option value=\"0\">----</option>\r\n");
	templateBuilder.Append("					  " + score["options"].ToString().Trim() + "\r\n");
	templateBuilder.Append("					</select>\r\n");
	templateBuilder.Append("					<input size=\"3\" value=\"0\" name=\"score\" id=\"score" + score["ScoreCode"].ToString().Trim() + "\" />\r\n");
	templateBuilder.Append("					<input type=\"hidden\" value=\"" + score["ScoreCode"].ToString().Trim() + "\" name=\"extcredits\" /> (今日还能评分 " + score["MaxInDay"].ToString().Trim() + " )\r\n");
	templateBuilder.Append("					</div>\r\n");

	}	//end loop

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="cancelrate")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"postid\">作者</label></th>\r\n");
	templateBuilder.Append("				<td>" + poster.ToString() + "<input type=\"hidden\" size=\"10\" name=\"postid\" value=\"" + postidlist.ToString() + "\" /></td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>标题:</th>\r\n");
	templateBuilder.Append("				<td>" + title.ToString() + "</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="identify")
	{

	templateBuilder.Append("		" + identifyjsarray.ToString() + "\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			function changeindentify(imgid)\r\n");
	templateBuilder.Append("			{\r\n");
	templateBuilder.Append("				if (imgid != \"0\" && imgid != \"-1\")\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					$(\"identify_preview\").src = \"images/identify/\" + topicidentify[imgid];\r\n");
	templateBuilder.Append("					$(\"identify_preview\").style.display = \"\";\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				else\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					$(\"identify_preview\").style.display = \"none\";\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"selectidentify\">鉴定</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<select name=\"selectidentify\" id=\"selectidentify\" onchange=\"changeindentify(this.value)\">\r\n");
	templateBuilder.Append("						<option value=\"0\" selected=\"selected\">请选择</option>\r\n");
	templateBuilder.Append("						<option value=\"-1\">* 取消鉴定 *</option>\r\n");

	int identify__loop__id=0;
	foreach(TopicIdentify identify in identifylist)
	{
		identify__loop__id++;

	templateBuilder.Append("						<option value=\"" + identify.Identifyid.ToString().Trim() + "\">" + identify.Name.ToString().Trim() + "</option>						  \r\n");

	}	//end loop

	templateBuilder.Append("					</select>		\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>图例预览</th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<img id=\"identify_preview\" style=\"display: none;\" />\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="delposts")
	{

	templateBuilder.Append("<input type=\"hidden\" size=\"10\" name=\"postid\" ID=\"postid\" value=\"" + postidlist.ToString() + "\" />\r\n");

	}	//end if


	if (operation!="identify" && operation!="bonus")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"selectreason\">操作原因:</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("				<select id=\"selectreason\" name=\"selectreason\" size=\"6\" style=\"width: 8em;height:8em; \" onchange=\"this.form.reason.value=this.value\">\r\n");
	templateBuilder.Append("                  <option value=\"\">自定义</option>\r\n");
	templateBuilder.Append("                  <option value=\"\">--------</option>\r\n");
	templateBuilder.Append("                  <option value=\"广告/SPAM\">广告</option>\r\n");
	templateBuilder.Append("                  <option value=\"恶意灌水\">恶意灌水</option>\r\n");
	templateBuilder.Append("                  <option value=\"违规内容\">违规内容</option>\r\n");
	templateBuilder.Append("                  <option value=\"发错版块\">发错版块</option>\r\n");
	templateBuilder.Append("                  <option value=\"文不对题\">文不对题</option>\r\n");
	templateBuilder.Append("                  <option value=\"重复发帖\">重复发帖</option>\r\n");
	templateBuilder.Append("                  <option value=\"屡教不改\">屡教不改</option>\r\n");
	templateBuilder.Append("                  <option value=\"已经过期\">已经过期</option>\r\n");
	templateBuilder.Append("                  <option value=\"\">--------</option>\r\n");
	templateBuilder.Append("                  <option value=\"我很赞同\">我很赞同</option>\r\n");
	templateBuilder.Append("                  <option value=\"精品文章\">精品文章</option>\r\n");
	templateBuilder.Append("                  <option value=\"原创内容\">原创内容</option>\r\n");
	templateBuilder.Append("				  <option value=\"鼓励分享\">鼓励分享</option>\r\n");
	templateBuilder.Append("                </select>\r\n");
	templateBuilder.Append("				<textarea name=\"reason\" style=\"height: 8em; width:20em; margin-bottom:-2px;\" class=\"colorblue\" onkeydown=\"if(this.value.length>200){ alert('操作原因不能多于200个字符');return false; }\"></textarea>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="split")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"postid\">选择内容</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");

	int post__loop__id=0;
	foreach(DataRow post in postlist.Rows)
	{
		post__loop__id++;

	templateBuilder.Append("<input name=\"postid\" type=\"checkbox\" value=\"" + post["pid"].ToString().Trim() + "\" /><strong>" + post["poster"].ToString().Trim() + "</strong><br />\r\n");
	templateBuilder.Append("						" + post["message"].ToString().Trim() + "<br />\r\n");

	}	//end loop

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="bonus")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"postbonus\">给分情况</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("				<div style=\"position: relative;\">\r\n");
	templateBuilder.Append("					<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("						var reg = /^\\d+$/i;\r\n");
	templateBuilder.Append("						$('moderate').onsubmit = function (){\r\n");
	templateBuilder.Append("							if (getCostBonus() != " + topicinfo.Price.ToString().Trim() + ")\r\n");
	templateBuilder.Append("							{\r\n");
	templateBuilder.Append("								alert('分数总和与悬赏总分不相符');\r\n");
	templateBuilder.Append("								return false;\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("							return true;\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						function getCostBonus()\r\n");
	templateBuilder.Append("						{\r\n");
	templateBuilder.Append("							var bonusboxs = document.getElementsByName('postbonus');\r\n");
	templateBuilder.Append("							var costbonus = 0;\r\n");
	templateBuilder.Append("							for (var i = 0; i < bonusboxs.length ; i ++ )\r\n");
	templateBuilder.Append("							{\r\n");
	templateBuilder.Append("								var bonus = isNaN(parseInt(bonusboxs[i].value)) ? 0 : parseInt(bonusboxs[i].value);\r\n");
	templateBuilder.Append("								costbonus += bonus;\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("							return costbonus;\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						function checkInt(obj)\r\n");
	templateBuilder.Append("						{				\r\n");
	templateBuilder.Append("							if (!reg.test(obj.value))\r\n");
	templateBuilder.Append("							{\r\n");
	templateBuilder.Append("								obj.value = 0;\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						function bonushint(obj)\r\n");
	templateBuilder.Append("						{							\r\n");
	templateBuilder.Append("							var costbonus = getCostBonus();\r\n");
	templateBuilder.Append("							var leftbonus = " + topicinfo.Price.ToString().Trim() + " - costbonus;\r\n");
	templateBuilder.Append("							$('bonus_menu').innerHTML = '总悬赏分: ' + " + topicinfo.Price.ToString().Trim() + " + '<br />当前可用: ' + leftbonus;\r\n");
	templateBuilder.Append("							$('bonus_menu').style.left = obj.offsetLeft + obj.offsetWidth/2 + 'px';\r\n");
	templateBuilder.Append("							$('bonus_menu').style.top = obj.offsetTop + obj.offsetHeight + 'px';\r\n");
	templateBuilder.Append("							$('bonus_menu').style.display = '';\r\n");
	templateBuilder.Append("							obj.focus();\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						function closebonushint(obj)\r\n");
	templateBuilder.Append("						{\r\n");
	templateBuilder.Append("							$('bonus_menu').style.display = 'none';\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						var originalColor = '';\r\n");
	templateBuilder.Append("						var valuableColor = '#cce2f8';\r\n");
	templateBuilder.Append("						var bestColor = '#ff9d25';\r\n");
	templateBuilder.Append("						function rgbToColor(forecolor) {\r\n");
	templateBuilder.Append("							if(forecolor == null) {\r\n");
	templateBuilder.Append("								forecolor = '';\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("							if(!is_moz && !is_opera) {\r\n");
	templateBuilder.Append("								if (forecolor.indexOf('#') == 0)\r\n");
	templateBuilder.Append("								{\r\n");
	templateBuilder.Append("									forecolor = forecolor.replace('#', '0x');	\r\n");
	templateBuilder.Append("								}\r\n");
	templateBuilder.Append("								return rgbhexToColor(((forecolor >> 16) & 0xFF).toString(16), ((forecolor >> 8) & 0xFF).toString(16), (forecolor & 0xFF).toString(16));\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("							if(forecolor.toLowerCase().indexOf('rgb') == 0) {\r\n");
	templateBuilder.Append("								var matches = forecolor.match(/^rgb\\s*\\(([0-9]+),\\s*([0-9]+),\\s*([0-9]+)\\)$/);\r\n");
	templateBuilder.Append("								if(matches) {\r\n");
	templateBuilder.Append("									return rgbhexToColor((matches[1] & 0xFF).toString(16), (matches[2] & 0xFF).toString(16), (matches[3] & 0xFF).toString(16));\r\n");
	templateBuilder.Append("								} else {\r\n");
	templateBuilder.Append("									return rgbToColor(null);\r\n");
	templateBuilder.Append("								}\r\n");
	templateBuilder.Append("							} else {\r\n");
	templateBuilder.Append("								return forecolor;\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						function rgbhexToColor(r, g, b) {\r\n");
	templateBuilder.Append("							var coloroptions = {'#000000' : 'Black', '#a0522d' : 'Sienna', '#556b2f' : 'DarkOliveGreen', '#006400' : 'DarkGreen', '#483d8b' : 'DarkSlateBlue', '#000080' : 'Navy', '#4b0082' : 'Indigo', '#2f4f4f' : 'DarkSlateGray', '#8b0000' : 'DarkRed', '#ff8c00' : 'DarkOrange', '#808000' : 'Olive', '#008000' : 'Green', '#008080' : 'Teal', '#0000ff' : 'Blue', '#708090' : 'SlateGray', '#696969' : 'DimGray', '#ff0000' : 'Red', '#f4a460' : 'SandyBrown', '#9acd32' : 'YellowGreen', '#2e8b57' : 'SeaGreen', '#48d1cc' : 'MediumTurquoise', '#4169e1' : 'RoyalBlue', '#800080' : 'Purple', '#808080' : 'Gray', '#ff00ff' : 'Magenta', '#ffa500' : 'Orange', '#ffff00' : 'Yellow', '#00ff00' : 'Lime', '#00ffff' : 'Cyan', '#00bfff' : 'DeepSkyBlue', '#9932cc' : 'DarkOrchid', '#c0c0c0' : 'Silver', '#ffc0cb' : 'Pink', '#f5deb3' : 'Wheat', '#fffacd' : 'LemonChiffon', '#98fb98' : 'PaleGreen', '#afeeee' : 'PaleTurquoise', '#add8e6' : 'LightBlue', '#dda0dd' : 'Plum', '#ffffff' : 'White'};\r\n");
	templateBuilder.Append("							var color = '#' + (str_pad(r, 2, 0) + str_pad(g, 2, 0) + str_pad(b, 2, 0));\r\n");
	templateBuilder.Append("							return coloroptions[color] ? coloroptions[color] : color;\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						function str_pad(text, length, padstring) {\r\n");
	templateBuilder.Append("							text += '';\r\n");
	templateBuilder.Append("							padstring += '';\r\n");
	templateBuilder.Append("							if(text.length < length) {\r\n");
	templateBuilder.Append("								padtext = padstring;\r\n");
	templateBuilder.Append("								while(padtext.length < (length - text.length)) {\r\n");
	templateBuilder.Append("									padtext += padstring;\r\n");
	templateBuilder.Append("								}\r\n");
	templateBuilder.Append("								text = padtext.substr(0, (length - text.length)) + text;\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("							return text;\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						function setValuableOrBestAnswer(obj, pid)\r\n");
	templateBuilder.Append("						{\r\n");
	templateBuilder.Append("							switch (rgbToColor(obj.style.backgroundColor))\r\n");
	templateBuilder.Append("							{\r\n");
	templateBuilder.Append("								case valuableColor:				\r\n");
	templateBuilder.Append("									var valuableAnswers = $('valuableAnswers').value.split(',');\r\n");
	templateBuilder.Append("									$('valuableAnswers').value = '';\r\n");
	templateBuilder.Append("									for (var i = 0; i < valuableAnswers.length ; i++)\r\n");
	templateBuilder.Append("									{\r\n");
	templateBuilder.Append("										if (valuableAnswers[i] != pid && valuableAnswers[i] != '')\r\n");
	templateBuilder.Append("										{\r\n");
	templateBuilder.Append("											$('valuableAnswers').value += ',' + valuableAnswers[i];\r\n");
	templateBuilder.Append("										}\r\n");
	templateBuilder.Append("									}\r\n");
	templateBuilder.Append("									var options = document.getElementsByName('answeroption');\r\n");
	templateBuilder.Append("									for (var i = 0; i < options.length ; i++ )\r\n");
	templateBuilder.Append("									{\r\n");
	templateBuilder.Append("										if (options[i].style.backgroundColor == bestColor)\r\n");
	templateBuilder.Append("										{\r\n");
	templateBuilder.Append("											options[i].style.backgroundColor = valuableColor;\r\n");
	templateBuilder.Append("											$('valuableAnswers').value += ',' + $('bestAnswer').value;\r\n");
	templateBuilder.Append("										}										\r\n");
	templateBuilder.Append("									}\r\n");
	templateBuilder.Append("									obj.style.backgroundColor = bestColor;\r\n");
	templateBuilder.Append("									$('bestAnswer').value = pid;\r\n");
	templateBuilder.Append("									break;\r\n");
	templateBuilder.Append("								case bestColor:\r\n");
	templateBuilder.Append("									obj.style.backgroundColor = originalColor;\r\n");
	templateBuilder.Append("									$('bestAnswer').value= '';\r\n");
	templateBuilder.Append("									break;\r\n");
	templateBuilder.Append("								default:\r\n");
	templateBuilder.Append("									obj.style.backgroundColor = valuableColor;\r\n");
	templateBuilder.Append("									if (!in_array(pid, $('valuableAnswers').value.split(',')))\r\n");
	templateBuilder.Append("									{\r\n");
	templateBuilder.Append("										$('valuableAnswers').value += ',' + pid;\r\n");
	templateBuilder.Append("									}\r\n");
	templateBuilder.Append("									break;\r\n");
	templateBuilder.Append("							}							\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("					</" + "script>\r\n");
	templateBuilder.Append("					提示: 每次点击答案可以切换\"最佳答案\"与\"有价值的答案\"的颜色状态.&nbsp;&nbsp;&nbsp;&nbsp;颜色含义:<script type=\"text/javascript\">document.write('<span style=\"padding: 3px; color: #fff;background-color: ' + bestColor + ';\">最佳答案</span><span style=\"margin-left: 3px;padding: 3px; color: #fff;background-color: ' + valuableColor + ';\">有价值的答案</span><br /><br />');</" + "script>\r\n");
	templateBuilder.Append("					<input type=\"hidden\" id=\"bestAnswer\" name=\"bestAnswer\" value=\"\" />\r\n");
	templateBuilder.Append("					<input type=\"hidden\" id=\"valuableAnswers\" name=\"valuableAnswers\" value=\"\" />\r\n");

	int post__loop__id=0;
	foreach(DataRow post in postlist.Rows)
	{
		post__loop__id++;

	templateBuilder.Append("					<div name=\"answeroption\" \r\n");

	if (Utils.StrToInt(post["posterid"].ToString().Trim(), 0)!=topicinfo.Posterid)
	{

	templateBuilder.Append("onclick=\"setValuableOrBestAnswer(this, " + post["pid"].ToString().Trim() + ");\" style=\"cursor: pointer; width: 100%;\"\r\n");

	}	//end if

	templateBuilder.Append(">\r\n");
	templateBuilder.Append("					<strong>" + post["poster"].ToString().Trim() + "</strong>&nbsp; \r\n");

	if (Utils.StrToInt(post["posterid"].ToString().Trim(), 0)!=topicinfo.Posterid)
	{

	templateBuilder.Append("得分: <input name=\"postbonus\" id=\"bonus_" + post["pid"].ToString().Trim() + "\" type=\"text\" value=\"0\" size=\"3\" maxlength=\"9\" onblur=\"checkInt(this);\" onmouseover=\"bonushint(this);\" onmouseout=\"closebonushint(this);\" /><input name=\"addons\" type=\"hidden\" value=\"" + post["posterid"].ToString().Trim() + "|" + post["pid"].ToString().Trim() + "|" + post["poster"].ToString().Trim() + "\" />\r\n");

	}
	else
	{

	templateBuilder.Append("不能给自己分\r\n");

	}	//end if

	templateBuilder.Append("<br />\r\n");
	templateBuilder.Append("						" + post["message"].ToString().Trim() + "<br />\r\n");
	templateBuilder.Append("					</div><br />\r\n");

	}	//end loop

	templateBuilder.Append("					<div id=\"bonus_menu\" style=\"position: absolute; z-index: 50; background: yellow;\"></div>\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (operation=="delete" || operation=="delposts")
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"reserveattach\">附件</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("				<input name=\"reserveattach\" type=\"checkbox\" value=\"1\" />保留附件(附件可能正在被相册使用, 如果希望保留, 请选中此选项)				\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if


	if (donext==1)
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th><label for=\"next\">后续</label></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("				<input name=\"next\" type=\"radio\" checked=\"checked\" value=\"\" />无\r\n");

	if (operation!="highlight")
	{

	templateBuilder.Append("				<input type=\"radio\" value=\"highlight\" name=\"next\" />高亮显示\r\n");

	}	//end if


	if (operation!="displayorder")
	{

	templateBuilder.Append("				<input type=\"radio\" value=\"displayorder\" name=\"next\" />置顶/解除置顶\r\n");

	}	//end if


	if (operation!="digest")
	{

	templateBuilder.Append("				<input type=\"radio\" value=\"digest\" name=\"next\" />加入/解除精华\r\n");

	}	//end if


	if (operation!="close")
	{

	templateBuilder.Append("				<input type=\"radio\" value=\"close\" name=\"next\" />打开/关闭主题\r\n");

	}	//end if

	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>&nbsp;</td>\r\n");
	templateBuilder.Append("				<td>\r\n");

	if (issendmessage)
	{

	templateBuilder.Append("				<input type=\"checkbox\" disabled checked=\"checked\"/>\r\n");
	templateBuilder.Append("				<input name=\"sendmessage\" type=\"hidden\" id=\"sendmessage\" value=\"1\"/>\r\n");

	}
	else
	{

	templateBuilder.Append("				<input name=\"sendmessage\" type=\"checkbox\" id=\"sendmessage\" value=\"1\"/>\r\n");

	}	//end if

	templateBuilder.Append("				发短消息通知作者\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>	\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>&nbsp;</th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<input type=\"submit\" value=\"提  交\" name=\"modsubmit\"/>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	if (operation=="cancelrate")
	{


	if (ratelogcount>0)
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("			<td colspan=\"6\">\r\n");
	templateBuilder.Append("				<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("				<thead>\r\n");
	templateBuilder.Append("					<tr>\r\n");
	templateBuilder.Append("						<td colspan=\"6\" align=\"left\">评分日志</td>\r\n");
	templateBuilder.Append("					</tr>\r\n");
	templateBuilder.Append("				</thead>\r\n");
	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td><input name=\"chkall\" type=\"checkbox\"  onclick=\"checkall(this.form, 'ratelogid')\" />删除</td>\r\n");
	templateBuilder.Append("					<td>用户名</td>\r\n");
	templateBuilder.Append("					<td>时间</td>\r\n");
	templateBuilder.Append("					<td>评分单位</td>\r\n");
	templateBuilder.Append("					<td>评分分值</td>\r\n");
	templateBuilder.Append("					<td>理由</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");

	int rateloginfo__loop__id=0;
	foreach(DataRow rateloginfo in ratelog.Rows)
	{
		rateloginfo__loop__id++;

	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td><input name=\"ratelogid\" type=\"checkbox\"  value=\"" + rateloginfo["id"].ToString().Trim() + "\" /></td>\r\n");
	templateBuilder.Append("					<td>" + rateloginfo["username"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("					<td>" + rateloginfo["postdatetime"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("					<td>" + rateloginfo["extcreditname"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("					<td>" + rateloginfo["score"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("					<td>" + rateloginfo["reason"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				</tr>\r\n");

	}	//end loop

	templateBuilder.Append("				</table>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("			</tbody>\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</form>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (operation=="highlight")
	{

	templateBuilder.Append("		<div  id=\"ColorPicker\" title=\"ColorPicker\" style=\"display:none;cursor:crosshair;border: black 1px solid;position: absolute; z-index: 10;background-color: aliceblue; width:250px;background: #FFFFFF;padding: 4px; margin-left:150px;\" onmouseover=\"ShowColorPanel();\">\r\n");
	templateBuilder.Append("						<table border=\"0\" cellPadding=\"0\" cellSpacing=\"10\" onmouseover=\"ShowColorPanel();\">\r\n");
	templateBuilder.Append("						<tr>\r\n");
	templateBuilder.Append("						<td>\r\n");
	templateBuilder.Append("						<table border=\"0\" cellPadding=\"0\" cellSpacing=\"0\" id=\"ColorTable\" style=\"cursor:crosshair;\"  onmouseover=\"ShowColorPanel();\">\r\n");
	templateBuilder.Append("						<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("						function wc(r, g, b, n){\r\n");
	templateBuilder.Append("							r = ((r * 16 + r) * 3 * (15 - n) + 0x80 * n) / 15;\r\n");
	templateBuilder.Append("							g = ((g * 16 + g) * 3 * (15 - n) + 0x80 * n) / 15;\r\n");
	templateBuilder.Append("							b = ((b * 16 + b) * 3 * (15 - n) + 0x80 * n) / 15;\r\n");
	templateBuilder.Append("							document.write('<td BGCOLOR=#' + ToHex(r) + ToHex(g) + ToHex(b) + ' title=\"#' + ToHex(r) + ToHex(g) + ToHex(b) + '\" height=8 width=8 onmouseover=\"ColorTableMouseOver(this)\" onmousedown=\"ColorTableMouseDown(this)\"  onmouseout=\"ColorTableMouseOut(this)\" ></TD>');\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						var cnum = new Array(1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0);\r\n");
	templateBuilder.Append("						for(i = 0; i < 16; i ++){\r\n");
	templateBuilder.Append("							document.write('<TR>');\r\n");
	templateBuilder.Append("							for(j = 0; j < 30; j ++){\r\n");
	templateBuilder.Append("								n1 = j % 5;\r\n");
	templateBuilder.Append("								n2 = Math.floor(j / 5) * 3;\r\n");
	templateBuilder.Append("								n3 = n2 + 3;\r\n");
	templateBuilder.Append("								wc((cnum[n3] * n1 + cnum[n2] * (5 - n1)),\r\n");
	templateBuilder.Append("								(cnum[n3 + 1] * n1 + cnum[n2 + 1] * (5 - n1)),\r\n");
	templateBuilder.Append("								(cnum[n3 + 2] * n1 + cnum[n2 + 2] * (5 - n1)), i);\r\n");
	templateBuilder.Append("							}\r\n");
	templateBuilder.Append("							document.writeln('</TR>');\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						</" + "script>\r\n");
	templateBuilder.Append("						</table></td>\r\n");
	templateBuilder.Append("						<td>\r\n");
	templateBuilder.Append("						<table border=\"0\" cellPadding=\"0\" cellSpacing=\"0\" id=\"GrayTable\" style=\"CURSOR: hand;cursor:crosshair;\"  onmouseover=\"ShowColorPanel();\">\r\n");
	templateBuilder.Append("						<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("						for(i = 255; i >= 0; i -= 8.5)\r\n");
	templateBuilder.Append("						document.write('<tr BGCOLOR=#' + ToHex(i) + ToHex(i) + ToHex(i) + '><td TITLE=' + Math.floor(i * 16 / 17) + ' height=4 width=20 onmouseover=\"GrayTableMouseOver(this)\" onmousedown=\"GrayTableMouseDown(this)\"  onmouseout=\"GrayTableMouseOut(this)\" ></td></tr>');\r\n");
	templateBuilder.Append("						</" + "script>\r\n");
	templateBuilder.Append("						</table></td></tr></table>\r\n");
	templateBuilder.Append("						<table border=\"0\" cellPadding=\"0\" cellSpacing=\"10\" onmouseover=\"ShowColorPanel();\">\r\n");
	templateBuilder.Append("						<tr>\r\n");
	templateBuilder.Append("						<td rowSpan=\"2\">选中色彩\r\n");
	templateBuilder.Append("						<table border=\"1\" cellPadding=\"0\" cellSpacing=\"0\" height=\"30\" id=\"ShowColor\" width=\"40\" bgcolor=\"\">\r\n");
	templateBuilder.Append("						<tr>\r\n");
	templateBuilder.Append("						<td></td></tr></table></td>\r\n");
	templateBuilder.Append("						<td rowSpan=2>基色: <span id=\"RGB\"></span><br />亮度: <span id=\"GRAY\">120</span><br />代码: <input id=\"SelColor\" size=\"7\" value=\"\" border=\"0\" name=\"SelColor\" /></TD>\r\n");
	templateBuilder.Append("						<td><input type=\"button\" onclick=\"javascript:ColorPickerOK();\" value=\"确定\" ID=\"ok\"/></td></tr>\r\n");
	templateBuilder.Append("						<tr>\r\n");
	templateBuilder.Append("						<td><input type=\"button\" onclick=\"javascript:document.getElementById('highlight_color').value='';document.getElementById('s_bgcolor').style.background='#FFFFFF';HideColorPanel();\" value=\"取消\" ID=\"Button2\" NAME=\"Button2\"/></td></tr></table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("						<!--colorpicker层显示结束-->\r\n");

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
