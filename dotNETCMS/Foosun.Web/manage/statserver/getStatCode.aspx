<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getStatCode.aspx.cs" Inherits="Manage_statserver_getStatCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
      <table width="90%" border="0" align="center" cellpadding="0" cellspacing="1">
        <tr class="TR_BG_list">
          <td colspan="2" class="list_link">统计调用代码为:</td>
        </tr>
        <tr class="TR_BG_list">
          <td colspan="2" class="list_link">
              <textarea name="textfield" style="width:98%;height:80px;" id="CodePath" runat="server" class="form" cols="20" rows="4"></textarea>
            </td>
        </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="padding-top:14px;padding-bottom:14px;"><div align="center">
              <input type="button" name="Submit" value=" 关 闭 " onClick="window.close();" class="form">
            </div></td>
          <td class="list_link" style="padding-top:14px;padding-bottom:14px;"><div align="center">
              <input type="button" name="copy" value=" 复 制 " onClick="copyToClipBoard();"  class="form">
            </div></td>
        </tr>
      </table>
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 30px">
      <tr>
        <td align="center"><label id="copyright" runat=server /></td>
      </tr>
    </table>

    </form>
</body>
</html>
<script  language="JavaScript">   
function copyToClipBoard()
{ 
    if(confirm("确定复制到剪贴板吗?\n如果你是火狐(FireFox)浏览器用户，请直接复制以上代码!"))
    {
        var clipBoardContent=document.getElementById("CodePath").value;
        window.clipboardData.setData("Text",clipBoardContent);
	}
	alert("复制成功");
}
</script>