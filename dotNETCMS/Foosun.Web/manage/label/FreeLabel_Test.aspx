<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_FreeLabel_Test" Codebehind="FreeLabel_Test.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="Form1" runat="server">
<div align="center">
<table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" align="left">自由标签SQL语句测试</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />自由标签SQL语句测试</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
  <tr>
    <td align="left">功能：<a class="topnavichar" href="javascript:window.close();">关闭</a></td>
  </tr>
</table>
<asp:Label ID="LblError" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
    <asp:GridView ID="GrvData" runat="server" Width="98%">
    </asp:GridView>
    <br />
    <br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"> <%Response.Write(CopyRight);%></td>
  </tr>
</table>
</div>
</form>
</body>
</html>
