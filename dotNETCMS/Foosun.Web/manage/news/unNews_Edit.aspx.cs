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
using System.Drawing;
using Foosun.CMS;

public partial class manage_news_unNews_Edit : Foosun.Web.UI.ManagePage
{
    public manage_news_unNews_Edit()
    {
        Authority_Code = "C050";
    }
    ContentManage nws = new ContentManage();
    protected String UnNewsJsArray = "";
    protected String TopLineArray = "";
    protected String unNewsid = "";
    protected String FamilyArray = "";
    protected String FontStyleArray = "";
    protected String fs_PicInfo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["UnID"] != null)
        {
            unNewsid = Request.QueryString["UnID"];
        }
        else if (Request.Form["UnID"] != null)
        {
            unNewsid = Request.Form["UnID"];
        }
        if (unNewsid == null)
            unNewsid = "";
        if (!IsPostBack)
        {
            GetunNewsData();
        }
        else
        {
            SaveunNewsData();
        }
    }

    protected void GetunNewsData()
    {
        String For_string;
        int For_number;
        if (unNewsid != "")
        {
            #region 编辑不规则新闻
            unNewsid = Foosun.Common.Input.Filter(unNewsid);
            DataTable DT = nws.sel(unNewsid);
            if (DT != null && DT.Rows.Count > 0)
            {
                DataTable DTNews = null;
                this.unName.Text = DT.Rows[0]["unName"].ToString();
                this.titleCSS.Text = DT.Rows[0]["titleCSS"].ToString();
                for (For_number = 0; For_number < DT.Rows.Count; For_number++)
                {
                    DTNews = nws.sel_DTNews(DT.Rows[For_number]["NewsTable"].ToString(), DT.Rows[For_number]["ONewsID"].ToString());
                    if (DTNews != null && DTNews.Rows.Count > 0)
                    {
                        For_string = "'" + DT.Rows[For_number]["ONewsID"] + "','" + DTNews.Rows[0][0] + "','" + DT.Rows[For_number]["unTitle"] + "'," + DT.Rows[For_number]["Rows"] + ",'" + DT.Rows[For_number]["NewsTable"] + "','" + DT.Rows[For_number]["SubCSS"] + "'";
                        For_string = "[" + For_string + "]";
                        if (UnNewsJsArray == "")
                        {
                            UnNewsJsArray = For_string;
                        }
                        else
                        {
                            UnNewsJsArray += "," + For_string;
                        }
                    }
                }

                if (DTNews != null)
                    DTNews.Dispose();
                DT.Dispose();

                if (UnNewsJsArray != "")
                {
                    UnNewsJsArray = "[" + UnNewsJsArray + "]";
                }
                else
                {
                    UnNewsJsArray = "new Array()";
                }
                if (TopLineArray == "")
                {
                    TopLineArray = "new Array()";
                }
            }
            else
            {
                PageError("找不到记录!","");
            }
            #endregion 编辑不规则新闻
        }
        else
        {
            unNewsid = "";
            UnNewsJsArray = "new Array()";
            TopLineArray = "new Array()";
        }
        #region 初始化字体列表
        FontFamily[] ArrFontFamily = FontFamily.Families;
        foreach (FontFamily Familys in ArrFontFamily)
        {
            if (FamilyArray == "")
            {
                FamilyArray = "'" + Familys.Name + "'";
            }
            else
            {
                FamilyArray += ",'" + Familys.Name + "'";
            }
        }
        if (FamilyArray != "")
        {
            FamilyArray = "[" + FamilyArray + "]";
        }
        else
        {
            FamilyArray = "new Array()";
        }
        #endregion
        #region 初始化字体样式
        foreach (int Item in Enum.GetValues(typeof(FontStyle)))
        {
            if (FontStyleArray == "")
            {
                FontStyleArray = Enum.GetName(typeof(System.Drawing.FontStyle), Item) + ":" + Item.ToString();
            }
            else
            {
                FontStyleArray += "," + Enum.GetName(typeof(System.Drawing.FontStyle), Item) + ":" + Item.ToString();
            }
        }
        FontStyleArray = "{" + FontStyleArray + "}";
        #endregion
    }

    //保存数据
    protected bool SaveunNewsData()
    {
        String OldNewsId = Foosun.Common.Input.Filter(Request.Form["NewsID"]);
        String[] Arr_OldNewsId;
        String NewsID, NewsTitle, NewsRow, NewsTable,SubCSS;

        NewsID = Request.Form["TopNewsID"];
        string unName = Foosun.Common.Input.Filter(this.unName.Text);
        string titleCSS = this.titleCSS.Text;
        #region 判断数据是否合法
        if (this.unName.Text.Trim() == "")
        {
            PageError("请填写不规则的标题", "");
        }
        if (NewsID == null && OldNewsId == null)
        {
            PageError("不规则新闻为空", "unNews.aspx");
            return false;
        }
        #endregion 判断数据是否合法

        #region 获取普通新闻数据
        if (OldNewsId != null)
        {
            OldNewsId = OldNewsId.Replace(" ", "");
            Arr_OldNewsId = OldNewsId.Split(',');
        }
        else
        {
            OldNewsId = "";
            Arr_OldNewsId = OldNewsId.Split(new char[] { ',' });
        }
        string unNewsids = Foosun.Common.Rand.Number(12);
        if (Request.Form["UnID"].Trim() != "")
        {
            unNewsids = Request.Form["UnID"];
            nws.delUnID(Request.Form["UnID"]);
        }
        for (int For_Num = 0; For_Num < Arr_OldNewsId.Length; For_Num++)
        {
            NewsTitle = Foosun.Common.Input.Filter(Request.Form["NewsTitle" + Arr_OldNewsId[For_Num]]);
            NewsRow = Foosun.Common.Input.Filter(Request.Form["Row" + Arr_OldNewsId[For_Num]]);
            NewsTable = Foosun.Common.Input.Filter(Request.Form["NewsTable" + Arr_OldNewsId[For_Num]]);
            SubCSS = Foosun.Common.Input.Filter(Request.Form["SubCSS" + Arr_OldNewsId[For_Num]]);
            if (nws.Add_2(unName, titleCSS, SubCSS, unNewsids, Arr_OldNewsId[For_Num], NewsRow, NewsTitle, NewsTable, SiteID) == 0)
            {
                PageError("保存不规则新闻失败!", "unNews.aspx");
            }
        }
        PageRight("保存不规则新闻成功!", "unNews.aspx");
        #endregion
        return true;
    }
}