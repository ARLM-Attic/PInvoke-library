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

public partial class user_Constraccount_up : Foosun.Web.UI.UserPage
{
    //连接数据库
    Constr con = new Constr();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string ConID = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
        DataTable dt = con.sel4(ConID);
        int cut = dt.Rows.Count;
        if (cut==0)
        {
            PageError("对不起参数错误","");
        }
        addressBox.Text = dt.Rows[0]["address"].ToString();
        postcodeBox.Text = dt.Rows[0]["postcode"].ToString();
        RealNameBox.Text = dt.Rows[0]["RealName"].ToString();
        bankNameBox.Text = dt.Rows[0]["bankName"].ToString();
        bankaccountBox.Text = dt.Rows[0]["bankaccount"].ToString();
        bankcardBox.Text = dt.Rows[0]["bankcard"].ToString();
        bankRealNameBox.Text = dt.Rows[0]["bankRealName"].ToString();
    }
    #endregion
    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 添加数据
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string ConIDs = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
            string address = Foosun.Common.Input.Htmls(Request.Form["addressBox"].ToString());
            string postcode = Foosun.Common.Input.Htmls(Request.Form["postcodeBox"].ToString());
            string RealName = Foosun.Common.Input.Htmls(Request.Form["RealNameBox"].ToString());
            string bankName = Foosun.Common.Input.Htmls(Request.Form["bankNameBox"].ToString());
            string bankaccount = Foosun.Common.Input.Htmls(Request.Form["bankaccountBox"].ToString());
            string bankcard = Foosun.Common.Input.Htmls(Request.Form["bankcardBox"].ToString());
            string bankRealName = Foosun.Common.Input.Htmls(Request.Form["bankRealNameBox"].ToString());
            Foosun.Model.STuserother stcn;
            stcn.address = address;
            stcn.postcode = postcode;
            stcn.RealName = RealName;
            stcn.bankName = bankName;
            stcn.bankaccount = bankaccount;
            stcn.bankcard = bankcard;
            stcn.bankRealName = bankRealName;
            if (con.Update1(stcn, ConIDs) == 0)
            {
                PageError("修改失败<br>", "Constraccount.aspx");
            }
            else
            {
                PageRight("修改成功", "Constraccount.aspx");
            }
          }
      }
    #endregion
  }


