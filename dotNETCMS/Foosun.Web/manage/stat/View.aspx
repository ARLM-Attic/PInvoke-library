<%@ Page Language="C#" AutoEventWireup="true" Inherits="Manage_Stat_View" Codebehind="View.aspx.cs" %>
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
<form id="BaseTableSet" runat="server">
  <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">统计系统</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />统计系统</div></td>
    </tr>
  </table>
  <div id="ShowNavi" runat="server"/>
  <div id="NoContent" runat="server" />
  <% 
    string Navi = Request.QueryString["Navi"];
    if (Navi == "view")
        {
%>
  <div id="statmethod" runat="server" />
  <%
    }
 %>
  <%
     string type = Request.QueryString["type"];
     if(type=="pram")
     {
  %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table" id="stat_ParamTable" >
    <tr class="TR_BG_list">
      <td colspan="2" class="list_link">统计系统参数设置</td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%"  class="list_link"><div align="right">系统名称</div></td>
      <td width="78%" class="list_link"><asp:TextBox ID="SystemName" runat="server" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_statParam0001',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%"  class="list_link"><div align="right">英文名称</div></td>
      <td width="78%" class="list_link"><asp:TextBox ID="SystemNameE" runat="server" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_statParam0002',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link"><div align="right">IP防刷新</div></td>
      <td class="list_link"><asp:DropDownList ID="ipCheck" runat="server" Style="position: relative" CssClass="form">
          <asp:ListItem Value="1">开启</asp:ListItem>
          <asp:ListItem Value="0">不开启</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_statParam0003',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%"  class="list_link" style="height: 32px"><div align="right">IP防刷新时间</div></td>
      <td width="78%" class="list_link" style="height: 32px"><asp:TextBox ID="ipTime" runat="server" CssClass="form"/>
        <font color="red">分钟</font>&nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_statParam0004',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link"><div align="right">开启在线统计</div></td>
      <td class="list_link"><asp:DropDownList ID="isOnlinestat" runat="server" Style="position: relative" CssClass="form">
          <asp:ListItem Value="1">开启</asp:ListItem>
          <asp:ListItem Value="0">不开启</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_statParam0005',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%"  class="list_link"><div align="right">每页记录数</div></td>
      <td width="78%" class="list_link"><asp:TextBox ID="pageNum" runat="server" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_statParam0006',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">Cookie时间</div></td>
      <td width="78%" class="list_link"><asp:TextBox ID="cookies" runat="server" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_statParam0007',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">小数精确度</div></td>
      <td width="78%" class="list_link"><asp:TextBox ID="pointNum" runat="server" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_statParam0009',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td class="list_link">&nbsp;</td>
      <td class="list_link"><input type="submit" id="savePram" name="savePram" value="保 存" class="form" runat="server" onserverclick="savePram_ServerClick" />
        &nbsp; &nbsp;&nbsp;
        <input type="reset" id="cancelPram" name="cancelPram" value="重 置" class="form" runat="server" />
    </tr>
  </table>
  <%
}
 %>
  <%
    if(type == "class")
    {
  %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1">
    <tr>
      <td height="18" style="width: 45%" colspan="2"  align="right" class="list_link"><a href="?act=add" class="topnavichar">新增分类</a>&nbsp;┊&nbsp;
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所选信息吗?')){return true;}return false;}" OnClick="DelP_Click">批量删除分类</asp:LinkButton>
        &nbsp;┊&nbsp;
        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除全部信息吗?')){return true;}return false;}" OnClick="DelAll_Click">删除全部分类</asp:LinkButton>
        &nbsp;┊&nbsp;
        <asp:LinkButton ID="LinkButton7" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认清空所有信息吗?\n清空后将不能还原!')){return true;}return false;}" OnClick="ClearAll_Click">清空所有统计信息</asp:LinkButton></td>
    </tr>
  </table>
  <asp:Repeater ID="DataList2" runat="server">
    <HeaderTemplate>
      <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table" id="StatClassTable">
      <tr class="TR_BG">
        <td width="7%" align="center" valign="middle" class="sys_topBg">类别编号</td>
        <td width="10%" align="center" valign="middle" class="sys_topBg">类别名称</td>
        <td width="20%" align="center" valign="middle" class="sys_topBg">操作
          <input type="checkbox" id="stat_checkbox" value="-1" name="stat_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list">
        <td width="7%" align="center" valign="middle" height=20><%#((DataRowView)Container.DataItem)[1]%></td>
        <td width="10%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
        <td width="20%" align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["oPerate"]%></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      </table>
    </FooterTemplate>
  </asp:Repeater>
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator2" runat="server" />
  </div>
  <%
    }
   %>
  <%
        string act = Request.QueryString["act"];
       if(act =="add")
       {
    %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="AddClass">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">统计系统新增分类</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 257px"> 分类名称：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="ClassName" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_statClass_0001',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Saveclass" value=" 提 交 " class="form" id="stataddclass" runat="server" onserverclick="stataddclass_ServerClick" />
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearclass" value=" 重 填 " class="form" id="statclearclass" runat="server" />
        </label></td>
    </tr>
  </table>
  <% 
    }
    %>
  <%
       if(act =="edit")
       {
    %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table" id="Table1">
    <tr class="TR_BG_list">
      <td class="list_link" colspan="2">统计系统修改分类</td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 257px"> 分类名称：</td>
      <td  align="left" class="list_link"><asp:TextBox ID="classnameEdit" runat="server" Width="124px" CssClass="form"/>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_statClass_0001',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"  colspan="2" class="list_link"><label>
        <input type="submit" name="Saveclass" value=" 提 交 " class="form" id="Editsave" runat="server" onserverclick="Editsave_ServerClick" />
        </label>
        &nbsp;&nbsp;
        <label>
        <input type="reset" name="Clearclass" value=" 重 填 " class="form" id="editclear" runat="server" />
        </label></td>
    </tr>
  </table>
  <% 
    }
    %>
  <%
    if (type == "zonghe")
    {
 %>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table" id="ZongStatTable">
    <tr class="TR_BG_list">
      <td colspan="2" class="list_link">综合统计信息显示</td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">总访问量:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="AllViewNum" runat="server" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">最高访问量:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="TheHightViewNum" runat="server" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">最高访问量日期:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="TheHightViewNumDay" runat="server" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">在线人数:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="OnlinePeopleNum" runat="server"/></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">开始统计于:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="StatTimeStart" runat="server" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">今日访问量:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="TodayViewNum" runat="server" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">昨日访问量:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="YesterDayViewNum" runat="server"/></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">今年访问量:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="ThisYearViewNum" runat="server"/></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">本月访问量:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="ThisMonthViewNum" runat="server"/></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">统计天数:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="StatDaysNum" runat="server"/></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">平均日访问量:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="AverageDayViewNum" runat="server"/></td>
    </tr>
    <tr class="TR_BG_list">
      <td width="22%" class="list_link" ><div align="right">预计今日:</div></td>
      <td class="list_link" style="width: 78%"><asp:Label ID="GuessTodayViewNum" runat="server"/></td>
    </tr>
  </table>
  <%
    }
 %>
  <%
    if(type=="all")
    {
 %>
  <asp:Repeater ID="DataList1" runat="server">
    <HeaderTemplate>
      <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" bgcolor="#FFFFFF" class="table" id="XiangxiStatTable">
      <tr class="TR_BG">
        <td align="center" valign="middle" class="sys_topBg">时间</td>
        <td align="center" valign="middle" class="sys_topBg">地区</td>
        <td align="center" valign="middle" class="sys_topBg">屏宽</td>
        <td align="center" valign="middle" class="sys_topBg">操作系统</td>
        <td align="center" valign="middle" class="sys_topBg">浏览器</td>
        <td align="center" valign="middle" class="sys_topBg">来源网页</td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list">
        <td align="center" valign="middle" height=20><%#((DataRowView)Container.DataItem)[1]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[2]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[3]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[4]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[5] %>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)[6]%> </td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      </table>
    </FooterTemplate>
  </asp:Repeater>
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
  </div>
  <%
      }
       %>
  <%

    if (type == "hour")
    {
     %>
  <table width="98%" cellspacing="0" align="center"  id="HoursTable"  class="table">
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 最近24小时访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="430" align="center">
          <tr height="9" class="TR_BG_list">
            <td colspan="29" class="list_link"></td>
          </tr>
          <tr height="101" class="TR_BG_list">
            <td align="right" valign="top" class="list_link"><%=HourStat(0)%>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="hour_lbhigh1" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="hour_lbhigh2" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="hour_lbhigh3" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="hour_lbhigh4" Runat="server" />
                <br>
                </font> </p></td>
            <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
            <%=HourStat(1)%>
            <td width="10" class="list_link"></td>
            <td width="10" class="list_link"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan="29" class="list_link"></td>
          </tr>
        </table></td>
    </tr>
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 所有24小时访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="430" align="center">
          <tr height="9" class="TR_BG_list">
            <td colspan="29" class="list_link"></td>
          </tr>
          <tr height="101" class="TR_BG_list">
            <td align="right" valign="top" class="list_link"><%=HourStat(2)%>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="hour_lbhigh5" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="hour_lbhigh6" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="hour_lbhigh7" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="hour_lbhigh8" Runat="server" />
                <br>
                </font> </p></td>
            <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
            <%=HourStat(3)%>
            <td width=10 class="list_link"></td>
            <td width=10 class="list_link"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan=29 class="list_link"></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
    }
 %>
  <%
    if (type == "day")
    {
 %>
  <table width="98%" cellspacing="0" align="center" id="DaysStatTable"  runat="server" class="table">
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 最近31天访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="453" align="center">
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="101" class="TR_BG_list">
            <td align="right" valign="top" class="list_link"><%=DayStat(0)%>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="day_lbhigh1" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="day_lbhigh2" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="day_lbhigh3" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="day_lbhigh4" Runat="server" />
                <br>
                </font> </p></td>
            <td width="10"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
            <%=DayStat(1)%>
            <td width="10" class="list_link"></td>
            <td width="10" class="list_link"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td></td>
          </tr>
        </table></td>
    </tr>
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 所有月份日访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="453" align="center">
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="101" class="TR_BG_list">
            <td align="right" valign="top" class="list_link"><%=DayStat(2)%>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="day_lbhigh5" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="day_lbhigh6" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="day_lbhigh7" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="day_lbhigh8" Runat="server" />
                <br>
                </font> </p></td>
            <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
            <%=DayStat(3)%>
            <td width="10" class="list_link"></td>
            <td width="10" class="list_link"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan="29" class="list_link"></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
   }
%>
  <%
    if(type=="week")
    {
 %>
  <table width="98%" cellspacing="0" align="center" id="WeekStatTable" runat="server" class="table">
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 周访问量统计 <br>
        <table width="90%" align="center">
          <tr class="TR_BG_list">
            <td><table border="0" cellpadding="0" cellspacing="0" width="175" align="center">
                <tr height="9" class="TR_BG_list">
                  <td class="list_link"></td>
                </tr>
                <tr height="101" class="TR_BG_list">
                  <td align="right" valign="top" class="list_link"><%=WeekStat(0)%>
                    <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                      <asp:Label ID="week_lbhigh1" Runat="server" />
                      </font>
                    <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                      <asp:Label ID="week_lbhigh2" Runat="server" />
                      </font>
                    <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                      <asp:Label ID="week_lbhigh3" Runat="server" />
                      </font>
                    <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                      <asp:Label ID="week_lbhigh4" Runat="server" />
                      <br>
                      </font> </p></td>
                  <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
                  <%=WeekStat(1)%>
                  <td width="10" class="list_link"></td>
                  <td width="10" class="list_link"></td>
                </tr>
                <tr height="5" class="TR_BG_list">
                  <td class="list_link"></td>
                </tr>
              </table></td>
            <td class="list_link"><table border="0" cellpadding="0" cellspacing="0" width="175" align="center">
                <tr height="9" class="TR_BG_list">
                  <td class="list_link"></td>
                </tr>
                <tr height="101" class="TR_BG_list">
                  <td align="right" valign="top" class="list_link"><%=WeekStat(2)%>
                    <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                      <asp:Label ID="week_lbhigh5" Runat="server" />
                      </font>
                    <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                      <asp:Label ID="week_lbhigh6" Runat="server" />
                      </font>
                    <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                      <asp:Label ID="week_lbhigh7" Runat="server" />
                      </font>
                    <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                      <asp:Label ID="week_lbhigh8" Runat="server" />
                      <br>
                      </font> </p></td>
                  <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
                  <%=WeekStat(3)%>
                  <td width=10 class="list_link"></td>
                  <td width=10 class="list_link"></td>
                </tr>
                <tr height="5" class="TR_BG_list">
                  <td colspan=29 class="list_link"></td>
                </tr>
              </table></td>
          </tr>
          <tr height="20" align="center" class="TR_BG_list">
            <td>↑ 最近的一周</td>
            <td>↑ 全部时段</td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
}
 %>
  <%
    if(type=="month")
    {
 %>
  <table width="98%" cellspacing="0" align="center" cellpadding="0" border="0" id="MonthStatTable" runat="server" class="table">
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 最近12个月访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="310" align="center">
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="101" class="TR_BG_list">
            <td align="right" valign="top" class="list_link"><%=MonthStat(0)%>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="month_lbhigh1" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="month_lbhigh2" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="month_lbhigh3" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="month_lbhigh4" Runat="server" />
                <br>
                </font> </p></td>
            <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
            <%=MonthStat(1)%>
            <td width="10" class="list_link"></td>
            <td width="10" class="list_link"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan="29" class="list_link"></td>
          </tr>
        </table></td>
    </tr>
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 所有12个月访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="310" align="center">
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="101" class="TR_BG_list">
            <td align="right" valign="top" class="list_link"><%=MonthStat(2)%>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="month_lbhigh5" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="month_lbhigh6" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="month_lbhigh7" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="month_lbhigh8" Runat="server" />
                <br>
                </font> </p></td>
            <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
            <%=MonthStat(3)%>
            <td width="10"></td>
            <td width="10"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan="29" class="list_link"></td>
          </tr>
        </table></td>
    </tr>
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 年访问量统计 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="270" align="center">
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="10" class="TR_BG_list">
            <td width="40" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_up.gif"></td>
          </tr>
          <%=MonthStat(4)%>
          <tr height="10" class="TR_BG_list">
            <td width="40" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_down.gif"></td>
          </tr>
          <tr height="5">
            <td colspan="29" class="list_link"></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
}
 %>
  <%
    if(type=="page")
    {
 %>
  <table width="98%" cellspacing="0" align="center" cellpadding="0" border="0" id="ViewPageTable" runat="server" class="table">
    <tr height="30" class="TR_BG_list">
      <td width="1" class="backs"></td>
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align=absmiddle> &nbsp; 被访问页面及访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="450" align=center>
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="10" class="TR_BG_list">
            <td width="220" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_up.gif"></td>
          </tr>
          <%=PageStat()%>
          <tr height="10" class="TR_BG_list">
            <td width="220" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_down.gif"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan=29 class="list_link"></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
}
 %>
  <%

    if (type == "ip")
    {
 %>
  <table width="98%" cellspacing="0" align="center" cellpadding="0" border="0" class="table" id="IPStatTable" runat="server">
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align=absmiddle> &nbsp; IP地址及访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="350" align=center>
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="10" class="TR_BG_list">
            <td width="120" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_up.gif"></td>
          </tr>
          <%=IpStat()%>
          <tr height="10">
            <td width="120" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_down.gif"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan=29 class="list_link"></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
    }
  %>
  <%
    if(type=="cs")
    {
 %>
  <table width="98%" cellspacing="0" align="center" cellpadding="0" border="0" id="SoftWareTable" runat="server" class="table">
    <tr height="30" class="TR_BG_list">
      <td class="list_link" style="width: 498px" colspan="2">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 浏览器及访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="385" align="center">
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="101" class="TR_BG_list">
            <td align="right" valign="top" class="list_link"><%=SoftStat(0)%>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="soft_lbhigh1" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="soft_lbhigh2" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="soft_lbhigh3" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="soft_lbhigh4" Runat="server" />
                <br>
                </font> </p></td>
            <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
            <%=SoftStat(1)%>
            <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_right.gif"></td>
            <td width="10" class="list_link"></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link"><p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">0</font></p></td>
            <td width="10" class="list_link"></td>
            <%=SoftStat(2)%>
            <td width="10" class="list_link"></td>
            <td width="10" class="list_link"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
        </table></td>
    </tr>
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link" colspan="2">&nbsp; <img src="../../sysImages/folder/stat.gif" align="absMiddle"> &nbsp; 操作系统及访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="385" align="center">
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="101" class="TR_BG_list">
            <td align="right" valign="top" class="list_link"><%=OsStat(0)%>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="soft_lbhigh5" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="soft_lbhigh6" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 13px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="soft_lbhigh7" Runat="server" />
                </font>
              <p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">
                <asp:Label ID="soft_lbhigh8" Runat="server" />
                <br>
                </font> </p></td>
            <td width="10" align="right" class="list_link"><img src="../../sysImages/StatIcon/tu_back_left.gif"></td>
            <%=OsStat(1)%>
            <td width="10" class="list_link"><img src="../../sysImages/StatIcon/tu_back_right.gif"></td>
            <td width="10" class="list_link"></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link"><p style="MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 100%; MARGIN-RIGHT: 2px"> <font face="Arial">0</font></p></td>
            <td width="10" class="list_link"></td>
            <%=OsStat(2)%>
            <td width="10" class="list_link"></td>
            <td width="10" class="list_link"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
        </table></td>
    </tr>
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link" colspan="2">&nbsp; <img src="../../sysImages/folder/stat.gif" align=absmiddle> &nbsp; 客户端屏幕宽度统计 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="270" align=center>
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="10" class="TR_BG_list">
            <td width="40" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_up.gif"></td>
          </tr>
          <%=WidthStat()%>
          <tr height="10" class="TR_BG_list">
            <td width="40" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_down.gif"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan=29></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
}
 %>
  <%
    if(type=="area")
    {
 %>
  <table width="98%" cellspacing="0" align="center" cellpadding="0" border="0"  id="AreaStatTable" class="table" runat="server">
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align=absmiddle> &nbsp; 访问者地区及访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="350" align=center>
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="10" class="TR_BG_list">
            <td width="120" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_up.gif"></td>
          </tr>
          <%=AreaStat()%>
          <tr height="10" class="TR_BG_list">
            <td width="120" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_down.gif"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan=29 class="list_link"></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
}
 %>
  <%
if(type=="come")
{
 %>
  <table width="98%" cellspacing="0" align="center" cellpadding="0" border="0" id="ComeStatTable" runat="server" class="table" >
    <tr height="30" class="TR_BG_list">
      <td width="498" class="list_link">&nbsp; <img src="../../sysImages/folder/stat.gif" align=absmiddle> &nbsp; 来路及访问量 <br>
        <table border="0" cellpadding="0" cellspacing="0" width="450" align=center>
          <tr height="9" class="TR_BG_list">
            <td class="list_link"></td>
          </tr>
          <tr height="10" class="TR_BG_list">
            <td width="220" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_up.gif"></td>
          </tr>
          <%=ComeStat()%>
          <tr height="10" class="TR_BG_list">
            <td width="220" class="list_link"></td>
            <td width="230" class="list_link"><img src="../../sysImages/StatIcon/tu_back_down.gif"></td>
          </tr>
          <tr height="5" class="TR_BG_list">
            <td colspan=29 class="list_link"></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <%
}
 %>
  <%
     if (type == "code")
     { 
  %>
  <div id="CodeUseTable" runat="server"></div>
  <%
  }
  %>
  <br />
  <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
    <tr>
      <td align="center"><label id="copyright" runat="server" /></td>
    </tr>
  </table>
</form>
</body>
</html>
