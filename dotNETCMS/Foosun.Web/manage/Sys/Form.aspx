<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Form.aspx.cs" Inherits="Foosun.Web.manage.Sys.Form" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
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
                            src="../../sysImages/folder/navidot.gif" border="0" />自定义表单管理</div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
                <td>
                    &nbsp;&nbsp;<a href="Form_Add.aspx" class="topnavichar">新增表单</a></td>
            </tr>
        </table>
        <div>
            <asp:Repeater ID="RptData" runat="server">
                <HeaderTemplate>
                    <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" id="tablist"
                        class="table">
                        <tr class="TR_BG">
                            <td width="7%" align="center" valign="middle" class="sysmain_navi">
                                表单名称</td>
                            <td width="29%" align="left" valign="middle" class="sysmain_navi">
                                表名</td>
                            <td width="22%" align="center" valign="middle" class="sysmain_navi">
                                说明</td>
                            <td align="center" valign="middle" class="sysmain_navi">
                                操作<input name="Checkboxc" type="checkbox" onclick="javascript:selectAll(this.form,this.checked);" /></td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
                        <td class="list_link">
                            <%# DataBinder.Eval(Container.DataItem, "formname")%>
                        </td>
                        <td class="list_link">
                            <%# DataBinder.Eval(Container.DataItem, "formtablename")%>
                        </td>
                        <td class="list_link">
                            <%# DataBinder.Eval(Container.DataItem, "memo")%>
                        </td>
                        <td class="list_link"></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div align="right" style="width: 98%">
                <uc1:PageNavigator ID="PageNavigator1" runat="server" />
            </div>
        </div>
        <br />
        <br />
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
            style="height: 76px">
            <tr>
                <td align="center">
                    <%Response.Write(CopyRight); %>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
