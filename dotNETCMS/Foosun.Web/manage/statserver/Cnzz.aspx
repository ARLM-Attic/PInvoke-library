<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cnzz.aspx.cs" Inherits="Manage_statserver_Cnzz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" height="30">Cnzz统计服务</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />Cnzz统计服务</div></td>
        </tr>
      </table>
     
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">开启Cnzz统计</td>
      <td Width="90%" align="left">
          <asp:DropDownList ID="OpenTF" runat="server" onchange="javascript:show(this.value);" Width="205px">
              <asp:ListItem Value="1">开启</asp:ListItem>
              <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
          </asp:DropDownList><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_statCnzz_001',this)">帮助</span>
        </td>
    </tr>  
    <tr class="TR_BG_list" id="tr_Domain" style="display:none;">
      <td align="center" class="navi_link" style="width: 13%">网站域名</td>
      <td Width="90%" align="left"><asp:TextBox ID="Domain" CssClass="form" runat="server" Width="200px"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_statCnzz_002',this)">帮助</span>
        </td>
    </tr>
    
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 10%" colspan="2"><label>
        <asp:Button ID="Button1" runat="server" Text=" 确 定 " CssClass="form" OnClick="Button1_Click"/>
        </label>
        <label>
        <input type="reset" name="UnDo" value=" 重 填 " class="form" />
        </label> <span style="display:none;" id="other"><input type="button" name="LookCode" value=" 查看统计代码 " class="form" onclick="javascript:getCode();" /> 
                       <asp:Button ID="Login" runat="server" Text=" 登录统计后台 " CssClass="form" OnClick="Login_Click" /></span></td>
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
<script language="javascript" type="text/javascript">
function show(value)
{
    if (value=="1")
        document.getElementById("tr_Domain").style.display="";
    else
        document.getElementById("tr_Domain").style.display="none";
}
function getCode()
{
    var WWidth = (window.screen.width-500)/2;
    var Wheight = (window.screen.height-150)/2;
    //--------------------------------------
    window.open('getStatCode.aspx', '统计代码调用', 'height=200, width=400, top='+Wheight+', left='+WWidth+', toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');
}
</script>
<%        
if (s_OpenTF == "1"){ ExecuteJs("document.getElementById(\"tr_Domain\").style.display='';document.getElementById(\"other\").style.display='';");}
%>