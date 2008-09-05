var over=false,down=false,divleft,divtop,temp;

function ClarityBoxclose(Flag)
{
	document.all[Flag].style.visibility='hidden'
 }
 
function ClarityBox(obj)
{
	temp=obj;
	down=true;
	divleft=event.clientX-parseInt(temp.style.left);
	divtop=event.clientY-parseInt(temp.style.top)
 }
 
function ClarityBoxMove(obj)
{
	if(down)
	  {
		obj.style.left=event.clientX-divleft;obj.style.top=event.clientY-divtop;
	   }
 }