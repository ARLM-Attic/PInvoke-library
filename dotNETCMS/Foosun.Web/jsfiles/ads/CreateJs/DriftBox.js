DriftBoxTF=navigator.javaEnabled()?true:false;
var DriftBoxMin=2; var DriftBoxMax=5; var DriftBoxRe=2; var DriftBoxTime;

function Chip(chipname,width,height)
{
	this.named=chipname;
	this.vx=DriftBoxMin+DriftBoxMax*Math.random();
	this.vy=DriftBoxMin+DriftBoxMax*Math.random();
	this.w=width; this.h=height;
	this.xx=0;
	this.yy=0;
	this.DriftBoxTime=null;
}

function movechip(chipname) 
{
	if(DriftBoxTF)
	{
		eval("chip="+chipname);
		if(DriftBoxStr)
		{
			pageX=window.pageXOffset;
			pageW=window.innerWidth;
			pageY=window.pageYOffset;
			pageH=window.innerHeight;
		}
		else
		{
			pageX=window.document.body.scrollLeft;
			pageW=window.document.body.offsetWidth-8;
			pageY=window.document.body.scrollTop;
			pageH=window.document.body.offsetHeight;
		}
		chip.xx=chip.xx+chip.vx;chip.yy=chip.yy+chip.vy;
		chip.vx+=DriftBoxRe*(Math.random()-0.5);
		chip.vy+=DriftBoxRe*(Math.random()-0.5);
		if(chip.vx>(DriftBoxMax+DriftBoxMin)) chip.vx=(DriftBoxMax+DriftBoxMin)*2-chip.vx;
		if(chip.vx<(-DriftBoxMax-DriftBoxMin)) chip.vx=(-DriftBoxMax-DriftBoxMin)*2-chip.vx;
		if(chip.vy>(DriftBoxMax+DriftBoxMin)) chip.vy=(DriftBoxMax+DriftBoxMin)*2-chip.vy;
		if(chip.vy<(-DriftBoxMax-DriftBoxMin)) chip.vy=(-DriftBoxMax-DriftBoxMin)*2-chip.vy;
		if(chip.xx<=pageX)
		{
			chip.xx=pageX;chip.vx=DriftBoxMin+DriftBoxMax*Math.random();
		 }
		if(chip.xx>=pageX+pageW-chip.w)
		{
			chip.xx=pageX+pageW-chip.w;
			chip.vx=-DriftBoxMin-DriftBoxMax*Math.random();
		 }
		if(chip.yy<=pageY){
			chip.yy=pageY;
			chip.vy=DriftBoxMin+DriftBoxMax*Math.random();
			}
		if(chip.yy>=pageY+pageH-chip.h){
			chip.yy=pageY+pageH-chip.h;
			chip.vy=-DriftBoxMin-DriftBoxMax*Math.random();
			}
		if(DriftBoxStr)
		{
			eval('document.'+chip.named+'.top ='+chip.yy);
			eval('document.'+chip.named+'.left='+chip.xx);
		 }
		else
		{
			eval('document.all.'+chip.named+'.style.pixelLeft='+chip.xx);
			eval('document.all.'+chip.named+'.style.pixelTop ='+chip.yy);
		 }
		chip.DriftBoxTime=setTimeout("movechip('"+chip.named+"')",100);
	}
}

function DriftBoxSM(chipname)
{ 
	if(DriftBoxTF)
	{
		eval("chip="+chipname);
		if(chip.DriftBoxTime!=null)
		{
			clearTimeout(chip.DriftBoxTime)
		}
	}
}

var DriftBox;

function DriftBox() 
{
	DriftBox=new Chip("DriftBox",60,80);
	if(DriftBoxTF)
	{
		movechip("DriftBox");
	 }
}

window.onload=DriftBox;