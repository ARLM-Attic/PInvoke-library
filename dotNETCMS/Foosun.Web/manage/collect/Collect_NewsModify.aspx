<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_collect_Collect_NewsModify" Codebehind="Collect_NewsModify.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>FoosunCMS For .NET v1.0.0</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Public.js"></script>
    <script type="text/javascript" src="../../editor/fckeditor.js"></script>
    <script language="javascript" type="text/javascript">
    <!--
    function ChangeUrl()
    {
        var obj = document.getElementById("A_Preview");
        obj.href = obj.parentNode.firstChild.value;
    }
    function ChooseEncode(obj)
    {
        obj.parentNode.firstChild.value = obj.innerText;
    }
    //-->
    </script>
</head>
<body>
    <form id="Form2" runat="server">
        <div>
            <table id="top1" width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
                <tr>
                    <td height="1" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td width="57%" class="sysmain_navi" style="padding-left: 14px">
                        采集系统</td>
                    <td width="43%" class="topnavichar" style="padding-left: 14px">
                        <div align="left">
                            位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />
                            <a href="Collect_News.aspx" target="sys_main" class="list_link">新闻处理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改采集新闻
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td>
                        功能：<a class="topnavichar" href="Collect_RuleList.aspx">关键字过滤</a>&nbsp;┊&nbsp;<a class="topnavichar" href="Collect_News.aspx">新闻处理</a></td>
                </tr>
            </table>
            
        <table id="tabList" width="98%" border="0" align="center" cellpadding="5" cellspacing="1"
            class="table">
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    新闻标题:
                </td>
                <td class="list_link" width="70%">
                    <asp:TextBox runat="server" ID="TxtTitle" Width="98%" MaxLength="100" CssClass="form"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写栏目名称!"
                        ControlToValidate="TxtTitle" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    新闻联接:
                </td>
                <td class="list_link" width="70%">
                    <asp:TextBox runat="server" ID="TxtLink" Width="98%" MaxLength="200" ReadOnly="true" CssClass="form"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtLink" ErrorMessage="链接地址不能为空!" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    采集站点:
                </td>
                <td class="list_link" width="70%">
                    <asp:DropDownList runat="server" ID="DdlSite"></asp:DropDownList>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    作&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;者:
                </td>
                <td class="list_link" width="70%">
                    <asp:TextBox runat="server" ID="TxtAuthor" Width="98%" MaxLength="100" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    来&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;源:
                </td>
                <td class="list_link" width="70%">
                    <asp:TextBox runat="server" ID="TxtSource" MaxLength="100" Width="98%" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    采集日期:
                </td>
                <td class="list_link" width="70%">
                    <asp:TextBox runat="server" ID="TxtDate" MaxLength="25" Width="98%" CssClass="form"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    入库后所属栏目:
                </td>
                <td class="list_link" width="70%">
                    <asp:TextBox ID="TxtClassName" Width="200px" runat="server" CssClass="form"></asp:TextBox>
                            <img src="../../sysImages/folder/s.gif" alt="选择栏目" style="cursor: pointer;" onclick="selectFile('newsclass',new Array(document.getElementById('HidClassID'),document.getElementById('TxtClassName')),300,500);document.getElementById('TxtClassName').focus();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtClassName"
                                ErrorMessage="请选择所属新闻栏目" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="HidClassID" runat="server" Value="" />
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    实际采集时间:
                </td>
                <td class="list_link" width="70%">
                    <asp:Label runat="server" ID="LblClTime"></asp:Label>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="30%" align="center">
                    代&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码:
                </td>
                <td class="list_link" width="70%">
               <script type="text/javascript" language="JavaScript">
                  window.onload = function()
                    {
                    var sBasePath = "../../editor/"
                    var oFCKeditor = new FCKeditor('EdtContent') ;
                    
                    oFCKeditor.BasePath	= sBasePath ;
                    oFCKeditor.Width = '100%' ;
                    oFCKeditor.Height = '300' ;	
                    oFCKeditor.ToolbarSet = 'Foosun_Basicstyle';
                    oFCKeditor.ReplaceTextarea() ;
                    }
                </script>
	            <textarea rows="1" cols="1" name="EdtContent" style="display:none" id="EdtContent" runat="server" ></textarea>
		                        
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" colspan="2" align="center">
                    <asp:HiddenField ID="HidNewsID" runat="server" />
                    <asp:Button ID="BtnOK" Text=" 保 存 " runat="server" CssClass="form" OnClick="BtnOK_Click" />
                    <asp:Button ID="Button1" Text=" 重 置 " runat="server" CssClass="form" CausesValidation="False" />
                </td>
            </tr>
        </table>  
            <br />
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
    </form>
</body>
</html>
