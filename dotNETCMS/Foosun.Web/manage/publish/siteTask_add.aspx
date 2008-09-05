<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_publish_siteTask_add" Codebehind="siteTask_add.aspx.cs" %>

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
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">ƻ</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px"><div align="left">λõ<a href="../main.aspx" target="sys_main" class="list_link">ҳ</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="siteTask.aspx" class="list_link">ƻ</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /></div></td>
    </tr>
  </table>
 <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="Navitable">
    <tr>
      <td height="18" style="width: 45%" colspan="2" style="PADDING-LEFT: 14px"><div align="left"> <a href="siteTask.aspx" class="topnavichar">ҳ</a>  <a href="siteTask_add.aspx?type=base" class="topnavichar">½</a></div>
      </td>
    </tr>
  </table>
  <div id="NoContent" runat="server"></div>
  <table width="98%" border="0" cellpadding="5" align="center" cellspacing="1" class="table" id="base">
    <tr class="TR_BG" id="base_tr">
              <td align="left" colspan="2" class="list_link"><strong></strong></td>
    </tr>
    <tr class="TR_BG_list" id="base_name">
      <td  align="right" style="width: 178px; height: 24px;" class="list_link">:</td>
      <td align="Left" class="list_link" style="height: 24px" ><asp:TextBox ID="TaskName" MaxLength="50" runat="server" CssClass="form"/>
        (<font color=red size=2>*</font>)<asp:RequiredFieldValidator ID="TaskNamee" runat="server" ControlToValidate="TaskName" Display="Dynamic" ErrorMessage="<span class=reshow>(*)ƲΪ!</span>"></asp:RequiredFieldValidator><span class="helpstyle" onclick="Help('H_task_0001',this)" style="cursor: help;" title="鿴"></span></td>
    </tr>
    <tr class="TR_BG_list" id="base_index">
      <td align="right"  class="list_link" style="width: 178px">ҳ</td>
      <td  align="left" class="list_link"><asp:CheckBox ID="isIndex" runat="server" Checked="true"/>
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0002',this)"></span></td>
    </tr>
    <tr class="TR_BG_list" id="IsTimee">
      <td align="right"  class="list_link" style="width: 178px">ʱ</td>
      <td  align="left" class="list_link"><select name="TimeSet" id="TimeSet" runat="server" multiple="true" style="width: 100px; height: 100px">
      <option value="0">ʱ</option>
      <option value="1">1</option>
      <option value="2">2</option>
      <option value="3">3</option>
      <option value="4">4</option>
      <option value="5">5</option>
      <option value="6">6</option>
      <option value="7">7</option>
      <option value="8">8</option>
      <option value="9">9</option>
      <option value="10">10</option>
      <option value="11">11</option>
      <option value="12">12</option>
      <option value="13">13</option>
      <option value="14">14</option>
      <option value="15">15</option>
      <option value="16">16</option>
      <option value="17">17</option>
      <option value="18">18</option>
      <option value="19">19</option>
      <option value="20">20</option>
      <option value="21">21</option>
      <option value="22">22</option>
      <option value="23">23</option>
      <option value="24">24</option></select>
      <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0012',this)"></span></td>
    </tr>
     <tr class="TR_BG_list" id="base_time" style="display:none;">
      <td align="right"  class="list_link" style="width: 178px"> ʱ䣺</td>
      <td  align="left" class="list_link"><asp:TextBox ID="CreatTime" runat="server" Width="124px" CssClass="form" Enabled="false"/>
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0003',this)"></span></td>
    </tr>
     <tr class="TR_BG" id="class_tr">
              <td align="left" colspan="2" class="list_link"><strong>Ŀ</strong></td>
    </tr>
    <% string str_publicType = Foosun.Config.verConfig.PublicType;
       if (str_publicType == "1")
       {
    %>
    <tr class="TR_BG_list" id="class_index1" >
      <td align="right"  class="list_link" style="width: 178px">Ŀ</td>
      <td  align="left" class="list_link"><asp:CheckBox ID="AllClass1" runat="server" />Ŀҳ
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0004',this)"></span><br />
        <asp:CheckBox ID="EveryDayClass1" runat="server" />ÿһҳķʽĿ
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0005',this)"></span><br />
        <asp:CheckBox ID="TodayClass1" runat="server" />ֻɽĿ
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0006',this)"></span></td>
    </tr>
   <% 
        }
        else
        {
   %>
   <tr class="TR_BG_list" id="class_index2" >
      <td align="right"  class="list_link" style="width: 178px">Ŀ</td>
      <td  align="left" class="list_link"><asp:CheckBox ID="AllClass0" runat="server" />Ŀҳ
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0004',this)"></span><br />
        <asp:CheckBox ID="TodayClass0" runat="server" />ֻɽĿ
        <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0006',this)"></span></td>
    </tr>
   <%
        }
   %>
    <tr class="TR_BG_list" id="class_class">
      <td align="right"  class="list_link" style="width: 178px">Ŀ</td>
      <td  align="left" class="list_link"><div id="divClassNews" runat="server" style="padding-bottom:0;padding-left:0;padding-right:0;padding-top:0;" align="left"></div><span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0007',this)"></span></div></td>
    </tr>
     <tr class="TR_BG" id="news_tr">
              <td align="left" colspan="2" class="list_link"><strong></strong></td>
    </tr>
    
    <tr class="TR_BG_list" id="Tr1" >
      <td align="right"  class="list_link" style="width: 178px">ŷʽ</td>
      <td  align="left" class="list_link">
      <input type="radio" runat="server" id="AllNews" checked="true" onclick="DispChange(9)"/>&nbsp;
      <input type="radio" runat="server" id="NewsID" onclick="DispChange(0)"/>ID&nbsp;
      <input type="radio" runat="server" id="Data" onclick="DispChange(1)"/>&nbsp;
      <input type="radio" runat="server" id="LastNewsNum_checkbox" onclick="DispChange(2)"/>
          <asp:DropDownList ID="unHTML" runat="server">
          <asp:ListItem Value="0" Text="״̬"></asp:ListItem>
          <asp:ListItem Value="1" Text="δɵ"></asp:ListItem>
          <asp:ListItem Value="2" Text="ɵ"></asp:ListItem>
          </asp:DropDownList>
      </td>
    </tr>
    <tr class="TR_BG_list" id="newsidd"  style="display:none" align="right">
      <td align="right"  class="list_link" style="width: 178px">IDΧ</td>
      <td  align="left" class="list_link"> <asp:TextBox ID="NewsID1" runat="server" CssClass="form" Width="66px"/>  <asp:TextBox ID="NewsID2" runat="server" CssClass="form" Width="66px"/><span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0009',this)"></span></td>
    </tr>
    
    <tr class="TR_BG_list" id="newsdata" style="display:none">
      <td align="right"  class="list_link" style="width: 178px">ɣ</td>
      <td  align="left" class="list_link">
       <asp:TextBox ID="Data1" runat="server" CssClass="form" Width="120px" /> 
      <img src="../../sysImages/folder/s.gif" alt="ѡԴ" border="0" style="cursor:pointer;" onclick="selectFile('date',document.form1.Data1,150,450);document.form1.Data1.focus();" /> 
       <asp:TextBox ID="Data2" runat="server" CssClass="form" Width="120px" /> 
      <img src="../../sysImages/folder/s.gif" alt="ѡԴ" border="0" style="cursor:pointer;" onclick="selectFile('date',document.form1.Data2,150,450);document.form1.Data2.focus();" /> 
      <span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0010',this)"></span></td>
    </tr>
    
    <tr class="TR_BG_list" id="lastnumm" style="display:none" >
      <td align="right"  class="list_link" style="width: 178px"></td>
      <td  align="left" class="list_link">ɵ:<asp:TextBox ID="LastNewsNum" runat="server" CssClass="form" Width="66px">10</asp:TextBox>&nbsp;<span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0011',this)"></span></td>
    </tr>
        
    <tr class="TR_BG_list" id="news_class" >
      <td align="right"  class="list_link" style="width: 178px">Ŀ</td>
      <td  align="left" class="list_link"><div id="divClassClass" runat="server" style="padding-bottom:0;padding-left:0;padding-right:0;padding-top:0;" align="left"></div><span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_0007',this)"></span></div></td>
    </tr>
     <tr class="TR_BG" id="special_tr">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">ר</span></strong></td>
    </tr>
         <tr class="TR_BG_list" id="special_sp" >
      <td align="right"  class="list_link" style="width: 178px">רⷢ</td>
      <td  align="left" class="list_link"><div id="DivSpecial" runat="server" style="padding-bottom:0;padding-left:0;padding-right:0;padding-top:0;" align="left"></div><span class="helpstyle" style="cursor:help;" title="鿴" onClick="Help('H_task_00013',this)"></span></div></td>
    </tr>
    <tr class="TR_BG_list" id="save">
       <td align="center" colspan="2" class="list_link"><label>
         <input type="submit" name="Save" value="   " class="form" id="Savetask" runat="server" onserverclick="Savetask_ServerClick" /></label>
                &nbsp;&nbsp;<label>
            <input type="reset" name="Clear" value="   " class="form" id="Cleartask" runat="server"/></label>
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
function DispChange(value)
{
    switch(parseInt(value))
    {
        case 0:
        document.getElementById("newsidd").style.display="";
        document.getElementById("newsdata").style.display="none";
        document.getElementById("lastnumm").style.display="none";
        break;
        case 1:
        document.getElementById("newsidd").style.display="none";
        document.getElementById("newsdata").style.display="";
        document.getElementById("lastnumm").style.display="none";
        break;
        case 2:
        document.getElementById("newsidd").style.display="none";
        document.getElementById("newsdata").style.display="none";
        document.getElementById("lastnumm").style.display="";
        break;
        case 9:
        document.getElementById("newsidd").style.display="none";
        document.getElementById("newsdata").style.display="none";
        document.getElementById("lastnumm").style.display="none";
        break;
    }
}
</script>
</html>


