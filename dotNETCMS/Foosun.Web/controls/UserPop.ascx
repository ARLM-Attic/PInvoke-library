<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_UserPop" Codebehind="UserPop.ascx.cs" %>
<asp:DropDownList ID="DdlAuthorityType" runat="server" onchange="GetSelect(this)">
    <asp:ListItem Value="0">未设置</asp:ListItem>
    <asp:ListItem Value="1">扣除金币</asp:ListItem>
    <asp:ListItem Value="2">扣除积分</asp:ListItem>
    <asp:ListItem Value="3">扣除金币和积分</asp:ListItem>
    <asp:ListItem Value="4">达到金币</asp:ListItem>
    <asp:ListItem Value="5">达到积分</asp:ListItem>
    <asp:ListItem Value="6">达到金币和积分</asp:ListItem>
</asp:DropDownList>
<div id="Div_AuthorityGold" style="display:inline">
&nbsp;&nbsp;&nbsp;
金币：
<asp:TextBox ID="TxtAuthorityGold" runat="server" Width="58px" CssClass="form">0</asp:TextBox>&nbsp;</div>
<div id="Div_AuthorityPoint" style="display:inline">
&nbsp;&nbsp;&nbsp;
积分:
<asp:TextBox ID="TxtAuthorityPoint" runat="server" Width="58px" CssClass="form">0</asp:TextBox>
    </div>
<div id="Div_AuthorityGroup">
请选择会员组:<br />
<asp:ListBox ID="LstAuthorityGroup" runat="server" SelectionMode="Multiple" Height="160px" Width="154px" CssClass="form"></asp:ListBox>
    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TxtAuthorityGold"
        ErrorMessage="金币必须输入非负整数" MaximumValue="2147483647" MinimumValue="0" Type="Integer"></asp:RangeValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtAuthorityPoint"
        ErrorMessage="积分必须输入非负整数" MaximumValue="2147483647" MinimumValue="0" Type="Integer"></asp:RangeValidator></div>
<script type="text/javascript" language="javascript">
<!--
GetSelect(document.getElementById("UserPop1_DdlAuthorityType"));
function GetSelect(obj)
{
    var selval = parseInt(obj.options[obj.selectedIndex].value);
    var divgroup = document.getElementById("Div_AuthorityGroup");
    var divpoint = document.getElementById("Div_AuthorityPoint");
    var divgold = document.getElementById("Div_AuthorityGold");
    
    switch(selval)
    {
        case 0:
            divgroup.style.display = "none";
            divpoint.style.display = "none";
            divgold.style.display = "none";
            break;
        case 1:
        case 4:
            divgroup.style.display = "";
            divgold.style.display = "inline";
            divpoint.style.display = "none";
            document.getElementById("UserPop1_TxtAuthorityPoint").value = "0";
            break;
        case 2:
        case 5:
            divgroup.style.display = "";
            divpoint.style.display = "inline";
            divgold.style.display = "none";
            document.getElementById("UserPop1_TxtAuthorityGold").value = "0";
            break;
        case 3:
        case 6:
            divgroup.style.display = "";
            divpoint.style.display = "inline";
            divgold.style.display = "inline";
            break;
    }
}
//-->
</script>