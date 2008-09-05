using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Common;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using DataGrid = Discuz.Control.DataGrid;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// �ֶ��������
    /// </summary>
     
#if NET1
    public class forumsgrid : AdminPage
#else
    public partial class forumsgrid : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DataGrid DataGrid1;
		protected Discuz.Control.Button SaveForum;
        #endregion
#endif

        #region �ؼ�����

        protected Button SysteAutoSet;

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
            #region ������
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.TableHeaderName = "��̳�б�";
            DataGrid1.DataSource = buildGridData();
            DataGrid1.DataBind();
            #endregion
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataView dv = new DataView(buildGridData());
            dv.Sort = e.SortExpression.ToString();
            DataGrid1.DataSource = dv;
            DataGrid1.DataBind();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }

        private void SaveForum_Click(object sender, EventArgs e)
        {
            #region �������޸���Ϣ
            int row = -1;
            bool error = false;
            foreach (object o in DataGrid1.GetKeyIDArray())
            {
                int fid = int.Parse(o.ToString());
                string name = DataGrid1.GetControlValue(row, "name").Trim();
                string subforumcount = DataGrid1.GetControlValue(row, "subforumcount").Trim();
                string displayorder = DataGrid1.GetControlValue(row, "displayorder").Trim();
                if (name == "" || !Utils.IsNumeric(subforumcount) || !Utils.IsNumeric(displayorder))
                {
                    error = true;
                    continue;
                }
                DatabaseProvider.GetInstance().UpdateForum(fid, name, int.Parse(subforumcount), int.Parse(displayorder));
                row++;
            }
            Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/UI/ForumListBoxOptions");
            Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/ForumList");
            if(error)
                base.RegisterStartupScript("PAGE", "alert('ĳЩ��¼ȡֵ����ȷ��δ�ܱ����£�');window.location.href='forum_forumsgrid.aspx';");
            else
                base.RegisterStartupScript("PAGE", "window.location.href='forum_forumsgrid.aspx';");
            #endregion
        }

        private void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            #region ���ݰ���ʾ���ȿ���

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox t = (TextBox)e.Item.Cells[1].Controls[0];
                t.Attributes.Add("maxlength", "50");
                t.Width = 80;

                t = (TextBox)e.Item.Cells[5].Controls[0];
                t.Attributes.Add("maxlength", "8");
                t.Width = 30;

                t = (TextBox)e.Item.Cells[6].Controls[0];
                t.Attributes.Add("maxlength", "8");
                t.Width = 30;
            }

            #endregion
        }

        private DataTable buildGridData()
        {
            #region ���ݰ�

            DataTable dt = AdminForums.GetAllForumList();
            foreach (DataRow dr in dt.Rows)
            {
                dr["parentidlist"] = dr["parentidlist"].ToString().Trim();
                dr["name"] = dr["name"].ToString().Trim().Replace("\"", "'");
            }
            return dt;

            #endregion
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveForum.Click += new EventHandler(this.SaveForum_Click);
            this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid_ItemDataBound);
            this.Load += new EventHandler(this.Page_Load);

            DataGrid1.DataKeyField = "fid";
            DataGrid1.TableHeaderName = "��̳�б�";
            DataGrid1.AllowPaging = false;
            DataGrid1.ShowFooter = false;
            DataGrid1.SaveDSViewState = true;
        }

        #endregion

    }
}