<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_sysLabel_out" Codebehind="sysLabel_out.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="Form1" runat="server">

    <table id="top1" width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
    <tr>
      <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >标签管理</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="SysLabel_List.aspx" class="list_link">标签管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><label id="outlabel_type" runat="server" /></div></td>
    </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
      <tr>
        <td style="padding-left:15px;"><a href="SysLabel_List.aspx" class="list_link">标签管理</a>&nbsp;┊&nbsp;<a class="topnavichar" href="syslabel_bak.aspx">备份库</a>&nbsp;┊&nbsp;<a class="reshow" href="syslable_add.aspx">新建标签</a>&nbsp;┊&nbsp; <a  class="topnavichar" href="syslabelclass_add.aspx">新建分类</a>&nbsp;┊&nbsp;<a href="sysLabel_out.aspx?type=out" class="topnavichar" title="导出所有标签">导出标签</a><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_label_out_001',this)">(如何导出标签?)</span>&nbsp;┊&nbsp; <a href="sysLabel_out.aspx?type=in" class="topnavichar">导入标签</a><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_label_in_001',this)">(如何导入标签?)</span> <span id="Back" runat="server"></span></td>
      </tr>
    </table>

      <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" runat="server" id="out_table" class="table">
      <tr class="TR_BG">
        <td align="left" valign="middle" class="sysmain_navi" style="width:400px;">导出标签 <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_label_out_002',this)">帮助?</span></td>
      </tr>   
        <tr class="TR_BG_list">
            <td>
              <asp:Button ID="Button1" runat="server" Text="清空服务器上旧的导出的标签" CssClass="form" OnClientClick="{if(confirm('确认要清空以前导出的标签吗？')){return true;}return false;}" OnClick="label_clear_Click" />
             </td>
       </tr> 
       <tr class="TR_BG_list">
            <td>
             <div id="classShow" runat="server" />
             </td>
         </tr>
         <tr class="TR_BG_list">
            <td align="left">
             <asp:CheckBox ID="outSystem" Text="导出系统标签(内置标签)" runat="server" />&nbsp;&nbsp;<input name="label_out1" type="button" onclick="Export();" class="form" value="导出到本地" />
             </td>
        </tr>
        <tr class="TR_BG_list">
            <td align="left">
              <asp:Button ID="label_out" runat="server" Text="导出到服务器" CssClass="form" OnClientClick="{if(confirm('确定要导出标签到服务器吗？')){return true;}return false;}" OnClick="label_out_Click" />
             <asp:HiddenField ID="classID" runat="server" />
             <span class="reshow">导出标签，请保证您的/xml/label目录为可写.</span>
            </td>
        </tr>
          
      </table>
       <iframe id="label_export" src="about:blank" border="0" height="0" width="0" style="visibility: hidden"></iframe>
      <table width="98%" border="0" align="center" cellpadding="4" runat="server" id="in_table" cellspacing="1" class="table">
      <tr class="TR_BG">
        <td align="left" valign="middle" class="sysmain_navi" style="width:400px;">导入标签</td>
      </tr> 
      <tr class="TR_BG_list">
        <td align="left" valign="middle">
          <asp:CheckBox ID="ATserverTF" Text="选择服务器上的标签"  onclick="AtServer(this);" runat="server" />
        </td>
      </tr> 
      <tr class="TR_BG_list">
        <td align="left" style="height:50px;">
          <div id="localxmlPath" style="display:">
          <asp:FileUpload ID="xmlPath" Width="300" runat="server" CssClass="form" />&nbsp;
          </div>
            
          <div id="serverxmlPath" style="display:none;">
            <asp:TextBox ID="sxmlPath" Width="300" CssClass="form" runat="server"></asp:TextBox>
            <img src="../../sysImages/folder/s.gif" alt="选择xml源" border="0" style="cursor:pointer;" onclick="selectFile('xml',document.Form1.sxmlPath,280,500);document.Form1.sxmlPath.focus();" />
          </div>
          <asp:HiddenField ID="xmlPath_put" runat="server" />  
        </td>
           
        </tr>
        <tr class="TR_BG_list">
        <td align="left" style="height:20px;">
         <asp:Button ID="Button2" runat="server" Text="导入标签" CssClass="form" OnClientClick="getvalue();" OnClick="label_in_Click" />   <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_label_in_sfiles',this)">如何选择文件</span>
        </td>
        </tr>
      </table>
      
            
     </form>
     
  <br />
  <br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat="server" /></td>
  </tr>
</table>     
</body>
</html>
<script language="javascript" type="text/javascript">
function Export()
{
	var ifm = document.getElementById("label_export");
	var outtype=null;
	if(document.getElementById("classID").value=="alllabel")
	{
	    var outSystem = document.getElementById("outSystem");
	    if(outSystem.checked){outtype = "trues";}
	    else{outtype = "falses";}
	}
	var labelID = document.getElementById("classID").value;
	ifm.src = "syslabel_outLocal.aspx?classID="+labelID+"&outtype="+ outtype +"";
	return false;
}

function AtServer()
{
    if(document.getElementById("ATserverTF").checked)
    {
          document.getElementById("serverxmlPath").style.display="";
          document.getElementById("localxmlPath").style.display="none";
    }
    else
    {
          document.getElementById("serverxmlPath").style.display="none";
          document.getElementById("localxmlPath").style.display="";
    }
    
}
function getvalue()
{
    var xmlpath = null;
    if(document.getElementById("ATserverTF").checked)
    {
        xmlpath = document.getElementById("sxmlPath").value;
    }
    else
    {
        xmlpath = document.getElementById("xmlPath").value;
    }
    document.getElementById("xmlPath_put").value = xmlpath;
}
</script>
