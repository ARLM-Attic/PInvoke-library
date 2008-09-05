<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_collect_Collect_News" Codebehind="Collect_News.aspx.cs" %>

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
function GetAllChecked()
{
    var retstr = "";
    var tb = document.getElementById("tablist");
    var j = 0;
    for(var i=1;i<tb.rows.length - 1;i++)
    {
        var objtr = tb.rows[i];
        var objtd = objtr.cells[0];
        var objnd = objtd.childNodes[0];
        if(objnd.type == "checkbox" && objnd.checked)
        {
            if(j>0)
                retstr += ",";
            retstr += objnd.value;
            j++;
        }
    }
    return retstr;
}
function GetAllHistoryNews()//已入库的新闻
{
    var retstr = "";
    var tb = document.getElementById("tablist");
    var j = 0;
    for(var i=1;i<tb.rows.length - 1;i++)
    {
        var objtr = tb.rows[i];
        var objtd = objtr.cells[2];
        var objtdid = objtr.cells[0];
        var objnd = objtdid.childNodes[0];
        if(objtd.innerText == "已入库")
        {
            if(j>0)
                retstr += ",";
            retstr += objnd.value;
            j++;
        }
    }
    return retstr;
}
function Transfer(id)
{
    var l;
    var m = "所有未入库";
    if(id == -1)
    {
        l = GetAllChecked();
        if(l == "")
        {
            alert("您没有选择要入库的新闻!");
            return;
        }
        m = "选中";
    }
    else if(id == 0)
    {
        l = id;
    }
    if(confirm('确定要入库'+ m +'的新闻吗?'))
    {
        location.href = 'Store.aspx?ID='+ l;
    }
}
function DeleteCllNews(id)
{
    var l;
    var m = '当前';
    if(id == -1)
    {
        l = GetAllChecked();
        if(l == "")
        {
            alert("您没有选择要删除的新闻!");
            return;
        }
        m = "选中";
    }
    else if(id == 0)
    {
        m = "所有已入库";
        // l = id;
        l = GetAllHistoryNews();
    }
    else
    {
        l = id;
    }
    if(confirm('确定要永久删除'+ m +'新闻吗?数据将不能恢复!'))
    {
         SendAjax("DeleteNews",l);
    }
}

function SendAjax(op,id)
{
    var param = 'Option='+ op +'&NewsID='+ id;
    var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {
		   var retv=transport.responseText;
		   onRcvMsg(retv);
		} 
	  }
	new  Ajax.Request('Collect_News.aspx',options);   
}
function onRcvMsg(rtstr)
{
    var n = rtstr.indexOf("%");
    alert(rtstr.substr(n+1,rtstr.length-n-1));
    if(parseInt(rtstr.substr(0,n)) > 0)
    {
        __doPostBack('PageNavigator1$LnkBtnGoto','');
    }
}

function ChooseAll(obj)
{
    var flag = obj.checked;
    var tb = document.getElementById("tablist");
    for(var i=1;i<tb.rows.length - 1;i++)
    {
        var objtr = tb.rows[i];
        var objtd = objtr.cells[0];
        var objnd = objtd.childNodes[0];
        if(objnd.type == "checkbox")
            objnd.checked = flag;        
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
                                src="../../sysImages/folder/navidot.gif" border="0" />采集新闻处理
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td>
                        功能：<a class="topnavichar" href="Collect_List.aspx">站点管理</a>&nbsp;┊&nbsp;<a class="topnavichar"
                            href="Collect_RuleList.aspx">关键字过滤</a></td>
                </tr>
            </table>
            <asp:Repeater ID="RptNews" runat="server" OnItemDataBound="RptNews_ItemDataBound">
                <HeaderTemplate>
                    <table id="tablist" width="98%" border="0" align="center" cellpadding="5" cellspacing="1"
                        class="table">
                        <tr class="TR_BG">
                            <td class="sys_topBg" width="2%" align="center">
                            </td>
                            <td class="sys_topBg" width="48%" align="center">
                                标题</td>
                            <td class="sys_topBg" width="10%" align="center">
                                状态</td>
                            <td class="sys_topBg" width="15%" align="center">
                                采集站点</td>
                            <td class="sys_topBg" width="15%" align="center">
                                添加日期</td>
                            <td class="sys_topBg" width="10%" align="center">
                                操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
                        <td class="list_link" align="center">
                            <input type="checkbox" value="<%# DataBinder.Eval(Container.DataItem, "ID")%>" /></td>
                        <td class="list_link">
                            <%# DataBinder.Eval(Container.DataItem, "Title")%>
                        </td>
                        <td class="list_link" align="center">
                            <asp:Label runat="server" ID="LblState" Text='<%# DataBinder.Eval(Container.DataItem, "History")%>' /></td>
                        <td class="list_link" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "SiteName")%>
                        </td>
                        <td class="list_link" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "AddDate")%>
                        </td>
                        <td class="list_link" align="center">
                            <a href="Collect_NewsModify.aspx?ID=<%# DataBinder.Eval(Container.DataItem, "ID")%>"
                                class="list_link">
                                <img src="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/sysico/edit.gif" border="0" alt="修改" /></a> <a href="javascript:DeleteCllNews(<%# DataBinder.Eval(Container.DataItem, "ID")%>);"
                                    class="list_link">
                                    <img src="../../sysImages/folder/dels.gif" border="0" alt="彻底删除" /></a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr class="TR_BG_list">
                        <td class="list_link" colspan="7">
                            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="75" align="left" valign="top">
                                        <input type="checkbox" onclick="ChooseAll(this)" />
                                        选中所有
                                    </td>
                                    <td align="right" valign="top">
                                        <input type="button" class="form" name="BnRecyle" value="删除所有已入库新闻" onclick="DeleteCllNews(0);" />
                                        <input type="button" class="form" name="BnDelete" value="入库所有未入库新闻" onclick="Transfer(0);" />
                                        <input type="button" class="form" name="BnProperty" value="入库选中新闻" onclick="Transfer(-1)" />
                                        <input type="button" class="form" name="BnMove" value="删除选中新闻" onclick="DeleteCllNews(-1)" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div style="width: 98%" align="right">
                <uc1:PageNavigator ID="PageNavigator1" runat="server" />
            </div>
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
    </form>
</body>
</html>
