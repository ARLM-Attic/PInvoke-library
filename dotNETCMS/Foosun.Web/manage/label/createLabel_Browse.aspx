<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_createLabel_Browse" Codebehind="createLabel_Browse.aspx.cs" %>
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
            <td align="right" class="navi_link" style="width: 15%">引用样式</td>
            <td align="left" class="navi_link">
            <asp:DropDownList ID="Root" runat="server" CssClass="form" onchange="javascript:selectRoot(this.value);">
                <asp:ListItem Value="true">固定样式</asp:ListItem>
                <asp:ListItem Value="false">自定义样式</asp:ListItem>
              </asp:DropDownList>
              <label id="TrStyleID">
                引用样式&nbsp;<asp:TextBox ID="StyleID" runat="server" CssClass="form" Width="120px" ReadOnly="true"></asp:TextBox>
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择样式"  onclick="selectFile('style',document.ListLabel.StyleID,300,420);document.ListLabel.StyleID.focus();" /><span id="sapnStyleID"></span>
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
        </td>
          </tr>

          <tr class="TR_BG_list">
            <td colspan="2" align="center" class="navi_link">&nbsp;<input class="form" type="button" value=" 确 定 "  onclick="javascript:ReturnDivValue();" />&nbsp;<input class="form" type="button" value=" 关 闭 "  onclick="javascript:CloseDiv();" /></td>
          </tr> 

        </table>
     </form>
</body>
</html>
<script language="javascript" type="text/javascript">
function selectRoot(type)
{
    if(type=="false")
        { document.getElementById("TrStyleID").style.display="none";document.getElementById("TrUserDefined").style.display="";  }
    else
        { document.getElementById("TrStyleID").style.display="";document.getElementById("TrUserDefined").style.display="none";  }
}
function ReturnDivValue()
{
    var CheckStr=true;
    if(document.ListLabel.Root.value=="true")
    {
       if(checkIsNull(document.ListLabel.StyleID,document.getElementById("sapnStyleID"),"请选择样式"))
        CheckStr=false;
    }
    var rvalue="[FS:unLoop,FS:SiteID=<%Response.Write(APIID); %>,FS:LabelType=ReadNews";
    if(document.ListLabel.Root.value=="true")
        { temproot = "[#FS:StyleID=" + document.ListLabel.StyleID.value+"]"; }
    else
        {
            var oEditor = FCKeditorAPI.GetInstance("UserDefined");
            temproot = oEditor.GetXHTML(true);
        }
    rvalue += "]";
    rvalue += temproot;
    rvalue += "[/FS:unLoop]";
    
    if(CheckStr)
	    parent.getValue(rvalue);
}
function CloseDiv()
{
    parent.document.getElementById("LabelDivid").style.display="none";
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
