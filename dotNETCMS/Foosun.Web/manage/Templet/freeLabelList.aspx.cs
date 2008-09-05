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

public partial class manage_Templet_freeLabelList : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            getLabelList();
        }
    }


    protected void getLabelList()
    {
        Foosun.CMS.Label lb = new Foosun.CMS.Label();
        DataTable dt = lb.getfreeLableList();

        string str_tempList = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#FFFFFF\" class=\"table\">";
        str_tempList += "<tr class=\"TR_BG_list\" >";
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str_tempList += "<td align=\"left\" valign=\"middle\" style=\"width:33%\">";
                str_tempList += "<a href=\"javascript:selectLabel(document.Label.Label" + i + ".value);\" " +
                                " class=\"list_link\" style=\"font-size:11.5px;font-family:Verdana;\"><input name=\"Label" + i + "\" type=hidden " +
                                " value=\"" + dt.Rows[i]["LabelName"].ToString() + "\" />" + dt.Rows[i]["LabelName"].ToString() + "</a>";
                str_tempList += "</td>";
                if ((i + 1) % 3 == 0)
                { str_tempList += "<tr class=\"TR_BG_list\">"; }
            }
            dt.Clear();
            dt.Dispose();
        }
        else
        {
            str_tempList += "<td align=\"left\" valign=\"middle\" height=\"20\">当前没有自由标签</td>";
        }
        str_tempList += "</tr></table>";
        LabelList.InnerHtml = str_tempList;
    }
}
