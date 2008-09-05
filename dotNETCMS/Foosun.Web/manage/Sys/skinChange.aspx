<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_Sys_skinChange" Codebehind="skinChange.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body onload="lsrc();">
    <form id="form1" runat="server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px"">更改皮肤</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />更换皮肤</div></td>
        </tr>
      </table>
      <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG_list">
          <td align="center" class="navi_link" style="width: 13%">选择皮肤风格</td>
          <td Width="90%" align="left">
          <label id="skinlist" runat="server" />
          <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_changeSkin_001',this)">帮助</span>
          </td>
        </tr>
        <tr class="TR_BG_list">
          <td align="center" class="navi_link" style="width: 13%"></td>
          <td width="90%" align="left"> 
             <table border="0" cellpadding="1" cellspacing="0" class="table" style="height:194px;width:300px;">
               <tr>
                 <td align="center"><img src="" id="dsrc" /></td>
               </tr>
             </table>
           </td>
        </tr>
         <tr class="TR_BG_list">
          <td align="center" class="navi_link" style="width: 13%"></td>
          <td width="90%" align="left"> 
           <asp:Button ID="buttons" runat="server" CssClass="form" OnClientClick="{if(confirm('您确定要更改皮肤？\n更改后，前台会员皮肤也将改变\n警告：改变皮肤后，您可能需要重新登陆系统！')){return true;}return false;}" OnClick="buttonsave" Text="保存皮肤" />
           
           </td>
        </tr>
        </table>
    </form><br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
 </table>
</body>
</html>
<script language="javascript" type="text/javascript">
    function lsrc()
    {
        var gskin = document.getElementById("styleDir").value;
        var gimg = document.getElementById("dsrc");
        switch(gskin)
        {
	        case "blue":
	           gimg.src="../../sysImages/skinreview/1.gif";
	           break;
	        case "red":
	           gimg.src="../../sysImages/skinreview/2.gif";
	           break;
	        case "sblue":
	           gimg.src="../../sysImages/skinreview/3.gif";
	           break;
	        case "green":
	           gimg.src="../../sysImages/skinreview/4.gif";
	           break;
	        case "blackwhite":
	           gimg.src="../../sysImages/skinreview/5.gif";
	           break;
	        case "default":
	           gimg.src="../../sysImages/skinreview/6.gif";
	           break;
        }
    }
</script>