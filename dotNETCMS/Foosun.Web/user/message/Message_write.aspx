<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_Message_write" EnableEventValidation="true" Codebehind="Message_write.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
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
      <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >
          短信管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />短信管理</div></td>
    </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
    <tr>
        <td style="padding-left:14px;">
            <a href="Message_write.aspx" class="navi_link">写新消息</a> &nbsp; &nbsp;<a href="Message_box.aspx?Id=1" class="navi_link">收件箱</a> &nbsp; &nbsp;<a href="Message_box.aspx?Id=2" class="navi_link">发件箱</a>&nbsp; &nbsp;<a href="Message_box.aspx?Id=3" class="navi_link">草稿箱</a>&nbsp; &nbsp;<a href="Message_box.aspx?Id=4" class="navi_link">废件箱</a>
        </td>
    </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  <tr class="TR_BG_list">
    <td class="list_link" width="20%" style="text-align: right">
        用户名：</td>
    <td class="list_link">
        <asp:TextBox ID="UserNameBox" runat="server" Width="252px" CssClass="form"></asp:TextBox>&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Width="127px" CssClass="form" onchange="javascript:_add(this.options[this.selectedIndex].text);"></asp:DropDownList>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Message_write_0001',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserNameBox"
            Display="Dynamic" ErrorMessage="请输入收信人"></asp:RequiredFieldValidator><br />
        <asp:Label ID="Label1" runat="server" Height="11px" Width="393px" ForeColor="Red"></asp:Label>
    </td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        消息标题：</td>
    <td class="list_link">
        <asp:TextBox ID="TitleBox" runat="server" Width="387px" CssClass="form"></asp:TextBox>&nbsp;
        
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Message_write_0002',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TitleBox"
            Display="Dynamic" ErrorMessage="请输入信息标题"></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        消息内容：</td>
    <td class="list_link">
        <div style="margin-top:3px;">
         <script type="text/javascript" language="JavaScript">
             window.onload = function()
	        {
	        var sBasePath = "../../editor/"
            var oFCKeditor = new FCKeditor('ContentBox') ;
            oFCKeditor.BasePath	= sBasePath ;
            oFCKeditor.ToolbarSet = 'Foosun_User';
            oFCKeditor.Width = '100%' ;
            oFCKeditor.Height = '300' ;	
            oFCKeditor.ReplaceTextarea() ;
            }
        </script>
		<textarea rows="1" cols="1" name="ContentBox" style="display:none" id="ContentBox" runat="server" ></textarea>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Message_write_0003',this)">帮助</span>
    </td>
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">保存消息：</td>
    <td class="list_link">
        <asp:CheckBox ID="CheckBox1" runat="server" Text="保存到草稿箱" />
    </td>
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        重要程度：</td>
    <td class="list_link">
        <asp:RadioButtonList ID="LevelFlagList" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True">普通</asp:ListItem>
            <asp:ListItem>加急</asp:ListItem>
            <asp:ListItem>紧急</asp:ListItem>
        </asp:RadioButtonList>
    </td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        附件：</td>
    <td class="list_link">
        <asp:FileUpload ID="MessFilesUpload" runat="server" Width="389px" CssClass="form" />&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Message_write_0004',this)">帮助</span>
    </td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link"></td>
    <td class="list_link">&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="发送消息" OnClick="Button1_Click" CssClass="form"/>
        &nbsp; &nbsp; &nbsp;&nbsp;<input type="reset" name="Submit3" value="重新填写" class="form">
<%--        &nbsp; &nbsp; &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="保存消息" OnClick="Button2_Click" CssClass="form"/>
--%>     </td>
  </tr>
</table>
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table>
</form>
</body>  
<script language="javascript" type="text/javascript">
function _add(value)
{
    if(value!="请选择")
    {
        var objs = document.getElementById("UserNameBox");
        if(objs.value.indexOf(value)==-1)
        {
            if(objs.value!="")
            {
                objs.value += ','+value;
            }
            else
            {
                objs.value += value ;
            }
        }
    }
}
</script>
</html>
