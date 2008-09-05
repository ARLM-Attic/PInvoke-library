using System;
using System.Web.UI.WebControls;

using Discuz.Control;
using Discuz.Forum;
using Button = Discuz.Control.Button;
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
    public class searchattchment : AdminPage
#else
    public partial class searchattchment : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownTreeList forumid;
        protected Discuz.Control.TextBox filesizemin;
        protected Discuz.Control.TextBox filesizemax;
        protected Discuz.Control.TextBox downloadsmin;
        protected Discuz.Control.TextBox downloadsmax;
        protected Discuz.Control.TextBox postdatetime;
        protected Discuz.Control.TextBox filename;
        protected Discuz.Control.TextBox description;
        protected Discuz.Control.TextBox poster;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button SaveSearchCondition;
        #endregion
#endif


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }


        private void SaveSearchCondition_Click(object sender, EventArgs e)
        {
            #region ���ɲ�ѯ����

            if (this.CheckCookie())
            {
                string sqlstring = DatabaseProvider.GetInstance().SearchAttachment(Utils.StrToInt(forumid.SelectedValue, 0), Posts.GetPostTableName(), filesizemin.Text, filesizemax.Text, downloadsmin.Text, downloadsmax.Text, postdatetime.Text, filename.Text, description.Text, poster.Text);

                Session["attchmentwhere"] = sqlstring;
                Response.Redirect("forum_attchemntgrid.aspx");
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
            this.SaveSearchCondition.Click += new EventHandler(this.SaveSearchCondition_Click);
            this.Load += new EventHandler(this.Page_Load);
            //forumid.BuildTree("SELECT [fid],[name],[parentid] FROM [" + BaseConfigs.GetTablePrefix + "forums] ORDER BY [displayorder] ASC");
            forumid.BuildTree(DatabaseProvider.GetInstance().GetForumsTree());
            forumid.TypeID.Items.RemoveAt(0);
            forumid.TypeID.Items.Insert(0, new ListItem("ȫ��", "0"));

        }

        #endregion

    }
}