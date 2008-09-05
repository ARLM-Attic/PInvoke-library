<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="manage_news_News_list" Codebehind="News_list.aspx.cs" %>
<%@ Import NameSpace="System.Data"%>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.divstyle {
	overflow:hidden;
	position:absolute;
	left:80px;
	top:62px;
	background:#FFFFE1 repeat-x left top;
	border:1px double #4F4F4F;
	width:88%;
	text-align:left;
	padding-left:8px;
	padding-top:8px;
	padding-bottom:12px;
	padding-right:8px;
	clip:rect(auto, auto, auto, auto);
	z-index:50;
	filter: progid:DXImageTransform.Microsoft.DropShadow(color=#B6B6B6,offX=2,offY=2,positives=true);

</style>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
<!--
var trold = null;
var classStr = "Foosun<%Response.Write(Request.QueryString["ClassID"]); %>"
function GetAllChecked()
{
    var retstr = "";
    var tb = document.getElementById("tablist");
    var j = 0;
    for(var i=1;i<tb.rows.length;i++)
    {
        var objtr = tb.rows[i];
        if(objtr.cells.length < 6)
            continue;
        var objtd = objtr.cells[5];
        for(var k=0;k<objtd.childNodes.length;k++)
        {
            var objnd = objtd.childNodes[k];
            if(objnd.type == "checkbox")
            {
                if(objnd.checked)
                {
                    if(j>0)
                        retstr += ",";
                    retstr += objnd.value;
                    j++;
                }
                break;
            }
        }
    }
    return retstr;
}

//获取当前栏目下所有新闻的id
function GetAllNews()
{
    var retstr = "";
    var tb = document.getElementById("tablist");
    var j = 0;
    for(var i=1;i<tb.rows.length;i++)
    {
        var objtr = tb.rows[i];
        if(objtr.cells.length < 6)
            continue;
        var objtd = objtr.cells[5];
        for(var k=0;k<objtd.childNodes.length;k++)
        {
            var objnd = objtd.childNodes[k];
            if(objnd.type == "checkbox")
            {
                  if(j>0)
                  retstr += ",";
                  retstr += objnd.value;
                  j++;
            }
        }
    }
    return retstr;
}

function SetTop(id)
{
    if(confirm('确定要所选中的新闻固顶吗?'))
    {
        //var tb = objtb.options[objtb.selectedIndex].value;
        var param = "Option=SetTop&NewsID="+ id;
        var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {
		   var retv=transport.responseText;
		   OnSetTop(retv);
		} 
	  }
	new  Ajax.Request('News_List.aspx',options);  
    }
}

function UnSetTop(id)
{
    if(confirm('确定要所选中的新闻解固吗?'))
    {
        //var tb = objtb.options[objtb.selectedIndex].value;
        var param = "Option=UnSetTop&NewsID="+ id;
        var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {
		   var retv=transport.responseText;
		   OnSetTop(retv);
		} 
	  }
	new  Ajax.Request('News_List.aspx',options);  
    }
}

function setLock(id)
{
    var l = id;
    if(l == "")
    {
        alert("您没有选择要锁定的新闻!");
        return;
    }
    if(confirm('确定要锁定您所选择的新闻吗?'))
    {
        SendAjax("LockNews",l);
    }
}

function setUNLock(id)
{
    var l = id;
    if(l == "")
    {
        alert("您没有选择要取消锁定的新闻!");
        return;
    }
    if(confirm('确定要取消锁定您所选择的新闻吗?'))
    {
        SendAjax("UNLockNews",l);
    }
}


function CheckStat(id)
{
    var l;
    if(id < 0)
    {
        l = GetAllChecked();
        if(l == "")
        {
            alert("您没有选择要审核的新闻!");
            return;
        }
    }
    else
    {
        l = id;
    }
    if(confirm('确定要审核您所选择的新闻吗'))
    {
        SendAjax("CheckStatNews",l);
    }
}


function OnSetTop(ret)
{
    alert(ret);
    __doPostBack('PageNavigator1$LnkBtnGoto','');
}

function Recycle(id)
{
    var l;
    if(id < 0)
    {
        l = GetAllChecked();
        if(l == "")
        {
            alert("您没有选择要删除的新闻!");
            return;
        }
    }
    else
    {
        l = id;
    }
    if(confirm('确定要删除您所选择的新闻吗?该新闻将被放入回收站中。'))
    {
        SendAjax("RecyleNews",l);
    }
}

function ToOld()
{
debugger;

   var cl = GetAllChecked();
   if(classStr=="Foosun")
   {
     if(cl == "")
     {
        alert("您没有选择要归档的新闻!");
        return;
     }
     else
     {
        if(confirm('确认归档吗？\n将归档您选择的新闻\n警告：此操作不可逆。\n如果您非要此操作。请按 [确定]按钮'))
        {
            SendAjax("ToOldNews",cl);
        }
     }
   }
   else
   {
        if(confirm('确认归档吗？\n将归档您选择的新闻\n如果您没选择新闻，将归档此类栏目符合条件的新闻。\n警告：此操作不可逆。\n如果您非要此操作。请按 [确定]按钮'))
        {
             if(cl == "")
             {
                 SendAjax("ToOldNewsClass","<%Response.Write(Request.QueryString["ClassID"]); %>");
             }
             else
             {
                 SendAjax("ToOldNews",cl);
             }
         }
   }
}

function Delete(id)
{
    var l;
    if(id < 0)
    {
        l = GetAllChecked();
        if(l == "")
        {
            alert("您没有选择要删除的新闻!");
            return;
        }
    }
    else
    {
        l = id;
    }
    if(confirm('确定要永久删除您所选择的新闻吗?该新闻将不能被恢复!'))
    {
        SendAjax("DeleteNews",l);
    }
}

function SendAjax(op,id)
{
    var HiddenSpecialID = document.getElementById("HiddenSpecialID").value;
    var param = "Option="+ op +"&NewsID="+ id + "&HiddenSpecialID=" + HiddenSpecialID;
    var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {
		   var retv=transport.responseText;
		  onRcvMsg(retv);
		} 
	  }
	new  Ajax.Request('News_List.aspx',options);   
}

function Lock()
{
    var l = GetAllChecked();
    if(l == "")
    {
        alert("您没有选择要锁定的新闻!");
        return;
    }
    if(confirm('确定要锁定您所选择的新闻吗?'))
    {
        SendAjax("LockNews",l);
    }
}

function clearFiles()
{
    var l = GetAllChecked();
    if(l == "")
    {
        if(confirm('此操作将操作所有您选定表的没用的数据?\n清理后，数据及附件文件都会清除!'))
        {
            SendAjax("clearFiles","foosun");
        }
    }
    else
    {
        if(confirm('您确定要清理您所选定新闻的所有没用的附件吗?\n清理后，数据及附件文件都会清除!'))
        {
            SendAjax("clearFiles",l);
        }
    }
}


function ResetOrder()
{
    var l = GetAllChecked();
    if(l == "")
    {
        alert("您没有选择要重置的新闻!");
        return;
    }
    if(confirm('确定要重置选定的新闻吗?\n重置后所有权重更新为0(最低级)'))
    {
        SendAjax("ResetOrder",l);
    }
}

function makeFilesHTML()
{
    var l = GetAllChecked();
    if(l == "")
    {
        alert("您没有选择要生成的新闻!");
        return;
    }
    SendAjax("makeFilesHTML",l);
}

function XMLRefresh(id)
{
     if(id == "")
    {
        alert("请选择栏目生成XML!");
        return;
    }
     SendAjax("XMLRefresh",id);
}

function makeClassIndex(id)
{
        alert("门户版、传媒版才具备此功能!");
        return;
}

function ClassRefresh(classid)
{
    SendAjax("ClassRefresh",classid);
}

function UNLock()
{
    var l = GetAllChecked();
    if(l == "")
    {
        alert("您没有选择要取消锁定的新闻!");
        return;
    }
    if(confirm('确定要取消锁定您所选择的新闻吗?'))
    {
        SendAjax("UNLockNews",l);
    }
}

function CheckStat1()
{
    var l = GetAllChecked();
    if(l == "")
    {
        alert("选择新闻!");
        return;
    }
    if(confirm('确定您选定的新闻全部通过终极审核吗?'))
    {
        SendAjax("allCheck",l);
    }
}

function onRcvMsg(rtstr)
{
    if(rtstr.length > 200)
    {
        alert("抱歉！操作失败");
    }
    else
    {
        var n = rtstr.indexOf("%");
        alert(rtstr.substr(n+1,rtstr.length-n-1));
        if(parseInt(rtstr.substr(0,n)) > 0)
        {
            __doPostBack('PageNavigator1$LnkBtnGoto','');
        }
    }
}

function ClickHandler(obj)
{
    var strn = GetAllChecked();
    //var seltb = document.getElementById("DdlNewsTable");
    //var val = seltb.options[seltb.selectedIndex].value;
    location.href = "News_Manage.aspx?Option="+ obj +"&ids="+ strn +"&dbtab=<%Response.Write(DPre); %>news";
}
function ShowDetail(obj)
{
    var trx = obj.parentNode.parentNode;
    var n = trx.rowIndex;
    n += 1;
    var tb = trx.parentNode;
    var trn = tb.rows[n];
    if(trold != null && trold != trn && trold.style.display == '')
        trold.style.display = 'none';
    if(trn.style.display == '')
        trn.style.display = 'none';
    else
        trn.style.display = '';
    trold = trn;
}
function ModPic(obj)
{
   var td1 = obj.parentNode.parentNode.lastChild;
   var chk = td1.lastChild;
   if(chk != null && chk.type == "checkbox")
   {
    var idx = chk.value;
    var objtb = document.getElementById("DdlNewsTable");
    var tbx = objtb.options[objtb.selectedIndex].value;
    window.open('News_PicSet.aspx?id='+ idx +'&tb='+ tbx,'','status=0,directories=0,resizable=0,toolbar=0,location=0,scrollbars=1,width=550,height=480');
   }
}

function delNum(ClassID)
{
    if(confirm('确定要清空该栏目下的所有数据吗?\n警告，此操作不可逆！'))
    {
        //var tb = objtb.options[objtb.selectedIndex].value;
        var param = "Option=delNumber&NewsID="+ ClassID;
        var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {
		   var retv=transport.responseText;
		   OnSetTop(retv);
		} 
	  }
	new  Ajax.Request('News_List.aspx',options);  
    }
}

//清空数据
function delSelectedNum()
{    
    var l;
    l = GetAllNews();
    if(l == "")
    {
        alert("当前栏目没有数据!");
        return;
    }
    if(confirm('您确定清空所有的新闻吗?清空后将不能被恢复!'))
    {
         SendAjax("DeleteNews",l);
    }
}

function AddToJS(id)
{
    var l;
    if(id < 0)
    {
        l = GetAllChecked();
        if(l == "")
        {
            alert("您没有选择要加入JS的新闻!");
            return;
        }
    }
    else
    {
        l = id;
    }
	if (l!="") 
	{
	
	    window.open('Frame.aspx?NewsID=' + l,'', 'width=350, height=250, top=300,left=250,toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no');
	}
	else alert('请选择新闻');
}

function AddToSpecial(id)
{
    var l;
    if(id < 0)
    {
        l = GetAllChecked();
        if(l == "")
        {
            alert("您没有选择要加入专题的新闻!");
            return;
        }
    }
    else
    {
        l = id;
    }
	if (l!="") 
	{
	
	    window.open('Frame.aspx?Special=1&NewsID=' + l,'', 'width=350, height=250, top=300,left=250,toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no');
	}
	else alert('请选择新闻');
}

function closediv()
{
     document.getElementById("opld").style.display="none";
}

//-->
</script>
</head>
<body>
<form id="form1" runat="server">
<div id="opld" class="divstyle" style="text-align:center;display:none;">
    <div style="text-align:right;cursor:pointer;"><img alt="关闭" src="../../sysImages/folder/colosediv.gif" border="0" onclick="closediv();" /></div>
    <asp:LinkButton ID="LnkBtnHeadline" CssClass="topnavichar" runat="server" OnClick="LnkBtnHeadline_Click">头条</asp:LinkButton>&nbsp;┊&nbsp;
    <asp:LinkButton ID="LnkBtnSlide" CssClass="topnavichar" runat="server" OnClick="LnkBtnSlide_Click">幻灯片</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnmy" CssClass="topnavichar" runat="server" OnClick="LnkBtnmy_Click">我的信息</asp:LinkButton>&nbsp;┊&nbsp;
    <asp:LinkButton ID="LnkBtnisHtml" CssClass="topnavichar" runat="server" OnClick="LnkBtnisHtml_Click">已生成</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnunisHtml" CssClass="topnavichar" runat="server" OnClick="LnkBtnunisHtml_Click">未生成</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnundiscuzz" CssClass="topnavichar" runat="server" OnClick="LnkBtnundiscuzz_Click">允许讨论组</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnuncommat" CssClass="topnavichar" runat="server" OnClick="LnkBtnuncommat_Click">允许评论</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnunvoteTF" CssClass="topnavichar" runat="server" OnClick="LnkBtnunvoteTF_Click">允许投票</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnuncontentPicTF" CssClass="topnavichar" runat="server" OnClick="LnkBtnuncontentPicTF_Click">画中画</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnunPOPTF" CssClass="topnavichar" runat="server" OnClick="LnkBtnunPOPTF_Click">浏览权限</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton ID="LnkBtnunFilesURL" CssClass="topnavichar" runat="server" OnClick="LnkBtnunFilesURL_Click">有附件的</asp:LinkButton></div>
<div>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="30%" class="sysmain_navi"  style="PADDING-LEFT: 14px; height: 32px;" >新闻管理</td>
          <td width="70%" class="topnavichar"  style="PADDING-LEFT: 14px; height: 32px;" ><div align="left">导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="News_List.aspx" target="sys_main" class="list_link">新闻管理</a><span id="naviClassName" runat="server" /></div></td>
        </tr>
        </table>
              <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                  <td style="PADDING-LEFT: 14px;"><a href="News_list.aspx" class="topnavichar" title="所有表内的新闻信息">全部</a>&nbsp;┊&nbsp;<a href="news_add.aspx?ClassID=<%Response.Write(Request.QueryString["ClassID"]); %>&EditAction=Add" class="topnavichar"><font color="red">添加</font></a>&nbsp;┊&nbsp;<asp:LinkButton
                          ID="LnkBtnAuditing" CssClass="topnavichar" runat="server" OnClick="LnkBtnAuditing_Click">已审核</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton
                              ID="LnkBtnUnAuditing" CssClass="topnavichar" runat="server" OnClick="LnkBtnUnAuditing_Click">未审核</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton
                                  ID="LnkBtnContribute" CssClass="topnavichar" runat="server" OnClick="LnkBtnContribute_Click">投稿</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton
                                    ID="LinkBtnLock" CssClass="topnavichar" runat="server" OnClick="LnkBtnLock_Click">锁定</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton   
                                        ID="LinkBtnUnLock" CssClass="topnavichar" runat="server" OnClick="LnkBtnUnLock_Click">开放</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton 
                                            ID="LnkBtnCommend" CssClass="topnavichar" runat="server" OnClick="LnkBtnCommend_Click">推荐</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton
                                              ID="LnkBtnTop" CssClass="topnavichar" runat="server" OnClick="LnkBtnTop_Click">置顶</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton
                                                  ID="LnkBtnHot" CssClass="topnavichar" runat="server" OnClick="LnkBtnHot_Click">热点</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton
                                                      ID="LnkBtnPic" CssClass="topnavichar" runat="server" OnClick="LnkBtnPic_Click">图片</asp:LinkButton>&nbsp;┊&nbsp;<asp:LinkButton
                                                          ID="LnkBtnSplendid" CssClass="topnavichar" runat="server" OnClick="LnkBtnSplendid_Click">精彩</asp:LinkButton>&nbsp;┊&nbsp;<span  onclick="document.getElementById('opld').style.display='block';" style="cursor:pointer;">更多</span>&nbsp;┊&nbsp;<asp:DropDownList ID="DdlSite"  Width="88px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlSite_SelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                </tr>
        </table>

    <div align="left"  style="PADDING-LEFT: 14px;"><a href="javascript:Recycle(-1)" class="topnavichar">删除</a>┊<a href="javascript:Delete(-1)" class="topnavichar">彻底删除</a>┊<a href="javascript:CheckStat1()" class="topnavichar" title="审核选定的新闻">审核</a><%--&nbsp;┊&nbsp;<a href="javascript:Recycle(-1)" class="topnavichar">取消审核</a>--%>┊<a href="javascript:Lock()" class="topnavichar">锁定</a>┊<a href="javascript:UNLock()" class="topnavichar">解锁</a>┊<a href="javascript:ResetOrder()" class="topnavichar">重置权重</a>┊<a href="javascript:ClickHandler('BnMove')" class="topnavichar">移动</a>┊<a href="javascript:ClickHandler('BnCopy')" class="topnavichar">复制</a>┊<a href="javascript:ToOld()" class="topnavichar">归档</a>┊<a href="javascript:AddToJS(-1)" class="topnavichar" title="把选定的新闻加入自由JS">JS</a>┊<a href="javascript:AddToSpecial(-1)" class="topnavichar" title="把选定的新闻加入专题">专题</a>┊<span id="deltable" runat="server"></span>┊<a href="javascript:ClickHandler('BnProperty')" class="topnavichar" title="批量设置属性">属性</a><span id="isMakeHTML" runat="server">┊<a href="javascript:makeFilesHTML()" class="topnavichar" title="生成选定的新闻的静态页面">生成静态文件</a></span>┊<label id="XMLFile" runat="server" />┊<label id="ClassNewsIndex" runat="server" />┊<label id="ClassRefresh" runat="server" /><span style="display:none;">┊<a href="javascript:clearFiles()" class="topnavichar">清理附件</a></span></div>
  
      <asp:Repeater ID="DataList1" runat="server"  OnItemDataBound="DataList1_ItemDataBound">
    <HeaderTemplate>
         <table id="tablist" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="table">
      <tr class="TR_BG">
        <td align="center" class="sysmain_navi" style="width:25px;"></td>
        <td align="center" class="sysmain_navi">标题</td>
        <td align="left" class="sysmain_navi" style="width:60px;">编辑</td>
       <td align="center" class="sysmain_navi" style="width:130px;">审核操作</td>
        <td align="center" class="sysmain_navi" style="width:35px;">状态</td>
        <td align="center" class="sysmain_navi" style="width:200px;">操作<input name="Checkboxc" type="checkbox" onclick="javascript:selectAll(this.form,this.checked);" /></td>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr class="TR_BG_list"  onmouseover="overColor(this)" onmouseout="outColor(this)">
          <td class="list_link" align="center"><asp:Image runat="server" onclick="ShowDetail(this)" ID="ImgOrder" Width="18" BorderWidth="0" AlternateText='<%#((DataRowView)Container.DataItem)["OrderID"]%>' style="CURSOR: hand"/></td>
          <td class="list_link"><asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%#((DataRowView)Container.DataItem)["URLaddress"]%>'><asp:Image runat="server" ID="ImgNewType" AlternateText='<%#((DataRowView)Container.DataItem)["NewsType"]%>' onclick="ModPic(this.parentNode)"/></asp:HyperLink><asp:Image ID="ImgPic" runat="server" AlternateText='<%#((DataRowView)Container.DataItem)["NewsType"]%>' onclick="ModPic(this)"/><a class="list_link" title="<%#((DataRowView)Container.DataItem)["NewsTitle"]%>" href="News_add.aspx?ClassID=<%#((DataRowView)Container.DataItem)["ClassID"]%>&NewsID=<%#((DataRowView)Container.DataItem)["NewsID"]%>&EditAction=Edit"><%#((DataRowView)Container.DataItem)["NewsTitles"]%></a><%#((DataRowView)Container.DataItem)["CommNum"]%><%#((DataRowView)Container.DataItem)["isConstrs"]%></td> 
          <td align="left"><a href="News_list.aspx?ClassID=<%#((DataRowView)Container.DataItem)["ClassID"]%>&Editor=<%#((DataRowView)Container.DataItem)["Editor"]%>" title="查看此编辑/会员的文章" class="list_link"><%#((DataRowView)Container.DataItem)["Editor"]%></a><a href="../../<%Response.Write(Foosun.Config.UIConfig.dirUser); %>/showuser-<%#((DataRowView)Container.DataItem)["Editor"]%>.aspx" target="_blank"><img src="../../sysImages/folder/addnew.gif" alt="" title="查看会员资料" border="0" /></a></td>
          <td class="list_link" align="center"><%#((DataRowView)Container.DataItem)["CheckStats"]%></td>
          <td align="center"><%#((DataRowView)Container.DataItem)["htmllock"]%></td>
          <td align="center"><%#((DataRowView)Container.DataItem)["op"]%></td>
      </tr>  
       <tr class="TR_BG_list" style="display:none">
        <td class="list_link" colspan="7" style="height:30px;">所属栏目:<%#((DataRowView)Container.DataItem)["ClassCName"]%> &nbsp;┊ &nbsp;作者:<%#((DataRowView)Container.DataItem)["Author"]%>&nbsp;┊  &nbsp;新闻属性:<asp:Label runat="server" ID="LblProperty" Text='<%#((DataRowView)Container.DataItem)["NewsProperty"]%>' /> &nbsp;┊ &nbsp;点击：<%#((DataRowView)Container.DataItem)["Click"]%></td>
       </tr>
     </ItemTemplate>
     <FooterTemplate>
    </table>
     </FooterTemplate>
    </asp:Repeater>
<table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
<tr><td align="left">    新闻搜索:
        &nbsp;&nbsp;
        栏目:
        <asp:TextBox ID="keyWorks" runat="server" CssClass="form" onclick="selectFile('newsclass',document.form1.keyWorks,300,500);document.form1.keyWorks.focus();" Width="141px"></asp:TextBox>
        &nbsp;
关键字:
        <asp:TextBox runat="server" ID="TxtKeywords" size="15" />
        &nbsp;
        <asp:DropDownList ID="DdlKwdType" runat="server">
            <asp:ListItem Value="title" Text="标题" />
            <asp:ListItem Value="content" Text="内容" />
            <asp:ListItem Value="author" Text="作者" />
            <asp:ListItem Value="editor" Text="编辑" />
            <asp:ListItem Value="souce" Text="来源" />
        </asp:DropDownList>
         &nbsp;&nbsp;
         <asp:Button runat="server" ID="BtnSearch" Text=" 搜索 " CssClass="form" OnClick="BtnSearch_Click"/></td><td align="right">
         </td></tr>
 <tr><td align="right" colspan="2">   <uc1:PageNavigator ID="PageNavigator1" runat="server" />
</td></tr>
</table>     
    </div>
    </form>
<br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><div id="SiteCopyRight" runat="server" /></td>
  </tr>
</table>
    <asp:Label ID="LblChoose" runat="server" Visible="False" Width="49px"></asp:Label>
    <input id="HiddenSpecialID" runat="server" type="hidden" />
</body>
</html>
