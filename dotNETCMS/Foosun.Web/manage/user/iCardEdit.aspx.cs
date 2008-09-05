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

public partial class manage_user_iCardEdit : Foosun.Web.UI.ManagePage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            DataTable dt = rd.getCardInfoID(int.Parse(Request.QueryString["id"]));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.CardNumber.Text = dt.Rows[0]["CardNumber"].ToString();
                    this.CardPassWord.Text = FSSecurity.FDESEncrypt(dt.Rows[0]["CardPassWord"].ToString(), 0);
                    this.Money.Text = Convert.ToDecimal(dt.Rows[0]["Money"]).ToString("0");
                    this.Point.Text = dt.Rows[0]["Point"].ToString();
                    this.TimeOutDate.Text = ((DateTime)dt.Rows[0]["TimeOutDate"]).ToString("yyyy-MM-dd");
                    int n = int.Parse(dt.Rows[0]["isLock"].ToString());
                    if (n == 0)
                        this.isLock.Items[0].Selected = true;
                    else
                        this.isLock.Items[1].Selected = true;

                    int n1 = int.Parse(dt.Rows[0]["isUse"].ToString());
                    if (n1 == 1)
                    {
                        PageError("使用过的点卡不能修改!", "iCard.aspx");
                    }
                    if (n1 == 0)
                        this.isUse.Items[0].Selected = true;
                    else
                        this.isUse.Items[1].Selected = true;

                    int n2 = int.Parse(dt.Rows[0]["isBuy"].ToString());
                    if (n2 == 0)
                        this.isBuy.Items[0].Selected = true;
                    else
                        this.isBuy.Items[1].Selected = true;

                    this.cId.Value = dt.Rows[0]["ID"].ToString();
                }
                else
                {
                    PageError("找不到记录", "");
                }
            }
            else
            {
                PageError("找不到记录", "");
            }
        }
    }

    protected void sumbitsave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)                       //判断是否验证成功
        {
            if (this.CardPassWord.Text.ToString().Length < 6)
            {
                PageError("密码不能少于6位.密码长度:6-50", "");
            }
            //------------------获取表单值-----------------------------------------

            string CardPassWord = this.CardPassWord.Text;
            int Money = int.Parse(this.Money.Text);
            int Point = int.Parse(this.Point.Text);
            DateTime TimeOutDates = System.DateTime.Now;
            if (this.TimeOutDate.Text != null && this.TimeOutDate.Text != "")
            {
                TimeOutDates = DateTime.Parse(this.TimeOutDate.Text);
            }
            else
            {
                TimeOutDates = DateTime.Parse("2099-01-01");
            }
            int islock = int.Parse(this.isLock.SelectedValue);
            int isbuy = int.Parse(this.isBuy.SelectedValue);
            int isuse = int.Parse(this.isUse.SelectedValue);
            int cId = int.Parse(this.cId.Value);

            Foosun.Model.IDCARD uc = new Foosun.Model.IDCARD();
            uc.CardPassWord = FSSecurity.FDESEncrypt(CardPassWord, 1);
            uc.Id = cId;
            uc.Money = Money;
            uc.Point = Point;
            uc.isBuy = isbuy;
            uc.isUse = isuse;
            uc.isLock = islock;
            uc.TimeOutDate = TimeOutDates;
            rd.UpdateCardR(uc);
            PageRight("更新点卡成功。", "icard.aspx");
        }
    }
}
