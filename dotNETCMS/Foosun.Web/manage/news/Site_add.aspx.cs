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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Foosun.Model;

public partial class manage_news_Site_add : Foosun.Web.UI.ManagePage
{
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    public string PublishType = Foosun.Config.verConfig.PublicType;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";

        if (!Page.IsPostBack)
        {
            
            if (PublishType != "1")
            {
                N_13.Visible = false;
            }
            if (SiteID != "0") { PageError("您没有创建站群的权限!", ""); }
            this.LblID.Text = "";
            this.LblCID.Text = "";
            this.RadStatus.Items[0].Selected = true;
            this.RadFileType.Items[0].Selected = true;
            this.DdlDataTable.Items.Clear();
            Foosun.CMS.News news = new Foosun.CMS.News();
            DataTable tbnews = news.GetTables();
            this.DdlDataTable.DataSource = tbnews;
            this.DdlDataTable.DataTextField = "TableName";
            this.DdlDataTable.DataValueField = "TableName";
            this.DdlDataTable.DataBind();
            tbnews.Dispose();
            this.LblCaption.Text = this.LblNavigation.Text = "新建站群";
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(string.Empty))
            {
                int id = int.Parse(Request.QueryString["ID"]);
                this.TxtEnName.Enabled = false;
                this.LblID.Text = id.ToString();
                Foosun.CMS.Site site = new Foosun.CMS.Site();
                DataTable tb = site.GetSingle(id);
                if (tb != null && tb.Rows.Count > 0)
                {
                    DataRow r = tb.Rows[0];
                    if (!r.IsNull("CName")) this.TxtCnName.Text = r["CName"].ToString();
                    if (!r.IsNull("EName")) this.TxtEnName.Text = r["EName"].ToString();
                    if (!r.IsNull("ChannelID")) this.LblCID.Text = r["ChannelID"].ToString();
                    if (!r.IsNull("ChannCName")) this.TxtItemName.Text = r["ChannCName"].ToString();
                    if (!r.IsNull("isLock"))
                    {
                        int n = int.Parse(r["isLock"].ToString());
                        if (n == 1)
                            this.RadStatus.Items[1].Selected = true;
                        else if (n == 0)
                            this.RadStatus.Items[0].Selected = true;
                    }
                    if (!r.IsNull("IsURL"))
                    {
                        int n = int.Parse(r["IsURL"].ToString());
                        if (n == 1)
                            this.ChbType.Checked = true;
                        else if (n == 0)
                            this.ChbType.Checked = false;
                    }

                    if (!r.IsNull("Urladdress")) this.TxtUrl.Text = r["Urladdress"].ToString();
                    if (!r.IsNull("DataLib")) this.DdlDataTable.SelectedValue = r["DataLib"].ToString();
                    if (!r.IsNull("IndexTemplet")) this.TxtIndxTmp.Text = r["IndexTemplet"].ToString();
                    if (!r.IsNull("ClassTemplet")) this.TxtClsTmp.Text = r["ClassTemplet"].ToString();
                    if (!r.IsNull("ReadNewsTemplet")) this.TxtBrwTmp.Text = r["ReadNewsTemplet"].ToString();
                    if (!r.IsNull("SpecialTemplet")) this.TxtSpcTmp.Text = r["SpecialTemplet"].ToString();
                    if (!r.IsNull("Domain")) this.TxtDomain.Text = r["Domain"].ToString();
                    //权限空缺
                    if (!r.IsNull("isCheck"))
                    {
                        int n = int.Parse(r["isCheck"].ToString());
                        if (n == 1)
                            this.ChbAuditing.Checked = true;
                        else if (n == 0)
                            this.ChbAuditing.Checked = false;
                    }
                    if (!r.IsNull("Keywords")) this.TxtKeywords.Text = r["Keywords"].ToString();
                    if (!r.IsNull("Descript")) this.TxtDescribe.Text = r["Descript"].ToString();
                    if (!r.IsNull("ContrTF"))
                    {
                        int n = int.Parse(r["ContrTF"].ToString());
                        if (n == 1)
                            this.ChbContribute.Checked = true;
                        else if (n == 0)
                            this.ChbContribute.Checked = false;
                    }
                    if (!r.IsNull("ShowNaviTF"))
                    {
                        int n = int.Parse(r["ShowNaviTF"].ToString());
                        if (n == 1)
                            this.ChbShowNavi.Checked = true;
                        else if (n == 0)
                            this.ChbShowNavi.Checked = false;
                    }
                    if (!r.IsNull("UpfileType")) this.TxtUpFileType.Text = r["UpfileType"].ToString();
                    if (!r.IsNull("UpfileSize")) this.TxtUpFileSize.Text = r["UpfileSize"].ToString();
                    if (!r.IsNull("NaviContent")) this.TxtLead.Text = r["NaviContent"].ToString();
                    if (!r.IsNull("NaviPicURL")) this.TxtPic.Text = r["NaviPicURL"].ToString();
                    if (PublishType == "1")
                    {
                        if (!r.IsNull("SaveType"))
                        {
                            int n = int.Parse(r["SaveType"].ToString());
                            if (n == 0)
                                this.DdlType.Items[0].Selected = true;
                            else if (n == 1)
                                this.DdlType.Items[1].Selected = true;
                            else if (n == 2)
                                this.DdlType.Items[2].Selected = true;
                        }
                    }
                    if (!r.IsNull("PicSavePath")) this.TxtAccessories.Text = r["PicSavePath"].ToString();
                    if (!r.IsNull("SaveFileType"))
                    {
                        int n = int.Parse(r["SaveFileType"].ToString());
                        if (n == 1)
                            this.RadFileType.Items[1].Selected = true;
                        else if (n == 0)
                            this.RadFileType.Items[0].Selected = true;
                    }
                    if (!r.IsNull("SaveDirPath")) this.TxtFileDir.Text = r["SaveDirPath"].ToString();
                    if (!r.IsNull("SaveDirRule")) this.TxtDirRule.Text = r["SaveDirRule"].ToString();
                    if (!r.IsNull("SaveFileRule")) this.TxtFileRule.Text = r["SaveFileRule"].ToString();
                    if (!r.IsNull("NaviPosition")) this.TxtNaviPosition.Text = r["NaviPosition"].ToString();
                    if (!r.IsNull("IndexEXName")) this.DdlIndexEXName.SelectedValue = r["IndexEXName"].ToString();
                    if (!r.IsNull("ClassEXName")) this.DdlClassEXName.SelectedValue = r["ClassEXName"].ToString();
                    if (!r.IsNull("NewsEXName")) this.DdlNewsEXName.SelectedValue = r["NewsEXName"].ToString();
                    if (!r.IsNull("SpecialEXName")) this.DdlOtherEXName.SelectedValue = r["SpecialEXName"].ToString();
                    //if (!r.IsNull("classRefeshNum")) this.TxtClassRefresh.Text = r["classRefeshNum"].ToString();
                    //if (!r.IsNull("infoRefeshNum")) this.TxtInfoRefresh.Text = r["infoRefeshNum"].ToString();
                    //if (!r.IsNull("DelNum")) this.TxtDelNum.Text = r["DelNum"].ToString();
                    //if (!r.IsNull("SpecialNum")) this.TxtSpecialNum.Text = r["SpecialNum"].ToString();
                    #region 权限
                    if (!r.IsNull("isDelPoint")) this.UserPop1.AuthorityType = int.Parse(r["isDelPoint"].ToString());
                    if (!r.IsNull("Gpoint")) this.UserPop1.Gold = int.Parse(r["Gpoint"].ToString());
                    if (!r.IsNull("iPoint")) this.UserPop1.Point = int.Parse(r["iPoint"].ToString());
                    if (!r.IsNull("GroupNumber")) this.UserPop1.MemberGroup = r["GroupNumber"].ToString().Split('|');
                    #endregion 权限
                }
                else
                {
                    PageError("找不到站群!", "");
                }
                this.LblCaption.Text = this.LblNavigation.Text = "修改站群";
            }
            else
            {
                PageError("此版本不具备创建站群管理功能。", "javascript:history.back()", true);
                string _tmpSite = "";
                string _dirSite ="";
                if (SiteID != "0")
                {
                    _tmpSite = "/{@dirTemplet}/siteTemplets/" + SiteID;
                    _dirSite = "/{@dirFile}/siteTemplets/" + SiteID;
                    this.TxtIndxTmp.Text = _tmpSite + "/index.html";
                    this.TxtAccessories.Text = "/{@dirFile}/siteFiles/" + SiteID;
                }
                else 
                {
                    string _tmpindex = "";
                    string[] indexTempletfileARR = pd.indexTempletfile().Split('|');
                    _tmpindex = indexTempletfileARR[0];
                    this.TxtIndxTmp.Text = _tmpindex;
                    _tmpSite = "/{@dirTemplet}";
                    this.TxtAccessories.Text = "/{@dirFile}";
                }
                this.TxtClsTmp.Text = _tmpSite + "/Content/class.html";
                this.TxtBrwTmp.Text = _tmpSite + "/Content/news.html";
                this.TxtSpcTmp.Text = _tmpSite + "/Content/special.html";
                string[] upfileTypeARR = pd.upfileType().Split('|');
                this.TxtUpFileType.Text = upfileTypeARR[0];
                this.TxtUpFileSize.Text = upfileTypeARR[1];
                this.TxtFileDir.Text = "/" + Foosun.Config.UIConfig.dirSite;
                this.TxtDirRule.Text = "{@year04}-{@month}-{@day}";
                this.TxtFileRule .Text = "{@year04}{@month}-{@自动编号ID}";
            }
        }
    }

    protected void BtnOK_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            #region 为变量赋值
            STSite stsite;
            if (this.TxtCnName.Text.Trim().Equals(string.Empty))
                PageError("站群中文名称不能为空!", "");

            if (TxtEnName.Text.Trim().Equals(string.Empty))
                PageError("站群英文名称不能为空!", "");
            if (TxtItemName.Text.Trim().Equals(string.Empty))
                PageError("项目名称不能为空!", "");
            if (ChbType.Checked && TxtUrl.Text.Trim().Equals(""))
                PageError("外部站群地址不能为空", "");
            if (UserPop1.AuthorityType != 0 && UserPop1.MemberGroup == null)
                PageError("请选择权限组!", "");
            if (!ChbType.Checked)
            {
                if (TxtIndxTmp.Text.Trim() == "")
                    PageError("站群主页模板地址不能为空", "");
                if (TxtClsTmp.Text.Trim() == "")
                    PageError("站群栏目模板地址不能为空", "");
                if (TxtBrwTmp.Text.Trim() == "")
                    PageError("站群浏览模板地址不能为空", "");
                if (TxtSpcTmp.Text.Trim() == "")
                    PageError("站群专题模板地址不能为空", "");
                if (TxtUpFileType.Text.Trim() == "")
                    PageError("上传类型必须填写", "");
                if (TxtFileDir.Text.Trim() == "")
                    PageError("静态文件保存路径必须填写", "");
                if (TxtDirRule.Text.Trim() == "")
                    PageError("站群下新闻文件生成的目录结构必须填写", "");
                if (TxtFileRule.Text.Trim()=="")
                    PageError("站群下新闻文件命名规则必须填写", "");
            }
            stsite.ChannelID = this.LblCID.Text;
            stsite.CName = this.TxtCnName.Text;
            stsite.EName = this.TxtEnName.Text;
            stsite.ChannCName = this.TxtItemName.Text;
            stsite.isLock = 0;
            if (RadStatus.Items[1].Selected)
                stsite.isLock = 1;
            stsite.IsURL = 0;
            if (ChbType.Checked)
                stsite.IsURL = 1;
            stsite.ShowNaviTF = 0;
            if (ChbShowNavi.Checked)
                stsite.ShowNaviTF = 1;
            stsite.Urladdress = TxtUrl.Text;
            stsite.DataLib = DdlDataTable.SelectedValue;
            stsite.IndexTemplet = TxtIndxTmp.Text;
            stsite.ClassTemplet = TxtClsTmp.Text;
            stsite.ReadNewsTemplet = TxtBrwTmp.Text;
            stsite.SpecialTemplet = TxtSpcTmp.Text;
            stsite.Domain = TxtDomain.Text;

            #region 权限
            stsite.isDelPoint = UserPop1.AuthorityType;
            stsite.GroupNumber = "";
            stsite.Gpoint = 0;
            stsite.iPoint = 0;
            if (stsite.isDelPoint != 0)
            {
                stsite.Gpoint = UserPop1.Gold;
                stsite.iPoint = UserPop1.Point;
                string[] _mgroup = this.UserPop1.MemberGroup;
                string sGroupNumber = "";
                int j = 0;
                for (int i = 0; i < _mgroup.Length; i++)
                {
                    if (j > 0)
                        sGroupNumber += "|";
                    if (!_mgroup[i].Trim().Equals(""))
                    {
                        sGroupNumber += _mgroup[j];
                        j++;
                    }
                }
                if (j < 1)
                    PageError("请选择权限组!", "");
                stsite.GroupNumber = sGroupNumber;
            }
            #endregion 权限
            stsite.isCheck = 0;
            if (ChbAuditing.Checked)
                stsite.isCheck = 1;
            stsite.Keywords = TxtKeywords.Text;
            stsite.Descript = TxtDescribe.Text;
            stsite.ContrTF = 0;
            if (ChbContribute.Checked)
                stsite.ContrTF = 1;
            stsite.UpfileType = TxtUpFileType.Text.Trim();
            stsite.UpfileSize = 10240;
            if (!TxtUpFileSize.Text.Trim().Equals(string.Empty))
                stsite.UpfileSize = int.Parse(TxtUpFileSize.Text);
            stsite.NaviContent = TxtLead.Text.Trim();
            stsite.NaviPicURL = TxtPic.Text.Trim();
            stsite.SaveType = 0;
            if (PublishType == "1")
            {
                if (DdlType.Items[1].Selected)
                    stsite.SaveType = 1;
                else if (DdlType.Items[2].Selected)
                    stsite.SaveType = 2;
            }
            stsite.PicSavePath = TxtAccessories.Text.Trim();
            stsite.SaveFileType = 0;
            if (RadFileType.Items[1].Selected)
                stsite.SaveFileType = 1;
            stsite.SaveDirPath = TxtFileDir.Text.Trim();
            stsite.SaveDirRule = TxtDirRule.Text.Trim();
            stsite.SaveFileRule = TxtFileRule.Text.Trim();
            stsite.NaviPosition = TxtNaviPosition.Text.Trim();
            stsite.IndexEXName = DdlIndexEXName.SelectedValue;
            stsite.ClassEXName = DdlClassEXName.SelectedValue;
            stsite.NewsEXName = DdlNewsEXName.SelectedValue;
            stsite.SpecialEXName = DdlOtherEXName.SelectedValue;
            stsite.classRefeshNum = 800;
            stsite.infoRefeshNum = 100;
            stsite.DelNum = 200;
            stsite.SpecialNum = 500;
            #endregion 为变量赋值

            Foosun.CMS.Site site = new Foosun.CMS.Site();
            if (LblID.Text.Trim().Equals(string.Empty))
            {
                PageError("此版本不具备创建站群管理功能。", "javascript:history.back()", true);
                int nsite = site.Add(stsite);
                this.LblID.Text = nsite.ToString();
                PageRight("站群添加成功!", "site_list.aspx");
            }
            else
            {
                int id = int.Parse(this.LblID.Text);
                site.Update(id, stsite);
                PageRight("站群修改成功", "site_list.aspx");
            }
        }
    }
}
