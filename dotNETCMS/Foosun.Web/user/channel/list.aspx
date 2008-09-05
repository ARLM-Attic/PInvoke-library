<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="user_channel_list" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>频道</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
   <form id="form1" runat="server">
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
              <td height="1" colspan="2"></td>
            </tr>
            <tr>
              <td width="57%" class="sysmain_navi" style="padding-left:14px;">我的信息管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="list.aspx?ChID=<%Response.Write(Request.QueryString["ChID"]); %>" class="list_link" target="sys_main">我的信息</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />列表</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;">
         <asp:DropDownList ID="gClassID" runat="server" onchange="getClass(this);">
        <asp:ListItem Value=""> - 选择栏目 - </asp:ListItem>
        </asp:DropDownList>
        <a href="content.aspx?ChID=<%Response.Write(Request.QueryString["ChID"]); %>&ClassID=<%Response.Write(Request.QueryString["ClassID"]); %>" class="reshow">添加<%Response.Write(itemname); %></a>
          &nbsp;  <a href="list.aspx?ChID=<%Response.Write(Request.QueryString["ChID"]); %>" class="topnavichar">我的<%Response.Write(itemname); %></a>
          &nbsp;  <asp:LinkButton ID="LinkButton1" OnClick="del_info" runat="server" CssClass="list_link" OnClientClick="{if(confirm('确定要删除吗?\n管理员审核通过的不能删除。')){return true;}return false;}">批量删除</asp:LinkButton>
          &nbsp;  <asp:TextBox ID="keywords" Width="120px" runat="server"></asp:TextBox><input type="button" value="搜索" onclick="gets();" />
          </td>
      </tr>
      </table>
    <asp:Repeater ID="DataList1" runat="server">
       <HeaderTemplate>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
            <tr class="TR_BG">
            <td class="sys_topBg" style="width:200px">标题</td>
            <td class="sys_topBg" style="text-align:center;">添加日期</td>
            <td class="sys_topBg" style="width:120px;text-align:center;">状态</td>
            <td class="sys_topBg" style="width:120px;text-align:center;">用户锁定</td>
            <td class="sys_topBg" style="width:120px;text-align:center;">操作<input name="Checkboxc" type="checkbox" onclick="javascript:selectAll(this.form,this.checked);" /></td>
            </tr>
        </HeaderTemplate>
          <ItemTemplate>
            <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
            <td align="left" valign="middle"><%#((DataRowView)Container.DataItem)["titles"]%></td>
            <td align="left" valign="middle"><%#((DataRowView)Container.DataItem)["creatTime"]%></td>
            <td align="left" valign="middle" style="text-align:center;"><%#((DataRowView)Container.DataItem)["isLocks"]%></td>
            <td align="left" valign="middle" style="text-align:center;"><%#((DataRowView)Container.DataItem)["ConstrTFs"]%></td>
            <td align="left" valign="middle" style="text-align:center;"><%#((DataRowView)Container.DataItem)["op"]%><input type="checkbox" name="infoID" value="<%#((DataRowView)Container.DataItem)["id"]%>"></td>
            </tr>
          </ItemTemplate>
          <FooterTemplate>
          </table>
         </FooterTemplate>
    </asp:Repeater> 
    <div style="text-align:right"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></div>
    <br />
    <br />
     <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><%Response.Write(CopyRight); %> </td>
       </tr>
</table>
</form>
</body>
</html>
<script language="javascript" type="text/javascript">
function getClass(obj)
{
   window.location.href="list.aspx?ClassID="+obj.value+"&ChID=<%Response.Write(Request.QueryString["ChID"]); %>&keywords=<%Response.Write(Request.QueryString["keywords"]); %>";
}
function gets()
{
   var keywords = document.getElementById("keywords");
   window.location.href="list.aspx?ClassID=<%Response.Write(Request.QueryString["ClassID"]); %>&ChID=<%Response.Write(Request.QueryString["ChID"]); %>&keywords="+keywords.value+"";
}
</script>