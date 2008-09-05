<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.search" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:04:04.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:04:04. 
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
	templateBuilder.Append("		<a href=\"" + config.Forumurl.ToString().Trim() + "\">" + config.Forumtitle.ToString().Trim() + "</a> &raquo; <strong>搜索</strong>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div id=\"headsearch\">\r\n");
	templateBuilder.Append("		<div id=\"search\">\r\n");

	if (usergroupinfo.Allowsearch>0)
	{


	if (searchid!=-1 || searchpost)
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


	}	//end if

	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (page_err==0)
	{


	if (searchpost)
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


	if (searchid==-1)
	{

	templateBuilder.Append("<div id=\"options_item\">\r\n");
	templateBuilder.Append("	<div id=\"postoptions\">\r\n");
	templateBuilder.Append("		<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"posttableid\">选择分表</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"posttableid\" id=\"posttableid\">\r\n");

	int table__loop__id=0;
	foreach(DataRow table in tablelist.Rows)
	{
		table__loop__id++;

	templateBuilder.Append("					<option value=\"" + table["id"].ToString().Trim() + "\">" + table["description"].ToString().Trim() + "\r\n");

	if (Utils.StrToInt(table__loop__id, 0)==1)
	{

	templateBuilder.Append("(当前使用)\r\n");

	}	//end if

	templateBuilder.Append("</option>\r\n");

	}	//end loop

	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"searchtime\">时间</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"searchtime\" id=\"searchtime\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">全部时间</option>\r\n");
	templateBuilder.Append("				  <option value=\"-1\">1天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-2\">2天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-3\">3天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-7\">1周前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-30\">1个月前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-90\">3个月前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-180\">半年前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-365\">1年前</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("				  <input type=\"radio\" name=\"searchtimetype\" value=\"1\" />\r\n");
	templateBuilder.Append("				之前\r\n");
	templateBuilder.Append("				<input name=\"searchtimetype\" type=\"radio\" value=\"0\" checked />\r\n");
	templateBuilder.Append("				之后\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"resultorder\">结果排序</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"resultorder\" id=\"resultorder\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">最后回复时间</option>\r\n");
	templateBuilder.Append("				  <option value=\"1\">发表时间</option>\r\n");
	templateBuilder.Append("				  <option value=\"2\">回复数量</option>\r\n");
	templateBuilder.Append("				  <option value=\"3\">查看次数</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"resultordertype\" value=\"1\" />\r\n");
	templateBuilder.Append("				升序\r\n");
	templateBuilder.Append("				<input name=\"resultordertype\" type=\"radio\" value=\"0\" checked />\r\n");
	templateBuilder.Append("				降序\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"searchforumid\">搜索范围</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"searchforumid\" size=\"12\" style=\"width:450px\" multiple=\"multiple\" id=\"searchforumid\">\r\n");
	templateBuilder.Append("				 <option selected value=\"\">---------- 所有版块(默认) ----------</option>\r\n");
	templateBuilder.Append("					<!--模版中所有版块的下拉框中一定要加入value=\"\"否则会提示没有选择版块-->\r\n");
	templateBuilder.Append("					" + forumlist.ToString() + "\r\n");
	templateBuilder.Append("				 </select>\r\n");
	templateBuilder.Append("				 <p>(按Ctrl或Shift键可以多选,不选择)</p>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div id=\"spacepostoptions\">\r\n");
	templateBuilder.Append("		<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"searchtime\">时间</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"searchtime\" id=\"searchtime\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">全部时间</option>\r\n");
	templateBuilder.Append("				  <option value=\"-1\">1天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-2\">2天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-3\">3天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-7\">1周前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-30\">1个月前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-90\">3个月前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-180\">半年前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-365\">1年前</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("				  <input type=\"radio\" name=\"searchtimetype\" value=\"1\" />\r\n");
	templateBuilder.Append("				之前\r\n");
	templateBuilder.Append("				<input name=\"searchtimetype\" type=\"radio\" value=\"0\" checked />\r\n");
	templateBuilder.Append("				之后\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"resultorder\">结果排序</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"resultorder\" id=\"resultorder\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">发表时间</option>\r\n");
	templateBuilder.Append("				  <option value=\"1\">回复数量</option>\r\n");
	templateBuilder.Append("				  <option value=\"2\">查看次数</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"resultordertype\" value=\"1\" />\r\n");
	templateBuilder.Append("				升序\r\n");
	templateBuilder.Append("				<input name=\"resultordertype\" type=\"radio\" value=\"0\" checked />\r\n");
	templateBuilder.Append("				降序\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div id=\"albumoptions\">\r\n");
	templateBuilder.Append("		<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"searchtime\">时间</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"searchtime\" id=\"searchtime\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">全部时间</option>\r\n");
	templateBuilder.Append("				  <option value=\"-1\">1天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-2\">2天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-3\">3天前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-7\">1周前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-30\">1个月前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-90\">3个月前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-180\">半年前</option>\r\n");
	templateBuilder.Append("				  <option value=\"-365\">1年前</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("				  <input type=\"radio\" name=\"searchtimetype\" value=\"1\" />\r\n");
	templateBuilder.Append("				之前\r\n");
	templateBuilder.Append("				<input name=\"searchtimetype\" type=\"radio\" value=\"0\" checked />\r\n");
	templateBuilder.Append("				之后\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"resultorder\">结果排序</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<select name=\"resultorder\" id=\"resultorder\">\r\n");
	templateBuilder.Append("				  <option value=\"0\" selected=\"selected\">创建时间</option>\r\n");
	templateBuilder.Append("				</select>\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"resultordertype\" value=\"1\" />\r\n");
	templateBuilder.Append("				升序\r\n");
	templateBuilder.Append("				<input name=\"resultordertype\" type=\"radio\" value=\"0\" checked />\r\n");
	templateBuilder.Append("				降序\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<form id=\"postpm\" name=\"postpm\" method=\"post\" onsubmit=\"if(this.chkAuthor.checked)$('type').value='author';return true;\" action=\"\">\r\n");
	templateBuilder.Append("<DIV class=\"mainbox formbox\">\r\n");
	templateBuilder.Append("	<h1>搜索</h1>\r\n");
	templateBuilder.Append("	<TABLE cellSpacing=\"0\" cellPadding=\"0\" summary=\"搜索\">\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr id=\"divkeyword\">\r\n");
	templateBuilder.Append("			<th><label for=\"keyword\">关键词</label></th>\r\n");
	templateBuilder.Append("			<td><input name=\"keyword\" type=\"text\" id=\"keyword\" size=\"45\" />&nbsp;&nbsp;多个关键词间用英文空格分开</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"poster\">作者</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("			<input name=\"poster\" type=\"text\" id=\"poster\" size=\"45\" />\r\n");
	templateBuilder.Append("			<input type=\"checkbox\" value=\"1\" id=\"chkAuthor\" name=\"chkAuthor\" onclick=\"checkauthoroption(this);\" />搜索该作者所有帖子,相册和空间日志\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	<table cellSpacing=\"0\" cellPadding=\"0\" summary=\"搜索选项\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th id=\"divsearchoption\">搜索选项</th>\r\n");
	templateBuilder.Append("			<td>&nbsp;</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");
	templateBuilder.Append("		<tbody id=\"divsearchtype\">\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"poster\">搜索类型</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("			    <input type=\"hidden\" name=\"type\" value=\"post\" id=\"type\" />\r\n");
	templateBuilder.Append("				<input name=\"keywordtype\" type=\"radio\" value=\"0\" checked onclick=\"changeoption('post');\" />\r\n");
	templateBuilder.Append("				帖子标题搜索\r\n");

	if (usergroupinfo.Allowsearch==1)
	{

	templateBuilder.Append("					<input type=\"radio\" name=\"keywordtype\" value=\"1\" onclick=\"changeoption('post');\" />\r\n");
	templateBuilder.Append("				内容搜索\r\n");

	}	//end if

	templateBuilder.Append("				<input type=\"radio\" name=\"keywordtype\" value=\"2\" onclick=\"changeoption('spacepost');\" />\r\n");
	templateBuilder.Append("				日志标题搜索\r\n");
	templateBuilder.Append("				<input type=\"radio\" name=\"keywordtype\" value=\"3\" onclick=\"changeoption('album');\"/>\r\n");
	templateBuilder.Append("				相册标题搜索\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	<div id=\"options\" style=\"margin-top:-1px;\"></div>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\" src=\"javascript/template_search.js\"></" + "script>	\r\n");
	templateBuilder.Append("	<table cellSpacing=\"0\" cellPadding=\"0\" summary=\"搜索类型\" style=\"margin-top:-1px;\">\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>&nbsp;</th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				 <input name=\"submit\" type=\"submit\" id=\"submit\" value=\"执行搜索\" />\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</form>\r\n");
	templateBuilder.Append("</div>\r\n");

	}
	else
	{


	if (type=="album")
	{

	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) {\r\n");
	templateBuilder.Append("window.location='search.aspx?searchid=" + searchid.ToString() + "&page='+this.value;}\"  size=\"4\" maxlength=\"9\"/>页</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<span class=\"postbtn\">共搜索到" + topiccount.ToString() + "个符合条件的相册</span>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<DIV class=\"mainbox\">\r\n");
	templateBuilder.Append("	<h1>搜索结果</h1>\r\n");
	templateBuilder.Append("	<TABLE cellSpacing=\"0\" cellPadding=\"0\" summary=\"搜索结果\">\r\n");
	templateBuilder.Append("	<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("		<th>标题</th>\r\n");
	templateBuilder.Append("		<th>所属分类</th>\r\n");
	templateBuilder.Append("		<th>作者</th>\r\n");
	templateBuilder.Append("		<th>图片数</th>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</thead>\r\n");

	int album__loop__id=0;
	foreach(DataRow album in albumlist.Rows)
	{
		album__loop__id++;

	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>\r\n");
	templateBuilder.Append("				<a href=\"showalbum.aspx?albumid=" + album["albumid"].ToString().Trim() + "\" target=\"_blank\">" + album["title"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("			</th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<a href=\"showalbumlist.aspx?cate=" + album["albumcateid"].ToString().Trim() + "\" target=\"_parent\">" + album["categorytitle"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("					<p>\r\n");

	if (Utils.StrToInt(album["userid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("						游客\r\n");

	}
	else
	{

	templateBuilder.Append("						<a href=\"showalbumlist.aspx?uid=" + album["userid"].ToString().Trim() + "\" target=\"_parent\">" + album["username"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("					<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(album["createdatetime"].ToString().Trim()).ToString("yyyy.MM.dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("			<td>" + album["imgcount"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) {\r\n");
	templateBuilder.Append("window.location='search.aspx?searchid=" + searchid.ToString() + "&page='+this.value;}\"  size=\"4\" maxlength=\"9\"/>页</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<span class=\"postbtn\">共搜索到" + topiccount.ToString() + "个符合条件的相册</span>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	if (type=="spacepost")
	{

	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) {\r\n");
	templateBuilder.Append("window.location='search.aspx?searchid=" + searchid.ToString() + "&page='+this.value;}\"  size=\"4\" maxlength=\"9\"/>页</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<span class=\"postbtn\">共搜索到" + topiccount.ToString() + "篇符合条件的日志</span>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<DIV class=\"mainbox\">\r\n");
	templateBuilder.Append("	<h1>搜索结果</h1>\r\n");
	templateBuilder.Append("	<TABLE cellSpacing=\"0\" cellPadding=\"0\" summary=\"搜索结果\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("			<th>标题</th>\r\n");
	templateBuilder.Append("			<th>作者</th>\r\n");
	templateBuilder.Append("			<td class=\"nums\">回复</td>\r\n");
	templateBuilder.Append("			<td class=\"nums\">查看</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	int spacepost__loop__id=0;
	foreach(DataRow spacepost in spacepostlist.Rows)
	{
		spacepost__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>\r\n");
	templateBuilder.Append("					<a href=\"" + spaceurl.ToString() + "space/viewspacepost.aspx?postid=" + spacepost["postid"].ToString().Trim() + "\" target=\"_blank\">" + spacepost["title"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("				</th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<p>\r\n");

	if (Utils.StrToInt(spacepost["uid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("						游客\r\n");

	}
	else
	{

	templateBuilder.Append("						<a href=\"" + spaceurl.ToString() + "space/?uid=" + spacepost["uid"].ToString().Trim() + "\" target=\"_parent\">" + spacepost["author"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("					<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(spacepost["postdatetime"].ToString().Trim()).ToString("yyyy.MM.dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td>" + spacepost["commentcount"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td>" + spacepost["views"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("		  	</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) {\r\n");
	templateBuilder.Append("	window.location='search.aspx?searchid=" + searchid.ToString() + "&page='+this.value;}\"  size=\"4\" maxlength=\"9\"/>页</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<span class=\"postbtn\">共搜索到" + topiccount.ToString() + "篇符合条件的日志</span>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	if (type=="")
	{

	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) {\r\n");
	templateBuilder.Append("	window.location='search.aspx?searchid=" + searchid.ToString() + "&page='+this.value;}\"  size=\"4\" maxlength=\"9\"/>页</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<span class=\"postbtn\">共搜索到" + topiccount.ToString() + "个符合条件的帖子</span>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<DIV class=\"mainbox forumlist\">\r\n");
	templateBuilder.Append("	<h1>搜索结果</h1>\r\n");
	templateBuilder.Append("	<TABLE cellSpacing=\"0\" cellPadding=\"0\" summary=\"搜索结果\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>标题</th>\r\n");
	templateBuilder.Append("			<th>所在版块</th>\r\n");
	templateBuilder.Append("			<td>作者</td>\r\n");
	templateBuilder.Append("			<td class=\"nums\">回复</td>\r\n");
	templateBuilder.Append("			<td class=\"nums\">查看</td>\r\n");
	templateBuilder.Append("			<td>最后发表</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	int topic__loop__id=0;
	foreach(DataRow topic in topiclist.Rows)
	{
		topic__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<td>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic["tid"].ToString().Trim(),0);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + topic["title"].ToString().Trim() + "</a></td>\r\n");
	templateBuilder.Append("				<td>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(topic["fid"].ToString().Trim(),0);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_parent\">" + topic["forumname"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<h4>\r\n");

	if (Utils.StrToInt(topic["posterid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("						游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(topic["posterid"].ToString().Trim());
	
	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_parent\">" + topic["poster"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("</h4>\r\n");
	templateBuilder.Append("					<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(topic["postdatetime"].ToString().Trim()).ToString("yyyy.MM.dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\">" + topic["replies"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\">" + topic["views"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("						<em><a href=\"showtopic.aspx?topicid=" + topic["tid"].ToString().Trim() + "&page=end\" target=\"_blank\">\r\n");
	templateBuilder.Append(Convert.ToDateTime(topic["lastpost"].ToString().Trim()).ToString("yyyy.MM.dd HH:mm"));
	templateBuilder.Append("</a></em>\r\n");
	templateBuilder.Append("						<cite>\r\n");

	if (Utils.StrToInt(topic["posterid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("							游客\r\n");

	}
	else
	{

	templateBuilder.Append("							<a href=\"{UserInfoAspxRewrite(topic[lastposterid])}\" target=\"_blank\">" + topic["lastposter"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("						</cite>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("	<div class=\"pages\">\r\n");
	templateBuilder.Append("		<em>" + pageid.ToString() + "/" + pagecount.ToString() + "页</em>" + pagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		<kbd>跳转<input name=\"gopage\" type=\"text\" id=\"gopage\" onKeyDown=\"if(event.keyCode==13) {\r\n");
	templateBuilder.Append("	window.location='search.aspx?searchid=" + searchid.ToString() + "&page='+this.value;}\"  size=\"4\" maxlength=\"9\"/>页</kbd>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<span class=\"postbtn\">共搜索到" + topiccount.ToString() + "个符合条件的帖子</span>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	if (type=="author")
	{

	templateBuilder.Append("<div class=\"searchtab\">\r\n");
	templateBuilder.Append("		<a id=\"result1\" class=\"currenttab\" onmouseover=\"javascript:doClick_result(this)\" href=\"###\">帖子搜索结果</a>\r\n");
	templateBuilder.Append("		<a id=\"result2\" class=\"\" onmouseover=\"javascript:doClick_result(this)\" href=\"###\">日志搜索结果</a>\r\n");
	templateBuilder.Append("		<a id=\"result3\" class=\"\" onmouseover=\"javascript:doClick_result(this)\" href=\"###\">相册搜索结果</a>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div id=\"resultid1\" style=\"display:block;\">\r\n");
	templateBuilder.Append("	<DIV class=\"mainbox forumlist\">\r\n");
	templateBuilder.Append("	<h1>帖子搜索结果(共" + topiccount.ToString() + "个)</h1>\r\n");
	templateBuilder.Append("	<TABLE cellSpacing=\"0\" cellPadding=\"0\" summary=\"帖子搜索结果\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>标题</th>\r\n");
	templateBuilder.Append("			<th>所在版块</th>\r\n");
	templateBuilder.Append("			<th>作者</th>\r\n");
	templateBuilder.Append("			<td class=\"nums\">回复</td>\r\n");
	templateBuilder.Append("			<td class=\"nums\">查看</td>\r\n");
	templateBuilder.Append("			<td>最后发表</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	int topic__loop__id=0;
	foreach(DataRow topic in topiclist.Rows)
	{
		topic__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(topic["tid"].ToString().Trim(),0);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + topic["title"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("				</th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(topic["fid"].ToString().Trim(),0);
	
	templateBuilder.Append("					<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_parent\">" + topic["forumname"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<p>\r\n");

	if (Utils.StrToInt(topic["posterid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("						游客\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(topic["posterid"].ToString().Trim());
	
	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_parent\">" + topic["poster"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("					<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(topic["postdatetime"].ToString().Trim()).ToString("yyyy.MM.dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\">" + topic["replies"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td class=\"nums\">" + topic["views"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("						<em><a href=\"showtopic.aspx?topicid=" + topic["tid"].ToString().Trim() + "&page=end\" target=\"_blank\">\r\n");
	templateBuilder.Append(Convert.ToDateTime(topic["lastpost"].ToString().Trim()).ToString("yyyy.MM.dd HH:mm"));
	templateBuilder.Append("</a></em>\r\n");
	templateBuilder.Append("						<cite>\r\n");

	if (Utils.StrToInt(topic["posterid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("							游客\r\n");

	}
	else
	{

	templateBuilder.Append("							<a href=\"{UserInfoAspxRewrite(topic[lastposterid])}\" target=\"_blank\">" + topic["lastposter"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("						</cite>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("		<div class=\"pages\">\r\n");
	templateBuilder.Append("			<em>" + topicpageid.ToString() + "/" + topicpagecount.ToString() + "页</em>" + topicpagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div id=\"resultid2\" style=\"display:none;\">\r\n");
	templateBuilder.Append("	<DIV class=\"mainbox\">\r\n");
	templateBuilder.Append("	<h1>日志搜索结果(共" + blogcount.ToString() + "篇)</h1>\r\n");
	templateBuilder.Append("	<TABLE cellSpacing=\"0\" cellPadding=\"0\" summary=\"日志搜索结果\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>标题</th>\r\n");
	templateBuilder.Append("			<th>作者</th>\r\n");
	templateBuilder.Append("			<th>回复/查看</th>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	int spacepost__loop__id=0;
	foreach(DataRow spacepost in spacepostlist.Rows)
	{
		spacepost__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>\r\n");
	templateBuilder.Append("					<a href=\"" + spaceurl.ToString() + "space/viewspacepost.aspx?postid=" + spacepost["postid"].ToString().Trim() + "\" target=\"_blank\">" + spacepost["title"].ToString().Trim() + "</a></th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<p>\r\n");

	if (Utils.StrToInt(spacepost["uid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("						游客\r\n");

	}
	else
	{

	templateBuilder.Append("						<a href=\"" + spaceurl.ToString() + "space/?uid=" + spacepost["uid"].ToString().Trim() + "\" target=\"_parent\">" + spacepost["author"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("					<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(spacepost["postdatetime"].ToString().Trim()).ToString("yyyy.MM.dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td><em>" + spacepost["commentcount"].ToString().Trim() + "</em> / " + spacepost["views"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("		<div class=\"pages\">\r\n");
	templateBuilder.Append("			<em>" + blogpageid.ToString() + "/" + blogpagecount.ToString() + "页</em>" + blogpagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div id=\"resultid3\" style=\"display:none;\">\r\n");
	templateBuilder.Append("	<DIV class=\"mainbox\">\r\n");
	templateBuilder.Append("	<h1>相册搜索结果(共" + albumcount.ToString() + "个)</h1>\r\n");
	templateBuilder.Append("	<TABLE cellSpacing=\"0\" cellPadding=\"0\" summary=\"相册搜索结果\">\r\n");
	templateBuilder.Append("		<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th>标题</th>\r\n");
	templateBuilder.Append("			<th>所属分类</th>\r\n");
	templateBuilder.Append("			<th>作者</th>\r\n");
	templateBuilder.Append("			<th>图片数</th>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</thead>\r\n");

	int album__loop__id=0;
	foreach(DataRow album in albumlist.Rows)
	{
		album__loop__id++;

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("				<th>\r\n");
	templateBuilder.Append("					<a href=\"showalbum.aspx?albumid=" + album["albumid"].ToString().Trim() + "\" target=\"_blank\">" + album["title"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("				</th>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<a href=\"showalbumlist.aspx?cate=" + album["albumcateid"].ToString().Trim() + "\" target=\"_parent\">" + album["categorytitle"].ToString().Trim() + "&nbsp;</a>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td>\r\n");
	templateBuilder.Append("					<p>\r\n");

	if (Utils.StrToInt(album["userid"].ToString().Trim(), 0)==-1)
	{

	templateBuilder.Append("						游客\r\n");

	}
	else
	{

	templateBuilder.Append("						<a href=\"showalbumlist.aspx?uid=" + album["userid"].ToString().Trim() + "\" target=\"_parent\">" + album["username"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("					<em>\r\n");
	templateBuilder.Append(Convert.ToDateTime(album["createdatetime"].ToString().Trim()).ToString("yyyy.MM.dd HH:mm"));
	templateBuilder.Append("</em>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("				<td>" + album["imgcount"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("			</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end loop

	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"pages_btns\">\r\n");
	templateBuilder.Append("		<div class=\"pages\">\r\n");
	templateBuilder.Append("			<em>" + albumpageid.ToString() + "/" + albumpagecount.ToString() + "页</em>" + albumpagenumbers.ToString() + "\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("switch (getQueryString('show'))\r\n");
	templateBuilder.Append("{	\r\n");
	templateBuilder.Append("	case 'album':\r\n");
	templateBuilder.Append("		doClick_result($('result3'));\r\n");
	templateBuilder.Append("		break;\r\n");
	templateBuilder.Append("	case 'blog':\r\n");
	templateBuilder.Append("		doClick_result($('result2'));\r\n");
	templateBuilder.Append("		break;\r\n");
	templateBuilder.Append("	case 'topic':\r\n");
	templateBuilder.Append("	default:\r\n");
	templateBuilder.Append("		doClick_result($('result1'));\r\n");
	templateBuilder.Append("		break;\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("function doClick_result(o){\r\n");
	templateBuilder.Append("	o.className=\"currenttab\";\r\n");
	templateBuilder.Append("	var j;\r\n");
	templateBuilder.Append("	var id;\r\n");
	templateBuilder.Append("	var e;\r\n");
	templateBuilder.Append("	for(var i=1;i<=3;i++){\r\n");
	templateBuilder.Append("		id =\"result\"+i;\r\n");
	templateBuilder.Append("		j = document.getElementById(id);\r\n");
	templateBuilder.Append("		e = document.getElementById(\"resultid\"+i);\r\n");
	templateBuilder.Append("		if(id != o.id){\r\n");
	templateBuilder.Append("			j.className=\"\";\r\n");
	templateBuilder.Append("			e.style.display = \"none\";\r\n");
	templateBuilder.Append("		}else{\r\n");
	templateBuilder.Append("			e.style.display = \"block\";\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("</" + "script>\r\n");

	}	//end if

	templateBuilder.Append("</div>\r\n");

	}	//end if


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
