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
using System.Xml;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.CMS.Common;
using Foosun.Model;
using Foosun.Config;
using System.Net;
using Foosun.PlugIn.Passport;

public partial class User_Login : Foosun.Web.UI.BasePage
{
    user rot = new user();
    rootPublic pd = new rootPublic();
    public string SiteID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AppendHeader("P3P", "CP=CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR");

        if (!IsPostBack)
        {
            Logout();
            string TmUrl = Request.QueryString["urls"];
            if (TmUrl != null && TmUrl != "")
            {
                string tmDir = Foosun.Config.UIConfig.dirUser.Trim() + "/index.aspx";
                if ((TmUrl.ToString().ToLower()).IndexOf(tmDir.ToLower()) == -1) { this.HidUrl.Value = TmUrl.ToString(); }
            }
            if (pd.getUserLoginCode() != 1) { safecodeTF.Visible = false; }
            else { safecodeTF.Visible = true; }
            SiteID = getSiteID();
        }
    }


    protected string getSiteID()
    {
        string _Str = "";
        string _dirdumm = Foosun.Config.UIConfig.dirDumm;
        if (_dirdumm.Trim() != "")
        { _dirdumm = "/" + _dirdumm; }
        if (!File.Exists(Server.MapPath(_dirdumm + "/site.xml"))) { PageError("找不到配置文件(/site.xml).<li>请与系统管理员联系。</li>", ""); }
        string xmlPath = Server.MapPath(_dirdumm + "/site.xml");
        System.Xml.XmlDocument xdoc = new XmlDocument();
        xdoc.Load(xmlPath);
        XmlElement root = xdoc.DocumentElement;
        XmlNodeList elemList = root.GetElementsByTagName("siteid");
        _Str = elemList[0].InnerXml;
        return _Str;
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string UserName = this.TxtName.Text;
        string PassWord = this.TxtPassword.Text;
        if (UserName.Trim() == string.Empty || PassWord.Trim() == string.Empty)
        {
            PageError("请输入用户名和密码!", "login.aspx?" + Request.QueryString);
        }
        if (pd.getUserLoginCode() != 0)
        {
            string SafeCode = this.TxtVerifyCode.Text;
            if (Session["CheckCode"] == null)
            {
                PageError("验证码已过期,请返回重新登录!", "login.aspx?" + Request.QueryString);
            }
            string _SafeCode = Session["CheckCode"].ToString().ToUpper();
            Session.Remove("CheckCode");
            if (SafeCode != _SafeCode)
            {
                PageError("验证码输入不正确!", "login.aspx?" + Request.QueryString);
            }
        }
        GlobalUserInfo info;
        EnumLoginState state = Login(UserName, PassWord, out info);
        if (state == EnumLoginState.Succeed)
        {
            Foosun.Global.Current.Set(info);
            
            if (info.uncert)
            {
                LoginResultShow(EnumLoginState.Err_UnCert);
            }
            if (Request.QueryString["reurl"] != null && Request.QueryString["reurl"].Trim() != string.Empty)
            {
                Response.Write("登录成功，自动转接中，请稍候……");
                //同步登录
                DPO_Request request = new DPO_Request(Context);
                request.RequestLogin(UserName, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PassWord, "MD5").Substring(8, 16).ToLower(), Request.QueryString["reurl"]);

                Response.End();
            }
            else
            {
                DPO_Request request = new DPO_Request(Context);
                request.RequestLogin(UserName, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PassWord, "MD5").Substring(8, 16).ToLower(), string.Format("{0}/user/index.aspx", Request.ApplicationPath == "/" ? string.Empty : Request.ApplicationPath));
                Response.End();
                /*
                #region 整合Discuz!NT
                try
                {
                    string xmlName = Server.MapPath("..\\api\\dz\\Adapt.config");
                    AdaptConfig conf = new AdaptConfig(xmlName);
                    if (conf.isAdapt)
                    {
                        string adaptePath = conf.adaptPath;
                        adaptePath += "?username=" + UserName + "&password=" + PassWord + "&tag=login";
                        //Response.Write("<script type=\"text/javascript\" language=\"javascript\">window.open(\"" + adaptePath + "\",\"\",\"left=5000,top=5000\");</script>");
                        string str = "<script language=\"javascript\" type=\"text/javascript\" src=\"" + adaptePath + "\"></script>";                         
                        Response.Write(str);          
                    }
                }
                finally
                {
                    Response.Write("<script language=\"javascript\">window.top.location.href=\"Index.aspx?urls=" + this.HidUrl.Value + "\";</script>");
                    System.Web.HttpContext.Current.Response.End();
                    //Response.End();
                    //Response.Redirect("index.aspx?urls=" + this.HidUrl.Value, false);
                } 
                 #endregion
                 * */
            }
        }
        else
        {
            LoginResultShow(state);
        }
    }
}
