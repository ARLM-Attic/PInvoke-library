<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_collect_Collect_RuleList" Codebehind="Collect_RuleList.aspx.cs" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>FoosunCMS For .NET v1.0.0</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet"
        type="text/css" />

    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Public.js"></script>

    <script language="javascript" type="text/javascript">
 <!--
 function DeleteMe(id)
 {
    if(window.confirm('您确定要删除该规则吗？数据将不能再恢复！'))
    { 
        var param = 'Option=DeleteRule&ID='+ id;
        var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	        {
	            var retv=transport.responseText;
		        OnRecv(retv);
            }
        }
        new  Ajax.Request('Collect_RuleList.aspx',options);
    }
 }
 function OnRecv(retv)
 {
    var n = retv.indexOf('%');
    alert(retv.substr(n+1,retv.length-n-1));
    if(parseInt(retv.substr(0,n)) > 0)
    {
        __doPostBack('PageNavigator1$LnkBtnGoto','');
    }
 }
 //-->
    </script>

</head>
<body>
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
                            位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt=""
                                src="../../sysImages/folder/navidot.gif" border="0" />关键字过滤
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td>
                        功能：<a class="topnavichar" href="Collect_RuleAdd.aspx">新建规则</a>&nbsp;┊&nbsp;<a class="topnavichar"
                            href="Collect_List.aspx">站点设置</a>&nbsp;┊&nbsp;<a class="topnavichar" href="Collect_News.aspx">新闻处理</a></td>
                </tr>
            </table>
            <asp:Repeater runat="server" ID="RptRule">
                <HeaderTemplate>
                    <table id="tablist" width="98%" border="0" align="center" cellpadding="5" cellspacing="1"
                        class="table">
                        <tr class="TR_BG">
                            <td class="sys_topBg" width="65%" align="center">
                                规则名称</td>
                            <td class="sys_topBg" width="25%" align="center">
                                创建时间</td>
                            <td class="sys_topBg" width="10%" align="center">
                                操 作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
                        <td class="list_link">
                            <%# DataBinder.Eval(Container.DataItem, "RuleName")%>
                        </td>
                        <td class="list_link" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "AddDate")%>
                        </td>
                        <td class="list_link" align="center">
                            <a href="Collect_RuleAdd.aspx?RID=<%# DataBinder.Eval(Container.DataItem, "ID")%>"
                                class="list_link">
                                <img src="../../sysImages/folder/re.gif" border="0" alt="修改" /></a> <a href="javascript:DeleteMe(<%# DataBinder.Eval(Container.DataItem, "ID")%>);"
                                    class="list_link">
                                    <img src="../../sysImages/folder/dels.gif" border="0" alt="彻底删除" /></a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:HiddenField runat="server" ID="HidFolderID" Value="" />
            <div align="right" style="width: 98%">
                <uc1:PageNavigator ID="PageNavigator1" runat="server" />
            </div>
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
