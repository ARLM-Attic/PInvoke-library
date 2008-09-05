<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_js_JS_Templet_Add" Codebehind="JS_Templet_Add.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet"  type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
<script language="javascript" type="text/javascript">
<!--
function Display(flag)
{
    if(flag==0)
    {
        document.getElementById('TR_Sys').style.display = '';
        document.getElementById('TR_Free').style.display = 'none';
    }
    else
    {
        document.getElementById('TR_Sys').style.display = 'none';
        document.getElementById('TR_Free').style.display = '';
    }
}
function getValue(str)
{
    var oEditor = FCKeditorAPI.GetInstance("ContentTextBox");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml(str);
    }
    else
    {
    return false;
    }
 
}
function loadme(rand)
{
    var f = 1;
    if(document.getElementById('RadSys').checked)
        f = 0;
    Display(f);
}
//-->
</script>

</head>
<body onload="loadme(Math.random())">
    <form id="form1" runat="server">
        <!------头部导航------>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px"><asp:Label runat="server" ID="LblCaption"></asp:Label></td>
                <td width="43%" class="topnavichar" style="padding-left: 14px">
                    位置：<a href="../main.aspx" class="navi_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="JS_Templet.aspx" class="navi_link">JS模型管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:Label runat="server" ID="LblTitle"></asp:Label></td>
            </tr>
        </table>
        <!----功能菜单----->
        <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
            <tr class="menulist">
                <td>
                     功能： <a href="JS_Templet.aspx" class="topnavichar">JS模型管理</a>
                </td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
            <tr class="TR_BG_list">
                <td colspan="2" class="navi_link">
                    <strong>JS模型信息</strong></td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" align="right">
                    名称：</td>
                <td class="list_link" align="left">
                    <asp:TextBox ID="TxtName" runat="server" CssClass="form" Width="212px" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtName"
                        Display="Dynamic" ErrorMessage="请填写名称!" SetFocusOnError="True"></asp:RequiredFieldValidator>(<font color="red"
                        size="2">*</font>)<span class="helpstyle" onclick="Help('H_jstemplet_0001',this)"
                            style="cursor: help;" title="点击查看帮助">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    模型类型：</td>
                <td align="left" class="list_link">
                    <asp:RadioButton ID="RadSys" runat="server" Text="系统JS" GroupName="JSTType" onclick="Display(0);" Checked="True" />&nbsp;<asp:RadioButton
                        ID="RadFree" runat="server" Text="自由JS" GroupName="JSTType" onclick="Display(1);" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jsadd_0003',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    模型分类：</td>
                <td align="left" class="list_link">
                    <asp:DropDownList ID="DdlClass" runat="server" CssClass="form">
                    <asp:ListItem Value="0">根结点</asp:ListItem>
                    </asp:DropDownList>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_jstemplet_0002',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="TR_Free" style="display: none">
                <td class="list_link" align="right">
                    插入内容</td>
                <td class="list_link" align="left">
                  <label id="style_base" runat="server" />
                  <label id="style_class" runat="server" />
                  <label id="style_special" runat="server" />                 

                  <asp:DropDownList CssClass="form" ID="DdlCustom" runat="server" Width="150px" onchange="javascript:setValue(this.value);">
                  <asp:ListItem Value="">自定义字段</asp:ListItem>
                  </asp:DropDownList>  
                  
                  </td>
            </tr>
            <tr class="TR_BG_list" id="TR_Sys">
                <td class="list_link" align="right">
                    插入内容
                </td>
                <td class="list_link" align="left">
                    <span id="adress"></span><a href="#" onclick="javascript:show('List',document.getElementById('adress'),'系统JS模型(创建调用格式)',600,340);"
                        class="list_link" title="包括最新、推荐、热点、头条、滚动、专题、公告、子类">创建调用格式</a><span class="helpstyle"
                            style="cursor: help;" title="点击查看帮助" onclick="Help('H_jstemplet_0005',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" align="right">
                    模型内容</td>
                <td class="list_link" align="left">
                <script type="text/javascript" language="JavaScript">
                window.onload = function()
                    {
                    var sBasePath = "../../editor/"
                    var oFCKeditor = new FCKeditor('ContentTextBox') ;
                    oFCKeditor.BasePath	= sBasePath ;
                    oFCKeditor.Width = '100%' ;
                    oFCKeditor.ToolbarSet = 'Foosun_style';
                    oFCKeditor.Height = '300px' ;	
                    oFCKeditor.ReplaceTextarea() ;
                    }
                </script>
                <textarea rows="1" cols="1" name="ContentTextBox" style="display:none" id="ContentTextBox" runat="server" ></textarea>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td height="26" colspan="2" align="center" class="list_link">
                    <asp:Button ID="BtnOK" runat="server" Text=" 保 存 " CssClass="form" OnClick="BtnOK_Click" />&nbsp;&nbsp;<asp:Button
                        ID="BtnCancel" runat="server" Text=" 取 消 " CssClass="form" />
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
            <tr>
                <td align="center">
                    <%Response.Write(CopyRight);%>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HidID" />
    </form>
</body>
</html>
 <script language="javascript" type="text/javascript">

function setValue(value)
{
    var oEditor = FCKeditorAPI.GetInstance("ContentTextBox");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml('{#FS:define='+value+'}');
    }
    else
    {
    return false;
    }
}
</script>