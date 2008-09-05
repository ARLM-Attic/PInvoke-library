<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="gb2312" AutoEventWireup="true" Inherits="user_Requestinformation" Debug="true" EnableEventValidation="false" Codebehind="Requestinformation.aspx.cs" %>
<%@ Register Src="../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Public.js"></script>
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Prototype.js"></script>
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %></title>
<link href="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <form id="form1" name="form1" method="post" action="" runat="server">
   <asp:Repeater ID="DataList1" runat="server" >
    <HeaderTemplate>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG">
        <td class="sys_topBg" align="left">要添加您为好友的用户</td>
        <td class="sys_topBg" style="width:50%;">验证信息</td>
        <td class="sys_topBg">时间</td>
        <td class="sys_topBg">操作</td>
        </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td align="left"><%#((DataRowView)Container.DataItem)["QName"]%></td>
        <td style="width:50%;"><%#((DataRowView)Container.DataItem)["Content"]%></td>
        <td><%#((DataRowView)Container.DataItem)["datatime"]%></td>
        <td><%#((DataRowView)Container.DataItem)["ops"]%></td>
        </tr>
     </ItemTemplate>
     <FooterTemplate>
     </table>
     </FooterTemplate>
    </asp:Repeater>
        <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
        <tr><td align="right" style="width: 928px">
            <uc1:PageNavigator ID="PageNavigator1" runat="server" />
        </td></tr>
      </table>  
                
</form>
</body>
</html>
<script type="text/javascript" language="javascript">
function checkfriend(id)
{
    var WWidth = (window.screen.width-500)/2;
    var Wheight = (window.screen.height-150)/2;
    window.open ("RequestinformationResult.aspx?ID="+id+"", '处理好友验证信息', 'height=180, width=400, top='+Wheight+', left='+WWidth+', toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); 
}
</script>