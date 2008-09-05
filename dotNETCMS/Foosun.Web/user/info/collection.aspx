<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_info_collection"  ResponseEncoding="utf-8" Codebehind="collection.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>收藏</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body><form id="form1" runat="server">
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
              <td height="1" colspan="2"></td>
            </tr>
            <tr>
              <td width="57%" class="sysmain_navi" style="padding-left:14px;">收藏夹管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="#" class="list_link" target="sys_main">收藏夹管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />收藏夹列表</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><div id="sc" runat="server"></div></td>
      </tr>
      </table>
      <div id="no" runat="server"></div>
 <table width="98%" align="center">
 <tr><td>
<div>   
    <asp:Repeater ID="DataList1" runat="server">
       <HeaderTemplate>
        <table width="100%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
            <tr class="TR_BG">
            <td class="sys_topBg" align="center">标题</td>
            <td class="sys_topBg" align="center">收藏日期</td>
            <td class="sys_topBg" align="center">操作<input type="checkbox" name="Checkbox2" onclick="javascript:selectAll(this.form,this.checked)" /></td>
            </tr>
        </HeaderTemplate>
          <ItemTemplate>
            <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
            <td align="center"><%#((DataRowView)Container.DataItem)["titleUrl"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["CreatTime"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["Operation"]%></td>
            </tr>
          </ItemTemplate>
          <FooterTemplate>
          </table>
         </FooterTemplate>
    </asp:Repeater> 
</div>
</td></tr>
<tr><td align="right" style="width: 928px">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
</td></tr>
</table>  
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><%Response.Write(CopyRight); %> </td>
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
function API(ID)
{
    document.form1.action="?APIID="+ID;
    document.form1.submit();
}
</script>
</html>