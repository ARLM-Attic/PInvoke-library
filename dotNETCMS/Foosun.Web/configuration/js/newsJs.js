// JScript 文件
function ChangeDiv(ID)
{
	document.getElementById('td_baseinfo').className='m_up_bg';
	document.getElementById('td_subnews').className='m_up_bg';
	document.getElementById('td_placeHolder').className='m_up_bg';
	document.getElementById('td_pram').className='m_up_bg'
    document.getElementById("div_baseinfo").style.display="none";
    document.getElementById("div_subnews").style.display="none";
    document.getElementById("div_placeHolder").style.display='none';
    document.getElementById("div_pram").style.display='none';
    document.getElementById('td_'+ ID).className='m_down_bg';
    
    if(ID=="subnews")
    {
	    document.Form1.subnew.value= document.Form1.NewsTitleTextBox.value;
	}
	document.getElementById("div_"+ ID).style.display="";
}

function DivShow(flag)
{
    if(flag)
    {
        /*模板Text与Control TextBox控件*/
        temp1.style.display="none";
        temp2.style.display="none";
        temp0.colSpan=3;
        /*控件Tags.作者.来源*/
        ta0.style.display="none";
        ta1.style.display="none";
        /*填写新闻内容*/
        C0.style.display="none";
        /*画中画*/
        t0.style.display="none";
    }
    else
    {
        /*模板Text与Control TextBox控件*/
        temp1.style.display="";
        temp2.style.display="";
        temp0.colSpan=1;
        /*控件Tags.作者.来源*/
        ta0.style.display="";
        ta1.style.display="";
        /*填写新闻内容*/
        C0.style.display="";
        /*画中画*/
        t0.style.display=""; 
    }
}

function ShowLink(flag)
{
    switch(flag)
    {
        case 0: //普通
        {
            td_linkurlcap.style.display = "none";
            td_linkurl.style.display = "none";
            at0.style.display="none";
            td_newstype.colSpan = 3;
            DivShow(false);
            break;
        }
        case 1: //图片
        {
            at0.style.display="";
            td_linkurlcap.style.display = "none";
            td_linkurl.style.display = "none";
            td_newstype.colSpan = 3;
            DivShow(false);
            break; 
        }
        case 2: //标题
        {
            td_newstype.colSpan = 1;
            td_linkurlcap.style.display = "";
            td_linkurl.style.display = "";
            at0.style.display="";
            DivShow(true);
            break;
        }
    }
}

/*是否允许画中画*/
function IsDispLayDiv()
{
    var obj = document.getElementById("IsDisplay");
    if(obj.checked)
    {
        document.getElementById("t1").style.display="";
        document.getElementById("t2").style.display="";
    }
    else
    {
        document.getElementById("t1").style.display="none";
        document.getElementById("t2").style.display="none";
    }
 }
 
function Dokesite(obj)
{
	if (obj!='')
	{
		if (document.Form1.TagsTextBox.value.search(obj)==-1)
		{
			if (document.Form1.TagsTextBox.value=='')
			    document.Form1.TagsTextBox.value=obj;
			else 
			    document.Form1.TagsTextBox.value=document.Form1.TagsTextBox.value+','+obj;
		}
	}    
}

function tbladdrow(id,type) 
{ 
    var otbd = document.getElementById(""+type+"");
    var otr = document.createElement('tr');
    otr.className = 'TR_BG_list';
    otr.align="right";
    otbd.appendChild(otr);
    var colSpan = 2;
    //判断多少列
    if(type=="optionsItems")
        colSpan=4;
        
    var StringChar=null;
    var StringColSpan=null;
   if(type=="subnewsTable")   
   {
        StringChar='<img src="../../sysImages/folder/del.gif" border="0" onclick="DelRow(this)" alt="删除本行" style="cursor:hand"/>文本'+id+'：';
     StringColSpan="<div align=\"left\"><input ID=\"textpi_"+id+""\" Width=\"15%\" name=\"textpi_"+id+"\" value=\"\" class=\"form\"></div>";
   }else{
    StringChar='<img src="../../sysImages/folder/del.gif" border="0" onclick="DelRow(this)" alt="删除本行" style="cursor:hand"/>文本'+id+'：';
     StringColSpan="<div align=\"left\"><input ID=\"strpi\" name=\"strpi\" Width=\"15%\" value=\"\" class=\"form\"></div>";
   } 
     
    for(var i=0;i<colSpan;i++)
    {
        var otd = document.createElement('td');
        otd.className = 'list_link';
        switch(i)
        {
            case 0:
                otd.innerHTML = StringChar;
                break;
            case 1:
                otd.innerHTML = StringColSpan;
                break;
            case 2:
                otd.innerHTML = '选项值'+id+'：';
                break;
            case 3: 
                otd.innerHTML = '<div align="left"><input ID="pic" name="pic" runat="server" Width="15%" value="" class="form"></div>';
                break;
        }
        otr.appendChild(otd);
    }    
} 

function TableSetting(items,type)
{
    for( i = 1 ; i <= items ; i++) 
    { 
        tbladdrow(i,type); 
    }
} 

//删除数据列
function DelRow(obj) 
{ 
    var otbd = document.getElementById('subnewsTable');
    otbd.removeChild(obj.parentNode.parentNode);
}

function checkboxTF()
{
    var cbTF = document.getElementById('CheckBox4');
    if(cbTF.checked)
    {
        document.getElementById("options").style.display="";
        document.getElementById("Button3").style.display="";
    }
    else
    {
        document.getElementById("options").style.display="none";
        document.getElementById("Button3").style.display="none"; 
    }
    
}

//头条信息
function touInfo(obj)
{
    if(obj.checked)
    {
        document.getElementById("toutiaoType").style.display="";
    }
    else
    {
        document.getElementById("toutiaoType").style.display="none";
        document.getElementById("toutiaotrc").style.display="none";
    }
}

function toutiaotr(obj)
{
   if(obj=="0")
    document.getElementById("toutiaotrc").style.display="none";
   else
   {
    document.getElementById("toutiaotrc").style.display="";
   }
}