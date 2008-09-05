<%@ Page Language="C#" ContentType="text/html" AutoEventWireup="true" Inherits="user_discuss_discussphoto_up" Debug="true" Codebehind="discussphoto_up.aspx.cs" %>

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
<div id="sc" runat="server"></div>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table" id="insert">

  <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">
        相片标题：</td>
    <td class="list_link" style="width: 707px">
        <asp:TextBox ID="PhotoName" runat="server" Width="258px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_discussphoto_add_0001',this)">帮助</span></td>
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
                  <td class="list_link" style="width: 184px"><div align="center"><input  class="form" type="button" value="上 传"  onclick="selectFile('discuss_pic',
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
        </asp:DropDownList>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussphoto_add_0002',this)">帮助</span></td>
  </tr>
     <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        图片说明：</td>
    <td class="list_link" style="width: 707px">
        <asp:TextBox ID="PhotoContent" runat="server" Height="84px" TextMode="MultiLine" Width="353px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussphoto_add_0003',this)">帮助</span></td>
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
			    $('pic_p_1').src="<% Response.Write(Foosun.Publish.CommonData.getUrl()); %>"+$('pic_p_1url').value.replace('{@UserdirFile}','<% Response.Write(Foosun.Config.UIConfig.UserdirFile); %>')
			}
		} 
function dels_1()
	{
		document.form1.pic_p_1url.value='../../sysImages/user/nopic_supply.gif'
	}
</script>