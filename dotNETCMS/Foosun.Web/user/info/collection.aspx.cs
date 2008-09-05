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
using Foosun.Model;

public partial class user_info_collection : Foosun.Web.UI.UserPage
{
    Info inf = new Info();
    Mycom rd = new Mycom();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        string APIID = "";
        if (Request.QueryString["APIID"]!=null && Request.QueryString["APIID"]!="")
        {
            APIID = Foosun.Common.Input.Filter(Request.QueryString["APIID"]);
        }
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            
        }
        StartLoad(1, APIID);
        string Type = Request.QueryString["Type"];  //取得操作类型
        string _ChID = Request.QueryString["ChID"] == null ? "" : Request.QueryString["ChID"];
        string[] paramList = _ChID.Split('|');
        if (!string.IsNullOrEmpty(_ChID))
        {
            if (_ChID.Split('|').Length >= 3)
                _ChID = paramList.Length < 3 ? "" : paramList[0];
        }
        if (string.IsNullOrEmpty(Type))
        {
            Type = paramList.Length < 3 ? "" : paramList[1];
        }
        string ID = null;
        if(Request.QueryString["ID"]!=null&&Request.QueryString["ID"]!="")
        {
            ID = Foosun.Common.Input.Filter(Request.QueryString["ID"]);  //取得需要操作的稿件ID
        }
        int ChID = 0;
        if (_ChID != null && _ChID != string.Empty && Foosun.Common.Input.IsInteger(_ChID))
        {
            ChID = int.Parse(_ChID);
        }
        switch (Type)
        {
            case "del":          //删除
                del(ID);
                break;
            case "PDel":            //批量删除
                PDel();
                break;
            case "Add":
                string ids = Request.QueryString["id"];
                if (string.IsNullOrEmpty(ids))
                {
                    ids = paramList.Length < 3 ? "" : paramList[2];
                }
                AddTo(ids, ChID);
                break;
            default:
                break;
        }

    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        string APIID = null;
        if (Request.QueryString["APIID"] != null && Request.QueryString["APIID"] != "")
        {
            APIID = Foosun.Common.Input.Filter(Request.QueryString["APIID"]);
        }     
        StartLoad(PageIndex, APIID);
    }

    protected void StartLoad(int PageIndex,string Api)
    {
        int i, j; 
        DataTable dt = null;
        if (Api != "0" && Api != null&& Api != "")
        {
            dt = Foosun.CMS.Pagination.GetPage("user_info_collection_1_aspx", PageIndex, 10, out i, out j, null);
        }
        SQLConditionInfo sts = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
        dt = Foosun.CMS.Pagination.GetPage("user_info_collection_2_aspx", PageIndex, 10, out i, out j, sts);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        no.InnerHtml = "";
        if (dt != null&&dt.Rows.Count > 0)
        {
            dt.Columns.Add("Operation", typeof(string));
            dt.Columns.Add("titleUrl", typeof(string));
            foreach (DataRow h in dt.Rows)
            {
                if (h["ChID"].ToString() != "0")
                {
                    Channel frd = new Channel();
                    h["titleUrl"] = frd.getfUrl(int.Parse(h["FID"].ToString()), int.Parse(h["ChID"].ToString()));
                }
                else
                {
                    h["titleUrl"] = rd.sel_2(h["FID"].ToString(), "" + Foosun.Config.UIConfig.dataRe + "news");
                }
                h["Operation"] = "<a href=\"javascript:del('" + h["FID"].ToString() + "');\" class=\"list_link\">删除</a>┆<input name=\"Checkbox1\" type=\"checkbox\" value=\"" + h["ID"].ToString() + "\" /></td>";
            }
            DataList1.Visible = true;
        }      
        else
        {
            no.InnerHtml = show_no();
            sc.InnerHtml = Show_sc();
            this.PageNavigator1.Visible = false;
        }
        DataList1.DataSource = dt;
        DataList1.DataBind();
        DataList1.Dispose();
        sc.InnerHtml = Show_scs();
    }
    string show_no()
    {
        
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        DataList1.Visible = false;
        return nos;
    }

    protected void AddTo(string NewsID,int ChID)
    {
        if (inf.addTo(NewsID,ChID))
        {
            PageRight("收藏成功", "collection.aspx");
        }
        else
        {
            PageError("您已经收藏了此信息", "collection.aspx");
        }
    }

    protected void PDel()
    {
        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的收藏!", "collection.aspx");
        }
        else
        {
            string[] chSplit = checkboxq.Split(',');
            for (int i = 0; i < chSplit.Length; i++)
            {
                if (chSplit[i] != "on")
                {
                    if (inf.Delete(chSplit[i]) == 0)
                    {
                        PageError("批量删除失败,可能你没有选择", "collection.aspx");
                        break;
                    }
                }
            }
            PageRight("批量删除成功", "collection.aspx");
        }

    }
    protected void del(string ID)
    {
        if (inf.Delete(ID) == 0)
        {
            PageError("批量删除失败", "collection.aspx");
        }
        else
        {
            PageRight("删除成功!", "collection.aspx");
        }
    }
    string Show_scs()
    {
        string scs = "<a href=\"javascript:API(0);\" class=\"topnavichar\">全部收藏</a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>";
        return scs;
    }
    string Show_sc()
    {
        string sc = "<a href=\"javascript:API();\" class=\"topnavichar\">全部收藏</a>";
        return sc;
    }
}
