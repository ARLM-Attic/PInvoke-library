<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="step4.aspx.cs" Inherits="Foosun.Web.Install.step4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title><%=Foosun.Install.Config.title%></title>
<%=Foosun.Install.Config.style%>
<style type="text/css">
.Greens{color:green;}
</style>
<script type="text/javascript" src="../configuration/js/Prototype.js"></script>
<script type="text/javascript" src="../configuration/js/public.js"></script>
</head>
<body>
<div class="setindexstyle" id="getLoading" style="display:none;" runat="server">
  <div style="font-family:Arial;line-height:22px;text-align:left;font-size:12px;font-weight:normal;color:red;padding:30px 30px 10px 30px;border:3px #000 solid;background-color:#eeffee;margin:auto 10px auto 10px;width:400px;height:100px;" id="MessageID"> </div>
</div>
<form id="form1" runat="server" method="post">
  <table width="700" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#666666">
  <tr>
  <td bgcolor="#ffffff">
  		<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td colspan="2" bgcolor="#333333"><table width="100%" border="0" cellspacing="0" cellpadding="8">
              <tr>
                <td background="image/01.jpg"><font color="#ffffff"> 创建管理员 </font> </td>
        </tr>
        </table>
        </td>
    </tr>
    <tr>
    
    <td width="180" valign="top"><%=Foosun.Install.Config.logo%> </td>
    <td width="520" valign="top">
    
    <br />
    <br />
        <table cellspacing="0" cellpadding="8" width="100%" border="0">
          <tr>
            <td style="background-color: #f5f5f5;color:Red;" colspan="2"><%Response.Write(gError); %></td>
            </tr>          
            
            <tr>
            <td width="100" style="background-color: #f5f5f5;" >用户名:</td>
            <td width="568">
                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
            </td>
            </tr>
          <tr>
            <td style="background-color: #f5f5f5">密码:</td>
            <td>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            </tr>                   
             <tr>
            <td style="background-color: #f5f5f5">确认密码:</td>
            <td>
                <asp:TextBox ID="confimPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            </tr>          
            
            <tr>
              <td colspan="2"> <asp:Button ID="Button1" runat="server" Text="创建管理员" OnClick="Button1_Click" />                              </td>
            </tr>
        </table>
        </td>
    </tr>

</table>
</form>

</body>
</html>
<script type="text/javascript">

function showLoading()
{
    var gu=document.getElementById("UserName");
    var pass=document.getElementById("Password");
    var cpass=document.getElementById("confimPassword");
    if(gu.value=="")
    {
        alert("请填写管理员用户名");
        gu.focus();
        return false;
    }  
    if(pass.value=="")
    {
        alert("请填写管理员密码");
        pass.focus();
        return false;
    }  
    if(pass.value.length<3)
    {
        alert("管理员密码不能小于3个字符");
        pass.focus();
        return false;
    }      
    if(pass.value!=cpass.value)
    {
        alert("2次密码不一致");
        cpass.focus();
        return false;
    }  
}
</script>