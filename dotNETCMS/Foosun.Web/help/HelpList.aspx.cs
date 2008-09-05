///************************************************************************************************************
///**********显示帮助信息列表,Code By DengXi*******************************************************************
///************************************************************************************************************
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

public partial class help_HelpList : Foosun.Web.UI.BasePage
{
    Help help = new Help();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        string Type = Request.QueryString["Type"];
        if (!IsPostBack)
        {
            if (Type == "Del")
            {
                string HelpID = Request.QueryString["HelpID"];

                if (HelpID == "" && HelpID == null && HelpID == string.Empty)
                {
                    PageError("参数错误", "");
                }
                else
                {
                    int ID = int.Parse(HelpID);
                    DelHelp(ID);
                }
            }
            Response.CacheControl = "no-cache";
            StartLoad(1,null);
        }
    }
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        string _helpID = Request.Form["HelpID"];
        StartLoad(PageIndex, _helpID);
    }

    protected void StartLoad(int PageIndex, string _helpID)
    {
        int i, j;
        DataTable dt = help.GetPage(_helpID,PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        
        if (dt.Rows.Count > 0)
        { 
            dt.Columns.Add("idc", typeof(string));
            for (int k = 0; dt.Rows.Count > k; k++)
            {
                dt.Rows[k]["idc"] = "<a href=HelpAdd.aspx?Action=Edit&id=" + dt.Rows[k]["id"].ToString() + " class=\"list_link\">修改</a>┊<a href='?Type=Del&HelpID=" + dt.Rows[k]["id"].ToString() + "' class='list_link'>删除</a>";
            }
        }
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }

    protected void DelHelp(int ID)
    {
        if (help.Str_DelSql(ID) != 0)
        {
            PageRight("删除成功", "HelpList.aspx");
        }
        else
        {
            PageError("删除失败", "HelpList.aspx");
        }
    }

    protected void Button8_ServerClick(object sender, EventArgs e)
    {
        string _helpID = Request.Form["HelpID"];
        StartLoad(1, _helpID);
    }
}
