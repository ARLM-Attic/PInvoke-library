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

public partial class user_photo_photoclass_up : Foosun.Web.UI.UserPage
{
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        
        if (!IsPostBack)
        {
            string ClassID = Foosun.Common.Input.Filter(Request.QueryString["ClassID"].ToString());
            this.ClassName.Text = pho.sel_17(ClassID);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime Creatime = DateTime.Now;
        string ClassName = Foosun.Common.Input.Filter(Request.Form["ClassName"].ToString());
        string ClassIDs = Foosun.Common.Input.Filter(Request.QueryString["ClassID"].ToString());
        if (pho.Update_3(ClassName, Creatime, ClassIDs) != 0)
            {
                PageRight("修改分类成功", "photoclass.aspx");
            }
            else 
            {
                PageError("修改失败", "photoclass.aspx");
            }
    }
}



