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

public partial class manage_Contribution_Constr_Return : Foosun.Web.UI.ManagePage
{
    public manage_Contribution_Constr_Return()
    {
        Authority_Code = "C044";
    }
    Constr con = new Constr();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            string ConID = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
            DataTable dt = con.sel23(ConID);
            int ispass = int.Parse(dt.Rows[0]["ispass"].ToString());
            if (ispass == 1)
            {
                PageError("抱歉此稿已退不能再次退稿","");
            }
            this.Title.Text=dt.Rows[0]["Title"].ToString();
        }

    }
    protected void But_Click(object sender, EventArgs e)
    {
        rootPublic rd = new rootPublic();
        string ConIDs = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
        string passcontent = Foosun.Common.Input.Filter(Request.Form["passcontent"].ToString());
        if (con.Update6(passcontent, ConIDs) == 0)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "退稿", "操作失败");
            PageError("退稿失败", "");
        }
        else 
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "退稿", "操作成功");
            PageRight("退稿成功", "Constr_List.aspx");
        }
    }
}
