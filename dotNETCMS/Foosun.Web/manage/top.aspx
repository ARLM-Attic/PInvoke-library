<%@ Page Language="C#" AutoEventWireup="true" Codebehind="top.aspx.cs" Inherits="Foosun.Web.manage.top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>无标题页</title>
    <link href="../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css"
        rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../Configuration/JS/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../Configuration/JS/Public.js"></script>

    <script language="JavaScript" type="text/javascript" src="../Configuration/JS/jspublic.js"></script>
            <script language="JavaScript" type="text/javascript">
<!--
function killErrors() {
return true;
}

window.onerror = killErrors;

// -->
    </script>

    <script language="JavaScript" type="text/javascript">
function CheckBTN1(theObj,URL,URLmain)
{
    var ns6 = document.getElementById&&!document.all
    if(ns6)
    {
        if (!theDownedButtonObj) {theDownedButtonObj='button_down';}
            if (theObj.className!='button')
            {
	            theObj.className='button';
	            theDownedButtonObj.className='button_down';
	            theDownedButtonObj=theObj;
	            window.parent.frames["menu"].location=URL;
	            if(URLmain!="")
	            {
	                window.parent.frames["sys_main"].location=URLmain;
	            }
            }
    }
    else
    {
        if (!theDownedButtonObj) {theDownedButtonObj=IDC_DownedBUtton;}
            if (theObj.className!='button')
            {
	            theObj.className='button';
	            theDownedButtonObj.className='button_down';
	            theDownedButtonObj=theObj;
	            window.parent.frames["menu"].location=URL;
	            if(URLmain!="")
	            {
	                window.parent.frames["sys_main"].location=URLmain;
	            }
            }
    }
}
function logout()
{
    window.parent.location = "Login.aspx";
}
    </script>

</head>
<body style="background-color: #eeeeee;">
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="indexq">
                            <tr>
                                <td style="width: 142px; height: 38px;">
                                    <img id="Img1" src="../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/logo.jpg" /></td>
                                <td style="width: 420px; height: 38px; text-align: center;">
                                </td>
                                <td align="right">
                                    <a href="#" onclick="logout();" class="Lion_1">退出</a>&nbsp;┊&nbsp;<a href="../<% Response.Write(Foosun.Config.UIConfig.dirUser); %>/index.aspx"
                                        target="_blank" class="Lion_1" title="进入会员中心">用户</a>&nbsp;┊&nbsp;<a href="../<% Response.Write(Foosun.Config.UIConfig.dirUser); %>/info/ChangePassword.aspx"
                                            target="sys_main" class="Lion_1" title="修改密码">密码</a>&nbsp;┊&nbsp;<a href="sys/skinChange.aspx"
                                                title="设置系统皮肤" target="sys_main" class="Lion_1">风格</a>&nbsp;┊&nbsp;<a href="ver/about.aspx"
                                                    target="sys_main" class="Lion_1" title="关于产品">关于</a>&nbsp;┊&nbsp;<a href="../help/HelpList.aspx"
                                                        target="sys_main" class="Lion_1" title="系统帮助">帮助</a>&nbsp;┊&nbsp;<a href="http://www.foosun.net"
                                                            target="sys_main" class="Lion_1" title="风讯官方站">Foosun.NET</a>&nbsp;&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="background-image: url(../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath());
                    %>/admin/menu_bg.jpg); width: 100%"><td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                     <tr>
                    
                    <td class="button_down" onclick="javascript:ChangeMenu(0);" style="cursor:pointer; " width=99>
                       隐藏菜单</td>
                    <td>
                        <div id="navi_index" runat="server" />
                     
                        <div class="menuskin" id="popmenu" onmouseover="clearhidemenu();highlightmenu(event,'on')"
                            style="z-index: 200" onmouseout="highlightmenu(event,'off');dynamichide(event)">
                        </div>
                    </td>
                    </tr></table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
var preFrameW = '160,*';
var FrameHide = 0;
function ChangeMenu(way){
	var addwidth = 10;
	var fcol = top.document.all.bodyFrame.cols;
	if(way==1) addwidth = 10;
	else if(way==-1) addwidth = -10;
	else if(way==0){
		if(FrameHide == 0){
			preFrameW = top.document.all.bodyFrame.cols;
			top.document.all.bodyFrame.cols = '0,*';
			FrameHide = 1;
			document.getElementById("closeopenmenu").innerHTML="打开左边";
			return;
		}else{
			top.document.all.bodyFrame.cols = preFrameW;
			FrameHide = 0;
			document.getElementById("closeopenmenu").innerHTML="关闭左边";
			return;
		}
	}
	fcols = fcol.split(',');
	fcols[0] = parseInt(fcols[0]) + addwidth;
	top.document.all.bodyFrame.cols = fcols[0]+',*';
}

</script>

