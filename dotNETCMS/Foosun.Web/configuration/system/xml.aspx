<%@ Page Language="C#" AutoEventWireup="true" Inherits="configuration_system_xml" Codebehind="xml.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title><% Response.Write(Foosun.Config.UIConfig.HeadTitle); %></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script><body>
<form id="Templetslist" action="" runat="server" method="post">
<div id="File_List" runat="server" />
</form>
</body>
</html>
<script language="javascript" type="text/javascript">
function ListGo(Path,ParentPath)
{
	document.Templetslist.Path.value=Path;
	document.Templetslist.ParentPath.value=ParentPath;
	document.Templetslist.submit();
}
function sFiles(obj)
{
  document.Templetslist.sUrl.value=obj;
}

function ReturnValue(obj)
{
	var Str=obj;
	parent.ReturnFun(Str);
}
</script>