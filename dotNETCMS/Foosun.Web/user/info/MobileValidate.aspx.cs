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
using System.Xml;

public partial class user_info_MobileValidate : Foosun.Web.UI.UserPage
{
    public user_info_MobileValidate()
    {
        UserCertificate = false;
    }
    Foosun.CMS.user rd = new Foosun.CMS.user();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            copyright.InnerHtml = CopyRight;
            string uid = Request.QueryString["uid"];
            if (uid != null && uid != "" )
            {
                string mobile;
                string mobileverify;
                bool flag = rd.sel_getUserMobileCode(uid, out mobile, out mobileverify);
                if (flag)
                {
                    PageError("您已经验证了手机.无须再验证", "");
                }
                if (mobile == string.Empty)
                {
                    PageError("找不到用户,或许您并非注册用户,或者您还没有填写手机号码!", "");
                }
                RegMobile.InnerHtml = mobile;
                uid_div.InnerHtml = uid;
                forgetPass.InnerHtml = forgetPassstr(uid, mobile);
                showContents_div.InnerHtml = showMContent();
                string _Type = Request.QueryString["Type"];
                if (_Type != null && _Type != string.Empty)
                {
                    if (_Type.ToString() == "ReGet")
                    {
                        ///发送信息到ISP。
                        ShowInfo.InnerHtml = "手机验证码已经发送到你的手机：" + mobile + ".请注意查收。根据网络情况。您可能在几分钟后收到短信。请不要刷新本页面。";
                    }
                }
            }
            else
            {
                PageError("错误的参数.", "");
            }
        }
    }

    protected string forgetPassstr(string uid, string mb)
    {
        string STR = "";
        STR = "<a href=\"MobileValidate.aspx?Type=ReGet&uid=" + uid + "\"><font color=\"Red\">获得手机验证码</font></a>";
        return STR;
    }

    protected string showMContent()
    {
        string _Str = "";
        try
        {
            string _dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (_dirdumm.Trim() != "")
            { _dirdumm = "/" + _dirdumm; }
            if (!File.Exists(Server.MapPath(_dirdumm + "/xml/sys/mobileBindTF.xml"))) { PageError("找不到配置文件(/xml/sys/mobileBindTF.xml).<li>请与系统管理员联系。</li>", ""); }
            string xmlPath = Server.MapPath(_dirdumm + "/xml/sys/mobileBindTF.xml");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("mbindtfcontent");
            _Str = "" + elemList[0].InnerXml + "";
        }
        catch
        { 
            _Str = "配置文件有问题。/xml/sys/mobileBindTF.xml";
        }
        return _Str;
    }

    /// <summary>
    /// 验证手机验证码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string MCode = this.MobileCode.Text;
            string mb;
            string mbcode;
            bool flag = rd.sel_getUserMobileCode(Request.QueryString["uid"], out mb, out mbcode);
            if (flag)
            {
                PageError("您已经验证了手机.", "");
            }
            if (mb == string.Empty)
            {
                PageError("找不到用户,或许您并非注册用户,或者您未填写手机号码!", "");
            }

            if (MCode.ToLower() != Foosun.CMS.FSSecurity.FDESEncrypt(mbcode, 0).ToLower())
            {
                PageError("输入的验证码不正确.", "");
            }
            else
            {
                if (rd.sel_updateUserMobileStat(Request.QueryString["uid"]) == 0)
                {
                    PageError("认证失败。请与系统管理员联系", "");
                }
                else
                {
                    //开始捆绑手机
                    if (this.BindTF.Checked)
                    {
                        //查询是否捆绑
                        if (rd.sel_getUserMobileBindTF(mb) == 1)
                        {
                            Logout();
                            Response.Write("<script language=\"JavaScript\" type=\"text/javascript\">alert('手机验证成功/小灵通.\\n但您输入的手机/小灵通已经被别人捆绑占用,此次捆绑失败。');location.href='../login.aspx';</script>");
                        }
                        else
                        {
                            //更新捆绑
                            rd.sel_updateMobileBindTF(Request.QueryString["uid"]);
                            Logout();
                            Response.Write("<script language=\"JavaScript\" type=\"text/javascript\">alert('手机验证成功\\n并捆绑了手机/小灵通');location.href='../login.aspx';</script>");
                        }
                    }
                    else
                    {
                        Logout();
                        Response.Write("<script language=\"JavaScript\" type=\"text/javascript\">alert('手机验证成功.');location.href='../login.aspx';</script>");
                    }
                }
            }
        }
    }
}
