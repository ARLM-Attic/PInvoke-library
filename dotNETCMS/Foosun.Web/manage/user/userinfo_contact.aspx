<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_user_userinfo_contact" Codebehind="userinfo_contact.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
      <form id="form1" runat="server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >修改会员资料</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userlist.aspx" class="list_link">会员管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />联系资料</div></td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;"><a class="topnavichar" href="userinfo.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">基本信息</a>　<a class="topnavichar" href="userinfo_contact.aspx?id=<% Response.Write(Request.QueryString["id"]);%>"><font color="red">联系资料</font></a>　<a class="topnavichar" href="userinfo_safe.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">安全资料</a>　<a class="topnavichar" href="userinfo_base.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">基本状态/实名认证</a></td>
        </tr>
</table>

      <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
        
                                                                                                                                                                                                                                                                                            
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">省份</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="province" runat="server"  Width="250"  MaxLength="20"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0002',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">城市</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="City" runat="server"  Width="250"  MaxLength="20"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_update_0003',this)">帮助</span> </td>
          </tr>
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">地址</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="Address" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0004',this)">帮助</span></td>
        </tr>
 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">邮政编码</div></td>
          <td class="list_link"> <asp:TextBox CssClass="form" ID="Postcode" runat="server"  Width="250"  MaxLength="10"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0005',this)">帮助</span></td>
        </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">家庭联系电话</div></td>
          <td class="list_link"> <asp:TextBox CssClass="form" ID="FaTel" runat="server"  Width="250"  MaxLength="30"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0006',this)">
              帮助</span></td>
        </tr>        
        
         <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">工作电话</div></td>
          <td class="list_link"> <asp:TextBox CssClass="form" ID="WorkTel" runat="server"  Width="250"  MaxLength="30"></asp:TextBox> 
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0007',this)">帮助</span></td>
        </tr>      
        

          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">传真</div></td>  
          <td class="list_link"><asp:TextBox CssClass="form" ID="Fax" runat="server"  Width="250"  MaxLength="30"></asp:TextBox>   
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0009',this)">帮助</span></td>
        </tr>      


          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">QQ</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="QQ" runat="server"  Width="250"  MaxLength="30"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0010',this)">帮助</span></td>
        </tr>     
 
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">MSN</div></td>
          <td class="list_link"> <asp:TextBox CssClass="form" ID="MSN" runat="server"  Width="250"  MaxLength="30"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0011',this)">帮助</span></td>
        </tr>  
         

        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 28px;">&nbsp;</td>
          <td class="list_link" style="height: 28px">&nbsp;<asp:Button ID="sumbitsave" runat="server" CssClass="form" Text=" 确 定 "  OnClick="submitSave" />
            <input name="reset" type="reset" value=" 重 置 "  class="form"> <asp:HiddenField ID="suid" runat="server" />         </td>
        </tr>

</table></form>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat=server /></td>
   </tr>
 </table>

</body>
</html>