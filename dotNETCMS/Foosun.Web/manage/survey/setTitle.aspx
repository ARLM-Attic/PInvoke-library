<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_survey_setTitle" Codebehind="setTitle.aspx.cs" %>
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
        <td height="18" style="width: 45%" colspan="2" style="PADDING-LEFT: 14px"><label id="param_id" runat="server" /><a href="setClass.aspx" class="menulist">投票分类设置</a>&nbsp;┊&nbsp;<a href="setTitle.aspx" class="menulist">投票主题设置</a>&nbsp;┊&nbsp;<a href="setItem.aspx" class="menulist">投票选项设置</a>&nbsp;┊&nbsp;<a href="setSteps.aspx" class="menulist">多步投票管理</a>&nbsp;┊&nbsp;<a href="ManageVote.aspx" class="menulist">投票情况管理</a> </div></td>
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
        <td height="18" style="width: 45%" colspan="2" ><div align="right"><a href="?type=add" class="topnavichar">新增主题</a> |
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
        <td class="sys_topBg">主题</td>
        <td class="sys_topBg">类别</td>
        <td class="sys_topBg">类型</td>
        <td class="sys_topBg">选项数</td>
        <td class="sys_topBg">方式</td>
        <td class="sys_topBg">开始时间</td>
        <td class="sys_topBg">结束时间</td>
        <td class="sys_topBg">JS调用</td>
        <td class="sys_topBg">操作
          <input type="checkbox" id="vote_checkbox" value="-1" name="vote_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td><%#((DataRowView)Container.DataItem)[2]%></td>
        <td><%#((DataRowView)Container.DataItem)["voteClass"]%></td>
        <td><%#((DataRowView)Container.DataItem)["type"]%></td>
        <td><%#((DataRowView)Container.DataItem)[4]%></td>
        <td><%#((DataRowView)Container.DataItem)["displayModel"]%></td>
        <td><%#((DataRowView)Container.DataItem)[6]%></td>
        <td><%#((DataRowView)Container.DataItem)[7]%></td>
        <td><%#((DataRowView)Container.DataItem)["js"]%></td>
        <td><%#((DataRowView)Container.DataItem)["oPerate"]%> </td>
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
      <td class="list_link"> 主题查询:
        &nbsp;
        关键字:
        <asp:TextBox runat="server" ID="KeyWord" size="15"  CssClass="form"/>
        &nbsp;&nbsp;
        查询类型:
        <asp:DropDownList ID="DdlKwdType" runat="server"  CssClass="form">
          <asp:ListItem Value="choose" Text="请选择" />
          <asp:ListItem Value="title" Text="主题" />
          <asp:ListItem Value="class" Text="类别" />
          <asp:ListItem Value="num" Text="选项数" />
          <asp:ListItem Value="starttime" Text="开始时间" />
          <asp:ListItem Value="endtime" Text="结束时间" />
          <asp:ListItem Value="itemmodel" Text="排列方式" />
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Button runat="server" ID="BtnSearch" Text=" 查询 " CssClass="form" OnClick="BtnSearch_Click"/>
        &nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_searchTitle_0001',this)">帮助</span> </td>
    </tr>
  </table>
  <%
      }
       %>
  <%
    if(type=="add")
    {
 %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="Addvote_Title">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">新增问卷调查主题信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 调查类别：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="vote_ClassName" runat="server" CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0001',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 调查主题：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="Title" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 项目类型：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="TypeSelect" runat="server" CssClass="form">
          <asp:ListItem Value="0" Selected="True">单选</asp:ListItem>
          <asp:ListItem Value="1">多选</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0003',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list" >
      <td align="right"  class="list_link" style="width: 171px"> 最多选项个数：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="MaxselectNum" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0004',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 显示方式：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="DisModel" runat="server" CssClass="form">
          <asp:ListItem Value="0" Selected="True">柱形图</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0005',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 是否允许多步投票：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="isSteps" runat="server" CssClass="form">
          <asp:ListItem Value="1">是</asp:ListItem>
          <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0009',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 开始时间：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="Starttime" runat="server" Width="124px" CssClass="form"/>
        &nbsp;
        <input type="button" name="starttime" value=" 选择时间 " class="form" id="starttime" onclick="selectFile('date',document.form1.Starttime,280,500);document.form1.Starttime.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0006',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 结束时间：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="Endtime" runat="server" Width="124px" CssClass="form"/>
        &nbsp;
        <input type="button" name="endtime" value=" 选择时间 " class="form" id="endtime" onclick="selectFile('date',document.form1.Endtime,280,500);document.form1.Endtime.focus();"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0007',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 选项排列方式：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="SortStyle" runat="server" CssClass="form">
          <asp:ListItem Value="0" Selected="True">横向排列</asp:ListItem>
          <asp:ListItem Value="1">1选项/行(纵向)</asp:ListItem>
          <asp:ListItem Value="2">2选项/行</asp:ListItem>
          <asp:ListItem Value="3">3选项/行</asp:ListItem>
          <asp:ListItem Value="4">4选项/行</asp:ListItem>
          <asp:ListItem Value="5">5选项/行</asp:ListItem>
          <asp:ListItem Value="6">6选项/行</asp:ListItem>
          <asp:ListItem Value="7">7选项/行</asp:ListItem>
          <asp:ListItem Value="8">8选项/行</asp:ListItem>
          <asp:ListItem Value="9">9选项/行</asp:ListItem>
          <asp:ListItem Value="10">10选项/行</asp:ListItem>
          <asp:ListItem Value="11">11选项/行</asp:ListItem>
          <asp:ListItem Value="12">12选项/行</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0008',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Savetitle" value=" 提 交 " class="form" id="Savetitle" runat="server" onserverclick="Savetitle_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Cleartitle" value=" 重 填 " class="form" id="ClearTitle" runat="server" />
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
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="EditTitle">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">修改问卷调查主题信息</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 调查类别：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="ClassnameE" runat="server" CssClass="form"> </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0001',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 调查主题：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="TitleE" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 项目类型：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="TypeE" runat="server" onchange="Select(this.value)" CssClass="form">
          <asp:ListItem Value="0" Selected="True">单选</asp:ListItem>
          <asp:ListItem Value="1">多选</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0003',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 最多选项个数：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="MaxNumE" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0004',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 显示方式：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="DisModelE" runat="server" CssClass="form">
          <asp:ListItem Value="0" Selected="True">柱形图</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0005',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 是否允许多步投票：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="isStepsE" runat="server" CssClass="form">
          <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
          <asp:ListItem Value="0">否</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0009',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 开始时间：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="StartTimeE" runat="server" Width="124px" CssClass="form"/>
        &nbsp;
        <input type="button" name="starttime" value=" 选择时间 " class="form" id="Button1" onclick="selectFile('date',document.form1.StartTimeE,280,500);document.form1.StartTimeE.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0006',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 结束时间：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="EndTimeE" runat="server" Width="124px" CssClass="form"/>
        &nbsp;
        <input type="button" name="starttime" value=" 选择时间 " class="form" id="Button2" onclick="selectFile('date',document.form1.EndTimeE,280,500);document.form1.EndTimeE.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0007',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 171px"> 选项排列方式：</td>
      <td  align="left" class="list_link"><asp:DropDownList ID="StyleE" runat="server" CssClass="form">
          <asp:ListItem Value="0" Selected="True">横向排列</asp:ListItem>
          <asp:ListItem Value="1">1选项/行(纵向)</asp:ListItem>
          <asp:ListItem Value="2">2选项/行</asp:ListItem>
          <asp:ListItem Value="3">3选项/行</asp:ListItem>
          <asp:ListItem Value="4">4选项/行</asp:ListItem>
          <asp:ListItem Value="5">5选项/行</asp:ListItem>
          <asp:ListItem Value="6">6选项/行</asp:ListItem>
          <asp:ListItem Value="7">7选项/行</asp:ListItem>
          <asp:ListItem Value="8">8选项/行</asp:ListItem>
          <asp:ListItem Value="9">9选项/行</asp:ListItem>
          <asp:ListItem Value="10">10选项/行</asp:ListItem>
          <asp:ListItem Value="11">11选项/行</asp:ListItem>
          <asp:ListItem Value="12">12选项/行</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_surveytitle_0008',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Savetitle" value=" 提 交 " class="form" id="Editsave" runat="server" onserverclick="Editsave_ServerClick"/>
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Cleartitle" value=" 重 填 " class="form" id="editclear" runat="server" />
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
<script language="javascript">
    function getjsCode(jsid)
    {
        if (jsid!=""&&!isNaN(jsid))
	     {
	        //----------控制居中显示----------------
	        var WWidth = (window.screen.width-500)/2;
            var Wheight = (window.screen.height-150)/2;
            //--------------------------------------
            window.open('showJsPath.aspx?jsid='+jsid, '投票JS代码调用', 'height=200, width=400, top='+Wheight+', left='+WWidth+', toolbar=no, menubar=no, scrollbars=no,resizable=yes,location=no, status=no');
	     }
	    else
	     {
		    alert("出现错误，请联系技术人员！");
	     }
    }
</script>
</html>
