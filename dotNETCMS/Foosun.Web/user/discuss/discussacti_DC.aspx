<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discussacti_DC" EnableEventValidation="true" Codebehind="discussacti_DC.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >讨论活动管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="discussacti_list.aspx" class="menulist">讨论活动管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />活动详细内容</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="discussacti_list.aspx" class="menulist">讨论活动列表</a>　<a href="discussactijoin_list.aspx" class="menulist">我加入的活动</a>&nbsp;&nbsp; <a href="discussactiestablish_list.aspx" class="menulist">我建立的活动</a>&nbsp;&nbsp; <a href="#" class="menulist">创建活动</a></span></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  <tr class="TR_BG_list">
    <td class="list_link" width="15%">
        活动主题</td>
    <td class="list_link" width="35%">
        <asp:Label ID="Activesubject" runat="server" Height="100%" Width="100%"></asp:Label></td>
    <td class="list_link" width="15%">
        活动地点</td>
    <td class="list_link" width="35%">
        <asp:Label ID="ActivePlace" runat="server" Height="100%" Width="100%"></asp:Label></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link">
        活动费用</td>
    <td class="list_link">
        <asp:Label ID="ActiveExpense" runat="server" Height="100%" Width="100%"></asp:Label></td>
    <td class="list_link">
        参与人数</td>
    <td class="list_link">
        <asp:Label ID="Anum" runat="server" Height="100%" Width="100%"></asp:Label></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link">
        联系方式</td>
    <td class="list_link">
        <asp:Label ID="Contactmethod" runat="server" Height="100%" Width="100%"></asp:Label></td>
    <td class="list_link">
        报名截止时间</td>
    <td class="list_link">
        <asp:Label ID="Cutofftime" runat="server" Height="100%" Width="100%"></asp:Label></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link">
        活动时间</td>
    <td class="list_link">
        <asp:Label ID="CreaTime" runat="server" Height="100%" Width="100%"></asp:Label></td>
    <td class="list_link">
        发起活动人</td>
    <td class="list_link">
        <asp:Label ID="UserName" runat="server" Height="100%" Width="100%"></asp:Label></td>
  </tr>
   <tr class="TR_BG_list">
       <td class="list_link" colspan="4">
           <table border="0" cellpadding="0" cellspacing="0" width="100%">
               <tr>
                   <td width="15%" style="height: 184px">
                       &nbsp;活动具体方案</td>
                   <td style="width: 85%; height: 184px">
                       <asp:Label ID="ActivePlan" runat="server" Height="100%" Width="100%"></asp:Label></td>
               </tr>
           </table>
       </td>
  </tr>
     <tr class="TR_BG_list">
    <td class="list_link" colspan="4" align="center">
        <asp:Button ID="Button1" runat="server" Text="确  定" Width="93px" OnClick="Button1_Click" CssClass="form"/></td>
  </tr>
</table>
<div style="PADDING-top: 50px"></div>
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table> 
</form>
</body>
</html>
