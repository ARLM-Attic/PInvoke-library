<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_survey_setItem" Codebehind="setItem.aspx.cs" %>
<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="form1" runat="server">
  <iframe width="260" height="165" id="colorPalette" src="../../configuration/system/selcolor.htm" style="visibility:hidden; position: absolute;border:1px gray solid; left: 31px; top: 140px;" frameborder="0" scrolling="no" ></iframe>
  <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">调查管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />调查管理</div></td>
    </tr>
  </table>
        <div>
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
      <tr class="menulist">
        <td height="18" style="width: 45%" colspan="2" style="PADDING-LEFT: 14px"><div align="left"><label id="param_id" runat="server" /><a href="setClass.aspx" class="menulist">投票分类设置</a>&nbsp;┊&nbsp;<a href="setTitle.aspx" class="menulist">投票主题设置</a>&nbsp;┊&nbsp;<a href="setItem.aspx" class="menulist">投票选项设置</a>&nbsp;┊&nbsp;<a href="setSteps.aspx" class="menulist">多步投票管理</a>&nbsp;┊&nbsp;<a href="ManageVote.aspx" class="menulist">投票情况管理</a> </div></td>
      </tr>
    </table>
  </div>
  <div id="NoContent" runat="server"></div>
  <%
         string type = Request.QueryString["type"];
         if(type !="add"&&type!="edit")
         {
      %>
      <div>
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1">
      <tr>
        <td height="18" style="width: 45%" colspan="2" ><div align="right"><a href="?type=add" class="topnavichar">新增选项</a> |
            <asp:LinkButton ID="DelP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所选信息吗?')){return true;}return false;}" OnClick="DelP_Click">批量删除</asp:LinkButton>
            |
            <asp:LinkButton ID="DelAll" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除全部信息吗?')){return true;}return false;}" OnClick="DelAll_Click">删除全部</asp:LinkButton></div></td>
      </tr>
    </table>
  </div>
  <asp:Repeater ID="DataList1" runat="server">
    <HeaderTemplate>
      <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
      <tr class="TR_BG">
        <td align="center" valign="middle" class="sys_topBg">选项描述</td>
        <td align="center" valign="middle" class="sys_topBg">所属主题</td>
        <td align="center" valign="middle" class="sys_topBg">选项模式</td>
        <td align="center" valign="middle" class="sys_topBg">图片位置</td>
        <td align="center" valign="middle" class="sys_topBg">显示颜色</td>
        <td align="center" valign="middle" class="sys_topBg">票数</td>
        <td align="center" valign="middle" class="sys_topBg">操作
          <input type="checkbox" id="vote_checkbox" value="-1" name="vote_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list">
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["title"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["ItemModel"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[5]%></td>
        <td align="center" valign="middle" style="background-color:#<%#((DataRowView)Container.DataItem)[6]%>"><%#((DataRowView)Container.DataItem)[6]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[7]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["oPerate"]%> </td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      </table>
    </FooterTemplate>
  </asp:Repeater>
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
  </div>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
    <tr class="TR_BG_list">
      <td class="list_link"> 选项查询:
        &nbsp;
        关键字:
        <asp:TextBox runat="server" ID="KeyWord" size="15" CssClass="form"/>
        &nbsp;&nbsp;
        查询类型:
        <asp:DropDownList ID="DdlKwdType" runat="server"  CssClass="form">
          <asp:ListItem Value="choose" Text="请选择" />
          <asp:ListItem Value="title" Text="所属主题"/>
          <asp:ListItem Value="ItemNamee" Text="选项描述" />
          <asp:ListItem Value="ItemValuee" Text="项目符号" />
          <asp:ListItem Value="PicSrcc" Text="图片位置" />
          <asp:ListItem Value="DisColorr" Text="显示颜色" />
          <asp:ListItem Value="VoteCountt" Text="当前票数" />
          <asp:ListItem Value="ItemDetaill" Text="详细说明" />
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Button runat="server" ID="BtnSearch" Text=" 查询 " CssClass="form" OnClick="BtnSearch_Click"/>
        &nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_searchItem_0001',this)">帮助</span> </td>
    </tr>
  </table>
  <%
      }
       %>
  <%
            if(type=="add")
            {
        %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="Addvote_Item">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">新增问卷调查选项信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 所属投票：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="vote_CTName" runat="server"  CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0001',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 选项描述：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="ItemName" runat="server" Width="124px"  CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 项目符号：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="ItemValue" runat="server"  CssClass="form">
          <asp:ListItem Value="0" Selected="True">1-9</asp:ListItem>
          <asp:ListItem Value="1">a-z</asp:ListItem>
          <asp:ListItem Value="2">A-Z</asp:ListItem>
          <asp:ListItem Value="3">.</asp:ListItem>
          <asp:ListItem Value="4">★</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0003',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 选项模式：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="ItemMode" runat="server"  CssClass="form">
          <asp:ListItem Value="1">文字描述模式</asp:ListItem>
          <asp:ListItem Value="2">自主填写模式</asp:ListItem>
          <asp:ListItem Value="3">图片模式</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0004',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 图片位置：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="PicSrc" runat="server" Width="124px"  CssClass="form"/>
        &nbsp;
        <input type="button" name="PicSrcClick" value=" 选择图片 " class="form" id="PicSrcClick" onclick="selectFile('pic',document.form1.PicSrc,280,500);document.form1.PicSrc.focus();"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0005',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right" class="list_link" style="width: 171px"> 显示颜色：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="DisColor" runat="server"  CssClass="form"/>
        <img src="../../sysImages/FileIcon/Rect.gif" alt="-" name="MarkFontColor_Show" width="18" height="17" border=0 align="absmiddle" id="MarkFontColor_Show" style="cursor:pointer;background-color:#<%= DisColor.Text%>;"title="选取颜色" onClick="GetColor(this,'DisColor');"><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0006',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 当前票数：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="VoteCount" runat="server" Width="124px"  CssClass="form" Text="0"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0007',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 选项详细说明：</td>
      <td  align="left" class="list_link"><textarea id="ItemDetail" runat="server" style="width: 290px; height: 103px" class="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0008',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Saveitem" value=" 提 交 " class="form" id="Saveitem" runat="server" onserverclick="Saveitem_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearitem" value=" 重 填 " class="form" id="Clearitem" runat="server" />
        </label></td>
    </tr>
  </table>
  <%
            }
         %>
  <%
            if(type=="edit")
            {
        %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="EditItem">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">修改问卷调查选项信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 所属投票：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="classnameedit" runat="server"  CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0001',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 选项描述：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="itemnameedit" runat="server" Width="124px"  CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 项目符号：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="valueedit" runat="server"  CssClass="form">
          <asp:ListItem Value="0" Selected="True">1-9</asp:ListItem>
          <asp:ListItem Value="1">a-z</asp:ListItem>
          <asp:ListItem Value="2">A-Z</asp:ListItem>
          <asp:ListItem Value="3">.</asp:ListItem>
          <asp:ListItem Value="4">★</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0003',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 选项模式：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="itemmodele" runat="server"  CssClass="form">
          <asp:ListItem Value="1">文字描述模式</asp:ListItem>
          <asp:ListItem Value="2">自主填写模式</asp:ListItem>
          <asp:ListItem Value="3">图片模式</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0004',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 图片位置：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="picsurl" runat="server" Width="124px"  CssClass="form"/>
        &nbsp;
        <input type="button" name="PicSrce" value=" 选择图片 " class="form" id="PicSrce"  onclick="selectFile('pic',document.form1.picsurl,280,500);document.form1.picsurl.focus();"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0005',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right" class="list_link" style="width: 171px"> 显示颜色：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="discoloredit" runat="server"  CssClass="form"/>
        <img src="../../sysImages/FileIcon/Rect.gif" alt="-" name="MarkFontColor_Show" width="18" height="17" border=0 align="absmiddle" id="discolore" style="cursor:pointer;background-color:#<%= DisColor.Text%>;"title="选取颜色" onClick="GetColor(this,'discoloredit');"><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0006',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 当前票数：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="pointqe" runat="server" Width="124px"  CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0007',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 选项详细说明：</td>
      <td  align="left" class="list_link"><textarea id="discriptionitem" runat="server" style="width: 290px; height: 103px"  class="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveyitem_0008',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Saveitem" value=" 提 交 " class="form" id="Editclick" runat="server" onserverclick="Editclick_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearitem" value=" 重 填 " class="form" id="Editclear" runat="server" />
        </label></td>
    </tr>
  </table>
  <%
            }
         %>
</form>
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat=server /></td>
  </tr>
</table>
</body>
</html>
