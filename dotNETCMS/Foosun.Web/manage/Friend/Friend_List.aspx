<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_Friend_Friend_List"
    Codebehind="Friend_List.aspx.cs" %>

<%@ Register Src="~/controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css"
        rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>

    <script language="JavaScript" type="text/javascript" charset="gb2312" src="../../configuration/js/Public.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="toptable">
            <tr>
                <td width="57%" class="sysmain_navi" style="padding-left: 14px" height="30">
                    友情连接</td>
                <td width="43%" class="topnavichar" style="padding-left: 14px">
                    <div align="left">
                        位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt=""
                            src="../../sysImages/folder/navidot.gif" border="0" />友情连接</div>
                </td>
            </tr>
        </table>
        <div id="ShowNavi" runat="server" />
        <div id="NoContent" runat="server" />
        <div id="NoContent_Link" runat="server" />
        <%
            string type = Request.QueryString["type"];
            if (type == "pram")
            {
        %>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
            <tr class="TR_BG_list">
                <td class="list_link" colspan="2">
                    <strong>友情连接参数设置</strong></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 257px">
                    是否开放友情连接申请：</td>
                <td align="left" class="list_link">
                    <asp:CheckBox ID="IsOpen" runat="server" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendParam_0001',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 257px">
                    注册会员才能申请：</td>
                <td align="left" class="list_link">
                    <asp:CheckBox ID="IsRegister" runat="server" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendParam_0002',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 257px">
                    友情连接图片最大及最小尺寸：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="ArrSize" runat="server" CssClass="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendParam_0003',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 244px">
                    申请连接须知：<br />
                    (支持html格式)</td>
                <td align="left" class="list_link">
                    <textarea id="Content" runat="server" style="width: 358px; height: 137px" class="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendParam_0004',this)">
                        帮助</span></td>
            </tr>
               <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 257px">
                    文字连接是否打开新窗口：</td>
                <td align="left" class="list_link">
                    <asp:CheckBox ID="isBlank" runat="server" /></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 257px">
                    图片连接是否打开新窗口：</td>
                <td align="left" class="list_link">
                    <asp:CheckBox ID="isImgBlank" runat="server" /></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 257px">
                    申请的连接是否要审核：</td>
                <td align="left" class="list_link">
                    <asp:CheckBox ID="isLock" runat="server" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendParam_0005',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="center" colspan="2" class="list_link">
                    <label>
                        <input type="submit" name="Save" value=" 提 交 " class="form" id="SavePram" runat="server"
                            onserverclick="SavePram_ServerClick" />
                    </label>
                    &nbsp;&nbsp;
                    <label>
                        <input type="reset" name="Clear" value=" 重 填 " class="form" id="ClearPram" runat="server" />
                    </label>
                </td>
            </tr>
        </table>
        <%
            }
        %>
        <%
            if (type == "class")
            {
        %>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1">
            <tr>
                <td height="18" style="width: 45%" colspan="2" align="right" class="list_link">
                    <a href="?type=add_class" class="topnavichar">添加分类</a>&nbsp;┊&nbsp;
                    <asp:LinkButton ID="delP_class" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所选信息吗?\n其下的子类也将被删除!')){return true;}return false;}"
                        OnClick="delP_class_Click">批量删除</asp:LinkButton>
                    &nbsp;┊&nbsp;
                    <asp:LinkButton ID="delall_class" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除全部信息吗?\n其下的子类也将被删除!')){return true;}return false;}"
                        OnClick="delall_class_Click">删除全部</asp:LinkButton>
                    <input type="checkbox" id="friend_checkbox1" value="-1" name="friend_checkbox1" onclick="javascript:return selectAll(this.form,this.checked);" /></td>
            </tr>
        </table>
        <asp:Repeater ID="DataList1" runat="server">
            <HeaderTemplate>
                <table width="98%" border="0" align="center" cellpadding="3" cellspacing="1" class="table">
                    <tr class="TR_BG">
                        <td align="center" valign="middle" class="sys_topBg">
                            类别名称</td>
                        <td align="center" valign="middle" class="sys_topBg">
                            描述</td>
                        <td align="center" valign="middle" class="sys_topBg">
                            操作</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <%#((DataRowView)Container.DataItem)["Colum"]%>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div align="right" style="width: 98%">
            <uc1:PageNavigator ID="PageNavigator1" runat="server" />
        </div>
        <%
            }
        %>
        <%
            if (type == "add_class")
            {
        %>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table"
            id="Add_Class">
            <tr class="TR_BG_list">
                <td class="list_link" colspan="2">
                    <strong>新增友情连接分类信息</strong></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 171px">
                    父类编号：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="ParentID" runat="server" Enabled="false" CssClass="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendClass_001',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 171px">
                    类别名称：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="ClassCName" runat="server" onChange="javascript:GetPY1(this);" CssClass="form" />
                    <span class="reshow">(*)</span> <span class="helpstyle" style="cursor: help;" title="点击查看帮助"
                        onclick="Help('H_friendClass_0001',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 171px">
                    英文名称：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="ClassEName" runat="server" CssClass="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendClass_0002',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 171px">
                    分类说明：</td>
                <td align="left" class="list_link">
                    <textarea id="Content_Class" runat="server" style="width: 266px; height: 99px" class="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendClass_0003',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="center" colspan="2" class="list_link">
                    <label>
                        <input type="submit" name="SaveA" value=" 提 交 " class="form" id="SaveAddClass" runat="server"
                            onserverclick="SaveAddClass_ServerClick" />
                    </label>
                    &nbsp;&nbsp;
                    <label>
                        <input type="reset" name="ClearA" value=" 重 填 " class="form" id="ClearAddClass" runat="server" />
                    </label>
                </td>
            </tr>
        </table>
        <%
            }
        %>
        <%
            if (type == "edit_class")
            {
        %>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table"
            id="Edit_Class">
            <tr class="TR_BG_list">
                <td class="list_link" colspan="2">
                    <strong>修改友情连接分类信息</strong></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 171px">
                    父类编号：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="ParentIDEdit" runat="server" Enabled="false" CssClass="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendClass_001',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 171px">
                    类别名称：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="ClassCNameEdit" runat="server" CssClass="form" />
                    <span class="reshow">(*)</span> <span class="helpstyle" style="cursor: help;" title="点击查看帮助"
                        onclick="Help('H_friendClass_0001',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 171px">
                    英文名称：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="ClassENameEdit" runat="server" CssClass="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendClass_0002',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link" style="width: 171px">
                    分类说明：</td>
                <td align="left" class="list_link">
                    <textarea id="DescriptionE" runat="server" style="width: 266px; height: 99px" class="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_friendClass_0003',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="center" colspan="2" class="list_link">
                    <label>
                        <input type="submit" name="SaveE" value=" 提 交 " class="form" id="UpdateClass" runat="server"
                            onserverclick="UpdateClass_ServerClick" />
                    </label>
                    &nbsp;&nbsp;
                    <label>
                        <input type="reset" name="ClearE" value=" 重 填 " class="form" id="ResetClass" runat="server" />
                    </label>
                </td>
            </tr>
        </table>
        <%
            }
        %>
        <%
            if (type == "link")
            {
        %>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1">
            <tr>
                <td height="18" style="width: 45%" colspan="2" align="right" class="list_link">
                    <a href="?type=add_link" class="topnavichar">添加连接</a>&nbsp;┊&nbsp;
                    <asp:LinkButton ID="SuoP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认锁定所选信息吗?')){return true;}return false;}"
                        OnClick="SuoP_Click">批量锁定</asp:LinkButton>
                    &nbsp;┊&nbsp;
                    <asp:LinkButton ID="UnsuoP" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认解锁所选信息吗?')){return true;}return false;}"
                        OnClick="UnsuoP_Click">批量解锁</asp:LinkButton>
                    &nbsp;┊&nbsp;
                    <asp:LinkButton ID="delP_link" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除所选信息吗?')){return true;}return false;}"
                        OnClick="delP_link_Click">批量删除</asp:LinkButton>
                    &nbsp;┊&nbsp;
                    <asp:LinkButton ID="delall_link" runat="server" CssClass="topnavichar" OnClientClick="{if(confirm('确认删除全部信息吗?')){return true;}return false;}"
                        OnClick="delall_link_Click">删除全部</asp:LinkButton>
                    <input type="checkbox" id="friend_checkbox_link1" value="-1" name="friend_checkbox_link1"
                        onclick="javascript:return selectAll(this.form,this.checked);" /></td>
            </tr>
        </table>
        <asp:Repeater ID="DataList2" runat="server">
            <HeaderTemplate>
                <table width="98%" border="0" align="center" cellpadding="4" cellspacing="1" class="table">
                    <tr class="TR_BG">
                        <td width="7%" align="center" valign="middle" class="sys_topBg">
                            编号</td>
                        <td width="10%" align="center" valign="middle" class="sys_topBg">
                            站点</td>
                        <td width="10%" align="center" valign="middle" class="sys_topBg">
                            类别</td>
                        <td width="9%" align="center" valign="middle" class="sys_topBg">
                            类型</td>
                        <td width="12%" align="center" valign="middle" class="sys_topBg">
                            作者</td>
                        <td width="9%" align="center" valign="middle" class="sys_topBg">
                            状态</td>
                        <td width="20%" align="center" valign="middle" class="sys_topBg">
                            操作</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="TR_BG_list">
                    <td width="7%" align="center" valign="middle" height="20">
                        <%#((DataRowView)Container.DataItem)[0]%>
                    </td>
                    <td width="10%" align="center" valign="middle">
                        <%#((DataRowView)Container.DataItem)[1]%>
                    </td>
                    <td width="10%" align="center" valign="middle">
                        <%#((DataRowView)Container.DataItem)["class"]%>
                    </td>
                    <td width="9%" align="center" valign="middle">
                        <%#((DataRowView)Container.DataItem)["type"]%>
                        <td width="12%" align="center" valign="middle">
                            <%#((DataRowView)Container.DataItem)["author"]%>
                            <td width="9%" align="center" valign="middle">
                                <%#((DataRowView)Container.DataItem)["lock"]%>
                                <td width="20%" align="center" valign="middle">
                                    <%#((DataRowView)Container.DataItem)["operate"]%>
                                </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div align="right" style="width: 98%">
            <uc1:PageNavigator ID="PageNavigator2" runat="server" />
        </div>
        <%
            }
        %>
        <%
            if (type == "add_link")
            {
        %>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table"
            id="Table1">
            <tr class="TR_BG_list">
                <td class="list_link" colspan="2">
                    <strong>新增友情连接连接信息</strong></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    选择类别：</td>
                <td align="left" class="list_link">
                    <asp:DropDownList ID="SelectClass" runat="server" CssClass="form">
                    </asp:DropDownList>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0001',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    站点名称：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="Name" runat="server" Width="124px" CssClass="form" />
                    <font color="red">(*)</font><span class="helpstyle" style="cursor: help;" title="点击查看帮助"
                        onclick="Help('H_FriendLink_0002',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    连接类型：</td>
                <td align="left" class="list_link">
                    <asp:DropDownList ID="Type" runat="server" CssClass="form" onchange="Select(this.value)">
                        <asp:ListItem Selected="True" Value="1">文字</asp:ListItem>
                        <asp:ListItem Value="0">图片</asp:ListItem>
                    </asp:DropDownList>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0003',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    连接地址：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="Url" runat="server" Width="124px" CssClass="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0004',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    站点说明：</td>
                <td align="left" class="list_link">
                    <textarea id="ContentFri" runat="server" style="width: 240px; height: 104px" class="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0005',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="Waterpicurl" style="display: none">
                <td align="right" class="list_link">
                    图片地址：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="PicUrl" runat="server" CssClass="form" />
                    &nbsp;
                    <label>
                        <input type="button" name="PintPicURLClick" value=" 选择图片 " class="form" id="PintPicURLC"
                            style="width: 84px" onclick="selectFile('pic',document.form1.PicUrl,280,500);document.form1.PicUrl.focus();" />
                    </label>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0006',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    是否锁定：</td>
                <td align="left" class="list_link">
                    <asp:CheckBox ID="Lock" runat="server" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0007',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="left" class="list_link" style="width: 257px" colspan="2">
                    显示高级选项：
                    <asp:CheckBox ID="chkAdvance" runat="server" onclick="DispChange()" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0008',this)">
                        帮助</span></td>
            </tr>
            <tr id="Friend_Link_DisP" style="display: none;" class="TR_BG_list">
                <td colspan="2" class="list_link">
                    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="table">
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    前台会员申请</div>
                            </td>
                            <td class="list_link">
                                <asp:CheckBox ID="IsUser" runat="server" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0009',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    申请人作者(编号)</div>
                            </td>
                            <td class="list_link">
                                <asp:TextBox ID="Author" runat="server" Enabled="false" CssClass="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0010',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    申请人电子邮件</div>
                            </td>
                            <td class="list_link">
                                <asp:TextBox ID="Mail" runat="server" CssClass="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0011',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    申请理由</div>
                            </td>
                            <td class="list_link">
                                <textarea rows="5" id="ContentFor" runat="server" style="width: 240px; height: 104px"
                                    class="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0012',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    其他联系方式</div>
                            </td>
                            <td class="list_link">
                                <textarea rows="5" id="LinkContent" style="width: 240px; height: 104px" runat="server"
                                    class="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0013',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    申请日期</div>
                            </td>
                            <td class="list_link">
                                <asp:TextBox ID="Addtime" runat="server" CssClass="form" />
                                &nbsp;&nbsp;
                                <input type="button" name="starttime" value=" 选择日期 " class="form" id="starttime"
                                    onclick="selectFile('date',document.form1.Addtime,280,500);document.form1.Addtime.focus();" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0014',this)">
                                    帮助</span></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td align="center" colspan="2" class="list_link">
                    <label>
                        <input type="submit" name="SaveAl" value=" 提 交 " class="form" id="SaveLink" runat="server"
                            onserverclick="SaveLink_ServerClick" />
                    </label>
                    &nbsp;&nbsp;
                    <label>
                        <input type="reset" name="ClearAl" value=" 重 填 " class="form" id="CancelLink" runat="server" />
                    </label>
                </td>
            </tr>
        </table>
        <%
            }
        %>
        <%
            if (type == "edit_link")
            {     
        %>
        <table width="98%" border="0" align="center" cellpadding="5" cellspacing="1" class="table"
            id="Table2">
            <tr class="TR_BG_list">
                <td class="list_link" colspan="2">
                    <strong>修改友情连接连接信息</strong></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    选择类别：</td>
                <td align="left" class="list_link">
                    <asp:DropDownList ID="Sclass" runat="server" CssClass="form">
                    </asp:DropDownList>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0001',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    站点名称：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="SiteName" runat="server" Width="124px" CssClass="form" />
                    <font color="red">(*)</font><span class="helpstyle" style="cursor: help;" title="点击查看帮助"
                        onclick="Help('H_FriendLink_0002',this)">帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    连接类型：</td>
                <td align="left" class="list_link">
                    <asp:DropDownList ID="LinkType" runat="server" CssClass="form" onchange="SelectE(this.value)">
                        <asp:ListItem Selected="True" Value="1">文字</asp:ListItem>
                        <asp:ListItem Value="0">图片</asp:ListItem>
                    </asp:DropDownList>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0003',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    连接地址：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="linkUrl" runat="server" Width="124px" CssClass="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0004',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    站点说明：</td>
                <td align="left" class="list_link">
                    <textarea id="siteDesc" runat="server" style="width: 240px; height: 104px" class="form" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0005',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list" id="editP" style="display: none">
                <td align="right" class="list_link">
                    图片地址：</td>
                <td align="left" class="list_link">
                    <asp:TextBox ID="PicUrll" runat="server" CssClass="form" />
                    &nbsp;
                    <label>
                        <input type="button" name="PintPicURLClick" value=" 选择图片 " class="form" id="PintPicURLCe"
                            style="width: 84px" onclick="selectFile('pic',document.form1.PicUrll,280,500);document.form1.PicUrll.focus();" />
                    </label>
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0006',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="right" class="list_link">
                    是否锁定：</td>
                <td align="left" class="list_link">
                    <asp:CheckBox ID="isSuo" runat="server" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0007',this)">
                        帮助</span></td>
            </tr>
            <tr class="TR_BG_list">
                <td align="left" class="list_link" style="width: 257px" colspan="2">
                    显示高级选项：
                    <asp:CheckBox ID="DisAdv" runat="server" onclick="change()" />
                    <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0008',this)">
                        帮助</span></td>
            </tr>
            <tr id="FriAdvance_Edit" style="display: none;" class="TR_BG_list">
                <td colspan="2" class="list_link">
                    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="table">
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    前台会员申请</div>
                            </td>
                            <td class="list_link">
                                <asp:CheckBox ID="isUserr" runat="server" CssClass="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0009',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    申请人作者(编号)</div>
                            </td>
                            <td class="list_link">
                                <asp:TextBox ID="Authorr" runat="server" Enabled="false" CssClass="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0010',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    申请人电子邮件</div>
                            </td>
                            <td class="list_link">
                                <asp:TextBox ID="Emaill" runat="server" CssClass="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0011',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    申请理由</div>
                            </td>
                            <td class="list_link">
                                <textarea rows="5" id="forfor" runat="server" style="width: 240px; height: 104px"
                                    class="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0012',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    其他联系方式</div>
                            </td>
                            <td class="list_link">
                                <textarea rows="5" id="otherl" style="width: 240px; height: 104px" runat="server"
                                    class="form" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0013',this)">
                                    帮助</span></td>
                        </tr>
                        <tr class="TR_BG_list">
                            <td class="list_link">
                                <div align="right">
                                    申请日期</div>
                            </td>
                            <td class="list_link">
                                <asp:TextBox ID="datetime" runat="server" CssClass="form" />
                                &nbsp;&nbsp;
                                <input type="button" name="starttime" value=" 选择日期 " class="form" id="Button2" onclick="selectFile('date',document.form1.datetime,280,500);document.form1.datetime.focus();" />
                                <span class="helpstyle" style="cursor: help;" title="点击查看帮助" onclick="Help('H_FriendLink_0014',this)">
                                    帮助</span></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TR_BG_list">
                <td align="center" colspan="2" class="list_link">
                    <label>
                        <input type="submit" name="SaveAle" value=" 提 交 " class="form" id="EditFriend" runat="server"
                            onserverclick="EditFriend_ServerClick" />
                    </label>
                    &nbsp;&nbsp;
                    <label>
                        <input type="reset" name="ClearAle" value=" 重 填 " class="form" id="EditFriendc" runat="server" />
                    </label>
                </td>
            </tr>
        </table>
        <%
            }
        %>
    </form>
    <br />
    <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
        style="height: 76px">
        <tr>
            <td align="center">
                <label id="copyright" runat="server" />
            </td>
        </tr>
    </table>
</body>

<script language="javascript">
//高级选项
function DispChange()
{
    var objadd = document.getElementById("chkAdvance").checked;
    if(objadd)
    {
      document.getElementById("Friend_Link_DisP").style.display="";
    }
    else
    {
      document.getElementById("Friend_Link_DisP").style.display="none";
    }
}
function change()
{
    var objedit = document.getElementById("DisAdv").checked;
    if(objedit)
    {
        document.getElementById("FriAdvance_Edit").style.display=""; 
    }
    else
    {
       document.getElementById("FriAdvance_Edit").style.display="none";
    }
}
function Select(value)
{
    switch(parseInt(value))
    {
        case 1:
        document.getElementById("Waterpicurl").style.display="none";
        break;
        case 0:
        document.getElementById("Waterpicurl").style.display="";
        break;
    }
}
function SelectE(value)
{
    switch(parseInt(value))
    {
        case 1:
        document.getElementById("editP").style.display="none";
        break;
        case 0:
        document.getElementById("editP").style.display="";
        break;
    }
}

function GetPY1(obj)
{
    if(document.getElementById('ClassCName').value!="")
    {
        document.getElementById('ClassEName').value = GetPY(obj.value);
    }
}
</script>

</html>
