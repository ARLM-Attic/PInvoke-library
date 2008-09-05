<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_Special_List" ResponseEncoding="utf-8" Codebehind="Special_List.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server"> 
  <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">专题管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />专题管理</div></td>
    </tr>
  </table>
  <table width="100%" border="0" cellpadding="3" cellspacing="1" class="Navitable" align="center">
  <tr>
    <td style="padding-left:15px;width:34%;"><a href="Special_Add.aspx" class="topnavichar">添加专题</a></td>
    <td style="padding-left:15px;"><a href="javascript:PDel();" class="topnavichar">彻底删除</a>&nbsp;┊&nbsp;<a href="javascript:PRDel();" class="topnavichar">删除到回收站</a>&nbsp;┊&nbsp;<a href="javascript:PUnlock();" class="topnavichar">批量解锁</a>&nbsp;┊&nbsp;<a href="javascript:Plock();" class="topnavichar">批量锁定</a>&nbsp;┊&nbsp;<a href="javascript:Publish();" class="topnavichar">生成静态文件</a>&nbsp;┊&nbsp;<a href="special_templet.aspx" class="topnavichar">批量捆绑模板</a> <span id="channelList" runat="server" style="display:none;"  /> </td></tr>
</table>

<asp:Repeater ID="DataList1" runat="server">
   <HeaderTemplate>
       <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
      <tr class="TR_BG">
        <td align="left" valign="middle" class="sys_topBg">专题名称</td>
        <td align="left" valign="middle" class="sys_topBg">添加时间</td>
        <td align="left" valign="middle" class="sys_topBg">状态</td>
        <td align="left" valign="middle" class="sys_topBg">专题新闻信息</td>
        <td align="left" valign="middle" class="sys_topBg">操作 <input type="checkbox" value="'-1'" name="S_ID" id="S_ID" onclick="javascript:selectAll(this.form,this.checked)" /></td>
      </tr>   
    </HeaderTemplate>
      <ItemTemplate>
       <%#((DataRowView)Container.DataItem)["Colum"]%>
      </ItemTemplate>
      <FooterTemplate>
      </table>
     </FooterTemplate>
</asp:Repeater>
<div style="width:98%;" align="right">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
</div>
<br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
</table>
</form>
</body>
<script language="javascript" type="text/javascript">
function Lock(ID)
{
    self.location="?Type=Lock&ID="+ID;
}
function UnLock(ID)
{
    self.location="?Type=UnLock&ID="+ID;
}
function Del(ID)
{
    if (confirm('你确认删除此专题\n以及此专题的子专题吗?'))
    {
        self.location="?Type=Del&ID="+ID;
    }
}
function Update(ID)
{
    self.location="Special_edit.aspx?ID="+ID;
}
function AddChild(ID)
{
    self.location="Special_add.aspx?parentID="+ID;
}

function PDel()
{
    if(confirm("你确定要彻底删除吗?\r此操作将会删除选中的专题\r以及选中专题的子专题\r删除之后将无法恢复！"))
    {
	    document.form1.action="?Type=PDel&Mode=Del";
	    document.form1.submit();
	}
}
function PUnlock()
{
    if(confirm("你确定要批量解锁吗?"))
    {
	    document.form1.action="?Type=PUnlock";
	    document.form1.submit();
	}
}
function Plock()
{
    if(confirm("你确定要批量锁定吗?\r此操作将会锁定选中的专题\r以及选中专题的子专题"))
    {
	    document.form1.action="?Type=Plock";
	    document.form1.submit();
	}
}
function PRDel()
{
    if(confirm("你确定要删除到回收站吗?\r此操作将会把选中的专题\r以及选中专题的子专题放入到回收站中\r删除之后可以从回收站中恢复！"))
    {
	    document.form1.action="?Type=PDel&Mode=Re";
	    document.form1.submit();
	}
}

function Publish()
{
    document.form1.action="?Type=Publish";
    document.form1.submit();
}

function getchanelInfo(obj)
{
   var SiteID=obj.value;
   window.location.href="special_List.aspx?SiteID="+SiteID+"";
}
</script>
</html>
