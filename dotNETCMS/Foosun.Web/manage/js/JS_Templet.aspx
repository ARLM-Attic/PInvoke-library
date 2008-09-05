<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_js_JS_Templet" Codebehind="JS_Templet.aspx.cs" %>

<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
<!--
function OnRcvMsg(rtstr)
{
   var n = rtstr.indexOf("%");
   alert(rtstr.substr(n+1,rtstr.length-n-1));
   if(parseInt(rtstr.substr(0,n)) > 0)
   {
      __doPostBack('PageNavigator1$LnkBtnGoto','');
   }
}
function DeleteClass(id)
{
    if(window.confirm('您确认要删除该分类吗?该分类下所有的子分类以及JS模型都将被级联删除,数据将不能恢复!'))
    {
        var options={
            method:'post',
            parameters:'Option=DeleteJSTmpClass&ID='+ id,
            onComplete:
                function(transport)
	            {
	                var retv =transport.responseText;
	                OnRcvMsg(retv);
	            } 
	    }
	    new  Ajax.Request('JS_Templet.aspx',options);
	}
}
function DeleteTmp(id)
{
    if(window.confirm('您确认要删除该分JS模型吗?数据将不能恢复!'))
    {
        var options={
            method:'post',
            parameters:'Option=DeleteJSTemplet&ID='+ id,
            onComplete:
                function(transport)
	            {
	                var retv =transport.responseText;
	                OnRcvMsg(retv);
	            } 
	    }
	    new  Ajax.Request('JS_Templet.aspx',options);
	}
}
function GoToClass(id)
{
    var obj = document.getElementById('DdlClass');
    for(var i=0;i<obj.options.length;i++)
    {
        if(obj.options[i].value == id)
        {
            obj.selectedIndex = i;
            form1.submit();
            break;
        }
    }
}
function AddTemp()
{
    var obj = document.getElementById('DdlClass');
    var val = obj.options[obj.selectedIndex].value;
    location.href = 'JS_Templet_Add.aspx?class='+ val;
}
function AddClass()
{
    var obj = document.getElementById('DdlClass');
    var val = obj.options[obj.selectedIndex].value;
    location.href = 'JS_Templet_Class.aspx?Upper='+ val;    
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
                <td width="57%" class="sysmain_navi" style="padding-left: 14px">
                    JS模型管理</td>
                <td width="43%" class="topnavichar" style="padding-left: 14px">
                    位置：<a href="../main.aspx" class="navi_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />JS模型管理</td>
            </tr>
        </table>
        <!----功能菜单----->
        <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
            <tr class="menulist">
                <td>
                    功能： <a href="javascript:AddClass();" class="topnavichar">新增分类</a>&nbsp;┊&nbsp;<a href="javascript:AddTemp();" class="topnavichar">新增JS模型</a>
                </td>
            </tr>
        </table>
        <asp:Table runat="server" ID="TableData" BorderWidth="1" HorizontalAlign="center" CellPadding="4" CellSpacing="1" Width="98%" CssClass="table">
        <asp:TableRow CssClass="TR_BG">
        <asp:TableCell CssClass="sys_topBg" Width="40%" HorizontalAlign="center">名称</asp:TableCell>
        <asp:TableCell CssClass="sys_topBg" Width="25%" HorizontalAlign="center">类型/数量</asp:TableCell>
        <asp:TableCell CssClass="sys_topBg" Width="20%" HorizontalAlign="center">创建日期</asp:TableCell>
        <asp:TableCell CssClass="sys_topBg" Width="15%" HorizontalAlign="center">操作</asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        <table border="0" align="center" cellpadding="4" cellspacing="1" width="98%" class="table">
        <tr class="TR_BG_list">
        <td class="list_link">
            &nbsp;当前JS模型分类： <asp:DropDownList runat="server" ID="DdlClass" AutoPostBack="True" OnSelectedIndexChanged="DdlClass_SelectedIndexChanged" CssClass="form"><asp:ListItem Value="0">根节点</asp:ListItem></asp:DropDownList></td>
        </tr>
        </table>
            <!--------分页--------->
            <div id="pagee" align="right" runat="server">
                <uc1:PageNavigator ID="PageNavigator1" runat="server" />
            </div>
        <br />
        <!-------CopyRight------->
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
            style="height: 76px">
            <tr>
                <td align="center">
                    <%Response.Write(CopyRight);%>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
