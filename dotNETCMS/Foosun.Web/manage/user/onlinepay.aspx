<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_user_onlinepay" Codebehind="onlinepay.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
              <td height="1" colspan="2"></td>
            </tr>
            <tr>
              <td width="57%"  class="sysmain_navi" style="padding-left:14px;" >在线支付管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />在线支付管理</td>
            </tr>
    </table>
     <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              选择ISP商</div></td>
          <td class="list_link">
               <asp:DropDownList ID="onpayType" runat="server" style="width: 250px">
                  <asp:ListItem Value="0">支付宝(www.alipay.com)</asp:ListItem>
                  <asp:ListItem Value="1">网银(www.chinabank.com)</asp:ListItem>
                  <asp:ListItem Value="2">云网(www.cncard.net)</asp:ListItem>
                  <asp:ListItem Value="3">其他</asp:ListItem>
              </asp:DropDownList>
              
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_onlinepay_0001',this)">帮助</span></td>
        </tr>

        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              帐号</div></td>
          <td class="list_link">
           <asp:TextBox CssClass="form" ID="O_userName" style="width: 250px" runat="server"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_onlinepay_0002',this)">帮助</span>
           <asp:RequiredFieldValidator ID="f_O_userName" runat="server" ControlToValidate="O_userName" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写帐号</span>"></asp:RequiredFieldValidator>
          </td>
        </tr>
        

        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              密码</div></td>
          <td class="list_link">
           <asp:TextBox CssClass="form" ID="O_key" style="width: 250px" runat="server" TextMode="Password"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_onlinepay_0003',this)">帮助</span>
          <asp:RequiredFieldValidator ID="f_O_key" runat="server" ControlToValidate="O_key" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)填写密码</span>"></asp:RequiredFieldValidator>
          </td>
        </tr>
        
 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              ISP接收页面</div></td>
          <td class="list_link">
           <asp:TextBox CssClass="form" ID="O_sendurl" style="width: 250px" runat="server"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_onlinepay_0004',this)">帮助</span></td>
        </tr>


        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              ISP返回页面/处理页面</div></td>
          <td class="list_link">
           <asp:TextBox CssClass="form" ID="O_returnurl" style="width: 250px" runat="server"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_onlinepay_0005',this)">帮助</span></td>
        </tr>
 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              MD5(其他校对)</div></td>
          <td class="list_link">
           <asp:TextBox CssClass="form" ID="O_md5" style="width: 250px" runat="server"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_onlinepay_0006',this)">帮助</span></td>
        </tr>

        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              其他参数一</div></td>
          <td class="list_link">
           <asp:TextBox CssClass="form" ID="O_other1" style="width: 250px" runat="server"></asp:TextBox>
           </td>
        </tr>        
 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              其他参数二</div></td>
          <td class="list_link">
           <asp:TextBox CssClass="form" ID="O_other2" style="width: 250px" runat="server"></asp:TextBox>
           </td>
        </tr>    
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              其他参数三</div></td>
          <td class="list_link">
           <asp:TextBox CssClass="form" ID="O_other3" style="width: 250px" runat="server"></asp:TextBox>
           </td>
        </tr>  
                     
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
            </div></td>
          <td class="list_link">
              <asp:Button ID="Button1" runat="server"  CssClass="form" Text="确定提交" OnClick="Button1_Click" />     </td>

           </tr>               

        </table>    
    </form>
  <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
       <tr>
         <td align="center"><label id="copyright" runat="server" /></td>
       </tr>
    </table>  
   </body>
</html>
