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

public partial class user_info_announce : Foosun.Web.UI.UserPage
{
    Info inf = new Info();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {
            
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            StartLoad(1);
        } 
    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }

    protected void StartLoad(int PageIndex)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        string[] UserGI = pd.getGIPoint(UserNum).Split('|');
        int cPoint = pd.getcPoint(UserNum);
        string SiteID = inf.sel_1(UserNum);
        int i, j;
        SQLConditionInfo sts = new SQLConditionInfo("@SiteID", SiteID);
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 10, out i, out j, sts);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        no.InnerHtml = "";
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //----------------------------------------添加列------------------------------------------------
                dt.Columns.Add("titles", typeof(string));
                dt.Columns.Add("contents", typeof(string));
                dt.Columns.Add("creatTimes", typeof(string));

                //----------------------------------------添加列结束--------------------------------------------
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    string gGnumber =dt.Rows[k]["GroupNumber"].ToString();
                    if (gGnumber.Trim() != "")
                    {
                        if (gGnumber.ToUpper() != pd.getUserGroupNumber(Foosun.Global.Current.UserNum).ToUpper())
                        {
                            continue;
                        }
                    }
                    else
                    {
                        string tmss = dt.Rows[k]["getPoint"].ToString();
                        if (tmss != "0|0|0")
                        {
                            if (tmss.Trim() == null && tmss.Trim() == "")
                            { 
                                tmss = "0|0|0";
                            }
                            string[] _tmp = tmss.Split('|');
                            if (int.Parse(_tmp[0]) > int.Parse(UserGI[0]))
                            {
                                continue;
                            }
                            else
                            {
                                dt.Rows[k]["titles"] = dt.Rows[k]["title"].ToString();
                                dt.Rows[k]["contents"] = dt.Rows[k]["Content"].ToString();
                                dt.Rows[k]["creatTimes"] = dt.Rows[k]["creatTime"].ToString();
                            }
                            if (int.Parse(_tmp[1]) > int.Parse(UserGI[1]))
                            {
                                continue;
                            }
                            else
                            {
                                dt.Rows[k]["titles"] = dt.Rows[k]["title"].ToString();
                                dt.Rows[k]["contents"] = dt.Rows[k]["Content"].ToString();
                                dt.Rows[k]["creatTimes"] = dt.Rows[k]["creatTime"].ToString();
                            }

                            if (int.Parse(_tmp[2]) > cPoint)
                            {
                                continue;
                            }
                            else
                            {
                                dt.Rows[k]["titles"] = dt.Rows[k]["title"].ToString();
                                dt.Rows[k]["contents"] = dt.Rows[k]["Content"].ToString();
                                dt.Rows[k]["creatTimes"] = dt.Rows[k]["creatTime"].ToString();
                            }                            
                        }
                        else
                        {
                            dt.Rows[k]["titles"] = dt.Rows[k]["title"].ToString();
                            dt.Rows[k]["contents"] = dt.Rows[k]["Content"].ToString();
                            dt.Rows[k]["creatTimes"] = dt.Rows[k]["creatTime"].ToString();
                        }
                    }
                }
                DataList1.Visible = true;
            }
            else
            {
                no.InnerHtml = show_no();
                this.PageNavigator1.Visible = false;
            }
            DataList1.DataSource = dt;                              //设置datalist数据源
            DataList1.DataBind();
            DataList1.Dispose();
        }
    }

    string show_no()
    {
        
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有公告</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        DataList1.Visible = false;
        return nos;
    }
}