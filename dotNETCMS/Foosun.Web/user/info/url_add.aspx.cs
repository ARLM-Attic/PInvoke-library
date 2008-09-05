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

public partial class user_info_url_add : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            string ID = Request.QueryString["id"];
            if (ID != null && ID != string.Empty)
            {
                DataTable dt = rd.getURL(int.Parse(ID.ToString()));
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.URLName.Text = dt.Rows[0]["URLName"].ToString();
                    this.URL.Text = dt.Rows[0]["URL"].ToString();
                    this.Content.Text = dt.Rows[0]["Content"].ToString();
                    //this.URLName.Text = dt.Rows[0]["URLName"].ToString();
                    //URLColor = dt.Rows[0]["URLColor"].ToString();
                    string URLColor = dt.Rows[0]["URLColor"].ToString();
                    for (int k = 0; k < this.URLColor.Items.Count; k++)
                    {
                        if (this.URLColor.Items[k].Value == URLColor)
                        {
                            this.URLColor.Items[k].Selected = true;
                        }
                    }
                }
                DataTable dts = rd.getClassList(Foosun.Global.Current.UserNum);
                if (dts != null)
                {
                    this.ClassID.DataSource = dts;
                    this.ClassID.DataTextField = "ClassName";
                    this.ClassID.DataValueField = "ID";
                    this.ClassID.DataBind();
                    for (int m = 0; m < this.ClassID.Items.Count; m++)
                    {
                        if (this.ClassID.Items[m].Value == dt.Rows[0]["ClassID"].ToString()) 
                        { 
                            this.ClassID.Items[m].Selected = true; 
                        }
                    }
                }
            }
            else
            {
                DataTable dts = rd.getClassList(Foosun.Global.Current.UserNum);
                if (dts != null)
                {
                    this.ClassID.DataSource = dts;
                    this.ClassID.DataTextField = "ClassName";
                    this.ClassID.DataValueField = "ID";
                    this.ClassID.DataBind();
                }
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string URLName = Foosun.Common.Input.Htmls(this.URLName.Text);
            string URL = this.URL.Text;
            string URLColor = this.URLColor.SelectedValue;
            string ClassID = this.ClassID.SelectedValue;
            string Content = Foosun.Common.Input.Htmls(this.Content.Text);
            string ID = Request.QueryString["id"];
            if (URLName == string.Empty  || URL == string.Empty || ClassID == string.Empty)
            {
                PageError("带*必须填写", "");
            }
            else
            {
                if (ID != null && ID != string.Empty)
                {
                    rd.updateURL(URLName, URL, URLColor, ClassID, Content, 1, int.Parse(ID));
                }
                else
                {
                    rd.updateURL(URLName, URL, URLColor, ClassID, Content, 0, 0);
                }
            }
            PageRight("操作成功", "URL.aspx");
        }
    }
}
