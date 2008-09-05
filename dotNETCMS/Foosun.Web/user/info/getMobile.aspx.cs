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
using System.IO;
using System.Xml;

public partial class user_info_getMobile : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            DataTable dt = rd.getMobileBindTF();
            if(dt!=null)
            {
                if (dt.Rows[0]["BindTF"].ToString() == "1")
                {
                    this.Mobile.Enabled = false;
                    this.Button1.Enabled = false;
                    this.Button1.Text = "已经捆绑了手机";
                    this.bindTF.Checked = true;
                    this.bindTF.Enabled = false;
                }
                dt.Clear(); dt.Dispose();
            }
            string mobileStr = Request.QueryString["MobileNum"];
            string bindTF = Request.QueryString["bindTF"];
            getmobileDiv.Visible = true;
            copyright.InnerHtml = CopyRight;
            showContents_div.InnerHtml = showMContent();
            bindCode.Visible = false;
            if (bindTF != null && bindTF != "")
            {
                bindCode.Visible = true;
                getmobileDiv.Visible = false;
                _tmpMobile.InnerHtml = Request.QueryString["MobileNumber"].ToString();
                this.BindMobile.Value = Request.QueryString["MobileNumber"].ToString();
            }
            this.Mobile.Text = mobileStr;
        }
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {
            string MobileNumber = this.Mobile.Text;
            if (MobileNumber.Length < 10)
            {
                PageError("请正确输入11位手机号码。<li>或者带区号的小灵通号码。</li>", "");
            }
            if (this.bindTF.Checked)
            {
                //向风讯网信通端口发送手机号码.并向手机返回验证码

                Response.Redirect("getMobile.aspx?MobileNumber=" + MobileNumber + "&bindTF=1");
            }
            else
            {
                rd.updateMobile(MobileNumber,0);
                PageRight("更新手机成功<li>此次操作并未捆绑手机/小灵通。</li>", "userinfo.aspx");
            }
        }
    }

    protected void Button1_Click_bindSave(object sender, EventArgs e)
    {
        if (Page.IsValid)                       //判断是否验证成功
        {
            string bindCodeNum = this.bindCodeNum.Text;
            string MobileNumber = this.BindMobile.Value;
            //开始验证输入的手机号是否被别人捆绑


            //向风讯网信通端口发送手机号码,并发送验证码.返回true,false
            rd.updateMobile(MobileNumber,1);
            PageRight("捆绑手机成功。", "userinfo.aspx");
        }
    }
    
}
