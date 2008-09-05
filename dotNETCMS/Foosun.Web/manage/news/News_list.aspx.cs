//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                        Forum:bbs.foosun.net                     ==
//==                       WebSite:www.foosun.net                    ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                     Tel:86-28-85098980/66026180                 ==
//==                     QQ:655071,MSN:ikoolls@gmail.com             ==
//==                     Email:Service@foosun.cn                     ==
//==                       Code By Simplt.Xie                        ==
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
using Foosun.CMS.Common;

public partial class manage_news_News_list : Foosun.Web.UI.ManagePage
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public manage_news_News_list()
    {
        Authority_Code = "C000";
    }

    ContentManage rd = new ContentManage();
    rootPublic pd = new rootPublic();
    UserMisc rds = new UserMisc();
    public string DPre = Foosun.Config.UIConfig.dataRe;
    protected void Page_Load(object sender, EventArgs e)
    {
        string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
        if (ReadType == "1")
        {
            isMakeHTML.Visible = false;
        }
        if (Request.Form["Option"] != null && !Request.Form["Option"].Trim().Equals("")
    && Request.Form["NewsID"] != null && !Request.Form["NewsID"].Trim().Equals(""))
        {
            string id = Foosun.Common.Input.Filter(Request.Form["NewsID"].Trim());
            string HiddenSpecialID = Request.Form["HiddenSpecialID"];

            switch (Request.Form["Option"].Trim())
            {
                case "RecyleNews":
                    this.Authority_Code = "C003";
                    this.CheckAdminAuthority();
                    if (HiddenSpecialID == null || HiddenSpecialID.Equals(""))
                    {
                        this.Option_Recyle(id);
                    }
                    else
                    {
                        this.Option_Recyle(id, HiddenSpecialID);
                    }
                    break;
                case "DeleteNews":
                    this.Authority_Code = "C003";
                    this.CheckAdminAuthority();
                    if (HiddenSpecialID == null || HiddenSpecialID.Equals(""))
                    {
                        this.Option_Delete(id);
                    }
                    else
                    {
                        this.Option_Recyle(id, HiddenSpecialID);
                    }
                    
                    break;
                case "LockNews":
                    this.Authority_Code = "C008";
                    this.CheckAdminAuthority();
                    this.Option_Lock(id, 1);
                    break;
                case "ResetOrder":
                    this.Authority_Code = "C007";
                    this.CheckAdminAuthority();
                    this.Option_ResetOrder(id);
                    break;
                case "makeFilesHTML":
                    this.Authority_Code = "C016";
                    this.CheckAdminAuthority();
                    this.Option_makeFilesHTML(id);
                    break;
                case "XMLRefresh":
                    this.Authority_Code = "C017";
                    this.CheckAdminAuthority();
                    this.Option_XMLRefresh(id);
                    break;
                case "ClassRefresh":
                    this.Option_ClassRefresh(id);
                    break;
                case "UNLockNews":
                    this.Option_Lock(id, 0);
                    break;
                case "ToOldNews":
                    this.Authority_Code = "C012";
                    this.CheckAdminAuthority();
                    this.Option_ToOld(id);
                    break;
                case "ToOldNewsClass":
                    this.Authority_Code = "C013";
                    this.CheckAdminAuthority();
                    this.Option_ToOldClass(id);
                    break;
                case "SetTop":
                    this.Authority_Code = "C011";
                    this.CheckAdminAuthority();
                    this.Option_SetTop(id);
                    break;
                case "UnSetTop":
                    this.Authority_Code = "C011";
                    this.CheckAdminAuthority();
                    this.Option_UnSetTop(id);
                    break;
                case "clearFiles":
                    this.Option_clearFiles(id);
                    break;
                case "delNumber":
                    this.Authority_Code = "C014";
                    this.CheckAdminAuthority();
                    this.Option_delNumber(id);
                    break;
                case "CheckStatNews":
                    this.Option_CheckStat(id);
                    break;
                case "allCheck":
                    this.allCheck(id);
                    break;
            }
            Response.End();
            return;
        }

        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {

            SiteCopyRight.InnerHtml = CopyRight;
            string _TClassID = Request.QueryString["ClassID"];
            if (_TClassID != null)
            {
                if (Foosun.Config.verConfig.PublicType != "1")
                {
                    ClassNewsIndex.InnerHtml = "<span title=\"门户版功能\" style=\"color:#999999\">索引</span>";
                }
                else
                {
                    ClassNewsIndex.InnerHtml = "<a href=\"javascript:makeClassIndex('" + _TClassID + "')\" title=\"生成此栏目的索引文件\" class=\"topnavichar\">索引</a>";
                }
            }
            else { ClassNewsIndex.InnerHtml = "<span title=\"选择了栏目才能生成栏目索引\" style=\"color:#999999\">索引</span>"; }
            if (_TClassID != null) { ClassRefresh.InnerHtml = "<a href=\"javascript:ClassRefresh('" + _TClassID + "')\" title=\"生成此栏目的新闻列表\" class=\"topnavichar\">刷新</a>"; }
            else { ClassRefresh.InnerHtml = "<span title=\"选择了栏目才能生成栏目的新闻列表\" style=\"color:#999999\">刷新</span>"; }
            if (_TClassID != null) { XMLFile.InnerHtml = "<a href=\"javascript:XMLRefresh('" + _TClassID + "')\" title=\"生成此栏目的XML文件\" class=\"topnavichar\">XML</a>"; }
            else { XMLFile.InnerHtml = "<span title=\"选择了栏目才能生成栏目的XML文件\" style=\"color:#999999\">XML</span>"; }

            deltable.InnerHtml = "<span style=\"color:#999999\" title=\"需要选择栏目\">清空数据</span>";
            //if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"] != "")
            //{
            //    keyWorks.Text = Request.QueryString["ClassID"].Trim();
            //    deltable.InnerHtml = "<a href=\"javascript:delNum('" + Request.QueryString["ClassID"].ToString() + "')\" class=\"topnavichar\">清空数据</a>";
            //}
            deltable.InnerHtml = "<a href=\"javascript:delSelectedNum()\" class=\"topnavichar\">清空数据</a>";

            DataTable SiteTB = rd.getSiteList();
            if (SiteTB != null)
            {
                this.DdlSite.DataSource = SiteTB;
                this.DdlSite.DataTextField = "CName";
                this.DdlSite.DataValueField = "ChannelID";
                this.DdlSite.DataBind();
                if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"] != "")
                {
                    string _SiteID = pd.getSiteIDFromClass(Request.QueryString["ClassID"].ToString());
                    for (int m = 0; m < this.DdlSite.Items.Count; m++)
                    {
                        if (this.DdlSite.Items[m].Value == _SiteID) { this.DdlSite.Items[m].Selected = true; }
                    }
                }
            }

            if (SiteID != "0")
            {
                this.DdlSite.Visible = false;
            }
            #region 判断导航
            if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"] != "")
            {
                naviClassName.InnerHtml = getNaviClassName(Request.QueryString["ClassID"].ToString()) + "<img src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />新闻列表";
            }
            else
            {
                naviClassName.InnerHtml = " <img alt=\"\" src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"News_List.aspx\" target=\"sys_main\" class=\"list_link\">全部内容";
            }
            #endregion 判断导航
            ListDataBind(1);
        }
    }
    /// <summary>
    /// 得到导航位置
    /// </summary>
    /// <param name="ClassID"></param>
    /// <returns></returns>
    string getNaviClassName(string ClassID)
    {
        string _Str = "";
        IDataReader dr = rd.getNaviClass(ClassID);
        if (dr.Read())
        {
            _Str += "<img src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"News_List.aspx?ClassID=" + dr["ClassID"].ToString() + "\" class=\"topnavichar\">" + dr["ClassCName"] + "</a>";
            if (dr["ParentID"] != DBNull.Value && dr["ParentID"].ToString() != "0")
            {
                IDataReader dr2 = rd.getNaviClass(dr["ParentID"].ToString());
                while (dr2.Read())
                {
                    _Str = "<a href=\"News_List.aspx?ClassID=" + dr2["ClassID"].ToString() + "\" class=\"topnavichar\">" + dr2["ClassCName"] + "</a>" + _Str;
                    _Str = getNaviClassName(dr2["ParentID"].ToString()) + "<img src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />" + _Str;
                }
                dr2.Close();
            }
        }
        dr.Close();

   
        return _Str;
    }



    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        ListDataBind(PageIndex);
    }

    private void ListDataBind(int PageIndex)
    {
        string ClassID = "";
        string SpecialID = Foosun.Common.Input.Filter(Request.QueryString["SpecialID"]);

        this.HiddenSpecialID.Value = SpecialID;

        if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"].Trim() != "")
        {
            ClassID = Request.QueryString["ClassID"].ToString();
        }
        if (!string.IsNullOrEmpty(this.keyWorks.Text))
        {
            ClassID = this.keyWorks.Text;
            if (ClassID.IndexOf(',') != -1)
            {
                ClassID = ClassID.Substring(0, ClassID.IndexOf(','));
            }
            this.keyWorks.Text = string.Empty;
        }
        string sKeywrd = Foosun.Common.Input.Filter(this.TxtKeywords.Text.Trim());
        string DdlKwdType = this.DdlKwdType.SelectedValue;
        string sChooses = this.LblChoose.Text.Trim();
        string site = "0";
        if (this.DdlSite.Visible == false)
        {
            site = SiteID;
        }
        else
        {
            site = this.DdlSite.SelectedValue;
        }
        int i = 0, j = 0;
        string Editor = "";
        if (Request.QueryString["Editor"] != null)
        {
            Editor = Request.QueryString["Editor"].ToString();
        }
        int num = 20;
        DataTable dt = rd.GetPage(SpecialID, Editor, ClassID, sKeywrd, DdlKwdType, sChooses, site, PageIndex, num, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null && dt.Rows.Count > 0)
        {
            dt.Columns.Add("op", typeof(string));
            dt.Columns.Add("htmllock", typeof(string));
            dt.Columns.Add("NewsTitles", typeof(string));
            dt.Columns.Add("CheckStats", typeof(string));
            dt.Columns.Add("isConstrs", typeof(string));
            dt.Columns.Add("CommNum", typeof(string));
            ArrayList arrNotPop = new ArrayList();//存放没有权限的新闻下标
            for (int k = 0; dt.Rows.Count > k; k++)
            {
                this.ClassID = dt.Rows[k]["ClassID"].ToString();
                bool ispop = this.CheckAuthority();
                if (!ispop)//如果没有权限,则将下标添加到集合中
                {
                    arrNotPop.Add(dt.Rows[k]);
                    continue;
                }
                string _ishtml1 = "";
                if (dt.Rows[k]["ishtml"].ToString() != "1") { _ishtml1 = "&nbsp;<img src=\"../../sysImages/folder/unhtml.gif\" border=\"0\" title=\"未生成静态页面\">"; }
                string titleB = "";
                string titleB1 = "";
                string titleI = "";
                string titleI1 = "";
                string titleC = "";
                string titleC1 = "";
                if (dt.Rows[k]["TitleBTF"].ToString() == "1") { titleB = "<strong>"; titleB1 = "</strong>"; }
                if (dt.Rows[k]["TitleITF"].ToString() == "1") { titleI = "<i>"; titleI1 = "</i>"; }
                if (dt.Rows[k]["TitleColor"].ToString().Length > 2) { titleC = "<font color=\"" + dt.Rows[k]["TitleColor"].ToString() + "\">"; titleC1 = "</font>"; }
                int intItitle = dt.Rows[k]["NewsTitle"].ToString().Length;
                string titleStr = dt.Rows[k]["NewsTitle"].ToString();
                if (intItitle > 26)
                {
                    titleStr = titleStr.Substring(0, 26) + "...";
                }
                dt.Rows[k]["NewsTitles"] = titleC + titleI + titleB + titleStr + titleB1 + titleI1 + titleC1 + _ishtml1;
                string[] CheckStat = dt.Rows[k]["CheckStat"].ToString().Split('|');
                string _strCheck = "";
                if (CheckStat[0] == "1") { _strCheck = "<img style=\"cursor:pointer;\" src=\"../../sysImages/folder/no1.gif\" title=\"一级审核的新闻\">"; }
                if (CheckStat[0] == "2") { _strCheck = "<img style=\"cursor:pointer;\" src=\"../../sysImages/folder/no2.gif\" title=\"二级审核的新闻\">"; }
                if (CheckStat[0] == "3") { _strCheck = "<img style=\"cursor:pointer;\" src=\"../../sysImages/folder/no3.gif\" title=\"三级审核的新闻\">"; }
                if (CheckStat[0] == "0") { _strCheck = "<img style=\"cursor:pointer;\" src=\"../../sysImages/folder/no0.gif\" title=\"不需要审核的新闻\">"; }
                if (CheckStat[1] == "0" && CheckStat[2] == "0" && CheckStat[3] == "0") { _strCheck += "<img src=\"../../sysImages/folder/yes.gif\" title=\"已审核\">"; }
                if (CheckStat[1] != "0" || CheckStat[2] != "0" || CheckStat[3] != "0") { _strCheck += "<img src=\"../../sysImages/folder/no.gif\" title=\"未通过最终审核\">"; }

                //无需审核
                if (CheckStat[0] == "0") { _strCheck += "&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"不需要审核\"></a>&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"不需要审核\"></a>&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"不需要审核\"></a>"; }

                //一级审核
                if (CheckStat[0] == "1" && CheckStat[1] == "1") { _strCheck += "&nbsp;┊&nbsp;<a href=\"javascript:CheckStat('" + dt.Rows[k]["ID"].ToString() + "|1')\"  class=\"list_link\"><img border=\"0\" src=\"../../sysImages/folder/cno1.gif\" title=\"需要审核\"></a></a>&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"不需要审核\"></a>&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"不需要审核\"></a>"; }
                if (CheckStat[0] == "1" && CheckStat[1] == "0") { _strCheck += "&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"已审核\"></a>&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"不需要审核\"></a>&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"不需要审核\"></a>"; }

                //二级审核
                if (CheckStat[0] == "2")
                {
                    string __strCheck2_1 = "";
                    string __strCheck2_2 = "";
                    if (CheckStat[1] == "1") { __strCheck2_1 += "&nbsp;┊&nbsp;<a href=\"javascript:CheckStat('" + dt.Rows[k]["ID"].ToString() + "|1')\"  class=\"list_link\"><img border=\"0\" src=\"../../sysImages/folder/cno1.gif\" title=\"需要审核\"></a>"; }
                    else { __strCheck2_1 += "&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"已审核\"></a>"; }

                    if (CheckStat[2] == "1") { __strCheck2_2 += "&nbsp;┊&nbsp;<a href=\"javascript:CheckStat('" + dt.Rows[k]["ID"].ToString() + "|2')\"  class=\"list_link\"><img border=\"0\" src=\"../../sysImages/folder/cno1.gif\" title=\"需要审核\"></a></a>"; }
                    else { __strCheck2_2 += "&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"已审核\"></a>"; }
                    _strCheck += __strCheck2_1 + __strCheck2_2 + "&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"非三级审核\"></a>";
                }

                //三级审核
                if (CheckStat[0] == "3")
                {
                    string _strCheck1 = "";
                    string _strCheck2 = "";
                    string _strCheck3 = "";
                    if (CheckStat[1] == "1") { _strCheck1 += "&nbsp;┊&nbsp;<a href=\"javascript:CheckStat('" + dt.Rows[k]["ID"].ToString() + "|1')\"  class=\"list_link\"><img border=\"0\" src=\"../../sysImages/folder/cno1.gif\" title=\"需要审核\"></a></a>"; }
                    else { _strCheck1 += "&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"已审核\"></a>"; }

                    if (CheckStat[2] == "1") { _strCheck2 += "&nbsp;┊&nbsp;<a href=\"javascript:CheckStat('" + dt.Rows[k]["ID"].ToString() + "|2')\"  class=\"list_link\"><img border=\"0\" src=\"../../sysImages/folder/cno1.gif\" title=\"需要审核\"></a></a>"; }
                    else { _strCheck2 += "&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"已审核\"></a>"; }

                    if (CheckStat[3] == "1") { _strCheck3 += "&nbsp;┊&nbsp;<a href=\"javascript:CheckStat('" + dt.Rows[k]["ID"].ToString() + "|3')\"  class=\"list_link\"><img border=\"0\" src=\"../../sysImages/folder/cno1.gif\" title=\"需要审核\"></a></a>"; }
                    else { _strCheck3 += "&nbsp;┊&nbsp;<img border=\"0\" src=\"../../sysImages/folder/cno0.gif\" title=\"已审核\"></a>"; }
                    _strCheck += _strCheck1 + _strCheck2 + _strCheck3;
                }
                dt.Rows[k]["CheckStats"] = _strCheck;
                dt.Rows[k]["isConstrs"] = "";
                if (dt.Rows[k]["isConstr"].ToString() == "1")
                {
                    dt.Rows[k]["isConstrs"] = "&nbsp;<img style=\"cursor:pointer;\" src=\"../../sysImages/folder/isConstr.gif\" title=\"此文章为用户投稿\" />";
                }
                string SetTop = null;
                if (dt.Rows[k]["OrderID"].ToString() != "10")
                {
                    SetTop = "<a href=\"javascript:SetTop('" + dt.Rows[k]["ID"].ToString() + "')\" class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/top.gif\" alt=\"固顶此内容\" border=\"0\" /></a>";
                }
                else
                {
                    SetTop = "<a href=\"javascript:UnSetTop('" + dt.Rows[k]["ID"].ToString() + "')\"  class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/untop.gif\" alt=\"解固此内容\" border=\"0\" /></a>";
                }
                //dt.Rows[k]["UserNames"] = "<a href=\"News_list.aspx?ClassID=" + Request.QueryString["ClassID"] + "&Editor=" + dt.Rows[k]["Editor"] + "\" class=\"list_link\">" + dt.Rows[k]["Editor"] + "</a>";
                string _islock = "";
                if (dt.Rows[k]["islock"].ToString() == "0")
                {
                    _islock = "<a href=\"javascript:setLock('" + dt.Rows[k]["ID"].ToString() + "')\"><img src=\"../../sysImages/folder/yes.gif\" alt=\"正常的新闻&#13;点击锁定\" border=\"0\" /></a>";
                }
                else
                {
                    _islock = "<a href=\"javascript:setUNLock('" + dt.Rows[k]["ID"].ToString() + "')\"><img src=\"../../sysImages/folder/no.gif\" alt=\"已被锁定的新闻&#13;点击取消锁定\" border=\"0\" /></a>";
                }
                dt.Rows[k]["htmllock"] = _islock;
                int CommNumber = rd.infoIDNum(dt.Rows[k]["NewsID"].ToString(), "0", dt.Rows[k]["DataLib"].ToString());
                if (CommNumber > 0)
                {
                    dt.Rows[k]["CommNum"] = "&nbsp;<a title=\"此新闻有" + CommNumber + "条评论\" href=\"../user/Usermycom.aspx?iID=" + dt.Rows[k]["NewsID"].ToString() + "&aID=0&TB=" + dt.Rows[k]["DataLib"].ToString() + "\" class=\"list_link\" style=\"font-size:10px;\">(" + CommNumber + ")</a>";
                }
                else
                {
                    dt.Rows[k]["CommNum"] = "";
                }
                if (!ispop)
                {
                    dt.Rows[k]["op"] = "无权限";
                }
                else
                {
                    dt.Rows[k]["op"] = "<a href=\"News_add.aspx?ClassID=" + dt.Rows[k]["ClassID"].ToString() + "&NewsID=" + dt.Rows[k]["NewsID"].ToString() + "&EditAction=Edit\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" alt=\"修改\" border=\"0\" /></a>&nbsp;<a href=\"news_review.aspx?ID=" + dt.Rows[k]["NewsID"].ToString() + "\" target=\"_blank\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/review.gif\" alt=\"预览\" border=\"0\" /></a>&nbsp;" + SetTop + "&nbsp;<a href=\"javascript:AddToJS('" + dt.Rows[k]["ID"].ToString() + "')\"  class=\"list_link\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/js.gif\" alt=\"加入自由JS\" border=\"0\" /></a>&nbsp;<a href=\"javascript:Recycle('" + dt.Rows[k]["ID"].ToString() + "')\" class=\"list_link\"><img src=\"../../sysImages/folder/del.gif\" alt=\"删除到回收站\" border=\"0\" /></a>&nbsp;<a href=\"javascript:Delete('" + dt.Rows[k]["ID"].ToString() + "')\" class=\"list_link\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"彻底删除\" border=\"0\" /></a><input name=\"Checkbox1\" type=\"checkbox\" value=" + dt.Rows[k]["ID"].ToString() + "  runat=\"server\" />";
                }
            }

            //从行中移出没有权限的新闻
            for (int m = 0; m < arrNotPop.Count; m++)
            {
                dt.Rows.Remove((DataRow)arrNotPop[m]);
            }
            DataList1.Visible = true;
        }
        DataList1.DataSource = dt;
        DataList1.DataBind();

    }
    protected void LnkBtnAll_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "";
        ListDataBind(1);
    }

    protected void LnkBtnAuditing_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Auditing";
        ListDataBind(1);
    }
    protected void LnkBtnUnAuditing_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "UnAuditing";
        ListDataBind(1);
    }
    protected void LnkBtnContribute_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Contribute";
        ListDataBind(1);
    }
    protected void LnkBtnCommend_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Commend";
        ListDataBind(1);
    }

    protected void LnkBtnLock_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Lock";
        ListDataBind(1);
    }

    protected void LnkBtnUnLock_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "UnLock";
        ListDataBind(1);
    }

    protected void LnkBtnTop_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Top";
        ListDataBind(1);
    }
    protected void LnkBtnHot_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Hot";
        ListDataBind(1);
    }
    protected void LnkBtnPic_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Pic";
        ListDataBind(1);
    }
    protected void LnkBtnSplendid_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Splendid";
        ListDataBind(1);
    }
    protected void LnkBtnHeadline_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Headline";
        ListDataBind(1);
    }
    protected void LnkBtnSlide_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "Slide";
        ListDataBind(1);
    }

    protected void LnkBtnmy_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "my";
        ListDataBind(1);
    }

    protected void LnkBtnisHtml_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "isHtml";
        ListDataBind(1);
    }

    protected void LnkBtnunisHtml_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "unisHtml";
        ListDataBind(1);
    }

    protected void LnkBtnundiscuzz_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "discuzz";
        ListDataBind(1);
    }

    protected void LnkBtnuncommat_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "commat";
        ListDataBind(1);
    }
    protected void LnkBtnunvoteTF_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "voteTF";
        ListDataBind(1);
    }
    protected void LnkBtnuncontentPicTF_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "contentPicTF";
        ListDataBind(1);
    }
    protected void LnkBtnunPOPTF_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "POPTF";
        ListDataBind(1);
    }

    protected void LnkBtnunFilesURL_Click(object sender, EventArgs e)
    {
        this.LblChoose.Text = "FilesURL";
        ListDataBind(1);
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        ListDataBind(1);
    }
    protected void DdlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListDataBind(1);
    }
    protected void DdlNewsTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListDataBind(1);
    }
    //删除到回收站
    private void Option_Recyle(string sid)
    {
        string id = "'" + sid.Replace(",", "','") + "'";
        int n = rd.Del_news(id);
        rd.delTBNewsID(sid);
        //rd.delTBDateNumber(SearchEngine.datenumber());
        //rd.delTBTypeNumber(SearchEngine.conditionnumbers());
        Response.Clear();
        Response.Write(n + "%成功将" + n + "条内容放入回收站中！");
    }

    //从此专题移出
    private void Option_Recyle(string sid,string specialID)
    {
        string[] id = sid.Split(',');
        Foosun.CMS.Special sc = new Foosun.CMS.Special();
        int ln = id.Length;
        int n = 0;
        for (int i = 0; i < id.Length; i++)
        {
            if (!id[i].Trim().Equals(""))
            {
                try
                {
                    string strNewsID = rd.getNewsIDById(id[i]);

                    //for(int j = 0;j<rd.)
                    //int newsID = rd.(id[i]);
                    sc.RemoveNews(specialID, strNewsID);
                    n++;
                }
                catch
                {
                    continue;
                }
            }
        }
        Response.Clear();
        Response.Write(n + "%成功将" + n + "条内容移出此专题！");
    }

    //彻底删除
    private void Option_Delete(string sid)
    {
        Response.Clear();
        string ids = "'" + sid.Replace(",", "','") + "'";
        rd.delTBNewsID(sid);
        //rd.delTBDateNumber(SearchEngine.datenumber());
        //rd.delTBTypeNumber(SearchEngine.conditionnumbers());

        string[] id = sid.Split(',');
        int ln = id.Length;
        int n = 0;
        for (int i = 0; i < id.Length; i++)
        {
            if (!id[i].Trim().Equals(""))
            {
                try
                {
                    string path = rd.sel_path(id[i]);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    rd.Del_newsc(id[i]);
                    n++;
                }
                catch
                {
                    continue;
                }
            }
        }
        Response.Write(n + "%成功删除" + n + "条新闻！");
    }

    //锁定内容
    private void Option_Lock(string sid, int NUMS)
    {
        string id = "'" + sid.Replace(",", "','") + "'";
        int n = rd.Update_Lock(id, NUMS);
        Response.Clear();
        Response.Write(n + "%成功操作" + n + "条新闻！");
    }
    //重置权重
    private void Option_ResetOrder(string sid)
    {
        string id = "'" + sid.Replace(",", "','") + "'";
        int n = rd.Update_ResetOrde(id);
        Response.Clear();
        Response.Write(n + "%成功操作" + n + "条新闻！");
    }

    //生成栏目
    private void Option_ClassRefresh(string sid)
    {
        Foosun.Publish.General PN = new Foosun.Publish.General();
        if (PN.publishSingleClass(sid))
        {
            Response.Clear();
            Response.Write("成功生成了此栏目！");
        }
        else
        {
            Response.Clear();
            Response.Write("生成失败！");
        }
    }

    private void Option_XMLRefresh(string sid)
    {
        if (Foosun.Publish.General.publishXML(sid))
        {
            Response.Clear();
            Response.Write("成功此栏目XML成功！");
        }
        else
        {
            Response.Clear();
            Response.Write("生成XML失败！\n可能是你选择的栏目没有新闻");
        }
    }

    //生成静态
    private void Option_makeFilesHTML(string sid)
    {
        string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
        if (ReadType == "1")
        {
            Response.Clear();
            Response.Write("动态调用不能生成静态！");
            Response.End();
        }
        string id = sid;
        if (id.IndexOf(",") == -1)
        {
            string[] ARR1 = rd.GetNewsIDfromID1(int.Parse(id)).Split('|');
            string NewsID_1 = ARR1[0];
            string ClassID_1 = ARR1[1];
            if (Foosun.Publish.General.publishSingleNews(NewsID_1, ClassID_1))
            {
                rd.updateNewsHTML(1, NewsID_1);
                Response.Clear();
                Response.Write("成功生成1条新闻！");
            }
            else
            {
                Response.Clear();
                Response.Write("生成失败！如果有浏览权限，也不会生成");
            }
        }
        else
        {
            string sNewsID = "";
            string sClassID = "";
            string[] idARR = id.Split(',');
            int j = 0;
            int m = 0;
            for (int i = 0; i < idARR.Length; i++)
            {
                string[] ARR2 = rd.GetNewsIDfromID1(int.Parse(idARR[i].ToString())).Split('|');
                sNewsID = ARR2[0];
                sClassID = ARR2[1];
                if (Foosun.Publish.General.publishSingleNews(sNewsID, sClassID))
                {
                    rd.updateNewsHTML(1, sNewsID);
                    j++;
                }
                else
                {
                    m++;
                }
            }
            Response.Clear();
            Response.Write(j + "%成功生成" + j + "条新闻！失败" + m + "条新闻(可能有浏览权限。)");
        }
    }

    private void allCheck(string sid)
    {
        string[] idARR = sid.Split(',');
        int[] nid = new int[idARR.Length];
        int i = 0;
        foreach (string s in idARR)
        {
            nid[i++] = int.Parse(s);
        }
        rd.allCheck(nid);
        Response.Write("1%成功操作" + (idARR.Length) + "条新闻！");
    }

    //归档新闻
    private void Option_ToOld(string sid)
    {
        Response.Clear();
        string id = "'" + sid.Replace(",", "','") + "'";
        DataTable tb = rd.sel_old_News();
        if (tb != null)
        {
            string fieldnm = "";
            int i = 0;
            foreach (DataColumn c in tb.Columns)
            {
                if (c.ColumnName.ToLower().Equals("id") || c.ColumnName.ToLower().Equals("oldtime") || c.ColumnName.ToLower().Equals("datalib"))
                    continue;
                if (i > 0)
                    fieldnm += ",";
                fieldnm += c.ColumnName;
                i++;
            }
            DateTime oldtimes = DateTime.Now;
            if (rd.Add_old_News(fieldnm, id, oldtimes) != 0 && rd.del_new_News(id) != 0)
                Response.Write("1%归档成功!");
            else
                Response.Write("0%");
        }
        else
        {
            Response.Write("0%");
        }
    }
    //归档栏目符合条件的新闻
    private void Option_ToOldClass(string ClassID)
    {
        Response.Clear();
        //得到此栏目的归档数字
        int intHistory = rd.sel_old_classInHitoryDay(ClassID);
        int j = 0;
        if (intHistory > 0)
        {
            //开始获得符合条件的的新闻
            DataTable dt = rd.sel_old_classNews(ClassID);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    DateTime oTime = DateTime.Parse(dt.Rows[m]["CreatTime"].ToString());
                    DateTime nTime = DateTime.Now;
                    TimeSpan getNum = nTime - oTime;
                    int daysTF = getNum.Days;
                    if (daysTF > intHistory)
                    {
                        DataTable tb = rd.sel_old_News();
                        if (tb != null)
                        {
                            string fieldnm = "";
                            int i = 0;
                            foreach (DataColumn c in tb.Columns)
                            {
                                if (c.ColumnName.ToLower().Equals("id") || c.ColumnName.ToLower().Equals("oldtime") || c.ColumnName.ToLower().Equals("datalib"))
                                    continue;
                                if (i > 0)
                                    fieldnm += ",";
                                fieldnm += c.ColumnName;
                                i++;
                            }
                            DateTime oldtimes = DateTime.Now;
                            if (rd.Add_old_News(fieldnm, dt.Rows[m]["id"].ToString(), oldtimes) != 0 && rd.del_new_News(dt.Rows[m]["id"].ToString()) != 0)
                            {
                                j++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                dt.Clear(); dt.Dispose();
            }
        }
        Response.Write("1%成功操作" + j + "条新闻!");
    }

    //固顶
    private void Option_SetTop(string sid)
    {
        Response.Clear();
        if (rd.settop(sid) != 0)
            Response.Write("成功将所选新闻固顶!");
        else
            Response.Write("固顶错误");
    }
    //解固
    private void Option_UnSetTop(string sid)
    {
        Response.Clear();
        if (rd.unsettop(sid) != 0)
            Response.Write("成功将所选新闻解固!");
        else
            Response.Write("解固错误");
    }

    //清空数据
    private void Option_delNumber(string ClassID)
    {
        Response.Clear();
        if (rd.delNumber(ClassID) != 0)
            Response.Write("栏目数据已经被清空!");
        else
            Response.Write("清除数据错误。\n或者此栏目无数据!");
    }

    private void Option_clearFiles(string ID)
    {
        if (ID == "foosun")
        {
            rd.deleteFilesurl(1, "");
        }
        else
        {
            string[] sID = ID.Split(',');
            for (int i = 0; i < sID.Length; i++)
            {
                rd.deleteFilesurl(0, sID[i]);
            }
        }
        Response.Write("1%附件清理成功!");
    }

    //审核内容
    private void Option_CheckStat(string ID)
    {
        Response.Clear();
        string[] tID = ID.Split('|');
        string getID = tID[0];
        string levelID = tID[1];
        switch (levelID)
        {
            case "1":
                this.Authority_Code = "C004";
                this.CheckAdminAuthority();
                rd.upCheckStat(getID, 1);
                break;
            case "2":
                this.Authority_Code = "C005";
                this.CheckAdminAuthority();
                rd.upCheckStat(getID, 2);
                break;
            case "3":
                this.Authority_Code = "C006";
                this.CheckAdminAuthority();
                rd.upCheckStat(getID, 3);
                break;
        }
        Response.Write("1%成功审核您选择的新闻！");
    }

    protected void DataList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            #region 属性
            System.Web.UI.WebControls.Label lblp = (System.Web.UI.WebControls.Label)e.Item.FindControl("LblProperty");
            if (lblp != null)
            {
                string txt = lblp.Text.Trim();
                string prpty = "";
                if (txt.Length >= 1 && txt.Substring(0, 1).Equals("1"))
                {
                    prpty = "推荐";
                }
                if (txt.Length >= 3 && txt.Substring(2, 1).Equals("1"))
                {
                    if (!prpty.Equals("")) prpty += " | ";
                    prpty += "滚动";
                }
                if (txt.Length >= 5 && txt.Substring(4, 1).Equals("1"))
                {
                    if (!prpty.Equals("")) prpty += " | ";
                    prpty += "热点";
                }
                if (txt.Length >= 7 && txt.Substring(6, 1).Equals("1"))
                {
                    if (!prpty.Equals("")) prpty += " | ";
                    prpty += "幻灯";
                }
                if (txt.Length >= 9 && txt.Substring(8, 1).Equals("1"))
                {
                    if (!prpty.Equals("")) prpty += " | ";
                    prpty += "头条";
                }
                if (txt.Length >= 11 && txt.Substring(10, 1).Equals("1"))
                {
                    if (!prpty.Equals("")) prpty += " | ";
                    prpty += "公告";
                }
                if (txt.Length >= 13 && txt.Substring(12, 1).Equals("1"))
                {
                    if (!prpty.Equals("")) prpty += " | ";
                    prpty += "WAP";
                }
                if (txt.Length >= 15 && txt.Substring(14, 1).Equals("1"))
                {
                    if (!prpty.Equals("")) prpty += " | ";
                    prpty += "精彩";
                }
                if (prpty.Equals("")) prpty = "未设置";
                lblp.Text = prpty;
            }
            #endregion 属性
            #region 置顶图片
            System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("ImgOrder");
            if (img != null)
            {
                if (img.AlternateText.Equals("10"))
                {
                    img.ImageUrl = "../../sysImages/folder/news_top.gif";
                    img.AlternateText = "总置顶新闻,点击查看简洁内容";
                }
                else
                {
                    img.ImageUrl = "../../sysImages/folder/news_common.gif";
                    img.AlternateText = "普通新闻,点击查看简洁内容";
                }
            }
            #endregion 置顶图片
            #region 内容类型
            System.Web.UI.WebControls.Image imgtp = (System.Web.UI.WebControls.Image)e.Item.FindControl("ImgNewType");
            System.Web.UI.WebControls.Image imgpic = (System.Web.UI.WebControls.Image)e.Item.FindControl("ImgPic");
            if (imgpic != null)
                imgpic.Visible = false;
            if (imgtp != null)
            {
                if (imgtp.AlternateText.Equals("0"))
                {
                    imgtp.ImageUrl = "../../sysImages/folder/news_text.gif";
                    imgtp.AlternateText = "普通内容";
                    imgtp.Attributes.Add("onclick", "");
                }
                else if (imgtp.AlternateText.Equals("1"))
                {
                    imgtp.ImageUrl = "../../sysImages/folder/news_img.gif";
                    imgtp.AlternateText = "图片信息,点击更改图片";
                }
                else if (imgtp.AlternateText.Equals("2"))
                {
                    imgtp.ImageUrl = "../../sysImages/folder/news_outer.gif";
                    imgtp.AlternateText = "标题信息";
                    imgtp.Attributes.Add("onclick", "");
                    if (imgpic != null)
                    {
                        imgpic.Visible = false;
                    }
                    else
                    {
                        imgpic.Visible = true;
                        imgpic.ImageUrl = "../../sysImages/folder/news_img.gif";
                        imgpic.AlternateText = "点击更改图片";
                    }
                }
            }
            #endregion 新闻类型
        }
    }
}