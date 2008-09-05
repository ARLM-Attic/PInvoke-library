<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_admin_GroupAdd"  ResponseEncoding="utf-8" Codebehind="admin_GroupAdd.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<form id="F_AdminGroup" action="" runat="server" method="post">
  <table width="100%" border="0" cellpadding="0" cellspacing="0"  class="toptable">
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" height="30">添加管理员组</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="admin_group.aspx" class="list_link">管理员组管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />添加管理员组</div></td>
    </tr>
  </table>
  <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
    <tr class="TR_BG_list">
      <td width="21%" align="center" class="navi_link">管理员组名称</td>
      <td colspan="4" align="left"><input type="text" name="GroupName" maxlength="20" id="GroupName" style="width:200px;" />
        <span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminGroupAdd_001',this)"> 帮助</span><span id="errorshow" class="reshow"></span> </td>
    </tr>
    <tr class="TR_BG_list">
      <td rowspan="4" align="center" class="navi_link" >可管理的栏目</td>
      <td width="13%" rowspan="4" align="left" valign="middle" style="width: 10%"><asp:ListBox ID="NewsClassList" SelectionMode="Multiple" runat="server" Height="120px" Width="200px"></asp:ListBox></td>
      <td width="15%" align="center" style="width: 13%"><input name="button" type="button" class="form" id="B_Class1" onclick="javascript:Selectone(document.F_AdminGroup.NewsClassList,document.F_AdminGroup.NewsClassList2,document.getElementById('Span1'),document.F_AdminGroup.News_List);" value=" 选  中 "  /></td>
      <td width="12%" rowspan="4" align="left" valign="middle" style="width: 10%">
          &nbsp;
      <asp:ListBox ID="NewsClassList2" SelectionMode="Multiple" runat="server" Height="120px" Width="200px"></asp:ListBox></td>
      <td width="39%" rowspan="4" align="left"><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminGroupAdd_002',this)">帮助</span>
      <div id="Span1" class="reshow"></div></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" style="width: 13%"><input name="button2" type="button" class="form" id="B_Class2" onclick="javascript:SelectAllClass(document.F_AdminGroup.NewsClassList,document.F_AdminGroup.NewsClassList2,document.F_AdminGroup.News_List);" value="全部选中" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"><input name="button2" type="button" class="form" id="Button10" onclick="javascript:unSelectone(document.F_AdminGroup.NewsClassList2,document.getElementById('Span1'),document.F_AdminGroup.News_List);" value=" 取  消 " /></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center" style="width: 13%"><input name="button2" type="button" class="form" id="Button6" onclick="javascript:UnSelectAllClass(document.F_AdminGroup.NewsClassList2,document.F_AdminGroup.News_List);" value="全部取消" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td rowspan="4" align="center" class="navi_link">可管理的频道</td>
      <td rowspan="4" align="left" valign="middle"><asp:ListBox ID="Site1"  SelectionMode="Multiple" runat="server" Height="120px" Width="200px"></asp:ListBox></td>
      <td align="center"><input name="button2" type="button" class="form" id="Button2" onclick="javascript:Selectone(document.F_AdminGroup.Site1,document.F_AdminGroup.Site2,document.getElementById('Span2'),document.F_AdminGroup.Site_List);" value=" 选  中 " /></td>
      <td rowspan="4" align="left" valign="middle">
          &nbsp;<asp:ListBox ID="Site2"  SelectionMode="Multiple" runat="server" Height="120px" Width="200px"></asp:ListBox></td>
      <td rowspan="4" align="left"><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminGroupAdd_003',this)">帮助</span>
        <div id="Span2" class="reshow"></div></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"><input name="button2" type="button" class="form" id="Button4" onclick="javascript:SelectAllClass(document.F_AdminGroup.Site1,document.F_AdminGroup.Site2,document.F_AdminGroup.Site_List);" value="全部选中" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"><input name="button2" type="button" class="form" id="Button9" onclick="javascript:unSelectone(document.F_AdminGroup.Site2,document.getElementById('Span2'),document.F_AdminGroup.Site_List);" value=" 取  消 " /></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"><input name="button2" type="button" class="form" id="Button8" onclick="javascript:UnSelectAllClass(document.F_AdminGroup.Site2,document.F_AdminGroup.Site_List);" value="全部取消" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td rowspan="4" align="center" class="navi_link">可管理的专题</td>
      <td rowspan="4" align="left" valign="middle"><asp:ListBox ID="Special1"  SelectionMode="Multiple" runat="server" Height="120px" Width="200px"></asp:ListBox></td>
      <td align="center"><input name="button2" type="button" class="form" id="Button3"  onclick="javascript:Selectone(document.F_AdminGroup.Special1,document.F_AdminGroup.Special2,document.getElementById('Span3'),document.F_AdminGroup.Sp_List);" value=" 选  中 " /></td>
      <td rowspan="4" align="left" valign="middle">
          &nbsp;<asp:ListBox ID="Special2"  SelectionMode="Multiple"  runat="server" Height="120px" Width="200px"></asp:ListBox></td>
      <td rowspan="4" align="left"><span class="helpstyle" style="cursor:help;" title="点击显示帮助" onclick="Help('H_adminGroupAdd_004',this)">帮助</span>
        <div id="Span3" class="reshow"></div></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"><input name="button2" type="button" class="form" id="Button5" onclick="javascript:SelectAllClass(document.F_AdminGroup.Special1,document.F_AdminGroup.Special2,document.F_AdminGroup.Sp_List);" value="全部选中"  /></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"><input name="button2" type="button" class="form" id="Button11" onclick="javascript:unSelectone(document.F_AdminGroup.Special2,document.getElementById('Span3'),document.F_AdminGroup.Sp_List);" value=" 取  消 " /></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="center"><input name="button2" type="button" class="form" id="Button7" onclick="javascript:UnSelectAllClass(document.F_AdminGroup.Special2,document.F_AdminGroup.Sp_List);" value="全部取消" /></td>
    </tr>
    <tr class="TR_BG_list">
      <td align="left" class="navi_link" colspan="5"><label> &nbsp;
        <input type="button" name="TJ" value=" 确 定 " class="form"  onclick="javascript:Submit(this.form);"/>
        </label>
        &nbsp;
        <label>
        <input type="reset" name="UnDo" value=" 重 填 " class="form" onclick="javascript:listClear();" />
        <input id="News_List" name="News_List" type="hidden" />
        <input id="Site_List" name="Site_list" type="hidden" />
        <input id="Sp_List" name="Sp_list" type="hidden" />
        </label></td>
    </tr>
  </table>
  <br />
  <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px" align="center">
    <tr>
      <td align="center"><label id="copyright" runat="server" /></td>
    </tr>
  </table>
</form>
</body>
<script language="javascript" type="text/javascript">
//-----------------------选择所有--------------------------------
function SelectAllClass(obj,obj1,hiddenobj)
{
    clearall(obj1);         //清空右边的列表框
    //---------------------正则-----------------------------
    var re = /┝/g;
    var re1 = /┉/g;
    //---------------------循环获取左边列表框值-------------
    hiddenobj.value = "";
    for(var i=0;i<obj.length;i++)
    {
        var text = obj.options[i].text;
        //----------------正则替换-------------------------
        text = text.replace(re,"");
        text = text.replace(re1,"");
        hiddenobj.value = hiddenobj.value +  obj.options[i].value + ","; //给隐藏域赋值
        obj1.options[i] = new Option(text,obj.options[i].value);//-添加选项到右边列表框
    }
    //---------------------循环获取左边列表框值结束---------
}
//---------------------选择所有结束-----------------------------
//---------------------取消选择所有-----------------------------
function UnSelectAllClass(obj,hiddenobj)
{
    clearall(obj);
    hiddenobj.value = "";                   //给隐藏域赋值
}
//---------------------取消选择所有结束-------------------------
//---------------------选择一个选项-----------------------------
function Selectone(obj,obj1,span,hiddenobj)
{
    var s=0;
    //--------------------正则--------------------------------
    var re = /┝/g;
    var re1 = /┉/g;
    /*
    //--------------------判断是否选中选项--------------------
    for(var i=0;i<obj.length;i++){ 
    if (obj.options[i].selected){s+=1;} 
    }      
    if (s==0){   
    span.innerHTML="  (*)请选择左边列表框的项目再点选中";
    return;}  
    span.innerHTML="";
    var text = obj.options[obj.selectedIndex].text;
    text = text.replace(re,"");
    text = text.replace(re1,"");
    //--------------------判断右边列表框中否包含此项----------
    for (var i=0;i<obj1.length;i++)
    {
        if(obj1.options[i].text==text)
        {
            span.innerHTML="  (*)右边列表框的项目中已包含此项";
            return;
        }
    }
    //-------------------添加到右边选项框----------------------
    hiddenobj.value = hiddenobj.value +  obj.options[i].value + ",";//给隐藏域赋值
    obj1.options[obj1.length] = new Option(text,obj.options[obj.selectedIndex].value);
    //-------------------判断是否到最后一项,如果不是则焦点移到下一项
    if(obj.selectedIndex<obj.length)
    {
        obj.selectedIndex = obj.selectedIndex + 1; 
    }
    */
    /*
    以下js由arjun更改2008-3-6
    */
    for(var i=0;i<obj.length;i++)
    {
        if(obj.options[i].selected)
        {
            var text=obj.options[i].text;
            text = text.replace(re,"");
            text = text.replace(re1,"");
			var wr=true;
			for(var j=0;j<obj1.length;j++)
			{
				if(obj1.options[j].text==text)
				{
					span.innerHTML="  (*)右边列表框的项目中已包含[<font color=red>"+text+"</font>]项";
					wr=false;
					break;
				}
			}
			if(wr)
			{
				 hiddenobj.value = hiddenobj.value +  obj.options[i].value + ",";//给隐藏域赋值
				 obj1.options[obj1.length] = new Option(text,obj.options[i].value);
			}
            
        }
    }
}
//---------------------选择一个选项结束-------------------------
//---------------------取消一个---------------------------------
//change by arjun
function unSelectone(obj,span,hiddenobj)
{
    /*
    var s=0;
    //-----------------判断是否选中------------------------
    for(var i=0;i<obj.length;i++){
    if (obj.options[i].selected){s+=1;}}   
    if (s==0){
    span.innerHTML="  (*)请选择右边列表框的项目再点取消";
    return;}
    //-----------------移除选中的选项----------------------
    hiddenobj.value = hiddenobj.value.replace(obj.options[obj.selectedIndex].value + ",",""); //给隐藏域赋值
    obj.options[obj.selectedIndex]=null;
    //-----------------判断是否还有选项,如有则移到最后-----
    if(obj.length > 0)
    {
        obj.options[obj.length-1].selected=true;
    }
    */
    var ii=[];
    for(var i=0;i<obj.length;i++)
    {
       if(obj.options[i].selected)
       {
			ii[ii.length]=i;
            hiddenobj.value=hiddenobj.value.replace(obj.options[obj.selectedIndex].value + ",","");
       }
    }
	for(var i=ii.length-1;i>=0;i--)
	{
		obj.options[ii[i]]=null;
	}
}
//---------------------取消一个结束-----------------------------
//---------------------移除右边列表框所有选项-------------------
// change by arjun
function clearall(obj)
{
    var testnum=obj.length;
    for(var j=testnum-1;j>=0;j--)
    {
        obj.options[j]=null;
    }
}
//--------------------移除右边列表框所有选项结束----------------
//--------------------提交表单信息------------------------------
function Submit(formobj)
{
    if(formobj.GroupName.value == "")
    {
        document.getElementById("errorshow").innerHTML = " (*)管理员组名称不能为空";
    }
    else
    {
        //var listStr=formobj.News_List.value;
        //var siteListStr=formobj.Site_List.value;
        //var spListStr=formobj.Sp_List.value;
        var groupName=formobj.GroupName.value;
        var listStr=getSelectStr(document.getElementById("NewsClassList2"),false);
        var siteListStr=getSelectStr(document.getElementById("Site2"),false);
        var spListStr=getSelectStr(document.getElementById("Special2"),false);
        //alert(listStr);
        //alert(siteListStr);
        //alert(spListStr);
        //alert(groupName);
        var ID='<% Response.Write(Request.QueryString["ID"]); %>';
        formobj.News_List.value=listStr;
        formobj.Site_List.value=siteListStr;
        formobj.Sp_List.value=spListStr;
        //alert(formobj.News_List.value);
        //alert(formobj.Site_List.value);
        //alert(formobj.Sp_List.value);
        //formobj.action = "?ID="+escape(ID)+"&Type=Add&GroupName="+groupName+"&News_List="+escape(listStr)+"&Site_List="+escape(siteListStr)+"&Sp_List="+escape(spListStr);
        formobj.action = "?ID="+escape(ID)+"&Type=Add"
        formobj.submit();
    }
}

//去除最后一个豆号 code by arjun
function qudouhao(str)
{
    var s=str;
    if(s==null)return "";
    if(s=="")return s;
    if(s.substr(s.length-1)==",")
    {
        s=s.substring(0,s.length-1)
    }
    return s;
}

//取得选择框的值 code by arjun
//sel为true时,返回选择的
//sel为false时,返回所有
function getSelectStr(obj,sel)
{
	var returnArr=[];
	var str="";
	for(var i=0;i<obj.length;i++)
	{
	    if(sel)
	    {
		    if(obj.options[i].selected)
		    {
			    returnArr[returnArr.length]=obj.options[i].value;
		    }
		}
		else
		{
		    returnArr[returnArr.length]=obj.options[i].value;
		}
	}
	str=returnArr.join(",");
	if(str==""||str==null)
	{
	    str="null";
	}
	return str;
}

//--------------------提交表单信息结束-------------------------
//--------------------重置右边列表框---------------------------
function listClear()
{
    UnSelectAllClass(document.F_AdminGroup.NewsClassList2,document.F_AdminGroup.News_List);
    UnSelectAllClass(document.F_AdminGroup.Site2,document.F_AdminGroup.Site_List);
    UnSelectAllClass(document.F_AdminGroup.Special2,document.F_AdminGroup.Sp_List);
}
//--------------------重置右边列表框结束-----------------------
</script>
</html>
