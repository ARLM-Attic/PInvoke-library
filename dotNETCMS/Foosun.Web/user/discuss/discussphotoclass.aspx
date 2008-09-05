<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discuss_discussphotoclass" Debug="true" Codebehind="discussphotoclass.aspx.cs" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<div id="sc" runat="server"></div>
<div id="no" runat="server"></div>
 <table width="98%" align="center" id="TABLE1" onclick="return TABLE1_onclick()">
 <tr><td>
<div>
    <asp:Repeater ID="Repeater1" runat="server" >
    <HeaderTemplate>
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG">
    <td class="sys_topBg" align="center" width="15%">分类名称</td>
    <td class="sys_topBg" align="center" width="15%">创建日期</td>
    <td class="sys_topBg" align="center" width="15%">拥有人</td>
    <td class="sys_topBg" align="center" width="40%">操作&nbsp; &nbsp;<input type="checkbox" name="Checkbox1" onclick="javascript:selectAll(this.form,this.checked)" /></td>
    </tr>
    <div id="tnzlist" runat="server"></div>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td class="navi_link" align="center" width="15%"><%#((DataRowView)Container.DataItem)[2]%></td>
        <td class="navi_link" align="center" width="15%"><%#((DataRowView)Container.DataItem)[3]%></td>
        <td class="navi_link" align="center" width="15%"><%#((DataRowView)Container.DataItem)[5]%></td>
        <td class="navi_link" align="center" width="15%"><%#((DataRowView)Container.DataItem)[6]%></td>           
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
   if(confirm("删除此分类将删除此分类下的所有相册，你确定要删除吗?"))
   { 
        self.location="?Type=del&ID="+ID;
   }
}
function PDel()
{
    if(confirm("删除此分类将删除此分类下的所有相册，你确定要彻底删除吗?"))
    {
	    document.form1.action="?Type=PDel";
	    document.form1.submit();
	}
}
function TABLE1_onclick() {

}

</script>
</html>