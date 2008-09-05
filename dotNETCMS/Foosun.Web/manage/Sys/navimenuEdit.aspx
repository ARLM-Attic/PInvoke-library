<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_navimenuEdit" Codebehind="navimenuEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<table width="100%" height="32" align="center" border="0" cellpadding="0" cellspacing="0" background="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/reght_1_bg_1.gif">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >创建功能菜单</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" >位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />创建功能菜单</td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a class="topnavichar" href="Navimenu_list.aspx">管理功能菜单</a>　<a class="topnavichar" href="Navimenu.aspx">创建功能菜单</a></span></td>
        </tr>
      </table>
      <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
        <form id="form1" runat="server"><tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">菜单名称</div></td>
          <td class="list_link"><asp:TextBox ID="menuName" runat="server"  Width="250"  MaxLength="50"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_navimenu_0001',this)">帮助</span><asp:RequiredFieldValidator ID="f_menuName" runat="server" ControlToValidate="menuName" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写菜单名称，长度为20字节</span>"></asp:RequiredFieldValidator></td>
        </tr>                                                                                                                                                                                                                                                                                             
        <tr class="TR_BG_list"  id="parent_ID" runat="server">
          <td class="list_link" style="width: 114px"><div align="right">父菜单名</div></td>
          <td class="list_link"><label id="parentIDs" runat="server" />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onClick="Help('H_navimenu_0002',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list"  id="position_ID" runat="server">
          <td class="list_link" style="width: 114px"><div align="right">位置标识</div></td>
          <td class="list_link"><input id="position" name="position" style="width:250px" maxlength="5" value="99999" runat=server />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onClick="Help('H_navimenu_0003',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list"  id="type_ID" runat="server">
          <td class="list_link" style="width: 114px; height: 26px;"><div align="right">菜单类型</div></td>
          <td class="list_link" style="height: 26px">
             <asp:DropDownList ID="type" runat="server">
            </asp:DropDownList><span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onClick="Help('H_navimenu_0004',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list" id="isys_ID" runat="server">
          <td class="list_link" style="width: 114px; height: 23px;"><div align="right">系统功能</div></td>
          <td class="list_link" style="height: 23px"><asp:CheckBox ID="isSys" runat="server" /><span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onClick="Help('H_navimenu_0005',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list" id="link_ID" runat="server">
          <td class="list_link" style="width: 114px"><div align="right">连接路径</div></td>
          <td class="list_link"><asp:TextBox ID="FilePath" runat="server" Width="250" MaxLength="200"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onClick="Help('H_navimenu_0006',this)">帮助</span><asp:RequiredFieldValidator ID="f_FilePath" runat="server" ControlToValidate="FilePath" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写快捷菜单连接路径，长度为200字节</span>"></asp:RequiredFieldValidator></td>
        </tr>
        <tr class="TR_BG_list" id="target_ID" runat="server">
          <td class="list_link" style="width: 114px"><div align="right">打开窗口</div></td>
          <td class="list_link"><asp:TextBox ID="f_target" runat="server"></asp:TextBox><span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onClick="Help('H_navimenu_0008',this)">帮助</span><asp:RequiredFieldValidator ID="f_f_target" runat="server" ControlToValidate="f_target" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写快捷菜单连接路径，长度为200字节</span>"></asp:RequiredFieldValidator></td>
        </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">排列顺序</div></td>
          <td class="list_link">
          <asp:TextBox ID="orderID" runat="server" MaxLength="2" value="0" Width="100" />
          <asp:HiddenField ID="am_id" runat="server" />
          <asp:HiddenField ID="Hiddenissys" runat="server" />
          <span class="helpstyle" style="cursor:help"; title="点击查看帮助"  onclick="Help('H_navimenu_0007',this)">帮助</span><asp:RequiredFieldValidator ID="f_orderID_1" runat="server" ControlToValidate="orderID" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写序号</span>"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="f_orderID" runat="server" ControlToValidate="orderID"  Display="Static" ErrorMessage="(*)排列序号不正确" ValidationExpression="^[0-9]{0,2}"></asp:RegularExpressionValidator></td>
        </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">权限代码</div></td>
          <td class="list_link">
          <asp:TextBox ID="popCode" runat="server" MaxLength="50" Width="100" />
          <span class="helpstyle" style="cursor:help"; title="点击查看帮助"  onclick="Help('H_navimenu_pop',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 28px;">&nbsp;</td>
          <td class="list_link" style="height: 28px">&nbsp;<asp:Button ID="sumbitsave" runat="server" CssClass="form" Text=" 修　改 " OnClick="naviedit" />
            <input name="reset" type="reset" value=" 重 置 "  class="form">          </td>
        </tr>
</form>
</table>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat=server /></td>
   </tr>
 </table>
<script language="JavaScript" type="text/javascript">
function changevalue(value)
{
	if(value=='0')
	{
		form1.position.value="99999";
	}
	else
	{
		form1.position.value="88888";
	}
}
</script>
</html>
