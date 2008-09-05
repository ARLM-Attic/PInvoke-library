<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_File_Win_Login" Codebehind="File_Win_Login.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<head>
    <title></title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">

        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">文件管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />文件管理</div></td>
        </tr>
</table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="5" align="center">
      <tr>
        <td class="reshow" style="height: 29px"> 注意：如果您不是具有该文件浏览权限的管理员，将不能进入文件管理.默认密码：foosun.net</td>
      </tr>
    </table>
    <form id="form2" runat="server">
    <div>
      <table width="100%" border="0" cellspacing="1" cellpadding="5" align="center">
          <tr>
            <td width="20%" align="right" style="height: 41px"><font size="2px">请输入文件管理密码:</font></td>
            <td width="80%" style="height: 41px"><asp:TextBox ID="File_Manag_Pass" runat="server" Height="20px" TextMode="Password" Width="190px"></asp:TextBox><script>document.getElementById('File_Manag_Pass').focus();</script>
            <input type="submit" runat="server" id="FilePassClick" name="FilePassClick" class="form" value="确定" onserverclick="FilePassClick_ServerClick" style="width: 59px; height: 26px"/></td>
          </tr>
        </table>
    </div>
    </form>
    
    
 <table width="100%" border="0" cellpadding="8" cellspacing="0" align="center" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
</table>
</body>
</html>
