<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_createLabel_API" Codebehind="createLabel_API.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
            <tr class="TR_BG_list">
                <td align="center" class="navi_link">API</td>
            </tr>
            <tr class="TR_BG_list">
            <td align="center" class="navi_link">&nbsp;<input class="form" type="button" value=" È· ¶¨ "  onclick="javascript:parent.ReturnLabelValue('');" />&nbsp;<input class="form" type="button" value=" ¹Ø ±Õ "  onclick="javascript:parent.document.getElementById('LabelDivid').style.display='none';" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
