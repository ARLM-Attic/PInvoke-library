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

public partial class user_info_buyCard : Foosun.Web.UI.UserPage
{
    Foosun.CMS.UserMisc rd = new Foosun.CMS.UserMisc();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
        sMoney.InnerHtml = moneylist();
    }

    protected string moneylist()
    {
        string _Str = "<select name=\"sMoney\">\r";
        DataTable dt = rd.getmoneylist();
        if(dt!=null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _Str += "<option value=\"" + double.Parse(dt.Rows[i]["Money"].ToString()) * (pd.getDiscount(Foosun.Global.Current.UserNum)) + "\">" + String.Format("{0:C}", (double.Parse(dt.Rows[i]["Money"].ToString())) * (pd.getDiscount(Foosun.Global.Current.UserNum))) + " (原价：￥" + double.Parse(dt.Rows[i]["Money"].ToString()) + "，折扣率:" + pd.getDiscount(Foosun.Global.Current.UserNum) + ") </option>\r";
                }
            }
            else
            {
                Button1.Enabled = false;
                Button1.Text = "没有可购买的点卡";
            }
            dt.Clear(); dt.Dispose();
        }
        _Str += "</select>\r";
        return _Str;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        double pointNumber = double.Parse(Request.Form["sMoney"]);
        Response.Redirect("onlinePoint.aspx?pointNumber=" + pointNumber + "&s=1&dec=购买点卡");
    }
}
