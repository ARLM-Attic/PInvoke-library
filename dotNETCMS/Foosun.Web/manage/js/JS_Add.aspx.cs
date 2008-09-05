using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.Model;

public partial class manage_js_JS_Add : Foosun.Web.UI.ManagePage
{
    public manage_js_JS_Add()
    {
        Authority_Code = "C052";
    }
    public string jspath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";//设置页面无缓存
        if (!Page.IsPostBack)
        {
            
            if (SiteID != "0")
            {
                jspath = "jsfiles/js/" + SiteID;
            }
            else
            {
                jspath = "jsfiles/js";
            }
            this.TxtSavePath.Text = "/" + jspath;
            this.TxtSavePath.Attributes.Add("readonly", "true");
            this.LblCatpion.Text = this.LblTitle.Text = "新增JS";
            Foosun.CMS.JSTemplet jt = new Foosun.CMS.JSTemplet();
            DataTable tb = jt.List();
            if (tb == null || tb.Rows.Count < 1)
            {
                PageError("没有JS模板,请先新增JS模板!", "JSTemp_Add.aspx");
            }
            int fsys = 0, ffree = 0;
            foreach (DataRow r in tb.Rows)
            {
                ListItem it = new ListItem();
                it.Value = r["TempletID"].ToString();
                it.Text = r["CName"].ToString();
                if (r["jsTType"].ToString().Equals("0"))
                {
                    fsys++;
                    this.DdlTempSys.Items.Add(it);
                }
                else
                {
                    ffree++;
                    this.DdlTempFree.Items.Add(it);
                }
            }
            ListItem itm = new ListItem();
            itm.Text = "<没有可用模板>";
            if (fsys.Equals(0))
            {
                this.DdlTempSys.Items.Add(itm);
                this.RadTypeSys.Enabled = false;
                this.RadTypeSys.Text += "[无可用模板]";
                this.RadTypeFree.Checked = true;
            }
            if (ffree.Equals(0))
            {
                this.DdlTempFree.Items.Add(itm);
                this.RadTypeFree.Enabled = false;
                this.RadTypeFree.Text += "[无可用模板]";
                this.RadTypeSys.Checked = true;
            }
            this.HidID.Value = "-1";
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                this.Authority_Code = "C053";
                this.CheckAdminAuthority();
                int id = int.Parse(Request.QueryString["ID"]);
                this.LblCatpion.Text = this.LblTitle.Text = "修改JS";
                this.HidID.Value = id.ToString();
                Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
                NewsJSInfo jf = nj.GetSingle(id);
                this.HidJsID.Value = jf.JsID;
                this.TxtName.Text = jf.JSName;
                this.TxtNum.Text = jf.jsNum.ToString();
                this.TxtSavePath.Text = jf.jssavepath;
                this.TxtFileName.Text = jf.jsfilename;
                this.TxtContent.Text = jf.jsContent;
                if (jf.jsType.Equals(0))
                {
                    this.RadTypeSys.Checked = true;
                    this.DdlTempSys.SelectedValue = jf.JsTempletID;
                }
                else if (jf.jsType.Equals(1))
                {
                    this.RadTypeFree.Checked = true;
                    this.TxtLenContent.Text = jf.jsLenContent.ToString();
                    this.TxtLenTitle.Text = jf.jsLenTitle.ToString();
                    this.TxtColsNum.Text = jf.jsColsNum.ToString();
                    this.TxtLenNavi.Text = jf.jsLenNavi.ToString();
                    this.DdlTempFree.SelectedValue = jf.JsTempletID;
                }
                else
                {
                    PageError("未知的JS类型!", "JSTemp_Add.aspx");
                }
                this.RadTypeSys.Enabled = false;
                this.RadTypeFree.Enabled = false;
            }
        }
    }

    protected void BtnOK_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            NewsJSInfo info = new NewsJSInfo();
            info.Id = int.Parse(this.HidID.Value);
            info.JSName = this.TxtName.Text.Trim();
            info.jsNum = int.Parse(this.TxtNum.Text);
            info.jsContent = this.TxtContent.Text.Trim();
            info.jsType = 0;
            info.JsID = this.HidJsID.Value;
            info.JsTempletID = this.DdlTempSys.SelectedValue;
            info.jsLenContent = -1;
            info.jsLenNavi = -1;
            info.jsLenTitle = -1;
            info.jsColsNum = -1;
            if (this.RadTypeFree.Checked)
            {
                info.jsType = 1;
                info.JsTempletID = this.DdlTempFree.SelectedValue;
                info.jsLenContent = int.Parse(this.TxtLenContent.Text);
                info.jsLenNavi = int.Parse(this.TxtLenNavi.Text);
                info.jsLenTitle = int.Parse(this.TxtLenTitle.Text);
                info.jsColsNum = int.Parse(this.TxtColsNum.Text);
            }
            info.jssavepath = this.TxtSavePath.Text.Trim();
            info.jsfilename = this.TxtFileName.Text.Trim();
            Foosun.CMS.NewsJS nj = new Foosun.CMS.NewsJS();
            if (info.Id > 0)
            {
                nj.Update(info);
                PageRight("修改JS信息成功!", "JS_List.aspx");
            }
            else
            {
                nj.Add(info);
                PageRight("新增JS成功!", "JS_List.aspx");
            }
        }
    }
}
