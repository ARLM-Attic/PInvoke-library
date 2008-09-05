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

public partial class manage_user_icard_adds : Foosun.Web.UI.ManagePage
{
    public manage_user_icard_adds()
    {
        Authority_Code = "U026";
    }
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
        }
    }

    protected void sumbitsave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)                       //判断是否验证成功
        {
            if (this.cardlen.Text.ToString().Length > 30 || this.passlen.Text.ToString().Length >30)
            {
                PageError("点卡长度不能大于30.位密码长度:5-30<li>密码长度不能大于30位.密码长度:6-30</li>", "");
            }
            int cNumber = int.Parse(this.cNumber.Text);
            int cardlen = int.Parse(this.cardlen.Text);
            int passlen = int.Parse(this.passlen.Text);
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
            int isChar_num = int.Parse(this.isChar_num.SelectedValue);
            int pass_card_num = int.Parse(this.pass_card_num.SelectedValue);
            string recard = this.recard.Text;
            int carNums = 0;
            //try
            //{
                for (int k=0; k < cNumber; k++)
                {
                    string CaID = Foosun.Common.Rand.Number(12);//产生12位随机字符
                    string cardlenID;
                    if (isChar_num == 1)
                    {
                        cardlenID = recard + Foosun.Common.Rand.Str(cardlen - recard.Length,true);
                    }
                    else
                    {
                        cardlenID = recard + Foosun.Common.Rand.Number(cardlen - recard.Length, true);
                    }

                    string passlenID;
                    if (pass_card_num == 1)
                    {
                        passlenID = Foosun.Common.Rand.Str(passlen, true);
                    }
                    else
                    {
                        passlenID = Foosun.Common.Rand.Number(passlen, true);
                    }
                    //判断卡号是否重复
                    DataTable dtc = rd.getCardNumberTF(cardlenID);
                    if (dtc != null)
                    {
                        if (dtc.Rows.Count > 0){continue;}
                        dtc.Clear(); dtc.Dispose();
                    }
                    if (rd.getCardPassTF(FSSecurity.FDESEncrypt(passlenID, 1)) == true) { continue; }
                    carNums = carNums + 1;
                    Foosun.Model.IDCARD uc = new Foosun.Model.IDCARD();
                    uc.CaID = CaID;
                    uc.CardNumber = cardlenID;
                    uc.CardPassWord = FSSecurity.FDESEncrypt(passlenID, 1);
                    uc.creatTime = DateTime.Now;
                    uc.Money = Money;
                    uc.Point = Point;
                    uc.isBuy = isbuy;
                    uc.isUse = isuse;
                    uc.isLock = islock;
                    uc.UserNum = "";
                    uc.siteID = SiteID;
                    uc.TimeOutDate = TimeOutDates;
                    rd.insertCardR(uc);
                }
                PageRight("添加点卡成功。<li>您需要生成的点卡数是：" + cNumber + ".实际共生成点卡" + carNums + "张。</li><li>如果不一致，可能原因是：生成过程中有卡号或者密码重复,从而跳过。</li>", "icard.aspx");
            //}
            //catch(Exception UX)
            //{
            //    PageError("系统错误.<li>"+UX.ToString()+"</li>", "");
            //}
        }    
    }
}
