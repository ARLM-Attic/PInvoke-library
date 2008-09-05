<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Templet_Txteditor" ResponseEncoding="utf-8" Codebehind="Txteditor.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
</head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="JavaScript" type="text/javascript" src="../../editor/editor.js"></script>
<body>
<form id="fromeditor" runat="server" method="post" action="">
  <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" height="30">文本编辑</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Manage_List.aspx" class="list_link">模板管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />文本编辑<asp:TextBox ID="FilePath"
            runat="server" Visible="False"></asp:TextBox></div></td>
    </tr>
  </table>
<table width="98%" align="center" cellpadding="5" border="0" cellspacing="1" class="table">
  <tr class="TR_BG_list">
    <td class="TR_BG_list"><span id="adress"></span><div>
        <span class="reshow">特殊页面标签：</span><asp:DropDownList ID="LabelList1" runat="server">
        <asp:ListItem Value="">=单页面标签=</asp:ListItem>
        <asp:ListItem Value="{#Page_Title}">页面标题</asp:ListItem>
        <asp:ListItem Value="{#Page_MetaKey}">meta关键字</asp:ListItem>
        <asp:ListItem Value="{#Page_MetaDesc}">meta描述</asp:ListItem>
        <asp:ListItem Value="{#Page_Split}">内容分页</asp:ListItem>
        <asp:ListItem Value="{#Page_Content}">内容</asp:ListItem>
        <asp:ListItem Value="{#Page_Navi}">导航</asp:ListItem>
        </asp:DropDownList>
        <input id="Button1" style="width:35px;" type="button" value="插入" onclick="javascript:getValue(document.fromeditor.LabelList1.value);" />
        <asp:DropDownList ID="history" runat="server">
        <asp:ListItem Value="">=归档页面标签=</asp:ListItem>
        <asp:ListItem Value="{#history_list}">列表</asp:ListItem>
        <asp:ListItem Value="{#history_PageTitle}">页面标题</asp:ListItem>
        </asp:DropDownList>
        <input id="Button3" style="width:35px;" type="button" value="插入" onclick="javascript:getValue(document.fromeditor.history.value);" />
        <asp:DropDownList ID="Search1" runat="server">
        <asp:ListItem Value="">=搜索页面标签=</asp:ListItem>
        <asp:ListItem Value="{#Page_SearchContent}">列表(标题/内容)</asp:ListItem>
        <asp:ListItem Value="{#Page_SearchPages}">分页</asp:ListItem>
        </asp:DropDownList>
        <input id="Button4" style="width:35px;" type="button" value="插入" onclick="javascript:getValue(document.fromeditor.Search1.value);" />
        <asp:DropDownList ID="Comm1" runat="server">
        <asp:ListItem Value="">=评论页面标签=</asp:ListItem>
        <asp:ListItem Value="{#Page_CommTitle}">评论内容[通用]</asp:ListItem>
        <asp:ListItem Value="{#Page_Commidea}">显示观点统计[通用]</asp:ListItem>
        <asp:ListItem Value="{#Page_CommPages}">分页[通用]</asp:ListItem>
        <asp:ListItem Value="{#Page_CommStat}">评论数据统计[通用]</asp:ListItem>
        <asp:ListItem Value="{#Page_PageTitle}">评论页面标题[评论独立列表]</asp:ListItem>
        <asp:ListItem Value="{#Page_PostComm}">发表评论[评论独立列表]</asp:ListItem>
        <asp:ListItem Value="{#Page_NewsURL}">新闻连接[评论独立列表]</asp:ListItem>
        </asp:DropDownList>
        <input id="Button5" style="width:35px;" type="button" value="插入" onclick="javascript:getValue(document.fromeditor.Comm1.value);" />
        <hr style="height:1px;color:#999999;border:dotted" />
        <div style="padding-top:1px;padding-bottom:5px;">
        <asp:DropDownList ID="LabelList" runat="server" Width="150px">
        </asp:DropDownList>
        <input id="sbutton1" style="width:35px;" type="button" value="插入" onclick="javascript:getValue(document.fromeditor.LabelList.value);" />
        <input id="sbutton2" style="width:120px;" type="button" value="系统标签(内置)" onclick="javascript:show('Label1',document.getElementById('adress'),'系统标签列表(点击选择)',600,380);" />
        <input id="sbutton3" style="width:170px;" type="button" value="动态栏目/专题标签(内置)" onclick="javascript:show('Labelm',document.getElementById('adress'),'动态栏目/专题标签(内置)列表(点击选择)',600,380);" />
        <input id="sbutton4" style="width:100px;color:Red;" type="button" value="自定义标签" onclick="javascript:show('Label',document.getElementById('adress'),'自定义标签列表(点击选择)',600,380);" />
        <input id="sbutton5" style="width:120px;" type="button" value="选择自由标签" onclick="javascript:show('freeLabel',document.getElementById('adress'),'自由标签列表(点击选择)',600,380);" />
        </div>
        </div>
        <input type="button" name="Submit" style="border:1px dotted #999999;padding:2px 0 0 0;background-color:#eeeeee;" runat="server" value="保存模板" id="Button7" onserverclick="Button2_Click" />	
        <input type="button" name="Submit" value=" 恢 复 "  style="border:1px dotted #999999;padding:2px 0 0 0;background-color:#eeeeee;" onclick="javascript:UnDo();" />                
        <span id="dirPath" runat="server" >
            </span>
        </td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link" style="padding-top:0;padding-left:0;padding-right:0;padding-bottom:0;text-align:center;">
        <!--编辑器开始-->
        <asp:TextBox ID="FileContent" runat="server" Width="98%" Height="350px" TextMode="MultiLine"></asp:TextBox><div id="test" runat="server"></div><!--编辑器结束--></td>
  </tr>
  <tr class="TR_BG_list">
  <td>
      站点目录：{$InstallDir}&nbsp;&nbsp;&nbsp;模板路径：{@dirTemplet}您可以直接在模板中插入此标签替代您的图片，CSS等目录.<br />
     自定义标签格式：{FS_xx}<br />
     (内置)系统标签：{FS_S_xx}<br />
     (内置)动态栏目标签：{FS_Class*_xx}(不调用子类)，{FS_Class*C_xx}(调用子类)，xx为栏目的ClassID <br />
     (内置)动态专题标签：{FS_Special*_xx}，xx为专题的SpecialID<br />
     自由标签：{FS_FREE_xx}
     <div class="reshow">特别注意：标签严格区分大小写</div>
  </td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link">
     <asp:Button ID="Button2" runat="server" Text="保存模板" OnClick="Button2_Click" />
	<input type="button" name="Submit" value=" 恢 复 " onclick="javascript:UnDo();" />
	&nbsp;&nbsp;<a style="color:Red;" onclick="{if(confirm('确定要切换到在线编辑吗?请在切换前先保存您的数据，否则会丢失!\n确定切换吗？')){return true;}return false;}" href="editor.aspx?dir=<%Response.Write(dir); %>&filename=<%Response.Write(filename); %>">切换到在线编辑器</a>
	</td>
  </tr>
  </table>
  <br />
  <br />
   <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
</table>
</form>
</body>
<script language="javascript" type="text/javascript">

function UnDo()
{
    if(confirm('你确定要取消所做的更改吗?'))
    {
        document.fromeditor.reset();
    }   
}
function getValue(value)
{
    if(value!="")
        insert(value);
}
</script>
</html>

