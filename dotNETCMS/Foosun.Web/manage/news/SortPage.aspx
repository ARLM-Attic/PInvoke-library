<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_SortPage" Codebehind="SortPage.aspx.cs" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js">
</script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>

<body>
<form id="server" runat=server>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px; height: 32px;" >栏目管理<span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onclick="Help('H_news__0001',this)">(帮助)</span></td>
          <td width="43%" class="topnavichar"  style="PADDING-LEFT: 14px; height: 32px;" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Class_List.aspx" class="topnavichar">栏目管理</a></div></td>
        </tr>
</table>
      <table width="100%" border="0" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td>
              &nbsp;&nbsp;<a href="Class_List.aspx" class="topnavichar">栏目首页</a> </td>
        </tr>
</table>
<div>
    
    <table width="98%" border="0" cellpadding="8" cellspacing="0" bgcolor="#FFFFFF" class="table" style="height: 76px; display:none;" id="uniteTable">
      <tr>
        <td height="52" class="TR_BG_list">           
            <table width="500" border="0" align="center" cellspacing="0" cellpadding="0" style="height: 83px">
              <tr>
                <td style="height: 22px">源栏目</td>
                <td style="height: 22px">目标栏目</td>
              </tr>
              <tr>
                <td height="28"><asp:DropDownList ID="SourceClassID" runat="server" Width="122px"> </asp:DropDownList></td>
                <td>
                    <asp:Label ID="ExprText" runat="server"></asp:Label>
                    <asp:DropDownList ID="TargetClassID" runat="server" Width="98px">
                    <asp:ListItem Value="0">根栏目</asp:ListItem>
                    </asp:DropDownList></td>
              </tr>
              <tr>
                <td height="23" colspan="2" align="center">
                    <br />
                    <asp:Button ID="Btc" runat="server" OnClick="Btc_Click" OnClientClick="{if(confirm('确认此操作吗?\n此操作后不可以恢复数据!')){return true;}return false;}" />
                    <asp:HiddenField ID="Randsize" runat="server" />
                    <br />
                </td>
              </tr>
            </table>
            
            </td>
      </tr>
    </table>

   <asp:Repeater ID="DataList1" runat="server" OnItemCommand="DataList1_ItemCommand">
   <HeaderTemplate>
       <table width="98%" border="0" cellpadding="4" align="center" cellspacing="1" bgcolor="#FFFFFF" class="table">
      <tr class="TR_BG_list">
        <td width="7%" align="center" valign="middle" class="sysmain_navi">ID</td>
        <td width="29%" valign="middle" class="sysmain_navi">栏目中文[英文]</td>
        <td width="9%" valign="middle" class="sysmain_navi">权重</td>
        <td width="28%" valign="middle" class="sysmain_navi"></td>
        <td width="27%" valign="middle" class="sysmain_navi">操作</td>
      </tr>   
    </HeaderTemplate>
      <ItemTemplate>
        <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td width="7%" align="center" valign="middle" height=20><%#((DataRowView)Container.DataItem)[0]%></td>
        <td width="29%" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%>[<%#((DataRowView)Container.DataItem)[3]%>]</td>
        <td width="9%" valign="middle" ><%#((DataRowView)Container.DataItem)[5]%></td>
        <td width="28%" valign="middle" >
            <asp:TextBox ID="TextBox1" runat="server" Text="10"></asp:TextBox><asp:HiddenField
                ID="HiddNum" runat="server" Value="<%#((DataRowView)Container.DataItem)[1]%>" />
        </td>
        <td width="27%" valign="middle" >
            <asp:Button ID="Button1" runat="server" Text="更改权重(排序号)" /></td>            
        </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
      </FooterTemplate>
  </asp:Repeater>
    <div align="right" style="width:98%"><uc1:PageNavigator ID="PageNavigator1" runat="server"/></div>
</div>
</form>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><%Response.Write(CopyRight); %></td>
   </tr>
 </table>
</body>
</html>
