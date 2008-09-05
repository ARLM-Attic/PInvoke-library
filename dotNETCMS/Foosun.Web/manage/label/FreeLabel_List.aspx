<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_FreeLabel_List" Codebehind="FreeLabel_List.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
<!--
function DeleteFreeLabel(id)
{
    if(window.confirm("您确定要删除该自由标签吗,数据将不能恢复?"))
    {
        var  options={
            method:'post',
            parameters:"Option=DeleteFreeLabel&ID="+ id,
            onComplete:
                function(transport)
	            {
	                var rtstr =transport.responseText;
	                OnDelete(rtstr);
	            } 
	        }
	    new  Ajax.Request('FreeLabel_List.aspx',options);
    }
}
function OnDelete(rtstr)
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
<form id="Form1" runat="server">
<div>
<table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >自由标签管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />自由标签管理</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td>功能：<a class="topnavichar" href="FreeLabel_Add.aspx">新建自由标签</a></td>
  </tr>
</table>
<asp:Repeater runat="server" ID="RptFreeLabel">
<HeaderTemplate>
<table id="tablist" width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
  <tr class="TR_BG">
    <td class="sys_topBg" width="20%" align="center">标签名称</td>
    <td class="sys_topBg" width="20%" align="center">建立日期</td>
    <td class="sys_topBg" width="45%" align="center">描述</td>
    <td class="sys_topBg" width="15%" align="center">操作</td>
  </tr>
 </HeaderTemplate>
 <ItemTemplate>
 <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
    <td class="list_link"><%# DataBinder.Eval(Container.DataItem, "LabelName")%></td>
    <td class="list_link" align="center"><%# DataBinder.Eval(Container.DataItem, "CreatTime")%></td>
    <td class="list_link"><%# DataBinder.Eval(Container.DataItem, "Description")%></td>
    <td class="list_link" align="center"><a class="list_link" href="FreeLabel_Add.aspx?id=<%# DataBinder.Eval(Container.DataItem, "Id")%>"><img src="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/sysico/edit.gif" border="0" alt="修改" /></a> <a class="list_link" href="javascript:DeleteFreeLabel(<%# DataBinder.Eval(Container.DataItem, "Id")%>)"><img src="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/sysico/del.gif" border="0" alt="彻底删除" /></a></td>
 </tr>
 </ItemTemplate>
 <FooterTemplate>
  </table>
  </FooterTemplate>
  </asp:Repeater>
  <div style="width:98%" align="right"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></div>
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"> <%Response.Write(CopyRight);%></td>
  </tr>
</table>
</div>
</form>
</body>
</html>
