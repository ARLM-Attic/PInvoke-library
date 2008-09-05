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

public partial class manage_publish_psf : Foosun.Web.UI.ManagePage
{
    Psframe rd = new Psframe();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        #endregion
        if (!IsPostBack)
        {
            PageError("此版本没有此功能","");
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            copyright.InnerHtml = CopyRight;
            PsfManage(1);
        }
        string type = Request.QueryString["type"];
        switch (type)
        {
            case "del":
                Del_PSF();
                break;
            case "delall":
                DelAll_PSF();
                break;
        }
    }
    #region page
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        PsfManage(PageIndex);
    }

    /// <summary>
    /// 显示接点管理页
    /// </summary>
    /// Code By ChenZhaohui

    protected void PsfManage(int PageIndex)//显示管理页面
    {
        int i, j;
        DataTable dt = null;
        dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, PAGESIZE, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("oPerate", typeof(String));//操作
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    string psfID = dt.Rows[k]["psfID"].ToString();

                    #region
                    dt.Rows[k]["psfName"] = "<a class=\"list_link\"  href=\"psf_edit.aspx?psfid=" + psfID + "\" title=\"点击查看详情或修改\">" + dt.Rows[k]["psfName"].ToString() + "</a>";
                    #endregion
                    dt.Rows[k]["oPerate"] = "<a class=\"list_link\"  href=\"psf_edit.aspx?psfid=" + psfID + "\" title=\"点击查看详情或修改\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/edit.gif\" border=\"0\" alt=\"修改此项\" /></a><a class=\"list_link\" href=\"?type=del&psfid=" + psfID + "\" title=\"点击删除\" onclick=\"{if(confirm('确认删除吗？')){return true;}return false;}\"><img src=\"../../sysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/del.gif\" border=\"0\" alt=\"删除此项\" /></a><input type='checkbox' name='psf_checkbox' id='psf_checkbox' value=\"" + psfID + "\"/>";
                }
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
            else
            {
                NoContent.InnerHtml = Show_NoContent();
                this.PageNavigator1.Visible = false;
            }
        }
        else
        {
            NoContent.InnerHtml = Show_NoContent();
            this.PageNavigator1.Visible = false;
        }
    }
    #endregion

    /// <summary>
    /// 提示没记录显示
    /// </summary>
    /// <returns>取得无记录显示信息</returns>
    /// Code By ChenZhaohui

    #region noshow
    string Show_NoContent()
    {

        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>当前没有记录！</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
    #endregion

    /// <summary>
    /// 删除单个接点
    /// </summary>
    /// Code By ChenZhaohui

    #region del
    protected void Del_PSF()
    {
        string psfId = Request.QueryString["psfid"];
        if (psfId == null || psfId == "" || psfId == string.Empty)
        {
            PageError("参数错误", "psf.aspx");
        }
        else
        {
            rd.Del_PSF(psfId);
            pd.SaveUserAdminLogs(1, 1, UserNum, "删除单个PSF接点", "删除成功,保存在回收站中.ID:" + psfId + "");
            PageRight("删除成功,保存在回收站中", "psf.aspx");

        }
    }
    #endregion

    /// <summary>
    /// 删除所有接点
    /// </summary>
    /// Code By ChenZhaohui

    #region DelAll
    protected void DelAll_PSF()
    {
        rd.DelAll_PSF();
        pd.SaveUserAdminLogs(1, 1, UserNum, "删除所有psf接点", "删除成功,保存在回收站中");
        PageRight("删除所有PSF成功,保存在回收站中", "psf.aspx");
    }
    #endregion

    /// <summary>
    /// 批量删除接点
    /// </summary>
    /// Code By ChenZhaohui

    #region DelP
    protected void DelP_Click(object sender, EventArgs e)
    {
        string psf_checkbox = Request.Form["psf_checkbox"];
        if (psf_checkbox == null || psf_checkbox == String.Empty)
        {
            PageError("请先选择批量操作的内容!", "");
        }
        else
        {
            String[] CheckboxArray = psf_checkbox.Split(',');
            psf_checkbox = null;
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                rd.Del_PSF(CheckboxArray[i]);
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "批量删除psf接点", "删除成功,保存在回收站中.ID:" + psf_checkbox + "");
            PageRight("删除数据成功,保存在回收站中,请返回继续操作!", "psf.aspx");
        }
    }
    #endregion
}
