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

public partial class survey_VoteJs : Foosun.Web.UI.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string Container = "Vote_HTML_ID";
        string Container = Request["ajaxid"];
        int Steps = 0, PicWidth = 60, Tid = 0;
        if (Request["TID"] == null)
        {
            Response.Write("document.write('投票编号不能为空');");
            Response.End();
        }
        else
        {
            Tid = int.Parse(Request["TID"]);
        }
        if (Request["Steps"] != null)
            Steps = int.Parse(Request["Steps"]);
        if (Request["PicW"] != null)
            PicWidth = int.Parse(Request["PicW"]);
        if(Request["OutHtmlID"] != null && !Request["OutHtmlID"].Trim().Equals(""))
            Container = Request["OutHtmlID"];
        Response.Write("function go(){new Ajax.Request('/survey/Vote_Show.aspx',{method:'post',parameters:'TID=" + Tid + "&OutHtmlID=" + Container + "&PicW=" + PicWidth + "&Steps=" + Steps + "',onComplete:function(transport){document.getElementById('" + Container + "').innerHTML = transport.responseText;}});}" + "go();");
        Response.End();
    }
}
