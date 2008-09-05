<%@ Page language="c#" Inherits="Discuz.Space.Admin.SpaceAggset"%>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxSpaceInfo" Src="../UserControls/ajaxspaceinfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>websitesetting</title>		
		<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
		<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />	
		<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
        <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />	
        <link href="../styles/draglist.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" src="../js/common.js"></script>
        <script type="text/javascript" src="../js/modalpopup.js"></script>
		<script type="text/javascript" src="../js/AjaxHelper.js"></script>
        <script type="text/javascript" src="../js/draglist.js"></script>
        <script type="text/javascript">
            function validate(theform)
            {
                var sidColl = $("dom0").getElementsByTagName("input");
                var sidlist = "";
                for(i = 0 ; i < sidColl.length ; i++)
                {
                    if(sidlist=="")
                    {
                       sidlist = sidColl[i].value;
                    }
                    else
                    {
                       sidlist = sidlist + "," + sidColl[i].value;
                    }
                }
                           
                $("spacestatus").value = sidlist;
                return true;
            }
        </script>
</head>

	<body >
		<form id="Form1" runat="server">
		<div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">搜索个人空间</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
					        <td style="width: 100px">空间所有者:</td>
					        <td>
				                <cc1:TextBox id="poster" runat="server" HintTitle="提示" HintInfo="多个用户名之间请用半角逗号 &amp;quot;,&amp;quot; 分割" Width="150px" RequiredFieldType="暂无校验"></cc1:TextBox>
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
				                <cc1:TextBox id="title" runat="server" HintTitle="提示" HintInfo="多关键字之间请用半角逗号 &amp;quot;,&amp;quot; 分割" Width="150px" RequiredFieldType="暂无校验"></cc1:TextBox>
				            </td>
                        </tr>
                    </table>
                </td>
            </tr>
			<tr><td colspan="2" align="center"><cc1:Button id="SearchTopicAudit" runat="server" Text=" 搜索符合条件的个人空间 "></cc1:Button></td></tr>
        </table>
		  </fieldset>
		  <cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
		  </div>
		  <div id="spacelistgrid"><uc1:AjaxSpaceInfo id="AjaxSpaceInfo1" runat="server"></uc1:AjaxSpaceInfo></div>		
		  <br />
		  <div class="content">
	            <div class="left" id="dom0">
	                <asp:Literal id="spacelist" runat="server"></asp:Literal>
	            </div>
            </div><br />
		  <table class="table1" cellspacing="0" cellpadding="4" width="100%">
		     <tr>
		     <td align="center">
		     <input id="spacestatus" type="hidden" runat="server" />
		     <cc1:Button id="SaveTopic" runat="server" Text=" 保存 " ValidateForm="false"></cc1:Button>
		     </td></tr> 
		  </table>		  
		</form>
		
		<%=footer%>
	</body>
</html>


