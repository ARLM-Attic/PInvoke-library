<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_publish_psf_edit" Codebehind="psf_edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
   <form id="form1" runat="server">
   <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">PSFӵ</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">λõ<a href="../main.aspx" class="list_link" target="sys_main">ҳ</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="psf.aspx" class="list_link">PSFӵ</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />޸</div></td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
    <tr>
      <td height="18" style="width: 45%" colspan="2"style="PADDING-LEFT: 14px"><div align="left"><a href="psf.aspx" class="topnavichar">ҳ</a><a href="psf_add.aspx" class="topnavichar">½ӵ</a></div>
      </td>
    </tr>
  </table>
  <div id="NoContent" runat="server"></div>
  <table width="98%" border="0" cellpadding="5" align="center" cellspacing="1" class="table" id="OM_AddNew">
    <tr class="TR_BG_list">
      <td colspan="2" class="list_link"><strong>޸PSFӵ</strong></td>
    </tr>
    <tr class="TR_BG_list">
      <td  align="right" style="width: 186px; height: 28px;" class="list_link">ӵ:</td>
      <td align="Left" class="list_link" style="height: 28px" ><asp:TextBox ID="psfName" MaxLength="50" runat="server" CssClass="form"/>
        (<font color=red size=2>*</font>)<asp:RequiredFieldValidator ID="psfNamere" runat="server" ControlToValidate="psfName" Display="Dynamic" ErrorMessage="<span class=reshow>(*)ӵƲΪ!</span>"></asp:RequiredFieldValidator><span class="helpstyle" onclick="Help('H_psf_0001',this)" style="cursor: help;" title="鿴"></span></td>
    </tr>
    
    <tr class="TR_BG_list">
      <td align="right" class="list_link" style="width: 186px" valign="top">ӵĿ¼ϵ:</td>
      <td align="left"><div id="DivadTxt" runat="server"></div></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 178px">ϴļĿ¼µݣ</td>
      <td  align="left" class="list_link"><asp:CheckBox ID="isAll" runat="server" />
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_psf_0007',this)"></span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 186px">ϴĿ¼µļ</td>
      <td  align="left" class="list_link"><asp:CheckBox ID="isSub" runat="server" />
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_psf_0003',this)"></span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="right"  class="list_link" style="width: 186px"> ʱ䣺</td>
      <td  align="left" class="list_link"><asp:TextBox ID="CreatTime" runat="server" Width="124px" CssClass="form" Enabled="false"/>
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_psf_0005',this)"></span></td>
    </tr>
    <tr class="TR_BG_list">
       <td align="center" colspan="2" class="list_link" style="height: 34px"><label>
         <input type="submit" name="Save" value="   " class="form" id="SavePsf" runat="server" onserverclick="SavePsf_ServerClick"/></label>
                &nbsp;&nbsp;<label>
            <input type="reset" name="Clear" value="   " class="form" id="ClearPsf" runat="server"/></label>
        </td>
     </tr>
    </table>
  </form>
    <br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat=server /></td>
  </tr>
</table>
</body>
<script language="javascript">
setCookie("psf_txt_num",0);
function psf_add()
{
    var num = 0;
    if(getCookie("psf_txt_num") != null || getCookie("psf_txt_num")!= "null")
    {
	    num = parseInt(getCookie("psf_txt_num"));
	    if(num>99)
	    {
	        return;
	    }
	    num = num +1;
	    setCookie("psf_txt_num",num);
	}
    var chars = "1234567890";
    var randNum = makeRandChar(chars,20);
    var tempstr = "<div id='"+randNum+"'>Ŀ¼ <input name='LocalDir' type='text' style='width:130px;' maxlength='200' value='' class='form' id='LocalDir' /> Զ̷Ŀ¼ <input name='RemoteDir' type='text' id='RemoteDir' value='' style='width:130px;' maxlength='100' class='form' /> <a href='#'onclick='psf_delete(this.parentNode)' class='list_link'>ɾ</a>(<font color='red'>*</font>)</div>"; 
    document.getElementById("temp").innerHTML+=tempstr;
}
function psf_delete(divobj)
{
    divobj.parentNode.removeChild(divobj);  
    var num = parseInt(getCookie("psf_txt_num"));
    num = num - 1;
	setCookie("psf_txt_num",num);
}

function makeRandChar(chars,marklen)
{//
    var tmpstr = '';
    var chrlen = chars.length;
    var iRandom ;
    do{
        iRandom = Math.round(Math.random() * chrlen);
        tmpstr += chars.charAt(iRandom);
        if( tmpstr.length == marklen ) break;    
    }while (tmpstr.length < marklen)
    return tmpstr;
}
</script>
</html>