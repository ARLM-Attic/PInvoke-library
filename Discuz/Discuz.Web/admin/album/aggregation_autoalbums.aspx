<%@ Page language="c#" Inherits="Discuz.Album.Admin.AutoAlbums"%>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="cc3" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxPhotoInfo" Src="../UserControls/ajaxphotoinfo.ascx" %>
<%@ Register Src="../UserControls/ajaxalbumlist.ascx" TagName="AjaxAlbumList" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>websitesetting</title>
		<link href="../styles/gridstyle.css" type="text/css" rel="stylesheet">		
		<link href="../styles/calendar.css" type="text/css" rel="stylesheet">
		<link href="../styles/datagrid.css" type="text/css" rel="stylesheet">
		<link href="../styles/tab.css" type="text/css" rel="stylesheet">		
		<script type="text/javascript" src="../js/common.js"></script>
		<script type="text/javascript" src="../js/AjaxHelper.js" type="text/javascript"></script>		
	    <script type="text/javascript" src="../js/tabstrip.js"></script>
	    <script type="text/javascript" src="../js/calendar.js"></script>
	    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet">
	    <script type="text/javascript" src="../js/draglist.js"></script>
        <link href="../styles/draglist.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
		    function validate(theform)
		    {
		        return true;
		    }
        </script>
</head>

	<body>
		<form id="Form1" runat="server">
		<div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">焦点图片设置</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
		                    <td style="width: 140px">焦点图片显示方式:</td>
		                    <td>
		                        <cc1:RadioButtonList ID="focusphotoshowtype" runat="server" RepeatColumns="1">
                                    <asp:ListItem Value="0">浏览量</asp:ListItem>
					                <asp:ListItem Value="1">评论数</asp:ListItem>
					                <asp:ListItem Value="2">上传时间</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" width="50%" align="right">
                    <table width="100%">
		                <tr>
		                    <td style="width: 140px">焦点图片显示天数:</td>
		                    <td><cc1:textbox id="focusphotodays" runat="server" Size="3"  CanBeNull="必填" MinimumValue="0" MaxLength="3"></cc1:textbox></td>
		                </tr>
		                <tr>
		                    <td>焦点图片显示条数:</td>
		                    <td colspan="3"><cc1:textbox id="focusphotocount" runat="server" Size="3"  CanBeNull="必填" MinimumValue="0" MaxLength="3"></cc1:textbox></td>
		                </tr>
                    </table>
                </td>
            </tr>
        </table>
	  </fieldset>
	  <fieldset>
		<legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">焦点相册设置</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
		                    <td style="width: 140px">焦点相册显示方式:</td>
		                    <td>
		                        <cc1:RadioButtonList ID="recommendalbumtype" runat="server" RepeatColumns="3">
                                    <asp:ListItem Value="0">发布时间</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
		                    <td>推荐相册显示条数:</td>
		                    <td><cc1:textbox id="focusalbumcount" runat="server" Size="3"  CanBeNull="必填" MinimumValue="0" MaxLength="3"></cc1:textbox></td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" width="50%" align="right">
                    <table width="100%">
		                <tr>
		                    <td style="width: 140px">焦点相册显示天数:</td>
		                    <td><cc1:textbox id="focusalbumdays" runat="server" Size="3"  CanBeNull="必填" MinimumValue="0" MaxLength="3"></cc1:textbox></td>
		                </tr>
                    </table>
                </td>
            </tr>
        </table>
	   </fieldset>
	   <fieldset>
		 <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">一周热图设置</legend>
		 <table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
                            <td style="width:140px">一周热图总排行显示条数:</td>		 
		                    <td><cc1:textbox id="weekhot" runat="server" Size="3"  CanBeNull="必填" MinimumValue="0" MaxLength="3"></cc1:textbox></td>
		                </tr>
		            </table>
		        </td>
		    </tr>
		</table>
	  </fieldset>
	 </div>
		<div class="Navbutton">
		  <input id="recommendphoto" type="hidden" runat="server" />
		  <input id="recommendalbum" type="hidden" runat="server" />
		  &nbsp;<cc1:Button id="savetopic" runat="server" Text=" 保存 "></cc1:Button>
		</div>  
		</form>
		<%=footer%>
	</body>
</html>


