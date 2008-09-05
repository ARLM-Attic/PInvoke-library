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

namespace Foosun.Web.customform
{
    public partial class CustomFormJS : Foosun.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CustomFormID"] != null)
            {
                try
                {
                    int id = int.Parse(Request.QueryString["CustomFormID"]);
                    Foosun.CMS.CustomForm cf = new Foosun.CMS.CustomForm();
                    string s = cf.GetHtmlCode(id);
                    s = s.Replace(@"'", @"\'");
                    s = s.Replace("\r\n", " ");
                    string outstr = "document.write('" + s +"');";
                    Response.Write(outstr);
                }
                catch
                { }
            }
            Response.End();
        }
    }
}
