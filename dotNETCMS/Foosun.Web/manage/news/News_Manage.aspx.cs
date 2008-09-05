//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                        Forum:bbs.foosun.net                     ==
//==                       WebSite:www.foosun.net                    ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                     Tel:86-28-85098980/66026180                 ==
//==                     QQ:655071,MSN:ikoolls@gmail.com             ==
//==                     Email:Service@foosun.cn                     ==
//==                       Code By WangZhenjiang                     ==
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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Foosun.CMS;

public partial class manage_news_News_Manage : Foosun.Web.UI.ManagePage
{
    ContentManage td = new ContentManage();
    private DataTable TbClass;
    private string OriginalType;
    private string sOrgNews = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int flag = 0;
            this.Panel1.Visible = this.Panel2.Visible = false;
            if (Request.QueryString["Option"] != null && Request.QueryString["dbtab"] != null && !Request.QueryString["dbtab"].Trim().Equals(""))
            {
                this.LblNewsTable.Text = Request.QueryString["dbtab"].Trim();
                string sssss = Request.QueryString["dbtab"].Trim();
                switch (Request.QueryString["Option"])
                {
                    case "BnProperty":
                        this.Authority_Code = "C015";
                        this.CheckAdminAuthority();
                        this.LblNarrate.Text = "批量设置";
                        this.BtnOK.Text = "确定按照以上方式开始设置";
                        this.BtnOK.CommandName = "set";
                        this.Panel2.Visible = true;
                        flag = 0;
                        break;
                    case "BnMove":
                        this.Authority_Code = "C009";
                        this.CheckAdminAuthority();
                        this.LblNarrate.Text = "移动到 >>";
                        this.BtnOK.Text = "确定开始移动";
                        this.BtnOK.CommandName = "move";
                        this.Panel1.Visible = true;
                        flag = 1;
                        break;
                    case "BnCopy":
                        this.Authority_Code = "C010";
                        this.CheckAdminAuthority();
                        this.LblNarrate.Text = "复制到 >>";
                        this.BtnOK.Text = "确定开始复制";
                        this.BtnOK.CommandName = "copy";
                        this.Panel1.Visible = true;
                        flag = 2;
                        break;
                    default:
                        PageError("没有参数或是参数无效！", "");
                        return;
                }
            }
            else
            {
                PageError("没有参数或是参数无效！", "");
                return;
            }
            TbClass = td.sel_News_Class();
            if (Request.QueryString["ids"] != null && !Request.QueryString["ids"].Trim().Equals(""))
            {
                this.LblIDs.Text = Request.QueryString["ids"];
                this.DdlType.SelectedValue = "0";
                BindNews();
            }
            else
            {
                this.DdlType.SelectedValue = "1";
                BindClass();
                this.DdlType.Enabled = false;
            }
            if (flag == 1 || flag == 2)
            {
                this.LstTarget.Items.Clear();
                ClassRender(this.LstTarget, "0", 0);
            }
            this.Templet.Text = td.GetParamBase("ReadNewsTemplet");
        }

    }
    private void BindNews()
    {
        this.LstOriginal.Items.Clear();
        string id = Foosun.Common.Input.Filter(this.LblIDs.Text.Trim());
        if (!id.Equals(""))
        {
            string s = Foosun.Common.Input.Filter(this.LblIDs.Text);
            string sfilter = this.LblNewsTable.Text;
            DataTable tb = null;
            if (s.IndexOf(",") > 0)
            {
                string ss = s.Replace(",", "','");
                tb = td.sel_LblNewsTable(sfilter, ss);
            }
            else
            {
                tb = td.sel_LblNewsTable(sfilter, s);
            }
            if (tb != null)
            {
                foreach (DataRow r in tb.Rows)
                {
                    ListItem it = new ListItem();
                    it.Value = r[1].ToString();
                    it.Text = r[2].ToString();
                    this.LstOriginal.Items.Add(it);
                }
                tb.Dispose();
                this.LstOriginal.Enabled = false;
            }
        }
    }
    private void BindClass()
    {
        if (TbClass == null || TbClass.Rows.Count < 1)
        {
            TbClass = td.sel_News_Class();
        }
        this.LstOriginal.Items.Clear();
        ClassRender(this.LstOriginal, "0", 0);
        this.LstOriginal.Enabled = true;
    }
    private void ClassRender(ListBox lst, string PID, int Layer)
    {
        Foosun.CMS.Common.rootPublic rp = new Foosun.CMS.Common.rootPublic();
        DataTable dts = rp.getClassListPublic(PID);
        if (dts.Rows.Count < 1)
            return;
        else
        {
            foreach (DataRow r in dts.Rows)
            {
                ListItem it = new ListItem();
                string stxt = "";
                it.Value = r["ClassID"].ToString();
                if (Layer > 0)
                    stxt = "┝";
                for (int i = 1; i < Layer; i++)
                {
                    stxt += "┉";
                }
                it.Text = stxt + r["ClassCName"].ToString();
                lst.Items.Add(it);
                ClassRender(lst, r["ClassID"].ToString(), Layer + 1);
            }
        }
    }
    protected void DdlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DdlType.SelectedValue.Equals("0"))
        {
            this.BindNews();
        }
        else
        {
            this.BindClass();
        }
    }
    protected void BtnOK_Click(object sender, EventArgs e)
    {
        OriginalType = this.DdlType.SelectedValue;
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
        switch (this.BtnOK.CommandName)
        {
            case "set":
                if (OriginalType.Equals("0"))
                    NewsSet();
                else if (OriginalType.Equals("1"))
                    ClassSet();
                break;
            case "copy":
                if (this.LstTarget.SelectedValue.Trim().Equals(""))
                {
                    PageError("请选择要复制到的栏目!", "News_List.aspx");
                    return;
                }
                if (OriginalType.Equals("1") && this.LstOriginal.SelectedValue.Trim().Equals(this.LstTarget.SelectedValue.Trim()))
                {
                    PageError("要进行操作的栏目和目标栏目不能相同!", "News_List.aspx");
                    return;
                }
                if (OriginalType.Equals("0"))
                    NewsCopy();
                else if (OriginalType.Equals("1"))
                    ClassCopy();
                break;
            case "move":
                if (this.LstTarget.SelectedValue.Trim().Equals(""))
                {
                    PageError("请选择要移动到的栏目!", "News_List.aspx");
                    return;
                }
                if (OriginalType.Equals("1") && this.LstOriginal.SelectedValue.Trim().Equals(this.LstTarget.SelectedValue.Trim()))
                {
                    PageError("要进行操作的栏目和目标栏目不能相同!", "News_List.aspx");
                    return;
                }
                if (OriginalType.Equals("0"))
                    NewsMove();
                else if (OriginalType.Equals("1"))
                    ClassMove();
                break;
        }
    }
    /// <summary>
    /// 对选中新闻复制
    /// </summary>
    /// 
    #region 对选中新闻复制
    protected void NewsCopy()
    {
        //td.delTBDateNumber(SearchEngine.datenumber());
        //td.delTBTypeNumber(SearchEngine.conditionnumbers());
        string sclassid = this.LstTarget.SelectedValue.Trim();
        string sclasstext = td.sel_sclasstext(sclassid);
        string sTb = this.LblNewsTable.Text.Trim();
        bool flag = CheckClass(sclassid);
        if (flag)
        {
            PageError("不能将新闻复制到外部栏目!", "News_List.aspx");
            return;
        }
        string DataLib = td.sel_copy_clsaa(sclassid);
        string[] sNews = sOrgNews.Split(',');
        for (int i = 0; i < sNews.Length; i++)
        {
        ID:
            string NewsID = Foosun.Common.Rand.Number(12);
            if (td.sel_NewsID(NewsID) != 0) { goto ID; }
            string _FileName = td.getFileNameInfo(sNews[i], sTb);
            td.Copy_news(sclassid, DataLib, sNews[i], sTb, NewsID, _FileName);
        }
        PageRight("成功将条新闻复制到&nbsp;<font color=red>" + sclasstext + "</font>&nbsp;栏目中!", "News_List.aspx");
    }
    #endregion
    /// <summary>
    /// 对选中栏目复制
    /// </summary>
    /// 
    #region 对选中栏目复制
    protected void ClassCopy()
    {
        string str = "";
        for (int l = 0; l < this.LstOriginal.Items.Count; l++)
        {
            if (this.LstOriginal.Items[l].Selected)
            {
                str += this.LstOriginal.Items[l].Value + ",";
            }
        }
        string[] soclass = Foosun.Common.Public.Lost(str).Split(',');
        string stclass = this.LstTarget.SelectedValue.Trim();
        string sclasstext = td.sel_sclasstext(stclass);
        string DataLibs = td.sel_copy_clsaa(stclass);
        for (int k = 0; k < soclass.Length; k++)
        {
            bool flag = CheckClass(soclass[k]);
            if (flag)
            {
                PageError("不能复制外部栏目的新闻!", "News_List.aspx");
            }
            bool flag1 = CheckClass(stclass);
            if (flag1)
            {
                PageError("不能将栏目的所有新闻复制到外部栏目!", "News_List.aspx");
            }
            string DataLib = td.sel_copy_clsaa(soclass[k]);
            DataTable dts6 = td.sel_copy_classnews(DataLib, soclass[k]);
            for (int i = 0; i < dts6.Rows.Count; i++)
            {
            ID:
                string NewsID = Foosun.Common.Rand.Number(12);
                if (td.sel_NewsID(NewsID) != 0) { goto ID; }
                string _FileName = td.getFileNameInfo(dts6.Rows[i]["NewsID"].ToString(), DataLib);
                td.Copy_ClassNews(stclass, DataLibs, dts6.Rows[i]["NewsID"].ToString(), DataLib, NewsID, _FileName);
            }
        }
        PageRight("成功将新闻复制到&nbsp;<font color=red>" + sclasstext + "</font>&nbsp;栏目中!", "");
    }
    #endregion
    /// <summary>
    /// 对选中新闻转移
    /// </summary>
    /// 
    #region 对选中新闻转移
    protected void NewsMove()
    {
        string sclassid = this.LstTarget.SelectedValue.Trim();
        string sclasstext = td.sel_sclasstext(sclassid);
        string sTb = this.LblNewsTable.Text.Trim();
        bool flag = CheckClass(sclassid);
        if (flag)
        {
            PageError("不能将新闻移动到外部栏目!", "News_List.aspx");
            return;
        }
        string DataLib = td.sel_copy_clsaa(sclassid);
        string[] sNews = sOrgNews.Split(',');
        for (int i = 0; i < sNews.Length; i++)
        {
        ID:
            string NewsID = Foosun.Common.Rand.Number(12);
            if (td.sel_NewsID(NewsID) != 0) { goto ID; }
            td.Copy_news(sclassid, DataLib, sNews[i], sTb, NewsID, "");
            if (td.del_move(sTb, sNews[i]) == 0)
            {
                PageError("新闻转移到目标栏目失败", "News_List.aspx");
            }
        }
        PageRight("成功将新闻转移到<font color=red>" + sclasstext + "</font>栏目中!", "News_List.aspx");
    }
    #endregion
    /// <summary>
    /// 对选中栏目转移
    /// </summary>
    /// 
    #region 对选中栏目转移
    protected void ClassMove()
    {
        string str = "";
        for (int l = 0; l < this.LstOriginal.Items.Count; l++)
        {
            if (this.LstOriginal.Items[l].Selected == true)
            {
                str += this.LstOriginal.Items[l].Value + ",";
            }
        }
        string[] soclass = Foosun.Common.Public.Lost(str).Split(',');
        string stclass = this.LstTarget.SelectedValue.Trim();
        string sclasstext = td.sel_sclasstext(stclass);
        string DataLibs = td.sel_copy_clsaa(stclass);
        for (int k = 0; k < soclass.Length; k++)
        {
            bool flag = CheckClass(soclass[k]);
            if (flag)
            {
                PageError("不能移动外部栏目的新闻!", "News_List.aspx");
            }
            flag = CheckClass(stclass);
            if (flag)
            {
                PageError("不能将栏目的所有新闻移动到外部栏目!", "News_List.aspx");
            }
            string DataLib = td.sel_copy_clsaa(soclass[k]);
            DataTable dts1 = td.sel_copy_classnews(DataLib, soclass[k]);
            for (int i = 0; i < dts1.Rows.Count; i++)
            {
            ID:
                string NewsID = Foosun.Common.Rand.Number(12);
                if (td.sel_NewsID(NewsID) != 0) { goto ID; }
                td.Copy_ClassNews(stclass, DataLibs, dts1.Rows[i]["Newsid"].ToString(), DataLib, NewsID, "");
                if (td.del_classmove(DataLib, dts1.Rows[i]["Newsid"].ToString()) == 0)
                {
                    PageError("将新闻转移到目标栏目失败!", "");
                }
            }
        }
        PageRight("成功将新闻转移到<font color=red>" + sclasstext + "</font>栏目中!", "");
    }
    #endregion
    /// <summary>
    /// 对选中新闻设置
    /// </summary>
    /// 
    #region 对选中新闻进行设置
    protected void NewsSet()
    {
        string Templet = "";
        int OrderID = 0;
        int CommLinkTF = 0;
        int CommTF = 0;
        int DiscussTF = 0;
        string Tags = "";
        int Click = 0;
        string FileEXName = "";
        string NewsProperty = "";
        string sclassid = this.LstOriginal.SelectedValue.Trim();
        string sTb = this.LblNewsTable.Text.Trim();
        if (this.CheckBox1.Checked)
        {
            Templet = this.Templet.Text;
            if (this.OrderIDDropDownList.SelectedValue != string.Empty)
            {
                OrderID = int.Parse(this.OrderIDDropDownList.SelectedValue);
            }
            if (this.CommLinkTF.Checked) { CommLinkTF = 1; }
            Tags = this.Tags.Text;
            try
            {
                Click = int.Parse(this.Click.Text);
            }
            catch
            {
                Click = 0;
            }
            FileEXName = this.FileEXName.SelectedValue;
            NewsProperty = Newsty();
            if (td.Up_news1(CommTF, DiscussTF, NewsProperty, Templet, OrderID, CommLinkTF, Click, FileEXName, sTb, sOrgNews) != 0)
            {
                PageRight("设置新闻属性成功!", "News_List.aspx");
            }
            else
            {
                PageRight("设置新闻属性失败!", "News_List.aspx");
            }
        }
        else
        {
            if (!this.NewsProperty_CommTF1.Checked || !this.NewsProperty_DiscussTF1.Checked || this.NewsProperty_RECTF1.Checked || this.NewsProperty_MARTF1.Checked || this.NewsProperty_HOTTF1.Checked || this.NewsProperty_FILTTF1.Checked || this.NewsProperty_TTTF1.Checked || this.NewsProperty_ANNTF1.Checked || this.NewsProperty_JCTF1.Checked || this.NewsProperty_WAPTF1.Checked)
            {
                NewsProperty = Newsty();
            }
            if (this.Templet.Text != "")
            {
                Templet = this.Templet.Text;
            }
            if (this.OrderIDDropDownList.SelectedValue != string.Empty)
            {
                OrderID = int.Parse(this.OrderIDDropDownList.SelectedValue);
            }
            if (!this.CommLinkTF.Checked)
            {
                CommLinkTF = 1;
            }
            if (this.Click.Text != "")
            {
                Click = int.Parse(this.Click.Text);
            }

            if (this.NewsProperty_CommTF1.Checked) { CommTF = 1; }
            if (this.NewsProperty_DiscussTF1.Checked) { DiscussTF = 1; }

            if (this.FileEXName.SelectedValue != ".html")
            {
                FileEXName = this.FileEXName.SelectedValue;
            }
            if (td.Up_news2(CommTF, DiscussTF, NewsProperty, Templet, OrderID, CommLinkTF, Click, FileEXName, sTb, sOrgNews) != 0)
            {
                PageRight("设置新闻属性成功!", "News_List.aspx");
            }
            else
            {
                PageRight("设置新闻属性失败!", "News_List.aspx");
            }
        }
    }
    #endregion
    /// <summary>
    /// 对选中栏目设置
    /// </summary>
    /// 
    #region 对选中栏目设置
    protected void ClassSet()
    {
        string Templet = "";
        int OrderID = 0;
        int CommLinkTF = 0;
        string Tags = "";
        string souce = "";
        int Click = 0;
        string FileEXName = "";
        string NewsProperty = "";
        string str = "";
        int CommTF = 0;
        int DiscussTF = 0;
        if (this.NewsProperty_CommTF1.Checked) { CommTF = 1; }
        if (this.NewsProperty_DiscussTF1.Checked) { DiscussTF = 1; }
        for (int l = 0; l < this.LstOriginal.Items.Count; l++)
        {
            if (this.LstOriginal.Items[l].Selected == true)
            {
                str += this.LstOriginal.Items[l].Value + ",";
            }
        }
        string[] soclass = Foosun.Common.Public.Lost(str).Split(',');

        Tags = this.Tags.Text;
        souce = this.Souce.Text;
        if (this.CheckBox1.Checked)
        {
            Templet = this.Templet.Text;
            if (!Foosun.Common.Input.IsInteger(this.OrderIDDropDownList.SelectedValue))
            {
                PageError("请正确填写权重!", "News_List.aspx");
            }
            OrderID = int.Parse(this.OrderIDDropDownList.SelectedValue);
            if (this.CommLinkTF.Checked) { CommLinkTF = 1; }
            if (!Foosun.Common.Input.IsInteger(this.Click.Text))
            {
                PageError("请正确填写点击!", "News_List.aspx");
            }
            Click = int.Parse(this.Click.Text);
            FileEXName = this.FileEXName.SelectedValue;
            NewsProperty = Newsty();
            for (int k = 0; k < soclass.Length; k++)
            {
                bool flag = CheckClass(soclass[k]);
                if (flag)
                {
                    PageError("不能设置外部栏目的新闻!", "News_List.aspx");
                }
                string DataLib = td.sel_copy_clsaa(soclass[k]);
                try
                {
                    td.Up_Classnews(CommTF, DiscussTF, NewsProperty, Templet, OrderID, CommLinkTF, Click, FileEXName, DataLib, soclass[k], Tags, souce);
                }
                catch
                {
                    PageRight("设置新闻属性失败!", "News_List.aspx");
                }

            }
            PageRight("设置新闻属性成功!", "News_List.aspx");
        }
        else
        {
            if (!this.NewsProperty_CommTF1.Checked || !this.NewsProperty_DiscussTF1.Checked || this.NewsProperty_RECTF1.Checked || this.NewsProperty_MARTF1.Checked || this.NewsProperty_HOTTF1.Checked || this.NewsProperty_FILTTF1.Checked || this.NewsProperty_TTTF1.Checked || this.NewsProperty_ANNTF1.Checked || this.NewsProperty_JCTF1.Checked || this.NewsProperty_WAPTF1.Checked)
            {
                NewsProperty = Newsty();
            }
            if (this.Templet.Text != "")
            {
                Templet = this.Templet.Text;
            }
            if (this.OrderIDDropDownList.SelectedValue.Trim() != "")
            {
                OrderID = int.Parse(this.OrderIDDropDownList.SelectedValue);
            }
            if (!this.CommLinkTF.Checked)
            {
                CommLinkTF = 1;
            }
            if (this.Click.Text != "")
            {
                Click = int.Parse(this.Click.Text);
            }
            if (this.FileEXName.SelectedValue.Trim() != "")
            {
                FileEXName = this.FileEXName.SelectedValue;
            }
            for (int s = 0; s < soclass.Length; s++)
            {
                bool flags = CheckClass(soclass[s]);
                if (flags)
                {
                    PageError("不能设置外部栏目的新闻!", "News_List.aspx");
                    return;
                }
                string DataLibs = td.sel_copy_clsaa(soclass[s]);
                try
                {
                    td.Up_Classnews(CommTF, DiscussTF, NewsProperty, Templet, OrderID, CommLinkTF, Click, FileEXName, DataLibs, soclass[s], Tags, souce);
                }
                catch
                {
                    PageRight("设置新闻属性失败!", "News_List.aspx");
                }
            }
            PageRight("设置新闻属性成功!", "News_List.aspx");
        }
    }
    #endregion

    #region 获得新闻属性
    protected string Newsty()
    {
        string NewsProperty_RECTF1 = "";
        string NewsProperty_MARTF1 = "";
        string NewsProperty_HOTTF1 = "";
        string NewsProperty_FILTTF1 = "";
        string NewsProperty_TTTF1 = "";
        string NewsProperty_ANNTF1 = "";
        string NewsProperty_JCTF1 = "";
        string NewsProperty_WAPTF1 = "";
        string NewsProperty = "";
        NewsProperty_RECTF1 = "0";
        if (this.NewsProperty_RECTF1.Checked) { NewsProperty_RECTF1 = "1"; }
        NewsProperty_MARTF1 = "0";
        if (this.NewsProperty_MARTF1.Checked) { NewsProperty_MARTF1 = "1"; }
        NewsProperty_HOTTF1 = "0";
        if (this.NewsProperty_HOTTF1.Checked) { NewsProperty_HOTTF1 = "1"; }
        NewsProperty_FILTTF1 = "0";
        if (this.NewsProperty_FILTTF1.Checked) { NewsProperty_FILTTF1 = "1"; }
        NewsProperty_TTTF1 = "0";
        if (this.NewsProperty_TTTF1.Checked) { NewsProperty_TTTF1 = "1"; }
        NewsProperty_ANNTF1 = "0";
        if (this.NewsProperty_ANNTF1.Checked) { NewsProperty_ANNTF1 = "1"; }
        NewsProperty_JCTF1 = "0";
        if (this.NewsProperty_JCTF1.Checked) { NewsProperty_JCTF1 = "1"; }
        NewsProperty_WAPTF1 = "0";
        if (this.NewsProperty_WAPTF1.Checked) { NewsProperty_WAPTF1 = "1"; }
        return NewsProperty = NewsProperty_RECTF1 + "," + NewsProperty_MARTF1 + "," + NewsProperty_HOTTF1 + "," + NewsProperty_FILTTF1 + "," + NewsProperty_TTTF1 + "," + NewsProperty_ANNTF1 + "," + NewsProperty_WAPTF1 + "," + NewsProperty_JCTF1;
    }
    #endregion
    /// <summary>
    /// 检查目标栏目是否外部栏目
    /// </summary>
    /// <returns></returns>
    /// 
    protected bool CheckClass(string cid)
    {
        bool ckTF = false;
        int n = td.sel_newsclass(cid);
        if (n > 0) { ckTF = true; }
        else { ckTF = false; }
        return ckTF;
    }

    /// <summary>
    /// 更新属性
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pro_click(object sender, EventArgs e)
    {
        OriginalType = this.DdlType.SelectedValue;
        string Prostr = Newsty();
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
            td.updateNewsPro(Prostr, sOrgNews, 0);
            PageRight("更新成功", "news_list.aspx");
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
            string str = "";
            for (int l = 0; l < this.LstOriginal.Items.Count; l++)
            {
                if (this.LstOriginal.Items[l].Selected == true)
                {
                    str += "'" + this.LstOriginal.Items[l].Value + "',";
                }
            }
            string soclass = Foosun.Common.Public.Lost(str);
            td.updateNewsPro(Prostr, soclass, 1);
            PageRight("更新成功", "news_list.aspx");
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
    }
    /// <summary>
    /// 更新模板
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void templet_click(object sender, EventArgs e)
    {
        OriginalType = this.DdlType.SelectedValue;
        string templetstr = this.Templet.Text;
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
            td.updateNewstemplet(templetstr, sOrgNews, 0);
            PageRight("更新成功", "news_list.aspx");
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
            string str = "";
            for (int l = 0; l < this.LstOriginal.Items.Count; l++)
            {
                if (this.LstOriginal.Items[l].Selected == true)
                {
                    str += "'" + this.LstOriginal.Items[l].Value + "',";
                }
            }
            string soclass = Foosun.Common.Public.Lost(str);
            td.updateNewstemplet(templetstr, soclass, 1);
            PageRight("更新成功", "news_list.aspx");
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
    }
    /// <summary>
    /// 更新权重
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void order_click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(OrderIDDropDownList.SelectedValue))
        {
            PageError("权重值没有选择!", "News_List.aspx");
        }
        OriginalType = this.DdlType.SelectedValue;
        string orderstr = this.OrderIDDropDownList.SelectedValue;
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
            td.updateNewsOrder(orderstr, sOrgNews, 0);
            PageRight("更新成功", "news_list.aspx");
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
            string str = "";
            for (int l = 0; l < this.LstOriginal.Items.Count; l++)
            {
                if (this.LstOriginal.Items[l].Selected == true)
                {
                    str += "'" + this.LstOriginal.Items[l].Value + "',";
                }
            }
            string soclass = Foosun.Common.Public.Lost(str);
            td.updateNewsOrder(orderstr, soclass, 1);
            PageRight("更新成功", "news_list.aspx");
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
    }
    /// <summary>
    /// 更新评论连接　
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void comm_click(object sender, EventArgs e)
    {
        OriginalType = this.DdlType.SelectedValue;
        string commTF = "0";
        if (this.CommLinkTF.Checked)
        {
            commTF = "1";
        }
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
            td.updateNewsComm(commTF, sOrgNews, 0);
            PageRight("更新成功", "news_list.aspx");
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
            string str = "";
            for (int l = 0; l < this.LstOriginal.Items.Count; l++)
            {
                if (this.LstOriginal.Items[l].Selected == true)
                {
                    str += "'" + this.LstOriginal.Items[l].Value + "',";
                }
            }
            string soclass = Foosun.Common.Public.Lost(str);
            td.updateNewsComm(commTF, soclass, 1);
            PageRight("更新成功", "news_list.aspx");
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
    }
    /// <summary>
    /// 更新ＴＡＧ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tag_click(object sender, EventArgs e)
    {
        OriginalType = this.DdlType.SelectedValue;
        string tagstr = this.Tags.Text;
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
            td.updateNewsTAG(tagstr, sOrgNews, 0);
            PageRight("更新成功", "news_list.aspx");
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
            string str = "";
            for (int l = 0; l < this.LstOriginal.Items.Count; l++)
            {
                if (this.LstOriginal.Items[l].Selected == true)
                {
                    str += "'" + this.LstOriginal.Items[l].Value + "',";
                }
            }
            string soclass = Foosun.Common.Public.Lost(str);
            td.updateNewsTAG(tagstr, soclass, 1);
            PageRight("更新成功", "news_list.aspx");
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
    }
    /// <summary>
    /// 更新点击
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void click_click(object sender, EventArgs e)
    {
        bool isMatch =Regex.IsMatch(Click.Text.Trim(), @"\d+");
        if (string.IsNullOrEmpty(Click.Text.Trim()) || !isMatch)
        {
            PageError("点击数填写不正确!", "News_List.aspx");
            return;
        }
        OriginalType = this.DdlType.SelectedValue;
        string Clickstr = this.Click.Text;
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
            td.updateNewsClick(Clickstr, sOrgNews, 0);
            PageRight("更新成功", "news_list.aspx");
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
            string str = "";
            for (int l = 0; l < this.LstOriginal.Items.Count; l++)
            {
                if (this.LstOriginal.Items[l].Selected == true)
                {
                    str += "'" + this.LstOriginal.Items[l].Value + "',";
                }
            }
            string soclass = Foosun.Common.Public.Lost(str);
            td.updateNewsClick(Clickstr, soclass, 1);
            PageRight("更新成功", "news_list.aspx");
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
    }
    /// <summary>
    /// 更新来源
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void source_click(object sender, EventArgs e)
    {
        OriginalType = this.DdlType.SelectedValue;
        string Soucestr = this.Souce.Text;
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
            td.updateNewsSouce(Soucestr, sOrgNews, 0);
            PageRight("更新成功", "news_list.aspx");
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
            string str = "";
            for (int l = 0; l < this.LstOriginal.Items.Count; l++)
            {
                if (this.LstOriginal.Items[l].Selected == true)
                {
                    str += "'" + this.LstOriginal.Items[l].Value + "',";
                }
            }
            string soclass = Foosun.Common.Public.Lost(str);
            td.updateNewsSouce(Soucestr, soclass, 1);
            PageRight("更新成功", "news_list.aspx");
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
    }
    /// <summary>
    /// 更新扩展名
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void exname_click(object sender, EventArgs e)
    {
        OriginalType = this.DdlType.SelectedValue;
        string FileEXstr = this.FileEXName.SelectedValue;
        if (OriginalType.Equals("0"))//对选中的新闻进行操作
        {
            if (this.LstOriginal.Items.Count < 1)
            {
                PageError("没有要进行操作的新闻!", "News_List.aspx");
                return;
            }
            for (int i = 0; i < this.LstOriginal.Items.Count; i++)
            {
                if (i > 0) sOrgNews += ",";
                sOrgNews += "'" + this.LstOriginal.Items[i].Value + "'";
            }
            td.updateNewsFileEXstr(FileEXstr, sOrgNews, 0);
            PageRight("更新成功", "news_list.aspx");
        }
        else if (OriginalType.Equals("1"))//对选中的栏目进行操作
        {
            if (this.LstOriginal.SelectedValue.Trim().Equals(""))
            {
                PageError("请选择要进行操作的栏目!", "News_List.aspx");
                return;
            }
            string str = "";
            for (int l = 0; l < this.LstOriginal.Items.Count; l++)
            {
                if (this.LstOriginal.Items[l].Selected == true)
                {
                    str += "'" + this.LstOriginal.Items[l].Value + "',";
                }
            }
            string soclass = Foosun.Common.Public.Lost(str);
            td.updateNewsFileEXstr(FileEXstr, soclass, 1);
            PageRight("更新成功", "news_list.aspx");
        }
        else
        {
            PageError("错误的原始数据类型!", "News_List.aspx");
            return;
        }
    }
}
