<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_user_message" Codebehind="message.aspx.cs" %>
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
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
              <td height="1" colspan="2"></td>
            </tr>
            <tr>
              <td width="57%"  class="sysmain_navi" style="padding-left:14px;" >短消息管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />短消息</td>
            </tr>
    </table>
    <table id="Table1" width="98%" align="center"  border="0" cellpadding="5" cellspacing="1" class="table">
            <tr>
              <td class="TR_BG_list" style="height:35px;">
              <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="form" Text="清空所有的垃圾短信" />&nbsp; &nbsp;
                  <asp:CheckBox ID="CheckBox22" Text="同时清除用户废件箱中的短消息" runat="server" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_clearmessage_0001',this)">清理原则?</span>
              </td>
            </tr>
    </table>    <br />
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><label id="copyright" runat="server" /></td>
       </tr>
    </table>
    </form>
</body>
</html>
