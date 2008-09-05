<%@ Page Language="C#" AutoEventWireup="true" Inherits="Discuz.Space.Admin.SpaceApplySetting" %>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register Src="../UserControls/PageInfo.ascx" TagName="PageInfo" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>spaceapplysetting</title>
		<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
		<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" src="../js/common.js"></script>
		<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />		
		<script type="text/javascript">
		    function ChanageUserGroupStatus(status)
		    {
		        var i = 0;
	            while(true)
	            {
	                var obj = $("UserGroup_" + i);
	                if(obj == null) break;
	                obj.disabled = !status;
	                obj.checked = status;
	                i++;
	            } 
		    }
		    function ShowHiddenOption(status)
		    {
		        $("ShowSpaceOption").style.display = status ? "block" : "none";
		        $("ShowUserGroup").style.display = status ? "block" : "none";
		    }		    
        </script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
            <uc1:PageInfo ID="info1" runat="server" Icon="information" Text="只有选择开通个人空间并选中选项名称前的复选框，该项才会发挥作用" />
			<asp:Panel id="searchtable" runat="server" Visible="true">
			<div class="ManagerForm">
		    <fieldset>
		    <legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">申请设置</legend>
		    <table cellspacing="0" cellpadding="4" width="100%" align="center">
	            <tr>
                    <td class="panelbox" colspan="2">
                        <table width="100%">
                            <tr>
				                <td style="width: 140px">是否启用个人空间服务:</td>
				                <td>
				                    <cc1:RadioButtonList id="EnableSpace" runat="server">
								        <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
								        <asp:ListItem Value="0">否</asp:ListItem>
							        </cc1:RadioButtonList>
				                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
		    <div id="ShowSpaceOption" runat="server">
		    <table cellspacing="0" cellpadding="4" width="100%" align="center">
		        <tr>
                    <td class="panelbox" align="left" width="50%">
                        <table width="100%">
                            <tr>
				                <td style="width: 140px"><asp:CheckBox id="allowPostcount" runat="server" />论坛发帖数超过:</td>
				                <td><cc1:TextBox id="Postcount" runat="server" width="60" RequiredFieldType="数据校验"></cc1:TextBox></td>
                            </tr>
			                <tr>
				                <td style="width: 140px"><asp:CheckBox id="allowDigestcount" runat="server" />论坛精华帖数超过:</td>
				                <td><cc1:TextBox id="Digestcount" runat="server" width="60" RequiredFieldType="数据校验"></cc1:TextBox></td>
			                </tr>
                            <tr>
				                <td><asp:CheckBox id="allowScore" runat="server" />论坛用户积分超过:</td>
				                <td><cc1:TextBox id="Score" runat="server" width="60" RequiredFieldType="数据校验"></cc1:TextBox></td>
                            </tr>
                            <tr>
				                <td style="width: 140px">达到以上条件后<br />申请是否自动开通?</td>
				                <td>
					                <cc1:RadioButtonList id="ActiveType" runat="server">
						                <asp:ListItem Value="1" Selected="True">自动开通</asp:ListItem>
						                <asp:ListItem Value="0">手动审核</asp:ListItem>
					                </cc1:RadioButtonList>
				                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="panelbox" align="right" width="50%">
                        <table width="100%">
			                <tr>
				                <td><asp:CheckBox id="allowUserGroup" runat="server" />属于用户组:</td>
				                <td><cc1:CheckBoxList id="UserGroup" Runat="server"  RepeatColumns="2"/></td>
			                </tr>
                            <tr>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
			</div>
			</fieldset>
			<div id="ShowUserGroup" runat="server">
		    <fieldset>
		    <legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">空间附件最大空间配置</legend>
		    <table cellspacing="0" cellpadding="4" width="100%" align="center"  id="groupattachsize" runat="server">
		    </table>
		    </fieldset>
		    </div>
			<div class="Navbutton"><cc1:Button id="Submit" runat="server" designtimedragdrop="247" Text="提 交" OnClick="Submit_Click"></cc1:Button></div>
			</div>
			</asp:Panel>
		</form>
	<script type="text/javascript">
	if(document.getElementById("<%=allowUserGroup.ClientID%>").checked==false)
	    ChanageUserGroupStatus(false);
   </script>
   <%=footer%>
	</body>
</html>

