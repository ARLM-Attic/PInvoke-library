﻿<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Page language="c#" Inherits="Discuz.Plugin.Spread.Admin.CreditStrategy" %>
<%@ Register Src="../UserControls/PageInfo.ascx" TagName="PageInfo" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>推广插件设置</title>
		<script type="text/javascript" src="../js/common.js"></script>
		<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
</head>
	<body>
		<form id="Form1" method="post" runat="server">
        <uc1:PageInfo ID="info1" runat="server" Icon="information" Text="您所编辑的字段必段是在[<a href=../global/global_scoreset.aspx>积分设置</a>]中指定了积分字段的名称, 未指定项无效" />
        <div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;"><a name="#comment"></a><asp:Literal id="Literal1" runat="server"></asp:Literal></legend>
			<table width="100%" align="center" class="table1" cellspacing="0" cellpadding="4"  bgcolor="#C3C7D1">
				<tr>
					<td class="td2" width="20%">转向地址</td>
					<td class="td2" colspan="3" >
						<cc1:TextBox id="txtTransferUrl" runat="server" Width="264px" HintInfo="设置推广链接的转向地址"></cc1:TextBox></td>
				</tr>
				<tr>
					<td class="td1" width="20%"><asp:Literal id="extcredits1name" runat="server" Text="extcredits1"></asp:Literal></td>
					<td class="td1" width="30%"><cc1:TextBox id="extcredits1" runat="server" RequiredFieldType="数据校验" Width="64px"></cc1:TextBox></td>
					<td class="td2" width="20%"><asp:Literal id="extcredits2name" runat="server" Text="extcredits2"></asp:Literal></td>
					<td class="td2" width="30%"><cc1:TextBox id="extcredits2" runat="server" RequiredFieldType="数据校验" Width="64px"></cc1:TextBox></td>
				</tr>
				<tr>
					<td class="td1"><asp:Literal id="extcredits3name" runat="server" Text="extcredits3"></asp:Literal></td>
					<td class="td1"><cc1:TextBox id="extcredits3" runat="server" Width="64px" RequiredFieldType="数据校验"></cc1:TextBox></td>
					<td class="td2"><asp:Literal id="extcredits4name" runat="server" Text="extcredits4"></asp:Literal></td>
					<td class="td2"><cc1:TextBox id="extcredits4" runat="server" Width="64px" RequiredFieldType="数据校验"></cc1:TextBox></td>
				</tr>
				<tr>
					<td class="td1"><asp:Literal id="extcredits5name" runat="server" Text="extcredits5"></asp:Literal></td>
					<td class="td1"><cc1:TextBox id="extcredits5" runat="server" Width="64px" RequiredFieldType="数据校验"></cc1:TextBox></td>
					<td class="td2"><asp:Literal id="extcredits6name" runat="server" Text="extcredits6"></asp:Literal></td>
					<td class="td2"><cc1:TextBox id="extcredits6" runat="server" Width="64px" RequiredFieldType="数据校验"></cc1:TextBox></td>
				</tr>
				<tr>
					<td class="td1"><asp:Literal id="extcredits7name" runat="server" Text="extcredits7"></asp:Literal></td>
					<td class="td1"><cc1:TextBox id="extcredits7" runat="server" Width="64px" RequiredFieldType="数据校验"></cc1:TextBox></td>
					<td class="td2"><asp:Literal id="extcredits8name" runat="server" Text="extcredits8"></asp:Literal></td>
					<td class="td2"><cc1:TextBox id="extcredits8" runat="server" Width="64px" RequiredFieldType="数据校验" ></cc1:TextBox></td>
				</tr>
			</table>
			</fieldset>
			<cc1:Hint ID="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
			<div align="center">
			<cc1:Button id="SaveInfo" runat="server" Text=" 提 交 "></cc1:Button>&nbsp;&nbsp;
			<cc1:Button id="DeleteSet" runat="server" Text="删除设置" ButtonImgUrl="../images/del.gif"></cc1:Button>
			</div>
			</div>			
			<%=footer%>
		</form>
	</body>
</html>
