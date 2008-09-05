<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_const" Codebehind="const.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<table width="100%" height="32" align="center" border="0" cellpadding="0" cellspacing="0" background="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/admin/reght_1_bg_1.gif">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="17%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >
              配置文件管理</td>
          <td height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ></td>
        </tr>
</table>
<form id="Form1" runat="server">
<table width="98%" border="0" cellspacing="1" cellpadding="5" class="table" bgcolor="#FFFFFF" align="center">
  <tr class="TR_BG_list">
    <td width="21%" height="29" align="right" valign="middle">后台管理目录</td>
    <td width="79%" align="left" valign="middle">&nbsp;<asp:TextBox Width="200px" ID="dirMana" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0001',this)">帮助</span>
        <font color="red">提示:修改目录后请立即手动修改物理文件名称(名称必须相同)</font>
        </td>
    </tr>
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">
        后台模版目录</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="dirTemplet" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0002',this)">帮助</span>
    <font color="red">提示:修改此目录可能会对原有数据造成效大影响，请慎重修改</font>
    </td>
    </tr>
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">虚拟目录</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="dirDumm" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0003',this)">帮助</span></td>
    </tr>
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">开启密码保护</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="protPass" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_Pass',this)">帮助</span>
    </tr>
  <tr class ="TR_BG_list">
    <td height="28" align="right" valign="middle">安全码</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="protRand" Width="200px" runat="server" MaxLength="50" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0004',this)">帮助</span></td>
    </tr>
  <tr class = "TR_BG_list">
    <td height="28" align="right" valign="middle">文件上传目录</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="dirFile" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0005',this)">帮助</span></td>
    </tr>
<%--  <tr class = "TR_BG_list">
    <td height="28" align="right" valign="middle">API目录(台)</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="dirManaApi" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="榭? onClick="Help('H_const_0006',this)"></span></td>
    </tr>
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">API目录(前台)</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="dirUserApi" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="榭? onClick="Help('H_const_0007',this)"></span></td>
    </tr>
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">苹路</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="projPath" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="榭? onClick="Help('H_const_0008',this)"></span></td>
    </tr>--%>
  <tr class="TR_BG_list"  style="display:none">
    <td height="28" align="right" valign="middle">统计系统是否使用独立数据库</td>
    <td align="left" valign="middle">
    <asp:RadioButton ID="stat1" runat="server" onclick="Change(1)" Text="是" GroupName="indeData" />&nbsp;<asp:RadioButton ID="stat0" runat="server" onclick="Change(0)" Text="否" GroupName="indeData" />
        <span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0009',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list" id="stat_dis" style="display:none">
    <td height="28" align="right" valign="middle">统计系统的数据库连接</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="sqlConnData" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0014',this)">帮助</span></td>
    </tr>
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">配置文件管理密码</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="constPass" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0010',this)">帮助</span></td>
    </tr>
    
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">资源文件管理密码</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="filePass" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0010',this)">帮助</span></td>
    </tr>  
    
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">资源文件管理目录</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="filePath" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0010',this)">帮助</span></td>
    </tr>  

  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">归档表态文件存放目录</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="dirPige" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0013',this)">帮助</span></td>
    </tr>
  <tr class="TR_BG_list">
    <td height="28" align="right" valign="middle">保存样式目录</td>
    <td align="left" valign="middle">&nbsp;<asp:TextBox ID="manner" Width="200px" runat="server" CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onClick="Help('H_const_0015',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
        <td align="right" height="28" valign="middle">
        </td>
        <td align="left" valign="middle">
            <asp:Button ID="btn_const" runat="server" Height="27px" OnClick="btn_const_Click" Text="提  交" OnClientClick="{if(confirm('确认要修改吗!')){return true;}return false;}" Width="60px" CssClass="form"/></td>
    </tr>
</table>
</form>
<br />

 <table width="100%" border="0" cellpadding="8" align="center" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat=server /><%Response.Write(CopyRight); %></td>
   </tr>
</table>

</body>
<script language="javascript">
function Change(value)
{
    switch(parseInt(value))
    {
        case 1:
        document.getElementById("stat_dis").style.display="";
        break;
        case 0:
        document.getElementById("stat_dis").style.display="none";
        break;
    }
}

</script>
<% showjs(); %>
</html>
