<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Logscreat" ResponseEncoding="utf-8" Codebehind="Logscreat.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >创建日历</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" >位置导航：<a href="main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />创建日历</td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="PADDING-LEFT: 14px;"><a class="topnavichar" href="logs.aspx">管理日历</a>　<a class="topnavichar" href="logsCreat.aspx">创建日历</a></td>
        </tr>
      </table>
      <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">日历标题</div></td>
          <td class="list_link"><asp:TextBox ID="title" runat="server"  Width="250"  MaxLength="50" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_logscreat_0001',this)">帮助</span><asp:RequiredFieldValidator ID="f_title" runat="server" ControlToValidate="title" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写日历标题，长度为50</span>"></asp:RequiredFieldValidator></td>
        </tr>                                                                                                                                                                                                                                                                                             
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">日历日期</div></td>
          <td class="list_link"><asp:TextBox ID="LogDateTime" runat="server" Width="250" MaxLength="200" CssClass="form"></asp:TextBox><input type="button" value="选择日期"  onclick="selectFile('date',document.form1.LogDateTime,160,400);document.form1.LogDateTime.focus();" />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onClick="Help('H_logscreat_0002',this)">帮助</span><asp:RequiredFieldValidator ID="f_LogDateTime" runat="server" ControlToValidate="LogDateTime" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写日期。格式为：2007-2-14</span>"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="f_LogDateTime1" runat="server"  ControlToValidate="LogDateTime"  ErrorMessage="正确填写日期"
                  ValidationExpression="^[12]{1}(\d){3}[-][01]?(\d){1}[-][0123]?(\d){1}$"></asp:RegularExpressionValidator></td>
        </tr> 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 112px;"><div align="right">事件描述</div></td>
          <td class="list_link" style="height: 112px"><asp:TextBox ID="Content" runat="server" Width="400" TextMode="MultiLine" Height="100px" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onClick="Help('H_logscreat_0003',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">提前提醒</div></td>
          <td class="list_link">
          <asp:TextBox ID="dateNum" runat="server" value="0" Width="100"  CssClass="form"/> 天
          <span class="helpstyle" style="cursor:help"; title="点击查看帮助"  onclick="Help('H_logscreat_0004',this)">帮助</span><asp:RequiredFieldValidator ID="f_dateNum" runat="server" ControlToValidate="dateNum" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写需要提前提醒的天数</span>"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="f_dateNum1" runat="server" ControlToValidate="dateNum"  Display="Static" ErrorMessage="(*)提前提醒的格式不正确。请填写正整数" ValidationExpression="^[0-9]{0,2}"></asp:RegularExpressionValidator></td>
        </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 28px;">&nbsp;</td>
          <td class="list_link" style="height: 28px">&nbsp;<asp:Button ID="sumbitsave" runat="server" CssClass="form" Text=" 确 定 "  OnClick="Logssubmit" />
            <input name="reset" type="reset" value=" 重 置 "  class="form"><asp:HiddenField ID="log_id" runat="server" />         </td>
        </tr>

</table>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat=server /></td>
   </tr>
 </table>
</form>
</body>
</html>