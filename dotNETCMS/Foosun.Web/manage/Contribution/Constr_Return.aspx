<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="manage_Contribution_Constr_Return" Codebehind="Constr_Return.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
</head>
<body style="background-color: #ffffff">
<form id="form1" name="form1" method="post" action="" runat="server">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >
              投稿管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Constr_List.aspx" class="topnavichar">稿件管理</a></div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td width="46%" class="navi_link">&nbsp; &nbsp; &nbsp;<a href="Constr_List.aspx" class="topnavichar">稿件管理</a> &nbsp; &nbsp;<a href="Constr_Stat.aspx" class="topnavichar">稿件统计</a> &nbsp; &nbsp;<a href="paymentannals.aspx" class="topnavichar">支付历史</a> &nbsp; &nbsp;<a href="Constr_SetParam.aspx" class="topnavichar">稿酬设定</a> &nbsp; &nbsp;<a href="Constr_chicklist.aspx" class="topnavichar">所有通过审核稿件</a></td>
  </tr>
</table></td>
  </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  <tr class="TR_BG_list">
    <td class="list_link" colspan="2">
        &nbsp;你要把<asp:Label ID="Title" runat="server" ForeColor="Red" Width="291px"></asp:Label>退稿吗？</td>
  </tr>
  <tr class="TR_BG_list">
    <td  class="list_link" width="15%">
        退稿理由：</td>
    <td class="list_link" width="85%">
        <asp:TextBox ID="passcontent" runat="server" Height="100px" TextMode="MultiLine"
            Width="431px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="passcontent"
            ErrorMessage="退稿理由不能为空"></asp:RequiredFieldValidator></td>
  </tr>  
  <tr class="TR_BG_list">
    <td class="list_link"></td>
    <td class="list_link">
        &nbsp; &nbsp; &nbsp;
        <asp:Button ID="But" runat="server" Text="确  定" CssClass="form" OnClick="But_Click"/>
        &nbsp; &nbsp; &nbsp; &nbsp;<input type="reset" name="Submit3" value="重  置" class="form"/>
    </td>
  </tr>
  </table>


<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table>
</form>
</body>
</html>






