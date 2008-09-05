<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discussacti" EnableEventValidation="true" Codebehind="discussacti.aspx.cs" %>
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
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >讨论活动管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="discussacti_list.aspx" class="menulist">讨论活动管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />创建活动</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="discussacti_list.aspx" class="menulist">讨论活动列表</a>　<a href="discussactijoin_list.aspx" class="menulist">我加入的活动</a>&nbsp;&nbsp; <a href="discussactiestablish_list.aspx" class="menulist">我建立的活动</a>&nbsp;&nbsp; <a href="#" class="menulist">创建活动</a></span></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">  
  <tr class="TR_BG_list">
    <td  class="list_link" width="20%">活动主题</td>
    <td  class="list_link" width="80%">
        <asp:TextBox ID="ActivesubjectBox" runat="server" Width="348px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussacti_0001',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入活动主题" ControlToValidate="ActivesubjectBox" Display="Dynamic"></asp:RequiredFieldValidator></td>
  </tr>
  <tr class="TR_BG_list">
    <td  class="list_link">活动地点</td>
    <td class="list_link">
        <asp:TextBox ID="ActivePlaceBox" runat="server" Width="348px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussacti_0002',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入活动地点" ControlToValidate="ActivePlaceBox" Display="Dynamic"></asp:RequiredFieldValidator></td>
  </tr>
    <tr class="TR_BG_list">
    <td  class="list_link">报名截止时间</td>
    <td class="list_link">
        <asp:TextBox ID="CutofftimeBox" runat="server" Width="270px" CssClass="form"></asp:TextBox>&nbsp;
        <input  class="form" type="button" value="选择日期"  onclick="selectFile('date',document.form1.CutofftimeBox,160,500);document.form1.CutofftimeBox.focus();" />&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussacti_0003',this)">帮助</span>
        &nbsp;&nbsp; &nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入截止时间" ControlToValidate="CutofftimeBox" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="日期格式不对" ControlToValidate="CutofftimeBox" ValidationExpression="^[12]{1}(\d){3}[-][01]?(\d){1}[-][0123]?(\d){1}$"></asp:RegularExpressionValidator></td>
  </tr>
    <tr class="TR_BG_list">
    <td  class="list_link">参与人数</td>
    <td class="list_link">
        <asp:TextBox ID="AnumBox" runat="server" Width="348px" CssClass="form">0</asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussacti_0004',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入参加人数" ControlToValidate="AnumBox" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="输入的格式不对" ControlToValidate="AnumBox" Display="Dynamic" ValidationExpression="^[1-9]\d*|0$"></asp:RegularExpressionValidator></td>
  </tr>
      <tr class="TR_BG_list">
    <td  class="list_link">标签</td>
    <td class="list_link">
        <asp:RadioButtonList ID="ALabelList" runat="server" RepeatDirection="Horizontal"
            Width="242px">
            <asp:ListItem Selected="True">正常</asp:ListItem>
            <asp:ListItem>推荐</asp:ListItem>
        </asp:RadioButtonList></td>
  </tr>
  
  
    <tr class="TR_BG_list">
    <td  class="list_link">活动费用</td>
    <td class="list_link">
        <asp:TextBox ID="ActiveExpenseBox" runat="server" Width="346px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussacti_0005',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ActiveExpenseBox"
            ErrorMessage="请输入活动费用"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ActiveExpenseBox"
            ErrorMessage="输入的格式不对" ValidationExpression="^[1-9]\d*|0$"></asp:RegularExpressionValidator></td>
  </tr>
    <tr class="TR_BG_list">
    <td  class="list_link">联系方式</td>
    <td class="list_link">
        <asp:TextBox ID="ContactmethodBox" runat="server" Width="348px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussacti_0006',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请输入联系方式" ControlToValidate="ContactmethodBox"></asp:RequiredFieldValidator></td>
  </tr>
    <tr class="TR_BG_list">
    <td  class="list_link">活动具体方案</td>
    <td class="list_link">
        <asp:TextBox ID="ActivePlanBox" runat="server" TextMode="MultiLine" Height="76px" Width="348px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussacti_0007',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请输入具体方案" ControlToValidate="ActivePlanBox" Display="Dynamic"></asp:RequiredFieldValidator></td>
  </tr>
      <tr class="TR_BG_list">
    <td  class="list_link"></td>
    <td class="list_link">
        &nbsp;&nbsp;
        <asp:Button ID="inBox" runat="server" Text="确 定" OnClick="inBox_Click" CssClass="form"/>
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<input type="reset" name="Submit3" value="重 置" class="form"></td>
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
