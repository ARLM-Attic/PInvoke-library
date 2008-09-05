//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==            Code By ZhenJiang.Wang                     == 
//===========================================================
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

public partial class user_Constrlistpass_DC : Foosun.Web.UI.UserPage
{
    Constr con = new Constr();
    protected void Page_Load(object sender, EventArgs e)
    {
        
         Response.CacheControl = "no-cache";
         if (!IsPostBack)
         {
             string ConID = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
             DataTable da = con.sel8(ConID);
             int cut = da.Rows.Count;
             if (cut==0)
             {
                 PageError("对不起数据错误", "ConstrList.aspx");
             }
             
             this.Title.Text = da.Rows[0]["Title"].ToString();
             this.creatTime.Text = da.Rows[0]["creatTime"].ToString();
             this.passcontent.Text = da.Rows[0]["passcontent"].ToString();
             this.Class.Text = con.sel9(da.Rows[0]["ClassID"].ToString());
         }
    }
}