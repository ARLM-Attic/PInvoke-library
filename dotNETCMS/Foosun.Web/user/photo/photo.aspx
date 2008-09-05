<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_photo_photo" Debug="true" Codebehind="photo.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../configuration/js/jquery-compressed.js"></script>
<script type="text/javascript" src="../../configuration/js/thickbox_zh.js"></script>
<!--可以自定义样式-->
<link rel="stylesheet" href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/thickbox.css" type="text/css" media="screen" />
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >相册管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Photoalbumlist.aspx" class="list_link">相册管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加图片</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="Photoalbumlist.aspx" class="menulist">相册首页</a>&nbsp;┊&nbsp;<a href="photo_add.aspx?PhotoalbumID=<%Response.Write(Request.QueryString["PhotoalbumID"]); %>" class="menulist">添加图片</a>&nbsp;┊&nbsp;<span id="sc" runat="server"></span>&nbsp;┊&nbsp;<a href="photoclass.aspx" class="menulist">相册分类</a>&nbsp;┊&nbsp;<a href="Photoalbum.aspx" class="menulist">添加相册</a>
         <%--&nbsp; <a href="javascript:PDel();" class="topnavichar">批量删除</a>--%></span></td>
  </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Width="100%" Visible="False">
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table" id="Table1">
  <tr class="TR_BG_list">
    <td class="list_link" width="25%" style="text-align: right">请输入密码：</td>
    <td class="list_link" style="width: 707px"><asp:TextBox ID="pwd" runat="server" Width="218px" TextMode="Password"></asp:TextBox></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right"></td>
    <td class="list_link" style="width: 707px"><asp:Button ID="open" runat="server" Text="进入相册" Width="66px" OnClick="open_Click" CssClass="form"/></td>
  </tr>
</table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="100%" Visible="False">
<div id="no" runat="server"></div>
   <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" id="Table2">
    <tr>
        <td>
            <asp:DataList ID="DataList1" runat="server" RepeatColumns="2" align="center" CellPadding="8" CellSpacing="6" CssClass="TR_BG_list" Width="100%" BorderStyle="Dotted" BorderWidth="1px">
            <ItemTemplate>
            <table align="center"  border="0" cellpadding="5" cellspacing="1"  style="width:100%">
            <tr class="TR_BG_list">
            <td align="center" rowspan="5"><a class="thickbox" href="<%#((DataRowView)Container.DataItem)["PhotoUrls"]%>" rel="gallery-plants"><Img ID="PicImage" style="height:80px;width:100px;" border="0" src="<%#((DataRowView)Container.DataItem)["PhotoUrls"]%>" /></a></td>
            <td class="list_link">相片名称：</td>
            <td class="list_link"><strong><%#((DataRowView)Container.DataItem)["PhotoName"]%></strong></td></tr>           
            <tr class="TR_BG_list" style="height:23px;">
            <td class="list_link">创建日期：</td>
            <td class="list_link"><%#((DataRowView)Container.DataItem)["PhotoTime"]%></td></tr>
            <tr class="TR_BG_list" style="height:23px;">
            <td class="list_link">相片描述：</td>
            <td class="list_link"><%#((DataRowView)Container.DataItem)["PhotoContent"]%></td></tr>
            <tr class="TR_BG_list" style="height:23px;">
            <td class="list_link">相册名称：</td>
            <td class="list_link"><%#((DataRowView)Container.DataItem)["PhotoalbumName"]%></td></tr>
            <tr class="TR_BG_list" style="height:23px;">
            <td class="list_link">操作：</td>
            <td class="list_link"><a href="photo_up.aspx?PhotoID=<%#((DataRowView)Container.DataItem)["PhotoID"]%>" class="list_link">修改</a>┆<a href="photo.aspx?PhotoID=<%#((DataRowView)Container.DataItem)["PhotoID"]%>&typeS=del" class="list_link" onclick="{if(confirm('确认删除吗？')){return true;}return false;}">删除</a> <input id="Checkbox1" type="checkbox" /></td>
                </tr>
            </table>
             </ItemTemplate>
            </asp:DataList>
        </td>
    </tr> 
    </table>
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">    
      <tr>
        <td align="right">
            <uc2:PageNavigator ID="PageNavigator2" runat="server" />
        </td>
    </tr>
    </table>
</asp:Panel>
<br />
<br />
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
