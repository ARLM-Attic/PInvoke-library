<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_File_Txt_Edit" Codebehind="File_Txt_Edit.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
</head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="JavaScript" type="text/javascript" src="../../editor/editor.js"></script>
<body>
<form id="FileTxtform" runat="server" method="post" action="">
  <%
      string FP = Foosun.Config.UIConfig.filePass;//从Web.config中读取文件密码信息
 %>
  <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">文本编辑<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_File_Txt_edit_0001',this)">帮助</span></td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="File_GetIn.aspx?id=<%= FP %>" class="list_link">文件管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />文本编辑</div></td>
    </tr>
  </table>
  <table width="98%" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG_list">
      <td class="TR_BG_list"><div id="dirPath" runat="server"></div></td>
    </tr>
    <tr class="TR_BG_list">
      <td class="list_link" style="padding-top:0;padding-left:0;padding-right:0;padding-bottom:0;"><!--编辑器开始-->
        <asp:TextBox ID="FileContent" runat="server" Width="810" Height="289px" CssClass="form" TextMode="MultiLine"></asp:TextBox>
        <div id="test" runat="server"></div>
        <!--编辑器结束--></td>
    </tr>
    <tr class="TR_BG_list">
      <td class="list_link"><div align="center">
          <input type="button" name="Submit" onclick="javascript:CheckForm();" value=" 确认提交 " class="form" />
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <input type="button" name="Submit" value=" 恢 复 " class="form" onclick="javascript:UnDo();" />
        </div></td>
    </tr>
  </table>
  <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
    <tr>
      <td align="center"><label id="copyright" runat="server" /></td>
    </tr>
  </table>
</form>
</body>
<script language="javascript" type="text/javascript">
function CheckForm()
{
    if(confirm('你确定要保存所做的更改吗?'))
    {
        document.FileTxtform.action='File_Txt_Edit.aspx?action=Save&dir=<% Response.Write(Request.QueryString["dir"]); %>&filename=<% Response.Write(Request.QueryString["filename"]); %>';
        document.FileTxtform.submit();
    }   
}
function UnDo()
{
    if(confirm('你确定要取消所做的更改吗?'))
    {
        document.FileTxtform.reset();
    }   
}
</script>
</html>
