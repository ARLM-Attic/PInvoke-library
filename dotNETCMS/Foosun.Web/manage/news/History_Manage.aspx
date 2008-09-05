<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_History_Manage" Codebehind="History_Manage.aspx.cs" %>
<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
  <!------头部导航------>
  <table width="100%" border="0" cellpadding="0" cellspacing="0"class="toptable">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">新闻归档管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" >位置：<a href="../main.aspx" class="navi_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />归档管理</td>
    </tr>
  </table>
  <!----功能菜单----->
  <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
    <tr class="menulist">
      <td height="18" style="padding-left:14px;width:45%" colspan="2"><div align="left"><a href="History_Manage.aspx" class="topnavichar">管理首页</a>&nbsp;┊&nbsp;
          <asp:LinkButton ID="SuoP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认锁定所选信息吗?')){return true;}return false;}" OnClick="Suo_ClickP">锁定</asp:LinkButton>
          &nbsp;┊&nbsp;
          <asp:LinkButton ID="UnsuoP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认解锁所选信息吗?')){return true;}return false;}" OnClick="Unsuo_ClickP">解锁</asp:LinkButton>
          &nbsp;┊&nbsp;
          <asp:LinkButton ID="index" runat="server" CssClass="topnavichar" OnClick="Index_ClickP">生成索引页</asp:LinkButton>
          &nbsp;┊&nbsp;
          <asp:LinkButton ID="DelP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所选信息吗?')){return true;}return false;}" OnClick="Del_ClickP">批量删除</asp:LinkButton>
          &nbsp;┊&nbsp;
          <asp:LinkButton ID="delall" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所有信息吗?')){return true;}return false;}" OnClick="DelAll_ClickP">删除全部</asp:LinkButton>
        </div></td>
    </tr>
  </table>
  <div id="NoContent" runat="server"></div>
  <!------显示归档管理的管理页------>
  <asp:Repeater ID="DataList1" runat="server">
    <HeaderTemplate>
      <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
      <tr class="TR_BG">
        <td width="40%" valign="middle" class="sys_topBg">新闻标题</td>
        <td align="center" valign="middle" class="sys_topBg">类型</td>
        <td align="center" valign="middle" class="sys_topBg">所属表</td>
        <td align="center" valign="middle" class="sys_topBg">状态</td>
        <td align="center" valign="middle" class="sys_topBg">归档时间</td>
        <td width="9%" align="center" valign="middle" class="sys_topBg">操作
          <input type="checkbox" id="history_checkbox1" value="-1" name="history_checkbox1" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td width="40%" valign="middle" ><%#((DataRowView)Container.DataItem)[1]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["Type"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["table"] %>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["stat"] %>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[5] %>
        <td width="9%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["oPerate"]%> </td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      </table>
    </FooterTemplate>
  </asp:Repeater>
  <!--------分页--------->
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
  </div>
</form>
<br />
<!-------CopyRight------->
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat=server /></td>
  </tr>
</table>
</body>
</html>
