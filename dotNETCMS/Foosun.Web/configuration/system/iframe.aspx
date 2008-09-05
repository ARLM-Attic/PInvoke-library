<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="configuration_system_iframe" Codebehind="iframe.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../js/Public.js"></script>
</head>
<body>
<div onmousedown="drag(event,$('s_id'));" class="titile_bg" style="cursor:move;"><table style="width:100%;background-color:#006699;" cellpadding="0" cellspacing="0" ><tr><td style="height:26px;"><font color="white">窗口可拖动;双击此处关闭窗口;列表处双击选择文件</font></td><%--<td style="width:10px"><img align="right" src="../../sysImages/normal/close.gif" style="cursor:pointer;padding-right:2px;padding-bottom:2px;" title="关闭" onclick="closediv($('s_id'));" /></td>--%></tr></table></div>
<div  id="select_iframe" runat="server" />
</body>
</html>
