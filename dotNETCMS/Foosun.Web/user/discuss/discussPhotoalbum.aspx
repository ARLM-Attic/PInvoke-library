<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discussPhotoalbum" Debug="true" Codebehind="discussPhotoalbum.aspx.cs" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<div id="sc" runat="server"></div>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table" id="insert">

  <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">
        相册名称：</td>
    <td class="list_link" width="75%">
        <asp:TextBox ID="PhotoalbumName" runat="server" Width="241px" CssClass="form" MaxLength="14"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussPhotoalbum_0001',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PhotoalbumName"
            ErrorMessage="请输入相册名称" Enabled="False"></asp:RequiredFieldValidator></td>
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">
        相册权限：</td>
    <td class="list_link" width="75%">
        &nbsp;<input id="Radio1" type="radio" onclick="DispChanges()" runat="server" />所有人可以上传 &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<input id="Radio2" type="radio" runat="server" onclick="DispChanges()" checked="true"/>只有我能上传&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussPhotoalbum_0002',this)">帮助</span></td>
  </tr>
      <tr class="TR_BG_list" style="display:none" id="numbers">
    <td class="list_link" width="25%" style="text-align: right">
        最大上传图片数目：</td>
    <td class="list_link" width="75%">
        &nbsp;<asp:TextBox ID="number" runat="server" Width="235px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussPhotoalbum_0003',this)">帮助</span></td>
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">
        相册类型：</td>
    <td class="list_link" width="75%">
        &nbsp;<asp:DropDownList ID="Photoalbum" runat="server" Width="141px">
        </asp:DropDownList>
        &nbsp; &nbsp;<input type="checkbox" id="chkAdvance" onclick="DispChange()" />密码保护&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussPhotoalbum_0004',this)">帮助</span></td>
  </tr>
    <tr class="TR_BG_list" id="pwd" style="display:none">
    <td class="list_link" width="25%" style="text-align: right" >
        密码：</td>
    <td class="list_link" width="75%">
        <asp:TextBox ID="pwd" runat="server" Width="242px" Height="18px" TextMode="Password" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussPhotoalbum_0005',this)">帮助</span>
        </td>
  </tr>
      <tr class="TR_BG_list" id="pwds" style="display:none">
    <td class="list_link" width="25%" style="text-align: right">
        确认密码：</td>
    <td class="list_link" width="75%">
        <asp:TextBox ID="pwds" runat="server" Width="242px" Height="18px" TextMode="Password" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussPhotoalbum_0006',this)">帮助</span>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="pwd"
            ControlToValidate="pwds" ErrorMessage="两次密码不一致"></asp:CompareValidator></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link"></td>
    <td class="list_link">
        <asp:Button ID="Button1" runat="server" Text="创 建" Width="75px" OnClick="Button1_Click" CssClass="form"/>
        &nbsp;&nbsp;&nbsp;
        <input name="reset" type="reset" value=" 重 置 " class="form"/>
    </td>
  </tr>
</table>

 <br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
</html>
<script language="javascript">
function DispChange()
{
    var obj = document.getElementById("chkAdvance").checked;
    if(obj)
    {
            document.getElementById("pwd").style.display="";
            document.getElementById("pwds").style.display="";
    }
    else
    {
            document.getElementById("pwds").style.display="none";
            document.getElementById("pwd").style.display="none";
    }
}
function DispChanges()
{
    var obj = document.getElementById("Radio1").checked;
    var objs = document.getElementById("Radio2").checked;
    if(obj)
    {
            document.getElementById("numbers").style.display="";
    }
    if(objs)
    {
            document.getElementById("numbers").style.display="none";
    }
}
</script>
