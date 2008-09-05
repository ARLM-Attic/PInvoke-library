///************************************************************************************************************
///**********显示广告Code By DengXi****************************************************************************
///************************************************************************************************************
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

public partial class jsfiles_ads_pic : Foosun.Web.UI.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string adsID = Request.QueryString["adsID"];
        adsID = Foosun.Common.Input.checkID(adsID);
        if (createJs.checkJs(adsID) == false)
        {
            Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo("leftPic,leftSize,LinkURL", "ads", adsID);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string str_leftPic = createJs.ReplaceDirfile(dt.Rows[0]["leftPic"].ToString());
                    string str_leftSize = dt.Rows[0]["leftSize"].ToString();
                    string[] arr_LiftSize = str_leftSize.Split('|');
                    string str_domain = dt.Rows[0]["LinkURL"].ToString();
                    if (str_leftPic.IndexOf(".swf") != -1)
                    {
                        if (str_leftPic.IndexOf("http://") != -1)
                        {
                            Response.Write("<a href=\"" + str_domain + "\" target=\"_blank\"><EMBED src=http://" + str_leftPic + " quality=high WIDTH=" + arr_LiftSize[0].ToString() + " HEIGHT=" + arr_LiftSize[1].ToString() + " TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/go/getflashplayer\"></EMBED></a>");
                        }
                        else
                        {
                            Response.Write("<a href=\"" + str_domain + "\" target=\"_blank\"><EMBED src=" + str_leftPic + " quality=high WIDTH=" + arr_LiftSize[0].ToString() + " HEIGHT=" + arr_LiftSize[1].ToString() + " TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/go/getflashplayer\"></EMBED></a>");
                        }
                    }
                    else
                    {
                        if (str_domain == null || str_domain.Equals(""))
                        {
                            str_domain = "#";
                        }
                        if (str_leftPic.IndexOf("http://") != -1)
                        {
                            Response.Write("<img onclick=\"window.showModalDialog('" + str_domain + "','','')\" src=http://" + str_leftPic + " border=0 width=" + arr_LiftSize[0].ToString() + " height=" + arr_LiftSize[1].ToString() + "></img>");
                        }
                        else
                        {
                            Response.Write("<img onclick=\"window.showModalDialog('" + str_domain + "','','')\" src=" + str_leftPic + " border=0 width=" + arr_LiftSize[0].ToString() + " height=" + arr_LiftSize[1].ToString() + "></img>");
                        }
                    }
                }
                else
                {
                    Response.Write("此广告已暂停或失效!");
                }
            }
            else
            {
                Response.Write("此广告已暂停或失效!");
            }
            dt.Clear();
            dt.Dispose();
        }
        else
        {
            Response.Write("此广告已暂停或失效!");
        }
    }
}
