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

public partial class manage_user_userinfo_contact : Foosun.Web.UI.ManagePage
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
            string uids = Foosun.Common.Input.Filter(Request.QueryString["id"]);
            int uid = 0;
            try
            {
                uid = int.Parse(uids);
            }
            catch (Exception us)
            {
                PageError("错误的参数.<li>具体错误："+us.ToString()+"</li>", "");
            }
            string getUserNum = pd.getUidUserNum(uid);
            suid.Value = uid.ToString();

            DataTable dt = rd.getUserInfoContact(getUserNum);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    province.Text = dt.Rows[0]["province"].ToString();
                    City.Text = dt.Rows[0]["City"].ToString();
                    Address.Text = dt.Rows[0]["Address"].ToString();
                    Postcode.Text = dt.Rows[0]["Postcode"].ToString();
                    FaTel.Text = dt.Rows[0]["FaTel"].ToString();
                    WorkTel.Text = dt.Rows[0]["WorkTel"].ToString();
                    Fax.Text = dt.Rows[0]["Fax"].ToString();
                    QQ.Text = dt.Rows[0]["QQ"].ToString();
                    MSN.Text = dt.Rows[0]["MSN"].ToString();
                }
            }
        }
    }

    protected void submitSave(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {
            string RealName = Request.Form["RealName"];
            string province = Request.Form["province"];
            string City = Request.Form["City"];
            string Address = Request.Form["Address"];
            string Postcode = Request.Form["Postcode"];
            string FaTel = Request.Form["FaTel"];
            string WorkTel = Request.Form["WorkTel"];
            string Fax = Request.Form["Fax"];
            string QQ = Request.Form["QQ"];
            string MSN = Request.Form["MSN"];
            int suid = int.Parse(Request.Form["suid"]);
            string getUserNum = pd.getUidUserNum(suid);

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

            DataTable sdt = rd.getUserContactRecord(getUserNum);
            if (sdt != null)
            {
                if (sdt.Rows.Count > 0)
                {
                    Foosun.Model.UserInfo2 uc1 = new Foosun.Model.UserInfo2();
                    uc1.UserNum = getUserNum;
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
                    uc1.UserNum = getUserNum;
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
                uc1.UserNum = getUserNum;
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
            PageRight("修改资料成功！", "userlist.aspx");
        }
    }
}
