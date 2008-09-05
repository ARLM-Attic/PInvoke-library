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

namespace Foosun.Web.configuration.system
{
    public partial class selectspecial  : Foosun.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Foosun.CMS.AdminGroup agc = new Foosun.CMS.AdminGroup();
                DataTable dc = agc.getClassList("SpecialID,SpecialCName,ParentID", "news_special", " Where isLock=0 And isRecyle=0 And SiteID='" + SiteID + "' ");
                listShow(dc, "0", 0, Special);
                dc.Clear(); dc.Dispose();
            }
        }

        /// <summary>
        /// 在ListBox中呈现出来
        /// </summary>
        /// <param name="tempdt">DataTable</param>
        /// <param name="PID">父类编号</param>
        /// <param name="Layer">层次</param>
        /// <param name="list">ListBox控件名称</param>

        protected void listShow(DataTable tempdt, string PID, int Layer, HtmlSelect list)
        {
            DataRow[] row = null;
            row = tempdt.Select("ParentID='" + PID + "'");
            if (row.Length < 1)
                return;
            else
            {
                foreach (DataRow r in row)
                {
                    string strText = "┝";
                    for (int j = 0; j < Layer; j++)
                    {
                        strText += "┉";
                    }
                    ListItem itm = new ListItem();
                    itm.Value = r[0].ToString() + "|" + r[1].ToString();
                    itm.Text = strText + r[1].ToString();
                    list.Items.Add(itm);
                    if (r[0].ToString() != "0")
                        listShow(tempdt, r[0].ToString(), Layer + 1, list);
                }
            }
        }
    }
}
