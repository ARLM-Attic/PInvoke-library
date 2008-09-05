using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Discuz.Control;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using RadioButtonList = Discuz.Control.RadioButtonList;
using TextBox = Discuz.Control.TextBox;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Common;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ��������
    /// </summary>

#if NET1
    public class seachtopic : AdminPage
#else
    public partial class seachtopic : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownTreeList forumid;
        protected Discuz.Control.TextBox viewsmin;
        protected Discuz.Control.TextBox viewsmax;
        protected Discuz.Control.TextBox repliesmin;
        protected Discuz.Control.TextBox repliesmax;
        protected Discuz.Control.TextBox rate;
        protected Discuz.Control.Calendar postdatetimeStart;
        protected Discuz.Control.Calendar postdatetimeEnd;
        protected Discuz.Control.TextBox lastpost;
        protected Discuz.Control.TextBox poster;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox lowerupper;
        protected Discuz.Control.TextBox keyword;
        protected Discuz.Control.RadioButtonList displayorder;
        protected Discuz.Control.RadioButtonList digest;
        protected Discuz.Control.RadioButtonList attachment;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button SaveSearchCondition;
        protected Discuz.Control.DropDownList typeid;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                postdatetimeStart.SelectedDate = DateTime.Now.AddDays(-30);
                postdatetimeEnd.SelectedDate = DateTime.Now;
            }
            forumid.BuildTree(DatabaseProvider.GetInstance().GetForumsTree());
            forumid.TypeID.Items.RemoveAt(0);
            forumid.TypeID.Items.Insert(0, new ListItem("ȫ��", "0"));

        }

        #region ��VIEWSTATEд������

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            base.DiscuzForumSavePageState(viewState);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            return base.DiscuzForumLoadPageState();
        }

        #endregion

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveSearchCondition.Click += new EventHandler(this.SaveSearchCondition_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

        private void SaveSearchCondition_Click(object sender, EventArgs e)
        {
            #region ���ɲ�ѯ����

            if (this.CheckCookie())
            {
                string sqlstring = DatabaseProvider.GetInstance().SearchTopics(Utils.StrToInt(forumid.SelectedValue, 0), keyword.Text, displayorder.SelectedValue, digest.SelectedValue, attachment.SelectedValue,
poster.Text, lowerupper.Checked, viewsmin.Text, viewsmax.Text, repliesmax.Text, repliesmin.Text, rate.Text, lastpost.Text, postdatetimeStart.SelectedDate, postdatetimeEnd.SelectedDate);

                Session["topicswhere"] = sqlstring;
                Response.Redirect("forum_topicsgrid.aspx");
            }

            #endregion
        }
    }
}