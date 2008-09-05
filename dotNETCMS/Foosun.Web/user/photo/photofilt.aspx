<%@ Page Language="C#" ContentType="text/html" AutoEventWireup="true" Inherits="user_photofilt" Debug="true" Codebehind="photofilt.aspx.cs" %>

<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body><form id="form1" name="form1" method="post" action="" runat="server"> 
<table width="100%"  border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%"  class="sysmain_navi"  style="PADDING-LEFT: 14px" >相册管理</td>
          <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="Photoalbumlist.aspx"  class="list_link">相册管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />幻灯片播放</div></td>
        </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
  <tr>
     <td><span class="topnavichar" style="PADDING-LEFT: 14px"><a href="Photoalbumlist.aspx" class="menulist">相册首页</a>&nbsp;┊&nbsp;<a href="photo_add.aspx" class="menulist">添加图片</a>&nbsp;┊&nbsp;<a href="#" class="menulist">幻灯播放</a>&nbsp;┊&nbsp;<a href="photoclass.aspx" class="menulist">相册分类</a>&nbsp;┊&nbsp;<a href="Photoalbum.aspx" class="menulist">添加相册</a></span></td>
  </tr>
</table>
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" style="border-collapse: collapse" class="table">
  <%--<form name="player" method="post" action="">--%>
    <tr class="TR_BG">
      <td><div align="center">频率：
        <select class="form" name="intsec">
                <option value="1000">1秒</option>
                <option value="3000" selected="selected">3秒</option>
                <option value="5000">5秒</option>
                <option value="8000">8秒</option>
                <option value="10000">10秒</option>
              </select>
              <input name="button" type="button" class="form" onclick="javascipt:go(1);" value="开始" />
              <input name="button" type="button" class="form" onclick="javascipt:go(2);" value="停止" />
              <input name="button" type="button" class="form" onclick="javascipt:go(3);" value="上一张" />
              <input name="button" type="button" class="form" onclick="javascipt:go(4);" value="下一张" />
      </div></td>
    </tr>
<%--    </form>--%>
  <tr class="TR_BG_list">
    <td height="377" align="center" valign="top"><div align="center"><br/>
            <img src="../../sysImages/folder/photoreview.gif" name="FLashIMG" id="img" style="cursor:hand" onclick="window.open(img.src);" onload="this.height=250;this.width=300;" /> <br/>
    </div></td>
  </tr>
</table>

<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
</form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var mid=0;
    var t=0;
    
    function ImgArray(len)
    {
        this.length=len;
    }
    
    <% Response.Write(photostr); %>
    
    function play_filt()
    {
        if(ImgName.length==0)
            return;
        t_end=document.form1.intsec.value;
        if (t==<% Response.Write(photocount); %>)
            t=0;
        else
            t++;
        
        if(ImgName[t]==undefined)
            t=0;
        if (mid==0)
        {
            document.getElementById("FLashIMG").style.filter="blendTrans(Duration=1)";
            document.getElementById("FLashIMG").filters[0].apply();
            document.getElementById("FLashIMG").src=ImgName[t];
            document.getElementById("FLashIMG").filters[0].play();
            setTimeout("play_filt()",t_end);
        }
    }
    function go(id)
    {
        if (id==1)
        {
	        mid=0
	        play_filt();
        }
        else if(id==2)
        {
	        mid=1;
        }
        else if(id==3)
        { 
	        mid=1;
	        t=t-1;
	        if (t>=0) 
	            document.getElementById("FLashIMG").src=ImgName[t]; 
	        else
	            t=0;
        }
        else if(id==4)
        {
	        mid=1;
	        t=t+1;
	        if (parseInt(t,10)<<% Response.Write(photocount); %>) 
	            document.getElementById("FLashIMG").src=ImgName[t];
	        else
	            t=<% Response.Write(photocount); %>;
        }
        else
        {
	        mid=0;
	        t=0;
	        play_filt();
        }
    }
</script>