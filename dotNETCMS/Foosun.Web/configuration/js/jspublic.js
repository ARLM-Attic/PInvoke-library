// JScript 文件
 //DIG参数
function getTopNum(url,newsid,num,divID)
{
    var Action='newsid='+newsid+'&getNum='+num+'';   var options={ 
                      method:'get', 
                      parameters:Action, 
                      onComplete:function(transport) 
                      { 
                          var returnvalue=transport.responseText; 
                          if (returnvalue.indexOf("??")>-1) 
                              document.getElementById(divID).innerHTML=''; 
                          else 
                              document.getElementById(divID).innerHTML=returnvalue; 
                      } 
                   }; 
       new  Ajax.Request(url+'?no-cache='+Math.random(),options);
}

//公共ajax取数据
function pubajax(url,actionstr,divID)
{
    var Action=actionstr; 
    var options={ 
                      method:'get', 
                      parameters:Action, 
                      onComplete:function(transport) 
                      { 
                          var returnvalue=transport.responseText; 
                          if (returnvalue.indexOf("??")>-1) 
                              document.getElementById(divID).innerHTML=''; 
                          else 
                              document.getElementById(divID).innerHTML=returnvalue; 
                      } 
                   }; 
       new  Ajax.Request(url+'?no-cache='+Math.random(),options);
}


//公共ajax取数据
function pubPostajax(url,actionstr,divID)
{
    document.getElementById(divID).innerHTML="保存中...";
    var Action=actionstr; 
    var options={ 
                      method:'get', 
                      parameters:Action, 
                      onComplete:function(transport) 
                      { 
                          var returnvalue=transport.responseText; 
                          if (returnvalue.indexOf("??")>-1) 
                              document.getElementById(divID).innerHTML='保存失败'; 
                          else 
                              document.getElementById(divID).innerHTML=returnvalue; 
                      } 
                   }; 
       new  Ajax.Request(url+'?no-cache='+Math.random(),options);
}

function GetCommentListContent(urlsitedomain,newsid,page)
{
   var Action='NewsID='+newsid+'&CommentType=getlist&showdiv=0&page='+page;   var options={ 
                  method:'get', 
                  parameters:Action, 
                  onComplete:function(transport) 
                  { 
                      var returnvalue=transport.responseText;
                      if (returnvalue.indexOf("??")>-1) 
                          document.getElementById("CommentlistPage").innerHTML='加载评论列表失败'; 
                      else 
                          document.getElementById("CommentlistPage").innerHTML=returnvalue; 
                  } 
               }; 
   new  Ajax.Request(''+urlsitedomain+'/comment.aspx?no-cache='+Math.random(),options);
}


function CommandSubmitContent(obj,url,newsid)
{
    if(obj.UserNum.value=="")
    {
        alert('帐号不能为空');
        return false;
    }
    if(obj.Content.value=="")
    {
        alert('评论内容不能为空');
        return false;
    }
    var r = obj.commtype; 
    var commtypevalue = '2'; 
    for(var i=0;i<r.length;i++) 
    {
        if(r[i].checked)
           commtypevalue=r[i].value;
    }
    var Action='CommentType=AddComment&showdiv=1&UserNum='+escape(obj.UserNum.value)+'&UserPwd='+escape(obj.UserPwd.value)+'&commtype='+escape(commtypevalue)+'&Content='+escape(obj.Content.value)+'&IsQID='+escape(obj.IsQID.value)+'&NewsID=437727796213';
    var options={ 
                    method:'get', 
                    parameters:Action, 
                    onComplete:function(transport) 
                    { 
                        var returnvalue=transport.responseText; 
                        var arrreturnvalue=returnvalue.split('$$$'); 
                        if (arrreturnvalue[0]=="ERR") 
                        { 
                           alert(arrreturnvalue[1]); 
                           obj.Content.value='';
                        } 
                        else 
                        { 
                           alert('发表评论成功!'); 
                           GetCommentListContent(''+url+'',''+newsid+'','1');
                           //document.getElementById("CommentlistPage").innerHTML=arrreturnvalue[1]; 
                           obj.Content.value='';
                        } 
                    } 
                 }; 
     new  Ajax.Request(''+url+'/comment.aspx?no-cache='+Math.random(),options); 
     
} 
function CommentLoginOut(obj,url)
{
    var Action='CommentType=LoginOut';
    var options={ 
                  method:'get', 
                  parameters:Action, 
                  onComplete:function(transport) 
                  { 
                      var returnvalue=transport.responseText; 
                      var arrreturnvalue=returnvalue.split('$$$'); 
                      if (arrreturnvalue[0]=="ERR") 
                          alert('未知错误!'); 
                      else 
                          document.getElementById('UserNum').value='Guest';
                          document.getElementById('UserPwd').value='';
                          document.getElementById('loginOutB').innerHTML='(匿名用户请直接使用Guest用户名) ';
                   } 
                 }; 
      new  Ajax.Request(''+url+'/comment.aspx?no-cache='+Math.random(),options);
}

//获得页面转向
function getPageInfoURLFileName(type)
{
	if(type=="0")
	{
	    var v1= document.location.pathname;
	    var temp_f = v1.lastIndexOf("/");
	    var fien = v1.substring(temp_f+1,v1.length)
	    if(fien.indexOf("_")>-1)
	    {
		    for (var i=0;i<document.getElementById('PageSelectOption').length;i++)
		    {
			    if(fien==document.getElementById('PageSelectOption').options[i].value)
			    {
				    document.getElementById('PageSelectOption').options[i].selected=true;
			    }
		    }
	    }
	}
	else
	{
	    var v1= document.location.href;
	    var temp_f = v1.lastIndexOf("=");
	    var fien = v1.substring(temp_f+1,v1.length)
	    if(v1.lastIndexOf("=")>-1)
	    {
		    for (var i=0;i<document.getElementById('PageSelectOption').length;i++)
		    {
			    document.getElementById('PageSelectOption').options[fien-1].selected=true;
		    }
	    }
	    else
	    {
	        document.getElementById('PageSelectOption').options[0].selected=true;
	    }
	}
	
}
