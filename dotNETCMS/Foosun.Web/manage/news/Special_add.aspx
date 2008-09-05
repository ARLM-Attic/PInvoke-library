<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_Special_add" Codebehind="Special_add.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head">
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="F_Speical" runat="server" method="post">
  <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" height="30">添加专题</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Special_List.aspx" class="list_link">专题管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加专题</div></td>
    </tr>
  </table>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG_list">
      <td width="11%" align="center" class="navi_link" style="width: 13%">专题中文名</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_Cname" runat="server" Width="250px"  onChange="javascript:GetPY1(this);" CssClass="form" MaxLength="50"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_001',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldS_Cname" runat="server" ErrorMessage="<span class=reshow>(*)请填写专题中文名</spna>" ControlToValidate="S_Cname" Display="Static"></asp:RequiredFieldValidator></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">专题英文名</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_Ename" runat="server" onChange="javascript:GetPositionHtml();" CssClass="form" MaxLength="50" Width="250px"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_002',this)">帮助</span>
        <asp:RequiredFieldValidator ID="RequiredFieldS_Ename" runat="server" ErrorMessage="<span class=reshow>(*)请填写专题英文名</spna>" ControlToValidate="S_Ename" Display="Static"></asp:RequiredFieldValidator></td>
    </tr>
      <tr class="TR_BG_list">
          <td align="center" class="navi_link" style="width: 13%">
              专题父栏目</td>
          <td align="left" colspan="2">
              <asp:TextBox ID="S_ParentName" runat="server" CssClass="form" MaxLength="12" ReadOnly="true"
                  Width="250px"></asp:TextBox></td>
      </tr>
    <tr class="TR_BG_list" style="display:none;">
      <td align="center" class="navi_link" style="width: 13%">专题父栏目</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_Parent" runat="server" CssClass="form" MaxLength="12" Width="250px" ReadOnly="true"></asp:TextBox>
<%--       <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择专题"  onclick="selectFile('special',document.F_Speical.S_Parent,300,380);document.F_Speical.S_Parent.focus();" /> <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_003',this)">帮助</span>
--%>       </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">专题域名</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_Domain" runat="server" CssClass="form" MaxLength="100" Width="250px"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_004',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">专题扩展名</td>
      <td colspan="2" align="left"><asp:DropDownList ID="S_FileExname" runat="server" CssClass="form" Width="255px" onchange="javascript:Hide(this.value);">
          <asp:ListItem Value=".html">.html</asp:ListItem>
          <asp:ListItem Value=".htm">.htm</asp:ListItem>
          <asp:ListItem Value=".shtml">.shtml</asp:ListItem>
          <asp:ListItem Value=".aspx">.aspx</asp:ListItem>
        </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_008',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list" id="Tr_Pop" style="display:none;">
      <td align="center" class="navi_link" style="width: 13%">专题浏览权限</td>
      <td colspan="2" align="left">会员组<br />
        <asp:DropDownList ID="S_UserGroup" runat="server" CssClass="form" Width="210px" Height="100px" multiple="multiple"> </asp:DropDownList><p>
        设&nbsp;&nbsp;&nbsp;置
        <asp:DropDownList ID="S_IsDel" runat="server" CssClass="form" Width="100px">
          <asp:ListItem Value="null">请选择</asp:ListItem>
          <asp:ListItem Value="0">都可以查看</asp:ListItem>
          <asp:ListItem Value="1">扣取金币</asp:ListItem>
          <asp:ListItem Value="2">扣取点数</asp:ListItem>
          <asp:ListItem Value="3">扣取金币和点数</asp:ListItem>
          <asp:ListItem Value="4">需要金币</asp:ListItem>
          <asp:ListItem Value="5">需要点数</asp:ListItem>
          <asp:ListItem Value="6">需要金币和点数</asp:ListItem>
        </asp:DropDownList>
        点数
        <asp:TextBox ID="S_Point" runat="server" CssClass="form" MaxLength="8" Width="35px"></asp:TextBox>
        金币
        <asp:TextBox ID="S_Money" runat="server" CssClass="form" MaxLength="8" Width="35px"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_005',this)">帮助</span></p></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">生成目录规则</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_DirRule" runat="server" CssClass="form" MaxLength="100" Width="250px"></asp:TextBox>&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择规则" onclick="selectFile('rulesmallPramo',document.F_Speical.S_DirRule,100,500);document.F_Speical.S_DirRule.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_006',this)">帮助</span> <asp:RequiredFieldValidator ID="RequiredFieldValidatorS_DirRule" runat="server" ErrorMessage="<span class=reshow>(*)请选择目录规则</spna>" ControlToValidate="S_DirRule" Display="Static"></asp:RequiredFieldValidator></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">生成文件规则</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_FileRule" runat="server" CssClass="form" MaxLength="100" Width="250px"></asp:TextBox>&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择规则" onclick="selectFile('rulePram',document.F_Speical.S_FileRule,100,500);document.F_Speical.S_FileRule.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_007',this)">帮助</span> <asp:RequiredFieldValidator ID="RequiredFieldValidatorS_FileRule" runat="server" ErrorMessage="<span class=reshow>(*)请选择文件规则</spna>" ControlToValidate="S_FileRule" Display="Static"></asp:RequiredFieldValidator></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">专题保存路径</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_SavePath" runat="server" CssClass="form" MaxLength="100" Width="250px"></asp:TextBox>&nbsp;
      <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择路径" onclick="selectFile('path|<%Response.Write(DirHtml); %>',document.F_Speical.S_SavePath,300,500);document.F_Speical.S_SavePath.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_009',this)">帮助</span> <asp:RequiredFieldValidator ID="RequiredFieldValidatorS_SavePath" runat="server" ErrorMessage="<span class=reshow>(*)请选择专题保存路径</spna>" ControlToValidate="S_SavePath" Display="Static"></asp:RequiredFieldValidator></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">专题导航图片</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_Pic" runat="server" CssClass="form" MaxLength="200" Width="250px"></asp:TextBox>&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择图片" onclick="selectFile('pic',document.F_Speical.S_Pic,280,500);document.F_Speical.S_Pic.focus();" /> 
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_010',this)">帮助</span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">专题导航文字</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_Text" runat="server" CssClass="form" Width="360px" TextMode="MultiLine" Height="100px"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_011',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">专题模板地址</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_Templet" runat="server" CssClass="form" MaxLength="200" Width="250px"></asp:TextBox>&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择模板" onclick="selectFile('templet',document.F_Speical.S_Templet,280,500);document.F_Speical.S_Templet.focus();" />  
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_012',this)">帮助</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorS_Templet" runat="server" ControlToValidate="S_Templet"
                Display="Static" ErrorMessage="<span class=reshow>(*)请选择专题模板地址</spna>"></asp:RequiredFieldValidator></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">专题页面导航</td>
      <td colspan="2" align="left"><asp:TextBox ID="S_Page" runat="server" CssClass="form"  TextMode="MultiLine" Width="360px" Height="100px"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_013',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 13%">是否锁定</td>
      <td align="left"><asp:RadioButtonList ID="S_Lock" runat="server" RepeatDirection="Horizontal"><asp:ListItem Value="1">是</asp:ListItem><asp:ListItem Value="0" Selected="True">否</asp:ListItem></asp:RadioButtonList></td>
      <td width="55%" align="left"><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_SpecialAdd_014',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="left" class="navi_link" colspan="3"><label>
        <asp:Button ID="Button1" runat="server" Text=" 确 定 " CssClass="form" OnClick="Button1_Click"/>
        </label>
        <label>
        <input type="reset" name="UnDo" value=" 重 填 " class="form" />
        </label><input type="hidden" value="0" name="isTrue" /></td>
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
<script language="javascript" type="text/javascript">
function Hide(value)
{
    if(value==".aspx")
    {
        document.getElementById("Tr_Pop").style.display="";
        document.F_Speical.isTrue.value="1";
    }
    else
    {
        document.getElementById("Tr_Pop").style.display="none";
        document.F_Speical.isTrue.value="0";
    }
}

function GetPositionHtml()
{
      document.getElementById("S_Page").value = "<a href=\"/\" target=\"_blank\">首页</a> >> <a href=\"{#URL}\">"+document.getElementById("S_Cname").value+"</a> >> 专题列表"; 
}

function GetPY1(obj)
{
        var s = obj.value.trim();
        if(s != '')
        {
            document.getElementById('S_Ename').value = GetPY(s);
        }
}

new Form.Element.Observer($('S_Ename'),1,TEname_1);
	function TEname_1()
		{
			if ($('S_Ename').value=='')
			{
				$('S_Page').value=''
			}
			else
			{
				$('S_Page').value="<a href=\"/\" target=\"_blank\">首页</a> >> <a href=\"{#URL}\"> "+document.getElementById("S_Cname").value+"</a> >> 专题列表";
			}
		} 

</script>
</html>
