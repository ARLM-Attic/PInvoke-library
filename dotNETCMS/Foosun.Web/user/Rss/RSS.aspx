<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_RSS" Codebehind="RSS.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" name="form1" method="post" action="" runat="server">
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
         <td><span class="topnavichar" style="PADDING-LEFT: 5px"><a href="RSS.aspx" class="menulist">订阅须知</a>&nbsp;┊&nbsp;<a href="RssFeed.aspx" class="menulist">RSS订阅</a></span></td>
      </tr>
    </table>
      <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
        <tr class="TR_BG_list">
          <td> <strong>·什么是RSS<font color="#3366CC"> </font></strong><br/> <br/>
            　RSS是站点用来和其他站点之间共享内容的一种简易方式，通常被用于新闻和其他按顺序排列的网站，例如Blog。一段项目的介绍可能包含新闻的全部介绍等。或者仅仅是额外的内容或者简短的介绍。这些项目的链接通常都能链接到全部的内容。网络用户可以在客户端借助于支持RSS的新闻聚合工具软件，在不打开网站内容页面的情况下阅读支持RSS输出的网站内容。<br/> 
            <br/>
            <strong>·RSS如何工作</strong><br/> <br/>
            　您一般需要下载和安装一个RSS新闻阅读器，然后从网站提供的聚合新闻目录列表中订阅您感兴趣的新闻栏目的内容。订阅后，您将会及时获得所订阅新闻频道的最新内容。<br/>
            <br/> <strong>·RSS新闻阅读器的特点</strong><br/> <br/>
            　a. 没有广告或者图片来影响标题或者文章概要的阅读。
<p>　b. RSS阅读器自动更新你定制的网站内容，保持新闻的及时性。</p>
            <p>　c. 用户可以加入多个定制的RSS提要，从多个来源搜集新闻整合到单个数据流中。<br/>
              <br/>
              <strong>·RSS阅读器下载</strong><br/>
            </p>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr> 
                <td width="24%"><div align="center"> <a href="http://fox.foxmail.com.cn/" target="_blank"><img src="../../sysImages/user/Rss/foxmail.gif" width="134" height="51" border="0"/></a> 
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td><div align="center"><a href="http://fox.foxmail.com.cn/">腾讯Foxmail 
                            6</a></div></td>
                      </tr>
                    </table>
                    
                  </div></td>
                <td width="19%"><div align="center"><a href="http://www.potu.com" target="_blank"><img src="../../sysImages/user/Rss/zbt.gif" border="0"/></a><br>
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                      <tr> 
                        <td><div align="center"><a href="http://www.potu.com" target="_blank">周博通RSS阅读器</a></div></td>
                      </tr>
                    </table>
                    
                  </div></td>
                <td width="57%" valign="bottom"><div align="center"><a href="http://www.sharpreader.net/SharpReader0960_Setup.exe" target="_blank"><img src="../../sysImages/user/Rss/sharp%20Reader.gif" width="91" height="18" border="0"></a>&nbsp;<a href="http://www.rssreader.com/download/rssreader.zip" target="_blank"><img src="../../sysImages/user/Rss/RssReader.gif" width="91" height="19" border="0"></a>&nbsp;<a href="http://feeddemon.com/download/dloadhandler.asp?file=feeddemon-trial.exe" target="_blank"><img src="../../sysImages/user/Rss/FeedDemon.gif" width="91" height="20" border="0"></a>&nbsp;<a href="http://www.newzcrawler.com/downloads.shtml" target="_blank"><img src="../../sysImages/user/Rss/Nc.gif" width="91" height="19" border="0"></a> 
                  </div>
                  <table width="98%" border="0" cellspacing="0" cellpadding="0">
                    <tr> 
                      <td><div align="center">国外RSS聚合阅读器</div></td>
                    </tr>
                  </table>
                  
                </td>
              </tr>
            </table>
            
          </td>
        </tr>
</table>
</form>
<br />    
<br />    
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</body>
</html>