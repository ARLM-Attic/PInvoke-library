<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_info_url_add" Codebehind="url_add.aspx.cs" %>
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
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="url.aspx" class="list_link" target="sys_main">网址收藏夹</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加/修改</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><a href="url.aspx" class="topnavichar">网址列表</a> ┊  <a href="url_add.aspx" class="topnavichar">添加网址</a> ┊  <a href="url_class.aspx" class="topnavichar">创建分类</a></td>
      </tr>
      </table>
      
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
      <tr class="TR_BG_list">
        <td style="width:10%;">
            网站名称*
        </td>
        <td >
            <asp:TextBox ID="URLName" runat="server" Width="200px"></asp:TextBox>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:10%;">
            网站地址*
        </td>
        <td >
            <asp:TextBox ID="URL" runat="server" Width="200px"></asp:TextBox>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:10%;">
            显示颜色
        </td>
        <td >
            <asp:DropDownList ID="URLColor" runat="server">
            <asp:ListItem Value="">普通</asp:ListItem>
            <asp:ListItem Value="#FF0000">红色</asp:ListItem>
            <asp:ListItem Value="#0033CC">蓝色</asp:ListItem>
            <asp:ListItem Value="#FF0099">紫色</asp:ListItem>
            <asp:ListItem Value="#339900">绿色</asp:ListItem>
            <asp:ListItem Value="#FF6600">橙色</asp:ListItem>
            <asp:ListItem Value="#999999">银灰色</asp:ListItem>
            </asp:DropDownList> 
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:10%;">
            分类*
        </td>
        <td >
            <asp:DropDownList ID="ClassID" runat="server">
            </asp:DropDownList> 
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:10%;">
            备注(最多200字符)
        </td>
        <td >
            <asp:TextBox ID="Content" runat="server" Width="300px" Height="50" TextMode="MultiLine"></asp:TextBox>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:10%;">
        </td>
        <td >
            <asp:Button ID="Button1" runat="server" Text="保存网址" OnClick="Button1_Click" />
        </td>
      </tr>
      </table>
    </form>
</body>
</html>
