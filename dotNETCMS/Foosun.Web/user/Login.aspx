<%@ Page Language="C#" AutoEventWireup="true" Inherits="User_Login" Codebehind="Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %></title>
<link rel="icon" href="../favicon.ico" type="image/x-icon" />
<link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" /> 
<link href="../sysImages/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" name="LoginForm" method="post" action="Index.aspx" runat="server">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../sysImages/Login/top_bg.gif">
    <tr>
      <td style="height: 55px;width:10%;"></td>
      <td style="height: 55px;width:40%;">&nbsp;</td>
      <td style="height: 55px;width:50%;padding-right:10px;"><div align="right"><a href="http://www.foosun.net" target="_blank" class="JH">官方站</a>&nbsp;┊&nbsp; <a href="http://bbs.foosun.net">技术论坛</a>&nbsp;┊&nbsp;<a href="http://www.newsCMS.com">NewsCMS</a>&nbsp;┊&nbsp;<a href="http://www.NewsIDC.com" title="专用虚拟主机" target="_blank">资讯数据(IDC)</a>&nbsp;┊&nbsp;<a href="http://help.foosun.net" target="_blank">在线帮助</a>&nbsp;┊&nbsp;<a href="http://doc.foosun.net" target="_blank">开发者中心</a></div></td>
    </tr>
  </table>

  <table style="width:100%;" border="0" cellpadding="0" cellspacing="10">
    <tr>
      <td style="width:74px;height:60px;" colspan="3">&nbsp;</td>
    </tr>
    <tr>
      <td style="width:442px;" valign="top"><div align="right">
        <table style="width:100%;" border="0" cellpadding="5" cellspacing="3">
          <tr>
            <td><div align="right"><img src="../sysImages/Login/Logo.gif" width="194" height="44" /></div></td>
          </tr>
          <tr>
            <td><div align="right" class="STYLE1">dotNETCMS&amp;NewsCMS v1.0.0</div></td>
          </tr>
          
          <tr>
            <td ><div align="right"><a href="#"><font color="#666666">在线客服</font></a></div></td>
          </tr>
        </table>
        </div></td>
      <td style="width:1px;" bgcolor="#CCCCCC"></td>
      <td style="width:508px;" valign="top">
        <table style="width:100%;" border="0" cellpadding="5" cellspacing="3">
          <tr>
            <td colspan="2" style="height: 19px"><div align="left">用户名
                <asp:TextBox ID="TxtName" runat="server" CssClass="username" Width="129px" MaxLength="18"></asp:TextBox>
            </div></td>
          </tr>
          <tr>
            <td colspan="2" style="height: 21px"><div align="left">密　码
                <asp:TextBox ID="TxtPassword" CssClass="password" Width="129px" runat="server" TextMode="Password"></asp:TextBox></div></td>
          </tr>
          <tr id="safecodeTF" runat="server">
            <td colspan="2" style="height: 14px">
            <div align="left">效验码
                <asp:TextBox ID="TxtVerifyCode" runat="server" CssClass="vercode" Width="52px"></asp:TextBox>
                   <SCRIPT LANGUAGE="JavaScript">
                   var numkey = Math.random();
                   numkey = Math.round(numkey*10000);
                   document.write("<img src=\"../comm/Image.aspx?k="+ numkey +"\" onClick=\"this.src+=Math.random()\" alt=\"图片看不清？点击重新得到验证码\" style=\"cursor:pointer;\" width=\"70\" height=\"23\" hspace=\"4\"");
                </SCRIPT>
            </div>
            </td>
          </tr>
          <tr>
            <td width="35%">
                <asp:HiddenField ID="HidUrl" runat="server" />
                &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../sysImages/Login/signin.gif" OnClick="ImageButton1_Click" />&nbsp;&nbsp;<input type="image" onclick="javascript:window.close();" name="imageField2" src="../sysImages/Login/reset.gif" />
            </td>
            <td width="65%"><a href="Register.aspx?SiteID=<%Response.Write(SiteID); %>">免费注册</a>&nbsp;&nbsp;&nbsp;<a href="info/getPassword.aspx">忘记密码？</a></td>
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
      <td><div align="center" class="STYLE2"><%Response.Write(CopyRight); %> </div></td>
    </tr>
  </table>
</form>
</body>
</html>