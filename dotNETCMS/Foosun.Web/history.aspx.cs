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
using Foosun.Common;
using Foosun.CMS;
using Foosun.Config;
using Foosun.Model;
using Foosun.Publish;

namespace Foosun.Web
{
    public partial class history : System.Web.UI.Page
    {
        /// <summary>
        /// code by arjun,归档新闻转向页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string year = Request.QueryString["year"];
            string month = Request.QueryString["month"];
            string day = Request.QueryString["day"];
            string Strday = year+"-"+month+"-"+day;
            year = Input.Filter(year);
            month = Input.Filter(month);
            day=Input.Filter(day);
            Foosun.CMS.Info inf=new Foosun.CMS.Info();
            int histornum = inf.historyCount(Strday);
            if (histornum<=0)
            {
                Response.Write("<script language=\"javascript\">alert(\"今天没有归档新闻！\");</script>");
                Response.Write("<script language=\"javascript\">location.replace(\"/\");</script>");
                Response.End();
            }
            string urls = "";
            string content = "{@year04}-{@month}/{@day}";
            content = content.Replace("{@year04}",year);
            content = content.Replace("{@year02}",year.Substring(2,2));
            content = content.Replace("{@month}",month);
            content = content.Replace("{@day}",day);
            string serverPort = Request.ServerVariables["Server_Port"].ToString();
            string sitedomain = CommonData.getUrl();
            //判断端口重置sitedomain
            if (sitedomain.IndexOf(":") == -1)
            {
                if (serverPort != "80")
                {
                    sitedomain = sitedomain + ":"+serverPort;
                }
            }
            urls = sitedomain + "/" + Foosun.Config.UIConfig.dirPige + "/" + content + "/index.html";
            Response.Redirect(urls);
        }
    }
}
