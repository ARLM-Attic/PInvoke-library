//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==            Code By ZhenJiang.Wang                     == 
//===========================================================
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

public partial class user_Constr : Foosun.Web.UI.UserPage
{
    Constr con = new Constr();
    Site st = new Site();
    public string ConstrTF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            
            Response.CacheControl = "no-cache";
            ConstrTF = con.ConstrTF();
            string UM = Foosun.Global.Current.UserNum;
            if (con.selGroupNumber(UM)==1)
            {
                PageRight("对不起.你所投稿的数目已经大于你所能投稿的数目!<li>请联系管理员</li>", "");
            }
            ContentManage rd = new ContentManage();
            this.Author.Text = Foosun.Global.Current.UserName;
            DataTable SiteTB = rd.getSiteList();            
            if (SiteTB != null)
            {
                this.site.DataSource = SiteTB;
                this.site.DataTextField = "CName";
                this.site.DataValueField = "ChannelID";
                this.site.DataBind();
            }

            DataTable tb1 = con.selConstrClass(UM);
            this.ConstrClass.DataSource = tb1;
            this.ConstrClass.DataTextField = "cName";
            this.ConstrClass.DataValueField = "Ccid";
            this.ConstrClass.DataBind();

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserNum = Foosun.Global.Current.UserNum;
            string Contents = Foosun.Common.Input.Htmls(ContentBox.Value);
            string ClassID = this.ConstrClass.SelectedValue.ToString();
            string Title = Foosun.Common.Input.Htmls(Request.Form["Title"].ToString());
            string Author = "";
            if (Request.Form["Author"].ToString() == "")
            {
                Author = Foosun.Global.Current.UserName;
            }
            else
            {
                Author = Foosun.Common.Input.Htmls(Request.Form["Author"].ToString());
            }
            string SiteID = this.site.SelectedValue.ToString();
            string Source = this.lxList1.SelectedValue.ToString();
            string Contrflg = "";
            Contrflg = this.inList1.SelectedValue.ToString() + "|" + fbList1.SelectedValue.ToString() + "|" + Locking.SelectedValue.ToString() + "|" + Recommendation.SelectedValue.ToString();
            string Tags = this.Tags.Text;
            string PicURL = Foosun.Common.Input.Htmls(Request.Form["photo"].ToString());
            PicURL = photo.Text.Trim();
            Foosun.Model.STConstr stcn;
            stcn.Content = Contents;
            stcn.ClassID = ClassID;
            stcn.Title = Title;
            stcn.Source = Source;
            stcn.Tags = Tags;
            stcn.Contrflg = Contrflg;
            stcn.Author = Author;
            stcn.PicURL = PicURL;
            stcn.SiteID = Foosun.Global.Current.SiteID;
            stcn.UserNum = UserNum;
            if (con.Add(stcn)!=0)
            {
                PageRight("添加成功", "Constrlist.aspx");
            }
            else
            {
                PageError("添加失败", "Constrlist.aspx");
            }
        }
    }
}