<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_user_icard" Codebehind="icard.aspx.cs" %>
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
              <td width="57%"  class="sysmain_navi" style="padding-left:14px;" >点卡管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="icard.aspx" class="list_link" target="sys_main">点卡管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />点卡列表</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><a href="icard.aspx" class="topnavichar">全部点卡</a>┋<a href="icard.aspx?islock=1" class="topnavichar">已锁定</a>┋<a href="icard.aspx?islock=0" class="topnavichar">未锁定</a>┋<a href="icard.aspx?isuse=1" class="topnavichar">已使用</a>┋<a href="icard.aspx?isuse=0" class="topnavichar">未使用</a>┋<a href="icard.aspx?isbuy=1" class="topnavichar">已购买</a>┋<a href="icard.aspx?isbuy=0" class="topnavichar">未购买</a>┋<a href="icard.aspx?timeout=0" class="topnavichar">未过期</a>┋<a href="icard.aspx?timeout=1" class="topnavichar">已过期</a>┋<a href="icard_add.aspx" class="topnavichar" >添加点卡</a>┋<a href="icard_adds.aspx" class="topnavichar" >批量生成点卡</a><span id="channelList" runat="server" /></td>
      </tr>
      </table>
   <asp:Repeater ID="cardlists" runat="server" OnItemCommand="DataList1_ItemCommand">
       <HeaderTemplate>
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
            <tr class="TR_BG">
            <td class="sys_topBg" style="width:120px">卡号</td>
            <td class="sys_topBg" style="width:140px">密码</td>
            <td class="sys_topBg" style="width:200px">金额/点数/过期日期</td>
            <td class="sys_topBg" align="center">购买状态</td>
            <td class="sys_topBg" align="center">使用状态</td>
            <td class="sys_topBg" align="center">状态</td>
            <td class="sys_topBg" align="center">操作<input type="checkbox" value="-222" onclick="selectAll(this.form,this.checked);" /></td>
            </tr>
        </HeaderTemplate>
          <ItemTemplate>
            <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
            <td title=""><%#((DataRowView)Container.DataItem)["UserNums"]%><%#((DataRowView)Container.DataItem)["CardNumber"]%></td>
            <td title=""><%#((DataRowView)Container.DataItem)["CardPassWords"]%></td>
            <td valign="middle" ><%#String.Format("{0:N}", ((DataRowView)Container.DataItem)["Money"])%> / <%#((DataRowView)Container.DataItem)["Point"]%> / <%#((DataRowView)Container.DataItem)["isTimeOut"]%></td>
            <td align="center" ><%#((DataRowView)Container.DataItem)["isBuyStr"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["isuseStr"]%></td>
            <td align="center"><%#((DataRowView)Container.DataItem)["islockStr"]%></td>
            <td align="center" ><%#((DataRowView)Container.DataItem)["op"]%></td>
            </tr>
          </ItemTemplate>
          <FooterTemplate>
          </table>
         </FooterTemplate>
    </asp:Repeater>       
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
       <tr>
         <td align="left">
             <uc1:PageNavigator ID="PageNavigator1" runat="server" />
             卡号：<asp:TextBox ID="cardnumber" runat="server" Width="80px"></asp:TextBox>  
             密码：<asp:TextBox ID="cardpassword" runat="server" Width="80px"></asp:TextBox>
             <asp:Button ID="search_button" runat="server" Text="搜索" OnClick="search_button_Click" />
             </td>
       </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="2" style="height: 20px">
       <tr>
         <td align="left">
             <asp:Button ID="Button1" runat="server" onclick="islock" Text="锁定" OnClientClick="{if(confirm('确定要锁定吗？')){return true;}return false;}"  />
             <asp:Button ID="Button2" runat="server" onclick="unlock" Text="解锁" OnClientClick="{if(confirm('确定要解锁吗？')){return true;}return false;}"  />
             <asp:Button ID="Button3" runat="server" onclick="delmul" Text="删除" OnClientClick="{if(confirm('确定要删除吗？')){return true;}return false;}" />
             <asp:Button ID="Button10" runat="server" onclick="delAll" Text="删除所有" OnClientClick="{if(confirm('确认清除所有点卡吗？\n此操作不可逆!')){return true;}return false;}" />
             <asp:Button ID="Button4" runat="server" onclick="timeout" Text="设置为过期" OnClientClick="{if(confirm('确定设置为过期吗？\n此操作不可逆!')){return true;}return false;}" />
             <asp:Button ID="Button6" runat="server" onclick="isuse" Text="设置为已使用" OnClientClick="{if(confirm('确定要设置为已使用吗？')){return true;}return false;}" />
             <asp:Button ID="Button7" runat="server" onclick="unisuse" Text="设置为未使用" OnClientClick="{if(confirm('确定要设置为未使用吗？')){return true;}return false;}" />
             <asp:Button ID="Button8" runat="server" onclick="isbuy" Text="设置为已购买" OnClientClick="{if(confirm('确定要设置为已购买吗？')){return true;}return false;}" />
             <asp:Button ID="Button9" runat="server" onclick="unisbuy" Text="设置为未购买" OnClientClick="{if(confirm('确定要设置为未购买吗？')){return true;}return false;}" />
        </td>
       </tr>
    </table>
    </form> <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><label id="copyright" runat="server" /></td>
       </tr>
    </table>

</body>
</html>
 <script type="text/javascript" language="javascript">
    function getchanelInfo(obj)
    {
       var SiteID=obj.value;
       window.location.href="icard.aspx?SiteID="+SiteID+"";
    }
 
   function getSearch(cardvalue,passvalue,typesvalue)
   {
       window.location.href="icard.aspx?cardNumber="+cardvalue+"&cardPassword="+passvalue+"&types="+typesvalue+"";
   }
 </script>