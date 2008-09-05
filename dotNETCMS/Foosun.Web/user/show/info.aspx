<%@ Page Language="C#" AutoEventWireup="true" Inherits="user_show_info" Codebehind="info.aspx.cs" %>
<%@ Register Src="../../controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import NameSpace="System.Data"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title><%Response.Write(Foosun.Config.UIConfig.HeadTitle); %>__用户查看</title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/divcss.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat = "Server">
<asp:Panel ID="infobase" runat="server" Width="100%">

        <table width="98%" align="center" cellpadding="2" cellspacing="1">
        <tr>
          <td  class="list_link"><strong>基本信息</strong></td>
        </tr>
        <tr>
        <td  class="list_link">
        <table width="100%" cellpadding="4" cellspacing="1" class="table" align="center">
            <!--基本资料-->
            <!--用户名-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="height: 8px; width: 122px;">用户名</td>
              <td width="529" style="width: 529px">
                  <asp:Label ID="UserNamex" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label>&nbsp;&nbsp;&nbsp;<label id="levelsFace" runat="server" /></td>
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
            <tr class="TR_BG_list" runat="server" id="mobileTF">
              <td  class="list_link" style="width: 122px">手机</td>
              <td  class="list_link" style="width: 529px">
              <asp:Label ID="Mobilex" runat="server" Width="100%" Height="100%"></asp:Label>
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
              <td  class="list_link" style="width: 529px"><asp:Label ID="UserGroupNumberx" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label>
              </td>
            </tr>
            <!--结婚-->
            <tr class="TR_BG_list" runat="server" id="marriTF">
              <td  class="list_link" style="width: 122px">婚姻状况</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="marriagex" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label></td>
            </tr>
            <!--职业-->
            <tr class="TR_BG_list">
              <td  class="list_link" style="width: 122px">职业</td>
              <td  class="list_link" style="width: 529px"><asp:Label ID="Jobx" runat="server" Width="100%" Height="100%" CssClass="list_link"></asp:Label></td>
            </tr>
            </table>
        </td>
        </tr>
        </table>
        
        <table width="98%" align="center" cellpadding="2" cellspacing="1">
        <tr>
              <td  class="list_link"><strong>基本状态</strong></td>
            </tr>
            <tr>
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
              <td  class="list_link"><asp:Label ID="iPointx" runat="server" Width="100%" Height="100%"></asp:Label></td>
              <td  class="list_link">用户在线时间</td><!--用户在线时间-->
              <td  class="list_link"><asp:Label ID="OnlineTimex" runat="server" Width="100%" Height="100%"></asp:Label></td>
            </tr>
            <tr class="TR_BG_list">
              <td  class="list_link">金币</td><!--金币-->
              <td  class="list_link"><asp:Label ID="gPointx" runat="server" Width="100%" Height="100%"></asp:Label> <label id="ePointName" runat="server" /></td>
              <td  class="list_link">人气值</td><!--用户在线状态-->
              <td  class="list_link"><asp:Label ID="ePointx" runat="server" Width="100%" Height="100%"></asp:Label></td>
            </tr>
            <tr class="TR_BG_list">
              <td  class="list_link">魅力值</td> <!--魅力值-->
              <td  class="list_link"><asp:Label ID="cPointx" runat="server" Width="100%" Height="100%"></asp:Label></td>
              <td  class="list_link">用户登陆次数</td><!-- 用户登陆次数-->
              <td  class="list_link"><asp:Label ID="LoginNumberx" runat="server" Width="100%" Height="100%"></asp:Label></td>
            </tr>
            </table>
            </td>
            </tr>
           </table>
            <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" runat="server" id="otherTF">
            <tr>
              <td  class="list_link"><strong>其他信息</strong></td>
            </tr>
            <tr>
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
             </table>
            <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" runat="server" id="linkTF">
            <tr>
              <td class="list_link"><strong>联系资料</strong></td>
            </tr>
            <tr>
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

    </asp:Panel>
     <asp:Panel ID="Constrlist" runat="server" Width="100%" Visible="False">
            <div style="padding-top:5px;">
            <span id="contentClass" style="padding-left:14px;width:98%;" runat="server"></span>位置：文章列表&nbsp;&nbsp;&nbsp;<a href="../index.aspx?urls=Constr/Constr.aspx" target="_blank"><img alt="写文章" src="../../sysImages/folder/writearcle.gif" border="0" /></a></div>
            <asp:Repeater ID="DataList1" runat="server" >
            <HeaderTemplate>
                <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
                <tr class="TR_BG">
                <td class="sys_topBg" align="left" style="width:50%;">标题</td>
                <td class="sys_topBg" align="center">分类</td>
                <td class="sys_topBg" align="center">添加时间</td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
                <td align="left" style="width:50%;"><a class="list_link" href="showcontent.aspx?ConID=<%#((DataRowView)Container.DataItem)["ConID"]%>&uid=<%Response.Write(Request.QueryString["uid"]); %>&ClassID=<%#((DataRowView)Container.DataItem)["ClassID"]%>"><%#((DataRowView)Container.DataItem)["Title"]%></a></a></td>
                <td align="center"><a class="list_link" href="info.aspx?s=content&uid=<%Response.Write(Request.QueryString["uid"]); %>&ClassID=<%#((DataRowView)Container.DataItem)["ClassID"]%>"><%#((DataRowView)Container.DataItem)["cNames"]%></a></td>
                <td align="center"><%#((DataRowView)Container.DataItem)["creatTime"]%></td>
                </tr>
             </ItemTemplate>
             <FooterTemplate>
             </table>
             </FooterTemplate>
            </asp:Repeater>
                <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
                <tr><td align="right" style="width: 928px">
                    <uc1:PageNavigator ID="PageNavigator1" runat="server" />
                </td></tr>
                </table>  
         
        </asp:Panel>
    <asp:Panel ID="Photolist" runat="server" Width="100%"  Visible="False">
    <asp:DataList ID="DataList2" runat="server" RepeatColumns="5" Width="98%">
                <ItemTemplate>
                    <table style="height:160px;width:140px;" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
                     <tr class="TR_BG_list">
                        <td align="center"><%#((DataRowView)Container.DataItem)["Pic"]%></td>
                    </tr>
                    <tr class="TR_BG_list">
                        <td align="center"><%#((DataRowView)Container.DataItem)["PhotoalbumNames"]%><%#((DataRowView)Container.DataItem)["picnum"]%>&nbsp;<%#((DataRowView)Container.DataItem)["pwds"]%></td>
                    </tr>
                 </table>
             </ItemTemplate>             
         </asp:DataList>
                         <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
                <tr><td align="right" style="width: 928px">
                    <uc1:PageNavigator ID="PageNavigator2" runat="server" />
                </td></tr>
                </table>  
    </asp:Panel>    
    
    <asp:Panel ID="infogroup" runat="server" Width="100%" Visible="False">
      <asp:Repeater ID="Repeater2" runat="server" >
    <HeaderTemplate>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
        <tr class="TR_BG">
        <td class="sys_topBg" align="left" width="60%">名称(人数)</td>
        <td class="sys_topBg" align="center" width="20%">创建时间</td>
        <td class="sys_topBg" align="center" width="20%">操作</td>
        </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="TR_BG_list" onmouseover="overColor(this)" onmouseout="outColor(this)">
        <td align="left" width="60%"><a class="list_link" title="进入：<%#((DataRowView)Container.DataItem)[2]%>" href="../discuss/discussTopi_list.aspx?DisID=<%#((DataRowView)Container.DataItem)["DisID"]%>"><%#((DataRowView)Container.DataItem)[2]%>(<%#((DataRowView)Container.DataItem)[5]%>)</a></td>
        <td align="center" width="20%"><%#((DataRowView)Container.DataItem)[4]%></td>
        <td align="center" width="20%"><a class="list_link" href="../discuss/discuss_Manageadd.aspx?DisID=<%#((DataRowView)Container.DataItem)["DisID"]%>">加入</a></td>
        </tr>
     </ItemTemplate>
     <FooterTemplate>
     </table>
     </FooterTemplate>
    </asp:Repeater>
        <table width="98%" border="0" align="center" cellpadding="3" cellspacing="2" style="height: 20px">
            <tr>
                <td align="right" style="width: 928px">
                    <uc1:PageNavigator ID="PageNavigator3" runat="server" />
                </td>
            </tr>
        </table>
     </asp:Panel>

    <asp:Panel ID="bbslist" runat="server" Width="100%" Visible="False">
       
     </asp:Panel>
     
     
     <asp:Panel ID="infolink" runat="server" Width="100%" Visible="False">
        <table width="98%" border="0" align="center" cellpadding="8" cellspacing="1" class="table">
        <tr class="TR_BG_list">
          <td>
            <label id="linkList" runat="server" />
          </td>
        </tr>
        </table>
     </asp:Panel>
        
     <br />
     <br />
  <table width="100%" style="height:74px;" border="0" cellpadding="0" cellspacing="0" class="copyright_bg">
    <tr>
      <td  class="list_link" align="center"><label id="copyright" runat="server" /></td>
    </tr>
  </table>
</form>
</body>
</html>