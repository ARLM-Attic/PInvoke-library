<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.postreply" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:04:16.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:04:16. 
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


	templateBuilder.Append("<script language=\"javascript\" type=\"text/javascript\" src=\"javascript/template_calendar.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("var postminchars = parseInt(" + config.Minpostsize.ToString().Trim() + ");\r\n");
	templateBuilder.Append("var postmaxchars = parseInt(" + config.Maxpostsize.ToString().Trim() + ");\r\n");
	templateBuilder.Append("var disablepostctrl = parseInt(" + disablepost.ToString() + ");\r\n");
	templateBuilder.Append("var forumpath = \"" + forumpath.ToString() + "\";\r\n");
	templateBuilder.Append("</" + "script>\r\n");
	templateBuilder.Append("	<div id=\"nav\">\r\n");
	templateBuilder.Append("		<a href=\"" + config.Forumurl.ToString().Trim() + "\" class=\"home\">" + config.Forumtitle.ToString().Trim() + "</a> &raquo; " + forumnav.ToString() + "  &raquo; \r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(topicid,0);
	
	templateBuilder.Append("		<a href=\"" + aspxrewriteurl.ToString() + "\">" + topictitle.ToString() + "</a>  &raquo; 回复主题\r\n");
	templateBuilder.Append("	</div>\r\n");

	if (page_err==0)
	{


	if (ispost)
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

	templateBuilder.Append("	<div class=\"mainbox viewthread\" id=\"previewtable\" style=\"display: none\">\r\n");
	templateBuilder.Append("		<h1>预览帖子</h1>\r\n");
	templateBuilder.Append("		<table summary=\"预览帖子\" cellspacing=\"0\" cellpadding=\"0\">\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("				<td class=\"postauthor\">" + username.ToString() + "</td>\r\n");
	templateBuilder.Append("				<td class=\"postcontent\">\r\n");
	templateBuilder.Append("			<span class=\"fontfamily\">" + nowdatetime.ToString() + "</span>\r\n");
	templateBuilder.Append("			<span id=\"previewmessage\"></span>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</table> \r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<form method=\"post\" name=\"postform\" id=\"postform\" action=\"\" enctype=\"multipart/form-data\" onsubmit=\"return validate(this);\">\r\n");
	templateBuilder.Append("	<div class=\"mainbox formbox\">\r\n");
	templateBuilder.Append("		<h1>回复主题</h1>\r\n");
	templateBuilder.Append("		<table summary=\"post\" cellspacing=\"0\" cellpadding=\"0\" id=\"newpost\">\r\n");

	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("var tempaccounts = false;\r\n");
	templateBuilder.Append("function showusername()\r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("    $(\"usernamelayer\").innerHTML = \"<input name='tempusername' type='text' id='tempusername' size='20' class='colorblue' onfocus=\\\"this.className='colorfocus';\\\" onblur=\\\"this.className='colorblue';\\\" value='" + username.ToString() + "' onkeyup=\\\"$('passwordlayer').style.display='';\\\">&nbsp;[<a href='javascript:;' onclick='resetusername()'>恢复</a>]\";\r\n");
	templateBuilder.Append("    tempaccounts = true;\r\n");
	templateBuilder.Append("    var i = 1;\r\n");
	templateBuilder.Append("    while(true)\r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        var obj = $(\"albums\" + i);\r\n");
	templateBuilder.Append("        if(obj == null) break;\r\n");
	templateBuilder.Append("        obj.options[0].selected = true;\r\n");
	templateBuilder.Append("        obj.disabled = true\r\n");
	templateBuilder.Append("        i++;\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("function resetusername()\r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("    $('passwordlayer').style.display='none';\r\n");
	templateBuilder.Append("    $(\"usernamelayer\").innerHTML = \"" + username.ToString() + "&nbsp;[<a href='javascript:;' onclick='showusername()'>切换临时帐号</a>]\";\r\n");
	templateBuilder.Append("    tempaccounts = false;\r\n");
	templateBuilder.Append("    var i = 1;\r\n");
	templateBuilder.Append("    while(true)\r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        var obj = $(\"albums\" + i);\r\n");
	templateBuilder.Append("        if(obj == null) break;\r\n");
	templateBuilder.Append("        obj.disabled = false\r\n");
	templateBuilder.Append("        i++;\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("</" + "script>\r\n");
	templateBuilder.Append("<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th>用户名</th>\r\n");
	templateBuilder.Append("		<td><h5 id=\"usernamelayer\">\r\n");

	if (userid>0)
	{

	templateBuilder.Append("		" + username.ToString() + "&nbsp;[<a href='javascript:;' onclick='showusername()'>切换临时帐号</a>]\r\n");

	}
	else
	{

	templateBuilder.Append("			匿名 [<a href=\"login.aspx\">登录</a>] [<a href=\"register.aspx\">注册</a>]\r\n");

	}	//end if

	templateBuilder.Append("</h5>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("</tbody>\r\n");
	templateBuilder.Append("<tbody id=\"passwordlayer\" style=\"display:none\">\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"temppassword\">密码</label></th>\r\n");
	templateBuilder.Append("		<td>\r\n");
	templateBuilder.Append("			<input name=\"temppassword\" type=\"password\" id=\"temppassword\" size=\"20\" />\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");

	if (isseccode)
	{

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

	}	//end if


	if (config.Secques==1)
	{

	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"question\">安全问题</label></th>\r\n");
	templateBuilder.Append("		<td>\r\n");
	templateBuilder.Append("			<select name=\"question\" id=\"question\">\r\n");
	templateBuilder.Append("			<option value=\"0\" selected=\"selected\">无</option>\r\n");
	templateBuilder.Append("			<option value=\"1\">母亲的名字</option>\r\n");
	templateBuilder.Append("			<option value=\"2\">爷爷的名字</option>\r\n");
	templateBuilder.Append("			<option value=\"3\">父亲出生的城市</option>\r\n");
	templateBuilder.Append("			<option value=\"4\">您其中一位老师的名字</option>\r\n");
	templateBuilder.Append("			<option value=\"5\">您个人计算机的型号</option>\r\n");
	templateBuilder.Append("			<option value=\"6\">您最喜欢的餐馆名称</option>\r\n");
	templateBuilder.Append("			<option value=\"7\">驾驶执照的最后四位数字</option>\r\n");
	templateBuilder.Append("			</select>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<th><label for=\"answer\">答案</label></th>\r\n");
	templateBuilder.Append("		<td><input name=\"answer\" type=\"text\" id=\"answer\" size=\"50\" /><br/>如果您设置了安全提问，请在此输入正确的问题和回答</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");

	}	//end if

	templateBuilder.Append("</tbody>\r\n");


	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"title\">标题</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");

	if (topic.Special==4)
	{

	templateBuilder.Append("			<select id=\"debateopinion\" name=\"debateopinion\">\r\n");
	templateBuilder.Append("			<option selected=\"\" value=\"0\"/>\r\n");
	templateBuilder.Append("			<option value=\"1\">正方</option>\r\n");
	templateBuilder.Append("			<option value=\"2\">反方</option>\r\n");
	templateBuilder.Append("			</select>\r\n");

	}	//end if

	templateBuilder.Append("			<input name=\"title\" id=\"title\" type=\"text\" value=\"\" size=\"60\" title=\"标题最多为60个字符\" /><em class=\"tips\">(可选)</em>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");

	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/common.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/menu.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/bbcode.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/ajax.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("var lang						= new Array();\r\n");
	templateBuilder.Append("lang['post_discuzcode_code'] = '请输入要插入的代码';\r\n");
	templateBuilder.Append("lang['post_discuzcode_quote'] = '请输入要插入的引用';\r\n");
	templateBuilder.Append("lang['post_discuzcode_free'] = '请输入要插入的免费信息';\r\n");
	templateBuilder.Append("lang['post_discuzcode_hide'] = '请输入要插入的隐藏内容';\r\n");
	templateBuilder.Append("var editorcss = 'templates/" + templatepath.ToString() + "/editor.css';\r\n");
	templateBuilder.Append("</" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	var typerequired = parseInt('0');\r\n");
	templateBuilder.Append("//		var bbinsert = parseInt('1');\r\n");
	templateBuilder.Append("	var seccodecheck = parseInt('0');\r\n");
	templateBuilder.Append("	var secqaacheck = parseInt('0');\r\n");
	templateBuilder.Append("	var special = parseInt('0');\r\n");
	templateBuilder.Append("	var isfirstpost = 1;\r\n");
	templateBuilder.Append("	var allowposttrade = parseInt('1');\r\n");
	templateBuilder.Append("	var allowpostreward = parseInt('1');\r\n");
	templateBuilder.Append("	var allowpostactivity = parseInt('1');\r\n");
	templateBuilder.Append("	lang['board_allowed'] = '系统限制';\r\n");
	templateBuilder.Append("	lang['lento'] = '到';\r\n");
	templateBuilder.Append("	lang['bytes'] = '字节';\r\n");
	templateBuilder.Append("	lang['post_curlength'] = '当前长度';\r\n");
	templateBuilder.Append("	lang['post_title_and_message_isnull'] = '请完成标题或内容栏。';\r\n");
	templateBuilder.Append("	lang['post_title_toolong'] = '您的标题超过 60 个字符的限制。';\r\n");
	templateBuilder.Append("	lang['post_message_length_invalid'] = '您的帖子长度不符合要求。';\r\n");
	templateBuilder.Append("	lang['post_type_isnull'] = '请选择主题对应的分类。';\r\n");
	templateBuilder.Append("	lang['post_reward_credits_null'] = '对不起，您输入悬赏积分。';\r\n");
	templateBuilder.Append("	var bbinsert = parseInt('1');\r\n");
	templateBuilder.Append("	var editorid = 'posteditor';\r\n");
	templateBuilder.Append("	var allowhtml = parseInt('0');\r\n");
	templateBuilder.Append("	var forumallowhtml = parseInt('0');\r\n");
	templateBuilder.Append("	var allowsmilies = 1 - parseInt('" + smileyoff.ToString() + "');\r\n");
	templateBuilder.Append("	var allowbbcode = 1 - parseInt('" + bbcodeoff.ToString() + "');\r\n");
	templateBuilder.Append("	var allowimgcode = parseInt('" + allowimg.ToString() + "');\r\n");
	templateBuilder.Append("	var wysiwyg = (is_ie || is_moz || (is_opera && opera.version() >= 9)) && parseInt('" + config.Defaulteditormode.ToString().Trim() + "') && allowbbcode == 1 ? 1 : 0;//bbinsert == 1 ? 1 : 0;\r\n");
	templateBuilder.Append("	var allowswitcheditor = parseInt('" + config.Allowswitcheditor.ToString().Trim() + "');\r\n");
	templateBuilder.Append("	//var Editor				= new Array();\r\n");
	templateBuilder.Append("	lang['enter_tag_option']		= \"请输入 %1 标签的选项:\";\r\n");
	templateBuilder.Append("	//lang['enter_list_item']			= \"输入一个列表项目.\\r\\n留空或者点击'取消'完成此列表.\";\r\n");
	templateBuilder.Append("	//lang['enter_link_url']			= \"请输入链接的地址:\";\r\n");
	templateBuilder.Append("	//lang['enter_image_url']			= \"请输入图片链接地址:\";\r\n");
	templateBuilder.Append("	//lang['enter_email_link']		= \"请输入此链接的邮箱地址:\";\r\n");
	templateBuilder.Append("	lang['enter_table_rows']		= \"请输入行数，最多 30 行:\";\r\n");
	templateBuilder.Append("	lang['enter_table_columns']		= \"请输入列数，最多 30 列:\";\r\n");
	templateBuilder.Append("	//lang['fontname']			= \"字体\";\r\n");
	templateBuilder.Append("	//lang['fontsize']			= \"大小\";\r\n");
	templateBuilder.Append("	var custombbcodes = { " + customeditbuttons.ToString() + " };\r\n");
	templateBuilder.Append("	var smileyinsert = parseInt('1');\r\n");
	templateBuilder.Append("	//var editor_id = 'posteditor';　//编辑器ID\r\n");
	templateBuilder.Append("	var smiliesCount = 12;//显示表情总数\r\n");
	templateBuilder.Append("	var colCount = 4; //每行显示表情个数\r\n");
	templateBuilder.Append("	var title = \"\";				   //标题\r\n");
	templateBuilder.Append("	var showsmiliestitle = 1;        //是否显示标题（0不显示 1显示）\r\n");
	templateBuilder.Append("	var smiliesIsCreate = 0;		   //编辑器是否已被创建(0否，1是）\r\n");
	templateBuilder.Append("	var smilies_HASH = {};//得到表情符号信息\r\n");
	templateBuilder.Append("	var smiliePageSize = 16; //表情每页显示数量 (共4列)\r\n");
	templateBuilder.Append("	//table变量\r\n");
	templateBuilder.Append("	var msgheader = \"margin:0 2em; font: 11px Arial, Tahoma; font-weight: bold; background: #F3F8D7; padding: 5px;\";\r\n");
	templateBuilder.Append("	var msgborder = \"margin: 0 2em; padding: 10px; border: 1px solid #dbddd3; word-break: break-all; background-color: #fdfff2;\";\r\n");
	templateBuilder.Append("	var INNERBORDERCOLOR = \"#D6E0EF\";\r\n");
	templateBuilder.Append("	var BORDERWIDTH = \"1\";\r\n");
	templateBuilder.Append("	var BORDERCOLOR = \"#7ac4ea\";\r\n");
	templateBuilder.Append("	var ALTBG2 = \"#ffffff\";\r\n");
	templateBuilder.Append("	var FONTSIZE = \"12px\";\r\n");
	templateBuilder.Append("	var FONT = \"Tahoma, Verdana\";\r\n");
	templateBuilder.Append("	//var fontoptions = new Array(\"仿宋_GB2312\", \"黑体\", \"楷体_GB2312\", \"宋体\", \"新宋体\", \"Tahoma\", \"Arial\", \"Impact\", \"Verdana\", \"Times New Roman\");\r\n");
	templateBuilder.Append("	var altbg1 = '#f5fbff';\r\n");
	templateBuilder.Append("	var altbg2 = 'background: #ffffff;font: 12px Tahoma, Verdana;';\r\n");
	templateBuilder.Append("	var tableborder = 'background: #D6E0EF;border: 1px solid #7ac4ea;';\r\n");
	templateBuilder.Append("	//var lang = new Array();\r\n");
	templateBuilder.Append("	if(is_ie >= 5 || is_moz >= 2) {\r\n");
	templateBuilder.Append("		window.onbeforeunload = function () {saveData(wysiwyg && bbinsert ? editdoc.body.innerHTML : textobj.value)};\r\n");
	templateBuilder.Append("		lang['post_autosave_none'] = \"没有可以恢复的数据\";\r\n");
	templateBuilder.Append("		lang['post_autosave_confirm'] = \"本操作将覆盖当前帖子内容，确定要恢复数据吗？\";\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("	var maxpolloptions = parseInt('" + config.Maxpolloptions.ToString().Trim() + "');\r\n");
	templateBuilder.Append("</" + "script>\r\n");
	templateBuilder.Append("<th valign=\"top\">\r\n");
	templateBuilder.Append("	<label for=\"posteditor_textarea\">\r\n");
	templateBuilder.Append("		内容\r\n");
	templateBuilder.Append("	</label>\r\n");
	templateBuilder.Append("	<div id=\"posteditor_left\" >\r\n");
	templateBuilder.Append("		<input type=\"checkbox\" value=\"1\" name=\"parseurloff\" ID=\"parseurloff\" \r\n");

	if (parseurloff==1)
	{

	templateBuilder.Append("checked\r\n");

	}	//end if

	templateBuilder.Append("> 禁用 URL 识别<br />\r\n");
	templateBuilder.Append("		<input type=\"checkbox\" value=\"1\" name=\"smileyoff\" ID=\"smileyoff\" \r\n");

	if (smileyoff==1)
	{

	templateBuilder.Append("checked\r\n");

	}	//end if


	if (forum.Allowsmilies!=1)
	{

	templateBuilder.Append("disabled\r\n");

	}	//end if

	templateBuilder.Append("> 禁用表情<br />\r\n");
	templateBuilder.Append("		<input type=\"checkbox\" value=\"1\" name=\"bbcodeoff\" ID=\"bbcodeoff\"\r\n");

	if (bbcodeoff==1)
	{

	templateBuilder.Append("				checked\r\n");

	}	//end if


	if (usergroupinfo.Allowcusbbcode!=1)
	{

	templateBuilder.Append("				disabled\r\n");

	}
	else
	{


	if (forum.Allowbbcode!=1)
	{

	templateBuilder.Append("					disabled\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("		> 禁用 Discuz!NT 代码<br />\r\n");
	templateBuilder.Append("		<input type=\"checkbox\" value=\"1\" name=\"usesig\" ID=\"usesig\" \r\n");

	if (usesig==1)
	{

	templateBuilder.Append("checked\r\n");

	}	//end if

	templateBuilder.Append("> 使用个人签名\r\n");

	if (pagename=="postreply.aspx")
	{

	templateBuilder.Append("<br />\r\n");
	templateBuilder.Append("			<input type=\"checkbox\" name=\"emailnotify\" id=\"emailnotify\" /> 发送邮件通知楼主\r\n");

	}	//end if

	templateBuilder.Append("<!--表情符号列表-->\r\n");

	if (config.Smileyinsert==1)
	{

	string defaulttypname = string.Empty;
	
	templateBuilder.Append("		<div class=\"editorsmiles\">\r\n");
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
	templateBuilder.Append("					<img src=\"editor/images/smilie_prev_default.gif\" alt=\"向前\" onmouseover=\"if($('scrollbar').scrollLeft>0)this.src=this.src.replace(/_default|_selected/, '_hover');\" onmouseout=\"this.src=this.src.replace(/_hover|_selected/, '_default');\" onmousedown=\"if($('scrollbar').scrollLeft>0){this.src=this.src.replace(/_hover|_default/, '_selected');this.boder=1;}\" onmouseup=\"if($('scrollbar').scrollLeft>0)this.src=this.src.replace(/_selected/, '_hover');else{this.src=this.src.replace(/_selected|_hover/, '_default');}this.border=0;\" onclick=\"scrollSmilieTypeBar($('scrollbar'), 1-$('t_s_1').clientWidth);\"/>\r\n");
	templateBuilder.Append("					<img src=\"editor/images/smilie_next_default.gif\" alt=\"向后\" onmouseover=\"if($('scrollbar').scrollLeft<scrMaxLeft)this.src=this.src.replace(/_default|_selected/, '_hover');\" onmouseout=\"this.src=this.src.replace(/_hover|_selected/, '_default');\" onmousedown=\"if($('scrollbar').scrollLeft<scrMaxLeft){this.src=this.src.replace(/_hover|_default/, '_selected');this.boder=1;}\" onmouseup=\"if($('scrollbar').scrollLeft<scrMaxLeft)this.src=this.src.replace(/_selected/, '_hover');else{this.src=this.src.replace(/_selected|_hover/, '_default');}this.border=0;\" onclick=\"scrollSmilieTypeBar($('scrollbar'), $('t_s_1').clientWidth);\" />\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("	 </div>\r\n");
	templateBuilder.Append("	 <div id=\"showsmilie\"><img src=\"images/loading_wide.gif\" width=\"90%\" style=\" margin-top:20px;\" alt=\"加载表情\"/><p>正在加载表情...</p></div>\r\n");
	templateBuilder.Append("	 <div id=\"showsmilie_pagenum\">&nbsp;</div>\r\n");
	templateBuilder.Append("    </div>\r\n");
	templateBuilder.Append("		<script src=\"javascript/post.js\" type=\"text/javascript\"></" + "script>\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			var scrMaxLeft; //表情滚动条宽度\r\n");
	templateBuilder.Append("			var firstpagesmilies_json ={ " + firstpagesmilies.ToString() + " };\r\n");
	templateBuilder.Append("			showFirstPageSmilies(firstpagesmilies_json, '" + defaulttypname.ToString() + "',  16);\r\n");
	templateBuilder.Append("			function getSmilies(func){\r\n");
	templateBuilder.Append("				var c=\"tools/ajax.aspx?t=smilies\";\r\n");
	templateBuilder.Append("				_sendRequest(c,function(d){var e={};try{e=eval(\"(\"+d+\")\")}catch(f){e={}}var h=e?e:null;func(h);e=null;func=null},false,true)\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("			getSmilies(function(obj){ \r\n");
	templateBuilder.Append("				smilies_HASH = obj; \r\n");
	templateBuilder.Append("				showsmiles(1, '" + defaulttypname.ToString() + "');\r\n");
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

	templateBuilder.Append("<!-- / 表情符号列表-->\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</td>\r\n");
	templateBuilder.Append("<td valign=\"top\">\r\n");
	templateBuilder.Append("<div id=\"posteditor\" class=\"editor\">\r\n");
	templateBuilder.Append("	<div id=\"posteditor_controls\">\r\n");
	templateBuilder.Append("		<div class=\"editorrow\">\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_bold\" title=\"粗体\">B</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_italic\" title=\"斜体\">I</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_underline\" title=\"下划线\">U</a>\r\n");
	templateBuilder.Append("			<em></em>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_popup_fontname\" title=\"字体\"><span style=\"width: 110px; display: block; white-space: nowrap;\" id=\"posteditor_font_out\">字体</span></a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_popup_fontsize\" title=\"大小\"><span style=\"width: 30px; display: block;\" id=\"posteditor_size_out\">大小</span></a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_popup_forecolor\" title=\"颜色\"><span style=\"width: 30px; display: block;\"><img alt=\"color\" src=\"../../editor/images/bb_color.gif\"/><br/><img width=\"21\" height=\"4\" style=\"background-color: Black;\" alt=\"\" id=\"posteditor_color_bar\" src=\"../../editor/images/bb_clear.gif\"/></span></a>\r\n");
	templateBuilder.Append("			<em></em>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_justifyleft\" title=\"居左\">Align Left</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_justifycenter\" title=\"居中\">Align Center</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_justifyright\" title=\"居右\">Align Right</a>\r\n");
	templateBuilder.Append("			<em></em>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_createlink\" title=\"插入链接\">Url</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_email\" title=\"插入邮箱链接\">Email</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_insertimage\" title=\"插入图片\">Image</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_popup_media\" title=\"插入在线视频\">Media</a>\r\n");
	templateBuilder.Append("			<em></em>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_quote\" title=\"插入引用\">Quote</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_code\" title=\"插入代码\">Code</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_buttonctrl\" class=\"plugeditor editormode\">简单功能</a>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<div class=\"editorrow\" id=\"posteditor_morebuttons\" >\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_removeformat\" title=\"清除文本格式\">Remove Format</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_unlink\" title=\"移除链接\">Unlink</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_undo\" title=\"撤销\">Undo</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_redo\" title=\"重做\">Redo</a>\r\n");
	templateBuilder.Append("			<em></em>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_insertorderedlist\" title=\"排序的列表\">Ordered List</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_insertunorderedlist\" title=\"未排序列表\">Unordered List</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_outdent\" title=\"减少缩进\">Outdent</a>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_cmd_indent\" title=\"增加缩进\">Indent</a>\r\n");
	templateBuilder.Append("			<em></em>\r\n");
	templateBuilder.Append("			<a id=\"posteditor_popup_table\" title=\"插入表格\">Table</a>\r\n");

	if (usergroupinfo.Allowhidecode==1)
	{

	templateBuilder.Append("			<a id=\"posteditor_cmd_hide\" title=\"插入隐藏内容\">Hide</a>\r\n");

	}	//end if

	templateBuilder.Append("<em></em>\r\n");
	templateBuilder.Append("			<!--<a class=\"plugeditor\" id=\"posteditor_cmd_custom1_qq\"><img src=\"../../editor/images/bb_qq.gif\" title=\"显示 QQ 在线状态，点这个图标可以和他（她）聊天\" alt=\"qq\" /></a>-->		\r\n");
	templateBuilder.Append("			<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("				//自定义按扭显示\r\n");
	templateBuilder.Append("				if(typeof(custombbcodes) != 'undefined') {\r\n");
	templateBuilder.Append("					//document.writeln('<td><img src=\"editor/images/separator.gif\" width=\"6\" height=\"23\"></td>');\r\n");
	templateBuilder.Append("					for (var id in custombbcodes){\r\n");
	templateBuilder.Append("						if (custombbcodes[id][1] == '')\r\n");
	templateBuilder.Append("						{\r\n");
	templateBuilder.Append("							continue;\r\n");
	templateBuilder.Append("						}\r\n");
	templateBuilder.Append("						document.writeln('<a id=\"posteditor_cmd_custom' + custombbcodes[id][5] + '_' + custombbcodes[id][0] + '\"><img title=\"' + custombbcodes[id][2] + '\" alt=\"' + custombbcodes[id][2] + '\" src = \"../../editor/images/' + custombbcodes[id][1] + '\" width=\"21\" height=\"20\" /></a>');\r\n");
	templateBuilder.Append("					}\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("			</" + "script>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<div id=\"posteditor_switcher\" class=\"editor_switcher_bar\">\r\n");
	templateBuilder.Append("				<button type=\"button\" id=\"bbcodemode\">Discuz! 代码模式</button>\r\n");
	templateBuilder.Append("				<button type=\"button\" id=\"wysiwygmode\">所见即所得模式</button>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div class=\"editortoolbar\">\r\n");
	templateBuilder.Append("	<div class=\"popupmenu_popup fontname_menu\" id=\"posteditor_popup_fontname_menu\" style=\"display: none\">\r\n");
	templateBuilder.Append("		<ul unselectable=\"on\">\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', '仿宋_GB2312');hideMenu()\" style=\"font-family: 仿宋_GB2312\" unselectable=\"on\">仿宋_GB2312</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', '黑体');hideMenu()\" style=\"font-family: 黑体\" unselectable=\"on\">黑体</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', '楷体_GB2312');hideMenu()\" style=\"font-family: 楷体_GB2312\" unselectable=\"on\">楷体_GB2312</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', '宋体');hideMenu()\" style=\"font-family: 宋体\" unselectable=\"on\">宋体</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', '新宋体');hideMenu()\" style=\"font-family: 新宋体\" unselectable=\"on\">新宋体</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', '微软雅黑');hideMenu()\" style=\"font-family: 微软雅黑\" unselectable=\"on\">微软雅黑</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', 'Trebuchet MS');hideMenu()\" style=\"font-family: Trebuchet MS\" unselectable=\"on\">Trebuchet MS</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', 'Tahoma');hideMenu()\" style=\"font-family: Tahoma\" unselectable=\"on\">Tahoma</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', 'Arial');hideMenu()\" style=\"font-family: Arial\" unselectable=\"on\">Arial</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', 'Impact');hideMenu()\" style=\"font-family: Impact\" unselectable=\"on\">Impact</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', 'Verdana');hideMenu()\" style=\"font-family: Verdana\" unselectable=\"on\">Verdana</li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontname', 'Times New Roman');hideMenu()\" style=\"font-family: Times New Roman\" unselectable=\"on\">Times New Roman</li>\r\n");
	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"popupmenu_popup fontsize_menu\" id=\"posteditor_popup_fontsize_menu\" style=\"display: none\">\r\n");
	templateBuilder.Append("		<ul unselectable=\"on\">\r\n");
	templateBuilder.Append("		<li onclick=\"discuzcode('fontsize', 1);hideMenu()\" unselectable=\"on\"><font size=\"1\" unselectable=\"on\">1</font></li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontsize', 2);hideMenu()\" unselectable=\"on\"><font size=\"2\" unselectable=\"on\">2</font></li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontsize', 3);hideMenu()\" unselectable=\"on\"><font size=\"3\" unselectable=\"on\">3</font></li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontsize', 4);hideMenu()\" unselectable=\"on\"><font size=\"4\" unselectable=\"on\">4</font></li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontsize', 5);hideMenu()\" unselectable=\"on\"><font size=\"5\" unselectable=\"on\">5</font></li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontsize', 6);hideMenu()\" unselectable=\"on\"><font size=\"6\" unselectable=\"on\">6</font></li>\r\n");
	templateBuilder.Append("			<li onclick=\"discuzcode('fontsize', 7);hideMenu()\" unselectable=\"on\"><font size=\"7\" unselectable=\"on\">7</font></li>\r\n");
	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"popupmenu_popup colorbar\" id=\"posteditor_popup_forecolor_menu\" style=\"display: none\">\r\n");
	templateBuilder.Append("	<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" unselectable=\"on\" style=\"width: auto;\">\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Black');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Black\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Sienna');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Sienna\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DarkOliveGreen');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DarkOliveGreen\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DarkGreen');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DarkGreen\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DarkSlateBlue');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DarkSlateBlue\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Navy');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Navy\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Indigo');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Indigo\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DarkSlateGray');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DarkSlateGray\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DarkRed');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DarkRed\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DarkOrange');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DarkOrange\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Olive');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Olive\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Green');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Green\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Teal');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Teal\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Blue');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Blue\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'SlateGray');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: SlateGray\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DimGray');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DimGray\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Red');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Red\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'SandyBrown');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: SandyBrown\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'YellowGreen');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: YellowGreen\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'SeaGreen');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: SeaGreen\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'MediumTurquoise');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: MediumTurquoise\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'RoyalBlue');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: RoyalBlue\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Purple');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Purple\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Gray');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Gray\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Magenta');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Magenta\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Orange');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Orange\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Yellow');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Yellow\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Lime');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Lime\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Cyan');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Cyan\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DeepSkyBlue');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DeepSkyBlue\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'DarkOrchid');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: DarkOrchid\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Silver');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Silver\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Pink');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Pink\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Wheat');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Wheat\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'LemonChiffon');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: LemonChiffon\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'PaleGreen');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: PaleGreen\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'PaleTurquoise');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: PaleTurquoise\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'LightBlue');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: LightBlue\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'Plum');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: Plum\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("			<td class=\"editor_colornormal\" onclick=\"discuzcode('forecolor', 'White');hideMenu()\" unselectable=\"on\" onmouseover=\"colorContext(this, 'mouseover')\" onmouseout=\"colorContext(this, 'mouseout')\"><div style=\"background-color: White\" unselectable=\"on\"></div></td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"popupmenu_popup\" id=\"posteditor_popup_table_menu\" style=\"display: none\">\r\n");
	templateBuilder.Append("		<table cellpadding=\"4\" cellspacing=\"0\" border=\"0\" unselectable=\"on\">\r\n");
	templateBuilder.Append("		  <tr class=\"popupmenu_option\">\r\n");
	templateBuilder.Append("				<td nowrap>表格行数:</td>\r\n");
	templateBuilder.Append("				<td nowrap><input type=\"text\" id=\"posteditor_table_rows\" size=\"3\" value=\"2\" /></td>\r\n");
	templateBuilder.Append("				<td nowrap>表格列数:</td>\r\n");
	templateBuilder.Append("				<td nowrap><input type=\"text\" id=\"posteditor_table_columns\" size=\"3\" value=\"2\" /></td>\r\n");
	templateBuilder.Append("		  </tr>\r\n");
	templateBuilder.Append("		  <tr class=\"popupmenu_option\">\r\n");
	templateBuilder.Append("				<td nowrap>表格宽度:</td>\r\n");
	templateBuilder.Append("				<td nowrap><input type=\"text\" id=\"posteditor_table_width\" size=\"3\" value=\"\" /></td>\r\n");
	templateBuilder.Append("				<td nowrap>背景颜色:</td>\r\n");
	templateBuilder.Append("				<td nowrap><input type=\"text\" id=\"posteditor_table_bgcolor\" size=\"3\" /></td>\r\n");
	templateBuilder.Append("		  </tr>\r\n");
	templateBuilder.Append("		  <tr class=\"popupmenu_option\">\r\n");
	templateBuilder.Append("				<td colspan=\"2\" align=\"right\"><input type=\"button\" onclick=\"discuzcode('table')\" value=\"提交\" /></td>\r\n");
	templateBuilder.Append("				<td colspan=\"2\" align=\"left\"><input type=\"button\" onclick=\"hideMenu()\" value=\"取消\" /></td>\r\n");
	templateBuilder.Append("		  </tr>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"popupmenu_popup\" id=\"posteditor_popup_media_menu\" style=\"width: 240px;display: none\">\r\n");
	templateBuilder.Append("	<input type=\"hidden\" id=\"posteditor_mediatype\" value=\"ra\">\r\n");
	templateBuilder.Append("	<input type=\"hidden\" id=\"posteditor_mediaautostart\" value=\"0\">\r\n");
	templateBuilder.Append("	<table cellpadding=\"4\" cellspacing=\"0\" border=\"0\" unselectable=\"on\">\r\n");
	templateBuilder.Append("	<tr class=\"popupmenu_option\">\r\n");
	templateBuilder.Append("		<td nowrap>\r\n");
	templateBuilder.Append("			请输入在线视频的地址:<br />\r\n");
	templateBuilder.Append("			<input id=\"posteditor_mediaurl\" size=\"40\" value=\"\" onkeyup=\"setmediatype('posteditor')\" />\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	<tr class=\"popupmenu_option\">\r\n");
	templateBuilder.Append("		<td nowrap>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\"><input type=\"radio\" name=\"posteditor_mediatyperadio\" id=\"posteditor_mediatyperadio_ra\" onclick=\"$('posteditor_mediatype').value = 'ra'\" checked=\"checked\">RA</label>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\"><input type=\"radio\" name=\"posteditor_mediatyperadio\" id=\"posteditor_mediatyperadio_wma\" onclick=\"$('posteditor_mediatype').value = 'wma'\">WMA</label>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\"><input type=\"radio\" name=\"posteditor_mediatyperadio\" id=\"posteditor_mediatyperadio_mp3\" onclick=\"$('posteditor_mediatype').value = 'mp3'\">MP3</label>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\"><input type=\"radio\" name=\"posteditor_mediatyperadio\" id=\"posteditor_mediatyperadio_rm\" onclick=\"$('posteditor_mediatype').value = 'rm'\">RM/RMVB</label>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\"><input type=\"radio\" name=\"posteditor_mediatyperadio\" id=\"posteditor_mediatyperadio_wmv\" onclick=\"$('posteditor_mediatype').value = 'wmv'\">WMV</label>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\"><input type=\"radio\" name=\"posteditor_mediatyperadio\" id=\"posteditor_mediatyperadio_mov\" onclick=\"$('posteditor_mediatype').value = 'mov'\">MOV</label>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	<tr class=\"popupmenu_option\">\r\n");
	templateBuilder.Append("		<td nowrap>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\">宽: <input id=\"posteditor_mediawidth\" size=\"5\" value=\"400\" /></label>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\">高: <input id=\"posteditor_mediaheight\" size=\"5\" value=\"300\"/></label>\r\n");
	templateBuilder.Append("			<label style=\"float: left; width: 32%\"><input type=\"checkbox\" onclick=\"$('posteditor_mediaautostart').value =this.checked ? 1 : 0\"> 自动播放</label>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	<tr class=\"popupmenu_option\">\r\n");
	templateBuilder.Append("		<td align=\"center\" colspan=\"2\"><input type=\"button\" size=\"8\" value=\"提交\" onclick=\"setmediacode('posteditor')\"> <input type=\"button\" onclick=\"hideMenu()\" value=\"取消\" /></td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<table class=\"editor_text\" summary=\"Message Textarea\" cellpadding=\"0\" cellspacing=\"0\" style=\"table-layout: fixed;\">\r\n");
	templateBuilder.Append("<tr>\r\n");
	templateBuilder.Append("	<td>\r\n");
	templateBuilder.Append("		<textarea class=\"autosave\" name=\"message\" rows=\"10\" cols=\"60\" style=\"width:99%; height:250px\" tabindex=\"100\" id=\"posteditor_textarea\"  onSelect=\"javascript: storeCaret(this);\" onClick=\"javascript: storeCaret(this);\" onKeyUp=\"javascript:storeCaret(this);\" onKeyDown=\"ctlent(event);\">" + message.ToString() + "</textarea><input type=\"hidden\" name=\"sposteditor_mode\" id=\"posteditor_mode\" value=\"" + config.Defaulteditormode.ToString().Trim() + "\" />\r\n");
	templateBuilder.Append("	</td>\r\n");
	templateBuilder.Append("</tr>\r\n");
	templateBuilder.Append("</table>\r\n");
	templateBuilder.Append("		<div id=\"posteditor_bottom\" >\r\n");
	templateBuilder.Append("		<table summary=\"Enitor Buttons\" cellpadding=\"0\" cellspacing=\"0\" class=\"editor_button\" style=\"border-top: none;\">\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<td style=\"border-top: none;\">\r\n");
	templateBuilder.Append("				<div class=\"editor_textexpand\">\r\n");
	templateBuilder.Append("					<img src=\"../../editor/images/contract.gif\" width=\"11\" height=\"21\" title=\"收缩编辑框\" alt=\"收缩编辑框\" id=\"posteditor_contract\" /><img src=\"../../editor/images/expand.gif\" width=\"12\" height=\"21\" title=\"扩展编辑框\" alt=\"扩展编辑框\" id=\"posteditor_expand\" />\r\n");
	templateBuilder.Append("				</div>\r\n");
	templateBuilder.Append("				</td>\r\n");
	templateBuilder.Append("			<td align=\"right\" style=\"border-top: none;\">\r\n");
	templateBuilder.Append("				<button type=\"button\" name=\"restoredata\" id=\"restoredata\">恢复数据</button>\r\n");
	templateBuilder.Append("				<button type=\"button\" id=\"checklength\">字数检查</button>\r\n");
	templateBuilder.Append("				<button type=\"button\" name=\"previewbutton\" id=\"previewbutton\" tabindex=\"102\">预览帖子</button>\r\n");
	templateBuilder.Append("				<button type=\"button\" tabindex=\"103\" id=\"clearcontent\">清空内容</button>\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</table>\r\n");

	if (canpostattach)
	{



	if (attachsize>0)
	{


	if (attachextensions!="")
	{

	templateBuilder.Append("<div class=\"box\" style=\"padding:0;\">\r\n");
	templateBuilder.Append("<table cellspacing=\"0\" cellpadding=\"0\" summary=\"Upload\">\r\n");
	templateBuilder.Append("	<thead>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><img src=\"templates/" + templatepath.ToString() + "/images/attachment.gif\" alt=\"附件\"/>上传附件</th><td class=\"nums\">阅读权限</td><td>描述</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("	</thead>\r\n");
	templateBuilder.Append("	<tbody style=\"display: none;\" id=\"attachbodyhidden\"><tr>\r\n");
	templateBuilder.Append("		<td>\r\n");
	templateBuilder.Append("			<input type=\"file\" name=\"postfile\"/>\r\n");
	templateBuilder.Append("			<span id=\"localfile[]\"></span>&nbsp;\r\n");
	templateBuilder.Append("			<input type=\"hidden\" name=\"localid\" />\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("		<td class=\"nums\"><input type=\"text\" name=\"readperm\" value=\"0\" size=\"1\"/></td>\r\n");
	templateBuilder.Append("		<td><input type=\"text\" name=\"attachdesc\" size=\"25\"/>\r\n");

	if (config.Enablealbum==1 && caninsertalbum)
	{

	templateBuilder.Append("			<select name=\"albums\" style=\"display:none\">\r\n");
	templateBuilder.Append("			<option value=\"0\">请选择相册</option>\r\n");

	int album__loop__id=0;
	foreach(DataRow album in albumlist.Rows)
	{
		album__loop__id++;

	templateBuilder.Append("			<option value=\"" + album["albumid"].ToString().Trim() + "\">" + album["title"].ToString().Trim() + "</option>\r\n");

	}	//end loop

	templateBuilder.Append("			</select>\r\n");

	}	//end if

	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");
	templateBuilder.Append("	<tbody id=\"attachbody\"></tbody>\r\n");
	templateBuilder.Append("	<tbody>\r\n");
	templateBuilder.Append("	<tr>\r\n");
	templateBuilder.Append("		<td style=\"border-bottom: medium none;\" colspan=\"5\">\r\n");
	templateBuilder.Append("			单个附件大小: <strong><script type=\"text/javascript\">ShowFormatBytesStr(" + usergroupinfo.Maxattachsize.ToString().Trim() + ");</" + "script></strong><br/>\r\n");
	templateBuilder.Append("			今天可上传大小: <strong><script type=\"text/javascript\">ShowFormatBytesStr(" + attachsize.ToString() + ");</" + "script></strong><br/>\r\n");
	templateBuilder.Append("			附件类型: <strong>" + attachextensionsnosize.ToString() + "</strong><br/>\r\n");
	templateBuilder.Append("		</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	</tbody>\r\n");
	templateBuilder.Append("</table>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<img id=\"img_hidden\" alt=\"1\" style=\"position:absolute;top:-100000px;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='image');width:400;height:300\"></img>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("var aid = 1;\r\n");
	templateBuilder.Append("var thumbwidth = parseInt(400);\r\n");
	templateBuilder.Append("var thumbheight = parseInt(300);\r\n");
	templateBuilder.Append("var attachexts = new Array();\r\n");
	templateBuilder.Append("var attachwh = new Array();\r\n");
	templateBuilder.Append("function delAttach(id) \r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("    var curattlength = $('attachbody').childNodes.length;\r\n");
	templateBuilder.Append("    $('attachbody').removeChild($('attach_' + id).parentNode.parentNode);\r\n");
	templateBuilder.Append("    $('attachbody').innerHTML == '' && addAttach();\r\n");
	templateBuilder.Append("    if (curattlength == " + config.Maxattachments.ToString().Trim() + " && $(\"attach_\" + (aid-1)).value != \"\")\r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("	    addAttach();\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("	if ($('localimgpreview_' + id + '_menu'))\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		document.body.removeChild($('localimgpreview_' + id + '_menu'));\r\n");
	templateBuilder.Append("	}    \r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("function addAttach() \r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("    if ($('attachbody').childNodes.length > " + config.Maxattachments.ToString().Trim() + " - 1)\r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("	    return;\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("    newnode = $('attachbodyhidden').firstChild.cloneNode(true);\r\n");
	templateBuilder.Append("    var id = aid;\r\n");
	templateBuilder.Append("    var tags;\r\n");
	templateBuilder.Append("    tags = findtags(newnode, 'input');\r\n");
	templateBuilder.Append("    for(i in tags) \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        if(tags[i].name == 'postfile') \r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("            tags[i].id = 'attach_' + id;\r\n");
	templateBuilder.Append("            tags[i].onchange = function() \r\n");
	templateBuilder.Append("            {\r\n");
	templateBuilder.Append("	            insertAttach(id);\r\n");
	templateBuilder.Append("            };\r\n");
	templateBuilder.Append("            tags[i].unselectable = 'on';\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("        if(tags[i].name == 'localid') \r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("            tags[i].value = id;\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("    tags = findtags(newnode, 'span');\r\n");
	templateBuilder.Append("    for(i in tags) \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        if(tags[i].id == 'localfile[]') \r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("            tags[i].id = 'localfile_' + id;\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("    }\r\n");

	if (caninsertalbum)
	{

	templateBuilder.Append("    tags = findtags(newnode, 'select');\r\n");
	templateBuilder.Append("    for(i in tags)\r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        if(tags[i].name == 'albums')\r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("            tags[i].id = 'albums' + id;\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("    }\r\n");

	}	//end if

	templateBuilder.Append("    aid++;\r\n");
	templateBuilder.Append("    $('attachbody').appendChild(newnode);\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("addAttach();\r\n");
	templateBuilder.Append("function insertAttach(id) \r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("    var localimgpreview = '';\r\n");
	templateBuilder.Append("    var path = $('attach_' + id).value;\r\n");
	templateBuilder.Append("    var extensions = '" + attachextensionsnosize.ToString() + "';\r\n");
	templateBuilder.Append("    var ext = path.lastIndexOf('.') == -1 ? '' : path.substr(path.lastIndexOf('.') + 1, path.length).toLowerCase();\r\n");
	templateBuilder.Append("    var re = new RegExp(\"(^|\\\\s|,)\" + ext + \"($|\\\\s|,)\", \"ig\");\r\n");
	templateBuilder.Append("    var localfile = $('attach_' + id).value.substr($('attach_' + id).value.replace(/\\\\/g, '/').lastIndexOf('/') + 1);\r\n");
	templateBuilder.Append("    if(path == '') \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        return;\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("    if(extensions != '' && (re.exec(extensions) == null || ext == '')) \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        alert('对不起，不支持上传此类扩展名的附件。');\r\n");
	templateBuilder.Append("        return;\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("    attachexts[id] = is_ie && in_array(ext, ['gif', 'jpg', 'jpeg', 'png', 'bmp']) ? 2 : 1;\r\n");
	templateBuilder.Append("    var err = false;\r\n");
	templateBuilder.Append("    if(attachexts[id] == 2) \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        $('img_hidden').alt = id;\r\n");
	templateBuilder.Append("        try \r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("	        $('img_hidden').filters.item(\"DXImageTransform.Microsoft.AlphaImageLoader\").sizingMethod = 'image';\r\n");
	templateBuilder.Append("        } \r\n");
	templateBuilder.Append("        catch (e) \r\n");
	templateBuilder.Append("        {err = true;}\r\n");
	templateBuilder.Append("        try \r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("            $('img_hidden').filters.item(\"DXImageTransform.Microsoft.AlphaImageLoader\").src = $('attach_' + id).value;\r\n");
	templateBuilder.Append("        } \r\n");
	templateBuilder.Append("        catch (e) \r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("			alert('无效的图片文件。');\r\n");
	templateBuilder.Append("			delAttach(id);\r\n");
	templateBuilder.Append("			err = true;		\r\n");
	templateBuilder.Append("            return;\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("        var wh = {'w' : $('img_hidden').offsetWidth, 'h' : $('img_hidden').offsetHeight};\r\n");
	templateBuilder.Append("        var aid = $('img_hidden').alt;\r\n");
	templateBuilder.Append("        if(wh['w'] >= thumbwidth || wh['h'] >= thumbheight) \r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("            wh = thumbImg(wh['w'], wh['h']);\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("        attachwh[id] = wh;\r\n");
	templateBuilder.Append("        $('img_hidden').style.width = wh['w']\r\n");
	templateBuilder.Append("        $('img_hidden').style.height = wh['h'];\r\n");
	templateBuilder.Append("        try \r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("	        $('img_hidden').filters.item(\"DXImageTransform.Microsoft.AlphaImageLoader\").sizingMethod = 'scale';\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("        catch (e)\r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("        if (err == true)\r\n");
	templateBuilder.Append("        {\r\n");
	templateBuilder.Append("	        $('img_hidden').src = $('attach_' + id).value;\r\n");
	templateBuilder.Append("        }\r\n");
	templateBuilder.Append("        div = document.createElement('div');\r\n");
	templateBuilder.Append("        div.id = 'localimgpreview_' + id + '_menu';\r\n");
	templateBuilder.Append("        div.style.display = 'none';\r\n");
	templateBuilder.Append("        div.style.marginLeft = '20px';\r\n");
	templateBuilder.Append("        div.className = 'popupmenu_popup';\r\n");
	templateBuilder.Append("        document.body.appendChild(div);\r\n");
	templateBuilder.Append("        div.innerHTML = '<img style=\"filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=\\'scale\\',src=\\'' + encodeURI($('attach_' + id).value).replace(')','%29').replace('(','%28') +'\\');width:'+wh['w']+';height:'+wh['h']+'\" src=\\'images/common/none.gif\\' border=\"0\" aid=\"attach_'+ aid +'\" alt=\"\" />';\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("    var isimg = in_array(ext, ['gif', 'jpg', 'jpeg', 'png', 'bmp']) ? 2 : 1;\r\n");

	if (caninsertalbum)
	{

	templateBuilder.Append("    if(isimg == 2)\r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        $('albums' + id).style.display='';\r\n");
	templateBuilder.Append("        $('albums' + id).disabled = tempaccounts;\r\n");
	templateBuilder.Append("    }\r\n");

	}	//end if

	templateBuilder.Append("    $('localfile_' + id).innerHTML = '<a href=\"###delAttach\" onclick=\"delAttach(' + id + ')\">[删除]</a> <a href=\"###insertAttach\" title=\"点击这里将本附件插入帖子内容中当前光标的位置\" onclick=\"insertAttachtext(' + id + ', ' + err + ');return false;\">[插入]</a> ' +\r\n");
	templateBuilder.Append("    (attachexts[id] == 2 ? '<span id=\"localimgpreview_' + id + '\" onmouseover=\"showMenu(this.id, 0, 0, 1, 0)\"> <span class=\"smalltxt\">[' + id + ']</span> <a href=\"###attachment\" onclick=\"insertAttachtext(' + id + ', ' + err + ');return false;\">' + localfile + '</a></span>' : '<span class=\"smalltxt\">[' + id + ']</span> ' + localfile);\r\n");
	templateBuilder.Append("    $('attach_' + id).style.display = 'none';\r\n");
	templateBuilder.Append("    /*if(isimg == 2)\r\n");
	templateBuilder.Append("        insertAttachtext(id, err);*/\r\n");
	templateBuilder.Append("    addAttach();\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("function insertAttachtext(id, iserr) \r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("    if(!attachexts[id]) \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        return;\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("    if(attachexts[id] == 2) \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        bbinsert && wysiwyg && iserr == false ? insertText($('localimgpreview_' + id + '_menu').innerHTML, false) : AddText('[localimg=' + attachwh[id]['w'] + ',' + attachwh[id]['h'] + ']' + id + '[/localimg]');\r\n");
	templateBuilder.Append("    } \r\n");
	templateBuilder.Append("    else \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        bbinsert && wysiwyg ? insertText('[local]' + id + '[/local]', false) : AddText('[local]' + id + '[/local]');\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("function thumbImg(w, h) \r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("    var x_ratio = thumbwidth / w;\r\n");
	templateBuilder.Append("    var y_ratio = thumbheight / h;\r\n");
	templateBuilder.Append("    var wh = new Array();\r\n");
	templateBuilder.Append("    if((x_ratio * h) < thumbheight) \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        wh['h'] = Math.ceil(x_ratio * h);\r\n");
	templateBuilder.Append("        wh['w'] = thumbwidth;\r\n");
	templateBuilder.Append("    } \r\n");
	templateBuilder.Append("    else \r\n");
	templateBuilder.Append("    {\r\n");
	templateBuilder.Append("        wh['w'] = Math.ceil(y_ratio * w);\r\n");
	templateBuilder.Append("        wh['h'] = thumbheight;\r\n");
	templateBuilder.Append("    }\r\n");
	templateBuilder.Append("    return wh;\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("</" + "script>\r\n");

	}
	else
	{

	templateBuilder.Append("		<div class=\"hintinfo\">							\r\n");
	templateBuilder.Append("				你没有上传附件的权限.\r\n");
	templateBuilder.Append("		</div>\r\n");

	}	//end if


	}
	else
	{

	templateBuilder.Append("	<div class=\"hintinfo\">\r\n");

	if (usergroupinfo.Maxsizeperday>0 && usergroupinfo.Maxattachsize>0)
	{

	templateBuilder.Append("			你目前可上传的附件大小为 0 字节.\r\n");

	}
	else
	{

	templateBuilder.Append("			你没有上传附件的权限.\r\n");

	}	//end if

	templateBuilder.Append("	</div>\r\n");

	}	//end if




	}	//end if

	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</td>\r\n");
	templateBuilder.Append("	</tr>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		if(!(is_ie >= 5 || is_moz >= 2)) {\r\n");
	templateBuilder.Append("			$('restoredata').style.display = 'none';\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("		var editorid = 'posteditor';\r\n");
	templateBuilder.Append("		var textobj = $(editorid + '_textarea');\r\n");
	templateBuilder.Append("		var special = parseInt('0');\r\n");
	templateBuilder.Append("		var charset = 'utf-8';\r\n");
	templateBuilder.Append("	</" + "script>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		var thumbwidth = parseInt(400);\r\n");
	templateBuilder.Append("		var thumbheight = parseInt(300);\r\n");
	templateBuilder.Append("		var extensions = '';\r\n");
	templateBuilder.Append("		lang['post_attachment_ext_notallowed']	= '对不起，不支持上传此类扩展名的附件。';\r\n");
	templateBuilder.Append("		lang['post_attachment_img_invalid']		= '无效的图片文件。';\r\n");
	templateBuilder.Append("		lang['post_attachment_deletelink']		= '删除';\r\n");
	templateBuilder.Append("		lang['post_attachment_insert']			= '点击这里将本附件插入帖子内容中当前光标的位置';\r\n");
	templateBuilder.Append("		lang['post_attachment_insertlink']		= '插入';\r\n");
	templateBuilder.Append("	</" + "script>\r\n");
	templateBuilder.Append("	<script src=\"javascript/post.js\" type=\"text/javascript\"></" + "script>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		var fontoptions = new Array(\"仿宋_GB2312\", \"黑体\", \"楷体_GB2312\", \"宋体\", \"新宋体\", \"微软雅黑\", \"Trebuchet MS\", \"Tahoma\", \"Arial\", \"Impact\", \"Verdana\", \"Times New Roman\");\r\n");
	templateBuilder.Append("		lang['enter_list_item']			= \"输入一个列表项目.\\r\\n留空或者点击取消完成此列表.\";\r\n");
	templateBuilder.Append("		lang['enter_link_url']			= \"请输入链接的地址:\";\r\n");
	templateBuilder.Append("		lang['enter_image_url']			= \"请输入图片链接地址:\";\r\n");
	templateBuilder.Append("		lang['enter_email_link']		= \"请输入此链接的邮箱地址:\";\r\n");
	templateBuilder.Append("		lang['fontname']				= \"字体\";\r\n");
	templateBuilder.Append("		lang['fontsize']				= \"大小\";\r\n");
	templateBuilder.Append("		lang['post_advanceeditor']		= \"全部功能\";\r\n");
	templateBuilder.Append("		lang['post_simpleeditor']		= \"简单功能\";\r\n");
	templateBuilder.Append("		lang['submit']					= \"提交\";\r\n");
	templateBuilder.Append("		lang['cancel']					= \"取消\";\r\n");
	templateBuilder.Append("	</" + "script>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\" src=\"javascript/editor.js\"></" + "script>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\" src=\"javascript/post_editor.js\"></" + "script>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		newEditor(wysiwyg);\r\n");
	templateBuilder.Append("		$(editorid + '_contract').onclick = function() { resizeEditor(-100) };\r\n");
	templateBuilder.Append("		$(editorid + '_expand').onclick = function() { resizeEditor(100) };\r\n");
	templateBuilder.Append("		$('checklength').onclick = function() { checklength($('postform')) };\r\n");
	templateBuilder.Append("		$('previewbutton').onclick = function() { previewpost() };\r\n");
	templateBuilder.Append("		$('clearcontent').onclick = function() { clearcontent() };\r\n");
	templateBuilder.Append("		$('restoredata').onclick = function() { loadData() };\r\n");
	templateBuilder.Append("		$('postform').onsubmit = function() { return validate(this); };\r\n");
	templateBuilder.Append("		try{ $('title').focus(); }catch(e){ }\r\n");
	templateBuilder.Append("	</" + "script>\r\n");


	templateBuilder.Append("		</tr>	      \r\n");
	templateBuilder.Append("		</tbody> \r\n");

	if (isseccode)
	{

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("		<tr>\r\n");
	templateBuilder.Append("			<th><label for=\"vcode\">验证码</label></th>\r\n");
	templateBuilder.Append("			<td>\r\n");

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


	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");

	}	//end if

	templateBuilder.Append("		<tbody>\r\n");
	templateBuilder.Append("			<tr>\r\n");
	templateBuilder.Append("			<th>&nbsp;</th>\r\n");
	templateBuilder.Append("			<td>\r\n");
	templateBuilder.Append("				<input name=\"replysubmit\" type=\"submit\" id=\"postsubmit\" value=\"发表回复\"/>	\r\n");
	templateBuilder.Append("				<input name=\"continuereply\" type=\"checkbox\" \r\n");

	if (continuereply!="")
	{

	templateBuilder.Append("checked\r\n");

	}	//end if

	templateBuilder.Append(" /> 连续回复	[完成后可按 Ctrl + Enter 发布]\r\n");
	templateBuilder.Append("			</td>\r\n");
	templateBuilder.Append("		</tr>\r\n");
	templateBuilder.Append("		</tbody>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("		<p class=\"textmsg\" id=\"divshowuploadmsg\" style=\"display:none\"></p>\r\n");
	templateBuilder.Append("		<p class=\"textmsg succ\" id=\"divshowuploadmsgok\" style=\"display:none\"></p>\r\n");
	templateBuilder.Append("		<input type=\"hidden\" name=\"uploadallowmax\" value=\"10\">\r\n");
	templateBuilder.Append("		<input type=\"hidden\" name=\"uploadallowtype\" value=\"jpg,gif\">\r\n");
	templateBuilder.Append("		<input type=\"hidden\" name=\"thumbwidth\" value=\"300\">\r\n");
	templateBuilder.Append("		<input type=\"hidden\" name=\"thumbheight\" value=\"250\">\r\n");
	templateBuilder.Append("		<input type=\"hidden\" name=\"noinsert\" value=\"0\">\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	</form>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		if (getQueryString('restore') == 1)\r\n");
	templateBuilder.Append("		{\r\n");
	templateBuilder.Append("			loadData(true);\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	</" + "script>\r\n");
	templateBuilder.Append("	<div class=\"mainbox\">\r\n");
	templateBuilder.Append("		<h3>最后5帖</h3>\r\n");
	templateBuilder.Append("		<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">\r\n");
	templateBuilder.Append("			<tbody>\r\n");

	int lastpost__loop__id=0;
	foreach(DataRow lastpost in lastpostlist.Rows)
	{
		lastpost__loop__id++;

	templateBuilder.Append("				<tr>\r\n");
	templateBuilder.Append("					<td width=\"15%\" align=\"left\" style=\"padding-left:2px;\">来自: <a href=\"showuser.aspx?userid=" + lastpost["posterid"].ToString().Trim() + "\">" + lastpost["poster"].ToString().Trim() + "</a></td> 	\r\n");
	templateBuilder.Append("					<td width=\"25%\">回复日期:" + lastpost["postdatetime"].ToString().Trim() + "</td>\r\n");
	templateBuilder.Append("					<td width=\"60%\" align=\"left\">内容:" + lastpost["message"].ToString().Trim() + "</td>	\r\n");
	templateBuilder.Append("				</tr>\r\n");

	}	//end loop

	templateBuilder.Append("			</tbody>\r\n");
	templateBuilder.Append("		</table>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

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
