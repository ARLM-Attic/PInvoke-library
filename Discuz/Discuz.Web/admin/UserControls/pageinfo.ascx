﻿<%@ Control Language="c#" AutoEventWireup="false" CodeBehind="pageinfo.ascx.cs" Inherits="Discuz.Web.Admin.pageinfo" %>
<div id="<%=this.ID %>" style="clear:both; margin-top:10px; position:relative;background:url('<%=GetInfoImg() %>') no-repeat 20px 15px;border:1px dotted #DBDDD3; background-color:#FDFFF2;
            margin-bottom:10px; padding:15px 10px 10px 56px; text-align: left; font-size: 12px;">
    <span class="infomessage">
        <a href="#" onclick="document.getElementById('<%=this.ID %>').style.display='none';"><img src="../images/off.gif" alt="关闭" style="margin-left:3px;"/></a>
    </span>
    <%=this.Text %>
</div>
<div style="clear:both;"></div>