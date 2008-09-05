<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discuss_discussactijoin_list" Codebehind="discussactijoin_list.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
</head>
<body style="background-color: #ffffff">
<form id="form1" name="form1" method="post" action="" runat="server">
<div id="sc" runat="server"></div>
<div id="no" runat="server"></div>
   <asp:Repeater ID="Repeater1" runat="server" >
    <HeaderTemplate>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG">
    <td class="sys_topBg" align="center" width="15%">活动名称</td>
    <td class="sys_topBg" align="center" width="15%">建立人</td>
    <td class="sys_topBg" align="center" width="15%">创建时间</td>
    <td class="sys_topBg" align="center" width="15%">参加人数</td>
    <td class="sys_topBg" align="center" width="40%">操作&nbsp; &nbsp;<input type="checkbox" name="Checkbox1" onclick="javascript:selectAll(this.form,this.checked)" /></td>
    </tr>
    <div id="tnzlist" runat="server"></div>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td align="left" width="15%"><%#((DataRowView)Container.DataItem)[2]%></td>
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)[3]%></td>
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)[4]%></td>
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)[6]%></td>
        <td align="center" width="40%"><%#((DataRowView)Container.DataItem)[7]%></td>            
        </tr>
     </ItemTemplate>
     <FooterTemplate>
     </table>
     </FooterTemplate>
    </asp:Repeater>
<table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
<tr><td align="right" style="width: 928px"><uc1:PageNavigator ID="PageNavigator2" runat="server" /></td></tr>
</table>

<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
<script language="javascript" type="text/javascript">
function del(ID)
{
   if(confirm("你确定要删除吗?"))
   { 
        self.location="?Type=del&ID="+ID;
   }
}
function PDel()
{
    if(confirm("你确定要彻底删除吗?"))
    {
	    document.form1.action="?Type=PDel";
	    document.form1.submit();
	}
}
</script>
</html>