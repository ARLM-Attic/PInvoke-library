<%@ Page Language="C#" AutoEventWireup="true" Inherits="Discuz.Space.Admin.EditModuleDef" %>
<%@ Register TagPrefix="cc2" Namespace="Discuz.Control" Assembly="Discuz.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑模块</title>
		<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
		<link href="../styles/tab.css" type="text/css" rel="stylesheet" />
	    <script type="text/javascript" src="../js/common.js"></script>
	    <script type="text/javascript" src="../js/tabstrip.js"></script>
	    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />        
        <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="ManagerForm">
        <fieldset>
		<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">模块编辑</legend>
		<table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td  class="panelbox" align="left" width="50%">
                    <table width="100%">
                        <tr>
					        <td style="width: 70px">模块名称:</td>
					        <td>
						        <cc2:TextBox id="modulename" runat="server" CanBeNull="必填"  IsReplaceInvertedComma="false" size="20"  MaxLength="20"></cc2:TextBox>
					        </td>
                        </tr>
                        <tr>
					        <td>所属分类:</td>
					        <td>
                                <cc2:DropDownList ID="category" runat="server"></cc2:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td  class="panelbox" align="right" width="50%">
                    <table width="100%">
				        <tr>
					        <td style="width: 70px">模块类型:</td>
					        <td>
						        <asp:literal id="moduletype" runat="server"></asp:literal></td>
				        </tr>
				        <tr>
					        <td>配置文件:</td>
					        <td>
						        <asp:literal id="configfile" runat="server"></asp:literal></td>
				        </tr>
                    </table>
                </td>
            </tr>
        </table>
		</fieldset>
		<div align="center">
		    <cc2:Button ID="btnSave" runat="server" OnClick="btnSave_Click" />&nbsp;&nbsp;
		    <button type="button" class="ManagerButton" id="Button3" onclick="window.location='space_moduledefmanage.aspx';"><img src="../images/arrow_undo.gif"/> 返 回 </button>
		</div>
		</div>
    </form>	
    <%=footer%>
</body>
</html>
