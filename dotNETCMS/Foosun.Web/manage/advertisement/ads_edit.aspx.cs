///************************************************************************************************************
///**********修改广告Code By DengXi****************************************************************************
///************************************************************************************************************
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


public partial class manage_advertisement_ads_edit : Foosun.Web.UI.ManagePage
{
    public manage_advertisement_ads_edit()
    {
        Authority_Code = "S007";
    }
    public string sadtype = "0";
    public string sconditf = "0";
    public int txtnum = 0;
    public DataTable TbClass = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;              //获取版权信息
            //------------------------获取广告信息-------------------------
            string str_ID = Request.QueryString["AdsID"];
            if (str_ID != null && str_ID != "" && str_ID != string.Empty)
            {
                str_ID = Foosun.Common.Input.checkID(str_ID);
                GetAdsInfo(str_ID);
            }
        }
        //------------------------修改广告-----------------------------
        string Type = Request.QueryString["Type"];
        if (Type == "Update")
        {
            UpdateAds();
        }
    }

    /// <summary>
    /// 取得广告信息
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <returns>取得广告信息</returns>
    /// Code By DengXi

    protected void GetAdsInfo(string adsID)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getAdsInfo(adsID);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                H_AdsID.Value = adsID;

                adName.Text = dt.Rows[0]["adName"].ToString();
                getClassInfo(dt.Rows[0]["ClassID"].ToString());
                OldClass.Value = dt.Rows[0]["ClassID"].ToString();
                sadtype = dt.Rows[0]["adType"].ToString();

                gettype(sadtype);

                leftPic.Text = dt.Rows[0]["leftPic"].ToString();
                leftSize.Text = dt.Rows[0]["leftSize"].ToString();
                rightPic.Text = dt.Rows[0]["rightPic"].ToString();
                rightSize.Text = dt.Rows[0]["rightSize"].ToString();
                LinkURL.Text = dt.Rows[0]["LinkURL"].ToString();

                sconditf = dt.Rows[0]["CondiTF"].ToString();
                CycTF.Value = dt.Rows[0]["CycTF"].ToString();

                getCycList(dt.Rows[0]["CycAdID"].ToString(),adsID);
                CycSpeed.Text = dt.Rows[0]["CycSpeed"].ToString();
                CycDic.SelectedValue = dt.Rows[0]["CycDic"].ToString();

                CondiTF.SelectedValue = dt.Rows[0]["CondiTF"].ToString();
                maxShowClick.Text = dt.Rows[0]["maxShowClick"].ToString();
                TimeOutDay.Text = dt.Rows[0]["TimeOutDay"].ToString();

                //----------取得文字广告内容---------
                getTxtList(adsID,dt.Rows[0]["AdTxtNum"].ToString());
                //-----------------------------------

                maxClick.Text = dt.Rows[0]["maxClick"].ToString();
                isLock.SelectedValue = dt.Rows[0]["isLock"].ToString();
                dt.Clear();
                dt.Dispose();
            }
            else
            {
                PageError("参数传递错误!", "");
            }
            dt.Clear();
            dt.Dispose();
        }
        else
            PageError("参数传递错误!", "");
    }

    /// <summary>
    /// 获得分类信息
    /// </summary>
    /// <param name="classid">类别编号</param>
    /// <returns>获得分类信息</returns>
    /// Code By DengXi

    protected void getClassInfo(string classid)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        TbClass = ac.getAdsClassList();

        if (TbClass != null)
            ClassRender("0", 0, classid);
        TbClass.Clear();TbClass.Dispose();
    }

    /// <summary>
    /// 获得分类信息(递归)
    /// </summary>
    /// <param name="PID">父类编号</param>
    /// <param name="Layer">层次</param>
    /// <param name="classid">类别编号</param>
    /// <returns>获得分类信息</returns>
    /// Code By DengXi

    private void ClassRender(string PID, int Layer, string classid)
    {
        DataRow[] row = TbClass.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                ListItem it = new ListItem();
                it.Value = r["AcID"].ToString();
                if (classid == it.Value)
                {
                    it.Selected = true;
                }
                string stxt = "┝";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "┉";
                }
                it.Text = stxt + r["Cname"].ToString();
                this.ClassID.Items.Add(it);
                ClassRender(r["AcID"].ToString(), Layer + 1, classid);
            }
        }
    }

    /// <summary>
    /// 获得广告类型,用于前台显示
    /// </summary>
    /// <param name="type">当前广告的类型</param>
    /// <returns>获得广告类型,用于前台显示</returns>
    /// Code By DengXi

    protected void gettype(string type)
    {
        for (int i = 0; i <= 12; i++)
        {
            ListItem itm = new ListItem();
            if (type == i.ToString())
                itm.Selected = true;
            itm.Value = i.ToString();
            itm.Text = Get_Type(i.ToString());
            adType.Items.Add(itm);
        }
    }

    /// <summary>
    /// 获得广告类型
    /// </summary>
    /// <param name="type">当前广告的类型</param>
    /// <returns>获得广告类型</returns>
    /// Code By DengXi

    protected string Get_Type(string type)
    {
        string str_tempstr ="";
        switch (type)
        {
            case "0":
                str_tempstr = "显示广告";
                break;
            case "1":
                str_tempstr = "弹出新窗口";
                break;
            case "2":
                str_tempstr = "打开新窗口";
                break;
            case "3":
                str_tempstr = "渐隐消失";
                break;
            case "4":
                str_tempstr = "网页对话框";
                break;
            case "5":
                str_tempstr = "透明对话框";
                break;
            case "6":
                str_tempstr = "满屏浮动";
                break;
            case "7":
                str_tempstr = "左下底端";
                break;
            case "8":
                str_tempstr = "右下底端";
                break;
            case "9":
                str_tempstr = "对联广告(顶部)";
                break;
            case "10":
                str_tempstr = "滚动广告";
                break;
            case "11":
                str_tempstr = "文字广告";
                break;
            case "12":
                str_tempstr = "对联广告(底部)";
                break;
        }
        return str_tempstr;
    }

    /// <summary>
    /// 获得循环广告列表
    /// </summary>
    /// <param name="CycID">循环广告编号</param>
    /// <param name="adsID">广告编号</param>
    /// <returns>在前台显示所有的广告列表,并且默认选中已被选中的广告</returns>
    /// Code By DengXi

    protected void getCycList(string Cyc_ID,string adsID)
    {
        CycID.Items.Clear();
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        TbClass = ac.getAdsList(adsID);
        if (TbClass != null)
        {
            for (int i = 0; i < TbClass.Rows.Count; i++)
            {
                ListItem it = new ListItem();
                if (it.Value == Cyc_ID)
                    it.Selected = true;
                it.Text = TbClass.Rows[i][1].ToString();
                it.Value = TbClass.Rows[i][0].ToString();
                CycID.Items.Add(it);
            }
        }
        TbClass.Clear();
        TbClass.Dispose();    
    }

    /// <summary>
    /// 取得文字广告列表
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="txtNum">文字广告显示的列数</param>
    /// <returns>取得文字广告列表</returns>
    /// Code By DengXi

    protected void getTxtList(string adsID,string txtNum)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getAdsPicInfo("AdTxt,AdCss,AdLink", "adstxt", adsID);
        string str_Temp = "";
        bool tf = false;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txtnum = dt.Rows.Count;
                    if (i == 0)
                    {
                        str_Temp += "<div id=\"default\" style=\"margin-bottom:1px;\"> 文本内容 <input name=\"AdTxtContent\" "+
                                    "type=\"text\" style=\"width:130px;\" maxlength=\"200\" value=\"" + dt.Rows[i]["AdTxt"].ToString() + "\" "+
                                    " class=\"form\" /> ";
                        str_Temp += "样式 <input name=\"AdTxtCss\" type=\"text\" style=\"width:30px;\" maxlength=\"20\" "+
                                    " value=\"" + dt.Rows[i]["AdCss"].ToString() + "\" class=\"form\" /> ";
                        str_Temp += "链接地址 <input name=\"AdTxtLink\" type=\"text\" id=\"AdTxtLink\" "+
                                    " value=\"" + dt.Rows[i]["AdLink"].ToString() + "\" style=\"width:130px;\" maxlength=\"100\" class=\"form\" /> ";
                        str_Temp += "列数 <input name=\"AdTxtColNum\" type=\"text\" id=\"AdTxtColNum\" style=\"width:20px;\" "+
                                    " maxlength=\"2\" onKeyUp=\"if(isNaN(value))execCommand('undo')\" "+
                                    " onafterpaste=\"if(isNaN(value))execCommand('undo')\" class=\"form\" value=\"" + txtNum + "\" /> ";
                        str_Temp += "<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击显示帮助\" "+
                                    " onclick=\"Help('H_AdsAdd_013',this)\">帮助</span><span id=\"spanAdTxtContent\"></span>"+
                                    "<span id=\"spanAdTxtNum\"></span></div><div id=\"temp\">";
                    }
                    else
                    {
                        str_Temp += "<div id=\"" + adsID + "\"> 文本内容 <input name=\"AdTxtContent\" type=\"text\" "+
                                    "style=\"width:130px;\" maxlength=\"200\" value=\"" + dt.Rows[i]["AdTxt"].ToString() + "\" class=\"form\" /> ";
                        str_Temp += "样式 <input name=\"AdTxtCss\" type=\"text\" style=\"width:30px;\" maxlength=\"20\" "+
                                    "value=\"" + dt.Rows[i]["AdCss"].ToString() + "\" class=\"form\" /> ";
                        str_Temp += "链接地址 <input name=\"AdTxtLink\" type=\"text\" id=\"AdTxtLink\" "+
                                    "value=\"" + dt.Rows[i]["AdLink"].ToString() + "\" style=\"width:130px;\" maxlength=\"100\" class=\"form\" /> ";
                        str_Temp += "<a href=\"#\" onclick='f_delete(this.parentNode)' class=\"list_link\">删除</a></div>";
                    }
                }
                str_Temp += "</div>";
                tf = false;
            }
            else
            {
                tf = true;
            }
            dt.Clear();
            dt.Dispose();
        }
        else
        {
            tf = true;
        }
        if (tf == true)
        {
            str_Temp += "<div id=\"default\" style=\"margin-bottom:1px;\"> 文本内容 <input name=\"AdTxtContent\"" +
                        " type=\"text\" style=\"width:130px;\" maxlength=\"200\" value=\"\" class=\"form\" /> ";
            str_Temp += "样式 <input name=\"AdTxtCss\" type=\"text\" style=\"width:30px;\" maxlength=\"20\" value=\"\" class=\"form\" /> ";
            str_Temp += "链接地址 <input name=\"AdTxtLink\" type=\"text\" id=\"AdTxtLink\" value=\"\" style=\"width:130px;\"" +
                        " maxlength=\"100\" class=\"form\" /> ";
            str_Temp += "列数 <input name=\"AdTxtColNum\" type=\"text\" id=\"AdTxtColNum\" style=\"width:20px;\" maxlength=\"2\"" +
                        " onKeyUp=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" " +
                        "class=\"form\" value=\"\" /> ";
            str_Temp += "<span class=\"helpstyle\" style=\"cursor:help;\" title=\"点击显示帮助\" " +
                        "onclick=\"Help('H_AdsAdd_013',this)\">帮助</span><span id=\"spanAdTxtContent\"></span>" +
                        "<span id=\"spanAdTxtNum\"></span></div><div id=\"temp\"></div>";
        }
        DivadTxt.InnerHtml = str_Temp;
    }

    /// <summary>
    /// 控制前台显示
    /// </summary>
    /// <returns>控制前台显示</returns>
    /// Code By DengXi

    protected void show()
    {
        Response.Write("<script language=\"javascript\">checkadType('" + sadtype + "');</script>\r");
        Response.Write("<script language=\"javascript\">checkCondiTF('" + sconditf + "');</script>\r");
    }

    /// <summary>
    /// 修改广告信息
    /// </summary>
    /// <returns>修改广告信息</returns>
    /// Code By DengXi

    protected void UpdateAds()
    {
        Foosun.Model.AdsInfo ai = new Foosun.Model.AdsInfo();
        ai.AdID = Foosun.Common.Input.checkID(Request.Form["H_AdsID"]);
        ai.adName = Request.Form["adName"];
        ai.ClassID = Request.Form["ClassID"];
        ai.OldClass = Request.Form["OldClass"];
        ai.adType = int.Parse(Request.Form["adType"]);
        ai.leftPic = Request.Form["leftPic"];
        ai.leftSize = Request.Form["leftSize"];
        ai.rightPic = Request.Form["rightPic"];
        ai.rightSize = Request.Form["rightSize"];
        ai.LinkURL = Request.Form["LinkURL"];
        if (Request.QueryString["CycTF"] == "1")
        {
            ai.CycTF = 1;
            ai.CycAdID = Request.Form["CycID"];
            ai.CycSpeed = int.Parse(Request.Form["CycSpeed"]);
            ai.CycDic = int.Parse(Request.Form["CycDic"]);
        }
        else
        {
            ai.CycTF = 0;
            ai.CycAdID = "-1";
            ai.CycSpeed = 0;
            ai.CycDic = 0;
        }

        ai.AdTxtContent = Request.Form["AdTxtContent"];
        ai.AdTxtCss = Request.Form["AdTxtCss"];
        ai.AdTxtLink = Request.Form["AdTxtLink"];

        if (Request.Form["AdTxtColNum"] != null && Request.Form["AdTxtColNum"] != "" && Request.Form["AdTxtColNum"] != string.Empty)
            ai.AdTxtNum = int.Parse(Request.Form["AdTxtColNum"]);
        else
            ai.AdTxtNum = 0;

        if (int.Parse(Request.Form["CondiTF"]) == 1)
        {
            ai.CondiTF = 1;
            if (Request.Form["maxShowClick"] != null && Request.Form["maxShowClick"] != "" && Request.Form["maxShowClick"] != string.Empty)
                ai.maxShowClick = int.Parse(Request.Form["maxShowClick"]);
            else
                ai.maxShowClick = 0;
            if (Request.Form["TimeOutDay"] != null && Request.Form["TimeOutDay"] != "" && Request.Form["TimeOutDay"] != string.Empty)
                ai.TimeOutDay = DateTime.Parse(Request.Form["TimeOutDay"]);
            else
                ai.TimeOutDay = DateTime.Parse("3000-1-1");
            if (Request.Form["maxClick"] != null && Request.Form["maxClick"] != "" && Request.Form["maxClick"] != string.Empty)
                ai.maxClick = int.Parse(Request.Form["maxClick"]);
            else
                ai.maxClick = 0;
        }
        else
        {
            ai.CondiTF = 0;
            ai.maxShowClick = 0;
            ai.TimeOutDay = DateTime.Parse("3000-1-1");
            ai.maxClick = 0;
        }
        ai.isLock = int.Parse(Request.Form["isLock"]);
        ai.SiteID = SiteID;
        ai.creatTime = DateTime.Now;
        ai.CusID = UserNum;


        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        int result = ac.adsEdit(ai);

        if (result == 1)
            PageRight("修改广告成功!", "list.aspx");
        else
            PageError("修改广告失败!", "");
    }
}
