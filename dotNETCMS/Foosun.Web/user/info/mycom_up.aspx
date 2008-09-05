<%@ Page Language="C#" AutoEventWireup="true" Inherits="mycom_up" Codebehind="mycom_up.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" name="form1" method="post" action="" runat="server">
  <table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >评论管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="mycom.aspx" class="menulist">评论管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改评论</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="mycom.aspx" class="menulist">评论管理</a></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  <tr class="TR_BG_list" style="display:none;">
    <td class="list_link" width="25%" style="text-align: right" >
        评论标题：</td>
      <td class="list_link" width="75%">
          <asp:TextBox ID="TitleBox" runat="server" Width="280px" CssClass="form"></asp:TextBox>&nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('usermycom_up_0001',this)">帮助</span>
         </td>
 </tr>
    <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        评论内容：</td>
      <td class="list_link">
          <asp:TextBox ID="ContentBox" runat="server" Height="150px" TextMode="MultiLine" Width="500px" CssClass="form"></asp:TextBox>&nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('usermycom_up_0002',this)">帮助</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="评论内容不能为空" ControlToValidate="ContentBox"></asp:RequiredFieldValidator></td>
 </tr>
    <tr class="TR_BG_list">
        <td class="list_link" colspan="2" align="center">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 100px; height: 22px;">
                        <asp:Button ID="sumbitsave" runat="server" CssClass="form" Text=" 确 定 "  OnClick="shortCutsubmit" /></td>
                    <td style="width: 26px; height: 22px;">
                    </td>
                    <td style="width: 100px; height: 22px;">
                        <input id="reset" type="reset" value="重 置" class="form" style="width: 74px"/></td>
                </tr>
            </table>
        </td>
 </tr>
</table>
<br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table>
</form>
</body>
</html>
