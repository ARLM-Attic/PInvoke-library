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
using System.Xml;
using Foosun.Config;
using System.Net;
using System.IO;
using Foosun.PlugIn.Passport;

namespace Foosun.Web.user
{
    public partial class Logout : Foosun.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("P3P", "CP=CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR");
            string tmpUserName = Foosun.Global.Current.UserName;
            Logout();
            DPO_Request request = new DPO_Request(Context);
            request.RequestLogout(tmpUserName, Request.ApplicationPath);
           
            
            try
            {
                /*
                #region 整合Discuz!NT
                XmlDocument xmlDoc = new XmlDocument();
                string xmlName = Server.MapPath("..\\api\\dz\\Adapt.config");
                AdaptConfig adConfig = new AdaptConfig(xmlName);           
                if (adConfig.isAdapt)
                {
                    string adaptePath = adConfig.adaptPath;
                    adaptePath += "?tag=logout";

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(adaptePath);

                    req.Method = "GET";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.AllowAutoRedirect = false;
                    req.Timeout = 1500;

                    HttpWebResponse Http_Res = (HttpWebResponse)req.GetResponse();

                    //if (Http_Res.StatusCode.ToString() != "OK")
                    //{
                    //    checkveriframe.InnerHtml = "";
                    //}
                    //else
                    //{
                    //    checkveriframe.InnerHtml = "<iframe style=\"width:98%;height:20px;\" frameborder=\"no\" border=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\" src=\"" + Foosun.Config.verConfig.getfoosunURL + "\"></iframe>";
                    //}

                    //Response.Write("<script type=\"text/javascript\" language=\"javascript\">window.open(\"" + adaptePath + "\",\"\",\"left=5000,top=5000\");</script>");
                    //Response.End();
                }
                #endregion
                 */
            }
            catch
            {
                Response.Write("<script type=\"text/javascript\" language=\"javascript\">window.location.href='Login.aspx';</script>");
                Response.End();
            }
            
        }
    }
}
