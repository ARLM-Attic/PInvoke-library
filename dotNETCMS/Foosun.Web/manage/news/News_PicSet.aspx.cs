//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                        Forum:bbs.foosun.net                     ==
//==                       WebSite:www.foosun.net                    ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                     Tel:86-28-85098980/66026180                 ==
//==                     QQ:655071,MSN:ikoolls@gmail.com             ==
//==                     Email:Service@foosun.cn                     ==
//==                       Code By WangZhenjiang                     ==
//=====================================================================
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

public partial class manage_news_News_PicSet : Foosun.Web.UI.ManagePage
{
    public string Udirs = "";
    public string dirDumm = Foosun.Config.UIConfig.dirDumm;
    public string dirFile = Foosun.Config.UIConfig.dirFile;
    ContentManage rd = new ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            if (dirDumm.Trim() != "") { dirDumm = dirDumm + "/"; }
            Udirs = dirDumm + dirFile;

            string ID=Request.QueryString["id"].ToString();
            string tb=Request.QueryString["tb"].ToString();
            DataTable dt = rd.sle_PicUrl(ID, tb);
            Picd.InnerHtml = Show_nod(dt.Rows[0]["PicURL"].ToString().Replace("{@dirfile}", Udirs));
            Picx.InnerHtml = Show_nox(dt.Rows[0]["SPicURL"].ToString().Replace("{@dirfile}", Udirs));
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string PicURL=this.PicURL.Text;
        string SPicURL=this.SPicURL.Text;
        string ID = Request.QueryString["id"].ToString();
        string tb = Request.QueryString["tb"].ToString();
        if (rd.Up_PicURL(PicURL, SPicURL, ID, tb) != 0)
        {
            Response.Write("<script language='javascript'>window.opener='anyone';window.close();</script>");
        }
    }
    string Show_nox(string pURLx)
    {
        string nos = "<img src=\"" + pURLx + "\" width=\"325px\" height=\"318px\" id=\"pic_p_1\" />";
        return nos;
    }
    string Show_nod(string pURLd)
    {
        string nos = "<img src=\"" + pURLd + "\" width=\"325px\" height=\"318px\" id=\"pic_p_2\" />";
        return nos;
    }
}
