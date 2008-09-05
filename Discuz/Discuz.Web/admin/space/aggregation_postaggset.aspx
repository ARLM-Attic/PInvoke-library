<%@ Page language="c#" Inherits="Discuz.Space.Admin.PostAggset"%>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxSpacepostInfo" Src="../UserControls/ajaxspacepostinfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>websitesetting</title>
		<link href="../styles/gridStyle.css" type="text/css" rel="stylesheet" />		
		<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
		<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
		<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
        <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
        <link href="../styles/draglist.css" type="text/css" rel="stylesheet" />		
		<script type="text/javascript" src="../js/common.js"></script>
		<script type="text/javascript" src="../js/AjaxHelper.js" type="text/javascript"></script>
        <script type="text/javascript" src="../js/modalpopup.js"></script>
        <script type="text/javascript" src="../js/draglist.js"></script>
        <script type="text/javascript">
		    function validate(theform)
		    {
		        var pidColl = $("dom0").getElementsByTagName("input");
                var pidlist = "";
                for(i = 0 ; i < pidColl.length ; i++)
                {
                    pidlist += pidColl[i].value + ",";
                }
                $("poststatus").value = pidlist;
                return true;
		    }
        </script>
</head>

	<body >
		<form id="Form1" runat="server">
		<div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">搜索日志</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
					        <td style="width: 100px">日志作者:</td>
					        <td>
				                <cc1:TextBox id="poster" runat="server" HintTitle="提示" HintInfo="多个作者名之间请用半角逗号 &amp;quot;,&amp;quot; 分割" Width="150px" RequiredFieldType="暂无校验"></cc1:TextBox>
				            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">日志发表时间范围:</td>
                            <td>
                                <cc1:Calendar id="postdatetimeStart" runat="server" HintTitle="提示" HintInfo="格式 yyyy-mm-dd, 不限制请留空" ReadOnly="False" ScriptPath="../js/calendar.js"></cc1:Calendar>&nbsp;-
                                <cc1:Calendar id="postdatetimeEnd" runat="server" HintTitle="提示" HintInfo="格式 yyyy-mm-dd, 不限制请留空" ReadOnly="False" ScriptPath="../js/calendar.js"></cc1:Calendar>
                            </td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" width="50%" align="right">
                    <table width="100%">
				        <tr>
				            <td style="width: 100px">日志关键字:</td>
					        <td>
				          <cc1:TextBox id="title" runat="server" HintTitle="提示" HintInfo="多关键字之间请用半角逗号 &amp;quot;,&amp;quot; 分割" Width="150px" RequiredFieldType="暂无校验"></cc1:TextBox></td>
				        </tr>
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
			<tr><td colspan="2" align="center"><cc1:Button id="SearchTopicAudit" runat="server" Text=" 搜索符合条件的日志 "></cc1:Button></td></tr>
        </table>
		  </fieldset>
		  <cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
		  </div>
		  <div id="postlistgrid"><uc1:AjaxSpacepostInfo id="AjaxSpaceInfo1" runat="server"></uc1:AjaxSpacepostInfo></div>		
		  <br />
		    <div class="content">
	            <div class="left" id="dom0">
	                <asp:Literal id="postlist" runat="server"></asp:Literal>
	            </div>
            </div>
         <br />
		  <table class="table1" cellspacing="0" cellpadding="4" width="100%">
            <tr><td align="center">
            <input id="poststatus" type="hidden" runat="server" />
            <cc1:Button id="SaveTopic" runat="server" Text=" 保存 "></cc1:Button>
            </td></tr> 
		  </table>		  
		</form>
		<%=footer%>
	</body>
</html>


