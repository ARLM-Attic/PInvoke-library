<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="step3.aspx.cs" Inherits="Foosun.Web.Install.step3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title><%=Foosun.Install.Config.title%></title>
<%=Foosun.Install.Config.style%>
<script type="text/javascript" src="../configuration/js/Prototype.js"></script>
<script type="text/javascript" src="../configuration/js/public.js"></script>
</head>
<body onload="SelectChange('SqlServer')">
<div class="setindexstyle" id="getLoading" style="display:none;" runat="server">
	<div style="font-family:Arial;line-height:22px;text-align:left;font-size:12px;font-weight:normal;color:red;padding:30px 30px 10px 30px;border:3px #000 solid;background-color:#DFE7ED;margin:auto 10px auto 10px;width:400px;height:100px;" id="MessageID">
	</div>
</div>
<form id="form1" name="form1" runat="server" method="post">
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
                                            创建数据库
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
                            <br />
                            <br />

                            <table cellspacing="0" cellpadding="8" width="100%" border="0">

                                <tr>
                                    <td width="30%" style="background-color: #f5f5f5">数据库类型:</td>
                                    <td style="background-color:#f5f5f5;width: 352px">
                                         <asp:DropDownList ID="DbType" runat="server" onchange="javascript:SelectChange(this.value);" 
                                            Width="155px">
                                            <asp:ListItem Value="SqlServer" Selected="True">SqlServer</asp:ListItem>
                                            <asp:ListItem Value="Access">Access</asp:ListItem>
                                        </asp:DropDownList>*<span id="msgDbType"></span>
                                        </td>
                                </tr>
                                
                                <tr id="tr1" style="display:none;">
                                    <td style="background-color: #f5f5f5">
                                        服务器名或IP地址:</td>
                                    <td style="background-color: #f5f5f5; width: 352px;">
                                        <asp:TextBox ID="datasource" runat="server"  Text="(local)"
                                            Width="150" Enabled="true"></asp:TextBox>*<span id="msg1"></span></td>
                                </tr>
                                <tr id="tr2" style="display:none"> 
                                    <td style="background-color: #f5f5f5">
                                        数据库名称:</td>
                                    <td style="background-color: #f5f5f5; width: 352px;">
                                        <asp:TextBox ID="initialcatalog" runat="server" Text="dotNETCMS"
                                            Width="150" Enabled="true"></asp:TextBox>*<span id="msg2"></span></td>
                                </tr>
                                <tr id="tr3" style="display:none">
                                    <td style="background-color: #f5f5f5">
                                        数据库用户名称:</td>
                                    <td style="background-color: #f5f5f5; width: 352px;">
                                        <asp:TextBox ID="userid" runat="server" Width="150" Text="sa"
                                            Enabled="true"></asp:TextBox>*<span id="msg3"></span></td>
                                </tr>
                                <tr id="tr4" style="display:none">
                                    <td style="background-color: #f5f5f5">
                                        数据库用户口令:</td>
                                    <td style="background-color: #f5f5f5; width: 352px;">
                                        <asp:TextBox ID="password" runat="server"  Width="150" Enabled="true"
                                            TextMode="Password"></asp:TextBox>*<span id="msg4"></span></td>
                                </tr>     
                                
                                <tr id="tr5" style="display:block;">
                                    <td style="background-color: #f5f5f5">
                                        产品序列号:</td>
                                    <td style="background-color: #f5f5f5; width: 352px;">
                                        <asp:TextBox ID="SN" runat="server"  Width="250" Enabled="true" MaxLength="30"></asp:TextBox>*<span id="Span1"></span></td>
                                </tr> 
                                
                                <tr id="tr6" style="display:none;">
                                    <td style="background-color: #f5f5f5">
                                        主数据库路径:</td>
                                    <td style="background-color: #f5f5f5; width: 352px;">
                                        <asp:TextBox ID="FoosunPath" runat="server"  text="/database/dotnetCMS.mdb" Width="250" Enabled="true" MaxLength="30"></asp:TextBox>*<span id="msg5"></span></td>
                                </tr> 
                                <tr id="tr8" style="display:none;">
                                    <td style="background-color: #f5f5f5">
                                        帮助数据库路径:</td>
                                    <td style="background-color: #f5f5f5; width: 352px;">
                                        <asp:TextBox ID="helpkeypath" runat="server" text="/database/fs_help.mdb"  Width="250" Enabled="true" MaxLength="30"></asp:TextBox>*<span id="Span2"></span></td>
                                </tr> 
                                <tr id="tr9" style="display:none;">
                                    <td style="background-color: #f5f5f5">
                                        采集数据库路径:</td>
                                    <td style="background-color: #f5f5f5; width: 352px;">
                                        <asp:TextBox ID="collectpath" runat="server" text="/database/fs_collect.mdb"  Width="250" Enabled="true" MaxLength="30"></asp:TextBox>*<span id="Span3"></span></td>
                                </tr> 
                                                                                          
                                <tr id="tr7" style="display:none">
                                    <td>
                                        <input onclick="javascript:expandoptions();" type="button" value="扩展设置>>" /></td>
                                    <td style="width: 352px">
                                    </td>
                                </tr>      
                                <tr id="tabfix" style="display:none">
                                    <td>
                                        数据库表名称前缀:</td>
                                    <td>
                                        <asp:TextBox ID="tableprefix" runat="server" Width="150" Enabled="true" Text="Fs_"  onblur="checkid(this,'tableprefix')"></asp:TextBox>*<span id="msgtableprefix"></span></td>
                                </tr>                                
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <table width="90%" border="0" cellspacing="0" cellpadding="8">
                                <tr>
                                    <td align="right">
                                        <input type="button"  id="cID" value="创建数据库" onclick="showLoading();" />
                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    <%=Foosun.Install.Config.corpRight%>
</body>
</html>
<script language="javascript" type="text/javascript">
function closediv()
{
    document.getElementById("getLoading").style.display="none";
    document.getElementById("cID").disabled=false; 
    document.getElementById("cID").value="创建数据库";
}
function showDivID(Err)
{
    document.getElementById("cID").disabled=false;
    document.getElementById("cID").value="创建数据库";
    document.getElementById("getLoading").style.display="block";
   document.getElementById("MessageID").innerHTML=""+Err+""; 
}
function showLoading()
{
    var gDbType=document.getElementById("DbType");
    
    var gdatasource=document.getElementById("datasource");
    var ginitialcatalog=document.getElementById("initialcatalog");
    var guserid=document.getElementById("userid");
    var gpassword =document.getElementById("password");
    var gtableprefix =document.getElementById("tableprefix"); 
    var gSN=document.getElementById("SN");
    var foosunPath=document.getElementById("foosunPath");
    var helpkeyPath=document.getElementById("helpkeyPath");
    var collectPath= document.getElementById("collectPath");
    if (document.getElementById("DbType").value == "0")
    {
        alert('请选择数据库类型!');
        document.getElementById("DbType").focus();
        return false;
    }
    
    if(gDbType.value.toLowerCase()=="access")
    {
        if(foosunPath.value=="")
        {
            alert('请填写主数据库路径');
            foosunPath.focus();
            return false;
        }
        if(helpkeyPath.value=="")
        {
            alert('请填写帮助数据库路径');
            helpkeyPath.focus();
            return false; 
        }
        if(collectPath.value=="")
        {
            alert('请填写采集数据库路径');
            collectPath.focus();
            return false; 
        }
        document.getElementById("getLoading").style.display="block";
        document.getElementById("MessageID").innerHTML="正在写配置信息";
        //document.getElementById("cID").disabled=true;
       // document.getElementById("cID").value="正在创建数据库....";
        var action="foosunPath="+foosunPath.value+"&helpkeyPath="+helpkeyPath.value+"&collectPath="+collectPath.value;
        var options={
            method:'get',
            parameters:action, 
            onComplete:function(transport) 
            { 
                      var returnvalue=transport.responseText; 
                      if (returnvalue.indexOf("??")>-1) 
                          document.getElementById("MessageID").innerHTMLL='发生错误'; 
                      else 
                         document.getElementById("MessageID").innerHTML=returnvalue;
            } 
        };
        //alert(action);
        new  Ajax.Request('step3.aspx?no-cache='+Math.random(),options);
        return true;
    }
    
    if (gdatasource.value == "")
    {
        alert('服务器名或IP地址不能为空!');
        gdatasource.focus();
        return false;
    }
    if (ginitialcatalog.value == "")
    {
        alert('数据库名称不能为空!');
        ginitialcatalog.focus();
        return false;
    }
    if (guserid.value == "")
    {
        alert('数据库用户名称不能为空!');
        guserid.focus();
        return false;
    }
    if (gpassword.value == "")
    {
        alert('数据库用户密码不能为空!');
        gpassword.focus();
        return false;
    }
    if (gtableprefix.value == "")
    {
        alert('数据库表名称前辍不能为空!');
        gtableprefix.focus();
        return false;
    }        
    
    if(gSN.value=="")
    {
        alert('请填写序列号!');
        gSN.focus();
        return false;
   }
     if(gSN.value.length>=30||gSN.value.length<25)
    {
        alert('请正确填写序列号!');
        gSN.focus();
        return false;
    }
     if(gSN.value.indexOf("-")==-1)
    {
        alert('请正确填写序列号!');
        gSN.focus();
        return false;
    }
    
    document.getElementById("getLoading").style.display="block";
    document.getElementById("MessageID").innerHTML="正在验证序列号，并执行SQL语句进行数据库创建....请耐心等待。<br /><br />根据您的网络,这可能要几十秒或者几分钟。";
    document.getElementById("cID").disabled=true;
    document.getElementById("cID").value="正在创建数据库....";
   var Action='sn='+gSN.value+'&set=1&DbType='+gDbType.value+'&datasource='+gdatasource.value+'&initialcatalog='+ginitialcatalog.value+'&userid='+guserid.value+'&password='+gpassword.value+'&gtableprefix=fs_';  
    var options={ 
                  method:'get', 
                  parameters:Action, 
                  onComplete:function(transport) 
                  { 
                      var returnvalue=transport.responseText; 
                      if (returnvalue.indexOf("??")>-1) 
                          document.getElementById("MessageID").innerHTMLL='发生错误'; 
                      else 
                         document.getElementById("MessageID").innerHTML=returnvalue;
                  } 
               }; 
   new  Ajax.Request('step3.aspx?no-cache='+Math.random(),options);
}
function SelectChange(value)
{
    if(value=="SqlServer")
    {
        document.getElementById("tr1").style.display='';
        document.getElementById("tr2").style.display='';
        document.getElementById("tr3").style.display='';
        document.getElementById("tr4").style.display='';
        document.getElementById("tr6").style.display='none';
        document.getElementById("tr8").style.display='none';
        document.getElementById("tr9").style.display='none';
        document.getElementById("tr7").style.display='none';//block
    }
    else
    {
        document.getElementById("tr1").style.display='none';
        document.getElementById("tr2").style.display='none';
        document.getElementById("tr3").style.display='none';
        document.getElementById("tr4").style.display='none';
        document.getElementById("tr5").style.display='none';
        document.getElementById("tr6").style.display='block';
        document.getElementById("tr8").style.display='block';
        document.getElementById("tr9").style.display='block';
        document.getElementById("tr7").style.display='none';
    }
}

function expandoptions()
{   
    if(document.getElementById("tabfix").style.display=='none')
        document.getElementById("tabfix").style.display='';
    else
        document.getElementById("tabfix").style.display='none';
}
</script>