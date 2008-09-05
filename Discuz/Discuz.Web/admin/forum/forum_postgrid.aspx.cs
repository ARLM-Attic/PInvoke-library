using System;
using System.Data;
using System.Web.UI.WebControls;

using Discuz.Forum;
using Discuz.Common;

using DataGrid = Discuz.Control.DataGrid;

namespace Discuz.Web.Admin
{
    /// <summary>
    /// �����б�
    /// </summary>

#if NET1
    public class postgrid : AdminPage
#else
    public partial class postgrid : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DataGrid DataGrid1;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            #region ���ݰ�

            DataGrid1.AllowCustomPaging = false;
            DataTable dt = AdminTopics.AdminGetPostList(DNTRequest.GetInt("tid", -1), DataGrid1.PageSize, DataGrid1.CurrentPageIndex + 1).Tables[0];

            foreach (DataRow dr in dt.Select("layer=0"))
            {
                dt.Rows.Remove(dr);
            }

            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();

            #endregion
        }


        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }


        protected void DataGrid_Cancel(Object sender, DataGridCommandEventArgs E)
        {
            DataGrid1.Cancel();
        }

        public string Invisible(string invisible)
        {
            if (invisible == "1")
            {
                return "<div align=center><img src=../images/OK.gif /></div>";
            }
            else
            {
                return "<div align=center><img src=../images/Cancel.gif /></div>";
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.Page_Load);

            DataGrid1.DataKeyField = "pid";
            DataGrid1.TableHeaderName = "�����б�";
            DataGrid1.ColumnSpan = 7;
        }

        #endregion
    }
}