<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" AutoEventWireup="true" Inherits="user_userinfo" Debug="true" Codebehind="userinfo.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat = "Server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >会员资料</td>
          <td width="43%" height="32" class="list_link"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userinfo.aspx" class="list_link">我的资料</a></div></td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
        <tr>
          <td style="padding-left:14px;"><a class="list_link" href="userinfo.aspx"><font color="red">我的资料</font></a>　<a class="list_link" href="userinfo_update.aspx">修改基本信息</a>　<a class="list_link" href="userinfo_contact.aspx">修改联系资料</a>　<a class="list_link" href="userinfo_safe.aspx">修改安全资料</a>　<a class="list_link" href="userinfo_idcard.aspx">修改实名认证</a></td>
        </tr>
    </table>
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr class="TR_BG_list">
      <td height="217" valign="top" style="width: 933px">
        <table width="100%" cellpadding="2" cellspacing="1">
        <tr class="TR_BG_list">
          <td  class="list_link"><strong>基本信息</strong></td>
        </tr>
        <tr class="TR_BG_list">
        <td  class="list_link">
        <table width="100%" cellpadding="4" cellspacing="1" class="table" align="center">
            <!--基本资料-->
            <!--用户名-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="height: 8px; width: 122px;">用户名</td>
              <td width="529" style="width: 529px">
                  <asp:Label ID="UserNamex" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<label id="levelsFace" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label id="Reviewmyfinfo" runat="server" /></td>
              <td rowspan="9" width="192" align="center"><asp:Image ID="UserFacex" runat="server" ImageUrl="~/sysImages/user/noHeadpic.gif" /></td>
            </tr>
            <!--会员昵称-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">昵称</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="NickNamex" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label></td>
            </tr>
            <!--会员真实姓名-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">真实姓名</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="RealNamex" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label></td>
            </tr>
            <!--会员性别-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">手机</td>
              <td  class="list_link" style="width: 529px">
              <asp:Label ID="Mobilex" runat="server" Width="100%" Height="100%"></asp:Label>
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_getMobile_0001',this)">怎么捆绑手机/小灵通?</span>
              </td>
            </tr>
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">性别</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="Sexx" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label></td>
            </tr>
            
              
            <!--出生日期-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">出生日期</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="birthdayx" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label></td>
            </tr>
            <!--会员所属于的会员组-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">
                  会员组</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="UserGroupNumberx" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label>&nbsp
              <span style="display:none;">
              <label id="reviewGroup" runat="server" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_getgroupUpdate',this)">我要怎么升级</span>
              </span>
              </td>
            </tr>
            <!--结婚-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">婚姻状况</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="marriagex" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label></td>
            </tr>
            <!--职业-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">职业</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="Jobx" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label></td>
            </tr>
            </table></td></tr>
            <tr class="TR_BG_list">
              <td  class="list_link"><strong>基本状态</strong></td>
            </tr>
            <tr class="TR_BG_list">
            <td  class="list_link">
           <table width="100%" cellpadding="4" cellspacing="1" class="table" align="center">
            <!--用户签名-->
            
            <tr class="TR_BG_list">
              <td width="15%" class="list_link">活跃值</td><!--城市-->
              <td width="35%"  class="list_link"><asp:Label ID="aPointx" runat="server" Width="100%" Height="100%"></asp:Label></td>
              <td width="15%"  class="list_link">注册日期</td><!--注册日期-->
              <td width="35%"  class="list_link"><asp:Label ID="RegTimex" runat="server" Width="100%" Height="100%"></asp:Label></td>
            </tr>
            <tr class="TR_BG_list">
              <td  class="list_link">积分</td><!-- 积分-->
              <td  class="list_link"><asp:Label ID="iPointx" runat="server" Width="100%" Height="100%"></asp:Label>&nbsp;&nbsp;<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_iPoint_0001',this)">如何兑换积分/金币?</span></td>
              <td  class="list_link">用户在线时间</td><!--用户在线时间-->
              <td  class="list_link"><asp:Label ID="OnlineTimex" runat="server" Width="100%" Height="100%"></asp:Label></td>
            </tr>
            <tr class="TR_BG_list">
              <td  class="list_link">金币</td><!--金币-->
              <td  class="list_link"><asp:Label ID="gPointx" runat="server" Width="100%" Height="100%"></asp:Label> <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_gPoint_0001',this)">帮助</span></td>
              <td  class="list_link">人气值</td><!--用户在线状态-->
              <td  class="list_link"><asp:Label ID="ePointx" runat="server" Width="100%" Height="100%"></asp:Label></td>
            </tr>
            <tr class="TR_BG_list">
              <td  class="list_link">魅力值</td> <!--魅力值-->
              <td  class="list_link"><asp:Label ID="cPointx" runat="server" Width="100%" Height="100%"></asp:Label></td>
              <td  class="list_link">用户登陆次数</td><!-- 用户登陆次数-->
              <td  class="list_link"><asp:Label ID="LoginNumberx" runat="server" Width="100%" Height="100%"></asp:Label></td>
            </tr>
            </table></td></tr>
            <tr class="TR_BG_list">
              <td  class="list_link"><strong>其他信息</strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td  class="list_link"><table width="100%" cellpadding="4" cellspacing="1" class="table" align="center">
                <!--详细资料-->
                <tr class="TR_BG_list">
                  <td width="15%"  class="list_link">民族</td>
                  <!-- 民族-->
                  <td width="35%"><asp:Label ID="Nationx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <td width="15%" class="list_link">电子邮件</td>
                  <!--用户爱好-->
                  <td width="35%"><asp:Label ID="Emailx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                <tr class="TR_BG_list">
                  <td  class="list_link">籍贯</td>
                  <!-- 籍贯-->
                  <td  class="list_link"><asp:Label ID="nativeplacex" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <td  class="list_link">学历</td>
                  <!--学历-->
                  <td  class="list_link"><asp:Label ID="educationx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                <tr class="TR_BG_list">
                  <td  class="list_link">组织关系</td>
                  <!--组织关系-->
                  <td  class="list_link"><asp:Label ID="orgSchx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <td  class="list_link">毕业学校</td>
                  <!--毕业学校-->
                  <td  class="list_link"><asp:Label ID="Lastschoolx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                <tr class="TR_BG_list">
                  <td  class="list_link">性格</td>
                  <!--性格-->
                  <td colspan="3"  class="list_link"><asp:Label ID="characterx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <!-- 电子邮件-->
                </tr>
                <tr class="TR_BG_list">
                  <td  class="list_link">用户签名</td>
                  <td colspan="3"  class="list_link"><asp:Label ID="Userinfox" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                <tr class="TR_BG_list">
                  <td  class="list_link">用户爱好</td>
                  <td colspan="3"  class="list_link"><asp:Label ID="UserFanx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                
              </table></td>
            </tr>
            
            <tr class="TR_BG_list">
              <td  class="list_link"><strong>联系资料</strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td  class="list_link"><table width="100%" cellpadding="4" cellspacing="1" class="table" align="center">
                <!--联系方式-->
                <tr class="TR_BG_list">
                  <td  class="list_link">省份</td>
                  <td><asp:Label ID="provincex" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <td class="list_link">城市</td>
                  <td><asp:Label ID="Cityx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                <tr class="TR_BG_list">
                  <td width="15%"  class="list_link">地址</td>
                  <!-- 地址-->
                  <td width="35%"><asp:Label ID="Addressx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <td width="15%" class="list_link">MSN</td>
                  <!--手机-->
                  <td width="35%"><asp:Label ID="MSNx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                <tr class="TR_BG_list">
                  <td  class="list_link">邮政编码</td>
                  <!--邮政编码-->
                  <td  class="list_link"><asp:Label ID="Postcodex" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <td  class="list_link">QQ</td>
                  <!--传真-->
                  <td  class="list_link"><asp:Label ID="QQx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                <tr class="TR_BG_list">
                  <td  class="list_link">家庭联系电话</td>
                  <!-- 家庭联系电话-->
                  <td  class="list_link"><asp:Label ID="FaTelx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <td  class="list_link">传真</td>
                  <!-- QQ-->
                  <td  class="list_link"><asp:Label ID="Faxx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                </tr>
                <tr class="TR_BG_list">
                  <td class="list_link">工作单位联系电话</td>
                  <!--工作单位联系电话-->
                  <td  class="list_link"><asp:Label ID="WorkTelx" runat="server" Width="100%" Height="100%"></asp:Label></td>
                  <td  class="list_link"></td>
                  <!--MSN-->
                  <td  class="list_link"></td>
                </tr>
              </table></td>
            </tr>
        </table>
     </td>
    </tr>
  </table>
  <table width="100%" height="74" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
    <tr>
      <td  class="list_link" align="center"><label id="copyright" runat=server /></td>
    </tr>
  </table>
</form>
</body>
<script language="javascript" type="text/javascript">
	function ChangeDiv(ID)
	{
		document.getElementById('td_Fundamental').className='m_up_bg';
		document.getElementById('td_Detailed').className='m_up_bg';
		document.getElementById('td_contact').className='m_up_bg';
		document.getElementById("td_"+ID).className='m_down_bg';

		document.getElementById("div_Fundamental").style.display="none";
		document.getElementById("div_Detailed").style.display="none";
		document.getElementById("div_contact").style.display="none";
		document.getElementById("div_"+ID).style.display="";
	}
</script>
</html>
