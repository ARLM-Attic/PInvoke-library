using System;
using System.Data;
using System.Web.UI;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;
using Discuz.Common;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ����Ҫ��˵����� 
    /// </summary>
    
#if NET1
    public class auditingtopic : AdminPage
#else
    public partial class auditingtopic : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownTreeList forumid;
        protected Discuz.Control.TextBox poster;
        protected Discuz.Control.TextBox title;
        protected Discuz.Control.TextBox moderatorname;
        protected Discuz.Control.Calendar postdatetimeStart;
        protected Discuz.Control.Calendar postdatetimeEnd;
        protected Discuz.Control.Calendar deldatetimeStart;
        protected Discuz.Control.Calendar deldatetimeEnd;
        protected Discuz.Control.Button SearchTopicAudit;
        protected Discuz.Control.TextBox RecycleDay;
        protected Discuz.Control.Button DeleteRecycle;
        protected Discuz.Web.Admin.ajaxpostinfo AjaxPostInfo1;
        protected Discuz.Control.Hint Hint1;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            forumid.BuildTree(DatabaseProvider.GetInstance().GetForumsTree());
        }


        public void SearchTopicAudit_Click(object sender, EventArgs e)
        {
            #region ���ò�ѯ��������ת��audittopicgrid.aspx ������ʾ

            if (this.CheckCookie())
            {
                string sqlstring = DatabaseProvider.GetInstance().SearchTopicAudit(Utils.StrToInt(forumid.SelectedValue, 0), poster.Text, title.Text, moderatorname.Text, postdatetimeStart.SelectedDate,
                                                                                    postdatetimeEnd.SelectedDate, deldatetimeStart.SelectedDate, deldatetimeEnd.SelectedDate);
                Session["audittopicswhere"] = sqlstring;
                Response.Redirect("forum_audittopicgrid.aspx");
            }

            #endregion
        }        

        public void DeleteRecycle_Click(object sender, EventArgs e)
        {
            #region ����վ����ɾ��

            if (this.CheckCookie())
            {

                string topiclist = "";
                //DataTable dt = DbHelper.ExecuteDataset("SELECT [tid] FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [displayorder]=-1 AND [postdatetime]<='" + DateTime.Now.AddDays(-Convert.ToInt32(RecycleDay.Text)) + "'").Tables[0];
                DataTable dt = DatabaseProvider.GetInstance().GetTidForModeratormanagelogByPostdatetime(DateTime.Now.AddDays(-Convert.ToInt32(RecycleDay.Text)));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        topiclist += dr["tid"].ToString() + ",";
                    }
                    TopicAdmins.DeleteTopics(topiclist.Substring(0, topiclist.Length - 1), 0, false);
                }
                base.RegisterStartupScript( "PAGE","window.location.href='forum_auditingtopic.aspx';");
            }

            #endregion
        }

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SearchTopicAudit.Click += new EventHandler(this.SearchTopicAudit_Click);
            this.DeleteRecycle.Click += new EventHandler(this.DeleteRecycle_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

    }
}