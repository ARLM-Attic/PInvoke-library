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
using Foosun.CMS.Common;

public partial class configuration_system_selectChannel : Foosun.Web.UI.DialogPage
{
    public configuration_system_selectChannel()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin;
    }
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Response.CacheControl = "no-cache";
            Response.Expires = 0;
            channelList.InnerHtml = newsstr();
        }
    }

    string newsstr()
    {
        string liststr = "";
        DataTable dt = pd.GetselectNewsList();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                liststr += "<img src=\"../../sysImages/normal/s.gif\" border=\"0\" class=\"SubItems\" /><span id=\"" + dt.Rows[i]["ChannelID"] + "\" class=\"LableItem\" ondblclick=\"ReturnValue(document.form1.channelID.value);\" onClick=\"SelectLable(this);sFiles('" + dt.Rows[i]["ChannelID"] + "');\" title=\"双击选择\">" + dt.Rows[i]["CName"] + "</span><br />";

            }
            dt.Clear();
            dt.Dispose();
        }
        return liststr;
    }
}
