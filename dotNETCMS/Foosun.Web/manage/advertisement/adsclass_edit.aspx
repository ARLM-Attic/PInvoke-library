<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_advertisement_adsclass_edit" ResponseEncoding="utf-8" Codebehind="adsclass_edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>修改分类信息</title>
    <link href="../../sysImages/<%Response.Write( Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server" method="post" action="adsclass_edit.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">修改分类信息</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="list.aspx" target="sys_main" class="list_link">广告系统</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改分类信息</div></td>
        </tr>
      </table>
      
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
        <tr class="TR_BG_list">
          <td align="center" class="navi_link" style="width: 13%">分类名称</td>
          <td Width="90%" align="left"><asp:TextBox ID="AdsClassName" runat="server" Width="200px" MaxLength="50" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_018',this)">帮助</span><asp:RequiredFieldValidator ID="RequireAdsClassName" runat="server" ControlToValidate="AdsClassName" Display="Dynamic" ErrorMessage="<span class=reshow>(*)请填写分类名称</spna>"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="TR_BG_list">
          <td align="center" class="navi_link" style="width: 13%">父类编号</td>
          <td Width="90%" align="left"><asp:TextBox ID="AdsParentID" runat="server" Width="200px" MaxLength="12" CssClass="form" ReadOnly="true"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_019',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list">
          <td align="center" class="navi_link" style="width: 13%"> 价 格 </td>
          <td Width="90%" align="left"><asp:TextBox ID="AdsPrice" runat="server" Width="200px" MaxLength="10" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_020',this)">帮助</span><span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AdsPrice" Display="Dynamic" ErrorMessage="<span class=reshow>(*)请填写价格</spna>"></asp:RequiredFieldValidator></span><span><asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="<span class=reshow>(*)价格格式不正确,只能为正整数</spna>" Type="Integer" ControlToValidate="AdsPrice" MaximumValue="1000000000" MinimumValue="0" Display="Dynamic"></asp:RangeValidator></span></td>
        </tr>
         <tr class="TR_BG_list">
          <td class="navi_link" colspan="2"><label><asp:Button ID="Button1" runat="server" Text=" 确 定 " CssClass="form" OnClick="Button1_Click"/></label>      <label><input type="reset" name="UnDo" value=" 重 填 " class="form" /></label><input name="adsclassid" type="hidden" runat="server" id="adsclassid" /></td>
        </tr>
       </table>
        
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
      <tr>
        <td align="center"><label id="copyright" runat="server" /></td>
      </tr>
    </table>
    </form>
</body>
</html>

