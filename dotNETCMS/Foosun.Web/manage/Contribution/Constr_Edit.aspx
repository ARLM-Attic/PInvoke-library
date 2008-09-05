<%@ Page Language="C#" ContentType="text/html" AutoEventWireup="true" Inherits="manage_Contribution_Constr_Edit" Debug="true" Codebehind="Constr_Edit.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
<title><%Response.Write(Foosun.Config.UIConfig.CssPath()); %></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />

</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >
              投稿管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />稿件管理</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
    <td width="46%" class="navi_link">&nbsp; &nbsp; &nbsp;<a href="Constr_List.aspx" class="topnavichar">稿件管理</a>&nbsp; &nbsp;<a href="Constr_Stat.aspx" class="topnavichar">稿件统计</a>&nbsp; &nbsp;<a href="paymentannals.aspx" class="topnavichar">支付历史</a>&nbsp; &nbsp;<a href="Constr_SetParam.aspx" class="topnavichar">稿酬设定</a>&nbsp; &nbsp;<a href="Constr_chicklist.aspx" class="topnavichar">所有通过审核稿件</a></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">

  <tr class="TR_BG_list">
    <td class="list_link" width="25%">
        稿件名称：</td>
    <td class="list_link" width="75%">
        <asp:TextBox ID="Title" runat="server" Width="325px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Title"
            ErrorMessage="请输入分类名称"></asp:RequiredFieldValidator></td>
  </tr>

  <tr class="TR_BG_list">
    <td class="list_link" valign="top">
        稿件内容：</td>
    <td class="list_link">
    <script type="text/javascript" language="JavaScript">
         window.onload = function()
        {
        var sBasePath = "../../editor/"
        var oFCKeditor = new FCKeditor('ContentBox') ;
        oFCKeditor.BasePath	= sBasePath ;
        oFCKeditor.ToolbarSet = 'Foosun_User';
        oFCKeditor.Width = '100%' ;
        oFCKeditor.Height = '250' ;	
        oFCKeditor.ReplaceTextarea() ;
        }
    </script>
	<textarea rows="1" cols="1" name="ContentBox" style="display:none" id="ContentBox" runat="server" ></textarea>
    </td>
  </tr>

    <tr class="TR_BG_list">
    <td class="list_link">作者：</td>
    <td class="list_link"><asp:Label ID="Author" runat="server" Width="160px"></asp:Label></td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link"> 关 键 字：</td>
    <td class="list_link"><asp:TextBox ID="Tags" runat="server"  CssClass="form"></asp:TextBox></td>
  </tr>
       <tr class="TR_BG_list">
    <td class="list_link" Width="15%">稿件复制到栏目：</td>
    <td class="list_link" Width="85%"><asp:TextBox ID="ClassCName" runat="server" Width="212px"></asp:TextBox>&nbsp;栏目:<span runat="server" id="getClassCname"></span>
        &nbsp;<input  class="form" type="button" value="选择栏目"  onclick="selectFile('newsclass',document.form1.ClassCName,300,500);document.form1.ClassCName.focus();" />
        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
            runat="server" ControlToValidate="ClassCName" ErrorMessage="栏目不能为空"></asp:RequiredFieldValidator></td>
   </tr>
   <tr class="TR_BG_list">
    <td class="list_link">请选择稿酬：</td>
    <td><asp:DropDownList ID="ParmConstr" runat="server" Width="146px"></asp:DropDownList></td>
</tr>
   <tr class="TR_BG_list">
    <td class="list_link"></td>
    <td class="list_link">
        &nbsp; &nbsp;
        <asp:Button ID="Button1" runat="server" Text="提 交" OnClick="Button1_Click" CssClass="form"/>
        &nbsp; &nbsp;<input type="reset" name="Submit3" value="重 置" class="form">
    </td>
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
<script language="javascript" type="text/javascript">
    new Form.Element.Observer($('ClassCName'),1,getClassCName);
    function getClassCName()
    {
        var strC=document.getElementById("ClassCName").value;
	    var  options={  
				       method:'get',  
				       parameters:"Type=Class&ClassID="+strC,  
				       onComplete:function(transport)
					    {  
						    var returnvalue=transport.responseText;
						    if (returnvalue.indexOf("??")>-1)
                               $('getClassCname').innerHTML="error!";
						    else
                               $('getClassCname').innerHTML=returnvalue;
					    }  
				       }; 
	    new  Ajax.Request('../../configuration/system/getClassCname.aspx?no-cache='+Math.random(),options);
    }  
</script>