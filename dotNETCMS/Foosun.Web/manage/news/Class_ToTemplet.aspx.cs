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

public partial class manage_news_Class_ToTemplet : Foosun.Web.UI.ManagePage
{
    public manage_news_Class_ToTemplet()
    {
        Authority_Code = "C028";
    }
    ContentManage rd = new ContentManage();
    rootPublic pd = new rootPublic();
    DataTable dt = new DataTable();
    public string DirHtml = Foosun.Config.UIConfig.dirHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (SiteID != "0")
            { 
                DirHtml = Foosun.Config.UIConfig.dirSite; 
            }
            this.Itemtemplets.Text = rd.GetParamBase("ClasslistTemplet");
            this.displaytemplets.Text = rd.GetParamBase("ReadNewsTemplet");
            DataListTrivee();
        }
    }

    protected void DataListTrivee()
    {
        dt = rd.getClassInfo_Templet();
        if (dt.Rows.Count > 0)
        {
            HistoryData("0", 0);
        }
    }

    /// <summary>
    /// 栏目递归处理
    /// </summary>
    /// <param name="dat"></param>
    /// <param name="div"></param>
    protected void HistoryData(string dat, int div)
    {
        DataRow[] dr = null;
        dr = dt.Select("ParentID='" + dat + "'");
        if (dr.Length < 1)
            return;
        else
        {
            string strText = null;
            foreach (DataRow row in dr)
            {
                this.ClassID = row["ClassID"].ToString() ;
                if (this.CheckAuthority())
                {
                    string strValue = "";
                    if (row["ParentID"].ToString() == "0")
                        strText = "";
                    else
                        strText = "├";

                    for (int j = 0; j < div; j++)
                    {
                        strText += "─";
                    }
                    strText += " " + row["ClassCname"].ToString();
                    strValue += row["ClassID"].ToString();
                    ListItem item = new ListItem();
                    item.Text = strText;
                    item.Value = strValue;
                    this.DataListBox.Items.Add(item);
                    HistoryData(row["ClassID"].ToString(), div + 1);
                }
            }
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {

        string str = "'";
        int i = 0;

        #region 获取listbox控件选中的值
        foreach (ListItem li in DataListBox.Items)
        {
            if (li.Selected)
            {
                if (i > 0)
                {
                    str += ",'";
                }
                str += li.Value + "'";
                i++;
            }
        }
        if (str == "," || str == "'" || (str == null && str == ""))
        {
            PageError("请选择栏目！", "");
        }
        #endregion

        #region 自动产生Update组合
        string strUpdate = "";

        //获取数据字段，此字段与Page TextBox的Index值对等
        string[] ColumnsData = { "ClassTemplet", "ReadNewsTemplet" };
        //if (!allCheck.Checked)
        //{
        //    i = 0;
        //    foreach (Control cl in Controls[0].Controls)
        //    {
        //        if (cl.GetType().Name == "TextBox")
        //        {
        //            TextBox tb = (TextBox)this.FindControl(cl.ID);
        //            if (TextBoxValue(tb))
        //            {
        //                if (i > 0)
        //                {
        //                    strUpdate += ",";
        //                }
        //                strUpdate += "" + ColumnsData[i] + "='" + tb.Text + "'";
        //                i++;
        //            }
        //        }
        //    }
        //}
        //else
        //{
            i = 0;
            foreach (Control cl in Controls[0].Controls)
            {
                if (cl.GetType().Name == "TextBox")
                {
                    TextBox tb = (TextBox)this.FindControl(cl.ID);
                    if (i > 0)
                    {
                        strUpdate += ",";
                    }
                    strUpdate += "" + ColumnsData[i] + "='" + tb.Text + "'";
                    i++;
                }
            }
        //}

        #region 常规属性
        //string[] StrPram ={ "isComm", "NaviShowtf" };
        //if (StrPram.Length == checkeditem.Items.Count)
        //{
        //    string str_tab = "";
        //    for (int b = 0; b < checkeditem.Items.Count; b++)
        //    {
        //        if (checkeditem.Items[b].Selected)
        //        {
        //            if (strUpdate != null && strUpdate != "")
        //                str_tab = ",";
        //            strUpdate += str_tab + StrPram[b] + "=1";
        //        }
        //        else
        //        {
        //            if (strUpdate != null && strUpdate != "")
        //                str_tab = ",";
        //            strUpdate += str_tab + StrPram[b] + "=0";
        //        }
        //    }
        //}
        //else
        // PageError("属性参数不值不正确，请正确操作！", "class_list.aspx");
        #endregion
        #endregion

        #region SQL语句执行
        if (strUpdate != null && strUpdate != "")
        {
            rd.UpdateClassInfo(strUpdate, str);
            if (this.isContent.Checked)
            {
                if ((this.displaytemplets.Text).Trim() != "")
                {
                    //更新栏目下新闻模板
                    rd.UpdateClassNewsInfo(this.displaytemplets.Text, str);
                }
            }
            pd.SaveUserAdminLogs(1, 1, UserNum, "批量设置属性", "批量设置属性,栏目ClassID:" + str + "");
            PageRight("批量更新成功!如果更新了新闻模板，需要重新生成新闻", "class_list.aspx");
        }
        #endregion
    }

    /// <summary>
    /// 检测为空的所有TextBox
    /// </summary>
    /// <param name="TextBoxName"></param>
    /// <returns>如是为空false;反之true</returns>
    protected bool TextBoxValue(TextBox TextBoxName)
    {
        bool flg = true;
        //检测控件是否有值
        if (TextBoxName.Text == "" || TextBoxName.Text == null)
            flg = false;
        return flg;
    }
}
