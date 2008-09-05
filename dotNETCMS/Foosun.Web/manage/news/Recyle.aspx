<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_Recyle" ResponseEncoding="utf-8" Codebehind="Recyle.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="RecList" runat="server" method="post">
  <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">回收站</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />回收站</div></td>
    </tr>
  </table>
  <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr class="TR_BG_list">
      <td align="left">
      <table style="width:100%;" border="0" cellpadding="0" cellspacing="0" class="topnavitable">
          <tr>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('NCList')"  id="TdNCList">新闻栏目</td>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('NList')" id="TdNList">新闻</td>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('CList')" id="TdCList">频道</td>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('SList')" id="TdSList">专题</td>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('LCList')" id="TdLCList">标签栏目</td>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('LList')" id="TdLList">标签</td>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('StCList')" id="TdStCList">样式栏目</td>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('StList')"  id="TdStList">样式</td>
            <td style="height:30px;width:7%;cursor:pointer;" align="center" onclick="javascript:ChangeDiv('PSFList')" id="TdPSFList">PSF</td>
            <td style="height:30px;width:7%;cursor:pointer;"  align="center" onclick="javascript:ChangeDiv('APIList');" id="TdAPIList">API</td>
            <td></td>
          </tr>
        </table></td>
    </tr>
    <tr class="TR_BG_list">
    <td align="left" colspan="9" style="background-color:White;">
    <div id="List">
    </div>
    </td></tr>
  </table>
<br />
<br />
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
  <tr>
    <td align="center"><label id="copyright" runat="server" /></td>
  </tr>
</table>
</form>
</body>
</html>
<script language="javascript" type="text/javascript">
function ChangeDiv(ID)
{
	Selete(ID);
	setCookie("type",ID);
    setCookie("page",0);
	GetList(ID,0);
}

function Selete(ID)
{
	document.getElementById("TdNCList").className='';
	document.getElementById("TdNList").className='';
	document.getElementById("TdCList").className='';
	document.getElementById("TdSList").className='';
	document.getElementById("TdLList").className='';
	document.getElementById("TdLCList").className='';
	document.getElementById("TdStCList").className='';
	document.getElementById("TdStList").className='';
	document.getElementById("TdPSFList").className='';
	document.getElementById("TdAPIList").className='';
	document.getElementById("Td"+""+ID+"").className='reshow';
}

function RAll(Type)//全部恢复
{
    switch (Type)
    {
        case "NCList":
            if(confirm('你确认恢复全部的新闻栏目吗?')){getresult(Type,"RAll","");}
            break;
        case "NList":
            if(confirm('你确认恢复全部的新闻吗?')){getresult(Type,"RAll","");}
            break;
        case "CList":
            if(confirm('你确认恢复全部的频道吗?')){getresult(Type,"RAll","");}
            break;
        case "SList":
            if(confirm('你确认恢复全部的专题吗?')){getresult(Type,"RAll","");}
            break;
        case "LList":
            if(confirm('你确认恢复全部的标签吗?')){getresult(Type,"RAll","");}
            break;
        case "LCList":
            if(confirm('你确认恢复全部的标签栏目吗?')){getresult(Type,"RAll","");}
            break;
        case "StCList":
            if(confirm('你确认恢复全部的样式栏目吗?')){getresult(Type,"RAll","");}
            break;
        case "StList":
            if(confirm('你确认恢复全部的样式吗?')){getresult(Type,"RAll","");}
            break;
        case "PSFList":
            if(confirm('你确认恢复全部的PSF(结点)吗?')){getresult(Type,"RAll","");}
            break;    
    }
}
function DAll(Type)//全部删除
{
    switch (Type)
    {
        case "NCList":
            if(confirm('你确认删除回收站中的新闻栏目吗?\r此操作将彻底删除回收站中所有的栏目以及与这些栏目有关的新闻以及评论!')){getresult(Type,"DAll","");}
            break;
        case "NList":
            if(confirm('你确认删除回收站中的新闻吗?\r此操作将彻底删除回收站中所有的新闻以及与这些新闻相关的评论!')){getresult(Type,"DAll","");}
            break;
        case "CList":
            if(confirm('你确认删除回收站中的频道吗?\r此操作将彻底删除回收站中所有的新闻栏目,专题,新闻以及与这些新闻相关的评论!')){getresult(Type,"DAll","");}
            break;
        case "SList":
            if(confirm('你确认删除回收站中的专题吗?\r此操作将彻底删除回收站中所有的专题!')){getresult(Type,"DAll","");}
            break;
        case "LList":
            if(confirm('你确认删除回收站中的标签吗?\r此操作将彻底删除回收站中所有的标签!')){getresult(Type,"DAll","");}
            break;
        case "LCList":
            if(confirm('你确认删除回收站中的标签栏目吗?\r此操作将彻底删除回收站中所有的标签栏目以及这些栏目下面有关的标签!')){getresult(Type,"DAll","");}
            break;
        case "StCList":
            if(confirm('你确认删除回收站中的样式栏目吗?\r此操作将彻底删除回收站中所有的样式栏目以及这些栏目下面有关的样式!')){getresult(Type,"DAll","");}
            break;
        case "StList":
            if(confirm('你确认删除回收站中的样式吗?\r此操作将彻底删除回收站中所有的样式!')){getresult(Type,"DAll","");}
            break;
        case "PSFList":
            if(confirm('你确认删除回收站中的PSF(结点)吗?\r此操作将彻底删除回收站中所有的PSF(结点)!')){getresult(Type,"DAll","");}
            break; 
    }   
}
function PR(Type)//批量恢复
{
    var tempID="";
    for(i=0;i<document.RecList.length;i++)
    {
	    if(document.RecList.elements[i].type=="checkbox" && document.RecList.elements[i].checked==true)
	    {
		    tempID = tempID + document.RecList.elements[i].value + ",";
	    }
    }
    switch (Type)
    {
        case "NCList":
            if(confirm('你确认批量恢复选中的新闻栏目吗?')){getresult(Type,"PR",tempID);}
            break;
        case "NList":
            if(confirm('你确认批量恢复选中的新闻吗?')){getresult(Type,"PR",tempID);}
            break;
        case "CList":
            if(confirm('你确认批量恢复选中的频道吗?')){getresult(Type,"PR",tempID);}
            break;
        case "SList":
            if(confirm('你确认批量恢复选中的专题吗?')){getresult(Type,"PR",tempID);}
            break;
        case "LList":
            if(confirm('你确认批量恢复选中的标签吗?')){getresult(Type,"PR",tempID);}
            break;
        case "LCList":
            if(confirm('你确认批量恢复选中的标签栏目吗?')){getresult(Type,"PR",tempID);}
            break;
        case "StCList":
            if(confirm('你确认批量恢复选中的栏目样式吗?')){getresult(Type,"PR",tempID);}
            break;
        case "StList":
            if(confirm('你确认批量恢复选中的样式吗?')){getresult(Type,"PR",tempID);}
            break;
        case "PSFList":
            if(confirm('你确认批量恢复选中的PSF(结点)吗?')){getresult(Type,"PR",tempID);}
            break;    
    }    
}
function PD(Type)//批量删除
{
    var tempID="";
    for(i=0;i<document.RecList.length;i++)
    {
	    if(document.RecList.elements[i].type=="checkbox" && document.RecList.elements[i].checked==true)
	    {
		    tempID = tempID + document.RecList.elements[i].value + ",";
	    }
    }
    switch (Type)
    {
        case "NCList":
            if(confirm('你确认批量删除选中的新闻栏目吗?\r此操作将彻底删除选中的栏目以及跟选中栏目有关的新闻,以及评论!')){getresult(Type,"PD",tempID);}
            break;
        case "NList":
            if(confirm('你确认批量删除选中的新闻吗?')){getresult(Type,"PD",tempID);}
            break;
        case "CList":
            if(confirm('你确认批量删除选中的频道吗?')){getresult(Type,"PD",tempID);}
            break;
        case "SList":
            if(confirm('你确认批量删除选中的专题吗?')){getresult(Type,"PD",tempID);}
            break;
        case "LList":
            if(confirm('你确认批量删除选中的标签吗?')){getresult(Type,"PD",tempID);}
            break;
        case "LCList":
            if(confirm('你确认批量删除选中的标签栏目吗?\r此操作将彻底删除选中栏目以及跟选中栏目有关的标签!')){getresult(Type,"PD",tempID);}
            break;
        case "StCList":
            if(confirm('你确认批量删除选中的样式栏目吗?\r此操作将彻底删除选中栏目以及跟选中栏目有关的样式!')){getresult(Type,"PD",tempID);}
            break;
        case "StList":
            if(confirm('你确认批量删除选中的样式吗?')){getresult(Type,"PD",tempID);}
            break;
        case "PSFList":
            if(confirm('你确认批量删除选中的PSF(结点)吗?')){getresult(Type,"PD",tempID);}
            break;    
    }        
}

function getresult(Type,Op,idlist)
{
    switch(Type)
    {
        case "NList":
            document.RecList.action="Recyle.aspx?className="+escape(document.RecList.className.value)+"&Type="+Type+"&Op="+Op+"&idlist="+idlist;
            break;
        case "StList":
            document.RecList.action="Recyle.aspx?className="+escape(document.RecList.className.value)+"&Type="+Type+"&Op="+Op+"&idlist="+idlist;
            break;
        case "LList":
            document.RecList.action="Recyle.aspx?className="+escape(document.RecList.className.value)+"&Type="+Type+"&Op="+Op+"&idlist="+idlist;
            break;
        default:
            document.RecList.action="Recyle.aspx?Type="+Type+"&Op="+Op+"&idlist="+idlist;
            break;
    }
    document.RecList.submit();
}
function GetList(Type,page)
{
	setCookie("type",Type);
    setCookie("page",page);
    Selete(Type);
	var  options={  
					   method:'get',  
					   parameters:"Type="+Type+"&page="+page,  
					   onComplete:function(transport)
						{  
							var returnvalue=transport.responseText;
							if (returnvalue.indexOf("??")>-1)
							    document.getElementById("List").innerHTML="Error";
							else
								document.getElementById("List").innerHTML=returnvalue;
						}  
				   }; 
	new  Ajax.Request('Recyle.aspx?no-cache='+Math.random(),options);
}
if(getCookie("type")!=null && getCookie("type")!="null" && getCookie("type")!="")
{
    GetList(getCookie("type"),getCookie("page"));
}
else
{
    GetList('NCList',0);
}
</script>