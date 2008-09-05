<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PageNavigator" Codebehind="PageNavigator.ascx.cs" %>
共<asp:Label runat="server" ID="LblRecordCount"/>条记录,共<asp:Label runat="server" ID="LblPageCount"/>页,当前第<asp:Label runat="server" ID="LblPageIndex" />页
 <asp:LinkButton ID="LnkBtnFirst" runat="server" CommandName="Page" OnClick="LnkBtnFirst_Click" CssClass="list_link">首页</asp:LinkButton> 
 
 <asp:LinkButton ID="LnkBtnPrevious" runat="server" CommandName="Page" OnClick="LnkBtnPrevious_Click" CssClass="list_link">上一页</asp:LinkButton> 

 <asp:LinkButton ID="LnkBtnNext" runat="server" CommandName="Page" OnClick="LnkBtnNext_Click" CssClass="list_link">下一页</asp:LinkButton> 
 
 <asp:LinkButton ID="LnkBtnLast" runat="server" CommandName="Page" OnClick="LnkBtnLast_Click" CssClass="list_link">尾页</asp:LinkButton> 
<asp:textbox id="txtNewPageIndex" runat="server" width="20px" CssClass="form"/>
<asp:linkbutton id="LnkBtnGoto" runat="server" causesvalidation="False" commandargument="-1" commandname="Page" text="转到此页" OnClick="LnkBtnGoto_Click" CssClass="list_link" />
