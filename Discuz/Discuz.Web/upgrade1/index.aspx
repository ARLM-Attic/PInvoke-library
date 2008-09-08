<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Discuz.Install.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>	    
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	    <title>Discuz!NT 升级向导</title>
	    <link rel="stylesheet" href="css/styles.css" type="text/css" media="all"  />
	</head>
	<body>
		<table width="700" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#666666">
			<tr>
				<td bgcolor="#ffffff"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
						<tr>
							<td colspan="2" bgcolor="#333333"><table width="100%" border="0" cellspacing="0" cellpadding="8">
									<tr>
										<td><FONT color="#ffffff">欢迎使用升级向导</FONT></td>
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
											<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 欢迎您升级为 <%=productname%></P>
											<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 本向导将协助您一步步的对 <%=preproduct%> 进行升级</P>
											<P>&nbsp;&nbsp;&nbsp; 强烈建议您在运行本向导前仔细阅读程序包中的《升级说明》文档, 如果您已经阅读过, 请点击下一步.</P>
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
										<td align="right"><input type="button" onclick="javascript:window.location.href='upgrade.aspx';" value="下一步"></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</html>
