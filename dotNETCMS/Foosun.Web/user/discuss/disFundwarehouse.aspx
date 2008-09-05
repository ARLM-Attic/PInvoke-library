<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_disFundwarehouse" EnableEventValidation="true" Codebehind="disFundwarehouse.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" name="form1" method="post" action="" runat="server">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >讨论组管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="discussManage_list.aspx" class="menulist">讨论组管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />讨论组列表</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="discussManage_list.aspx" class="menulist">讨论组列表</a>　<a href="discussManagejoin_list.aspx" class="menulist">我加入的讨论组</a>&nbsp;&nbsp; <a href="discussManageestablish_list.aspx" class="menulist">我建立的讨论组</a>&nbsp;&nbsp; <a href="add_discussManage.aspx" class="menulist">添加讨论组</a></span></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  
  <tr class="TR_BG_list">
    <td class="list_link" width="30%" style="text-align: right">
        捐献金币数：</td>
    <td class="list_link" width="70%">
        <asp:TextBox ID="FHTextBox1" runat="server" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_disFundwarehouse_0001',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FHTextBox1"
            ErrorMessage="请输入捐献金币数" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FHTextBox1"
            Display="Dynamic" ErrorMessage="您输入的格式不对" ValidationExpression="^[1-9]\d*|0$"></asp:RegularExpressionValidator></td>
  </tr>
  <tr class="TR_BG_list">
    <td  class="list_link" style="text-align: right">
        捐献积分数：</td>
    <td  class="list_link">
        <asp:TextBox ID="FHTextBox2" runat="server" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_disFundwarehouse_0002',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FHTextBox2"
            ErrorMessage="请输入捐献积分数" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FHTextBox2"
            Display="Dynamic" ErrorMessage="您输入的格式不对" ValidationExpression="^[1-9]\d*|0$"></asp:RegularExpressionValidator></td>
  </tr>
  <tr class="TR_BG_list">
    <td  class="list_link"></td>
    <td  class="list_link">
        <asp:Button ID="FHBut1" runat="server" Text="确 定" CssClass="form" OnClick="FHBut1_Click" />
        <asp:Button ID="FHBut2" runat="server" Text="重 置" CssClass="form" OnClick="FHBut2_Click" />
        </td>
  </tr>
</table>
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
</html>
