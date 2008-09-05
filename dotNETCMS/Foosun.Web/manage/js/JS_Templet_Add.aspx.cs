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

public partial class manage_js_JS_Templet_Add : Foosun.Web.UI.ManagePage
{
    public manage_js_JS_Templet_Add()
    {
        Authority_Code = "C056";
    }
    private Foosun.CMS.JSTemplet jt;
    public string APIID = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache"; //清除缓存
        APIID = SiteID;
        jt = new Foosun.CMS.JSTemplet();
        if (!IsPostBack)
        {
            
            string _dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (_dirdumm.Trim() != "") { _dirdumm = "/" + _dirdumm; }
            style_base.InnerHtml = Foosun.Common.Public.getxmlstylelist("DdlViewStyle", _dirdumm + "/xml/cuslabeStyle/cstylebase.xml");
            style_class.InnerHtml = Foosun.Common.Public.getxmlstylelist("DdlFixStyle", _dirdumm + "/xml/cuslabeStyle/cstyleclass.xml");
            style_special.InnerHtml = Foosun.Common.Public.getxmlstylelist("DdlSpcStyle", _dirdumm + "/xml/cuslabeStyle/cstylespecial.xml");
            this.HidID.Value = "-1";
            getDefine();
            DataTable tb = jt.ClassList();
            ClassRender(tb, "0", 0);

            this.LblCaption.Text = this.LblTitle.Text = "新增JS模型";
            if (Request.QueryString["ID"] != null)
            {
                int id = int.Parse(Request.QueryString["ID"]);
                this.LblCaption.Text = this.LblTitle.Text = "修改JS模型";
                this.HidID.Value = id.ToString();
                DataTable dt = jt.GetSingle(id);
                if (dt == null || dt.Rows.Count < 1)
                    PageError("没有找到相关记录", "JS_Templet.aspx");
                this.TxtName.Text = dt.Rows[0]["CName"].ToString();
                if (int.Parse(dt.Rows[0]["JSTType"].ToString()) == 1)
                    this.RadFree.Checked = true;
                this.DdlClass.SelectedValue = dt.Rows[0]["JSClassid"].ToString();
                this.ContentTextBox.Value = dt.Rows[0]["JSTContent"].ToString();
                this.RadFree.Enabled = false;
                this.RadSys.Enabled = false;
            }
            if (Request.QueryString["class"] != null && !Request.QueryString["class"].Trim().Equals(""))
                this.DdlClass.SelectedValue = Request.QueryString["class"];
        }
    }

    protected void getDefine()
    {
        Foosun.CMS.Style.Style stClass = new Foosun.CMS.Style.Style();
        DataTable dt = stClass.styledefine();

        if (dt != null)
        {
            DdlCustom.DataTextField = "defineCname";
            DdlCustom.DataValueField = "defineColumns";
            DdlCustom.DataSource = dt;
            DdlCustom.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Value = "";
        itm.Text = "自定义字段";
        DdlCustom.Items.Insert(0, itm);
        itm = null;

    }
    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="PID"></param>
    /// <param name="Layer"></param>
    private void ClassRender(DataTable tb,string PID, int Layer)
    {
        DataRow[] row = tb.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                ListItem it = new ListItem();
                it.Value = r["ClassID"].ToString();
                string stxt = "├";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "─";
                }
                it.Text = stxt + r["CName"].ToString();
                this.DdlClass.Items.Add(it);
                ClassRender(tb, r["ClassID"].ToString(), Layer + 1);
            }
        }
    }

    protected void BtnOK_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            int id = int.Parse(this.HidID.Value);
            string sName = this.TxtName.Text.Trim();
            if (sName.Equals(""))
            {
                PageError("名称请必须填写!", "");
            }
            string sContent = this.ContentTextBox.Value;
            if (sContent.Equals(""))
            {
                PageError("模型内容请必须填写!", "");
            }
            int JsTtype = 0;
            if (this.RadFree.Checked)
                JsTtype = 1;
            string sClass = this.DdlClass.SelectedValue;
            if (id > 0)
            {
                jt.Update(id, sName, sClass, sContent);
                PageRight("修改JS模型成功!", "JS_Templet.aspx");
            }
            else
            {
                jt.Add(sName, sClass, JsTtype, sContent);
                PageRight("新增JS模型成功!", "JS_Templet.aspx");
            }
        }
    }
}
