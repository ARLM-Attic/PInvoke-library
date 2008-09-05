<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_syslable_add" ResponseEncoding="utf-8" Codebehind="syslable_add.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
    <script type="text/javascript" src="../../editor/fckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px;height:30px;"><span id="adress"></span>添加标签 </td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="SysLabel_List.aspx" class="list_link">标签管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加标签</div></td>
        </tr>
      </table>


  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">标签名称</td>
      <td  style="width:90%" align="left"><span style="font-weight:bold;color:Red">{FS_</span><asp:TextBox ID="LabelName" runat="server" Width="100px" MaxLength="30"></asp:TextBox><span style="font-weight:bold;color:Red">}&nbsp;</span>
          <asp:DropDownList ID="LabelClass" runat="server" Width="195px">
          </asp:DropDownList><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_Labeladd_001',this)">帮助</span><span><asp:RequiredFieldValidator ID="RequireLabelName" runat="server" ControlToValidate="LabelName" Display="Dynamic" ErrorMessage="<span class=reshow>(*)请填写标签名称</spna>"></asp:RequiredFieldValidator></span><span></span>
           <span style="color:Red">注意：标签名称及标签内容严格区分大小写。</span>
           </td>
    </tr>  
    
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">插入内容</td>
      <td  style="width:90%" align="left" >
      <a href="javascript:show('List',document.getElementById('adress'),'选择标签参数:列表类(自定义)',700,420);"  class="list_link" title="此类标签包括以下样式的创建:&#13;·最新&#13;·推荐&#13;·热点&#13;·头条&#13;·滚动&#13;·专题&#13;·公告&#13;·子类">列表类</a>&nbsp;&nbsp;&nbsp;
      <a href="javascript:show('Ultimate',document.getElementById('adress'),'选择标签参数:终极类(自定义)',700,420);" class="list_link" title="包括新闻终极、专题终极">终极类</a>&nbsp;&nbsp;&nbsp;
      <a href="javascript:show('Browse',document.getElementById('adress'),'选择标签参数(浏览类)',700,420);" class="list_link" >浏览类</a>&nbsp;&nbsp;&nbsp;
      <a href="javascript:show('Routine',document.getElementById('adress'),'选择标签参数(常规类/高级扩展)',700,430);" class="list_link" title="包括新闻栏目子类、不规则新闻、位置导航、搜索、统计、幻灯片、站点地图、图片头条、相关新闻、归档、总站导航、栏目导航、专题导航、RSS、栏目调用、调用栏目信息(关键字、meta)、自定义页面、新闻排行、评论排行">常规&扩展类</a>&nbsp;&nbsp;&nbsp;
      <a href="javascript:show('Member',document.getElementById('adress'),'选择标签参数(会员类)',700,420);" class="list_link" title="包括用户登陆、用户排行、最新注册用户、投稿标签、讨论组">会员类</a>&nbsp;&nbsp;&nbsp;
      <a href="javascript:show('Other',document.getElementById('adress'),'选择标签参数(其他类)',700,200);" class="list_link" title="包括自由标签、自由JS、系统JS、广告JS、统计JS、其他JS、友情连接">其他类</a>&nbsp;&nbsp;&nbsp;
    </tr>    
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">标签内容</td>
      <td  style="width:90%" align="left" >
        <script type="text/javascript" language="JavaScript">
        window.onload = function()
	        {
	        var sBasePath = "../../editor/"
            var oFCKeditor = new FCKeditor('ContentTextBox') ;
            oFCKeditor.BasePath	= sBasePath ;
            oFCKeditor.Width = '100%' ;
            oFCKeditor.ToolbarSet = 'Foosun_style';
            oFCKeditor.Height = '300px' ;	
            oFCKeditor.ReplaceTextarea() ;
            }
        </script>
		<textarea rows="1" cols="1" name="ContentTextBox" style="display:none" id="ContentTextBox" runat="server" ></textarea>
      </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">标签描述</td>
      <td  style="width:90%" align="left">
          <asp:TextBox ID="LabelDescription" runat="server" Height="30px" TextMode="MultiLine" Width="400px" MaxLength="200"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_Labeladd_003',this)">帮助</span></td>
    </tr>
    
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">放入备份库</td>
      <td  style="width:90%" align="left">
          <asp:RadioButtonList ID="LabelBack" runat="server" RepeatDirection="Horizontal"
              RepeatLayout="Flow" Width="200px">
              <asp:ListItem Value="1">是</asp:ListItem>
              <asp:ListItem Value="0" Selected="true">否</asp:ListItem>
          </asp:RadioButtonList>
     </td>
    </tr>
     <tr class="TR_BG_list">
      <td align="left" class="navi_link" style="width:10%;text-align:center;" colspan="2">
         <asp:Button ID="Button1" runat="server" Text=" 保 存 " CssClass="form" OnClick="Button1_Click" />&nbsp;&nbsp;<input type="reset" name="UnDo" value=" 重 填 " class="form" />
        </td>
    </tr>    
    </table>      

      <br />
     <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
      <tr>
        <td align="center"><%Response.Write(CopyRight);%></td>
      </tr>
    </table>
   </form>
</body>
</html>
<script language="javascript" type="text/javascript">
function getValue(value)
{
    var oEditor = FCKeditorAPI.GetInstance("ContentTextBox");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml(value);
    }else
    {
    return false;
    }
    document.getElementById("LabelDivid").style.display="none";
}
</script>