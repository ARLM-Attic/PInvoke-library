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

public partial class user_discussPhotoalbum_up : Foosun.Web.UI.UserPage
{
    public string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    Discuss dis = new Discuss();
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
        
        string PhotoalbumIDsa = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());

        if (dis.sel_28(PhotoalbumIDsa) != Foosun.Global.Current.UserNum)
        {
            PageError("此相册不是你建立的你无权修改", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }

        if (!IsPostBack)
        {
            DataTable dts = dis.sel_27(DisIDs);
            this.Photoalbum.DataSource = dts;
            this.Photoalbum.DataTextField = "ClassName";
            this.Photoalbum.DataValueField = "ClassID";
            this.Photoalbum.DataBind();

            string PhotoalbumID = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());

            DataTable dt_sel = pho.sel_8(PhotoalbumID);
            PhotoalbumName.Text = dt_sel.Rows[0]["PhotoalbumName"].ToString();
            string[] pj = dt_sel.Rows[0]["PhotoalbumJurisdiction"].ToString().Split('|');
            if (pj[0] == "0")
            {
                this.Radio1.Checked = true;
            }
            else
            {
                this.Radio2.Checked = true;
            }
            this.number.Text = pj[1];
            if (pho.sel_9(dt_sel.Rows[0]["ClassID"].ToString()) != null)
            {
                string cm = pho.sel_9(dt_sel.Rows[0]["ClassID"].ToString());
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
                sc.InnerHtml = Show_sc(DisIDs);
            }
            else
            {
                sc.InnerHtml = Show_scs(DisIDs);
            }
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime Creatime = DateTime.Now;
            string PhotoalbumIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());
            string PhotoalbumName = Foosun.Common.Input.Filter(Request.Form["PhotoalbumName"].ToString());
            string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
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
                PageRight("修改成功", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
            }
            else
            {
                PageError("修改失败<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());

        if (this.oldpwd.Text == "")
        {
            PageError("旧密码不能为空<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
        if (this.newpwd.Text == "")
        {
            PageError("新密码不能为空<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
        if (this.newpwds.Text == "")
        {
            PageError("确认密码不能为空<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
        if (this.newpwd.Text != this.newpwds.Text)
        {
            PageError("两次密码不一致<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
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
                PageRight("修改成功", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
            }
            else
            {
                PageError("修改失败<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
            }
        }
        else 
        {
            PageError("修改失败,你输入的原先的密码不正确<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());

        if(this.pwd.Text=="")
        {
            PageError("新密码不能为空<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
        if (this.pwds.Text == "")
        {
            PageError("确认密码不能为空<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
        if (this.pwd.Text != this.pwds.Text)
        {
            PageError("两次密码不一致<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }

        string PhotoalbumIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoalbumID"].ToString());
        string newpwds = "";
        if (Request.Form["pwd"].ToString() != "")
        {
           newpwds = Foosun.Common.Input.MD5(Foosun.Common.Input.Filter(Request.Form["pwd"].ToString()), true);
        }
        if (pho.Update_2(newpwds, PhotoalbumIDs) != 0)
        {
            PageRight("修改成功", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
        else
        {
            PageError("修改失败<br>", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
    }

    string Show_sc(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs +=
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册管理</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a> &nbsp;&nbsp;<a href=\"#\" OnClick=\"upMaterial()\" class=\"list_link\">修改相册资料</a>&nbsp;&nbsp;<a href=\"?DisID=" + DisID + "\" OnClick=\"uppwd()\" class=\"list_link\">修改密码</a></td></tr></table>";
        return scs;
    }
    string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs +=
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />相册管理</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a> &nbsp;&nbsp;<a href=\"#\" OnClick=\"upMaterial()\" class=\"list_link\">修改相册资料</a>&nbsp;&nbsp;<a href=\"?DisID=" + DisID + "\" OnClick=\"addpwd()\" class=\"list_link\">创建密码</a></td></tr></table>";
        return scs;
    }
}
