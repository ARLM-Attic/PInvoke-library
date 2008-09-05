<%@ Page Language="C#" AutoEventWireup="true" Inherits="configuration_system_Upload_user" Codebehind="Upload_user.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>文件上传__<%Response.Write(Foosun.Config.UIConfig.HeadTitle); %></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
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
                <td class="list_link" style="text-align:left; padding-left:30px;" nowrape>
                    文件保存命名方式：<asp:RadioButtonList ID="radFileType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">文件名不变</asp:ListItem>
                        <asp:ListItem Value="1">&quot;副件&quot;+文件名</asp:ListItem>
                        <asp:ListItem Value="2">1+&quot;文件名&quot;</asp:ListItem>
                        <asp:ListItem Value="3">当前时间</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" style="text-align:center;">
                <input type="file" id="file" name="file" class="form" style="width:400px;" runat="server" />
                </td>
            </tr>
            <tr class="TR_BG_list">
            <td style="text-align:center;">
                <input type="button" id="tj" value=" 上 传 " onclick="javascript:SubmitClick();"/>
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
                string FileType=Request.QueryString["FileType"];
            %>
            document.f_Upload.action="Upload_user.aspx?Type=Upload&Path=<% Response.Write(Path); %>&FileType=<%Response.Write(FileType);%>&ParentPath=<% Response.Write(ParentPath);%>";
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
