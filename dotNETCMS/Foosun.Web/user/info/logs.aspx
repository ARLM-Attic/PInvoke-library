<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="user_manage_logs" Codebehind="logs.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >日历管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />日历管理</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td style="PADDING-LEFT: 14px;"><a class="topnavichar" href="logs.aspx">管理日历</a>　<a class="topnavichar" href="logsCreat.aspx">创建日历</a></td>
  </tr>
</table>

<asp:Repeater ID="DataList1" runat="server">
   <HeaderTemplate>
       <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
      <tr class="TR_BG">
        <td align="left" valign="middle" class="sys_topBg">日历名称</td>
        <td align="left" valign="middle" class="sys_topBg">描述</td>
        <td align="left" valign="middle" class="sys_topBg">日期</td>
        <td align="left" valign="middle" class="sys_topBg">提前提醒</td>
        <td align="left" valign="middle" class="sys_topBg">操作</td>
      </tr>   
    </HeaderTemplate>
      <ItemTemplate>
        <tr class="TR_BG_list">
        <td align="left" valign="middle"><%#((DataRowView)Container.DataItem)["Title"]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["Content"]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["LogDateTime"]%></td>
        <td align="left" valign="middle" width="60"><%#((DataRowView)Container.DataItem)["dateNum"]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["op"]%></td>
        </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
     </FooterTemplate>
</asp:Repeater>
   <div width="98%" align="right"> 
       <uc1:PageNavigator ID="PageNavigator1" runat="server"  /> </div>
<br />
 </form>
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
</table>

</body>
</html>
<script language="javascript" type="text/javascript">
function del(ID)
{
    if (confirm('你确认删除此记录吗?'))
    {
        self.location="Logs.aspx?Type=del&ID="+ID;
    }
}
function edit(ID)
{
    {
        self.location="LogsCreat.aspx?Type=edit&ID="+ID;
    }
}
</script>