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

public partial class user_photo_up : Foosun.Web.UI.UserPage
{
    Photo pho = new Photo();
    public string dirPic = Foosun.Config.UIConfig.UserdirFile;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Foosun.Config.UIConfig.dirDumm.Trim() != "")
            {
                dirPic = Foosun.Config.UIConfig.dirDumm + "/" + Foosun.Config.UIConfig.UserdirFile;
            }
            string UserNum = Foosun.Global.Current.UserNum;
            string PhotoID = Foosun.Common.Input.Filter(Request.QueryString["PhotoID"].ToString());
            DataTable dt_photo = pho.sel_4(PhotoID);
            DataTable dts1 = pho.sel_2(UserNum); ;
            this.Photoalbum.DataSource = dts1;
            this.Photoalbum.DataTextField = "PhotoalbumName";
            this.Photoalbum.DataValueField = "PhotoalbumID";
            this.Photoalbum.DataBind();
           
            this.PhotoName.Text = dt_photo.Rows[0]["PhotoName"].ToString();
            this.PhotoContent.Text = dt_photo.Rows[0]["PhotoContent"].ToString();
            if (pho.sel_5(dt_photo.Rows[0]["PhotoalbumID"].ToString()) != "")
            {
                for (int s = 0; s < this.Photoalbum.Items.Count; s++)
                {
                    if (this.Photoalbum.Items[s].Text == pho.sel_5(dt_photo.Rows[0]["PhotoalbumID"].ToString()))
                    {
                        this.Photoalbum.Items[s].Selected = true;
                    }
                }

            }
            pic_p_1url.Value = dt_photo.Rows[0]["PhotoUrl"].ToString();
            no.InnerHtml = Show_no(dt_photo.Rows[0]["PhotoUrl"].ToString().Replace("{@userdirfile}", dirPic));
        }
    }
    protected void server_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string PhotoIDs = Foosun.Common.Input.Filter(Request.QueryString["PhotoID"].ToString());

            string PhotoUrl1 = "";
            PhotoUrl1 = this.pic_p_1url.Value;
            string PhotoName = Foosun.Common.Input.Filter(Request.Form["PhotoName"].ToString());
            string PhotoalbumID = this.Photoalbum.SelectedValue;
            string PhotoContent = Foosun.Common.Input.Filter(Request.Form["PhotoContent"].ToString());

            DateTime PhotoTime = DateTime.Now;

            if (Photoalbum.SelectedValue != "")
            {
                if (PhotoName == null)
                    PhotoName = "";
                if (PhotoContent == null)
                    PhotoContent = "";
                if (PhotoUrl1 == null)
                    PhotoUrl1 = "";

                if (pho.Update(PhotoName, PhotoTime, PhotoalbumID, PhotoContent, PhotoUrl1, PhotoIDs) == 0)
                {
                    PageError("修改图片错误<br>", "Photoalbumlist.aspx");
                }
                else
                {
                    PageRight("修改图片成功!<br>", "Photoalbumlist.aspx");
                }
            }
            PageError("修改图片错误图片分类不能为空<br>", "");
        }
    }
    string Show_no(string pURL)
    {
        string nos = "<td class=\"list_link\" ><img src=\"" + pURL + "\" width=\"90\" height=\"90\" id=\"pic_p_1\" /></td>";
        return nos;
    }
}