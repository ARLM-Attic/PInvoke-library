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
using Foosun.Web.UI;
using Foosun.CMS;
using Foosun.Model;

public partial class configuration_system_Genlist : Foosun.Web.UI.DialogPage
{
    public configuration_system_Genlist()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        if (!IsPostBack)
        {
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
        int i = 0;
        int j = 0;

        string _type = Request.QueryString["type"];
        int _tmpstr = 0;
        switch (_type)
        { 
            case "Souce":
                _tmpstr = 1;
                break;
            case "Author":
                _tmpstr = 2;
                break;
            default:
                _tmpstr = 0;
                break;
        }
        DataTable dt = null;
        if (_type != null && _type != "")
        {
            SQLConditionInfo st = new SQLConditionInfo("@gType",_tmpstr);
            dt = Foosun.CMS.Pagination.GetPage("configuration_system_Genlist_1_aspx", PageIndex, 40, out i, out j, st);
        }
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("op", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    if (_tmpstr == 0)
                    {
                        dt.Rows[k]["op"] = "<a class=\"helpstyle\" href=\"#\" onclick=\"ReturnValue('" + dt.Rows[k]["Cname"].ToString() + "');\">" + dt.Rows[k]["Cname"] + "</a>&nbsp;&nbsp;";
                    }
                    else
                    {
                        dt.Rows[k]["op"] = "<a class=\"helpstyle\" href=\"#\" onclick=\"ReturnValue('" + dt.Rows[k]["Cname"].ToString() + "');\">" + dt.Rows[k]["Cname"] + "</a>&nbsp;&nbsp;";
                    }
                }
            }
        }
        gList.DataSource = dt;                              //设置datalist数据源
        gList.DataBind();                                   //绑定数据源
    }

    protected void DataList1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}
