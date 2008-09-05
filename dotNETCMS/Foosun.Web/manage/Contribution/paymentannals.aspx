<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="manage_Contribution_paymentannals" Codebehind="paymentannals.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
</head>
<body style="background-color: #ffffff">
<form id="form1" name="form1" method="post" action="" runat="server">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >
              投稿管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Constr_List.aspx" class="topnavichar">稿件管理</a></div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td width="46%" class="navi_link">&nbsp; &nbsp; &nbsp;<a href="Constr_List.aspx" class="topnavichar">稿件管理</a>&nbsp; &nbsp;<a href="Constr_Stat.aspx" class="topnavichar">稿件统计</a>&nbsp; &nbsp;<a href="paymentannals.aspx" class="topnavichar">支付历史</a>&nbsp; &nbsp;<a href="Constr_SetParam.aspx" class="topnavichar">稿酬设定</a>&nbsp; &nbsp;<a href="Constr_chicklist.aspx" class="topnavichar">所有通过审核稿件</a>&nbsp; &nbsp;<span id="pdels" runat="server"></span></td>
  </tr>
</table>
<div id="no" runat="server"></div>
  <asp:Repeater ID="DataList1" runat="server" >
    <HeaderTemplate>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
    <tr class="TR_BG">
    <td class="sys_topBg" align="center" width="20%">会员名</td>
    <td class="sys_topBg" align="center" width="20%">支付数目</td>
    <td class="sys_topBg" align="center" width="30%">支付时间</td>
    <td class="sys_topBg" align="center" width="15%">支付管理员</td>
    <td class="sys_topBg" align="center" width="15%">操作<input type="checkbox" name="Checkbox1" onclick="javascript:selectAll(this.form,this.checked)" /></td>
    </tr>
    <div id="tnzlist" runat="server"  onmouseover="overColor(this)" onmouseout="outColor(this)"></div>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="TR_BG_list">
        <td align="center" width="20%"><%#((DataRowView)Container.DataItem)["gusername"]%></td>
        <td align="center" width="20%"><%#((DataRowView)Container.DataItem)["moneys"]%></td>
        <td align="center" width="30%"><%#((DataRowView)Container.DataItem)["payTimes"]%></td>
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)["payAdmin"]%></td>
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)["handle"]%></td>
        </tr>
     </ItemTemplate>
     <FooterTemplate>
     </table>
     </FooterTemplate>
    </asp:Repeater>
<table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
<tr><td align="right" style="width: 928px"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></td></tr>
</table>
<br />
<br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %>   </div></td>
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






