<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_FreeLabel_AddEnd" Codebehind="FreeLabel_AddEnd.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
    rel="stylesheet" type="text/css" />
<style type="text/css">
p{padding-left:20px;padding-top:0px;padding-bottom:0px;margin-top:3px;margin-bottom:0px;}
</style>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
<script language="javascript" type="text/javascript">
<!--
function AddTag(val)
{
    var oEditor = FCKeditorAPI.GetInstance("EdtContent");
    var str = val.trim();
    if(str != '')
    {
        if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
        {
             oEditor.InsertHtml(str);
        }
    }
}
function AddDate()
{
    var str = document.getElementById('TxtDateStyle').value.trim();
    if(str != '')
    {
        str = '[$'+ str +'$]'
        AddTag(str);
    }
}
//-->
</script>
</head>
<body>
    <form id="Form1" runat="server">
        <div>
            <table id="top1" width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
                <tr>
                    <td height="1" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td width="57%" class="sysmain_navi" style="padding-left: 14px">
                        <asp:Label ID="LblCaption" runat="server" Text="添加自由标签"></asp:Label></td>
                    <td width="43%" class="topnavichar" style="padding-left: 14px">
                        <div align="left">
                            位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a
                                href="FreeLabel_List.aspx" target="sys_main" class="list_link">自由标签管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:Label
                                    ID="LblNavigt" runat="server" Text="添加自由标签"></asp:Label></div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td style="padding-left: 14px">
                        <a class="topnavichar" href="javascript:history.back();" id="PreSteps" runat="server">上一步</a> <asp:LinkButton CssClass="topnavichar" ID="LnkBtnSave" runat="server" OnClick="LnkBtnSave_Click">保存</asp:LinkButton>  <asp:LinkButton CssClass="topnavichar" ID="reviewBtn" runat="server" OnClick="reviewBtn_Click">预览</asp:LinkButton></td>
                </tr>
            </table>
            <div runat="server" id="review"></div>
            <table width="98%" cellpadding="5" cellspacing="1" align="center" class="table">
                <tr class="TR_BG_list">
                    <td align="right" width="15%">标签名称：</td>
                    <td width="85%">
                        <asp:TextBox runat="server" Width="200" ID="TxtLabelName" CssClass="form"></asp:TextBox>
                    </td>
                </tr>
                <tr class="TR_BG_list">
                    <td align="right">标签说明：</td>
                    <td>
                        <asp:TextBox runat="server" Width="200" ID="TxtDescrpt" CssClass="form" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        自定义：</td>
                    <td>
                        <input type="button" value="循环内容" onclick="javascript:AddTag('{# 您要加的内容 #}')" />  <input type="button" value="不循环内容" onclick="javascript:AddTag('{*记录序号 您要加的内容 *}')" />  <input type="button" value="函数" onclick="javascript:AddTag('(#函数内容#)')" /></td>
                </tr>
                <tr class="TR_BG_list">
                    <td align="right">可用字段：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="DdlField1" onchange="AddTag(this.options[this.selectedIndex].value)" >
                            <asp:ListItem Value="">请选择字段</asp:ListItem></asp:DropDownList> ┆ <asp:DropDownList runat="server" ID="DdlField2" onchange="AddTag(this.options[this.selectedIndex].value)" ><asp:ListItem Value="">请选择字段</asp:ListItem>
</asp:DropDownList>
                        <span style="color:Red">新闻编号和新闻栏目编号会自动替换为连接</span></td>
                </tr>
                <tr class="TR_BG_list">
                    <td align="right">日期样式：</td>
                    <td>
                        <asp:TextBox runat="server" ID="TxtDateStyle" Text="YY02年MM月DD日" CssClass="form" MaxLength="200"></asp:TextBox> <input type="button" value=" 插入 " onclick="AddDate()" />
                        <span style="color:Red">需要选择时间字段，格式见说明 2</span>
                    </td>
                </tr>
                <tr class="TR_BG_list">
                    <td align="right">标签内容：</td>
                    <td>
                        <span style="color:Red">由HTML代码加所选择字段、自定义函数组成，用来定义查询记录的显示样式</span>
                    </td>
                </tr>
                <tr class="TR_BG_list">
                    <td colspan="2">
                        <script type="text/javascript" language="JavaScript">
                        window.onload = function()
                            {
                            var sBasePath = "../../editor/"
                            var oFCKeditor = new FCKeditor('EdtContent') ;
                            oFCKeditor.BasePath	= sBasePath ;
                            oFCKeditor.Width = '100%' ;
                            oFCKeditor.ToolbarSet = 'Foosun_style';
                            oFCKeditor.Height = '200px' ;	
                            oFCKeditor.ReplaceTextarea() ;
                            }
                        </script>
                        <textarea rows="1" cols="1" name="EdtContent" style="display:none" id="EdtContent" runat="server" ></textarea>
                    </td>
                </tr>
            </table>
            <div style="color:Red">
            <p>说明：</p>
            <p>1.预定义字段需要选择各自对应编号。如新闻浏览路径需要选择新闻编号，栏目浏览路径需要选择栏目编号(注意：是新闻编号，不是编号)。</p>
            <p>2.日期格式:YY02代表2位的年份(如06表示2006年),YY04表示4位数的年份(2006)，MM代表月，DD代表日，HH代表小时，MI代表分，SS代表秒。</p>  
            <p>3.自定义函数：循环内容{#...#}、不循环内容{*n...*}(n>0)代表记录序号、函数(#...#)；如(#Left([*FS_News.Title*],20)#) </p> 
            </div>
            <br />
            <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
                style="height: 76px">
                <tr>
                    <td align="center">
                        <%Response.Write(CopyRight);%>
                    </td>
                </tr>
            </table>
        </div>
         <asp:HiddenField ID="HidID" runat="server" />
         <asp:HiddenField ID="HidName" runat="server" />
         <asp:HiddenField ID="HidSQL" runat="server" />
    </form>
</body>
</html>

