<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_js_createLabel_List" Codebehind="createLabel_List.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
</head>
<body>
    <form id="ListLabel" runat="server">   
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">列表类型</td>
            <td width="79%" align="left" class="list_link"><asp:DropDownList ID="NewsType" runat="server" Width="200px" CssClass="form" onchange="javascript:selectNewsType(this.value);" >
                <asp:ListItem Value="Last">最新新闻</asp:ListItem>
                <asp:ListItem Value="Rec">推荐新闻</asp:ListItem>
                <asp:ListItem Value="Hot">热点新闻</asp:ListItem>
                <asp:ListItem Value="Tnews">头条新闻</asp:ListItem>
                <asp:ListItem Value="Jnews">精彩新闻</asp:ListItem>
                <asp:ListItem Value="ANN">公告新闻</asp:ListItem>
                <asp:ListItem Value="MarQuee">滚动新闻</asp:ListItem>
                <asp:ListItem Value="Special">专题新闻</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list" id="TrClassId">
            <td align="right" class="list_link" style="width: 28%">栏目ID</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="ClassId" runat="server" CssClass="form" Width="120px"></asp:TextBox>
              &nbsp;<asp:HiddenField ID="ClassNames" runat="server" />
              <input class="form" type="button" value="选择栏目"  onclick="selectFile('newsclass',new Array(document.ListLabel.ClassId,document.ListLabel.ClassNames),300,380);" />&nbsp;&nbsp;<span id="spans" runat="server" style="color:blue">不选则显示所有</span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">引用样式<span id="url"></span></td>
            <td align="left" class="navi_link">
            <asp:DropDownList ID="Root" runat="server" CssClass="form" Width="100px" onchange="javascript:selectRoot(this.value);">
                <asp:ListItem Value="true">固定样式</asp:ListItem>
                <asp:ListItem Value="false">自定义样式</asp:ListItem>
              </asp:DropDownList>
              <label id="TrStyleID">
              <asp:TextBox ID="StyleID" runat="server" CssClass="form" Width="120px" ReadOnly="true"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer" title="选择样式" onclick="selectFile('style',document.getElementById('StyleID'),240,550);document.ListLabel.StyleID.focus();" /><span id="sapnStyleID"></span>
              </label>
              </td>
          </tr>
          <tr class="TR_BG_list" id="TrUserDefined" style="display:none;">
            <td align="right" class="navi_link">自定义样式</td>
            <td align="left" class="navi_link">
            <div>
              <label id="style_base" runat="server" />
              <label id="style_class" runat="server" />
              <label id="style_special" runat="server" />                 
          <asp:DropDownList ID="define" CssClass="form" runat="server" Width="150px" onchange="javascript:setValue(this.value);">
          <asp:ListItem Value="">自定义字段</asp:ListItem>
          </asp:DropDownList></div>
           <script type="text/javascript" language="JavaScript">
              window.onload = function()
                {
                var sBasePath = "../../editor/"
                var oFCKeditor = new FCKeditor('UserDefined') ;
                oFCKeditor.BasePath	= sBasePath ;
                oFCKeditor.ToolbarSet = 'Foosun_Basicstyle';
                oFCKeditor.Width = '100%' ;
                oFCKeditor.Height = '150' ;	
                oFCKeditor.ReplaceTextarea() ;
                }
            </script>
            <textarea rows="1" cols="1" name="UserDefined" style="display:none" id="UserDefined" runat="server" ></textarea>
    
     </td>
          </tr>
          
          <tr class="TR_BG_list" id="TrSpecialID" style="display:none;">
            <td align="right" class="list_link" style="width: 28%">专题栏目</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="SpecialID" runat="server" CssClass="form" Width="120px"></asp:TextBox>
              &nbsp;
              <input class="form" type="button" value="选择专题"  onclick="selectFile('special',document.ListLabel.SpecialID,300,380);document.ListLabel.SpecialID.focus();" /><span id="spanSpecialID"></span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">循环条数</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="Number" runat="server" CssClass="form" Width="190px" Text="10"></asp:TextBox><span id="spanNumber"></span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">点击大于多少</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="ClickNumber" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="spanClickNumber"></span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">显示多少天内的信息</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="ShowDateNumer" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="spanShowDateNumer"></span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">每行显示多少条</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="Cols" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="spanCols"></span></td>
          </tr>
          <tr class="TR_BG_list" id="TrMarqDirec" style="display:none;">
            <td align="right" class="list_link" style="width: 28%">滚动方向</td>
            <td width="79%" align="left" class="list_link">
                <asp:DropDownList ID="MarqDirec" runat="server" CssClass="form" Width="200px">
                <asp:ListItem Value="1">请选择滚动方向</asp:ListItem>
                <asp:ListItem Value="1">上</asp:ListItem>
                <asp:ListItem Value="2">下</asp:ListItem>
                <asp:ListItem Value="3">左</asp:ListItem>
                <asp:ListItem Value="4">右</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list" id="TrMarqSpeed" style="display:none;">
            <td align="right" class="list_link" style="width: 28%">滚动速度</td>
            <td width="79%" align="left" class="list_link">
                <asp:TextBox ID="MarqSpeed" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="sapnMarqSpeed"></span></td>
          </tr>
          <tr class="TR_BG_list" id="TrMarqwidth" style="display:none;">
            <td align="right" class="list_link" style="width: 28%">宽度</td>
            <td width="79%" align="left" class="list_link">
                <asp:TextBox ID="Marqwidth" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="sapnMarqwidth"></span></td>
          </tr>
          <tr class="TR_BG_list" id="TrMarqheight" style="display:none;">
            <td align="right" class="list_link" style="width: 28%">高度</td>
            <td width="79%" align="left" class="list_link">
                <asp:TextBox ID="Marqheight" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="sapnMarqheight"></span></td>
          </tr>
          
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">调用图片</td>
            <td width="79%" align="left" class="list_link"><asp:DropDownList ID="isPic" runat="server" CssClass="form" Width="200px">
                <asp:ListItem Value="0">请选择是否调用</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
                <asp:ListItem Value="0">否</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">按照什么排序</td>
            <td width="79%" align="left" class="list_link"><asp:DropDownList ID="DescType" runat="server" CssClass="form" Width="200px">
                <asp:ListItem Value="1">请选择排序方式</asp:ListItem>
                <asp:ListItem Value="1">自动编号</asp:ListItem>
                <asp:ListItem Value="2">添加日期</asp:ListItem>
                <asp:ListItem Value="3">点击次数</asp:ListItem>
                <asp:ListItem Value="4">权重</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">排序顺序</td>
            <td width="79%" align="left" class="list_link"><asp:DropDownList ID="Desc" runat="server" CssClass="form" Width="200px">
                <asp:ListItem Value="0">请选择排序顺序</asp:ListItem>
                <asp:ListItem Value="0">desc(降序)</asp:ListItem>
                <asp:ListItem Value="1">asc(升序)</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">在标题前加导航</td>
            <td width="79%" align="left" class="list_link"><asp:DropDownList ID="ShowNavi" runat="server" CssClass="form" Width="200px" onchange="javascript:selectShowNavi(this.value);">
                <asp:ListItem Value="1">请选择是否加导航</asp:ListItem>
                <asp:ListItem Value="1">数字导航(1,2,3...)</asp:ListItem>
                <asp:ListItem Value="2">字母导航(A,B,C...)</asp:ListItem>
                <asp:ListItem Value="3">字母导航(a,b,c...)</asp:ListItem>
                <asp:ListItem Value="4">自定义图片</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list" id="TrNaviPic" style="display:none;">
            <td align="right" class="list_link" style="width: 28%">导航图片地址</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="NaviPic" runat="server" CssClass="form" Width="120px"></asp:TextBox>
              &nbsp;
              <input class="form" type="button" value="选择图片"  onclick="selectFile('pic',document.ListLabel.NaviPic,280,380);document.ListLabel.NaviPic.focus();" /></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">标题显示字数</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="TitleNumer" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="spanTitleNumer"></span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">内容截取字数</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="ContentNumber" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="spanContentNumber"></span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">导航截取字数</td>
            <td width="79%" align="left" class="list_link"><asp:TextBox ID="NaviNumber" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="spanNaviNumber"></span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%">是否调用子类</td>
            <td width="79%" align="left" class="list_link"><asp:DropDownList ID="isSub" runat="server" CssClass="form" Width="200px">
                <asp:ListItem Value="0">请选择是否调用</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
                <asp:ListItem Value="0">否</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="list_link" style="width: 28%"></td>
            <td width="79%" align="left" class="list_link">&nbsp;<input class="form" type="button" value=" 确 定 "  runat="server" onclick="javascript:ReturnDivValue();" id="savv"/>&nbsp;<input class="form" type="button" value=" 关 闭 "  onclick="javascript:CloseDiv();" /></td>
          </tr>
        </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
function selectRoot(type)
{
    if(type=="true")
    {
        document.getElementById("TrStyleID").style.display="";
        document.getElementById("TrUserDefined").style.display="none";
    }
    else
    {
        document.getElementById("TrStyleID").style.display="none";
        document.getElementById("TrUserDefined").style.display="";
    }
}
function selectNewsType(type)
{
    switch(type)
    {
        case "Special":
            document.getElementById("TrSpecialID").style.display="";
            document.getElementById("TrClassId").style.display="none";
            document.getElementById("TrMarqDirec").style.display="none";
            document.getElementById("TrMarqSpeed").style.display="none";
            document.getElementById("TrMarqwidth").style.display="none";
            document.getElementById("TrMarqheight").style.display="none";
            break;
//        case "MarQuee":
//            document.getElementById("TrMarqDirec").style.display="";
//            document.getElementById("TrMarqSpeed").style.display="";
//            document.getElementById("TrMarqwidth").style.display="";
//            document.getElementById("TrMarqheight").style.display="";
//            break;
        case "ANN":
            document.getElementById("TrMarqDirec").style.display="";
            document.getElementById("TrMarqSpeed").style.display="";
            document.getElementById("TrMarqwidth").style.display="";
            document.getElementById("TrMarqheight").style.display="";
            break;
        default:
            document.getElementById("TrClassId").style.display="";
            document.getElementById("TrSpecialID").style.display="none";
            document.getElementById("TrMarqDirec").style.display="none";
            document.getElementById("TrMarqSpeed").style.display="none";
            document.getElementById("TrMarqwidth").style.display="none";
            document.getElementById("TrMarqheight").style.display="none";
            break;
    }
}
function selectShowNavi(type)
{
    if(type=="4")
    {
        document.getElementById("TrNaviPic").style.display="";
    }
    else
    {
        document.getElementById("TrNaviPic").style.display="none";
    }
}

function ReturnDivValue()
{
    spanClear();
    var CheckStr=true;
    if(document.ListLabel.NewsType.value=="Special")
    {
        if(checkIsNull(document.ListLabel.SpecialID,document.getElementById("spanSpecialID"),"请选择专题栏目"))
            CheckStr=false;
    }
    if(checkIsNull(document.ListLabel.Number,document.getElementById("spanNumber"),"循环数目不能为空"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.Number,document.getElementById("spanNumber"),"循环数目只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.ClickNumber,document.getElementById("spanClickNumber"),"点击次数只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.ShowDateNumer,document.getElementById("spanShowDateNumer"),"显示多少天天数只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.Cols,document.getElementById("spanCols"),"每行显示条数只能为正整数"))
        CheckStr=false;
        
    if(checkIsNumber(document.ListLabel.MarqSpeed,document.getElementById("sapnMarqSpeed"),"滚动速度只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.Marqwidth,document.getElementById("sapnMarqwidth"),"滚动宽度只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.Marqheight,document.getElementById("sapnMarqheight"),"滚动高度只能为正整数"))
        CheckStr=false;
                
    if(checkIsNumber(document.ListLabel.TitleNumer,document.getElementById("spanTitleNumer"),"标题显示字数只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.ContentNumber,document.getElementById("spanContentNumber"),"内容截取字数只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.NaviNumber,document.getElementById("spanNaviNumber"),"导航截取字数只能为正整数"))
        CheckStr=false;
    
    //--------------------返回标签值
    var temproot = "";
    var rvalue="[FS:Loop,FS:SiteID=<%Response.Write(APIID); %>";
        
    rvalue += ",FS:NewsType=" + document.ListLabel.NewsType.value;
    if(document.ListLabel.NewsType.value=="Special")
    {
    if(document.ListLabel.SpecialID.value!=""){ rvalue += ",FS:SpecialID=" + document.ListLabel.SpecialID.value; }
    }
    else
    {
    if(document.ListLabel.ClassId.value!=""){ rvalue += ",FS:ClassID=" + document.ListLabel.ClassId.value;}
    else { rvalue += ",FS:ClassID=0";} 
    }
    if(document.ListLabel.Number.value!=""){ rvalue += ",FS:Number=" + document.ListLabel.Number.value; }
    
    if(document.ListLabel.ClickNumber.value!=""){ rvalue += ",FS:ClickNumber=" + document.ListLabel.ClickNumber.value; }
    else {rvalue += ",FS:ClickNumber=0";}
    
    if(document.ListLabel.ShowDateNumer.value!=""){ rvalue += ",FS:ShowDateNumer=" + document.ListLabel.ShowDateNumer.value; }
  else {rvalue += ",FS:ShowDateNumer=7";}
    
    if(document.ListLabel.Cols.value!=""){ rvalue += ",FS:Cols=" + document.ListLabel.Cols.value; }
    else {rvalue += ",FS:Cols=1";}
    
    if(document.ListLabel.NewsType.value=="MarQuee")
    {
        if(document.ListLabel.MarqDirec.value!=""){ rvalue += ",FS:MarqDirec=" + document.ListLabel.MarqDirec.value; }
        else {rvalue += ",FS:MarqDirec=1";}
        if(document.ListLabel.MarqSpeed.value!=""){ rvalue += ",FS:MarqSpeed=" + document.ListLabel.MarqSpeed.value; }
        else {rvalue += ",FS:MarqSpeed=10";}
        if(document.ListLabel.Marqwidth.value!=""){ rvalue += ",FS:Marqwidth=" + document.ListLabel.Marqwidth.value; }
        else {rvalue += ",FS:Marqwidth=10";}
        if(document.ListLabel.Marqheight.value!=""){ rvalue += ",FS:Marqheight=" + document.ListLabel.Marqheight.value; }
        else {rvalue += ",FS:Marqheight=15";}
    }
    
    if(document.ListLabel.isPic.value!=""){ rvalue += ",FS:isPic=" + document.ListLabel.isPic.value; }
    else {rvalue += ",FS:isPic=0";}
    
    if(document.ListLabel.DescType.value!=""){ rvalue += ",FS:DescType=" + document.ListLabel.DescType.value; }
    else { rvalue += ",FS:DescType=1";}
    
    if(document.ListLabel.Desc.value!=""){ rvalue += ",FS:Desc=" + document.ListLabel.Desc.value; }
    else {rvalue += ",FS:Desc=0";}
    
    if(document.ListLabel.ShowNavi.value!=""){ rvalue += ",FS:ShowNavi=" + document.ListLabel.ShowNavi.value; }
    else { rvalue += ",FS:ShowNavi=0";}
    //if(document.ListLabel.ShowNavi.value=="4")
     if(document.ListLabel.NaviPic.value!=""){ rvalue += ",FS:NaviPic=" + document.ListLabel.NaviPic.value; }
     else {rvalue += ",FS:NaviPic=0";}
    
    if(document.ListLabel.TitleNumer.value!=""){ rvalue += ",FS:TitleNumer=" + document.ListLabel.TitleNumer.value; }
    else { rvalue += ",FS:TitleNumer=10";}
    if(document.ListLabel.ContentNumber.value!=""){ rvalue += ",FS:ContentNumber=" + document.ListLabel.ContentNumber.value; }
    else {rvalue += ",FS:ContentNumber=200";}
    if(document.ListLabel.NaviNumber.value!=""){ rvalue += ",FS:NaviNumber=" + document.ListLabel.NaviNumber.value; }
    else {rvalue += ",FS:NaviNumber=5";}
    if(document.ListLabel.isSub.value!=""){ rvalue += ",FS:isSub=" + document.ListLabel.isSub.value; }
    else {rvalue += ",FS:isSub=0";}
    
    rvalue += "]";
    if(document.ListLabel.Root.value=="true")
   
        { 
        if(document.ListLabel.StyleID.value=="")
        {
            alert("请选择样式");
            return false;
        }
        temproot = "[#FS:StyleID=" + document.ListLabel.StyleID.value+"]"; }
    else
        { 
            var oEditor = FCKeditorAPI.GetInstance("UserDefined");
            temproot = oEditor.GetXHTML(true);
         }
    rvalue += temproot;
    rvalue += "[/FS:Loop]";
    if(CheckStr)
	    parent.getValue(rvalue);
        parent.document.getElementById("LabelDivid").style.display="none";
}

function CloseDiv()
{
    parent.document.getElementById("LabelDivid").style.display="none";
}

function spanClear()
{
    document.getElementById("spanSpecialID").innerHTML="";
    document.getElementById("spanNumber").innerHTML="";
    document.getElementById("spanClickNumber").innerHTML="";
    document.getElementById("spanCols").innerHTML="";
    document.getElementById("spanShowDateNumer").innerHTML="";
    document.getElementById("spanTitleNumer").innerHTML="";
    document.getElementById("spanContentNumber").innerHTML="";
    document.getElementById("spanNaviNumber").innerHTML="";
    document.getElementById("sapnMarqSpeed").innerHTML="";
    document.getElementById("sapnMarqwidth").innerHTML="";
    document.getElementById("sapnMarqheight").innerHTML="";
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
function checkIsNumber(obj,spanobj,error)
{
    var re = /^[0-9]*$$/;
    if(re.test(obj.value)==false)
    {
        spanobj.innerHTML="<span class=reshow>(*)"+error+"</spna>";
        return true;
    }
    return false;
}
function getValue(value)
{
    var oEditor = FCKeditorAPI.GetInstance("UserDefined");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml(value);
    }
    else
    {
        return false;
    }
}
function setValue(value)
{
    if(value == "")
    {
        return;
    }
    var oEditor = FCKeditorAPI.GetInstance("UserDefined");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml('{#FS:define='+value+'}');
    }
    else
    {
        return false;
    }
}
</script>
