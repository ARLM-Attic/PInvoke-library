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

public partial class user_Constr_up : Foosun.Web.UI.UserPage
{
    /// <summary>
    /// 初始化信息
    /// </summary>
    #region 初始化信息
    Constr con = new Constr();
    Site st = new Site();
    public string ConstrTF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
         Response.CacheControl = "no-cache";
         ConstrTF = con.ConstrTF();
         string _ConID = Request.QueryString["ConID"];
         if (_ConID == "" && _ConID == null){PageError("参数错误", "");}
         string ConID = Foosun.Common.Input.Filter(_ConID.ToString());
         if (!IsPostBack)
         {
             DataTable dt = con.sel1(ConID);
             DataRow dr = dt.Rows[0];    
             int cut = dt.Rows.Count;
             if (cut == 0)
             {
                 PageError("参数错误", "");
             }
             if (dr["isCheck"].ToString() == "1")
             {
                 PageError("稿件已经通过审核不能在修改", "");
             }
             else
             {
                 //加载频道
                 ContentManage rd = new ContentManage();
                 DataTable SiteTB = rd.getSiteList();
                 if (SiteTB != null)
                 {
                     this.site.DataSource = SiteTB;
                     this.site.DataTextField = "CName";
                     this.site.DataValueField = "ChannelID";
                     this.site.DataBind();
                 }

                 //加载稿件分类
                 DataTable tb1 = con.selConstrClass(Foosun.Global.Current.UserNum);
                 this.ConstrClass.DataSource = tb1;
                 this.ConstrClass.DataTextField = "cName";
                 this.ConstrClass.DataValueField = "Ccid";
                 this.ConstrClass.DataBind();
                 string u_ClassID = dr["ClassID"].ToString();
                 ConstrClass.Text = u_ClassID;
                 string selcNames = con.selcName(u_ClassID);
                 for (int s = 0; s < this.ConstrClass.Items.Count - 1; s++)
                 {
                     if (this.ConstrClass.Items[s].Text == selcNames)
                     {
                         this.ConstrClass.Items[s].Selected = true;
                     }
                 }

                 string u_SiteID = dr["SiteID"].ToString();
                 for (int s = 0; s < this.site.Items.Count - 1; s++)
                 {
                     if (this.site.Items[s].Value == u_SiteID){ this.site.Items[s].Selected = true;}
                 }

                 string Sourceaa = dr["Source"].ToString();
                 for (int s = 0; s < this.lxList1.Items.Count - 1; s++)
                 {
                     if (this.lxList1.Items[s].Text == Sourceaa){this.lxList1.Items[s].Selected = true;}
                 }
                 this.photo.Text = dr["PicURL"].ToString();
                 Contentbox.Value = dr["Content"].ToString();
                 this.Title.Text = dr["Title"].ToString();
                 this.Author.Text = dr["Author"].ToString();
                 this.Tags.Text = dr["Tags"].ToString();
                 string[] Tagsp = dr["Contrflg"].ToString().Split('|');
                 int tags1 = int.Parse(Tagsp[0].ToString());
                 int tags2 = int.Parse(Tagsp[1].ToString());
                 int tags3 = int.Parse(Tagsp[2].ToString());
                 int tags4 = int.Parse(Tagsp[3].ToString());
                 if (tags1 == 0){this.inList1.Items[0].Selected = true;}
                 else if (tags1 == 1){this.inList1.Items[1].Selected = true;}
                 else{this.inList1.Items[2].Selected = true;}
                 if (tags2 == 1){ fbList1.Items[0].Selected = true;}
                 else{fbList1.Items[1].Selected = true;}
                 if (tags3 == 1){Locking.Items[0].Selected = true;}
                 else{Locking.Items[1].Selected = true;}
                 if (tags4 == 1){Recommendation.Items[0].Selected = true;}
                 else{Recommendation.Items[1].Selected = true;}
             }
         }
     }
    #endregion
    /// <summary>
    /// 修改信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region 修改信息
     protected void  Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string ConIDs = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
            string Contents = Foosun.Common.Input.Htmls(Contentbox.Value);
            string ClassID = this.ConstrClass.SelectedValue.ToString();
            string Title = Foosun.Common.Input.Htmls(Request.Form["Title"].ToString());
            string Author = Foosun.Common.Input.Htmls(Request.Form["Author"].ToString());
            string SiteID = this.site.SelectedValue.ToString();
            string Source = this.lxList1.SelectedValue.ToString();
            string Contrflg = "";
            Contrflg = this.inList1.SelectedValue.ToString() + "|" + fbList1.SelectedValue.ToString() + "|" + Locking.SelectedValue.ToString() + "|" + Recommendation.SelectedValue.ToString();
            string PicURL = Foosun.Common.Input.Htmls(Request.Form["photo"].ToString());
            string Tags = this.Tags.Text;
            Foosun.Model.STConstr stcn;
            stcn.Content = Contents;
            stcn.ClassID = ClassID;
            stcn.Title = Title;
            stcn.Source = Source;
            stcn.Tags = Tags;
            stcn.Contrflg = Contrflg;
            stcn.Author = Author;
            stcn.PicURL = PicURL;
            stcn.SiteID = SiteID;
            stcn.UserNum = Foosun.Global.Current.UserNum;
            if (con.Update(stcn, ConIDs) == 0)
            {
                PageError("更新错误", "Constrlist.aspx");
            }
            else
            {
                PageRight("更新成功", "Constrlist.aspx");
            }
        }
    }
    #endregion
}