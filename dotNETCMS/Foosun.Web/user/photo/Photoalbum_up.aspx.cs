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
using System.IO;

public partial class user_Photoalbum_up : Foosun.Web.UI.UserPage
{
    Photo pho = new Photo();
    public string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        
        DataTable dts = pho.sel_7(Foosun.Global.Current.UserNum);
        this.Photoalbum.DataSource = dts;
        this.Photoalbum.DataTextField = "ClassName";
        this.Photoalbum.DataValueField = "ClassID";
        this.Photoalbum.DataBind();

        string PhotoalbumID = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());
        
        DataTable dt_sel = pho.sel_8(PhotoalbumID);
        PhotoalbumName.Text=dt_sel.Rows[0]["PhotoalbumName"].ToString();
        string[] pj=dt_sel.Rows[0]["PhotoalbumJurisdiction"].ToString().Split('|');
        if(pj[0]=="0")
        {
            this.Radio1.Checked=true;
        }
        else        
        {
            this.Radio2.Checked=true;
        }
        this.number.Text=pj[1];

        if (pho.sel_9(dt_sel.Rows[0]["ClassID"].ToString()) != null)
        {
            string cm = pho.sel_9(dt_sel.Rows[0]["ClassID"].ToString());//职业
            for (int r = 0; r < this.Photoalbum.Items.Count - 1; r++)
            {
                if (this.Photoalbum.Items[r].Text == cm)
                {
                    this.Photoalbum.Items[r].Selected = true;
                }
            }
        }

        if (dt_sel.Rows[0]["pwd"].ToString() != "")
        {
            uppwd.InnerHtml=show_np();
        }
        else
        {
            uppwd.InnerHtml=show_nps();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
        DateTime Creatime = DateTime.Now;
        string PhotoalbumIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());
        string PhotoalbumName = Foosun.Common.Input.Filter(Request.Form["PhotoalbumName"].ToString());

        string ClassID = this.Photoalbum.SelectedValue.ToString();
        string Jurisdiction1="";
        string Jurisdiction2="";
        string PhotoalbumJurisdiction="";
        if(this.Radio1.Checked)
        {
             Jurisdiction1="0";
             Jurisdiction2=this.number.Text;
            PhotoalbumJurisdiction=Jurisdiction1+"|"+Jurisdiction2;
        }
        else
        {
            Jurisdiction1="1";
            Jurisdiction2="0";
            PhotoalbumJurisdiction=Jurisdiction1+"|"+Jurisdiction2;
        }
        if (pho.Update_1(PhotoalbumName, PhotoalbumJurisdiction, ClassID, Creatime, PhotoalbumIDs) != 0)
            {
                PageRight("修改成功", "Photoalbumlist.aspx");
            }
            else
            {
                PageError("修改失败", "Photoalbumlist.aspx");
            }
        }
    }

   string show_np()
   {
    string np="<a href=\"javascript:uppwd()\" class=\"list_link\">修改密码</a>";
    return np;
   }
   string show_nps()
   {
       string np = "<a href=\"javascript:addpwd()\" class=\"list_link\">创建密码</a>";
    return np;
   }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (this.oldpwd.Text == "")
        {
            PageError("旧密码不能为空", "Photoalbumlist.aspx");
        }
        if (this.newpwd.Text == "")
        {
            PageError("新密码不能为空", "Photoalbumlist.aspx");
        }
        if (this.newpwds.Text == "")
        {
            PageError("确认密码不能为空", "Photoalbumlist.aspx");
        }
        if (this.newpwd.Text != this.newpwds.Text)
        {
            PageError("两次密码不一致", "Photoalbumlist.aspx");
        }
        string oldpwds = "";
        string newpwds = "";
        string PhotoalbumIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());
        if (Request.Form["oldpwd"].ToString() != "")
        {
            oldpwds = Foosun.Common.Input.MD5(Foosun.Common.Input.Filter(Request.Form["oldpwd"].ToString()), true);
        }
        newpwds = Foosun.Common.Input.MD5(Foosun.Common.Input.Filter(Request.Form["newpwd"].ToString()), true);      
        if (oldpwds != pho.sel_10())
        {
            if (pho.Update_2(newpwds, PhotoalbumIDs) != 0)
            {
                PageRight("修改成功", "Photoalbumlist.aspx");
            }
            else
            {
                PageError("修改失败", "Photoalbumlist.aspx");
            }
        }
        else 
        {
            PageError("修改失败,你输入的原先的密码不正确", "Photoalbumlist.aspx");
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if(this.pwd.Text=="")
        {
            PageError("新密码不能为空", "Photoalbumlist.aspx");
        }
        if (this.pwds.Text == "")
        {
            PageError("确认密码不能为空", "Photoalbumlist.aspx");
        }
        if (this.pwd.Text != this.pwds.Text)
        {
            PageError("两次密码不一致", "Photoalbumlist.aspx");
        }

        string PhotoalbumIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());
        string newpwds = "";
        if (Request.Form["pwd"].ToString() != "")
        {
            newpwds = Foosun.Common.Input.MD5(Foosun.Common.Input.Filter(Request.Form["pwd"].ToString()), true);
        }
        if (pho.Update_2(newpwds, PhotoalbumIDs) != 0)
        {
            PageRight("修改成功", "Photoalbumlist.aspx");
        }
        else
        {
            PageError("修改失败", "Photoalbumlist.aspx");
        }
    }
}
