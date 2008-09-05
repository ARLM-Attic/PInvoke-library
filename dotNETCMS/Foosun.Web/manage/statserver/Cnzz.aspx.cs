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
using System.IO;
using System.Net;


public partial class Manage_statserver_Cnzz : Foosun.Web.UI.ManagePage
{
    public string s_OpenTF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!Page.IsPostBack)
        {
            this.copyright.InnerHtml = CopyRight;
            getTF();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string s_XMLPath = "xml/sys/Cnzz.Config";
            if (OpenTF.Text == "1")
            {
                string s_Url = "http://intf.cnzz.com/user/companion/foosun.php?domain=" + Domain.Text.Trim() + "&" +
                               "key=" + Foosun.Common.Input.MD5(Domain.Text.Trim() + "CdfW9gQa", false) + "";
                string s_result = Request(s_Url);

                switch (s_result)
                {
                    case "-1":
                        PageError("key输入有误", "Cnzz.aspx");
                        break;
                    case "-2":
                        PageError("域名长度有误", "Cnzz.aspx");
                        break;
                    case "-3":
                        PageError("域名输入有误", "Cnzz.aspx");
                        break;
                    case "-4":
                        PageError("插入数据库有误", "Cnzz.aspx");
                        break;
                    case "-5":
                        PageError("同一个IP用户调用页面超过阀值", "Cnzz.aspx");
                        break;
                    case "-6":
                        PageError("连接统计服务器失败", "Cnzz.aspx");
                        break;
                    default:
                        string[] arr_result = s_result.Split('@');
                        Foosun.Common.Public.SaveXmlConfig("Open", "1", s_XMLPath);
                        Foosun.Common.Public.SaveXmlConfig("Domain", Domain.Text.Trim(), s_XMLPath);
                        Foosun.Common.Public.SaveXmlConfig("SiteID", arr_result[0].ToString(), s_XMLPath);
                        Foosun.Common.Public.SaveXmlConfig("Password", arr_result[1].ToString(), s_XMLPath);
                        break;
                }
            }
            else
            {
                Foosun.Common.Public.SaveXmlConfig("Open", "0", s_XMLPath);
                Foosun.Common.Public.SaveXmlConfig("Domain", "", s_XMLPath);
                Foosun.Common.Public.SaveXmlConfig("SiteID", "0", s_XMLPath);
                Foosun.Common.Public.SaveXmlConfig("Password", "0", s_XMLPath);
            }
            PageRight("修改成功", "Cnzz.aspx");
        }
    }

    protected void getTF()
    {
        s_OpenTF = Foosun.Common.Public.readparamConfig("Open", "Cnzz");
        string s_Domain = Foosun.Common.Public.readparamConfig("Domain", "Cnzz");
        OpenTF.Text = s_OpenTF;
        Domain.Text = s_Domain;
    }

    protected string Request(string Url)
    {
        Uri uri = new Uri(Url);
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

        Stream responseStream = response.GetResponseStream();
        StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.Default);
        return readStream.ReadToEnd();
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        string s_SiteID = Foosun.Common.Public.readparamConfig("SiteID", "Cnzz");
        string s_Password = Foosun.Common.Public.readparamConfig("Password", "Cnzz");
        string s_Url = "http://intf.cnzz.com/user/companion/foosun_login.php?site_id=" + s_SiteID + "&password=" + s_Password + "";
        string s_result = Request(s_Url);

        switch (s_result)
        { 
            case "-1":
                PageError("参数为空或参数非数字", "Cnzz.aspx");
                break;
            case "-2":
                PageError("password错误", "Cnzz.aspx");
                break;
            default:
                ExecuteJs("window.open('" + s_Url + "')");
                break;
        }
        s_OpenTF = "1";
    }
}

