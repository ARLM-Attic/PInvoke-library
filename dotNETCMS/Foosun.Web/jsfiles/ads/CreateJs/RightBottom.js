if (navigator.appName == "Netscape")
	document.ns = navigator.appName == "Netscape"
	window.screen.width>800 ? imgheight=190:imgheight=180
	window.screen.width>800 ? imgright=20:imgright=30
	
function RightBottomLoad()
{
	if (navigator.appName == "Netscape")
	{
		document.RightBottom.pageY=pageYOffset+window.innerHeight-imgheight;
		document.RightBottom.pageX=imgright;
		RightBottomMove();
	}
	else
	{
		RightBottom.style.top=document.body.scrollTop+document.body.offsetHeight-imgheight;
		RightBottom.style.right=imgright;
		RightBottomMove();
	}
}
function RightBottomMove()
{
	if(document.ns)
	{
		document.RightBottom.top=pageYOffset+window.innerHeight-imgheight;
		document.RightBottom.right=imgright;
		setTimeout("RightBottomMove();",80)
	}
	else
	{
		RightBottom.style.top=document.body.scrollTop+document.body.offsetHeight-imgheight;
		RightBottom.style.right=imgright;
		setTimeout("RightBottomMove();",80)
	}
}

function RightBottomReload(RBRTF) 
{ 
	if (RBRTF==true) with (navigator) 
	{
		if ((appName=="Netscape")&&(parseInt(appVersion)==4)) 
		  {
			document.RightBottomWidth=innerWidth; document.RightBottomHeight=innerHeight; onresize=MM_reloadPage;
		   }
	 }
	else if (innerWidth!=document.RightBottomWidth || innerHeight!=document.RightBottomHeight) location.reload();
}

RightBottomReload(true);
RightBottomLoad();