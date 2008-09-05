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

public partial class Click : Foosun.Web.UI.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string NewsID = Request.QueryString["id"];
        string ChID = Request.QueryString["ChID"];
        if (ChID != null && ChID != string.Empty)
        {
            if (Foosun.Common.Input.IsInteger(ChID))
            {
                Foosun.CMS.Channel rd = new Foosun.CMS.Channel();
                Response.Write(rd.AddinfoClick(int.Parse(NewsID.ToString()), int.Parse(ChID.ToString())));
            }
            else
            {
                Response.Write("0");
            }
        }
        else
        {
            Foosun.CMS.News news = new Foosun.CMS.News();
            Response.Write(news.AddNewsClick(NewsID));
        }
        
    }
}
