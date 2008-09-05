<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_Frame" Codebehind="Frame.aspx.cs" %>

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
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table" id="Frame">
  <tr class="TR_BG_list" runat="server" id="js">
      <td class="list_link" align="center">
          JS名称
          <asp:DropDownList ID="DropDownList1" runat="server" Width="270px">
          </asp:DropDownList></td>
    </tr>
  <tr class="TR_BG_list" runat="server" id="dspecial">
      <td class="list_link" align="center" valign="top">
          专题名称
          <select id="Special" name="Special" style="width:250px;height:150px"  runat="server" class="form" multiple></select>
          </td>
  </tr>
  <tr class="TR_BG_list">
      <td align="center" class="list_link">
      <asp:Button ID="Button1" runat="server" Text="确 定" OnClick="Button1_Click" CssClass="form"/>
      &nbsp; &nbsp;&nbsp;
      <asp:Button ID="Button2" runat="server" Text="取 消" OnClientClick="javascript:window.close();"  CssClass="form"/></td>
  </tr>
</table>
    </form>
</body>
</html>
