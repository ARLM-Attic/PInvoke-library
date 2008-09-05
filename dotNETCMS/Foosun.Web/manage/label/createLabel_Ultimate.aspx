<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_createLabel_Ultimate" Codebehind="createLabel_Ultimate.aspx.cs" %>
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
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF" class="table">
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px;">列表类型</td>
            <td align="left" class="navi_link"><asp:DropDownList ID="ListType" runat="server" Width="200px" CssClass="form" onchange="//javascript:selectListType(this.value);">
                <asp:ListItem Value="News">新闻</asp:ListItem>
                <asp:ListItem Value="Special">专题</asp:ListItem>
              </asp:DropDownList></td>
          </tr>

          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px;">引用样式</td>
            <td align="left" class="navi_link"><asp:DropDownList ID="Root" runat="server" CssClass="form" Width="100px" onchange="javascript:selectRoot(this.value);">
                <asp:ListItem Value="true">固定样式</asp:ListItem>
                <asp:ListItem Value="false">自定义样式</asp:ListItem>
              </asp:DropDownList>
               <label id="TrStyleID">
               引用样式
              <asp:TextBox ID="StyleID" runat="server" CssClass="form" Width="120px" ReadOnly="true"></asp:TextBox>
              <img src="../../sysImages/folder/s.gif" style="cursor:pointer" title="选择样式" onclick="selectFile('style',document.getElementById('StyleID'),240,550);document.ListLabel.StyleID.focus();" /><span id="sapnStyleID"></span>
              </label>
              </td>
          </tr>
          
          <tr class="TR_BG_list" id="TrUserDefined" style="display:none;">
            <td align="right" class="navi_link" style="width:105px;">
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
                  </asp:DropDownList>
              </div>
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
        </div>
        </td>
          </tr>
          
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px;">调用子类</td>
            <td align="left" class="navi_link">
            <asp:DropDownList ID="isSub" runat="server" CssClass="form" Width="100px">
                <asp:ListItem Value="">是否调用</asp:ListItem>
                <asp:ListItem Value="true">是</asp:ListItem>
                <asp:ListItem Value="false">否</asp:ListItem>
              </asp:DropDownList>
               调用子新闻
               <asp:DropDownList ID="SubNews" runat="server" CssClass="form" Width="100px">
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
            </td>
          </tr>

          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px;">排序顺序</td>
            <td align="left" class="navi_link"><asp:DropDownList ID="Desc" runat="server" CssClass="form" Width="100px">
                <asp:ListItem Value="">排序顺序</asp:ListItem>
                <asp:ListItem Value="desc">desc(降序)</asp:ListItem>
                <asp:ListItem Value="asc">asc(升序)</asp:ListItem>
              </asp:DropDownList>
              调用图片
               <asp:DropDownList ID="isPic" runat="server" CssClass="form" Width="100px">
                <asp:ListItem Value="">是否调用</asp:ListItem>
                <asp:ListItem Value="true">是</asp:ListItem>
                <asp:ListItem Value="false">否</asp:ListItem>
              </asp:DropDownList>  
               输出格式
               <asp:DropDownList ID="isDiv" runat="server" CssClass="form" Width="100px" ><%--onchange="javascript:selectisDiv(this.value);"--%>
                <asp:ListItem Value="">输出格式</asp:ListItem>
                <asp:ListItem Value="false">Table</asp:ListItem>
                <asp:ListItem Value="true">Div</asp:ListItem>
              </asp:DropDownList>
              </td>
          </tr>

         <tr class="TR_BG_list" id="TrulID" style="display:none;">
            <td align="right" class="navi_link" style="width:105px;">DIV的ul属性ID</td>
            <td align="left" class="navi_link"> 
                <asp:TextBox ID="ulID" runat="server" CssClass="form" Width="120px"></asp:TextBox>
                DIV的ul属性Class  <asp:TextBox ID="ulClass" runat="server" CssClass="form" Width="120px"></asp:TextBox>
                </td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px;">标题字数</td>
            <td align="left" class="navi_link">
            <asp:TextBox ID="TitleNumer" runat="server" CssClass="form" Width="50px"></asp:TextBox><span id="spanTitleNumer"></span>
            内容字数<asp:TextBox ID="ContentNumber" runat="server" CssClass="form" Width="50px"></asp:TextBox><span id="spanContentNumber"></span>
            导航字数<asp:TextBox ID="NaviNumber" runat="server" CssClass="form" Width="50px"></asp:TextBox><span id="spanNaviNumber"></span>
            每行显示数<asp:TextBox ID="Cols" runat="server" CssClass="form" Width="50px"></asp:TextBox><span id="spanCols"></span>
            </td>
          </tr>
  
          <tr class="TR_BG_list" style="display:;">
            <td align="right" class="navi_link" style="width:105px;">在标题前加导航</td>
            <td align="left" class="navi_link"><asp:DropDownList ID="ShowNavi" runat="server" CssClass="form" Width="120px" onchange="javascript:selectShowNavi(this.value);">
                <asp:ListItem Value="">是否加导航</asp:ListItem>
                <asp:ListItem Value="1">数字导航(1,2,3...)</asp:ListItem>
                <asp:ListItem Value="2">字母导航(A,B,C...)</asp:ListItem>
                <asp:ListItem Value="3">字母导航(a,b,c...)</asp:ListItem>
                <asp:ListItem Value="4">自定义图片</asp:ListItem>
              </asp:DropDownList>
              <label id="TrNaviCSS" style="display:none;">导航CSS：<asp:TextBox ID="NaviCSS" Width="80" CssClass="form" runat="server"></asp:TextBox></label>
                <label id="TrNaviPic" style="display:none;">
                导航图片地址<asp:TextBox ID="NaviPic" runat="server" CssClass="form" Width="150px" ReadOnly="true"></asp:TextBox>
                <img src="../../sysImages/folder/s.gif" title="选择图片" style="cursor:pointer;"  onclick="selectFile('pic',document.ListLabel.NaviPic,280,380);document.ListLabel.NaviPic.focus();" />
                </label>
              </td>
          </tr>

          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px;">是否分页</td>
            <td align="left" class="navi_link"><asp:DropDownList ID="isPage" runat="server" CssClass="form" Width="200px" onchange="javascript:selectPage(this.value);">
                <asp:ListItem Value="true">请选择是否分页</asp:ListItem>
                <asp:ListItem Value="true">是</asp:ListItem>
                <asp:ListItem Value="false">否</asp:ListItem>
              </asp:DropDownList></td>
          </tr>
          <tr class="TR_BG_list" id="TrPageID">
            <td align="right" class="navi_link" style="width:105px;"><span id="spanPageID"></span>分页样式</td>
            <td align="left" class="navi_link"><asp:TextBox ID="PageID" runat="server" CssClass="form" Width="120px"></asp:TextBox>
              &nbsp;
              <input class="form" type="button" value="选择分页样式"  onclick="javascript:show('PageID',document.getElementById('spanPageID'),'选择分页样式',410,200);" /></td>
          </tr>
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">行参数控制</td>
            <td align="left" class="navi_link">
             奇数行背景CSS：<asp:TextBox ID="css1" Width="50" CssClass="form" runat="server"></asp:TextBox>&nbsp;偶数行背景CSS：<asp:TextBox ID="css2" CssClass="form" Width="50" runat="server"></asp:TextBox>
            </td>
          </tr>
          
          <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px">分行参数控制</td>
            <td align="left" class="navi_link">
                <asp:DropDownList ID="brTF" runat="server" CssClass="form" Width="200px" onchange="javascript:selectTF(this.value);">
                <asp:ListItem Value="false">显示分行效果</asp:ListItem>
                <asp:ListItem Value="true">是</asp:ListItem>
                <asp:ListItem Value="false">否</asp:ListItem>
              </asp:DropDownList>
             </td>
          </tr>
          
          <tr class="TR_BG_list" id="divbrtf" style="display:none;">
            <td align="right" class="navi_link" style="width:105px">定义分行参数</td>
            <td align="left" class="navi_link">
                <asp:TextBox ID="bfstr" Width="80%" CssClass="form" Text="0|5|CSS" runat="server"></asp:TextBox> 每行排列一个起作用<br />
                <span class="reshow">
                格式：0|5|css,第一个参数表示使用样式,第2个表示多少信息使用此设置,第3个参数表示具体参数<br />
                0表示使用CSS样式，如：0|5|tableCSS <br />
                1表示使用使用图片，如：1|5|/templet/br.gif <br />
                2表示使用使用文字，如：2|5|----------------
                </span>
             </td>
          </tr>          
           <tr class="TR_BG_list">
            <td align="right" class="navi_link" style="width:105px;"></td>
            <td align="left" class="navi_link">&nbsp;<input class="form" type="button" value=" 确 定 "  onclick="javascript:ReturnDivValue();" />&nbsp;<input class="form" type="button" value=" 关 闭 "  onclick="javascript:CloseDiv();" /></td>
          </tr>
          
        </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">

function selectisDiv(type)
{
    if(type=="true")
        { document.getElementById("TrulID").style.display="";  }     
    else
        { document.getElementById("TrulID").style.display="none"; }   
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
            document.getElementById("TrNaviPic").style.display="none"; 
            document.getElementById("TrNaviCSS").style.display="none"; 
        }
        else
        {
            document.getElementById("TrNaviPic").style.display="none"; 
            document.getElementById("TrNaviCSS").style.display=""; 
        }
    }
}
function selectRoot(type)
{
    if(type=="false")
        { document.getElementById("TrStyleID").style.display="none";document.getElementById("TrUserDefined").style.display="";  }
    else
        { document.getElementById("TrStyleID").style.display="";document.getElementById("TrUserDefined").style.display="none";  }
}
function selectPage(type)
{
    if(type=="false")
        { document.getElementById("TrPageID").style.display="none";  }
    else
        { document.getElementById("TrPageID").style.display=""; }
}
function selectTF(type)
{
    if(type=="false")
        { document.getElementById("divbrtf").style.display="none";  }
    else
        { document.getElementById("divbrtf").style.display=""; }
}

function CloseDiv()
{
    parent.document.getElementById("LabelDivid").style.display="none";
}

function spanClear()
{
    document.getElementById("spanCols").innerHTML="";
    document.getElementById("spanTitleNumer").innerHTML="";
    document.getElementById("spanContentNumber").innerHTML="";
    document.getElementById("spanNaviNumber").innerHTML="";
    document.getElementById("sapnStyleID").innerHTML="";
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

function ReturnDivValue()
{
    spanClear();
    var CheckStr=true;

    if(checkIsNumber(document.ListLabel.Cols,document.getElementById("spanCols"),"每行显示条数只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.TitleNumer,document.getElementById("spanTitleNumer"),"标题显示字数只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.ContentNumber,document.getElementById("spanContentNumber"),"内容截取字数只能为正整数"))
        CheckStr=false;
    if(checkIsNumber(document.ListLabel.NaviNumber,document.getElementById("spanNaviNumber"),"导航截取字数只能为正整数"))
        CheckStr=false;
    if(document.ListLabel.Root.value=="true")
    {
       if(checkIsNull(document.ListLabel.StyleID,document.getElementById("sapnStyleID"),"请选择样式"))
        CheckStr=false;
    }
    
    var rvalue="[FS:Loop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=ClassList";
    if(document.ListLabel.Root.value=="true")
        { temproot = "[#FS:StyleID=" + document.ListLabel.StyleID.value+"]"; }
    else
        {
                    var oEditor = FCKeditorAPI.GetInstance("UserDefined");
                    temproot = oEditor.GetXHTML(true);
         }
    rvalue += ",FS:ListType=" + document.ListLabel.ListType.value;

    if(document.ListLabel.isSub.value!=""){ rvalue += ",FS:isSub=" + document.ListLabel.isSub.value; }
    if(document.ListLabel.SubNews.value!=""){ rvalue += ",FS:SubNews=" + document.ListLabel.SubNews.value; }
    if(document.ListLabel.Cols.value!=""){ rvalue += ",FS:Cols=" + document.ListLabel.Cols.value; }
    if(document.ListLabel.Desc.value!=""){ rvalue += ",FS:Desc=" + document.ListLabel.Desc.value; }
    if(document.ListLabel.DescType.value!=""){ rvalue += ",FS:DescType=" + document.ListLabel.DescType.value; }
    if(document.ListLabel.isDiv.value!=""){ rvalue += ",FS:isDiv=" + document.ListLabel.isDiv.value; }
    if(document.ListLabel.isDiv.value=="true")
    {
        if(document.ListLabel.ulID.value!=""){ rvalue += ",FS:ulID=" + document.ListLabel.ulID.value; }
        if(document.ListLabel.ulClass.value!=""){ rvalue += ",FS:ulClass=" + document.ListLabel.ulClass.value; }
    }
    
    if(document.ListLabel.brTF.value=="true")
    {
        rvalue += ",FS:bfStr=" + document.ListLabel.bfstr.value; 
    }
    if(document.ListLabel.isPic.value!=""){ rvalue += ",FS:isPic=" + document.ListLabel.isPic.value; }
    if(document.ListLabel.TitleNumer.value!=""){ rvalue += ",FS:TitleNumer=" + document.ListLabel.TitleNumer.value; }
    if(document.ListLabel.ContentNumber.value!=""){ rvalue += ",FS:ContentNumber=" + document.ListLabel.ContentNumber.value; }
    if(document.ListLabel.NaviNumber.value!=""){ rvalue += ",FS:NaviNumber=" + document.ListLabel.NaviNumber.value; }
    if(document.ListLabel.ShowNavi.value!=""){ rvalue += ",FS:ShowNavi=" + document.ListLabel.ShowNavi.value; }
    if(document.ListLabel.ShowNavi.value=="4")
    { if(document.ListLabel.NaviPic.value!=""){ rvalue += ",FS:NaviPic=" + document.ListLabel.NaviPic.value; } }
    if(document.ListLabel.ShowNavi.value!=""&&document.ListLabel.ShowNavi.value!="4"){ rvalue += ",FS:NaviCSS=" + document.ListLabel.NaviCSS.value; }
    if(document.ListLabel.css1.value!=""&&document.ListLabel.css2.value!=""){ rvalue += ",FS:ColbgCSS=" + document.ListLabel.css1.value+"|"+document.ListLabel.css2.value; }
    if(document.ListLabel.isPage.value=="true")
    { if(document.ListLabel.PageID.value!=""){ rvalue += "," + document.ListLabel.PageID.value; } }
    rvalue += "]";
    rvalue += temproot;
    rvalue += "[/FS:Loop]";
    
    if(CheckStr)
	    parent.getValue(rvalue);
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
       oEditor.InsertHtml('{#FS:define='+value+'}');
    }
    else
    {
    return false;
    }
}
</script>