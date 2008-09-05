<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_user_announce_add" Codebehind="announce_add.aspx.cs" %>

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
              <td style="height:1px;" colspan="2"></td>
            </tr>
            <tr>
              <td style="width:57%;padding-left:14px;" class="sysmain_navi">公告管理</td>
              <td style="width:43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="announce.aspx" class="list_link" target="sys_main">公告管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />增加公告</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><a href="announce.aspx" class="topnavichar">公告列表</a>┋<a href="announce_add.aspx" class="topnavichar" ><font color=red>添加公告</font></a><span id="channelList" runat="server" /></td>
      </tr>
      </table>


     <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
       
       
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">标题</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="title" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_announce_add_0001',this)">帮助</span></td>
          </tr>
          
         <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">内容</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="content" runat="server"  Width="300px" TextMode="MultiLine" Rows="6"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_announce_add_0002',this)">帮助</span></td>
          </tr>
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">点数/条件</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="getPoint" runat="server" Text="0|0|0" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_announce_add_0003',this)">帮助</span></td>
          </tr>
                          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">会员组</div></td> 
          <td class="list_link"><label id="GroupList" runat="server" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_announce_add_0004',this)">帮助</span></td>
          </tr>
                          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px; height: 28px;">&nbsp;</td>
          <td class="list_link" style="height: 28px">&nbsp;<asp:Button ID="buttons" runat="server" CssClass="form" Text=" 确 定 "  OnClick="sumbitsave" />
            <input name="reset" type="reset" value=" 重 置 "  class="form">
          </td>
        </tr>

</table>
    <br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
 </table>    

    
    </form>
</body>
</html>
