<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="special_templet.aspx.cs" Inherits="Foosun.Web.manage.news.special_templet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding:10px;">
        
        
    
        <asp:ListBox ID="splist" runat="server"   Height="200" SelectionMode="Multiple" Width="287px">
        
        </asp:ListBox>
        <br />
        选择模板：
        <asp:TextBox ID="templet" runat="server" MaxLength="200" Width="40%" CssClass="form"></asp:TextBox><img src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor:pointer;" onclick="selectFile('templet',document.form1.templet,250,500);document.form1.templet.focus();" />
        &nbsp;<asp:Button ID="Button1" runat="server" Text="提交捆绑" OnClick="Button1_Click" />
        
        </div>
    </form>
    
</body>
</html>
