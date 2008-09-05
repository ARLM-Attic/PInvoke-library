<%@ Page Language="C#" AutoEventWireup="true" Inherits="Foosun.Web.manage.Sys.CustomForm_HtmlCode" Codebehind="CustomForm_HtmlCode.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet"
        type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>

    <script language="javascript" type="text/javascript">
<!--   
function copyToClipBoard()
{ 
    var clipBoardContent = document.getElementById("TxtCode").value;
    window.clipboardData.setData("Text",clipBoardContent);
}
function ChangeTR(n)
{
    var tr0 = document.getElementById('TRHTML');
    var tr1 = document.getElementById('TRPRE');
    if(n == 0)
    {
        tr0.style.display = '';
        tr1.style.display = 'none';
    }
    else
    {
        tr0.style.display = 'none';
        tr1.style.display = '';
    }
}
//-->
</script>
</head>
<body onload="ChangeTR(<%# flag%>);">
    <form id="form1" runat="server">
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="table">
            <tr class="TR_BG" style="height:33px;">
                <td class="sysmain_navi" width="200px" style="cursor:hand;" onclick="ChangeTR(0)">
                    自定义表单HTML代码
                </td>
                <td class="sysmain_navi" width="200px" style="cursor:hand;" onclick="ChangeTR(1)">
                    预览
                </td>
                <td class="sysmain_navi" width="300px">
                </td>
            </tr>
            <tr class="TR_BG_list" id="TRHTML">
                <td class="list_link" align="center" style="word-break: break-all;" colspan="3">
                    <asp:TextBox TextMode="multiLine" Width="99%" Height="430px" ID="TxtCode" runat="server" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list" id="TRPRE">
                <td runat="server" id="TD_Code" class="list_link" align="center" colspan="3">
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" align="center" colspan="3">
                    <input type="button" name="Submit" value=" 关闭对话框 " onclick="self.close();" class="form" />
                
                    <input type="button" name="copy" value="复制到剪帖板" onclick="copyToClipBoard();" class="form" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
