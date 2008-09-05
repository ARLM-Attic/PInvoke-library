<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_channel_list" Codebehind="list.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
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
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
              <td height="1" colspan="2"></td>
            </tr>
            <tr>
              <td width="57%" class="sysmain_navi" style="padding-left:14px;">频道管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="list.aspx" class="list_link" target="sys_main">频道列表</a></td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><a href="list.aspx" class="topnavichar">频道列表</a>&nbsp;┋&nbsp;<a href="channel_add.aspx" class="topnavichar" >添加频道</a></td>
      </tr>
      </table>
    <asp:Repeater ID="Channlist" runat="server" OnItemCommand="DataList1_ItemCommand">
       <HeaderTemplate>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
          <tr class="TR_BG">
            <td class="sys_topBg" style="width:20px;">ID</td>
            <td class="sys_topBg" style="width:180px;">频道名称(识别ID)</td>
            <td align="center" class="sys_topBg" style="width:60px;">项目名称</td>
            <td align="center" class="sys_topBg" style="width:150px;">域名</td>
            <td align="center" class="sys_topBg" style="width:40px;">性质</td>
            <td align="center" class="sys_topBg">操作</td>
          </tr>
            </HeaderTemplate>
              <ItemTemplate>
          <tr class="TR_BG_list">
            <td style="text-align:center;"><%#((DataRowView)Container.DataItem)["Id"]%></td>
            <td><%#((DataRowView)Container.DataItem)["channelName"]%>(<%#((DataRowView)Container.DataItem)["channelEItem"]%>)</td>
            <td align="center"><%#((DataRowView)Container.DataItem)["channelItem"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["binddomain"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["systf"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["op"]%>&nbsp;</td>
          </tr>
          </ItemTemplate>
          <FooterTemplate>
          </table>
         </FooterTemplate>
    </asp:Repeater> 
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
       <tr>
         <td align="left">
             <uc1:PageNavigator ID="PageNavigator1" runat="server" /></td>
       </tr>
    </table>
<br />
<br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><label id="copyright" runat="server" /></td>
       </tr>
    </table>
    </form>
</body>
</html>
