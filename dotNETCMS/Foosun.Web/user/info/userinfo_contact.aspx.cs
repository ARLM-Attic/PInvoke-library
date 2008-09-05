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
using Foosun.CMS.Common;

public partial class user_info_userinfo_contact : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            DataTable dt = rd.getUserInfoContact(Foosun.Global.Current.UserNum);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.province.Text = dt.Rows[0]["province"].ToString();
                    this.City.Text = dt.Rows[0]["City"].ToString();
                    this.Address.Text = dt.Rows[0]["Address"].ToString();
                    this.Postcode.Text = dt.Rows[0]["Postcode"].ToString();
                    this.FaTel.Text = dt.Rows[0]["FaTel"].ToString();
                    this.WorkTel.Text = dt.Rows[0]["WorkTel"].ToString();
                    this.Fax.Text = dt.Rows[0]["Fax"].ToString();
                    this.QQ.Text = dt.Rows[0]["QQ"].ToString();
                    this.MSN.Text = dt.Rows[0]["MSN"].ToString();
                }
            }
        }
    }

    protected void submitSave(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {
            string province = Foosun.Common.Input.Htmls(this.province.Text);
            string City = Foosun.Common.Input.Htmls(this.City.Text);
            string Address = Foosun.Common.Input.Htmls(this.Address.Text);
            string Postcode = Foosun.Common.Input.Htmls(this.Postcode.Text);
            string FaTel = Foosun.Common.Input.Htmls(this.FaTel.Text);
            string WorkTel = Foosun.Common.Input.Htmls(this.WorkTel.Text);
            string Fax = Foosun.Common.Input.Htmls(this.Fax.Text);
            string QQ = Foosun.Common.Input.Htmls(this.QQ.Text);
            string MSN = Foosun.Common.Input.Htmls(this.MSN.Text);


            //同步更新用户信息
            Foosun.PlugIn.Passport.DPO_Request request = new Foosun.PlugIn.Passport.DPO_Request(Context);
            request.Province = province;
            request.City = City;
            request.address = Address;
            request.TelePhone = FaTel;
            request.QQ = QQ;
            request.MSN = MSN;
            request.UserName = Foosun.Global.Current.UserName;
            request.ProcessMultiPing("update");

            if (request.FoundErr)
            {
                PageError("同步更新用户信息失败", "userinfo_contact.aspx");
            }


            DataTable sdt = rd.getUserContactRecord(Foosun.Global.Current.UserNum);
            if (sdt != null)
            {
                if (sdt.Rows.Count > 0)
                {
                    Foosun.Model.UserInfo2 uc1 = new Foosun.Model.UserInfo2();
                    uc1.UserNum = Foosun.Global.Current.UserNum;
                    uc1.province = province;
                    uc1.City = City;
                    uc1.Address = Address;
                    uc1.Postcode = Postcode;
                    uc1.FaTel = FaTel;
                    uc1.WorkTel = WorkTel;
                    uc1.Fax = Fax;
                    uc1.QQ = QQ;
                    uc1.MSN = MSN;
                    rd.UpdateUserInfoContact1(uc1);
                }
                else
                {
                    Foosun.Model.UserInfo2 uc1 = new Foosun.Model.UserInfo2();
                    uc1.UserNum = Foosun.Global.Current.UserNum;
                    uc1.province = province;
                    uc1.City = City;
                    uc1.Address = Address;
                    uc1.Postcode = Postcode;
                    uc1.FaTel = FaTel;
                    uc1.WorkTel = WorkTel;
                    uc1.Fax = Fax;
                    uc1.QQ = QQ;
                    uc1.MSN = MSN;
                    rd.UpdateUserInfoContact2(uc1);
                }
            }
            else
            {
                Foosun.Model.UserInfo2 uc1 = new Foosun.Model.UserInfo2();
                uc1.UserNum = Foosun.Global.Current.UserNum;
                uc1.province = province;
                uc1.City = City;
                uc1.Address = Address;
                uc1.Postcode = Postcode;
                uc1.FaTel = FaTel;
                uc1.WorkTel = WorkTel;
                uc1.Fax = Fax;
                uc1.QQ = QQ;
                uc1.MSN = MSN;
                rd.UpdateUserInfoContact1(uc1);
            }
            PageRight("修改资料成功！", "userinfo_contact.aspx");
        }
    }
}
