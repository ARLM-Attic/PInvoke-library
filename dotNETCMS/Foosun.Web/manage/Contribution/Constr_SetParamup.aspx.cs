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

public partial class manage_Contribution_Constr_SetParamup : Foosun.Web.UI.ManagePage
{
    Constr con = new Constr();
    protected void Page_Load(object sender, EventArgs e)
    {

        string PCIdup = Foosun.Common.Input.Filter(Request.QueryString["PCId"].ToString());
        DataTable dt = con.sel25(PCIdup);

        this.ConstrPayName.Text = dt.Rows[0]["ConstrPayName"].ToString();
        this.moneys.Text = dt.Rows[0]["money"].ToString();
        this.ipoint.Text = dt.Rows[0]["iPoint"].ToString();
        this.gpoint.Text = dt.Rows[0]["gPoint"].ToString();
        if (dt.Rows[0]["Gunit"].ToString() != "")
        {
            for (int s = 0; s < this.Gunit.Items.Count - 1; s++)
            {
                if (this.Gunit.Items[s].Text == dt.Rows[0]["Gunit"].ToString())
                {
                    this.Gunit.Items[s].Selected = true;
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        rootPublic rd = new rootPublic();
        string PCIdup = Foosun.Common.Input.Filter(Request.QueryString["PCId"].ToString());
        string ConstrPayName = Foosun.Common.Input.Filter(Request.Form["ConstrPayName"].ToString());
        int moneys1 = int.Parse(Foosun.Common.Input.Filter(Request.Form["moneys"].ToString()));
        string ipoint = Foosun.Common.Input.Filter(Request.Form["ipoint"].ToString());
        string gpoint = Foosun.Common.Input.Filter(Request.Form["gpoint"].ToString());
        string Gunit = this.Gunit.SelectedItem.ToString();
        if (con.Update6(ConstrPayName, gpoint, ipoint, moneys1, Gunit, PCIdup) == 0)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "稿酬设置", "更新失败");
            PageError("添加失败<br>", "");
        }
        else
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "稿酬设置", "更新成功");
            PageRight("添加成功", "Constr_SetParamlist.aspx");
        }
    }
}