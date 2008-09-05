<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CustomForm_Add.aspx.cs" Inherits="Foosun.Web.manage.Sys.CustomForm_Add" %>

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
    function changetm(flag)
    {
        var f = 'none';
        if(flag == 0)
            f = '';
        document.getElementById('tr_tms').style.display = f;
        document.getElementById('tr_tme').style.display = f;            
    }
    function LoadMe(i)
    {
        var f = 0;
        if(document.getElementById('RadTimeNotLmt').checked)
            f = 1;
        changetm(f); 
    }
    function GetPY1(obj)
    {
        //if(document.getElementById('ClassID').value=="")
        //{
            var s = obj.value.trim();
            if(s != '')
            {
                document.getElementById('TxtTableName').value = GetPY(s);
            }
        //}
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
                    自定义表单管理<span class="helpstyle" style="cursor: hand;" title="点击查看帮助" onclick="Help('',this)">(帮助)</span></td>
                <td width="43%" class="topnavichar" style="padding-left: 14px; height: 32px;">
                    <div align="left">
                        位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" /><a href="CustomForm.aspx" class="topnavichar">自定义表单管理</a><img
                                alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:Literal runat="server"
                                    ID="LtrCaption"></asp:Literal></div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
                <td>
                    &nbsp;&nbsp;<a href="CustomForm.aspx" class="topnavichar">返回</a>
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" id="tablist"
            class="table">
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    表单名称：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TxtName" Width="309px" onChange="javascript:GetPY1(this);" CssClass="form" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtName"
                        Display="Dynamic" ErrorMessage="请填写表单名称" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    表名称：</td>
                <td class="list_link">
                    <asp:Label runat="server" ID="LblTablePre"></asp:Label>
                    <asp:TextBox runat="server" ID="TxtTableName" Width="222px" CssClass="form" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTableName"
                        Display="Dynamic" ErrorMessage="请填写表名称" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtTableName"
                        Display="Dynamic" ErrorMessage="表名必须由英文字母或数字、下划线组成!" SetFocusOnError="True" ValidationExpression="^[A-Za-z0-9_]+$"></asp:RegularExpressionValidator></td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    上传附件保存地址：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TxtFolder" Width="309px" CssClass="form" MaxLength="50"></asp:TextBox><img src="../../sysImages/folder/s.gif" alt="选择日期" border="0" style="cursor:pointer;" onclick="selectFile('path',document.Form1.TxtFolder,250,500);document.Form1.TxtFolder.focus();" />
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    上传文件大小：</td>
                <td width="80%" align="left" class="list_link">
                    最大值<asp:TextBox runat="server" ID="TxtMaxSize" Width="66px" CssClass="form" MaxLength="10"></asp:TextBox>KB
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtMaxSize"
                        Display="Dynamic" ErrorMessage="文件大小必须是正整数" MaximumValue="2147483647" MinimumValue="1"
                        SetFocusOnError="True" Type="Integer"></asp:RangeValidator></td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    状态：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:RadioButton runat="server" ID="RadNormal" GroupName="RadGrpState" Text="正常" Checked="True" />
                    <asp:RadioButton runat="server" ID="RadLock" GroupName="RadGrpState" Text="锁定" />
                    &nbsp;</td>
            </tr>
            <tr class="TR_BG_list">
                <td width="20%" align="right" class="list_link">
                    启用时间限制：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:RadioButton runat="server" ID="RadTimeLimited" onclick="changetm(0)" GroupName="RadGrpTimeSet" Text="启用" />
                    <asp:RadioButton runat="server" ID="RadTimeNotLmt" onclick="changetm(1)" GroupName="RadGrpTimeSet" Text="不启用" Checked="True" />
                </td>
            </tr>
            <tr class="TR_BG_list" id="tr_tms">
                <td width="20%" align="right" class="list_link">
                    开始时间：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TxtStartTm" Width="150px" CssClass="form"></asp:TextBox><img src="../../sysImages/folder/s.gif" alt="选择日期" border="0" style="cursor:pointer;" onclick="selectFile('date',document.Form1.TxtStartTm,250,500);document.Form1.TxtStartTm.focus();" />
                </td>
            </tr>
            <tr class="TR_BG_list" id="tr_tme">
                <td width="20%" align="right" class="list_link">
                    结束时间：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TxtEndTm" Width="150px" CssClass="form"></asp:TextBox><img src="../../sysImages/folder/s.gif" alt="选择日期" border="0" style="cursor:pointer;" onclick="selectFile('date',document.Form1.TxtEndTm,250,500);document.Form1.TxtEndTm.focus();" />
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
                    提交次数限制：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:Checkbox runat="server" ID="ChbOnce" Text="每个用户只能提交一次" />
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
                <td width="20%" align="right" class="list_link">
                    表单说明：</td>
                <td width="80%" align="left" class="list_link">
                    <asp:TextBox TextMode="multiLine" runat="server" ID="TxtMemo" Height="140px" Width="292px"></asp:TextBox>
                    (255个字符以内有效)</td>
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
