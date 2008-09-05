<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_show_showphotofilt" Codebehind="showphotofilt.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>	
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>__幻灯</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<script language="javascript" type="text/javascript">
var mid=0;
var t=0;
var i=0;
var ImgName=null;
function ImgArray(len)
{
this.length=len;
}
function CreateImage(url)
{
    ImgName = url.split('\t');
}
function play_filt()
{ 
t_end=document.form1.intsec.value;
if (t==ImgName.length)
{
t=0;
}
else
{
t++;
} 
if (t==ImgName.length)
{
t_end=100;
} 
if (mid==0)
{ 
document.getElementById("img").style.filter="blendTrans(Duration=1)"; 
document.getElementById("img").filters[0].apply();
document.getElementById("img").src=ImgName[t];
tIndex=t; 
document.getElementById("img").filters[0].play();
mytimeout=setTimeout("play_filt()",t_end);
}
}
function go(id){ 
if (id==1){ 
mid=0;
play_filt();
} 
else if(id==2){ 
mid=1;
}
else if(id==3){ 
mid=1;
t=t+1; 
if (t<=ImgName.length) document.getElementById("img").src=ImgName[t-1];
} 
else if(id==4){ 
mid=1; 
t=t-1; 
if (t>0) document.getElementById("img").src=ImgName[t+1];
} 
} 
</script>

<body onload="CreateImage('<%# sImgUrl %>')">
<form id="form1" name="form1" runat="server">
<table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" style="border-collapse: collapse" class="table">
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
              <input name="button" type="button" class="form" onclick="javascipt:window.close();" value="关闭" />
      </div></td>
    </tr>
  <tr class="TR_BG_list">
    <td height="377" align="center" valign="top"><div align="center"><br/>
            <img src="../../sysImages/folder/photoreview.gif" name="img" id="img" style="cursor:hand" onclick="window.open(img.src);" onload="this.width=300;this.height=250;" /> <br/>
    </div></td>
  </tr>
</table>
 <br />
 <br />
<table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
  <tr>
    <td><div align="center"><%Response.Write(CopyRight); %> </div></td>
  </tr>
</table>
    </form>
</body>
</html>
