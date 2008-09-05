<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="user_info_userinfo_update" Codebehind="userinfo_update.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server"><table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >修改会员资料</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userinfo.aspx" class="list_link">我的资料</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改基本资料</div></td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;"><a class="topnavichar" href="userinfo.aspx">我的资料</a>　<a class="topnavichar" href="userinfo_update.aspx"><font color="red">修改基本信息</font></a>　<a class="topnavichar" href="userinfo_contact.aspx">修改联系资料</a>　<a class="topnavichar" href="userinfo_safe.aspx">修改安全资料</a>　<a class="topnavichar" href="userinfo_idcard.aspx">修改实名认证</a></td>
        </tr>
</table>
      
      <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
        
        <tr class="TR_BG_list">
          <td width="114" class="list_link" style="width: 114px"><div align="right">昵称</div></td>
          <td width="591" class="list_link"><asp:TextBox ID="NickName" runat="server"  Width="250"  MaxLength="20" CssClass="form"></asp:TextBox> &nbsp;&nbsp; Email:<%Response.Write(gEmaill); %>(不能修改) <input type="hidden" value="<% Response.Write(gEmaill);%>" name="gEmaill" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_update_0001',this)">帮助</span><asp:RequiredFieldValidator ID="f_NickName" runat="server" ControlToValidate="NickName" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写昵称，最大长度为20字符</span>"></asp:RequiredFieldValidator></td>
          <td width="219" rowspan="6" class="list_link" style="width:196px;text-align:center;"><label id="userFace_div" runat="server" /></td>
        </tr>   
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">真实姓名</div></td>
          <td class="list_link"><asp:TextBox ID="RealName" runat="server"  Width="250"  MaxLength="20" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_update_00031',this)">帮助</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NickName" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写真实姓名，最大长度为20字符</span>"></asp:RequiredFieldValidator></td>
        </tr>     
                                                                                                                                                                                                                                                                                                       
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">性别</div></td>
          <td class="list_link"><label id="sex" runat="server" />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_00038',this)">帮助</span></td>
        </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">出生日期</div></td>
          <td class="list_link"><asp:TextBox ID="birthday" runat="server"  Width="250"  MaxLength="12" CssClass="form" onclick="selectFile('date',document.form1.birthday,160,500);document.form1.birthday.focus();"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_update_00032',this)">帮助</span>       <asp:RegularExpressionValidator ID="f_birthday" runat="server"  ControlToValidate="birthday"  ErrorMessage="正确填写出生日期" ValidationExpression="^[12]{1}(\d){3}[-][01]?(\d){1}[-][0123]?(\d){1}$"></asp:RegularExpressionValidator></td>
        </tr>
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">民族</div></td>
          <td class="list_link"><asp:TextBox ID="Nation" runat="server"  Width="250"  MaxLength="12" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0004',this)">帮助</span></td>
        </tr>
 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">籍贯</div></td>
          <td class="list_link"> <asp:TextBox ID="nativeplace" runat="server"  Width="250"  MaxLength="20" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0005',this)">帮助</span></td>
        </tr>
        
         <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">头像</div></td>
          <td colspan="2" class="list_link"> <asp:TextBox ID="UserFace" runat="server"  Width="280px"  MaxLength="220" CssClass="form"></asp:TextBox>
              &nbsp;
              <input class="form" type="button" value="系统头像"  onclick="selectFile('user_Hpic',document.form1.UserFace,300,500);document.form1.UserFace.focus();" style="width: 64px" />&nbsp;
              <input class="form" type="button" value="自定义头像"  onclick="selectFile('user_pic',document.form1.UserFace,300,500);document.form1.UserFace.focus();" style="width: 85px" /> 头像宽|高<asp:TextBox ID="userFacesize" runat="server"  Width="62px"  MaxLength="7" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_00033',this)">帮助</span></td>
        </tr>    
          
        <tr class="TR_BG_list" id="UserInfo_div" runat="server">
          <td class="list_link" style="width: 114px"><div align="right">用户签名</div></td>
          <td colspan="2" class="list_link"><asp:TextBox ID="Userinfo" runat="server" TextMode="MultiLine" MaxLength="3000" Width="400px" Height="100px" CssClass="form"></asp:TextBox>(不支持HTML语法)
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_00034',this)">
              帮助</span></td>
        </tr>        
        
        
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">性格</div></td>
          <td colspan="2" class="list_link"><asp:TextBox ID="character" runat="server" TextMode="MultiLine" MaxLength="3000" Width="400px" Height="100px" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0008',this)">帮助</span></td>
        </tr>      
        
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">爱好</div></td>
          <td colspan="2" class="list_link"><asp:TextBox ID="UserFan" runat="server" TextMode="MultiLine" MaxLength="3000" Width="400px" Height="100px" CssClass="form"></asp:TextBox>   
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_00035',this)">帮助</span></td>
        </tr>      


          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">组织关系</div></td>
          <td colspan="2" class="list_link"><asp:TextBox ID="orgSch" runat="server"  Width="250"  MaxLength="12" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_00036',this)">帮助</span></td>
        </tr>     
 
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">职业</div></td>
          <td colspan="2" class="list_link"> <asp:TextBox ID="job" runat="server"  Width="250"  MaxLength="30" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_00037',this)">帮助</span></td>
        </tr>   
        
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">学历</div></td>
          <td colspan="2" class="list_link"><asp:TextBox ID="education" runat="server"  Width="250"  MaxLength="20" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0012',this)">帮助</span></td>
        </tr>   
        
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">毕业院校</div></td>
          <td colspan="2" class="list_link"><asp:TextBox ID="Lastschool" runat="server"  Width="250"  MaxLength="80" CssClass="form"></asp:TextBox> 
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0013',this)">帮助</span></td>
        </tr>   
 
          <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">是否结婚</div></td>
          <td colspan="2" class="list_link"><label id="marriage" runat="server" />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_0014',this)">帮助</span></td>
        </tr>    

         
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">对外开放联系方式</div></td>
          <td colspan="2" class="list_link"><label id="isopen" runat="server" />
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_update_00015',this)">帮助</span></td>
        </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 28px;">&nbsp;</td>
          <td colspan="2" class="list_link" style="height: 28px">&nbsp;<asp:Button ID="sumbitsave" runat="server" CssClass="form" Text=" 确 定 "  OnClick="submitSave" />
            <input name="reset" type="reset" value=" 重 置 "  class="form">          </td>
        </tr>
</table>
</form>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat=server /></td>
   </tr>
 </table>

</body>
</html>
<script language="javascript" type="text/javascript">
new Form.Element.Observer($('UserFace'),1,pics_1);
function pics_1()
    {
	    if ($('UserFace').value=='')
	    {
		    $('changeFace').src='../../sysImages/user/noHeadpic.gif';
	    }
	    else
	    {
	    $('changeFace').src=$('UserFace').value.replace('{@UserdirFile}','<% Response.Write(Rdir); %>');
	    }
    } 
</script>