using System;
using System.Web.UI.WebControls;

using Discuz.Forum;
using DataGrid = Discuz.Control.DataGrid;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ϵͳ�������б�
    /// </summary>
    
#if NET1
    public class sysadminusergroupgrid : AdminPage
#else
    public partial class sysadminusergroupgrid : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DataGrid DataGrid1;
        #endregion
#endif


        #region �ؼ�����

        protected CheckBox chkConfirmInsert;
        protected CheckBox chkConfirmUpdate;
        protected CheckBox chkConfirmDelete;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "ϵͳ���б�";
            DataGrid1.BindData(DatabaseProvider.GetInstance().GetSystemGroupInfoSql());
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.SortTable(e.SortExpression.ToString(), DatabaseProvider.GetInstance().GetSystemGroupInfoSql()); 
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
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
            DataGrid1.DataKeyField = "groupid";
            DataGrid1.ColumnSpan = 12;
        }

        #endregion
    }
}