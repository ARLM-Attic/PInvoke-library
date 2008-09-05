<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_survey_setClass" Codebehind="setClass.aspx.cs" %>
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
        <td height="18" style="width: 45%" colspan="2" ><div align="right"><a href="?type=add" class="topnavichar">新增分类</a> |
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
        <td width="7%" align="center" valign="middle" class="sys_topBg">编号</td>
        <td width="10%" align="center" valign="middle" class="sys_topBg">类别名称</td>
        <td width="9%" align="center" valign="middle" class="sys_topBg">描述</td>
        <td width="27%" align="center" valign="middle" class="sys_topBg">操作
          <input type="checkbox" id="vote_checkbox" value="-1" name="vote_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list">
        <td width="7%" align="center" valign="middle" height=20><%#((DataRowView)Container.DataItem)[0]%></td>
        <td width="10%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[1]%></td>
        <td width="9%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
        <td width="27%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["oPerate"]%> </td>
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
      <td class="list_link"> 分类查询:
        &nbsp;
        关键字:
        <asp:TextBox runat="server" ID="KeyWord" size="15"  CssClass="form"/>
        &nbsp;&nbsp;
        查询类型:
        <asp:DropDownList ID="DdlKwdType" runat="server"  CssClass="form">
          <asp:ListItem Value="choose" Text="请选择" />
          <asp:ListItem Value="number" Text="编号" />
          <asp:ListItem Value="classname" Text="类名" />
          <asp:ListItem Value="description" Text="描述" />
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Button runat="server" ID="BtnSearch" Text=" 查询 " CssClass="form" OnClick="BtnSearch_Click" />
        &nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_searchClass_0001',this)">帮助</span> </td>
    </tr>
  </table>
  <%
      }
       %>
  <%
         if(type == "add")
         {
             this.PageNavigator1.Visible = false;
             this.NoContent.Visible=false;
      %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="Addvote_Class">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">新增问卷调查分类信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 类别名称：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="ClassName" runat="server" Width="124px" CssClass="form"/>
        <span class=reshow>(*)</span> <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surverClass_0001',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 描述：</td>
      <td  align="left" class="list_link"><textarea ID="Description" runat="server" Width="124px" style="width: 266px; height: 99px" class="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surverClass_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Saveupload" value=" 提 交 " class="form" id="SaveClass" runat="server" onserverclick="SaveClass_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearupload" value=" 重 填 " class="form" id="ClearClass" runat="server" />
        </label></td>
    </tr>
  </table>
  <%
      }
     %>
  <%
         if(type == "edit")
         {
      %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="Editvote_Class">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">修改问卷调查分类信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 175px"> 类别名称：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="ClassNameEdit" runat="server" Width="124px" CssClass="form"/>
        <span class=reshow>(*)</span> <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surverClass_0001',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 175px"> 描述：</td>
      <td  align="left" class="list_link"><textarea ID="DescriptionE" runat="server" rows="5" Width="124px" style="width: 266px; height: 99px" class="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surverClass_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Savevote" value=" 提 交 " class="form" id="EditSave" runat="server" onserverclick="EditSave_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearvote" value=" 重 填 " class="form" id="EditClear" runat="server" />
        </label></td>
    </tr>
  </table>
  <%
      }
     %>
</form>
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat="server" /></td>
  </tr>
</table>
</body>
</html>
