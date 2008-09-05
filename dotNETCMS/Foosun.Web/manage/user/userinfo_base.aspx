<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_user_userinfo_base" Codebehind="userinfo_base.aspx.cs" %>

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
              <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userlist.aspx" class="list_link">会员管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />基本状态/实名认证</div></td>
            </tr>
    </table>
          <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
              <td style="padding-left:14px;"><a class="topnavichar" href="userinfo.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">基本信息</a>　<a class="topnavichar" href="userinfo_contact.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">联系资料</a>　<a class="topnavichar" href="userinfo_safe.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">安全资料</a>　<a class="topnavichar" href="userinfo_base.aspx?id=<% Response.Write(Request.QueryString["id"]);%>"><font color="red">基本状态/实名认证</font></a></td>
            </tr>
    </table>
      <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
       
       
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">锁定</div></td> 
          <td class="list_link"><label runat="server" id="lockTF" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_base_0001',this)">帮助</span></td>
          </tr>
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">管理员</div></td> 
          <td class="list_link"><label runat="server" id="adminTF" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_base_0002',this)">帮助</span></td>
          </tr>          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">会员组</div></td> 
          <td class="list_link"><label id="GroupList" runat="server" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_base_0003',this)">帮助</span></td>
          </tr>

        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">证件类型</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="CertType" ReadOnly runat="server" Width="250"></asp:TextBox>
          <select class="form" name="glist" onchange="document.form1.CertType.value=this.options[this.selectedIndex].text;;">
          <option value="">选择证件</option>
          <option value="身份证">身份证</option>
          <option value="军官证">军官证</option>
          <option value="学生证">学生证</option>
          <option value="其他">其他</option>
          </select>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_userinfo_base_0004',this)">帮助</span></td>
          </tr>
          
        <tr class="TR_BG_list"><label id="IDcard" runat="server" />
          <td class="list_link" style="width: 114px"><div align="right">证件号码</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="CertNumber" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_0005',this)">帮助</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class="list_link" href="userIDCard.aspx?id=<% Response.Write(Request.QueryString["id"]);%>">认证状态:<span id="isCerts" runat="server" /></a></td>
        </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">积分</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="ipoint" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_0006',this)">帮助</span></td>
        </tr> 
        
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">G币</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="gpoint" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_0007',this)">帮助</span></td>
        </tr>        
        
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">魅力值</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="cpoint" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_0008',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">人气值</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="epoint" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_0009',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">活跃值</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="apoint" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_00010',this)">帮助</span></td>
        </tr> 
        
         <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">注册日期</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ReadOnly ID="RegTime" runat="server"  Width="250"></asp:TextBox>
          <input  class="form" type="button" value="选择日期"  onclick="selectFile('date',document.form1.RegTime,140,500);document.form1.RegTime.focus();" /> 
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_00011',this)">帮助</span></td>
        </tr>
 
         <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">最后登陆</div></td>
          <td class="list_link"><asp:TextBox CssClass="form"  ReadOnly ID="LastLoginTime" runat="server"  Width="250"></asp:TextBox>
          <input  class="form" type="button" value="选择日期"  onclick="selectFile('date',document.form1.LastLoginTime,140,500);document.form1.LastLoginTime.focus();" /> 
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_00017',this)">帮助</span></td>
        </tr>
       
       <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">在线时间</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="onlineTime" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_00012',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">登陆次数</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="LoginNumber" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_00013',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">同一IP最大登陆次数</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="LoginLimtNumber" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_00014',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">最后登陆IP</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="lastIP" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_00015',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px"><div align="right">所属频道</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="TxtSite" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_userinfo_base_00016',this)">帮助</span></td>
        </tr> 
                

        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 28px;">&nbsp;</td>
          <td class="list_link" style="height: 28px">&nbsp;<asp:Button ID="sumbitsave" runat="server" CssClass="form" Text=" 确 定 "  OnClick="submitSave" />
            <input name="reset" type="reset" value=" 重 置 "  class="form"><asp:HiddenField ID="userID" runat="server" />
              说明：此项前台会员是不允许修改的         </td>
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
 <script type="text/javascript" language="javascript">
    function getFormInfo(obj,value)
    {
       alert(value);
       document.obj.CertType.value=value;
    }

</script>