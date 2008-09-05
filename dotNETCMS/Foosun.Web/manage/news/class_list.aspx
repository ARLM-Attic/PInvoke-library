<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_news_class_list" Codebehind="class_list.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css"
        rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js">
    </script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body onload="InItParentIdList()">
    <form id="server" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px; height: 32px;">
                    栏目管理<span class="helpstyle" style="cursor: hand;" title="点击查看帮助" onclick="Help('H_news__0001',this)">(帮助)</span></td>
                <td width="43%" class="topnavichar" style="padding-left: 14px; height: 32px;">
                    <div align="left">
                        位置导航：<a href="../main.aspx" target="sys_main" class="topnavichar">首页</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" />栏目管理</div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
                <td>
                    &nbsp;&nbsp;<a href="class_list.aspx" class="topnavichar">首页</a>&nbsp;&nbsp;<a href="Class_add.aspx?SiteID=<%Response.Write(Request.QueryString["SiteID"]); %>"
                        class="topnavichar">添加根栏目</a>&nbsp;&nbsp;<a href="news_Page.aspx" class="topnavichar">添加单页面</a>&nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="topnavichar" OnClick="LinkButton1_Click"
                        OnClientClick="{if(confirm('警告：确认此操作吗?\n此操作将对所选择数据复位一级栏目!')){return true;}return false;}">复位</asp:LinkButton>&nbsp;
                    <a href="SortPage.aspx?Acton=unite" class="topnavichar">合并</a>&nbsp; <a href="SortPage.aspx?Acton=allmove"
                        class="topnavichar">转移</a>&nbsp;&nbsp;<asp:LinkButton ID="del_allClass1" runat="server"
                            CssClass="topnavichar" OnClick="del_allClass" OnClientClick="{if(confirm('警告：确认要初始化栏目?\n将删除站点中的所有栏目及内容信息!\n同时将删除所有的静态页面!')){return true;}return false;}">初始化</asp:LinkButton>&nbsp;&nbsp;<span><a
                                href="Class_ToTemplet.aspx" class="topnavichar">属性</a>&nbsp;&nbsp;</span>┇&nbsp;&nbsp;<asp:LinkButton
                                    ID="Lock" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认批量锁定/解锁吗!')){return true;}return false;}"
                                    OnClick="Lock_Click">锁定/解锁</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="AllDel" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所选栏目吗?\n将删除到回收站中.\n如果要恢复，请在[控制面板]-回收站中恢复。')){return true;}return false;}"
                        OnClick="AllDel_Click">删除</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="Selected_del" CssClass="topnavichar" runat="server" OnClick="Selected_del_Click"
                        OnClientClick="{if(confirm('警告：确认删除所选栏目信息吗?\n删除后不可以恢复!')){return true;}return false;}">彻底删除</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="makeXML2" runat="server" CssClass="topnavichar" OnClick="makeXML">生成XML</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="makeHTML2" runat="server" CssClass="topnavichar" OnClick="makeHTML">生成静态文件</asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton
                        ID="ClassIndex" runat="server" CssClass="topnavichar" OnClick="makeClassIndex"><span title="门户版功能">索引</span></asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton
                            ID="clearNewsInfo2" runat="server" CssClass="topnavichar" OnClick="clearNewsInfo"
                            OnClientClick="{if(confirm('警告：确实清空数据吗?\n确定后将清除选顶栏目下的新闻!')){return true;}return false;}">清空</asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton
                                ID="customShow" CssClass="topnavichar" runat="server" OnClick="customShow_Click">显示全部栏目</asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton
                                    ID="treeShow" runat="server" CssClass="topnavichar" OnClick="treeShow_Click">只显示顶级栏目</asp:LinkButton>&nbsp;&nbsp;<span
                                        id="channelList" runat="server" /></td>
            </tr>
        </table>
        <div>
            <asp:Repeater ID="DataList1" runat="server">
                <HeaderTemplate>
                    <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" id="tablist"
                        class="table">
                        <tr class="TR_BG">
                            <td width="7%" align="center" valign="middle" class="sysmain_navi">
                                ID</td>
                            <td width="36%" align="left" valign="middle" class="sysmain_navi">
                                栏目中文[英文]</td>
                            <td width="5%" align="center" valign="middle" class="sysmain_navi">
                                权重</td>
                            <td width="24%" align="center" valign="middle" class="sysmain_navi">
                                属性</td>
                            <td align="center" valign="middle" class="sysmain_navi">
                                操作<input name="Checkboxc" type="checkbox" onclick="javascript:selectAll(this.form,this.checked);" /></td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#((DataRowView)Container.DataItem)["Colum"]%>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <table align="center" border="0" cellpadding="5" cellspacing="0" width="98%">
         <tr>
            <td colspan="10" align="right"><uc1:PageNavigator ID="PageNavigator1" runat="server" /></td>
        </tr>
            <tr>
                <td class="reshow">
                    <b>说明:</b><br />
                    系统：系统目录&nbsp;&nbsp;┇&nbsp;&nbsp;外部：外部栏目&nbsp;&nbsp;┇&nbsp;&nbsp;显示：导航中显示&nbsp;&nbsp;┇
                    &nbsp;&nbsp;隐藏：导航中隐藏&nbsp;&nbsp;┇ &nbsp;&nbsp;域：捆绑了二级域名的目录┇ &nbsp;&nbsp;单页：单页栏目</td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenField_ParentID" runat="server" />
    </form>
    <br />
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
        style="height: 76px">
        <tr>
            <td align="center">
                <%Response.Write(CopyRight); %>
            </td>
        </tr>
    </table>
</body>
</html>

<script language="javascript" type="text/javascript">
//初始化参数
var __manageForderName;
function InItParentIdList()
{
    //得到节点ID
    var listObj = document.getElementById("HiddenField_ParentID").value;
    if(listObj == null || listObj == "")
    {return;}

    //得到管理目录
    var  options={  
	       method:'get',  
	       onComplete:function(transport)
	       {  		
	           __manageForderName = transport.responseText;
		       getForInfo(listObj,0);
		   }
       }; 
    new  Ajax.Request('../../configuration/system/getManageForder.aspx?no-cache='+Math.random(),options);
    
    
}


//递归
function getForInfo(listObj,i)
{
    var IDlist = listObj.split('|');
    if(IDlist[i] == null || IDlist[i] == "")
        return;
    
    var url="/"+__manageForderName+"/news/Class_list_ajax.aspx";
	var Action="ParentId="+IDlist[i] + "&ShowFlag=true";
	var myAjax = new Ajax.Request(
	url,
	{method:"get",
	parameters:Action,
	onComplete:function(obj)
    {
	    $("Parent"+IDlist[i]).style.display="";
	    //
	    var ClassInfo;
	
	    ClassInfo=obj.responseText.split("|||");
	    if(ClassInfo[0] != "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" class=\"table\"></table>")
	    {
	        $("Parent"+ClassInfo[1]).innerHTML=ClassInfo[0];
	    }
	    else
	    {
	        $("Parent"+ClassInfo[1]).innerHTML = "&nbsp&nbsp&nbsp&nbsp&nbsp没有子栏目";
	    }
    		

        var ImgObj = document.getElementById("img_parentid_" + IDlist[i]);
        if(ImgObj != null)
        {
            ImgObj.src=ImgObj.src.replace("b.gif","s.gif");
            ImgObj.alt="点击收起子栏目";
        }
	    //递归调用
	    i++;
	    getForInfo(listObj,i);
    }
	}
	);
}

function orderAction(id,order)
{
	var ReturnValue='';
	ReturnValue=prompt('输入权重(数字越大，排列越靠前)：',order);
	if ((ReturnValue!='') && (ReturnValue!=null))
	{
	    location.href='Class_list.aspx?Type=orderAction&ClassId='+id+'&OrderId='+ReturnValue;
	}
	else
	{
	    if(ReturnValue!=null)
	    {
	        alert('输入权重');
	    }    
	}
}
function getchanelInfo(obj)
{
   var SiteID=obj.value;
   if(SiteID=="")
   {
       window.location.href="class_List.aspx";
   }
   else
   {
       window.location.href="class_List.aspx?SiteID="+SiteID+"";
   }
}

function checkTF(ojb)
{
    if(confirm('您确定要['+ojb+']吗？\n如果您选择的栏目过多，生成将需要花较长时间!'))
    {
        return true;
    }
    else
    {
        return false;
    }
}
function gchclass(classid)
{
    var cid = document.getElementById(classid);
    if(cid.style.display=="")
    {
        cid.style.display="none";
    }
    else
    {
        cid.style.display="";
    }
}

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
		GetSubClass(ParentId,true);
//		if ($("Parent"+ParentId).innerHTML=="" || $("Parent"+ParentId).innerHTML=="&nbsp;&nbsp;loading..."){
//			$("Parent"+ParentId).innerHTML="&nbsp;&nbsp;loading...";
//			
//		}
	}else{
		$("Parent"+ParentId).style.display="none";
		GetSubClass(ParentId,false);
	}
}

function GetSubClass(ParentId,ShowFlag){
//得到管理目录
    var  options={  
	       method:'get',  
	       onComplete:function(transport)
	       {
                var url="/"+transport.responseText+"/news/Class_list_ajax.aspx";
                var Action="ParentId="+ParentId + "&ShowFlag=" + ShowFlag;
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
	
	ClassInfo=OriginalRequest.responseText.split("|||");
	if(ClassInfo[0] != "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" class=\"table\"></table>")
	{
	    $("Parent"+ClassInfo[1]).innerHTML=ClassInfo[0];
	}
	else
	{
	    $("Parent"+ClassInfo[1]).innerHTML = "&nbsp&nbsp&nbsp&nbsp&nbsp没有子栏目";
	}
}

function GetRootClass(){
	GetSubClass("0","");
}


</script>

