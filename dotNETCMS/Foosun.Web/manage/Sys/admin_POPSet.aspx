<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_admin_POPSet" Codebehind="admin_POPSet.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td class="sysmain_navi" style="PADDING-LEFT: 14px;height:30px;width:57%">管理员权限管理</td>
      <td class="topnavichar"  style="PADDING-LEFT: 14px;height:30px;width:43%"><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="admin_list.aspx" target="sys_main" class="list_link">管理员管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />权限设置</div></td>
    </tr>
    </table>
    
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;">套用固定权限组：<a href="javascript:getPopGroup(5)" class="list_link">录入员</a>&nbsp;┊&nbsp;<a href="javascript:getPopGroup(4)" class="list_link">编辑</a>&nbsp;┊&nbsp;<a href="javascript:getPopGroup(3)" class="list_link">责任编辑</a>&nbsp;┊&nbsp; <a href="javascript:getPopGroup(2)" class="list_link">总编辑</a>&nbsp;┊&nbsp;<a href="javascript:getPopGroup(1)" class="list_link">普通管理员</a>&nbsp;┊&nbsp;<a href="javascript:getPopGroup(0)" class="list_link">高级管理员</a>&nbsp;&nbsp;<span class="helpstyle" style="cursor:help;" title="如何使用扩展权限" onclick="Help('H_pop_ext',this)">如何使用扩展权限?</span> <input type="checkbox" value="-222" onclick="selectAll(this.form,this.checked);" />全选</td>
      </tr>
    </table>
    
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;" class="sys_topBg"> 内容管理&nbsp;&nbsp;<img src="../../sysImages/folder/bs.gif" border="0" style="cursor:pointer;" onclick="hiddenShowDiv('contentPop');" title="点击隐藏/展开" /></td>
      </tr>
      <tr>
        <td style="width:43%;line-height:25px;" class="TR_BG_list">
        <div id="contentPop" runat="server" />
        </td>
      </tr>
    </table>
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;" class="sys_topBg"> 会员管理&nbsp;&nbsp;<img src="../../sysImages/folder/bs.gif" border="0" style="cursor:pointer;" onclick="hiddenShowDiv('UserPop');" title="点击隐藏/展开" /></td>
      </tr>
      <tr>
        <td style="width:43%;" class="TR_BG_list">
            <div id="UserPop" runat="server" />
        </td>
      </tr>
    </table>
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;" class="sys_topBg"> 模板管理&nbsp;&nbsp;<img src="../../sysImages/folder/bs.gif" border="0" style="cursor:pointer;" onclick="hiddenShowDiv('TempletPop');" title="点击隐藏/展开" /></td>
      </tr>
      <tr>
        <td style="width:43%;" class="TR_BG_list">
            <div id="TempletPop" runat="server" />
        </td>
      </tr>
    </table>
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;" class="sys_topBg"> 发布管理&nbsp;&nbsp;<img src="../../sysImages/folder/bs.gif" border="0" style="cursor:pointer;" onclick="hiddenShowDiv('PublishPop');" title="点击隐藏/展开" /></td>
      </tr>
      <tr>
        <td style="width:43%;" class="TR_BG_list">
         <div id="PublishPop" runat="server" />
        </td>
      </tr>
    </table>
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;" class="sys_topBg"> 系统插件&nbsp;&nbsp;<img src="../../sysImages/folder/bs.gif" border="0" style="cursor:pointer;" onclick="hiddenShowDiv('sysPlusPop');" title="点击隐藏/展开" /></td>
      </tr>
      <tr>
        <td style="width:43%;" class="TR_BG_list">
         <div id="sysPlusPop" runat="server" />
        </td>
      </tr>
    </table>
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;" class="sys_topBg"> 控制面板&nbsp;&nbsp;<img src="../../sysImages/folder/bs.gif" border="0" style="cursor:pointer;" onclick="hiddenShowDiv('ControlPop');" title="点击隐藏/展开" /></td>
      </tr>
      <tr>
        <td style="width:43%;" class="TR_BG_list">
         <div id="ControlPop" runat="server" />
        </td>
      </tr>
    </table>
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;" class="sys_topBg"> API管理&nbsp;&nbsp;<img src="../../sysImages/folder/bs.gif" border="0" style="cursor:pointer;" onclick="hiddenShowDiv('APIPop');" title="点击隐藏/展开" /></td>
      </tr>
      <tr>
        <td style="width:43%;" class="TR_BG_list">
         <div id="APIPop" runat="server" />
        </td>
      </tr>
    </table>
    <table style="width:98%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
      <tr>
        <td style="width:43%;" class="TR_BG_list">
          <asp:Button ID="Button1" runat="server" Text="保存权限" OnClick="Button1_Click" />
            <input type="button" name="Submit" value="重新设置" onclick="javascript:UnDo();" />
            </td>
          
      </tr>
    </table>
    </form>
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
       <tr>
         <td align="center"><div id="copyright" runat="server" /></td>
       </tr>
    </table>
</body>
</html>
 <script language="javascript" type="text/javascript">
    function UnDo()
    {
        if(confirm('你确定要取消所做的更改吗?'))
        {
            document.form1.reset();
        }   
    }
    function getPopGroup(num)
    {
       var  UserNum = <%Response.Write(Request.QueryString["UserNum"]); %>
       var  id = <%Response.Write(Request.QueryString["id"]); %>
       window.location.href="admin_POPSet.aspx?UserNum="+UserNum+"&id="+id+"&num="+num+""
    }
    function getPopCode(code)
    {
	    var ie4=document.all&&navigator.userAgent.indexOf("Opera")==-1
	    var ns6=document.getElementById&&!document.all
        if (ie4)
        {
            var clipBoardContent=code;
            window.clipboardData.setData("Text",clipBoardContent);
            alert("代码已经复制。代码："+code+"");
        }
        else
        {
            alert("FireFox浏览器用户请直接复制代码!");
        }
    }
    function hiddenShowDiv(id)
    {
        var objs = document.getElementById(id);
        if(objs.style.display=="")
        {
           objs.style.display = "none"; 
        }
        else
        {
           objs.style.display = ""; 
        }
    }
</script>