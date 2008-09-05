<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_syslabel_bak" ResponseEncoding="utf-8" Codebehind="syslabel_bak.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
      <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >标签备份库</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="sysLabel_List.aspx" class="list_link">标签管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />标签备份库</div></td>
    </tr>
    </table>
     <asp:Repeater ID="DataList1" runat="server">
       <HeaderTemplate>
           <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table">
          <tr class="TR_BG">
            <td align="left" valign="middle" class="sys_topBg">编号</td>
            <td align="left" valign="middle" class="sys_topBg">标签名称</td>
            <td align="left" valign="middle" class="sys_topBg">创建日期</td>
            <td align="left" valign="middle" class="sys_topBg">操作</td>
          </tr>   
        </HeaderTemplate>
          <ItemTemplate>
            <tr class="TR_BG_list" onmouseover="javascript:overColor(this);" onmouseout="javascript:outColor(this);">
            <td align="left" valign="middle" height="20"><%#((DataRowView)Container.DataItem)[1]%></td>
            <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
            <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[3]%></td>
            <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[4]%></td>
            </tr>
          </ItemTemplate>
          <FooterTemplate>
          </table>
         </FooterTemplate>
    </asp:Repeater>
    <div style="width:98%;" align="right"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></div>
     <br />
     <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
      <tr>
        <td align="center"><%Response.Write(CopyRight);%></td>
      </tr>
    </table>
</form>
</body>
</html>
<script language="javascript" type="text/javascript">
function Rec(id)
{
    if(confirm('你确认恢复此标签吗?'))
    {
        self.location="?Op=Rec&LabelID="+id;
    }
}
function Update(type,id)
{
    self.location="syslabel_edit.aspx?LabelID="+id;
}
</script>
