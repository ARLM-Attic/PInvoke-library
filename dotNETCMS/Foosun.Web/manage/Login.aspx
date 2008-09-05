<%@ Page Language="C#" AutoEventWireup="true" Inherits="Manage_Login" enableEventValidation="false" Codebehind="Login.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="Copyright" content="www.foosun.net" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %> _By Foosun Inc.</title>
<link rel="icon" href="../favicon.ico" type="image/x-icon" />
<link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" /> 
<link href="../sysImages/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../sysImages/Login/top_bg.gif">
    <tr>
      <td style="height: 55px;width:10%;"></td>
      <td style="height: 55px;width:40%;">&nbsp;</td>
      <td style="height: 55px;width:50%;padding-right:10px;"><div align="right"><a href="http://www.foosun.net" target="_blank" class="JH">官方站</a>&nbsp;┊&nbsp; <a href="http://bbs.foosun.net" target="_blank">技术论坛</a>&nbsp;┊&nbsp;<a href="http://www.newsCMS.com" target="_blank">NewsCMS</a>&nbsp;┊&nbsp;<a href="http://www.NewsIDC.com" title="专用虚拟主机" target="_blank">资讯数据(IDC)</a>&nbsp;┊&nbsp;<a href="http://help.foosun.net" target="_blank">在线帮助</a>&nbsp;┊&nbsp;<a href="http://doc.foosun.net" target="_blank">开发者中心</a></div></td>
    </tr>
  </table>

  <table width="100%" height="297" border="0" cellpadding="0" cellspacing="10">
    <tr>
      <td height="74" colspan="3">&nbsp;</td>
    </tr>
    <tr>
      <td width="442" valign="top"><div align="right">
        <table width="100%" style="height:160px" border="0" cellpadding="1" cellspacing="5">
          <tr>
            <td><div align="right"><img src="../sysImages/Login/Logo.gif" width="194" height="44" /></div></td>
          </tr>
          <tr>
            <td><div align="right" class="STYLE1">dotNETCMS&amp;NewsCMS v1.0.0</div></td>
          </tr>
          
          <tr>
            <td></td>
          </tr>
        </table>
        </div></td>
      <td style="width:1px;" bgcolor="#CCCCCC"></td>
      <td style="width:508px;" valign="top"><label></label>
        <table width="100%" style="height:193px" border="0" cellpadding="1" cellspacing="5">

          <tr>
            <td colspan="2"><div align="left">用户名&nbsp;
                <asp:TextBox ID="TxtName" runat="server" Width="129px" CssClass="username"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="f_UserNameX" runat="server" ControlToValidate="TxtName" ErrorMessage="请填写用户名"></asp:RequiredFieldValidator><script language="javascript" type="text/javascript">document.getElementById('TxtName').focus();</script>
            </div></td>
          </tr>
          <tr>
            <td colspan="2"><div align="left">密　码&nbsp;
                <asp:TextBox ID="TxtPassword" runat="server"  CssClass="password" TextMode="Password" Width="129px" MaxLength="18"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="f_PasswordX" runat="server" ControlToValidate="TxtPassword" ErrorMessage="请填写密码"></asp:RequiredFieldValidator>
                </div></td>
          </tr>
 <%--        <tr runat="server" id="safeCodeVerify_1">
            <td colspan="2"><div align="left">安全码&nbsp;
                <asp:TextBox ID="TxtSafeCode" runat="server"  CssClass="password" TextMode="Password" Width="129px" MaxLength="80"></asp:TextBox>&nbsp;
                区分大小写，默认为:foosun.net&nbsp;
                <asp:RequiredFieldValidator ID="f_safeCodeVerify" runat="server" ControlToValidate="TxtSafeCode" ErrorMessage="请填写安全码"></asp:RequiredFieldValidator>
                </div></td>
          </tr>--%>
          
          <tr>
            <td style="width:53%;" align="left">效验码&nbsp;
                <asp:TextBox ID="TxtVerify" CssClass="vercode" runat="server" Width="52px"></asp:TextBox>
                <script type="text/javascript" language="JavaScript">
                   var numkey = Math.random();
                   numkey = Math.round(numkey*10000);
                   document.write("<img src=\"../comm/Image.aspx?k="+ numkey +"\" width=\"70\" onClick=\"this.src+=Math.random()\" alt=\"图片看不清？点击重新得到验证码\" style=\"cursor:pointer;\" height=\"23\" hspace=\"4\"");
                </script>&nbsp;
            </td>
          </tr>
          <tr>
            <td colspan="2" style="height:36px;">
                <asp:HiddenField ID="HidUrl" runat="server" />
                &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../sysImages/Login/signin.gif" OnClick="login_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <label><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../sysImages/Login/reset.gif" OnClientClick="javascript:window.close();" /></label></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <br />
  <br />
  <br />
  <br />
  <br />
  <table width="100%" height="56" border="0" cellpadding="0" cellspacing="0" background="../sysImages/Login/bottom_bg.gif">
    <tr>
      <td><div align="center" class="STYLE2"><%Response.Write(CopyRight); %></div></td>
    </tr>
  </table>
</form>
</body>
</html>
