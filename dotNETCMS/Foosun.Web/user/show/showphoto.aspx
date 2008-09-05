<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_show_showphoto" Codebehind="showphoto.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>__相册</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/divcss.css" rel="stylesheet" type="text/css" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/thickbox.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../configuration/js/jquery-compressed.js"></script>
<script type="text/javascript" src="../../configuration/js/thickbox_zh.js"></script>
<!--可以自定义样式-->
<link rel="stylesheet" href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/thickbox.css" type="text/css" media="screen" />
</head>
<body onload="TB_init();">
    <form id="form1" runat="server">
        <div style="padding-left:14px;"><span id="sc" runat="server" /> 
         </div>
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
        <span id="no" runat="server"></span>
           <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" id="Table2">
            <tr>
                <td>
                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="3"  align="center" CellPadding="8" CellSpacing="6" CssClass="TR_BG_list" Width="100%" BorderColor="Gainsboro" BorderStyle="Dotted" BorderWidth="1px">
                    <ItemTemplate>
                    <table align="center"  border="0" cellpadding="5" cellspacing="0" style="width:100%">
                    <tr class="TR_BG_list">
                    <td align="center" rowspan="2" style="padding-left:14px;"><a class="thickbox" href="<%#((DataRowView)Container.DataItem)["PhotoUrls"]%>" rel="gallery-plants"><img id="PicImage" style="height:80px;width:100px;" border="0" src="<%#((DataRowView)Container.DataItem)["PhotoUrls"]%>" /></a></td>
                    <td class="list_link" style="padding-left:14px;"><strong><%#((DataRowView)Container.DataItem)["PhotoName"]%></strong></td>
                    </tr>           
                    <tr class="TR_BG_list" style="padding-left:14px;">
                    <td class="list_link" style="padding-left:14px;"><%#((DataRowView)Container.DataItem)["PhotoTime"]%></td>
                    </tr>

                    <tr class="TR_BG_list" style="padding-left:14px;">
                    <td class="list_link" style="padding-left:14px;"><%#((DataRowView)Container.DataItem)["PhotoContent"]%></td>
                    </tr>
           
                    </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr> 
            </table>
        <br />
            <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">    
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
</html>
