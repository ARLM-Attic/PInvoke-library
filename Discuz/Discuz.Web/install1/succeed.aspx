<%@ Page Language="C#" AutoEventWireup="true" Inherits="Discuz.Install.succeed" %>
<%@ Import Namespace="Discuz.Common" %>
<%@ Import Namespace="Discuz.Install" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<%=SetupPage.header%>
<body>
    <table width="700" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#666666">
        <tr>
            <td bgcolor="#ffffff">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" bgcolor="#333333">
                            <table width="100%" border="0" cellspacing="0" cellpadding="8">
                                <tr>
                                    <td>
                                        <span style="color: #ffffff">安装成功</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="180" valign="top">
                            <%=SetupPage.logo%>
                        </td>
                        <td width="520" valign="top">
                            <br />
                            <br />
                            <table id="Table2" cellspacing="1" cellpadding="1" width="90%" align="center" border="0">
                                <tr>
                                    <td>
                                        <img src="images/succeed.jpg" alt="安装成功" style="margin-left:140px" /><br />
                                        <br />
                                        恭喜! 您已经成功安装<%=SetupPage.producename%><br /><br />
                                        请您牢记以下您的个人信息<br /><br />
                                      用户名：<%=Session["SystemAdminName"]%><br />
                                       密码：<%=Session["SystemAdminPws"]%><br />
                                        E-Mail：<%=Session["SystemAdminEmail"]%><br />
                                        <br />
                                        接下来您将可以访问首页。<br />
                                                                                如果进行系统设置，请在"登录"前台后，选择"系统设置"选项。</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <table width="90%" border="0" cellspacing="0" cellpadding="8">
                                <tr>
                                    <td align="right">
                                        <input type="button" onclick="javascript:window.location.href='../index.aspx';" value="进入首页"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%=SetupPage.footer%>
</body>
</html>
