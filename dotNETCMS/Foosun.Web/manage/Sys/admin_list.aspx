<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_admin_list" ResponseEncoding="utf-8" Codebehind="admin_list.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
<tr>
  <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">管理员管理</td>
  <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />管理员管理</div></td>
</tr>
</table>
 <table width="100%" border="0" cellpadding="3" cellspacing="1" class="Navitable" align="center">
  <tr>
    <td style="padding-left:15px;"><a href="admin_add.aspx" class="topnavichar">添加管理员</a>&nbsp;┊&nbsp;<span id="channelList" runat="server" /></td>
  </tr>
</table>
<asp:Repeater ID="DataList1" runat="server">
   <HeaderTemplate>
       <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
      <tr class="TR_BG">
        <td align="left" valign="middle" class="sys_topBg">用户名</td>
        <td align="left" valign="middle" class="sys_topBg">姓名</td>
        <td align="left" valign="middle" class="sys_topBg">电子邮件</td>
        <td align="left" valign="middle" class="sys_topBg">超级管理员</td>
        <td align="left" valign="middle" class="sys_topBg">状态</td>
        <td align="left" valign="middle" class="sys_topBg">操作</td>
      </tr>   
    </HeaderTemplate>
      <ItemTemplate>
        <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td align="left" valign="middle" height="20"><%#((DataRowView)Container.DataItem)["userNames"]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[3]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[7]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[9]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["Op"]%></td>
        </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
     </FooterTemplate>
</asp:Repeater>
<div style="width:98%;" align="right"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></div>
<br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
</table>
</form>
</body>
<script language="javascript" type="text/javascript">
function Lock(ID)
{
    self.location="?Type=Lock&ID="+ID;
}
function UnLock(ID)
{
    self.location="?Type=UnLock&ID="+ID;
}
function Del(ID)
{
    if (confirm('你确认删除此管理员吗?'))
    {
        self.location="?Type=Del&ID="+ID;
    }
}
function Update(ID)
{
    self.location="admin_edit.aspx?Type=Update&ID="+ID;
}
function getchanelInfo(obj)
{
   var SiteID=obj.value;
   window.location.href="admin_list.aspx?SiteID="+SiteID+"";
}

</script>
</html>
