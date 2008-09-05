<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Templet_LabelList" Codebehind="LabelList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .midtxtad{margin:5px;padding:8px 5px;float:left;width:96%;border:1px #AFCAE4 dotted;background-color:#FCFDFE;}
    .midtxtad a{color:#03C;}
    .midtxtad a:hover{color:#F00;}
    .midtxtad ul{float:left;margin:0 5px;display:inline;}
    .midtxtad li{float:left;width:170px;height:22px;}
</style>
</head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<body>
<form id="Label" action="" runat="server">
     <div style="padding:5px 5px;"><span id="channelList" runat="server" /></div>
    <div runat="server" id="LabelList" class="midtxtad"></div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
function selectLabel(rvalue)
{
    parent.ReturnLabelValueText(rvalue);
}
function getchanelInfo(obj)
{
   var SiteID=obj.value;
   window.location.href="labelList.aspx?SiteID="+SiteID+"";
}
</script>