<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discuss_discussphoto_add" Debug="true" Codebehind="discussphoto_add.aspx.cs" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
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
<body><form id="form1" runat="server"> 
<div id="sc" runat="server"></div>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table" id="insert">

  <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">
        相片标题：</td>
    <td class="list_link" width="75%">
        <asp:TextBox ID="TextBox1" runat="server" Width="480px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussphoto_add_0001',this)">帮助</span></td>
  </tr>

  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        图片：</td>
<td class="list_link" style="width: 707px">  
    <table width="81%" border="0" cellspacing="1" cellpadding="5">
                <tr> 
                  <td style="width: 176px">
                    <div align="center"> 
                      <table width="10" border="0" cellspacing="1" cellpadding="2" class="table">
                        <tr> 
                          <td class="list_link" ><img src="../../sysImages/user/nopic_supply.gif" width="90" height="90" id="pic_p_1" /></td>
                        </tr>
                      </table>
                     </div>
                    </td>
                  <td width="34%"><div align="center"> 
                      <table width="10" border="0" cellspacing="1" cellpadding="2" class="table">
                        <tr> 
                          <td class="list_link" ><img src="../../sysImages/user/nopic_supply.gif" width="90" height="90" id="pic_p_2" /></td>
                        </tr>
                      </table>
                    </div></td>
                  <td width="33%"><div align="center"> 
                      <table width="10" border="0" cellspacing="1" cellpadding="2" class="table">
                        <tr> 
                          <td class="list_link" style="width: 95px" ><img src="../../sysImages/user/nopic_supply.gif" width="90" height="90" id="pic_p_3" /></td>
                        </tr>
                      </table>
                    </div></td>
                </tr>
               <tr>
               <td style="width: 176px" align="center">
               <asp:TextBox ID="pic_p_1url" runat="server" Width="0px" BackColor="#F8FDFF" BorderStyle="None"></asp:TextBox>
               </td>
               <td align="center">
                <asp:TextBox ID="pic_p_1ur2" runat="server" Width="0px" BackColor="#F8FDFF" BorderStyle="None"></asp:TextBox>
               </td>
               <td align="center">
               <asp:TextBox ID="pic_p_1ur3" runat="server" Width="0px" BackColor="#F8FDFF" BorderStyle="None"></asp:TextBox>
               </td>
               </tr> 
                <tr> 
                  <td class="list_link"><div align="center"><input  class="form" type="button" value="上 传"  onclick="selectFile('discuss_pic',
document.form1.pic_p_1url,400,550);" />&nbsp;&nbsp;<input id="Button2" type="button" value="删 除" class="form" onClick="dels_1();"/>
                    </div></td>
                  <td class="list_link" ><div align="center"><input  class="form" type="button" value="上 传"  onclick="selectFile('discuss_pic',document.form1.pic_p_1ur2,400,550);" />&nbsp;&nbsp;<input id="Button3" type="button" value="删 除" class="form" onClick="dels_2();"/>
                    </div></td>
                  <td class="list_link" ><div align="center"><input  class="form" type="button" value="上 传"  onclick="selectFile('discuss_pic',document.form1.pic_p_1ur3,400,550);" /> &nbsp;&nbsp;<input id="Button4" type="button" value="删 除"  class="form" onClick="dels_3();"/>
                    </div></td>
                </tr>
              </table>
    </td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        相册：</td>
    <td class="list_link">
        <asp:DropDownList ID="DropDownList1" runat="server" Width="133px">
        </asp:DropDownList>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussphoto_add_0002',this)">帮助</span></td>
  </tr>
     <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        图片说明：</td>
    <td class="list_link">
        <asp:TextBox ID="TextBox2" runat="server" Height="130px" TextMode="MultiLine" Width="480px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussphoto_add_0003',this)">帮助</span></td>
  </tr>
         <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        </td>
    <td class="list_link" style="width: 707px">
        <asp:Button ID="server" runat="server" Text="保存到相册" Width="115px" OnClick="server_Click" CssClass="form"/></td>
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
			    $('pic_p_1').src=$('pic_p_1url').value.toLowerCase().replace('{@userdirfile}','<% Response.Write(dimm+UserdirFile); %>');
			}
		} 
new Form.Element.Observer($('pic_p_1ur2'),1,pics_2);
	function pics_2()
		{
			if($('pic_p_1ur2').value=='')
			{
			$('pic_p_2').src='../Images/nopic_supply.gif'
			}
			else
			{
			$('pic_p_2').src=$('pic_p_1ur2').value.toLowerCase().replace('{@userdirfile}','<% Response.Write(dimm+UserdirFile); %>');
			}		
		}
new Form.Element.Observer($('pic_p_1ur3'),1,pics_3);
	function pics_3()
		{
			if($('pic_p_1ur3').value=='')
			{
			    $('pic_p_3').src='../Images/nopic_supply.gif'
			}
			else
			{
	            $('pic_p_3').src=$('pic_p_1ur3').value.toLowerCase().replace('{@userdirfile}','<% Response.Write(dimm+UserdirFile); %>');
			}
		} 
		
function dels_1()
	{
		document.form1.pic_p_1url.value='../../sysImages/user/nopic_supply.gif'
	}
function dels_2()
	{
		document.form1.pic_p_2url.value='../../sysImages/user/nopic_supply.gif'
	}
function dels_3()
	{
		document.form1.pic_p_3url.value='../../sysImages/user/nopic_supply.gif'
	}
</script>