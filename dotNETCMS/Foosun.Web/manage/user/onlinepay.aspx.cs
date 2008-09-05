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

public partial class manage_user_onlinepay : Foosun.Web.UI.ManagePage
{
    public manage_user_onlinepay()
    {
        Authority_Code = "U030";
    }
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            DataTable dt = rd.getOnlinePay();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["onpayType"].ToString() == "0")
                        this.onpayType.Items[0].Selected = true;
                    else if (dt.Rows[0]["onpayType"].ToString() == "1")
                        this.onpayType.Items[1].Selected = true;
                    else if (dt.Rows[0]["onpayType"].ToString() == "2")
                        this.onpayType.Items[2].Selected = true;
                    else
                        this.onpayType.Items[3].Selected = true;
                    this.O_userName.Text = dt.Rows[0]["O_userName"].ToString();
                    this.O_key.Text = dt.Rows[0]["O_key"].ToString();
                    this.O_sendurl.Text = dt.Rows[0]["O_sendurl"].ToString();
                    this.O_returnurl.Text = dt.Rows[0]["O_returnurl"].ToString();
                    this.O_md5.Text = dt.Rows[0]["O_md5"].ToString();
                    this.O_other1.Text = dt.Rows[0]["O_other1"].ToString();
                    this.O_other2.Text = dt.Rows[0]["O_other2"].ToString();
                    this.O_other3.Text = dt.Rows[0]["O_other3"].ToString();
                }
                dt.Clear();
                dt.Dispose();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int onpayType = int.Parse(this.onpayType.SelectedValue);
            string O_userName = this.O_userName.Text;
            string O_key = this.O_key.Text;
            string O_sendurl = this.O_sendurl.Text;
            string O_returnurl = this.O_returnurl.Text;
            string O_md5 = this.O_md5.Text;
            string O_other1 = this.O_other1.Text;
            string O_other2 = this.O_other2.Text;
            string O_other3 = this.O_other3.Text;
            Foosun.Model.UserInfo6 uc = new Foosun.Model.UserInfo6();
            uc.onpayType = onpayType;
            uc.O_userName = O_userName;
            uc.O_key = O_key;
            uc.O_sendurl = O_sendurl;
            uc.O_returnurl = O_returnurl;
            uc.O_md5 = O_md5;
            uc.O_other1 = O_other1;
            uc.O_other2 = O_other2;
            uc.O_other3 = O_other3;
            rd.UpdateOnlinePay(uc); 
            PageRight("更新成功", "onlinepay.aspx");
        }
    }
}
