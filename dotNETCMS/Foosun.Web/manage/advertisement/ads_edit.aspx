<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_advertisement_ads_edit" ResponseEncoding="utf-8" Codebehind="ads_edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>修改广告信息</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="AdsForm" runat="server" method="post" action="">
      <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">修改广告信息</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="list.aspx" target="sys_main" class="list_link">广告系统</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />修改广告信息</div></td>
        </tr>
      </table>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
    <tr class="TR_BG_list">
      <td width="13%" align="center" class="navi_link" style="width: 15%">广告名称</td>
      <td colspan="2" align="left"><asp:TextBox ID="adName" runat="server" Width="200px" MaxLength="50" CssClass="form"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_001',this)">帮助</span><span id="spanadName"></span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link">所属分类</td>
      <td colspan="2" align="left"><asp:DropDownList ID="ClassID" runat="server" CssClass="form" Width="205px"></asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_002',this)">帮助</span><span id="spanClassID"></span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 15%">广告类型</td>
      <td colspan="2" align="left"><asp:DropDownList ID="adType" runat="server" CssClass="form" Width="205px" onchange="javascript:checkadType(this.value);">
     </asp:DropDownList>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_003',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list" id="TrleftPic">
      <td align="center" class="navi_link" style="width: 15%">(左图片/动画)地址</td>
      <td colspan="2" align="left"><asp:TextBox ID="leftPic" runat="server" Width="200px" MaxLength="200" CssClass="form"></asp:TextBox> <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择图片"  onclick="selectFile('pic',document.AdsForm.leftPic,280,500);document.AdsForm.leftPic.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_004',this)">帮助</span> <span id="spanleftPic"></span></td>
    </tr>
    <tr class="TR_BG_list" id="TrleftSize">
      <td align="center" class="navi_link" style="width: 15%">(左图片/动画)宽高</td>
      <td colspan="2" align="left"><asp:TextBox ID="leftSize" runat="server" Width="200px" MaxLength="12" CssClass="form"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_005',this)">帮助</span><span id="spanleftSize"></span><span id="spanleftSize1"></td>
    </tr>
    <tr class="TR_BG_list" style="display:none;" id="TrrightPic">
      <td align="center" class="navi_link" style="width: 15%">(右图片/动画)地址</td>
      <td colspan="2" align="left"><asp:TextBox ID="rightPic" runat="server" Width="200px" MaxLength="200" CssClass="form"></asp:TextBox> <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择图片"  onclick="selectFile('pic',document.AdsForm.rightPic,280,500);document.AdsForm.rightPic.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_006',this)">帮助</span><span id="spanRightPic"></span></td>
    </tr>
    <tr class="TR_BG_list" style="display:none;" id="TrrightSize">
      <td align="center" class="navi_link" style="width: 15%">(右图片/动画)宽高</td>
      <td colspan="2" align="left"><asp:TextBox ID="rightSize" runat="server" Width="200px" MaxLength="12" CssClass="form"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_007',this)">帮助</span><span id="spanrightSize"></span><span id="spanrightSize1"></span></td>
    </tr>
    <tr class="TR_BG_list" id="TrLinkURL">
      <td align="center" class="navi_link" style="width: 15%">链接地址</td>
      <td colspan="2" align="left"><asp:TextBox ID="LinkURL" runat="server" Width="200px" MaxLength="200" CssClass="form"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_008',this)">帮助</span><span id="spanLinkURL"></span><span id="spanLinkURL1"></span></td>
    </tr>
    <tr class="TR_BG_list" id="TrCycID" style="display:none;">
      <td align="center" class="navi_link" style="width: 15%">循环广告位</td>
      <td colspan="2" align="left">
      <asp:DropDownList ID="CycID" runat="server" CssClass="form" Width="205px"> 
     </asp:DropDownList><input type="hidden" id="CycTF" runat="server" name="CycTF" />
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_009',this)">帮助</span><span id="spanCycID"></span></td>
    </tr>
    <tr class="TR_BG_list" id="TrCycSpeed" style="display:none;">
      <td align="center" class="navi_link" style="width: 15%">循环速度</td>
      <td colspan="2" align="left"><asp:TextBox ID="CycSpeed" runat="server" Width="200px" MaxLength="4" CssClass="form" Text="0"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_010',this)">帮助</span><span id="spanCycSpeed"></span><span id="spanCycSpeed1"></span></td>
    </tr>
    <tr class="TR_BG_list" id="TrCycDic" style="display:none;">
      <td align="center" class="navi_link" style="width: 15%">循环方向</td>
      <td align="left"><asp:RadioButtonList ID="CycDic" runat="server" RepeatDirection="Horizontal">
          <asp:ListItem Value="0" Selected="true">上</asp:ListItem>
          <asp:ListItem Value="1" >下</asp:ListItem>
          <asp:ListItem Value="2" >左</asp:ListItem>
          <asp:ListItem Value="3" >右</asp:ListItem>
        </asp:RadioButtonList></td>
      <td align="left" Width="65%"><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_011',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list" style="display:none;" id="TrAdTxt">
      <td align="center" class="navi_link" style="width: 15%" valign="top">广告内容<br />(<a href="javascript:f_add()" class="list_link">添加</a>)</td>
      <td colspan="2" align="left"><div id="DivadTxt" runat="server"></div></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 15%">显示条件</td>
      <td align="left"><asp:RadioButtonList ID="CondiTF" runat="server" RepeatDirection="Horizontal" Width="196px">
          <asp:ListItem Value="1" onclick="javascript:checkCondiTF(this.value);">有</asp:ListItem>
          <asp:ListItem Value="0" Selected="true" onclick="javascript:checkCondiTF(this.value);">无</asp:ListItem>
        </asp:RadioButtonList></td>
      <td align="left" Width="65%"><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_012',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list" style="display:none;" id="TrmaxShowClick">
      <td align="center" class="navi_link" style="width: 15%">最大显示数</td>
      <td colspan="2" align="left"><asp:TextBox ID="maxShowClick" runat="server" Width="200px" MaxLength="4" CssClass="form"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_014',this)">帮助</span><span id="spanmaxShowClick"></span></td>
    </tr>
    <tr class="TR_BG_list" style="display:none;" id="TrTimeOutDay">
      <td align="center" class="navi_link" style="width: 15%">到期时间</td>
      <td colspan="2" align="left"><asp:TextBox ID="TimeOutDay" runat="server" Width="200px" CssClass="form" ReadOnly="true"></asp:TextBox>  <input class="form" type="button" value="选择日期"  onclick="selectFile('date',document.AdsForm.TimeOutDay,160,400);document.AdsForm.TimeOutDay.focus();" />
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_015',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list"  style="display:none;" id="TrmaxClick">
      <td align="center" class="navi_link" style="width: 15%">点击次数</td>
      <td colspan="2" align="left"><asp:TextBox ID="maxClick" runat="server" Width="200px" MaxLength="4" CssClass="form"></asp:TextBox>
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_016',this)">帮助</span><span id="spanmaxClick">
          </span></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" class="navi_link" style="width: 15%">是否锁定</td>
      <td align="left"><asp:RadioButtonList ID="isLock" runat="server" RepeatDirection="Horizontal" Width="196px">
          <asp:ListItem Value="1">是</asp:ListItem>
          <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
        </asp:RadioButtonList></td>
      <td align="left" Width="65%"><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_AdsAdd_017',this)">帮助</span></td>
    </tr>
    <tr class="TR_BG_list">
      <td class="navi_link" colspan="3"><label>
        <input type="button" name="Sub_mit" value=" 确 定 " class="form" onclick="javascript:checkData(document.AdsForm.adType.value);" id="Button1" />
        </label>
        <label>
        <input type="reset" name="UnDo" value=" 重 填 " class="form" />
            <asp:HiddenField ID="H_AdsID" runat="server" /><asp:HiddenField ID="OldClass" runat="server" />
        </label></td>
    </tr>
  </table>      <br />
      <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
        <tr>
          <td align="center"><label id="copyright" runat="server" /></td>
        </tr>
      </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
setCookie("ads_txt_num",<% =txtnum-1 %>);
function checkData(value)
{
    spanclear();
    var re = /^[0-9]{0,4}\|[0-9]{0,4}$/;
    var re1 = /^http:\/\/([\w-]+\.)+[\w-]+(\/[\w-.\/?%&=]*)?$/;    
    var re2 = /^[0-9]*$$/;
    if(document.AdsForm.adName.value=="")
    {
        document.getElementById("spanadName").innerHTML="<span class=reshow>(*)请输入广告名称</spna>";
        return false;
    }
    if(document.AdsForm.ClassID.value=="")
    {
        document.getElementById("spanClassID").innerHTML="<span class=reshow>(*)请选择广告栏目</spna>";
        return false;
    }
    if(value=="11")
    {
        var _arr=document.AdsForm.AdTxtContent
        if (typeof(_arr.length)=="undefined")
        {
	        if (_arr.value=="")
	        {
                document.getElementById("spanAdTxtContent").innerHTML="<span class=reshow>(*)请输入文字显示内容</spna>";
		        _arr.focus();
		        return false;
	        }
        }
        if (document.AdsForm.AdTxtColNum.value=="")
        {
            document.getElementById("spanAdTxtNum").innerHTML="<span class=reshow>(*)请输入列数</spna>";
	        return false;
        }
        else
        {
	        for (var j=0;j<_arr.length;j++)
	        {	
		        if (_arr[j].value=="")
		        {
                    document.getElementById("spanAdTxtContent").innerHTML="<span class=reshow>(*)请输入文字显示内容</spna>";
			        _arr[j].focus();
			        return false;
		        }
	        }
        }
    }
    else
    {
        if(document.AdsForm.leftPic.value=="")
        {
            document.getElementById("spanleftPic").innerHTML="<span class=reshow>(*)请输入图片/动画地址</spna>";
            return false;
        }
        if(document.AdsForm.leftSize.value=="")
        {
            document.getElementById("spanleftSize").innerHTML="<span class=reshow>(*)请输入图片/动画大小</spna>";
            return false;
        }
        var leftsizevalue = document.AdsForm.leftSize.value;  
        if(re.test(leftsizevalue)==false)
        {
            document.getElementById("spanleftSize1").innerHTML="<span class=reshow>(*)格式不正确,格式为(200|300),高或者宽不能超过四位数</spna>";
            return false;
        }
        if(value=="9")
        {
            if(document.AdsForm.rightPic.value=="")
            {
                document.getElementById("spanRightPic").innerHTML="<span class=reshow>(*)请输入图片/动画地址</spna>";
                return false;
            }    
            if(document.AdsForm.rightSize.value=="")
            {
                document.getElementById("spanrightSize").innerHTML="<span class=reshow>(*)请输入图片/动画大小</spna>";
                return false;
            }
            var rightSizevalue = document.AdsForm.rightSize.value;  
            if(re.test(rightSizevalue)==false)
            {
                document.getElementById("spanrightSize1").innerHTML="<span class=reshow>(*)格式不正确,格式为(200|300),高或者宽不能超过四位数</spna>";
                return false;
            }
        }
        if(document.AdsForm.LinkURL.value=="")
        {
            document.getElementById("spanLinkURL").innerHTML="<span class=reshow>(*)请输入链接地址</spna>";
            return false;
        }
        var LinkURLvalue = document.AdsForm.LinkURL.value;
        if(re1.test(LinkURLvalue)==false)
        {
            document.getElementById("spanLinkURL1").innerHTML="<span class=reshow>(*)链接地址格式不正确</spna>";
            return false;
        }
        if(value=="10")
        {
            if(document.AdsForm.CycID.value=="")
            {
                document.getElementById("spanCycID").innerHTML="<span class=reshow>(*)请添加要循环的广告再进行选择</spna>";
                return false;
            }
            if(document.AdsForm.CycSpeed.value=="")
            {
                document.getElementById("spanCycSpeed").innerHTML="<span class=reshow>(*)请输入循环速度</spna>";
                return false;
            }
            var CycSpeedvalue = document.AdsForm.CycSpeed.value;
            if(re2.test(CycSpeedvalue)==false)
            {
                document.getElementById("spanCycSpeed1").innerHTML="<span class=reshow>(*)循环速度只能为正整数</spna>";
                return false;
            }
        }
    }
    if(document.AdsForm.CycTF.value== "1")
    {
        var maxShowClickvalue = document.AdsForm.maxShowClick.value;
        if(re2.test(maxShowClickvalue)==false)
        {
            document.getElementById("spanmaxShowClick").innerHTML="<span class=reshow>(*)最大点击数只能为正整数</spna>";
            return false;
        }
        var maxClickvalue = document.AdsForm.maxClick.value;
        if(re2.test(maxClickvalue)==false)
        {
            document.getElementById("spanmaxClick").innerHTML="<span class=reshow>(*)显示次数只能为正整数</spna>";
            return false;
        }
    }
    document.AdsForm.action="ads_edit.aspx?Type=Update&CycTF="+document.AdsForm.CycTF.value;
    document.AdsForm.submit();
}
function spanclear()
{
    document.getElementById("spanmaxClick").innerHTML="";
    document.getElementById("spanmaxShowClick").innerHTML="";
    document.getElementById("spanCycSpeed1").innerHTML="";
    document.getElementById("spanCycSpeed").innerHTML="";
    document.getElementById("spanCycID").innerHTML="";
    document.getElementById("spanLinkURL1").innerHTML="";
    document.getElementById("spanLinkURL").innerHTML="";
    document.getElementById("spanRightPic").innerHTML="";
    document.getElementById("spanrightSize").innerHTML="";
    document.getElementById("spanrightSize1").innerHTML="";
    document.getElementById("spanleftSize").innerHTML="";
    document.getElementById("spanleftSize1").innerHTML="";
    document.getElementById("spanadName").innerHTML="";
    document.getElementById("spanrightSize1").innerHTML="";
    document.getElementById("spanAdTxtContent").innerHTML="";
    document.getElementById("spanleftPic").innerHTML="";
}
function checkCondiTF(value)
{
    if(value=="1")
    {
        document.getElementById("TrmaxShowClick").style.display="";
        document.getElementById("TrTimeOutDay").style.display="";
        document.getElementById("TrmaxClick").style.display="";
    }
    else
    {
        document.getElementById("TrmaxShowClick").style.display="none";
        document.getElementById("TrTimeOutDay").style.display="none";
        document.getElementById("TrmaxClick").style.display="none";
    }
}
function checkadType(value)
{
    hide();
    switch(value)
    {
        case "9":
            document.getElementById("TrrightPic").style.display="";
            document.getElementById("TrrightSize").style.display="";
            break;
        case "10":
            document.AdsForm.CycTF.value="1";
            document.getElementById("TrCycID").style.display="";
            document.getElementById("TrCycSpeed").style.display="";
            document.getElementById("TrCycDic").style.display="";
            break;
        case "11":
            document.getElementById("TrleftPic").style.display="none";
            document.getElementById("TrleftSize").style.display="none";
            document.getElementById("TrLinkURL").style.display="none";
            document.getElementById("TrAdTxt").style.display="";
            break;
        case "12":
            document.getElementById("TrrightPic").style.display="";
            document.getElementById("TrrightSize").style.display="";
            break;
        default:
            hide();
            break;
    }
}
function hide()
{
    document.getElementById("TrleftPic").style.display="";
    document.getElementById("TrleftSize").style.display="";
    document.getElementById("TrLinkURL").style.display="";
    document.getElementById("TrAdTxt").style.display="none";
    document.getElementById("TrrightPic").style.display="none";
    document.getElementById("TrrightSize").style.display="none";
    document.AdsForm.CycTF.value="0";
    document.getElementById("TrCycID").style.display="none";
    document.getElementById("TrCycSpeed").style.display="none";
    document.getElementById("TrCycDic").style.display="none";
}

function f_add()
{
    var num = 0;
    if(getCookie("ads_txt_num") != null || getCookie("ads_txt_num")!= "null")
    {
	    num = parseInt(getCookie("ads_txt_num"));
	    if(num>8)
	    {
	        return;
	    }
	    num = num +1;
	    setCookie("ads_txt_num",num);
	}
    var chars = "1234567890";
    var randNum = makeRandChar(chars,20);
    var tempstr = "<div id='"+randNum+"'>文本内容 <input name='AdTxtContent' type='text' style='width:130px;' maxlength='200' value='' class='form' /> 样式 <input name='AdTxtCss' type='text' style='width:30px;' maxlength='20' value='' class='form' /> 链接地址 <input name='AdTxtLink' type='text' id='AdTxtLink' value='' style='width:130px;' maxlength='100' class='form' /> <a href='#'onclick='f_delete(this.parentNode)' class='list_link'>删除</a></div>"; 
    document.getElementById("temp").innerHTML+=tempstr;
}
function f_delete(divobj)
{
    divobj.parentNode.removeChild(divobj);  
    var num = parseInt(getCookie("ads_txt_num"));
    num = num - 1;
	setCookie("ads_txt_num",num);
}

function makeRandChar(chars,marklen)
{//创建随机数
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
<% show(); %>