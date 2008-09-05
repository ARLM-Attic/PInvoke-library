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

public partial class user_getPassword : Foosun.Web.UI.BasePage
{
    Info inf = new Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        copyright.InnerHtml = CopyRight;
    }
    protected void firstBut_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserName = Foosun.Common.Input.Filter(Request.Form["firstnameBox"].ToString());
            DataTable selectUserName = inf.sel_7(UserName);
            if (selectUserName != null)
            {
                if (selectUserName.Rows.Count > 0)
                {
                    string u_UserName = selectUserName.Rows[0]["UserName"].ToString();
                    string PassQuestion = selectUserName.Rows[0]["PassQuestion"].ToString();
                    if ((UserName == u_UserName) && (PassQuestion != ""))
                    {
                        this.Panel1.Visible = false;
                        this.Panel2.Visible = true;
                        this.pwdBox1.Text = PassQuestion;
                        this.nms.Text = Request.Form["firstnameBox"].ToString();
                    }
                    else
                    {
                        PageError("您输入的用户名没有设置密码保护,不能取回密码", "");
                    }
                }
                else
                {
                    PageError("此用户 [" + UserName + "] 不存在!", "");
                }
            }
            else
            {
                PageError("此用户 [" + UserName + "] 不存在!", "");
            }
        }

    }
    protected void  secindBut_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string u_un = this.nms.Text;
            DataTable dt = inf.sel_8(Foosun.Common.Input.Filter(u_un.ToString()));
            string u_PassKey = dt.Rows[0]["PassKey"].ToString();
            string u_Email = dt.Rows[0]["Email"].ToString();
            string passkey = Foosun.Common.Input.MD5(Request.Form["pwdBox2"].ToString(), true);
            string email = Request.Form["emailBox"].ToString();
            if ((u_PassKey == passkey.ToLower()) && (u_Email.ToLower() == email.ToLower()))
            {
                this.Panel2.Visible = false;
                this.Panel3.Visible = true;
                this.nms2.Text = this.nms.Text;
            }
            else
            {
                PageError("您输入的密码答案或电子邮箱错误", "getPassword.aspx");
            }
        }
    }
    protected void pwds_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string u_nmss = this.nms2.Text;
            string u_pwd = Foosun.Common.Input.MD5(Foosun.Common.Input.Filter(Request.Form["newpwdBox1"].ToString()),true);
            if (inf.Update2(u_pwd, u_nmss) == 0)
            {
                PageError("对不起，找回密码失败", "getPassword.aspx");
            }
            else
            {
                //发送电子邮件开始

                PageRight("恭喜你!您找回密码成功。<li>一封电子邮件已经发送到您的邮箱！</li><li><a href=\"../login.aspx\"><font color=\"Blue\">现在登陆</font></a></li>", "login.aspx");
            }
        }
    }
}


