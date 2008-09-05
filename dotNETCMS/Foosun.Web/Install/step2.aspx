<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="step2.aspx.cs" Inherits="Foosun.Web.Install.step2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Foosun.Install.Config.title%></title>
           <%=Foosun.Install.Config.style%>
</head>
	<body>
		<table width="700" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#666666">
			<tr>
				<td bgcolor="#ffffff"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
						<tr>
							<td colspan="2" bgcolor="#333333"><table width="100%" border="0" cellspacing="0" cellpadding="8">
									<tr>
										<td background="image/01.jpg"><FONT color="#ffffff">环境检测</FONT></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td width="180" valign="top"><%=Foosun.Install.Config.logo%></td>
							<td width="520" valign="top"><BR>
								<BR>
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="90%" align="center" border="0">
									<TR>
										<TD>
											<P>现在对您的运行环境进行检测，以确认您的环境符合要求.</P>
											<P><font color="red">注意:</font>如果出现目录或文件没有写入和删除权限情况,请选择该目录或文件->右键属性->安全->添加,
											在"输入对象名称来选择"中输入"Network Service",点击"确定".选择"组或用户名称"中"Network Service"用户组,在下面
											"Network Service"的权限中勾选"修改"的"允许"复选框,点击"确定"后再次重新刷新本页面继续.</P>
											<%
												 bool err = false;
											     string result = Foosun.Install.Config.InitialSystemValidCheck(ref err);
											     Response.Write("<font color=red>"+result+"</font>");
											%>
										</TD>
									</TR>
								</TABLE>
								
								<P></P>
							</td>
						</tr>
						<%if(!err){%>
						<tr>
							<td>&nbsp;</td>
							<td><table width="90%" border="0" cellspacing="0" cellpadding="8">
									<tr>
										<td align="right"><input type="button" onclick="javascript:window.location.href='step3.aspx';" value="下一步"></td>
									</tr>
								</table>
							</td>
						</tr>
						<%}%>
					</table>
				</td>
			</tr>
		</table>
        <%=Foosun.Install.Config.corpRight%>
	</body>
</html>
