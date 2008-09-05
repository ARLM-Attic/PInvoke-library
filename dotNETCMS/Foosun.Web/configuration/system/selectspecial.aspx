<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectspecial.aspx.cs" Inherits="Foosun.Web.configuration.system.selectspecial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>选择专题__By Foosun.net & Foosun Inc.</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <select id="Special" name="Special" style="width:250px;height:150px"  runat="server" class="form" multiple></select>
    </div>
    <div align="center"><input type="button" value=" 选定" class="form" onclick="javascript:ReturnValue()" /> <span style="font-size:12px;">(可以多选)</span></div>
    </form>
</body>
</html>
<script language ="javascript" type="text/javascript">
function ReturnValue()
{
    var obj = document.form1.Special;
	var sid = "";
	var snm = "";
    for (var i=0;i<obj.length;i++)
    {
        var value = obj.options[i].value;
        if(obj.options[i].selected==true)
        {
            var text = value.split('|');
            sid += text[0]+",";
            snm += text[1]+",";
        }
    }
    sid = sid.substring(0,sid.length-1);
    snm = snm.substring(0,snm.length-1);
	var arryret = new Array(sid,snm);
	parent.ReturnFun(arryret);
}
</script>