<%@ Page Language="C#" AutoEventWireup="true" Inherits="Manage_System_Data_Backup" ResponseEncoding="utf-8" Codebehind="database.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<script language="javascript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">数据库维护</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />数据库维护</div></td>
    </tr>
  </table>
<table width="98%" border="0" cellpadding="0" cellspacing="0" align="center">
  <tr class="TR_BG_list">
    <td align="left"><table width="772" align="left" height="29" border="0" cellpadding="0" cellspacing="0" background="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/reght_1_bg_1.gif" >
        <tr>
          <td Width="193" class="m_down_bg" height="29" align="center" onclick="javascript:ChangeDiv('ExcuteSql')" style="cursor:hand;" id="TdExcuteSql">执行SQL语句</td>
          <td Width="193" class="m_up_bg" align="center" height="29" onclick="javascript:ChangeDiv('Bak')" style="cursor:hand;" id="TdBak">数据库备份</td>
          <td Width="193" class="m_up_bg" align="center" height="29" onclick="javascript:ChangeDiv('Replace')" style="cursor:hand;" id="TdReplace">数据库批量替换</td> 

          <td Width="193" class="m_up_bg" align="center" height="29" onclick="javascript:ChangeDiv('Rar')" style="cursor:hand;<% if (Foosun.Config.UIConfig.WebDAL == "Foosun.SQLServerDAL"){ Response.Write("display:none;");}%>" id="TdRar" >数据库压缩</td>
        </tr>
      </table></td>
  </tr>
  <tr class="TR_BG_list">
    <td class="list_link"><div id="DivExcuteSql" style="display">
        <form runat="server" id="FormSql">
          <table width="98%" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
            <tr class="TR_BG_list">
              <td class="sysmain_navi"><font size="2">执行SQL语句</font></td>
            </tr>
            <tr class="TR_BG_list">
              <td class="list_link">说明:一次只能执行一条Sql语句,如果你对SQL不熟悉,请尽量不要使用.否则一旦出错,将是致命的.<br>
                建议使用查询语句.如:select count(id) From Table,尽量不要使用delete,update等命令.</td>
            </tr>
            <tr class="TR_BG_list">
              <td class="list_link" align="left"><textarea name="SqlText" style="width:99%;" cols="4" rows="4"><%Response.Write(Request.Form["SqlText"]); %>
</textarea>
                <span id="ErrorMsg" class="reshow"></span></td>
            </tr>
            <tr class="TR_BG_list">
              <td class="list_link" style="height: 1px"><div id="ResultShow" runat="server" style="width:98%;"></div></td>
            </tr>
            <tr class="TR_BG_list">
              <td class="list_link"><label>
                <input type="submit" name="Excute" value=" 执 行 " class="form" onclick="javascript:return Is_Excute();" />
                </label>
                <label>
                <input type="reset" name="UnDo" value=" 重 填 " class="form" onclick="javascript:document.getElementById('ResultShow').innerHTML='';" />
                </label>
                <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_Db_001',this)">帮助</span> </td>
            </tr>
          </table>
        </form>
      </div>
      <% if (Foosun.Config.UIConfig.WebDAL == "Foosun.SQLServerDAL")
         { 
      %>
      <div id="DivBak" style="display:none">
        <table width="98%" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
          <tr class="TR_BG_list">
            <td class="sysmain_navi"><font size="2">数据库备份</font></td>
          </tr>
          <tr class="TR_BG_list">
            <td class="list_link" height="40">
                <select id="Type">
                    <option selected="selected" value="1" class="form" style="width:100px;">主数据库</option>
                    <option value="2">帮助库</option>
                    <option value="3">采集库</option>
                </select>
                <input type="button" name="DbBak" value=" 开 始 备 份 " class="form" onclick="javascript:BakStart();"/>
              <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onClick="Help('H_Db_002',this)">帮助</span></td>
          </tr>
          <tr class="TR_BG_list">
            <td class="list_link">说明:请在备份完成后,及时删除备份文件,以防止别人恶意下载数据库文件.(<span style="color:Red;">只支持网站与数据库在同一服务器</span>)</td>
          </tr>
        </table>
      </div>
      <div id="DivRar" style="display:none"></div>
      <% }
         else
         {
      %>
      <div id="DivBak" style="display:none">
        <table width="98%" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
          <tr class="TR_BG_list">
            <td class="sysmain_navi"><font size="2">数据库备份(只针对Access数据库)</font></td>
          </tr>
          <tr class="TR_BG_list">
            <td class="list_link" height="40"><select id="Type" runat="server" visible="false">
                </select><input type="button" name="DbBak" value=" 开 始 备 份 " class="form" onclick="javascript:BakStart();"/>
              <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onClick="Help('H_Db_002',this)">帮助</span></td>
          </tr>
          <tr class="TR_BG_list">
            <td class="list_link">说明:请在备份完成后,及时删除备份文件,以防止别人恶意下载数据库文件.</td>
          </tr>
        </table>
      </div>
      <div id="DivRar" style="display:none">
        <table width="98%" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
          <tr class="TR_BG_list">
            <td class="sysmain_navi"><font size="2">数据库压缩(只针对Access数据库)</font></td>
          </tr>
          <tr class="TR_BG_list">
            <td class="list_link" height="40"><input type="button" name="DbRar" value=" 开 始 压 缩 " class="form" onclick="javascript:RarStart();"/>
              <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onClick="Help('H_Db_003',this)">帮助</span></td>
          </tr>
          <tr class="TR_BG_list">
            <td class="list_link">说明:压缩前请备份您的数据库,以防止万一.</td>
          </tr>
        </table>
      </div>
      <% } %>
      <div id="DivReplace" style="display:none">
        <table width="98%" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
          <tr class="TR_BG_list">
            <td class="sysmain_navi"><font size="2">数据库批量替换</font></td>
          </tr>
          <tr class="TR_BG_list">
            <td class="list_link">
            <div >表名 
            <select id="TableName" name="TableName" class="form" style="width:180px;" onchange="javascript:getFieldName(this.value);">
                <option value="0">请选择</option>
                <% getTableList(); %>
            </select>
            字段名
            <span id="spanFieldName"><select id="fieldname" name="fieldname" class="form" style="width:180px;"></select></span>
            原始字符 
            <input id="OldTxt" type="text" class="form" style="width:110px;" value="原始字符" onfocus="javascript:this.value='';"/>    
            新字符        
            <input id="NewTxt" type="text" class="form" style="width:110px;" value="新字符" onfocus="javascript:this.value='';"/></div> <br />  
            <div align="center">
            <input type="button" name="DbReplace" value=" 开 始 替 换 " class="form" onclick="javascript:ReplaceStart();"/></div> 
              </td>
          </tr>
          <tr class="TR_BG_list">
            <td class="list_link">说明:替换数据库里面的字符.执行操作前请备份好数据库,慎重操作.</td>
          </tr>
        </table>
      </div>
      </td>
  </tr>
</table>
<br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
  <tr>
    <td align="center"><label id="copyright" runat="server" /></td>
  </tr>
</table>
</body>
<script language="javascript" type="text/javascript">
function ChangeDiv(ID)
{
	document.getElementById("TdExcuteSql").className='m_up_bg';
	document.getElementById("TdBak").className='m_up_bg';
	document.getElementById("TdRar").className='m_up_bg';
	document.getElementById("TdReplace").className='m_up_bg';
	document.getElementById("Td"+""+ID+"").className='m_down_bg';

	document.getElementById("DivExcuteSql").style.display="none";
	document.getElementById("DivBak").style.display="none";
	document.getElementById("DivRar").style.display="none";
	document.getElementById("DivReplace").style.display="none";
	document.getElementById("Div"+""+ID+"").style.display="";
}
function Is_Excute()
{
    if(document.FormSql.SqlText.value=="")
    {
        document.getElementById("ErrorMsg").innerHTML='请输入要执行的SQL语句';
        return false;
    }
    if(confirm('确定要执行SQL语句吗?\n如果操作不当将会造成意想不到的损失!'))
    {
        document.FormSql.action='?Action=Sql';  
        return true;
    }
    else
    {
        return false;
    }
}
function BakStart()
{
   // alert("bakstart");
    self.location.href='?Action=Bak&Type=1';
}
function RarStart()
{
    self.location.href='?Action=Rar';
}
function ReplaceStart()
{
    if(document.getElementById("TableName").value=="0")
    {
        alert('请选择表');
        return false;
    }
    self.location.href='?Action=Replace&TableName='+document.getElementById("TableName").value+'&FieldName='+document.getElementById("fieldname").value+'&NewTxt='+document.getElementById("NewTxt").value+'&OldTxt='+document.getElementById("OldTxt").value;
}
</script>
<%  Show(); %>
</html>
<script language="javascript" type="text/javascript">
function getFieldName(value)
{
    if(value=="0")
    {
        document.getElementById("spanFieldName").innerHTML="<select id=\"fieldname\" name=\"fieldname\" class=\"form\" style=\"width:180px;\"></select>";    
        return false;
    }
    var Action='Action=getFieldName&TableName='+value;
    var options={ 
            method:'get', 
            parameters:Action, 
            onComplete:function(transport) 
            { 
                var returnvalue=transport.responseText; 
                if (returnvalue.indexOf("??")>-1) 
                    alert('未知错误!请联系管理员'); 
                else 
                    document.getElementById("spanFieldName").innerHTML=returnvalue; 
            } 
        }; 
    new  Ajax.Request('database.aspx?no-cache='+Math.random(),options);
}
</script>