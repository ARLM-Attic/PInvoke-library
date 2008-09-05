<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_Reg_Result" Debug="true" Codebehind="Reg_Result.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
    var intLeft = 10; 
    function leavePage() 
    {
        if (0 == intLeft)
        {
	        window.location.href="index.aspx";
	    }
        else 
        {
            intLeft -= 1;
            document.all.countdown.innerText = intLeft + " ";
            setTimeout("leavePage()", 1000);
        }
    }
</script>
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>__注册结束</title>
<link href="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body onload="setTimeout('leavePage()', 1000)">
<form id="form1" name="form1" method="post" action="" runat="server">
<div style="width:100%" id="topshow">
    <table border="0" cellpadding="2" class="2" style="width:100%;">
    <tr>
        <td style="width:30%;"><a href="http://www.foosun.net" target="_blank"><img src="../sysImages/user/userlogo.gif" border="0" /></a></td>
        <td style="width:70%;">此处插入您的广告</td>
    </tr>
    </table>
</div>
 <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
   <tr>
     <td style="padding:5px 8px 5 5px;"><div style="width:98%;">敬爱的&nbsp;<asp:Label ID="username" runat="server" Width="103px"></asp:Label>&nbsp;用户你好！恭喜你已注册成功<span class="reshow"> &nbsp; &nbsp;<%Response.Write(Request.QueryString["Error"]); %></span></div></td>
   </tr>
 </table>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
            <tr class="TR_BG_list">
                <td class="list_link" align="center">
                    系统将在<span id="countdown" style="font-size:20px"> <script language="JavaScript">document.write(intLeft);</script>
                              </span>秒后自动转入  <a href="index.aspx" class="list_link">会员中心</a>
                              
                              </td>
            </tr>
        </table>        
</form>
</body>
</html>
