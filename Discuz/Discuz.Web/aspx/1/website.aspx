<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.website" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="Discuz.Common" %>
<%@ Import namespace="Discuz.Forum" %>
<%@ Import namespace="Discuz.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Discuz!NT Template Engine at 2008/6/12 11:05:04.
		本页面代码由Discuz!NT模板引擎生成于 2008/6/12 11:05:04. 
	*/

	base.OnInit(e);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	templateBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	templateBuilder.Append("<head>\r\n");
	templateBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n");
	templateBuilder.Append("" + meta.ToString() + "\r\n");
	templateBuilder.Append("<title>" + pagetitle.ToString() + " " + config.Seotitle.ToString().Trim() + " - " + config.Webtitle.ToString().Trim() + " - Powered by Discuz!NT</title>\r\n");
	templateBuilder.Append("<link rel=\"icon\" href=\"favicon.ico\" type=\"image/x-icon\" />\r\n");
	templateBuilder.Append("<link rel=\"shortcut icon\" href=\"favicon.ico\" type=\"image/x-icon\" /> \r\n");
	templateBuilder.Append("<!-- 调用样式表 -->\r\n");
	templateBuilder.Append("<link rel=\"stylesheet\" href=\"templates/" + templatepath.ToString() + "/website.css\" type=\"text/css\" media=\"all\"  />\r\n");
	templateBuilder.Append("" + link.ToString() + "\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_report.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_utils.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/common.js\"></" + "script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/menu.js\"></" + "script>\r\n");
	templateBuilder.Append("" + script.ToString() + "\r\n");
	templateBuilder.Append("</head>\r\n");
	templateBuilder.Append("<body>\r\n");
	templateBuilder.Append("<div id=\"container\">\r\n");
	templateBuilder.Append("<div id=\"wraper\">\r\n");
	templateBuilder.Append("<!--header start-->\r\n");
	templateBuilder.Append("<div id=\"header\">\r\n");
	templateBuilder.Append("	<h2><a href=\"" + config.Forumurl.ToString().Trim() + "\" title=\"Discuz!NT|BBS|论坛 - Powered by Discuz!NT\"><img src=\"templates/" + templatepath.ToString() + "/images/logo.gif\" alt=\"Discuz!NT|BBS|论坛\"/></a>\r\n");
	templateBuilder.Append("	</h2>\r\n");

	if (headerad!="")
	{

	templateBuilder.Append("		<div id=\"ad_headerbanner\">" + headerad.ToString() + "</div>\r\n");

	}	//end if

	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div id=\"menu\">\r\n");
	templateBuilder.Append("	<span class=\"avataonline\">\r\n");

	if (userid==-1)
	{

	templateBuilder.Append("		<form id=\"loginform\" name=\"login\" method=\"post\" action=\"login.aspx?loginsubmit=true\">\r\n");
	templateBuilder.Append("			<input type=\"hidden\" name=\"referer\" value=\"website.aspx\" />\r\n");
	templateBuilder.Append("			<input onclick=\"if(this.value=='用户名')this.value = ''\" value=\"用户名\" tabindex=\"1\" maxlength=\"40\" size=\"15\" name=\"postusername\" id=\"username\" type=\"text\" />\r\n");
	templateBuilder.Append("			<input type=\"password\" size=\"10\" name=\"password\" id=\"password\" tabindex=\"2\" />\r\n");
	templateBuilder.Append("			<button value=\"true\" type=\"submit\" name=\"userlogin\"> 登录 </button>\r\n");
	templateBuilder.Append("		</form>\r\n");

	}
	else
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(userid);
	
	templateBuilder.Append("		  <cite>欢迎:<a class=\"dropmenu\" id=\"viewpro\" onmouseover=\"showMenu(this.id)\">" + username.ToString() + "</a></cite>\r\n");
	templateBuilder.Append("		   <a href=\"" + forumurl.ToString() + "logout.aspx?userkey=" + userkey.ToString() + "&reurl=website.aspx\">退出</a>\r\n");

	}	//end if

	templateBuilder.Append("	</span>\r\n");
	templateBuilder.Append("	<ul>\r\n");

	if (userid!=-1)
	{

	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "usercpinbox.aspx\" class=\"notabs\">短消息</a></li>\r\n");
	templateBuilder.Append("		<li id=\"my\" class=\"dropmenu\" onMouseOver=\"showMenu(this.id);\"><a href=\"#\">我的</a></li>\r\n");

	}	//end if

	templateBuilder.Append("		<li><a href=\"" + forumurl.ToString() + "tags.aspx\" \r\n");

	if (userid==-1)
	{

	templateBuilder.Append("class=\"notabs\"\r\n");

	}	//end if

	templateBuilder.Append(" >标签</a></li>\r\n");
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
	templateBuilder.Append("<div id=\"infobox\">	\r\n");
	templateBuilder.Append("	<div id=\"ntforumposition\">\r\n");

	if (announcementcount>0)
	{

	templateBuilder.Append("	<div class=\"ntforumnote\">\r\n");
	templateBuilder.Append("		<dl>\r\n");
	templateBuilder.Append("			<dt>公告:</dt>\r\n");
	templateBuilder.Append("			<dd>\r\n");
	templateBuilder.Append("			<ul>\r\n");
	templateBuilder.Append("				<li>\r\n");
	templateBuilder.Append("					<marquee width=\"98%\" direction=\"left\" scrollamount=\"2\" scrolldelay=\"1\" onmouseover=\"this.stop();\" onmouseout=\"this.start();\">\r\n");

	int announcement__loop__id=0;
	foreach(DataRow announcement in announcementlist.Rows)
	{
		announcement__loop__id++;

	templateBuilder.Append("						<a href=\"" + forumurl.ToString() + "announcement.aspx#" + announcement["id"].ToString().Trim() + "\">" + announcement["title"].ToString().Trim() + "</a><cite>\r\n");
	templateBuilder.Append(Convert.ToDateTime(announcement["starttime"].ToString().Trim()).ToString("MM.dd"));
	templateBuilder.Append("</cite>\r\n");

	}	//end loop

	templateBuilder.Append("					</marquee>\r\n");
	templateBuilder.Append("				</li>\r\n");
	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("			</dd>\r\n");
	templateBuilder.Append("		</dl>\r\n");
	templateBuilder.Append("	</div>		    \r\n");

	}	//end if

	templateBuilder.Append("	<div class=\"ntforumsearch\">\r\n");
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
	templateBuilder.Append("    </div>\r\n");
	templateBuilder.Append("</div>\r\n");



	if (page_err==0)
	{


	/*
	聚合首面方法说明
	
	///////////////////////////////////////////////////////////////////////////////////////////////
	
	方法名称: GetForumTopicList(count, views, forumid, timetype, ordertype, isdigest, onlyimg)
	方法说明: 返回指定条件的主题列表信息
	参数说明:
	          count : 返回的主题数 
	          views : 浏览量 [返回等于或大于当前浏览量的主题]
	          forumid : 版块ID [默认值 0 为所有版块]
	          timetype : 指定时间段内的主题 [ TopicTimeType.Day(一天内)  , TopicTimeType.Week(一周内),   TopicTimeType.Month(一个月内),   TopicTimeType.SixMonth(六个月内),  TopicTimeType.Year(一年内),  TopicTimeType.All(默认 从1754-1-1至今的所有主题)
	          ordertype : 排序字段(降序) [TopicOrderType.ID(默认 主题ID) , TopicOrderType.Views(浏览量),   TopicOrderType.LastPost(最后回复),    TopicOrderType.PostDataTime(按最新主题查),    TopicOrderType.Digest(按精华主题查),    TopicOrderType.Replies(按回复数)]  
	          isdigest : 是否精化 [true(仅返回精华主题)   false(不加限制)]
	          onlyimg : 是否包含附件 [true(仅返回包括图片附件的主题)   false(不加限制)]
	      
	//////////////////////////////////////////////////////////////////////////////////////////////    
	
	方法名称: GetHotForumList(count)   
	方法说明: 返回指定数量的热门版块列表
	参数说明:
	          count : 返回的版块数
	    
	//////////////////////////////////////////////////////////////////////////////////////////////      
	
	方法名称: GetForumList(forumid)   
	方法说明: 返回指定版块下的所有子段块列表
	参数说明:
	          forumid : 指定的版块id
	      
	//////////////////////////////////////////////////////////////////////////////////////////////  
	
	方法名称: GetLastPostList(forumid, count)   
	方法说明: 返回指定版块下的最新回帖列表
	参数说明:
	          forumid : 指定的版块id     
	          count : 返回的回帖数
	 
	//////////////////////////////////////////////////////////////////////////////////////////////  
	
	方法名称: GetAlbumList(photoconfig.Focusalbumshowtype, count, days)   
	方法说明: 返回指定条件的相册列表
	参数说明:
	          photoconfig.Focusalbumshowtype : 排序字段(降序) [1(浏览量), 2(照片数), 3(创建时间)]    注:管理后台聚合设置项
	          count : 返回的相册数
	          days :有效天数 [指定天数内的相册]
	      
	//////////////////////////////////////////////////////////////////////////////////////////////  
	
	方法名称: GetWeekHotPhotoList(photoconfig.Weekhot)
	方法说明: 返回指定数量的热门图片
	参数说明:
	          photoconfig.Weekhot : 返回的热图数量  注:管理后台聚合设置项
	          
	//////////////////////////////////////////////////////////////////////////////////////////////  
	
	方法名称: GetSpaceTopComments(count)
	方法说明: 返回指定数量的空间最新评论
	参数说明:
	          count : 返回的评论数
	          
	//////////////////////////////////////////////////////////////////////////////////////////////  
	
	方法名称: GetRecentUpdateSpaceList(count)
	方法说明: 返回指定数量的最新更新空间列表
	参数说明:
	          count : 返回的空间信息数
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////  
	
	方法名称: GetGoodsList(condition, orderby, categoryid, count)
	方法说明: 返回指定数量的最新更新空间列表
	参数说明:
	          condition : 条件 [recommend(仅返回推荐商品, 商城模式下可用) , quality_new(仅返回全新(状态)商品),    quality_old(仅返回二手(状态)商品)]  
	          orderby: 排序字段(降序) [viewcount(按浏览量排序),    hotgoods(按商品交易量排序),  newgoods(按发布商品先后顺序排序) ]
	          categoryid : 商品所属分类id [默认值 0 为不加限制]
	          count : 返回的商品数
	          
	 
	//////////////////////////////////////////////////////////////////////////////////////////////  
	
	方法名称: GetUserList(count, orderby)
	方法说明: 返回指定数量及排序方式的用户列表
	参数说明:
	          count : 返回的用户数       
	          orderby: 排序字段(降序) [credits(用户积分), posts(用户发帖数), lastactivity(最后活动时间), joindate(注册时间), oltime(在线时间)]
	*/
	
	templateBuilder.Append("<script type=\"text/javascript\" src=\"javascript/template_website.js\"></" + "script>\r\n");
	templateBuilder.Append("<div class=\"mainbox\">\r\n");
	templateBuilder.Append("	<div class=\"box firstbox\">\r\n");
	templateBuilder.Append("		<div class=\"focusbox\">\r\n");

	if (rotatepicdata!=null && rotatepicdata!="")
	{

	templateBuilder.Append("			<div class=\"modulebox sidebox\" style=\"padding:1px;\">\r\n");
	templateBuilder.Append("				<script type='text/javascript'>\r\n");
	templateBuilder.Append("				var imgwidth = 237;\r\n");
	templateBuilder.Append("				var imgheight = 210;\r\n");
	templateBuilder.Append("				</" + "script>			\r\n");
	templateBuilder.Append("				<!--图片轮换代码开始-->\r\n");
	templateBuilder.Append("				<script type=\"text/javascript\" src=\"javascript/template_rotatepic.js\"></" + "script>\r\n");
	templateBuilder.Append("				<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("				var data = { };\r\n");
	templateBuilder.Append("				" + rotatepicdata.ToString() + "\r\n");
	templateBuilder.Append("				var ri = new MzRotateImage();\r\n");
	templateBuilder.Append("				ri.dataSource = data;\r\n");
	templateBuilder.Append("				ri.width = 237;\r\n");
	templateBuilder.Append("				ri.height = 210;\r\n");
	templateBuilder.Append("				ri.interval = 3000;\r\n");
	templateBuilder.Append("				ri.duration = 2000;\r\n");
	templateBuilder.Append("				document.write(ri.render());\r\n");
	templateBuilder.Append("				</" + "script>\r\n");
	templateBuilder.Append("				<!--图片轮换代码结束-->\r\n");
	templateBuilder.Append("			</div>\r\n");

	}	//end if

	templateBuilder.Append("        <!--\r\n");
	templateBuilder.Append("			<div id=\"focusimg\"><img src=\"images/gather/img.gif\"/></div>\r\n");
	templateBuilder.Append("			<h3>春节前将为大家准备一个大礼包</h3>\r\n");
	templateBuilder.Append("			<div class=\"focuspage\"><a href=\"#\" class=\"current\">1</a><a href=\"#\">2</a><a href=\"#\">3</a><a href=\"#\">4</a><a href=\"#\">5</a></div>\r\n");
	templateBuilder.Append("		--->\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box newtopicbox\">\r\n");
	templateBuilder.Append("		<h2>最新主题</h2>\r\n");

	if (postlist.Length>0)
	{

	templateBuilder.Append("		<dl>\r\n");

	int __postinfo__loop__id=0;
	foreach(PostInfo __postinfo in postlist)
	{
		__postinfo__loop__id++;


	if (__postinfo__loop__id==1)
	{

	 aspxrewriteurl = this.ShowTopicAspxRewrite(__postinfo.Tid,0);
	
	templateBuilder.Append("				<dt><strong><a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\">" + __postinfo.Title.ToString().Trim() + "</a></strong>\r\n");
	templateBuilder.Append("				<cite>\r\n");

	if (__postinfo.Posterid>0)
	{

	 aspxrewriteurl = this.UserInfoAspxRewrite(__postinfo.Posterid);
	
	templateBuilder.Append("				    <a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + __postinfo.Poster.ToString().Trim() + "</a>\r\n");

	}
	else
	{

	templateBuilder.Append("				    " + __postinfo.Poster.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("				      " + __postinfo.Postdatetime.ToString().Trim() + "\r\n");
	templateBuilder.Append("				</cite></dt>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(__postinfo.Tid,0);
	
	templateBuilder.Append("				<dd>" + __postinfo.Message.ToString().Trim() + " <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">详细</a></dd>\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("		</dl>\r\n");

	}	//end if

	templateBuilder.Append("		<ul class=\"topiclist\">\r\n");

	if (postlist.Length>0)
	{

	 topiclist = forumagg.GetForumTopicList(6, 0, 0, TopicTimeType.All, TopicOrderType.PostDataTime, false, false);
	

	}
	else
	{

	 topiclist = forumagg.GetForumTopicList(10, 0, 0, TopicTimeType.All, TopicOrderType.PostDataTime, false, false);
	

	}	//end if


	int __newtopicinfo__loop__id=0;
	foreach(DataRow __newtopicinfo in topiclist.Rows)
	{
		__newtopicinfo__loop__id++;

	templateBuilder.Append("            <li>\r\n");
	templateBuilder.Append("                <cite>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(__newtopicinfo["fid"].ToString().Trim(),0);
	
	templateBuilder.Append("                    <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + __newtopicinfo["name"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("                </cite>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(__newtopicinfo["tid"].ToString().Trim(),0);
	
	templateBuilder.Append("                <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(__newtopicinfo["title"].ToString().Trim(),0,43,"..."));
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("           </li>            \r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\">\r\n");
	templateBuilder.Append("		<div class=\"titlebar\"><ul><li class=\"current\" id=\"li_hotforum\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_hotforum'));\">热门版块</a></li><li id=\"li_bbsmessage\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_bbsmessage'));\">论坛信息</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"hotforum\">\r\n");
	 hotforumlist = forumagg.GetHotForumList(10);
	

	int __foruminfo__loop__id=0;
	foreach(DataRow __foruminfo in hotforumlist.Rows)
	{
		__foruminfo__loop__id++;

	 aspxrewriteurl = this.ShowForumAspxRewrite(__foruminfo["fid"].ToString().Trim(),0);
	
	templateBuilder.Append("			<li><em>" + __foruminfo["topics"].ToString().Trim() + "</em><cite \r\n");

	if (__foruminfo__loop__id==1)
	{

	templateBuilder.Append("class=\"first\"\r\n");

	}	//end if


	if (__foruminfo__loop__id==2)
	{

	templateBuilder.Append("class=\"second\"\r\n");

	}	//end if


	if (__foruminfo__loop__id==3)
	{

	templateBuilder.Append("class=\"third\"\r\n");

	}	//end if

	templateBuilder.Append(" > " + __foruminfo__loop__id.ToString() + "</cite><a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + __foruminfo["name"].ToString().Trim() + "</a></li>\r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		<ul id=\"bbsmessage\"  style=\"display:none;\">\r\n");
	templateBuilder.Append("			<li>会员总数: <i>" + totalusers.ToString() + "</i>人</li>\r\n");
	templateBuilder.Append("			<li>最新注册会员:<i>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(lastuserid);
	
	templateBuilder.Append("<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + lastusername.ToString() + "</a></i></li>\r\n");
	templateBuilder.Append("			<li>主题数:<i>" + totaltopic.ToString() + "</i>主题</li>\r\n");
	templateBuilder.Append("			<li>帖子数:<i>" + totalpost.ToString() + "</i> 个(含回帖) </li>\r\n");
	templateBuilder.Append("			<li>今  日:<i>" + todayposts.ToString() + "</i>贴  昨 日: <i>" + yesterdayposts.ToString() + "</i> 贴</li>\r\n");

	if (highestpostsdate!="")
	{

	templateBuilder.Append("			    <li>	\r\n");
	templateBuilder.Append("		            最高日帖子:<i>" + highestposts.ToString() + "</i>个\r\n");
	templateBuilder.Append("		        </li>\r\n");
	templateBuilder.Append("		        <li>	\r\n");
	templateBuilder.Append("		            最高日帖子发生于:<i>" + highestpostsdate.ToString() + "</i>\r\n");
	templateBuilder.Append("		        </li>\r\n");

	}	//end if

	templateBuilder.Append("			<li>在线总数:<i>" + totalonline.ToString() + "</i>人</li>\r\n");
	templateBuilder.Append("			<li>最高在线:<i>" + highestonlineusercount.ToString() + "</i> 人 </li>\r\n");
	templateBuilder.Append("			<li>最高在线发生于:<i>" + highestonlineusertime.ToString() + "</i></li>\r\n");
	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("var reco_topic = " + forumagg.GetTopicJsonFromFile().ToString().Trim() + ";\r\n");
	templateBuilder.Append("var templatepath = \"" + templatepath.ToString() + "\";\r\n");
	templateBuilder.Append("var aspxrewrite = " + config.Aspxrewrite.ToString().Trim() + ";\r\n");
	templateBuilder.Append("</" + "script>\r\n");

	int forumid__loop__id=0;
	foreach(int forumid in forumidarray)
	{
		forumid__loop__id++;

	ForumInfo foruminfo = Forums.GetForumInfo(forumid);
	

	if (foruminfo!=null)
	{

	templateBuilder.Append("<div class=\"mainbox\">\r\n");
	templateBuilder.Append("	<div class=\"box topicbox\">\r\n");
	templateBuilder.Append("		<span>\r\n");

	int sub_forum__loop__id=0;
	foreach(DataRow sub_forum in Forums.GetForumList(forumid).Rows)
	{
		sub_forum__loop__id++;


	if (sub_forum__loop__id<=5)
	{

	 aspxrewriteurl = this.ShowForumAspxRewrite(sub_forum["fid"].ToString().Trim(),0);
	
	templateBuilder.Append("		    <a href=\"" + aspxrewriteurl.ToString() + "\" tabindex=\"_blank\">" + sub_forum["name"].ToString().Trim() + "</a>\r\n");

	}	//end if


	}	//end loop

	 aspxrewriteurl = this.ShowForumAspxRewrite(forumid,0);
	
	templateBuilder.Append("		<a href=\"" + aspxrewriteurl.ToString() + "\" tabindex=\"_blank\">更多&gt;&gt;</a>\r\n");
	templateBuilder.Append("		</span>\r\n");
	templateBuilder.Append("		<h2><a href=\"" + aspxrewriteurl.ToString() + "\" tabindex=\"_blank\">" + foruminfo.Name.ToString().Trim() + "</a></h2>\r\n");
	templateBuilder.Append("		<div class=\"maintopic\">\r\n");
	templateBuilder.Append("		<script type=\"text/javascript\">document.write(showtopicinfo(" + forumid.ToString() + ", " + forumid__loop__id.ToString() + "-1));</" + "script>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<ul class=\"topiclist\">\r\n");
	 topiclist = forumagg.GetForumTopicList(8, 0, forumid, TopicTimeType.All, TopicOrderType.PostDataTime, false, false);
	

	int newtopicinfo__loop__id=0;
	foreach(DataRow newtopicinfo in topiclist.Rows)
	{
		newtopicinfo__loop__id++;

	templateBuilder.Append("		   <li>\r\n");
	templateBuilder.Append("                <cite>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(newtopicinfo["fid"].ToString().Trim(),0);
	
	templateBuilder.Append("                    <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + newtopicinfo["name"].ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("                </cite>\r\n");
	 aspxrewriteurl = this.ShowTopicAspxRewrite(newtopicinfo["tid"].ToString().Trim(),0);
	
	templateBuilder.Append("                <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(newtopicinfo["title"].ToString().Trim(),0,43,"..."));
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("           </li>   \r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\">\r\n");
	templateBuilder.Append("		<div class=\"titlebar\"><ul><li class=\"current\" id=\"li_forum_" + forumid.ToString() + "_topic\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_forum_" + forumid.ToString() + "_topic')," + forumid.ToString() + ");\">最热主题</a></li><li id=\"li_forum_" + forumid.ToString() + "_reply\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_forum_" + forumid.ToString() + "_reply'), " + forumid.ToString() + ");\">最新回复主题</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"forum_" + forumid.ToString() + "_topic\" class=\"topicdot\">\r\n");
	 topiclist = forumagg.GetForumTopicList(8, 0, forumid, TopicTimeType.All, TopicOrderType.Replies, false, false);
	

	if (topiclist.Rows.Count>0)
	{


	int hottopicinfo__loop__id=0;
	foreach(DataRow hottopicinfo in topiclist.Rows)
	{
		hottopicinfo__loop__id++;

	 aspxrewriteurl = this.ShowTopicAspxRewrite(hottopicinfo["tid"].ToString().Trim(),0);
	
	templateBuilder.Append("            <li><em>" + hottopicinfo["replies"].ToString().Trim() + "</em><a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(hottopicinfo["title"].ToString().Trim(),0,18,"..."));
	templateBuilder.Append("</a></li> \r\n");

	}	//end loop


	}
	else
	{

	templateBuilder.Append("		    暂无数据!\r\n");

	}	//end if

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		<ul id=\"forum_" + forumid.ToString() + "_reply\" class=\"topicdot\" style=\"display:none;\">\r\n");
	 topiclist = forumagg.GetForumTopicList(8, 0, forumid, TopicTimeType.All, TopicOrderType.LastPost, false, false);
	

	if (topiclist.Rows.Count>0)
	{


	int replytopic__loop__id=0;
	foreach(DataRow replytopic in topiclist.Rows)
	{
		replytopic__loop__id++;

	 aspxrewriteurl = this.ShowTopicAspxRewrite(replytopic["tid"].ToString().Trim(),0);
	
	templateBuilder.Append("            <li><em><script type=\"text/javascript\">document.write(convertdate('" + replytopic["postdatetime"].ToString().Trim() + "'));</" + "script></em><a href=\"" + forumurl.ToString() + "showtopic.aspx?topicid=" + replytopic["tid"].ToString().Trim() + "&page=end#lastpost\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(replytopic["title"].ToString().Trim(),0,20,"..."));
	templateBuilder.Append("</a></li> \r\n");

	}	//end loop


	}
	else
	{

	templateBuilder.Append("		    暂无数据!\r\n");

	}	//end if

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("<!--<div class=\"adinner\"><img src=\"images/gather/ad.gif\" alt=\"广告\"/></div>-->\r\n");

	if (config.Enablealbum==1)
	{

	templateBuilder.Append("<div class=\"mainbox\">\r\n");
	templateBuilder.Append("	<div class=\"box albumbox\">\r\n");
	templateBuilder.Append("		<span>\r\n");

	int ac__loop__id=0;
	foreach(AlbumCategoryInfo ac in albumcategorylist)
	{
		ac__loop__id++;


	if (ac__loop__id<5)
	{

	templateBuilder.Append("				<a href=\"showalbumlist.aspx?cate=" + ac.Albumcateid.ToString().Trim() + "\" target=\"_blank\">" + ac.Title.ToString().Trim() + "</a>\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("		<a href=\"albumindex.aspx\" target=\"_blank\">更多&gt;&gt;</a></span>\r\n");
	templateBuilder.Append("		<h2>推荐相册</h2>\r\n");

	int __albuminfo__loop__id=0;
	foreach(AlbumInfo __albuminfo in recommendalbumlist)
	{
		__albuminfo__loop__id++;


	if (__albuminfo__loop__id<=4)
	{

	templateBuilder.Append("			<dl>\r\n");
	templateBuilder.Append("			    <dd>\r\n");
	templateBuilder.Append("			        <a href=\"" + albumurl.ToString() + "showalbum.aspx?albumid=" + __albuminfo.Albumid.ToString().Trim() + "\">\r\n");

	if (__albuminfo.Logo!="")
	{

	templateBuilder.Append("			                <img src=\"" + __albuminfo.Logo.ToString().Trim() + "\" alt=\"" + __albuminfo.Title.ToString().Trim() + "\" style=\"height: 75px; width: 115px\"/>\r\n");

	}
	else
	{

	templateBuilder.Append("							<img src=\"templates/" + templatepath.ToString() + "/images/NoPhoto.jpg\" alt=\"" + __albuminfo.Albumid.ToString().Trim() + "\"  style=\"height: 75px; width: 115px\"/>\r\n");

	}	//end if

	templateBuilder.Append("				    </a>\r\n");
	templateBuilder.Append("				</dd>\r\n");
	templateBuilder.Append("				<dt><a href=\"" + albumurl.ToString() + "showalbum.aspx?albumid=" + __albuminfo.Albumid.ToString().Trim() + "\" target=\"_blank\">" + __albuminfo.Title.ToString().Trim() + "</a> (" + __albuminfo.Imgcount.ToString().Trim() + ")</dt>\r\n");
	templateBuilder.Append("		    </dl>\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\">\r\n");
	templateBuilder.Append("		<div class=\"sidetitlebar\"><ul><li class=\"current\" id=\"li_album\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_album'));\">热门相册</a></li><li id=\"li_photo\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_photo'));\">热门相片</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"albumlist\" class=\"topicdot\">\r\n");
	 albumlist = albumagg.GetAlbumList(photoconfig.Focusalbumshowtype, 7, 180);
	

	if (albumlist.Count>0)
	{


	int hotalbuminfo__loop__id=0;
	foreach(AlbumInfo hotalbuminfo in albumlist)
	{
		hotalbuminfo__loop__id++;

	templateBuilder.Append("			<li><em>" + hotalbuminfo.Views.ToString().Trim() + "</em><a href=\"showalbum.aspx?albumid=" + hotalbuminfo.Albumid.ToString().Trim() + "\" target=\"_blank\">" + hotalbuminfo.Title.ToString().Trim() + "</a> (<a href=\"showalbumlist.aspx?uid=" + hotalbuminfo.Userid.ToString().Trim() + "\">" + hotalbuminfo.Username.ToString().Trim() + "</a>)</li>\r\n");

	}	//end loop


	}
	else
	{

	templateBuilder.Append("		    暂无数据!\r\n");

	}	//end if

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		<ul id=\"photolist\" class=\"topicdot\" style=\"display:none;\">\r\n");
	templateBuilder.Append("		  <!--一周热图总排行-->\r\n");
	 photolist = albumagg.GetWeekHotPhotoList(photoconfig.Weekhot);
	

	if (photolist.Count>0)
	{


	int __photolist__loop__id=0;
	foreach(PhotoInfo __photolist in photolist)
	{
		__photolist__loop__id++;

	templateBuilder.Append("			<li><em>" + __photolist.Views.ToString().Trim() + "</em><a href=\"showphoto.aspx?photoid=" + __photolist.Photoid.ToString().Trim() + "\" target=\"_blank\">" + __photolist.Title.ToString().Trim() + "</a> (<a href=\"showalbumlist.aspx?uid=" + __photolist.Userid.ToString().Trim() + "\">" + __photolist.Username.ToString().Trim() + "</a>)</li>\r\n");

	}	//end loop


	}
	else
	{

	templateBuilder.Append("		    暂无数据!\r\n");

	}	//end if

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	if (config.Enablespace==1)
	{

	templateBuilder.Append("<div class=\"mainbox\">\r\n");
	templateBuilder.Append("	<div class=\"box spacebox\">\r\n");
	templateBuilder.Append("		<span><a href=\"spaceindex.aspx\">更多&gt;&gt;</a></span>\r\n");
	templateBuilder.Append("		<h2>个人空间</h2>\r\n");

	int __spaceconfig__loop__id=0;
	foreach(SpaceConfigInfoExt __spaceconfig in spaceconfigs)
	{
		__spaceconfig__loop__id++;


	if (__spaceconfig__loop__id<=2)
	{

	templateBuilder.Append("		<dl>\r\n");
	templateBuilder.Append("			<dt><a href=\"" + spaceurl.ToString() + "space/?uid=" + __spaceconfig.Userid.ToString().Trim() + "\" target=\"_blank\"><img src=\"" + __spaceconfig.Spacepic.ToString().Trim() + "\" alt=\"blogphoto\" width=\"52\" height=\"56\" onerror=\"this.onerror=null;this.src='avatars/common/0.gif';\"/></a></dt>\r\n");
	templateBuilder.Append("			<dd class=\"spacetitle\"><a href=\"" + spaceurl.ToString() + "space/?uid=" + __spaceconfig.Userid.ToString().Trim() + "\">\r\n");
	templateBuilder.Append(Utils.GetSubString(__spaceconfig.Spacetitle,0,20,"..."));
	templateBuilder.Append("</a></dd>\r\n");
	templateBuilder.Append("			<dd><a href=\"" + spaceurl.ToString() + "space/viewspacepost.aspx?postid=" + __spaceconfig.Postid.ToString().Trim() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(__spaceconfig.Posttitle,0,20,"..."));
	templateBuilder.Append("</a></dd>\r\n");
	templateBuilder.Append("			<dd>日志: <em>" + __spaceconfig.Postcount.ToString().Trim() + "</em>   \r\n");

	if (config.Enablealbum==1)
	{

	templateBuilder.Append("" + config.Albumname.ToString().Trim() + ": <em>" + __spaceconfig.Albumcount.ToString().Trim() + "</em>\r\n");

	}	//end if

	templateBuilder.Append(" </dd>\r\n");
	templateBuilder.Append("		</dl>\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box journalbox\">\r\n");
	templateBuilder.Append("		<span><a href=\"bloglist.aspx\">更多&gt;&gt;</a></span>\r\n");
	templateBuilder.Append("		<h2>推荐日志</h2>\r\n");
	templateBuilder.Append("		<ul class=\"topiclist\">\r\n");

	int __spacepostinfo__loop__id=0;
	foreach(SpaceShortPostInfo __spacepostinfo in spacepostlist)
	{
		__spacepostinfo__loop__id++;

	templateBuilder.Append("		    <li><cite><a href=\"" + spaceurl.ToString() + "space/?uid=" + __spacepostinfo.Uid.ToString().Trim() + "\" target=\"_blank\">" + __spacepostinfo.Author.ToString().Trim() + "</a> ( <em title=\"回复数/浏览量\">" + __spacepostinfo.Commentcount.ToString().Trim() + " / " + __spacepostinfo.Views.ToString().Trim() + " )</em></cite><a href=\"" + spaceurl.ToString() + "space/viewspacepost.aspx?postid=" + __spacepostinfo.Postid.ToString().Trim() + "\">\r\n");
	templateBuilder.Append(Utils.GetSubString(__spacepostinfo.Title,0,56,"..."));
	templateBuilder.Append("</a> </li>\r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\">\r\n");
	templateBuilder.Append("		<div class=\"sidetitlebar\"><ul><li class=\"current\" id=\"li_spacecomment\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_spacecomment'));\">最新评论</a></li><li id=\"li_space\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_space'));\">最新更新空间</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"spacecommentlist\" class=\"topicdot\">\r\n");

	int comment__loop__id=0;
	foreach(DataRow comment in spaceagg.GetSpaceTopComments(7).Rows)
	{
		comment__loop__id++;

	templateBuilder.Append("		    <li><em>" + comment["author"].ToString().Trim() + "</em><a href=\"" + spaceurl.ToString() + "space/viewspacepost.aspx?postid=" + comment["postid"].ToString().Trim() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(comment["content"].ToString().Trim(),0,30,"..."));
	templateBuilder.Append("</a></li>\r\n");

	}	//end loop

	templateBuilder.Append("			<!--<li><em>fanzjgw</em><a href=\"#\">Discuz!NT程序发布</a></li>-->\r\n");
	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		<ul id=\"spacelist\" class=\"topicdot\" style=\"display:none;\">\r\n");

	int space__loop__id=0;
	foreach(DataRow space in spaceagg.GetRecentUpdateSpaceList(7).Rows)
	{
		space__loop__id++;

	templateBuilder.Append("		    <li><a href=\"" + spaceurl.ToString() + "space/?uid=" + space["userid"].ToString().Trim() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(space["spacetitle"].ToString().Trim(),0,30,"..."));
	templateBuilder.Append("</a></li>			\r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	if (config.Enablemall==1)
	{

	templateBuilder.Append("<div class=\"mainbox\">\r\n");
	templateBuilder.Append("	<div class=\"box mallbox\">\r\n");
	templateBuilder.Append("		<span><a href=\"#\">更多&gt;&gt;</a></span>\r\n");
	templateBuilder.Append("		<h2>最新上架产品</h2>\r\n");
	templateBuilder.Append("		<ul>\r\n");
	 goodscoll = goodsagg.GetGoodsList("", "newgoods",0,6);
	

	if (goodscoll.Count>0)
	{


	int goodsinfo__loop__id=0;
	foreach(Goodsinfo goodsinfo in goodscoll)
	{
		goodsinfo__loop__id++;

	templateBuilder.Append("			<li>\r\n");
	 aspxrewriteurl = this.ShowGoodsAspxRewrite(goodsinfo.Goodsid);
	

	if (goodsinfo.Goodspic=="")
	{

	templateBuilder.Append("				        <img height=\"80\" src=\"templates/" + templatepath.ToString() + "/images/mall/nogoods_small.gif\" onerror=\"this.onerror=null;this.src='" + goodsinfo.Goodspic.ToString().Trim() + "';\"  title=\"" + goodsinfo.Title.ToString().Trim() + "\">\r\n");

	}
	else
	{

	templateBuilder.Append("				        <img height=\"80\" src=\"upload/" + goodsinfo.Goodspic.ToString().Trim() + "\" onerror=\"this.onerror=null;this.src='" + goodsinfo.Goodspic.ToString().Trim() + "';\"  title=\"" + goodsinfo.Title.ToString().Trim() + "\">\r\n");

	}	//end if

	templateBuilder.Append("				<h4><a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + goodsinfo.Htmltitle.ToString().Trim() + "</a></h4>\r\n");
	templateBuilder.Append("				<p>市场价:<strike>" + goodsinfo.Price.ToString().Trim() + "</strike>元</p>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(goodsinfo.Selleruid);
	
	templateBuilder.Append("				<p class=\"price\">卖家:<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + goodsinfo.Seller.ToString().Trim() + "</a></p>\r\n");
	templateBuilder.Append("			</li>\r\n");

	}	//end loop


	}
	else
	{

	templateBuilder.Append("		    暂无数据!\r\n");

	}	//end if

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\">\r\n");
	templateBuilder.Append("		<div class=\"sidetitlebar\"><ul><li class=\"current\" id=\"li_hot_goods\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_hot_goods'));\">热门商品</a></li><li id=\"li_old_goods\"><a href=\"javascript:;\" onclick=\"javascript:tabselect($('li_old_goods'));\">二手商品</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"hot_goodslist\" class=\"topicdot\" style=\"display:;\">\r\n");
	 goodscoll = goodsagg.GetGoodsList("quality_new", "hotgoods",0,7);
	

	if (goodscoll.Count>0)
	{


	int hot_goods__loop__id=0;
	foreach(Goodsinfo hot_goods in goodscoll)
	{
		hot_goods__loop__id++;

	templateBuilder.Append("		    <li>\r\n");
	templateBuilder.Append("		        <em>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(hot_goods.Selleruid);
	
	templateBuilder.Append("<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + hot_goods.Seller.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("		        </em>\r\n");
	 aspxrewriteurl = this.ShowGoodsAspxRewrite(hot_goods.Goodsid);
	
	templateBuilder.Append("<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(hot_goods.Title,0,20,"..."));
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("		    </li>\r\n");

	}	//end loop


	}
	else
	{

	templateBuilder.Append("		    暂无数据!\r\n");

	}	//end if

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		<ul id=\"old_goodslist\" class=\"topicdot\" style=\"display:none;\">\r\n");
	 goodscoll = goodsagg.GetGoodsList("quality_old", "" ,0,7);
	

	if (goodscoll.Count>0)
	{


	int reco_goods__loop__id=0;
	foreach(Goodsinfo reco_goods in goodscoll)
	{
		reco_goods__loop__id++;

	templateBuilder.Append("		    <li>\r\n");
	templateBuilder.Append("		        <em>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(reco_goods.Selleruid);
	
	templateBuilder.Append("<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">" + reco_goods.Seller.ToString().Trim() + "</a>\r\n");
	templateBuilder.Append("		        </em>\r\n");
	 aspxrewriteurl = this.ShowGoodsAspxRewrite(reco_goods.Goodsid);
	
	templateBuilder.Append("<a href=\"" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(reco_goods.Title,0,20,"..."));
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("		    </li>\r\n");

	}	//end loop


	}
	else
	{

	templateBuilder.Append("		    暂无数据!\r\n");

	}	//end if

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if

	templateBuilder.Append("<!--<div class=\"adinner\"><img src=\"images/gather/ad.gif\" alt=\"广告\"/></div>-->\r\n");
	templateBuilder.Append("<div id=\"statistics\" class=\"mainbox\">\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\">\r\n");
	templateBuilder.Append("		<div class=\"titlebar\"><ul><li class=\"current\"><a href=\"#\">论坛点击量排行</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"topic1\">\r\n");
	 topiclist = forumagg.GetForumTopicList(10, 0, 0, TopicTimeType.All, TopicOrderType.Views, false, false);
	

	int views_topicinfo__loop__id=0;
	foreach(DataRow views_topicinfo in topiclist.Rows)
	{
		views_topicinfo__loop__id++;

	templateBuilder.Append("            <li>\r\n");
	templateBuilder.Append("                <em>" + views_topicinfo["views"].ToString().Trim() + "</em><cite \r\n");

	if (views_topicinfo__loop__id==1)
	{

	templateBuilder.Append("class=\"first\"\r\n");

	}	//end if


	if (views_topicinfo__loop__id==2)
	{

	templateBuilder.Append("class=\"second\"\r\n");

	}	//end if


	if (views_topicinfo__loop__id==3)
	{

	templateBuilder.Append("class=\"third\"\r\n");

	}	//end if

	templateBuilder.Append(">" + views_topicinfo__loop__id.ToString() + "</cite>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(views_topicinfo["fid"].ToString().Trim(),0);
	
	templateBuilder.Append("                <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(views_topicinfo["title"].ToString().Trim(),0,20,"..."));
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("           </li>            \r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\">\r\n");
	templateBuilder.Append("		<div class=\"titlebar\"><ul><li class=\"current\"><a href=\"#\">论坛精华排行</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"topic2\">\r\n");
	 topiclist = forumagg.GetForumTopicList(10, 0, 0, TopicTimeType.All, TopicOrderType.Replies, true, false);
	

	int digest_topicinfo__loop__id=0;
	foreach(DataRow digest_topicinfo in topiclist.Rows)
	{
		digest_topicinfo__loop__id++;

	templateBuilder.Append("           <li>\r\n");
	templateBuilder.Append("                <em>" + digest_topicinfo["views"].ToString().Trim() + "</em><cite \r\n");

	if (digest_topicinfo__loop__id==1)
	{

	templateBuilder.Append("class=\"first\"\r\n");

	}	//end if


	if (digest_topicinfo__loop__id==2)
	{

	templateBuilder.Append("class=\"second\"\r\n");

	}	//end if


	if (digest_topicinfo__loop__id==3)
	{

	templateBuilder.Append("class=\"third\"\r\n");

	}	//end if

	templateBuilder.Append(">" + digest_topicinfo__loop__id.ToString() + "</cite>\r\n");
	 aspxrewriteurl = this.ShowForumAspxRewrite(digest_topicinfo["fid"].ToString().Trim(),0);
	
	templateBuilder.Append("                <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(digest_topicinfo["title"].ToString().Trim(),0,20,"..."));
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("           </li>            \r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\">\r\n");
	templateBuilder.Append("		<div class=\"titlebar\"><ul><li class=\"current\"><a href=\"#\">用户积分排行</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"hottopic\">\r\n");
	 userlist = forumagg.GetUserList(10, "credits");
	

	int credits_user__loop__id=0;
	foreach(DataRow credits_user in userlist.Rows)
	{
		credits_user__loop__id++;

	templateBuilder.Append("		   <li>\r\n");
	templateBuilder.Append("                <em>" + credits_user["credits"].ToString().Trim() + "</em><cite \r\n");

	if (credits_user__loop__id==1)
	{

	templateBuilder.Append("class=\"first\"\r\n");

	}	//end if


	if (credits_user__loop__id==2)
	{

	templateBuilder.Append("class=\"second\"\r\n");

	}	//end if


	if (credits_user__loop__id==3)
	{

	templateBuilder.Append("class=\"third\"\r\n");

	}	//end if

	templateBuilder.Append(">" + credits_user__loop__id.ToString() + "</cite>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(credits_user["uid"].ToString().Trim());
	
	templateBuilder.Append("                <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(credits_user["username"].ToString().Trim(),0,20,"..."));
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("           </li> \r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"box sidebox\" style=\"float:right; margin-right:0;\">\r\n");
	templateBuilder.Append("		<div class=\"titlebar\"><ul><li class=\"current\"><a href=\"#\">在线时长排行</a></li></ul></div>\r\n");
	templateBuilder.Append("		<div class=\"sideinner\">\r\n");
	templateBuilder.Append("		<ul id=\"hottopic\">\r\n");
	 userlist = forumagg.GetUserList(10, "oltime");
	

	int oltime_user__loop__id=0;
	foreach(DataRow oltime_user in userlist.Rows)
	{
		oltime_user__loop__id++;

	templateBuilder.Append("		   <li>\r\n");
	templateBuilder.Append("                <em>" + oltime_user["oltime"].ToString().Trim() + "</em><cite \r\n");

	if (oltime_user__loop__id==1)
	{

	templateBuilder.Append("class=\"first\"\r\n");

	}	//end if


	if (oltime_user__loop__id==2)
	{

	templateBuilder.Append("class=\"second\"\r\n");

	}	//end if


	if (oltime_user__loop__id==3)
	{

	templateBuilder.Append("class=\"third\"\r\n");

	}	//end if

	templateBuilder.Append(">" + oltime_user__loop__id.ToString() + "</cite>\r\n");
	 aspxrewriteurl = this.UserInfoAspxRewrite(oltime_user["uid"].ToString().Trim());
	
	templateBuilder.Append("                <a href=\"" + forumurl.ToString() + "" + aspxrewriteurl.ToString() + "\" target=\"_blank\">\r\n");
	templateBuilder.Append(Utils.GetSubString(oltime_user["username"].ToString().Trim(),0,20,"..."));
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("           </li> \r\n");

	}	//end loop

	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	if (forumlinkcount>0)
	{

	templateBuilder.Append("<div class=\"mainbox\">\r\n");
	templateBuilder.Append("	<div class=\"box linksbox\">\r\n");
	templateBuilder.Append("		<h2>友情链接</h2>\r\n");
	templateBuilder.Append("		<ul>\r\n");

	int forumlink__loop__id=0;
	foreach(DataRow forumlink in forumlinklist.Rows)
	{
		forumlink__loop__id++;

	templateBuilder.Append("			<li>\r\n");

	if (forumlink["logo"].ToString().Trim()!="")
	{

	templateBuilder.Append("				<a href=\"" + forumlink["url"].ToString().Trim() + "\" target=\"_blank\">" + forumlink["name"].ToString().Trim() + "</a>" + forumlink["note"].ToString().Trim() + "\r\n");
	templateBuilder.Append("				<a href=\"" + forumlink["url"].ToString().Trim() + "\" target=\"_blank\"><img src=\"" + forumlink["logo"].ToString().Trim() + "\" alt=\"" + forumlink["name"].ToString().Trim() + "\"/></a>\r\n");

	}
	else if (forumlink["name"].ToString().Trim()!="$$otherlink$$")
	{

	templateBuilder.Append("				<a href=\"" + forumlink["url"].ToString().Trim() + "\" target=\"_blank\">" + forumlink["name"].ToString().Trim() + "</a>" + forumlink["note"].ToString().Trim() + "\r\n");

	}
	else
	{

	templateBuilder.Append("				" + forumlink["note"].ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("			</li>\r\n");

	}	//end loop

	templateBuilder.Append("		</ul>		\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

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


	templateBuilder.Append("</div>\r\n");

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

	templateBuilder.Append("				 , " + querycount.ToString() + " query.\r\n");

	}	//end if


	}	//end if

	templateBuilder.Append("		" + config.Icp.ToString().Trim() + "\r\n");
	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<div id=\"quicksearch_menu\" class=\"searchmenu\" style=\"display: none;\">\r\n");
	templateBuilder.Append("	<div onclick=\"document.getElementById('keywordtype').value='0';document.getElementById('quicksearch').innerHTML='帖子标题';document.getElementById('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">帖子标题</div>\r\n");
	templateBuilder.Append("	<div onclick=\"document.getElementById('keywordtype').value='2';document.getElementById('quicksearch').innerHTML='空间日志';document.getElementById('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">空间日志</div>\r\n");
	templateBuilder.Append("	<div onclick=\"document.getElementById('keywordtype').value='3';document.getElementById('quicksearch').innerHTML='相册标题';document.getElementById('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">相册标题</div>\r\n");
	templateBuilder.Append("	<div onclick=\"document.getElementById('keywordtype').value='8';document.getElementById('quicksearch').innerHTML='作&nbsp;&nbsp;者';document.getElementById('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">作&nbsp;&nbsp;者</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("<ul class=\"popupmenu_popup\" id=\"my_menu\" style=\"display: none\">\r\n");

	if (config.Enablespace==1)
	{

	templateBuilder.Append("	<li><a href=\"" + spaceurl.ToString() + "space/\">我的空间</a></li>\r\n");

	}	//end if


	if (config.Enablealbum==1)
	{

	templateBuilder.Append("	<li><a href=\"showalbumlist.aspx?uid=" + userid.ToString() + "\">我的相册</a></li>\r\n");

	}	//end if

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
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</body>\r\n");
	templateBuilder.Append("</html>\r\n");



	Response.Write(templateBuilder.ToString());
}
</script>
