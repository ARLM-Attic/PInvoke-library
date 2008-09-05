<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_constr_Constraccount" Debug="true" Codebehind="Constraccount.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript">
function Constrclass(sa)
    {
        switch(sa)
        {
            case 0://参数设置
            document.getElementById("all").style.display="";
            document.getElementById("insert").style.display="none";
            break;
            case 1://参数设置
            document.getElementById("all").style.display="none";
            document.getElementById("insert").style.display="";
            break;
         }
     }
</script>
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td colspan="2" style="height: 1px"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >账号管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Constrlist.aspx" class="menulist">文章管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />账号管理</div>
          </td>
        </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;">
          <a href="Constr.aspx" class="menulist">发表文章</a>&nbsp; &nbsp;<a href="Constrlistpass.aspx" class="topnavichar" >所有退稿</a>&nbsp; &nbsp;<a href="Constrlist.aspx" class="menulist">文章管理</a>&nbsp; &nbsp;<a href="ConstrClass.aspx" class="menulist">分类管理</a>&nbsp; &nbsp;<a href="Constraccount.aspx" class="menulist">账号管理</a>&nbsp; &nbsp;<a href="Constraccount_add.aspx" class="menulist">添加账号</a>
          </td>
          <td align="right" style="padding-right:28px;"><span id="addcount" runat="server"></span></td>
        </tr>
</table>
<div id="no" runat="server"></div>
    <asp:Repeater ID="DataList1" runat="server" >
    <HeaderTemplate>
    <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
    <tr class="TR_BG">
    <td class="sys_topBg" align="center" width="15%">开户名</td>
    <td class="sys_topBg" align="center" width="25%">开户银行</td>
    <td class="sys_topBg" align="center" width="25%">银行账号</td>
    <td class="sys_topBg" align="center" width="15%">卡号</td>
    <td class="sys_topBg" align="center" width="20%">操作</td>
    </tr>
    <div id="tnzlist" runat="server"></div>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)[5]%></td>
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)[2]%></td>
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)[3]%></td>
        <td align="center" width="15%"><%#((DataRowView)Container.DataItem)[4]%></td>
        <td align="center" width="40%"><%#((DataRowView)Container.DataItem)[6]%></td>            
        </tr>
     </ItemTemplate>
     <FooterTemplate>
     </table>
     </FooterTemplate>
    </asp:Repeater>
<table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
<tr>
<td align="right" style="width: 928px"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></td>
</tr>
</table>
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
<script language="javascript" type="text/javascript">
function del(ID)
{
   if(confirm("你确定要删除吗?"))
   { 
        self.location="?Type=del&ID="+ID;
   }
}
</script>
</html>
