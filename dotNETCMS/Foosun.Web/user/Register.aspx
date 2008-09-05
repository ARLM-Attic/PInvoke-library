<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_Register" Codebehind="Register.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <script language="JavaScript" type="text/javascript" src="../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../configuration/js/Public.js"></script>
     
    <title>
        <%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>
        __会员注册</title>
    <link href="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css"
        rel="stylesheet" type="text/css" />
</head>
<body onload="GetRootClass()">
    <form id="form1" name="form1" method="post" action="Register.aspx" runat="server">
        <div style="width: 100%" id="topshow">
            <table border="0" cellpadding="2" class="2" style="width: 100%;">
                <tr>
                    <td style="width: 30%;">
                        <a href="http://www.foosun.net" target="_blank">
                            <img src="../sysImages/user/userlogo.gif" border="0" /></a></td>
                    <td style="width: 70%;">
                        此处插入您的广告</td>
                </tr>
            </table>
        </div>
        <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
            <tr>
                <td style="padding: 5px 8px 5 5px;">
                    <div style="width: 98%;">
                        <span style="width: 70%;">新用户注册：填写注册项&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span
                            style="width: 30%; text-align: right;">有帐号了？<a href="Login.aspx"><font color="red">点这里登陆</font></a></span></div>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server" Height="36%" Width="100%">
            <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
                <tr class="TR_BG_list">
                    <td class="list_link" style="height: 213px; width: 950px;">
                        <%=agreement %>
                    </td>
                </tr>
                <tr class="TR_BG_list">
                    <td class="list_link" align="center" style="height: 24px; width: 950px;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="list_link" style="width: 50px; height: 20px;">
                                    <asp:Button ID="submit" runat="server" OnClick="submit_Click" Text="同  意" CssClass="form" /></td>
                                <td class="list_link" style="width: 40px; height: 20px;">
                                </td>
                                <td class="list_link" style="width: 50px; height: 20px;">
                                    <asp:Button ID="noBut" runat="server" Text="不同意" CssClass="form" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="SiteID" runat="server" />
            
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Height="36%" Width="100%" Visible="False">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </asp:Panel>
    </form>
    <br />
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
        style="height: 38px">
        <tr>
            <td align="center">
                <label id="copyright" runat="server" />
            </td>
        </tr>
    </table>
</body>
</html>

<script language="javascript" type="text/javascript">
function checkusername(username)
{
    
    //alert(escape(username));
	var  options={  
					   method:'get',  
					   parameters:"Action=checkusername&username="+escape(username),  
					   onComplete:function(transport)
						{  
							var returnvalue=transport.responseText;
							if (returnvalue.indexOf("??")>-1)
							    document.getElementById("div_content").innerHTML="Error";
							else
								document.getElementById("div_content").innerHTML=returnvalue;
						}  
				   }; 
	new  Ajax.Request('register.aspx?no-cache='+Math.random(),options);
} 

function chkpwd(obj){
	var t=obj.value;
	var id=getResult(t);
	
	//定义对应的消息提示
	var msg=new Array(4);
	msg[0]="密码过短。";
	msg[1]="密码强度差。";
	msg[2]="密码强度良好。";
	msg[3]="密码强度高。";
	
	var sty=new Array(4);
	sty[0]=-45;
	sty[1]=-30;
	sty[2]=-15;
	sty[3]=0;
	
	var col=new Array(4);
	col[0]="gray";
	col[1]="red";
	col[2]="#ff6600";
	col[3]="Green";
	
	//设置显示效果
	//var bImg="../sysImages/user/qd.gif";//一张显示用的图片
	var sWidth=300;
	var sHeight=15;
	var Bobj=document.getElementById("chkResult");

	Bobj.style.fontSize="12px";
	Bobj.style.color=col[id];
	Bobj.style.width=sWidth + "px";
	Bobj.style.height=sHeight + "px";
	Bobj.style.lineHeight=sHeight + "px";
	//Bobj.style.background="url(" + bImg + ") no-repeat left " + sty[id] + "px";
	Bobj.style.textIndent="20px";
	Bobj.innerHTML="检测提示：" + msg[id];
}

//定义检测函数,返回0/1/2/3分别代表无效/差/一般/强
function getResult(s){
	if(s.length < 4){
		return 0;
	}
	var ls = 0;
	if (s.match(/[a-z]/ig)){
		ls++;
	}
	if (s.match(/[0-9]/ig)){
		ls++;
	}
 	if (s.match(/(.[^a-z0-9])/ig)){
		ls++;
	}
	if (s.length < 6 && ls > 0){
		ls--;
	}
	return ls
}


function GetSubClass(CityId){
if($("citydiv")==null)
return;
	var url="/user/City_ajax.aspx";
	var Action="CityId="+CityId;
	var myAjax = new Ajax.Request(
		url,
		{method:"get",
		parameters:Action,
		onComplete:GetSubClassOk
		}
		);
}

	function GetSubClassOk(OriginalRequest){
	
	$("citydiv").innerHTML = OriginalRequest.responseText;
	
		
}

function GetRootClass(){
if($("citydiv")!=null){
    if( $("province")!=null)
    {
    var cid = $("province").options[$("province").selectedIndex].value;
    
	GetSubClass(cid);
	}
	else
	{
	    GetSubClass("-1");
	}
	}
}

//if($("citydiv")!=null)
//{

//    window.onload(GetRootClass());
//}
</script>


