<%@ Page Language="C#" AutoEventWireup="true" Inherits="configuration_system_selectLabelStyle" Codebehind="selectLabelStyle.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>选择栏目</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<style  type="text/css">
.LableSelectItem {
	background-color:highlight;
	cursor: hand;
	color: white;
	text-decoration: none;
}
.LableItem {
	cursor: hand;
}
.SubItems {
	margin-left: 12px;
}
.RootItem {
	margin-left:15px;
}
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
</style>
</head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>

<body ondragstart="return false;" onselectstart="return false;">
    <form name="form1">
    样式：<input type="text" id="styleID" readonly name="styleID" style="width:50%" />&nbsp;<input type="button" class="form" name="Submit" value="选择样式" onclick="ReturnValue(document.form1.styleID.value);" />
    <div id="styleList" runat="server" class="RootItem">
    样式加载中...
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
var SelectClass="";
<!--
function SwitchImg(ImgObj,ParentId){
	var ImgSrc="",SubImgSrc;
	ImgSrc=ImgObj.src;
	SubImgSrc=ImgSrc.substr(ImgSrc.length-5,12);
	if (SubImgSrc=="b.gif"){
		ImgObj.src=ImgObj.src.replace(SubImgSrc,"s.gif");
		ImgObj.alt="点击收起子样式";
	}else{
		if (SubImgSrc=="s.gif"){
			ImgObj.src=ImgObj.src.replace(SubImgSrc,"b.gif");
			ImgObj.alt="点击展开子样式";
		}else{
			return false;
		}
	}
}
function ListGo(Path,ParentPath)
{
	document.Templetslist.Path.value=Path;
	document.Templetslist.ParentPath.value=ParentPath;
	document.Templetslist.submit();
}
function sFiles(obj)
{
  document.form1.styleID.value=obj;
}

function ReturnValue(obj)
{
	var Str=obj;
	parent.ReturnFun(Str);
}
function SelectLable(Obj)
{
	var SelectedInfo="";
	if (SelectClass!=""){
		SelectedInfo=SelectClass.split("***");
		$(SelectedInfo[0]).className='LableItem';
	}
	Obj.className='LableSelectItem';
	SelectClass=Obj.id+"***"+Obj.innerText;
}

function opencat(cat)
{
  if(cat.style.display=="none"){
     cat.style.display="";
  } else {
     cat.style.display="none";
  }
}

function getReview1(id)
{
    if(document.getElementById(id).style.display=="")
    {
       document.getElementById(id).style.display="none";
    }   
    else
    {
       document.getElementById(id).style.display="";
    }
}
</script>
