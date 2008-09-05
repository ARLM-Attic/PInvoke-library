<%@ Page Language="C#" AutoEventWireup="true" Inherits="configuration_system_Genlist" Codebehind="Genlist.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Ñ¡Ôñ³£¹æ__by Foosun.net & Foosun Inc.</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
</head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<body>
<form id="gListform" action="" runat="server" method="post">
<div style="padding-top:8px;padding-left:10px;padding-bottom:6px;">
<input type="hidden" id="returnV" name="returnV" />
<asp:Repeater ID="gList" runat="server" OnItemCommand="DataList1_ItemCommand">
   <HeaderTemplate>
    </HeaderTemplate>
      <ItemTemplate>
        <%#((DataRowView)Container.DataItem)["op"]%>
      </ItemTemplate>
      <FooterTemplate>
     </FooterTemplate>
</asp:Repeater>
</div>
<div style="padding-top:0px;padding-left:10px;padding-bottom:0px;">
<uc1:PageNavigator ID="PageNavigator1" runat="server" />
 </div>
</form>
</body>
</html>
<script language="javascript" type="text/javascript">

    function ReturnValue(obj)
    {
	    var Str=obj;
	    parent.ReturnTagsFun(Str);
    }
</script>