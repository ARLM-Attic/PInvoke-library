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
using Foosun.CMS;
using Foosun.Web.UI;

public partial class configuration_system_getClassCname : Foosun.Web.UI.DialogPage
{
    public configuration_system_getClassCname()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin;
    }
    ContentManage rd = new ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string Type = Request.QueryString["Type"];
        string ClassID = Request.QueryString["ClassID"];
        string TCID = "";
        if (Type == "Class")
        {
            //add
            TCID = rd.getClassCName(ClassID.ToString());
            if (TCID == "没选择栏目")
            {
                if (Request.QueryString["add"] != null && Request.QueryString["add"] != string.Empty)
                {
                    TCID = "根目录";
                }
            }
        }
        else if (Type == "special")
        {
            TCID = rd.getspecialCName(ClassID.ToString());
        }
        Response.Write(TCID);
    }
}
