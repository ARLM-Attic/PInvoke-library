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

public partial class manage_news_SortPage : Foosun.Web.UI.ManagePage
{
    ContentManage rd = new ContentManage();
    rootPublic pd = new rootPublic();
    DataTable dtDate = null;
    //加载函数
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        string action = Request.QueryString["Acton"];
        if (!IsPostBack)
        {

            switch (action)
            {
                case "More":
                    StartLoad(1);
                    break;
                case "unite":
                    this.Authority_Code = "C025";
                    this.CheckAdminAuthority();
                    LandCheand();
                    Randsize.Value = "unite";
                    Btc.Text = "确定合并栏目";
                    ExprText.Text = "合并数据到>>>";
                    break;
                case "allmove":
                    this.Authority_Code = "C026";
                    this.CheckAdminAuthority();
                    LandCheand();
                    Randsize.Value = "allmove";
                    Btc.Text = "确定转移栏目";
                    ExprText.Text = "转移数据到>>>";
                    break;
                default:
                    PageError("参数错误,请正确操作!", "SortPage.aspx");
                    break;
            }
        }
    }

    //分页处理
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        StartLoad(PageIndex);
    }

    //数据初始化
    protected void StartLoad(int PageIndex)
    {
        int i, j;
        string _tmp = "";
        string _tmp1 = "";
        DataTable dt = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out i, out j, null);
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;
        for (int k = 0; k < dt.Rows.Count; k++)
        {
            _tmp = "┝ " + dt.Rows[k]["ClassCName"] + "";
            DataTable dts = rd.getChildList(dt.Rows[k]["ClassID"].ToString());
            string sign = " ┉ ";
            if (dts.Rows.Count > 0)
            {
                for (int m = 0; m < dts.Rows.Count; m++)
                {
                    _tmp1 = "" + sign + dts.Rows[m]["ClassCName"] + "";
                    dt.Rows[k]["ClassCName"] = _tmp1;
                }
                dts.Clear(); dts.Dispose();
            }
            dt.Rows[k]["ClassCName"] = _tmp;
        }
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }

    //复位操作
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        rd.resetClass();
        pd.SaveUserAdminLogs(1, 1, UserNum, "复位所有栏目", "复位所有栏目");
        PageRight("操作成功,此操作对锁定栏目以及回收站里栏目无效!", "Class_list.aspx");
    }

    //一级排序操作
    protected void FirsSort_Click(object sender, EventArgs e)
    {
        Response.Redirect("SortPage.aspx?Acton=First");
    }

    //
    protected void DataList1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        TextBox tb = (TextBox)e.Item.FindControl("TextBox1");
        HiddenField tbv = (HiddenField)e.Item.FindControl("HiddNum");
        int nValue = int.Parse(tb.Text);
        rd.resetOrder(nValue, tbv.Value);
        Button bt = (Button)e.Item.FindControl("Button1");
        StartLoad(1);
        bt.Text = "权重更改成功!";
    }


    protected void LandCheand()
    {
        //隐藏分页控件
        PageNavigator1.Visible = false;
        Page.RegisterStartupScript("foosun", "<script>document.getElementById(\"uniteTable\").style.display=\"\";</script>");
        //初始化源栏目控件
        dtDate = rd.getSouceClass();
        if (dtDate.Rows.Count > 0)
            DdlParentBound("0", 0);
        dtDate.Clear();
        dtDate.Dispose();
    }

    /// <summary>
    /// 类栏递归
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="Layer"></param>
    private void DdlParentBound(string PID, int Layer)
    {
        DataRow[] row = null;
        row = dtDate.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                string strValue = "";
                string strText = "";
                if (r["ParentID"].ToString() != "0")
                {
                    strText = "┝";
                }

                for (int j = 0; j < Layer; j++)
                {
                    strText += " ┉ ";
                }
                strText += " " + r["ClassCName"].ToString();
                strValue = r["ClassID"].ToString().Trim();
                ListItem itm = new ListItem();
                itm.Value = strValue;
                itm.Text = strText;
                this.SourceClassID.Items.Add(itm);
                this.TargetClassID.Items.Add(itm);
                DdlParentBound(r["ClassID"].ToString(), Layer + 1);
            }
        }
    }

    /// <summary>
    /// 合并栏目
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btc_Click(object sender, EventArgs e)
    {
        String HiddSize = Randsize.Value;

        //源栏目值
        string Source_ = SourceClassID.SelectedItem.Value;
        //目标栏目值
        string Target_ = TargetClassID.SelectedItem.Value;
        if (Source_ == Target_)
            PageError("源栏目与目标栏目相同,<font color=red>操作失败!</font>", "");

        if (HiddSize == "unite")
        {
            if (Source_ == null || Target_ == null || Source_ == "" || Target_ == "")
            {
                PageError("合并栏目出现异常错误!", "SortPage.aspx?Acton=unite");
            }
            else
            {
                //删除源栏目
                rd.delSouce(Source_);
                rd.updateSouce(Source_, Target_);
                pd.SaveUserAdminLogs(1, 1, UserNum, "合并栏目", "合并栏目,源：" + Source_ + ",目标：" + Target_ + "");
                PageRight("合并数据成功", "SortPage.aspx?Acton=unite");
            }
        }
        else
        {
            if (Source_ == null || Target_ == null || Source_ == "" || Target_ == "")
            {
                PageError("转移栏目出现异常错误!", "SortPage.aspx?Acton=allmove");
            }
            else
            {
                //更改源栏目父ID
                rd.changeParent(Source_, Target_);
                //更改新闻表源栏目数据到目标栏目
                //rd.updateSouce1(Source_, Target_);
                pd.SaveUserAdminLogs(1, 1, UserNum, "转移栏目", "转移栏目,源：" + Source_ + ",目标：" + Target_ + "");
                PageRight("数据转移成功!", "SortPage.aspx?Acton=allmove");
            }

        }

    }
}
