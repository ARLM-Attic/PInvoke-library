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

public partial class manage_label_createLabel_Other : Foosun.Web.UI.ManagePage
{
    public string APIID = "0";
    public DataTable dt_class = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        APIID = SiteID;
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
        }
        getFreeLabelInfo();
        getfreeJSInfo();
        getsysJSInfo();
        getadsJsInfo();
        getsurveyJSInfo();
        getstatJSInfo();
        getFreeLinks();
    }

    /// <summary>
    /// 显示自由JS
    /// </summary>
    /// <returns>显示自由JS</returns>
    /// 编写时间2007-04-28   Code By DengXi
    
    protected void getfreeJSInfo()
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getfreeJSInfo();

        if (dt != null)
        {
            freeJSID.DataTextField = "JSName";
            freeJSID.DataValueField = "JsID";
            freeJSID.DataSource = dt;
            freeJSID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Text = "请选择";
        itm.Value = "";
        freeJSID.Items.Insert(0, itm);
        itm = null;
    }

    /// <summary>
    /// 显示系统JS
    /// </summary>
    /// <returns>显示系统JS</returns>
    /// 编写时间2007-04-28   Code By DengXi

    protected void getsysJSInfo()
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getsysJSInfo();
        if (dt != null)
        {
            sysJSID.DataTextField = "JSName";
            sysJSID.DataValueField = "JsID";
            sysJSID.DataSource = dt;
            sysJSID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Text = "请选择";
        itm.Value = "";
        sysJSID.Items.Insert(0, itm);
        itm = null;              
    }

    /// <summary>
    /// 显示广告JS
    /// </summary>
    /// <returns>显示广告JS</returns>
    /// 编写时间2007-04-28   Code By DengXi

    protected void getadsJsInfo()
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getadsJsInfo();

        if (dt != null)
        {
            adJSID.DataTextField = "adName";
            adJSID.DataValueField = "AdID";
            adJSID.DataSource = dt;
            adJSID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Text = "请选择";
        itm.Value = "";
        adJSID.Items.Insert(0, itm);
        itm = null;
    }


    /// <summary>
    /// 调查JS
    /// </summary>
    /// <returns>调查JS</returns>
    /// 编写时间2007-04-28   Code By DengXi

    protected void getsurveyJSInfo()
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getsurveyJSInfo();
        if (dt != null)
        {
            surveyJSID.DataTextField = "Title";
            surveyJSID.DataValueField = "TID";
            surveyJSID.DataSource = dt;
            surveyJSID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Text = "请选择";
        itm.Value = "";
        surveyJSID.Items.Insert(0, itm);
        itm = null;
    }

    /// <summary>
    /// 统计JS
    /// </summary>
    /// <returns>统计JS</returns>
    /// 编写时间2007-04-28   Code By DengXi

    protected void getstatJSInfo()
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getstatJSInfo();

        if (dt != null)
        {
            statJSID.DataTextField = "classname";
            statJSID.DataValueField = "Statid";
            statJSID.DataSource = dt;
            statJSID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Text = "请选择";
        itm.Value = "";
        statJSID.Items.Insert(0, itm);
        itm = null;
    }

    protected void getFreeLabelInfo()
    { 
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getFreeLabelInfo();

        if (dt != null)
        {
            freeLabelID.DataTextField = "LabelName";
            freeLabelID.DataValueField = "LabelName";
            freeLabelID.DataSource = dt;
            freeLabelID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Text = "请选择自由标签";
        itm.Value = "";
        freeLabelID.Items.Insert(0, itm);
        itm = null;
    }


    #region 友情链接
    private void getFreeLinks()
    {
        //友情链接参数
        FrindLink fl = new FrindLink();
        dt_class = fl.GetClass();
        if (dt_class != null)
        {
            ClassRender("0", 0);
        }
        //设置参数
        dt_class.Clear();
        dt_class.Dispose();
    }

    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="Layer"></param>

    private void ClassRender(string PID, int Layer)
    {
        DataRow[] row = dt_class.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                ListItem it = new ListItem();
                it.Value = r["ClassID"].ToString();
                string stxt = "┝";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "┉";
                }
                it.Text = stxt + r["ClassCName"].ToString();
                this.SelectClass.Items.Add(it);
                ClassRender(r["ClassID"].ToString(), Layer + 1);
            }
        }
    }
    #endregion
}
