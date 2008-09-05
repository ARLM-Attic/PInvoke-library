<%@ Page language="c#" Inherits="Discuz.Web.Admin.baseset" Codebehind="global_baseset.aspx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<%@ Register TagPrefix="uc1" TagName="TextareaResize" Src="../UserControls/TextareaResize.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>baseset</title>		
        <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />        
   	    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
   	    
        <script type="text/javascript" src="../js/common.js"></script>
		<script type="text/javascript" src="../js/modalpopup.js"></script>
		<script type="text/javascript">
		    function setStatus(status)
		    {
		        document.getElementById("closeinfo").style.display = (status) ? "block" : "none";
		        document.getElementById("closedreason").style.display = (status) ? "block" : "none";
		        document.getElementById("configother").style.display = (!status) ? "block" : "none";
		    }
        </script>
</head>
<body>
	<div class="ManagerForm">
		<form id="Form1" method="post" runat="server" name="Form1">
		<fieldset>
		<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">基本设置</legend>
		    <table cellspacing="0" cellpadding="4" width="100%" align="center">
		        <tr>
		            <td  class="panelbox" width="50%" align="left">
		                <table width="100%">
		                    <tr>
		                        <td style="width:80px">网站名称:</td>
					            <td>
						            <cc1:TextBox id="webtitle" runat="server" RequiredFieldType="暂无校验" Width="200px" HintTitle="提示" 
						                HintInfo="网站名称, 将显示在页面底部的联系方式处"></cc1:TextBox>
					            </td>
		                    </tr>
		                    <tr>
		                        <td>论坛名称:</td>
					            <td>
						            <cc1:TextBox id="forumtitle" runat="server" RequiredFieldType="暂无校验" Width="200px" HintTitle="提示" 
						                HintInfo="论坛名称, 将显示在导航条和标题中"></cc1:TextBox>
					            </td>
		                    </tr>
		                    <tr<%=haveSpace?"":" style='display:none'"%>>
					            <td>空间名称:</td>
					            <td>
						            <cc1:TextBox id="spacename" runat="server" RequiredFieldType="暂无校验" Width="200px" HintTitle="提示" 
						                HintInfo="该内容将会替换前台所有&amp;quot;空间&amp;quot;字样"></cc1:TextBox>
					            </td>
		                    </tr>
		                    <tr<%=haveAlbum?"":" style='display:none'"%>>
					            <td>相册名称:</td>
					            <td>
						            <cc1:TextBox id="albumname" runat="server" RequiredFieldType="暂无校验" Width="200px" HintTitle="提示" 
						                HintInfo="该内容将会替换前台所有&amp;quot;相册&amp;quot;字样"></cc1:TextBox>
					            </td>
		                    </tr>
		                    <tr>
					            <td>网站备案信息代码:</td>
					            <td>
						            <cc1:TextBox id="icp" runat="server"  RequiredFieldType="暂无校验" Width="200px" HintTitle="提示" 
						                HintInfo="页面底部可以显示 ICP 备案信息,如果网站已备案,在此输入您的授权码,它将显示在页面底部,如果没有请留空"></cc1:TextBox>
					            </td>
		                    </tr>
		                    <tr>
				                <td>启用 RSS:</td>
					            <td>
						            <cc1:RadioButtonList id="rssstatus" runat="server"  RepeatLayout="flow" HintTitle="提示" 
						                HintInfo="选择&amp;quot;是&amp;quot;, 论坛将允许用户使用 RSS 客户端软件接收最新的论坛帖子更新. 注意: 在分论坛很多的情况下, 本功能可能会加重服务器负担">
							            <asp:ListItem Value="1">是</asp:ListItem>
							            <asp:ListItem Value="0">否</asp:ListItem>
						            </cc1:RadioButtonList>
					            </td>
		                    </tr>
		                    <tr>
				                <td>启用 SiteMap:</td>
					            <td>
						            <cc1:RadioButtonList id="sitemapstatus" runat="server"  RepeatLayout="flow" HintTitle="提示" HintInfo="SiteMap为百度论坛收录协议,是否允许百度收录">
							            <asp:ListItem Value="1">是</asp:ListItem>
							            <asp:ListItem Value="0">否</asp:ListItem>
						            </cc1:RadioButtonList>
					            </td>
		                    </tr>
		                    <tr>		                    
					            <td>显示程序运行信息:</td>
					            <td>
						            <cc1:RadioButtonList ID="debug" runat="server" RepeatLayout="flow" 
						                HintInfo="选择&amp;quot;是&amp;quot;将在页脚处显示程序运行时间" HintTitle="提示">
							            <asp:ListItem Value="1">是</asp:ListItem>
							            <asp:ListItem Value="0">否</asp:ListItem>
						            </cc1:RadioButtonList>
					            </td>
		                    </tr>
		                    <tr>
					            <td>启用伪静态url:</td>
					            <td> 
					                <cc1:RadioButtonList id="aspxrewrite" runat="server"  RepeatLayout="flow" HintTitle="提示" 
					                    HintInfo="只有启用该设置，伪静态url设置才会生效">
							            <asp:ListItem Value="1">是</asp:ListItem>
							            <asp:ListItem Value="0" Selected="true">否</asp:ListItem>
						            </cc1:RadioButtonList>
					            </td>
		                    </tr>
		                    <tr>					
					            <td>数据库全文搜索:</td>
					            <td>
						            <cc1:RadioButtonList id="fulltextsearch" runat="server" RepeatLayout="flow" HintTitle="提示" 
						            HintInfo="论坛会对查询使用SQL2000的全文搜索功能以提升效率. 注意: 本功能会增加数据库的体积">
							            <asp:ListItem Value="1">是</asp:ListItem>
							            <asp:ListItem Value="0">否</asp:ListItem>
						            </cc1:RadioButtonList>
					            </td>
		                    </tr>
		                    <tr>
					            <td>密码模式:</td>
					            <td>
						            <cc1:RadioButtonList id="passwordmode" runat="server" RepeatLayout="flow" HintTitle="提示" 
						            HintInfo="注意: 动网兼容模式只适用于从动网论坛(或LeadBBS和雪人论坛等)转换而来的论坛用户数据.">
						                <asp:ListItem Value="0"  Selected=true>默认</asp:ListItem>
							            <asp:ListItem Value="1" >动网兼容模式</asp:ListItem>								
						            </cc1:RadioButtonList>
					            </td>
		                    </tr>
		                    <tr>
				                <td>身份验证Cookie域:</td>
                                <td>
						            <cc1:TextBox id="CookieDomain" runat="server" Text="" Size="30" RequiredFieldType="暂无校验" 
						            HintInfo="如需所有子域共享此Cookie, 例如:<br />要让www.abc.com 与 bbs.abc.com共享论坛Cookie,则请设置此处为 .abc.com" HintTitle="提示"></cc1:TextBox>
					            </td>
		                    </tr>
		                    <tr>
				                <td>是否记录缓存日志:</td>
					            <td>
						            <cc1:RadioButtonList id="cachelog" RepeatLayout="flow" runat="server" HintTitle="提示" HintInfo="此项功能会记录论坛的缓存日志并在缓存日志中进行显示. 注意: 当此项功能会加重系统负担" >
							            <asp:ListItem Value="1">是</asp:ListItem>
							            <asp:ListItem Value="0" Selected=true>否</asp:ListItem>
						            </cc1:RadioButtonList>
					            </td>
		                    </tr>		                    
				            <tr>
					            <td>允许查看会员列表:</td>
					            <td>
						            <cc1:RadioButtonList ID="memliststatus" runat="server" RepeatLayout="Flow" HintInfo="允许查看会员列表" HintTitle="提示">
							            <asp:ListItem Value="1">是</asp:ListItem>
							            <asp:ListItem Value="0">否</asp:ListItem>
						            </cc1:RadioButtonList>
					            </td>
				            </tr>
				            <tr>
				                <td style="width: 140px">&nbsp;&nbsp;外部链接:</td>
				                <td>
				                    <uc1:TextareaResize id="Linktext" runat="server"  cols="45" controlname="Linktext" HintTitle="提示" 
				                    HintInfo="用户可以自己添加的外部链接html字符串，例如&amp;amp;lt;a href=\'/download/\'&amp;amp;gt;下载频道&amp;amp;lt;/a&amp;amp;gt;" HintPosOffSet="160"></uc1:TextareaResize>
				                </td>
				            </tr>
		                </table>
		            </td>
		            <td class="panelbox" width="50%" align="right">
		                <table width="100%">
		                <tr>					
				            <td style="width: 80px">网站URL地址:</td>
				            <td>
						        <cc1:TextBox id="weburl" runat="server" RequiredFieldType="网页地址" Width="200px" 
						            HintTitle="提示" HintInfo="网站 URL, 将作为链接显示在页面底部"></cc1:TextBox>
					        </td>
				        </tr>
				        <tr>        					
				            <td>论坛URL地址:</td>
				            <td>
				                <cc1:TextBox id="forumurl" runat="server" RequiredFieldType="暂无校验" CanBeNull="必填" Width="200px" cols="45" 
				                    HintTitle="提示" HintInfo="论坛URL地址, 默认为 &amp;quot;forumindex.aspx&amp;quot;"></cc1:TextBox>
					        </td>
				        </tr>
				        <tr<%=haveSpace?"":" style='display:none'"%>>
				            <td>空间URL地址:</td>
				            <td>
						        <cc1:TextBox id="spaceurl" runat="server" RequiredFieldType="暂无校验" CanBeNull="必填" Width="200px" HintTitle="提示" 
						            HintInfo="空间首页地址, 默认为 &amp;quot;spaceindex.aspx&amp;quot;"></cc1:TextBox>
					        </td>
				        </tr>	
				        <tr<%=haveAlbum?"":" style='display:none'"%>>
				            <td>相册URL地址:</td>
				            <td>
						        <cc1:TextBox id="albumurl" runat="server" RequiredFieldType="暂无校验" CanBeNull="必填" Width="200px" HintTitle="提示" 
						            HintInfo="相册首页地址, 默认为 &amp;quot;albumindex.aspx&amp;quot;"></cc1:TextBox>
					        </td>
				        </tr>
				        <tr>
				            <td>显示授权信息链接:</td>
				            <td>
						        <cc1:RadioButtonList id="licensed" runat="server"  RepeatLayout="Flow" HintTitle="提示" 
						            HintInfo="选择&amp;quot;是&amp;quot;将在页脚显示商业授权用户链接, 链接将指向 Discuz!NT 官方网站, 用户可通过此链接验证其所使用的Discuz!NT 是否经过商业授权">
							        <asp:ListItem Value="1">是</asp:ListItem>
							        <asp:ListItem Value="0">否</asp:ListItem>
						        </cc1:RadioButtonList>
					        </td>
				        </tr>
				        <tr>
					        <td>RSS TTL(单位:分钟):</td>
					        <td>
						        <cc1:TextBox id="rssttl" runat="server"  CanBeNull="必填" MinimumValue="0" Text="60" Size="5" MaxLength="4" HintTitle="提示" 
						            HintInfo="TTL(Time to Live) 是 RSS 2.0 的一项属性, 用于控制订阅内容的自动刷新时间, 时间越短则资料实时性就越高, 但会加重服务器负担, 通常可设置为 30～180 范围内的数值"></cc1:TextBox>
					        </td>
				        </tr>				
				        <tr>			
					        <td>SiteMap TTL<br />(单位:小时):</td>
					        <td>
					            <cc1:TextBox id="sitemapttl" runat="server"  CanBeNull="必填" MinimumValue="1" MaximumValue="24" Text="12" Size="5" MaxLength="2" HintTitle="提示" 
					                HintInfo="百度论坛收录协议更新时间, 用于控制百度论坛收录协议更新时间, 时间越短则资料实时性就越高, 但会加重服务器负担, 通常可设置为 1～24 范围内的数值"></cc1:TextBox>
					        </td>
				        </tr>
				        <tr>
				            <td>禁止浏览器缓冲:</td>
					        <td>
						        <cc1:RadioButtonList id="nocacheheaders" runat="server"  RepeatLayout="flow" HintTitle="提示" 
						            HintInfo="选择&amp;quot;是&amp;quot;将禁止浏览器对论坛页面进行缓冲, 用于解决极个别浏览器内容刷新不正常的问题. 注意: 本功能会加重服务器负担">
							        <asp:ListItem Value="1">是</asp:ListItem>
							        <asp:ListItem Value="0">否</asp:ListItem>
						        </cc1:RadioButtonList>
					        </td>
				        </tr>
				        <tr>
					        <td>伪静态url的扩展名:</td>
					        <td>
					            <cc1:TextBox id="extname" runat="server"  RequiredFieldType="暂无校验" CanBeNull="必填" Text="10" Size="5" HintTitle="提示" 
					            HintInfo="此功能会实现网页链接页面的扩展名使用当前的设置!"></cc1:TextBox>
					            <a href="http://nt.discuz.net/doc/default.aspx?cid=36" target="_blank"><img src="../images/nav/sysinfo.gif" border="0" alt="伪静态url设置帮助" /></a>
					        </td>
				        </tr>
				        <tr>
					        <td>伪静态url的替换规则:</td>
					        <td>
					           <span id="Span1" onmouseout="hidehintinfo();" 
					           onmouseover="showhintinfo(this,0,0,'提示','此处功能会实现网页链接的地址重定向的正则式校验内容,当您修改时请谨用!','50','up');">
					                <span id="Span2" style="display:inline-block;border-width:0px;border-style:Dotted;">
					                    <a href="#" class="TextButton" onclick="javascript:window.location.href='global_urlgrid.aspx';" >编辑伪静态url替换规则</a>
					                </span>
					            </span>
					        </td>
				        </tr>
				        <tr>
					        <td>启用SilverLight:</td>
					        <td>
						        <cc1:RadioButtonList id="enablesilverlight" runat="server" RepeatLayout="flow" HintTitle="提示" HintInfo="本功能用于调查、相册、视频播放、广告的特殊效果">
						            <asp:ListItem Value="1"  Selected=true>是</asp:ListItem>
							        <asp:ListItem Value="0" >否</asp:ListItem>								
						        </cc1:RadioButtonList>
					        </td>
				        </tr>				
				        <tr>
					        <td>Discuz!NT代码模式:</td>
					        <td>
							        <cc1:RadioButtonList id="bbcodemode" runat="server" RepeatColumns="1" RepeatLayout="flow" HintTitle="提示" HintInfo="注意: 动网UBB兼容模式只适用于从动网论坛转换而来的论坛数据.">
							            <asp:ListItem Value="0"  Selected=true>标准Discuz!NT代码</asp:ListItem>
								        <asp:ListItem Value="1" >动网UBB代码兼容模式</asp:ListItem>								
							        </cc1:RadioButtonList>
					        </td>
				        </tr>
				        <tr>
                            <td>首页类型:</td>
                            <td>
                                <cc1:RadioButtonList ID="Indexpage" RepeatLayout="flow" runat="server">
                                    <asp:ListItem Value="0">论坛首页</asp:ListItem>
                                    <asp:ListItem Value="1">聚合首页</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
				        </tr>
				        <tr>
				                <td style="width: 140px">&nbsp;&nbsp;统计代码设置:</td>
				                <td>
				                    <uc1:TextareaResize id="Statcode" runat="server"  cols="45" controlname="Linktext" HintTitle="提示" 
				                    HintInfo="用户可以自己添加的统计代码" HintPosOffSet="160"></uc1:TextareaResize>
				                </td>
				            </tr>
		                </table>
		            </td>
		        </tr>
		    </table>
		</fieldset>
		<cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
		<fieldset>
	    <legend style="background:url(../images/icons/icon23.jpg) no-repeat 6px 50%;">模块开启设置</legend>
		    <table cellspacing="0" cellpadding="4" width="100%" align="center">
		        <tr>
		            <td class="panelbox" width="50%" align="left">
		                <table width="100%">
		                    <tr>
		                        <td style="width: 140px">是否全部关闭<br />Discuz!NT模块:</td>
			                    <td>
				                    <cc1:RadioButtonList id="closed" runat="server" RepeatLayout="flow" HintTitle="提示" 
				                    HintInfo="暂时将Discuz!NT模块关闭(包括论坛、个人空间、相册等), 其他人无法访问, 但不影响管理员访问" HintPosOffSet="80">
					                    <asp:ListItem Value="1">是</asp:ListItem>
					                    <asp:ListItem Value="0">否</asp:ListItem>
				                    </cc1:RadioButtonList>
			                    </td>
	                        </tr>
	                    </table>
	                </td>
	                <td class="panelbox" width="50%" align="right">
	                    <table width="100%">
	                        <tr>				  
		                        <td><span id="closeinfo">关闭的原因:</span></td>
		                        <td>
		                            <uc1:TextareaResize id="closedreason" runat="server" controlname="closedreason" HintTitle="提示" HintInfo="论坛关闭时出现的提示信息" HintPosOffSet="160" >
		                            </uc1:TextareaResize>
		                        </td>
	                        </tr>
	                    </table>
	                </td>
	            </tr>
	      </table>
          <table id="configother" cellspacing="0" cellpadding="4" width="100%" align="center">
                <tr>
		            <td class="panelbox" width="50%" align="left">
		                <table width="100%">
                            <tr>
	                            <td style="width: 140px"><asp:Label ID="EnableSpaceLabel" Text="是否启用个人空间服务:" runat="server"></asp:Label></td>
		                        <td>
                                    <cc1:RadioButtonList id="EnableSpace" runat="server" RepeatLayout="flow">
			                            <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
			                            <asp:ListItem Value="0">否</asp:ListItem>
		                            </cc1:RadioButtonList>
		                        </td>
		                     </tr>
		                     <tr<%=haveMall?"":" style='display:none'"%>>
		                        <td>是否启用交易<br />帖或商城服务:</td>
		                        <td>
                                    <cc1:DropDownList ID="EnableMall" runat="server">
			                            <asp:ListItem Value="0" Selected="True">不开启</asp:ListItem>
			                            <asp:ListItem Value="1">启用交易帖</asp:ListItem>
		                            </cc1:DropDownList>
		                        </td>
		                     </tr>
		                </table>
		            </td>
		            <td class="panelbox" width="50%" align="right">
                        <table width="100%">
                            <tr>
                                <td style="width: 140px"><asp:Label ID="EnableAlbumLabel" Text="是否启用相册服务:" runat="server"></asp:Label></td>
	                            <td>
	                                <cc1:RadioButtonList id="EnableAlbum" runat="server" RepeatLayout="flow">
	                                    <asp:ListItem Value="1">是</asp:ListItem>
	                                    <asp:ListItem Value="0">否</asp:ListItem>
                                    </cc1:RadioButtonList>
	                            </td>
                            </tr>
                        </table>
		            </td>
		        </tr>
          </table>
		</fieldset>
        <div class="Navbutton">
            <cc1:Button ID="SaveInfo" runat="server" Text="提 交" ></cc1:Button>
        </div>
        <script type="text/javascript">
            setStatus(document.getElementById("closed_0").checked);
        </script>
	    </form>
    </div>		
	<%=footer%>
</body>
</html>
