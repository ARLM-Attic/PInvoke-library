<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_Class_ToTemplet" Codebehind="Class_ToTemplet.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js">
</script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>

<body>
<form id="form1" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px; height: 32px;" >
              批量设置<span class="helpstyle" style="cursor:hand;" title="点击查看帮助" onclick="Help('H_news__0001',this)">(帮助)</span></td>
          <td width="43%" class="topnavichar"  style="PADDING-LEFT: 14px; height: 32px;" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="class_list.aspx" target="sys_main" class="topnavichar">栏目管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />批量设置</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
<tr>
  <td>功能：<a href="class_list.aspx" class="topnavichar">栏目首页</a>&nbsp; &nbsp;&nbsp;
  </td>
</tr>
</table>
<table width="98%" border="0" cellpadding="8" cellspacing="0" bgcolor="#FFFFFF" class="table" style="height: 76px" align="center">
  <tr  class="TR_BG_list">
    <td  align="center" style="width:32%;height:142;" valign="top">
        <asp:ListBox ID="DataListBox" runat="server" Height="320px" Width="250px" SelectionMode="Multiple"></asp:ListBox>
        <table border="0" cellspacing="0" cellpadding="0" style="width: 87%">
          <tr>
            <td align="center" valign="top"><input name="button2" type="button" class="form" id="B_Class2" onclick="javascript:SelectAllClass(1);" value="全选" />
              &nbsp;&nbsp;
              <input name="button22" type="button" class="form" id="Button6" onclick="javascript:SelectAllClass(0);" value="取消选定栏目" />
              &nbsp;&nbsp;
              <input name="button22" type="button" class="form" id="Button1" onclick="javascript:Selectflg();" value="反选" /></td>
          </tr>
        </table></td>
    <td align="center" style="width: 1px"></td>
    <td style="width:68%" valign="top">
    <table width="100%" border="0" cellspacing="3" cellpadding="0" id="ChangeText">
<%--      <tr>
        <td>
            <asp:CheckBox ID="allCheck" runat="server" />按照以下方式全部更新,未填写的将更新为空；不选择,则不更新未填写项目；</td>
      </tr>
--%>    </table>    
      <table width="100%" border="0" cellspacing="1" cellpadding="3" class="table">
<%--        <tr class="TR_BG_list">
          <td align="right" style="height: 19px; width: 108px;">属性：</td>
          <td align="left" style="height: 19px; width: 397px;">
              <asp:CheckBoxList ID="checkeditem" runat="server" RepeatDirection="Horizontal">
                  <asp:ListItem>评论</asp:ListItem>
                  <asp:ListItem>导航中显示</asp:ListItem>
            </asp:CheckBoxList></td>
        </tr>--%>
        <tr class="TR_BG_list">
          <td align="right" style="height: 19px; width: 108px;">栏目列表模板：</td>
          <td align="left" style="height: 19px; width: 397px;">&nbsp;<asp:TextBox ID="Itemtemplets" runat="server"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择模板"  onclick="selectFile('templet',document.form1.Itemtemplets,280,500);document.form1.Itemtemplets.focus();" /></td>
        </tr>
        <tr class="TR_BG_list">
          <td align="right" style="height: 18px; width: 108px;">新闻浏览模板：</td>
          <td align="left" style="height: 18px; width: 397px;">&nbsp;<asp:TextBox ID="displaytemplets" runat="server"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择模板"  onclick="selectFile('templet',document.form1.displaytemplets,280,500);document.form1.displaytemplets.focus();" />
              <asp:CheckBox ID="isContent" Text="更新栏目下的新闻模板" runat="server" />
              </td>
        </tr>
<%--        <tr class="TR_BG_list">
          <td align="right" style="height: 19px; width: 108px;">栏目保存路径：</td>
          <td align="left" style="height: 19px; width: 397px;">&nbsp;<asp:TextBox ID="itemPath" runat="server"></asp:TextBox>
          <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择路径"  onclick="selectFile('path',document.form1.itemPath,300,500);document.form1.itemPath.focus();"/></td>
        </tr>
        <tr class="TR_BG_list">
          <td align="right" style="height: 27px; width: 108px;">新闻保存路径：</td>
          <td align="left" style="height: 27px; width: 397px;">&nbsp;<asp:TextBox ID="newsPath" runat="server"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择路径"  onclick="selectFile('path|<%Response.Write(DirHtml); %>',document.form1.newsPath,300,500);document.form1.newsPath.focus();" /></td>
        </tr>
        <tr class="TR_BG_list">
          <td height="26" align="right" style="width: 108px">栏目目录规则：</td>
          <td align="left" style="width: 397px">&nbsp;<asp:TextBox ID="dirStyle" runat="server"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="栏目规则"  onclick="selectFile('rulesmallPram',document.form1.dirStyle,100,350);document.form1.dirStyle.focus();" /></td>
        </tr>
        <tr class="TR_BG_list">
          <td height="26" align="right" style="width: 108px">栏目文件名规则：</td>
          <td align="left" style="width: 397px">&nbsp;<asp:TextBox ID="fileStyle" runat="server"></asp:TextBox>
             <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="栏目名规则"  onclick="selectFile('rulesmallPram',document.form1.fileStyle,100,350);document.form1.fileStyle.focus();" /></td>
        </tr>
        <tr class="TR_BG_list" style="display:none;">
          <td height="26" align="right" style="width: 108px">索引页规则：</td>
          <td align="left" style="width: 397px">&nbsp;<asp:TextBox ID="IndexStyle" runat="server"></asp:TextBox>
             <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="索引页规则"  onclick="selectFile('rulePram',document.form1.IndexStyle,100,350);document.form1.IndexStyle.focus();" /></td>
        </tr>
        <tr class="TR_BG_list">
          <td height="23" align="right" style="width: 108px">浏览页规则：</td>
          <td align="left" style="width: 397px">&nbsp;<asp:TextBox ID="displayStyle" runat="server"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="浏览页规则"  onclick="selectFile('rulePram',document.form1.displayStyle,100,350);document.form1.displayStyle.focus();" /></td>
        </tr>
        <tr class="TR_BG_list">
          <td height="24" align="right" style="width: 108px">图片上传目录：</td>
          <td align="left" style="width: 397px">&nbsp;<asp:TextBox ID="uploadDir" runat="server"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="图片上传目录"  onclick="selectFile('path',document.form1.uploadDir,300,400);document.form1.uploadDir.focus();" /></td>
        </tr>
        <tr class="TR_BG_list">
          <td height="25" align="right" style="width: 108px">
              文件的扩展名：</td>
          <td align="left" style="width: 397px">&nbsp;<asp:DropDownList ID="ExFileName" runat="server">
              <asp:ListItem>.html</asp:ListItem>
              <asp:ListItem>.htm</asp:ListItem>
              <asp:ListItem>.shtml</asp:ListItem>
              <asp:ListItem>.aspx</asp:ListItem>
              </asp:DropDownList></td>
        </tr>--%>
      </table>
    </td>
  </tr>
<tr>
    <td colspan="4" align="left" style="text-align: center"  class="TR_BG_list"><asp:Button ID="btn" CssClass="form" runat="server" Text="批量绑定数据" OnClick="btn_Click" /></td>
</tr>
</table>
</form>
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
   <tr>
     <td align="center"><%Response.Write(CopyRight); %></td>
   </tr>
</table>
</body>
</html>
<script language="javascript">
function SelectAllClass(VarInt)
{
    var obj=document.form1.DataListBox;
    for(var i=0;i<obj.length;i++)
    {
        if(VarInt==1)
            obj.options[i].selected=true;
        else
            obj.options[i].selected=false;
    }
}

function Selectflg()
{
    var obj= document.form1.DataListBox;
    for(var i=0;i<obj.length;i++)
    {
        if(obj.options[i].selected)
            obj.options[i].selected=false;
        else
            obj.options[i].selected=true;
    }
}

</script>