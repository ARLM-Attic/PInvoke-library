<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_info_url" Codebehind="url.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
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
              <td width="57%" class="sysmain_navi" style="padding-left:14px;">网址收藏夹</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="url.aspx" class="list_link" target="sys_main">网址收藏夹</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />列表</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><a href="url.aspx" class="topnavichar">网址列表</a> ┊  <a href="url_add.aspx" class="topnavichar">添加网址</a> ┊  <a href="url_class.aspx" class="topnavichar">创建分类</a> ┊  <a href="javascript:void(0);" onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('<%Response.Write(fURL); %>');" style="cursor:pointer;" class="topnavichar"><span style="color:Red">把网址设为首页</span></a>┊  <a href="../url.aspx?uid=<%Response.Write(myUID); %>" target="_blank" class="topnavichar">浏览我的网址导航</a> </td>
      </tr>
      </table>
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
      <tr>
        <td>
        分类：<span id="URLClassList" runat="server" />
        </td>
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
            <td class="sys_topBg" style="width:40%">网站</td>
            <td class="sys_topBg">日期</td>
            <td class="sys_topBg">操作</td>
            </tr>
        </HeaderTemplate>
          <ItemTemplate>
            <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
            <td align="left"><%#((DataRowView)Container.DataItem)["URLNames"]%></td>
            <td align="left"><%#((DataRowView)Container.DataItem)["CreatTime"]%></td>
            <td align="left"><%#((DataRowView)Container.DataItem)["op"]%></td>
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
<div style="width:98px;padding-left:10px;"></div>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><%Response.Write(CopyRight); %> </td>
   </tr>
</table>
</form>
</body>
</html>