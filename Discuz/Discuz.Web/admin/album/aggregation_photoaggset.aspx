﻿<%@ Page language="c#" Inherits="Discuz.Album.Admin.PhotoAggset"%>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="cc3" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxPhotoInfo" Src="../UserControls/ajaxphotoinfo.ascx" %>
<%@ Register Src="../UserControls/ajaxalbumlist.ascx" TagName="AjaxAlbumList" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>websitesetting</title>
		<link href="../styles/gridstyle.css" type="text/css" rel="stylesheet" />		
		<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
		<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
		<link href="../styles/tab.css" type="text/css" rel="stylesheet" />
	    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
        <link href="../styles/draglist.css" type="text/css" rel="stylesheet" />		
		<script type="text/javascript" src="../js/common.js"></script>
		<script type="text/javascript" src="../js/AjaxHelper.js" type="text/javascript"></script>		
	    <script type="text/javascript" src="../js/tabstrip.js"></script>
	    <script type="text/javascript" src="../js/calendar.js"></script>
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
                $("recommendphoto").value = pidlist;
		        return true;
		    }
        </script>
</head>

	<body>
		<form id="Form1" runat="server">
		<div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">搜索照片</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
					        <td style="width: 100px">照片所有者:</td>
					        <td>
				                <input name="photousername" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" style="width:150px;"/>
				            </td>
                        </tr>
                        <tr>
					        <td style="width: 100px">照片发表时间范围:</td>
					        <td>
					            <input name="photodatetimeStart" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" readonly="true"/>
					            <img src="../images/btn_calendar.gif" align="bottom" onclick="showcalendar(event, $('photodatetimeStart'))" class="calendarimg" /><br />
				                <input name="photodatetimeEnd" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" readonly="true"/>
					            <img src="../images/btn_calendar.gif" align="bottom" onclick="showcalendar(event, $('photodatetimeEnd'))" class="calendarimg" />
				            </td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" width="50%" align="right">
                    <table width="100%">
                        <tr>
					        <td style="width: 100px">照片关键字:</td>
					        <td>
				                <input name="phototitle" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" style="width:150px;"/>
				            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
		<div class="Navbutton"><cc1:Button id="SearchPhoto" runat="server" Text=" 搜索符合条件的照片 "></cc1:Button></div>
		</fieldset>
		</div>
		  <div id="photolistgrid"><uc1:AjaxPhotoInfo id="AjaxPhotoInfo1" runat="server"></uc1:AjaxPhotoInfo></div>
		  <div class="content">
	            <div class="left" id="dom0">
	                <asp:Literal id="photolist" runat="server"></asp:Literal>
	            </div>
           </div><br />
		  <table class="table1" cellspacing="0" cellpadding="4" width="100%">
		     <tr>
		     <td align="center">
		      <input id="recommendphoto" type="hidden" runat="server" />
		     <cc1:Button id="SaveTopic" runat="server" Text=" 保存 "></cc1:Button>
		     </td></tr> 
		  </table>
		</form>
		<%=footer%>
	</body>
</html>


