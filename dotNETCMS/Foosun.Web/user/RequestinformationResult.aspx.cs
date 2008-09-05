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

public partial class user_RequestinformationResult : Foosun.Web.UI.UserPage
{
    Foosun.CMS.Friend fir = new Foosun.CMS.Friend();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
        }
        DataTable Q_dfriend = fir.sel_9(Foosun.Global.Current.UserNum);
        this.infomationDownList.DataTextField = "FriendName";
        this.infomationDownList.DataValueField = "HailFellow";
        this.infomationDownList.DataSource = Q_dfriend;
        this.infomationDownList.DataBind();
    }

    /// <summary>
    /// 提交动作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string _ID = Request.QueryString["id"];
        if (_ID != null && _ID != string.Empty)
        {
            if (this.isCheck.Checked)
            {
                fir.Delete_2(Foosun.Global.Current.UserName, int.Parse(_ID.ToString()));
                Response.Write("<script>alert('拒绝用户添加您为好友成功。');window.opener.location.reload();window.close();</script>");
            }
            else
            {
                string u_menume = Foosun.Global.Current.UserName;
                string qUsername = fir.sel_10(u_menume);
                string qUserNum = fir.sel_11(qUsername);
                string u_meNume = Foosun.Global.Current.UserName;
                string bUserName = fir.sel_10(u_meNume);
                string bdUserName = fir.sel_11(bUserName);
                string Hail_Fellow = this.infomationDownList.SelectedValue;
                string FriendUserNum = Foosun.Common.Rand.Number(12);
                DateTime CreatTime = DateTime.Now;
                int se = this.infomationDownList.SelectedIndex;
                if (se == 0)
                {
                    fir.Add_6(FriendUserNum, Foosun.Global.Current.UserNum, bUserName, bdUserName, Hail_Fellow, CreatTime);
                    fir.Update_1(u_meNume, qUsername);
                    fir.Update_2(Foosun.Global.Current.UserNum, qUserNum);
                }
                else
                {
                    fir.Delete_2(Foosun.Global.Current.UserName, int.Parse(_ID.ToString()));
                }
                Response.Write("<script>alert('同意用户成为好友操作成功。');window.opener.location.reload();window.close();</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('错误的参数。');window.opener.location.reload();window.close();</script>");
        }
    }
}
