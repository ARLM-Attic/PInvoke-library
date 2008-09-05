<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_createLabel_List" Codebehind="createLabel_List.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/jsPublic.js"></script>
<script type="text/javascript" src="../../editor/fckeditor.js"></script>
</head>
<body>
    <form id="ListLabel" runat="server">   
        <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">列表类型</td>
            <td align="left" class="navi_link"><asp:DropDownList ID="NewsType" runat="server" Width="150px" CssClass="form" onchange="javascript:selectNewsType(this.value);" >
                <asp:ListItem Value="list">栏目列表(任意指定条件)</asp:ListItem>
                <asp:ListItem Value="Last">最新新闻</asp:ListItem>
                <asp:ListItem Value="Rec">推荐新闻</asp:ListItem>
                <asp:ListItem Value="Hot">热点新闻</asp:ListItem>
                <asp:ListItem Value="Tnews">头条新闻</asp:ListItem>
                <asp:ListItem Value="Jnews">精彩新闻</asp:ListItem>
                <asp:ListItem Value="ANN">公告新闻</asp:ListItem>
                <asp:ListItem Value="MarQuee">滚动新闻</asp:ListItem>
                <asp:ListItem Value="Special">专题新闻</asp:ListItem>
                <asp:ListItem Value="SubNews">子类新闻</asp:ListItem>
             </asp:DropDownList>
              循环条数
              <asp:TextBox ID="Number" runat="server" CssClass="form" Width="60px" Text="10"></asp:TextBox>&nbsp;<span id="spanNumber"></span>
              </td>
          </tr>
          <tr class="TR_BG_list" id="TrClassId">
            <td align="right" class="navi_link" style="width:105px" valign="top">栏目ID</td>
            <td align="left" class="navi_link"><asp:TextBox ReadOnly="true" ID="ClassName" onpropertychange="javascript:changetrMore(this);" runat="server" CssClass="form" Width="120px"></asp:TextBox><input id="ClassId" type="hidden" />
            <span class="reshow" id="getClassCname"></span>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择栏目"  onclick="selectFile('newsclass',new Array(document.ListLabel.ClassId,document.ListLabel.ClassName),300,380);document.ListLabel.ClassName.focus();" />
              调用类型：<a href="javascript:void(0);" onclick="getType(-1);" class="reshow">所有</a>&nbsp;&nbsp;<a href="javascript:void(0);" onclick="getType(0);" class="reshow">自适应</a><br /><span style="color:Blue;">说明：如果填写为0或者为空，调用标签所在栏目的符合条件新闻(自适应),如果不在栏目，则调用所有。如果为-1，则调用所有。</span>
              </td>
          </tr>
          <tr class="TR_BG_list" id="TrSpecialID" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">专题栏目</td>
            <td align="left" class="navi_link"><asp:TextBox ID="SpecialName" runat="server" CssClass="form" Width="120px" ReadOnly="true"></asp:TextBox><input id="SpecialID" type="hidden" />
              &nbsp;
              <img src="../../sysImages/folder/s.gif" alt="" style="cursor:pointer;" title="选择专题"  onclick="selectFile('special',new Array(document.ListLabel.SpecialID,document.ListLabel.SpecialName),300,380);document.ListLabel.SpecialName.focus();" /><span id="spanSpecialID"></span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">引用样式<span id="url"></span></td>
            <td align="left" class="navi_link"><asp:DropDownList ID="Root" runat="server" CssClass="form" Width="100px" onchange="javascript:selectRoot(this.value);">
                <asp:ListItem Value="true">固定样式</asp:ListItem>
                <asp:ListItem Value="false">自定义样式</asp:ListItem>
              </asp:DropDownList>
              <label id="TrStyleID">
              <asp:TextBox ID="StyleID" runat="server" CssClass="form" Width="100px" ReadOnly="true"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer" title="选择样式" onclick="selectFile('style',document.getElementById('StyleID'),240,550);document.ListLabel.StyleID.focus();" /><span id="sapnStyleID"></span>
              </label>
              </td>
          </tr>
          <tr class="TR_BG_list" id="TrUserDefined" style="display:none;">
            <td align="right" class="navi_link">
            <div style="margin:5px 0 5px 0;"><a style="color:blue;cursor:pointer;" onclick="selectFile('picEdit',document.getElementById('UserDefined'),320,500);" title="在上传的时候，请在编辑区鼠标点击，设置要上传图片的位置。">选择图片</a></div>
            
            自定义样式
            <div style="height:3px;margin:2px 0 4px 0;"></div>
            <input name="saveStyled" value="保存样式" type="button" onclick="ShowStyle();" />
            <div id="showOther" style="display:none;">
            <div style="height:3px;border-bottom:1px dotted #999999;margin:2px 0 4px 0;"></div>
            <asp:TextBox ID="StyleName" Width="94px" runat="server"></asp:TextBox>
            <div style="height:3px;border-bottom:1px dotted #999999;margin:2px 0 4px 0;"></div>
            <asp:DropDownList ID="StyleClassID"  Width="100px" runat="server">
            </asp:DropDownList>
            <div style="height:3px;border-bottom:1px dotted #999999;margin:2px 0 4px 0;"></div>
            <input name="saveStyle" id="saveStyle" value="保存" type="button" onclick="savePostStyle();" />
            <div id="sResultHTML" class="reshow"></div>
            </div>
            </td>
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
            oFCKeditor.Width = '100%' ;
            oFCKeditor.ToolbarSet = 'Foosun_Basicstyle';
            oFCKeditor.Height = '150' ;	
            oFCKeditor.ReplaceTextarea() ;
            }
        </script>
        <textarea rows="1" cols="1" name="UserDefined" style="display:none" id="UserDefined" ></textarea>
        <script language="javascript" type="text/javascript">
        function insertHTMLEdit(url)
            {
                var urls = url.replace('{@dirfile}','<% Response.Write(Foosun.Config.UIConfig.dirFile); %>')
                var oEditor = FCKeditorAPI.GetInstance("UserDefined");
                if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
                {
                   oEditor.InsertHtml('<img src=\"'+urls+'\" border=\"0\" />');
                }
                else
                {
                    return false;
                }
                return;
            }
        function ShowStyle()
        {
            if(document.getElementById("showOther").style.display=="none")
            {
               document.getElementById("showOther").style.display="";
            }
        }
        function savePostStyle()
        {
            var saveStyle= document.getElementById("saveStyle");
            var sname=document.getElementById("StyleName");
            var StyleClassID=document.getElementById("StyleClassID");
            if(sname.value=="")
            {
                alert('请填写样式名称');
                sname.focus();
                return false;
            }
            if(StyleClassID.value=="")
            {
                alert('请选择分类，如果没有分类，请在样式中创建');
                return false;
            }
            var olEditor = FCKeditorAPI.GetInstance("UserDefined");
            var gtemproot = olEditor.GetXHTML(true);
            
            var url="SaveStyle.aspx"
            var actionstr="StyleName="+escape(sname.value)+"&ClassID="+escape(StyleClassID.value)+"&Content="+escape(gtemproot)+"";
            var divID = "sResultHTML";
            pubPostajax(url,actionstr,divID)
        }
        </script> 
        </td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">每行显示多少条</td>
            <td align="left" class="navi_link">
             <asp:TextBox ID="Cols" title="只对table格式有效，如果为div格式请控制<li的属性>" runat="server" CssClass="form" Width="60px"></asp:TextBox><span id="spanCols"></span>
            &nbsp;点击大于<asp:TextBox ID="ClickNumber" runat="server" CssClass="form" Width="60px"></asp:TextBox><span id="spanClickNumber"></span>
            &nbsp;显示多少天内信息&nbsp;<asp:TextBox ID="ShowDateNumer" runat="server" CssClass="form" Width="60px"></asp:TextBox><span id="spanShowDateNumer"></span>
            </td>
          </tr>
          
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">标题显示字数</td>
            <td align="left" class="navi_link"><asp:TextBox ID="TitleNumer" runat="server" CssClass="form" Width="60px"></asp:TextBox><span id="spanTitleNumer"></span>
             内容截取字数
             <asp:TextBox ID="ContentNumber" runat="server" CssClass="form" Width="60px"></asp:TextBox><span id="spanContentNumber"></span>
              是否有导读 <asp:DropDownList ID="HashNaviContent" runat="server" onchange="getNaviNumber(this.value);">
                <asp:ListItem Value="">是否有导读</asp:ListItem>
                <asp:ListItem Value="true">有导读</asp:ListItem>
                <asp:ListItem Value="false">无导读</asp:ListItem>
                </asp:DropDownList>
             <span id="TRNaviNumber">导读截取字数
             <asp:TextBox ID="NaviNumber" runat="server" CssClass="form" Width="60px"></asp:TextBox></span><span id="spanNaviNumber"></span>
            </td>
          </tr>          
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">调用子(副)新闻</td>
            <td align="left" class="navi_link">
            <asp:DropDownList ID="SubNews" runat="server" CssClass="form" Width="100px"  onchange="javascript:selectShowSubNavi(this.value);">
                <asp:ListItem Value="">是否调用</asp:ListItem>
                <asp:ListItem Value="true">是</asp:ListItem>
                <asp:ListItem Value="false">否</asp:ListItem>
              </asp:DropDownList>
              <span id ="span_isSub">
              调用子类
                <asp:DropDownList ID="isSub" runat="server" CssClass="form" Width="100px">
                <asp:ListItem Value="">是否调用</asp:ListItem>
                <asp:ListItem Value="true">是</asp:ListItem>
                <asp:ListItem Value="false">否</asp:ListItem>
              </asp:DropDownList>
              </span>
              输出格式
            <asp:DropDownList ID="isDiv" runat="server" CssClass="form" Width="200px">
                <asp:ListItem Value="">输出格式</asp:ListItem>
                <asp:ListItem Value="false">Table</asp:ListItem>
                <asp:ListItem Value="true">Div(默认,请在样式里定义li或者dd)</asp:ListItem>
              </asp:DropDownList>
              
              <span id="span_classnamestyle" style="display:none;">
              子类栏目样式
              <asp:TextBox ID="ClassStyle" runat="server" CssClass="form" Width="60px"></asp:TextBox>
              </span>
             </td>
          </tr>
          <tr class="TR_BG_list" id="TrulID" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">DIV的ul属性ID</td>
            <td align="left" class="navi_link"> 
                <asp:TextBox ID="ulID" runat="server" CssClass="form" Width="190px"></asp:TextBox></td>
          </tr>
          <tr class="TR_BG_list" id="TrulClass" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">DIV的ul属性Class</td>
            <td align="left" class="navi_link">
                <asp:TextBox ID="ulClass" runat="server" CssClass="form" Width="190px"></asp:TextBox></td>
          </tr>

          <tr class="TR_BG_list" id="TrMarqDirec" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">滚动方向</td>
            <td align="left" class="navi_link">
                <asp:DropDownList ID="MarqDirec" runat="server" CssClass="form" Width="200px">
                <asp:ListItem Value="">滚动方向</asp:ListItem>
                <asp:ListItem Value="up">上</asp:ListItem>
                <asp:ListItem Value="down">下</asp:ListItem>
                <asp:ListItem Value="left">左</asp:ListItem>
                <asp:ListItem Value="right">右</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list" id="TrMarqSpeed" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">滚动速度</td>
            <td align="left" class="navi_link">
                <asp:TextBox ID="MarqSpeed" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="sapnMarqSpeed"></span></td>
          </tr>
          <tr class="TR_BG_list" id="TrMarqwidth" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">宽度</td>
            <td align="left" class="navi_link">
                <asp:TextBox ID="Marqwidth" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="sapnMarqwidth"></span></td>
          </tr>
          <tr class="TR_BG_list" id="TrMarqheight" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">高度</td>
            <td align="left" class="navi_link">
                <asp:TextBox ID="Marqheight" runat="server" CssClass="form" Width="190px"></asp:TextBox><span id="sapnMarqheight"></span></td>
          </tr>
          
          <tr class="TR_BG_list" id="TrSubNaviCSS" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">副新闻导航文字或图片</td>
            <td align="left" class="navi_link">
            <asp:TextBox ID="SubNaviCSS" runat="server" CssClass="form" Width="190px" />
            <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择图片"  onclick="selectFile('pic',document.ListLabel.SubNaviCSS,380,400);document.ListLabel.SubNaviCSS.focus();" />
            </td>
          </tr>
          
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">调用图片</td>
            <td align="left" class="navi_link">
            <asp:DropDownList ID="isPic" runat="server" CssClass="form" Width="100px">
                <asp:ListItem Value="">是否调用</asp:ListItem>
                <asp:ListItem Value="true">是</asp:ListItem>
                <asp:ListItem Value="false">否</asp:ListItem>
              </asp:DropDownList>
               排列方式
                <asp:DropDownList ID="DescType" runat="server" CssClass="form" Width="100px">
                <asp:ListItem Value="">排序方式</asp:ListItem>
                <asp:ListItem Value="id">自动编号</asp:ListItem>
                <asp:ListItem Value="date">添加日期</asp:ListItem>
                <asp:ListItem Value="click">点击次数</asp:ListItem>
                <asp:ListItem Value="pop">权重</asp:ListItem>
                <asp:ListItem Value="digg">digg(顶客)</asp:ListItem>
              </asp:DropDownList>    
                排序顺序
                <asp:DropDownList ID="Desc" runat="server" CssClass="form" Width="100px">
                <asp:ListItem Value="">排序顺序</asp:ListItem>
                <asp:ListItem Value="desc">desc(降序)</asp:ListItem>
                <asp:ListItem Value="asc">asc(升序)</asp:ListItem>
              </asp:DropDownList>         
              </td>
          </tr>

          <tr class="TR_BG_list" style="display:;">
            <td align="right" class="navi_link" style="width:105px">在标题前加导航</td>
            <td align="left" class="navi_link">
             <asp:DropDownList ID="ShowNavi" runat="server" CssClass="form" Width="100px" onchange="javascript:selectShowNavi(this.value);">
                <asp:ListItem Value="">是否加导航</asp:ListItem>
                <asp:ListItem Value="1">数字导航(1,2,3...)</asp:ListItem>
                <asp:ListItem Value="2">字母导航(A,B,C...)</asp:ListItem>
                <asp:ListItem Value="3">字母导航(a,b,c...)</asp:ListItem>
                <asp:ListItem Value="4">自定义图片</asp:ListItem>
              </asp:DropDownList>
              <span class="reshow">div+CSS格式，如果设置此项会前台生成变形！建议在CSS里控制li等的属性</span>
              <label id="TrNaviCSS" style="display:none;">导航CSS：<asp:TextBox ID="NaviCSS" title="如果为空，可以在前台CSS里控制<dd>的属性" Width="80" CssClass="form" runat="server"></asp:TextBox></label>
              <label id="TrNaviPic" style="display:none;">导航图片地址：<asp:TextBox ID="NaviPic" runat="server" CssClass="form" Width="120px" ReadOnly="true"></asp:TextBox>
               <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择图片"  onclick="selectFile('pic',document.ListLabel.NaviPic,280,380);document.ListLabel.NaviPic.focus();" /></label>            </td>
          </tr>
 
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">行参数控制</td>
            <td align="left" class="navi_link">
             奇数行背景CSS：<asp:TextBox ID="css1" Width="50" CssClass="form" runat="server"></asp:TextBox>&nbsp;偶数行背景CSS：<asp:TextBox ID="css2" CssClass="form" Width="50" runat="server"></asp:TextBox>
            </td>
          </tr>
          <tr class="TR_BG_list" id="tr_more" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">更多设置</td>
            <td align="left" class="navi_link">
             <asp:TextBox ID="More" Width="150" CssClass="form" runat="server"></asp:TextBox><img src="../../sysImages/folder/s.gif" style="cursor:pointer" title="设置更多图片" onclick="selectFile('pic',document.getElementById('More'),240,550);" />
            <span class="reshow">填写字符或选择更多图片</span></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px"></td>
            <td align="left" class="navi_link">&nbsp;<input class="form" type="button" value=" 确 定 "  onclick="javascript:ReturnDivValue();" />&nbsp;<input class="form" type="button" value=" 关 闭 "  onclick="javascript:CloseDiv();" /></td>
          </tr>
        </table>

    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
function getNaviNumber(obj)
{
    if(obj=="false")
   {
        document.getElementById("TRNaviNumber").style.display="none";
   } 
   else
   {
        document.getElementById("TRNaviNumber").style.display="";
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
            document.getElementById("span_isSub").style.display="";
            document.getElementById("span_classnamestyle").style.display="none";
            document.ListLabel.ClassStyle.value="";
           break;
//        case "MarQuee":
//            document.getElementById("TrMarqDirec").style.display="";
//            document.getElementById("TrMarqSpeed").style.display="";
//            document.getElementById("TrMarqwidth").style.display="";
//            document.getElementById("TrMarqheight").style.display="";
//            document.getElementById("TrClassId").style.display="";
//            document.getElementById("TrSpecialID").style.display="none";
//            document.getElementById("span_isSub").style.display="";
//            document.getElementById("span_classnamestyle").style.display="none";
//            document.ListLabel.ClassStyle.value="";
//            break;
        case "SubNews":
            document.getElementById("TrMarqDirec").style.display="none";
            document.getElementById("TrMarqSpeed").style.display="none";
            document.getElementById("TrMarqwidth").style.display="none";
            document.getElementById("TrMarqheight").style.display="none";
            document.getElementById("TrClassId").style.display="none";
            document.getElementById("span_isSub").style.display="none";
            document.getElementById("span_classnamestyle").style.display="";
            document.ListLabel.isSub.value="";
            break;
        default:
            document.getElementById("TrClassId").style.display="";
            document.getElementById("TrSpecialID").style.display="none";
            document.getElementById("TrMarqDirec").style.display="none";
            document.getElementById("TrMarqSpeed").style.display="none";
            document.getElementById("TrMarqwidth").style.display="none";
            document.getElementById("TrMarqheight").style.display="none";
            document.getElementById("span_isSub").style.display="";
            document.getElementById("span_classnamestyle").style.display="none";
            document.ListLabel.ClassStyle.value="";
            break;
    }
}
function selectisDiv(type)
{
    if(type=="true")
    {
        document.getElementById("TrulID").style.display="";
        document.getElementById("TrulClass").style.display="";
    }
    else
    {
        document.getElementById("TrulID").style.display="none";
        document.getElementById("TrulClass").style.display="none";
    }
}
function selectShowSubNavi(type)
{
    if(type=="true")
    {
       document.getElementById("TrSubNaviCSS").style.display=""; 
    }
    else
    {
       document.getElementById("TrSubNaviCSS").style.display="none"; 
    }
}
function selectShowNavi(type)
{
    if(type=="4")
    {
        document.getElementById("TrNaviPic").style.display="";
        document.getElementById("TrNaviCSS").style.display="none";
    }
    else
    {
        if(type=="")
        {
            document.getElementById("TrNaviCSS").style.display="none";
            document.getElementById("TrNaviPic").style.display="none";
        }
        else
        {
            document.getElementById("TrNaviCSS").style.display="";
            document.getElementById("TrNaviPic").style.display="none";
        }
    }
}
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
    if(checkIsNumber(document.ListLabel.NaviNumber,document.getElementById("spanNaviNumber"),"导读截取字数只能为正整数"))
        CheckStr=false;
    if(document.ListLabel.Root.value=="true")
    {
       if(checkIsNull(document.ListLabel.StyleID,document.getElementById("sapnStyleID"),"请选择样式"))
        CheckStr=false;
    }
    //--------------------返回标签值
    var temproot = "";
    var rvalue="[FS:Loop,FS:SiteID=<%Response.Write(SiteID); %>,FS:LabelType=List";
    
    if(document.ListLabel.Number.value!=""){ rvalue += ",FS:Number=" + document.ListLabel.Number.value; }
    if(document.ListLabel.Root.value=="true")
        { temproot = "[#FS:StyleID=" + document.ListLabel.StyleID.value+"]"; }
    else
        { 
            var oEditor = FCKeditorAPI.GetInstance("UserDefined");
            temproot = oEditor.GetXHTML(true);
        }
    rvalue += ",FS:NewsType=" + document.ListLabel.NewsType.value;
    if(document.ListLabel.NewsType.value=="Special")
        { rvalue += ",FS:SpecialID=" + document.ListLabel.SpecialID.value; }
    else
        { if(document.ListLabel.ClassId.value!="") { rvalue += ",FS:ClassID=" + document.ListLabel.ClassId.value } }
    if(document.ListLabel.SubNews.value!="")
    { 
        rvalue += ",FS:SubNews=" + document.ListLabel.SubNews.value; 
        if(document.ListLabel.SubNaviCSS.value!="")
        {
            rvalue += ",FS:SubNaviCSS=" + document.ListLabel.SubNaviCSS.value; 
        }
    }
    if(document.ListLabel.Cols.value!=""){ rvalue += ",FS:Cols=" + document.ListLabel.Cols.value; }
    if(document.ListLabel.Desc.value!=""){ rvalue += ",FS:Desc=" + document.ListLabel.Desc.value; }
    if(document.ListLabel.DescType.value!=""){ rvalue += ",FS:DescType=" + document.ListLabel.DescType.value; }
    if(document.ListLabel.isDiv.value!=""){ rvalue += ",FS:isDiv=" + document.ListLabel.isDiv.value; }
    if(document.ListLabel.isDiv.value=="true")
    {
        if(document.ListLabel.ulID.value!=""){ rvalue += ",FS:ulID=" + document.ListLabel.ulID.value; }
        if(document.ListLabel.ulClass.value!=""){ rvalue += ",FS:ulClass=" + document.ListLabel.ulClass.value; }
    }
//    if(document.ListLabel.NewsType.value=="MarQuee")
//    {
//        if(document.ListLabel.MarqDirec.value!=""){ rvalue += ",FS:MarqDirec=" + document.ListLabel.MarqDirec.value; }
//        if(document.ListLabel.MarqSpeed.value!=""){ rvalue += ",FS:MarqSpeed=" + document.ListLabel.MarqSpeed.value; }
//        if(document.ListLabel.Marqwidth.value!=""){ rvalue += ",FS:Marqwidth=" + document.ListLabel.Marqwidth.value; }
//        if(document.ListLabel.Marqheight.value!=""){ rvalue += ",FS:Marqheight=" + document.ListLabel.Marqheight.value; }
//    }
    if(document.ListLabel.isPic.value!=""){ rvalue += ",FS:isPic=" + document.ListLabel.isPic.value; }
    if(document.ListLabel.TitleNumer.value!=""){ rvalue += ",FS:TitleNumer=" + document.ListLabel.TitleNumer.value; }
   if(document.ListLabel.HashNaviContent.value!=""){rvalue += ",FS:HashNaviContent=" + document.ListLabel.HashNaviContent.value; } 
    if(document.ListLabel.ContentNumber.value!=""){ rvalue += ",FS:ContentNumber=" + document.ListLabel.ContentNumber.value; }
    if(document.ListLabel.NaviNumber.value!=""){ rvalue += ",FS:NaviNumber=" + document.ListLabel.NaviNumber.value; }
    if(document.ListLabel.ClickNumber.value!=""){ rvalue += ",FS:ClickNumber=" + document.ListLabel.ClickNumber.value; }
    if(document.ListLabel.ShowDateNumer.value!=""){ rvalue += ",FS:ShowDateNumer=" + document.ListLabel.ShowDateNumer.value; }
    if(document.ListLabel.isSub.value!=""){ rvalue += ",FS:isSub=" + document.ListLabel.isSub.value; }
    if(document.ListLabel.ShowNavi.value!=""){ rvalue += ",FS:ShowNavi=" + document.ListLabel.ShowNavi.value; }
    if(document.ListLabel.ShowNavi.value!=""&&document.ListLabel.ShowNavi.value!="4") {rvalue += ",FS:NaviCSS=" + document.ListLabel.NaviCSS.value;}
    if(document.ListLabel.ClassStyle.value!=""){ rvalue += ",FS:ClassStyle=" + document.ListLabel.ClassStyle.value; }
    if(document.ListLabel.ShowNavi.value=="4")
    { if(document.ListLabel.NaviPic.value!=""){ rvalue += ",FS:NaviPic=" + document.ListLabel.NaviPic.value; } }
    if(document.ListLabel.css1.value!=""&&document.ListLabel.css2.value!=""){ rvalue += ",FS:ColbgCSS=" + document.ListLabel.css1.value+"|"+document.ListLabel.css2.value; }
    if(document.ListLabel.More.value!="")
    {
        rvalue+=",FS:More="+document.ListLabel.More.value;
    }
    rvalue += "]";
    rvalue += temproot;
    rvalue += "[/FS:Loop]";
    
    if(CheckStr)
	    parent.getValue(rvalue);
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
    document.getElementById("sapnStyleID").innerHTML="";
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
    var oEditor = FCKeditorAPI.GetInstance("UserDefined");
    if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
    {
       oEditor.InsertHtml('{#FS:define='+value+'} ');
    }
    else
    {
    return false;
    }
}
function getType(value)
{
    document.getElementById("ClassId").value=value;
    if(value=="-1")
        document.getElementById("ClassName").value="调用所有";
    else
        document.getElementById("ClassName").value="自适应";
}
    new Form.Element.Observer($('ClassId'),1,getClassCName);
    function getClassCName()
    {
        var strC=document.getElementById("ClassId").value;
	    var  options={  
				       method:'get',  
				       parameters:"Type=Class&ClassID="+strC,  
				       onComplete:function(transport)
					    {  
						    var returnvalue=transport.responseText;
						    if (returnvalue.indexOf("??")>-1)
                               $('getClassCname').innerHTML="error!";
						    else
                               $('getClassCname').innerHTML=returnvalue;
					    }  
				       }; 
	    new  Ajax.Request('../../configuration/system/getClassCname.aspx?no-cache='+Math.random(),options);
    }  

function changetrMore(obj)
{
    if(obj.value!="")
    {
        if(obj.value=="调用所有"||obj.value=="自适应")
        {
            document.getElementById("tr_more").style.display="none";
            return;
        }
        document.getElementById("tr_more").style.display="block";
    }
    else
    {
        document.getElementById("tr_more").style.display="none";
    }
}
</script>


