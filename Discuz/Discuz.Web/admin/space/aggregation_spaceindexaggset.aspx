<%@ Page language="c#" Inherits="Discuz.Space.Admin.SpaceIndexAggset"%>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>CacheManage</title>
		<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" /> 
  </head>
	<body>
		<form id="Form1" method="post" runat="server">
		<div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">自动提取数据</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
                            <td style="width:120px">提取最新空间评论条数:</td>
                            <td><cc1:TextBox id="newcommentcount" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="0"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最多评论日志条数:</td>
                            <td><cc1:TextBox id="maxarticlecommentcount" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="0"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最多访问日志条数:</td>
                            <td><cc1:TextBox id="maxarticleviewcount" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="0"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最多评论空间条数:</td>
                            <td><cc1:TextBox id="maxcommentcount" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="0"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最多访问空间条数:</td>
                            <td><cc1:TextBox id="maxspaceviewcount" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="0"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最近更新的空间条数:</td>
                            <td><cc1:TextBox id="updatespacecount" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="0"></cc1:TextBox></td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" width="50%" align="right">
                    <table width="100%">
                        <tr>
                            <td style="width: 120px">提取最新空间评论时间间隔(分钟):</td>
                            <td><cc1:TextBox id="newcommentcounttimeout" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="10" MaximumValue="300"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最多评论日志时间间隔(分钟):</td>
                            <td><cc1:TextBox id="maxarticlecommentcounttimeout" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="10" MaximumValue="300"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最多访问日志时间间隔(分钟):</td>
                            <td><cc1:TextBox id="maxarticleviewcounttimeout" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="10" MaximumValue="300"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最多评论空间时间间隔(分钟):</td>
                            <td><cc1:TextBox id="maxcommentcounttimeout" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="10" MaximumValue="300"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最多访问空间时间间隔(分钟):</td>
                            <td><cc1:TextBox id="maxspaceviewcounttimeout" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="10" MaximumValue="300"></cc1:TextBox></td>
                        </tr>
                        <tr>
                            <td>提取最近更新的空间时间间隔(分钟):</td>
                            <td><cc1:TextBox id="updatespacetimeout" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="10" MaximumValue="300"></cc1:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
<div style="display:none">		
<!--因前台显示已去掉所以在此隐藏-->			
					<tr align="center">
						<td  width="50%" class="td1">
						提取最多发帖数空间条数:<cc1:TextBox id="maxpostarticlespacecount" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="0"></cc1:TextBox>
						</td>
						<td class="td1">
						提取最多发帖数空间时间间隔(分钟):<cc1:TextBox id="maxpostarticlespacecounttimeout" runat="server" size="3" MaxLength="4" CanBeNull="必填" MinimumValue="10" MaximumValue="300"></cc1:TextBox>
						</td>
					</tr>
</div>					
			</fieldset><br />
			<div align="center">
			<cc1:Button id="Btn_SaveInfo" runat="server" Text="  保存  " ButtonImgUrl="../images/submit.gif"></cc1:Button>
			</div>
			</div>
		</form>
		<%=footer%>
	</body>
</html>
