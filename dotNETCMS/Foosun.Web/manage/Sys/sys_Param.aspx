<%@ Page Language="C#" AutoEventWireup="true" Inherits="sys_Param" Codebehind="sys_Param.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/public.js"></script>
</head>
<body>
<form id="SetParam" runat="server" onclick="check()">
  <iframe width="260" height="165" id="colorPalette" src="../../configuration/system/selcolor.htm" style="visibility:hidden; position: absolute;border:1px gray solid; left: 31px; top: 140px;" frameborder="0" scrolling="no" ></iframe>
  <table style="height:40px;width: 100%" border="0" cellpadding="0" cellspacing="0"class="toptable">
    <tr>
      <td height="1" colspan="2"></td>
    </tr>
    <tr>
      <td width="57%" class="sysmain_navi"  style="PADDING-LEFT: 14px" height="30">系统参数设置</td>
      <td width="43%"  class="topnavichar"  style="PADDING-LEFT: 14px" >位置：<a href="../main.aspx" class="navi_link">首页</a> <img alt="" src="../../sysImages/folder/navidot.gif" border="0" /> 系统参数设置</td>
    </tr>
  </table>
  <table border="0" cellpadding="5" cellspacing="1" class="Navitable" style="width: 100%">
    <tr class="TR_BG_list">
    <td style="padding-left:14px;">
      <span style="cursor:pointer;width:100px;" id="td_baseinfo"  onclick="javascript:ChangeDiv('baseinfo')">基本属性</span>&nbsp;┊ &nbsp;
      <span style="cursor:pointer;width:100px;" id="td_user" onclick="javascript:ChangeDiv('user')">会员参数</span>&nbsp;┊ &nbsp;
      <span id="td_upload" style="cursor:pointer;width:100px;" onclick="javascript:ChangeDiv('upload')">上传.分组刷新</span>&nbsp;┊ &nbsp;
      <span id="td_js" style="cursor:pointer;width:100px;" onclick=";//javascript:ChangeDiv('js')" title="此版本未开放">FTP设置</span>&nbsp;┊ &nbsp;
      <span id="td_water" style="cursor:pointer;width:100px;" onclick="javascript:ChangeDiv('water')">水印缩图</span>&nbsp;┊ &nbsp;
      <span id="td_rssxmlwap"  style="cursor:pointer;width:100px;" onclick="javascript:ChangeDiv('rssxmlwap')">RSS.XML.WAP</span>&nbsp; &nbsp;
      <span id="td_api" style="cursor:pointer;display:none;" onclick="javascript:ChangeDiv('api')">API参数</span>
    </td>
    </tr>
    </table>
  <table border="0" cellpadding="0" align="center" cellspacing="0" style="width: 98%">
    <tr class="TR_BG_list">
      <td colspan="9" valign="top" class="list_link" style="width: 954px">
      <div id="div_baseinfo" style="display:block" align="center">
          <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">基本参数设置</span></strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link" style="width:25%">站点名称：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="SiteName" runat="server" CssClass="form"/><span id="SiteName_Span" runat="server"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_BaseParam_0001',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link">站点域名：</td>
              <td align="left" class="list_link"><asp:TextBox Width="250" ID="SiteDomain" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_BaseParam_0002',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link">首页模板路径：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="IndexTemplet" runat="server" CssClass="form"/>
                <label>
                <img  alt="选择模板" src="../../sysImages/folder/s.gif" style="cursor:pointer;" name="IndexTempletClick" title=" 选择摸板 " id="IndexTempletClick"  onclick="selectFile('templet',document.SetParam.IndexTemplet,280,500);document.SetParam.IndexTemplet.focus();" />
                </label>
              </td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link"> 首页生成的文件名：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="IndexFileName" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_BaseParam_0003',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" style="display:none;">
              <td align="right" class="list_link"> 默认的扩展名为（主站）：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="FileEXName" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_BaseParam_0004',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link"> 默认的新闻浏览页模板：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="ReadNewsTemplet" runat="server"  CssClass="form"/>
                <label>
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" name="ReadNewsTempletClick" title=" 选择摸板 " id="ReadNewsTempletClick" onclick="selectFile('templet',document.SetParam.ReadNewsTemplet,280,500);document.SetParam.ReadNewsTemplet.focus();"/>
                </label>
              </td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link"> 默认的栏目浏览页模板：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="ClassListTemplet" runat="server"  CssClass="form"/>
                <label>
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" name="ClassListTempletClick" title=" 选择摸板 " id="ClassListTempletClick" onclick="selectFile('templet',document.SetParam.ClassListTemplet,280,500);document.SetParam.ClassListTemplet.focus();"/>
                </label>
              </td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link"> 默认的专题浏览页模板：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="SpecialTemplet" runat="server"  CssClass="form"/>
                <label>
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" name="SpecialTempletClick" title=" 选择摸板 " id="SpecialTempletClick" onclick="selectFile('templet',document.SetParam.SpecialTemplet,280,500);document.SetParam.SpecialTemplet.focus();"/>
                </label>
              </td>
            </tr>            
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">前台浏览方式：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="DongTai" runat="server" Text="动态调用" GroupName="ReadType" />&nbsp;<asp:RadioButton ID="JingTai" runat="server" Text="静态调用" GroupName="ReadType" /></td>
            </tr>
            
            <tr class="TR_BG_list" style="display:none;">
              <td align="right" class="list_link" style="height: 15px"> 管理列表每页显示记录条数：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="Pram_Index" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_BaseParam_0015',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link" style="height: 15px"> 后台登陆过期时间：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="LoginTimeOut" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_BaseParam_0005',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link"> 管理员信箱：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="Email" runat="server" CssClass="form"/></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">站点采用路径方式：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="JueDui" runat="server" Text="绝对路径" GroupName="LinkType" />&nbsp;<asp:RadioButton ID="XiangDui" runat="server" Text="相对路径" GroupName="LinkType" /></td>
            </tr>
            
            <tr class="TR_BG_list">
              <td align="right" class="list_link"> 版权信息：<br />
                (支持html格式)</td>
              <td  align="left" class="list_link"><textarea Width="250" id="BaseCopyRight" name="BaseCopyRight" runat="server" tabindex="0" style="width: 313px; height: 110px" class="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_BaseParam_0006',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link">新闻后台审核机制：</td>
              <td  align="left" class="list_link"><asp:DropDownList ID="CheckInt" runat="server">
                  <asp:ListItem Selected="True" Value="0">不审核</asp:ListItem>
                  <asp:ListItem Value="1">一级审核</asp:ListItem>
                  <asp:ListItem Value="2">二级审核</asp:ListItem>
                  <asp:ListItem Value="3">三级审核</asp:ListItem>
                </asp:DropDownList></td>
            </tr>  
           <tr class="TR_BG_list">
              <td align="right"  class="list_link"> 生成归档索引生成多少天前索引：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="HistoryNum" runat="server" MaxLength="3" Text="30" CssClass="form"/><span id="Span4" runat="server" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_BaseParam_HistoryNum',this)">帮助</span> </td>
            </tr>     
                   
           <tr class="TR_BG_list">
              <td align="right"  class="list_link"> 画中画插入新闻内容中的位置：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="InsertPicPosition" runat="server" Text="200|left" CssClass="form"/><span id="Span3" runat="server" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_BaseParam_InsertPicPosition',this)">帮助</span> </td>
            </tr>
            <tr class="TR_BG_list" style="display:none;">
              <td  align="right" class="list_link">是否开启图片防盗链：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="Yes" runat="server" Text="开启" GroupName="UnLinkTF" />&nbsp;<asp:RadioButton ID="No" runat="server" Text="不开启" GroupName="UnLinkTF" /></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link"> 搜索关键字长度：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="LenSearch" runat="server" CssClass="form"/><span id="keyLength" runat="server" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_BaseParam_0007',this)">帮助</span> </td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">添加新闻检查是否有相同标题：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="checktitle" runat="server" Text="检测" GroupName="CheckNewsTitle" />&nbsp;<asp:RadioButton ID="nochecktitle" runat="server" Text="不检测" GroupName="CheckNewsTitle" /></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">是否开启防采集：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="Ischeck" runat="server" Text="开启" GroupName="CollectTF" />&nbsp;<asp:RadioButton ID="nocheck" runat="server" Text="不开启" GroupName="CollectTF" /></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link">生成栏目文件保存路径：</td>
              <td align="left" class="list_link"><asp:TextBox ID="SaveClassFilePath" runat="server"  Width="250" CssClass="form" />
                &nbsp;
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择路径"  onclick="selectFile('path|<% Response.Write(Foosun.Config.UIConfig.dirHtml); %>',document.SetParam.SaveClassFilePath,280,500);document.SetParam.SaveClassFilePath.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_BaseParam_0008',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link">生成索引页的规则：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="SaveIndexPage" runat="server"  Width="250" CssClass="form" />
                &nbsp;
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择规则"  onclick="selectFile('rulesmallPram',document.SetParam.SaveIndexPage,100,500);document.SetParam.SaveIndexPage.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_BaseParam_0009',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link">生成新闻的文件命名规则：</td>
              <td align="left" class="list_link"><asp:TextBox ID="SaveNewsFilePath" runat="server"  Width="250px" CssClass="form"/>
                &nbsp;
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择规则"  onclick="selectFile('rulePram',document.SetParam.SaveNewsFilePath,100,500);document.SetParam.SaveNewsFilePath.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_BaseParam_0010',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link">生成新闻的文件保存路径规则：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="SaveNewsDirPath" runat="server"  Width="250px" CssClass="form"/>
                &nbsp;
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择规则"  onclick="selectFile('rulesmallPram',document.SetParam.SaveNewsDirPath,100,500);document.SetParam.SaveNewsDirPath.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_BaseParam_0011',this)">帮助</span></td>
            </tr>
              <tr class="TR_BG_list">
                  <td align="right" class="list_link">
                      是否开启内容自动分页：</td>
                  <td align="left" class="list_link">
                      <asp:RadioButton ID="RadioButton_autoPageSplit1" runat="server" GroupName="autoPageSplit"
                          Text="开启" />
                      <asp:RadioButton ID="RadioButton_autoPageSplit2" runat="server" GroupName="autoPageSplit"
                          Text="不开启" /></td>
              </tr>
              <tr class="TR_BG_list">
                  <td align="right" class="list_link">
                      内容分页字数：</td>
                  <td align="left" class="list_link">
                      <asp:TextBox ID="txt_pageSplitCount" runat="server" Width="250px"></asp:TextBox></td>
              </tr>
            <tr class="TR_BG_list">
                  <td align="right" class="list_link">
                      内容分页样式：</td>
                  <td align="left" class="list_link">
                  <select name="PageStyle" runat="server" style="width:150px;" id="NewsPageStyle">
                    <option value="0" selected="selected">前一页 后一页</option>
                    <option value="1">共N页,第1页,第2页</option>
                    <option value="2">共N页.1 2 3</option>
                    <option value="3"> |<<  <<  <  > > >>  >>| </option>
                </select></td>
              </tr>
              <tr class="TR_BG_list">
                  <td align="right" class="list_link">
                      &nbsp;分页显示链接个数：</td>
                  <td align="left" class="list_link">
                      <asp:TextBox ID="PageLinkCount" runat="server" Width="250px"></asp:TextBox>&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PageLinkCount"
                          ErrorMessage="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PageLinkCount"
                          ErrorMessage="只能输入数字" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
              </tr>
            <tr class="TR_BG_list">
                  <td align="right" class="list_link">
                      发布新闻方式：</td>
                  <td align="left" class="list_link">
                  <input name="radPublishState" checked="true" id="radPublishOne" type="radio" value="快速发布" runat="server" />快速发布<input id="radPublishTwo" name="radPublishState" type="radio" value="节省CPU" runat="server" />节省CPU
                  </td>
              </tr>
            <tr class="TR_BG_list">
              <td align="center" colspan="2" class="list_link"><label>
                <input type="submit" name="Savebaseinfo" value=" 提 交 " class="form" id="SaveBaseInfo" runat="server" onserverclick="SaveBaseInfo_ServerClick"/>
                </label>
                &nbsp;&nbsp;
                <label>
                <input type="reset" name="Clearbaseinfo" value=" 重 填 " class="form" id="ClearBaseInfo" runat="server"/>
                </label></td>
            </tr>
          </table>
        </div>
        <div id="div_user" style="display:none" align="center">
          <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">会员参数设置</span></strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width:25%">会员注册默认会员组：</td>
              <td  align="left" class="list_link"><asp:DropDownList ID="RegGroupNumber" runat="server"></asp:DropDownList>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0001',this)">帮助</span></td>
            </tr>
             <tr class="TR_BG_list">
              <td  align="right" class="list_link">投稿状态：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="IsConstrTF" runat="server" Text="开启" GroupName="ConstrTF" />&nbsp;<asp:RadioButton ID="NoConstrTF" runat="server" Text="不开启" GroupName="ConstrTF" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0002',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">是否允许注册：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="RegYes" runat="server" Text="允许" GroupName="RegTF" />&nbsp;<asp:RadioButton ID="RegNo" runat="server" Text="不允许" GroupName="RegTF" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0003',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">会员登陆是否需要验证码：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="codeyes" runat="server" Text="需要" GroupName="UserLoginCodeTF" />&nbsp;<asp:RadioButton ID="codeno" runat="server" Text="不需要" GroupName="UserLoginCodeTF" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0004',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" style="display:none;">
              <td  align="right" class="list_link">会员评论是否需要验证码：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="dis" runat="server" Text="需要" GroupName="CommCodeTF" />&nbsp;<asp:RadioButton ID="nodis" runat="server" Text="不需要" GroupName="CommCodeTF" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0005',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">评论需要审核：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="CommCheck1" runat="server" Text="需要" GroupName="CommCheck" />&nbsp;<asp:RadioButton ID="CommCheck0" runat="server" Text="不需要" GroupName="CommCheck" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_CommCheck',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" style="display:none;">
              <td  align="right" class="list_link">是否开启群发功能：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="sendall" runat="server" Text="开启" GroupName="SendMessageTF" />&nbsp;<asp:RadioButton ID="sendone" runat="server" Text="不开启" GroupName="SendMessageTF" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0006',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">是否允许匿名评论：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="yun" runat="server" Text="开启" GroupName="UnRegCommTF" />&nbsp;<asp:RadioButton ID="noyun" runat="server" Text="不开启" GroupName="UnRegCommTF" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0007',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" style="display:none;">
              <td  align="right" class="list_link">评论是否需要加载html编辑器：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="html" runat="server" Text="需要" GroupName="CommHTMLLoad" />&nbsp;<asp:RadioButton ID="nohtml" runat="server" Text="不需要" GroupName="CommHTMLLoad" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0008',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link" style="width: 244px"> 评论过滤字符：</td>
              <td align="left" class="list_link" style="width: 568px"><textarea id="Commfiltrchar"  runat="server" style="width: 500px; height: 96px" class="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0009',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 244px"> 会员IP登陆限制：</td>
              <td  align="left" class="list_link" style="width: 568px"><textarea id="IPLimt" runat="server" style="width: 500px; height: 113px" class="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0010',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 244px"> 会员G币单位：</td>
              <td  align="left" class="list_link" style="width: 568px"><asp:TextBox ID="GpointName" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0011',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 244px"> 会员注册获得的金币|积分：</td>
              <td  align="left" class="list_link" style="width: 568px"><asp:TextBox ID="setPoint" runat="server" CssClass="form"/><span id="Point_Span" runat="server" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0012',this)">帮助</span></td>
            </tr>
             <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 244px"> 魅力值增加：</td>
              <td  align="left" class="list_link" style="width: 568px"><asp:TextBox ID="cPointParam" runat="server" CssClass="form"/><span id="Span1" runat="server" /><span id="MeiL" runat="server" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0017',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 244px"> 活跃值增加：</td>
              <td  align="left" class="list_link" style="width: 568px"><asp:TextBox ID="aPointparam" runat="server" CssClass="form"/><span id="Span2" runat="server" /><span id="Huo" runat="server" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0018',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 244px">会员冲值类型：</td>
              <td  align="left" class="list_link" style="width: 568px"><asp:RadioButton ID="JiFen" runat="server" Text="冲值为积分" GroupName="MoneyType"  Checked="True"/>            
                    <asp:RadioButton ID="GB" runat="server" Text="冲值为金币" GroupName="MoneyType"/>              
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0020',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 244px"> 错误登陆次数|锁定时间：</td>
              <td  align="left" class="list_link" style="width: 568px"><asp:TextBox ID="LoginLock" runat="server" CssClass="form"/><span id="Login_Span" runat="server" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0013',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 244px"> 会员注册需知：<br />
                (支持html格式)</td>
              <td  align="left" class="list_link" style="width: 568px"><textarea id="RegContent" runat="server" style="width: 500px; height: 175px" class="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_0014',this)">帮助</span></td>
            </tr>
            
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">会员注册设置</span></strong></td>
            </tr>
             <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">选择需要注册的参数</div></td> 
          <td class="list_link" align="left"><asp:TextBox CssClass="form" ID="regitemContent" runat="server" Width="500" Height="60px" TextMode="MultiLine"></asp:TextBox>
          <input type="button" name="t234" value="清空" onclick="clears();" CssClass="form" />
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_registerParam_0001',this)">帮助</span>    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="数据不能为空" ControlToValidate="regitemContent"></asp:RequiredFieldValidator>
         </td>
       </tr>      
 
         <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px; height: 26px;"><div align="right">选择参数</div></td> 
          <td class="list_link" style="height: 26px"  align="left"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td>
                <input type="checkbox" id="ChbUName" value="UserName"　name="regitem" onclick="ay(this);"  />
              用户名</td>
              <td>
                <input type="checkbox" value="UserPassword"　name="regitem" onclick="ay(this);"  />
              密    码</td>
              <td>
                <input type="checkbox" value="email"　name="regitem" onclick="ay(this);"  />
              电子邮件</td>
              <td>
                <input type="checkbox" value="PassQuestion"　name="regitem" onclick="ay(this);"  />
              密码问题</td>
              <td>
                <input type="checkbox" value="PassKey"　name="regitem" onclick="ay(this);"  />
              密码答案</td>
              <td>
                <input type="checkbox" value="RealName"　name="regitem" onclick="ay(this);"  />
              真实姓名</td>
              <td>
                <input type="checkbox" value="NickName"　name="regitem" onclick="ay(this);"  />
              昵称</td>
            </tr>
            <tr>
              <td>
                <input type="checkbox" value="CertType"　name="regitem" onclick="ay(this);"  />
              证件类型</td>
              <td>
                <input type="checkbox" value="CertNumber"　name="regitem" onclick="ay(this);"  />
              证件号码</td>
              <td>
                <input type="checkbox" value="province"　name="regitem" onclick="ay(this);"  />
              省份</td>
              <td>
                <input type="checkbox" value="City"　name="regitem" onclick="ay(this);"  />
              城市</td>
              <td>
                <input type="checkbox" value="Address"　name="regitem" onclick="ay(this);"  />
              地址</td>
              <td>
                <input type="checkbox" value="Postcode"　name="regitem" onclick="ay(this);"  />
              邮政编码</td>

            </tr>
            <tr>
              <td>
                <input type="checkbox" value="Mobile"　name="regitem" onclick="ay(this);"  />
              手机</td>
              <td>
                <input type="checkbox" value="Fax"　name="regitem" onclick="ay(this);"  />
              传真</td>
              <td>
                <input type="checkbox" value="WorkTel"　name="regitem" onclick="ay(this);"  />
              工作电话</td>
              <td>
                <input type="checkbox" value="FaTel"　name="regitem"  onclick="ay(this);" />
              家庭电话</td>
              <td>
                <input type="checkbox" value="QQ"　name="regitem"  onclick="ay(this);" />
              QQ</td>
              <td>
                <input type="checkbox" value="MSN"　name="regitem"  onclick="ay(this);" />
              MSN</td>
              <td>&nbsp;</td>
            </tr>

          </table>
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_registerParam_0002',this)">帮助</span>
         </td>
       </tr> 
        <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">注册是否需要电子邮件验证</div></td> 
          <td class="list_link"  align="left">
                 <asp:DropDownList ID="returnemail" runat="server">
                 <asp:ListItem Value="0">否</asp:ListItem>
                 <asp:ListItem Value="1">是</asp:ListItem>
                 </asp:DropDownList>          
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_registerParam_0003',this)">帮助</span>
         </td>
       </tr>
       <tr class="TR_BG_list">
          <td class="list_link" style="width: 149px"><div align="right">注册是否需要手机认证(需要ISP接口)</div></td> 
          <td class="list_link"  align="left">
                 <asp:DropDownList ID="returnmobile" runat="server">
                 <asp:ListItem Value="0">否</asp:ListItem>
                 <asp:ListItem Value="1">是</asp:ListItem>
                 </asp:DropDownList>          
          <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onclick="Help('H_registerParam_0004',this)">帮助</span>
         </td>
       </tr>
          <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">会员等级设置</span></strong><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UserParam_levels',this)">帮助</span></td>
            </tr>
            <tr align="left" class="TR_BG_list">
              <td align="center"  colspan="2" class="list_link" id="LevelID"><p>1,名称：
                  <asp:TextBox ID="LTitle_TextBox1" runat="server" Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox1" runat="server" Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox1" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 2,名称：
                  <asp:TextBox ID="LTitle_TextBox2" runat="server" Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox2" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox2" runat="server" Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 3,名称：
                  <asp:TextBox ID="LTitle_TextBox3" runat="server" Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox3" runat="server" Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox3" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 4,名称：
                  <asp:TextBox ID="LTitle_TextBox4" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox4" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox4" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 5,名称：
                  <asp:TextBox ID="LTitle_TextBox5" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox5" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox5" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 6,名称：
                  <asp:TextBox ID="LTitle_TextBox6" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox6" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox6" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 7,名称：
                  <asp:TextBox ID="LTitle_TextBox7" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox7" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox7" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 8,名称：
                  <asp:TextBox ID="LTitle_TextBox8" runat="server" Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox8" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox8" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 9,名称：
                  <asp:TextBox ID="LTitle_TextBox9" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox9" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox9" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p>
                <p> 10,名称：<asp:TextBox ID="LTitle_TextBox10" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  &nbsp;
                  头像:
                  <asp:TextBox ID="Lpicurl_TextBox10" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                  需要积分:
                  <asp:TextBox ID="iPoint_TextBox10" runat="server"  Width="90px" Height="16px" CssClass="form"/>
                </p></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="center" colspan="2" class="list_link"><label>
                <input type="submit" name="Saveuser" value=" 提 交 " class="form" id="SaveUser" runat="server" onserverclick="SaveUser_ServerClick"/>
                </label>
                &nbsp;&nbsp;
                <label>
                <input type="reset" name="Clearuser" value=" 重 填 " class="form" id="ClearUser" runat="server" />
                </label></td>
            </tr>
          </table>
        </div>
        <div id="div_upload" style="display:none" align="center">
          <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1"  class="table">
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">上传分组参数设置</span></strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">图片存放路径做为单独域名(此版本没开放)：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="picy" runat="server" onclick="SelectOpPic0(1)" Text="是" GroupName="PicServerTF" />&nbsp;<asp:RadioButton ID="picn" runat="server" onclick="SelectOpPic0(0)" Text="否" GroupName="PicServerTF" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0001',this)">帮助</span></td>
            </tr>
            
            <tr class="TR_BG_list" id="PicDomain" style="display:none">
              <td align="right"  class="list_link" style="width: 261px"> 域名：</td>
              <td align="left" class="list_link"><asp:TextBox ID="PicServerDomain" Width="250" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0002',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="PicDir" style="display:none">
              <td align="right"  class="list_link" style="width: 261px">图片（附件）目录：</td>
              <td align="left" class="list_link"><asp:TextBox ID="PicUpload" Width="250" runat="server"   CssClass="form"/>
                &nbsp;
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择路径"  onclick="selectFile('path|<%Response.Write(Foosun.Config.UIConfig.dirFile); %>',document.SetParam.PicUpload,280,500);document.SetParam.PicUpload.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0003',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 261px"> 上传文件允许格式：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="UpfilesType" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0004',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 261px"> 上传文件允许大小：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="UpFilesSize" runat="server" CssClass="form"/>
                kb<span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0005',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">远程图片服务器域名启用(此版本没开放)：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="domainr" runat="server" onclick="SelectOpPic1(1)"  Text="是" GroupName="ReMoteDomainTF"/>&nbsp;<asp:RadioButton ID="domainn"  runat="server" onclick="SelectOpPic1(0)" Text="否" GroupName="ReMoteDomainTF"/>
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0006',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="FarDomain" style="display:none">
              <td align="right" style="width: 261px; height: 22px;" class="list_link"> 远程图片域名：</td>
              <td align="left" class="list_link"><asp:TextBox Width="250" ID="RemoteDomain" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0007',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="FarDir" style="display:none" >
              <td align="right"  class="list_link" style="width: 261px"> 远程图片保存路径：</td>
              <td align="left" class="list_link"><asp:TextBox  Width="250" ID="RemoteSavePath" runat="server" CssClass="form"/>
                &nbsp;
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择路径"  onclick="selectFile('path|<% Response.Write(Foosun.Config.UIConfig.dirFile); %>',document.SetParam.RemoteSavePath,280,500);document.SetParam.RemoteSavePath.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0008',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">分组刷新设置</span></strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 261px"> 列表每次刷新数（终极栏目列表）：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="ClassListNum" runat="server" CssClass="form" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0009',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 261px"> 信息每次刷新数：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="NewsNum" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0011',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 261px"> 批量删除信息每次删除数：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="BatDelNum" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0012',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link" style="width: 261px"> 专题每次刷新数：</td>
              <td  align="left" class="list_link"><asp:TextBox Width="250" ID="SpecialNum" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_UploadParam_0013',this)">帮助</span> </td>
            </tr>
            <tr class="TR_BG_list">
              <td align="center"  colspan="2" class="list_link"><label>
                <input type="submit" name="Saveupload" value=" 提 交 " class="form" id="SaveUpload" runat="server" onserverclick="SaveUpload_ServerClick"/>
                </label>
                &nbsp;&nbsp;
                <label>
                <input type="reset" name="Clearupload" value=" 重 填 " class="form" id="ClearUpload" runat="server" />
                </label></td>
            </tr>
          </table>
        </div>
        <div id="div_js" style="display:none" align="center">
          <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">FTP参数设置</span></strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="left" class="list_link">远程发布功能(此版本没开放)：
              <asp:RadioButton ID="ftpy" runat="server" onclick="SelectOpPic2(1)" Text="是" GroupName="FtpTF"/>&nbsp;<asp:RadioButton ID="ftpn"  runat="server" onclick="SelectOpPic2(0)" Text="否" GroupName="FtpTF" />
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_JSParam_0001',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="Ftp" style="display:none">
              <td align="left" class="list_link" colspan="2"> FTP地址：
                <asp:TextBox ID="FTPIP" runat="server" CssClass="form" Width="107px"/>
                FTP端口：
                <asp:TextBox ID="Ftpport" runat="server" CssClass="form" Width="107px"/>
                FTP帐号：
                <asp:TextBox ID="FtpUserName" runat="server" CssClass="form" Width="107px"/>
                FTP密码：
                <asp:TextBox ID="FTPPASSword" runat="server" TextMode="Password" CssClass="form" Width="107px"/>
              </td>
            </tr>
            <tr class="TR_BG" style="display:none;">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">JS设置</span></strong><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_JSParam_0002',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" style="display:none;">
              <td align="left" class="list_link"><p>热门信息显示：
                  <asp:TextBox ID="JsNews1" runat="server" Height="16px" Width="90px" CssClass="form"></asp:TextBox>
                  条信息，标题截取
                  <asp:TextBox ID="JsTitle1" runat="server" Height="16px" Width="90px" CssClass="form"></asp:TextBox>
                  字
                  <asp:DropDownList ID="JsModel1" runat="server" Height="18px" Width="90px">
                  </asp:DropDownList>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <label>
                  <input type="submit" name="HotNewsJsButton" value=" 查看模型 " class="form" id="HotNewsJsButton"/>
                  </label>
                </p>
                <p>最新信息显示：
                  <asp:TextBox ID="JsNews2" runat="server" Height="16px" Width="90px" CssClass="form"/>
                  条信息，标题截取
                  <asp:TextBox ID="JsTitle2" runat="server" Height="16px" Width="90px" CssClass="form"></asp:TextBox>
                  字
                  <asp:DropDownList ID="JsModel2" runat="server" Height="18px" Width="90px">
                  </asp:DropDownList>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <label>
                  <input type="submit" name="LastNewsJsButton" value=" 查看模型 " class="form" id="LastNewsJsButton"/>
                  </label>
                <p> 推荐信息显示：
                  <asp:TextBox ID="JsNews3" runat="server" Height="16px" Width="90px" CssClass="form"/>
                  条信息，标题截取
                  <asp:TextBox
                  ID="JsTitle3" runat="server" Height="16px" Width="90px" CssClass="form"></asp:TextBox>
                  字
                  <asp:DropDownList ID="JsModel3" runat="server" Height="18px" Width="90px">
                  </asp:DropDownList>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <label>
                  <input type="submit" name="RecNewsJSButton" value=" 查看模型 " class="form" id="RecNewsJSButton"/>
                  </label>
                <p> 热门评论显示：
                  <asp:TextBox ID="JsNews4" runat="server" Height="16px" Width="90px" CssClass="form"/>
                  条信息，标题截取
                  <asp:TextBox
                  ID="JsTitle4" runat="server" Height="16px" Width="90px" CssClass="form"></asp:TextBox>
                  字
                  <asp:DropDownList ID="JsModel4" runat="server" Height="18px" Width="90px">
                  </asp:DropDownList>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <label>
                  <input type="submit" name="HotCommJSButton" value=" 查看模型 " class="form" id="HotCommJSButton"/>
                  </label>
                <p> 头条信息显示：
                  <asp:TextBox ID="JsNews5" runat="server" Height="16px" Width="90px" CssClass="form"/>
                  条信息，标题截取
                  <asp:TextBox
                  ID="JsTitle5" runat="server" Height="16px" Width="90px" CssClass="form"></asp:TextBox>
                  字
                  <asp:DropDownList ID="JsModel5" runat="server" Height="18px" Width="90px">
                  </asp:DropDownList>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <label>
                  <input type="submit" name="TNewsJSButton" value=" 查看模型 " class="form" id="TNewsJSButton"/>
                  </label>
              </td>
            </tr>
            <tr  class="TR_BG_list">
              <td align="center" class="list_link"><label>
                <input type="submit" name="Savejs" value=" 提 交 " class="form" id="SaveJs" runat="server" onserverclick="SaveJs_ServerClick"/>
                </label>
                &nbsp;&nbsp;
                <label>
                <input type="reset" name="Clearjs" value=" 重 填 " class="form" id="ClearJs" runat="server"/>
                </label></td>
            </tr>
          </table>
        </div>
        <div id="div_water" style="display:none" align="center">
          <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">水印缩图参数设置</span></strong></td>
            </tr>
             <tr class="TR_BG_list">
              <td  align="right" class="list_link">是否开启水印/缩图功能：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="WaterY" runat="server" Text="是" GroupName="PrintTF"/>&nbsp;<asp:RadioButton ID="WaterN" runat="server" Text="否" GroupName="PrintTF"/>
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0001',this)">帮助</span></td>
            </tr>
            
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 214px">水印类型：</td>
              <td align="left" class="list_link"><asp:DropDownList ID="PrintPicTF" runat="server" onchange="SelectOpPic(this.value)">
                  <asp:ListItem Value="99" Selected="True">请选择</asp:ListItem>
                  <asp:ListItem Value="7">文字水印</asp:ListItem>
                  <asp:ListItem Value="8">图片水印</asp:ListItem>
                </asp:DropDownList>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0002',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="Waterword" style="display:none">
              <td align="right" class="list_link" style="width: 214px"> 水印文字为：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="PrintWord" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0003',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="Watersize" style="display:none">
              <td align="right" class="list_link" style="width: 214px"> 水印文字大小为：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="Printfontsize" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0004',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="Waterfont" style="display:none">
              <td align="right" style="width: 214px">水印字体选择：</td>
              <td  align="left" class="list_link"><asp:DropDownList ID="Printfontfamily" runat="server" Width="93px" >
                  <asp:ListItem Selected="True" Value="0">宋体</asp:ListItem>
                  <asp:ListItem Value="1">黑体</asp:ListItem>
                  <asp:ListItem Value="2">楷体</asp:ListItem>
                  <asp:ListItem Value="3">隶书</asp:ListItem>
                  <asp:ListItem Value="4">Andale Mono</asp:ListItem>
                  <asp:ListItem Value="5">Arial</asp:ListItem>
                  <asp:ListItem Value="6">Book Antiqua</asp:ListItem>
                  <asp:ListItem Value="7">Century Gothic</asp:ListItem>
                  <asp:ListItem Value="8">Comic Sans MS</asp:ListItem>
                  <asp:ListItem Value="9">Courier New</asp:ListItem>
                  <asp:ListItem Value="10">Georgia</asp:ListItem>
                  <asp:ListItem Value="11">Impact</asp:ListItem>
                  <asp:ListItem Value="12">Tahoma</asp:ListItem>
                  <asp:ListItem Value="13">Times New Roman</asp:ListItem>
                  <asp:ListItem Value="13">Trebuchet MS</asp:ListItem>
                  <asp:ListItem Value="13">Script MT Bold</asp:ListItem>
                  <asp:ListItem Value="13">Stencil</asp:ListItem>
                  <asp:ListItem Value="13">Verdana</asp:ListItem>
                  <asp:ListItem Value="13">Lucida Console</asp:ListItem>
                </asp:DropDownList>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0005',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="Watercolor" style="display:none">
              <td align="right" class="list_link" style="width: 214px"> 水印文字颜色：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="Printfontcolor" runat="server" CssClass="form"/>
                <img src="../../sysImages/FileIcon/Rect.gif" alt="-" name="MarkFontColor_Show" width="18" height="17" border=0 align="absmiddle" id="MarkFontColor_Show" style="cursor:pointer;background-color:#<%= Printfontcolor.Text%>;" title="选取颜色" onClick="GetColor(this,'Printfontcolor');"><span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0006',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list"  id="WaterB" style="display:none">
              <td align="right" class="list_link" style="width: 214px"> 水印文字是否粗体：</td>
              <td  align="left" class="list_link"><asp:DropDownList ID="PrintBTF" runat="server" Width="93px">
                  <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                  <asp:ListItem Value="0">否</asp:ListItem>
                </asp:DropDownList>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0007',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="Waterpicurl" style="display:none">
              <td align="right"  class="list_link" style="width: 214px"> 水印图片地址为：</td>
              <td align="left" class="list_link"><asp:TextBox ID="PintPicURL" runat="server"  CssClass="form"/>
                &nbsp;
                <label>
                <input type="button" name="PintPicURLClick" value=" 选择图片 " class="form" id="PintPicURLC" style="width: 84px" onclick="selectFile('pic',document.SetParam.PintPicURL,280,500);document.SetParam.PintPicURL.focus();"/>
                </label>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0008',this)">帮助</span></td>
            </tr>
            <%--时间：208-07-21 修改者：吴静岚  新增水印图片按比例生成 开始--%>
          <tr class="TR_BG_list" id="Waterwidth" style="display:none">
              <td align="right" class="list_link" style="width: 214px"> 水印图片比例为：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="PrintPicsize" runat="server" CssClass="form"  Text="0.8"/><span id="print_span" runat="server" style="display:none;"/>
                <span class="helpstyle" style="cursor:help;" title="例：填写0.8表示占原图比例为80%">帮助</span></td>
            </tr>
             <tr class="TR_BG_list" id="WaterP" style="display:none">
              <td align="right" class="list_link" style="width: 214px;"> 水印图片透明度为：</td>
              <td  align="left" class="list_link" ><asp:TextBox ID="PintPictrans" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0010',this)">帮助</span></td>
            </tr>
            <%--时间：208-07-21 修改者：吴静岚  新增水印图片按比例生成 结束--%>
            <tr class="TR_BG_list" id="WaterW" style="display:none">
              <td align="right" class="list_link" style="width: 214px">水印位置： </td>
              <td  align="left" class="list_link"><asp:DropDownList ID="PrintPosition" runat="server" Width="93px">
                  <asp:ListItem Selected="True" Value="0">居中</asp:ListItem>
                  <asp:ListItem Value="1">左上</asp:ListItem>
                  <asp:ListItem Value="2">左下</asp:ListItem>
                  <asp:ListItem Value="3">右上</asp:ListItem>
                  <asp:ListItem Value="4">右下</asp:ListItem>
                </asp:DropDownList>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0011',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 214px">是否开启缩图功能： </td>
              <td  align="left" class="list_link"><asp:DropDownList ID="PrintSmallTF" runat="server" Width="93px" onchange="SelectOpPic(this.value)">
                  <asp:ListItem Value="9">关闭</asp:ListItem>
                  <asp:ListItem Value="10">开启</asp:ListItem>
                </asp:DropDownList>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0012',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="smallselect" style="display:none">
              <td align="right"  class="list_link" style="width: 214px">缩图的方式选择：</td>
              <td  align="left" class="list_link"><asp:DropDownList ID="PrintSmallSizeStyle" runat="server" Width="93px" onchange="SelectOpPic(this.value)">
                  <asp:ListItem Value="13">选择</asp:ListItem>
                  <asp:ListItem Value="11">大小</asp:ListItem>
                  <asp:ListItem Value="12">比例</asp:ListItem>
                </asp:DropDownList>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0013',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="width" style="display:none">
              <td align="right" class="list_link" style="width: 214px"> 高|宽：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="PrintSmallSize" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0014',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="inv" style="display:none">
              <td align="right" class="list_link" style="width: 214px"> 比例：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="PrintSmallinv" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_WaterParam_0015',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="center" colspan="2" class="list_link"><label>
                <input type="submit" name="Savewater" value=" 提 交 " class="form" id="Savewater" runat="server" onserverclick="Savewater_ServerClick"/>
                </label>
                &nbsp;&nbsp;
                <label>
                <input type="reset" name="Clearwater" value=" 重 填 " class="form" id="Clearwater" runat="server" />
                </label></td>
            </tr>
          </table>
        </div>
        <div id="div_rssxmlwap" style="display:none" align="center">
          <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">RSS.XML.WAP参数设置</span></strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link" style="width: 219px">显示最新范围：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="RssNum" runat="server" Width="250"  CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_rssParam_0001',this)">帮助</span> </td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right"  class="list_link" style="width: 219px">简介截取数：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="RssContentNum" Width="250" runat="server" CssClass="form" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_rssParam_0002',this)">帮助</span> </td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link" style="width: 219px">RSS标题：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="RssTitle" Width="250" runat="server" CssClass="form" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_rssParam_0003',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td align="right" class="list_link" style="width: 219px">RSS图片地址：</td>
              <td align="left" class="list_link"><asp:TextBox ID="RssPicURL" Width="250" runat="server"  CssClass="form"/>
                <img src="../../sysImages/folder/s.gif" id="RssPicURLClick" runat="server" style="cursor:pointer;" title="选择图片" onclick="selectFile('pic',document.SetParam.RssPicURL,280,500);document.SetParam.RssPicURL.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_rssParam_0004',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
              <td  align="right" class="list_link">新闻是否添加到wap服务器中：</td>
              <td  align="left" class="list_link"><asp:RadioButton ID="wapy" Width="250"  runat="server" onclick="SelectOpPic3(1)" Text="是" GroupName="WapTF"/>&nbsp;<asp:RadioButton ID="wapn"  runat="server" onclick="SelectOpPic3(0)" Text="否" GroupName="WapTF"/>
              <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_rssParam_0005',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="wapdir" style="display:none">
              <td align="right"  class="list_link" style="width: 219px">Wap文件存放路径：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="WapPath" Width="250" runat="server"  CssClass="form"/>
                <img src="../../sysImages/folder/s.gif" style="cursor:pointer;" title="选择路径"  onclick="selectFile('path|xml/wap',document.SetParam.WapPath,280,500);document.SetParam.WapPath.focus();" />
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_rssParam_0006',this)">帮助</span> </td>
            </tr>
            <tr class="TR_BG_list" id="lastn" style="display:none">
              <td align="right"  class="list_link" style="width: 219px">wap显示最新数：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="WapLastNum" Width="250" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_rssParam_0008',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="wapd" style="display:none">
              <td align="right"  class="list_link" style="width: 219px">Wap捆绑域名：</td>
              <td  align="left" class="list_link"><asp:TextBox ID="WapDomain" Width="250" runat="server" CssClass="form"/>
                <span class="helpstyle" style="cursor:help;" title="点击查看帮助" onClick="Help('H_rssParam_0007',this)">帮助</span></td>
            </tr>
            <tr  class="TR_BG_list">
              <td align="right"  colspan="2" class="list_link"><label>
                <input type="submit" name="Saverss" value=" 提 交 " class="form" id="Saverss" runat="server" onserverclick="Saverss_ServerClick"/>
                </label>
                &nbsp;&nbsp;
                <label>
                <input type="reset" name="Clearrss" value=" 重 填 " class="form" id="Clearrss" runat="server"/>
                </label></td>
            </tr>
          </table>
        </div>
        <div id="div_api" style="display:none" align="center">
          <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
            <tr class="TR_BG">
              <td align="left" colspan="2" class="list_link"><strong><span style="color: #000033">API参数设置</span></strong></td>
            </tr>
            <tr class="TR_BG_list">
              <td class="list_link">api</td>
            </tr>
          </table>
        </div></td>
    </tr>
  </table>
</form>
<table border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px; width: 100%;">
  <tr>
    <td align="center"><label id="copyright" runat="server" /></td>
  </tr>
</table>
</body>
<script type="text/javascript" language="javascript">
//--------------系统参数设置选项卡方式选择的函数定义及声明----------------------------
	function ChangeDiv(ID)
	{
		document.getElementById('td_baseinfo').className='';
		document.getElementById('td_user').className='';
		document.getElementById('td_upload').className='';
		document.getElementById('td_js').className='';
		document.getElementById('td_water').className='';
		document.getElementById('td_rssxmlwap').className='';
		document.getElementById('td_api').className='';
		document.getElementById("td_"+ID).className='reshow';

		document.getElementById("div_baseinfo").style.display="none";
		document.getElementById("div_user").style.display="none";
		document.getElementById("div_upload").style.display="none";
		document.getElementById("div_js").style.display="none";
		document.getElementById("div_water").style.display="none";
		document.getElementById("div_rssxmlwap").style.display="none";
		document.getElementById("div_api").style.display="none";
		document.getElementById("div_"+ID).style.display="";
	}
	//-------PicDomain-----------------------------------
    function SelectOpPic(OpType)
    {
	    switch(parseInt(OpType))
		{
				//----------水印----------
		    case 7:
		    	document.getElementById("Waterword").style.display="";//文字
				document.getElementById("Watersize").style.display="";//大小
				document.getElementById("Waterfont").style.display="";//字体
				document.getElementById("Watercolor").style.display="";//颜色
				document.getElementById("WaterB").style.display="";	//粗体？
				
                document.getElementById("Waterpicurl").style.display="none";//图片地址
				document.getElementById("Waterwidth").style.display="none";//高宽
				document.getElementById("WaterP").style.display="none";//透明度
				document.getElementById("WaterW").style.display="";//位置	
				break;
		    case 8:			
			    document.getElementById("Waterword").style.display="none";
				document.getElementById("Watersize").style.display="none";
				document.getElementById("Waterfont").style.display="none";
				document.getElementById("Watercolor").style.display="none";
				document.getElementById("WaterB").style.display="none";	
				
                document.getElementById("Waterpicurl").style.display="";
				document.getElementById("Waterwidth").style.display="";
				document.getElementById("WaterP").style.display="";//透明度
				document.getElementById("WaterW").style.display="";	
				break;	
			case 99://文字图片都不选择
		    	document.getElementById("Waterword").style.display="none";//文字
				document.getElementById("Watersize").style.display="none";//大小
				document.getElementById("Waterfont").style.display="none";//字体
				document.getElementById("Watercolor").style.display="none";//颜色
				document.getElementById("WaterB").style.display="none";	//粗体？
				
                document.getElementById("Waterpicurl").style.display="none";//图片地址
				document.getElementById("Waterwidth").style.display="none";//高宽
				document.getElementById("WaterP").style.display="none";//透明度
				document.getElementById("WaterW").style.display="none";//位置	
				break;
				
				
			//----------缩图-------
			case 9:
				document.getElementById("smallselect").style.display="none";//开启缩图？
				document.getElementById("width").style.display="none";//大小
				document.getElementById("inv").style.display="none";//比例
				document.getElementById("PrintSmallSizeStyle").style.value="13";
				break;
			case 10:
			    document.getElementById("smallselect").style.display="";
			    document.getElementById("width").style.display="";//大小
			    document.getElementById("inv").style.display="none";//比例
				break;
			case 11:
				document.getElementById("width").style.display="";//大小
				document.getElementById("inv").style.display="none";//比例
				break;
			case 12:
				document.getElementById("width").style.display="none";
				document.getElementById("inv").style.display="";
				break;
			case 13:
				document.getElementById("width").style.display="none";
				document.getElementById("inv").style.display="none";
				break;
		}
    }
    function SelectOpPic0(value)
    {
         switch(parseInt(value))
		 {
		        //----------图片域名--------
			case 0:
				document.getElementById("PicDomain").style.display="none";//图片域名
				document.getElementById("PicDir").style.display="none";//图片附件目录
				break;
			case 1:
 			    document.getElementById("PicDomain").style.display="";
				document.getElementById("PicDir").style.display="";
     			break;
		 }
    }
    function SelectOpPic1(value)
    {
        switch(parseInt(value))
        {
            case 0:
				document.getElementById("FarDomain").style.display="none";//远程图片域名
				document.getElementById("FarDir").style.display="none";//远程图片保存路径
				break;
			case 1:
				document.getElementById("FarDomain").style.display="";
				document.getElementById("FarDir").style.display="";
				break;
        }
    }
    function SelectOpPic2(value)
    {
        switch(parseInt(value))
        {
        	//----------ftp-----------
			case 1:
				document.getElementById("Ftp").style.display="";//ftp设置
				break;
			case 0:
				document.getElementById("Ftp").style.display="none";
				break;
		}
    }
    function SelectOpPic3(value)
    {
         switch(parseInt(value))
         {
            //-------wap------------	
			case 0:
				document.getElementById("wapdir").style.display="none";//wap路径
				document.getElementById("lastn").style.display="none";//wap最新新闻数
				document.getElementById("wapd").style.display="none";//wap域名
				break;
			case 1:
				document.getElementById("wapdir").style.display="";
				document.getElementById("lastn").style.display="";
				document.getElementById("wapd").style.display="";
				break;
		}
    }
    function check()
    {
        if(document.SetParam.SiteName.value=="")
        {
            document.getElementById("SiteName_Span").innerHTML="<span class=reshow>(*)请输入站点名称</span>";
            return false;
        }
        var keywordlength = /^[0-9]{0,4}\|[0-9]{0,4}$/;
        var keytest = document.SetParam.LenSearch.value;
        var setPointest = document.SetParam.setPoint.value;
        var LoginLockest = document.SetParam.LoginLock.value;
        var PrintPicsizest = document.SetParam.PrintPicsize.value;
        var meili = document.SetParam.cPointParam.value;
        var huoyue = document.SetParam.aPointparam.value;
        if(keywordlength.test(keytest)==false)
        {
            document.getElementById("keyLength").innerHTML="<span class=reshow>(*)格式不正确，请参考帮助</span>";
            return false;
        }
        if(keywordlength.test(setPointest)==false)
        {
            document.getElementById("Point_Span").innerHTML="<span class=reshow>(*)格式不正确，请参考帮助</span>";
            return false;
        }
        if(keywordlength.test(LoginLockest)==false)
        {
            document.getElementById("Login_Span").innerHTML="<span class=reshow>(*)格式不正确，请参考帮助</span>";
            return false;
        }
        if(keywordlength.test(PrintPicsizest)==false)
        {
            document.getElementById("print_span").innerHTML="<span class=reshow>(*)格式不正确，请参考帮助</span>";
            return false;
        }
        if(keywordlength.test(meili)==false)
        {
            document.getElementById("MeiL").innerHTML="<span class=reshow>(*)格式不正确，请参考帮助</span>";
            return false;
        }
        if(keywordlength.test(huoyue)==false)
        {
            document.getElementById("Huo").innerHTML="<span class=reshow>(*)格式不正确，请参考帮助</span>";
            return false;
        }
    }
function clears()
{
    document.SetParam.regitemContent.value="";
}
function ay(obj)
{
	if (obj!='')
	{
		if(obj.checked)
		{
		    if (document.SetParam.regitemContent.value.search(obj.value)==-1)
		    {
			    if (document.SetParam.regitemContent.value=='')
			        document.SetParam.regitemContent.value=obj.value;
			    else 
			        document.SetParam.regitemContent.value=document.SetParam.regitemContent.value+','+obj.value;
		    }
		}else
		{
            var ep=document.SetParam.regitemContent.value;
            document.SetParam.regitemContent.value = ep.replace(","+obj.value,"");
		}
	}    
}

</script>
<!--调用函数控制选择按钮选则是的时候其相应的框架是否显示出来-->
<% isshow(); %>
</html>