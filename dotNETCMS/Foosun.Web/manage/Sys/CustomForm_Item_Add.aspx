<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CustomForm_Item_Add.aspx.cs" Inherits="Foosun.Web.manage.Sys.CustomForm_Item_Add" %>

<%@ Register Src="../../controls/UserPop.ascx" TagName="UserPop" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
    <!--
    function changetype(obj)
    {
        var val = obj.options[obj.selectedIndex].value;
        var f = 'none';
        if(val == 'RadioBox' || val == 'CheckBox' || val == 'DropList' || val == 'List')
            f = '';
        document.getElementById('tr_sel').style.display = f;
    }
    function LoadMe(i)
    {
        changetype(document.getElementById('DdlItemType')); 
    }
    //-->
    </script>
</head>
<body onload="LoadMe(Math.random())">
    <form id="Form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px; height: 32px;">
                    自定义表单项<span class="helpstyle" style="cursor: hand;" title="点击查看帮助" onclick="Help('',this)">(帮助)</span></td>
                <td width="43%" class="topnavichar" style="padding-left: 14px; height: 32px;">
                    <div align="left">
                        位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" /><a href="CustomForm.aspx" class="topnavichar">自定义表单</a>
                            <img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:HyperLink ID="HlkManage" runat="server" class="topnavichar">表单项管理</asp:HyperLink>
                            <img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:Literal runat="server" ID="LtrCaption"></asp:Literal></div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
                <td>
                    &nbsp;&nbsp;<asp:HyperLink ID="HlkBack" runat="server" class="topnavichar">返回</asp:HyperLink></td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" id="tablist"
            class="table">
             <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    当前表单：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:Label runat="server" ID="LblFormName"></asp:Label>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    表单项名称：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TxtName" Width="310px" CssClass="form" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtName"
                        Display="Dynamic" ErrorMessage="请填写表单项名称" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    字段名：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TxtFieldName" Width="310px" CssClass="form" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFieldName"
                        Display="Dynamic" ErrorMessage="请填写字段名" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtFieldName"
                        Display="Dynamic" ErrorMessage="字段名必须由英文字母或数字、下划线组成!" SetFocusOnError="True"
                        ValidationExpression="^[A-Za-z0-9_]+$"></asp:RegularExpressionValidator></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    排序序号：</td>
                <td class="list_link">
                    <asp:DropDownList ID="DdlSN" runat="server">
                    </asp:DropDownList>序号越小，排在越前面</td>
            </tr>
             <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    是否启用：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:RadioButton runat="server" ID="RadOpenYes" GroupName="RadGrpState" Text="是" Checked="True" />
                    <asp:RadioButton runat="server" ID="RadOpenNo" GroupName="RadGrpState" Text="否" />
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    是否必填：</td>
                <td class="list_link">
                    <asp:RadioButton ID="RadNotNullYes" runat="server" Text="是" GroupName="RadGrpNull" />
                    <asp:RadioButton ID="RadNotNullNo" runat="server" Text="否" GroupName="RadGrpNull" Checked="True" /></td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    表单项类型：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:DropDownList ID="DdlItemType" runat="server" onchange="changetype(this);">
                    </asp:DropDownList></td>
            </tr>
            <tr class="TR_BG_list">
               <td align="right" class="list_link">
                    文本长度</td>
               <td class="list_link">
                    <asp:TextBox runat="server" ID="TxtMaxSize" Width="66px" CssClass="form" MaxLength="10">20</asp:TextBox>0表示不设置<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtMaxSize"
                       Display="Dynamic" ErrorMessage="文本长度必须是整数" MaximumValue="4000" MinimumValue="0"
                       SetFocusOnError="True" Type="Integer"></asp:RangeValidator></td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    默认值：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox ID="TxtDefault" runat="server" MaxLength="50" Width="310px"></asp:TextBox></td>
            </tr>
            <tr class="TR_BG_list" id="tr_sel">
                <td width="20%" align="right" class="list_link">
                    选项：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox ID="TxtSelectItem" runat="server" Height="143px" TextMode="MultiLine" Width="288px"></asp:TextBox>
                    每一行为一个列表选项</td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    附加提示：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox TextMode="multiLine" runat="server" ID="TxtPrompt" Height="140px" Width="292px"></asp:TextBox>
                    (在名称旁的提示信息，255个字符以内有效)</td>
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
        <asp:HiddenField runat="server" ID="HidItemID" />
        <asp:HiddenField runat="server" ID="HidFormID" />
    </form>
</body>
</html>
