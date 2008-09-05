<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_news_page" Codebehind="news_page.aspx.cs" %>

<%@ Register Src="../../controls/UserPop.ascx" TagName="UserPop" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>

    <script type="text/javascript" src="../../editor/fckeditor.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table id="top1" width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td style="height: 1px;" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 30%; padding-left: 14px" class="sysmain_navi">
                    单页面添加</td>
                <td style="width: 70%;" class="topnavichar">
                    <div align="left">
                        <a href="../main.aspx" class="topnavichar" target="sys_main">首页</a><img alt="" src="../../sysImages/folder/navidot.gif"
                            border="0" /><a href="class_list.aspx" class="topnavichar" target="sys_main">栏目管理</a><span
                                id="naviClassName" runat="server" /><img alt="" src="../../sysImages/folder/navidot.gif"
                                    border="0" />添加单页面</div>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" border="0" cellpadding="3" cellspacing="0" class="table">
            <tr>
                <td class="TR_BG_list" style="width: 100px; text-align: right;">
                    直贴源码
                </td>
                <td class="TR_BG_list">
                    <input type="checkbox" class="form" id="zt" name="zt" onclick="zhitie(this)" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="TR_BG_list" style="width: 100px; text-align: right;">
                    页面标题
                </td>
                <td class="TR_BG_list">
                    <asp:TextBox ID="TCname" Width="300" CssClass="form" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TCname"
                        Display="Dynamic" ErrorMessage="<span class=reshow>(*)名字不能为空!</span>"></asp:RequiredFieldValidator>
                    <asp:CheckBox ID="NaviShowtf" Text="导航中显示" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="TR_BG_list" style="width: 100px; text-align: right;">
                    父栏目
                </td>
                <td class="TR_BG_list">
                    <span style="display: none;">
                        <asp:TextBox ID="TParentId" runat="server" MaxLength="100" Width="80" CssClass="form"></asp:TextBox><img
                            src="../../sysImages/folder/s.gif" alt="选择栏目" border="0" style="cursor: pointer;"
                            onclick="selectFile('newsclass',document.form1.TParentId,250,500);document.form1.TParentId.focus();" /></span><span
                                id="ClassCnamev" class="reshow"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TParentId"
                        Display="Dynamic" ErrorMessage="<span class=reshow>(*)选择栏目!如果为根栏目，请填写0</span>"></asp:RequiredFieldValidator>
                    &nbsp;权重&nbsp;<asp:TextBox ID="TOrder" runat="server" Width="93" CssClass="form"></asp:TextBox>
                    <span class="helpstyle" style="cursor: hand;" title="点击查看帮助" onclick="Help('Class_Aspx_05',this)">
                        帮助</span>
                </td>
            </tr>
            <tr id="rows_key" runat="server">
                <td class="TR_BG_list" style="width: 100px; text-align: right;">
                    meta关键字
                </td>
                <td class="TR_BG_list">
                    <asp:TextBox ID="KeyMeata" TextMode="MultiLine" Height="50" CssClass="form" Width="500"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="rows_meta" runat="server">
                <td class="TR_BG_list" style="width: 100px; text-align: right;">
                    meta描述
                </td>
                <td class="TR_BG_list">
                    <asp:TextBox ID="BeWrite" TextMode="MultiLine" Height="50" CssClass="form" Width="500"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="TR_BG_list" id="div_Templet" runat="server">
                <td style="width: 10%; text-align: right;">
                    模板</td>
                <td style="width: 90%;">
                    <asp:TextBox ID="FProjTemplets" runat="server" MaxLength="200" Width="40%" CssClass="form"></asp:TextBox><img
                        src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor: pointer;"
                        onclick="selectFile('templet',document.form1.FProjTemplets,250,500);document.form1.FProjTemplets.focus();" />
                    <span class="helpstyle" style="cursor: help;" title="点击显示帮助" onclick="Help('H_page_add_Templet',this)">
                        帮助</span>
                </td>
            </tr>
            <tr class="TR_BG_list" id="Tr1">
                <td style="width: 10%; text-align: right;">
                    路径及文件名</td>
                <td style="width: 90%;">
                    <asp:TextBox ID="TPath" runat="server" MaxLength="200" Width="40%" CssClass="form"></asp:TextBox>
                    <span class="helpstyle" style="cursor: help;" title="点击显示帮助" onclick="Help('H_page_add_path',this)">
                        帮助</span>
                </td>
            </tr>
                     <%--时间：2008-07-17 修改者：吴静岚
             添加分页功能步骤1 开始
      --%>
            <tr class="TR_BG_list"  id="tr_autoPageSplit">
            <td style="width: 10%; text-align: right">
                分页设置</td>
            <td style="width: 90%">
                &nbsp;&nbsp; 插入分页符：<span style="cursor: pointer; color: red">分页标题</span>
                <asp:TextBox ID="PageTitle" runat="server" Text="" Width="200px"></asp:TextBox>
                <a href="###" onclick="insertPageStr();">插入</a> &nbsp;
                <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="自动分页" />
                每页字数：<asp:TextBox ID="TxtPageCount" runat="server" Height="11px"
                    Width="28px">20</asp:TextBox>
                    </td>
        </tr>
                <%--
             添加分页功能步骤1 结束
      --%>
            <tr>
                <td class="TR_BG_list" style="width: 100px; text-align: right;">
                    <%--时间：2008-07-04 修改者：吴静岚
             添加选择图片以及上传图片功能步骤1 开始
      --%>
                    <label id="picContentTF">
                    </label>
                    <div style="padding-bottom: 3px;">
                        <a style="cursor: pointer;" onclick="UpFile('<% Response.Write(UDir); %>');" title="在上传的时候，请在编辑区鼠标点击，设置要上传图片的位置。">
                            <font color="red">上传图片</font></a></div>
                    <div>
                        <a style="cursor: pointer;" onclick="selectFile('picEdit',document.getElementById('picContentTF'),320,500);"
                            title="在上传的时候，请在编辑区鼠标点击，设置要上传图片的位置。"><font color="blue">选择图片</font></a></div>
                    <br/>
                    <%--
             添加选择图片以及上传图片功能步骤2 结束
      --%>
                    <label id="contentTag" runat="server">
                        内容</label>
                </td>
                <td class="TR_BG_list">
                    <div id="editorcontent" runat="server">

                        <script type="text/javascript" language="JavaScript">
			window.onload = function()
				{
				var sBasePath = "../../editor/"
                var oFCKeditor = new FCKeditor('Content') ;
                oFCKeditor.BasePath	= sBasePath ;
                oFCKeditor.Width = '100%' ;
                oFCKeditor.Height = '400px' ;	
                oFCKeditor.ReplaceTextarea() ;
                }
                        </script>

                        <textarea name="Content" rows="1" cols="1" style="display: none" id="Content" runat="server">			</textarea>
                    </div>
                    <div id="textcontent" style="display: none;" runat="server">
                        <asp:TextBox ID="tContent" TextMode="MultiLine" Height="250" CssClass="form" Width="500"
                            runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr class="TR_BG_list" id="div_UserPop1" style="display: none;">
                <td style="text-align: right;">
                    浏览权限</td>
                <td>
                    <uc1:UserPop ID="UserPop1" runat="server" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" border="0" cellpadding="3" cellspacing="0" class="table">
            <tr>
                <td style="text-align: center;" class="TR_BG_list">
                    <asp:HiddenField ID="gClassID" runat="server" />
                    <asp:HiddenField ID="acc" runat="server" />
                    <asp:Button ID="Button1" runat="server" CssClass="form" Text="保存单页面" OnClick="Buttonsave_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
            style="height: 76px">
            <tr>
                <td align="center">
                    <div runat="server" id="SiteCopyRight" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
new Form.Element.Observer($('TParentId'),1,getClassCName());

    function getClassCName()
    {
        var strC=document.getElementById("TParentId").value;
	    var  options={  
				       method:'get',  
				       parameters:"Type=Class&add=1&ClassID="+strC,  
				       onComplete:function(transport)
					    {  
						    var returnvalue=transport.responseText;
						    if (returnvalue.indexOf("??")>-1)
                               $('ClassCnamev').innerHTML="error!";
						    else
                               $('ClassCnamev').innerHTML=returnvalue;
					    }  
				       }; 
	    new  Ajax.Request('../../configuration/system/getClassCname.aspx?no-cache='+Math.random(),options);
    } 

function zhitie(obj)
{
    if(obj.checked)
    {
        document.getElementById("rows_key").style.display="none";
        document.getElementById("rows_meta").style.display="none";
        document.getElementById("div_Templet").style.display="none";
        document.getElementById("editorcontent").style.display="none";
        document.getElementById("textcontent").style.display="block";
        document.getElementById("contentTag").innerHTML="网页源码";
        document.getElementById("tr_autoPageSplit").style.display="none";
    }
    else
    {
        document.getElementById("rows_key").style.display="block";
        document.getElementById("rows_meta").style.display="block";
        document.getElementById("div_Templet").style.display="block";
        document.getElementById("editorcontent").style.display="block";
        document.getElementById("textcontent").style.display="none";
        document.getElementById("contentTag").innerHTML="内容";
        document.getElementById("tr_autoPageSplit").style.display="";
    }
}

/*<--时间：2008-07-04 修改者：吴静岚
       添加选择图片以及上传图片功能步骤2 开始
   */
  function UpFile(path)
    {
        var WWidth = (window.screen.width-500)/2;
        var Wheight = (window.screen.height-150)/2;
        window.open("../../configuration/system/Upload.aspx?Path="+path+"&upfiletype=files", '文件上传', 'height=200, width=500, top='+Wheight+', left='+WWidth+', toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); 
    }
    
    function insertHTMLEdit(url)
    {
        var urls = url.replace('{@dirfile}','<% Response.Write(Foosun.Config.UIConfig.dirFile); %>')
        var oEditor = FCKeditorAPI.GetInstance("Content");
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
   //修改者：吴静岚.  步骤2  结束-->
   
   
   //时间：2008-07-17 修改者：吴静岚  添加分页功能步骤2 开始
    function insertPageStr()
    {
	   var str=document.getElementById("PageTitle");
	   if(str.value!="")
	   {
            var oEditor = FCKeditorAPI.GetInstance("Content");
            if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
            {
               oEditor.InsertHtml('[FS:PAGE='+str.value+'$]');
            }
            else
            {
                return false;
            }
        }
        else
        {
            var oEditor = FCKeditorAPI.GetInstance("Content");
            if (oEditor.EditMode==FCK_EDITMODE_WYSIWYG)
            {
               oEditor.InsertHtml('[FS:PAGE]');
            }
            else
            {
                return false;
            }
        }
	}
	//时间：2008-07-17 修改者：吴静岚 添加分页功能步骤2 结束-->
</script>

