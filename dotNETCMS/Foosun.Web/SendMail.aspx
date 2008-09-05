<%@ Page Language="C#" AutoEventWireup="true" Inherits="SendMail" Codebehind="SendMail.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>发送给好友</title>
<script language="javascript" type="text/javascript" src="configuration/js/Prototype.js"></script>
<style type="text/css">
/* CSS Document */
body {
	margin:10;
	padding:0;
	background: #FFF;
	font-size:12px;
	color:#000;
	margin-left: 0px;
	margin-top: 0px;
	background-color: #F7F7F7;
	line-height:20px;
}
.class1
{
	font-size:14px;
	color:#FF0000;
}
.classnoe
{
	font-size:14px;
}
</style>
</head>
<body>
<form action="" method="post" id="form1" runat="server">
<div style="background-color:#EEEEEE;"><a href="http://www.foosun.net" target="_blank"><img src="sysImages/normal/logo.jpg" alt="powered by Foosun Inc." width="207" border="0" /></a></div>
<div style="padding-left:15px;padding-top:100px;padding-bottom:200px;">
  <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#EEEEEE">
    <tr>
      <td><b>发送“<font color="red"><span id="NewsTitle" runat="server"></span></font>”给好友</b></td>
    </tr>
    <tr>
      <td style="padding-left:300px;">好友电子邮件地址：<asp:TextBox ID="TO" runat="server" Width="200"></asp:TextBox> <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="TO" Display="Dynamic" ErrorMessage="请填写好友电子邮件地址"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" Display="Dynamic" ErrorMessage="邮箱格式不正确" ControlToValidate="TO" ValidationExpression="^[a-zA-Z0-9]{1,}@[a-zA-Z0-9]{1,}\.(com|net|org|edu|mil|cn|cc)$"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
      <td style="padding-left:300px;">您的电子邮件地址：<asp:TextBox ID="FROM" runat="server" Width="200"></asp:TextBox>  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FROM" Display="Dynamic" ErrorMessage="请填写您的电子邮件地址"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ErrorMessage="邮箱格式不正确" ControlToValidate="FROM" ValidationExpression="^[a-zA-Z0-9]{1,}@[a-zA-Z0-9]{1,}\.(com|net|org|edu|mil|cn|cc)$"></asp:RegularExpressionValidator></td>
    </tr>   
    <tr>
      <td style="padding-left:400px;">
          <asp:Button ID="Button1" runat="server" Text=" 发送 " OnClick="Button1_Click" /> 
          <input id="Button2" type="button" value=" 关闭 " onclick="javascript:window.close();" /><asp:HiddenField ID="Content" runat="server" /><asp:HiddenField ID="NewsLinkURL" runat="server" /><asp:HiddenField ID="Title" runat="server" />
      </td>
    </tr>
  </table>
</div>
</form>
<div style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:10px;height:50px;text-align:center;"> Powered by dotNETCMS v1.0 for Foosun Inc. website:www.foosun.net </div>
</body>
</html>
