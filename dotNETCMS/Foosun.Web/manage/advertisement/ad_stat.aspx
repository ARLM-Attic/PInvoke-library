<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_advertisement_ad_stat" Codebehind="ad_stat.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>查看统计信息</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">查看统计信息</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="list.aspx" class="list_link">广告系统</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />查看统计信息</div></td>
        </tr>
      </table>
        <table width="100%" border="0" cellpadding="3" cellspacing="1" class="Navitable" align="center">
          <tr>
            <td style="padding-left:15px;width:50%;"> <a href="ad_stat.aspx?st=hour&adsID=<% =str_adsID %>" class="list_link">24小时统计</a>&nbsp;┊&nbsp;<a href="ad_stat.aspx?st=day&adsID=<% =str_adsID %>" class="list_link">日统计</a>&nbsp;┊&nbsp;<a href="ad_stat.aspx?st=week&adsID=<% =str_adsID %>" class="list_link">周统计</a>&nbsp;┊&nbsp;<a href="ad_stat.aspx?st=month&adsID=<% =str_adsID %>" class="list_link">月统计</a>&nbsp;┊&nbsp; <a href="ad_stat.aspx?st=year&adsID=<% =str_adsID %>" class="list_link">年统计</a>&nbsp;┊&nbsp;<a href="ad_stat.aspx?st=source&adsID=<% =str_adsID %>" class="list_link">来源统计</a> </td></tr>
        </table>
    <div id="DivStat" runat="server"></div>
    <br />
    <div id="DivCorpright" runat="server"></div>
    </form>
</body>
</html>
