<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Sys_checkFiles" Codebehind="checkFiles.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Public.js"></script>
<script language="javascript" type="text/javascript">
<!--
function CheckFile()
{
    var chb = document.getElementById('ChbIsLocal');
    var fl = document.getElementById('localfile');
    if(!chb.checked)
    {
        chb.checked = true;
        document.getElementById('divselfile').style.display = '';
        fl.focus();
        alert('请选择本地文件作为对比标准!');
        return false;
    }
    if(fl.value.trim() == '')
    {
        alert('请选择本地文件作为对比标准!');
        fl.focus();
        return false;
    }
}
function RemoveFile()
{
    document.getElementById('ChbIsLocal').checked = false;
}
function LoadMe(i)
{
    var chb = document.getElementById('ChbIsLocal');
    var obj = document.getElementById('divselfile');
    if(chb != null)
    {
        if(chb.checked)
            obj.style.display = '';
        else
            obj.style.display = 'none';
    }
}
function Hint()
{
    alert();
}
//-->
</script>
</head>
<body onload="LoadMe(Math.random());">
    <form id="form1" runat="server">
        <table id="top1" width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td height="1" colspan="2">
                </td>
            </tr>
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px;">
                    文件对比</td>
                <td width="43%">
                    位置导航：<a href="../main.aspx" class="list_link" target="sys_main">首页</a><img alt=""
                        src="../../sysImages/folder/navidot.gif" border="0" />文件对比</td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="PnlStart">
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td style="padding-left: 12px;">
                        <a href="javascript:getDownfilexml();" class="list_link">从风讯官方站下载文件包</a>
                        <asp:LinkButton runat="server" CssClass="list_link" Text="重新生成文件包" ID="LnkDownload" OnClick="LnkDownload_Click"></asp:LinkButton>
                        <a href="CreateCheckfile.aspx" target="_blank" onclick="Hint();" class="list_link"></a>
                     </td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
                <tr class="TR_BG_list">
                    <td style="padding-left: 12px;">
                        <asp:CheckBox ID="ChbIsLocal" onclick="sLocal(this);" Text="选择本地的包(XML包,请使用风讯规定的格式的xml文件)"
                            runat="server" /><font color="red">为了安全,请将对比文件与网站分开,存放于本地!</font>
                        <br />
                        <br />
                        <div id="divselfile">
                        <asp:FileUpload runat="server" ID="localfile" />
                            <br />
                            <br />
                        </div>
                    </td>
                </tr>
                <tr class="TR_BG_list">
                    <td align="center">
                        <asp:Button ID="BtnCompare" runat="server" CssClass="form" Text="以本地文件作为标准对比" OnClick="BtnCompare_Click" />
                        <asp:Button ID="BtnOnline" runat="server" CssClass="form" Text="以官方文件作为标准对比" OnClick="BtnOnline_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="PnlResult">
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td style="padding-left: 12px;">
                        <a href="checkFiles.aspx" class="list_link">返回</a>
                        <asp:LinkButton runat="server" ID="LnkAll" Text="全部显示" CssClass="list_link" OnClick="LnkAll_Click"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="LnkDiff" Text="只显示差异文件" CssClass="list_link" OnClick="LnkDiff_Click"></asp:LinkButton>
                    </td>
                </tr>
                 <tr>
                    <td style="padding-left: 12px;"><font color="red">注:</font>
                '√'----两边大小时间完全相同 '<font color="red">≠</font>'----两边大小不相同 '<font color="sienna">≈</font>'----两边仅仅时间不同<br />
                '<font style="border-left:inherit; text-decoration: line-through;color:Blue;">×</font>'----表示本地有,但是标准文件里没有的文件
                '<font color="gray">×</font>'----表示本地没有的文件
                文件的大小的单位为字节(byte)
                  </td>
                </tr>
            </table>
            <table runat="server" id="TabResult" width="98%" border="0" align="center" cellpadding="4"
                cellspacing="1" bgcolor="#ffffff" class="table">
                <tr class="TR_BG" style="color: Green">
                    <td align="center" class="sys_topBg" width="2%">
                    </td>
                    <td align="center" class="sys_topBg" width="38%">
                        文件名</td>
                    <td align="center" class="sys_topBg" width="10%">
                        标准大小</td>
                    <td align="center" class="sys_topBg" width="20%">
                        标准修改时间</td>
                    <td align="center" class="sys_topBg" width="10%">
                        本地大小</td>
                    <td align="center" class="sys_topBg" width="20%">
                        本地修改时间</td>
                </tr>
            </table>
            <table runat="server" id="Table1" width="98%" border="0" align="center" cellpadding="4"
                cellspacing="1" bgcolor="#ffffff" class="table">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="LblStat"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:HiddenField runat="server" ID="HidStandard" />
        </asp:Panel>
        <br />
        <br />
        <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
            style="height: 76px">
            <tr>
                <td align="center">
                    <label id="copyright" runat="server" />
                </td>
            </tr>
        </table>
    </form>
    <iframe id="downloadfromfoosun" src="about:blank" border="0" height="0" width="0"
        style="visibility: hidden"></iframe>
</body>
</html>

<script language="javascript" type="text/javascript">
function getDownfilexml()
{
    if(confirm('您确认要重新从风讯(Foosun.net)下载 [文件包] 吗?\n如果您确认。请点[确定]按钮'))
    {
	    var ifm = document.getElementById("downloadfromfoosun");
	    ifm.src = "<%Response.Write(ReloadURL);%>";
    }
}

function sLocal(obj)
{
   var objdiv = document.getElementById("divselfile");
   if(obj.checked){objdiv.style.display="";}else{objdiv.style.display="none";}                      
}
</script>

