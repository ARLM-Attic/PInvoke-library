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
using Foosun.Model;

namespace Foosun.Web.manage.Sys
{
    public partial class Form_Add : Foosun.Web.UI.ManagePage
    {
        public Form_Add()
        {
            Authority_Code = "Q037";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LblTablePre.Text = Config.DBConfig.TableNamePrefix + "Form_";
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim() != string.Empty)
                {
                    this.HidID.Value = int.Parse(Request.QueryString["id"]).ToString();
                    this.LtrCaption.Text = "修改表单";
                }
                else
                {
                    this.HidID.Value = "0";
                    this.LtrCaption.Text = "新增表单";
                }
            }
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CustomFormInfo cf = new CustomFormInfo();

            }
        }
    }
}
