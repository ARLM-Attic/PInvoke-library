<%@ Page Language="C#" AutoEventWireup="true" Inherits="help_help" Codebehind="help.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
<title>帮助</title>
<link href="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" /></head>
<body>
    <form id="form1" runat="server">
    <div>
     <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
        <tr class="TR_BG">
          <td align="center" class="navi_link">简体中文帮助文档</td>
        </tr>
      </table>
      <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG_list">
          <td align="left" class="sys_topBg" colspan="2" style="height:25px;"><span id="title" runat="server" />(编号:<span id="helpid" runat="server" />)</td>
        </tr>
        <tr class="TR_BG_list">
          <td align="left" style="height:50px;" valign="top">
          <div style="font-size:12px;line-height:18px;" id="content" runat="server" />
          </td>
        </tr>
      </table>
      <br />
      <br />
      <table width="100%" height="56" border="0" cellpadding="0" cellspacing="0" background="../sysImages/Login/bottom_bg.gif">
        <tr>
          <td><div align="center" class="STYLE2"><%Response.Write(CopyRight); %> </div></td>
        </tr>
      </table>
      </div>
    </form>
</body>
</html>
