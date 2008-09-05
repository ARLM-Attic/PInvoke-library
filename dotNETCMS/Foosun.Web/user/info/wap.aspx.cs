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

public partial class user_info_wap : Foosun.Web.UI.UserPage
{
    Foosun.CMS.user rd = new Foosun.CMS.user();
    Foosun.CMS.ContentManage nd = new Foosun.CMS.ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            copyright.InnerHtml = CopyRight;
            wapGetParam.InnerHtml = getWap();
            wapContent.InnerHtml = getContent();
        }
    }

    /// <summary>
    /// 得到wap参数
    /// </summary>
    /// <returns></returns>
    protected string getWap()
    {
        string Retr = "";
        int WapTF = 0;
        string WapDomain = "";
        int WapLastNum = 0;
        string WapPath = "";
        DataTable dt = rd.getWapParam();
        if(dt!=null&&dt.Rows.Count>0)
        {
            WapTF = int.Parse(dt.Rows[0]["WapTF"].ToString());
            WapDomain = dt.Rows[0]["WapDomain"].ToString();
            WapLastNum = int.Parse(dt.Rows[0]["WapLastNum"].ToString());
            WapPath = dt.Rows[0]["WapPath"].ToString();
            dt.Clear(); dt.Dispose();
        }
        if (WapTF != 1)
        {
            Retr = "站点未开通wap!";
        }
        else
        {
            if (WapDomain.Length < 5)
            {
                string dirdumm = Foosun.Config.UIConfig.dirDumm;
                string wapDir = "";
                if (dirdumm.Trim() != "")
                {
                    wapDir = "/" + dirdumm;
                }
                string gDomain = Request.ServerVariables["SERVER_NAME"];
                Retr = "http://" + gDomain + wapDir + WapPath;
            }
            else
            {
                Retr = WapDomain;
            }
        }
        return "<span style=\"color:red;text-weight:bold;\">" + Retr + "</span>";
    }

    protected string getContent()
    {
        string Retr = "";
        int WapLastNum = 10;
        DataTable dtp = rd.getWapParam();
        if (dtp != null && dtp.Rows.Count > 0)
        {
            WapLastNum = int.Parse(dtp.Rows[0]["WapLastNum"].ToString());
            dtp.Clear(); dtp.Dispose();
        }

        IDataReader dr = rd.getWapContent("0");
        int m = 0;
        while (dr.Read())
        {
            if (m >= WapLastNum) { break; }
            Retr += "\r<li><a class=\"list_link\" href=\"../../xml/wap/Content/" + dr["NewsID"].ToString() + ".wml\">" + dr["NewsTitle"].ToString() + "<span style=\"font-size:10px;\">&nbsp;(" + dr["CreatTime"].ToString() + ")</span></li>\r";
            m++;
        }
        dr.Close();
        return Retr;
    }
}
