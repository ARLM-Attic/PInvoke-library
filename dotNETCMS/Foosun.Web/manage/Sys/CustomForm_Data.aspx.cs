using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Foosun.Web.manage.Sys
{
    public partial class CustomForm_Data : Foosun.Web.UI.ManagePage
    {
        Foosun.CMS.CustomForm cf = new Foosun.CMS.CustomForm();
        protected int formid = 0;
        protected string tablenm = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Form["Option"] != null && Request.Form["ID"] != null
                    && Request.Form["Option"] == "TruncateTb")
                {
                    try
                    {
                        int id = int.Parse(Request.Form["ID"]);
                        cf.TruncateTable(id);
                        Response.Write("1%操作成功!");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("0%" + ex.Message);
                    }
                    Response.End();
                }
                if (Request.QueryString["id"] != null)
                {
                    formid = int.Parse(Request.QueryString["id"]);
                    string fname;
                    DataTable dt = cf.GetSubmitData(formid, out fname, out tablenm);
                    this.LblName.Text = fname;
                    this.GrdData.DataSource = dt.DefaultView;
                    this.GrdData.DataBind();
                    DataBind();
                }
                else
                {
                    PageError("参数不完整!", "CustomForm.aspx");
                }
            }
        }
    }
}
