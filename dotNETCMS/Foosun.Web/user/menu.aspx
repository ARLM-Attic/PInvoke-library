<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_menu" Codebehind="menu.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %></title>
<link href="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Public.js"></script>
</head>
<body>
<div style="padding-left:14px;padding-top:6px;padding-bottom:3px;" class="menulist"><strong>快捷导航</strong></div>
<div id="shortcut_id" runat="server" />
</body>
</html>
