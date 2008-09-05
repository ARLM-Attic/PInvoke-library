<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="content.aspx.cs" Inherits="user_channel_content" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>频道</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.reshows{height:28px;background-color: #FFFFB5;TEXT-DECORATION: none;COLOR: #FF0000;}
</style>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
</head>
<body onload="getNewsInfo('baseinfo');">
   <form id="form1" runat="server">
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" class="sysmain_navi" style="padding-left:14px;">添加信息</td>
          <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="list.aspx?ChID=<%Response.Write(Request.QueryString["ChID"]); %>" class="list_link" target="sys_main">我的信息</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加信息</td>
        </tr>
    </table>
    <table width="98%"  border="0" cellpadding="3" align="center" cellspacing="0" class="table">
        <tr class="TR_BG_list">
          <td style="padding-left:14px;height:20px;width:70%;">
          <span id="A1" class="reshows" style="cursor:pointer" onclick="getNewsInfo('baseinfo');">基本内容</span>
          &nbsp; &nbsp; <span id="A2" class="list_link" style="cursor:pointer" onclick="getNewsInfo('definfo');">自定义属性</span>
          </td>
          <td>
            <span class="list_link"> 所属频道：<span id="channelName" runat="server" /></span>
          </td>
        </tr>
    </table>
    <!--自定义属性-->
    <div id="definfo" style="width:100%;display:none;" runat="server">
          loading...
     </div>

    <table width="98%"  border="0" id="baseinfo" cellpadding="3" align="center" cellspacing="1" class="table">
        <tr class="TR_BG_list">
          <td style="width:100px;text-align:right;">
          标题
          </td>
          <td>
            <asp:TextBox ID="title" CssClass="form" MaxLength="100" Width="260px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="title" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写标题</span>"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr class="TR_BG_list">
          <td style="width:100px;text-align:right;">
          栏目
          </td>
          <td>
        <asp:DropDownList ID="ClassID" runat="server">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ClassID" Display="Dynamic" ErrorMessage="<span class='reshow'>请选择栏目</span>"></asp:RequiredFieldValidator>
        </td>
      </tr>
        <tr class="TR_BG_list">
          <td style="width:100px;text-align:right;">
          图片地址
          </td>
          <td>
          <asp:TextBox ID="PicURL" runat="server" Width="50%" MaxLength="200" CssClass="form" onmouseover="javascript:ShowDivPic(this,document.form1.PicURL.value.toLowerCase().replace('{@dirfile}','files'),'.jpg',1);" onmouseout="javascript:hiddDivPic();"></asp:TextBox>
          <input  class="form" type="button" value="选择图片"  onclick="selectFile('user_pic',document.form1.PicURL,300,520);document.form1.PicURL.focus();" />
          </td>
          </tr>
         <tr class="TR_BG_list" id="div_naviContent" style="display:none;">
          <td style="width:100px;text-align:right;">导读</td>
          <td>
          <asp:TextBox ID="naviContent" runat="server" Width="80%" MaxLength="200" CssClass="form" Height="50px" TextMode="MultiLine"></asp:TextBox> <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_News_add_NaviContent',this)"> 帮助</span>
          </td>
        </tr>  
        <tr class="TR_BG_list">
          <td style="width:100px;text-align:right;" valign="top">
          内容
          <br /><asp:CheckBox ID="naviContentTF" runat="server" title="为内容设置导读" onclick="NaviClick(this);" Text="设置导读" /><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_News_add_naviContentTF',this)">帮助</span>
          <br /><br /><div align="center">缩放编辑区
          <br /><a href="javascript:ZoonEdit('300')" class="list_link" style="text-decoration:underline;">原始</a>&nbsp;&nbsp;<a class="list_link" style="text-decoration:underline;" href="javascript:ZoonEdit('500')">中</a>&nbsp;&nbsp;<a class="list_link" style="text-decoration:underline;" href="javascript:ZoonEdit('700')">大</a></div>
          <div style="padding-top:2px;padding-bottom:2px;position:relative;width:100%;height:2px;border-top-width:1px;border-right-width: 1px;border-bottom-width: 1px;border-left-width: 1px;border-top-style: dashed;	border-right-style: none;border-bottom-style: none;border-left-style: none;border-top-color: #CCCCCC;"></div>
          <div><a style="cursor:pointer;" onclick="selectFile('picEdit',document.getElementById('picContentTF'),320,500);" title="在上传的时候，请在编辑区鼠标点击，设置要上传图片的位置。"><font color="blue">选择图片</font></a></div>
          </td>
          <td id="EditSizeID" style="height:300px;">
            <label id="picContentTF"></label>
           <script type="text/javascript" language="JavaScript">
              window.onload = function()
                {
                var sBasePath = "../../editor/"
                var oFCKeditor = new FCKeditor('Content') ;
                oFCKeditor.BasePath	= sBasePath ;
                oFCKeditor.ToolbarSet="Foosun_User"
                oFCKeditor.Width = '100%' ;
                oFCKeditor.Height = '100%' ;	
                oFCKeditor.ReplaceTextarea() ;
                } 
            </script>
	        <textarea rows="1" cols="1" name="Content" style="display:none" id="Content" runat="server" ></textarea>
          </td>
          </tr>
        <tr class="TR_BG_list">
          <td style="width:100px;text-align:right;">
          来源
          </td>
          <td>
          <asp:TextBox ID="Souce" runat="server" Width="16%" MaxLength="100" CssClass="form"></asp:TextBox>
          </td>
          </tr>
        <tr class="TR_BG_list">
          <td style="width:100px;text-align:right;">
          Tags
          </td>
          <td>
          <asp:TextBox ID="Tags" runat="server" MaxLength="100" Width="28%" CssClass="form"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_News_add_tags',this)">什么是标签</span> 
          </td>
          </tr>
    </table>
       <table width="98%"  border="0" cellpadding="3" align="center" cellspacing="0" class="table">
        <tr class="TR_BG_list">
          <td style="text-align:center;">
              <asp:Button ID="Button1" runat="server" Text="保存数据" OnClick="Button1_Click" />
              <asp:HiddenField ID="CTime" runat="server" />
              <input type="reset" value="重新填写" />
          </td>
        </tr>
      </table>
    <br />
    <br />
     <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><%Response.Write(CopyRight); %> </td>
       </tr>
</table>
  </form>
</body>
</html>
<script language="javascript" type="text/javascript">

function getNewsInfo(obj)
{
    if(obj=='baseinfo')
    {
        document.getElementById("baseinfo").style.display="";
        document.getElementById("definfo").style.display="none";
        document.getElementById("A1").className="reshows";
        document.getElementById("A2").className="list_link";
        
    }
    else if(obj=='definfo')
    {
        document.getElementById("definfo").style.display="";
        document.getElementById("baseinfo").style.display="none";
       
        document.getElementById("A2").className="reshows";
        document.getElementById("A1").className="list_link";
    }
    else
    {
        document.getElementById("definfo").style.display="none";
        document.getElementById("baseinfo").style.display="";
        document.getElementById("A2").className="list_link";
        document.getElementById("A1").className="list_link";

    }
}
function titleFlag(obj)
{
   var t = document.getElementById("title");
  if(t.value!="")
  {
   t.value = obj + t.value;
   }
   else
   {
     t.value = obj;
   }
}

function ZoonEdit(obj)
{
   document.getElementById('EditSizeID').style.height=obj+'px';
}  
function NaviClick(obj)
{
       if(obj.checked)
       {
            document.getElementById('div_naviContent').style.display = "";
        }
        else
        {
            document.getElementById('div_naviContent').style.display = "none";
        }
}
 
    function insertHTMLEdit(url)
    {
        var urls = url.replace('{@dirfile}','<% Response.Write(Foosun.Config.UIConfig.dirFile); %>')
        IDContent.insertHTML('<img src=\"'+urls+'\" border=\"0\" />')

        return;
    }

</script>