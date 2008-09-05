<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_getPoint" Codebehind="getPoint.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body><form id="form1" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
     <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >冲值管理</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />冲值管理</div></td>
        </tr>
        </table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;"><a class="topnavichar" href="getPoint.aspx">点卡冲值</a>&nbsp;┊&nbsp;<a class="topnavichar" href="onlinePoint.aspx">在线银行冲值</a>&nbsp;┊&nbsp;<a href="buyCard.aspx" class="list_link">购买点卡</a>&nbsp;┊&nbsp;<a href="history.aspx"  class="topnavichar">交易明晰</a></td>
        </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Width="100%">
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
  <tr class="TR_BG_list">
    <td class="list_link" width="30%" style="text-align: right">点卡</td>
    <td class="list_link" width="70%">
        <asp:TextBox ID="CardNumber" runat="server" Width="188px" CssClass="form"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CardNumber"
            ErrorMessage="请输入点卡卡号"></asp:RequiredFieldValidator></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">密码</td>
    <td class="list_link">
        <asp:TextBox ID="CardPassWord" runat="server" Width="188px" TextMode="Password" CssClass="form"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CardPassWord"
            ErrorMessage="请输入点卡密码"></asp:RequiredFieldValidator></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link"></td>
    <td class="list_link">
        &nbsp;<asp:Button ID="insert" runat="server" Text="充  值"  OnClick="insert_Click" CssClass="form"/>
        &nbsp;&nbsp; &nbsp;<input type="reset" name="Submit3" value="重  置" class="form">
        &nbsp;&nbsp; &nbsp;<a href="buyCard.aspx" class="list_link"><strong>购买点卡</strong></a>
        </td>
  </tr>
  </table>    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server"  Width="100%" Visible="False">
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
  <tr class="TR_BG_list">
    <td class="list_link" width="30%" style="text-align: right">
        点卡金额</td>
            <td class="list_link" width="70%">
                <asp:Label ID="Money" runat="server" Width="194px"></asp:Label>
                <asp:Label ID="cz" runat="server" Visible="False" Width="12px"></asp:Label></td>
        </tr>
        <tr class="TR_BG_list">
            <td class="list_link" style="text-align: right">
                点卡点数</td>
            <td class="list_link">
                <asp:Label ID="Pion" runat="server" Width="194px"></asp:Label></td>
        </tr>
        <tr class="TR_BG_list">
            <td class="list_link" style="text-align: right">
            </td>
            <td class="list_link">
                <asp:Button ID="Button1" runat="server" Text="确定充值" Width="94px" OnClick="Button1_Click" CssClass="form"/></td>
        </tr>
</table> 
    </asp:Panel>
<br />
<br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table>
</form>
</body>
</html>
