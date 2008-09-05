﻿<%@ Page language="c#" Inherits="Discuz.Web.Admin.auditingtopic" Codebehind="forum_auditingtopic.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxPostInfo" Src="../UserControls/AjaxPostInfo.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
  <head>
		<title>AuditingTopic</title>	
		<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
        <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />        
        <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
        <script type="text/javascript" src="../js/modalpopup.js"></script>
		<script type="text/javascript" src="../js/common.js"></script>
    </head>

	<body >
		<form id="Form1" runat="server">
		<div class="ManagerForm">
        <fieldset>
		<legend style="background:url(../images/icons/icon19.jpg) no-repeat 6px 50%;">搜索帖子</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" align="left" width="50%">
                    <table width="100%">
                        <tr>
					        <td style="width: 90px">所在论坛:</td>
					        <td><cc1:dropdowntreelist id="forumid" runat="server"></cc1:dropdowntreelist></td>
                        </tr>
                        <tr>
					        <td>标题关键字:</td>
					        <td>
				            <cc1:TextBox id="title" runat="server" Width="150px" HintTitle="提示" HintInfo="多关键字之间请用半角逗号 &amp;quot;,&amp;quot; 分割" RequiredFieldType="暂无校验"></cc1:TextBox>
				            </td>
                        </tr>
                        <tr>
					        <td>帖子发表<br />时间范围:</td>
					        <td>
				              起始日期:<cc1:Calendar id="postdatetimeStart" runat="server" HintTitle="提示" HintInfo="格式 yyyy-mm-dd, 不限制请留空" ReadOnly="False" ScriptPath="../js/calendar.js"></cc1:Calendar><br />
				              结束日期:<cc1:Calendar ID="postdatetimeEnd" runat="server" HintInfo="格式 yyyy-mm-dd, 不限制请留空" HintTitle="提示" ReadOnly="False" ScriptPath="../js/calendar.js" />
				            </td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" align="left" width="50%">
                    <table width="100%">
				        <tr>
					        <td style="width: 90px">原帖作者:</td>
					        <td>
				          <cc1:TextBox id="poster" runat="server" HintTitle="提示" HintInfo="多个用户名之间请用半角逗号 &amp;quot;,&amp;quot; 分割" Width="150px" RequiredFieldType="暂无校验"></cc1:TextBox></td>
				        </tr>
				        <tr>  
					        <td>删帖管理员:</td>
					        <td>
				          <cc1:TextBox id="moderatorname" runat="server" Width="150px" RequiredFieldType="暂无校验"></cc1:TextBox></td>
				        </tr>
				        <tr>
					        <td>删帖时间范围:</td>
					        <td>
				              起始日期:<cc1:Calendar id="deldatetimeStart" runat="server" HintTitle="提示" HintInfo="格式 yyyy-mm-dd, 不限制请留空" ReadOnly="False" ScriptPath="../js/calendar.js"></cc1:Calendar><br />
				              结束日期:<cc1:Calendar id="deldatetimeEnd" runat="server" HintTitle="提示" HintInfo="格式 yyyy-mm-dd, 不限制请留空" ReadOnly="False" ScriptPath="../js/calendar.js"></cc1:Calendar>
				            </td>
				        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center"><cc1:Button id="SearchTopicAudit" runat="server" Text=" 搜索符合条件的被删帖子 "></cc1:Button></td>
            </tr>
        </table>
		</fieldset>
		</div>
		<div class="ManagerForm">
        <fieldset>
		    <legend style="background:url(../images/icons/icon46.jpg) no-repeat 6px 50%;">清除回帖站</legend>
			<table class="table1" cellspacing="0" cellpadding="4" width="100%">
				<tr>
					<td style="width: 200px">清空多少天以前的回收站主题:</td>
					<td style="width: 100px">
				        <cc1:TextBox ID="RecycleDay" runat="server" HintInfo="0 为清空全部" HintTitle="提示"
                        RequiredFieldType="数据校验" Text="30" Size="5"/>
                    </td>
                  <td><cc1:Button id="DeleteRecycle" runat="server" Text=" 批量清空回收站 " OnClick="DeleteRecycle_Click"></cc1:Button></td>
				</tr>
		  </table>
		</fieldset>
		</div>	
		  <uc1:AjaxPostInfo id="AjaxPostInfo1" runat="server"></uc1:AjaxPostInfo>
		 <cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint> 
		</form>		
		<%=footer%>
	</body>
</html>
