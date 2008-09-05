<%@ Page Language="C#" ContentType="text/html" AutoEventWireup="true" Inherits="user_photo_up" Debug="true" Codebehind="photo_up.aspx.cs" %>

<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >相册管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Photoalbumlist.aspx" target="sys_main" class="list_link">相册管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加图片</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="Photoalbumlist.aspx" class="menulist">相册首页</a>　<a href="photo_add.aspx" class="menulist">添加图片</a> &nbsp;&nbsp;<a href="photoclass.aspx" class="menulist">相册分类</a> &nbsp;&nbsp; <a href="Photoalbum.aspx" class="menulist">添加相册</a></span></td>
  </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table" id="insert">

  <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">
        相片标题：</td>
    <td class="list_link" style="width: 707px">
        <asp:TextBox ID="PhotoName" runat="server" Width="258px" CssClass="form"></asp:TextBox></td>
  </tr>

  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        图片：</td>
    <td class="list_link" style="width: 707px">  
    <table border="0" cellspacing="1" cellpadding="5">
                <tr> 
                  <td style="width: 184px">
                    <div align="center"> 
                      <table width="10" border="0" cellspacing="1" cellpadding="2" class="table">
                        <tr> 
                          <span id="no" runat="server"></span>
                        </tr>
                      </table>
                     </div>
                    </td>
                </tr>
               <tr>
               <td style="width: 184px" align="center">
                   <asp:HiddenField ID="pic_p_1url" runat="server" />
               </td>
               </tr> 
                <tr> 
                  <td class="list_link" style="width: 184px"><div align="center"><input  class="form" type="button" value="上 传"  onclick="selectFile('user_pic',
document.form1.pic_p_1url,300,400);" />&nbsp;&nbsp;<input id="Button2" type="button" value="删 除" class="form" onClick="dels_1();"/>
                    </div></td>
                </tr>
              </table>
    </td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        相册：</td>
    <td class="list_link" style="width: 707px">
        <asp:DropDownList ID="Photoalbum" runat="server" Width="133px">
        </asp:DropDownList></td>
  </tr>
     <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        图片说明：</td>
    <td class="list_link" style="width: 707px">
        <asp:TextBox ID="PhotoContent" runat="server" Height="84px" TextMode="MultiLine" Width="353px" CssClass="form"></asp:TextBox></td>
  </tr>
       <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        </td>
    <td class="list_link" style="width: 707px">
        <asp:Button ID="server" runat="server" Text="保  存" Width="115px" OnClick="server_Click" CssClass="form"/></td>
        </tr>
           
</table>
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
</html>
<script type="text/javascript" language="javascript">
new Form.Element.Observer($('pic_p_1url'),1,pics_1);
	function pics_1()
		{
			if ($('pic_p_1url').value=='')
			{
				$('pic_p_1').src='../Images/nopic_supply.gif'
			}
			else
			{
			$('pic_p_1').src=$('pic_p_1url').value.replace('{@UserdirFile}','<% Response.Write(dirPic); %>');
			}
		} 
function dels_1()
	{
		document.form1.pic_p_1url.value='../../sysImages/user/nopic_supply.gif'
	}
</script>