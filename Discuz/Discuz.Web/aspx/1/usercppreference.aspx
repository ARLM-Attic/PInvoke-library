<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.usercppreference" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:04:54.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:04:54. 
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
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("function changeAvatarOption(currentOption)\r\n");
	templateBuilder.Append("{\r\n");
	templateBuilder.Append("	if ($('enterurl'))\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		$('enterurl').style.display='none';\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("	if ($('uploadfile'))\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		$('uploadfile').style.display='none';\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("	switch (currentOption)\r\n");
	templateBuilder.Append("	{\r\n");
	templateBuilder.Append("		case \"0\":\r\n");
	templateBuilder.Append("			$('templateid').style.display='none';\r\n");
	templateBuilder.Append("			break;\r\n");
	templateBuilder.Append("		case \"1\":\r\n");
	templateBuilder.Append("			$('uploadfile').style.display='block';\r\n");
	templateBuilder.Append("			break;\r\n");
	templateBuilder.Append("		case \"2\":\r\n");
	templateBuilder.Append("			$('enterurl').style.display='block';\r\n");
	templateBuilder.Append("			break;\r\n");
	templateBuilder.Append("	}\r\n");
	templateBuilder.Append("}\r\n");
	templateBuilder.Append("</" + "script>\r\n");
	templateBuilder.Append("<div id=\"foruminfo\">\r\n");
	templateBuilder.Append("	<div id=\"nav\">\r\n");
	templateBuilder.Append("		<a href=\"" + config.Forumurl.ToString().Trim() + "\" class=\"home\">" + config.Forumtitle.ToString().Trim() + "</a>  &raquo; <a href=\"usercp.aspx\">用户中心</a>  &raquo; <strong>个性设置</strong>\r\n");
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
	templateBuilder.Append("<!--主体-->\r\n");
	templateBuilder.Append("<div class=\"controlpannel\">\r\n");

	templateBuilder.Append("<div class=\"pannelmenu\">\r\n");

	if (userid>0)
	{


	if (pagename=="usercptopic.aspx"||pagename=="usercppost.aspx"||pagename=="usercpdigest.aspx"||pagename=="usercpprofile.aspx"      ||pagename=="usercpnewpassword.aspx"||pagename=="usercppreference.aspx")
	{

	templateBuilder.Append("	   <a href=\"usercpprofile.aspx\" class=\"current\"><span>个人设置</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("	   <a href=\"usercpprofile.aspx\">个人设置</a>\r\n");

	}	//end if


	if (pagename=="usercpinbox.aspx"||pagename=="usercpsentbox.aspx"||pagename=="usercpdraftbox.aspx"||pagename=="usercppostpm.aspx"||pagename=="usercpshowpm.aspx"||pagename=="usercppmset.aspx")
	{

	templateBuilder.Append("	   <a href=\"usercpinbox.aspx\" class=\"current\"><span>短消息</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("	   <a href=\"usercpinbox.aspx\">短消息</a>\r\n");

	}	//end if


	if (pagename=="usercpsubscribe.aspx")
	{

	templateBuilder.Append("	   <a href=\"usercpsubscribe.aspx\" class=\"current\"><span>收藏夹</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("	   <a href=\"usercpsubscribe.aspx\">收藏夹</a>\r\n");

	}	//end if


	if (pagename=="usercpcreditspay.aspx"||pagename=="usercpcreditstransfer.aspx"||pagename=="usercpcreditspayoutlog.aspx"||pagename=="usercpcreditspayinlog.aspx"	   ||pagename=="usercpcreaditstransferlog.aspx")
	{

	templateBuilder.Append("       <a href=\"usercpcreditspay.aspx\" class=\"current\"><span>积分交易</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("       <a href=\"usercpcreditspay.aspx\">积分交易</a>\r\n");

	}	//end if


	if (pagename=="usercpforumsetting.aspx")
	{

	templateBuilder.Append("			<a href=\"usercpforumsetting.aspx\" class=\"current\"><span>论坛设置</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("			<a href=\"usercpforumsetting.aspx\">论坛设置</a>\r\n");

	}	//end if


	if (config.Enablespace==1 && user.Spaceid>0)
	{


	if (pagename=="usercpspacepostblog.aspx"||pagename=="usercpspacemanageblog.aspx"||pagename=="usercpspaceeditblog.aspx"||pagename=="usercpspacelinklist.aspx"||pagename=="usercpspacelinkedit.aspx"||pagename=="usercpspacelinkadd.aspx"||pagename=="usercpspacecomment.aspx"||pagename=="usercpspacemanagecategory.aspx"||pagename=="usercpspacecategoryadd.aspx"||pagename=="usercpspacecategoryedit.aspx"||pagename=="usercpspacemanageattachment.aspx"||pagename=="usercpspaceset.aspx")
	{

	templateBuilder.Append("		   <a href=\"usercpspacemanageblog.aspx\" class=\"current\"><span>" + config.Spacename.ToString().Trim() + "</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("		   <a href=\"usercpspacemanageblog.aspx\">" + config.Spacename.ToString().Trim() + "</a>\r\n");

	}	//end if


	}	//end if


	if (config.Enablealbum==1)
	{


	if (pagename=="usercpspacemanagealbum.aspx"||pagename=="usercpspacemanagephoto.aspx"||pagename=="usercpspacephotoadd.aspx"||pagename=="usercpeditphoto.aspx")
	{

	templateBuilder.Append("	            <a href=\"usercpspacemanagealbum.aspx\" class=\"current\"><span>" + config.Albumname.ToString().Trim() + "</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("	            <a href=\"usercpspacemanagealbum.aspx\">" + config.Albumname.ToString().Trim() + "</a>\r\n");

	}	//end if


	}	//end if


	if (config.Enablemall>=1)
	{


	if (pagename=="usercpmygoods.aspx")
	{

	templateBuilder.Append("	            <a href=\"usercpmygoods.aspx\" class=\"current\"><span>我的商品</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("	            <a href=\"usercpmygoods.aspx\">我的商品</a>\r\n");

	}	//end if


	}	//end if


	if (config.Enablemall==2)
	{


	if (pagename=="usercpshopcategory.aspx")
	{

	templateBuilder.Append("	            <a href=\"usercpshopcategory.aspx?item=shopcategory\" class=\"current\"><span>店铺管理</span></a>\r\n");

	}
	else
	{

	templateBuilder.Append("	            <a href=\"usercpshopcategory.aspx?item=shopcategory\">店铺管理</a>\r\n");

	}	//end if


	}	//end if


	}	//end if

	templateBuilder.Append("	</div>\r\n");


	templateBuilder.Append("	<div class=\"pannelcontent\">\r\n");
	templateBuilder.Append("		<div class=\"pcontent\">\r\n");
	templateBuilder.Append("			<div class=\"panneldetail\">\r\n");

	templateBuilder.Append("				<div class=\"panneltabs\">\r\n");

	if (userid>0)
	{

	templateBuilder.Append("					<a href=\"usercpprofile.aspx\"\r\n");

	if (pagename=="usercpprofile.aspx")
	{

	templateBuilder.Append("					 class=\"current\"\r\n");

	}	//end if

	templateBuilder.Append(">编辑个人档案</a>\r\n");
	templateBuilder.Append("					<a href=\"usercpnewpassword.aspx\"\r\n");

	if (pagename=="usercpnewpassword.aspx")
	{

	templateBuilder.Append("					 class=\"current\"\r\n");

	}	//end if

	templateBuilder.Append(">更改密码</a>\r\n");
	templateBuilder.Append("					 <a href=\"usercppreference.aspx\"\r\n");

	if (pagename=="usercppreference.aspx")
	{

	templateBuilder.Append("					 class=\"current\"\r\n");

	}	//end if

	templateBuilder.Append(">个性设置</a>\r\n");

	}	//end if

	templateBuilder.Append("				</div>	\r\n");


	templateBuilder.Append("				<div class=\"pannelbody\">\r\n");
	templateBuilder.Append("					<div class=\"pannellist\">\r\n");

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

	templateBuilder.Append("						<form action=\"\" method=\"post\" ID=\"Form1\" enctype=\"multipart/form-data\">\r\n");
	templateBuilder.Append("								<label for=\"templateid\" class=\"labellong2\" style=\"line-height:220%;\">风格:</label>\r\n");
	templateBuilder.Append("								<select name=\"templateid\" id=\"templateid\" >\r\n");
	templateBuilder.Append("								<option value=\"0\" \r\n");

	if (user.Templateid==0)
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append(">默认</option>\r\n");

	int template__loop__id=0;
	foreach(DataRow template in templatelist.Rows)
	{
		template__loop__id++;

	templateBuilder.Append("								<option value=\"" + template["templateid"].ToString().Trim() + "\" \r\n");

	if (user.Templateid==Utils.StrToInt(template["templateid"].ToString().Trim(), 0))
	{

	templateBuilder.Append("selected\r\n");

	}	//end if

	templateBuilder.Append("								>" + template["name"].ToString().Trim() + "</option>\r\n");

	}	//end loop

	templateBuilder.Append("							    </select>\r\n");
	templateBuilder.Append("								<br />\r\n");
	templateBuilder.Append("								<div class=\"photoimg\" style=\"border-top:1px dashed #CCC; margin-bottom:10px; \">\r\n");
	templateBuilder.Append("								<img src=\"" + user.Avatar.ToString().Trim() + "\" \r\n");

	if (user.Avatarwidth>0)
	{

	templateBuilder.Append("width=\"" + user.Avatarwidth.ToString().Trim() + "\" height=\"" + user.Avatarheight.ToString().Trim() + "\"\r\n");

	}	//end if

	templateBuilder.Append(" id=\"usingavatarimg\" alt=\"形象图\"/>\r\n");

	if (usergroupinfo.Allowavatar>0)
	{

	templateBuilder.Append("										<ul id=\"avatarbox\">\r\n");

	if (usergroupinfo.Allowavatar>1)
	{

	templateBuilder.Append("											<li>\r\n");
	templateBuilder.Append("												<input type=\"radio\" id=\"avatartype1\" name=\"avatartype\" value=\"2\" onclick=\"changeAvatarOption(this.value);\"\r\n");

	if (avatartype==2)
	{

	templateBuilder.Append("													checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("/><label for=\"avatartype1\">使用外部图片</label>\r\n");
	templateBuilder.Append("												<div id=\"enterurl\" style=\"display: none;\">\r\n");
	templateBuilder.Append("													URL地址: <input name=\"avatarurl\" type=\"text\" id=\"avatarurl\" value=\"" + avatarurl.ToString() + "\" size=\"40\" />\r\n");
	templateBuilder.Append("													宽度:\r\n");
	templateBuilder.Append("													<input name=\"avatarwidth\" type=\"text\" id=\"avatarwidth\" value=\"" + avatarwidth.ToString() + "\" size=\"3\" maxlength=\"3\" style=\"width: 30px;\" /> &nbsp; &nbsp; \r\n");
	templateBuilder.Append("													高度:\r\n");
	templateBuilder.Append("													<input name=\"avatarheight\" type=\"text\" id=\"avatarheight\" value=\"" + avatarheight.ToString() + "\" size=\"3\" maxlength=\"3\" style=\"width: 30px;\" />	\r\n");
	templateBuilder.Append("												</div>\r\n");
	templateBuilder.Append("											</li>\r\n");

	}	//end if


	if (usergroupinfo.Allowavatar>2)
	{

	templateBuilder.Append("											<li>\r\n");
	templateBuilder.Append("												<input type=\"radio\" id=\"avatartype2\" name=\"avatartype\" value=\"1\" onclick=\"changeAvatarOption(this.value);\"\r\n");

	if (avatartype==1)
	{

	templateBuilder.Append("													checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("/><label for=\"avatartype2\">上传头像图片</label>\r\n");
	templateBuilder.Append("												<div id=\"uploadfile\" style=\"display: none;\">\r\n");
	templateBuilder.Append("												选择本地图片文件: <input name=\"file\" id=\"file\" type=\"file\" size=\"40\"/>\r\n");
	templateBuilder.Append("												</div>\r\n");
	templateBuilder.Append("											</li>\r\n");

	}	//end if

	templateBuilder.Append("										<li>\r\n");
	templateBuilder.Append("											<input type=\"radio\" id=\"avatartype3\" name=\"avatartype\" value=\"0\" onclick=\"changeAvatarOption(this.value);BOX_show('fromsystem');\" \r\n");

	if (avatartype==0)
	{

	templateBuilder.Append("												checked=\"checked\"\r\n");

	}	//end if

	templateBuilder.Append("											/><label for=\"avatartype3\">使用系统头像 (点击选择...)</label>\r\n");
	templateBuilder.Append("											<input type=\"hidden\" name=\"usingavatar\" id=\"usingavatar\" value=\"" + avatar.ToString() + "\" />\r\n");
	templateBuilder.Append("										</li>\r\n");
	templateBuilder.Append("										</ul>\r\n");

	}
	else
	{

	templateBuilder.Append("										<ul style=\"margin-top: 40px;\"><li>您所在的用户组 \"" + usergroupinfo.Grouptitle.ToString().Trim() + "\" 没有更改头像的权限</li></ul>\r\n");

	}	//end if

	templateBuilder.Append("								</div>\r\n");
	templateBuilder.Append("								<input id=\"sendmsg\" type=\"submit\" value=\"确定\" name=\"sendmsg\"/>\r\n");
	templateBuilder.Append("						</form>\r\n");

	}	//end if

	templateBuilder.Append("						</div>\r\n");

	}
	else
	{


	templateBuilder.Append("<div class=\"box message\">\r\n");
	templateBuilder.Append("	<h1>错误显示</h1>\r\n");
	templateBuilder.Append("	<p>" + msgbox_text.ToString() + "</p>\r\n");
	templateBuilder.Append("	<p class=\"errorback\">\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			if(" + msgbox_showbacklink.ToString() + ")\r\n");
	templateBuilder.Append("			{\r\n");
	templateBuilder.Append("				document.write(\"<a href=\\\"" + msgbox_backlink.ToString() + "\\\">返回上一步</a> &nbsp; &nbsp;|  \");\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		</" + "script>\r\n");
	templateBuilder.Append("		&nbsp; &nbsp; <a href=\"forumindex.aspx\">论坛首页</a>\r\n");

	if (usergroupid==7)
	{

	templateBuilder.Append("		 |&nbsp; &nbsp; <a href=\"register.aspx\">注册</a>\r\n");

	}	//end if

	templateBuilder.Append("	</p>\r\n");
	templateBuilder.Append("</div>\r\n");



	}	//end if

	templateBuilder.Append("			  </div>\r\n");
	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<!--主体-->\r\n");
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



	if (page_err==0 && !ispost)
	{

	templateBuilder.Append("	<div id=\"BOX_overlay\" style=\"background: #000; position: absolute; z-index:100; filter:alpha(opacity=50);-moz-opacity: 0.6;opacity: 0.6;\"></div>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\" src=\"javascript/template_album.js\"></" + "script>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		function usethisavatar(e)\r\n");
	templateBuilder.Append("		{\r\n");
	templateBuilder.Append("			$('usingavatar').value = e.src;\r\n");
	templateBuilder.Append("			var avatars = $('fromsystem').getElementsByTagName('div');\r\n");
	templateBuilder.Append("			for (var i=0; i < avatars.length;i++)\r\n");
	templateBuilder.Append("			{\r\n");
	templateBuilder.Append("				avatars[i].style.border = \"2px dashed white\";\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("			e.parentNode.style.border = \"2px dashed black\";\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("		function selectionborder(e)\r\n");
	templateBuilder.Append("		{\r\n");
	templateBuilder.Append("			if (e.style.border != \"2px dashed black\" && e.style.border != \"black 2px dashed\")\r\n");
	templateBuilder.Append("			{				\r\n");
	templateBuilder.Append("				if (e.style.border == \"2px dashed red\" || e.style.border == \"red 2px dashed\" )\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					e.style.border = \"2px dashed white\";\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("				else\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					e.style.border = \"2px dashed red\";\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("			}\r\n");
	templateBuilder.Append("		}\r\n");
	templateBuilder.Append("	</" + "script>\r\n");
	templateBuilder.Append("	<div id=\"fromsystem\" class=\"avatarbackground\" style=\"display: none; position: relative;\">\r\n");
	templateBuilder.Append("			<div class=\"avatarlist\">\r\n");
	templateBuilder.Append("				<ul>\r\n");

	int avatarfile__loop__id=0;
	foreach(DataRow avatarfile in avatarfilelist.Rows)
	{
		avatarfile__loop__id++;

	templateBuilder.Append("						<li>\r\n");
	templateBuilder.Append("							<div onmouseover=\"selectionborder(this)\" onmouseout=\"selectionborder(this)\" style=\"\r\n");

	if (avatarfile["filename"].ToString().Trim()==avatar)
	{

	templateBuilder.Append("border: 2px dashed black;\r\n");

	}
	else
	{

	templateBuilder.Append("border: 2px dashed white;\r\n");

	}	//end if

	templateBuilder.Append("cursor: pointer; overflow:hidden; zoom:1;\" ><img src=\"" + avatarfile["filename"].ToString().Trim() + "\" onclick=\"usethisavatar(this);\" alt=\"形象图名称\"/></div>\r\n");
	templateBuilder.Append("						</li>\r\n");

	}	//end loop

	templateBuilder.Append("				</ul>\r\n");
	templateBuilder.Append("			</div>\r\n");
	templateBuilder.Append("			<span class=\"avatarbutton\">\r\n");
	templateBuilder.Append("				<input type=\"button\" name=\"userthisavatar\" id=\"userthisavatar\" class=\"sbutton\" value=\"确定\" onclick=\"$('templateid').style.display='';$('usingavatarimg').src=$('usingavatar').value;BOX_remove('fromsystem');\" />\r\n");
	templateBuilder.Append("				<input type=\"button\" name=\"canceluserthisavatar\" id=\"canceluserthisavatar\" class=\"sbutton\" value=\"取消\" onclick=\"$('templateid').style.display='';BOX_remove('fromsystem');\" />\r\n");
	templateBuilder.Append("			</span>\r\n");
	templateBuilder.Append("	</div>\r\n");

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
