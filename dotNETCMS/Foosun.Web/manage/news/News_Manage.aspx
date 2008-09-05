<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_News_Manage" Codebehind="News_Manage.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>

</head>
<body>
<form id="Form1" runat="server">
<div>
<table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >新闻管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="javascript:history.back();" target="sys_main" class="list_link">新闻管理</a></div></td>
        </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
    <tr class="TR_BG_list">
        <td class="list_link" width="30%" valign="top">
            <table border="0" width="100%" cellpadding="1" cellspacing="1">
                <tr>
                    <td>
                        <asp:DropDownList ID="DdlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlType_SelectedIndexChanged" Width="100%">
                            <asp:ListItem Value="0">指定新闻</asp:ListItem>
                            <asp:ListItem Value="1">指定栏目</asp:ListItem>
                        </asp:DropDownList>
                     </td>
                </tr>
                <tr height="231">
                    <td>
                      <asp:ListBox ID="LstOriginal" runat="server" Width="100%" Height="231" SelectionMode="Multiple"></asp:ListBox>
                    </td>
                </tr>
            </table>
        </td>
	    <td class="list_link" width="10%" align="center">
            <asp:Label ID="LblNarrate" runat="server"/><asp:Label ID="LblIDs" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LblNewsTable" runat="server" Visible="False"></asp:Label></td>
	    <td class="list_link" width="60%" valign="top">
	    <!--移动或是复制-->
            <asp:Panel ID="Panel1" runat="server">
                <table border="0" width="100%">
                    <tr>
                        <td>
                        </td>
                     </tr>
                     <tr>
                        <td>
                        </td>
                     </tr>
                     <tr>
                        <td><asp:ListBox ID="LstTarget" runat="server" Width="100%" Height="231"></asp:ListBox></td>
                     </tr>
                  </table>
            </asp:Panel>
            <!--批量设置属性-->
            <asp:Panel runat="server" ID="Panel2">
                <table border="0" width="100%">
                    <tr>
                        <td><asp:CheckBox
                            ID="CheckBox1" runat="server" /> 默认按照以下方式全部更新<span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_News_Manage_001',this)">帮助</span>

				        </td>
		            </tr>
                    <tr>
                        <td>属性：
                            <asp:CheckBox ID="NewsProperty_CommTF1" runat="server" />允许评论&nbsp;
                            <asp:CheckBox ID="NewsProperty_DiscussTF1" runat="server" />允许创建讨论组&nbsp;
                            <asp:CheckBox ID="NewsProperty_RECTF1" runat="server" />推荐&nbsp;
                            <asp:CheckBox ID="NewsProperty_MARTF1" runat="server" />滚动&nbsp;
                            <asp:CheckBox ID="NewsProperty_HOTTF1" runat="server" />热点&nbsp;
                            <asp:CheckBox ID="NewsProperty_FILTTF1" runat="server" />幻灯&nbsp;
                            <asp:CheckBox ID="NewsProperty_TTTF1"  runat="server" />头条&nbsp;
                            <asp:CheckBox ID="NewsProperty_ANNTF1" runat="server" />公告&nbsp;
                            <asp:CheckBox ID="NewsProperty_JCTF1" runat="server" />精彩&nbsp;
                            <asp:CheckBox ID="NewsProperty_WAPTF1" runat="server" />WAP&nbsp;
                            <asp:Button ID="Button9" runat="server" OnClick="pro_click" Text="设置属性" />
				        </td>
		            </tr>
		            <tr>
			            <td>模　板：
			                <asp:TextBox Width="40%" ID="Templet" runat="server" CssClass="form"/>
			                <img src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor:pointer;" onclick="selectFile('templet',document.Form1.Templet,250,400);document.Form1.Templet.focus();" />
                            <asp:Button ID="Button2" runat="server" OnClick="templet_click" Text="设置模板" /></td>
		            </tr>
		            <tr>
			            <td>权　重：
			              <asp:DropDownList ID="OrderIDDropDownList" runat="server" CssClass="form">
                            <asp:ListItem Value="" Text="选择权重" />
                            <asp:ListItem Value="10" Text="10" />
                            <asp:ListItem Value="9" Text="9" />
                            <asp:ListItem Value="8" Text="8" />
                            <asp:ListItem Value="7" Text="7" />
                            <asp:ListItem Value="6" Text="6" />
                            <asp:ListItem Value="5" Text="5" />
                            <asp:ListItem Value="4" Text="4" />
                            <asp:ListItem Value="3" Text="3" />
                            <asp:ListItem Value="2" Text="2" />
                            <asp:ListItem Value="1" Text="1" />
                            <asp:ListItem Value="0" Text="0"/>
                          </asp:DropDownList>
                            <asp:Button ID="Button3" runat="server" OnClick="order_click" Text="设置权重" /></td>
		            </tr>   	
		            <tr>
			            <td>评　论：
			                <asp:CheckBox ID="CommLinkTF" runat="server"/>
				            标题后显示&quot;评论&quot;字样
                            <asp:Button ID="Button4" runat="server" OnClick="comm_click" Text="设置评论" /></td>
		            </tr>
		            <tr>
			            <td>标　签：
			            <asp:TextBox ID="Tags" runat="server" MaxLength="100" Width="40%" CssClass="form"></asp:TextBox><img src="../../sysImages/folder/s.gif" alt="选择已有标签" border="0" style="cursor:pointer;" onclick="selectFile('Tag',document.Form1.Tags,220,480);document.Form1.Tags.focus();" />
                            <asp:Button ID="Button5" runat="server" OnClick="tag_click" Text="设置TAG标签" /></td>
		            </tr>
		            <tr>
			            <td>点击数：
			                <asp:TextBox ID="Click" Width="40%" runat="server" CssClass="form"/>
                            <asp:Button ID="Button6" runat="server" OnClick="click_click" Text="设置点击" /></td>
		            </tr>
		            <tr>
			            <td>
                            来　源：
                            <asp:TextBox ID="Souce" runat="server"  Width="40%" MaxLength="100" CssClass="form"></asp:TextBox><img src="../../sysImages/folder/s.gif" alt="选择已有来源" border="0" style="cursor:pointer;" onclick="selectFile('Souce',document.Form1.Souce,220,450);document.Form1.Souce.focus();" />
                            <asp:Button ID="Button7" runat="server" OnClick="source_click" Text="设置来源" /></td>
		            </tr>
		            <tr>
			            <td class="hback"> 扩展名：
			        <asp:DropDownList ID="FileEXName" runat="server" Height="21px" Width="92px" CssClass="form">
                    <asp:ListItem Value="">选择扩展名</asp:ListItem>
                    <asp:ListItem>.html</asp:ListItem>
                    <asp:ListItem>.htm</asp:ListItem>
                    <asp:ListItem>.shtml</asp:ListItem>
                    <asp:ListItem>.shtm</asp:ListItem>
                    <asp:ListItem>.aspx</asp:ListItem>
                </asp:DropDownList>
                            <asp:Button ID="Button8" runat="server" OnClick="exname_click" Text="设置扩展名" /> 说明：如果为标题新闻，此设置不起作用</td>
		            </tr>
		            <tr>
			            <td></td>
		            </tr>
               </table>
            </asp:Panel>
	    </td>
	</tr>
	<tr class="TR_BG_list">
	    <td colspan="3" align="center" class="list_link">
	    <asp:Button runat="server" ID="BtnOK" OnClick="BtnOK_Click" CssClass="form"/>
            <asp:Button ID="Button1" runat="server" Text=" 重置 " CssClass="form" /></td>
	</tr>
  </table>
  <br />
  <br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td class="list_link" align="center"><%Response.Write(CopyRight); %></td>
  </tr>
</table>
</div>
</form>
</body>
</html>
