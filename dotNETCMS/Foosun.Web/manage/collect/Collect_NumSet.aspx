<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_collect_Collect_NumSet" Codebehind="Collect_NumSet.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>确认采集新闻</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
    <script language="JavaScript" type="text/javascript">
    <!--
    function OnOK()
    {
	    var _n = document.getElementById('TxtNum').value;
	    var reg = /^[0-9]+$/;
	    if(!reg.test(_n))
	    {
	        alert("采集数量请输入正整数");
	        document.getElementById("TxtNum").focus();
	        return;
	    }
	    var n = parseInt(_n);
	    if(n < 1)
	    {
	        alert("采集数量必须为正整数");
	        document.getElementById('TxtNum').focus();
	        return;
	    }
	    var norpt = 0;
	    if(document.getElementById('ChkNoRepeat').checked)
	        norpt = 1;
	    window.opener.location.href = 'Collect.aspx?num='+ n +'&norepeat='+ norpt +'&id=<%# nid%>';
	    self.close();
    }
    function OnCancel()
    {
	    window.returnValue = 0;
	    self.close();
    }
    
    function KeyDown()
    {
        if(event.keyCode == 13)
        {
            OnOK();
        }
    }
    //-->
</script>
</head>
<body>
    <form id="Form1" runat="server">
        <div>
        <table id="tabList" width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
            <tr class="TR_BG_list">
                <td class="list_link" align="center" colspan="2">
                    <br/>欢迎使用Foosun Inc. Collect System V1.0 For .Net<br/>
	                如果涉及到版权问题与四川风讯科技发展有限公司无关<br/>
	                您同意非要使用吗？如果同意，请输入采集数量!<br/>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" width="35%" align="right">
                    重复设置：
                </td>
                 <td class="list_link" width="65%" align="left">
                    <input type="checkbox" id="ChkNoRepeat" checked="checked" />标题相同则不重复采集
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" align="right">
                    设置本次采集数量：
                </td>
                <td class="list_link" align="left">
                    <input type="text" id='TxtNum' onkeydown="KeyDown();" class="form"/>
                    <script language="javascript" type="text/javascript">document.getElementById('TxtNum').focus();</script>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" align="center" colspan="2">
                    <input type="button" onclick="OnOK()" class="form" value=" 确 定 "/>&nbsp;&nbsp;
                    <input type="button" onclick="OnCancel()" class="form" value=" 取 消 "/>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>