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

public partial class manage_Contribution_Constr_SetParam : Foosun.Web.UI.ManagePage
{
    public manage_Contribution_Constr_SetParam()
    {
        Authority_Code = "C047";
    }
    Constr con = new Constr();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        rootPublic rd = new rootPublic();
        if (Page.IsValid)
        {
            string PCId = Foosun.Common.Rand.Number(12);
            string ConstrPayName = Foosun.Common.Input.Filter(Request.Form["ConstrPayName"].ToString());
            int moneys1 = int.Parse(Foosun.Common.Input.Filter(Request.Form["moneys"].ToString()));
            string ipoint = Foosun.Common.Input.Filter(Request.Form["ipoint"].ToString());
            string gpoint = Foosun.Common.Input.Filter(Request.Form["gpoint"].ToString());
            string Gunit = this.Gunit.SelectedItem.ToString();
            DataTable dt = con.sel24();
            int cutb = dt.Rows.Count;
            string PCIds = "";
            if (cutb > 0)
            {
                PCIds = dt.Rows[0]["PCId"].ToString();
            }
            if (PCIds != PCId)
            {
                if (con.Add6(PCId, ConstrPayName, gpoint, ipoint, moneys1, Gunit) == 0)
                {
                    rd.SaveUserAdminLogs(1, 1, UserNum, "稿酬设置", "添加失败");
                    PageError("添加失败。可能有重复名称", "");
                }
                else 
                {
                    rd.SaveUserAdminLogs(1, 1, UserNum, "稿酬设置", "添加成功");
                    PageRight("添加成功", "Constr_SetParamlist.aspx");
                }
            }
            else
            {
                PageError("对不起建立失败有可能是编号重复", "");
            }
        }
    }
}