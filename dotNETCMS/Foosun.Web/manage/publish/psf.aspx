<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_publish_psf" Codebehind="psf.aspx.cs" %>
<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">PSF接点管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />PSF接点管理</div></td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
    <tr>
      <td height="18" style="width: 45%" colspan="2"  style="PADDING-LEFT: 14px"><div align="left"> <a href="psf.aspx" class="topnavichar">管理首页</a>┊<a href="psf_add.aspx" class="topnavichar">新建接点</a>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color="#ff0000">事件:</font> ┊ <a href="psf.aspx?type=delall" onclick="{if(confirm('确认删除全部所有添加的信息吗？\n删除后将保存在回收站中')){return true;}return false;}" class="topnavichar">删除全部</a> ┊ <asp:LinkButton ID="DelP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所选信息吗?\n删除后将保存在回收站中')){return true;}return false;}" OnClick="DelP_Click">批量删除</asp:LinkButton></div>
      </td>
    </tr>
  </table>
  <div id="NoContent" runat="server"></div>
  <asp:Repeater ID="DataList1" runat="server">
    <HeaderTemplate>
      <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
      <tr class="TR_BG">
        <td width="7%" align="center" valign="middle" class="sys_topBg">编号</td>
        <td width="7%" align="center" valign="middle" class="sys_topBg">接点名称</td>
        <td width="12%" align="center" valign="middle" class="sys_topBg">创建日期</td>
        <td width="10%" align="center" valign="middle" class="sys_topBg">操作
          <input type="checkbox" id="psf_checkbox" value="-1" name="psf_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list">
        <td width="7%" align="center" valign="middle" height=20><%#((DataRowView)Container.DataItem)[1]%></td>
        <td width="7%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
        <td width="12%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[6]%></td>
        <td width="10%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["oPerate"]%> </td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      </table>
    </FooterTemplate>
  </asp:Repeater>
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator1" runat="server"/>
  </div>
    </form>
    <br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat=server /></td>
  </tr>
</table>
</body>
</html>
