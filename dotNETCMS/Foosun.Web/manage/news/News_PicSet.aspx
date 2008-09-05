<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_News_PicSet" Codebehind="News_PicSet.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table" id="Frame">
      <tr class="TR_BG_list">
          <td align="left" class="list_link" style="width: 346px">
              ԭͼƬ(ͼ)</td>
          <td align="left" class="list_link" rowspan="4" valign="top">
                     ѡͼ
          <asp:TextBox ID="SPicURL" runat="server" Width="135px" MaxLength="200" CssClass="form"></asp:TextBox>
          <img src="../../sysImages/folder/s.gif" alt="ѡͼƬ" border="0" style="cursor:pointer;" onclick="selectFile('pic',document.form1.SPicURL,250,500);document.form1.SPicURL.focus();" /><br />
<br />
          <p></p>               ѡСͼ
          <asp:TextBox ID="PicURL" runat="server" Width="135px" MaxLength="200" CssClass="form"></asp:TextBox>
          <img src="../../sysImages/folder/s.gif" alt="ѡͼƬ" border="0" style="cursor:pointer;" onclick="selectFile('pic',document.form1.PicURL,250,500);document.form1.PicURL.focus();" />

          <p></p>
              <asp:Button ID="Button1" runat="server" Text="޸ͼƬ" CssClass="form" OnClick="Button1_Click"/></td>
      </tr>
      <tr class="TR_BG_list">
          <td align="center" class="list_link" style="width: 346px">
          <span id="Picd" runat="server" />
      </tr>
      <tr class="TR_BG_list">
          <td align="left" class="list_link" style="width: 346px">
              ԭͼƬ(Сͼ)</td>
      </tr>
      <tr class="TR_BG_list">
          <td align="center" class="list_link" style="width: 346px">
          <span id="Picx" runat="server" /></td>
      </tr>
    </table>    
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">

new Form.Element.Observer($('PicURL'),1,pics_1);
	function pics_1()
		{
			$('pic_p_1').src=$('PicURL').value.replace('{@dirfile}','<% Response.Write(Udirs); %>');
		} 
new Form.Element.Observer($('SPicURL'),1,pics_2);
	function pics_2()
		{
			$('pic_p_2').src=$('SPicURL').value.replace('{@dirfile}','<% Response.Write(Udirs); %>');
		} 
</script>
