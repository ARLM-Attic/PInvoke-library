<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_js_JS_List" Codebehind="JS_List.aspx.cs" %>
<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
<!--
function GetJSCode(id)
{
    var WWidth = (window.screen.width-500)/2;
    var Wheight = (window.screen.height-150)/2-50;
    window.open ('JS_GetCode.aspx?JSID='+id, '获取JS', 'height=165px, width=400px,toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no,top='+Wheight+', left='+WWidth+', status=no'); 
}
function ShowNewsJs(id)
{
    var WWidth = (window.screen.width-500)/2;
    var Wheight = (window.screen.height-150)/2-50;
    window.open ('JS_Files.aspx?JSID='+id, '查看新闻JS', 'height=400px, width=500px,toolbar=no, menubar=no, scrollbars=yes,top='+Wheight+', left='+WWidth+', resizable=no,location=no, status=no'); 
}
function DeleteJS(n)
{
    var ids = n;
    if(n < 0)
    {
        ids = GetSelected();
        if(ids == '')
        {
            alert('您没有选中任何JS');
            return;
        }
    }
    if(window.confirm("您确认要删除选中的JS吗?数据将不可恢复!"))
    {
        var  options={
        method:'post',
        parameters:"Option=DeleteJS&JSID="+ ids,
        onComplete:
            function(transport)
            {
                var retv =transport.responseText;
                OnDelete(retv);
            }
        }
        new  Ajax.Request('JS_List.aspx',options);
    }
}
function GetSelected()
{
    var ret = '';
    var tab = document.getElementById('TabData');
    for(var i = 1;i<tab.rows.length;i++)
    {
        var td = tab.rows[i].cells[5];
        for(var j = 0;j<td.childNodes.length;j++)
        {
            var obj = td.childNodes[j];
            if(obj.type == 'checkbox')
            {
                if(obj.checked)
                {
                    if(ret != '')
                        ret += ',';
                    ret += obj.value;
                }
                break;
            }
        }
    }
    return ret;
}
function OnDelete(retv)
{
    var n = retv.indexOf('%');
    alert(retv.substr(n+1,retv.length-n-1));
    if(parseInt(retv.substr(0,n)) > 0)
    {
        __doPostBack('PageNavigator1$LnkBtnGoto','');
    }
}
function JsPublic()
{
    var stat=document.getElementById("publicStat");
    stat.innerHTML="正在更新js...";
    var ids=GetSelected();
    if(ids=="")
    {
        alert("至少您要选择一条JS");
        return;
    }
    var options={
        method:'get',
        parameters:"ids="+ ids,
        onComplete:
            function(transport)
            {
                var retv =transport.responseText;
                stat.innerHTML="";
                alert(retv);
            }
        }
        new  Ajax.Request('JS_Publish.aspx',options);
}
//-->
</script>
</head>
<body>
    <form id="form1" runat="server">
     <!------头部导航------>
  <table width="100%" border="0" cellpadding="0" cellspacing="0"class="toptable" id="toptb1">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px">新闻JS管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" >位置：<a href="../main.aspx" class="navi_link">首页</a> <img alt="" src="../../sysImages/folder/navidot.gif" border="0" /> JS管理</td>
    </tr>
  </table>
  <!----功能菜单----->
  <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
    <tr>
      <td style="padding-left:14px;">功能： <a href="JS_Add.aspx" class="topnavichar">新增</a>
      &nbsp;┊&nbsp;
      <a href="javascript:DeleteJS(-1);" class="topnavichar">删除选中</a>
      &nbsp;┊&nbsp;
      <asp:LinkButton ID="LnkBtnALL" runat="server" CssClass="topnavichar" OnClick="LnkBtnALL_Click">所有JS</asp:LinkButton>
      &nbsp;┊&nbsp;
      <asp:LinkButton ID="LnkBtnSys" runat="server" CssClass="topnavichar" OnClick="LnkBtnSys_Click">系统JS</asp:LinkButton>
      &nbsp;┊&nbsp;
      <asp:LinkButton ID="LnkBtnFree" runat="server" CssClass="topnavichar" OnClick="LnkBtnFree_Click">自由JS</asp:LinkButton>
      &nbsp;┊&nbsp;
      <span class="topnavichar"><a href="javascript:JsPublic();"  class="topnavichar">发布JS</a></span><span id="publicStat" style="color:Red;"></span>
      </td>
    </tr>
  </table>
  <asp:Table ID="TabData" runat="server" Width="98%" BorderWidth="1" CssClass="table" CellPadding="5" CellSpacing="1" HorizontalAlign="center">
  <asp:TableRow CssClass="TR_BG">
  <asp:TableCell Width="30%" HorizontalAlign="center" CssClass="sys_topBg">名称</asp:TableCell>
  <asp:TableCell Width="10%" HorizontalAlign="center" CssClass="sys_topBg">类型</asp:TableCell>
  <asp:TableCell Width="10%" HorizontalAlign="center" CssClass="sys_topBg">代码</asp:TableCell>
  <asp:TableCell Width="15%" HorizontalAlign="center" CssClass="sys_topBg">新闻条数</asp:TableCell>
  <asp:TableCell Width="20%" HorizontalAlign="center" CssClass="sys_topBg">创建时间</asp:TableCell>
  <asp:TableCell Width="15%" HorizontalAlign="center" CssClass="sys_topBg">操作 <input type="checkbox" id="js_checkbox" value="-1" name="js_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></asp:TableCell>
  </asp:TableRow>
  </asp:Table>
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
  </div>
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><%Response.Write(CopyRight);%></td>
  </tr>
</table>
<asp:HiddenField ID="HidType" runat="server" />
</form>
</body>
</html>
