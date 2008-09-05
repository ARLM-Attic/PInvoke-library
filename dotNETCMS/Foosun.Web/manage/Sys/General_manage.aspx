<%@ Page Language="C#" AutoEventWireup="true" Inherits="General_manage" Codebehind="General_manage.aspx.cs" %>
<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
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
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">新闻常规管理</td>
      <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />常规管理</div></td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
    <tr>
      <td height="18" style="width: 45%" colspan="2"><div align="left"> <a href="General_manage.aspx" class="topnavichar">管理首页</a> ┊ <a href="General_Add_Manage.aspx" class="topnavichar">添加</a> ┊ <a href="General_manage.aspx?key=0" class="topnavichar"> 关键字(TAG)</a> ┊ <a href="General_manage.aspx?key=2" class="topnavichar">作者</a> ┊ <a href="General_manage.aspx?key=3" class="topnavichar">内部链接</a> ┊ <a href="General_manage.aspx?key=1" class="menulist">来源</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label style="color:Red";>功能事件:</label>&nbsp;<a href="General_manage.aspx?type=delall" onclick="{if(confirm('确认删除全部所有添加的信息吗？')){return true;}return false;}" class="topnavichar">删除全部</a> ┊
          <asp:LinkButton ID="DelP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所选信息吗?')){return true;}return false;}" OnClick="Del_ClickP">批量删除</asp:LinkButton>
          ┊
          <asp:LinkButton ID="SuoP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认锁定所选信息吗?')){return true;}return false;}" OnClick="Suo_ClickP">批量锁定</asp:LinkButton>
          ┊
          <asp:LinkButton ID="UnsuoP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认解锁所选信息吗?')){return true;}return false;}" OnClick="Unsuo_ClickP">批量解锁</asp:LinkButton>
        </div></td>
    </tr>
  </table>
  <div id="NoContent" runat="server"></div>
  <asp:Repeater ID="DataList1" runat="server">
    <HeaderTemplate>
      <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
      <tr class="TR_BG">
        <td width="10%" align="center" valign="middle" class="sys_topBg">名称</td>
        <td width="15%" align="center" valign="middle" class="sys_topBg">类型</td>
        <td width="10%" align="center" valign="middle" class="sys_topBg">状态</td>
        <td width="12%" align="center" valign="middle" class="sys_topBg">连接地址</td>
        <td width="25%" align="center" valign="middle" class="sys_topBg">电子邮件</td>
        <td align="center" valign="middle" class="sys_topBg">操作
          <input type="checkbox" id="general_checkbox" value="-1" name="general_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td width="10%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[1]%></td>
        <td width="9%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["Type"]%></td>
        <td width="10%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["stat"]%></td>
        <td width="12%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[3] %>
        <td width="25%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[4] %>
        <td width="27%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["oPerate"]%> </td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      </table>
    </FooterTemplate>
  </asp:Repeater>
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
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
