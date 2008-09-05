<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_survey_setSteps" Codebehind="setSteps.aspx.cs" %>
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
     <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" height="30">调查管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />调查管理</div></td>
    </tr>
  </table>
          <div>
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
      <tr class="menulist">
        <td height="18" style="width: 45%;PADDING-LEFT: 14px" colspan="2"><div align="left"><label id="param_id" runat="server" /><a href="setClass.aspx" class="menulist">投票分类设置</a>&nbsp;┊&nbsp;<a href="setTitle.aspx" class="menulist">投票主题设置</a>&nbsp;┊&nbsp;<a href="setItem.aspx" class="menulist">投票选项设置</a>&nbsp;┊&nbsp;<a href="setSteps.aspx" class="menulist">多步投票管理</a>&nbsp;┊&nbsp;<a href="ManageVote.aspx" class="menulist">投票情况管理</a> </div></td>
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
        <td height="18" style="width: 45%" colspan="2" ><div align="right"><a href="?type=add" class="topnavichar">新增多步投票</a> |
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
        <td width="5%" class="sys_topBg">编号</td>
        <td width="27%" class="sys_topBg">调查主题</td>
        <td width="7%" class="sys_topBg">顺序号</td>
        <td width="12%" class="sys_topBg">调用主题</td>
        <td width="12%" class="sys_topBg">操作
          <input type="checkbox" id="vote_checkbox" value="-1" name="vote_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr  class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td width="5%"><%#((DataRowView)Container.DataItem)[0]%></td>
        <td width="27%"><%#((DataRowView)Container.DataItem)["titlesearch"]%></td>
        <td width="12%"><%#((DataRowView)Container.DataItem)["num"]%></td>
        <td width="7%"><%#((DataRowView)Container.DataItem)["titleuse"]%></td>
        <td width="12%"><%#((DataRowView)Container.DataItem)["oPerate"]%></td>
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
      <td class="list_link"> 多步投票查询:
        &nbsp;
        关键字:
        <asp:TextBox runat="server" ID="KeyWord" size="15"  CssClass="form"/>
        &nbsp;&nbsp;
        查询类型:
        <asp:DropDownList ID="DdlKwdType" runat="server"  CssClass="form">
          <asp:ListItem Value="choose" Text="请选择" />
          <asp:ListItem Value="nums" Text="编号"/>
          <asp:ListItem Value="titles" Text="调查主题" />
          <asp:ListItem Value="nunber" Text="顺序号" />
          <asp:ListItem Value="titleu" Text="调用主题" />
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Button runat="server" ID="BtnSearch" Text=" 查询 " CssClass="form" OnClick="BtnSearch_Click"/>
        &nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_searchSteps_0001',this)">帮助</span> </td>
    </tr>
  </table>
  <%
      }
       %>
  <%
            if(type=="add")
            {
        %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="Addvote_Steps">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">新增多步投票信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 调查主题：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="vote_CNameSe" runat="server" CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_surveysteps_0001',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 顺序号：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="StepsN" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_surveysteps_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 调用主题：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="vote_CNameUse" runat="server" CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_surveysteps_0003',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Savesteps" value=" 提 交 " class="form" id="Savesteps" runat="server" onserverclick="Savesteps_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearsteps" value=" 重 填 " class="form" id="Clearsteps" runat="server" />
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
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="voteEdit">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">修改多步投票信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 调查主题：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="votecnameEditse" runat="server" CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_surveysteps_0001',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 顺序号：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="NumEdit" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_surveysteps_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 调用主题：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="votecnameEditue" runat="server" CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_surveysteps_0003',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Savesteps" value=" 提 交 " class="form" id="SavestepsEdit" runat="server" onserverclick="SavestepsEdit_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearsteps" value=" 重 填 " class="form" id="Clearstepsedit" runat="server" />
        </label></td>
    </tr>
  </table>
  <%
            }
         %>
</form>
<br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat="server" /></td>
  </tr>
</table>
</body>
</html>
