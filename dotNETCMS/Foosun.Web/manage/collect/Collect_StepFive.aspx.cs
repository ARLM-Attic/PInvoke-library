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
using Foosun.CMS.Collect;

namespace Foosun.Web.manage.collect
{
    public partial class Collect_StepFive : Foosun.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //得到要查看的网址
            string showURLContent = Request.QueryString["showURLContent"];
            //目录ID
            string showForderID = Request.QueryString["showForderID"];
            //得到目录信息
            Collect cl = new Collect();
            DataTable dt = cl.GetSite(Convert.ToInt32(showForderID));
            DataRow r = dt.Rows[0];
            PageNews pn = new PageNews(showURLContent, r["Encode"].ToString());
            if (pn.Fetch())
            {
                pn.RuleOfTitle = r["PageTitleSetting"].ToString();
                pn.RuleOfContent = r["PagebodySetting"].ToString();
                pn.FigureTitle();
                pn.FigureContent();
                if (r.IsNull("HandSetAuthor"))
                {
                    pn.FigureAuthor(r["AuthorSetting"].ToString(), false);
                }
                else
                {
                    pn.FigureAuthor(r["HandSetAuthor"].ToString(), true);
                }
                if (r.IsNull("HandSetSource"))
                {
                    pn.FigureSource(r["SourceSetting"].ToString(), false);
                }
                else
                {
                    pn.FigureSource(r["HandSetSource"].ToString(), true);
                }
                if (r.IsNull("HandSetAddDate"))
                {
                    pn.FigureAddTime(r["AddDateSetting"].ToString(), false);
                }
                else
                {
                    pn.FigureAddTime(r["HandSetAddDate"].ToString(), true);
                }
            }

            this.Label1.Text = pn.Author;
            this.Label2.Text = pn.AddTime + "";
            this.Label3.Text = pn.Source;
            this.showContextDiv.InnerHtml = pn.Content;
        }

        protected void Button2_ServerClick(object sender, EventArgs e)
        {
            PageRight("采集站点设置成功！", "Collect_List.aspx");
        }
    }
}
