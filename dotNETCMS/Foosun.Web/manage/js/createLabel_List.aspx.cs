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

public partial class manage_js_createLabel_List : Foosun.Web.UI.ManagePage
{
    public string APIID = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        APIID = SiteID;
        if (!IsPostBack)
        {

            string _dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
            style_base.InnerHtml = Foosun.Common.Public.getxmlstylelist("styleContent", _dirdumm + "/xml/cuslabeStyle/cstylebase.xml");
            style_class.InnerHtml = Foosun.Common.Public.getxmlstylelist("styleContent1", _dirdumm + "/xml/cuslabeStyle/cstyleclass.xml");
            style_special.InnerHtml = Foosun.Common.Public.getxmlstylelist("DropDownList2", _dirdumm + "/xml/cuslabeStyle/cstylespecial.xml");
            getDefine();
        }
    }

    /// <summary>
    /// 读取自定义字段数据
    /// </summary>
    private void getDefine()
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