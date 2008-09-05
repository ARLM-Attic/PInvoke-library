<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CustomForm_Data.aspx.cs" Inherits="Foosun.Web.manage.Sys.CustomForm_Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" language="javascript">
<!--
function TruncateTb(id,nm)
{
    if(window.confirm('你确定要清空数据表:'+ nm +' 吗?数据将不能被恢复!'))
    {
        var options={
            method:'post',
            parameters:"Option=TruncateTb&ID="+ id,
            onComplete:
                function(transport)
	            {
	                var retv =transport.responseText;
	                OnRcvMsg(retv);
	            } 
	    }
	    new  Ajax.Request('CustomForm_Data.aspx',options);
    }
}
function OnRcvMsg(rtstr)
{
   var n = rtstr.indexOf("%");
   alert(rtstr.substr(n+1,rtstr.length-n-1));
   if(parseInt(rtstr.substr(0,n)) > 0)
   {
      location.reload();
   }
}
//-->
</script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px; height: 32px;">
                    自定义表单管理<span class="helpstyle" style="cursor: hand;" title="点击查看帮助" onclick="Help('',this)">(帮助)</span></td>
                <td width="43%" class="topnavichar" style="padding-left: 14px; height: 32px;">
                    <div align="left">
                        位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" /><a href="CustomForm.aspx" class="topnavichar">自定义表单</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" /><asp:Label runat="server" ID="LblName"></asp:Label><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" />提交数据</div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
                <td>
                   &nbsp;&nbsp;<a href="javascript:TruncateTb(<%# formid%>,'<%# tablenm%>');" class="topnavichar">清空该表</a></td>
            </tr>
        </table>
        <div>
            <asp:GridView runat="server" ID="GrdData" CellPadding="4" GridLines="None" Width="98%" CssClass="table">
                <FooterStyle  CssClass="TR_BG" />
                <RowStyle CssClass="TR_BG_list" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle CssClass="TR_BG_list"/>
                <HeaderStyle CssClass="TR_BG" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
        <br />
        <br />
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
            style="height: 76px">
            <tr>
                <td align="center">
                    <%Response.Write(CopyRight); %>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
