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

public partial class user_friend_Establishment : Foosun.Web.UI.UserPage
{
    Friend fri = new Friend();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            
            string UserNum = Foosun.Global.Current.UserNum;
            int fE = 0;
            if (fri.sel_6(UserNum) != null && fri.sel_6(UserNum) != "")
            {
                fE = int.Parse(fri.sel_6(UserNum));
                switch (fE)
                {
                    case 0:
                        this.RadioButtonList1.Items[2].Selected = true;
                        break;
                    case 1:
                        this.RadioButtonList1.Items[1].Selected = true;
                        break;
                    case 2:
                        this.RadioButtonList1.Items[0].Selected = true;
                        break;
                }
            }
        }
    }
    protected void addfriend_Click(object sender, EventArgs e)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        int FE = int.Parse(this.RadioButtonList1.SelectedValue);
        if (fri.Update(FE, UserNum)==0)
        {
            PageError("设置失败", "friendList.aspx");
        }
        else 
        {
            PageRight("设置成功", "friendList.aspx");
        }
    }
}
