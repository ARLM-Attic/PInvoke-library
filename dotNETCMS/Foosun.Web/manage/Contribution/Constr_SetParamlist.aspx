<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="manage_Contribution_Constr_SetParamlist" Codebehind="Constr_SetParamlist.aspx.cs" %>
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
              稿酬设置</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Constr_List.aspx" target="sys_main" class="list_link">稿件管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />稿酬设置</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td width="46%" class="navi_link">&nbsp; &nbsp; &nbsp;<a href="Constr_List.aspx" class="topnavichar">稿件管理</a>&nbsp; &nbsp;<a href="#" class="topnavichar">所有设置</a>&nbsp; &nbsp;<a href="Constr_SetParam.aspx" class="topnavichar">添加设置</a>&nbsp; &nbsp;<span id="pdels" runat="server"></span></td>
  </tr>
</table>
<div id="no" runat="server"></div>

 <table width="98%" align="center">
 <tr><td>
<div>
    <asp:Repeater ID="DataList1" runat="server" >
    <HeaderTemplate>
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG">
    <td class="sys_topBg" align="center" width="17.5%">等级名称</td>
    <td class="sys_topBg" align="center" width="17.5%">稿酬</td>
    <td class="sys_topBg" align="center" width="17.5%">积分数</td>
    <td class="sys_topBg" align="center" width="17.5%">金币数</td>
    <td class="sys_topBg" align="center" width="17.5%">单位</td>
    <td class="sys_topBg" align="center" width="17.5%">操作&nbsp; &nbsp;<input type="checkbox" name="Checkbox1" onclick="javascript:selectAll(this.form,this.checked)" /></td>
    </tr>
    <div id="tnzlist" runat="server"  onmouseover="overColor(this)" onmouseout="outColor(this)"></div>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="TR_BG_list">
        <td align="center" width="17.5%"><%#((DataRowView)Container.DataItem)[2]%></td>
        <td align="center" width="17.5%"><%#((DataRowView)Container.DataItem)[7]%></td>
        <td align="center" width="17.5%"><%#((DataRowView)Container.DataItem)[4]%></td>
        <td align="center" width="17.5%"><%#((DataRowView)Container.DataItem)[3]%></td>
        <td align="center" width="17.5%"><%#((DataRowView)Container.DataItem)[6]%></td>
        <td align="center" width="17.5%"><%#((DataRowView)Container.DataItem)[8]%></td>            
        </tr>
     </ItemTemplate>
     <FooterTemplate>
     </table>
     </FooterTemplate>
    </asp:Repeater>
</div>
</td></tr>
<tr><td align="right" style="width: 928px"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></td></tr>
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






