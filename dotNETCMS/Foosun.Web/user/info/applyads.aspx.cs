///************************************************************************************************************
///**********申请广告,Code By DengXi***************************************************************************
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
using Foosun.Model;

public partial class user_info_applyads : Foosun.Web.UI.UserPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            StartLoad(1);
        }
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
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
        SQLConditionInfo st = new SQLConditionInfo("@CusID", Foosun.Global.Current.UserNum);
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, st);

        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            DataList1.DataSource = dt;                              //设置datalist数据源
            DataList1.DataBind();                                   //绑定数据源
            dt.Clear();
            dt.Dispose();
        }
    }

    /// <summary>
    /// 获得广告类型
    /// </summary>
    /// <param name="type">需要返回的广告类型</param>
    /// <returns>返回广告类型</returns>
    /// Code By DengXi

    protected string GetAdsType(string type)
    {
        string str_Type = "";
        switch (type)
        {
            case "0":
                str_Type = "显示广告";
                break;
            case "1":
                str_Type = "弹出新窗口";
                break;
            case "2":
                str_Type = "打开新窗口";
                break;
            case "3":
                str_Type = "渐隐消失";
                break;
            case "4":
                str_Type = "网页对话框";
                break;
            case "5":
                str_Type = "透明对话框";
                break;
            case "6":
                str_Type = "满屏浮动";
                break;
            case "7":
                str_Type = "左下底端";
                break;
            case "8":
                str_Type = "右下底端";
                break;
            case "9":
                str_Type = "对联广告";
                break;
            case "10":
                str_Type = "循环广告";
                break;
            case "11":
                str_Type = "文字广告";
                break;
        }
        return str_Type;
    }

    /// <summary>
    /// 获取广告是否被锁定
    /// </summary>
    /// <param name="type"></param>
    /// <returns>如果为0则为正常否则为锁定</returns>

    protected string GetAdsMode(string type)
    {
        string str_Temp = "";
        if (type == "0")
            str_Temp = "正常";
        else
            str_Temp = "<font color=\"red\">锁定</font>";
        return str_Temp;
    }
}
