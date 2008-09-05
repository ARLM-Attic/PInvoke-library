<%@ Page language="c#" Inherits="Discuz.Album.Admin.CommendAlbums" %>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="cc3" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxPhotoInfo" Src="../UserControls/ajaxphotoinfo.ascx" %>
<%@ Register Src="../UserControls/ajaxalbumlist.ascx" TagName="AjaxAlbumList" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>websitesetting</title>	
		<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
		<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
		<link href="../styles/tab.css" type="text/css" rel="stylesheet" />		
		<script type="text/javascript" src="../js/common.js"></script>
		<script type="text/javascript" src="../js/AjaxHelper.js" type="text/javascript"></script>		
	    <script type="text/javascript" src="../js/tabstrip.js"></script>
	    <script type="text/javascript" src="../js/calendar.js"></script>
	    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
	    <script type="text/javascript" src="../js/draglist.js"></script>
        <link href="../styles/draglist.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript">
		    function validate(theform)
		    {
		        var aidColl = $("dom0").getElementsByTagName("input");
                var aidlist = "";
                for(i = 0 ; i < aidColl.length ; i++)
                {
                    aidlist += aidColl[i].value + ",";
                }
                $("recommendalbum").value = aidlist;
		        return true;
		    }
		    function managephoto(albumid)
	        {
	            window.location = "../album/album_managephoto.aspx?albumid=" + albumid;
	        }
        </script>
    </head>
	<body>
		<form id="Form1" runat="server">
		<div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">搜索相册</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
					        <td style="width: 100px">相册所有者:</td>
					        <td>
					            <input name="albumusername" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" style="width:150px;"/>
				            </td>
                        </tr>
                        <tr>
					        <td style="width: 100px">相册描述:</td>
					        <td> 
					            <input class="FormBase" name="albumdescription" onblur="this.className='FormBase';" onfocus="this.className='FormFocus';" style="width: 150px;" type="text" />
					        </td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" width="50%" align="right">
                    <table width="100%">
				        <tr>
					        <td class="td1" style="width: 100px">相册关键字:</td>
					        <td class="td1"><input class="FormBase" name="albumtitle" onblur="this.className='FormBase';" onfocus="this.className='FormFocus';" style="width: 150px;" type="text" /></td>
				        </tr>
				        <tr>
					        <td class="td2" style="width: 100px">创建时间:</td>
					        <td class="td2">
					            <input name="albumdatetimeStart" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" readonly="true"/>
					            <img src="../images/btn_calendar.gif" align="bottom" onclick="showcalendar(event, $('albumdatetimeStart'))" class="calendarimg" /><br />
				                <input name="albumdatetimeEnd" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" readonly="true"/>
					            <img src="../images/btn_calendar.gif" align="bottom" onclick="showcalendar(event, $('albumdatetimeEnd'))" class="calendarimg" />
				            </td>
				        </tr>
                    </table>
                </td>
            </tr>
        </table>
		  <div class="Navbutton"><cc1:Button id="searchalbum" runat="server" Text=" 搜索符合条件的相册 "></cc1:Button></div>
		  </fieldset>
		  </div>
		  <div id="albumslist"><uc2:AjaxAlbumList id="ajaxalbumlist1" runat="server"></uc2:AjaxAlbumList></div>		
		  <div class="content">
	            <div class="left" id="dom0">
	                <asp:Literal id="albumlist" runat="server"></asp:Literal>
	            </div>
           </div>
		<div class="Navbutton">
		  <input id="recommendalbum" type="hidden" runat="server" />
		  &nbsp;<cc1:Button id="savetopic" runat="server" Text=" 保存 "></cc1:Button>
		</div>  
		</form>
		<%=footer%>
	</body>
</html>


