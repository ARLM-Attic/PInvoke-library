<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_discuss_discussTopi_commentaryEdit" Codebehind="discussTopi_commentaryEdit.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
</head>
<body>
    <form id="form1" runat="server">
    <span id="sc" runat="server"></span>
    <div>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table" id="commentary" style="display:">
    <tr class="TR_BG_list" id="titleTF" runat="server" style="display:">
    <td class="list_link" width="20%" style="text-align: right">
        主题：</td>
    <td class="list_link" width="80%">
        <asp:TextBox ID="title" runat="server" Width="392px" CssClass="form"></asp:TextBox>&nbsp;
            <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussTopi_commentary_0001',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        内容：</td>
    <td class="list_link">
                    <!--编辑器开始-->
        <editor:wysiwygeditor 
            Runat="server"
            ButtonFeatures = "['StyleAndFormatting','TextFormatting','ListFormatting','BoxFormatting','ParagraphFormatting','CssText','Styles','Cut','Copy','Paste','|','Undo','Redo','|','Bold','Italic','Underline','|','JustifyLeft','JustifyCenter','JustifyRight','JustifyFull','|','Numbering','Bullets','Indent','Outdent','|','ForeColor','Hyperlink','Clean']"
            StyleList="[['BODY',false,'','font-size:14px;font-family:Verdana,Arial,Helvetica;line-height:20px']]"
            EditorHeight="200"
            EditorWidth="750"
            scriptPath="../../Editor/scripts/"
            Content=""
            ID="contentBox" />
        <!--编辑器结束-->
             <asp:HiddenField ID="DtIDs" runat="server" />
        </td>
        
    </tr>
    <tr class="TR_BG_list">
        
    <td class="list_link"></td>
    <td class="list_link">
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Button ID="subset" runat="server" Text="修改" CssClass="form" OnClick="subset_Click"/>
        &nbsp; &nbsp;&nbsp; &nbsp;<input type="reset" name="Submit3"  class="form" value="重 置"/>
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>