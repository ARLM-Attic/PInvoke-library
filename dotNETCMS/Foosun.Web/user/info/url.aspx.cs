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

public partial class user_info_url : Foosun.Web.UI.UserPage
{
    Foosun.CMS.UserMisc rd = new Foosun.CMS.UserMisc();
    public string fURL = "";
    public string dirDumm = Foosun.Config.UIConfig.dirDumm;
    public string dirUser = Foosun.Config.UIConfig.dirUser;
    public string myUID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {
            
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            string tmpDir = "/";
            myUID = Foosun.Common.Input.EncryptString(Foosun.Global.Current.UserNum);
            if (dirDumm.Trim() != string.Empty) { tmpDir = "/" + dirDumm + "/"; }
            fURL = "http://" + Request.ServerVariables["SERVER_NAME"] + tmpDir + dirUser + "/url.aspx?uid=" + Foosun.Common.Input.EncryptString(Foosun.Global.Current.UserNum) + ""; ;
            URLClassList.InnerHtml = getClassList();
            if (Request.QueryString["action"] != null && Request.QueryString["action"] != string.Empty)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    rd.delURL(int.Parse(Request.QueryString["id"].ToString()));
                    PageRight("删除成功", "url.aspx");
                }
            }
            if (Request.QueryString["actionclass"] != null && Request.QueryString["actionclass"] != string.Empty)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    rd.delclass(int.Parse(Request.QueryString["id"].ToString()));
                    PageRight("删除成功", "url.aspx");
                }
            }
            StartLoad(1);
        }
    }

    protected string getClassList()
    {
        string ReturnList = "";
        DataTable dt = rd.getClassList(Foosun.Global.Current.UserNum);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ReturnList += "<a href=\"url.aspx?ClassID=" + dt.Rows[i]["id"].ToString() + "\" class=\"list_link\">" + dt.Rows[i]["ClassName"].ToString() + "</a>&nbsp;(<a class=\"list_link\" href=\"url_class.aspx?id=" + dt.Rows[i]["Id"].ToString() + "\">修</a>&nbsp;<a onclick=\"{if (confirm('你确认删除此记录吗?')){return true;}return false;}\" class=\"list_link\" href=\"url.aspx?actionclass=del&id=" + dt.Rows[i]["Id"].ToString() + "\">删</a>)&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            dt.Clear(); dt.Dispose();
        }
        return ReturnList;
    }

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }

    protected void StartLoad(int PageIndex)
    {
        int i, j;
        DataTable dt = null;
        if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"] != string.Empty)
        {
            SQLConditionInfo sts = new SQLConditionInfo("@ClassID", int.Parse(Request.QueryString["ClassID"].ToString()));
            dt = Foosun.CMS.Pagination.GetPage("user_info_url_1_aspx", PageIndex, 20, out i, out j, sts);
        }
        else
        {
            dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, null);
        }
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        no.InnerHtml = "";
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //----------------------------------------添加列------------------------------------------------
                dt.Columns.Add("URLNames", typeof(string));
                dt.Columns.Add("URLs", typeof(string));
                dt.Columns.Add("op", typeof(string));

                //----------------------------------------添加列结束--------------------------------------------
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    string URLs = dt.Rows[k]["URL"].ToString();
                    if (URLs.IndexOf("http://") >= 0)
                    {
                        dt.Rows[k]["URLs"] = URLs;
                    }
                    else
                    {
                        dt.Rows[k]["URLs"] = "http://" + URLs;
                    }
                    string URLColor = dt.Rows[k]["URLColor"].ToString();
                    if (URLColor.Trim() != string.Empty && URLColor != null)
                    {
                        dt.Rows[k]["URLNames"] = "<a title=\"" + dt.Rows[k]["Content"].ToString() + "\" href=\"" + dt.Rows[k]["URLs"] + "\" target=\"_blank\"><font color=" + dt.Rows[k]["URLColor"].ToString() + ">" + dt.Rows[k]["URLName"].ToString() + "</font></a>";
                    }
                    else
                    {
                        dt.Rows[k]["URLNames"] = "<a title=\"" + dt.Rows[k]["Content"].ToString() + "\" href=\"" + dt.Rows[k]["URLs"] + "\" target=\"_blank\" class=\"list_link\">" + dt.Rows[k]["URLName"].ToString() + "</a>";
                    }
                    dt.Rows[k]["op"] = "<a href=\"url_add.aspx?id=" + dt.Rows[k]["id"] + "\" class=\"list_link\">修改</a>&nbsp;┊&nbsp;<a href=\"url.aspx?id=" + dt.Rows[k]["id"] + "&action=del\" onclick=\"{if (confirm('你确认删除此记录吗?')){return true;}return false;}\" class=\"list_link\">删除</a>";
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
        nos = nos + "<td class='navi_link'>没有记录</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        DataList1.Visible = false;
        return nos;
    }

}
