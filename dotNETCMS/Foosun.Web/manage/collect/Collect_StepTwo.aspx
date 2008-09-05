<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_collect_Collect_StepTwo" Codebehind="Collect_StepTwo.aspx.cs" %>

<%@ Register Src="CollectEditor.ascx" TagName="CollectEditor" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>FoosunCMS For .NET v1.0.0</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Public.js"></script>
    <script language="javascript" type="text/javascript">
function ChangeCutPara(obj,flag)
{
    var flag;
    if(document.getElementById('RadPageNone').checked)
        flag = 0;
    else if(document.getElementById('RadPageFlag').checked)
        flag = 1;
    else if(document.getElementById('RadPageSingle').checked)
        flag = 2;
    else if(document.getElementById('RadPageIndex').checked)
        flag = 3;
    var tb = document.getElementById('tabList');
    var n = 1;
    var sp = document.getElementById('SpanPage');
    switch(flag)
    {
        case 0:
            tb.rows[n+1].style.display = 'none';
            tb.rows[n+2].style.display = 'none';
            break;
        case 1:
            tb.rows[n+1].style.display = '';
            tb.rows[n+2].style.display = 'none';
            sp.innerText = '从当前页获取下一页的地址，再从下一页中获取下一页的地址，以此类推。例如:<a href=[其他页面]>下一页,当前页面的下一页必须咋一';
            break;
        case 2:
            tb.rows[n+1].style.display = '';
            tb.rows[n+2].style.display = 'none';
            sp.innerText = '从当前页获取所有分页的地址。';
            break;
        case 3:
            tb.rows[n+1].style.display = 'none';
            tb.rows[n+2].style.display = '';
            //sp.innerText = '例如:<a href=?page=^$^&class_ID=32>  ^$^为发生变化的页码值';
            break;
    }
}
function StepBack()
{
    location.href = "Collect_Add.aspx?Type=Site&ID="+ document.getElementById("HidSiteID").value;
}
function LoadMe(flag)
{
    ChangeCutPara();
}
    </script>
</head>
<body onload="LoadMe(Math.random())">
    <form id="Form2" runat="server">
        <div>
            <table id="top1" width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
                <tr>
                    <td height="1" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td width="57%" class="sysmain_navi" style="padding-left: 14px">
                        采集系统</td>
                    <td width="43%" class="topnavichar" style="padding-left: 14px">
                        <div align="left">
                            位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />
                            <a href="Collect_List.aspx" target="sys_main" class="list_link">站点设置</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />设置向导
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td>
                        功能：<a href="Collect_List.aspx" class="list_link">站点列表</a>&nbsp;┊&nbsp;<a class="topnavichar" href="Collect_RuleList.aspx">关键字过滤</a>&nbsp;┊&nbsp;<a class="topnavichar" href="Collect_News.aspx">新闻处理</a></td>
                </tr>
            </table>
    <table id="tabList" width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG_list" id="tt">
            <td class="list_link" width="30%" align="center">
                列表内容:
            </td>
            <td class="list_link" width="70%">
                <uc1:CollectEditor ID="EdtList" runat="server" />
            </td>
        </tr>
        <tr class="TR_BG_list">
            <td class="list_link" width="30%" align="center">分页设置</td>
            <td class="list_link">
                <asp:RadioButton onclick="ChangeCutPara();" runat="server" ID="RadPageNone" GroupName="RadPageSet" Text="不分页"/>
                <asp:RadioButton onclick="ChangeCutPara();" runat="server" ID="RadPageFlag" GroupName="RadPageSet" Text="递归分页设置"/>
                <asp:RadioButton onclick="ChangeCutPara();" runat="server" ID="RadPageSingle" GroupName="RadPageSet" Text="单页分页设置" OnCheckedChanged="RadPageSingle_CheckedChanged"/>
                <asp:RadioButton onclick="ChangeCutPara();" runat="server" ID="RadPageIndex" GroupName="RadPageSet" Text="索引分页设置"/>
            </td>
        </tr>
        <tr class="TR_BG_list">
            <td class="list_link" align="center">
                其他页面</td>
            <td class="list_link">
                <uc1:CollectEditor ID="EdtPageFlag" runat="server" />
                <br />
                <span style="color:Red" id="SpanPage"></span>
            </td>
        </tr>
        <tr class="TR_BG_list">
            <td class="list_link" align="center">
                索引规则</td>
            <td class="list_link">
                <uc1:CollectEditor ID="EdtPageIndex" runat="server" />
                页码开始：
                <asp:TextBox runat="server" size="5" ID="TxtPageStart" CssClass="form"/>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtPageStart"
                    Display="Dynamic" ErrorMessage="开始页码必须是1-1000以内的整数!" MaximumValue="1000" MinimumValue="1"
                    SetFocusOnError="True" Type="Integer" CssClass="form"></asp:RangeValidator>
                &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                页码结束：
                <asp:TextBox runat="server" size="5" ID="TxtPageEnd"/>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TxtPageEnd"
                    Display="Dynamic" ErrorMessage="结束页码必须是1-1000以内的整数!" MaximumValue="1000" MinimumValue="1"
                    SetFocusOnError="True" Type="Integer"></asp:RangeValidator><br><FONT color=#ff4500>例如:&lta href=?page=[页码]&amp;class_ID=32&gt; 注: [页码]为发生变化的页码值</FONT>
                </td>
        </tr>
        <tr class="TR_BG_list">
            <td class="list_link" align="center" colspan="2">
                <asp:HiddenField ID="HidSiteID" runat="server" />
                <input type="button" value="上一步" class="form" onclick="StepBack()"/>
                <asp:Button runat="server" ID="BtnNext" Text="下一步" CssClass="form" OnClick="BtnNext_Click" />
            </td>
        </tr>
    </table>
            <br />
            <br />
            <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
                style="height: 76px">
                <tr>
                    <td align="center">
                        <%Response.Write(CopyRight);%>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>