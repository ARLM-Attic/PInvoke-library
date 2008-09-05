<%@ Page Language="C#" AutoEventWireup="true" Inherits="Discuz.Album.Admin.AlbumConfig" %>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
        function ShowHiddenOption(status)
	    {
	        $("ShowAlbumOption").style.display = status ? "block" : "none";
	        $("ShowUserGroup").style.display = status ? "block" : "none";
	    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">相册配置</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
					        <td style="width: 110px">是否启用相册服务:</td>
					        <td>
					            <cc1:RadioButtonList id="EnableAlbum" runat="server">
					                <asp:ListItem Value="1">是</asp:ListItem>
					                <asp:ListItem Value="0">否</asp:ListItem>
				                </cc1:RadioButtonList>
					        </td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" width="50%" align="right">
		            <div id="ShowAlbumOption" runat="server">
                    <table width="100%">
                        <tr>
					        <td style="width: 200px">允许每个用户建立最大相册数上限:</td>
					        <td><cc1:TextBox id="maxalbumcount" runat="server" RequiredFieldType="数据校验" Size="5"  MaxLength="4"></cc1:TextBox></td>
                        </tr>
                    </table>
                    </div>
                </td>
            </tr>
        </table>
		</fieldset>
		<div id="ShowUserGroup" runat="server">
		<fieldset>
		<legend style="background:url(../images/icons/icon55.jpg) no-repeat 6px 50%;">相册大小配置</legend>
		<table width="100%" id="groupphotosize" align="center" class="table1" cellspacing="0" cellPadding="4"  bgcolor="#C3C7D1" runat="server">
		</table>
		</fieldset>
		</div>
		<div class="Navbutton"><cc1:Button id="SaveCombinationInfo" runat="server" Text=" 提 交 " OnClick="SaveCombinationInfo_Click"></cc1:Button></div>
		</div>
    </form>
    <%=footer%>
</body>
</html>
