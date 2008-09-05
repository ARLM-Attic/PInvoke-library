if (navigator.appName == "Netscape")
	document.ns = navigator.appName == "Netscape"
	window.screen.width>800 ? imgheight=160:imgheight=150
	window.screen.width>800 ? imgleft=10:imgleft=30
	window.screen.width>800 ? imgRight=10:imgRight=30
	
function Adsload_Couplet()
{
  if (navigator.appName == "Netscape")
    {
	document.AdsLayerLeft.pageY=pageYOffset+10;
	document.AdsLayerLeft.pageX=imgleft;
	document.AdsLayerRight.pageY=pageYOffset+10;
	document.AdsLayerRight.pageX=imgRight;Adsmove_Couplet();
    }
  else
   {
	AdsLayerLeft.style.top=document.body.scrollTop+10;
	AdsLayerLeft.style.left=imgleft;
	AdsLayerRight.style.top=document.body.scrollTop+10;
	AdsLayerRight.style.right=imgRight;
	Adsmove_Couplet();
   }
}

function Adsmove_Couplet()
{
  if(document.ns)
   {
	document.AdsLayerLeft.top=pageYOffset+10
	document.AdsLayerLeft.left=imgleft;
	document.AdsLayerRight.top=pageYOffset+10
	document.AdsLayerRight.right=imgRight;
	setTimeout("Adsmove_Couplet();",80)
   }
  else
  {
	AdsLayerLeft.style.top=document.body.scrollTop+10;
	AdsLayerLeft.style.left=imgleft;
	AdsLayerRight.style.top=document.body.scrollTop+10;
	AdsLayerRight.style.right=imgRight;
	setTimeout("Adsmove_Couplet();",80)
  }
}

Adsload_Couplet();