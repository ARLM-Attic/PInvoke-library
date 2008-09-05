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

public partial class manage_js_JS_templetReview : Foosun.Web.UI.ManagePage
{
    public manage_js_JS_templetReview()
    {
        Authority_Code = "C051";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            templet_review.InnerHtml = templets();
        }
    }

    /// <summary>
    /// 得到内容
    /// </summary>
    /// <returns></returns>
    protected string templets()
    {
        string tmplet = "找不到内容模型";
        Foosun.CMS.JSTemplet rd = new Foosun.CMS.JSTemplet();
        string tid = Request.QueryString["tid"];
        if (tid != "" && tid != null)
        {
            DataTable dt =  rd.reviewTempletContent(tid.ToString());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    tmplet = dt.Rows[0]["JSTContent"].ToString();
                }
                dt.Clear(); dt.Dispose();
            }
        }
        else
        {
            PageError("参数错误!", "");
        }
        return tmplet;
    }

}
