<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Collect_StepFive.aspx.cs" Inherits="Foosun.Web.manage.collect.Collect_StepFive" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>FoosunCMS For .NET v1.0.0</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet"
        type="text/css" />

    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" charset="utf-8" src="../../configuration/js/Public.js"></script>

    <script language="javascript" type="text/javascript">
    <!--
    /*function ChangeSet(obj)
    {
        var td2 = obj.parentNode.parentNode.lastChild;
        if(obj.checked)
        {
            td2.firstChild.style.display = 'none';
            td2.childNodes[1].style.display = '';
        }
        else
        {
            td2.firstChild.style.display = '';
            td2.childNodes[1].style.display = 'none';
        }
    }
    function ChangePage()
    {
        var flag;
        if(document.getElementById('RadPageNone').checked)
            flag = 0;
        else if(document.getElementById('RadPageOther').checked)
            flag = 1;
        else if(document.getElementById('RadPageCode').checked)
            flag = 2;
        var tr1 = document.getElementById("Tr_PageOther");
        var tr2 = document.getElementById("Tr_PageCode");
        switch(flag)
        {
            case 0:
                tr1.style.display = 'none';
                tr2.style.display = 'none';
                break;
            case 1:
                tr1.style.display = '';
                tr2.style.display = 'none';
                break;
            case 2:
                tr1.style.display = 'none';
                tr2.style.display = '';
                break;
        }
    }
    function ChangeDiv(ID)
    {
	    document.getElementById('td_baseinfo').className='m_up_bg';
	    document.getElementById('td_preview').className='m_up_bg';
        document.getElementById('tr_baseinfo').style.display='none';
        document.getElementById('tr_preview').style.display='none';
        document.getElementById('td_'+ ID).className='m_down_bg';
        document.getElementById('tr_'+ ID).style.display='';
	}
	function ChangeUrl(obj)
	{
	    var url = obj.options[obj.selectedIndex].value;
	    var frm = document.getElementById("PreviewArea");
	    frm.src = url;
    }*/
	function StepBack()
	{
	    history.back();
	}
	/*function LoadMe(flag)
	{
	    ChangeDiv('baseinfo');
        ChangeSet(document.getElementById('ChbAuthor'));
        ChangeSet(document.getElementById('ChbSource'));
        ChangeSet(document.getElementById('ChbTime'));
        ChangePage();
    }*/
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
                            位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt=""
                                src="../../sysImages/folder/navidot.gif" border="0" /><a href="Collect_List.aspx" target="sys_main" class="list_link">站点设置</a><img alt="" src="../../sysImages/folder/navidot.gif"
                                        border="0" />设置向导
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td style="height: 24px">
                        功能：<a href="Collect_List.aspx" class="list_link">站点列表</a>&nbsp;┊&nbsp;<a class="topnavichar"
                            href="Collect_RuleList.aspx">关键字过滤</a>&nbsp;┊&nbsp;<a class="topnavichar" href="Collect_News.aspx">新闻处理</a></td>
                </tr>
            </table>
            <table border="0" cellpadding="0" align="center" cellspacing="0" width="98%" class="toptable">
                <tr class="TR_BG_list">
                    <td width="30%" colspan="3" class="m_up_bg"
                         align="center">釆集结果预览
                        </td>
                </tr>
                <tr class="TR_BG_list" id="tr_baseinfo">
                    <td colspan="3" valign="top" align="center" class="list_link">
                        作者:<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        &nbsp; 时间:<asp:Label ID="Label2"
                            runat="server" Text=""></asp:Label>
                        &nbsp; 来源:<asp:Label ID="Label3" runat="server"
                                Text=""></asp:Label><br />
                        <div runat="server" id="showContextDiv" style="width:90%"></div></td>
                </tr>
                <tr class="TR_BG_list" id="tr_preview">
                    <td colspan="3" valign="top" class="list_link">
                    </td>
                </tr>
                <tr class="TR_BG_list">
                    <td class="list_link" align="center" colspan="3">
                        &nbsp;<input type="button" value="上一步" class="form" onclick="StepBack()" id="Button1" />&nbsp;
                        <input type="button" runat="server" class="form" value="完  成" id="Button2" onserverclick="Button2_ServerClick" /></td>
                </tr>
            </table>
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
