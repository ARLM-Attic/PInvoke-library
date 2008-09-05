<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_Exchange" Codebehind="Exchange.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
function Change(o)
{
     var t;
      if(o==0)
      {
        t = "确定兑换成G币吗？";
      }
      else
      {
        t = "确定兑换成积分吗？";
      }
      {if(confirm(t)){return true;}return false;}
//      if(confirm(t))
//      {
//        return true;
//      }
//      else
//      {
//        return false;
//      }
}
</script>
</head>
<body><form id="form1" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >积分兑换</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />积分兑换</div></td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;"><span id="scs" runat="server"></span>  <span id="sc" runat="server"></span></td>
        </tr>
</table>
<%
string types = Request.QueryString["types"];
if ((type == "G" && types == null) || (type == null && types == "G"))
{
%>
<asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
<%} %>
<% else
{ %>
<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>

<%} %>  

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

