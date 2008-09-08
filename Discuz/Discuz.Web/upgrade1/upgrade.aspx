<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upgrade.aspx.cs" Inherits="Discuz.Install.upgrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
	    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	    <title>Discuz!NT 升级向导</title>
	    <link rel="stylesheet" href="css/styles.css" type="text/css" media="all"  />
	    <script type="text/javascript">
	        function submittoupgrade(submited)
	        {   
	            document.getElementById("btnSubmit").disabled = true;
                document.getElementById("hint").style.display = "block";
	            if (!submited)
	            {   
                    document.forms.item(0).submit();
                }
	            
	        }
	    </script>
	</head>
	<body>
	    <form id="form1" runat="server" >
		<table width="700" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#666666">
			<tr>
				<td bgcolor="#ffffff"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
						<tr>
							<td colspan="2" bgcolor="#333333"><table width="100%" border="0" cellspacing="0" cellpadding="8">
									<tr>
										<td><FONT color="#ffffff">升级 Discuz!NT</FONT></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td width="180" valign="top"><img src="images/logo.jpg" /></td>
							<td width="520" valign="top"><BR>
								<BR>
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="90%" align="center" border="0">
									<TR>
										<TD>
											<P style="text-align: center">系统检测到您当前安装的 Discuz!NT 版本为</P>
                                            <p style="text-align: center">
                                                Discuz!NT <font color="red"><%=baseconfig.Dbtype %></font> 版
                                            </p>
                                            <p style="text-align: center">
                                                如果您认为系统检测错误,您可以在下面手工更改</p>
                                            <p style="text-align: center">
                                                <asp:RadioButtonList ID="rblDbtype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                   <asp:ListItem>Access</asp:ListItem>                                                    
                                                
                                                <asp:ListItem Selected="True">SqlServer</asp:ListItem>
                                                    <asp:ListItem>MySql</asp:ListItem></asp:RadioButtonList>
                                            </p>
                                            <div id="hint" style="display:none;">&nbsp;正在升级, 请稍候...</div>
										</TD>
									</TR>
								</TABLE>
								
								<P></P>
							</td>
						</tr>
						<tr>
							<td>&nbsp;</td>
							<td><table width="90%" border="0" cellspacing="0" cellpadding="8">
									<tr>
										<td align="right">
                                            &nbsp;<input id="btnSubmit" type="button" onclick="submittoupgrade(false);" value="下一步" /></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		</form>
	<%
        if (IsPostBack)
        { 
        %>
            <script type="text/javascript">submittoupgrade(true);</script>
        <%
        }
	 %>
	</body>
</html>
