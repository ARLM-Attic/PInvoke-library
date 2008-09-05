<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" Inherits="user_info_reviewGroup" Codebehind="reviewGroup.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<title></title>
<link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/usercss.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
function fTable()
{
    Form.disable(form1);
}
</script>
</head>
<body onload="fTable();">
<form id="form1" runat = "Server">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="57%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >查看会员权限</td>
          <td width="43%" height="32" class="topnavichar"  style="PADDING-LEFT: 14px" ><div align="left">位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a href="userinfo.aspx" class="list_link">我的资料</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" />查看您的权限</div></td>
        </tr>
</table>
     <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
       
       
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">会员组名</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="GroupName" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_usergroup_add_0001',this)">帮助</span></td>
          </tr>
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">需要积分</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="iPoint" runat="server" Text="0" MaxLength="4" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_usergroup_add_0002',this)">帮助</span></td>
          </tr>          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">需要G币</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="gPoint" runat="server" Text="0" MaxLength="4" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_usergroup_add_0003',this)">帮助</span></td>
          </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">折扣率</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="Discount" runat="server" Text="0" MaxLength="4" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_usergroup_add_Discount',this)">帮助</span></td>
          </tr>
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">有效期</div></td> 
          <td class="list_link"><asp:TextBox CssClass="form" ID="Rtime" runat="server" Text="0"  Width="250"  MaxLength="20"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_usergroup_add_0004',this)">帮助</span></td>
          </tr>
          
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">评论内容字数限制</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="LenCommContent" runat="server" Text="500" MaxLength="4" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_0005',this)">帮助</span></td>
        </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              评论需要审核</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="CommCheckTF" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_0006',this)">帮助</span></td>
        </tr> 
        
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">发表评论间隔时间(秒)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="PostCommTime" runat="server" Text="60"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_0007',this)">帮助</span></td>
        </tr>        
        
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px; height: 28px;"><div align="right">允许上传格式</div></td>
          <td class="list_link" style="height: 28px"><asp:TextBox CssClass="form" ID="upfileType" runat="server" Text="jpg,gif,bmp,png,swf,zip,rar"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_0008',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">上传文件个数(个)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="upfileNum" runat="server" Text="10" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_0009',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">单个文件大小(kb)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="upfileSize" runat="server"  Text="10" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00010',this)">帮助</span></td>
        </tr> 
        
         <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">每天最大上传数(个)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="DayUpfilenum" runat="server" Text="3"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00011',this)">帮助</span></td>
        </tr>
        
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">最多允许投稿数(篇)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="ContrNum" runat="server" Text="50" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00012',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              允许创建讨论组</div></td>
          <td class="list_link">
                <asp:RadioButtonList ID="DicussTF" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00013',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              允许发表主题</div></td>
          <td class="list_link">
                <asp:RadioButtonList ID="PostTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>          
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00014',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              查看其他会员资料</div></td>
          <td class="list_link">
                <asp:RadioButtonList ID="ReadUser" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>             
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00015',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              最大发送短消息数(条)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="MessageNum" runat="server" Text="100" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00016',this)">帮助</span></td>
        </tr> 
                
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              支持群发消息</div></td>
          <td class="list_link">
           <asp:TextBox ID="MessageGroupNum"  CssClass="form" runat="server"></asp:TextBox>
           
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00017',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              注册需要实名认证</div></td>
          <td class="list_link">
                <asp:RadioButtonList ID="IsCert" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>   
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00018',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              设置签名</div></td>
          <td class="list_link">
                <asp:RadioButtonList ID="CharTF" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>  
              <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00019',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              签名使用html语法</div></td>
          <td class="list_link">
                <asp:RadioButtonList ID="CharHTML" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>            
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00020',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">签名最大长度(字符)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="CharLenContent" MaxLength="3" runat="server" Text="500"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00021',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">注册多少分钟后允许发言</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="RegMinute" MaxLength="3" runat="server" Text="10"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00022',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              发言允许HTML编辑器</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="PostTitleHTML" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>          
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00023',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              删除自己的主题</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="DelSelfTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>           
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00024',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">删除其他人的帖子</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="DelOTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>            
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00025',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              编辑自己的发言</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="EditSelfTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>             
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00026',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              编辑他人帖子</div></td>
          <td class="list_link">
          
              <asp:RadioButtonList ID="EditOtitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>           
              <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00027',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">允许浏览发言</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="ReadTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>               
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00028',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              移动自己的帖子</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="MoveSelfTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>             
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00029',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              移动他人帖子</div></td>
          <td class="list_link">
          
              <asp:RadioButtonList ID="MoveOTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>             
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00030',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              解固/固顶帖子</div></td>
          <td class="list_link">
          
              <asp:RadioButtonList ID="TopTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>              
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00031',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">精华帖子操作</div></td>
          <td class="list_link">
          
              <asp:RadioButtonList ID="GoodTitle" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>            
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00032',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              锁定用户</div></td>
          <td class="list_link">
          
              <asp:RadioButtonList ID="LockUser" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>            
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00033',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">用户标识</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="UserFlag" runat="server" Text="FS_" Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00034',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              审核主题</div></td>
          <td class="list_link">
          
          
              <asp:RadioButtonList ID="CheckTtile" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>   
             <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00035',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              限制IP访问</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="IPTF" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>             
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00036',this)">帮助</span></td>
        </tr> 
                 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              对独立用户进行奖励/惩罚</div></td>
          <td class="list_link">
          
              <asp:RadioButtonList ID="EncUser" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>                    
              <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00037',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
              打开/关闭其它人帖子</div></td>
          <td class="list_link">
              <asp:RadioButtonList ID="OCTF" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>                    
          
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00038',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">允许用户选择风格</div></td>
          <td class="list_link">
          
              <asp:RadioButtonList ID="StyleTF" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>     
              <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00039',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">会员上传头像最大允许(kb)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="UpfaceSize" MaxLength="3" runat="server" Text="20"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00040',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">
             积分兑换金币/金币兑换积分</div></td>
          <td class="list_link">
            <asp:TextBox CssClass="form" ID="GIChange" MaxLength="3" runat="server" Text="0|1"  Width="250"></asp:TextBox>
                      
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00041',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">兑换比例</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="GTChageRate" MaxLength="30" Text="1000|1/10000" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00042',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">登陆时候获得的积分|G币</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="LoginPoint" runat="server" MaxLength="10" Text="5|0"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00043',this)">帮助</span></td>
        </tr> 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">注册时候获得的积分|G币</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="RegPoint" runat="server" MaxLength="10" Text="2|0"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00048',this)">帮助</span></td>
        </tr>                   
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">是否允许创建社群</div></td>
          <td class="list_link">
          
              <asp:RadioButtonList ID="GroupTF" runat="server" Width="73px" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
              </asp:RadioButtonList>   
                      
          
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00044',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">社群空间大小(kb)</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="GroupSize" runat="server" Text="2000"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00045',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">社群最大允许人数</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="GroupPerNum" Text="100" MaxLength="3" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00046',this)">帮助</span></td>
        </tr> 
        
                  
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">允许最大建立数量</div></td>
          <td class="list_link"><asp:TextBox CssClass="form" ID="GroupCreatNum" Text="3" MaxLength="2" runat="server"  Width="250"></asp:TextBox>
          <span class="helpstyle" style="cursor:help;"  title="点击查看帮助" onclick="Help('H_usergroup_add_00047',this)">帮助</span></td>
        </tr> 

</table>
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat="server" /></td>
   </tr>
 </table>   
    </form>
</body>
</html>
