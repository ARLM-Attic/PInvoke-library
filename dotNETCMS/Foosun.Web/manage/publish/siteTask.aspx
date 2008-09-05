<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_publish_siteTask" Codebehind="siteTask.aspx.cs" %>
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
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">ƻ</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">λõ<a href="../main.aspx" target="sys_main" class="list_link">ҳ</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />ƻ</div></td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
    <tr>
      <td height="18" style="width: 45%" colspan="2" style="PADDING-LEFT: 14px"><div align="left"><a href="siteTask.aspx" class="topnavichar">ҳ</a>  <a href="siteTask_add.aspx?type=base" class="topnavichar">½</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color="red">¼:</font>&nbsp; <asp:LinkButton ID="Delall" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('ȷɾȫϢ?')){return true;}return false;}" OnClick="Delall_Click">ɾȫ</asp:LinkButton>  <asp:LinkButton ID="DelP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('ȷɾѡϢ?')){return true;}return false;}" OnClick="DelP_Click">ɾ</asp:LinkButton></div>
      </td>
    </tr>
  </table>
  <div id="NoContent" runat="server"></div>
  <asp:Repeater ID="DataList1" runat="server">
    <HeaderTemplate>
      <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
      <tr class="TR_BG">
        <td style="width:100px;" align="center" valign="middle" class="sys_topBg"></td>
        <td align="center" class="sys_topBg"></td>
        <td align="center" valign="middle" class="sys_topBg"></td>
        <td style="width:120px;" align="center" valign="middle" class="sys_topBg">
          <input type="checkbox" id="task_checkbox1" value="-1" name="task_checkbox1" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td style="width:100px;" align="center" valign="middle" height=20><%#((DataRowView)Container.DataItem)[1]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[7]%></td>
        <td style="width:120px;" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["oPerate"]%> </td>
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
