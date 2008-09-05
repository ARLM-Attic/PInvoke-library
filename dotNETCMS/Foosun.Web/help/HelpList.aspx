<%@ Page Language="C#" AutoEventWireup="true" Inherits="help_HelpList"  ResponseEncoding="utf-8" Codebehind="HelpList.aspx.cs" %>
<%@ Register Src="../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>帮助管理</title>
    <link href='../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css' rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="HelpList" runat="server">
     <table width="98%" align="center" height="32" border="0" cellpadding="0" cellspacing="0" background="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/reght_1_bg_1.gif">
      <tr>
        <td Height="1" colspan="2"></td>
      </tr>
      <tr>
        <td Width="46%" class="navi_link">位置：<a href="#" class="navi_link">首页</a> <img alt="" src="../sysImages/folder/navidot.gif" border="0" /> 帮助管理</td>
      </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="HelpAdd.aspx" class="menulist">添加帮助</a> | <a href="HelpList.aspx" class="menulist">帮助管理</a>&nbsp;&nbsp;&nbsp;搜索编号:<asp:TextBox
             ID="HelpID" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server"
                 Text="搜索" onclick="Button8_ServerClick" /></span></td>
  </tr>
</table>
<asp:Repeater ID="DataList1" runat="server">
   <HeaderTemplate>
       <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
      <tr class="TR_BG_list">
        <td width="7%" align="center" valign="middle" class="sysmain_navi">ID</td>
        <td width="15%" align="left" valign="middle" class="sysmain_navi">帮助编号</td>
        <td width="29%" align="left" valign="middle" class="sysmain_navi">帮助标题</td>
        <td width="21%" align="center" valign="middle" class="sysmain_navi">操作</td>
      </tr>   
    </HeaderTemplate>
      <ItemTemplate>
        <tr bgcolor="#FFFFFF">
        <td width="7%" align="center" valign="middle" height=20><%#((DataRowView)Container.DataItem)[0]%></td>
        <td width="15%" align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[1]%></td>
        <td width="29%" align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
        <td width="21%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[3]%></td>
        </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
      </FooterTemplate>
</asp:Repeater>
        <uc1:PageNavigator ID="PageNavigator1" runat="server" />
    </form>
</body>
</html>