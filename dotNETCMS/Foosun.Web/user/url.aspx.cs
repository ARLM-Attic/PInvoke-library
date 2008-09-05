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
using Foosun.Common;

public partial class user_url : Foosun.Web.UI.BasePage
{
    UserMisc rd = new UserMisc();
    public string fURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            copyright.InnerHtml = CopyRight;
            string uid = Request.QueryString["uid"];
            fURL = Request.Url.ToString();
            if (uid == string.Empty || uid == null)
            {
                PageError("找不到该页面", "../");
            }
             urlList.InnerHtml = getURLlist(uid.ToString());
        }
    }

    public string getURLlist(string uid)
    {
        string list = "<table width=\"70%\" border=\"0\" align=\"center\" cellpadding=\"6\" cellspacing=\"1\" class=\"table\">\r";
        list += "<tr align=\"center\" class=\"TR_BG\"><td style=\"font-size:14px;\"><strong>网址分类</strong></td><td style=\"font-size:14px;\"><strong>相关联接</strong></td></tr>";
        string UserNum = Foosun.Common.Input.NcyString(uid);
        DataTable dt = rd.getClassList(UserNum);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Classstyle = "";
                if (i % 2 == 0)
                {
                    Classstyle = " class=\"TR_BG_list\"";
                }
                else
                {
                    Classstyle = " class=\"TR_BG\"";
                }
                list += "   <tr" + Classstyle + ">\r";
                list += "       <td align=\"center\" style=\"width:90px;\">\r";
                list += "       <span style=\"font-size:14px;\">" + dt.Rows[i]["ClassName"] + "</span>";
                list += "       </td>\r";
                list += "       <td>\r";
                DataTable dts = rd.getClassURLList(int.Parse(dt.Rows[i]["id"].ToString()));
                if (dts != null && dts.Rows.Count > 0)
                {
                    for (int j = 0; j < dts.Rows.Count; j++)
                    {
                        string URLs = dts.Rows[j]["URL"].ToString();
                        string TmpUrls = "";
                        string lists = "";
                        if (URLs.IndexOf("http://") >= 0)
                        {
                            TmpUrls = URLs;
                        }
                        else
                        {
                            TmpUrls = "http://" + URLs;
                        }
                        string URLColor = dts.Rows[j]["URLColor"].ToString();
                        if (URLColor.Trim() != string.Empty && URLColor != null)
                        {
                            lists = "<a title=\"" + dts.Rows[j]["Content"].ToString() + "\" href=\"" + TmpUrls + "\" target=\"_blank\"><font color=" + URLColor + ">" + dts.Rows[j]["URLName"].ToString() + "</font></a>";
                        }
                        else
                        {
                            lists += "<a title=\"" + dts.Rows[j]["Content"].ToString() + "\" href=\"" + TmpUrls + "\" target=\"_blank\" class=\"list_link\">" + dts.Rows[j]["URLName"].ToString() + "</a>";
                        }

                        list += lists + "&nbsp;&nbsp;&nbsp;";
                    }
                    dts.Clear(); dts.Dispose();
                }
                list += "       </td>\r";
                list += "   </tr>\r";
            }

            dt.Clear(); dt.Dispose();
        }
        list += "</table>";
        return list;
    }
}
