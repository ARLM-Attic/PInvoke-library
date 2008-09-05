<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CustomForm_Item.aspx.cs" Inherits="Foosun.Web.manage.Sys.CustomForm_Item" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" language="javascript">
<!--
function DeleteItem(id)
{
    if(window.confirm('你确定要删除该表单项吗?数据将不能被恢复!'))
    {
        var options={
            method:'post',
            parameters:"Option=DeleteItem&ID="+ id,
            onComplete:
                function(transport)
	            {
	                var retv =transport.responseText;
	                OnRcvMsg(retv);
	            } 
	    }
	    new  Ajax.Request('CustomForm_Item.aspx',options);
    }
}
function OnRcvMsg(rtstr)
{
   var n = rtstr.indexOf("%");
   alert(rtstr.substr(n+1,rtstr.length-n-1));
   if(parseInt(rtstr.substr(0,n)) > 0)
   {
       __doPostBack('PageNavigator1$LnkBtnGoto','');
   }
}
//-->
</script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="50%" class="sysmain_navi" style="padding-left: 14px; height: 32px;">
                    自定义表单项管理<span class="helpstyle" style="cursor: hand;" title="点击查看帮助" onclick="Help('',this)">(帮助)</span></td>
                <td width="50%" class="topnavichar" style="height: 32px;">
                    <div align="left">
                        位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" /><a href="CustomForm.aspx" class="topnavichar">自定义表单</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" /><asp:Literal runat="server" ID="LtrFormName"></asp:Literal><img alt="" 
                            src="../../sysImages/folder/navidot.gif" border="0" />表单项管理</div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
                <td>
                    &nbsp;&nbsp;<asp:HyperLink ID="HlkCreate" runat="server" class="topnavichar">新建表单项</asp:HyperLink>
                    </td>
            </tr>
        </table>
        <div>
            <asp:Repeater ID="RptData" runat="server">
                <HeaderTemplate>
                    <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" id="tablist"
                        class="table">
                        <tr class="TR_BG">
                            <td width="5%" align="center" class="sysmain_navi">
                                顺序</td>
                            <td width="20%" align="center" class="sysmain_navi">
                                表单项名</td>
                            <td width="20%" align="center" class="sysmain_navi">
                                字段名</td>
                            <td width="20%" align="center" class="sysmain_navi">
                                字段类型</td>
                            <td width="20%" align="center" class="sysmain_navi">
                                是否必填</td>
                            <td width="15%" align="center" class="sysmain_navi">
                                操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
                        <td class="list_link" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "seriesnumber")%>
                        </td>
                        <td class="list_link">
                            <%# DataBinder.Eval(Container.DataItem, "itemname")%>
                        </td>
                        <td class="list_link">
                            <%# DataBinder.Eval(Container.DataItem, "fieldname")%>
                        </td>
                        <td class="list_link">
                            <%# GetTypeName(DataBinder.Eval(Container.DataItem, "itemtype"))%>
                        </td>
                        <td class="list_link" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "notnull")%>
                        </td>
                        <td class="list_link" align="center">
                            <a class="list_link" href="CustomForm_Item_Add.aspx?ID=<%# DataBinder.Eval(Container.DataItem, "id")%>">修改</a> 
                            <a class="list_link" href="javascript:DeleteItem(<%# DataBinder.Eval(Container.DataItem, "id")%>);">删除</a>
                        </td>
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
