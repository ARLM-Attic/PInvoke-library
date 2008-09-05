<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_news_class_site"  Codebehind="class_site.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript">
if(self==top)
{self.location.href='../index.aspx';}
</script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.LableSelectItem {
	background-color:highlight;
	cursor: hand;
	color: white;
	text-decoration: none;
}
.LableItem {
	cursor: hand;
}
.SubItem {
	margin-left:2px;
	background-image: url(../../normal/s_1.gif);
}
.RootItem {
	margin-left:2px;
	}
.bodys
{
	line-height:25px;
}
</style>
</head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<body ondragstart="return false;" onselectstart="return false;">
<table width="165" height="29" border="0" cellpadding="0" cellspacing="0" class="menuq">
  <tr>
    <td width="26" rowspan="2" align="center"><img src="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot.gif" width="8" height="11" border="0"  alt="展开/隐藏"  id="arrow_1"/></td>
    <td height="2"></td>
    <td width="50" rowspan="2" align="center"></td>
  </tr>
  <tr>
    <td width="101" align="left" onClick="show_hide('profile_1', 'arrow_1')" style="cursor:pointer;" class="Lion_menu_2">快捷导航</td>
  </tr>
</table>
<div id="profile_1" style="display:none;">
<div id="shortcut_id" runat="server" />
</div>
<table style="width:165px;height:29px;" border="0" cellpadding="0" cellspacing="0" class="menuq">
  <tr>
    <td style="width:26px;" rowspan="2" align="center"><img src="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot1.gif" width="8" height="11" border="0" alt="展开/隐藏" id="arrow_2" /></td>
    <td style="height:2px;"></td>
    <td style="width:50px;" rowspan="2" align="center"></td>
  </tr>
  <tr>
    <td style="width:101px;cursor:pointer;" align="left" onClick="show_hide('profile_2', 'arrow_2')" class="Lion_menu_2">功能导航</td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="1" cellpadding="1">
  <tr>
    <td style="padding-left:0px;"><img src="../../sysImages/folder/navidot.gif" border="0" /><a href="news_list.aspx" target="sys_main" class="menulist" title="点击查看所有的新闻">全部新闻</a><img src="../../sysImages/folder/navidot.gif" border="0" /><a href="news_stat.aspx" target="sys_main" class="menulist" title="统计编辑的稿件">统筹</a>&nbsp;<label id="returnmenu" runat="server" /></td>
  </tr>
</table>
<div id="profile_2" style="width:100%;"><div style="width:100%;" id="Parent0" class="RootItem">&nbsp;&nbsp;loading...</div>
    </div>
    <input id="Button1" type="button" value="点此刷新数据" language="javascript" onclick="return Button1_onclick()" style="height: 68px; background-color: transparent; border-top-style: none; border-right-style: none; border-left-style: none; text-decoration: underline; border-bottom-style: none; width: 97px;" />

  
</body>
</html>
<script language="javascript" type="text/javascript">
var SelectClass="";
function SwitchImg(ImgObj,ParentId){
	var ImgSrc="",SubImgSrc;
	ImgSrc=ImgObj.src;
	SubImgSrc=ImgSrc.substr(ImgSrc.length-5,12);
	if (SubImgSrc=="b.gif"){
		ImgObj.src=ImgObj.src.replace(SubImgSrc,"s.gif");
		ImgObj.alt="点击收起子栏目";
		SwitchSub(ParentId,true);
	}else{
		if (SubImgSrc=="s.gif"){
			ImgObj.src=ImgObj.src.replace(SubImgSrc,"b.gif");
			ImgObj.alt="点击展开子栏目";
			SwitchSub(ParentId,false);
		}else{
			return false;
		}
	}
}
function SwitchSub(ParentId,ShowFlag)
{
	if (ShowFlag==true){
		$("Parent"+ParentId).style.display="";
		if ($("Parent"+ParentId).innerHTML=="" || $("Parent"+ParentId).innerHTML=="&nbsp;&nbsp;loading..."){
			$("Parent"+ParentId).innerHTML="&nbsp;&nbsp;loading...";
			GetSubClass(ParentId);
		}
	}else{
		$("Parent"+ParentId).style.display="none";
	}
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
function GetRootClass(){
	GetSubClass("0");
}

function GetSubClass(ParentId){

    //得到管理目录
    var  options={  
	       method:'get',  
	       onComplete:function(transport)
	       {  		       
		       var url="/"+ transport.responseText +"/news/class_site_ajax.aspx";
	            var Action="ParentId="+ParentId;
	            var myAjax = new Ajax.Request(
		            url,
		            {method:"get",
		            parameters:Action,
		            onComplete:GetSubClassOk
		            }
		            );
	                   }
       }; 
    new  Ajax.Request('../../configuration/system/getManageForder.aspx?no-cache='+Math.random(),options);
}

function GetSubClassOk(OriginalRequest){
	var ClassInfo;
	//alert(OriginalRequest.responseText);
	if (OriginalRequest.responseText!="" && OriginalRequest.responseText.indexOf("|||")>-1){
		ClassInfo=OriginalRequest.responseText.split("|||");
		
		if (ClassInfo[0]=="<div id=\"newsList\">Succee"){
			$("Parent"+ClassInfo[1]).innerHTML=ClassInfo[2];
		}else{
			$("Parent"+ClassInfo[1]).innerHTML="<a class=\"list_link\" title=\"点击重试\" href=\"点击重试\" onclick=\"$('Parent"+ClassInfo[1]+"').innerHTML='栏目加载中...';GetSubClass('"+ClassInfo[1]+"');return false;\">none</a>";
		}
	}else{
		alert("读取栏目错误,可能没有栏目。");
		return false;
	}
}



window.onload=GetRootClass;

 function show_hide(DivID,ImgID)
 {
    if (document.getElementById(DivID).style.display=='')
    {
        document.getElementById(DivID).style.display='none';
        document.getElementById(ImgID).src='../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot.gif';
    }
    else
    {
        document.getElementById(DivID).style.display='';
        document.getElementById(ImgID).src='../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_dot1.gif';
    }
}

function hsite(siteid)
{
    if(siteid!="")
    {
        if(document.getElementById(siteid).style.display=="none")
        {
              document.getElementById(siteid).style.display="";
        }
        else
        {
              document.getElementById(siteid).style.display="none";
         }
    }
}
function Button1_onclick() {
    document.location.reload();
}

</script>