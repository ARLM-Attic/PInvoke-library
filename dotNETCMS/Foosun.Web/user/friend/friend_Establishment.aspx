<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_friend_Establishment" Codebehind="friend_Establishment.aspx.cs" %>
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
  <table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >好友管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="friendList.aspx" class="menulist">好友管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />好友设置</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="friendList.aspx" class="menulist">好友管理</a>　<a href="friendmanage.aspx" class="menulist">好友分类</a>&nbsp;&nbsp; <a href="friend_add.aspx" class="menulist">添加好友</a>&nbsp;&nbsp; <a href="#" class="menulist">好友设置</a></span></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  
   <tr class="TR_BG_list">
    <td class="list_link" Width="20%">
        身份验证：</td>
      <td class="list_link" colspan="4" Width="70%"> 
          <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="90px" Width="296px">
              <asp:ListItem Value="2">允许任何人把我列为好友</asp:ListItem>
              <asp:ListItem Value="1">需要身份认证才能把我列为好友</asp:ListItem>
              <asp:ListItem Value="0">不允许任何人把我列为好友</asp:ListItem>
          </asp:RadioButtonList></td>
  </tr>    
  <tr class="TR_BG_list">
    <td class="list_link"></td>
   <td class="list_link">
       &nbsp; &nbsp; &nbsp;
       <asp:Button ID="addfriend" runat="server" Text="提  交"  OnClick="addfriend_Click"  CssClass="form"/></td>   
  </tr>
</table>
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table>
</form>
</body>
</html>
