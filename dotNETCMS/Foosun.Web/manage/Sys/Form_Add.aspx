<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Form_Add.aspx.cs" Inherits="Foosun.Web.manage.Sys.Form_Add" %>

<%@ Register Src="../../controls/UserPop.ascx" TagName="UserPop" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px; height: 32px;">
                    自定义表单管理<span class="helpstyle" style="cursor: hand;" title="点击查看帮助" onclick="Help('',this)">(帮助)</span></td>
                <td width="43%" class="topnavichar" style="padding-left: 14px; height: 32px;">
                    <div align="left">
                        位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" /><a href="Form.aspx" class="topnavichar">自定义表单管理</a><img
                                alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:Literal runat="server"
                                    ID="LtrCaption"></asp:Literal></div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" id="tablist"
            class="table">
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    表单名称：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="Txt" Width="309px" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    表名称：</td>
                <td class="list_link">
                    <asp:Label runat="server" ID="LblTablePre"></asp:Label>
                    <asp:TextBox runat="server" ID="TxtTableName" Width="222px" CssClass="form"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTableName"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtTableName"
                        Display="Dynamic" ErrorMessage="表名必须是英文字母或者数字的组合" SetFocusOnError="True" ValidationExpression="^\w+$"></asp:RegularExpressionValidator></td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    上传附件保存地址：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TextBox1" Width="309px" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    上传文件大小：</td>
                <td width="80%" align="left" class="list_link">
                    最大值<asp:TextBox runat="server" ID="TextBox2" Width="169px" CssClass="form"></asp:TextBox>KB
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    状态：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TextBox3" Width="309px" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    启用时间限制：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:RadioButton runat="server" ID="RadOpen" GroupName="RadGrpTimeSet" Text="启用" />
                    <asp:RadioButton runat="server" ID="RadForbid" GroupName="RadGrpTimeSet" Text="不启用" />
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    开始时间：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TextBox5" Width="163px" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    结束时间：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TextBox6" Width="150px" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    提交权限：</td>
                <td width="80%" align="left" class="list_link">
                    <uc1:UserPop ID="UserPop1" runat="server" />
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    验证码设置：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:Checkbox runat="server" ID="ChbShowValidate" Text="显示验证码" />
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td align="center" class="list_link" colspan="2">
                    <asp:Button runat="server" ID="BtnOK" Text=" 确定 " CssClass="form" OnClick="BtnOK_Click" />
                    <input type="reset" value=" 重写 " class="form" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
            <tr>
                <td align="center">
                    <%Response.Write(CopyRight); %>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HidID" />
    </form>
</body>
</html>
