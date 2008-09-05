<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_user_iCardEdit" Codebehind="iCardEdit.aspx.cs" %>

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
              <td width="57%"  class="sysmain_navi" style="padding-left:14px;" >点卡管理</td>
              <td width="43%">位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="icard.aspx" class="list_link" target="sys_main">点卡管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改点卡</td>
            </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td  style="padding-left:12px;"><a href="icard.aspx" class="topnavichar">全部点卡</a>┋<a href="icard.aspx?islock=1" class="topnavichar">已锁定</a>┋<a href="icard.aspx?islock=0" class="topnavichar">未锁定</a>┋<a href="icard.aspx?isuse=1" class="topnavichar">已使用</a>┋<a href="icard.aspx?isuse=0" class="topnavichar">未使用</a>┋<a href="icard.aspx?isbuy=1" class="topnavichar">已购买</a>┋<a href="icard.aspx?isbuy=0" class="topnavichar">未购买</a>┋<a href="icard.aspx?timeout=0" class="topnavichar">未过期</a>┋<a href="icard.aspx?timeout=1" class="topnavichar">已过期</a>┋<a href="icard_add.aspx" class="topnavichar" >添加点卡</a>┋<a href="icard_adds.aspx" class="topnavichar" >批量生成点卡</a></td>
      </tr>
      </table>
     <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
       
       
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">卡号</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="CardNumber"  ReadOnly runat="server" MaxLength="30"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_icard_add_0001',this)">帮助</span>
          <asp:RequiredFieldValidator ID="f_CardNumber" runat="server" ControlToValidate="CardNumber" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)卡号长度为30</span>"></asp:RequiredFieldValidator>
         </td></tr>       
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">密码</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="CardPassWord" runat="server" MaxLength="30"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_icard_add_0002',this)">帮助</span>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CardPassWord" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)密码长度为30</span>"></asp:RequiredFieldValidator>
          </td>
          </tr>
    
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">金额</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="Money" runat="server" MaxLength="5" Text="0"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_icard_add_0003',this)">帮助</span>
          <asp:RequiredFieldValidator ID="RequiredFieldValidators" runat="server" ControlToValidate="Money" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)金额长度为5</span>"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Money"  Display="Static" ErrorMessage="(*)格式不正确。请填写正整数" ValidationExpression="^[0-9]{0,5}"></asp:RegularExpressionValidator>
          </td></tr>    
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">点数</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="Point" runat="server" MaxLength="8"  Text="100" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_icard_add_0004',this)">帮助</span>
          <asp:RequiredFieldValidator ID="RequiredFieldValidatordd" runat="server" ControlToValidate="Point" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)点数长度为8</span>"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Point"  Display="Static" ErrorMessage="(*)格式不正确。请填写正整数" ValidationExpression="^[0-9]{0,8}"></asp:RegularExpressionValidator>
          </td></tr>

       <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">有效日期</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="TimeOutDate" runat="server" MaxLength="10"  Width="250"></asp:TextBox>&nbsp;<img src="../../sysImages/folder/s.gif" alt="选择点卡过期日期" border="0" style="cursor:pointer;" onclick="selectFile('date',document.form1.TimeOutDate,160,500);document.form1.TimeOutDate.focus();" /><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_icard_add_0006',this)">帮助</span>
              <asp:RequiredFieldValidator ID="f_TimeOutDatsdee" runat="server" ControlToValidate="TimeOutDate" Display="Dynamic" ErrorMessage="<span class='reshow'>(*)请填写日期。格式为：2007-02-14</span>"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="f_TimeOutDatessa" runat="server"  ControlToValidate="TimeOutDate"  ErrorMessage="<span class='reshow'>正确填写日期.格式为：2007-02-14</span>"  ValidationExpression="^[12]{1}(\d){3}[-][01]?(\d){1}[-][0123]?(\d){1}$"></asp:RegularExpressionValidator>
              </td>
          </tr>
         
 
       <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              锁定</div></td> 
          <td class="list_link">
          
         <asp:DropDownList ID="isLock" runat="server">
             <asp:ListItem Value="0">否</asp:ListItem>
             <asp:ListItem Value="1">是</asp:ListItem>
         </asp:DropDownList>
         
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_icard_add_0007',this)">帮助</span></td>
          </tr>   
                
 
       <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              使用</div></td> 
          <td class="list_link">
          
         <asp:DropDownList ID="isUse" runat="server">
             <asp:ListItem Value="0">否</asp:ListItem>
             <asp:ListItem Value="1">是</asp:ListItem>
         </asp:DropDownList>
         
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_icard_add_0008',this)">帮助</span></td>
          </tr>   
 
 
       <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              购买</div></td> 
          <td class="list_link">
          
         <asp:DropDownList ID="isBuy" runat="server">
             <asp:ListItem Value="0">否</asp:ListItem>
             <asp:ListItem Value="1">是</asp:ListItem>
         </asp:DropDownList>
         
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_icard_add_0009',this)">帮助</span></td>
          </tr> 
                          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 114px; height: 28px;">&nbsp;</td>
          <td class="list_link" style="height: 28px">&nbsp;
          <asp:Button ID="sumbitsave" runat="server" CssClass="form" Text=" 修 改 " OnClick="sumbitsave_Click" />
            <input name="reset" type="reset" value=" 重 置 "  class="form">
              <asp:HiddenField ID="cId" runat="server" />
             </td>
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
  <script type="text/javascript" language="javascript">
    function getchanelInfo(obj)
    {
       var SiteID=obj.value;
       window.location.href="icard.aspx?SiteID="+SiteID+"";
    }
 
   function getSearch(cardvalue,passvalue,typesvalue)
   {
       window.location.href="icard.aspx?cardNumber="+cardvalue+"&cardPassword="+passvalue+"&types="+typesvalue+"";
   }
 </script>
