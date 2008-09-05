<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_createLabel_Other" Codebehind="createLabel_Other.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="ListLabel" runat="server">
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width: 28%">标签类型</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="LabelType" runat="server" Width="200px" CssClass="form" onchange="javascript:selectLabelType(this.value);">
                <asp:ListItem Value="">请选择标签类型</asp:ListItem>
                <asp:ListItem Value="frindlink">友情链接</asp:ListItem>
                <asp:ListItem Value="freeJS">自由JS</asp:ListItem>
                <asp:ListItem Value="sysJS">系统JS</asp:ListItem>
                <asp:ListItem Value="adJS">广告JS</asp:ListItem>
                <asp:ListItem Value="statJS">统计JS</asp:ListItem>
                <asp:ListItem Value="surveyJS">调查JS</asp:ListItem>
                 <asp:ListItem Value="OtherJS">其它JS</asp:ListItem>
             </asp:DropDownList><span id="spanLabelType"></span></td>
          </tr>
          
          <tr class="TR_BG_list" id="TrfreeLabel" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">自由标签</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="freeLabelID" runat="server" Width="200px" CssClass="form">
             </asp:DropDownList><span id="spanfreeLabel"></span></td>
          </tr>

          <tr class="TR_BG_list" id="TrfreeJS" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">自由JS编号</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="freeJSID" runat="server" Width="200px" CssClass="form">
             </asp:DropDownList><span id="spanfreeJS"></span></td>
          </tr>

          <tr class="TR_BG_list" id="TrsysJS" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">系统JS编号</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="sysJSID" runat="server" Width="200px" CssClass="form">
             </asp:DropDownList><span id="spansysJS"></span></td>
          </tr>

          <tr class="TR_BG_list" id="TradJS" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">广告JS编号</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="adJSID" runat="server" Width="200px" CssClass="form">
             </asp:DropDownList><span id="spanadJS"></span></td>
          </tr>

          <tr class="TR_BG_list" id="TrsurveyJS" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">调查JS编号</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="surveyJSID" runat="server" Width="200px" CssClass="form">
             </asp:DropDownList><span id="spansurveyJS"></span></td>
          </tr>
          <tr class="TR_BG_list" id="TrShowtype" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">统计表现形式</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="statShowType" runat="server" Width="200px" CssClass="form">
            <asp:ListItem Value="2">图标统计样式</asp:ListItem>
            <asp:ListItem Value="1">滚动统计样式</asp:ListItem>
            <asp:ListItem Value="0" Selected="true">文字统计样式</asp:ListItem>            
             </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list" id="TrstatJS" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">统计JS编号</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="statJSID" runat="server" Width="200px" CssClass="form">
             </asp:DropDownList><span id="spanstatJS"></span></td>
          </tr>
           <tr class="TR_BG_list" id="TrOtherJS" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">其它JS编号</td>
            <td width="79%" align="left" class="navi_link"><asp:DropDownList ID="OtherJSID" runat="server" Width="200px" CssClass="form">
            <asp:ListItem Value="">请选择JS</asp:ListItem>
             </asp:DropDownList><span id="spanOtherJS"></span></td>
          </tr> 
          
          
          <tr class="TR_BG_list" style="display:none;" id="selectLinkTypes">
              <td align="right"  class="list_link" >选择类别</td>
              <td  align="left" class="list_link">
                 <asp:DropDownList ID="SelectClass" runat="server" CssClass="form"> </asp:DropDownList>
              </td>
          </tr>
          
                   
          <tr class="TR_BG_list" id="Trfrindlink" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">调用数量</td>
            <td width="79%" align="left" class="navi_link">
            <asp:TextBox ID="fNumber" CssClass="form" Width="80px" Font-Size="Smaller" runat="server">20</asp:TextBox> 每行<asp:TextBox Width="80px" ID="Cols" CssClass="form" Font-Size="Smaller" runat="server">10</asp:TextBox>个 div+css，请在前台CSS中设置
            </td>
          </tr>
          <tr class="TR_BG_list" id="TrTarget" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">类型</td>
            <td width="79%" align="left" class="navi_link">
            <asp:DropDownList ID="fType" runat="server">
            <asp:ListItem Value="1">文字类</asp:ListItem>
            <asp:ListItem Value="0">图片类</asp:ListItem>
            </asp:DropDownList>
            
            <asp:DropDownList ID="isDiv" runat="server">
            <asp:ListItem Value="">输出格式</asp:ListItem>
            <asp:ListItem Value="true">div(默认)</asp:ListItem>
            <asp:ListItem Value="false">table</asp:ListItem>
            </asp:DropDownList>
            </td>
          </tr>
          <tr class="TR_BG_list" id="TrUser" style="display:none;">
            <td align="right" class="navi_link" style="width: 28%">其他</td>
            <td width="79%" align="left" class="navi_link">
            <asp:DropDownList ID="isAdmin" runat="server">
            <asp:ListItem Value="">所有</asp:ListItem>
            <asp:ListItem Value="0">管理员录入</asp:ListItem>
            <asp:ListItem Value="1">会员申请</asp:ListItem>
            </asp:DropDownList>
            </td>
          </tr>
           <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width: 28%"></td>
            <td width="79%" align="left" class="navi_link">&nbsp;<input class="form" type="button" value=" 确 定 "  onclick="javascript:ReturnDivValue();" />&nbsp;<input class="form" type="button" value=" 关 闭 "  onclick="javascript:CloseDiv();" /></td>
          </tr>
       </table>  
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
function selectLabelType(type)
{
    allhide();
    switch (type)
    {
        case "freeJS":
            document.getElementById("TrfreeJS").style.display="";
            break;
        case "sysJS":
            document.getElementById("TrsysJS").style.display="";
            break;
        case "adJS":
            document.getElementById("TradJS").style.display="";
            break;
        case "surveyJS":
            document.getElementById("TrsurveyJS").style.display="";
            break;
        case "statJS":
            document.getElementById("TrstatJS").style.display="";
            document.getElementById("TrShowtype").style.display="";
            break;
        case "OtherJS":
            document.getElementById("TrOtherJS").style.display="";
            break;    
        case "frindlink":
            document.getElementById("Trfrindlink").style.display="";
            document.getElementById("TrTarget").style.display="";
            document.getElementById("TrUser").style.display="";
            document.getElementById("selectLinkTypes").style.display="";
    }

}
function allhide()
{
    document.getElementById("TrfreeJS").style.display="none";
    document.getElementById("TrsysJS").style.display="none";
    document.getElementById("TradJS").style.display="none";
    document.getElementById("TrsurveyJS").style.display="none";
    document.getElementById("TrstatJS").style.display="none";
    document.getElementById("TrOtherJS").style.display="none";
    document.getElementById("TrShowtype").style.display="none";
    document.getElementById("Trfrindlink").style.display="none";
    document.getElementById("TrTarget").style.display="none";
    document.getElementById("TrUser").style.display="none";
    document.getElementById("selectLinkTypes").style.display="none";
}
function ReturnDivValue()
{
    spanClear();
    var CheckStr=true;
    var rvalue="";
    switch (document.ListLabel.LabelType.value)
    {
        case "frindlink":
            rvalue+="[FS:Loop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=";
            rvalue += "Frindlink";
            if(document.ListLabel.SelectClass.value != "")
            {
                rvalue += ",FS:TypeClassID="+document.ListLabel.SelectClass.value+"";
            }
            if(document.ListLabel.fNumber.value!="")
            {
                rvalue += ",FS:Number="+document.ListLabel.fNumber.value+"";
            }
            if(document.ListLabel.Cols.value!="")
            {
                rvalue += ",FS:Cols="+document.ListLabel.Cols.value+"";
            }
            if(document.ListLabel.fType.value!="")
            {
                rvalue += ",FS:FType="+document.ListLabel.fType.value+"";
            }
            if(document.ListLabel.isAdmin.value!="")
            {
                rvalue += ",FS:isAdmin="+document.ListLabel.isAdmin.value+"";
            }
            if(document.ListLabel.isDiv.value!="")
            {
                rvalue += ",FS:isDiv="+document.ListLabel.isDiv.value+"";
            }
            rvalue += "][/FS:Loop]";
            break;
        case "freeJS":
            rvalue+="[FS:unLoop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=";
            if(checkIsNull(document.ListLabel.freeJSID,document.getElementById("spanfreeJS"),"请选择自由JS"))
                CheckStr=false;
            rvalue += "freeJS,FS:JSID=" + document.ListLabel.freeJSID.value + "][/FS:unLoop]";
            break;
        case "sysJS":
            rvalue+="[FS:unLoop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=";
            if(checkIsNull(document.ListLabel.sysJSID,document.getElementById("spansysJS"),"请选择系统JS"))
                CheckStr=false;
            rvalue += "sysJS,FS:JSID=" + document.ListLabel.sysJSID.value + "][/FS:unLoop]";
            break;
        case "adJS":
            rvalue+="[FS:unLoop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=";
            if(checkIsNull(document.ListLabel.adJSID,document.getElementById("spanadJS"),"请选择广告JS"))
                CheckStr=false;
            rvalue += "adJS,FS:JSID=" + document.ListLabel.adJSID.value + "][/FS:unLoop]";
            break;
        case "surveyJS":
            rvalue+="[FS:unLoop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=";
            if(checkIsNull(document.ListLabel.surveyJSID,document.getElementById("spansurveyJS"),"请选择调查JS"))
                CheckStr=false;
            rvalue += "surveyJS,FS:JSID=" + document.ListLabel.surveyJSID.value + "][/FS:unLoop]";
            break;
        case "statJS":
            rvalue+="[FS:unLoop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=";
            if(checkIsNull(document.ListLabel.statJSID,document.getElementById("spanstatJS"),"请选择统计JS"))
                CheckStr=false;
            rvalue += "statJS,FS:JSID=" + document.ListLabel.statJSID.value;
            rvalue += ",FS:statShowType=" + document.ListLabel.statShowType.value;
            rvalue += "][/FS:unLoop]";
            break;
        case "OtherJS":
            rvalue+="[FS:unLoop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=";
            if(checkIsNull(document.ListLabel.OtherJSID,document.getElementById("spanOtherJS"),"请选择其它JS"))
                CheckStr=false;
            rvalue += "OtherJS,FS:JSID=?";
            rvalue += "][/FS:unLoop]";
            break;
        default:
            if(checkIsNull(document.ListLabel.LabelType,document.getElementById("spanLabelType"),"请选择JS类型"))
                CheckStr=false;
            break;    
    }
    if(CheckStr==true)
	    parent.getValue(rvalue);
        
}
function spanClear()
{
//    document.getElementById("spanfreeLabel").innerHTML="";
    document.getElementById("spanfreeJS").innerHTML="";
    document.getElementById("spansysJS").innerHTML="";
    document.getElementById("spanadJS").innerHTML="";
    document.getElementById("spansurveyJS").innerHTML="";
    document.getElementById("spanstatJS").innerHTML="";
    document.getElementById("spanOtherJS").innerHTML="";
}
function checkIsNull(obj,spanobj,error)
{
    if(obj.value=="")
    {
        spanobj.innerHTML="<span class=reshow>(*)"+error+"</spna>";
        return true;
    }
    return false;
}
function CloseDiv()
{
    parent.document.getElementById("LabelDivid").style.display="none";
}
function loadNum()
{
    var numkey = Math.random();
    numkey = Math.round(numkey*100000);
    document.getElementById('SPANID').value="JsSpanID_"+numkey;
 }
</script>
