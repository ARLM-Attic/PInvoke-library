<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_unnews_iframe" Codebehind="unnews_iframe.aspx.cs" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
<!--
var IsMSIE=(navigator.appName=="Microsoft Internet Explorer");
function AddUnNewList(NewId,NewTitle){
	var ListLen=parent.UnNewArray.length;
	var FindFlag=-2;
	for (var i=0;i<ListLen;i++){
		if (parent.UnNewArray[i][0]==NewId){
			FindFlag=i;
			break;
		}
	}
	if (FindFlag>=-1){
		if (confirm("确定移除吗？")){
		    parent.UnNewArray.remove(FindFlag,1);
			parent.DisplayUnNews();
			CheckUnNews();
			parent.UnNewPreviewCh();
		}
	}else{
		parent.UnNewArray[ListLen]=[NewId,NewTitle,NewTitle,(ListLen+1),'',''];
		parent.DisplayUnNews();
		CheckUnNews();
		parent.UnNewPreviewCh();
	}
}

function CheckUnNews(){
    var ListLen=parent.UnNewArray.length;
	var FindFlag=-1;
	var buttons=document.getElementsByTagName("button");
	for (var j=0;j<buttons.length;j++){
		FindFlag=-1;
		for (var i=0;i<ListLen;i++){
			if (("New"+parent.UnNewArray[i][0])==buttons[j].id)
			{
				FindFlag=j;
				break;
			}
		}
		if (FindFlag>-1){
		    buttons[j].innerHTML="<span style=\"color:red;font-size:12px;\">删除</span>";
		}else{
		    buttons[j].innerHTML="<span style=\"font-size:12px;\">加入</span>";
		}
	}
}
window.onload=CheckUnNews;
-->
</script>
</head>
<body>
<form id="Form1" runat="server">
<div>

<table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td>筛选: <asp:LinkButton ID="LnkBtnAll" runat="server" CssClass="topnavichar" OnClick="LnkBtnAll_Click">全部新闻</asp:LinkButton>
        &nbsp;┊&nbsp; 
        <asp:LinkButton ID="LnkBtnContribute" CssClass="topnavichar" runat="server" OnClick="LnkBtnContribute_Click">投稿</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnCommend" runat="server" CssClass="topnavichar" OnClick="LnkBtnCommend_Click">推荐</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnTop" runat="server" CssClass="topnavichar" OnClick="LnkBtnTop_Click">置顶</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnHot" runat="server" CssClass="topnavichar" OnClick="LnkBtnHot_Click">热点</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnPic" runat="server" CssClass="topnavichar" OnClick="LnkBtnPic_Click">图片</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnSplendid" runat="server" CssClass="topnavichar" OnClick="LnkBtnSplendid_Click">精彩</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnHeadline" runat="server" CssClass="topnavichar" OnClick="LnkBtnHeadline_Click">头条</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnSlide" runat="server" CssClass="topnavichar" OnClick="LnkBtnSlide_Click">幻灯片</asp:LinkButton>
        </td>
  </tr>
  <tr>
    <td>
        新闻搜索:
        &nbsp;&nbsp;
        栏目:
        <asp:TextBox ID="DdlClass" runat="server" CssClass="form"  />
        <img src="../../sysImages/folder/s.gif" alt="选择栏目" border="0" style="cursor:pointer;" onclick="selectFile('newsclass',document.Form1.DdlClass,250,500);document.Form1.DdlClass.focus();" />
        &nbsp;&nbsp;
        关键字:
        <asp:TextBox runat="server" ID="TxtKeywords" CssClass="form" Columns="15" />
        &nbsp;
        <asp:DropDownList ID="DdlKwdType" runat="server" CssClass="form">
            <asp:ListItem Value="title" Text="标题" />
            <asp:ListItem Value="content" Text="内容" />
            <asp:ListItem Value="author" Text="作者" />
            <asp:ListItem Value="editor" Text="录入者" />
        </asp:DropDownList>
         &nbsp;&nbsp;
         <asp:Button runat="server" ID="BtnSearch" Text=" 搜索 " CssClass="form" OnClick="BtnSearch_Click" />
    </td>
  </tr>
</table>
<table id="tablist" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
  <tr class="TR_BG">
    <td class="sys_topBg" width="50%" align="center">新闻标题(点击)</td>
    <td class="sys_topBg" width="15%" align="center">所属栏目</td>
    <td class="sys_topBg" width="15%" align="center">编辑</td>
    <td class="sys_topBg" width="20%" align="center">操作</td>
  </tr>
 <asp:Repeater ID="RptNews" runat="server">
 <ItemTemplate>
 <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
    <td><%# DataBinder.Eval(Container.DataItem, "NewsTitle")%>(<%# DataBinder.Eval(Container.DataItem, "Click")%>)</td>
    <td style="text-align:center;"><%# DataBinder.Eval(Container.DataItem, "ClassCName")%></td>
    <td><%# DataBinder.Eval(Container.DataItem, "UserName")%></td>
    <td><button id="New<%# DataBinder.Eval(Container.DataItem, "Newsid")%>" onclick="AddUnNewList('<%# DataBinder.Eval(Container.DataItem, "Newsid")%>','<%# replacechar(DataBinder.Eval(Container.DataItem, "NewsTitle"))%>');return false;">加入</button></td>
 </tr>
 </ItemTemplate>
  </asp:Repeater>
  </table>
  <div style="width:98%" align="right"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></div>
 
<asp:Label runat="server" ID="LblChoose" Visible="false" Text=""/>
</div>
</form>
</body>
</html>