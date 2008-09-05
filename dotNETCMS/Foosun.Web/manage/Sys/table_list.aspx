<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_Sys_table_list" Codebehind="table_list.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >管理新闻表</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />新闻表</div></td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
        <td style="padding-left:14px;">功能：<a class="topnavichar" href="table_manage.aspx">新闻表管理</a>　<a class="topnavichar" href="table_list.aspx">复制新闻表</a></td>
        </tr>
</table>
      <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
        <form id="form1" runat="server"><tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">创建的新表名</div></td>
          <td class="list_link"><asp:TextBox ID="tableName" runat="server"  Width="250"  MaxLength="20"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_table_list_0001',this)">帮助</span><asp:RequiredFieldValidator ID="f_tableName" runat="server" ControlToValidate="tableName" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写快捷菜单名称，长度为20</span>"></asp:RequiredFieldValidator></td>
        </tr>                                                                                                                                                                                                                                                                                             
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 28px;">&nbsp;</td>
          <td class="list_link" style="height: 28px">&nbsp;<asp:Button ID="sumbitsave" runat="server" CssClass="form" Text=" 复制新闻表 "  OnClientClick="{if(confirm('确认进行复制表吗?\n一旦创建，将不能修改和删除！')){return true;}return false;}" OnClick="shortCutsubmit" />
        </tr>

</form>
</table>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat=server /></td>
   </tr>
 </table>

</body>
</html>