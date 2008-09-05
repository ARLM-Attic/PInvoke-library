<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" Inherits="User_index" Codebehind="index.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>_会员中心</title>
<link rel="icon" href="../favicon.ico" type="image/x-icon" />
<link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" /> 
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Public.js"></script>
<script language="JavaScript" type="text/javascript" src="../Configuration/JS/Prototype.js"></script>
<script language="JavaScript" type="text/javascript">
    var theDownedButtonObj=null;
    function CheckBTN(theObj,URL)
    {
	    var ns6 = document.getElementById&&!document.all
        if(ns6)
        {
	        if (!theDownedButtonObj) {theDownedButtonObj='button_down';}
	            if (theObj.className!='button')
	            {
		            theObj.className='button';
		            theDownedButtonObj.className='button_down';
		            theDownedButtonObj=theObj;
		            frames["sys_main"].location=URL;
	            }
	    }
	    else
	    {
            if (!theDownedButtonObj) {theDownedButtonObj=IDC_DownedBUtton;}
                if (theObj.className!='button')
                {
	                theObj.className='button';
	                theDownedButtonObj.className='button_down';
	                theDownedButtonObj=theObj;
	                frames["sys_main"].location=URL;
                }
	    }
    }

    
     function CheckBTNu(DivID,URL)
     {
        if (document.getElementById(DivID).style.display=='')
        {
            document.getElementById(DivID).style.display='none';
            document.getElementById("fontchar").innerHTML="显示菜单";
        }
        else
        {
            document.getElementById(DivID).style.display='';
            document.getElementById("table_id").style.width='100%';
            document.getElementById("fontchar").innerHTML="隐藏菜单";
	        frames["menu"].location=URL;
        }
    }    
    
    if (ie4||ns6)
	document.onclick=hidemenu
	linkset[0]=new Array()
    //动态调用菜单
	linkset[0][0]='<div><a class="menu_ctr" href="info/userinfo.aspx" target="sys_main">我的资料</a></div>'
	linkset[0][1]='<div><a class="menu_ctr" href="photo/Photoalbumlist.aspx" target="sys_main">我的相册</a></div>'
	linkset[0][2]='<div><a class="menu_ctr" href="info/User_ChangePassword.aspx" target="sys_main">修改密码</a></div>'
	linkset[0][4]='<div><a class="menu_ctr" href="info/history.aspx?ghtypep=3" target="sys_main">交易明晰</a></div>'
	linkset[0][5]='<div><a class="menu_ctr" href="info/getPoint.aspx" target="sys_main">冲值管理</a></div>'
    linkset[0][6]='<div><a class="menu_ctr" href="info/Exchange.aspx" target="sys_main">积分兑换</a></div>'
	linkset[0][7]='<div><a class="menu_ctr" href="info/announce.aspx" target="sys_main">公告管理</a></div>'
	linkset[0][8]='<div><a class="menu_ctr" href="info/collection.aspx" target="sys_main">我的收藏夹</a></div>'
	linkset[0][9]='<div><a class="menu_ctr" href="info/mycom.aspx" target="sys_main">我的评论</a></div>'
	linkset[0][10]='<div><a class="menu_ctr" href="info/friend.aspx" target="sys_main">申请友情联接</a></div>'
	linkset[0][11]='<div><a class="menu_ctr" href="info/shortcut.aspx" target="sys_main">自定义快捷方式</a></div>'
	linkset[0][12]='<div><span class="menu_ctr">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;━━━━━━&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>'
	linkset[0][13]='<div><a class="menu_ctr" href="info/wap.aspx" target="sys_main">WAP访问</a></div>'
	linkset[0][14]='<div><a class="menu_ctr" href="Rss/RSS.aspx" target="sys_main">RSS订阅</a></div>'
	//linkset[0][15]='<div><a class="menu_ctr" href="mobile/mobile.aspx" target="sys_main">手机服务</a></div>'
	//linkset[0][16]='<div><a class="menu_ctr" href="idcard/idcard.aspx" target="sys_main">身份证查询</a></div>'
	
	linkset[1]=new Array()
	linkset[1][0]='<div><a class="menu_ctr" href="Message/Message_write.aspx" target="sys_main">写新消息</a></div>'
	linkset[1][1]='<div><a class="menu_ctr" href="Message/Message_box.aspx?Id=1" target="sys_main">收件箱</a></div>'
	linkset[1][2]='<div><a class="menu_ctr" href="Message/Message_box.aspx?Id=2" target="sys_main">发件箱</a></div>'
	linkset[1][3]='<div><a class="menu_ctr" href="Message/Message_box.aspx?Id=3" target="sys_main">草稿箱</a></div>'
	linkset[1][4]='<div><a class="menu_ctr" href="Message/Message_box.aspx?Id=4" target="sys_main">废件箱</a></div>'


	linkset[2]=new Array()
	linkset[2][0]='<div><a class="menu_ctr" href="Constr/Constr.aspx" target="sys_main">发表文章</a></div>'
	linkset[2][1]='<div><a class="menu_ctr" href="Constr/Constrlist.aspx" target="sys_main">文章管理</a></div>'
	linkset[2][2]='<div><a class="menu_ctr" href="Constr/ConstrClass.aspx" target="sys_main">分类管理</a></div>'
	linkset[2][3]='<div><a class="menu_ctr" href="Constr/Constraccount.aspx" target="sys_main">账号管理</a></div>'
	linkset[2][4]='<div><a class="menu_ctr" href="Constr/ConstrMoney.aspx" target="sys_main">稿酬记录查询</a>&nbsp;&nbsp;&nbsp;</div>'

	linkset[3]=new Array()
	linkset[3][0]='<div><a class="menu_ctr" href="discuss/discussManage_list.aspx" target="sys_main">讨论组管理</a></div>'
	linkset[3][1]='<div><a class="menu_ctr" href="discuss/add_discussManage.aspx" target="sys_main">创建讨论组</a></div>'
	linkset[3][2]='<div><a class="menu_ctr" href="discuss/discussacti.aspx" target="sys_main">创建活动</a></div>'
	linkset[3][3]='<div><a class="menu_ctr" href="discuss/discussacti_list.aspx" target="sys_main">活动管理</a>&nbsp;&nbsp;&nbsp;</div>'
		
	linkset[4]=new Array()
	linkset[4][0]='<div><a class="menu_ctr" href="friend/friendmanage.aspx" target="sys_main">好友分类</a></div>'
	linkset[4][1]='<div><a class="menu_ctr" href="friend/friendList.aspx" target="sys_main">好友管理</a></div>'
	linkset[4][2]='<div><a class="menu_ctr" href="friend/friendmanage_add.aspx" target="sys_main">添加好友分类</a></div>'
	linkset[4][3]='<div><a class="menu_ctr" href="friend/friend_add.aspx" target="sys_main">添加好友</a>&nbsp;&nbsp;&nbsp;</div>'
	
	linkset[5]=new Array()
	<%Response.Write(ChannelList); %>
	
</script>
<link href="../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><table width="100%" border="0" cellpadding="0" cellspacing="0" class="indexq">
      <tr>
        <td align=left><img alt="" src="../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/logo.jpg" /></td>
        <td align="center">&nbsp;</td>
        <td align="right" style="padding-right:10px;"><label id="isManage" runat="server" />&nbsp;&nbsp;<a href="friend/friendList.aspx" class="Lion_1" target="sys_main">好友</a><label id="isFriendPass" runat="server" />&nbsp;&nbsp;┊&nbsp;&nbsp;<label id="messageID" runat="server" />&nbsp;&nbsp;┊&nbsp;&nbsp;<a class="Lion_1" href="info/userinfo.aspx" target="sys_main">资料</a>&nbsp;┊&nbsp;<a href="info/User_ChangePassword.aspx" target="sys_main" class="Lion_1"  title="修改密码">密码</a>&nbsp;┊&nbsp;<a href="Logout.aspx" class="Lion_1">退出</a></td>
      </tr>
    </table></td>
  </tr>
</table>                                
<table width="100%" border="0" cellpadding="0" cellspacing="0" style="cursor:pointer;" background="../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_bg.jpg">
  <tr>
    <td id="IDC_DownedBUtton" class="button_down" onclick="CheckBTNu('menuid','menu.aspx');" style="width:10%;cursor:pointer;"><label id="fontchar">隐藏菜单</label></td>
    <td class="button_down"  style="width:10%;text-align:left;padding-left:10px;background-repeat:no-repeat;background-image:../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_bg.jpg;cursor:pointer;" title="控制面板" onclick="CheckBTN(this,'info/userinfo.aspx');" onmouseover="showmenu(event,0,1,false)" onmouseout="delayhidemenu()">控制面板</td>
    <td class="button_down" style="width:10%;text-align:left;padding-left:10px;background-repeat:no-repeat;background-image:../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_bg.jpg;cursor:pointer;" title="短消息" onclick="CheckBTN(this,'message/Message_box.aspx?Id=1');" onmouseover="showmenu(event,1,1,false)" onmouseout="delayhidemenu()">站内消息</td>
    <td class="button_down" style="width:10%;text-align:left;padding-left:10px;background-repeat:no-repeat;background-image:../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_bg.jpg;cursor:pointer;" title="文章管理" onclick="CheckBTN(this,'Constr/Constrlist.aspx');" onmouseover="showmenu(event,2,1,false)" onmouseout="delayhidemenu()">文章管理</td>
    <td class="button_down" style="width:10%;text-align:left;padding-left:10px;background-repeat:no-repeat;background-image:../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_bg.jpg;cursor:pointer;" title="信息管理" onclick="javascript:void(0);" onmouseover="showmenu(event,5,1,false)" onmouseout="delayhidemenu()">发布信息</td>
    <td class="button_down" style="width:10%;text-align:left;padding-left:10px;background-repeat:no-repeat;background-image:../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_bg.jpg;cursor:pointer;" title="社群管理" onclick="CheckBTN(this,'discuss/discussManage_list.aspx');" onmouseover="showmenu(event,3,1,false)" onmouseout="delayhidemenu()">社群/讨论</td>
    <td class="button_down" style="width:10%;text-align:left;padding-left:10px;background-repeat:no-repeat;background-image:../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/menu_bg.jpg;cursor:pointer;" title="好友管理" onclick="CheckBTN(this,'friend/friendlist.aspx');" onmouseover="showmenu(event,4,1,false)" onmouseout="delayhidemenu()">好友管理</td>
    <td style="width:10%;height:38px;"></td>
    <td style="width:10%;height:38px;"></td>
    <td style="width:10%;height:38px;"></td>
    <td style="width:10%;height:38px;"></td>
  </tr>
</table>
<div class="menuskin" id="popmenu" onmouseover="clearhidemenu();highlightmenu(event,'on')" style="Z-INDEX: 200"  onmouseout="highlightmenu(event,'off');dynamichide(event)" divalpha></div>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td style="width:15%;height:100%;" valign="top" id="menuid"><iframe class="Lion_menu" src="menu.aspx" scrolling="auto" frameborder="0" name="menu" style="height:100%;width:100%;"></iframe>
	</td>
    <td id="table_id" valign="top">
		<div style="width:100%">
		<iframe style="width:100%;padding-left:1px;" scrolling="no" onunload="this.height=480;" onload="foosun_iframeResize();foosun_Scrolliframe();" frameborder="0" id="sys_main" name="sys_main" src="<%Response.Write(URL); %>">您的浏览器不支持此功能，请您使用最新的版本。</iframe>
		</div>
       </td>
  </tr>
</table> 
<input type="hidden" id="content" />
</form>
</body>
<%--<%--<script language="javascript" type="text/javascript">	
　　var timer2=null;
	function go()
    {
         var  options={  
				           method:'get',  
				           parameters:"Type=1",  
				           onComplete:function(transport)
					        {  
						        var returnvalue=transport.responseText;
						        if(returnvalue=="1")	   
	                            window.open('Requestinformation.aspx','','height=200,width=500,top=200,left=250,toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no');
	 
					        }  
			           }; 
        new  Ajax.Request('index.aspx?no-cache='+Math.random(),options);      
   }
   //timer2=setTimeout("go()", 10000); </script>--%>
</html>
