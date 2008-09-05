<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="manage_Contribution_Constr_Pay" Codebehind="Constr_Pay.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
</head>
<body style="background-color: #ffffff">
<form id="form1" name="form1" method="post" action="" runat="server">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >
              投稿管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Constr_List.aspx" class="topnavichar">稿件管理</a></div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td width="46%" class="navi_link">&nbsp; &nbsp; &nbsp;<a href="Constr_List.aspx" class="topnavichar">稿件管理</a>&nbsp; &nbsp;<a href="Constr_Stat.aspx" class="topnavichar">稿件统计</a>&nbsp; &nbsp;<a href="paymentannals.aspx" class="topnavichar">支付历史</a>&nbsp; &nbsp;<a href="Constr_SetParam.aspx" class="topnavichar">稿酬设定</a>&nbsp; &nbsp;<a href="Constr_chicklist.aspx" class="topnavichar">所有通过审核稿件</a></td>
  </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  <tr class="TR_BG_list">
      <td class="list_link" colspan="4">
      <span>您选择的要支付稿酬的用户名：
          <asp:Label ID="LblName" runat="server" Width="127px" ForeColor="Red"></asp:Label>
      </span>
     <span id="no" runat="server" width="140px">
     </span>
    <span>需要支付的稿酬为：<label id="money_1" runat="server" />&nbsp;&nbsp;&nbsp;
          <asp:Button ID="Button1" runat="server" Text="设置支付状态"  CssClass="form" OnClientClick="{if(confirm('确定要支付吗？')){return true;}return false;}" OnClick="Button1_Click1"/>
      </span> 
      </td>
    </tr>
    <tr class="TR_BG">
      <td class="list_link" colspan="2">
          &nbsp;
          <asp:Label ID="LblName1" runat="server" ForeColor="Red" Width="80px"></asp:Label>的银行账号信息</td>
      <td class="list_link" colspan="2">
          汇款地址</td>
   </tr>
      <tr class="TR_BG_list">
    <td class="list_link" Width="10%">
        帐户名：</td>
    <td class="list_link" Width="40%">
        <asp:Label ID="bankRealName" runat="server" Text="Label" Width="100%"></asp:Label></td>
        <td class="list_link" Width="10%">
            地址：</td>
    <td class="list_link" Width="40%">
        <asp:Label ID="address" runat="server" Text="Label" Width="100%"></asp:Label></td>
   </tr>
   <tr class="TR_BG_list">
    <td class="list_link">
        卡号：</td>
    <td>
        <asp:Label ID="bankcard" runat="server" Text="Label" Width="100%"></asp:Label></td>
        <td class="list_link">
            邮编：</td>
    <td class="list_link">
        <asp:Label ID="postcode" runat="server" Text="Label" Width="100%"></asp:Label></td>
</tr>
   <tr class="TR_BG_list">
    <td class="list_link">
        开户银行：</td>
    <td>
        <asp:Label ID="bankName" runat="server" Text="Label" Width="100%"></asp:Label></td>
        <td class="list_link">
            姓名：</td>
    <td class="list_link">
        <asp:Label ID="RealName" runat="server" Text="Label" Width="100%"></asp:Label></td>
</tr>
</table>
<br />
<br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
</html>






