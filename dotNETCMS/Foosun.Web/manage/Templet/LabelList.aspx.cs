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

public partial class manage_Templet_LabelList : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)                                               
        {
            
            string getSiteID = Request.QueryString["SiteID"];
            string _sys = Request.QueryString["sys"];
            int intsys = 0;
            if (_sys != "" && _sys != null)
            {
                intsys = 1;
                getLabelList(SiteID, 1);
            }
            else
            {
                if (getSiteID != null && getSiteID != "")
                {
                    channelList.InnerHtml = SiteList(getSiteID);
                    getLabelList(getSiteID.ToString(), intsys);
                }
                else
                {
                    channelList.InnerHtml = SiteList(SiteID);
                    getLabelList(SiteID, intsys);
                }
            }
        }
    }

    /// <summary>
    /// 得到站点列表
    /// </summary>
    /// <param name="SessionSiteID"></param>
    /// <returns></returns>
    protected string SiteList(string SessionSiteID)
    {
        Foosun.CMS.UserMisc rd = new Foosun.CMS.UserMisc();
        string siteStr = "<select class=\"form\" name=\"SiteID\" id=\"SiteID\" onChange=\"getchanelInfo(this)\">\r";
        DataTable crs = rd.getSiteList();
        if (crs != null)
        {
            for (int i = 0; i < crs.Rows.Count; i++)
            {
                string getSiteID = SessionSiteID;
                string SiteID1 = crs.Rows[i]["ChannelID"].ToString();
                if (getSiteID != SiteID1) { siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\">==" + crs.Rows[i]["CName"] + "==</option>\r"; }
                else { siteStr += "<option value=\"" + crs.Rows[i]["ChannelID"] + "\"  selected=\"selected\">==" + crs.Rows[i]["CName"] + "==</option>\r"; }
            }
            crs.Clear(); crs.Dispose();
        }
        siteStr += "</select>\r";
        return siteStr;
    }


    protected void getLabelList(string SiteID, int intsys)
    {
        Foosun.CMS.Label lb = new Foosun.CMS.Label();
        DataTable dt = lb.getLableList(SiteID, intsys);

        string str_tempList = string.Empty;
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tmpStr = "";
                if (Request.QueryString["sys"] == "1")
                {
                    tmpStr =  dt.Rows[i]["Label_Name"].ToString().Replace("{FS_S_", "").Replace("}", "");
                }
                else
                {
                    tmpStr = dt.Rows[i]["Label_Name"].ToString().Replace("{FS_", "").Replace("}", "");
                }
                str_tempList += "<li><a title=\"" + dt.Rows[i]["Description"].ToString() + "\" href=\"javascript:selectLabel(document.Label.Label" + i + ".value);\" " +
                                " class=\"list_link\" style=\"font-size:11.5px;font-family:Verdana;\"><input name=\"Label" + i + "\" type=hidden " +
                                " value=\"" + dt.Rows[i]["Label_Name"].ToString() + "\" />" + tmpStr + "</a>";
                str_tempList += "</li>";
            }
            dt.Clear();
            dt.Dispose();
        }
        else
        {
            str_tempList += "<li>当前没有标签</li>";
        }
        LabelList.InnerHtml = "<ul>" + str_tempList + "</ul>";        
    }
}
