<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="user_info_getMobile" Codebehind="getMobile.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>__捆绑手机</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat = "Server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >捆绑手机</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userinfo.aspx" class="list_link">我的资料</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />捆绑手机</div></td>
        </tr>
        </table>
       <div align="center" id="getmobileDiv" runat="server">
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px">
            输入手机/小灵通 </td>  
          <td class="list_link" align="left"><asp:TextBox ID="Mobile" MaxLength="15" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="f_Mobile" runat="server" ControlToValidate="Mobile" Display="Dynamic" ErrorMessage="<span class='reshow'>请输入您手机/小灵通号码</span>"></asp:RequiredFieldValidator><asp:CheckBox onclick="showDU();" ID="bindTF" Text="捆绑手机/小灵通" runat="server" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_bMobile_001',this)">帮助</span></td>
          </tr>          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"></td> 
          <td class="list_link" align="left">
             <asp:Button ID="Button1" runat="server" CssClass="form" Text="开始操作" OnClick="Button1_Click" OnClientClick="{if(confirm('确定要操作吗？')){return true;}return false;}" />
          </td>
          </tr>
      </table>
      </div>
      <div align="center" id="bindCode" runat="server">
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px">
            您输入的手机号码为 </td>  
          <td class="list_link" align="left">
          <label id="_tmpMobile" runat="server" />
          </td>
          </tr>          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px">
            请输入收到的验证码 </td>  
          <td class="list_link" align="left">
          <asp:TextBox ID="bindCodeNum" MaxLength="20" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="f_bindCodeNum" runat="server" ControlToValidate="bindCodeNum" Display="Dynamic" ErrorMessage="<span class='reshow'>请输入您手机/小灵通收到的验证码</span>"></asp:RequiredFieldValidator>
          <asp:HiddenField ID="BindMobile" runat="server" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_bMobile_002',this)">帮助</span></td>
          </tr>          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"></td> 
          <td class="list_link" align="left">
          <asp:Button ID="Button2" runat="server" CssClass="form" Text="下一步" OnClientClick="{if(confirm('确定要捆绑吗？')){return true;}return false;}" OnClick="Button1_Click_bindSave" />
          </td>
          </tr>
      </table> 
     </div>
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
      <br />
     <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><label id="copyright" runat="server" /></td>
       </tr>
     </table>   </body>
</html>
<script type="text/javascript" language="javascript">
function showDU()
{
    if(document.getElementById("bindTF").checked)
    {
        document.getElementById("showContents").style.display="";
    }
    else
    {
        document.getElementById("showContents").style.display="none";
    }
    
}

</script>