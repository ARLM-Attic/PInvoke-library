<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="step1.aspx.cs" Inherits="Foosun.Web.Install.setp1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Foosun.Install.Config.title%></title>
           <%=Foosun.Install.Config.style%>
</head>
<body>
    <table width="700" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#666666">
        <tr>
            <td bgcolor="#ffffff">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" bgcolor="#333333">
                            <table width="100%" border="0" cellspacing="0" cellpadding="8">
                                <tr>
                                    <td background="image/01.jpg">
                                        <font color="#ffffff">
                                            <%=Foosun.Install.Config.producename%> 安装协议
                                        </font>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="180" valign="top">
                            <%=Foosun.Install.Config.logo%>
                        </td>
                        <td width="520" valign="top">
                            <br>
                            <br>
                            <table id="Table2" cellspacing="1" cellpadding="1" width="90%" align="center" border="0">
                                <tr>
                                    <td>
                                        <%=Foosun.Install.Config.regprotocol%>
                                    </td>
                                </tr>
                            </table>
                            <p>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <table width="90%" border="0" cellspacing="0" cellpadding="8">
                                <tr>
                                    <td align="right">
                                      <input type="button" onclick="javascript:document.getElementById('Next').disabled=disabled;" value="同意注册协议">  <input type="button" onclick="javascript:window.location.href='step2.aspx';" value="下一步" disabled="fales" id="Next"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%=Foosun.Install.Config.corpRight%>
</body>
</html>