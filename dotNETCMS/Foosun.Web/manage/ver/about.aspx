<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_ver_about" Codebehind="about.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<style type="text/css">
<!--
.STYLE1 {
	color: #FF0000;
	font-weight: bold;
}
-->
</style>
</head>
<body>
<table width="100%" height="32" align="center" border="0" cellpadding="0" cellspacing="0" background="../../sysImages/<% Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/reght_1_bg_1.gif">
        <tr>
          <td height="1" colspan="2"></td>
        </tr>
        <tr>
          <td width="17%" height="32" class="sysmain_navi"  style="PADDING-LEFT: 14px" >关于产品</td>
          <td height="32" class="topnavichar"  style="PADDING-LEFT: 14px" >&nbsp;</td>
        </tr>
</table>
      <table border=0 cellpadding="0" width="772px" cellspacing="0" background="../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath()); %>/admin/line.gif">
        <tr>
          <td width="194" height="28" id="td_1" class="m_down_bg" onclick="javascript:ChangeDiv('1')" style="cursor:hand;"><div align="center">产品信息</div></td>
          <td width="193" height="28"  class="m_up_bg" id="td_2" onclick="javascript:ChangeDiv('2')" style="cursor:hand;"><div align="center">获得商业版</div></td>
          <td width="193" height="28" class="m_up_bg" id="td_3" onclick="javascript:ChangeDiv('3')" style="cursor:hand;"><div align="center">开发商信息</div></td>
          <td width="193" height="28" class="m_up_bg" id="td_5" onclick="javascript:ChangeDiv('5')" style="cursor:hand;"><div align="center">使用协议</div></td>
        </tr>
        <tr>
          <td colspan="5" valign="top"><div id="div_1" style="display:">
              <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1">
                <tr>
                  <td class="about">
				  产品名称：风讯网站内容管理系统<span class="STYLE1">.NET</span>版本<br />
				产品英文：FoosunCMS For <span class="STYLE1">.NET</span><br />
				版本类型：<span class="STYLE1">dotNETCMS</span>(普通新闻站，企业站)、<span class="STYLE1">NewsCMS</span>(门户行业站，传媒版)<br />
				产品版本：<%Response.Write(Foosun.Config.verConfig.Productversion); %> <br />
				本软件是在 FoosunCMS 基于Microsoft .NET Framework SDK v2.0的基础上完成的。版权归风讯公司所有<br>
              未经 风讯公司的授权许可不得擅自发布该软件。<br>
              版权所有(c) 2002-2007 Foosun Inc. 保留所有权利。Foosun 是 风讯公司的注册商标。<br>
            警告: 本计算机程序受著作权法和国际公约的保护，未经授权擅自复制或散布本程序的部分或全部，将承受严厉的民事和刑事处罚，对已知的违反者将给予法律范围内的全面制裁。
</td>
                </tr>
              </table>
          </div>
              <div id="div_2" style="display:none">
                <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1">
                  <tr>
                    <td class="list_link"><label id="linkus1" runat=server /></td>
                  </tr>
                </table>
              </div>
            <div id="div_3" style="display:none">
                <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1">
                  <tr>
                    <td class="about">　　四川风讯科技发展有限公司是一家主营网络软件开发业务的网络高科技股份制公司，位于成都市武侯区惠民街109号双楠嘉园大厦A座2层，风讯公司专业致力于研发互联网综合应用服务的运营平台，面向政府机构、企事业单位和广大个人用户提供：网络应用软件开发、系统集成、电子商务解决方案、程序定制、网站策划、网站建设等业务，拥有互联网著名品牌“风讯”和“FoosunCMS”。公司立足于发挥自身优势，聚集网络精英，本着团结协作、奋力拼搏、勇于创新的开拓精神，力争做软件市场的“主导者”、网络技术的“引领者”、客户服务的“佼佼者”，争创业界一流的网络服务公司。公司网站（www.foosun.net）和技术论坛(bbs.foosun.net)经过一年多的稳定运行，现已成为目前国内颇有影响力的技术服务型网站。
              <p>　　公司开发的《风讯网站内容管理系统》（以下简称：FoosunCMS），   在CMS产品领域里，FoosunCMS已形成内容管理系统整站解决方案。从《风讯网站内容管理系统》FoosunCMSv0410版至今天的《风讯网站内容管理系统》FoosunCMSv3.1.1120升级版，系统经过几次飞跃性改进，在原有的FoosunCMS系列优势上取得的重大突破，更加“傻瓜”化、人性化，更加符合广大用户的需求，从而使得网站的架设与管理变得极其轻松！特别优化的模块化体系结构，强大的HTML静态生成功能，便捷的后台管理，以人为本的设计理念......每一处都显现出与众不同的经典创意和个性化需求完美展现的编程思想。全新内核的FoosunCMSv3.1.1120版的不同版本可以满足从小流量到大流量、从个人到企业各方面应用的要求，为用户提供了一个适用于各种服务器运行环境的高效、全新、快速和优秀的网站解决方案，广泛适应企业、政府、学校等不同群体及个人的建站需要！《风讯网站内容管理系统》的用户面非常广泛，在为数百家企业服务的过程中建立了成熟、稳定的客户服务保障体系，得到国内众多知名企业和政府部门的选择和好评。</p>
              <p>　　系统包括信息采集、整理、分类、审核、发布和管理的全过程，具备完善的信息管理和发布管理功能，是企事业单位网站、内部网站和各类ICP网站内容管理和维护的理想工具。应用该系统，政府各部门可以随时方便地提交需要发布的信息而无须掌握复杂的技术；FoosunCMS已成为国产CMS“第一品牌”。</p>
              <p>　　风讯不断追求技术领先、服务领先、模式领先、业绩领先的发展目标。并凭借其一流的运营管理团队，强大的资金、技术、电信资源优势，吸引了众多知名厂商和合作伙伴，结成了广泛的业务联盟。其不断推出的基于互联网技术的创新应用服务，已成为企业持续高速增长的核心动力。</p>
            <p>　　成功来自协作，信赖源于诚信，风讯正在全力打造互联网应用服务领域的一流品牌,为企事业单位提供信息化和电子商务方面的优质服务，成为“您身边的电子商务专家”！</p></td>
          </td>
                  </tr>
                </table>
            </div>

            <div id="div_5" style="display:none">
                <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1">
                  <tr>
                    <td class="about"> (以下简称《协议》)，如果您在您的PC或服务器上安装我们的产品，即表明您同意了此《协议》。

              <br>
              以下是《协议》所做的描述：

              <br>
              1、本软件产品的版权归风讯科技发展有限公司所有，受《中华人民共和国计算机软件保护条例》等知识产权法律及国际条约与惯例的保护。您获得的只是本软件的使用权。

              <br>
              2、您有权在同意本协议的前提下下载、安装、使用本软件免费版本。<br>
 3、您不得：
　<br>
　　a. 在未得到授权的情况下删除、修改、拷贝本软件及其他副本上一切关于版权的信息；
　<br>
　　b. 销售、出租此软件产品的任何部分；
　<br>
　　c. 从事其他侵害本软件版权的行为；
  <br>
  　　d. <font color="red">免费版本只供用户学习参考使用，不能用于任何性质的商业用途。 未经风讯公司书面允许，任何团体、组织及个人不能对本产品进行2次
  开发。</font>

  <br>
  4、如果您未遵守本条款的任一约定，风讯科技发展有限公司有权立即终止本条款的执行，且您必须立即终止使用本软件并销毁本软件产品的所有副本。这项要求对各种拷贝形式有效。

  <br>
  5、您同意自己承担使用本软件产品的风险，在适用法律允许的最大范围内，风讯科技发展有限公司在任何情况下不就因使用或不能使用本软件产
品所发生的特殊的、意外的、非直接或间接的损失承担赔偿责任。即使已事先被告知该损害发生的可能性。<br>
 6、如使用本系统所添加的任何信息，发生版权纠纷，风讯公司不承担任何责任。<br> 
 7、本条款内容与我国法律法规有不相一致的部分，以国家法律法规的规定为准。

 <br>
 8、风讯科技发展有限公司对本条款有最终解释权。<br>
 9、若您对本协议内容有任何疑问，请与风讯科技发展有限公司联系。


  在您同意授权协议的全部条件后，即可继续 FoosunCMS 的安装。即：您一旦开始安装 FoosunCMS，即被视为完全同意本授权协议的全部内容，如果出现纠纷，我们将根据相关法律和协议条款追究责任。</td>
                  </tr>
                </table>
            </div>
			</td>
        </tr>
        <tr>
          <td height="1" colspan="5" valign="top" bgcolor="A4B2BD"></td>
        </tr>
      </table>
      <br />
<br />
 <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg" style="height: 76px">
   <tr>
     <td align="center"><label id="copyright" runat=server /></td>
   </tr>
 </table>
<script language="javascript">
	function ChangeDiv(ID)
	{
		document.getElementById('td_1').className='m_up_bg';
		document.getElementById('td_2').className='m_up_bg';
		document.getElementById('td_3').className='m_up_bg';
		document.getElementById('td_5').className='m_up_bg';
		document.getElementById("td_"+ID).className='m_down_bg';

		document.getElementById("div_1").style.display="none";
		document.getElementById("div_2").style.display="none";
		document.getElementById("div_3").style.display="none";
		document.getElementById("div_5").style.display="none";
		document.getElementById("div_"+ID).style.display="";
	}

</script></body>
</html>

