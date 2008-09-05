<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_SysLabel_List" Codebehind="SysLabel_List.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="Form1" runat="server">
<div>
<table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
<tr>
  <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >标签管理</td>
  <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="SysLabel_List.aspx" class="list_link">标签管理</a></div></td>
</tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td style="padding-left:15px;"><a class="topnavichar" href="syslabel_bak.aspx">备份库</a>&nbsp;┊&nbsp; <a class="reshow" href="syslable_add.aspx?ClassID=<%Response.Write(Request.QueryString["ClassID"]); %>">新建标签</a>&nbsp;┊&nbsp; <a  class="topnavichar" href="syslabelclass_add.aspx">新建分类</a>&nbsp;┊&nbsp;<a href="sysLabel_out.aspx?type=out" class="topnavichar" title="导出所有标签">导出标签</a><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_label_out_001',this)">(如何导出?)</span>&nbsp;┊&nbsp; <a href="sysLabel_out.aspx?type=in" class="topnavichar">导入标签</a><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_label_in_001',this)">(如何导入?)</span>&nbsp;┊&nbsp; <a href="style.aspx" class="topnavichar">显示格式(样式管理)</a> <span id="Back" runat="server"></span>&nbsp;<span id="channelList" runat="server" /></td>
  </tr>
</table>

 <asp:Repeater ID="DataList1" runat="server">
   <HeaderTemplate>
       <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
      <tr class="TR_BG">
        <td align="left" valign="middle" class="sysmain_navi" style="width:400px;"><% =Cname %></td>
        <td align="left" valign="middle" class="sysmain_navi">创建日期</td>
        <td align="left" valign="middle" class="sysmain_navi" style="width:200px;">操作</td>
      </tr>   
    </HeaderTemplate>
      <ItemTemplate>
        <tr class="TR_BG_list" onmouseover="javascript:overColor(this);" onmouseout="javascript:outColor(this);">
        <td align="left" valign="middle" style="width:400px;"><%#((DataRowView)Container.DataItem)[7]%></td>
        <td align="left" valign="middle" ><%#((DataRowView)Container.DataItem)[3]%></td>
        <td align="left" valign="middle" style="width:200px;"><%#((DataRowView)Container.DataItem)[6]%></td>
        </tr>
        <tr style="display:none;background-color:#FFFFFF;" id="<%#((DataRowView)Container.DataItem)[1]%>">
        <td colspan="3" style="padding-top:12px;padding-left:12px;padding-bottom:12px;padding-right:12px;">
            <div style="font-size:11.5px;word-wrap:bread-word;word-break:break-all;"><%#((DataRowView)Container.DataItem)[5]%></div>
        </td>
        </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
     </FooterTemplate>
</asp:Repeater>

    <div style="width:98%;" align="right"><span style="float:left;">&nbsp;&nbsp;搜索：<input type="text" id="keywords" title="搜索标签名称和描述" value="" /> <input type="button" value="搜索标签" onclick="getKeywords(this);return false;" /> </span><span style="float:right;"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></span></div>
  <iframe id="reloadfromfoosun" src="about:blank" border="0" height="0" width="0" style="visibility: hidden"></iframe>
  <br />
  <br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat="server" /></td>
  </tr>
</table>
</div>
</form>
</body>
</html>
<script language="javascript" type="text/javascript">
function Update(type,id)
{
    switch (type)
    {
        case "Label":
            self.location="syslabel_edit.aspx?LabelID="+id;
            break;
        case "LabelClass":
            self.location="syslabelclass_edit.aspx?ClassID="+id;
           break;
    }
}
function Bak(id)
{
    if(confirm('你确认将此标签放入备份库吗?操作成功后此标签将失效!'))
    {
        self.location="?Op=Bak&ID="+id;
    }
}
function Del(type,id)
{
    switch (type)
    {
        case "Label":
            if(confirm('你确认将此标签放入回收站吗?'))
            {
                self.location="?Op=Del&type=Label&ID="+id;
            }
            break;
        case "LabelClass":
            if(confirm('你确认将此栏目放入回收站吗?\r此操作将会将此栏目以及属于此栏目的标签放入回收站.'))
            {
                self.location="?Op=Del&type=LabelClass&ID="+id;
            }
           break;
    }
}

function reload()
{
    if(confirm('您确认要重新从风讯(Foosun.net)下载 [系统内置标签] 吗?\n重新下载标签，将把您系统内置标签全部清空。\n特别注意：下载的是xml文件，把xml文件通过导入标签功能导入!\n如果您确认。请点[确定]按钮'))
    {
	    var ifm = document.getElementById("reloadfromfoosun");
	    ifm.src = "<%Response.Write(ReloadURL);%>";
    }
}

function Dels(type,id)
{
    switch (type)
    {
        case "Label":
            if(confirm('你将彻底删除此标签'))
            {
                self.location="?Op=Dels&type=Label&ID="+id;
            }
            break;
        case "LabelClass":
            if(confirm('你将彻底删除此栏目\r此操作将会彻底删除此栏目以及属于此栏目的标签'))
            {
                self.location="?Op=Dels&type=LabelClass&ID="+id;
            }    
           break;
    }
}

function getchanelInfo(obj)
{
   var SiteID=obj.value;
   window.location.href="sysLabel_list.aspx?SiteID="+SiteID+"&ClassID=<%Response.Write(Request.QueryString["ClassID"]); %>";
}

function shdivlabel(id)
{
    var gid=document.getElementById(id);
    if(gid.style.display=="")
    {
        gid.style.display="none";
    }
    else
    {
        gid.style.display="";
    }
}
function getKeywords(obj)
{
    var getValue = document.getElementById("keywords");
    if(getValue.value=="")
    {
        alert('请输入关键字!');
        return false;
    }
    else
    {
       window.location.href="SysLabel_List.aspx?s=1&keyword="+getValue.value+""; 
    }
}
</script>
