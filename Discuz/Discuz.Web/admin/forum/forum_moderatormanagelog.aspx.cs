using System;
using System.Data;
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
    /// ����������־�б�
    /// </summary>

#if NET1
    public class moderatormanagelog : AdminPage
#else
    public partial class moderatormanagelog : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.Calendar postdatetimeStart;
        protected Discuz.Control.Calendar postdatetimeEnd;
        protected Discuz.Control.TextBox others;
        protected Discuz.Control.TextBox Username;
        protected Discuz.Control.Button SearchLog;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.DataGrid DataGrid1;
        protected Discuz.Control.TextBox deleteNum;
        protected Discuz.Control.Calendar deleteFrom;
        protected Discuz.Control.Button DelRec;
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
            #region ���ݰ�

            DataGrid1.AllowCustomPaging = true;
            DataGrid1.VirtualItemCount = GetRecordCount();

            if (ViewState["condition"] == null)
            {
                DataGrid1.DataSource = AdminModeratorLogs.LogList(DataGrid1.PageSize, DataGrid1.CurrentPageIndex + 1);
            }
            else
            {
                DataGrid1.DataSource = AdminModeratorLogs.LogList(DataGrid1.PageSize, DataGrid1.CurrentPageIndex + 1, ViewState["condition"].ToString());
            }
            DataGrid1.DataBind();

            #endregion
        }

        private int GetRecordCount()
        {
            #region �õ���־��¼��

            if (ViewState["condition"] == null)
            {
                return AdminModeratorLogs.RecordCount();
            }
            else
            {
                return AdminModeratorLogs.RecordCount(ViewState["condition"].ToString());
            }

            #endregion
        }


        private void DelRec_Click(object sender, EventArgs e)
        {
            #region ɾ��ָ����������־��Ϣ

            if (this.CheckCookie())
            {
                string condition = "";
                //switch (Request.Form["deleteMode"])
                //{
                //    case "chkall":
                //        if (DNTRequest.GetString("id") != "")
                //            condition = " [id] IN(" + DNTRequest.GetString("id") + ")";
                //        break;
                //    case "deleteNum":
                //        if (deleteNum.Text != "" && Utils.IsNumeric(deleteNum.Text))
                //            condition = " [id] not in (select top " + deleteNum.Text + " [id] from [" + BaseConfigs.GetTablePrefix + "moderatormanagelog] order by [id] desc)";
                //        break;
                //    case "deleteFrom":
                //        if (deleteFrom.SelectedDate.ToString() != "")
                //            condition = " [postdatetime]<'" + deleteFrom.SelectedDate.ToString() + "'";
                //        break;
                //}
                condition = DatabaseProvider.GetInstance().DelModeratorManageCondition(Request.Form["deleteMode"].ToString(), DNTRequest.GetString("id").ToString(), deleteNum.Text.ToString(), deleteFrom.SelectedDate.ToString());
                if (condition != "")
                {
                    AdminModeratorLogs.DeleteLog(condition);
                    Response.Redirect("forum_moderatormanagelog.aspx");
                }
                else
                {
                    base.RegisterStartupScript( "", "<script>alert('��δѡ���κ�ѡ��������������');window.location.href='forum_moderatormanagelog.aspx';</script>");
                }
            }

            #endregion
        }

        public string GroupName(string groupid)
        {
            #region ͨ����ID��ȡ�������

            DataTable dt = DatabaseProvider.GetInstance().GroupNameTable(groupid);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString().Trim();
            }
            return "";

            #endregion
        }

        private void SearchLog_Click(object sender, EventArgs e)
        {
            #region ��ָ����ѯ����������־��Ϣ

            if (this.CheckCookie())
            {
                string sqlstring = DatabaseProvider.GetInstance().SearchModeratorManageLog(postdatetimeStart.SelectedDate, postdatetimeEnd.SelectedDate, Username.Text, others.Text);

                ViewState["condition"] = sqlstring;
                DataGrid1.CurrentPageIndex = 0;
                BindData();
            }

            #endregion
        }


        protected void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }

        public void GoToPagerButton_Click(object sender, EventArgs e)
        {
            BindData();
        }


        private void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.Cells[9].Text.ToString().Length > 8)
            {
                e.Item.Cells[9].Text = Utils.HtmlEncode(e.Item.Cells[9].Text.Substring(0, 8)) + "��";
            }
        }

        public string BoolStr(string closed)
        {
            if (closed == "1")
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
            this.SearchLog.Click += new EventHandler(this.SearchLog_Click);
            this.DelRec.Click += new EventHandler(this.DelRec_Click);
            this.DataGrid1.GoToPagerButton.Click += new EventHandler(GoToPagerButton_Click);
            DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid_ItemDataBound);

            this.Load += new EventHandler(this.Page_Load);

            DataGrid1.TableHeaderName = "����������־��¼";
            DataGrid1.AllowSorting = false;
            DataGrid1.ColumnSpan = 9;
        }

        #endregion
    }
}