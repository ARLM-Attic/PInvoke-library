<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_user_pointhistory" Codebehind="pointhistory.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
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
              <td width="57%" class="sysmain_navi" style="padding-left:14px;">冲值记录</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />冲值记录</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><span id="dels" runat="server"></span><span id="channelList" runat="server" /></td>
      </tr>
    </table> 
    <div id="no" runat="server"></div>
<asp:Repeater ID="userlists" runat="server">
   <HeaderTemplate>
       <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
        <tr class="TR_BG">
        <td class="sys_topBg" align="center" width="15%">用户名</td>
        <td class="sys_topBg" align="center" width="10%">收入/支出</td>
        <td class="sys_topBg" align="center" width="10%">G币</td>
        <td class="sys_topBg" align="center" width="10%">积分</td>   
        <td class="sys_topBg" align="center" width="10%">现金</td> 
        <td class="sys_topBg" align="center" width="18%">说明</td>              
        <td class="sys_topBg" align="center" width="17%">操作日期</td>
        <td class="sys_topBg" align="center" width="10%">操作<input type="checkbox" value="-222" onclick="selectAll(this.form,this.checked);" /></td>
        </tr>
    </HeaderTemplate>
      <ItemTemplate>
        <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td align="center" valign="middle"><%#((DataRowView)Container.DataItem)["UserName"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["ghtypes"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["Gpoint"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["iPoint"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["Moneys"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["content"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["CreatTime"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["op"]%></td>
        </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
     </FooterTemplate>
</asp:Repeater> 

<table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
   <tr><td>
       用户名：<asp:TextBox ID="UserNameBox" runat="server" Width="91px" CssClass="form"></asp:TextBox>&nbsp;&nbsp;
       <asp:DropDownList ID="DropDownList1" runat="server" Width="102px">
       <asp:ListItem Value="0">全部交易</asp:ListItem>
       <asp:ListItem Value="2">在线冲值</asp:ListItem>
       <asp:ListItem Value="3">积分兑换</asp:ListItem>
       <asp:ListItem Value="4">稿酬</asp:ListItem>
       <asp:ListItem Value="5">阅读权限</asp:ListItem>
       <asp:ListItem Value="1">捐献</asp:ListItem>
       <asp:ListItem Value="6">登录获得</asp:ListItem>
       <asp:ListItem Value="7">注册获得</asp:ListItem>
       <asp:ListItem Value="8">收入</asp:ListItem>
       <asp:ListItem Value="9">支出</asp:ListItem>
   </asp:DropDownList>&nbsp;
       <asp:Button ID="selbut" runat="server" Text="搜　索" OnClick="selbut_Click" CssClass="form"/></td>
     <td align="right">
         <uc1:PageNavigator ID="PageNavigator1" runat="server" /></td>
   </tr>
</table>
<br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
</table>
</form>
</body>
<script language="javascript" type="text/javascript">
function del(ID)
{
   if(confirm("你确定要删除吗?"))
   { 
        self.location="?Types=del&ID="+ID;
   }
}
function PDel()
{
    if(confirm("你确定要彻底删除吗?"))
    {
	    document.form1.action="?Types=PDel";
	    document.form1.submit();
	}
}
function getchanelInfo(obj)
{
   var SiteID=obj.value;
   window.location.href="?SiteID="+SiteID+"";
}
</script>
</html>
