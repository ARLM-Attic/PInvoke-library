//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By JiangDong                       ==
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
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

public partial class manage_collect_Collect_StepFour : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.EdtCaption.SetTextAreaRows = this.EdtAuthor.SetTextAreaRows = this.EdtPageOther.SetTextAreaRows = this.EdtPageRule.SetTextAreaRows = 3;
            this.EdtSource.SetTextAreaRows = this.EdtTime.SetTextAreaRows = 3;
            this.EdtContent.SetTextAreaRows = 5;
            this.EdtCaption.SetTag = new string[] { "[标题]", "[变量]" };
            this.EdtContent.SetTag = new string[] { "[内容]", "[变量]" };
            this.EdtAuthor.SetTag = new string[] { "[作者]", "[变量]" };
            this.EdtSource.SetTag = new string[] { "[来源]", "[变量]" };
            this.EdtTime.SetTag = new string[] { "[加入时间]", "[变量]" };
            this.EdtPageOther.SetTag = new string[] { "[分页新闻]"};
            this.EdtPageRule.SetTag = new string[] { "[分页新闻]", "[变量]" };
            this.EdtAuthor.SetMaxLength = 4000;
            this.EdtCaption.SetMaxLength = 4000;
            this.EdtContent.SetMaxLength = 4000;
            this.EdtPageOther.SetMaxLength = 4000;
            this.EdtPageRule.SetMaxLength = 4000;
            this.EdtSource.SetMaxLength = 4000;
            this.EdtTime.SetMaxLength = 4000;
            if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim().Equals(""))
            {
                PageError("没有传送必要的参数", "Collect_List.aspx");
            }
            else
            {
                int n = int.Parse(Request.QueryString["ID"]);
                Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
                DataTable tb = cl.GetSite(n);
                if (tb == null || tb.Rows.Count == 0)
                {
                    PageError("传送的参数无效,没有找到该站点的记录!", "Collect_List.aspx");
                }
                DataRow r = tb.Rows[0];
                this.HidSiteID.Value = n.ToString();
                if (!r.IsNull("HandSetAuthor"))
                {
                    this.TxtAuthor.Text = tb.Rows[0]["HandSetAuthor"].ToString();
                    this.ChbAuthor.Checked = true;
                }
                if (!r.IsNull("HandSetSource"))
                {
                    this.TxtSource.Text = r["HandSetSource"].ToString();
                    this.ChbSource.Checked = true;
                }
                if (!r.IsNull("HandSetAddDate"))
                {
                    this.TxtTime.Text = r["HandSetAddDate"].ToString();
                    this.ChbTime.Checked = true;
                }
                if (!r.IsNull("PageTitleSetting")) this.EdtCaption.Text = r["PageTitleSetting"].ToString();
                if (!r.IsNull("PagebodySetting")) this.EdtContent.Text = r["PagebodySetting"].ToString();
                if (!r.IsNull("AuthorSetting"))
                {
                    this.EdtAuthor.Text = r["AuthorSetting"].ToString();
                    this.ChbAuthor.Checked = false;
                }
                if (!r.IsNull("SourceSetting"))
                {
                    this.EdtSource.Text = r["SourceSetting"].ToString();
                    this.ChbSource.Checked = false;

                }
                if (!r.IsNull("AddDateSetting"))
                {
                    this.EdtTime.Text = r["AddDateSetting"].ToString();
                    this.ChbTime.Checked = false;
                }

                if (!r.IsNull("OtherNewsType"))
                {
                    switch (int.Parse(r["OtherNewsType"].ToString()))
                    {
                        case 1:
                            this.Tr_PageCode.Style.Add("display", "none");
                            this.RadPageOther.Checked = true;
                            this.EdtPageOther.Text = r["OtherNewsPageSetting"].ToString();
                            break;
                        case 2:
                            this.Tr_PageOther.Style.Add("display", "none");
                            this.RadPageCode.Checked = true;
                            this.EdtPageRule.Text = r["OtherNewsPageSetting"].ToString();
                            break;
                        default:
                            this.RadPageNone.Checked = true;
                            this.Tr_PageOther.Style.Add("display", "none");
                            this.Tr_PageCode.Style.Add("display", "none");
                            break;
                    }
                }
                tb.Dispose();
                HttpCookie ck = Request.Cookies["CollectObtainURL"];
                if (ck != null && !ck.Value.Trim().Equals(""))
                {
                    string urls = ck.Value;
                    string[] _url = Regex.Split(urls, @"\*\*\*");
                    foreach (string s in _url)
                    {
                        if (!s.Equals(""))
                            this.DdlObtUrl.Items.Add(new ListItem(s, s));
                    }
                }
                Response.Cookies.Remove("CollectObtainURL");
            }
        }
    }
    protected void BtnNext_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (this.EdtCaption.Text.IndexOf("[标题]") < 0)
            {
                PageError("新闻标题没有设置或设置不正确！", "Collect_List.aspx");
            }
            if (this.EdtContent.Text.IndexOf("[内容]") < 0)
            {
                PageError("新闻内容没有设置或设置不正确！", "Collect_List.aspx");
            }
            Foosun.Model.CollectSiteInfo nf = new Foosun.Model.CollectSiteInfo();
            nf.ID = Convert.ToInt32(this.HidSiteID.Value);
            nf.PageTitleSetting = EdtCaption.Text.Trim();
            nf.PagebodySetting = EdtContent.Text.Trim();
            if (this.ChbAuthor.Checked)
            {
                if (this.TxtAuthor.Text.Trim().Equals(""))
                {
                    PageError("作者没有设置,请设置新闻作者(手动)!", "Collect_List.aspx");
                }
                else
                {
                    nf.HandSetAuthor = this.TxtAuthor.Text.Trim();
                    nf.AuthorSetting = "";
                }
            }
            else
            {
                if (this.EdtAuthor.Text.Trim().Equals(""))
                {
                    PageError("作者没有设置,请设置作者(自动匹配项)!", "Collect_List.aspx");
                }
                else
                {
                    nf.AuthorSetting = this.EdtAuthor.Text.Trim();
                    nf.HandSetAuthor = "";
                }
            }
            if (this.ChbSource.Checked)
            {
                if (this.TxtSource.Text.Trim().Equals(""))
                {
                    PageError("来源没有设置,请设置新闻来源(手动)!", "Collect_List.aspx");
                }
                else
                {
                    nf.HandSetSource = this.TxtSource.Text.Trim();
                    nf.SourceSetting = "";
                }
            }
            else
            {
                if (this.EdtSource.Text.Trim().Equals(""))
                {
                    PageError("来源没有设置,请设置新闻来源(自动匹配项)!", "Collect_List.aspx");
                }
                else
                {
                    nf.SourceSetting = this.EdtSource.Text.Trim();
                    nf.HandSetSource = "";
                }
            }
            if (this.ChbTime.Checked)
            {
                if (this.TxtTime.Text.Trim().Equals(""))
                {
                    PageError("时间没有设置,请设置新闻时间(手动)!", "Collect_List.aspx");
                }
                else
                {
                    try
                    {
                        nf.HandSetAddDate = Convert.ToDateTime(this.TxtTime.Text.Trim());
                    }
                    catch(FormatException fex)
                    {
                        PageError("手动时间格式错误!正确的格式如:2007-12-12 15:16:35.系统错误" + fex.ToString()+ "", "Collect_List.aspx");
                    }
                    nf.AddDateSetting = "";
                }
            }
            else
            {
                if (this.EdtTime.Text.Trim().Equals(""))
                {
                    PageError("时间没有设置,请设置新闻时间(自动匹配项)!", "Collect_List.aspx");
                }
                else
                {
                    nf.AddDateSetting = this.EdtTime.Text.Trim();
                }
            }
            nf.OtherNewsType = 0;
            if (this.RadPageOther.Checked)
            {
                nf.OtherNewsType = 1;
                nf.OtherNewsPageSetting = EdtPageOther.Text.Trim();
                if (nf.OtherNewsPageSetting.IndexOf("[分页新闻]") < 0)
                {
                    PageError("您已选择了分页，请设置分页新闻!", "Collect_List.aspx");
                    return;
                }
            }
            else if (this.RadPageCode.Checked)
            {
                nf.OtherNewsType = 2;
                nf.OtherNewsPageSetting = EdtPageRule.Text.Trim();
                if (nf.OtherNewsPageSetting.IndexOf("[分页新闻]") < 0)
                {
                    PageError("您已选择了分页，请设置分页新闻!", "Collect_List.aspx");
                    return;
                }
            }
            Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
            cl.SiteUpdate(nf, 4);
            //bug修改  周峻平  2008-6-4
            //下一步DdlObtUrl
            string showURLContent = this.DdlObtUrl.Items[0].Text;

            Response.Redirect("Collect_StepFive.aspx?showURLContent=" + showURLContent + "&showForderID=" + this.HidSiteID.Value);
        }
    }
}
