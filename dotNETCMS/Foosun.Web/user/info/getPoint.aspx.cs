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

public partial class user_getPoint : Foosun.Web.UI.UserPage
{
    Info inf = new Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void insert_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string CardNumber = null;
            string CardPassWord = null;
            decimal Money = 0;
            int isUse = 0;
            int isLock = 0;
            DateTime creatTimes = DateTime.Now;

            string Number = Foosun.Common.Input.Filter(Request.Form["CardNumber"].ToString());

            string passwords = Foosun.Common.Input.Filter(Request.Form["CardPassWord"].ToString());
            string password = FSSecurity.FDESEncrypt(passwords, 1);
            DateTime CreatTime1 = DateTime.Now;

            DataTable u_CardNm = inf.sel_9(Number);
            int cut = u_CardNm.Rows.Count;
            if (cut==0)
            {
                PageError("对不起此卡不存在", "getPoint.aspx");
            }
            else
            {
                CardNumber = u_CardNm.Rows[0]["CardNumber"].ToString();
                CardPassWord = u_CardNm.Rows[0]["CardPassWord"].ToString();
                Money = (decimal)u_CardNm.Rows[0]["Money"];
                isUse = int.Parse(u_CardNm.Rows[0]["isUse"].ToString());//是否使用
                int isBuy = int.Parse(u_CardNm.Rows[0]["isBuy"].ToString());//是否被买
                if (isBuy == 1)
                {
                    PageError("此卡已经被购买", "getPoint.aspx");
                }
                {
                    if (isUse !=0)
                    {
                        PageError("对不起,此卡已经被使用", "getPoint.aspx");
                    }
                    else
                    {
                        isLock = int.Parse(u_CardNm.Rows[0]["isLock"].ToString());//是否锁定
                        if (isLock !=0)
                        {
                            PageError("对不起,此卡已锁定", "getPoint.aspx");
                        }
                        else
                        {
                            creatTimes = DateTime.Parse(u_CardNm.Rows[0]["TimeOutDate"].ToString());
                            if (CreatTime1 > creatTimes)
                            {
                                PageError("对不起,此卡已过期", "getPoint.aspx");
                            }
                            else
                            {
                                if ((CardNumber == Number) && (CardPassWord == password))
                                {
                                    this.Panel1.Visible = false;
                                    this.Panel2.Visible = true;
                                    this.Money.Text = (String.Format("{0:C}", Money));
                                    this.cz.Text = Foosun.Common.Input.Filter(Request.Form["CardNumber"]);
                                    this.Pion.Text = u_CardNm.Rows[0]["Point"].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        string cnm = this.cz.Text;

        DataTable u_CardNm1 = inf.sel_10(cnm);
        int Money1 = 0;
        int points = 0;
        if (u_CardNm1 != null && u_CardNm1.Rows.Count > 0)
        {
            Money1 = Convert.ToInt32(u_CardNm1.Rows[0]["Money"].ToString().Remove(u_CardNm1.Rows[0]["Money"].ToString().IndexOf(".")));
             points = int.Parse(u_CardNm1.Rows[0]["Point"].ToString());
            u_CardNm1.Clear(); u_CardNm1.Dispose();
        }

        int Gh = inf.sel_12();    

        string contents="点卡充值";

        string GhID = Foosun.Common.Rand.Number(12);

        DateTime CreatTime = DateTime.Now;

        int u_isUse = inf.sel_11(cnm);
        if (u_isUse != 1)
        {
            if (Gh == 1)
            {
                if ((inf.Add1(GhID, UserNum, points, Money1, CreatTime, contents) != 0) && (inf.Update4(Money1, UserNum) != 0) && (inf.Update3(UserNum, cnm) != 0))
                {
                    PageRight("恭喜你充值成功", "getPoint.aspx");
                }
                else
                {
                    PageError("对不起充值失败", "getPoint.aspx");
                }
            }
            else
            {
                if ((inf.Add1(GhID, UserNum, points, Money1, CreatTime, contents) != 0) && (inf.Update5(points, UserNum) != 0) && (inf.Update3(UserNum, cnm) != 0))
                {
                    PageRight("恭喜你充值成功", "getPoint.aspx");
                }
                else
                {
                    PageError("对不起充值失败", "getPoint.aspx");
                }
            }
        }
        else
        {
            PageError("对不起此卡已经被使用过了", "getPoint.aspx");
        }
    }
}
