<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Templet_LabelListM" Codebehind="LabelListM.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
</head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<body>
<form id="Label" action="" runat="server">
    <span class="reshow" style="padding-top:12px;padding-left:8px;">栏目动态标签</span>
    <div runat="server" id="LabelList" />

    <span class="reshow" style="padding-top:12px;padding-left:8px;">专题动态标签</span>
    <div runat="server" id="LabelList1" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
function selectLabel(rvalue)
{
    parent.ReturnLabelValueText(rvalue);
}
</script>