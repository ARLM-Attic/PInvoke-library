<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Templet_Upload" ResponseEncoding="utf-8" Codebehind="Upload.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>文件上传__<%Response.Write(Foosun.Config.UIConfig.HeadTitle); %></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../js/Public.js"></script>
</head>
<body>
    <form id="f_Upload" runat="server" method="post" action="" enctype="multipart/form-data">
        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" background="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/reght_1_bg_1.gif">
            <tr>
                <td class="sysmain_navi"  style="PADDING-LEFT: 14px" height="30">文件上传</td>
            </tr>
        </table>
        <table width="98%" cellpadding="5" cellspacing="1" class="table" align="center">
            <tr class="TR_BG_list">
                <td class="list_link" style="text-align:left;">
                <input type="file" id="file" name="file" class="form" style="width:400px;" runat="server" />
                <br />
                <asp:CheckBox ID="isWater" Text="加水印" runat="server" />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="isDelineation" runat="server" Text="略缩图" />
                <br /><asp:CheckBox ID="CheckFileTF" runat="server" Enabled="false" Checked="true" Text="如果文件存在则重命名(格式:月日时5位随机数-原文件)." />
                <br /><asp:CheckBox ID="yearDirTF" runat="server" Text="上传创建(年-月)目录" />
                </td>
            </tr>
            <tr class="TR_BG_list">
            <td style="text-align:center;">
                <input type="button" id="tj" value=" 上 传 " onclick="javascript:SubmitClick();"/>
                <input type="button" id="Button1" value=" 关 闭 " onclick="javascript:window.close();"/>
            </td>
            </tr>
        </table>
    </form>
</body>
<script language="javascript" type="text/javascript">
    function SubmitClick()
    {
        if (document.getElementById("file").value=="")
        {
            alert('请选择要上传的文件!');
        }
        else
        {
            <% 
                string Path=Server.UrlEncode(Request.QueryString["Path"]);
                string ParentPath=Server.UrlEncode(Request.QueryString["ParentPath"]);
                string upfiletype=Request.QueryString["UpfilesType"];
            %>
            document.f_Upload.action="Upload.aspx?Type=Upload&Path=<% Response.Write(Path); %>&upfiletype=<% Response.Write(upfiletype); %>&ParentPath=<% Response.Write(ParentPath); %>";
            document.f_Upload.submit();
        }
    }
    function killErrors() 
    { 
        return true; 
    } 
    window.onerror = killErrors; 
 
</script>


</html>
