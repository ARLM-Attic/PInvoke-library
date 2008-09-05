<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_DefineTable_Edit_Manage" Codebehind="DefineTable_Edit_Manage.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript" src="../../Editor/scripts/editor.js"></script>
</head>
<body>
<form id="form1" runat="server">
  <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >自定义自段管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />自定义字段</div></td>
    </tr>
  </table>
  <table width="100%" border="0" cellpadding="3" cellspacing="1" class="Navitable" align="center">
    <tr>
      <td style="padding-left:15px;"><a href="DefineTable_Manage.aspx" class="topnavichar">分类管理</a>┋<a href="DefineTable.aspx" class="topnavichar">新增字段</a>┋<a href="DefineTable_Manage.aspx?action=add" class="topnavichar">新增分类</a></td>
    </tr>
  </table>
  <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG_list">
      <td colspan="2">新增自定义字段分类信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td width="34%" height="27" align="right">上一级自定义字段编号：</td>
      <td width="66%">&nbsp;
        <asp:TextBox ID="PraText" runat="server" Enabled="false" CssClass="form"></asp:TextBox></td>
    </tr>
    <tr class="TR_BG_list">
      <td height="26" align="right">自定义字段名称：</td>
      <td>&nbsp;
        <asp:TextBox ID="NewText" runat="server" CssClass="form"></asp:TextBox></td>
    </tr>
    <tr class="TR_BG_list">
      <td height="26" colspan="2" align="center"><asp:Button ID="Button1" runat="server" Text="提交数据" CssClass="form" OnClick="Button1_Click" />
        &nbsp;</td>
    </tr>
  </table>
</form>
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td class="list_link" align="center"><%Response.Write(CopyRight); %></td>
  </tr>
</table>
</body>
</html>
