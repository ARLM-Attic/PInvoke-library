<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_info_buyCard" Codebehind="buyCard.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
         <tr>
              <td height="1" colspan="2"></td>
            </tr>
            <tr>
              <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >银行冲值</td>
              <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />购买点卡</div></td>
            </tr>
            </table>
          <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
              <td style="padding-left:14px;"><a class="topnavichar" href="getPoint.aspx">点卡冲值</a>&nbsp;┊&nbsp;<a class="topnavichar" href="onlinePoint.aspx">在线银行冲值</a>&nbsp;┊&nbsp;<a href="buyCard.aspx" class="list_link">购买点卡</a>&nbsp;┊&nbsp;<a href="history.aspx"  class="topnavichar">交易明晰</a></td>
            </tr>
          </table>
          <div align="center" id="div_1" runat="server">
              <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
                <tr class="TR_BG_list">
                  <td style="padding-left:14px;width:20%;">请选择要购买的点卡金额</td>
                  <td align="left">
                  <label id="sMoney" runat="server" />
                  </td>
                </tr>  
                <tr class="TR_BG_list">
                  <td style="padding-left:14px;width:20%;"></td><td align="left"><asp:Button ID="Button1" CssClass="form" runat="server" Text="购买点卡" OnClientClick="{if(confirm('确认要购买吗？')){return true;}return false;}" OnClick="Button1_Click" /></td>
                </tr>          
            </table>
        </div>
    </form>
    <br />
    <br />
     <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><% Response.Write(CopyRight); %></td>
       </tr>
     </table>    
</body>
</html>
