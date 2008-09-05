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

public partial class manage_js_JS_Templet_Class : Foosun.Web.UI.ManagePage
{
    public manage_js_JS_Templet_Class()
    {
        Authority_Code = "C056";
    }
    private Foosun.CMS.JSTemplet jt;
    private int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache"; //清除缓存
        jt = new Foosun.CMS.JSTemplet();
        if (!IsPostBack)
        {
            id = 0;
            if (Request.QueryString["ID"] != null)
            {
                id = int.Parse(Request.QueryString["ID"]);
            }
            DataTable tb = jt.ClassList();
            ClassRender(tb, "0", 0);
            this.LblCaption.Text = this.LblTitle.Text = "新增JS模型分类";
            if (id>0)
            {
                this.LblCaption.Text = this.LblTitle.Text = "修改JS模型分类";
                DataTable dt = jt.GetClass(id);
                if (dt == null || dt.Rows.Count < 1)
                    PageError("没有找到相关记录", "JS_Templet.aspx");
                this.TxtName.Text = dt.Rows[0]["CName"].ToString();
                this.DdlUpperClass.SelectedValue = dt.Rows[0]["ParentID"].ToString();
                this.TxtDescription.Text = dt.Rows[0]["Description"].ToString();
            }
            this.HidID.Value = id.ToString();
            if (Request.QueryString["Upper"] != null && !Request.QueryString["Upper"].Trim().Equals(""))
                this.DdlUpperClass.SelectedValue = Request.QueryString["Upper"];
        }
    }

    /// <summary>
    /// 递归
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="PID"></param>
    /// <param name="Layer"></param>
    private void ClassRender(DataTable tb,string PID, int Layer)
    {
        string sFilter = "ParentID='" + PID + "'";
        if (id > 0)
            sFilter += " and id<>" + id;
        DataRow[] row = tb.Select(sFilter);
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
                this.DdlUpperClass.Items.Add(it);
                ClassRender(tb, r["ClassID"].ToString(), Layer + 1);
            }
        }
    }

    protected void BtnOK_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断页面是否通过验证
        {
            int cid = int.Parse(this.HidID.Value);
            string sName = this.TxtName.Text.Trim();
            if (sName.Equals(""))
            {
                PageError("分类名称请必须填写!", "");
            }
            string sParent = this.DdlUpperClass.SelectedValue;
            string sDescrpt = this.TxtDescription.Text.Trim();
            if (sDescrpt.Length > 500)
            {
                PageError("描述信息必须在500字以内!", "");
            }
            if (cid > 0)
            {
                jt.ClassUpdate(cid, sName, sParent, sDescrpt);
                PageRight("修改JS模型分类成功!", "JS_Templet.aspx");
            }
            else
            {
                jt.ClassAdd(sName, sParent, sDescrpt);
                PageRight("新增JS模型分类成功!", "JS_Templet.aspx");
            }
        }
    }
}
