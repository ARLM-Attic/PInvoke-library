<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="user_info_UpdateGroup" Codebehind="UpdateGroup.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat = "Server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >升级会员组</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userinfo.aspx" class="list_link">我的资料</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />升级会员组</div></td>
        </tr>
        </table>
     <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">您现在的会员组</div></td> 
          <td class="list_link"><div ID="GroupName" runat="server"  />
          </td>
          </tr>
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">您需要升级到的会员组</div></td> 
          <td class="list_link"><label ID="groupList" runat="server" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_Updategroup_001',this)">帮助</span></td>
          </tr>          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"></td> 
          <td class="list_link">
             <asp:Button ID="Button1" runat="server" CssClass="form" Text="确认开始升级" OnClientClick="if(confirm('确定要升级吗?'));{return true;}return false;" OnClick="Button1_Click" />
          </td>
          </tr>
      </table>
      <br />
      <br />
      <br />
     <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><label id="copyright" runat="server" /></td>
       </tr>
     </table>   
 
     </form>
</body>
</html>
