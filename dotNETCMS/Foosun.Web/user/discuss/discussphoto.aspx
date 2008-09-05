<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discuss_discussphoto" Debug="true" Codebehind="discussphoto.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../configuration/js/jquery-compressed.js"></script>
<script type="text/javascript" src="../../configuration/js/thickbox_zh.js"></script>
<!--可以自定义样式-->
<link rel="stylesheet" href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/thickbox.css" type="text/css" media="screen" />
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<div id="sc" runat="server"></div>
<asp:Panel ID="Panel1" runat="server" Width="100%" Visible="False">
        <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#ffffff" class="table" id="Table1">

  <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">
        请输入密码：</td>
    <td class="list_link" style="width: 707px">
        <asp:TextBox ID="pwd" runat="server" Width="218px" TextMode="Password"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussphoto_0001',this)">帮助</span></td>
  </tr>
       <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right">
        </td>
    <td class="list_link" style="width: 707px">
        <asp:Button ID="open" runat="server" Text="进入相册" Width="66px" OnClick="open_Click"/></td>
  </tr>
</table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="100%" Visible="False">
<div id="no" runat="server"></div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table2">
      <tr>
    <td>
            <asp:DataList ID="DataList1" runat="server" RepeatColumns="2"  align="center">
                <ItemTemplate>
                  <table align="center"  border="0" cellpadding="3" cellspacing="1" class="table" width="450">
            <tr class="TR_BG_list">
            <td align="center" rowspan="6"><a class="thickbox" href="<%#((DataRowView)Container.DataItem)["PhotoUrls"]%>" rel="gallery-plants"><Img ID="PicImage" Height="120px" border="0" Width="100px" src="<%# ((DataRowView)Container.DataItem)["PhotoUrls"].ToString()%>" /></a></td>
            <td class="list_link">相片名称：</td>
            <td class="list_link"><%#((DataRowView)Container.DataItem)["PhotoName"]%></td></tr>           
            <tr class="TR_BG_list">
            <td class="list_link">创建日期：</td>
            <td class="list_link"><%#((DataRowView)Container.DataItem)["PhotoTime"]%></td></tr>
            <tr class="TR_BG_list">
            <td class="list_link">相片拥有人：</td>
            <td class="list_link"><%#((DataRowView)Container.DataItem)["UserNamess"]%></td></tr>
            <tr class="TR_BG_list">
            <td class="list_link">相片描述：</td>
            <td class="list_link"><%#((DataRowView)Container.DataItem)["PhotoContent"]%></td></tr>
            <tr class="TR_BG_list">
            <td class="list_link">相册名称：</td>
            <td class="list_link"><%#((DataRowView)Container.DataItem)["PhotoalbumName"]%></td></tr>
            <tr class="TR_BG_list">
            <td class="list_link">操作：</td>
            <td class="list_link"><a href="discussphoto_up.aspx?PhotoID=<%#((DataRowView)Container.DataItem)["PhotoID"]%>&DisID=<%=DisIDq %>" class="list_link">修改</a>┆<a href="discussphoto_del.aspx?PhotoID=<%#((DataRowView)Container.DataItem)["PhotoID"]%>&DisID=<%=DisIDq %>" class="list_link" onclick="{if(confirm('确认删除吗？')){return true;}return false;}">删除</a> <input id="Checkbox1" type="checkbox" /></td>
                </tr>
            </table>
                </ItemTemplate>
            </asp:DataList>
            </td></tr>     
      <tr>
        <td align="right">
            <uc2:PageNavigator ID="PageNavigator2" runat="server" />
        </td>
    </tr>
    </table>
</asp:Panel>
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
<script language="javascript" type="text/javascript">
function PDel()
{
    if(confirm("你确定要彻底删除吗?"))
    {
	    document.form1.action="?Type=PDel";
	    document.form1.submit();
	}
}
</script>
</html>
