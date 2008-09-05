//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
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

public partial class usermycom_Look : Foosun.Web.UI.ManagePage
{
    Mycom myc = new Mycom();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        string Commid = Foosun.Common.Input.Filter(Request.QueryString["Commid"]);
        DataTable dt = myc.sel(Commid);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                TitleBox.InnerHtml = dt.Rows[0]["Title"].ToString();
                ContentBox.InnerHtml = dt.Rows[0]["Content"].ToString();
                strUserNum.InnerHtml = "<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/ShowUser.aspx?uid=" + pd.getUserName(dt.Rows[0]["UserNum"].ToString()) + "\" target=\"_blank\"><font color=\"red\">" + pd.getUserName(dt.Rows[0]["UserNum"].ToString()) +"</font></a>";
                ipstr.InnerHtml = dt.Rows[0]["IP"].ToString();
                creatTime.InnerHtml = dt.Rows[0]["creatTime"].ToString();
            }
            dt.Clear(); dt.Dispose();
        }
    }   

}
