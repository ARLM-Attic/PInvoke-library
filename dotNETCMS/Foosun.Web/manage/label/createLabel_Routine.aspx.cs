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

public partial class manage_label_createLabel_Routine : Foosun.Web.UI.ManagePage
{
    public string APIID = "0";
    Foosun.CMS.Label rd = new Foosun.CMS.Label();
    protected void Page_Load(object sender, EventArgs e)
    {
        APIID = SiteID;
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            
            string _dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
            style_base.InnerHtml = Foosun.Common.Public.getxmlstylelist("styleContent", _dirdumm + "/xml/cuslabeStyle/cstylebase.xml");
            style_class.InnerHtml = Foosun.Common.Public.getxmlstylelist("styleContent1", _dirdumm + "/xml/cuslabeStyle/cstyleclass.xml");
            style_special.InnerHtml = Foosun.Common.Public.getxmlstylelist("DropDownList2", _dirdumm + "/xml/cuslabeStyle/cstylespecial.xml");
            getRuleID();
            getTodayPicID();
            getDefine();
            GetStyleList(this.StyleClassID);
        } 
    }

    protected void GetStyleList(DropDownList lst)
    {
        IDataReader dr = rd.GetStyleList(SiteID);
        while (dr.Read())
        {
            ListItem it = new ListItem();
            it.Value = dr["ClassID"].ToString();
            it.Text = dr["Sname"].ToString();
            lst.Items.Add(it);
        }
        dr.Close();
    }

    /// <summary>
    ///  获取不规则新闻编号列表
    /// </summary>

    protected void getRuleID()
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getRuleID();
        if (dt != null)
        {
            RuleID.DataTextField = "UnName";
            RuleID.DataValueField = "UnID";
            RuleID.DataSource = dt;
            RuleID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Value = "";
        itm.Text = "请选择不规则新闻";
        RuleID.Items.Insert(0, itm);
        itm=null;
    }


    /// <summary>
    /// 获取今日图片头条编号列表
    /// </summary>
    
    protected void getTodayPicID()
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getTodayPicID();
        if (dt != null)
        {
            TodayPicID.DataTextField = "NewsTitle";
            TodayPicID.DataValueField = "NewsID";
            TodayPicID.DataSource = dt;
            TodayPicID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Value = "";
        itm.Text = "请选择图片头条新闻";
        TodayPicID.Items.Insert(0, itm);
        itm = null;
    }

    /// <summary>
    /// 获得自定义字段列表
    /// </summary>

    protected void getDefine()
    {
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        DataTable dt = stClass.styledefine();

        if (dt != null)
        {
            define.DataTextField = "defineCname";
            define.DataValueField = "defineColumns";
            define.DataSource = dt;
            define.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Value = "";
        itm.Text = "自定义字段";
        define.Items.Insert(0, itm);
        itm = null;
    }
}