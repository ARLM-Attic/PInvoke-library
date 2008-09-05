<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_user_userlist" Codebehind="userlist.aspx.cs" %>
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
              <td width="57%" class="sysmain_navi" style="padding-left:14px;">会员管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />会员管理</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><a href="userlist.aspx" class="topnavichar">会员首页</a>┋<a href="userlist.aspx?usertype=" class="topnavichar" >所有会员</a>┋<a href="userlist.aspx?usertype=0" class="topnavichar" >开放的会员</a>┋<a href="userlist.aspx?usertype=1" class="topnavichar" >锁定的会员</a>┋<a href="userlist.aspx?iscert=1" class="topnavichar" >实名认证用户</a>┋<a href="userlist.aspx?iscert=2" class="topnavichar" >审核实名认证用户</a>┋<span style="CURSOR: hand"  onclick="opencats('searchuserID');">查询</span>┋<span id="groupList" runat="server" /><span id="channelList" runat="server" /></td>
      </tr>
    </table> 
     
     <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable" style="display:none;" id="searchuserID">
      <tr>
        <td  style="padding-left:12px;">用户名：
        <input type="text" name="username" id="TxtUserNm" value="" style="width:80px;" />
        &nbsp;姓名：<input type="text" name="realname" id="realname" value="" style="width:80px;" />
        &nbsp;编号：<input type="text" name="userNum" id="userNum" value="" style="width:80px;" />
        &nbsp;性别：<select name="sex" id="sex">
            <option value="1">男</option>
            <option value="2">女</option>
            <option value="0">保密</option>
            <option value="" selected="selected">不限制</option>
          </select>
          </td>
          </tr>
       <tr>
        <td style="padding-left:12px;">
        积分：>=<input type="text" name="ipoint" id="ipoint" value="" style="width:40px;"  /><=<input type="text" name="bipoint" id="bipoint" value="" style="width:40px;"  />
        &nbsp;&nbsp;G 币：>=<input type="text" name="gpoint" id="gpoint" value="" style="width:40px;" /><=<input type="text" name="bgpoint" id="bgpoint" value="" style="width:40px;" />
       <input type="button" name="Submit" value="搜索" runat="server" class="form" id="Button8" onserverclick="Button8_ServerClick" />
        </td>
       </tr>
    </table>  
    
<asp:Repeater ID="userlists" runat="server">
   <HeaderTemplate>
       <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#FFFFFF" class="table">
        <tr class="TR_BG">
        <td class="sys_topBg" style="width:120px">用户名</td>
        <td class="sys_topBg" style="width:85px">所属会员组</td>
        <td class="sys_topBg" align="center" style="width:45px">点数</td>
        <td class="sys_topBg" align="center" style="width:45px">G币</td>
<%--        <td class="sys_topBg" style="width:130px">注册日期</td>
--%>        <td class="sys_topBg" align="center" style="width:45px">状态</td>
        <td class="sys_topBg">登陆日期</td>
        <td class="sys_topBg">操作<input type="checkbox" value="-222" onclick="selectAll(this.form,this.checked);" /></td>
        </tr>
    </HeaderTemplate>
      <ItemTemplate>
        <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td align="left" valign="middle"><%#((DataRowView)Container.DataItem)["userNames"]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["groupname"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["iPoint"]%></td>
        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["gPoint"]%></td>
<%--        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["regTime"]%></td>
--%>        <td align="center" valign="middle" ><%#((DataRowView)Container.DataItem)["lock"]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["LastLoginTime"]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)["op"]%></td>
        </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
     </FooterTemplate>
</asp:Repeater> 

<table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
   <tr>
     <td align="left">
         <uc1:PageNavigator ID="PageNavigator1" runat="server" /></td>
   </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="height: 20px">
   <tr>
     <td align="left">
         <asp:Button ID="Button1" CssClass="form" runat="server" onclick="islock" Text="批量锁定"  OnClientClick="{if(confirm('确定要锁定吗？')){return true;}return false;}" />&nbsp;
         <asp:Button  ID="Button2" CssClass="form" runat="server" onclick="unlock" Text="批量解锁" OnClientClick="{if(confirm('确定要解锁吗？')){return true;}return false;}" />&nbsp;
         <asp:Button ID="Button3" CssClass="form"  runat="server" onclick="dels" Text="批量删除"  OnClientClick="{if(confirm('确定要删除吗？')){return true;}return false;}"/>&nbsp;
         <asp:Button ID="Button4" CssClass="form" runat="server" onclick="bIpoint" Text="增加点数" />&nbsp;
         <asp:Button ID="Button5" CssClass="form" onclick="sIpoint"  runat="server" Text="扣除点数"/>&nbsp;
         <asp:Button ID="Button6" CssClass="form" onclick="bGpoint"  runat="server" Text="增加G币" />&nbsp;
         <asp:Button ID="Button7" CssClass="form" onclick="sGpoint"  runat="server" Text="扣除G币" />&nbsp;
         </td>
   </tr>
</table>

</form><br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
</table>
</body>
</html>
<script type="text/javascript" language="javascript">
    function getFormInfo(obj)
    {
       var GroupNumber=obj.value;
       window.location.href="userList.aspx?GroupNumber="+GroupNumber+"";
    }
    function getchanelInfo(obj)
    {
       var SiteID=obj.value;
       window.location.href="userList.aspx?SiteID="+SiteID+"";
    }
    
    function opencats(cat)
    {
      if(document.getElementById(cat).style.display=="none")
      {
         document.getElementById(cat).style.display="";
      } else {
         document.getElementById(cat).style.display="none";
      }
    }
</script>