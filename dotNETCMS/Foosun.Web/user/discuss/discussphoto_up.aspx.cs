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

public partial class user_discuss_discussphoto_up : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    Photo pho = new Photo();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);
            sc.InnerHtml = Show_scs(DisIDs);
            string PhotoID = Foosun.Common.Input.Filter(Request.QueryString["PhotoID"].ToString());
            DataTable dt_photo = dis.sel_24(PhotoID);

            if (dt_photo.Rows[0]["UserNum"].ToString() != Foosun.Global.Current.UserNum)
            {
                PageError("这张图片不是你的你无权修改", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
            }

            DataTable dts1 = dis.sel_25(DisIDs);
            this.Photoalbum.DataSource = dts1;
            this.Photoalbum.DataTextField = "PhotoalbumName";
            this.Photoalbum.DataValueField = "PhotoalbumID";
            this.Photoalbum.DataBind();

            this.PhotoName.Text = dt_photo.Rows[0]["PhotoName"].ToString();
            this.PhotoContent.Text = dt_photo.Rows[0]["PhotoContent"].ToString();
            if (dis.sel_26(dt_photo.Rows[0]["PhotoalbumID"].ToString()) != "")
            {
                for (int s = 0; s < this.Photoalbum.Items.Count - 1; s++)
                {
                    if (this.Photoalbum.Items[s].Text == dis.sel_26(dt_photo.Rows[0]["PhotoalbumID"].ToString()))
                    {
                        this.Photoalbum.Items[s].Selected = true;
                    }
                }

            }
            no.InnerHtml = Show_no(dt_photo.Rows[0]["PhotoUrl"].ToString().Replace("{@UserdirFile}", Foosun.Config.UIConfig.dirDumm + "/" + Foosun.Config.UIConfig.UserdirFile));
        }
    }
    protected void server_Click(object sender, EventArgs e)
    {
        string DisIDs = Foosun.Common.Input.Filter(Request.QueryString["DisID"]);

        string PhotoIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoID"].ToString());
        string PhotoUrl1 = "";
        if (this.pic_p_1url.Value == "")
        {
            PhotoUrl1 = pho.sel_6(PhotoIDs);
        }

        else
        {
            PhotoUrl1 = pic_p_1url.Value;
        }
        string PhotoIDsa = Foosun.Common.Input.Filter(Request.QueryString["PhotoID"].ToString());
        string PhotoName = Foosun.Common.Input.Filter(Request.Form["PhotoName"].ToString());
        string PhotoalbumID = this.Photoalbum.SelectedValue;
        string PhotoContent = Foosun.Common.Input.Filter(Request.Form["PhotoContent"].ToString());

        if (PhotoIDsa == null) 
            PhotoIDsa = "";
        if (PhotoName == null) 
            PhotoName = "";
        if (PhotoalbumID == null) 
            PhotoalbumID = "";
        if (PhotoContent == null) 
            PhotoContent = "";

        DateTime PhotoTime = DateTime.Now;

        if (Photoalbum.SelectedValue != "")
        {
            if (pho.Update(PhotoName, PhotoTime, PhotoalbumID, PhotoContent, PhotoUrl1, PhotoIDs) == 0)
            {
                PageError("修改图片错误", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
            }    
            else
            {
                PageRight("修改图片成功", "discussPhotoalbumlist.aspx?DisID=" + DisIDs + "");
            }
        }
        PageError("修改图片错误图片分类不能为空", "");

    }
    string Show_no(string pURL)
    {
        string nos = "<td class=\"list_link\" ><img src=\"" + Foosun.Publish.CommonData.getUrl() + pURL + "\" width=\"90\" height=\"90\" id=\"pic_p_1\" /></td>";
        return nos;
    }
    string Show_scs(string DisID)
    {
        string scs = "<table width=\"100%\"  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"toptable\">";
        scs += "<tr><td height=\"1\" colspan=\"2\"></td></tr>";
        scs +=
        scs += "<tr><td width=\"57%\"  class=\"sysmain_navi\"  style=\"PADDING-LEFT: 14px\" >相册管理</td>";
        scs += "<td width=\"43%\"  class=\"topnavichar\"  style=\"PADDING-LEFT: 14px\" ><div align=\"left\">位置导航：<a href=\"../main.aspx\" target=\"sys_main\" class=\"list_link\">首页</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册管理</a><img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />修改图片</div></td></tr></table>";
        scs += "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"1\" class=\"Navitable\">";
        scs += "<tr> <td><span class=\"topnavichar\" style=\"PADDING-LEFT: 14px\"><a href=\"discussPhotoalbumlist.aspx?DisID=" + DisID + "\" class=\"menulist\">相册首页</a>　<a href=\"discussphoto_add.aspx?DisID=" + DisID + "\" class=\"menulist\">添加图片</a> &nbsp;&nbsp;<a href=\"discussphotoclass.aspx?DisID=" + DisID + "\" class=\"menulist\">相册分类</a> &nbsp;&nbsp; <a href=\"discussPhotoalbum.aspx?DisID=" + DisID + "\" class=\"menulist\">添加相册</a></td></tr></table>";
        return scs;
    }
}