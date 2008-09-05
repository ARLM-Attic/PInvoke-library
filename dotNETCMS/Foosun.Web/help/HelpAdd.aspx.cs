///************************************************************************************************************
///**********添加帮助信息,Code By DengXi***********************************************************************
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
using System.Drawing;
using Foosun.CMS;
using Foosun.CMS.Common;

public partial class Help_HelpAdd : Foosun.Web.UI.BasePage
{
    Help help = new Help();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Response.CacheControl = "no-cache";
            if (Request.QueryString["Action"] != "" && Request.QueryString["Action"] != null)
            {
                this.id.Value = Request.QueryString["ID"].ToString();
                DataTable dt = help.getHelpID(int.Parse(Request.QueryString["ID"].ToString()));
                if (dt != null)
                {
                    this.CnHelpTitle.Text = dt.Rows[0]["TitleCN"].ToString();
                    this.CnHelpContent.Value = dt.Rows[0]["ContentCN"].ToString();
                    this.HelpID.Text = dt.Rows[0]["HelpID"].ToString();
                    this.HelpID.Enabled = false;
                }
            }
        }
    }
    protected void Submit1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断是否通过验证
        {
            string Str_CnHelpTitle = Request.Form["CnHelpTitle"];
            string Str_CnHelpContent = CnHelpContent.Value;
            if (this.id.Value != null && this.id.Value != "")
            {
                int Str_ID = int.Parse(this.id.Value);
                if (help.updatehelp(Str_ID, Str_CnHelpTitle, Str_CnHelpContent) == 0)
                {
                    PageError("修改失败", "");
                }
                else
                {
                    PageRight("修改成功", "HelpList.aspx");
                }
            }
            else
            {
                string Str_HelpID = Request.Form["HelpID"];
                if (help.Str_CheckSql(Str_HelpID) != 0)
                {
                    PageError("帮助ID已存在", "");
                }
                if (help.Str_InsSql(Str_HelpID, Str_CnHelpTitle, Str_CnHelpContent) == 0)
                {
                    PageError("添加失败", "");
                }
                else
                {
                    PageRight("添加成功", "HelpList.aspx");
                }
            }
        }
    }

}
