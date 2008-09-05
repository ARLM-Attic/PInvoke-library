<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_discuss_discussubsclass" Codebehind="discusssubsclass.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >讨论组管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="discussManage_list.aspx" class="menulist">讨论组管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />讨论组分类</div></td>
    </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
         <td style="PADDING-LEFT: 14px"><a href="discusssubsclass.aspx" class="menulist">讨论组分类</a> &nbsp;&nbsp; <a href="discusssubclass_add.aspx" class="menulist">添加讨论组分类</a></span></td>
      </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
      <tr>
         <td style="PADDING-LEFT: 14px">系统分类：<label id="sysClass" runat="server" /></td>
      </tr>
      <tr class="TR_BG_list">
         <td style="PADDING-LEFT: 14px"><label id="classLists" runat="server" /></td>
      </tr>
    </table>
    </form>
<br />
<br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table>
</body>
</html>
