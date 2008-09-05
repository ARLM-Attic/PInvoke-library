<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_js_JS_Add" Codebehind="JS_Add.aspx.cs" %>

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
var numkey = Math.random();
numkey = Math.round(numkey*10000);
var isId = 'foosun<%Response.Write(Request.QueryString["ID"]); %>';
function Display(flag)
{
    var tab = document.getElementById('TableDis');
    for(var i = 4;i<8;i++)
    {
        var tr = tab.rows[i];
        if(flag == 0)
            tr.style.display = 'none';
        else
            tr.style.display = '';
    }
    if(flag == 0)
    {
        document.getElementById('DdlTempSys').style.display = '';
        document.getElementById('DdlTempFree').style.display = 'none';
        if(isId=="foosun")
        {
            document.getElementById('TxtFileName').value = "sys_"+numkey+"";
        }
    }
    else
    {
        document.getElementById('DdlTempSys').style.display = 'none';
        document.getElementById('DdlTempFree').style.display = '';
        if(isId=="foosun")
        {
            document.getElementById('TxtFileName').value = "free_"+numkey+"";
        }
    }
}
function loadjsfile()
{
        document.getElementById('TxtFileName').value = "sys_"
}
function loadme(rand)
{
    var f = 1;
    if(document.getElementById('RadTypeSys').checked)
        f = 0;
    Display(f);
}
//-->
</script>
</head>
<body onload="loadme(Math.random())">
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px"><asp:Label runat="server" ID="LblCatpion"></asp:Label></td>
                <td width="43%" class="topnavichar" style="padding-left: 14px">
                    位置：<a href="../main.aspx" class="navi_link">首页</a> <img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:Label runat="server"
                        ID="LblTitle"></asp:Label></td>
            </tr>
        </table>
        <!----功能菜单----->
        <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
            <tr class="menulist">
                <td style="width: 45%">
                    功能： <a href="JS_List.aspx" class="topnavichar">JS管理</a></td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table" id="TableDis">
            <tr class="TR_BG">
                <td class="sys_topBg" colspan="2">
                    JS信息</td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" width="25%">
                    选择类型：</td>
                <td align="left" class="list_link" width="75%">
                    <asp:RadioButton ID="RadTypeSys" runat="server" Text="系统JS" GroupName="jsType" onclick="Display(0);" Checked="True" />&nbsp;<asp:RadioButton
                        ID="RadTypeFree" runat="server" Text="自由JS" GroupName="jsType" onclick="Display(1);" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0003',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    JS名称：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="TxtName" runat="server" Width="224px" MaxLength="50" CssClass="form" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtName"
                        Display="Dynamic" ErrorMessage="请填写JS名称!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <font color="red">(*)</font><span class="helpstyle" style="cursor: help;" title="点击查看帮助"
                        onclick="Help('H_jsadd_0002',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    新闻调用数量：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="TxtNum" runat="server" Width="224px" CssClass="form" Text="10" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写调用数量!" ControlToValidate="TxtNum" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="新闻调用数量必须为正整数!"
                        Type="Integer" ControlToValidate="TxtNum" Display="Dynamic" MaximumValue="2147483647" MinimumValue="1" SetFocusOnError="True"></asp:RangeValidator>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0006',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    新闻每行显示条数：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="TxtColsNum" runat="server" Width="224px" CssClass="form" Text="1" />
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="新闻每行显示条数必须为正整数!"
                        Type="Integer" ControlToValidate="TxtColsNum" Display="Dynamic" MaximumValue="2147483647" MinimumValue="1" SetFocusOnError="True"></asp:RangeValidator>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0007',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    新闻标题显示字数：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="TxtLenTitle" runat="server" Width="224px" CssClass="form" Text="10" />
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="标题显示字数必须为正整数!"
                        Type="Integer" ControlToValidate="TxtLenTitle" Display="Dynamic" MaximumValue="2147483647" MinimumValue="1" SetFocusOnError="True"></asp:RangeValidator>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0008',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    新闻内容显示字数：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="TxtLenContent" runat="server" Width="224px" CssClass="form" Text="200" />
                    <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="内容显示字数必须为正整数!"
                        Type="Integer" ControlToValidate="TxtLenContent" Display="Dynamic" MaximumValue="2147483647" MinimumValue="1" SetFocusOnError="True"></asp:RangeValidator>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0009',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    新闻导航显示字数：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="TxtLenNavi" runat="server" Width="224px" CssClass="form" Text="5" />
                    <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="导航显示字数必须为正整数!"
                        Type="Integer" ControlToValidate="TxtLenNavi" Display="Dynamic" MaximumValue="2147483647" MinimumValue="1" SetFocusOnError="True"></asp:RangeValidator>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0010',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    JS引用模型：</td>
                <td align="left" class="list_link">
                    <asp:DropDownList ID="DdlTempSys" runat="server" CssClass="form">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DdlTempFree" runat="server" CssClass="form">
                    </asp:DropDownList>
                    <a href="javascript:Reviewtemplet();"><font color="red">浏览模型</font></a>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0011',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    生成JS文件名：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="TxtFileName" runat="server" Width="224px" MaxLength="47" CssClass="form" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtFileName"
                        Display="Dynamic" ErrorMessage="请填写JS文件名!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="生成文件名必须为字母、下划线及数字!" ControlToValidate="TxtFileName" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^[a-zA-Z_0-9_.__]+$"></asp:RegularExpressionValidator>
                    <font color="red">(*)</font><span class="helpstyle" style="cursor: help;" title="点击查看帮助"
                        onclick="Help('H_jsadd_0004',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    生成的JS文件保存路径：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="TxtSavePath" runat="server" MaxLength="200" Width="237px" CssClass="form" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtSavePath"
                        Display="Dynamic" ErrorMessage="请选择JS文件保存路径!" SetFocusOnError="True"></asp:RequiredFieldValidator><font
                        color="red">(*)</font> &nbsp;
                    <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="" title="选择路径" onclick="selectFile('path|<%Response.Write(jspath); %>',document.getElementById('TxtSavePath'),280,500);document.getElementById('TxtSavePath').focus();" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0005',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="Tr1">
                <td align="right" class="list_link">
                    JS描述：</td>
                <td align="left" class="list_link">
                    <asp:TextBox runat="server" ID="TxtContent" CssClass="form" TextMode="multiLine" Width="371px" Height="108px"></asp:TextBox>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0013',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="center" colspan="2" class="list_link">
                    <asp:Button runat="server" ID="BtnOK" Text=" 保 存 " CssClass="form" OnClick="BtnOK_Click" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="Button1" Text=" 重 填 " CssClass="form" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
            style="height: 76px">
            <tr>
                <td align="center">
                    <%Response.Write(CopyRight);%>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HidID" />
        <asp:HiddenField runat="server" ID="HidJsID" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
function Reviewtemplet()
{
    var types;
    var WWidth = (window.screen.width-500)/2;
    //var Wheight = (window.screen.height-150)/2;
    if(document.getElementById('RadTypeSys').checked)
    {
       types = "DdlTempSys";
    }
    else
    {
       types = "DdlTempFree";
    }
    window.open ("JS_templetReview.aspx?tid="+document.getElementById(types).value+"", '浏览模型', 'height=600, width=600, left='+WWidth+', toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); 
}

</script>
