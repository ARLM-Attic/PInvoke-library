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
using System.Xml;
using Foosun.Config;
using System.Net;
using System.IO;

public partial class manage_ChangePassword : Foosun.Web.UI.ManagePage
{
    public manage_ChangePassword()
    {
        Authority_Code = "Q035";
    }
    Info inf = new Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.Expires = 0;

        if (!IsPostBack)
        {
        Response.CacheControl = "no-cache";
        
        }
    }
    protected void saveSumbit(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserNum = Foosun.Global.Current.UserNum;
            string oldpass = Foosun.Common.Input.Filter(Request.Form["oPass"]);
            string newPass = Request.Form["newPass"];
            string pnewPass = Request.Form["pnewPass"];
            string MD = Foosun.Common.Input.MD5(oldpass,true);
            string MD2 = Foosun.Common.Input.MD5(newPass, true);
            if (oldpass == "" || newPass == "" || newPass.Length<3)
            {
                PageError("填写完整。<li>或者密码太短密码不能小于3位</li>", "");
            }
            if (newPass != pnewPass)
            {
                PageError("两次密码不一致。", "");
            }
            else
            {
                if (inf.sel_2(UserNum,MD) == 0)
                {
                    PageError("原始密码错误。", "");
                }
                else
                {
                    if (inf.Update(MD2, UserNum) == 0)
                    {
                        PageError("意外错误。", "");
                    }
                    else
                    {
                        string BbsApiErr = string.Empty;
                        #region 整合Discuz!NT
                        try
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            string xmlName = Server.MapPath("../../api/dz/Adapt.config");
                            AdaptConfig adConfig = new AdaptConfig(xmlName);

                            if (adConfig.isAdapt)
                            {
                                string adaptePath = adConfig.adaptPath;
                                adaptePath += "?username=" + Foosun.Global.Current.UserName + "&password=" + newPass + "&tag=change";
                                //PageRight("修改密码成功", "ChangePassword.aspx", adaptePath, adaptPrams);

                                Uri uri = new Uri(adaptePath);
                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                                request.KeepAlive = false;
                                request.ProtocolVersion = HttpVersion.Version10;
                                request.Method = "GET";
                                request.ContentType = "application/x-www-form-urlencoded";
                                request.Proxy = System.Net.WebProxy.GetDefaultProxy();
                                request.AllowAutoRedirect = true;
                                request.MaximumAutomaticRedirections = 10;
                                request.Timeout = (int)new TimeSpan(0, 0, 1).TotalMilliseconds;
                                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                //Stream responseStream = response.GetResponseStream();
                                //StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.Default);
                                //readStream.ReadToEnd();
                            }
                        }
                        catch
                        {
                            BbsApiErr = "但因为您开启了BBS整合，但修改BBS的密码错误.";
                        }
                        #endregion

                        PageRight("修改密码成功." + BbsApiErr + "", "ChangePassword.aspx");
                    }
                }
            }
        }
    }
}
