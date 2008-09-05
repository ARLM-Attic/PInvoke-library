<%@ Page Language="C#" ContentType="text/html" AutoEventWireup="true" Inherits="user_discuss_discussTopi_commentary" Codebehind="discussTopi_commentary.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<style  rel="stylesheet" type="text/css">
.t_signature {
	height: expression(signature(this));
}
</style>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
</head>
<body>
<form id="form1" name="form1" method="post" action="" runat="server">
<span id="sc" runat="server"></span>

<table width="98%" align="center" border="0"  cellpadding="0" cellspacing="0">
    <asp:Panel ID="Panel1" runat="server" Width="98%" Visible="False" align="center">
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#ffffff" class="table">
        <span id="VoteTF" runat="server" ></span>
            <tr class="TR_BG_list">    
            <td class="list_link" width="20%"></td>
                <td class="list_link" width="80%" align="left">
                    <asp:Button ID="vot" runat="server" Text="提 交" CssClass="form" OnClick="vot_Click"/>
                    <asp:Button ID="view" runat="server" OnClick="view_Click" Text="查看结果" CssClass="form"/>
                </td>
            </tr>
        </table>
    </asp:Panel>
</table>

<div id="no" runat="server"></div>
<span id="cmment1" runat="server" style="padding-left:14px;"></span>
<table width="98%" align="center">
 <tr>
     <td colspan="2">
      <asp:Repeater ID="DataList1" runat="server" OnItemCommand="DataList1_ItemCommand" >
        <ItemTemplate>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
                <tr class="TR_BG_list">
                <td style="height:20px;width:16%;">
                    <div style="height:22px;"><%#((DataRowView)Container.DataItem)["UserName"]%></div>
                    <div style="height:20px;padding-bottom:3px;"><%#((DataRowView)Container.DataItem)["infos"]%></div>
                    <div style="padding-bottom:3px;"><%#((DataRowView)Container.DataItem)["userfaces"]%></div>
                    <div style="padding-top:5px;height:20px;position:relative;width:50%;border-top-width:1px;border-right-width: 1px;border-bottom-width: 1px;border-left-width: 1px;border-top-style: dashed;	border-right-style: none;border-bottom-style: none;border-left-style: none;border-top-color: #CCCCCC;">积分：<span style="font-size:10px;"><%#((DataRowView)Container.DataItem)["iPoint"]%></span></div>
                    <div style="height:18px;">金币：<span style="font-size:10px;"><%#((DataRowView)Container.DataItem)["gPoint"]%></span></div>
                    <div style="height:18px;">魅力值：<span style="font-size:10px;"><%#((DataRowView)Container.DataItem)["cPoint"]%></span></div>
                    <div style="height:18px;">人气值：<span style="font-size:10px;"><%#((DataRowView)Container.DataItem)["ePoint"]%></span></div>
                    <div style="height:18px;">活跃值：<span style="font-size:10px;"><%#((DataRowView)Container.DataItem)["aPoint"]%></span></div>
                    <div style="height:18px;">注册：<span style="font-size:10px;"><%#((DataRowView)Container.DataItem)["RegTime"]%></span></div>
                    </td>
                    <td class="list_link" valign="top" style="height:20px;width:84%;padding-top:10px;padding-left:8px;">
                                <%#((DataRowView)Container.DataItem)["Content"]%>
     
                            <!--显示签名-->
               
                        <div style="padding-left:10px;padding-top:5px;">
                            <div class="writer-Name">&nbsp;</div>
                                <div style="overflow: hidden; max-height: 4em;maxHeightIE:48px;position:relative;width:50%;border-top-width:1px;border-right-width: 1px;border-bottom-width: 1px;border-left-width: 1px;border-top-style: dashed;	border-right-style: none;border-bottom-style: none;border-left-style: none;border-top-color: #CCCCCC;">
                                <%#((DataRowView)Container.DataItem)["chars"]%>
                                </div>
                        </div>
                         <!--显示签名-->
             
                   </td>
                 </tr>
            </table>
            <a name="btom">
        </ItemTemplate>
        </asp:Repeater>
     </td>
     </tr>
<tr class="TR_BG_list">
<span id="cmment" runat="server"></span>
<a id="bottom" />
<td align="right" style="width: 928px" class="list_link"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></td></tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table" id="commentary" style="display:">
<tr class="TR_BG_list" style="display:none;">
<td class="list_link" width="20%" style="text-align: right">
    主题：</td>
<td class="list_link" width="80%">
    <asp:TextBox ID="titlesd" runat="server" Width="392px" CssClass="form"></asp:TextBox>&nbsp;
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_discussTopi_commentary_0001',this)">帮助</span></td>
</tr>
<tr class="TR_BG_list">
<td class="list_link" style="text-align: right">
    内容：</td>
<td class="list_link">

 <script type="text/javascript" language="JavaScript">
             window.onload = function()
	        {
	        var sBasePath = "../../editor/"
            var oFCKeditor = new FCKeditor('contentBox') ;
            oFCKeditor.BasePath	= sBasePath ;
            oFCKeditor.ToolbarSet = 'Foosun_User';
            oFCKeditor.Width = '100%' ;
            oFCKeditor.Height = '350' ;	
            oFCKeditor.ReplaceTextarea() ;
            }
        </script>
		<textarea rows="1" cols="1" name="contentBox" style="display:none" id="contentBox" runat="server" ></textarea>    
    </td>
</tr>
<tr class="TR_BG_list">
    
<td class="list_link"></td>
<td class="list_link">
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Button ID="subset" runat="server" Text="回复" CssClass="form" OnClick="subset_Click"/>
    &nbsp; &nbsp;&nbsp; &nbsp;<input type="reset" name="Submit3"  class="form" value="重 置"/>
</td>
</tr>
</table>
<br />
<br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table>

</form>
</body>
</html>
<script language="javascript" type="text/javascript">
function DispChanges()
{
	if(document.getElementById("commentary").style.display=="none")
	{
		document.getElementById("commentary").style.display="";
	}
	else
	{
		document.getElementById("commentary").style.display="none";
	}
}
</script>


