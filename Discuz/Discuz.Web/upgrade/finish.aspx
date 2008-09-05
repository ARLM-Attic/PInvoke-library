<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="finish.aspx.cs" Inherits="Discuz.Install.finish" %>

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
										<td><FONT color="#ffffff">升级完成</FONT></td>
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
                                            <p>您已经升级至 <%=productname %></p>
                                            <p>如果您的论坛使用到了分表，请登录到论坛系统后台 论坛 -> 论坛维护 -> 论坛数据维护 重建分表存储过程</p>
                                            <p>为了您的系统安全,请立即删除upgrade文件夹中的所有文件</p>
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
										<td align="right"><input type="button" onclick="javascript:window.location.href='../index.aspx';" value="进入首页"></td></td>
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
