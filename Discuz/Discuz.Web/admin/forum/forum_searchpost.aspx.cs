using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Discuz.Control;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;
using Discuz.Common;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ��������
    /// </summary>

#if NET1
    public class searchpost : AdminPage
#else
    public partial class searchpost : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownTreeList forumid;
        protected Discuz.Web.Admin.DropDownPost postlist;
        protected Discuz.Control.Calendar postdatetimeStart;
        protected Discuz.Control.Calendar postdatetimeEnd;
        protected Discuz.Control.TextBox poster;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox lowerupper;
        protected Discuz.Control.TextBox Ip;
        protected Discuz.Control.TextBox message;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button SaveConditionInf;
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


        private void SaveConditionInf_Click(object sender, EventArgs e)
        {
            #region ���ɲ�ѯ����

            if (this.CheckCookie())
            {
                string sqlstring = DatabaseProvider.GetInstance().SearchPost(Utils.StrToInt(forumid.SelectedValue, 0), postlist.SelectedValue, postdatetimeStart.SelectedDate, postdatetimeEnd.SelectedDate, poster.Text, lowerupper.Checked, Ip.Text, message.Text);
                Session["seachpost_fid"] = forumid.SelectedValue;
                //Session["posttablename"] = BaseConfigs.GetTablePrefix + "posts" + postlist.SelectedValue;
                Session["posttablename"] = BaseConfigs.GetTablePrefix + "posts" + DNTRequest.GetString("postlist:postslist");
                Session["postswhere"] = sqlstring;
                Response.Redirect("forum_postgridmanage.aspx");

            }

            #endregion
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
            this.SaveConditionInf.Click += new EventHandler(this.SaveConditionInf_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

    }
}