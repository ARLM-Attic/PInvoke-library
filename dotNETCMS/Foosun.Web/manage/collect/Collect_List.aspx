<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_collect_Collect_List" Codebehind="Collect_List.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>FoosunCMS For .NET v1.0.0</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Public.js"></script>
    <script language="javascript" type="text/javascript">
 <!--
 function Handle(op,tp,id)
 {
    var retype = '站点';
    var ob = 'Site';
    if(tp==0)
    {
        retype = '栏目';
        ob = 'Folder';
    }
    var opt = 'Delete';
    var confm = '您确定要删除该'+ retype +'吗?数据将不可恢复!';
    if(op == 0)
    {
        opt = 'Reproduce';
        confm = '您确定要复制该'+ retype +'吗?';
    }
    
    if(window.confirm(confm))
    {
        var param = 'Option='+ opt + ob +'&ID='+ id;
        SendAjax(param);
    }
 }
 function SendAjax(param)
 {
    var options={
    method:'post',
    parameters:param,
    onComplete:
    function(transport)
	    {
	        var retv=transport.responseText;
		    OnRecv(retv);
        }
    }
    new  Ajax.Request('Collect_List.aspx',options);
 }
 function OnRecv(retv)
 {
    var n = retv.indexOf('%');
    alert(retv.substr(n+1,retv.length-n-1));
    if(parseInt(retv.substr(0,n)) > 0)
    {
        __doPostBack('PageNavigator1$LnkBtnGoto','');
    }
 }
 function Collect(id,flag)
 {
    if(flag != '有效')
    {
        alert('该站点参数没有设置完成,不能采集!\r请点击"向导"设置参数...');
        return;
    }
    
    var WWidth = (window.screen.width-500)/2;
    var Wheight = (window.screen.height-150)/2;
    document.getElementById('HidClNum').value = 0;
    window.open('Collect_NumSet.aspx?id='+ id,'采集设置','status=0,directories=0,resizable=0,top='+Wheight+', left='+WWidth+',toolbar=0,location=0,scrollbars=0,width=360px,height=165px');
 }
 //-->
 </script>
</head>
<body>
    <form id="Form2" runat="server">
        <div>
            <table id="top1" width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
                <tr>
                    <td height="1" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td width="57%" class="sysmain_navi" style="padding-left: 14px">
                        采集系统</td>
                    <td width="43%" class="topnavichar" style="padding-left: 14px">
                        <div align="left">
                            位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />站点设置
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td>
                        功能：<a class="topnavichar" href="Collect_Add.aspx?Type=Folder">新建栏目</a>&nbsp;┊&nbsp;<a class="topnavichar" href="Collect_Add.aspx?Type=Site">新建站点</a>&nbsp;┊&nbsp;<a class="topnavichar" href="Collect_RuleList.aspx">关键字过滤</a>&nbsp;┊&nbsp;<a class="topnavichar" href="Collect_News.aspx">新闻处理</a></td>
                </tr>
            </table>
           <asp:Repeater runat="server" ID="RptSite" OnItemDataBound="RptSite_ItemDataBound">
 <HeaderTemplate>
<table id="tablist" width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
  <tr class="TR_BG">
    <td class="sys_topBg" width="45%" align="center">名 称</td>
    <td class="sys_topBg" width="10%" align="center">状态</td>
    <td class="sys_topBg" width="15%" align="center">采集对象页</td>
    <td class="sys_topBg" width="30%" align="center">操 作</td>
  </tr>
  <asp:Panel runat="server" ID="PnlUpFolder">
  <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
    <td class="list_link"><asp:LinkButton runat="server" ID="LnkBtnUp" OnClick="LnkBtnUp_Click" CssClass="list_link"><img src="../../sysImages/folder/folderup.gif" border="0" alt="返回上一级" /> 返回上一级</asp:LinkButton> </td>
    <td class="list_link"></td>
    <td class="list_link"></td>
    <td class="list_link"></td>
  </tr>
  </asp:Panel>
  </HeaderTemplate>
  <ItemTemplate>
<tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
    <td class="list_link"><asp:Image runat="server" ID="ImgRowCaption" AlternateText='<%# DataBinder.Eval(Container.DataItem, "TP")%>' /> <%# DataBinder.Eval(Container.DataItem, "SName")%></td>
    <td class="list_link" align="center"><asp:Label runat="server" ID="LblState" Text='<%# DataBinder.Eval(Container.DataItem, "LockState")%>' /></td>
    <td class="list_link" align="center"><a href="<%# DataBinder.Eval(Container.DataItem, "objURL")%>" target="_blank"><asp:Image runat="server" ID="ImgLink" ImageUrl="../../sysImages/folder/objpage.gif" border="0" AlternateText='<%# DataBinder.Eval(Container.DataItem, "TP")%>'/></a></td>
    <td class="list_link" align="center">
        <asp:Panel runat="server" ID="PnlFolder" Visible='<%# ((int)(DataBinder.Eval(Container.DataItem, "TP"))==0)%>'>
            <asp:LinkButton ID="LnkBtnEnter" runat="server" CssClass="list_link" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>' OnClick="LnkBtnEnter_Click" Text="进入"/>
            &nbsp;┊&nbsp;
            <a href="javascript:Handle(0,0,<%# DataBinder.Eval(Container.DataItem, "ID")%>);" class="list_link">复制</a>
            &nbsp;┊&nbsp;
            <a href="Collect_Add.aspx?Type=Folder&ID=<%# DataBinder.Eval(Container.DataItem, "ID")%>" class="list_link">属性</a>
            &nbsp;┊&nbsp;
            <a href="javascript:Handle(1,0,<%# DataBinder.Eval(Container.DataItem, "ID")%>);" class="list_link">删除</a>
        </asp:Panel>
        <asp:Panel runat="server" ID="PnlSite" Visible='<%# ((int)(DataBinder.Eval(Container.DataItem, "TP"))==1)%>'>
            <a href="javascript:Handle(0,1,<%# DataBinder.Eval(Container.DataItem, "ID")%>);" class="list_link">复制</a>&nbsp;┊&nbsp;<a href="Collect_Add.aspx?Type=Site&ID=<%# DataBinder.Eval(Container.DataItem, "ID")%>" class="list_link">向导</a>&nbsp;┊&nbsp;<a href="javascript:Handle(1,1,<%# DataBinder.Eval(Container.DataItem, "ID")%>);" class="list_link">删除</a>&nbsp;┊&nbsp;<a href="javascript:Collect(<%# DataBinder.Eval(Container.DataItem, "ID")%>,'<%# DataBinder.Eval(Container.DataItem, "LockState")%>');" class="list_link">采集</a>
        </asp:Panel>
    </td>
  </tr>
 </ItemTemplate>
 <FooterTemplate>
  </table>
  </FooterTemplate>
</asp:Repeater>
 <asp:HiddenField runat="server" ID="HidFolderID" Value="" />
 <input type="hidden" id="HidClNum" value="0" />
  <div align="right" style="width:98%"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></div>
            <br />
            <br />
            <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
                style="height: 76px">
                <tr>
                    <td align="center">
                        <%Response.Write(CopyRight);%>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>