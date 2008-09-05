<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_unnews" Codebehind="unnews.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<style type="text/css">
.divstyle {
	top:62px;
	background:#FFFFE1 repeat-x left top;
	border:1px double #4F4F4F;
	width:97%;
	text-align:left;
	padding-left:8px;
	padding-top:8px;
	padding-bottom:12px;
	padding-right:8px;
	clip:rect(auto, auto, auto, auto);
	z-index:50;
	filter: progid:DXImageTransform.Microsoft.DropShadow(color=#B6B6B6,offX=2,offY=2,positives=true);

</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
            <td height="1" colspan="2"></td>
        </tr>
        <tr>
            <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px;">不规则新闻管理</td>
            <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px;"><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />不规则新闻管理</div></td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
            <td>功能: <a href="unnews_Edit.aspx" class="topnavichar">添加不规则新闻</a></td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1">
    <tr>
    <td>
    <div id="reviewopld" class="divstyle" style="text-align:left;display:none;">
    <span id="getReviewContent">加载预览中....</span>
        <div style="text-align:right;"><a href="javascript:void(0);" onclick="colsed();" style="color:Red;">关闭</a></div>
    </div>
    </td>
    </tr>
    </table>
    <script language="javascript" type="text/javascript">
    function getReviw(id)
    {
        var gid = document.getElementById("reviewopld");
        document.getElementById("getReviewContent").innerHTML='加载预览中...';
        gid.style.display="";
        var Action='UnID='+id;   var options={ 
                          method:'get', 
                          parameters:Action, 
                          onComplete:function(transport) 
                          { 
                              var returnvalue=transport.responseText; 
                              if (returnvalue.indexOf("??")>-1) 
                                  document.getElementById("getReviewContent").innerHTML='加载失败...'; 
                              else 
                                  document.getElementById("getReviewContent").innerHTML=returnvalue; 
                          } 
                       }; 
           new  Ajax.Request('../../configuration/system/reviewUnNews.aspx?no-cache='+Math.random(),options);
      }
      function colsed()
      {
         var gid = document.getElementById("reviewopld");
         gid.style.display="none";
      }
    </script>
    <table id="tablist" width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG">
            <td class="sys_topBg" width="30%" align="center">新闻标题</td>
            <td class="sys_topBg" width="29%" align="center">操作</td>
        </tr>
        <asp:Repeater ID="RptunNews" runat="server">
        <ItemTemplate>
        <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
            <td><%# DataBinder.Eval(Container.DataItem, "UnName")%></td>
            <td><button class="form" onclick="window.location.href='unNews_Edit.aspx?UnID=<%# DataBinder.Eval(Container.DataItem, "UnID")%>';return false;">修改</button>&nbsp;<button class="form" onclick="if(confirm('确定要删除吗？')){window.location.href='unnews_Del.aspx?UnID=<%# DataBinder.Eval(Container.DataItem, "UnID")%>';}return false;">删除</button>&nbsp;<button class="form" onclick="getReviw('<%# DataBinder.Eval(Container.DataItem, "UnID")%>')">预览</button></td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
    </table>
    <div style="width:98%" align="right"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></div>
    <br />
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px;">
        <tr>
            <td align="center"><%Response.Write(CopyRight); %></td>
        </tr>
    </table>
    <asp:Label runat="server" ID="LblChoose" Visible="false" Text="" />
    </div>
    </form>
</body>
</html>
