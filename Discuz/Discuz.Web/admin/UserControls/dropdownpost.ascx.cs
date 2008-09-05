using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{

    /// <summary>
    ///	�����ֱ�ؼ�
    /// </summary>

#if NET1
    public class DropDownPost : UserControl
#else
    public partial class DropDownPost : UserControl
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.DropDownList postslist;    
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region ��ʼ���ֱ�ؼ�

                postslist.Items.Clear();
                foreach (DataRow r in DatabaseProvider.GetInstance().GetDatechTableIds())
                {
                    postslist.Items.Add(new ListItem(BaseConfigs.GetTablePrefix + "posts" + r[0].ToString(), r[0].ToString()));
                }
                postslist.DataBind();
                postslist.SelectedValue = Posts.GetPostTableID();

                #endregion
            }
        }

        public string SelectedValue
        {
            get { return postslist.SelectedValue; }
            set { postslist.SelectedValue = value; }
        }

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}