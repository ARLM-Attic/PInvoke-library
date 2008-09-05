<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="Foosun.Web.Update.FS4.Update" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>风讯4.x升级到dotNETCMS v1.0正式版</title>
<script type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script type="text/javascript" src="../../configuration/js/public.js"></script>
<link href="../styles.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .green{color:green;font-weight:bold;}
</style>
</head>
<body onload="GetPath('1');">
    <form id="form1" runat="server">
      <div style="margin:10px auto 10px auto;width:800px;background-color:#fff;border:1px solid #eeeeee;">
        <div style="float:left;width:200px;"> 
            <img src="../logo.jpg" />
        </div>
        <div style="float:right;margin:15px;">
            <div style="margin:10px auto 10px auto;width:550px;">
            <span style="color:Blue;font-size:16px;font-weight:bold;">风讯4.x升级到dotNETCMS v1.0正式版转换程序。</span>
            </div>
            <div style="margin:10px auto 10px auto;width:550px;">
            <asp:DropDownList ID="isSQL" runat="server" onchange="GetPath(this.value);">
            <asp:ListItem Value="0">ACCESS数据库(数据源,请勿使用物理路径)</asp:ListItem>
            <asp:ListItem Value="1" Selected="True">SQL Server数据库(数据源,sql数据库连接字符串)</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div style="margin:10px auto 10px auto;width:550px;">
            <asp:TextBox ID="Connstr" runat="server" Width="500px">这里填写数据库连接字符串</asp:TextBox>
           <div id="mDB" style="display:none;"> 
                <asp:TextBox ID="mConnstr" runat="server" Width="500px">会员数据库</asp:TextBox> 
            </div> 
            </div>
           <div style="margin:10px auto 10px auto;width:550px;text-align:left;">本程序只适合数据量小于8万条记录的客户，如果超过8万条数据记录，请与风讯官方联系。转换数据包括：新闻，栏目，专题，会员，常规管理。如果您要转换标签，请与官方联系(收费服务)</div> 
           <div id="Results" style="margin:15px auto 10px auto;width:550px;text-align:left;">
           任务： 
           <span id="inews">新闻</span>&nbsp;
           <span id="iclass">栏目</span>&nbsp;
           <span id="ispecial">专题</span>&nbsp;
           <span id="iuser">会员</span>&nbsp;
           <span id="igen">常规管理</span>
           </div>            
           <div id="Resultnews" style="margin:10px auto 10px auto;width:550px;text-align:left;font-size:12px;color:Red;"></div>            
           <div id="Resultclass" style="margin:10px auto 10px auto;width:550px;text-align:left;font-size:12px;color:Red;"></div>            
           <div id="Resultspecial" style="margin:10px auto 10px auto;width:550px;text-align:left;font-size:12px;color:Red;"></div>            
           <div id="Resultuser" style="margin:10px auto 10px auto;width:550px;text-align:left;font-size:12px;color:Red;"></div>            
           <!--<div id="Resultgen" style="margin:10px auto 10px auto;width:550px;text-align:left;font-size:12px;color:Red;"></div>-->            
           <div id="ResultT" style="margin:10px auto 10px auto;width:550px;text-align:left;font-size:12px;color:Red;"></div>            

           <div id="Div1" style="margin:10px auto 10px auto;width:550px;text-align:left;"><input type="button" id="bup" name="bup" value="开始升级" onclick="update();" /></div> 
            <div style="margin:10px auto 10px auto;width:500px;text-align:right;">
            by 轻风云@Foosun Inc.
            </div>        
       </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
function update()
{
   document.getElementById("bup").disabled=true;   
   document.getElementById("bup").value="正在转移新闻数据中...";   
   var dbtye=document.getElementById("isSQL");
   var connstrs=document.getElementById("Connstr");
   var mconnstrs=document.getElementById("mConnstr");
   if(document.getElementById("Connstr").value=="")
   {
        alert("请填写数据库连接字符串，或ACCESS路径");
        document.getElementById("Connstr").focus();
        return false;
   }
   var Action='set=1&type=news&isSQL='+dbtye.value+'&connstr='+escape(connstrs.value)+'&mConnstr='+escape(mconnstrs.value)+'';   var options={ 
                  method:'get', 
                  parameters:Action, 
                  onComplete:function(transport) 
                  { 
                      var returnvalue=transport.responseText; 
                      if (returnvalue.indexOf("??")>-1) 
                     { 
                          document.getElementById("ResultT").innerHTMLL='发生错误'; 
                      } 
                      else 
                     { 
                         document.getElementById("Resultnews").innerHTML=returnvalue;
                         document.getElementById("inews").className="green";
                         setTimeout(preConvert('class'),2000)
                     }
                  } 
               }; 
   new  Ajax.Request('update.aspx?no-cache='+Math.random(),options);
}
//新闻，栏目，专题，会员
function preConvert(v)
{
   var dbtye=document.getElementById("isSQL");
   var connstrs=document.getElementById("Connstr");
   var mconnstrs=document.getElementById("mConnstr");
   var Action='set=1&type='+v+'&isSQL='+dbtye.value+'&connstr='+escape(connstrs.value)+'&mConnstr='+escape(mconnstrs.value)+'';   var options={ 
                  method:'get', 
                  parameters:Action, 
                  onComplete:function(transport) 
                  { 
                      var returnvalue=transport.responseText; 
                      if (returnvalue.indexOf("??")>-1) 
                     { 
                          document.getElementById("ResultT").innerHTMLL='发生错误'; 
                     } 
                      else 
                        { 
                            if(v=='gen')
                            {
                                   //document.getElementById("Resultgen").innerHTML=returnvalue; 
                                   document.getElementById("ResultT").innerHTML="<span style=\"color:red\">"+returnvalue+"</span>\n<span style=\"color:green\">升级完成</span>";
                                   document.getElementById("bup").disabled=true;   
                                   document.getElementById("bup").value="升级完成";   
                                   document.getElementById("igen").className="green";
                            }
                            else
                            {
                                 switch(v)
                                 {
                                    case "class":
                                       document.getElementById("bup").disabled=true;   
                                       document.getElementById("bup").value="正在转移栏目数据中...";   
                                        document.getElementById("Resultclass").innerHTML=returnvalue;
                                        document.getElementById("iclass").className="green";
                                        setTimeout(preConvert('special'),2000)
                                        break;
                                    case "special":
                                       document.getElementById("bup").disabled=true;   
                                       document.getElementById("bup").value="正在转移专题数据中...";   
                                        document.getElementById("Resultspecial").innerHTML=returnvalue; 
                                        document.getElementById("ispecial").className="green";
                                        setTimeout(preConvert('user'),2000)
     
                                        break;
                                    case "user":
                                       document.getElementById("bup").disabled=true;   
                                       document.getElementById("bup").value="正在转移会员数据中...";   
                                        document.getElementById("Resultuser").innerHTML=returnvalue; 
                                        document.getElementById("iuser").className="green"; 
                                        setTimeout(preConvert('gen'),2000)
                                        break;
                                 }                             
                           }

                         }
                  } 
               }; 
   new  Ajax.Request('update.aspx?no-cache='+Math.random(),options);
}

 function GetPath(v)
{
    if(v=="0")
    {
        document.getElementById("Connstr").value="/foosun_data/fs400.mdb";
        document.getElementById("mConnstr").value="/foosun_data/fs_me.mdb";
        document.getElementById("mDB").style.display="";
        
    }
    else
    {
        document.getElementById("Connstr").value="server=192.168.1.10;uid=sa;pwd=sa;database=dotNETCMS;";
        document.getElementById("mDB").style.display="none";
    }
} 
</script>