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

public partial class user_disFundwarehouse : Foosun.Web.UI.UserPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void FHBut1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserNum = Foosun.Global.Current.UserNum;
            Discuss dis = new Discuss();
            if (!Foosun.Common.Input.IsInteger(this.FHTextBox1.Text) ||! Foosun.Common.Input.IsInteger(this.FHTextBox2.Text))
            {
                PageError("正确填写G币和点数!", "");
            }
            int iPoint1 = int.Parse(this.FHTextBox1.Text);
            int gPoint1 = int.Parse(this.FHTextBox2.Text);
            string DisID = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
            DateTime Creatime = DateTime.Now;
            string GhID = Foosun.Common.Rand.Number(12);//产生12位随机字符         
            //讨论组原有基金
            DataTable selFundwarehouse1 = dis.sel_44(DisID);
            string[] Fund = selFundwarehouse1.Rows[0]["Fundwarehouse"].ToString().Split('|');
            int iPoint2 = int.Parse(Fund[0].ToString());
            int gPoint2 = int.Parse(Fund[1].ToString());
            int iPoint3=iPoint2+iPoint1;
            int gPoint3=gPoint2+gPoint1;
            string Fundwarehouse1 = iPoint3 + " | " + gPoint3 ;
            string Fundwarehouse2 = iPoint1 + " | " + gPoint1 ;
             //捐献会员自己的基金
            DataTable selectig = dis.sel_45(UserNum);
            int iPoint = int.Parse(selectig.Rows[0]["iPoint"].ToString());
            int gPoint = int.Parse(selectig.Rows[0]["gPoint"].ToString());
            if ((iPoint <= iPoint1) && (gPoint <= gPoint1))
            {
                PageError("对不起你的资金不足不能捐献", "discussManage_list.aspx");
            }
            else
            {
                if (dis.Add_10(GhID, UserNum, gPoint1, iPoint1, Creatime) != 0 && dis.Add_11(Fundwarehouse2, UserNum, DisID, Creatime) != 0 && dis.Update_7(iPoint1, gPoint1, UserNum) != 0 && dis.Update_8(Fundwarehouse1, DisID) != 0)
                {
                    PageRight("感谢你为本讨论组捐献积分或金币", "discussManage_list.aspx");
                }
                else
                {
                    PageError("捐献错误", "discussManage_list.aspx");
                }
            }           
        }
    }
    protected void FHBut2_Click(object sender, EventArgs e)
    {
        this.FHTextBox1.Text = "";
        this.FHTextBox2.Text = "";
    }
}
