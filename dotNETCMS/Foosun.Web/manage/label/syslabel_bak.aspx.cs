///************************************************************************************************************
///**********标签备份库Code By DengXi**************************************************************************
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

public partial class manage_label_syslabel_bak : Foosun.Web.UI.ManagePage
{
    public manage_label_syslabel_bak()
    {
        Authority_Code = "T015";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
        }

        string str_Op = Request.QueryString["Op"];
        if (str_Op != "" && str_Op != null && str_Op != string.Empty)
        {
            string str_ID = Request.QueryString["LabelID"];
            Rec(Foosun.Common.Input.checkID(str_ID));
        }
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        StartLoad(1);
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <returns>分页</returns>
    /// Code By DengXi 

    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }

    protected void StartLoad(int PageIndex)
    {
        int i, j;

        Foosun.Model.SQLConditionInfo st = new Foosun.Model.SQLConditionInfo("@SiteID", SiteID);

        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, st);

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //----------------------------------------添加列------------------------------------------------
                dt.Columns.Add("Op", typeof(string));
                for (int k = 0; dt.Rows.Count > k; k++)
                {
                    dt.Rows[k]["Op"] = "<a href=\"javascript:Rec('" + dt.Rows[k]["LabelID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/folder/bak.gif\" border=\"0\" alt=\"恢复标签\" /></a><a href=\"javascript:Update('Label','" + dt.Rows[k]["LabelID"].ToString() + "');\" class='list_link'><img src=\"../../sysImages/folder/re.gif\" border=\"0\" alt=\"修改\" /></a>";
                }
            }
            DataList1.DataSource = dt;                              //设置datalist数据源
            DataList1.DataBind();                                   //绑定数据源
            dt.Clear();
            dt.Dispose();
        }
    }

    /// <summary>
    /// 恢复标签
    /// </summary>
    /// <param name="ID">编号</param>
    /// <returns>恢复标签</returns>
    /// 编写时间2007-04-24   Code By DengXi


    protected void Rec(string ID)
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        lc.LabelToResume(ID);
        PageRight("从备份库中恢复标签成功!", "SysLabel_List.aspx");
    }
}
