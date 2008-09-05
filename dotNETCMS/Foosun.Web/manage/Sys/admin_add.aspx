<%@ Page Language="C#" AutoEventWireup="true" Inherits="Manage_System_admin_add" ResponseEncoding="utf-8" Codebehind="admin_add.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
  <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">添加管理员</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="admin_list.aspx" class="list_link">管理员管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加管理员</div></td>
    </tr>
  </table>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">用户名</td>
      <td Width="90%" align="left"><asp:TextBox ID="TxtUserName" CssClass="form" runat="server" Width="200px" MaxLength="18"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_001',this)">帮助</span><asp:RequiredFieldValidator ID="RequireUserName" runat="server" ControlToValidate="TxtUserName" Display="Dynamic" ErrorMessage="<span class=reshow>(*)请填写用户名</spna>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%"> 密 码</td>
      <td Width="90%" align="left"><asp:TextBox ID="UserPwd" CssClass="form" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_002',this)">帮助</span>&nbsp;&nbsp;如果前台会员存在，此项将不起作用。</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">确认密码</td>
      <td Width="90%" align="left"><asp:TextBox ID="SecUserPwd" CssClass="form" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
        <asp:CompareValidator ID="CompareSecUserPwd" runat="server" ErrorMessage="<span class=reshow>(*)两次密码不一致</span>" ControlToValidate="UserPwd" ControlToCompare="SecUserPwd" Type="String"></asp:CompareValidator></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">是否禁用</td>
      <td Width="90%" align="left"><span class="list_link">&nbsp;
        <input name="IsInvocation" type="radio" value="1"/>
        是&nbsp;&nbsp;&nbsp;
        <input name="IsInvocation" type="radio" value="0" checked />
        否</span><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_003',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%; height: 30px;"> 姓 名</td>
      <td Width="90%" align="left" style="height: 30px"><span class="list_link"><asp:TextBox CssClass="form" ID="RealName" runat="server" Width="200px" MaxLength="20"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_005',this)">帮助</span></span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%"> 电子邮件</td>
      <td Width="90%" align="left"><asp:TextBox CssClass="form" ID="Email" runat="server" Width="200px" MaxLength="120"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_006',this)">帮助</span><asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="<span class=reshow>(*)请填写电子邮件</span>"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" Display="Static" ErrorMessage="<span class=reshow>邮箱格式不正确</span>" ControlToValidate="Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">所属管理员组</td>
      <td Width="90%" align="left"><asp:DropDownList ID="AdminGroup" runat="server" Width="200px"> </asp:DropDownList><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_007',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">
          是否频道管理员</td>
      <td Width="90%" align="left"><span class="list_link">&nbsp;
      <input name="isChannel" type="radio" value="1" onclick="javascript:Hide(this.value);" checked="checked"  />
      是&nbsp;&nbsp;&nbsp;
      <input name="isChannel" type="radio" value="0" onclick="javascript:Hide(this.value);" />
      否<span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_004',this)">帮助</span></span></td>
    </tr>
    <tr class="TR_BG_list" id="Tr_SiteAdmin">
      <td align="center" class="navi_link" style="width: 13%">频道超级管理员</td>
      <td Width="90%" align="left"><span class="list_link">&nbsp;
        <input name="isChSupper" type="radio" value="1" />
        是&nbsp;&nbsp;&nbsp;
        <input name="isChSupper" type="radio" value="0" checked="checked"/>
        否</span><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_008',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list" id="Tr_SiteList">
      <td align="center" class="navi_link" style="width: 13%">所属频道</td>
      <td align="left"><span class="list_link" runat="server" id="Site_Span"></span><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_012',this)">帮助</span></td>
    </tr>
    <tr visible="false" runat="server" class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">允许多人登陆</td>
      <td Width="90%" align="left"><span class="list_link">&nbsp;
        <input name="MoreLogin" type="radio" value="1"/>
        是&nbsp;&nbsp;&nbsp;
        <input name="MoreLogin" type="radio" value="0" checked="checked" >
        否</span><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_009',this)">帮助</span></td>
    </tr>
    <%--    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">后台登陆限制</td>
      <td Width="90%" align="left"><asp:TextBox CssClass="form" ID="LimitType" runat="server" Width="200px" MaxLength="12"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_010',this)">帮助</span><asp:RequiredFieldValidator ID="RequiredFieldLimitType" runat="server" ControlToValidate="LimitType" Display="Dynamic" ErrorMessage="<span class=reshow>(*)请填写限制类型,不限制则填写:0|0</span>"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularLimitType" runat="server" Display="Static" ErrorMessage="<span class=reshow>格式不正确,格式为:3|5,说明,3代表错误登录的次数,5代表锁定的时间(单位小时)</span>" ControlToValidate="LimitType" ValidationExpression="^[0-9]{1,4}\|[0-9]{1,4}"></asp:RegularExpressionValidator></td>
    </tr>--%>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">IP限制</td>
      <td Width="90%" align="left">
          <asp:TextBox ID="Iplimited" runat="server" CssClass="form" Height="74px" TextMode="MultiLine" Width="558px"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminAdd_011',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="left" class="navi_link" style="width: 10%" colspan="2"><label>
        <asp:Button ID="Button1" runat="server" Text=" 确 定 " CssClass="form" OnClick="Button1_Click"/>
        </label>
        <label>
        <input type="reset" name="UnDo" value=" 重 填 " class="form" />
        </label></td>
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
<script language="javascript" type="text/javascript">
    function Hide(value)
    {
        if (value=="1")
        {
            document.getElementById("Tr_SiteAdmin").style.display="";
            document.getElementById("Tr_SiteList").style.display="";
        }
        else
        {
            document.getElementById("Tr_SiteAdmin").style.display="none";
            document.getElementById("Tr_SiteList").style.display="none";
        }
    }
</script>
</html>
