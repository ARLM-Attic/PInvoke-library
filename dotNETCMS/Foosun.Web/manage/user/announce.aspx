<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_user_announce" Codebehind="announce.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
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
              <td width="57%" class="sysmain_navi" style="padding-left:14px;">公告管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="announce.aspx" class="list_link" target="sys_main">公告管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />公告列表</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><a href="announce.aspx" class="topnavichar">公告列表</a>┋<a href="announce_add.aspx" class="topnavichar" >添加公告</a><span id="channelList" runat="server" /></td>
      </tr>
      </table>

    <asp:Repeater ID="announcelists" runat="server" OnItemCommand="DataList1_ItemCommand">
       <HeaderTemplate>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
            <tr class="TR_BG">
            <td class="sys_topBg">标题</td>
            <td class="sys_topBg" style="width:120px">发布日期</td>
            <td class="sys_topBg" style="width:40px">状态</td>
            <td class="sys_topBg" style="width:55px">操作<input type="checkbox" value="-222" onclick="selectAll(this.form,this.checked);" /></td>
            </tr>
        </HeaderTemplate>
          <ItemTemplate>
            <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
            <td align="left" valign="middle" title="<%#((DataRowView)Container.DataItem)["Content"]%>"><%#((DataRowView)Container.DataItem)["title"]%></td>
            <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["creatTime"]%></td>
            <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["islocks"]%></td>
            <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["op"]%></td>
            </tr>
          </ItemTemplate>
          <FooterTemplate>
          </table>
         </FooterTemplate>
    </asp:Repeater> 

    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
       <tr>
         <td align="left">
             <uc1:PageNavigator ID="PageNavigator1" runat="server" /></td>
       </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="height: 20px">
       <tr>
         <td align="left">
             <asp:Button ID="Button1" CssClass="form" runat="server" onclick="islock" Text="批量锁定" OnClientClick="{if(confirm('确定要锁定吗？')){return true;}return false;}"  />&nbsp;
             <asp:Button  ID="Button2" CssClass="form" runat="server" onclick="unlock" Text="批量解锁" OnClientClick="{if(confirm('确定要解锁吗？')){return true;}return false;}"  />&nbsp;
             <asp:Button ID="Button3" CssClass="form"  runat="server" onclick="delmul" Text="批量删除" OnClientClick="{if(confirm('确定要删除吗？')){return true;}return false;}" />&nbsp;
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
    function getchanelInfo(obj)
    {
       var SiteID=obj.value;
       window.location.href="announce.aspx?SiteID="+SiteID+"";
    }
 
 </script>