<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.focuslist" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:03:45.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:03:45. 
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


	templateBuilder.Append("<body id=\"focuslist\">\r\n");
	templateBuilder.Append("<div class=\"wrap\">\r\n");
	templateBuilder.Append("<div id=\"foruminfo\">\r\n");
	templateBuilder.Append("	<div id=\"userinfo\">\r\n");
	templateBuilder.Append("		<div id=\"nav\">\r\n");
	templateBuilder.Append("		<p>积分: <strong>" + userinfo.Credits.ToString().Trim() + "</strong> / 头衔:<strong> " + usergroupinfo.Grouptitle.ToString().Trim() + "</strong> / 你上次访问是在 " + lastvisit.ToString() + "</p>\r\n");
	templateBuilder.Append("		<p>共 <strong>" + totalusers.ToString() + "</strong> 位会员 / 欢迎新会员 <strong>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(lastuserid);
	
	templateBuilder.Append("		<a href=\"" + aspxrewriteurl.ToString() + "\">" + lastusername.ToString() + "</a></strong></p>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>	\r\n");
	templateBuilder.Append("	<div id=\"forumstats\">\r\n");
	templateBuilder.Append("		<p>共 <strong>" + totaltopic.ToString() + "</strong>  篇主题 /<strong> " + totalpost.ToString() + "</strong>  个帖子 / 今日<strong> " + todayposts.ToString() + "</strong>  个帖子</p>\r\n");
	templateBuilder.Append("		<p>\r\n");

	if (userid!=-1)
	{

	templateBuilder.Append("		<a href=\"mytopics.aspx\">我的主题</a>\r\n");
	templateBuilder.Append("		<a href=\"myposts.aspx\">我的帖子</a>\r\n");
	templateBuilder.Append("		<a href=\"search.aspx?posterid=" + userid.ToString() + "&amp;type=digest\">我的精华</a>\r\n");

	}	//end if

	templateBuilder.Append("		<a href=\"showtopiclist.aspx?type=newtopic&amp;newtopic=" + newtopicminute.ToString() + "&amp;forums=all\">查看新帖</a>\r\n");
	templateBuilder.Append("		<a href=\"showtopiclist.aspx?type=digest&amp;forums=all\">精华帖区</a>\r\n");

	if (config.Rssstatus!=0)
	{

	templateBuilder.Append("		<a href=\"tools/rss.aspx\" target=\"_blank\"><img src=\"templates/" + templatepath.ToString() + "/images/rss.gif\" alt=\"Rss\"/></a>\r\n");

	}	//end if

	templateBuilder.Append("	</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");


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



	templateBuilder.Append("<div id=\"forumfocus\">\r\n");
	templateBuilder.Append("	<div class=\"focuslistleft\">\r\n");
	templateBuilder.Append("		<div class=\"mainbox\">\r\n");
	templateBuilder.Append("			<h3>最新精华主题</h3>\r\n");
	templateBuilder.Append("			<ul class=\"navfocuslist\">\r\n");

	int digesttopic__loop__id=0;
	foreach(DataRow digesttopic in digesttopiclist.Rows)
	{
		digesttopic__loop__id++;

	 aspxrewriteurl = this.ShowTopicAspxRewrite(digesttopic["tid"].ToString().Trim(),0);
	

	if (digesttopic["iconid"].ToString().Trim()!="0")
	{

	templateBuilder.Append("						<li><img src=\"images/posticons/" + digesttopic["iconid"].ToString().Trim() + ".gif\" alt=\"smile\"/><a href=\"" + aspxrewriteurl.ToString() + "\" target=\"main\">" + digesttopic["title"].ToString().Trim() + "</a></li>\r\n");

	}
	else
	{

	templateBuilder.Append("						<li class=\"listspace\"><a href=\"" + aspxrewriteurl.ToString() + "\" target=\"main\">" + digesttopic["title"].ToString().Trim() + "</a> </li>\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"focuslistright\">\r\n");
	templateBuilder.Append("		<div class=\"mainbox\">\r\n");
	templateBuilder.Append("			<h3>最新热门主题</h3>\r\n");
	templateBuilder.Append("			<ul class=\"navfocuslist\">\r\n");

	int hottopic__loop__id=0;
	foreach(DataRow hottopic in hottopiclist.Rows)
	{
		hottopic__loop__id++;

	 aspxrewriteurl = this.ShowTopicAspxRewrite(hottopic["tid"].ToString().Trim(),0);
	

	if (hottopic["iconid"].ToString().Trim()!="0")
	{

	templateBuilder.Append("						<li><img src=\"images/posticons/" + hottopic["iconid"].ToString().Trim() + ".gif\" alt=\"smile\"/><a href=\"" + aspxrewriteurl.ToString() + "\" target=\"main\">" + hottopic["title"].ToString().Trim() + "</a></li>\r\n");

	}
	else
	{

	templateBuilder.Append("						<li class=\"listspace\"><a href=\"" + aspxrewriteurl.ToString() + "\" target=\"main\">" + hottopic["title"].ToString().Trim() + "</a> </li>\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<!---bbs-list area end--->\r\n");

	if (forumlinkcount>0)
	{

	templateBuilder.Append("<div class=\"box\">\r\n");
	templateBuilder.Append("	<span class=\"headactions\"><img id=\"forumlinks_img\" src=\"templates/" + templatepath.ToString() + "/images/collapsed_yes.gif\" alt=\"\" onClick=\"toggle_collapse('linklist');\" /></span>\r\n");
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
	foreach(DataRow onlineuser in onlineuserlist.Rows)
	{
		onlineuser__loop__id++;


	if (onlineuser["invisible"].ToString().Trim()=="1")
	{

	 invisiblecount = invisiblecount + 1;
	
	templateBuilder.Append("				<li>(隐身会员)</li>\r\n");

	}
	else
	{

	templateBuilder.Append("					<li>" + onlineuser["olimg"].ToString().Trim() + "\r\n");

	if (onlineuser["userid"].ToString().Trim()=="-1")
	{

	templateBuilder.Append("							" + onlineuser["username"].ToString().Trim() + "\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(onlineuser["userid"].ToString().Trim());
	
	templateBuilder.Append("						<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + onlineuser["username"].ToString().Trim() + "</a>\r\n");

	}	//end if

	templateBuilder.Append("				</li>\r\n");

	}	//end if


	}	//end loop


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
