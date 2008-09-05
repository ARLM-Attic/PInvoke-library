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

public partial class manage_Contribution_Constr_Pay : Foosun.Web.UI.ManagePage
{
    Constr con = new Constr();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {

            string GetUnum = Foosun.Common.Input.Filter(Request.QueryString["UserNum"].ToString());
            if (GetUnum == UserNum)
            {
                PageError("不能给自己支付稿酬！", "");
            }
            DataTable dt_u = con.sel20(GetUnum);
            int cuts = dt_u.Rows.Count;
            if (cuts > 0)
            {
                this.LblName.Text = dt_u.Rows[0]["UserName"].ToString();
                this.LblName1.Text = dt_u.Rows[0]["UserName"].ToString();
                money_1.InnerHtml = dt_u.Rows[0]["ParmConstrNum"].ToString();
            }
            else
            {
                PageError("错误此用户已被删除不能进行此操作", "");
            }
            int iPC = 0;
            if (dt_u.Rows[0]["ParmConstrNum"].ToString() != null && dt_u.Rows[0]["ParmConstrNum"].ToString() != "")
            {
                iPC = int.Parse(dt_u.Rows[0]["ParmConstrNum"].ToString());
            }
            if (iPC == 0)
            {
                PageError("你已经支付了不能再次支付", "");
            }
            no.InnerHtml = Show_no();
            DataTable dt = con.sel21(GetUnum);
            int cut = dt.Rows.Count;
            if (cut > 0)
            {
                this.address.Text = dt.Rows[0]["address"].ToString();
                this.postcode.Text = dt.Rows[0]["postcode"].ToString();
                this.RealName.Text = dt.Rows[0]["RealName"].ToString();
                this.bankName.Text = dt.Rows[0]["bankName"].ToString();
                this.bankRealName.Text = dt.Rows[0]["bankRealName"].ToString();
                this.bankcard.Text = dt.Rows[0]["bankcard"].ToString();
            }
            else
            {
                PageError("对不起该会员没有付款资料不能付款", "");
            }
        }
    }

    string Show_no()
    {
        string GetUNum = Foosun.Common.Input.Filter(Request.QueryString["UserNum"].ToString());
        string nos = "(<a href=\"../../" + Foosun.Config.UIConfig.dirUser + "/showUser.aspx?uid=" + pd.getUserName(GetUNum) + "\" class=\"list_link\" target=\"_blank\">查看他的用户信息</a>)";
        return nos;
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        rootPublic rd = new rootPublic();
        string GetUNum = Foosun.Common.Input.Filter(Request.QueryString["UserNum"].ToString());
        DataTable dt = con.sel20(GetUNum);
        int ParmConstrNums = int.Parse(dt.Rows[0]["ParmConstrNum"].ToString());
        DateTime payTime = DateTime.Now;

        string constrPayID = Foosun.Common.Rand.Number(12);
        DataTable dt_CP = con.sel22();
        int cuts = dt_CP.Rows.Count;
        string constrPayIDs = "";
        if (cuts > 0)
        {
            constrPayIDs = dt_CP.Rows[0]["constrPayID"].ToString();
        }
        if (constrPayIDs != constrPayID)
        {
            if (con.Add5(GetUNum, ParmConstrNums, payTime, constrPayID) == 0 || con.Update5(GetUNum) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "支付稿酬", "操作失败");
                PageError("支付错误", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "支付稿酬", "操作成功");
                PageRight("支付成功", "Constr_Stat.aspx");
            }
        }
        else
        {
            PageError("支付错误可能编号重复", "");
        }
    }
}



