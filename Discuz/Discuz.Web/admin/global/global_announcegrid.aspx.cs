using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using DataGrid = Discuz.Control.DataGrid;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// �����б�
    /// </summary>
    
#if NET1
    public class announcegrid : AdminPage
#else
    public partial class announcegrid : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox poster;
        protected Discuz.Control.TextBox title;
        protected Discuz.Control.Calendar postdatetimeStart;
        protected Discuz.Control.Calendar postdatetimeEnd;
        protected Discuz.Control.Button Search;
        protected Discuz.Control.Button DelRec;
        protected Discuz.Control.DataGrid DataGrid1;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                postdatetimeStart.SelectedDate = DateTime.Now.AddDays(-30);
                postdatetimeEnd.SelectedDate = DateTime.Now;
                BindData();
            }
        }

        public void BindData()
        {
            DataGrid1.AllowCustomPaging = false;
            DataGrid1.BindData(DatabaseProvider.GetInstance().GetAnnouncements());
        }

        protected void Sort_Grid(Object sender, DataGridSortCommandEventArgs e)
        {
            DataGrid1.Sort = e.SortExpression.ToString();
        }

        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.LoadCurrentPageIndex(e.NewPageIndex);
        }

        private void DelRec_Click(object sender, EventArgs e)
        {
            #region ɾ������

            if (this.CheckCookie())
            {
                if (DNTRequest.GetString("id") != "")
                {
                    DatabaseProvider.GetInstance().DeleteAnnouncements(DNTRequest.GetString("id"));

                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/AnnouncementList");

                    Discuz.Cache.DNTCache.GetCacheService().RemoveObject("/Forum/SimplifiedAnnouncementList");

                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "ɾ������", "ɾ������,����IDΪ: " + DNTRequest.GetString("id"));

                    Response.Redirect("global_announcegrid.aspx");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��');window.location.href='global_announcegrid.aspx';</script>");
                }
            }

            #endregion
        }


        private void Search_Click(object sender, EventArgs e)
        {
            #region ��������������

            if (this.CheckCookie())
            {
                StringBuilder builder = new StringBuilder();
                if (!this.poster.Text.Equals(""))
                {
                    builder.Append("[poster] LIKE '%");
                    builder.Append(this.poster.Text);
                    builder.Append("%'");
                }

                if (!this.title.Text.Equals(""))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.Append("[title] LIKE '%");
                    builder.Append(this.title.Text);
                    builder.Append("%'");
                }

                if (!this.postdatetimeStart.SelectedDate.ToString().Equals(""))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.Append("[starttime] >= '");
                    builder.Append(this.postdatetimeStart.SelectedDate.ToString());
                    builder.Append("'");
                }

                if (!this.postdatetimeEnd.SelectedDate.AddDays(1).ToString().Equals(""))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.Append("[starttime] <= '");
                    builder.Append(this.postdatetimeEnd.SelectedDate.ToString());
                    builder.Append("'");
                }

                if (builder.Length > 0)
                {
                    builder.Insert(0, " WHERE ");
                }

                DataGrid1.BindData("SELECT * FROM [" + BaseConfigs.GetTablePrefix + "announcements] " + builder.ToString());
            }

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
            this.Search.Click += new EventHandler(this.Search_Click);
            this.DelRec.Click += new EventHandler(this.DelRec_Click);
            this.Load += new EventHandler(this.Page_Load);

            DataGrid1.TableHeaderName = "�����б�";
            DataGrid1.ColumnSpan = 7;
        }

        #endregion

    }
}