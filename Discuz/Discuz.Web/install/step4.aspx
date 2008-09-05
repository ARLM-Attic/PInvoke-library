<%@ Register TagPrefix="cc1" Namespace="Discuz.Control" Assembly="Discuz.Control" %>

<%@ Page Language="c#" AutoEventWireup="false" Inherits="Discuz.Install.install1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<script language="javascript" src="../admin/js/common.js"></script>

<%=header%>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="0" width="700" align="center" bgcolor="#666666"
            border="0">
            <tr>
                <td bgcolor="#ffffff">
                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                        <tr>
                            <td bgcolor="#333333" colspan="2">
                                <table cellspacing="0" cellpadding="8" width="100%" border="0">
                                    <tr>
                                        <td>
                                            <font color="#ffffff">安装
                                                <%=producename%>
                                            </font>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" width="180">
                                <%=logo%>
                            </td>
                            <td valign="top" width="520">
                                <%if (Discuz.Common.DNTRequest.GetString("isforceload") == "1")
                                  {%>
                                <div style="display: block; width: 100%">
                                    <%}
                                      else
                                      {%>
                                    <div style="display: none; width: 100%">
                                        <%}%>
                                        <table width="98%">
                                            <tr>
                                                <td width="98%">
                                                    <font color="#ff6633">错误: 无法把设置写入"DNT.config"文件, 您可以将下面文件框内容保存为"DNT.config"文件, 然后通过FTP软件上传到<strong>网站根目录(注意是整个网站的根目录)</strong><br>
                                                    </font>
                                                    <br>
                                                    DNT.config 内容:
                                                    <input type="button" value="复制到剪贴板" accesskey="c" onclick="HighlightAll(this.form.TextBox1)">
                                                    <cc1:TextBox ID="txtMsg" runat="server" Height="180" TextMode="MultiLine" RequiredFieldType="暂无校验"
                                                        Width="98%"></cc1:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                     <p>
                                        <br>
                                        <br />您选择的数据库类型是: <b><%=ViewState["dbtype"].ToString()%></b> <br /><br />
                                        <%switch (ViewState["dbtype"].ToString())
                                          {
                                              case "sqlserver":
                                                  {
                                                      Response.Write("<font color=\"gray\">下列dll文件可以从bin目录中清除.<br> &nbsp;&nbsp;&nbsp;&nbsp;Discuz.Data.Access.dll <br> &nbsp;&nbsp;&nbsp;&nbsp;Discuz.Data.MySql.dll <br> &nbsp;&nbsp;&nbsp;&nbsp;MySql.Data.dll</font>\r\n");
                                                      Response.Write("<br /><br />\r\n");
                                                      Response.Write("<p>系统将会执行如下操作, 这可能需要一些时间......\r\n");
                                                      Response.Write("</p>\r\n");
                                                      Response.Write("<p>\r\n");
                                                      Response.Write("&nbsp; 1.对数据库中已有的旧版Discuz!NT论坛表和存储过程进行删除操作.</p>\r\n");
                                                      Response.Write("<p>\r\n");
                                                      Response.Write("    &nbsp; 2.创建表和存储过程.</p>\r\n");
                                                      Response.Write(" <p>\r\n");
                                                      Response.Write("     &nbsp; 3.初始化数据</p>\r\n");
                                                      Response.Write(" <p>\r\n");
                                                      Response.Write("&nbsp;</p>\r\n");
                                                      break;
                                                  }
                                              case "access":
                                                  {
                                                      Response.Write("<font color=\"gray\">下列dll文件可以从bin目录中清除.<br> &nbsp;&nbsp;&nbsp;&nbsp;Discuz.Data.SqlServer.dll <br> &nbsp;&nbsp;&nbsp;&nbsp;Interop.SQLDMO.dll <br> &nbsp;&nbsp;&nbsp;&nbsp;Discuz.Data.MySql.dll <br> &nbsp;&nbsp;&nbsp;&nbsp;MySql.Data.dll </font>\r\n");
                                                      Response.Write("<br /><br />\r\n");
                                                      Response.Write("<p>系统将会执行如下操作, 这可能需要一些时间......\r\n");
                                                      Response.Write("</p>\r\n");
                                                      Response.Write("<p>\r\n");
                                                      Response.Write("&nbsp; 初始化数据.</p>\r\n");
                                                      Response.Write(" <p>\r\n");
                                                      Response.Write("&nbsp;</p>\r\n");
                                                      break;
                                                  }
                                              case "mysql":
                                                  {
                                                      Response.Write("<font color=\"gray\">下列dll文件可以从bin目录中清除.<br> &nbsp;&nbsp;&nbsp;&nbsp; Discuz.Data.SqlServer.dll <br> &nbsp;&nbsp;&nbsp;&nbsp;Interop.SQLDMO.dll <br> &nbsp;&nbsp;&nbsp;&nbsp;Discuz.Data.Access.dll </font>\r\n");
                                                      Response.Write("<br /><br />\r\n");
                                                      Response.Write("<p>系统将会执行如下操作, 这可能需要一些时间......\r\n");
                                                      Response.Write("</p>\r\n");
                                                      Response.Write("<p>\r\n");
                                                      Response.Write("&nbsp; 1.对数据库中已有的旧版Discuz!NT论坛表进行删除操作.</p>\r\n");
                                                      Response.Write("<p>\r\n");
                                                      Response.Write("    &nbsp; 2.创建表.</p>\r\n");
                                                      Response.Write(" <p>\r\n");
                                                      Response.Write("     &nbsp; 3.初始化数据</p>\r\n");
                                                      Response.Write(" <p>\r\n");
                                                      Response.Write("&nbsp;</p>\r\n");
                                                      break;
                                                  }
                                          }                                             
                                         %>
                                     
                                 </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <table cellspacing="0" cellpadding="8" width="90%" border="0">
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="PrevPage" runat="server" Text="上一步" OnClick="PrevPage_Click"></asp:Button>&nbsp;&nbsp;
                                            <asp:Button ID="ClearDBInfo" runat="server" Text="开始运行" Enabled="true"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%=footer%>
    </form>
</body>
</html>
