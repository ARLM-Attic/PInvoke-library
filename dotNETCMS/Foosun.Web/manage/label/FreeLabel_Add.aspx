<%@ Page Language="C#" AutoEventWireup="true" Inherits="manage_label_FreeLabel_Add" Codebehind="FreeLabel_Add.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="../../sysImages/<%Response.Write(Foosun.Config.UIConfig.CssPath());%>/css/css.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
    p{padding-left:20px;padding-top:0px;padding-bottom:0px;margin-top:3px;margin-bottom:0px;}
    </style>
    <script language="javascript" type="text/javascript" src="../../configuration/js/Public.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../configuration/js/Prototype.js"></script>
<script language="javascript" type="text/javascript">
<!--
var CurrentTR = null;
var ConTxt = '';
function ChangeDbTable(obj,flag)
{
    var val = obj.options[obj.selectedIndex].value;
    
    var tbd = document.getElementById('TBD'+ flag);
    var tbl = document.getElementById('TBL'+ flag);
    var len = tbl.rows.length;
    for(var i=1;i<len;i++)
    {
        tbl.deleteRow(1);
    }
    var oth = 'SelPrin';
    if(flag == 0)
    {
        oth = 'SelSub';
        document.getElementById('SelJoinPrin').options.length = 1;
    }
    else
    {
        document.getElementById('SelJoinSub').options.length = 1;
    }
    
    if(val == '0')
        return;
    
    var objoth = document.getElementById(oth);
    if(objoth.options[objoth.selectedIndex].value == val)
    {
        alert('主从表不能选择同一个数据表!');
        obj.selectedIndex = 0;
        return;
    }
    var tr = document.createElement('tr');
    tbd.appendChild(tr);
    var td = document.createElement('td');
    td.align = 'center';
    td.innerHTML = '正在读取数据,请稍候...';
    tr.appendChild(td);
    var options={
        method:'post',
        parameters:'Option=GetFields&TableName='+ val,
        onComplete:
            function(transport)
	        {
	            var retv =transport.responseText;
	            OnGetFields(retv,flag);
	        }
	    }
    new  Ajax.Request('FreeLabel_Add.aspx',options);
}
function OnGetFields(retv,flag)
{
    var tbd = document.getElementById('TBD'+ flag);
    var tbl = document.getElementById('TBL'+ flag);
    var objjn;
    if(flag == 0)
        objjn = document.getElementById('SelJoinPrin');
    else
        objjn = document.getElementById('SelJoinSub');
    if(tbl.rows.length > 1)
        tbl.deleteRow(1);
    var ret = retv.split(';');
    for(var i = 0;i < ret.length; i++)
    {
        var fld = ret[i].split(',');
        var tr = document.createElement('tr');
        tr.style.cursor = 'hand';
        tr.onclick = function(){RowClick(this);};
        tr.setAttribute('height','22');
        tbd.appendChild(tr);
        var td1 = document.createElement('td');
        td1.innerHTML = fld[0];
        tr.appendChild(td1);
        var td2 = document.createElement('td');
        td2.innerHTML = fld[1];
        tr.appendChild(td2);
        var td3 = document.createElement('td');
        td3.align = 'center';
        tr.appendChild(td3);
        var td4 = document.createElement('td');
        td4.align = 'center';
        tr.appendChild(td4);
        var td5 = document.createElement('td');
        td5.align = 'center';
        tr.appendChild(td5);
        var op = document.createElement('option');
        op.value = fld[0];
        op.innerHTML = fld[0];
        objjn.appendChild(op);
    }
}
function TxtFocus(obj)
{
    ConTxt = obj.value;
}
function TxtBlur(obj)
{
    if(obj.value != ConTxt)
    {
        ChangeSql(obj);
    }
}
function RowClick(obj)
{
    if(obj != CurrentTR)
    {
        if(CurrentTR != null)
            HidOld(CurrentTR);
        //第三列
        if(obj.childNodes[2].firstChild != null && obj.childNodes[2].firstChild.type == 'checkbox')
            return;
        obj.className = 'TR_BG';
        var td3 = obj.childNodes[2];
        var dis = td3.innerHTML;
        td3.innerHTML = '';
        var ele_check_3 = document.createElement('input');
        ele_check_3.setAttribute('type','checkbox');
        ele_check_3.onclick = function(){ChangeSql(this);};
        td3.appendChild(ele_check_3);
        if(dis == '√')
        {
            ele_check_3.checked = true;
        }
        //第四列
        var td4 = obj.childNodes[3];
        var con = td4.innerHTML;
        td4.innerHTML = '';
        var con1 = '',con2 = '';
        if(con != '')
        {
            var pos = con.indexOf(' ');
            con1 = con.substr(0,pos);
            con2 = con.substr(pos+1);
        }
        var ele_sel_4 = document.createElement('select');
        ele_sel_4.onclick = function(){ChangeSql(this);};
        td4.appendChild(ele_sel_4);
        for(var i=0;i<9;i++)
        {
            var opt = document.createElement('option');
            switch(i)
            {
                case 0:
                    opt.value = '';
                    opt.innerHTML = '';
                    break;
                case 1:
                    opt.value = '<>';
                    opt.innerHTML = '&lt;&gt;';
                    break;
                case 2:
                    opt.value = '<=';
                    opt.innerHTML = '&lt;=';
                    break;
                case 3:
                    opt.value = '>=';
                    opt.innerHTML = '&gt;=';
                    break;
                case 4:
                    opt.value = '<';
                    opt.innerHTML = '&lt;';
                    break;
                case 5:
                    opt.value = '>';
                    opt.innerHTML = '&gt;';
                    break;
                case 6:
                    opt.value = '=';
                    opt.innerHTML = '=';
                    break;
                case 7:
                    opt.value = 'in';
                    opt.innerHTML = 'in';
                    break;
                case 8:
                    opt.value = 'like';
                    opt.innerHTML = 'like';
                    break;
            }
            if(con1 == opt.innerHTML)
                opt.setAttribute('selected','selected');
            ele_sel_4.appendChild(opt);
        }
        var ele_txt_4 = document.createElement('input');
        //onblur onfocus
        ele_txt_4.setAttribute('type','text');
        ele_txt_4.setAttribute('size','5');
        ele_txt_4.setAttribute('value',con2);
        ele_txt_4.onfocus = function(){TxtFocus(this);};
        ele_txt_4.onblur = function(){TxtBlur(this);};
        td4.appendChild(ele_txt_4);
        //第五列
        var td5 = obj.childNodes[4];
        var ord = td5.innerHTML;
        td5.innerHTML = '';
        var ele_sel_5 = document.createElement('select');
        ele_sel_5.onclick = function(){ChangeSql(this);};
        td5.appendChild(ele_sel_5);
        for(var i=0;i<3;i++)
        {
            var opt = document.createElement('option');
            switch(i)
            {
                case 0:
                    opt.value = '';
                    opt.innerHTML = '';
                    break;
                case 1:
                    opt.value = 'ASC';
                    opt.innerHTML = '升序';
                    break;
                case 2:
                    opt.value = 'DESC';
                    opt.innerHTML = '降序';
                    break;
            }
            if(ord == opt.innerHTML)
                opt.setAttribute('selected','selected');
            ele_sel_5.appendChild(opt);
        }
        CurrentTR = obj;
    }
}
function HidOld(obj)
{
    obj.className = null;
    //第三列
    var td3 = obj.childNodes[2];
    if(td3.firstChild != null && td3.firstChild.type == 'checkbox')
    {
        var ch = td3.firstChild;
        var str1 = '';
        if(ch.checked)
            str1 = '√';
        td3.removeChild(td3.firstChild);
        td3.innerHTML = str1;
    }
    //第四列
    var td4 = obj.childNodes[3];
    if(td4.firstChild != null && td4.firstChild.type == 'select-one' && td4.lastChild != null && td4.lastChild.type == 'text')
    {
        var selc = td4.firstChild;
        var txt = td4.lastChild;
        var s = selc.options[selc.selectedIndex].value;
        var t = txt.value;
        td4.removeChild(td4.firstChild);
        td4.removeChild(td4.lastChild);
        if(s != '' && t != '')
        {
            td4.innerHTML = s +' '+ t;
        }
    }
    //第五列
    var td5 = obj.childNodes[4];
    if(td5.firstChild != null && td5.firstChild.type == 'select-one')
    {
        var sel = td5.firstChild;
        var ord = sel.options[sel.selectedIndex].innerHTML;
        td5.removeChild(sel);
        td5.innerHTML = ord;
    }
}
function ChangeSql(obj)
{
    var otb0 = document.getElementById('SelPrin');
    var otb1 = document.getElementById('SelSub');
    var tb0 = otb0.options[otb0.selectedIndex].value;
    var tb1 = otb1.options[otb1.selectedIndex].value;
    var ojn0 = document.getElementById('SelJoinPrin');
    var ojn1 = document.getElementById('SelJoinSub');
    var jn0 = ojn0.options[ojn0.selectedIndex].value;
    var jn1 = ojn1.options[ojn1.selectedIndex].value;
    var num = document.getElementById('TxtNum').value;
    var flag = false;
    if(tb0 != '0' && tb1 != '0' && jn0 != '0' && jn1 != '0')
        flag = true;
    var sql1 = null;
    var sql2 = null;
    if(tb0 != '0')
    {
        var tbl0 = document.getElementById("TBL0");
        sql1 = GenerateSql(tbl0,tb0,flag);
    }
    if(tb1 != '0')
    {
        var tbl1 = document.getElementById("TBL1");
        sql2 = GenerateSql(tbl1,tb1,flag);
    }
    var sql = '';
    var sqlwh = '';
    var sqlrd = '';
    if(flag)
    {
        var s = '';
        if(sql1[0] != '')
        {
            s = sql1[0];        
        }
        if(sql2[0] != '')
        {
            if(s != '')
                s += ',';
            s += sql2[0];
            
        }
        if(s != '')
        {
            sql = 'select '
            if(num != '')
                sql += 'top '+ num +' ';
            sql +=  s +' from '+ tb0 +','+ tb1 +' where '+ tb0 +'.'+ jn0 +'='+ tb1 +'.'+ jn1;
            if(sql1[1] != '')
                sql += ' and '+ sql1[1];
            if(sql2[1] != '')
                sql += ' and '+ sql2[1];
            if(sql1[2] != '')
            {
                sql += ' order by '+ sql1[2];
            }
            if( sql2[2] != '')
            {
                if(sql1[2] != '')
                    sql += ','+ sql2[2];
                else
                    sql += ' order by '+ sql2[2];
            }
        }
    }
    else
    {
        var asq = null;
        var dtb;
        if(sql1 != null)
        {
            asq = sql1;
            dtb = tb0;
        }
        else if(sql2 != null)
        {
            asq = sql1;
            dtb = tb0;
        }
        if(asq != null)
        {
            if(asq[0] != '')
            {
                sql = 'select '
                if(num != '')
                    sql += 'top '+ num +' ';
                sql += asq[0] +' from '+ dtb;
                if(asq[1] != '')
                    sql += ' where '+ asq[1];
                if(asq[2] != '')
                    sql += ' order by '+ asq[2];
            }
        }
    }
    
    document.getElementById("TxtSql").value = sql;
    
}
function GenerateSql(tbl,tb,flag)
{
    var sqls = '';
    var sqlw = '';
    var sqld = '';
    var n = tbl.rows.length;
    for(var i=1;i<n;i++)
    {
        var tr = tbl.rows[i];
        if(tr == null)
            continue;
        var fld = tr.cells[0].innerHTML;
        
        var obj1 = tr.cells[2].firstChild;
        var bdis = false;
        if(obj1 != null && obj1.type == 'checkbox')
        {
            bdis = obj1.checked;
        }
        else
        {
            if(tr.cells[2].innerHTML == '√')
                bdis = true;
        }
        
        var obj2 = tr.cells[3].firstChild;
        var obj3 = tr.cells[3].lastChild;
        var con1 = '';
        var con2 = '';
        if(obj2 != null && obj3 != null && obj2.type == 'select-one' && obj3.type == 'text')
        {
            con1 = obj2.options[obj2.selectedIndex].value;
            con2 = obj3.value;
        }
        else
        {
            var con = tr.cells[3].innerHTML;
            if(con != '')
            {
                var pos = con.indexOf(' ');
                con1 = con.substr(0,pos);
                con2 = con.substr(pos+1);
            }
        }
        
        var obj4 = tr.cells[4].firstChild;
        var ordr = '';
        if(obj4 != null && obj4.type == 'select-one')
        {
            ordr = obj4.options[obj4.selectedIndex].value;
        }
        else
        {
            var ord = tr.cells[4].innerHTML;
            if(ord == '升序')
                ordr = 'ASC';
            else if(ord == '降序')
                ordr = 'DESC';
        }
        
        if(flag)
            fld = tb +'.'+ fld;
        //选中某字段
        if(bdis)
        {
            if(sqls != '')
                sqls += ',';
            sqls += fld;
        }
        if(con1 != '' && con2 != '')
        {
            if(sqlw != '')
                sqlw += ' and ';
            con1 = con1.replace('&lt;','<');
            con1 = con1.replace('&gt;','>');
            sqlw += fld +' '+ con1 +' '+ con2;
        }
        if(ordr != '')
        {
            if(sqld != '')
                sqld += ',';
            sqld += fld +' '+ ordr;
        }
    }
    var ret = new Array(3);
    ret[0] = sqls;
    ret[1] = sqlw;
    ret[2] = sqld;
    return ret;
}
function GoNext()
{
    var snm = document.getElementById('TxtName').value.trim();
    if(snm == '')
    {
        alert('标签名称不能为空!');
        document.getElementById('TxtName').focus();
        return;
    }
    var s = document.getElementById('TxtSql').value.trim();
    if(s == '')
    {
        alert('SQL语句不能为空!');
        document.getElementById('TxtSql').focus();
        return;
    }
    if(s.length > 4000)
    {
        alert('SQL语句长度不能超过4000,请适当减少查询的字段或条件!');
        document.getElementById('TxtSql').focus();
        return;
    }
    document.Form1.target = '_self';
    document.Form1.action = 'FreeLabel_AddEnd.aspx';
    document.Form1.submit();
}
function CheckNumber(obj)
{
    var sql = document.getElementById('TxtSql').value;
    var n = obj.value.trim();
    var reg = /select top \d+/i;
    if(n != '')
    {
        if(!n.IsNum())
        {
            alert('查询数量必须为正整数!');
            obj.value = '10';
            obj.focus();
        }
        document.getElementById('TxtSql').value = sql.replace(reg,'select top '+ n);
    }
    else
    {
        document.getElementById('TxtSql').value = sql.replace(reg,'select');
    }
}
function TestSQL()
{
    document.Form1.action = 'FreeLabel_Test.aspx';
    document.Form1.target = '_blank';
    document.Form1.submit();
}
//-->
</script>
</head>
<body>
    <form id="Form1" name="Form1" method="post" action="FreeLabel_AddEnd.aspx">
        <div>
            <table id="top1" width="100%" border="0" cellpadding="0" cellspacing="0" class="toptable">
                <tr>
                    <td height="1" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td width="57%" class="sysmain_navi" style="padding-left: 14px">
                        <%# Caption%></td>
                    <td width="43%" class="topnavichar" style="padding-left: 14px">
                        <div align="left">
                            位置导航：<a href="../main.aspx" target="sys_main" class="list_link">首页</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><a
                                href="FreeLabel_List.aspx" target="sys_main" class="list_link">自由标签管理</a><img alt="" src="../../sysImages/folder/navidot.gif" border="0" /><%# Caption%></div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" class="Navitable">
                <tr>
                    <td style="padding-left: 14px">
                        <a class="topnavichar" href="javascript:GoNext();">下一步</a> <a class="topnavichar" href="javascript:history.back();">
                            返回</a></td>
                </tr>
            </table>
            <table width="98%" cellpadding="5" cellspacing="1" align="center" class="table">
                <tr class="TR_BG_list">
                    <td width="50%">
                        标签名称：<span style="font-weight:bold;color:Red">{FS_FREE</span><input name="TxtName" id="TxtName" maxlength="30" type="text" class="form" value="<%# lblname%>"/><span style="font-weight:bold;color:Red">}</span>
                    </td>
                    <td width="50%">
                        查询数量：<input name="TxtNum" onchange="CheckNumber(this)" id="TxtNum" style="width:80%" type="text" maxlength="9" value="<%# TopNum%>" class="form" />
                    </td>
                </tr>
                <tr class="TR_BG_list">
                    <td>主 &nbsp;&nbsp; 表： 
                        <select id="SelPrin" name="SelPrin" onchange="ChangeDbTable(this,0)">
                            <option value="0">请选择</option>
                            <%# TabList1%>
                         </select>
                    </td>
                    <td>从 &nbsp;&nbsp; 表： 
                        <select id="SelSub" name="SelSub" onchange="ChangeDbTable(this,1)">
                            <option value="0">请选择</option>
                            <%# TabList2%>
                        </select>
                    </td>
               </tr>
               <tr class="TR_BG_list">
                    <td>
                        <div style="background-color:White;width:100%;height:200px;overflow:auto;border-color:#cccccc;border-width:1px;border-style:groove;padding-left:3px;padding-top:5px;">
                            <table id="TBL0" width="96%" cellpadding="0" cellspacing="0">
                                <tbody id="TBD0">
                                    <tr>
                                        <td align="center" width="40%">字段名</td><td align="center" width="15%">类型</td><td align="center" width="10%">显示</td><td align="center" width="25%">条件</td><td align="center" width="10%">排序</td>
                                    </tr>
                                    <%# List1%>
                                </tbody>
                            </table>
                        </div>
                    </td>
                    <td>
                        <div style="background-color:White;width:100%;height:200px;overflow:auto;border-color:#cccccc;border-width:1px;border-style:groove;padding-left:3px;padding-top:5px;">
                            <table id="TBL1" width="96%" cellpadding="0" cellspacing="0">
                                <tbody id="TBD1">
                                    <tr>
                                        <td align="center" width="40%">字段名</td><td align="center" width="15%">类型</td><td align="center" width="10%">显示</td><td align="center" width="25%">条件</td><td align="center" width="10%">排序</td>
                                    </tr>
                                    <%# List2%>
                                </tbody>
                            </table>
                        </div>
                   </td>
               </tr>
                <tr class="TR_BG_list">
                    <td colspan="2">
                        联接字段：主表字段 <select id="SelJoinPrin" onchange="ChangeSql(this)"><option value="0">请选择</option><%# JoinFlds1%></select> 从表字段 <select id="SelJoinSub" onchange="ChangeSql(this)"><option value="0">请选择</option><%# JoinFlds2%></select>
                    </td>
                </tr>
                <tr class="TR_BG_list">
                    <td colspan="2">SQL语句  <input type="button" onclick="TestSQL()" value="执行SQL语句" /></td>
                </tr>
                <tr class="TR_BG_list">
                    <td colspan="2" style="WORD-BREAK: break-all;">
                        <textarea cols="7" rows="7" id="TxtSql" name="TxtSql" readonly="readonly" class="form" style="width:99%;height:70px; overflow:auto;"><%# lblsql%></textarea>
                    </td>
                </tr>
            </table>
            <div style="color:Red">
<p>说明：</p>
<p>1.此功能建议具有一定sql基础的人使用。如果不熟悉sql，请谨慎使用，以防误操作引起的数据损坏。</p>
<p>2.判断型数字赋值：0为否，1为是。</p>
<p>3.如果设置条件的字段为文本、文本型ID、时间日期等类型，赋值时请前后加 ' ，如：= '内容' 、In('内容1'，'内容2'),否则会出错；Like关系除外。</p>
<p>4.在下一步设置之前，建议先点击测试按钮确认sql语句没有问题再继续。</p>
            </div>
            <br />
            <table width="100%" border="0" cellpadding="8" cellspacing="0" class="copyright_bg"
                style="height: 76px">
                <tr>
                    <td align="center">
                        <%Response.Write(CopyRight);%>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" name="LID" value="<%# id%>" />
        <textarea style="display:none;" name="StyleCon" rows="1" cols="1"><%# stylecon%></textarea>
        <input type="hidden" name="Descrpt" value="<%# descrpt%>" />
    </form>
</body>
</html>

