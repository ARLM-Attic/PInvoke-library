<%@ Page Language="C#" AutoEventWireup="true" Inherits="usermycom_Look" Codebehind="usermycom_Look.aspx.cs" %>
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
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="mycom.aspx" class="menulist">评论管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />评论信息</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="usermycom.aspx" class="menulist">评论管理</a>&nbsp;┊&nbsp;<a href="javascript:history.back();"><font color="red">返回</font></a></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  <tr class="TR_BG_list">
    <td class="list_link" width="15%" style="text-align: right">评论标题：</td>
      <td class="list_link" width="85%"><label id="TitleBox" runat="server" /></td>
 </tr>
 <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">评论内容：</td>
    <td class="list_link"><label id="ContentBox" runat="server" /></td>
 </tr>
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">评论人：</td>
    <td class="list_link"><label id="strUserNum" runat="server" /></td>
 </tr>
 
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">IP：</td>
    <td class="list_link"><label id="ipstr" runat="server" /></td>
 </tr> 
 
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">发布日期：</td>
    <td class="list_link"><label id="creatTime" runat="server" /></td>
 </tr> 
 
 </table>
<br />
<br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
</html>
