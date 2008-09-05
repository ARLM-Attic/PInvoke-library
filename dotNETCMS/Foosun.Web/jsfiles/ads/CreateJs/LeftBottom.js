if (navigator.appName == "Netscape")
document.ns = navigator.appName == "Netscape"
window.screen.width>800 ? imgheight=190:imgheight=180
window.screen.width>800 ? imgleft=20:imgleft=30

function LeftBottomload()
{
   if (navigator.appName == "Netscape")
     {
	 document.LeftBottom.pageY=pageYOffset+window.innerHeight-imgheight;
     document.LeftBottom.pageX=imgleft;
     LeftBottomMove();
     }
   else
    {
	LeftBottom.style.posTop=parseInt(document.body.scrollTop)+parseInt(window.screen.height)-parseInt(LeftBottom.style.height);
	//alert(parseInt(document.body.scrollTop));
	//alert(parseInt(window.screen.height));
	//alert(parseInt(LeftBottom.style.height));
	//alert(parseInt(document.body.scrollTop)+parseInt(document.body.clientHeight)-parseInt(LeftBottom.style.height));
	LeftBottom.style.left=imgleft;
	LeftBottomMove();
    }
}

function LeftBottomMove()
{
   if(document.ns)
    {
	document.LeftBottom.top=pageYOffset+window.innerHeight-imgheight;
	document.LeftBottom.left=imgleft;
	setTimeout("LeftBottomMove();",80)
    }
  else
   {
	LeftBottom.style.top=document.body.scrollTop+document.body.offsetHeight-imgheight;
	LeftBottom.style.left=imgleft;
	setTimeout("LeftBottomMove();",80)
   }
}

function LeftBottomReload(LBRTF) 
{
  if (LBRTF==true) with (navigator) 
   {
    if ((appName=="Netscape")&&(parseInt(appVersion)==4)) 
      {
       document.LeftBottomWidth=innerWidth; 
	   document.LeftBottomHeight=innerHeight; 
	   onresize=MM_reloadPage; 
	   }
	}
  else 
   if (innerWidth!=document.LeftBottomWidth || innerHeight!=document.LeftBottomHeight) location.reload();
}
LeftBottomReload(true);
LeftBottomload();