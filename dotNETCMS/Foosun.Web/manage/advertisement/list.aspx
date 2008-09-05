<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_advertisement_list"  ResponseEncoding="utf-8" Codebehind="list.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>广告系统</title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
        <tr>
          <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">广告系统</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />广告系统</div></td>
        </tr>
      </table>
      <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center" >
        <tr class="TR_BG_list">
          <td align="left">
            <table style="width:100%" border="0" cellpadding="5" cellspacing="1" class="Navitable">
              <tr>
                <td align="center" onclick="javascript:ChangeDiv('Ads')" style="cursor:pointer;width:55px;" id="TdAds">广告管理</td>
                <td align="center"  onclick="javascript:ChangeDiv('Class')" style="cursor:pointer;width:55px;" id="TdClass">分类管理</td>
                <td align="center"  onclick="javascript:ChangeDiv('Stat')" style="cursor:pointer;width:55px;" id="TdStat">统计管理</td>
                <td align="center" style="cursor:hand;" id="Td1"></td>
               </tr>
            </table>
           </td>
        </tr>
        <tr class="TR_BG_list"><td align="left" colspan="5">
        <div id="List"></div>
        </td></tr>
      </table>      
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
      <tr>
        <td align="center"><label id="copyright" runat="server" /><input type="hidden" name="ads_Type" id="ads_Type" value="" /><input type="hidden" name="Show_Type" id="Show_Type" value="" /><input type="hidden" name="SiteValue" id="SiteValue" value="" /></td>
      </tr>
    </table>

    </form>
</body>
</html>
<script language="javascript" type="text/javascript">

document.form1.Show_Type.value = "";
function AddAdsClass(classid)
{
    self.location="adsclass_add.aspx?ParentID="+classid;
}
function EditAds(type,id)
{
    switch(type)
    {
        case "Ads":
            self.location="ads_edit.aspx?AdsID="+id;
            break;
        case "Class":
            self.location="adsclass_edit.aspx?AdsClassID="+id;
            break;
        default:
            break;
    }
}

function ShowType(type)
{
    document.form1.ads_Type.value = document.form1.adType.value;
    document.form1.Show_Type.value = "showadstype";
    GetList(getCookie("ads_type"),getCookie("ads_page"));
}
function SearchGo()
{
    document.form1.Show_Type.value = "search";
    GetList(getCookie("ads_type"),getCookie("ads_page"));
}

function changeSite(value)
{
    
    document.form1.Show_Type.value = "site";
    document.form1.SiteValue.value = document.form1.Site.value;
    GetList(getCookie("ads_type"),getCookie("ads_page"));
}

function getID()
{
   var idstr = "";
   for(i=0;i<document.form1.length;i++)
    {
	    if(document.form1.elements[i].type=="checkbox")
	    {
	        if(document.form1.elements[i].checked==true)
	        {
	            idstr = idstr + document.form1.elements[i].value + ",";
	        }
	    }
    }
    return idstr;
}
function DelAll(type)
{
    var idstr = getID();
    switch (type)
    {
        case "Ads":
            if(confirm("你确定要删除全部的广告?"))
            {
                self.location="list.aspx?type="+type+"&OpType=adsDelAll";
            }
            break;
        case "Class":
            if(confirm("你确定要删除全部的栏目?\r此操作将会删除全部栏目以及栏目下面的广告!"))
            {
                self.location="list.aspx?type="+type+"&OpType=classDelAll";
            }
            break;
        case "Stat":
            if(confirm("你确定要删除全部统计信息?"))
            {
                self.location="list.aspx?type="+type+"&OpType=statDelAll";
            }
            break;
    }
}
function Del(type)
{
    var idstr = getID();
    switch (type)
    {
        case "Ads":
            if(confirm("你确定要删除选中的广告?"))
            {
                self.location="list.aspx?type="+type+"&OpType=adsDel&ID="+idstr;
            }
            break;
        case "Class":
            if(confirm("你确定要删除选中的栏目?\r此操作将会删除选中的栏目以及选中栏目下面的广告!"))
            {
                self.location="list.aspx?type="+type+"&OpType=classDel&ID="+idstr;
            }
            break;
        case "Stat":
            if(confirm("你确定要删除选中的统计信息?"))
            {
                self.location="list.aspx?type="+type+"&OpType=statDel&ID="+idstr;
            }
            break;
    }
}

function Lock(type,ID)
{
    self.location="list.aspx?type="+type+"&OpType=adsLock&ID="+ID;
}
function UnLock(type,ID)
{
    self.location="list.aspx?type="+type+"&OpType=adsUnLock&ID="+ID;
}
function LookInfo(ID)
{
    self.location="ad_stat.aspx?adsID="+ID;
}
function ChangeDiv(ID)
{
	Selete(ID);
	setCookie("ads_type",ID);
    setCookie("ads_page",0);
	GetList(ID,0);
}
function Selete(ID)
{
	document.getElementById("TdAds").className='list_link';
	document.getElementById("TdClass").className='list_link';
	document.getElementById("TdStat").className='list_link';
	document.getElementById("Td"+ID+"").className='list_link';
}
function GetList(Type,page)
{
	setCookie("ads_type",Type);
    setCookie("ads_page",page);
    var selecttype = "Type="+Type+"&page="+page;
    
    switch (Type)
    {
        case "Ads":
            switch (document.form1.Show_Type.value)
            {
                case "showadstype":
                    selecttype = "Type="+Type+"&page="+page+"&SiteID="+escape(document.form1.SiteValue.value)+"&showadstype="+escape(document.form1.Show_Type.value)+"&adsType="+escape(document.form1.ads_Type.value);
                    break;
                case "search":
                    selecttype = "Type="+Type+"&page="+page+"&showadstype="+escape(document.form1.Show_Type.value)+"&searchType="+escape(document.form1.SearchType.value)+"&SearchKey="+escape(document.form1.SearchKey.value);
                    break;
                case "site":
                    selecttype = "Type="+Type+"&page="+page+"&SiteID="+escape(document.form1.SiteValue.value)+"&showadstype="+escape(document.form1.Show_Type.value)+"&adsType="+escape(document.form1.ads_Type.value);
                    break;
                default:
                    selecttype = "Type="+Type+"&page="+page;
                    break;
            }        
            break;
        case "Class":
            selecttype = "Type="+Type+"&page="+page+"&SiteID="+escape(document.form1.SiteValue.value);
            break;
        case "Stat":
            selecttype = "Type="+Type+"&page="+page+"&SiteID="+escape(document.form1.SiteValue.value);
            break;
        default:
            selecttype = "Type="+Type+"&page="+page;
            break;
    }
    Selete(Type);
	var  options={  
					   method:'get',  
					   parameters:selecttype,  
					   onComplete:function(transport)
						{  
							var returnvalue=transport.responseText;
							if (returnvalue.indexOf("??")>-1)
							    document.getElementById("List").innerHTML="Error";
							else
								document.getElementById("List").innerHTML=returnvalue;
						}  
				   }; 
	new  Ajax.Request('List.aspx?no-cache='+Math.random(),options);
}
if(getCookie("ads_type")!=null && getCookie("ads_type")!="null" && getCookie("ads_type")!="")
{
    GetList(getCookie("ads_type"),getCookie("ads_page"));
}
else
{
    GetList('Ads',0);
}

function getCode(adsID)
{
    var WWidth = (window.screen.width-500)/2;
    var Wheight = (window.screen.height-150)/2;
    //--------------------------------------
    window.open('showJsPath.aspx?adsID='+adsID, '广告JS代码调用', 'height=200, width=400, top='+Wheight+', left='+WWidth+', toolbar=no, menubar=no, scrollbars=no,resizable=yes,location=no, status=no');
}
</script>
