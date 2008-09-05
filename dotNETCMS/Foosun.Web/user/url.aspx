<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_url" Codebehind="url.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>__网址收藏</title>
<link rel="icon" href="../favicon.ico" type="image/x-icon" />
<link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" /> 
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Public.js"></script>
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Prototype.js"></script>
<link href="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<link href="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/divcss.css" rel="stylesheet" type="text/css" />
<style type="text/css">
a{
    text-decoration:none;
}
a:hover{
    text-decoration:underline;
</style>
</head>
<body>
<form id="form1" runat = "Server">
    <table border="0" cellpadding="2" align="center" class="2" style="width:70%;">
    <tr>
        <td style="width:30%;"><a href="http://www.foosun.net" target="_blank"><img src="../sysImages/user/userlogo.gif" border="0" /></a></td>
        <td style="width:50%;">此处插入您的广告</td>
        <td style="width:20%;"><a href="login.aspx" class="list_link">登陆</a>&nbsp;┊&nbsp;<a href="Register.aspx" class="list_link">注册</a>&nbsp;┊&nbsp;<a href="javascript:void(0);" onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('<%Response.Write(fURL); %>');" style="cursor:pointer;" class="list_link"><span style="color:Red">设为首页</span></a></td>
    </tr>
    </table>
    <span id="urlList" runat="server" />
 <table width="70%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
   <tr>
     <td style="text-align:center">
        <label id="copyright" runat="server" />
     </td>
   </tr>
 </table>
</form>
</body>
</html>