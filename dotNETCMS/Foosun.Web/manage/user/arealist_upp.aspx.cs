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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.CMS;
using Foosun.CMS.Common;

public partial class user_arealist_upp : Foosun.Web.UI.ManagePage
{
    public user_arealist_upp()
    {
        Authority_Code = "U032";
    }
    Arealist ali = new Arealist();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.CacheControl = "no-cache";
            
            string Cid = Foosun.Common.Input.Filter(Request.QueryString["Cid"].ToString());
            DataTable dt = ali.sel_7(Cid);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.cityName.Text = dt.Rows[0]["cityName"].ToString();
                    this.OrderID.Text = dt.Rows[0]["OrderID"].ToString();
                }
                dt.Clear(); dt.Dispose();
            }
        }
    }
    protected void but1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            rootPublic rd = new rootPublic();
            string cityName = Foosun.Common.Input.Filter(Request.Form["cityName"].ToString());
            string OrderID = this.OrderID.Text;
            if (!Foosun.Common.Input.IsInteger(OrderID))
            {
                PageError("排序号请用0-100的数字。数字越大，越靠前。", "arealist.aspx");
            }
            DateTime creatTime = DateTime.Now;
            string Cids = Foosun.Common.Input.Filter(Request.QueryString["Cid"].ToString());
            if (ali.Update_1(cityName, creatTime, Cids, int.Parse(OrderID)) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "修改小类", "修改错误");
                PageError("修改错误<br>", "arealist.aspx");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "修改小类", "修改成功");
                PageRight("修改成功", "arealist.aspx");
            }

        }
    }

}

