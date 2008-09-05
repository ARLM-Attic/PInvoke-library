<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_Site_add" Codebehind="Site_add.aspx.cs" %>

<%@ Register Src="../../controls/UserPop.ascx" TagName="UserPop" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
  <title></title>
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" charset="gb2312" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
<!--
//站群是否外部站群选项变化
function DispChange()
{
    var tab = document.getElementById("tabList");
    var obj1 = document.getElementById("ChbType");
    var bouter = obj1.checked;
    var obj2 = document.getElementById("chkAdvance");
    var bshowad = obj2.checked;
    var obj3 = document.getElementById("captionadv");
    for(var i=0;i<tab.rows.length-1;i++)
    {
        var obj = tab.rows[i];
        var tagname = obj.id;
        var stydis = "";
        if(i<14)
        {
            if((bouter && tagname.charAt(0) == "N")
                || (!bouter && tagname.charAt(0) == "Y"))
                stydis = "NONE";
        }
        else if(i>14)
        {
            if(bshowad)
            {
                if((bouter && tagname.charAt(0) == "N")
                || (!bouter && tagname.charAt(0) == "Y"))
                stydis = "NONE";
                obj3.innerText = "关闭高级设置选项";          
            }
            else
            {
                stydis = "NONE";
                obj3.innerText = "显示高级设置选项";  
            }
        }
        obj.style.display = stydis;
    }
}

//选择生成格式是否静态/动态生成
function StaticGenrate(flag)
{
    var obj1 = document.getElementById("divdir");
    var obj2 = document.getElementById("divfile");
    if(flag)
    {
        obj1.style.display = "";
        obj2.style.display = "";
        var ti = document.getElementById("DdlIndexEXName");
        ti.selectedIndex = 1;
        ti.disabled = false;
        ti = document.getElementById("DdlClassEXName");
        ti.selectedIndex = 1;
        ti.disabled = false;
        ti = document.getElementById("DdlNewsEXName");
        ti.selectedIndex = 1;
        ti.disabled = false;
        ti = document.getElementById("DdlOtherEXName");
        ti.selectedIndex = 1;
        ti.disabled = false;
    }
    else
    {
        obj1.style.display = "NONE";
        obj2.style.display = "NONE";
        var ti = document.getElementById("DdlIndexEXName");
        ti.selectedIndex = 0;
        ti.disabled = true;
        ti = document.getElementById("DdlClassEXName");
        ti.selectedIndex = 0;
        ti.disabled = true;
        ti = document.getElementById("DdlNewsEXName");
        ti.selectedIndex = 0;
        ti.disabled = true;
        ti = document.getElementById("DdlOtherEXName");
        ti.selectedIndex = 0;
        ti.disabled = true;
    }
}

function Load()
{
    var obj = document.getElementById("RadFileType_1");
    if(obj.checked)
    {
        var ti = document.getElementById("DdlIndexEXName");
        ti.disabled = true;
        ti = document.getElementById("DdlClassEXName");
        ti.disabled = true;
        ti = document.getElementById("DdlNewsEXName");
        ti.disabled = true;
        ti = document.getElementById("DdlOtherEXName");
        ti.disabled = true;
        var obj1 = document.getElementById("divdir");
        obj1.style.display = "NONE";
        var obj2 = document.getElementById("divfile");
        obj2.style.display = "NONE";
    }
}
function GetPositionHtml(obj)
{
    var s = obj.value.trim();
    if(s != '')
    {
        document.getElementById('TxtNaviPosition').value = '<a href="/" target="_blank">首页</a> >> {@ParentClassStr} >> '+ s;
        if(!document.getElementById('TxtEnName').disabled )
            document.getElementById('TxtEnName').value = GetPY(s);
    }
}
//-->
</script>
</head>
<body onload="//DispChange();//Load();">
<form id="Form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" ><asp:Label runat="server" ID="LblCaption"/></td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Site_List.aspx" target="sys_main" class="list_link">站群管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><asp:Label runat="server" ID="LblNavigation"/></div></td>
        </tr>
    </table>
    <table id="tabList" width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
            <tr id="T_Base" class="TR_BG"> 
              <td class="sys_topBg" colspan="2">基本设置</td>
            </tr>
            <tr id="B_1" class="TR_BG_list"> 
              <td class="list_link" width="30%">站群中文名称：</td>
              <td class="list_link" width="70%"><asp:TextBox ID="TxtCnName" onchange="GetPositionHtml(this)" runat="server" MaxLength="50" Width="280" CssClass="form" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtCnName"
                      ErrorMessage="请输入站群中文名称!" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>&nbsp;<asp:RegularExpressionValidator
                          ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtCnName"
                          Display="Dynamic" ErrorMessage="请输入2-50个有效字符!" ValidationExpression="^[^']{2,50}$"></asp:RegularExpressionValidator>
                  <asp:Label ID="LblID" runat="server" Visible="False"></asp:Label><span style="color: #ff0000"></span></td>
            </tr>
            <tr id="B_2" class="TR_BG_list"> 
              <td class="list_link">站群英文名称：</td>
              <td  class="list_link"><asp:TextBox ID="TxtEnName" runat="server" MaxLength="50" Width="280" CssClass="form"/>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtEnName"
                      Display="Dynamic" ErrorMessage="请输入站群英文名称!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtEnName"
                      Display="Dynamic" ErrorMessage="站群英文名称只能输入3-50个英文字母或数字!" SetFocusOnError="True"
                      ValidationExpression="^[a-zA-Z0-9]{3,50}$"></asp:RegularExpressionValidator>&nbsp;
                  <asp:Label ID="LblCID" runat="server" Visible="False"></asp:Label>
                  <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_EName',this)">英文名称命名规则</span>
                  </td>
            </tr>
            <tr id="B_3" class="TR_BG_list"> 
              <td  class="list_link">项目名称：</td>
              <td  class="list_link"><asp:TextBox ID="TxtItemName" runat="server" MaxLength="50" Width="280" CssClass="form"/>
              &nbsp;<span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_ItemCName',this)">什么叫项目名称?</span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtItemName"
                      ErrorMessage="请输入项目名称!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                      </td>
            </tr>
            <tr id="B_4" class="TR_BG_list"  style="display:none;"> 
              <td  class="list_link">
                  站群状态：</td>
              <td  class="list_link">
                 <asp:RadioButtonList ID="RadStatus" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Value="0">开启</asp:ListItem>
                      <asp:ListItem Value="1">锁定</asp:ListItem>
                  </asp:RadioButtonList></td>
            </tr>
            <tr id="B_5" class="TR_BG_list" style="display:none;">
              <td class="list_link">站群类型：</td>
              <td class="list_link">
                  <asp:CheckBox ID="ChbType" runat="server" Text="外部站群" onclick="DispChange()" /></td>
            </tr>
            <tr id="N_40" class="TR_BG_list"  style="display:none;">
              <td class="list_link">使用数据库表：</td>
              <td class="list_link">
                  <asp:DropDownList ID="DdlDataTable" runat="server" CssClass="form">
                  </asp:DropDownList>
                  &nbsp;<span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_DataTable',this)">数据库表</span>
                  </td>
            </tr>
            <tr id="B_7" class="TR_BG_list"  style="display:none;"> 
              <td  class="list_link">是否在导航中显示：</td>
              <td  class="list_link">
                  <asp:CheckBox ID="ChbShowNavi" runat="server" Text="在导航中显示" />
                  &nbsp;<span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_IsShow',this)">帮助</span>
                  </td>
            </tr>
            <tr id="Y_8" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">外部站群地址：</td>
             <td  class="list_link"><asp:TextBox ID="TxtUrl" runat="server" MaxLength="200" Width="280" CssClass="form"/>
             &nbsp;<span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_EName_02',this)">外部站群</span>
             </td>
            </tr>
            <tr id="N_9" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">站群主页模板地址：</td>
             <td  class="list_link"><asp:TextBox ID="TxtIndxTmp" runat="server" MaxLength="200" Width="280" CssClass="form" />&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择模板" onclick="selectFile('templet',document.Form1.TxtIndxTmp,280,500);document.Form1.TxtIndxTmp.focus();" /></td>
            </tr>
             <tr id="N_10" class="TR_BG_list"> 
              <td class="list_link">站群栏目模板地址：</td>
             <td  class="list_link"><asp:TextBox ID="TxtClsTmp" runat="server" MaxLength="200" Width="280" CssClass="form"/>&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择模板" onclick="selectFile('templet',document.Form1.TxtClsTmp,280,500);document.Form1.TxtClsTmp.focus();" /></td>
            </tr>
             <tr id="N_11" class="TR_BG_list"> 
              <td class="list_link">站群新闻浏览模板地址：</td>
             <td  class="list_link"><asp:TextBox ID="TxtBrwTmp" runat="server" MaxLength="200" Width="280" CssClass="form" />&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择模板" onclick="selectFile('templet',document.Form1.TxtBrwTmp,280,500);document.Form1.TxtBrwTmp.focus();" /></td>
            </tr>
             <tr id="N_12" class="TR_BG_list"> 
              <td class="list_link">站群专题模板地址：</td>
             <td  class="list_link"><asp:TextBox ID="TxtSpcTmp" runat="server" MaxLength="200" Width="280" CssClass="form" />&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择模板" onclick="selectFile('templet',document.Form1.TxtSpcTmp,280,500);document.Form1.TxtSpcTmp.focus();" /></td>
            </tr>
            <tr id="N_13" class="TR_BG_list" runat="server"  style="display:none;">  
                <td  class="list_link">站群生成格式：</td>
                    <td class="list_link">
                    <asp:DropDownList ID="DdlType" runat="server" CssClass="form">
                        <asp:ListItem Value="0">普通新闻站格式</asp:ListItem>
                        <asp:ListItem Value="1">传媒版、门户版格式</asp:ListItem>
                        <asp:ListItem Value="2">不限制</asp:ListItem>
                     </asp:DropDownList>
                      <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_MakeType',this)">生成格式</span></td>
           </tr>
           <!------------------------>
            <tr id="T_Advnc" class="TR_BG"  style="display:none;">
                <td colspan="2" class="sys_topBg"><input type="checkbox" id="chkAdvance" onclick="DispChange()" checked="CHECKED" /><a id="captionadv">显示高级设置选项</a></td>
            </tr>
           <tr id="N_14" class="TR_BG_list"> 
              <td class="list_link">站群域名：</td>
              <td  class="list_link"><asp:TextBox ID="TxtDomain" runat="server" MaxLength="100" Width="280" CssClass="form"/><span style="color: #ff0000"></span>
              <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_domain',this)">域名</span></td>
           </tr>  
            <tr id="N_38" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">浏览权限：</td>
              <td  class="list_link">
                  <uc2:UserPop ID="UserPop1" runat="server" />
              </td>
            </tr>
            <tr id="N_16" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">此站群录入的信息是否需要审核：</td>
              <td  class="list_link">
                  <asp:CheckBox ID="ChbAuditing" runat="server" Text="需要审核" /></td>
            </tr>
             <tr id="N_17" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">站群META关键字：</td>
              <td class="list_link"><asp:TextBox ID="TxtKeywords" runat="server" TextMode="MultiLine" MaxLength="100" Width="280" Height="81px" CssClass="form"></asp:TextBox><span style="color: #ff0000"></span>
              <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_metaKeywords',this)">Meta关键字</span></td>
            </tr>
            <tr id="N_39" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">站群META描述：</td>
              <td  class="list_link">
                  <asp:TextBox ID="TxtDescribe" runat="server" MaxLength="200" TextMode="MultiLine" Width="280" Height="81px" CssClass="form"></asp:TextBox>
                  <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_metadesc',this)">Meta描述</span></td>
            </tr>
             <tr id="N_19" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">是否允许投稿：</td>
              <td  class="list_link">
                  <asp:CheckBox ID="ChbContribute" runat="server" Text="允许投稿" />
                  <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_isConstr',this)">允许投稿</span>
                  </td>
            </tr>
             
            <tr id="N_20" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">
                  允许上传文件格式：</td>
              <td  class="list_link">
                  <asp:TextBox ID="TxtUpFileType" runat="server" MaxLength="150" Width="280" CssClass="form"></asp:TextBox>
                  <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_upfiletype',this)">上传格式</span>
                  </td>
            </tr>
            <tr id="N_21" class="TR_BG_list"  style="display:none;"> 
              <td class="list_link">上传文件大小限制：</td>
              <td  class="list_link">
                  <asp:TextBox ID="TxtUpFileSize" runat="server" MaxLength="10" Width="280" CssClass="form">10240</asp:TextBox>KB<span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_upfilesize',this)">上传允许大小</span>
                  <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtUpFileSize"
                      ErrorMessage="请输入正整数!" MaximumValue="2147483647" MinimumValue="1" SetFocusOnError="True"
                      Type="Integer"></asp:RangeValidator></td>
            </tr>
            <tr id="B_22" class="TR_BG_list"  style="display:none;"> 
              <td  class="list_link">站群导读：</td>
              <td class="list_link"><asp:TextBox ID="TxtLead" runat="server" MaxLength="200" TextMode="MultiLine" Width="280" Height="81px" CssClass="form"/>
              </td>
            </tr>
               <tr id="B_23" class="TR_BG_list"  style="display:none;"> 
              <td  class="list_link">站群图片：</td>
              <td  class="list_link"><asp:TextBox ID="TxtPic" runat="server" MaxLength="200" Width="280" CssClass="form"/>
              <img src="../../sysImages/folder/s.gif" alt="选择站群图片" border="0" style="cursor:pointer;" onclick="selectFile('pic',document.Form1.TxtPic,280,500);document.Form1.TxtPic.focus();" />
              </td>
            </tr>
             <tr id="N_24" class="TR_BG_list"  style="display:none;">  
              <td  class="list_link">站群附件存放地址：</td>
              <td class="list_link"><asp:TextBox ID="TxtAccessories" runat="server" MaxLength="100" Width="280" CssClass="form"/>
              <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_EName_03',this)">站群附件</span>
              </td>
            </tr>   
            <tr id="N_25" class="TR_BG_list"  style="display:none;"> 
              <td  class="list_link">生成格式：</td>
              <td  class="list_link">
                  <asp:RadioButtonList ID="RadFileType" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Value="0">静态</asp:ListItem>
                      <asp:ListItem Value="1">动态</asp:ListItem>
                  </asp:RadioButtonList></td>
            </tr>
            <tr id="N_26" class="TR_BG_list"  style="display:none;"> 
              <td  class="list_link">静态文件保存路径：</td>
              <td  class="list_link">
              <asp:TextBox ID="TxtFileDir" runat="server" MaxLength="100" Width="280" CssClass="form"/>
               <img src="../../sysImages/folder/s.gif" alt="选择站群静态文件保存路径" border="0" style="cursor:pointer;" onclick="selectFile('path|<%Response.Write(Foosun.Config.UIConfig.dirSite); %>',document.Form1.TxtFileDir,280,500);document.Form1.TxtFileDir.focus();" />
              <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_fileDir',this)">帮助</span>
              </td>
            </tr>
            <tr id="N_27" class="TR_BG_list" > 
              <td  class="list_link">站群下新闻文件生成的目录结构：</td>
              <td  class="list_link">
                <div id="divdir">
                    <asp:TextBox ID="TxtDirRule" runat="server" MaxLength="200" Width="280" CssClass="form"/>&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择规则" onclick="selectFile('rulesmallPram',document.Form1.TxtDirRule,100,500);document.Form1.TxtDirRule.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_dirrule',this)">帮助</span>
                </div>
                </td>
            </tr>
            <tr id="N_28" class="TR_BG_list"> 
              <td  class="list_link">站群下新闻文件命名规则：</td>
              <td  class="list_link">
                <div id="divfile">
                   <asp:TextBox ID="TxtFileRule" runat="server" MaxLength="100" Width="280" CssClass="form"/>&nbsp;<img src="../../sysImages/folder/s.gif" style="cursor:pointer;" alt="选择规则" onclick="selectFile('rulePram',document.Form1.TxtFileRule,100,500);document.Form1.TxtFileRule.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_filerule',this)">帮助</span>
                </div>
                </td>
            </tr>
            <tr id="B_29" class="TR_BG_list"  style="display:none;"> 
              <td  class="list_link">导航位置HTML：</td>
              <td  class="list_link"><asp:TextBox ID="TxtNaviPosition" runat="server" TextMode="MultiLine" Width="280" Height="81px" CssClass="form"/>
              <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_site_add_NaviPostion',this)">什么叫导航?</span>
              </td>
            </tr>
            <tr id="sN_30" class="TR_BG_list" style="display:none;"> 
              <td  class="list_link">站群首页扩展名：</td>
              <td  class="list_link">
                  <asp:DropDownList ID="DdlIndexEXName" runat="server" CssClass="form">
                      <asp:ListItem Value="html">html</asp:ListItem>
                      <asp:ListItem Value="htm">htm</asp:ListItem>
                      <asp:ListItem Value="shtm">shtm</asp:ListItem>
                      <asp:ListItem Value="shtml">shtml</asp:ListItem>
                      <asp:ListItem Value="aspx">aspx</asp:ListItem>
                  </asp:DropDownList></td>
            </tr>
            <tr id="sN_31" class="TR_BG_list" style="display:none;"> 
              <td  class="list_link">站群栏目扩展名：</td>
              <td  class="list_link">
                  <asp:DropDownList ID="DdlClassEXName" runat="server" CssClass="form">
                      <asp:ListItem Value="html">html</asp:ListItem>
                      <asp:ListItem Value="htm">htm</asp:ListItem>
                      <asp:ListItem Value="shtm">shtm</asp:ListItem>
                      <asp:ListItem Value="shtml">shtml</asp:ListItem>
                      <asp:ListItem Value="aspx">aspx</asp:ListItem>
                  </asp:DropDownList>
              </td>
            </tr>
            <tr id="sN_32" class="TR_BG_list" style="display:none;"> 
              <td  class="list_link">站群浏览页扩展名：</td>
              <td  class="list_link">
                  <asp:DropDownList ID="DdlNewsEXName" runat="server" CssClass="form">
                      <asp:ListItem Value="html">html</asp:ListItem>
                      <asp:ListItem Value="htm">htm</asp:ListItem>
                      <asp:ListItem Value="shtm">shtm</asp:ListItem>
                      <asp:ListItem Value="shtml">shtml</asp:ListItem>
                      <asp:ListItem Value="aspx">aspx</asp:ListItem>
                  </asp:DropDownList></td>
            </tr>
            <tr id="sN_33" class="TR_BG_list" style="display:none;"> 
              <td  class="list_link">站群其他扩展名：</td>
              <td  class="list_link">
                  <asp:DropDownList ID="DdlOtherEXName" runat="server" CssClass="form">
                      <asp:ListItem Value="html">html</asp:ListItem>
                      <asp:ListItem Value="htm">htm</asp:ListItem>
                      <asp:ListItem Value="shtm">shtm</asp:ListItem>
                      <asp:ListItem Value="shtml">shtml</asp:ListItem>
                      <asp:ListItem Value="aspx">aspx</asp:ListItem>
                  </asp:DropDownList></td>
            </tr>
            <tr class="TR_BG_list">
                <td class="list_link" colspan="2" align="center">
                    <asp:Button ID="BtnOK" runat="server" Text=" 保存 " CssClass="form" OnClick="BtnOK_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnCancel" runat="server" Text=" 重置 "  CssClass="form"/>
                 </td>
            </tr> 
        </table>  
        <br />
        <br />
<!-------CopyRight------->
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
          <tr>
            <td align="center"><%Response.Write(CopyRight);%></td>
          </tr>
        </table>
   </form> 
</body>
</html>
