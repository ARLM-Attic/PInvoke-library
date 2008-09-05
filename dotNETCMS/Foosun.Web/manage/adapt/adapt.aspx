<%@ Page Language="C#" AutoEventWireup="true" Codebehind="adapt.aspx.cs" Inherits="Foosun.Web.manage.adapt.adapt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css"
        rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" style="height: 32" align="center" border="0" cellpadding="0"
            cellspacing="0" background="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/reght_1_bg_1.gif">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" height="32" class="sysmain_navi" style="padding-left: 14px">
                    整合接口</td>
                <td width="43%" height="32" class="topnavichar" style="padding-left: 14px">
                    位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt=""
                        src="../../sysImages/folder/navidot.gif" border="0" />整合接口</td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
            <tr class="TR_BG_list">
                <td class="list_link" style="width: 114px">
                    <div align="right">
                        是否开启整合</div>
                </td>
                <td class="list_link" style="height: 23px">
                    <asp:CheckBox ID="Api_Enable" runat="server" /><span class="helpstyle" style="cursor: help;"
                        title="点击查看帮助" onclick="Help('H_navimenu_0005',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" style="width: 114px">
                    <div align="right">
                        应用程序标识</div>
                </td>
                <td class="list_link">
                    <asp:TextBox ID="TextBoxAppID" runat="server" Style="width: 350px"></asp:TextBox>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" ">
                        <!--onclick="Help('H_navimenu_0003',this)height="32"-->
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" style="width: 114px">
                    <div align="right">
                        系统整合密钥</div>
                </td>
                <td class="list_link">
                    <asp:TextBox ID="Api_Key" runat="server" Style="width: 350px"></asp:TextBox>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" ">
                        <!--onclick="Help('H_navimenu_0003',this)height="32"-->
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" style="width: 114px; height: 28px;">
                    &nbsp;</td>
                <td class="list_link" style="height: 28px">
                    &nbsp;<asp:Button ID="submit" runat="server" CssClass="form" Text=" 确 定 " OnClick="submit_Click" />
                    <input name="reset" type="reset" value=" 重 置 " class="form">
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1">
            <tr>
                <td height="18" style="width: 45%" colspan="2" align="right" class="list_link">
                    <a href="add.aspx" class="topnavichar">添加接口应用程序</a>&nbsp;┊&nbsp;
                    </td>
            </tr>
        </table>
        <asp:Repeater ID="RepeaterApplist" runat="server" OnItemDataBound="RepeaterApplist_ItemDataBound">
            <HeaderTemplate>
                <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
                    <tr class="TR_BG">
                        <td width="120px" align="center" valign="middle" class="sys_topBg">
                            应用程序标识</td>
                        <td align="center" valign="middle" class="sys_topBg">
                            接口文件URL</td>
                            <td align="center" width="150px">
                            操作
                            </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="TR_BG_list">
                    <td>
                        <asp:Literal ID="LiteralAppID" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="LiteralAppUrl" runat="server"></asp:Literal>
                    </td>
                    <td>
                       <asp:HyperLink ID="hpedit" runat="server" > <img src="../../sysImages/<%=Foosun.Config.UIConfig.CssPath() %>/sysico/edit.gif" border="0"  title="修改"/></asp:HyperLink>
                       <asp:HyperLink ID="hpdelete" runat="server" > <img src="../../sysImages/<%=Foosun.Config.UIConfig.CssPath() %>/sysico/del.gif" border="0"  title="删除"/></asp:HyperLink>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
    <br />
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
        style="height: 76px">
        <tr>
            <td align="center">
                <label id="copyright" runat="server" />
            </td>
        </tr>
    </table>
</body>
</html>
