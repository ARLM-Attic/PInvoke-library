using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.CMS.Common;

public partial class survey_Vote_Show : Foosun.Web.UI.BasePage
{
    public string dc = null;
    public string strtheurl = null;
    Survey sur = new Survey();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache"; //清除缓存
        RequestQueyStrig();//获取前台调用参数
    }
    protected void RequestQueyStrig()
    {
        int tid = int.Parse(Request["TID"]);//主题ID
        int PicW = int.Parse(Request["PicW"]);//图片宽度
        int Steps = 0;
        if (Request["Steps"] != null)
        {
            Steps = int.Parse(Request["Steps"]);
        }
        string OutHtmlID = "Vote_HTML_ID";
        if (Request["OutHtmlID"] != null)
        {
            OutHtmlID = Request["OutHtmlID"];
        }
        if (tid <= 0)
        {
            PageError("参数传递错误", "");
        }
        if (PicW <= 0)
        {
            PicW = 60;
        }
        ShowItem(tid, PicW, Steps, OutHtmlID);
    }
    protected void ShowItem(int Tid, int PicW, int Steps, string OutHtmlID)
    {
        rootPublic rd = new rootPublic();
        string SiteID = "0";
        if (Validate_Session())
        {
            SiteID = Foosun.Global.Current.SiteID;
        }
        string Str_dirMana = Foosun.Config.UIConfig.dirDumm;
        if (Str_dirMana != "" && Str_dirMana != null && Str_dirMana != string.Empty)//判断虚拟路径是否为空,如果不是则加上//
        {
            Str_dirMana = "/" + Str_dirMana;
        }
        else
        {
            Str_dirMana = "";
        }
        string Cookie_Domain = "";
        string strlist = "";
        string ItemValueDisP = "";//1-9/a-z/A-Z/.
        string type = "";//单选？多选？
        string checkboxOrrediobox = "";//复选框还是单选框?
        string issteps = "";//是否是多步投票？
        #region 根据传递的主题ID查询相应的主题
        DataTable dtt = sur.sel_11(Tid);
        if (dtt.Rows.Count <= 0)
        {
            Response.Write("<font color=\"red\">后台已经删除此投票</font>");
            Response.End();
        }
        //判断时间
        string _isTimeOut = string.Empty;
        if (dtt.Rows[0]["StartDate"] != null && dtt.Rows[0]["EndDate"] != null)
        {
            if (!string.IsNullOrEmpty(dtt.Rows[0]["StartDate"].ToString()) && !string.IsNullOrEmpty(dtt.Rows[0]["EndDate"].ToString()))
            {
                if (DateTime.Parse(dtt.Rows[0]["StartDate"].ToString()) > DateTime.Now)
                {
                    _isTimeOut = "alert('投票时间还未到');return false;";
                }
                if (DateTime.Parse(dtt.Rows[0]["EndDate"].ToString()) < DateTime.Now)
                {
                    _isTimeOut = "alert('投票时间已过');return false;";
                }
            }
        }
        #endregion
        string TitleName = dtt.Rows[0]["Title"].ToString();//相应ID的主题名
        string ItemModeTitle = dtt.Rows[0]["ItemMode"].ToString();//该主题下选项的排列方式/5项/行...
        type = dtt.Rows[0]["Type"].ToString();//取得是单选还是复选的值
        issteps = dtt.Rows[0]["isSteps"].ToString();//是否是多步投票？

        if (issteps != "1")
        {
            string validateItem = string.Empty;
            #region 单选还是复选?
            if (type == "1")
            {
                string maxNum = dtt.Rows[0]["MaxNum"].ToString();
                checkboxOrrediobox = "checkbox";//复选
                validateItem = _isTimeOut + "var i = 0;for(var j = 0;j<document.voteForm.Items.length;j++){if(document.voteForm.Items[j].checked)i++;}if(i > " + maxNum + "){alert('只能同时选择" + maxNum + "项');return false;	}	";
            }
            else
            {
                checkboxOrrediobox = "radio";//单选
            }
            #endregion

            strlist = "<table width=98%% border=0 align=center cellpadding=5 cellspacing=1 class=table>";
            strlist += "<form name=voteForm id=voteForm method=\"post\" onSubmit=\"" + validateItem + "if(document.getElementById('ItemsInput')!=null){new Ajax.Request('/survey/Vote_Ajax.aspx?TID=" + Tid + "&ItemsInput='+$('ItemsInput').value , {method: 'post', parameters: Form.serialize(this),onComplete:function(transport){document.getElementById('Ajax_TPInfo').innerHTML = transport.responseText;if(transport.responseText == '感谢您的投票'){if(confirm('是否查看投票结果') == true){window.open('/survey/view.aspx?TID=" + Tid + "');}}    } });} else {new Ajax.Request('/survey/Vote_Ajax.aspx?TID=" + Tid + "', {method: 'post', parameters: Form.serialize(this) ,onComplete:function(transport){document.getElementById('Ajax_TPInfo').innerHTML = transport.responseText;if(transport.responseText == '感谢您的投票'){if(confirm('是否查看投票结果') == true){window.open('/survey/view.aspx?TID=" + Tid + "');}}}});} return false;\">";
            strlist += "<tr class=TR_BG_list><td width=100% class=list_link colspan=3>所属主题：<strong>" + TitleName + "</strong></td></tr>";
            strlist += "</table>";
            strlist += "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">";
            strlist += "<tr class=\"TR_BG_list\"><td width=\"100%\" class=\"list_link\" colspan=\"3\">";
            strlist += Options(Tid, checkboxOrrediobox, ItemModeTitle);
            Cookie_Domain = rd.sitedomain();
            if (Cookie_Domain == "")
            {
                Cookie_Domain = "localhost";
            }
            strlist += "</td></tr><tr class=\"TR_BG_list\"><td width=\"100%\" class=\"list_link\">";
            strlist += "<input type=\"submit\" value=\"提 交\"></input>&nbsp;&nbsp;<input type=\"button\" id=\"but_ViewVote\" value=\"查看结果\"  style=\"width:80px;height:23px;\" onClick=\"window.open('/survey/view.aspx?TID=" + Tid + "')\">";
            strlist += "</td></tr><tr><td id=\"Ajax_TPInfo\"></td></tr></table>";
            Response.Write(strlist);
            Response.End();
        }
        else
        {
            #region 第一次
            int TIDU = 0;
            string Stepss = "";
            DataTable dts = null;
            if (Steps == 0)
            {
                dts = sur.sel_12(Tid, SiteID);
                if (dts.Rows.Count <= 0)
                {
                    Response.Write("此投票不存在");
                    Response.End();
                }
                TIDU = int.Parse(dts.Rows[0]["TIDU"].ToString());
                Stepss = dts.Rows[0]["Steps"].ToString();
                #region 根据传递的主题ID查询相应的主题
                DataTable dtts = sur.sel_11(TIDU);
                //Response.Write(TIDU);
                #endregion
                if (dtts.Rows.Count >= 0)
                {
                    TitleName = dtts.Rows[0]["Title"].ToString();//相应ID的主题名
                    ItemModeTitle = dtts.Rows[0]["ItemMode"].ToString();//该主题下选项的排列方式/5项/行...
                    type = dtts.Rows[0]["Type"].ToString();//取得是单选还是复选的值
                    issteps = dtts.Rows[0]["isSteps"].ToString();//是否是多步投票？

                    #region 单选还是复选?
                    if (type == "1")
                    {
                        checkboxOrrediobox = "checkbox";//复选
                    }
                    else
                    {
                        checkboxOrrediobox = "radio";//单选
                    }
                    #endregion
                    strlist = "<table width=98% border=0 align=center cellpadding=5 cellspacing=1 class=table>";
                    strlist += "<form name=voteForm id=voteForm method=\"post\" onSubmit=\"if(document.getElementById('ItemsInput')!=null){new Ajax.Request('/survey/Vote_Ajax.aspx?TID=" + Tid + "&ItemsInput='+$('ItemsInput').value , {method: 'post', parameters: Form.serialize(this),onComplete:function(transport){document.getElementById('Ajax_TPInfo').innerHTML = transport.responseText;} });} else {new Ajax.Request('/survey/Vote_Ajax.aspx?TID=" + Tid + "', {method: 'post', parameters: Form.serialize(this) ,onComplete:function(transport){document.getElementById('Ajax_TPInfo').innerHTML = transport.responseText;}});} return false;\">";
                    strlist += "<tr class=TR_BG_list><td width=100% class=list_link colspan=3>所属主题：<strong>" + TitleName + "</strong></td></tr>";
                    strlist += "</table>";
                    strlist += "<table width=98% border=0 align=center cellpadding=5 cellspacing=1 class=table>";
                    strlist += "<tr class=TR_BG_list><td width=100% class=list_link colspan=3>";
            #endregion
                    strlist += Options(Tid, checkboxOrrediobox, ItemModeTitle);
                    Cookie_Domain = rd.sitedomain();
                    if (Cookie_Domain == "")
                    {
                        Cookie_Domain = "localhost";
                    }
                    strlist += "</td></tr><tr class=TR_BG_list><td width=100% class=list_link>";
                    strlist += "<input type=\"button\"  value=\"下一步\" style=\"width:80px;height:23px; \" onclick=\"new Ajax.Request('/survey/Vote_Show.aspx',{method:'post',parameters:'TID=" + Tid + "&OutHtmlID=" + OutHtmlID + "&PicW=" + PicW + "&Steps=" + Stepss + "',onComplete:function(transport){document.getElementById('" + OutHtmlID + "').innerHTML = transport.responseText;}})\"/>&nbsp&nbsp&nbsp";
                    strlist += "<input type=\"submit\" id=\"but_VoteSubmit\" value=\" 提 交 \" style=\"width:80px;height:23px;\">&nbsp&nbsp&nbsp";
                    strlist += "<input type=\"button\" id=\"but_ViewVote\" value=\"查看结果\"  style=\"width:80px;height:23px;\" onClick=\"window.open('/survey/view.aspx?TID=" + Tid + "')\">";
                    strlist += "</td></tr><tr><td id=\"Ajax_TPInfo\"></td></tr></table>";
                    Response.Write(strlist);
                    Response.End();
                }
            }
            else
            {
                int st = Steps;
                if (Request["top"] == "top")
                {
                    dts = sur.sel_15(Tid, SiteID, st);
                    int cut = dts.Rows.Count;
                    if (cut >= 0)
                    {
                        if (cut == 1)
                        {
                            int Stepds = 0;
                            ShowItem(Tid, PicW, Stepds, OutHtmlID);
                            //break;
                        }
                        else
                        {
                            TIDU = int.Parse(dts.Rows[0]["TIDU"].ToString());
                            Stepss = dts.Rows[0]["Steps"].ToString();
                        }
                    }
                }
                else
                {
                    dts = sur.sel_13(Tid, SiteID, st);
                    if (dts.Rows.Count > 0)
                    {
                        TIDU = int.Parse(dts.Rows[0]["TIDU"].ToString());
                        Stepss = dts.Rows[0]["Steps"].ToString();
                    }
                    else //提示多步调查只设置了第一步没有设置多步，需重新设置
                    {
                        Response.Write("请将该多步投票主题下投票步骤设置完整，至少两步！");
                        return;
                    }
                }

                int step = int.Parse(Stepss);

                DataTable dtss = sur.sel_13(Tid, SiteID, step);
                int p = dtss.Rows.Count;
                if (p >= 0)
                {
                    if (p > 0)
                    {
                        #region 根据传递的主题ID查询相应的主题
                        DataTable dtts = sur.sel_11(TIDU);
                        #endregion
                        TitleName = dtts.Rows[0]["Title"].ToString();//相应ID的主题名
                        ItemModeTitle = dtts.Rows[0]["ItemMode"].ToString();//该主题下选项的排列方式/5项/行...
                        type = dtts.Rows[0]["Type"].ToString();//取得是单选还是复选的值
                        issteps = dtts.Rows[0]["isSteps"].ToString();//是否是多步投票？
                        #region 单选还是复选?
                        if (type == "1")
                        {
                            checkboxOrrediobox = "checkbox";//复选
                        }
                        else
                        {
                            checkboxOrrediobox = "radio";//单选
                        }
                        #endregion
                        #region 查出该TID下的所有选项
                        strlist = "<table width=98% border=0 align=center cellpadding=5 cellspacing=1 class=table>";
                        strlist += "<form name=voteForm id=voteForm method=\"post\" onSubmit=\"if(document.getElementById('ItemsInput')!=null){new Ajax.Request('/survey/Vote_Ajax.aspx?TID=" + Tid + "&ItemsInput='+$('ItemsInput').value , {method: 'post', parameters: Form.serialize(this),onComplete:function(transport){document.getElementById('Ajax_TPInfo').innerHTML = transport.responseText;} });} else {new Ajax.Request('/survey/Vote_Ajax.aspx?TID=" + Tid + "', {method: 'post', parameters: Form.serialize(this) ,onComplete:function(transport){document.getElementById('Ajax_TPInfo').innerHTML = transport.responseText;}});} return false;\">";
                        strlist += "<tr class=TR_BG_list><td width=100% class=list_link colspan=3>所属主题：<strong>" + TitleName + "</strong></td></tr>";
                        strlist += "</table>";
                        strlist += "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">";
                        strlist += "<tr class=\"TR_BG_list\"><td width=\"100%\" class=\"list_link\" colspan=\"3\">";

                        strlist += Options(TIDU, checkboxOrrediobox, ItemModeTitle);


                        Cookie_Domain = rd.sitedomain();
                        if (Cookie_Domain == "")
                        {
                            Cookie_Domain = "localhost";
                        }
                        strlist += "</td></tr><tr class=\"TR_BG_list\"><td width=\"100%\" class=\"list_link\">";
                        strlist += "<input type=\"button\"  value=\"上一步\" style=\"width:80px;height:23px; \" onclick=\"new Ajax.Request('/survey/Vote_Show.aspx',{method:'post',parameters:'TID=" + Tid + "&OutHtmlID=" + OutHtmlID + "&PicW=" + PicW + "&Steps=" + Stepss + "&top=top',onComplete:function(transport){document.getElementById('" + OutHtmlID + "').innerHTML = transport.responseText;}})\"/>&nbsp&nbsp&nbsp";

                        strlist += "<input type=\"button\"  value=\"下一步\" style=\"width:80px;height:23px; \" onclick=\"new Ajax.Request('/survey/Vote_Show.aspx',{method:'post',parameters:'TID=" + Tid + "&OutHtmlID=" + OutHtmlID + "&PicW=" + PicW + "&Steps=" + Stepss + "',onComplete:function(transport){document.getElementById('" + OutHtmlID + "').innerHTML = transport.responseText;}})\"/>&nbsp&nbsp&nbsp";
                        strlist += "<input type=\"submit\" id=\"but_VoteSubmit\" value=\" 提 交 \" style=\"width:80px;height:23px;\">&nbsp&nbsp&nbsp";
                        strlist += "<input type=\"button\" id=\"but_ViewVote\" value=\"查看结果\"  style=\"width:80px;height:23px;\" onClick=\"window.open('/survey/view.aspx?TID=" + Tid + "')\">";
                        strlist += "</td></tr><tr><td id=\"Ajax_TPInfo\"></td></tr></table>";
                        #endregion
                        Response.Write(strlist);
                        //Response.Write(TIDU);
                        Response.End();
                    }
                    else
                    {
                        #region 根据传递的主题ID查询相应的主题
                        DataTable dtts = sur.sel_11(TIDU);
                        #endregion
                        TitleName = dtts.Rows[0]["Title"].ToString();//相应ID的主题名
                        ItemModeTitle = dtts.Rows[0]["ItemMode"].ToString();//该主题下选项的排列方式/5项/行...
                        type = dtts.Rows[0]["Type"].ToString();//取得是单选还是复选的值
                        issteps = dtts.Rows[0]["isSteps"].ToString();//是否是多步投票？

                        #region 单选还是复选?
                        if (type == "1")
                        {
                            checkboxOrrediobox = "checkbox";//复选
                        }
                        else
                        {
                            checkboxOrrediobox = "radio";//单选
                        }
                        #endregion

                        #region 查出该TID下的所有选项
                        #region
                        strlist = "<table width=98% border=0 align=center cellpadding=5 cellspacing=1 class=table>";
                        strlist += "<form name=voteForm id=voteForm method=\"post\" onSubmit=\"if(document.getElementById('ItemsInput')!=null){new Ajax.Request('/survey/Vote_Ajax.aspx?TID=" + Tid + "&ItemsInput='+$('ItemsInput').value , {method: 'post', parameters: Form.serialize(this),onComplete:function(transport){document.getElementById('Ajax_TPInfo').innerHTML = transport.responseText;} });} else {new Ajax.Request('/survey/Vote_Ajax.aspx?TID=" + Tid + "', {method: 'post', parameters: Form.serialize(this) ,onComplete:function(transport){document.getElementById('Ajax_TPInfo').innerHTML = transport.responseText;}});} return false;\">";
                        strlist += "<tr class=TR_BG_list><td width=100% class=list_link colspan=3>所属主题：<strong>" + TitleName + "</strong></td></tr>";
                        strlist += "</table>";
                        strlist += "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\">";
                        strlist += "<tr class=\"TR_BG_list\"><td width=\"100%\" class=\"list_link\" colspan=\"3\">";
                        #endregion
                        strlist += Options(TIDU, checkboxOrrediobox, ItemModeTitle);
                        Cookie_Domain = rd.sitedomain();
                        if (Cookie_Domain == "")
                        {
                            Cookie_Domain = "localhost";
                        }
                        strlist += "</td></tr><tr class=\"TR_BG_list\"><td width=\"100%\" class=\"list_link\">";

                        strlist += "<input type=\"button\"  value=\"上一步\" style=\"width:80px;height:23px; \" onclick=\"new Ajax.Request('/survey/Vote_Show.aspx',{method:'post',parameters:'TID=" + Tid + "&OutHtmlID=" + OutHtmlID + "&PicW=" + PicW + "&Steps=" + Stepss + "&top=top',onComplete:function(transport){document.getElementById('" + OutHtmlID + "').innerHTML = transport.responseText;}})\"/>&nbsp&nbsp&nbsp";

                        strlist += "<input type=\"submit\" id=\"but_VoteSubmit\" value=\" 提 交 \" style=\"width:80px;height:23px;\">&nbsp&nbsp&nbsp";
                        strlist += "<input type=\"button\" id=\"but_ViewVote\" value=\"查看结果\"  style=\"width:80px;height:23px;\" onClick=\"window.open('/survey/view.aspx?TID=" + Tid + "')\">";
                        strlist += "</td></tr><tr><td id=\"Ajax_TPInfo\"></td></tr></table>";
                        #endregion
                        Response.Write(strlist);
                        // Response.Write(TIDU);
                        Response.End();
                    }
                }
            }
        }
    }
    #region 选项
    protected string Options(int Tid, string checkboxOrrediobox, string ItemModeTitle)
    {
        int iid = 0;//相应选项的iid
        string ItemName = "";//选项名
        string ItemValue = "";////1-9/a-z/A-Z/.(0,1,2,3)
        string ItemModel = "";//文字，图片，自主(选项)
        string ItemModell = "";//控制文字，图片，自主(选项)时后跟的内容
        string PicSrcc = "";//图片地址
        string ItemColor = "";//选项颜色
        string ItemValueDisP = "";//1-9/a-z/A-Z/.
        int y = 0;//控制前台显示1-9样式排序
        string strlist = null;
        DataTable dti = sur.sel_14(Tid);//找出tid的所有记录(选项)
        for (int i = 0; i < dti.Rows.Count; i++)
        {
            y++;//前台数字1-9显示自增
            iid = int.Parse(dti.Rows[i][0].ToString());//相应选项的iid
            ItemName = dti.Rows[i]["ItemName"].ToString();//选项名
            ItemValue = dti.Rows[i]["ItemValue"].ToString();////1-9/a-z/A-Z/.(0,1,2,3)
            ItemModel = dti.Rows[i]["ItemMode"].ToString();//文字，图片，自主(选项)
            ItemModell = "";//控制文字，图片，自主(选项)时后跟的内容
            PicSrcc = dti.Rows[i]["PicSrc"].ToString();//图片地址
            ItemColor = dti.Rows[i]["DisColor"].ToString();//选项颜色
            #region 序号
            switch (ItemValue)
            {
                case "0"://1-9样式
                    ItemValueDisP = y.ToString() + "<strong>.</strong>";
                    break;
                case "1"://a-z
                    char b = (char)(96 + y);
                    ItemValueDisP = b.ToString() + "<strong>.</strong>";
                    break;
                case "2"://A-Z
                    char a = (char)(64 + y);
                    ItemValueDisP = a.ToString() + "<strong>.</strong>";
                    break;
                case "3"://.
                    ItemValueDisP = "<strong>.</strong>";
                    break;
                case "4"://★
                    ItemValueDisP = "<strong>★</strong>";
                    break;

            }
            #endregion

            #region 显示方式
            switch (ItemModel)
            {
                case "1"://文字
                    ItemModell = "";
                    break;
                case "2"://自主(后有文本框，可自行输入额外的投票内容)
                    ItemModell = "<input type=\"text\" name=\"ItemsInput\" id=\"ItemsInput\" value=\"\" size=\"15\" maxlength=\"25\">";
                    break;
                case "3"://图片
                    if (PicSrcc != null)
                    {
                        ItemModell = "<img src=\"" + PicSrcc + "\" title=\"点击查看图片\" style=\"cursor:hand\" width=\"10%\" onclick=\"window.open(\"" + PicSrcc + "\");\" alt=\"暂无图片\">";
                    }
                    else
                    {
                        ItemModell = "";
                    }
                    break;
                default://默认文字
                    ItemModell = "";
                    break;

            }
            #endregion

            strlist += "<input type=\"" + checkboxOrrediobox + "\" name=\"Items\" id=\"Items\" value=\"" + iid + "\"><font color=#" + ItemColor + ">" + ItemValueDisP + " " + ItemName + "</font>  " + ItemModell + "";

            #region 排列方式
            if (ItemModeTitle == "0")
            {
                //横向排列(什么也不做。照原样排列成一行)
            }
            else
            {
                //纵向排列
                int nowNumber = int.Parse(ItemModeTitle);
                if (y % nowNumber == 0)
                {
                    strlist += "<br />";//换x行排列
                }
            }
            #endregion
        }
        return strlist;
    }
    #endregion
}