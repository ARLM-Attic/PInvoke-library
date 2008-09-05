<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discuss_discussphotoclass_add" Debug="true" Codebehind="discussphotoclass_add.aspx.cs" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<div id="sc" runat="server"></div>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table" id="insert">

  <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">
        分类名称：</td>
    <td class="list_link" width="75%">
        <asp:TextBox ID="ClassName" runat="server" Width="241px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussphotoclass_0001',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ClassName"
            ErrorMessage="请输入分类名称"></asp:RequiredFieldValidator></td>
  </tr>

  <tr class="TR_BG_list">
    <td class="list_link"></td>
    <td class="list_link">
        <asp:Button ID="Button1" runat="server" Text="创 建" Width="75px" OnClick="Button1_Click"  CssClass="form"/>
        &nbsp;&nbsp;&nbsp;
        <input name="reset" type="reset" value=" 重 置 "  class ="form"/>
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