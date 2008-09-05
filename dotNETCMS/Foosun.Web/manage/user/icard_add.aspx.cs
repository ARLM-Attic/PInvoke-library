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

public partial class manage_user_icard_add : Foosun.Web.UI.ManagePage
{
    public manage_user_icard_add()
    {
        Authority_Code = "U025";
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
            string CardNumber = this.CardNumber.Text;
            string CardPassWord = this.CardPassWord.Text;
            if (CardNumber.Length < 5 || CardPassWord.Length < 6)
            {
                PageError("点卡长度不能少于5.位密码长度:5-30<li>密码不能少于6位.密码长度:6-50</li>", "");
            }
            //------------------获取表单值-----------------------------------------
            #region 判断卡号是否重复
            DataTable dt = rd.getCardNumberTF(CardNumber);
            if (dt != null)
            {
                if (dt.Rows.Count > 0){PageError("卡号已经存在", "");}
                dt.Clear();dt.Dispose();
            }
            #endregion 判断卡号是否重复
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

            string CaID = Foosun.Common.Rand.Number(12);//产生12位随机字符
            Foosun.Model.IDCARD uc = new Foosun.Model.IDCARD();
            uc.CaID = CaID;
            uc.CardNumber = CardNumber;
            uc.CardPassWord =  FSSecurity.FDESEncrypt(CardPassWord,1);
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
            PageRight("添加点卡成功。", "icard.aspx");
        }
    }
}
