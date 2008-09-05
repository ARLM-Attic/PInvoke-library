<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_user_userinfo" Codebehind="userinfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
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
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userlist.aspx" class="list_link">会员管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改基本资料</div></td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;"><a class="topnavichar" href="userinfo.aspx?id=<% Response.Write(Request.QueryString["id"]);%>"><font color="red">基本信息</font></a>　<a class="topnavichar" href="userinfo_contact.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">联系资料</a>　<a class="topnavichar" href="userinfo_safe.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">安全资料</a>　<a class="topnavichar" href="userinfo_base.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">基本状态/实名认证</a></td>
        </tr>
</table>
      
      <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">昵称</div></td>
          <td class="list_link"><asp:TextBox ID="NickName" CssClass="form" runat="server"  Width="250"  MaxLength="20"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_manage_0001',this)">帮助</span><asp:RequiredFieldValidator ID="f_NickName" runat="server" ControlToValidate="NickName" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写昵称，最大长度为20字符</span>"></asp:RequiredFieldValidator></td>
        </tr>   
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">真实姓名</div></td>
          <td class="list_link"><asp:TextBox ID="RealName" runat="server"  Width="250"  MaxLength="20" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_update_00030',this)">帮助</span></td>
        </tr>   
         
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">会员组</div></td>
          <td class="list_link"><label id="GroupNumber" runat="server" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_update_group',this)">帮助</span></td>
        </tr>   
                                                                                                                                                                                                                                                                                                                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">性别</div></td>
          <td class="list_link"><label id="sex" runat="server" />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0002',this)">帮助</span></td>
        </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">电子邮件</div></td>
          <td class="list_link"><asp:TextBox  CssClass="form" ID="email" runat="server"  Width="250"  MaxLength="220"></asp:TextBox>
          </td>
          </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">出生日期</div></td>
          <td class="list_link"><asp:TextBox  CssClass="form" ID="birthday" runat="server"  Width="250"  MaxLength="12"></asp:TextBox>
          <img src="../../sysImages/folder/s.gif" title="选择日期" style="cursor:pointer;" onclick="selectFile('date',document.form1.birthday,140,500);document.form1.birthday.focus();" /> 
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_manage_0003',this)">帮助</span>       <asp:RegularExpressionValidator ID="f_birthday" runat="server"  ControlToValidate="birthday"  ErrorMessage="正确填写出生日期" ValidationExpression="^[12]{1}(\d){3}[-][01]?(\d){1}[-][0123]?(\d){1}$"></asp:RegularExpressionValidator></td>
          </tr>
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">民族</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="Nation" runat="server"  Width="250"  MaxLength="12"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0004',this)">帮助</span></td>
        </tr>
 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">籍贯</div></td>
          <td class="list_link"> <asp:TextBox CssClass="form" ID="nativeplace" runat="server"  Width="250"  MaxLength="20"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0005',this)">帮助</span></td>
        </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">用户签名</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="Userinfo" runat="server" TextMode="MultiLine" MaxLength="3000" Width="400px" Height="100px"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0006',this)">
              帮助</span></td>
        </tr>        
        
         <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">头像</div></td>
          <td class="list_link"> <asp:TextBox CssClass="form" ID="UserFace" runat="server"  Width="250"  MaxLength="220"></asp:TextBox><img src="../../sysImages/folder/s.gif" title="选择日期" style="cursor:pointer;" onclick="selectFile('pic',document.form1.UserFace,350,500);document.form1.UserFace.focus();" />  头像宽|高&nbsp;<asp:TextBox ID="userFacesize" runat="server"  Width="85" CssClass="form"  MaxLength="7"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0007',this)">帮助</span></td>
        </tr>      
        
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">性格</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="character" runat="server" TextMode="MultiLine" MaxLength="3000" Width="400px" Height="100px"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0008',this)">帮助</span></td>
        </tr>      
        
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">爱好</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="UserFan" runat="server" TextMode="MultiLine" MaxLength="3000" Width="400px" Height="100px"></asp:TextBox>   
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0009',this)">帮助</span></td>
        </tr>      


          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">组织关系</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="orgSch" runat="server"  Width="250"  MaxLength="12"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0010',this)">帮助</span></td>
        </tr>     
 
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">职业</div></td>
          <td class="list_link"> <asp:TextBox CssClass="form" ID="job" runat="server"  Width="250"  MaxLength="30"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0011',this)">帮助</span></td>
        </tr>   
        
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">学历</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="education" runat="server"  Width="250"  MaxLength="20"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0012',this)">帮助</span></td>
        </tr>   
        
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">毕业院校</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="Lastschool" runat="server"  Width="250"  MaxLength="80"></asp:TextBox> 
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0013',this)">帮助</span></td>
        </tr>   
 
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">是否结婚</div></td>
          <td class="list_link"><label id="marriage" runat="server" />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_0014',this)">帮助</span></td>
        </tr>    

         
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">对外开放联系方式</div></td>
          <td class="list_link"><label id="isopen" runat="server" />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_manage_00015',this)">帮助</span></td>
        </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 28px;">&nbsp;</td>
          <td class="list_link" style="height: 28px">&nbsp;<asp:Button ID="sumbitsave" runat="server" OnClick="submitSave" CssClass="form" Text=" 确 定 " />
            <input name="reset" type="reset" value=" 重 置 "  class="form" />  
              <asp:HiddenField ID="suid" runat="server" />        </td>
        </tr>

</table></form>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
 </table>
</body>
</html>
