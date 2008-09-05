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
using System.Net.Mail;

public partial class SendMail : Foosun.Web.UI.UserPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            getNewsInfo();
    }

    /// <summary>
    /// 取得相关信息
    /// </summary>
    protected void getNewsInfo()
    {
        string NewsID = Foosun.Common.Input.Filter(Request.QueryString["id"]);
        if (NewsID != "" && NewsID != null)
        {
            Foosun.CMS.UserMisc user = new Foosun.CMS.UserMisc();
            DataTable dt = user.getUserUserNumRecord(Foosun.Global.Current.UserNum);
            if (dt != null && dt.Rows.Count > 0)
            {
                FROM.Text = dt.Rows[0]["Email"].ToString();
                dt.Clear(); dt.Dispose();
            }
            Foosun.CMS.ContentManage news = new Foosun.CMS.ContentManage();
            IDataReader dr = news.getNewsID(NewsID);
            if (dr.Read())
            {
                NewsTitle.InnerHtml = dr["NewsTitle"].ToString();
                Title.Value = dr["NewsTitle"].ToString();
                if (dr["Content"] != DBNull.Value)
                    Content.Value = Foosun.Common.Input.GetSubString(dr["Content"].ToString(), 500);

                if (dr["NewsType"].ToString() == "2")
                    NewsLinkURL.Value = dr["URLaddress"].ToString();
                else
                {
                    string SaveClassframe = "";
                    DataTable dc = news.getClassContent(dr["ClassID"].ToString());
                    if (dc != null && dc.Rows.Count > 0)
                    {
                        SaveClassframe = dc.Rows[0]["SavePath"] + "/" + dc.Rows[0]["SaveClassframe"].ToString();
                        dc.Clear(); dc.Dispose();
                        SaveClassframe = SaveClassframe.Replace("//", "/");
                    }
                    NewsLinkURL.Value = Foosun.Publish.CommonData.getUrl() + SaveClassframe + "/" + dr["SavePath"].ToString() + "/" + dr["FileName"].ToString() + dr["FileEXName"].ToString();
                }
                dr.Close();
            }
            else
            {
                dr.Close();
                Response.Write("<script language=\"javascript\">alert('参数传递错误!');history.back();</script>");
                Response.End();
            }
        }
        else
        {
            Response.Write("<script language=\"javascript\">alert('参数传递错误!');history.back();</script>");
            Response.End();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string smtpserver = Foosun.Config.UIConfig.smtpserver;
            string userName = Foosun.Config.UIConfig.emailuserName;
            string pwd = Foosun.Config.UIConfig.emailuserpwd;
            string strfrom = Foosun.Config.UIConfig.emailfrom;
            string strto = TO.Text;
            string subj = FROM.Text + " 给你发送了一篇精彩新闻";
            string bodys = "<div><a href=\"" + NewsLinkURL.Value + "\">" + Title.Value + "</a><div>";
            bodys += "<br /><br />";
            bodys += "<div>" + Content.Value + "</div>";
            Foosun.Common.Public.sendMail(smtpserver, userName, pwd, strfrom, strto, subj, bodys);
            PageRight("发送成功！", "");
        }
    }
}
