<%@ Page Language="C#" ContentType="text/html" AutoEventWireup="true" Inherits="user_Constr_up" Debug="true" Codebehind="Constr_up.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body onload="DispChange('<%= ConstrTF %>')"><form id="form1" name="form1" method="post" action="" runat="server"> 
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >文章管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Constrlist.aspx" class="menulist">文章管理</a></div>
      </td>
    </tr>
</table>
      <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;">          
          <a href="Constr.aspx" class="menulist">发表文章</a>&nbsp; &nbsp;<a href="Constrlistpass.aspx" class="topnavichar" >所有退稿</a>&nbsp; &nbsp;<a href="Constrlist.aspx" class="menulist">文章管理</a>&nbsp; &nbsp;<a href="ConstrClass.aspx" class="menulist">分类管理</a>&nbsp; &nbsp;<a href="Constraccount.aspx" class="menulist">账号管理</a>
          </td>
        </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right; width: 110px;">
        文章名称：</td>
      <td class="list_link" colspan="5">
        <asp:TextBox ID="Title" runat="server" Width="325px" CssClass="form" MaxLength="100"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constr_0001',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Title" ErrorMessage="请输入分类名称"></asp:RequiredFieldValidator>
      </td>   
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right; width: 110px;">
        文章内容：</td>
      <td class="list_link" colspan="5" style="width: 750px;height:250px;">
        <script type="text/javascript" language="JavaScript">
             window.onload = function()
	        {
	        var sBasePath = "../../editor/"
            var oFCKeditor = new FCKeditor('Contentbox') ;
            oFCKeditor.BasePath	= sBasePath ;
            oFCKeditor.ToolbarSet = 'Foosun_User';
            oFCKeditor.Width = '100%' ;
            oFCKeditor.Height = '350' ;	
            oFCKeditor.ReplaceTextarea() ;
            }
        </script>
		<textarea rows="1" cols="1" name="Contentbox" style="display:none" id="Contentbox" runat="server" ></textarea>
      </td>
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right; width: 110px;"> 作 者：</td>
        <td class="list_link" colspan="5">
            <asp:TextBox ID="Author" runat="server"  CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constr_0003',this)">帮助</span> &nbsp; &nbsp; &nbsp; 关 键 字：<asp:TextBox ID="Tags" runat="server"  CssClass="form"></asp:TextBox><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constr_0019',this)">帮助</span> &nbsp; &nbsp; &nbsp; 类 型：<asp:DropDownList ID="lxList1" runat="server" Width="146px" CssClass="form">
            <asp:ListItem>原创</asp:ListItem>
            <asp:ListItem>转载</asp:ListItem>
            <asp:ListItem>代理</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constr_0008',this)">帮助</span>
        </td>
  </tr>
      <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right; width: 110px;"> 
                          信息级：</td>
          <td class="list_link" colspan="5" valign="top"><table border="0" cellpadding="0" cellspacing="0" Width="100%" height="100%">
                  <tr>
                      <td Width="7%">
        <asp:RadioButtonList ID="inList1" runat="server" RepeatDirection="Horizontal"
            Width="192px">
            <asp:ListItem Selected="True" Value="0">普通</asp:ListItem>
            <asp:ListItem Value="1">优先</asp:ListItem>
            <asp:ListItem Value="2">加急</asp:ListItem>
        </asp:RadioButtonList></td>
                      <td Width="18%" style="text-align: right;display:;" id="site1">
                          投稿到总站：</td>
                      <td Width="75%" style="display:;" id="site2">
        <asp:RadioButtonList ID="fbList1" runat="server" RepeatDirection="Horizontal" Width="103px">
            <asp:ListItem Value="1">是</asp:ListItem>
            <asp:ListItem Value="0">否</asp:ListItem>
        </asp:RadioButtonList></td>
                  </tr>
              </table>
          </td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right; width: 110px;">频道分类：</td>
       <td class="list_link" colspan="5">
        &nbsp;<asp:DropDownList ID="site" runat="server" Width="147px" CssClass="form" Enabled="false"></asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constr_0005',this)">帮助</span>
           &nbsp; &nbsp; &nbsp; 文章分类：<asp:DropDownList ID="ConstrClass" runat="server" Width="146px" CssClass="form"></asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constr_0006',this)">帮助</span>
       </td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right; width: 110px;">锁 定：</td>
       <td class="list_link" colspan="5" valign="top"><table border="0" cellpadding="0" cellspacing="0"  Width="100%" height="100%">
               <tr>
                   <td style="width: 3%">
        <asp:RadioButtonList ID="Locking" runat="server" RepeatDirection="Horizontal" Width="93px">
            <asp:ListItem Value="1">是</asp:ListItem>
            <asp:ListItem Value="0">否</asp:ListItem>
        </asp:RadioButtonList></td>
                   <td style="text-align: right; width: 15%;">
                       推 荐：</td>
                   <td Width="92%">
                       <asp:RadioButtonList ID="Recommendation" runat="server" RepeatDirection="Horizontal" Width="94px">
                            <asp:ListItem Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:RadioButtonList></td>
               </tr>
           </table>
       </td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link" style="height: 32px; text-align: right; width: 110px;">图 片：</td>
       <td class="list_link" colspan="5">
           <asp:TextBox ID="photo" runat="server" Width="265px"  CssClass="form"></asp:TextBox>
         <input  class="form" type="button" value="选择图片"  onclick="selectFile('user_pic',document.form1.photo,400,500);document.form1.photo.focus();" /><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_Constr_0011',this)">帮助</span> 
       </td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link" style="text-align: right; width: 110px;"></td>
       <td class="list_link" colspan="5">
        &nbsp;<asp:Button ID="Button1" runat="server" CssClass="form" Text="提 交" OnClick="Button1_Click" />
           &nbsp; &nbsp;&nbsp; &nbsp;<input type="reset" name="Submit3" value="重 置" class="form"></td>
  </tr>
 
</table>
<br />
<br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %> </div></td>
  </tr>
</table>
</form>
</body>
</html>
<script language="javascript">
function DispChange(contf)
{
    if(contf =="1")
    {
            document.getElementById("site1").style.display="";
            document.getElementById("site2").style.display="";
    }
}
</script>