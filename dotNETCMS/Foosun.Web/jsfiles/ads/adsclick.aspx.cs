///************************************************************************************************************
///**********增加广告点击数Code By DengXi**********************************************************************
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

public partial class jsfiles_ads_adsclick : Foosun.Web.UI.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();

        string Type = Request.QueryString["Type"];
        DataTable dt = null;
        string str_Url = "";
        string adsID = Foosun.Common.Input.checkID(Request.QueryString["adsID"]);  //检测参数

        if (Type == "Txt")      //文字广告
        {
            //增加点击数
            ac.upClickNum(adsID, "1");

            //取得文字广告链接地址

            dt = ac.getAdsPicInfo(" AdID,AdLink", "adstxt", adsID);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    str_Url = dt.Rows[0]["AdLink"].ToString();
                    string str_TempID = dt.Rows[0]["AdID"].ToString();
                    //添加统计信息
                    ac.addStat(str_TempID, Request.ServerVariables["REMOTE_ADDR"].ToString());

                    dt.Clear();dt.Dispose();
                    
                    //转到广告链接页面
                    Response.Redirect(str_Url);
                }
                else
                {
                    PageError("参数错误!", "");
                }
                dt.Clear();
                dt.Dispose();
            }
            else
            {
                PageError("参数错误!", "");
            }
        }
        else       //图片广告
        {
            //增加点击数
            ac.upClickNum(adsID, "0");
            //取得广告链接地址
            dt = ac.getAdsPicInfo("LinkURL", "ads", adsID);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    str_Url = dt.Rows[0]["LinkURL"].ToString();
                    //添加统计信息
                    ac.addStat(adsID, Request.ServerVariables["REMOTE_ADDR"].ToString());
                    dt.Clear();dt.Dispose();
                    
                    //转到广告链接页面
                    Response.Redirect(str_Url);
                }
                else
                {
                    PageError("参数错误!", "");               
                }
                dt.Clear();
                dt.Dispose();
            }
            else
            {
                PageError("参数错误!", "");
            }
        }
    }

}
