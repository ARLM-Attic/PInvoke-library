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

public partial class configuration_system_history_page : Foosun.Web.UI.DialogPage
{
    public configuration_system_history_page()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
}
