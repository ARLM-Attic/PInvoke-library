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

public partial class user_discuss_discussphoto_add : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    Photo pho = new Photo();
    public string dimm = Foosun.Config.UIConfig.dirDumm;
    public string UserdirFile = Foosun.Config.UIConfig.UserdirFile;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            if (dimm.Trim() != string.Empty)
            {
                dimm = dimm + "/";
            }
            string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
            DataTable dts1 = dis.sel_23(DisIDs);
            this.DropDownList1.DataSource = dts1;
            this.DropDownList1.DataTextField = "PhotoalbumName";
            this.DropDownList1.DataValueField = "PhotoalbumID";
            this.DropDownList1.DataBind();
            sc.InnerHtml = Show_scs(DisIDs);
        }
    }
    protected void server_Click(object sender, EventArgs e)
    {
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);

        string PhotoUrl1 = Foosun.Common.Input.Htmls(this.pic_p_1url.Text);
        string PhotoUrl2 = Foosun.Common.Input.Htmls(this.pic_p_1ur2.Text);
        string PhotoUrl3 = Foosun.Common.Input.Htmls(this.pic_p_1ur3.Text);
        bool flg = false;
        if (PhotoUrl1 != String.Empty && PhotoUrl1 != null)
        {
            flg = add_phtoto(PhotoUrl1);
        }

        if (PhotoUrl2 != String.Empty && PhotoUrl2 != null)
        {
            flg = add_phtoto(PhotoUrl2);
        }

        if (PhotoUrl3 != String.Empty && PhotoUrl3 != null)
        {
            flg = add_phtoto(PhotoUrl3);
        }

        if (!flg)
        {
            PageError("添加图片错误", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
        else
        {
            PageRight("添加图片成功", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
        }
    }
    protected bool add_phtoto(string PValue)
    {
        string PhotoName = Request.Form["TextBox1"].ToString();

        string PhotoalbumID = this.DropDownList1.SelectedValue;

        string PhotoContent = Request.Form["TextBox2"].ToString();

        System.Threading.Thread.Sleep(10);
        string PhotoID = Foosun.Common.Rand.Number(12);

        string UserNum = Foosun.Global.Current.UserNum;

        Foosun.Model.STPhoto ph = new Foosun.Model.STPhoto();
        ph.PhotoContent = PhotoContent;
        ph.PhotoName = PhotoName;

        DataTable dID = pho.sel_3();
        int cut = dID.Rows.Count;
        string pID = "";
        if (cut > 0)
        {
            pID = dID.Rows[0]["PhotoID"].ToString();
        }
        if (DropDownList1.SelectedValue != "")
        {
            if (pID != PhotoID)
            {
                if (pho.Add(ph, UserNum, PhotoalbumID, PValue, PhotoID) == 0)
                    return false;
                return true;
            }
            return false;
        }
        return false;
    }

    string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs +=
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />添加图片</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a></td></tr></table>";
        return scs;
    }
}