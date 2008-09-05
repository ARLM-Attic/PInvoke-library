<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_windlogin" Codebehind="windlogin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<table width="100%" height="32" align="center" border="0" cellpadding="0" cellspacing="0" background="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/admin/reght_1_bg_1.gif">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="17%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >
              配置文件管理</td>
          <td height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ></td>
        </tr>
</table>
    <br />
    <table width="98%" border="0" cellspacing="0" cellpadding="5" align="center">
      <tr>
        <td class="reshow" style="height: 29px"> 注意：配置文件密码默认为<font color="blue">foosun.net</font>,管理员请进入配置文件,修改密码。</td>
      </tr>
    </table>
    <form id="form1" runat="server">
    <div>
      <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
          <tr>
            <td width="20%" align="right"><font size="2px">请输入配置文件管理密码:</font></td>
            <td width="80%"><asp:TextBox ID="TextBox1" runat="server" Height="20px" TextMode="Password" Width="190px"></asp:TextBox><script>document.getElementById('TextBox1').focus();</script>
            <asp:Button ID="Button1" runat="server" Height="27px" Text="确定" Width="64px" OnClick="Button1_Click" CssClass="form" /></td>
          </tr>
      </table>
    </div>
    </form>
    
    
 <table width="100%" border="0" cellpadding="8" cellspacing="0" align="center" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat=server /><%Response.Write(CopyRight); %></td>
   </tr>
</table>
</body>
</html>
