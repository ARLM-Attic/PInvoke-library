<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_info_MobileValidate" Codebehind="MobileValidate.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>__验证手机</title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
    <table width="98%" border="0" cellpadding="0" align="center" cellspacing="0" class="toptable">
            <tr>
              <td height="1" colspan="2"></td>
            </tr>
            <tr>
                <td class="topnavichar" colspan="2" height="32" style="padding-left: 14px">
                    验证您的手机</td>
            </tr>
    </table>
      <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
        <tr class="TR_BG">
          <td style="PADDING-LEFT: 14px;height:30px;" class="sys_topBg"><label id="uid_div" class="reshow" runat="server" />！您已经是本站用户。但需要手机验证，才能登陆本系统。</td>
        </tr>
        <tr class="TR_BG_list">
          <td style="PADDING-LEFT: 14px;height:30px;">请输入您注册的手机&nbsp;<label id="RegMobile" runat="server" />&nbsp;收到的验证码：<asp:TextBox ID="MobileCode" CssClass="form" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox onclick="showDU();" ID="BindTF" runat="server" />捆绑手机&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="开始验证" OnClick="Button1_Click" />&nbsp;&nbsp;<span id="forgetPass" runat="server" />&nbsp;&nbsp;<div style="padding-top:8px;" id="ShowInfo" runat="server" /></td>
        </tr>
      </table>
      
      <table width="98%" id="showContents" style="display:none;" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
        <tr class="TR_BG">
          <td style="PADDING-LEFT: 14px;height:30px;" class="sys_topBg"><label id="Label1" class="reshow" runat="server" />捆绑手机/小灵通条约</td>
        </tr>
        <tr class="TR_BG_list">
          <td style="PADDING-LEFT: 14px;height:30px;"><div style="padding-top:8px;" id="showContents_div" runat="server" /></td>
        </tr>
      </table>      
    </form>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
 </table>
 </body>
</html>
<script type="text/javascript" language="javascript">
function showDU()
{
    if(document.getElementById("BindTF").checked)
    {
        document.getElementById("showContents").style.display="";
    }
    else
    {
        document.getElementById("showContents").style.display="none";
    }
    
}

</script>