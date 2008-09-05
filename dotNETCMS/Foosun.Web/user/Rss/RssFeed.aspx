<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_Rss_RssFeed" Codebehind="RssFeed.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
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
<div id="no" runat="server"></div>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG_list">
            <td align="left" style="width:20%;" colspan="2"><label id="Newsxml" runat="server" /></td>
        </tr>
    </table>
     <asp:Repeater ID="DataList1" runat="server" >
    <HeaderTemplate>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
    <tr class="TR_BG">
    <td class="sys_topBg" align="left" style="width:20%;">栏目名称</td>
    </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="TR_BG_list">
        <td align="left" valign="top">
        <%#((DataRowView)Container.DataItem)["pic"]%>&nbsp;&nbsp;<span class="sysmain_navi"><%#((DataRowView)Container.DataItem)["ClassCNames"]%></span>&nbsp;&nbsp;<a href="<%#((DataRowView)Container.DataItem)["url"]%>" target="_blank" class="list_link"><%#((DataRowView)Container.DataItem)["url"]%></a>
        <br />
        <div>
            <ul>
                <%#((DataRowView)Container.DataItem)["xmllist"]%>
            </ul>
         </div>
       </td>          
        </tr>

     </ItemTemplate>
     <FooterTemplate>
     </table>
     </FooterTemplate>
    </asp:Repeater>
<table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
<tr><td align="left">
        <span style="color: #ff0000">提醒：IE5.0以上用户点击图片复制RSS聚合地址；FireFox用户请直接复制以上地址。</span></td><td align="right">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
</td></tr>
</table> 
<br />    
<br />    
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
<script type="text/javascript" language="JavaScript">   
function copyToClipBoard(url)
{ 
    
	var ie4=document.all&&navigator.userAgent.indexOf("Opera")==-1
	var ns6=document.getElementById&&!document.all
    if (ie4)
    {
        var clipBoardContent=url;
        window.clipboardData.setData("Text",clipBoardContent);
        alert("地址复制成功!\n地址："+url+"");
    }
    else
    {
        alert("FireFox用户请直接复制地址!");
    }
}
</script>
</html>
