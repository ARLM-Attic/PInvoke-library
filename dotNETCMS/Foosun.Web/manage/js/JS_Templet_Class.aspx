<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_js_JS_Templet_Class" Codebehind="JS_Templet_Class.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet"  type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
<script language="javascript" type="text/javascript">
<!--
function CheckLength(obj)
{
    var val = obj.value;
    if(val.length > 500)
        obj.value = val.substr(0,500);
}
//-->
</script>

</head>
<body>
    <form id="form1" runat="server">
        <!------头部导航------>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px"><asp:Label runat="server" ID="LblCaption"></asp:Label></td>
                <td width="43%" class="topnavichar" style="padding-left: 14px">
                    位置：<a href="../main.aspx" class="navi_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="JS_Templet.aspx" class="navi_link">JS模型管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:Label runat="server" ID="LblTitle"></asp:Label></td>
            </tr>
        </table>
        <!----功能菜单----->
        <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
            <tr class="menulist">
                <td>
                     功能： <a href="JS_Templet.aspx" class="topnavichar">JS模型管理</a>
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
            <tr class="TR_BG_list">
                <td colspan="2" class="navi_link">
                    <strong>JS模型分类信息</strong></td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" align="right">
                    JS模型分类名称：</td>
                <td class="list_link" align="left">
                    <asp:TextBox ID="TxtName" runat="server" CssClass="form" Width="212px" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtName"
                        Display="Dynamic" ErrorMessage="请填写分类名称!" SetFocusOnError="True"></asp:RequiredFieldValidator>(<font color="red"
                        size="2">*</font>)<span class="helpstyle" onclick="Help('h_jstempletclass_001',this)"
                            style="cursor: help;" title="点击查看帮助">帮助</span>
                            </td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    上级分类：</td>
                <td align="left" class="list_link">
                    <asp:DropDownList ID="DdlUpperClass" runat="server" CssClass="form">
                    <asp:ListItem Value="0">根结点</asp:ListItem>
                    </asp:DropDownList>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('h_jstempletclass_002',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    JS模型分类描述：</td>
                <td align="left" class="list_link">
                    <asp:TextBox runat="server" onchange="CheckLength(this)" onkeydown="CheckLength(this)" ID="TxtDescription" MaxLength="500" TextMode="MultiLine" Width="340px" Height="121px" CssClass="form"></asp:TextBox>
                    <font color="red">500字以内</font><span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('h_jstempletclass_003',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td height="26" colspan="2" align="center" class="list_link">
                  <asp:Button ID="BtnOK" runat="server" Text=" 保 存 " CssClass="form" OnClick="BtnOK_Click" />&nbsp;&nbsp;<asp:Button
                        ID="BtnCancel" runat="server" Text=" 取 消 " CssClass="form" />
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
            <tr>
                <td align="center">
                    <%Response.Write(CopyRight);%>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HidID" />
    </form>
</body>
</html>
