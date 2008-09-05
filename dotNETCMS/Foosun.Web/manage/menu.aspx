<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_menu" Codebehind="menu.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Public.js"></script>
<script language="javascript" type="text/javascript">
if(self==top)
{self.location.href='index.aspx';}
</script>
</head>
<body>
<table width="165" height="29" border="0" cellpadding="0" cellspacing="0" class="menuq">
  <tr>
    <td width="26" rowspan="2" align="center"><img src="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot.gif" width="8" height="11" border="0"  alt="展开/隐藏"  id="arrow_1"/></td>
    <td height="2"></td>
    <td width="50" rowspan="2" align="center"></td>
  </tr>
  <tr>
    <td width="121" align="left" class="Lion_menu_2" style="cursor:pointer;" onclick="show_hide('profile_1', 'arrow_1')">快捷导航</td>
  </tr>
</table>
<div id="profile_1" style="display:<%if (stype == "000000"){Response.Write("");}else{Response.Write("none");}%>">
<!--------快捷方式导航开始,by simplt.xie--------------------->
<div id="shortcut_id" runat="server" />
<!--------快捷方式导航结束,by simplt.xie--------------------->
</div>

<table width="165" height="29" border="0" cellpadding="0" cellspacing="0" class="menuq">
  <tr>
    <td width="26" rowspan="2" align="center"><img src="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot.gif" width="8" height="11" border="0"  alt="展开/隐藏"  id="arrow_3"/></td>
    <td height="2"></td>
    <td width="50" rowspan="2" align="center"></td>
  </tr>
  <tr>
    <td width="121" align="left" class="Lion_menu_2" style="cursor:pointer;" onclick="show_hide('profile_3', 'arrow_3')">频道列表</td>
  </tr>
</table>
<div id="profile_3" style="display:<%if (stype == "000000"){Response.Write("");}else{Response.Write("none");}%>">
<!--------频道列表导航开始,by simplt.xie--------------------->
<div id="channelContent" runat="server" />
<!--------频道列表导航结束,by simplt.xie--------------------->
</div>

<%if (stype != "000000")
  {%>
<table width="165" height="29" border="0" cellpadding="0" cellspacing="0" class="menuq">
  <tr>
    <td width="26" rowspan="2" align="center"><img src="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot1.gif" width="8" height="11" border="0" alt="展开/隐藏" id="arrow_2" /></td>
    <td height="2"></td>
    <td width="50" rowspan="2" align="center"></td>
  </tr>
  <tr>
    <td width="101" align="left" class="Lion_menu_2" style="cursor:pointer;" onclick="show_hide('profile_2', 'arrow_2')">功能导航</td>
  </tr>
</table>
<!--------自定义菜单开始,by simplt.xie--------------------->
<div id="profile_2" style="display:"><label id="menuNavi_id" runat="server" />
<!--------自定义菜单结束,by simplt.xie--------------------->
 <% } %>
</div>
</body>
<script language="javascript" type="text/ecmascript">
 function show_hide(DivID,ImgID)
 {
    if (document.getElementById(DivID).style.display=='')
    {
        document.getElementById(DivID).style.display='none';
        document.getElementById(ImgID).src='../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot.gif';
    }
    else
    {
        document.getElementById(DivID).style.display='';
        document.getElementById(ImgID).src='../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot1.gif';
    }
}
</script>
</html>
