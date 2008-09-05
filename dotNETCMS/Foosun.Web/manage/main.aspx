<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" Inherits="Manage_main" Codebehind="~/manage/main.aspx.cs"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Public.js"></script>
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/jspublic.js"></script>
<link href="../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.divstyle {
	overflow:hidden;
	position:absolute;
	right:200px;
	top:200px;
	background:#FFFFE1 repeat-x left top;
	border:1px double #4F4F4F;
	width:300px;
	height:200px;
	text-align:left;
	padding-left:8px;
	padding-top:8px;
	padding-bottom:12px;
	padding-right:8px;
	clip:rect(auto, auto, auto, auto);
	z-index:50;
	filter: progid:DXImageTransform.Microsoft.DropShadow(color=#B6B6B6,offX=2,offY=2,positives=true);
</style>
</head>
<body onload="thisPageLoads()">
<form id="Form1" runat="server">
<table width="100%" height="47" border="0" cellpadding="0" cellspacing="0" class="mainnq">
  <tr>
    <td colspan="2" style="height: 8px"></td>
  </tr>
  <tr>
    <td height="37" colspan="2">&nbsp;&nbsp;<label class="Lion_welcome" id="welcome" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;[<asp:linkbutton id="linkbutton" CssClass="list_link" onclick="linkbutton_Click" runat="server">退出</asp:linkbutton>，<asp:linkbutton id="changePass" CssClass="list_link" onclick="changePass_Click" runat="server">修改密码</asp:linkbutton>]&nbsp;&nbsp;&nbsp;<label id="messageID" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;<a href="../<%Response.Write(Foosun.Config.UIConfig.dirUser); %>/info/userinfo_update.aspx" class="list_link" target="sys_main">[资料维护]</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="../<%Response.Write(Foosun.Config.UIConfig.dirUser); %>/friend/friendlist.aspx" class="list_link" target="sys_main">[我的好友]</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="../<%Response.Write(Foosun.Config.UIConfig.dirUser); %>/discuss/discussManageestablish_list.aspx" class="list_link" target="sys_main">[我的讨论组]</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="../<%Response.Write(Foosun.Config.UIConfig.dirUser); %>/info/url.aspx" class="list_link" target="sys_main">[网址收藏夹]</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="../help/help.aspx?HelpID=adminHelp" class="list_link" target="sys_main"><font color=red>[新手上路！]</font></a></td>
  </tr>
</table>
<table width="99%" border="0" align="center" cellpadding="1" cellspacing="0" class="table">
  <tr>
    <td height="24" style="background-color:#FFFFFF"><span id="checkveriframe" runat="server"></span></td>
  </tr>
</table>
<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top">
	  <table width="98%" border=0 cellpadding="0" cellspacing="0" class="table">
      <tr>
	  <td width="100" height="23" id="td_info" class="m_down_bg" onclick="javascript:ChangeDiv('info')" style="cursor:hand;"><div align="center">信息列表</div></td>
      <td width="100" height="23" class="m_up_bg" id="td_stat" onclick="javascript:ChangeDiv('stat')" style="cursor:hand;">统计信息</td>
      <td width="100" height="23"  class="m_up_bg" id="td_server" onclick="javascript:ChangeDiv('server')" style="cursor:hand;"><div align="center">服务器统计</div></td>
      <td width="100" height="23" class="m_up_bg" id="td_product" onclick="javascript:ChangeDiv('product')" style="cursor:hand;"><div align="center">产品信息</div></td>
      <td width="179" height="23" class="m_up_bg">&nbsp;</td>
    </tr>
      <tr id="div_info" style="display:block" class="TR_BG_list">
		  <td colspan="5">
                <div class="sys_topBg" style="padding-top:5px;padding-left:5px;font-size:14px;padding-bottom:5px;"><img src="../sysImages/folder/contenttitle.gif" border="0" />待审核的信息(最新5条)</div>
                <div style="padding-left:16px;" runat="server" id="unlockNewsList" />
                <div class="sys_topBg" style="padding-left:5px;font-size:14px;padding-top:10px;padding-bottom:5px;"><img src="../sysImages/folder/contenttitle.gif" border="0" />我发布的新闻(最新5条)</div>
                <div style="padding-left:16px;" runat="server" id="myNewsList" />
		  </td>
	  </tr>
      <tr id="div_server" style="display:none" class="TR_BG_list">
		  <td colspan="5">
		  <label id="div_server_1" runat="server" />
		  </td>
	  </tr>
      <tr id="div_product" style="display:none" class="TR_BG_list">
		  <td colspan="5">
		  <label id="div_product_1" runat="server" />
		  </td>
	  </tr>
      <tr id="div_stat" style="display:none" class="TR_BG_list">
		  <td colspan="5">
		  <label id="div_stat_1" runat="server" />
		  </td>
	  </tr>
    </table>
    </td>
    <td width="183" align="left" valign="top">
    <table width="172" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td style="width: 172px" valign="top">
        <asp:Calendar ID="Calendar2" runat="server" BackColor="Transparent" BorderColor="Gainsboro"
                BorderWidth="1px" Font-Names="宋体" Font-Size="Small" Font-Underline="False" ForeColor="DimGray"
                SelectMonthText="下月" ShowGridLines="True" Width="100%" OnSelectionChanged="Calendar2_SelectionChanged">
                <SelectedDayStyle BackColor="WhiteSmoke" ForeColor="#FF8000" />
                <TodayDayStyle BackColor="Gainsboro" BorderColor="Transparent" Font-Bold="True" ForeColor="Red"
                    Wrap="False" />
                <DayStyle BackColor="White" BorderColor="Control" />
                <WeekendDayStyle BackColor="White" />
                <OtherMonthDayStyle BackColor="Transparent" ForeColor="LightGray" />
                <NextPrevStyle BackColor="Transparent" />
                <DayHeaderStyle BackColor="WhiteSmoke" BorderColor="WhiteSmoke" Font-Names="Arial Black" />
                <TitleStyle BackColor="Transparent" />
            </asp:Calendar>
        </td>
      </tr>
      <tr>
        <td height="10" style="width: 172px"></td>
      </tr>
      <tr>
        <td height="20" align="left" style="width: 172px"><label id="Todaydate" runat="server" /></td>
      </tr>
      <tr>
        <td height="30" align="left" class="Lion_2" style="width: 172px"><a href="../<% Response.Write(Foosun.Config.UIConfig.dirUser); %>/info/Logscreat.aspx" target="sys_main" class="list_link_o">创建</a> <span class="list_link_o">|</span> <a href="../<% Response.Write(Foosun.Config.UIConfig.dirUser); %>/info/Logs.aspx" target="sys_main" class="list_link_o">管理</a></td>
      </tr>
<%--      <tr>
        <td height="20" align="left" class="Lion_2" style="width: 172px"><label id="weather" runat="server" /></td>
      </tr>
--%>    </table>
     </td>
  </tr>
</table>
<table width="98%" border="0" cellpadding="6" cellspacing="0" align="center" class="table">
    <tr class="TR_BG_list">
      <td align="left" class="Lion_7"><span class="Lion_6">关于产品：</span><br />
        生产商：Foosun Inc.&nbsp;&nbsp;&nbsp;&nbsp;生产版本：<%Response.Write(Foosun.Config.verConfig.Productversion); %><br />
        咨询电话：(+86)028-85098980/66026180&nbsp;&nbsp;&nbsp;&nbsp;咨询QQ：52159324&nbsp;&nbsp;532311018&nbsp;&nbsp;394226379&nbsp;&nbsp;&nbsp;&nbsp;Mail：yujs888@126.com<br />
       </td>
    </tr>
</table> 
<br />
<br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 38px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
 </table> 
<div id="ClistID" class="divstyle" style="display:none;">
<div><a href="javascript:;" onclick="closediv('ClistID')">关闭</a></div>
    loading...
</div> 
</form>
</body>
<script type="text/javascript" language="javascript">
    function thisPageLoads()
    {
        ChangeDiv("info");
    }
	function ChangeDiv(ID)
	{
		document.getElementById('td_info').className='m_up_bg';
		document.getElementById('td_server').className='m_up_bg';
		document.getElementById('td_product').className='m_up_bg';
		document.getElementById('td_stat').className='m_up_bg';
		document.getElementById("td_"+ID).className='m_down_bg';

		document.getElementById("div_info").style.display="none";
		document.getElementById("div_server").style.display="none";
		document.getElementById("div_product").style.display="none";
		document.getElementById("div_stat").style.display="none";
		document.getElementById("div_"+ID).style.display="";
	}
    function ShowC(gid,divID)
    {
        document.getElementById(divID).style.display="block"; 
        var Action='id='+gid;   var options={ 
                          method:'get', 
                          parameters:Action, 
                          onComplete:function(transport) 
                          { 
                              var returnvalue=transport.responseText; 
                              if (returnvalue.indexOf("??")>-1) 
                                  document.getElementById(divID).innerHTML=''; 
                              else 
                                  document.getElementById(divID).innerHTML=returnvalue; 
                          } 
                       };   

           new  Ajax.Request('../userlogs.aspx?no-cache='+Math.random(),options);
    }
   function closediv(divID)
   {
        document.getElementById(divID).style.display="none"; 
   } 
</script>
</html>
