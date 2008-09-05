<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_user_useraction" Codebehind="useraction.aspx.cs" %>

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
     <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
              <td height="1" colspan="2"></td>
            </tr>
            <tr>
              <td width="57%" style="padding-left:14px;">会员管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userlist.aspx" class="list_link" target="sys_main">会员管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />积分/G币处理</td>
            </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
      <tr>
        <td class="TR_BG_list" style="height:50px">
            <asp:HiddenField ID="hidden_uid" runat="server" />
            <div id="actionContent" runat="server" />
        </td>
      </tr>
    </table> 
  <br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
</table>  
    
    
    
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
    function bIpoint(obj,obj1)
    {
        if(confirm('确定进行此操作吗?'))
        {
	        self.location.href="useraction.aspx?sPointType=bIpoint&uid="+obj+"&point="+obj1+"";
        }
    }
    function sIpoint(obj,obj1)
    {
        if(confirm('确定进行此操作吗?'))
        {
	    self.location.href="useraction.aspx?sPointType=sIpoint&uid="+obj+"&point="+obj1+"";
	    }
    }
    function bGpoint(obj,obj1)
    {
        if(confirm('确定进行此操作吗?'))
        {
	    self.location.href="useraction.aspx?sPointType=bGpoint&uid="+obj+"&point="+obj1+"";
	    }
    }
    function sGpoint(obj,obj1)
    {
        if(confirm('确定进行此操作吗?'))
        {
	    self.location.href="useraction.aspx?sPointType=sGpoint&uid="+obj+"&point="+obj1+"";
	    }
    }
</script>
