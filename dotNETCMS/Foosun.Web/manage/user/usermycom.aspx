<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_mycom"  ResponseEncoding="utf-8" Codebehind="usermycom.aspx.cs" %>
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
<body><form id="form1" runat="server">
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" class="sysmain_navi" style="padding-left:14px;">评论管理</td>
          <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="#" class="list_link" target="sys_main">评论管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />评论列表</td>
        </tr>
    </table>
    
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><span><span id="sc" runat="server"></span><span id="channelList" runat="server" /></span></td>
      </tr>
    </table>
           <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable" style="display:none;" id="searchuserID">
      <tr>
      <td width="14%">用户名：<asp:TextBox ID="UserNumbox" runat="server" Width="83px" CssClass="form"></asp:TextBox></td>
        <td width="14%">标题：<asp:TextBox ID="Title1" runat="server" Width="104px" CssClass="form"></asp:TextBox></td>
        <td width="13%">
            <%--类型：<asp:DropDownList ID="APIIDTitle1" runat="server" Width="124px" CssClass="form"></asp:DropDownList>--%></td>
        <td width="20%">被评论信息：<asp:TextBox ID="InfoTitle1" runat="server" Width="123px" CssClass="form"></asp:TextBox></td>
       </tr>
             <tr>
        <td width="11%">
            审核：<asp:DropDownList ID="isCheck1" runat="server" Width="98px" CssClass="form">
                <asp:ListItem Value="0">请选择</asp:ListItem>
                <asp:ListItem Value="1">否</asp:ListItem>
                <asp:ListItem Value="2">是</asp:ListItem>
            </asp:DropDownList></td>
        <td width="11%">
            锁定：<asp:DropDownList ID="islock1" runat="server" Width="79px" CssClass="form">
                <asp:ListItem Value="0">请选择</asp:ListItem>
                <asp:ListItem Value="1">否</asp:ListItem>
                <asp:ListItem Value="2">是</asp:ListItem>
            </asp:DropDownList></td>
        <td width="26%">日期： <asp:TextBox ID="creatTime1" runat="server" Width="106px" CssClass="form" onclick="selectFile('date',document.form1.creatTime1,160,500);document.form1.creatTime1.focus();"></asp:TextBox>--<asp:TextBox ID="creatTime2" runat="server" Width="105px" CssClass="form" onclick="selectFile('date',document.form1.creatTime2,160,500);document.form1.creatTime2.focus();"></asp:TextBox></td>
        <td width="5%"><input type="button" name="Submit" value="搜索" runat="server" class="form" id="Button1" onserverclick="Button8_ServerClick" style="width: 58px"/></td>
       </tr>
    </table>    
      <span id="no" runat="server"></span>  
 <table width="98%" align="center">
 <tr><td colspan="7">
<div>   
    <asp:Repeater ID="DataList1" runat="server">
       <HeaderTemplate>
        <table width="100%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
            <tr class="TR_BG">
            <td class="sys_topBg" align="center" width="2%"></td>
            <td class="sys_topBg" align="left" width="50%">标题</td>
            <td class="sys_topBg" align="center">类型</td>
            <td class="sys_topBg" align="center">被评论信息</td>
            <td class="sys_topBg" align="center">锁定</td>
            <td class="sys_topBg" align="center">操作<input type="checkbox" name="Checkbox122" onclick="javascript:selectAll(this.form,this.checked)" /></td>
            </tr>
        </HeaderTemplate>
          <ItemTemplate>
            <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
            <td align="center" ><%#((DataRowView)Container.DataItem)["OrderIDs"]%></td>
            <td align="left"><%#((DataRowView)Container.DataItem)["Content"]%>&nbsp;(<%#((DataRowView)Container.DataItem)["UserNames"]%>)&nbsp;<%#((DataRowView)Container.DataItem)["GoodTitles"]%>
            <br />
            <span style="color:#999999;font-size:10px;"><%#((DataRowView)Container.DataItem)["creatTime"]%></span>
            </td>
            <td align="center"><%#((DataRowView)Container.DataItem)["APIIDTitle"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["InfoTitle"]%>
            </td>
            <td align="center"><%#((DataRowView)Container.DataItem)["islocks"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["Operation"]%></td>
            </tr>
          </ItemTemplate>
          <FooterTemplate>
          </table>
         </FooterTemplate>
    </asp:Repeater> 
</div>
</td></tr>
<tr>
<td><asp:Button ID="TopTitle1" runat="server" Text="固顶" CssClass="form" Height="20px" Width="60px" OnClick="TopTitle1_Click"/>
&nbsp;&nbsp;<asp:Button ID="TopTitle12" runat="server" Text="解固" Height="20px" Width="60px"  CssClass="form" OnClick="TopTitle12_Click"/>
&nbsp;&nbsp;<asp:Button ID="GoodTitle" runat="server" Text="精华" Height="20px" Width="60px" CssClass="form" OnClick="GoodTitle_Click"/>
&nbsp;&nbsp;<asp:Button ID="UNGoodTitle" runat="server" Text="取消精华" Height="20px" Width="60px" CssClass="form" OnClick="unGoodTitle_Click"/>
<span style="display:none;">
&nbsp;&nbsp;<asp:Button ID="CheckTtile" runat="server" Text="审核" Height="20px" Width="60px" CssClass="form" OnClick="CheckTtile_Click"/>
&nbsp;&nbsp;<asp:Button ID="Button3" runat="server" Text="取消审核" Height="20px" Width="60px" CssClass="form" OnClick="unCheckTtile_Click"/>
</span>
&nbsp;&nbsp;<asp:Button ID="OCTF1" runat="server" Text="锁定" Height="20px" Width="60px" CssClass="form" OnClick="OCTF1_Click"/>
&nbsp;&nbsp;<asp:Button ID="OCTF2" runat="server" Text="解锁" Height="20px" Width="60px" CssClass="form" OnClick="OCTF2_Click"/>
&nbsp;&nbsp;<asp:Button ID="Button4" runat="server" Text="删除" Height="20px" Width="60px" CssClass="form" OnClientClick="PDel();"/></td>
</tr>
<tr>
<td align="right" colspan="6">   
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
</td>
</tr>
</table>  
<br />
<br />
 <table width="100%" border="0" cellpadding="5" cellspacing="0" class="copyright_bg" style="height: 72px">
   <tr>
     <td align="center" style="height: 60px"><label id="copyright" runat="server" /></td>
   </tr>
</table>
</form>
</body>
<script language="javascript" type="text/javascript">
function del(ID)
{
   if(confirm("你确定要删除吗?"))
   { 
        self.location="?Type=del&ID="+ID;
   }
}
function PDel()
{
    if(confirm("你确定要彻底删除吗?"))
    {
	    document.form1.action="?Type=PDel";
	    document.form1.submit();
	}
}
function opencats()
{
  if(document.getElementById("searchuserID").style.display=="none")
  {
     document.getElementById("searchuserID").style.display="";
  } else {
     document.getElementById("searchuserID").style.display="none";
  }
}
function getchanelInfo(obj)
{
var SiteID=obj.value;
window.location.href="usermycom.aspx?SiteID="+SiteID+"";
}
</script>
</html>