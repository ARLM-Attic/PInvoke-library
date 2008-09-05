<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_discussManage_add" EnableEventValidation="false"  Codebehind="discussManage_add.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
    <title>
        <%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>
    </title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css"
        rel="stylesheet" type="text/css" />
    <script language="javascript">			
			function ChangeSort(obj)
			{
			    var val = obj.options[obj.selectedIndex].value;
			    if(val == '0')
			    {		  
			        document.getElementById('ClassIDList2').options.length = 0;
			        var sel = document.getElementById('ClassIDList2');
			        var opts = document.createElement('option');
                    opts.value = "0";
                    opts.innerText = "请选择";
                    sel.appendChild(opts);
			        return;
			    }
                var param = "provinces="+ val;
                 var options={
                 method:'post',
                    parameters:param,
                    onComplete:
                    function(transport)
	                {
		            var retv=transport.responseText;
		             onRcvMsg(retv);
		            } 
	            }
	            new  Ajax.Request('discussManage_add.aspx',options);  
			}
		    function onRcvMsg(retv)
			{
			  var sel = document.getElementById('ClassIDList2');
			  var inpt = retv.split(";");
			  sel.options.length = 0;
	          var opts = document.createElement('option');
              opts.value = "0";
              opts.innerText = "请选择";
              sel.appendChild(opts);
            for(var i=0;i<inpt.length;i++)
                 {
                    var opt = document.createElement('option');
                    var txt = inpt[i].split(",");
                    opt.value = txt[0];
                    opt.innerText = txt[1];
                    sel.appendChild(opt);
                 }
			}
    </script>
</head>
<body style="background-color: #ffffff">
<form id="form1" name="form1" method="post" action="" runat="server">
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >讨论组管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userdiscuss_list.aspx" class="menulist">讨论组管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加讨论组</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="userdiscuss_list.aspx" class="menulist">讨论组</a>
         &nbsp; <a href="userdiscuss_list.aspx" class="menulist">讨论组活动</a> &nbsp;&nbsp; <a href="discussclass.aspx" class="menulist">讨论组分类</a> &nbsp; <a href="discussManage_add.aspx" class="menulist"><span style="color: #ff6666">添加讨论组</span></a></span></td>
  </tr>
</table>

<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table" id="addmanage">
  <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        讨论组名称</td>
    <td class="list_link" Width="75%">
        <asp:TextBox ID="CnameBox" runat="server" Width="314px" CssClass="form"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CnameBox"
            ErrorMessage="请输入讨论组名称"></asp:RequiredFieldValidator></td>
  </tr>

    <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        讨论组父分类</td>
    <td class="list_link" Width="75%">
        <asp:DropDownList ID="ClassIDList1" runat="server" onchange="ChangeSort(this)" Width="142px">
        </asp:DropDownList></td>
  </tr>
    <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        讨论组子分类</td>
    <td class="list_link" Width="75%">
        <asp:DropDownList ID="ClassIDList2" runat="server" Width="142px">
        </asp:DropDownList>
        <a href="discusssubclass_add.aspx" class="list_link">添加子分类</a></td>
      </tr>  
    <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        是否对外公开</td>
    <td class="list_link" Width="75%">
        <asp:RadioButtonList ID="AuthorityList1" runat="server" RepeatDirection="Horizontal"
            Width="140px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList></td>
  </tr>
  
      <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        是否允许组员发表主题</td>
    <td class="list_link" Width="75%">
        <asp:RadioButtonList ID="AuthorityList2" runat="server" RepeatDirection="Horizontal"
            Width="140px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList></td>
  </tr>
      <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        是否需要管理员审核</td>
    <td class="list_link" Width="75%">
        <asp:RadioButtonList ID="AuthorityList3" runat="server" RepeatDirection="Horizontal"
            Width="140px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList></td>
  </tr>
      <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        是否允许非组员发帖</td>
    <td class="list_link" Width="75%">
        <asp:RadioButtonList ID="AuthorityList4" runat="server" RepeatDirection="Horizontal"
            Width="140px">
            <asp:ListItem Selected="True">是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
        </asp:RadioButtonList></td>
  </tr>  
   <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        组员加入设置</td>
    <td class="list_link" Width="75%">
        <input id="Radio1" type="radio" onclick="DispChanges()" runat="server" checked="true"/>直接加入
        <input id="Radio2" type="radio" onclick="DispChanges()" runat="server"/>拒绝加入
        <input id="Radio3" type="radio" onclick="DispChanges()" runat="server"/>需要积分或金币加入
        </td>
  </tr>
  <tr class="TR_BG_list" style="display:none" id="numbers">
    <td class="list_link" Width="25%" style="height: 70px">
        需要积分或金币</td>
    <td class="list_link" Width="75%" style="height: 70px">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="20%" style="height: 24px">
                    &nbsp;&nbsp;
                    金币</td>
                <td width="30%" style="height: 24px">
                    <asp:TextBox ID="gPointBox" runat="server" Width="169px" CssClass="form">0</asp:TextBox></td>
                    <td width="50%" style="height: 24px">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="gPointBox"
                        ErrorMessage="您输入的格式不对" ValidationExpression="^[1-9]\d*|0$"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td colspan="3" height="2">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    &nbsp;&nbsp;
                    积分</td>
                <td style="width: 100px">
                    <asp:TextBox ID="iPointBox" runat="server" Width="171px" CssClass="form">0</asp:TextBox></td>
                    <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="iPointBox"
                        ErrorMessage="您输入的格式不对" ValidationExpression="^[1-9]\d*|0$" Display="Dynamic"></asp:RegularExpressionValidator></td>
            </tr>
        </table>
    </td>
  </tr>
            <tr class="TR_BG_list">
    <td class="list_link" Width="25%">
        内部公告</td>
    <td class="list_link" Width="75%">
        <asp:TextBox ID="D_annoBox" runat="server" Width="314px" CssClass="form"></asp:TextBox></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link">
        讨论组对外说明</td>
    <td class="list_link">
        <asp:TextBox ID="D_ContentBox" runat="server" Width="314px" Height="62px" TextMode="MultiLine" CssClass="form"></asp:TextBox></td>
  </tr>
   <tr class="TR_BG_list">
    <td class="list_link"></td>
    <td class="list_link">
        &nbsp; &nbsp; &nbsp;
        <asp:Button ID="but1" runat="server" Text="提  交" OnClick="but1_Click" CssClass="form" />
        &nbsp; &nbsp;&nbsp;
        <input type="reset" name="Submit3" value="重  置" class="form"></td>  
   </tr>
</table>

<div style="PADDING-top: 50px"></div>
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %></div></td>
  </tr>
</table>
</form>
</body>
</html>
<script language="javascript">
function DispChanges()
{
    var obj = document.getElementById("Radio3").checked;
    var objs = document.getElementById("Radio2").checked;
    var objss = document.getElementById("Radio1").checked;
    if(obj)
    {
            document.getElementById("numbers").style.display="";
    }
    if(objs||objss)
    {
            document.getElementById("numbers").style.display="none";
    }
}
</script>