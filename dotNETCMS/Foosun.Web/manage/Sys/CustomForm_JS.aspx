<%@ Page Language="C#" AutoEventWireup="true" Inherits="Foosun.Web.manage.Sys.CustomForm_JS" Codebehind="CustomForm_JS.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>自定义表单JS</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet"
        type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>

    <script language="javascript" type="text/javascript">
<!--   
function copyToClipBoard()
{
    var clipBoardContent=document.getElementById("CodePath").value;
    window.clipboardData.setData("Text",clipBoardContent);
}
//-->
    </script>

</head>
<body class="TR_BG_list">
    <form id="form1" runat="server">
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="table">
            <tr class="TR_BG_list">
                <td class="list_link">
                    自定义表单JS:</td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" align="center" style="word-break: break-all;">
                    <textarea name="textfield" style="width: 99%; height: 110px" id="CodePath" runat="server" class="form"></textarea>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" align="center">
                    <input type="button" name="Submit" value=" 关闭对话框 " onclick="self.close();" class="form" />
                
                    <input type="button" name="copy" value=" 复制到剪贴板 " onclick="copyToClipBoard();" class="form" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
