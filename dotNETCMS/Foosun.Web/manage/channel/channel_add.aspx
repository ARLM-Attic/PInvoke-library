<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_channel_channel_add" Codebehind="channel_add.aspx.cs" %>
<%@ Register Src="../../controls/UserPop.ascx" TagName="UserPop" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body onload="getID();">
    <form id="form1" runat="server">
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" class="sysmain_navi" style="padding-left:14px;">添加频道</td>
          <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="list.aspx" class="list_link" target="sys_main">频道列表</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加频道</td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">选择频道模板</td>
        <td style="width:80%">
          <label id="getchannelType" runat="server" />
       </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道名称</td>
        <td style="width:80%">
            <asp:TextBox ID="channelName" runat="server" Width="300" MaxLength="50"></asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_channelName',this)">帮助</span>
            <asp:RequiredFieldValidator ID="f_channelName" runat="server" ControlToValidate="channelName" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道名称</span>"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道类型</td>
        <td style="width:80%">
            <asp:DropDownList ID="issys" runat="server">
            <asp:ListItem Value="0">自定义</asp:ListItem>
            <asp:ListItem Value="1">系统</asp:ListItem>
            </asp:DropDownList>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_issys',this)">帮助</span>
        </td>
      </tr>
      
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道项目名称</td>
        <td style="width:80%">
            <asp:TextBox ID="channelItem" runat="server" Width="300" MaxLength="50"></asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_channelItem',this)">帮助</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="channelItem" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道项目名称</span>"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">捆绑域名</td>
        <td style="width:80%">
            <asp:TextBox ID="binddomain" runat="server" Width="300" MaxLength="150"></asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_binddomain',this)">帮助</span>
        </td>
      </tr>
     <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道名字标识(英文)</td>
        <td style="width:80%">
            <asp:TextBox ID="channelEItem" runat="server" Width="300" MaxLength="20"></asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_channelEItem',this)">帮助</span>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="channelEItem" Display="Dynamic" ErrorMessage="<span class='reshow'>频道名字标识</span>"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="channelEItem"  Display="Dynamic" ErrorMessage="<span class='reshow'>只能为数字或者字母.第一个必须为字母,长度：3-18位</span>" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_]{2,18}"></asp:RegularExpressionValidator>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道描述</td>
        <td style="width:80%">
            <asp:TextBox ID="channelDescript" runat="server" Width="300" MaxLength="200"></asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_channelDescript',this)">帮助</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="channelDescript" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道描述</span>"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道单位</td>
        <td style="width:80%">
            <asp:TextBox ID="channelunit" runat="server" Width="300" MaxLength="50"></asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_channelunit',this)">帮助</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="channelunit" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道单位</span>"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">数据库表名</td>
        <td style="width:80%">
        <span id="DataRe" runat="server">数据库扩展名_channel_</span><asp:TextBox ID="DataLib" runat="server" Width="162" MaxLength="30"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_DataLib',this)">帮助</span>
        <span id="TSdiv" runat="server">
        <asp:RequiredFieldValidator ID="DataLibisNULL" runat="server" ControlToValidate="DataLib" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写数据库名</span>"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1s" runat="server" ControlToValidate="DataLib"  Display="Dynamic" ErrorMessage="<span class='reshow'>只能为数字或者字母.第一个必须为字母,长度：3-18位</span>" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_]{2,18}"></asp:RegularExpressionValidator>
        </span>
        </td>
      </tr>

      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">生成静态</td>
        <td style="width:80%">
            <asp:CheckBox ID="isHTML" runat="server" onclick="getID();" />
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_isHTML',this)">帮助</span>
        </td>
      </tr>
       <tr class="TR_BG_list" style="display:none;" id="Trhtmldir">
        <td style="width:20%;text-align:right;">频道生成静态目录</td>
        <td style="width:80%">
            <asp:TextBox ID="htmldir" runat="server" Width="300" Text="/{@dirHTML}" MaxLength="100"></asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_htmldir',this)">帮助</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="htmldir" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道静态生成目录</span>"></asp:RequiredFieldValidator>
            
        </td>
      </tr>
       <tr class="TR_BG_list" style="display:none;" id="Tr1indexFileName">
        <td style="width:20%;text-align:right;">频道首页文件名</td>
        <td style="width:80%">
            <asp:TextBox ID="indexFileName" runat="server" Width="300" Text="index.html" MaxLength="100"></asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_indexFileName',this)">帮助</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="indexFileName" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道首页文件名</span>"></asp:RequiredFieldValidator>
            
        </td>
      </tr>
      <tr class="TR_BG_list"  style="display:none;" id="TrClassSave">
        <td style="width:20%;text-align:right;">栏目保存目录规则</td>
        <td style="width:80%">
            <asp:TextBox ID="ClassSave" runat="server" Width="300" Text="" MaxLength="100"></asp:TextBox>
            <img src="../../sysImages/folder/s.gif" alt="选择规则" border="0" style="cursor:pointer;" onclick="selectFile('rulesmallPram',document.form1.ClassSave,100,500);document.form1.ClassSave.focus();" />
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_ClassSave',this)">帮助</span>
        </td>
      </tr>      
      
      <tr class="TR_BG_list"  style="display:none;" id="TrClassFileName">
        <td style="width:20%;text-align:right;">栏目文件名规则(包含扩展名)</td>
        <td style="width:80%">
            <asp:TextBox ID="ClassFileName" runat="server" Width="300" Text="{@EName}/index.html" MaxLength="100"></asp:TextBox>
            <img src="../../sysImages/folder/s.gif" alt="选择规则" border="0" style="cursor:pointer;" onclick="selectFile('rulePram',document.form1.ClassFileName,100,500);document.form1.ClassFileName.focus();" />
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_ClassFileName',this)">帮助</span>
        </td>
      </tr>      
      
      <tr class="TR_BG_list"  style="display:none;" id="TrSavePath">
        <td style="width:20%;text-align:right;">静态文件保存目录规则</td>
        <td style="width:80%">
            <asp:TextBox ID="SavePath" runat="server" Width="300" Text="{@year04}/{@month}/{@day}" MaxLength="100"></asp:TextBox>
            <img src="../../sysImages/folder/s.gif" alt="选择规则" border="0" style="cursor:pointer;" onclick="selectFile('rulesmallPram',document.form1.SavePath,100,500);document.form1.SavePath.focus();" />
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_SavePath',this)">帮助</span>
        </td>
      </tr>      
      
      <tr class="TR_BG_list" style="display:none;" id="TrFileName">
        <td style="width:20%;text-align:right;">静态文件名规则(包含扩展名)</td>
        <td style="width:80%">
            <asp:TextBox ID="FileName" runat="server" Width="300" Text="{@hour}{@minute}{@second}_{@Ram2_0}.html" MaxLength="100"></asp:TextBox>
            <img src="../../sysImages/folder/s.gif" alt="选择规则" border="0" style="cursor:pointer;" onclick="selectFile('rulePram',document.form1.FileName,100,500);document.form1.FileName.focus();" />
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_FileName',this)">帮助</span>
        </td>
      </tr>      
      
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">允许上传的最大文件大小</td>
        <td style="width:80%">
            <asp:TextBox ID="upfilessize" runat="server" Width="300" MaxLength="4">800</asp:TextBox>&nbsp;KB
            <asp:RegularExpressionValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="upfilessize"  Display="Dynamic" ErrorMessage="<span class='reshow'>格式不正确。请填写正整数</span>" ValidationExpression="^[0-9]{0,4}"></asp:RegularExpressionValidator>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_upfilessize',this)">帮助</span>
        </td>
      </tr>
      
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">允许上传的类型</td>
        <td style="width:80%">
            <asp:TextBox ID="upfiletype" runat="server" Width="300" MaxLength="100">jpg,jpeg,gif,bmp,swf,rar,doc,zip</asp:TextBox>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_upfiletype',this)">帮助</span>
        </td>
      </tr>
      
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">添加信息需要审核</td>
        <td style="width:80%">
            <asp:CheckBox ID="ischeck" runat="server" />
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_ischeck',this)">帮助</span>
        </td>
      </tr>

      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">是否允许投稿</td>
        <td style="width:80%">
            <asp:CheckBox ID="isconstr" runat="server" />
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_isconstr',this)">帮助</span>
        </td>
      </tr>
      <tr class="TR_BG_list" id="TrPop">
        <td style="width:20%;text-align:right;">浏览权限</td>
        <td style="width:80%">
            <uc2:UserPop ID="UserPop1" runat="server"/>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_Pop',this)">帮助</span>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">模板目录(相对于整站的模板目录(Templets))</td>
        <td style="width:80%">
            <asp:TextBox ID="TempletPath" runat="server" Width="300" MaxLength="100">channel</asp:TextBox><%--<img src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor:pointer;" onclick="selectFile('templet',document.form1.indextemplet,250,500);document.form1.indextemplet.focus();" />--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TempletPath" Display="Dynamic" ErrorMessage="<span class='reshow'>模板目录</span>"></asp:RequiredFieldValidator>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_indextemplet',this)">帮助</span>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道首页模板</td>
        <td style="width:80%">
            <asp:TextBox ID="indextemplet" runat="server" Width="300" MaxLength="200">index.html</asp:TextBox><%--<img src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor:pointer;" onclick="selectFile('templet',document.form1.indextemplet,250,500);document.form1.indextemplet.focus();" />--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="indextemplet" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道首页模板</span>"></asp:RequiredFieldValidator>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_indextemplet',this)">帮助</span>
        </td>
      </tr>
      
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道列表模板</td>
        <td style="width:80%">
            <asp:TextBox ID="classtemplet" runat="server" Width="300" MaxLength="200">list.html</asp:TextBox><%--<img src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor:pointer;" onclick="selectFile('templet',document.form1.classtemplet,250,500);document.form1.classtemplet.focus();" />--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="classtemplet" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道列表模板</span>"></asp:RequiredFieldValidator>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_classtemplet',this)">帮助</span>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道内容显示模板</td>
        <td style="width:80%">
            <asp:TextBox ID="newstemplet" runat="server" Width="300" MaxLength="200">content.html</asp:TextBox><%--<img src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor:pointer;" onclick="selectFile('templet',document.form1.newstemplet,250,500);document.form1.newstemplet.focus();" />--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="newstemplet" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道内容显示模板</span>"></asp:RequiredFieldValidator>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_newstemplet',this)">帮助</span>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;">频道专题模板</td>
        <td style="width:80%">
            <asp:TextBox ID="specialtemplet" runat="server" Width="300" MaxLength="200">special.html</asp:TextBox><%--<img src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor:pointer;" onclick="selectFile('templet',document.form1.specialtemplet,250,500);document.form1.specialtemplet.focus();" />--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="specialtemplet" Display="Dynamic" ErrorMessage="<span class='reshow'>请填写频道专题模板</span>"></asp:RequiredFieldValidator>
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_specialtemplet',this)">帮助</span>
        </td>
      </tr>
      <tr class="TR_BG_list" style="display:none;">
        <td style="width:20%;text-align:right;">定义为频道模板</td>
        <td style="width:80%">
            <asp:CheckBox ID="isModelContent" runat="server" />
            <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_channel_isModelContent',this)">帮助</span>
        </td>
      </tr>
      <tr class="TR_BG_list">
        <td style="width:20%;text-align:right;"></td>
        <td style="width:80%">
         <asp:Button ID="Button1" runat="server" Text="保存频道" OnClick="Button1_Click" />
         <input type="reset" value="重新填写" />
        </td>
      </tr>
      </table>
    <br />
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><label id="copyright" runat="server" /></td>
       </tr>
    </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">

new Form.Element.Observer($('channelEItem'),1,getTempletSave);
	function getTempletSave()
		{
			if ($('channelEItem').value=='')
			{
				$('TempletPath').value='Channel'
				$('htmldir').value='/{@dirHTML}';
				$('DataLib').value=''
			}
			else
			{
			    $('TempletPath').value='channel/'+document.getElementById("channelEItem").value+''
			    $('htmldir').value='/{@dirHTML}/'+document.getElementById("channelEItem").value+''
			    $('DataLib').value=document.getElementById("channelEItem").value
			}
		} 

function getID()
{
    var isHTML = document.getElementById("isHTML");
    var  htmldir = document.getElementById("Trhtmldir");
    var  ClassSave = document.getElementById("TrClassSave");
    var  ClassFileName = document.getElementById("TrClassFileName");
    var  SavePath = document.getElementById("TrSavePath");
    var  FileName = document.getElementById("TrFileName");
    var  Pop = document.getElementById("TrPop");
    if(isHTML.checked)
    {
          htmldir.style.display="";
          ClassSave.style.display="";
          ClassSave.style.display="";
          ClassFileName.style.display="";
          SavePath.style.display="";
          FileName.style.display="";
          Pop.style.display="none";
    }
    else
    {
          htmldir.style.display="none";
          ClassSave.style.display="none";
          ClassSave.style.display="none";
          ClassFileName.style.display="none";
          SavePath.style.display="none";
          FileName.style.display="none";
          Pop.style.display="";
    }
}
</script>