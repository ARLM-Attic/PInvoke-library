<%@ Page Language="C#" AutoEventWireup="true" Inherits="Manage_Survey_setParam" Codebehind="setParam.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
  <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">调查管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />调查管理</div></td>
    </tr>
  </table>
  <div>
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
      <tr class="menulist">
        <td height="18" style="width: 45%" colspan="2"  style="PADDING-LEFT: 14px" ><div align="left"> <a href="ManageVote.aspx" class="menulist"> <a href="setParam.aspx" class="menulist">系统参数设置</a>&nbsp;┊&nbsp;<a href="setClass.aspx" class="menulist">投票分类设置</a>&nbsp;┊&nbsp;<a href="setTitle.aspx" class="menulist">投票主题设置</a>&nbsp;┊&nbsp;<a href="setItem.aspx" class="menulist">投票选项设置</a>&nbsp;┊&nbsp;<a href="setSteps.aspx" class="menulist">多步投票管理</a>&nbsp;┊&nbsp;<a href="ManageVote.aspx" class="menulist">投票情况管理</a> </div></td>
      </tr>
    </table>
  </div>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">问卷调查系统参数设置</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 257px"> IP时间间隔：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="IPtime" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surverParam_0001',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 257px"> 是否注册才能投票：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="IsReg" runat="server" CssClass="form">
          <asp:ListItem Value="1">是</asp:ListItem>
          <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surverParam_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 257px"> 禁止投票的IP段：</td>
      <td  align="left" class="list_link"><textarea ID="IpLimit" runat="server" style="width: 251px; height: 105px" class="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surverParam_0003',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Saveupload" value=" 提 交 " class="form" id="SavePram" runat="server" onserverclick="SavePram_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearupload" value=" 重 填 " class="form" id="ClearPram" runat="server" />
        </label></td>
    </tr>
  </table>
  <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
    <tr>
      <td align="center"><label id="copyright" runat=server /></td>
    </tr>
  </table>
</form>
</body>
</html>
