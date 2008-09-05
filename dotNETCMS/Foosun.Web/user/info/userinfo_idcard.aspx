<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_info_userinfo_idcard" Codebehind="userinfo_idcard.aspx.cs" %>

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
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userinfo.aspx" class="list_link">我的资料</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改安全资料</div></td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;"><a class="topnavichar" href="userinfo.aspx">我的资料</a>　<a class="topnavichar" href="userinfo_update.aspx">修改基本信息</a>　<a class="topnavichar" href="userinfo_contact.aspx">修改联系资料</a>　<a class="topnavichar" href="userinfo_safe.aspx">修改安全资料</a>　<a class="topnavichar" href="userinfo_idcard.aspx"><font color="red">修改实名认证</font></a></td>
        </tr>
</table>

    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
              <td style="padding-left:14px;">认证状态：<span id="icardcertstat" runat="server" /></td>
            </tr>
    </table>
    <label id="isCertstat" runat="server" />
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
 <script language="javascript" type="text/javascript">
    function subup()
    {
       if(document.form1.f_IDcardFiles.value==""||document.form1.f_IDcardFiles.value==null)
       {
            alert('请选择一张图片!');
            return false;
       }
       else
       {
            if(document.form1.f_IDcardFiles.value.indexOf("/")==-1)
           {
            alert('图片地址不正确!');
            return false;
           }             
            if(document.form1.f_IDcardFiles.value.length<5)
           {
            alert('图片地址不正确!');
            return false;
           }  
            if(confirm("你确定要认证吗?"))
            {
	            document.form1.action="?isCert=postICARD";
	            document.form1.submit();
	        }
       }
    }
 
var f1tf ="foosun<%Response.Write(f1); %>";
if(f1tf=="foosun")
{    
    new Form.Element.Observer($('f_IDcardFiles'),1,pics_1);
	    function pics_1()
		    {
			    if ($('f_IDcardFiles').value=='')
			    {
				    $('imgsrc').src='../../sysImages/normal/nopic.gif'
			    }
			    else
			    {
			    $('imgsrc').src=$('f_IDcardFiles').value.toLowerCase().replace('{@userdirfile}','<% Response.Write(Foosun.Config.UIConfig.UserdirFile); %>');
			    }
		    } 
}
 </script>