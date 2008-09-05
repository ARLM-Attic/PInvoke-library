<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_General_Edit_Manage" Codebehind="General_Edit_Manage.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body onload="javascript:load('<%=Request.QueryString["kkey"] %>');">
<table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
  <tr>
    <td height="1" colspan="2"></td>
  </tr>
  <tr>
     <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">新闻常规管理</td>
    <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />常规管理</div></td>
  </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
  <tr>
    <td height="18" style="width: 45%" colspan="2"><div align="left"><font color="#ff0000" size="2">功能:</font> | <a href="General_manage.aspx?key=6" class="topnavichar">管理首页</a> | <a href="General_Add_Manage.aspx" class="topnavichar">添加</a> | <a href="General_manage.aspx?key=0" class="topnavichar" onclick=""> 关键字(TAG)</a> | <a href="General_manage.aspx?key=1" class="menulist">来源</a> | <a href="General_manage.aspx?key=2" class="topnavichar">作者</a> | <a href="General_manage.aspx?key=3" class="topnavichar">内部链接</a> | <a href="General_manage.aspx?type=delall" onclick="{if(confirm('确认删除全部所有添加的信息吗？')){return true;}return false;}" class="topnavichar">删除全部</a></div></td>
  </tr>
</table>
<form id="form1" method="post" runat="server" onsubmit="javascript:return check();" >
  <table width="98%" border="0" cellpadding="5" align="center" cellspacing="1" class="table" id="OM_AddNew">
    <tr class="TR_BG_list">
      <td colspan="2" class="list_link"><strong>修 改</strong></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="10%" align="Left" style="width: 11%; height: 29px;" class="list_link">类型</td>
      <td width="90%" align="Left" class="list_link" style="height: 29px" ><asp:DropDownList ID="Sel_Type" runat="server" Style="position: relative" Width="15%" onchange="SelectOpType(this.value)" CssClass="form">
          <asp:ListItem Selected="True" Value="9">请选择</asp:ListItem>
          <asp:ListItem Value="0">关键字(TAG)</asp:ListItem>
          <asp:ListItem Value="1">来源</asp:ListItem>
          <asp:ListItem Value="2">作者</asp:ListItem>
          <asp:ListItem Value="3">内部连接</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_GenType_0001',this)">帮助</span> </td>
    </tr>
    <tr id="Tr_Title" class="TR_BG_list" style="display:none">
      <td  align="Left" style="width: 11%; height: 28px;" class="list_link">标题</td>
      <td align="Left" class="list_link" style="height: 28px" ><asp:TextBox ID="Txt_Name" MaxLength="50" runat="server" CssClass="form"/>
        (<font color=red size=2>*</font>)<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_GenTitle_0001',this)">帮助</span></td>
    </tr>
    <tr id="Tr_Url" class="TR_BG_list" style="display:none">
      <td height="-5"  align="Left" style="width: 11%"  class="list_link">链接地址</td>
      <td align="Left" class="list_link" height="-5"><asp:TextBox ID="Txt_LinkUrl" MaxLength="50"  runat="server" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_GenUrl_0001',this)">帮助</span></td>
    </tr>
    <tr id="Tr_Email" class="TR_BG_list" style="display:none">
      <td height="-5" align="Left" style="width: 11%"  class="list_link">电子邮件</td>
      <td height="-5" align="Left" class="list_link"><asp:TextBox ID="Txt_Email" runat="server" MaxLength="50" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_GenEmail_0001',this)">帮助</span></td>
    </tr>
    <tr id="Tr_SubMit" class="TR_BG_list" style="display:none">
      <td align="center" class="list_link" colspan="2" height="-5" style="height: 32px"><input type="submit" id="But_AddNew" name="But_AddNew" value="保 存"  class="form" runat="server" onserverclick="But_AddNewEdit_ServerClick" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <input type="submit" id="But_Clear" name="But_Clear" value="重 置" class="form" runat="server" />
        (注：(<font color=red size=2>*</font>)为必填)</td>
    </tr>
  </table>
</form>
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat=server /></td>
  </tr>
</table>
</body>
<script language=javascript>
//从General_manage.aspx.cs中传递参数kkey，判断是什么类型，进而显示相应框架
function load(Value)
{
       
    switch(Value)
    {
            case "0"://关键字(TAG)
				document.getElementById("Tr_Url").style.display="none";
				document.getElementById("Tr_Title").style.display="";
				document.getElementById("Tr_Email").style.display="none";
				document.getElementById("Tr_SubMit").style.display="";
				break;
			case "1"://来源
				document.getElementById("Tr_Url").style.display="";
				document.getElementById("Tr_Title").style.display="";
				document.getElementById("Tr_Email").style.display="none";
				document.getElementById("Tr_SubMit").style.display="";
				break;
			case "2"://作者
				document.getElementById("Tr_Url").style.display="none";
				document.getElementById("Tr_Title").style.display="";
				document.getElementById("Tr_Email").style.display="";
				document.getElementById("Tr_SubMit").style.display="";
				document.getElementById("Sel_Type").style.value="2";//控制作者
				break;	
			case "3"://内部连接
				document.getElementById("Tr_Url").style.display="";
				document.getElementById("Tr_Title").style.display="";
				document.getElementById("Tr_Email").style.display="none";
				document.getElementById("Tr_SubMit").style.display="";
				break;	
    }
}
//------------------------------------------------------------------
function SelectOpType(OpType)
{
	switch(parseInt(OpType))
		{
			case 9://默认全部不显示
				document.getElementById("Tr_Url").style.display="none";
				document.getElementById("Tr_Title").style.display="none";
				document.getElementById("Tr_Email").style.display="none";
				document.getElementById("Tr_SubMit").style.display="none";
				break;
			case 0://关键字(TAG)
				document.getElementById("Tr_Url").style.display="none";
				document.getElementById("Tr_Title").style.display="";
				document.getElementById("Tr_Email").style.display="none";
				document.getElementById("Tr_SubMit").style.display="";
				break;
			case 1://来源
				document.getElementById("Tr_Url").style.display="";
				document.getElementById("Tr_Title").style.display="";
				document.getElementById("Tr_Email").style.display="none";
				document.getElementById("Tr_SubMit").style.display="";
				break;
			case 2://作者
				document.getElementById("Tr_Url").style.display="none";
				document.getElementById("Tr_Title").style.display="";
				document.getElementById("Tr_Email").style.display="";
				document.getElementById("Tr_SubMit").style.display="";
				document.getElementById("Sel_Type").style.value="2";//控制作者
				break;	
			case 3://内部连接
				document.getElementById("Tr_Url").style.display="";
				document.getElementById("Tr_Title").style.display="";
				document.getElementById("Tr_Email").style.display="none";
				document.getElementById("Tr_SubMit").style.display="";
				break;	
		}
}
//-----------------------------------------------------------------
function check()
{
    if	(document.getElementById("Txt_Name").value=="")
		{
			alert("请添加标题！");
			document.getElementById("Txt_Name").focus();
			return false;
		}
		//----------检查电子邮件-------------------
	if (document.getElementById("Sel_Type").value=="2"&&document.getElementById("Txt_Email").value=="")
		{
			alert("请输入电子邮件地址！");
			document.getElementById("Txt_Email").focus();
			return false;	
		}
	if( document.getElementById("Sel_Type").value=="2"&&document.getElementById("Txt_Email").value.length<6 || document.getElementById("Sel_Type").value=="2"&&document.getElementById("Txt_Email").value.length>36 || document.getElementById("Sel_Type").value=="2"&&!validateEmail() ) 
		{
		      alert("\请您输入正确的邮箱地址 !");
		     document.getElementById("Txt_Email").focus();
		     return false;	
	    }
	return true
}
//检查电子邮件格式
function validateEmail()
{
	var re=/^[\w-]+(\.*[\w-]+)*@([0-9a-z]+[0-9a-z-]*[0-9a-z]+\.)+[a-z]{2,3}$/i;
	if(re.test(document.getElementById("Txt_Email").value))
		return true;
	else
		return false;
}
</script>
</html>
