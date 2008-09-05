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

namespace Foosun.Web.configuration.system
{
    public partial class reviewUnNews : Foosun.Web.UI.DialogPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //getUnNewsReview
            if (!IsPostBack)
            {
                Response.CacheControl = "no-cache";
                Response.Expires = 0;
                Foosun.CMS.ContentManage rd = new Foosun.CMS.ContentManage();
                DataTable dt = rd.getUnNewsReview(Request.QueryString["UnID"].ToString());
                string str_unRule = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    int int_rows = 0;
                    int int_rows1 = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string str_SubClass = dt.Rows[i]["SubCSS"].ToString();
                        if (str_SubClass != null && str_SubClass != "")
                        {
                            str_SubClass = " class=\"" + str_SubClass + "\"";
                        }
                        int_rows = int.Parse(dt.Rows[i]["Rows"].ToString());
                        if (int_rows == int_rows1)
                        {
                            str_unRule += "<a href=\"javascript:void(0);\" " + str_SubClass + "  class=\"list_link\">" + dt.Rows[i]["unTitle"].ToString() + "</a>&nbsp;";
                        }
                        else
                        {
                            int_rows1 = int_rows1 + 1;
                            str_unRule += "<br /><a href=\"javascript:void(0);\" " + str_SubClass + "  class=\"list_link\">" + dt.Rows[i]["unTitle"].ToString() + "</a>&nbsp;";
                        }
                    }
                    dt.Clear(); dt.Dispose();
                }
                Response.Write(str_unRule);
            }
        }
    }
}
