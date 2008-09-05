<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_js_JS_Files" Codebehind="JS_Files.aspx.cs" %>
<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<base target="_self">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
<!--
function RemoveJS(id)
{
    if(window.confirm('确定要移除JS对该新闻的调用吗?'))
    {
        var  options={
            method:'post',
            parameters:"Option=RemoveJS&ID="+ id,
            onComplete:
                function(transport)
	            {
	                var retv =transport.responseText;
	                OnRemove(retv);
	            }
	        }
	    new  Ajax.Request('JS_Files.aspx',options);
    }
}
function OnRemove(retv)
{
    var n = retv.indexOf('%');
    alert(retv.substr(n+1,retv.length-n-1));
    if(parseInt(retv.substr(0,n)) > 0)
    {
        window.opener.window.execScript("__doPostBack('PageNavigator1$LnkBtnGoto','')","javascript");
        __doPostBack('PageNavigator1$LnkBtnGoto','');
    }
}

//批量删除
function DeleteJS(n)
{
    var ids = n;
    if(n < 0)
    {
        ids = GetSelected();
        if(ids == '')
        {
            alert('您没有选中任何JS');
            return;
        }
    }
    if(window.confirm("您确认要删除选中的JS吗?数据将不可恢复!"))
    {
        var  options={
        method:'post',
        parameters:"Option=RemoveAllJS&idList="+ ids,
        onComplete:
            function(transport)
            {
                var retv =transport.responseText;
                OnDelete(retv);
            }
        }
        new  Ajax.Request('JS_Files.aspx',options);
    }
}
function GetSelected()
{
    var ret = '';
    var tab = document.getElementById('TabData');
    for(var i = 1;i<tab.rows.length;i++)
    {
        var td = tab.rows[i].cells[1];
        for(var j = 0;j<td.childNodes.length;j++)
        {
            var obj = td.childNodes[j];
            if(obj.type == 'checkbox')
            {
                if(obj.checked)
                {
                    if(ret != '')
                        ret += ',';
                    ret += obj.value;
                }
                break;
            }
        }
    }
    return ret;
}
function OnDelete(retv)
{
    var n = retv.indexOf('%');
    alert(retv.substr(n+1,retv.length-n-1));
    if(parseInt(retv.substr(0,n)) > 0)
    {
        __doPostBack('PageNavigator1$LnkBtnGoto','');
    }
}
//-->
</script>
</head>

<body>
    <form id="form1" runat="server">
   <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td class="sysmain_navi"  style="PADDING-LEFT: 14px; width: 54%;" >JS调用新闻列表</td>
      <td width="43%" class="topnavichar"  style="PADDING-LEFT: 14px" >位置导航：<a target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />JS新闻列表</td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
    <tr>
      <td>功能： <a href="javascript:self.close();" class="topnavichar">关闭</a>
      &nbsp;┊&nbsp;
      <a href="javascript:DeleteJS(-1);" class="topnavichar">删除选中</a>
      </td>
    </tr>
  </table>
  <asp:Repeater ID="RptData" runat="server">
    <HeaderTemplate>
      <table width="98%" id="TabData" border="0" align="center" cellpadding="4" cellspacing="1"class="table">
      <tr class="TR_BG">
        <td width="90%" align="center" class="sys_topBg">新闻标题</td>
        <td width="10%" align="center" class="sys_topBg">操作<input type="checkbox" id="js_checkbox" value="-1" name="js_checkbox" onclick="javascript:return selectAll(this.form,this.checked);"/></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list">
        <td class="list_link"><%# DataBinder.Eval(Container.DataItem, "Njf_title")%></td>
        <td class="list_link" align="center"><a href="javascript:RemoveJS(<%# DataBinder.Eval(Container.DataItem, "ID")%>);" class="list_link"><img src="../../sysImages/folder/dels.gif" border="0" alt="删除JS调用" /></a><input type="checkbox" name="checkbox" value="<%# DataBinder.Eval(Container.DataItem, "ID")%>"/></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      </table>
    </FooterTemplate>
  </asp:Repeater>
  <div align="right" style="width:98%">
    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
  </div>
    <br />
<asp:HiddenField id="HidJsID" runat="server" />
</form>
</body>
</html>
