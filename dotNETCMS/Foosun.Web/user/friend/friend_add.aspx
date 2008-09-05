<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_friend_add" Codebehind="friend_add.aspx.cs" %>
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
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="friendList.aspx" class="menulist">好友管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加好友</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="friendList.aspx" class="menulist">好友管理</a>　<a href="friendmanage.aspx" class="menulist">好友分类</a>&nbsp;&nbsp; <a href="friend_add.aspx" class="menulist">添加好友</a>&nbsp;&nbsp; <a href="friend_Establishment.aspx" class="menulist">好友设置</a></span></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  
   <tr class="TR_BG_list">
    <td class="list_link" Width="30%">用户名</td>
      <td class="list_link" colspan="4" Width="70%">
          <asp:TextBox ID="usernameBox" runat="server" Width="198px" CssClass="form"></asp:TextBox>&nbsp;&nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('u_friend_add_0001',this)">帮助</span> &nbsp; &nbsp;
          &nbsp;&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="用户名不能为空" ControlToValidate="usernameBox"></asp:RequiredFieldValidator></td>
  </tr>

  <tr class="TR_BG_list">
    <td class="list_link">好友分类</td>
      <td class="list_link" colspan="4">
          <asp:DropDownList ID="friendmanageList" runat="server" Width="205px">
          </asp:DropDownList>&nbsp;&nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('u_friend_add_0001',this)">帮助</span></td>
  </tr>
       <tr class="TR_BG_list">
            <td class="list_link">
        请求信息</td>
            <td class="list_link">
            <asp:TextBox ID="AddfriendContent" runat="server" Enabled="true" Height="89px" TextMode="MultiLine" Width="284px" CssClass="form"></asp:TextBox>&nbsp;&nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('u_friend_add_0001',this)">帮助</span> <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddfriendContent" runat="server" ErrorMessage="请求信息不能为空" ControlToValidate="AddfriendContent"></asp:RequiredFieldValidator></td>
  </tr>
     
  <tr class="TR_BG_list">
    <td class="list_link"></td>
   <td class="list_link"><asp:Button ID="addfriend" runat="server" Text="提  交"  OnClick="addfriend_Click"  CssClass="form"/>&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp; &nbsp;
       <input type="reset" name="Submit3" value="重  置" class="form"></td>   
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
