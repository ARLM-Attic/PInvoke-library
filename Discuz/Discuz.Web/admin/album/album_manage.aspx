<%@ Page Language="C#" AutoEventWireup="true" Inherits="Discuz.Album.Admin.Manage" %>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxAlbumList" Src="../UserControls/ajaxalbumlist.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
		<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
		<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />	
	    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
	    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />	
		<script type="text/javascript" src="../js/common.js"></script>
		<script type="text/javascript" src="../js/AjaxHelper.js"></script>
	    <script type="text/javascript" src="../js/calendar.js"></script>
        <script type="text/javascript" src="../js/modalpopup.js"></script>
	    <script type="text/javascript">
	        function managephoto(albumid)
	        {
	            window.location = "album_managephoto.aspx?albumid=" + albumid;
	        }
	        function insertElement()
	        {
	        }
	    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="ManagerForm">
	<fieldset>
	<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">搜索相册</legend>
	<table cellspacing="0" cellpadding="4" width="100%" align="center">
        <tr>
            <td  class="panelbox" width="50%" align="left">
                <table width="100%">
                    <tr>
					    <td style="width: 60px">用户名:</td>
					    <td>
					        <input name="albumusername" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" style="width:200px;"/>
					    </td>
                    </tr>
                    <tr>
					    <td>相册描述:</td>
					    <td> 
					        <input class="FormBase" name="albumdescription" onblur="this.className='FormBase';" onfocus="this.className='FormFocus';" style="width: 200px;" type="text" />
					    </td>
                    </tr>
                </table>
            </td>
            <td  class="panelbox" width="50%" align="right">
                <table width="100%">
				    <tr>
					    <td style="width: 60px">相册标题:</td>
					    <td>
                            <input class="FormBase" name="albumtitle" onblur="this.className='FormBase';" onfocus="this.className='FormFocus';" style="width: 200px;" type="text" />
			            </td>
				    </tr>
				    <tr>
					    <td>创建时间:</td>
					    <td>
				            起始日期:<input name="albumdatetimeStart" id="albumdatetimeStart" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" readonly="true"/>
					        <img src="../images/btn_calendar.gif" align="bottom" onclick="showcalendar(event, $('albumdatetimeStart'))" class="calendarimg" /><br />
				            结束日期:<input name="albumdatetimeEnd" id="albumdatetimeEnd" type="text" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" readonly="true"/>
					        <img src="../images/btn_calendar.gif" align="bottom" onclick="showcalendar(event, $('albumdatetimeEnd'))" class="calendarimg" />
				      </td>
				    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2"><cc1:Button id="SearchAlbum" runat="server" Text=" 搜索符合条件的相册 " ></cc1:Button></td>
        </tr>
    </table>
    </fieldset>
    <cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
    </div>
    <div id="albumslist"><uc1:AjaxAlbumList id="AjaxAlbumList1" runat="server"></uc1:AjaxAlbumList></div>		
    <p style="text-align:right;"><cc1:Button id="DeleteApply" runat="server" Text=" 删 除 "  ButtonImgUrl="../images/del.gif" OnClick="DeleteApply_Click"></cc1:Button></p>
    </form>
    <%=footer%>
</body>
</html>
