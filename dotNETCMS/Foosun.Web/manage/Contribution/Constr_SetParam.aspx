<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="manage_Contribution_Constr_SetParam" Codebehind="Constr_SetParam.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
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
              稿酬设置</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Constr_List.aspx" target="sys_main" class="list_link">稿件管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />稿酬设置</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td width="46%" class="navi_link">&nbsp; &nbsp; &nbsp;<a href="Constr_List.aspx" class="topnavichar">稿件管理</a>&nbsp; &nbsp;<a href="Constr_SetParamlist.aspx" class="topnavichar">所有设置</a>&nbsp; &nbsp;<a href="#" class="topnavichar">添加设置</a></td>
  </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  <tr class="TR_BG_list">
    <td class="list_link">
        等级：</td>
    <td class="list_link">
        <asp:TextBox ID="ConstrPayName" runat="server" Width="211px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ConstrPayName"
            Display="Dynamic" ErrorMessage="请输入等级名称"></asp:RequiredFieldValidator></td>
    </tr>
      <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        稿酬数：</td>
    <td class="list_link" Width="75%"><asp:TextBox ID="moneys" runat="server" Width="212px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="moneys"
            Display="Dynamic" ErrorMessage="请输入稿酬数"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="moneys"
            Display="Dynamic" ErrorMessage="输入的格式不对" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator></td>
   </tr>
   <tr class="TR_BG_list">
    <td class="list_link">
        获得积分：</td>
    <td>
        <asp:TextBox ID="ipoint" runat="server" Width="212px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ipoint"
            Display="Dynamic" ErrorMessage="请输入积分数"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ipoint"
            Display="Dynamic" ErrorMessage="输入的格式不对" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator></td>
</tr>
   <tr class="TR_BG_list">
    <td class="list_link">
        获得金币：</td>
    <td>
        <asp:TextBox ID="gpoint" runat="server" Width="212px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="gpoint"
            Display="Dynamic" ErrorMessage="请输入金币数"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="gpoint"
            Display="Dynamic" ErrorMessage="输入的格式不对" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator></td>
</tr>
      <tr class="TR_BG_list">
    <td class="list_link">
        单位：</td><td class="list_link">
    <asp:DropDownList ID="Gunit" runat="server" Width="146px">
        <asp:ListItem>人民币</asp:ListItem>
        <asp:ListItem>欧元</asp:ListItem>
        <asp:ListItem>美元</asp:ListItem>
        <asp:ListItem>英镑</asp:ListItem>
        <asp:ListItem>日元</asp:ListItem>
        <asp:ListItem>台币</asp:ListItem>
        <asp:ListItem>港币</asp:ListItem>
        <asp:ListItem>马克</asp:ListItem>
        <asp:ListItem>泰铢</asp:ListItem>
        <asp:ListItem>其它</asp:ListItem>
    </asp:DropDownList></td>
        
</tr>
<tr class="TR_BG_list">
    <td class="list_link">
        </td><td class="list_link">
    &nbsp; &nbsp;&nbsp;
    <asp:Button ID="info" runat="server" OnClick="Button1_Click" Text="添  加"  CssClass="form"/>
    &nbsp; &nbsp; &nbsp;&nbsp;<input type="reset" name="Submit3" value="重  置" class="form"/>
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






