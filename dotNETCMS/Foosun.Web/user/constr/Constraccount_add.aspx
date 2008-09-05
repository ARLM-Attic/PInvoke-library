<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_Constraccount_add" Debug="true" Codebehind="Constraccount_add.aspx.cs" %>

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
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td colspan="2" style="height: 1px"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >账号管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Constrlist.aspx" class="menulist">稿件管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加账号</div></td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;">          
          <a href="Constr.aspx" class="menulist">发表文章</a>&nbsp; &nbsp;<a href="Constrlistpass.aspx" class="topnavichar" >所有退稿</a>&nbsp; &nbsp;<a href="Constrlist.aspx" class="menulist">文章管理</a>&nbsp; &nbsp;<a href="ConstrClass.aspx" class="menulist">分类管理</a>&nbsp; &nbsp;<a href="Constraccount.aspx" class="menulist">账号管理</a>&nbsp; &nbsp;<a href="Constraccount_add.aspx" class="menulist">添加账号</a></td>
        </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
    <tr class="TR_BG_list">
    <td class="list_link">
        真实姓名：</td>
    <td class="list_link">
        <asp:TextBox ID="RealNameBox" runat="server" Width="349px"  CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constraccount_add_0001',this)">帮助</span> 

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="真实姓名不能为空" ControlToValidate="RealNameBox"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="TR_BG_list">
    <td class="list_link">
        开户银行：</td>
    <td class="list_link">
        <asp:TextBox ID="bankNameBox" runat="server" Width="349px" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constraccount_add_0002',this)">帮助</span> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="开户银行不能为空" ControlToValidate="bankNameBox"></asp:RequiredFieldValidator></td>
        
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link">
        开户名：</td>
    <td class="list_link">
        <asp:TextBox ID="bankRealNameBox" runat="server" Width="349px" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constraccount_add_0003',this)">帮助</span> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="开户名不能为空" ControlToValidate="bankRealNameBox"></asp:RequiredFieldValidator></td>
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link">
        银行账号：</td>
    <td class="list_link">
        <asp:TextBox ID="bankaccountBox" runat="server" Width="349px" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constraccount_add_0004',this)">帮助</span> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="银行账号不能为空" ControlToValidate="bankaccountBox"></asp:RequiredFieldValidator></td>
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link">
        卡号：</td>
    <td class="list_link">
        <asp:TextBox ID="bankcardBox" runat="server" Width="349px" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constraccount_add_0005',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="卡号不能为空" ControlToValidate="bankcardBox"></asp:RequiredFieldValidator> </td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link" width="25%">
        地址：</td>
    <td class="list_link" width="75%">
       <asp:TextBox ID="addressBox" runat="server" Width="349px" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constraccount_add_0006',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="地址不能为空" ControlToValidate="addressBox"></asp:RequiredFieldValidator> </td>
  </tr>

  <tr class="TR_BG_list">
    <td class="list_link">
        邮政编码：</td>
    <td class="list_link">
        <asp:TextBox ID="postcodeBox" runat="server" Width="349px" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constraccount_add_0007',this)">帮助</span> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
            ErrorMessage="邮政编码不能为空" ControlToValidate="postcodeBox"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="邮政编码不对"
            ValidationExpression="\d{6}" ControlToValidate="postcodeBox"></asp:RegularExpressionValidator></td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link"></td>
    <td class="list_link">
        &nbsp; &nbsp;
        <asp:Button ID="Button1" runat="server" Text="提 交" OnClick="Button1_Click" CssClass="form"/>
        &nbsp; &nbsp;<input type="reset" name="Submit3"  class="form" value="重 置">
    </td>
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
