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

public partial class manage_ver_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Foosun.Common.HtmlProgressBar.Start();
        for (int i = 1; i < 11; i++)
        {
            System.Threading.Thread.Sleep(600);
            Foosun.Common.HtmlProgressBar.Roll("已完成"+ i.ToString(),i*10);
        }
    }
}
