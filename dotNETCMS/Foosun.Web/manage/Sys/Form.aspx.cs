using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Foosun.Web.manage.Sys
{
    public partial class Form : Foosun.Web.UI.ManagePage
    {
        public Form()
        {
            Authority_Code = "Q037";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChage);
        }
        private void PageNavigator1_PageChage(object sender, int PageIndex)
        {
            DataListBind(PageIndex);
        }
        private void DataListBind(int PageIndex)
        {
            int nRCount, nPCount;
            DataTable tb = Foosun.CMS.Pagination.GetPage(this.GetType().Name, PageIndex, 20, out nRCount, out nPCount);
            this.PageNavigator1.PageCount = nPCount;
            this.PageNavigator1.PageIndex = PageIndex;
            this.PageNavigator1.RecordCount = nRCount;
            this.RptData.DataSource = tb;
            this.RptData.DataBind();
        }
    }
}
