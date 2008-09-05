<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_style_edit" ResponseEncoding="utf-8" Codebehind="style_edit.aspx.cs" %>
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
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" height="30">样式管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="style.aspx" class="list_link">样式管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改样式</div></td>
        </tr>
      </table>
      <table border="0" cellpadding="5" cellspacing="1" class="Navitable" style="width: 100%">
    <tr class="TR_BG_list">
    <td style="padding-left:14px;">
      <span style="cursor:pointer;width:100px;" id="TD_putongstyle"  onclick="javascript:ChangeDiv('putongstyle')">普通样式</span>&nbsp;┊ &nbsp;
      <span style="cursor:pointer;width:100px;" id="TD_denglustyle" onclick="javascript:ChangeDiv('denglustyle')">登陆样式</span>
    </td>
    </tr>
    </table> 
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">样式名称</td>
      <td width="90%" align="left"><asp:TextBox ID="styleName" runat="server" Width="195px" MaxLength="30"></asp:TextBox>
          <asp:DropDownList ID="styleClass" runat="server" Width="195px">
          </asp:DropDownList><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_styleadd_001',this)">帮助</span><span><asp:RequiredFieldValidator ID="RequirestyleName" runat="server" ControlToValidate="styleName" Display="Dynamic" ErrorMessage="<span class=reshow>(*)请填写样式名称</spna>"></asp:RequiredFieldValidator></span><span><asp:RequiredFieldValidator ID="RequiredFieldstyleClass" runat="server" ControlToValidate="styleClass" Display="Dynamic" ErrorMessage="<span class=reshow>(*)请选择分类</spna>"></asp:RequiredFieldValidator></span>
        </td>
    </tr>
    <tr class="TR_BG_list" id="TR_putongstyle">
      <td align="center" class="navi_link" style="width: 13%">插入内容<label id="picContentTF"></label></td>
      <td width="90%" align="left" >
          <label id="style_base" runat="server" />
          <label id="style_class" runat="server" />
          <label id="style_special" runat="server" />                 
          <asp:DropDownList CssClass="form" ID="define" runat="server" Width="150px" onchange="javascript:setValue(this.value);">
          <asp:ListItem Value="">自定义字段</asp:ListItem>
          </asp:DropDownList>  
          <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_styleadd_002',this)">帮助</span></td>
      
      
    </tr>
      <tr class="TR_BG_list" id="TR_denglustyle" style="display:none;">
      <td align="center" class="navi_link" style="width: 13%">插入内容<label id="Label1"></label></td>
      <td width="90%" align="left" >               
          <asp:DropDownList CssClass="form" ID="dengluqian" runat="server" width="150px" onchange="javascript:getValue(this.value);">
          <asp:ListItem Value="">选择登陆前显示字段</asp:ListItem>
          <asp:ListItem Value="{#Login_Name}">用户名输入框(必选)</asp:ListItem>
          <asp:ListItem Value="{#Login_Password}">密码输入框(必选)</asp:ListItem>
          <asp:ListItem Value="{#Login_Submit}">登陆提交按钮(必选)</asp:ListItem>
          <asp:ListItem Value="{#Login_Reset}">登陆取消按钮</asp:ListItem>
          <asp:ListItem Value="{#Reg_LinkUrl}">注册新用户连接</asp:ListItem>
          <asp:ListItem Value="{#Get_PassLink}">取回密码连接</asp:ListItem>
          </asp:DropDownList>┊
          <asp:DropDownList CssClass="form" ID="dengluhou" runat="server" width="150px" onchange="javascript:getValue(this.value);">
          <asp:ListItem Value="">选择登陆后显示字段</asp:ListItem>
          <asp:ListItem Value="{#User_Name}">会员姓名</asp:ListItem>
          <asp:ListItem Value="{#User_HomePage}">会员主页</asp:ListItem>
          <asp:ListItem Value="{#User_DiscussGroup}">讨论组连接</asp:ListItem>
          <asp:ListItem Value="{#User_AdminCenter}">控制面版连接</asp:ListItem>
          <asp:ListItem Value="{#User_logout}">退出连接</asp:ListItem>
          </asp:DropDownList>
          <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_styleadd_002',this)">帮助</span></td>
       
    </tr>     
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">样式内容
      <div><a style="cursor:pointer;" onclick="selectFile('picEdit',document.getElementById('picContentTF'),320,500);" title="在上传的时候，请在编辑区鼠标点击，设置要上传图片的位置。"><font color="blue">选择图片</font></a></div>
      </td>
      <td width="90%" align="left" >
        <script type="text/javascript" language="JavaScript">
        window.onload = function()
            {
            var sBasePath = "../../editor/"
            var oFCKeditor = new FCKeditor('ContentTextBox') ;
            oFCKeditor.BasePath	= sBasePath ;
            oFCKeditor.Width = '100%' ;
            oFCKeditor.ToolbarSet = 'Foosun_style';
            oFCKeditor.Height = '250px' ;	
            oFCKeditor.ReplaceTextarea() ;
            }
        </script>
      <textarea rows="1" cols="1" name="ContentTextBox" runat="server" style="display:none" id="ContentTextBox" ></textarea>
      </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">样式描述</td>
      <td width="90%" align="left">
      <asp:TextBox ID="Description" runat="server" Height="50px" TextMode="MultiLine" Width="400px" MaxLength="200"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_styleadd_003',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td class="navi_link" style="width:10%;text-align:center;" colspan="2"><label>
        <asp:Button ID="Button1" runat="server" Text="保存" OnClick="Button1_Click" />
        </label>
        <label>
        <input type="reset" name="UnDo" value=" 重 填 " class="form" />
            <asp:HiddenField ID="styleID" runat="server" />
        </label></td>
    </tr>    
    </table>            
      <br />
      <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
        <tr>
          <td align="center"><label id="copyright" runat="server" /></td>
        </tr>
      </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
function insertHTMLEdit(url)
{
    var urls = url.replace('{@dirfile}','<% Response.Write(Foosun.Config.UIConfig.dirFile); %>')
    var oEditor = FCKeditorAPI.GetInstance("ContentTextBox");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml('<img src=\"'+urls+'\" border=\"0\" />');
    }
    else
    {
        return false;
    }
    return;
}

function initeditor()
{
    var str=document.getElementById("ContentTextBox").value;
    //alert(str);
    var teststr="{#Login_Name}*{#Login_Password}*{#Login_Submit}*{#Login_Reset}*{#Reg_LinkUrl}*{#Get_PassLink}*{#User_Name}*{#User_HomePage}*{#User_DiscussGroup}*{#User_AdminCenter}*{#User_logout}";
    var strArr=teststr.split("*");
    for(var i=0;i<strArr.length;i++)
    {
        if(str.indexOf(strArr[i])!=-1)
        {
            ChangeDiv("denglustyle");
            break;
        }
    }
}
initeditor();

function ChangeDiv(ID)
{
	document.getElementById("TD_putongstyle").className='';
	document.getElementById('TD_denglustyle').className='';
	document.getElementById('TD_'+ID).className='reshow';

	document.getElementById("TR_putongstyle").style.display="none";
	document.getElementById("TR_denglustyle").style.display="none";
	document.getElementById("TR_"+ID).style.display="";
}

function getValue(value)
{
    var oEditor = FCKeditorAPI.GetInstance("ContentTextBox");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml(value);
    }
    else
    {
    return false;
    }
}
function setValue(value)
{
    var oEditor = FCKeditorAPI.GetInstance("ContentTextBox");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml('{#FS:define='+value+'}');
    }
    else
    {
    return false;
    }
}
function UpFile(path)
    {
        var WWidth = (window.screen.width-500)/2;
        var Wheight = (window.screen.height-150)/2;
        window.open("../../configuration/system/Upload.aspx?Path="+path+"&upfiletype=files", '文件上传', 'height=200, width=500, top='+Wheight+', left='+WWidth+', toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); 
    }
</script>
