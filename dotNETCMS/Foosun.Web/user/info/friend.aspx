<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_friend_friend" Codebehind="friend.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import Namespace="System.Data" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
  <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">友情连接</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img src="../../sysImages/folder/navidot.gif" alt="" />友情链接管理</div></td>
    </tr>
  </table>
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
    <tr>
      <td align="left" class="topnavichar" style="PADDING-LEFT: 14px"><a href="friend.aspx" class="topnavichar">管理我的连接</a>&nbsp;&nbsp;&nbsp;<a href="?type=add" class="topnavichar">添加连接</a></td>
    </tr>
  </table>
  <div id="sH" runat="server">
  <asp:Repeater ID="DataList1" runat="server">
    <HeaderTemplate>
    <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
      <tr class="TR_BG">
        <td width="7%" align="center" valign="middle" class="sys_topBg">编号</td>
        <td width="10%" align="center" valign="middle" class="sys_topBg">站点</td>
        <td width="10%" align="center" valign="middle" class="sys_topBg">类别</td>
        <td width="9%" align="center" valign="middle" class="sys_topBg">类型</td>
        <td width="9%" align="center" valign="middle" class="sys_topBg">总站采用</td>
      </tr>
      </HeaderTemplate>
      
      <ItemTemplate>
      <tr class="TR_BG_list">
        <td width="7%" align="center" valign="middle" height=20><%#((DataRowView)Container.DataItem)[0]%></td>
        <td width="10%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[1]%></td>
        <td width="10%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["class"]%></td>
        <td width="9%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["type"]%>
        <td width="9%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["lock"]%>
      </tr>
      </ItemTemplate>
      
      <FooterTemplate>
    </table>
    </FooterTemplate>
  </asp:Repeater>
    <div id="NoContent" runat="server" />
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
  </div>
  </div>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table" style="display:none" id="OK">
    <tr class="TR_BG_list">
      <td colspan="2" class="list_link">友情链接申请</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right" class="list_link">阅读申请须知</td>
      <td align="left" class="list_link"><asp:CheckBox ID="isread" runat="server" />
        我已经阅读 　查看阅读须知
        <asp:CheckBox ID="ViewC" runat="server"  onclick="DispChange()"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UFriendLink_0020',this)">帮助</span></td>
    </tr>
    <tr id="knowContent" style="display:none;" class="TR_BG_list">
      <td align="right" class="list_link">申请须知</td>
      <td align="left" class="list_link"><label id="Know" runat="server" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right" class="list_link">类别</td>
      <td align="left" class="list_link"><asp:DropDownList ID="Selectclass" runat="server" CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UFriendLink_0001',this)">帮助<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Selectclass"
                ErrorMessage="类别不能为空"></asp:RequiredFieldValidator></span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right" class="list_link">站点名称</td>
      <td align="left" class="list_link"><asp:TextBox ID="Name" runat="server" CssClass="form"/>
        <font color="red">(*)</font><asp:RequiredFieldValidator ID="Namee" runat="server" ControlToValidate="Name" Display="Dynamic" ErrorMessage="<span class=reshow>站点名称不能为空!</span>"></asp:RequiredFieldValidator><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UFriendLink_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link"> 连接类型</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="LinkType" runat="server" CssClass="form" onchange="Select(this.value)">
          <asp:ListItem Selected="True" Value="1">文字</asp:ListItem>
          <asp:ListItem Value="0">图片</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UFriendLink_0003',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right" class="list_link">链接地址</td>
      <td align="left" class="list_link"><asp:TextBox ID="Url" runat="server" CssClass="form"/>
       <font color="red">(*)</font><asp:RequiredFieldValidator ID="urll" runat="server" ControlToValidate="Url" Display="Dynamic" ErrorMessage="<span class=reshow>链接地址不能为空!</span>"></asp:RequiredFieldValidator><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UFriendLink_0004',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 站点说明：</td>
      <td  align="left" class="list_link"><textarea id="ContentFri" runat="server" style="width: 240px; height: 104px" class="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_FriendLink_0005',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list" id="pis" style="display:none">
      <td align="right"  class="list_link" style="width: 214px">图片地址</td>
      <td align="left" class="list_link"><asp:TextBox ID="PicUrll" runat="server" CssClass="form"/> 
        <img src="../../sysImages/folder/s.gif" border="0" alt="选择图片" onclick="selectFile('user_pic',document.form1.PicUrll,280,500);document.form1.PicUrll.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_FriendLink_0006',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list" style="display:none;">
      <td align="right" class="list_link">申请人作者(编号)</td>
      <td align="left" class="list_link"><asp:TextBox ID="Author" runat="server" Enabled="false" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UFriendLink_0006',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right" class="list_link">申请人电子邮件</td>
      <td align="left" class="list_link"><asp:TextBox ID="Mail" runat="server" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UFriendLink_0007',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td class="list_link"><div align="right">申请理由</div></td>
      <td class="list_link"><textarea rows="5" id="ContentFor" runat="server" style="width: 240px; height: 104px" class="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_FriendLink_0012',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="SaveAle" value=" 提 交 " class="form" id="addFriend" runat="server" onserverclick="addFriend_ServerClick" />
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="ClearAle" value=" 重 填 " class="form" id="addFriendc" runat="server" />
        </label></td>
    </tr>
  </table>
</form>
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><%Response.Write(CopyRight); %> </td>
  </tr>
</table>
</body>
<script language="javascript">
function DispChange()
{
    var obj = document.getElementById("ViewC").checked;
    if(obj)
    {
      document.getElementById("knowContent").style.display="";
    }
    else
    {
      document.getElementById("knowContent").style.display="none";
    }
}
var ty = '<% Response.Write(Request.QueryString["type"]); %>';
if(ty=="add")
{
    document.getElementById("OK").style.display="";
    document.getElementById("sH").style.display="none";
}
function Select(value)
{
    switch(parseInt(value))
    {
        case 1:
        document.getElementById("pis").style.display="none";
        break;
        case 0:
        document.getElementById("pis").style.display="";
        break;
    }
}
</script>
</html>
