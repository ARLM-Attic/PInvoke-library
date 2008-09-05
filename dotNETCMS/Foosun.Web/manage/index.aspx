<%@ Page Language="C#" ContentType="text/html" Inherits="Manage_Index" Codebehind="~/Manage/Index.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>--后台管理</title>
        <script language="JavaScript" type="text/javascript">
<!--
function killErrors() {
return true;
}

window.onerror = killErrors;

// -->
    </script>
<%--<link href="../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />--%>
</head>
<frameset rows="85,*" cols="*" frameborder="no" border="0" framespacing="0">
    <frame src="top.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame"
        title="topFrame" />
    <frameset cols="183,*" frameborder="no" border="0" framespacing="0" name="bodyFrame"
        id="bodyFrame">
        <frame src="menu.aspx?type=000000000006" name="menu" scrolling="yes" frameborder="0"
            noresize="noresize" id="menu" title="leftFrame" />
        <frame src="main.aspx" name="sys_main" id="sys_main" title="mainFrame" />
    </frameset>
</frameset>
<noframes>
    <body>
    </body>
</noframes>
</html>
