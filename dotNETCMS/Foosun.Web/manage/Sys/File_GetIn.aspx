<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="File_GetIn.aspx.cs" Inherits="Foosun.Web.manage.Sys.File_GetIn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
</head>
<body>
<table width="100%"  align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
  <tr>
    <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" Height="30">文件管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img src="../../sysImages/folder/navidot.gif" border="0" />资源文件管理</div></td>
  </tr>
</table>
<!----显示功能菜单------------------------>
<div id="operatefile" runat="server"></div>
<!---------显示文件夹及文件的管理页-------------------->
<div id="filemanage_list" runat="server" />
<!----------------------------------------------------->
<!------------------------------------------------------------>
<table width="98%" border="0" cellspacing="0" cellpadding="5" align="center">
  <tr>
    <td class="reshow">提示：点击目录进入下一级目录<br />
      注意：同级目录之间的转移请直接填写目的文件夹名称,如(foosun),同级到其下级或是同级间其他文件夹的下级目录之间的转移,填写方式如(foosun\netcms),转移到上级文件夹目录时,填写方式如(..\foosun\netcms)</td>
  </tr>
</table>
<br />
<br />
<!-------CopyRight------->
<table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
  <tr>
    <td align="center"><label id="copyright" runat="server" /></td>
  </tr>
</table>
<form id="FileList" action="" runat="server" method="post">
  <input type="hidden" name="Type" />
  <input type="hidden" name="Path"/>
  <input type="hidden" name="ParentPath" />
  <input type="hidden" name="OldFileName" />
  <input type="hidden" name="NewFileName" />
  <input type="hidden" name="filename" />
  <input type="hidden" name="Urlx" />
  <input type="hidden" name="dateNums" />
</form>
</body>
<script language="javascript" type="text/javascript">
//JS函数，传递参数EidtDirName,EidtFileName,DelDir,DelFile,AddDir,在.cs文件中调用相应函数实现其功能
//---------------------进入下一级目录-----------
function ListGo(Path,ParentPath)
{
	document.FileList.Path.value=Path;
	document.FileList.ParentPath.value=ParentPath;
	document.FileList.submit();
}
//--------------------修改文件夹名称--------------
function EditFolder(path,filename)   
{
	var ReturnValue='';
	ReturnValue=prompt('修改的名称：',filename.replace(/'|"/g,''));
	if ((ReturnValue!='') && (ReturnValue!=null))
	{
	    document.FileList.Type.value="EidtDirName";
	    document.FileList.Path.value=path;
	    document.FileList.OldFileName.value=filename;
	    document.FileList.NewFileName.value=ReturnValue;
	    document.FileList.submit();
	}
	else
	{
	    if(ReturnValue!=null)
	    {
	        alert('请填写要更名的名称');
	    }    
	}
}
//---------------------修改文件名称---------------
function EditFile(path,filename)   
{
	var ReturnValue='';
	ReturnValue=prompt('修改的名称：',filename.replace(/'|"/g,''));
	if ((ReturnValue!='') && (ReturnValue!=null))
	{
	    document.FileList.Type.value="EidtFileName";
	    document.FileList.Path.value=path;
	    document.FileList.OldFileName.value=filename;
	    document.FileList.NewFileName.value=ReturnValue;
	    document.FileList.submit();
	}
	else
	{
	    if(ReturnValue!=null)
	    {
	        alert('请填写要更名的名称');
	    }    
	}
}
//-------------------删除文件夹------------------
function DelDir(path)
{
    if(confirm('请确定你在做什么!!!\n确定删除此文件夹以及此文件夹下所有文件吗?'))
    {
	    document.FileList.Type.value="DelDir";
	    document.FileList.Path.value=path;
	    document.FileList.submit();
    }
}
//------------------清空图片
function getDateNum_del(path)
{
    var dateNum = document.getElementById("dateNum").value;
    if(dateNum!="")
    {
        if(confirm('请确定你在做什么!!!\n确定清除' + dateNum + '天(包括)前的图片文件吗?\n清除图片格式有：jpg,jpeg,gif,swf,bmp,png,ico.'))
        {
	        document.FileList.Type.value="clearFile";
	        document.FileList.Path.value=path;
	        document.FileList.dateNums.value=dateNum;
	        document.FileList.submit();
        }  
    }
    else
    {
        alert('请填写天数！');
        document.getElementById("dateNum").focus();
    }
      
}
//---------------删除文件-------------------------
function DelFile(path,filename)
{
    if(confirm('请确定你在做什么!!!\n确定删除此文件吗'))
    {
	    document.FileList.Type.value="DelFile";
	    document.FileList.Path.value=path;
	    document.FileList.filename.value=filename;
	    document.FileList.submit();
    }
}
//-----------------新增文件夹---------------------
function AddDir(path)
{
	var ReturnValue='';
	var filename='';
	ReturnValue=prompt('要添加的文件夹名称',filename.replace(/'|"/g,''));
	if ((ReturnValue!='') && (ReturnValue!=null))
	{
	    document.FileList.Type.value="AddDir";
	    document.FileList.Path.value=path;
	    document.FileList.filename.value=ReturnValue;
	    document.FileList.submit();
	}
	else
	{
	    if(ReturnValue!=null)
	    {
	        alert('请填写要添加的文件夹名称');
	    }    
	}
}
//------------------上传文件---------------------
function UpFile(path)
{
     var WWidth = (window.screen.width-500)/2;
     var Wheight = (window.screen.height-150)/2;
     window.open ("../../configuration/system/Upload.aspx?Path="+path, '文件上传', 'height=150, width=500, top='+Wheight+', left='+WWidth+', toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no'); 
}
//-----------------移动文件夹--------------------
function MoveFileFolder(path,filename)
{ 
    var ReturnValue='';
	ReturnValue=prompt('请输入您转移的目的文件夹名称','');
	if ((ReturnValue!='') && (ReturnValue!=null))
    {
	    document.FileList.Type.value="MoveFileFolder";
	    document.FileList.Path.value=path;
	    document.FileList.OldFileName.value=filename;
	    document.FileList.NewFileName.value=ReturnValue;
	    document.FileList.submit();
	}
}
//---------------移动文件------------------------
function MoveFile(path,filename)
{ 
    var ReturnValue='';
	ReturnValue=prompt('请输入您转移的目的文件夹名称','');
	if ((ReturnValue!='') && (ReturnValue!=null))
    {
	    document.FileList.Type.value="MoveFile";
	    document.FileList.Path.value=path;
	    document.FileList.OldFileName.value=filename;
	    document.FileList.NewFileName.value=ReturnValue;
	    document.FileList.submit();
	}
}
</script>
</html>
